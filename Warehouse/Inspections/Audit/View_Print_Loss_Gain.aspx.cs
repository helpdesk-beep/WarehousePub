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

public partial class Inspections_Audit_View_Print_Loss_Gain : System.Web.UI.Page
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
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_View_Loss_Gain_Statment", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", PFID);
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
                            // lblTotalInsp.Text = Convert.ToString(dt.Rows[0].Count);
                            //this.GrdOfficerPreviousInsp.Columns[12].Visible = false;
                            //this.GrdOfficerPreviousInsp.Columns[13].Visible = false;
                        }
                        else
                        {

                            GrdOfficerPreviousInsp.DataSource = null;
                            GrdOfficerPreviousInsp.DataBind();
                            lblTotalInsp.Text = "0";
                        }
                    }
                }
            }
        }
    }
    protected void GrdOfficerPreviousInsp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    HiddenField hdnVerificationType = (HiddenField)e.Row.FindControl("hdnVerificationType");
        //    Button btnfilloverallinsp = (Button)e.Row.FindControl("btnfilloverallinsp");
        //    if (hdnVerificationType.Value == "1")
        //    {
        //        btnfilloverallinsp.Visible = true;
        //    }
        //    else if(hdnVerificationType.Value=="2")
        //    {
        //        btnfilloverallinsp.Visible = false;
        //    }
        //}

    }
    public void FinalSubmit(string Inspection_Id, string Branch_Id)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();
            }

            SqlCommand cmd = new SqlCommand("Inspection_Final_Submit_by_Officer_Insert", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Insp_Officer_ID", Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@Inspection_Id", Inspection_Id.ToString());
            cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id.ToString());
            cmd.Parameters.AddWithValue("@IP_Adress", localIP.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Your Inspection Final Submited Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                fillScheduleInsp_Grid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
    protected void GrdOfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];
            //Fetch value of Name.           
            string hdnInspection_ID = (row.FindControl("hdnInspection_ID") as HiddenField).Value;
            string hdndistrictid = (row.FindControl("hdndistrictid") as HiddenField).Value;
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdninspmonth = (row.FindControl("hdninspmonth") as HiddenField).Value;
            string hdninsptype = (row.FindControl("hdninsptype") as HiddenField).Value;
            Session["hdndistrictid"] = hdndistrictid.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnInspection_ID"] = hdnInspection_ID.ToString();
            Session["hdninspmonth"] = hdninspmonth.ToString();
            Session["hdninsptype"] = hdninsptype.ToString();
            FinalSubmit(hdnInspection_ID, hdnbranchid);
        }
        if (e.CommandName == "Overallinsp")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];
            //Fetch value of Name.           
            //string hdnInspection_ID = (row.FindControl("hdnInspection_ID") as HiddenField).Value;
            //string hdndistrictid = (row.FindControl("hdndistrictid") as HiddenField).Value;
            //string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            //Session["hdndistrictid"] = hdndistrictid.ToString();
            //Session["hdnbranchid"] = hdnbranchid.ToString();
            //Session["hdnInspection_ID"] = hdnInspection_ID.ToString();
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdninsptype = (row.FindControl("hdninsptype") as HiddenField).Value;
            string hdnfinancialYear = (row.FindControl("hdnfinancialYear") as HiddenField).Value;
            string hdnVerificationType = (row.FindControl("hdnVerificationType") as HiddenField).Value;
            string hdnEmployeeID = (row.FindControl("hdnEmployeeID") as HiddenField).Value;

            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdninsptype"] = hdninsptype.ToString();
            Session["hdnfinancialYear"] = hdnfinancialYear.ToString();
            Session["hdnVerificationType"] = hdnVerificationType.ToString();
            Session["hdnEmployeeID"] = hdnEmployeeID.ToString();
            //  Response.Redirect("/Warehouse/Inspections/Audit/Loss_Gain_Statement_for_Inspection.aspx");
            Page.ClientScript.RegisterStartupScript(
   this.GetType(), "OpenWindow", "window.open('/Warehouse/Inspections/Audit/View_Print_Loss_Gain_Statement_for_Inspection.aspx','_newtab');", true);


        }
    }
  
}