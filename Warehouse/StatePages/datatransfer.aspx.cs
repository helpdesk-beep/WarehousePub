using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class StatePages_datatransfer : System.Web.UI.Page
{

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == "Admin")
        {
            if (!IsPostBack)
            {
                GetDist("4");
            }
        }
        else
        {

            Response.Redirect("~/login.aspx");
        }
    }


    private void GetDist(string Scope)
    {
        try
        {

            string strDist = "";

            if (Scope == "4")
            {
                strDist = "SELECT [District_Id] as  [login_id], [District_Name] as  [User_Name] FROM [tbl_MetaData_DISTRICT] order by [User_Name]";
            }
            SqlDataAdapter da = new SqlDataAdapter(strDist, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Dist.DataSource = ds.Tables[0];
                DDL_Dist.DataTextField = "User_Name";
                DDL_Dist.DataValueField = "login_id";

                DDL_Dist.DataBind();
                ddldist2.DataSource = ds.Tables[0];
                ddldist2.DataTextField = "User_Name";
                ddldist2.DataValueField = "login_id";
                ddldist2.DataBind();
                ddldist2.Items.Insert(0, "---Select---");
                DDL_Dist.Items.Insert(0, "---Select---");
            }
            else
            {
                DDL_Dist.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }


    private void getDepot(string distId)
    {
        try
        {
            string str = "SELECT  [DepotID],[DepotName] ,[BranchId] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_DEPOT] where DistrictId='" + distId.ToString() + "' order by DepotName";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Depot.DataSource = ds.Tables[0];
                DDL_Depot.DataTextField = "DepotName";
                DDL_Depot.DataValueField = "BranchId";
                DDL_Depot.DataBind();
                DDL_Depot.Items.Insert(0, "---Select---");
            }
            else
            {
                DDL_Depot.Items.Clear();
            }

            DDL_Depot.Visible = true;
        }

        catch (Exception ex)
        {

        }
    }

    private void getDepot2()
    {
        try
        {
            string str = "SELECT  [DepotID],[DepotName] ,[BranchId] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_DEPOT] where DistrictId='" + ddldist2.SelectedValue.ToString() + "' order by DepotName ";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbranch2.DataSource = ds.Tables[0];
                ddlbranch2.DataTextField = "DepotName";
                ddlbranch2.DataValueField = "BranchId";
                ddlbranch2.DataBind();
                ddlbranch2.Items.Insert(0, "---Select---");
            }
            else
            {
                ddlbranch2.Items.Clear();
            }

            DDL_Depot.Visible = true;
        }

        catch (Exception ex)
        {

        }
    }

    protected void DDL_Dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        getDepot(DDL_Dist.SelectedItem.Value.ToString());
    }
    protected void ddldist2_SelectedIndexChanged(object sender, EventArgs e)
    {
        getDepot2();
    }
    private void GetGodown()
    {
        try
        {
            string str = "SELECT  [Godown_ID],[Godown_Name],[Hired_Type],[Storage_Type],[BranchID] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where BranchID='" + DDL_Depot.SelectedValue.ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvgodown.DataSource = ds.Tables[0];

                gvgodown.DataBind();

            }
            else
            {
                //    gvgodown.Items.Clear();
            }

            DDL_Depot.Visible = true;
        }

        catch (Exception ex)
        {

        }
    }

    protected void DDL_Depot_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodown();
    }
    protected void gvgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbranch2.Text != "---Select---" && ddlbranch2.Text != "")
        {
            SqlTransaction sqltran = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                sqltran = con.BeginTransaction();
                string str = "insert into tbl_MetaData_GODOWN_log select * from [tbl_MetaData_GODOWN] where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "'";
                cmd = new SqlCommand(str, con, sqltran);
                int req = cmd.ExecuteNonQuery();
                if (req > 0)
                {
                    string str2 = "update [tbl_MetaData_GODOWN] set DistrictId='" + ddldist2.SelectedValue.ToString() + "',DepotId='" + ddlbranch2.SelectedValue.ToString() + "' , BranchID='" + ddlbranch2.SelectedValue.ToString() + "' where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "'";
                    cmd = new SqlCommand(str2, con, sqltran);
                    int req2 = cmd.ExecuteNonQuery();
                    if (req2 > 0)
                    {
                        //csms_godown_new.csms_godown_webser newgodwn = new csms_godown_new.csms_godown_webser();
                        //newgodwn.UpdateRecord(gvgodown.SelectedRow.Cells[1].Text.ToString(), "23", ddldist2.SelectedValue.ToString(), ddlbranch2.SelectedValue.ToString(), gvgodown.SelectedRow.Cells[2].Text.ToString(), "01/01/2016", "01/01/2016", "50000", "Y", gvgodown.SelectedRow.Cells[3].Text.ToString(), gvgodown.SelectedRow.Cells[4].Text.ToString(), "50000", ddlbranch2.SelectedValue.ToString());

                        string str3 = "insert into dbo.tbl_MetaData_STACK_Log select * from [tbl_MetaData_STACK] where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "'";
                        cmd = new SqlCommand(str3, con, sqltran);
                        int req3 = cmd.ExecuteNonQuery();

                        string str4 = "  update [tbl_MetaData_STACK] set District_Id='"+ ddldist2.SelectedValue.ToString() + "', DepotId='" + ddlbranch2.SelectedValue.ToString() + "' , BranchId='" + ddlbranch2.SelectedValue.ToString() + "' where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "'";
                        cmd = new SqlCommand(str4, con, sqltran);
                        int req4 = cmd.ExecuteNonQuery();

                        string str5 = "insert into dbo.tbl_Storage_Receipt_Details_DeleteLog select * from [tbl_Storage_Receipt_Details] where StorageReceipt_Id in (select StorageReceipt_Id from  [tbl_storage_Stacking_Details] where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "')";
                        cmd = new SqlCommand(str5, con, sqltran);
                        int req5 = cmd.ExecuteNonQuery();

                        string str6 = "  update [tbl_Storage_Receipt_Details] set District_Id='" + ddldist2.SelectedValue.ToString() + "', BranchID='" + ddlbranch2.SelectedValue.ToString() + "' ,Depotid='" + ddlbranch2.SelectedValue.ToString() + "' where StorageReceipt_Id in (select StorageReceipt_Id from  [tbl_storage_Stacking_Details] where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "')";
                        cmd = new SqlCommand(str6, con, sqltran);
                        int req6 = cmd.ExecuteNonQuery();

                        string str7 = "   insert into dbo.tbl_storage_Stacking_Details_Log select * from [tbl_storage_Stacking_Details] where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "'";
                        cmd = new SqlCommand(str7, con, sqltran);
                        int req7 = cmd.ExecuteNonQuery();

                        string str8 = "     update [tbl_storage_Stacking_Details] set District_Id='" + ddldist2.SelectedValue.ToString() + "', Depotid='" + ddlbranch2.SelectedValue.ToString() + "' , Branchid='" + ddlbranch2.SelectedValue.ToString() + "' where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "'";
                        cmd = new SqlCommand(str8, con, sqltran);
                        int req8 = cmd.ExecuteNonQuery();

                        string str9 = "insert into dbo.tbl_Storage_Arrival_Stock_Log select * from [tbl_Storage_Arrival_Stock] where Receipt_ID in (select StorageReceipt_Id from  [tbl_storage_Stacking_Details] where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "')";
                        cmd = new SqlCommand(str9, con, sqltran);
                        int req9 = cmd.ExecuteNonQuery();

                        string str10 = "  update [tbl_Storage_Arrival_Stock] set District_Id='" + ddldist2.SelectedValue.ToString() + "', DepotId='" + ddlbranch2.SelectedValue.ToString() + "' ,BranchID='" + ddlbranch2.SelectedValue.ToString() + "' where Receipt_ID in (select StorageReceipt_Id from  [tbl_storage_Stacking_Details] where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "')";
                        cmd = new SqlCommand(str10, con, sqltran);
                        int req10 = cmd.ExecuteNonQuery();

                        string str11 = "insert into dbo.tbl_storage_Depositor_WHR_Relation_Log select * from [tbl_storage_Depositor_WHR_Relation] where Whr_No in (select distinct WHRId from  [tbl_storage_Stacking_Details] where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "' and WHRId is not null)";
                        cmd = new SqlCommand(str11, con, sqltran);
                        int req11 = cmd.ExecuteNonQuery();

                        string str12 = "    update [tbl_storage_Depositor_WHR_Relation] set District_Id='" + ddldist2.SelectedValue.ToString() + "', Depotid='" + ddlbranch2.SelectedValue.ToString() + "',BranchID='" + ddlbranch2.SelectedValue.ToString() + "' where Whr_No in (select distinct WHRId from  [tbl_storage_Stacking_Details] where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "' and WHRId is not null)";
                        cmd = new SqlCommand(str12, con, sqltran);
                        int req12 = cmd.ExecuteNonQuery();

                        //  Here Stock is Transfer from One Branch To another 
                        //  Str 13 Or 14 in last

                        string str15 = "insert into tbl_RO_Details_log select * from tbl_RO_Details as RO where RO.GatePass_No in (select GP.GatePass_No from  tbl_Storage_GatePass_Enrty as GP where GP.Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "')";
                        cmd = new SqlCommand(str15, con, sqltran);
                        int req15 = cmd.ExecuteNonQuery();

                        string str16 = "Update tbl_RO_Details set District_Id='" + ddldist2.SelectedValue.ToString() + "', DepotId='" + ddlbranch2.SelectedValue.ToString() + "', BranchID='" + ddlbranch2.SelectedValue.ToString() + "' where GatePass_No in (select SGE.GatePass_No from  tbl_Storage_GatePass_Enrty as SGE where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "')";
                        cmd = new SqlCommand(str16, con, sqltran);
                        int req16 = cmd.ExecuteNonQuery();

                        string str17 = "insert into  tbl_Storage_Final_Stock_Delivery_GatePass_DelLog  select * from  tbl_Storage_Final_Stock_Delivery_GatePass as SFS where SFS.GatePass_No in (select GP.GatePass_No from  tbl_Storage_GatePass_Enrty as GP where GP.Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "')";
                        cmd = new SqlCommand(str17, con, sqltran);
                        int req17 = cmd.ExecuteNonQuery();

                        string str18 = "update tbl_Storage_Final_Stock_Delivery_GatePass set District_Id='" + ddldist2.SelectedValue.ToString() + "', DepotId='" + ddlbranch2.SelectedValue.ToString() + "',BranchID='" + ddlbranch2.SelectedValue.ToString() + "' where GatePass_No in (select GP.GatePass_No from  tbl_Storage_GatePass_Enrty as GP where GP.Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "')";
                        cmd = new SqlCommand(str18, con, sqltran);
                        int req18 = cmd.ExecuteNonQuery();

                        string str19 = "insert into tbl_Storage_Final_Stock_Delivery_Order_Log select * from  tbl_Storage_Final_Stock_Delivery_Order as DO where DO.Delivery_Order_No in (select Issue_Source_ID from  tbl_Storage_GatePass_Enrty  as GP where GP.Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "')";
                        cmd = new SqlCommand(str19, con, sqltran);
                        int req19 = cmd.ExecuteNonQuery();

                        string str20 = "update tbl_Storage_Final_Stock_Delivery_Order set District_Id='" + ddldist2.SelectedValue.ToString() + "', DepotId='" + ddlbranch2.SelectedValue.ToString() + "',BranchID='" + ddlbranch2.SelectedValue.ToString() + "' where Delivery_Order_No in (select Issue_Source_ID from  tbl_Storage_GatePass_Enrty  as GP where GP.Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "')  ";
                        cmd = new SqlCommand(str20, con, sqltran);
                        int req20 = cmd.ExecuteNonQuery();

                        string str21 = "insert into OtherDepoChallanDetails_log select * from  OtherDepoChallanDetails as ODC where ODC.Gatepass in (select GP.GatePass_No from  tbl_Storage_GatePass_Enrty as GP where GP.Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "')";
                        cmd = new SqlCommand(str21, con, sqltran);
                        int req21 = cmd.ExecuteNonQuery();

                        string str22 = "update OtherDepoChallanDetails set BranchID='" + ddlbranch2.SelectedValue.ToString() + "'  where Gatepass in (select GP.GatePass_No from  tbl_Storage_GatePass_Enrty as GP where GP.Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "')";
                        cmd = new SqlCommand(str22, con, sqltran);
                        int req22 = cmd.ExecuteNonQuery();

                        string str13 = "insert into tbl_Storage_GatePass_Enrty_Log select * from  tbl_Storage_GatePass_Enrty where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "'";
                        cmd = new SqlCommand(str13, con, sqltran);
                        int req13 = cmd.ExecuteNonQuery();

                        string str14 = "Update tbl_Storage_GatePass_Enrty set District_Id='" + ddldist2.SelectedValue.ToString() + "', BranchID='" + ddlbranch2.SelectedValue.ToString() + "' , Depot_ID='" + ddlbranch2.SelectedValue.ToString() + "', UpdatedDate=GETDATE() where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "'";
                        cmd = new SqlCommand(str14, con, sqltran);
                        int req14 = cmd.ExecuteNonQuery();

                        string str133 = "insert into tbl_MetaData_GODOWN_2018_log select * from  tbl_MetaData_GODOWN_2018 where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "'";
                        cmd = new SqlCommand(str133, con, sqltran);
                        int req133 = cmd.ExecuteNonQuery();

                        string str144 = "Update tbl_MetaData_GODOWN_2018 set BranchID='" + ddlbranch2.SelectedValue.ToString() + "' , DepotID='" + ddlbranch2.SelectedValue.ToString() + "',DistrictId='" + ddldist2.SelectedValue.ToString().Trim() + "', UpdatedDate=GETDATE() where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "'";
                        cmd = new SqlCommand(str144, con, sqltran);
                        int req144 = cmd.ExecuteNonQuery();

                        string str1333 = "insert into Pvt_Warehouse_Login_Log select * from  Pvt_Warehouse_Login where Godown_Id ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "'";
                        cmd = new SqlCommand(str1333, con, sqltran);
                        int req1333 = cmd.ExecuteNonQuery();

                        string str1444 = "Update Pvt_Warehouse_Login set BranchID='" + ddlbranch2.SelectedValue.ToString() + "' , DepotID='" + ddlbranch2.SelectedValue.ToString() + "',DistrictId='" + ddldist2.SelectedValue.ToString().Trim() + "' where Godown_Id ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "'";
                        cmd = new SqlCommand(str1444, con, sqltran);
                        int req1444 = cmd.ExecuteNonQuery();

                        string str1555 = "insert into tbl_MetaData_GODOWN_2018_NonExist_log select * from  tbl_MetaData_GODOWN_2018_NonExist where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "'";
                        cmd = new SqlCommand(str1555, con, sqltran);
                        int req1555 = cmd.ExecuteNonQuery();

                        string str1666 = "Update tbl_MetaData_GODOWN_2018_NonExist set BranchID='" + ddlbranch2.SelectedValue.ToString() + "' , DepotId='" + ddlbranch2.SelectedValue.ToString() + "',DistrictId='" + ddldist2.SelectedValue.ToString().Trim() + "' where Godown_Id ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "'";
                        cmd = new SqlCommand(str1666, con, sqltran);
                        int req1666 = cmd.ExecuteNonQuery();

                        string str1777 = "insert into tbl_Godown_Owner_Account_Details_Log select * from  tbl_Godown_Owner_Account_Details where Godown_ID ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "'";
                        cmd = new SqlCommand(str1555, con, sqltran);
                        int req1777 = cmd.ExecuteNonQuery();

                        string str1888 = "Update tbl_Godown_Owner_Account_Details set BranchID='" + ddlbranch2.SelectedValue.ToString() + "' ,DistrictId='" + ddldist2.SelectedValue.ToString().Trim() + "' where Godown_Id ='" + gvgodown.SelectedRow.Cells[1].Text.ToString() + "'";
                        cmd = new SqlCommand(str1666, con, sqltran);
                        int req1888 = cmd.ExecuteNonQuery();

                        if (req1444 > 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Successfully Update'); </script> ");
                        }
                    }
                    sqltran.Commit();
                    con.Close();
                    GetGodown();
                }
                else
                {
                    //lbl_message.Text = "WHR record saved successfully";
                }
            }


            catch (Exception ex)
            {
                sqltran.Rollback();
                // lbl_message.Text = ex.Message;
                Response.Write(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }
    }
}