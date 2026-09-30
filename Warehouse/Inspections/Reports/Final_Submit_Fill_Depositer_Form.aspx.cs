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

public partial class Inspections_Reports_Final_Submit_Fill_Depositer_Form : System.Web.UI.Page
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
    public void fillGodownDetails()
    {
        using (SqlConnection con2 = new SqlConnection(constr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Name_For_DF", con2);
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
    //public void fillGodownDetails()
    //{
    //    using (SqlConnection con = new SqlConnection(constr2))
    //    {
    //        SqlCommand cmd = new SqlCommand("Rpt_Get_Godown_Name_For_DF", con);
    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
    //        con.Open();
    //        ddl_gdwn.DataSource = cmd.ExecuteReader();
    //        ddl_gdwn.DataTextField = "Godown_Name";
    //        ddl_gdwn.DataValueField = "Godown_ID";
    //        ddl_gdwn.DataBind();
    //        ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
    //        con.Close();
    //    }
    //}

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
    public void GetdataForGrid()
    {
        SqlCommand cmd = new SqlCommand("[dbo].[Rpt_Get_Depositer_Entry_By_Inpection_Officer]", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Branch_id", ddlbranch.SelectedValue);
        cmd.Parameters.AddWithValue("@EmpID", PFID);
        cmd.Parameters.AddWithValue("@Inspection_Date", getDate_MDY(txt_inspdate.Text)); 
        cmd.Parameters.AddWithValue("@FinancialYear", ddlfinancialyear.SelectedValue);
        cmd.Parameters.AddWithValue("@GodownID", ddl_gdwn.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GD_StackBal.Caption = @"<b style=""font-weight: bold;"">Branch Name: " + "  -   " + dt.Rows[0]["DepotName"].ToString() + "</b> ";
            tr_griddata.Visible = true;
            divbtn.Visible = true;
            GD_StackBal.DataSource = dt;
            GD_StackBal.DataBind();
           // txt_inspdate.Text = dt.Rows[0]["Inspection_Date"].ToString();

        }
        else
        {
            tr_griddata.Visible = false;
            divbtn.Visible = false;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Depositer Form Details have been Finally SUbmitted by Inspection Officer Successfully')", true);
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        GetdataForGrid();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        // fillGodownDetails();
        fillGodownDetails();
    }
    public void Submit(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            foreach (GridViewRow row in GD_StackBal.Rows)
            {
                HiddenField hdngodownid = (HiddenField)row.FindControl("hdngodownid");
                Label lblWhr_No = (Label)row.FindControl("lblWhr_No");

                conStr.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[Depositer_Final_Submit_For_IO]", conStr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Godown_ID", hdngodownid.Value);
                cmd.Parameters.AddWithValue("@WHR_No", lblWhr_No.Text);
                cmd.Parameters.AddWithValue("@Emp_ID", PFID.ToString());
                cmd.Parameters.AddWithValue("@IO_Final_Submit", 1);
                cmd.Parameters.AddWithValue("@Createt_By", IPAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    count++;
                    tr_griddata.Visible = false;
                    divbtn.Visible = false;
                }
                conStr.Close();

            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Depositer Form Details Finally SUbmited')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Depositer Form Details Not SUbmited')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }
        finally
        {
            conStr.Close();
        }
    }
}