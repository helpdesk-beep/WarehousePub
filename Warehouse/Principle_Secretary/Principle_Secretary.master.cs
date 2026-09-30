using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class Principle_Secretary_Principle_Secretary : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lang"] != null)
        {
            if (Session["lang"].ToString() == "Hindi")
            {
                spanHome.InnerText = Resources.hindi.spanHome;
                hypHome.Text = Resources.hindi.hypHome;
                spanReports.InnerText = Resources.hindi.spanReports;
                hypStateReports.Text = Resources.hindi.hypStateReports;
            }
        }
        if (Session["UserName"] != null)
        {
            UxUserName.Text = Session["UserName"].ToString();

            if (Session["UserName"].ToString() == "Chief Secretary" || Session["UserName"].ToString() == "Principal Secretary" || Session["UserName"].ToString() == "Commissioner Food" || Session["UserName"].ToString() == "MD MPWLC")
            {
                divHome.Visible = true;
                spanHome.Visible = true;
                hypHome.Visible = true;
                hypStateReports.Visible = true;
                divReports.Visible = true;
                spanReports.Visible = true;
                hypstockreport.Visible = true;
            }
            else if (Session["UserName"].ToString() == "MPSWLC")
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
                divBillAcc.Visible = false;
                HyperLink14.Visible = true;
                HyperLink21.Visible = true;
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
                HlnPMSLogin.Visible = true;
                Prmhrplink.Visible = true;
                divEWHR.Visible = true;
                divDeduction.Visible = true;
                Span1.Visible = true;
                hplDelDSC.Visible = true;
                HyperLink22.Visible = true;
                HyperLink23.Visible = true;
                HyperLink24.Visible = true;
                HyperLink25.Visible = true;
                HyperLink26.Visible = true;
                HyperLink27.Visible = true;
                HyperLink28.Visible = true;
                hypSearchgodown.Visible = true;
            }
            else if (Session["UserName"].ToString() == "Business(MPWLC)")
            {
                Prmhrplink.Visible = true;
                //divMasters.Visible = true;
                spanMasters.Visible = true;

            }
            else
            {
                if (Session["RoleId"].ToString() == "2")
                {
                    hypHome.NavigateUrl = "~/Welcome.aspx";
                }
                else if (Session["RoleId"].ToString() == "9")
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
            Response.Redirect("~/Login.aspx");
        }
    }
}
