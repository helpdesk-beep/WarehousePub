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

public partial class Reports_States_Rpt_JIT_Payment_Response_By_Date : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string PFID = "";
    SqlTransaction sqltran;
    int a_id = 0;
    SqlCommand cmd;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //fillGrid();
        }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    public void fillGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("JIT_Payment_Response_By_Date", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Trans_Status", ddlStatus.SelectedValue);
                cmd.Parameters.AddWithValue("@User_Type", "H");
                cmd.Parameters.AddWithValue("@Godown_Type", ddlGType.SelectedValue);
                cmd.Parameters.AddWithValue("@FromDate", txtFDate.Text);
                cmd.Parameters.AddWithValue("@ToDate", txtTDate.Text);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                        else
                        {

                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }
}