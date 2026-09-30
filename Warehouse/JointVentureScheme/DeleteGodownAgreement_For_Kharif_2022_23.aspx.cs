using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JointVentureScheme_DeleteGodownAgreement_For_Kharif_2022_23 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltran;
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        //string SessRegion = Session["UserName"].ToString();
        //string SessRegionid = Session["UserId"].ToString();
        //if (SessRegion != "" && SessRegionid != "")
        if (Session["UserName"] != null && Session["UserId"] != null)
        {
            if (!string.IsNullOrEmpty(Session["UserName"].ToString()) && !string.IsNullOrEmpty(Session["UserId"].ToString()))
            {
                if (!IsPostBack)
                {
                    //lbluser.Text = SessRegion;
                }
            }
            else
            {
                Response.Redirect("Logins.aspx");
            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text != "" && txtSearch.Text.Length > 5)
        {
            fillgrid();
            Agreementyear();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Registration ID .....')", true);
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Agreement_For_Kharif_2022_23", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegistrationId", txtSearch.Text);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            gvGodown.DataSource = dt;
                            gvGodown.DataBind();
                        }
                        else
                        {
                            gvGodown.DataSource = null;
                            gvGodown.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void Agreementyear()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Agreement_Year_Registration_ID", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegistrationId", txtSearch.Text);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            grdjvsyear.DataSource = dt;
                            grdjvsyear.DataBind();
                        }
                        else
                        {
                            grdjvsyear.DataSource = null;
                            grdjvsyear.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void gvGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = gvGodown.SelectedRow;
        HiddenField hdnAgreementID = (HiddenField)gvr.FindControl("hdnAgreementID");
        Delete(hdnAgreementID.Value);
    }
    public void Delete(string InspectionID)
    {
        string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        using (SqlConnection constr = new SqlConnection(CS))
        {
            SqlCommand cmd = new SqlCommand("Delete_Godown_Agreement", constr);
            cmd.CommandType = CommandType.StoredProcedure;
            constr.Open();
            cmd.Parameters.AddWithValue("@AgreementID", InspectionID);
            cmd.Parameters.AddWithValue("@DeletedBy", IPAddress);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = TheResult;
                fillgrid();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                //Clear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
            }
        }
    }
    public void Clear()
    {
        txtSearch.Text = "";
        gvGodown.DataSource = null;
        gvGodown.DataBind();
    }
}