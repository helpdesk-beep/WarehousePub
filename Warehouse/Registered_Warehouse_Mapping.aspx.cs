using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Registered_Warehouse_Mapping : System.Web.UI.Page
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
                adDDLValueGodownType();
                divrblist.Visible = false;
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

    private void adDDLValueGodownType()
    {
        ddlGodownType.Items.Insert(0, new ListItem("--Select--", "0"));
        ddlGodownType.Items.Insert(1, new ListItem("Owned", "Owned"));
        ddlGodownType.Items.Insert(2, new ListItem("Hired", "Hired"));
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

    private void FillGodownGrid(string ddlValue)
    {
        string gdTypeValue = ddlGodownType.SelectedItem.Text.Trim();
        if (gdTypeValue == "Owned")
        {
            if (string.Equals(ddlValue, "district"))
            {
                DistrictID = ddlDistrict.SelectedValue;
                qry = "select [Godown_ID],[Godown_Name],[Godown_Address],[Godown_Mobile],[Godown_Email],[Godown_Capacity],[Godown_Scientific_Capacity],[Godown_APN]";
                qry = qry + "from [tbl_MetaData_GODOWN] where [DistrictId]='" + DistrictID + "' and [Hired_Type]='Owned' and [Godown_ID] not in(select Godown_ID from [tbl_Godown_Warehouse_Map]) order by Godown_Name";
            }
            else if (string.Equals(ddlValue, "branch"))
            {
                BranchID = ddlBranch.SelectedValue;
                qry = "select [Godown_ID],[Godown_Name],[Godown_Address],[Godown_Mobile],[Godown_Email],[Godown_Capacity],[Godown_Scientific_Capacity],[Godown_APN]";
                qry = qry + "from [tbl_MetaData_GODOWN] where [BranchID]='" + BranchID + "' and [Hired_Type]='Owned' and [Godown_ID] not in(select Godown_ID from [tbl_Godown_Warehouse_Map]) order by Godown_Name";
            }
        }
        else if (gdTypeValue == "Hired")
        {
            if (string.Equals(ddlValue, "district"))
            {
                DistrictID = ddlDistrict.SelectedValue;
                qry = "select [Godown_ID],[Godown_Name],[Godown_Address],[Godown_Mobile],[Godown_Email],[Godown_Capacity],[Godown_Scientific_Capacity],[Godown_APN]";
                qry = qry + "from [tbl_MetaData_GODOWN] where [DistrictId]='" + DistrictID + "' and [Hired_Type]!='Owned' and [Godown_ID] not in(select Godown_ID from [tbl_Godown_Warehouse_Map]) order by Godown_Name";
            }
            else if (string.Equals(ddlValue, "branch"))
            {
                BranchID = ddlBranch.SelectedValue;
                qry = "select [Godown_ID],[Godown_Name],[Godown_Address],[Godown_Mobile],[Godown_Email],[Godown_Capacity],[Godown_Scientific_Capacity],[Godown_APN]";
                qry = qry + "from [tbl_MetaData_GODOWN] where [BranchID]='" + BranchID + "' and [Hired_Type]!='Owned' and [Godown_ID] not in(select Godown_ID from [tbl_Godown_Warehouse_Map]) order by Godown_Name";
            }
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
            else
            {
                gvGodown.DataSource = null;
                gvGodown.DataBind();
                lblGridGMsg.Visible = true;
                lblGridGMsg.Text = "No Godown Available";

            }

        }
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

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlValue = "district";
        divSearch.Visible = true;
        divGrid.Visible = true;
        divBtnMap.Visible = true;
        lblGridGMsg.Visible = false;
        lblGridWMsg.Visible = false;
        FillWarehouseGrid(ddlValue);
        FillGodownGrid(ddlValue);
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlValue = "branch";
        divSearch.Visible = true;
        divGrid.Visible = true;
        divBtnMap.Visible = true;
        lblGridGMsg.Visible = false;
        lblGridWMsg.Visible = false;
        FillWarehouseGrid(ddlValue);
        FillGodownGrid(ddlValue);
    }

    private void FillWarehouseGrid(string ddlValue)
    {
        string gdTypeValue = ddlGodownType.SelectedItem.Text.Trim();

        if (string.Equals(ddlValue, "district"))
        {
            DistrictID = ddlDistrict.SelectedValue;
            qry = "select [Registration_No], [Warehouse_Name],[Address],[Authorized_PName],[Mobile_No],[Email_Id],[Capacity(MT)],[Registration_Date],[Warehouse_Type]";
            qry = qry + " from [tbl_Registered_Warehouse] where [District_Id]='" + DistrictID + "' and [Warehouse_Type]='" + gdTypeValue + "' and [Registration_No] not in(select Registration_No from [tbl_Godown_Warehouse_Map]) order by Warehouse_Name";
        }
        else if (string.Equals(ddlValue, "branch"))
        {
            BranchID = ddlBranch.SelectedValue;
            qry = "select [Registration_No], [Warehouse_Name],[Address],[Authorized_PName],[Mobile_No],[Email_Id],[Capacity(MT)],[Registration_Date]";
            qry = qry + " from [tbl_Registered_Warehouse] where [Br_Name]='" + BranchID + "'and [Warehouse_Type]='" + gdTypeValue + "' and [Registration_No] not in(select Registration_No from [tbl_Godown_Warehouse_Map]) order by Warehouse_Name";
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
                gvWarehouse.DataSource = ds.Tables[0];
                gvWarehouse.DataBind();

            }
            else
            {
                gvWarehouse.DataSource = null;
                gvWarehouse.DataBind();
                lblGridWMsg.Visible = true;
                lblGridWMsg.Text = "No Warehouse Available";
            }
        }
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
            string wType = "";
            
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


            foreach (GridViewRow row1 in gvWarehouse.Rows)
            {
                if (row1.RowType == DataControlRowType.DataRow)
                {
                    chkRowwhr = (row1.Cells[0].FindControl("chbWarehouse") as CheckBox);

                    if (chkRowwhr.Checked)
                    {
                        chkcnt = chkcnt + 1;

                        regNo = row1.Cells[1].Text;
                        qrySelect = "select [Warehouse_Name],[Address],[Authorized_PName],[Mobile_No],[Email_Id],[Capacity(MT)],[Registration_Date],[Region_Id],[District_Id],[Br_Name],[Warehouse_Type]";
                        qrySelect = qrySelect + " from [tbl_Registered_Warehouse] where [Registration_No]='" + regNo + "' order by Warehouse_Name";
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
                        wType = ds.Tables[0].Rows[0][10].ToString();

                        qry = "Select [Godown_ID] from [tbl_Godown_Warehouse_Map] where [Registration_No]='" + regNo + "' ";
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
                            qryInsert = "insert into [tbl_Godown_Warehouse_Map]([Godown_ID],[Registration_No],[Warehouse_Name],[Address]," +
                            "[Authorized_PName],[Mobile_No],[Email_Id],[Capacity(MT)],[Registration_Date],[Region_Id],[District_Id],[Branch_Id],[Warehouse_Type]) " +
                            "values('" + gId + "','" + regNo + "','" + wName + "','" + wAdd + "','" + wAuthPName + "','" + Convert.ToDouble(wMobNo) + "'," +
                            " '" + wEmailId + "','" + Convert.ToDecimal(wCapacity) + "','" + DateTime.Parse(wRegDate) + "','" + wRegion + "','" + wDistrict + "','" + wBranch + "','" + wType + "') ";

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
                FillWarehouseGrid(value.ToLower());

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

    protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string gdTypeValue = ddlGodownType.SelectedItem.Text.Trim();
        rbCriteria.SelectedIndex = -1;
        divrblist.Visible = false;
        divDistrict.Visible = false;
        divBranch.Visible = false;
        divSearch.Visible = false;
        divGrid.Visible = false;
        divBtnMap.Visible = false;

            if (!string.IsNullOrEmpty(gdTypeValue) && gdTypeValue != "0" && gdTypeValue != "--Select--")
            {
                divrblist.Visible = true;
            }
         
    }

    protected void rbCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkCriteria();
    }

  }