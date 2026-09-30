using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_StateMasterNafed : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            UxUserName.Text = Session["UserName"].ToString();
            string rol = Session["RoleId"].ToString();
            if (rol == "11")
            {
                //hldeletereq.Visible = true;
                //div_Delete.Visible = true;
                //HyperLink4.Visible = true;

            }
            //else if (rol == "9")
            //{
            //    hldeletereq.Visible = true;
            //    div_Delete.Visible = true;
            //    hlwhrreset.Visible = true;
            //    // HyperLink4.Visible = true;
            //}

        }
        else
        {
            Response.Redirect("../login.aspx");
        }

    }
}
