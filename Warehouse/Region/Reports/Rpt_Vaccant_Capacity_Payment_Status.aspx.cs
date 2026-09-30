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

public partial class Region_Reports_Rpt_Vaccant_Capacity_Payment_Status : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Region_Logid"] != null))
            {
                fillgrid(Session["Region_Logid"].ToString());
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }
    }
    protected void fillgrid(string RID)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("rpt_get_Vaccant_Capacity_Bill_Payment_Status", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", RID);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            grdbill.DataSource = dt;
                            grdbill.DataBind();
                            //ViewState["dt"] = dt;
                            grdbill.FooterRow.Style.Add("text-align", "right");
                            grdbill.FooterRow.Cells[6].Text = "Total";
                            grdbill.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount")).ToString();
                            grdbill.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TDS_Amount")).ToString();
                            grdbill.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Other_Deduction")).ToString();
                            grdbill.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Credit_Amount")).ToString();
                            
                        }
                        else
                        {
                            grdbill.DataSource = dt;
                            grdbill.DataBind();
                            // divshowdetails.Visible = false;
                        }
                    }
                }
            }
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
                str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#' as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit_For_Vaccant] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "' and Bank_Type='S' union select [Account_No]+'#'+SUBSTRING(IFSC_Code,7,6)+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description as Statements,IFSC_Code from [tbl_Payment_Credit_Vaccant_Capacity] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "') as AA order by IFSC_Code";

            }
            else if (Bank_Type == "O")
            {
                //str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "' union select [Account_No]+'#'+IFSC_Code+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,IFSC_Code from [tbl_Payment_Credit] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "') as AA order by IFSC_Code";
                str = "select Statements from (select Account_No+'#'+'30343'+'#'+convert(varchar(10),Date,103)+'#'+CONVERT(varchar(10),Debit_Amount)+'#'+''+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,'30343' as IFSC_Code from [tbl_Payment_Debit_For_Vaccant] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "' and Bank_Type='O' union select [Account_No]+'#'+IFSC_Code+'#'+convert(varchar(10),Date,103)+'#'+''+'#'+CONVERT(varchar(10),Credit_Amount)+'#'+Reference_No+'#'+Trans_Description+'#'+Payment_Identifier as Statements,IFSC_Code from [tbl_Payment_Credit_Vaccant_Capacity] where Branch_Id='" + Branch_Id + "' and District_Id='" + Dist_id + "' and Reference_No='" + Reference_No + "') as AA order by IFSC_Code";

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
    protected void grdbill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string status = DataBinder.Eval(e.Row.DataItem, "Payment_Status").ToString();

            if (status == "Pending")
            {
                e.Row.BackColor = System.Drawing.Color.MistyRose;   // हल्का लाल
            }
            else if (status == "Completed")
            {
                e.Row.BackColor = System.Drawing.Color.Honeydew;   // हल्का हरा
            }
        }
    }
}