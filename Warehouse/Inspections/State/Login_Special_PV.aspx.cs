using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_Login_Special_PV : System.Web.UI.Page
{
    private const string ADMIN_PASSWORD = "admin@123";
    private static readonly HashSet<string> AllowedMobiles =
       new HashSet<string>()
       {
            "9575629234",
            "9826681780",
            "9425457367",
            "9752087392",
            "9981961530",
            "7987011210",
            "8103862528",
            "9425028216",
            "9893246082"
       };
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlAdmin.Visible = ddlUserType.SelectedValue == "Admin";
        pnlOfficer.Visible = ddlUserType.SelectedValue == "Officer";
        lblMsg.Text = "";
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (ddlUserType.SelectedValue == "Admin")
            {
                if (txtAdminPassword.Text == ADMIN_PASSWORD)
                {
                    Session["UserType"] = "Admin";
                    Response.Redirect("Inspection_Officers.aspx");
                }
                else
                {
                    lblMsg.Text = "Invalid admin password";
                }
            }
            else if (ddlUserType.SelectedValue == "Officer")
            {
                if (txtMobile.Text.Length == 10)
                {
                    string mobile = txtMobile.Text.Trim();
                    foreach (char c in mobile)
                    {
                        if (!char.IsDigit(c))
                        {
                            lblMsg.Text = "Enter valid 10-digit mobile number";
                            return;
                        }
                    }
                    if (!AllowedMobiles.Contains(mobile))
                    {
                        lblMsg.Text = "No record found";
                        return;
                    }
                    Session["Mobile"] = txtMobile.Text;
                    Session["UserType"] = "Officer";
                    //Response.Redirect("Inspection_Officers.aspx");
                    Response.Redirect("~/Inspections/State/PVInspectionReport.aspx");
                }
                else
                {
                    lblMsg.Text = "Enter valid 10-digit mobile number";
                }
            }
            else
            {
                lblMsg.Text = "Please select user type";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }
}