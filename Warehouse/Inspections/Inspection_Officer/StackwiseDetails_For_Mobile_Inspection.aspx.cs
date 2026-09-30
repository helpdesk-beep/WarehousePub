
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;

public partial class Inspections_Inspection_Officer_StackwiseDetails_For_Mobile_Inspection : System.Web.UI.Page
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
        using (SqlCommand cmd = new SqlCommand("Get_Stack_Wise_PV_Details", con))
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
            {
                dt.Columns.Add("StackImageBase64", typeof(string));
            }

            if (dt.Columns.Contains("CommodityImage") && !dt.Columns.Contains("CommodityImageBase64"))
            {
                dt.Columns.Add("CommodityImageBase64", typeof(string));
            }

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
        BindReport(); // Fresh data binding

        string fileName = "StackWise_" + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") + ".xls";

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                // ⚡ USER CRITICAL: Excel export se pehle dono Image Columns ko pakad kar hide karein
                int totalCols = gvReport.Columns.Count;
                if (totalCols >= 2)
                {
                    gvReport.Columns[totalCols - 1].Visible = false; // Last column (Commodity Image)
                    gvReport.Columns[totalCols - 2].Visible = false; // Second last column (Stack Image)
                }

                // Re-bind taaki hidden attributes apply ho sakein aur paging ka load na aaye
                gvReport.AllowPaging = false;
                gvReport.DataBind();

                // ✅ Top Header Design Layout (Dono columns hide hone ke baad ab proper colspan 7 ya 8 scale hoga)
                // Aapke visible data columns ke structure ke mutabik hum colspan set kar rahe hain taaki header break na ho
                int activeColspan = 8;

                hw.Write("<table style='width:100%; border-collapse:collapse;'>");
                hw.Write("<tr><td colspan='" + activeColspan + "' style='text-align:center; font-size:18px; font-weight:bold; background-color:#6c5ce7; color:white; border:1px solid #000000; padding:10px;'>INSPECTION OFFICER PV REPORT</td></tr>");
                hw.Write("<tr><td colspan='" + activeColspan + "' style='text-align:center; font-size:11px; border:1px solid #000000; padding:4px;'>Generated On: " + DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") + "</td></tr>");
                hw.Write("<tr><td colspan='" + activeColspan + "'>&nbsp;</td></tr>");

                // Info Section
                hw.Write("<tr>");
                hw.Write("<td style='font-weight:bold; border:1px solid #000000; background-color:#f1f2f6; padding:5px;'>Officer Incharge:</td><td colspan='2' style='border:1px solid #000000; padding:5px;'>" + lblOfficer.Text + "</td>");
                hw.Write("<td style='font-weight:bold; border:1px solid #000000; background-color:#f1f2f6; padding:5px;'>District:</td><td colspan='" + (activeColspan - 4) + "' style='border:1px solid #000000; padding:5px;'>" + lblDistrict.Text + "</td>");
                hw.Write("</tr>");
                hw.Write("<tr>");
                hw.Write("<td style='font-weight:bold; border:1px solid #000000; background-color:#f1f2f6; padding:5px;'>Active Depot:</td><td colspan='2' style='border:1px solid #000000; padding:5px;'>" + lblDepot.Text + "</td>");
                hw.Write("<td style='font-weight:bold; border:1px solid #000000; background-color:#f1f2f6; padding:5px;'>Godown ID/Name:</td><td colspan='" + (activeColspan - 4) + "' style='border:1px solid #000000; padding:5px;'>" + lblGodown.Text + "</td>");
                hw.Write("</tr>");

                hw.Write("<tr><td colspan='" + activeColspan + "'>&nbsp;</td></tr>");
                hw.Write("</table>");

                // ✅ Header Row Styling
                if (gvReport.HeaderRow != null)
                {
                    gvReport.HeaderRow.Style.Add("background-color", "#f8f9fa");
                    gvReport.HeaderRow.Style.Add("font-weight", "bold");
                    foreach (TableCell cell in gvReport.HeaderRow.Cells)
                    {
                        cell.Attributes.Add("style", "border:1px solid #000000; text-align:center; background-color:#dfe6e9; font-weight:bold; padding:8px;");
                    }
                }

                // ✅ Data Rows Handling (Borders aur Dynamic TextBox cleaner logic)
                foreach (GridViewRow row in gvReport.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        foreach (TableCell cell in row.Cells)
                        {
                            cell.Attributes.Add("style", "border:1px solid #000000; text-align:center; padding:6px;");

                            // Remark ke TextBox aur any dynamic fields ko direct text value mein convert karein
                            if (cell.Controls.Count > 0)
                            {
                                for (int i = cell.Controls.Count - 1; i >= 0; i--)
                                {
                                    Control ctrl = cell.Controls[i];
                                    if (ctrl is TextBox)
                                    {
                                        TextBox txt = (TextBox)ctrl;
                                        Literal literal = new Literal();
                                        literal.Text = txt.Text;
                                        cell.Controls.RemoveAt(i);
                                        cell.Controls.AddAt(i, literal);
                                    }
                                    else if (ctrl is LinkButton)
                                    {
                                        LinkButton btn = (LinkButton)ctrl;
                                        Literal literal = new Literal();
                                        literal.Text = btn.Text;
                                        cell.Controls.RemoveAt(i);
                                        cell.Controls.AddAt(i, literal);
                                    }
                                }
                            }
                        }
                    }
                }

                // ✅ Footer Styling
                if (gvReport.FooterRow != null)
                {
                    foreach (TableCell cell in gvReport.FooterRow.Cells)
                    {
                        cell.Attributes.Add("style", "border:1px solid #000000; text-align:center; font-weight:bold; background-color:#b2bec3; padding:8px;");
                    }
                }

                // Render output to sheet stream
                gvReport.RenderControl(hw);

                Response.Output.Write(sw.ToString());
                Response.Flush();

                // ⚡ IMPORTANT: Export script execute hone ke baad portal UI par fields ko wapas visible true karein
                if (totalCols >= 2)
                {
                    gvReport.Columns[totalCols - 1].Visible = true;
                    gvReport.Columns[totalCols - 2].Visible = true;
                }

                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verification target endpoint bypass handler */
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int onlineBags = 0, pv = 0, spillage = 0, Difference = 0;

            // Safe C# 5 Object Mapping
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

            if (lblTotalDiff != null)
            {
                if (totalDiff > 0)
                    lblTotalDiff.Text = "+" + totalDiff.ToString();
                else
                    lblTotalDiff.Text = totalDiff.ToString();
            }

            e.Row.BackColor = System.Drawing.Color.FromName("#f1f2f6");
            e.Row.Font.Bold = true;
        }
    }
}