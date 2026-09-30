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

public partial class Inspections_Reports_View_Final_Submit_Depositer_Form : System.Web.UI.Page
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
    string Branch_ID = "";
    string Insp_ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        PFID = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            fillBranchDetails();
        }

    }
    public void fillBranchDetails()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Rpt_Get_Branch_Name_For_DF", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PFID_ID", PFID);
            con.Open();
            ddlbranch.DataSource = cmd.ExecuteReader();
            ddlbranch.DataTextField = "Depo_Name";
            ddlbranch.DataValueField = "Branch_ID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
            con.Close();
        }
    }
    //public void fillGodownDetails()
    //{
    //    using (SqlConnection con = new SqlConnection(constr2))
    //    {
    //        SqlCommand cmd = new SqlCommand("Rpt_Get_Godown_Name_For_DF", con);
    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
    //        con.Open();
    //        ddl_gdwn.DataSource = cmd.ExecuteReader();
    //        ddl_gdwn.DataTextField = "Godown_Name";
    //        ddl_gdwn.DataValueField = "Godown_ID";
    //        ddl_gdwn.DataBind();
    //        ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
    //        con.Close();
    //    }
    //}
    public void GetdataForGrid()
    {
        SqlCommand cmd = new SqlCommand("[dbo].[Rpt_Get_Depositer_Form_Details]", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Branch_id", ddlbranch.SelectedValue);
        cmd.Parameters.AddWithValue("@EmpID", PFID);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GD_StackBal.Caption = @"<b style=""font-weight: bold;"">Branch Name: " + "  -   " + dt.Rows[0]["DepotName"].ToString() + "</b> ";
            tr_griddata.Visible = true;
            GD_StackBal.DataSource = dt;
            GD_StackBal.DataBind();
            txt_inspdate.Text = dt.Rows[0]["Inspection_Date"].ToString();
            GD_StackBal.FooterRow.Style.Add("text-align", "center");
            GD_StackBal.FooterRow.Cells[7].Text = "Total";
            GD_StackBal.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("No_of_Bags")).ToString();
            GD_StackBal.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Quantity")).ToString();
        }
        else
        {
            tr_griddata.Visible = false;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Depositer Form Details have been Finally SUbmitted by Inspection Officer Successfully')", true);
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        GetdataForGrid();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        // fillGodownDetails();
    }
  
}