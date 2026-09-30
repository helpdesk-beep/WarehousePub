using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI;

public partial class Inspections_State_rpt_Moisture_Inspected_by_FCI_With_Percent : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
      ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);
    string currentDistrict = string.Empty;

    int subTotalMoisture = 0;
    int subTotalSentDM = 0;
    int subTotalFCIInspected = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindReportData();
        }
    }
    private void BindReportData()
    {
        try
        {
            // Web.config से Connection String रीड करना
            string connString = ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connString))
            {
                // Stored Procedure का नाम यहाँ पास करें
                using (SqlCommand cmd = new SqlCommand("Get_Godown_Moisture_Inspected_By_FCIReport", con))
                {
                    // यहाँ Command Type को बदलकर Stored Procedure सेट करें
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();

                        con.Open(); // कनेक्शन ओपन करें
                        sda.Fill(dt); // डेटा टेबल को भरें

                        // ग्रिडव्यू को डेटा बाइंड करना
                        gvReport.DataSource = dt;
                        gvReport.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // एरर हैंडलिंग (आप अपनी जरूरत के अनुसार लेबल या लॉग में एरर दिखा सकते हैं)
            // Response.Write("Error: " + ex.Message);
        }
    }

    // RowDataBound इवेंट जो डिस्ट्रिक्ट सबटोटल जनरेट करेगा
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // डेटा को रीड करना
            string district = DataBinder.Eval(e.Row.DataItem, "District Name").ToString();
            int totalMoisture = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Moisture"));
            int sentDM = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Stack Moisture Sent to DM MPSCSC/FCI"));
            int fciInspected = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FCI Inspected Stack"));

            // पहली रो (Row) के लिए डिस्ट्रिक्ट सेट करना
            if (string.IsNullOrEmpty(currentDistrict))
            {
                currentDistrict = district;
            }

            // यदि डिस्ट्रिक्ट बदल गया है, तो सबटोटल रो इन्सर्ट करें
            if (currentDistrict != district)
            {
                AddSubTotalRow(e.Row.RowIndex, currentDistrict);

                // वेरिएबल्स को नए डिस्ट्रिक्ट के लिए रीसेट करें
                currentDistrict = district;
                subTotalMoisture = 0;
                subTotalSentDM = 0;
                subTotalFCIInspected = 0;
            }

            // डिस्ट्रिक्ट वाइज वैल्यूज का सम (Sum) करना
            subTotalMoisture += totalMoisture;
            subTotalSentDM += sentDM;
            subTotalFCIInspected += fciInspected;
        }
        // डेटा बाइंडिंग खत्म होने के बाद आखिरी डिस्ट्रिक्ट का सबटोटल जोड़ना
        else if (e.Row.RowType == DataControlRowType.Footer || e.Row.RowState == DataControlRowState.Normal)
        {
            // चूँकि 'Footer' इवेंट डेटा रो के बाद चलता है, हम आखिरी डिस्ट्रिक्ट को ग्रिडव्यू रेंडर होने के बाद हैंडल करेंगे।
            // नीचे Render ओवरराइड मेथड में आखिरी डिस्ट्रिक्ट और ग्रैंड टोटल आसानी से मैनेज हो जाता है।
        }
    }

    // ग्रिडव्यू में सबटोटल रो जोड़ने का हेल्प मेथड
    private void AddSubTotalRow(int rowIndex, string districtName)
    {
        Table gridTable = (Table)gvReport.Controls[0];
        GridViewRow subTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        subTotalRow.CssClass = "subtotal-row";

        // प्रतिशत की गणना (Divide by Zero से बचने के लिए चेक)
        double percentage = subTotalMoisture > 0 ? ((double)subTotalFCIInspected * 100 / subTotalMoisture) : 0;

        // सेल्स (Cells) बनाना
        TableCell cellDistrict = new TableCell { Text = districtName + " Total :", ColumnSpan = 4, HorizontalAlign = HorizontalAlign.Right };
        TableCell cellMoisture = new TableCell { Text = subTotalMoisture.ToString(), HorizontalAlign = HorizontalAlign.Right };
        TableCell cellSentDM = new TableCell { Text = subTotalSentDM.ToString(), HorizontalAlign = HorizontalAlign.Right };
        TableCell cellFCI = new TableCell { Text = subTotalFCIInspected.ToString(), HorizontalAlign = HorizontalAlign.Right };
        TableCell cellPerc = new TableCell { Text = percentage.ToString("F2") + "%", HorizontalAlign = HorizontalAlign.Right };

        subTotalRow.Cells.Add(cellDistrict);
        subTotalRow.Cells.Add(cellMoisture);
        subTotalRow.Cells.Add(cellSentDM);
        subTotalRow.Cells.Add(cellFCI);
        subTotalRow.Cells.Add(cellPerc);

        // ग्रिडव्यू में सही जगह रो को पुश करना
        gridTable.Rows.AddAt(rowIndex + gridTable.Rows.Count - gvReport.Rows.Count, subTotalRow);
    }

    // आखिरी ग्रुप (District) के सबटोटल को ग्रिडव्यू के अंत में रेंडर करने के लिए
    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
        if (gvReport.Rows.Count > 0 && !string.IsNullOrEmpty(currentDistrict))
        {
            // आखिरी डिस्ट्रिक्ट का सबटोटल जोड़ना
            AddSubTotalRow(gvReport.Rows.Count, currentDistrict);
        }
        base.Render(writer);
    }
}