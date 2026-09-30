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

public partial class Accounting_frm_Storage_Charges_Bill : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    DataSet ds2 = null;
    decimal ChargeOfTotal = 0;
    decimal RebateAmount = 0;
    decimal NetAmount = 0;
    decimal AccruedNetAmount = 0;
    string NetAmountWord = "";
    string Bill_No = "";
    decimal Discount = 0;
    decimal Service_Tax = 0;
    string Bill_Type = "";
    int BID = 0;
    decimal ChargeOfTotalWeight = 0;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        CalendarExtender1.EndDate = DateTime.Now;   //to dissable future  Date
        CalendarExtender2.EndDate = DateTime.Now;   //to dissable future  Date
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                Printcurrentdate();
                fillCropYear();
                GetDepositioType();
                GetVerity();
                GetPackingType();
                GetWeight();
                Session["dt1"] = null;
                string strMsg = "यहाँ सुविधा कुछ दिनों के लिए सॉफ्टवेयर में कार्य होने कारण बंद कर दी गई हैं |||";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Branch_Welcome.aspx';", true);
            }
        }
        else
        {    
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillCropYear()
    {
        ddlcropyr.Items.Insert(0, "--Select--");
        ddlcropyr.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
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
            //qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name  in ('MPSCSC') union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE  BranchId='" + Session["BranchID"].ToString() + "' and Depositor_Type ='Institution'";
            qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name  in ('MPSCSC') union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE  BranchId='" + Session["BranchID"].ToString() + "' and Depositor_Type ='Institution' and Depositor_Name not like '%nafed%'";

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
            //qry = "select Depositor_Name,Depositor_ID from dbo.tbl_MetaData_DEPOSITOR where Depot_ID='" + Session["BranchID"].ToString() + "' and District_ID='23" + Dist_id + "' and Depositor_Type='" + dtype + "'";
            //qry = "select Depositor_Name,Depositor_ID from dbo.tbl_MetaData_DEPOSITOR where Depot_ID='" + Session["BranchID"].ToString() + "' and District_ID='23" + Dist_id + "' and Depositor_Type='" + dtype + "' and Depositor_Name not like '%nafed%'";
            qry = "select Depositor_Name,Depositor_ID from dbo.tbl_MetaData_DEPOSITOR where BranchId='" + Session["BranchID"].ToString() + "' and District_ID='23" + Dist_id + "' and DepositorType_ID='" + ddldepositor.SelectedValue.ToString() + "' and Depositor_Name not like '%nafed%'";

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
        if (ddldepositor.SelectedValue.ToString() == "1")
        {
            Visible_Rebate();
        }
        else
        {
            Clear_Rebate();
        }
        GetDepositorName();
        Clear_Data();
        trRentBill.Visible = false;
        trReportsView.Visible = false;
        trReservationBill.Visible = false;
        trOverAboveDaily.Visible = false;
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
        ddlCalcTy.Items.Clear();
        fillCalType();
        trRentBill.Visible = false;
        trReportsView.Visible = false;
        trReservationBill.Visible = false;
        trOverAboveDaily.Visible = false;
    }
    protected void fillCalType()
    {
        //ddlCalcTy.Items.Insert(0, "--Select--");
        //ddlCalcTy.DataValueField.Insert(0, "0");
        //ddlCalcTy.Items.Insert(1, "Per Day");
        //ddlCalcTy.DataValueField.Insert(1, "1");
        //ddlCalcTy.Items.Insert(2, "Per Month");
        //ddlCalcTy.DataValueField.Insert(2, "2");
        //ddlCalcTy.Items.Insert(3, "Up To 15 Days");
        //ddlCalcTy.DataValueField.Insert(3, "3");
        if (ddlBillType.SelectedValue.ToString() == "1")
        {
            if (ddldepositor.SelectedValue.ToString() == "4")
            {
                ddlCalcTy.Items.Add(new ListItem("--Select--", "0"));
                ddlCalcTy.Items.Add(new ListItem("Per Day", "1"));
                //ddlCalcTy.Items.Add(new ListItem("Per Month", "2"));
                //ddlCalcTy.Items.Add(new ListItem("Up To 15 Days", "3"));
            }
            else
            {
                ddlCalcTy.Items.Add(new ListItem("--Select--", "0"));
                ddlCalcTy.Items.Add(new ListItem("Per Day", "1"));
                //ddlCalcTy.Items.Add(new ListItem("Per Month", "2"));
                ddlCalcTy.Items.Add(new ListItem("Up To 15 Days", "3"));
            }
        }
        else if (ddlBillType.SelectedValue.ToString() == "2")
        {
              ddlCalcTy.Items.Add(new ListItem("--Select--", "0"));
            //ddlCalcTy.Items.Add(new ListItem("Per Day", "1"));
            ddlCalcTy.Items.Add(new ListItem("Per Month", "2"));
            //ddlCalcTy.Items.Add(new ListItem("Up To 15 Days", "3"));
        }
        else if (ddlBillType.SelectedValue.ToString() == "3")
        {
            if (ddldepositor.SelectedValue.ToString() == "4")
            {
                ddlCalcTy.Items.Add(new ListItem("--Select--", "0"));
                ddlCalcTy.Items.Add(new ListItem("Per Day", "1"));
                //ddlCalcTy.Items.Add(new ListItem("Per Month", "2"));
                ddlCalcTy.Items.Add(new ListItem("Up To 15 Days", "3"));
            }
            else
            {
                ddlCalcTy.Items.Add(new ListItem("--Select--", "0"));
                //ddlCalcTy.Items.Add(new ListItem("Per Day", "1"));
                //ddlCalcTy.Items.Add(new ListItem("Per Month", "2"));
                //ddlCalcTy.Items.Add(new ListItem("Up To 15 Days", "3"));
            }
        }
        ddlCalcTy.SelectedIndex = 0;
    }
    void GetPackingType()
    {
        //string qry = "select Packing_Id,Packing_Name + '('+rtrim(Remarks)+')' as Packing_Name from dbo.Packing_type ";
        string qry = "select Packing_Id,Packing_Name + '('+rtrim(Remarks)+')' as Packing_Name from dbo.Packing_type where Packing_Id='1'";
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
        //string qry = "select Weigt_ID,Weight_Type from dbo.tbl_MetaData_WeightType";
        string qry = "select Weigt_ID,Weight_Type from dbo.tbl_MetaData_WeightType where Weigt_ID='5'";
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
        try
        {
            string FromDate = getDate_MDY(txtfdate.Text);
            string grate = "select max(Rate) as rate from tbl_MetaDataEffectiveRateDetail where Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and Rate_Effective_Date<='" + FromDate + "' and Commodity_Type='" + ddlverity.SelectedValue.ToString() + "' and Packing_Id='" + ddlpacktype.SelectedValue.ToString() + "' and Weight_ID='" + ddlweight.SelectedValue.ToString() + "'";
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
                    txtcomrate.Text = "0";
                }
                else
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    txtcomrate.Text = dr["Rate"].ToString();
                    decimal Month_Rate = Convert.ToDecimal(dr["Rate"]);
                    txtCPRate.Text = Math.Round(Month_Rate / 30, 2).ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void txtqty_TextChanged(object sender, EventArgs e)
    {
        GetRate();
    }
    protected void ddleposCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeposCategory.SelectedItem.Text == "GEN/OBC")
        {
            txtdiscount.Text = "30";
        }
        else if (ddlDeposCategory.SelectedItem.Text == "SC" || ddlDeposCategory.SelectedItem.Text == "ST")
        {
            txtdiscount.Text = "40";
        }
        else
        {
            txtdiscount.Text = "0";
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
    protected void ddlcomodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillCropYear();
    }
    protected void txtcrate_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void txttodate_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    public void Get_Bill_Type()
    {
        if (ddlBillType.SelectedValue.ToString() == "1" && ddlCalcTy.SelectedValue.ToString() == "1")
        {
            Bill_Type = "AD";
        }
        else if (ddlBillType.SelectedValue.ToString() == "1" && ddlCalcTy.SelectedValue.ToString() == "3")
        {
            Bill_Type = "AU";
        }
        else if (ddlBillType.SelectedValue.ToString() == "3" && ddlCalcTy.SelectedValue.ToString() == "1")
        {
            Bill_Type = "OD";
        }
        else if (ddlBillType.SelectedValue.ToString() == "3" && ddlCalcTy.SelectedValue.ToString() == "3")
        {
            Bill_Type = "OU";
        }
        else if (ddlBillType.SelectedValue.ToString() == "2" && ddlCalcTy.SelectedValue.ToString() == "2")
        {
            Bill_Type = "RB";
        }
    }
    public void Insert_Bill_Detail()
    {
        try
        {

            if (ddlBillType.SelectedValue.ToString() == "3" && ddlCalcTy.SelectedValue.ToString() == "3")
            {
                GetRebateDiscount();
            }
            if (ddlCalcTy.SelectedValue.ToString() != "3")
            {
                GetRebateDiscount();
            }
                   string qry = "";
                   Bill_No = ViewState["BillNo"].ToString();
                   string Is_Rebate = "N";
                   string DeposCategory = "";
                   decimal RebatePer = 0;
                   decimal NoBags = 0;
                   string Rin_Pustika_No = "";
                   string Cast_Certificate_No = "";
                   if ((ddlDeposCategory.SelectedItem.Text != "--Select--" && ddlronbags.SelectedItem.Text != "--Select--" && txtdiscount.Text != "" && txtkhasrano.Text != ""))
                   {
                       Is_Rebate = "Y";
                       DeposCategory = ddlDeposCategory.SelectedItem.Text;
                       RebatePer = Convert.ToDecimal(txtdiscount.Text);
                       NoBags = Convert.ToDecimal(ddlronbags.SelectedValue);
                   }
                   if (ddlBillType.SelectedValue.ToString()=="1" && ddlCalcTy.SelectedValue.ToString() == "3")
                   {
                       txtcomrate.Text = "0";
                       txtCPRate.Text = "0";
                       Rin_Pustika_No = txtrpn.Text;
                       Cast_Certificate_No = txtCastCert.Text;
                   }
            decimal SerTax = (ChargeOfTotal * Convert.ToDecimal(txtstax.Text)) / 100;
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
            string BranchID = Session["BranchID"].ToString();
            Get_Bill_Type();
            BID = Convert.ToInt32(ViewState["BID"]);
            if (ddlBillType.SelectedValue.ToString() == "1" && ddlCalcTy.SelectedValue.ToString() == "3")
            {
                qry = "INSERT INTO tbl_Storage_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','" + ddldepositor.SelectedValue.ToString() + "','" + ddldepos_name.SelectedValue.ToString() + "','','','','','','','" + ddlcropyr.SelectedItem.Text + "'," + txtcomrate.Text + "," + NetAmount + "," + ChargeOfTotal + ",'" + txtstax.Text + "'," + SerTax + ",'" + DeposCategory + "'," + RebatePer + "," + RebateAmount + "," + NoBags + ",'" + txtkhasrano.Text + "','" + Is_Rebate + "',getdate(),'','" + ip + "','" + txtCPRate.Text + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No + "','" + Bill_Type + "','"+ BID +"')";
            }
            else
            {
                //qry = "INSERT INTO tbl_Storage_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','" + ddldepositor.SelectedValue.ToString() + "','" + ddldepos_name.SelectedValue.ToString() + "','" + ddlverity.SelectedValue.ToString() + "','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(txtfdate.Text) + "','" + getDate_MDY(txttodate.Text) + "','" + ddlpacktype.SelectedValue.ToString() + "','" + ddlweight.SelectedValue.ToString() + "','" + ddlcropyr.SelectedItem.Text + "'," + txtcomrate.Text + "," + NetAmount + "," + ChargeOfTotal + ",'" + txtstax.Text + "'," + SerTax + ",'" + DeposCategory + "'," + RebatePer + "," + RebateAmount + "," + NoBags + ",'" + txtkhasrano.Text + "','" + Is_Rebate + "',getdate(),'','" + ip + "','" + txtCPRate.Text + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No + "','" + Bill_Type + "','" + BID + "')";
                //qry = "INSERT INTO tbl_Storage_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Crop_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','" + ddldepositor.SelectedValue.ToString() + "','" + ddldepos_name.SelectedValue.ToString() + "','" + ddlverity.SelectedValue.ToString() + "','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(txtfdate.Text) + "','" + getDate_MDY(txttodate.Text) + "','" + ddlpacktype.SelectedValue.ToString() + "','" + ddlweight.SelectedValue.ToString() + "','" + ddlcropyr.SelectedItem.Text + "'," + txtcomrate.Text + "," + NetAmount + "," + ChargeOfTotal + ",'" + txtstax.Text + "'," + SerTax + ",'" + DeposCategory + "'," + RebatePer + "," + RebateAmount + "," + NoBags + ",'" + txtkhasrano.Text + "','" + Is_Rebate + "',getdate(),'','" + ip + "','" + txtCPRate.Text + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No + "','" + Bill_Type + "','" + BID + "')";
                if (ddlBType.SelectedValue.ToString() == "B" && ddlBType.SelectedValue.ToString() != "-1")
                {
                    qry = "INSERT INTO tbl_Storage_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Crop_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','" + ddldepositor.SelectedValue.ToString() + "','" + ddldepos_name.SelectedValue.ToString() + "','" + ddlverity.SelectedValue.ToString() + "','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(txtfdate.Text) + "','" + getDate_MDY(txttodate.Text) + "','" + ddlpacktype.SelectedValue.ToString() + "','" + ddlweight.SelectedValue.ToString() + "','" + ddlcropyr.SelectedItem.Text + "'," + txtcomrate.Text + "," + NetAmount + "," + ChargeOfTotal + ",'" + txtstax.Text + "'," + SerTax + ",'" + DeposCategory + "'," + RebatePer + "," + RebateAmount + "," + NoBags + ",'" + txtkhasrano.Text + "','" + Is_Rebate + "',getdate(),'','" + ip + "','" + txtCPRate.Text + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No + "','" + Bill_Type + "','" + BID + "')";
                }
                else if (ddlBType.SelectedValue.ToString() == "W" && ddlBType.SelectedValue.ToString() != "-1")
                {
                    SerTax = (ChargeOfTotalWeight * Convert.ToDecimal(txtstax.Text)) / 100;
                    qry = "INSERT INTO tbl_Storage_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Crop_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','" + ddldepositor.SelectedValue.ToString() + "','" + ddldepos_name.SelectedValue.ToString() + "','" + ddlverity.SelectedValue.ToString() + "','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(txtfdate.Text) + "','" + getDate_MDY(txttodate.Text) + "','" + ddlpacktype.SelectedValue.ToString() + "','" + ddlweight.SelectedValue.ToString() + "','" + ddlcropyr.SelectedItem.Text + "'," + txtcomrate.Text + "," + NetAmount + "," + ChargeOfTotalWeight + ",'" + txtstax.Text + "'," + SerTax + ",'" + DeposCategory + "'," + RebatePer + "," + RebateAmount + "," + NoBags + ",'" + txtkhasrano.Text + "','" + Is_Rebate + "',getdate(),'','" + ip + "','" + txtCPRate.Text + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No + "','" + Bill_Type + "','" + BID + "')";

                }

            } 
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
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
    public void Over_N_Above_Accrued_Bill_Detail()
    {
        try
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string Dates = "";
            //DateTime Dates = new DateTime();
            decimal Opening_Balance = 0;
            decimal Rec_Bags = 0;
            decimal Issue_Bags = 0;
            decimal Closing_Balance = 0;
            decimal Per_Day_Rate = 0;
            decimal Monthly_Rate = 0;
            decimal Total_Charges = 0;
            decimal Reserved_Bag = 0;
            decimal Over_Bag = 0;

            //string Godown_Id = "";
            decimal Opening_Weight = 0;
            decimal Rec_Weight = 0;
            decimal Issue_Weight = 0;
            decimal Closing_Weight = 0;
            string Storage_Period = "";
            string Accrued_Period = "";

            if (gvAccruedOverNAbove.Rows.Count > 0)
            {
                for (int i = 0; i <= gvAccruedOverNAbove.Rows.Count - 1; i++)
                {
                    Dates = getDate_MDY(gvAccruedOverNAbove.Rows[i].Cells[0].Text.ToString());
                    if (Dates != "&nbsp;")
                    {
                        Opening_Balance = Convert.ToDecimal(gvAccruedOverNAbove.Rows[i].Cells[1].Text);
                        Rec_Bags = Convert.ToDecimal(gvAccruedOverNAbove.Rows[i].Cells[2].Text);
                        Issue_Bags = Convert.ToDecimal(gvAccruedOverNAbove.Rows[i].Cells[3].Text);
                        Closing_Balance = Convert.ToDecimal(gvAccruedOverNAbove.Rows[i].Cells[4].Text);
                        Reserved_Bag = Convert.ToDecimal(gvAccruedOverNAbove.Rows[i].Cells[5].Text);
                        Over_Bag = Convert.ToDecimal(gvAccruedOverNAbove.Rows[i].Cells[6].Text);
                        Monthly_Rate = Convert.ToDecimal(gvAccruedOverNAbove.Rows[i].Cells[9].Text);
                        Total_Charges = Convert.ToDecimal(gvAccruedOverNAbove.Rows[i].Cells[10].Text);
                        
                        Opening_Weight = Convert.ToDecimal(gvAccruedOverNAbove.Rows[i].Cells[12].Text);
                        Rec_Weight = Convert.ToDecimal(gvAccruedOverNAbove.Rows[i].Cells[13].Text);
                        Issue_Weight = Convert.ToDecimal(gvAccruedOverNAbove.Rows[i].Cells[14].Text);
                        Closing_Weight = Convert.ToDecimal(gvAccruedOverNAbove.Rows[i].Cells[15].Text);

                        Storage_Period = gvAccruedOverNAbove.Rows[i].Cells[7].Text.ToString();
                        Accrued_Period = gvAccruedOverNAbove.Rows[i].Cells[8].Text.ToString();
                        if (Storage_Period == "&nbsp;")
                        {
                            Storage_Period = "";
                        }
                        if (Accrued_Period == "&nbsp;")
                        {
                            Accrued_Period = "";
                        }
                        else if (Accrued_Period == "15 Days")
                        {
                            Accrued_Period = "H";
                        }
                        else if (Accrued_Period == "01 Month")
                        {
                            Accrued_Period = "M";
                        }
                        //string qry = "INSERT INTO tbl_OVER_N_ABOVE_Accrued_Storage_Charges(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Client_IP,Reserved_Bag,Over_Bag,Godown_Id,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight,Accrued_Period,Monthly_Rate) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate + "','" + Total_Charges + "',getdate(),'','" + ip + "','" + Reserved_Bag + "','" + Over_Bag + "','" + Godown_Id + "','" + Opening_Weight + "','" + Rec_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "','" + Accrued_Period + "','" + Monthly_Rate + "')";
                        string qry = "INSERT INTO tbl_OVER_N_ABOVE_Accrued_Storage_Charges(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Reserved_Bag,Over_Bag,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight,Accrued_Period,Monthly_Rate) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate + "','" + Total_Charges + "',getdate(),'','" + Reserved_Bag + "','" + Over_Bag + "','" + Opening_Weight + "','" + Rec_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "','" + Accrued_Period + "','" + Monthly_Rate + "')";
                        SqlCommand cmd = new SqlCommand(qry, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ///////////////////Insert Godown detail/////////////////////
                        Get_Bill_Type();
                        string Godown_Id = "";
                        Godown_Id = gvAccruedOverNAbove.Rows[i].Cells[11].Text.ToString();
                        string SG_ID = "";
                        string HGodownId = "";
                        if (Godown_Id != "&nbsp;")
                        {
                            Godown_Id = gvAccruedOverNAbove.Rows[i].Cells[11].Text.ToString();
                            if (Godown_Id.Contains(","))
                            {
                                HGodownId = Godown_Id;
                                string[] G_Id = HGodownId.Split(',');
                                G_Id = G_Id.Distinct().ToArray();
                                for (int g = 0; g < G_Id.Length; g++)
                                {
                                    SG_ID = G_Id[g];

                                    if (SG_ID != "")
                                    {
                                        string qryg = "INSERT INTO tbl_Storage_Bills_Godown_Details(Bill_Number,Oper_Date,Godown_Id,Bill_Type) values('" + ViewState["BillNo"] + "','" + Dates + "','" + SG_ID + "','" + Bill_Type + "')";
                                        SqlCommand cmdg = new SqlCommand(qryg, con);
                                        con.Open();
                                        cmdg.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                            else
                            {
                                string qryg = "INSERT INTO tbl_Storage_Bills_Godown_Details(Bill_Number,Oper_Date,Godown_Id,Bill_Type) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Godown_Id + "','" + Bill_Type + "')";
                                SqlCommand cmdg = new SqlCommand(qryg, con);
                                con.Open();
                                cmdg.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        else
                        {
                            Godown_Id = "";
                        }
                        ///////////////////Insert Godown detail/////////////////////
                    }
                }
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
    public void Over_N_Above_Bill_Daily_Detail()
    {
        try
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string Dates = "";
            //DateTime Dates = new DateTime();
            decimal Opening_Balance = 0;
            decimal Rec_Bags = 0;
            decimal Issue_Bags = 0;
            decimal Closing_Balance = 0;
            decimal Per_Day_Rate = 0;
            decimal Total_Charges = 0;
            decimal Reserved_Bag = 0;
            decimal Over_Bag = 0;

            //string Godown_Id = "";
            decimal Opening_Weight = 0;
            decimal Rec_Weight = 0;
            decimal Issue_Weight = 0;
            decimal Closing_Weight = 0;

            if (gvOverAboveDaily.Rows.Count > 0)
            {
                for (int i = 0; i <= gvOverAboveDaily.Rows.Count - 1; i++)
                {
                    Dates = getDate_MDY(gvOverAboveDaily.Rows[i].Cells[0].Text.ToString());
                    Opening_Balance = Convert.ToDecimal(gvOverAboveDaily.Rows[i].Cells[1].Text);
                    Rec_Bags = Convert.ToDecimal(gvOverAboveDaily.Rows[i].Cells[2].Text);
                    Issue_Bags = Convert.ToDecimal(gvOverAboveDaily.Rows[i].Cells[3].Text);
                    Closing_Balance = Convert.ToDecimal(gvOverAboveDaily.Rows[i].Cells[4].Text);
                    Reserved_Bag = Convert.ToDecimal(gvOverAboveDaily.Rows[i].Cells[5].Text);
                    Over_Bag = Convert.ToDecimal(gvOverAboveDaily.Rows[i].Cells[6].Text);
                    Per_Day_Rate = Convert.ToDecimal(gvOverAboveDaily.Rows[i].Cells[7].Text);
                    Total_Charges = Convert.ToDecimal(gvOverAboveDaily.Rows[i].Cells[8].Text);
                    
                    Opening_Weight = Convert.ToDecimal(gvOverAboveDaily.Rows[i].Cells[10].Text);
                    Rec_Weight = Convert.ToDecimal(gvOverAboveDaily.Rows[i].Cells[11].Text);
                    Issue_Weight = Convert.ToDecimal(gvOverAboveDaily.Rows[i].Cells[12].Text);
                    Closing_Weight = Convert.ToDecimal(gvOverAboveDaily.Rows[i].Cells[13].Text);
                    //string qry = "INSERT INTO tbl_OVER_N_ABOVE_Storage_Daily_Charges(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Client_IP,Reserved_Bag,Over_Bag,Godown_Id,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate + "','" + Total_Charges + "',getdate(),'','" + ip + "','" + Reserved_Bag + "','" + Over_Bag + "','" + Godown_Id + "','" + Opening_Weight + "','" + Rec_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "')";
                    string qry = "INSERT INTO tbl_OVER_N_ABOVE_Storage_Daily_Charges(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Reserved_Bag,Over_Bag,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate + "','" + Total_Charges + "',getdate(),'','" + Reserved_Bag + "','" + Over_Bag + "','" + Opening_Weight + "','" + Rec_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "')";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ///////////////////Insert Godown detail/////////////////////
                    Get_Bill_Type();
                    string Godown_Id = "";
                    Godown_Id = gvOverAboveDaily.Rows[i].Cells[9].Text.ToString();
                    string SG_ID = "";
                    string HGodownId = "";
                    if (Godown_Id != "&nbsp;")
                    {
                        Godown_Id = gvOverAboveDaily.Rows[i].Cells[9].Text.ToString();
                        if (Godown_Id.Contains(","))
                        {
                            HGodownId = Godown_Id;
                            string[] G_Id = HGodownId.Split(',');
                            G_Id = G_Id.Distinct().ToArray();
                            for (int g = 0; g < G_Id.Length; g++)
                            {
                                SG_ID = G_Id[g];

                                if (SG_ID != "")
                                {
                                    string qryg = "INSERT INTO tbl_Storage_Bills_Godown_Details(Bill_Number,Oper_Date,Godown_Id,Bill_Type) values('" + ViewState["BillNo"] + "','" + Dates + "','" + SG_ID + "','" + Bill_Type + "')";
                                    SqlCommand cmdg = new SqlCommand(qryg, con);
                                    con.Open();
                                    cmdg.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                        else
                        {
                            string qryg = "INSERT INTO tbl_Storage_Bills_Godown_Details(Bill_Number,Oper_Date,Godown_Id,Bill_Type) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Godown_Id + "','" + Bill_Type + "')";
                            SqlCommand cmdg = new SqlCommand(qryg, con);
                            con.Open();
                            cmdg.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    else
                    {
                        Godown_Id = "";
                    }
                    ///////////////////Insert Godown detail/////////////////////
                }
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
    public void Insert_Bill_Daily_Detail()
    {
        try
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string Dates = "";
            //DateTime Dates = new DateTime();
            decimal Opening_Balance = 0;
            decimal Rec_Bags = 0;
            decimal Issue_Bags = 0;
            decimal Closing_Balance = 0;
            decimal Per_Day_Rate=0;
            decimal Total_Charges=0;

            //string Godown_Id = "";
            decimal Opening_Weight = 0;
            decimal Rec_Weight = 0;
            decimal Issue_Weight = 0;
            decimal Closing_Weight = 0;

            decimal Per_Day_Rate_Weight = 0;
            decimal Charges_Weight = 0;

            if (gvIStorageCharge.Rows.Count > 0)
            {
                for (int i = 0; i <= gvIStorageCharge.Rows.Count-1; i++)
                {
                    Dates = getDate_MDY(gvIStorageCharge.Rows[i].Cells[0].Text.ToString());
                    Opening_Balance = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[1].Text);
                    Rec_Bags = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[2].Text);
                    Issue_Bags = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[3].Text);
                    Closing_Balance = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[4].Text);
                    Per_Day_Rate = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[5].Text);
                    Total_Charges = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[6].Text);
                    Opening_Weight = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[8].Text);
                    Rec_Weight=Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[9].Text);
                    Issue_Weight=Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[10].Text);
                    Closing_Weight = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[11].Text);

                    Per_Day_Rate_Weight = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[12].Text);
                    Charges_Weight = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[13].Text);
                    //string qry = "INSERT INTO tbl_Bills_Daily_Storage_Charges_Details(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate + "','" + Total_Charges + "',getdate(),'','" + Opening_Weight + "','" + Rec_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "')";
                    string qry = "";
                    if (ddlBType.SelectedValue.ToString() == "B" && ddlBType.SelectedValue.ToString() != "-1")
                    {
                        qry = "INSERT INTO tbl_Bills_Daily_Storage_Charges_Details(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate + "','" + Total_Charges + "',getdate(),'','" + Opening_Weight + "','" + Rec_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "')";
                    }
                    else if (ddlBType.SelectedValue.ToString() == "W" && ddlBType.SelectedValue.ToString() != "-1")
                    {
                        qry = "INSERT INTO tbl_Bills_Daily_Storage_Charges_Details(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate_Weight + "','" + Charges_Weight + "',getdate(),'','" + Opening_Weight + "','" + Rec_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "')";
                    }
                    SqlCommand cmd = new SqlCommand(qry, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ///////////////////Insert Godown detail/////////////////////
                    Get_Bill_Type();
                    string Godown_Id = gvIStorageCharge.Rows[i].Cells[7].Text.ToString();
                    string SG_ID = "";
                    string HGodownId = "";
                    if (Godown_Id != "&nbsp;")
                    {
                        Godown_Id = gvIStorageCharge.Rows[i].Cells[7].Text.ToString();
                        if (Godown_Id.Contains(","))
                        {
                            HGodownId = Godown_Id;
                            string[] G_Id = HGodownId.Split(',');
                            G_Id = G_Id.Distinct().ToArray();
                            for (int g = 0; g < G_Id.Length; g++)
                            {
                                SG_ID = G_Id[g];

                                if (SG_ID != "")
                                {
                                    string qryg = "INSERT INTO tbl_Storage_Bills_Godown_Details(Bill_Number,Oper_Date,Godown_Id,Bill_Type) values('" + ViewState["BillNo"] + "','" + Dates + "','" + SG_ID + "','" + Bill_Type + "')";
                                    SqlCommand cmdg = new SqlCommand(qryg, con);
                                    con.Open();
                                    cmdg.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                        else
                        {
                            string qryg = "INSERT INTO tbl_Storage_Bills_Godown_Details(Bill_Number,Oper_Date,Godown_Id,Bill_Type) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Godown_Id + "','" + Bill_Type + "')";
                            SqlCommand cmdg = new SqlCommand(qryg, con);
                            con.Open();
                            cmdg.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    else
                    {
                        Godown_Id = "";
                    }
                    ///////////////////Insert Godown detail/////////////////////
                }
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
    public void Insert_Accrued_Bill_WHR_Detail()
    {
        try
        {
            Bill_No = ViewState["BillNo"].ToString();
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string Deposit_Date = "";
            string Deliver_Date = "";
            //string Commodity_Name = "";
            string Commodity_Id = "";
            string WHR_No = "";
            decimal Deposit_Bags = 0;
            decimal Deliver_Bags = 0;
            //string Storage_Period = "";
            //string Accrued_Period = "";
            decimal Monthly_Rate = 0;
            decimal Per_Day_Rate = 0;
            //Per_Day_Rate = Convert.ToDecimal(txtCPRate.Text);
            decimal Total_Charges = 0;
            decimal Rebate_Bags_Charges = 0;
            decimal STax_Per = 0;
            decimal STax_Amt = 0;
            decimal Rebate_Per = 0;
            decimal Rebate_Amt = 0;
            decimal Net_Amounts = 0;
            decimal Rebate_Bags = 0;
            //string Godown_Id = "";
            decimal Deposit_Weight = 0;
            decimal Deliver_Weight = 0;

            int SP_In_MM=0;
            int SP_In_DD = 0;
            int AP_In_MM = 0;
            int AP_In_DD = 0;
            
            if (gvAccruedBill.Rows.Count > 0)
            {
                for (int i = 0; i <= gvAccruedBill.Rows.Count - 1; i++)
                {
                    Commodity_Id = gvAccruedBill.Rows[i].Cells[12].Text.ToString();
                    WHR_No = gvAccruedBill.Rows[i].Cells[1].Text.ToString();
                    Deposit_Date = getDate_MDY(gvAccruedBill.Rows[i].Cells[2].Text.ToString());
                    Deposit_Bags = Convert.ToDecimal(gvAccruedBill.Rows[i].Cells[3].Text);
                    Deliver_Date = getDate_MDY(gvAccruedBill.Rows[i].Cells[4].Text.ToString());
                    Deliver_Bags = Convert.ToDecimal(gvAccruedBill.Rows[i].Cells[5].Text);
                    //Storage_Period = gvAccruedBill.Rows[i].Cells[6].Text.ToString();
                    //Accrued_Period = gvAccruedBill.Rows[i].Cells[7].Text.ToString();
                    //string Accrued_Period2=Accrued_Period.Substring(1,2);
                    SP_In_MM = Convert.ToInt32(gvAccruedBill.Rows[i].Cells[16].Text);
                    SP_In_DD = Convert.ToInt32(gvAccruedBill.Rows[i].Cells[17].Text);
                    AP_In_MM = Convert.ToInt32(gvAccruedBill.Rows[i].Cells[18].Text);
                    AP_In_DD = Convert.ToInt32(gvAccruedBill.Rows[i].Cells[19].Text);
                    Monthly_Rate = Convert.ToDecimal(((TextBox)gvAccruedBill.Rows[i].FindControl("txtMonthlyRate")).Text.ToString());
                    Rebate_Bags = Convert.ToDecimal(gvAccruedBill.Rows[i].Cells[8].Text);
                  
                    Deposit_Weight = Convert.ToDecimal(gvAccruedBill.Rows[i].Cells[14].Text);
                    Deliver_Weight = Convert.ToDecimal(gvAccruedBill.Rows[i].Cells[15].Text);
                    /////
                    Total_Charges = (((Deliver_Bags) * AP_In_MM * Monthly_Rate) + ((Deliver_Bags) * AP_In_DD) * (Monthly_Rate / 30));
                    Rebate_Bags_Charges = (((Rebate_Bags * AP_In_MM) * Monthly_Rate) + (Rebate_Bags * AP_In_DD) * (Monthly_Rate / 30));
                    Rebate_Per = Convert.ToDecimal(((TextBox)gvAccruedBill.Rows[i].FindControl("txtRebate")).Text.ToString());
                    Rebate_Amt = (Rebate_Bags_Charges * Rebate_Per) / 100;
                    STax_Per = Convert.ToDecimal(((TextBox)gvAccruedBill.Rows[i].FindControl("txtSTax")).Text.ToString());
                    STax_Amt = ((Total_Charges + Rebate_Bags_Charges) * STax_Per) / 100;
                    Net_Amounts = (Total_Charges) + STax_Amt - Rebate_Amt;

                    //string qry = "INSERT INTO tbl_Accrued_Storage_Charges_WHR_Details(Bill_Number,Commodity_Id,WHR_No,Deposit_Date,Deposit_Bags,Delivery_Date,Deliver_Bags,Storage_Period,Accrued_Period,Monthly_Rate,Per_Day_Rate,Total_Charges,Rebate_Per,Rebate_Amt,STax_Per,STax_Amt,Net_Amount,Created_Date,Client_IP,Rebate_Bags,Godown_Id,Deposit_Weight,Deliver_Weight) values('" + Bill_No + "','" + Commodity_Id + "','" + WHR_No + "','" + Deposit_Date + "','" + Deposit_Bags + "','" + Deliver_Date + "','" + Deliver_Bags + "','" + Storage_Period + "','" + Accrued_Period + "','" + Monthly_Rate + "','" + Per_Day_Rate + "','" + Total_Charges + "','" + Rebate_Per + "','" + Rebate_Amt + "','" + STax_Per + "','" + STax_Amt + "','" + Net_Amounts + "',getdate(),'" + ip + "','" + Rebate_Bags + "','" + Godown_Id + "','" + Deposit_Weight + "','" + Deliver_Weight + "')";
                    //string qry = "INSERT INTO tbl_Accrued_Storage_Charges_WHR_Details(Bill_Number,Commodity_Id,WHR_No,Deposit_Date,Deposit_Bags,Delivery_Date,Deliver_Bags,Monthly_Rate,Per_Day_Rate,Total_Charges,Rebate_Per,Rebate_Amt,STax_Per,STax_Amt,Net_Amount,Created_Date,Client_IP,Rebate_Bags,Godown_Id,Deposit_Weight,Deliver_Weight,SP_In_MM,SP_In_DD,AP_In_MM,AP_In_DD) values('" + Bill_No + "','" + Commodity_Id + "','" + WHR_No + "','" + Deposit_Date + "','" + Deposit_Bags + "','" + Deliver_Date + "','" + Deliver_Bags + "','" + Monthly_Rate + "','" + Per_Day_Rate + "','" + Total_Charges + "','" + Rebate_Per + "','" + Rebate_Amt + "','" + STax_Per + "','" + STax_Amt + "','" + Net_Amounts + "',getdate(),'" + ip + "','" + Rebate_Bags + "','" + Godown_Id + "','" + Deposit_Weight + "','" + Deliver_Weight + "','" + SP_In_MM + "','" + SP_In_DD + "','" + AP_In_MM + "','" + AP_In_DD + "')";
                    string qry = "INSERT INTO tbl_Accrued_Storage_Charges_WHR_Details(Bill_Number,Commodity_Id,WHR_No,Deposit_Date,Deposit_Bags,Delivery_Date,Deliver_Bags,Monthly_Rate,Per_Day_Rate,Total_Charges,Rebate_Per,Rebate_Amt,STax_Per,STax_Amt,Net_Amount,Created_Date,Rebate_Bags,Deposit_Weight,Deliver_Weight,SP_In_MM,SP_In_DD,AP_In_MM,AP_In_DD) values('" + Bill_No + "','" + Commodity_Id + "','" + WHR_No + "','" + Deposit_Date + "','" + Deposit_Bags + "','" + Deliver_Date + "','" + Deliver_Bags + "','" + Monthly_Rate + "','" + Per_Day_Rate + "','" + Total_Charges + "','" + Rebate_Per + "','" + Rebate_Amt + "','" + STax_Per + "','" + STax_Amt + "','" + Net_Amounts + "',getdate(),'" + Rebate_Bags + "','" + Deposit_Weight + "','" + Deliver_Weight + "','" + SP_In_MM + "','" + SP_In_DD + "','" + AP_In_MM + "','" + AP_In_DD + "')";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    NetAmount = NetAmount + Net_Amounts;
                    ///////////////////Insert Godown detail/////////////////////
                    Get_Bill_Type();
                    string Godown_Id = "";
                    Godown_Id = gvAccruedBill.Rows[i].Cells[13].Text.ToString();
                    string SG_ID = "";
                    string HGodownId = "";
                    if (Godown_Id != "&nbsp;")
                    {
                        Godown_Id = gvAccruedBill.Rows[i].Cells[13].Text.ToString();
                        if (Godown_Id.Contains(","))
                        {
                            HGodownId = Godown_Id;
                            string[] G_Id = HGodownId.Split(',');
                            G_Id = G_Id.Distinct().ToArray();
                            for (int g = 0; g < G_Id.Length; g++)
                            {
                                SG_ID = G_Id[g];

                                if (SG_ID != "")
                                {
                                    string qryg = "INSERT INTO tbl_Storage_Bills_Godown_Details(Bill_Number,Oper_Date,Godown_Id,Bill_Type) values('" + ViewState["BillNo"] + "','" + Deliver_Date + "','" + SG_ID + "','" + Bill_Type + "')";
                                    SqlCommand cmdg = new SqlCommand(qryg, con);
                                    con.Open();
                                    cmdg.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                        else
                        {
                            string qryg = "INSERT INTO tbl_Storage_Bills_Godown_Details(Bill_Number,Oper_Date,Godown_Id,Bill_Type) values('" + ViewState["BillNo"] + "','" + Deliver_Date + "','" + Godown_Id + "','" + Bill_Type + "')";
                            SqlCommand cmdg = new SqlCommand(qryg, con);
                            con.Open();
                            cmdg.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    else
                    {
                        Godown_Id = "";
                    }
                    ///////////////////Insert Godown detail/////////////////////
                }
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
    public static string Left(string param, int length)
    {
        string result = param.Substring(0, length);
        return result;
    }
    public static string Right(string param, int length)
    {
        string result = param.Substring(param.Length - length, length);
        return result;
    }
    public void Insert_Reservation_Bill_Detail()
    {
        try
        {
            string ReservationPeriod = lblmonth.Text;
            decimal Reserved_Capacity = Convert.ToDecimal(txtrunit.Text);
            decimal TotalAmt = Convert.ToDecimal(txtNetAmt.Text);
            Service_Tax = (TotalAmt * Convert.ToDecimal(txtstax.Text)) / 100;
            Discount = (TotalAmt * Convert.ToDecimal(txtdisc.Text)) / 100;
            NetAmount = TotalAmt + Service_Tax - Discount;
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
            string BranchID = Session["BranchID"].ToString();
            //string qry = "INSERT INTO tbl_Reseravation_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Created_Date,Modified_Date,Client_IP,Reserved_Capacity,Reservation_Period,Remark,Service_Tax,Discount,Service_Tax_Perc,Discount_Perc) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','" + ddldepositor.SelectedValue.ToString() + "','" + ddldepos_name.SelectedValue.ToString() + "','" + ddlverity.SelectedValue.ToString() + "','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(txtfdate.Text) + "','" + getDate_MDY(txttodate.Text) + "','" + ddlpacktype.SelectedValue.ToString() + "','" + ddlweight.SelectedValue.ToString() + "','" + ddlcropyr.SelectedItem.Text + "'," + txtcomrate.Text + "," + NetAmount + ",getdate(),'','" + ip + "'," + Reserved_Capacity + ",'" + ReservationPeriod + "','" + txtremark.Text + "','" + Service_Tax + "','" + Discount + "','" + txtstax.Text + "','" + txtdisc.Text + "')";
            string qry = "INSERT INTO tbl_Reseravation_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Created_Date,Modified_Date,Client_IP,Reserved_Capacity,Reservation_Period,Remark,Service_Tax,Discount,Service_Tax_Perc,Discount_Perc) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','" + ddldepositor.SelectedValue.ToString() + "','" + ddldepos_name.SelectedValue.ToString() + "','" + ddlverity.SelectedValue.ToString() + "','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(txtfdate.Text) + "','" + getDate_MDY(txttodate.Text) + "','" + ddlpacktype.SelectedValue.ToString() + "','" + ddlweight.SelectedValue.ToString() + "','" + ddlcropyr.SelectedItem.Text + "'," + txtcomrate.Text + "," + TotalAmt + ",getdate(),'','" + ip + "'," + Reserved_Capacity + ",'" + ReservationPeriod + "','" + txtremark.Text + "','" + Service_Tax + "','" + Discount + "','" + txtstax.Text + "','" + txtdisc.Text + "')";
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            if (NetAmount != 0)
            {
                BID = Convert.ToInt32(ViewState["BID"]);
                Get_Bill_Type();
                qry = "INSERT INTO tbl_Storage_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','','" + ddldepos_name.SelectedValue.ToString() + "','','','','','','','',0," + NetAmount + ",0,0,0,'',0,0,0,'','',getdate(),'','" + ip + "',0,'','','" + Bill_Type + "','" + BID + "')";
                SqlCommand cmd2 = new SqlCommand(qry, con);
                con.Open();
                cmd2.ExecuteNonQuery();
                con.Close();
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
    public void Insert_MCReservation_Bill_Detail()
    {
        if (gvReservation.Rows.Count > 0)
        {
            for (int i = 0; i < gvReservation.Rows.Count; i++)
            {
                    string Commodity_Type = gvReservation.Rows[i].Cells[1].Text.ToString();
                    string Commodity = gvReservation.Rows[i].Cells[2].Text.ToString();
                    string FromDate = getDate_MDY(gvReservation.Rows[i].Cells[3].Text.ToString()).ToString();
                    string ToDate = getDate_MDY(gvReservation.Rows[i].Cells[4].Text.ToString()).ToString();
                    string PackingType = gvReservation.Rows[i].Cells[5].Text.ToString();
                    string Weight = gvReservation.Rows[i].Cells[6].Text.ToString();
                    string FYear = gvReservation.Rows[i].Cells[7].Text.ToString();
                    string NofUnit = gvReservation.Rows[i].Cells[8].Text.ToString();
                    decimal CRate = Convert.ToDecimal(gvReservation.Rows[i].Cells[9].Text.ToString());
                    string RMonth = gvReservation.Rows[i].Cells[10].Text.ToString();         
                    decimal NetAmt = Convert.ToDecimal(gvReservation.Rows[i].Cells[11].Text.ToString());
                    string Remark = gvReservation.Rows[i].Cells[12].Text.ToString();
                    if(Remark=="&nbsp;")
                    {
                        Remark = "";
                    }
                    string ReservationPeriod = gvReservation.Rows[i].Cells[10].Text.ToString();
                    decimal Service_Tax_Perc = Convert.ToDecimal(gvReservation.Rows[i].Cells[14].Text);
                    decimal Discount_Perc = Convert.ToDecimal(gvReservation.Rows[i].Cells[13].Text.ToString());      
                    Service_Tax = (NetAmt * Service_Tax_Perc) / 100;
                    Discount = (NetAmt * Discount_Perc) / 100;
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
                    string BranchID = Session["BranchID"].ToString();       
                    string qry = "INSERT INTO tbl_Reseravation_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Created_Date,Modified_Date,Client_IP,Reserved_Capacity,Reservation_Period,Remark,Service_Tax,Discount,Service_Tax_Perc,Discount_Perc) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','" + ddldepositor.SelectedValue.ToString() + "','" + ddldepos_name.SelectedValue.ToString() + "','" + Commodity_Type + "','" + Commodity + "','" + FromDate + "','" + ToDate + "','" + PackingType + "','" + Weight + "','" + FYear + "'," + CRate + "," + NetAmt + ",getdate(),'','" + ip + "'," + NofUnit + ",'" + ReservationPeriod + "','" + Remark + "','" + Service_Tax + "','" + Discount + "','" + Service_Tax_Perc + "','" + Discount_Perc + "')";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();         
            }
        }
    }
    public void Report_Storage_Bill()
    {
        string BranchID = Session["BranchID"].ToString();
        trReportsView.Visible = true;
        ReportViewer_SC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string serverFullName = null;
            string path = Request.Url.ToString();
            int index = path.IndexOf(":") + 3;
            string path2 = path.Substring(index);
            int index2 = path2.IndexOf("/");
            int index3 = path2.IndexOf("/", index2 + 1);
            string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();

            if (index3 > 0)

                serverFullName = path2.Substring(0, index3);
            else
                serverFullName = servername;

            ReportViewer_SC.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            ReportViewer_SC.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            ReportViewer_SC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer_SC.ServerReport.ReportServerUrl = new Uri(reportURL);
            decimal Discounts = 0;
            if (ddlDeposCategory.SelectedItem.Text != "--Select--" && ddlronbags.SelectedItem.Text != "--Select--" && txtdiscount.Text != "" && txtkhasrano.Text != "")
            {
                Discounts = Convert.ToDecimal(txtdiscount.Text);
            }
            else
            {
                Discounts = 0;
            }

            ReportViewer_SC.ServerReport.ReportPath = folder + "/" + "rptAccruedStorageCharges";
            ReportViewer_SC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            ReportViewer_SC.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[14];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Branch_Id";
            reportParameterCollection[0].Values.Add(BranchID);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Rate";
            reportParameterCollection[1].Values.Add(txtcomrate.Text);
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "Commodity_ID";
            reportParameterCollection[2].Values.Add(ddlcomodity.SelectedValue.ToString());
            reportParameterCollection[3] = new ReportParameter();
            reportParameterCollection[3].Name = "Depositor_Name";
            reportParameterCollection[3].Values.Add(ddldepos_name.SelectedItem.Text);
            reportParameterCollection[4] = new ReportParameter();
            reportParameterCollection[4].Name = "From_Date";
            reportParameterCollection[4].Values.Add(getDate_MDY(txtfdate.Text));
            reportParameterCollection[5] = new ReportParameter();
            reportParameterCollection[5].Name = "To_Date";
            reportParameterCollection[5].Values.Add(getDate_MDY(txttodate.Text));
            reportParameterCollection[6] = new ReportParameter();
            reportParameterCollection[6].Name = "Crop_Year";
            reportParameterCollection[6].Values.Add(ddlcropyr.SelectedItem.Text);
            reportParameterCollection[7] = new ReportParameter();
            reportParameterCollection[7].Name = "Bill_No";
            reportParameterCollection[7].Values.Add(Bill_No);
            reportParameterCollection[8] = new ReportParameter();
            reportParameterCollection[8].Name = "Service_Tax";
            reportParameterCollection[8].Values.Add(txtstax.Text);
            reportParameterCollection[9] = new ReportParameter();
            reportParameterCollection[9].Name = "Discount";
            reportParameterCollection[9].Values.Add(Discounts.ToString());
            reportParameterCollection[10] = new ReportParameter();
            reportParameterCollection[10].Name = "ChargeOfTotal";
            reportParameterCollection[10].Values.Add(Math.Round(ChargeOfTotal, 2).ToString());
            reportParameterCollection[11] = new ReportParameter();
            reportParameterCollection[11].Name = "RebateAmount";
            reportParameterCollection[11].Values.Add(Math.Round(RebateAmount, 2).ToString());
            reportParameterCollection[12] = new ReportParameter();
            reportParameterCollection[12].Name = "NetAmountWord";
            reportParameterCollection[12].Values.Add(NetAmountWord);
            reportParameterCollection[13] = new ReportParameter();
            reportParameterCollection[13].Name = "PerDayRate";
            reportParameterCollection[13].Values.Add(txtCPRate.Text);

            ReportViewer_SC.ServerReport.SetParameters(reportParameterCollection);
            ReportViewer_SC.ServerReport.Refresh();

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    public void Accrued_Storage_Bill_Details()
    {
        string BranchID = Session["BranchID"].ToString();
        trReportsView.Visible = true;
        trRentBill.Visible = false;
        TrAccrued.Visible = false;
        ReportViewer_SC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string serverFullName = null;
            string path = Request.Url.ToString();
            int index = path.IndexOf(":") + 3;
            string path2 = path.Substring(index);
            int index2 = path2.IndexOf("/");
            int index3 = path2.IndexOf("/", index2 + 1);
            string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();

            if (index3 > 0)

                serverFullName = path2.Substring(0, index3);
            else
                serverFullName = servername;

            ReportViewer_SC.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            ReportViewer_SC.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            ReportViewer_SC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer_SC.ServerReport.ReportServerUrl = new Uri(reportURL);

            NetAmountWord = (Convert_To_Word(NetAmount)).ToString();
            Bill_No = ViewState["BillNo"].ToString();

            ReportViewer_SC.ServerReport.ReportPath = folder + "/" + "rptAccruedStorageCharges";
            ReportViewer_SC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            ReportViewer_SC.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[3];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Branch_Id";
            reportParameterCollection[0].Values.Add(BranchID);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Bill_No";
            reportParameterCollection[1].Values.Add(Bill_No);
           
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "NetAmountWord";
            reportParameterCollection[2].Values.Add(NetAmountWord);

            ReportViewer_SC.ServerReport.SetParameters(reportParameterCollection);
            ReportViewer_SC.ServerReport.Refresh();

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    public void Get_Reservation_NetAmt()
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string BranchID = Session["BranchID"].ToString();

        qry = "select (sum(Net_Amount)+SUM(Service_Tax)-SUM(Discount)) as NetTotal from tbl_Reseravation_Bill_Details where Bill_Number='" + Bill_No + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dts = new DataTable();
        da.Fill(dts);
        if (dts.Rows[0]["NetTotal"].ToString() != "0")
        {
            NetAmount = Convert.ToDecimal(dts.Rows[0]["NetTotal"]);
        }

        if (NetAmount != 0)
        {
            NetAmountWord = Convert_To_Word(NetAmount);   
        }
        else
        {
            NetAmountWord = "";
        }
        if (NetAmount != 0 && btnGVReserveBill.Visible==true)
        {
            BID = Convert.ToInt32(ViewState["BID"]);
            Get_Bill_Type();
            qry = "INSERT INTO tbl_Storage_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','','" + ddldepos_name.SelectedValue.ToString() + "','','','','','','','',0," + NetAmount + ",0,0,0,'',0,0,0,'','',getdate(),'','" + ip + "',0,'','','" + Bill_Type + "','" + BID + "')";
            SqlCommand cmd2 = new SqlCommand(qry, con);
            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();
        }
            
    }
    public void Report_MCReservation_Bill()
    {
        try
        {
            Bill_No = ViewState["BillNo"].ToString();
            Get_Reservation_NetAmt();
            trReportsView.Visible = true;
            ReportViewer_SC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;

            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string serverFullName = null;
            string path = Request.Url.ToString();
            int index = path.IndexOf(":") + 3;
            string path2 = path.Substring(index);
            int index2 = path2.IndexOf("/");
            int index3 = path2.IndexOf("/", index2 + 1);
            string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();

            if (index3 > 0)

                serverFullName = path2.Substring(0, index3);
            else
                serverFullName = servername;

            ReportViewer_SC.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            ReportViewer_SC.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            ReportViewer_SC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer_SC.ServerReport.ReportServerUrl = new Uri(reportURL);
            ReportViewer_SC.ServerReport.ReportPath = folder + "/" + "rptMCReservationBillCharges";
            ReportViewer_SC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            ReportViewer_SC.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[3];          
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "NetAmountWord";
            reportParameterCollection[0].Values.Add(NetAmountWord);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Bill_No";
            reportParameterCollection[1].Values.Add(Bill_No);
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "NetAmount";
            reportParameterCollection[2].Values.Add(NetAmount.ToString());
            ReportViewer_SC.ServerReport.SetParameters(reportParameterCollection);
            ReportViewer_SC.ServerReport.Refresh();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    public string GetBillStatus(string fdate,string tdate,string BillType)
    {
        //string CHKFlag = "N";
        string BranchID = Session["BranchID"].ToString();
        string BStatus="N";
        string Bills_Type=BillType;
        DateTime FDate=new DateTime();
        DateTime TDate=new DateTime();
        FDate = Convert.ToDateTime(getDate_MDY(fdate));
        TDate = Convert.ToDateTime(getDate_MDY(tdate));

        if (Bills_Type == "AD")
        {
//AD
//AU
//OD
//OU
//RB
            string qry = "select Bill_Number,Bill_Type,Commodity_Id,From_Date,To_Date,Created_Date from tbl_Storage_Bill_Details where Branch_Id='" + BranchID + "' and Depositor_Id='" + ddldepos_name.SelectedValue.ToString() + "' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and Bill_Type='"+ Bills_Type +"'";
            SqlCommand cmd = new SqlCommand(qry,con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count-1; i++)
                {
                    if (FDate <Convert.ToDateTime(dt.Rows[i]["From_Date"].ToString()))
                    {
                        if (TDate < Convert.ToDateTime(dt.Rows[i]["From_Date"].ToString()))
                        {
                        BStatus = "Y";
                        }
                    }
                    else if (FDate > Convert.ToDateTime(dt.Rows[i]["To_Date"].ToString()))
                    {
                        if (TDate > Convert.ToDateTime(dt.Rows[i]["To_Date"].ToString()))
                        {
                            BStatus = "Y";
                        }
                    }
                    else
                    {
                        i = dt.Rows.Count;
                        BStatus = "N";
                    }

                }
            }
            else
            {
                BStatus = "Y";
            }
        }
        return BStatus;
    }
    protected void btnGenerateBill_Click(object sender, EventArgs e)
    {
        string BranchID = Session["BranchID"].ToString();
        if (ddldepositor.SelectedItem.Text == "--Select--" || ddldepos_name.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Depositor...'); </script> ");
        }
        else if (ddlBillType.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Bill Type...'); </script> ");
        }
        else if (ddlCalcTy.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Calculation Type...'); </script> ");
        }
        else if (ddlBillType.SelectedValue.ToString()=="1" && ddlCalcTy.SelectedValue.ToString()=="1")
        {
              if (ddlverity.SelectedItem.Text == "--Select--")
              {
                  ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity Type...'); </script> ");
              }
              else if (ddlcomodity.SelectedItem.Text == "--Select--")
              {
                  ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity...'); </script> ");
              }
              else if (txtfdate.Text == "")
              {
                  ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select From Date...'); </script> ");
              }
              else if (txttodate.Text == "")
              {
                   ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select To Date...'); </script> ");
              }
              else if (ddlpacktype.SelectedItem.Text == "--Select--")
             {
                  ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Packing Type...'); </script> ");
             }
              else if (ddlweight.SelectedItem.Text == "--Select--")
              {
                  ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Weight...'); </script> ");
              }
              else if (txtCPRate.Text == "")
              {
                  ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Per Day Rate....'); </script> ");
              }
              else if (txtstax.Text == "")
              {
                  ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Service Tax Amount....'); </script> ");
              }
              else if (Convert.ToDateTime(getDate_MDY(txtfdate.Text)) > Convert.ToDateTime(getDate_MDY(txttodate.Text)))
              {
                  ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Date(To Date should be grater then From date)...'); </script> ");
              }
              else if (ddlBType.SelectedItem.Text == "--Select--")
              {
                  ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Bill Unit (In Bag/M.T.)'); </script> ");
              }
              else
              {
                  string CheckStatus="";
                  //CheckStatus = GetBillStatus(txtfdate.Text,txttodate.Text,"AD");
                  CheckStatus = "Y";
                  if (CheckStatus == "Y")
                  {
                      GetStorageBillNo();
                      GetStorageDailyChargesBillDetail();
                  }
                  else
                  {
                      ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Bill of This Date are Already Generated..'); </script> ");
                      trRentBill.Visible = false;
                      trReportsView.Visible = false;
                  }
              }
        }
        else if (ddlBillType.SelectedValue.ToString() == "1" && ddlCalcTy.SelectedValue.ToString() == "3")
        {
            if (txtdiscount.Text != "" && ddlDeposCategory.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Depositor Category...'); </script> ");
            }
            else if (txtdiscount.Text!="" && ddlronbags.SelectedItem.Text=="--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please select Rebate Bags...'); </script> ");
            }
            else if (txtdiscount.Text != "" && txtkhasrano.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Khasra No...'); </script> ");
            }
            else if (txtdiscount.Text != "" && txtrpn.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Rin Pustika No....'); </script> ");
            }
            else if (txtdiscount.Text != "" && txtCastCert.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Cast Certificate Ref. No..'); </script> ");
            }
            else if (txtstax.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Service Tax Amount....'); </script> ");
            }
            else if (Convert.ToDateTime(getDate_MDY(txtfdate.Text)) > Convert.ToDateTime(getDate_MDY(txttodate.Text)))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Date(To Date should be grater then From date)...'); </script> ");
            }
            else
            {
                //string CheckStatus = "";
                //CheckStatus = GetBillStatus(txtfdate.Text, txttodate.Text, "AD");
                //if (CheckStatus == "Y")
                //{
                    GetStorageBillNo();
                    GetAccruedBillDetail();
                //}
                //else
                //{
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Bill of This Date are Already Generated..'); </script> ");
                //}
            }
        }
        else if (ddlBillType.SelectedValue.ToString() == "2" && ddlCalcTy.SelectedValue.ToString() == "2")
        {
            if (ddlverity.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity Type...'); </script> ");
            }
            else if (ddlcomodity.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity...'); </script> ");
            }
            else if (txtfdate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select From Date...'); </script> ");
            }
            else if (txttodate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select To Date...'); </script> ");
            }
            else if (ddlpacktype.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Packing Type...'); </script> ");
            }
            else if (txtrunit.Text=="")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter No of Bags to Reserve...'); </script> ");
            }
            else if (txtcomrate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Commodity Rate...'); </script> ");
            }
            else if (txtstax.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Service Tax %...'); </script> ");
            }
            else if (txtdisc.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Rebate %...'); </script> ");
            }
            else if (txtNetAmt.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Total Amount Required....!')", true);
            }
            else if (Convert.ToDateTime(getDate_MDY(txtfdate.Text)) > Convert.ToDateTime(getDate_MDY(txttodate.Text)))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Date(To Date should be grater then From date)...'); </script> ");
            }
            else
            {
                GetStorageBillNo(); 
                Insert_Reservation_Bill_Detail();
                Report_MCReservation_Bill();
 
            }
        }
        else if (ddlBillType.SelectedValue.ToString() == "3" && ddlCalcTy.SelectedValue.ToString() == "1")
        {
            if (ddlverity.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity Type...'); </script> ");
            }
            else if (ddlcomodity.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity...'); </script> ");
            }
            else if (txtfdate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select From Date...'); </script> ");
            }
            else if (txttodate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select To Date...'); </script> ");
            }
            else if (ddlpacktype.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Packing Type...'); </script> ");
            }
            else if (ddlweight.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Weight...'); </script> ");
            }
            else if (txtCPRate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Per Day Rate....'); </script> ");
            }
            else if (txtdiscount.Text != "" && ddlDeposCategory.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Depositor Category...'); </script> ");
            }
            else if (txtdiscount.Text != "" && ddlronbags.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please select Rebate Bags...'); </script> ");
            }
            else if (txtdiscount.Text != "" && txtkhasrano.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Khasra No...'); </script> ");
            }
            else if (txtdiscount.Text != "" && txtrpn.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Rin Pustika No....'); </script> ");
            }
            else if (txtdiscount.Text != "" && txtCastCert.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Cast Certificate Ref. No..'); </script> ");
            }
            else if (txtstax.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Service Tax Amount....'); </script> ");
            }
            else if (Convert.ToDateTime(getDate_MDY(txtfdate.Text)) > Convert.ToDateTime(getDate_MDY(txttodate.Text)))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Date(To Date should be grater then From date)...'); </script> ");
            }
            else
            {
                GetStorageBillNo();
                GetDailyOverAboveBillDetail();
            }
        }
        else if (ddlBillType.SelectedValue.ToString() == "3" && ddlCalcTy.SelectedValue.ToString() == "3")
        {
            if (ddlverity.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity Type...'); </script> ");
            }
            else if (ddlcomodity.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity...'); </script> ");
            }
            else if (txtfdate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select From Date...'); </script> ");
            }
            else if (txttodate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select To Date...'); </script> ");
            }
            else if (ddlpacktype.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Packing Type...'); </script> ");
            }
            else if (ddlweight.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Weight...'); </script> ");
            }
            else if (txtCPRate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Per Day Rate....'); </script> ");
            }
            /////
            else if (txtdiscount.Text != "" && ddlDeposCategory.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Depositor Category...'); </script> ");
            }
            else if (txtdiscount.Text != "" && ddlronbags.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please select Rebate Bags...'); </script> ");
            }
            else if (txtdiscount.Text != "" && txtkhasrano.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Khasra No...'); </script> ");
            }
            else if (txtdiscount.Text != "" && txtrpn.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Rin Pustika No....'); </script> ");
            }
            else if (txtdiscount.Text != "" && txtCastCert.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Cast Certificate Ref. No..'); </script> ");
            }
            /////
            else if (txtstax.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Service Tax Amount....'); </script> ");
            }
            else if (Convert.ToDateTime(getDate_MDY(txtfdate.Text)) > Convert.ToDateTime(getDate_MDY(txttodate.Text)))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Date(To Date should be grater then From date)...'); </script> ");
            }
            else
            {
                GetStorageBillNo();
                GetAccruedOverAboveBillDetail();
            }
        }
    }

    [Serializable]

    public sealed class ReportServerNetworkCredentials : IReportServerCredentials
    {
        #region IReportServerCredentials Members
        public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName,
        out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;
            return false;
        }

        // Specifies the user to impersonate when connecting to a report server. 
        //A WindowsIdentity object representing the user to impersonate.
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }

        // Returns network credentials to be used for authentication with the report server. 
        //A NetworkCredentials object.
        public System.Net.ICredentials NetworkCredentials
        {
            get
            {
                //you can place below settings in configuration xml file
                //string userName = "Administrator";
                //string password = "nic123";
                //string domain_warehouseName="VALUED-RDPRSG34\\SQL2008";
                string userName = ConfigurationManager.ConnectionStrings["uname"].ProviderName;
                string password = ConfigurationManager.ConnectionStrings["psw"].ProviderName;
                string domain_warehouseName = ConfigurationManager.ConnectionStrings["domain"].ProviderName;
                return new System.Net.NetworkCredential(userName, password, domain_warehouseName);
            }
        }

        #endregion
    }
    public void GetStorageBillNo()
    {
        string BranchID = Session["BranchID"].ToString();
        //qry = "select max(Bill_Number) as Bill_Number from tbl_Storage_Bill_Details where branch_Id='" + BranchID + "' and Depositor_Id='" + ddldepos_name.SelectedValue.ToString() + "'";
        qry = "select max(BId) as BId from tbl_Storage_Bill_Details where branch_Id='" + BranchID + "' and Depositor_Id='" + ddldepos_name.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        //string Bill_No = "";

        if (dt.Rows.Count != 0 && dt.Rows[0]["BId"].ToString() != null && dt.Rows[0]["BId"].ToString() != "")
        {
            if (dt.Rows[0]["BId"].ToString() != "" || Convert.ToInt32(dt.Rows[0]["BId"]) != 0)
            {
                BID = Convert.ToInt32(dt.Rows[0]["BId"]);
                int SubBN = BID + 1;
                Bill_No = BranchID + "" + ddldepos_name.SelectedValue.ToString() + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + SubBN.ToString();
                BID = SubBN;
            }
            else
            {
                Bill_No = BranchID + "" + ddldepos_name.SelectedValue.ToString() + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
                BID = 1;
            }
        }
        else
        {
            Bill_No = BranchID + "" + ddldepos_name.SelectedValue.ToString() + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
            BID = 1;
        }
        ViewState["BillNo"] = Bill_No;
        ViewState["BID"] = BID;
    }
    
    public void GetRebateDiscount()
    {
        try
        {
                string BranchID = Session["BranchID"].ToString();
                if (ddlBillType.SelectedValue.ToString() == "1" && ddlCalcTy.SelectedValue.ToString() == "3")
                {                  
                }
                else if (ddlBillType.SelectedValue.ToString()=="1" && ddlCalcTy.SelectedValue.ToString() == "1")
                {
                    if (ddlBType.SelectedValue.ToString() == "B" && ddlBType.SelectedValue.ToString() != "-1")
                    {
                        if (gvIStorageCharge.Rows.Count > 0)
                        {
                            for (int i = 0; i <= gvIStorageCharge.Rows.Count - 1; i++)
                            {
                                ChargeOfTotal = ChargeOfTotal + Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[6].Text);
                            }
                        }
                        if (txtdiscount.Text != "")
                        {
                            RebateAmount = (ChargeOfTotal * Convert.ToDecimal(txtdiscount.Text)) / 100;
                        }
                        else
                        {
                            RebateAmount = 0;
                        }
                    }
                    else if (ddlBType.SelectedValue.ToString() == "W" && ddlBType.SelectedValue.ToString() != "-1")
                    {
                        if (gvIStorageCharge.Rows.Count > 0)
                        {
                            for (int i = 0; i <= gvIStorageCharge.Rows.Count - 1; i++)
                            {
                                ChargeOfTotalWeight = ChargeOfTotalWeight + Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[13].Text);
                            }
                        }
                        if (txtdiscount.Text != "")
                        {
                            RebateAmount = (ChargeOfTotal * Convert.ToDecimal(txtdiscount.Text)) / 100;
                        }
                        else
                        {
                            RebateAmount = 0;
                        }
                    }
                }
                else if (ddlBillType.SelectedValue.ToString() == "3" && ddlCalcTy.SelectedValue.ToString() == "1")
                {
                    if (gvOverAboveDaily.Rows.Count > 0)
                    {
                        for (int i = 0; i <= gvOverAboveDaily.Rows.Count - 1; i++)
                        {
                            ChargeOfTotal = ChargeOfTotal + Convert.ToDecimal(gvOverAboveDaily.Rows[i].Cells[8].Text);
                        }
                    }
                    if (txtdiscount.Text != "")
                    {
                        RebateAmount = (ChargeOfTotal * Convert.ToDecimal(txtdiscount.Text)) / 100;
                    }
                    else
                    {
                        RebateAmount = 0;
                    }
                }
                else if (ddlBillType.SelectedValue.ToString() == "3" && ddlCalcTy.SelectedValue.ToString() == "3")
                {
                    if (gvAccruedOverNAbove.Rows.Count > 0)
                    {
                        for (int i = 0; i <= gvAccruedOverNAbove.Rows.Count - 1; i++)
                        {
                            if (gvAccruedOverNAbove.Rows[i].Cells[10].Text != "&nbsp;")
                            {
                            ChargeOfTotal = ChargeOfTotal + Convert.ToDecimal(gvAccruedOverNAbove.Rows[i].Cells[10].Text);
                            }
                        }
                    }
                    if (txtdiscount.Text != "")
                    {
                        RebateAmount = (ChargeOfTotal * Convert.ToDecimal(txtdiscount.Text)) / 100;
                    }
                    else
                    {
                        RebateAmount = 0;
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No Record found for this Period/Commodity...'); </script> ");
                }
                if (ddlBType.SelectedValue.ToString() == "B" && ddlBType.SelectedValue.ToString() != "-1")
                {
                    decimal SerTax = (ChargeOfTotal * Convert.ToDecimal(txtstax.Text)) / 100;
                    NetAmount = Math.Round(((ChargeOfTotal + SerTax) - RebateAmount), 2);
                    NetAmountWord = Convert_To_Word(NetAmount);
                }
                else if (ddlBType.SelectedValue.ToString() == "W" && ddlBType.SelectedValue.ToString() != "-1")
                {
                    decimal SerTax = (ChargeOfTotalWeight * Convert.ToDecimal(txtstax.Text)) / 100;
                    NetAmount = Math.Round(((ChargeOfTotalWeight + SerTax) - RebateAmount), 2);
                    NetAmountWord = Convert_To_Word(NetAmount);
                }

                   
            }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    public string Convert_To_Word(decimal Number)
    {
        string Word = "";
        if (Number != 0)
        {
            string NTWqry = "select dbo.AnkNumberToWords('" + Number + "')";    // Convert amount in word
            SqlCommand cmd2 = new SqlCommand(NTWqry, con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da2.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                Word = dt.Rows[0]["Column1"].ToString();
            }
        }
        else
        {
            Word = "Zero Rupees.";
        }
        return Word;
    }
    protected void ddlDeposCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeposCategory.SelectedItem.Text == "GEN/OBC")
        {
            txtdiscount.Text = "30";
        }
        else if (ddlDeposCategory.SelectedItem.Text == "SC" || ddlDeposCategory.SelectedItem.Text == "ST")
        {
            txtdiscount.Text = "40";
        }
        else
        {
            txtdiscount.Text = "0";
        }
    }
    protected void ddlCalcTy_SelectedIndexChanged(object sender, EventArgs e)
    {
        trRentBill.Visible = false;
        trReportsView.Visible = false;
        trReservationBill.Visible = false;
        trOverAboveDaily.Visible = false;
        if (ddlCalcTy.SelectedValue.ToString() == "3")
        {
            Get_Tax();
        }
        if (ddlBillType.SelectedValue.ToString() == "1" && ddlCalcTy.SelectedValue.ToString() == "3")
        {
            //lblrpn.Visible = true;
            //lblCastCert.Visible = true;
            //txtCastCert.Visible = true;
            //txtrpn.Visible = true;

            lblcc.Visible = false;
            ddlverity.Visible = false;
            lblcommodity.Visible = false;
            ddlcomodity.Visible = false;
            lblpacking.Visible = false;
            lblweight.Visible = false;
            ddlpacktype.Visible = false;
            ddlweight.Visible = false;

            lblcrate.Visible = false;
            lblcropyear.Visible = false;
            txtcomrate.Visible = false;
            txtCPRate.Visible = false;
            ddlcropyr.Visible = false;

            lblTodate.Visible = false;
            txttodate.Visible = false;
            lblFromDate.Text = "Delivery Date";
        }
        else if (ddlBillType.SelectedValue.ToString() == "2" && ddlCalcTy.SelectedValue.ToString() == "2")
        {
            ClearUptoStorage();
            lblcropyear.Text = "Financial Year";
        }
        else if (ddlBillType.SelectedValue.ToString() == "1" && ddlCalcTy.SelectedValue.ToString() == "1")
        {
            ClearUptoStorage();
            lblcropyear.Text = "Crop Year";
        }
        else if (ddlBillType.SelectedValue.ToString() == "1" && ddlCalcTy.SelectedValue.ToString() == "2")
        {
            ClearUptoStorage();
        }
    }
    public void ClearUptoStorage()
    {
        lblcc.Visible = true;
        ddlverity.Visible = true;
        lblcommodity.Visible = true;
        ddlcomodity.Visible = true;
        lblpacking.Visible = true;
        lblweight.Visible = true;
        ddlpacktype.Visible = true;
        ddlweight.Visible = true;

        lblcrate.Visible = true;
        lblcropyear.Visible = true;
        txtcomrate.Visible = true;
        txtCPRate.Visible = true;
        ddlcropyr.Visible = true;

        lblTodate.Visible = true;
        txttodate.Visible = true;
        lblFromDate.Text = "From Date ";
    }
    public void GetPeriod()
    {
        try
            {
                decimal rate = Convert.ToDecimal(txtcomrate.Text);
                decimal Perdayrate = Math.Round((rate / 30),4);
                decimal qty = Convert.ToDecimal(txtrunit.Text);
                decimal AmtPerMonth = rate * qty;
                decimal AmtPerday = Perdayrate * qty;
                string Period = lblmonth.Text;
                decimal Months = Convert.ToDecimal(Period.Substring(1,2));
                decimal days = Convert.ToDecimal(Period.Substring(10, 2));
                //decimal days = Convert.ToDecimal(lbldays.Text);
                txtNetAmt.Text = Math.Round(((AmtPerMonth * Months) + (AmtPerday * days)),2).ToString();
                NetAmount = Math.Round(((AmtPerMonth * Months) + (AmtPerday * days)), 2);
            }
            catch(Exception ex)
            {
                lblmsg.Text = ex.Message;
            }   
    }
    public void GetPeriods()
    {
        try
        {
            DateTime fdate = new DateTime();
            DateTime tdate = new DateTime();

            string sfdate = getDate_MDY(txtfdate.Text);

            //fdate = DateTime.Parse(sfdate);
           
            string stodate = getDate_MDY(txttodate.Text);

            tdate = DateTime.Parse(stodate);
            tdate = tdate.AddDays(1);
            string NewToDate = tdate.ToString("MM/dd/yyyy");
            //DateD(tdate, fdate);
            qry = "select dbo.udfDateDiffinYrMonDay('" + sfdate + "','" + NewToDate + "') as Period";
            SqlCommand cmd = new SqlCommand(qry,con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows[0]["Period"].ToString() != "")
            {
                lblmonth.Text = dt.Rows[0]["Period"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void btnAddmore_Click(object sender, EventArgs e)
    {
        try
        {
            bool checkstatus = false;

            if (ddldepositor.SelectedItem.Text == "--Select--" || ddldepos_name.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Depositor/Commodity...'); </script> ");
            }
            if (ddlverity.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity Type...'); </script> ");
            }
            else if (ddlcomodity.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity...'); </script> ");
            }
            else if (txtfdate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select From Date...'); </script> ");
            }
            else if (txttodate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select To Date...'); </script> ");
            }
            else if (ddlpacktype.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Packing Type...'); </script> ");
            }
            else if (Convert.ToDateTime(getDate_MDY(txtfdate.Text)) > Convert.ToDateTime(getDate_MDY(txttodate.Text)))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Date(To Date should be grater then From date)...'); </script> ");
            }
            else if (txtrunit.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter No of Bags to Reserve...'); </script> ");
            }
            else if (txtcomrate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Commodity Rate...'); </script> ");
            }
            else if (txtstax.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Service Tax %...'); </script> ");
            }
            else if (txtdisc.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Rebate %...'); </script> ");
            }
            else if (txtNetAmt.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Total Amount Required....!')", true);               
            }
            
            else
            {
                if (Session["dt1"] == null)
                {
                    Dt1 = CreateTable();
                    Session["dt1"] = Dt1;
                }
                // adding rows to the datatable
                DataRow dr = ((DataTable)Session["dt1"]).NewRow();
                ((DataTable)Session["dt1"]).AcceptChanges();
                int s = 1;
                dr["Id"] = s;
                dr["Commodity_Type"] = ddlverity.SelectedValue.ToString();
                dr["Commodity"] = ddlcomodity.SelectedValue.ToString();
                dr["Commodity_Name"] = ddlcomodity.SelectedItem.Text;
                dr["FromDate"] = txtfdate.Text;
                dr["ToDate"] = txttodate.Text;
                dr["PackingType"] = ddlpacktype.SelectedValue.ToString();
                dr["Weight"] = ddlweight.SelectedValue.ToString();
                dr["FYear"] = ddlcropyr.SelectedItem.Text;
                dr["NofUnit"] = txtrunit.Text;
                dr["CRate"] = txtcomrate.Text;
                dr["RMonth"] = lblmonth.Text;
                dr["Remark"] = txtremark.Text;
                dr["NetAmt"] = txtNetAmt.Text;
                dr["Discount"] = txtdisc.Text;
                dr["STax"] = txtstax.Text;
                if (gvReservation.Rows.Count > 0)
                {
                    if (checkstatus == false)
                    {
                        ((DataTable)Session["dt1"]).Rows.Add(dr);
                        ((DataTable)Session["dt1"]).AcceptChanges();
                        gvReservation.DataSource = (DataTable)Session["dt1"];
                        gvReservation.DataBind();
                        gvReservation.Visible = true;
                        gvReservation.HeaderRow.Cells[2].Visible = false;
                        gvReservation.HeaderRow.Cells[5].Visible = false;
                        gvReservation.HeaderRow.Cells[6].Visible = false;
                        for (int i = 0; i <= gvReservation.Rows.Count-1; i++)
                        {
                            gvReservation.Rows[i].Cells[2].Visible = false;
                            gvReservation.Rows[i].Cells[5].Visible = false;
                            gvReservation.Rows[i].Cells[6].Visible = false;
                        }
                        Clear_Reservation_Data();
                        btnGVReserveBill.Visible = true;
                        btnGenerateBill.Visible = false;

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Entry for this stack is already done')", true);
                    }
                }
                else
                {
                    ((DataTable)Session["dt1"]).Rows.Add(dr);
                    ((DataTable)Session["dt1"]).AcceptChanges();
                    gvReservation.DataSource = (DataTable)Session["dt1"];
                    gvReservation.DataBind();
                    Clear_Reservation_Data();
                    gvReservation.Visible = true;
                    gvReservation.HeaderRow.Cells[2].Visible = false;
                    gvReservation.HeaderRow.Cells[5].Visible = false;
                    gvReservation.HeaderRow.Cells[6].Visible = false;
                    for (int i = 0; i <= gvReservation.Rows.Count-1; i++)
                    {
                        gvReservation.Rows[i].Cells[2].Visible = false;
                        gvReservation.Rows[i].Cells[5].Visible = false;
                        gvReservation.Rows[i].Cells[6].Visible = false;
                    }
                    btnGVReserveBill.Visible = true;
                    btnGenerateBill.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = ex.Message;
        }
        finally
        {
            con.Close();          
        }
    }
    public void Clear_Reservation_Data()
    {
        Printcurrentdate();
        ddldepositor.Enabled = false;
        ddlcomodity.ClearSelection();
        ddlverity.ClearSelection();
        ddldepos_name.Enabled = false;
        ddlpacktype.ClearSelection();
        ddlweight.ClearSelection();
        ddlcropyr.ClearSelection();
        //ddlBillType.ClearSelection();
        //ddlCalcTy.ClearSelection();
        txtcomrate.Text = "";
        lblmonth.Visible = false;
        lblNetAmt.Visible = false;
        txtNetAmt.Visible = false;
        lblmonth.Visible = false;
        lblm1.Visible = false;
        lblremark.Visible = false;
        txtremark.Visible = false;
        btnAddmore.Visible = false;
        txtrunit.Visible = false;
        lblrunit.Visible = false;
        lblstax.Visible = true;
        txtstax.Visible = true;
        lblmonth.Text = "";  
        txtNetAmt.Text = "";
        lblmonth.Text = "";
        txtremark.Text = "";
        txtrunit.Text = "";
        txtstax.Text = "";

        ddlBillType.Enabled = false;
        ddlCalcTy.Enabled = false;
        //ddlcomodity.DataSource = null;
        //ddlcomodity.DataBind();
        //ddlcomodity.SelectedItem.Text = null;
        //ddlcomodity.SelectedValue = null;
        ddlcomodity.Items.Clear();
        ddlcomodity.ClearSelection();
    }
    public void Clear_Data()
    {
        Printcurrentdate();
        //ddldepositor.Enabled = false;
        ddlcomodity.ClearSelection();
        ddlverity.ClearSelection();
        ddldepos_name.ClearSelection();
        ddlpacktype.ClearSelection();
        ddlweight.ClearSelection();
        ddlcropyr.ClearSelection();
        ddlBillType.ClearSelection();
        ddlCalcTy.ClearSelection();
        txtcomrate.Text = "";
        lblmonth.Visible = false;
        lblNetAmt.Visible = false;
        txtNetAmt.Visible = false;
        lblmonth.Visible = false;
        lblm1.Visible = false;
        lblremark.Visible = false;
        txtremark.Visible = false;
        btnAddmore.Visible = false;
        txtrunit.Visible = false;
        lblrunit.Visible = false;
        lblstax.Visible = true;
        txtstax.Visible = true;
        lblmonth.Text = "";
        txtNetAmt.Text = "";
        lblmonth.Text = "";
        txtremark.Text = "";
        txtrunit.Text = "";
        txtstax.Text = "";
        txtCPRate.Text = "";
    }
    public void Clear_Rebate()
    {
        lblDepoCategory.Visible = false;
        lblkhasrano.Visible = false;
        lblrebates.Visible = false;
        lblronb.Visible = false;
        ddlDeposCategory.Visible = false;
        txtdiscount.Visible = false;
        ddlronbags.Visible = false;
        txtkhasrano.Visible = false;

        lblrpn.Visible = false;
        txtrpn.Visible = false;
        lblCastCert.Visible = false;
        txtCastCert.Visible = false;
    }
    public void Visible_Rebate()
    {
        lblDepoCategory.Visible = true;
        lblkhasrano.Visible = true;
        lblrebates.Visible = true;
        lblronb.Visible = true;
        ddlDeposCategory.Visible = true;
        txtdiscount.Visible = true;
        ddlronbags.Visible = true;
        txtkhasrano.Visible = true;

        lblrpn.Visible = true;
        txtrpn.Visible = true;
        lblCastCert.Visible = true;
        txtCastCert.Visible = true;
    }
    public DataTable CreateTable()
    {
        DataTable dt = new DataTable();//DataTable is created
        DataColumn Id = new DataColumn("Id", Type.GetType("System.String"));
        DataColumn Commodity_Type = new DataColumn("Commodity_Type", Type.GetType("System.String"));
        DataColumn Commodity = new DataColumn("Commodity", Type.GetType("System.String"));
        DataColumn Commodity_Name = new DataColumn("Commodity_Name", Type.GetType("System.String"));
        DataColumn FromDate = new DataColumn("FromDate", Type.GetType("System.String"));
        DataColumn ToDate = new DataColumn("ToDate", Type.GetType("System.String"));
        DataColumn PackingType = new DataColumn("PackingType", Type.GetType("System.String"));
        DataColumn Weight = new DataColumn("Weight", Type.GetType("System.String"));
        DataColumn FYear = new DataColumn("FYear", Type.GetType("System.String"));
        DataColumn NofUnit = new DataColumn("NofUnit", Type.GetType("System.String"));
        DataColumn CRate = new DataColumn("CRate", Type.GetType("System.String"));
        DataColumn RMonth = new DataColumn("RMonth", Type.GetType("System.String"));
        DataColumn Remark = new DataColumn("Remark", Type.GetType("System.String"));
        DataColumn NetAmt = new DataColumn("NetAmt", Type.GetType("System.String"));
        DataColumn Discount = new DataColumn("Discount", Type.GetType("System.String"));
        DataColumn STax = new DataColumn("STax", Type.GetType("System.String"));
        dt.Columns.Add(Id);//Column is added to the DataTable
        dt.Columns.Add(Commodity_Type);//Column is added to the DataTable
        dt.Columns.Add(Commodity);//Column is added to the DataTable
        dt.Columns.Add(Commodity_Name);
        dt.Columns.Add(FromDate);
        dt.Columns.Add(ToDate);
        dt.Columns.Add(PackingType);
        dt.Columns.Add(Weight);
        dt.Columns.Add(FYear);
        dt.Columns.Add(NofUnit);
        dt.Columns.Add(CRate);
        dt.Columns.Add(RMonth);
        dt.Columns.Add(Remark);
        dt.Columns.Add(NetAmt);
        dt.Columns.Add(Discount);
        dt.Columns.Add(STax);
        dt.AcceptChanges();
        return dt;
    }
    protected void BindGrid()
    {
        gvReservation.DataSource = ViewState["dt"] as DataTable;
        gvReservation.DataBind();
    }
    protected void gvReservation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int i = e.RowIndex;
            if (gvReservation.Rows.Count < 1)
            {
                ViewState["ckstat"] = "Delete";
            }
            ((DataTable)Session["dt1"]).Rows[i].Delete();
            ((DataTable)Session["dt1"]).AcceptChanges();

            gvReservation.DataSource = (DataTable)Session["dt1"];
            gvReservation.DataBind();
        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = ex.Message;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }

    protected void btnGVReserveBill_Click(object sender, EventArgs e)
    {     
        if (gvReservation.Rows.Count > 0)
        {
            GetStorageBillNo();
            Insert_MCReservation_Bill_Detail();
        }
        Report_MCReservation_Bill();
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
                txtfdate.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
                txttodate.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
    }
    protected void txttodate_TextChanged1(object sender, EventArgs e)
    {
        GetPeriods();
    }
    protected void txtfdate_TextChanged(object sender, EventArgs e)
    {
        GetPeriods();
    }
    protected void btnGenBill_Click(object sender, EventArgs e)
    {
        Insert_Bill_Daily_Detail();
        Insert_Bill_Detail();
        Report_Storage_Bill_Daily_Details();
    }
    public void GetAccruedBillDetail()
    {
        int RBags = 0;
        RBags = Convert.ToInt32(ddlronbags.SelectedValue.ToString());
        int j=0;
        int h = 0;
        DataTable dw = new DataTable();
        dw.Columns.AddRange(new DataColumn[]  { 
                new DataColumn("Opening_Bags",typeof(decimal),null),
              new DataColumn("Deposit_Bags",typeof(decimal),null),
                new DataColumn("Issue_Bags",typeof(decimal)),
                new DataColumn("Available_Bags",typeof(decimal)),
                new DataColumn("Depositor_WHR_Id",typeof(string)),
             new DataColumn("Delivery_Order_Date",typeof(DateTime)),
            });

        DataTable dtg = new DataTable();
        dtg.Columns.AddRange(new DataColumn[]  { 
            new DataColumn("Commodity_Id",typeof(string),null),
                new DataColumn("Commodity_Name",typeof(string),null),
              new DataColumn("Depositor_WHR_Id",typeof(string),null),
                new DataColumn("Date_of_Deposit",typeof(string)),
                new DataColumn("Delivery_Order_Date",typeof(string)),
                new DataColumn("Deposit_Bags",typeof(decimal)),
                new DataColumn("Deliver_Bags",typeof(decimal)),
                new DataColumn("PeriodOfStorage",typeof(string)),
            new DataColumn("ChargeOnPeriod",typeof(string)),
            new DataColumn("MonthlyRate",typeof(decimal)),
             new DataColumn("Rebate_Bags",typeof(decimal)),
             new DataColumn("Service_Tax",typeof(decimal)),
              new DataColumn("Rebate_Perc",typeof(decimal)),
              new DataColumn("Godown_Id",typeof(decimal)),
              new DataColumn("Deposit_Weight",typeof(decimal)),
              new DataColumn("Deliver_Weight",typeof(decimal)),
               new DataColumn("AP_In_Months",typeof(int)),
               new DataColumn("AP_In_Days",typeof(int)),
               new DataColumn("SP_In_Months",typeof(int)),
               new DataColumn("SP_In_Days",typeof(int)),
            });

        //string str = "SELECT Depositor_WHR_Id,whryear,Godown_Id, Date_of_Deposit AS Date_of_Deposit,a.Commodity_ID,(SELECT Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY AS c WHERE (c.Commodity_Id = a.Commodity_ID)) AS Commodity_Name, Deposit_Bags, Deposit_Weight,Deliver_Qty, Deliver_Bags,Delivery_Order_Date AS Delivery_Order_Date, PeriodOfStorage, ChargeOnPeriod,max(Rate) AS MonthlyRate, max(Rate)/30 AS PerDayRate,Deliver_Bags * max(Rate) * CAST(SUBSTRING(ChargeOnPeriod, 1, 2) AS int) + Deliver_Bags * max(Rate)/30 * CAST(SUBSTRING(ChargeOnPeriod, 10, 2) AS int) AS Charges,CASE WHEN CAST(datepart(MM,Date_of_Deposit) AS int) <= 3 THEN CAST((CAST(datepart(YYYY, Date_of_Deposit) AS int) - 1) AS varchar(10)) + '-' + substring(CAST(datepart(YYYY,Date_of_Deposit) AS varchar(10)), 3, 2) WHEN CAST(datepart(MM, Date_of_Deposit) AS int) >= 4 THEN CAST(datepart(YYYY, Date_of_Deposit) AS varchar(10)) + '-' + substring(CAST((CAST(datepart(YYYY, Date_of_Deposit) AS int) + 1) AS varchar(10)), 3, 2) ELSE '0' END AS Financial_Year FROM View_Storage_Period AS a inner join tbl_MetaDataEffectiveRateDetail as b on (b.Commodity_ID=a.Commodity_ID and Rate_Effective_Date<=Date_of_Deposit and Packing_Id='1' and Weight_ID='5') WHERE (Depositor_Name = '" + ddldepos_name.SelectedItem.Text + "') AND (a.Delivery_Order_Date ='" + getDate_MDY(txtfdate.Text) + "') AND (Deliver_Bags <> 0) GROUP BY Depositor_Name, Godown_ID, a.Commodity_ID, Deposit_Bags, Deposit_Weight, Deliver_Bags, Deliver_Qty, Date_of_Deposit, Delivery_Order_Date, PeriodOfStorage, Depositor_WHR_Id, whrDate, whryear, Godown_Name, Issue_Source_ID, ChargeOnPeriod ORDER BY Date_of_Deposit,Depositor_WHR_Id";
        string str = "SELECT Depositor_WHR_Id,whryear,Godown_Id, Date_of_Deposit AS Date_of_Deposit,a.Commodity_ID,(SELECT Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY AS c WHERE (c.Commodity_Id = a.Commodity_ID)) AS Commodity_Name, Deposit_Bags, Deposit_Weight,Deliver_Qty, Deliver_Bags,Delivery_Order_Date AS Delivery_Order_Date,SP_In_Months,SP_In_Days,(SP_In_Months+' Months '+SP_In_Days+' Days') as PeriodOfStorage,AP_In_MM_DD as ChargeOnPeriod,max(Rate) AS MonthlyRate, max(Rate)/30 AS PerDayRate,Deliver_Bags * max(Rate) * SP_In_Months + Deliver_Bags * max(Rate)/30 * SP_In_Days AS Charges,CASE WHEN CAST(datepart(MM,Date_of_Deposit) AS int) <= 3 THEN CAST((CAST(datepart(YYYY, Date_of_Deposit) AS int) - 1) AS varchar(10)) + '-' + substring(CAST(datepart(YYYY,Date_of_Deposit) AS varchar(10)), 3, 2) WHEN CAST(datepart(MM, Date_of_Deposit) AS int) >= 4 THEN CAST(datepart(YYYY, Date_of_Deposit) AS varchar(10)) + '-' + substring(CAST((CAST(datepart(YYYY, Date_of_Deposit) AS int) + 1) AS varchar(10)), 3, 2) ELSE '0' END AS Financial_Year FROM View_Storage_Period AS a inner join tbl_MetaDataEffectiveRateDetail as b on (b.Commodity_ID=a.Commodity_ID and Rate_Effective_Date<=Date_of_Deposit and Packing_Id='1' and Weight_ID='5') WHERE (Depositor_Name = '" + ddldepos_name.SelectedItem.Text + "') AND (a.Delivery_Order_Date ='" + getDate_MDY(txtfdate.Text) + "') AND (Deliver_Bags <> 0) GROUP BY Depositor_Name, Godown_ID, a.Commodity_ID, Deposit_Bags, Deposit_Weight, Deliver_Bags, Deliver_Qty, Date_of_Deposit, Delivery_Order_Date,Depositor_WHR_Id, whrDate, whryear, Godown_Name, Issue_Source_ID,AP_In_MM_DD,SP_In_Months,SP_In_Days ORDER BY Date_of_Deposit,Depositor_WHR_Id";
        SqlCommand cmd = new SqlCommand(str,con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            string Commodity_Id = "";
            string Commodity_Names = "";
            string WHR_NO = "";
            //string Deposit_Date = "";
            DateTime Deposit_Dates = new DateTime();
            decimal Deposit_Bags = 0;
            DateTime Deliver_Date = new DateTime();
            decimal Deliver_Bags = 0;
            string Storage_Period = "";
            string Accrued_Period = "";
            decimal Monthly_Rate = 0;
            string Financial_Year = "";
            string CalQry = "";
            decimal RebateBags = 0;
            //string CWhr_No = "";
            decimal CDeposit_Bags = 0;
            decimal CDeliver_Bags = 0;
            decimal TDeliver_Bags = 0;
            decimal CAvailable_Bags = 0;
            string Godown_Id = "";
            decimal Deposit_Weight = 0;
            decimal Deliever_Weight = 0;

            decimal Service_Tax = 0;
            decimal Discount = 0;
            int SP_In_Months = 0;
            int SP_In_Days = 0;

            int AP_In_Months = 0;
            int AP_In_Days = 0;

           
            if (txtstax.Text != "" && txtdiscount.Text != "")
            {
                Service_Tax = Convert.ToDecimal(txtstax.Text);
                Discount = Convert.ToDecimal(txtdiscount.Text);
            }
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                Commodity_Id = dt.Rows[i]["Commodity_Id"].ToString();
                Commodity_Names = dt.Rows[i]["Commodity_Name"].ToString();
                WHR_NO = dt.Rows[i]["Depositor_WHR_Id"].ToString();
                Deposit_Dates = Convert.ToDateTime(dt.Rows[i]["Date_of_Deposit"].ToString());
                Deposit_Bags = Convert.ToDecimal(dt.Rows[i]["Deposit_Bags"].ToString());
                Deliver_Date = Convert.ToDateTime(dt.Rows[i]["Delivery_Order_Date"].ToString());
                Deliver_Bags = Convert.ToDecimal(dt.Rows[i]["Deliver_Bags"].ToString());
                Storage_Period = dt.Rows[i]["PeriodOfStorage"].ToString();
                string Accrued_Periods = dt.Rows[i]["ChargeOnPeriod"].ToString();
                //Accrued_Period2 = dt.Rows[i]["ChargeOnPeriod"].ToString();////
                string[] words = Accrued_Periods.Split('.');
                string Accrued_Period_A = "";
                string Accrued_Period_B = "";
                string SF = "N";
                foreach (string word in words)
                {
                    if (SF != "Y")
                    {
                        Accrued_Period_A = word;
                        AP_In_Months = Convert.ToInt32(word);
                        SF = "Y";
                    }
                    else
                    {
                        Accrued_Period_B = word;
                        AP_In_Days = Convert.ToInt32(word);
                    }
                }
                Accrued_Period = Accrued_Period_A + " Months " + Accrued_Period_B + " Days";
                Monthly_Rate = Convert.ToDecimal(dt.Rows[i]["MonthlyRate"].ToString());
                Financial_Year = dt.Rows[i]["Financial_Year"].ToString();
                Godown_Id = dt.Rows[i]["Godown_Id"].ToString();
                Deposit_Weight = Convert.ToDecimal(dt.Rows[i]["Deposit_Weight"].ToString());
                Deliever_Weight = Convert.ToDecimal(dt.Rows[i]["Deliver_Qty"].ToString());
                SP_In_Months = Convert.ToInt32(dt.Rows[i]["SP_In_Months"]);
                SP_In_Days = Convert.ToInt32(dt.Rows[i]["SP_In_Days"]);

                //CalQry = "select Depositor_WHR_Id,Deposit_Bags,SUM(Deliver_Bags) as Issue_Bags,(Deposit_Bags-SUM(Deliver_Bags)) as Available_Bags from [View_Storage_Period] where Depositor_Name='"+ ddldepos_name.SelectedItem.Text +"' and Financial_Year='"+ Financial_Year +"' and Delivery_Order_Date<='"+ Deliver_Date +"' group by Depositor_WHR_Id,Deposit_Bags";
                CalQry = "select Delivery_Order_Date,Depositor_WHR_Id,Deposit_Bags,Deliver_Bags as Issue_Bags,(Deposit_Bags-Deliver_Bags) as Available_Bags from [View_Storage_Period] where Depositor_Name='" + ddldepos_name.SelectedItem.Text + "' and Financial_Year='" + Financial_Year + "' and Delivery_Order_Date<='" + Deliver_Date + "' order by Delivery_Order_Date";
                //CalQry = "select Depositor_WHR_Id,Deposit_Bags,Deliver_Bags as Issue_Bags,(Deposit_Bags-Deliver_Bags) as Available_Bags from [View_Storage_Period] where Depositor_Name='" + ddldepos_name.SelectedItem.Text + "' and Financial_Year='" + Financial_Year + "' and Delivery_Order_Date<='" + Deliver_Date + "' order by Depositor_WHR_Id";
                SqlCommand cmda = new SqlCommand(CalQry, con);
                SqlDataAdapter daa = new SqlDataAdapter(cmda);
                DataTable dta = new DataTable();
                daa.Fill(dta);
                if (dta.Rows.Count > 0)
                    if (RBags > 0)
                    {
                        {
                            {
                                dw.Clear();
                                int k = 0;
                                string F = "N";
                                if (dta.Rows.Count > 0)
                                {
                                    //decimal DB = 0;
                                    for (k = 0; k <= dta.Rows.Count - 1; k++)
                                    {
                                        dw.Rows.Add();
                                        if (k == 0)
                                        {
                                            dw.Rows[k]["Opening_Bags"] = 0;
                                        }
                                        else
                                        {
                                            dw.Rows[k]["Opening_Bags"] = dw.Rows[k - 1]["Available_Bags"];
                                        }
                                        if (k == 0)
                                        {
                                            dw.Rows[k]["Deposit_Bags"] = Convert.ToDecimal(dta.Rows[k]["Deposit_Bags"].ToString());
                                        }

                                        for (int v = 0; v <= dw.Rows.Count - 1; v++)
                                        {
                                            if (k == 0)
                                            {
                                                string ad = dw.Rows[v]["Depositor_WHR_Id"].ToString();
                                                if ((dw.Rows[v]["Depositor_WHR_Id"].ToString() == dta.Rows[k]["Depositor_WHR_Id"].ToString()))
                                                {
                                                    dw.Rows[k]["Deposit_Bags"] = 0;
                                                    //F = "Y";
                                                }
                                                else
                                                {
                                                    F = "Y";
                                                }
                                            }
                                            else if (dw.Rows[v]["Depositor_WHR_Id"].ToString() != "")
                                            {
                                                if ((dw.Rows[v]["Depositor_WHR_Id"].ToString() == dta.Rows[k]["Depositor_WHR_Id"].ToString()))
                                                {
                                                    dw.Rows[k]["Deposit_Bags"] = 0;
                                                    //F = "Y";
                                                }
                                                else
                                                {
                                                    F = "Y";
                                                }
                                            }

                                        }
                                        if (F != "Y")
                                        {
                                            dw.Rows[k]["Deposit_Bags"] = 0;
                                            F = "N";
                                        }
                                        else
                                        {
                                            dw.Rows[k]["Deposit_Bags"] = Convert.ToDecimal(dta.Rows[k]["Deposit_Bags"].ToString());
                                            F = "N";
                                        }
                                        dw.Rows[k]["Delivery_Order_Date"] = Convert.ToDateTime(dta.Rows[k]["Delivery_Order_Date"]);
                                        dw.Rows[k]["Depositor_WHR_Id"] = dta.Rows[k]["Depositor_WHR_Id"].ToString();
                                        dw.Rows[k]["Issue_Bags"] = Convert.ToDecimal(dta.Rows[k]["Issue_Bags"].ToString());
                                        dw.Rows[k]["Available_Bags"] = (Convert.ToDecimal(dw.Rows[k]["Opening_Bags"])) + (Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"])) - (Convert.ToDecimal(dw.Rows[k]["Issue_Bags"]));
                                    }
                                }
                            }
                        }
                        if (dw.Rows.Count > 0)
                        {
                            RebateBags = 0;
                            TDeliver_Bags = 0;
                            CDeposit_Bags = 0;
                            CDeliver_Bags = 0;
                            CAvailable_Bags = 0;
                            int s = 0;
                            for (s = 0; s <= dw.Rows.Count - 1; s++)
                            {
                                //string ss = dw.Rows[s]["Depositor_WHR_Id"].ToString();
                                if (Convert.ToDecimal(dw.Rows[s]["Issue_Bags"].ToString()) == Deliver_Bags && dw.Rows[s]["Depositor_WHR_Id"].ToString() == WHR_NO && dw.Rows[s]["Delivery_Order_Date"].ToString() == Deliver_Date.ToString())
                                {
                                    CDeposit_Bags = Convert.ToDecimal(dw.Rows[s]["Deposit_Bags"].ToString());
                                    CDeliver_Bags = Convert.ToDecimal(dw.Rows[s]["Issue_Bags"].ToString());
                                    CAvailable_Bags = Convert.ToDecimal(dw.Rows[s]["Opening_Bags"].ToString());
                                    s = dw.Rows.Count - 1;
                                }
                                else
                                {
                                    TDeliver_Bags = TDeliver_Bags + Convert.ToDecimal(dw.Rows[s]["Issue_Bags"].ToString());
                                }
                            }

                            if (TDeliver_Bags >= RBags)
                            {
                                RebateBags = 0;
                            }
                            else if (TDeliver_Bags == 0 && CDeliver_Bags <= RBags)
                            {
                                RebateBags = CDeliver_Bags;
                            }
                            else if (TDeliver_Bags < RBags && CDeliver_Bags <= (RBags - TDeliver_Bags))
                            {
                                RebateBags = CDeliver_Bags;
                            }
                            else if (TDeliver_Bags < RBags && CDeliver_Bags >= (RBags - TDeliver_Bags))
                            {
                                RebateBags = RBags - TDeliver_Bags;
                            }
                            else
                            {
                                RebateBags = 0;
                            }
                        }
                    }
                    else
                    {
                        RebateBags = 0;
                    }
                string Delivery_Date = "";
                Delivery_Date = Deliver_Date.ToString("dd/MM/yyyy");
                //if (Delivery_Date == DateTime.Now.ToString("dd/MM/yyyy"))
                if (Delivery_Date == txtfdate.Text)
                {

                    dtg.Rows.Add();

                    dtg.Rows[h]["Commodity_Id"] = Commodity_Id;
                    dtg.Rows[h]["Commodity_Name"] = Commodity_Names;
                    dtg.Rows[h]["Depositor_WHR_Id"] = WHR_NO;
                    dtg.Rows[h]["Date_of_Deposit"] = Deposit_Dates.ToString("dd/MM/yyyy");
                    dtg.Rows[h]["Delivery_Order_Date"] = Delivery_Date;
                    dtg.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(Deposit_Bags);
                    dtg.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(Deliver_Bags);
                    dtg.Rows[h]["PeriodOfStorage"] = Storage_Period;
                    dtg.Rows[h]["ChargeOnPeriod"] = Accrued_Period;
                    dtg.Rows[h]["MonthlyRate"] = Convert.ToDecimal(Monthly_Rate);
                    dtg.Rows[h]["Rebate_Bags"] = Convert.ToDecimal(RebateBags);
                    dtg.Rows[h]["Service_Tax"] = Convert.ToDecimal(Service_Tax);
                    dtg.Rows[h]["Rebate_Perc"] = Convert.ToDecimal(Discount);
                    dtg.Rows[h]["Godown_Id"] = Godown_Id.ToString();
                    dtg.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(Deposit_Weight);
                    dtg.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(Deliever_Weight);

                    dtg.Rows[h]["SP_In_Months"] = SP_In_Months;
                    dtg.Rows[h]["SP_In_Days"] = SP_In_Days;
                    dtg.Rows[h]["AP_In_Months"] = AP_In_Months;
                    dtg.Rows[h]["AP_In_Days"] = AP_In_Days;

                    h++;
                }
            }

            gvAccruedBill.DataSource = dtg;
            gvAccruedBill.DataBind();
            gvAccruedBill.HeaderRow.Cells[12].Visible = false;
            gvAccruedBill.HeaderRow.Cells[13].Visible = false;
            gvAccruedBill.HeaderRow.Cells[14].Visible = false;
            gvAccruedBill.HeaderRow.Cells[15].Visible = false;
            gvAccruedBill.HeaderRow.Cells[16].Visible = false;
            gvAccruedBill.HeaderRow.Cells[17].Visible = false;
            gvAccruedBill.HeaderRow.Cells[18].Visible = false;
            gvAccruedBill.HeaderRow.Cells[19].Visible = false;
            for (int i = 0; i <= gvAccruedBill.Rows.Count - 1; i++)
            {
                gvAccruedBill.Rows[i].Cells[12].Visible = false;
                gvAccruedBill.Rows[i].Cells[13].Visible = false;
                gvAccruedBill.Rows[i].Cells[14].Visible = false;
                gvAccruedBill.Rows[i].Cells[15].Visible = false;
                gvAccruedBill.Rows[i].Cells[16].Visible = false;
                gvAccruedBill.Rows[i].Cells[17].Visible = false;
                gvAccruedBill.Rows[i].Cells[18].Visible = false;
                gvAccruedBill.Rows[i].Cells[19].Visible = false;
            }
            TrAccrued.Visible = true;
            trRentBill.Visible = false;
            trReportsView.Visible = false;
            trReservationBill.Visible = false;
            btnAccruedBill.Visible = true;
            btnAccruedCancel.Visible = true;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('There is No Reservation/Delivery/Invalid Date...'); </script> ");
            TrAccrued.Visible = false;
            trRentBill.Visible = false;
            trReportsView.Visible = false;
            trReservationBill.Visible = false;
        }
    }
     public void GetDailyOverAboveBillDetail()
    {
        try
        {
            DateTime LDateTime = new DateTime();
            DateTime StartDate = new DateTime();
            DateTime EndDate = new DateTime();
            StartDate = Convert.ToDateTime(getDate_MDY(txtfdate.Text));
            EndDate = Convert.ToDateTime(getDate_MDY(txttodate.Text));

            decimal PerDayRate = 0;
            if (txtCPRate.Text != "")
            {
                PerDayRate = Convert.ToDecimal(txtCPRate.Text);
            }
            int j = 0;
            int h = 0;
            int m = 0;
            DataTable dw = new DataTable();
            dw.Columns.AddRange(new DataColumn[]  { 
                 new DataColumn("Static_Date",typeof(DateTime)),   
              new DataColumn("Deposit_Bags",typeof(decimal),null),
                new DataColumn("Deliver_Bags",typeof(decimal)),
                new DataColumn("Deposit_Weight",typeof(decimal)),
                new DataColumn("Deliver_Weight",typeof(decimal)),
                new DataColumn("Godown_Id",typeof(string)),
                new DataColumn("Flag",typeof(string)),
            });

            DateTime Static_date = new DateTime();
            DateTime Recent_Static_date = new DateTime();
            DataTable ddt2 = new DataTable();

            ddt2.Columns.AddRange(new DataColumn[]  { 
          new DataColumn("Static_Date",typeof(DateTime),null),
            new DataColumn("Opening_Balance",typeof(decimal)),
            new DataColumn("Receive_Bags",typeof(decimal)),
            new DataColumn("Issue_Bags",typeof(decimal)),
            new DataColumn("Closing_Balance",typeof(decimal)),

            new DataColumn("Opening_Weight",typeof(decimal)),
            new DataColumn("Receive_Weight",typeof(decimal)),
            new DataColumn("Issue_Weight",typeof(decimal)),
            new DataColumn("Closing_Weight",typeof(decimal)),
        new DataColumn("Per_Day_Rate",typeof(decimal)),
        new DataColumn("Charges",typeof(decimal)),
        //new DataColumn("Weight_Charges",typeof(decimal)),
        new DataColumn("Godown_Id",typeof(string)),
        });

            DataTable ddt3 = new DataTable();
            ddt3.Columns.AddRange(new DataColumn[]  { 
          new DataColumn("Deposit_Date",typeof(DateTime),null),
            new DataColumn("Opening_Balance",typeof(decimal)),
            new DataColumn("Receive_Bags",typeof(decimal)),
            new DataColumn("Issue_Bags",typeof(decimal)),
            new DataColumn("Closing_Balance",typeof(decimal)),

             new DataColumn("Opening_Weight",typeof(decimal)),
            new DataColumn("Receive_Weight",typeof(decimal)),
            new DataColumn("Issue_Weight",typeof(decimal)),
            new DataColumn("Closing_Weight",typeof(decimal)),
        new DataColumn("Per_Day_Rate",typeof(decimal)),
        new DataColumn("Charges",typeof(decimal)),
          new DataColumn("Reservation_In_Bag",typeof(decimal)),
        new DataColumn("Over_Bags",typeof(decimal)),
         //new DataColumn("Weight_Charges",typeof(decimal)),
         new DataColumn("Godown_Id",typeof(string)),
        });

            string BranchID = Session["BranchID"].ToString();
            string str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,delwght as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] where BranchID='" + BranchID + "' and Depositor_Name='" + ddldepos_name.SelectedItem.Text + "' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Static_date = Convert.ToDateTime(dt.Rows[0]["datecom"].ToString());
                if (StartDate < Static_date)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid From Date(There are No Deposit/Delivery)...'); </script> ");
                    trOverAboveDaily.Visible = false;
                    trRentBill.Visible = false;
                    trReportsView.Visible = false;
                    btnGenBill.Visible = false;
                    btncancel2.Visible = false;
                }
                //else if (EndDate > (Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["datecom"].ToString())))
                //{
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid To Date(There are No Deposit/Delivery)...'); </script> ");
                //    trRentBill.Visible = false;
                //    trReportsView.Visible = false;
                //    btnGenBill.Visible = false;
                //    btncancel2.Visible = false;
                //}
                else
                {
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {

                            Static_date = Convert.ToDateTime(dt.Rows[i]["datecom"].ToString());

                            if (i == 0)
                            {
                                Static_date = Convert.ToDateTime(dt.Rows[i]["datecom"].ToString());
                                dw.Rows.Add();
                                //            
                                dw.Rows[h]["Static_date"] = Convert.ToDateTime(Static_date);
                                dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
                                dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
                                dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
                                dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
                                dw.Rows[h]["Godown_Id"] = dt.Rows[i]["Godown_Id"].ToString();
                                dw.Rows[h]["Flag"] = "Y";
                                Recent_Static_date = Static_date;
                                h++;

                            }
                            else if (Recent_Static_date == Static_date)
                            {
                                h--;
                                Static_date = Recent_Static_date;
                                dw.Rows.Add();

                                dw.Rows[h]["Static_date"] = Static_date;
                                dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dw.Rows[h]["Deposit_Bags"]) + Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
                                dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dw.Rows[h]["Deliver_Bags"]) + Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
                                dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dw.Rows[h]["Deposit_Weight"]) + Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
                                dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dw.Rows[h]["Deliver_Weight"]) + Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
                                if (dw.Rows[h]["Godown_Id"].ToString().Contains(dt.Rows[i]["Godown_Id"].ToString()))
                                {

                                }
                                else
                                {
                                    dw.Rows[h]["Godown_Id"] = dw.Rows[h]["Godown_Id"].ToString() + "," + dt.Rows[i]["Godown_Id"].ToString();
                                }
                                dw.Rows[h]["Flag"] = "Y";
                                Recent_Static_date = Static_date;
                                h++;
                            }
                            else if (Recent_Static_date.AddDays(1) == Static_date)
                            {

                                dw.Rows.Add();

                                dw.Rows[h]["Static_date"] = Static_date;
                                dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
                                dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
                                dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
                                dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
                                dw.Rows[h]["Godown_Id"] = dt.Rows[i]["Godown_Id"].ToString();
                                dw.Rows[h]["Flag"] = "Y";
                                Recent_Static_date = Static_date;
                                h++;
                            }

                            else if (Recent_Static_date != Static_date)
                            {
                                dw.Rows.Add();
                                Static_date = Recent_Static_date.AddDays(1);
                                dw.Rows[h]["Static_date"] = Static_date;
                                dw.Rows[h]["Deposit_Bags"] = 0;
                                dw.Rows[h]["Deliver_Bags"] = 0;
                                dw.Rows[h]["Deposit_Weight"] = 0;
                                dw.Rows[h]["Deliver_Weight"] = 0;
                                dw.Rows[h]["Godown_Id"] = "";
                                dw.Rows[h]["Flag"] = "Y";
                                h++;
                                i--;
                                Recent_Static_date = Static_date;
                            }
                            else if (Recent_Static_date == Static_date && (dt.Rows[i]["recbags"].ToString() == "" && dt.Rows[i]["delbags"].ToString() == ""))
                            {
                                dw.Rows.Add();
                                Static_date = Static_date.AddDays(1);
                                dw.Rows[h]["Static_date"] = Static_date;
                                dw.Rows[h]["Deposit_Bags"] = 0;
                                dw.Rows[h]["Deliver_Bags"] = 0;
                                dw.Rows[h]["Deposit_Weight"] = 0;
                                dw.Rows[h]["Deliver_Weight"] = 0;
                                dw.Rows[h]["Godown_Id"] = "";
                                dw.Rows[h]["Flag"] = "Y";
                                h++;
                                i--;
                                Recent_Static_date = Static_date;
                            }
                            else if (Recent_Static_date != Static_date)
                            {
                                dw.Rows.Add();
                                Static_date = Recent_Static_date.AddDays(1);
                                dw.Rows[h]["Static_date"] = Static_date;
                                dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
                                dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
                                dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
                                dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
                                dw.Rows[h]["Godown_Id"] = dt.Rows[i]["Godown_Id"].ToString();
                                dw.Rows[h]["Flag"] = "Y";
                                Recent_Static_date = Static_date;
                                h++;
                            }
                        }
                        for (int i = dt.Rows.Count - 1; i <= dt.Rows.Count + 365; i++)
                        {
                            if (i == (dt.Rows.Count - 1))
                            {
                                LDateTime = Convert.ToDateTime(dw.Rows[h - 1]["Static_date"]);
                                LDateTime = LDateTime.AddDays(1);
                            }
                            else
                            {
                                LDateTime = LDateTime.AddDays(1);
                            }
                            dw.Rows.Add();
                            dw.Rows[h]["Static_date"] = LDateTime;
                            dw.Rows[h]["Deposit_Bags"] = 0;
                            dw.Rows[h]["Deliver_Bags"] = 0;
                            dw.Rows[h]["Deposit_Weight"] = 0;
                            dw.Rows[h]["Deliver_Weight"] = 0;
                            dw.Rows[h]["Godown_Id"] = "";
                            dw.Rows[h]["Flag"] = "Y";
                            h++;
                        }
                    }
                    if (dw.Rows.Count > 0)
                    {
                        for (int k = 0; k <= dw.Rows.Count - 1; k++)
                        {
                            if (k == 0 && dw.Rows[k]["Flag"].ToString() == "Y")
                            {
                                ddt2.Rows.Add();
                                ddt2.Rows[k]["Static_Date"] = Convert.ToDateTime(dw.Rows[k]["Static_Date"]);
                                ddt2.Rows[k]["Opening_Balance"] = 0;
                                ddt2.Rows[k]["Receive_Bags"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"]);
                                ddt2.Rows[k]["Issue_Bags"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Bags"]);
                                ddt2.Rows[k]["Closing_Balance"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"]) - Convert.ToDecimal(dw.Rows[k]["Deliver_Bags"]);
                                ddt2.Rows[k]["Per_Day_Rate"] = PerDayRate;
                                ddt2.Rows[k]["Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Balance"]);

                                ddt2.Rows[k]["Opening_Weight"] = 0;
                                ddt2.Rows[k]["Receive_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]);
                                ddt2.Rows[k]["Issue_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]);
                                ddt2.Rows[k]["Closing_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]) - Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]);
                                //ddt2.Rows[k]["Weight_Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Weight"]);
                                ddt2.Rows[k]["Godown_Id"] = dw.Rows[k]["Godown_Id"].ToString();

                            }
                            else if (dw.Rows[k]["Flag"].ToString() == "Y")
                            {
                                ddt2.Rows.Add();
                                ddt2.Rows[k]["Static_Date"] = Convert.ToDateTime(dw.Rows[k]["Static_Date"]);
                                ddt2.Rows[k]["Opening_Balance"] = ddt2.Rows[k - 1]["Closing_Balance"];
                                ddt2.Rows[k]["Receive_Bags"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"]);
                                ddt2.Rows[k]["Issue_Bags"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Bags"]);
                                ddt2.Rows[k]["Closing_Balance"] = Convert.ToDecimal(ddt2.Rows[k - 1]["Closing_Balance"]) + Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"]) - Convert.ToDecimal(dw.Rows[k]["Deliver_Bags"]);
                                ddt2.Rows[k]["Per_Day_Rate"] = PerDayRate;
                                ddt2.Rows[k]["Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Balance"]);

                                ddt2.Rows[k]["Opening_Weight"] = ddt2.Rows[k - 1]["Closing_Weight"];
                                ddt2.Rows[k]["Receive_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]);
                                ddt2.Rows[k]["Issue_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]);
                                ddt2.Rows[k]["Closing_Weight"] = Convert.ToDecimal(ddt2.Rows[k - 1]["Closing_Weight"]) + Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]) - Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]);
                                //ddt2.Rows[k]["Weight_Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Weight"]);
                                ddt2.Rows[k]["Godown_Id"] = dw.Rows[k]["Godown_Id"].ToString();
                            }
                        }
                    }
                    if (ddt2.Rows.Count > 0)
                    {
                        for (int l = 0; l <= ddt2.Rows.Count - 1; l++)
                        {
                            decimal RBAGS=0;
                            DateTime dt1 = Convert.ToDateTime(ddt2.Rows[l]["Static_Date"]);
                            DateTime dt2 = Convert.ToDateTime(ddt2.Rows[ddt2.Rows.Count - 1]["Static_Date"]);
                            if (dt1 >= StartDate && dt1 <= EndDate)
                            {
                                /////////
                                string StartDates = dt1.ToString("MM/dd/yyyy");
                                //string RQuery = "select SUM(Bags) as Bags from Godown_Reservation where Depositor_Name='" + ddldepos_name.SelectedValue.ToString() + "' and ('" + StartDates + "'>=From_Date) and ('" + StartDates + "'<= To_Date) and Depot_Id='" + BranchID + "'";
                                string RQuery = "select SUM(tbl_Reservation_Register.Reserved_Quantity) as Bags from tbl_Reservation_Register inner join tbl_Register_Master_Detail on tbl_Register_Master_Detail.Register_No=tbl_Reservation_Register.Register_No where Depositor_Id='" + ddldepos_name.SelectedValue.ToString() + "' and ('" + StartDates + "'>=From_Date) and ('" + StartDates + "'<= To_Date) and tbl_Register_Master_Detail.Branch_Id='" + BranchID + "'";
                                SqlDataAdapter rda = new SqlDataAdapter(RQuery,con);
                                DataTable rdt = new DataTable();
                                rda.Fill(rdt);
                                if (rdt.Rows.Count > 0)
                                {
                                    if (rdt.Rows[0]["Bags"].ToString() != "")
                                    {
                                        RBAGS = Convert.ToDecimal(rdt.Rows[0]["Bags"]);
                                    }
                                    else
                                    {
                                        RBAGS = 0;
                                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('...'); </script> ");
                                    }
                                }
                                //////////
                                ddt3.Rows.Add();
                                ddt3.Rows[m]["Deposit_Date"] = Convert.ToDateTime(ddt2.Rows[l]["Static_Date"]);
                                ddt3.Rows[m]["Opening_Balance"] = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
                                ddt3.Rows[m]["Receive_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Receive_Bags"]);
                                ddt3.Rows[m]["Issue_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Issue_Bags"]);
                                ddt3.Rows[m]["Closing_Balance"] = Convert.ToDecimal(ddt2.Rows[l]["Closing_Balance"]);
                                ddt3.Rows[m]["Reservation_In_Bag"] = RBAGS;
                                if (Convert.ToDecimal((ddt3.Rows[m]["Closing_Balance"])) >= RBAGS)
                                {
                                    ddt3.Rows[m]["Over_Bags"] = Convert.ToDecimal((ddt3.Rows[m]["Closing_Balance"])) - RBAGS;
                                }
                                else
                                {
                                    ddt3.Rows[m]["Over_Bags"] = 0;
                                }
                                ddt3.Rows[m]["Per_Day_Rate"] = PerDayRate;
                                ddt3.Rows[m]["Charges"] = Convert.ToDecimal(ddt3.Rows[m]["Over_Bags"]) * PerDayRate;

                                ddt3.Rows[m]["Opening_Weight"] = Convert.ToDecimal(ddt2.Rows[l]["Opening_Weight"]);
                                ddt3.Rows[m]["Receive_Weight"] = Convert.ToDecimal(ddt2.Rows[l]["Receive_Weight"]);
                                ddt3.Rows[m]["Issue_Weight"] = Convert.ToDecimal(ddt2.Rows[l]["Issue_Weight"]);
                                ddt3.Rows[m]["Closing_Weight"] = Convert.ToDecimal(ddt2.Rows[l]["Closing_Weight"]);
                                ddt3.Rows[m]["Godown_Id"] = dw.Rows[l]["Godown_Id"].ToString();

                                m = m + 1;
                            }
                        }
                    }
                    gvOverAboveDaily.DataSource = ddt3;
                    gvOverAboveDaily.DataBind();
                    gvOverAboveDaily.HeaderRow.Cells[9].Visible = false;
                    gvOverAboveDaily.HeaderRow.Cells[10].Visible = false;
                    gvOverAboveDaily.HeaderRow.Cells[11].Visible = false;
                    gvOverAboveDaily.HeaderRow.Cells[12].Visible = false;
                    gvOverAboveDaily.HeaderRow.Cells[13].Visible = false;
                    for (int i = 0; i <= gvOverAboveDaily.Rows.Count - 1; i++)
                    {
                        gvOverAboveDaily.Rows[i].Cells[9].Visible = false;
                        gvOverAboveDaily.Rows[i].Cells[10].Visible = false;
                        gvOverAboveDaily.Rows[i].Cells[11].Visible = false;
                        gvOverAboveDaily.Rows[i].Cells[12].Visible = false;
                        gvOverAboveDaily.Rows[i].Cells[13].Visible = false;
                    }
                    trOverAboveDaily.Visible = true;
                    trRentBill.Visible = false;
                    trReportsView.Visible = false;
                    btnOSubmit.Visible = true;
                    btnOCancel.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

     public void GetAccruedOverAboveBillDetail()
     {
         try
         {
             DateTime StartDate = new DateTime();
             DateTime EndDate = new DateTime();
             StartDate = Convert.ToDateTime(getDate_MDY(txtfdate.Text));
             EndDate = Convert.ToDateTime(getDate_MDY(txttodate.Text));

             decimal PerDayRate = 0;
             if (txtCPRate.Text != "")
             {
                 PerDayRate = Convert.ToDecimal(txtCPRate.Text);
             }
             int m = 0;
             DataTable dw = new DataTable();
             dw.Columns.AddRange(new DataColumn[]  { 
                 new DataColumn("Static_Date",typeof(DateTime)),   
              new DataColumn("Deposit_Bags",typeof(decimal),null),
                new DataColumn("Deliver_Bags",typeof(decimal)),
                new DataColumn("Deposit_Weight",typeof(decimal)),
                new DataColumn("Deliver_Weight",typeof(decimal)),
                new DataColumn("Godown_Id",typeof(string)),
                new DataColumn("Flag",typeof(string)),
            });

             DateTime Static_date = new DateTime();
             DataTable ddt2 = new DataTable();

             ddt2.Columns.AddRange(new DataColumn[]  { 
          new DataColumn("Static_Date",typeof(DateTime),null),
            new DataColumn("Opening_Balance",typeof(decimal)),
            new DataColumn("Receive_Bags",typeof(decimal)),
            new DataColumn("Issue_Bags",typeof(decimal)),
            new DataColumn("Closing_Balance",typeof(decimal)),

            new DataColumn("Opening_Weight",typeof(decimal)),
            new DataColumn("Receive_Weight",typeof(decimal)),
            new DataColumn("Issue_Weight",typeof(decimal)),
            new DataColumn("Closing_Weight",typeof(decimal)),
        new DataColumn("Per_Day_Rate",typeof(decimal)),
        new DataColumn("Charges",typeof(decimal)),
        new DataColumn("Godown_Id",typeof(string)),
         //new DataColumn("Storage_Period",typeof(string)),
          //new DataColumn("Accrued_Period",typeof(string)),
          new DataColumn("Depositor_WHR_Id",typeof(string)),
        });

             DataTable ddt3 = new DataTable();
             ddt3.Columns.AddRange(new DataColumn[]  { 
          new DataColumn("Deposit_Date",typeof(DateTime),null),
            new DataColumn("Opening_Balance",typeof(decimal)),
            new DataColumn("Receive_Bags",typeof(decimal)),
            new DataColumn("Issue_Bags",typeof(decimal)),
            new DataColumn("Closing_Balance",typeof(decimal)),

             new DataColumn("Opening_Weight",typeof(decimal)),
            new DataColumn("Receive_Weight",typeof(decimal)),
            new DataColumn("Issue_Weight",typeof(decimal)),
            new DataColumn("Closing_Weight",typeof(decimal)),
        new DataColumn("Per_Day_Rate",typeof(decimal)),
        new DataColumn("Charges",typeof(decimal)),
          new DataColumn("Reservation_In_Bag",typeof(decimal)),
        new DataColumn("Over_Bags",typeof(decimal)),
        new DataColumn("NOver_Bags",typeof(decimal)),
         //new DataColumn("Weight_Charges",typeof(decimal)),
         new DataColumn("Godown_Id",typeof(string)),
          new DataColumn("Storage_Period",typeof(string)),
          new DataColumn("Accrued_Period",typeof(string)),
          new DataColumn("Depositor_WHR_Id",typeof(string)),
        });

             DataTable ddt4 = new DataTable();
             ddt4.Columns.AddRange(new DataColumn[]  { 
          new DataColumn("Deposit_Date",typeof(DateTime),null),
            new DataColumn("Opening_Balance",typeof(decimal)),
            new DataColumn("Receive_Bags",typeof(decimal)),
            new DataColumn("Issue_Bags",typeof(decimal)),
            new DataColumn("Closing_Balance",typeof(decimal)),

             new DataColumn("Opening_Weight",typeof(decimal)),
            new DataColumn("Receive_Weight",typeof(decimal)),
            new DataColumn("Issue_Weight",typeof(decimal)),
            new DataColumn("Closing_Weight",typeof(decimal)),
        new DataColumn("Monthly_Rate",typeof(decimal)),
        new DataColumn("Charges",typeof(decimal)),
          new DataColumn("Reservation_In_Bag",typeof(decimal)),
        new DataColumn("Over_Bags",typeof(decimal)),
        new DataColumn("NOver_Bags",typeof(decimal)),
         //new DataColumn("Weight_Charges",typeof(decimal)),
         new DataColumn("Godown_Id",typeof(string)),
          new DataColumn("Storage_Period",typeof(string)),
          new DataColumn("Accrued_Period",typeof(string)),
          new DataColumn("Depositor_WHR_Id",typeof(string)),
        });
             DataTable ddt5 = new DataTable();
             ddt5.Columns.AddRange(new DataColumn[]  { 
          new DataColumn("Deposit_Date",typeof(DateTime),null),
            new DataColumn("Opening_Balance",typeof(decimal)),
            new DataColumn("Receive_Bags",typeof(decimal)),
            new DataColumn("Issue_Bags",typeof(decimal)),
            new DataColumn("Closing_Balance",typeof(decimal)),

             new DataColumn("Opening_Weight",typeof(decimal)),
            new DataColumn("Receive_Weight",typeof(decimal)),
            new DataColumn("Issue_Weight",typeof(decimal)),
            new DataColumn("Closing_Weight",typeof(decimal)),
        new DataColumn("Monthly_Rate",typeof(decimal)),
        new DataColumn("Charges",typeof(decimal)),
          new DataColumn("Reservation_In_Bag",typeof(decimal)),
        new DataColumn("Over_Bags",typeof(decimal)),
        new DataColumn("NOver_Bags",typeof(decimal)),
         //new DataColumn("Weight_Charges",typeof(decimal)),
         new DataColumn("Godown_Id",typeof(string)),
          new DataColumn("Storage_Period",typeof(string)),
          new DataColumn("Accrued_Period",typeof(string)),
          new DataColumn("Depositor_WHR_Id",typeof(string)),
        });
             string BranchID = Session["BranchID"].ToString();
             //string str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,delwght as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] where BranchID='" + BranchID + "' and Depositor_Name='" + ddldepos_name.SelectedItem.Text + "' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "'";
             //string str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,delwght as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM,ISNULL(dbo.dkDateDiffInMonthDay(WHR_Issue_Date,DeliveryDate), 0) AS PeriodOfStorage,CASE WHEN isnull(dbo.dkDateDiffInMonthDay(WHR_Issue_Date, DeliveryDate), 0)='0' THEN '0' WHEN CAST(SUBSTRING(isnull(dbo.dkDateDiffInMonthDay(WHR_Issue_Date, DeliveryDate), 0), 11, 2) AS int) <= 15 THEN substring(isnull(dbo.dkDateDiffInMonthDay(WHR_Issue_Date, DeliveryDate), 0), 1, 9)+ '15 Days' WHEN CAST(SUBSTRING(isnull(dbo.dkDateDiffInMonthDay(WHR_Issue_Date, DeliveryDate), 0), 11, 2) AS int) >= 16 AND CAST(SUBSTRING(isnull(dbo.dkDateDiffInMonthDay(WHR_Issue_Date, DeliveryDate), 0), 1, 2) AS int) BETWEEN 0 AND 8 THEN '0' + CAST(CAST(substring(isnull(dbo.dkDateDiffInMonthDay(WHR_Issue_Date, DeliveryDate), 0), 1, 2) AS int) + 1 AS varchar)+ ' Months 00 Days' WHEN CAST(SUBSTRING(isnull(dbo.dkDateDiffInMonthDay(WHR_Issue_Date,DeliveryDate), 0), 11, 2) AS int)>= 16 THEN CAST(CAST(substring(isnull(dbo.dkDateDiffInMonthDay(WHR_Issue_Date, DeliveryDate), 0), 1, 2) AS int) + 1 AS varchar)+ ' Months 00 Days' WHEN CAST(SUBSTRING(isnull(dbo.dkDateDiffInMonthDay(WHR_Issue_Date, DeliveryDate), 0), 1, 2) AS int) = 0 AND CAST(SUBSTRING(isnull(dbo.dkDateDiffInMonthDay(WHR_Issue_Date, DeliveryDate), 0), 1, 2) AS int)= 0 THEN '00' ELSE '0' END AS ChargeOnPeriod FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] where BranchID='" + BranchID + "' and Depositor_Name='" + ddldepos_name.SelectedItem.Text + "' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "'";
             string str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,delwght as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] where BranchID='" + BranchID + "' and Depositor_Name='" + ddldepos_name.SelectedItem.Text + "' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "'";
             SqlCommand cmd = new SqlCommand(str, con);
             SqlDataAdapter da = new SqlDataAdapter(cmd);
             DataTable dt = new DataTable();
             da.Fill(dt);
             if (dt.Rows.Count > 0)
             {
                 Static_date = Convert.ToDateTime(dt.Rows[0]["datecom"].ToString());
                 if (StartDate < Static_date)
                 {
                     ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid From Date(There are No Deposit/Delivery)...'); </script> ");
                     trOverAboveDaily.Visible = false;
                     trRentBill.Visible = false;
                     trReportsView.Visible = false;
                     btnGenBill.Visible = false;
                     btncancel2.Visible = false;
                 }
                 if (dt.Rows.Count > 0)
                 {
                     for (int k = 0; k <= dt.Rows.Count - 1; k++)
                     {
                         if (k == 0)
                         {
                             ddt2.Rows.Add();
                             ddt2.Rows[k]["Static_Date"] = Convert.ToDateTime(dt.Rows[k]["datecom"]);
                             ddt2.Rows[k]["Opening_Balance"] = 0;
                             ddt2.Rows[k]["Receive_Bags"] = Convert.ToDecimal(dt.Rows[k]["recbags"]);
                             ddt2.Rows[k]["Issue_Bags"] = Convert.ToDecimal(dt.Rows[k]["delbags"]);
                             ddt2.Rows[k]["Closing_Balance"] = Convert.ToDecimal(dt.Rows[k]["recbags"]) - Convert.ToDecimal(dt.Rows[k]["delbags"]);
                             ddt2.Rows[k]["Per_Day_Rate"] = PerDayRate;
                             ddt2.Rows[k]["Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Balance"]);

                             ddt2.Rows[k]["Opening_Weight"] = 0;
                             ddt2.Rows[k]["Receive_Weight"] = Convert.ToDecimal(dt.Rows[k]["Rec_Weight"]);
                             ddt2.Rows[k]["Issue_Weight"] = Convert.ToDecimal(dt.Rows[k]["Del_Weight"]);
                             ddt2.Rows[k]["Closing_Weight"] = Convert.ToDecimal(dt.Rows[k]["Rec_Weight"]) - Convert.ToDecimal(dt.Rows[k]["Del_Weight"]);
                             //ddt2.Rows[k]["Weight_Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Weight"]);
                             ddt2.Rows[k]["Godown_Id"] = dt.Rows[k]["Godown_Id"].ToString();
                             //ddt2.Rows[k]["Storage_Period"] = dt.Rows[k]["PeriodOfStorage"].ToString();
                             //ddt2.Rows[k]["Accrued_Period"] = dt.Rows[k]["ChargeOnPeriod"].ToString();
                             ddt2.Rows[k]["Depositor_WHR_Id"] = dt.Rows[k]["Depositor_WHR_Id"].ToString();
                         }
                         else
                         {
                             ddt2.Rows.Add();
                             ddt2.Rows[k]["Static_Date"] = Convert.ToDateTime(dt.Rows[k]["datecom"]);
                             ddt2.Rows[k]["Opening_Balance"] = ddt2.Rows[k - 1]["Closing_Balance"];
                             ddt2.Rows[k]["Receive_Bags"] = Convert.ToDecimal(dt.Rows[k]["recbags"]);
                             ddt2.Rows[k]["Issue_Bags"] = Convert.ToDecimal(dt.Rows[k]["delbags"]);
                             ddt2.Rows[k]["Closing_Balance"] = Convert.ToDecimal(ddt2.Rows[k - 1]["Closing_Balance"]) + Convert.ToDecimal(ddt2.Rows[k]["Receive_Bags"]) - Convert.ToDecimal(ddt2.Rows[k]["Issue_Bags"]);
                             ddt2.Rows[k]["Per_Day_Rate"] = PerDayRate;
                             ddt2.Rows[k]["Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Balance"]);

                             ddt2.Rows[k]["Opening_Weight"] = ddt2.Rows[k - 1]["Closing_Weight"];
                             ddt2.Rows[k]["Receive_Weight"] = Convert.ToDecimal(dt.Rows[k]["Rec_Weight"]);
                             ddt2.Rows[k]["Issue_Weight"] = Convert.ToDecimal(dt.Rows[k]["Del_Weight"]);
                             ddt2.Rows[k]["Closing_Weight"] = Convert.ToDecimal(ddt2.Rows[k - 1]["Closing_Weight"]) + Convert.ToDecimal(ddt2.Rows[k]["Receive_Weight"]) - Convert.ToDecimal(ddt2.Rows[k]["Issue_Weight"]);
                             ddt2.Rows[k]["Godown_Id"] = dt.Rows[k]["Godown_Id"].ToString();
                             ddt2.Rows[k]["Depositor_WHR_Id"] = dt.Rows[k]["Depositor_WHR_Id"].ToString();
                         }
                     }
                 }
                 string F = "N";
                 if (ddt2.Rows.Count > 0)
                 {
                     for (int l = 0; l <= ddt2.Rows.Count - 1; l++)
                     {
                         //int Big = 0;
                         decimal Big = 0;
                         //int k=0;

                         decimal RBAGS = 0;
                         //int RBC = 0;
                         decimal RBC = 0;
                         DateTime dt1 = Convert.ToDateTime(ddt2.Rows[l]["Static_Date"]);
                         DateTime dt2 = Convert.ToDateTime(ddt2.Rows[ddt2.Rows.Count - 1]["Static_Date"]);
                         int x = Convert.ToInt32(dt1.ToString("dd"));
                         //if (dt1 >= StartDate && dt1 <= EndDate)
                         {
                             /////////
                             string StartDates = dt1.ToString("MM/dd/yyyy");
                             //string RQuery = "select SUM(Bags) as Bags from Godown_Reservation where Depositor_Name='" + ddldepos_name.SelectedValue.ToString() + "' and ('" + StartDates + "'>=From_Date) and ('" + StartDates + "'<= To_Date) and Depot_Id='" + BranchID + "'";
                             string RQuery = "select SUM(tbl_Reservation_Register.Reserved_Quantity) as Bags from tbl_Reservation_Register inner join tbl_Register_Master_Detail on tbl_Register_Master_Detail.Register_No=tbl_Reservation_Register.Register_No where Depositor_Id='" + ddldepos_name.SelectedValue.ToString() + "' and ('" + StartDate + "'>=From_Date) and ('" + StartDate + "'<= To_Date) and tbl_Register_Master_Detail.Branch_Id='" + BranchID + "'";
                             SqlDataAdapter rda = new SqlDataAdapter(RQuery, con);
                             DataTable rdt = new DataTable();
                             rda.Fill(rdt);
                             if (rdt.Rows.Count > 0)
                             {
                                 if (rdt.Rows[0]["Bags"].ToString() != "")
                                 {
                                     RBAGS = Convert.ToDecimal(rdt.Rows[0]["Bags"]);
                                 }
                                 else
                                 {
                                     RBAGS = 0;
                                     //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('...'); </script> ");
                                 }
                             }
                             else
                             {
                                 ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('There are No Reservation...'); </script> ");
                             }

                             /////////
                             ddt3.Rows.Add();
                             ddt3.Rows[m]["Deposit_Date"] = Convert.ToDateTime(ddt2.Rows[l]["Static_Date"]);
                             ddt3.Rows[m]["Opening_Balance"] = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
                             ddt3.Rows[m]["Receive_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Receive_Bags"]);
                             ddt3.Rows[m]["Issue_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Issue_Bags"]);
                             ddt3.Rows[m]["Closing_Balance"] = Convert.ToDecimal(ddt2.Rows[l]["Closing_Balance"]);

                             ddt3.Rows[m]["Reservation_In_Bag"] = RBAGS;
                             if (Convert.ToDecimal(ddt3.Rows[m]["Issue_Bags"]) != 0 && RBAGS != 0 && Convert.ToDecimal((ddt3.Rows[m]["Opening_Balance"])) >= RBAGS)
                             {
                                 decimal OverBags = 0;
                                 OverBags = Convert.ToDecimal((ddt3.Rows[m]["Opening_Balance"])) - RBAGS;
                                 if (Convert.ToDecimal(ddt3.Rows[m]["Issue_Bags"]) <= OverBags)
                                 {
                                     ddt3.Rows[m]["Over_Bags"] = ddt3.Rows[m]["Issue_Bags"];
                                 }
                                 else if (Convert.ToDecimal(ddt3.Rows[m]["Issue_Bags"]) >= OverBags)
                                 {
                                     ddt3.Rows[m]["Over_Bags"] = OverBags;
                                 }
                                 //ddt3.Rows[m]["Over_Bags"] = Convert.ToDecimal((ddt3.Rows[m]["Opening_Balance"])) - RBAGS;
                             }
                             else
                             {
                                 ddt3.Rows[m]["Over_Bags"] = 0;
                             }
                             ddt3.Rows[m]["Per_Day_Rate"] = PerDayRate;
                             ddt3.Rows[m]["Charges"] = Convert.ToDecimal(ddt3.Rows[m]["Over_Bags"]) * PerDayRate;

                             ddt3.Rows[m]["Opening_Weight"] = Convert.ToDecimal(ddt2.Rows[l]["Opening_Weight"]);
                             ddt3.Rows[m]["Receive_Weight"] = Convert.ToDecimal(ddt2.Rows[l]["Receive_Weight"]);
                             ddt3.Rows[m]["Issue_Weight"] = Convert.ToDecimal(ddt2.Rows[l]["Issue_Weight"]);
                             ddt3.Rows[m]["Closing_Weight"] = Convert.ToDecimal(ddt2.Rows[l]["Closing_Weight"]);
                             ddt3.Rows[m]["Godown_Id"] = ddt2.Rows[l]["Godown_Id"].ToString();
                             ddt3.Rows[m]["Depositor_WHR_Id"] = ddt2.Rows[l]["Depositor_WHR_Id"].ToString();
                             //////////////////////
                             if (l != 0)
                             {
                                 RBC = 0;
                                 for (int v = 0; v <= l; v++)
                                 {
                                     if (v != l)
                                     {
                                         //int OBS = Convert.ToInt32(ddt3.Rows[v]["Over_Bags"].ToString());   date: feb-04-2016
                                         decimal OBS = Convert.ToDecimal(ddt3.Rows[v]["Over_Bags"].ToString());
                                         Big = GetMaxValue(OBS, RBC);
                                         RBC = Big;
                                     }

                                 }
                                 //int NBS = Convert.ToInt32(ddt3.Rows[m]["Over_Bags"].ToString());   date: feb-04-2016
                                 decimal NBS = Convert.ToDecimal(ddt3.Rows[m]["Over_Bags"].ToString());
                                 if (RBC != 0 && F != "Y")
                                 {
                                     ddt3.Rows[m - 1]["NOver_Bags"] = RBC;
                                     F = "Y";
                                     if (RBC == 0 || RBC > NBS)
                                     {

                                         ddt3.Rows[m]["NOver_Bags"] = 0;
                                     }
                                     else if (RBC < NBS)
                                     {
                                         ddt3.Rows[m]["NOver_Bags"] = NBS - RBC;
                                     }
                                 }
                                 else if (RBC == 0 || RBC > NBS)
                                 {

                                     ddt3.Rows[m]["NOver_Bags"] = 0;
                                 }
                                 else if (RBC < NBS)
                                 {
                                     ddt3.Rows[m]["NOver_Bags"] = NBS - RBC;
                                 }
                             }
                             else
                             {
                                 ddt3.Rows[m]["NOver_Bags"] = 0;
                             }
                             m = m + 1;
                         }
                     }
                 }
                 /////////////////////////////////////////////////////
                 if (ddt3.Rows.Count > 0)
                 {
                     decimal HMOverBags = 0;
                     string DFlag = "N";
                     decimal RemainOB = 0;
                     int h = 0;
                     for (int h1 = 0; h1 <= ddt3.Rows.Count - 1; h1++)
                     {
                         ddt4.Rows.Add();
                         ddt4.Rows[h]["Deposit_Date"] = ddt3.Rows[h1]["Deposit_Date"];
                         ddt4.Rows[h]["Opening_Balance"] = ddt3.Rows[h1]["Opening_Balance"];
                         ddt4.Rows[h]["Receive_Bags"] = ddt3.Rows[h1]["Receive_Bags"];
                         ddt4.Rows[h]["Issue_Bags"] = ddt3.Rows[h1]["Issue_Bags"];
                         ddt4.Rows[h]["Closing_Balance"] = ddt3.Rows[h1]["Closing_Balance"];
                         ddt4.Rows[h]["Reservation_In_Bag"] = ddt3.Rows[h1]["Reservation_In_Bag"];
                         ddt4.Rows[h]["Opening_Weight"] = ddt3.Rows[h1]["Opening_Weight"];
                         ddt4.Rows[h]["Receive_Weight"] = ddt3.Rows[h1]["Receive_Weight"];
                         ddt4.Rows[h]["Issue_Weight"] = ddt3.Rows[h1]["Issue_Weight"];
                         ddt4.Rows[h]["Closing_Weight"] = ddt3.Rows[h1]["Closing_Weight"];
                         ddt4.Rows[h]["Godown_Id"] = ddt3.Rows[h1]["Godown_Id"];
                         ddt4.Rows[h]["Over_Bags"] = ddt3.Rows[h1]["Over_Bags"];
                         ddt4.Rows[h]["NOver_Bags"] = ddt3.Rows[h1]["NOver_Bags"];
                         ddt4.Rows[h]["Monthly_Rate"] = Convert.ToDecimal(txtcomrate.Text);
                         //ddt4.Rows[h]["Accrued_Period"] = "15 Days";
                         ddt4.Rows[h]["Depositor_WHR_Id"] = ddt3.Rows[h1]["Depositor_WHR_Id"];
                         //
                         string SWHR = "";
                         SWHR = ddt4.Rows[h]["Depositor_WHR_Id"].ToString();

                         ddt4.Rows[h]["Storage_Period"] = "";

                         if (Convert.ToDateTime(ddt4.Rows[h]["Deposit_Date"]).Day == 15)
                         {
                             if (Convert.ToDecimal(ddt4.Rows[h]["Closing_Balance"]) >= Convert.ToDecimal(ddt4.Rows[h]["Reservation_In_Bag"]))
                             {
                                 HMOverBags = Convert.ToDecimal(ddt4.Rows[h]["Closing_Balance"]) - Convert.ToDecimal(ddt4.Rows[h]["Reservation_In_Bag"]);
                             }
                             else
                             {
                                 HMOverBags = 0;
                             }
                         }
                         else if (Convert.ToDateTime(ddt4.Rows[h]["Deposit_Date"]).Day > 15 && DFlag == "N")
                         {
                             if (Convert.ToDecimal(ddt4.Rows[h]["Closing_Balance"]) >= Convert.ToDecimal(ddt4.Rows[h]["Reservation_In_Bag"]))
                             {
                                 if (ddt4.Rows.Count != 1)
                                 {
                                     HMOverBags = Convert.ToDecimal(ddt4.Rows[h - 1]["Closing_Balance"]) - Convert.ToDecimal(ddt4.Rows[h - 1]["Reservation_In_Bag"]);
                                 }
                                 else
                                 {
                                     HMOverBags = 0;
                                 }
                             }
                             else
                             {
                                 HMOverBags = 0;
                             }
                             DFlag = "Y";
                         }

                         //if (DFlag == "N" && Convert.ToDateTime(ddt4.Rows[h]["Deposit_Date"]).Day >= 15)
                         if (Convert.ToDecimal(ddt4.Rows[h]["Issue_Bags"]) != 0 && Convert.ToDecimal(ddt4.Rows[h]["Over_Bags"]) != 0 && Convert.ToDecimal((ddt4.Rows[h]["Opening_Balance"])) >= Convert.ToDecimal(ddt4.Rows[h]["Over_Bags"]))
                         {
                             if (Convert.ToDateTime(ddt4.Rows[h]["Deposit_Date"]).Day <= 15)
                             {
                                 ddt4.Rows[h]["Accrued_Period"] = "15 Days";
                             }
                             else if (HMOverBags > 0 && HMOverBags>=Convert.ToDecimal(ddt4.Rows[h]["Issue_Bags"]))
                             {
                                 ddt4.Rows[h]["Accrued_Period"] = "01 Month";
                                 HMOverBags = HMOverBags - Convert.ToDecimal(ddt4.Rows[h]["Issue_Bags"]);
                             }
                             else if (HMOverBags > 0 && HMOverBags < Convert.ToDecimal(ddt4.Rows[h]["Issue_Bags"]))
                             {
                                 ddt4.Rows[h]["Accrued_Period"] = "01 Month";
                                 RemainOB = Convert.ToDecimal(ddt4.Rows[h]["Issue_Bags"]) - HMOverBags;
                                 //HMOverBags = HMOverBags - Convert.ToDecimal(ddt4.Rows[h]["Issue_Bags"]);
                                 ddt4.Rows[h]["Over_Bags"] = HMOverBags;
                                 HMOverBags = 0;
                                 
                             }
                             else
                             {
                                 ddt4.Rows[h]["Accrued_Period"] = "15 Days";
                             }
                         }
                         else
                         {
                             ddt4.Rows[h]["Accrued_Period"] = "";
                         }
                         int COB = Convert.ToInt32(ddt4.Rows[h]["Over_Bags"]);

                         if (ddt4.Rows[h]["Accrued_Period"].ToString() == "15 Days" && Convert.ToDecimal(ddt4.Rows[h]["Issue_Bags"]) != 0)
                         {
                             ddt4.Rows[h]["Charges"] = Convert.ToDecimal(ddt3.Rows[h1]["Over_Bags"]) * Convert.ToDecimal(txtcomrate.Text) / 2;
                         }
                         else if (ddt4.Rows[h]["Accrued_Period"].ToString() == "01 Month" && Convert.ToDecimal(ddt4.Rows[h]["Issue_Bags"]) != 0)
                         {
                             ddt4.Rows[h]["Charges"] = Convert.ToDecimal(ddt3.Rows[h1]["Over_Bags"]) * Convert.ToDecimal(txtcomrate.Text);
                         }
                         else
                         {
                             ddt4.Rows[h]["Charges"] = 0;
                         }
                         //ddt4.Rows[h]["Charges"] = 0;
                         if (RemainOB > 0)
                         {
                             h = h + 1;
                             ddt4.Rows.Add();
                             ddt4.Rows[h]["Deposit_Date"] = ddt3.Rows[h1]["Deposit_Date"];
                             ddt4.Rows[h]["Opening_Balance"] = ddt3.Rows[h1]["Opening_Balance"];
                             ddt4.Rows[h]["Receive_Bags"] = 0;
                             ddt4.Rows[h]["Issue_Bags"] = 0;
                             ddt4.Rows[h]["Closing_Balance"] = ddt3.Rows[h1]["Closing_Balance"];
                             ddt4.Rows[h]["Reservation_In_Bag"] = ddt3.Rows[h1]["Reservation_In_Bag"];
                             ddt4.Rows[h]["Opening_Weight"] = ddt3.Rows[h1]["Opening_Weight"];
                             ddt4.Rows[h]["Receive_Weight"] = 0;
                             ddt4.Rows[h]["Issue_Weight"] = 0;
                             ddt4.Rows[h]["Closing_Weight"] = ddt3.Rows[h1]["Closing_Weight"];
                             ddt4.Rows[h]["Godown_Id"] = ddt3.Rows[h1]["Godown_Id"];
                             ddt4.Rows[h]["Over_Bags"] = RemainOB;
                             ddt4.Rows[h]["NOver_Bags"] = ddt3.Rows[h1]["NOver_Bags"];
                             ddt4.Rows[h]["Monthly_Rate"] = Convert.ToDecimal(txtcomrate.Text);
                             ddt4.Rows[h]["Depositor_WHR_Id"] = ddt3.Rows[h1]["Depositor_WHR_Id"];
                             ddt4.Rows[h]["Storage_Period"] = "";
                             ddt4.Rows[h]["Accrued_Period"] = "15 Days";
                             ddt4.Rows[h]["Charges"] = RemainOB * Convert.ToDecimal(txtcomrate.Text) / 2;
                             RemainOB = 0;
                             h = h + 1;
                         }
                         else
                         {
                             h = h + 1;
                         }
                         
                     }
                     int b = 0;
                     for (int g = 0; g <= ddt3.Rows.Count - 1; g++)
                     {
                         DateTime SDate = new DateTime();
                         SDate = Convert.ToDateTime(ddt4.Rows[g]["Deposit_Date"]);
                         //(dt1 >= StartDate && dt1 <= EndDate)
                         if (SDate >= StartDate && SDate <= EndDate)
                         {
                             ddt5.Rows.Add();
                             ddt5.Rows[b]["Deposit_Date"] = ddt4.Rows[g]["Deposit_Date"];
                             ddt5.Rows[b]["Opening_Balance"] = ddt4.Rows[g]["Opening_Balance"];
                             ddt5.Rows[b]["Receive_Bags"] = ddt4.Rows[g]["Receive_Bags"];
                             ddt5.Rows[b]["Issue_Bags"] = ddt4.Rows[g]["Issue_Bags"];
                             ddt5.Rows[b]["Closing_Balance"] = ddt4.Rows[g]["Closing_Balance"];
                             ddt5.Rows[b]["Reservation_In_Bag"] = ddt4.Rows[g]["Reservation_In_Bag"];
                             ddt5.Rows[b]["Opening_Weight"] = ddt4.Rows[g]["Opening_Weight"];
                             ddt5.Rows[b]["Receive_Weight"] = ddt4.Rows[g]["Receive_Weight"];
                             ddt5.Rows[b]["Issue_Weight"] = ddt4.Rows[g]["Issue_Weight"];
                             ddt5.Rows[b]["Closing_Weight"] = ddt4.Rows[g]["Closing_Weight"];
                             ddt5.Rows[b]["Godown_Id"] = ddt4.Rows[g]["Godown_Id"];
                             ddt5.Rows[b]["Over_Bags"] = ddt4.Rows[g]["Over_Bags"];
                             ddt5.Rows[b]["NOver_Bags"] = ddt4.Rows[g]["NOver_Bags"];
                             ddt5.Rows[b]["Monthly_Rate"] = ddt4.Rows[g]["Monthly_Rate"];
                             ddt5.Rows[b]["Depositor_WHR_Id"] = ddt4.Rows[g]["Depositor_WHR_Id"];
                             ddt5.Rows[b]["Storage_Period"] = ddt4.Rows[g]["Storage_Period"];
                             string ss = ddt4.Rows[g]["Accrued_Period"].ToString();
                             ddt5.Rows[b]["Accrued_Period"] = ddt4.Rows[g]["Accrued_Period"];
                             ddt5.Rows[b]["Charges"] = ddt4.Rows[g]["Charges"];
                             b = b + 1;
                         }
                     }
                     /////////////////
                     if (ddt5.Rows.Count > 0)
                     {
                         if (Convert.ToDateTime(ddt5.Rows[ddt5.Rows.Count - 1]["Deposit_Date"]) == EndDate && Convert.ToDecimal(ddt5.Rows[ddt5.Rows.Count - 1]["Closing_Balance"]) <= Convert.ToDecimal(ddt5.Rows[ddt5.Rows.Count - 1]["Reservation_In_Bag"]))
                         {
                             ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Out of Date '); </script> ");
                         }

                         else if (Convert.ToDecimal(ddt5.Rows[ddt5.Rows.Count - 1]["Closing_Balance"]) > Convert.ToDecimal(ddt5.Rows[ddt5.Rows.Count - 1]["Reservation_In_Bag"]) && HMOverBags == 0)
                         {
                             b = ddt5.Rows.Count;
                             ddt5.Rows.Add();
                             ddt5.Rows[b]["Deposit_Date"] = EndDate;
                             ddt5.Rows[b]["Opening_Balance"] = ddt5.Rows[b - 1]["Closing_Balance"];
                             ddt5.Rows[b]["Receive_Bags"] = 0;
                             ddt5.Rows[b]["Issue_Bags"] = 0;
                             ddt5.Rows[b]["Closing_Balance"] = ddt5.Rows[b - 1]["Closing_Balance"];
                             ddt5.Rows[b]["Reservation_In_Bag"] = ddt4.Rows[b]["Reservation_In_Bag"];
                             ddt5.Rows[b]["Opening_Weight"] = 0;
                             ddt5.Rows[b]["Receive_Weight"] = 0;
                             ddt5.Rows[b]["Issue_Weight"] = 0;
                             ddt5.Rows[b]["Closing_Weight"] = 0;
                             ddt5.Rows[b]["Godown_Id"] = 0;
                             ddt5.Rows[b]["Over_Bags"] = Convert.ToDecimal(ddt5.Rows[ddt5.Rows.Count - 2]["Closing_Balance"]) - Convert.ToDecimal(ddt5.Rows[ddt5.Rows.Count - 2]["Reservation_In_Bag"]);
                             ddt5.Rows[b]["NOver_Bags"] = 0;
                             ddt5.Rows[b]["Monthly_Rate"] = ddt4.Rows[b]["Monthly_Rate"];
                             ddt5.Rows[b]["Depositor_WHR_Id"] = "";
                             ddt5.Rows[b]["Storage_Period"] = "";
                             ddt5.Rows[b]["Accrued_Period"] = "15 Days";
                             ddt5.Rows[b]["Charges"] = Convert.ToDecimal(ddt5.Rows[b]["Over_Bags"]) * Convert.ToDecimal(ddt4.Rows[b]["Monthly_Rate"]) / 2;
                         }
                         else if (Convert.ToDecimal(ddt5.Rows[ddt5.Rows.Count - 1]["Closing_Balance"]) > Convert.ToDecimal(ddt5.Rows[ddt5.Rows.Count - 1]["Reservation_In_Bag"]) && HMOverBags > 0)
                         {
                             b = ddt5.Rows.Count;
                             ddt5.Rows.Add();
                             ddt5.Rows[b]["Deposit_Date"] = EndDate;
                             ddt5.Rows[b]["Opening_Balance"] = ddt5.Rows[b - 1]["Closing_Balance"];
                             ddt5.Rows[b]["Receive_Bags"] = 0;
                             ddt5.Rows[b]["Issue_Bags"] = 0;
                             ddt5.Rows[b]["Closing_Balance"] = ddt5.Rows[b - 1]["Closing_Balance"];
                             ddt5.Rows[b]["Reservation_In_Bag"] = ddt4.Rows[b]["Reservation_In_Bag"];
                             ddt5.Rows[b]["Opening_Weight"] = 0;
                             ddt5.Rows[b]["Receive_Weight"] = 0;
                             ddt5.Rows[b]["Issue_Weight"] = 0;
                             ddt5.Rows[b]["Closing_Weight"] = 0;
                             ddt5.Rows[b]["Godown_Id"] = 0;
                             ddt5.Rows[b]["Over_Bags"] = Convert.ToDecimal(ddt5.Rows[ddt5.Rows.Count - 2]["Closing_Balance"]) - Convert.ToDecimal(ddt5.Rows[ddt5.Rows.Count - 2]["Reservation_In_Bag"]);
                             ddt5.Rows[b]["NOver_Bags"] = 0;
                             ddt5.Rows[b]["Monthly_Rate"] = ddt4.Rows[b]["Monthly_Rate"];
                             ddt5.Rows[b]["Depositor_WHR_Id"] = "";
                             ddt5.Rows[b]["Storage_Period"] = "";
                             ddt5.Rows[b]["Accrued_Period"] = "01 Month";
                             ddt5.Rows[b]["Charges"] = Convert.ToDecimal(ddt5.Rows[b]["Over_Bags"]) * Convert.ToDecimal(ddt4.Rows[b]["Monthly_Rate"]);
                         }
                     }
                 }
                 //////////////////////////////////////////////////////////
                 if (ddt5.Rows.Count > 0)
                 {
                     gvAccruedOverNAbove.DataSource = ddt5;
                     gvAccruedOverNAbove.DataBind();
                     //gvAccruedOverNAbove.HeaderRow.Cells[9].Visible = false;
                     gvAccruedOverNAbove.HeaderRow.Cells[11].Visible = false;
                     gvAccruedOverNAbove.HeaderRow.Cells[12].Visible = false;
                     gvAccruedOverNAbove.HeaderRow.Cells[13].Visible = false;
                     gvAccruedOverNAbove.HeaderRow.Cells[14].Visible = false;

                     gvAccruedOverNAbove.HeaderRow.Cells[7].Visible = false;
                     gvAccruedOverNAbove.HeaderRow.Cells[15].Visible = false;
                     for (int i = 0; i <= gvAccruedOverNAbove.Rows.Count - 1; i++)
                     {
                         //gvAccruedOverNAbove.Rows[i].Cells[9].Visible = false;
                         gvAccruedOverNAbove.Rows[i].Cells[11].Visible = false;
                         gvAccruedOverNAbove.Rows[i].Cells[12].Visible = false;
                         gvAccruedOverNAbove.Rows[i].Cells[13].Visible = false;
                         gvAccruedOverNAbove.Rows[i].Cells[14].Visible = false;

                         gvAccruedOverNAbove.Rows[i].Cells[7].Visible = false;
                         gvAccruedOverNAbove.Rows[i].Cells[15].Visible = false;
                     }
                     TrAccruedOverNAbove.Visible = true;
                     trOverAboveDaily.Visible = false;
                     trRentBill.Visible = false;
                     trReportsView.Visible = false;
                     btnAccruedAbove.Visible = true;
                     btnAccruedCancel.Visible = true;
                 }
                 else
                 {
                     ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid '); </script> ");
                 }
             }
             else
             {
                 ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid '); </script> ");
             }
         }
         catch (Exception ex)
         {
             lblmsg.Text = ex.Message;
         }
     }
     public int GetDatePeriodC(DateTime date1, DateTime date2)
     {
         DateTime fdate = new DateTime();
         DateTime tdate = new DateTime();
         fdate = date1;
         tdate = date2;
         int TodalDay = 0;
         TodalDay = Convert.ToInt32((tdate - fdate).TotalDays);
         return TodalDay;
     }
     public string  GetDateDiff(DateTime date1,DateTime date2)
     {
         string Period_Diff = "";
         DateTime fdate = new DateTime();
         DateTime tdate = new DateTime();

         string sfdate = date1.ToString("MM/dd/yyyy");

         //fdate = DateTime.Parse(sfdate);

         string stodate = date2.ToString("MM/dd/yyyy");
         tdate = DateTime.Parse(stodate);
         tdate = tdate.AddDays(1);
         string NewToDate = tdate.ToString("MM/dd/yyyy");
         //DateD(tdate, fdate);
         qry = "select dbo.udfDateDiffinYrMonDay('" + sfdate + "','" + NewToDate + "') as Period";
         SqlCommand cmd = new SqlCommand(qry, con);
         SqlDataAdapter da = new SqlDataAdapter(cmd);
         DataTable dt = new DataTable();
         da.Fill(dt);
         if (dt.Rows[0]["Period"].ToString() != "")
         {
             Period_Diff = dt.Rows[0]["Period"].ToString();
         }
         else
         { 
             Period_Diff = "";
         }
         return Period_Diff;
     }
     //public int GetMaxValue(int p1,int p2)
     //{
     //    int v1, v2,v3;
     //    v1 = p1;
     //    v2 = p2;
     //    if (v1 > v2)
     //    {
     //        v3 = v1;
     //    }
     //    else
     //    {
     //        v3 = v2;
     //    }
     //    return v3;
     //}
     public decimal GetMaxValue(decimal p1, decimal p2)
     {
         decimal v1, v2, v3;
         v1 = p1;
         v2 = p2;
         if (v1 > v2)
         {
             v3 = v1;
         }
         else
         {
             v3 = v2;
         }
         return v3;
     }
    public void GetStorageDailyChargesBillDetail()
    {
        try
        {
            DateTime LDateTime = new DateTime();
            DateTime StartDate = new DateTime();
            DateTime EndDate = new DateTime();
            StartDate = Convert.ToDateTime(getDate_MDY(txtfdate.Text));
            EndDate = Convert.ToDateTime(getDate_MDY(txttodate.Text));
            
            decimal PerDayRate = 0;
            if (txtCPRate.Text != "")
            {
                PerDayRate = Convert.ToDecimal(txtCPRate.Text);
            }
            int j = 0;
            int h = 0;
            int m = 0;
            DataTable dw = new DataTable();
            dw.Columns.AddRange(new DataColumn[]  { 
                 new DataColumn("Static_Date",typeof(DateTime)),   
              new DataColumn("Deposit_Bags",typeof(decimal),null),
                new DataColumn("Deliver_Bags",typeof(decimal)),
                new DataColumn("Deposit_Weight",typeof(decimal)),
                new DataColumn("Deliver_Weight",typeof(decimal)),
                new DataColumn("Godown_Id",typeof(string)),
                new DataColumn("Flag",typeof(string)),
            });

            DateTime Static_date = new DateTime();
            DateTime Recent_Static_date = new DateTime();
            DataTable ddt2 = new DataTable();

            ddt2.Columns.AddRange(new DataColumn[]  { 
          new DataColumn("Static_Date",typeof(DateTime),null),
            new DataColumn("Opening_Balance",typeof(decimal)),
            new DataColumn("Receive_Bags",typeof(decimal)),
            new DataColumn("Issue_Bags",typeof(decimal)),
            new DataColumn("Closing_Balance",typeof(decimal)),

            new DataColumn("Opening_Weight",typeof(decimal)),
            new DataColumn("Receive_Weight",typeof(decimal)),
            new DataColumn("Issue_Weight",typeof(decimal)),
            new DataColumn("Closing_Weight",typeof(decimal)),
        new DataColumn("Per_Day_Rate",typeof(decimal)),
        new DataColumn("Charges",typeof(decimal)),
        //new DataColumn("Weight_Charges",typeof(decimal)),
        new DataColumn("Godown_Id",typeof(string)),
        });

            DataTable ddt3 = new DataTable();
            ddt3.Columns.AddRange(new DataColumn[]  { 
          new DataColumn("Deposit_Date",typeof(DateTime),null),
            new DataColumn("Opening_Balance",typeof(decimal)),
            new DataColumn("Receive_Bags",typeof(decimal)),
            new DataColumn("Issue_Bags",typeof(decimal)),
            new DataColumn("Closing_Balance",typeof(decimal)),

             new DataColumn("Opening_Weight",typeof(decimal)),
            new DataColumn("Receive_Weight",typeof(decimal)),
            new DataColumn("Issue_Weight",typeof(decimal)),
            new DataColumn("Closing_Weight",typeof(decimal)),
        new DataColumn("Per_Day_Rate",typeof(decimal)),
        new DataColumn("Charges",typeof(decimal)),
         //new DataColumn("Weight_Charges",typeof(decimal)),
         new DataColumn("Godown_Id",typeof(string)),
         new DataColumn("Per_Day_Rate_Weight",typeof(decimal)),
        new DataColumn("Charges_Weight",typeof(decimal)),
        });

            string BranchID = Session["BranchID"].ToString();
            //string str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,delwght as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] where BranchID='" + BranchID + "' and Depositor_Name='" + ddldepos_name.SelectedItem.Text + "' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "'";
            //string str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,delwght as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] where BranchID='" + BranchID + "' and Depositor_Name='" + ddldepos_name.SelectedItem.Text + "' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='"+ ddlcropyr.SelectedItem.Text +"'";
            string str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght-Gain+Loss) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] where BranchID='" + BranchID + "' and Depositor_Name='" + ddldepos_name.SelectedItem.Text + "' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Static_date = Convert.ToDateTime(dt.Rows[0]["datecom"].ToString());
                string Static_date_MM = Static_date.ToString("MM");
                string StartDate_MM = StartDate.ToString("MM");
                if (StartDate < Static_date && Static_date_MM != "04" && StartDate_MM!="04")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid From Date(There are No Deposit/Delivery)...'); </script> ");
                    trOverAboveDaily.Visible = false;
                    trRentBill.Visible = false;
                    trReportsView.Visible = false;
                    btnGenBill.Visible = false;
                    btncancel2.Visible = false;
                }
                else
                {
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {

                            Static_date = Convert.ToDateTime(dt.Rows[i]["datecom"].ToString());

                            if (i == 0)
                            {
                                Static_date = Convert.ToDateTime(dt.Rows[i]["datecom"].ToString());
                                dw.Rows.Add();
                                //            
                                dw.Rows[h]["Static_date"] = Convert.ToDateTime(Static_date);
                                dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
                                dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
                                dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
                                dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
                                dw.Rows[h]["Godown_Id"] = dt.Rows[i]["Godown_Id"].ToString();
                                dw.Rows[h]["Flag"] = "Y";
                                Recent_Static_date = Static_date;
                                h++;

                            }
                            else if (Recent_Static_date == Static_date)
                            {
                                h--;
                                Static_date = Recent_Static_date;
                                dw.Rows.Add();

                                dw.Rows[h]["Static_date"] = Static_date;
                                dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dw.Rows[h]["Deposit_Bags"]) + Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
                                dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dw.Rows[h]["Deliver_Bags"]) + Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
                                dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dw.Rows[h]["Deposit_Weight"]) + Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
                                dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dw.Rows[h]["Deliver_Weight"]) + Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
                                if (dw.Rows[h]["Godown_Id"].ToString().Contains(dt.Rows[i]["Godown_Id"].ToString()))
                                {

                                }
                                else
                                {
                                    dw.Rows[h]["Godown_Id"] = dw.Rows[h]["Godown_Id"].ToString() + "," + dt.Rows[i]["Godown_Id"].ToString();
                                }
                                dw.Rows[h]["Flag"] = "Y";
                                Recent_Static_date = Static_date;
                                h++;
                            }
                            else if (Recent_Static_date.AddDays(1) == Static_date)
                            {

                                dw.Rows.Add();

                                dw.Rows[h]["Static_date"] = Static_date;
                                dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
                                dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
                                dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
                                dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
                                dw.Rows[h]["Godown_Id"] = dt.Rows[i]["Godown_Id"].ToString();
                                dw.Rows[h]["Flag"] = "Y";
                                Recent_Static_date = Static_date;
                                h++;
                            }

                            else if (Recent_Static_date != Static_date)
                            {
                                dw.Rows.Add();
                                Static_date = Recent_Static_date.AddDays(1);
                                dw.Rows[h]["Static_date"] = Static_date;
                                dw.Rows[h]["Deposit_Bags"] = 0;
                                dw.Rows[h]["Deliver_Bags"] = 0;
                                dw.Rows[h]["Deposit_Weight"] = 0;
                                dw.Rows[h]["Deliver_Weight"] = 0;
                                dw.Rows[h]["Godown_Id"] = "";
                                dw.Rows[h]["Flag"] = "Y";
                                h++;
                                i--;
                                Recent_Static_date = Static_date;
                            }
                            else if (Recent_Static_date == Static_date && (dt.Rows[i]["recbags"].ToString() == "" && dt.Rows[i]["delbags"].ToString() == ""))
                            {
                                dw.Rows.Add();
                                Static_date = Static_date.AddDays(1);
                                dw.Rows[h]["Static_date"] = Static_date;
                                dw.Rows[h]["Deposit_Bags"] = 0;
                                dw.Rows[h]["Deliver_Bags"] = 0;
                                dw.Rows[h]["Deposit_Weight"] = 0;
                                dw.Rows[h]["Deliver_Weight"] = 0;
                                dw.Rows[h]["Godown_Id"] = "";
                                dw.Rows[h]["Flag"] = "Y";
                                h++;
                                i--;
                                Recent_Static_date = Static_date;
                            }
                            else if (Recent_Static_date != Static_date)
                            {
                                dw.Rows.Add();
                                Static_date = Recent_Static_date.AddDays(1);
                                dw.Rows[h]["Static_date"] = Static_date;
                                dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
                                dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
                                dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
                                dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
                                dw.Rows[h]["Godown_Id"] = dt.Rows[i]["Godown_Id"].ToString();
                                dw.Rows[h]["Flag"] = "Y";
                                Recent_Static_date = Static_date;
                                h++;
                            }
                        }
                        for (int i = dt.Rows.Count - 1; i <= dt.Rows.Count + 365; i++)
                        {
                            if (i == (dt.Rows.Count - 1))
                            {
                                LDateTime = Convert.ToDateTime(dw.Rows[h - 1]["Static_date"]);
                                LDateTime = LDateTime.AddDays(1);
                            }
                            else
                            {
                                LDateTime = LDateTime.AddDays(1);
                            }
                            dw.Rows.Add();
                            dw.Rows[h]["Static_date"] = LDateTime;
                            dw.Rows[h]["Deposit_Bags"] = 0;
                            dw.Rows[h]["Deliver_Bags"] = 0;
                            dw.Rows[h]["Deposit_Weight"] = 0;
                            dw.Rows[h]["Deliver_Weight"] = 0;
                            dw.Rows[h]["Godown_Id"] = "";
                            dw.Rows[h]["Flag"] = "Y";
                            h++;
                        }
                    }
                    if (dw.Rows.Count > 0)
                    {
                        for (int k = 0; k <= dw.Rows.Count - 1; k++)
                        {
                            if (k == 0 && dw.Rows[k]["Flag"].ToString() == "Y")
                            {
                                ddt2.Rows.Add();
                                ddt2.Rows[k]["Static_Date"] = Convert.ToDateTime(dw.Rows[k]["Static_Date"]);
                                ddt2.Rows[k]["Opening_Balance"] = 0;
                                ddt2.Rows[k]["Receive_Bags"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"]);
                                ddt2.Rows[k]["Issue_Bags"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Bags"]);
                                ddt2.Rows[k]["Closing_Balance"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"]) - Convert.ToDecimal(dw.Rows[k]["Deliver_Bags"]);
                                ddt2.Rows[k]["Per_Day_Rate"] = PerDayRate;
                                ddt2.Rows[k]["Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Balance"]);

                                ddt2.Rows[k]["Opening_Weight"] = 0;
                                ddt2.Rows[k]["Receive_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]);
                                ddt2.Rows[k]["Issue_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]);
                                ddt2.Rows[k]["Closing_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]) - Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]);
                                //ddt2.Rows[k]["Weight_Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Weight"]);
                                ddt2.Rows[k]["Godown_Id"] = dw.Rows[k]["Godown_Id"].ToString();

                            }
                            else if (dw.Rows[k]["Flag"].ToString() == "Y")
                            {
                                ddt2.Rows.Add();
                                ddt2.Rows[k]["Static_Date"] = Convert.ToDateTime(dw.Rows[k]["Static_Date"]);
                                ddt2.Rows[k]["Opening_Balance"] = ddt2.Rows[k - 1]["Closing_Balance"];
                                ddt2.Rows[k]["Receive_Bags"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"]);
                                ddt2.Rows[k]["Issue_Bags"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Bags"]);
                                ddt2.Rows[k]["Closing_Balance"] = Convert.ToDecimal(ddt2.Rows[k - 1]["Closing_Balance"]) + Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"]) - Convert.ToDecimal(dw.Rows[k]["Deliver_Bags"]);
                                ddt2.Rows[k]["Per_Day_Rate"] = PerDayRate;
                                ddt2.Rows[k]["Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Balance"]);

                                ddt2.Rows[k]["Opening_Weight"] = ddt2.Rows[k - 1]["Closing_Weight"];
                                ddt2.Rows[k]["Receive_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]);
                                ddt2.Rows[k]["Issue_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]);
                                ddt2.Rows[k]["Closing_Weight"] = Convert.ToDecimal(ddt2.Rows[k - 1]["Closing_Weight"]) + Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]) - Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]);
                                //ddt2.Rows[k]["Weight_Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Weight"]);
                                ddt2.Rows[k]["Godown_Id"] = dw.Rows[k]["Godown_Id"].ToString();
                            }
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Record Not Found...!');</script>");
                    }
                    if (ddt2.Rows.Count > 0)
                    {
                        for (int l = 0; l <= ddt2.Rows.Count - 1; l++)
                        {
                            DateTime dt1 = Convert.ToDateTime(ddt2.Rows[l]["Static_Date"]);
                            DateTime dt2 = Convert.ToDateTime(ddt2.Rows[ddt2.Rows.Count - 1]["Static_Date"]);
                            if (dt1 >= StartDate && dt1 <= EndDate)
                            {

                                ddt3.Rows.Add();
                                ddt3.Rows[m]["Deposit_Date"] = Convert.ToDateTime(ddt2.Rows[l]["Static_Date"]);
                                ddt3.Rows[m]["Opening_Balance"] = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
                                ddt3.Rows[m]["Receive_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Receive_Bags"]);
                                ddt3.Rows[m]["Issue_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Issue_Bags"]);
                                ddt3.Rows[m]["Closing_Balance"] = Convert.ToDecimal(ddt2.Rows[l]["Closing_Balance"]);
                                ddt3.Rows[m]["Per_Day_Rate"] = Convert.ToDecimal(ddt2.Rows[l]["Per_Day_Rate"]);
                                ddt3.Rows[m]["Charges"] = Convert.ToDecimal(ddt2.Rows[l]["Charges"]);

                                ddt3.Rows[m]["Opening_Weight"] = (Convert.ToDecimal(ddt2.Rows[l]["Opening_Weight"])/10);
                                ddt3.Rows[m]["Receive_Weight"] = (Convert.ToDecimal(ddt2.Rows[l]["Receive_Weight"])/10);
                                ddt3.Rows[m]["Issue_Weight"] = (Convert.ToDecimal(ddt2.Rows[l]["Issue_Weight"])/10);
                                ddt3.Rows[m]["Closing_Weight"] = (Convert.ToDecimal(ddt2.Rows[l]["Closing_Weight"])/10);
                                //ddt3.Rows[m]["Weight_Charges"] = Convert.ToDecimal(ddt2.Rows[l]["Weight_Charges"]);
                                ddt3.Rows[m]["Godown_Id"] = dw.Rows[l]["Godown_Id"].ToString();
                                //New
                                ddt3.Rows[m]["Per_Day_Rate_Weight"] = Convert.ToDecimal(txtCPRate.Text);
                                decimal PerDayWeightCharges = ((Convert.ToDecimal(ddt2.Rows[l]["Closing_Weight"])/10) * Convert.ToDecimal(txtCPRate.Text));

                                ddt3.Rows[m]["Charges_Weight"] = Convert.ToDecimal(PerDayWeightCharges);
                                m = m + 1;
                            }
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Record Not Found...!');</script>");
                    }
                    if (ddt3.Rows.Count > 0)
                    {
                        gvIStorageCharge.DataSource = ddt3;
                        gvIStorageCharge.DataBind();
                        if (ddlBType.SelectedValue.ToString() == "B" && ddlBType.SelectedValue.ToString() != "-1")
                        {
                            gvIStorageCharge.HeaderRow.Cells[7].Visible = false;
                            gvIStorageCharge.HeaderRow.Cells[8].Visible = false;
                            gvIStorageCharge.HeaderRow.Cells[9].Visible = false;
                            gvIStorageCharge.HeaderRow.Cells[10].Visible = false;
                            gvIStorageCharge.HeaderRow.Cells[11].Visible = false;
                            gvIStorageCharge.HeaderRow.Cells[12].Visible = false;
                            gvIStorageCharge.HeaderRow.Cells[13].Visible = false;
                        }
                        else if (ddlBType.SelectedValue.ToString() == "W" && ddlBType.SelectedValue.ToString() != "-1")
                        {
                            gvIStorageCharge.HeaderRow.Cells[7].Visible = false;
                            //gvIStorageCharge.HeaderRow.Cells[8].Visible = true;
                            //gvIStorageCharge.HeaderRow.Cells[9].Visible = true;
                            //gvIStorageCharge.HeaderRow.Cells[10].Visible = true;
                            //gvIStorageCharge.HeaderRow.Cells[11].Visible = true;
                            //gvIStorageCharge.HeaderRow.Cells[12].Visible = true;
                            //gvIStorageCharge.HeaderRow.Cells[13].Visible = true;

                            gvIStorageCharge.HeaderRow.Cells[1].Visible = false;
                            gvIStorageCharge.HeaderRow.Cells[2].Visible = false;
                            gvIStorageCharge.HeaderRow.Cells[3].Visible = false;
                            gvIStorageCharge.HeaderRow.Cells[4].Visible = false;
                            gvIStorageCharge.HeaderRow.Cells[5].Visible = false;
                            gvIStorageCharge.HeaderRow.Cells[6].Visible = false;
;
                        }
                        for (int i = 0; i <= gvIStorageCharge.Rows.Count - 1; i++)
                        {
                          
                            if (ddlBType.SelectedValue.ToString() == "B" && ddlBType.SelectedValue.ToString() != "-1")
                            {
                                gvIStorageCharge.Rows[i].Cells[7].Visible = false;
                                gvIStorageCharge.Rows[i].Cells[8].Visible = false;
                                gvIStorageCharge.Rows[i].Cells[9].Visible = false;
                                gvIStorageCharge.Rows[i].Cells[10].Visible = false;
                                gvIStorageCharge.Rows[i].Cells[11].Visible = false;
                                gvIStorageCharge.Rows[i].Cells[12].Visible = false;
                                gvIStorageCharge.Rows[i].Cells[13].Visible = false;
                            }
                            else if (ddlBType.SelectedValue.ToString() == "W" && ddlBType.SelectedValue.ToString() != "-1")
                            {
                                gvIStorageCharge.Rows[i].Cells[7].Visible = false;
                                gvIStorageCharge.Rows[i].Cells[1].Visible = false;
                                gvIStorageCharge.Rows[i].Cells[2].Visible = false;
                                gvIStorageCharge.Rows[i].Cells[3].Visible = false;
                                gvIStorageCharge.Rows[i].Cells[4].Visible = false;
                                gvIStorageCharge.Rows[i].Cells[5].Visible = false;
                                gvIStorageCharge.Rows[i].Cells[6].Visible = false;
                               
                            }
                        }
                        trRentBill.Visible = true;
                        trReportsView.Visible = false;
                        btnGenBill.Visible = true;
                        btncancel2.Visible = true;
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Record Not Found...!');</script>");
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(),"mymsg2","<script language=javascript>alert('Record Not Found...!');</script>");
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    protected void btnAccruedBill_Click(object sender, EventArgs e)
    {
        Insert_Accrued_Bill_WHR_Detail();
        Insert_Bill_Detail();
        Accrued_Storage_Bill_Details();
    }
    protected void btnSetTax_Click(object sender, EventArgs e)
    {
        if (gvAccruedBill.Rows.Count > 0)
        {
            Update_gvAccruedBill();
        }
        GetAccruedBillDetail();
    }
    public void Get_Tax()
    {
        try
        {
            //qry = "select Service_Tax,TDS,Remarks from Tax_Master";
            qry = "select SUM(SGST+CGST) as GST from tbl_MetaData_GST";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtstax.Text = Convert.ToDecimal(dt.Rows[0]["GST"]).ToString();
                //txttds.Text = dt.Rows[0]["TDS"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = ex.Message;
        }
    }
    public void Update_gvAccruedBill()
    {
        string Deposit_Date = "";
        string Deliver_Date = "";
        string Commodity_Name = "";
        string WHR_No = "";
        decimal Deposit_Bags = 0;
        decimal Deliver_Bags = 0;
        string Storage_Period = "";
        string Accrued_Period = "";
        decimal Monthly_Rate = 0;
        //decimal Per_Day_Rate = 0;
        //Per_Day_Rate = Convert.ToDecimal(txtCPRate.Text);  
        decimal Total_Charges = 0;
        decimal STax_Per = 0;
        decimal STax_Amt = 0;
        decimal Rebate_Per = 0;
        decimal Rebate_Amt = 0;
        decimal Net_Amounts = 0;
        if (gvAccruedBill.Rows.Count > 0)
        {
            for (int i = 0; i <= gvAccruedBill.Rows.Count - 1; i++)
            {
                Commodity_Name = gvAccruedBill.Rows[i].Cells[0].Text.ToString();
                WHR_No = gvAccruedBill.Rows[i].Cells[1].Text.ToString();
                Deposit_Date = getDate_MDY(gvAccruedBill.Rows[i].Cells[2].Text.ToString());
                Deposit_Bags = Convert.ToDecimal(gvAccruedBill.Rows[i].Cells[3].Text);
                Deliver_Date = getDate_MDY(gvAccruedBill.Rows[i].Cells[4].Text.ToString());
                Deliver_Bags = Convert.ToDecimal(gvAccruedBill.Rows[i].Cells[5].Text);
                Storage_Period = gvAccruedBill.Rows[i].Cells[6].Text.ToString();
                Accrued_Period = gvAccruedBill.Rows[i].Cells[7].Text.ToString();
                Monthly_Rate = Convert.ToDecimal(gvAccruedBill.Rows[i].Cells[8].Text);
                Total_Charges = Convert.ToDecimal(gvAccruedBill.Rows[i].Cells[9].Text);
                Rebate_Per = Convert.ToDecimal(((TextBox)gvAccruedBill.Rows[i].FindControl("txtRebate")).Text.ToString());
                Rebate_Amt = (Total_Charges * Rebate_Per) / 100;
                STax_Per = Convert.ToDecimal(((TextBox)gvAccruedBill.Rows[i].FindControl("txtSTax")).Text.ToString());
                STax_Amt = (Total_Charges * STax_Per) / 100;
                Net_Amounts = Total_Charges + STax_Amt - Rebate_Amt;
            }
        }
    }
    protected void ddlweight_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlweight.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Weight...'); </script> ");
        }
        else if (ddldepositor.SelectedItem.Text == "--Select--" || ddldepos_name.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Depositor...'); </script> ");
        }
        else if (ddlBillType.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Bill Type...'); </script> ");
        }
        else if (ddlCalcTy.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Calculation Type...'); </script> ");
        }
        //else if ((ddlBillType.SelectedValue.ToString() == "1" || ddlBillType.SelectedValue.ToString() == "3") && (ddlCalcTy.SelectedValue.ToString() == "1"||ddlCalcTy.SelectedValue.ToString() == "3"))
        //{
        //    if (ddlverity.SelectedItem.Text == "--Select--")
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity Type...'); </script> ");
        //    }
        //    else if (ddlcomodity.SelectedItem.Text == "--Select--")
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity...'); </script> ");
        //    }
        //    else if (txtfdate.Text == "")
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select From Date...'); </script> ");
        //    }
        //    else if (txttodate.Text == "")
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select To Date...'); </script> ");
        //    }
        //    else if (ddlpacktype.SelectedItem.Text == "--Select--")
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Packing Type...'); </script> ");
        //    }
        //    else if (txtfdate.Text == "" || ddlweight.SelectedItem.Text == "--Select--" || ddlpacktype.SelectedItem.Text == "--Select--" || ddlverity.SelectedItem.Text == "--Select--" || ddlcomodity.SelectedItem.Text == "--Select--")
        //    {
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Commomdity/Date/Packing type...'); </script> ");
        //    }
        //    else if (gvReservation.Visible != true)
        //    {
        //        GetRate();
        //        //txtstax.Text = (14).ToString();
        //        Get_Tax();
        //    }
        //    else
        //    {
        //        GetRate();
        //        txtstax.Text = (0).ToString();
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Correct Bill Type...'); </script> ");
        //    }
        //}
        else if (ddlBillType.SelectedValue.ToString() == "2" && ddlCalcTy.SelectedValue.ToString() == "2")
        {
            if (ddlverity.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity Type...'); </script> ");
            }
            else if (ddlcomodity.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity...'); </script> ");
            }
            else if (txtfdate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select From Date...'); </script> ");
            }
            else if (txttodate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select To Date...'); </script> ");
            }
            else if (ddlpacktype.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Packing Type...'); </script> ");
            }
            else
            {
                Clear_Rebate();
                txtcomrate.Text = "";
                txtrunit.Visible = true;
                lblrunit.Visible = true;
                lbldisc.Visible = true;
                txtdisc.Visible = true;
                lblmonth.Visible = true;
                lblNetAmt.Visible = true;
                txtNetAmt.Visible = true;
                lblmonth.Visible = true;
                lblm1.Visible = true;
                lblremark.Visible = true;
                txtremark.Visible = true;
                btnAddmore.Visible = true;

                txtstax.Text = "0";
                txtdisc.Text = "0";
                GetRate();
                GetPeriods();
            }
       }
    }
    public void Report_Accrued_Over_N_Above_Bill()
    {
        string BranchID = Session["BranchID"].ToString();
        trReportsView.Visible = true;
        trRentBill.Visible = false;
        trOverAboveDaily.Visible = false;
        TrAccruedOverNAbove.Visible = false;
        ReportViewer_SC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string serverFullName = null;
            string path = Request.Url.ToString();
            int index = path.IndexOf(":") + 3;
            string path2 = path.Substring(index);
            int index2 = path2.IndexOf("/");
            int index3 = path2.IndexOf("/", index2 + 1);
            string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();

            if (index3 > 0)

                serverFullName = path2.Substring(0, index3);
            else
                serverFullName = servername;

            ReportViewer_SC.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            ReportViewer_SC.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            ReportViewer_SC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer_SC.ServerReport.ReportServerUrl = new Uri(reportURL);

            Bill_No = ViewState["BillNo"].ToString();
            decimal Comm_Rate = Convert.ToDecimal(txtcomrate.Text);
            ChargeOfTotal = Convert.ToDecimal(ViewState["ChargeOfTotal"]);
            ReportViewer_SC.ServerReport.ReportPath = folder + "/" + "rptOverNAboveAccruedBills";
            ReportViewer_SC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            ReportViewer_SC.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[3];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Branch_Id";
            reportParameterCollection[0].Values.Add(BranchID);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Bill_No";
            reportParameterCollection[1].Values.Add(Bill_No);
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "NetAmountWord";
            reportParameterCollection[2].Values.Add(NetAmountWord);
            ReportViewer_SC.ServerReport.SetParameters(reportParameterCollection);
            ReportViewer_SC.ServerReport.Refresh();

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    public void Report_Over_N_Above_Bill_Details()
    {
        string BranchID = Session["BranchID"].ToString();
        trReportsView.Visible = true;
        trRentBill.Visible = false;
        trOverAboveDaily.Visible = false;
        ReportViewer_SC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string serverFullName = null;
            string path = Request.Url.ToString();
            int index = path.IndexOf(":") + 3;
            string path2 = path.Substring(index);
            int index2 = path2.IndexOf("/");
            int index3 = path2.IndexOf("/", index2 + 1);
            string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();

            if (index3 > 0)

                serverFullName = path2.Substring(0, index3);
            else
                serverFullName = servername;

            ReportViewer_SC.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            ReportViewer_SC.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            ReportViewer_SC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer_SC.ServerReport.ReportServerUrl = new Uri(reportURL);

            Bill_No = ViewState["BillNo"].ToString();
            decimal Comm_Rate = Convert.ToDecimal(txtcomrate.Text);
            ChargeOfTotal = Convert.ToDecimal(ViewState["ChargeOfTotal"]);
            ReportViewer_SC.ServerReport.ReportPath = folder + "/" + "rptOverNAboveDailyBill";
            ReportViewer_SC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            ReportViewer_SC.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[3];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Branch_Id";
            reportParameterCollection[0].Values.Add(BranchID);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Bill_No";
            reportParameterCollection[1].Values.Add(Bill_No);
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "NetAmountWord";
            reportParameterCollection[2].Values.Add(NetAmountWord);
            ReportViewer_SC.ServerReport.SetParameters(reportParameterCollection);
            ReportViewer_SC.ServerReport.Refresh();

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    public void Report_Storage_Bill_Daily_Details()
    {
        string BranchID = Session["BranchID"].ToString();
        trReportsView.Visible = true;
        trRentBill.Visible = false;
        ReportViewer_SC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string serverFullName = null;
            string path = Request.Url.ToString();
            int index = path.IndexOf(":") + 3;
            string path2 = path.Substring(index);
            int index2 = path2.IndexOf("/");
            int index3 = path2.IndexOf("/", index2 + 1);
            string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();

            if (index3 > 0)

                serverFullName = path2.Substring(0, index3);
            else
                serverFullName = servername;

            ReportViewer_SC.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            ReportViewer_SC.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            ReportViewer_SC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer_SC.ServerReport.ReportServerUrl = new Uri(reportURL);

            Bill_No = ViewState["BillNo"].ToString();
            decimal Comm_Rate = Convert.ToDecimal(txtcomrate.Text);
            ChargeOfTotal = Convert.ToDecimal(ViewState["ChargeOfTotal"]);
            if (ddlBType.SelectedValue.ToString() == "B" && ddlBType.SelectedValue.ToString() != "-1")
            {
                ReportViewer_SC.ServerReport.ReportPath = folder + "/" + "rptStorageChargesDailyBill";
            }
            else if (ddlBType.SelectedValue.ToString() == "W" && ddlBType.SelectedValue.ToString() != "-1")
            {
                ReportViewer_SC.ServerReport.ReportPath = folder + "/" + "Bill_InstitutionDailyChargesOnMT";
            }
            ReportViewer_SC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            ReportViewer_SC.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[3];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Branch_Id";
            reportParameterCollection[0].Values.Add(BranchID);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Bill_No";
            reportParameterCollection[1].Values.Add(Bill_No);
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "NetAmountWord";
            reportParameterCollection[2].Values.Add(NetAmountWord);
            ReportViewer_SC.ServerReport.SetParameters(reportParameterCollection);
            ReportViewer_SC.ServerReport.Refresh();

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    protected void txtrunit_TextChanged(object sender, EventArgs e)
    {

        if (ddlBillType.SelectedValue == "2" && ddlCalcTy.SelectedValue.ToString() == "2")
        {
            if (txtrunit.Text != "")
            {
                GetPeriod();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter bags to Reservation...'); </script> ");
            }
        }
    }
    protected void btnOCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void btnOSubmit_Click(object sender, EventArgs e)
    {
        Over_N_Above_Bill_Daily_Detail();
         Insert_Bill_Detail();
        Report_Over_N_Above_Bill_Details();
    }
    protected void btncancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void btnAccruedCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void ddldepos_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        trRentBill.Visible = false;
        trReportsView.Visible = false;
        trReservationBill.Visible = false;
        trOverAboveDaily.Visible = false;
    }
    protected void btnAccruedAbove_Click(object sender, EventArgs e)
    {
        Over_N_Above_Accrued_Bill_Detail();
        Insert_Bill_Detail();
        Report_Accrued_Over_N_Above_Bill();
    }
    protected void btnCanAcrAbove_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void ddlcropyr_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlweight.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Weight...'); </script> ");
        }
        else if (ddldepositor.SelectedItem.Text == "--Select--" || ddldepos_name.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Depositor...'); </script> ");
        }
        else if (ddlBillType.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Bill Type...'); </script> ");
        }
        else if (ddlCalcTy.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Calculation Type...'); </script> ");
        }
        else
        {
            GetRate();
            //txtstax.Text = (14).ToString();
            Get_Tax();
        }
    }
   
}

