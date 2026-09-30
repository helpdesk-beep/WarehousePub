using System;
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
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.IO;
using System.Net.Sockets;

public partial class Accounting_Nafed_acknowledgement : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    public string qry = "";
    string Bill_No = "";
    int BID = 0;
    DateTime StartDate = new DateTime();
    DateTime EndDate = new DateTime();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDistrict();
            GetCommodity();
            GetCropYear();
            lblAmount.Enabled = false;
        }
    }
    public void GetDistrict()
    {
        string qry = "";
        qry = "select distinct District_Id,District_Name from tbl_MetaData_DISTRICT Order By District_Name ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void GetCommodity()
    {
        string qry = "";
        qry = "select distinct Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in ('63', '64', '33', '52', '27', '92', '123', '31', '65','26') order by Commodity_Name asc";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void GetCropYear()
    {
        string qry = "";
        qry = "Select Distinct Crop_Year from tbl_Storage_Bill_Details Order By Crop_Year ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcropyear.DataSource = ds.Tables[0];
            ddlcropyear.DataTextField = "Crop_Year";
            ddlcropyear.DataValueField = "Crop_Year";
            ddlcropyear.DataBind();
            ddlcropyear.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void GetBranch()
    {
        string qry = "";
        qry = "select distinct DepotID,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "DepotID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
        fillgridForExcel();
    }
    public void fillgrid()
    {
        string Dist_id = ddldistrict.SelectedValue;
        string Branch_Id = ddlbranch.SelectedValue;
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_Acknowledgement_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddldistrict.SelectedValue == "ALL")
            {
                cmd.Parameters.AddWithValue("@District_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@District_Id", Dist_id);
            }
            if (ddlbranch.SelectedValue == "ALL")
            {
                cmd.Parameters.AddWithValue("@Branch_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
            }
            if (ddlFinancialyear.SelectedValue == "ALL")
            {
                cmd.Parameters.AddWithValue("@Financial_Year", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Financial_Year", ddlFinancialyear.SelectedValue);
            }
            if (ddlmonth.SelectedValue == "ALL")
            {
                cmd.Parameters.AddWithValue("@Month", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
            }
            cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                GrdBills.DataSource = dt;
                GrdBills.DataBind();
                grdbill.Visible = true;
            }
            else
            {
                GrdBills.DataSource = null;
                GrdBills.DataBind();
                grdbill.Visible = true;
            }
        }
    }
    protected void Checked_CheckedChanged(object sender, EventArgs e)
    {
        int count = 0;
        decimal total = 0;
        foreach (GridViewRow row in GrdBills.Rows)
        {
            CheckBox checkBox = (CheckBox)row.FindControl("Checked");
            TextBox PassAmount = (TextBox)row.FindControl("txtpass");
            Label Bill_Number = (Label)row.FindControl("lblBill_Number");
            decimal Amount = Convert.ToDecimal(PassAmount.Text);

            if (checkBox != null && checkBox.Checked)
            {
                PassAmount.Enabled = false;
                count++;
                total = total + Amount;
            }
            else
            {
                PassAmount.Enabled = true;
            }
        }
        lblAmount.Text = total.ToString();
        txtcount.Text = count.ToString();
        lbltotalcount.Visible = true;
        txtcount.Visible = true;
        lbltotalamount.Visible = true;
        lblAmount.Visible = true;
        txtcount.Enabled = false;
        btnproceed.Visible = true;
    }
    protected void btnproceed_Click(object sender, EventArgs e)
    {
        string TotalCount = txtcount.Text.Trim();
        decimal TotalAmount = Convert.ToDecimal(lblAmount.Text.Trim());
        string Commodity = ddlcommodity.SelectedValue;
        String Crop_Year = ddlcropyear.SelectedValue;
        string Crop_YearEnd = Crop_Year.Substring(Crop_Year.Length - 2, 2);
        string CommodityId = Commodity.ToString(); ;
        String Depositor_ID = "10535";
        qry = "select max(BId) as BId from tbl_Nafed_acknowledgement_No where  Depositor_Id='" + Depositor_ID + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count != 0 && dt.Rows[0]["BId"].ToString() != null && dt.Rows[0]["BId"].ToString() != "")
        {
            if (dt.Rows[0]["BId"].ToString() != "" || Convert.ToInt32(dt.Rows[0]["BId"]) != 0)
            {
                BID = Convert.ToInt32(dt.Rows[0]["BId"]);

                int SubBN = BID + 1;
                Bill_No = "10535" + Crop_YearEnd + CommodityId + "01" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + SubBN.ToString();
                BID = SubBN;
            }
            else
            {
                Bill_No = "10535" + Crop_YearEnd + CommodityId + "01" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
                BID = 1;
            }
        }
        else
        {
            Bill_No = "10535" + Crop_YearEnd + CommodityId + "01" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
            BID = 1;
        }
        ViewState["BillNo"] = Bill_No;
        ViewState["BID"] = BID;
        this.Insert(Commodity, Crop_Year, TotalCount, TotalAmount);
    }
    private void Insert(string Commodity, string Crop_Year, string TotalCount, Decimal TotalAmount)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
            string qry = "";
            int ICount = 0;
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            Bill_No = ViewState["BillNo"].ToString();
            BID = Convert.ToInt32(ViewState["BID"]);
            con.Open();
            qry = "INSERT INTO tbl_Nafed_acknowledgement_No(Bill_Number,Depositor_Id,Commodity_ID,Crop_Year,Bill_Count,Total_Amount,BId,CreateBy,Createdby_Ip,CreatedOn) values('" + Bill_No + "','10535','" + Commodity + "','" + Crop_Year + "','" + TotalCount + "','" + TotalAmount + "','" + BID + "','" + Session["State_Logid"].ToString() + "','" + ip + "',getdate())";
            SqlCommand cmd = new SqlCommand(qry, con);
            int n = cmd.ExecuteNonQuery();
            if (n > 0)
            {
                foreach (GridViewRow row in GrdBills.Rows)
                {
                    CheckBox chkbox = (CheckBox)row.FindControl("Checked");
                    if (chkbox.Checked == true)
                    {
                        string FinalPayment = "0";
                        string BillNo = "0";
                        string Final_Payment = (row.FindControl("txtpass") as TextBox).Text;
                        string Godown_Bill = (row.FindControl("lblBill_Number") as Label).Text;
                        BillNo = Godown_Bill.ToString();
                        FinalPayment = Final_Payment.ToString();
                        string qry2 = "update tbl_Storage_Bill_Details set Fin_Bill_no='" + Bill_No + "', Pass_Amount ='" + FinalPayment + "' where Bill_Number='" + BillNo + "'";
                        SqlCommand cmd2 = new SqlCommand(qry2, con);
                        int i = cmd2.ExecuteNonQuery();
                        ICount = ICount + i;
                    }
                }
                string strMsg = "Bill Submit Successfully |||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                fillgrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
            }
        }
        catch (Exception ex)
        {
            lblrmsg.Text = ex.Message;
        }
        finally
        {
            con.Close();
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
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    public string GetLocalIPAddress()
    {
        var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
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
     protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkHeaderCheck = (CheckBox)sender;
        foreach (GridViewRow gRow in GrdBills.Rows)
        {
            CheckBox ckRowSel = (CheckBox)gRow.FindControl("Checked");
            TextBox PassAmount = (TextBox)gRow.FindControl("txtpass");
            ckRowSel.Checked = chkHeaderCheck.Checked;
            if (ckRowSel != null && ckRowSel.Checked)
            {
                int count = 0;
                decimal total = 0;
                foreach (GridViewRow row in GrdBills.Rows)
                {
                    CheckBox checkBox = (CheckBox)row.FindControl("Checked");

                    Label Bill_Number = (Label)row.FindControl("lblBill_Number");
                    decimal Amount = Convert.ToDecimal(PassAmount.Text);

                    if (checkBox != null && checkBox.Checked)
                    {
                        PassAmount.Enabled = false;
                        count++;
                        total = total + Amount;
                    }
                    else
                    {
                        PassAmount.Enabled = true;
                    }
                }
                lblAmount.Text = total.ToString();
                txtcount.Text = count.ToString();
                lbltotalcount.Visible = true;
                txtcount.Visible = true;
                lbltotalamount.Visible = true;
                lblAmount.Visible = true;
                txtcount.Enabled = false;
                btnproceed.Visible = true;
                PassAmount.Enabled = false;
            }
            else
            {
                PassAmount.Enabled = true;
                lbltotalcount.Visible = false;
                txtcount.Visible = false;
                lbltotalamount.Visible = false;
                lblAmount.Visible = false;
                btnproceed.Visible = false;
            }
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportGridViewToExcel(GridView1);
    }
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //fillgrid();         
    }
    private void ExportGridViewToExcel(GridView GridView1)
    {
        // Clear the response
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "NAFED_Acknowledgement.xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GridView1.GridLines = GridLines.Both;
        GridView1.HeaderStyle.Font.Bold = true;
        GridView1.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }

    public void fillgridForExcel()
    {
        string Dist_id = ddldistrict.SelectedValue;
        string Branch_Id = ddlbranch.SelectedValue;
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_Acknowledgement_Details_For_Excel", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddldistrict.SelectedValue == "ALL")
            {
                cmd.Parameters.AddWithValue("@District_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@District_Id", Dist_id);
            }
            if (ddlbranch.SelectedValue == "ALL")
            {
                cmd.Parameters.AddWithValue("@Branch_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
            }
            if (ddlFinancialyear.SelectedValue == "ALL")
            {
                cmd.Parameters.AddWithValue("@Financial_Year", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Financial_Year", ddlFinancialyear.SelectedValue);
            }
            if (ddlmonth.SelectedValue == "ALL")
            {
                cmd.Parameters.AddWithValue("@Month", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
            }
            cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                //grdbill.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                //grdbill.Visible = true;
            }
        }
    }
}

