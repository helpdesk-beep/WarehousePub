using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Resources;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;

public partial class BranchPages_GodownWise_PaymentStatusInformation : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                fillGodownType();
                fillGodownName();
                fillDetailsInGrid();
            }
        }
    }
    private void fillGodownName()
    {
        string vBranchID = Session["BranchID"].ToString();
        try
        {
            string query = "";
            query = "select MG.Godown_ID [GodownID], MG.Godown_Name [GodownName] from tbl_Metadata_Godown_2018 MG WHERE MG.BranchID =" + vBranchID + " and Hired_Type  ='" + ddlGodownType.SelectedItem.ToString() + "' and IsActive='Y' order by Godown_Name Asc";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodownName.Items.Clear();
                ddlGodownName.DataSource = ds.Tables[0];
                ddlGodownName.DataTextField = "GodownName";
                ddlGodownName.DataValueField = "GodownID";
                ddlGodownName.DataBind();
                ddlGodownName.Items.Insert(0, "--Select--");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void fillGodownType()
    {
        try
        {
            string query = "";
            query = "select distinct MG.Hired_Type [GodownType] from tbl_Metadata_Godown_2018 MG ORDER BY MG.Hired_Type";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodownType.Items.Clear();
                ddlGodownType.DataSource = ds.Tables[0];
                ddlGodownType.DataTextField = "GodownType";
                ddlGodownType.DataValueField = "GodownType";
                ddlGodownType.DataBind();
                ddlGodownType.Items.Insert(0, "--Select--");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rowIndex++;
                }
            }
        }
    }
    private void FillCropYearDropDownList(DropDownList ddlCropYear)
    {
        ArrayList arr = GetDummyData();
        foreach (ListItem item in arr)
        {
            ddlCropYear.Items.Add(item);
        }
    }
    private ArrayList GetDummyData()
    {
        ArrayList arr = new ArrayList();
        arr.Add(new ListItem("2020-21", "2020-21"));
        arr.Add(new ListItem("2021-22", "2021-22"));
        arr.Add(new ListItem("2022-23", "2022-23"));
        arr.Add(new ListItem("2023-24", "2023-24"));
        arr.Add(new ListItem("2024-25", "2024-25"));
        arr.Add(new ListItem("2025-26", "2025-26"));
        return arr;
    }
    //public void InsertGodownPaymentDetail()
    //{
    //	try
    //	{

    //	}
    //	catch (Exception ex)
    //	{
    //		ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Combination of Financial Year & Godown should be different')", true);
    //	}
    //}
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ErrorMsg = "";
        lblMsg.Text = "";
        if (!string.IsNullOrEmpty(txtAgreementDate.Text))
        {
            getDate_MDY(txtAgreementDate.Text);
        }
        if (!string.IsNullOrEmpty(txtAgreementEndDate.Text))
        {
            getDate_MDY(txtAgreementEndDate.Text);
        }
        ErrorMsg += ddlFinancialYear.SelectedIndex > 0 ? "" : "वित्‍तीय वर्ष चुने \\n";
        ErrorMsg += ddlGodownType.SelectedIndex > 0 ? "" : "गोदाम का प्रकार चुने \\n";
        ErrorMsg += ddlGodownName.SelectedIndex > 0 ? "" : "गोदाम का नाम चुने \\n";
        ErrorMsg += !string.IsNullOrEmpty(txtAgreementCapacity.Text) ? "" : "अनुबंधित क्षमता दर्ज करे \\n";
        ErrorMsg += !string.IsNullOrEmpty(txtAgreementDate.Text) ? "" : "अनुबंध दिनांक दर्ज करे \\n";
        ErrorMsg += !string.IsNullOrEmpty(txtRentAmountInFY.Text) ? "" : "वित्‍तीय वर्ष में किराये की कुल राशि दर्ज करे \\n";
        ErrorMsg += !string.IsNullOrEmpty(txtRentAmountInFY.Text) ? "" : "वित्‍तीय वर्ष में किराये की कुल राशि दर्ज करे \\n";
        ErrorMsg += !string.IsNullOrEmpty(txtFYPaymentAmount.Text) ? "" : "वित्‍तीय वर्ष में भुगतान राशि दर्ज करे \\n";
        ErrorMsg += !string.IsNullOrEmpty(txtGodownOwnerRemainingAmt.Text) ? "" : "गोदाम संचालक की शेष लंबित राशि दर्ज करे \\n";
        ErrorMsg += !string.IsNullOrEmpty(txtAgreementEndDate.Text) ? "" : "अनुबंध समाप्ति दिनांक (yyyy-MM-dd) दर्ज करे \\n";
        if (ErrorMsg == "")
        {
            /*string RegionID = Session["Region_ID"].ToString();*////Session["Region_ID"].ToString();
            string DistrictID = Session["Depot_DistID"].ToString();
            string BranchID = Session["BranchID"].ToString();
            string GodownID = ddlGodownName.SelectedValue.ToString();
            string GodownType = ddlGodownType.SelectedItem.ToString();
            string GodownAgrCapacity = txtAgreementCapacity.Text.ToString();
            string GodownAgrDate = txtAgreementDate.Text.ToString();
            string GdnTotalAmountinRent = txtRentAmountInFY.Text.ToString();
            string GdnTotalAmountPaymentinFY = txtFYPaymentAmount.Text.ToString();
            string GdnOwnerPendingAmount = txtGodownOwnerRemainingAmt.Text.ToString();
            string GdnAgreementDate = txtAgreementEndDate.Text.ToString();
            string Remarks = txtRemarks.Text.ToString();
            string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
            string qry = "INSERT INTO [dbo].[tbl_PaymentStatusInfomation_PvtGodown] ([FinancialYear],[DistrictID],[BranchID],[GodownID],[GodownType],[GodownAgreementCapacity(InMT)]" +
                                     ",[GodownAgreementDate],[TotalAmountInRentInFY],[TotalAmountPaidToGodownInFY],[RemainingAmountOfGodownOwner],[AgreementEndDate], [Remarks]" +
                                     ",[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[DeletedBy], [DeletedDate])" +
                                     " VALUES ('" + ddlFinancialYear.SelectedValue + "','" + DistrictID + "','" + BranchID + "','" + GodownID + "','" + GodownType + "','" + GodownAgrCapacity + "','" + getDate_MDY(txtAgreementDate.Text) + "','" + GdnTotalAmountinRent + "','" + GdnTotalAmountPaymentinFY + "','" + GdnOwnerPendingAmount + "','" + getDate_MDY(txtAgreementEndDate.Text) + "','" + Remarks + "','" + ClientIP + "', GETDATE()" + ",'" + ClientIP + "', GETDATE()" + ",'" + ClientIP + "', GETDATE()" + ")";
            cmd = new SqlCommand(qry, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int c = cmd.ExecuteNonQuery();

            if (c > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Saved Successfully')", true);
                fillDetailsInGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Saved')", true);
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' " + ErrorMsg.ToString() + "')", true);
        }
    }
    protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodownName();
    }
    protected void fillDetailsInGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        string BranchID = Session["BranchID"].ToString();
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_PaymentStatusEntryData", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    DataTable MainTable = ds.Tables[0];

                    if (MainTable.Rows.Count > 0)
                    {
                        GV_EntryDone.DataSource = MainTable;
                        GV_EntryDone.DataBind();
                    }
                    else
                    {
                        GV_EntryDone.DataSource = null;
                        GV_EntryDone.DataBind();
                    }

                }
            }
        }
    }
    protected void GV_EntryDone_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GV_EntryDone.Rows[rowIndex];

            //Fetch value of Name.
            string hdnId = (row.FindControl("hdnId") as HiddenField).Value;
            Session["hdnId"] = hdnId.ToString();
            RemoveRow(hdnId);

        }
    }
    public void RemoveRow(string id)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmd1 = new SqlCommand();
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            SqlCommand cmd = new SqlCommand("usp_DeletePaymentStatusEntry", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Deleted Successfully!');", true);
                fillDetailsInGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Deleted!');", true);
            }

        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }
}


