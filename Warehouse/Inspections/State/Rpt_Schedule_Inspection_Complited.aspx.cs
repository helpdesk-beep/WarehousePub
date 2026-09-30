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

public partial class Inspections_State_Rpt_Schedule_Inspection_Complited : System.Web.UI.Page
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
        strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Regionnm";
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
            SqlCommand cmd = new SqlCommand("Get_Employee_Details_Complited_Inspection", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Region_ID", ddl_dist.SelectedValue);
            cmd.Parameters.AddWithValue("@Inspection_type_ID", ddlquater.SelectedValue);
            cmd.Parameters.AddWithValue("@Year", ddlyear.SelectedValue);
            cmd.Parameters.AddWithValue("@Verification_Type", ddlverification.SelectedValue);
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnVerificationType = (HiddenField)e.Row.FindControl("hdnVerificationType");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            if (lblStatus.Text == "Pending")
            {
                lblStatus.BackColor = Color.Red;
            }
            else
            {
                lblStatus.BackColor = Color.Green;
            }
        }
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
            string hdnemployeeid = (row.FindControl("hdnemployeeid") as HiddenField).Value;
            string hdnInspection_ID = (row.FindControl("hdnInspection_ID") as HiddenField).Value;
            string hdndistrictid = (row.FindControl("hdndistrictid") as HiddenField).Value;
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnorderno = (row.FindControl("lblOrder_No") as Label).Text;
            string hdnorderdate = (row.FindControl("lblOrder_Date") as Label).Text;
            string lblOfficer_Name = (row.FindControl("lblOfficer_Name") as Label).Text;
            Session["hdndistrictid"] = hdndistrictid.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnInspection_ID"] = hdnInspection_ID.ToString();
            Session["hdnorderno"] = hdnorderno.ToString();
            Session["hdnorderdate"] = hdnorderdate.ToString();
            Session["lblOfficer_Name"] = lblOfficer_Name.ToString();
            Session["hdnemployeeid"] = hdnemployeeid.ToString();
            Page.ClientScript.RegisterStartupScript(
   this.GetType(), "OpenWindow", "window.open('/Warehouse/Inspections/State/View_Annaxure_B.aspx','_newtab');", true);
            // Response.Redirect("/Warehouse/Inspections/State/View_Annaxure_B.aspx");
        }
        if (e.CommandName == "GadnaPatrak")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];
            //Fetch value of Name.           
            string hdnInspection_ID = (row.FindControl("hdnInspection_ID") as HiddenField).Value;
            string hdndistrictid = (row.FindControl("hdndistrictid") as HiddenField).Value;
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnorderno = (row.FindControl("lblOrder_No") as Label).Text;
            string hdnorderdate = (row.FindControl("lblOrder_Date") as Label).Text;
            Session["hdndistrictid"] = hdndistrictid.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnInspection_ID"] = hdnInspection_ID.ToString();
            Session["hdnorderno"] = hdnorderno.ToString();
            Session["hdnorderdate"] = hdnorderdate.ToString();
            //Response.Redirect("/Warehouse/Inspections/State/View_Gadna_Patrak.aspx");
            Page.ClientScript.RegisterStartupScript(
   this.GetType(), "OpenWindow", "window.open('/Warehouse/Inspections/State/View_Gadna_Patrak.aspx','_newtab');", true);

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ddlverification.SelectedValue == "1")
        {
            fillInpOff_Grid();
        }
        else if (ddlverification.SelectedValue == "2")
        {
            fillInpOff_Grid();
        }
        else if (ddlverification.SelectedValue == "3")
        {
            fillInpOff_Grid();
        }
        else
        {
            PVGrd.Visible = false;
            PVGenGrd.Visible = false;
        }
    }

}
