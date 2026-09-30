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

public partial class Inspections_TQRO_RO_View_All_Annaxures_Reports : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd;
    DataTable dt = new DataTable();
    string PFID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        PFID = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            fillFinsncilYear();
            GetDist();
            ddl_dist.SelectedValue = Session["UserId"].ToString();
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
            ddl_dist.SelectedValue = Session["UserId"].ToString();

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
            SqlCommand cmd = new SqlCommand("Get_Complite_Inspection_Details_Employee_Wise", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Region_ID", ddl_dist.SelectedValue);
            cmd.Parameters.AddWithValue("@Quater_ID", ddlquater.SelectedValue);
            cmd.Parameters.AddWithValue("@Ins_type_ID", ddlverification.SelectedValue);
            cmd.Parameters.AddWithValue("@Financial_year", ddlfinancialyear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { conStr.Close(); }

            if (dt.Rows.Count > 0)
            {
                GrdOfficerPreviousInsp.DataSource = dt;
                GrdOfficerPreviousInsp.DataBind();
                lblOfficerList.Text = Convert.ToString(dt.Rows.Count);
            }
            else
            {
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
        if (e.CommandName == "Annexure_A")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];
            //Fetch value of Name.           
            string hdnVerificationType = (row.FindControl("hdnVerificationType") as HiddenField).Value;
            string hdninsp_type_id = (row.FindControl("hdnInspection_ID") as HiddenField).Value;
            string hdnorderdate = (row.FindControl("lblOrder_Date") as Label).Text;
            string lblOrder_No = (row.FindControl("lblOrder_No") as Label).Text;
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnemployeeid = (row.FindControl("hdnemployeeid") as HiddenField).Value;
            Session["hdnVerificationType"] = hdnVerificationType.ToString();
            Session["hdninsp_type_id"] = hdninsp_type_id.ToString();
            Session["hdnorderdate"] = hdnorderdate.ToString();
            Session["lblOrder_No"] = lblOrder_No.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnemployeeid"] = hdnemployeeid.ToString();
            // UpdateAnnexure_A(hdnbranchid, hdnemployeeid, hdnorderdate);
            Page.ClientScript.RegisterStartupScript(
   this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/RO/View_Annaxure_A.aspx','_newtab');", true);
        }
        if (e.CommandName == "Annexure_B")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];
            //Fetch value of Name.           
            string hdnVerificationType = (row.FindControl("hdnVerificationType") as HiddenField).Value;
            string hdninsp_type_id = (row.FindControl("hdnInspection_ID") as HiddenField).Value;
            string hdnorderdate = (row.FindControl("lblOrder_Date") as Label).Text;
            string lblOrder_No = (row.FindControl("lblOrder_No") as Label).Text;
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnemployeeid = (row.FindControl("hdnemployeeid") as HiddenField).Value;
            Session["hdnVerificationType"] = hdnVerificationType.ToString();
            Session["hdninsp_type_id"] = hdninsp_type_id.ToString();
            Session["hdnorderdate"] = hdnorderdate.ToString();
            Session["lblOrder_No"] = lblOrder_No.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnemployeeid"] = hdnemployeeid.ToString();
            // UpdateAnnexure_A(hdnbranchid, hdnemployeeid, hdnorderdate);
            Page.ClientScript.RegisterStartupScript(
   this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/RO/View_Annexure_B.aspx','_newtab');", true);
        
    }
        if (e.CommandName == "Annexure_C")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];
            //Fetch value of Name.           
            string hdnVerificationType = (row.FindControl("hdnVerificationType") as HiddenField).Value;
            string hdninsp_type_id = (row.FindControl("hdnInspection_ID") as HiddenField).Value;
            string hdnorderdate = (row.FindControl("lblOrder_Date") as Label).Text;
            string lblOrder_No = (row.FindControl("lblOrder_No") as Label).Text;
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnemployeeid = (row.FindControl("hdnemployeeid") as HiddenField).Value;
            Session["hdnVerificationType"] = hdnVerificationType.ToString();
            Session["hdninsp_type_id"] = hdninsp_type_id.ToString();
            Session["hdnorderdate"] = hdnorderdate.ToString();
            Session["lblOrder_No"] = lblOrder_No.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnemployeeid"] = hdnemployeeid.ToString();
            // UpdateAnnexure_A(hdnbranchid, hdnemployeeid, hdnorderdate);
            Page.ClientScript.RegisterStartupScript(
   this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/Inspection_Officer/View_Annaxure_C.aspx','_newtab');", true);
        }
       
    }
   protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fillInpOff_Grid();
    }

}
