using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_MPWLC_Download_RO_EPF_Date_Wise : System.Web.UI.Page
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
           
        }
    }
    private void GetBillsDetail()
    {
        try
        {
            string str = "";
            cmd = new SqlCommand("[dbo].[Get_Generated_RO_Date_Wise]", con, sqltrans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TransactuionDate", txtdate.Text);
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
    public String IsExists(String Reference_No)
    {
        con.Open();
        cmd = new SqlCommand("[dbo].[Get_Payment_Exists]", con, sqltrans);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Reference_No", Reference_No);
        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
        con.Close();
        return TheResult;
    }
    public void Delete(object sender, EventArgs e)
    {
        int count = 0;
        string TheResult = "";
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            String hdnRefno = (row.FindControl("hdnRefno") as HiddenField).Value;
            TheResult = IsExists(hdnRefno);
            if (TheResult.StartsWith("DEL"))
            {
                con.Open();
                cmd = new SqlCommand("[dbo].[Delete_RO_EPF]", con, sqltrans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Reference_No", hdnRefno);
                cmd.Parameters.AddWithValue("@IPAddress", Request.UserHostAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    count++;
                }
                con.Close();
            }
            else
            {

            }
        }
        if (TheResult.StartsWith("DEL"))
        {
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
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('इसका भुगतान हो चुका है, अतः इस फाइल को डिलीट नहीं कर सकते')", true);
        }
        //try
        //{

        //}
        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message() + "')", true);
        //}
        //finally
        //{
        //    con.Close();
        //}
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetBillsDetail();
    }
}