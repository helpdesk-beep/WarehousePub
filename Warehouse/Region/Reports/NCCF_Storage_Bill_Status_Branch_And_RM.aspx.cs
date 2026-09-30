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

public partial class Region_Reports_NCCF_Storage_Bill_Status_Branch_And_RM : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    public string qry = "";
    int Total_Bills_Generated_At_BM = 0;
    int Bills_Submitted_By_BM_To_RM = 0;
    int Bills_Pending_At_BM = 0;
    int Bills_Submitted_By_RM_To_NCCF = 0;
    int Bills_Pending_At_RM = 0;
    int TotalGen = 0, SubmittedBM = 0, PendingBM = 0, SubmittedRM = 0, PendingRM = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["Region_ID"] != null))
        {
            if (!IsPostBack)
            {
                fillgrid();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    private void fillgrid()
    {
        String Region = Session["Region_ID"].ToString();
        SqlCommand cmd = new SqlCommand("NCCF_Bills_Pendency_Status_For_RM_And_BM", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Region_Id", Region);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grpendding.DataSource = dt;
        grpendding.DataBind();
        grdbill.Visible = dt.Rows.Count > 0;
    }


    protected void grpendding_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int tGen = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Bills_Generated_At_BM"));
            int subBM = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bills_Submitted_By_BM_To_RM"));
            int penBM = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bills_Pending_At_BM"));
            int subRM = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bills_Submitted_By_RM_To_NCCF"));
            int penRM = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bills_Pending_At_RM"));

            TotalGen += tGen;
            SubmittedBM += subBM;
            PendingBM += penBM;
            SubmittedRM += subRM;
            PendingRM += penRM;

            // Highlight if any pending
            if (penBM > 0 || penRM > 0)
            {
                e.Row.CssClass = "pending-row";
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "GRAND TOTAL";
            e.Row.Cells[0].ColumnSpan = 4;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[0].Font.Bold = true;

            // Hide merged cells
            for (int i = 1; i <= 3; i++)
            {
                e.Row.Cells[i].Visible = false;
            }

            e.Row.Cells[7].Text = TotalGen.ToString();
            e.Row.Cells[8].Text = SubmittedBM.ToString();
            e.Row.Cells[9].Text = PendingBM.ToString();
            e.Row.Cells[10].Text = SubmittedRM.ToString();
            e.Row.Cells[11].Text = PendingRM.ToString();

            e.Row.Font.Bold = true;
        }
    }
}