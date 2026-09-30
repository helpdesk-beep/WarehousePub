using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JointVentureScheme_DeleteJVSOffer_Rabi_2026_27 : System.Web.UI.Page
{
    private string connStr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        // Standard caching and session check
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();

        if (Session["UserName"] != null && Session["UserId"] != null)
        {
            if (!IsPostBack) { lbluser.Text = Session["UserName"].ToString(); }
        }
        else { Response.Redirect("Logins.aspx"); }
    }

    protected void fillgrid()
    {
        using (SqlConnection con = new SqlConnection(connStr))
        {
            // Grid 1 Data Retrieval
            using (SqlCommand cmd = new SqlCommand("Get_Offered_Godown_Rabi_2026_27", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Registration_Id", txtRegID.Text.Trim());
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    RegGrid.DataSource = dt;
                    RegGrid.DataBind();

                    ToggleSummary(true);
                    Label3.Text = dt.Rows.Count.ToString();

                    // Efficient Summation
                    decimal totalOffer = dt.AsEnumerable().Sum(r => r.Field<decimal>("Offer_Capacity"));
                    decimal totalReg = dt.AsEnumerable().Sum(r => r.Field<decimal>("RegCapacity"));

                    Label5.Text = totalOffer.ToString("N2");
                    RegGrid.FooterRow.Cells[1].Text = "Summary Total";
                    RegGrid.FooterRow.Cells[5].Text = totalReg.ToString("N2");
                    RegGrid.FooterRow.Cells[6].Text = totalOffer.ToString("N2");
                }
                else { ResetPage(); Alert("Registration ID not found."); }
            }

            // Grid 2 Data Retrieval
            using (SqlCommand cmd1 = new SqlCommand("Get_Godown_Offered_Capacity_Part_Wise_JVS_2026_27", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Registration_Id", txtRegID.Text.Trim());
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);

                if (dt1.Rows.Count > 0)
                {
                    divDetail.Visible = true;
                    GridView1.DataSource = dt1;
                    GridView1.DataBind();

                    decimal totalPart = dt1.AsEnumerable().Sum(r => r.Field<decimal>("G_OfferCapacity"));
                    GridView1.FooterRow.Cells[2].Text = "Grand Total";
                    GridView1.FooterRow.Cells[3].Text = totalPart.ToString("N2");
                }
                else { divDetail.Visible = false; }
            }
        }
    }

    private void ToggleSummary(bool visible)
    {
        Label2.Visible = Label3.Visible = Label4.Visible = Label5.Visible = Button1.Visible = visible;
    }

    private void ResetPage()
    {
        RegGrid.DataSource = null; RegGrid.DataBind();
        GridView1.DataSource = null; GridView1.DataBind();
        ToggleSummary(false); divDetail.Visible = false;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtRegID.Text)) fillgrid();
        else Alert("Please provide a Registration ID.");
    }

    protected void RegGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hdn = (HiddenField)RegGrid.Rows[e.RowIndex].FindControl("hdnOffer_Id");
        if (hdn != null) { DeleteRecord(hdn.Value); }
    }

    private void DeleteRecord(string offerId)
    {
        using (SqlConnection con = new SqlConnection(connStr))
        {
            SqlCommand cmd = new SqlCommand("Delete_Offered_Godown_Rabi_2026_27", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Offer_Id", offerId);
            cmd.Parameters.AddWithValue("@Deleted_By", Request.ServerVariables["REMOTE_ADDR"]);
            cmd.Parameters.AddWithValue("@JVSYear", "Rab2026_27");

            SqlParameter res = new SqlParameter("@TheResult", SqlDbType.VarChar, 250) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(res);

            con.Open();
            cmd.ExecuteNonQuery();
            Alert(res.Value.ToString());
            if (res.Value.ToString().StartsWith("SUCCESS")) { fillgrid(); }
        }
    }

    private void Alert(string msg) { ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", "alert('" + msg.Replace("'", "") + "');", true); }
    protected void LinkButton1_Click(object sender, EventArgs e) { Session.Abandon(); Response.Redirect("Logins.aspx"); }
    public override void VerifyRenderingInServerForm(Control control) { }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=OfferReport_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            toexport.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}