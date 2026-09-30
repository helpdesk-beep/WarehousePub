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

public partial class Inspections_RO_Allote_Branch_FOr_Employee : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            bindemploye();
            if (Session["hdnId"] != null)
            {
                fillinfo();
            }
        }
    }
    protected void bindemploye()
    {
        string strDist = "";
        strDist = "select PF_ID,Officer_Name from tbl_metadata_Inspection_officer";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlemployee.DataSource = ds.Tables[0];
            ddlemployee.DataTextField = "Officer_Name";
            ddlemployee.DataValueField = "PF_ID";
            ddlemployee.DataBind();
            ddlemployee.Items.Insert(0, new ListItem("--Select Officer Name--", "0"));
            con_WLC.Close();
        }
    }
    protected void fillinfo()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Fill_Branc_Allote_Text", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Generate_RosID", Session["hdnId"].ToString());
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                txtinspectiontype.Text = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Inspection_Status"].ToString()) ? ds.Tables[0].Rows[0]["Inspection_Status"].ToString() : "";
                                txtallotMonth.Text = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Month_Name"].ToString()) ? ds.Tables[0].Rows[0]["Month_Name"].ToString() : "";
                                txtverificationtype.Text = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["VerificationType_Stutes"].ToString()) ? ds.Tables[0].Rows[0]["VerificationType_Stutes"].ToString() : "";
                                txtregion.Text = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Regionnm"].ToString()) ? ds.Tables[0].Rows[0]["Regionnm"].ToString() : "";
                                txtdistrict.Text = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["District_Name"].ToString()) ? ds.Tables[0].Rows[0]["District_Name"].ToString() : "";
                                txtbranch.Text = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DepotName"].ToString()) ? ds.Tables[0].Rows[0]["DepotName"].ToString() : "";
                                txtfinancial.Text = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Financial_year"].ToString()) ? ds.Tables[0].Rows[0]["Financial_year"].ToString() : "";
                            }
                            else
                            {
                                txtinspectiontype.Text = "";
                                txtallotMonth.Text = "";
                                txtverificationtype.Text = "";
                                txtregion.Text = "";
                                txtdistrict.Text = "";
                                txtbranch.Text = "";
                                txtfinancial.Text = "";
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('!')", true);
        }
    }
    protected void btn_saveAllote_Branch_Click(object sender, EventArgs e)
    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        try
        {
            string ErrorMsg = "";
            ErrorMsg += ddlemployee.SelectedIndex > 0 ? "" : "Please Select Employee... \\n";
            if (ErrorMsg == "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Allote_Employee_For_Branch_Inspection_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Generate_RosID", Session["hdnId"].ToString());
                cmd.Parameters.AddWithValue("@Inspection_Type", txtinspectiontype.Text);
                cmd.Parameters.AddWithValue("@Allot_Month", txtallotMonth.Text);
                cmd.Parameters.AddWithValue("@Verification_Type", txtverificationtype.Text);
                cmd.Parameters.AddWithValue("@Region", txtregion.Text);
                cmd.Parameters.AddWithValue("@District", txtdistrict.Text);
                cmd.Parameters.AddWithValue("@Branch", txtbranch.Text);
                cmd.Parameters.AddWithValue("@PF_ID", ddlemployee.SelectedValue);
                cmd.Parameters.AddWithValue("@Financial_Year", txtfinancial.Text);
                cmd.Parameters.AddWithValue("@CreatedBy_Ip", ipAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Allot Officer For Branch Inspection Officer Successfully |||";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    Response.AppendHeader("Refresh", "1;url=Generate_Online_Roster_For_Inspections.aspx");
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}