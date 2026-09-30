using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_States_StateReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)

    {

    }

    protected void lnk_paytnotrecd_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PaymentNotReceived";
        Response.Redirect("~/Reports/States/ReportViewer_Payment_Response.aspx");
    }

    protected void lnk_paytrecd_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PaymentRecieved";
        Response.Redirect("~/Reports/States/ReportViewer_Payment_Response.aspx");
    }

    protected void lnk_googlemap_Click1(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Geo_map_for_Distance_Prct_Godown.aspx");
    }

    protected void lnk_issuedqty_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Issued_Qty_State_Godownwise";
        Response.Redirect("~/Reports/States/ReportViewer_Issued_Quantity.aspx");
    }

    protected void lnk_Vctcpty_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Godown_Vacant_Storage_For_State";
        Response.Redirect("~/Reports/States/ReportViewer_Godown_Vacant_Storage_Cpty.aspx");
    }

    protected void lnk_vctmorethan1lac_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Godown_VacStorage_Morethan_1_Lac_MetricTon_For_State";
        Response.Redirect("~/Reports/States/ReportViewer_Godown_Vacant_Storage_Cpty.aspx");

    }

    protected void lnk_gdningo_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Godown_Details_Districtwise";
        Response.Redirect("~/Reports/States/Godown_Details.aspx");

    }
}