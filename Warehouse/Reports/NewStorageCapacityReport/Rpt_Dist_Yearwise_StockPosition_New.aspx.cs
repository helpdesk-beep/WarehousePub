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
using System.Linq;

public partial class SRV_Storage_Reports_Inspenctions_Rpt_Dist_Yearwise_StockPosition_New : System.Web.UI.Page
{
    //string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd = null;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            {
                if (!IsPostBack)
                {
                    fillRegion();
                    //fillComodity();
                }
            }
        }
    }

    private void fillRegion()
    {
        try
        {
            string query1 = "select Region_Id,region from tbl_MetaData_Region";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {

                ddlRegion.DataSource = ds1.Tables[0];
                ddlRegion.DataTextField = "region";
                ddlRegion.DataValueField = "Region_Id";
                ddlRegion.DataBind();
                ddlRegion.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    private void fillDistrict(string RegionID)
    {
        string strDist = "";
        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT where RegionID='" + RegionID + "' order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDistrict.DataSource = ds.Tables[0];
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_Id";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlDistrict.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        string query1 = "select District_Id, District_Name from tbl_metadata_district MD left  join tbl_MetaData_Region MR on MR.Region_Id = MD.Region_ID where MD.Region_ID ='" + ddlRegion.SelectedValue + "'";
        SqlCommand cmd1 = new SqlCommand(query1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        if (ds1.Tables[0].Rows.Count > 0)
        {

            ddlDistrict.DataSource = ds1.Tables[0];
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_Id";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("जिला चुने", "0"));
            //ddlBranch.Items.Clear();

        }
    }

    protected void GV_StockReport_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetBranch(ddlDistrict.SelectedValue.ToString());
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            string CropYear = ddlCropYear.SelectedValue.ToString();
            string DistrictID = ddlDistrict.SelectedValue.ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_DistrictYearStockPosition_Test1_New", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            // ******** MULTIPLE COMMODITY IDS ********
            string selectedIDs = string.Join(",",
                    ddlCommodity.Items.Cast<ListItem>()
                    .Where(i => i.Selected)
                    .Select(i => i.Value)
            );

            if (string.IsNullOrEmpty(selectedIDs))
                selectedIDs = "0";   // All commodities
            else
                cmd.Parameters.Add("@CommodityIDs", SqlDbType.VarChar).Value = selectedIDs;
            //cmd.Parameters.AddWithValue("@CommodityIDs", selectedIDs);

            cmd.Parameters.AddWithValue("@CropYear", CropYear);
            cmd.Parameters.AddWithValue("@DistrictID", DistrictID);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();
            //cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            string script = "alert('Error: " + ex.Message.Replace("'", "\\'") + "');";
            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script, true);
        }
        
    }

    private void fillComodity()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY WHERE CommodityID IN ('3','8','11','12','22') order by Commodity_Name";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCommodity.Items.Clear();
                ddlCommodity.DataSource = ds.Tables[0];
                ddlCommodity.DataTextField = "Commodity_Name";
                ddlCommodity.DataValueField = "Commodity_Id";
                ddlCommodity.DataBind();
                //ddlCommodity.Items.Insert(0, "--Select--");
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


    protected void btnExportData_Click(object sender, EventArgs e)
    {
        string FileDateTime = DateTime.Now.ToString("ddMMyyyyhhmmss");
        string CropYear = ddlCropYear.SelectedValue.ToString();
        string DistrictID = ddlDistrict.SelectedValue.ToString();
        string CommodityID = ddlCommodity.SelectedValue.ToString();
        con.Open();
        //SqlCommand cmd = new SqlCommand("usp_DistrictYearStockPosition", con);
        SqlCommand cmd = new SqlCommand("usp_DistrictYearStockPosition_Test1", con);
        cmd.Parameters.AddWithValue("@CropYear", CropYear);
        cmd.Parameters.AddWithValue("@DistrictID", DistrictID);
        cmd.Parameters.AddWithValue("@CommodityID", CommodityID);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 100;
        cmd.ExecuteNonQuery();
        //SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dt.TableName = "Records";
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            string attachment = "attachment; filename=DistrictWise_Godown_StockPosition_OnBefore2024_25_" + FileDateTime + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        con.Close();
    }

}
