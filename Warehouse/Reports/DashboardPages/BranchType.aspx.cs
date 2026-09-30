using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Reports_DashboardPages_BranchType : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["DepoBelongs"] != null)
            {
                string type = Request.QueryString["DepoBelongs"].ToString();
                lblType.Text = "Branch Type : " + type;
                LoadGodown(type);
            }
        }
    }

    private void LoadGodown(string type)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = @"select DISTINCT  * from tbl_MetaData_DEPOT where DepoBelongs= @Type";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Type", type);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvDepot.DataSource = dt;
                gvDepot.DataBind();
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../NewStorageCapacityReport/DashboardNew.aspx"); // jaha se aaye ho
    }

}