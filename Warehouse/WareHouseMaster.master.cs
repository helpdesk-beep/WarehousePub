using System;
using System.Web;
using System.Web.UI;

public partial class WareHouseMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Agar session null hai ya username MPSWLC nahi hai
        if (Session["UserName"] == null || Session["UserName"].ToString() != "MPSWLC")
        {
            // Root par login.aspx bhej do
            Response.Redirect("~/login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        // Session null / clear
        Session.Clear();
        Session.Abandon();

        // Cache clear
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        // Login page par redirect
        Response.Redirect("~/login.aspx", false);
        Context.ApplicationInstance.CompleteRequest();
    }

}
