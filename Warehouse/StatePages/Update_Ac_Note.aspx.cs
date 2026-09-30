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
using MPSCSC_GodownDetails;

public partial class StatePages_Update_Ac_Note : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    MPSCSC_GodownDetails.MPSCSC_AddGodownDetails GodownDetailsDemo = new MPSCSC_GodownDetails.MPSCSC_AddGodownDetails();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["State_Logid"].ToString() != null)
        {
            if (!IsPostBack)
            {
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Acceptance_Data", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Acceptance_No", txtacceptanceno.Text);
                cmd.Parameters.AddWithValue("@SessionYear", ddlcropyear.SelectedValue.ToString());
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
                            Session["Session"] = ddlcropyear.SelectedValue.ToString();
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
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = GridView1.Rows[rowIndex];

        lblgodownname.Text = (row.FindControl("lblGodown_Name") as Label).Text;
        txtgodown_ID.Text = (row.FindControl("lblRecd_Godown") as Label).Text;
        txtAcceptance.Text = (row.FindControl("lblAcceptance_No") as Label).Text; ;
        txtAcdate.Text = (row.FindControl("lblAcceptance_Date") as Label).Text;
        txtrecdbags.Text = (row.FindControl("lblRecd_Bags") as Label).Text;
        txtacqty.Text = (row.FindControl("lblAcceptanceQty") as Label).Text;
        divNewInsp.Visible = true;
        ModalPopupExtender1.Show();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
            cmd = new SqlCommand("Update_Acceptance_Data", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@SessionYear", Session["Session"].ToString());
            cmd.Parameters.AddWithValue("@Acceptance_No", txtAcceptance.Text.Trim());
            cmd.Parameters.AddWithValue("@Recd_Godown", txtgodown_ID.Text.Trim());
            cmd.Parameters.AddWithValue("@Acceptance_Date", getDate_MDY(txtAcdate.Text.Trim()));
            cmd.Parameters.AddWithValue("@AcceptanceQty", txtacqty.Text.Trim());
            cmd.Parameters.AddWithValue("@Recd_Bags", txtrecdbags.Text.Trim());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            TheResult = cmd.Parameters["@TheResult"].Value.ToString();
            if (TheResult.StartsWith("SUCCESS"))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Update Successfully')", true);
                fillgrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Acceptance Received')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error')", true);
        }
        finally
        {
            con.Close();
        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
}