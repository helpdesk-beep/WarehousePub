using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Drawing;

public partial class Inspections_RO_View_Complited_Inspewction_Summary_Region_Wise : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd;
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        lbl_user.Text = Session["UserName"].ToString();
        if (!IsPostBack)
        {
            //fillInpOff_Grid();
            GetDist();
        }
    }
    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillInpOff_Grid();

    }
    private void GetDist()
    {
        string strDist = "";
        strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT where Region_ID='" + Session["UserId"].ToString() + "' order by Regionnm";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
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
    public void fillInpOff_Grid()
    {
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();

            }
            SqlCommand cmd = new SqlCommand("Get_Annaxures_Summary_Region_Wise", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RegionID", ddl_dist.SelectedValue);
            //cmd.Parameters.AddWithValue("@Inspection_type_ID", ddlquater.SelectedValue);
            //cmd.Parameters.AddWithValue("@Verification_Type", ddlverification.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { conStr.Close(); }

            if (dt.Rows.Count > 0)
            {
                PVGrd.Visible = true;
                GrdOfficerPreviousInsp.DataSource = dt;
                GrdOfficerPreviousInsp.DataBind();
                lblOfficerList.Text = Convert.ToString(dt.Rows.Count);
            }
            else
            {
                PVGrd.Visible = false;
                GrdOfficerPreviousInsp.DataSource = null;
                GrdOfficerPreviousInsp.DataBind();
                lblOfficerList.Text = "0";
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
        finally
        { if (conStr.State == ConnectionState.Open) { conStr.Close(); } }
    }

    protected void GrdOfficerPreviousInsp_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GrdOfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Annexure_B")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];
            //Fetch value of Name.           
            string hdnRegion_ID = (row.FindControl("hdnRegion_ID") as HiddenField).Value;
            string hdnQuarterID = (row.FindControl("hdnQuarterID") as HiddenField).Value;
            string hdnInspTypeID = (row.FindControl("hdnInspTypeID") as HiddenField).Value;
            Session["hdnRegion_ID"] = hdnRegion_ID.ToString();
            Session["hdnQuarterID"] = hdnQuarterID.ToString();
            Session["hdnInspTypeID"] = hdnInspTypeID.ToString();
            Page.ClientScript.RegisterStartupScript(
   this.GetType(), "OpenWindow", "window.open('/Warehouse/Inspections/RO/View_Annaxure_B_Branch_Wise.aspx','_newtab');", true);
            // Response.Redirect("/Warehouse/Inspections/State/View_Annaxure_B.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        fillInpOff_Grid();
    }

}
