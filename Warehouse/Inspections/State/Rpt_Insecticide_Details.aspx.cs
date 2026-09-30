using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

public partial class Inspections_State_Rpt_Insecticide_Details : System.Web.UI.Page
{
    public SqlConnection con_JVS = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string RegionId = Request.QueryString["RegionId"];
            string InsecticideId = Request.QueryString["Insecticide_ID"];

            LoadData(RegionId, InsecticideId);
        }
    }
    private void LoadData(string RegionId, string InsecticideId)
    {
        SqlCommand cmd = new SqlCommand("Get_Branch_Wise_Insecticide_Details_by_RMID", con_JVS);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Regionid", RegionId);
        cmd.Parameters.AddWithValue("@Insecticide_ID", InsecticideId);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        da.Fill(dt);

        grdDetails.DataSource = dt;
        grdDetails.DataBind();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Inspections/State/Rpt_Get_Insecticide_Details_For_RM.aspx");
    }
}