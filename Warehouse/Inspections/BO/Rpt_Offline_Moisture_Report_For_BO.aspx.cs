using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text;
public partial class Inspections_BO_Rpt_Offline_Moisture_Report_For_BO : System.Web.UI.Page
{
    string connStr = ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString;

    // Sub Total variables
    string currentRegion = "";
    int subGodown = 0, subStack = 0, subSent = 0, subInspected = 0, subPending = 0;

    // Grand Total variables
    int grandGodown = 0, grandStack = 0, grandSent = 0, grandInspected = 0, grandPending = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindReportData();
        }
    }
    private void BindReportData()
    {
        using (SqlConnection con = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("rpt_Godown_Wise_Offline_moisture_Entry_For_BO", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branchid", Session["UserId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))

                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvReport.DataSource = dt;
                    gvReport.DataBind();
                }
            }
        }
    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Current row values nikalna
            //string region = DataBinder.Eval(e.Row.DataItem, "Regionnm").ToString();
            //int godown = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Godown"));
            int stack = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Stack"));
            int sent = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Sent to FCI/DM"));
            int inspected = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Inspected By FCI"));
            int pending = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending As FCI"));

            // FIXED: Agar Region badalta hai toh purane region ka subtotal insert karein
            //if (currentRegion != "" && currentRegion != region)
            //{
            //    //AddSubTotalRow(e.Row.RowIndex);
            //}

            //currentRegion = region;

            // Subtotal counter update
            //subGodown += godown;
            subStack += stack;
            subSent += sent;
            subInspected += inspected;
            subPending += pending;

            // Grandtotal counter update
            //grandGodown += godown;
            grandStack += stack;
            grandSent += sent;
            grandInspected += inspected;
            grandPending += pending;
        }
    }

    // Is method ko override kiya hai taki last row ke baad child rows inject ki ja sakein
    protected override void Render(HtmlTextWriter writer)
    {
        if (gvReport.Rows.Count > 0)
        {
            //// Last Subtotal Row add karna
            Table table = (Table)gvReport.Controls[0];
            //GridViewRow subTotalRow = CreateCustomRow("Sub Total (" + currentRegion + ")", subGodown, subStack, subSent, subInspected, subPending, "subtotal-row");
            //table.Rows.AddAt(table.Rows.Count, subTotalRow);

            // Grand Total Row add karna
            GridViewRow grandTotalRow = CreateCustomRow("Grand Total", grandStack, grandSent, grandInspected, grandPending, "grandtotal-row");
            table.Rows.AddAt(table.Rows.Count, grandTotalRow);
        }
        base.Render(writer);
    }

    //private void AddSubTotalRow(int rowIndex)
    //{
    //    Table table = (Table)gvReport.Controls[0];
    //    GridViewRow subTotalRow = CreateCustomRow("Sub Total (" + currentRegion + ")", subGodown, subStack, subSent, subInspected, subPending, "subtotal-row");

    //    // Dynamic row insert calculate logic
    //    table.Rows.AddAt(rowIndex + table.Rows.Count - gvReport.Rows.Count, subTotalRow);

    //    // CRITICAL FIX: Agle region ke liye subtotal variables ko dubara ZERO (0) karna zaroori hai
    //    subGodown = 0;
    //    subStack = 0;
    //    subSent = 0;
    //    subInspected = 0;
    //    subPending = 0;
    //}

    private GridViewRow CreateCustomRow(string title, int stack, int sent, int inspected, int pending, string cssClass)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.CssClass = cssClass;

        // Alignment aur structure proper maintain rakhne ke liye design
        TableCell cellTitle = new TableCell { Text = title, ColumnSpan = 2 };
        cellTitle.Font.Bold = true;

        //TableCell cellGodown = new TableCell { Text = godown.ToString(), HorizontalAlign = HorizontalAlign.Right };
        TableCell cellStack = new TableCell { Text = stack.ToString(), HorizontalAlign = HorizontalAlign.Right };
        TableCell cellSent = new TableCell { Text = sent.ToString(), HorizontalAlign = HorizontalAlign.Right };
        TableCell cellInspected = new TableCell { Text = inspected.ToString(), HorizontalAlign = HorizontalAlign.Right };
        TableCell cellPending = new TableCell { Text = pending.ToString(), HorizontalAlign = HorizontalAlign.Right };

        row.Cells.Add(cellTitle);
        row.Cells.Add(cellStack);
        row.Cells.Add(cellSent);
        row.Cells.Add(cellInspected);
        row.Cells.Add(cellPending);

        return row;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Moisture_Entry_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvReport.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }

    // --- Print To PDF Logic (Using iTextSharp) ---
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Moisture_Entry_Report.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Document pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 0f); // Landscape format for clean look
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();

        PdfPTable table = new PdfPTable(8); // Total columns count specified directly (8 columns)

        // GridView se Headers PDF table mein copy karna
        foreach (TableCell headerCell in gvReport.HeaderRow.Cells)
        {
            PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text));
            pdfCell.BackgroundColor = new BaseColor(28, 90, 150); // Primary blue color
            table.AddCell(pdfCell);
        }

        // GridView controls ke text ko PDF Table mein fill karna (including dynamic total rows)
        Table htmlTable = (Table)gvReport.Controls[0];
        int rowIndex = 0;

        foreach (TableRow row in htmlTable.Rows)
        {
            // Agar pehli row hai, toh wo header hai, usko skip karein
            if (rowIndex == 0)
            {
                rowIndex++;
                continue;
            }

            foreach (TableCell cell in row.Cells)
            {
                PdfPCell pdfCell = new PdfPCell(new Phrase(cell.Text));

                // Color customization for exported rows
                if (row.CssClass == "subtotal-row") pdfCell.BackgroundColor = new BaseColor(234, 242, 248);
                if (row.CssClass == "grandtotal-row") pdfCell.BackgroundColor = new BaseColor(212, 230, 241);

                if (cell.ColumnSpan > 1) pdfCell.Colspan = cell.ColumnSpan;

                table.AddCell(pdfCell);
            }

            rowIndex++; // Counter badhayein
        }

        pdfDoc.Add(table);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time. */
    }
}