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

public partial class Region_Rpt_District_CropYear_Wise_StockPosition : System.Web.UI.Page
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
            FillGodownType();
            fillGodown();
            fillDepositor();
            fillCommodity();
            if (Request.QueryString["ID"] != null)
            {
                fillRegion();
                // filldetails();
            }
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
        string RegionID = Session["Region_ID"].ToString();
        string query = "select distinct Region_ID,Regionnm from tbl_MetaData_district WHERE Region_ID = '" + RegionID + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //ddlregion.DataSource = ds.Tables[0];
            //ddlregion.DataTextField = "Regionnm";
            //ddlregion.DataValueField = "Region_ID";
            //ddlregion.Text = ds.Tables[0].Rows[0].ToString();

            ddlregion.Text = ds.Tables[0].Rows[0]["Regionnm"].ToString();
            //ddlregion.DataBind();
            //ddlregion.Items.Insert(0, "--Select--");

        }

    }

    private void fillDistrict()
    {
        string RegionID = Session["Region_ID"].ToString();
        string query = "select district_id,DIstrict_Name from tbl_MetaData_district where Region_ID ='" + RegionID + "'";
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
            ddlcommodity.Items.Insert(0, "--Select--");
            //ddlcommodity.SelectedValue = "22";
        }
    }

    private void fillGodown()
    {

        string query = " SELECT Godown_ID,Godown_Name FROM tbl_MetaData_GODOWN_2018 where BranchID  ='" + ddlbranch.SelectedValue + "' and Hired_Type  ='" + ddlgodowntype.SelectedItem.ToString() + "' and IsActive='Y' order by Godown_Name Asc";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");
        }
        else
        {
            // lblmsg.Text = "User Details Update Sucessfully";
        }

    }

    private void FillGodownType()
    {
        string query = "select distinct Hired_Type from tbl_MetaData_GODOWN_2018 order by Hired_Type";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlgodowntype.Items.Clear();
            ddlgodowntype.DataSource = ds.Tables[0];
            ddlgodowntype.DataTextField = "Hired_Type";
            ddlgodowntype.DataValueField = "Hired_Type";
            ddlgodowntype.DataBind();
            ddlgodowntype.Items.Insert(0, "--Select--");
        }
    }

    public void FillGrid()
    {
        //DataTable dtdetails = new DataTable();
        //dtdetails = clsAdmin.GetCreteUsers();
        //if (dtdetails.Rows.Count > 0)
        //{
        //    grdalreadyattended.DataSource = dtdetails;
        //    grdalreadyattended.DataBind();
        //}
        //else
        //{
        //    grdalreadyattended.DataSource = null;
        //    grdalreadyattended.DataBind();
        //}
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

        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            ////////////////////////Region/////////////////////////
            //if (ddlregion.SelectedItem.ToString() == "--Select--")
            //{
            //    //ddlregion.SelectedValue = DBNull.Value.ToString();
            //    vRegion = DBNull.Value.ToString();

            //}
            //else
            //{
            //    vRegion = ddlregion.SelectedValue.ToString();
            //}
            ///////////////////////////District/////////////////////////
            //if (ddldistrict.SelectedItem.ToString() == "--Select--")
            //{
            //    //ddldistrict.SelectedValue = DBNull.Value.ToString();
            //    vDistrict = DBNull.Value.ToString();
            //}
            //else
            //{
            //    vDistrict = ddldistrict.SelectedValue.ToString();
            //}
            ///////////////////////////Branch/////////////////////////
            //if (ddlbranch.SelectedItem.ToString() == "--Select--")
            //{
            //    //ddlbranch.SelectedValue= DBNull.Value.ToString();
            //    vBranch = DBNull.Value.ToString();
            //}
            //else
            //{
            //    vBranch = ddlbranch.SelectedValue.ToString();
            //}
            ///////////////////////////Godown/////////////////////////
            //if (ddlgodown.SelectedItem.ToString() == "--Select--")
            //{
            //    //ddlgodown.SelectedValue = DBNull.Value.ToString();
            //    vGodown = DBNull.Value.ToString();
            //}
            //else
            //{
            //    vGodown = ddlgodown.SelectedValue.ToString();
            //}
            ///////////////////////////Depositor/////////////////////////
            //if (ddlDepositor.SelectedItem.ToString() == "--Select--")
            //{
            //    //ddlDepositor.SelectedValue= DBNull.Value.ToString();
            //    vDepositor = DBNull.Value.ToString();
            //}
            //else
            //{
            //    vDepositor = ddlDepositor.SelectedValue.ToString();
            //}
            ///////////////////////////Commodity/////////////////////////
            //if (ddlcommodity.SelectedItem.ToString() == "--Select--")
            //{
            //    //ddlcommodity.SelectedValue= DBNull.Value.ToString();
            //    vCommodity = DBNull.Value.ToString();
            //}
            //else
            //{
            //    vCommodity = ddlcommodity.SelectedValue.ToString();
            //}
            ///////////////////////////Crop Year/////////////////////////
            //if (ddlCropYear.SelectedItem.ToString() == "--Select--")
            //{
            //    //ddlCropYear.SelectedValue= DBNull.Value.ToString();
            //    vCropYear = DBNull.Value.ToString();
            //}
            //else
            //{
            //    vCropYear = ddlCropYear.SelectedValue.ToString();
            //}
            ///////////////////////////Godown Type/////////////////////////
            //if (ddlgodowntype.SelectedItem.ToString() == "--Select--")
            //{
            //    //ddlgodowntype.SelectedValue= DBNull.Value.ToString();
            //    vGodownType = DBNull.Value.ToString();
            //}
            //else
            //{
            //    vGodownType = ddlgodowntype.SelectedValue.ToString();
            //}
            //////////////////////////////////////////////////


            using (SqlCommand cmd = new SqlCommand("usp_AllDetails", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Region", ddlregion.SelectedValue.ToString() ?? DBNull.Value.ToString());
                //cmd.Parameters.AddWithValue("@DistrictID", ddldistrict.SelectedValue.ToString() ?? DBNull.Value.ToString());
                //cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue.ToString() ?? DBNull.Value.ToString());
                //cmd.Parameters.AddWithValue("@GodownID", ddlgodown.SelectedValue.ToString() ?? DBNull.Value.ToString());
                //cmd.Parameters.AddWithValue("@DepositorID", ddlDepositor.SelectedValue.ToString() ?? DBNull.Value.ToString());
                //cmd.Parameters.AddWithValue("@CommodityID", ddlcommodity.SelectedValue.ToString() ?? DBNull.Value.ToString());
                //cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedValue.ToString() ?? DBNull.Value.ToString());
                //cmd.Parameters.AddWithValue("@GodownType", ddlgodowntype.SelectedValue.ToString() ?? DBNull.Value.ToString());
                //if (ddlregion.SelectedValue == "--Select--")
                //{
                //    cmd.Parameters.AddWithValue("@Region", "0"); //ddlregion.SelectedValue.ToString());// ?? vRegion);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@Region", ddlregion.SelectedValue); //ddlregion.SelectedValue.ToString());// ?? vRegion);
                //}
                string RegionID = Session["Region_ID"].ToString();
                cmd.Parameters.AddWithValue("@Region", RegionID);
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
                if (ddlgodown.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@GodownID", "0"); //ddlgodown.SelectedValue.ToString());// ?? vRegion); ?? vGodown);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@GodownID", ddlgodown.SelectedValue); //ddlgodown.SelectedValue.ToString());// ?? vRegion); ?? vGodown);
                }
                if (ddlDepositor.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@DepositorID", "0"); //ddlDepositor.SelectedValue.ToString());// ?? vRegion); ?? vDepositor);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DepositorID", ddlDepositor.SelectedValue); //ddlDepositor.SelectedValue.ToString());// ?? vRegion); ?? vDepositor);
                }
                if (ddlcommodity.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@CommodityID", "0"); //ddlcommodity.SelectedValue.ToString());// ?? vRegion); ?? vCommodity);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CommodityID", ddlcommodity.SelectedValue); //ddlcommodity.SelectedValue.ToString());// ?? vRegion); ?? vCommodity);
                }
                if (ddlCropYear.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@CropYear", "0"); //ddlCropYear.SelectedValue.ToString());// ?? vRegion); ?? vCropYear);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedValue); //ddlCropYear.SelectedValue.ToString());// ?? vRegion); ?? vCropYear);
                }
                if (ddlgodowntype.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@GodownType", "0"); //ddlgodowntype.SelectedValue.ToString());// ?? vRegion); ?? vGodownType);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@GodownType", ddlgodowntype.SelectedValue); //ddlgodowntype.SelectedValue.ToString());// ?? vRegion); ?? vGodownType);
                }

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    DataTable MainTable = ds.Tables[0];
                    if (MainTable.Rows.Count > 0)
                    {
                        GV_StockPositionDetails.DataSource = MainTable;
                        GV_StockPositionDetails.DataBind();

                        GV_StockPositionDetails.FooterRow.Style.Add("text-align", "right");
                        GV_StockPositionDetails.FooterRow.Cells[8].Text = "Grand Total";
                        GV_StockPositionDetails.FooterRow.Cells[9].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn6Months")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[10].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn9Months")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[11].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn12Months")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[12].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn18Months")).ToString();

                        GV_StockPositionDetails.FooterRow.Cells[13].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn24Months")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[14].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn30Months")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[15].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn36Months")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[16].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionFiveYear")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[17].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionGreaterthanFiveYear")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[18].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString();
                        //GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AmountafterRMDSC")).ToString();
                    }
                    else
                    {
                        GV_StockPositionDetails.DataSource = null;
                        GV_StockPositionDetails.DataBind();
                    }

                }
            }
        }
    }

    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDistrict();
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranch();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodown();
    }

    //protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    protected void ddlgodowntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodown();
    }

    protected void GV_StockPositionDetails_OnRowDataBound(object sender, EventArgs e)
    {

    }

    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    if (ddlrole.SelectedValue == "0")
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Depositor')", true);
    //        ddlrole.Focus();
    //        return;
    //    }
    //    else if (ddlcommodity.SelectedValue == "0")
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Commodity')", true);
    //        ddlcommodity.Focus();
    //        return;
    //    }
    //    else if (ddlgodowntype.SelectedValue == "0")
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown Type')", true);
    //        ddlgodowntype.Focus();
    //        return;
    //    }
    //    else if (ddlgodown.SelectedValue == "0")
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown')", true);
    //        ddlgodown.Focus();
    //        return;
    //    }
    //    else
    //    {
    //        InsertStockPositionDetail();
    //    }
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillBillDetailsInGrid();
    }
}