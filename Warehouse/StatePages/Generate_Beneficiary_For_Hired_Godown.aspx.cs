using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Generate_Beneficiary_For_Hired_Godown : System.Web.UI.Page
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

                //if (Session["Region_ID"].ToString() != null)
                //{
                //    region = Session["Region_ID"].ToString();

                //}
            }

            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            }
            else
            {
                if (Session["RoleId"].ToString() == "2")
                {
                    query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
                }
                else
                {
                    query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where District_Id ='" + Session["Depot_DistID"].ToString() + "' order by District_Name asc";

                }
            }
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
                ddlDistrict.Items.Insert(0, new ListItem("--Select--", "0"));


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
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

                //if (Session["Region_ID"].ToString() != null)
                //{
                //    region = Session["Region_ID"].ToString();

                //}
            }

            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "'";
            }
            else
            {
                query = "  SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "' and DepoTypeID='4'";

            }
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
                ddlbranch.Items.Insert(0, new ListItem("--Select--", "0"));
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
    private void fillParty()
    {
        try
        {
            cmd = new SqlCommand("dbo.Get_Party_Name_For_Generation_Hired_Godown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
            cmd.Parameters.AddWithValue("@Beneficiary_Type", ddlBankType.SelectedValue);

            //SqlDataAdapter da = new SqlDataAdapter(str, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPartyName.Items.Clear();
                ddlPartyName.DataSource = ds.Tables[0];
                ddlPartyName.DataTextField = "Party_Name";
                ddlPartyName.DataValueField = "Party_Id";
                ddlPartyName.DataBind();
                ddlPartyName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                ////
            }
        }
        catch (Exception ex)
        {
            //////
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillIssuecenter();
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillParty();
    }
    protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPartyName.SelectedIndex != 0)
        {
            GetBillsDetail();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Party Name')", true);
        }
    }
    public void GetBillsDetail()
    {
        SqlCommand cmd = new SqlCommand("Get_Beneficiary_Details_For_Hired_Godown", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Beneficiary_TypeID", ddlBankType.SelectedValue);
        cmd.Parameters.AddWithValue("@DistrictID", ddlDistrict.SelectedValue);
        cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
        cmd.Parameters.AddWithValue("@Beneficiary_Id", ddlPartyName.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            gvBOBillApp.DataSource = dt;
            gvBOBillApp.DataBind();
            trnewproc.Visible = true;
            tblbtn.Visible = true;
            lblNoofAC.Text = dt.Rows.Count.ToString();
            btnDownload.Visible = true;
            btnDownload.Enabled = true;
        }
        else
        {
            gvBOBillApp.DataSource = null;
            gvBOBillApp.DataBind();
            btnDownload.Enabled = false;
            trnewproc.Visible = false;
            btnDownload.Visible = false;
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data Found'); </script> ");
        }
    }
    //private void GetBillsDetail()
    //{
    //    try
    //    {

    //        string str = "";
    //        if (ddlPartyName.SelectedValue != "0")
    //        {
    //            if (ddlBankType.SelectedValue == "S")
    //            {
    //                //str = "select Ref_Bill_No,RPO.Account_No,RPO.IFSC_Code,CONVERT(varchar(10),GETDATE(),103) as Transaction_Date,Net_Amount,Party_Name,Godown_Id,(select G.Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=RPO.Godown_Id) as Godown_Name,B.Beneficiary_Id as Beneficiary_Id from tbl_Digitally_Signed_Bill_RPO as RPO inner join tbl_Beneficiary_Account_Details as B on B.Account_No=RPO.Account_No where Net_Amount>0 and B.Beneficiary_Type='S' and RPO.District_Id='" + Dist_id + "' and RPO.Branch_Id='" + Branch_Id + "' and RPO.Month_No='"+ ddlmonth.SelectedValue +"'";
    //                str = "select Beneficiary_Id,Beneficiary_Name,Account_No,SUBSTRING(BAD.IFSC_Code, 5, 7) IFSC_Code,Mobile from tbl_Beneficiary_Account_Details BAD inner join tbl_MetaData_DISTRICT dst	on BAD.District_Id = dst.District_Id INNER JOIN tbl_MetaData_DEPOT MD	ON MD.BranchId=BAD.Branch_Id INNER JOIN tbl_MetaData_GODOWN_2018 gdn ON gdn.BranchID=MD.BranchId where  (GO_Approval_Status='Y'or RO_Approval_Status='Y') and gdn.Hired_Type in('Hired') and Beneficiary_Type = 'S'  AND BAD.District_Id='" + ddlDistrict.SelectedValue + "' AND BAD.Branch_Id='" + ddlbranch.SelectedValue + "' AND BAD.Beneficiary_Id=" + ddlPartyName.SelectedValue + "";

    //            }
    //            else if (ddlBankType.SelectedValue == "O")
    //            {
    //                //str = "select Ref_Bill_No,RPO.Account_No,RPO.IFSC_Code,CONVERT(varchar(10),GETDATE(),103) as Transaction_Date,Net_Amount,Party_Name,Godown_Id,(select G.Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=RPO.Godown_Id) as Godown_Name,B.Beneficiary_Id as Beneficiary_Id from tbl_Digitally_Signed_Bill_RPO as RPO inner join tbl_Beneficiary_Account_Details as B on B.Account_No=RPO.Account_No where Net_Amount>0 and B.Beneficiary_Type='O' and RPO.District_Id='" + Dist_id + "' and RPO.Branch_Id='" + Branch_Id + "' and RPO.Month_No='" + ddlmonth.SelectedValue + "'";
    //               // str = "select Beneficiary_Id, Beneficiary_Name, Account_No, SUBSTRING(IFSC_Code, 5, 7) IFSC_Code, Mobile from tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICT on tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id where (GO_Approval_Status='Y'or RO_Approval_Status='Y') and Beneficiary_Type = 'O'  AND tbl_Beneficiary_Account_Details.District_Id='" + ddlDistrict.SelectedValue + "'AND tbl_Beneficiary_Account_Details.Branch_Id='" + ddlbranch.SelectedValue + "' AND Beneficiary_Id=" + ddlPartyName.SelectedValue + "";
    //                str = "select Beneficiary_Id,Beneficiary_Name,Account_No,SUBSTRING(BAD.IFSC_Code, 5, 7) IFSC_Code,Mobile from tbl_Beneficiary_Account_Details BAD inner join tbl_MetaData_DISTRICT dst	on BAD.District_Id = dst.District_Id INNER JOIN tbl_MetaData_DEPOT MD	ON MD.BranchId=BAD.Branch_Id INNER JOIN tbl_MetaData_GODOWN_2018 gdn ON gdn.BranchID=MD.BranchId where  (GO_Approval_Status='Y'or RO_Approval_Status='Y') and gdn.Hired_Type in('Hired') and Beneficiary_Type = 'O'  AND BAD.District_Id='" + ddlDistrict.SelectedValue + "' AND BAD.Branch_Id='" + ddlbranch.SelectedValue + "' AND BAD.Beneficiary_Id=" + ddlPartyName.SelectedValue + "";

    //            }
    //        }
    //        else if (ddlBankType.SelectedValue == "S")
    //        {
    //            //str = "select Ref_Bill_No,RPO.Account_No,RPO.IFSC_Code,CONVERT(varchar(10),GETDATE(),103) as Transaction_Date,Net_Amount,Party_Name,Godown_Id,(select G.Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=RPO.Godown_Id) as Godown_Name,B.Beneficiary_Id as Beneficiary_Id from tbl_Digitally_Signed_Bill_RPO as RPO inner join tbl_Beneficiary_Account_Details as B on B.Account_No=RPO.Account_No where Net_Amount>0 and B.Beneficiary_Type='S' and RPO.District_Id='" + Dist_id + "' and RPO.Branch_Id='" + Branch_Id + "' and RPO.Month_No='"+ ddlmonth.SelectedValue +"'";
    //              str = "select Beneficiary_Id,Beneficiary_Name,Account_No,SUBSTRING(BAD.IFSC_Code, 5, 7) IFSC_Code,Mobile from tbl_Beneficiary_Account_Details BAD inner join tbl_MetaData_DISTRICT dst	on BAD.District_Id = dst.District_Id INNER JOIN tbl_MetaData_DEPOT MD	ON MD.BranchId=BAD.Branch_Id INNER JOIN tbl_MetaData_GODOWN_2018 gdn ON gdn.BranchID=MD.BranchId where  (GO_Approval_Status='Y'or RO_Approval_Status='Y') and gdn.Hired_Type in('Hired') and Beneficiary_Type = 'S'  AND BAD.District_Id='" + ddlDistrict.SelectedValue + "' AND BAD.Branch_Id='" + ddlbranch.SelectedValue + "' AND BAD.Beneficiary_Id=" + ddlPartyName.SelectedValue + "";
    //            //str = "select Beneficiary_Id, Beneficiary_Name, Account_No, SUBSTRING(IFSC_Code, 5, 7) IFSC_Code, Mobile from tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICT on tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id where  (GO_Approval_Status='Y'or RO_Approval_Status='Y') and Beneficiary_Type = 'S'  AND tbl_Beneficiary_Account_Details.District_Id='" + ddlDistrict.SelectedValue + "'AND tbl_Beneficiary_Account_Details.Branch_Id='" + ddlbranch.SelectedValue + "'";

    //        }
    //        else if (ddlBankType.SelectedValue == "O")
    //        {
    //            //str = "select Ref_Bill_No,RPO.Account_No,RPO.IFSC_Code,CONVERT(varchar(10),GETDATE(),103) as Transaction_Date,Net_Amount,Party_Name,Godown_Id,(select G.Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=RPO.Godown_Id) as Godown_Name,B.Beneficiary_Id as Beneficiary_Id from tbl_Digitally_Signed_Bill_RPO as RPO inner join tbl_Beneficiary_Account_Details as B on B.Account_No=RPO.Account_No where Net_Amount>0 and B.Beneficiary_Type='O' and RPO.District_Id='" + Dist_id + "' and RPO.Branch_Id='" + Branch_Id + "' and RPO.Month_No='" + ddlmonth.SelectedValue + "'";
    //           // str = "select Beneficiary_Id, Beneficiary_Name, Account_No, SUBSTRING(IFSC_Code, 5, 7) IFSC_Code, Mobile from tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICT on tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id where (GO_Approval_Status='Y'or RO_Approval_Status='Y') and Beneficiary_Type = 'O'  AND tbl_Beneficiary_Account_Details.District_Id='" + ddlDistrict.SelectedValue + "'AND tbl_Beneficiary_Account_Details.Branch_Id='" + ddlbranch.SelectedValue + "'";
    //            str = "select Beneficiary_Id,Beneficiary_Name,Account_No,SUBSTRING(BAD.IFSC_Code, 5, 7) IFSC_Code,Mobile from tbl_Beneficiary_Account_Details BAD inner join tbl_MetaData_DISTRICT dst	on BAD.District_Id = dst.District_Id INNER JOIN tbl_MetaData_DEPOT MD	ON MD.BranchId=BAD.Branch_Id INNER JOIN tbl_MetaData_GODOWN_2018 gdn ON gdn.BranchID=MD.BranchId where  (GO_Approval_Status='Y'or RO_Approval_Status='Y') and gdn.Hired_Type in('Hired') and Beneficiary_Type = 'O'  AND BAD.District_Id='" + ddlDistrict.SelectedValue + "' AND BAD.Branch_Id='" + ddlbranch.SelectedValue + "' AND BAD.Beneficiary_Id=" + ddlPartyName.SelectedValue + "";

    //        }

    //        SqlDataAdapter da = new SqlDataAdapter(str, con);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            gvBOBillApp.DataSource = ds.Tables[0];
    //            gvBOBillApp.DataBind();
    //            trnewproc.Visible = true;
    //            tblbtn.Visible = true;
    //            lblNoofAC.Text = ds.Tables[0].Rows.Count.ToString();
    //            btnDownload.Visible = true;
    //            btnDownload.Enabled = true;
    //        }
    //        else
    //        {
    //            gvBOBillApp.DataSource = null;
    //            gvBOBillApp.DataBind();
    //            btnDownload.Enabled = false;
    //            trnewproc.Visible = false;
    //            btnDownload.Visible = false;
    //            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data Found'); </script> ");
    //        }

    //    }

    //    catch (Exception ex)
    //    {

    //    }
    //}

    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillsDetail();
        lblRespMsg.Text = "";
        lblRespMsgNo.Text = "";
        lblRespMsg.Visible = false;
        lblRespMsgNo.Visible = false;
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

    private string CreateDelimitedFileFromDt(DataTable dt, string delimiter)
    {
        StringBuilder sb = new StringBuilder();
        btn_Sbi_Link.Visible = true;
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
        //string RefNo = lblRespMsgNo.Text.ToString();
        string File_NAME = Bank_T + ".txt";
        //Response.AddHeader("content-disposition", @"attachment;filename='" + File_NAME + "'");
        Response.AddHeader("content-disposition", "attachment;filename=" + File_NAME);
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(sb);
        Response.Flush();
        Response.End();

        return sb.ToString();

    }

    private void GenerateBeneficiaryFile()
    {
        try
        {
            string str = "";
            if (ddlPartyName.SelectedValue != "0")
            {
                if (ddlBankType.SelectedValue == "S")
                {
                    str = "select Beneficiary_Name + '#' + Account_No + '#' + IFSC_Code + '#' + Mobile + '#' from tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICT on tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id where  RO_Approval_Status = 'Y' and Beneficiary_Type = 'S' AND Beneficiary_Id=" + ddlPartyName.SelectedValue + " ";
                }
                else if (ddlBankType.SelectedValue == "O")
                {
                    str = "select Beneficiary_Name + '#' + Account_No + '#' + IFSC_Code + '#' + Mobile + '#' from tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICT on tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id where RO_Approval_Status = 'Y' and Beneficiary_Type = 'O' AND Beneficiary_Id=" + ddlPartyName.SelectedValue + "";
                }
            }
            else if (ddlBankType.SelectedValue == "S")
            {
                //str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + lblRespMsgNo.Text + "' union select [Account_No]+'#'+IFSC_Code+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,IFSC_Code from [tbl_Payment_Credit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + lblRespMsgNo.Text + "') as AA order by IFSC_Code";
                //str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#' as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + lblRespMsgNo.Text + "' and Bank_Type='S' union select [Account_No]+'#'+SUBSTRING(IFSC_Code,7,6)+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description as Statements,IFSC_Code from [tbl_Payment_Credit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + lblRespMsgNo.Text + "') as AA order by IFSC_Code";
                str = "select Beneficiary_Name + '#' + Account_No + '#' + IFSC_Code + '#' + Mobile + '#' from tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICT on tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id where  RO_Approval_Status = 'Y' and Beneficiary_Type = 'S' AND tbl_Beneficiary_Account_Details.District_Id='" + ddlDistrict.SelectedValue + "'AND tbl_Beneficiary_Account_Details.Branch_Id='" + ddlbranch.SelectedValue + "'";

            }
            else if (ddlBankType.SelectedValue == "O")
            {
                //str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + lblRespMsgNo.Text + "' union select [Account_No]+'#'+IFSC_Code+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,IFSC_Code from [tbl_Payment_Credit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + lblRespMsgNo.Text + "') as AA order by IFSC_Code";
                //str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + lblRespMsgNo.Text + "' and Bank_Type='O' union select [Account_No]+'#'+IFSC_Code+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,IFSC_Code from [tbl_Payment_Credit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + lblRespMsgNo.Text + "') as AA order by IFSC_Code";
                str = "select Beneficiary_Name + '#' + Account_No + '#' + IFSC_Code + '#' + Mobile + '#' from tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICT on tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id where RO_Approval_Status = 'Y' and Beneficiary_Type = 'O' AND tbl_Beneficiary_Account_Details.District_Id='" + ddlDistrict.SelectedValue + "'AND tbl_Beneficiary_Account_Details.Branch_Id='" + ddlbranch.SelectedValue + "'";
            }
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                CreateDelimitedFileFromDt(ds.Tables[0], "#");
            }
            else
            {

            }

        }

        catch (Exception ex)
        {

        }
    }


    protected void btn_Sbi_Link_Click(object sender, EventArgs e)
    {
        //Response.Redirect("https://yonobusiness.sbi/login/yonobusinesslogin");
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        //lblRespMsg.Text = "Your Payment File has been Generated with Reference Number : " + Ref_Number;
        //lblRespMsg.Visible = true;
        GenerateBeneficiaryFile();
        //btn_Sbi_Link.Visible = true;
        //btnDownload.Visible = true;
        //btnDownload.Enabled = true;
        //btn_save.Visible = false;
        Update_Downlod_Detail();

        btn_Sbi_Link.Visible = true;
    }
    public void Update_Downlod_Detail()
    {
        //string RegionID = Session["Region_ID"].ToString();
        string Created_By = Request.ServerVariables["REMOTE_ADDR"].ToString();
        //qry = "INSERT INTO [dbo].[tbl_Payment_Credit] ([Trans_Id] ,[Region_Id] ,[District_Id] ,[Branch_Id] ,[Account_No] ,[IFSC_Code] ,[Branch_Code] ,[Party_Name] ,[Date] ,[Credit_Amount] ,[Trans_Description] ,[Payment_Identifier] ,[Reference_No] ,[Mobile_No] ,[Account_Type] ,[Amount_Type] ,[Created_By] ,[Created_Date] ,[Is_Download] ,[Download_By] ,[Download_Date] ,[Status] ,[Beneficiary_Id] ,[Bill_No] ,[Godown_Id] ,[Aid]) VALUES ('" + Trans_Id + "' ,'" + RegionID + "' ,'" + Dist_id + "' ,'" + BranchID + "' ,'" + Account_No + "','" + IFSC_Code + "' ,'" + IFSC_Code + "' ,'" + Party_Name + "',GetDate() ,'" + Credit_Amount + "'  ,'" + Trans_Description + "' ,'" + Payment_Identifier + "' ,'" + Ref_Number + "'  ,'" + Mobile_No + "'  ,'" + Account_Type + "'  ,'" + Amount_Type + "'  ,'" + Created_By + "' ,Getdate() ,'','' ,'' ,'' ,'" + Beneficiary_Id + "' ,'" + Rent_Bill_No + "','" + Godown_ID + "' ,'" + AID + "')";
        //qry = "update tbl_Payment_Debit set Is_Download='Y',Download_By='" + Created_By + "',Download_Date=GETDATE() where Reference_No='" + lblRespMsgNo.Text + "' and Branch_Id='" + BranchID + "'";
        qry = "UPDATE tbl_Beneficiary_Account_Details SET Flag='Y' WHERE Beneficiary_Type='" + ddlBankType.SelectedValue + "'";

        SqlCommand cmd2 = new SqlCommand(qry, con);
        con.Open();
        int i = cmd2.ExecuteNonQuery();
        con.Close();
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounting/Generate_Beneficiary.aspx");
    }

    protected void ddlBankType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillParty();
       // GetBillsDetail();
        lblRespMsg.Text = "";
        lblRespMsgNo.Text = "";
        lblRespMsg.Visible = false;
        lblRespMsgNo.Visible = false;
        //btn_Sbi_Link.Visible = false;
        // btnDownload.Visible = false;
        // btnDownload.Enabled = false;
        // btn_save.Visible = true;
    }
    //protected void fillMonth()
    //{
    //    //ddlmonth.ClearSelection();
    //    ddlmonth.Items.Clear();
    //    ddlmonth.Items.Add(new ListItem("--Select--", "0"));
    //    ddlmonth.Items.Add(new ListItem("January", "1"));
    //    ddlmonth.Items.Add(new ListItem("February", "2"));
    //    ddlmonth.Items.Add(new ListItem("March", "3"));
    //    ddlmonth.Items.Add(new ListItem("April", "4"));
    //    ddlmonth.Items.Add(new ListItem("May", "5"));
    //    ddlmonth.Items.Add(new ListItem("June", "6"));
    //    ddlmonth.Items.Add(new ListItem("July", "7"));
    //    ddlmonth.Items.Add(new ListItem("August", "8"));
    //    ddlmonth.Items.Add(new ListItem("September", "9"));
    //    ddlmonth.Items.Add(new ListItem("October", "10"));
    //    ddlmonth.Items.Add(new ListItem("November", "11"));
    //    ddlmonth.Items.Add(new ListItem("December", "12"));
    //    ddlmonth.SelectedIndex = 0;
    //}
}