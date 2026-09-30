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
using System.Configuration;
using System;

public partial class Inspections_Inspection_Officer_Rpt_Fill_Depositer_Form : System.Web.UI.Page
{
    public SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    string PFID = "";
    SqlTransaction sqltran;
    string client_IP = "";
    int a_id = 0;
    string Branch_ID = "";
    string Insp_ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        PFID = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            fillBranchDetails();
            fillFinsncilYear();
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
    public void fillBranchDetails()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Rpt_Get_Branch_Name_For_DF", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PFID_ID", PFID);
            con.Open();
            ddlbranch.DataSource = cmd.ExecuteReader();
            ddlbranch.DataTextField = "Depo_Name";
            ddlbranch.DataValueField = "Branch_ID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
            con.Close();
        }
    }
    public void fillGodownDetails()
    {
        using (SqlConnection con = new SqlConnection(constr2))
        {
            SqlCommand cmd = new SqlCommand("Rpt_Get_Godown_Name_For_DF", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
            con.Open();
            ddl_gdwn.DataSource = cmd.ExecuteReader();
            ddl_gdwn.DataTextField = "Godown_Name";
            ddl_gdwn.DataValueField = "Godown_ID";
            ddl_gdwn.DataBind();
            ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con.Close();
        }
    }
    public void GetdataForGrid()
    {
        SqlCommand cmd = new SqlCommand("[dbo].[Get_Depositer_Entry_By_Inpection_Officer_For_Edit]", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Branch_id", ddlbranch.SelectedValue);
        cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
        cmd.Parameters.AddWithValue("@EmpID", PFID);
        cmd.Parameters.AddWithValue("@WHR_No", ViewState["Whr_No"].ToString());
        cmd.Parameters.AddWithValue("@ID", ViewState["hdnid"].ToString());
        cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
        cmd.Parameters.AddWithValue("@InspDate", getDate_MDY(txt_inspdate.Text));
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GD_StackBal.Caption = @"<b style=""font-weight: bold;"">Branch Name: " + "  -   " + dt.Rows[0]["DepotName"].ToString() + "</b> ";
            tr_griddata.Visible = true;
            GD_StackBal.DataSource = dt;
            GD_StackBal.DataBind();
            // txt_inspdate.Text = dt.Rows[0]["Inspection_Date"].ToString();

        }
        else
        {
            tr_griddata.Visible = false;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
        }
    }
    public void GetdataForGridsub()
    {
        SqlCommand cmd = new SqlCommand("[dbo].[Get_Depositer_Entry_By_Inpection_Officer]", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Branch_id", ddlbranch.SelectedValue);
        cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
        cmd.Parameters.AddWithValue("@EmpID", PFID);
        cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
        cmd.Parameters.AddWithValue("@InspDate", getDate_MDY(txt_inspdate.Text));
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GD_StackBal.Caption = @"<b style=""font-weight: bold;"">Branch Name: " + "  -   " + dt.Rows[0]["DepotName"].ToString() + "</b> ";
            tr_griddata.Visible = true;
            GD_StackBal.DataSource = dt;
            GD_StackBal.DataBind();
            // txt_inspdate.Text = dt.Rows[0]["Inspection_Date"].ToString();

        }
        else
        {
            tr_griddata.Visible = false;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        GetdataForGridsub();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodownDetails();
    }


    protected void GD_StackBal_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GD_StackBal_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        //Finding the controls from Gridview for the row which is going to update  
        HiddenField hdnid = GD_StackBal.Rows[e.RowIndex].FindControl("hdnid") as HiddenField;
        TextBox txt_DO_form_No = GD_StackBal.Rows[e.RowIndex].FindControl("txt_Depositer_form_No") as TextBox;
        TextBox txt_DO_Date = GD_StackBal.Rows[e.RowIndex].FindControl("txt_Depositer_Date") as TextBox;
        TextBox txtRemark = GD_StackBal.Rows[e.RowIndex].FindControl("txtRemark") as TextBox;

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Insp_Depositer_Form_Entry_Update", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        cmd.Parameters.AddWithValue("@ID", hdnid.Value);
        cmd.Parameters.AddWithValue("@deposit_form_No", txt_DO_form_No.Text);
        cmd.Parameters.AddWithValue("@Date_of_deposit_form", getDate_MDY(txt_DO_Date.Text));
        cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
        cmd.Parameters.AddWithValue("@Createt_By", ipAddress);
        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

        if (TheResult.StartsWith("SUCCESS"))
        {
            string strMsg = "Depositer Detail Update Successfully |||";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GD_StackBal.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            GetdataForGridsub();
        }

    }
    protected void GD_StackBal_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        GD_StackBal.EditIndex = -1;
        GetdataForGridsub();
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
    protected void GD_StackBal_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited.  \
        HiddenField hdnid = GD_StackBal.Rows[e.NewEditIndex].FindControl("hdnid") as HiddenField;
        Label lblWhr_No = GD_StackBal.Rows[e.NewEditIndex].FindControl("lblWhr_No") as Label;
        ViewState["Whr_No"] = lblWhr_No.Text;
        ViewState["hdnid"] = hdnid.Value;
        GD_StackBal.EditIndex = e.NewEditIndex;
        GetdataForGrid();
    }


}