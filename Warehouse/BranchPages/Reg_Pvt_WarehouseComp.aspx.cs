using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Globalization;

public partial class BranchPages_Reg_Pvt_WarehouseComp : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    string RegionID = "";
    string DistrictID = "";
    string BranchID = "";
    string ddlValue = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //FillRegionDD();
                divDistrict.Visible = false;
                divBranch.Visible = false;
                divSearch.Visible = false;
                divGrid.Visible = false;
                divBtnMap.Visible = false;
            }
        }
        catch (Exception ex)
        {
            string display = "Some error has occured, try again";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
        }

    }
    private void chkCriteria()
    {
        string value = rbCriteria.SelectedItem.Text.Trim();
        if (!string.IsNullOrEmpty(value))
        {
            if (value == "District")
            {
                divDistrict.Visible = true;
                divBranch.Visible = false;
                divSearch.Visible = false;
                divGrid.Visible = false;
                divBtnMap.Visible = false;
                FillRegionDD();
                ddlDistrict.Items.Clear();
                ddlDistrict.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else if (value == "Branch")
            {
                divBranch.Visible = true;
                divDistrict.Visible = false;
                divSearch.Visible = false;
                divGrid.Visible = false;
                divBtnMap.Visible = false;
                FillBranchDD();
            }
        }
        else
        {
            string display = "Please Select Criteria";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
        }
    }
    private void FillRegionDD()
    {
        qry = "select [Region_Id],[region] From [tbl_MetaData_Region] order by [region]";
        con.Open();
        cmd = new SqlCommand(qry, con);
        da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlRegion.DataSource = ds.Tables[0];
            ddlRegion.DataTextField = "region";
            ddlRegion.DataValueField = "Region_Id";
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlDistrict.Items.Insert(0, new ListItem("--Select--", "0"));
        }

    }

    private void FillBranchDD()
    {
        qry = "select [BranchId],[DepotName] From [tbl_MetaData_DEPOT] order by [DepotName]";
        con.Open();
        cmd = new SqlCommand(qry, con);
        da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("--Select--", "0"));
        }

    }

    private void FillPvtWarehouseGrid(string ddlValue)
    {
        if (string.Equals(ddlValue, "district"))
        {
            DistrictID = ddlDistrict.SelectedValue;
            qry = "select [Registration_No], [Warehouse_Name],[Address],[Authorized_PName],[Mobile_No],[Email_Id],[Capacity(MT)],[Registration_Date]";
            qry = qry + " from [tbl_Registered_PVTWarehouse] where [District_Id]='" + DistrictID + "'and [Registration_No] not in(select Registration_No from tbl_Godown_PVTWarehouse_Map) order by Warehouse_Name";
        }
        else if (string.Equals(ddlValue, "branch"))
        {
            BranchID = ddlBranch.SelectedValue;
            qry = "select [Registration_No], [Warehouse_Name],[Address],[Authorized_PName],[Mobile_No],[Email_Id],[Capacity(MT)],[Registration_Date]";
            qry = qry + " from [tbl_Registered_PVTWarehouse] where [Br_Name]='" + BranchID + "'and [Registration_No] not in(select Registration_No from tbl_Godown_PVTWarehouse_Map) order by Warehouse_Name";
        }

        if (!string.IsNullOrEmpty(DistrictID) || !string.IsNullOrEmpty(BranchID))
        {
            con.Open();
            cmd = new SqlCommand(qry, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPVTWarehouse.DataSource = ds.Tables[0];
                gvPVTWarehouse.DataBind();

            }
        }
    }

    private void FillGodownGrid(string ddlValue)
    {
        if (string.Equals(ddlValue, "district"))
        {
            DistrictID = ddlDistrict.SelectedValue;
            qry = "select [Godown_ID],[Godown_Name],[Godown_Address],[Godown_Mobile],[Godown_Email],[Godown_Capacity],[Godown_Scientific_Capacity],[Godown_APN]";
            qry = qry + "from [tbl_MetaData_GODOWN] where [DistrictId]='" + DistrictID + "' and [Hired_Type]!='Owned' and [Godown_ID] not in(select Godown_ID from tbl_Godown_PVTWarehouse_Map) order by Godown_Name";
        }
        else if (string.Equals(ddlValue, "branch"))
        {
            BranchID = ddlBranch.SelectedValue;
            qry = "select [Godown_ID],[Godown_Name],[Godown_Address],[Godown_Mobile],[Godown_Email],[Godown_Capacity],[Godown_Scientific_Capacity],[Godown_APN]";
            qry = qry + "from [tbl_MetaData_GODOWN] where [BranchID]='" + BranchID + "' and [Hired_Type]!='Owned' and [Godown_ID] not in(select Godown_ID from tbl_Godown_PVTWarehouse_Map) order by Godown_Name";
        }

        if (!string.IsNullOrEmpty(DistrictID) || !string.IsNullOrEmpty(BranchID))
        {
            con.Open();
            cmd = new SqlCommand(qry, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGodown.DataSource = ds.Tables[0];
                gvGodown.DataBind();
            }
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlValue = "district";
        divSearch.Visible = true;
        divGrid.Visible = true;
        divBtnMap.Visible = true;
        FillPvtWarehouseGrid(ddlValue);
        FillGodownGrid(ddlValue);
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlValue = "branch";
        divSearch.Visible = true;
        divGrid.Visible = true;
        divBtnMap.Visible = true;
        FillPvtWarehouseGrid(ddlValue);
        FillGodownGrid(ddlValue);
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        RegionID = ddlRegion.SelectedValue;
        qry = "select [District_Id],[District_Name] from [tbl_MetaData_DISTRICT] where [Region_ID]='" + RegionID + "'order by [District_Name]";
        con.Open();
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        ddlDistrict.DataSource = ds;
        ddlDistrict.DataTextField = "District_Name";
        ddlDistrict.DataValueField = "District_Id";
        ddlDistrict.DataBind();
        ddlDistrict.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void gvPVTWarehouse_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        //DistrictID = ddlDistrict.SelectedValue;
        //gvPVTWarehouse.PageIndex = e.NewPageIndex;
        //FillPvtWarehouseGrid(DistrictID);
    }

    protected void gvGodown_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        //DistrictID = ddlDistrict.SelectedValue;
        //gvGodown.PageIndex = e.NewPageIndex;
        //FillGodownGrid(DistrictID);
    }

    protected void btnMapRecords_Click(object sender, EventArgs e)
    {
        try
        {

            string qrySelect = "";
            string qryInsert = "";
            string gId = "";
            string regNo = "";
            string wName = "";
            string wAdd = "";
            string wAuthPName = "";
            string wMobNo = "";
            string wEmailId = "";
            string wCapacity = "";
            string wRegDate = "";
            string wRegion = "";
            string wDistrict = "";
            string wBranch = "";
            int count = 0;
            int chkcnt = 0;

            CheckBox chkRowgd = null;
            CheckBox chkRowwhr = null;


            foreach (GridViewRow row2 in gvGodown.Rows)
            {

                if (row2.RowType == DataControlRowType.DataRow)
                {
                    chkRowgd = (row2.Cells[0].FindControl("chbGodown") as CheckBox);
                    if (chkRowgd.Checked)
                    {
                        chkcnt = chkcnt + 1;
                        gId = row2.Cells[1].Text;
                        chkRowgd.Checked = false;
                        break;
                    }
                }
            }


            foreach (GridViewRow row1 in gvPVTWarehouse.Rows)
            {
                if (row1.RowType == DataControlRowType.DataRow)
                {
                    chkRowwhr = (row1.Cells[0].FindControl("chbPvtWarehouse") as CheckBox);

                    if (chkRowwhr.Checked)
                    {
                        chkcnt = chkcnt + 1;

                        regNo = row1.Cells[1].Text;
                        qrySelect = "select [Warehouse_Name],[Address],[Authorized_PName],[Mobile_No],[Email_Id],[Capacity(MT)],[Registration_Date],[Region_Id],[District_Id],[Br_Name]";
                        qrySelect = qrySelect + " from [tbl_Registered_PVTWarehouse] where [Registration_No]='" + regNo + "' order by Warehouse_Name";
                        con.Open();
                        cmd = new SqlCommand(qrySelect, con);
                        da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        con.Close();
                        wName = ds.Tables[0].Rows[0][0].ToString();
                        wAdd = ds.Tables[0].Rows[0][1].ToString();
                        wAuthPName = ds.Tables[0].Rows[0][2].ToString();
                        wMobNo = ds.Tables[0].Rows[0][3].ToString();
                        wEmailId = ds.Tables[0].Rows[0][4].ToString();
                        wCapacity = ds.Tables[0].Rows[0][5].ToString();
                        wRegDate = ds.Tables[0].Rows[0][6].ToString();
                        wRegion = ds.Tables[0].Rows[0][7].ToString();
                        wDistrict = ds.Tables[0].Rows[0][8].ToString();
                        wBranch = ds.Tables[0].Rows[0][9].ToString();

                        qry = "Select [Godown_ID] from [tbl_Godown_PVTWarehouse_Map] where [Registration_No]='" + regNo + "' ";
                        con.Open();
                        cmd = new SqlCommand(qry, con);
                        da = new SqlDataAdapter(cmd);
                        DataSet ds1 = new DataSet();
                        da.Fill(ds1);
                        con.Close();
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            string display1 = "Record Already Exist";
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display1 + "');", true);
                        }
                        else
                        {
                            qryInsert = "insert into [tbl_Godown_PVTWarehouse_Map] ([Godown_ID],[Registration_No],[Warehouse_Name],[Address]," +
                            "[Authorized_PName],[Mobile_No],[Email_Id],[Capacity(MT)],[Registration_Date],[Region_Id],[District_Id],[Branch_Id]) " +
                            "values('" + gId + "','" + regNo + "','" + wName + "','" + wAdd + "','" + wAuthPName + "','" + Convert.ToDouble(wMobNo) + "'," +
                            " '" + wEmailId + "','" + Convert.ToDecimal(wCapacity) + "','" + DateTime.Parse(wRegDate) + "','" + wRegion + "','" + wDistrict + "','" + wBranch + "') ";

                            con.Open();
                            cmd = new SqlCommand(qryInsert, con);
                            int x = cmd.ExecuteNonQuery();
                            con.Close();
                            count = count + x;


                        }
                        chkRowwhr.Checked = false;
                        break;
                    }
                }
            }

            RegionID = ddlRegion.SelectedValue;
            DistrictID = ddlDistrict.SelectedValue;
            BranchID = ddlBranch.SelectedValue;
            string value = rbCriteria.SelectedItem.Text.Trim();

            if (value == "District")
            {

                if (RegionID == null || RegionID == "0" || RegionID == "")
                {
                    string display = "Please Select Region";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                }
                else if (DistrictID == null || DistrictID == "0" || DistrictID == "")
                {
                    string display = "Please Select District";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                }
            }
            else if (value == "Branch")
            {
                if (BranchID == null || BranchID == "0" || BranchID == "")
                {
                    string display = "Please Select Branch";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                }
            }


            if (chkcnt < 2)
            {
                string display = "No Values Selected for Mapping";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
            }
            else
            {
                string display = "Godown Information Updated Successfully!";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                FillGodownGrid(value.ToLower());
                FillPvtWarehouseGrid(value.ToLower());

            }
        }
        catch (ArgumentNullException)
        {
            divMsg.Visible = true;
            Label2.Visible = true;
            Label2.Text = "Error has occured. Record not updated";

        }
        catch (ArgumentOutOfRangeException)
        {
            divMsg.Visible = true;
            Label2.Visible = true;
            Label2.Text = "Error has occured. Record not updated";

        }
        catch (FormatException)
        {
            divMsg.Visible = true;
            Label2.Visible = true;
            Label2.Text = " Date is not in the correct format. Record not updated";

        }
        catch (Exception ex)
        {
            divMsg.Visible = true;
            Label2.Visible = true;
            Label2.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }
    }
    //protected void chbGodown_CheckedChanged(object sender, EventArgs e)
    //{

    //}

    protected void rbCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkCriteria();
    }
}
