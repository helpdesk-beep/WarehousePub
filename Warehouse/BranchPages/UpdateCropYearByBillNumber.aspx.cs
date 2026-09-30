using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_UpdateCropYearByBillNumber : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid = "";
    SqlTransaction sqltran;
    string depottype = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string PopMsg = "";
            PopMsg = Request.QueryString["PopMsg"];
            if (Request.QueryString["PopMsg"] != null)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + PopMsg + "'); </script> ");
            }
            FillGrid();
        }
    }
    public void FillGrid()
    {
        DataTable dt = new DataTable();
        cmd = new SqlCommand("dbo.Get_Bill_For_Update_CropYear", con, sqltran);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            godown_GridView.DataSource = dt;
            godown_GridView.DataBind();
            Session["dsGodown"] = dt;

        }
    }
    protected void godown_GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        godown_GridView.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void godown_GridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        String BillNumber = (godown_GridView.SelectedRow.FindControl("hdnBillNumber") as HiddenField).Value;
        hdnBillNumber.Value = BillNumber;
    }
    protected void Edit(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            lblGodownID.Text = row.Cells[1].Text;
            hdnBillNumber.Value = row.Cells[3].Text;
            lblBillNumber.Text = row.Cells[3].Text;
            popup.Show();
        }
    }
    protected void Save(object sender, EventArgs e)
    {
        
        if (con != null)
        {
            con.Open();
            cmd = new SqlCommand("Update_Crop_Year_By_Bill_Number", con, sqltran);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BillNumber", lblBillNumber.Text);
            cmd.Parameters.AddWithValue("@CropYear", ddlChangeCropYear.SelectedValue);
            cmd.Parameters.AddWithValue("@UpdatedBy", Request.UserHostAddress);
            int res = cmd.ExecuteNonQuery();
            if (res > 0)
            {
                popup.Hide();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record updated successfully..')", true);
                FillGrid();
            }
            else
            {
                popup.Hide();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record NOT updated')", true);
            }
        }
    }
}