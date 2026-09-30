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

public partial class Inspections_Ro_View_Annaxure_B_Branch_Wise : System.Web.UI.Page
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
           
        }
    }
   
    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Summary_Reports_Annaxure_A", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionID", Session["hdnRegion_ID"].ToString());
                cmd.Parameters.AddWithValue("@QuarterID", Session["hdnQuarterID"].ToString());
                cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnInspTypeID"].ToString());
                cmd.Parameters.AddWithValue("@Year", ddlyear.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            divshow.Visible = true;
                            GrdOfficerPreviousInsp.Caption = @"<b style=""font-weight: bold;"">Annaxure A: " + "</b> ";
                            GrdOfficerPreviousInsp.DataSource = dt;
                            GrdOfficerPreviousInsp.DataBind();
                           
                        }
                        else
                        {
                            divshow.Visible = false;
                            GrdOfficerPreviousInsp.DataSource = null;
                            GrdOfficerPreviousInsp.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void FillAnnaxureBgrd()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Summary_Reports_Annaxure_B", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionID", Session["hdnRegion_ID"].ToString());
                cmd.Parameters.AddWithValue("@QuarterID", Session["hdnQuarterID"].ToString());
                cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnInspTypeID"].ToString());
                cmd.Parameters.AddWithValue("@Year", ddlyear.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            divshow.Visible = true;
                            grdannaxureB.Caption = @"<b style=""font-weight: bold;"">Annaxure B: " + "</b> ";
                            grdannaxureB.DataSource = dt;
                            grdannaxureB.DataBind();

                        }
                        else
                        {
                            divshow.Visible = false;
                            grdannaxureB.DataSource = null;
                            grdannaxureB.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void FillAnnaxureCgrd()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Summary_Reports_Annaxure_C", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionID", Session["hdnRegion_ID"].ToString());
                cmd.Parameters.AddWithValue("@QuarterID", Session["hdnQuarterID"].ToString());
                cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnInspTypeID"].ToString());
                cmd.Parameters.AddWithValue("@Year", ddlyear.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            divshow.Visible = true;
                            GrdAnnaxureC.Caption = @"<b style=""font-weight: bold;"">Annaxure C: " + "</b> ";
                            GrdAnnaxureC.DataSource = dt;
                            GrdAnnaxureC.DataBind();

                        }
                        else
                        {
                            divshow.Visible = false;
                            GrdAnnaxureC.DataSource = null;
                            GrdAnnaxureC.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void FillGandanPatrak()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Summary_Reports_Gadna_Patrak", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionID", Session["hdnRegion_ID"].ToString());
                cmd.Parameters.AddWithValue("@QuarterID", Session["hdnQuarterID"].ToString());
                cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnInspTypeID"].ToString());
                cmd.Parameters.AddWithValue("@Year", ddlyear.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            divshow.Visible = true;
                            Grddagnapatrak.Caption = @"<b style=""font-weight: bold;"">Counting Sheet: " + "</b> ";
                            Grddagnapatrak.DataSource = dt;
                            Grddagnapatrak.DataBind();

                        }
                        else
                        {
                            divshow.Visible = false;
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