using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_NewStorageCapacityReport_DashboardNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBranchTypes();
            BindGodownTypes();
            GetTotalBranch();
            GetTotalGodown();
        }
    }
    string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    private void BindBranchTypes()
    {

        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = @"SELECT DepoBelongs, COUNT(*) AS Total_Count
                         FROM tbl_MetaData_DEPOT
                         WHERE DepoBelongs IS NOT NULL
                         AND LTRIM(RTRIM(DepoBelongs)) <> ''
                         GROUP BY DepoBelongs
                         ORDER BY Total_Count DESC";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptBranchType.DataSource = dt;
                rptBranchType.DataBind();
            }
        }
    }

    private void GetTotalBranch()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = "SELECT COUNT(*) FROM tbl_MetaData_DEPOT";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                int total = Convert.ToInt32(cmd.ExecuteScalar());

                totalBranches.InnerText = total.ToString("N0"); // comma format 1,234
            }
        }
    }

    private void GetTotalGodown()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = "SELECT COUNT(*) AS Total_Count FROM tbl_MetaData_GODOWN_2018";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                int total = Convert.ToInt32(cmd.ExecuteScalar());

                totalGodowns.InnerText = total.ToString("N0"); // comma format 1,234
            }
        }
    }

    private void BindGodownTypes()
    {

        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = @"
        SELECT 
            CASE 
                WHEN Hired_Type IN ('Joint Venture(JV)', 'WDRA') 
                    THEN 'JV + WDRA'
                ELSE Hired_Type
            END AS Hired_Type_Group,
            COUNT(*) AS Total_Count
        FROM tbl_MetaData_GODOWN_2018
        GROUP BY 
            CASE 
                WHEN Hired_Type IN ('Joint Venture(JV)', 'WDRA') 
                    THEN 'JV + WDRA'
                ELSE Hired_Type
            END
        ORDER BY Total_Count DESC";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            rptGodownTypes.DataSource = dt;
            rptGodownTypes.DataBind();
        }
    }
}