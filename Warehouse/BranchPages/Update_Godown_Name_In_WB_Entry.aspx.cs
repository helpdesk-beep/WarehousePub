using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_Update_Godown_Name_In_WB_Entry : System.Web.UI.Page
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
            fillgrid();
        }
    }
    public void fillgrid()
    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("usp_GetWeightBridgeByBranchForUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchID"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                gvWB.DataSource = dt;
                gvWB.DataBind();
            }
            else
            {
                gvWB.DataSource = null;
                gvWB.DataBind();
            }
        }
    }
    protected void gvWB_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "UpdateWB")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            // WB_ID (varchar)
            string wbId = gvWB.DataKeys[rowIndex].Value.ToString();
            // TextBox se WB Name
            TextBox txtWB_Name = (TextBox)gvWB.Rows[rowIndex].FindControl("txtWB_Name");
            string wbName = txtWB_Name.Text.Trim();
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Update_WB_Name_By_WBID", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@WB_ID", SqlDbType.VarChar, 50).Value = wbId;
                    cmd.Parameters.Add("@WB_Name", SqlDbType.VarChar, 200).Value = wbName;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            string strMsg = "WB Name Updated Successfully";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
            //lblmsg.Visible = true;
            //lblmsg.Text = "WB Name Updated Successfully";
            //lblmsg.ForeColor = System.Drawing.Color.Green;
            fillgrid(); // Grid refresh
        }
    }
}