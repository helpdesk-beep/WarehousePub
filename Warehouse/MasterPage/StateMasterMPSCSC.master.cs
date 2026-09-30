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

public partial class MasterPage_StateMasterMPSCSC : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            UxUserName.Text = Session["UserName"].ToString();
            //string rol = Session["RoleId"].ToString();
            //if (rol == "12")
            //{
            //    hldeletereq.Visible = true;
            //    div_Delete.Visible = true;
            //    HyperLink4.Visible = true;
            //}
            //else if (rol == "9")
            //{
            //    // Rol 9 is Markfed

            //    hldeletereq.Visible = true;
            //    //   div_Delete.Visible = true;
            //    //   hlwhrreset.Visible = true;
            //    // HyperLink4.Visible = true;
            //}

        }
        else
        {
            Response.Redirect("../login.aspx");
        }
    }
}
