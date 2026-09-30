using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Drawing;

public partial class Inspections_State_Update_Gadna_Patrak : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd;
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        lbl_user.Text = Session["UserName"].ToString();
        if (!IsPostBack)
        {
            //fillInpOff_Grid();
            GetDist();
        }
    }
    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillInpOff_Grid();

    }

    private void GetDist()
    {
        string strDist = "";
        strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Regionnm";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_dist.DataSource = ds.Tables[0];
            ddl_dist.DataTextField = "Regionnm";
            ddl_dist.DataValueField = "Region_ID";
            ddl_dist.DataBind();
            ddl_dist.Items.Insert(0, "--Select--");
        }
        else
        {
            ddl_dist.Items.Insert(0, "--Select--");
        }
    }
    public void fillInpOff_Grid()
    {
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();

            }
            SqlCommand cmd = new SqlCommand("Get_Data_For_Update_Gadna_Patrak", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Region_ID", ddl_dist.SelectedValue);
            cmd.Parameters.AddWithValue("@Inspection_type_ID", ddlquater.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { conStr.Close(); }

            if (dt.Rows.Count > 0)
            {
                GrdOfficerPreviousInsp.DataSource = dt;
                GrdOfficerPreviousInsp.DataBind();
                lblOfficerList.Text = Convert.ToString(dt.Rows.Count);
            }
            else
            {
                GrdOfficerPreviousInsp.DataSource = null;
                GrdOfficerPreviousInsp.DataBind();
                lblOfficerList.Text = "0";
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
        finally
        { if (conStr.State == ConnectionState.Open) { conStr.Close(); } }
    }

    protected void GrdOfficerPreviousInsp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       

    }

    protected void GrdOfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Annexure_A")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];
            //Fetch value of Name.           
            string hdnemployeeid = (row.FindControl("hdnemployeeid") as HiddenField).Value;
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnorderdate = (row.FindControl("lblOrder_Date") as Label).Text;
            string hdnVerificationType = (row.FindControl("hdnVerificationType") as HiddenField).Value;
            string hdninsp_type_id = (row.FindControl("hdnInspection_ID") as HiddenField).Value;
            string lblOrder_No = (row.FindControl("lblOrder_No") as Label).Text;
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnemployeeid"] = hdnemployeeid.ToString();
            Session["hdnorderdate"] = hdnorderdate.ToString();
            Session["hdnVerificationType"] = hdnVerificationType.ToString();
            Session["hdninsp_type_id"] = hdninsp_type_id.ToString();
            Session["lblOrder_No"] = lblOrder_No.ToString();
            UpdateAnnexure_A(hdnbranchid, hdnemployeeid, lblOrder_No,hdnVerificationType);
        }
       
    }
    public void UpdateAnnexure_A(string BranchID, string EmployeeID,string Orderno,string Verificationtype)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {


            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();
            }

            SqlCommand cmd = new SqlCommand("Update_Gadna_Patrak", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", BranchID.ToString());
            cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID.ToString());
            cmd.Parameters.AddWithValue("@Inspection_Quarter", ddlquater.SelectedValue);
            cmd.Parameters.AddWithValue("@Order_no", Orderno.ToString());
            cmd.Parameters.AddWithValue("@Verification_Type", Verificationtype.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Gadna Patrak Updated in Annexure_A Successfully|||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                fillInpOff_Grid();
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        
            fillInpOff_Grid();
        
    }

}
