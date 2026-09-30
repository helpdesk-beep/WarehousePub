using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_Administration : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["lang"] != null)
                lang.Value = Session["lang"].ToString();
            if (Session["UserName"] != null)
                UserName.Value = Session["UserName"].ToString();
            if (Session["RoleId"] != null)
                RoleId.Value = Session["RoleId"].ToString();
            if (lang.Value != null)
            {
                if (lang.Value == "Hindi")
                {
                    spanHome.InnerText = Resources.hindi.spanHome;
                    hypHome.Text = Resources.hindi.hypHome;
                    spanReports.InnerText = Resources.hindi.spanReports;
                    hypStateReports.Text = Resources.hindi.hypStateReports;
                }
            }
            if (UserName.Value != null)
            {
                //UxUserName.Text = Session["UserName"].ToString();

                if (UserName.Value == "Chief Secretary" || UserName.Value == "Principal Secretary" || UserName.Value == "Commissioner Food")
                {
                    divHome.Visible = true;
                    spanHome.Visible = true;
                    hypHome.Visible = true;
                    hypStateReports.Visible = true;
                    divReports.Visible = true;
                    spanReports.Visible = true;

                }
                else if (UserName.Value == "MPSWLC")
                {
                    ///////////////////Div Reports//////////////////
                    hypRegionReports.Visible = false;
                    divReports.Visible = true;
                    spanReports.Visible = true;
                    hypDepotReports.Visible = true;
                    hypRegionReports.Visible = true;
                    hypStateReports.Visible = true;
                    hlnbranchmaster.Visible = true;
                    hldeletereq.Visible = true;
                    hlwhrreset.Visible = true;
                    HyperLink2.Visible = true;
                    HyperLink3.Visible = true;
                    HyperLink4.Visible = true;
                    HyperLink5.Visible = true;
                    HyperLink6.Visible = true;
                    HyperLink7.Visible = true;
                    //  hlngodownmaster.Visible = true;
                    ////////////////////Div Password////////////////
                    // divPass.Visible = true;
                    // spanChangePassword.Visible = true;
                    //  hypChangePassword.Visible = true;
                    /////////////////////Div Delete//////////////////
                    div_Delete.Visible = true;
                    spn_delte.Visible = true;
                    hlnkDeleteOprtrWhr.Visible = true;
                    hlnk_Delete_Open_Balance.Visible = true;
                    hlnkdeletereceipt.Visible = true;
                    hlnk_DeleteDO.Visible = true;
                    hlnk_Delete_GP.Visible = true;
                    hlnkupgatepass.Visible = true;
                    hlnkserchdo.Visible = true;
                    ////////////////////////////Div master////////////
                    divMasters.Visible = true;
                    spanMasters.Visible = true;
                    hypCommodityMaster.Visible = true;
                    hlnkratemaster.Visible = false;
                    hlnktaxmaster.Visible = true;
                    //Pvt Login
                    HlnPvtLogin.Visible = true;
                    Prmhrplink.Visible = true;
                    divEWHR.Visible = true;
                    Span1.Visible = true;
                    hplDelDSC.Visible = true;
                    div_Update.Visible = true;
                    HyperLink13.Visible = true;
                }
                else if (UserName.Value == "Business(MPWLC)")
                {
                    Prmhrplink.Visible = true;
                    //divMasters.Visible = true;
                    spanMasters.Visible = true;

                }
                else if (UserName.Value == "MD MPWLC")
                {
                    ///////////////////Div Reports//////////////////
                    //hypRegionReports.Visible = false;
                    //divReports.Visible = true;
                    //hypRegionReports.Visible = true;
                    //divReports.Visible = true;
                    //spanReports.Visible = true;
                }
                else
                {
                    if (UserName.Value == "2")
                    {
                        hypHome.NavigateUrl = "~/Welcome.aspx";
                    }
                    else if (UserName.Value == "9")
                    {

                        hypHome.NavigateUrl = "~/StatePages/StateReportsMfd.aspx";
                    }
                    else
                    {
                        hypHome.NavigateUrl = "~/IssueCenterLevel/Storage/Report_Region.aspx";
                    }

                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
    }
    protected void lb_logout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Default.aspx");
    }
    protected void lgs_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Default.aspx");
    }
}
