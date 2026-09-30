using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DashboardPages_AllBranch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDepot();
        }
    }
    string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    private void BindDepot()
    {

        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = "SELECT DISTINCT * FROM tbl_MetaData_DEPOT ORDER BY DepotName";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
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
