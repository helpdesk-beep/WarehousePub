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

public partial class Accounting_frm_Add_Beneficiary_Account : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                Beneficiary_List();
                //GetBankDistrict();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void Beneficiary_List()
    {
        string Dist_id = Session["Depot_DistID"].ToString();
        string sid = Session["BranchID"].ToString();
        ddlBenificiary.Items.Clear();
        //qry = "select distinct Acc_Holder_Name,Account_No from tbl_Godown_Owner_Account_Details where District_Id='"+ Dist_id +"' and Branch_Id='"+ sid +"' and RO_Approval_Status='Y' and GO_Approval_Status='Y' order by Acc_Holder_Name asc";
        //qry = "select distinct Acc_Holder_Name,Account_No from tbl_Godown_Owner_Account_Details where District_Id='" + Dist_id + "' and Branch_Id='" + sid + "' and RO_Approval_Status='Y' and GO_Approval_Status='Y' and Account_No not in (select Account_No from tbl_Beneficiary_Account_Details) order by Acc_Holder_Name asc";
        //qry = "select distinct Acc_Holder_Name,Account_No from tbl_Godown_Owner_Account_Details where District_Id='" + Dist_id + "' and Branch_Id='" + sid + "' and RO_Approval_Status='Y' and GO_Approval_Status='Y' and Account_No not in (select Account_No from tbl_Beneficiary_Account_Details) union select distinct Acc_Holder_Name,Account_No from tbl_Godown_Owner_Account_Details inner join tbl_MetaData_GODOWN_2018 on tbl_MetaData_GODOWN_2018.Godown_ID=tbl_Godown_Owner_Account_Details.Godown_Id where District_Id='" + Dist_id + "' and Branch_Id='" + sid + "' and RO_Approval_Status='Y' and tbl_MetaData_GODOWN_2018.Hired_Type='Hired' and Account_No not in (select Account_No from tbl_Beneficiary_Account_Details) order by Acc_Holder_Name asc";
        qry = "select distinct Acc_Holder_Name,Account_No from tbl_Godown_Owner_Account_Details where Branch_Id='" + sid + "' and RO_Approval_Status='Y' and GO_Approval_Status='Y' and Account_No not in (select Account_No from tbl_Beneficiary_Account_Details) union select distinct Acc_Holder_Name,Account_No from tbl_Godown_Owner_Account_Details inner join tbl_MetaData_GODOWN_2018 on tbl_MetaData_GODOWN_2018.Godown_ID=tbl_Godown_Owner_Account_Details.Godown_Id where Branch_Id='" + sid + "' and RO_Approval_Status='Y' and tbl_MetaData_GODOWN_2018.Hired_Type='Hired' and Account_No not in (select Account_No from tbl_Beneficiary_Account_Details) order by Acc_Holder_Name asc";

        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlBenificiary.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlBenificiary.DataSource = ds.Tables[0];
            ddlBenificiary.DataTextField = "Acc_Holder_Name";
            ddlBenificiary.DataValueField = "Account_No";
            ddlBenificiary.DataBind();
            ddlBenificiary.Items.Insert(0, "--Select--");
        }
    }
    public void Beneficiary_Account()
    {
        string Dist_id = Session["Depot_DistID"].ToString();
        string sid = Session["BranchID"].ToString();

        //qry = "select distinct Acc_Holder_Name,Account_No from tbl_Godown_Owner_Account_Details where District_Id='"+ Dist_id +"' and Branch_Id='"+ sid +"' and RO_Approval_Status='Y' and GO_Approval_Status='Y' order by Acc_Holder_Name asc";
        //qry = "select top 1 IFSC_Code,Account_No from tbl_Godown_Owner_Account_Details where Account_No='" + ddlBenificiary.SelectedValue.ToString() +"' and District_Id='"+ Dist_id +"' and Branch_Id='"+ sid +"' and RO_Approval_Status='Y' and GO_Approval_Status='Y'";
        //qry = "select top 1 IFSC_Code,Account_No from tbl_Godown_Owner_Account_Details where Account_No='" + ddlBenificiary.SelectedValue.ToString() + "' and District_Id='" + Dist_id + "' and Branch_Id='" + sid + "' and RO_Approval_Status='Y'";
        qry = "select top 1 IFSC_Code,Account_No from tbl_Godown_Owner_Account_Details where Account_No='" + ddlBenificiary.SelectedValue.ToString() + "' and Branch_Id='" + sid + "' and RO_Approval_Status='Y'";

        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
           
        }
        else
        {
            txtAccNo.Text = ds.Tables[0].Rows[0]["Account_No"].ToString();
            txtIFSCCode.Text = ds.Tables[0].Rows[0]["IFSC_Code"].ToString();
        }
    }

    protected void ddlBenificiary_SelectedIndexChanged(object sender, EventArgs e)
    {
        Beneficiary_Account();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (ddlBenificiary.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Party Name..'); </script> ");
        }
        else if (txtAccNo.Text == ""|| txtIFSCCode.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Invalid Account..'); </script> ");
        }
        else if (txtPAN.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter PAN..'); </script> ");
        }
        else if (txtMobile.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Mobile No..'); </script> ");
        }
        else if (txtEmailId.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Email Address..'); </script> ");
        }
        else if (txtAdd1.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Address1...'); </script> ");
        }
        else if (txtAdd2.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Address2....'); </script> ");
        }
        else if (txtAddCity.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter City..'); </script> ");
        }
        else
        {
            string Benificiary_Type = "";
            string IFSC = txtIFSCCode.Text;
            string IFSC_Substring=IFSC.Substring(0, 4);
            if (IFSC_Substring == "SBIN")
            {
                Benificiary_Type = "S";
            }
            else
            {
                Benificiary_Type = "O";
            }
            int Beni_Id = Get_Benificiery_Id();
            string DistrictId = Session["Depot_DistID"].ToString();
            string BranchId = Session["BranchId"].ToString();
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            //string qry = "INSERT INTO [tbl_Godown_Owner_Account_Details]([District_Id],[Branch_Id],[Godown_Id],[Acc_Holder_Name],[Account_No],[IFSC_Code],[Bank_District_Id],[Bank_Id],[Bank_Branch_Id],[Created_Date],[Created_By])  VALUES ('" + DistrictId + "','" + BranchId + "','" + ddlGodown.SelectedValue + "','" + txtGOwnerName.Text + "','" + txtAccNo.Text.Trim() + "','" + txtIFSC.Text + "','" + ddlDistrict.SelectedValue + "','" + ddlBankName.SelectedValue + "','" + ddlBBranch.SelectedValue + "',GETDATE(),'" + ip + "')";
            string qry = "INSERT INTO [dbo].[tbl_Beneficiary_Account_Details] (Beneficiary_Id,[Beneficiary_Type] ,[Beneficiary_Name] ,[Account_No] ,[IFSC_Code] ,[Beneficiary_Action_Type] ,[PAN] ,[GST] ,[Mobile] ,[Email] ,[Adress1] ,[Adress2] ,[Adress_City] ,[District_Id] ,[Branch_Id] ,[GO_Approval_Status] ,[GO_Approval_Date] ,[GO_Approval_IP] ,[RO_Approval_Status] ,[RO_Approval_Date] ,[RO_Approval_IP] ,[Created_Date],[Created_By] ,[DeletedBy] ,[DeletedDate] ,[Updated_Date],[Updated_By],Aadhar_No) values ('"+ Beni_Id + "','" + Benificiary_Type + "' ,'"+ ddlBenificiary.SelectedItem.Text +"' ,'"+ txtAccNo.Text +"' ,'"+ txtIFSCCode.Text +"' ,'A' ,'"+ txtPAN.Text +"' ,'"+ txtGSTNo.Text +"' ,'"+ txtMobile.Text +"' ,'"+ txtEmailId.Text +"','"+ txtAdd1.Text +"' ,'"+ txtAdd2.Text +"' ,'"+ txtAddCity.Text +"' ,'"+ DistrictId +"' ,'"+ BranchId +"' ,'' ,'' ,'' ,'' ,'','' ,Getdate(),'"+ ip +"' ,'' ,'' ,'','','"+ txtAadharNo.Text +"')";

            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            int c = cmd.ExecuteNonQuery();
            con.Close();
            if (c > 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Saved Successfully..'); </script> ");
                Beneficiary_List();
                txtAccNo.Text = "";
                txtIFSCCode.Text = "";
                txtGSTNo.Text = "";
                txtPAN.Text = "";
                txtMobile.Text = "";
                txtEmailId.Text = "";
                txtAdd1.Text = "";
                txtAdd2.Text = "";
                txtAddCity.Text = "";
                txtAadharNo.Text = "";
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Something Error..'); </script> ");
            }
        }
    }
    public int Get_Benificiery_Id()
    {
        int BID = 0;
        string BranchID = Session["BranchID"].ToString();
        qry = "select max(beneficiary_Id) as BId from tbl_Beneficiary_Account_Details";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        //string Bill_No = "";
        if (dt.Rows.Count != 0 && dt.Rows[0]["BId"].ToString() != null && dt.Rows[0]["BId"].ToString() != "")
        {
            if (dt.Rows[0]["BId"].ToString() != "" || Convert.ToInt32(dt.Rows[0]["BId"]) != 0)
            {
                BID = Convert.ToInt32(dt.Rows[0]["BId"]);

                int SubBN = BID + 1;
                BID = SubBN;
            }
        }
        return BID;
    }
}