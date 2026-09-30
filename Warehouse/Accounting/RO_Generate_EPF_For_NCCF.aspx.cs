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
using System.Globalization;
using System.IO;
using System.Text;

public partial class Accounting_RO_Generate_EPF_For_NCCF : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    string Bill_Type = "";
    string Ref_Number = "";
    string Ref_Aid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                fillDistrict();
                fillMonth();
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
                //gv.DataSource = null;
                //gv.DataBind();
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
    public void GetBranch()
    {
        string qry = "";
        qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDepotList.DataSource = ds.Tables[0];
            ddlDepotList.DataTextField = "DepotName";
            ddlDepotList.DataValueField = "BranchId";
            ddlDepotList.DataBind();
            ddlDepotList.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex != 0)
        {
            GetBranch();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District')", true);
        }
    }
    private void GetBillsDetail()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_JVS_Godown_Details_For_Generate_NEFT_For_NCCF", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@DistictID", ddlDistrict.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@BranchID", ddlDepotList.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Beneficiary_Type", ddlBankType.SelectedValue.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            gvBOBillApp.DataSource = dt;
                            gvBOBillApp.DataBind();
                            trnewproc.Visible = true;
                            tblbtn.Visible = true;
                            lblNoofAC.Text = dt.Rows.Count.ToString();
                            btn_save.Enabled = true;
                        }
                        else
                        {

                            gvBOBillApp.DataSource = null;
                            gvBOBillApp.DataBind();
                            trnewproc.Visible = false;
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data Found'); </script> ");

                        }
                    }
                }
            }
        }
    }
    //private void GetBillsDetail()
    //{
    //    try
    //    {

    //        string Dist_id = ddlDistrict.SelectedValue.ToString();
    //        string Branch_Id = ddlDepotList.SelectedValue.ToString();
    //        string str = "";
    //        if (ddlBankType.SelectedValue == "S")
    //        {
    //            //str = "select Ref_Bill_No,RPO.Account_No,RPO.IFSC_Code,CONVERT(varchar(10),GETDATE(),103) as Transaction_Date,Net_Amount,Party_Name,Godown_Id,(select G.Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=RPO.Godown_Id) as Godown_Name,B.Beneficiary_Id as Beneficiary_Id from tbl_Digitally_Signed_Bill_RPO as RPO inner join tbl_Beneficiary_Account_Details as B on B.Account_No=RPO.Account_No where Net_Amount>0 and B.Beneficiary_Type='S' and RPO.District_Id='" + Dist_id + "' and RPO.Branch_Id='" + Branch_Id + "' and RPO.Month_No='"+ ddlmonth.SelectedValue +"'";
    //            str = "select RPO.Financial_Year,RPO.Crop_Year,RPO.Ref_Bill_No,RPO.Account_No,RPO.IFSC_Code,CONVERT(varchar(10),GETDATE(),103) as Transaction_Date,RPO.Net_Amount,Party_Name,RPO.Godown_Id,(select G.Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=RPO.Godown_Id AND Hired_Type not in('Tribal Scheme')) as Godown_Name,B.Beneficiary_Id as Beneficiary_Id from tbl_Digitally_Signed_Bill_RPO as RPO inner join tbl_Beneficiary_Account_Details as B on B.Account_No=RPO.Account_No inner join tbl_GdwnRentBill_Detuction_RM as RMD on RMD.JVS_Bill_Number=RPO.Ref_Bill_No inner join mpscsc.dbo.StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020 as CR on CR.Bill_Number=RMD.Ref_Bill_Number where RPO.Net_Amount>0 and B.Beneficiary_Type='S' and RPO.District_Id='" + Dist_id + "' and RPO.Branch_Id='" + Branch_Id + "' and RPO.Month_No='" + ddlmonth.SelectedValue + "' and RPO.Ref_Bill_No not in (select C.Bill_No as a from tbl_Payment_Credit as C where C.Branch_Id='" + Branch_Id + "')";

    //        }
    //        else if (ddlBankType.SelectedValue == "O")
    //        {
    //            //str = "select Ref_Bill_No,RPO.Account_No,RPO.IFSC_Code,CONVERT(varchar(10),GETDATE(),103) as Transaction_Date,Net_Amount,Party_Name,Godown_Id,(select G.Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=RPO.Godown_Id) as Godown_Name,B.Beneficiary_Id as Beneficiary_Id from tbl_Digitally_Signed_Bill_RPO as RPO inner join tbl_Beneficiary_Account_Details as B on B.Account_No=RPO.Account_No where Net_Amount>0 and B.Beneficiary_Type='O' and RPO.District_Id='" + Dist_id + "' and RPO.Branch_Id='" + Branch_Id + "' and RPO.Month_No='" + ddlmonth.SelectedValue + "'";
    //            str = "select RPO.Financial_Year,RPO.Crop_Year,RPO.Ref_Bill_No,RPO.Account_No,RPO.IFSC_Code,CONVERT(varchar(10),GETDATE(),103) as Transaction_Date,RPO.Net_Amount,Party_Name,RPO.Godown_Id,(select G.Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=RPO.Godown_Id AND Hired_Type not in('Tribal Scheme')) as Godown_Name,B.Beneficiary_Id as Beneficiary_Id from tbl_Digitally_Signed_Bill_RPO as RPO inner join tbl_Beneficiary_Account_Details as B on B.Account_No=RPO.Account_No inner join tbl_GdwnRentBill_Detuction_RM as RMD on RMD.JVS_Bill_Number=RPO.Ref_Bill_No inner join mpscsc.dbo.StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020 as CR on CR.Bill_Number=RMD.Ref_Bill_Number where RPO.Net_Amount>0 and B.Beneficiary_Type='O' and RPO.District_Id='" + Dist_id + "' and RPO.Branch_Id='" + Branch_Id + "' and RPO.Month_No='" + ddlmonth.SelectedValue + "' and RPO.Ref_Bill_No not in (select C.Bill_No as a from tbl_Payment_Credit as C where C.Branch_Id='" + Branch_Id + "')";

    //        }

    //        SqlDataAdapter da = new SqlDataAdapter(str, con);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            gvBOBillApp.DataSource = ds.Tables[0];
    //            gvBOBillApp.DataBind();
    //            trnewproc.Visible = true;
    //            tblbtn.Visible = true;
    //            lblNoofAC.Text = ds.Tables[0].Rows.Count.ToString();
    //            btn_save.Enabled = true;
    //            //pnlCofirmmsg.Visible = false;
    //            //Panel2.Visible = false;
    //            //btn_Sbi_Link.Visible = false;

    //        }
    //        else
    //        {
    //            gvBOBillApp.DataSource = null;
    //            gvBOBillApp.DataBind();
    //            trnewproc.Visible = false;
    //            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data Found'); </script> ");


    //        }

    //    }

    //    catch (Exception ex)
    //    {

    //    }
    //}
    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillsDetail();
        lblRespMsg.Text = "";
        lblRespMsgNo.Text = "";
        lblRespMsg.Visible = false;
        lblRespMsgNo.Visible = false;
        btn_Sbi_Link.Visible = false;
        btnDownload.Visible = false;
        btnDownload.Enabled = false;
        btn_save.Visible = true;
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (hdnLabelState.Value.ToString() == "0" || hdnLabelState.Value.ToString() == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please select at least one Record'); </script> ");
        }
        else
        {
            Insert_Bill_Detail();
            //btn_Sbi_Link.Visible = true;
            btn_Close.Text = "New";
        }
    }
    public void Insert_Bill_Detail()
    {
        try
        {
            string Rent_Bill_No = "";
            string Godown_ID = "";
            //string Trans_Id = "";
            string Account_No = "";
            string IFSC_Code = "";
            string Branch_Code = "";
            string Party_Name = "";
            //DateTime Trans_Date = new DateTime();
            decimal Debit_Amount = 0;
            decimal Credit_Amount = 0;
            string Trans_Description = "Rent Payment to Godowners";
            string Payment_Identifier = "NEFT";
            string Trans_Id = "";
            string Mobile_No = "";
            string Account_Type = "";
            string Amount_Type = "";
            string Created_By = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string Beneficiary_Id = "";
            string qry = "";
            string Dist_id = ddlDistrict.SelectedValue.ToString();
            string BranchID = ddlDepotList.SelectedValue.ToString();
            string RegionID = Session["Region_ID"].ToString();
            string AID = "";
            GetReferenceNo();
            foreach (GridViewRow row in gvBOBillApp.Rows)
            {
                CheckBox ChkBoxHeader = (CheckBox)gvBOBillApp.HeaderRow.FindControl("chkBxHeader");
                HiddenField hdnParty_Name = (HiddenField)row.FindControl("hdnParty_Name");
                HiddenField hdnAccount = (HiddenField)row.FindControl("hdnAccount");
                HiddenField hdnRef_Bill_No = (HiddenField)row.FindControl("hdnRef_Bill_No");
                HiddenField hdnIFSC_Code = (HiddenField)row.FindControl("hdnIFSC_Code");
                HiddenField hdnGodown_Id = (HiddenField)row.FindControl("hdnGodown_Id");
                HiddenField hdnBeneficiary_Id = (HiddenField)row.FindControl("hdnBeneficiary_Id");
                CheckBox chkbox = (CheckBox)row.FindControl("chk_Sum");

                if (chkbox.Checked == true)
                {
                    //Rent_Bill_No = row.Cells[0].Text;
                    //Godown_ID = row.Cells[8].Text;
                    //Trans_Id = "";
                    //Account_No = row.Cells[1].Text;
                    //IFSC_Code = row.Cells[2].Text;
                    //Branch_Code = "";
                    //Party_Name = row.Cells[5].Text;
                    //TextBox TotalC1 = (TextBox)row.FindControl("txtcharges");
                    //Credit_Amount =  Convert.ToDecimal(TotalC1.Text);
                    //Beneficiary_Id = row.Cells[6].Text;
                    Rent_Bill_No = hdnRef_Bill_No.Value;
                    Godown_ID = hdnGodown_Id.Value;
                    Trans_Id = "";
                    Account_No = hdnAccount.Value;
                    IFSC_Code = hdnIFSC_Code.Value;
                    Branch_Code = "";
                    Party_Name = hdnParty_Name.Value;
                    TextBox TotalC1 = (TextBox)row.FindControl("txtcharges");
                    Credit_Amount = Convert.ToDecimal(TotalC1.Text);
                    Beneficiary_Id = hdnBeneficiary_Id.Value;
                    Trans_Description = Party_Name + Beneficiary_Id + "GRP" + Rent_Bill_No;
                    Get_TransID();
                    Trans_Id = ViewState["Trans_Id"].ToString();
                    AID = ViewState["BID"].ToString();
                    Account_Type = "Credit";
                    Amount_Type = "C";
                    Debit_Amount = Debit_Amount + Credit_Amount;

                    //qry = "INSERT INTO [dbo].[tbl_Payment_Credit] ([Trans_Id] ,[Region_Id] ,[District_Id] ,[Branch_Id] ,[Account_No] ,[IFSC_Code] ,[Branch_Code] ,[Party_Name] ,[Date] ,[Credit_Amount] ,[Trans_Description] ,[Payment_Identifier] ,[Reference_No] ,[Mobile_No] ,[Account_Type] ,[Amount_Type] ,[Created_By] ,[Created_Date] ,[Is_Download] ,[Download_By] ,[Download_Date] ,[Status] ,[Beneficiary_Id] ,[Bill_No] ,[Godown_Id] ,[Aid]) VALUES ('" + Trans_Id + "' ,'" + RegionID + "' ,'" + Dist_id + "' ,'" + BranchID + "' ,'" + Account_No + "','" + IFSC_Code + "' ,'" + IFSC_Code + "' ,'" + Party_Name + "',GetDate() ,'" + Credit_Amount + "'  ,'" + Trans_Description + "' ,'" + Payment_Identifier + "' ,'" + Ref_Number + "'  ,'" + Mobile_No + "'  ,'" + Account_Type + "'  ,'" + Amount_Type + "'  ,'" + Created_By + "' ,Getdate() ,'','' ,'' ,'' ,'' ,'" + Rent_Bill_No + "','" + Godown_ID + "' ,'" + AID + "')";
                    //qry = "INSERT INTO [dbo].[tbl_Payment_Credit] ([Trans_Id] ,[Region_Id] ,[District_Id] ,[Branch_Id] ,[Account_No] ,[IFSC_Code] ,[Branch_Code] ,[Party_Name] ,[Date] ,[Credit_Amount] ,[Trans_Description] ,[Payment_Identifier] ,[Reference_No] ,[Mobile_No] ,[Account_Type] ,[Amount_Type] ,[Created_By] ,[Created_Date] ,[Is_Download] ,[Download_By] ,[Download_Date] ,[Status] ,[Beneficiary_Id] ,[Bill_No] ,[Godown_Id] ,[Aid]) VALUES ('" + Trans_Id + "' ,'" + RegionID + "' ,'" + Dist_id + "' ,'" + BranchID + "' ,'" + Account_No + "','" + IFSC_Code + "' ,'" + IFSC_Code + "' ,'" + Party_Name + "',GetDate() ,'" + Credit_Amount + "'  ,'" + Trans_Description + "' ,'" + Payment_Identifier + "' ,'" + Ref_Number + "'  ,'" + Mobile_No + "'  ,'" + Account_Type + "'  ,'" + Amount_Type + "'  ,'" + Created_By + "' ,Getdate() ,'','' ,'' ,'' ,'"+ Beneficiary_Id +"' ,'" + Rent_Bill_No + "','" + Godown_ID + "' ,'" + AID + "')";
                    qry = "INSERT INTO [dbo].[tbl_Payment_Credit_NCCF] ([Trans_Id] ,[Region_Id] ,[District_Id] ,[Branch_Id] ,[Account_No] ,[IFSC_Code] ,[Branch_Code] ,[Party_Name] ,[Date] ,[Credit_Amount] ,[Trans_Description] ,[Payment_Identifier] ,[Reference_No] ,[Mobile_No] ,[Account_Type] ,[Amount_Type] ,[Created_By] ,[Created_Date],[Status] ,[Beneficiary_Id] ,[Bill_No] ,[Godown_Id] ,[Aid],[Depositor_Type]) VALUES ('" + Trans_Id + "' ,'" + RegionID + "' ,'" + Dist_id + "' ,'" + BranchID + "' ,'" + Account_No + "','" + IFSC_Code + "' ,'" + IFSC_Code + "' ,'" + Party_Name + "',GetDate() ,'" + Credit_Amount + "'  ,'" + Trans_Description + "' ,'" + Payment_Identifier + "' ,'" + Ref_Number + "'  ,'" + Mobile_No + "'  ,'" + Account_Type + "'  ,'" + Amount_Type + "'  ,'" + Created_By + "' ,Getdate() ,'' ,'" + Beneficiary_Id + "' ,'" + Rent_Bill_No + "','" + Godown_ID + "' ,'" + AID + "','15478')";

                    SqlCommand cmd2 = new SqlCommand(qry, con);
                    con.Open();
                    int i = cmd2.ExecuteNonQuery();
                    con.Close();
                }
                chkbox.Enabled = false;
                ChkBoxHeader.Enabled = false;
            }
            //Debit
            Ref_Aid = ViewState["RID"].ToString();
            //qry = "INSERT INTO [dbo].[tbl_Payment_Debit] ([Reference_No] ,[Region_Id] ,[District_Id] ,[Branch_Id] ,[Account_No] ,[IFSC_Code] ,[Branch_Code] ,[Party_Name] ,[Date] ,[Debit_Amount] ,[Trans_Description] ,[Payment_Identifier] ,[Mobile_No] ,[Account_Type] ,[Amount_Type] ,[Created_By] ,[Created_Date],[Aid]) VALUES ('" + Ref_Number + "' ,'" + RegionID + "' ,'" + Dist_id + "' ,'" + BranchID + "' ,'38906616113','SBIN0030343' ,'SBIN0030343' ,'Madhya Pradesh Warehousing and Logistics Corporation',GetDate() ,'" + Debit_Amount + "' ,'Godown Rent Payment' ,'" + Payment_Identifier + "'  ,''  ,'Debit'  ,'D'  ,'" + Created_By + "' ,Getdate(),'"+ Ref_Aid + "')";
            qry = "INSERT INTO [dbo].[tbl_Payment_Debit_NCCF] ([Reference_No] ,[Region_Id] ,[District_Id] ,[Branch_Id] ,[Account_No] ,[IFSC_Code] ,[Branch_Code] ,[Party_Name] ,[Date] ,[Debit_Amount] ,[Trans_Description] ,[Payment_Identifier] ,[Mobile_No] ,[Account_Type] ,[Amount_Type] ,[Created_By] ,[Created_Date],[Aid],Bank_Type) VALUES ('" + Ref_Number + "' ,'" + RegionID + "' ,'" + Dist_id + "' ,'" + BranchID + "' ,'38906616113','SBIN0030343' ,'SBIN0030343' ,'Madhya Pradesh Warehousing and Logistics Corporation',GetDate() ,'" + Debit_Amount + "' ,'Godown Rent Payment' ,'" + Payment_Identifier + "'  ,''  ,'Debit'  ,'D'  ,'" + Created_By + "' ,Getdate(),'" + Ref_Aid + "','" + ddlBankType.SelectedValue.ToString() + "')";

            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            int n = cmd.ExecuteNonQuery();
            con.Close();
            lblRespMsg.Text = "Your Payment File has been Generated with Reference Number : ";
            lblRespMsgNo.Text = Ref_Number;
            lblRespMsg.Visible = true;
            lblRespMsgNo.Visible = true;
            //GeneratePaymentFile();
            btn_Sbi_Link.Visible = true;
            btnDownload.Visible = true;
            btnDownload.Enabled = true;
            btn_save.Visible = false;


        }
        catch (Exception ex)
        {
            lblrmsg.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    public void Get_TransID()
    {
        int BID = 0;
        string Bill_No = "";
        string MonthSub = "";
        //string MonthStr = ddlmonth.SelectedValue;
        //int Month = Convert.ToInt32(ddlmonth.SelectedValue);
        string sMonth = DateTime.Now.ToString("MM");
        int MonthLen = sMonth.Length;
        if (MonthLen == 1)
        {
            MonthSub = "0" + sMonth;
        }
        else
        {
            MonthSub = sMonth;
        }
        string BranchID = ddlDepotList.SelectedValue.ToString();
        //qry = "select max(BId) as BId from tbl_Storage_Bill_Details where branch_Id='" + BranchID + "' and Depositor_Id='" + Depositor_ID + "'";
        qry = "select max(AId) as BId from tbl_Payment_Credit_NCCF where branch_Id='" + BranchID + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        //string Bill_No = "";
        //string Godown_Id = ddlgodown.SelectedValue.ToString();
        if (dt.Rows.Count != 0 && dt.Rows[0]["BId"].ToString() != null && dt.Rows[0]["BId"].ToString() != "")
        {
            if (dt.Rows[0]["BId"].ToString() != "" || Convert.ToInt32(dt.Rows[0]["BId"]) != 0)
            {
                BID = Convert.ToInt32(dt.Rows[0]["BId"]);

                int SubBN = BID + 1;
                //9-1-2302001-20-11-0001
                //Bill_No = ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + BillSubType + Godown_Id + SubBN.ToString();
                Bill_No = "15" + BranchID + ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + SubBN.ToString();

                BID = SubBN;
            }
            else
            {
                //Bill_No = ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + BillSubType + Godown_Id + "1";
                Bill_No = "15" + BranchID + ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + "1";
                BID = 1;
            }
        }
        else
        {
            //Bill_No = ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + BillSubType + Godown_Id + "1";
            Bill_No = "15" + BranchID + ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + "1";
            BID = 1;
        }
        //ViewState["BillNo"] = Bill_No;
        //if (ddlGodownType.SelectedValue == "1")
        //{
        //    ViewState["BillNo"] = "91"+Bill_No;
        //}
        //else
        //{
        ViewState["Trans_Id"] = Bill_No;
        //}
        ViewState["BID"] = BID;
    }
    public void GetReferenceNo()
    {
        int Ref_Aid = 0;
        string Bill_No = "";
        string MonthSub = "";
        //string MonthStr = ddlmonth.SelectedValue;
        //int Month = Convert.ToInt32(ddlmonth.SelectedValue);
        string sMonth = DateTime.Now.ToString("MM");
        int MonthLen = sMonth.Length;
        if (MonthLen == 1)
        {
            MonthSub = "0" + sMonth;
        }
        else
        {
            MonthSub = sMonth;
        }
        string BranchID = ddlDepotList.SelectedValue.ToString();
        //qry = "select max(BId) as BId from tbl_Storage_Bill_Details where branch_Id='" + BranchID + "' and Depositor_Id='" + Depositor_ID + "'";
        qry = "select max(AId) as BId from tbl_Payment_Debit_NCCF where branch_Id='" + BranchID + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        //string Bill_No = "";
        //string Godown_Id = ddlgodown.SelectedValue.ToString();
        if (dt.Rows.Count != 0 && dt.Rows[0]["BId"].ToString() != null && dt.Rows[0]["BId"].ToString() != "")
        {
            if (dt.Rows[0]["BId"].ToString() != "" || Convert.ToInt32(dt.Rows[0]["BId"]) != 0)
            {
                Ref_Aid = Convert.ToInt32(dt.Rows[0]["BId"]);

                int SubBN = Ref_Aid + 1;
                //9-1-2302001-20-11-0001
                //Bill_No = ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + BillSubType + Godown_Id + SubBN.ToString();
                //Trans_Id = "6" + BranchID + ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + SubBN.ToString();
                //Ref_Number = "7" + BranchID + ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub+((DateTime.Now.Day).ToString()) + SubBN.ToString();
                Ref_Number = "11" + BranchID + ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + SubBN.ToString();

                Ref_Aid = SubBN;
            }
            else
            {
                //Bill_No = ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + BillSubType + Godown_Id + "1";
                Ref_Number = "11" + BranchID + ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + "1";
                Ref_Aid = 1;
            }
        }
        else
        {
            //Bill_No = ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + BillSubType + Godown_Id + "1";
            Ref_Number = "11" + BranchID + ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + "1";
            Ref_Aid = 1;
        }
        //ViewState["BillNo"] = Bill_No;
        //if (ddlGodownType.SelectedValue == "1")
        //{
        //    ViewState["BillNo"] = "91"+Bill_No;
        //}
        //else
        //{
        //ViewState["Trans_Id"] = Bill_No;
        ////}
        ViewState["RID"] = Ref_Aid;
    }
    private string CreateDelimitedFileFromDt(DataTable dt, string delimiter)
    {
        StringBuilder sb = new StringBuilder();
        foreach (DataRow row in dt.Rows)
        {

            string stuff = "";
            foreach (DataColumn col in row.Table.Columns)
            {
                string colvalue = Convert.ToString(row[col]);
                colvalue += delimiter;
                stuff += colvalue;
            }
            // get rid of delimiter after last column if any
            stuff = stuff.TrimEnd(delimiter.ToCharArray());
            // add line feed
            stuff += "\r\n";
            // append to sb
            sb.Append(stuff);

        }
        Response.Clear();
        Response.Buffer = true;
        string Bank_T = "";
        if (ddlBankType.SelectedValue == "S")
        {
            Bank_T = "SBI";
        }
        else if (ddlBankType.SelectedValue == "O")
        {
            Bank_T = "OTH";
        }
        string RefNo = lblRespMsgNo.Text.ToString();
        string File_NAME = Bank_T + RefNo + ".txt";
        //Response.AddHeader("content-disposition", @"attachment;filename='" + File_NAME + "'");
        Response.AddHeader("content-disposition", "attachment;filename=" + File_NAME);
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(sb);
        Response.Flush();
        Response.End();
        //btn_Sbi_Link.Visible = true;

        return sb.ToString();

    }
    private void GeneratePaymentFile()
    {
        try
        {
            string Dist_id = ddlDistrict.SelectedValue.ToString();
            string Branch_Id = ddlDepotList.SelectedValue.ToString();
            string str = "";
            if (ddlBankType.SelectedValue == "S")
            {
                //str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + lblRespMsgNo.Text + "' union select [Account_No]+'#'+IFSC_Code+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,IFSC_Code from [tbl_Payment_Credit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + lblRespMsgNo.Text + "') as AA order by IFSC_Code";
                str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#' as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit_NCCF] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + lblRespMsgNo.Text + "' and Bank_Type='S' union select [Account_No]+'#'+SUBSTRING(IFSC_Code,7,6)+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description as Statements,IFSC_Code from [tbl_Payment_Credit_NCCF] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + lblRespMsgNo.Text + "') as AA order by IFSC_Code";

            }
            else if (ddlBankType.SelectedValue == "O")
            {
                //str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + lblRespMsgNo.Text + "' union select [Account_No]+'#'+IFSC_Code+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,IFSC_Code from [tbl_Payment_Credit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + lblRespMsgNo.Text + "') as AA order by IFSC_Code";
                str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit_NCCF] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + lblRespMsgNo.Text + "' and Bank_Type='O' union select [Account_No]+'#'+IFSC_Code+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,IFSC_Code from [tbl_Payment_Credit_NCCF] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + lblRespMsgNo.Text + "') as AA order by IFSC_Code";

            }
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                CreateDelimitedFileFromDt(ds.Tables[0], "#");
            }
            else
            {

            }

        }

        catch (Exception ex)
        {

        }
    }
    protected void btn_Sbi_Link_Click(object sender, EventArgs e)
    {
        //Response.Redirect("https://yonobusiness.sbi/login/yonobusinesslogin");
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        //lblRespMsg.Text = "Your Payment File has been Generated with Reference Number : " + Ref_Number;
        //lblRespMsg.Visible = true;
        GeneratePaymentFile();
        btn_Sbi_Link.Visible = true;
        //btnDownload.Visible = true;
        //btnDownload.Enabled = true;
        btn_save.Visible = false;
        Update_Downlod_Detail();

    }
    public void Update_Downlod_Detail()
    {
        string Dist_id = ddlDistrict.SelectedValue.ToString();
        string BranchID = ddlDepotList.SelectedValue.ToString();
        string RegionID = Session["Region_ID"].ToString();
        string Created_By = Request.ServerVariables["REMOTE_ADDR"].ToString();
        //qry = "INSERT INTO [dbo].[tbl_Payment_Credit] ([Trans_Id] ,[Region_Id] ,[District_Id] ,[Branch_Id] ,[Account_No] ,[IFSC_Code] ,[Branch_Code] ,[Party_Name] ,[Date] ,[Credit_Amount] ,[Trans_Description] ,[Payment_Identifier] ,[Reference_No] ,[Mobile_No] ,[Account_Type] ,[Amount_Type] ,[Created_By] ,[Created_Date] ,[Is_Download] ,[Download_By] ,[Download_Date] ,[Status] ,[Beneficiary_Id] ,[Bill_No] ,[Godown_Id] ,[Aid]) VALUES ('" + Trans_Id + "' ,'" + RegionID + "' ,'" + Dist_id + "' ,'" + BranchID + "' ,'" + Account_No + "','" + IFSC_Code + "' ,'" + IFSC_Code + "' ,'" + Party_Name + "',GetDate() ,'" + Credit_Amount + "'  ,'" + Trans_Description + "' ,'" + Payment_Identifier + "' ,'" + Ref_Number + "'  ,'" + Mobile_No + "'  ,'" + Account_Type + "'  ,'" + Amount_Type + "'  ,'" + Created_By + "' ,Getdate() ,'','' ,'' ,'' ,'" + Beneficiary_Id + "' ,'" + Rent_Bill_No + "','" + Godown_ID + "' ,'" + AID + "')";
        qry = "update tbl_Payment_Debit_NCCF set Is_Download='Y',Download_By='" + Created_By + "',Download_Date=GETDATE() where Reference_No='" + lblRespMsgNo.Text + "' and Branch_Id='" + BranchID + "'";

        SqlCommand cmd2 = new SqlCommand(qry, con);
        con.Open();
        int i = cmd2.ExecuteNonQuery();
        con.Close();
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounting/RO_Generate_EPF_For_NCCF.aspx");
    }

    protected void ddlBankType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillsDetail();
        lblRespMsg.Text = "";
        lblRespMsgNo.Text = "";
        lblRespMsg.Visible = false;
        lblRespMsgNo.Visible = false;
        btn_Sbi_Link.Visible = false;
        btnDownload.Visible = false;
        btnDownload.Enabled = false;
        btn_save.Visible = true;
    }
    protected void fillMonth()
    {
        //ddlmonth.ClearSelection();
        ddlmonth.Items.Clear();
        ddlmonth.Items.Add(new ListItem("--Select--", "0"));
        ddlmonth.Items.Add(new ListItem("January", "1"));
        ddlmonth.Items.Add(new ListItem("February", "2"));
        ddlmonth.Items.Add(new ListItem("March", "3"));
        ddlmonth.Items.Add(new ListItem("April", "4"));
        ddlmonth.Items.Add(new ListItem("May", "5"));
        ddlmonth.Items.Add(new ListItem("June", "6"));
        ddlmonth.Items.Add(new ListItem("July", "7"));
        ddlmonth.Items.Add(new ListItem("August", "8"));
        ddlmonth.Items.Add(new ListItem("September", "9"));
        ddlmonth.Items.Add(new ListItem("October", "10"));
        ddlmonth.Items.Add(new ListItem("November", "11"));
        ddlmonth.Items.Add(new ListItem("December", "12"));
        ddlmonth.SelectedIndex = 0;
    }
}