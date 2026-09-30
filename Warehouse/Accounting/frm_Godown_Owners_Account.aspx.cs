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

public partial class Accounting_frm_Godown_Owners_Account : System.Web.UI.Page
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
                GetGodown();
                GetBankDistrict();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void GetGodown()
    {
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string sid = Session["BranchID"].ToString();
        ddlGodown.Items.Clear();
        qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Godown_ID not in (select Godown_ID from tbl_Godown_Owner_Account_Details where Branch_Id='" + Session["BranchID"].ToString() + "') order by Godown_Name asc";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlGodown.Items.Insert(0, "--Select--");
        }
        else
        {
                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
                ddlGodown.Items.Insert(0, "--Select--");
        }
    }
    public void GetBankDistrict()
    {
       
        ddlDistrict.Items.Clear();
        qry = "select District_Name,District_Id from tbl_MetaData_DISTRICT order by District_Name asc";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlDistrict.Items.Insert(0, "--Select--");
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
    public void GetBankName()
    {
        ddlBankName.Items.Clear();
        qry = "select distinct BANK,BANK_ID from IfscBankmar15_eUP where District_Id='" + ddlDistrict.SelectedValue + "' order by BANK asc";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlBankName.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlBankName.DataSource = ds.Tables[0];
            ddlBankName.DataTextField = "BANK";
            ddlBankName.DataValueField = "BANK_ID";
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, "--Select--");
        }
    }
    public void GetBank_Branches()
    {
        string Dist_id = ddlDistrict.SelectedValue;
        ddlBBranch.Items.Clear();
        qry = "select BRANCH,BRANCH_ID from IfscBankmar15_eUP where District_Id='" + Dist_id + "' and BANK_ID='" + ddlBankName.SelectedValue + "' order by BANK asc";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlBBranch.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlBBranch.DataSource = ds.Tables[0];
            ddlBBranch.DataTextField = "BRANCH";
            ddlBBranch.DataValueField = "BRANCH_ID";
            ddlBBranch.DataBind();
            ddlBBranch.Items.Insert(0, "--Select--");
        }
    }
    public void GetIFSC_Code()
    {
        string Dist_id = ddlDistrict.SelectedValue;
        //ddlBBranch.Items.Clear();
        qry = "select IFSC from IfscBankmar15_eUP where District_Id='" + Dist_id + "' and BANK_ID='" + ddlBankName.SelectedValue + "' AND BRANCH_ID='" + ddlBBranch.SelectedValue + "'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlBBranch.Items.Insert(0, "--Select--");
        }
        else
        {
            txtIFSC.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBankName();
    }
    protected void ddlBBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetIFSC_Code();
    }
    protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBank_Branches();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        if (ddlGodown.SelectedItem.Text=="--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Godown No..'); </script> ");
        }
        else if (txtGOwnerName.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Account Holder Name..'); </script> ");
        }
        else if (ddlDistrict.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select District..'); </script> ");
        }
        else if (ddlBankName.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Bank..'); </script> ");
        }
        else if (ddlBBranch.SelectedItem.Text == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Bank Branch..'); </script> ");
        }
        else if (txtIFSC.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter IFSC Code..'); </script> ");
        }
        else if (txtAccNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Account No...'); </script> ");
        }
        else if (txtAccNo2.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Re Type Account No....'); </script> ");
        }
        else if (txtAccNo.Text.Trim() != txtAccNo2.Text.Trim())
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Account No Does Not Matched..'); </script> ");
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
        else if (txtAddCity.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter City..'); </script> ");
        }
        else
        {
            string Benificiary_Type = "";
            string IFSC = txtIFSC.Text;
            string IFSC_Substring = IFSC.Substring(0, 4);
            if (IFSC_Substring == "SBIN")
            {
                Benificiary_Type = "S";
            }
            else
            {
                Benificiary_Type = "O";
            }
            string DistrictId = Session["Depot_DistID"].ToString();
            string BranchId = Session["BranchId"].ToString();
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string qry = "INSERT INTO [tbl_Godown_Owner_Account_Details]([District_Id],[Branch_Id],[Godown_Id],[Acc_Holder_Name],[Account_No],[IFSC_Code],[Bank_District_Id],[Bank_Id],[Bank_Branch_Id],[Created_Date],[Created_By])  VALUES ('" + DistrictId + "','" + BranchId + "','" + ddlGodown.SelectedValue + "','" + txtGOwnerName.Text + "','" + txtAccNo.Text.Trim() + "','" + txtIFSC.Text + "','" + ddlDistrict.SelectedValue + "','" + ddlBankName.SelectedValue + "','" + ddlBBranch.SelectedValue + "',GETDATE(),'" + ip + "')";
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            int c = cmd.ExecuteNonQuery();
            con.Close();
            if (c > 0)
            {
                string qry2 = "INSERT INTO [dbo].[tbl_Beneficiary_Account_Details] ([Beneficiary_Type] ,[Beneficiary_Name] ,[Account_No] ,[IFSC_Code] ,[Beneficiary_Action_Type] ,[PAN] ,[GST] ,[Mobile] ,[Email] ,[Adress1] ,[Adress2] ,[Adress_City] ,[District_Id] ,[Branch_Id] ,[GO_Approval_Status] ,[GO_Approval_Date] ,[GO_Approval_IP] ,[RO_Approval_Status] ,[RO_Approval_Date] ,[RO_Approval_IP] ,[Created_Date],[Created_By] ,[DeletedBy] ,[DeletedDate] ,[Updated_Date],[Updated_By],Aadhar_No) values ('" + Benificiary_Type + "' ,'" + txtGOwnerName.Text + "' ,'" + txtAccNo.Text + "' ,'" + txtIFSC.Text + "' ,'A' ,'" + txtPAN.Text + "' ,'" + txtGSTNo.Text + "' ,'" + txtMobile.Text + "' ,'" + txtEmailId.Text + "','" + txtAdd1.Text + "' ,'" + txtAdd2.Text + "' ,'" + txtAddCity.Text + "' ,'" + DistrictId + "' ,'" + BranchId + "' ,'' ,'' ,'' ,'' ,'','' ,Getdate(),'" + ip + "' ,'' ,'' ,'','','"+ txtAadharNo.Text +"')";
                SqlCommand cmd2 = new SqlCommand(qry2, con);
                con.Open();
                int c2 = cmd2.ExecuteNonQuery();
                con.Close();
                if (c2 > 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Saved Successfully..'); </script> ");
                    GetGodown();
                    txtAccNo.Text = "";
                    txtAccNo2.Text = "";
                    txtIFSC.Text = "";
                    txtGOwnerName.Text = "";
                    ddlBBranch.Items.Clear();
                    ddlBankName.Items.Clear();

                    txtAccNo.Text = "";
                    txtIFSC.Text = "";
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
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Something Error..'); </script> ");
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
}
