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
using System.Collections.Generic;

public partial class Inspections_RO_View_Annexure_B : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    string PFID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        PFID = Session["hdnemployeeid"].ToString();
        if (!IsPostBack)
        {
            fillBranchDetails();
            GetEmployeeInspectionDetails(PFID);
            // ddlbranch.SelectedValue=
            fillGodownDetails();
        }
    }
    public void GetEmployeeInspectionDetails(string PFID)
    {
        SqlCommand cmd = new SqlCommand("[dbo].[Get_Inspection_Quater_Details]", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Employee_ID", PFID);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Session["hdninspectionid"] = dt.Rows[0]["ID"].ToString();
            Session["hdnmonth"] = dt.Rows[0]["Inspection_month_ID"].ToString();
            Session["hdnquater"] = dt.Rows[0]["Inspection_type_ID"].ToString();
            Session["Order_Date"] = dt.Rows[0]["Order_Date"].ToString();
        }

    }
    public void fillBranchDetails()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Branch_Name_For_DF", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PFID_ID", PFID);
            con.Open();
            ddlbranch.DataSource = cmd.ExecuteReader();
            ddlbranch.DataTextField = "Depo_Name";
            ddlbranch.DataValueField = "Branch_ID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
            ddlbranch.SelectedValue = Session["hdnbranchid"].ToString();
            ddlbranch.Enabled = false;
            con.Close();
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
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodownDetails();
    }

    public void fillGodownDetails()
    {
        using (SqlConnection con2 = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_NameFor_Gadna_Patrak", con2);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
            con2.Open();
            ddl_gdwn.DataSource = cmd.ExecuteReader();
            ddl_gdwn.DataTextField = "Godown_Name";
            ddl_gdwn.DataValueField = "Godown_ID";
            ddl_gdwn.DataBind();
            ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con2.Close();
        }
    }


    public void fillScheduleInsp_Grid()
    {
        SqlCommand cmd = new SqlCommand("Get_Data_Annaxure_B", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
        cmd.Parameters.AddWithValue("@godown_ID", ddl_gdwn.SelectedValue);
        cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnemployeeid"].ToString());
        cmd.Parameters.AddWithValue("@Inspection_Quarter", Session["hdninsp_type_id"].ToString());
        cmd.Parameters.AddWithValue("@Order_no", Session["lblOrder_No"].ToString());
        cmd.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
        //cmd.Parameters.AddWithValue("@cropyear", ddlcropyear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GD_StackBal.Caption = @"<b style=""font-weight: bold;"">Godown Name: " + "  -   " + ddl_gdwn.SelectedItem.ToString() + "</b> ";
            divshow.Visible = true;
            btnPrint.Visible = true;
            GD_StackBal.DataSource = dt;
            GD_StackBal.DataBind();
            GD_StackBal.FooterRow.Style.Add("text-align", "center");
            GD_StackBal.FooterRow.Cells[5].Text = "Total";
            GD_StackBal.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Available_Bags")).ToString();
            GD_StackBal.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PV_Bags")).ToString();
            GD_StackBal.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Spillage_bags")).ToString();

        }
        else
        {
            divshow.Visible = false;
            GD_StackBal.DataSource = null;
            btnPrint.Visible = false;
            GD_StackBal.DataBind();
            string strMsg = "इस गोदाम की फाइनल एंट्री कर दी गई हैं यदि फाइनल एंट्री में कोई संशोधन करना हो तो कृपया RM ऑफिस से संपर्क करे  |";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "')", true);
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }
    protected void GD_StackBal_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GD_StackBal_RowCommand(object sender, GridViewCommandEventArgs e)
    {



    }
    protected void GD_StackBal_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
}