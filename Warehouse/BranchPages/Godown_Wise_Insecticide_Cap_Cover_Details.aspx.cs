using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_Godown_Wise_Insecticide_Cap_Cover_Details : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            fillGodowntype();
            BindGrid();
            txtInsecticideName.Text = "Alluminium Phosphide";
            txtInsecticideName.ReadOnly = true;
            FillClosingBalance();
        }
    }
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    // Retrieving values from the form inputs
    //    string insecticideName = txtInsecticideName.Text.Trim();
    //    string closingBalance = txtClosingBalance.Text.Trim();
    //    string godownType = ddlGodownType.SelectedValue;
    //    string godownID = ddlGodown.SelectedValue;
    //    string totalCapCover = txtTotalCapCover.Text.Trim();

    //    // Example: Simple validation check
    //    if (!string.IsNullOrEmpty(insecticideName) && !string.IsNullOrEmpty(godownID))
    //    {
    //        // TODO: Save to database or perform business logic here

    //        // Optional: Clear form after success
    //        ClearForm();
    //    }
    //}
    private void fillGodowntype()
    {
        try
        {
            string query = "";
            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select Distinct Hired_Type from Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 Where BranchID ='" + Session["BranchId"].ToString() + "' Order By Hired_Type ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodownType.DataSource = ds.Tables[0];
                ddlGodownType.DataTextField = "Hired_Type";
                ddlGodownType.DataValueField = "Hired_Type";
                ddlGodownType.DataBind();
                ddlGodownType.Items.Insert(0, "Select");
            }
            else
            {
                ddlGodownType.Items.Clear();
                ddlGodownType.Items.Insert(0, "Select");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void ClearForm()
    {
        txtInsecticideName.Text = string.Empty;
        txtClosingBalance.Text = string.Empty;
        ddlGodownType.SelectedIndex = 0;
        ddlGodown.SelectedIndex = 0;
        txtTotalCapCover.Text = string.Empty;
    }
    private void fillGodown()
    {
        try
        {

            if (ddlGodownType.SelectedIndex <= 0)
            {
                ddlGodown.Items.Clear();
                ddlGodown.Items.Insert(0, new ListItem("Select", "0"));
                return;
            }
            string query = "";
            query = "Select Godown_ID,Godown_Name from Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 Where BranchID ='" + Session["BranchId"].ToString() + "' And Hired_Type ='" + ddlGodownType.SelectedValue + "' Order By Godown_Name ASC";

            cmd = new SqlCommand(query, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            ddlGodown.Items.Clear();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
            }

            // Data ho ya na ho Select hamesha rahega
            ddlGodown.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception)
        {
            ddlGodown.Items.Clear();
            ddlGodown.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    //protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillGodown();
    //}
    private void FillClosingBalance()
    {
        try
        {
            string qry = @"SELECT TOP 1 Closing_Blance
                       FROM tbl_Branch_Wise_Closing_Blance
                       WHERE Branch_ID = @BranchID";

            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtClosingBalance.Text = dt.Rows[0]["Closing_Blance"].ToString();

                // Value mili hai to readonly kar do
                txtClosingBalance.ReadOnly = true;
            }
            else
            {
                // Record nahi mila to blank rahe
                txtClosingBalance.Text = "";
                txtClosingBalance.ReadOnly = false;
            }
        }
        catch
        {
            txtClosingBalance.Text = "";
            txtClosingBalance.ReadOnly = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string ipAddress;
            ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];

            if (ddlGodownType.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                    "alert('Please Select Godown Type');", true);
                return;
            }

            // Godown validation only when Godown Type is NOT Owned
            if (ddlGodownType.SelectedItem.Text.Trim().ToUpper() != "OWNED")
            {
                if (ddlGodown.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                        "alert('Please Select Godown');", true);
                    return;
                }
            }

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Insert_Godown_Wise_Cap_Cover_Details", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
                    cmd.Parameters.AddWithValue("@InsecticideName", txtInsecticideName.Text.Trim());
                    cmd.Parameters.AddWithValue("@ClosingBalance", Convert.ToDecimal(txtClosingBalance.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Godown_Type", ddlGodownType.SelectedValue);

                    // Owned ke case me GodownID 0 ya NULL pass karo
                    if (ddlGodownType.SelectedItem.Text.ToUpper() == "OWNED")
                    {
                        cmd.Parameters.AddWithValue("@GodownID", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue);
                    }

                    cmd.Parameters.AddWithValue("@TotalCapCover", Convert.ToDecimal(txtTotalCapCover.Text.Trim()));
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["BranchId"].ToString());
                    cmd.Parameters.AddWithValue("@Created_Byip", ipAddress);
                    cmd.Parameters.AddWithValue("@CreatedBy_IP", ipAddress);

                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        string Status = dt.Rows[0]["Status"].ToString();
                        string Msg = dt.Rows[0]["Msg"].ToString();

                        ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                            "alert('" + Msg.Replace("'", "") + "');", true);

                        if (Status == "1")
                        {
                            ClearForm();

                            txtInsecticideName.Text = "Alluminium Phosphide";
                            txtInsecticideName.ReadOnly = true;

                            FillClosingBalance();
                            BindGrid();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                "alert('" + ex.Message.Replace("'", "") + "');", true);
        }
    }
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCapCoverDetails();
    }
    private void FillCapCoverDetails()
    {
        try
        {
            txtTotalCapCover.Text = "";

            string qry = @"SELECT TOP 1 Total_Cap_Cover
                       FROM tbl_Godown_Wise_Cap_Cover_Details
                       WHERE Godown_ID = @GodownID";

            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtTotalCapCover.Text = dt.Rows[0]["Total_Cap_Cover"].ToString();

                btnSubmit.Text = "Update Details";

                // Optional
                ViewState["Mode"] = "UPDATE";
            }
            else
            {
                txtTotalCapCover.Text = "";

                btnSubmit.Text = "Save Details";

                ViewState["Mode"] = "INSERT";
            }
        }
        catch
        {
            btnSubmit.Text = "Save Details";
        }
    }
    private void BindGrid()
    {
        try
        {
            string qry = @"
       SELECT
    B.Insecticide_Name,
        G.Godown_Type,
    ISNULL(M.Godown_Name,'Owned Godown') AS Godown_Name,
    G.Total_Cap_Cover
        FROM tbl_Godown_Wise_Cap_Cover_Details G
            LEFT JOIN Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 M
             ON G.Godown_ID COLLATE Latin1_General_CI_AS =
                 M.Godown_ID COLLATE Latin1_General_CI_AS
                 LEFT JOIN tbl_Branch_Wise_Closing_Blance B
                ON G.Branch_ID = B.Branch_ID
            WHERE G.Branch_ID = @BranchID
            ORDER BY M.Godown_Name DESC";

            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvCapCoverDetails.DataSource = dt;
            gvCapCoverDetails.DataBind();
        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGodownType.SelectedItem.Text.ToUpper() == "OWNED")
        {
            // Godown dropdown hide
            pnlGodown.Visible = false;

            // Clear selection
            ddlGodown.SelectedIndex = 0;
        }
        else
        {
            // Show dropdown for Hired/CWC etc.
            pnlGodown.Visible = true;

            // Existing code to bind godowns
            fillGodown();
        }
    }
}
