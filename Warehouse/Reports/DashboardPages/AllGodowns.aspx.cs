using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Reports_DashboardPages_AllGodowns : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
                LoadGodownData();
        }
    }

    private void LoadGodownData()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = "select DISTINCT BranchId, Godown_ID, Godown_Name, Godown_Capacity, Hired_Type, Storage_Type, Godown_Mobile FROM tbl_MetaData_GODOWN_2018 Order BY Godown_Name";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvGodown.DataSource = dt;
                gvGodown.DataBind();
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../NewStorageCapacityReport/DashboardNew.aspx"); // jaha se aaye ho
    }
}