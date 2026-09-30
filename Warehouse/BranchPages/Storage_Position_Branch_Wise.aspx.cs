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

public partial class BranchPages_Storage_Position_Branch_Wise : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        //string vBranchID = Session["BranchID"].ToString();


        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                lblRegionName.Text = fillRegion();
                lblDistrictName.Text = fillDistrict();
                lblBranchName.Text = fillBranch();
                fillDepositorType();
                fillCommodityType();
                fillGodownType();
               // fillGodownName();
                SetInitialRow();
                fillDetailsInGrid();
            }
        }
    }

    private void fillDepositorType()
    {
        try
        {
            string query = "";
            query = "select Depositor_ID, Depositor_Name FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679','181','15478') order by Depositor_Name ";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
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

    private void fillGodownType()
    {
        try
        {

            string query = "";
            query = "select distinct MG.Hired_Type [GodownType] from tbl_Metadata_Godown_2018 MG ORDER BY MG.Hired_Type";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodownType.Items.Clear();
                ddlGodownType.DataSource = ds.Tables[0];
                ddlGodownType.DataTextField = "GodownType";
                ddlGodownType.DataValueField = "GodownType";
                ddlGodownType.DataBind();
                ddlGodownType.Items.Insert(0, "--Select--");
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

    private void fillCommodityType()
    {
        try
        {

            string query = "";
            //query = "select Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in ('8','131','122','129','63','25','64','12','92','33','13','3','23','75','27','22','35') ORDER BY Commodity_Name asc";
            query = "select Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY ORDER BY Commodity_Name asc";
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
                ddlCommodity.Items.Insert(0, "--Select--");
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

    private string fillBranch()
    {
        string vBranchID = Session["BranchID"].ToString();
        try
        {

            string query = "";
            query = "select DepotName from tbl_MetaData_Depot where  BranchID = " + vBranchID + "";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblBranchName.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
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
        return lblBranchName.Text.ToString();
    }

    private string fillDistrict()
    {
        string vBranchID = Session["BranchID"].ToString();
        try
        {

            string query = "";
            query = "SELECT MD.District_ID [DistrictID], MD.District_Name [DistrictName] from tbl_MetaData_DISTRICT MD INNER JOIN tbl_MetaData_DEPOT MDD ON MD.District_Id = MDD.DistrictId WHERE MDD.DepotID = " + vBranchID + "";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblDistrictName.Text = ds.Tables[0].Rows[0]["DistrictName"].ToString();
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
        return lblDistrictName.Text.ToString();
    }

    private string fillRegion()
    {
        //string vRegionID = "1";//Session["RegionID"].ToString();
        string DistrictID = Session["Depot_DistID"].ToString();
        try
        {
            string query = "";
            query = "select MR.Region_Id [RegionID], MR.Region [RegionName] from tbl_Metadata_Region MR INNER JOIN tbl_Metadata_District MD ON MR.Region_Id = MD.Region_ID where MD.District_Id = " + DistrictID + "";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblRegionName.Text = ds.Tables[0].Rows[0]["RegionName"].ToString();
                RegionID = ds.Tables[0].Rows[0]["RegionID"].ToString();
                Session["Region_ID"] = RegionID.ToString();


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
        return lblRegionName.Text.ToString();
    }

    private void fillGodownName()
    {
        string vBranchID = Session["BranchID"].ToString();
        try
        {
            string query = "";
            query = "select MG.Godown_ID [GodownID], MG.Godown_Name [GodownName] from tbl_Metadata_Godown_2018 MG WHERE MG.BranchID =" + vBranchID + " and Hired_Type  ='" + ddlGodownType.SelectedItem.ToString() + "' and IsActive='Y' order by Godown_Name Asc";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodownName.Items.Clear();
                ddlGodownName.DataSource = ds.Tables[0];
                ddlGodownName.DataTextField = "GodownName";
                ddlGodownName.DataValueField = "GodownID";
                ddlGodownName.DataBind();
                ddlGodownName.Items.Insert(0, "--Select--");
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

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("CropYear", typeof(string)));
        dt.Columns.Add(new DataColumn("StockPositionIn6Months", typeof(decimal)));
        dt.Columns.Add(new DataColumn("StockPositionIn9Months", typeof(decimal)));
        dt.Columns.Add(new DataColumn("StockPositionIn12Months", typeof(decimal)));
        dt.Columns.Add(new DataColumn("StockPositionIn18Months", typeof(decimal)));
        dt.Columns.Add(new DataColumn("StockPositionIn24Months", typeof(decimal)));
        dt.Columns.Add(new DataColumn("StockPositionIn30Months", typeof(decimal)));
        dt.Columns.Add(new DataColumn("StockPositionIn36Months", typeof(decimal)));
        dt.Columns.Add(new DataColumn("StockPositionFiveYear", typeof(decimal)));
        dt.Columns.Add(new DataColumn("StockPositionGreaterthanFiveYear", typeof(decimal)));

        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["CropYear"] = 0;
        dr["StockPositionIn6Months"] = 0.00;
        dr["StockPositionIn9Months"] = 0.00;
        dr["StockPositionIn12Months"] = 0.00;
        dr["StockPositionIn18Months"] = 0.00;
        dr["StockPositionIn24Months"] = 0.00;
        dr["StockPositionIn30Months"] = 0.00;
        dr["StockPositionIn36Months"] = 0.00;
        dr["StockPositionFiveYear"] = 0.00;
        dr["StockPositionGreaterthanFiveYear"] = 0.00;

        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        GV_CommodityInfo.DataSource = dt;
        GV_CommodityInfo.DataBind();
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    private void AddNewRowToGrid()
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                drCurrentRow["CropYear"] = "";
                drCurrentRow["StockPositionIn6Months"] = 0;
                drCurrentRow["StockPositionIn9Months"] = 0;
                drCurrentRow["StockPositionIn12Months"] = 0;
                drCurrentRow["StockPositionIn18Months"] = 0;
                drCurrentRow["StockPositionIn24Months"] = 0;
                drCurrentRow["StockPositionIn30Months"] = 0;
                drCurrentRow["StockPositionIn36Months"] = 0;


                //add new row to DataTable
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState
                ViewState["CurrentTable"] = dtCurrentTable;

                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    TextBox vSixMonth = (TextBox)GV_CommodityInfo.Rows[i].Cells[1].FindControl("txtSixMonth");
                    TextBox vNineMonth = (TextBox)GV_CommodityInfo.Rows[i].Cells[1].FindControl("txtNineMonth");
                    TextBox vTwelveMonth = (TextBox)GV_CommodityInfo.Rows[i].Cells[1].FindControl("txtTwelveMonth");
                    TextBox vEighteenMonth = (TextBox)GV_CommodityInfo.Rows[i].Cells[1].FindControl("txtEighteenMonth");
                    TextBox vTwentyFourMonth = (TextBox)GV_CommodityInfo.Rows[i].Cells[1].FindControl("txtTwentyFourMonth");
                    TextBox vThirtyMonth = (TextBox)GV_CommodityInfo.Rows[i].Cells[1].FindControl("txtThirtyMonth");
                    TextBox vThirtySixMonth = (TextBox)GV_CommodityInfo.Rows[i].Cells[1].FindControl("txtThirtySixMonth");

                    dtCurrentTable.Rows[i]["StockPositionIn6Months"] = Convert.ToDecimal(vSixMonth.Text);
                    dtCurrentTable.Rows[i]["StockPositionIn9Months"] = Convert.ToDecimal(vNineMonth.Text);
                    dtCurrentTable.Rows[i]["StockPositionIn12Months"] = Convert.ToDecimal(vTwelveMonth.Text);
                    dtCurrentTable.Rows[i]["StockPositionIn18Months"] = Convert.ToDecimal(vEighteenMonth.Text);
                    dtCurrentTable.Rows[i]["StockPositionIn24Months"] = Convert.ToDecimal(vTwentyFourMonth.Text);
                    dtCurrentTable.Rows[i]["StockPositionIn30Months"] = Convert.ToDecimal(vThirtyMonth.Text);
                    dtCurrentTable.Rows[i]["StockPositionIn36Months"] = Convert.ToDecimal(vThirtySixMonth.Text);
                }

                //Rebind the Grid with the current data
                GV_CommodityInfo.DataSource = dtCurrentTable;
                GV_CommodityInfo.DataBind();

                //for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                //{
                //    if (Convert.ToDecimal(((TextBox)GV_CommodityInfo.Rows[i].FindControl("txtCapacity")).Text) != 0)
                //    {
                //        ((CheckBox)gvGodown.Rows[i].FindControl("ckstack")).Checked = true;
                //        ((TextBox)gvGodown.Rows[i].FindControl("txtLenght")).Enabled = false;
                //        ((TextBox)gvGodown.Rows[i].FindControl("txtWidth")).Enabled = false;
                //        ((TextBox)gvGodown.Rows[i].FindControl("txtHeight")).Enabled = false;
                //    }

                //}
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Set the Previous Selected Items on Each DropDownList on Postbacks
                    //DropDownList ddl1 = (DropDownList)gvGodown.Rows[rowIndex].Cells[1].FindControl("DropDownList1");
                    //DropDownList ddl1 = (DropDownList)GV_CommodityInfo.Rows[rowIndex].Cells[1].FindControl("ddlCropYear");
                    ////Fill the DropDownList with Data
                    //FillCropYearDropDownList(ddl1);
                    //if (i < dt.Rows.Count - 1)
                    //{
                    //    ddl1.ClearSelection();
                    //    ddl1.Items.FindByText(dt.Rows[i]["ddlCropYear"].ToString()).Selected = true;
                    //
                    //}
                    rowIndex++;
                }
            }
        }
    }


    protected void GV_CommodityInfo_OnRowDataBound(object sender, EventArgs e)
    {

    }

    private void FillCropYearDropDownList(DropDownList ddlCropYear)
    {
        ArrayList arr = GetDummyData();
        foreach (ListItem item in arr)
        {
            ddlCropYear.Items.Add(item);
        }
    }

    private ArrayList GetDummyData()
    {
        ArrayList arr = new ArrayList();
        arr.Add(new ListItem("2020-21", "2020-21"));
        arr.Add(new ListItem("2021-22", "2021-22"));
        arr.Add(new ListItem("2022-23", "2022-23"));
        arr.Add(new ListItem("2023-24", "2023-24"));
        arr.Add(new ListItem("2024-25", "2024-25"));
        return arr;
    }

    public void InsertStockPositionDetail()
    {
        string RegionID = Session["Region_ID"].ToString();///Session["Region_ID"].ToString();
        string DistrictID = Session["Depot_DistID"].ToString();
        string BranchID = Session["BranchID"].ToString();
        string GodownID = ddlGodownName.SelectedValue.ToString();
        string DepositorID = ddlDepositor.SelectedValue.ToString();
        string CommodityID = ddlCommodity.SelectedValue.ToString();
        string GodownType = ddlGodownType.SelectedItem.ToString();

        string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
        try
        {
            for (int j = 0; j < GV_CommodityInfo.Rows.Count; j++)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string vCropYear = ((DropDownList)GV_CommodityInfo.Rows[j].FindControl("ddlCropYear")).SelectedValue.ToString();
                if (vCropYear != "0" && vCropYear != "")
                {
                    //if (((CheckBox)GV_CommodityInfo.Rows[j].FindControl("ckstack")).Checked == true)
                    //{
                    TextBox vtxtSixMonth = (TextBox)GV_CommodityInfo.Rows[j].Cells[1].FindControl("txtSixMonth");
                    TextBox vtxtNineMonth = (TextBox)GV_CommodityInfo.Rows[j].Cells[2].FindControl("txtNineMonth");
                    TextBox vtxtTwelveMonth = (TextBox)GV_CommodityInfo.Rows[j].Cells[3].FindControl("txtTwelveMonth");
                    TextBox vtxtEighteenMonth = (TextBox)GV_CommodityInfo.Rows[j].Cells[4].FindControl("txtEighteenMonth");
                    TextBox vtxtTwentyFourMonth = (TextBox)GV_CommodityInfo.Rows[j].Cells[5].FindControl("txtTwentyFourMonth");
                    TextBox vtxtThirtyMonth = (TextBox)GV_CommodityInfo.Rows[j].Cells[6].FindControl("txtThirtyMonth");
                    TextBox vtxtThirtySixMonth = (TextBox)GV_CommodityInfo.Rows[j].Cells[7].FindControl("txtThirtySixMonth");
                    TextBox vtxtFiveYear = (TextBox)GV_CommodityInfo.Rows[j].Cells[8].FindControl("txtFiveYear");
                    TextBox vtxtGreaterthanFiveYear = (TextBox)GV_CommodityInfo.Rows[j].Cells[9].FindControl("txtGreaterthanFiveYear");
                    DropDownList vddlCropYear = (DropDownList)GV_CommodityInfo.Rows[j].FindControl("ddlCropYear");

                    //TextBox ConstructionYears = (TextBox)GV_CommodityInfo.Rows[j].Cells[6].FindControl("txtThirtySixMonth");

                    string CropYear = Convert.ToString(vddlCropYear.SelectedValue.ToString());
                    decimal vSixMonth = Convert.ToDecimal(vtxtSixMonth.Text);
                    decimal vNineMonth = Convert.ToDecimal(vtxtNineMonth.Text);
                    decimal vTwelveMonth = Convert.ToDecimal(vtxtTwelveMonth.Text);
                    decimal vEighteenMonth = Convert.ToDecimal(vtxtEighteenMonth.Text);
                    decimal vTwentyFourMonth = Convert.ToDecimal(vtxtTwentyFourMonth.Text);
                    decimal vThirtyMonth = Convert.ToDecimal(vtxtThirtyMonth.Text);
                    decimal vThirtySixMonth = Convert.ToDecimal(vtxtThirtySixMonth.Text);
                    decimal vFiveYear = Convert.ToDecimal(vtxtFiveYear.Text);
                    decimal vGreaterthanFiveYear = Convert.ToDecimal(vtxtGreaterthanFiveYear.Text);

                    string qry = "INSERT INTO [dbo].[tbl_DepositorCommodityGodownwise] ([RegionID],[DistrictID],[BranchID],[GodownID],[DepositorID],[CommodityID],[CropYear],[GodownType]" +
                                 ",[StockPositionIn6Months],[StockPositionIn9Months],[StockPositionIn12Months],[StockPositionIn18Months],[StockPositionIn24Months],[StockPositionIn30Months]" +
                                 ",[StockPositionIn36Months],[StockPositionFiveYear],[StockPositionGreaterthanFiveYear],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[DeletedBy], [DeletedDate])" +
                                 " VALUES ('" + RegionID + "','" + DistrictID + "','" + BranchID + "','" + GodownID + "','" + DepositorID + "','" + CommodityID + "','" + CropYear + "','" + GodownType + "','" + vSixMonth + "','" + vNineMonth + "','" + vTwelveMonth + "','" + vEighteenMonth + "','" + vTwentyFourMonth + "','" + vThirtyMonth + "','" + vThirtySixMonth + "','" + vFiveYear + "','" + vGreaterthanFiveYear + "','" + ClientIP + "', GETDATE()" + ",'" + ClientIP + "', GETDATE()" + ",'" + ClientIP + "', GETDATE()" + ")";
                    cmd = new SqlCommand(qry, con);
                    int c = cmd.ExecuteNonQuery();
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    if (c > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Saved Successfully')", true);
                        fillDetailsInGrid();
                    }
                    //}
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please enter Crop Year')", true);

                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No two CropYear for same commodity in same branch for same Depositor and Godown should be same')", true);
        }
    }

    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlDepositor.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Depositor')", true);
            ddlDepositor.Focus();
            return;
        }
        else if (ddlCommodity.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Commodity')", true);
            ddlCommodity.Focus();
            return;
        }
        else if (ddlGodownType.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown Type')", true);
            ddlGodownType.Focus();
            return;
        }
        else if (ddlGodownName.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown')", true);
            ddlGodownName.Focus();
            return;
        }
        else
        {
            InsertStockPositionDetail();
        }
    }

    protected void ddlcomodity_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodownName();
    }

    protected void ddlGodownName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GV_CommodityInfo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void fillDetailsInGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        string BranchID = Session["BranchID"].ToString();
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_EntryData", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    DataTable MainTable = ds.Tables[0];

                    if (MainTable.Rows.Count > 0)
                    {
                        GV_EntryDone.DataSource = MainTable;
                        GV_EntryDone.DataBind();
                    }
                    else
                    {
                        GV_EntryDone.DataSource = null;
                        GV_EntryDone.DataBind();
                    }

                }
            }
        }
    }

    protected void GV_EntryDone_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GV_EntryDone.Rows[rowIndex];

            //Fetch value of Name.
            string hdnId = (row.FindControl("hdnId") as HiddenField).Value;
            Session["hdnId"] = hdnId.ToString();
            RemoveRow(hdnId);

        }
    }

    public void RemoveRow(string id)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmd1 = new SqlCommand();
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            SqlCommand cmd = new SqlCommand("usp_DeleteEntry", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Deleted Successfully!');", true);
                fillDetailsInGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Deleted!');", true);
            }
            //string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
            //if (TheResult.StartsWith("SUCCESS"))
            //{
            //    string strMsg = "Remove Row Successfully|||";

            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);

            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
            //}
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }

    protected void GV_EntryDone_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

}


