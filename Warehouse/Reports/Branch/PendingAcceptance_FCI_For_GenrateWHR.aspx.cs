using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Branch_PendingAcceptance_FCI_For_GenrateWHR : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid = "";
    SqlTransaction sqltran;
    string depottype = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillFinsncilYear();
            //if (Session["UserName"] != null)
            //    //fillgrid();
            //    fillFinsncilYear();
            //else
            //    Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void fillFinsncilYear()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Crop_Year_From_CSMC_For_CMR", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            ddlcropyear.DataSource = cmd.ExecuteReader();
            ddlcropyear.DataTextField = "CropYear";
            ddlcropyear.DataValueField = "CropYear";
            ddlcropyear.DataBind();
            ddlcropyear.Items.Insert(0, new ListItem("--Select Crop Year--", "0"));
            con.Close();
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_CMR_Pending_Acceptance_Details_FCI_For_GenrateWHR", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DistrictID", Session["Depot_DistID"].ToString());
                cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
                cmd.Parameters.AddWithValue("@CropYEar", ddlcropyear.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Depositor_Gridview.DataSource = dt;
                            Depositor_Gridview.DataBind();
                        }
                        else
                        {
                            Depositor_Gridview.DataSource = dt;
                            Depositor_Gridview.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void Depositor_Gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int indx = e.NewPageIndex;
        Depositor_Gridview.PageIndex = e.NewPageIndex;
        fillgrid();
    }

    protected void ddlcropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}