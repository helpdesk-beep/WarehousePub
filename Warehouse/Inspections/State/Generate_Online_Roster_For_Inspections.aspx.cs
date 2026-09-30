using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

public partial class Inspections_State_Generate_Online_Roster_For_Inspections : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    public SqlConnection con_JVS = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string con_WLC2 = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    private object con;
    private object ob_value;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] != null))
        {
            if (!IsPostBack)
            {
                fillgrid();
                GetRegion();
                fillFinsncilYear();
                if (Session["UserName"].ToString() == "HOMPWLC")
                {
                    generateinspection.Columns[10].Visible = false;
                    divallot.Visible = true;
                }
                else
                {
                    generateinspection.Columns[10].Visible = true;
                    divallot.Visible = false;
                }
            }
        }
        else
        {
            Response.Redirect("~/Inspections/Default.aspx");
        }
    }
    public void fillFinsncilYear()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Fianancial_Year_For_inspection", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            ddlfinancialyear.DataSource = cmd.ExecuteReader();
            ddlfinancialyear.DataTextField = "Financial_Year";
            ddlfinancialyear.DataValueField = "Financial_Year";
            ddlfinancialyear.DataBind();
            ddlfinancialyear.Items.Insert(0, new ListItem("--Select Financial Year--", "0"));
            con.Close();
        }
    }
    private void GetRegion()
    {
        string strDist = "";
        strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlregion.DataSource = ds.Tables[0];
            ddlregion.DataTextField = "Regionnm";
            ddlregion.DataValueField = "Region_ID";
            ddlregion.DataBind();
            ddlregion.Items.Insert(0, new ListItem("--Select Region--", "0"));
            con_WLC.Close();
        }
    }
    private void GetDist(string RegionID)
    {
        string strDist = "";
        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT where Region_ID='" + RegionID + "' order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "--Select--");
        }
        else
        {
            ddldistrict.Items.Insert(0, "--Select--");
        }
    }
    private void GetBranch(string DistID)
    {
        string strBranch = "";
        strBranch = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + DistID + "' order by Depotname";
        SqlDataAdapter da = new SqlDataAdapter(strBranch, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "Depotname";
            ddlbranch.DataValueField = "BranchID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlbranch.Items.Insert(0, "--Select--");
        }
    }
    public void fillMonthQuarterWise()
    {
        using (SqlConnection con = new SqlConnection(con_WLC2))
        {
            SqlCommand cmd = new SqlCommand("Get_Month_Quater_Wise", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Quarter_ID", ddlquater.SelectedValue);
            con.Open();
            ddlmonth.DataSource = cmd.ExecuteReader();
            ddlmonth.DataTextField = "Month_Name";
            ddlmonth.DataValueField = "ID";
            ddlmonth.DataBind();
            ddlmonth.Items.Insert(0, new ListItem("-- Select Month --", "0"));
            con.Close();
        }
    }
    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDist(ddlregion.SelectedValue.ToString());
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch(ddldistrict.SelectedValue.ToString());
    }
    protected void ddlquater_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillMonthQuarterWise();
    }
    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];

        try
        {
            string ErrorMsg = "";
            if (!string.IsNullOrEmpty(txt_InspDate.Text))
            {
                getDate_MDY(txt_InspDate.Text);
            }
            ErrorMsg += ddlquater.SelectedIndex > 0 ? "" : "Please Select Quarter... \\n";
            ErrorMsg += ddlmonth.SelectedIndex > 0 ? "" : "Please Select Month... \\n";
            ErrorMsg += ddlverification.SelectedIndex > 0 ? "" : "Please Select Verification Type... \\n";
            ErrorMsg += ddlregion.SelectedIndex > 0 ? "" : "Please Select Region... \\n";
            ErrorMsg += ddldistrict.SelectedIndex > 0 ? "" : "Please Select District... \\n";
            ErrorMsg += ddlbranch.SelectedIndex > 0 ? "" : "Please Select Branch... \\n";
            ErrorMsg += ddlfinancialyear.SelectedIndex > 0 ? "" : "Please Select Financial Year... \\n";
            ErrorMsg += !string.IsNullOrEmpty(txt_OrderNo.Text) ? "" : "Enter Order No \\n";
            if (ErrorMsg == "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Generate_Online_Roster_For_Inspection_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Inspection_type_ID", ddlquater.SelectedValue);
                cmd.Parameters.AddWithValue("@Inspection_month_ID", ddlmonth.SelectedValue);
                cmd.Parameters.AddWithValue("@Verification_Type", ddlverification.SelectedValue);
                cmd.Parameters.AddWithValue("@Region_ID", ddlregion.SelectedValue);
                cmd.Parameters.AddWithValue("@District_ID", ddldistrict.SelectedValue);
                cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
                cmd.Parameters.AddWithValue("@Financial_year", ddlfinancialyear.SelectedValue);
                cmd.Parameters.AddWithValue("@Order_No", txt_OrderNo.Text);
                cmd.Parameters.AddWithValue("@Order_Date", getDate_MDY(txt_InspDate.Text));
                cmd.Parameters.AddWithValue("@IP_Adress", ipAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Generate Online Roster For Inspection Successfully |||";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    fillgrid();
                    ClearField();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    ClearField();
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
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    protected void fillgrid()
    {
        using (SqlConnection con = new SqlConnection(con_WLC2))
        {
            SqlCommand cmd = new SqlCommand("Select_GenerateInspection", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                generateinspection.DataSource = dt;
                generateinspection.DataBind();
            }
            else
            {
                generateinspection.DataSource = null;
                generateinspection.DataBind();
            }
        }
    }
    protected void ClearField()
    {
        ddlquater.ClearSelection();
        ddlmonth.ClearSelection();
        ddlverification.ClearSelection();
        ddlregion.ClearSelection();
        ddldistrict.ClearSelection();
        ddlbranch.ClearSelection();
        ddlfinancialyear.ClearSelection();
        txt_OrderNo.Text = "";
        txt_InspDate.Text = "";
    }
    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = generateinspection.Rows[rowIndex];
        string hdnId = (row.FindControl("hdnId") as HiddenField).Value;
        Session["hdnId"] = hdnId.ToString();
        GetDist(Session["UserId"].ToString());
        Response.Redirect("~/Inspections/State/Allot_Branch_Inspection.aspx");
    }
    protected void generateinspection_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            //Reference the GridView Row.
            GridViewRow row = generateinspection.Rows[rowIndex];
            string hdnID = (row.FindControl("hdnID") as HiddenField).Value;
            //string hdnRid = (row.FindControl("hdnRid") as HiddenField).Value;
            Session["hdnID"] = hdnID.ToString();
            //Session["hdnRid"] = hdnRid.ToString();
            RemoveRowFromDatabase(hdnID);
        }
    }
    public void RemoveRowFromDatabase(string hdnid)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Delete_Generate_Online_Roster_For_Inspection_Entry", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Generate_RosID", hdnid.ToString());
            cmd.Parameters.AddWithValue("@Deleted_By", Session["UserId"].ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                fillgrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
}