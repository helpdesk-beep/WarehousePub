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

public partial class Masters_GodownMaster : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection JVScon = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlTransaction sqltran;
    string qry = "";
    decimal SciCapacity = 0, MaxCapacity = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Session["Depot_DepotID"] as string))
        {
            try
            {
                txtGodownName.Attributes.Add("onkeypress", "return Spc_characteralpha(this);");
                txtCapacity.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this);");
                txt_mobile.Attributes.Add("onkeypress", "return CheckNumeric(event,this);");

                if (Session["lang"].ToString() == "Hindi")
                {
                    btnaddnew.Text = Resources.hindi.btnaddnew;
                    Label3.Text = Resources.hindi.godawonname;
                    lblGodownMaster.Text = Resources.hindi.lblGodownMaster;
                    Label4.Text = Resources.hindi.godawoncapcity;
                    Label5.Text = Resources.hindi.godawontype;
                    Label6.Text = Resources.hindi.storagetype;
                    Label8.Text = Resources.hindi.lblAddress;
                    lbl_emailid.Text = Resources.hindi.lblEmail;
                    lbl_mobile.Text = Resources.hindi.lblMobileNo;
                    Label1.Text = Resources.hindi.SciCap;
                    lbl_apn.Text = Resources.hindi.godownownername;
                    // btnCan.Text = Resources.hindi.btncancel;
                }

                if (!IsPostBack)
                {
                    if (Session["Depot_DepotID"].ToString() != "")
                    {
                        GetRegID();
                        lbllicnu.Visible = true;
                        lbllidate.Visible = true;
                        txtlicnum.Visible = true;
                        txtlicdate.Visible = true;
                        string depotId = Session["Depot_DepotID"].ToString();
                        string Branchid = Session["BranchID"].ToString();
                        GetGodown(depotId);
                        fillIssuecenter();
                        getotherDepot();
                        fillGodownType();
                        Get_Blocks2();

                    }
                }
            }
            catch (Exception ex)
            {
                //  throw;
                lblMsg.Text = ex.Message.ToString();
            }
        }
        else
        {
            Response.Redirect("../login.aspx ");
        }
    }
    private void selectgodowntype()
    {
        string branchtype = Session["BranchType"].ToString();
        if (branchtype == "I" || branchtype == "O")
        {


        }

    }
    public void GetRegID()
    {
        ddlRegID.DataSource = "";
        string qry = "";
        qry = "select Registration_ID,UPPER(Warehouse_name) +' ( '+ Registration_ID +' )' as Warehouse_name from tbl_warehouseRegistration as WREG where WREG.BranchId='" + Session["BranchId"].ToString() + "' and RegCapacity>0 order by Warehouse_name";
        SqlCommand cmd = new SqlCommand(qry, JVScon);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlRegID.DataSource = ds.Tables[0];
            ddlRegID.DataTextField = "Warehouse_name";
            ddlRegID.DataValueField = "Registration_ID";
            ddlRegID.DataBind();
            ddlRegID.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlRegID.Items.Insert(0, "--Select--");
        }

    }
    private void fillIssuecenter()
    {
        try
        {
            string region = "";
            string District = Session["Depot_DistID"].ToString();
            string depotId = Session["Depot_DepotID"].ToString();
            string query = "";

            query = "SELECT DepotID,BranchId,DistrictId,DepotName FROM [tbl_MetaData_DEPOT] where DistrictId='" + District + "'";

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


            }

            else
            {
                ////
            }
            ddlbranch.SelectedValue = Session["BranchID"].ToString();
            ddlbranch.Enabled = false;
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
            string BranchType = Session["BranchType"].ToString();

            string query = "";
            //if (depottype == "I" || depottype == "O")
            //{
            query = "SELECT  [Gid],[GodownType],[TypeValue] FROM [dbo].[GodownTypeMaster]";

            //}
            //else
            //{
            //    query = "SELECT  [Gid],[GodownType],[TypeValue] FROM [dbo].[GodownTypeMaster] where RelBranchType='" + depottype + "'";
            //}
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddllst_hired.Items.Clear();
                ddllst_hired.DataSource = ds.Tables[0];
                ddllst_hired.DataTextField = "GodownType";
                ddllst_hired.DataValueField = "TypeValue";
                ddllst_hired.DataBind();


            }

            else
            {
                ////
            }
            ddlbranch.SelectedValue = Session["BranchID"].ToString();
            ddlbranch.Enabled = false;
        }
        catch (Exception)
        {
            //////
        }
    }

    //10/05/2015

    private void getotherDepot()
    {
        try
        {
            string str = "SELECT [BranchTypeID] FROM [MetaDataBranchWithIssueCenter] where BranchID='" + Session["BranchID"].ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string BranchType = ds.Tables[0].Rows[0][0].ToString();
                if (BranchType == "A")
                {
                    ddllst_hired.SelectedValue = "OtherAgency";
                    ddllst_hired.Enabled = false;
                }
                else
                {
                    ddllst_hired.SelectedValue = "Owned";
                    ddllst_hired.Enabled = true;
                }

            }
            else
            {
                ddllst_hired.SelectedValue = "Owned";
                ddllst_hired.Enabled = true;

            }


        }

        catch (Exception ex)
        {

        }
    }


    private void GetGodown(string depotId)
    {
        try
        {
            string BranchId = Session["BranchID"].ToString();
            //string qry = "select * from dbo.tbl_MetaData_GODOWN where BranchId='" + BranchId + "' and Remarks='Y' order by tbl_MetaData_GODOWN.Godown_Name ";
            string qry = "select GD.Godown_ID,GD.Godown_Name,GD.Godown_Capacity,GD.Godown_Scientific_Capacity,GD.Hired_Type,GD.Storage_Type,CONVERT(varchar(10),GD.LicDate,103) as Licence_Validity,GD.LicNum as Licence_No,CASE WHEN CONVERT(DATE,GD.CreatedDate,103)>='2021-03-22' THEN 'Yes' ELSE 'No' END [Print] from dbo.tbl_MetaData_GODOWN_2018 as GD where GD.BranchId='" + BranchId + "' and GD.Godown_ID in (select Godown_ID from tbl_MetaData_GODOWN where BranchID='" + BranchId + "' and Remarks='Y') order by GD.Godown_Name";

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblRowCount.Text = "Total records are : " + ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["dsGodown"] = ds;
                fillGrid(ds);

            }
        }
        catch (Exception)
        {
            //////
        }
    }

    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        PanelGodown.Visible = true;
        btnUpdate.Text = "Insert";
        btn_Close.Visible = false;
        btnaddnew.Visible = false;
        txtScientificCapacity.Text = "";
        txtGodownName.Enabled = true;
        txtCapacity.Text = "";
        selectgodowntype();

        /// Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('नया गोदाम Create करने का प्रावधान शाखा से बंद कर दिया है।..'); </script> ");


    }

    protected void godown_GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnPrint = (HiddenField)e.Row.FindControl("hdnPrint");
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                if (hdnPrint.Value.Equals("Yes"))
                {
                    lnkEdit.Visible = true;
                }
                else
                {
                    lnkEdit.Visible = false;
                }
                LinkButton lb = new LinkButton();
                lb = (LinkButton)e.Row.Cells[0].Controls[0];
                lb.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this row?');");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occurred,Please Try Again..'); </script> ");
        }
    }

    protected void godown_GridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnUpdate.Text = "Update";
        PanelGodown.Visible = true;
        txtGodownName.Enabled = false;
        string gid = godown_GridView.SelectedRow.Cells[9].Text;
        if (gid != "")
        {
            //string str = "  SELECT [Godown_ID],[Godown_APN],[Godown_Email],[Godown_Mobile],[Godown_Address],LicNum,convert (nvarchar(20),LicDate,103) as LicDate FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] WHERE Godown_ID='" + gid + "'";
            string str = "  SELECT [Godown_ID],[Godown_APN],[Godown_Email],[Khasranum],[TehshilID],[VillageName],[Rakwanum],[Godown_Mobile],[Godown_Address],LicNum,convert (nvarchar(20),LicDate,103) as LicDate,Latitude,Longitude,GodownNum FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] WHERE Godown_ID='" + gid + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                txt_APN.Text = ds.Tables[0].Rows[0]["Godown_APN"].ToString();
                txt_emailid.Text = ds.Tables[0].Rows[0]["Godown_Email"].ToString();
                txt_mobile.Text = ds.Tables[0].Rows[0]["Godown_Mobile"].ToString();
                txt_address.Text = ds.Tables[0].Rows[0]["Godown_Address"].ToString();
                txtlicdate.Text = ds.Tables[0].Rows[0]["LicDate"].ToString();
                txtlicnum.Text = ds.Tables[0].Rows[0]["LicNum"].ToString();
                if (ds.Tables[0].Rows[0]["VillageName"].ToString() != "")
                {
                    //ddlVillage.SelectedValue = ds.Tables[0].Rows[0]["VillageName"].ToString();
                    //ddlVillage.SelectedItem.Text = ds.Tables[0].Rows[0]["VillageName"].ToString();
                    ddlVillage.DataSource = null;
                    ddlVillage.DataTextField = null;
                    ddlVillage.DataValueField = null;
                    ddlVillage.DataBind();
                    ddlVillage.Items.Insert(0, ds.Tables[0].Rows[0]["VillageName"].ToString());
                }
                else
                {
                    ddlVillage.DataSource = null;
                    ddlVillage.DataTextField = null;
                    ddlVillage.DataValueField = null;
                    ddlVillage.DataBind();
                    ddlVillage.Items.Insert(0, "--Select--");
                }
                if (ds.Tables[0].Rows[0]["TehshilID"].ToString() != "")
                {
                    ddlPBlock.SelectedValue = ds.Tables[0].Rows[0]["TehshilID"].ToString();
                }
                txtlatitude.Text = ds.Tables[0].Rows[0]["Latitude"].ToString();
                txtlongitude.Text = ds.Tables[0].Rows[0]["Longitude"].ToString();

                txtkhasra.Text = ds.Tables[0].Rows[0]["Khasranum"].ToString();
                txtrakwa.Text = ds.Tables[0].Rows[0]["Rakwanum"].ToString();
                txtgodownnum.Text = ds.Tables[0].Rows[0]["GodownNum"].ToString();
            }

            txtGodownName.Text = godown_GridView.SelectedRow.Cells[4].Text.Trim();
            Session["GodownName"] = godown_GridView.SelectedRow.Cells[4].Text.Trim();
            txtCapacity.Text = godown_GridView.SelectedRow.Cells[5].Text.Trim();
            if (godown_GridView.SelectedRow.Cells[6].Text.Trim() == "" || godown_GridView.SelectedRow.Cells[6].Text.Trim() == "&nbsp;")
            {
                txtScientificCapacity.Text = "";
            }
            else
            {

                txtScientificCapacity.Text = godown_GridView.SelectedRow.Cells[6].Text.Trim();
            }

            string hired = godown_GridView.SelectedRow.Cells[7].Text.Trim();
            ddllst_hired.SelectedItem.Selected = false;
            foreach (ListItem lst1 in ddllst_hired.Items)
            {
                if (lst1.Value == hired)
                {
                    lst1.Selected = true;
                }
            }
            string storage = godown_GridView.SelectedRow.Cells[8].Text.Trim();
            ddllst_storage.SelectedItem.Selected = false;
            foreach (ListItem lst1 in ddllst_storage.Items)
            {
                if (lst1.Value == storage)
                {
                    lst1.Selected = true;
                }
            }
            btn_Close.Visible = false;
            btnaddnew.Visible = false;
        }
        // Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('गोदाम संबंधित Updation शाखा लॉगिन से बंद कर दिया गया है। ..'); </script> ");
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    //public void whrnomax()
    //{
    //    try
    //    {

    //            string WHR_Id = "";
    //            if (con.State == ConnectionState.Closed)
    //            {
    //                con.Open();
    //            }
    //            string QueryMax = "select isnull(Max(Gid),0)+1 from [tbl_MetaData_GODOWN] where DistrictId='" + Session["Depot_DistID"].ToString() + "' and BranchID ='" + Session["BranchId"].ToString() + "' ";
    //            cmd = new SqlCommand(QueryMax, con); // check WhrId present in whr_status table
    //            string str3 = cmd.ExecuteScalar().ToString();
    //            if ((str3 == String.Empty) || str3 == "")
    //            {
    //                str3 = "0";
    //            }
    //            if (Convert.ToInt64(str3) != 0)
    //            {
    //                string Depotid = Session["Depot_DepotID"].ToString();
    //                WHR_Id = Session["BranchId"].ToString() + "G" + Convert.ToString(Convert.ToInt64(str3));
    //            }
    //            else
    //            {
    //                string Depotid = Session["Depot_DepotID"].ToString();
    //                WHR_Id = "";
    //                WHR_Id = Session["BranchId"].ToString() + "G" + Convert.ToString(Convert.ToInt64(str3));
    //            }
    //            Godown_ID.Text = WHR_Id.ToString();


    //    }
    //    catch (System.Data.SqlClient.SqlException ex)
    //    {
    //        string msg = "Insert Error:";
    //        msg += ex.Message;
    //        throw new Exception(msg);
    //    }
    //    finally
    //    {
    //        con.Close();
    //    }
    //}
    protected void ddlRegID_SelectedIndexChanged(object sender, EventArgs e)
    {
        string WH_RegName = ddlRegID.SelectedItem.Text;
        string[] WHName = WH_RegName.Split('(');
        lblwhname.Text = WHName[0].Trim();
        txtGodownName.Text = WHName[0].Trim();
        Session["JVS_RegNo"] = WHName[1].Trim().Replace(")", "");
        ModalPopupExtender1.Show();
    }
    protected void txtLenght_TextChanged(object sender, EventArgs e)
    {
        SciCapacity = Convert.ToDecimal(txtLenght.Text) * Convert.ToDecimal(txtWidth.Text) * ((Convert.ToDecimal(txtHeight.Text) - 3) / 80);
        txtScientificCapacity.Text = Math.Round((SciCapacity * 10), 2).ToString();
        MaxCapacity = ((SciCapacity * 10) * 100) / 100;
        txtCapacity.Text = Math.Round(MaxCapacity, 2).ToString();
        txtScientificCapacity.Enabled = false;
        txtCapacity.Enabled = false;
        ModalPopupExtender1.Show();

    }
    protected void txtWidth_TextChanged(object sender, EventArgs e)
    {
        SciCapacity = Convert.ToDecimal(txtLenght.Text) * Convert.ToDecimal(txtWidth.Text) * ((Convert.ToDecimal(txtHeight.Text) - 3) / 80);
        txtScientificCapacity.Text = Math.Round(SciCapacity * 10, 2).ToString();
        MaxCapacity = ((SciCapacity * 10) * 100) / 100;
        txtCapacity.Text = Math.Round(MaxCapacity, 2).ToString();
        txtScientificCapacity.Enabled = false;
        txtCapacity.Enabled = false;
        ModalPopupExtender1.Show();
    }
    protected void txtHeight_TextChanged(object sender, EventArgs e)
    {
        SciCapacity = Convert.ToDecimal(txtLenght.Text) * Convert.ToDecimal(txtWidth.Text) * ((Convert.ToDecimal(txtHeight.Text) - 3) / 80);
        txtScientificCapacity.Text = Math.Round(SciCapacity * 10, 2).ToString();
        MaxCapacity = ((SciCapacity * 10) * 100) / 100;
        txtCapacity.Text = Math.Round(MaxCapacity, 2).ToString();
        txtScientificCapacity.Enabled = false;
        txtCapacity.Enabled = false;
        ModalPopupExtender1.Show();
    }
    public void AddUpGodown()
    {
        string TheResult = "";
        //if (ddllst_hired.SelectedItem.Text != "Steel Silo")
        //{
        string Godown_ID = "";
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string godown = txtGodownName.Text.Trim();
        float capacity = CheckFloat(txtCapacity.Text.Trim());
        float scapacity = CheckFloat(txtScientificCapacity.Text.Trim());
        string hired = ddllst_hired.SelectedValue.Trim();
        string storage = ddllst_storage.SelectedValue.Trim();
        string distid = Session["Depot_DistID"].ToString();
        string depotId = Session["Depot_DepotID"].ToString();
        string BranchId = Session["BranchID"].ToString();
        //string depotId = ddlbranch.SelectedValue.ToString();
        string apn = txt_APN.Text.Trim();
        string email = txt_emailid.Text.Trim();
        string mobile = txt_mobile.Text.Trim();
        string address = txt_address.Text.Trim();
        float Godowncapacity;
        string Latitude = txtlatitude.Text;
        string Longitude = txtlongitude.Text;
        string khashra = txtkhasra.Text;
        string rakwa = txtrakwa.Text;
        string tehshil = ddlPBlock.SelectedValue.ToString();
        string village = ddlVillage.SelectedItem.Text;
        string WeightmentType = "";
        if (ddlWeightmentS.SelectedItem.Text == "Yes")
        {
            WeightmentType = ddlWeightmentType.SelectedValue;
        }
        else
        {
            WeightmentType = "N";
        }
        try
        {
            if (con != null)
            {
                con.Open();
                sqltran = con.BeginTransaction();
                if (btnUpdate.Text == "Insert")
                {
                    #region Insert Godown Master
                    //qry = "select MAX(RIGHT(Godown_ID,4)) as SGID from tbl_MetaData_GODOWN where BranchId='" + ddlbranch.SelectedValue.ToString() + "'";
                    //qry = "select MAX(RIGHT(Godown_ID,4)) as SGID from tbl_MetaData_GODOWN where BranchId='" + ddlbranch.SelectedValue.ToString() + "' and DistrictId='" + distid.ToString() + "'";
                    qry = "select MAX(RIGHT(Godown_ID,3)) as SGID from tbl_MetaData_GODOWN where BranchId='" + ddlbranch.SelectedValue.ToString() + "' and DistrictId='" + distid.ToString() + "'";

                    cmd = new SqlCommand(qry, con, sqltran); // check WhrId present in whr_status table
                    string str3 = cmd.ExecuteScalar().ToString();
                    if ((str3 == String.Empty) || str3 == "")
                    {
                        str3 = "0";
                    }
                    if (Convert.ToInt64(str3) != 0)
                    {
                        string AGC = "";
                        int MGN = Convert.ToInt32(str3);
                        int SubBN = MGN + 1;
                        string INCN = SubBN.ToString();
                        if (INCN.Length == 1)
                        {
                            AGC = "00" + INCN.ToString();
                        }
                        else if (INCN.Length == 2)
                        {
                            AGC = "0" + INCN.ToString();
                        }
                        else if (INCN.Length == 3)
                        {
                            AGC = INCN.ToString();
                        }
                        else if (INCN.Length > 3)
                        {
                            AGC = INCN.ToString();
                        }
                        //Godown_ID = Convert.ToString(Convert.ToInt64(str3) + 1);
                        if (distid.ToString() == "2353")
                        {
                            Godown_ID = (ddlbranch.SelectedValue.ToString() + Convert.ToString(Convert.ToInt64(str3) + 1)).ToString();
                        }
                        else
                        {
                            if (ddlbranch.SelectedValue.ToString() == "231800802")
                            {
                                Godown_ID = (ddlbranch.SelectedValue.ToString() + "00" + AGC).ToString();
                            }
                            else
                            {
                                //Godown_ID = (ddlbranch.SelectedValue.ToString() + Convert.ToString(Convert.ToInt64(str3) + 1)).ToString();
                                Godown_ID = (ddlbranch.SelectedValue.ToString() + "0" + AGC).ToString();
                            }
                        }
                    }
                    else
                    {
                        Godown_ID = ddlbranch.SelectedValue.ToString() + "001";
                    }

                    //string QueryMax = "select isnull(Max(Gid),0)+1 from tbl_MetaData_GODOWN where DistrictId='" + Session["Depot_DistID"].ToString() + "' and BranchId='" + Session["BranchId"].ToString() + "' ";
                    ////  cmd = new SqlCommand(QueryMax, con); // check WhrId present in whr_status table
                    //cmd = new SqlCommand(QueryMax, con, sqltran);
                    //string str3 = cmd.ExecuteScalar().ToString();
                    //if ((str3 == String.Empty) || str3 == "")
                    //{
                    //    str3 = "0";
                    //}
                    //if (Convert.ToInt64(str3) != 0)
                    //{

                    //    Godown_ID = ddlbranch.SelectedValue.ToString() + Convert.ToString(Convert.ToInt64(str3));
                    //}


                    cmd = new SqlCommand("Insert_Godown_Master", con, sqltran);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Godown_ID", Godown_ID);
                    cmd.Parameters.AddWithValue("@DepotId", depotId);
                    cmd.Parameters.AddWithValue("@DistrictId", distid);
                    cmd.Parameters.AddWithValue("@GodownName", godown);
                    cmd.Parameters.AddWithValue("@Capacity", capacity);
                    cmd.Parameters.AddWithValue("@SCapacity", scapacity);
                    cmd.Parameters.AddWithValue("@CreatedBy", ip);
                    cmd.Parameters.AddWithValue("@Hired_Type", hired);
                    cmd.Parameters.AddWithValue("@Storage_Type", storage);
                    cmd.Parameters.AddWithValue("@Godown_APN", apn);
                    cmd.Parameters.AddWithValue("@Godown_Email", email);
                    cmd.Parameters.AddWithValue("@Godown_Mobile", mobile);
                    cmd.Parameters.AddWithValue("@Godown_Address", address);
                    cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue.ToString());
                    if (txtlicnum.Text == "")
                    {
                        txtlicnum.Text = "NA";
                    }
                    cmd.Parameters.AddWithValue("@LicNum", txtlicnum.Text);
                    if (txtlicdate.Text == "")
                    {
                        txtlicdate.Text = "01/01/2016";
                    }
                    cmd.Parameters.AddWithValue("@LicDate", getDate_MDY(txtlicdate.Text));
                    cmd.Parameters.AddWithValue("@LicIssueDate", txtLicIssueDate.Text);
                    cmd.Parameters.AddWithValue("@Latitude", Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", Longitude);
                    cmd.Parameters.AddWithValue("@GodownNum", txtgodownnum.Text);
                    cmd.Parameters.AddWithValue("@khashranum", khashra);
                    cmd.Parameters.AddWithValue("@Rakwanum", rakwa);
                    cmd.Parameters.AddWithValue("@TehshilID", tehshil);
                    cmd.Parameters.AddWithValue("@Village", village);
                    cmd.Parameters.AddWithValue("@WeightmentType", WeightmentType);
                    cmd.Parameters.AddWithValue("@Lenght", txtLenght.Text);
                    cmd.Parameters.AddWithValue("@Width", txtWidth.Text);
                    cmd.Parameters.AddWithValue("@Height", txtHeight.Text);
                    cmd.Parameters.AddWithValue("@PremiseCpt", txtPremiseCpt.Text);
                    if (ddlselfpms.SelectedValue == "1")
                    {
                        cmd.Parameters.AddWithValue("@Maintain_By", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Maintain_By", ddlselfpms.SelectedValue);
                    }

                    if (Session["JVS_RegNo"] != null)
                    {
                        //cmd.Parameters.AddWithValue("@JVS_RegNo", Session["JVS_RegNo"].ToString());
                        cmd.Parameters.AddWithValue("@JVS_RegNo", Session["JVS_RegNo"].ToString());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@JVS_RegNo", "NA");
                    }

                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        try
                        {
                            string query = "Select * from tbl_MetaData_GODOWN where Godown_ID='" + Godown_ID + "'";
                            cmd = new SqlCommand(query, con, sqltran);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds != null)
                            {

                                // csms_godown_new.csms_godown_webser newgodwn = new csms_godown_new.csms_godown_webser();
                                // newgodwn.InsertRecord(Godown_ID, "23", distid, depotId, godown, "01/01/2016", "01/01/2016", capacity.ToString(), "Y", hired.ToString(), storage.ToString(), scapacity.ToString(), BranchId);

                            }
                        }
                        catch (Exception ex)
                        {
                            ////
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                            lblMsg.Text = ex.Message;
                        }
                        btn_Close.Visible = true;
                        btnaddnew.Visible = true;
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record saved Successfully..'); </script> ");
                        Session["GodownID"] = Godown_ID;

                    }
                    else
                    {
                        if (TheResult.StartsWith("ALREADY"))
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Godown Already Created..'); </script> ");

                        }
                        else if (TheResult.StartsWith("FAIL"))
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Not Saved..'); </script> ");
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + TheResult + "'); </script> ");
                        }
                    }
                    #endregion
                }
                else if (btnUpdate.Text == "Update")
                {
                    string godownid = godown_GridView.SelectedRow.Cells[3].Text.Trim();
                    int status = 0;
                    if (Session["GodownName"].ToString() == txtGodownName.Text.Trim())
                    {
                        status = 0;
                    }
                    else
                    {
                        status = 1;
                    }
                    qry = "select Godown_Capacity from tbl_MetaData_GODOWN where Godown_ID='" + godownid + "' and BranchID='" + BranchId + "' ";
                    cmd = new SqlCommand(qry, con, sqltran);
                    string str1 = cmd.ExecuteScalar().ToString();
                    Godowncapacity = CheckFloat(str1);

                    string qry2 = "select sum(Stack_capacity) as stack_capacity from tbl_MetaData_Stack where Godown_ID='" + godownid + "' and BranchID='" + ddlbranch.SelectedValue.ToString() + "' and Stack_Killed='N' ";
                    cmd = new SqlCommand(qry2, con, sqltran);
                    string str2 = cmd.ExecuteScalar().ToString();
                    float stackcapacity = CheckFloat(str2);

                    if (capacity >= stackcapacity)
                    {
                        #region Update Godown Master
                        qry = "Insert Into tbl_MetaData_GODOWN_log SELECT * from tbl_MetaData_GODOWN where Godown_ID='" + godownid + "' and BranchID='" + BranchId + "'";
                        cmd = new SqlCommand(qry, con, sqltran);
                        int a = cmd.ExecuteNonQuery();
                        if (a > 0)
                        {
                            cmd = new SqlCommand("Update_Godown_Master_New", con, sqltran);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Godown_Name", godown);
                            cmd.Parameters.AddWithValue("@Godown_Id", godownid);
                            cmd.Parameters.AddWithValue("@DepotId", depotId);
                            cmd.Parameters.AddWithValue("@Godown_Capacity", capacity);
                            cmd.Parameters.AddWithValue("@SCapacity", scapacity);
                            cmd.Parameters.AddWithValue("@status", status);
                            cmd.Parameters.AddWithValue("@UpdatedBy", ip);
                            cmd.Parameters.AddWithValue("@Hired_Type", hired);
                            cmd.Parameters.AddWithValue("@Storage_Type", storage);
                            cmd.Parameters.AddWithValue("@Godown_APN", apn);
                            cmd.Parameters.AddWithValue("@Godown_Email", email);
                            cmd.Parameters.AddWithValue("@Godown_Mobile", mobile);
                            cmd.Parameters.AddWithValue("@Godown_Address", address);
                            if (txtlicnum.Text == "")
                            {
                                txtlicnum.Text = "NA";
                            }
                            cmd.Parameters.AddWithValue("@LicNum", txtlicnum.Text);
                            if (txtlicdate.Text == "")
                            {

                                txtlicdate.Text = "01/01/2016";
                            }
                            cmd.Parameters.AddWithValue("@LicDate", getDate_MDY(txtlicdate.Text));
                            cmd.Parameters.AddWithValue("@LicIssueDate", txtLicIssueDate.Text);
                            cmd.Parameters.AddWithValue("@Latitude", Latitude);
                            cmd.Parameters.AddWithValue("@Longitude", Longitude);
                            cmd.Parameters.AddWithValue("@GodownNum", txtgodownnum.Text);
                            cmd.Parameters.AddWithValue("@khashranum", khashra);
                            cmd.Parameters.AddWithValue("@Rakwanum", rakwa);
                            cmd.Parameters.AddWithValue("@TehshilID", tehshil);
                            cmd.Parameters.AddWithValue("@Village", village);
                            cmd.Parameters.AddWithValue("@WeightmentType", WeightmentType);
                            cmd.Parameters.AddWithValue("@Lenght", txtLenght.Text);
                            cmd.Parameters.AddWithValue("@Width", txtWidth.Text);
                            cmd.Parameters.AddWithValue("@Height", txtHeight.Text);
                            cmd.Parameters.AddWithValue("@PremiseCpt", txtPremiseCpt.Text);
                            cmd.Parameters.AddWithValue("@JVS_RegNo", hdnJVS_RegNo.Value);

                            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                            if (TheResult.StartsWith("SUCCESS"))
                            {
                                try
                                {

                                    string query = "Select * from tbl_MetaData_GODOWN where Godown_ID='" + godownid + "'";
                                    cmd = new SqlCommand(query, con, sqltran);
                                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds);
                                    if (ds != null)
                                    {

                                        //csms_godown_new.csms_godown_webser newgodwn = new csms_godown_new.csms_godown_webser();
                                        // newgodwn.UpdateRecord(godownid, "23", distid, depotId, godown, "01/01/2016", "01/01/2016", capacity.ToString(), "Y", hired.ToString(), storage.ToString(), scapacity.ToString(), BranchId);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    lblMsg.Text = ex.Message;
                                }

                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record updated successfully..'); </script> ");
                                Session["GodownID"] = godownid;
                                //Response.Redirect("~/Masters/PrintGodownMaster.aspx");
                                // lblMsg.Visible = false;
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Not updated..'); </script> ");
                            }

                            int res = cmd.ExecuteNonQuery();
                            if (res > 0)
                            {


                            }
                        }

                        #endregion
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Godown Capacity can not be less than Total stack Capacity'); </script> ");
                        lblMsg.Visible = false;
                    }
                }
                txtGodownName.Text = "";
                txtCapacity.Text = "";
                txt_address.Text = "";
                txt_APN.Text = "";
                txt_emailid.Text = "";
                txt_mobile.Text = "";
                ddllst_hired.SelectedItem.Selected = false;
                ddllst_storage.SelectedItem.Selected = false;
                PanelGodown.Visible = false;
                btn_Close.Visible = true;
                btnaddnew.Visible = true;
                sqltran.Commit();
                GetGodown(depotId);
                Response.Redirect("~/Masters/PrintGodownMaster.aspx");
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            lblMsg.Text = ex.Message;
        }
        finally
        {
            sqltran.Dispose();
            con.Close();
        }
        //}
        //else
        //{
        //    string Godown_ID = "";
        //    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        //    string godown = txtGodownName.Text.Trim();
        //    float capacity = CheckFloat(txtCapacity.Text.Trim());
        //    float scapacity = CheckFloat(txtScientificCapacity.Text.Trim());
        //    string hired = ddllst_hired.SelectedValue.Trim();
        //    string storage = ddllst_storage.SelectedValue.Trim();
        //    string distid = Session["Depot_DistID"].ToString();
        //    string depotId = Session["Depot_DepotID"].ToString();
        //    string apn = txt_APN.Text.Trim();
        //    string email = txt_emailid.Text.Trim();
        //    string mobile = txt_mobile.Text.Trim();
        //    string address = txt_address.Text.Trim();
        //    float Godowncapacity;
        //    float stackcapacity;
        //    string Latitude = txtlatitude.Text;
        //    string Longitude = txtlongitude.Text;
        //    string khashra = txtkhasra.Text;
        //    string rakwa = txtrakwa.Text;
        //    string tehshil = ddlPBlock.SelectedValue.ToString();
        //    string village = ddlVillage.SelectedItem.Text;
        //    try
        //    {
        //        if (con != null)
        //        {
        //            con.Open();
        //            sqltran = con.BeginTransaction();
        //            if (btnUpdate.Text == "Insert")
        //            {
        //                #region Insert Godown Master
        //                qry = "select Max(Godown_ID) from tbl_MetaData_GODOWN where BranchId='" + ddlbranch.SelectedValue.ToString() + "' and DistrictId='" + distid + "' ";
        //                cmd = new SqlCommand(qry, con, sqltran); // check WhrId present in whr_status table
        //                string str3 = cmd.ExecuteScalar().ToString();
        //                if ((str3 == String.Empty) || str3 == "")
        //                {
        //                    str3 = "0";
        //                }
        //                if (Convert.ToInt64(str3) != 0)
        //                {
        //                    Godown_ID = Convert.ToString(Convert.ToInt64(str3) + 1);
        //                }
        //                else
        //                {
        //                    Godown_ID = ddlbranch.SelectedValue.ToString() + "001";
        //                }
        //                cmd = new SqlCommand("sp_insertGodownMaster_New", con, sqltran);
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@Godown_ID", Godown_ID);
        //                cmd.Parameters.AddWithValue("@DepotId", depotId);
        //                cmd.Parameters.AddWithValue("@DistrictId", distid);
        //                cmd.Parameters.AddWithValue("@GodownName", godown);
        //                cmd.Parameters.AddWithValue("@Capacity", capacity);
        //                cmd.Parameters.AddWithValue("@SCapacity", scapacity);
        //                cmd.Parameters.AddWithValue("@CreatedBy", ip);
        //                cmd.Parameters.AddWithValue("@Hired_Type", hired);
        //                cmd.Parameters.AddWithValue("@Storage_Type", storage);
        //                cmd.Parameters.AddWithValue("@Godown_APN", apn);
        //                cmd.Parameters.AddWithValue("@Godown_Email", email);
        //                cmd.Parameters.AddWithValue("@Godown_Mobile", mobile);
        //                cmd.Parameters.AddWithValue("@Godown_Address", address);
        //                cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue.ToString());
        //                cmd.Parameters.AddWithValue("@LicNum", txtlicnum.Text);
        //                cmd.Parameters.AddWithValue("@LicDate", getDate_MDY(txtlicdate.Text));
        //                cmd.Parameters.AddWithValue("@Latitude", Latitude);
        //                cmd.Parameters.AddWithValue("@Longitude", Longitude);
        //                cmd.Parameters.AddWithValue("@GodownNum", txtgodownnum.Text);
        //                cmd.Parameters.AddWithValue("@khashranum", khashra);
        //                cmd.Parameters.AddWithValue("@Rakwanum", rakwa);
        //                cmd.Parameters.AddWithValue("@TehshilID", tehshil);
        //                cmd.Parameters.AddWithValue("@Village", village);
        //                int res = cmd.ExecuteNonQuery();
        //                if (res > 0)
        //                {
        //                    try
        //                    {

        //                        //insert into metadatasilo

        //                        cmd = new SqlCommand("insert into [Tbl_MetaData_Silo]([DistrictID],[BranchAssociatID],[SiloID],[SiloName],[SiloAddress],[HiredType],[StorageType],[MaxCapacity],[SciCapacity],[MobileNo],[EmailId],[LicenceNum],[LicenceDate],[AuthSignatory],[WlcCoSign],[createddate],[creatadby]) values (@DistrictId,@DepotId,@Godown_ID,@GodownName,@Godown_Address,@Hired_Type,@Storage_Type,@Capacity,@SCapacity,@Godown_Mobile,@Godown_Email,@licnum,@licdate,@Godown_APN,@wlccosign,getdate(),@CreatedBy)", con, sqltran);
        //                        cmd.CommandType = CommandType.Text;
        //                        cmd.Parameters.AddWithValue("@Godown_ID", Godown_ID);
        //                        cmd.Parameters.AddWithValue("@DepotId", ddlbranch.SelectedValue.ToString());
        //                        cmd.Parameters.AddWithValue("@DistrictId", distid);
        //                        cmd.Parameters.AddWithValue("@GodownName", godown);
        //                        cmd.Parameters.AddWithValue("@Capacity", capacity);
        //                        cmd.Parameters.AddWithValue("@SCapacity", scapacity);
        //                        cmd.Parameters.AddWithValue("@CreatedBy", ip);
        //                        cmd.Parameters.AddWithValue("@Hired_Type", hired);
        //                        cmd.Parameters.AddWithValue("@Storage_Type", storage);
        //                        cmd.Parameters.AddWithValue("@Godown_APN", apn);
        //                        cmd.Parameters.AddWithValue("@Godown_Email", email);
        //                        cmd.Parameters.AddWithValue("@Godown_Mobile", mobile);
        //                        cmd.Parameters.AddWithValue("@Godown_Address", address);
        //                        cmd.Parameters.AddWithValue("@licnum", txtlicnum.Text.Trim());
        //                        cmd.Parameters.AddWithValue("@licdate", getDate_MDY(txtlicdate.Text.Trim()));
        //                        cmd.Parameters.AddWithValue("@wlccosign", "MPWLC Auth Person");


        //                        int exe = cmd.ExecuteNonQuery();
        //                        if (exe > 0)
        //                        {

        //                            if (ddllst_storage.SelectedItem.Text == "Steel Silo")
        //                            {
        //                                string stack_id = "";



        //                                string stackname = "01";

        //                                //string commodity = dprlst_Commodity.SelectedValue.ToString();

        //                                decimal GodowncapacityS = 0;
        //                                decimal Sumofstackcap = 0;
        //                                decimal Allowstackcap = 0;


        //                                //qry = "IF EXISTS (select * from tbl_metadata_stack where Godown_ID='" + godownid + "')BEGIN select convert(decimal(18,2),GD.Godown_Capacity) as Godown_Capacity,ISNULL(convert(decimal(18,2),sum(ST.Stack_capacity)),0) as Stack_capacity from tbl_MetaData_GODOWN as GD LEFT JOIN tbl_MetaData_STACK as ST ON GD.Godown_ID = ST.Godown_ID where gd.Godown_ID='" + godownid + "' and gd.DepotId='" + depotId + "' and Stack_Killed='N' group by GD.Godown_Capacity,GD.Godown_ID END ELSE BEGIN select convert(decimal(18,2),GD.Godown_Capacity) as Godown_Capacity,ISNULL(convert(decimal(18,2),sum(ST.Stack_capacity)),0) as Stack_capacity from tbl_MetaData_GODOWN as GD LEFT JOIN tbl_MetaData_STACK as ST ON GD.Godown_ID = ST.Godown_ID where gd.Godown_ID='" + godownid + "' and gd.DepotId='" + depotId + "'  group by GD.Godown_Capacity,GD.Godown_ID END";
        //                                qry = "select convert(decimal(18,2),GD.Godown_Capacity) as Godown_Capacity ,(select ISNULL(convert(decimal(18,2),sum(ST.Stack_capacity)),0) as Stack_capacity from dbo.tbl_MetaData_STACK as ST where Godown_ID='" + Godown_ID + "'  and Stack_Killed='N' and ST.BranchID ='" + ddlbranch.SelectedValue.ToString() + "') as Stack_capacity from tbl_MetaData_GODOWN as GD where Godown_ID='" + Godown_ID + "' and gd.BranchId='" + ddlbranch.SelectedValue.ToString() + "'";
        //                                cmd = new SqlCommand(qry, con, sqltran);
        //                                SqlDataAdapter da = new SqlDataAdapter();
        //                                DataSet ds = new DataSet();
        //                                da = new SqlDataAdapter(cmd);
        //                                ds = new DataSet();
        //                                da.Fill(ds);
        //                                if (ds.Tables[0].Rows.Count > 0)
        //                                {
        //                                    GodowncapacityS = Convert.ToDecimal(ds.Tables[0].Rows[0]["Godown_Capacity"].ToString());
        //                                    Sumofstackcap = Convert.ToDecimal(ds.Tables[0].Rows[0]["Stack_capacity"].ToString());
        //                                }
        //                                Allowstackcap = GodowncapacityS - Sumofstackcap;
        //                                if (btnUpdate.Text == "Insert")
        //                                {
        //                                    if (Allowstackcap >= Convert.ToDecimal(capacity))
        //                                    {

        //                                        qry = "select isnull(Max(Stack_ID),0) from tbl_MetaData_STACK where BranchID='" + ddlbranch.SelectedValue.ToString() + "' and Godown_ID='" + Godown_ID + "' ";
        //                                        cmd = new SqlCommand(qry, con, sqltran); // check WhrId present in whr_status table
        //                                        string str35 = cmd.ExecuteScalar().ToString();
        //                                        if (Convert.ToInt64(str35) != 0)
        //                                        {
        //                                            stack_id = Convert.ToString(Convert.ToInt64(str35) + 1);
        //                                            //lbl_stackid.Text = stack_id;
        //                                        }
        //                                        else
        //                                        {
        //                                            stack_id = Godown_ID + "0001";
        //                                            // lbl_stackid.Text = stack_id;
        //                                        }

        //                                        cmd = new SqlCommand("sp_insertStackMaster_New", con, sqltran);
        //                                        cmd.CommandType = CommandType.StoredProcedure;
        //                                        cmd.Parameters.AddWithValue("@Stack_ID", stack_id);
        //                                        cmd.Parameters.AddWithValue("@DepotId", depotId);
        //                                        cmd.Parameters.AddWithValue("@District_Id", distid);
        //                                        cmd.Parameters.AddWithValue("@Godown_ID", Godown_ID);
        //                                        cmd.Parameters.AddWithValue("@Stack_Name", stackname);
        //                                        cmd.Parameters.AddWithValue("@Commodity_Id", 22);
        //                                        cmd.Parameters.AddWithValue("@Category_Id", "1");
        //                                        cmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());

        //                                        if (capacity != null)
        //                                        {
        //                                            cmd.Parameters.AddWithValue("@Stack_capacity", Convert.ToDecimal(capacity));
        //                                        }
        //                                        else
        //                                        {
        //                                            cmd.Parameters.AddWithValue("@Stack_capacity", 0);
        //                                        }


        //                                        cmd.Parameters.AddWithValue("@Parant_Stack_ID", DBNull.Value);
        //                                        cmd.Parameters.AddWithValue("@Remarks", DBNull.Value);


        //                                        cmd.Parameters.AddWithValue("@Storage_Type", ddllst_storage.SelectedItem.Text);
        //                                        cmd.Parameters.AddWithValue("@Hired_type", ddllst_hired.SelectedItem.Text);
        //                                        cmd.Parameters.AddWithValue("@CreatedBy", ip);
        //                                        int ris = cmd.ExecuteNonQuery();
        //                                        if (ris > 0)
        //                                        {

        //                                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record saved Successfully..')", true);


        //                                        }
        //                                        else
        //                                        {
        //                                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Saved,Stack Name Already Exits..')", true);
        //                                        }
        //                                        // sqltran.Commit();


        //                                    }
        //                                    else
        //                                    {
        //                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Can Not Insert Capacity More Than Gowdown Capacity ,The Maximum allowed capacity is =" + Allowstackcap.ToString() + "')", true);
        //                                    }
        //                                }

        //                            }


        //                        }

        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        ////
        //                    }



        //                }
        //                else
        //                {
        //                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Not Saved..'); </script> ");
        //                }

        //                #endregion
        //            }
        //            else if (btnUpdate.Text == "Update")
        //            {
        //                string godownid = godown_GridView.SelectedRow.Cells[9].Text.Trim();
        //                int status = 0;
        //                if (Session["GodownName"].ToString() == txtGodownName.Text.Trim())
        //                {
        //                    status = 0;
        //                }
        //                else
        //                {
        //                    status = 1;
        //                }
        //                qry = "select Godown_Capacity from tbl_MetaData_GODOWN where Godown_ID='" + godownid + "' and BranchID='" + ddlbranch.SelectedValue.ToString() + "' ";
        //                cmd = new SqlCommand(qry, con, sqltran);
        //                string str1 = cmd.ExecuteScalar().ToString();
        //                Godowncapacity = CheckFloat(str1);

        //                string qry2 = "select sum(Stack_capacity) as stack_capacity from tbl_MetaData_Stack where Godown_ID='" + godownid + "' and BranchID='" + ddlbranch.SelectedValue.ToString() + "' and Stack_Killed='N' ";
        //                cmd = new SqlCommand(qry2, con, sqltran);
        //                string str2 = cmd.ExecuteScalar().ToString();
        //                stackcapacity = CheckFloat(str2);
        //                if (capacity >= stackcapacity)
        //                {
        //                    #region Update Godown Master
        //                    qry = "Insert Into tbl_MetaData_GODOWN_log SELECT * from tbl_MetaData_GODOWN where Godown_ID='" + godownid + "' and BranchID='" + ddlbranch.SelectedValue.ToString() + "'";
        //                    cmd = new SqlCommand(qry, con, sqltran);
        //                    int a = cmd.ExecuteNonQuery();
        //                    if (a > 0)
        //                    {
        //                        cmd = new SqlCommand("sp_godownupdate_New", con, sqltran);
        //                        cmd.CommandType = CommandType.StoredProcedure;
        //                        cmd.Parameters.AddWithValue("@Godown_Name", godown);
        //                        cmd.Parameters.AddWithValue("@Godown_Id", godownid);
        //                        cmd.Parameters.AddWithValue("@DepotId", depotId);
        //                        cmd.Parameters.AddWithValue("@Godown_Capacity", capacity);
        //                        cmd.Parameters.AddWithValue("@SCapacity", scapacity);
        //                        cmd.Parameters.AddWithValue("@status", status);
        //                        cmd.Parameters.AddWithValue("@UpdatedBy", ip);
        //                        cmd.Parameters.AddWithValue("@Hired_Type", hired);
        //                        cmd.Parameters.AddWithValue("@Storage_Type", storage);
        //                        cmd.Parameters.AddWithValue("@Godown_APN", apn);
        //                        cmd.Parameters.AddWithValue("@Godown_Email", email);
        //                        cmd.Parameters.AddWithValue("@Godown_Mobile", mobile);
        //                        cmd.Parameters.AddWithValue("@Godown_Address", address);
        //                        cmd.Parameters.AddWithValue("@LicNum", txtlicnum.Text);
        //                        cmd.Parameters.AddWithValue("@LicDate", getDate_MDY(txtlicdate.Text));
        //                        cmd.Parameters.AddWithValue("@Latitude", Latitude);
        //                        cmd.Parameters.AddWithValue("@Longitude", Longitude);
        //                        cmd.Parameters.AddWithValue("@GodownNum", txtgodownnum.Text);
        //                        cmd.Parameters.AddWithValue("@khashranum", khashra);
        //                        cmd.Parameters.AddWithValue("@Rakwanum", rakwa);
        //                        cmd.Parameters.AddWithValue("@TehshilID", tehshil);
        //                        cmd.Parameters.AddWithValue("@Village", village);
        //                        int res = cmd.ExecuteNonQuery();
        //                        if (res > 0)
        //                        {
        //                            try
        //                            {
        //                                string query = "Select * from tbl_MetaData_GODOWN where Godown_ID='" + godownid + "'";
        //                                cmd = new SqlCommand(query, con, sqltran);
        //                                SqlDataAdapter da = new SqlDataAdapter(cmd);
        //                                DataSet ds = new DataSet();
        //                                da.Fill(ds);
        //                                if (ds != null)
        //                                {
        //                                    csms_godown_new.csms_godown_webser newgodwn = new csms_godown_new.csms_godown_webser();
        //                                    newgodwn.UpdateRecord(Godown_ID, "23", distid, depotId, godown, "01/01/2016", "01/01/2016", capacity.ToString(), "Y", hired.ToString(), storage.ToString(), scapacity.ToString());

        //                                }
        //                            }
        //                            catch (Exception)
        //                            {
        //                                ////
        //                            }

        //                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record updated successfully..'); </script> ");
        //                            lblMsg.Visible = false;

        //                        }
        //                    }

        //                    #endregion
        //                }
        //                else
        //                {
        //                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Godown Capacity can not be less than Total stack Capacity'); </script> ");
        //                    lblMsg.Visible = false;
        //                }
        //            }
        //            txtGodownName.Text = "";
        //            txtCapacity.Text = "";
        //            txt_address.Text = "";
        //            txt_APN.Text = "";
        //            txt_emailid.Text = "";
        //            txt_mobile.Text = "";
        //            ddllst_hired.SelectedItem.Selected = false;
        //            ddllst_storage.SelectedItem.Selected = false;

        //            sqltran.Commit();
        //            GetGodown(depotId);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sqltran.Rollback();
        //        // lblMsg.Text = "some error has been occured please try again later";
        //        lblMsg.Text = ex.Message;
        //    }
        //    finally
        //    {
        //        sqltran.Dispose();
        //        con.Close();
        //    }

        //}
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        if (txtGodownName.Text == "")
        {
            lblMsg.Text = "Enter Godown Name....";
        }
        else if (txtgodownnum.Text == "")
        {
            lblMsg.Text = "Enter Godown No....";
        }
        else if (txt_APN.Text == "")
        {
            lblMsg.Text = "Enter APN....";
        }
        else if (txt_emailid.Text == "")
        {
            lblMsg.Text = "Enter Email ID....";
        }
        else if (txt_mobile.Text == "")
        {
            lblMsg.Text = "Enter Moblie No....";
        }
        else if (txtCapacity.Text == "" || Convert.ToDecimal(txtCapacity.Text) == 0)
        {
            lblMsg.Text = "Enter Valid Max Capacity....";
        }
        else if (txtScientificCapacity.Text == "" || Convert.ToDecimal(txtScientificCapacity.Text) == 0)
        {
            lblMsg.Text = "Enter Valid Scientific Capacity....";
        }
        else if (txtlicnum.Text == "")
        {
            lblMsg.Text = "Enter Valid Licence No....";
        }
        else if (txtlicdate.Text == "")
        {
            lblMsg.Text = "Enter Valid Licence Date....";
        }
        else if (txt_address.Text == "")
        {
            lblMsg.Text = "Enter Valid Address...";
        }
        else if (!(txtlatitude.Text.Contains(".")) || (!(txtlongitude.Text.Contains("."))))
        {
            lblMsg.Text = "Fill Accurate Latitude & Longitude";
        }
        else if (ddlPBlock.SelectedItem.Text == "--Select--" || ddlVillage.SelectedItem.Text == "--Select--")
        {
            lblMsg.Text = "आपने खण्ड का चयन नहीं किया है";
        }
        else if (txtkhasra.Text == "")
        {
            lblMsg.Text = "Enter Valid Khasra No ...";
        }
        else if (txtrakwa.Text == "")
        {
            lblMsg.Text = "Enter Valid Rakba No ...";
        }
        else if (ddlWeightmentS.SelectedItem.Text == "--Select--")
        {
            lblMsg.Text = "Select Weighment....";
        }
        else if (ddlselfpms.SelectedValue == "--Select--")
        {
            lblMsg.Text = "Select Selef/PMS....";
        }
        else
        {
            AddUpGodown();
        }
    }

    private void fillGrid(DataSet ds)
    {
        godown_GridView.DataSource = ds.Tables[0];
        godown_GridView.DataBind();
        // lblRowCount.Text = "Total records are : " + godown_GridView.Rows.Count.ToString();
    }

    protected void godown_GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet ds = (DataSet)Session["dsGodown"];
        godown_GridView.PageIndex = e.NewPageIndex;
        fillGrid(ds);
    }

    float CheckFloat(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;
    }

    Int64 CheckInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        Int64 ValF = Int64.Parse(ValS);
        return ValF;
    }

    protected void godown_GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string depotId = Session["Depot_DepotID"].ToString();
            int _rowindex = e.RowIndex;
            string gid = godown_GridView.DataKeys[_rowindex].Value.ToString();
            ///////check godown in CSMS//////
            //string CSMSCK = CheckGodownInCSMS(gid);
            string CSMSCK = "Y";
            if (CSMSCK == "N")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There may be some stock in CSMS module...'); </script> ");
            }
            else
            {
                string stcCnt = "Select count(Stack_ID) as count from tbl_MetaData_STACK where Godown_ID='" + gid + "' and Stack_Killed = 'N'";
                SqlCommand cmd = new SqlCommand(stcCnt, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["count"]) > 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Delete Stack of this GODOWN first.'); </script> ");
                }
                else
                {
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    //string query = "update  tbl_MetaData_GODOWN set Remarks='N' output deleted.* into tbl_MetaData_GODOWN_log where Godown_ID='" + gid + "'";
                    string query = "update  tbl_MetaData_GODOWN set Remarks='N', [DeletedBy]='" + ip + "' , DeletedDate=getdate() output deleted.* into tbl_MetaData_GODOWN_log where Godown_ID='" + gid + "'";
                    if (con != null)
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = query;
                        int state = cmd.ExecuteNonQuery();

                        ///////////////////////////////////////////////////////////////

                        if (state > 0)
                        {

                            //string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                            //string queryupd = "update  tbl_MetaData_GODOWN_log set [DeletedBy]='" + ip + "' , DeletedDate='" + DateTime.Now.ToShortDateString() + "' where Godown_ID='" + gid + "'";
                            string queryupd = "update  tbl_MetaData_GODOWN_log set [DeletedBy]='" + ip + "' , DeletedDate=getdate() where Godown_ID='" + gid + "'";

                            cmd.Connection = con;
                            cmd.CommandText = queryupd;
                            int stateupd = cmd.ExecuteNonQuery();

                            //csms_godown_new.csms_godown_webser newgodwn = new csms_godown_new.csms_godown_webser();
                            // newgodwn.DeleteRecord(gid, Session["Depot_DistID"].ToString());

                        }
                        ///////////////////////////////////////////////////////////////

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data deleted Successfully......'); </script> ");
                        GetGodown(depotId);
                        btnUpdate.Text = "";
                        PanelGodown.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
    }

    protected void btnCan_Click(object sender, EventArgs e)
    {
        txtCapacity.Text = "";
        txtGodownName.Text = "";
        txt_address.Text = "";
        txt_emailid.Text = "";
        txt_mobile.Text = "";

        txt_APN.Text = "";
        ddllst_hired.SelectedItem.Selected = false;
        ddllst_storage.SelectedItem.Selected = false;
        PanelGodown.Visible = false;
        btnaddnew.Visible = true;
        btn_Close.Visible = true;

    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void txtScientificCapacity_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDecimal(txtScientificCapacity.Text) > Convert.ToDecimal(txtCapacity.Text))
        {
            lbl_checkcapcity.Visible = true;
            lbl_checkcapcity.Text = "*Scientific capacity should be less than maximum capacity";
            txtScientificCapacity.Text = "";
            txtCapacity.Text = "";
        }

        else

        {
            lbl_checkcapcity.Text = "";

        }
    }

    protected void ddllst_hired_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddllst_hired.SelectedValue == "OtherAgency")
        {
            lbllicnu.Visible = true;
            lbllidate.Visible = true;
            txtlicnum.Visible = true;
            txtlicdate.Visible = true;
        }
        else
        {

        }
    }

    public void Get_Blocks2()
    {
        string DistrictId = Session["Depot_DistID"].ToString();
        string qry = "SELECT [TehsilCode],[Tehsil_Name] FROM [Tehsils] where District_Code='" + DistrictId + "' order by [Tehsil_Name]";
        SqlCommand cmd = new SqlCommand(qry, con);
        DataSet ds1 = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds1);
        if (ds1 == null)
        {
        }
        else
        {
            ddlPBlock.DataSource = ds1.Tables[0];
            ddlPBlock.DataTextField = "Tehsil_Name";
            ddlPBlock.DataValueField = "TehsilCode";
            ddlPBlock.DataBind();
            ddlPBlock.Items.Insert(0, "--Select--");
        }
    }

    public void Get_Village()
    {
        //string DistrictId = ddlDistrict.SelectedValue.ToString();
        string qry = "SELECT [Hindi_Village] FROM [Intergrated_MP_STORAGE].[dbo].[VillageMaster] where Tehsil_ID='" + ddlPBlock.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        DataSet ds1 = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds1);
        if (ds1 == null)
        {
        }
        else
        {
            ddlVillage.DataSource = ds1.Tables[0];
            ddlVillage.DataTextField = "Hindi_Village";
            ddlVillage.DataValueField = "Hindi_Village";
            ddlVillage.DataBind();
            //  ddlVillage.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlPBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_Village();
    }
    protected void ddlPBlock_SelectedIndexChanged1(object sender, EventArgs e)
    {
        Get_Village();
    }
    public string CheckGodownInCSMS(string GOId)
    {
        string GodownsID = "";
        GodownsID = GOId.ToString(); ;
        string DeleteFlag = "N";
        //string stcCnt = "Select count(Recd_Godown) as counts from MPSCSC.dbo.SCSC_Procurement where Recd_Godown='" + GodownsID + "'";
        string stcCnt = "Select count(Recd_Godown) as counts from MPSCSC.dbo.SCSC_Procurement where Recd_Godown='" + GodownsID + "'";

        SqlCommand cmd = new SqlCommand(stcCnt, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["counts"]) > 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There may be some stock in CSMS module...'); </script> ");
        }
        else
        {
            //string stcCnt2 = "Select count(Recd_Godown) as counts from MPSCSC.dbo.SCSC_Procurement2016 where Recd_Godown='" + GodownsID + "'";
            string stcCnt2 = "Select count(Recd_Godown) as counts from MPSCSC.dbo.SCSC_Procurement2016 where Recd_Godown='" + GodownsID + "'";

            SqlCommand cmd2 = new SqlCommand(stcCnt2, con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            if (Convert.ToInt32(ds2.Tables[0].Rows[0]["counts"]) > 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There may be some stock in CSMS module...'); </script> ");
            }
            else
            {
                //////
                //string stcCnt2 = "Select count(Recd_Godown) as counts from MPSCSC.dbo.SCSC_Procurement2016 where Recd_Godown='" + GodownsID + "'";
                string stcCnt3 = "Select count(Recd_Godown) as counts from MPSCSC.dbo.SCSC_Procurement_Wheat2017 where Recd_Godown='" + GodownsID + "'";

                SqlCommand cmd3 = new SqlCommand(stcCnt3, con);
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                if (Convert.ToInt32(ds3.Tables[0].Rows[0]["counts"]) > 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('There may be some stock in CSMS module...'); </script> ");
                }

                //////
                else
                {
                    DeleteFlag = "Y";
                }
            }
        }
        return DeleteFlag;
    }
    protected void ddlWeightmentS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWeightmentS.SelectedItem.Text == "Yes")
        {
            ddlWeightmentType.Visible = true;
        }
        else
        {
            ddlWeightmentType.Visible = false;
        }
    }
    protected void PrintMaster(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            Session["GodownID"] = (row.FindControl("hdnGodown_ID") as HiddenField).Value;
            Response.Redirect("~/Masters/PrintGodownMaster.aspx");
        }
    }
}
