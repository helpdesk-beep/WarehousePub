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

public partial class Accounting_PaddyStorageLossDetail : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    SqlDataAdapter da = new SqlDataAdapter();
    string qry = "";
    string Branch_Id = "";
    string District_Id = "";
    DateTime FromDate = new DateTime();
    DateTime ToDate = new DateTime();
    int RId = 0;
    string Client_Ip = "";
    string Godown_Id = "";
    string GPID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                FillCropYear();
                GetDepositorName();
                fillCommodity();
                GetRegionId();
            }
            District_Id = Session["Depot_DistID"].ToString();
            Branch_Id = Session["BranchID"].ToString();
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void btnPSubmit_Click(object sender, EventArgs e)
    {
        if (ddldepos_name.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Depositor Name...!'); </script> ");
        }
        if (ddlCommodity.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Commodity Name...!'); </script> ");
        }
        else if (txtGodown.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Godown NO/Name....!'); </script> ");
        }
        else if (txtFirstDate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Deposit Date....!'); </script> ");
        }
        else if (txtLastDate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Issue Date....!'); </script> ");
        }
        else
        {
            Insert_PaddyLoss_Detail();
            btnPSubmit.Visible = false;
            btnPSubmit.Enabled = false;
            btnNew.Visible = true;
        }
    }
    public void Insert_PaddyLoss_Detail()
    {
        int r = 0;
        Client_Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        //qry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Paddy_Loss_Detail]([DistrictId],[BranchId],[RegionId],[DepositorName],[CommodityId],[CropYear],[Godown],[FirstDepositDate],[RecWeight],[RecAvgMois],[RecModeOfWeight],[LastIssueDate],[IssueWeight],[IssueAvgMois],[IssueModeOfWeight],[StorageMonth],[StorageDay],[Reason],[CreatedDate],[CreatedBy])VALUES('" + District_Id + "','" + Branch_Id + "','1','" + txtDepositor.Text + "','24','" + ddlcropyear.SelectedItem.Text + "','" + txtGodown.Text + "','" + getDate_MDY(txtFirstDate.Text) + "','" + txtRWeight.Text + "','" + txtAvgMos.Text + "','" + txtWeigtment.Text + "','" + getDate_MDY(txtLastDate.Text) + "','" + txtIWeight.Text + "','" + txtIAvgMos.Text + "','" + txtIWeigtment.Text + "','" + txtMonth.Text + "','" + txtDay.Text + "','" + txtResion.Text + "',getdate(),'" + Client_Ip + "')";
        //qry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Paddy_Loss_Detail]([DistrictId],[BranchId],[RegionId],[DepositorName],DepositorId,[CommodityId],[CropYear],[Godown],[FirstDepositDate],[RecWeight],[RecAvgMois],[RecModeOfWeight],[LastIssueDate],[IssueWeight],[IssueAvgMois],[IssueModeOfWeight],[StorageMonth],[StorageDay],[Reason],[CreatedDate],[CreatedBy])VALUES('" + District_Id + "','" + Branch_Id + "','1','" + ddldepos_name.SelectedItem.Text + "'," + ddldepos_name.SelectedValue.ToString() + ",'24','" + ddlcropyear.SelectedItem.Text + "','" + txtGodown.Text + "','" + getDate_MDY(txtFirstDate.Text) + "','" + txtRWeight.Text + "','" + txtAvgMos.Text + "','" + txtWeigtment.Text + "','" + getDate_MDY(txtLastDate.Text) + "','" + txtIWeight.Text + "','" + txtIAvgMos.Text + "','" + txtIWeigtment.Text + "','" + txtMonth.Text + "','" + txtDay.Text + "','" + txtResion.Text + "',getdate(),'" + Client_Ip + "')";
        qry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Paddy_Loss_Detail]([DistrictId],[BranchId],[RegionId],[DepositorName],DepositorId,[CommodityId],[CropYear],[Godown],[FirstDepositDate],[RecWeight],[RecAvgMois],[RecModeOfWeight],[LastIssueDate],[IssueWeight],[IssueAvgMois],[IssueModeOfWeight],[Reason],[CreatedDate],[CreatedBy])VALUES('" + District_Id + "','" + Branch_Id + "','" + ViewState["Region_ID"].ToString() + "','" + ddldepos_name.SelectedItem.Text + "'," + ddldepos_name.SelectedValue.ToString() + ",'"+ ddlCommodity.SelectedValue.ToString() +"','" + ddlcropyear.SelectedItem.Text + "','" + txtGodown.Text + "','" + getDate_MDY(txtFirstDate.Text) + "','" + txtRWeight.Text + "','" + txtAvgMos.Text + "','" + ddlRWeighmentMode.SelectedItem.Text + "','" + getDate_MDY(txtLastDate.Text) + "','" + txtIWeight.Text + "','" + txtIAvgMos.Text + "','" + ddlIWeighmentMode.SelectedItem.Text + "','" + txtResion.Text + "',getdate(),'" + Client_Ip + "')";

        cmd.CommandText = qry;
        cmd.Connection = con;
        con.Open();
        r = cmd.ExecuteNonQuery();
        con.Close();
        if (r > 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Record Save Successfully...!'); </script> ");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occured...!'); </script> ");
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
    public void GetRegionId()
    {
        qry = "select Region_ID from tbl_MetaData_DISTRICT where District_Id='" + Session["Depot_DistID"].ToString() + "'";
        da = new SqlDataAdapter(qry, con);
        DataTable dt=new DataTable();
        da.Fill(dt);
       if (dt.Rows.Count > 0)
       {
           ViewState["Region_ID"] = dt.Rows[0]["Region_ID"].ToString();
       }
       else
       {
           //ViewState["Region_ID"] = dt.Rows[0]["Region_ID"].ToString();
       }
    }
    public void FillCropYear()
    {

        ddlcropyear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        ddlcropyear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        ddlcropyear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        ddlcropyear.Items.Add((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        ddlcropyear.Items.Add((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        ddlcropyear.Items.Add((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2,2));
        ddlcropyear.Items.Add((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2));
    }
    public void GetDepositorName()
    {
        qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID in ('129','4679') and Depositor_Type ='Institution'";
            da = new SqlDataAdapter(qry, con);
            ds = new DataSet();
            da.Fill(ds);
            if (ds == null)
            {
            }
            else
            {
                ddldepos_name.DataSource = ds.Tables[0];
                ddldepos_name.DataTextField = "Depositor_Name";
                ddldepos_name.DataValueField = "Depositor_ID";
                ddldepos_name.DataBind();
                ddldepos_name.Items.Insert(0, "--Select--");
            }
        }
    private void fillCommodity()
    {
        try
        {
            //string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Qry_Order";
            string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name asc";
            SqlCommand cmd = new SqlCommand(query,con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCommodity.DataSource = ds.Tables[0];
                ddlCommodity.DataTextField = "Commodity_Name";
                ddlCommodity.DataValueField = "Commodity_Id";
                ddlCommodity.DataBind();
                ddlCommodity.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception)
        {

            //// throw;
        }
    }
   
    protected void txtLastDate_TextChanged(object sender, EventArgs e)
    {
        //GetPeriods();
    }
    public void GetPeriods()
    {
        try
        {
            DateTime fdate = new DateTime();
            DateTime tdate = new DateTime();

            string sfdate = getDate_MDY(txtFirstDate.Text);

            //fdate = DateTime.Parse(sfdate);

            string stodate = getDate_MDY(txtLastDate.Text);

            tdate = DateTime.Parse(stodate);
            tdate = tdate.AddDays(1);
            string NewToDate = tdate.ToString("MM/dd/yyyy");
            //DateD(tdate, fdate);
            //qry = "select dbo.udfDateDiffinYrMonDay('" + sfdate + "','" + NewToDate + "') as Period";
            qry = "select SUBSTRING((select dbo.udfDateDiffinYrMonDay('" + sfdate + "','" + NewToDate + "')),1 ,2) as Month,SUBSTRING((select dbo.udfDateDiffinYrMonDay('" + sfdate + "','" + NewToDate + "')),11,2) as Day";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows[0]["Period"].ToString() != "")
            {
                //txtMonth.Text = dt.Rows[0]["Month"].ToString();
                //txtDay.Text = dt.Rows[0]["Day"].ToString();
            }
        }
        catch (Exception ex)
        {
            //lblmsg.Text = ex.Message;
        }
    }
    protected void txtFirstDate_TextChanged(object sender, EventArgs e)
    {
        //GetPeriods();
    }
    protected void btnPCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounting/PaddyStorageLossDetail.aspx");
    }
}
