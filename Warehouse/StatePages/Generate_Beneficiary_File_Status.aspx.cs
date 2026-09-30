using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

public partial class StatePages_Generate_Beneficiary_File_Status : System.Web.UI.Page
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
                //fillMonth();
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
            if (ddlBankType.SelectedValue == "S")
            {
                if (ddlStatus.SelectedValue == "1")
                {
                    str = "select Beneficiary_Id, Beneficiary_Name, Account_No, SUBSTRING(IFSC_Code, 5, 7) IFSC_Code, Mobile from tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICT on tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id where  GO_Approval_Status = 'Y' and Beneficiary_Type = 'S' and Flag<>'Y'";
                }
                else if (ddlStatus.SelectedValue == "2")
                {
                    str = "select Beneficiary_Id, Beneficiary_Name, Account_No, SUBSTRING(IFSC_Code, 5, 7) IFSC_Code, Mobile from tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICT on tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id where  GO_Approval_Status = 'Y' and Beneficiary_Type = 'S' and Flag='Y'";
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select File Status'); </script> ");
                }

            }
            else if (ddlBankType.SelectedValue == "O")
            {
                if (ddlStatus.SelectedValue == "1")
                {
                    str = "select Beneficiary_Id, Beneficiary_Name, Account_No, SUBSTRING(IFSC_Code, 5, 7) IFSC_Code, Mobile from tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICT on tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id where  GO_Approval_Status = 'Y' and Beneficiary_Type = 'O' and Flag<>'Y'";
                }
                else if (ddlStatus.SelectedValue == "2")
                {
                    str = "select Beneficiary_Id, Beneficiary_Name, Account_No, SUBSTRING(IFSC_Code, 5, 7) IFSC_Code, Mobile from tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICT on tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id where  GO_Approval_Status = 'Y' and Beneficiary_Type = 'O' and Flag='Y'";
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select File Status'); </script> ");
                }
            }

            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBOBillApp.DataSource = ds.Tables[0];
                gvBOBillApp.DataBind();
                trnewproc.Visible = true;
                lblNoofAC.Text = ds.Tables[0].Rows.Count.ToString();

            }
            else
            {
                gvBOBillApp.DataSource = null;
                gvBOBillApp.DataBind();
                trnewproc.Visible = false;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data Found'); </script> ");
            }

        }

        catch (Exception ex)
        {

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

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounting/Generate_Beneficiary.aspx");
    }



    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillsDetail();
    }
}