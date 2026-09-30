using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Reports_States_Rpt_District_Wise_Qty_Avl_Complete_JVS_Owned : System.Web.UI.Page
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
            fillComodity();
            fillGodownHiredType();
            //fillgrid();
        }
    }

    private void fillGodownHiredType()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "select distinct Hired_Type from tbl_MetaData_GODOWN_2018";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlHiredType.Items.Clear();
                ddlHiredType.DataSource = ds.Tables[0];
                ddlHiredType.DataTextField = "Hired_Type";
                ddlHiredType.DataValueField = "Hired_Type";
                ddlHiredType.DataBind();
                ddlHiredType.Items.Insert(0, "--Select--");
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
    private void fillComodity()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                drpDwnCommodity.Items.Clear();
                drpDwnCommodity.DataSource = ds.Tables[0];
                drpDwnCommodity.DataTextField = "Commodity_Name";
                drpDwnCommodity.DataValueField = "Commodity_Id";
                drpDwnCommodity.DataBind();
                drpDwnCommodity.Items.Insert(0, "--Select--");
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
            // using (SqlCommand cmd = new SqlCommand("Get_Stock_Position_Complete_JVS", con))
            using (SqlCommand cmd = new SqlCommand("Get_Stock_Position_CapacityDtls_Complete_JVS", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (drpDwnCommodity.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@CmdID", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CmdID", drpDwnCommodity.SelectedValue);
                }
                if (ddlHiredType.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@hiredtype", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@hiredtype", ddlHiredType.SelectedValue);
                }
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
                            GridView1.FooterRow.Style.Add("text-align", "left");
                            GridView1.FooterRow.Cells[2].Text = "Total";
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalCapacity")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvailableCapacity")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("VacantCapacity")).ToString("#,##0.00");
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

    protected void drpDwnCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillgrid();
    }
    protected void dllGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (drpDwnCommodity.SelectedValue != "--Select--")
        //    fillgrid();
    }

    protected void ddlHiredType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string varComID;
        string varHiredType;

        varComID = drpDwnCommodity.SelectedValue.ToString();
        varHiredType = ddlHiredType.SelectedItem.ToString();

        //if (varHiredType == "Joint Venture")
        //{
        //    fillgridJVS();
        //}
        //else if (varHiredType == "Owned")
        //{
        //    fillgridOwned();
        //}
        //else 
        //{
        //    fillgrid();
        //}
        fillgrid();
    }

    protected void fillgridJVS()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Stock_Position_CapacityDtls_Complete_JVS", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (drpDwnCommodity.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@CmdID", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CmdID", drpDwnCommodity.SelectedValue);
                }
                if (ddlHiredType.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@hiredtype", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@hiredtype", ddlHiredType.SelectedValue);
                }
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
                            GridView1.FooterRow.Style.Add("text-align", "left");
                            GridView1.FooterRow.Cells[1].Text = "Total";
                            GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvailableQty")).ToString();

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


    protected void fillgridOwned()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Stock_Position_CapacityDtls_Complete_Owned", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (drpDwnCommodity.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@CmdID", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CmdID", drpDwnCommodity.SelectedValue);
                }
                if (ddlHiredType.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@hiredtype", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@hiredtype", ddlHiredType.SelectedValue);
                }
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
                            GridView1.FooterRow.Style.Add("text-align", "left");
                            GridView1.FooterRow.Cells[1].Text = "Total";
                            GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalCapacity")).ToString();
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvailableCapacity")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("VacantCapacity")).ToString();
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
}