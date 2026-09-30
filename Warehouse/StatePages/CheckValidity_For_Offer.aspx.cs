using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class StatePages_CheckValidity_For_Offer : System.Web.UI.Page
{
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string regID = txtRegistrationID.Text.Trim();

        if (string.IsNullOrEmpty(regID))
            return;

        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Licence_Validity_For_offer", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Registration_ID", regID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvLicence.DataSource = dt;
                gvLicence.DataBind();
            }
        }
    }
}