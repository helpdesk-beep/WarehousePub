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

public partial class Accounting_BM_PrintFinal_StorageBill : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["BranchId"] != null))
        {
            if (!IsPostBack)
            {
                GetBillNo();
            }

        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void GetFinalBillDetail()
    {
        try
        {
            string Dist_id = Session["Depot_DistID"].ToString();
            string Branch_Id = Session["BranchId"].ToString();
            //string str = "select DT.District_Name,DP.DepotName,SB.Commodity_Rate,SB.Bill_Number,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id) as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,Floor(SB.Sub_Amount) as Charges_Amount,Floor(isnull(SB.Service_Tax_Amt,0)) as GST_AMT,Floor(isnull(SB.MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(SB.GST_Amt_SC,0))as GST_Sup_Amt,Floor(SB.Net_Amount) as Net_Amount,DateName( month , DateAdd( month ,SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year,(select count(A.Bill_Number) from tbl_Institution_Storage_Bill_Details as A where A.Fin_Bill_No='"+ ddlBillNo.SelectedItem.Text +"') as TotalGodown from tbl_Institution_Storage_Bill_Summary as SB inner join tbl_MetaData_DEPOT as dp on dp.BranchId=SB.Branch_Id inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=SB.District_Id  where SB.Created_Date>=CONVERT(varchar(10),'10/01/2019',101) and SB.Branch_Id='" + Branch_Id + "' and Bill_Number='" + ddlBillNo.SelectedItem.Text + "'and SB.District_Id='" + Dist_id + "' and BO_Approval_Status is null and SB.Depositor_Id='129'";
            //string str = "select DT.District_Name,DP.DepotName,SB.Commodity_Rate,SB.Bill_Number,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id)+' '+SB.Crop_Year+'' as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,Floor(SB.Sub_Amount) as Charges_Amount,Floor(isnull(SB.Service_Tax_Amt,0)) as GST_AMT,Floor(isnull(SB.MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(SB.GST_Amt_SC,0))as GST_Sup_Amt,Floor(SB.Net_Amount) as Net_Amount,DateName( month , DateAdd( month ,SB.Month , 0 ) - 1 )+'-'+SB.Year+'' as Bill_Month,SB.Crop_Year,case when SB.Bill_Type='1' then 'MPWLC Godown' when SB.Bill_Type='2' then 'MPWLC Cap'when SB.Bill_Type='3' then 'Silo Bags' when SB.Bill_Type='4' then 'Tribal Godowns' when SB.Bill_Type='5' then 'Steel Silo' else '' end +'-'+(select CONVERT(varchar(20),count(A.Bill_Number)) from tbl_Institution_Storage_Bill_Details as A where A.Fin_Bill_No='" + ddlBillNo.SelectedValue + "') as TotalGodown,(select GST.GST_No from tbl_MetaData_GST_Number as GST where DT.Region_ID=GST.Region_Id) as GST,(select PN.PAN from tbl_MetaData_GST_Number as PN where DT.Region_ID=PN.Region_Id) as PAN from tbl_Institution_Storage_Bill_Summary as SB inner join tbl_MetaData_DEPOT as dp on dp.BranchId=SB.Branch_Id inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=SB.District_Id  where SB.Created_Date>=CONVERT(varchar(10),'10/01/2019',101) and SB.Branch_Id='" + Branch_Id + "' and Bill_Number='" + ddlBillNo.SelectedItem.Text + "'and SB.District_Id='" + Dist_id + "' and BO_Approval_Status is null and SB.Depositor_Id='129'";
            string str = "select DT.District_Name,DP.DepotName,SB.Commodity_Rate,SB.Bill_Number,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id)+' '+SB.Crop_Year+'' as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,Floor(SB.Sub_Amount) as Charges_Amount,Floor(isnull(SB.Service_Tax_Amt,0)) as GST_AMT,Floor(isnull(SB.MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(SB.GST_Amt_SC,0))as GST_Sup_Amt,Floor(SB.Net_Amount) as Net_Amount,DateName( month , DateAdd( month ,SB.Month , 0 ) - 1 )+'-'+SB.Year+'' as Bill_Month,SB.Crop_Year,case when SB.Bill_Type='1' then 'MPWLC Godown' when SB.Bill_Type='2' then 'MPWLC Cap'when SB.Bill_Type='3' then 'Silo Bags' when SB.Bill_Type='4' then 'Tribal Godowns' when SB.Bill_Type='5' then 'Steel Silo' else '' end +'-'+(select CONVERT(varchar(20),count(A.Bill_Number)) from tbl_Institution_Storage_Bill_Details as A where A.Fin_Bill_No='" + ddlBillNo.SelectedValue + "') as TotalGodown,(select GST.GST_No from tbl_MetaData_GST_Number as GST where DT.Region_ID=GST.Region_Id) as GST,(select PN.PAN from tbl_MetaData_GST_Number as PN where DT.Region_ID=PN.Region_Id) as PAN from tbl_Institution_Storage_Bill_Summary as SB inner join tbl_MetaData_DEPOT as dp on dp.BranchId=SB.Branch_Id inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=SB.District_Id  where SB.Created_Date>=CONVERT(varchar(10),'10/01/2019',101) and SB.Branch_Id='" + Branch_Id + "' and Bill_Number='" + ddlBillNo.SelectedItem.Text + "'and SB.District_Id='" + Dist_id + "' and SB.Depositor_Id='129'";

            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBill.DataSource = ds.Tables[0];
                gvBill.DataBind();

                lblDistrict.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                lblBranch.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                Label12.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                lblRate.Text = ds.Tables[0].Rows[0]["Commodity_Rate"].ToString();
                lblBillingDate.Text = ds.Tables[0].Rows[0]["Billing_Date"].ToString();

                lblSPAN.Text= ds.Tables[0].Rows[0]["PAN"].ToString();
                lblSGST.Text = ds.Tables[0].Rows[0]["GST"].ToString();
                btnDetail.Text = "WAREHOUSES DETAIL(" + ds.Tables[0].Rows[0]["TotalGodown"].ToString() + ")";
                Panel2.Visible = true;
                //btnNo.Enabled = false;
            }
            else
            {
                gvBill.DataSource = "";
                gvBill.DataBind();
            }

        }

        catch (Exception ex)
        {

        }
    }
    public void GetBillNo()
    {
        string Dist_id = Session["Depot_DistID"].ToString();
        string Branch_Id = Session["BranchId"].ToString();
        string qry = "";
        qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Summary where Branch_Id='"+ Branch_Id + "' and District_Id='"+ Dist_id + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBillNo.DataSource = ds.Tables[0];
            ddlBillNo.DataTextField = "Bill_Number";
            ddlBillNo.DataValueField = "Bill_Number";
            ddlBillNo.DataBind();
            ddlBillNo.Items.Insert(0, "--Select--");
        }
        else
        {

        }
    }

    protected void ddlBillNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetFinalBillDetail();
        DSCSign();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounting/BM_PrintFinal_StorageBill.aspx");
    }
    public void DSCSign()
    {
        qry = " select DSC_Serial_No,DSC_Holder_Name,Client_Ip,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_BO where Ref_Bill_No='" + ddlBillNo.SelectedValue.ToString() + "'  and DSC_User_Type='B'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Image1.Visible = true;
            lblBSerialNo.Text = "DSC Serial No : " + dt.Rows[0]["DSC_Serial_No"].ToString();
            lblBIP.Text = "Client IP : " + dt.Rows[0]["Client_Ip"].ToString();
            lblBHolderName.Text = "DSC Holder Name : " + dt.Rows[0]["DSC_Holder_Name"].ToString();
            lblBCreatedDate.Text = "DSC Sign Date : " + dt.Rows[0]["CreatedDate"].ToString();
        }
        else
        {
            Image1.Visible = false;
            lblBSerialNo.Text = "";
            lblBIP.Text = "";
            lblBHolderName.Text = "";
            lblBCreatedDate.Text = "";
        }
    }

    protected void gvBill_SelectedIndexChanged(object sender, EventArgs e)
    {
  
        string Bill_No = GridView1.SelectedRow.Cells[0].Text;
        string Godown_ID = GridView1.SelectedRow.Cells[5].Text;
        string JVS_Bill = GridView1.SelectedRow.Cells[12].Text;
        Response.Write("<script>window.open ('BM_PrintGeneratedBill_DSC.aspx?BillNo=" + Bill_No + "&GodownID=" + Godown_ID + "&JVSBill=" + JVS_Bill + "','_blank');</script>");
        ModalPopupExtender1.Show();
        GetBillDetail();
    }
    private void GetBillDetail()
    {
        try
        {
            string Dist_id = Session["Depot_DistID"].ToString();
            string Branch_Id = Session["BranchId"].ToString();
            //string str = "select DT.District_Name,DP.DepotName,SB.Commodity_Rate,SB.Bill_Number,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id)+' '+SB.Crop_Year+'' as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,Floor(SB.Sub_Amount) as Charges_Amount,Floor(isnull(SB.Service_Tax_Amt,0)) as GST_AMT,Floor(isnull(SB.MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(SB.GST_Amt_SC,0))as GST_Sup_Amt,Floor(SB.Net_Amount) as Net_Amount,DateName( month , DateAdd( month ,SB.Month , 0 ) - 1 )+'-'+SB.Year+'' as Bill_Month,SB.Crop_Year,(select count(A.Bill_Number) from tbl_Institution_Storage_Bill_Details as A where A.Fin_Bill_No='" + ddlBillNo.SelectedItem.Text + "') as TotalGodown from tbl_Institution_Storage_Bill_Summary as SB inner join tbl_MetaData_DEPOT as dp on dp.BranchId=SB.Branch_Id inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=SB.District_Id  where SB.Created_Date>=CONVERT(varchar(10),'10/01/2019',101) and SB.Branch_Id='" + Branch_Id + "' and Bill_Number='" + ddlBillNo.SelectedItem.Text + "'and SB.District_Id='" + Dist_id + "' and BO_Approval_Status is null and SB.Depositor_Id='129'";
            //string str = "select DT.District_Name,DP.DepotName,SB.Commodity_Rate,SB.Bill_Number,SB.Godown_Id,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id)+' '+SB.Crop_Year+'' as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,Floor(SB.Sub_Amount) as Charges_Amount,Floor(isnull(SB.Service_Tax_Amt,0)) as GST_AMT,Floor(isnull(SB.MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(SB.GST_Amt_SC,0))as GST_Sup_Amt,Floor(SB.Net_Amount) as Net_Amount,DateName( month , DateAdd( month ,SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=SB.Godown_Id ) as Godown,(select Bill_No from tbl_Godown_Rent_Deduction_Amount where Ref_Bill_No=SB.Bill_Number) as JVS_Bill from tbl_Institution_Storage_Bill_Details as SB inner join tbl_MetaData_DEPOT as dp on dp.BranchId=SB.Branch_Id inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=SB.District_Id  where SB.Created_Date>=CONVERT(varchar(10),'10/01/2019',101) and SB.Branch_Id='" + Branch_Id + "' and SB.Fin_Bill_No='" + ddlBillNo.SelectedItem.Text + "'and SB.District_Id='" + Dist_id + "' and BO_Approval_Status is null and SB.Depositor_Id='129'";
            string str = "select DT.District_Name,DP.DepotName,SB.Commodity_Rate,SB.Bill_Number,SB.Godown_Id,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id)+' '+SB.Crop_Year+'' as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,Floor(SB.Sub_Amount) as Charges_Amount,Floor(isnull(SB.Service_Tax_Amt,0)) as GST_AMT,Floor(isnull(SB.MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(SB.GST_Amt_SC,0))as GST_Sup_Amt,Floor(SB.Net_Amount) as Net_Amount,DateName( month , DateAdd( month ,SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=SB.Godown_Id ) as Godown,(select Bill_No from tbl_Godown_Rent_Deduction_Amount where Ref_Bill_No=SB.Bill_Number) as JVS_Bill from tbl_Institution_Storage_Bill_Details as SB inner join tbl_MetaData_DEPOT as dp on dp.BranchId=SB.Branch_Id inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=SB.District_Id  where SB.Created_Date>=CONVERT(varchar(10),'10/01/2019',101) and SB.Branch_Id='" + Branch_Id + "' and SB.Fin_Bill_No='" + ddlBillNo.SelectedItem.Text + "'and SB.District_Id='" + Dist_id + "' and SB.Depositor_Id='129'";

            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GridView1.Columns[12].Visible = false;
                //GridView1.Columns[5].Visible = false;
                int TotalR = 0;
                decimal TotalC = 0;
                decimal TGSTA = 0;
                decimal TotalSC = 0;
                decimal TotalGSTOnSC = 0;
                decimal TotalBillAmt = 0;
                foreach (GridViewRow row in GridView1.Rows)
                {
                        TotalR = TotalR + 1;
                    TotalC = TotalC + Convert.ToDecimal(row.Cells[6].Text);
                    TGSTA = TGSTA + Convert.ToDecimal(row.Cells[7].Text);
                    TotalSC = TotalSC + Convert.ToDecimal(row.Cells[8].Text);
                    TotalGSTOnSC = TotalGSTOnSC + Convert.ToDecimal(row.Cells[9].Text);
                    TotalBillAmt = TotalBillAmt + Convert.ToDecimal(row.Cells[10].Text);

                }
                GridView1.FooterRow.Cells[4].Text = "Total : ";
                GridView1.FooterRow.Cells[6].Text = TotalC.ToString();
                GridView1.FooterRow.Cells[7].Text = TGSTA.ToString();
                GridView1.FooterRow.Cells[8].Text = TotalSC.ToString();
                GridView1.FooterRow.Cells[9].Text = TotalGSTOnSC.ToString();
                GridView1.FooterRow.Cells[10].Text = TotalBillAmt.ToString();
            }
            else
            {
                GridView1.DataSource = "";
                GridView1.DataBind();
            }

        }

        catch (Exception ex)
        {

        }
    }

    protected void btnDetail_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
        GetBillDetail();
    }
}