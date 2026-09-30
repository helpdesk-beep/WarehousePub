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

public partial class State_DepotProfile : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    SqlCommand cmd = new SqlCommand();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["lang"].ToString() == "Hindi")
        //{
        //    lblDepotMaster.Text = Resources.hindi.lblDepotMaster;
        //    lblState.Text = Resources.hindi.lblState;
        //    lblRegion.Text = Resources.hindi.lblRegion;
        //    lblDistrict.Text = Resources.hindi.lblDistrict;
        //    lblDepotName.Text = Resources.hindi.lblDepotName;
        //    lblDepotBelongs.Text = Resources.hindi.lblDepotBelongs;
        //    lblHiredType.Text = Resources.hindi.lblHiredType;
        //    lblDepotType.Text = Resources.hindi.lblDepotType;
        //    lblDepotDetails.Text = Resources.hindi.lblDepotDetails;
        //    lblTehsil_Block.Text = Resources.hindi.lblTehsil_Block;
        //    lblAddress.Text = Resources.hindi.lblAddress;
        //    lnlPhNo.Text = Resources.hindi.lnlPhNo;
        //    lblFax.Text = Resources.hindi.lblFax;
        //    lblDepotCapacity.Text = Resources.hindi.lblDepotCapacity;
        //    lblEmail.Text = Resources.hindi.lblEmail;
        //    lblRailSliding.Text = Resources.hindi.lblRailSliding;
        //    lblNodalOfficeDetails.Text = Resources.hindi.lblNodalOfficeDetails;
        //    lblNodalOffice.Text = Resources.hindi.lblNodalOffice;
        //    lblAddress1.Text = Resources.hindi.lblAddress;
        //    //lblPhNo1.Text = Resources.hindi.lnlPhNo;
        //    lblMobileNo.Text = Resources.hindi.lblMobileNo;
        //    lblEmail1.Text = Resources.hindi.lblEmail;
        //    lblFax1.Text = Resources.hindi.lblFax;
        //    lblRemarks.Text = Resources.hindi.lblRemarks;
        //    btnupdate.Text = Resources.hindi.btnupdate;
        //}

        //txtTehsilName.Attributes.Add("onkeypress", "return Spc_characteralpha(this);");
        //txtLocationPhoneNo.Attributes.Add("onkeypress", "return CheckOnlyNumeric(event,this);");
        //txtLocationFaxNo.Attributes.Add("onkeypress", "return CheckOnlyNumeric(event,this);");
        //txtDepoCapicity.Attributes.Add("onkeypress", "CheckIsNumeric(event,this);");
        //txtNodalOfficerName.Attributes.Add("onkeypress", "return Spc_characteralpha(this);");
        //txtNodalOfficerPhoneNo.Attributes.Add("onkeypress", "return CheckOnlyNumeric(event,this);");
        //txtNodalOfficerMobileNo.Attributes.Add("onkeypress", "return CheckOnlyNumeric(event,this);");
        //txtNodalOfficerFaxNo.Attributes.Add("onkeypress", "return CheckOnlyNumeric(event,this);");

        if (Session["UserName"] != null)
        {
            string user = Session["UserName"].ToString();
            if (user == "MPSWLC")
            {
                linkEncpass.Visible = true;
            }
            else
            {
                linkEncpass.Visible = false;
            }
        }

        if (!IsPostBack)
        {
            GetState();
            if (Session["UserName"] != null)
            {
                GetRegion();
                if (ddlRegion.SelectedValue != "--Select--")
                {
                    GetDistricts(ddlRegion.SelectedValue);
                }
                string user = Session["UserName"].ToString();
                if (user == "MPSWLC")
                {
                    btnupdate.Text = "Save";
                    linkEncpass.Visible = true;
                }
                else
                {
                    btnupdate.Text = "Update";
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
                    txtLocationName.Text = Session["UserName"].ToString();
                    ddlDepoBeloggsTo.Enabled = false;
                    GetDepotBelongs(Session["BranchId"].ToString());
                }
            }
        }
    }

    private void GetDepotBelongs(string depotId)
    {
        try
        {
            string qry = "Select MDD.*,MDIC.IssueCenterName  from tbl_MetaData_DEPOT as MDD left join MetaDataBranchWithIssueCenter as MDIC on MDD.BranchId=MDIC.BranchID where MDD.BranchId='" + depotId + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepoBeloggsTo.SelectedItem.Text = ds.Tables[0].Rows[0]["DepoBelongs"].ToString();
                txtTehsilName.Text = ds.Tables[0].Rows[0]["TehsilName"].ToString();
                txtLocationAddress.Text = ds.Tables[0].Rows[0]["DepotAddress"].ToString();
                txtLocationPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                txtLocationFaxNo.Text = ds.Tables[0].Rows[0]["FaxNo"].ToString();
                txtLocationEMailAddress.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                txtNodalOfficerName.Text = ds.Tables[0].Rows[0]["NodalOfficeName"].ToString();
                txtNodalOfficerAddress.Text = ds.Tables[0].Rows[0]["NodalOfficeraddress"].ToString();
                txtNodalOfficerPhoneNo.Text = ds.Tables[0].Rows[0]["NodalOfficerphone"].ToString();
                txtNodalOfficerMobileNo.Text = ds.Tables[0].Rows[0]["NodalOfficerMobile"].ToString();
                txtNodalOfficerFaxNo.Text = ds.Tables[0].Rows[0]["NodalOfficerFax"].ToString();
                txtNodalOfficerEmailAddress.Text = ds.Tables[0].Rows[0]["NodalOfficerEmail"].ToString();
                txtlicDate.Text = ds.Tables[0].Rows[0]["LicenceDate"].ToString();
                txtlicNo.Text = ds.Tables[0].Rows[0]["LincenseNo"].ToString();
                oprtnametxt.Text = ds.Tables[0].Rows[0]["OperatorName"].ToString();
                oprtmobiletxt.Text = ds.Tables[0].Rows[0]["OperatorMobileNo"].ToString();
                oprtemailtxt.Text = ds.Tables[0].Rows[0]["OperatorEmailID"].ToString();
                OprtQulddl.SelectedValue = ds.Tables[0].Rows[0]["OperatorQualification"].ToString();
                if (ds.Tables[0].Rows[0]["RailSiding"].ToString().Trim() == "N")
                {
                    ddlRailSiding.SelectedIndex = 2;
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["RailSiding"].ToString().Trim() != "")
                    {
                        ddlRailSiding.SelectedIndex = 1;
                    }
                }
                if (ds.Tables[0].Rows[0]["IntConnectivity"].ToString().Trim() == "N")
                {
                    IntConnctddl.SelectedIndex = 2;
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["IntConnectivity"].ToString().Trim() != "")
                    {
                        IntConnctddl.SelectedIndex = 1;
                        Label11.Visible = true;
                        typeOfIntrConnctddl.Visible = true;
                        typeOfIntrConnctddl.SelectedValue = ds.Tables[0].Rows[0]["IntConnectivity"].ToString().Trim();
                    }
                }

                if (ds.Tables[0].Rows[0]["HardWareAvailability"].ToString().Trim() == "N")
                {
                    HerdAvlddl.SelectedIndex = 2;
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["HardWareAvailability"].ToString().Trim() != "")
                    {
                        HerdAvlddl.SelectedIndex = 1;
                        Label16.Visible = true;
                        YearOfIstallddl.Visible = true;
                        YearOfIstallddl.Text = ds.Tables[0].Rows[0]["HardWareAvailability"].ToString().Trim();
                    }
                }
                if (ds.Tables[0].Rows[0]["ElectronicWeighbridge"].ToString().Trim() == "N")
                {
                    elctWeighbridgeddl.SelectedIndex = 2;
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["ElectronicWeighbridge"].ToString().Trim() != "")
                    {
                        elctWeighbridgeddl.SelectedIndex = 1;
                    }
                }
                PowerSuplyddl.SelectedValue = ds.Tables[0].Rows[0]["PowerSuply"].ToString().Trim();
                //CUGtxt.Text = ds.Tables[0].Rows[0]["BranchCUG_No"].ToString();
                txtDepoCapicity.Text = ds.Tables[0].Rows[0]["DepoCapaty"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                issuecentertxt.Text = ds.Tables[0].Rows[0]["IssueCenterName"].ToString();

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
        try
        {
            string query = "";
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string tehsilname = txtTehsilName.Text.Trim();
            string address = txtLocationAddress.Text.Trim();
            string phoneno = txtLocationPhoneNo.Text.Trim();
            string FaxNo = txtLocationFaxNo.Text.Trim();
            string email = txtLocationEMailAddress.Text.Trim();
            string NodalOfficeName = txtNodalOfficerName.Text.Trim();
            string NodalOfficeraddress = txtNodalOfficerAddress.Text.Trim();
            string NodalOfficerphone = txtNodalOfficerPhoneNo.Text.Trim();
            string NodalOfficerMobile = txtNodalOfficerMobileNo.Text.Trim();
            string NodalOfficerFax = txtNodalOfficerFaxNo.Text.Trim();
            string NodalOfficerEmail = txtNodalOfficerEmailAddress.Text.Trim();
            float depotcapacity = CheckFloat(txtDepoCapicity.Text.Trim());
            string DepoBelongs = ddlDepoBeloggsTo.SelectedItem.Text.Trim();
            string remarks = txtRemarks.Text.Trim();
            string railsiding = ddlRailSiding.SelectedValue.ToString();
            string createddate = getDate_MDY(DateTime.Now.ToString("dd/MM/yyyy"));
            string OperatorName = oprtnametxt.Text.Trim();
            string OperatorMobile = oprtmobiletxt.Text.Trim();
            string OperatorEmail = oprtemailtxt.Text.Trim();
            string OperatorQual = OprtQulddl.SelectedItem.Text.Trim();
            if (OperatorQual == "--Select--")
            {
                OperatorQual = "";
            }
            string ElectWeighbridge = elctWeighbridgeddl.SelectedValue.Trim();
            string IntrConnct = IntConnctddl.SelectedItem.Text.Trim();
            if (IntrConnct != "--Select--")
            {
                if (IntrConnct == "No")
                {
                    IntrConnct = "N";
                }
                else
                {
                    IntrConnct = typeOfIntrConnctddl.SelectedValue.ToString().Trim();
                }
            }
            string PowerSuply = PowerSuplyddl.SelectedValue.Trim();
            //string CUGNo = CUGtxt.Text.Trim();
            string hrdavail = HerdAvlddl.SelectedItem.Text.Trim();
            if (hrdavail != "--Select--")
            {
                if (hrdavail == "No")
                {
                    hrdavail = "N";
                }
                else
                {
                    hrdavail = YearOfIstallddl.SelectedValue.ToString();
                }
            }

            if (btnupdate.Text == "Save")
            {
                SqlTransaction trans = null;
                if (ddlRegion.SelectedItem.Text != "--Select--" && ddlDistrictName.SelectedItem.Text != "" && txtLocationName.Text != "")
                {
                    string distid = ddlDistrictName.SelectedValue.ToString();
                    string stateid = ddlStateName.SelectedValue.ToString();
                    string querymax = "Select max(DepotID) as DepotId from tbl_MetaData_DEPOT where StateId='" + stateid + "' and DistrictId='" + distid + "' ";
                    SqlCommand cmd = new SqlCommand(querymax, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet dsdepot = new DataSet();
                    da.Fill(dsdepot);
                    string depotid = "";
                    if (dsdepot.Tables[0].Rows.Count > 0)
                    {
                        depotid = dsdepot.Tables[0].Rows[0]["DepotId"].ToString();
                    }

                    if (depotid == "")
                    {
                        depotid = distid.ToString() + "001";
                    }
                    else
                    {
                        Int32 id = CheckInt(depotid.ToString()) + 1;
                        depotid = id.ToString();
                    }
                    string depotname = txtLocationName.Text;

                    if (ddlRailSiding.SelectedItem.Text == "Yes")
                    {
                        railsiding = "Y";
                    }
                    else
                    {
                        railsiding = "N";
                    }
                    int scope = 1;
                    string Access_Restrict = "N";
                    query = "Insert into dbo.tbl_MetaData_DEPOT(DepotID,StateId,DistrictId,CategoryID,DepoTypeID,DepotName,TehsilName,DepotAddress,PhoneNo,FaxNo,Email,NodalOfficeName,NodalOfficeraddress,NodalOfficerphone,NodalOfficerMobile,NodalOfficerFax,NodalOfficerEmail,RailSiding,DepoCapaty,DepoBelongs,Remarks,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,DeletedBy,DeletedDate,LincenseNo,LicenceDate) values('" + depotid + "','" + stateid + "','" + distid + "','','','" + depotname + "','" + tehsilname + "','" + address + "','" + phoneno + "','" + FaxNo + "','" + email + "','" + NodalOfficeName + "','" + NodalOfficeraddress + "','" + NodalOfficerphone + "','" + NodalOfficerMobile + "','" + NodalOfficerFax + "','" + NodalOfficerEmail + "','" + railsiding + "'," + depotcapacity + ",'" + DepoBelongs + "','" + remarks + "','" + ip + "','" + createddate + "','','','','',''" + txtlicNo.Text + ",'" + txtlicDate.Text + "') ";

                    string queryloginIns = "Insert into Storage_Login(User_Name,Password,DepotId,DistrictId,Fname,Lname,Scope,Access_Restrict) values('" + depotname + "',convert(varbinary(50),'nic'),'" + depotid + "','" + distid + "','" + depotname + "',''," + scope + ",'" + Access_Restrict + "')";
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
                            cmd.CommandText = queryloginIns;
                            cmd.ExecuteNonQuery();
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully  , Please Save Password For Branch/Depot ......'); </script> ");
                        }

                        trans.Commit();
                        trans.Dispose();
                        string str = "SELECT [User_Name],convert(varchar(20),[login_id])+'nicF$9' as 'pwd',[login_id]  FROM [Storage_Login] where User_Name='" + depotname + "' and  DistrictId='" + distid + "' and DepotId='" + depotid + "' ";
                        SqlDataAdapter da2 = new SqlDataAdapter(str, con);
                        DataSet dsval = new DataSet();
                        da2.Fill(dsval, "temp");
                        if (dsval.Tables[0].Rows.Count > 0)
                        {
                            txtPwd.Value = dsval.Tables[0].Rows[0]["pwd"].ToString();
                            txtUID.Value = dsval.Tables[0].Rows[0]["login_id"].ToString();
                            txtName.Value = dsval.Tables[0].Rows[0]["User_Name"].ToString();
                            lblDepot.Text = dsval.Tables[0].Rows[0]["User_Name"].ToString();
                            pnlPass.Visible = true;
                            btnSavePass.Visible = true;
                            btnupdate.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        lblMsg.Text = ex.Message.ToString();
                    }

                }
            }
            else if (btnupdate.Text == "Update")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string BranchId = Session["BranchID"].ToString();
                string depotid = Session["Depot_DepotID"].ToString();
                string chkcugno = "";
                try
                {
                    chkcugno = NodalOfficerphone.Substring(0, 7);
                }
                catch
                {
                }
                if (txtLocationAddress.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Postal Address Of Branch...')", true);
                }
                else if (tehsilname == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Tehsil/Block Name...')", true);
                }
                else if (NodalOfficerphone.Length != 10)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter 10 Digit CUG mobile No...')", true);
                }
                //else if (chkcugno != "7225018")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter CUG mobile No...')", true);
                //}
                else if (txtLocationEMailAddress.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Email ID...')", true);
                }
                else if (ddlRailSiding.SelectedItem.Text == "--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Rail Siding...')", true);
                }
                else if (IntConnctddl.SelectedItem.Text == "--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Internet Connectivity...')", true);
                }
                else if (IntConnctddl.SelectedItem.Text == "Yes" && typeOfIntrConnctddl.SelectedItem.Text == "--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Type Of Internet Connectivity...')", true);
                }

                else if (elctWeighbridgeddl.SelectedItem.Text == "--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Electronic Weighbridge ...')", true);
                }
                else if (HerdAvlddl.SelectedItem.Text == "--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Hardware Availability...')", true);
                }
                else if (HerdAvlddl.SelectedItem.Text == "Yes" && YearOfIstallddl.SelectedItem.Text == "--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Installation Year...')", true);
                }
                else if (NodalOfficeName == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Branch Manager Name...')", true);
                }
                else if (txtNodalOfficerMobileNo.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Branch Manager Personla Mobile No...')", true);
                }
                else if (txtLocationEMailAddress.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Branch Email Id...')", true);
                }
                else if (oprtnametxt.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Operator Name...')", true);
                }
                else if (oprtmobiletxt.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Operator Mobile No...')", true);
                }
                else if (OprtQulddl.SelectedItem.Text == "--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Operator Qualification ...')", true);
                }

                else
                {

                    cmd = new SqlCommand("[dbo].[Update_Godown_Master]", con, sqltrans);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BranchId", BranchId);
                    cmd.Parameters.AddWithValue("@TehsilName", tehsilname);
                    cmd.Parameters.AddWithValue("@DepotAddress", address);
                    cmd.Parameters.AddWithValue("@PhoneNo", phoneno);
                    cmd.Parameters.AddWithValue("@FaxNo", FaxNo);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@NodalOfficeName", NodalOfficeName);
                    cmd.Parameters.AddWithValue("@NodalOfficeraddress", NodalOfficeraddress);
                    cmd.Parameters.AddWithValue("@NodalOfficerphone", NodalOfficerphone);
                    cmd.Parameters.AddWithValue("@NodalOfficerMobile", NodalOfficerMobile);
                    cmd.Parameters.AddWithValue("@NodalOfficerEmail", NodalOfficerEmail);
                    cmd.Parameters.AddWithValue("@RailSiding", railsiding);
                    cmd.Parameters.AddWithValue("@DepoCapaty", depotcapacity);
                    cmd.Parameters.AddWithValue("@DepoBelongs", DepoBelongs);
                    cmd.Parameters.AddWithValue("@Remarks", remarks);
                    cmd.Parameters.AddWithValue("@UpdatedBy", ip);
                    cmd.Parameters.AddWithValue("@LicenceDate", txtlicDate.Text);
                    cmd.Parameters.AddWithValue("@LincenseNo", txtlicNo.Text);
                    cmd.Parameters.AddWithValue("@OperatorName", OperatorName);
                    cmd.Parameters.AddWithValue("@OperatorMobileNo", OperatorMobile);
                    cmd.Parameters.AddWithValue("@OperatorEmailID", OperatorEmail);
                    cmd.Parameters.AddWithValue("@OperatorQualification", OperatorQual);
                    cmd.Parameters.AddWithValue("@ElectronicWeighbridge", ElectWeighbridge);
                    cmd.Parameters.AddWithValue("@PowerSuply", IntrConnct);
                    cmd.Parameters.AddWithValue("@IntConnectivity", PowerSuply);
                    cmd.Parameters.AddWithValue("@HardWareAvailability", hrdavail);
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                    //if (res > 0)
                    //{
                    //}

                    //string query1 = "Insert into tbl_MetaData_DEPOT_log select * from tbl_MetaData_DEPOT where BranchId='" + BranchId + "' ";
                    //SqlCommand cmd1 = new SqlCommand(query1, con);
                    //int x1 = cmd1.ExecuteNonQuery();
                    //if (x1 > 0)
                    //{
                    //    //query = "Update dbo.tbl_MetaData_DEPOT set TehsilName='" + tehsilname + "',DepotAddress='" + address + "',PhoneNo='" + phoneno + "',FaxNo='" + FaxNo + "',Email='" + email + "',NodalOfficeName='" + NodalOfficeName + "',NodalOfficeraddress='" + NodalOfficeraddress + "',NodalOfficerphone='" + NodalOfficerphone + "',NodalOfficerMobile='" + NodalOfficerFax + "',NodalOfficerEmail='" + NodalOfficerEmail + "',RailSiding='" + railsiding + "',DepoCapaty='" + depotcapacity + "',DepoBelongs='" + DepoBelongs + "',Remarks='" + remarks + "',UpdatedBy='" + ip + "',UpdatedDate='" + createddate + "',LicenceDate='" + txtlicDate.Text + "',LincenseNo='"+txtlicNo.Text+"' where BranchId='" + BranchId + "'";
                    //    query = "Update dbo.tbl_MetaData_DEPOT set TehsilName='" + tehsilname + "',DepotAddress='" + address + "',PhoneNo='" + phoneno + "',FaxNo='" + FaxNo + "',Email='" + email + "',NodalOfficeName='" + NodalOfficeName + "',NodalOfficeraddress='" + NodalOfficeraddress + "',NodalOfficerphone='" + NodalOfficerphone + "',NodalOfficerMobile='" + NodalOfficerMobile + "',NodalOfficerEmail='" + NodalOfficerEmail + "',RailSiding='" + railsiding + "',DepoCapaty='" + depotcapacity + "',DepoBelongs='" + DepoBelongs + "',Remarks='" + remarks + "',UpdatedBy='" + ip + "',UpdatedDate='" + createddate + "',LicenceDate='" + txtlicDate.Text + "',LincenseNo='" + txtlicNo.Text + "' ,OperatorName='" + OperatorName + "',OperatorMobileNo='" + OperatorMobile + "', OperatorEmailID='" + OperatorEmail + "',OperatorQualification='" + OperatorQual + "',ElectronicWeighbridge='" + ElectWeighbridge + "',IntConnectivity='" + IntrConnct + "',PowerSuply='" + PowerSuply + "',HardWareAvailability='" + hrdavail + "' where BranchId='" + BranchId + "' ";
                    //    cmd = new SqlCommand(query, con);
                    //    int x = cmd.ExecuteNonQuery();
                    //    if (x > 0)
                    //    {

                    //        string qry3 = "update tbl_Warehousing_Contact set Mobile_No='" + NodalOfficerphone + "' where Branch_Id='" + BranchId + "'";
                    //        SqlCommand cmd3 = new SqlCommand(qry3, con);
                    //        int a3 = cmd3.ExecuteNonQuery();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Updated Successfully ......'); </script> ");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Not Updated ......'); </script> ");
                    }

                    //txtTehsilName.Text = "";
                    //txtLocationAddress.Text = "";
                    //txtLocationPhoneNo.Text = "";
                    //txtLocationFaxNo.Text = "";
                    //txtLocationEMailAddress.Text = "";
                    //txtNodalOfficerName.Text = "";
                    //txtNodalOfficerAddress.Text = "";
                    //txtNodalOfficerPhoneNo.Text = "";
                    //txtNodalOfficerMobileNo.Text = "";
                    //txtNodalOfficerFaxNo.Text = "";
                    //txtlicNo.Text = "";
                    //txtlicDate.Text = "";
                    //txtNodalOfficerEmailAddress.Text = "";
                    //txtDepoCapicity.Text = "";
                    //txtRemarks.Text = "";
                    //}
                    //        else
                    //{
                    //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Not Updated....'); </script> ");
                    //}
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }
        finally
        {
            con.Close();
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

    protected void IntConnctddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IntConnctddl.SelectedItem.Text == "Yes")
        {
            Label11.Visible = true;
            typeOfIntrConnctddl.Visible = true;
        }
        else
        {
            Label11.Visible = false;
            typeOfIntrConnctddl.Visible = false;
        }
    }
    protected void HerdAvlddl_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (HerdAvlddl.SelectedItem.Text == "Yes")
        {
            Label16.Visible = true;
            YearOfIstallddl.Visible = true;
        }
        else
        {
            Label16.Visible = false;
            YearOfIstallddl.Visible = false;
        }

    }
}
