using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
public partial class Reports_State_Rpt_Payment_Status_OF_Godown_Type_Wise : System.Web.UI.Page
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
            fillGodownType();
          fillgrid();
         
        }
    }
   
   
    protected void fillgrid()
    {                
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_GodownTypw_Wise_Payment_Status_At_Varius_lavel", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlGodownType.SelectedValue == "Select")
                {
                    cmd.Parameters.AddWithValue("@HireType", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@HireType", ddlGodownType.SelectedValue.ToString());
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
                            GridView1.FooterRow.Cells[2].Text = "Total";
                            GridView1.FooterRow.Cells[3].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("Generated_Bill_Number")).ToString();
                            GridView1.FooterRow.Cells[4].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Generated_Net_Amount")).ToString();
                            GridView1.FooterRow.Cells[5].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("Submitted_Bill_Number")).ToString();
                            GridView1.FooterRow.Cells[6].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Submitted_Net_Amount")).ToString();
                            GridView1.FooterRow.Cells[7].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("PendingBillForSubmition")).ToString();
                            GridView1.FooterRow.Cells[8].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PendingBillAmountForSubmition")).ToString();
                            GridView1.FooterRow.Cells[9].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("TotalBill_Paid_by_HOMPSCSC")).ToString();
                            GridView1.FooterRow.Cells[10].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Gross_Amount")).ToString();
                            GridView1.FooterRow.Cells[11].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("TDS_Amt")).ToString();
                            GridView1.FooterRow.Cells[12].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("OtherDeduction")).ToString();
                            GridView1.FooterRow.Cells[13].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Payable_Amount")).ToString();

                           
                            GridView1.FooterRow.Cells[14].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("DM_Bill_Number")).ToString();
                            GridView1.FooterRow.Cells[15].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("DM_Net_Amount")).ToString();
                            GridView1.FooterRow.Cells[16].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("Pending_Bill_AT_HOMPSCSC")).ToString();
                            GridView1.FooterRow.Cells[17].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Pending_Bill_Amount_AT_HOMPSCSC")).ToString();
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
    protected void OnDataBound(object sender, EventArgs e)
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
        cell.ColumnSpan = 5;
        cell.Text = "Storage Charges Payment Status From MPSCSC";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "Pending Storage Charges Payment Status at DM MPSCSC";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "Pending Storage Charges Payment Status at HO MPSCSC";
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    private void fillGodownType()
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
                ddlGodownType.Items.Clear();
                ddlGodownType.DataSource = ds.Tables[0];
                ddlGodownType.DataTextField = "Hired_Type";
                ddlGodownType.DataValueField = "Hired_Type";
                ddlGodownType.DataBind();
                ddlGodownType.Items.Insert(0, new ListItem("Select", "0"));
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

    protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}