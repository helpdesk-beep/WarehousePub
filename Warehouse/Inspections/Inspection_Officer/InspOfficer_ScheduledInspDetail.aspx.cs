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

public partial class Inspections_Inspection_Officer_InspOfficer_ScheduledInspDetail : System.Web.UI.Page
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
        if (Session["role"] != null)
        {
            PFID = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            fillScheduleInsp_Grid();
        }
        }
        else
        {
            Session.Abandon();
            Response.Redirect("/Warehouse/Inspections/Default.aspx");
        }
    }
   

    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Branch_name_By_Inspection_Officer", con))
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
    public void FinalSubmit(string Inspection_Id, string Branch_Id,string QuaterType,string VerificationType,string FinancialYear)
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
            cmd.Parameters.AddWithValue("@Financial_Year", FinancialYear.ToString());
            cmd.Parameters.AddWithValue("@Quater_Type", QuaterType.ToString());
            cmd.Parameters.AddWithValue("@Verification_Type", VerificationType.ToString());
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
            string hdnfinancialYear = (row.FindControl("hdnfinancialYear") as HiddenField).Value;
            string hdnVerificationType = (row.FindControl("hdnVerificationType") as HiddenField).Value;
            Session["hdndistrictid"] = hdndistrictid.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnInspection_ID"] = hdnInspection_ID.ToString();
            Session["hdninspmonth"] = hdninspmonth.ToString();
            Session["hdninsptype"] = hdninsptype.ToString();
            Session["hdnfinancialYear"] = hdnfinancialYear.ToString();
            Session["hdnVerificationType"] = hdnVerificationType.ToString();
            FinalSubmit(hdnInspection_ID, hdnbranchid, hdninsptype, hdnVerificationType, hdnfinancialYear);
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
            string hdnInspection_ID = (row.FindControl("hdnInspection_ID") as HiddenField).Value;
            string hdndistrictid = (row.FindControl("hdndistrictid") as HiddenField).Value;
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdninspmonth = (row.FindControl("hdninspmonth") as HiddenField).Value;
            string hdninsptype = (row.FindControl("hdninsptype") as HiddenField).Value;
            string hdnfinancialYear = (row.FindControl("hdnfinancialYear") as HiddenField).Value;
            string hdnVerificationType = (row.FindControl("hdnVerificationType") as HiddenField).Value;
            Session["hdndistrictid"] = hdndistrictid.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnInspection_ID"] = hdnInspection_ID.ToString();
            Session["hdninspmonth"] = hdninspmonth.ToString();
            Session["hdninsptype"] = hdninsptype.ToString();
            Session["hdnfinancialYear"] = hdnfinancialYear.ToString();
            Session["hdnVerificationType"] = hdnVerificationType.ToString();
            Response.Redirect("/Warehouse/Inspections/Inspection_Officer/InspOfficer_FillOverall_PVInsp.aspx");

        }
    }
    //public void fillScheduleInsp_Grid(string PFID)
    //{
    //    string strsql = "select Inspection_ID,PF_ID,(select Officer_Name  from tbl_metadata_Inspection_officer where PF_ID=ISD.PF_ID) as Officer_Name,(select district_name from tbl_metadata_district as MDDIS where MDDIS.District_id=ISD.District_ID) as distirct_name,(select Depotname from tbl_metadata_depot as MDD where MDD.branchID=ISD.Branch_ID) as Depotname, Inspection_Status,BranchManagerName,BranchManagerCUGNo,Order_No,Insp_Period,Insp_Type,Convert(varchar(10),Order_Date,103) as Order_Date,ISD.District_ID,ISD.Branch_ID from tbl_Inpection_Scheduled_Date as ISD where PF_ID='" + PFID + "' ";
    //    SqlCommand cmd = new SqlCommand(strsql, conStr);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        Gridview_OfficerPreviousInsp.DataSource = ds;
    //        Gridview_OfficerPreviousInsp.DataBind();
    //        lblTotalInsp.Text = Convert.ToString(ds.Tables[0].Rows.Count);
    //        this.Gridview_OfficerPreviousInsp.Columns[12].Visible = false;
    //        this.Gridview_OfficerPreviousInsp.Columns[13].Visible = false;
    //    }
    //    else
    //    {
    //        Gridview_OfficerPreviousInsp.DataSource = null;
    //        Gridview_OfficerPreviousInsp.DataBind();
    //        lblTotalInsp.Text = "0";
    //    }
    //}

    //protected void Gridview_OfficerPreviousInsp_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GridViewRow gvr = Gridview_OfficerPreviousInsp.SelectedRow;
    //    Session["S_InspType"] = gvr.Cells[10].Text;
    //    String inspty = (gvr.Cells[10].Text.Trim());
    //    if (inspty == "PV" && (gvr.Cells[5].Text.Trim() == "Pending"))
    //    {
    //        Session["SInspID"] = (gvr.Cells[0].Text.Trim());
    //        Session["SInsp_DisID"] = (gvr.Cells[12].Text.Trim());
    //        Session["SInsp_BranchID"] = (gvr.Cells[13].Text.Trim());
    //        Response.Redirect("/Inspections/Inspection_Officer/InspOfficer_PVWelcome.aspx");
    //    }
    //    else if (inspty == "HYI")
    //    {
    //        Session["SInspID"] = (gvr.Cells[0].Text.Trim());
    //        Session["SInsp_DisID"] = (gvr.Cells[12].Text.Trim());
    //        Session["SInsp_BranchID"] = (gvr.Cells[13].Text.Trim());
    //        Response.Redirect("/Inspections/Inspection_Officer/InspOfficer_NirikshanWelcome.aspx");
    //    }
    //    else if (inspty == "PV" && (gvr.Cells[5].Text.Trim() == "Submitted"))
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Already Successfully Submitted This Inspection ...')", true);
    //    }
    //    else if (inspty == "SP" && (gvr.Cells[5].Text.Trim() == "Pending"))
    //    {
    //        Session["SInspID"] = (gvr.Cells[0].Text.Trim());
    //        Session["SInsp_DisID"] = (gvr.Cells[12].Text.Trim());
    //        Session["SInsp_BranchID"] = (gvr.Cells[13].Text.Trim());
    //        Response.Redirect("/Inspections/Inspection_Officer/InspOfficer_PVWelcome.aspx");
    //    }
    //    else if (inspty == "SP" && (gvr.Cells[5].Text.Trim() == "Submitted"))
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Already Successfully Submitted This Inspection ...')", true);
    //    }
    //}
    //public void FatchScheduleInspData(string PFID)
    //{
    //    string strsql = "select Inspection_ID,PF_ID,(select Officer_Name  from tbl_metadata_Inspection_officer where PF_ID=ISD.PF_ID) as Officer_Name,(select district_name from tbl_metadata_district as MDDIS where MDDIS.District_id=ISD.District_ID) as distirct_name,(select Depotname from tbl_metadata_depot as MDD where MDD.branchID=ISD.Branch_ID) as Depotname, Inspection_Status,BranchManagerName,BranchManagerCUGNo,Order_No,Insp_Period,Insp_Type,Convert(varchar(10),Order_Date,103) as Order_Date,ISD.District_ID,ISD.Branch_ID from tbl_Inpection_Scheduled_Date as ISD where PF_ID='" + PFID + "' ";
    //    SqlCommand cmd = new SqlCommand(strsql, conStr);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        GrdOfficerPreviousInsp.DataSource = ds;
    //        GrdOfficerPreviousInsp.DataBind();
    //        lblTotalInsp.Text = Convert.ToString(ds.Tables[0].Rows.Count);
    //        this.GrdOfficerPreviousInsp.Columns[12].Visible = false;
    //        this.GrdOfficerPreviousInsp.Columns[13].Visible = false;
    //    }
    //    else
    //    {
    //        GrdOfficerPreviousInsp.DataSource = null;
    //        GrdOfficerPreviousInsp.DataBind();
    //        lblTotalInsp.Text = "0";
    //    }
    //}
}