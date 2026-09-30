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

public partial class Accounting_Account_Manager_Rejected_Bills_ : System.Web.UI.Page
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
    public void GetBranch()
    {
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
            ddlbranch.Items.Insert(0, new ListItem("All", "0"));
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
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_Details_Reject_By_Account", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddldistrict.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@District_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@District_Id", Dist_id);
            }
            if (ddlbranch.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Branch_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
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
            if (ddlcommodity.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Commodity_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            }
            if (ddlcommodity.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Crop_Year", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
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
                grdbill.Visible = true;
                lblAmount.Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount")).ToString();
                Session["Amount"] = lblAmount.Text;
                txtcount.Text = GrdBills.Rows.Count.ToString();

            }
            else
            {
                GrdBills.DataSource = null;
                GrdBills.DataBind();
                grdbill.Visible = true;
                lblAmount.Text = "";
                txtcount.Text = "";
            }
        }
    }
}