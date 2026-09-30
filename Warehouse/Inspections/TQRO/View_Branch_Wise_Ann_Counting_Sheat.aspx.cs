using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using System;

public partial class Inspections_TQRO_View_Branch_Wise_Ann_Counting_Sheat : System.Web.UI.Page
{
    public SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    string PFID = "";
    SqlTransaction sqltran;
    string client_IP = "";
    int a_id = 0;
    string Region_ID = "";
    string Insp_ID = "";
    string Veri_ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        if (!IsPostBack)
        {
            lblname.Text = Session["hdnemployeename"].ToString();
            lblbranchname.Text = Session["hdnbranchname"].ToString();
            fillFinsncilYear();
           
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
    protected void ddlfinancialyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
        FillAnnaxureBgrd();
        FillAnnaxureCgrd();
        FillGandanPatrak();
    }
    protected void GrdOfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = grdannaxureB.Rows[rowIndex];

            //Fetch value of Name.
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnqauterid = (row.FindControl("hdnqauterid") as HiddenField).Value;
            string hdnverificationid = (row.FindControl("hdnverificationid") as HiddenField).Value;
            string hdnempid = (row.FindControl("hdnempid") as HiddenField).Value;
            string hdnfinancialYear = (row.FindControl("hdnfinancialYear") as HiddenField).Value;
            Session["hdnfinancialYear"] = hdnfinancialYear.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnqauterid"] = hdnqauterid.ToString();
            Session["hdnverificationid"] = hdnverificationid.ToString();
            Session["hdnempid"] = hdnempid.ToString();
            // Response.Redirect("/warehouse/Inspections/BO/Owned_Edit_Bolck_Wise_Entry.aspx");
            Page.ClientScript.RegisterStartupScript(
  this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/TQRO/View_Annaxure_A_For_RO.aspx','_newtab');", true);
        }
    }
    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Summary_Reports_Employee_Wise_Annaxure_A", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@QuarterID", Session["hdnQuarterID"].ToString());
                cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@FinacialYear", ddlfinancialyear.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                            GrdOfficerPreviousInsp.Caption = @"<b style=""font-weight: bold;"">Annaxure A: " + "</b> ";
                            GrdOfficerPreviousInsp.DataSource = dt;
                            GrdOfficerPreviousInsp.DataBind();

                        }
                        else
                        {

                            GrdOfficerPreviousInsp.DataSource = null;
                            GrdOfficerPreviousInsp.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void grdannaxureB_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = grdannaxureB.Rows[rowIndex];

            //Fetch value of Name.
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnqauterid = (row.FindControl("hdnqauterid") as HiddenField).Value;
            string hdnverificationid = (row.FindControl("hdnverificationid") as HiddenField).Value;
            string hdnempid = (row.FindControl("hdnempid") as HiddenField).Value;
            string hdnfinancialYear = (row.FindControl("hdnfinancialYear") as HiddenField).Value;
            Session["hdnfinancialYear"] = hdnfinancialYear.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnqauterid"] = hdnqauterid.ToString();
            Session["hdnverificationid"] = hdnverificationid.ToString();
            Session["hdnempid"] = hdnempid.ToString();
            // Response.Redirect("/warehouse/Inspections/BO/Owned_Edit_Bolck_Wise_Entry.aspx");
            Page.ClientScript.RegisterStartupScript(
  this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/TQRO/View_Annaxure_B_For_RO.aspx','_newtab');", true);
        }
    }
    protected void FillAnnaxureBgrd()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Summary_Reports_Employee_Wise_Annaxure_B", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@QuarterID", Session["hdnQuarterID"].ToString());
                cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@FinacialYear", ddlfinancialyear.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                            grdannaxureB.Caption = @"<b style=""font-weight: bold;"">Annaxure B: " + "</b> ";
                            grdannaxureB.DataSource = dt;
                            grdannaxureB.DataBind();

                        }
                        else
                        {

                            grdannaxureB.DataSource = null;
                            grdannaxureB.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void GrdAnnaxureC_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = grdannaxureB.Rows[rowIndex];

            //Fetch value of Name.
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnqauterid = (row.FindControl("hdnqauterid") as HiddenField).Value;
            string hdnverificationid = (row.FindControl("hdnverificationid") as HiddenField).Value;
            string hdnempid = (row.FindControl("hdnempid") as HiddenField).Value;
            string hdnfinancialYear = (row.FindControl("hdnfinancialYear") as HiddenField).Value;
            Session["hdnfinancialYear"] = hdnfinancialYear.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnqauterid"] = hdnqauterid.ToString();
            Session["hdnverificationid"] = hdnverificationid.ToString();
            Session["hdnempid"] = hdnempid.ToString();
            // Response.Redirect("/warehouse/Inspections/BO/Owned_Edit_Bolck_Wise_Entry.aspx");
            Page.ClientScript.RegisterStartupScript(
  this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/TQRO/View_Annaxure_C_For_RO.aspx','_newtab');", true);
        }
    }
    protected void FillAnnaxureCgrd()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Summary_Reports_Employee_Wise_Annaxure_C", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@QuarterID", Session["hdnQuarterID"].ToString());
                cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@FinacialYear", ddlfinancialyear.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                            GrdAnnaxureC.Caption = @"<b style=""font-weight: bold;"">Annaxure C: " + "</b> ";
                            GrdAnnaxureC.DataSource = dt;
                            GrdAnnaxureC.DataBind();

                        }
                        else
                        {

                            GrdAnnaxureC.DataSource = null;
                            GrdAnnaxureC.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void Grddagnapatrak_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = grdannaxureB.Rows[rowIndex];

            //Fetch value of Name.
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnqauterid = (row.FindControl("hdnqauterid") as HiddenField).Value;
            string hdnverificationid = (row.FindControl("hdnverificationid") as HiddenField).Value;
            string hdnempid = (row.FindControl("hdnempid") as HiddenField).Value;
            string hdnfinancialYear = (row.FindControl("hdnfinancialYear") as HiddenField).Value;
            Session["hdnfinancialYear"] = hdnfinancialYear.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnqauterid"] = hdnqauterid.ToString();
            Session["hdnverificationid"] = hdnverificationid.ToString();
            Session["hdnempid"] = hdnempid.ToString();
            // Response.Redirect("/warehouse/Inspections/BO/Owned_Edit_Bolck_Wise_Entry.aspx");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/TQRO/View_Gadna_Patrak__For_RO.aspx','_newtab');", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/Reports/Akhilesh_Bhai.aspx','_newtab');", true);
        }
    }
    protected void FillGandanPatrak()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Summary_Reports_Employee_Wise_Gadna_Patrak", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@QuarterID", Session["hdnQuarterID"].ToString());
                cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@FinacialYear", ddlfinancialyear.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                            Grddagnapatrak.Caption = @"<b style=""font-weight: bold;"">Counting Sheet: " + "</b> ";
                            Grddagnapatrak.DataSource = dt;
                            Grddagnapatrak.DataBind();

                        }
                        else
                        {

                            Grddagnapatrak.DataSource = null;
                            Grddagnapatrak.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
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

    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
        FillAnnaxureBgrd();
        FillAnnaxureCgrd();
        FillGandanPatrak();
    }
}