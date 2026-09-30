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
using System.Net;
using System.Net.Sockets;
public partial class BranchPages_GodownWise__Depositor_WisePaymentStatusInformation : System.Web.UI.Page
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
                fillDepositorType();
                //fillGodown();
                fillCommodity();
                fillDetailsInGrid();
                txtDepositorRemainingAmt.Enabled = false;
            }
        }
    }
    private void fillDepositorType()
    {
        try
        {
            string query = "";
            query = "select Depositor_ID, Depositor_Name FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679','181','15478') order by Depositor_Name ";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepositor.Items.Clear();
                ddlDepositor.DataSource = ds.Tables[0];
                ddlDepositor.DataTextField = "Depositor_Name";
                ddlDepositor.DataValueField = "Depositor_ID";
                ddlDepositor.DataBind();
                ddlDepositor.Items.Insert(0, new ListItem("--Select--", "0"));
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
    private void fillCommodity()
    {
        try
        {
            string query = "";
            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcommodity.DataSource = ds.Tables[0];
                ddlcommodity.DataTextField = "Commodity_Name";
                ddlcommodity.DataValueField = "Commodity_Id";
                ddlcommodity.DataBind();
                ddlcommodity.Items.Insert(0, "Select");
            }
            else
            {
                ddlcommodity.Items.Clear();
                ddlcommodity.Items.Insert(0, "Select");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    //private void fillGodown()
    //{
    //    try
    //    {
    //        string query = "";
    //        //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
    //        query = "Select Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 Where BranchID ='" + Session["BranchId"].ToString() + "' Order By Godown_Name ASC";
    //        cmd = new SqlCommand(query, con);
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlGodown.DataSource = ds.Tables[0];
    //            ddlGodown.DataTextField = "Godown_Name";
    //            ddlGodown.DataValueField = "Godown_ID";
    //            ddlGodown.DataBind();
    //            ddlGodown.Items.Insert(0, "Select");
    //        }
    //        else
    //        {
    //            ddlGodown.Items.Clear();
    //            ddlGodown.Items.Insert(0, "Select");
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //////
    //    }
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Page.Validate("a");
        string ErrorMsg = "";
        lblMsg.Text = "";
        ErrorMsg += ddlFinancialYear.SelectedIndex > 0 ? "" : "वित्‍तीय वर्ष चुने";
        ErrorMsg += ddlDepositor.SelectedIndex > 0 ? "" : "जमाकर्ता का नाम चुने";
        //ErrorMsg += ddlGodown.SelectedIndex > 0 ? "" : "गोदाम का नाम चुने";
        ErrorMsg += ddlcommodity.SelectedIndex > 0 ? "" : "स्कंध का नाम चुने";
        ErrorMsg += !string.IsNullOrEmpty(txtBillCountFY.Text) ? "" : "वित्‍तीय वर्ष में प्रस्तुत देयकों की संख्या दर्ज करे \\n";
        ErrorMsg += !string.IsNullOrEmpty(txtPresentedBillAmntInFY.Text) ? "" : "वित्‍तीय वर्ष में प्रस्तुत देयकों की राशि (in CR) दर्ज करे \\n";
        ErrorMsg += !string.IsNullOrEmpty(txtReceivedAmtFromDepositor.Text) ? "" : "प्राप्त राशि का विवरण (in CR) दर्ज करे \\n";
        ErrorMsg += !string.IsNullOrEmpty(txtDepositorRemainingAmt.Text) ? "" : "शेष लंबित राशि (in CR) दर्ज करे \\n";
        if (ErrorMsg == "")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
            //string con = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            SqlCommand cmd = new SqlCommand("Usp_tbl_GodownDepositorwise_PaymentStatus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@FinancialYear", ddlFinancialYear.SelectedValue);
           // cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue);
            cmd.Parameters.AddWithValue("@DistrictID", Session["Depot_DistID"].ToString());
            cmd.Parameters.AddWithValue("@BranchID", Session["BranchID"].ToString());
            cmd.Parameters.AddWithValue("@Commodity_ID", ddlcommodity.SelectedValue); ;
            cmd.Parameters.AddWithValue("@DepositorID", ddlDepositor.SelectedValue);
            cmd.Parameters.AddWithValue("@TotalBillPresentedinFY", txtBillCountFY.Text);
            cmd.Parameters.AddWithValue("@TotalBillAmountPresented", txtPresentedBillAmntInFY.Text);
            cmd.Parameters.AddWithValue("@TotalAmountReceivedInFY", txtReceivedAmtFromDepositor.Text);
            cmd.Parameters.AddWithValue("@RemainingAmountFromDepositor", txtDepositorRemainingAmt.Text);
            cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
            cmd.Parameters.AddWithValue("@CreatedBy", GetLocalIPAddress());
            cmd.Parameters.AddWithValue("@IP_Adress", GetLocalIPAddress());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Record Saved Successfully |||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                fillDetailsInGrid();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);

            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alertMessage", "alert('" + ErrorMsg.ToString() + "')", true);
        }
    }
    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        String ipaddress = string.Empty;
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipaddress = ip.ToString();
            }
        }
        return ipaddress.Length > 0 ? ipaddress : null;
    }
    protected void fillDetailsInGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        string BranchID = Session["BranchID"].ToString();
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_Godown_DepositorWisePaymentStatus", con))
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
            SqlCommand cmd = new SqlCommand("usp_DeleteGodown_DepositorwisePaymentStatus", con);
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

    protected void txtPresentedBillAmntInFY_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtPresentedBillAmntInFY.Text))
        {
            decimal PresentedBillAmt = Convert.ToDecimal(txtPresentedBillAmntInFY.Text);
            if (!string.IsNullOrEmpty(txtReceivedAmtFromDepositor.Text))
            {
                decimal ReceivedAmt = Convert.ToDecimal(txtReceivedAmtFromDepositor.Text);
                decimal DepositorRemainingAmt = Convert.ToDecimal(PresentedBillAmt - ReceivedAmt);
                txtDepositorRemainingAmt.Text = Convert.ToString(DepositorRemainingAmt);
                txtDepositorRemainingAmt.Enabled = false;
            }
            else
            {
                txtDepositorRemainingAmt.Text = Convert.ToString(PresentedBillAmt);
            }


        }
    }
}