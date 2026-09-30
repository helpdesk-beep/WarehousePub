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

public partial class Accounting_Delete_Bill_DSign : System.Web.UI.Page
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
            //string region = Session["Region_ID"].ToString();
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
                //ddlGodown.DataSource = null;
                //ddlGodown.DataBind();
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
        if (RadioButton1.Checked == true)
        {
            Bill_Type ="SCB";
        }
        else if (RadioButton2.Checked == true)
        {
            Bill_Type = "GRB";
        }

        if (Bill_Type == "SCB")
        {
             //query = "select SB.Bill_Number,case when SB.Bill_Type='AD' then 'Daily Storage Charges Bill' when SB.Bill_Type='AU' then 'Accrued Storage Charges Bill' when SB.Bill_Type='OD' then 'Daily Over & Above Storage Charges Bill' when SB.Bill_Type='HG' then 'Godown(Hired) Rent Bill' when SB.Bill_Type='GR' then 'Godown(JVS) Rent Bill' when SB.Bill_Type='OU' then 'Accrued Over & Above Storage Charges Bill' when SB.Bill_Type='RB' then 'Reservation Bill' else '' end as Bill_Name,Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount from tbl_Institution_Storage_Bill_Details as SB where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and SB.Bill_Number not in (select DS.Ref_Bill_No from tbl_Digitally_Signed_Bill_Details as DS where DS.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and DS.Ref_Bill_No is not null)";
            query = "select SB.Bill_Number,'Storage Charges Bill' as Bill_Name,SB.Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount,DSB.DSC_Holder_Name,DSB.DSC_Serial_No from tbl_Institution_Storage_Bill_Details as SB inner join tbl_Digitally_Signed_Bill_Details as DSB on DSB.Ref_Bill_No=SB.Bill_Number where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and SB.Bill_Number in (select DS.Ref_Bill_No from tbl_Digitally_Signed_Bill_Details as DS where DS.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and DS.Ref_Bill_No is not null) and SB.Bill_Number not in (select RM.Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM as RM where RM.BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and RM.Ref_Bill_Number is not null) and SB.Bill_Number not in (select CDS.Ref_Bill_No from MPSCSC.dbo.Digitally_Sign_StorageBill_IC as CDS where Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and CDS.Ref_Bill_No is not null) and SB.Bill_Number='" + txtSearchWHR.Text + "'";
        }
        else if (Bill_Type == "GRB")
        {
            //query = "select SB.Bill_Number,case when SB.Bill_Type='AD' then 'Daily Storage Charges Bill' when SB.Bill_Type='AU' then 'Accrued Storage Charges Bill' when SB.Bill_Type='OD' then 'Daily Over & Above Storage Charges Bill' when SB.Bill_Type='HG' then 'Godown(Hired) Rent Bill' when SB.Bill_Type='GR' then 'Godown(JVS) Rent Bill' when SB.Bill_Type='OU' then 'Accrued Over & Above Storage Charges Bill' when SB.Bill_Type='RB' then 'Reservation Bill' else '' end as Bill_Name,Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount from tbl_Institution_Storage_Bill_Details as SB where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and SB.Bill_Number not in (select DS.Ref_Bill_No from tbl_Digitally_Signed_Bill_Details as DS where DS.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and DS.Ref_Bill_No is not null)";
            query = "select SB.Bill_Number,'Godown Rent Bill' as Bill_Name,Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount from tbl_Institution_Storage_Bill_Details as SB where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and SB.Bill_Number in (select distinct DS.Bill_Number from tbl_Digitally_Signed_Bill_PVT as DS where DS.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and DS.Bill_Number is not null) and SB.Bill_Number not in (select RM.Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM as RM where RM.BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and RM.Ref_Bill_Number is not null) and SB.Bill_Number='" + txtSearchWHR.Text + "'";

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
            lblRowCount.Text = "";
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            Btn_Delete.Enabled = true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Bill Data Found...')", true);
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
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
        //string Stackid = "";
        int count = 0;
        try
        {
            if (gv.Rows.Count > 0)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //int i = 0;
                foreach (GridViewRow gr2 in gv.Rows)
                {
                    DataSet ds = (DataSet)Session["ds_GridInfo"];
                    CheckBox chk_Delete = new CheckBox();
                    string Bill_No = Convert.ToString(gv.DataKeys[gr2.RowIndex].Value);
                    string Bill_Type = gv.Rows[gr2.RowIndex].Cells[2].Text.ToString();
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                    if (chk_Delete.Checked == true)
                    {
                        ds = (DataSet)Session["ds_GridInfo"];
                        foreach (DataRow drs in ds.Tables[0].Select("Bill_Number = '" + Bill_No + "'"))
                        {
                            {
                                ////////////////tbl_Storage_Bill_Details_Log/////////////////////////
                                string log_qry = "";
                                if (RadioButton1.Checked == true)
                                   {
                                   Bill_Type ="SCB";
                                    }
                                  else if (RadioButton2.Checked == true)
                                   {
                                   Bill_Type = "GRB";
                                   }
                                //string log_qry = "insert into tbl_Institution_Storage_Bill_Details_Log select Sno,[Bill_Number],[District_Id],[Branch_Id],[Depositor_Type_Id],[Depositor_Id],[Commodity_Type_Id],[Commodity_Id],[From_Date],[To_Date],[Packing_Type],[Weight],[Financial_Year],[Commodity_Rate],[Net_Amount],[Sub_Amount],[Service_Tax_Perc],[Service_Tax_Amt],[Depositor_Category],[Rebate_Perc],[Rebate_Amt],[Rebate_on_Unit],[Khasra_Number],[Rin_Pustika_No],[Cast_Certificate_No],[Is_Rebate],[Created_Date],[Modified_Date],[Client_IP],[Per_Day_Rate],[Bill_Type],[BId],[Month],[Godown_Id],[Crop_Year],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[BO_Approval_Status],[BO_Approval_Date],[BO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[HO_Approval_Status],[HO_Approval_Date],[HO_Approval_IP] from tbl_Institution_Storage_Bill_Details where Bill_Number='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "' and BO_Approval_Status is null";
                                if (Bill_Type == "SCB")
                                {
                                    log_qry = "insert into tbl_Digitally_Signed_Bill_Details_Log SELECT [Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],[Account_No],[IFSC_Code],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],'" + ip + "',getdate(),[Ref_Bill_No],[Party_Name],[Party_Id],[Bill_Type],'S' FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details] where Ref_Bill_No='" + Bill_No + "'";
                                }
                                else if (Bill_Type == "GRB")
                                {
                                    //log_qry = "insert into tbl_Digitally_Signed_Bill_PVT_Log SELECT [Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],[Account_No],[IFSC_Code],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],'" + ip + "',getdate() FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT] where Bill_Number='" + Bill_No + "'";
                                    log_qry = "insert into tbl_Digitally_Signed_Bill_PVT_Log SELECT [Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],[Account_No],[IFSC_Code],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No, '" + ip + "',getdate(),'S' FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT] where Bill_Number='" + Bill_No + "'";

                                }
                                cmd = new SqlCommand(log_qry, con);
                                int s = cmd.ExecuteNonQuery();
                                ////////////////tbl_Storage_Bill_Details_Log/////////////////////////
                                if (s > 0)
                                {
                                    if (Bill_Type == "SCB")
                                    {
                                        //qry = "Delete from tbl_Digitally_Signed_Bill_Details where Bill_Number='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "' and BO_Approval_Status is null";
                                        qry = "delete from tbl_Digitally_Signed_Bill_Details where Ref_Bill_No='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "'";

                                    }
                                    else if (Bill_Type == "GRB")
                                    {
                                        //qry = "Delete from tbl_Institution_Storage_Bill_Details where Bill_Number='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "' and BO_Approval_Status is null";
                                        qry = "delete from tbl_Digitally_Signed_Bill_PVT where Bill_Number='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "'";
                                    }
                                    cmd = new SqlCommand(qry, con);
                                    int d = cmd.ExecuteNonQuery();
                                    if (d > 0)
                                    {
                                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in Log Storage')", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in Log Storage')", true);
                                }
                            }
                        }
                        count++;
                    }
                    //i = i + 1;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully deleted')", true);
                FillGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast One Record For Deleting')", true);
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
