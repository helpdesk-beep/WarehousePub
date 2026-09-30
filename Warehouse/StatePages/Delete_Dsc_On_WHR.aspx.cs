using AjaxControlToolkit;
using Data;
using DataAccess;
using MPSCSC_GodownDetails;
using System;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Resources;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class StatePages_Delete_Dsc_On_WHR : System.Web.UI.Page
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
            using (SqlCommand cmd = new SqlCommand("Get_WHR_DSC_DATA", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Whr_No", txtacceptanceno.Text);
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
    protected void btndlt_Click(object sender, EventArgs e)
    {
        try
        {
            string ipAddress;
            ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipAddress == "" || ipAddress == null)
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Delete_Dsc_WHR", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            cmd.Parameters.AddWithValue("@Whr_No", txtacceptanceno.Text);
            cmd.Parameters.AddWithValue("@IPAddress", ipAddress);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "WHR DSC Deleted Successfully |||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                fillgrid();
            }
            else
            {
                string strMsg = "Try Again......|||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
            }
        }
        catch(Exception ex)
        {
            string strMsg = "Somthing Wrong......|||";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + ex + "')", true);
        }
        }
        
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        fillgrid();
        if (GridView1.Rows.Count > 0)
        {
            // Show the button only if data is found
            btndlt.Visible = true;
        }
        else
        {
            // Keep it hidden if no records exist
            btndlt.Visible = false;
        }

    }

    //protected void Display(object sender, EventArgs e)
    //{
    //    int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
    //    GridViewRow row = GridView1.Rows[rowIndex];
    //    lblgodownname.Text = (row.FindControl("lblGodown_Name") as Label).Text;
    //    txtgodown_ID.Text = (row.FindControl("lblRecd_Godown") as Label).Text;
    //    txtAcceptance.Text = (row.FindControl("lblAcceptance_No") as Label).Text; ;
    //    txtAcdate.Text = (row.FindControl("lblAcceptance_Date") as Label).Text;
    //    txtrecdbags.Text = (row.FindControl("lblRecd_Bags") as Label).Text;
    //    txtacqty.Text = (row.FindControl("lblAcceptanceQty") as Label).Text;
    //    divNewInsp.Visible = true;
    //    //ModalPopupExtender1.Show();
    //}

}