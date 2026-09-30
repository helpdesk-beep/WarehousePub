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
using System.Configuration;
using System.Data;

public partial class Inspection_RegionInspecPrint : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataTable Dt2 = new DataTable();
    DataTable EditStack = new DataTable();
    DataTable EditStackNonMPSCSC = new DataTable();
    string Todaydate = "";
    string CheckValid = "";
    string receiptid = string.Empty;
    SqlCommand cmd = null;
    decimal total1 = 0;
    decimal total2 = 0;
    decimal total3 = 0;
    decimal total4 = 0;
    decimal total5 = 0;
    decimal total6 = 0;
    decimal total7 = 0;
    decimal total8 = 0;
    decimal total9 = 0;
    decimal total10 = 0;
    decimal total11 = 0;
    decimal total12 = 0;
    decimal total13 = 0;
    decimal total14 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["AuditId"].ToString() != "" && Session["AuditId"].ToString() != null)
                {
                    //lbluser.Text = Session["RegionName"].ToString();
                    //txt_ROName.Value = Session["RegionName"].ToString();
                    Get_Region_Inspection_Detail();
                    getImprestDetail();
                }
                else
                {
                    Response.Redirect("InspectionLogin.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("InspectionLogin.aspx");
            }
        } 
    }
    public void Get_Region_Inspection_Detail()
    {
        //qry = "SELECT AuditID,AuditerName,AuditerPost,RegionId,RegionName,CONVERT(varchar(10),AuditDate,103) as AuditDate,RMName,RMPost,CONVERT(varchar(10),RMPostingDate,103) as RMPostingDate,OwnCap,OwnUses,CapCap,CapUses,HiredCap,HiredUSes,TargetIncome,TargetRatio,TargetAchiveR,TargetAchiveP,AuditerRemark,RecentYearsComp,BusinessDevelopInfo,IncomeDetailUptoAuditD,ROInfo1,ROInfo2,ROInfo3,AchiveIncomeCurrentY,ReceivedIncomeCurrentY,PendingIncomeRecentY,ReceivedIncomeRecentY,CashbookBalance,ImprestCashBalance,BankBalance,RNoOfGodown,RCapacityOfGodown,HOLevelAudit,ROLevelAudit,Truti_Patrak,Created_By,CONVERT(varchar(10),Created_Date,103) as Created_Date FROM Intergrated_MP_STORAGE.dbo.RegionAudit where AuditID='" + Session["AuditId"].ToString() + "'";
        qry = "SELECT AuditID,AuditerName,AuditerPost,RegionId,RegionName,CONVERT(varchar(10),AuditDate,103) as AuditDate,RMName,RMPost,CONVERT(varchar(10),RMPostingDate,103) as RMPostingDate,OwnCap,OwnUses,CapCap,CapUses,HiredCap,HiredUSes,TargetIncome,TargetRatio,TargetAchiveR,TargetAchiveP,AuditerRemark,RecentYearsComp,BusinessDevelopInfo,IncomeDetailUptoAuditD,ROInfo1,ROInfo2,ROInfo3,AchiveIncomeCurrentY,ReceivedIncomeCurrentY,PendingIncomeRecentY,ReceivedIncomeRecentY,CashbookBalance,ImprestCashBalance,BankBalance,RNoOfGodown,RCapacityOfGodown,HOLevelAudit,ROLevelAudit,Truti_Patrak,Created_By,CONVERT(varchar(10),Created_Date,103) as Created_Date,[TDS1],[TDS2],[MoneyTrans],[WhrCharges],[LbrCharges],[JVS1],[JVS2],Financial_Year FROM Intergrated_MP_STORAGE.dbo.RegionAudit where AuditID='" + Session["AuditId"].ToString() + "'";
        SqlCommand cmds = new SqlCommand(qry,Con);
        SqlDataAdapter da = new SqlDataAdapter(cmds);
        da.Fill(Dt1);
        if (Dt1.Rows.Count > 0)
        {
            lblRgn.Text = Dt1.Rows[0]["RegionName"].ToString();
            lblauditornamepost.Text = Dt1.Rows[0]["AuditerName"].ToString() + "/" + Dt1.Rows[0]["AuditerPost"].ToString();
            lblRMandPost.Text = Dt1.Rows[0]["RMName"].ToString() + "/" + Dt1.Rows[0]["RMPost"].ToString();
            lblROName.Text = Dt1.Rows[0]["RegionName"].ToString();
            //////employee name
            lblRMPostingDate.Text = Dt1.Rows[0]["RMPostingDate"].ToString();
            lblowncap.Text = Dt1.Rows[0]["OwnCap"].ToString();
            lblownuse.Text = Dt1.Rows[0]["OwnUses"].ToString();
            lblcapcap.Text = Dt1.Rows[0]["CapCap"].ToString();
            lblcapuse.Text = Dt1.Rows[0]["CapUses"].ToString();
            lblhiredcap.Text = Dt1.Rows[0]["HiredCap"].ToString();
            lblhireduse.Text = Dt1.Rows[0]["HiredUSes"].ToString();
            lblownper.Text = (Convert.ToDecimal(Dt1.Rows[0]["OwnUses"]) / 100).ToString()+" %";
            lblcapper.Text = (Convert.ToDecimal(Dt1.Rows[0]["CapUses"]) / 100).ToString() + " %";
            lblhiredper.Text = (Convert.ToDecimal(Dt1.Rows[0]["HiredUSes"]) / 100).ToString() + " %";
            lblTotalUtilizationPer.Text = ((Convert.ToDecimal(Dt1.Rows[0]["OwnUses"]) + Convert.ToDecimal(Dt1.Rows[0]["CapUses"]) + Convert.ToDecimal(Dt1.Rows[0]["HiredUSes"])) / 100).ToString() + " %";
            lblTotalCapacity.Text = (Convert.ToDecimal(Dt1.Rows[0]["OwnCap"]) + Convert.ToDecimal(Dt1.Rows[0]["CapCap"]) + Convert.ToDecimal(Dt1.Rows[0]["HiredCap"])).ToString();
            lblTotalUtilization.Text = (Convert.ToDecimal(Dt1.Rows[0]["OwnUses"]) + Convert.ToDecimal(Dt1.Rows[0]["CapUses"]) + Convert.ToDecimal(Dt1.Rows[0]["HiredUSes"])).ToString();
            lblTargetIncome.Text = Dt1.Rows[0]["TargetIncome"].ToString();
            lblTargetRatio.Text = Dt1.Rows[0]["TargetRatio"].ToString();
            lblTargetAchiveR.Text = Dt1.Rows[0]["TargetAchiveR"].ToString();
            lblTargetAchiveP.Text = Dt1.Rows[0]["TargetAchiveP"].ToString();
            lblAuditerRemark.Text = Dt1.Rows[0]["AuditerRemark"].ToString();
            lblRecentYearsComp.Text = Dt1.Rows[0]["RecentYearsComp"].ToString();
            lblBusinessDevelopInfo.Text = Dt1.Rows[0]["BusinessDevelopInfo"].ToString();
            lblIncomeDetailUptoAuditD.Text = Dt1.Rows[0]["IncomeDetailUptoAuditD"].ToString();
            lblROInfo1.Text = Dt1.Rows[0]["ROInfo1"].ToString();
            lblROInfo2.Text = Dt1.Rows[0]["ROInfo2"].ToString();
            lblROInfo3.Text = Dt1.Rows[0]["ROInfo3"].ToString();
            lblAchiveIncomeCurrentY.Text = Dt1.Rows[0]["AchiveIncomeCurrentY"].ToString();
            lblReceivedIncomeCurrentY.Text = Dt1.Rows[0]["ReceivedIncomeCurrentY"].ToString();
            lblPendingIncomeRecentY.Text = Dt1.Rows[0]["PendingIncomeRecentY"].ToString();
            lblReceivedIncomeRecentY.Text = Dt1.Rows[0]["ReceivedIncomeRecentY"].ToString();
            lblCashbookBalance.Text = Dt1.Rows[0]["CashbookBalance"].ToString();
            lblImprestCashBalance.Text = Dt1.Rows[0]["ImprestCashBalance"].ToString();
            lblBankBalance.Text = Dt1.Rows[0]["BankBalance"].ToString();
            lblRNoOfGodown.Text = Dt1.Rows[0]["RNoOfGodown"].ToString();
            lblRCapacityOfGodown.Text = Dt1.Rows[0]["RCapacityOfGodown"].ToString();
            lblHOLevelAudit.Text = Dt1.Rows[0]["HOLevelAudit"].ToString();  
            lblROLevelAudit.Text = Dt1.Rows[0]["ROLevelAudit"].ToString();
            lblTruti_Patrak.Text = Dt1.Rows[0]["Truti_Patrak"].ToString();

            ////////////new//////////////////////
            tds1.Text = Dt1.Rows[0]["TDS1"].ToString();
            tds2.Text = Dt1.Rows[0]["TDS2"].ToString();
            dr1.Text = Dt1.Rows[0]["MoneyTrans"].ToString();
            WC.Text = Dt1.Rows[0]["WhrCharges"].ToString();
            lc.Text = Dt1.Rows[0]["LbrCharges"].ToString();
            jvs1.Text = Dt1.Rows[0]["JVS1"].ToString();
            jvs2.Text = Dt1.Rows[0]["JVS2"].ToString();
            lblFy.Text = Dt1.Rows[0]["Financial_Year"].ToString();
            //lblFy.Text = Dt1.Rows[0]["Financial_Year"].ToString();

            ///////Fill Employee name////////
            if (Session["AuditId"].ToString() != "" && Session["AuditId"].ToString() != null)
            {
                //string qrye = "select ROW_NUMBER() OVER (ORDER BY AuditID) AS SN,EmpName,EmpPost from BranchAuditEmp where Employee_Type='RO' and AuditID='" + Session["AuditId"].ToString() + "'";
                string qrye = "select ROW_NUMBER() OVER (ORDER BY AuditID) AS SN,EmpName,EmpPost,EMPPostingdate from BranchAuditEmp where Employee_Type='RO' and AuditID='" + Session["AuditId"].ToString() + "'";
                SqlCommand cmde = new SqlCommand(qrye, Con);
                SqlDataAdapter dae = new SqlDataAdapter(cmde);
                dae.Fill(Dt2);
                if (Dt2.Rows.Count > 0)
                {
                    GVE.DataSource = Dt2;
                    GVE.DataBind();
                }
            }
          }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("InspectionLogin.aspx");
    }
    private void getImprestDetail()
    {
        try
        {
            string query = "SELECT [AuditId],CASE [Month] When 1 then 'January' When 2 then 'February' When 3 then 'March' When 4 then 'April' When 5 then 'May' When 6 then 'June' When 7 then 'July' When 8 then 'August' When 9 then 'September' When 10 then 'October' When 11 then 'November' When 12 then 'December' End as Month,[OpeningBalance],[RecupmentAmount],[DepositByBM],[TFCashBook],[TFConstIimprest],[Total],[ImprestForPass],[ImprestPass],[WitheldAmount],[DisalloudAmount],[ReturnToBM],[ReturnToCashBook],[ReturnToConstImp],[ClosingBalance] FROM [Intergrated_MP_STORAGE].[dbo].[Imprest_Recupment] where AuditID='" + Session["AuditId"].ToString() + "'";
            // string query = "SELECT  ROW_NUMBER() OVER(ORDER BY [Depositor_WHR_Id] DESC) AS Row,[AuditID],[LiyanPatraDate],[BankName] ,[Depositor_WHR_Id],[Depotid],[Commodity_Id],(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whr.Commodity_Id) as comm,[Depositor_Name],[TotalBags_Received],[Total_Qty_Received] ,[MktValue_of_Commodity],convert(varchar(20),[WHR_Issue_Date],103) as whrdate FROM [Intergrated_MP_STORAGE].[dbo].[tbl_storage_Depositor_WHR_Relation] as whr inner join [BranchAuditWHRDtl] as awhr on whr.Whr_No=awhr.WHRID where BranchID='2301002' and awhr.AuditID='23010021512'";

            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvImprest.DataSource = ds.Tables[0];
                gvImprest.DataBind();
            }
            else
            {

            }

        }

        catch (Exception ex)
        {

        }
    }
    protected void gvImprest_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total1 += (DataBinder.Eval(e.Row.DataItem, "OpeningBalance") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "OpeningBalance")) : 0;

            total2 += (DataBinder.Eval(e.Row.DataItem, "RecupmentAmount") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RecupmentAmount")) : 0;

            total3 += (DataBinder.Eval(e.Row.DataItem, "DepositByBM") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DepositByBM")) : 0;

            total4 += (DataBinder.Eval(e.Row.DataItem, "TFCashBook") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TFCashBook")) : 0;

            total5 += (DataBinder.Eval(e.Row.DataItem, "TFConstIimprest") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TFConstIimprest")) : 0;

            total6 += (DataBinder.Eval(e.Row.DataItem, "Total") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total")) : 0;

            total7 += (DataBinder.Eval(e.Row.DataItem, "ImprestForPass") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ImprestForPass")) : 0;

            total8 += (DataBinder.Eval(e.Row.DataItem, "ImprestPass") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ImprestPass")) : 0;

            total9 += (DataBinder.Eval(e.Row.DataItem, "WitheldAmount") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "WitheldAmount")) : 0;

            total10 += (DataBinder.Eval(e.Row.DataItem, "DisalloudAmount") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DisalloudAmount")) : 0;

            total11 += (DataBinder.Eval(e.Row.DataItem, "ReturnToBM") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ReturnToBM")) : 0;

            total12 += (DataBinder.Eval(e.Row.DataItem, "ReturnToCashBook") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ReturnToCashBook")) : 0;

            total13 += (DataBinder.Eval(e.Row.DataItem, "ReturnToConstImp") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ReturnToConstImp")) : 0;

            total14 += (DataBinder.Eval(e.Row.DataItem, "ClosingBalance") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ClosingBalance")) : 0;


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lblOpeningBalance = (Label)e.Row.FindControl("OpeningBalance");

            lblOpeningBalance.Text = total1.ToString();


            Label lblRecupmentAmount = (Label)e.Row.FindControl("RecupmentAmount");

            lblRecupmentAmount.Text = total2.ToString();

            Label lblDepositByBM = (Label)e.Row.FindControl("DepositByBM");

            lblDepositByBM.Text = total3.ToString();

            Label lblTFCashBook = (Label)e.Row.FindControl("TFCashBook");

            lblTFCashBook.Text = total4.ToString();

            Label lblTFConstIimprest = (Label)e.Row.FindControl("TFConstIimprest");

            lblTFConstIimprest.Text = total5.ToString();

            Label lblTotal = (Label)e.Row.FindControl("Total");

            lblTotal.Text = total6.ToString();

            Label lblImprestForPass = (Label)e.Row.FindControl("ImprestForPass");

            lblImprestForPass.Text = total7.ToString();

            Label lblImprestPass = (Label)e.Row.FindControl("ImprestPass");

            lblImprestPass.Text = total8.ToString();

            Label lblWitheldAmount = (Label)e.Row.FindControl("WitheldAmount");

            lblWitheldAmount.Text = total9.ToString();

            Label lblDisalloudAmount = (Label)e.Row.FindControl("DisalloudAmount");

            lblDisalloudAmount.Text = total10.ToString();

            Label lblReturnToBM = (Label)e.Row.FindControl("ReturnToBM");

            lblReturnToBM.Text = total11.ToString();

            Label lblReturnToCashBook = (Label)e.Row.FindControl("ReturnToCashBook");

            lblReturnToCashBook.Text = total12.ToString();

            Label lblReturnToConstImp = (Label)e.Row.FindControl("ReturnToConstImp");

            lblReturnToConstImp.Text = total13.ToString();

            Label lblClosingBalance = (Label)e.Row.FindControl("ClosingBalance");

            lblClosingBalance.Text = total14.ToString();

        }
    }
}
