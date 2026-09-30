using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            UxUserName.Text = Session["UserName"].ToString();
            if(Session["UserName"].ToString()== "BHOPAL" || Session["UserName"].ToString() == "JABALPUR" || Session["UserName"].ToString() == "UJJAIN" || Session["UserName"].ToString() == "REWA" || Session["UserName"].ToString() == "NARMADAPURAM")
            {
                HyperLink15.Visible = false;
                HyperLink24.Visible = false;
                HyperLink45.Visible = true;
                HyperLink46.Visible = true;
                HyperLink39.Visible = true;
                HyperLink55.Visible = false;
                HyperLink58.Visible = true;
                HyperLink57.Visible = false;
                HyperLink59.Visible = false;
                HyperLink62.Visible = true;
            }
            else
            {
                HyperLink15.Visible = true;
                HyperLink24.Visible = true;
                HyperLink45.Visible = false;
                HyperLink46.Visible = false;
                HyperLink39.Visible = false;
                HyperLink55.Visible = true;
                HyperLink58.Visible = false;
                HyperLink57.Visible = true;
                HyperLink59.Visible = true;
                HyperLink62.Visible = false;

            }
        }
    }
}
