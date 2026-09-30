using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Security.Cryptography;
using System.Data.SqlClient;

public partial class Welcome : System.Web.UI.Page
{
    string userSession = string.Empty;
    string userAgent = string.Empty;
    string process = string.Empty;
    public SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            if (Session["UserName"].ToString() != "")
            {
                UxName.Text = Session["UserName"].ToString();
                if (UxName.Text == "Admin")
                {
                    HyperLink1.Visible = true;
                    HyperLink2.Visible = true;
                    HyperLink3.Visible = true;
                }
                else
                {
                    HyperLink1.Visible = false;
                    HyperLink2.Visible = false;
                    HyperLink3.Visible = false;
                    // HyperLink4.Visible = true;
                    //Modalpopup closed 22/07/2024
                    // ModalPopupExtender1.Show();
                    fillData(Session["Region_Logid"].ToString());
                }
                if (!IsPostBack)
                {

                }
            }

            if (Session["IsSussess"] == "Success")
            {

                UserLog();
                Session.Remove("IsSussess");


            }
        }
        else
        {

            Response.Redirect("Logout.aspx");

        }
    }

    protected void fillData(string RID)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[Get_Pendency_at_RM_01]", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", RID);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            PendingBillforPassing.InnerText = dt.Rows[0]["PendingBillforPassing"].ToString();
                            PendingAmtforPassing.InnerText = dt.Rows[0]["PendingAmtforPassing"].ToString();
                            BillPendingAGMDSC.InnerText = dt.Rows[0]["BillPendingAGMDSC"].ToString();
                            AmtPendingAGMDSC.InnerText = dt.Rows[0]["AmtPendingAGMDSC"].ToString();
                            ApprovedBillbyAGM.InnerText = dt.Rows[0]["ApprovedBillbyAGM"].ToString();
                            ApprovedAmtbyAGM.InnerText = dt.Rows[0]["ApprovedAmtbyAGM"].ToString();
                            PendingBillForNeft.InnerText = dt.Rows[0]["PendingBillForNeft"].ToString();
                            PendingAmtForNeft.InnerText = dt.Rows[0]["PendingAmtForNeft"].ToString();
                        }
                        else
                        {
                            PendingBillforPassing.InnerText = "0";
                            PendingAmtforPassing.InnerText = "0";
                            BillPendingAGMDSC.InnerText = "0";
                            AmtPendingAGMDSC.InnerText = "0";
                            ApprovedBillbyAGM.InnerText = "0";
                            ApprovedAmtbyAGM.InnerText = "0";
                            PendingBillForNeft.InnerText = "0";
                            PendingAmtForNeft.InnerText = "0";
                        }
                    }
                }
            }
        }
    }

    private void UserLog()
    {
        try
        {
            string RoleID = "";
            string Logid = "";
            RoleID = Session["RoleId"].ToString();
            if (RoleID == "1")
            {

                Logid = Session["Depot_Logid"].ToString();

            }
            else if (RoleID == "2")
            {

                Logid = Session["Region_Logid"].ToString();

            }
            else if (RoleID == "3")
            {

                Logid = Session["State_Logid"].ToString();

            }
            else
            {

                //

            }

            userSession = HttpContext.Current.Session.SessionID;
            userAgent = Request.Browser.Browser;
            userAgent = userAgent + "-" + Request.Browser.Version;
            process = System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
            string strHost = System.Net.Dns.GetHostName();
            string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand cmd = new SqlCommand("LoginAttempt_Insert", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@User_Name", SqlDbType.VarChar, 50);
            cmd.Parameters["@User_Name"].Value = Session["UserName"].ToString();
            cmd.Parameters.Add("@ClientIP", SqlDbType.VarChar, 20);
            cmd.Parameters["@ClientIP"].Value = ClientIP.ToString();
            cmd.Parameters.Add("@Login_Statue", SqlDbType.VarChar, 50);
            cmd.Parameters["@Login_Statue"].Value = "Success";
            cmd.Parameters.Add("@User_Session_ID", SqlDbType.VarChar, 200);
            cmd.Parameters["@User_Session_ID"].Value = userSession.ToString();
            cmd.Parameters.Add("@User_Agent", SqlDbType.VarChar, 100);
            cmd.Parameters["@User_Agent"].Value = userAgent.ToString();
            cmd.Parameters.Add("@Process_ID", SqlDbType.VarChar, 20);
            cmd.Parameters["@Process_ID"].Value = process.ToString();
            cmd.Parameters.Add("@Session_TimeOut", SqlDbType.Int);
            cmd.Parameters["@Session_TimeOut"].Value = Session.Timeout;
            cmd.Parameters.Add("@U_ID", SqlDbType.VarChar, 20);
            cmd.Parameters["@U_ID"].Value = Logid.ToString();
            int sts = cmd.ExecuteNonQuery();
            cmd.Dispose();
            sqlCon.Close();
        }
        catch (Exception ex)
        {
            Session["errDesc"] = "Sorry ,retry or login again";
            Server.Transfer("CustomError.aspx");
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_RegionWise_CropWise_WHR";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Region_CropYear_ReportViewer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_rigionwise_pendinggatepass";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_RigionWise_Whr_Do_PendingReciving";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Godownlist_on_Region";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "ProcurmentPendingRigion";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "ProcurementstatusRegion15";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Regionwisemtotalwhr15";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }

    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_MPWLC_C_U_Region";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);

    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownCapacityDetails";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);

    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "paddyProcRegion516";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);

    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        //  Response.Redirect("Reports/Region/Geo_Stock_godown_region.aspx");
        Response.Write("<script>window.open( 'Geo_Stock_godown_region.aspx' , '-blank' );</script>");
    }
    protected void LinkButton12_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Bhugtanrashidatewise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);

    }
    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "ProcurementstatusRegion16";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);

    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_Master_Bill_Report";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton15_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CommodityLossBranchWiseSummary";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton16_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_PVT_PEG_Godown_CPT_and_UTL";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton17_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_SteelSilo_Godown_CPT_and_UTL";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton18_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_WDRA_Godown_CPT_and_UTL";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton19_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_JVS_Godown_CPT_and_UTL";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton20_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_Owned_Godown_Capacity_And_Utillazation";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton22_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "RegionDiff_BW_GPIssueAndEntryDate";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton21_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_DiffBW_WHRIssueAndEntryDate";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton23_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_BranchWiseStockReport";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Region_CropYear_ReportViewer.aspx\",\"_blank\")", true);

    }

    protected void LinkButton24_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_GodownWiseCurrentStock";

        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Region_CropYear_ReportViewer.aspx\",\"_blank\")", true);
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton25_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_KharifProc2016_17_AllComm";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton26_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_KharifProc2016_17_AllComodity";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton27_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_GodownWise_CommodityWise_MPSCSC_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton8_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "WheatPSS_Gain_RegionSummary_Manual";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton28_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_CmdHiredTypeWise_TillDate";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton29_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_StockReportMPSCSC_TillDate";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton30_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "RegionDistrictWise_Paddy_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton31_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "RegionDistrictWise_Rice_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton32_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_IssueCenterWise_BranchDetail";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton33_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "RegionCommodityStock_GraphRepresentation";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton34_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "RegionCropYeraWiseStock_ChartRepresentation";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton35_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "RegionCpt_Utl_GraphRepresentation";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton36_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_GodownWise_Stack_StockPosition";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton37_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_Wheat_Procurement2017_18";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton38_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_wheatpss_1718_RemainingDF_For_WHR";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);

    }
    protected void LinkButton39_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_GatepassDateWiseStockIssueDtl";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RptViewer_Regional_Level_Dates.aspx\",\"_blank\")", true);
    }
    protected void LinkButton40_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_Arhar_Proc_2017_18";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton41_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_Onion_Proc_2017_18";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton42_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "RegionPremWise_GodownComparision";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton29_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_KharifProc2017_18_CommodityWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton43_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_GodownOwner_Details";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton44_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_MonthWiseCreatedBillDetails";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton45_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_Paddy_1718_RemainingDF_For_WHR";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton46_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_WheatProcurement_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton47_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_GramProc_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton48_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_WheatProc1819_Without_CWCFCIMFDG";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton49_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_WheatProc1819_CWCFCIMFDG";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton50_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_ProvDF_FinalDF_WHR";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton51_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_CMS_Proc1819_Without_CWCFCIMFDG_DistWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton52_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_CMS_Proc1819_CWCFCIMFDG_DistWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton53_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_QCDeletedReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton54_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_DeletedWHR_Details";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton55_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_DeletedWHR_WithReqDate_DeletedDate";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton56_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_DistrictWise_HiredTypeWise_StockRpt";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton57_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_DistrictWiseVacantCpt";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton58_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_Region_New_Godown_Summary";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton59_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_VerifyGdwnReport2018";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton60_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_HiredTypeWiseCpt_VerifyGdwnRpt";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton61_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_VerifyGodownVacantCPT";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton62_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_Kharif_Procurement_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton63_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_CoarseGrain_Procurement_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton64_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_Paddy_Procurement_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton65_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_Wheat_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton66_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_DSC_UploaderDetails";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton67_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_CMS_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton68_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_NAFED_WHRPrint_CMS_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton69_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_Arahar_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton70_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_Wheat_Procurement_2019_20_eWHR";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton71_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_Godown_Owner_Account_Detail";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton72_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_StorageChrg_GnrtBillSummary2019_MPWLCGdwn";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton73_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_StorageChrg_GnrtBillSummary2019_Other";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton74_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "RO_GRent_PassingOrder_Detail";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton75_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_Kharif_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton76_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "RO_Bill_Payment_Status";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton76_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Region_CourseGrain_Proc_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton77_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "RO_Bill_Payment_Status";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton78_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "RO_GRent_PassingOrder_Detail_Old";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton79_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "RO_Wheat_Procurement_2020_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton80_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "RO_CSM_Procurement_2020_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void LinkButton81_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "RO_NAFED_WHRPrint_CMS_Proc_2012_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }
    protected void btnPCBJCD_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Get_BajraProcurement";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Rpt_Get_BajraProcurement.aspx\",\"_blank\")", true);
    }
    protected void btn18_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PVT_Godown_Storage_Charges_Bill";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Rpt_Get_Bill_Detail_After_August_District_Branch_Wise_With_Amount.aspx\",\"_blank\")", true);
    }
    protected void btn19_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PVT_Godown_Storage_Charges_Bill";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Rpt_Get_Bill_Detail_Before_August_District_Wise_With_Amount.aspx\",\"_blank\")", true);
    }
    protected void btn10_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PVT_Godown_Storage_Charges_Bill";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Rpt_Get_Bill_Detail_August_to_Till.aspx\",\"_blank\")", true);
    }
    protected void btn20_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Get_Bill_Detail_Before_August_District_Branch_Wise_With_Amount";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Rpt_Get_Bill_Detail_Before_August_District_Branch_Wise_With_Amount.aspx\",\"_blank\")", true);
    }
    protected void btn21_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Get_Bill_Detail_Before_August_District_Branch_Wise_With_Amount";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Rpt_Get_Bill_Detail_After_August_With_Amount_And_Difference_District_Wise.aspx\",\"_blank\")", true);
    }

    protected void btnpasfromaug_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Get_RO_GRent_PassingOrder_Detail_From_Aug";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Rpt_Get_RO_GRent_PassingOrder_Detail_From_Aug.aspx\",\"_blank\")", true);
    }
    protected void btnPaymentreceivedfrommpscsc_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Get_Payment_Received_From_MPSCSC_Details_From_Aug";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Rpt_Get_Payment_Received_From_MPSCSC_Details_From_Aug.aspx\",\"_blank\")", true);
    }
    protected void btnpendingbillgenerationFAug_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Get_Pending_Bill_Detail_August_to_Till_All_District";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Rpt_Get_Pending_Bill_Detail_August_to_Till_All_District.aspx\",\"_blank\")", true);
    }
    protected void btnbranchwisereceivedpayment_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Get_Payment_Received_From_MPSCSC_Details_From_Aug_Branch_Wise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Rpt_Get_Payment_Received_From_MPSCSC_Details_From_Aug_Branch_Wise.aspx\",\"_blank\")", true);
    }

    protected void btngodownwisepayment_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Get_Payment_Received_From_MPSCSC_Details_From_Aug_Godown_Wise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Rpt_Get_Payment_Received_From_MPSCSC_Details_From_Aug_Godown_Wise.aspx\",\"_blank\")", true);
    }

    protected void LinkButton77_Click1(object sender, EventArgs e)
    {

        //Session["reporturl"] = "";
        // Session["reporturl"] = "Rpt_Get_Payment_Received_From_MPSCSC_Details_From_Aug_Godown_Wise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Get_Payment_Details.aspx\",\"_blank\")", true);
    }

    protected void LinkButton82_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Get_Payment_Credit_From_MPWLC_To_Godown_Wise.aspx\",\"_blank\")", true);

    }

    protected void LinkButton83_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Get_BajraProcurement";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Wheat.aspx\",\"_blank\")", true);
    }

    protected void LinkButton85_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Get_BajraProcurement";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_CMS.aspx\",\"_blank\")", true);
    }

    protected void LinkButton86_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Get_PPayment";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_And_Month_Wise_Pending_Amount.aspx\",\"_blank\")", true);
    }

    protected void LinkButton87_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Get_PPayment";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Payment_Credit_From_MPWLC_To_Godown.aspx\",\"_blank\")", true);
    }

    protected void LinkButton88_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "RO_NAFED_WHRPrint_CMS_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);
    }

    protected void LinkButton89_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_NEFT_File";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Download_RO_EPF.aspx\",\"_blank\")", true);
    }

    protected void LinkButton90_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Procurement_Moong";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Moong.aspx\",\"_blank\")", true);
    }

    protected void LinkButton91_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Procurement_urad";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_urad.aspx\",\"_blank\")", true);
    }

    protected void LinkButton92_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Get_PPayment";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Pendancy_At_Verius_Level_New.aspx\",\"_blank\")", true);
    }

    protected void LinkButton93_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Kharif2021.aspx\",\"_blank\")", true);
    }

    protected void LinkButton94_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_KharifBajra2021.aspx\",\"_blank\")", true);

    }

    protected void LinkButton95_Click(object sender, EventArgs e)
    {

        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_KharifJwar2021.aspx\",\"_blank\")", true);
    }

    //protected void LinkButton96_Click(object sender, EventArgs e)
    //{
    //    ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_JVS_Payment_Against_Received_Payment_From_MPSCSC.aspx\",\"_blank\")", true);
    //}

    protected void LinkButton97_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Download_RO_EPF_Godown_Wise.aspx\",\"_blank\")", true);

    }
    protected void LinkButton99_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Rabi2022.aspx\",\"_blank\")", true);

    }
    protected void LinkButton100_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_CMS_2022.aspx\",\"_blank\")", true);

    }

    protected void LinkButton100_Click1(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Pending_Bill_Details.aspx\",\"_blank\")", true);
    }

    protected void LinkButton101_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Godown_Wise_Pending_Bill_Details_All_Godown.aspx\",\"_blank\")", true);
    }

    protected void LinkButton102_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Pandancy_at_Varius_Level_after_recieved_payment.aspx\",\"_blank\")", true);
    }

    protected void LinkButton103_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Pandancy_at_RM_Level_For_File_Generation.aspx\",\"_blank\")", true);
    }

    protected void LinkButton104_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Moong_Urad_2022_23.aspx\",\"_blank\")", true);
    }
    protected void LinkButton105_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Stock_Reconciliation_August_2022_Region.aspx\",\"_blank\")", true);
    }
    //protected void LinkButton106_Click(object sender, EventArgs e)
    //{
    //    ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"/../Region/Reports/Rpt_Get_Data_For_Selection_FCI_PDS.aspx\",\"_blank\")", true);
    //}
    protected void LinkButton106_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Get_Data_For_Selection_FCI_PDS.aspx\",\"_blank\")", true);
    }
    protected void LinkButton107_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Rpt_FIFO_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton108_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_mapping_for_rackpoint_godown_wise.aspx\",\"_blank\")", true);
    }

    protected void LinkButton109_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Pending_Warehouse_For_ElectroninWeighbrige_INformation.aspx\",\"_blank\")", true);
    }

    protected void LinkButton110_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Get_Stack_Count_For_FCI.aspx\",\"_blank\")", true);
    }
    protected void LinkButton111_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Stock_Reconciliation_August_2022_Region_With_Remark.aspx\",\"_blank\")", true);

    }

    protected void LinkButton112_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Kharif2022_23.aspx\",\"_blank\")", true);

    }

    protected void LinkButton113_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/View_Godown_Compalint_Region_Report.aspx\",\"_blank\")", true);
    }

    protected void LinkButton114_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Rabi2023.aspx\",\"_blank\")", true);
    }

    protected void LinkButton115_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_CMS_2023.aspx\",\"_blank\")", true);

    }

    protected void LinkButton116_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Moong_Urad_2023_24.aspx\",\"_blank\")", true);
    }

    protected void LinkButton117_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Summary_of_Panding_Payment_For_Godown_For_Region.aspx\",\"_blank\")", true);
    }

    protected void LinkButton118_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Moong_Urad_2023_E_WHR_For_Region.aspx\",\"_blank\")", true);

    }
    protected void btnPaddy202324_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Kharif2023_24.aspx\",\"_blank\")", true);
    }

    protected void LinkButton118_Click1(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Rabi2024_25.aspx\",\"_blank\")", true);
    }

    protected void LinkButton120_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_CMS_2024_25.aspx\",\"_blank\")", true);
    }

    protected void LinkButton121_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Moong_Urad_2024_25.aspx\",\"_blank\")", true);
    }

    protected void LinkButton122_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Moong_Urad_2024_E_WHR_For_Region.aspx\",\"_blank\")", true);
    }

    protected void LinkButton123_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/View_FAQ_Non_FAQ_DCC_Stock_position.aspx\",\"_blank\")", true);
    }

    protected void LinkButton124_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Godown_Wise_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton125_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Moong_Urad_E_WHR_For_Region_2024_25.aspx\",\"_blank\")", true);
    }

    protected void LinkButton126_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Update_Godown_Wise_Loss_Gain_By_RM.aspx\",\"_blank\")", true);
    }

    protected void LinkButton127_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Update_Loss_Gain_by_RM_For_RM.aspx\",\"_blank\")", true);
    }

    protected void LinkButton128_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton129_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_BOT_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton130_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Accounting/RM_Nafed_Pendding_Report.aspx\",\"_blank\")", true);
    }

    protected void LinkButton131_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Payment_Status_All.aspx\",\"_blank\")", true);
    }

    protected void LinkButton132_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_BM_not_Deduction_Bill.aspx\",\"_blank\")", true);
    }

    protected void LinkButton133_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Godown_Wise_region-Wise_Qty_Available.aspx\",\"_blank\")", true);
    }

    protected void LinkButton134_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Payment_Status_Alll_Commodity_Wise.aspx\",\"_blank\")", true);
    }

    protected void LinkButton135_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_CropYear_Wise_StockPosition.aspx\",\"_blank\")", true);
    }
    protected void LinkButton136_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_DepositorWise_PaymentStatusInformation.aspx\",\"_blank\")", true);
    }
    protected void LinkButton137_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_GodownWise_PaymentStatusInformation.aspx\",\"_blank\")", true);
    }
    protected void LinkButton138_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Godown_Wise_Payment_Status_For_Owned_Godown.aspx\",\"_blank\")", true);
    }
    protected void LinkButton139_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Payment_Status_Godown_Type_Wise.aspx\",\"_blank\")", true);
    }
    protected void LinkButton140_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Pending_Bill_Details.aspx\",\"_blank\")", true);
    }
    protected void LinkButton141_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_FAQ_Non_FAQ_DCC_Stock_position_New_RegionWise.aspx\",\"_blank\")", true);
    }
    protected void LinkButton142_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_SoyaBeens_2024_25.aspx\",\"_blank\")", true);
    }
    protected void LinkButton143_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_SoyaBeens_E_WHR_For_Region_2024_25.aspx\",\"_blank\")", true);
    }
    protected void LinkButton145_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Region_District_StockEntry_by_BM.aspx\",\"_blank\")", true);
    }
    protected void LinkButton146_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_RegionWise_GodownDCC_StockEntry.aspx\",\"_blank\")", true);
    }
    protected void LinkButton147_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Kharif2024_25.aspx\",\"_blank\")", true);

    }
    protected void LinkButton148_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/District_Wise_Rent_Bill_Status.aspx\",\"_blank\")", true);
    }
    protected void LinkButton149_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Godown_Bill_Wise_Payment_Status_For_NAFED.aspx\",\"_blank\")", true);
    }
    protected void LinkButton150_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Nafed_Region_Print_Bill.aspx\",\"_blank\")", true);
    }
    protected void LinkButton151_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/REgion_Wise_Branch_Wise_Jvs_Payment_Against_Received_Payment_Pending.aspx\",\"_blank\")", true);
    }
    protected void LinkButton152_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Pending_Rent_Bill_Details_Summary_For_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton153_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Rabi2025_26.aspx\",\"_blank\")", true);
    }
    protected void LinkButton153_Click1(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Rabi2025_26.aspx\",\"_blank\")", true);
    }
    protected void LinkButton154_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_CMS_2025_26.aspx\",\"_blank\")", true);
    }
    protected void LinkButton155_Click1(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Moong_Urad_2025_26.aspx\",\"_blank\")", true);
    }
    protected void LinkButton156_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Moong_Urad_2025_E_WHR_For_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton157_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Region_Stack_Wise_Fumigation.aspx\",\"_blank\")", true);
    }

    protected void LinkButton158_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Region_Stack_Wise_Moisture.aspx\",\"_blank\")", true);
    }

    protected void LinkButton159_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Region_Stack_Wise_Moisture_As_Per_System.aspx\",\"_blank\")", true);
    }

    protected void LinkButton160_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Moong_Urad_E_WHR_For_Region_2025_26.aspx\",\"_blank\")", true);
    }

    protected void LinkButton161_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Rpt_Pending_Bill_For_Generation.aspx\",\"_blank\")", true);
    }

    protected void LinkButton162_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Post_Mansoon_Fumigation_For_region.aspx\",\"_blank\")", true);
    }

    protected void LinkButton163_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Rpt_Vaccant_Capacity_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton164_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Kharif2025_26.aspx\",\"_blank\")", true);
    }

    protected void LinkButton165_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/NCCF_Storage_Bill_Status_Branch_And_RM.aspx\",\"_blank\")", true);
    }

    protected void LinkButton166_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_JVS_Payment_Against_Received_Payment_From_MPSCSC.aspx\",\"_blank\")", true);
    }

    protected void LinkButton96_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Kharif_Milets2025_26.aspx\",\"_blank\")", true);
    }

    protected void LinkButton167_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Godown_Mapping_With_WeightBridge_Region.aspx\",\"_blank\")", true);
    }

    protected void LinkButton168_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Pending_WB_For_Updation.aspx\",\"_blank\")", true);
    }

    protected void LinkButton169_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Pending_WB_to_Godown_Mapping.aspx\",\"_blank\")", true);
    }

    protected void LinkButton170_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Depositor_CommodityWise_Payment_Information.aspx\",\"_blank\")", true);
    }

    protected void LinkButton171_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Compare_Old_New_Depositor_Entry_Report.aspx\",\"_blank\")", true);
    }

    protected void LinkButton172_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/JIT_Bill_Details_For_Region.aspx\",\"_blank\")", true);
    }

    protected void LinkButton173_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Godown_Wise_Payment_Advise_Details.aspx\",\"_blank\")", true);
    }

    protected void LinkButton174_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Rabi2026_27.aspx\",\"_blank\")", true);
    }
    protected void LinkButton176_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Rabi2026_27.aspx\",\"_blank\")", true);
    }
    protected void LinkButton175_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_CMS_2026_27.aspx\",\"_blank\")", true);
    }

    protected void LinkButton177_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Get_RO_GRent_PassingOrder_Detail_From_Aug_Date_Wise.aspx\",\"_blank\")", true);
    }
    protected void LinkButton178_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/ReceivePaymentFromNCCF_For_Rigion.aspx\",\"_blank\")", true);
    }
    protected void LinkButton182_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/ReceivePaymentFromNAFED_For_Rigion.aspx\",\"_blank\")", true);
    }
    protected void LinkButton179_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Get_JIT_Payment_Status_MPWLC_To_Godown_Owner_For_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton180_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Get_Godown_Bill_Wise_Payment_Status_NCCF.aspx\",\"_blank\")", true);
    }

    protected void LinkButton181_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_NCCF_Godown_Wise_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton183_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Reports/Godown_Bill_Wise_Deduction_Report.aspx\",\"_blank\")", true);
    }

    protected void LinkButton186_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Get_Godown_Bill_Wise_Pending_DSC_From_DM_MPSCSC.aspx\",\"_blank\")", true);
    }

    protected void LinkButton184_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Region/Nafed_Wrong_Storage_Bill_Generation_Report.aspx\",\"_blank\")", true);

    }

    protected void LinkButton185_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Procurement_Moong_Urad_2026_27.aspx\",\"_blank\")", true);
    }
}

