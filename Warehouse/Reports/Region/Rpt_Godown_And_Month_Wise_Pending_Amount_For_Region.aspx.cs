using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public partial class Reports_Region_Rpt_Godown_And_Month_Wise_Pending_Amount_For_Region : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid = "";
    SqlTransaction sqltran;
    string depottype = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
        {
            if (!IsPostBack)
            {
                loadMonth();
                string PopMsg = "";
                PopMsg = Request.QueryString["PopMsg"];
                if (Request.QueryString["PopMsg"] != null)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + PopMsg + "'); </script> ");
                }
                fillDistrict();
            }
        }
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
    private void fillDistrict()
    {
        try
        {
            string region = "";

            string query = "";

            if (Session["RoleId"].ToString() == "2")
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + Session["Region_ID"].ToString() + "' order by District_Name asc";
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
            if (Session["Region_ID"].ToString() != null)
            {
                query = "SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "'";
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
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillIssuecenter();
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Rpt_Godown_And_Month_Wise_Pending_Amount_For_Branch", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
                cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
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
                            //GridView1.Caption = @"<b style=""font-weight: bold; text-align:center;""> Godown Name" + "  -   " + dt.Rows[0]["Godown_Name"].ToString();
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[6].Text = "Total";
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount")).ToString();
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Gross_Amount")).ToString();
                            GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TDS_Amt")).ToString();
                            GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OtherDeduction")).ToString();
                            GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("RecievedAmount")).ToString();
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
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbranch.SelectedValue == "--Select--")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Branch.....'); </script> ");
        }
        else
            fillgrid();
    }
}