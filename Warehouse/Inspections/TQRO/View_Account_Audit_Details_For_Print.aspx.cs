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

public partial class Inspections_TQRO_View_Account_Audit_Details_For_Print : System.Web.UI.Page
{
    //public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string PFID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
       // PFID = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            GetDist();
            fillFinsncilYear();
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

        }
        else
        {
            ddl_dist.Items.Insert(0, "--Select--");
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
    protected void Button1_Click(object sender, EventArgs e)
    {

        fillScheduleInsp_Grid();

    }
    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Account_Details_For_HO_Print", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", ddl_dist.SelectedValue);
                cmd.Parameters.AddWithValue("@Inspection_Quarter", ddlquater.SelectedValue);
                cmd.Parameters.AddWithValue("@Verification_Type", ddlverification.SelectedValue);
                cmd.Parameters.AddWithValue("@Financial_Year_Insp", ddlfinancialyear.SelectedValue);
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
   
    protected void GrdOfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Overallinsp")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];
            
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnVerificationType = (row.FindControl("hdnVerificationType") as HiddenField).Value;
            string hdninsptype = (row.FindControl("hdninsptype") as HiddenField).Value;
            string hdnfinancialYear = (row.FindControl("hdnfinancialYear") as HiddenField).Value;
            string hdnauid = (row.FindControl("hdnauid") as HiddenField).Value;
            string hdnEmployeeID = (row.FindControl("hdnEmployeeID") as HiddenField).Value;
           

            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnVerificationType"] = hdnVerificationType.ToString();
            Session["hdninsptype"] = hdninsptype.ToString();
            Session["hdnfinancialYear"] = hdnfinancialYear.ToString();
            Session["hdnauid"] = hdnauid.ToString();
            Session["hdnEmployeeID"] = hdnEmployeeID.ToString();
            //Response.Redirect("/Warehouse/Inspections/TQRO/Branch_Audit_Inspection_Print.aspx");
            Page.ClientScript.RegisterStartupScript(
   this.GetType(), "OpenWindow", "window.open('/Warehouse/Inspections/TQRO/Branch_Audit_Inspection_Print.aspx','_newtab');", true);
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