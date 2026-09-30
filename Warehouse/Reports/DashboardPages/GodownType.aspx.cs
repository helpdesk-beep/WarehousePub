using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Activities.Expressions;
public partial class Reports_DashboardPages_GodownType : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Type"] != null)
            {
                string type = Request.QueryString["Type"].ToString();
                lblType.Text = "Hired Type : " + type;
                LoadGodown(type);
            }
        }
    }

    private void LoadGodown(string type)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            //string query = @"select DISTINCT BranchId, Godown_ID, Godown_Name, Godown_Capacity, Hired_Type, Storage_Type, Godown_Mobile FROM tbl_MetaData_GODOWN_2018 where Hired_Type= @Type";
            //string query = @"SELECT DISTINCT BranchId, Godown_ID, Godown_Name, Godown_Capacity, CASE WHEN Hired_Type IN ('Joint Venture(JV)', 'WDRA') THEN 'JV + WDRA' ELSE Hired_Type END AS Hired_Type, Storage_Type, Godown_Mobile FROM tbl_MetaData_GODOWN_2018 WHERE CASE WHEN Hired_Type IN ('Joint Venture(JV)', 'WDRA') THEN 'JV + WDRA' ELSE Hired_Type END = '"+ type + "'";

            string query = @"SELECT DISTINCT
    BranchId, 
    Godown_ID, 
    Godown_Name, 
    Godown_Capacity,

    CASE
        WHEN Hired_Type IN('Joint Venture(JV)', 'WDRA') 
            THEN 'JV + WDRA'
        ELSE Hired_Type
    END AS Hired_Type,

    Storage_Type, 
    Godown_Mobile
FROM tbl_MetaData_GODOWN_2018
WHERE
    REPLACE(REPLACE(
        CASE
            WHEN Hired_Type IN('Joint Venture(JV)', 'WDRA')
                THEN 'JV + WDRA'
            ELSE Hired_Type
        END
    , ' ', ''), '+', '')
    =
    REPLACE(REPLACE('"+type+"', ' ', ''), '+', '')";


            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Type", type);

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