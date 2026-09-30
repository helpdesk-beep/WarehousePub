using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Inspections_TQRO_UploadDocuments : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd;
    DataTable dt = new DataTable();
    string PFID = "";
    string branchid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["University_E"] == null)
        //{
        //    Response.Redirect("/VC_Application/Default.aspx");
        //}
        PFID = Session["UserId"].ToString();
       // branchid = Session["hdnbranchid"].ToString();
        if (!IsPostBack)
        {
            GetRegion();
            //fillBranchDetails();
            fillFinsncilYear();
            Fill12thmarksheetdata();
            //Fill12thmarksheetdata();
            //FillGeneralInformation();
        }
    }
    public void fillFinsncilYear()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Fianancial_Year_For_inspection", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            ddlfinancialyear.DataSource = cmd.ExecuteReader();
            ddlfinancialyear.DataTextField = "Financial_Year";
            ddlfinancialyear.DataValueField = "Financial_Year";
            ddlfinancialyear.DataBind();
            ddlfinancialyear.Items.Insert(0, new ListItem("--Select Financial Year--", "0"));
            con.Close();
        }
    }
    private void GetRegion()
    {
        string strDist = "";
        strDist = "SELECT District_Id,District_Name FROM tbl_MetaData_DISTRICT where Region_ID= '" + Session["UserId"].ToString() + "' order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, conStr);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_dist.DataSource = ds.Tables[0];
            ddl_dist.DataTextField = "District_Name";
            ddl_dist.DataValueField = "District_Id";
            ddl_dist.DataBind();
            ddl_dist.Items.Insert(0, "--Select--");
        }
        else
        {
            ddl_dist.Items.Insert(0, "--Select--");
        }
    }
   
    public void fillBranchDetails()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("sp_get_branch", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DistrictId", ddl_dist.SelectedValue);
            con.Open();
            ddlbranch.DataSource = cmd.ExecuteReader();
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "BranchId";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
            ddlbranch.SelectedValue = branchid.ToString();
            //ddlbranch.Enabled = false;
            con.Close();
        }
    }
    
    public void Fill12thmarksheetdata()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlCommand cmd = new SqlCommand("Show_Uploaded_Document_For_TQRO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RegionID", Session["UserId"].ToString());
        if (ddlbranch.SelectedValue == "-- Select Branch --")
        {
            cmd.Parameters.AddWithValue("@BranchID", "0");
        }
        else
        {
            cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
        }
        if (ddlquater.SelectedValue == "--Select--")
        {
            cmd.Parameters.AddWithValue("@QuaterID", 0);
        }
        else
        {
            cmd.Parameters.AddWithValue("@QuaterID", ddlquater.SelectedValue);
        }
        if (ddlverification.SelectedValue == "--Select--")
        {
            cmd.Parameters.AddWithValue("@InspectionTypeID", 0);
        }
        else
        {
            cmd.Parameters.AddWithValue("@InspectionTypeID", ddlverification.SelectedValue);
        }
        if (ddlfinancialyear.SelectedValue == "--Select Financial Year--")
        {
            cmd.Parameters.AddWithValue("@FinancialYear", "0");
        }
        else
        {
            cmd.Parameters.AddWithValue("@FinancialYear", ddlfinancialyear.SelectedValue);
        }
        //cmd.Parameters.AddWithValue("@QuaterID", Session["hdninsp_type_id"].ToString());
        //cmd.Parameters.AddWithValue("@InspectionTypeID", Session["hdnVerificationType"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        if (dt.Rows.Count > 0)
        {
            GrdOfficerPreviousInsp.DataSource = dt;
            GrdOfficerPreviousInsp.DataBind();
        }
    }
   
    protected void GrdOfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {       
        if (e.CommandName == "Overallinsp")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];

            //Fetch value of Name.
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnVerificationType = (row.FindControl("hdnVerificationType") as HiddenField).Value;
            string hdnquatertype = (row.FindControl("hdnquatertype") as HiddenField).Value;
            string hdnfinancialyear = (row.FindControl("hdnfinancialyear") as HiddenField).Value;
            string hdnemployeeid = (row.FindControl("hdnemployeeid") as HiddenField).Value;
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnVerificationType"] = hdnVerificationType.ToString();
            Session["hdnquatertype"] = hdnquatertype.ToString();
            Session["hdnfinancialyear"] = hdnfinancialyear.ToString();
            Session["hdnemployeeid"] = hdnemployeeid.ToString();
            Page.ClientScript.RegisterStartupScript(
 this.GetType(), "OpenWindow", "window.open('/Warehouse/Inspections/TQRO/ViewUploadedDocument.aspx','_newtab');", true);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {

    }

  
    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranchDetails();
    }
}