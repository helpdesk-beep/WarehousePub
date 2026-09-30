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


public partial class Reports_States_Region_Nafed_Pendding_Ammount_Report : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        fillgrid();
    }
    public void fillgrid()
    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_NAFED_Storage_Bill_Details_For_Region", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            //cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                grpendding.DataSource = dt;
                grpendding.DataBind();
                grdbill.Visible = true;
                grpendding.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                grpendding.FooterRow.Cells[2].Text = "Total";
                grpendding.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("TotalBillGeneration")).ToString();
                grpendding.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("TotalBillGenerationAmount")).ToString();
                grpendding.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("BMSUBMITToRM")).ToString();
                grpendding.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("PendingBillSubmitToRM")).ToString();
                grpendding.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("BMSUBMITToRMAmount")).ToString();
                grpendding.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("PendingBillAmountSubmitToRM")).ToString();
                grpendding.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("RMSUBMITToNAFED")).ToString();
                grpendding.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("PendingBillSubmitToNAFED")).ToString();
                grpendding.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("RMSUBMITToNAFEDAmount")).ToString();
                grpendding.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("PendingBillAmountSubmitToNAFED")).ToString();
            }
            else
            {
                grpendding.DataSource = null;
                grpendding.DataBind();
                grdbill.Visible = true;
            }
        }
    }
    void ShowingGroupingDataInGridView(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
    {
        if (totalColumns == 0) return;
        int i, count = 1;
        ArrayList lst = new ArrayList();
        lst.Add(gridViewRows[0]);
        var ctrl = gridViewRows[0].Cells[startIndex];
        for (i = 1; i < gridViewRows.Count; i++)
        {
            TableCell nextTbCell = gridViewRows[i].Cells[startIndex];
            if (ctrl.Text == nextTbCell.Text)
            {
                count++;
                nextTbCell.Visible = false;
                lst.Add(gridViewRows[i]);
            }
            else
            {
                if (count > 1)
                {
                    ctrl.RowSpan = count;
                    ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
                }
                count = 1;
                lst.Clear();
                ctrl = gridViewRows[i].Cells[startIndex];
                lst.Add(gridViewRows[i]);
            }
        }
        if (count > 1)
        {
            ctrl.RowSpan = count;
            ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
        }
        count = 1;
        lst.Clear();
    }
}