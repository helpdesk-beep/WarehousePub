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

public partial class Region_Gdwn_GeneratedBill_Detuction : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string qry;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            if (!IsPostBack)
            {
                GetBranch();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    public void GetBillData()
    {
    }

    public void GetBillOtherData()
    {
        if (ddlGodownType.SelectedItem.Text != "Silo Bags")
        {
            qry = " select (CONVERT(varchar(10),b.BId)+'/'+CONVERT(varchar(10),b.Created_Date,105)) as ReceiptNo,b.Bill_Number,b.District_Id,b.Branch_Id,mg.Godown_ID,SUBSTRING(mg.Godown_ID,8,3) as Godown_No,mg.Godown_APN as Godown_Owner,mg.Godown_Name,cast((mg.Godown_Scientific_Capacity/10) as int) as Godown_Scientific_Capacity,mg.Hired_Type,b.Commodity_Id,(select a.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as a where a.Commodity_Id=b.Commodity_Id) as Commodity_Name, (SELECT DateName(mm,DATEADD(mm,b.Month,-1)) as [MonthName]) as Month,CONVERT(varchar(10),b.From_Date,103) as FromDate,CONVERT(varchar(10),b.To_Date,103) as ToDate,b.Financial_Year,b.Commodity_Rate, CONVERT(decimal(18,0), b.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),b.Sub_Amount) as Sub_Amount,b.Service_Tax_Perc,b.Service_Tax_Amt,b.Rebate_Perc,b.Rebate_Amt,b.Per_Day_Rate,b.Crop_Year from tbl_Institution_Storage_Bill_Details as b inner join tbl_MetaData_GODOWN_2018 as mg on (mg.Godown_ID=b.Godown_Id)  where b.Bill_Number=@BillNumber";
        }
        else
        {
            qry = " select (CONVERT(varchar(10),b.BId)+'/'+CONVERT(varchar(10),b.Created_Date,105)) as ReceiptNo,b.Bill_Number,b.District_Id,b.Branch_Id,mg.Godown_ID,SUBSTRING(mg.Godown_ID,8,3) as Godown_No,mg.Godown_APN as Godown_Owner,mg.Godown_Name,cast((mg.Godown_Scientific_Capacity/10) as int) as Godown_Scientific_Capacity,mg.Hired_Type,b.Commodity_Id,(select a.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as a where a.Commodity_Id=b.Commodity_Id) as Commodity_Name, (SELECT DateName(mm,DATEADD(mm,b.Month,-1)) as [MonthName]) as Month,CONVERT(varchar(10),b.From_Date,103) as FromDate,CONVERT(varchar(10),b.To_Date,103) as ToDate,b.Financial_Year,b.Commodity_Rate, CONVERT(decimal(18,0), b.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),b.Sub_Amount) as Sub_Amount,b.Service_Tax_Perc,b.Service_Tax_Amt,b.Rebate_Perc,b.Rebate_Amt,b.Per_Day_Rate,b.Crop_Year from tbl_Institution_Storage_Bill_Details as b inner join tbl_MetaData_GODOWN_2018 as mg on (mg.Godown_ID=b.Godown_Id)  where b.Bill_Number=@BillNumber";
        }

        SqlCommand cmd = new SqlCommand(qry, con);
        string selectedBillNo = (ddlGodownType.SelectedItem.Text != "Silo Bags") ? ddlbillno.SelectedValue : ddlactualbill.SelectedValue;
        cmd.Parameters.AddWithValue("@BillNumber", selectedBillNo ?? string.Empty);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            lblCPYear.Text = HttpUtility.HtmlEncode(dt.Rows[0]["Commodity_Rate"].ToString());
            lblrmmonth.Text = HttpUtility.HtmlEncode(dt.Rows[0]["Month"].ToString());
            lblrmcmd.Text = HttpUtility.HtmlEncode(dt.Rows[0]["Commodity_Name"].ToString() + "  " + dt.Rows[0]["Crop_Year"].ToString());
            txtBillAmt.Text = HttpUtility.HtmlEncode(dt.Rows[0]["Sub_Amount"].ToString());
            lblfrmdate.Text = HttpUtility.HtmlEncode(dt.Rows[0]["FromDate"].ToString());
            lbltodate.Text = HttpUtility.HtmlEncode(dt.Rows[0]["ToDate"].ToString());
            lblfinancial.Text = HttpUtility.HtmlEncode(dt.Rows[0]["Financial_Year"].ToString());
        }
        else
        {
            lblCPYear.Text = "";
            lblrmmonth.Text = "";
            lblrmcmd.Text = "";
            txtBillAmt.Text = "0";
            lblfrmdate.Text = "";
            lbltodate.Text = "";
            lblfinancial.Text = "";
        }
    }

    public void GetBillDetuctionData()
    {
        qry = "select CONVERT(decimal(18,0), TResources_Deduct_Amt) as TResources_Deduct_Amt,TBill_Deduct_Amt,TBill_Amount,Net_Bill_Amount from tbl_Godown_Rent_Deduction_Amount where Bill_No = @BillNo";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.Parameters.AddWithValue("@BillNo", ddlbillno.SelectedValue ?? string.Empty);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            txtresdetAmt.Text = HttpUtility.HtmlEncode(dt.Rows[0]["TResources_Deduct_Amt"].ToString());
        }
        else
        {
            txtresdetAmt.Text = "0";
        }
    }

    public void GetBranch()
    {
        string qry = "select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId in (select District_ID from tbl_metadata_district where Region_ID=@RegionID) order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.Parameters.AddWithValue("@RegionID", Session["Region_ID"] != null ? Session["Region_ID"].ToString() : string.Empty);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddlgdwn_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetActualBilNo();
    }

    public void GetActualBilNo()
    {
        string qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Godown_Id=@GodownID and Bill_Number not in (select D.Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM as D where D.GodownID=@GodownID) and Bill_Number in (select CMS.Ref_Bill_No from MPSCSC.dbo.Digitally_Sing_StorageBill as CMS where CMS.Godown_Id=@GodownID)";

        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.Parameters.AddWithValue("@GodownID", ddlgdwn.SelectedValue ?? string.Empty);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            trdet.Visible = true;
            ddlactualbill.DataSource = ds.Tables[0];
            ddlactualbill.DataTextField = "Bill_Number";
            ddlactualbill.DataValueField = "Bill_Number";
            ddlactualbill.DataBind();
            ddlactualbill.Items.Insert(0, "--Select--");
        }
        else
        {
            trdet.Visible = false;
            ddlactualbill.DataSource = null;
            ddlactualbill.DataBind();
        }
    }

    protected void ddlbillno_SelectedIndexChanged(object sender, EventArgs e)
    {
        trdet.Visible = true;
        GetBillData();
        GetBillOtherData();
        GetBillDetuctionData();

        decimal billAmt = 0;
        decimal.TryParse(txtBillAmt.Text, out billAmt);

        txtTDSAmt.Text = Convert.ToString(Math.Round((billAmt * 10) / 100));

        if (ddlGodownType.SelectedItem.Text != "Silo Bags")
        {
            txtGainAmt.Text = Convert.ToString(Math.Round((billAmt * 20) / 100));
        }
        else
        {
            txtGainAmt.Text = "0";
        }
        getcalculation();
    }

    public void getcalculation()
    {
        if (!string.IsNullOrEmpty(txtBillAmt.Text))
        {
            decimal billAmt = 0, resdetAmt = 0, tdsAmt = 0, gainAmt = 0, scrDep = 0, otherDet = 0, prastutAmt = 0;

            decimal.TryParse(txtBillAmt.Text, out billAmt);
            decimal.TryParse(txtresdetAmt.Text, out resdetAmt);
            decimal.TryParse(txtTDSAmt.Text, out tdsAmt);
            decimal.TryParse(txtGainAmt.Text, out gainAmt);
            decimal.TryParse(txtscrDep.Text, out scrDep);
            decimal.TryParse(txtotherdetuction.Text, out otherDet);
            decimal.TryParse(lblPrastut_netamount.Text, out prastutAmt);

            lblFinalTDS.Text = HttpUtility.HtmlEncode(txtTDSAmt.Text);
            lblgrandtotal.Text = Convert.ToString(Math.Round(billAmt - resdetAmt - tdsAmt - gainAmt - scrDep - otherDet));
            lbltotDetuc.Text = Convert.ToString(Math.Round(resdetAmt + gainAmt + scrDep + otherDet));
            lblMPWLCPartAmt.Text = Convert.ToString(Math.Round(prastutAmt - billAmt));
        }
    }

    protected void btnGenerateBill_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

        if (ddlactualbill.SelectedItem == null || ddlactualbill.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown...')", true);
        }
        else if (ddlBranch.SelectedItem == null || ddlBranch.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Branch...')", true);
        }
        else if (ddlbillno.SelectedItem == null || ddlbillno.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown Rent Bill...')", true);
        }
        else if (string.IsNullOrEmpty(txtBillAmt.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Bill Amount Not Blank...')", true);
        }
        else if (string.IsNullOrEmpty(lblMPWLCPartAmt.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('MPWLC Part Amount Not Blank...')", true);
        }
        else if (string.IsNullOrEmpty(lblPrastut_netamount.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Storage Charges Amount Not Blank..')", true);
        }
        else
        {
            decimal prastut = 0, totDetuc = 0, finalTDS = 0, grandTotal = 0, mpwlcPart = 0;
            decimal.TryParse(lblPrastut_netamount.Text, out prastut);
            decimal.TryParse(lbltotDetuc.Text, out totDetuc);
            decimal.TryParse(lblFinalTDS.Text, out finalTDS);
            decimal.TryParse(lblgrandtotal.Text, out grandTotal);
            decimal.TryParse(lblMPWLCPartAmt.Text, out mpwlcPart);

            if (prastut < 0 || totDetuc < 0 || finalTDS < 0 || grandTotal < 0 || mpwlcPart < 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Amount should be Positive..')", true);
            }
            else
            {
                qry = @"INSERT INTO [tbl_GdwnRentBill_Detuction_RM] 
                        ([BranchID],[GodownID],[JVS_Bill_Number],[Ref_Bill_Number],[JVS_Bill_Amount],
                         [Res_Detuction_Amount],[TDS_Detuction_Amount],[Gain_Detuction_Amount],[JVS_Net_Amount],
                         [CreatedBy],[CreatedDate],[Net_Amount],[Security_Detuction_Amt],[Other_Detuction_Amt],
                         [Total_Detuction_Amt],[MPWLC_Amt]) 
                        VALUES (@BranchID, @GodownID, @JVSBillNo, @RefBillNo, @JVSBillAmt, 
                                @ResDetAmt, @TDSAmt, @GainAmt, @JVSNetAmt, 
                                @CreatedBy, GETDATE(), @NetAmount, @SecDetAmt, @OtherDetAmt, 
                                @TotalDetAmt, @MPWLCAmt)";

                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue ?? string.Empty);
                cmd.Parameters.AddWithValue("@GodownID", ddlgdwn.SelectedValue ?? string.Empty);
                cmd.Parameters.AddWithValue("@JVSBillNo", ddlbillno.SelectedValue ?? string.Empty);
                cmd.Parameters.AddWithValue("@RefBillNo", ddlactualbill.SelectedValue ?? string.Empty);
                cmd.Parameters.AddWithValue("@JVSBillAmt", txtBillAmt.Text.Trim());
                cmd.Parameters.AddWithValue("@ResDetAmt", txtresdetAmt.Text.Trim());
                cmd.Parameters.AddWithValue("@TDSAmt", txtTDSAmt.Text.Trim());
                cmd.Parameters.AddWithValue("@GainAmt", txtGainAmt.Text.Trim());
                cmd.Parameters.AddWithValue("@JVSNetAmt", lblgrandtotal.Text.Trim());
                cmd.Parameters.AddWithValue("@CreatedBy", ClientIP ?? string.Empty);
                cmd.Parameters.AddWithValue("@NetAmount", lblPrastut_netamount.Text.Trim());
                cmd.Parameters.AddWithValue("@SecDetAmt", txtscrDep.Text.Trim());
                cmd.Parameters.AddWithValue("@OtherDetAmt", txtotherdetuction.Text.Trim());
                cmd.Parameters.AddWithValue("@TotalDetAmt", lbltotDetuc.Text.Trim());
                cmd.Parameters.AddWithValue("@MPWLCAmt", lblMPWLCPartAmt.Text.Trim());

                int a = cmd.ExecuteNonQuery();
                if (a == 1)
                {
                    con.Close();
                    btnGenerateBill.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Successfully Save Record'); window.location =('Gdwn_GeneratedBill_Detuction.aspx');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error..')", true);
                }
            }
        }
    }

    protected void ddlactualbill_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGodownType.SelectedItem.Text != "Silo Bags")
        {
            qry = "select CONVERT(decimal(18,0), Net_Amount) as Net_Amount,Month,(SELECT DateName(mm,DATEADD(mm,Month,-1)) as [MonthName]) as MonthName,Financial_Year from tbl_Institution_Storage_Bill_Details where Bill_Number=@BillNo";
        }
        else
        {
            qry = "select CONVERT(decimal(18,0), Net_Amount) as Net_Amount,CONVERT(decimal(18,0), Sub_Amount) as Sub_Amount,Month,(SELECT DateName(mm,DATEADD(mm,Month,-1)) as [MonthName]) as MonthName,Financial_Year from tbl_Institution_Storage_Bill_Details where Bill_Number=@BillNo";
        }

        SqlCommand cmd = new SqlCommand(qry, con);
        cmd.Parameters.AddWithValue("@BillNo", ddlactualbill.SelectedValue ?? string.Empty);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            lblPrastut_netamount.Text = HttpUtility.HtmlEncode(dt.Rows[0]["Net_Amount"].ToString());

            if (ddlactualbill.DataSource != null)
            {
                if (ddlGodownType.SelectedItem.Text != "Silo Bags")
                {
                    qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='GR' and Godown_Id=@GodownID and Bill_Number in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Ref_Bill_No=@RefBillNo) and Bill_Number in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT group by Bill_Number having COUNT(Bill_Number)=2) order by Bill_Number";
                }
                else
                {
                    qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='AD' and Godown_Id=@GodownID and Bill_Number=@RefBillNo";
                }

                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@GodownID", ddlgdwn.SelectedValue ?? string.Empty);
                cmd.Parameters.AddWithValue("@RefBillNo", ddlactualbill.SelectedValue ?? string.Empty);

                da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlbillno.DataSource = ds.Tables[0];
                    ddlbillno.DataTextField = "Bill_Number";
                    ddlbillno.DataValueField = "Bill_Number";
                    ddlbillno.DataBind();
                    ddlbillno.SelectedValue = ds.Tables[0].Rows[0][0].ToString();
                    ddlbillno_SelectedIndexChanged(null, null);
                }
                else
                {
                    ddlbillno.DataSource = null;
                    ddlbillno.DataBind();
                    ddlbillno_SelectedIndexChanged(null, null);
                }
            }

            getcalculation();
        }
    }

    protected void txtscrDep_TextChanged(object sender, EventArgs e)
    {
        getcalculation();
    }

    protected void txtTDSAmt_TextChanged(object sender, EventArgs e)
    {
        getcalculation();
    }

    protected void txtotherdetuction_TextChanged(object sender, EventArgs e)
    {
        getcalculation();
    }

    protected void txtGainAmt_TextChanged(object sender, EventArgs e)
    {
        getcalculation();
    }

    protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = "";
        if (ddlGodownType.SelectedValue.ToString() == "2")
        {
            qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID=@BranchID and Godown_ID in (select distinct CS.Godown_Id from mpscsc.dbo.Digitally_Sing_StorageBill as CS where CS.Branch_Id=@BranchID) and Hired_Type in ('Joint Venture(JV)','WDRA')";
        }
        else if (ddlGodownType.SelectedValue.ToString() == "3")
        {
            qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID=@BranchID and Godown_ID in (select distinct CS.Godown_Id from mpscsc.dbo.Digitally_Sing_StorageBill as CS where CS.Branch_Id=@BranchID) and Hired_Type in ('Hired')";
        }
        else if (ddlGodownType.SelectedValue.ToString() == "4")
        {
            qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID=@BranchID and Godown_ID in (select distinct CS.Godown_Id from mpscsc.dbo.Digitally_Sing_StorageBill as CS where CS.Branch_Id=@BranchID) and Hired_Type in ('Silo Bags')";
        }

        if (!string.IsNullOrEmpty(qry))
        {
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue ?? string.Empty);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgdwn.DataSource = ds.Tables[0];
                ddlgdwn.DataTextField = "Godown_name";
                ddlgdwn.DataValueField = "Godown_ID";
                ddlgdwn.DataBind();
                ddlgdwn.Items.Insert(0, "--Select--");
            }
        }
    }
}