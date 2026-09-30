using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Diagnostics;
using System.Resources;
using AjaxControlToolkit;



public partial class IssueCenterLevel_Storage_Rpt_CMR_ACNote : System.Web.UI.Page
{
    //public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //string ConnectionStrings = "Data Source=10.115.145.43;InitialCatalog=CSMS;UID=Csms_Usr;Password=Usr#@15824*csms";

    //public SqlConnection con = new SqlConnection("Data Source=172.16.11.16;User ID=sa;Password=abc*123;Initial Catalog=CSMS");
    public SqlConnection con = new SqlConnection("Data Source=10.115.145.43;User ID=Csms_Usr;Password=Usr#@15824*csms;Initial Catalog=CSMS");
    //public SqlConnection con = new SqlConnection("Data Source=10.115.145.43;User ID=Integrated_Usr;Password=Mp@*nt*15824#;Initial Catalog=CSMS");
    //public SqlConnection con = new SqlConnection("Data Source=10.115.145.39;User ID=Integrated_Csms;Password=Mp@csms*nt*15824*;Initial Catalog=CSMS");

    //string ConnectionStrings = "Data Source=172.16.11.16;User ID=sa;Password=abc*123;Initial Catalog=CSMS;";
    //SqlConnection con = new SqlConnection(ConnectionStrings);
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings.ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd;
    DataTable dt = new DataTable();
    private object localIP;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetWHRwiseACNoteBranchData();
    }

    public void GetWHRwiseACNoteBranchData()
    {
        try
        {
            //string BranchID = Session["BranchId"].ToString();
            //string CommodityID = ddlProcCmd.SelectedValue.ToString();
            con.Open();
            //cmd = new SqlCommand("usp_GetWHRwiseACNote_2024_25", con);
            //cmd.Parameters.AddWithValue("@Branch", BranchID);
            //cmd.Parameters.AddWithValue("@CommodityID", CommodityID);
            //cmd.CommandType = CommandType.StoredProcedure;
            string query = "SELECT TOP 10 [District],[issueCentre_code],[CropYear],[Book_Number],[Acceptance_No] FROM [CSMS].[dbo].[CMR_QualityInspection_2019]";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GV_ACNote.DataSource = ds;
                GV_ACNote.DataBind();
                showgrid.Visible = true;

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
                showgrid.Visible = false;
                GV_ACNote.DataSource = null;
                GV_ACNote.DataBind();

            }


        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
            
        }
    }

    protected void GV_ACNote_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_ACNote.PageIndex = e.NewPageIndex;
        GetWHRwiseACNoteBranchData();
    }

    
}