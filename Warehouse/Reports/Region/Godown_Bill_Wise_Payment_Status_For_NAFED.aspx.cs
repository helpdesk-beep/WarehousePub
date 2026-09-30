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
                fillDistrict();
                GetCommodity();
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
    private void fillHired_Type()
    {
        try
        {
            string region = "";
            string query = "";
            query = "Select Distinct Hired_Type From tbl_MetaData_GODOWN_2018";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlHired_Type.Items.Clear();
                ddlHired_Type.DataSource = ds.Tables[0];
                ddlHired_Type.DataTextField = "Hired_Type";
                ddlHired_Type.DataValueField = "Hired_Type";
                ddlHired_Type.DataBind();
                ddlHired_Type.Items.Insert(0, "---Select---");
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

    public void GetCommodity()
    {
        string qry = "";
        qry = "select distinct cmd.Commodity_Id,cmd.Commodity_Name from tbl_Institution_Storage_Bill_Details_For_NAFED A INNER JOIN tbl_MetaData_STORAGE_COMMODITY cmd ON A.Commodity_Id=cmd.Commodity_Id";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlCommodity.DataSource = ds.Tables[0];
            ddlCommodity.DataTextField = "Commodity_Name";
            ddlCommodity.DataValueField = "Commodity_Id"; 
            ddlCommodity.DataBind();
            ddlCommodity.Items.Insert(0, "--Select--");
        }
    }
    public void fillgrid()
    {
        String Region = Session["Region_ID"].ToString();
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_Wise_Payment_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DistrictID", ddlDistrict.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Hired_Type", ddlHired_Type.SelectedValue.ToString());
            if (ddlDepotList.SelectedValue == "--Select--")
            {
                cmd.Parameters.AddWithValue("@BranchID", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BranchID", ddlDepotList.SelectedValue.ToString());
            }
            if (ddlCommodity.SelectedValue == "--Select--")
            {
                cmd.Parameters.AddWithValue("@CommodityID", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CommodityID", ddlCommodity.SelectedValue);
            }
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
                grpendding.FooterRow.Cells[4].Text = "Total SC Bill Amount";
                grpendding.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("AmountRecivedFromNAfed")).ToString();
                grpendding.FooterRow.Cells[10].Text = "Total Rent Bill Amount";
                grpendding.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("TotalRentBillAmount")).ToString();
                grpendding.FooterRow.Cells[18].Text = "Total Rent Bill Amount Pay to Godown Owner";
                grpendding.FooterRow.Cells[19].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("PaytogodownOwner")).ToString();
            }
            else
            {
                grpendding.DataSource = null;
                grpendding.DataBind();
                grdbill.Visible = true;
            }
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetBranch();
        //fillgrid();
        fillHired_Type();
    }
    protected void ddlHired_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
        fillgrid();
    }
    

    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void ddlCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}