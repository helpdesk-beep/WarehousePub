using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JointVentureScheme_DeleteGodownInspection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Security headers
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();

        if (Session["UserName"] != null)
        {
            lblUser.Text = Session["UserName"].ToString();
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("Logins.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtSearch.Text) && txtSearch.Text.Length > 5 && ddl_session.SelectedValue != "--Select--")
        {
            fillgrid();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please provide Registration ID and select a Season.');", true);
        }
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Inspection", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Season", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@RegistrationId", txtSearch.Text.Trim());
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvGodown.DataSource = dt.Rows.Count > 0 ? dt : null;
                    gvGodown.DataBind();
                }
            }
        }
    }

    protected void gvGodown_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            Delete(e.CommandArgument.ToString());
        }
    }

    public void Delete(string InspectionID)
    {
        string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];

        using (SqlConnection con = new SqlConnection(CS))
        {
            SqlCommand cmd = new SqlCommand("Delete_JVS_Godown_Inspection", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InspectionID", InspectionID);
            cmd.Parameters.AddWithValue("@Season", ddl_session.SelectedValue);
            cmd.Parameters.AddWithValue("@DeletedBy", IPAddress);

            SqlParameter outRes = new SqlParameter("@TheResult", SqlDbType.VarChar, 250);
            outRes.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outRes);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                string result = cmd.Parameters["@TheResult"].Value.ToString();

                if (result.StartsWith("SUCCESS"))
                {
                    fillgrid();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('" + result + "');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('ERROR: " + result + "');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('System Error: " + ex.Message + "');", true);
            }
        }
    }
}