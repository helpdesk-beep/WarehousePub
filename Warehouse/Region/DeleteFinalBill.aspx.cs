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
using System.Xml;
using System.Text;
using MPSCSC_WS;

public partial class Region_DeleteFinalBill : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    SqlTransaction sqltran;

    MPSCSC_WS.MPSCSC_InstituitionStorageBillDetails MPSCSCDemo = new MPSCSC_WS.MPSCSC_InstituitionStorageBillDetails();
    //CSMS_WS.MPSCSC_InstituitionStorageBillDetails MPSCSCDemo = new CSMS_WS.MPSCSC_InstituitionStorageBillDetails();
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
                        //string strMsg = "यहाँ सुविधा कुछ दिनों के लिए सॉफ्टवेयर में कार्य होने कारण बंद कर दी गई हैं |||";


                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Welcome.aspx';", true);
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
        //string query = "select SB.Bill_Number,case when SB.Bill_Type='AD' then 'Daily Storage Charges Bill' when SB.Bill_Type='AU' then 'Accrued Storage Charges Bill' when SB.Bill_Type='OD' then 'Daily Over & Above Storage Charges Bill' when SB.Bill_Type='HG' then 'Godown(Hired) Rent Bill' when SB.Bill_Type='GR' then 'Godown(JVS) Rent Bill' when SB.Bill_Type='OU' then 'Accrued Over & Above Storage Charges Bill' when SB.Bill_Type='RB' then 'Reservation Bill' else '' end as Bill_Name,Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount from tbl_Storage_Bill_Details as SB where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "'";
        string query = "select SB.Bill_Number,SB.Branch_Id,SB.District_Id,case when SB.Bill_Type = '1' then 'Daily Storage Charges Bill' when SB.Bill_Type = '2' then 'Accrued Storage Charges Bill' when SB.Bill_Type = '3' then 'Daily Over & Above Storage Charges Bill' when SB.Bill_Type = '4' then 'Godown(Hired) Rent Bill' when SB.Bill_Type = '5' then 'Godown(JVS) Rent Bill' when SB.Bill_Type = '6' then 'Accrued Over & Above Storage Charges Bill' when SB.Bill_Type = '7' then 'Reservation Bill' else '' end as Bill_Name, Bill_Type, (select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID = SB.Depositor_Id) as Depositor_Name, CONVERT(varchar(10), SB.Created_Date, 103) as DateOfBill,SB.Net_Amount from[tbl_Institution_Storage_Bill_Summary] as SB  where SB.Branch_Id = '" + ddlDepotList.SelectedValue.ToString() + "'";

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
    public bool CheckIC(string BillNumber)
    {
        bool IsValid = true;
        //string query = "select COUNT(1) IsCount from MPSCSC.dbo.Digitally_Sign_StorageBill_IC WHERE Ref_Bill_No='" + BillNumber + "'";
        string query = "select COUNT(1) IsCount from MPSCSC.dbo.tbl_Processing_StorageBill_AtDM WHERE Ref_Bill_No='" + BillNumber + "'";

        cmd = new SqlCommand(query, con);
        da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["IsCount"].ToString() != "0")
                IsValid = false;
            else IsValid = true;
        }
        else
        {
            IsValid = true;
        }
        return IsValid;
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
            if (validate())
            {
                foreach (GridViewRow row in gv.Rows)
                {
                    CheckBox chk_Delete = (CheckBox)(row.FindControl("chk_Delete"));
                    HiddenField hdnBillNo = (HiddenField)(row.FindControl("hdnBillNo"));
                    HiddenField hdnBranchId = (HiddenField)(row.FindControl("hdnBranchId"));
                    HiddenField hdnDistrictId = (HiddenField)(row.FindControl("hdnDistrictId"));
                    if (chk_Delete.Checked == true)
                    {
                        if (CheckIC(hdnBillNo.Value))
                        {
                            con.Open();
                            cmd = new SqlCommand("dbo.Delete_tbl_Institution_Storage_Bill_Summary", con, sqltran);
                            //cmd = new SqlCommand("dbo.Delete_tbl_Institution_Storage_Bill_Summary_Test", con, sqltran);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Bill_Number", hdnBillNo.Value);
                            cmd.Parameters.AddWithValue("@District_Id", hdnDistrictId.Value);
                            cmd.Parameters.AddWithValue("@Branch_Id", hdnBranchId.Value);
                            cmd.Parameters.AddWithValue("@IPAddress", Request.UserHostAddress);
                            cmd.Parameters.AddWithValue("@DeleteFlag", "R");
                            int res = cmd.ExecuteNonQuery();
                            if (res > 0)
                            {
                                count++;
                                {
                                    string Bill_No = hdnBillNo.Value.ToString();
                                    string Dist_id = hdnDistrictId.Value.ToString();
                                    string BranchID = hdnBranchId.Value.ToString();
                                    string ip = Request.UserHostAddress.ToString();

                                    //MPSCSCDemo.EDDeleteFinalInstituitionStorageBillSummary(myUri);
                                    //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                                    //Web Service Call For data to MPSCSC
                                    System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
                                    MPSCSCDemo.EDDeleteFinalBill(Bill_No, Dist_id, BranchID, ip);

                                }
                            }
                            
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('DM MPSCSC द्वारा Bill Verify कर दिया गया है, अतः इस बिल " + hdnBillNo.Value + " को डिलीट नहीं  कर सकते है| पहले DM MPSCSC से डिलीट कराये।')", true);
                        }
                    }
                }
                if (count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record deleted successfully..')", true);
                    FillGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record NOT deleted')", true);
                }
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
    public Boolean validate()
    {
        int chkstatus = 0;
        int iCount = 0;
        string BillNo = "";
        foreach (GridViewRow row in gv.Rows)
        {
            CheckBox chk_Delete = (CheckBox)(row.FindControl("chk_Delete"));
            HiddenField hdnBillNo = (HiddenField)(row.FindControl("hdnBillNo"));
            BillNo = hdnBillNo.Value;
            if (chk_Delete.Checked == true)
            {
                if (CheckIC(hdnBillNo.Value))
                {
                    chkstatus = chkstatus + 1;
                }
                iCount++;
            }
        }
        if (iCount > 0)
        {
            if (chkstatus > 0)
            {
                return true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('MPSCSC द्वारा डिजिटल Sign कर दिया गया अतः इस बिल " + BillNo + " को डिलीट नहीं  कर सकते है')", true);
                return false;
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया कम से कम एक चेकबॉक्स चेक करे ')", true);
            return false;
        }
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
}