using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.IO;
public partial class Reports_States_Rpt_Godown_And_Month_Wise_Pending_Amount : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid = "";
    SqlTransaction sqltran;
    string depottype = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string PopMsg = "";
            PopMsg = Request.QueryString["PopMsg"];
            if (Request.QueryString["PopMsg"] != null)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + PopMsg + "'); </script> ");
            }
            loadMonth();
            fillDistrict();
            CropYear();
            if (!string.IsNullOrEmpty(Request.QueryString["MID"]) && !string.IsNullOrEmpty(Request.QueryString["DID"]) && !string.IsNullOrEmpty(Request.QueryString["BID"]))
            {
                ddlMonth.SelectedValue = Request.QueryString["MID"].ToString();
                ddlDistrict.SelectedValue = Request.QueryString["DID"].ToString();
                ddlDistrict.Enabled = false;
                fillIssuecenter();
                ddlbranch.SelectedValue = Request.QueryString["BID"].ToString();
                ddlbranch.Enabled = false;
            }
            fillgrid();
        }
    }
    private void loadMonth()
    {
        List<Month> commoList = new List<Month>();
        commoList.Add(new Month(0, "All"));
        commoList.Add(new Month(1, "1"));
        commoList.Add(new Month(2, "2"));
        commoList.Add(new Month(3, "3"));
        commoList.Add(new Month(4, "4"));
        commoList.Add(new Month(5, "5"));
        commoList.Add(new Month(6, "6"));
        commoList.Add(new Month(7, "7"));
        commoList.Add(new Month(8, "8"));
        commoList.Add(new Month(9, "9"));
        commoList.Add(new Month(10, "10"));
        commoList.Add(new Month(11, "11"));
        commoList.Add(new Month(12, "12"));

        ddlMonth.DataSource = commoList;
        ddlMonth.DataTextField = "MonthName";
        ddlMonth.DataValueField = "MonthID";
        ddlMonth.DataBind();

    }
    class Month
    {
        public int MonthID { get; set; }
        public string MonthName { get; set; }

        public Month(int MonthID, string MonthName)
        {
            this.MonthID = MonthID;
            this.MonthName = MonthName;
        }
        public Month() { }
    }
    private void fillDistrict()
    {
        try
        {
            string region = "";


            string query = "";

            query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";

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
                ddlDistrict.Items.Insert(0, "--Select--");
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


            string query = "";

            query = "  SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "' ";

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
                depottype = ds.Tables[0].Rows[0]["DepoTypeID"].ToString().Trim();

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
    private void fillGodown()
    {
        try
        {

            string query = "";

            query = "SELECT Godown_ID,Godown_Name FROM [dbo].[tbl_MetaData_GODOWN] where BranchId='" + ddlbranch.SelectedValue + "'";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodown.Items.Clear();
                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
                ddlGodown.Items.Insert(0, "--Select--");
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
    protected void fillgrid()
    {
        string Godown = "0", CropYear = "0";
        if (ddlGodown.SelectedValue == "--All--")
        {
            Godown = "0";
        }
        else if (ddlGodown.SelectedValue == "--Select--")
        {
            Godown = "0";
        }
        else
        {
            Godown = ddlGodown.SelectedValue;
        }

        if (ddlCropYear.SelectedValue == "--All--")
        {
            CropYear = "0";
        }
        else if (ddlCropYear.SelectedValue == "--Select--")
        {
            CropYear = "0";
        }
        else
        {
            CropYear = ddlCropYear.SelectedValue;
        }
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Rpt_Godown_And_Month_Wise_Pending_Amount", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
                cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
                cmd.Parameters.AddWithValue("@GodownID", Godown);
                cmd.Parameters.AddWithValue("@CropYear", CropYear);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[12].Text = "Total";
                            GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("RentBillAmt")).ToString();
                        }
                        else
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }
    public void CropYear()
    {
        try
        {
            qry = "select Full_Crop_Year CropYear from tbl_MetaData_Crop_Year";
            cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds == null)
            {
            }
            else
            {
                ddlCropYear.DataSource = ds.Tables[0];
                ddlCropYear.DataTextField = "CropYear";
                ddlCropYear.DataValueField = "CropYear";
                ddlCropYear.DataBind();
                ddlCropYear.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception)
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
        fillGodown();
        fillgrid();
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}