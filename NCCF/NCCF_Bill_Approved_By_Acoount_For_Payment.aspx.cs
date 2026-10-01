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
using System.Security.Principal;
using System.IO;

public partial class NCCF_NCCF_Bill_Approved_By_Acoount_For_Payment : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    DataTable dt = new DataTable();
    public string qry = "";
    string Bill_No = "";
    int BID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDistrict();
            GetCommodity();
            GetCropYear();
        }
    }

    public void GetCropYear()
    {
        string qry = "Select Distinct Crop_Year from tbl_NCCF_Storage_Bill_Details Order By Crop_Year ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcropyear.DataSource = ds.Tables[0];
            ddlcropyear.DataTextField = "Crop_Year";
            ddlcropyear.DataValueField = "Crop_Year";
            ddlcropyear.DataBind();
            ddlcropyear.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    public void GetCommodity()
    {
        string qry = "select distinct Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in ('63', '64', '33', '52', '27', '92', '123', '31', '65','26') order by Commodity_Name asc";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    public void GetDistrict()
    {
        string qry = "select distinct District_Id,District_Name from tbl_MetaData_DISTRICT  Order By District_Name ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }

    public void GetBranch()
    {
        string qry = "select distinct DepotID,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "'Order By DepotName ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "DepotID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbranch.SelectedValue == "0")
        {
            ddlGodown.Items.Clear();
            ddlGodown.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlGodown.Enabled = true;
            GetGodown(ddlbranch.SelectedValue);
        }
    }

    public void GetGodown(string branchId)
    {
        string qry = "select distinct Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 Where BranchID='" + branchId + "'Order By Godown_Name ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGodown.DataSource = ds.Tables[0];
            ddlGodown.DataTextField = "Godown_Name";
            ddlGodown.DataValueField = "Godown_ID";
            ddlGodown.DataBind();
            ddlGodown.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    public void fillgrid()
    {
        string CommodityID = ddlcommodity.SelectedValue;
        string Dist_id = ddldistrict.SelectedValue;
        string Branch_Id = ddlbranch.SelectedValue;
        string User_Type = Session["UserID"].ToString();
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_NCCF_Bill_Acknowledgement_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddldistrict.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@District_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@District_Id", Dist_id);
            }
            if (ddlbranch.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Branch_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
            }

            if (ddlFinancialyear.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Financial_Year", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Financial_Year", ddlFinancialyear.SelectedValue);
            }
            if (ddlmonth.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Month", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
            }
            if (ddlGodown.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Godown_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Godown_Id", ddlGodown.SelectedValue);
            }
            cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            cmd.Parameters.Add("@Created_By", SqlDbType.Int).Value = User_Type;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt != null && dt.Rows.Count > 0)
            {
                GrdBills.DataSource = dt;
                GrdBills.DataBind();
                ViewState["Bills"] = dt;
                grdbill.Visible = true;
            }
            else
            {
                GrdBills.DataSource = null;
                GrdBills.DataBind();
                grdbill.Visible = true;
            }
        }
    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    protected void GrdBills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Session["Bill_Number"] = (row.RowIndex).ToString();
            Label Billnumber = (Label)row.FindControl("lblBill_Number");
            string url = "State_Nafed_Print_Generate_Bill.aspx?BN=" + Base64Encode(Billnumber.Text);
            string s = "window.open('" + url + "', 'popup_window', 'width=600,height=600,left=100,top=100,resizable=yes');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", s, true);
        }
    }

    protected void GrdBills_DataBound(object sender, EventArgs e)
    {
        int totalBills = GrdBills.Rows.Count;
        decimal totalAmount = 0;

        foreach (GridViewRow row in GrdBills.Rows)
        {
            Label lblAmount = (Label)row.FindControl("lblNet_Amount");
            TextBox txtTDS = (TextBox)row.FindControl("txtTDS_Deduction");
            TextBox txtPSS = (TextBox)row.FindControl("txtPSS");
            TextBox txtPass = (TextBox)row.FindControl("txtpass");
            TextBox txtPSF = (TextBox)row.FindControl("txtpsf");
            TextBox txtOther = (TextBox)row.FindControl("txtOther_Deduction");

            if (lblAmount != null)
            {
                decimal amt = 0;
                decimal.TryParse(lblAmount.Text.Replace(",", ""), out amt);
                totalAmount += amt;

                decimal tds = amt * 0.10m;
                if (txtTDS != null)
                {
                    txtTDS.Text = tds.ToString("0.00");
                }

                decimal other = 0;
                if (txtOther != null) decimal.TryParse(txtOther.Text, out other);

                decimal pass = amt - tds - other;
                if (pass < 0) pass = 0;

                if (txtPass != null)
                {
                    txtPass.Text = pass.ToString("0.00");
                }

                decimal psf = 0;
                if (txtPSF != null) decimal.TryParse(txtPSF.Text, out psf);

                // PSS Amount = Pass Amount - PSF Amount
                if (txtPSS != null)
                {
                    decimal pss = pass - psf;
                    if (pss < 0) pss = 0;
                    txtPSS.Text = pss.ToString("0.00");
                }
            }
        }

        litTotalBills.Text = totalBills.ToString();
        litTotalAmount.Text = totalAmount.ToString("N2");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUTRNumber.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('UTR Number dalna zaroori hai!');", true);
            return;
        }
        if (string.IsNullOrWhiteSpace(txtPaymentDate.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Payment Date select karna zaroori hai!');", true);
            return;
        }

        int TotalCount = Convert.ToInt32(lblSelectedCount.Text);
        decimal TotalAmount = Convert.ToDecimal(lblSelectedAmount.Text);
        string Commodity = ddlcommodity.SelectedValue;
        String Crop_Year = ddlcropyear.SelectedValue;
        string Crop_YearEnd = Crop_Year.Length >= 2 ? Crop_Year.Substring(Crop_Year.Length - 2, 2) : "00";
        string CommodityId = Commodity.ToString();
        String Depositor_ID = "15478";
        qry = "select max(BId) as BId from tbl_NCCF_acknowledgement_No where  Depositor_Id='" + Depositor_ID + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count != 0 && dt.Rows[0]["BId"].ToString() != null && dt.Rows[0]["BId"].ToString() != "")
        {
            if (dt.Rows[0]["BId"].ToString() != "" || Convert.ToInt32(dt.Rows[0]["BId"]) != 0)
            {
                BID = Convert.ToInt32(dt.Rows[0]["BId"]);

                int SubBN = BID + 1;
                Bill_No = "15478" + Crop_YearEnd + CommodityId + "01" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + SubBN.ToString();
                BID = SubBN;
            }
            else
            {
                Bill_No = "15478" + Crop_YearEnd + CommodityId + "01" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
                BID = 1;
            }
        }
        else
        {
            Bill_No = "15478" + Crop_YearEnd + CommodityId + "01" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
            BID = 1;
        }
        ViewState["BillNo"] = Bill_No;
        ViewState["BID"] = BID;
        this.Insert(Commodity, Crop_Year, TotalCount, TotalAmount);
    }

    private void Insert(string Commodity, string Crop_Year, int TotalCount, Decimal TotalAmount)
    {
        SqlConnection dbCon = new SqlConnection(con_WLC1);
        SqlTransaction transaction = null;
        try
        {
            int ICount = 0;
            string ip = Request.ServerVariables["REMOTE_ADDR"] != null ? Request.ServerVariables["REMOTE_ADDR"].ToString() : "127.0.0.1";
            Bill_No = ViewState["BillNo"].ToString();
            BID = Convert.ToInt32(ViewState["BID"]);

            dbCon.Open();
            transaction = dbCon.BeginTransaction();

            const string qryMaster = "INSERT INTO tbl_NCCF_acknowledgement_No(Bill_Number,Depositor_Id,Commodity_ID,Crop_Year,Bill_Count,Total_Amount,BId,CreateBy,Createdby_Ip,CreatedOn) values(@Bill_Number,'15478',@Commodity_ID,@Crop_Year,@Bill_Count,@Total_Amount,@BId,@CreateBy,@Createdby_Ip,getdate())";
            SqlCommand cmdMaster = new SqlCommand(qryMaster, dbCon, transaction);
            cmdMaster.Parameters.AddWithValue("@Bill_Number", Bill_No);
            cmdMaster.Parameters.AddWithValue("@Commodity_ID", Commodity);
            cmdMaster.Parameters.AddWithValue("@Crop_Year", Crop_Year);
            cmdMaster.Parameters.AddWithValue("@Bill_Count", TotalCount);
            cmdMaster.Parameters.AddWithValue("@Total_Amount", TotalAmount);
            cmdMaster.Parameters.AddWithValue("@BId", BID);
            cmdMaster.Parameters.AddWithValue("@CreateBy", Session["UserID"].ToString());
            cmdMaster.Parameters.AddWithValue("@Createdby_Ip", ip);
            int n = cmdMaster.ExecuteNonQuery();

            if (n > 0)
            {
                foreach (GridViewRow row in GrdBills.Rows)
                {
                    CheckBox chkbox = (CheckBox)row.FindControl("Checked");
                    if (chkbox != null && chkbox.Checked == true)
                    {
                        TextBox txtOther = row.FindControl("txtOther_Deduction") as TextBox;
                        TextBox txtRemark = row.FindControl("txtDeduction_Remark") as TextBox;

                        decimal otherVal = 0;
                        decimal.TryParse(txtOther.Text, out otherVal);

                        if (otherVal > 0 && string.IsNullOrWhiteSpace(txtRemark.Text))
                        {
                            transaction.Rollback();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Other Deduction > 0 hone par Remark mandatory hai!');", true);
                            return;
                        }
                        else
                        {
                            string Godown_Bill = (row.FindControl("lblBill_Number") as Label).Text;

                            Label lblNetAmountControl = row.FindControl("lblNet_Amount") as Label;
                            HiddenField hdnCommIdControl = row.FindControl("hdnCommodityId") as HiddenField;
                            HiddenField hdnGodownIdControl = row.FindControl("hdnGodownId") as HiddenField;

                            string itemCommodityId = hdnCommIdControl != null ? hdnCommIdControl.Value : Commodity;
                            string godownId = hdnGodownIdControl != null ? hdnGodownIdControl.Value : "0";

                            decimal netAmountValue = 0;
                            if (lblNetAmountControl != null)
                            {
                                decimal.TryParse(lblNetAmountControl.Text.Replace(",", ""), out netAmountValue);
                            }

                            // Calculate Exact Values on Server Side
                            decimal tdsVal = netAmountValue * 0.10m;
                            decimal passVal = netAmountValue - tdsVal - otherVal;
                            if (passVal < 0) passVal = 0;

                            TextBox txtPSF = row.FindControl("txtpsf") as TextBox;
                            decimal psfVal = 0;
                            if (txtPSF != null) decimal.TryParse(txtPSF.Text, out psfVal);

                            // PSS = Pass Amount - PSF Amount
                            decimal pssVal = passVal - psfVal;
                            if (pssVal < 0) pssVal = 0;

                            string TDS_Deduction = tdsVal.ToString("0.00");
                            string Other_Deduction = otherVal.ToString("0.00");
                            string Deduction_Remark = (row.FindControl("txtDeduction_Remark") as TextBox).Text;
                            string pass_Amount = passVal.ToString("0.00");
                            string pss_Amount = pssVal.ToString("0.00");
                            string psf_Amount = psfVal.ToString("0.00");

                            // Update Table with correct PSS value (Pass - PSF)
                            string qry2 = "update tbl_NCCF_Marketing_Approve_Reject set Fin_Bill_No='" + ViewState["BillNo"].ToString() + "', TDS_Deduction ='" + TDS_Deduction + "' , Pass_Amount ='" + pass_Amount + "' ,Other_Deduction ='" + Other_Deduction + "' ,Deduction_Remark ='" + Deduction_Remark + "', PSF ='" + psf_Amount + "', PSS ='" + pss_Amount + "' where Bill_Number='" + Godown_Bill + "'";
                            SqlCommand cmd2 = new SqlCommand(qry2, dbCon, transaction);
                            int i = cmd2.ExecuteNonQuery();
                            ICount = ICount + i;

                            string qryNewTable = "INSERT INTO tbl_NCCF_Bill_Payment_Transaction(Fin_Bill_No, Godown_Bill_Number, Godown_ID, Commodity_ID, Crop_Year, NetAmount, TDS_Deduction, Other_Deduction, Deduction_Remark, UTR_Number, Payment_Date, Created_By, Created_IP, PSS, PSF, PassAmount) VALUES(@FinBill, @GodownBill, @GodownID, @CommID, @CYear, @NetAmt, @TDS, @Other, @Rem, @UTR, @PDate, @CBy, @IP, @PSS, @PSF, @PassAmount)";
                            SqlCommand cmdNew = new SqlCommand(qryNewTable, dbCon, transaction);
                            cmdNew.Parameters.AddWithValue("@FinBill", Bill_No);
                            cmdNew.Parameters.AddWithValue("@GodownBill", Godown_Bill);
                            cmdNew.Parameters.AddWithValue("@GodownID", godownId);
                            cmdNew.Parameters.AddWithValue("@CommID", itemCommodityId);
                            cmdNew.Parameters.AddWithValue("@CYear", Crop_Year);
                            cmdNew.Parameters.AddWithValue("@NetAmt", netAmountValue);
                            cmdNew.Parameters.AddWithValue("@TDS", tdsVal);
                            cmdNew.Parameters.AddWithValue("@Other", otherVal);
                            cmdNew.Parameters.AddWithValue("@Rem", string.IsNullOrEmpty(Deduction_Remark) ? "NA" : Deduction_Remark);
                            cmdNew.Parameters.AddWithValue("@UTR", txtUTRNumber.Text.Trim());
                            cmdNew.Parameters.AddWithValue("@PDate", DateTime.ParseExact(txtPaymentDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture));
                            cmdNew.Parameters.AddWithValue("@CBy", Session["UserID"].ToString());
                            cmdNew.Parameters.AddWithValue("@IP", ip);
                            cmdNew.Parameters.AddWithValue("@PSS", pssVal); // Correctly saved as Pass Amount - PSF Amount
                            cmdNew.Parameters.AddWithValue("@PSF", psfVal);
                            cmdNew.Parameters.AddWithValue("@PassAmount", passVal);
                            cmdNew.ExecuteNonQuery();
                        }
                    }
                }

                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('Bill Submit Successfully |||')", true);

                txtUTRNumber.Text = "";
                txtPaymentDate.Text = "";
                fillgrid();
                allcount.Visible = false;
            }
            else
            {
                transaction.Rollback();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
            }
        }
        catch (Exception ex)
        {
            if (transaction != null) transaction.Rollback();
            lblrmsg.Text = ex.Message;
        }
        finally
        {
            dbCon.Close();
        }
    }

    private void ProcessRowCalculations(GridViewRow row)
    {
        Label lblAmount = row.FindControl("lblNet_Amount") as Label;
        TextBox txtTDS = row.FindControl("txtTDS_Deduction") as TextBox;
        TextBox txtOther = row.FindControl("txtOther_Deduction") as TextBox;
        TextBox txtPass = row.FindControl("txtpass") as TextBox;
        TextBox txtPSF = row.FindControl("txtpsf") as TextBox;
        TextBox txtPSS = row.FindControl("txtPSS") as TextBox;

        if (lblAmount != null)
        {
            decimal net = 0;
            decimal.TryParse(lblAmount.Text.Replace(",", ""), out net);

            decimal tds = net * 0.10m;
            if (txtTDS != null) txtTDS.Text = tds.ToString("0.00");

            decimal other = 0;
            if (txtOther != null) decimal.TryParse(txtOther.Text, out other);

            decimal pass = net - tds - other;
            if (pass < 0) pass = 0;
            if (txtPass != null) txtPass.Text = pass.ToString("0.00");

            decimal psf = 0;
            if (txtPSF != null) decimal.TryParse(txtPSF.Text, out psf);

            decimal pss = pass - psf;
            if (pss < 0) pss = 0;
            if (txtPSS != null) txtPSS.Text = pss.ToString("0.00");
        }
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkHeaderCheck = sender as CheckBox;

        int count = 0;
        decimal total = 0;

        foreach (GridViewRow row in GrdBills.Rows)
        {
            ProcessRowCalculations(row); // Recalculate on PostBack to sync PSS field

            CheckBox ckRowSel = row.FindControl("Checked") as CheckBox;

            TextBox txtOther = row.FindControl("txtOther_Deduction") as TextBox;
            TextBox txtRemark = row.FindControl("txtDeduction_Remark") as TextBox;
            TextBox txtPSF = row.FindControl("txtpsf") as TextBox;
            TextBox txtPass = row.FindControl("txtpass") as TextBox;

            if (ckRowSel != null)
            {
                ckRowSel.Checked = chkHeaderCheck.Checked;

                if (chkHeaderCheck.Checked)
                {
                    if (txtOther != null)
                    {
                        txtOther.ReadOnly = true;
                        txtOther.CssClass = "Other_Deduction readonly-box";
                    }
                    if (txtRemark != null)
                    {
                        txtRemark.ReadOnly = true;
                        txtRemark.CssClass = "Deduction_Remark readonly-box";
                    }
                    if (txtPSF != null)
                    {
                        txtPSF.ReadOnly = true;
                        txtPSF.CssClass = "psf-amount readonly-box";
                    }
                }
                else
                {
                    if (txtOther != null)
                    {
                        txtOther.ReadOnly = false;
                        txtOther.CssClass = "Other_Deduction";
                    }
                    if (txtPSF != null)
                    {
                        txtPSF.ReadOnly = false;
                        txtPSF.CssClass = "psf-amount";
                    }
                    if (txtRemark != null && txtOther != null)
                    {
                        decimal otherVal;
                        if (decimal.TryParse(txtOther.Text, out otherVal) && otherVal > 0)
                        {
                            txtRemark.ReadOnly = false;
                            txtRemark.CssClass = "Deduction_Remark";
                        }
                        else
                        {
                            txtRemark.ReadOnly = true;
                            txtRemark.CssClass = "Deduction_Remark readonly-box";
                        }
                    }
                }

                if (ckRowSel.Checked && txtPass != null)
                {
                    decimal amt;
                    if (decimal.TryParse(txtPass.Text, out amt))
                    {
                        total += amt;
                        count++;
                    }
                }
            }
        }

        lblSelectedAmount.Text = total.ToString("0.00");
        lblSelectedCount.Text = count.ToString();

        lblSelectedAmount.Visible = count > 0;
        lblSelectedCount.Visible = count > 0;
        lbltotalsebils.Visible = count > 0;
        lblselecAmount.Visible = count > 0;
        btnSubmit.Visible = count > 0;
        allcount.Visible = true;
    }

    protected void Checked_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ckRowSel = sender as CheckBox;
        GridViewRow row = ckRowSel.NamingContainer as GridViewRow;

        TextBox txtOther = row.FindControl("txtOther_Deduction") as TextBox;
        TextBox txtRemark = row.FindControl("txtDeduction_Remark") as TextBox;
        TextBox txtPSF = row.FindControl("txtpsf") as TextBox;

        if (ckRowSel.Checked)
        {
            if (txtOther != null)
            {
                txtOther.ReadOnly = true;
                txtOther.CssClass = "Other_Deduction readonly-box";
            }
            if (txtRemark != null)
            {
                txtRemark.ReadOnly = true;
                txtRemark.CssClass = "Deduction_Remark readonly-box";
            }
            if (txtPSF != null)
            {
                txtPSF.ReadOnly = true;
                txtPSF.CssClass = "psf-amount readonly-box";
            }
        }
        else
        {
            if (txtOther != null)
            {
                txtOther.ReadOnly = false;
                txtOther.CssClass = "Other_Deduction";
            }
            if (txtPSF != null)
            {
                txtPSF.ReadOnly = false;
                txtPSF.CssClass = "psf-amount";
            }
            if (txtRemark != null && txtOther != null)
            {
                decimal otherVal;
                if (decimal.TryParse(txtOther.Text, out otherVal) && otherVal > 0)
                {
                    txtRemark.ReadOnly = false;
                    txtRemark.CssClass = "Deduction_Remark";
                }
                else
                {
                    txtRemark.ReadOnly = true;
                    txtRemark.CssClass = "Deduction_Remark readonly-box";
                }
            }
        }

        int count = 0;
        decimal total = 0;

        foreach (GridViewRow r in GrdBills.Rows)
        {
            ProcessRowCalculations(r); // Recalculate on PostBack to sync PSS field

            CheckBox chk = r.FindControl("Checked") as CheckBox;
            TextBox txt = r.FindControl("txtpass") as TextBox;

            if (chk != null && chk.Checked && txt != null)
            {
                decimal amt;
                if (decimal.TryParse(txt.Text, out amt))
                {
                    total += amt;
                    count++;
                }
            }
        }

        lblSelectedAmount.Text = total.ToString("0.00");
        lblSelectedCount.Text = count.ToString();

        lblSelectedAmount.Visible = count > 0;
        lblSelectedCount.Visible = count > 0;
        lbltotalsebils.Visible = count > 0;
        lblselecAmount.Visible = count > 0;
        btnSubmit.Visible = count > 0;
        allcount.Visible = true;
    }
}