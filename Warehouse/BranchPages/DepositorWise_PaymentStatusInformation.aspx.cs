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

public partial class BranchPages_DepositorWise_PaymentStatusInformation : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        //string vBranchID = Session["BranchID"].ToString();
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                fillDepositorType();
                fillDetailsInGrid();
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
                    //Set the Previous Selected Items on Each DropDownList on Postbacks
                    //DropDownList ddl1 = (DropDownList)gvGodown.Rows[rowIndex].Cells[1].FindControl("DropDownList1");
                    //DropDownList ddl1 = (DropDownList)GV_CommodityInfo.Rows[rowIndex].Cells[1].FindControl("ddlCropYear");
                    ////Fill the DropDownList with Data
                    //FillCropYearDropDownList(ddl1);
                    //if (i < dt.Rows.Count - 1)
                    //{
                    //    ddl1.ClearSelection();
                    //    ddl1.Items.FindByText(dt.Rows[i]["ddlCropYear"].ToString()).Selected = true;
                    //
                    //}
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
    public void InsertDepositorWisePaymentDetail()
    {
        try
        {


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Combination of Financial Year & Depositor should be different')", true);
        }
        /*******
            try
            {
                for (int j = 0; j < GV_PaymentStatusInfo.Rows.Count; j++)
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string vCropYear = ((DropDownList)GV_CommodityInfo.Rows[j].FindControl("ddlCropYear")).SelectedValue.ToString();
                    if (vCropYear != "0" && vCropYear != "")
                    {
                        //if (((CheckBox)GV_CommodityInfo.Rows[j].FindControl("ckstack")).Checked == true)
                        //{
                        TextBox vtxtSixMonth = (TextBox)GV_PaymentStatusInfo.Rows[j].Cells[1].FindControl("txtSixMonth");
                        TextBox vtxtNineMonth = (TextBox)GV_PaymentStatusInfo.Rows[j].Cells[2].FindControl("txtNineMonth");
                        TextBox vtxtTwelveMonth = (TextBox)GV_PaymentStatusInfo.Rows[j].Cells[3].FindControl("txtTwelveMonth");
                        TextBox vtxtEighteenMonth = (TextBox)GV_PaymentStatusInfo.Rows[j].Cells[4].FindControl("txtEighteenMonth");
                        TextBox vtxtTwentyFourMonth = (TextBox)GV_PaymentStatusInfo.Rows[j].Cells[5].FindControl("txtTwentyFourMonth");
                        TextBox vtxtThirtyMonth = (TextBox)GV_PaymentStatusInfo.Rows[j].Cells[6].FindControl("txtThirtyMonth");
                        TextBox vtxtThirtySixMonth = (TextBox)GV_PaymentStatusInfo.Rows[j].Cells[7].FindControl("txtThirtySixMonth");
                        TextBox vtxtFiveYear = (TextBox)GV_PaymentStatusInfo.Rows[j].Cells[8].FindControl("txtFiveYear");
                        TextBox vtxtGreaterthanFiveYear = (TextBox)GV_PaymentStatusInfo.Rows[j].Cells[9].FindControl("txtGreaterthanFiveYear");
                        DropDownList vddlCropYear = (DropDownList)GV_PaymentStatusInfo.Rows[j].FindControl("ddlCropYear");

                        //TextBox ConstructionYears = (TextBox)GV_CommodityInfo.Rows[j].Cells[6].FindControl("txtThirtySixMonth");

                        string CropYear = Convert.ToString(vddlCropYear.SelectedValue.ToString());
                        decimal vSixMonth = Convert.ToDecimal(vtxtSixMonth.Text);
                        decimal vNineMonth = Convert.ToDecimal(vtxtNineMonth.Text);
                        decimal vTwelveMonth = Convert.ToDecimal(vtxtTwelveMonth.Text);
                        decimal vEighteenMonth = Convert.ToDecimal(vtxtEighteenMonth.Text);
                        decimal vTwentyFourMonth = Convert.ToDecimal(vtxtTwentyFourMonth.Text);
                        decimal vThirtyMonth = Convert.ToDecimal(vtxtThirtyMonth.Text);
                        decimal vThirtySixMonth = Convert.ToDecimal(vtxtThirtySixMonth.Text);
                        decimal vFiveYear = Convert.ToDecimal(vtxtFiveYear.Text);
                        decimal vGreaterthanFiveYear = Convert.ToDecimal(vtxtGreaterthanFiveYear.Text);

                        string qry = "INSERT INTO [dbo].[tbl_PaymentStatusInfomation_PvtGodown] ([FinancialYear],[RegionID],[DistrictID],[BranchID],[GodownID],[GodownType],[GodownAgreementCapacity(InMT)]" +
                                     ",[GodownAgreementDate],[TotalAmountInRentInFY],[TotalAmountPaidToGodownInFY],[RemainingAmountOfGodownOwner],[AgreementEndDate]" +
                                     ",[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[DeletedBy], [DeletedDate])" +
                                     " VALUES ('" + FinancialYear + "','" + RegionID + "','" + DistrictID + "','" + BranchID + "','" + GodownID + "','" + GodownType + "','" + CommodityID + "','" + CropYear + "','" + GodownType + "','" + vSixMonth + "','" + vNineMonth + "','" + vTwelveMonth + "','" + vEighteenMonth + "','" + vTwentyFourMonth + "','" + vThirtyMonth + "','" + vThirtySixMonth + "','" + vFiveYear + "','" + vGreaterthanFiveYear + "','" + ClientIP + "', GETDATE()" + ",'" + ClientIP + "', GETDATE()" + ",'" + ClientIP + "', GETDATE()" + ")";
                        cmd = new SqlCommand(qry, con);
                        int c = cmd.ExecuteNonQuery();
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        if (c > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Saved Successfully')", true);
                            fillDetailsInGrid();
                        }
                        //}
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please enter Crop Year')", true);

                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No two CropYear for same commodity in same branch for same Depositor and Godown should be same')", true);
            }****/

    }
    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {


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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Page.Validate("a");
        string ErrorMsg = "";
        lblMsg.Text = "";
        ErrorMsg += ddlFinancialYear.SelectedIndex > 0 ? "" : "वित्‍तीय वर्ष चुने";
        ErrorMsg += ddlDepositor.SelectedIndex > 0 ? "" : "जमाकर्ता का नाम चुने";
        ErrorMsg += !string.IsNullOrEmpty(txtBillCountFY.Text) ? "" : "वित्‍तीय वर्ष में प्रस्तुत देयकों की संख्या दर्ज करे \\n";
        ErrorMsg += !string.IsNullOrEmpty(txtPresentedBillAmntInFY.Text) ? "" : "वित्‍तीय वर्ष में प्रस्तुत देयकों की राशि (in CR) दर्ज करे \\n";
        ErrorMsg += !string.IsNullOrEmpty(txtReceivedAmtFromDepositor.Text) ? "" : "प्राप्त राशि का विवरण (in CR) दर्ज करे \\n";
        ErrorMsg += !string.IsNullOrEmpty(txtDepositorRemainingAmt.Text) ? "" : "शेष लंबित राशि (in CR) दर्ज करे \\n";
        if (ErrorMsg == "")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
            //string con = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            SqlCommand cmd = new SqlCommand("Usp_Depositorwise_PaymentStatusInfomation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@FinancialYear", ddlFinancialYear.SelectedValue);
            cmd.Parameters.AddWithValue("@DistrictID", Session["Depot_DistID"].ToString());
            cmd.Parameters.AddWithValue("@BranchID", Session["BranchID"].ToString());
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
    protected void fillDetailsInGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        string BranchID = Session["BranchID"].ToString();
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_DepositorWisePaymentStatusEntryData", con))
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
            SqlCommand cmd = new SqlCommand("usp_DeleteDepositorwisePaymentStatusEntry", con);
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

    protected void GV_EntryDone_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

}


