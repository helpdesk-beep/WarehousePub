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

public partial class Accounting_frm_Storage_Rent : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    DataSet ds2 = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillCropYear();
            GetDepositioType();
            GetVerity();
        }
    }
    protected void fillCropYear()
    {
        ddlcropyr.Items.Insert(0, "2015");
        ddlcropyr.Items.Insert(1, "2014");
        ddlcropyr.Items.Insert(2, "2013");
        ddlcropyr.Items.Insert(3, "2012");
        ddlcropyr.Items.Insert(4, "2011");
        ddlcropyr.Items.Insert(5, "2010");
        ddlcropyr.Items.Insert(6, "2009");
        ddlcropyr.Items.Insert(7, "Before 2009");
        ddlcropyr.SelectedIndex = 0;
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
            ddldepositor.Items.Insert(0, "--Select--");
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
                ddldepos_name.Items.Insert(0, "--Select--");
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
                ddldepos_name.Items.Insert(0, "--Select--");
            }
        }
    }
    protected void ddldepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepositorName();
    }
    void GetVerity()
    {
        qry = "select verity_code,Verity_Eng from dbo.tbl_MetaData_Verity";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlverity.DataSource = ds.Tables[0];
            ddlverity.DataTextField = "Verity_Eng";
            ddlverity.DataValueField = "verity_code";
            ddlverity.DataBind();
            ddlverity.Items.Insert(0, "--Select--");
        }
    }
    void GetCommodity()
    {
        string verity = ddlverity.SelectedValue;
        qry = "select Commodity_ID,Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY_RList where Rep_Grp_Code='" + verity + "'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlcomodity.DataSource = ds.Tables[0];
            ddlcomodity.DataTextField = "Commodity_Name";
            ddlcomodity.DataValueField = "Commodity_ID";
            ddlcomodity.DataBind();
            ddlcomodity.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlverity_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCommodity();
    }
    protected void ddlBillType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldepositor.SelectedItem.Text == "--Select--" || ddlverity.SelectedItem.Text == "--Select--")
        {          
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Depositor/Commodity...'); </script> ");
        }
        else if (ddldepos_name.SelectedItem.Text == "--Select--" || ddlcomodity.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Depositor/Commodity...'); </script> ");
        }
        else
        {
            if (ddlBillType.SelectedValue == "2")
            {                                       //Storage Bill
                txtcomrate.ReadOnly = false;
                lblcrate.Visible = true;
                txtcomrate.Visible = true;
                trRentBill.Visible = true;
                TotalAmt.Visible = true;
                trReservationBill.Visible = false;
                FillGodownNewRent();
                btnsubmit.Visible = true;
                btncancel.Visible = true;
                Printcurrentdate();
            }
            else if (ddlBillType.SelectedValue == "1")
            {                                         //Reservation Bill
                ClearReservation();
                lblcrate.Visible = false;
                txtcomrate.Visible = false;
                trReservationBill.Visible = true;
                trRentBill.Visible = false;
                TotalAmt.Visible = false;
                GetPackingType();
                GetWeight();
                btnsubmit.Visible = true;
                btncancel.Visible = true;
            }
        }
    }
    void ClearReservation()
    {
        txtfdate.Text = "";
        txttodate.Text = "";
        txtqty.Text = "";
        txtcrate.Text = "";
        lblmonth.Text = "";
        lbldays.Text = "";
        txtamount.Text = "";
    }

    void GetPackingType()
    {
        string qry = "select Packing_Id,Packing_Name + '('+rtrim(Remarks)+')' as Packing_Name from dbo.Packing_type ";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlpacktype.DataSource = ds.Tables[0];
            ddlpacktype.DataTextField = "Packing_Name";
            ddlpacktype.DataValueField = "Packing_Id";
            ddlpacktype.DataBind();
            ddlpacktype.Items.Insert(0, "--Select--");
        }
    }
    void GetWeight()
    {
        string qry = "select Weigt_ID,Weight_Type from dbo.tbl_MetaData_WeightType";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlweight.DataSource = ds.Tables[0];
            ddlweight.DataTextField = "Weight_Type";
            ddlweight.DataValueField = "Weigt_ID";
            ddlweight.DataBind();
            ddlweight.Items.Insert(0, "--Select--");
        }
    }
    void GetRate()
    {
        string FromDate = getDate_MDY(txtfdate.Text);
        //string grate = "select * from dbo.tbl_MetaData_Storage_Rate where Verity_Code='" + ddlverity.SelectedValue + "' and Commodity_Id='" + ddlcomodity.SelectedValue + "'";
        string grate = "select max(Rate) as rate from tbl_MetaDataEffectiveRateDetail where Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and Rate_Effective_Date<='" + FromDate + "' and Depositor_Type='" + ddldepositor.SelectedValue.ToString() + "' and Commodity_Type='" + ddlverity.SelectedValue.ToString() + "'";
        da = new SqlDataAdapter(grate, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                lblrate.Visible = true;
                txtcrate.Visible = true;
                txtcrate.Text = "0";
            }
            else
            {
                lblrate.Visible = true;
                txtcrate.Visible = true;
                DataRow dr = ds.Tables[0].Rows[0];
                txtcrate.Text = dr["Rate"].ToString();
            }
        }
    }

    protected void ddlpacktype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtqty_TextChanged(object sender, EventArgs e)
    {
        GetRate();
    }
    public void DateD(DateTime date, DateTime dateToCompare)
    {
        // First we calculate total months  
        int totalMonths = ((date.Year - dateToCompare.Year) * 12) + date.Month - dateToCompare.Month;
        int days = 0;
        lblmonth.Text = totalMonths.ToString();
        if (date.Day < dateToCompare.Day)
        {
            int day, month, year;
            day = dateToCompare.Day;
            // If month is jan, switch to dec 
            if (date.Month == 1)
            {
                month = 12;
                year = date.Year - 1;
            }
            else
            {
                month = date.Month - 1;
                year = date.Year;
            }
            DateTime dateCalculator = new DateTime(year, month, day);
            days = (date - dateCalculator).Days;
            //return  totalMonths--; 
            lblmonth.Text = (totalMonths--).ToString();
            lbldays.Text = days.ToString();
        }
        else
        {
            days = date.Day - dateToCompare.Day;
            //return days;
            lbldays.Text = days.ToString();
        }
    }
    protected void rdbadpay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtfdate.Text == "" || txttodate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Reservation Period...'); </script> ");
        }
        else
        {
            try
            {
                DateTime fdate = new DateTime();
                DateTime tdate = new DateTime();

                string sfdate = getDate_MDY(txtfdate.Text);
                //string sfdate = txtfdate.Text;
                //fdate = DateTime.Parse(sfdate, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"));
                fdate = DateTime.Parse(sfdate);

                string stodate = getDate_MDY(txttodate.Text);
                //string stodate = txttodate.Text;
                //tdate = DateTime.Parse(stodate, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"));
                tdate = DateTime.Parse(stodate);
                DateD(tdate, fdate);
                int mm = int.Parse(lblmonth.Text);
                ///// calculate rent
                decimal rate = Convert.ToDecimal(txtcrate.Text);
                decimal qty = Convert.ToDecimal(txtqty.Text);
                decimal AmtPerMonth = rate * qty;
                txtamount.Text = AmtPerMonth.ToString();
                decimal Months = Convert.ToDecimal(lblmonth.Text);
                txttamt.Text = (AmtPerMonth * Months).ToString();
                ////
                if (mm < 3)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Reservation Period Should not be less than 3 Months...'); </script> ");

                    //Button2.Enabled = false;
                }
                else
                {
                    //Button2.Enabled = true;

                }
            }
            catch(Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }      
    }
    void FillGodownNewRent()
    {
        try
        {
            string BranchID = Session["BranchID"].ToString();
            qry = "select a.[Godown_ID],a.[Depositor_WHR_Id],a.[Depositor_Name],convert(varchar(10),a.[whrDate],103) as WHR_Date,a.[whryear],convert(varchar(10),a.[Date_of_Deposit],103) as Date_of_Deposit,a.[Godown_Name],a.[Commodity_ID],a.[Deposit_Bags],a.[Deposit_Weight],a.[Deliver_Qty],a.[Deliver_Bags],convert(varchar(10),a.[Delivery_Order_Date],103) as Delivery_Order_Date,a.[Issue_Source_ID],a.[PeriodOfStorage],[ChargeOnPeriod],max(b.Rate) as MonthlyRate, max(b.Rate)/30 as PerDayRate,((a.Deliver_Bags*max(b.Rate))*cast(SUBSTRING(a.ChargeOnPeriod,1,2) as int))+ ((a.Deliver_Bags*(max(b.Rate)/30))*cast(SUBSTRING(a.ChargeOnPeriod,11,2) as int)) as Charges from View_Storage_Period as a join tbl_MetaDataEffectiveRateDetail as b on a.Commodity_ID=b.Commodity_Id where b.Depositor_Type='" + ddldepositor.SelectedValue.ToString() + "' and a.Depositor_Name='" + ddldepos_name.SelectedItem.Text + "' and substring(a.Godown_ID,1,7)='" + BranchID + "' and b.Rate_Effective_Date<=a.whrDate and a.whryear='"+ ddlcropyr.SelectedItem.Text +"' group by a.Depositor_Name,a.Godown_ID,a.Commodity_ID,a.Deposit_Bags,a.Deposit_Weight,a.Deliver_Bags,a.Deliver_Qty,a.Date_of_Deposit,a.Delivery_Order_Date,a.PeriodOfStorage,a.Depositor_WHR_Id,a.whrDate,a.whryear,a.Godown_Name,a.Issue_Source_ID,a.ChargeOnPeriod order by a.Depositor_WHR_Id";
            da = new SqlDataAdapter(qry, con);
            Dt1 = new DataTable();
            da.Fill(Dt1);
            if (Dt1.Rows.Count > 0)
            {
                NewGrid.DataSource = Dt1;
                NewGrid.DataBind();

                //RentDetail.Visible = true;
                trRentBill.Visible = true;
                TotalAmt.Visible = true;
                decimal sum = 0;
                for (int i = 0; i < NewGrid.Rows.Count; i++)
                { 
                        sum += Convert.ToDecimal(NewGrid.Rows[i].Cells[10].Text);
                }
                txtcomrate.Text=NewGrid.Rows[0].Cells[8].Text; 
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
                //RentDetail.Visible = false;
                TotalAmt.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void txtSamount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal AmtToBePaid = Convert.ToDecimal(txtatbepaid.Text);
            decimal PaidAmt = Convert.ToDecimal(txtSamount.Text);
            decimal BalanceAmt = AmtToBePaid - PaidAmt;
            txtramount.Text = BalanceAmt.ToString();
        }
        catch(Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void ddlDeposCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeposCategory.SelectedItem.Text == "SC")
        {
            txtdiscount.Text = "30";
        }
        else if (ddlDeposCategory.SelectedItem.Text == "ST")
        {
            txtdiscount.Text = "40";
        }
        else 
        {
             txtdiscount.Text = "0";
        }
        ///////////////////////////////////////
        decimal DiscountAmt = 0;
        decimal TotalAmt = Convert.ToDecimal(txtTotalCharge.Text);
        decimal DiscPer = Convert.ToDecimal(txtdiscount.Text);
        DiscountAmt = TotalAmt * DiscPer / 100;
        txtatbepaid.Text = (TotalAmt - DiscountAmt).ToString();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (trRentBill.Visible == true)
        {
            if (ddlDeposCategory.SelectedValue.ToString() != "1" && txtkhasrano.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Fill Khasra Number...!');", true);
            }
            else if (txtamount.Text=="" && txtramount.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Fill Amount...!');", true);
            }
            else
            {
                try
                {
                    decimal TotalAmount = Convert.ToDecimal(txtTotalCharge.Text);
                    string Depositor_Category = ddlDeposCategory.SelectedItem.Text;
                    string Discount_Per = txtdiscount.Text;
                    string Khasra_Number = txtkhasrano.Text;
                    string Commodity_Type = ddlverity.SelectedValue.ToString();
                    string Commodity_Id = ddlcomodity.SelectedValue.ToString();
                    decimal Commodity_Rate = Convert.ToDecimal(txtcomrate.Text);
                    //txttamt.ReadOnly = false;
                    string Crop_Year = ddlcropyr.SelectedItem.Text;
                    decimal AmtToBePaid = Convert.ToDecimal(txtatbepaid.Text);
                    decimal PaidAmount = Convert.ToDecimal(txtSamount.Text);
                    decimal RemainingAmount = Convert.ToDecimal(txtramount.Text);
                    string DepositorType = ddldepositor.SelectedValue.ToString();
                    string DepositorName = ddldepos_name.SelectedItem.Text;
                    string DepositorId = ddldepos_name.SelectedValue.ToString();
                    string BranchID = Session["BranchID"].ToString();
                    string DepositorPaymentId = BranchID + "" + DepositorType + "" + DepositorId;
                    string DateofPayment = getDate_MDY(txtdop.Text);
                    string I = "NNN";
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string qry = "insert into tbl_Depositor_Godown_Paid_Rent_Detail(Depositor_Payment_Id,Depositor_Id,Depositor_Name,Depositor_Type,Godown,Total_Amount,Paid_Amount,Remaining_Amount,Date_Of_Payment,Created_Date,Client_IP,Branch_Id,Depositor_Category,Discount_Per,Khasra_Number,Amount_To_Be_Paid,Commodity_Type,Commodity_Id,Crop_Year,Commodity_Rate) values ('" + DepositorPaymentId + "','" + DepositorId + "','" + DepositorName + "','" + DepositorType + "','','" + TotalAmount + "','" + PaidAmount + "','" + RemainingAmount + "','" + DateofPayment + "',getdate(),'" + ip + "','" + BranchID + "','" + Depositor_Category + "','" + Discount_Per + "','" + Khasra_Number + "','" + AmtToBePaid + "','" + Commodity_Type + "','" + Commodity_Id + "','" + Crop_Year + "','" + Commodity_Rate + "')";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(qry, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    btnsubmit.Enabled = false;

                    I = "YYY";
                    if (I == "YYY")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Saved Successfully');", true);
                    }
                }
                catch (Exception ex)
                {
                    lblmsg.Text = ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        else if (trReservationBill.Visible == true)
        {
            if (txtfdate.Text=="" && txttodate.Text=="")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Date...!');", true);
            }
            else
            {
                try
                {
                    string FromDate = getDate_MDY(txtfdate.Text);
                    string ToDate = getDate_MDY(txttodate.Text);
                    //decimal TotalAmount = Convert.ToDecimal(txtTotalCharge.Text);
                    string Depositor_Category = ddlDeposCategory.SelectedItem.Text;
                    //string Discount_Per = txtdiscount.Text;
                    string PackType = ddlpacktype.SelectedValue.ToString();
                    string weight = ddlweight.SelectedValue.ToString();
                    int Qty = Convert.ToInt32(txtqty.Text);
                    //string Khasra_Number = txtkhasrano.Text;
                    string Commodity_Type = ddlverity.SelectedValue.ToString();
                    string Commodity_Id = ddlcomodity.SelectedValue.ToString();
                    decimal Commodity_Rate = Convert.ToDecimal(txtcrate.Text);
                    //txttamt.ReadOnly = false;
                    string Crop_Year = ddlcropyr.SelectedItem.Text;
                    decimal AmtToBePaid = Convert.ToDecimal(txttamt.Text);
                    //decimal PaidAmount = Convert.ToDecimal(txtSamount.Text);
                    //decimal RemainingAmount = Convert.ToDecimal(txtramount.Text);
                    string DepositorType = ddldepositor.SelectedValue.ToString();
                    string DepositorName = ddldepos_name.SelectedValue.ToString();
                    string DepositorId = ddldepos_name.SelectedValue.ToString();
                    string BranchID = Session["BranchID"].ToString();
                    string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
                    string DepositorPaymentId = BranchID + "" + DepositorType + "" + DepositorId;
                    //string DateofReservation = getDate_MDY(txtdop.Text);
                    string I = "NNN";
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string qry = "INSERT INTO dbo.Godown_Reservation( district_code, Depot_Id, Receipt_no, Entry_Date, Depositor_type,Depositor_Name,Depositor_Add, Depositor_Cat,Commodity_Verity, Commodity_ID, Quantity, Bags,Rate, Amount, Advance_Res, Advance_Pay, Doc_Submitted, From_Date, To_Date,Dis_percent, Discount, Tax_Tds_Invoked ,Service_Tax,TDS,Net_Amount,Amount_Deposited,Godown,payment_mode,DD_chq_no,DD_chq_date,Bank_id,Remarks,Transuction,Created_Date,IP,IsReserved,Credit_amt)Values('" + Dist_id + "','" + BranchID + "','',getdate(),'" + DepositorType + "','" + DepositorName + "','','" + Depositor_Category + "'," + Commodity_Type + "," + Commodity_Id + "," + Qty + ",''," + Commodity_Rate + "," + AmtToBePaid + ",'','','','','','','','','','','','','','','','','','','',getdate(),'" + ip + "','Y','')";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(qry, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    btnsubmit.Enabled = false;

                    I = "YYY";
                    if (I == "YYY")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Saved Successfully');", true);
                    }
                }
                catch (Exception ex)
                {
                    lblmsg.Text = ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
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
    protected void ddlcomodity_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
    protected void txtcomrate_TextChanged(object sender, EventArgs e)
    {
        trRentBill.Visible = true;
        TotalAmt.Visible = true;
        trReservationBill.Visible = false;
        FillEditStorageRent();
    }
    void FillEditStorageRent()
    {
        try
        {
            decimal ComRate = Convert.ToDecimal(txtcomrate.Text);
            string BranchID = Session["BranchID"].ToString();
            qry = "select a.[Godown_ID],a.[Depositor_WHR_Id],a.[Depositor_Name],convert(varchar(10),a.[whrDate],103) as WHR_Date,a.[whryear],convert(varchar(10),a.[Date_of_Deposit],103) as Date_of_Deposit,a.[Godown_Name],a.[Commodity_ID],a.[Deposit_Bags],a.[Deposit_Weight],a.[Deliver_Qty],a.[Deliver_Bags],convert(varchar(10),a.[Delivery_Order_Date],103) as Delivery_Order_Date,a.[Issue_Source_ID],a.[PeriodOfStorage],[ChargeOnPeriod]," + ComRate + " as MonthlyRate, " + ComRate + "/30 as PerDayRate,((a.Deliver_Bags*" + ComRate + ")*cast(SUBSTRING(a.ChargeOnPeriod,1,2) as int))+ ((a.Deliver_Bags*(" + ComRate + "/30))*cast(SUBSTRING(a.ChargeOnPeriod,11,2) as int)) as Charges from View_Storage_Period as a join tbl_MetaDataEffectiveRateDetail as b on a.Commodity_ID=b.Commodity_Id where b.Depositor_Type='" + ddldepositor.SelectedValue.ToString() + "' and a.Depositor_Name='" + ddldepos_name.SelectedItem.Text + "' and substring(a.Godown_ID,1,7)='" + BranchID + "' and b.Rate_Effective_Date<=a.whrDate group by a.Depositor_Name,a.Godown_ID,a.Commodity_ID,a.Deposit_Bags,a.Deposit_Weight,a.Deliver_Bags,a.Deliver_Qty,a.Date_of_Deposit,a.Delivery_Order_Date,a.PeriodOfStorage,a.Depositor_WHR_Id,a.whrDate,a.whryear,a.Godown_Name,a.Issue_Source_ID,a.ChargeOnPeriod order by a.Depositor_WHR_Id";
            da = new SqlDataAdapter(qry, con);
            Dt1 = new DataTable();
            da.Fill(Dt1);
            if (Dt1.Rows.Count > 0)
            {
                NewGrid.DataSource = Dt1;
                NewGrid.DataBind();

                //RentDetail.Visible = true;
                trRentBill.Visible = true;
                TotalAmt.Visible = true;
                decimal sum = 0;
                for (int i = 0; i < NewGrid.Rows.Count; i++)
                {
                    //if (NewGrid.Rows[i].Cells[18].Text != "&nbsp;")
                    //{
                    sum += Convert.ToDecimal(NewGrid.Rows[i].Cells[10].Text);

                    //}
                }
                txtcomrate.Text = NewGrid.Rows[0].Cells[8].Text;
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
                //RentDetail.Visible = false;
                TotalAmt.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    protected void txtcrate_TextChanged(object sender, EventArgs e)
    {
        GetEditRate();
    }
    void GetEditRate()
    {
        try
        {
            lblrate.Visible = true;
            txtcrate.Visible = true;
            decimal NewCRate = Convert.ToDecimal(txtcrate.Text);
            decimal qty = Convert.ToDecimal(txtqty.Text);
            decimal AmtPerMonth = NewCRate * qty;
            txtamount.Text = AmtPerMonth.ToString();
            decimal Months = Convert.ToDecimal(lblmonth.Text);
            decimal TotalAmt = AmtPerMonth * Months;
            txttamt.Text = TotalAmt.ToString();
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void txttodate_TextChanged(object sender, EventArgs e)
    {
        rdbadpay.ClearSelection();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
}
