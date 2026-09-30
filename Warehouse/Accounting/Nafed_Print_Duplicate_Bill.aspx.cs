using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Nafed_Print_Duplicate_Bill : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    // Sub-Total variables for calculation
    private decimal origTotalCharges = 0;
    private decimal dupTotalCharges = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Extract BN (New/Duplicate Bill No) & BN1 (Old/Original Bill No) from QueryString
            if (Request.QueryString["BN"] != null)
            {
                string billNoNew = Base64Decode(Request.QueryString["BN"].ToString());
                string billNoOld = billNoNew;

                if (Request.QueryString["BN1"] != null)
                {
                    billNoOld = Base64Decode(Request.QueryString["BN1"].ToString());
                }

                FillGrid(billNoNew, billNoOld);

                // Load Digital Signatures based on Original Bill Number
                DSCSign(billNoOld);
                DSCSignR(billNoOld);
            }
        }
    }

    protected void FillGrid(string billNoNew, string billNoOld)
    {
        try
        {
            // Reset totals on re-bind
            origTotalCharges = 0;
            dupTotalCharges = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_For_Print_Duplicate_Bill", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Stored Procedure parameters: @Bill_Number (Original/Old) and @Bill_Number1 (Duplicate/New)
                    cmd.Parameters.AddWithValue("@Bill_Number", billNoOld);
                    cmd.Parameters.AddWithValue("@Bill_Number1", billNoNew);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                // Attach RowDataBound event dynamically if not attached in ASPX
                                GD2.RowDataBound += new GridViewRowEventHandler(GD2_RowDataBound);

                                GD2.DataSource = dt;
                                GD2.DataBind();

                                // Header control details from first record
                                DataRow dr = dt.Rows[0];
                                lblRegion.Text = dr["Regionnm"] != DBNull.Value ? dr["Regionnm"].ToString() : "";
                                lbldist.Text = dr["District_Name"] != DBNull.Value ? dr["District_Name"].ToString() : "";
                                lblBranch.Text = dr["DepotName"] != DBNull.Value ? dr["DepotName"].ToString() : "";

                                // Displaying both New and Old bill numbers in header
                                lblbillno_Actual.Text = billNoNew + " (Duplicate) / " + billNoOld + " (Original)";

                                lblAcGdwnName.Text = dr["Godown_Name"] != DBNull.Value ? dr["Godown_Name"].ToString() : "";
                                lblgodownid.Text = dr["Godown_Id"] != DBNull.Value ? dr["Godown_Id"].ToString() : "";
                                lbldatefromto.Text = dr["Bill_Month"] != DBNull.Value ? dr["Bill_Month"].ToString() : "";
                                lblbillingdate.Text = dr["Billing_Date"] != DBNull.Value ? dr["Billing_Date"].ToString() : "";
                                lblcmd_ac.Text = dr["Commodity"] != DBNull.Value ? dr["Commodity"].ToString() : "";
                                lblrate.Text = dr["Commodity_Rate"] != DBNull.Value ? dr["Commodity_Rate"].ToString() : "Rs.6.20 PER BAG / PER MONTH";
                                lblam.Text = dr["Net_Amount"] != DBNull.Value ? dr["Net_Amount"].ToString() : "";

                                if (dt.Columns.Contains("GST") && dr["GST"] != DBNull.Value)
                                    Label1.Text = dr["GST"].ToString();

                                if (dt.Columns.Contains("PAN") && dr["PAN"] != DBNull.Value)
                                    Label2.Text = dr["PAN"].ToString();
                            }
                            else
                            {
                                GD2.DataSource = null;
                                GD2.DataBind();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log error if required
        }
    }

    protected void GD2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string billType = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Bill_Type"));
            decimal charges = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total_Charges") ?? 0);

            // Accumulate values based on Bill Type
            if (billType.IndexOf("Original", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                origTotalCharges += charges;
            }
            else
            {
                dupTotalCharges += charges;
            }
        }
    }

    // Grid Render hotey samay Original aur Duplicate ka alag-alag Sub Total Add karna
    //protected override void Render(HtmlTextWriter writer)
    //{
    //    if (GD2.Rows.Count > 0)
    //    {
    //        Table gridTable = (Table)GD2.Controls[0];
    //        int originalLastRowIndex = -1;

    //        // Loop to find where the Original section ends
    //        for (int i = 0; i < GD2.Rows.Count; i++)
    //        {
    //            string billType = GD2.Rows[i].Cells[1].Text; // Index 1 is Bill_Type
    //            if (billType.IndexOf("Original", StringComparison.OrdinalIgnoreCase) >= 0)
    //            {
    //                originalLastRowIndex = i + 1; // +1 to account for Header row
    //            }
    //        }

    //        // 1. Add Original Sub-Total Row right after Original items
    //        if (originalLastRowIndex > 0)
    //        {
    //            GridViewRow origRow = new GridViewRow(-1, -1, DataControlRowType.Footer, DataControlRowState.Normal);
    //            origRow.BackColor = System.Drawing.Color.FromName("#FFF3CD");
    //            origRow.Font.Bold = true;

    //            TableCell origLabelCell = new TableCell();
    //            origLabelCell.ColumnSpan = 12;
    //            origLabelCell.Text = "Original Bill Total :- ";
    //            origLabelCell.HorizontalAlign = HorizontalAlign.Right;
    //            origRow.Cells.Add(origLabelCell);

    //            TableCell origValCell = new TableCell();
    //            origValCell.Text = origTotalCharges.ToString("N2");
    //            origValCell.HorizontalAlign = HorizontalAlign.Center;
    //            origRow.Cells.Add(origValCell);

    //            gridTable.Rows.AddAt(originalLastRowIndex + 1, origRow);
    //        }

    //        // 2. Add Duplicate Sub-Total Row at the bottom of the table
    //        GridViewRow dupRow = new GridViewRow(-1, -1, DataControlRowType.Footer, DataControlRowState.Normal);
    //        dupRow.BackColor = System.Drawing.Color.FromName("#D1E7DD");
    //        dupRow.Font.Bold = true;

    //        TableCell dupLabelCell = new TableCell();
    //        dupLabelCell.ColumnSpan = 12;
    //        dupLabelCell.Text = "Duplicate Bill Total :- ";
    //        dupLabelCell.HorizontalAlign = HorizontalAlign.Right;
    //        dupRow.Cells.Add(dupLabelCell);

    //        TableCell dupValCell = new TableCell();
    //        dupValCell.Text = dupTotalCharges.ToString("N2");
    //        dupValCell.HorizontalAlign = HorizontalAlign.Center;
    //        dupRow.Cells.Add(dupValCell);

    //        gridTable.Rows.Add(dupRow);
    //    }

    //    base.Render(writer);
    //}

    protected override void Render(HtmlTextWriter writer)
    {
        if (GD2.Rows.Count > 0)
        {
            Table gridTable = (Table)GD2.Controls[0];

            // GridView ka default empty FooterRow agar render ho raha hai to use remove karein
            if (GD2.FooterRow != null)
            {
                gridTable.Rows.Remove(GD2.FooterRow);
            }

            int originalLastRowIndex = -1;

            // Find index where Original section ends
            for (int i = 0; i < GD2.Rows.Count; i++)
            {
                string billType = GD2.Rows[i].Cells[1].Text; // Index 1 is Bill_Type
                if (billType.IndexOf("Original", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    originalLastRowIndex = i + 1; // +1 for Header row
                }
            }

            // 1. Add Original Sub-Total Row
            if (originalLastRowIndex > 0)
            {
                GridViewRow origRow = new GridViewRow(-1, -1, DataControlRowType.Footer, DataControlRowState.Normal);
                origRow.BackColor = System.Drawing.Color.FromName("#FFF3CD");
                origRow.Font.Bold = true;

                TableCell origLabelCell = new TableCell();
                origLabelCell.ColumnSpan = 12;
                origLabelCell.Text = "Original Bill Total :- ";
                origLabelCell.HorizontalAlign = HorizontalAlign.Right;
                origRow.Cells.Add(origLabelCell);

                TableCell origValCell = new TableCell();
                origValCell.Text = origTotalCharges.ToString("N2");
                origValCell.HorizontalAlign = HorizontalAlign.Center;
                origRow.Cells.Add(origValCell);

                gridTable.Rows.AddAt(originalLastRowIndex + 1, origRow);
            }

            // 2. Add Duplicate Sub-Total Row (No blank row before it)
            GridViewRow dupRow = new GridViewRow(-1, -1, DataControlRowType.Footer, DataControlRowState.Normal);
            dupRow.BackColor = System.Drawing.Color.FromName("#D1E7DD");
            dupRow.Font.Bold = true;

            TableCell dupLabelCell = new TableCell();
            dupLabelCell.ColumnSpan = 12;
            dupLabelCell.Text = "Duplicate Bill Total :- ";
            dupLabelCell.HorizontalAlign = HorizontalAlign.Right;
            dupRow.Cells.Add(dupLabelCell);

            TableCell dupValCell = new TableCell();
            dupValCell.Text = dupTotalCharges.ToString("N2");
            dupValCell.HorizontalAlign = HorizontalAlign.Center;
            dupRow.Cells.Add(dupValCell);

            gridTable.Rows.Add(dupRow);
        }

        base.Render(writer);
    }

    public void DSCSign(string billNo)
    {
        string qry = "SELECT DSC_Serial_No, DSC_Holder_Name, Client_Ip, CONVERT(VARCHAR(10), CreatedDate, 103) AS CreatedDate " +
                     "FROM tbl_Digitally_Signed_Bill_Details_NAFED WHERE Ref_Bill_No=@BillNo AND DSC_User_Type='B'";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(qry, con))
            {
                cmd.Parameters.AddWithValue("@BillNo", billNo);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Image1.Visible = true;
                        lblBSerialNo.Text = "DSC Serial No : " + dt.Rows[0]["DSC_Serial_No"].ToString();
                        lblBIP.Text = "Client IP : " + dt.Rows[0]["Client_Ip"].ToString();
                        lblBHolderName.Text = "DSC Holder Name : " + dt.Rows[0]["DSC_Holder_Name"].ToString();
                        lblBCreatedDate.Text = "DSC Sign Date : " + dt.Rows[0]["CreatedDate"].ToString();
                    }
                    else
                    {
                        Image1.Visible = false;
                        lblBSerialNo.Text = "";
                        lblBIP.Text = "";
                        lblBHolderName.Text = "";
                        lblBCreatedDate.Text = "";
                    }
                }
            }
        }
    }

    public void DSCSignR(string billNo)
    {
        string qry = "SELECT DSC_Serial_No, DSC_Holder_Name, Client_Ip, CONVERT(VARCHAR(10), CreatedDate, 103) AS CreatedDate " +
                     "FROM tbl_Digitally_Signed_Bill_Details_NAFED WHERE Ref_Bill_No=@BillNo AND DSC_User_Type='R'";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(qry, con))
            {
                cmd.Parameters.AddWithValue("@BillNo", billNo);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Image2.Visible = true;
                        lblICSerialNo.Text = "DSC Serial No : " + dt.Rows[0]["DSC_Serial_No"].ToString();
                        lblICIp.Text = "Client IP : " + dt.Rows[0]["Client_Ip"].ToString();
                        lblICHoldername.Text = "DSC Holder Name : " + dt.Rows[0]["DSC_Holder_Name"].ToString();
                        lblICCreatedDate.Text = "DSC Sign Date : " + dt.Rows[0]["CreatedDate"].ToString();
                    }
                    else
                    {
                        Image2.Visible = false;
                        lblICSerialNo.Text = "";
                        lblICIp.Text = "";
                        lblICHoldername.Text = "";
                        lblICCreatedDate.Text = "";
                    }
                }
            }
        }
    }

    public static string Base64Decode(string base64EncodedData)
    {
        try
        {
            if (string.IsNullOrEmpty(base64EncodedData)) return "";
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        catch
        {
            return base64EncodedData;
        }
    }
}