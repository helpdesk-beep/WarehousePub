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

public partial class BranchPages_UpdateVacantCapacity : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlTransaction sqltran;
    string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGrid();
        }
    }
    public void FillGrid()
    {
        DataTable dt = new DataTable();
        cmd = new SqlCommand("Get_tbl_Godown_Vacant_Capacity_Kharif2020", con, sqltran);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BranchId", Session["BranchID"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        lblRowCount.Text = "Total records are : " + dt.Rows.Count.ToString();
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
        String GodownID = (godown_GridView.SelectedRow.FindControl("hdnGodownID") as HiddenField).Value;
        hdnGodownID.Value = GodownID;
    }
    protected void Edit(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            hdnGodownID.Value = row.Cells[1].Text;
            lblGodownID.Text = row.Cells[1].Text;
            txtVacantCapacity.Text= row.Cells[4].Text;
            txtUnloadCapacity.Text = row.Cells[5].Text;
            popup.Show();
        }
    }
    protected void Save(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txtUnloadCapacity.Text))
        {
            txtUnloadCapacity.Text = "0";
        }
        if (String.IsNullOrEmpty(txtVacantCapacity.Text))
        {
            txtUnloadCapacity.Text = "0";
        }
        if (con != null)
        {
            con.Open();
            cmd = new SqlCommand("Update_Vacant_Capacity_Kharif2020", con, sqltran);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VacantCapacity", Convert.ToDecimal(txtVacantCapacity.Text));
            cmd.Parameters.AddWithValue("@UnloadCapacity", Convert.ToDecimal(txtUnloadCapacity.Text));
            cmd.Parameters.AddWithValue("@GodownID", lblGodownID.Text);
            cmd.Parameters.AddWithValue("@IPAddress", Request.UserHostAddress);
            int res = cmd.ExecuteNonQuery();
            if (res > 0)
            {
                popup.Hide();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record updated successfully..')", true);
                txtUnloadCapacity.Text = "";
                txtVacantCapacity.Text = "";
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