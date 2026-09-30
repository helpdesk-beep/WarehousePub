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

public partial class Reports_Region_Download_RO_EPF : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlTransaction sqltrans;
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    string Bill_Type = "";
    string Ref_Number = "";
    string Ref_Aid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetRegionfordist();
            fillMonth();
            fillDistrict();
            GetBillsDetail(); 

        }
    }
    private void GetRegionfordist()
    {
        try
        {
            string qrySelect = "SELECT * FROM tbl_MetaData_Region where Region_ID='" + Session["Region_Logid"].ToString() + "' order by region";
            SqlDataAdapter da = new SqlDataAdapter(qrySelect, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlregion.DataSource = ds.Tables[0];
                ddlregion.DataTextField = "region";
                ddlregion.DataValueField = "Region_Id";
                ddlregion.DataBind();
                ddlregion.Enabled = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    //protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillDistrict();
    //    GetBillsDetail();
    //}
    private void fillDistrict()
    {
        try
        {
            string region = "";
            //if (Session["UserName"].ToString() != "MPSWLC")
            //{

            //    if (Session["Region_ID"].ToString() != null)
            //    {
            //        region = Session["Region_ID"].ToString();

            //    }
            //}
            string query = "";
            //if (Session["UserName"].ToString() == "MPSWLC")
            //{
            //    query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            //}
            //else
            //{
            //    query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
            //}
            query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + Session["Region_Logid"].ToString() + "' order by District_Name asc";
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
    protected void fillMonth()
    {
        //ddlmonth.ClearSelection();
        ddlmonth.Items.Clear();
        ddlmonth.Items.Add(new ListItem("--Select--", "0"));
        ddlmonth.Items.Add(new ListItem("January", "1"));
        ddlmonth.Items.Add(new ListItem("February", "2"));
        ddlmonth.Items.Add(new ListItem("March", "3"));
        ddlmonth.Items.Add(new ListItem("April", "4"));
        ddlmonth.Items.Add(new ListItem("May", "5"));
        ddlmonth.Items.Add(new ListItem("June", "6"));
        ddlmonth.Items.Add(new ListItem("July", "7"));
        ddlmonth.Items.Add(new ListItem("August", "8"));
        ddlmonth.Items.Add(new ListItem("September", "9"));
        ddlmonth.Items.Add(new ListItem("October", "10"));
        ddlmonth.Items.Add(new ListItem("November", "11"));
        ddlmonth.Items.Add(new ListItem("December", "12"));
        ddlmonth.SelectedIndex = 0;
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
            GetBillsDetail();
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

            //string Dist_id = ddlDistrict.SelectedValue.ToString();
            //string Branch_Id = ddlDepotList.SelectedValue.ToString();
            string Month_No = "0";
            string RegionID = "0";
            string District_Id = "0";
            string Branch_Id = "0";
            string str = "";
            if (ddlmonth.SelectedValue != "0")
            {
                Month_No = ddlmonth.SelectedValue;
            }
            if (ddlregion.SelectedValue != "---Select---")
            {
                RegionID = ddlregion.SelectedValue;
            }
            if (ddlDistrict.SelectedValue != "---Select---")
            {
                District_Id = ddlDistrict.SelectedValue;
            }
            if (ddlDepotList.SelectedValue != "--Select--")
            {
                Branch_Id = ddlDepotList.SelectedValue;
            }


            //str = "select Ref_Bill_No,RPO.Account_No,RPO.IFSC_Code,CONVERT(varchar(10),GETDATE(),103) as Transaction_Date,Net_Amount,Party_Name,Godown_Id,(select G.Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=RPO.Godown_Id) as Godown_Name,B.Beneficiary_Id as Beneficiary_Id from tbl_Digitally_Signed_Bill_RPO as RPO inner join tbl_Beneficiary_Account_Details as B on B.Account_No=RPO.Account_No where Net_Amount>0 and B.Beneficiary_Type='O' and RPO.District_Id='" + Dist_id + "' and RPO.Branch_Id='" + Branch_Id + "' and RPO.Month_No='" + ddlmonth.SelectedValue + "'";
            //str = "select RPO.Ref_Bill_No,RPO.Account_No,RPO.IFSC_Code,CONVERT(varchar(10),GETDATE(),103) as Transaction_Date,RPO.Net_Amount,Party_Name,RPO.Godown_Id,(select G.Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=RPO.Godown_Id) as Godown_Name,B.Beneficiary_Id as Beneficiary_Id from tbl_Digitally_Signed_Bill_RPO as RPO inner join tbl_Beneficiary_Account_Details as B on B.Account_No=RPO.Account_No inner join tbl_GdwnRentBill_Detuction_RM as RMD on RMD.JVS_Bill_Number=RPO.Ref_Bill_No inner join mpscsc.dbo.StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020 as CR on CR.Bill_Number=RMD.Ref_Bill_Number where RPO.Net_Amount>0 and B.Beneficiary_Type='O' and RPO.District_Id='" + Dist_id + "' and RPO.Branch_Id='" + Branch_Id + "' and RPO.Month_No='" + ddlmonth.SelectedValue + "' and RPO.Ref_Bill_No not in (select C.Bill_No as a from tbl_Payment_Credit as C where C.Branch_Id='" + Branch_Id + "')";

            cmd = new SqlCommand("[dbo].[Get_Generated_RO]", con, sqltrans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Month_No", Month_No);
            cmd.Parameters.AddWithValue("@RegionID", RegionID);
            cmd.Parameters.AddWithValue("@District_Id", District_Id);
            cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);

            SqlDataAdapter da = new SqlDataAdapter(str, con);
            da = new SqlDataAdapter(cmd);
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
    protected void Edit(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            String Reference_No = (row.FindControl("hdnRefno") as HiddenField).Value;
            String hdnBank_Type = (row.FindControl("hdnBank_Type") as HiddenField).Value;
            String hdnDistrict_Id = (row.FindControl("hdnDistrict_Id") as HiddenField).Value;
            String hdnBranch_Id = (row.FindControl("hdnBranch_Id") as HiddenField).Value;
            GeneratePaymentFile(Reference_No, hdnBank_Type, hdnDistrict_Id, hdnBranch_Id);
        }
    }
    private void GeneratePaymentFile(string Reference_No, string Bank_Type, string Dist_id, string Branch_Id)
    {
        try
        {
            //string Dist_id = ddlDistrict.SelectedValue.ToString();
            //string Branch_Id = ddlDepotList.SelectedValue.ToString();
            string str = "";
            if (Bank_Type == "S")
            {
                //str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "' union select [Account_No]+'#'+IFSC_Code+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,IFSC_Code from [tbl_Payment_Credit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "') as AA order by IFSC_Code";
                str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#' as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "' and Bank_Type='S' union select [Account_No]+'#'+SUBSTRING(IFSC_Code,7,6)+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description as Statements,IFSC_Code from [tbl_Payment_Credit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "') as AA order by IFSC_Code";

            }
            else if (Bank_Type == "O")
            {
                //str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "' union select [Account_No]+'#'+IFSC_Code+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,IFSC_Code from [tbl_Payment_Credit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "') as AA order by IFSC_Code";
                str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "' and Bank_Type='O' union select [Account_No]+'#'+IFSC_Code+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,IFSC_Code from [tbl_Payment_Credit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "') as AA order by IFSC_Code";

            }
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                CreateDelimitedFileFromDt(ds.Tables[0], "#", Reference_No, Bank_Type);
            }
            else
            {

            }

        }

        catch (Exception ex)
        {

        }
    }
    private string CreateDelimitedFileFromDt(DataTable dt, string delimiter, string Reference_No, string Bank_Type)
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
        if (Bank_Type == "S")
        {
            Bank_T = "SBI";
        }
        else if (Bank_Type == "O")
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
    //public void Delete(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int count = 0;
    //        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
    //        {
    //            String hdnRefno = (row.FindControl("hdnRefno") as HiddenField).Value;
    //            con.Open();
    //            cmd = new SqlCommand("[dbo].[Delete_RO_EPF]", con, sqltrans);
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@Reference_No", hdnRefno);
    //            cmd.Parameters.AddWithValue("@IPAddress", Request.UserHostAddress);
    //            int res = cmd.ExecuteNonQuery();
    //            if (res > 0)
    //            {
    //                count++;
    //            }
    //            con.Close();
    //        }
    //        if (count > 0)
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record deleted successfully..')", true);
    //            GetBillsDetail();
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record NOT deleted')", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
    //    }
    //    finally
    //    {
    //        con.Close();
    //    }
    //}

    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillsDetail();
    }

    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillsDetail();
    }
}