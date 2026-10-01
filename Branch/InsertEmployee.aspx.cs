using System;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data;
using WLCBusinessLayer;

public partial class Admin_InsertEmployee : System.Web.UI.Page
{
    Admin clsAdmin = new Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;
        if (String.IsNullOrWhiteSpace(Convert.ToString(Session["username"])))
        {
            Response.Redirect("/Login/Login.aspx");
            return;
        }
        if (!IsPostBack)
        {
            fillDistrict();
        }
    }
    public void fillDistrict()
    {
        ListItem item = new ListItem("Select", "0");

        ddldistrict.Items.Clear();
        ddldistrict.Items.Add(item);

        DataTable dt = WebsiteLookups.GetDistricts(ddldistrict.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ListItem();
                item.Value = dt.Rows[i]["District_Id"].ToString();
                item.Text = dt.Rows[i]["District_Name"].ToString();

                ddldistrict.Items.Add(item);
            }


        }

    }
    public void fillBranch()
    {
        ListItem item = new ListItem("Select", "0");

        ddlbranch.Items.Clear();
        ddlbranch.Items.Add(item);

        DataTable dt = WebsiteLookups.GetBranches(ddldistrict.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ListItem();
                item.Value = dt.Rows[i]["BranchId"].ToString();
                item.Text = dt.Rows[i]["DepotName"].ToString();

                ddlbranch.Items.Add(item);
            }


        }

    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranch();
    }
    protected override void OnInit(System.EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrWhiteSpace(Convert.ToString(Session["username"])))
        {
            Response.Redirect("/Login/Login.aspx");
            return;
        }

        try
        {
            string image = EmployeeImageUpload.Save(FileUpload1, "../Upload/EmployeeImages/");
            string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            using (SqlCommand cmd = new SqlCommand("insert_employee", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpNameH", txtNameH.Value);
                cmd.Parameters.AddWithValue("@EmpNameE", txtNameE.Value);
                cmd.Parameters.AddWithValue("@DOB", datepicker.Value);
                cmd.Parameters.AddWithValue("@DOJ", txtDOJ.Value);
                cmd.Parameters.AddWithValue("@Designation", txtDesignation.Value);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Value);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Value);
                cmd.Parameters.AddWithValue("@Image", image);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            lblErr.Text = "Employee Details Inserted Successfuly";
            lblErr.ForeColor = System.Drawing.Color.ForestGreen;
            txtNameH.Value = "";
            txtNameE.Value = "";
            txtMobile.Value = "";
            txtEmail.Value = "";
            txtDesignation.Value = "";
            datepicker.Value = "";
            txtDOJ.Value = "";
        }
        catch (ArgumentException ex)
        {
            lblErr.ForeColor = System.Drawing.Color.Red;
            lblErr.Text = Server.HtmlEncode(ex.Message);
        }
        catch (Exception)
        {
            lblErr.ForeColor = System.Drawing.Color.Red;
            lblErr.Text = "Unable to save employee details.";
        }
    }
}