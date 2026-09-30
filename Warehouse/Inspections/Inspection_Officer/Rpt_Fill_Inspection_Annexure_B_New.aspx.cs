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

public partial class Inspections_Rpt_Fill_Inspection_Annexure_B_New : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    string PFID = "";
    SqlTransaction sqltran;
    string client_IP = "";
    int a_id = 0;
    string Branch_ID = "";
    string Insp_ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        PFID = Session["UserId"].ToString();
        Insp_ID = Session["hdnInspection_ID"].ToString();
        Branch_ID = Session["hdnbranchid"].ToString();
        if (!IsPostBack)
        {
            lblinspid.Text = Insp_ID.ToString();
            FatchScheduleInspData();
            fillGodownDetails();
            fillGodownType();
            txtmaxcpt.Attributes.Add("readonly", "readonly");
            txtsci_CPT.Attributes.Add("readonly", "readonly");
            ddlhiredtype.Enabled = false;
            ddlStorageType.Enabled = false;
        }

    }

    public void FatchScheduleInspData()
    {
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();

            }
            SqlCommand cmd = new SqlCommand("Insp_GetInspection_Schedule_For_Inspection_Officer", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
            //cmd.Parameters.AddWithValue("@godownID", Session["Godown_ID"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { conStr.Close(); }

            if (dt.Rows.Count > 0)
            {

                lblbranch.Text = dt.Rows[0]["Depotname"].ToString().Trim();
                lblinsptype.Text = dt.Rows[0]["Insp_Type"].ToString().Trim();
                lblInspPeriod.Text = dt.Rows[0]["Insp_Period"].ToString().Trim();
                //Label86.Visible = true;
                //Label79.Text = "यह जानकारी भरी जा चुकी है ।";
                //btnsaveprofile.Visible = false;
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Not Available!')", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('............!')", true);
        }
        finally
        { if (con.State == ConnectionState.Open) { con.Close(); } }
    }

    public void fillGodownDetails()
    {
        using (SqlConnection con = new SqlConnection(constr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Name_For_Inspection", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
            con.Open();
            ddl_gdwn.DataSource = cmd.ExecuteReader();
            ddl_gdwn.DataTextField = "Godown_Name";
            ddl_gdwn.DataValueField = "Godown_ID";
            ddl_gdwn.DataBind();
            ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con.Close();
        }
    }
    protected void ddl_gdwn_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SqlCommand cmd = new SqlCommand("Get_Inspection_stackwiseBal_Annex_B", conStr);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
        //cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    GD_StackBal.DataSource = ds;
        //    GD_StackBal.DataBind();
        //    this.GD_StackBal.Columns[0].Visible = false;
        //    tr_griddata.Visible = true;
        //}
        //else
        //{
        //    GetdataForGrid();
        //}
        GetdataForGrid();
    }
    public void GetdataForGrid()
    {
        SqlCommand cmd = new SqlCommand("Get_Inspection_stackwiseBal_Annex_B_Rpt_Details", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            tr_griddata.Visible = true;
            GD_StackBal.DataSource = dt;
            GD_StackBal.DataBind();
            txt_inspdate.Text = dt.Rows[0]["Inspection_Date"].ToString().Trim();
            GetGdwnData();
        }
        else
        {
            tr_griddata.Visible = false;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
            // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found...')", true);
        }
    }
    
    private void fillGodownType()
    {
        try
        {
            string query = "";
            query = "SELECT  [Gid],[GodownType],[TypeValue] FROM [dbo].[GodownTypeMaster]";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlhiredtype.Items.Clear();
                ddlhiredtype.DataSource = ds.Tables[0];
                ddlhiredtype.DataTextField = "GodownType";
                ddlhiredtype.DataValueField = "GodownType";
                ddlhiredtype.DataBind();
                ddlhiredtype.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlhiredtype.Items.Clear();
            }
        }
        catch (Exception)
        {
        }
    }
    public void GetGdwnData()
    {
        SqlCommand cmd = new SqlCommand("Get_Godown_Capacity_Details", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
        //string strsql = "select Godown_ID,Convert(Decimal(18,2),Godown_Capacity) as Godown_Capacity,Convert(Decimal(18,2),Godown_Scientific_capacity) as Godown_Scientific_capacity,Hired_Type,Storage_Type from tbl_MetaData_GODOWN_2018 where Godown_ID='" + ddl_gdwn.SelectedValue.ToString() + "'";
        //SqlCommand cmd = new SqlCommand(strsql, conStr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtmaxcpt.Text = dt.Rows[0]["Godown_Capacity"].ToString().Trim();
            txtsci_CPT.Text = dt.Rows[0]["Godown_Scientific_capacity"].ToString().Trim();
            ddlhiredtype.SelectedValue = dt.Rows[0]["Hired_Type"].ToString().Trim();
            ddlStorageType.SelectedValue = dt.Rows[0]["Storage_Type"].ToString().Trim();
           
        }
        else
        {

        }
    }
  
}