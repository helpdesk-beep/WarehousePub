using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_Godown_Wise_Dcc_Stock_Entry : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    string fileupload1 = "";
    string fileupload2 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Form.Enctype = "multipart/form-data";
        if (!IsPostBack)
        {
            fillGodownType();
            fillCropYear();
            GetDepositorType();
            GetCommodityType();
        }
    }

    private void fillGodownType()
    {
        try
        {

            string query = "";
            query = "Select  Distinct Hired_Type from tbl_MetaData_GODOWN_2018 Order By Hired_Type";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodowntype.Items.Clear();
                ddlGodowntype.DataSource = ds.Tables[0];
                ddlGodowntype.DataTextField = "Hired_Type";
                ddlGodowntype.DataValueField = "Hired_Type";
                ddlGodowntype.DataBind();
                ddlGodowntype.Items.Insert(0, "Select");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    protected void ddlGodowntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodown();
    }
    private void fillGodown()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 Where BranchID ='" + Session["BranchId"].ToString() + "' And Hired_Type ='" + ddlGodowntype.SelectedItem.Text + "' Order By Godown_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodown.Items.Clear();
                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
                ddlGodown.Items.Insert(0, "Select");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void fillCropYear()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select Crop_Year from tbl_MetaData_Crop_Year Order By Crop_Year ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcropyear.Items.Clear();
                ddlcropyear.DataSource = ds.Tables[0];
                ddlcropyear.DataTextField = "Crop_Year";
                ddlcropyear.DataValueField = "Crop_Year";
                ddlcropyear.DataBind();
                ddlcropyear.Items.Insert(0, "Select");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void GetDepositorType()
    {
        string qry = "";
        qry = "select Depositor_Type_Id,Depositor_Type from tbl_MetaData_Depositor_Type order by Depositor_Type";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDepositorType.DataSource = ds.Tables[0];
            ddlDepositorType.DataTextField = "Depositor_Type";
            ddlDepositorType.DataValueField = "Depositor_Type_Id";
            ddlDepositorType.DataBind();
            ddlDepositorType.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {

        }
    }
    protected void ddlDepositorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepositor();
    }
    private void GetDepositor()
    {
        if (ddlDepositorType.SelectedItem.Text == "Institution")
        {
            string query2 = "";
            query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679','181')";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddlDepositor.DataSource = ds2;
                ddlDepositor.DataTextField = "Depositor_Name";
                ddlDepositor.DataValueField = "Depositor_ID";
                ddlDepositor.DataBind();
                ddlDepositor.Items.Insert(0, "Select");
            }
        }
        else
        {
            string depositer = ddlDepositorType.SelectedValue.ToString().Trim();
            string qry = "";
            qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE DepositorType_ID ='" + ddlDepositorType.SelectedValue + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepositor.DataSource = ds.Tables[0];
                ddlDepositor.DataTextField = "Depositor_Name";
                ddlDepositor.DataValueField = "Depositor_ID";
                ddlDepositor.DataBind();
                ddlDepositor.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
    }
    public void GetCommodityType()
    {
        string qry = "";
        qry = "Select Comm_Group_id,Group_name from tbl_MetaData_STORAGE_COMMODITY_Group Order By Group_name ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlCommoditytype.DataSource = ds.Tables[0];
            ddlCommoditytype.DataTextField = "Group_name";
            ddlCommoditytype.DataValueField = "Comm_Group_id";
            ddlCommoditytype.DataBind();
            ddlCommoditytype.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {

        }
    }
    protected void ddlCommoditytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCommodity();
    }
    public void GetCommodity()
    {
        string qry = "";
        qry = "select Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY Where  Rep_Grp_Code='" + ddlCommoditytype.SelectedValue + "' order by Commodity_Name asc";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {

        }
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string result1 = DccImage();
            string result2 = TenderImage();
            string ErrorMsg = "";
            if (!string.IsNullOrEmpty(txtDate.Text))
            {
                getDate_MDY(txtDate.Text);
            }
            ErrorMsg += ddlGodowntype.SelectedIndex > 0 ? "" : "Please Select Godown Type... \\n";
            ErrorMsg += ddlGodown.SelectedIndex > 0 ? "" : "Please Select Godown... \\n";
            ErrorMsg += ddlcropyear.SelectedIndex > 0 ? "" : "Please Select Crop-Year... \\n";
            ErrorMsg += ddlDepositorType.SelectedIndex > 0 ? "" : "Please Select Depositor Type... \\n";
            ErrorMsg += ddlDepositor.SelectedIndex > 0 ? "" : "Please Select Depositor... \\n";
            ErrorMsg += ddlCommoditytype.SelectedIndex > 0 ? "" : "Please Select Commodity Type... \\n";
            ErrorMsg += ddlcommodity.SelectedIndex > 0 ? "" : "Please Select Commodity... \\n";
            ErrorMsg += ddldccStock.SelectedIndex > 0 ? "" : "Please Select DCC Stock... \\n";
            ErrorMsg += !string.IsNullOrEmpty(txttotalweight.Text) ? "" : "Enter Total Weight \\n";
            ErrorMsg += !string.IsNullOrEmpty(txttotalBags.Text) ? "" : "Enter Total Bags \\n";
            if (ErrorMsg == "")
            {
                if (btnsave.Text == "SUBMIT")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Usp_Insert_BranchWise_DCC_Stock", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Godown_Type_ID", ddlGodowntype.SelectedValue);
                    cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue);
                    cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
                    cmd.Parameters.AddWithValue("@Depositor_Type_Id", ddlDepositorType.SelectedValue);
                    cmd.Parameters.AddWithValue("@Depositor_ID", ddlDepositor.SelectedValue);
                    cmd.Parameters.AddWithValue("@Commodity_Type_Id", ddlCommoditytype.SelectedValue);
                    cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
                    cmd.Parameters.AddWithValue("@Total_Bags", txttotalBags.Text);
                    cmd.Parameters.AddWithValue("@Total_Weight", string.IsNullOrEmpty(txttotalweight.Text) ? "0" : txttotalweight.Text);
                    cmd.Parameters.AddWithValue("@Prastavit_DCC_Stock", ddldccStock.SelectedValue);
                    cmd.Parameters.AddWithValue("@DCC_Stock", string.IsNullOrEmpty(txtdccstock.Text) ? "0" : txtdccstock.Text);
                    cmd.Parameters.AddWithValue("@District_Manager", ddldistrictmanager.SelectedValue);
                    cmd.Parameters.AddWithValue("@Date", getDate_MDY(txtDate.Text));
                    cmd.Parameters.AddWithValue("@File_Upload", fileupload1);
                    cmd.Parameters.AddWithValue("@physicalverification", ddlphysicalverification.SelectedValue);
                    cmd.Parameters.AddWithValue("@decisionDate", getDate_MDY(txtdecision.Text));
                    cmd.Parameters.AddWithValue("@totaldccquantity", string.IsNullOrEmpty(txtDCC.Text) ? "0" : txtDCC.Text);
                    cmd.Parameters.AddWithValue("@UpgradableQty", string.IsNullOrEmpty(txtUpgradableQty.Text) ? "0" : txtUpgradableQty.Text);
                    cmd.Parameters.AddWithValue("@DumpingQty", string.IsNullOrEmpty(txtDumpingQty.Text) ? "0" : txtDumpingQty.Text);
                    cmd.Parameters.AddWithValue("@tenderstutes", ddltenderstutes.SelectedValue);
                    cmd.Parameters.AddWithValue("@TenderFileUpload", ViewState["TenderUpload"].ToString());
                    cmd.Parameters.AddWithValue("@weight", string.IsNullOrEmpty(txtweight.Text) ? "0" : txtweight.Text);
                    cmd.Parameters.AddWithValue("@StockDelivered", ddlStockDelivered.SelectedValue);
                    cmd.Parameters.AddWithValue("@Created_By", Session["BranchId"].ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy_Ip", GetLocalIPAddress());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "DCC Stock Insert Successfully |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        //FillGrid();
                        //TextClear();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                        //TextClear();
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' " + ErrorMsg.ToString() + "')", true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        String ipaddress = string.Empty;
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipaddress = ip.ToString();
            }
        }
        return ipaddress.Length > 0 ? ipaddress : null;
    }
    protected void ddldccStock_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldccStock.SelectedValue == "1")
        {
            divdccstock.Visible = true;
            district.Visible = true;
        }
        else
        {
            divdccstock.Visible = false;
            district.Visible = false;
        }
    }
    protected void ddldistrictmanager_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldistrictmanager.SelectedValue == "1")
        {
            divdate.Visible = true;
            divsamiti.Visible = true;
        }
        else
        {
            divdate.Visible = false;
            divsamiti.Visible = false;
        }
    }
    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Stock_Position_Entry_By_BM", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CropYear", ddlcropyear.SelectedValue);
                cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
                cmd.Parameters.AddWithValue("@Depositor_ID", ddlDepositor.SelectedValue);
                cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            txttotalBags.Text = dt.Rows[0]["TotalNoOfBagsAvailable"].ToString();
                            txttotalweight.Text = dt.Rows[0]["TotalQuantityAvailable"].ToString();
                            fielddcc.Visible = true;
                            opennull.Visible = false;
                        }
                        else
                        {
                            fielddcc.Visible = false;
                            opennull.Visible = true;
                        }
                    }
                }
            }
        }
    }
    protected void chkDCC_CommiteeDecision_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDCC_CommiteeDecision.Checked)
        {
            txtDCC.Visible = true;
            txtDCC.Enabled = true;
            totaldccquantity.Visible = true;
            divdccTenderStutes.Visible = true;
        }
        else
        {
            txtDCC.Visible = false;
            txtDCC.Enabled = false;
            totaldccquantity.Visible = false;
            divdccTenderStutes.Visible = false;
        }
    }
    protected void chkUpgradableCommiteeDecision_CheckedChanged(object sender, EventArgs e)
    {
        if (chkUpgradableCommiteeDecision.Checked)
        {
            txtUpgradableQty.Visible = true;
            txtUpgradableQty.Enabled = true;
            totalUpgradable.Visible = true;
        }
        else
        {
            txtUpgradableQty.Visible = false;
            txtUpgradableQty.Enabled = false;
            totalUpgradable.Visible = false;
        }
    }
    protected void chkDumping_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDumping.Checked)
        {
            txtDumpingQty.Visible = true;
            txtDumpingQty.Enabled = true;
            totalDumping.Visible = true;
        }
        else
        {
            txtDumpingQty.Visible = false;
            txtDumpingQty.Enabled = false;
            totalDumping.Visible = false;
        }
    }
    protected void ddlphysicalverification_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlphysicalverification.SelectedValue == "1")
        {
            divdisi.Visible = true;
        }
        else
        {
            divdisi.Visible = false;
        }
    }
    protected string DccImage()
    {
        string result = "";
        try
        {
            string strFileName = "", strExtension = "", strTimeStamp = "";

            if (IdFileUpload.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {
                int iFailedCntExt = 0;
                int iFailedCntSize = 0;
                string fileExt = System.IO.Path.GetExtension(IdFileUpload.FileName).Substring(1);
                string[] supportedTypes = { "pdf", "PDF" };
                if (!supportedTypes.Contains(fileExt))
                {
                    iFailedCntExt += 1;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert ('Nursery Image Should be In PDF Format Only')", true);
                    result = " Image Should be In PDF Format Only";
                }
                else if (IdFileUpload.PostedFile.ContentLength > 512000) // 100 KB = 1024 * 100
                {
                    iFailedCntSize += 1;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert ('Nursery Image Should be Under 500KB')", true);
                    result = "PDF Should be Under 500KB";
                }
                else
                {
                    strFileName = IdFileUpload.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "");
                    strTimeStamp = strTimeStamp.Replace(" ", "");
                    strTimeStamp = strTimeStamp.Replace(":", "");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../BranchPages/Docoment_Upload/"), strFileName);
                    IdFileUpload.SaveAs(path);
                    ViewState["Docoment_Upload"] = strFileName;
                    fileupload1 = ViewState["Docoment_Upload"].ToString();
                    path = "";
                    strFileName = "";
                    strName = "";
                }
            }
            else
            {
                strFileName = ddlGodown.SelectedValue + DateTime.Now.ToString("dd_MM_yyyy") + ".pdf";
                fileupload1 = strFileName;
                string path3 = Path.Combine(Server.MapPath("../BranchPages/Docoment_Upload/"), strFileName);
                if (File.Exists(path3))
                {
                    File.Delete(path3);
                }
            }
        }
        catch
        {
            throw;
        }
        return result;
    }
    protected string TenderImage()
    {
        string result = "";
        try
        {
            string strFileName = "", strExtension = "", strTimeStamp = "";

            if (TenderFileUpload.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {
                int iFailedCntExt = 0;
                int iFailedCntSize = 0;
                string fileExt = System.IO.Path.GetExtension(TenderFileUpload.FileName).Substring(1);
                string[] supportedTypes = { "pdf", "PDF" };
                if (!supportedTypes.Contains(fileExt))
                {
                    iFailedCntExt += 1;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert ('Nursery Image Should be In PDF Format Only')", true);
                    result = " Image Should be In PDF Format Only";
                }
                else if (TenderFileUpload.PostedFile.ContentLength > 512000) // 100 KB = 1024 * 100
                {
                    iFailedCntSize += 1;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert ('Nursery Image Should be Under 500KB')", true);
                    result = "PDF Should be Under 500KB";
                }
                else
                {
                    strFileName = TenderFileUpload.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "");
                    strTimeStamp = strTimeStamp.Replace(" ", "");
                    strTimeStamp = strTimeStamp.Replace(":", "");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../BranchPages/TenderUpload/"), strFileName);
                    TenderFileUpload.SaveAs(path);
                    ViewState["TenderUpload"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                }
            }
            else
            {
                strFileName = ddlGodown.SelectedValue + DateTime.Now.ToString("dd_MM_yyyy") + ".pdf";
                ViewState["TenderUpload"] = strFileName;
                string path3 = Path.Combine(Server.MapPath("../BranchPages/TenderUpload/"), strFileName);
                if (File.Exists(path3))
                {
                    File.Delete(path3);
                }
            }
        }
        catch
        {
            throw;
        }
        return result;
    }
}