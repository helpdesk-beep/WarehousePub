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

public partial class Inspections_State_Rpt_All_Schedule_Inspection_BriefInfo : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd;
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.Cache.SetExpires(DateTime.Now);
        //Response.Cache.SetNoStore();
        //lbl_user.Text = Session["UserName"].ToString();
        if (!IsPostBack)
        {
            FillInspectionInfo_Grid();
            //GetDist();
        }
    }
    //protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillInpOff_Grid();
    //}
    //private void GetDist()
    //{
    //    string strDist = "";
    //    strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Regionnm";
    //    SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        //ddl_dist.DataSource = ds.Tables[0];
    //        //ddl_dist.DataTextField = "Regionnm";
    //        //ddl_dist.DataValueField = "Region_ID";
    //        //ddl_dist.DataBind();
    //        //ddl_dist.Items.Insert(0, "--Select--");
    //    }
    //    else
    //    {
    //        //ddl_dist.Items.Insert(0, "--Select--");
    //    }
    //}
    public void FillInspectionInfo_Grid()
    {
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();

            }
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("usp_InspectionDetails", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(ds);
            DataTable GVTable0 = ds.Tables[0];
            DataTable GVTable1 = ds.Tables[1];
            DataTable GVTable2 = ds.Tables[2];
            DataTable GVTable3 = ds.Tables[3];
            DataTable GVTable4 = ds.Tables[4];
            DataTable GVTable5 = ds.Tables[5];

            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { 
                conStr.Close(); 
            }

            if (dt.Rows.Count > 0)
            {
                //Status wise
                GV_InspStatusWise.DataSource = GVTable0;
                GV_InspStatusWise.DataBind();
                //Financial Year wise
                GV_FinancialYearInspCount.DataSource = GVTable1;
                GV_FinancialYearInspCount.DataBind();
                //Financial Year, Status wise
                GV_FYStatus_Info.DataSource = GVTable2;
                GV_FYStatus_Info.DataBind();
                //Financial Year, Status, Inspection and Verification Type wise
                GV_FYStatus_InspVerificationType.DataSource = GVTable3;
                GV_FYStatus_InspVerificationType.DataBind();
                //Region, Status wise Inspection Count
                GV_RegionStatusInfo.DataSource = GVTable4;
                GV_RegionStatusInfo.DataBind();
                //Region, Financial Year, Status wise Inspection Count
                GV_RegionFY_Status.DataSource = GVTable5;
                GV_RegionFY_Status.DataBind();

            }
            else
            {

                GV_InspStatusWise.DataSource = null;
                GV_InspStatusWise.DataBind();

                //Financial Year wise
                GV_FinancialYearInspCount.DataSource = null;
                GV_FinancialYearInspCount.DataBind();
                //Financial Year, Status wise
                GV_FYStatus_Info.DataSource = null;
                GV_FYStatus_Info.DataBind();
                //Financial Year, Status, Inspection and Verification Type wise
                GV_FYStatus_InspVerificationType.DataSource = null;
                GV_FYStatus_InspVerificationType.DataBind();
                //Region, Status wise Inspection Count
                GV_RegionStatusInfo.DataSource = null;
                GV_RegionStatusInfo.DataBind();
                //Region, Financial Year, Status wise Inspection Count
                GV_RegionFY_Status.DataSource = null;
                GV_RegionFY_Status.DataBind();

            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
        finally
        { if (conStr.State == ConnectionState.Open) { conStr.Close(); } }
    }
    
}