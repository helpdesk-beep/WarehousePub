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

public partial class Accounting_Delete_RO_DSC : System.Web.UI.Page
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
           
                    if (!IsPostBack)
                    {
                        fillDistrict();
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
            //string region = "";
            //if (Session["UserName"].ToString() != "MPSWLC")
            //{

            //    if (Session["Region_ID"].ToString() != null)
            //    {
            //        region = Session["Region_ID"].ToString();

            //    }
            //}
            string query = "";            
            query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
                      
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

        //Bill_Type = "SCB";
        //query = "select  Ref_Bill_No,(select Godown_Name from tbl_MetaData_GODOWN_2018 where Godown_Id=RPO.Godown_Id) as Godown,Godown_Id,(select Commodity_Name from  tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=RPO.Commodity_Id) as Commodity,Net_Amount,Month_No,party_Name,Account_No,IFSC_Code,Crop_Year,Financial_Year,DSC_Holder_Name as RM,DSC_Holder_Name_RAM as Acc_Mgr from tbl_Digitally_Signed_Bill_RPO as RPO where District_Id='" + ddlDistrict.SelectedValue.ToString() + "' and Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and Party_Name='" + ddlParty.SelectedItem.Text + "'";
        //query = "select  Ref_Bill_No,(select Godown_Name from tbl_MetaData_GODOWN_2018 where Godown_Id=RPO.Godown_Id) as Godown,RPO.Godown_Id,(select Commodity_Name from  tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=RPO.Commodity_Id) as Commodity,Net_Amount,Month_No,RPO.Party_Name,RPO.Account_No,RPO.IFSC_Code,Crop_Year,Financial_Year,DSC_Holder_Name as RM,DSC_Holder_Name_RAM as Acc_Mgr from tbl_Digitally_Signed_Bill_RPO as RPO LEFT JOIN tbl_Payment_Credit AS PC ON RPO.Ref_Bill_No=PC.Bill_No where PC.Bill_No is null AND RPO.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and RPO.Party_Name='" + ddlParty.SelectedItem.Text + "'";
        query = "select  Ref_Bill_No,(select Godown_Name from tbl_MetaData_GODOWN_2018 where Godown_Id=RPO.Godown_Id) as Godown,RPO.Godown_Id,(select Commodity_Name from  tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=RPO.Commodity_Id) as Commodity,Net_Amount,Month_No,RPO.Party_Name,RPO.Account_No,RPO.IFSC_Code,Crop_Year,Financial_Year,DSC_Holder_Name as RM,DSC_Holder_Name_RAM as Acc_Mgr,CASE WHEN PC.Bill_No is not null then 'NEFT File is Generated' else '' END AS NEFT from tbl_Digitally_Signed_Bill_RPO as RPO LEFT JOIN tbl_Payment_Credit AS PC ON RPO.Ref_Bill_No=PC.Bill_No where RPO.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and RPO.Party_Name='" + ddlParty.SelectedItem.Text + "'";

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
            Btn_Delete.Visible = true;
            btn_Close.Visible = true;
            lblRowCount.Visible = true;
            lbl_notfound.Visible = true;
            txtSearch.Visible = true;
            panelContainer.Visible = true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Allready NEFT is Generated...')", true);
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
                        foreach (DataRow drs in ds.Tables[0].Select("Ref_Bill_No = '" + Bill_No + "'"))
                        {
                            //{
                                string log_qry = "";
                                   Bill_Type = "SCB";
                                    //log_qry = "insert into tbl_Digitally_Signed_Bill_PVT_Log SELECT [Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],[Account_No],[IFSC_Code],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],'" + ip + "',getdate() FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT] where Bill_Number='" + Bill_No + "'";
                                    //log_qry = "insert into tbl_Digitally_Signed_Bill_RPO_Log select [Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[MPWLC_SC],[GST_Perc_SC],[GST_Amt_SC],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],[Account_No],[IFSC_Code],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],'"+ ip +"',Getdate(),[Ref_Bill_No],[Party_Name],[Party_Id],[MPWLC_Amt],[TDS_Amt],[Deduction_Amt],[DSC_Serial_No_RAM],[DSC_Holder_Name_RAM],[Client_Ip_RAM],[DSC_User_Type_RAM],[Bill_Check_Sum_RAM],[CreatedDate_RAM],[CreatedBy_RAM],[Re_Push] from tbl_Digitally_Signed_Bill_RPO where Ref_Bill_No='" + Bill_No + "' and Branch_Id='"+ ddlDepotList.SelectedValue +"' and District_Id='"+ ddlDistrict.SelectedValue +"'";
                                    log_qry = "insert into tbl_Digitally_Signed_Bill_RPO_Log select [Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[MPWLC_SC],[GST_Perc_SC],[GST_Amt_SC],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],[Account_No],[IFSC_Code],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],'"+ ip +"',Getdate(),[Ref_Bill_No],[Party_Name],[Party_Id],[MPWLC_Amt],[TDS_Amt],[Deduction_Amt],[DSC_Serial_No_RAM],[DSC_Holder_Name_RAM],[Client_Ip_RAM],[DSC_User_Type_RAM],[Bill_Check_Sum_RAM],[CreatedDate_RAM],[CreatedBy_RAM],[Re_Push] from tbl_Digitally_Signed_Bill_RPO where Ref_Bill_No='" + Bill_No + "' and Branch_Id='"+ ddlDepotList.SelectedValue +"'";
                                cmd = new SqlCommand(log_qry, con);
                                int s = cmd.ExecuteNonQuery();
                                if (s > 0)
                                {
                                    //qry = "delete from tbl_Digitally_Signed_Bill_Details where Ref_Bill_No='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "'";
                                    //qry = "delete from tbl_Digitally_Signed_Bill_RPO where Ref_Bill_No='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "' and District_Id='" + ddlDistrict.SelectedValue + "'";
                                qry = "delete from tbl_Digitally_Signed_Bill_RPO where Ref_Bill_No='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "'";
                                    cmd = new SqlCommand(qry, con);
                                    int d = cmd.ExecuteNonQuery();
                                    if (d > 0)
                                    {
                                    FillGrid();
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully deleted..')", true);
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
        if (ddlDistrict.SelectedIndex== 0 || ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District')", true);
        }
        else if (ddlDepotList.SelectedIndex == 0 || ddlDepotList.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Branch')", true);
        }
        else if (ddlParty.SelectedIndex == 0 || ddlParty.SelectedIndex <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Select Beneficiary Name')", true);
        }
        else
        {
            FillGrid();
        }
    }
    public void Beneficiary_List()
    {
        
        ddlParty.Items.Clear();
        //qry = "select distinct Party_Name from tbl_Digitally_Signed_Bill_RPO where District_Id='" + ddlDistrict.SelectedValue + "' and Branch_Id='" + ddlDepotList.SelectedValue + "'";
        qry = "select distinct Party_Name from tbl_Digitally_Signed_Bill_RPO where Branch_Id='" + ddlDepotList.SelectedValue + "'";

        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlParty.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlParty.DataSource = ds.Tables[0];
            ddlParty.DataTextField = "Party_Name";
            ddlParty.DataValueField = "Party_Name";
            ddlParty.DataBind();
            ddlParty.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlDepotList_SelectedIndexChanged1(object sender, EventArgs e)
    {
        Beneficiary_List();
    }
}