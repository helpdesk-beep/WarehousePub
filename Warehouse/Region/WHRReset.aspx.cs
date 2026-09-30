using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class Region_WHRReset : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "", TheResult = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            //string region = Session["Region_ID"].ToString();

            if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        Session["RefreshButton"] = "No";
                        string PopMsg = "";
                        PopMsg = Request.QueryString["PopMsg"];
                        if (PopMsg != null)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + PopMsg + "')", true);
                        }
                        fillDistrict();
                    }
                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
                }
            }

            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
    }

    private void getDepot(string distId)
    {
        try
        {
            string query = "";
            if (Session["RoleId"].ToString() == "9")
            {
                query = "select depo.BranchId,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' and DepoTypeID='4' order by DepotName asc";
            }
            else
            {
                query = "select depo.BranchId,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";
            }
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepotList.DataSource = ds.Tables[0];
                ddlDepotList.DataTextField = "DepotName";
                ddlDepotList.DataValueField = "BranchId";
                ddlDepotList.DataBind();
                ddlDepotList.Items.Insert(0, "---Select---");
                ddlGodown.DataSource = null;
                ddlGodown.DataBind();
                gv.DataSource = null;
                gv.DataBind();
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

    public void FillGrid()
    {
        //  string query = "select DISTINCT WHR.Depositor_WHR_Id,CONVERT(varchar(10),WHR.WHR_Issue_Date,103) AS whrdate,WHR.Depositor_Name,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,WHR.TotalBags_Received AS Bags,convert(decimal(18,2),WHR.Total_Qty_Received) AS Qty,tbl_MetaData_GODOWN.Godown_Name from tbl_storage_Depositor_WHR_Relation AS WHR join tbl_Storage_Receipt_Details on WHR.Depositor_WHR_Id = tbl_Storage_Receipt_Details.WHR_Id join tbl_storage_Stacking_Details on WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId JOIN tbl_MetaData_STORAGE_COMMODITY on WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id JOIN tbl_MetaData_GODOWN on tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID where WHR.Depotid = '" + ddlDepotList.SelectedValue.ToString() + "' and WHR.District_Id = '" + ddlDistrict.SelectedValue.ToString() + "' and tbl_MetaData_GODOWN.Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' AND tbl_Storage_Receipt_Details.WHR_Flag = 'Y'  order by WHR.Depositor_WHR_Id asc";
        string query = "select DISTINCT WHR.Depositor_WHR_Id,CONVERT(varchar(10),WHR.WHR_Issue_Date,103) AS whrdate,WHR.Depositor_Name,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,WHR.TotalBags_Received AS Bags,convert(decimal(18,2),WHR.Total_Qty_Received) AS Qty,tbl_MetaData_GODOWN.Godown_Name from tbl_storage_Depositor_WHR_Relation AS WHR  join tbl_storage_Stacking_Details as sd on WHR.Depositor_WHR_Id = sd.WHRId JOIN tbl_MetaData_STORAGE_COMMODITY on WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id JOIN tbl_MetaData_GODOWN on sd.Godown_ID = tbl_MetaData_GODOWN.Godown_ID where WHR.BranchID = '" + ddlDepotList.SelectedValue.ToString() + "' and WHR.District_Id = '" + ddlDistrict.SelectedValue.ToString() + "' and tbl_MetaData_GODOWN.Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and WHR.Depositor_Name in('MPSCSC','DMO Markfed')   order by WHR.Depositor_WHR_Id asc";
        cmd = new SqlCommand(query, con);
        da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        Session["ds_GridInfo"] = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            lblRowCount.Text = "";
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No WHR Found...')", true);
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            gv.DataSource = null;
            gv.DataBind();
        }
    }

    private void fillDistrict()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

                if (Session["Region_ID"].ToString() != null)
                {
                    region = Session["Region_ID"].ToString();
                }
            }
            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            }
            else
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
            }
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
                gv.DataSource = null;
                gv.DataBind();
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

    private void fillDistrictmfd()
    {
        try
        {
            string region = "";

            string query = "";

            query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where ISMFD='Y' order by District_Name asc";

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
                gv.DataSource = null;
                gv.DataBind();
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

    private void Getgodowns()
    {
        string str = "select Godown_Name,Godown_id from tbl_MetaData_GODOWN where BranchId='" + ddlDepotList.SelectedValue.ToString() + "' and DistrictId = '" + ddlDistrict.SelectedValue.ToString() + "'  order by Godown_Name ";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGodown.DataSource = ds.Tables[0];
            ddlGodown.DataTextField = "Godown_Name";
            ddlGodown.DataValueField = "Godown_id";
            ddlGodown.DataBind();
            ddlGodown.Items.Insert(0, "--Select--");
            gv.DataSource = null;
            gv.DataBind();
        }
        else
        {
            ddlGodown.DataSource = null;
            ddlGodown.DataBind();
            ddlGodown.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District First..')", true);
        }
        else if (ddlDepotList.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Branch First..')", true);
        }
        else if (ddlGodown.SelectedIndex != 0)
        {
            FillGrid();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Godown First')", true);
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
            Getgodowns();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Branch First..')", true);
        }
    }


    protected void gv_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string whr = gv.SelectedRow.Cells[1].Text.ToString();
        // string query1 = "insert into [WhrPrintResetDetail] values('" + whr + "','" + ip + "',GetDate(),'" + ddlDepotList.SelectedValue.ToString() + "','R')";
        con.Open();

        cmd = new SqlCommand("Reset_Whr_Details", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@WHRID", whr);
        cmd.Parameters.AddWithValue("@UpdatedBy", ip);
        cmd.Parameters.AddWithValue("@BranchID", ddlDepotList.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Delete_Flag", "R");
        if (rbori.Checked == true)
        {
            cmd.Parameters.AddWithValue("@PrintStatus", "1st");
        }
        else if (rbdupli.Checked == true)
        {
            cmd.Parameters.AddWithValue("@PrintStatus", "4th");

        }
        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        con.Close();
        TheResult = cmd.Parameters["@TheResult"].Value.ToString();
        if (TheResult.StartsWith("SUCCESS"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Whr status updated')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Whr status NOT updated')", true);
        }
        //SqlCommand cmd1 = new SqlCommand(query1, con);
        //cmd1.ExecuteNonQuery();
        //con.Close();
        //string query = "";
        //if (rbori.Checked == true)
        //{
        //    query = "update [whrprintstatus] set PrintStatus='1st' , DateUpdated=GETDATE() where WHRID='" + whr + "'";
        //}
        //else if (rbdupli.Checked == true)
        //{
        //    query = "update [whrprintstatus] set PrintStatus='4th' , DateUpdated=GETDATE() where WHRID='" + whr + "'";

        //}
        //con.Open();
        //SqlCommand cmd2 = new SqlCommand(query, con);
        //cmd2.ExecuteNonQuery();
    }
}