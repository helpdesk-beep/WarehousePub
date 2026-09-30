using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounts_MPWLC_FailedPaymentRepushbyAccountSection_NCCF : System.Web.UI.Page
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

            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void GetBillsDetail()
    {
        try
        {

            string str = "";

            cmd = new SqlCommand("dbo.Get_Failed_Payment_Status_NCCF", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BillNumber", txtBillNumber.Text.ToString());
            //SqlDataAdapter da = new SqlDataAdapter(str, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);


            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBOBillApp.DataSource = ds.Tables[0];
                gvBOBillApp.DataBind();
                //trnewproc.Visible = true;
            }
            else
            {
                gvBOBillApp.DataSource = null;
                gvBOBillApp.DataBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data Found'); </script> ");


            }

        }

        catch (Exception ex)
        {

        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        Insert_Bill_Detail();
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
            string Uqry = "";
            string RegionID = hdnRegionID.Value;
            string AID = "";
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
                    Rent_Bill_No = hdnRef_Bill_No.Value;
                    Godown_ID = hdnGodown_Id.Value;
                    qry = "insert into tbl_Payment_Credit_NCCF_Log SELECT [Trans_Id] ,[Region_Id] ,[District_Id] ,[Branch_Id] ,[Account_No] ,[IFSC_Code] ,[Branch_Code] ,[Party_Name] ,[Date] ,[Credit_Amount] ,[Trans_Description] ,[Payment_Identifier] ,[Reference_No] ,[Mobile_No] ,[Account_Type] ,[Amount_Type] ,[Created_By] ,[Created_Date],[Status] ,[Beneficiary_Id] ,[Bill_No] ,[Godown_Id] ,[Aid],[Updated_By],[Updated_Date],'" + Created_By + "','"+DateTime.Now.ToString()+ "',[Depositor_Type] From [dbo].[tbl_Payment_Credit_NCCF] where Bill_no='" + Rent_Bill_No + "'";

                    SqlCommand cmd2 = new SqlCommand(qry, con);
                    con.Open();
                    int i = cmd2.ExecuteNonQuery();
                    con.Close();

                    Uqry = "Delete From [dbo].[tbl_Payment_Credit_NCCF] where Bill_no='" + Rent_Bill_No + "'";

                    SqlCommand Ucmd = new SqlCommand(Uqry, con);
                    con.Open();
                    int Ii = Ucmd.ExecuteNonQuery();
                    con.Close();
                    lblRespMsg.Text = "Your Payment File has been Unmerge : ";
                    lblRespMsgNo.Text = Ref_Number;
                    lblRespMsg.Visible = true;
                    lblRespMsgNo.Visible = true;
                    //GeneratePaymentFile();
                    btn_save.Visible = false;
                    GetBillsDetail();
                    txtBillNumber.Text = "";
                }
                chkbox.Enabled = false;
                ChkBoxHeader.Enabled = false;
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
    protected void btn_Sbi_Link_Click(object sender, EventArgs e)
    {
        //Response.Redirect("https://yonobusiness.sbi/login/yonobusinesslogin");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetBillsDetail();
    }
}