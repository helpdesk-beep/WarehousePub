using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounting_frm_MPSCSC_SC_Final_Bill_Rejected_By_HOMPSCSC : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    DataSet ds2 = null;
    string DFReceive_ID = "";
    string Bill_No = "";
    int BID = 0;
    string Depositor_ID = "129";
    int Days_In_Month = 0;
    decimal Total_Charge = 0;
    decimal Rate_M = 0;
     DateTime StartDate = new DateTime();
    DateTime EndDate = new DateTime();
    string Crop_Year = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((Session["Depot_DistID"] != null) && (Session["BranchId"] != null))
        //{
            if (!IsPostBack)
            {
                //GetCropYear();
                GetDistrict();
            }

        //}
        //else
        //{
        //    Response.Redirect("~/SessionExpired.htm");
        //}
    }
    void GetDistrict()
    {
        //qry = "select distinct CropYear from View_WHRcurrentstock where Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
        qry = "Select District_Id,District_Name from tbl_MetaData_DISTRICT order by District_Name";

        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlDistrict.DataSource = ds.Tables[0];
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_Id";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, "--Select--");
        }
    }

    void GetBranch()
    {
        //qry = "select distinct CropYear from View_WHRcurrentstock where Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
        qry = "Select BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName";

        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "BranchId";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, "--Select--");
        }
    }

    void GetGodown()
    {
        //qry = "select distinct CropYear from View_WHRcurrentstock where Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
        qry = "Select Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 where BranchID='" + ddlbranch.SelectedValue.ToString() + "' order by Godown_Name";

        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");
        }
    }

    void GetBillNo()
    {
        //qry = "select distinct CropYear from View_WHRcurrentstock where Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
        qry = "select distinct Ref_Bill_No from [MPSCSC ].[dbo].[tbl_Processing_StorageBill_AtDM_BlackList_Bills] where Godown_Id='" + ddlgodown.SelectedValue+ "' order by Ref_Bill_No";

        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlbill.DataSource = ds.Tables[0];
            ddlbill.DataTextField = "Ref_Bill_No";
            ddlbill.DataValueField = "Ref_Bill_No";
            ddlbill.DataBind();
            ddlbill.Items.Insert(0, "--Select--");
        }
    }
    public void GetBillData()
    {
       
        GetBillsDetail();
        GetBillDetailsFromSummaryTable();
            //Panel1.Visible = false;
    }

    protected void GetBillsDetail()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_HOMPSCSC_Rejected_Bill_Details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FinalBillNo", ddlbill.SelectedValue);

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet dt = new DataSet())
                    {
                        int Storage_Value = 0;
                        sda.Fill(dt);
                        if (dt.Tables[0].Rows.Count > 0)
                        {

                            gvBOBillApp.DataSource = dt.Tables[0];
                            gvBOBillApp.DataBind();
                            //gvBOBillApp.Columns[1].Visible = false;
                            trnewproc.Visible = true;
                            tblbtn.Visible = true;
                            lblNoofAC.Text = dt.Tables[0].Rows.Count.ToString();
                            btn_save.Enabled = true;

                        }                        
                    }
                }
            }
        }
    }

    protected void GetBillDetailsFromSummaryTable()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Bill_Details_Form_Summary_Table", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BillNo", ddlbill.SelectedValue);

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet dt = new DataSet())
                    {
                        sda.Fill(dt);
                        if (dt.Tables[0].Rows.Count > 0)
                        {
                            lblBill_Number.Text = dt.Tables[0].Rows[0]["Bill_Number"].ToString();
                            lblBill_Count.Text = dt.Tables[0].Rows[0]["Bill_Count"].ToString();
                            lblNet_Amount.Text = dt.Tables[0].Rows[0]["Net_Amount"].ToString();
                            lblSub_Amount.Text = dt.Tables[0].Rows[0]["Sub_Amount"].ToString();
                            Session["lblBill_Count"]= dt.Tables[0].Rows[0]["Bill_Count"].ToString();
                            Session["lblNet_Amount"] = dt.Tables[0].Rows[0]["Net_Amount"].ToString();
                            Session["lblSub_Amount"] = dt.Tables[0].Rows[0]["Sub_Amount"].ToString();
                        }
                    }
                }
            }
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        //ViewState["TotalBags"] = lblTotalBags.Text;
        //string changedLabelValue = hdnLabelState.Value;
        try
        {
            int TotalR = 0;
            decimal TotalC = 0;
            decimal TGSTA = 0;
            decimal TotalSC = 0;
            decimal TotalGSTOnSC = 0;
            decimal TotalBillAmt = 0;
            foreach (GridViewRow row in gvBOBillApp.Rows)
            {
                CheckBox chkbox = (CheckBox)row.FindControl("chk_Sum");
                if (chkbox.Checked == true)
                {
                    TotalR = TotalR + 1;
                    TextBox TotalC1 = (TextBox)row.FindControl("txtcharges");
                    TotalC = TotalC + Convert.ToDecimal(TotalC1.Text);
                    TextBox TGSTA1 = (TextBox)row.FindControl("txtGSTAmt");
                    TGSTA = TGSTA + Convert.ToDecimal(TGSTA1.Text);
                    TextBox TotalSC1 = (TextBox)row.FindControl("txtSupcharges");
                    TotalSC = TotalSC + Convert.ToDecimal(TotalSC1.Text);
                    TextBox TotalGSTOnSC1 = (TextBox)row.FindControl("txtGSTPer");
                    TotalGSTOnSC = TotalGSTOnSC + Convert.ToDecimal(TotalGSTOnSC1.Text);
                    TextBox TotalBillAmt1 = (TextBox)row.FindControl("txtweight");
                    TotalBillAmt = TotalBillAmt + Convert.ToDecimal(TotalBillAmt1.Text);
                }
            }
            string Deposit_Date = gvBOBillApp.Rows[0].Cells[2].Text;
            //lblTotalRecord.Text = hdnLabelState.Value;
            GetReceivedSummary();
            
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
    }
    public void GetReceivedSummary()
    {

        //string TB = hdnLabelState.Value;
        //string TBD = Session["lblBill_Count"].ToString();
        int TB = Convert.ToInt32(Session["lblBill_Count"])- Convert.ToInt32(hdnLabelState.Value);
        Decimal TBA = Convert.ToDecimal(Session["lblNet_Amount"])- Convert.ToDecimal(hdnLabelStateQty.Value);
        Decimal TBAS = Convert.ToDecimal(Session["lblSub_Amount"]) - Convert.ToDecimal(HiddenField11.Value);
        // int TBAS = Convert.ToInt32(Session["lblNet_Amount"])- Convert.ToInt32(hdnLabelStateQty.Value);
        lblTotalRecord.Text = TB.ToString();
        lblBillAmount.Text = TBA.ToString();
        lblSTotalCharges.Text = TBAS.ToString();
        //lblTotalRecord.Text = hdnLabelState.Value;
        //lblBillAmount.Text = hdnLabelStateQty.Value;
        //lblSTotalCharges.Text = HiddenField11.Value;
        lblSGSTAmt.Text= HiddenField12.Value;
        lblSSupCharges.Text = HiddenField13.Value;
        lblSGSTonSupChar.Text= HiddenField14.Value;
        //lblRPM.Text = ViewState["Rate_PM"].ToString();
        //lblRPD.Text = ViewState["Rate_PD"].ToString();

        //lblGodown.Text = ddl_godown.SelectedItem.Text;
        //lblSendBags.Text = lblTotalBagSend.Text;
        //lblSendQty.Text = lblTotalQtySend.Text;
        //lblRcdBags.Text = hdnLabelState.Value;
        //lblRecdQty.Text = hdnLabelStateQty.Value;
        //lblDepositor.Text = ddlDepositor.SelectedItem.Text;
        //lblCropYear.Text = ddlcropyear.SelectedValue;
        //lblCommodity.Text = ddlProcCmd.SelectedItem.Text;
        //hdnGodownID.Text = ddl_godown.SelectedValue;
        //hdnDepositorID.Text = ddlDepositor.SelectedValue;
        //hdnCommodityID.Text = ddlProcCmd.SelectedValue;
        ModalPopupExtender2.Show();
        Button2.Visible = true;
        //
        //lblTotalBags.Text = hdnLabelState.Value;
        //lblTotalQty.Text = hdnLabelStateQty.Value;


    }
    public void Insert_Bill_Detail()
    {
        try
        {
            SqlTransaction sqltran;
            string Godown_Bill_No = "";
            string Godown_ID = "";
            string qry = "";
            //Bill_No = ViewState["BillNo"].ToString();
            string Is_Rebate = "N";
            string DeposCategory = "";
            decimal RebatePer = 0;
            decimal NoBags = 0;
            string Rin_Pustika_No = "";
            string Cast_Certificate_No = "";
            decimal ChargeOfTotalWeight = 0;
            decimal NetAmount = 0;
            string Bill_Type = hdnBillCategoryID.Text;

            //string FinYear = "2020-21";
           
            Is_Rebate = "N";
            RebatePer = 0;
            NoBags = 0;
            Rin_Pustika_No = "0";
            Cast_Certificate_No = "0";
            //ChargeOfTotalWeight=
            //decimal SerTax = (ChargeOfTotalWeight * Convert.ToDecimal(txtstax.Text)) / 100;
            decimal GST_Per = 0;
            decimal GST_Amount = Convert.ToDecimal(lblSGSTAmt.Text);
            if (GST_Amount != 0 && GST_Amount.ToString() != null)
            {
                GST_Per = 18;
            }
            else
            {
                GST_Per = 0;
            }
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string Dist_id = Session["Depot_DistID"].ToString();
            string BranchID = Session["BranchID"].ToString();
            //string VStartDate = ViewState["SD"].ToString();
            //string VEndDate = ViewState["ED"].ToString();
            //Get_Bill_Type();
            //BID = Convert.ToInt32(ViewState["BID"]);
            //For Silo Bags Only
            decimal MPWLC_SC = 0;
            //MPWLC_SC = Math.Round(((Math.Round(ChargeOfTotalWeight) * 10) / 100));
            MPWLC_SC = Math.Round(Convert.ToDecimal(lblSSupCharges.Text));


            //GST_Amt_SC = Math.Round((MPWLC_SC * GST_Per_SC) / 100);
            decimal GST_Per_SC = 0;
            decimal GST_Amt_SC = 0;
            GST_Amt_SC = Math.Round(Convert.ToDecimal(lblSGSTonSupChar.Text)); ;
            if (GST_Amt_SC == 0 || GST_Amt_SC.ToString() == null)
            {
                GST_Per_SC = 0;
            }
            else
            {
                GST_Per_SC = 18;
            }
            int ICount = 0;
            //
            //Rounding
            //decimal Rount_NetAmount = Math.Round(NetAmount);
            decimal Rount_NetAmount = Math.Round(Convert.ToDecimal(lblBillAmount.Text));
            NetAmount = Rount_NetAmount;
            decimal Rount_ChargeOfTotalWeight = Math.Round(Convert.ToDecimal(lblSTotalCharges.Text));
            ChargeOfTotalWeight = Rount_ChargeOfTotalWeight;
            con.Open();
            sqltran = con.BeginTransaction();
            //if (ddlGodownType.SelectedValue.ToString() != "4" && ddlGodownType.SelectedValue.ToString() != "6")
            //{
            //qry = "INSERT INTO tbl_Institution_Storage_Bill_Summary(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Crop_Year,Month,Year,MPWLC_SC,GST_Perc_SC,GST_Amt_SC) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','4','129','3','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(VStartDate) + "','" + getDate_MDY(VEndDate) + "','1','5','" + FinYear + "'," + Convert.ToDecimal(lblRPM.Text) + "," + NetAmount + "," + ChargeOfTotalWeight + ",18," + SerTax + ",'" + DeposCategory + "'," + RebatePer + ",0," + NoBags + ",'0','" + Is_Rebate + "',getdate(),'','" + ip + "','" + Convert.ToDecimal(lblRPD.Text) + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No + "','" + Bill_Type + "','" + BID + "','" + CropYear + "','" + ddlmonth.SelectedValue + "','"+ ddlFYearNew.SelectedItem.Text + "','" + Convert.ToDecimal(lblSSupCharges.Text) + "',18,'" + Convert.ToDecimal(lblSGSTonSupChar.Text) + "')";
            //qry = "INSERT INTO tbl_Institution_Storage_Bill_Summary(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Crop_Year,Month,Year,MPWLC_SC,GST_Perc_SC,GST_Amt_SC) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','4','129','3','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(VStartDate) + "','" + getDate_MDY(VEndDate) + "','1','5','" + FinYear + "'," + Convert.ToDecimal(lblRPM.Text) + "," + NetAmount + "," + ChargeOfTotalWeight + ",'"+ GST_Per + "'," + GST_Amount + ",'" + DeposCategory + "'," + RebatePer + ",0," + NoBags + ",'0','" + Is_Rebate + "',getdate(),'','" + ip + "','" + Convert.ToDecimal(lblRPD.Text) + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No + "','" + Bill_Type + "','" + BID + "','" + CropYear + "','" + ddlmonth.SelectedValue + "','" + ddlFYearNew.SelectedItem.Text + "','" + Convert.ToDecimal(lblSSupCharges.Text) + "','"+ GST_Per_SC + "','" + GST_Amt_SC + "')";
            //qry = "INSERT INTO tbl_Institution_Storage_Bill_Summary(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Crop_Year,Month,Year,MPWLC_SC,GST_Perc_SC,GST_Amt_SC,Bill_Count) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','4','129','3','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(VStartDate) + "','" + getDate_MDY(VEndDate) + "','1','5','" + FinYear + "'," + Convert.ToDecimal(lblRPM.Text) + "," + NetAmount + "," + ChargeOfTotalWeight + ",'" + GST_Per + "'," + GST_Amount + ",'" + DeposCategory + "'," + RebatePer + ",0," + NoBags + ",'0','" + Is_Rebate + "',getdate(),'','" + ip + "','" + Convert.ToDecimal(lblRPD.Text) + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No + "','" + Bill_Type + "','" + BID + "','" + CropYear + "','" + ddlmonth.SelectedValue + "','" + ddlFYearNew.SelectedItem.Text + "','" + Convert.ToDecimal(lblSSupCharges.Text) + "','" + GST_Per_SC + "','" + GST_Amt_SC + "','"+ lblTotalRecord.Text + "')";

            //qry = "update tbl_Institution_Storage_Bill_Summary set Net_Amount='" + NetAmount + "',Sub_Amount='"+ ChargeOfTotalWeight + "',Bill_Count='"+ lblTotalRecord.Text + "' where Bill_Number='" + ddlbill.SelectedValue + "' and Godown_Id='" + Godown_ID + "'";
            SqlCommand cmd = new SqlCommand("Update_Bill_details_Rejected_by_HOMPSCSC", con, sqltran);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BillNo", ddlbill.SelectedValue);
            cmd.Parameters.AddWithValue("@Net_Amount", NetAmount);
            cmd.Parameters.AddWithValue("@Sub_Amount", ChargeOfTotalWeight);
            cmd.Parameters.AddWithValue("@Bill_Count", lblTotalRecord.Text);
            //con.Open();
            int n=cmd.ExecuteNonQuery();
            //con.Close();
            if(n>0)
            {  
                foreach (GridViewRow row in gvBOBillApp.Rows)
                {
                    CheckBox chkbox = (CheckBox)row.FindControl("chk_Sum");
                    if (chkbox.Checked == true)
                    {
                        Godown_Bill_No = row.Cells[0].Text;
                        Godown_ID = row.Cells[1].Text;

                        //string qry2 = "update tbl_Institution_Storage_Bill_Details set Fin_Bill_No='"+ DBNull.Value + "',Delete_Flag='RN' where Bill_Number='" + Godown_Bill_No + "' and Godown_Id='" + Godown_ID + "'";
                        //SqlCommand cmd2 = new SqlCommand(qry2, con, sqltran);
                        SqlCommand cmd2 = new SqlCommand("Update_Final_Bill_details_Rejected_by_HOMPSCSC", con, sqltran);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@BillNo", Godown_Bill_No);
                        cmd2.Parameters.AddWithValue("@Fin_Bill_No", DBNull.Value);
                        cmd2.Parameters.AddWithValue("@GodownID", Godown_ID);
                        //con.Open();
                        int i = cmd2.ExecuteNonQuery();
                        //con.Close();
                        ICount = ICount + i;
                    }
                }
                if(!string.IsNullOrEmpty(lblTotalRecord.Text))
                {
                    sqltran.Commit();
                    con.Close();
                    GetBillsDetail();
                    //if (Convert.ToInt32(lblTotalRecord.Text) == ICount)
                    //{
                    //    sqltran.Commit();
                    //    con.Close();
                    //}
                    //else
                    //{
                    //    sqltran.Rollback();
                    //    con.Close();
                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in button save click Please Try Again')", true);
                    //}
                }
                else
                {
                    sqltran.Rollback();
                    con.Close();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in button save click Please Try Again')", true);
                }

                lblrmsg.Visible = true;
                //lblrmsg.Text = "Bill Number : " + ViewState["BillNo"].ToString() + " Generated Successfully...";
                lblrmsg.Text = "Bill Number : Updated Successfully...";
                btnNewBill.Visible = true;
                Button2.Visible = false;
                //Button1.Visible = true;
                //GetFinalBillDetail();
                ModalPopupExtender2.Show();
                //pnlCofirmmsg.Visible = true;

            }
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
   protected void Button2_Click(object sender, EventArgs e)
    {
        if(lblSTotalCharges.Text=="0")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please check Total Charges Amount'); </script> ");
        }
        else if (lblBillAmount.Text == "0")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please check Total Bill Amount'); </script> ");
        }
        else
        { 
        Insert_Bill_Detail();
        }
    }
  
    protected void btnNewBill_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounting/frm_MPSCSC_SC_Final_Bill.aspx");
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
       // Response.Redirect("~/Accounting/frm_MPSCSC_SC_Final_Bill.aspx");
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
        
    }

    protected void ddlbill_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillData();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodown();
    }

    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillNo();
    }
}