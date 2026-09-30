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
using System.Security.Principal;

public partial class Accounting_frm_Godown_Rent_Bill_Deduction : System.Web.UI.Page
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
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                //fillFinancialYear();
                GetGodown();

            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void GetGodown()
    {
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string sid = Session["BranchID"].ToString();
        ddlgodown.Items.Clear();
        //if (ddlGodownType.SelectedValue.ToString() == "1")
        //{
        //qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN where DistrictId='23" + Dist_id + "' and DepotId='" + Session["BranchID"].ToString() + "'";
        //qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type not in ('Joint Venture(JV)','JointVenture(JV)','WDRA') and Remarks='Y'";
        qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "'";
        //}
        //else if (ddlGodownType.SelectedValue.ToString() == "2")
        //{
        //    qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type in ('Hired') and Remarks='Y'";
        //}
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlgodown.Items.Insert(0, "--Select--");
        }
        else
        {
            //if (ddlGodownType.SelectedValue.ToString() == "1")
            //{
            //    ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");
            //}
            //else if (ddlGodownType.SelectedValue.ToString() == "2")
            //{
            //ddlGodown2.DataSource = ds.Tables[0];
            //ddlGodown2.DataTextField = "Godown_Name";
            //ddlGodown2.DataValueField = "Godown_ID";
            //ddlGodown2.DataBind();
            //ddlGodown2.Items.Insert(0, "--Select--");

            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");
            //}
        }
    }
    void GetCommodity()
    {

        //string verity = ddlverity.SelectedValue;
        //qry = "select Commodity_ID,Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY_RList where Rep_Grp_Code='" + verity + "'";
        //qry = "select distinct Commodity_Id,Commodity_Name from View_WHRcurrentstock where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
        qry = "select distinct Commodity_Id,Commodity_Name from View_WHRcurrentstock where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Depositor_Name='MPSCSC'";
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
    void GetBillList()
    {
        ddlBill.Items.Clear();
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string sid = Session["BranchID"].ToString();
        //qry = "select distinct Bill_Number from tbl_Storage_Bill_Details where Branch_Id='" + sid + "' and Commodity_Id='"+ ddlcomodity.SelectedValue +"' and Godown_Id='" + ddlgodown.SelectedValue.ToString() + "'";
        qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Branch_Id='" + sid + "' and Commodity_Id='" + ddlcomodity.SelectedValue + "' and Godown_Id='" + ddlgodown.SelectedValue.ToString() + "'";

        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlBill.DataSource = ds.Tables[0];
            ddlBill.DataTextField = "Bill_Number";
            ddlBill.DataValueField = "Bill_Number";
            ddlBill.DataBind();
            ddlBill.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCommodity();
    }
    protected void ddlcomodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillList();
    }
    protected void ddlBill_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string sid = Session["BranchID"].ToString();
        //string str = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Storage_Bill_Details.Godown_Id)+'('+Godown_Id+')' as Godown,Depositor_Category from tbl_Storage_Bill_Details where Commodity_Id='" + ddlcomodity.SelectedValue + "' and Branch_Id='" + sid + "' and Bill_Number='" + ddlBill.SelectedItem.Text + "'";
        string str = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+Godown_Id+')' as Godown,Depositor_Category from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + ddlcomodity.SelectedValue + "' and Branch_Id='" + sid + "' and Bill_Number='" + ddlBill.SelectedItem.Text + "'";

        da = new SqlDataAdapter(str, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            lblMonth.Text = ds.Tables[0].Rows[0]["Bill_Month"].ToString();
            //txtCropYear.Text = ds.Tables[0].Rows[0]["Crop_Year"].ToString();
            lblRPM.Text = ds.Tables[0].Rows[0]["Rate_PM"].ToString();
            //txtBillAmt.Text = ds.Tables[0].Rows[0]["Sub_Amount"].ToString();
            //txtGSTAmt.Text = ds.Tables[0].Rows[0]["GST_Amount"].ToString();
            txtBilAmt.Text = ds.Tables[0].Rows[0]["Sub_Amount"].ToString();
            //txtGodown.Text = ds.Tables[0].Rows[0]["Godown"].ToString();
            //string Godown_Owner = ds.Tables[0].Rows[0]["Depositor_Category"].ToString();
            //if (Godown_Owner != "MPWLC")
            //{

            //}
        }
    }
    protected void btnSumbmitRent_Click(object sender, EventArgs e)
    {
        Insert_Godown_Rent_Deduction();
    }
    public void Insert_Godown_Rent_Deduction()
    {
            decimal Total_Bill_Deduction_Amt = 0;
            decimal Net_Bill_Amt = 0;
            Total_Bill_Deduction_Amt = (Convert.ToDecimal(txtTDSAmt.Text) + Convert.ToDecimal(txtGainDeductAmt.Text) + Convert.ToDecimal(txtTotalDeduction.Text));
            Net_Bill_Amt = Convert.ToDecimal(txtBilAmt.Text) - Total_Bill_Deduction_Amt;
            string DistrictId = Session["Depot_DistID"].ToString();
            string BranchId = Session["BranchId"].ToString();
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            bool Common_Facility=false;
            if (rdoYes.Checked == true)
            {
                Common_Facility = true;
            }
            else if (rdoNo.Checked == true)
            {
                Common_Facility = false;
            }
            string qry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Resources_Availability]([Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS],[Elect_Beam_Scale],[Wooden_Planke],[Ana_Kit_Set],[Fumigation_Cover],[Fire_Extinguisher],[Fire_Buckets],[Security_Guard],[Sprey_Pump],[Digital_Moisture_Meter],[Fumigation_Emloyee],[Common_Facility],[Insectiside_Quantity],[Godown_Closing_Balance],[Total_Stock_Deposit],CreatedBy,CreatedDate) VALUES ('" + ddlBill.SelectedItem.Text + "','" + DistrictId + "','" + BranchId + "','" + ddlgodown.SelectedValue + "','" + txt1.Text + "','" + TextBox4.Text + "','" + TextBox13.Text + "','" + TextBox14.Text + "','" + TextBox15.Text + "','" + TextBox16.Text + "','" + TextBox17.Text + "','" + TextBox18.Text + "','" + TextBox19.Text + "','" + TextBox20.Text + "','" + TextBox21.Text + "','" + Common_Facility + "','" + TextBox366.Text + "','" + txtBillClosingBalance.Text + "','" + txtGodownBalance.Text + "','" + ip + "',GETDATE())";
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            int c = cmd.ExecuteNonQuery();
            con.Close();
            if (c > 0)
            {
                string qry2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Resources_Deduction]([Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS],[Elect_Beam_Scale],[Wooden_Planke],[Ana_Kit_Set],[Fumigation_Cover],[Fire_Extinguisher],[Fire_Buckets],[Security_Guard],[Sprey_Pump],[Digital_Moisture_Meter],[Fumigation_Emloyee],[Common_Facility],[Insectiside_Quantity],[Godown_Closing_Balance],[Total_Stock_Deposit],CreatedBy,CreatedDate) VALUES ('" + ddlBill.SelectedItem.Text + "','" + DistrictId + "','" + BranchId + "','" + ddlgodown.SelectedValue + "','" + lblDP.Text + "','" + TextBox24.Text + "','" + TextBox25.Text + "','" + TextBox26.Text + "','" + TextBox23.Text + "','" + TextBox27.Text + "','" + TextBox28.Text + "','" + TextBox29.Text + "','" + TextBox30.Text + "','" + TextBox31.Text + "','" + TextBox32.Text + "','" + Common_Facility + "','" + TextBox366.Text + "','" + txtBillClosingBalance.Text + "','" + txtGodownBalance.Text + "','" + ip + "',GETDATE())";
                SqlCommand cmd2 = new SqlCommand(qry2, con);
                con.Open();
                int c2 = cmd2.ExecuteNonQuery();
                con.Close();
                if (c2 > 0)
                {
                    string qry3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Amount]([Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],TBill_Amount,Net_Bill_Amount,CreatedBy,CreatedDate) VALUES ('" + ddlBill.SelectedItem.Text + "','" + DistrictId + "','" + BranchId + "','" + ddlgodown.SelectedValue + "','" + txtDP.Text + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox7.Text + "','" + TextBox8.Text + "','" + TextBox9.Text + "','" + TextBox10.Text + "','" + TextBox11.Text + "','" + TextBox12.Text + "','" + TextBox37.Text + "','" + txtTotalDeduction.Text + "','" + Total_Bill_Deduction_Amt + "','" + txtBilAmt.Text + "','" + Net_Bill_Amt + "','" + ip + "',GETDATE())";
                    SqlCommand cmd3 = new SqlCommand(qry3, con);
                    con.Open();
                    int c3 = cmd3.ExecuteNonQuery();
                    con.Close();
                    if (c3 > 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Saved Successfully..'); </script> ");
                    }
                }
            }
    }

}
