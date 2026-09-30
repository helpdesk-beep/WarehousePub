using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Diagnostics;
using System.Resources;

public partial class Region_Update_Gdwn_Acc_No_ForPayment : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string qry = "";
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    string distnew = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                GetBranch();
                GetBankDistrict();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void GetBranch()
    {
        qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId in (select MD.District_Id from tbl_MetaData_DISTRICT as MD where MD.Region_ID='" + Session["Region_ID"].ToString() + "') order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "--Select--");

            ddlAddBranch.DataSource = ds.Tables[0];
            ddlAddBranch.DataTextField = "DepotName";
            ddlAddBranch.DataValueField = "BranchId";
            ddlAddBranch.DataBind();
            ddlAddBranch.Items.Insert(0, "--Select--");




            ddldeleteBranch.DataSource = ds.Tables[0];
            ddldeleteBranch.DataTextField = "DepotName";
            ddldeleteBranch.DataValueField = "BranchId";
            ddldeleteBranch.DataBind();
            ddldeleteBranch.Items.Insert(0, "--Select--");
        }
    }
    protected void Depositor_Gridview_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = Depositor_Gridview.SelectedRow;
        lblgdwnid.Text = gvr.Cells[0].Text;
        txtGdwnName.Text = gvr.Cells[1].Text;

        ddlDistrict.SelectedValue= gvr.Cells[8].Text;
        ddlDistrict_SelectedIndexChanged(null,null);
        ddlBankName.SelectedValue = gvr.Cells[9].Text;
        ddlBankName_SelectedIndexChanged(null, null);
        ddlBBranch.SelectedValue = gvr.Cells[10].Text;

        txtAccNo.Text = gvr.Cells[6].Text;
        txtAccNo2.Text = gvr.Cells[6].Text;
        txtGOwnerName.Text = gvr.Cells[5].Text;
        txtIFSC.Text = gvr.Cells[7].Text;

        btnClose.Focus();
        tr1.Visible = true;
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
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBankName();
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

    protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBank_Branches();
    }
    protected void ddlBBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetIFSC_Code();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  qry = "select Godown_Id,Bank_District_Id,Bank_Branch_Id,Bank_Id ,(select Godown_Name from tbl_MetaData_GODOWN_2018 as MG where MG.Godown_ID=ACCNO.Godown_Id) as Godown_Name,(select District_Name from tbl_MetaData_DISTRICT as MDD where MDD.District_Id=ACCNO.District_Id ) as DistrictName,(select distinct BANK from IfscBankmar15_2019 as BNK where BNK.BANK_ID=ACCNO.Bank_Id)  as BankName,(select distinct BRANCH from IfscBankmar15_2019 as BNKB where BNKB.BRANCH_ID=ACCNO.Bank_Branch_Id)  as BranchName, Acc_Holder_Name,Account_No,IFSC_Code from tbl_Godown_Owner_Account_Details as ACCNO where Branch_Id ='" + ddlBranch.SelectedValue.ToString() + "'";

        cleardata();

        qry = "select Godown_Id,Bank_District_Id,ACCNO.Bank_Id ,Bank_Branch_Id, Acc_Holder_Name,ACCNO.Account_No,ACCNO.IFSC_Code, (select Godown_Name from tbl_MetaData_GODOWN_2018 as MG where MG.Godown_ID=ACCNO.Godown_Id) as Godown_Name,(select District_Name from tbl_MetaData_DISTRICT as MDD where MDD.District_Id=ACCNO.District_Id ) as DistrictName,BranchName,BankName,ACCNO.GO_Approval_Status,ACCNO.RO_Approval_Status,CASE WHEN BAD.Account_No IS NULL THEN 'Beneficiary not Created' else 'Y' END AS Beneficiary from tbl_Godown_Owner_Account_Details as ACCNO LEFT JOIN tbl_Beneficiary_Account_Details BAD ON ACCNO.Account_No=BAD.Account_No  left join  (select distinct BANK as BankName,BANK_ID from IfscBankmar15_eUP ) as BNK on BNK.BANK_ID=ACCNO.Bank_Id left join (select distinct BRANCH as BranchName ,BRANCH_ID from IfscBankmar15_eUP ) as BNKB on BNKB.BRANCH_ID=ACCNO.Bank_Branch_Id  where ACCNO.Branch_Id ='" + ddlBranch.SelectedValue.ToString() + "' order by Godown_Name";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Depositor_Gridview.DataSource = ds;
            Depositor_Gridview.DataBind();
            Depositor_Gridview.Columns[8].Visible = false;
            Depositor_Gridview.Columns[9].Visible = false;
            Depositor_Gridview.Columns[10].Visible = false;
        }
        else
        {
            Depositor_Gridview.DataSource = "";
            Depositor_Gridview.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('No Record Find'); window.location =('Update_Gdwn_Acc_No_ForPayment.aspx');", true);
        }
    }
    public void cleardata()
    {
        txtAccNo.Text = "";
        txtAccNo2.Text = "";
        txtGOwnerName.Text = "";
        txtIFSC.Text = "";
        ddlBankName.DataSource = "";
        ddlBBranch.DataSource = "";
        tr1.Visible = false;
        Depositor_Gridview.DataSource = "";
        GetBankDistrict();
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (txtGdwnName.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Godown No..'); </script> ");
        }
        else if (txtGOwnerName.Text == "")
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
        else if (txtIFSC.Text == "")
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
        else
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

            //qry = "insert into tbl_Godown_Owner_Account_Details_log select * from tbl_Godown_Owner_Account_Details where Godown_Id='" + lblgdwnid.Text + "'";
            qry = "insert into tbl_Godown_Owner_Account_Details_log SELECT [AId],[District_Id],[Branch_Id],[Godown_Id],[Acc_Holder_Name],[Account_No],[IFSC_Code],[Bank_District_Id],[Bank_Id],[Bank_Branch_Id],[Created_Date],[Created_By],'" + ip + "',GETDATE(),[GO_Approval_Status],[GO_Approval_Date],[GO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[Updated_Date],[Updated_By] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Owner_Account_Details] where Godown_Id='" + lblgdwnid.Text + "'";

            SqlCommand cmd1 = new SqlCommand(qry, con);
            con.Open();
            int c1 = cmd1.ExecuteNonQuery();
            if (c1 == 1)
            {
                qry = "Update [tbl_Godown_Owner_Account_Details] SET [Acc_Holder_Name]='" + txtGOwnerName.Text.Trim() + "',[Account_No]='" + txtAccNo.Text.Trim() + "',[IFSC_Code]='" + txtIFSC.Text.Trim() + "',[Bank_District_Id]='" + ddlDistrict.SelectedValue.ToString() + "',[Bank_Id]='" + ddlBankName.SelectedValue.ToString() + "',[Bank_Branch_Id]='" + ddlBBranch.SelectedValue.ToString() + "',[Updated_Date]=GETDATE(),[Updated_By]='" + ip + "' where Godown_Id='" + lblgdwnid.Text + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                int c = cmd.ExecuteNonQuery();
                con.Close();
                if (c > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Successfully Update Record'); window.location =('Update_Gdwn_Acc_No_ForPayment.aspx');", true);
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
    protected void btncloseconfrm_Click(object sender, EventArgs e)
    {

    }
    protected void ddlAddBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodown();

        ModalPopupExtender1.Show();
    }

    public void GetGodown()
    {
        ddlGodown.Items.Clear();
        qry = "select Godown_Name +'( '+ Godown_ID +' )' as Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where  BranchID='" + ddlAddBranch.SelectedValue.ToString() + "' and Godown_ID not in (select Godown_ID from tbl_Godown_Owner_Account_Details where Branch_Id='" + ddlAddBranch.SelectedValue.ToString() + "') order by Godown_Name asc";
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
    protected void btnAddSubmit_Click(object sender, EventArgs e)
    {
        if (ddlAddBranch.SelectedItem.Text=="--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Branch..'); </script> ");
        }
        else if (ddlGodown.SelectedItem.Text=="--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Godown No..'); </script> ");
        }
        else if (txtA_Holder.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Account Holder Name..'); </script> ");
        }
        else if (txtA_BName.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Bank..'); </script> ");
        }
        else if (txtA_BankBranch.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Bank Branch..'); </script> ");
        }
        else if (txtA_IFSC.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter IFSC Code..'); </script> ");
        }
        else if (txtA_Acc.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Account No...'); </script> ");
        }
        else if (txtA_AccRe.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Re Type Account No....'); </script> ");
        }
        else if (txtA_Acc.Text.Trim().Trim() != txtA_AccRe.Text.Trim())
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Account No Does Not Matched..'); </script> ");
        }
        else
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

            qry = "select DistrictId from tbl_MetaData_DEPOT where BranchId='" + ddlAddBranch.SelectedValue.ToString() + "'";
            da = new SqlDataAdapter(qry, con);
            ds = new DataSet();
            da.Fill(ds);
            if (ds == null)
            {

            }
            else
            {
                distnew = ds.Tables[0].Rows[0]["DistrictId"].ToString();
            }


            //qry = "INSERT INTO [tbl_Godown_Owner_Account_Details]([District_Id],[Branch_Id],[Godown_Id],[Acc_Holder_Name],[Account_No],[IFSC_Code],[Bank_Id],[Bank_Branch_Id],[Created_Date],[Created_By])  VALUES ('" + distnew + "','" + ddlAddBranch.SelectedValue.ToString() + "','" + ddlGodown.SelectedValue + "','" + txtA_Holder.Text.Trim() + "','" + txtA_Acc.Text.Trim() + "','" + txtA_IFSC.Text.Trim() + "','" + txtA_BName.Text.Trim() + "','" + txtA_BankBranch.Text.Trim() + "',GETDATE(),'" + ip + "')";
            qry = "INSERT INTO [tbl_Godown_Owner_Account_Details]([District_Id],[Branch_Id],[Godown_Id],[Acc_Holder_Name],[Account_No],[IFSC_Code],[Bank_Id],[Bank_Branch_Id],[Created_Date],[Created_By])  VALUES ('" + distnew + "','" + ddlAddBranch.SelectedValue.ToString() + "','" + ddlGodown.SelectedValue + "','" + txtA_Holder.Text.Trim() + "','" + txtA_Acc.Text.Trim() + "','" + txtA_IFSC.Text.Trim() + "','','',GETDATE(),'" + ip + "')";

            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            int c = cmd.ExecuteNonQuery();
            con.Close();
            con.Close();
            if (c > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Successfully save Record'); window.location =('Update_Gdwn_Acc_No_ForPayment.aspx');", true);
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Something Error..'); </script> ");
            }
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        //qry = "insert into tbl_Godown_Owner_Account_Details_log select * from tbl_Godown_Owner_Account_Details where Godown_Id='" + ddlDeleteGodown.SelectedValue.ToString() + "'";
        qry = "insert into tbl_Godown_Owner_Account_Details_log SELECT [AId],[District_Id],[Branch_Id],[Godown_Id],[Acc_Holder_Name],[Account_No],[IFSC_Code],[Bank_District_Id],[Bank_Id],[Bank_Branch_Id],[Created_Date],[Created_By],'" + ip + "',GETDATE(),[GO_Approval_Status],[GO_Approval_Date],[GO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[Updated_Date],[Updated_By] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Owner_Account_Details] where Godown_Id='" + ddlDeleteGodown.SelectedValue.ToString() + "'";
        SqlCommand cmd1 = new SqlCommand(qry, con);
        con.Open();
        int c1 = cmd1.ExecuteNonQuery();
        if (c1 == 1)
        {
            qry = "Delete [tbl_Godown_Owner_Account_Details]  where Godown_Id='" + ddlDeleteGodown.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            int c = cmd.ExecuteNonQuery();
            con.Close();
            if (c > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Successfully Deleted Record'); window.location =('Update_Gdwn_Acc_No_ForPayment.aspx');", true);
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
    protected void ddldeleteBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        qry = "select Godown_Name +'( '+ Godown_ID +' )' as Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where  BranchID='" + ddldeleteBranch.SelectedValue.ToString() + "' and Godown_ID in (select Godown_ID from tbl_Godown_Owner_Account_Details where Branch_Id='" + ddldeleteBranch.SelectedValue.ToString() + "') order by Godown_Name asc";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlGodown.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlDeleteGodown.DataSource = ds.Tables[0];
            ddlDeleteGodown.DataTextField = "Godown_Name";
            ddlDeleteGodown.DataValueField = "Godown_ID";
            ddlDeleteGodown.DataBind();
            ddlDeleteGodown.Items.Insert(0, "--Select--");
        }
        ModalPopupExtender2.Show();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Welcome.aspx");
    }
}
