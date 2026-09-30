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

public partial class Reports_Branch_Rpt_Get_Godown_Wise_Bill_Status : System.Web.UI.Page
{
    

    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    DataSet ds1 = new DataSet();
    DataSet ds2 = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            fillgrid();
            get_Districts();
        }
        
    }
    public void get_Districts()
    {
        string qry = "select Godown_ID,Godown_Name From tbl_MetaData_GODOWN_2018 where BranchID='" + Session["BranchId"].ToString() + "' order by Godown_Name";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds1);
        if (ds1 == null)
        {
        }
        else
        {
            ddlGodown.DataSource = ds1.Tables[0];
            ddlGodown.DataTextField = "Godown_Name";
            ddlGodown.DataValueField = "Godown_ID";
            ddlGodown.DataBind();
            ddlGodown.Items.Insert(0, "--Select--");
        }

    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_GodownWiseBillStatus", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branchid", Session["BranchId"].ToString());
                if (ddlGodown.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@Godownid", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Godownid", ddlGodown.SelectedValue);
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
                            GridView1.Caption = @"<b style=""font-weight: bold;"">Godown Wise " + "</br> " + "Bill Status" + "</br> ";

                            //GridView1.FooterRow.Style.Add("text-align", "right");
                            //GridView1.FooterRow.Cells[12].Text = "Total";
                            //GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("CREDIT_AMOUNT")).ToString();
                            divshowdetails.Visible = true;
                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            divshowdetails.Visible = false;
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }  
  

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "S.No.";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Godown Name";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Commodity";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Crop Year";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Financial Year";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Bill Month";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Bill Number";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Net Amount";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "DSC BY BM Storage Chrg";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "DSC BY BM Godown Rent";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "DSC BY Gdwn Owner";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "RC Dituction";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Submit to DM MPSCSC";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "DSC BY DM MPSCSC";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "DSC BY RM";
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
   
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
}