using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Text;
using System.Resources;

public partial class Region_UpdateGatepass : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connect_warehouse"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    SqlDataAdapter da = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] != null)
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
                    Notinstoragestacking();
                    NotinstoragestackingGatepass();
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void fillGatepassGrid()
    {
        try
        {
            string query = "select SG.GatePass_No,COM.Commodity_Name,SG.[Depositor/Issuer_Name] AS Depname,SG.Vehicle_No,SG.NO_of_Bage,convert(decimal(18,2),SG.Weight) as Weight,SG.Status,convert(nvarchar(10),SG.Issue_Date,103) as Issue_Date from tbl_Storage_GatePass_Enrty as SG join tbl_MetaData_STORAGE_COMMODITY AS COM on SG.Commodity_ID = COM.Commodity_Id WHERE SG.District_ID = '" + ddlDistrict.SelectedValue.ToString() + "' AND SG.Depot_ID = '" + ddlDepotList.SelectedValue.ToString() + "' AND SG.Status = 'Cancel'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbl_Empty.Text = "";
                lbl_Empty.Visible = false;
                gv_gatepass.DataSource = ds.Tables[0];
                gv_gatepass.DataBind();
                lblRowCount.Text = "Total No. of Records -" + gv_gatepass.Rows.Count.ToString();
            }
            else
            {
                lbl_Empty.Visible = true;
                lbl_Empty.Text = "There is No Delivery GatePass Pending";
                lblRowCount.Text = "Total No. of Records -" + gv_gatepass.Rows.Count.ToString();
                gv_gatepass.DataSource = null;
                gv_gatepass.DataBind();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected string getDate_MDY(string inDate)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

    }

    private void fillDistrict()
    {
        try
        {
            string query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            cmd = new SqlCommand(query, con);
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
                ddlDistrict.Items.Insert(0, "---Select---");
                gv_gatepass.DataSource = null;
                gv_gatepass.DataBind();
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

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StatePages/StateWelcome.aspx");
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex != 0)
        {
            getDepot(ddlDistrict.SelectedValue.ToString());
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District')", true);
        }
    }

    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District First..')", true);
        }
        else if (ddlDepotList.SelectedIndex != 0)
        {
            fillGatepassGrid();
        }
    }

    private void getDepot(string distId)
    {
        try
        {
            string query = "select depo.DepotID,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepotList.DataSource = ds.Tables[0];
                ddlDepotList.DataTextField = "DepotName";
                ddlDepotList.DataValueField = "DepotID";
                ddlDepotList.DataBind();
                ddlDepotList.Items.Insert(0, "---Select---");
                gv_gatepass.DataSource = null;
                gv_gatepass.DataBind();
            }
            else
            {
                ddlDepotList.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception)
        {
            ///////
        }
    }

    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        try
        {
            if (gv_gatepass.Rows.Count > 0)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                foreach (GridViewRow gr2 in gv_gatepass.Rows)
                {
                    CheckBox chk_Delete = new CheckBox();
                    string Gatepass = Convert.ToString(gv_gatepass.DataKeys[gr2.RowIndex].Value);
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                    if (chk_Delete.Checked == true)
                    {
                        qry = "insert into tbl_Storage_GatePass_Enrty_Log select * from tbl_Storage_GatePass_Enrty where Depot_ID = '" + ddlDepotList.SelectedValue.ToString() + "' and GatePass_No = '" + Gatepass + "'";
                        cmd = new SqlCommand(qry, con);
                        int c = cmd.ExecuteNonQuery();
                        if (c > 0)
                        {
                            qry = "update tbl_Storage_GatePass_Enrty set Status = 'Active' where Depot_ID = '" + ddlDepotList.SelectedValue.ToString() + "' and GatePass_No = '" + Gatepass + "'";
                            cmd = new SqlCommand(qry, con);
                            int d = cmd.ExecuteNonQuery();
                            if (d > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has been Updated successfully')", true);
                                fillGatepassGrid();
                            }
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Updating')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }
        finally
        {
            con.Close();
        }
    }

    private void Notinstoragestacking()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            qry = "insert into tbl_Delivery_Stacking_Details_GatePass_Log select * from tbl_Delivery_Stacking_Details_GatePass where Stack_ID not in (select Stack_ID from tbl_MetaData_STACK) union all select * from tbl_Delivery_Stacking_Details_GatePass where Depositor_WHR_Id not in (select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation) union all select * from tbl_Delivery_Stacking_Details_GatePass where GatePass_No not in (select GatePass_No from tbl_Storage_GatePass_Enrty)";
            cmd = new SqlCommand(qry, con);
            int c = cmd.ExecuteNonQuery();
            if (c > 0)
            {
                qry = "Delete  from tbl_Delivery_Stacking_Details_GatePass where Stack_ID not in (select Stack_ID from tbl_MetaData_STACK)";
                cmd = new SqlCommand(qry, con);
                int d = cmd.ExecuteNonQuery();

                ///////////////////////////////

                qry = "Delete from tbl_Delivery_Stacking_Details_GatePass where Depositor_WHR_Id not in (select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation)";
                cmd = new SqlCommand(qry, con);
                int e = cmd.ExecuteNonQuery();

                ///////////////////////////////

                qry = "Delete from tbl_Delivery_Stacking_Details_GatePass where GatePass_No not in (select GatePass_No from tbl_Storage_GatePass_Enrty)";
                cmd = new SqlCommand(qry, con);
                int f = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception)
        {
            //  throw;
        }
        finally
        {
            con.Close();
        }
    }

    private void NotinstoragestackingGatepass()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            qry = "insert into tbl_storage_Stacking_Details_Log select * from tbl_storage_Stacking_Details where Stack_ID not in (select Stack_ID from tbl_MetaData_STACK) union all select * from tbl_storage_Stacking_Details where WHRId not in (select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation) union all select * from tbl_storage_Stacking_Details where StorageReceipt_Id not in (select StorageReceipt_Id from tbl_Storage_Receipt_Details)";
            cmd = new SqlCommand(qry, con);
            int c = cmd.ExecuteNonQuery();
            if (c > 0)
            {
                qry = "delete from tbl_storage_Stacking_Details where Stack_ID not in (select Stack_ID from tbl_MetaData_STACK)";
                cmd = new SqlCommand(qry, con);
                int d = cmd.ExecuteNonQuery();

                ///////////////////////////////

                qry = "delete from tbl_storage_Stacking_Details where WHRId not in (select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation)";
                cmd = new SqlCommand(qry, con);
                int e = cmd.ExecuteNonQuery();

                ///////////////////////////////

                qry = "delete from tbl_storage_Stacking_Details where StorageReceipt_Id not in (select StorageReceipt_Id from tbl_Storage_Receipt_Details)";
                cmd = new SqlCommand(qry, con);
                int f = cmd.ExecuteNonQuery();
            }
        }
        catch (Exception)
        {
            //  throw;
        }
        finally
        {
            con.Close();
        }
    }
}
