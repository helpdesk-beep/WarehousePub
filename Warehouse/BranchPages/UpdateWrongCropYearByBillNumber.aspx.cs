using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_UpdateWrongCropYearByBillNumber : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
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
            fillCropYEar();
           // FillGrid();
        }
    }

    public void fillCropYEar()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Crop_Year_in_Bill_Details", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
            con.Open();
            ddlcropyear.DataSource = cmd.ExecuteReader();
            ddlcropyear.DataTextField = "Crop_Year";
            ddlcropyear.DataValueField = "Crop_Year";
            ddlcropyear.DataBind();
            ddlcropyear.Items.Insert(0, new ListItem("-- Select CropYear --", "0"));
            con.Close();
        }
    }

    public void fillBillMonth()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Crop_Year_Wise_Bill_Month", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
            cmd.Parameters.AddWithValue("@CropYear", ddlcropyear.SelectedValue.ToString());
            con.Open();
            ddlmonth.DataSource = cmd.ExecuteReader();
            ddlmonth.DataTextField = "MonthName";
            ddlmonth.DataValueField = "Month";
            ddlmonth.DataBind();
            ddlmonth.Items.Insert(0, new ListItem("-- Select Month --", "0"));
            con.Close();
        }
    }
    public void FillGrid()
    {
        DataTable dt = new DataTable();
        cmd = new SqlCommand("dbo.Get_Bill_For_Update_Wrong_CropYear", con, sqltran);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
        if (ddlcropyear.SelectedValue == "-- Select CropYear --")
        {
            cmd.Parameters.AddWithValue("@Crop_Year", "");
        }
        else
        {
            cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue.ToString());
        }
        if (ddlmonth.SelectedValue == "-- Select Month --")
        {
            cmd.Parameters.AddWithValue("@Month", "0");
        }
        else
        {
            cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue.ToString());
        }
        cmd.Parameters.AddWithValue("@Bill_Number", txtbillnumber.Text.ToString());
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

    protected void ddlcropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBillMonth();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
}