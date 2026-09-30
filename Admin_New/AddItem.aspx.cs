using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class Admin_New_AddItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();

        if (Session["username"] != null)
        {
            if (!IsPostBack)
            {
                BindFinancialYearDropdown();
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("/Login/Login.aspx");
        }
    }

    private void BindFinancialYearDropdown()
    {
        ddlYear.Items.Clear();
        ddlYear.Items.Add(new ListItem("-- Select Year --", "0"));

        int currentYear = DateTime.Now.Year;

        for (int i = -2; i <= 1; i++)
        {
            int startYear = currentYear + i;
            int endYear = startYear + 1;
            string yearText = startYear.ToString() + "-" + endYear.ToString();

            ddlYear.Items.Add(new ListItem(yearText, yearText));
        }

        ddlYear.SelectedValue = "0";
    }

    private string GetCurrentFinancialYear()
    {
        int currentYear = DateTime.Now.Year;
        int currentMonth = DateTime.Now.Month;

        if (currentMonth >= 4)
            return currentYear.ToString() + "-" + (currentYear + 1).ToString();
        else
            return (currentYear - 1).ToString() + "-" + currentYear.ToString();
    }

    protected void FillGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Usp_SelectInventory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Grdinventory.DataSource = dt;
                Grdinventory.DataBind();
            }
            else
            {
                Grdinventory.DataSource = null;
                Grdinventory.DataBind();
            }
        }
    }

    // =========================================================================
    // EXCEL UPLOAD LOGIC
    // =========================================================================
    protected void btnUploadExcel_Click(object sender, EventArgs e)
    {
        if (fileUploadExcel.HasFile)
        {
            try
            {
                string extension = Path.GetExtension(fileUploadExcel.FileName).ToLower();
                if (extension != ".xlsx")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "err", "alert('Please upload a valid .xlsx Excel file.');", true);
                    return;
                }

                string folderPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string tempPath = folderPath + Guid.NewGuid().ToString();
                Directory.CreateDirectory(tempPath);

                string filePath = tempPath + "\\data.zip";
                fileUploadExcel.SaveAs(filePath);

                // Unpack .xlsx (Zip structure) to temporary folder
                UnpackXlsxZip(filePath, tempPath);

                DataTable dtExcel = ParseXmlSheet(tempPath);

                // Clean Temp Folder
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }

                if (dtExcel != null && dtExcel.Rows.Count > 0)
                {
                    int successCount = 0;
                    int failCount = 0;

                    string defaultFinancialYear = GetCurrentFinancialYear(); // Fallback to 2026-2027
                    string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (string.IsNullOrEmpty(ipAddress))
                        ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                    int userId = Session["UserId"] != null ? Convert.ToInt32(Session["UserId"]) : 1;

                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString))
                    {
                        con.Open();
                        foreach (DataRow row in dtExcel.Rows)
                        {
                            string itemName = row["PARTICULARS"] != DBNull.Value ? row["PARTICULARS"].ToString().Trim() : "";

                            if (string.IsNullOrEmpty(itemName) || itemName.ToLower().Contains("total amount") || itemName.ToLower().Equals("particulars"))
                                continue;

                            int quantity = 1;
                            string qtyStr = row["QTY"] != DBNull.Value ? row["QTY"].ToString() : "";
                            if (!string.IsNullOrEmpty(qtyStr))
                            {
                                string digitsOnly = Regex.Match(qtyStr, @"\d+").Value;
                                if (!string.IsNullOrEmpty(digitsOnly))
                                    int.TryParse(digitsOnly, out quantity);
                            }

                            decimal rate = 0;
                            string rateStr = row["RATE"] != DBNull.Value ? row["RATE"].ToString() : "";
                            decimal.TryParse(rateStr, out rate);

                            string rowYear = defaultFinancialYear;
                            if (dtExcel.Columns.Contains("YEAR") && row["YEAR"] != DBNull.Value && !string.IsNullOrEmpty(row["YEAR"].ToString().Trim()))
                            {
                                rowYear = row["YEAR"].ToString().Trim();
                            }

                            SqlCommand cmd = new SqlCommand("InsertItem", con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Inventory_Name", itemName);
                            cmd.Parameters.AddWithValue("@Inventory_Rate", rate);
                            cmd.Parameters.AddWithValue("@Inventory_Quentity", quantity);
                            cmd.Parameters.AddWithValue("@Year", rowYear);
                            cmd.Parameters.AddWithValue("@Created_by", userId);
                            cmd.Parameters.AddWithValue("@Createdby_Ip", ipAddress);
                            cmd.Parameters.AddWithValue("@IP_Adress", ipAddress);

                            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 500);
                            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();

                            string result = cmd.Parameters["@TheResult"].Value.ToString();
                            if (result.StartsWith("SUCCESS"))
                                successCount++;
                            else
                                failCount++;
                        }
                    }

                    FillGrid();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('Excel Upload Completed! Successfully Added: " + successCount + " items. Skipped/Failed: " + failCount + " items.');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "err", "alert('Excel file contains no data.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "err", "alert('Error processing file: " + ex.Message.Replace("'", "").Replace("\r\n", " ") + "');", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "err", "alert('Please select an Excel file.');", true);
        }
    }

    private void UnpackXlsxZip(string zipPath, string extractPath)
    {
        // Simple shell unzipper compatible with all legacy Windows / IIS environments
        Type shellType = Type.GetTypeFromProgID("Shell.Application");
        dynamic shell = Activator.CreateInstance(shellType);
        dynamic destinationFolder = shell.NameSpace(extractPath);
        dynamic sourceFile = shell.NameSpace(zipPath);

        foreach (var item in sourceFile.Items())
        {
            destinationFolder.CopyHere(item, 16);
        }
    }

    private DataTable ParseXmlSheet(string extractPath)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PARTICULARS");
        dt.Columns.Add("QTY");
        dt.Columns.Add("RATE");
        dt.Columns.Add("YEAR");

        List<string> sharedStrings = new List<string>();
        string stringsPath = Path.Combine(extractPath, "xl\\sharedStrings.xml");

        if (File.Exists(stringsPath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(stringsPath);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("s", "http://schemas.openxmlformats.org/spreadsheetml/2006/main");

            XmlNodeList stringNodes = xmlDoc.SelectNodes("//s:si", nsmgr);
            foreach (XmlNode node in stringNodes)
            {
                sharedStrings.Add(node.InnerText);
            }
        }

        string sheetPath = Path.Combine(extractPath, "xl\\worksheets\\sheet1.xml");
        if (File.Exists(sheetPath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(sheetPath);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("s", "http://schemas.openxmlformats.org/spreadsheetml/2006/main");

            XmlNodeList rowNodes = xmlDoc.SelectNodes("//s:row", nsmgr);
            bool isHeader = true;

            foreach (XmlNode rowNode in rowNodes)
            {
                XmlNodeList cellNodes = rowNode.SelectNodes("s:c", nsmgr);
                Dictionary<string, string> rowValues = new Dictionary<string, string>();

                foreach (XmlNode cellNode in cellNodes)
                {
                    string cellRef = cellNode.Attributes["r"] != null ? cellNode.Attributes["r"].Value : "";
                    string colName = Regex.Replace(cellRef, @"[\d]", "");

                    string cellType = cellNode.Attributes["t"] != null ? cellNode.Attributes["t"].Value : "";
                    XmlNode valNode = cellNode.SelectSingleNode("s:v", nsmgr);
                    string cellValue = valNode != null ? valNode.InnerText : "";

                    if (cellType == "s" && !string.IsNullOrEmpty(cellValue))
                    {
                        int stringIndex = int.Parse(cellValue);
                        if (stringIndex < sharedStrings.Count)
                        {
                            cellValue = sharedStrings[stringIndex];
                        }
                    }

                    rowValues[colName] = cellValue;
                }

                if (isHeader)
                {
                    isHeader = false;
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr["PARTICULARS"] = rowValues.ContainsKey("B") ? rowValues["B"] : "";
                    dr["QTY"] = rowValues.ContainsKey("C") ? rowValues["C"] : "";
                    dr["RATE"] = rowValues.ContainsKey("D") ? rowValues["D"] : "";
                    dr["YEAR"] = rowValues.ContainsKey("E") ? rowValues["E"] : "";

                    dt.Rows.Add(dr);
                }
            }
        }

        return dt;
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(ipAddress))
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];

        try
        {
            string ErrorMsg = "";
            ErrorMsg += !string.IsNullOrEmpty(txtItemName.Text.Trim()) ? "" : "Enter Item Name \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtPrice.Text.Trim()) ? "" : "Enter Item Price \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtavailablequan.Text.Trim()) ? "" : "Enter Item Available Quantity \\n";

            if (ddlYear.SelectedValue == "0" || string.IsNullOrEmpty(ddlYear.SelectedValue))
            {
                ErrorMsg += "Select Year \\n";
            }

            if (ErrorMsg == "")
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString))
                {
                    int userId = Session["UserId"] != null ? Convert.ToInt32(Session["UserId"]) : 1;

                    if (btnsave.Text == "Save")
                    {
                        SqlCommand cmd = new SqlCommand("InsertItem", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        cmd.Parameters.AddWithValue("@Inventory_Name", txtItemName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Inventory_Rate", Convert.ToDecimal(txtPrice.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Inventory_Quentity", Convert.ToInt32(txtavailablequan.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedValue);
                        cmd.Parameters.AddWithValue("@Created_by", userId);
                        cmd.Parameters.AddWithValue("@Createdby_Ip", ipAddress);
                        cmd.Parameters.AddWithValue("@IP_Adress", ipAddress);

                        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 500);
                        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                        if (TheResult.StartsWith("SUCCESS"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('Item Saved Successfully !!!')", true);
                            FillGrid();
                            TextClear();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult.Replace("'", "") + "');", true);
                        }
                    }
                    else if (btnsave.Text == "Edit")
                    {
                        SqlCommand cmd = new SqlCommand("Usp_UpdateItem", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        cmd.Parameters.AddWithValue("@Inventory_Id", Convert.ToInt32(ViewState["Inventory_Id"]));
                        cmd.Parameters.AddWithValue("@Inventory_Name", txtItemName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Inventory_Rate", Convert.ToDecimal(txtPrice.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Inventory_Quentity", Convert.ToInt32(txtavailablequan.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedValue);

                        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 500);
                        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                        if (TheResult.StartsWith("SUCCESS"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('Item Updated Successfully !!!')", true);
                            FillGrid();
                            TextClear();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Update Failed!');", true);
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ErrorMsg + "')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "err", "alert('" + ex.Message.Replace("'", "").Replace("\r\n", " ") + "')", true);
        }
    }

    protected void TextClear()
    {
        txtItemName.Text = "";
        txtPrice.Text = "";
        txtavailablequan.Text = "";
        ddlYear.SelectedValue = "0";
        btnsave.Text = "Save";
        ViewState["Inventory_Id"] = null;
    }

    protected void Grdinventory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRecord")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblInventoryName = (Label)row.FindControl("lblInventoryName");
            Label lblInventoryRate = (Label)row.FindControl("lblInventoryRate");
            Label lblQuentity = (Label)row.FindControl("lblQuentity");
            Label lblYear = (Label)row.FindControl("lblYear");

            ViewState["Inventory_Id"] = e.CommandArgument.ToString();

            txtItemName.Text = lblInventoryName.Text;
            txtPrice.Text = lblInventoryRate.Text;
            txtavailablequan.Text = lblQuentity.Text;

            if (ddlYear.Items.FindByValue(lblYear.Text) != null)
            {
                ddlYear.SelectedValue = lblYear.Text;
            }
            else
            {
                ddlYear.SelectedValue = "0";
            }

            btnsave.Text = "Edit";
        }
    }
}