using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Reports_State_Rpt_Godown_Type_Wise_Payment_Status_For_All : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //fillRegion();
            //fillMonth();
            //fillgrid();
            fillDistrict();
            //fillgrid();
            fillGodownType();
        }
    }
    private void fillDistrict()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select District_Name, District_Id from tbl_MetaData_DISTRICT order by District_Name";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldistrict.Items.Clear();
                ddldistrict.DataSource = ds.Tables[0];
                ddldistrict.DataTextField = "District_Name";
                ddldistrict.DataValueField = "District_Id";
                ddldistrict.DataBind();
                ddldistrict.Items.Insert(0, "--Select--");
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
    private void fillBranch()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select DepotName, BranchId from tbl_MetaData_DEPOT where DistrictId=" + ddldistrict.SelectedValue + "";
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
    private void fillGodownType()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
            cmd = new SqlCommand("Get_GodownType", con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodowntype.Items.Clear();
                ddlGodowntype.DataSource = ds.Tables[0];
                ddlGodowntype.DataTextField = "Hired_Type";
                ddlGodowntype.DataValueField = "Hired_Type";
                ddlGodowntype.DataBind();
                ddlGodowntype.Items.Insert(0, "--Select--");
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

    public void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Payment_Summary_For_All", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddldistrict.SelectedValue == "--Select--")
            {
                cmd.Parameters.AddWithValue("@DistrictID", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DistrictID", ddldistrict.SelectedValue);
            }
            if (ddlbranch.SelectedValue == "--Select--")
            {
                cmd.Parameters.AddWithValue("@BranchID", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
            }
            if (ddlGodowntype.SelectedValue == "--Select--")
            {
                cmd.Parameters.AddWithValue("@GodownType", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@GodownType", ddlGodowntype.SelectedValue);
            }
            //cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            //cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                //GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "Godown Wise Payment Status" + "</b> ";
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Visible = true;
                GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                GridView1.FooterRow.Cells[1].Text = "Total";
                GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("TotalStorageBill")).ToString();
                GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Amount")).ToString();
                GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("TotalSubmittedBill")).ToString();
                GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("SubmittedBillAmount")).ToString();
                GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("PendingForSubmission")).ToString();
                GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("PendingForSubmissionAmount")).ToString();

                GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("TotalBillReceivedPayment")).ToString();
                GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Gross_Amount")).ToString();
                GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("TDS_Amt")).ToString();
                GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("OtherDeduction")).ToString();
                GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Payable_Amount")).ToString();
                GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("NoofBillPendingatMPSCSC")).ToString();
                GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("NoofBillAmountPendingatMPSCSC")).ToString();

                GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("TotalRentBill")).ToString();
                GridView1.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("RentBillAmt")).ToString();

                GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("ReceivedRentBill")).ToString();
                GridView1.FooterRow.Cells[18].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("ReceivedRentBillAmount")).ToString();

                //GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Total")).ToString();
                GridView1.FooterRow.Cells[19].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("PayBilltoGodownOwner")).ToString();
                GridView1.FooterRow.Cells[20].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("PaytoGodownOwner")).ToString();

                GridView1.FooterRow.Cells[21].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("PendingBillatMPWLC")).ToString();
                GridView1.FooterRow.Cells[22].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("PendingBillAmountatMPWLC")).ToString();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
    }
    //protected void OnDataBound(object sender, EventArgs e)
    //{
    //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
    //    TableHeaderCell cell = new TableHeaderCell();
    //    //cell.Text = "";
    //    cell.ColumnSpan = 2;
    //    row.Controls.Add(cell);

    //    cell = new TableHeaderCell();
    //    cell.ColumnSpan = 5;
    //    cell.Text = "Storage Payment Details";
    //    row.Controls.Add(cell);

    //    cell = new TableHeaderCell();
    //    cell.ColumnSpan = 3;
    //    cell.Text = "Rent Payment Details";
    //    row.Controls.Add(cell);

    //    cell = new TableHeaderCell();
    //    cell.ColumnSpan = 1;
    //    //cell.Text = ;
    //    row.Controls.Add(cell);

    //    row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
    //    GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    //}
   
   protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

    }

    //protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgrid();
    //    fillBranch();
    //   // fillFinancialYear();
    //}

    //protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgrid();
    //   // fillFinancialYear();
    //}

    //protected void ddlfy_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgrid();
    //    fillMonth();
    //}

    //protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgrid();
    //}

    protected void btnback_Click(object sender, EventArgs e)
    {

    }

    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void ddlGodowntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranch();
        fillgrid();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "View")
        //{
        //    // Retrieve the ID from the CommandArgument
        //    string Godownid = Convert.ToString(e.CommandArgument);
        //    Session["GodownID"] = Godownid.ToString();
        //   // Response.Redirect("~/Reports/States/Rpt_Godown_Bill_Wise_Payment_Status.aspx");
        //    Response.Redirect("window.open('~/Reports/States/Rpt_Godown_Bill_Wise_Payment_Status.aspx','_blank')");
        //    // Logic to handle the view action
        //    // For example, redirect to a detail page or show a modal
        //    //lblMessage.Text = "You clicked View for Item ID";
        //}
        if (e.CommandName == "View")
        {
            GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            //Session["GodownID"] = (row.RowIndex).ToString();
            string Godownid = Convert.ToString(e.CommandArgument);
            Session["GodownID"] = Godownid.ToString();
           // Label Billnumber = (Label)row.FindControl("lblBill_Number");
            string url = "Rpt_Godown_Bill_Wise_Payment_Status.aspx?BN=" + (Session["GodownID"].ToString());
            string s = "window.open('" + url + "', 'popup_window', 'width=1000,height=600,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }
}