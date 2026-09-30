using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Globalization;

public partial class Reports_Branch_Acceptance_Wise_WHR_Details__For_All : System.Web.UI.Page
{
   

    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            labelName.Text = DateTime.Now.ToString();
            //fillgrid();
            filDivision();
        }
    }
    private void filDivision()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select Distinct Region_ID,Regionnm from tbl_MetaData_DISTRICT Order By Regionnm ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldivision.Items.Clear();
                ddldivision.DataSource = ds.Tables[0];
                ddldivision.DataTextField = "Regionnm";
                ddldivision.DataValueField = "Region_ID";
                ddldivision.DataBind();
                ddldivision.Items.Insert(0, "All");
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
    protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        filDistrict();
        //fillgrid();
    }
    private void filDistrict()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select District_Id,District_Name from tbl_MetaData_DISTRICT where Region_id='" + ddldivision.SelectedValue + "' Order By District_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldistrict.Items.Clear();
                ddldistrict.DataSource = ds.Tables[0];
                ddldistrict.DataTextField = "District_Name";
                ddldistrict.DataValueField = "District_Id";
                ddldistrict.DataBind();
                ddldistrict.Items.Insert(0, "All");
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
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        //divregion.Visible = false;
        filBranch();
       // fillgrid();
    }
    private void filBranch()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue.ToString() + "' Order By DepotName ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbranch.Items.Clear();
                ddlbranch.DataSource = ds.Tables[0];
                ddlbranch.DataTextField = "DepotName";
                ddlbranch.DataValueField = "BranchId";
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, "All");
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
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Acceptance_Wise_WHR_Details_For_All", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
                //cmd.Parameters.AddWithValue("@BranchID", "2333001");
                cmd.Parameters.AddWithValue("@Region_ID", ddldivision.SelectedValue);
                if (ddldistrict.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@District_Id", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@District_Id", ddldistrict.SelectedValue);
                }
                if (ddlbranch.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Branch_Id", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Branch_Id", ddlbranch.SelectedValue);
                }
                cmd.Parameters.AddWithValue("@SessionYear", ddlcropyear.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Commodityid", ddlcommodity.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            //GridView1.Caption = @"<b style=""font-weight: bold; text-align:center;""> M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "Acceptance Wise WHR Details" + "</br> " + "Branch Name" + "  -   " + dt.Rows[0]["DepotName"].ToString() ;
                            GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GridView1.FooterRow.Cells[6].Text = "Total";
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("No_of_Bags")).ToString();
                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalBags_Received")).ToString();
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Rec_Qty")).ToString();
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Qty_Received")).ToString();

                        }
                        else
                        {
                            // btnUpdate.Visible = false;
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       

    }
    void ShowingGroupingDataInGridView(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
    {
      
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
      
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }


   
    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillgrid();
    }
}