using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.IO;


public partial class Reports_Region_Rpt_Post_Mansoon_Fumigation_For_region : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserName"].ToString() != ""))
        {
            if (!IsPostBack)
            {
                txtRegion.Text = Session["UserName"].ToString();
                fillgridForDistrict();
                GetDistrict();
            }
        }
    }
    public void GetDistrict()
    {
        // string Dist_id = Session["Depot_DistID"].ToString();
        string qry = "";
        qry = "select distinct District_Id,District_Name from tbl_MetaData_DISTRICT Where Region_ID= '" + Session["Region_ID"].ToString() + "' Order By District_Name ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
        if (ddldistrict.SelectedItem.ToString() == "All")
        {
            divBranch.Visible = false;
            fillgridForDistrict();
        }
        else
        {
            fillgridForBranch();
        }
        //fillgrid();
    }
    public void GetBranch()
    {
        string qry = "";
        qry = "select BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "BranchId";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbranch.SelectedItem.ToString() == "All")
        {
            divStack.Visible = false;
            fillgridForBranch();
        }
        else
        {
            fillgrid();
        }
    }
    public void fillgridForDistrict()
    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Rpt_Region_Wise_Post_Mansoon_Fumigation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Region_ID", Session["Region_ID"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                grddistrict.DataSource = dt;
                grddistrict.DataBind();
                //divStack.Visible = false;
                divdistrict.Visible = true;
                //DivRegion.Visible = true;
                grddistrict.FooterRow.Style.Add("text-align", "right");
                grddistrict.FooterRow.Cells[1].Text = "Total";
                grddistrict.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Stack")).ToString();
                grddistrict.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Fumigated")).ToString();
                grddistrict.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Pending_Stack_For_Fumigation")).ToString();
            }
            else
            {
                grddistrict.DataSource = null;
                grddistrict.DataBind();
                grddistrict.Visible = true;
            }
        }
    }
    public void fillgridForBranch()
    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Rpt_District_Wise_Post_Mansoon_Fumigation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District_Id", ddldistrict.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                grdbranch.DataSource = dt;
                grdbranch.DataBind();
                //divStack.Visible = false;
                //DivRegion.Visible = false;
                divdistrict.Visible = false;
                divBranch.Visible = true;
                grdbranch.FooterRow.Style.Add("text-align", "right");
                grdbranch.FooterRow.Cells[1].Text = "Total";
                grdbranch.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Stack")).ToString();
                grdbranch.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Fumigated")).ToString();
                grdbranch.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Pending_Stack_For_Fumigation")).ToString();
            }
            else
            {
                grdbranch.DataSource = null;
                grdbranch.DataBind();
                grdbranch.Visible = true;
            }
        }
    }
    public void fillgrid()
    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Rpt_Godown_Wise_Post_Mansoon_Fumigation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_Id", ddlbranch.SelectedValue);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                GrdStack.DataSource = dt;
                GrdStack.DataBind();
                divStack.Visible = true;
                divdistrict.Visible = false;
                divBranch.Visible = false;
                GrdStack.FooterRow.Style.Add("text-align", "right");
                GrdStack.FooterRow.Cells[4].Text = "Total";
                GrdStack.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Stack")).ToString();
                GrdStack.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Fumigated")).ToString();
                GrdStack.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Pending_Stack_For_Fumigation")).ToString();
            }
            else
            {
                GrdStack.DataSource = null;
                GrdStack.DataBind();
                divStack.Visible = true;
            }
        }
    }
}