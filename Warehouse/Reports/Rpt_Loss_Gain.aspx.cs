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
using System.Globalization;

public partial class Reports_States_Rpt_Loss_Gain : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDistrict();
            FillCommodity();
        }
    }
    public void FillDistrict()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_District", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            // cmd.Parameters.AddWithValue("@PFID_ID", PFID);
            con.Open();
            ddldistrict.DataSource = cmd.ExecuteReader();
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, new ListItem("-- Select District --", "0"));
            con.Close();
        }
    }

    public void FillCommodity()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Storage_Commodity", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            ddlcommodity.DataSource = cmd.ExecuteReader();
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, new ListItem("-- Select Commodity --", "0"));
            con.Close();
        }
    }
    public void fillBranchDetails()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Fill_Branch", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District_Id", ddldistrict.SelectedValue);
            con.Open();
            ddlbranch.DataSource = cmd.ExecuteReader();
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "BranchId";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
            con.Close();
        }
    }

    public void fillGodownDetails()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Fill_Godown_List", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District_Id", ddldistrict.SelectedValue);
            cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
            con.Open();
            ddlgodown.DataSource = cmd.ExecuteReader();
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_id";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con.Close();
        }
    }

    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    protected void fillgrid()
    {
        Decimal opcloavg = 0;
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Data_For_Loss_Gain_Live", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
                cmd.Parameters.AddWithValue("@Godown_ID", ddlgodown.SelectedValue);
                cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtFDate.Text));
                cmd.Parameters.AddWithValue("@ToDate", getDate_MDY(txtTDate.Text));
                cmd.Parameters.AddWithValue("@Commodity_ID", ddlcommodity.SelectedValue);
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
                            GridView1.Caption = @"<b style=""font-weight: bold;""> भण्डारण कमी / आधिक्य का मासिक पत्रक" + "</br> " + "क्षेत्रीय कार्यालय का नाम" + "  -   " + dt.Rows[0]["Regionnm"].ToString() + "   " + "जिला का नाम" + "  -   " + dt.Rows[0]["District_Name"].ToString() + "   " + "शाखा का नाम" + "  -   " + dt.Rows[0]["DepotName"].ToString() + "</br> " + "Commodity Name" + "  -   " + ddlcommodity.SelectedItem.ToString();
                            GridView1.Columns[1].Visible = false;
                            // GridView1.columns.RemoveAt(1);
                            GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GridView1.FooterRow.Cells[2].Text = "Total";
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("OpeningBags")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OpeningQty")).ToString();
                            if (Convert.ToInt32(dt.AsEnumerable().Sum(row => row.Field<int>("OpeningBags")).ToString()) > 0)
                            {
                                opcloavg = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("OpeningQty")).ToString())) / Convert.ToInt32(dt.AsEnumerable().Sum(row => row.Field<int>("OpeningBags")).ToString());
                                GridView1.FooterRow.Cells[5].Text = Math.Round(opcloavg, 2).ToString();
                            }
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("RecBags")).ToString();
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("RecQty")).ToString();

                            //decimal opcloavg2 = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("RecQty")).ToString())) / Convert.ToInt32(dt.AsEnumerable().Sum(row => row.Field<int>("RecBags")).ToString());
                            //GridView1.FooterRow.Cells[8].Text = Math.Round(opcloavg2, 2).ToString();

                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<int>("OPRecBags")).ToString();
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OPRecQty")).ToString();

                            decimal opcloavg3 = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("OPRecQty")).ToString())) / Convert.ToInt32(dt.AsEnumerable().Sum(row => row.Field<int>("OPRecBags")).ToString());
                            GridView1.FooterRow.Cells[11].Text = Math.Round(opcloavg3, 2).ToString();
                            //GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("CalculateWeight")).ToString();
                            //GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Loss")).ToString();
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<int>("DelBags")).ToString();
                            GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("DelQty")).ToString();
                            if (Convert.ToInt32(dt.AsEnumerable().Sum(row => row.Field<int>("DelBags")).ToString()) > 0)
                            {
                                decimal opcloavg4 = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("DelQty")).ToString())) / Convert.ToInt32(dt.AsEnumerable().Sum(row => row.Field<int>("DelBags")).ToString());
                                GridView1.FooterRow.Cells[14].Text = Math.Round(opcloavg4, 2).ToString();
                            }
                            //GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BalanceWeight")).ToString();
                            GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("CalculateWeight")).ToString();
                            GridView1.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Loss")).ToString();
                            if (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("DelQty")).ToString()) > 0)
                            {
                                decimal LossPer = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("Loss")).ToString()) * 100) / Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("DelQty")).ToString());
                                GridView1.FooterRow.Cells[17].Text = Math.Round(LossPer, 2).ToString();
                            }
                            GridView1.FooterRow.Cells[18].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Gain")).ToString();
                            if (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("DelQty")).ToString()) > 0)
                            {
                                decimal gainper = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("Gain")).ToString()) * 100) / Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("DelQty")).ToString());
                                GridView1.FooterRow.Cells[19].Text = Math.Round(gainper, 2).ToString();
                            }
                            GridView1.FooterRow.Cells[20].Text = dt.AsEnumerable().Sum(row => row.Field<int>("BalanceBags")).ToString();
                            GridView1.FooterRow.Cells[21].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BalanceWeight")).ToString();
                            //ShowingGroupingDataInGridView(GridView1.Rows, 0, 10);
                        }
                        else
                        {
                            // btnUpdate.Visible = false;
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "";
        cell.ColumnSpan = 2;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 3;
        cell.Text = "Opening Balance";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 3;
        cell.Text = "Deposit";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 3;
        cell.Text = "Opening + Deposit";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 3;
        cell.Text = "Actual Delivery";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Avg Delivery Weight";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "Loss";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "Gain";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "Balance";
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranchDetails();
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodownDetails();
    }
}