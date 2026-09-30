using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
public partial class Reports_Branch_Rpt_Godown_Bill_Wise_Payment_Status : System.Web.UI.Page
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
            GetGodownName();

        }
    }
    //private void loadMonth()
    //{
    //    List<Month> commoList = new List<Month>();
    //    commoList.Add(new Month(0, "All"));
    //    commoList.Add(new Month(1, "1"));
    //    commoList.Add(new Month(2, "2"));
    //    commoList.Add(new Month(3, "3"));
    //    commoList.Add(new Month(4, "4"));
    //    commoList.Add(new Month(5, "5"));
    //    commoList.Add(new Month(6, "6"));
    //    commoList.Add(new Month(7, "7"));
    //    commoList.Add(new Month(8, "8"));
    //    commoList.Add(new Month(9, "9"));
    //    commoList.Add(new Month(10, "10"));
    //    commoList.Add(new Month(11, "11"));
    //    commoList.Add(new Month(12, "12"));

    //    ddlMonth.DataSource = commoList;
    //    ddlMonth.DataTextField = "MonthName";
    //    ddlMonth.DataValueField = "MonthID";
    //    ddlMonth.DataBind();

    //}
    //class Month
    //{
    //    public int MonthID { get; set; }
    //    public string MonthName { get; set; }

    //    public Month(int MonthID, string MonthName)
    //    {
    //        this.MonthID = MonthID;
    //        this.MonthName = MonthName;
    //    }
    //    public Month() { }
    //}
    public void GetGodownName()
    {
        string qry = "";
        qry = "select Godown_Id,Godown_Name from tbl_MetaData_GODOWN_2018 where Branchid='" + Session["BranchId"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGodown.DataSource = ds.Tables[0];
            ddlGodown.DataTextField = "Godown_Name";
            ddlGodown.DataValueField = "Godown_Id";
            ddlGodown.DataBind();
            ddlGodown.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {

        }
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Rpt_Godown_And_Month_Wise_Pending_Amount_For_Godown", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //if(ddlFY.SelectedItem.ToString() == "All")
                //{
                //    cmd.Parameters.AddWithValue("@FinacialYear", "0");
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@FinacialYear", ddlFY.SelectedValue.ToString());
                //}
                cmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue.ToString());
                //cmd.Parameters.AddWithValue("@FinacialYear", "0");
                //cmd.Parameters.AddWithValue("@GodownID", "23280010118");
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
                            GridView1.Caption = @"<b style=""font-weight: bold; text-align:center;""> Godown Name" + "  -   " + dt.Rows[0]["Godown_Name"].ToString();
                            GridView1.FooterRow.Style.Add("text-align", "center");
                            GridView1.FooterRow.Cells[10].Text = "Total Received Amount";
                            GridView1.FooterRow.Cells[6].Text = "Total Storage Bill Amount";
                            GridView1.FooterRow.Cells[17].Text = "Pay to Godown";
                            GridView1.FooterRow.Cells[12].Text = "Total Rent Bill Amount";
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount")).ToString();
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("RecievedAmount")).ToString();
                            GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("RentBillAmt")).ToString();
                            GridView1.FooterRow.Cells[18].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PaymentToGO")).ToString();
                            // lblgdnname.Text = dt.Rows[0]["Godown_Name"].ToString();
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

    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "";
        cell.ColumnSpan = 6;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 6;
        cell.Text = "Storage Bill Payment Details";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 8;
        cell.Text = "Rent Bill Payment Details";
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }


    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}