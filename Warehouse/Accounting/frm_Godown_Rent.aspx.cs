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

public partial class Depot_frm_Godown_Rent : System.Web.UI.Page
{
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    DataSet ds2 = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (!IsPostBack)
            {
                GetDepositioType();
                GetGodown();
                Printcurrentdate();
            }
        }
        else
        {
            Response.Redirect("../Logout.aspx");
        }
    }
    void GetDepositioType()
    {
        qry = "select Depositor_Type,Depositor_Type_Id from dbo.tbl_MetaData_Depositor_Type";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddldepositor.DataSource = ds.Tables[0];
            ddldepositor.DataTextField = "Depositor_Type";
            ddldepositor.DataValueField = "Depositor_Type_Id";
            ddldepositor.DataBind();
            ddldepositor.Items.Insert(0, "--Se|ect--");
        }
    }
    void GetDepositorName()
    {
        string dtype = ddldepositor.SelectedItem.Text;
        if (dtype == "Institution")
        {
            qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name  in ('MPSCSC','MPWLC','FCI') union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE  BranchId='" + Session["BranchID"].ToString() + "' and Depositor_Type ='Institution'";
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
                ddldepos_name.Items.Insert(0, "--Se|ect--");
            }
        }
        else
        {
            string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
            qry = "select Depositor_Name,Depositor_ID from dbo.tbl_MetaData_DEPOSITOR where Depot_ID='" + Session["BranchID"].ToString() + "' and District_ID='23" + Dist_id + "' and Depositor_Type='" + dtype + "'";
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
                ddldepos_name.Items.Insert(0, "--Se|ect--");
            }
        }
    }
 void GetGodown()
    {
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string sid = Session["BranchID"].ToString();
        ddlgodown.Items.Clear();
        qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN where DistrictId='23" + Dist_id + "' and DepotId='" + Session["BranchID"].ToString() + "'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {            
            ddlgodown.Items.Insert(0, "--Se|ect--");
        }
        else
        {            
            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Se|ect--");
        }
    }
    protected void ddldepos_name_SelectedIndexChanged(object sender, EventArgs e)
    {      
        //GetDepositorAdd();      
    }
   
    protected void ddlverity_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetCommodity();
        //FillGrid();
    }
    protected void ddlcomodity_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    } 
    protected void ddldepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldepositor.SelectedValue.ToString() == "4")
        {
            ddlDeposCategory.SelectedValue = "1";
            ddlDeposCategory.SelectedItem.Text = "GEN";
            ddlDeposCategory.Enabled = false;
        }
        else
        {
            ddlDeposCategory.Enabled = true;       
        }
        
        GetDepositorName();
    }
    protected void GVGRent_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    void FillGodownNewRent()
    {
        try
        {
            //qry = "  select a.*,max(b.Rate) as MonthlyRate, max(b.Rate)/4 as WeeklyRate,((a.DeliverBags*max(b.Rate))*cast(SUBSTRING(PeriodOfStorage,10,02) as int))+ ((a.DeliverBags*(max(b.Rate)/4))*cast(SUBSTRING(PeriodOfStorage,20,02) as int)) as Charge from View_WHR_Wise_Delivery_Rent as a join tbl_MetaDataEffectiveRateDetail as b on a.Commodity_ID=b.Commodity_Id where b.Depositor_Type='" + ddldepositor.SelectedValue.ToString() + "' and a.Depositor_Name='" + ddldepos_name.SelectedItem.Text + "' and a.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and b.Rate_Effective_Date<=a.WHR_Issue_Date group by a.Depositor_Name,a.Godown_ID,a.Commodity_ID,a.DepositBags,a.DepositWeight,a.DeliverBags,a.DeliverWeight,a.DateOfDeposit,a.DateOfDelivery,a.PeriodOfStorage,a.Depositor_WHR_Id,a.WHR_Issue_Date order by a.Depositor_WHR_Id";
            qry = "  select a.*,max(b.Rate) as MonthlyRate, max(b.Rate)/4 as WeeklyRate,((a.DeliverBags*max(b.Rate))*cast(SUBSTRING(PeriodOfStorage,1,2) as int))+ ((a.DeliverBags*(max(b.Rate)/4))*cast(SUBSTRING(PeriodOfStorage,11,2) as int)) as Charge from View_WHR_Wise_Delivery_Rent as a join tbl_MetaDataEffectiveRateDetail as b on a.Commodity_ID=b.Commodity_Id where b.Depositor_Type='" + ddldepositor.SelectedValue.ToString() + "' and a.Depositor_Name='" + ddldepos_name.SelectedItem.Text + "' and a.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and b.Rate_Effective_Date<=a.WHR_Issue_Date group by a.Depositor_Name,a.Godown_ID,a.Commodity_ID,a.DepositBags,a.DepositWeight,a.DeliverBags,a.DeliverWeight,a.DateOfDeposit,a.DateOfDelivery,a.PeriodOfStorage,a.Depositor_WHR_Id,a.WHR_Issue_Date order by a.Depositor_WHR_Id";          
            da = new SqlDataAdapter(qry, con);
            Dt1 = new DataTable();
            da.Fill(Dt1);
            if (Dt1.Rows.Count > 0)
            {
                NewGrid.DataSource = Dt1;
                NewGrid.DataBind();

                RentDetail.Visible = true;
                TotalAmt.Visible = true;
                decimal sum = 0;
                for (int i = 0; i < NewGrid.Rows.Count; i++)
                {
                    if (NewGrid.Rows[i].Cells[11].Text != "&nbsp;")
                    {
                        sum += Convert.ToDecimal(NewGrid.Rows[i].Cells[11].Text);
                    }
                }
                txtTotalCharge.Text = sum.ToString();
                decimal DiscountPer = 0;
                if (ddldepositor.SelectedValue.ToString() == "4")
                {
                    DiscountPer = 0;
                    txtdiscount.Text = "0";
                }
                else
                {
                    DiscountPer = Convert.ToDecimal(txtdiscount.Text);
                }
                decimal DiscountAmt = (sum * DiscountPer) / 100;
                decimal AmtTobePaid = sum - DiscountAmt;
                txtatbepaid.Text = AmtTobePaid.ToString();
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Record Not Found...!";
                RentDetail.Visible = false;
                TotalAmt.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }
    }
    protected void ddlWhrNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillGodownNewRent();
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGodownNewRent();
    }
    protected void btnSumbmitRent_Click(object sender, EventArgs e)
    {
        if (txtamount.Text=="" || txtramount.Text=="" || txtTotalCharge.Text=="")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg1", "<script language=javascript>alert('Please Fill Amount');</script>");
        }
        else
        {
            try
            {
                decimal TotalAmount = Convert.ToDecimal(txtTotalCharge.Text);
                decimal PaidAmount = Convert.ToDecimal(txtamount.Text);
                decimal RemainingAmount = Convert.ToDecimal(txtramount.Text);
                string DepositorType = ddldepositor.SelectedValue.ToString();
                string DepositorName = ddldepos_name.SelectedItem.Text;
                string DepositorId = ddldepos_name.SelectedValue.ToString();
                string Godown = ddlgodown.SelectedValue.ToString();
                string DepositorPaymentId = Godown + "" + DepositorType + "" + DepositorId;
                string DateofPayment = getDate_MDY(txtdop.Text);
                string I="NNN";
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string qry = "insert into tbl_Depositor_Godown_Paid_Rent_Detail(Depositor_Payment_Id,Depositor_Id,Depositor_Name,Depositor_Type,Godown,Total_Amount,Paid_Amount,Remaining_Amount,Date_Of_Payment,Created_Date,Client_IP) values ('" + DepositorPaymentId + "','" + DepositorId + "','" + DepositorName + "','" + DepositorType + "','" + Godown + "','" + TotalAmount + "','" + PaidAmount + "','" + RemainingAmount + "','" + DateofPayment + "',getdate(),'" + ip + "')";
                con.Open();
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.ExecuteNonQuery();
                con.Close();
                btnSumbmitRent.Enabled = false;
                I = "YYY";
                if (I == "YYY")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Saved Successfully');", true);
                }
            }
            catch (Exception ex)
            {
                lblerror.Text = ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
    }
    protected void brnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void ddlDeposCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeposCategory.SelectedItem.Text == "GEN" || ddlDeposCategory.SelectedItem.Text == "OBC")
        {
            txtdiscount.Text = "30";
        }
        else if (ddlDeposCategory.SelectedItem.Text == "SC" || ddlDeposCategory.SelectedItem.Text == "ST")
        {
            txtdiscount.Text = "40";
        }
    }
    protected void txtamount_TextChanged(object sender, EventArgs e)
    {
        //txtamount.Text = (CheckNull(txtatbepaid.Text) - CheckNull(txtramount.Text)).ToString();
        decimal AmtToBePaid = Convert.ToDecimal(txtatbepaid.Text);
        decimal PaidAmt = Convert.ToDecimal(txtamount.Text);
        decimal BalanceAmt = AmtToBePaid - PaidAmt;
        txtramount.Text = BalanceAmt.ToString();
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "")
        {
            inDate = FixDateTime(inDate).ToString();
        }
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public static DateTime FixDateTime(object valueToFix)
    {
        return FixDate(valueToFix);
    }
    public static DateTime FixDate(object valueToFix)
    {
        if (valueToFix == null)
            return new DateTime(1899, 1, 1);
        else if (Convert.IsDBNull(valueToFix))
            return new DateTime(1899, 1, 1);
        else
        {
            try
            {
                return Convert.ToDateTime(valueToFix);
            }
            catch
            {
                return new DateTime(1899, 1, 1);
            }
        }
    }
    decimal CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;

    }
    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;

    }
    protected void Printcurrentdate()
    {
        try
        {
            string query = "SELECT  convert(varchar(10),getdate(),103) as 'Date1'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtdop.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
                txtdop.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
    }
}
