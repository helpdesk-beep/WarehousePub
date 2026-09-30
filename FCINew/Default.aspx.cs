using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FCI_Default : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDashboard();
            LoadRecentEmployees();
        }
    }

    void LoadDashboard()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT 
                    COUNT(*) Total,
                    SUM(CASE WHEN Status = 1 THEN 1 ELSE 0 END) Active,
                    SUM(CASE WHEN Status = 0 THEN 1 ELSE 0 END) Inactive,
                    SUM(CASE WHEN CAST(Allocated_Date AS DATE) = CAST(GETDATE() AS DATE) THEN 1 ELSE 0 END) Today
                FROM FCI_Employee Where 1=1", con))
            {
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        lblTotal.Text = HttpUtility.HtmlEncode(dr["Total"] != DBNull.Value ? dr["Total"].ToString() : "0");
                        lblActive.Text = HttpUtility.HtmlEncode(dr["Active"] != DBNull.Value ? dr["Active"].ToString() : "0");
                        lblInactive.Text = HttpUtility.HtmlEncode(dr["Inactive"] != DBNull.Value ? dr["Inactive"].ToString() : "0");
                        lblToday.Text = HttpUtility.HtmlEncode(dr["Today"] != DBNull.Value ? dr["Today"].ToString() : "0");
                    }
                }
            }
        }
    }

    void LoadRecentEmployees()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlDataAdapter da = new SqlDataAdapter(@"
                SELECT TOP 10 
                    E.FCI_ID,
                    E.Emp_Name,
                    E.Mobile_No,
                    D.District_Name,
                    DP.DepotName,
                    E.Designation,
                    E.Status,
                    E.Allocated_Date
                FROM FCI_Employee E

                LEFT JOIN Intergrated_MP_STORAGE.dbo.tbl_MetaData_DISTRICT D 
                    ON E.District_Id = D.District_Id

                LEFT JOIN Intergrated_MP_STORAGE.dbo.tbl_MetaData_DEPOT DP 
                    ON E.Branch_Id = DP.BranchId

                ORDER BY E.FCI_ID DESC", con))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvRecent.DataSource = dt;
                gvRecent.DataBind();
            }
        }
    }
}