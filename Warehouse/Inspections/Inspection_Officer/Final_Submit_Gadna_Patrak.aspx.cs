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

public partial class Inspections_Inspection_Officer_Final_Submit_Gadna_Patrak : System.Web.UI.Page
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
            fillInpOff_Grid();

        }
    }

    public void fillInpOff_Grid()
    {
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();

        }
        SqlCommand cmd = new SqlCommand("Get_Alloted_Branchech_For_Verification", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@PF_ID", PFID);
        cmd.Parameters.AddWithValue("@Inspection_type_ID", ddlquater.SelectedValue);
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
            string hdninsp_type_id = (row.FindControl("hdninsp_type_id") as HiddenField).Value;
            string hdnorderdate = (row.FindControl("lblOrder_Date") as Label).Text;
            string lblOrder_No = (row.FindControl("lblOrder_No") as Label).Text;
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            Session["hdnVerificationType"] = hdnVerificationType.ToString();
            Session["hdninsp_type_id"] = hdninsp_type_id.ToString();
            Session["hdnorderdate"] = hdnorderdate.ToString();
            Session["lblOrder_No"] = lblOrder_No.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            // UpdateAnnexure_A(hdnbranchid, hdnemployeeid, hdnorderdate);
            Page.ClientScript.RegisterStartupScript(
   this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/Reports/View_Gadna_Patrak_For_Approve.aspx','_newtab');", true);
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
