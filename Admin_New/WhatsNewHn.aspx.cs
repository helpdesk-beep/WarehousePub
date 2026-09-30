using System;
using System.Web;

public partial class WhatsNewHn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;

        if (!this.IsPostBack)
        {
            if (Session["Id"] != null)
            {
                int id = Convert.ToInt32(Session["Id"]);
                string embed = "<object data=\"{0}{1}\" type=\"application/pdf\" width=\"1110px\" height=\"1130px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}{1}&download=1\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = string.Format(embed, ResolveUrl("/Handler/ViewWhatsNewHn.ashx?Id="), id);
            }
        }

    }
    protected override void OnInit(System.EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;
    }
}