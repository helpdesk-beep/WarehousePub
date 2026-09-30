using System;
using System.Collections;
using System.Configuration;
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

public partial class Inspections_Inspection_Officer_Branch_Wise_Truti_Patrak_Submit_By_BM : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string PFID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        PFID = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            fillScheduleInsp_Grid();
        }
    }

    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_TrutiPatrak_Details_Submit_By_BM_to_IO", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PF_ID", PFID);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
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
    protected void GrdOfficerPreviousInsp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnVerificationType = (HiddenField)e.Row.FindControl("hdnVerificationType");
            Button btnfilloverallinsp = (Button)e.Row.FindControl("btnfilloverallinsp");
            if (hdnVerificationType.Value == "1")
            {
                btnfilloverallinsp.Visible = true;
            }
            else if(hdnVerificationType.Value=="2")
            {
                btnfilloverallinsp.Visible = false;
            }
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
            string hdnInspection_ID = (row.FindControl("hdnInspection_ID") as HiddenField).Value;
            string hdndistrictid = (row.FindControl("hdndistrictid") as HiddenField).Value;
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnquatertype = (row.FindControl("hdnquatertype") as HiddenField).Value;
            string hdnfinancialyear = (row.FindControl("hdnfinancialyear") as HiddenField).Value;
            string hdnemployeeid = (row.FindControl("hdnemployeeid") as HiddenField).Value;
            string hdnVerificationType = (row.FindControl("hdnVerificationType") as HiddenField).Value;
            Session["hdndistrictid"] = hdndistrictid.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnInspection_ID"] = hdnInspection_ID.ToString();
            Session["hdnquatertype"] = hdnquatertype.ToString();
            Session["hdnfinancialyear"] = hdnfinancialyear.ToString();
            Session["hdnemployeeid"] = hdnemployeeid.ToString();
            Session["hdnVerificationType"] = hdnVerificationType.ToString();
            Response.Redirect("~/Inspections/Inspection_Officer/Truti_Anupalan_Prativedan_Opinion_BY_IO.aspx");

        }
    }
    
}