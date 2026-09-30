using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Inspections_State_BranchWisePendingInspection : System.Web.UI.Page
{
    string conn = ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (Session["GodownParams"] == null)
            {
                //lblCount.Text = "Session Expired";
                return;
            }

            string[] param = (string[])Session["GodownParams"];

            string regionID = param[0];
            string inspectionTypeID = param[1];
            string verificationType = param[2];
            string financialYear = param[3];
           // string Inspection_month_ID = param[4];

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Branch_Wise_Pendding_Inspection_Status", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Region_ID", regionID);
                    cmd.Parameters.AddWithValue("@Inspection_type_ID", inspectionTypeID);
                    cmd.Parameters.AddWithValue("@Verification_Type", verificationType);
                    cmd.Parameters.AddWithValue("@Financial_year", financialYear);
                    //cmd.Parameters.AddWithValue("@Inspection_month_ID", Inspection_month_ID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvPendingInspection.DataSource = dt;
                    gvPendingInspection.DataBind();
                    //lblTotalOnlineStackCard.Text = lblTotalOnlineStackCard.ToString();
                    //lblTotalInspStackCard.Text = lblTotalInspStackCard.ToString();
                    //lblTotalPVBagsCard.Text = lblTotalPVBagsCard.ToString();
                    //lblTotalSpillageCard.Text = lblTotalSpillageCard.ToString();
                    //lblCount.Text = dt.Rows.Count.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            // lblCount.Text = "Error";
        }
    }
    //private void BindData()
    //{
    //    using (SqlConnection con = new SqlConnection(conn))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("Get_Branch_Wise_Pendding_Inspection_Status", con))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;

    //            cmd.Parameters.AddWithValue("@Financial_year", Request.QueryString["FY"]);
    //            cmd.Parameters.AddWithValue("@Inspection_type_ID", Request.QueryString["InspectionType"]);
    //            cmd.Parameters.AddWithValue("@Region_ID", Request.QueryString["RegionID"]);
    //            cmd.Parameters.AddWithValue("@Verification_Type", Request.QueryString["VerificationType"]);

    //            SqlDataAdapter da = new SqlDataAdapter(cmd);

    //            DataTable dt = new DataTable();

    //            da.Fill(dt);

    //            gvPendingInspection.DataSource = dt;
    //            gvPendingInspection.DataBind();
    //        }
    //    }
    //}
}