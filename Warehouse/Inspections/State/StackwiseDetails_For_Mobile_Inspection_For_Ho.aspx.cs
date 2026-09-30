using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Inspections_State_StackwiseDetails_For_Mobile_Inspection_For_Ho : System.Web.UI.Page
{
    int totalOnlineBags = 0;
    int totalPV = 0;
    int totalSpillage = 0;
    int totalDiff = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindReport();
        }
    }
    private void BindReport()
    {
        if (Session["StackParams"] == null)
        {
            Response.Write("<script>alert('Session Expired!');window.close();</script>");
            return;
        }

        string[] paramArray = (string[])Session["StackParams"];

        // ✅ Correct mapping
        string BranchID = paramArray[0];
        string Emp_ID = paramArray[1];
        string Quarter = paramArray[2];
        string Verification = paramArray[3];
        string FinancialYear = paramArray[4];
        string OrderNo = paramArray[5];
        string GodownID = paramArray[6];

        string conStr = ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString;

        using (SqlConnection con = new SqlConnection(conStr))
        using (SqlCommand cmd = new SqlCommand("Get_Stack_Wise_PV_Details", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
	    cmd.CommandTimeout = 1600;
            cmd.Parameters.AddWithValue("@Emp_ID", Emp_ID);
            cmd.Parameters.AddWithValue("@Quater_Type", Quarter);
            cmd.Parameters.AddWithValue("@Verification_Type", Verification);
            cmd.Parameters.AddWithValue("@Financial_Year", FinancialYear);
            cmd.Parameters.AddWithValue("@Order_No", OrderNo);
            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            cmd.Parameters.AddWithValue("@Godown_ID", GodownID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // ✅ Safe column add (duplicate error se bachne ke liye)
            if (dt.Columns.Contains("StackImage") && !dt.Columns.Contains("StackImageBase64"))
                dt.Columns.Add("StackImageBase64", typeof(string));

            if (dt.Columns.Contains("CommodityImage") && !dt.Columns.Contains("CommodityImageBase64"))
                dt.Columns.Add("CommodityImageBase64", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                if (dt.Columns.Contains("StackImage") && row["StackImage"] != DBNull.Value)
                {
                    byte[] img = (byte[])row["StackImage"];
                    row["StackImageBase64"] = "data:image/jpeg;base64," + Convert.ToBase64String(img);
                }

                if (dt.Columns.Contains("CommodityImage") && row["CommodityImage"] != DBNull.Value)
                {
                    byte[] img = (byte[])row["CommodityImage"];
                    row["CommodityImageBase64"] = "data:image/jpeg;base64," + Convert.ToBase64String(img);
                }
            }

            gvReport.DataSource = dt;
            gvReport.DataBind();

            if (dt.Rows.Count > 0)
            {
                lblOfficer.Text = dt.Rows[0]["Officer_Name"].ToString();
                lblDistrict.Text = dt.Rows[0]["District_Name"].ToString();
                lblDepot.Text = dt.Rows[0]["DepotName"].ToString();
                lblGodown.Text = dt.Rows[0]["Godown_Name"].ToString();
            }
        }
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        // 1. Paging disable karein aur data bind karein
        gvReport.AllowPaging = false;
        BindReport();

        // 2. ❗ IMAGE COLUMNS KO HIDE KAREIN ❗
        // Maan lijiye aapke 10 columns hain (0 se 9 index). 
        // Agar last 2 columns image ke hain toh:
        int totalCols = gvReport.Columns.Count;
        if (totalCols >= 2)
        {
            gvReport.Columns[totalCols - 1].Visible = false; // Last column (Commodity Image)
            gvReport.Columns[totalCols - 2].Visible = false; // Second last (Stack Image)
        }

        // --- Baki ka export code waisa hi rahega ---
        string fileName = "GodownSummary_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        // Header Design
        hw.Write("<table style='width:100%; border-collapse:collapse;'>");
        hw.Write("<tr><td colspan='8' style='text-align:center; font-size:20px; font-weight:bold;'>Godown Wise PV Report</td></tr>");
        hw.Write("</table>");

        // Grid Render (Ab hide kiye huye columns excel mein nahi jayenge)
        gvReport.RenderControl(hw);

        Response.Output.Write(sw.ToString());
        Response.Flush();

        // 3. IMPORTANT: Columns ko wapas visible karein taaki web page par dikhte rahein
        gvReport.Columns[totalCols - 1].Visible = true;
        gvReport.Columns[totalCols - 2].Visible = true;

        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that the rendered control is embodied in a ASP.NET server control */
    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int onlineBags = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "OnlineBags"));
            int pv = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PV_Bags"));
            int spillage = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SpillageBags"));
            int Difference = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Difference"));

            totalOnlineBags += onlineBags;
            totalPV += pv;
            totalSpillage += spillage;
            totalDiff += Difference;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            ((Label)e.Row.FindControl("lblTotalOnlineBags")).Text = totalOnlineBags.ToString();
            ((Label)e.Row.FindControl("lblTotalPV")).Text = totalPV.ToString();
            ((Label)e.Row.FindControl("lblTotalSpillage")).Text = totalSpillage.ToString();
            ((Label)e.Row.FindControl("lblTotalDiff")).Text = totalDiff.ToString();

            e.Row.BackColor = System.Drawing.Color.LightGray;
            e.Row.Font.Bold = true;
        }
    }
}