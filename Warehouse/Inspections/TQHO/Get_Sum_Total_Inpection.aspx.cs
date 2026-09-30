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
using System.Drawing;

public partial class Inspections_State_Inspection_Status_Third_Party_Month_Wise : System.Web.UI.Page
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
            GetDist();
        }
    }
    private void GetDist()
    {
        string strDist = "";
        strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Regionnm";
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
    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Sum_Total_Inpection", con))
            //using (SqlCommand cmd = new SqlCommand("Get_Alloted_and_Status_of_Inspection_Months", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                    cmd.Parameters.AddWithValue("@Inspection_type_ID", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Inspection_type_ID", ddlquater.SelectedValue);
                }
                //if (ddlmonth.SelectedValue == "--Select--")
                //{
                //    cmd.Parameters.AddWithValue("@MonthID", 0);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@MonthID", ddlmonth.SelectedValue);
                //}
                //if (ddlstatus.SelectedValue == "--Select--")
                //{
                //    cmd.Parameters.AddWithValue("@Status", 0);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@Status", ddlstatus.SelectedValue);
                //}

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
                            GrdOfficerPreviousInsp.FooterRow.Style.Add("text-align", "Right");
                            GrdOfficerPreviousInsp.FooterRow.Cells[3].Text = "Total";
                            GrdOfficerPreviousInsp.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoofallottedInspection")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoofCompleteInspection")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoofpendingInspection")).ToString();
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
          //  HiddenField hdnVerificationType = (HiddenField)e.Row.FindControl("hdnVerificationType");
            Label lblBranchClosingBalance = (Label)e.Row.FindControl("lblBranchClosingBalance");
            //if (lblBranchClosingBalance.Text== "Pending")
            //{
            //    e.Row.BackColor = ColorTranslator.FromHtml("#e9716b");//e9716b
            //}
            //else 
            //{
            //    e.Row.BackColor = ColorTranslator.FromHtml("#28b779");//28b779
            //}
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
            string hdnTAPID = (row.FindControl("hdnTAPID") as HiddenField).Value;
            string hdnquatertype = (row.FindControl("hdnquatertype") as HiddenField).Value;
            string hdnfinancialyear = (row.FindControl("hdnfinancialyear") as HiddenField).Value;
            string hdnemployeeid = (row.FindControl("hdnemployeeid") as HiddenField).Value;
            Session["hdndistrictid"] = hdndistrictid.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnInspection_ID"] = hdnInspection_ID.ToString();
            Session["hdnTAPID"] = hdnTAPID.ToString();
            Session["hdnquatertype"] = hdnquatertype.ToString();
            Session["hdnfinancialyear"] = hdnfinancialyear.ToString();
            Session["hdnemployeeid"] = hdnemployeeid.ToString();
            //  Response.Redirect("/Warehouse/Inspections/State/Truti_Anupalan_Prativedan.aspx");
            Page.ClientScript.RegisterStartupScript(
  this.GetType(), "OpenWindow", "window.open('/Warehouse/Inspections/State/Truti_Anupalan_Prativedan.aspx','_newtab');", true);

        }
    }
    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }

    public void fillMonthQuarterWise()
    {
        string conStr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("Get_Month_Quater_Wise", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Quarter_ID", ddlquater.SelectedValue);
            con.Open();
            ddlmonth.DataSource = cmd.ExecuteReader();
            ddlmonth.DataTextField = "Month_Name";
            ddlmonth.DataValueField = "ID";
            ddlmonth.DataBind();
            ddlmonth.Items.Insert(0, new ListItem("-- Select Month --", "0"));
            con.Close();
        }
    }




    protected void ddlquater_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillMonthQuarterWise();
    }

    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillMonthQuarterWise();
        fillScheduleInsp_Grid();
    }
}