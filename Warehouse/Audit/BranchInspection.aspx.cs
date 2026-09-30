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
//using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Specialized;
using System.Collections;


public partial class Inspection_BranchInspection : System.Web.UI.Page
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
    string gatePassid = string.Empty;
    string ArrivalStockid = string.Empty;
    SqlCommand cmd = null;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["login"] != null)
        {
              if (!IsPostBack)
        {
            owncapacity();
            owncapacityuses();
            capcapacity();
            capcapacityuses();
            Branchmgname();
            TextBox1_CalendarExtender.SelectedDate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            owngwnnum();
            //GodownList();
            GetEmpDetail();
            Hiredcapacity();
            Hiredcapacityuses();
            fillFinancialYear();
            SetInitialRow();
        }
        }
        else
        {
            Response.Redirect("InspectionLogin.aspx");

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
        DataColumn emppostingdate = new DataColumn("emppostingdate", Type.GetType("System.String"));

        dt.Columns.Add(empname);//Column is added to the DataTable
        dt.Columns.Add(emppost);//Column is added to the DataTable
        dt.Columns.Add(emppostingdate);//Column is added to the DataTable
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
                        dr["emppostingdate"] = TextBox3.Text;
                      
                            ((DataTable)Session["dt1"]).Rows.Add(dr);
                            ((DataTable)Session["dt1"]).AcceptChanges();
                            gdstackingdetails.DataSource = (DataTable)Session["dt1"];
                            gdstackingdetails.DataBind();

                            txtemppost.Value = "";
                            txtempname.Value = "";
                            TextBox3.Text = "";
                
            }
        }
        catch (Exception ex)
        {
           // Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in btnAddStack_Click has occured, try again'); </script> ");
        }
    }


    protected void gdwrdtl_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (ViewState["ckstat"].ToString() != "Delete")
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
            }
        }
        catch (Exception ex)
        {
            //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error gdStackingDetails_RowCreated has occured, try again'); </script> ");
        }
    }

    protected void gdwrdtl_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int i = e.RowIndex;
            if (gdwrdtl.Rows.Count < 1)
            {
                ViewState["ckstat"] = "Delete";
            }
            ((DataTable)Session["dt2"]).Rows[i].Delete();
            ((DataTable)Session["dt2"]).AcceptChanges();

            gdwrdtl.DataSource = (DataTable)Session["dt2"];
            gdwrdtl.DataBind();
            // chksum();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in gdStackingDetails_RowDeleting has occured, try again'); </script> ");
        }
    }
    private DataTable CreateTablewhr()
    {
        DataTable dt = new DataTable();//DataTable is created
        DataColumn whrnum = new DataColumn("whrnum", Type.GetType("System.String"));
        DataColumn whrdate = new DataColumn("whrdate", Type.GetType("System.String"));
        DataColumn comm = new DataColumn("comm", Type.GetType("System.String"));
        DataColumn qty = new DataColumn("qty", Type.GetType("System.String"));
        DataColumn bags = new DataColumn("bags", Type.GetType("System.String"));
        DataColumn qty2 = new DataColumn("qty2", Type.GetType("System.String"));
        DataColumn depos = new DataColumn("depos", Type.GetType("System.String"));
        DataColumn dateofliyan = new DataColumn("dateofliyan", Type.GetType("System.String"));
        dt.Columns.Add(whrnum);//Column is added to the DataTable
        dt.Columns.Add(whrdate);//Column is added to the DataTable
        dt.Columns.Add(comm);//Column is added to the DataTable
        dt.Columns.Add(qty);//Column is added to the DataTable
        dt.Columns.Add(bags);//Column is added to the DataTable
        dt.Columns.Add(qty2);//Column is added to the DataTable
        dt.Columns.Add(depos);//Column is added to the DataTable
        dt.Columns.Add(dateofliyan);//Column is added to the DataTable

        dt.AcceptChanges();
        return dt;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        bool checkstatus = false;
        try
        {
            if (txtwhrnum.Value == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आपने कर्मचारी का नाम नहीं लिखा है!'); </script> ");
                return;
            }
            
            else
            {


                if (Session["dt2"] == null)
                {
                    Dt2 = CreateTablewhr();
                    Session["dt2"] = Dt2;
                }
                string query = "SELECT  [Depositor_WHR_Id],[Depotid],[Commodity_Id],(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=[tbl_storage_Depositor_WHR_Relation].Commodity_Id) as comm,[Depositor_Name],[TotalBags_Received],[Total_Qty_Received] ,[MktValue_of_Commodity],convert(varchar(20),[WHR_Issue_Date],103) as whrdate FROM [Intergrated_MP_STORAGE].[dbo].[tbl_storage_Depositor_WHR_Relation] where Whr_No='" + txtwhrnum.Value + "' and BranchID='" + Session["UserID"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {


                // adding rows to the datatable
                DataRow dr = ((DataTable)Session["dt2"]).NewRow();
                ((DataTable)Session["dt2"]).AcceptChanges();
                dr["whrnum"] = ds.Tables[0].Rows[0]["Depositor_WHR_Id"].ToString();
                dr["whrdate"] = ds.Tables[0].Rows[0]["whrdate"].ToString();
                dr["comm"] = ds.Tables[0].Rows[0]["comm"].ToString();
                dr["qty"] = ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString();
                dr["bags"] = ds.Tables[0].Rows[0]["TotalBags_Received"].ToString();
                dr["qty2"] = ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString();
                dr["depos"] = txtrahanbank.Value;
                dr["dateofliyan"] = txtrahandate.Text;
                ((DataTable)Session["dt2"]).Rows.Add(dr);
                ((DataTable)Session["dt2"]).AcceptChanges();
                gdwrdtl.DataSource = (DataTable)Session["dt2"];
                gdwrdtl.DataBind();



            }
            }
        }
        catch (Exception ex)
        {
            // Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in btnAddStack_Click has occured, try again'); </script> ");
        }
    }

    //protected void GodownList()
    //{
    //    string query = "SELECT  [Godown_ID],[Godown_Name] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where BranchID='" + Session["UserID"].ToString() + "' and Remarks='Y'";
    //    SqlCommand cmd = new SqlCommand(query, Con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {

    //        ddlgodown.DataSource = ds.Tables[0];
    //        ddlgodown.DataTextField = "Godown_Name";
    //        ddlgodown.DataValueField = "Godown_id";
    //        ddlgodown.DataBind();
    //        ddlgodown.Items.Insert(0, "--Select--");



    //    }

    //}


    protected void owncapacity()
    {
        string query = "SELECT sum([Godown_Scientific_Capacity])/10 as owncapacity FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type='Owned' and BranchID='" + Session["UserID"].ToString() + "' and Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and Remarks='Y'";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {


            // adding rows to the datatable

            txt_ownCap.Text = ds.Tables[0].Rows[0]["owncapacity"].ToString();
         
        }

    }

    protected void owngwnnum()
    {
        string query = "SELECT  count(isnull([Godown_ID],0)) as owngodown FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type='Owned' and BranchID='" + Session["UserID"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {


            // adding rows to the datatable

            Text3.Value = ds.Tables[0].Rows[0]["owngodown"].ToString();




        }

    }

    protected void capcapacity()
    {
        string query = "SELECT isnull(sum([Godown_Scientific_Capacity])/10,0) as capcapacity FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type='Owned' and BranchID='" + Session["UserID"].ToString() + "' and Storage_Type  in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and Remarks='Y'";
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

    protected void Branchmgname()
    {
        string query = "SELECT [DepotName],[NodalOfficeName] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_DEPOT] where BranchId='" + Session["UserID"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {


            // adding rows to the datatable
            txt_BranchName.Value = ds.Tables[0].Rows[0]["DepotName"].ToString();
            txt_bm.Value = ds.Tables[0].Rows[0]["NodalOfficeName"].ToString();

        }

    }
    protected void owncapacityuses()
    {
        string query = "SELECT (sum([RecQty])-sum([DelQty]))/10 as ownuses FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] where BranchID='" + Session["UserID"].ToString() + "' and Godown_ID in ( select Godown_ID FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type='Owned' and BranchID='" + Session["UserID"].ToString() + "' and Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and Remarks='Y')";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            // adding rows to the datatable

            txt_ownuse.Text = ds.Tables[0].Rows[0]["ownuses"].ToString();
        }

    }

    protected void capcapacityuses()
    {
        string query = "SELECT isnull((sum([RecQty])-sum([DelQty]))/10,0) as capuses FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] where BranchID='" + Session["UserID"].ToString() + "' and Godown_ID in ( select Godown_ID FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type='Owned' and BranchID='" + Session["UserID"].ToString() + "' and Storage_Type  in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and Remarks='Y')";
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

    protected void GetEmpDetail()
    {
        try
        {
            string query = "SELECT  [EmpName],[EmpPost],[EMPPostingdate] FROM [Intergrated_MP_STORAGE].[dbo].[BranchAuditEmp] where AuditID in (select MAX(AuId) from dbo.BranchAudit where BranchId='" + Session["UserID"].ToString() + "')";
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


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_AuditerName.Value == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('अंकेक्षणकर्ता अधिकारी का नाम लिखें')", true);
            }
            else if (TextBox1.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('अंकेक्षण संपादन की दिनांक')", true);
            }
            else if (txt_bm.Value == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('ब्रांच मेनेजर का नाम लिखें')", true);
            }
            else if (TextBox2.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('ब्रांच मेनेजर शाखा में कब से पदस्थ हैं ')", true);
            }
            else if (ddlFinncialYear.SelectedItem.Text=="--Select--")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('इम्प्रेस्ट व्ययो का वित्तीय वर्ष चुने')", true);
            }
            else
            {
                string auid = "";
                string QueryMax = "select isnull(Max(InId),0)+1 from BranchAudit where  BranchId='" + Session["UserID"].ToString() + "' ";
                SqlCommand cmd2 = new SqlCommand(QueryMax, Con); // check WhrId present in whr_status table
                Con.Open();
                string str3 = cmd2.ExecuteScalar().ToString();
                if ((str3 == String.Empty) || str3 == "")
                {
                    str3 = "0";
                }
                if (Convert.ToInt64(str3) != 0)
                {
                    string Depotid = Session["UserID"].ToString();
                    auid = Depotid + System.DateTime.Now.Date.ToString("yy") + Convert.ToString(Convert.ToInt64(str3));
                }
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                //string sql = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[BranchAudit] VALUES (@AuditerName,@AuditerPost,@BranchId,@BranchName,@AuditDate,@BranchManName,@BranchManPost ,@BranchPostingDate,@OwnCap,@OwnUses,@CapCap,@CapUses,@HiredCap,@HiredUSes,@ObtnBsns,@ObtnableBsns,@AttemntfrmBMForBsns,@AimofBranch,@RetioofAimtilOditdate,@Income,@lakchapraptiparsent,@lakhasekamjada,@rokigairashi,@overandabovedtl,@commoditydtl,@stackingvegyanik,@skandhmekamiadhik,@spelage,@lambitbhugtan,@lijarent,@viybildtl ,@stafperfom,@RMTeep ,@BankRin,@Teep,@Trutipatrak,@CreatedBy,@CreatedDate,@SandharitRcd,@InId,@AuId,@agrim,@lambitdabe,@vividhstock,@gunbatta,@nijigodownno,@nijigodamchanta,@surakchaupkaran,@owngwnno,@barsikrakh,@veshashrakh,@vaundriwall,@vyay,@TDS1,@TDS2,@MoneyTrans,@WhrCharges,@LbrCharges,@JVS1,@JVS12,@Financial_Year)";
                string sql = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[BranchAudit] VALUES (@AuditerName,@AuditerPost,@BranchId,@BranchName,@AuditDate,@BranchManName,@BranchManPost ,@BranchPostingDate,@OwnCap,@OwnUses,@CapCap,@CapUses,@HiredCap,@HiredUSes,@ObtnBsns,@ObtnableBsns,@AttemntfrmBMForBsns,@AimofBranch,@RetioofAimtilOditdate,@Income,@lakchapraptiparsent,@lakhasekamjada,@rokigairashi,@overandabovedtl,@commoditydtl,@stackingvegyanik,@skandhmekamiadhik,@spelage,@lambitbhugtan,@lijarent,@viybildtl ,@stafperfom,@RMTeep ,@BankRin,@Teep,@Trutipatrak,@CreatedBy,@CreatedDate,@SandharitRcd,@InId,@AuId,@agrim,@lambitdabe,@vividhstock,@gunbatta,@nijigodownno,@nijigodamchanta,@surakchaupkaran,@owngwnno,@barsikrakh,@veshashrakh,@vaundriwall,@vyay,@TDS1,@TDS2,@MoneyTrans,@WhrCharges,@LbrCharges,@JVS1,@JVS12,@Financial_Year,@CashBookBalance,@ImprestCashBook,@NirmanImprestCB,@SRepairWork,@PostageStamp,@RevenueStamp,@BankStatementD,@SChargesAmt,@SChargesMilan,@SChargesStatus,@PurveDeyak,@ChaluVarshDeyak,@PurveVarshRashi,@PurveVarshDeyak,@RashiLambitKaran)";

                SqlCommand cmd = new SqlCommand(sql, Con);
                SqlParameter[] prms = new SqlParameter[76];


                prms[0] = new SqlParameter("@AuditerName", SqlDbType.NVarChar, 50);
                prms[0].Value = txt_AuditerName.Value;
                prms[1] = new SqlParameter("@AuditerPost", SqlDbType.NVarChar, 50);
                prms[1].Value = txt_AuditerPost.Value;
                prms[2] = new SqlParameter("@BranchId", SqlDbType.VarChar, 20);
                prms[2].Value = Session["UserID"].ToString();
                prms[3] = new SqlParameter("@BranchName", SqlDbType.NVarChar, 50);
                prms[3].Value = txt_BranchName.Value;
                prms[4] = new SqlParameter("@AuditDate", SqlDbType.VarChar, 30);
                prms[4].Value = TextBox1.Text;
                prms[5] = new SqlParameter("@BranchManName", SqlDbType.NVarChar, 50);
                prms[5].Value = txt_bm.Value;
                prms[6] = new SqlParameter("@BranchManPost", SqlDbType.NVarChar, 50);
                prms[6].Value = txt_bm_post.Value;
                prms[7] = new SqlParameter("@BranchPostingDate", SqlDbType.VarChar, 30);
                prms[7].Value = getDate_MDY(TextBox2.Text);
                prms[8] = new SqlParameter("@OwnCap", SqlDbType.Decimal);
                decimal owncap = 0;
                if (txt_ownCap.Text == "")
                {
                    owncap = 0;
                }
                else
                {
                    owncap = Convert.ToDecimal(txt_ownCap.Text);
                }
                prms[8].Value = owncap;

                decimal ownuses = 0;
                if (txt_ownuse.Text == "")
                {
                    ownuses = 0;
                }
                else
                {
                    ownuses = Convert.ToDecimal(txt_ownuse.Text);
                }

                prms[9] = new SqlParameter("@OwnUses", SqlDbType.Decimal);
                prms[9].Value = ownuses;

                decimal capcap = 0;
                if (txt_capcap.Text == "")
                {
                    capcap = 0;
                }
                else
                {
                    capcap = Convert.ToDecimal(txt_capcap.Text);
                }

                prms[10] = new SqlParameter("@CapCap", SqlDbType.Decimal);
                prms[10].Value = capcap;

                decimal capuses = 0;
                if (txt_capuse.Text == "")
                {
                    capuses = 0;
                }
                else
                {
                    capuses = Convert.ToDecimal(txt_capuse.Text);
                }

                prms[11] = new SqlParameter("@CapUses", SqlDbType.Decimal);
                prms[11].Value = capuses;

                decimal hiredcap = 0;
                if (txt_hiredcap.Text == "")
                {
                    hiredcap = 0;
                }
                else
                {

                    hiredcap = Convert.ToDecimal(txt_hiredcap.Text);
                }

                prms[12] = new SqlParameter("@HiredCap", SqlDbType.Decimal);
                prms[12].Value = hiredcap;

                decimal hireduses = 0;
                if (txt_hiredUSe.Text == "")
                {
                    hireduses = 0;
                }
                else
                {
                    hireduses = Convert.ToDecimal(txt_hiredUSe.Text);
                }
                prms[13] = new SqlParameter("@HiredUSes", SqlDbType.Decimal);
                prms[13].Value = hireduses;
                prms[14] = new SqlParameter("@ObtnBsns", SqlDbType.NVarChar, 550);
                prms[14].Value = txt_ObtnBsns.Value;
                prms[15] = new SqlParameter("@ObtnableBsns", SqlDbType.NVarChar, 550);
                prms[15].Value = txt_ObtnableBsns.Value;
                prms[16] = new SqlParameter("@AttemntfrmBMForBsns", SqlDbType.NVarChar, 550);
                prms[16].Value = txt_AttemntfrmBMForBsns.Value;
                prms[17] = new SqlParameter("@AimofBranch", SqlDbType.NVarChar, 550);
                prms[17].Value = Text17.Value;
                prms[18] = new SqlParameter("@RetioofAimtilOditdate", SqlDbType.VarChar, 50);
                prms[18].Value = Text18.Value;
                prms[19] = new SqlParameter("@Income", SqlDbType.VarChar, 50);
                prms[19].Value = Text19.Value;

                prms[20] = new SqlParameter("@lakchapraptiparsent", SqlDbType.VarChar, 50);
                prms[20].Value = Text21.Value;
                prms[21] = new SqlParameter("@lakhasekamjada", SqlDbType.NVarChar, 450);
                prms[21].Value = Text22.Value;
                prms[22] = new SqlParameter("@rokigairashi", SqlDbType.NVarChar, 450);
                prms[22].Value = Text23.Value;
                prms[23] = new SqlParameter("@overandabovedtl", SqlDbType.NVarChar, 450);
                prms[23].Value = Text24.Value;
                prms[24] = new SqlParameter("@commoditydtl", SqlDbType.NVarChar, 450);
                prms[24].Value = Textarea10.Value;
                prms[25] = new SqlParameter("@stackingvegyanik", SqlDbType.NVarChar, 450);
                prms[25].Value = Textarea9.Value;
                prms[26] = new SqlParameter("@skandhmekamiadhik", SqlDbType.NVarChar, 450);
                prms[26].Value = Textarea8.Value;
                prms[27] = new SqlParameter("@spelage", SqlDbType.NVarChar, 450);
                prms[27].Value = Textarea7.Value;
                prms[28] = new SqlParameter("@lambitbhugtan", SqlDbType.NVarChar, 450);
                prms[28].Value = Textarea6.Value;
                prms[29] = new SqlParameter("@lijarent", SqlDbType.NVarChar, 450);
                prms[29].Value = Textarea5.Value;
                prms[30] = new SqlParameter("@viybildtl", SqlDbType.NVarChar, 450);
                prms[30].Value = Textarea4.Value;
                prms[31] = new SqlParameter("@stafperfom", SqlDbType.NVarChar, 450);
                prms[31].Value = Text33.Value;

                prms[32] = new SqlParameter("@RMTeep", SqlDbType.NVarChar, 450);
                prms[32].Value = TextaT.Value;
                prms[33] = new SqlParameter("@BankRin", SqlDbType.NVarChar, 450);
                prms[33].Value = Text35.Value;
                prms[34] = new SqlParameter("@Teep", SqlDbType.NVarChar, 500);
                prms[34].Value = Textarea1.Value;
                prms[35] = new SqlParameter("@Trutipatrak", SqlDbType.NVarChar, 500);
                prms[35].Value = txtCaddress.Value;
                prms[36] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 20);
                prms[36].Value = ip;
                prms[37] = new SqlParameter("@CreatedDate", SqlDbType.DateTime);
                prms[37].Value = DateTime.Now;
                prms[38] = new SqlParameter("@SandharitRcd", SqlDbType.NVarChar, 450);
                prms[38].Value = Textarea3.Value;
                prms[39] = new SqlParameter("@InId", SqlDbType.Int);
                prms[39].Value = str3;
                prms[40] = new SqlParameter("@AuId", SqlDbType.VarChar, 20);
                prms[40].Value = auid;


                prms[41] = new SqlParameter("@agrim", SqlDbType.NVarChar, 450);
                prms[41].Value = Textarea11.Value;
                prms[42] = new SqlParameter("@lambitdabe", SqlDbType.NVarChar, 450);
                prms[42].Value = Textarea12.Value;

                prms[43] = new SqlParameter("@vividhstock", SqlDbType.NVarChar, 450);
                prms[43].Value = Textarea13.Value;
                prms[44] = new SqlParameter("@gunbatta", SqlDbType.NVarChar, 450);
                prms[44].Value = Textarea14.Value;
                prms[45] = new SqlParameter("@nijigodownno", SqlDbType.NVarChar, 20);
                prms[45].Value = Text1.Value;
                prms[46] = new SqlParameter("@nijigodamchanta", SqlDbType.NVarChar, 50);
                prms[46].Value = Text2.Value;
                prms[47] = new SqlParameter("@surakchaupkaran", SqlDbType.NVarChar, 450);
                prms[47].Value = Textarea17.Value;
                prms[48] = new SqlParameter("@owngwnno", SqlDbType.VarChar, 30);
                prms[48].Value = Text3.Value;
                prms[49] = new SqlParameter("@barsikrakh", SqlDbType.NVarChar, 450);
                prms[49].Value = Text4.Value;
                prms[50] = new SqlParameter("@veshashrakh", SqlDbType.NVarChar, 450);
                prms[50].Value = Textarea20.Value;
                prms[51] = new SqlParameter("@vaundriwall", SqlDbType.NVarChar, 450);
                prms[51].Value = Textarea21.Value;
                prms[52] = new SqlParameter("@vyay", SqlDbType.VarChar, 50);
                prms[52].Value = Text20.Value;
               
                prms[53] = new SqlParameter("@TDS1", SqlDbType.NVarChar, 200);
                prms[53].Value = txttds1.Value;
                prms[54] = new SqlParameter("@TDS2", SqlDbType.NVarChar, 200);
                prms[54].Value = txttds2.Value;
                prms[55] = new SqlParameter("@MoneyTrans", SqlDbType.NVarChar, 200);
                prms[55].Value = txtdr.Value;
                prms[56] = new SqlParameter("@WhrCharges", SqlDbType.NVarChar, 200);
                prms[56].Value = txtwc.Value;
                prms[57] = new SqlParameter("@LbrCharges", SqlDbType.NVarChar, 200);
                prms[57].Value = txtct.Value;
                prms[58] = new SqlParameter("@JVS1", SqlDbType.NVarChar, 200);
                prms[58].Value = txtjvs1.Value;
                prms[59] = new SqlParameter("@JVS12", SqlDbType.NVarChar, 200);
                prms[59].Value = txtjvs2.Value;
                prms[60] = new SqlParameter("@Financial_Year", SqlDbType.VarChar, 10);
                prms[60].Value = ddlFinncialYear.SelectedItem.Text;

                prms[61] = new SqlParameter("@CashBookBalance", SqlDbType.Decimal);
                prms[61].Value = txtCBB.Value;
                prms[62] = new SqlParameter("@ImprestCashBook", SqlDbType.Decimal);
                prms[62].Value = txtICB.Value;
                prms[63] = new SqlParameter("@NirmanImprestCB", SqlDbType.Decimal);
                prms[63].Value = txtNICB.Value;
                prms[64] = new SqlParameter("@SRepairWork", SqlDbType.NVarChar, 150);
                prms[64].Value = txtSRK.Value;
                prms[65] = new SqlParameter("@PostageStamp", SqlDbType.Decimal);
                prms[65].Value = txtPS.Value;
                prms[66] = new SqlParameter("@RevenueStamp", SqlDbType.Decimal);
                prms[66].Value = txtRS.Value;
                prms[67] = new SqlParameter("@BankStatementD", SqlDbType.NVarChar, 150);
                prms[67].Value = txtBST.Value;
                prms[68] = new SqlParameter("@SChargesAmt", SqlDbType.NVarChar, 150);
                prms[68].Value = txtDS.Value;
                prms[69] = new SqlParameter("@SChargesMilan", SqlDbType.NVarChar, 150);
                prms[69].Value = txtBSPD.Value;
                prms[70] = new SqlParameter("@SChargesStatus", SqlDbType.NVarChar, 150);
                prms[70].Value = txtBS1.Value;
                prms[71] = new SqlParameter("@PurveDeyak", SqlDbType.NVarChar, 150);
                prms[71].Value = txtPD.Value;
                prms[72] = new SqlParameter("@ChaluVarshDeyak", SqlDbType.Decimal);
                prms[72].Value = txtCYDP.Value;
                prms[73] = new SqlParameter("@PurveVarshRashi", SqlDbType.Decimal);
                prms[73].Value = txtLB.Value;
                prms[74] = new SqlParameter("@PurveVarshDeyak", SqlDbType.Decimal);
                prms[74].Value = txtPVKD.Value;
                prms[75] = new SqlParameter("@RashiLambitKaran", SqlDbType.NVarChar, 150);
                prms[75].Value = txtRLKR.Value;
             
                
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
                            string sql3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[BranchAuditEmp] VALUES (@AuditID,@EmpName,@EmpPost,@EMPTYPE,@EMPPostingdate)";

                            SqlCommand cmd3 = new SqlCommand(sql3, Con);
                            SqlParameter[] prms3 = new SqlParameter[5];


                            prms3[0] = new SqlParameter("@AuditID", SqlDbType.VarChar, 20);
                            prms3[0].Value = auid;
                            prms3[1] = new SqlParameter("@EmpName", SqlDbType.NVarChar, 50);
                            prms3[1].Value = gdstackingdetails.Rows[k].Cells[1].Text.ToString();
                            prms3[2] = new SqlParameter("@EmpPost", SqlDbType.NVarChar, 20);
                            prms3[2].Value = gdstackingdetails.Rows[k].Cells[2].Text.ToString();
                            prms3[3] = new SqlParameter("@EMPTYPE", SqlDbType.VarChar, 2);
                            prms3[3].Value = "B";
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
                        }
                    }
                   
                }
                if (gdwrdtl.Rows.Count > 0)
                {
                    int j;
                    for (j = 0; j < gdwrdtl.Rows.Count; j++)
                    {

                        string datestring = gdwrdtl.Rows[j].Cells[8].Text.ToString();
                        string[] tempsplit = datestring.Split('/');
                        string joinstring = "/";
                        string newdatefrom = tempsplit[2] + joinstring + tempsplit[1] + joinstring + tempsplit[0];

                        string sql4 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[BranchAuditWHRDtl] VALUES (@AuditID,@WHRID,@LiyanPatraDate,@BankName)";

                        SqlCommand cmd4 = new SqlCommand(sql4, Con);
                        SqlParameter[] prms4 = new SqlParameter[4];


                        prms4[0] = new SqlParameter("@AuditID", SqlDbType.VarChar, 20);
                        prms4[0].Value = auid;
                        prms4[1] = new SqlParameter("@WHRID", SqlDbType.VarChar, 20);
                        prms4[1].Value = gdwrdtl.Rows[j].Cells[1].Text.ToString();
                        prms4[2] = new SqlParameter("@LiyanPatraDate", SqlDbType.DateTime);
                        prms4[2].Value = newdatefrom;
                        prms4[3] = new SqlParameter("@BankName", SqlDbType.NVarChar, 30);
                        prms4[3].Value = gdwrdtl.Rows[j].Cells[7].Text.ToString();

                        int CT4 = 0;
                        cmd4.Parameters.AddRange(prms4);
                        Con.Open();
                        CT4 = cmd4.ExecuteNonQuery();
                        Con.Close();
                        if (CT4 > 0)
                        {
                           
                        }
                    }
                }
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
                        prms5[16] = new SqlParameter("@Remark", SqlDbType.VarChar,20);
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
                }
               

                Session["Auid"] = auid;
                Response.Redirect("BranchInspecPrint.aspx");
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("InspectionLogin.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("OldBranchInsp.aspx");
    }
    protected void Hiredcapacity()
    {
        //string query = "SELECT sum([Godown_Scientific_Capacity])/10 as owncapacity FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type='Owned' and BranchID='" + Session["UserID"].ToString() + "' and Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and Remarks='Y'";
        string query = "SELECT sum([Godown_Scientific_Capacity])/10 as Hiredcapacity FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type in('Joint Venture(JV)','JointVenture(JV)','Hired') and BranchID='" + Session["UserID"].ToString() + "' and Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and Remarks='Y'";
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
        string query = "SELECT (sum([RecQty])-sum([DelQty]))/10 as Hireduses FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] where BranchID='" + Session["UserID"].ToString() + "' and Godown_ID in (select Godown_ID FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type in('Joint Venture(JV)','JointVenture(JV)','Hired') and BranchID='2301002' and Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and Remarks='Y')";
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
}