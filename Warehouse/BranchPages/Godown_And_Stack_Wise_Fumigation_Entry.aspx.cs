using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_Godown_And_Stack_Wise_Fumigation_Entry : System.Web.UI.Page
{
    string connStr = ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        // Session Check
        if (Session["BranchId"] == null || string.IsNullOrEmpty(Session["BranchId"].ToString()))
        {
            Response.Redirect("~/Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
            return;
        }

        if (!IsPostBack)
        {
            BindGrid();
            fillGodown();
            BindAlpBrands();

            DateTime maxDate = new DateTime(2026, 6, 8);

            txtlastFumigationDate.Attributes["max"] = maxDate.ToString("yyyy-MM-dd");
            txtFumigationDate.Attributes["max"] = maxDate.ToString("yyyy-MM-dd");

        }

    }
    private void fillGodown()
    {
        try
        {
            string query = "";
            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select Godown_ID,Godown_Name from Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 Where BranchID ='" + Session["BranchId"].ToString() + "' Order By Godown_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
                ddlGodown.Items.Insert(0, "Select");
            }
            else
            {
                ddlGodown.Items.Clear();
                ddlGodown.Items.Insert(0, "Select");
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    // 2. Insecticide Brands Dropdown Populate krna
    private void BindAlpBrands()
    {
        ddlAlpBrand.Items.Clear();
        ddlAlpBrand.Items.Add(new ListItem("-- Select Insecticide Brand --", ""));
        ddlAlpBrand.Items.Add(new ListItem("QuickPhos (Aluminium Phosphide)", "B1"));
        ddlAlpBrand.Items.Add(new ListItem("Celphos", "B2"));
        ddlAlpBrand.Items.Add(new ListItem("Alphos", "B3"));
    }

    // 3. Jese hi Godown select hoga, Stacks load honge
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedGodown = ddlGodown.SelectedValue;

        // Clear previous selections & fields
        ddlStack.Items.Clear();
        txtStackName.Text = "";
        txtQuantity.Text = "";

        try
        {
            string query = "";

            query = @"SELECT 
                    Stack_ID,
                    Stack_ID + ' - ' + Stack_Name AS Stack_Display
                  FROM Intergrated_MP_STORAGE.dbo.tbl_MetaData_STACK
                  WHERE Stack_Killed='N' And Godown_ID = '" + selectedGodown + @"'
                  ORDER BY Stack_Name ASC";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlStack.DataSource = ds.Tables[0];
                ddlStack.DataTextField = "Stack_Display"; // Dropdown me Stack ID + Stack Name dikhega
                ddlStack.DataValueField = "Stack_ID";     // SelectedValue me sirf Stack_ID jayega
                ddlStack.DataBind();

                ddlStack.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlStack.Items.Clear();
                ddlStack.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            // Error Handling
        }
    }

    // 4. Jese hi Stack select hoga, Name aur Quantity load hogi
    protected void ddlStack_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedStackId = ddlStack.SelectedValue;

        // Agar default select kiya to fields clear ho jayein
        if (string.IsNullOrEmpty(selectedStackId))
        {
            txtStackName.Text = "";
            txtQuantity.Text = "";
            return;
        }

        // Database se Stack_Name aur Stack_capacity nikalne ki query
        // SQL Injection se bachne ke liye parameterized query use ki hai WHERE clause me
        string query = "SELECT Stack_Name, Stack_capacity FROM Intergrated_MP_STORAGE.dbo.tbl_MetaData_STACK WHERE Stack_ID = @StackID";

        using (SqlConnection con = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // Yahan assume kiya hai ki database me column 'Stack_ID' ya jo bhi pkey hai usse match kar rahe hain
                cmd.Parameters.AddWithValue("@StackID", selectedStackId);

                try
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Database se value nikal kar textbox me fill karna
                            txtStackName.Text = reader["Stack_Name"].ToString();
                            txtQuantity.Text = reader["Stack_capacity"].ToString();
                        }
                        else
                        {
                            // Agar record nahi mila
                            txtStackName.Text = "Not Found";
                            txtQuantity.Text = "0";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Error handling (Testing ke liye alert show karega)
                    Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
                }
            }
        }
    }

    // 5. Final Submit Button Click
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlGodown.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                    "alert('Please Select Godown.');", true);
                return;
            }

            if (ddlStack.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                    "alert('Please Select Stack.');", true);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtlastFumigationDate.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                    "alert('Please Select Last Fumigation Date.');", true);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtFumigationDate.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                    "alert('Please Select Fumigation Date.');", true);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAlpQty.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                    "alert('Please Enter ALP Quantity.');", true);
                return;
            }

            SqlCommand cmd = new SqlCommand("sp_Insert_Stack_Wise_Fumigation_Entry_By_Branch", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
            cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue);
            cmd.Parameters.AddWithValue("@Stack_ID", ddlStack.SelectedValue);
            cmd.Parameters.AddWithValue("@Stack_Name", txtStackName.Text.Trim());
            cmd.Parameters.AddWithValue("@Available_Qty", Convert.ToDecimal(txtQuantity.Text.Trim()));
            cmd.Parameters.AddWithValue("@Last_Fumigation_Date", Convert.ToDateTime(txtlastFumigationDate.Text));
            cmd.Parameters.AddWithValue("@Fumigation_Date", Convert.ToDateTime(txtFumigationDate.Text));
            cmd.Parameters.AddWithValue("@ALP_Qty", Convert.ToInt32(txtAlpQty.Text.Trim()));
            cmd.Parameters.AddWithValue("@Insecticide_Name", ddlAlpBrand.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@CreatedBy", Session["BranchId"].ToString());
            cmd.Parameters.AddWithValue("@CreatedBy_IP", Request.UserHostAddress);

            if (con.State != ConnectionState.Open)
                con.Open();

            cmd.ExecuteNonQuery();

            ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                "alert('Record Saved Successfully.');", true);
            BindGrid();
            ClearControls();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                "alert('Error : " + ex.Message.Replace("'", "") + "');", true);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }
    private void ClearControls()
    {
        ddlGodown.SelectedIndex = 0;

        ddlStack.Items.Clear();
        ddlStack.Items.Insert(0, new ListItem("Select", "0"));

        txtStackName.Text = "";
        txtQuantity.Text = "";
        txtlastFumigationDate.Text = "";
        txtFumigationDate.Text = "";
        txtAlpQty.Text = "";

        if (ddlAlpBrand.Items.Count > 0)
            ddlAlpBrand.SelectedIndex = 0;
    }

    private void BindGrid()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Get_Stack_Wise_Fumigation_Entry_By_Branch", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvFumigation.DataSource = dt;
            gvFumigation.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(),
                "msg",
                "alert('" + ex.Message.Replace("'", "") + "');",
                true);
        }
    }
}