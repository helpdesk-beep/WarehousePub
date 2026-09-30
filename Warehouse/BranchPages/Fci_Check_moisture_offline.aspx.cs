using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Globalization;
using System.Net;
using System.Net.Sockets;

public partial class BranchPages_Fci_Check_moisture_offline : System.Web.UI.Page
{
    private string connString = ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Initializing Temporary DataTable Structure for Batch Processing
            InitializeTempTable();
            BindInspectionGrid();
            fillGodown();
        }
    }

    private void InitializeTempTable()
    {
        DataTable dtBatch = new DataTable();
        dtBatch.Columns.Add("Stack_ID", typeof(string));
        dtBatch.Columns.Add("Godown_ID", typeof(string));
        dtBatch.Columns.Add("Godown_Name", typeof(string));
        dtBatch.Columns.Add("Stack_Name", typeof(string));
        dtBatch.Columns.Add("Commodity_Name", typeof(string)); // Agar specific static fill karna ho toh
        dtBatch.Columns.Add("FCI_Checked", typeof(string));
        dtBatch.Columns.Add("FCI_Checked_Date", typeof(string));
        dtBatch.Columns.Add("FCI_Moisture", typeof(string));

        ViewState["BatchRecords"] = dtBatch;
    }

    private void fillGodown()
    {
        try
        {
            string query = "Select Godown_ID,Godown_Name from Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 Where BranchID ='" + Session["BranchId"].ToString() + "' Order By Godown_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
                ddlGodown.Items.Insert(0, "Select");
            }
            else
            {
                ddlGodown.Items.Clear();
                ddlGodown.Items.Insert(0, "Select");
            }
        }
        catch (Exception) { }
    }

    private void BindStackDropdown()
    {
        string query = @"SELECT DISTINCT M.Stack_ID, M.Stack_Name
                        FROM tbl_Stack_Wise_Moisture_Entry_By_BM M
                        WHERE M.Recorded_By = 'General' And M.FCI_Inspected IS null
                            AND M.Godown_ID = @GodownID
                            AND M.Branch_ID = @BranchID
                            AND NOT EXISTS (
                                SELECT 1 FROM tbl_Stack_Wise_Moisture_Entry_By_BM F
                                WHERE F.Stack_ID = M.Stack_ID AND F.Recorded_By = 'FCI' AND F.Godown_ID = M.Godown_ID AND F.Branch_ID = M.Branch_ID
                            )
                            AND NOT EXISTS (
                                SELECT 1 FROM tbl_FCI_Moisture_Entry_web W
                                WHERE W.StackId = M.Stack_ID AND W.GodownId = M.Godown_ID AND W.BranchId = M.Branch_ID And W.IsFCIChecked='Y'
                            );";

        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            string branchId = Session["BranchId"] != null ? Session["BranchId"].ToString() : "";
            cmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue);
            cmd.Parameters.AddWithValue("@BranchID", branchId);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                try
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    da.Fill(dt);
                    ddlStack.Items.Clear();
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("DisplayMember", typeof(string), "Stack_ID + ' - ' + Stack_Name");
                        ddlStack.DataSource = dt;
                        ddlStack.DataTextField = "DisplayMember";
                        ddlStack.DataValueField = "Stack_ID";
                        ddlStack.DataBind();
                    }
                    ddlStack.Items.Insert(0, new ListItem("Select", "0"));
                }
                catch (Exception) { }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                }
            }
        }
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindStackDropdown();
    }

    // ➕ ADD TO GRID LOGIC (Saves to ViewState DataTable)
    protected void btnAddToGrid_Click(object sender, EventArgs e)
    {
        string ErrorMsg = "";
        ErrorMsg += ddlGodown.SelectedIndex > 0 ? "" : "Please Select Godown... \\n";
        ErrorMsg += ddlStack.SelectedIndex > 0 ? "" : "Please Select Stack... \\n";
        ErrorMsg += ddlFCIChecked.SelectedIndex > 0 ? "" : "Please Select FCI Checked... \\n";
        ErrorMsg += !string.IsNullOrEmpty(txtfcimoisture.Text) ? "" : "Enter FCI Moisture. \\n";

        if (ErrorMsg != "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ErrorMsg + "')", true);
            return;
        }

        DataTable dtBatch = (DataTable)ViewState["BatchRecords"];

        // Duplicate Check in Current Batch Grid
        DataRow[] duplicateCheck = dtBatch.Select("Stack_ID = '" + ddlStack.SelectedValue + "'");
        if (duplicateCheck.Length > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "duplicateMsg", "alert('This Stack is already added to the list!');", true);
            return;
        }

        // Add Record to Local DataTable
        DataRow dr = dtBatch.NewRow();
        dr["Stack_ID"] = ddlStack.SelectedValue;
        dr["Godown_ID"] = ddlGodown.SelectedValue;
        dr["Godown_Name"] = ddlGodown.SelectedItem.Text;
        dr["Stack_Name"] = ddlStack.SelectedItem.Text;
        dr["Commodity_Name"] = "N/A"; // Sp_GetFciMoistureRecords ke dynamic structured logic ke anusaar default ya manual daal sakte hain
        dr["FCI_Checked"] = ddlFCIChecked.SelectedValue;
        dr["FCI_Checked_Date"] = !string.IsNullOrEmpty(txtfcidate.Text) ? getDate_MDY(txtfcidate.Text) : DateTime.Now.ToString("MM/dd/yyyy");
        dr["FCI_Moisture"] = txtfcimoisture.Text;

        dtBatch.Rows.Add(dr);
        ViewState["BatchRecords"] = dtBatch;

        // Bind Grid & Control Panel Visibility
        gvFciRecords.DataSource = dtBatch;
        gvFciRecords.DataBind();

        // Control Visibility of Final Panel
        ControlFinalSubmitPanelVisibility();

        // Specific Clean fields only (Keep Godown for ease of data entry)
        ddlStack.SelectedIndex = 0;
        ddlFCIChecked.SelectedIndex = 0;
        txtfcidate.Text = "";
        txtfcimoisture.Text = "";
    }

    // ❌ REMOVE FROM GRID LOGIC
    protected void gvFciRecords_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRecord")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            DataTable dtBatch = (DataTable)ViewState["BatchRecords"];

            dtBatch.Rows[index].Delete();
            dtBatch.AcceptChanges();
            ViewState["BatchRecords"] = dtBatch;

            gvFciRecords.DataSource = dtBatch;
            gvFciRecords.DataBind();

            ControlFinalSubmitPanelVisibility();
        }
    }

    private void ControlFinalSubmitPanelVisibility()
    {
        DataTable dtBatch = (DataTable)ViewState["BatchRecords"];
        // Is control string ko dynamically frontend element container control panel se bind kiya h
        pnlFinalSubmit.Visible = (dtBatch != null && dtBatch.Rows.Count > 0);
        Addgrid.Visible = (dtBatch != null && dtBatch.Rows.Count > 0);
    }

    // 💾 FINAL SUBMIT & SAVE BATCH TO DATABASE WITH PDF
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dtBatch = (DataTable)ViewState["BatchRecords"];
        if (dtBatch == null || dtBatch.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('No record found in list to submit!');", true);
            return;
        }

        if (!FileUpload1.HasFile)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('Please select PDF Document!');", true);
            return;
        }

        try
        {
            string strFileName = "", strExtension = "", strTimeStamp = "";
            string[] allowedExtensions = { ".pdf" };
            string extension = Path.GetExtension(FileUpload1.FileName).ToLower();

            if (Array.IndexOf(allowedExtensions, extension) < 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('Please select PDF file Only!');", true);
                return;
            }

            // PDF Upload Handler
            string folder = Server.MapPath("~/FCI_Document/");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            strFileName = FileUpload1.FileName.ToString();
            strExtension = Path.GetExtension(strFileName);
            strTimeStamp = DateTime.Now.ToString("ddMMyyyyHHmmss");
            string strName = Path.GetFileNameWithoutExtension(strFileName).Replace(" ", "_");
            strFileName = strName + "_" + strTimeStamp + strExtension;
            string path = Path.Combine(folder, strFileName);
            FileUpload1.SaveAs(path);

            int successCount = 0;

            // Loop to Save Each Record From Grid View State to Database
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                foreach (DataRow row in dtBatch.Rows)
                {
                    using (SqlCommand cmd = new SqlCommand("Usp_Update_FCI_Offline_Moisture", con))
                    {
                        cmd.CommandType = cmd.CommandType == CommandType.StoredProcedure ? CommandType.StoredProcedure : CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FCI_Checked", row["FCI_Checked"].ToString());
                        cmd.Parameters.AddWithValue("@FCI_Checked_Date", row["FCI_Checked_Date"].ToString());
                        cmd.Parameters.AddWithValue("@FCI_Moisture", row["FCI_Moisture"].ToString());
                        cmd.Parameters.AddWithValue("@FCI_Document", strFileName); // Same Saved Document Link for all Grid batch entries
                        cmd.Parameters.AddWithValue("@Stack_ID", row["Stack_ID"].ToString());
                        cmd.Parameters.AddWithValue("@Updated_by", Session["BranchId"].ToString());
                        cmd.Parameters.AddWithValue("@Updatedby_Ip", GetLocalIPAddress());

                        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                        if (TheResult.StartsWith("SUCCESS"))
                        {
                            successCount++;
                        }
                    }
                }
            }

            if (successCount > 0)
            {
                string strMsg = successCount + " Records Batch Submitting Successfully With Document.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                InitializeTempTable(); // Reset temporary datatable structure
                BindInspectionGrid();
                gvFciRecords.DataSource = null;
                gvFciRecords.DataBind();
                ControlFinalSubmitPanelVisibility();
                TextClear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('Submission failed or records already exist.');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "System Error: " + ex.Message;
        }
    }

    protected string getDate_MDY(string inDate)
    {
        if (string.IsNullOrEmpty(inDate))
        {
            return "01/01/1919";
        }
        else
        {
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "M/d/yyyy", "dd MMM yyyy", "yyyy/MM/dd", "MM/dd/yyyy" };
            return DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
        }
    }
    private void BindInspectionGrid()
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            // Stored Procedure ka naam aur connection object pass karein
            using (SqlCommand cmd = new SqlCommand("sp_GetFciMoistureRecords", con))
            {
                // Batayein ki hum ek Stored Procedure use kar rahe hain, normal query nahi
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    try
                    {
                        con.Open();
                        sda.Fill(dt); // Data table me data fill ho raha hai

                        if (dt.Rows.Count > 0)
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Kisi error ki surat me message show karne ke liye
                        lblMsg.Text = "Error: " + ex.Message;
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        TextClear();
        InitializeTempTable();
        gvFciRecords.DataSource = null;
        gvFciRecords.DataBind();
        ControlFinalSubmitPanelVisibility();
    }

    protected void TextClear()
    {
        if (ddlGodown.Items.Count > 0) ddlGodown.SelectedIndex = 0;
        ddlStack.Items.Clear();
        ddlStack.Items.Insert(0, new ListItem("Select", "0"));
        if (ddlFCIChecked.Items.Count > 0) ddlFCIChecked.SelectedIndex = 0;
        txtfcidate.Text = "";
        txtfcimoisture.Text = "";
    }

    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        string ipaddress = "127.0.0.1";
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipaddress = ip.ToString();
            }
        }
        return ipaddress;
    }
}