using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_Branch_Rpt_Get_Data_For_Selection_FCI_PDS : System.Web.UI.Page
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
            fillgrid();
        }
        
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            //using (SqlCommand cmd = new SqlCommand("Get_Amount_From_MPSCSC_From_Aug", con))
            using (SqlCommand cmd = new SqlCommand("Get_Selection_Data_For_PDS_FCI_For_Branch", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["Depot_DepotID"].ToString());
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
                            //GridView1.Caption = @"<b style=""font-weight: bold;"">District Wise " + "</br> " + "FIFO पद्धति से उठाव हेतु शाखाओ द्वारा फ्रिज किये गये Stock की जानकारी" + "</br> ";
                            //GridView1.Columns[1].Visible = false;
                            //// GridView1.columns.RemoveAt(1);

                            //GridView1.FooterRow.Style.Add("text-align", "right");
                            //GridView1.FooterRow.Cells[2].Text = "Total";
                            //GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalQTY")).ToString();
                            //GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalQTY_For_PDS_FCI")).ToString();
                            //GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("SelectedForPDS")).ToString();
                            //GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("SelectedForFCI")).ToString();
                            //GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingForSelection")).ToString();

                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

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

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "जिले का नाम";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "शाखा का नाम";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "फ्रिज करने हेतु कुल मात्रा";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "शाखा द्वारा फ्रिज की गई कुल मात्रा";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "शाखा द्वारा PDS के लिए फ्रिज की गई कुल मात्रा";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "शाखा द्वारा FCI के लिए फ्रिज की गई कुल मात्रा";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "शाखा स्तर पर फ्रिज करने हेतु शेष मात्रा";
        //cell.Text = @"<b style=""font-weight: bold; color:white;"">MPSCSC से दिनांक " + "</br> " + DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy") + "</br> "+ "तक प्राप्त राशि रूपये में" +"</br> ";
        row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "आज दिनांक"+ "</br> " + DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy") + "</br> " + "को प्राप्त राशि रूपये में";
        //// cell.Text = @"<b style=""font-weight: bold; color:white;"">आज दिनांक " + "</br> " + System.DateTime.Now.ToShortDateString() + "</br> "+"को प्राप्त राशि रूपये में" + "</br> ";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "प्रगतिशील प्राप्त राशि";
        //row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
   
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
}