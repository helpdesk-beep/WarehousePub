using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JointVentureScheme_ChangeBranch : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection conWlc = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltran;
    public string qry = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        string SessRegion = Session["UserName"].ToString();
        string SessRegionid = Session["UserId"].ToString();
        if (SessRegion != "" && SessRegionid != "")
        {
            if (!IsPostBack)
            {
                lbluser.Text = SessRegion;
                BindddlDistrict(SessRegionid);

                if (Request.QueryString["SP"] != null)
                {
                    btnSPRun.Visible = true;
                }
            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text != "" && txtSearch.Text.Length > 4)
        {
            Search();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Registration ID .....')", true);
        }
    }
    public void Search()
    {
        qry = "select d.District_Name,t.Tehsil_Name,(select distinct Block_Name from tbl_Branch_Block_Mapping where Block_ID=w.W_Block)  as Block_Name,b.DepotName,w.Registration_Id,w.Warehouse_Name,p.Auth_Person,w.Mobile_No,w.Warehouse_Address,Convert(varchar(10),w.Registration_Date,103) as Registration_Date,d.District_Id,b.BranchId,t.TehsilCode,w.W_Block from tbl_WarehouseRegistration w join Intergrated_MP_STORAGE.dbo.tbl_MetaData_DISTRICT d on w.DistrictId=d.District_Id join Intergrated_MP_STORAGE.dbo.tbl_MetaData_DEPOT b on b.BranchId = w.BranchId join Tehsils t on t.TehsilCode = w.TehsilID join tbl_Warehouse_PreReg p on p.Reg_No = w.Registration_Id where Registration_Id = '" + txtSearch.Text + "' and d.District_Id='" + ddl_session.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvGodown.DataSource = ds.Tables[0];
            gvGodown.DataBind();
            gvGodown.Columns[11].Visible = false;
            gvGodown.Columns[12].Visible = false;
            gvGodown.Columns[13].Visible = false;
            gvGodown.Columns[14].Visible = false;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found Please Enter Correct Registration No.')", true);
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindddlTehsil();
        Bindddlblock();
    }
    public void BindddlDistrict(string RID)
    {
        string str = "Select District_Id,District_Name from tbl_MetaData_DISTRICT where Region_ID='" + RID + "' order by District_Name ";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDistrict.DataSource = ds.Tables[0];
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_Id";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, "---Select---");

            ddl_session.DataSource = ds.Tables[0];
            ddl_session.DataTextField = "District_Name";
            ddl_session.DataValueField = "District_Id";
            ddl_session.DataBind();
            ddl_session.Items.Insert(0, "---Select---");
        }
        else
        {
            ddl_session.Items.Clear();
            ddl_session.Items.Insert(0, "---Select---");

            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, "---Select---");
        }
    }

    public void Bindddlblock()
    {
        string str = "select distinct Block_Name,Block_ID from tbl_Branch_Block_Mapping where District_ID='" + ddlDistrict.SelectedValue.ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlblocknew.DataSource = ds.Tables[0];
            ddlblocknew.DataTextField = "Block_Name";
            ddlblocknew.DataValueField = "Block_ID";
            ddlblocknew.DataBind();
            ddlblocknew.Items.Insert(0, "---Select---");
        }
        else
        {
            ddlblocknew.Items.Clear();
            ddlblocknew.Items.Insert(0, "---Select---");
        }
    }

    public void BindddlBranch()
    {
        string str = "SELECT mbi.BranchID,mbi.BranchName FROM Intergrated_MP_STORAGE.dbo.MetaDataBranchWithIssueCenter as mbi WHERE mbi.BranchID in (select [Branch_ID] from tbl_Branch_Block_Mapping where [Block_ID]='" + ddlblocknew.SelectedValue.ToString().Trim() + "')";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
       // SqlDataAdapter da = new SqlDataAdapter(str, conWlc);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchID";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "---Select---");
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Insert(0, "---Select---");
        }
    }
    public void BindddlTehsil()
    {
        string str = "Select TehsilCode,Tehsil_Name,District_Code from Tehsils where District_Code = '" + ddlDistrict.SelectedValue + "'";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlTehsil.DataSource = ds.Tables[0];
            ddlTehsil.DataTextField = "Tehsil_Name";
            ddlTehsil.DataValueField = "TehsilCode";
            ddlTehsil.DataBind();
            ddlTehsil.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlTehsil.Items.Clear();
            ddlTehsil.Items.Insert(0, "---Select---");
        }
    }

    protected void gvGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = gvGodown.SelectedRow;
        ddlDistrict.SelectedValue = gvr.Cells[11].Text;
        Session["UPDTREGNO"] = gvr.Cells[5].Text.Trim();
        ddlDistrict_SelectedIndexChanged(null, null);
        if (gvGodown.SelectedRow != null)
        {
            gv.Visible = true;
            btnUpdate.Visible = true;
        }
        else
        {
            gv.Visible = false;
            btnUpdate.Visible = false;
        }
    }
    protected void ddlblocknew_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindddlBranch();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string Client_Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        sqltran = con.BeginTransaction();
        if (txtSearch.Text != "" && txtSearch.Text.Length > 4 )
        {
            try
            {
                if (txtSearch.Text=="")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Registeration ID District...'); </script> ");
                }
                else if (ddlblocknew.SelectedItem.Text.Trim() == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Block...'); </script> ");
                }
                else if (ddlTehsil.SelectedItem.Text.Trim() == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Tehsil...'); </script> ");
                }
                else if (ddlBranch.SelectedItem.Text.Trim() == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Branch...'); </script> ");
                }
                else if (ddlDistrict.SelectedItem.Text.Trim() == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select District...'); </script> ");
                }
                else if (txtSearch.Text.Trim() != Session["UPDTREGNO"].ToString().Trim())
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registration ID Not Match ...'); </script> ");
                }
                else
                {
                    string qryInsert = "insert into tbl_WarehouseRegistration_Log select * from tbl_WarehouseRegistration where Registration_Id = '" + txtSearch.Text + "' ";
                    SqlCommand cmd = new SqlCommand(qryInsert, con, sqltran);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        string qryUpdate = "Update tbl_WarehouseRegistration set DistrictId='" + ddlDistrict.SelectedValue + "',TehsilID = '" + ddlTehsil.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate(), W_Block='" + ddlblocknew.SelectedValue.ToString() + "' where Registration_Id = '" + txtSearch.Text + "'";
                        SqlCommand cmd2 = new SqlCommand(qryUpdate, con, sqltran);
                        int j = cmd2.ExecuteNonQuery();
                        if (j == 1)
                        {
                            string qryInsert1 = "insert into tbl_WarehouseGodown_Reg_Log select * from tbl_WarehouseGodown_Reg where Registration_Id = '" + txtSearch.Text + "' ";
                            SqlCommand cmd3 = new SqlCommand(qryInsert1, con, sqltran);
                            int k = cmd3.ExecuteNonQuery();
                            if (k > 0)
                            {
                                string qryUpdate1 = "update tbl_WarehouseGodown_Reg set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";
                                SqlCommand cmd4 = new SqlCommand(qryUpdate1, con, sqltran);
                                int L = cmd4.ExecuteNonQuery();
                                if (L > 0)
                                {
                                    //string qryInsert2 = "insert into tbl_Warehouse_Capacity_Offer_2019_log select * from tbl_Warehouse_Capacity_Offer_2019 where Registration_Id = '" + txtSearch.Text + "' ";
                                    //string qryInsert2 = "insert into tbl_Warehouse_Capacity_Offer_2020_log select * from tbl_Warehouse_Capacity_Offer_2020 where Registration_Id = '" + txtSearch.Text + "' ";
                                    //string qryInsert2 = "insert into tbl_Warehouse_Capacity_Offer_2021_log select * from tbl_Warehouse_Capacity_Offer_2021 where Registration_Id = '" + txtSearch.Text + "' ";
                                    //string qryInsert2 = "insert into tbl_Warehouse_Capacity_Offer_2022_log select * from tbl_Warehouse_Capacity_Offer_2022 where Registration_Id = '" + txtSearch.Text + "' ";
                                    //string qryInsert2 = "insert into tbl_Warehouse_Capacity_Offer_Kharif2022_log select * from tbl_Warehouse_Capacity_Offer_Kharif2022 where Registration_Id = '" + txtSearch.Text + "' ";
                                    //string qryInsert2 = "insert into tbl_warehouse_capacity_offer_Rabi2023_log select * from tbl_warehouse_capacity_offer_Rabi2023 where Registration_Id = '" + txtSearch.Text + "' ";
                                    //string qryInsert2 = "insert into tbl_Warehouse_Capacity_Offer_Rabi_2024_25_Log select * from tbl_Warehouse_Capacity_Offer_Rabi_2024_25 where Registration_Id = '" + txtSearch.Text + "' ";
                                    //string qryInsert2 = "insert into tbl_Warehouse_Capacity_Offer_Rabi_2025_26_Log select * from tbl_Warehouse_Capacity_Offer_Rabi_2025_26 where Registration_Id = '" + txtSearch.Text + "' ";
                                    string qryInsert2 = "insert into tbl_Warehouse_Capacity_Offer_Rabi_2026_27_Log select * from tbl_Warehouse_Capacity_Offer_Rabi_2026_27 where Registration_Id = '" + txtSearch.Text + "' ";

                                    SqlCommand cmd5 = new SqlCommand(qryInsert2, con, sqltran);
                                    int M = cmd5.ExecuteNonQuery();
                                    if (M > 0)
                                    {
                                        //string qryUpdate2 = "Update tbl_Warehouse_Capacity_Offer_2019 set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";
                                        //string qryUpdate2 = "Update tbl_Warehouse_Capacity_Offer_2020 set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";
                                        //string qryUpdate2 = "Update tbl_Warehouse_Capacity_Offer_2021 set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";
                                        //string qryUpdate2 = "Update tbl_Warehouse_Capacity_Offer_2022 set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";
                                        //string qryUpdate2 = "Update tbl_Warehouse_Capacity_Offer_Kharif2022 set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";
                                        //string qryUpdate2 = "Update tbl_Warehouse_Capacity_Offer_Rabi_2024_25 set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";
                                        //string qryUpdate2 = "Update tbl_Warehouse_Capacity_Offer_Rabi_2025_26 set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";
                                        string qryUpdate2 = "Update tbl_Warehouse_Capacity_Offer_Rabi_2026_27 set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";

                                        SqlCommand cmd6 = new SqlCommand(qryUpdate2, con, sqltran);
                                        int N = cmd6.ExecuteNonQuery();
                                        if (N > 0)
                                        {
                                            //string qryInsert3 = "insert into tbl_Warehouse_Godown_Offer_2019_log select * from tbl_Warehouse_Godown_Offer_2019 where Registration_Id = '" + txtSearch.Text + "' ";
                                            //string qryInsert3 = "insert into tbl_Warehouse_Godown_Offer_2020_log select * from tbl_Warehouse_Godown_Offer_2020 where Registration_Id = '" + txtSearch.Text + "' ";
                                            //string qryInsert3 = "insert into tbl_Warehouse_Godown_Offer_2021_log select * from tbl_Warehouse_Godown_Offer_2021 where Registration_Id = '" + txtSearch.Text + "' ";
                                            //string qryInsert3 = "insert into tbl_Warehouse_Godown_Offer_2022_log select * from tbl_Warehouse_Godown_Offer_2022 where Registration_Id = '" + txtSearch.Text + "' ";
                                            //string qryInsert3 = "insert into tbl_Warehouse_Godown_Offer_Kharif2022_log select * from tbl_Warehouse_Godown_Offer_Kharif2022 where Registration_Id = '" + txtSearch.Text + "' ";
                                            //string qryInsert3 = "insert into tbl_Warehouse_Godown_Offer_Rabi_2024_25_log select * from tbl_Warehouse_Godown_Offer_Rabi_2024_25 where Registration_Id = '" + txtSearch.Text + "' ";
                                            //string qryInsert3 = "insert into tbl_Warehouse_Godown_Offer_Rabi_2025_26_log select * from tbl_Warehouse_Godown_Offer_Rabi_2025_26 where Registration_Id = '" + txtSearch.Text + "' ";
                                            string qryInsert3 = "insert into tbl_Warehouse_Godown_Offer_Rabi_2026_27_log select * from tbl_Warehouse_Godown_Offer_Rabi_2026_27 where Registration_Id = '" + txtSearch.Text + "' ";

                                            SqlCommand cmd7 = new SqlCommand(qryInsert3, con, sqltran);
                                            int O = cmd7.ExecuteNonQuery();
                                            if (O > 0)
                                            {
                                                //string qryUpdate3 = "Update tbl_Warehouse_Godown_Offer_2019 set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";
                                                //string qryUpdate3 = "Update tbl_Warehouse_Godown_Offer_2020 set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";
                                                //string qryUpdate3 = "Update tbl_Warehouse_Godown_Offer_2021 set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";
                                                //string qryUpdate3 = "Update tbl_Warehouse_Godown_Offer_2022 set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";
                                                //string qryUpdate3 = "Update tbl_Warehouse_Godown_Offer_Kharif2022 set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";
                                                //string qryUpdate3 = "Update tbl_Warehouse_Godown_Offer_Rabi_2024_25 set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";
                                                //string qryUpdate3 = "Update tbl_Warehouse_Godown_Offer_Rabi_2025_26 set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";
                                                string qryUpdate3 = "Update tbl_Warehouse_Godown_Offer_Rabi_2026_27 set DistrictId='" + ddlDistrict.SelectedValue + "',BranchId = '" + ddlBranch.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "'";

                                                SqlCommand cmd8 = new SqlCommand(qryUpdate3, con, sqltran);
                                                int P = cmd8.ExecuteNonQuery();
                                                if (P > 0)
                                                {
                                                    string qryInsert4 = "insert into tbl_Warehouse_PreReg_Log select * from tbl_Warehouse_PreReg where Reg_No = '" + txtSearch.Text + "' ";
                                                    SqlCommand cmd9 = new SqlCommand(qryInsert4, con, sqltran);
                                                    int Q = cmd9.ExecuteNonQuery();
                                                    if (Q == 1)
                                                    {
                                                        string qryUpdate4 = "Update tbl_Warehouse_PreReg set DistrictID='" + ddlDistrict.SelectedValue + "',UpdatedBy='" + Client_Ip + "',UpdatedDate=GetDate() where Reg_No = '" + txtSearch.Text + "'";
                                                        SqlCommand cmd10 = new SqlCommand(qryUpdate4, con, sqltran);
                                                        int R = cmd10.ExecuteNonQuery();
                                                        if (R == 1)
                                                        {
                                                            int chkinsp = ChkInspection();
                                                            if (chkinsp == 1)
                                                            {
                                                                //string qryInsert5 = "insert into tbl_Godown_Inspection_2023_Log select * from tbl_Godown_Inspection_2023 where Registration_Id='" + txtSearch.Text + "'";
                                                                string qryInsert5 = "insert into tbl_Godown_Inspection_2026_Log select * from tbl_Godown_Inspection_2026 where Registration_Id='" + txtSearch.Text + "'";
                                                                SqlCommand cmd11 = new SqlCommand(qryInsert5, con, sqltran);
                                                                int S = cmd11.ExecuteNonQuery();

                                                                string qryUpdate5 = "update tbl_Godown_Inspection_2026 set BranchId='" + ddlBranch.SelectedValue + "',DistrictId='" + ddlDistrict.SelectedValue + "',UpdatedDate=GETDATE() where Registration_Id='" + txtSearch.Text + "'";
                                                                SqlCommand cmd12 = new SqlCommand(qryUpdate5, con, sqltran);
                                                                int T = cmd12.ExecuteNonQuery();
                                                            }
                                                            int chkAgree = ChkAgreement();
                                                            if (chkinsp == 1)
                                                            {
                                                                string qryInsert6 = "insert into tbl_Godown_Agreement_log select * from tbl_Godown_Agreement where Registration_Id='" + txtSearch.Text + "'";
                                                                SqlCommand cmd13 = new SqlCommand(qryInsert6, con, sqltran);
                                                                int U = cmd13.ExecuteNonQuery();

                                                                string qryUpdate6 = "update tbl_Godown_Agreement set BranchId='" + ddlBranch.SelectedValue + "',DistrictId='" + ddlDistrict.SelectedValue + "',UpdatedDate=GETDATE() where Registration_Id='" + txtSearch.Text + "'";
                                                                SqlCommand cmd14 = new SqlCommand(qryUpdate6, con, sqltran);
                                                                int V = cmd14.ExecuteNonQuery();
                                                            }

                                                            sqltran.Commit();
                                                            btnUpdate.Enabled = false;
                                                            ModalPopupExtender1.Show();
                                                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save...'); </script> ");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('since this godown has not been offered in the current Year'); </script> ");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sqltran.Rollback();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Something Error...'); </script> ");
            }
            finally
            {
                sqltran.Dispose();
                con.Close();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Registration ID .....')", true);
        }
        Search();
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    public int ChkInspection()
    {
        int ch = 0;
        string strsql = "select Registration_Id from tbl_Godown_Inspection_2026 where Registration_Id='" + txtSearch.Text + "' and CreatedDate >= convert(varchar(10),'04/01/2026',101) ";
        SqlCommand cmd = new SqlCommand(strsql, con, sqltran);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ch = 1;
        }
        else
        {
            ch = 0;
        }
        return ch;
    }

    public int ChkAgreement()
    {
        int chk = 0;
        //string strsql = " select Registration_Id from tbl_Godown_Agreement where Registration_Id='" + txtSearch.Text + "' and CreatedDate > convert(varchar(10),'02/17/2019',101)";
        //string strsql = " select Registration_Id from tbl_Godown_Agreement where Registration_Id='" + txtSearch.Text + "' and CreatedDate >= convert(varchar(10),'02/27/2023',101)";
        string strsql = " select Registration_Id from tbl_Godown_Agreement where Registration_Id='" + txtSearch.Text + "' and CreatedDate >= convert(varchar(10),'04/01/2026',101)";
        SqlCommand cmd = new SqlCommand(strsql, con, sqltran);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            chk = 1;
        }
        else
        {
            chk = 0;
        }
        return chk;
    }

    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangeBranch.aspx");
    }
    protected void btnSPRun_Click(object sender, EventArgs e)
    {
        try
        {
            if (conWlc.State == ConnectionState.Closed)
            {
                conWlc.Open();
            }
            sqltran = conWlc.BeginTransaction();
            SqlCommand cmd = new SqlCommand("spUpdateReceiptProc2020", conWlc, sqltran);
            cmd.CommandType = CommandType.StoredProcedure;
            int aa = cmd.ExecuteNonQuery();
            if (aa > 1)
            {
                sqltran.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + aa + "')", true);
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex + "')", true);
        }
    }
}