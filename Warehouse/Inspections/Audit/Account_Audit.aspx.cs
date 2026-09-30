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


public partial class Inspections_Audit_Account_Audit : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
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
    decimal total1 = 0;
    decimal total2 = 0;
    decimal total3 = 0;
    decimal total4 = 0;
    decimal total5 = 0;
    decimal total6 = 0;
    decimal total7 = 0;
    decimal total8 = 0;
    decimal total9 = 0;
    decimal total10 = 0;
    decimal total11 = 0;
    decimal total12 = 0;
    decimal total13 = 0;
    decimal total14 = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["login"] != null)
        //{
        if (!IsPostBack)
        {
            //CheckFinalSUbmit();
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
            getImprestDetail();
            getempdata();
            FillWHRdata();
            Filldata();
        }
        //}
        //else
        //{
        //    Response.Redirect("InspectionLogin.aspx");

        //}
    }

    public void Filldata()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();

        }
        SqlCommand cmd = new SqlCommand("Get_Account_Audit_For_Branch_Insp_Acco_Auditer", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BranchId", Session["hdnbranchid"].ToString());
        cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnEmployeeID"].ToString());
        cmd.Parameters.AddWithValue("@Inspection_Quarter", Session["hdninsptype"].ToString());
        cmd.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
        cmd.Parameters.AddWithValue("@Financial_Year_Insp", Session["hdnfinancialYear"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        if (ds.Tables[0].Rows.Count > 0)
        {
            //branchname = ds.Tables[0].Rows[0]["BranchName"].ToString();
            //  Session["BranchType"] = ds.Tables[0].Rows[0]["BranchTypeID"].ToString();
            txt_AuditerName.Text = ds.Tables[0].Rows[0]["AuditerName"].ToString() + "/" + ds.Tables[0].Rows[0]["AuditerPost"].ToString();
            //lblbranchauditdate.Text = ds.Tables[0].Rows[0]["BranchName"].ToString() + "/" + ds.Tables[0].Rows[0]["AuditDate"].ToString();
            //lblbmpost.Text = ds.Tables[0].Rows[0]["BranchManName"].ToString() + "/" + ds.Tables[0].Rows[0]["BranchManPost"].ToString();
            //lblpostingdate.Text = ds.Tables[0].Rows[0]["BranchPostingDate"].ToString();
            //lblowncap.Text = ds.Tables[0].Rows[0]["OwnCap"].ToString();
            txt_ownuse.Text = ds.Tables[0].Rows[0]["OwnUses"].ToString();
            try
            {
                decimal ownper = (Convert.ToDecimal(ds.Tables[0].Rows[0]["OwnUses"].ToString()) * 100) / Convert.ToDecimal(ds.Tables[0].Rows[0]["OwnCap"].ToString());
                txt_ownuse.Text = Math.Round(ownper, 2, MidpointRounding.AwayFromZero).ToString();
            }
            catch (Exception ex)
            {

                txt_ownuse.Text = "0";
            }
            //lblcapcap.Text = ds.Tables[0].Rows[0]["CapCap"].ToString();
            txt_capuse.Text = ds.Tables[0].Rows[0]["CapUses"].ToString();
            try
            {
                decimal capper = (Convert.ToDecimal(ds.Tables[0].Rows[0]["CapUses"].ToString()) * 100) / Convert.ToDecimal(ds.Tables[0].Rows[0]["CapCap"].ToString());
                txt_capuse.Text = Math.Round(capper, 2, MidpointRounding.AwayFromZero).ToString();
            }
            catch (Exception ex)
            {
                txt_capuse.Text = "0";
            }
            //lblhiredcap.Text = ds.Tables[0].Rows[0]["HiredCap"].ToString();
            txt_hiredUSe.Text = ds.Tables[0].Rows[0]["HiredUSes"].ToString();
            try
            {
                decimal hiredper = (Convert.ToDecimal(ds.Tables[0].Rows[0]["HiredUSes"].ToString()) * 100) / Convert.ToDecimal(ds.Tables[0].Rows[0]["HiredCap"].ToString());
                txt_hiredUSe.Text = Math.Round(hiredper, 2, MidpointRounding.AwayFromZero).ToString();
            }
            catch (Exception ex)
            {
                txt_hiredUSe.Text = "0";
            }
            //try
            //{
            //    decimal totalcap = Convert.ToDecimal(ds.Tables[0].Rows[0]["OwnCap"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[0]["CapCap"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[0]["HiredCap"].ToString());
            //    lbltotalcap.Text = totalcap.ToString();
            //}
            //catch
            //{
            //    lbltotalcap.Text = "0";
            //}
            //try
            //{
            //    decimal totaluses = Convert.ToDecimal(ds.Tables[0].Rows[0]["OwnUses"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[0]["CapUses"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[0]["HiredUSes"].ToString());
            //    lblbtotaluses.Text = totaluses.ToString();
            //}
            //catch
            //{
            //    lblbtotaluses.Text = "0";
            //}
            //decimal totalper = (Convert.ToDecimal(lblbtotaluses.Text) * 100) / Convert.ToDecimal(lbltotalcap.Text);
            //lbltotalper.Text = Math.Round(totalper, 2, MidpointRounding.AwayFromZero).ToString();
            txt_ObtnBsns.Text = ds.Tables[0].Rows[0]["ObtnBsns"].ToString();
            txt_ObtnableBsns.Text = ds.Tables[0].Rows[0]["ObtnableBsns"].ToString();
            txt_AttemntfrmBMForBsns.Text = ds.Tables[0].Rows[0]["AttemntfrmBMForBsns"].ToString();
            txtAimofBranch.Text = ds.Tables[0].Rows[0]["AimofBranch"].ToString();
            txtRetioofAimtilOditdate.Text = ds.Tables[0].Rows[0]["RetioofAimtilOditdate"].ToString();
            txtIncome.Text = ds.Tables[0].Rows[0]["Income"].ToString();
            Text20.Text = ds.Tables[0].Rows[0]["lakchapraptiparsent"].ToString();
            Text21.Text = ds.Tables[0].Rows[0]["lakhasekamjada"].ToString();
            Text22.Text = ds.Tables[0].Rows[0]["rokigairashi"].ToString();
            Text23.Text = ds.Tables[0].Rows[0]["overandabovedtl"].ToString();
            Textarea8.Value = ds.Tables[0].Rows[0]["commoditydtl"].ToString();
            //Label29.Text = "संलग्न है।";
            //Label30.Text = "संलग्न है।";

            Textarea9.Value = ds.Tables[0].Rows[0]["stackingvegyanik"].ToString();
            Textarea8.Value = ds.Tables[0].Rows[0]["skandhmekamiadhik"].ToString();
            Textarea7.Value = ds.Tables[0].Rows[0]["spelage"].ToString();
            txtLB.Text = ds.Tables[0].Rows[0]["lambitbhugtan"].ToString();
            Textarea5.Value = ds.Tables[0].Rows[0]["lijarent"].ToString();
            Textarea4.Value = ds.Tables[0].Rows[0]["viybildtl"].ToString();
            Textarea3.Value = ds.Tables[0].Rows[0]["SandharitRcd"].ToString();
            Text33.Value = ds.Tables[0].Rows[0]["stafperfom"].ToString();
            TextaT.Value = ds.Tables[0].Rows[0]["RMTeep"].ToString();
            Text35.Text = ds.Tables[0].Rows[0]["BankRin"].ToString();
            Textarea1.Value = ds.Tables[0].Rows[0]["Teep"].ToString();
            txtCaddress.Value = ds.Tables[0].Rows[0]["Trutipatrak"].ToString();
            //lblbranch2.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
            //lblbranch3.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
            //lblbranch4.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
            //lblbrnach5.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
            //lblbranch6.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
            //lblbranch3.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
            //lblbranch44.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
            //lblauditdate2.Text = ds.Tables[0].Rows[0]["AuditDate"].ToString();
            //lblauditdate3.Text = ds.Tables[0].Rows[0]["AuditDate"].ToString();
            //lbldate7.Text = ds.Tables[0].Rows[0]["AuditDate"].ToString();
            //lbldate8.Text = ds.Tables[0].Rows[0]["AuditDate"].ToString();
            //lbldate9.Text = ds.Tables[0].Rows[0]["AuditDate"].ToString();
            //lbldate10.Text = ds.Tables[0].Rows[0]["AuditDate"].ToString();
            //lbldategdn.Text = ds.Tables[0].Rows[0]["AuditDate"].ToString();
            Textarea11.Value = ds.Tables[0].Rows[0]["agrim"].ToString();
            Textarea12.Value = ds.Tables[0].Rows[0]["lambitdabe"].ToString();
            Text20.Text = ds.Tables[0].Rows[0]["vyay"].ToString();
            Textarea13.Value = ds.Tables[0].Rows[0]["vividhstock"].ToString();
            Textarea14.Value = ds.Tables[0].Rows[0]["gunbatta"].ToString();
            Text1.Text = ds.Tables[0].Rows[0]["nijigodownno"].ToString();
            Text2.Text = ds.Tables[0].Rows[0]["nijigodamchanta"].ToString();
            Textarea17.Value = ds.Tables[0].Rows[0]["surakchaupkaran"].ToString();
            Text3.Text = ds.Tables[0].Rows[0]["owngwnno"].ToString();
            Text4.Text = ds.Tables[0].Rows[0]["barsikrakh"].ToString();
            Textarea20.Value = ds.Tables[0].Rows[0]["veshashrakh"].ToString();
            Textarea21.Value = ds.Tables[0].Rows[0]["vaundriwall"].ToString();
            ////////////new//////////////////////
            txttds1.Value = ds.Tables[0].Rows[0]["TDS1"].ToString();
            txttds2.Value = ds.Tables[0].Rows[0]["TDS2"].ToString();
            txtdr.Value = ds.Tables[0].Rows[0]["MoneyTrans"].ToString();
            txtwc.Value = ds.Tables[0].Rows[0]["WhrCharges"].ToString();
            txtct.Value = ds.Tables[0].Rows[0]["LbrCharges"].ToString();
            txtjvs1.Value = ds.Tables[0].Rows[0]["JVS1"].ToString();
            txtjvs2.Value = ds.Tables[0].Rows[0]["JVS2"].ToString();
            //lblFy.Text = ds.Tables[0].Rows[0]["Financial_Year"].ToString();
            ////////////////////////////////////////////////
            txtCBB.Text = ds.Tables[0].Rows[0]["CashBookBalance"].ToString();
            txtICB.Text = ds.Tables[0].Rows[0]["ImprestCashBook"].ToString();
            txtNICB.Text = ds.Tables[0].Rows[0]["NirmanImprestCB"].ToString();
            txtSRK.Text = ds.Tables[0].Rows[0]["SRepairWork"].ToString();
            txtPS.Text = ds.Tables[0].Rows[0]["PostageStamp"].ToString();
            txtRS.Text = ds.Tables[0].Rows[0]["RevenueStamp"].ToString();

            txtBST.Text = ds.Tables[0].Rows[0]["BankStatementD"].ToString();

            txtDS.Text = ds.Tables[0].Rows[0]["SChargesAmt"].ToString();
            txtBS1.Text = ds.Tables[0].Rows[0]["SChargesMilan"].ToString();

            txtBS1.Text = ds.Tables[0].Rows[0]["SChargesStatus"].ToString();
            txtPD.Text = ds.Tables[0].Rows[0]["PurveDeyak"].ToString();

            txtCYDP.Text = ds.Tables[0].Rows[0]["ChaluVarshDeyak"].ToString();
            txtLB.Text = ds.Tables[0].Rows[0]["PurveVarshRashi"].ToString();
            txtPVKD.Text = ds.Tables[0].Rows[0]["PurveVarshDeyak"].ToString();
            txtRLKR.Text = ds.Tables[0].Rows[0]["RashiLambitKaran"].ToString();


        }
        else
        {

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
            emptbl.Visible = true;
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
            if (txtempname.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('आपने कर्मचारी का नाम नहीं लिखा है!'); </script> ");
                return;
            }
            else if (txtemppost.Text == "")
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
                dr["empname"] = txtempname.Text;
                dr["emppost"] = txtemppost.Text;
                dr["emppostingdate"] = TextBox3.Text;

                ((DataTable)Session["dt1"]).Rows.Add(dr);
                ((DataTable)Session["dt1"]).AcceptChanges();
                gdstackingdetails.DataSource = (DataTable)Session["dt1"];
                gdstackingdetails.DataBind();
                emptbl.Visible = true;

                txtemppost.Text = "";
                txtempname.Text = "";
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
            whrtbl.Visible = true;
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
            if (txtwhrnum.Text == "")
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
                string query = "SELECT  [Depositor_WHR_Id],[Depotid],[Commodity_Id],(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=[tbl_storage_Depositor_WHR_Relation].Commodity_Id) as comm,[Depositor_Name],[TotalBags_Received],[Total_Qty_Received] ,[MktValue_of_Commodity],convert(varchar(20),[WHR_Issue_Date],103) as whrdate FROM [Intergrated_MP_STORAGE].[dbo].[tbl_storage_Depositor_WHR_Relation] where Whr_No='" + txtwhrnum.Text + "' and BranchID='" + Session["hdnbranchid"].ToString() + "'";
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
                    dr["depos"] = txtrahanbank.Text;
                    dr["dateofliyan"] = txtrahandate.Text;
                    ((DataTable)Session["dt2"]).Rows.Add(dr);
                    ((DataTable)Session["dt2"]).AcceptChanges();
                    gdwrdtl.DataSource = (DataTable)Session["dt2"];
                    gdwrdtl.DataBind();
                    whrtbl.Visible = true;
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
    //    string query = "SELECT  [Godown_ID],[Godown_Name] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where BranchID='" + Session["hdnbranchid"].ToString() + "' and Remarks='Y'";
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
        //string query = "SELECT sum([Godown_Scientific_Capacity])/10 as owncapacity FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type='Owned' and BranchID='" + Session["hdnbranchid"].ToString() + "' and Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and Remarks='Y'";
        SqlCommand cmd = new SqlCommand("Get_OwnedCapacity", Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
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
        //string query = "SELECT  count(isnull([Godown_ID],0)) as owngodown FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type='Owned' and BranchID='" + Session["hdnbranchid"].ToString() + "'";
        SqlCommand cmd = new SqlCommand("Get_owngwnnum", Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            // adding rows to the datatable
            Text3.Text = ds.Tables[0].Rows[0]["owngodown"].ToString();
        }

    }

    protected void capcapacity()
    {
        //string query = "SELECT isnull(sum([Godown_Scientific_Capacity])/10,0) as capcapacity FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type='Owned' and BranchID='" + Session["hdnbranchid"].ToString() + "' and Storage_Type  in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and Remarks='Y'";
        SqlCommand cmd = new SqlCommand("Get_capcapacity", Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
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
        string query = "SELECT [DepotName],[NodalOfficeName] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_DEPOT] where BranchId='" + Session["hdnbranchid"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {


            // adding rows to the datatable
            txt_BranchName.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
            txt_bm.Text = ds.Tables[0].Rows[0]["NodalOfficeName"].ToString();

        }

    }
    protected void owncapacityuses()
    {
        string query = "SELECT (sum([RecQty])-sum([DelQty]))/10 as ownuses FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] where BranchID='" + Session["hdnbranchid"].ToString() + "' and Godown_ID in ( select Godown_ID FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type='Owned' and BranchID='" + Session["hdnbranchid"].ToString() + "' and Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and Remarks='Y')";
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
        string query = "SELECT isnull((sum([RecQty])-sum([DelQty]))/10,0) as capuses FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] where BranchID='" + Session["hdnbranchid"].ToString() + "' and Godown_ID in ( select Godown_ID FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type='Owned' and BranchID='" + Session["hdnbranchid"].ToString() + "' and Storage_Type  in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and Remarks='Y')";
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
            string query = "SELECT  [EmpName],[EmpPost],[EMPPostingdate] FROM [Intergrated_MP_STORAGE].[dbo].[BranchAuditEmp] where AuditID in (select MAX(AuId) from dbo.BranchAudit where BranchId='" + Session["hdnbranchid"].ToString() + "')";
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
            if (txt_AuditerName.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('अंकेक्षणकर्ता अधिकारी का नाम लिखें')", true);
                txt_AuditerName.Focus();
                return;
            }
            else if (TextBox1.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('अंकेक्षण संपादन की दिनांक')", true);
                TextBox1.Focus();
                return;
            }
            else if (txt_bm.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('ब्रांच मेनेजर का नाम लिखें')", true);
                txt_bm.Focus();
                return;
            }
            else if (TextBox2.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('ब्रांच मेनेजर शाखा में कब से पदस्थ हैं ')", true);
                TextBox2.Focus();
                return;
            }

            else if (txtCBB.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('केश बुक बैलेन्स')", true);
                txtCBB.Focus();
                return;
            }
            else if (txtICB.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('इम्प्रेस्ट केश बुक')", true);
                txtICB.Focus();
                return;
            }
            else
            {
                string auid = "";
                string QueryMax = "select isnull(Max(InId),0)+1 from BranchAudit where  BranchId='" + Session["hdnbranchid"].ToString() + "' ";
                SqlCommand cmd2 = new SqlCommand(QueryMax, Con); // check WhrId present in whr_status table
                Con.Open();
                string str3 = cmd2.ExecuteScalar().ToString();
                if ((str3 == String.Empty) || str3 == "")
                {
                    str3 = "0";
                }
                if (Convert.ToInt64(str3) != 0)
                {
                    string Depotid = Session["hdnbranchid"].ToString();
                    auid = Depotid + System.DateTime.Now.Date.ToString("ddmmyy") + Convert.ToString(Convert.ToInt64(str3));
                }
                //string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string CS = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                using (SqlConnection constr = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("Account_Audit_Report_insert_by_Account_Officer", constr);
                    cmd.CommandType = CommandType.StoredProcedure;
                    constr.Open();
                    cmd.Parameters.AddWithValue("@AuditerName", txt_AuditerName.Text);
                    cmd.Parameters.AddWithValue("@AuditerPost", txt_AuditerPost.Text);
                    cmd.Parameters.AddWithValue("@BranchId", Session["hdnbranchid"].ToString());
                    //cmd.Parameters.AddWithValue("@BranchName", txt_BranchName);
                    cmd.Parameters.AddWithValue("@AuditDate", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@BranchManName", txt_bm.Text);
                    cmd.Parameters.AddWithValue("@BranchManPost", txt_bm_post.Text);
                    cmd.Parameters.AddWithValue("@BranchPostingDate", TextBox2.Text);
                    //Grid Fill Emp Detils
                    if (txt_ownCap.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@OwnCap", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@OwnCap", txt_ownCap.Text);
                    }
                    //cmd.Parameters.AddWithValue("@OwnCap", txt_ownCap.Text);
                    if (txt_ownuse.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@OwnUses", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@OwnUses", txt_ownuse.Text);
                    }
                    // cmd.Parameters.AddWithValue("@OwnUses", txt_ownuse.Text);
                    if (txt_capcap.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@CapCap", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CapCap", txt_capcap.Text);
                    }
                    //cmd.Parameters.AddWithValue("@CapCap", txt_capcap.Text);
                    if (txt_capuse.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@CapUses", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CapUses", txt_capuse.Text);
                    }
                    // cmd.Parameters.AddWithValue("@CapUses", txt_capuse.Text);
                    if (txt_hiredcap.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@HiredCap", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@HiredCap", txt_hiredcap.Text);
                    }
                    //cmd.Parameters.AddWithValue("@HiredCap", txt_hiredcap.Text);
                    if (txt_hiredUSe.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@HiredUSes", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@HiredUSes", txt_hiredUSe.Text);
                    }
                    //cmd.Parameters.AddWithValue("@HiredUSes", txt_hiredUSe.Text);
                    cmd.Parameters.AddWithValue("@ObtnBsns", txt_ObtnBsns.Text);
                    cmd.Parameters.AddWithValue("@ObtnableBsns", txt_ObtnableBsns.Text);
                    cmd.Parameters.AddWithValue("@AttemntfrmBMForBsns", txt_AttemntfrmBMForBsns.Text);
                    cmd.Parameters.AddWithValue("@AimofBranch", txtAimofBranch.Text);
                    cmd.Parameters.AddWithValue("@RetioofAimtilOditdate", txtRetioofAimtilOditdate.Text);
                    cmd.Parameters.AddWithValue("@Income", txtIncome.Text);
                    cmd.Parameters.AddWithValue("@lakchapraptiparsent", Text21.Text);
                    cmd.Parameters.AddWithValue("@lakhasekamjada", Text22.Text);
                    cmd.Parameters.AddWithValue("@rokigairashi", Text23.Text);
                    if (txtCBB.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@CashBookBalance", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CashBookBalance", txtCBB.Text);
                    }
                    if (txtICB.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@ImprestCashBook", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ImprestCashBook", txtICB.Text);
                    }
                    // cmd.Parameters.AddWithValue("@ImprestCashBook", txtICB.Text);
                    if (txtNICB.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@NirmanImprestCB", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@NirmanImprestCB", txtNICB.Text);
                    }
                    if (txtSRK.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@SRepairWork", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SRepairWork", txtSRK.Text);
                    }
                    //cmd.Parameters.AddWithValue("@SRepairWork", txtSRK.Text);
                    if (txtPS.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@PostageStamp", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PostageStamp", txtPS.Text);
                    }
                    // cmd.Parameters.AddWithValue("@PostageStamp", txtPS.Text);
                    if (txtRS.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@RevenueStamp", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RevenueStamp", txtRS.Text);
                    }
                    //cmd.Parameters.AddWithValue("@RevenueStamp", txtRS.Text);
                    cmd.Parameters.AddWithValue("@BankStatementD", txtBST.Text);
                    cmd.Parameters.AddWithValue("@SChargesAmt", txtDS.Text);
                    cmd.Parameters.AddWithValue("@SChargesMilan", txtBS1.Text);
                    cmd.Parameters.AddWithValue("@SChargesStatus", txtBS1.Text);
                    cmd.Parameters.AddWithValue("@PurveDeyak", txtPD.Text);
                    if (txtCYDP.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@ChaluVarshDeyak", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ChaluVarshDeyak", txtCYDP.Text);
                    }
                    //cmd.Parameters.AddWithValue("@ChaluVarshDeyak", txtCYDP.Text);
                    if (txtLB.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@PurveVarshRashi", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PurveVarshRashi", txtLB.Text);
                    }
                    // cmd.Parameters.AddWithValue("@PurveVarshRashi", txtLB.Text);
                    if (txtPVKD.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@PurveVarshDeyak", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PurveVarshDeyak", txtPVKD.Text);
                    }
                    // cmd.Parameters.AddWithValue("@PurveVarshDeyak", txtPVKD.Text);
                    //if (txtRLKR.Text == "")
                    //{
                    //    cmd.Parameters.AddWithValue("@RashiLambitKaran", 0);
                    //}
                    //else
                    //{
                    //    cmd.Parameters.AddWithValue("@RashiLambitKaran", txtRLKR.Text);
                    //}
                    cmd.Parameters.AddWithValue("@RashiLambitKaran", txtRLKR.Text);
                    cmd.Parameters.AddWithValue("@JVS1", txtjvs1.Value);
                    cmd.Parameters.AddWithValue("@JVS2", txtjvs2.Value);
                    cmd.Parameters.AddWithValue("@Financial_Year", ddlFinncialYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@WhrCharges", txtwc.Value);
                    cmd.Parameters.AddWithValue("@overandabovedtl", Text24.Text);
                    cmd.Parameters.AddWithValue("@commoditydtl", Textarea10.Value);
                    cmd.Parameters.AddWithValue("@stackingvegyanik", Textarea9.Value);
                    cmd.Parameters.AddWithValue("@skandhmekamiadhik", Textarea8.Value);
                    cmd.Parameters.AddWithValue("@spelage", Textarea7.Value);
                    cmd.Parameters.AddWithValue("@lambitbhugtan", txtLB.Text);
                    cmd.Parameters.AddWithValue("@lijarent", Textarea5.Value);
                    cmd.Parameters.AddWithValue("@viybildtl", Textarea4.Value);
                    cmd.Parameters.AddWithValue("@stafperfom", Text33.Value);
                    cmd.Parameters.AddWithValue("@RMTeep", TextaT.Value);
                    cmd.Parameters.AddWithValue("@BankRin", Text35.Text);
                    cmd.Parameters.AddWithValue("@Teep", Textarea1.Value);
                    cmd.Parameters.AddWithValue("@Trutipatrak", txtCaddress.Value);
                    cmd.Parameters.AddWithValue("@SandharitRcd", Textarea3.Value);
                    cmd.Parameters.AddWithValue("@InId", str3);
                    cmd.Parameters.AddWithValue("@AuId", auid);
                    cmd.Parameters.AddWithValue("@agrim", Textarea11.Value);
                    cmd.Parameters.AddWithValue("@lambitdabe", Textarea12.Value);
                    cmd.Parameters.AddWithValue("@vividhstock", Textarea13.Value);
                    cmd.Parameters.AddWithValue("@gunbatta", Textarea14.Value);
                    cmd.Parameters.AddWithValue("@nijigodownno", Text1.Text);
                    cmd.Parameters.AddWithValue("@nijigodamchanta", Text2.Text);
                    cmd.Parameters.AddWithValue("@surakchaupkaran", Textarea17.Value);
                    cmd.Parameters.AddWithValue("@owngwnno", Text3.Text);
                    cmd.Parameters.AddWithValue("@barsikrakh", Text4.Text);
                    cmd.Parameters.AddWithValue("@veshashrakh", Textarea20.Value);
                    cmd.Parameters.AddWithValue("@vaundriwall", Textarea21.Value);
                    cmd.Parameters.AddWithValue("@vyay", Text20.Text);
                    cmd.Parameters.AddWithValue("@TDS1", txttds1.Value);
                    cmd.Parameters.AddWithValue("@TDS2", txttds2.Value);
                    cmd.Parameters.AddWithValue("@MoneyTrans", txtdr.Value);
                    cmd.Parameters.AddWithValue("@LbrCharges", txtct.Value);
                    cmd.Parameters.AddWithValue("@CreatedBy", IPAddress);
                    cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnEmployeeID"].ToString());
                    cmd.Parameters.AddWithValue("@Inspection_Quarter", Session["hdninsptype"].ToString());
                    cmd.Parameters.AddWithValue("@Order_no", Session["lblOrder_No"].ToString());
                    cmd.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
                    cmd.Parameters.AddWithValue("@Financial_Year_Insp", Session["hdnfinancialYear"].ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 500);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Account Details has been Final submitted Successfully|||";

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        CheckFinalSUbmit();
                    }
                }
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
        //string query = "SELECT sum([Godown_Scientific_Capacity])/10 as owncapacity FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type='Owned' and BranchID='" + Session["hdnbranchid"].ToString() + "' and Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and Remarks='Y'";
        string query = "SELECT sum([Godown_Scientific_Capacity])/10 as Hiredcapacity FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type in('Joint Venture(JV)','JointVenture(JV)','Hired') and BranchID='" + Session["hdnbranchid"].ToString() + "' and Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and Remarks='Y'";
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
        string query = "SELECT (sum([RecQty])-sum([DelQty]))/10 as Hireduses FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] where BranchID='" + Session["hdnbranchid"].ToString() + "' and Godown_ID in (select Godown_ID FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Hired_Type in('Joint Venture(JV)','JointVenture(JV)','Hired') and BranchID='2301002' and Storage_Type not in ('Permanent(CAP)','Temporary(CAP)','Open(CAP)') and Remarks='Y')";
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
        ddlFinncialYear.Items.Insert(1, "2022-23");
        ddlFinncialYear.Items.Insert(1, "2021-22");
        ddlFinncialYear.Items.Insert(1, "2020-21");
        ddlFinncialYear.Items.Insert(1, "2019-20");
        ddlFinncialYear.Items.Insert(1, "2018-19");
        ddlFinncialYear.Items.Insert(1, "2016-17");
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
    protected void btnsaveprofile_Click(object sender, EventArgs e)
    {
        string auid = "";
        string QueryMax = "select isnull(Max(InId),0)+1 from BranchAudit where  BranchId='" + Session["hdnbranchid"].ToString() + "' ";
        SqlCommand cmd2 = new SqlCommand(QueryMax, Con); // check WhrId present in whr_status table
        Con.Open();
        string str3 = cmd2.ExecuteScalar().ToString();
        if ((str3 == String.Empty) || str3 == "")
        {
            str3 = "0";
        }
        if (Convert.ToInt64(str3) != 0)
        {
            string Depotid = Session["hdnbranchid"].ToString();
            auid = Depotid + System.DateTime.Now.Date.ToString("yy") + Convert.ToString(Convert.ToInt64(str3));
        }
        if (gdstackingdetails.Rows.Count > 0)
        {
            int k;
            for (k = 0; k < gdstackingdetails.Rows.Count; k++)
            {
                string CSS = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
                string IPAdd = Request.ServerVariables["REMOTE_ADDR"];
                using (SqlConnection constr2 = new SqlConnection(CSS))
                {
                    SqlCommand cmd3 = new SqlCommand("Branch_Employee_Details_insert", constr2);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    constr2.Open();
                    cmd3.Parameters.AddWithValue("@AuditID", auid);
                    cmd3.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                    cmd3.Parameters.AddWithValue("@EmpName", gdstackingdetails.Rows[k].Cells[1].Text.ToString());
                    cmd3.Parameters.AddWithValue("@EmpPost", gdstackingdetails.Rows[k].Cells[2].Text.ToString());
                    cmd3.Parameters.AddWithValue("@Employee_Type", "B");
                    cmd3.Parameters.AddWithValue("@EMPPostingdate", gdstackingdetails.Rows[k].Cells[3].Text.ToString());
                    cmd3.Parameters.AddWithValue("@Employee_ID", Session["hdnEmployeeID"].ToString());
                    cmd3.Parameters.AddWithValue("@Inspection_Quarter", Session["hdninsptype"].ToString());
                    cmd3.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
                    cmd3.Parameters.AddWithValue("@Financial_Year_Insp", Session["hdnfinancialYear"].ToString());
                    //cmd3.Parameters.Add("@TheResult", SqlDbType.VarChar, 500);
                    //cmd3.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd3.ExecuteNonQuery();
                    //string TheResult2 = cmd.Parameters["@TheResult"].Value.ToString();

                    //if (TheResult2.StartsWith("SUCCESS"))
                    //{
                    string strMsg = "Employees Details has been submitted Successfully|||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    // }
                    getempdata();
                    //gdstackingdetails.DataSource = null;
                    //gdstackingdetails.DataBind();
                    //btnsaveprofile.Visible = false;

                }
            }

        }
    }
    public void getImprestDetail()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();

        }
        SqlCommand cmd = new SqlCommand("Get_Imprest_Recupment_Audit_Details", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AuditId", 0);
        cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
        cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnEmployeeID"].ToString());
        cmd.Parameters.AddWithValue("@Inspection_Quarter", Session["hdninsptype"].ToString());
        cmd.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
        cmd.Parameters.AddWithValue("@Financial_Year_Insp", Session["hdnfinancialYear"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        if (ds.Tables[0].Rows.Count > 0)
        {
            grdImprest2.DataSource = ds.Tables[0];
            grdImprest2.DataBind();

        }
        else
        {

        }
    }
    protected void grdImprest2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = grdImprest2.Rows[rowIndex];

            //Fetch value of Name.
            string hdnId = (row.FindControl("hdnId") as HiddenField).Value;
            Session["hdnId"] = hdnId.ToString();
            RemoveRowFromImpress(hdnId);
            // RemoveRowJVS(hdnId);

        }
    }
    public void RemoveRowFromImpress(string id)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (con_WLC.State == ConnectionState.Closed)
        {
            con_WLC.Open();
        }
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (con_WLC.State == ConnectionState.Closed)
            {
                con_WLC.Open();
            }

            SqlCommand cmd = new SqlCommand("Delete_Imprest_Recupment_Audit", con_WLC
                );
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnEmployeeID"].ToString());
            cmd.Parameters.AddWithValue("@Inspection_Quarter", Session["hdninsptype"].ToString());
            cmd.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
            cmd.Parameters.AddWithValue("@Financial_Year_Insp", Session["hdnfinancialYear"].ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                getImprestDetail();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }
    protected void gvImprest2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total1 += (DataBinder.Eval(e.Row.DataItem, "OpeningBalance") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "OpeningBalance")) : 0;

            total2 += (DataBinder.Eval(e.Row.DataItem, "RecupmentAmount") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RecupmentAmount")) : 0;

            total3 += (DataBinder.Eval(e.Row.DataItem, "DepositByBM") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DepositByBM")) : 0;

            total4 += (DataBinder.Eval(e.Row.DataItem, "TFCashBook") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TFCashBook")) : 0;

            total5 += (DataBinder.Eval(e.Row.DataItem, "TFConstIimprest") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TFConstIimprest")) : 0;

            total6 += (DataBinder.Eval(e.Row.DataItem, "Total") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total")) : 0;

            total7 += (DataBinder.Eval(e.Row.DataItem, "ImprestForPass") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ImprestForPass")) : 0;

            total8 += (DataBinder.Eval(e.Row.DataItem, "ImprestPass") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ImprestPass")) : 0;

            total9 += (DataBinder.Eval(e.Row.DataItem, "WitheldAmount") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "WitheldAmount")) : 0;

            total10 += (DataBinder.Eval(e.Row.DataItem, "DisalloudAmount") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DisalloudAmount")) : 0;

            total11 += (DataBinder.Eval(e.Row.DataItem, "ReturnToBM") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ReturnToBM")) : 0;

            total12 += (DataBinder.Eval(e.Row.DataItem, "ReturnToCashBook") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ReturnToCashBook")) : 0;

            total13 += (DataBinder.Eval(e.Row.DataItem, "ReturnToConstImp") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ReturnToConstImp")) : 0;

            total14 += (DataBinder.Eval(e.Row.DataItem, "ClosingBalance") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ClosingBalance")) : 0;


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lblOpeningBalance = (Label)e.Row.FindControl("OpeningBalance");

            lblOpeningBalance.Text = total1.ToString();


            Label lblRecupmentAmount = (Label)e.Row.FindControl("RecupmentAmount");

            lblRecupmentAmount.Text = total2.ToString();

            Label lblDepositByBM = (Label)e.Row.FindControl("DepositByBM");

            lblDepositByBM.Text = total3.ToString();

            Label lblTFCashBook = (Label)e.Row.FindControl("TFCashBook");

            lblTFCashBook.Text = total4.ToString();

            Label lblTFConstIimprest = (Label)e.Row.FindControl("TFConstIimprest");

            lblTFConstIimprest.Text = total5.ToString();

            Label lblTotal = (Label)e.Row.FindControl("Total");

            lblTotal.Text = total6.ToString();

            Label lblImprestForPass = (Label)e.Row.FindControl("ImprestForPass");

            lblImprestForPass.Text = total7.ToString();

            Label lblImprestPass = (Label)e.Row.FindControl("ImprestPass");

            lblImprestPass.Text = total8.ToString();

            Label lblWitheldAmount = (Label)e.Row.FindControl("WitheldAmount");

            lblWitheldAmount.Text = total9.ToString();

            Label lblDisalloudAmount = (Label)e.Row.FindControl("DisalloudAmount");

            lblDisalloudAmount.Text = total10.ToString();

            Label lblReturnToBM = (Label)e.Row.FindControl("ReturnToBM");

            lblReturnToBM.Text = total11.ToString();

            Label lblReturnToCashBook = (Label)e.Row.FindControl("ReturnToCashBook");

            lblReturnToCashBook.Text = total12.ToString();

            Label lblReturnToConstImp = (Label)e.Row.FindControl("ReturnToConstImp");

            lblReturnToConstImp.Text = total13.ToString();

            Label lblClosingBalance = (Label)e.Row.FindControl("ClosingBalance");

            lblClosingBalance.Text = total14.ToString();

        }
    }
    protected void btnempressbook_Click(object sender, EventArgs e)
    {
        string auid = "";
        string QueryMax = "select isnull(Max(InId),0)+1 from BranchAudit where  BranchId='" + Session["hdnbranchid"].ToString() + "' ";
        SqlCommand cmd2 = new SqlCommand(QueryMax, Con); // check WhrId present in whr_status table
        Con.Open();
        string str3 = cmd2.ExecuteScalar().ToString();
        if ((str3 == String.Empty) || str3 == "")
        {
            str3 = "0";
        }
        if (Convert.ToInt64(str3) != 0)
        {
            string Depotid = Session["hdnbranchid"].ToString();
            auid = Depotid + System.DateTime.Now.Date.ToString("yy") + Convert.ToString(Convert.ToInt64(str3));
        }
        if (ddlFinncialYear.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('इम्प्रेस्ट व्ययो का वित्तीय वर्ष चुने')", true);
            ddlFinncialYear.Focus();
            return;
        }
        else
        {
            foreach (GridViewRow row in gvImprest.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList DropDoddwnList1 = row.FindControl("DropDownList1") as DropDownList;
                    TextBox txtOB = row.FindControl("txtOB") as TextBox;
                    TextBox txtRptAmt = row.FindControl("txtRptAmt") as TextBox;
                    TextBox txtDBRM = row.FindControl("txtDBRM") as TextBox;
                    TextBox txtTFCB = row.FindControl("txtTFCB") as TextBox;
                    TextBox txtTFCImp = row.FindControl("txtTFCImp") as TextBox;
                    TextBox txtTotal = row.FindControl("txtTotal") as TextBox;
                    TextBox txtImpFPass = row.FindControl("txtImpFPass") as TextBox;
                    //Label lblPrice = row.FindControl("lblPrice") as Label;
                    TextBox txtImpPass = row.FindControl("txtImpPass") as TextBox;
                    TextBox txtWithAmt = row.FindControl("txtWithAmt") as TextBox;
                    TextBox GtxtRemark = row.FindControl("GtxtRemark") as TextBox;
                    TextBox txtDisAmt = row.FindControl("txtDisAmt") as TextBox;
                    TextBox txtRetTBM = row.FindControl("txtRetTBM") as TextBox;
                    TextBox txtRetTCB = row.FindControl("txtRetTCB") as TextBox;
                    TextBox txtRetTCImp = row.FindControl("txtRetTCImp") as TextBox;
                    TextBox txtCB = row.FindControl("txtCB") as TextBox;

                    string ipAddress;
                    ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ipAddress == "" || ipAddress == null)
                        ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                    SqlCommand cmd4 = new SqlCommand("Imprest_Recupment_Audit_Details_insert", con);
                    cmd4.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    //cmd.Parameters.AddWithValue("@ID", hdnid.Value);
                    cmd4.Parameters.AddWithValue("@AuditId", auid);
                    cmd4.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                    cmd4.Parameters.AddWithValue("@FinacialYear", ddlFinncialYear.SelectedValue);
                    cmd4.Parameters.AddWithValue("@Month", DropDoddwnList1.SelectedValue);
                    cmd4.Parameters.AddWithValue("@OpeningBalance", txtOB.Text);
                    cmd4.Parameters.AddWithValue("@RecupmentAmount", txtRptAmt.Text);
                    cmd4.Parameters.AddWithValue("@DepositByBM", txtDBRM.Text);
                    cmd4.Parameters.AddWithValue("@TFCashBook", txtTFCB.Text);
                    cmd4.Parameters.AddWithValue("@TFConstIimprest", txtTFCImp.Text);
                    cmd4.Parameters.AddWithValue("@Total", txtTotal.Text);
                    cmd4.Parameters.AddWithValue("@ImprestForPass", txtImpFPass.Text);
                    cmd4.Parameters.AddWithValue("@ImprestPass", txtImpPass.Text);
                    cmd4.Parameters.AddWithValue("@WitheldAmount", txtWithAmt.Text);
                    cmd4.Parameters.AddWithValue("@DisalloudAmount", txtDisAmt.Text);
                    cmd4.Parameters.AddWithValue("@ReturnToBM", txtRetTBM.Text);
                    cmd4.Parameters.AddWithValue("@ReturnToCashBook", txtRetTCB.Text);
                    cmd4.Parameters.AddWithValue("@ReturnToConstImp", txtRetTCImp.Text);
                    cmd4.Parameters.AddWithValue("@ClosingBalance", txtCB.Text);

                    cmd4.Parameters.AddWithValue("@Employee_ID", Session["hdnEmployeeID"].ToString());
                    cmd4.Parameters.AddWithValue("@Inspection_Quarter", Session["hdninsptype"].ToString());
                    cmd4.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
                    cmd4.Parameters.AddWithValue("@Financial_Year_Insp", Session["hdnfinancialYear"].ToString());
                    cmd4.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd4.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd4.ExecuteNonQuery();
                    string TheResult4 = cmd4.Parameters["@TheResult"].Value.ToString();

                    if (TheResult4.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Imprest Recupment Audit Details has been submitted Successfully|||";

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        getImprestDetail();
                        //gvImprest.DataSource = null;
                        //gvImprest.DataBind();
                        //btnempressbook.Visible = false;
                    }
                    con.Close();
                }
            }
        }
    }

    public void getempdata()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();

        }
        SqlCommand cmd = new SqlCommand("Get_Branch_Audit_Employee_Details", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AuditId", 0);
        cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
        cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnEmployeeID"].ToString());
        cmd.Parameters.AddWithValue("@Inspection_Quarter", Session["hdninsptype"].ToString());
        cmd.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
        cmd.Parameters.AddWithValue("@Financial_Year_Insp", Session["hdnfinancialYear"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvempdtl.DataSource = ds.Tables[0];
            gvempdtl.DataBind();
            //emptbl.Visible = false;

        }
        else
        {

        }
    }
    protected void gvempdtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = gvempdtl.Rows[rowIndex];

            //Fetch value of Name.
            string hdnId = (row.FindControl("hdnId") as HiddenField).Value;
            Session["hdnId"] = hdnId.ToString();
            RemoveRow(hdnId);
            // RemoveRowJVS(hdnId);

        }
    }
    public void RemoveRow(string id)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (con_WLC.State == ConnectionState.Closed)
        {
            con_WLC.Open();
        }
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (con_WLC.State == ConnectionState.Closed)
            {
                con_WLC.Open();
            }

            SqlCommand cmd = new SqlCommand("Delete_Branch_Audit_Employee", con_WLC
                );
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnEmployeeID"].ToString());
            cmd.Parameters.AddWithValue("@Inspection_Quarter", Session["hdninsptype"].ToString());
            cmd.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
            cmd.Parameters.AddWithValue("@Financial_Year_Insp", Session["hdnfinancialYear"].ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                getempdata();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }
    protected void btnwhrsave_Click(object sender, EventArgs e)
    {
        string auid = "";
        string QueryMax = "select isnull(Max(InId),0)+1 from BranchAudit where  BranchId='" + Session["hdnbranchid"].ToString() + "' ";
        SqlCommand cmd2 = new SqlCommand(QueryMax, Con); // check WhrId present in whr_status table
        Con.Open();
        string str3 = cmd2.ExecuteScalar().ToString();
        if ((str3 == String.Empty) || str3 == "")
        {
            str3 = "0";
        }
        if (Convert.ToInt64(str3) != 0)
        {
            string Depotid = Session["hdnbranchid"].ToString();
            auid = Depotid + System.DateTime.Now.Date.ToString("yy") + Convert.ToString(Convert.ToInt64(str3));
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

                string CSSS = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
                using (SqlConnection constr3 = new SqlConnection(CSSS))
                {
                    SqlCommand cmd4 = new SqlCommand("Branch_Audit_WHR_Details_insert", constr3);
                    cmd4.CommandType = CommandType.StoredProcedure;
                    constr3.Open();
                    cmd4.Parameters.AddWithValue("@AuditID", auid);
                    cmd4.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                    cmd4.Parameters.AddWithValue("@WHRID", gdwrdtl.Rows[j].Cells[1].Text.ToString());
                    cmd4.Parameters.AddWithValue("@LiyanPatraDate", newdatefrom);
                    cmd4.Parameters.AddWithValue("@BankName", gdwrdtl.Rows[j].Cells[7].Text.ToString());
                    cmd4.Parameters.AddWithValue("@Employee_ID", Session["hdnEmployeeID"].ToString());
                    cmd4.Parameters.AddWithValue("@Inspection_Quarter", Session["hdninsptype"].ToString());
                    cmd4.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
                    cmd4.Parameters.AddWithValue("@Financial_Year_Insp", Session["hdnfinancialYear"].ToString());
                    //cmd4.Parameters.Add("@TheResult", SqlDbType.VarChar, 500);
                    //cmd4.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd4.ExecuteNonQuery();
                    //string TheResult3 = cmd.Parameters["@TheResult"].Value.ToString();

                    //if (TheResult3.StartsWith("SUCCESS"))
                    //{
                    string strMsg = "WHR Details has been submitted Successfully|||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    FillWHRdata();
                    gdwrdtl.DataSource = null;
                    gdwrdtl.DataBind();
                    //btnwhrsave.Visible = false;
                    //}

                }
            }
        }
    }
    public void FillWHRdata()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();

        }
        SqlCommand cmd = new SqlCommand("Get_Branch_Audit_WHR_Details", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
        cmd.Parameters.AddWithValue("@AuditId", 0);
        cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnEmployeeID"].ToString());
        cmd.Parameters.AddWithValue("@Inspection_Quarter", Session["hdninsptype"].ToString());
        cmd.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
        cmd.Parameters.AddWithValue("@Financial_Year_Insp", Session["hdnfinancialYear"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvwhrdtl.DataSource = ds.Tables[0];
            gvwhrdtl.DataBind();

        }
        else
        {

        }
    }

    protected void gvwhrdtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = gvwhrdtl.Rows[rowIndex];

            //Fetch value of Name.
            string hdnId = (row.FindControl("hdnId") as HiddenField).Value;
            Session["hdnId"] = hdnId.ToString();
            RemoveRowWHR(hdnId);
            // RemoveRowJVS(hdnId);

        }
    }
    public void RemoveRowWHR(string id)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (con_WLC.State == ConnectionState.Closed)
        {
            con_WLC.Open();
        }
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (con_WLC.State == ConnectionState.Closed)
            {
                con_WLC.Open();
            }

            SqlCommand cmd = new SqlCommand("Delete_Branch_Audit_WHR_Details", con_WLC
                );
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnEmployeeID"].ToString());
            cmd.Parameters.AddWithValue("@Inspection_Quarter", Session["hdninsptype"].ToString());
            cmd.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
            cmd.Parameters.AddWithValue("@Financial_Year_Insp", Session["hdnfinancialYear"].ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                FillWHRdata();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }
    public void CheckFinalSUbmit()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();

        }
        SqlCommand cmd = new SqlCommand("Get_Final_Submit_Details", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
        cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnEmployeeID"].ToString());
        cmd.Parameters.AddWithValue("@Inspection_Quarter", Session["hdninsptype"].ToString());
        cmd.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
        cmd.Parameters.AddWithValue("@Financial_Year_Insp", Session["hdnfinancialYear"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["AuId"].ToString() == "1")
            {
                btnsaveprofile.Visible = false;
                btnempressbook.Visible = false;
                btnwhrsave.Visible = false;
                btnsubmit.Visible = false;
            }
            else if (ds.Tables[0].Rows[0]["AuId"].ToString() == "0")
            {
                btnsaveprofile.Visible = true;
                btnempressbook.Visible = true;
                btnwhrsave.Visible = true;
                btnsubmit.Visible = true;
            }
        }
        //else
        //{
        //    btnsaveprofile.Visible = true;
        //    btnempressbook.Visible = true;
        //    btnwhrsave.Visible = true;
        //    btnsubmit.Visible = true;
        //}
    }
}