using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_Godown_Wise_Procurement_Information : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGodownType();
            GetCommodity();
            fillgrid();
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

                ddlgodowntype.DataSource = ds.Tables[0];
                ddlgodowntype.DataTextField = "Hired_Type";
                ddlgodowntype.DataValueField = "Hired_Type";
                ddlgodowntype.DataBind();
                ddlgodowntype.Items.Insert(0, "Select");
                con.Close();
            }
            else
            {
                ddlgodowntype.Items.Clear();
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlgodowntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodown();
    }
    private void fillGodown()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 Where BranchID ='" + Session["BranchId"].ToString() + "' And Hired_Type ='" + ddlgodowntype.SelectedItem.Text + "' Order By Godown_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
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
        catch (Exception)
        {
            //////
        }
    }
    public void GetCommodity()
    {
        string qry = "";
        qry = "select Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY Where Commodity_Id in ('22','33','63','64','65','75')  order by Commodity_Name asc";
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
            ddlcommodity.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string ErrorMsg = "";
            if (!string.IsNullOrEmpty(txtDate.Text))
            {
                getDate_MDY(txtDate.Text);
            }
            ErrorMsg += ddlgodowntype.SelectedIndex > 0 ? "" : "Please Select Godown Type... \\n";
            ErrorMsg += ddlGodown.SelectedIndex > 0 ? "" : "Please Select Godown Name... \\n";
            ErrorMsg += ddlcommodity.SelectedIndex > 0 ? "" : "Please Select Commodity... \\n";
            ErrorMsg += !string.IsNullOrEmpty(txttodaypurchaseBore.Text) ? "" : "आज दिनांक को भंडारित किये गये गये बोरे दर्ज करे. \\n";
            ErrorMsg += !string.IsNullOrEmpty(txttodaypurchaseWeight.Text) ? "" : "आज दिनांक को भंडारित किये गये बोरे का वजन दर्ज करे\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtaccepatance.Text) ? "" : "आज दिनांक को जारी स्वीक्रति पत्रक दर्ज करे\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtBore.Text) ? "" : "बोरे दर्ज करे \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtweight.Text) ? "" : "बोरे का वजन दर्ज करे \\n";
            ErrorMsg += !string.IsNullOrEmpty(txttotalbore.Text) ? "" : "आज दिनांक तक गोदाम में भंडारित टोटल बोरे दर्ज करे \\n";
            ErrorMsg += !string.IsNullOrEmpty(txttotalweight.Text) ? "" : "आज दिनांक तक गोदाम में भंडारित टोटल वजन \\n";
            if (ErrorMsg == "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                //string con = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
                SqlCommand cmd = new SqlCommand("usp_Insert_Godown_Wise_Procurement_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@GodownType_ID", ddlgodowntype.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Godown_Id", ddlGodown.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Commodity_ID", ddlcommodity.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Procurement_Date", getDate_MDY(txtDate.Text));
                cmd.Parameters.AddWithValue("@Godam_Bhandarit_Bore", txttodaypurchaseBore.Text);
                cmd.Parameters.AddWithValue("@Godam_me_Bhandarit_Bore_Ka_Weight", txttodaypurchaseWeight.Text);
                cmd.Parameters.AddWithValue("@Today_date_Acceptance", txtaccepatance.Text);
                cmd.Parameters.AddWithValue("@Acceptance_Wise_Bore", txtBore.Text);
                cmd.Parameters.AddWithValue("@Acceptance_Wise_Bore_Ka_Weight", txtweight.Text);
                cmd.Parameters.AddWithValue("@Total_Bore", txttotalbore.Text);
                cmd.Parameters.AddWithValue("@Total_Weight", txttotalweight.Text);
                cmd.Parameters.AddWithValue("@Create_By", Session["BranchId"].ToString());
                cmd.Parameters.AddWithValue("@CreatedBy_Ip", GetLocalIPAddress());
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Record Saved Successfully |||";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    Text_Clear();
                    fillgrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' " + ErrorMsg.ToString() + "')", true);
            }
        }
        catch
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
    protected void Text_Clear()
    {
        ddlgodowntype.ClearSelection();
        ddlGodown.ClearSelection();
        ddlcommodity.ClearSelection();
        txtDate.Text = "";
        txttodaypurchaseBore.Text = "";
        txttodaypurchaseWeight.Text = "";
        txtaccepatance.Text = "";
        txtBore.Text = "";
        txtweight.Text = "";
        txttotalbore.Text = "";
        txttotalweight.Text = "";
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Procurement_Details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            grdwhr.DataSource = dt;
                            grdwhr.DataBind();
                            Div1.Visible = true;
                        }
                        else
                        {
                            grdwhr.DataSource = null;
                            grdwhr.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void grdwhr_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {

            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            //Reference the GridView Row.
            GridViewRow row = grdwhr.Rows[rowIndex];
            //Fetch value of Name.
            string hdnGodown_Id = (row.FindControl("hdnGodown_Id") as HiddenField).Value;
            string hdnID = (row.FindControl("hdnID") as HiddenField).Value;
            string hdnCommodity_ID = (row.FindControl("hdnCommodity_ID") as HiddenField).Value;
            Session["hdnGodown_Id"] = hdnGodown_Id.ToString();
            Session["hdnID"] = hdnID.ToString();
            Session["hdnCommodity_ID"] = hdnCommodity_ID.ToString();
            RemoveRow(hdnGodown_Id.ToString(), hdnID.ToString(), hdnCommodity_ID.ToString());
        }
    }
    public void RemoveRow(string Godown_ID, string ID, string Commodity_Id)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlCommand cmd1 = new SqlCommand();
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            SqlCommand cmd = new SqlCommand("usp_Delete_Godown_Wise_Procurement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Godown_ID", Godown_ID);
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@Commodity_ID", Commodity_Id);
            cmd.Parameters.AddWithValue("@Delete_By", Session["BranchId"].ToString());
            cmd.Parameters.AddWithValue("@Deletedby_IP", GetLocalIPAddress());
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Deleted Successfully!');", true);
                fillgrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Deleted!');", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
}