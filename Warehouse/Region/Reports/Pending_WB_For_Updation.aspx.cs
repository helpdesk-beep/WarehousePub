using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_Reports_Pending_WB_For_Updation : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString();
    int totalWB = 0;           // Grand total
    int districtWBCount = 0;   // District subtotal
    string currentDistrict = ""; // Track current district

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            if (!IsPostBack)
            {
                BindReport();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }

    private void BindReport()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlCommand cmd = new SqlCommand("usp_Pendding_Forn_WB_Name_Updation", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", Session["Region_ID"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Sort by district
                DataView dv = dt.DefaultView;
                dv.Sort = "District_Name ASC";
                DataTable sortedDt = dv.ToTable();

                // Insert district subtotal rows
                DataTable finalDt = new DataTable();
                finalDt.Columns.Add("District_Name");
                finalDt.Columns.Add("DepotName");
                finalDt.Columns.Add("WBCount", typeof(int));
                finalDt.Columns.Add("IsSubtotal", typeof(bool));

                string currentDistrict = "";
                int districtTotal = 0;

                foreach (DataRow row in sortedDt.Rows)
                {
                    string district = row["District_Name"].ToString();
                    int wbCount = Convert.ToInt32(row["WBCount"]);

                    if (currentDistrict != "" && currentDistrict != district)
                    {
                        // Insert subtotal row for previous district
                        DataRow subRow = finalDt.NewRow();
                        subRow["District_Name"] = currentDistrict + " Subtotal";
                        subRow["DepotName"] = "";
                        subRow["WBCount"] = districtTotal;
                        subRow["IsSubtotal"] = true;
                        finalDt.Rows.Add(subRow);

                        districtTotal = 0;
                    }

                    // Add actual row
                    DataRow newRow = finalDt.NewRow();
                    newRow["District_Name"] = row["District_Name"];
                    newRow["DepotName"] = row["DepotName"];
                    newRow["WBCount"] = wbCount;
                    newRow["IsSubtotal"] = false;
                    finalDt.Rows.Add(newRow);

                    districtTotal += wbCount;
                    currentDistrict = district;
                }

                // Last district subtotal
                if (districtTotal > 0)
                {
                    DataRow subRow = finalDt.NewRow();
                    subRow["District_Name"] = currentDistrict + " Subtotal";
                    subRow["DepotName"] = "";
                    subRow["WBCount"] = districtTotal;
                    subRow["IsSubtotal"] = true;
                    finalDt.Rows.Add(subRow);
                }

                gvReport.DataSource = finalDt;
                gvReport.DataBind();
            }
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            bool isSubtotal = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsSubtotal"));
            int wbCount = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "WBCount"));

            totalWB += wbCount;

            if (isSubtotal)
            {
                e.Row.CssClass = "subtotal-row"; // Bootstrap styling
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "TOTAL";
            e.Row.Cells[0].ColumnSpan = 3;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

            for (int i = 1; i <= 2; i++)
            {
                e.Row.Cells[i].Visible = false;
            }

            e.Row.Cells[3].Text = totalWB.ToString();
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition",
            "attachment;filename=Weighbridge_Null_Name_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                gvReport.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for Excel Export
    }
}