using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounting_Generate_Beneficiary_By_Branch : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    SqlTransaction sqltrans;
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
                fillDistrict();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    private void fillDistrict()
    {
        try
        {

            string query = "";

            query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "--Select--");


            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    private void fillIssuecenter()
    {
        try
        {
            
            string query = "";
            
                query = "SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "'";
            
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbranch.Items.Clear();
                ddlbranch.DataSource = ds.Tables[0];
                ddlbranch.DataTextField = "DepotName";
                ddlbranch.DataValueField = "BranchId";
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, "--Select--");

            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillIssuecenter();
    }
    private void GetBillsDetail()
    {
        try
        {

            string str = "";
            if (ddlBankType.SelectedValue == "S")
            {
                //str = "select Ref_Bill_No,RPO.Account_No,RPO.IFSC_Code,CONVERT(varchar(10),GETDATE(),103) as Transaction_Date,Net_Amount,Party_Name,Godown_Id,(select G.Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=RPO.Godown_Id) as Godown_Name,B.Beneficiary_Id as Beneficiary_Id from tbl_Digitally_Signed_Bill_RPO as RPO inner join tbl_Beneficiary_Account_Details as B on B.Account_No=RPO.Account_No where Net_Amount>0 and B.Beneficiary_Type='S' and RPO.District_Id='" + Dist_id + "' and RPO.Branch_Id='" + Branch_Id + "' and RPO.Month_No='"+ ddlmonth.SelectedValue +"'";
                //str = "select Beneficiary_Id, Beneficiary_Name, Account_No, SUBSTRING(IFSC_Code, 5, 7) IFSC_Code, Mobile from tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICT on tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id where  Beneficiary_Type = 'S' and tbl_Beneficiary_Account_Details.District_Id='" + ddlDistrict.SelectedValue + "' and Branch_Id='" + ddlbranch.SelectedValue + "'";
                str = "select Beneficiary_Id, Beneficiary_Name, Account_No, SUBSTRING(IFSC_Code, 5, 7) IFSC_Code, Mobile from tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICT on tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id where  Beneficiary_Type = 'S' and Branch_Id='" + ddlbranch.SelectedValue + "'";

            }
            else if (ddlBankType.SelectedValue == "O")
            {
                //str = "select Ref_Bill_No,RPO.Account_No,RPO.IFSC_Code,CONVERT(varchar(10),GETDATE(),103) as Transaction_Date,Net_Amount,Party_Name,Godown_Id,(select G.Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=RPO.Godown_Id) as Godown_Name,B.Beneficiary_Id as Beneficiary_Id from tbl_Digitally_Signed_Bill_RPO as RPO inner join tbl_Beneficiary_Account_Details as B on B.Account_No=RPO.Account_No where Net_Amount>0 and B.Beneficiary_Type='O' and RPO.District_Id='" + Dist_id + "' and RPO.Branch_Id='" + Branch_Id + "' and RPO.Month_No='" + ddlmonth.SelectedValue + "'";
                //str = "select Beneficiary_Id, Beneficiary_Name, Account_No, SUBSTRING(IFSC_Code, 5, 7) IFSC_Code, Mobile from tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICT on tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id where Beneficiary_Type = 'O' and tbl_Beneficiary_Account_Details.District_Id='" + ddlDistrict.SelectedValue + "' and Branch_Id='" + ddlbranch.SelectedValue + "'";
                str = "select Beneficiary_Id, Beneficiary_Name, Account_No, SUBSTRING(IFSC_Code, 5, 7) IFSC_Code, Mobile from tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICT on tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id where Beneficiary_Type = 'O' and Branch_Id='" + ddlbranch.SelectedValue + "'";

            }

            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBOBillApp.DataSource = ds.Tables[0];
                gvBOBillApp.DataBind();
                trnewproc.Visible = true;
                tblbtn.Visible = true;
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

    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillsDetail();
        lblRespMsg.Text = "";
        lblRespMsgNo.Text = "";
        lblRespMsg.Visible = false;
        lblRespMsgNo.Visible = false;
    }



    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StatePages/Generate_Beneficiary_By_Branch.aspx");
    }

    protected void ddlBankType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillsDetail();
        lblRespMsg.Text = "";
        lblRespMsgNo.Text = "";
        lblRespMsg.Visible = false;
        lblRespMsgNo.Visible = false;
    }
    public Boolean validate()
    {
        int chkstatus = 0;
        foreach (GridViewRow row in gvBOBillApp.Rows)
        {
            CheckBox chk_Sum = (CheckBox)(row.FindControl("chk_Sum"));
            HiddenField hdnBeneficiary_Id = (HiddenField)(row.FindControl("hdnBeneficiary_Id"));

            if (chk_Sum.Checked == true)
            {
                chkstatus = chkstatus + 1;
            }
        }
        if (chkstatus > 0)
        {
            return true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया कम से कम एक चेकबॉक्स चेक करे ')", true);
            return false;
        }
    }
    public void Delete(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            if (validate())
            {
                foreach (GridViewRow row in gvBOBillApp.Rows)
                {
                    CheckBox chk_Sum = (CheckBox)(row.FindControl("chk_Sum"));
                    HiddenField hdnBeneficiary_Id = (HiddenField)(row.FindControl("hdnBeneficiary_Id"));
                    HiddenField hdnAccount_No = (HiddenField)(row.FindControl("hdnAccount_No"));

                    if (chk_Sum.Checked == true)
                    {
                        con.Open();
                        cmd = new SqlCommand("[dbo].[Delete_Beneficiary_Account_Details]", con, sqltrans);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Beneficiary_Id", hdnBeneficiary_Id.Value);
                        cmd.Parameters.AddWithValue("@AccountNo", hdnAccount_No.Value);
                        cmd.Parameters.AddWithValue("@DeletedBy", Request.UserHostAddress);
                        int res = cmd.ExecuteNonQuery();
                        if (res > 0)
                        {
                            count++;
                        }
                        con.Close();
                    }
                }
                if (count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record deleted successfully..')", true);
                    GetBillsDetail();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record NOT deleted')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }
        finally
        {
            con.Close();
        }
    }
}