using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_TQHO_Branch_Audit_Inspection_Print : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string qry = "";
    string branchname = "";
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
        if (!IsPostBack)
        {
            fillScheduleInsp_Grid();
            Filldata();
            FillWHRdata();
            getempdata();
            getImprestDetail();
            //getGodowndata(); 
            Branchmgname();
            CheckFinalSubmit();
        }
    }
    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("View_Inspection_Officer_Details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"]);
                cmd.Parameters.AddWithValue("@PF_ID", Session["hdnEmployeeID"]);
                cmd.Parameters.AddWithValue("@QuaterType", Session["hdninsptype"]);
                cmd.Parameters.AddWithValue("@VerificationType", Session["hdnVerificationType"]);
                cmd.Parameters.AddWithValue("@FinacialYear", Session["hdnfinancialYear"]);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            lblname.Text = dt.Rows[0]["Officer_Name"].ToString();
                            lblInsBranch.Text = dt.Rows[0]["Depotname"].ToString();
                            lblInstype.Text = dt.Rows[0]["VerificationType"].ToString();
                            lblorderdate.Text = dt.Rows[0]["Order_Date"].ToString();
                            lblorderno.Text = dt.Rows[0]["Order_No"].ToString();
                        }
                       
                    }
                }
            }
        }
    }
    public void Filldata()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();

        }
        SqlCommand cmd = new SqlCommand("Get_Account_Audit_For_Branch_Insp", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AuditId", Session["hdnauid"].ToString());
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
            branchname = ds.Tables[0].Rows[0]["BranchName"].ToString();
            //  Session["BranchType"] = ds.Tables[0].Rows[0]["BranchTypeID"].ToString();
            lblauditornamepost.Text = ds.Tables[0].Rows[0]["AuditerName"].ToString() + "/" + ds.Tables[0].Rows[0]["AuditerPost"].ToString();
            lblbranchauditdate.Text = ds.Tables[0].Rows[0]["BranchName"].ToString() + "/" + ds.Tables[0].Rows[0]["AuditDate"].ToString();
            lblbmpost.Text = ds.Tables[0].Rows[0]["BranchManName"].ToString() + "/" + ds.Tables[0].Rows[0]["BranchManPost"].ToString();
            lblpostingdate.Text = ds.Tables[0].Rows[0]["BranchPostingDate"].ToString();
            lblowncap.Text = ds.Tables[0].Rows[0]["OwnCap"].ToString();
            lblownuse.Text = ds.Tables[0].Rows[0]["OwnUses"].ToString();
            try
            {
                decimal ownper = (Convert.ToDecimal(ds.Tables[0].Rows[0]["OwnUses"].ToString()) * 100) / Convert.ToDecimal(ds.Tables[0].Rows[0]["OwnCap"].ToString());
                lblownper.Text = Math.Round(ownper, 2, MidpointRounding.AwayFromZero).ToString();
            }
            catch (Exception ex)
            {

                lblownper.Text = "0";
            }
            lblcapcap.Text = ds.Tables[0].Rows[0]["CapCap"].ToString();
            lblcapuse.Text = ds.Tables[0].Rows[0]["CapUses"].ToString();
            try
            {
                decimal capper = (Convert.ToDecimal(ds.Tables[0].Rows[0]["CapUses"].ToString()) * 100) / Convert.ToDecimal(ds.Tables[0].Rows[0]["CapCap"].ToString());
                lblcapper.Text = Math.Round(capper, 2, MidpointRounding.AwayFromZero).ToString();
            }
            catch (Exception ex)
            {
                lblcapper.Text = "0";
            }
            lblhiredcap.Text = ds.Tables[0].Rows[0]["HiredCap"].ToString();
            lblhireduse.Text = ds.Tables[0].Rows[0]["HiredUSes"].ToString();
            try
            {
                decimal hiredper = (Convert.ToDecimal(ds.Tables[0].Rows[0]["HiredUSes"].ToString()) * 100) / Convert.ToDecimal(ds.Tables[0].Rows[0]["HiredCap"].ToString());
                lblhiredper.Text = Math.Round(hiredper, 2, MidpointRounding.AwayFromZero).ToString();
            }
            catch (Exception ex)
            {
                lblhiredper.Text = "0";
            }
            try
            {
                decimal totalcap = Convert.ToDecimal(ds.Tables[0].Rows[0]["OwnCap"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[0]["CapCap"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[0]["HiredCap"].ToString());
                lbltotalcap.Text = totalcap.ToString();
            }
            catch
            {
                lbltotalcap.Text = "0";
            }
            try
            {
                decimal totaluses = Convert.ToDecimal(ds.Tables[0].Rows[0]["OwnUses"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[0]["CapUses"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[0]["HiredUSes"].ToString());
                lblbtotaluses.Text = totaluses.ToString();
            }
            catch
            {
                lblbtotaluses.Text = "0";
            }
            //decimal totalper = (Convert.ToDecimal(lblbtotaluses.Text) * 100) / Convert.ToDecimal(lbltotalcap.Text);
            //lbltotalper.Text = Math.Round(totalper, 2, MidpointRounding.AwayFromZero).ToString();
            Label17.Text = ds.Tables[0].Rows[0]["ObtnBsns"].ToString();
            Label18.Text = ds.Tables[0].Rows[0]["ObtnableBsns"].ToString();
            Label19.Text = ds.Tables[0].Rows[0]["AttemntfrmBMForBsns"].ToString();
            Label20.Text = ds.Tables[0].Rows[0]["AimofBranch"].ToString();
            Label21.Text = ds.Tables[0].Rows[0]["RetioofAimtilOditdate"].ToString();
            Label22.Text = ds.Tables[0].Rows[0]["Income"].ToString();
            Label24.Text = ds.Tables[0].Rows[0]["lakchapraptiparsent"].ToString();
            Label25.Text = ds.Tables[0].Rows[0]["lakhasekamjada"].ToString();
            Label26.Text = ds.Tables[0].Rows[0]["rokigairashi"].ToString();
            Label27.Text = ds.Tables[0].Rows[0]["overandabovedtl"].ToString();
            Label28.Text = ds.Tables[0].Rows[0]["commoditydtl"].ToString();
            Label29.Text = "संलग्न है।";
            Label30.Text = "संलग्न है।";

            Label31.Text = ds.Tables[0].Rows[0]["stackingvegyanik"].ToString();
            Label32.Text = ds.Tables[0].Rows[0]["skandhmekamiadhik"].ToString();
            Label33.Text = ds.Tables[0].Rows[0]["spelage"].ToString();
            Label35.Text = ds.Tables[0].Rows[0]["lambitbhugtan"].ToString();
            Label36.Text = ds.Tables[0].Rows[0]["lijarent"].ToString();
            Label37.Text = ds.Tables[0].Rows[0]["viybildtl"].ToString();
            Label38.Text = ds.Tables[0].Rows[0]["SandharitRcd"].ToString();
            Label39.Text = ds.Tables[0].Rows[0]["stafperfom"].ToString();
            Label40.Text = ds.Tables[0].Rows[0]["RMTeep"].ToString();
            Label41.Text = ds.Tables[0].Rows[0]["BankRin"].ToString();
            Label42.Text = ds.Tables[0].Rows[0]["Teep"].ToString();
            Label43.Text = ds.Tables[0].Rows[0]["Trutipatrak"].ToString();
            lblbranch2.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
            lblbranch3.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
            lblbranch4.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
            lblbrnach5.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
            lblbranch6.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
            lblbranch3.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
            //lblbranch44.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
            lblauditdate2.Text = ds.Tables[0].Rows[0]["AuditDate"].ToString();
            lblauditdate3.Text = ds.Tables[0].Rows[0]["AuditDate"].ToString();
            lbldate7.Text = ds.Tables[0].Rows[0]["AuditDate"].ToString();
            lbldate8.Text = ds.Tables[0].Rows[0]["AuditDate"].ToString();
            lbldate9.Text = ds.Tables[0].Rows[0]["AuditDate"].ToString();
            lbldate10.Text = ds.Tables[0].Rows[0]["AuditDate"].ToString();
            //lbldategdn.Text = ds.Tables[0].Rows[0]["AuditDate"].ToString();
            lblagrim.Text = ds.Tables[0].Rows[0]["agrim"].ToString();
            lbllambitdabe.Text = ds.Tables[0].Rows[0]["lambitdabe"].ToString();
            Label23.Text = ds.Tables[0].Rows[0]["vyay"].ToString();
            lblvividhstock.Text = ds.Tables[0].Rows[0]["vividhstock"].ToString();
            lblgunbatta.Text = ds.Tables[0].Rows[0]["gunbatta"].ToString();
            lblnijigodamno.Text = ds.Tables[0].Rows[0]["nijigodownno"].ToString();
            lblnijigodamcap.Text = ds.Tables[0].Rows[0]["nijigodamchanta"].ToString();
            lblsurakchaupkaran.Text = ds.Tables[0].Rows[0]["surakchaupkaran"].ToString();
            lblowngdn.Text = ds.Tables[0].Rows[0]["owngwnno"].ToString();
            lblvarsikrakh.Text = ds.Tables[0].Rows[0]["barsikrakh"].ToString();
            lblvisheshrakh.Text = ds.Tables[0].Rows[0]["veshashrakh"].ToString();
            lblvaundriwall.Text = ds.Tables[0].Rows[0]["vaundriwall"].ToString();
            ////////////new//////////////////////
            tds1.Text = ds.Tables[0].Rows[0]["TDS1"].ToString();
            tds2.Text = ds.Tables[0].Rows[0]["TDS2"].ToString();
            dr1.Text = ds.Tables[0].Rows[0]["MoneyTrans"].ToString();
            WC.Text = ds.Tables[0].Rows[0]["WhrCharges"].ToString();
            lc.Text = ds.Tables[0].Rows[0]["LbrCharges"].ToString();
            jvs1.Text = ds.Tables[0].Rows[0]["JVS1"].ToString();
            jvs2.Text = ds.Tables[0].Rows[0]["JVS2"].ToString();
            lblFy.Text = ds.Tables[0].Rows[0]["Financial_Year"].ToString();
            ////////////////////////////////////////////////
            lblCBB.Text = ds.Tables[0].Rows[0]["CashBookBalance"].ToString();
            lblICB.Text = ds.Tables[0].Rows[0]["ImprestCashBook"].ToString();
            lblNICB.Text = ds.Tables[0].Rows[0]["NirmanImprestCB"].ToString();
            lblSRK.Text = ds.Tables[0].Rows[0]["SRepairWork"].ToString();
            lblPS.Text = ds.Tables[0].Rows[0]["PostageStamp"].ToString();
            lblRS.Text = ds.Tables[0].Rows[0]["RevenueStamp"].ToString();

            lblBST.Text = ds.Tables[0].Rows[0]["BankStatementD"].ToString();

            lblDS.Text = ds.Tables[0].Rows[0]["SChargesAmt"].ToString();
            lblBSPD.Text = ds.Tables[0].Rows[0]["SChargesMilan"].ToString();

            lblBS1.Text = ds.Tables[0].Rows[0]["SChargesStatus"].ToString();
            lblPD.Text = ds.Tables[0].Rows[0]["PurveDeyak"].ToString();

            lblCYDP.Text = ds.Tables[0].Rows[0]["ChaluVarshDeyak"].ToString();
            lblLB.Text = ds.Tables[0].Rows[0]["PurveVarshRashi"].ToString();
            lblPVKD.Text = ds.Tables[0].Rows[0]["PurveVarshDeyak"].ToString();
            lblRLKR.Text = ds.Tables[0].Rows[0]["RashiLambitKaran"].ToString();


        }
        else
        {

        }
    }

    public void FillWHRdata()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();

        }
        SqlCommand cmd = new SqlCommand("Get_Branch_Audit_WHR_Details_For_Print", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
        cmd.Parameters.AddWithValue("@AuditId", Session["hdnauid"].ToString());
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

    public void getImprestDetail()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();

        }
        SqlCommand cmd = new SqlCommand("Get_Imprest_Recupment_Audit_Details_For_Print", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AuditId", Session["hdnauid"].ToString());
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
            gvImprest.DataSource = ds.Tables[0];
            gvImprest.DataBind();

        }
        else
        {

        }
    }

    public void getempdata()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();

        }
        SqlCommand cmd = new SqlCommand("Get_Branch_Audit_Employee_Details_For_Print", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AuditId", Session["hdnauid"].ToString());
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

        }
        else
        {

        }
    }

    protected void Branchmgname()
    {
        string query = "SELECT [DepotName],[NodalOfficeName] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_DEPOT] where BranchId='" + Session["UserID"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {


            // adding rows to the datatable
            lblBnch.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
            //txt_bm.Value = ds.Tables[0].Rows[0]["NodalOfficeName"].ToString();

        }

    }
    protected void gvImprest_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total1 += (DataBinder.Eval(e.Row.DataItem, "OpeningBalance") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OpeningBalance")) : 0;

            total2 += (DataBinder.Eval(e.Row.DataItem, "RecupmentAmount") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RecupmentAmount")) : 0;

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
    protected void CheckFinalSubmit()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Check_Account_Information_Final_Submit_to_TQRO", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@InspectionTypeID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@QuaterID", Session["hdninsptype"].ToString());
                cmd.Parameters.AddWithValue("@FinancialYear", Session["hdnfinancialyear"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            txtremrktqro.Text = dt.Rows[0]["Remrak_by_TQRO"].ToString();
                            if (dt.Rows[0]["Submit_TQHO_to_HOMPWLC"].ToString() == "0")
                            {
                                divbtn.Visible = true;
                            }
                            else if (dt.Rows[0]["Submit_TQHO_to_HOMPWLC"].ToString() == "Y")
                            {
                                divbtn.Visible = false;
                                
                                txtremrakTQHO.Text = dt.Rows[0]["Remrak_by_TQHO"].ToString();
                                string strMsg = "यहाँ Truti patrak आपके द्वारा पहले ही HO MPWLC को Final Submit किया जा चुका है |||";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                            }
                            else if (dt.Rows[0]["Submit_TQHO_to_HOMPWLC"].ToString() == "N")
                            {
                                divbtn.Visible = true;
                            }

                        }
                    }
                }
            }
        }
    }
    protected void btnfinalsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string CS = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Account_Information_Forward_to_HO_MPWLC", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@InspectionTypeID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@QuaterID", Session["hdninsptype"].ToString());
                cmd.Parameters.AddWithValue("@FinancialYear", Session["hdnfinancialyear"].ToString());
                cmd.Parameters.AddWithValue("@Remrak_by_TQHO", txtremrakTQHO.Text.ToString());
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "You have successfully submitted to HO MPWLC |||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Inspections/Inspection_Officer/Godown_wise_Stock_Details.aspx';", true);
                    fillScheduleInsp_Grid();
                    Filldata();
                    FillWHRdata();
                    getempdata();
                    getImprestDetail();
                    //getGodowndata(); 
                    Branchmgname();
                    divbtn.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
}