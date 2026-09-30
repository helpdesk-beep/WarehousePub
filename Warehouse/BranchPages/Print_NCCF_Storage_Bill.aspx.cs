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

public partial class BranchPages_Print_NCCF_Storage_Bill : System.Web.UI.Page
{
    System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["BranchId"] != null)
        {
            if (!IsPostBack)
            {
                GetGodown();
                GetCommodity();
                GetCropYear();
            }
        }
        else
        {
            Response.Redirect("../login.aspx");
        }

    }
    public void GetCommodity()
    {
        string qry = "";
        qry = "select distinct Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in ('63', '64', '33', '52', '27', '92', '123', '31', '65','26') order by Commodity_Name asc";
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
            ddlcommodity.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void GetCropYear()
    {
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
            ddlcropyear.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void GetGodown()
    {
        string Branch_Id = Session["BranchId"].ToString();
        string qry = "";
        qry = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 where BranchID='" + Branch_Id + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGodown.DataSource = ds.Tables[0];
            ddlGodown.DataTextField = "Godown_Name";
            ddlGodown.DataValueField = "Godown_ID";
            ddlGodown.DataBind();
            ddlGodown.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    public void fillgrid()
    {
        string Branch_Id = Session["BranchId"].ToString();
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_NCCF_Branch_Wise_Bill_Details_For_Print", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddlGodown.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Godown_Id", "0");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Godown_Id", ddlGodown.SelectedValue);
            }
            if (ddlFinancialyear.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Financial_Year", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Financial_Year", ddlFinancialyear.SelectedValue);
            }
            if (ddlmonth.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Month", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
            }
            cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
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
                lblAmount.Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount")).ToString("#,##0.00");
                txtCount.Text = GrdBills.Rows.Count.ToString();
                Session["Amount"] = lblAmount.Text;
                txtCount.Enabled = false;
                lblAmount.Enabled = false;
            }
            else
            {
                GrdBills.DataSource = null;
                GrdBills.DataBind();
                grdbill.Visible = true;
                lblAmount.Text = "";
                txtCount.Text = "";
            }
        }
    }
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    protected void GrdBills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Session["Bill_Number"] = (row.RowIndex).ToString();
            Label Billnumber = (Label)row.FindControl("lblBill_Number");
            string url = "Branch_NCCF_Print_Bill.aspx?BN=" + Base64Encode(Billnumber.Text);
            string s = "window.open('" + url + "', 'popup_window', 'width=600,height=600,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblAmount.Text = "";
    }
}