using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Inspections_TQHO_UploadDocuments : System.Web.UI.Page
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
        strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Region_ID";
        SqlDataAdapter da = new SqlDataAdapter(strDist, conStr);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_dist.DataSource = ds.Tables[0];
            ddl_dist.DataTextField = "Regionnm";
            ddl_dist.DataValueField = "Region_ID";
            ddl_dist.DataBind();
            ddl_dist.Items.Insert(0, "--Select--");
        }
        else
        {
            ddl_dist.Items.Insert(0, "--Select--");
        }
    }

    public void Fill12thmarksheetdata()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlCommand cmd = new SqlCommand("Show_Uploaded_Document_For_TQHO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddl_dist.SelectedValue == "--Select--")
        {
            cmd.Parameters.AddWithValue("@RegionID", 0);
        }
        else
        {
            cmd.Parameters.AddWithValue("@RegionID", ddl_dist.SelectedValue);
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
          
            string lblOfficer_Name = (row.FindControl("lblOfficer_Name") as Label).Text;
            string lblDesignation = (row.FindControl("lblDesignation") as Label).Text;
            string lblDepotname = (row.FindControl("lblDepotname") as Label).Text;
            string lblInsp_Type = (row.FindControl("lblInsp_Type") as Label).Text;
            string lblQuater = (row.FindControl("lblQuater") as Label).Text;
            string lblInsp_Month = (row.FindControl("lblQuater") as Label).Text;

            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnVerificationType"] = hdnVerificationType.ToString();
            Session["hdnquatertype"] = hdnquatertype.ToString();
            Session["hdnfinancialyear"] = hdnfinancialyear.ToString();
            Session["hdnemployeeid"] = hdnemployeeid.ToString();
           
            Session["lblOfficer_Name"] = lblOfficer_Name.ToString();
            Session["lblDesignation"] = lblDesignation.ToString();
            Session["lblDepotname"] = lblDepotname.ToString();
            Session["lblInsp_Type"] = lblInsp_Type.ToString();
            Session["lblQuater"] = lblQuater.ToString();
            Session["lblInsp_Month"] = lblInsp_Month.ToString();
            
            Page.ClientScript.RegisterStartupScript(
 this.GetType(), "OpenWindow", "window.open('/Warehouse/Inspections/TQHO/ViewUploadedDocument.aspx','_newtab');", true);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {

    }


    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
       // fillBranchDetails();
    }
}