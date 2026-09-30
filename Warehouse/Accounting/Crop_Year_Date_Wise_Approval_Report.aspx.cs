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

public partial class Accounting_Crop_Year_Date_Wise_Approval_Report : System.Web.UI.Page
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

            GetCommodity();
            GetCropYear();
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    public void fillgrid()
    {
        string CommodityID = ddlcommodity.SelectedValue;
        string CropYear = ddlcropyear.SelectedValue;
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Crop_Year_WIse_Date_Wise_Bill_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddlcommodity.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Commodity_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            }
            //cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            if (ddlcropyear.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Crop_Year", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            }
            if (getDate_MDY(txtDate.Text) == "")
            {
                cmd.Parameters.AddWithValue("@Created_On", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Created_On", getDate_MDY(txtDate.Text));
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                GrdBills.DataSource = dt;
                GrdBills.DataBind();
                showbill.Visible = true;
                grdbill.Visible = true;
                lblAmount.Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount")).ToString();
                Session["Amount"] = lblAmount.Text;
                txtcount.Text = GrdBills.Rows.Count.ToString();
                txtcount.Enabled = false;
                lblAmount.Enabled = false;
                showsearch.Visible = false;
            }
            else
            {
                GrdBills.DataSource = null;
                GrdBills.DataBind();
                grdbill.Visible = true;
                showbill.Visible = true;
                lblAmount.Text = "";
                txtcount.Text = "";
            }
        }
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy-MM-dd");
            return converted;
        }
    }

    private void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Bill_Number_Wise_Data_Nafed-Account", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Bill_Number", txtBillNumber.Text.Trim());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                Gridsearch.DataSource = dt;
                Gridsearch.DataBind();
                showbill.Visible = false;
                showsearch.Visible = true;
            }
            else
            {
                Gridsearch.DataSource = null;
                Gridsearch.DataBind();
                showbill.Visible = true;
                lblAmount.Text = "";
                txtcount.Text = "";
            }
        }
    }
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    BindGrid(txtBillNumber.Text.Trim());
    //}
    protected void btnSearch_Click1(object sender, EventArgs e)
    {
        BindGrid();
        showbill.Visible = false;
        showsearch.Visible = true;
    }
}