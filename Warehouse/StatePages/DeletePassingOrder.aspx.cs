using System;
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Diagnostics;
using System.Resources;

public partial class StatePages_DeletePassingOrder : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                // filldepositer();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Delete_Passing_Order", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        cmd.Parameters.AddWithValue("@JVS_Bill_Number", txttjvsbillno.Text);
        cmd.Parameters.AddWithValue("@IPAddress", ipAddress);
        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

        if (TheResult.StartsWith("SUCCESS"))
        {
            string strMsg = "Passing Order Deleted Successfully |||";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
        }
        else
        {
            string strMsg = "कृप्या पहले RM की DSC डिलीट करे, उसके बाद पासिंग आर्डर डिलीट होगा |||";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
        }
    }

    protected void fill_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Passing_Order_For_Delete", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@JVS_Bill_Number", txttjvsbillno.Text.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Passing_Gridview.DataSource = dt;
                            Passing_Gridview.DataBind();
                            divdelete.Visible = true;
                        }
                        else
                        {
                            Passing_Gridview.DataSource = null;
                            Passing_Gridview.DataBind();
                            divdelete.Visible = false;
                        }
                    }
                }
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        fill_Grid();
    }
}
