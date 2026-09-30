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
using System.IO;
using System.Text;

public partial class Accounting_RO_Download_EPF : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    SqlTransaction sqltrans;
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
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

                if (Session["Region_ID"].ToString() != null)
                {
                    region = Session["Region_ID"].ToString();

                }
            }
            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            }
            else
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
            }
            cmd = new SqlCommand(query, con);
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
                ddlDistrict.Items.Insert(0, "---Select---");
                //gv.DataSource = null;
                //gv.DataBind();
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
    public void GetBranch()
    {
        string qry = "";
        qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDepotList.DataSource = ds.Tables[0];
            ddlDepotList.DataTextField = "DepotName";
            ddlDepotList.DataValueField = "BranchId";
            ddlDepotList.DataBind();
            ddlDepotList.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex != 0)
        {
            GetBranch();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District')", true);
        }
    }
    private void GetBillsDetail()
    {
        try
        {
            string Dist_id = ddlDistrict.SelectedValue.ToString();
            string Branch_Id = ddlDepotList.SelectedValue.ToString();
            string str = "";
            //str = "select Ref_Bill_No,RPO.Account_No,RPO.IFSC_Code,CONVERT(varchar(10),GETDATE(),103) as Transaction_Date,Net_Amount,Party_Name,Godown_Id,(select G.Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=RPO.Godown_Id) as Godown_Name,B.Beneficiary_Id as Beneficiary_Id from tbl_Digitally_Signed_Bill_RPO as RPO inner join tbl_Beneficiary_Account_Details as B on B.Account_No=RPO.Account_No where Net_Amount>0 and B.Beneficiary_Type='O' and RPO.District_Id='" + Dist_id + "' and RPO.Branch_Id='" + Branch_Id + "' and RPO.Month_No='" + ddlmonth.SelectedValue + "'";
            //str = "SELECT Reference_No, Account_No, IFSC_Code, CONVERT(varchar(10), Created_Date, 103) as Transaction_Date, Debit_Amount, Party_Name FROM[tbl_Payment_Debit] WHERE District_Id ='" + Dist_id + "' AND Branch_Id ='" + Branch_Id + "'AND Bank_Type='" + ddlBankType.SelectedValue + "' ";
            str = "select DISTINCT PD.Reference_No, RPO.Ref_Bill_No, RPO.Account_No, RPO.IFSC_Code, CONVERT(varchar(10), PD.Created_Date, 103) as Transaction_Date, RPO.Net_Amount, RPO.Party_Name, RPO.Godown_Id, (select G.Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID = RPO.Godown_Id) as Godown_Name, B.Beneficiary_Id as Beneficiary_Id from[tbl_Payment_Debit] PD INNER JOIN[tbl_Payment_Credit] PC ON PC.Reference_No = PD.Reference_No INNER JOIN tbl_Digitally_Signed_Bill_RPO as RPO ON PC.Bill_No = RPO.Ref_Bill_No inner join tbl_Beneficiary_Account_Details as B on B.Account_No = RPO.Account_No WHERE PD.District_Id = '" + Dist_id + "' AND PD.Branch_Id = '" + Branch_Id + "' AND Bank_Type = '" + ddlBankType.SelectedValue + "'";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBOBillApp.DataSource = ds.Tables[0];
                gvBOBillApp.DataBind();
                trnewproc.Visible = true;
                lblNoofAC.Text = ds.Tables[0].Rows.Count.ToString();
                //pnlCofirmmsg.Visible = false;
                //Panel2.Visible = false;
                //btn_Sbi_Link.Visible = false;

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
    protected void ddlBankType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillsDetail();
    }
    protected void Edit(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            String Reference_No = (row.FindControl("hdnRefno") as HiddenField).Value;
            GeneratePaymentFile(Reference_No);
        }
    }
    private void GeneratePaymentFile(string Reference_No)
    {
        try
        {
            string Dist_id = ddlDistrict.SelectedValue.ToString();
            string Branch_Id = ddlDepotList.SelectedValue.ToString();
            string str = "";
            if (ddlBankType.SelectedValue == "S")
            {
                //str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "' union select [Account_No]+'#'+IFSC_Code+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,IFSC_Code from [tbl_Payment_Credit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "') as AA order by IFSC_Code";
                str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#' as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "' and Bank_Type='S' union select [Account_No]+'#'+SUBSTRING(IFSC_Code,7,6)+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description as Statements,IFSC_Code from [tbl_Payment_Credit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "') as AA order by IFSC_Code";

            }
            else if (ddlBankType.SelectedValue == "O")
            {
                //str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "' union select [Account_No]+'#'+IFSC_Code+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,IFSC_Code from [tbl_Payment_Credit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "') as AA order by IFSC_Code";
                str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "' and Bank_Type='O' union select [Account_No]+'#'+IFSC_Code+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,IFSC_Code from [tbl_Payment_Credit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "') as AA order by IFSC_Code";

            }
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                CreateDelimitedFileFromDt(ds.Tables[0], "#", Reference_No);
            }
            else
            {

            }

        }

        catch (Exception ex)
        {

        }
    }
    private string CreateDelimitedFileFromDt(DataTable dt, string delimiter, string Reference_No)
    {
        StringBuilder sb = new StringBuilder();
        foreach (DataRow row in dt.Rows)
        {

            string stuff = "";
            foreach (DataColumn col in row.Table.Columns)
            {
                string colvalue = Convert.ToString(row[col]);
                colvalue += delimiter;
                stuff += colvalue;
            }
            // get rid of delimiter after last column if any
            stuff = stuff.TrimEnd(delimiter.ToCharArray());
            // add line feed
            stuff += "\r\n";
            // append to sb
            sb.Append(stuff);

        }
        Response.Clear();
        Response.Buffer = true;
        string Bank_T = "";
        if (ddlBankType.SelectedValue == "S")
        {
            Bank_T = "SBI";
        }
        else if (ddlBankType.SelectedValue == "O")
        {
            Bank_T = "OTH";
        }
        string RefNo = Reference_No.ToString();
        string File_NAME = Bank_T + RefNo + ".txt";
        //Response.AddHeader("content-disposition", @"attachment;filename='" + File_NAME + "'");
        Response.AddHeader("content-disposition", "attachment;filename=" + File_NAME);
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(sb);
        Response.Flush();
        Response.End();
        //btn_Sbi_Link.Visible = true;

        return sb.ToString();

    }
    public void Delete(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {
                String hdnRefno = (row.FindControl("hdnRefno") as HiddenField).Value;
                con.Open();
                cmd = new SqlCommand("[dbo].[Delete_RO_EPF]", con, sqltrans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Reference_No", hdnRefno);
                cmd.Parameters.AddWithValue("@IPAddress", Request.UserHostAddress);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    count++;
                }
                con.Close();
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
