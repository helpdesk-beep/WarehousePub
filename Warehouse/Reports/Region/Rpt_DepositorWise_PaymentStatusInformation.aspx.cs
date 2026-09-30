using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Resources;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;
using System.Net;
using System.Net.Sockets;

public partial class Region_Rpt_DepositorWise_PaymentStatusInformation : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {

            if (!IsPostBack)
            {
                fillDepositorType();
                fillDistrict();
                //fillDetailsInGrid();
                
            }

    }
    private void fillDepositorType()
    {
        try
        {
            string query = "";
            query = "select Depositor_ID, Depositor_Name FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679','181','15478') order by Depositor_Name ";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepositor.Items.Clear();
                ddlDepositor.DataSource = ds.Tables[0];
                ddlDepositor.DataTextField = "Depositor_Name";
                ddlDepositor.DataValueField = "Depositor_ID";
                ddlDepositor.DataBind();
                ddlDepositor.Items.Insert(0, new ListItem("--Select--", "0"));
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
    
    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        String ipaddress = string.Empty;
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipaddress = ip.ToString();
            }
        }
        return ipaddress.Length > 0 ? ipaddress : null;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Page.Validate("a");
        string ErrorMsg = "";
        lblMsg.Text = "";
        string RegionID = Session["Region_ID"].ToString();
        DataSet ds = new DataSet();
        if (ErrorMsg == "")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("usp_DepositorWisePaymentEntry", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@RegionID", RegionID);
            cmd.Parameters.AddWithValue("@FinancialYear", ddlFinancialYear.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@DepositorID", ddlDepositor.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@DistrictID", ddlDistrict.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@BranchID", ddlDepotList.SelectedValue.ToString());
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(ds);
            DataTable MainTable = ds.Tables[0];
            if (MainTable.Rows.Count > 0)
            {
                GV_EntryDone.DataSource = MainTable;
                GV_EntryDone.DataBind();
                GV_EntryDone.FooterRow.Cells[4].Text = "Total";
                GV_EntryDone.FooterRow.Cells[5].Text = MainTable.AsEnumerable().Sum(row => row.Field<Int32>("TotalBillPresentedinFY")).ToString();
                GV_EntryDone.FooterRow.Cells[6].Text = MainTable.AsEnumerable().Sum(row => row.Field<Decimal>("TotalBillAmountPresented")).ToString();
                GV_EntryDone.FooterRow.Cells[7].Text = MainTable.AsEnumerable().Sum(row => row.Field<Decimal>("TotalAmountReceivedInFY")).ToString();
                GV_EntryDone.FooterRow.Cells[8].Text = MainTable.AsEnumerable().Sum(row => row.Field<Decimal>("RemainingAmountFromDepositor")).ToString();
                
            }
            else
            {
                GV_EntryDone.DataSource = null;
                GV_EntryDone.DataBind();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alertMessage", "alert('" + ErrorMsg.ToString() + "')", true);
        }
    }

    protected void fillDetailsInGrid()
    {
        string RegionID = Session["Region_ID"].ToString();
        DataSet ds = new DataSet(); 
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("usp_DepositorWisePaymentEntry", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        cmd.Parameters.AddWithValue("@RegionID", RegionID);
        cmd.Parameters.AddWithValue("@FinancialYear", ddlFinancialYear.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@DepositorID", ddlDepositor.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@DistrictID", ddlDistrict.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@BranchID", ddlDepotList.SelectedValue.ToString());
        cmd.ExecuteNonQuery();
        SqlDataAdapter sda = new SqlDataAdapter();
        cmd.Connection = con;
        sda.SelectCommand = cmd;
        sda.Fill(ds);
        DataTable MainTable = ds.Tables[0];
        if (MainTable.Rows.Count > 0)
        {
            GV_EntryDone.DataSource = MainTable;
            GV_EntryDone.DataBind();
            GV_EntryDone.FooterRow.Cells[4].Text = "Total";
            GV_EntryDone.FooterRow.Cells[5].Text = MainTable.AsEnumerable().Sum(row => row.Field<Int32>("TotalBillPresentedinFY")).ToString();
            GV_EntryDone.FooterRow.Cells[6].Text = MainTable.AsEnumerable().Sum(row => row.Field<Decimal>("TotalBillAmountPresented")).ToString();
            GV_EntryDone.FooterRow.Cells[7].Text = MainTable.AsEnumerable().Sum(row => row.Field<Decimal>("TotalAmountReceivedInFY")).ToString();
            GV_EntryDone.FooterRow.Cells[8].Text = MainTable.AsEnumerable().Sum(row => row.Field<Decimal>("RemainingAmountFromDepositor")).ToString();

        }
        else
        {
            GV_EntryDone.DataSource = null;
            GV_EntryDone.DataBind();
        }
    }

    protected void GV_EntryDone_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void GV_EntryDone_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd = new SqlCommand(query, con);
            sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
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

    private void getDepot(string distId)
    {
        try
        {
            string query = "select depo.BranchId,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepotList.DataSource = ds.Tables[0];
                ddlDepotList.DataTextField = "DepotName";
                ddlDepotList.DataValueField = "BranchId";
                ddlDepotList.DataBind();
                ddlDepotList.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception)
        {
            ///////
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex != 0)
        {
            getDepot(ddlDistrict.SelectedValue.ToString());
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District')", true);
        }
    }


    //protected void btnCancel_Click(object sender, EventArgs e)
    //{

    //}
}


