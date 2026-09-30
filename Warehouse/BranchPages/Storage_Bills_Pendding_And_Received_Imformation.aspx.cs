﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Drawing;

public partial class BranchPages_Storage_Bills_Pendding_And_Received_Imformation : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchId"] != null))
        {
            if (!IsPostBack)
            {
                txtbranch.Text = Session["UserName"].ToString();
                fillComodity();
                fillgrid();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    private void fillComodity()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlComodity.Items.Clear();
                ddlComodity.DataSource = ds.Tables[0];
                ddlComodity.DataTextField = "Commodity_Name";
                ddlComodity.DataValueField = "Commodity_Id";
                ddlComodity.DataBind();
                ddlComodity.Items.Insert(0, "--Select--");
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
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            //checkvalidation();
            string ErrorMsg = "";
            if (!string.IsNullOrEmpty(txtDate.Text))
            {
                getDate_MDY(txtDate.Text);
            }
            ErrorMsg += ddlFinancialYear.SelectedIndex > 0 ? "" : "वित्तीय वर्ष चुने... \\n";
            ErrorMsg += ddlComodity.SelectedIndex > 0 ? "" : "स्कंध का नाम चुने... \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtbillnumber.Text) ? "" : "देयक क्रमांक दर्ज करे... \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtMpwlcAmount.Text) ? "" : "देयक राशि दर्ज करे... \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtkatoraamount.Text) ? "" : "कटोत्रा राशि दर्ज करे \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtinformation.Text) ? "" : "कटोत्रा का विवरण दर्ज करे\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtbhugtan.Text) ? "" : "शुद्ध भुगतान दर्ज करे\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtadwise.Text) ? "" : "चेक क्र./एडवाइस क्र. दर्ज करे\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtUnpaidamount.Text) ? "" : "भुगतान हेतु लंबित राशि दर्ज करे\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtdelyreason.Text) ? "" : "लंबित का कारण दर्ज करे\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtremark.Text) ? "" : "रिमार्क दर्ज करे\\n";
            if (ErrorMsg == "")
            {
                if (btnsave.Text == "SUBMIT")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Insert_Storage_Amount_Paid_Unpaid_MPSCSC", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
                    cmd.Parameters.AddWithValue("@Finacial_Year", ddlFinancialYear.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Commodity_ID", ddlComodity.SelectedValue);
                    cmd.Parameters.AddWithValue("@Paid_Date", getDate_MDY(txtDate.Text));
                    cmd.Parameters.AddWithValue("@Paid_Amount", txtMpwlcAmount.Text);
                    cmd.Parameters.AddWithValue("@Katotra_amount", txtkatoraamount.Text);
                    cmd.Parameters.AddWithValue("@Katotra_Information", txtinformation.Text);
                    cmd.Parameters.AddWithValue("@Shudh_Paid", txtbhugtan.Text);
                    cmd.Parameters.AddWithValue("@Check_Number", txtadwise.Text);
                    cmd.Parameters.AddWithValue("@MPSCSC_Pending_Amount", txtUnpaidamount.Text);
                    cmd.Parameters.AddWithValue("@Pendency_Reason", txtdelyreason.Text);
                    cmd.Parameters.AddWithValue("@Remark", txtremark.Text);
                    cmd.Parameters.AddWithValue("@BIll_Number", txtbillnumber.Text);
                    cmd.Parameters.AddWithValue("@Created_By", Session["BranchId"].ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy_IP", GetLocalIPAddress());
                    cmd.Parameters.AddWithValue("@IP_Adress", GetLocalIPAddress());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Data Saved Successfully |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        fillgrid();
                        //TextClear();
                        TextClear();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                        //TextClear();
                    }
                }
                //else if (btnsave.Text == "Edit")
                //{
                //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                //    SqlCommand cmd = new SqlCommand("Usp_Update_DistributedItem", con);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    con.Open();
                //    cmd.Parameters.AddWithValue("@Distribution_Id", ViewState["Distribution_Id"].ToString());
                //    cmd.Parameters.AddWithValue("@Inventory_Id", ddlitem.SelectedValue);
                //    cmd.Parameters.AddWithValue("@Available_Qty", txtremainning.Text);
                //    cmd.Parameters.AddWithValue("@Distribution_Date", getDate_MDY(txtDate.Text));
                //    cmd.Parameters.AddWithValue("@Department_Id", ddldepartment.SelectedValue);
                //    cmd.Parameters.AddWithValue("@Designation", txtDesignationname.Text);
                //    cmd.Parameters.AddWithValue("@Employee_Name", txtemp.Text);
                //    cmd.Parameters.AddWithValue("@Distributer_Name", txtdistributer.Text);
                //    cmd.Parameters.AddWithValue("@Distribute_Quantity", txtdistribute.Text);
                //    cmd.Parameters.AddWithValue("@Remaining_Quantity", txtremai.Text);
                //    cmd.Parameters.AddWithValue("@Updated_by", Session["UserId"].ToString());
                //    cmd.Parameters.AddWithValue("@Updatedby_Ip", GetLocalIPAddress());
                //    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                //    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                //    cmd.ExecuteNonQuery();
                //    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                //    if (TheResult.StartsWith("SUCCESS"))
                //    {
                //        string strMsg = "Purchase Item Update Successfully|||";
                //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                //        FillGrid();
                //        TextClear();
                //    }
                //    else
                //    {
                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
                //        TextClear();
                //    }
                //}
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' " + ErrorMsg.ToString() + "')", true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    protected void TextClear()
    {
        ddlFinancialYear.ClearSelection();
        ddlComodity.ClearSelection();
        txtbillnumber.Text = "";
        txtDate.Text = "";
        txtMpwlcAmount.Text = "";
        txtkatoraamount.Text = "";
        txtinformation.Text = "";
        txtbhugtan.Text = "";
        txtadwise.Text = "";
        txtUnpaidamount.Text = "";
        txtdelyreason.Text = "";
        txtremark.Text = "";
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_MPSCSC_Paid_And_Unpaid_Amount_Details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet DS = new DataSet())
                    {
                        sda.Fill(DS);
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            GridView1.DataSource = DS.Tables[0];
                            GridView1.DataBind();

                        }

                    }
                }
            }
        }
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "";
        cell.ColumnSpan = 2;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 5;
        cell.Text = "MPWLC द्वारा प्रस्तुत देयकों का विवरण";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 8;
        cell.Text = "MPSCSCS द्वारा किये गये भुगतान का विवरण";
        row.Controls.Add(cell);


        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);

        //GridViewRow row1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        //TableHeaderCell cell1 = new TableHeaderCell();
        //cell1.Text = "";
        //cell1.ColumnSpan = 10;
        //row1.Controls.Add(cell1);

        //cell1 = new TableHeaderCell();
        //cell1.ColumnSpan = 2;
        //cell1.Text = "कटोत्रा का विवरण";
        //row1.Controls.Add(cell1);

        //row1.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        //GridView1.HeaderRow.Parent.Controls.AddAt(0, row1);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblRowNumber = (Label)row.FindControl("lblRowNumber");
            Label lblDepotName = (Label)row.FindControl("lblDepotName");
            Label lblFinacial_Year = (Label)row.FindControl("lblFinacial_Year");
            Label lblCommodity_ID = (Label)row.FindControl("lblCommodity_ID");
            Label lblBIll_Number = (Label)row.FindControl("lblBIll_Number");
            Label lblPaid_Date = (Label)row.FindControl("lblPaid_Date");
            Label lblPaid_Amount = (Label)row.FindControl("lblPaid_Amount");
            Label lblKatotra_amount = (Label)row.FindControl("lblKatotra_amount");
            Label lblKatotra_Information = (Label)row.FindControl("lblKatotra_Information");
            Label lblShudh_Paid = (Label)row.FindControl("lblShudh_Paid");
            Label lblCheck_Number = (Label)row.FindControl("lblCheck_Number");
            Label lblMPSCSC_Pending_Amount = (Label)row.FindControl("lblMPSCSC_Pending_Amount");
            Label lblPendency_Reason = (Label)row.FindControl("lblPendency_Reason");
            Label lblRemark = (Label)row.FindControl("lblRemark");
            ViewState["BIll_Number"] = lblBIll_Number.Text;
            if (e.CommandName == "EditRecord")
            {
                txtbranch.Text = lblDepotName.Text;
                ddlFinancialYear.SelectedValue = lblFinacial_Year.Text;
                ddlComodity.SelectedItem.Text = lblCommodity_ID.Text;
                txtbillnumber.Text = lblBIll_Number.Text;
                txtDate.Text = lblPaid_Date.Text;
                txtMpwlcAmount.Text = lblPaid_Amount.Text;
                txtkatoraamount.Text = lblKatotra_amount.Text;
                txtinformation.Text = lblKatotra_Information.Text;
                txtbhugtan.Text = lblShudh_Paid.Text;
                txtadwise.Text = lblCheck_Number.Text;
                txtUnpaidamount.Text = lblMPSCSC_Pending_Amount.Text;
                txtdelyreason.Text = lblPendency_Reason.Text;
                txtremark.Text = lblRemark.Text;
            }
            btnsave.Text = "Edit";
        }
    }
}
