using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Linq;

public partial class Reports_States_Payment_Credit_From_MPWLC_To_Godown : System.Web.UI.Page
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
            fillRegion();
        }
    }  
    private void fillRegion()
    {
        try
        {
            string query = "";

            query = "SELECT DISTINCT [Region_ID],[Regionnm] FROM [tbl_MetaData_DISTRICT] order by Regionnm asc";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlRegion.Items.Clear();
                ddlRegion.DataSource = ds.Tables[0];
                ddlRegion.DataTextField = "Regionnm";
                ddlRegion.DataValueField = "Region_ID";
                ddlRegion.DataBind();
                ddlRegion.Items.Insert(0, "--Select--");
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

    private void fillDistrict()
    {
        try
        {
            string query = "";

            query = "SELECT District_Id,District_Name FROM [tbl_MetaData_DISTRICT] where Region_ID='" + ddlRegion.SelectedValue + "' order by Regionnm asc";

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

            query = "SELECT BranchId,DepotName FROM tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "' order by DepotName asc";

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
    protected void fillgrid()
    {
        
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Payment_Credit_From_MPWLC_to_Godown", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", ddlRegion.SelectedValue);
                cmd.Parameters.AddWithValue("@District_ID", ddldistrict.SelectedValue);
                cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "Payment Credit Form MPWLC TO Godown " + "</b> ";
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[14].Text = "Total";
                            GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Rent_Bill_Amount")).ToString();
                            GridView1.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TDS_Amount")).ToString();
                            GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Gain_Deduction_Amount")).ToString();
                            GridView1.FooterRow.Cells[18].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Other_Deduction_Amount")).ToString();
                            GridView1.FooterRow.Cells[19].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Resource_Detuction")).ToString();
                            GridView1.FooterRow.Cells[20].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Deduction_Amount")).ToString();
                            GridView1.FooterRow.Cells[21].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Pay_to_Godown")).ToString();
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
        fillDistrict();
    }
    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
      
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
        fillBranch();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}