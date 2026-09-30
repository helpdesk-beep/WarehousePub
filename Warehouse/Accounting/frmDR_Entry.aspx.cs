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

public partial class Depot_frmDR_Entry : System.Web.UI.Page
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
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (!IsPostBack)
            { 
                GetDepositioType();
                GetVerity();
                Getbank();
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
        da = new SqlDataAdapter(qry,con);
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
            ddlverity.Items.Insert(0, "--Se|ect--");
        }
    }
    void GetCommodity()
    {
        string verity=ddlverity.SelectedValue ;
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
            ddlcomodity.Items.Insert(0, "--Se|ect--");
        }
    }
    void GetDepositorName()
    {
        try
        {
            string dtype = ddldepositor.SelectedItem.Text;
            if (dtype == "Institution")
            {
                string qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name  in ('MPSCSC','MPWLC','FCI') union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE  BranchId='" + Session["BranchID"].ToString() + "' and Depositor_Type ='Institution'";
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
        catch (Exception ex)
        {
            lblerror.Visible = true;
            lblerror.Text = ex.Message;
        }
    }
    void GetGodown()
    {
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
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
    void Getbank()
    {
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        ddl_bank.Items.Clear();
        qry = "select BankName,Bank_ID from dbo.tbl_MetaData_BANK where District_Id='23" + Dist_id + "' and Depotid='" + Session["BranchID"].ToString() + "'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddl_bank.Items.Insert(0, "--Se|ect--");
        }
        else
        {
            ddl_bank.DataSource = ds.Tables[0];
            ddl_bank.DataTextField = "BankName";
            ddl_bank.DataValueField = "Bank_ID";
            ddl_bank.DataBind();
            ddl_bank.Items.Insert(0, "--Se|ect--");
        }
    }
    void GetDepositorAdd()
    {
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string Depo_Id = Session["BranchID"].ToString();
        string dtype = ddldepositor.SelectedItem.Text;
        string dptrid = ddldepos_name.SelectedValue;
        qry = "select * from dbo.tbl_MetaData_DEPOSITOR where Depot_ID='" + Depo_Id + "' and District_ID='23" + Dist_id + "' and Depositor_Type='" + dtype + "' and Depositor_ID='" + dptrid + "'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ddldepositor.SelectedItem.Text != "Institution")
        {
            if (ds == null)
            {
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                txtaddress.Text = dr["Address"].ToString();
            }
        }
        else
        {
            txtaddress.Text = "CivilSuppCorp";
        }
    }
    void GetCapacity()
    {
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string Depo_Id = Session["BranchID"].ToString();
        string gid = ddlgodown.SelectedValue;
        string dptrid = ddldepos_name.SelectedValue;
        qry = "select Godown_Capacity,Convert(int,Godown_Capacity) as GodownBags from dbo.tbl_MetaData_GODOWN where DistrictId='23" + Dist_id + "' and DepotId='" + Depo_Id + "' and Godown_ID='" + ddlgodown.SelectedValue + "'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
            }
            else
            {
                DataRow dr = ds.Tables[0].Rows[0];
                txtmaxcap.Text = dr["Godown_Capacity"].ToString();
                int bags = int.Parse(dr["GodownBags"].ToString());
                txtmaxbags.Text = (bags*2).ToString();    
            }
        }
    }
    void GetCapOcc()
    {
        try
        {
            string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
            string Depo_Id = Session["BranchID"].ToString();
            int recdbag = 0;
            decimal recqty = 0;
            int delbags = 0;
            decimal delqty = 0;
            int resbags = 0;
            decimal resqty = 0;
            string gid = ddlgodown.SelectedValue;
            string dptrid = ddldepos_name.SelectedValue;
            string getcap = "SELECT tbl_MetaData_GODOWN.DistrictId, tbl_MetaData_GODOWN.DepotId, tbl_MetaData_GODOWN.Godown_ID,Sum(tbl_storage_Stacking_Details.Bags) as Bags , Sum(tbl_storage_Stacking_Details.Weight) as Weight , Sum(tbl_Delivery_Stacking_Details.No_Of_Bags) as DelBags,Sum(tbl_Delivery_Stacking_Details.Bags_Weight) as DelWeight, Sum(tbl_Delivery_Stacking_Details.Loss) as Loss , Sum(tbl_Delivery_Stacking_Details.Gain) as Gain  FROM tbl_MetaData_GODOWN left JOIN tbl_storage_Stacking_Details ON tbl_MetaData_GODOWN.Godown_ID = tbl_storage_Stacking_Details.Godown_ID AND tbl_MetaData_GODOWN.DistrictId = tbl_storage_Stacking_Details.District_Id AND tbl_MetaData_GODOWN.DepotId = tbl_storage_Stacking_Details.Depotid left JOIN tbl_Delivery_Stacking_Details ON tbl_MetaData_GODOWN.Godown_ID = tbl_Delivery_Stacking_Details.Godown_ID where tbl_MetaData_GODOWN.Godown_ID ='" + ddlgodown.SelectedValue + "' and tbl_MetaData_GODOWN.DepotId='" + Depo_Id + "' group by tbl_MetaData_GODOWN.DistrictId, tbl_MetaData_GODOWN.DepotId, tbl_MetaData_GODOWN.Godown_ID";
            da = new SqlDataAdapter(getcap, con);
            ds = new DataSet();
            da.Fill(ds);
            if (ds == null)
            {
            }
            else
            {
                if (ds.Tables[0].Rows.Count == 0)
                {
                }
                else
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    recdbag = CheckNullInt(dr["Bags"].ToString());
                    recqty = CheckNull(dr["Weight"].ToString());
                    delbags = CheckNullInt(dr["DelBags"].ToString());
                    delqty = CheckNull(dr["DelWeight"].ToString());

                    string getres = "SELECT Sum(Quantity) as  Quantity, Sum(Bags) as Bags   from Godown_Reservation where district_code='23" + Dist_id + "' and Depot_Id='" + Depo_Id + "' and Godown='" + ddlgodown.SelectedValue + "'";
                    da = new SqlDataAdapter(getres, con);
                    ds2 = new DataSet();
                    da.Fill(ds2);
                    if (ds2 == null)
                    {
                    }
                    else
                    {
                        if (ds2.Tables[0].Rows.Count == 0)
                        {
                        }
                        else
                        {
                            DataRow drres = ds2.Tables[0].Rows[0];
                            resqty = CheckNull(drres["Quantity"].ToString());
                            //resqty = CheckNull(drres["Weight"].ToString());
                            resbags = CheckNullInt(drres["Bags"].ToString());

                        }
                    }
                    txtusecap.Text = (recqty - delqty + resqty).ToString();
                    txtusebags.Text = (recdbag - delbags + resbags).ToString();
                    txtvaccap.Text = (CheckNull(txtmaxcap.Text) - CheckNull(txtusecap.Text)).ToString();
                    txtvacbags.Text = (CheckNullInt(txtmaxbags.Text) - CheckNullInt(txtusebags.Text)).ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblerror.Visible = true;
            lblerror.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }
    }
    public string get_days(DateTime fromDate, DateTime toDate)
    {
        int y1 = 0, m1 = 0, d1 = 0, y2 = 0, m2 = 0, d2 = 0;
        y1 = fromDate.Year;
        m1 = fromDate.Month;
        d1 = fromDate.Day;
        y2 = toDate.Year;
        m2 = toDate.Month;
        d2 = toDate.Day;

        int y = (y2 - y1) * 12;
        int m = (y + m2) - m1;
        int d = (m * 30) + d2;
        int day = d - d1;
        return day.ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void ddldepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
         GetDepositorName();
         ddlcomodity.Items.Clear();
         ddlverity.Items.Clear();
         GetVerity();
         if (ddldepositor.SelectedValue == "1" || ddldepositor.SelectedValue == "2" || ddldepositor.SelectedValue == "3")
        {         
            ddlcategory.Enabled = true;
            lblreqdoc.Visible = true;
            rdbdocst.Visible = true;
        }
        else
        {           
            ddlcategory.Enabled = false;
            ddlcategory.SelectedValue = "Other";
            lblreqdoc.Visible = false ;
            rdbdocst.Visible = false;
        }    
    }
    protected void ddlverity_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCommodity();
    }
    protected void ddldepos_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepositorAdd();
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCapacity();
        GetCapOcc();
    }
    protected void txtaddress_TextChanged(object sender, EventArgs e)
    {

    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate =="" || inDate ==null )
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string Depo_Id = Session["BranchID"].ToString();
        string getcap = "SELECT * from dbo.Godown_Reservation where district_code='" + Dist_id + "' and Depot_Id='" + Depo_Id  + "' and Receipt_no='" + txtrecdno.Text + "'";
        da = new SqlDataAdapter(getcap, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            if (ds.Tables[0].Rows.Count == 0)
            {

                if (ddldepositor.SelectedItem.Text == "--Se|ect--" || ddldepos_name.SelectedItem.Text == "--Se|ect--" || ddlverity.SelectedItem.Text == "--Se|ect--" || ddlcomodity.SelectedItem.Text == "--Se|ect--" || ddlgodown.SelectedItem.Text == "--Se|ect--" || txtentrydt.Text=="")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(),"mymsg2", "<script language=javascript> alert('Please Select Depositor/Depositor Name/Verity/Commodity/Godown/Date'); </script> ");
                    }
                    else
                    {
                        string mrecdno = txtrecdno.Text;
                        string mentrydate = getDate_MDY(txtentrydt.Text);
                        string mdeptype = ddldepositor.SelectedValue;
                        string mdepname = ddldepos_name.SelectedValue;
                        string mdepadd = txtaddress.Text;
                        string mdepcat = ddlcategory.SelectedValue;
                        string mcomcat = ddlverity.SelectedValue;
                        string mcomdty = ddlcomodity.SelectedValue;
                        decimal mqty = CheckNull(txtqty.Text);
                        int mbags = CheckNullInt(txtbags.Text);
                        decimal mamt = CheckNull(txtamount.Text);
                        string madres = rdbadvres.SelectedValue;
                        string madpay = rdbadpay.SelectedValue;
                        string mdocs = rdbdocst.SelectedValue;
                        decimal mdiscount = CheckNull(txtdiscount.Text);
                        string mtaxtds = ddltaxtds.SelectedValue;
                        decimal msertax = CheckNull(txtstax.Text);
                        decimal mtds = CheckNull(txttds.Text);
                        decimal mnetamt = CheckNull(txtnetamt.Text);
                        decimal mamtdip = CheckNull(txtamtdeposited.Text);
                        decimal credit = mnetamt - mamtdip;
                        string mgodown = ddlgodown.SelectedValue;
                        string mremark = txtremarks.Text;
                        string did = Session["Depot_DistID"].ToString().Substring(2, 2);
                        string mtid = did + mrecdno;
                        //string mtid = "";
                        string mfromresrvd = getDate_MDY(txtfdate.Text);
                        string mtoresrvd = getDate_MDY(txttodate.Text);
                        decimal rate = CheckNull(txtcrate.Text);
                        string disper = ddldicount.SelectedValue;
                        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        string pmode = ddl_pmode.SelectedItem.Value;
                        string ddchqno = tx_dd_no.Text;
                        string ddchqdate = getDate_MDY(txtdd_date.Text);
                        string bankid = ddl_bank.SelectedItem.Value;
                        //string bankid = "--Select--";
                        string qry = "INSERT INTO dbo.Godown_Reservation( district_code, Depot_Id, Receipt_no, Entry_Date, Depositor_type,Depositor_Name,Depositor_Add, Depositor_Cat,Commodity_Verity, Commodity_ID, Quantity, Bags,Rate, Amount, Advance_Res, Advance_Pay, Doc_Submitted, From_Date, To_Date,Dis_percent, Discount, Tax_Tds_Invoked ,Service_Tax,TDS,Net_Amount,Amount_Deposited,Godown,payment_mode,DD_chq_no,DD_chq_date,Bank_id,Remarks,Transuction,Created_Date,IP,IsReserved,Credit_amt)Values('" + Dist_id + "','" + Depo_Id + "','" + mrecdno + "','" + mentrydate + "','" + mdeptype + "','" + mdepname + "','" + mdepadd + "','" + mdepcat + "'," + mcomcat + "," + mcomdty + "," + mqty + "," + mbags + "," + rate + "," + mamt + ",'" + madres + "','" + madpay + "','" + mdocs + "','" + mfromresrvd + "','" + mtoresrvd + "','" + disper + "'," + mdiscount + ",'" + mtaxtds + "'," + msertax + "," + mtds + "," + mnetamt + "," + mamtdip + ",'" + mgodown + "','" + pmode + "','" + ddchqno + "','" + ddchqdate + "','" + bankid + "','" + mremark + "','" + mtid + "',getdate(),'" + ip + "','Y'," + credit + ")";
                        cmd.Connection = con;
                        cmd.CommandText = qry;

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            ClientScript.RegisterClientScriptBlock(this.GetType(),"mymsg2", "<script language=javascript> alert('Data Saved Successfully...'); </script> ");
                            Button2.Enabled = false;
                        }
                        catch (Exception ex)
                        {
                            lblerror.Visible = true;
                            lblerror.Text = ex.Message;
                        }
                        finally
                        {
                            con.Close();
                        }                   
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(),"mymsg2", "<script language=javascript> alert('Receipt No-- " + txtrecdno.Text +" --Already Exist '); </script> ");
            }           
        }            
    }
    protected void ddlcomodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtfdate.Text==""||txttodate.Text==""||ddldepositor.SelectedItem.Text == "--Se|ect--" || ddldepos_name.SelectedItem.Text == "--Se|ect--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Godown Reservation Date/D'); </script> ");
        }
        GetRate();
    }
    void GetRate()
    {
        string FromDate = getDate_MDY(txtfdate.Text);
        //string grate = "select * from dbo.tbl_MetaData_Storage_Rate where Verity_Code='" + ddlverity.SelectedValue + "' and Commodity_Id='" + ddlcomodity.SelectedValue + "'";
        string grate = "select max(Rate) as rate from tbl_MetaDataEffectiveRateDetail where Commodity_Id='" + ddlcomodity.SelectedValue.ToString() +"' and Rate_Effective_Date<='" + FromDate + "' and Depositor_Type='" + ddldepositor.SelectedValue.ToString() + "' and Commodity_Type='" + ddlverity.SelectedValue.ToString() + "'";
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
    protected void txtbags_TextChanged(object sender, EventArgs e)
    {
        txtamount.Text = (CheckNull(txtcrate.Text) * CheckNull(txtbags.Text)).ToString();
        GetDiscount(); 
    }    
    protected void rdbadpay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtfdate.Text == "" || txttodate.Text == "" || rdbadvres.SelectedValue=="")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(),"mymsg2", "<script language=javascript> alert('Please Enter the Reservetion period or Check Advanced Reservation'); </script> ");
        }
        else
        {
            decimal amt = CheckNull(txtamount.Text);
            decimal disc = 0;
            if (rdbadvres.SelectedValue == "Y" && rdbadpay.SelectedValue == "Y")
            {
                int cmonth = int.Parse(lblmonth.Text);
                int cdays = int.Parse(lbldays.Text);

                if (cmonth >= 3 && cmonth < 4)
                {
                    disc = 10;
                    lbldiscmsg.Text = "10 % Discount for Advance Reservetion and Advance Payment for the period of 3 months or more..";
                }
                else if (cmonth >= 4 && cmonth < 6)
                {
                    disc = 15;
                    lbldiscmsg.Text = "15 % Discount for Advance Reservetion and Advance Payment for the period of 4 months or more.. ";
                }
                else if (cmonth >= 6 && cmonth < 12)
                {
                    disc = 20;
                    lbldiscmsg.Text = "20 % Discount for Advance Reservetion and Advance Payment for the period of 6 months or more.. ";
                }
                else if (cmonth >= 12)
                {
                    disc = 25;
                    lbldiscmsg.Text = "25 % Discount for Advance Reservetion and Advance Payment for the period of 12 months or more..";
                }
                decimal disamt = (amt * disc) / 100;
                txtdiscount.Text = disamt.ToString();
            }
        }
    }
    protected void ddltaxtds_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtfdate.Text == "" || txttodate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(),"mymsg2", "<script language=javascript> alert('Please Enter the Reservetion period'); </script> ");
        }
        else
        {
            string chkdup = "Select * from dbo.Tax_Master";
            da = new SqlDataAdapter(chkdup, con);
            ds = new DataSet();
            da.Fill(ds);
            if (ds == null)
            {
            }
            else
            {
                if (ds.Tables[0].Rows.Count == 0)
                {

                }
                else
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    lblstax.Text = dr["Service_Tax"].ToString();
                    lbltds.Text = dr["TDS"].ToString();

                }
            }
            decimal amt = CheckNull(txtamount.Text);
            decimal discount = CheckNull(txtdiscount.Text);
            if (ddltaxtds.SelectedValue == "Y")
            {
                decimal st1 = decimal.Parse(lblstax.Text);
                decimal disamt = (amt * st1) / 100;
                txtstax.Text = disamt.ToString();

                decimal st2 = decimal.Parse(lbltds.Text);
                decimal tdsamt = (amt * (st2)) / 100;

                txttds.Text = tdsamt.ToString();
                decimal rate = (amt + CheckNull(txtstax.Text)) -( CheckNull(txttds.Text) +discount);

                int cmonth = int.Parse(lblmonth.Text);
                int cdays = int.Parse(lbldays.Text);

                if (cdays < 15 && cdays>0)
                {
                    decimal ddrate = rate;
                    decimal dddrate = ddrate / 2;
                    rate = rate * cmonth;
                    rate = rate + dddrate;                  
                }
                else
                {
                    //cmonth = cmonth + 1;
                    rate = rate * cmonth;
                }
                txtnetamt.Text = rate.ToString();
            }
            else
            {
                decimal st1 = 1;
                decimal disamt = (amt * st1) / 100;
                txtstax.Text = "0";

                decimal st2 = 1;
                decimal tdsamt = (amt * (st2)) / 100;

                txttds.Text = "0";
                decimal rate = (amt + CheckNull(txtstax.Text)) - (CheckNull(txttds.Text) + discount);

                int cmonth = int.Parse(lblmonth.Text);
                int cdays = int.Parse(lbldays.Text);

                if (cdays < 15 && cdays > 0)
                {
                    decimal ddrate = rate;
                    decimal dddrate = ddrate / 2;
                    rate = rate * cmonth;
                    rate = rate + dddrate;
                }
                else
                {
                    //cmonth = cmonth + 1;
                    rate = rate * cmonth;
                }
                txtnetamt.Text = rate.ToString();
            }
        }
        txtamtdeposited.Focus();
    }
    protected void rdbdocst_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddltaxtds.Enabled = true;
            string dtype = ddldepositor.SelectedValue;
            string dcat = ddlcategory.SelectedValue;
            decimal amt = CheckNull(txtamount.Text);
            int bags = CheckNullInt(txtbags.Text);
            if (rdbadvres.SelectedValue == "Y" && rdbadpay.SelectedValue == "Y")
            {
            }
            else
            {
                if (dtype == "3")
                {
                    if (bags > 200)
                    {
                        lbldiscnt.Visible = true;
                        lbldiscnt.Text = "(0%)";
                        txtdiscount.Text = "0";
                        ddldicount.SelectedValue = "0%";
                        lbldiscmsg.Text = "discount is aplicable only  for 200 bags";

                        if (dcat == "GEN")
                        {
                            lbldiscnt.Visible = true;
                            lbldiscnt.Text = "(30%)";
                            bags = bags - 200;
                            amt = bags * (CheckNull(txtcrate.Text));
                            decimal disamt = (amt * 30) / 100;
                            txtdiscount.Text = disamt.ToString();
                            ddldicount.SelectedValue = "30%";
                            lbldiscmsg.Text = "! 30 % Discount for GEN Category Fertilizer : 200 bags per season";
                        }
                        else if (dcat == "SC/ST")
                        {
                            lbldiscnt.Visible = true;
                            lbldiscnt.Text = "(40%)";
                            bags = bags - 200;
                            amt = bags * (CheckNull(txtcrate.Text));
                            decimal disamt = (amt * 40) / 100;
                            txtdiscount.Text = disamt.ToString();
                            ddldicount.SelectedValue = "40%";
                            lbldiscmsg.Text = "! 40 % Discount for SC/ST Category Fertilizer : 200 bags per season ";
                        }
                    }
                    else
                    {
                        if (dcat == "GEN")
                        {
                            lbldiscnt.Visible = true;
                            lbldiscnt.Text = "(30%)";
                            decimal disamt = (amt * 30) / 100;
                            txtdiscount.Text = disamt.ToString();
                            ddldicount.SelectedValue = "30%";
                            lbldiscmsg.Text = "! 30 % Discount for GEN Category Fertilizer";
                        }
                        else if (dcat == "SC/ST")
                        {
                            lbldiscnt.Visible = true;
                            lbldiscnt.Text = "(40%)";
                            decimal disamt = (amt * 40) / 100;
                            txtdiscount.Text = disamt.ToString();
                            ddldicount.SelectedValue = "40%";
                            lbldiscmsg.Text = "! 40 % Discount for GEN Category Fertilizer";
                        }
                    }
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            lblerror.Visible = true;
            lblerror.Text = ex.Message;
        }
    }
    void GetDiscount()
    {
        string dtype = ddldepositor.SelectedValue;
        string dcat = ddlcategory.SelectedValue;
        decimal amt = CheckNull(txtamount.Text);
        int bags = CheckNullInt(txtbags.Text);
       
            lbldiscnt.Visible = true;
            lbldiscnt.Text = "(10%)";
            decimal disamt = (amt * 10) / 100;
            txtdiscount.Text = disamt.ToString();
            ddldicount.SelectedValue = "10%";
            lbldiscmsg.Text = "10 % Discount for Institution and Other";       
    }
    public void DateD(DateTime date, DateTime  dateToCompare)   
{       
    // First we calculate total months  
      int totalMonths = ((date.Year - dateToCompare.Year) * 12) + date.Month - dateToCompare.Month;  
    int days = 0; 
    // A month completes on one day before the exact day of the  
    // actual date. For example, if starting date is 15-Mar, one   
    // month will complete on 14-Apr regardless of how many days  
    // are present in march.  
    // So, this is the code to do the same...
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
        //DateSpan ds = new DateSpan();  
        //ds.Years = totalMonths / 12;  
        //ds.Months = totalMonths % 12;  
        //ds.Days = days;  
        //return ds;  
    }
    protected void rdbadvres_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (txtfdate.Text == "" || txttodate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(),"mymsg2", "<script language=javascript> alert('Please Enter Reservation Period...'); </script> ");
        }
        else
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
            if (mm < 3)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(),"mymsg2", "<script language=javascript> alert('Reservation Period Should not be less than 3 Months...'); </script> ");
                
                Button2.Enabled = false;
            }
            else
            {
                Button2.Enabled = true;

            }
        }      
    }

    protected void btnnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounting/frmDR_Entry.aspx");
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
                txtentrydt.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
                txtentrydt.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
    }
}
