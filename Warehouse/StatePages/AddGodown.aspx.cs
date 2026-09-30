using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class StatePages_AddGodown : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid = "";
    SqlTransaction sqltran;
    string depottype = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            if (!IsPostBack)
            {
                string PopMsg = "";
                PopMsg = Request.QueryString["PopMsg"];
                if (Request.QueryString["PopMsg"] != null)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + PopMsg + "'); </script> ");
                }
                fillDistrict();
                fillIssuecenter();
                GetGodown(depotid);
                GetCommodity();
                fillGodownType();
                Get_Blocks2();
            }
        }
        else if (Session["RoleId"].ToString() == "10")
        {
            if (!IsPostBack)
            {
                string PopMsg = "";
                PopMsg = Request.QueryString["PopMsg"];
                if (Request.QueryString["PopMsg"] != null)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + PopMsg + "'); </script> ");
                }
                fillDistrict();
                fillIssuecenter();
                GetGodown(depotid);
                GetCommodity();
                fillGodownType();
                Get_Blocks2();
            }

        }
    }
    private void fillDistrict()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

                //if (Session["Region_ID"].ToString() != null)
                //{
                //    region = Session["Region_ID"].ToString();

                //}
            }

            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            }
            else
            {
                if (Session["RoleId"].ToString() == "2")
                {
                    query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
                }
                else
                {
                    query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where District_Id ='" + Session["Depot_DistID"].ToString() + "' order by District_Name asc";

                }
            }
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
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
            string BranchType = ddlbranch.SelectedValue.ToString();

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
    private void fillIssuecenter()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

                //if (Session["Region_ID"].ToString() != null)
                //{
                //    region = Session["Region_ID"].ToString();

                //}
            }

            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "'";
            }
            else
            {
                query = "  SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "' and DepoTypeID='4'";

            }
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
                depottype = ds.Tables[0].Rows[0]["DepoTypeID"].ToString().Trim();
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

    private void fillBranchType()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

                //if (Session["Region_ID"].ToString() != null)
                //{
                //    region = Session["Region_ID"].ToString();

                //}
            }

            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where BranchId='" + ddlbranch.SelectedValue.ToString() + "'";
            }
            else
            {
                query = "SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where BranchId='" + ddlbranch.SelectedValue.ToString() + "'";

            }
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                depottype = ds.Tables[0].Rows[0]["DepoTypeID"].ToString().Trim();

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
    private void GetGodown(string depotid)
    {
        try
        {
            string BranchId = ddlbranch.SelectedValue.ToString();
            string qry = "SELECT  [Godown_ID],[Godown_Name],[Godown_Formation_Date],[Godown_Capacity],[Remarks],[Hired_Type],[Storage_Type],[Godown_Scientific_Capacity],[Godown_APN],[Godown_Email],[Godown_Mobile],[Godown_Address],[BranchID] ,[LicNum] ,[LicDate] FROM [dbo].[tbl_MetaData_GODOWN] where BranchId='" + BranchId + "'  order by tbl_MetaData_GODOWN.Godown_Name";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            // lblRowCount.Text = "Total records are : " + ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["dsGodown"] = ds;
                //fillGrid(ds);
                gvgodowns.DataSource = ds.Tables[0];
                gvgodowns.DataBind();

            }
        }
        catch (Exception)
        {
            //////
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillIssuecenter();
        Get_Blocks2();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {

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
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ddlPBlock.SelectedItem.Text != "--Select--")
        {
            //if (ddllst_hired.SelectedItem.Text != "Steel Silo")
            //{
            string Godown_ID = "";
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string godown = txtGodownName.Text.Trim();
            float capacity = CheckFloat(txtCapacity.Text.Trim());
            float scapacity = CheckFloat(txtScientificCapacity.Text.Trim());
            string hired = ddllst_hired.SelectedValue.Trim();
            string storage = ddllst_storage.SelectedValue.Trim();
            string distid = ddlDistrict.SelectedValue.ToString();
            string depotId = lblIssueCenterId.Text.Trim();
            string BranchId = ddlbranch.SelectedValue.ToString();
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
            //try
            {
                if (con != null)
                {
                    con.Open();
                    // sqltran = con.BeginTransaction();
                    if (btnUpdate.Text == "Insert")
                    {
                        #region Insert Godown Master
                        //qry = "select Max(Godown_ID) from tbl_MetaData_GODOWN where BranchId='" + ddlbranch.SelectedValue.ToString() + "'";
                        qry = "select MAX(RIGHT(Godown_ID,3)) as SGID from tbl_MetaData_GODOWN where BranchId='" + ddlbranch.SelectedValue.ToString() + "'";

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
                            //Godown_ID = (ddlbranch.SelectedValue.ToString() + Convert.ToString(Convert.ToInt64(str3) + 1)).ToString();
                            Godown_ID = (ddlbranch.SelectedValue.ToString() + "0" + AGC).ToString();
                        }
                        else
                        {
                            Godown_ID = ddlbranch.SelectedValue.ToString() + "001";
                        }
                        cmd = new SqlCommand("sp_insertGodownMaster_New", con, sqltran);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Godown_ID", Godown_ID);
                        cmd.Parameters.AddWithValue("@DepotId", depotId);
                        cmd.Parameters.AddWithValue("@DistrictId", ddlDistrict.SelectedValue.ToString());
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
                        cmd.Parameters.AddWithValue("@LicNum", txtlicnum.Text);
                        cmd.Parameters.AddWithValue("@LicDate", getDate_MDY(txtlicdate.Text));
                        cmd.Parameters.AddWithValue("@Latitude", Latitude);
                        cmd.Parameters.AddWithValue("@Longitude", Longitude);
                        cmd.Parameters.AddWithValue("@GodownNum", txtgodownnum.Text);
                        cmd.Parameters.AddWithValue("@khashranum", khashra);
                        cmd.Parameters.AddWithValue("@Rakwanum", rakwa);
                        cmd.Parameters.AddWithValue("@TehshilID", tehshil);
                        cmd.Parameters.AddWithValue("@Village", village);
                        cmd.Parameters.AddWithValue("@WeightmentType", WeightmentType);
                        int res = cmd.ExecuteNonQuery();
                        if (res > 0)
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


                                    //csms_godown_new.csms_godown_webser newgodwn = new csms_godown_new.csms_godown_webser();
                                    //newgodwn.InsertRecord(Godown_ID, "23", distid, depotId, godown, "01/01/2016", "01/01/2016", capacity.ToString(), "Y", hired.ToString(), storage.ToString(), scapacity.ToString(), BranchId);


                                }
                            }
                            catch (Exception)
                            {
                                ////
                            }
                            //  btn_Close.Visible = true;
                            // btnaddnew.Visible = true;
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record saved Successfully..'); </script> ");
                            //lblMsg.Visible = false;
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Not Saved..'); </script> ");
                        }

                        #endregion
                    }
                    //update godown details
                    else if (btnUpdate.Text == "Update")
                    {
                        string godownid = gvgodowns.SelectedRow.Cells[2].Text.Trim();
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
                                cmd = new SqlCommand("sp_godownupdate_New", con, sqltran);
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
                                cmd.Parameters.AddWithValue("@LicNum", txtlicnum.Text);
                                cmd.Parameters.AddWithValue("@LicDate", getDate_MDY(txtlicdate.Text));
                                cmd.Parameters.AddWithValue("@Latitude", Latitude);
                                cmd.Parameters.AddWithValue("@Longitude", Longitude);
                                cmd.Parameters.AddWithValue("@GodownNum", txtgodownnum.Text);
                                cmd.Parameters.AddWithValue("@khashranum", khashra);
                                cmd.Parameters.AddWithValue("@Rakwanum", rakwa);
                                cmd.Parameters.AddWithValue("@TehshilID", tehshil);
                                cmd.Parameters.AddWithValue("@Village", village);
                                cmd.Parameters.AddWithValue("@WeightmentType", WeightmentType);
                                int res = cmd.ExecuteNonQuery();
                                ////update godown name in Pvt_Warehouse_Login
                                string queryp = "select * from Pvt_Warehouse_Login where Godown_Id='" + godownid + "'";
                                cmd = new SqlCommand(queryp, con, sqltran);
                                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                                DataTable dsp = new DataTable();
                                dap.Fill(dsp);
                                if (dsp.Rows.Count > 0)
                                {
                                    string qrym = "update Pvt_Warehouse_Login set Godown_Name='" + godown + "' where Godown_Id='" + godownid + "'";
                                    SqlCommand cmdm = new SqlCommand(qrym, con, sqltran);
                                    //con.Open();
                                    cmdm.ExecuteNonQuery();
                                    //con.Close();
                                }

                                ////Close update godown name in Pvt_Warehouse_Login

                                if (res > 0)
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
                                            //Godown.CSMS_Service_For_Godown_Entry newgodown = new Godown.CSMS_Service_For_Godown_Entry();
                                            //string web_url = "http://mpsc.mp.nic.in/csms/CSMS_Web_Service/CSMS_Godown.asmx";
                                            //// string web_url = "http://10.131.5.125/csms/CSMS_Web_Service/CSMS_Godown.asmx";
                                            //string Pass = "csms@godown";
                                            //string User = "csms";
                                            //newgodown.Url = web_url;
                                            //newgodown.Insert_Godown_Info(ds, Pass, User);

                                            //csms_godown_new.csms_godown_webser newgodwn = new csms_godown_new.csms_godown_webser();
                                            //newgodwn.UpdateRecord(godownid, "23", distid, depotId, godown, "01/01/2016", "01/01/2016", capacity.ToString(), "Y", hired.ToString(), storage.ToString(), scapacity.ToString(), BranchId);

                                        }
                                    }
                                    catch (Exception)
                                    {
                                        ////
                                    }

                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record updated successfully..'); </script> ");
                                    //   lblMsg.Visible = false;

                                }
                            }

                            #endregion
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Godown Capacity can not be less than Total stack Capacity'); </script> ");
                            //   lblMsg.Visible = false;
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
                    //  PanelGodown.Visible = false;
                    //  btn_Close.Visible = true;
                    //  btnaddnew.Visible = true;
                    //sqltran.Commit();
                    GetGodown(depotId);
                }
            }
            //catch (Exception ex)
            {
                //  sqltran.Rollback();
                //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                // lblMsg.Text = "some error has been occured please try again later";
            }
            //   finally
            {
                // sqltran.Dispose();
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
            //    string distid = ddlDistrict.SelectedValue.ToString();
            //    string depotId = ddlbranch.SelectedValue.ToString();
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
            //                cmd.Parameters.AddWithValue("@LicDate", txtlicdate.Text);
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
            //                        cmd.Parameters.AddWithValue("@licdate", txtlicdate.Text.Trim());
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
            //                string godownid = gvgodowns.SelectedRow.Cells[9].Text.Trim();
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
            //                        cmd.Parameters.AddWithValue("@LicDate", txtlicdate.Text);
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
            //                                    newgodwn.UpdateRecord(godownid, "23", distid, depotId, godown, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString(), capacity.ToString(), "Y", hired.ToString(), storage.ToString(), scapacity.ToString());

            //                                }
            //                            }
            //                            catch (Exception)
            //                            {
            //                                ////
            //                            }

            //                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record updated successfully..'); </script> ");
            //                            //  lblMsg.Visible = false;

            //                        }
            //                    }

            //                    #endregion
            //                }
            //                else
            //                {
            //                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Godown Capacity can not be less than Total stack Capacity'); </script> ");
            //                    //  lblMsg.Visible = false;
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
            //    }
            //    finally
            //    {
            //        sqltran.Dispose();
            //        con.Close();
            //    }

            //}
        }
        else
        {
            //lblMsg.Text = "आपने खण्ड का चयन नहीं किया है";
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आपने खण्ड का चयन नहीं किया है'); </script> ");
        }

    }
    protected void btnCan_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StatePages/AddGodown.aspx");
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodown(ddlbranch.SelectedValue.ToString());
        fillBranchType();
        fillGodownType();
        GetIssueCenterId();
    }

    private void GetCommodity()
    {
        try
        {
            qry = "select * from dbo.tbl_MetaData_STORAGE_COMMODITY";
            cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dprlst_Commodity.DataSource = ds.Tables[0];
                dprlst_Commodity.DataValueField = "Commodity_Id";
                dprlst_Commodity.DataTextField = "Commodity_Name";
                dprlst_Commodity.DataBind();
                dprlst_Commodity.SelectedValue = "22";
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = ex.Message.ToString();
        }
    }

    protected void F_addstack()
    {
        string stack_id = "";
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string godownid = "";
        string capacity = txtCapacity.Text.Trim();
        string stackname = "01";
        string hired = ddllst_hired.SelectedItem.Text;
        string storage = ddllst_storage.SelectedItem.Text;
        //string commodity = dprlst_Commodity.SelectedValue.ToString();
        string distid = ddlDistrict.SelectedValue.ToString();
        string depotId = ddlbranch.SelectedValue.ToString(); ;
        decimal Godowncapacity = 0;
        decimal Sumofstackcap = 0;
        decimal Allowstackcap = 0;
        con.Open();
        sqltran = con.BeginTransaction();
        //qry = "IF EXISTS (select * from tbl_metadata_stack where Godown_ID='" + godownid + "')BEGIN select convert(decimal(18,2),GD.Godown_Capacity) as Godown_Capacity,ISNULL(convert(decimal(18,2),sum(ST.Stack_capacity)),0) as Stack_capacity from tbl_MetaData_GODOWN as GD LEFT JOIN tbl_MetaData_STACK as ST ON GD.Godown_ID = ST.Godown_ID where gd.Godown_ID='" + godownid + "' and gd.DepotId='" + depotId + "' and Stack_Killed='N' group by GD.Godown_Capacity,GD.Godown_ID END ELSE BEGIN select convert(decimal(18,2),GD.Godown_Capacity) as Godown_Capacity,ISNULL(convert(decimal(18,2),sum(ST.Stack_capacity)),0) as Stack_capacity from tbl_MetaData_GODOWN as GD LEFT JOIN tbl_MetaData_STACK as ST ON GD.Godown_ID = ST.Godown_ID where gd.Godown_ID='" + godownid + "' and gd.DepotId='" + depotId + "'  group by GD.Godown_Capacity,GD.Godown_ID END";
        qry = "select convert(decimal(18,2),GD.Godown_Capacity) as Godown_Capacity ,(select ISNULL(convert(decimal(18,2),sum(ST.Stack_capacity)),0) as Stack_capacity from dbo.tbl_MetaData_STACK as ST where Godown_ID='" + godownid + "'  and Stack_Killed='N' and ST.DepotId='" + depotId + "') as Stack_capacity from tbl_MetaData_GODOWN as GD where Godown_ID='" + godownid + "' and gd.DepotId='" + depotId + "'";
        cmd = new SqlCommand(qry, con, sqltran);
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Godowncapacity = Convert.ToDecimal(ds.Tables[0].Rows[0]["Godown_Capacity"].ToString());
            Sumofstackcap = Convert.ToDecimal(ds.Tables[0].Rows[0]["Stack_capacity"].ToString());
        }
        Allowstackcap = Godowncapacity - Sumofstackcap;
        if (btnUpdate.Text == "Insert")
        {
            if (Allowstackcap >= Convert.ToDecimal(capacity))
            {

                qry = "select isnull(Max(Stack_ID),0) from tbl_MetaData_STACK where DepotId='" + depotId + "' and Godown_ID='" + godownid + "' ";
                cmd = new SqlCommand(qry, con, sqltran); // check WhrId present in whr_status table
                string str3 = cmd.ExecuteScalar().ToString();
                if (Convert.ToInt64(str3) != 0)
                {
                    stack_id = Convert.ToString(Convert.ToInt64(str3) + 1);
                    //lbl_stackid.Text = stack_id;
                }
                else
                {
                    stack_id = godownid + "0001";
                    // lbl_stackid.Text = stack_id;
                }

                cmd = new SqlCommand("sp_insertStackMaster_New", con, sqltran);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Stack_ID", stack_id);
                cmd.Parameters.AddWithValue("@DepotId", depotId);
                cmd.Parameters.AddWithValue("@District_Id", distid);
                cmd.Parameters.AddWithValue("@Godown_ID", godownid);
                cmd.Parameters.AddWithValue("@Stack_Name", stackname);
                cmd.Parameters.AddWithValue("@Commodity_Id", dprlst_Commodity.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Category_Id", "1");
                if (capacity != "")
                {
                    cmd.Parameters.AddWithValue("@Stack_capacity", Convert.ToDecimal(capacity));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Stack_capacity", 0);
                }


                cmd.Parameters.AddWithValue("@Parant_Stack_ID", DBNull.Value);
                cmd.Parameters.AddWithValue("@Remarks", DBNull.Value);


                cmd.Parameters.AddWithValue("@Storage_Type", ddllst_storage.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Hired_type", ddllst_hired.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@CreatedBy", ip);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record saved Successfully..')", true);


                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Saved,Stack Name Already Exits..')", true);
                }
                sqltran.Commit();


            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Can Not Insert Capacity More Than Gowdown Capacity ,The Maximum allowed capacity is =" + Allowstackcap.ToString() + "')", true);
            }
        }
    }
    protected void gvgodowns_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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
    protected void gvgodowns_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string depotId = ddlbranch.SelectedValue.ToString();
            string distid = ddlDistrict.SelectedValue.ToString();
            int _rowindex = e.RowIndex;
            //string gid = godown_GridView.DataKeys[_rowindex].Value.ToString();
            string gid = gvgodowns.DataKeys[_rowindex].Value.ToString();
            //string gid = gvgodowns.SelectedRow.Cells[2].Text.ToString();
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
                con.Open();
                string qrylog = "insert into tbl_MetaData_GODOWN_log select [Godown_ID],[StateId],[DistrictId],[DepotId],[Godown_Name],[Godown_Formation_Date],[Godown_Updation_Date],[Godown_Capacity],[Remarks],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "' as [DeletedBy],Getdate() as [DeletedDate],[Hired_Type],[Storage_Type],[Godown_Scientific_Capacity],[Godown_APN],[Godown_Email],[Godown_Mobile],[Godown_Address],[BranchID],[LicNum],[LicDate],[PAN],[Bank_ID],[AccNo],[IFSC_Code],[Bank_Add],[Latitude],[Longitude],[GodownNum],[Khasranum],[Rakwanum],[TehshilID],[VillageName],[Org_Name],[GInchargeName],[GInchargeAddress],[GInchargeMobile],[GInchargeEmail],[WeightmentType],[LicIssueDate],[Godown_Reg_No],[IsActive],[GodcapMT] from tbl_MetaData_GODOWN where  Godown_ID='" + gid + "'";
                cmd.Connection = con;
                cmd.CommandText = qrylog;
                int state = cmd.ExecuteNonQuery();
                if (state > 0)
                {
                    string queryupd = "update  tbl_MetaData_GODOWN set Remarks='N',[DeletedBy]='" + ip + "' , DeletedDate=Getdate() where Godown_ID='" + gid + "'";
                    cmd.Connection = con;
                    cmd.CommandText = queryupd;
                    int stateupd = cmd.ExecuteNonQuery();
                    if (stateupd > 0)
                    {
                        //Godown.CSMS_Service_For_Godown_Entry newgodown = new Godown.CSMS_Service_For_Godown_Entry();
                        //string web_url = "http://mpsc.mp.nic.in/csms/CSMS_Web_Service/CSMS_Godown.asmx";
                        //string Pass = "csms@godown";
                        //string User = "csms";
                        //newgodown.Url = web_url;
                        //newgodown.Delete_Godown(depotId, gid, Pass, User);
                        //csms_godown_new.csms_godown_webser newgodwn = new csms_godown_new.csms_godown_webser();
                        // newgodwn.DeleteRecord(gid, distid);
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data deleted Successfully......'); </script> ");
                    }
                    GetGodown(depotId);
                    btnUpdate.Text = "";
                    //   PanelGodown.Visible = false;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
                }
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
    }
    protected void gvgodowns_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnUpdate.Text = "Update";
        gvgodowns.Visible = true;
        string gid = gvgodowns.SelectedRow.Cells[2].Text;
        if (gid != "")
        {
            string str = "  SELECT [Godown_ID],[Godown_APN],[Godown_Email],Latitude,Longitude,[Godown_Mobile],[Khasranum],[TehshilID],[VillageName],[Rakwanum],[Godown_Address],LicNum,convert (nvarchar(20),LicDate,103) as LicDate,GodownNum FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] WHERE Godown_ID='" + gid + "'";
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
                    ddlVillage.SelectedValue = ds.Tables[0].Rows[0]["VillageName"].ToString();
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

            txtGodownName.Text = gvgodowns.SelectedRow.Cells[3].Text.Trim();
            Session["GodownName"] = gvgodowns.SelectedRow.Cells[3].Text.Trim();
            txtCapacity.Text = gvgodowns.SelectedRow.Cells[5].Text.Trim();
            if (gvgodowns.SelectedRow.Cells[9].Text.Trim() == "" || gvgodowns.SelectedRow.Cells[9].Text.Trim() == "&nbsp;")
            {
                txtScientificCapacity.Text = "";
            }
            else
            {
                txtScientificCapacity.Text = gvgodowns.SelectedRow.Cells[9].Text.Trim();
            }

            string hired = gvgodowns.SelectedRow.Cells[7].Text.Trim();
            ddllst_hired.SelectedItem.Selected = false;
            foreach (ListItem lst1 in ddllst_hired.Items)
            {
                if (lst1.Value == hired)
                {
                    lst1.Selected = true;
                }
            }
            string storage = gvgodowns.SelectedRow.Cells[8].Text.Trim();
            ddllst_storage.SelectedItem.Selected = false;
            foreach (ListItem lst1 in ddllst_storage.Items)
            {
                if (lst1.Value == storage)
                {
                    lst1.Selected = true;
                }
            }
            //    btn_Close.Visible = false;
            //    btnaddnew.Visible = false;
        }
    }

    public void Get_Blocks2()
    {
        string DistrictId = ddlDistrict.SelectedValue.ToString();
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

    protected void ddlPBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_Village();
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
    private void GetIssueCenterId()
    {
        try
        {
            string BranchId = ddlbranch.SelectedValue.ToString();
            string qry = "select User_Name,DepotId from Storage_Login where BranchID='" + BranchId + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblIssueCenterId.Text = ds.Tables[0].Rows[0]["DepotId"].ToString();
            }
        }
        catch (Exception)
        {
            //////
        }
    }
}