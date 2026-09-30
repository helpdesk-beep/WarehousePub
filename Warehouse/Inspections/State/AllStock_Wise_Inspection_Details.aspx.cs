using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Inspections_State_AllStock_Wise_Inspection_Details : System.Web.UI.Page
{
    // GRAND TOTAL
    int totalOnlineBags = 0;
    int totalPV = 0;
    int totalSpillage = 0;
    int totalDiff = 0;

    // SUB TOTAL
    int subOnline = 0;
    int subPV = 0;
    int subSpillage = 0;
    int subDiff = 0;

    string currentGodown = "";

    DataTable dtMain = new DataTable();

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
            Response.Write("<script>alert('Session Expired');window.close();</script>");
            return;
        }

        string[] paramArray = (string[])Session["StackParams"];

        string BranchID = paramArray[0];
        string Emp_ID = paramArray[1];
        string Quarter = paramArray[2];
        string Verification = paramArray[3];
        string FinancialYear = paramArray[4];
        string OrderNo = paramArray[5];

        string conStr =
            ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlCommand cmd =
                new SqlCommand("Get_AllStack_Wise_PV_Details", con))
            {
                cmd.CommandTimeout = 1600;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Emp_ID", Emp_ID);
                cmd.Parameters.AddWithValue("@Quater_Type", Quarter);
                cmd.Parameters.AddWithValue("@Verification_Type", Verification);
                cmd.Parameters.AddWithValue("@Financial_Year", FinancialYear);
                cmd.Parameters.AddWithValue("@Order_No", OrderNo);
                cmd.Parameters.AddWithValue("@BranchID", BranchID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                dtMain = dt;

                if (dt.Columns.Contains("StackImage") &&
                    !dt.Columns.Contains("StackImageBase64"))
                {
                    dt.Columns.Add("StackImageBase64", typeof(string));
                }

                if (dt.Columns.Contains("CommodityImage") &&
                    !dt.Columns.Contains("CommodityImageBase64"))
                {
                    dt.Columns.Add("CommodityImageBase64", typeof(string));
                }

                foreach (DataRow row in dt.Rows)
                {
                    if (row["StackImage"] != DBNull.Value)
                    {
                        byte[] img = (byte[])row["StackImage"];

                        row["StackImageBase64"] =
                            "data:image/jpeg;base64," +
                            Convert.ToBase64String(img);
                    }

                    if (row["CommodityImage"] != DBNull.Value)
                    {
                        byte[] img = (byte[])row["CommodityImage"];

                        row["CommodityImageBase64"] =
                            "data:image/jpeg;base64," +
                            Convert.ToBase64String(img);
                    }
                }

                gvReport.DataSource = dt;
                gvReport.DataBind();

                if (dt.Rows.Count > 0)
                {
                    lblOfficer.Text =
                        dt.Rows[0]["Officer_Name"].ToString();

                    lblDistrict.Text =
                        dt.Rows[0]["District_Name"].ToString();

                    lblDepot.Text =
                        dt.Rows[0]["DepotName"].ToString();
                }
            }
        }
    }

    protected void gvReport_RowDataBound(
        object sender,
        GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string godown =
                DataBinder.Eval(e.Row.DataItem, "Godown_Name").ToString();

            int online =
                Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "OnlineBags"));

            int pv =
                Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PV_Bags"));

            int spill =
                Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SpillageBags"));

            int diff =
                Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Difference"));

            // FIRST ROW
            if (currentGodown == "")
            {
                currentGodown = godown;
            }

            // GODOWN CHANGED -> ADD SUBTOTAL
            if (currentGodown != godown)
            {
                AddSubTotalRow();

                // RESET SUBTOTAL
                subOnline = 0;
                subPV = 0;
                subSpillage = 0;
                subDiff = 0;

                currentGodown = godown;
            }

            // SUB TOTAL
            subOnline += online;
            subPV += pv;
            subSpillage += spill;
            subDiff += diff;

            // GRAND TOTAL
            totalOnlineBags += online;
            totalPV += pv;
            totalSpillage += spill;
            totalDiff += diff;
        }

        // LAST ROW SUBTOTAL
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            AddSubTotalRow();

            e.Row.Cells[0].Text = "GRAND TOTAL";
            e.Row.Cells[0].Font.Bold = true;

            e.Row.Cells[6].Text = totalOnlineBags.ToString();
            e.Row.Cells[7].Text = totalPV.ToString();
            e.Row.Cells[8].Text = totalSpillage.ToString();
            e.Row.Cells[9].Text = totalDiff.ToString();

            e.Row.BackColor = Color.LightGray;
            e.Row.ForeColor = Color.Black;
            e.Row.Font.Bold = true;
        }
    }

    private void AddSubTotalRow()
    {
        GridViewRow subRow = new GridViewRow(
            0,
            0,
            DataControlRowType.DataRow,
            DataControlRowState.Normal);

        // FULL ROW STYLE
        subRow.BackColor = System.Drawing.Color.FromArgb(255, 230, 153);
        subRow.ForeColor = System.Drawing.Color.Black;
        subRow.Font.Bold = true;

        // ===========================
        // GODOWN SUB TOTAL
        // ===========================
        TableCell cell1 = new TableCell();

        cell1.Text = currentGodown + " SUB TOTAL";

        // S.No + Godown + StackID
        cell1.ColumnSpan = 6;

        cell1.HorizontalAlign = HorizontalAlign.Left;

        cell1.Style["padding"] = "10px";

        cell1.Style["font-size"] = "16px";

        cell1.Style["font-weight"] = "bold";

        cell1.BorderStyle = BorderStyle.Solid;
        cell1.BorderWidth = 1;

        subRow.Cells.Add(cell1);

        // ===========================
        // ONLINE BAGS
        // ===========================
        TableCell cell2 = new TableCell();

        cell2.Text = subOnline.ToString();

        cell2.HorizontalAlign = HorizontalAlign.Center;

        cell2.BorderStyle = BorderStyle.Solid;
        cell2.BorderWidth = 1;

        subRow.Cells.Add(cell2);

        // ===========================
        // PV BAGS
        // ===========================
        TableCell cell3 = new TableCell();

        cell3.Text = subPV.ToString();

        cell3.HorizontalAlign = HorizontalAlign.Center;

        cell3.BorderStyle = BorderStyle.Solid;
        cell3.BorderWidth = 1;

        subRow.Cells.Add(cell3);

        // ===========================
        // SPILLAGE
        // ===========================
        TableCell cell4 = new TableCell();

        cell4.Text = subSpillage.ToString();

        cell4.HorizontalAlign = HorizontalAlign.Center;

        cell4.BorderStyle = BorderStyle.Solid;
        cell4.BorderWidth = 1;

        subRow.Cells.Add(cell4);

        // ===========================
        // DIFFERENCE
        // ===========================
        TableCell cell5 = new TableCell();

        cell5.Text = subDiff.ToString();

        cell5.HorizontalAlign = HorizontalAlign.Center;

        cell5.BorderStyle = BorderStyle.Solid;
        cell5.BorderWidth = 1;

        subRow.Cells.Add(cell5);

        // ===========================
        // REMARK
        // ===========================
        TableCell cell6 = new TableCell();
        cell6.Text = "-";
        subRow.Cells.Add(cell6);

        // ===========================
        // STACK IMAGE
        // ===========================
        TableCell cell7 = new TableCell();
        cell7.Text = "-";
        subRow.Cells.Add(cell7);

        // ===========================
        // COMMODITY IMAGE
        // ===========================
        TableCell cell8 = new TableCell();
        cell8.Text = "-";
        subRow.Cells.Add(cell8);

        // ===========================
        // ADD ROW
        // ===========================
        gvReport.Controls[0].Controls.AddAt(
            gvReport.Controls[0].Controls.Count - 1,
            subRow);
    }
}