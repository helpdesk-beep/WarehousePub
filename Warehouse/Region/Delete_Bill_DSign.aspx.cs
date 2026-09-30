using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class Region_Delete_Bill_DSign : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    string Bill_Type = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
            {
                try
                {
                    if (!IsPostBack)
                    {
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
        else
        {
            Response.Redirect("~/SessionExpired.htm");
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

    private void getDepot(string distId)
    {
        try
        {
            string query = "select depo.BranchId,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";
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
        string query = "";
        if (RadioButton1.Checked == true) { Bill_Type = "SCB"; }
        else if (RadioButton2.Checked == true) { Bill_Type = "GRB"; }
        else if (RadioButton3.Checked == true) { Bill_Type = "SSB"; }
        else if (RadioButton4.Checked == true) { Bill_Type = "NAFED"; }
        else if (RadioButton5.Checked == true) { Bill_Type = "NCCF"; }
        else if (RadioButton6.Checked == true) { Bill_Type = "NCCFRent"; }

        if (Bill_Type == "SCB")
        {
            query = "select SB.Bill_Number,'Storage Charges Bill' as Bill_Name,SB.Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount,DSB.DSC_Holder_Name,DSB.DSC_Serial_No from tbl_Institution_Storage_Bill_Details as SB inner join tbl_Digitally_Signed_Bill_Details as DSB on DSB.Ref_Bill_No=SB.Bill_Number where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' AND SB.BO_Approval_Status is null and SB.Bill_Number in (select DS.Ref_Bill_No from tbl_Digitally_Signed_Bill_Details as DS where DS.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and DS.Ref_Bill_No is not null) and SB.Bill_Number not in (select RM.Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM as RM where RM.BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and RM.Ref_Bill_Number is not null) and SB.Bill_Number not in (select CDS.Ref_Bill_No from MPSCSC.dbo.Digitally_Sign_StorageBill_IC as CDS where Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and CDS.Ref_Bill_No is not null) and SB.Bill_Number='" + txtSearchWHR.Text.Trim() + "'";
        }
        else if (Bill_Type == "GRB")
        {
            query = "select SB.Bill_Number,'Godown Rent Bill' as Bill_Name,Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount from tbl_Institution_Storage_Bill_Details as SB where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and SB.Bill_Number in (select distinct DS.Bill_Number from tbl_Digitally_Signed_Bill_PVT as DS where DS.Bill_Number='" + txtSearchWHR.Text.Trim() + "' and DS.Bill_Number is not null) and SB.Bill_Number not in (select RM.JVS_Bill_Number from tbl_GdwnRentBill_Detuction_RM as RM where RM.JVS_Bill_Number='" + txtSearchWHR.Text.Trim() + "' and RM.JVS_Bill_Number is not null) and SB.Bill_Number='" + txtSearchWHR.Text.Trim() + "'";
        }
        else if (Bill_Type == "SSB")
        {
            query = "select SB.Bill_Number,'Godown Rent Bill' as Bill_Name,Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount from tbl_Institution_Steel_Silo_Storage_Bill_Details as SB where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and SB.Bill_Number in (select distinct DS.Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo as DS where DS.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and DS.Bill_Number is not null) and SB.Bill_Number='" + txtSearchWHR.Text.Trim() + "'";
        }
        else if (Bill_Type == "NAFED")
        {
            query = "select SB.Bill_Number,'Godown Rent Bill' as Bill_Name,Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount from tbl_Storage_Bill_Details as SB where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and SB.Bill_Number in (select distinct DS.Bill_Number from tbl_Digitally_Signed_Bill_Details_Nafed as DS where DS.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and DS.Bill_Number is not null) and SB.Bill_Number='" + txtSearchWHR.Text.Trim() + "'";
        }
        else if (Bill_Type == "NCCF")
        {
            query = "select SB.Bill_Number,'Storage Charges Bill' as Bill_Name,SB.Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount,DSB.DSC_Holder_Name,DSB.DSC_Serial_No from tbl_NCCF_Storage_Bill_Details as SB Inner join tbl_Digitally_Signed_Bill_Details_NCCF as DSB on DSB.Ref_Bill_No=SB.Bill_Number where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' AND SB.BO_Approval_Status is null and SB.Bill_Number in (select DS.Ref_Bill_No from tbl_Digitally_Signed_Bill_Details_NCCF as DS where DS.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and DS.Ref_Bill_No is not null) and SB.Bill_Number not in (select RM.Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM_For_NCCF as RM where RM.BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and RM.Ref_Bill_Number is not null) and SB.Bill_Number not in (select CDS.Bill_Number from tbl_NCCF_Marketing_Approve_Reject as CDS where Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and CDS.Bill_Number is not null) and SB.Bill_Number='" + txtSearchWHR.Text.Trim() + "'";
        }
        else if (Bill_Type == "NCCFRent")
        {
            query = "SELECT SB.Bill_Number,'Godown Rent Bill' AS Bill_Name,Bill_Type,(SELECT Depositor_Name FROM tbl_MetaData_DEPOSITOR WHERE Depositor_ID = SB.Depositor_Id) AS Depositor_Name,CONVERT(varchar(10), SB.Created_Date, 103) AS DateOfBill,SB.Net_Amount,'Branch DSC' AS DSC_Source FROM tbl_Institution_NCCF_Storage_Bill_Details AS SB WHERE SB.Branch_Id = '" + ddlDepotList.SelectedValue.ToString() + "' AND SB.Bill_Number IN (SELECT DISTINCT DS.Bill_Number FROM tbl_Digitally_Signed_Bill_PVT_For_NCCF AS DS WHERE DS.Bill_Number = '" + txtSearchWHR.Text.Trim() + "' AND DS.Bill_Number IS NOT NULL) AND SB.Bill_Number NOT IN (SELECT RM.JVS_Bill_Number FROM tbl_GdwnRentBill_Detuction_RM AS RM WHERE RM.JVS_Bill_Number = '" + txtSearchWHR.Text.Trim() + "' AND RM.JVS_Bill_Number IS NOT NULL) AND SB.Bill_Number = '" + txtSearchWHR.Text.Trim() + "' UNION ALL SELECT SB.Bill_Number,'Godown Rent Bill' AS Bill_Name,Bill_Type,(SELECT Depositor_Name FROM tbl_MetaData_DEPOSITOR WHERE Depositor_ID = SB.Depositor_Id) AS Depositor_Name,CONVERT(varchar(10), SB.Created_Date, 103) AS DateOfBill,SB.Net_Amount,'NCCF Rent DSC' AS DSC_Source FROM tbl_Institution_NCCF_Storage_Bill_Details AS SB WHERE SB.Branch_Id = '" + ddlDepotList.SelectedValue.ToString() + "' AND SB.Bill_Number IN (SELECT DISTINCT NR.Bill_Number FROM tbl_Digitally_Signed_NCCF_Rent_Bill AS NR WHERE NR.Bill_Number = '" + txtSearchWHR.Text.Trim() + "' AND NR.Bill_Number IS NOT NULL) AND SB.Bill_Number NOT IN (SELECT RM.JVS_Bill_Number FROM tbl_GdwnRentBill_Detuction_RM AS RM WHERE RM.JVS_Bill_Number = '" + txtSearchWHR.Text.Trim() + "' AND RM.JVS_Bill_Number IS NOT NULL) AND SB.Bill_Number = '" + txtSearchWHR.Text.Trim() + "';";
        }

        cmd = new SqlCommand(query, con);
        da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        Session["ds_GridInfo"] = ds;

        if (ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            Btn_Delete.Enabled = true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
            lblRowCount.Text = "Total No. Of records are : 0";
            gv.DataSource = null;
            gv.DataBind();
        }
    }

    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        int totalChecked = 0;
        int deleteSuccessCount = 0;

        try
        {
            if (gv.Rows.Count > 0)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                foreach (GridViewRow gr2 in gv.Rows)
                {
                    CheckBox chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");

                    if (chk_Delete != null && chk_Delete.Checked)
                    {
                        totalChecked++;
                        string Bill_No = Convert.ToString(gv.DataKeys[gr2.RowIndex].Value);
                        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                        if (RadioButton1.Checked) Bill_Type = "SCB";
                        else if (RadioButton2.Checked) Bill_Type = "GRB";
                        else if (RadioButton3.Checked) Bill_Type = "SSB";
                        else if (RadioButton4.Checked) Bill_Type = "NAFED";
                        else if (RadioButton5.Checked) Bill_Type = "NCCF";
                        else if (RadioButton6.Checked) Bill_Type = "NCCFRent";

                        string log_qry = "";

                        if (Bill_Type == "SCB")
                        {
                            log_qry = "insert into tbl_Digitally_Signed_Bill_Details_Log SELECT [Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],[Account_No],[IFSC_Code],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],'" + ip + "',getdate(),[Ref_Bill_No],[Party_Name],[Party_Id],[Bill_Type],'R' FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details] where Ref_Bill_No='" + Bill_No + "'";
                        }
                        else if (Bill_Type == "GRB")
                        {
                            log_qry = "insert into tbl_Digitally_Signed_Bill_PVT_Log SELECT [Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],[Account_No],[IFSC_Code],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No, '" + ip + "',getdate(),'R' FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT] where Bill_Number='" + Bill_No + "'";
                        }
                        else if (Bill_Type == "SSB")
                        {
                            log_qry = "insert into tbl_Digitally_Signed_Bill_SteelSilo_Log SELECT [Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],[Account_No],[IFSC_Code],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],'R',getdate(),Re_Push,Ref_Bill_No,Prev_Bill_No FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_SteelSilo] where Bill_Number='" + Bill_No + "'";
                        }
                        else if (Bill_Type == "NAFED")
                        {
                            log_qry = "insert into tbl_Digitally_Signed_Bill_Details_NAFED_Log SELECT [Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],[Account_No],[IFSC_Code],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],'R',getdate(),Re_Push,Ref_Bill_No,Prev_Bill_No FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details_NAFED] where Bill_Number='" + Bill_No + "'";
                        }
                        else if (Bill_Type == "NCCF")
                        {
                            log_qry = "insert into tbl_Digitally_Signed_Bill_Details_NCCF_log SELECT [Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],[Account_No],[IFSC_Code],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],'R',getdate(), Ref_Bill_No, Party_Name, Party_Id, 'Storage Charges Bill' FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details_NCCF] where Ref_Bill_No='" + Bill_No + "'";
                        }
                        else if (Bill_Type == "NCCFRent")
                        {
                            log_qry = @"INSERT INTO tbl_Digitally_Signed_Bill_PVT_For_NCCF_Log 
                                        SELECT [Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],[Account_No],[IFSC_Code],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],'1',GETDATE(),[Re_Push],[Ref_Bill_No],[Prev_Bill_No] 
                                        FROM tbl_Digitally_Signed_Bill_PVT_For_NCCF WHERE Bill_Number='" + Bill_No + "' OR Ref_Bill_No='" + Bill_No + "'; " +
                                      @"INSERT INTO tbl_Digitally_Signed_NCCF_Rent_Bill_Log 
                                        SELECT [Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],[Account_No],[IFSC_Code],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],'1',GETDATE(),[Re_Push],[Ref_Bill_No],[Prev_Bill_No] 
                                        FROM tbl_Digitally_Signed_NCCF_Rent_Bill WHERE Bill_Number='" + Bill_No + "' OR Ref_Bill_No='" + Bill_No + "';";
                        }

                        cmd = new SqlCommand(log_qry, con);
                        int logResult = cmd.ExecuteNonQuery();

                        // Log insert successful (or processed)
                        if (logResult > 0 || Bill_Type == "NCCFRent")
                        {
                            if (Bill_Type == "SCB")
                            {
                                qry = "delete from tbl_Digitally_Signed_Bill_Details where Ref_Bill_No='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "'";
                            }
                            else if (Bill_Type == "GRB")
                            {
                                qry = "delete from tbl_Digitally_Signed_Bill_PVT where Bill_Number='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "'";
                            }
                            else if (Bill_Type == "SSB")
                            {
                                qry = "delete from tbl_Digitally_Signed_Bill_SteelSilo where Bill_Number='" + Bill_No + "'";
                            }
                            else if (Bill_Type == "NAFED")
                            {
                                qry = "delete from tbl_Digitally_Signed_Bill_Details_NAFED where Bill_Number='" + Bill_No + "'";
                            }
                            else if (Bill_Type == "NCCF")
                            {
                                qry = "delete from tbl_Digitally_Signed_Bill_Details_NCCF where Ref_Bill_No='" + Bill_No + "'";
                            }
                            else if (Bill_Type == "NCCFRent")
                            {
                                qry = "DELETE FROM tbl_Digitally_Signed_Bill_PVT_For_NCCF WHERE Bill_Number='" + Bill_No + "' OR Ref_Bill_No='" + Bill_No + "'; " +
                                      "DELETE FROM tbl_Digitally_Signed_NCCF_Rent_Bill WHERE Bill_Number='" + Bill_No + "' OR Ref_Bill_No='" + Bill_No + "';";
                            }

                            cmd = new SqlCommand(qry, con);
                            int deleteResult = cmd.ExecuteNonQuery();

                            if (deleteResult > 0)
                            {
                                deleteSuccessCount++;
                            }
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
                return;
            }

            if (totalChecked == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast One Record For Deleting')", true);
            }
            else if (deleteSuccessCount > 0)
            {
                // 👇 Targeted Success Alert Requested
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Bill DSC deleted successfully!');", true);
                FillGrid(); // Grid Refresh after deletion
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in deleting Bill DSC from storage/log.')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message.Replace("'", "") + "')", true);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }

    protected void btnSerachWHR_Click(object sender, EventArgs e)
    {
        if (txtSearchWHR.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Bill No')", true);
        }
        else
        {
            FillGrid();
        }
    }
}