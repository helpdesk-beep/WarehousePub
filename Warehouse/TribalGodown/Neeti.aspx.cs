using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TribalGodown_Neeti : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        try
        {
            lbluser.Text = Session["fname"].ToString() + " " + Session["mname"].ToString() + " " + Session["lname"].ToString();
        }
        catch (Exception ex)
        {
            Response.Redirect("UserReg.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            Session["Agry"] = "agry";
            Response.Redirect("Registration.aspx");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपने checkbox को टिक नहीं किया है')", true);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("UserReg.aspx");
    }
}