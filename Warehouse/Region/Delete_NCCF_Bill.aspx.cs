using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

public partial class Region_Delete_NCCF_Bill : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Region_ID"] != null)
        {
            if (!IsPostBack)
            {
                //fillDistrict();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        gvBill.DataSource = null;
        gvBill.DataBind();

        if (string.IsNullOrWhiteSpace(txtBillNo.Text))
        {
            lblMessage.Text = "Bill Number डालें।";
            return;
        }

        using (SqlConnection con = new SqlConnection(conStr))
        using (SqlCommand cmd = new SqlCommand("Get_NCCF_Bill_For_Delete", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Bill_Number", txtBillNo.Text.Trim());
            cmd.Parameters.AddWithValue("@Bill_Type", ddlBillType.SelectedValue);
            cmd.Parameters.AddWithValue("@Region_ID", Session["Region_ID"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                //lblMessage.Text = "कोई Bill नहीं मिला।";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "showAlert('कोई Bill नहीं मिला।','warning'); hideLoader();", true);
            }
            else
            {
                gvBill.DataSource = dt;
                gvBill.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "hideLoader", "hideLoader();", true);
            }
        }
    }

    protected void gvBill_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteBill")
        {
            string billNo = e.CommandArgument.ToString();
            string deletedBy = Session["Region_ID"] != null ? Session["Region_ID"].ToString() : "";
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                using (SqlCommand cmd = new SqlCommand("Delete_NCCF_Bill", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Bill_Number", billNo);
                    cmd.Parameters.AddWithValue("@Bill_Type", ddlBillType.SelectedValue);
                    cmd.Parameters.AddWithValue("@DeletedBy", deletedBy);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "showAlert('Bill सफलतापूर्वक delete हो गया।','success');", true);
                //lblMessage.ForeColor = System.Drawing.Color.Green;
                //lblMessage.Text = "Bill सफलतापूर्वक delete हो गया।";

                gvBill.DataSource = null;
                gvBill.DataBind();
                txtBillNo.Text = "";
            }
            catch (SqlException ex)
            {
                //lblMessage.ForeColor = System.Drawing.Color.Red;
                //lblMessage.Text = ex.Message;   // DSC message
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "showAlert('" + ex.Message.Replace("'", "") + "','error');", true);
            }
        }
    }
}