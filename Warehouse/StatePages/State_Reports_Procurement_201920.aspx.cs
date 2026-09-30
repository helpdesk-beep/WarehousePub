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

public partial class StatePages_State_Reports_Procurement_201920 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2019_20_DistrictWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_CSM_Proc201920_District_Wise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_CSM_Proc201920_District_Wise_GodownTypeWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_Arhar_Proc201920_District_Wise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_Arhar_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2019_20_eWHR";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Pending_Print_EWHR";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2019_20_eWHR_MPWLCGdwn";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton12_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Paddy_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CoarseGrain_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Paddy_Procurement_2019_20_MPWLC";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton15_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2020_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton16_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_Procurement_2020_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton17_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Proc_2012_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton18_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Paddy_Procurement_2020_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton19_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Bajra_Procurement_2020_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton20_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Jowar_Procurement_2020_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton21_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton22_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_Procurement_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton23_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton24_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Moong_Urad_Procurement_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton25_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton26_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Kharif2021.aspx\",\"_blank\")", true);
    }

    protected void LinkButton27_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_KharifBajra2021.aspx\",\"_blank\")", true);

    }

    protected void LinkButton28_Click(object sender, EventArgs e)
    {

        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_KharifJwar2021.aspx\",\"_blank\")", true);
    }

    protected void LinkButton29_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Cap_Pms.aspx\",\"_blank\")", true);

    }

    protected void LinkButton30_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Cap_PmsNot.aspx\",\"_blank\")", true);

    }

    protected void LinkButton31_Click(object sender, EventArgs e)
    {

        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Kharif2021_District.aspx\",\"_blank\")", true);
    }
    protected void LinkButton32_Click(object sender, EventArgs e)
    {

        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Rabi2022.aspx\",\"_blank\")", true);
    }
    protected void LinkButton33_Click(object sender, EventArgs e)
    {

        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Rabi2022_District.aspx\",\"_blank\")", true);
    }
    protected void LinkButton34_Click(object sender, EventArgs e)
    {

        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2022.aspx\",\"_blank\")", true);
    }
    protected void LinkButton35_Click(object sender, EventArgs e)
    {

        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2022_District.aspx\",\"_blank\")", true);
    }
    protected void LinkButton36_Click(object sender, EventArgs e)
    {

        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Moong_Urad_2022.aspx\",\"_blank\")", true);
    }

    protected void LinkButton37_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Kharif2023_24.aspx\",\"_blank\")", true);
    }

    protected void LinkButton38_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Kharif2023_24_With_PMS.aspx\",\"_blank\")", true);

    }
    protected void LinkButton39_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Jawar_Bajra_Kharif2023.aspx\",\"_blank\")", true);

    }


    protected void LinkButton40_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Rabi2023.aspx\",\"_blank\")", true);
    }

    protected void LinkButton41_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Rabi2023_District.aspx\",\"_blank\")", true);
    }

    protected void LinkButton42_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2023.aspx\",\"_blank\")", true);
    }

    protected void LinkButton43_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2023_District.aspx\",\"_blank\")", true);
    }

    protected void LinkButton44_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Moong_Urad_2023.aspx\",\"_blank\")", true);
        //https://mpsc.mp.nic.in/Warehouse/StatePages/Rpt_Procurement_Rabi2023_24_With_PMS.aspx
    }

    protected void LinkButton45_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Rabi2023_24_With_PMS.aspx\",\"_blank\")", true);
    }

    protected void LinkButton46_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Proc_2023_24";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton50_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Rabi2024_25.aspx\",\"_blank\")", true);
    }

    protected void LinkButton51_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Rabi2024_25_District.aspx\",\"_blank\")", true);
    }

    protected void LinkButton52_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2024_25.aspx\",\"_blank\")", true);
    }

    protected void LinkButton53_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2024_25_District.aspx\",\"_blank\")", true);
    }

    protected void LinkButton54_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Rabi2024_25_With_PMS.aspx\",\"_blank\")", true);
    }
    protected void LinkButton55_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Moong_Urad_2024_25.aspx\",\"_blank\")", true);
    }

    protected void LinkButton56_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Proc_2024_25";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton57_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_SoyaBeens_2024_25.aspx\",\"_blank\")", true);
    }

    protected void LinkButton58_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Kharif2024_25.aspx\",\"_blank\")", true);
    }
    protected void LinkButton59_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Rabi2025_26.aspx\",\"_blank\")", true);
    }

    protected void LinkButton60_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Rabi2025_26_District.aspx\",\"_blank\")", true);
    }

    protected void LinkButton61_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2025_26.aspx\",\"_blank\")", true);
    }

    protected void LinkButton62_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2025_26_District.aspx\",\"_blank\")", true);
    }

    protected void LinkButton63_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Moong_Urad_2025_26.aspx\",\"_blank\")", true);
    }

    protected void LinkButton64_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Kharif2025_26.aspx\",\"_blank\")", true);
    }
    protected void LinkButton65_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Rabi2026_27_District.aspx\",\"_blank\")", true);
    }
    protected void LinkButton66_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Rabi2026_27.aspx\",\"_blank\")", true);
    }

    protected void LinkButton67_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2026_27.aspx\",\"_blank\")", true);
    }

    protected void LinkButton68_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2026_27_District.aspx\",\"_blank\")", true);
    }

    protected void LinkButton69_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Moong_Urad_2026_27.aspx\",\"_blank\")", true);
    }
}
