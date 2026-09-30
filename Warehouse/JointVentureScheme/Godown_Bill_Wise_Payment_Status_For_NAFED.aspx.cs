using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Resources;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;

public partial class Reports_Region_Godown_Bill_Wise_Payment_Status_For_NAFED : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Region_ID"] != null))
        {
            if (!IsPostBack)
            {
                fillgrid();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void fillDistrict()
    {
        try
        {
            string region = "";
            //if (Session["UserName"].ToString() != "MPSWLC")
            //{

            //    if (Session["Region_ID"].ToString() != null)
            //    {
            //        region = Session["Region_ID"].ToString();

            //    }
            //}
            string query = "";
            //if (Session["UserName"].ToString() == "MPSWLC")
            //{
            //    query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            //}
            //else
            //{
            //    query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
            //}
            query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + Session["Region_Logid"].ToString() + "' order by District_Name asc";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "---Select---");
                //gv.DataSource = null;
                //gv.DataBind();
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    public void GetBranch()
    {
        string qry = "";
        qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDepotList.DataSource = ds.Tables[0];
            ddlDepotList.DataTextField = "DepotName";
            ddlDepotList.DataValueField = "BranchId";
            ddlDepotList.DataBind();
            ddlDepotList.Items.Insert(0, "--Select--");
        }
    }
    public void fillgrid()
    {
        String Region = Session["Region_ID"].ToString();
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_Wise_Payment_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", ddlDepotList.SelectedValue.ToString());
            //cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                grpendding.DataSource = dt;
                grpendding.DataBind();
                grdbill.Visible = true;
                grpendding.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                grpendding.FooterRow.Cells[1].Text = "Total";
                //grpendding.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("Total_rent_Bill_Generated")).ToString();
                //grpendding.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Total_rent_Bill_Amount")).ToString();
               
            }
            else
            {
                grpendding.DataSource = null;
                grpendding.DataBind();
                grdbill.Visible = true;
            }
        }
    }
}