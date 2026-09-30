using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_RO_AllGodown_Wise_Inspection_Details_RO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            if (Session["StackParams"] == null)
            {
                lblCount.Text = "Session Expired";
                return;
            }

            string[] paramArray = (string[])Session["StackParams"];

            string Emp_ID = paramArray[0];
            string Quarter = paramArray[1];
            string Verification = paramArray[2];
            string FinancialYear = paramArray[3];
            string OrderNo = paramArray[4];
            string BranchID = paramArray[5];

            string conStr = ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_AllStack_Wise_PV_Details_Without_Image", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 120;

                    cmd.Parameters.AddWithValue("@Emp_id", Emp_ID);
                    cmd.Parameters.AddWithValue("@Quater_Type", Convert.ToInt64(Quarter));
                    cmd.Parameters.AddWithValue("@verification_Type", Convert.ToInt64(Verification));
                    cmd.Parameters.AddWithValue("@Financial_Year", FinancialYear);
                    cmd.Parameters.AddWithValue("@Order_No", OrderNo);
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dtOriginal = new DataTable();
                    da.Fill(dtOriginal);

                    if (dtOriginal.Rows.Count > 0)
                    {
                        // सब-टोटल और ग्रैंड-टोटल वाली नई टेबल जनरेट करें
                        DataTable dtWithTotals = CreateSubTotalRollup(dtOriginal);

                        gvAllReport.DataSource = dtWithTotals;
                        gvAllReport.DataBind();

                        // केवल वास्तविक डेटा रिकॉर्ड्स की गिनती दिखाने के लिए dtOriginal का काउंट लें
                        lblCount.Text = dtOriginal.Rows.Count.ToString();
                    }
                    else
                    {
                        gvAllReport.DataSource = null;
                        gvAllReport.DataBind();
                        lblCount.Text = "0";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblCount.Text = "Error loading data: " + ex.Message;
        }
    }

    //private DataTable CreateSubTotalRollup(DataTable dt)
    //{
    //    DataTable dtCloned = dt.Clone();

    //    // डेटा टाइप को सुरक्षित करने के लिए कॉलम स्ट्रक्चर को ऑब्जेक्ट एडजस्टमेंट दें
    //    dtCloned.Columns["OnlineBags"].DataType = typeof(object);
    //    dtCloned.Columns["PV_Bags"].DataType = typeof(object);
    //    dtCloned.Columns["SpillageBags"].DataType = typeof(object);
    //    dtCloned.Columns["Difference"].DataType = typeof(object);

    //    foreach (DataColumn col in dt.Columns)
    //    {
    //        if (col.ColumnName != "OnlineBags" && col.ColumnName != "PV_Bags" && col.ColumnName != "SpillageBags" && col.ColumnName != "Difference")
    //        {
    //            dtCloned.Columns[col.ColumnName].DataType = col.DataType;
    //        }
    //    }

    //    string currentGodownId = "";
    //    string currentGodownName = "";

    //    // सब-टोटल के लिए वैरिएबल्स
    //    int subOnline = 0, subPV = 0, subSpillage = 0, subDiff = 0;

    //    // ग्रैंड-टोटल के लिए वैरिएबल्स
    //    int grandOnline = 0, grandPV = 0, grandSpillage = 0, grandDiff = 0;

    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        // यहाँ सुधारा गया है: dt.Rows.GetRowValueAt(i) को dt.Rows[i] किया गया है
    //        DataRow row = dt.Rows[i];
    //        string godownId = row["Godown_ID"].ToString();
    //        string godownName = row["Godown_Name"].ToString();

    //        // अगर गोदाम बदल रहा है और यह पहली रो नहीं है, तो पिछले गोदाम का सब-टोटल इन्सर्ट करें
    //        if (i > 0 && godownId != currentGodownId)
    //        {
    //            AddTotalRow(dtCloned, currentGodownName + " - Sub Total", subOnline, subPV, subSpillage, subDiff, "SUBTOTAL");

    //            // सब-टोटल काउंटर्स री-सेट करें
    //            subOnline = 0; subPV = 0; subSpillage = 0; subDiff = 0;
    //        }

    //        currentGodownId = godownId;
    //        currentGodownName = godownName;

    //        // वैल्यूज पार्स करें
    //        int online = Convert.ToInt32(row["OnlineBags"] == DBNull.Value ? 0 : row["OnlineBags"]);
    //        int pv = Convert.ToInt32(row["PV_Bags"] == DBNull.Value ? 0 : row["PV_Bags"]);
    //        int spillage = Convert.ToInt32(row["SpillageBags"] == DBNull.Value ? 0 : row["SpillageBags"]);
    //        int diff = Convert.ToInt32(row["Difference"] == DBNull.Value ? 0 : row["Difference"]);

    //        // सब-टोटल और ग्रैंड-टोटल में जोड़ें
    //        subOnline += online; subPV += pv; subSpillage += spillage; subDiff += diff;
    //        grandOnline += online; grandPV += pv; grandSpillage += spillage; grandDiff += diff;

    //        // ओरिजिनल डेटा रो ऐड करें
    //        dtCloned.ImportRow(row);
    //    }

    //    // आखरी गोदाम का सब-टोटल रो जोड़ें
    //    if (dt.Rows.Count > 0)
    //    {
    //        AddTotalRow(dtCloned, currentGodownName + " - Sub Total", subOnline, subPV, subSpillage, subDiff, "SUBTOTAL");
    //    }

    //    // पूरी रिपोर्ट का ग्रैंड टोटल (Grand Total) रो जोड़ें
    //    AddTotalRow(dtCloned, "GRAND TOTAL", grandOnline, grandPV, grandSpillage, grandDiff, "GRANDTOTAL");

    //    return dtCloned;
    //}


    private DataTable CreateSubTotalRollup(DataTable dt)
    {
        // C# 5 के लिए बेसिक नल और एम्प्टी चेक
        if (dt == null || dt.Rows.Count == 0)
        {
            return dt != null ? dt.Clone() : new DataTable();
        }

        DataTable dtCloned = new DataTable();

        // C# 5 सेफ तरीका: कॉलम दर कॉलम स्ट्रक्चर कॉपी करें और चुनिंदा कॉलम्स को 'object' टाइप दें
        foreach (DataColumn col in dt.Columns)
        {
            if (col.ColumnName == "OnlineBags" || col.ColumnName == "PV_Bags" ||
                col.ColumnName == "SpillageBags" || col.ColumnName == "Difference")
            {
                dtCloned.Columns.Add(col.ColumnName, typeof(object)); // यहाँ ऑब्जेक्ट टाइप सेट कर दिया
            }
            else
            {
                dtCloned.Columns.Add(col.ColumnName, col.DataType); // बाकी कॉलम का ओरिजinal टाइप
            }
        }

        // पहली रो से शुरूआती गोदाम का नाम लें
        string currentGodownName = dt.Rows[0]["Godown_Name"].ToString();

        // सब-टोटल के लिए वैरिएबल्स
        int subOnline = 0, subPV = 0, subSpillage = 0, subDiff = 0;

        // ग्रैंड-टोटल के लिए वैरिएबल्स
        int grandOnline = 0, grandPV = 0, grandSpillage = 0, grandDiff = 0;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];
            string godownName = row["Godown_Name"].ToString();

            // सुधारा गया लॉजिक: अगर गोदाम का नाम बदल गया है (Same नहीं है), तो सब-टोटल ऐड करें
            if (godownName != currentGodownName)
            {
                AddTotalRow(dtCloned, currentGodownName + " - Sub Total", subOnline, subPV, subSpillage, subDiff, "SUBTOTAL");

                // सब-टोटल काउंटर्स को वापस 0 करें
                subOnline = 0; subPV = 0; subSpillage = 0; subDiff = 0;

                // करंट गोदाम का नाम अपडेट करें
                currentGodownName = godownName;
            }

            // DBNull चेक के साथ वैल्यूज पार्स करें (C# 5 कम्पैटिबल)
            int online = Convert.ToInt32(row["OnlineBags"] == DBNull.Value ? 0 : row["OnlineBags"]);
            int pv = Convert.ToInt32(row["PV_Bags"] == DBNull.Value ? 0 : row["PV_Bags"]);
            int spillage = Convert.ToInt32(row["SpillageBags"] == DBNull.Value ? 0 : row["SpillageBags"]);
            int diff = Convert.ToInt32(row["Difference"] == DBNull.Value ? 0 : row["Difference"]);

            // सब-टोटल और ग्रैंड-टोटल में जोड़ें
            subOnline += online; subPV += pv; subSpillage += spillage; subDiff += diff;
            grandOnline += online; grandPV += pv; grandSpillage += spillage; grandDiff += diff;

            // ओरिजिनल डेटा रो ऐड करें
            dtCloned.ImportRow(row);
        }

        // लूप खत्म होने के बाद आखिरी गोदाम का सब-टोटल रो जोड़ें
        AddTotalRow(dtCloned, currentGodownName + " - Sub Total", subOnline, subPV, subSpillage, subDiff, "SUBTOTAL");

        // पूरी रिपोर्ट का ग्रैंड टोटल (Grand Total) रो जोड़ें
        AddTotalRow(dtCloned, "GRAND TOTAL", grandOnline, grandPV, grandSpillage, grandDiff, "GRANDTOTAL");

        return dtCloned;
    }

    private void AddTotalRow(DataTable dt, string title, int online, int pv, int spillage, int diff, string rowType)
    {
        DataRow newRow = dt.NewRow();
        newRow["Officer_Name"] = rowType; // पहचान के लिए RowDataBound में काम आएगा
        newRow["Godown_Name"] = title;
        newRow["Stack_Name"] = "";
        newRow["OnlineBags"] = online;
        newRow["PV_Bags"] = pv;
        newRow["SpillageBags"] = spillage;
        newRow["Difference"] = diff;
        dt.Rows.Add(newRow);
    }

    protected void gvAllReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Officer_Name कॉलम से रो के प्रकार (Data, SubTotal, GrandTotal) का पता लगाएं
            string rowType = DataBinder.Eval(e.Row.DataItem, "Officer_Name").ToString();

            Label lblRowDiff = (Label)e.Row.FindControl("lblRowDiff");
            string diffText = DataBinder.Eval(e.Row.DataItem, "Difference").ToString();
            int difference = 0;
            int.TryParse(diffText, out difference);

            if (rowType == "SUBTOTAL")
            {
                // सब-टोटल रो का बैकग्राउंड कलर और फॉन्ट स्टाइल बदलें
                e.Row.BackColor = System.Drawing.Color.FromName("#eccc68"); // हल्का पीला रंग
                e.Row.Font.Bold = true;
                e.Row.Cells[0].Text = ""; // सीरियल नंबर खाली करें

                if (lblRowDiff != null)
                {
                    lblRowDiff.Text = (difference >= 0 ? "+" : "") + difference.ToString();
                    lblRowDiff.CssClass = "fw-bold text-dark";
                }
            }
            else if (rowType == "GRANDTOTAL")
            {
                // ग्रैंड-टोटल रो का बैकग्राउंड कलर और फॉन्ट स्टाइल बदलें
                e.Row.BackColor = System.Drawing.Color.FromName("#2ed573"); // हल्का हरा रंग
                e.Row.Font.Bold = true;
                e.Row.Cells[0].Text = ""; // सीरियल नंबर खाली करें

                if (lblRowDiff != null)
                {
                    lblRowDiff.Text = (difference >= 0 ? "+" : "") + difference.ToString();
                    lblRowDiff.CssClass = "fw-bold text-white text-uppercase bg-dark px-2 py-1 rounded";
                }
            }
            else
            {
                // यह सामान्य डेटा रो है - पुराना कलर कोड लागू करें
                if (lblRowDiff != null)
                {
                    if (difference > 0)
                    {
                        lblRowDiff.Text = "+" + difference.ToString();
                        lblRowDiff.CssClass = "badge-diff diff-info";
                    }
                    else if (difference < 0)
                    {
                        lblRowDiff.Text = difference.ToString();
                        lblRowDiff.CssClass = "badge-diff diff-danger";
                    }
                    else
                    {
                        lblRowDiff.Text = "0";
                        lblRowDiff.CssClass = "badge-diff diff-success";
                    }
                }
            }
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindData();

        if (gvAllReport.Rows.Count == 0) return;

        string fileName = "All_Godown_Inspection_Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                hw.Write("<table style='width:100%; border-collapse:collapse;'>");
                hw.Write("<tr><td colspan='14' style='text-align:center; font-size:18px; font-weight:bold; background-color:#0984e3; color:white; border:1px solid #000000; padding:10px;'>ALL GODOWN INSPECTION COOPERATIVE REPORT WITH SUB-TOTALS</td></tr>");
                hw.Write("<tr><td colspan='14' style='text-align:center; font-size:11px; border:1px solid #000000; padding:4px;'>Generated On: " + DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") + "</td></tr>");
                hw.Write("<tr><td colspan='14'>&nbsp;</td></tr>");
                hw.Write("</table>");

                if (gvAllReport.HeaderRow != null)
                {
                    foreach (TableCell cell in gvAllReport.HeaderRow.Cells)
                    {
                        cell.Style.Clear();
                        cell.Attributes.Add("style", "border:1px solid #000000; text-align:center; background-color:#dfe6e9; font-weight:bold; padding:8px;");
                    }
                }

                foreach (GridViewRow row in gvAllReport.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string cellStyle = "border:1px solid #000000; text-align:center; padding:6px;";

                        // एक्सेल एक्सपोर्ट में भी कलर कोडिंग बनाए रखने के लिए चेक करें
                        if (row.BackColor.Name == "eccc68") // SubTotal Row
                        {
                            cellStyle += "background-color:#eccc68; font-weight:bold;";
                        }
                        else if (row.BackColor.Name == "2ed573") // GrandTotal Row
                        {
                            cellStyle += "background-color:#2ed573; font-weight:bold;";
                        }

                        foreach (TableCell cell in row.Cells)
                        {
                            cell.Style.Clear();
                            cell.Attributes.Add("style", cellStyle);
                        }
                    }
                }

                gvAllReport.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // एक्सपोर्ट चेकर सत्यापन बायपास के लिए आवश्यक
    }
}