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

public partial class StatePages_Get_Bill_Wise_Amount_Details_For_JIT : System.Web.UI.Page
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
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        string billNo = txtbillnumber.Text.Trim();

        DataTable dt1 = GetBillData("Get_Bill_Wise_Amount_Details", billNo);
        DataTable dt2 = GetBillData("Get_Bill_Wise_Amount_Details_For_Log", billNo);

        // Bind GridView1 if data found in first SP
        if (dt1 != null && dt1.Rows.Count > 0)
        {
            grdbill.DataSource = dt1;
            grdbill.DataBind();
            divinsititution.Visible = true;
        }
        else
        {
            divinsititution.Visible = false;
        }

        // Bind GridView2 if data found in second SP
        if (dt2 != null && dt2.Rows.Count > 0)
        {
            grdbilllog.DataSource = dt2;
            grdbilllog.DataBind();
            divlog.Visible = true;
        }
        else
        {
            divlog.Visible = false;
        }

        // Show message if both are empty
        if ((dt1 == null || dt1.Rows.Count == 0) && (dt2 == null || dt2.Rows.Count == 0))
        {
            lblmsg.Text = "No data found";
        }
        else
        {
            //lblmsg.Text = "";
        }
    }
    private DataTable GetBillData(string storedProcedure, string billNo)
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Bill_Number", billNo);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }
        return dt;
    }
}