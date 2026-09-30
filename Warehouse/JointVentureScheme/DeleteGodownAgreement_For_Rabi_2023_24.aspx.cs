using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JointVentureScheme_DeleteGodownAgreement_For_Rabi_2023_24 : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                // Initial logic if needed
            }
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
        // The logic here is called whether the user clicks the mouse 
        // or presses 'Enter' via the JavaScript trigger above.
        if (!string.IsNullOrEmpty(txtSearch.Text) && txtSearch.Text.Trim().Length > 5)
        {
            FillGrid();
            AgreementYear();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Enter a valid Registration ID');", true);
        }
    }

    protected void FillGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Agreement_For_Rabi_2023_24", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegistrationId", txtSearch.Text.Trim());
                cmd.Parameters.AddWithValue("@Season", ddl_session.SelectedValue);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvGodown.DataSource = dt;
                gvGodown.DataBind();
            }
        }
    }

    protected void AgreementYear()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Agreement_Year_Registration_ID", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegistrationId", txtSearch.Text.Trim());
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                grdjvsyear.DataSource = dt;
                grdjvsyear.DataBind();
            }
        }
    }

    protected void gvGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = gvGodown.SelectedRow;
        HiddenField hdnAgreementID = (HiddenField)gvr.FindControl("hdnAgreementID");
        if (hdnAgreementID != null)
        {
            DeleteAgreement(hdnAgreementID.Value);
        }
    }

    private void DeleteAgreement(string agreementID)
    {
        string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        using (SqlConnection con = new SqlConnection(CS))
        {
            using (SqlCommand cmd = new SqlCommand("Delete_Godown_Agreement_All_Year", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AgreementID", agreementID);
                cmd.Parameters.AddWithValue("@Season", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@DeletedBy", IPAddress);
                SqlParameter resultParam = new SqlParameter("@TheResult", SqlDbType.VarChar, 250) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(resultParam);

                con.Open();
                cmd.ExecuteNonQuery();
                string result = resultParam.Value.ToString();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('" + result + "');", true);
                if (result.StartsWith("SUCCESS")) FillGrid();
            }
        }
    }

    protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtSearch.Text.Length > 5) FillGrid();
    }
}