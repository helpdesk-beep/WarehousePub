using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public partial class Accounting_Print_BM_Generated_Bill_For_DMO_Markfed : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string qry;
    decimal TT1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["BN"].ToString()))
            {
                GetBillOtherDataActual(Base64Decode(Request.QueryString["BN"].ToString()));
                fillgrid();
                // DSCSign();
            }
        }
    }
    public void GetBillOtherDataActual(String BillNo)
    {
        SqlCommand cmd = new SqlCommand("Get_Bill_Other_Data_Actual_Dmo_Markfed", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BillNumber", BillNo);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lbldatefromto.Text = dt.Rows[0]["Month"].ToString();
            lbldepositor.Text = "DMO_MARKFED";
            lblbillno_Actual.Text = dt.Rows[0]["Bill_Number"].ToString();
            lblcmd_ac.Text = dt.Rows[0]["Commodity_Name"].ToString() + " " + dt.Rows[0]["Crop_Year"].ToString();


            lblAcGdwnName.Text = dt.Rows[0]["Godown_Name"].ToString();
            lblCropyear.Text = dt.Rows[0]["Commodity_Rate"].ToString();
            lblBranch.Text = dt.Rows[0]["Branch"].ToString();
            lbldist.Text = dt.Rows[0]["District"].ToString();

            if (dt.Rows[0]["Hired_Type"].ToString().ToUpper().Contains("SILO BAGS"))
            {
                trSilo.Visible = true;
                lblSpAmt.Text = dt.Rows[0]["Sup_Charges_Amt"].ToString();
                lblGSTAmt.Text = dt.Rows[0]["GST_Sup_Amt"].ToString();
                lblBillNetAmount.Text = dt.Rows[0]["Net_Amount"].ToString();
            }
            else
            {
                trSilo.Visible = false;
            }
        }

        else
        {
            lbldatefromto.Text = "";
            lbldepositor.Text = "";
            lblbillno_Actual.Text = "";
            lblcmd_ac.Text = "";

            lblAcGdwnName.Text = "";
            lblCropyear.Text = "";
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetActual_BillData_DMO_Markfed]", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BillNumber", Base64Decode(Request.QueryString["BN"].ToString()));
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GD2.DataSource = dt;
                            GD2.DataBind();
                            GD2.FooterRow.Style.Add("text-align", "right");
                            GD2.FooterRow.Cells[6].Text = "Total";
                            GD2.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Charges")).ToString();

                        }
                        else
                        {
                            GD2.DataSource = null;
                            GD2.DataBind();
                        }
                    }
                }
            }
        }
    }
    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
    public void DSCSign()
    {
        qry = " select DSC_Serial_No,DSC_Holder_Name,Client_Ip,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_Details where Ref_Bill_No='" + Base64Decode(Request.QueryString["BN"].ToString()) + "'  and DSC_User_Type='B'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Image1.Visible = true;
            lblBSerialNo.Text = "DSC Serial No : " + dt.Rows[0]["DSC_Serial_No"].ToString();
            lblBIP.Text = "Client IP : " + dt.Rows[0]["Client_Ip"].ToString();
            lblBHolderName.Text = "DSC Holder Name : " + dt.Rows[0]["DSC_Holder_Name"].ToString();
            lblBCreatedDate.Text = "DSC Sign Date : " + dt.Rows[0]["CreatedDate"].ToString();
        }
        else
        {
            Image1.Visible = false;
            lblBSerialNo.Text = "";
            lblBIP.Text = "";
            lblBHolderName.Text = "";
            lblBCreatedDate.Text = "";
        }

        qry = "SELECT 'eSing'as UserName,'eSing@$data#$!Verification'as Pwd,d.Bill_Number,d.District_Id,d.Branch_Id,d.Depositor_Id,d.Commodity_Id,d.Financial_Year,d.Per_Month_Rate,d.Per_Day_Rate,d.Net_Amount,d.Sub_Amount,d.GST_Perc,d.GST_Amt,d.Created_Date_Bill,d.Created_By_Bill,d.Month_No,d.Godown_Id,d.Crop_Year, d.Account_No,d.IFSC_Code,d.WHR_Check_Sum,d.CreatedDate,STUFF(STUFF(CONVERT(CHAR(20), CreatedDate, 113),3,1, '-'),7,1,'-')CreatedDate1,d.CreatedBy,d.DSC_Serial_No,d.DSC_Holder_Name,d.Client_Ip,d.DSC_User_Type,d.csms_DSC_H_Name,d.csms_DSC_Serial_No,d.csms_DSC_User_Type ,d.csms_CreatedBy,d.csms_Check_Sum ,d.csms_CreatedDate,d.csms_Client_Ip,(select distinct c.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY c where c.Commodity_Id=d.Commodity_Id)CommodityName,(select distinct  i.BranchName from Intergrated_MP_STORAGE.dbo.MetaDataBranchWithIssueCenter i where i.BranchID=Branch_Id)BranchName,(select distinct  g.Godown_Name from  Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 g where g.Godown_ID=d.Godown_Id)GodownName,(select distinct  i.IssueCenterName from Intergrated_MP_STORAGE.dbo.MetaDataBranchWithIssueCenter i where i.IssueCenterId=d.csms_CreatedBy)IssueCenterName,STUFF(STUFF(CONVERT(CHAR(20), csms_CreatedDate, 113),3,1, '-'),7,1,'-')csms_CreatedDate1,(select REPLACE(CONVERT(VARCHAR(11),min(csms_Dates),106), ' ','-') from MPSCSC.dbo.StorageBillVerification2019 s where d.Bill_Number='" + Base64Decode(Request.QueryString["BN"].ToString()) + "')FromDate,(select REPLACE(CONVERT(VARCHAR(11),Max(csms_Dates),106), ' ','-') from MPSCSC.dbo.StorageBillVerification2019 s where d.Bill_Number='" + Base64Decode(Request.QueryString["BN"].ToString()) + "')ToDate from MPSCSC.dbo.Digitally_Sign_StorageBill_IC d where d.Bill_Number='" + Base64Decode(Request.QueryString["BN"].ToString()) + "'";
        SqlCommand cmd2 = new SqlCommand(qry, con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataTable dt2 = new DataTable();
        da2.Fill(dt2);
        if (dt2.Rows.Count > 0)
        {
            Image2.Visible = true;
            lblICSerialNo.Text = "DSC Serial No : " + dt2.Rows[0]["csms_DSC_Serial_No"].ToString();
            lblICIp.Text = "Client IP : " + dt2.Rows[0]["csms_Client_Ip"].ToString();
            lblICHoldername.Text = "DSC Holder Name : " + dt2.Rows[0]["csms_DSC_H_Name"].ToString();
            lblICCreatedDate.Text = "DSC Sign Date : " + dt2.Rows[0]["csms_CreatedDate1"].ToString();
        }
        else
        {
            Image2.Visible = false;
            lblICSerialNo.Text = "";
            lblICIp.Text = "";
            lblICHoldername.Text = "";
            lblICCreatedDate.Text = "";
        }
    }
}