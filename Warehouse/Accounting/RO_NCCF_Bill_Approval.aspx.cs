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
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
public partial class Accounting_RO_NCCF_Bill_Approval : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    DataSet ds2 = null;
    decimal ChargeOfTotal = 0;
    decimal RebateAmount = 0;
    decimal NetAmount = 0;
    decimal AccruedNetAmount = 0;
    string NetAmountWord = "";
    string Bill_No = "";
    decimal Discount = 0;
    decimal Service_Tax = 0;
    string Bill_Type = "";
    int BID = 0;
    public string GenerateOTP = "", OTPSMS = "";
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Region_ID"] != null))
        {
            if (!IsPostBack)
            {
                //ModalPopupExtender1.Show();
                GetBillsDetail();
                //string MoBNo = ChkMobNo_ForOTP();
                string MoBNo = "";
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void gvBOBillApp_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubmitBill();
    }
    public void SubmitBill()
    {
        SqlTransaction sqltran = null;
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            sqltran = con.BeginTransaction();
            string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
            string str = "update tbl_NCCF_Storage_Bill_Details set RO_Approval_Status='Y',RO_Approval_Date=getdate(),RO_Approval_IP='" + ClientIP + "' where Bill_Number='" + gvBOBillApp.SelectedRow.Cells[3].Text.ToString() + "'";

            cmd = new SqlCommand(str, con, sqltran);
            int req = cmd.ExecuteNonQuery();
            if (req > 0)
            {
                sqltran.Commit();
                con.Close();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Sussessfully Submit Bill ....')", true);

                GetBillsDetail();
            }
            else
            {
                //lbl_message.Text = "WHR record saved successfully";
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            Response.Write(ex.Message);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

    }
    //}
    private void GetBillsDetail()
    {

        try
        {
            //string Dist_id = Session["Depot_DistID"].ToString();
            string Region_Id = Session["Region_ID"].ToString();
            SqlCommand cmd = new SqlCommand("[dbo].[Get_NCCF_RO_Final_Bill_Details]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Region_id", Region_Id.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                gvBOBillApp.DataSource = dt;
                gvBOBillApp.DataBind();
            }
            else
            {
                gvBOBillApp.DataSource = "";
                gvBOBillApp.DataBind();
            }

        }

        catch (Exception ex)
        {

        }
    }
    protected void GenerateUniqueOTP()
    {
        string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "1234567890";

        string characters = numbers;

        characters += alphabets + small_alphabets + numbers;

        string MONumber = gvBOBillApp.SelectedRow.Cells[1].Text.ToString();
        //   string MONumber = "";
        int lastdigit = int.Parse(MONumber.Substring(6));
        int length = 0;
        if (lastdigit >= 6)
        {
            length = 8;
        }
        else if (lastdigit >= 3 && lastdigit <= 5)
        {
            length = 6;
        }
        else
        {
            length = 5;
        }

        //int length = int.Parse(ddlMvmtNo.SelectedItem.Value);
        string otp = string.Empty;
        for (int i = 0; i < length; i++)
        {
            string character = string.Empty;
            do
            {
                int index = new Random().Next(0, characters.Length);
                character = characters.ToCharArray()[index].ToString();
            } while (otp.IndexOf(character) != -1);
            otp += character;
        }

        GenerateOTP = otp;
        //  OTPSMS = "'" + ddl_commodity.SelectedItem.Text + "' Depositor Form Number " + ddl_depositerform.SelectedItem.Text + " OTP Is '" + otp + "'";
        OTPSMS = "Net Amount '" + gvBOBillApp.SelectedRow.Cells[5].Text.ToString() + "' Bill Number " + gvBOBillApp.SelectedRow.Cells[1].Text.ToString() + " OTP Is " + otp + " ";

        SMS Message = new SMS();
        string MobileNo = "";

    }
    public string ChkMobNo_ForOTP()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string ReturnMobNo = "";
        string QueryMax = "select Mobile_No from [tbl_Warehousing_Contact] where Branch_ID='" + Session["BranchId"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(QueryMax, con);
        string str3 = cmd.ExecuteScalar().ToString();
        if ((str3 != String.Empty) || str3 != "" || str3 != "0")
        {
            ReturnMobNo = str3;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Mobile No Available For Sending OTP Please Contact HeadOffice Bhopal ....')", true);
        }
        return ReturnMobNo;
    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        SubmitBill();
    }
}