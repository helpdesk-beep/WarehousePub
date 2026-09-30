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

public partial class TribalGodown_Tribal_State_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if ((Session["UserId"].ToString() != null || Session["UserId"].ToString() != null) && (Session["UserName"].ToString() != null || Session["UserName"].ToString() != null))
            {
                if (!IsPostBack)
                {
                    lbluser.Text = Session["UserName"].ToString();
                }
            }
            else
            {
                Response.Redirect("StateRegionDistrictLogin.aspx");
            }
        }
        catch
        {
            Response.Redirect("StateRegionDistrictLogin.aspx");
        }
    }
    protected void HOR1_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "HOR1";
        Response.Redirect("AdminPanel.aspx");
    }
    protected void HOR3_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "HOR3";
        Response.Redirect("AdminPanel.aspx");
    }
    protected void HOR2_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "HOR2";
        Response.Redirect("AdminPanel.aspx");
    }
    protected void HOR4_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "HOR4";
        Response.Redirect("AdminPanel.aspx");
    }
    protected void HOR34_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "HOR34";
        Response.Redirect("AdminPanel.aspx");
    }
    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("StateRegionDistrictLogin.aspx");
    }
}
