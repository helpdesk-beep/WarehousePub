using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_BO_Financed_WHR_Entry : System.Web.UI.Page
{
    public SqlConnection conStr2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string PFID = "";
    SqlTransaction sqltran;
    int a_id = 0;
    SqlCommand cmd;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        CalendarExtender1.EndDate = DateTime.Now;
        CalendarExtender2.EndDate = DateTime.Now;
        if (!IsPostBack)
        {
            fillGodownDetails();
            fillCommodity();
            if (!String.IsNullOrEmpty(Session["UserId"].ToString()))
            {
                fillGrid();
            }
            if (Session["hdnID"] != null)
            {
                if (!String.IsNullOrEmpty(Session["hdnID"].ToString()))
                {
                    FillData();

                }
            }
        }
    }
    public void fillGodownDetails()
    {
        string conStr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Name", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
            con.Open();

            ddl_gdwn.DataSource = cmd.ExecuteReader();
            ddl_gdwn.DataTextField = "Godown_Name";
            ddl_gdwn.DataValueField = "Godown_ID";
            ddl_gdwn.DataBind();
            ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con.Close();
        }
    }
    public void fillCommodity()
    {
        string conStr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Storage_Commodity", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();

            ddlCommodity.DataSource = cmd.ExecuteReader();
            ddlCommodity.DataTextField = "Commodity_Name";
            ddlCommodity.DataValueField = "Commodity_Id";
            ddlCommodity.DataBind();
            ddlCommodity.Items.Insert(0, new ListItem("-- Select Commodity --", "0"));
            con.Close();
        }
    }
    protected void fillGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Financed_WHR_Entry", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["UserId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GrdOfficerPreviousInsp.DataSource = dt;
                            GrdOfficerPreviousInsp.DataBind();
                        }
                        else
                        {

                            GrdOfficerPreviousInsp.DataSource = null;
                            GrdOfficerPreviousInsp.DataBind();
                        }
                    }
                }
            }
        }
    }
    public void FillData()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Financed_WHR_Entry_By_ID", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", Session["hdnID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            ddl_gdwn.SelectedValue = dt.Rows[0]["Godown_No"].ToString();
                            txtReceiptNo.Text = dt.Rows[0]["WH_Receipt_No"].ToString();
                            txtDate.Text = dt.Rows[0]["Date"].ToString();
                            ddlCommodity.SelectedValue = dt.Rows[0]["Commodity_ID"].ToString();
                            txtBags.Text = dt.Rows[0]["Bags"].ToString();
                            txtWeight.Text = dt.Rows[0]["Weight"].ToString();
                            txtLetterDate.Text = dt.Rows[0]["Letter_Date"].ToString();
                            txtName.Text = dt.Rows[0]["Bank_Self_Name"].ToString();
                            hdnHID.Value = dt.Rows[0]["ID"].ToString();
                            ddl_gdwn.Enabled = false;
                           // txtReceiptNo.Enabled = false;
                            btn_addnewoff.Text = "Update";
                            Session["hdnID"] = "0";
                        }
                        else
                        {

                            ddl_gdwn.SelectedValue = "0";
                            txtReceiptNo.Text = "";
                            txtDate.Text = "";
                            ddlCommodity.SelectedValue = "0";
                            txtBags.Text = "";
                            txtWeight.Text = "";
                            txtLetterDate.Text = "";
                            txtName.Text = "";
                            ddl_gdwn.Enabled = true;
                           // txtReceiptNo.Enabled = true;
                            hdnHID.Value = "0";
                        }
                    }
                }
            }
        }
    }
    public void Clear()
    {
        ddl_gdwn.SelectedValue = "0";
        txtReceiptNo.Text = "";
        txtDate.Text = "";
        ddlCommodity.SelectedValue = "0";
        txtBags.Text = "";
        txtWeight.Text = "";
        txtLetterDate.Text = "";
        txtName.Text = "";
        ddl_gdwn.Enabled = true;
        //txtReceiptNo.Enabled = true;
        hdnHID.Value = "0";
    }
    public void checkvalidation()
    {
        if (ddl_gdwn.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('गोदाम का नाम चुनें')", true);
            ddl_gdwn.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtReceiptNo.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('वेयर हाउस रशीद क्रमांक रिक्त नहीं हो सकता है')", true);
            txtReceiptNo.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtDate.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('दिनाँक रिक्त नहीं हो सकता है')", true);
            txtDate.Focus();
            return;
        }
        if (ddlCommodity.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('स्कंध चुनें')", true);
            ddlCommodity.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtBags.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('बोरे रिक्त नहीं हो सकता है')", true);
            txtBags.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtWeight.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('बजन की मात्रा रिक्त नहीं हो सकता है')", true);
            txtWeight.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtLetterDate.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('लयन पत्र की तिथि रिक्त नहीं हो सकता है')", true);
            txtLetterDate.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtName.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('बैंक/ निजी व्यक्ति का नाम जिसके पास रहन रखी गई हो रिक्त नहीं हो सकता है')", true);
            txtName.Focus();
            return;
        }
    }
    protected void btnsaveprofile_Click(object sender, EventArgs e)
    {
        try
        {
            checkvalidation();
            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Insert_Financed_WHR_Entry", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@ID", hdnHID.Value);
                cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@Godown_No", ddl_gdwn.SelectedValue);
                cmd.Parameters.AddWithValue("@WH_Receipt_No", txtReceiptNo.Text);
                cmd.Parameters.AddWithValue("@Date", txtDate.Text);
                cmd.Parameters.AddWithValue("@Commodity_ID", ddlCommodity.SelectedValue);
                cmd.Parameters.AddWithValue("@Bags", txtBags.Text);
                cmd.Parameters.AddWithValue("@Weight", txtWeight.Text);
                cmd.Parameters.AddWithValue("@Bank_Self_Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Letter_Date", txtLetterDate.Text);
                cmd.Parameters.AddWithValue("@Insert_By", IPAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Details of Financed whr in bank Successfully Submitted|||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    Clear();
                    fillGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
                }
                //fillGrid();
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void GrdOfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Overallinsp")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];
            //Fetch value of Name.           
            string hdnID = (row.FindControl("hdnID") as HiddenField).Value;
            Session["hdnID"] = hdnID.ToString();
            Response.Redirect("~/Inspections/BO/Financed_WHR_Entry.aspx");

        }
        if (e.CommandName == "DeleteRecord")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];

            //Fetch value of Name.
            string hdnId = (row.FindControl("hdnId") as HiddenField).Value;
            string lblWH_Receipt_No = (row.FindControl("lblWH_Receipt_No") as Label).Text;
            Session["hdnId"] = hdnId.ToString();
            Session["lblWH_Receipt_No"] = lblWH_Receipt_No.ToString();
            RemoveRowWHR(hdnId, lblWH_Receipt_No);
            // RemoveRowJVS(hdnId);

        }
    }
    public void RemoveRowWHR(string id,string WHRID)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();
            }

            SqlCommand cmd = new SqlCommand("Delete_Financed_WHR_Entry", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@WH_Receipt_No", WHRID.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                fillGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }
    protected void GrdOfficerPreviousInsp_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}