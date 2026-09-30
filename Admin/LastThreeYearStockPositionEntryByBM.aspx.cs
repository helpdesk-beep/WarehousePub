using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WLCBusinessLayer;
using System.Drawing;
using System.Xml;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

public partial class Admin_LastThreeYearStockPositionEntryByBM : System.Web.UI.Page
{
    Admin clsAdmin = new Admin();
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
            FillGrid();
            fillRoll();
            fillCommodity();
            FillGodownType();
            SetInitialRow();
            if (Request.QueryString["ID"] != null)
            {
                fillRegion();
               // filldetails();
            }
        }

    }
    private void fillRoll()
    {
        string query = "select Depositor_ID, Depositor_Name FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679','181','15478')";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlrole.Items.Clear();
            ddlrole.DataSource = ds.Tables[0];
            ddlrole.DataTextField = "Depositor_Name";
            ddlrole.DataValueField = "Depositor_ID";
            ddlrole.DataBind();
            ddlrole.Items.Insert(0, "Select");
            //ddlrole.SelectedValue = "22";
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
            ddlregion.DataSource = ds.Tables[0];
            ddlregion.DataTextField = "Regionnm";
            ddlregion.DataValueField = "Region_ID";
            ddlregion.DataBind();
            ddlregion.Items.Insert(0, "-Select");
        }

    }

    
    private void fillDistrict()
    {

        string query = "select district_id,DIstrict_Name from tbl_MetaData_district where Region_ID ='" + ddlregion.SelectedValue + "'";
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
            ddldistrict.Items.Insert(0, "-Select");
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
                ddlbranch.Items.Insert(0, "-Select");
            }
        
    }
    private void fillCommodity()
    {
        string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in('8','131','122','129','63','25','64','12','92','33','13','3','23','75','27','22','35') order by Commodity_Name";
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
            ddlcommodity.Items.Insert(0, "Select");
            //ddlcommodity.SelectedValue = "22";
        }
    }
    private void fillGodown()
    {

        string query = " SELECT Godown_ID,Godown_Name  FROM tbl_MetaData_GODOWN_2018 where BranchID  ='" + ddlbranch.SelectedValue + "' and Hired_Type  ='" + ddlgodowntype.SelectedItem.ToString() + "' and IsActive='Y' order by Godown_Name Asc";
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
            ddlgodown.Items.Insert(0, "Select");
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
            ddlgodowntype.Items.Insert(0, "Select");
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
    
    //protected void btnSave_Click(object sender, EventArgs e)
    //{

        //try
        //{
            
        //    if (btnSave.Text != "Update")
        //    {
        //        //clsguarddata.Insert_GuardData(txtdate.Text.Trim(), ddlcategory.SelectedValue, ddlservicepost.SelectedValue, txtname.Text.Trim(), txtfathername.Text.Trim(), txttelno.Text.Trim(), txtmobileno.Text.Trim(), txtaddress.Text.Trim(), ddlReligion.SelectedValue, txtexpectedsalary.Text.Trim(), cbexp.Checked == true ? "1" : "0", txtyear.Text.Trim(), txtcompany.Text.Trim(), clsCredentials.GetUserNo, ddldistrict.SelectedValue, txtreference.Text.Trim(), txtremark.Text.Trim(), txtrefcontactno.Text.Trim(), ddlcall.SelectedValue, txtcalldate.Text.Trim(), txtcalldetail.Text.Trim());
        //        Response.Write(getdata("Insert"));
        //        DataTable Dt = clsAdmin.CreateNewUser(getdata("Insert"));

        //        if (Dt != null && Dt.Rows.Count > 0 && Convert.ToInt32("0" + Dt.Rows[0][0]) > 0)
        //        {
        //            // fns.updateLOG("Guard Data has been created !!", "", txtdate.Text.Trim(), clsCredentials.GetUserNo());
        //            lblmsg.Text = "Creat User Sucessfully";
        //            FillGrid();
        //            //  FillData();
        //            //clear();
        //        }
        //        else
        //        {
        //            lblmsg.Text = "User Not Created";
        //        }

        //    }
        //    else
        //    {

        //        //clsguarddata.Insert_GuardData(txtdate.Text.Trim(), ddlcategory.SelectedValue, ddlservicepost.SelectedValue, txtname.Text.Trim(), txtfathername.Text.Trim(), txttelno.Text.Trim(), txtmobileno.Text.Trim(), txtaddress.Text.Trim(), ddlReligion.SelectedValue, txtexpectedsalary.Text.Trim(), cbexp.Checked == true ? "1" : "0", txtyear.Text.Trim(), txtcompany.Text.Trim(), clsCredentials.GetUserNo, ddldistrict.SelectedValue, txtreference.Text.Trim(), txtremark.Text.Trim(), txtrefcontactno.Text.Trim(), ddlcall.SelectedValue, txtcalldate.Text.Trim(), txtcalldetail.Text.Trim());
        //        Response.Write(getdata("Edit"));
        //        DataTable Dt = clsAdmin.CreateNewUser(getdata("Edit"));

        //        if (Dt != null && Dt.Rows.Count > 0 && Convert.ToInt32("0" + Dt.Rows[0][0]) > 0)
        //        {
        //            lblmsg.Text = "User Details Update Sucessfully";
        //            FillGrid();
        //            //  FillData();
        //            //clear();
        //        }
        //        else
        //        {
        //            lblmsg.Text = "User Details Not Update";
        //        }

        //    }


        //}
        //catch (Exception ex)
        //{
        //    lblmsg.Text = ex.Message.ToString();
        //}

    //}

    //public string getdata(string forwhat)
    //{
    //    string newpassword = txtpassword.Text;
    //    byte[] newpasswordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(newpassword);
    //    string newsaltpwd = Convert.ToBase64String(newpasswordAndSaltBytes);
    //    byte[] passwor = System.Text.Encoding.UTF8.GetBytes(newpassword + newsaltpwd);
    //    byte[] newhashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwor);
    //    string newhashString = Convert.ToBase64String(newhashBytes);
    //    string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
    //    XmlWriterSettings wsetting = new XmlWriterSettings();
    //    wsetting.NewLineOnAttributes = true;
    //    wsetting.Indent = true;
    //    wsetting.OmitXmlDeclaration = true;
    //    wsetting.CloseOutput = false;
    //    wsetting.Encoding = Encoding.UTF8;
    //    StringBuilder str = new StringBuilder();
    //    XmlWriter xw = XmlWriter.Create(str, wsetting);
    //    xw.WriteStartDocument();
    //    xw.WriteStartElement("ROOT");
    //    xw.WriteStartElement("ROWS");
    //    xw.WriteAttributeString("Username", txtusername.Text);
    //    xw.WriteAttributeString("Password", txtpassword.Text);
    //    xw.WriteAttributeString("salt", newsaltpwd);
    //    xw.WriteAttributeString("hashedPassword", newhashString);
    //    xw.WriteAttributeString("Role", ddlrole.SelectedValue);
    //    xw.WriteAttributeString("Emp_ID", txtemail.Text);
    //    xw.WriteAttributeString("Region_ID", ddlregion.SelectedValue);
    //    xw.WriteAttributeString("District_ID", ddldistrict.SelectedValue);
    //    xw.WriteAttributeString("Branch_ID", ddlbranch.SelectedValue);
    //    xw.WriteAttributeString("Name", txtname.Text);
    //    xw.WriteAttributeString("Mobile_No", txtmobileno.Text);
    //    xw.WriteAttributeString("Created_By", localIP.ToString());

    //    if (forwhat.Equals("Edit"))
    //        xw.WriteAttributeString("ID", Request.QueryString["ID"].ToString());
    //    xw.WriteEndElement();
    //    xw.WriteEndElement();
    //    xw.WriteEndDocument();
    //    xw.Flush();
    //    xw.Close();
    //    return str.ToString();

    //}

    protected void gvAgreementList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRecord")
        {
            int id = int.Parse(e.CommandArgument.ToString());

            DataTable dt = new Admin().GetAgreementById(id);
            if (dt.Rows.Count > 0)
            {
                //hfId.Value = dt.Rows[0]["Id"].ToString();
                //// ddlSection.SelectedItem.Text = dt.Rows[0]["Section"].ToString();
                //txtTitle.Text = dt.Rows[0]["Title"].ToString();
                //txtExpDate.Text = Convert.ToDateTime(dt.Rows[0]["Expiry"]).ToString("dd/MM/yyyy");
                //hfFileName.Value = dt.Rows[0]["Filename"].ToString();

                //btnSave.Text = "UPDATE";
                //btnSave.CssClass = "btn btn-warning";
            }
        }


        if (e.CommandName == "DeleteRecord")
        {
            int id = int.Parse(e.CommandArgument.ToString());

            int rvalue = new Admin().DeleteAgreementById(id);

            if (rvalue > 0)
            {
                //lblMsg.Text = "Deleted Successfully";
                //lblMsg.ForeColor = Color.Green;

                //fillGrid();
            }

            else
            {
                //lblMsg.Text = "Sorry Not Delete";
                //lblMsg.ForeColor = Color.Red;
            }
        }
    }

    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("CreateUser.aspx");
    //}

    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDistrict();
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranch();
    }

    //protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillGodown();
    //}

    protected void ddlrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlrole.SelectedValue.ToString() == "2")
        //{
        //    fillRegion();
        //    divregion.Visible = true;
        //    divdistrict.Visible = false;
        //    divbranch.Visible = false;
        //    divgodown.Visible = false;
        //}
        //else if (ddlrole.SelectedValue.ToString() == "3")
        //{
        //    divregion.Visible = true;
        //    divdistrict.Visible = true;
        //    divbranch.Visible = false;
        //    divgodown.Visible = false;
        //}
        //else if (ddlrole.SelectedValue.ToString() == "4")
        //{
        //    divregion.Visible = true;
        //    divdistrict.Visible = true;
        //    divbranch.Visible = true;
        //    divgodown.Visible = false;
        //}
        //else if (ddlrole.SelectedValue.ToString() == "5")
        //{
        //    divregion.Visible = true;
        //    divdistrict.Visible = true;
        //    divbranch.Visible = true;
        //    divgodown.Visible = true;
        //}
        //else
        //{
        //    divregion.Visible = false;
        //    divdistrict.Visible = false;
        //    divbranch.Visible = false;
        //    divgodown.Visible = false;
        //}
    }

    protected void ddlgodowntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodown();
    }


    // Ad new row Code

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
        string RegionID = ddlregion.SelectedValue.ToString();///Session["Region_ID"].ToString();
        string DistrictID = ddldistrict.SelectedValue.ToString();
        string BranchID = ddlbranch.SelectedValue.ToString();
        string GodownID = ddlgodown.SelectedValue.ToString();
        string DepositorID = ddlrole.SelectedValue.ToString();
        string CommodityID = ddlcommodity.SelectedValue.ToString();
        string GodownType = ddlgodowntype.SelectedItem.ToString();

        string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

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
                TextBox vtxtEighteenMonth = (TextBox)GV_CommodityInfo.Rows[j].Cells[5].FindControl("txtEighteenMonth");
                TextBox vtxtTwentyFourMonth = (TextBox)GV_CommodityInfo.Rows[j].Cells[6].FindControl("txtTwentyFourMonth");
                TextBox vtxtThirtyMonth = (TextBox)GV_CommodityInfo.Rows[j].Cells[3].FindControl("txtThirtyMonth");
                TextBox vtxtThirtySixMonth = (TextBox)GV_CommodityInfo.Rows[j].Cells[5].FindControl("txtThirtySixMonth");
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

                string qry = "INSERT INTO [dbo].[tbl_DepositorCommodityGodownwise] ([RegionID],[DistrictID],[BranchID],[GodownID],[DepositorID],[CommodityID],[CropYear],[GodownType]" +
                             ",[StockPositionIn6Months],[StockPositionIn9Months],[StockPositionIn12Months],[StockPositionIn18Months],[StockPositionIn24Months],[StockPositionIn30Months]" +
                             ",[StockPositionIn36Months],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[DeletedBy], [DeletedDate])" +
                             " VALUES ('" + RegionID + "','" + DistrictID + "','" + BranchID + "','" + GodownID + "','" + DepositorID + "','" + CommodityID + "','" + CropYear + "','" + GodownType + "','" + vSixMonth + "','" + vNineMonth + "','" + vTwelveMonth + "','" + vEighteenMonth + "','" + vTwentyFourMonth + "','" + vThirtyMonth + "','" + vThirtySixMonth + "','" + ClientIP + "', GETDATE()" + ",'" + ClientIP + "', GETDATE()" + ",'" + ClientIP + "', GETDATE()" + ")";
                cmd = new SqlCommand(qry, con);
                int c = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (c > 0)
                {
                    lblmsg.Text = "Record saved successfully";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record save successfully')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please enter Crop Year')", true);

            }
                //}
            }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlrole.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Depositor')", true);
            ddlrole.Focus();
            return;
        }
        else if (ddlcommodity.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Commodity')", true);
            ddlcommodity.Focus();
            return;
        }
        else if (ddlgodowntype.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown Type')", true);
            ddlgodowntype.Focus();
            return;
        }
        else if (ddlgodown.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown')", true);
            ddlgodown.Focus();
            return;
        }
        else
        {
            InsertStockPositionDetail();
        }
    }

}