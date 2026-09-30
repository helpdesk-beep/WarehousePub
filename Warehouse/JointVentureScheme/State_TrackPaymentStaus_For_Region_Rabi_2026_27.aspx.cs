using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JointVentureScheme_State_TrackPaymentStaus_For_Region_Rabi_2026_27 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbluser.Text = Session["UserName"] != null ? Session["UserName"].ToString() : "RM MPWLC";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtRegID.Text.Trim() != "" && txtRegID.Text.Trim().Length > 4)
        {
            gerreg();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "val", "alert('Please Enter Valid Registration ID');", true);
        }
    }

    public void gerreg()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Get_Payment_Details_State_Rebi_2026_27", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RegiNo", txtRegID.Text.Trim());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                RegGrid.DataSource = ds;
                RegGrid.DataBind();
            }
            else
            {
                RegGrid.DataSource = null;
                RegGrid.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "msg", "alert('No Data Found');", true);
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "err", "alert('Something Went Wrong');", true);
        }
    }

    protected void RegGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Conditional formatting for Registration Status
            Label lblReg = (Label)e.Row.FindControl("lblRegStatus");
            if (lblReg != null)
            {
                if (lblReg.Text.ToLower().Contains("pending"))
                    lblReg.CssClass = "status-pending";
                else if (lblReg.Text.ToLower().Contains("confirmed"))
                    lblReg.CssClass = "status-confirmed";
            }

            // Conditional formatting for Offer Status
            Label lblOffer = (Label)e.Row.FindControl("lblOfferStatus");
            if (lblOffer != null)
            {
                if (lblOffer.Text.ToLower().Contains("pending"))
                    lblOffer.CssClass = "status-pending";
                else if (lblOffer.Text.ToLower().Contains("confirmed"))
                    lblOffer.CssClass = "status-confirmed";
            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Logins.aspx");
    }
}