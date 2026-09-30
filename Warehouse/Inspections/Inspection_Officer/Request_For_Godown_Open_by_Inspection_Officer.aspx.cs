using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_Inspection_Officer_Request_For_Godown_Open_by_Inspection_Officer : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] != null) && (Session["UserId"] != null))
        {
            if (!IsPostBack)
            {
                fillGodown();
            }
        }
        else
        {
            // Agar session nahi milta toh is page par bhej dega
            Response.Redirect("~/Inspections/Default.aspx");
        }
    }

    private void fillGodown()
    {
        try
        {
            string query = "";
            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select Distinct ISB.Godown_ID,MG.Godown_Name from MPWLCInspection.dbo.Insp_Stack_Block_Wise_Godown_Entry ISB Inner join tbl_MetaData_GODOWN_2018 MG on ISB.Godown_ID COLLATE DATABASE_DEFAULT = MG.Godown_ID COLLATE DATABASE_DEFAULT  Where Emp_ID ='" + Session["UserId"].ToString() + "' And ISB.Godown_Submit='Y'  And NOT EXISTS (Select 1 from MPWLCInspection.dbo.Godown_Open_Request GOR Where GOR.Godown_ID COLLATE DATABASE_DEFAULT = ISB.Godown_ID COLLATE DATABASE_DEFAULT) Order By Godown_Name ASC";
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlGodown.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please select Godown');", true);
            return;
        }
        else if (ddlFinancialYear.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please select Financial Year');", true);
            return;
        }
        else if (ddlQuarter.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please select Inspection Quarter');", true);
            return;
        }
        else if (ddlVerificationType.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please select Verification Type');", true);
            return;
        }
        string godownId = ddlGodown.SelectedValue;
        string FinancialYear = ddlFinancialYear.SelectedValue;
        string InspectionQuarter = ddlQuarter.SelectedValue;
        string Verificationtype = ddlVerificationType.SelectedValue;
        string userId = Session["UserId"].ToString();
        string constr = ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        { 
            string query = @"INSERT INTO Godown_Open_Request
                        (Godown_Id, Requested_By,Financial_Year,Inspection_Quarter,Verification_Type)
                        VALUES (@Godown_Id, @Requested_By,@Financial_Year,@Inspection_Quarter,@Verification_Type)";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Godown_Id", godownId);
            cmd.Parameters.AddWithValue("@Requested_By", userId);
            cmd.Parameters.AddWithValue("@Financial_Year", FinancialYear);
            cmd.Parameters.AddWithValue("@Inspection_Quarter", InspectionQuarter);
            cmd.Parameters.AddWithValue("@Verification_Type", Verificationtype);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ddlGodown.ClearSelection();
            ddlFinancialYear.ClearSelection();
            ddlQuarter.ClearSelection();
            ddlVerificationType.ClearSelection();
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Request Sent To RM Successfully');", true);
    }
}