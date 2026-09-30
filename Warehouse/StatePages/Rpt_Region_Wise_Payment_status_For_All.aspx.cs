using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
public partial class Reports_State_Rpt_Region_Wise_Payment_status_For_All : System.Web.UI.Page
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
            fillFinasncialYear();
            fillgrid();
            fillgridFY();
            //fillgrid2();
            //fillgrid3();
        }
    }

    private void fillFinasncialYear()
    {
        try
        {
            string query = "";

            query = "select distinct Financial_Year from tbl_Institution_Storage_Bill_Details where Financial_Year>'2019-2020'";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlFY.Items.Clear();
                ddlFY.DataSource = ds.Tables[0];
                ddlFY.DataTextField = "Financial_Year";
                ddlFY.DataValueField = "Financial_Year";
                ddlFY.DataBind();
                ddlFY.Items.Insert(0, "--Select--");
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
            using (SqlCommand cmd = new SqlCommand("Get_All_WH_Payment_Status", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlFY.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@FinancialYear", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FinancialYear", ddlFY.SelectedValue);
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet DS = new DataSet())
                    {
                        sda.Fill(DS);
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            GridView1.DataSource = DS.Tables[0];
                            GridView1.DataBind();

                            GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GridView1.FooterRow.Cells[1].Text = "Total";
                            GridView1.FooterRow.Cells[2].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("TotalGodown")).ToString();
                            GridView1.FooterRow.Cells[3].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("TotalStorageBill")).ToString();
                            GridView1.FooterRow.Cells[4].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Amount")).ToString();
                            GridView1.FooterRow.Cells[5].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("TotalBillReceivedPayment")).ToString();
                            GridView1.FooterRow.Cells[6].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Gross_Amount")).ToString();
                            GridView1.FooterRow.Cells[7].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("TDS_Amt")).ToString();
                            GridView1.FooterRow.Cells[8].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("OtherDeduction")).ToString();
                            GridView1.FooterRow.Cells[9].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Payable_Amount")).ToString();
                            GridView1.FooterRow.Cells[10].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PendindAtMPSCSC")).ToString();
                            }
                        else
                        {
                            GridView1.DataSource = DS.Tables[0];
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void fillgridFY()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Region_Fiacial_Year_Wise_All_WH_Payment_Status", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlFY.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@FinancialYear", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FinancialYear", ddlFY.SelectedValue);
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet DS = new DataSet())
                    {
                        sda.Fill(DS);
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            GridView2.DataSource = DS.Tables[0];
                            GridView2.DataBind();

                            GridView2.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GridView2.FooterRow.Cells[2].Text = "Total";
                            GridView2.FooterRow.Cells[3].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("TotalGodown")).ToString();
                            GridView2.FooterRow.Cells[4].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("TotalStorageBill")).ToString();
                            GridView2.FooterRow.Cells[5].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Amount")).ToString();
                            GridView2.FooterRow.Cells[6].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("TotalBillReceivedPayment")).ToString();
                            GridView2.FooterRow.Cells[7].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Gross_Amount")).ToString();
                            GridView2.FooterRow.Cells[8].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("TDS_Amt")).ToString();
                            GridView2.FooterRow.Cells[9].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("OtherDeduction")).ToString();
                            GridView2.FooterRow.Cells[10].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Payable_Amount")).ToString();
                            GridView2.FooterRow.Cells[11].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PendindAtMPSCSC")).ToString();
                           }
                        else
                        {
                            GridView2.DataSource = DS.Tables[0];
                            GridView2.DataBind();
                        }
                    }
                }
            }
        }
    }



    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "";
        cell.ColumnSpan = 2;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 6;
        cell.Text = "Storage Charges Bill Generated by Branch Manager";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 6;
        cell.Text = "Storage Charges Payment Status From MPSCSC";
        row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 5;
        //cell.Text = "Private Warehouse Storage Charges Payment Status From MPSCSC";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 4;
        //cell.Text = "Rent Bill Payment Details";
        //row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }


    protected void GridView2_DataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "";
        cell.ColumnSpan = 3;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 6;
        cell.Text = "Storage Charges Bill Generated by Branch Manager";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 6;
        cell.Text = "Storage Charges Payment Status From MPSCSC";
        row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 5;
        //cell.Text = "Private Warehouse Storage Charges Payment Status From MPSCSC";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 3;
        //cell.Text = "Rent Bill Payment Details";
        //row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView2.HeaderRow.Parent.Controls.AddAt(0, row);
    }

    
   
    protected void ddlFY_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
        fillgridFY();
    }
}