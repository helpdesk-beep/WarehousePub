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

public partial class WarehouseLevel_PvtGodownProfile : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    DataTable dt = new DataTable();
    public string LicDate = "";
    //public string WManagerId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lang"].ToString() == "Hindi")
        {
            lblDepotMaster.Text = Resources.hindi.lblDepotMaster;
            lblState.Text = Resources.hindi.lblState;
            lblRegion.Text = Resources.hindi.lblRegion;
            lblDistrict.Text = Resources.hindi.lblDistrict;
            lblDepotName.Text = Resources.hindi.lblDepotName;
            //lblDepotBelongs.Text = Resources.hindi.lblDepotBelongs;
            lblHiredType.Text = Resources.hindi.lblHiredType;
            //lblDepotType.Text = Resources.hindi.lblDepotType;
            lblDepotDetails.Text = Resources.hindi.lblDepotDetails;
            //lblTehsil_Block.Text = Resources.hindi.lblTehsil_Block;
            lblAddress.Text = Resources.hindi.lblAddress;
            lnlPhNo.Text = Resources.hindi.lnlPhNo;
            //lblFax.Text = Resources.hindi.lblFax;
            lblDepotCapacity.Text = Resources.hindi.lblDepotCapacity;
            lblEmail.Text = Resources.hindi.lblEmail;
           // lblRailSliding.Text = Resources.hindi.lblRailSliding;
            lblNodalOfficeDetails.Text = Resources.hindi.lblNodalOfficeDetails;
            lblNodalOffice.Text = Resources.hindi.lblNodalOffice;
            lblAddress1.Text = Resources.hindi.lblAddress;
            //lblPhNo1.Text = Resources.hindi.lnlPhNo;
            lblMobileNo.Text = Resources.hindi.lblMobileNo;
            lblEmail1.Text = Resources.hindi.lblEmail;
            //lblFax1.Text = Resources.hindi.lblFax;
            //lblRemarks.Text = Resources.hindi.lblRemarks;
            //btnupdate.Text = Resources.hindi.btnupdate;
        }

        //txtTehsilName.Attributes.Add("onkeypress", "return Spc_characteralpha(this);");
        txtLocationPhoneNo.Attributes.Add("onkeypress", "return CheckOnlyNumeric(event,this);");
        //txtLocationFaxNo.Attributes.Add("onkeypress", "return CheckOnlyNumeric(event,this);");
        txtDepoCapicity.Attributes.Add("onkeypress", "CheckIsNumeric(event,this);");
        txtNodalOfficerName.Attributes.Add("onkeypress", "return Spc_characteralpha(this);");
        //txtNodalOfficerPhoneNo.Attributes.Add("onkeypress", "return CheckOnlyNumeric(event,this);");
        txtNodalOfficerMobileNo.Attributes.Add("onkeypress", "return CheckOnlyNumeric(event,this);");
       // txtNodalOfficerFaxNo.Attributes.Add("onkeypress", "return CheckOnlyNumeric(event,this);");

        if (Session["UserName"] != null && Session["GodownID_New"]!=null)
        {
            string user = Session["UserName"].ToString();
        }

        if (!IsPostBack)
        {
            GetWManager();
            GetState();
            GetBranch();
            if (Session["UserName"] != null)
            {
                GetRegion();
                if (ddlRegion.SelectedValue != "--Select--")
                {
                    GetDistricts(ddlRegion.SelectedValue);
                }
                string user = Session["UserName"].ToString();
               
                    ddlStateName.Enabled = false;
                    ddlStateName.SelectedIndex = 0;
                    ddlRegion.Enabled = false;
                    GetRegionByDistrict(Session["Depot_DistID"].ToString());
                    ddlDistrictName.Enabled = false;
                    GetDistricts(ddlRegion.SelectedValue.ToString());
                    string dist = Session["Depot_DistID"].ToString();
                    foreach (ListItem lst1 in ddlDistrictName.Items)
                    {
                        if (lst1.Value == dist)
                        {
                            lst1.Selected = true;
                        }
                    }

                    txtLocationName.Enabled = false;
                    fillBankList();
                    Get_Blocks2();
                    GetDepotBelongs();
                    
                    
                //}
            }
        }
    }
    private void GetDepotBelongs()
    {
        try
        {
            
            //string qry = "SELECT [tbl_MetaData_PvtW_Manager].[WMId],[StateId],[CategoryID],[DepoTypeID],[DepotName],Org_Name,[TehsilName],[DepotAddress],[PhoneNo],[FaxNo],[Email],[WManagerName],[WManagerAddress],[WManagerPhone],[WManagerMobile],[WManagerFax],[WManagerEmail],[RailSiding],[DepoCapaty],[DepoBelongs],[Remarks],[LincenseNo],convert(varchar(10),[LicenceDate],103) as LicenceDate,[RegionID],[MAId] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_PvtW_Manager] inner join Pvt_Warehouse_Login on Pvt_Warehouse_Login.WMId=[tbl_MetaData_PvtW_Manager].WMId where Pvt_Warehouse_Login.Godown_Id='" + Session["GodownID_New"].ToString() + "'";
            string qry = "select * from tbl_MetaData_GODOWN where Godown_ID='" + Session["GodownID_New"].ToString() + "' and BranchID='" + Session["G_BranchId"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {              
                txtOrg.Text = ds.Tables[0].Rows[0]["Org_Name"].ToString();
                txtAPN.Text = ds.Tables[0].Rows[0]["Godown_APN"].ToString();
                txtLocationAddress.Text = ds.Tables[0].Rows[0]["Godown_Address"].ToString();
                txtlatitude.Text = ds.Tables[0].Rows[0]["Latitude"].ToString();
                txtlongitude.Text = ds.Tables[0].Rows[0]["Longitude"].ToString();
                txtLocationPhoneNo.Text = ds.Tables[0].Rows[0]["Godown_Mobile"].ToString();
                txtLocationEMailAddress.Text = ds.Tables[0].Rows[0]["Godown_Email"].ToString();
                txtDepoCapicity.Text = ds.Tables[0].Rows[0]["Godown_Capacity"].ToString();
                txtGodownNo.Text = ds.Tables[0].Rows[0]["GodownNum"].ToString();
                txtlicNo.Text = ds.Tables[0].Rows[0]["LicNum"].ToString();
                //txtlicDate.Text = ds.Tables[0].Rows[0]["LicDate"].ToString();
                txtlicDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["LicDate"]).ToString("dd/MM/yyyy");
                txtPan.Text = ds.Tables[0].Rows[0]["PAN"].ToString();
                txtBAdd.Text = ds.Tables[0].Rows[0]["Bank_Add"].ToString();
                txtAcc.Text = ds.Tables[0].Rows[0]["AccNo"].ToString();
                txtIfsc.Text = ds.Tables[0].Rows[0]["IFSC_Code"].ToString();
                txtkhasra.Text = ds.Tables[0].Rows[0]["Khasranum"].ToString();
                txtrakwa.Text = ds.Tables[0].Rows[0]["Rakwanum"].ToString();
                txtNodalOfficerName.Text = ds.Tables[0].Rows[0]["GInchargeName"].ToString();
                txtNodalOfficerAddress.Text = ds.Tables[0].Rows[0]["GInchargeAddress"].ToString();
                txtNodalOfficerMobileNo.Text = ds.Tables[0].Rows[0]["GInchargeMobile"].ToString();
                txtNodalOfficerEmailAddress.Text = ds.Tables[0].Rows[0]["GInchargeEmail"].ToString();
                ddlPBlock.SelectedValue = ds.Tables[0].Rows[0]["TehshilID"].ToString();
                ddlBank.SelectedValue = ds.Tables[0].Rows[0]["Bank_ID"].ToString();
                ddlVillage.Items.Add(ds.Tables[0].Rows[0]["VillageName"].ToString());
                

                //btnupdate.Text = "Update";
            }
        }
        catch (Exception)
        {
            /////////////
        }

    }

    private void GetRegionByDistrict(string dist)
    {
        try
        {
            string qry = "Select r.Region_Id,r.region from tbl_MetaData_Region r,tbl_MetaData_DISTRICT d where d.Region_ID=r.Region_Id and d.District_Id='" + dist + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlRegion.DataSource = ds.Tables[0];
                ddlRegion.DataTextField = "region";
                ddlRegion.DataValueField = "Region_Id";
                ddlRegion.DataBind();
                ddlRegion.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    private void GetDistricts(string region)
    {
        try
        {
            string qry = "select * from dbo.tbl_MetaData_DISTRICT where Region_ID='" + region + "' ";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrictName.DataSource = ds.Tables[0];
                ddlDistrictName.DataTextField = "District_Name";
                ddlDistrictName.DataValueField = "District_Id";
                ddlDistrictName.DataBind();
                ddlDistrictName.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    private void GetRegion()
    {
        try
        {
            string qry = "select * from dbo.tbl_MetaData_Region";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlRegion.DataSource = ds.Tables[0];
                ddlRegion.DataTextField = "region";
                ddlRegion.DataValueField = "Region_Id";
                ddlRegion.DataBind();
                ddlRegion.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    private void GetState()
    {
        try
        {
            string qry = "select * from dbo.tbl_MetaData_STATE";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlStateName.DataSource = ds.Tables[0];
                ddlStateName.DataTextField = "State_Name";
                ddlStateName.DataValueField = "State_Id";
                ddlStateName.DataBind();
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegion.SelectedValue != "--Select--")
        {
            GetDistricts(ddlRegion.SelectedValue);
        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (txtOrg.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Organization/Company Name...'); </script> ");
        }
        else if (txtAPN.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Owner Name...'); </script> ");
        }
        else if (txtLocationAddress.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Address...'); </script> ");
        }
        else if (ddlPBlock.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Tehsil...'); </script> ");
        }
        //else if (ddlVillage.SelectedItem.Text == "--Select--" || ddlVillage.SelectedItem.Text == "")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Tehsil...'); </script> ");
        //}
        else if (txtlatitude.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter latitude...'); </script> ");
        }
        else if (txtlongitude.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter longitude...'); </script> ");
        }
        else if (txtLocationPhoneNo.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Mobile No....'); </script> ");
        }
        else if (txtLocationEMailAddress.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Godown E-Mail Address....'); </script> ");
        }
        else if (txtlicNo.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Lincese No:....'); </script> ");
        }
        else if (txtlicDate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Lincese Date....'); </script> ");
        }
        else if (txtPan.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter PAN No....'); </script> ");
        }
        else if (ddlBank.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please select Bank...'); </script> ");
        }
        else if (txtBAdd.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Bank Branch Add.....'); </script> ");
        }
        else if (txtAcc.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Account No....'); </script> ");
        }
        else if (txtIfsc.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter IFSC Code....'); </script> ");
        }
        else if (txtkhasra.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter khasra...'); </script> ");
        }
        else if (txtrakwa.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter rakwa...'); </script> ");
        }
        else if (txtNodalOfficerName.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Godown/Silo Incharge...'); </script> ");
        }
        else if (txtNodalOfficerAddress.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Incharge Address...'); </script> ");
        }
        else if (txtNodalOfficerMobileNo.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Incharge Mobile No....'); </script> ");
        }
        else if (txtNodalOfficerEmailAddress.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Incharge E-Mail Address....'); </script> ");
        }
        else
        {
            UpdateGodown();
        }
    }
    public void UpdateGodown()
    {
        try
        {
            string query = "";
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            //string tehsilname = txtTehsilName.Text.Trim();
            string address = txtLocationAddress.Text.Trim();
            string phoneno = txtLocationPhoneNo.Text.Trim();
            //string FaxNo = txtLocationFaxNo.Text.Trim();
            string email = txtLocationEMailAddress.Text.Trim();
            string NodalOfficeName = txtNodalOfficerName.Text.Trim();
            string NodalOfficeraddress = txtNodalOfficerAddress.Text.Trim();
            // string NodalOfficerphone = txtNodalOfficerPhoneNo.Text.Trim();
            string NodalOfficerMobile = txtNodalOfficerMobileNo.Text.Trim();
            //string NodalOfficerFax = txtNodalOfficerFaxNo.Text.Trim();
            string NodalOfficerEmail = txtNodalOfficerEmailAddress.Text.Trim();
            float depotcapacity = CheckFloat(txtDepoCapicity.Text.Trim());
            //string DepoBelongs = ddlDepoBeloggsTo.SelectedItem.Text.Trim();
            // string remarks = txtRemarks.Text.Trim();
            //string railsiding = "";
            string GBranchId = Session["G_BranchId"].ToString();
            string GTypeId = Session["GodownTypeId"].ToString();
            string createddate = getDate_MDY(DateTime.Now.ToString("dd/MM/yyyy"));
            string OrgName = txtOrg.Text;
            //Org_Name
            if (btnupdate.Text == "Save")
            {
                SqlTransaction trans = null;
                if (ddlRegion.SelectedItem.Text != "--Select--" && ddlDistrictName.SelectedItem.Text != "" && txtLocationName.Text != "")
                {

                    //query = "Insert into dbo.tbl_MetaData_PvtW_Manager(WMId,StateId,DistrictId,CategoryID,DepoTypeID,DepotName,Org_Name,TehsilName,DepotAddress,PhoneNo,FaxNo,Email,WManagerName,WManagerAddress,WManagerPhone,WManagerMobile,WManagerFax,WManagerEmail,RailSiding,DepoCapaty,DepoBelongs,Remarks,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,DeletedBy,DeletedDate,LincenseNo,LicenceDate,BranchId,RegionId,MAId) values('" + WManagerId + "','" + stateid + "','" + distid + "','','" + ddlDepoBeloggsTo.SelectedValue.ToString() + "','" + depotname + "','" + OrgName + "','" + tehsilname + "','" + address + "','" + phoneno + "','" + FaxNo + "','" + email + "','" + NodalOfficeName + "','" + NodalOfficeraddress + "','" + NodalOfficerphone + "','" + NodalOfficerMobile + "','" + NodalOfficerFax + "','" + NodalOfficerEmail + "','" + railsiding + "'," + depotcapacity + ",'" + DepoBelongs + "','" + remarks + "','" + ip + "','" + createddate + "','','','','','" + txtlicNo.Text + "','" + txtlicDate.Text + "','" + Session["G_BranchId"].ToString() + "','" + ddlRegion.SelectedValue.ToString() + "'," + WMAId + ") ";
                    query = "update tbl_MetaData_GODOWN set [Godown_APN]=N'" + txtAPN.Text + "',[Godown_Email]='" + txtLocationEMailAddress.Text + "',[Godown_Mobile]='" + txtLocationPhoneNo.Text + "',[Godown_Address]='" + txtLocationAddress.Text + "',[LicNum]='" + txtlicNo.Text + "',[LicDate]='" + getDate_MDY(txtlicDate.Text) + "',[PAN]='" + txtPan.Text + "',[Bank_ID]='" + ddlBank.SelectedValue.ToString() + "',[AccNo]='" + txtAcc.Text + "',[IFSC_Code]='" + txtIfsc.Text + "',[Bank_Add]='" + txtBAdd.Text + "',[Latitude]='" + txtlatitude.Text + "',[Longitude]='" + txtlongitude.Text + "',[GodownNum]='" + txtGodownNo.Text + "',[Khasranum]='" + txtkhasra.Text + "',[Rakwanum]='" + txtrakwa.Text + "',[TehshilID]='" + ddlPBlock.SelectedValue.ToString() + "',[VillageName]=N'" + ddlVillage.SelectedValue.ToString() + "',[Org_Name]='" + txtOrg.Text + "',[GInchargeName]=N'" + txtNodalOfficerName.Text + "',[GInchargeAddress]=N'" + txtNodalOfficerAddress.Text + "',[GInchargeMobile]='" + txtNodalOfficerMobileNo.Text + "',[GInchargeEmail]='" + txtNodalOfficerEmailAddress.Text + "',[UpdatedBy]='" + ip + "',[UpdatedDate]=getdate() where Godown_ID='" + Session["GodownID_New"].ToString() + "' and BranchID='" + Session["G_BranchId"].ToString() + "'";
                    try
                    {
                        con.Open();
                        trans = con.BeginTransaction();
                        cmd.Transaction = trans;
                        cmd.Connection = con;
                        if (con != null)
                        {
                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();

                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully...'); </script> ");
                        }

                        trans.Commit();
                        trans.Dispose();

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        lblMsg.Text = ex.Message.ToString();
                    }
                    finally
                    {
                        con.Close();
                    }

                }
            }
        }
        catch (Exception)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Try Again..'); </script> ");
        }
        finally
        {
            con.Close();
            btnupdate.Enabled = false;
        }
    }
    Int32 CheckInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        Int32 ValF = int.Parse(ValS);
        return ValF;
    }

    float CheckFloat(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void btnSavePass_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtUID.Value != null && txtUID.Value != "")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string str_upadte = "update Storage_Login set Password=convert(varbinary(300),'" + txtEncrypted.Value.Trim() + "') ,MasterPassword=convert(varbinary(300),'" + txtMEncrypted.Value.Trim() + "') where login_id='" + txtUID.Value.Trim() + "'";
                SqlCommand cmd2 = new SqlCommand(str_upadte, con);
                int ax = cmd2.ExecuteNonQuery();
                if (ax > 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Password Saved Successfully......'); </script> ");
                    pnlPass.Visible = false;
                    btnSavePass.Visible = false;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Password Not Saved'); </script> ");
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

    protected void linkEncpass_Click(object sender, EventArgs e)
    {
        Response.Redirect("DepotmasterPassword.aspx");
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    private void GetBranch()
    {
        try
        {
            string qry = "select DepotName from tbl_MetaData_DEPOT where BranchId='" + Session["G_BranchId"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {             
                txtLocationName.Text = dt.Rows[0]["DepotName"].ToString();
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void GetWManager()
    {
        try
        {
            //string qry = "select GInchargeName,Godown_ID from tbl_MetaData_GODOWN where BranchId='" + Session["G_BranchId"].ToString() + "' and GInchargeName is not null and Hired_Type in ('WDRA','PVT.PEG','SteelSilo')";
            string qry = "select GInchargeName,Godown_ID from tbl_MetaData_GODOWN where BranchId='" + Session["G_BranchId"].ToString() + "' and Godown_ID='" + Session["GodownID_New"].ToString() + "' and GInchargeName is not null";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlWManager.DataSource = ds.Tables[0];
                ddlWManager.DataTextField = "GInchargeName";
                ddlWManager.DataValueField = "Godown_ID";
                ddlWManager.DataBind();
                ddlWManager.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlWManager_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //string qry = "SELECT [tbl_MetaData_PvtW_Manager].[WMId],[StateId],[CategoryID],[DepoTypeID],[DepotName],[TehsilName],[DepotAddress],[PhoneNo],[FaxNo],[Email],[WManagerName],[WManagerAddress],[WManagerPhone],[WManagerMobile],[WManagerFax],[WManagerEmail],[RailSiding],[DepoCapaty],[DepoBelongs],[Remarks],[LincenseNo],[LicenceDate],[RegionID],[MAId] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_PvtW_Manager] inner join Pvt_Warehouse_Login on Pvt_Warehouse_Login.WMId=[tbl_MetaData_PvtW_Manager].WMId where [tbl_MetaData_PvtW_Manager].WMId='" + ddlWManager.SelectedValue.ToString() + "'";
            string qry = " SELECT * from tbl_MetaData_GODOWN where BranchID='" + Session["G_BranchId"].ToString() + "' and Godown_ID='" + ddlWManager.SelectedValue.ToString() + "' and GInchargeName='" + ddlWManager.SelectedItem.Text + "'";

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtOrg.Text = ds.Tables[0].Rows[0]["Org_Name"].ToString();
                txtAPN.Text = ds.Tables[0].Rows[0]["Godown_APN"].ToString();
                txtLocationAddress.Text = ds.Tables[0].Rows[0]["Godown_Address"].ToString();
                txtlatitude.Text = ds.Tables[0].Rows[0]["Latitude"].ToString();
                txtlongitude.Text = ds.Tables[0].Rows[0]["Longitude"].ToString();
                txtLocationPhoneNo.Text = ds.Tables[0].Rows[0]["Godown_Mobile"].ToString();
                txtLocationEMailAddress.Text = ds.Tables[0].Rows[0]["Godown_Email"].ToString();
                txtDepoCapicity.Text = ds.Tables[0].Rows[0]["Godown_Capacity"].ToString();
                txtGodownNo.Text = ds.Tables[0].Rows[0]["GodownNum"].ToString();
                txtlicNo.Text = ds.Tables[0].Rows[0]["LicNum"].ToString();
                //txtlicDate.Text = ds.Tables[0].Rows[0]["LicDate"].ToString();
                txtlicDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["LicDate"]).ToString("dd/MM/yyyy");
                txtPan.Text = ds.Tables[0].Rows[0]["PAN"].ToString();
                txtBAdd.Text = ds.Tables[0].Rows[0]["Bank_Add"].ToString();
                txtAcc.Text = ds.Tables[0].Rows[0]["AccNo"].ToString();
                txtIfsc.Text = ds.Tables[0].Rows[0]["IFSC_Code"].ToString();
                txtkhasra.Text = ds.Tables[0].Rows[0]["Khasranum"].ToString();
                txtrakwa.Text = ds.Tables[0].Rows[0]["Rakwanum"].ToString();
                txtNodalOfficerName.Text = ds.Tables[0].Rows[0]["GInchargeName"].ToString();
                txtNodalOfficerAddress.Text = ds.Tables[0].Rows[0]["GInchargeAddress"].ToString();
                txtNodalOfficerMobileNo.Text = ds.Tables[0].Rows[0]["GInchargeMobile"].ToString();
                txtNodalOfficerEmailAddress.Text = ds.Tables[0].Rows[0]["GInchargeEmail"].ToString();
                ddlPBlock.SelectedValue = ds.Tables[0].Rows[0]["TehshilID"].ToString();
                ddlBank.SelectedValue = ds.Tables[0].Rows[0]["Bank_ID"].ToString();
                ddlVillage.Items.Clear();
                ddlVillage.Items.Add(ds.Tables[0].Rows[0]["VillageName"].ToString());
                
                //btnupdate.Text = "Updates";
            }
        }
        catch (Exception)
        {
            /////////////
        }
    }
    public void Get_Blocks2()
    {
        string DistrictId = ddlDistrictName.SelectedValue.ToString();
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
    protected void ddlPBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_Village();
    }
    public void Get_Village()
    {
        //  string DistrictId = ddlDistrict.SelectedValue.ToString();
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
    protected void fillBankList()
    {
        ddlBank.Items.Add(new ListItem("--Select--", "0"));
        ddlBank.Items.Add(new ListItem("Allahabad Bank", "1"));
        ddlBank.Items.Add(new ListItem("Andhra Bank", "2"));
        ddlBank.Items.Add(new ListItem("Axis bank", "3"));
        ddlBank.Items.Add(new ListItem("Bandhan Bank", "16"));
        ddlBank.Items.Add(new ListItem("Bank of Baroda", "4"));
        ddlBank.Items.Add(new ListItem("Bank of India", "5"));
        ddlBank.Items.Add(new ListItem("Bank of Maharashtra", "6"));
        ddlBank.Items.Add(new ListItem("Canara Bank", "7"));
        ddlBank.Items.Add(new ListItem("Central Bank of India", "8"));
        ddlBank.Items.Add(new ListItem("Central Madhya Pradesh Gramin Bank", "38"));
        ddlBank.Items.Add(new ListItem("City Union Bank", "17"));
        ddlBank.Items.Add(new ListItem("Corporation Bank", "18"));
        ddlBank.Items.Add(new ListItem("DCB Bank", "19"));
        ddlBank.Items.Add(new ListItem("Dena Bank", "9"));
        ddlBank.Items.Add(new ListItem("Dhanlaxmi Bank", "10"));
        ddlBank.Items.Add(new ListItem("Federal Bank", "20"));
        ddlBank.Items.Add(new ListItem("HDFC Bank", "11"));
        ddlBank.Items.Add(new ListItem("ICICI Bank", "21"));
        ddlBank.Items.Add(new ListItem("IDBI Bank", "12"));
        ddlBank.Items.Add(new ListItem("IDFC Bank", "22"));
        ddlBank.Items.Add(new ListItem("Indian Bank", "23"));
        ddlBank.Items.Add(new ListItem("Indian Overseas Bank", "24"));
        ddlBank.Items.Add(new ListItem("IndusInd Bank", "25"));
        ddlBank.Items.Add(new ListItem("Karnataka Bank", "26"));
        ddlBank.Items.Add(new ListItem("Karur Vysya Bank", "27"));
        ddlBank.Items.Add(new ListItem("Kotak Mahindra Bank", "28"));
        ddlBank.Items.Add(new ListItem("Oriental Bank of Commerce", "13"));
        ddlBank.Items.Add(new ListItem("Punjab & Sindh Bank", "29"));
        ddlBank.Items.Add(new ListItem("Punjab National Bank", "14"));
        ddlBank.Items.Add(new ListItem("RBL Bank", "30"));
        ddlBank.Items.Add(new ListItem("South Indian Bank", "31"));
        ddlBank.Items.Add(new ListItem("State Bank of India", "15"));
        ddlBank.Items.Add(new ListItem("Syndicate Bank", "32"));
        ddlBank.Items.Add(new ListItem("UCO Bank", "33"));
        ddlBank.Items.Add(new ListItem("Union Bank of India", "34"));
        ddlBank.Items.Add(new ListItem("United Bank of India", "35"));
        ddlBank.Items.Add(new ListItem("Vijaya Bank", "36"));
        ddlBank.Items.Add(new ListItem("Yes Bank", "37"));
        ddlBank.Items.Add(new ListItem("Other Bank", "99"));
        ddlBank.SelectedIndex = 0;
    }
}
