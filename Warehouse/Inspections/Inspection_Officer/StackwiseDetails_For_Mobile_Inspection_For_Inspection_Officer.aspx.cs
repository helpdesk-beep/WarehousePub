using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;

public partial class Inspections_Inspection_Officer_StackwiseDetails_For_Mobile_Inspection_For_Inspection_Officer : System.Web.UI.Page
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

        string Emp_ID = paramArray[0];
        string Quarter = paramArray[1];
        string Verification = paramArray[2];
        string FinancialYear = paramArray[3];
        string OrderNo = paramArray[4];
        string BranchID = paramArray[5];
        string GodownID = paramArray[6];

        string conStr = ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString;

        using (SqlConnection con = new SqlConnection(conStr))
        using (SqlCommand cmd = new SqlCommand("Get_Stack_Wise_PV_Details-Without_Image", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

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
        string fileName = "Stackwise_Inspection_Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                // 1. Top Header Metadata Layout (With Proper Borders)
                hw.Write("<table style='width:100%; border-collapse:collapse;'>");
                hw.Write("<tr><td colspan='6' style='text-align:center; font-size:18px; font-weight:bold; background-color:#6c5ce7; color:white; border:1px solid #000000; padding:10px;'>STACK WISE INSPECTION REPORT</td></tr>");
                hw.Write("<tr><td colspan='6' style='text-align:center; font-size:12px; font-weight:600; border:1px solid #000000; padding:5px;'>Detailed Warehouse Inspection</td></tr>");
                hw.Write("<tr><td colspan='6' style='border-left:1px solid #000000; border-right:1px solid #000000;'>&nbsp;</td></tr>"); // Empty space styling

                // Info Row 1
                hw.Write("<tr>");
                hw.Write("<td style='font-weight:bold; border:1px solid #000000; background-color:#f1f2f6; padding:5px;'>Officer Incharge:</td><td style='border:1px solid #000000; padding:5px;'>" + lblOfficer.Text + "</td>");
                hw.Write("<td style='font-weight:bold; border:1px solid #000000; background-color:#f1f2f6; padding:5px;'>District:</td><td colspan='3' style='border:1px solid #000000; padding:5px;'>" + lblDistrict.Text + "</td>");
                hw.Write("</tr>");

                // Info Row 2
                hw.Write("<tr>");
                hw.Write("<td style='font-weight:bold; border:1px solid #000000; background-color:#f1f2f6; padding:5px;'>Active Depot:</td><td style='border:1px solid #000000; padding:5px;'>" + lblDepot.Text + "</td>");
                hw.Write("<td style='font-weight:bold; border:1px solid #000000; background-color:#f1f2f6; padding:5px;'>Godown ID/Name:</td><td colspan='3' style='border:1px solid #000000; padding:5px;'>" + lblGodown.Text + "</td>");
                hw.Write("</tr>");

                hw.Write("<tr><td colspan='6'>&nbsp;</td></tr>");
                hw.Write("</table>");

                // 2. GridView Cells Styling for Excel Borders
                // Header Row Styling
                gvReport.HeaderRow.Style.Add("background-color", "#f8f9fa");
                gvReport.HeaderRow.Style.Add("color", "#2d3436");
                gvReport.HeaderRow.Style.Add("font-weight", "bold");
                foreach (TableCell cell in gvReport.HeaderRow.Cells)
                {
                    cell.Attributes.Add("style", "border:1px solid #000000; text-align:center; font-weight:bold; background-color:#e1e2e6; padding:8px;");
                }

                // Data Rows Styling
                foreach (GridViewRow row in gvReport.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        foreach (TableCell cell in row.Cells)
                        {
                            cell.Attributes.Add("style", "border:1px solid #000000; text-align:center; padding:6px;");

                            // Clean any inner HTML wrappers if needed for raw text representation
                            if (cell.Controls.Count > 0 && cell.Controls[0] is System.Web.UI.HtmlControls.HtmlGenericControl)
                            {
                                // nested div (jaise badge tha) uski padding reset karne ke liye taaki excel grid kharab na ho
                                var ctrl = (System.Web.UI.HtmlControls.HtmlGenericControl)cell.Controls[0];
                                ctrl.Style.Add("border", "none");
                            }
                        }
                    }
                }

                // Footer Row Styling
                if (gvReport.FooterRow != null)
                {
                    foreach (TableCell cell in gvReport.FooterRow.Cells)
                    {
                        cell.Attributes.Add("style", "border:1px solid #000000; text-align:center; font-weight:bold; background-color:#d1d2d6; padding:8px;");
                    }
                }

                // 3. Render into Output
                gvReport.RenderControl(hw);

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verification required for GridView Export */
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int onlineBags = 0, pv = 0, spillage = 0, Difference = 0;

            // C# 5 Compatible Null Checking
            object objOnline = DataBinder.Eval(e.Row.DataItem, "OnlineBags");
            object objPV = DataBinder.Eval(e.Row.DataItem, "PV_Bags");
            object objSpillage = DataBinder.Eval(e.Row.DataItem, "SpillageBags");
            object objDiff = DataBinder.Eval(e.Row.DataItem, "Difference");

            if (objOnline != null && objOnline != DBNull.Value) int.TryParse(objOnline.ToString(), out onlineBags);
            if (objPV != null && objPV != DBNull.Value) int.TryParse(objPV.ToString(), out pv);
            if (objSpillage != null && objSpillage != DBNull.Value) int.TryParse(objSpillage.ToString(), out spillage);
            if (objDiff != null && objDiff != DBNull.Value) int.TryParse(objDiff.ToString(), out Difference);

            totalOnlineBags += onlineBags;
            totalPV += pv;
            totalSpillage += spillage;
            totalDiff += Difference;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalOnlineBags = (Label)e.Row.FindControl("lblTotalOnlineBags");
            Label lblTotalPV = (Label)e.Row.FindControl("lblTotalPV");
            Label lblTotalSpillage = (Label)e.Row.FindControl("lblTotalSpillage");
            Label lblTotalDiff = (Label)e.Row.FindControl("lblTotalDiff");

            if (lblTotalOnlineBags != null) lblTotalOnlineBags.Text = totalOnlineBags.ToString();
            if (lblTotalPV != null) lblTotalPV.Text = totalPV.ToString();
            if (lblTotalSpillage != null) lblTotalSpillage.Text = totalSpillage.ToString();
            if (lblTotalDiff != null) lblTotalDiff.Text = totalDiff.ToString();

            e.Row.BackColor = System.Drawing.Color.FromName("#f8f9fa");
            e.Row.Font.Bold = true;
        }
    }
}