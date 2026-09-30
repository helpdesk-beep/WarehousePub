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
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.IO;

public partial class Accounting_DRAFT_FORMAT_REQUIRED_IN_MS_EXCEL_FORMAT_FOR_NAFED_Marketing_aspx : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDistrict();
            GetCommodity();
            GetCropYear();
        }
    }
    public void GetDistrict()
    {
        // string Dist_id = Session["Depot_DistID"].ToString();
        string qry = "";
        qry = "select distinct District_Id,District_Name from tbl_MetaData_DISTRICT Order By District_Name ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void GetCommodity()
    {
        //string Dist_id = Session["Depot_DistID"].ToString();
        string qry = "";
        qry = "select distinct Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in ('63', '64', '33', '52', '27', '92', '123', '31', '65','26','75') order by Commodity_Name asc";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, "--Select--");
            //ddlcommodity.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {

        }
    }
    public void GetCropYear()
    {
        //string Dist_id = Session["Depot_DistID"].ToString();
        string qry = "";
        qry = "Select Distinct Crop_Year from tbl_Storage_Bill_Details Order By Crop_Year ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcropyear.DataSource = ds.Tables[0];
            ddlcropyear.DataTextField = "Crop_Year";
            ddlcropyear.DataValueField = "Crop_Year";
            ddlcropyear.DataBind();
            ddlcropyear.Items.Insert(0, "--Select--");
            //ddlcommodity.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {

        }
    }
    public void GetBranch()
    {
        //string Dist_id = Session["Depot_DistID"].ToString();
        //string Branch_Id = Session["BranchId"].ToString();
        string qry = "";
        qry = "select distinct DepotID,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "DepotID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, "--All--");
            //ddlcommodity.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    public void fillgrid()
    {
        string Dist_id = ddldistrict.SelectedValue;
        string Branch_Id = ddlbranch.SelectedValue;
        //SqlConnection con_WLC1 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("NAFED_BHOPAL_DRAFT_FORMAT_REQUIRED_IN_MS_EXCEL_FORMAT_For_Marketing", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddldistrict.SelectedValue == "ALL")
            {
                cmd.Parameters.AddWithValue("@District_Id", 0);

            }
            else
            {
                cmd.Parameters.AddWithValue("@District_Id", Dist_id);
            }
            if (ddlbranch.SelectedValue == "ALL")
            {
                cmd.Parameters.AddWithValue("@Branch_Id", 0);


            }
            else
            {
                cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);

            }
            if (ddlFinancialyear.SelectedValue == "ALL")
            {
                cmd.Parameters.AddWithValue("@Financial_Year", 0);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Financial_Year", ddlFinancialyear.SelectedValue);

            }
            if (ddlmonth.SelectedValue == "ALL")
            {
                cmd.Parameters.AddWithValue("@Month", 0);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);

            }
            cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                GrdBills.DataSource = dt;
                GrdBills.DataBind();
                grdbill.Visible = true;
                GrdBills.FooterRow.Style.Add("text-align", "right");
                GrdBills.FooterRow.Cells[12].Text = "Total";
                GrdBills.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("STotalCharges")).ToString();
                GrdBills.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("FTotalCharges")).ToString();
                GrdBills.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ClosingBalance")).ToString();
                GrdBills.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BillAmount")).ToString();
                ddlFinancialyear.ClearSelection();
                ddlmonth.ClearSelection();
                ddlcommodity.ClearSelection();
                ddlcropyear.ClearSelection();
                ddldistrict.ClearSelection();
                ddlbranch.ClearSelection();
                //lblOfficerList.Text = Convert.ToString(dt.Rows.Count);
                //ViewState["Region"] = dt;
            }
            else
            {
                GrdBills.DataSource = null;
                GrdBills.DataBind();
                grdbill.Visible = true;
                ddlFinancialyear.ClearSelection();
                ddlmonth.ClearSelection();
                ddlcommodity.ClearSelection();
                ddlcropyear.ClearSelection();
                ddldistrict.ClearSelection();
                ddlbranch.ClearSelection();
                //lblOfficerList.Text = "0";
            }
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportGridViewToExcel(GrdBills);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //fillgrid();         
    }
    private void ExportGridViewToExcel(GridView GrdBills)
    {
        // Clear the response
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "DRAFT_FORMAT_NAFED.xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GrdBills.GridLines = GridLines.Both;
        GrdBills.HeaderStyle.Font.Bold = true;
        GrdBills.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    // Do nothing
    //}
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    protected void GrdBills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Print")
        //{
        //    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
        //    Session["Bill_Number"] = (row.RowIndex).ToString();
        //    Label Billnumber = (Label)row.FindControl("lblBill_Number");
        //    string url = "State_Nafed_Print_Generate_Bill.aspx?BN=" + Base64Encode(Billnumber.Text);
        //    string s = "window.open('" + url + "', 'popup_window', 'width=600,height=600,left=100,top=100,resizable=yes');";
        //    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        //}
    }
}