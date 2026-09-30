using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Accounting_ApprovedByNafedAccountByDate : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMonthDropdown();
        }
    }


    private void BindMonthDropdown()
    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Year_Account_New", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            if (dt.Rows.Count > 0)
            {
                ddlMonth.DataSource = dt;
                ddlMonth.DataTextField = "Bill_Year";   // dropdown में दिखेगा
                ddlMonth.DataValueField = "Bill_Year";  // value भी वही रहेगा
                ddlMonth.DataBind();

                // सबसे ऊपर default option डालने के लिए
                ddlMonth.Items.Insert(0, new ListItem("-- Select Year --", ""));
            }
            else
            {
                ddlMonth.Items.Clear();
                ddlMonth.Items.Insert(0, new ListItem("-- No Year Found --", ""));
            }
        }
    }



    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedValue == "-- Select Year --" || ddlMonth.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Account Approval Year')", true);
            return;
        }
        int month = Convert.ToInt32(ddlMonth.SelectedValue);
        if (month > 0)
        {
            BindGrid(month);
        System.Threading.Thread.Sleep(1000);
        }
        else
        {
            gvData.DataSource = null;
            gvData.DataBind();
        }
    }

    public void BindGrid(int month)
    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_Details_Approved_By_NafedAccountManagerByMonthWise_New", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Year", month);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                gvData.DataSource = dt;
                gvData.DataBind();
                
            }
            else
            {
                gvData.DataSource = null;
                gvData.DataBind();
            }
        }
    }
}
