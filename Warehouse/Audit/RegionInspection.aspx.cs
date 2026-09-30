using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Specialized;
using System.Collections;

public partial class Inspection_RegionInspection : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataTable Dt2 = new DataTable();
    DataTable EditStack = new DataTable();
    DataTable EditStackNonMPSCSC = new DataTable();
    string Todaydate = "";
    string CheckValid = "";
    string receiptid = string.Empty;
    SqlCommand cmd = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["RegionId"].ToString() != "" && Session["RegionId"].ToString() != null)
                {
                    lbluser.Text = Session["RegionName"].ToString();
                    txt_ROName.Value = Session["RegionName"].ToString();
                    owncapacity();
                    owncapacityuses();
                    capcapacity();
                    capcapacityuses();
                    WarehouseCapacityDetail();
                    GetEmpDetail();
                    Hiredcapacity();
                    Hiredcapacityuses();
                    fillFinancialYear();
                    SetInitialRow();
                }
                else
                {
                    Response.Redirect("InspectionLogin.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("InspectionLogin.aspx");
            }
        }
    }
    protected void gdstackingdetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (ViewState["ckstat"].ToString() != "Delete")
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
            }
        }
        catch (Exception ex)
        {
            //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error gdStackingDetails_RowCreated has occured, try again'); </script> ");
        }
    }
    protected void fillFinancialYear()
    {
        ddlFinncialYear.Items.Insert(0, "--Select--");
        ddlFinncialYear.Items.Insert(1, "2015-16");
        ddlFinncialYear.Items.Insert(2, "2014-15");
        ddlFinncialYear.Items.Insert(3, "2013-14");
        ddlFinncialYear.Items.Insert(4, "2012-13");
        ddlFinncialYear.Items.Insert(5, "2011-12");
        ddlFinncialYear.Items.Insert(6, "2010-11");
        ddlFinncialYear.Items.Insert(7, "2009-10");
        ddlFinncialYear.Items.Insert(8, "Before 2009");
        ddlFinncialYear.SelectedIndex = 0;
    }
    protected void gdstackingdetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int i = e.RowIndex;
            if (gdstackingdetails.Rows.Count < 1)
            {
                ViewState["ckstat"] = "Delete";
            }
            ((DataTable)Session["dt1"]).Rows[i].Delete();
            ((DataTable)Session["dt1"]).AcceptChanges();

            gdstackingdetails.DataSource = (DataTable)Session["dt1"];
            gdstackingdetails.DataBind();
            // chksum();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in gdStackingDetails_RowDeleting has occured, try again'); </script> ");
        }
    }
    private DataTable CreateTable()
    {
        DataTable dt = new DataTable();//DataTable is created
        DataColumn empname = new DataColumn("empname", Type.GetType("System.String"));
        DataColumn emppost = new DataColumn("emppost", Type.GetType("System.String"));
        DataColumn emppDate = new DataColumn("emppDate", Type.GetType("System.String"));
        dt.Columns.Add(empname);//Column is added to the DataTable
        dt.Columns.Add(emppost);//Column is added to the DataTable
        dt.Columns.Add(emppDate);//Column is added to the DataTable
        dt.AcceptChanges();
        return dt;
    }

    private DataTable CreateTableEditStack()
    {
        DataTable dtEditStack = new DataTable();//DataTable is created
        DataColumn Godownid = new DataColumn("Godownid", Type.GetType("System.String"));
        DataColumn Stackid = new DataColumn("Stackid", Type.GetType("System.String"));
        DataColumn GodownName = new DataColumn("GodownName", Type.GetType("System.String"));
        DataColumn StackName = new DataColumn("StackName", Type.GetType("System.String"));
        DataColumn Bags = new DataColumn("Bags", Type.GetType("System.Int32"));
        DataColumn Weight = new DataColumn("Weight", Type.GetType("System.Decimal"));
        dtEditStack.Columns.Add(Godownid);//Column is added to the DataTable
        dtEditStack.Columns.Add(Stackid);//Column is added to the DataTable
        dtEditStack.Columns.Add(GodownName);//Column is added to the DataTable
        dtEditStack.Columns.Add(StackName);//Column is added to the DataTable
        dtEditStack.Columns.Add(Bags);//Column is added to the DataTable
        dtEditStack.Columns.Add(Weight);//Column is added to the DataTable

        dtEditStack.AcceptChanges();
        return dtEditStack;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bool checkstatus = false;
        try
        {
            if (txtempname.Value == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आपने कर्मचारी का नाम नहीं लिखा है!'); </script> ");
                return;
            }
            else if (txtemppost.Value == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आपने कर्मचारी का पद नाम नहीं लिखा है'); </script> ");
                return;
            }
            else if (txtREP_Date.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आपने कर्मचारी का पदस्थ दिनांक नहीं लिखा है'); </script> ");
                return;
            }
            else
            {


                if (Session["dt1"] == null)
                {
                    Dt1 = CreateTable();
                    Session["dt1"] = Dt1;
                }
                // adding rows to the datatable
                DataRow dr = ((DataTable)Session["dt1"]).NewRow();
                ((DataTable)Session["dt1"]).AcceptChanges();
                dr["empname"] = txtempname.Value;
                dr["emppost"] = txtemppost.Value;
                //dr["emppDate"] = txtREP_Date.Text;
                dr["emppDate"] = txtREP_Date.Text;

                ((DataTable)Session["dt1"]).Rows.Add(dr);
                ((DataTable)Session["dt1"]).AcceptChanges();
                gdstackingdetails.DataSource = (DataTable)Session["dt1"];
                gdstackingdetails.DataBind();

                txtemppost.Value = "";
                txtempname.Value = "";
                txtREP_Date.Text = "";
                //txtREP_Date.Value = "";


            }
        }
        catch (Exception ex)
        {
            // Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in btnAddStack_Click has occured, try again'); </script> ");
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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txt_AuditerName.Value == "" || txt_AuditerName.Value == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया अंकेक्षणकर्ता अधिकारी का नाम दर्ज करे'); </script> ");
        }
        else if (txt_AuditerPost.Value == "" || txt_AuditerPost.Value == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया अंकेक्षणकर्ता अधिकारी का पद दर्ज करे'); </script> ");
        }
        else if (txtAuditDate.Text == "" || txtAuditDate.Text == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया अंकेक्षण संपादन की दिनांक दर्ज करे'); </script> ");
        }
        else if (txtRMName.Value == "" || txtRMName.Value == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया क्षेत्रीय प्रबंधक का नाम दर्ज करे'); </script> ");
        }
        else if (txtRMpost.Value == "" || txtRMpost.Value == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया क्षेत्रीय प्रबंधक का पद दर्ज करे'); </script> ");
        }
        else if (txtRMPostingDate.Text == "" || txtRMPostingDate.Text == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया क्षेत्रीय प्रबंधक का पदस्थ दिनांक दर्ज करे'); </script> ");
        }
        //else if (txtempname.Value == "" || txtempname.Value == null)
        //{
        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया क्षेत्रीय कार्यालय में पदस्थ कर्मचारियों के नाम दर्ज करे'); </script> ");
        //}
        //else if (txtemppost.Value == "" || txtemppost.Value == null)
        //{
        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया क्षेत्रीय कार्यालय में पदस्थ कर्मचारियों के पद दर्ज करे'); </script> ");
        //}
        else if (txt_ownCap.Text == "" || txt_ownCap.Text == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया स्वनिर्मित क्षमता दर्ज करे'); </script> ");
        }
        else if (txt_ownuse.Text == "" || txt_ownuse.Text == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया स्वनिर्मित उपयोगिता दर्ज करे'); </script> ");
        }
        else if (txt_capcap.Text == "" || txt_capcap.Text == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया केप क्षमता दर्ज करे'); </script> ");
        }
        else if (txt_capuse.Text == "" || txt_capuse.Text == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया केप उपयोगिता दर्ज करे'); </script> ");
        }
        else if (txt_hiredcap.Text == "" || txt_hiredcap.Text == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया किराये की क्षमता दर्ज करे'); </script> ");
        }
        else if (txt_hiredUSe.Text == "" || txt_hiredUSe.Text == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया किराये की उपयोगिता दर्ज करे'); </script> ");
        }
        else if (txtTargetIncome.Text == "" || txtTargetIncome.Text == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया क्षेत्रीय कार्यालय की आय का लक्ष्य दर्ज करे'); </script> ");
        }
        else if (txtTargetAchiveR.Text == "" || txtTargetAchiveR.Text == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया आनुपातिक लक्ष्य(निरीक्षण माह तक) दर्ज करे'); </script> ");
        }
        else if (txtTargetAchiveP.Text == "" || txtTargetAchiveP.Text == null)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया लक्ष्य प्राप्ति रु० दर्ज करे'); </script> ");
        }
        //else if (txtTargetRatio.Value == "" || txtTargetRatio.Value == null)
        //{
        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('कृपया लक्ष्य प्राप्ति का प्रतिशत दर्ज करे'); </script> ");
        //}
        //else if (txtTargetRatio.Value == "" || txtTargetRatio.Value == null)
        //{
        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some Error has occured, try again'); </script> ");
        //}
        //else if (txtTargetRatio.Value == "" || txtTargetRatio.Value == null)
        //{
        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some Error has occured, try again'); </script> ");
        //}
        //else if (txtTargetRatio.Value == "" || txtTargetRatio.Value == null)
        //{
        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some Error has occured, try again'); </script> ");
        //}
        else
        {
            Insert_Detail();
        }
        

    }
    public void Insert_Detail()
    {
        string auid = "";
        string s = Session["RegionId"].ToString();
        string QueryMax = "select isnull(Max(AID),0)+1 from RegionAudit where  RegionId='" + Session["RegionId"].ToString() + "'";
        SqlCommand cmd2 = new SqlCommand(QueryMax, Con);
        Con.Open();
        string str3 = cmd2.ExecuteScalar().ToString();
        if ((str3 == String.Empty) || str3 == "")
        {
            str3 = "0";
        }
        if (Convert.ToInt64(str3) != 0)
        {
            string Regionid = Session["RegionId"].ToString();
            auid = Regionid + System.DateTime.Now.Date.ToString("yy") + Convert.ToString(Convert.ToInt64(str3));
        }
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        //string sql = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[BranchAudit] VALUES (@AuditerName,@AuditerPost,@BranchId,@BranchName,@AuditDate,@BranchManName,@BranchManPost ,@BranchPostingDate,@OwnCap,@OwnUses,@CapCap,@CapUses,@HiredCap,@HiredUSes,@ObtnBsns,@ObtnableBsns,@AttemntfrmBMForBsns,@AimofBranch,@RetioofAimtilOditdate,@Income,@lakchapraptiparsent,@lakhasekamjada,@rokigairashi,@overandabovedtl,@commoditydtl,@stackingvegyanik,@skandhmekamiadhik,@spelage,@lambitbhugtan,@lijarent,@viybildtl ,@stafperfom,@RMTeep ,@BankRin,@Teep,@Trutipatrak,@CreatedBy,@CreatedDate,@SandharitRcd,@InId,@AuId)";
        //string sql = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[RegionAudit](AuditerName,AuditerPost,RegionId,RegionName,AuditDate,RMName,RMPost,RMPostingDate,OwnCap,OwnUses,CapCap,CapUses,HiredCap,HiredUSes,TargetIncome,TargetRatio,TargetAchiveR,TargetAchiveP,AuditerRemark,RecentYearsComp,BusinessDevelopInfo,IncomeDetailUptoAuditD,ROInfo1,ROInfo2,ROInfo3,AchiveIncomeCurrentY,ReceivedIncomeCurrentY,PendingIncomeRecentY,ReceivedIncomeRecentY,CashbookBalance,ImprestCashBalance,BankBalance,RNoOfGodown,RCapacityOfGodown,HOLevelAudit,ROLevelAudit,Truti_Patrak,Created_By,Created_Date,AID,AuditID) VALUES (@AuditerName,@AuditerPost,@RegionId,@RegionName,@AuditDate,@RMName,@RMPost,@RMPostingDate,@OwnCap,@OwnUses,@CapCap,@CapUses,@HiredCap,@HiredUSes,@TargetIncome,@TargetRatio,@TargetAchiveR,@TargetAchiveP,@AuditerRemark,@RecentYearsComp,@BusinessDevelopInfo,@IncomeDetailUptoAuditD,@ROInfo1,@ROInfo2,@ROInfo3,@AchiveIncomeCurrentY,@ReceivedIncomeCurrentY,@PendingIncomeRecentY,@ReceivedIncomeRecentY,@CashbookBalance,@ImprestCashBalance,@BankBalance,@RNoOfGodown,@RCapacityOfGodown,@HOLevelAudit,@ROLevelAudit,@Truti_Patrak,@Created_By,@Created_Date,@AID,@AuditID)";
        string sql = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[RegionAudit](AuditerName,AuditerPost,RegionId,RegionName,AuditDate,RMName,RMPost,RMPostingDate,OwnCap,OwnUses,CapCap,CapUses,HiredCap,HiredUSes,TargetIncome,TargetRatio,TargetAchiveR,TargetAchiveP,AuditerRemark,RecentYearsComp,BusinessDevelopInfo,IncomeDetailUptoAuditD,ROInfo1,ROInfo2,ROInfo3,AchiveIncomeCurrentY,ReceivedIncomeCurrentY,PendingIncomeRecentY,ReceivedIncomeRecentY,CashbookBalance,ImprestCashBalance,BankBalance,RNoOfGodown,RCapacityOfGodown,HOLevelAudit,ROLevelAudit,Truti_Patrak,Created_By,Created_Date,AID,AuditID,TDS1,TDS2,MoneyTrans,WhrCharges,LbrCharges,JVS1,JVS2,Financial_Year) VALUES (@AuditerName,@AuditerPost,@RegionId,@RegionName,@AuditDate,@RMName,@RMPost,@RMPostingDate,@OwnCap,@OwnUses,@CapCap,@CapUses,@HiredCap,@HiredUSes,@TargetIncome,@TargetRatio,@TargetAchiveR,@TargetAchiveP,@AuditerRemark,@RecentYearsComp,@BusinessDevelopInfo,@IncomeDetailUptoAuditD,@ROInfo1,@ROInfo2,@ROInfo3,@AchiveIncomeCurrentY,@ReceivedIncomeCurrentY,@PendingIncomeRecentY,@ReceivedIncomeRecentY,@CashbookBalance,@ImprestCashBalance,@BankBalance,@RNoOfGodown,@RCapacityOfGodown,@HOLevelAudit,@ROLevelAudit,@Truti_Patrak,@Created_By,@Created_Date,@AID,@AuditID,@TDS1,@TDS2,@MoneyTrans,@WhrCharges,@LbrCharges,@JVS1,@JVS2,@Financial_Year)";

        SqlCommand cmd = new SqlCommand(sql, Con);
        SqlParameter[] prms = new SqlParameter[49];

        prms[0] = new SqlParameter("@AuditerName", SqlDbType.NVarChar, 50);
        prms[0].Value = txt_AuditerName.Value;
        prms[1] = new SqlParameter("@AuditerPost", SqlDbType.NVarChar, 50);
        prms[1].Value = txt_AuditerPost.Value;
        prms[2] = new SqlParameter("@RegionId", SqlDbType.VarChar, 20);
        prms[2].Value = Session["RegionId"].ToString();
        prms[3] = new SqlParameter("@RegionName", SqlDbType.NVarChar, 50);
        prms[3].Value = txt_ROName.Value;
        prms[4] = new SqlParameter("@AuditDate", SqlDbType.DateTime);
        prms[4].Value = getDate_MDY(txtAuditDate.Text);
        prms[5] = new SqlParameter("@RMName", SqlDbType.NVarChar, 50);
        prms[5].Value = txtRMName.Value;
        prms[6] = new SqlParameter("@RMPost", SqlDbType.NVarChar, 50);
        prms[6].Value = txtRMpost.Value;
        prms[7] = new SqlParameter("@RMPostingDate", SqlDbType.DateTime);
        prms[7].Value = getDate_MDY(txtRMPostingDate.Text);
        prms[8] = new SqlParameter("@OwnCap", SqlDbType.Decimal);
        prms[8].Value = Convert.ToDecimal(txt_ownCap.Text);
        prms[9] = new SqlParameter("@OwnUses", SqlDbType.Decimal);
        prms[9].Value = Convert.ToDecimal(txt_ownuse.Text);
        prms[10] = new SqlParameter("@CapCap", SqlDbType.Decimal);
        prms[10].Value = Convert.ToDecimal(txt_capcap.Text);
        prms[11] = new SqlParameter("@CapUses", SqlDbType.Decimal);
        prms[11].Value = Convert.ToDecimal(txt_capuse.Text);
        prms[12] = new SqlParameter("@HiredCap", SqlDbType.Decimal);
        prms[12].Value = Convert.ToDecimal(txt_hiredcap.Text);
        prms[13] = new SqlParameter("@HiredUSes", SqlDbType.Decimal);
        prms[13].Value = Convert.ToDecimal(txt_hiredUSe.Text);
        prms[14] = new SqlParameter("@TargetIncome", SqlDbType.Decimal);
        prms[14].Value = txtTargetIncome.Text;
        prms[15] = new SqlParameter("@TargetRatio", SqlDbType.Decimal);
        prms[15].Value = txtTargetRatio.Text;
        prms[16] = new SqlParameter("@TargetAchiveR", SqlDbType.Decimal);
        prms[16].Value = txtTargetAchiveP.Text;
        prms[17] = new SqlParameter("@TargetAchiveP", SqlDbType.Decimal);
        prms[17].Value = txtTargetAchiveP.Text;
        prms[18] = new SqlParameter("@AuditerRemark", SqlDbType.NVarChar, 200);
        prms[18].Value = txtAuditerRemark.Value;
        prms[19] = new SqlParameter("@RecentYearsComp", SqlDbType.NVarChar, 200);
        prms[19].Value = txtRecentYearsComp.Value;

        prms[20] = new SqlParameter("@BusinessDevelopInfo", SqlDbType.NVarChar, 200);
        prms[20].Value = txtBusinessDevelopInfo.Value;
        prms[21] = new SqlParameter("@IncomeDetailUptoAuditD", SqlDbType.NVarChar, 200);
        prms[21].Value = txtIncomeDetailUptoAuditD.Value;
        prms[22] = new SqlParameter("@ROInfo1", SqlDbType.NVarChar, 20);
        prms[22].Value = txtROInfo1.Value;
        prms[23] = new SqlParameter("@ROInfo2", SqlDbType.NVarChar, 20);
        prms[23].Value = txtROInfo2.Value;
        prms[24] = new SqlParameter("@ROInfo3", SqlDbType.NVarChar, 20);
        prms[24].Value = txtROInfo3.Value;
        prms[25] = new SqlParameter("@AchiveIncomeCurrentY", SqlDbType.Decimal);
        prms[25].Value = txtAchiveIncomeCurrentY.Value;
        prms[26] = new SqlParameter("@ReceivedIncomeCurrentY", SqlDbType.Decimal);
        prms[26].Value = txtReceivedIncomeCurrentY.Value;
        prms[27] = new SqlParameter("@PendingIncomeRecentY", SqlDbType.Decimal);
        prms[27].Value = txtPendingIncomeRecentY.Value;
        prms[28] = new SqlParameter("@ReceivedIncomeRecentY", SqlDbType.Decimal);
        prms[28].Value = txtReceivedIncomeRecentY.Value;
        prms[29] = new SqlParameter("@CashbookBalance", SqlDbType.Decimal);
        prms[29].Value = txtCashbookBalance.Value;
        prms[30] = new SqlParameter("@ImprestCashBalance", SqlDbType.Decimal);
        prms[30].Value = txtImprestCashBalance.Value;
        prms[31] = new SqlParameter("@BankBalance", SqlDbType.Decimal);
        prms[31].Value = txtBankBalance.Value;

        prms[32] = new SqlParameter("@RNoOfGodown", SqlDbType.Int);
        prms[32].Value = txtRNoOfGodown.Value;
        prms[33] = new SqlParameter("@RCapacityOfGodown", SqlDbType.Decimal);
        prms[33].Value = txtRCapacityOfGodown.Value;
        prms[34] = new SqlParameter("@HOLevelAudit", SqlDbType.NVarChar, 20);
        prms[34].Value = txtHOLevelAudit.Value;
        prms[35] = new SqlParameter("@ROLevelAudit", SqlDbType.NVarChar, 20);
        prms[35].Value = txtROLevelAudit.Value;
        prms[36] = new SqlParameter("@Truti_Patrak", SqlDbType.NVarChar, 200);
        prms[36].Value = txtTruti_Patrak.Value;
        prms[37] = new SqlParameter("@Created_By", SqlDbType.VarChar, 20);
        prms[37].Value = ip;
        prms[38] = new SqlParameter("@Created_Date", SqlDbType.NVarChar, 150);
        prms[38].Value = DateTime.Now;
        prms[39] = new SqlParameter("@AID", SqlDbType.Int);
        prms[39].Value = str3;
        prms[40] = new SqlParameter("@AuditID", SqlDbType.VarChar, 20);
        prms[40].Value = auid;


        prms[41] = new SqlParameter("@TDS1", SqlDbType.NVarChar, 200);
        prms[41].Value = txttds1.Value;
        prms[42] = new SqlParameter("@TDS2", SqlDbType.NVarChar, 200);
        prms[42].Value = txttds2.Value;
        prms[43] = new SqlParameter("@MoneyTrans", SqlDbType.NVarChar, 200);
        prms[43].Value = txtdr.Value;
        prms[44] = new SqlParameter("@WhrCharges", SqlDbType.NVarChar, 200);
        prms[44].Value = txtwc.Value;
        prms[45] = new SqlParameter("@LbrCharges", SqlDbType.NVarChar, 200);
        prms[45].Value = txtct.Value;
        prms[46] = new SqlParameter("@JVS1", SqlDbType.NVarChar, 200);
        prms[46].Value = txtjvs1.Value;
        prms[47] = new SqlParameter("@JVS2", SqlDbType.NVarChar, 200);
        prms[47].Value = txtjvs2.Value;
        prms[48] = new SqlParameter("@Financial_Year", SqlDbType.NVarChar, 10);
        prms[48].Value = ddlFinncialYear.Text;


        int CT = 0;
        cmd.Parameters.AddRange(prms);

        CT = cmd.ExecuteNonQuery();
        Con.Close();
        if (CT > 0)
        {
            if (gdstackingdetails.Rows.Count > 0)
            {
                int k;
                for (k = 0; k < gdstackingdetails.Rows.Count; k++)
                {
                    string sql3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[BranchAuditEmp](AuditID,EmpName,EmpPost,Employee_Type,EMPPostingdate) VALUES (@AuditID,@EmpName,@EmpPost,@Employee_Type,@EMPPostingdate)";

                    SqlCommand cmd3 = new SqlCommand(sql3, Con);
                    SqlParameter[] prms3 = new SqlParameter[5];

                    prms3[0] = new SqlParameter("@AuditID", SqlDbType.VarChar, 20);
                    prms3[0].Value = auid;
                    prms3[1] = new SqlParameter("@EmpName", SqlDbType.NVarChar, 50);
                    prms3[1].Value = gdstackingdetails.Rows[k].Cells[1].Text.ToString();
                    prms3[2] = new SqlParameter("@EmpPost", SqlDbType.NVarChar, 20);
                    prms3[2].Value = gdstackingdetails.Rows[k].Cells[2].Text.ToString();
                    prms3[3] = new SqlParameter("@Employee_Type", SqlDbType.VarChar, 20);
                    prms3[3].Value = "RO";
                    prms3[4] = new SqlParameter("@EMPPostingdate", SqlDbType.VarChar, 20);
                    prms3[4].Value = gdstackingdetails.Rows[k].Cells[3].Text.ToString();

                    int CT3 = 0;
                    cmd3.Parameters.AddRange(prms3);
                    Con.Open();
                    CT3 = cmd3.ExecuteNonQuery();
                    Con.Close();
                    if (CT3 > 0)
                    {

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some Error has occured, try again'); </script> ");
                    }
                }
            }
            else
            {
                string sql3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[BranchAuditEmp](AuditID,EmpName,EmpPost,Employee_Type,EMPPostingdate) VALUES (@AuditID,@EmpName,@EmpPost,@Employee_Type,@EMPPostingdate)";

                SqlCommand cmd3 = new SqlCommand(sql3, Con);
                SqlParameter[] prms3 = new SqlParameter[5];


                prms3[0] = new SqlParameter("@AuditID", SqlDbType.VarChar, 20);
                prms3[0].Value = auid;
                prms3[1] = new SqlParameter("@EmpName", SqlDbType.NVarChar, 50);
                prms3[1].Value = txtempname.Value;
                prms3[2] = new SqlParameter("@EmpPost", SqlDbType.VarChar, 20);
                prms3[2].Value = txtemppost.Value;
                prms3[3] = new SqlParameter("@Employee_Type", SqlDbType.VarChar, 20);
                prms3[3].Value = "RO";
                prms3[4] = new SqlParameter("@EMPPostingdate", SqlDbType.VarChar, 20);
                prms3[4].Value = txtREP_Date.Text.ToString();

                int CT3 = 0;
                cmd3.Parameters.AddRange(prms3);
                Con.Open();
                CT3 = cmd3.ExecuteNonQuery();
                Con.Close();
                if (CT3 > 0)
                {

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some Error has occured, try again'); </script> ");
                }
            }
           
        }
        //else if (CT > 0)
        //{
            if (gvImprest.Rows.Count > 0)
            {
                int k;
                for (k = 0; k < gvImprest.Rows.Count; k++)
                {
                    string sql5 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Imprest_Recupment] VALUES (@AuditId,@Month,@OpeningBalance,@RecupmentAmount,@DepositByBM,@TFCashBook,@TFConstIimprest,@Total,@ImprestForPass,@ImprestPass,@WitheldAmount,@DisalloudAmount,@ReturnToBM,@ReturnToCashBook,@ReturnToConstImp,@ClosingBalance,@Remark,@AID)";

                    SqlCommand cmd5 = new SqlCommand(sql5, Con);
                    SqlParameter[] prms5 = new SqlParameter[18];


                    prms5[0] = new SqlParameter("@AuditID", SqlDbType.VarChar, 20);
                    prms5[0].Value = auid;
                    prms5[1] = new SqlParameter("@Month", SqlDbType.Int);
                    DropDownList ddl1 = (DropDownList)gvImprest.Rows[k].Cells[1].FindControl("DropDownList1");
                    int DID = Convert.ToInt32(ddl1.SelectedItem.Value);
                    //prms5[1].Value = Convert.ToDecimal(gvImprest.Rows[k].Cells[1].Text);
                    prms5[1].Value = DID;

                    prms5[2] = new SqlParameter("@OpeningBalance", SqlDbType.Decimal);
                    TextBox OB = (TextBox)gvImprest.Rows[k].Cells[1].FindControl("txtOB");
                    //prms5[2].Value = Convert.ToDecimal(gvImprest.Rows[k].Cells[2].Text);
                    prms5[2].Value = Convert.ToDecimal(OB.Text);
                    prms5[3] = new SqlParameter("@RecupmentAmount", SqlDbType.Decimal);
                    TextBox RptAmt = (TextBox)gvImprest.Rows[k].Cells[1].FindControl("txtRptAmt");

                    prms5[3].Value = Convert.ToDecimal(RptAmt.Text);
                    prms5[4] = new SqlParameter("@DepositByBM", SqlDbType.Decimal);
                    TextBox DBRM = (TextBox)gvImprest.Rows[k].Cells[1].FindControl("txtDBRM");

                    prms5[4].Value = Convert.ToDecimal(DBRM.Text);
                    prms5[5] = new SqlParameter("@TFCashBook", SqlDbType.Decimal);
                    TextBox TFCB = (TextBox)gvImprest.Rows[k].Cells[1].FindControl("txtTFCB");

                    prms5[5].Value = Convert.ToDecimal(TFCB.Text);
                    prms5[6] = new SqlParameter("@TFConstIimprest", SqlDbType.Decimal);
                    TextBox TFCImp = (TextBox)gvImprest.Rows[k].Cells[1].FindControl("txtTFCImp");

                    prms5[6].Value = Convert.ToDecimal(TFCImp.Text);
                    prms5[7] = new SqlParameter("@Total", SqlDbType.Decimal);
                    TextBox Total = (TextBox)gvImprest.Rows[k].Cells[1].FindControl("txtTotal");

                    prms5[7].Value = Convert.ToDecimal(Total.Text);
                    prms5[8] = new SqlParameter("@ImprestForPass", SqlDbType.Decimal);
                    TextBox ImpFPass = (TextBox)gvImprest.Rows[k].Cells[1].FindControl("txtImpFPass");

                    prms5[8].Value = Convert.ToDecimal(ImpFPass.Text);
                    prms5[9] = new SqlParameter("@ImprestPass", SqlDbType.Decimal);
                    TextBox ImpPass = (TextBox)gvImprest.Rows[k].Cells[1].FindControl("txtImpPass");

                    prms5[9].Value = Convert.ToDecimal(ImpPass.Text);
                    prms5[10] = new SqlParameter("@WitheldAmount", SqlDbType.Decimal);
                    TextBox WithAmt = (TextBox)gvImprest.Rows[k].Cells[1].FindControl("txtWithAmt");

                    prms5[10].Value = Convert.ToDecimal(WithAmt.Text);
                    prms5[11] = new SqlParameter("@DisalloudAmount", SqlDbType.Decimal);
                    TextBox DisAmt = (TextBox)gvImprest.Rows[k].Cells[1].FindControl("txtDisAmt");

                    prms5[11].Value = Convert.ToDecimal(DisAmt.Text);
                    prms5[12] = new SqlParameter("@ReturnToBM", SqlDbType.Decimal);
                    TextBox RetTBM = (TextBox)gvImprest.Rows[k].Cells[1].FindControl("txtRetTBM");

                    prms5[12].Value = Convert.ToDecimal(RetTBM.Text);
                    prms5[13] = new SqlParameter("@ReturnToCashBook", SqlDbType.Decimal);
                    TextBox RetTCB = (TextBox)gvImprest.Rows[k].Cells[1].FindControl("txtRetTCB");
                    ;
                    prms5[13].Value = Convert.ToDecimal(RetTCB.Text);
                    prms5[14] = new SqlParameter("@ReturnToConstImp", SqlDbType.Decimal);
                    TextBox RetTCImp = (TextBox)gvImprest.Rows[k].Cells[1].FindControl("txtRetTCImp");

                    prms5[14].Value = Convert.ToDecimal(RetTCImp.Text);
                    prms5[15] = new SqlParameter("@ClosingBalance", SqlDbType.Decimal);
                    TextBox CB = (TextBox)gvImprest.Rows[k].Cells[1].FindControl("txtCB");
                    prms5[15].Value = Convert.ToDecimal(CB.Text);
                    prms5[16] = new SqlParameter("@Remark", SqlDbType.VarChar, 20);
                    prms5[16].Value = "Remark";
                    prms5[17] = new SqlParameter("@AID", SqlDbType.Decimal);
                    prms5[17].Value = 1;


                    int CT5 = 0;
                    cmd5.Parameters.AddRange(prms5);
                    Con.Open();
                    CT5 = cmd5.ExecuteNonQuery();
                    Con.Close();
                    if (CT5 > 0)
                    {

                    }
                }
            //}
                Session["AuditId"] = auid;
                Response.Redirect("RegionInspecPrint.aspx");
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some Error has occured, try again'); </script> ");
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("InspectionLogin.aspx");
    }
    protected void owncapacity()
    {
        string query = "SELECT sum(MG.[Godown_Scientific_Capacity])/10 as owncapacity FROM [tbl_MetaData_GODOWN] as MG inner join tbl_MetaData_DISTRICT as MD on MD.District_Id=MG.DistrictId where MG.Hired_Type='Owned' and MD.Region_ID='" + Session["RegionId"].ToString() + "' and MG.Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and MG.Remarks='Y'";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {

            txt_ownCap.Text = ds.Tables[0].Rows[0]["owncapacity"].ToString();

        }

    }
    protected void owncapacityuses()
    {
        //string query = "SELECT (sum([RecQty])-sum([DelQty]))/10 as ownuses FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] where BranchID='" + Session["UserID"].ToString() + "' and Godown_ID in ( select Godown_ID FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type='Owned' and BranchID='" + Session["UserID"].ToString() + "' and Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and Remarks='Y')";
        string query = "SELECT (sum([RecQty])-sum([DelQty]))/10 as ownuses FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] where Region='" + Session["RegionId"].ToString() + "' and Godown_ID in( select Godown_ID FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] as MG inner join  tbl_MetaData_DISTRICT as MD on MD.District_Id=MG.DistrictId where MG.Hired_Type='Owned' and MD.Region_ID='" + Session["RegionId"].ToString() + "' and MG.Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and MG.Remarks='Y')";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_ownuse.Text = ds.Tables[0].Rows[0]["ownuses"].ToString();

        }

    }
    protected void capcapacity()
    {
        string query = "SELECT sum(MG.[Godown_Scientific_Capacity])/10 as capcapacity FROM [tbl_MetaData_GODOWN] as MG inner join tbl_MetaData_DISTRICT as MD on MD.District_Id=MG.DistrictId where MG.Hired_Type='Owned' and MD.Region_ID='" + Session["RegionId"].ToString() + "' and MG.Storage_Type in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and MG.Remarks='Y'";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {


            // adding rows to the datatable

            txt_capcap.Text = ds.Tables[0].Rows[0]["capcapacity"].ToString();

        }

    }
    protected void capcapacityuses()
    {
        string query = "SELECT (sum([RecQty])-sum([DelQty]))/10 as capuses FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] where Region='" + Session["RegionId"].ToString() + "' and Godown_ID in( select Godown_ID FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] as MG inner join  tbl_MetaData_DISTRICT as MD on MD.District_Id=MG.DistrictId where MG.Hired_Type='Owned' and MD.Region_ID='" + Session["RegionId"].ToString() + "' and MG.Storage_Type in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and MG.Remarks='Y')";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {


            // adding rows to the datatable

            txt_capuse.Text = ds.Tables[0].Rows[0]["capuses"].ToString();

        }
    }
    protected void WarehouseCapacityDetail()
    {
        string query = "SELECT COUNT(MG.Godown_ID) as NoOfGodown,SUM(MG.Godown_Scientific_Capacity)/10 as Capacity FROM [tbl_MetaData_GODOWN] as MG inner join tbl_MetaData_DISTRICT as MD on MD.District_Id=MG.DistrictId where MG.Hired_Type='Owned' and MD.Region_ID='" + Session["RegionId"].ToString() + "' and MG.Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and MG.Remarks='Y'";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {


            // adding rows to the datatable

            txtRNoOfGodown.Value = ds.Tables[0].Rows[0]["NoOfGodown"].ToString();
            txtRCapacityOfGodown.Value = ds.Tables[0].Rows[0]["Capacity"].ToString();

        }

    }
    protected void GetEmpDetail()
    {
        try
        {
            string query = "SELECT  [EmpName],[EmpPost],[EMPPostingdate] FROM [Intergrated_MP_STORAGE].[dbo].[BranchAuditEmp] where AuditID in (select MAX(AuditID) from dbo.RegionAudit where RegionId='" + Session["RegionId"].ToString() + "')";
            //string query = "SELECT  [AuditID],[EmpName],[EmpPost] FROM [Intergrated_MP_STORAGE].[dbo].[BranchAuditEmp] where AuditID='23010021512'";

            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();


            Dt1 = CreateTable();
            Session["dt1"] = Dt1;

            // adding rows to the datatable
            //DataRow dr = ((DataTable)Session["dt1"]).NewRow();
            ((DataTable)Session["dt1"]).AcceptChanges();

            //((DataTable)Session["dt1"]).Rows.Add(dr);
            ((DataTable)Session["dt1"]).AcceptChanges();


            da.Fill(Dt1);
            if (Dt1.Rows.Count > 0)
            {
                gdstackingdetails.DataSource = (DataTable)Session["dt1"];
                gdstackingdetails.DataBind();
            }
            else
            {

            }

        }

        catch (Exception ex)
        {

        }

    }
    protected void Hiredcapacity()
    {
        string query = "SELECT sum(MG.[Godown_Scientific_Capacity])/10 as Hiredcapacity FROM [tbl_MetaData_GODOWN] as MG inner join tbl_MetaData_DISTRICT as MD on MD.District_Id=MG.DistrictId where MG.Hired_Type in('Joint Venture(JV)','JointVenture(JV)','Hired') and MD.Region_ID='" + Session["RegionId"].ToString() + "' and MG.Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and MG.Remarks='Y'";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {


            // adding rows to the datatable

            txt_hiredcap.Text = ds.Tables[0].Rows[0]["Hiredcapacity"].ToString();

        }

    }
    protected void Hiredcapacityuses()
    {
        string query = "SELECT (sum([RecQty])-sum([DelQty]))/10 as Hireduses FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] where Region='" + Session["RegionId"].ToString() + "' and Godown_ID in( select Godown_ID FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] as MG inner join  tbl_MetaData_DISTRICT as MD on MD.District_Id=MG.DistrictId where MG.Hired_Type in('Joint Venture(JV)','JointVenture(JV)','Hired') and MD.Region_ID='" + Session["RegionId"].ToString() + "' and MG.Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and MG.Remarks='Y')";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            // adding rows to the datatable

            txt_hiredUSe.Text = ds.Tables[0].Rows[0]["Hireduses"].ToString();
        }

    }
    protected void JVScapacityuses()
    {
        string query = "SELECT (sum([RecQty])-sum([DelQty]))/10 as Hireduses FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] where Region='" + Session["RegionId"].ToString() + "' and Godown_ID in( select Godown_ID FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] as MG inner join  tbl_MetaData_DISTRICT as MD on MD.District_Id=MG.DistrictId where MG.Hired_Type in('Joint Venture(JV)','JointVenture(JV)','Hired') and MD.Region_ID='" + Session["RegionId"].ToString() + "' and MG.Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and MG.Remarks='Y')";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            // adding rows to the datatable

            txt_hiredUSe.Text = ds.Tables[0].Rows[0]["Hireduses"].ToString();
        }

    }
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        //Define the Columns
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("OB", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RptAmt", typeof(decimal)));
        dt.Columns.Add(new DataColumn("DBRM", typeof(decimal)));
        dt.Columns.Add(new DataColumn("TFCB", typeof(decimal)));
        dt.Columns.Add(new DataColumn("TFCImp", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Total", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ImpFPass", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ImpPass", typeof(decimal)));
        dt.Columns.Add(new DataColumn("WithAmt", typeof(decimal)));
        dt.Columns.Add(new DataColumn("DisAmt", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RetTBM", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RetTCB", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RetTCImp", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CB", typeof(decimal)));

        //Add a Dummy Data on Initial Load
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["OB"] = 0;
        dr["RptAmt"] = 0;
        dr["DBRM"] = 0;
        dr["TFCB"] = 0;
        dr["TFCImp"] = 0;
        dr["Total"] = 0;
        dr["ImpFPass"] = 0;
        dr["ImpPass"] = 0;
        dr["WithAmt"] = 0;
        dr["DisAmt"] = 0;
        dr["RetTBM"] = 0;
        dr["RetTCB"] = 0;
        dr["RetTCImp"] = 0;
        dr["CB"] = 0;

        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;
        //Bind the DataTable to the Grid
        gvImprest.DataSource = dt;
        gvImprest.DataBind();

        //Extract and Fill the DropDownList with Data
        DropDownList ddl1 = (DropDownList)gvImprest.Rows[0].Cells[1].FindControl("DropDownList1");
        FillDropDownList(ddl1);
    }
    private void AddNewRowToGrid()
    {

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                drCurrentRow["OB"] = 0;

                drCurrentRow["RptAmt"] = 0;
                drCurrentRow["DBRM"] = 0;
                drCurrentRow["TFCB"] = 0;
                drCurrentRow["TFCImp"] = 0;
                drCurrentRow["Total"] = 0;
                drCurrentRow["ImpFPass"] = 0;
                drCurrentRow["ImpPass"] = 0;
                drCurrentRow["WithAmt"] = 0;
                drCurrentRow["DisAmt"] = 0;
                drCurrentRow["RetTBM"] = 0;
                drCurrentRow["RetTCB"] = 0;
                drCurrentRow["RetTCImp"] = 0;
                drCurrentRow["CB"] = 0;
                //add new row to DataTable
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState
                ViewState["CurrentTable"] = dtCurrentTable;

                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    //extract the DropDownList Selected Items
                    DropDownList ddl1 = (DropDownList)gvImprest.Rows[i].Cells[1].FindControl("DropDownList1");
                    TextBox OB = (TextBox)gvImprest.Rows[i].Cells[1].FindControl("txtOB");
                    TextBox RptAmt = (TextBox)gvImprest.Rows[i].Cells[1].FindControl("txtRptAmt");
                    TextBox DBRM = (TextBox)gvImprest.Rows[i].Cells[1].FindControl("txtDBRM");
                    TextBox TFCB = (TextBox)gvImprest.Rows[i].Cells[1].FindControl("txtTFCB");
                    TextBox TFCImp = (TextBox)gvImprest.Rows[i].Cells[1].FindControl("txtTFCImp");
                    TextBox Total = (TextBox)gvImprest.Rows[i].Cells[1].FindControl("txtTotal");
                    TextBox ImpFPass = (TextBox)gvImprest.Rows[i].Cells[1].FindControl("txtImpFPass");
                    TextBox ImpPass = (TextBox)gvImprest.Rows[i].Cells[1].FindControl("txtImpPass");
                    TextBox WithAmt = (TextBox)gvImprest.Rows[i].Cells[1].FindControl("txtWithAmt");
                    TextBox DisAmt = (TextBox)gvImprest.Rows[i].Cells[1].FindControl("txtDisAmt");
                    TextBox RetTBM = (TextBox)gvImprest.Rows[i].Cells[1].FindControl("txtRetTBM");
                    TextBox RetTCB = (TextBox)gvImprest.Rows[i].Cells[1].FindControl("txtRetTCB");
                    TextBox RetTCImp = (TextBox)gvImprest.Rows[i].Cells[1].FindControl("txtRetTCImp");
                    TextBox CB = (TextBox)gvImprest.Rows[i].Cells[1].FindControl("txtCB");

                    dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                    //dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["OB"] = Convert.ToDecimal(OB.Text);
                    dtCurrentTable.Rows[i]["RptAmt"] = Convert.ToDecimal(RptAmt.Text);
                    dtCurrentTable.Rows[i]["DBRM"] = Convert.ToDecimal(DBRM.Text);
                    dtCurrentTable.Rows[i]["TFCB"] = Convert.ToDecimal(TFCB.Text);
                    dtCurrentTable.Rows[i]["TFCImp"] = Convert.ToDecimal(TFCImp.Text);
                    dtCurrentTable.Rows[i]["Total"] = Convert.ToDecimal(Total.Text);
                    dtCurrentTable.Rows[i]["ImpFPass"] = Convert.ToDecimal(ImpFPass.Text);
                    dtCurrentTable.Rows[i]["ImpPass"] = Convert.ToDecimal(ImpPass.Text);
                    dtCurrentTable.Rows[i]["WithAmt"] = Convert.ToDecimal(WithAmt.Text);
                    dtCurrentTable.Rows[i]["DisAmt"] = Convert.ToDecimal(DisAmt.Text);
                    dtCurrentTable.Rows[i]["RetTBM"] = Convert.ToDecimal(RetTBM.Text);
                    dtCurrentTable.Rows[i]["RetTCB"] = Convert.ToDecimal(RetTCB.Text);
                    dtCurrentTable.Rows[i]["RetTCImp"] = Convert.ToDecimal(RetTCImp.Text);
                    dtCurrentTable.Rows[i]["CB"] = Convert.ToDecimal(CB.Text);
                }

                //Rebind the Grid with the current data
                gvImprest.DataSource = dtCurrentTable;
                gvImprest.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Set the Previous Selected Items on Each DropDownList on Postbacks
                    DropDownList ddl1 = (DropDownList)gvImprest.Rows[rowIndex].Cells[1].FindControl("DropDownList1");

                    //Fill the DropDownList with Data
                    FillDropDownList(ddl1);
                    if (i < dt.Rows.Count - 1)
                    {
                        ddl1.ClearSelection();
                        ddl1.Items.FindByText(dt.Rows[i]["Column1"].ToString()).Selected = true;
                    }

                    rowIndex++;
                }
            }
        }
    }
    private ArrayList GetDummyData()
    {
        ArrayList arr = new ArrayList();
        arr.Add(new ListItem("January", "1"));
        arr.Add(new ListItem("February", "2"));
        arr.Add(new ListItem("March", "3"));
        arr.Add(new ListItem("April", "4"));
        arr.Add(new ListItem("May", "5"));
        arr.Add(new ListItem("June", "6"));
        arr.Add(new ListItem("July", "7"));
        arr.Add(new ListItem("August", "8"));
        arr.Add(new ListItem("September", "9"));
        arr.Add(new ListItem("October", "10"));
        arr.Add(new ListItem("November", "11"));
        arr.Add(new ListItem("December", "12"));
        return arr;
    }

    private void FillDropDownList(DropDownList ddl)
    {
        ArrayList arr = GetDummyData();
        foreach (ListItem item in arr)
        {
            ddl.Items.Add(item);
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Response.Redirect("AuditChangePassword.aspx");
    }
}
