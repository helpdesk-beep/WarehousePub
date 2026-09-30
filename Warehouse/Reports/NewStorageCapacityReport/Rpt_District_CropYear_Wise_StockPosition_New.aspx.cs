using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SRV_Storage_Reports_Inspenctions_Rpt_District_CropYear_Wise_StockPosition_New : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    DataSet ds = null;
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    string Branch = "";
    string Distid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillRegion();
            fillDistrict();
            fillBranch();
            fillDepositor();
            fillCommodity();
            fillRegion();
            FillBillDetailsInGrid();
        }

    }

    private void fillDepositor()
    {
        string query = "select Depositor_ID, Depositor_Name FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679','181','15478')";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDepositor.Items.Clear();
            ddlDepositor.DataSource = ds.Tables[0];
            ddlDepositor.DataTextField = "Depositor_Name";
            ddlDepositor.DataValueField = "Depositor_ID";
            ddlDepositor.DataBind();
            ddlDepositor.Items.Insert(0, "--Select--");
        }
    }

    private void fillRegion()
    {
        string query = "select distinct Region_ID,Regionnm from tbl_MetaData_district";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlRegion.DataSource = ds.Tables[0];
            ddlRegion.DataTextField = "Regionnm";
            ddlRegion.DataValueField = "Region_ID";
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0, "--Select--");

        }

    }

    private void fillDistrict()
    {
        //string RegionID = Session["Region_ID"].ToString();
        string query = "select district_id,DIstrict_Name from tbl_MetaData_district where Region_ID ='" + ddlRegion.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "DIstrict_Name";
            ddldistrict.DataValueField = "district_id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "--Select--");
        }

    }

    private void fillBranch()
    {

        string query = "select BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId ='" + ddldistrict.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "BranchId";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, "--Select--");
        }

    }

    private void fillCommodity()
    {
        string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.Items.Clear();
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            //ddlcommodity.Items.Insert(0, "--Select--");
            //ddlcommodity.SelectedValue = "22";
        }
    }

    public void FillGrid()
    {
    }

    protected void FillBillDetailsInGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        string vRegion = string.Empty;
        string vDistrict = string.Empty;
        string vBranch = string.Empty;
        string vGodown = string.Empty;
        string vGodownType = string.Empty;
        string vDepositor = string.Empty;
        string vCommodity = string.Empty;
        string vCropYear = string.Empty;
        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                DataSet ds = new DataSet();


                using (SqlCommand cmd = new SqlCommand("usp_AllDetails_For_State_New", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (ddlRegion.SelectedValue == "--Select--")
                    {
                        cmd.Parameters.AddWithValue("@Region", "0");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Region", ddlRegion.SelectedValue);
                    }
                    if (ddldistrict.SelectedValue == "--Select--")
                    {
                        cmd.Parameters.AddWithValue("@DistrictID", "0"); //ddldistrict.SelectedValue.ToString());// ?? vRegion); ?? vDistrict);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@DistrictID", ddldistrict.SelectedValue); //ddldistrict.SelectedValue.ToString());// ?? vRegion); ?? vDistrict);
                    }

                    if (ddlbranch.SelectedValue == "--Select--")
                    {
                        cmd.Parameters.AddWithValue("@BranchID", "0"); //ddlbranch.SelectedValue.ToString());// ?? vRegion); ?? vBranch);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue); //ddlbranch.SelectedValue.ToString());// ?? vRegion); ?? vBranch);
                    }
                    if (ddlDepositor.SelectedValue == "--Select--")
                    {
                        cmd.Parameters.AddWithValue("@DepositorID", "0"); //ddlDepositor.SelectedValue.ToString());// ?? vRegion); ?? vDepositor);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@DepositorID", ddlDepositor.SelectedValue); //ddlDepositor.SelectedValue.ToString());// ?? vRegion); ?? vDepositor);
                    }
                    //if (ddlcommodity.SelectedValue == "--Select--")
                    //{
                    //    cmd.Parameters.AddWithValue("@CommodityID", "0"); //ddlcommodity.SelectedValue.ToString());// ?? vRegion); ?? vCommodity);
                    //}
                    //else
                    //{
                    //    cmd.Parameters.AddWithValue("@CommodityID", ddlcommodity.SelectedValue); //ddlcommodity.SelectedValue.ToString());// ?? vRegion); ?? vCommodity);
                    //}
                    string selectedIDs = string.Join(",",
                            ddlcommodity.Items.Cast<ListItem>()
                            .Where(i => i.Selected)
                            .Select(i => i.Value)
                    );

                    if (string.IsNullOrEmpty(selectedIDs))
                        selectedIDs = "0";   // All commodities
                    else
                        cmd.Parameters.Add("@CommodityIDs", SqlDbType.VarChar).Value = selectedIDs;
                    //cmd.Parameters.AddWithValue("@CommodityIDs", selectedIDs);

                    if (ddlCropYear.SelectedValue == "--Select--")
                    {
                        cmd.Parameters.AddWithValue("@CropYear", "0"); //ddlCropYear.SelectedValue.ToString());// ?? vRegion); ?? vCropYear);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedValue); //ddlCropYear.SelectedValue.ToString());// ?? vRegion); ?? vCropYear);
                    }

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(ds);
                        DataTable MainTable = ds.Tables[0];
                        if (MainTable.Rows.Count > 0)
                        {
                            GridView1.DataSource = MainTable;
                            GridView1.DataBind();

                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[6].Text = "Grand Total";
                            GridView1.FooterRow.Cells[7].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn6Months")).ToString();
                            GridView1.FooterRow.Cells[8].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn9Months")).ToString();
                            GridView1.FooterRow.Cells[9].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn12Months")).ToString();
                            GridView1.FooterRow.Cells[10].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn18Months")).ToString();

                            GridView1.FooterRow.Cells[11].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn24Months")).ToString();
                            GridView1.FooterRow.Cells[12].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn30Months")).ToString();
                            GridView1.FooterRow.Cells[13].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn36Months")).ToString();
                            GridView1.FooterRow.Cells[14].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionFiveYear")).ToString();
                            GridView1.FooterRow.Cells[15].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionGreaterthanFiveYear")).ToString();
                            GridView1.FooterRow.Cells[16].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString();
                            //GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AmountafterRMDSC")).ToString();
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {
            string script = "alert('Error: " + ex.Message.Replace("'", "\\'") + "');";
            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script, true);
        }

        
    }


    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranch();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBillDetailsInGrid();
    }

    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlgodowntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillGodown();
    }

    protected void GV_StockPositionDetails_OnRowDataBound(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillBillDetailsInGrid();
    }


    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDistrict();
    }


}