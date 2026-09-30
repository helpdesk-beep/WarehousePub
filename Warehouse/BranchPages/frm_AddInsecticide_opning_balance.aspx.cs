using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Principal;

public partial class Accounting_frm_AddInsecticide_opning_balance : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    //public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    private object con;
    private object ob_value;

    public object GridView1 { get; private set; }
    public Label HiddenField { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void FillGrid()
    {
        SqlCommand cmdd = new SqlCommand("SP_Get_Fertilizer_Entry", con_WLC);
        cmdd.CommandType = CommandType.StoredProcedure;
        cmdd.Parameters.AddWithValue("@branch_ID", Session["BranchID"].ToString());
        //conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GVOfStock.DataSource = dt;
            GVOfStock.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlinsecticide.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('select ')", true);
            }

            SqlCommand cmd = new SqlCommand("sp_fertilizer_insecticide", con_WLC);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchID"].ToString());
            cmd.Parameters.AddWithValue("@Insecticide_ID", ddlinsecticide.SelectedValue);
            cmd.Parameters.AddWithValue("@Opening_Balance_quantity", txtQuantity.Text);
            cmd.Parameters.AddWithValue("@Opening_Balance_value", txtValue.Value);
            cmd.Parameters.AddWithValue("@Opening_Balance_market_value", txtMarketRed.Text);
            cmd.Parameters.AddWithValue("@Unit", ddlUnitStock.SelectedValue);
            cmd.Parameters.AddWithValue("@created_by", Request.UserHostAddress);
            con_WLC.Open();
            cmd.ExecuteNonQuery();
            con_WLC.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Save Sucessfully')", true);
            FillGrid();
            clear();
        }
        catch (Exception ex)
        {
            string except = ex.Message.ToString();

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + except + "')", true);
        }
        finally
        {
            con_WLC.Close();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    public void clear()
    {
        ddlinsecticide.SelectedValue = "0";
        ddlUnitStock.SelectedValue = "0";
        txtQuantity.Text = "";
        txtMarketRed.Text = "";
        txtValue.Value = "";
    }


    protected void txtMarketRed_TextChanged(object sender, EventArgs e)
    {
        Int32 val1 = Convert.ToInt32(txtQuantity.Text);
        Int32 val2 = Convert.ToInt32(txtMarketRed.Text);
        Int32 val3 = val1 * val2;
        txtValue.Value = val3.ToString();
    }

    protected void GVOfStock_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited.  
        GVOfStock.EditIndex = e.NewEditIndex;
        FillGrid();
    }
    protected void GVOfStock_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
         
         HiddenField id = GVOfStock.Rows[e.RowIndex].FindControl("hdnpfid") as HiddenField;
        //TextBox Unit_Name = GVOfStock.Rows[e.RowIndex].FindControl("lbl_Unit_Name") as TextBox;
        TextBox ob_quantity = GVOfStock.Rows[e.RowIndex].FindControl("txt_ob_quantity") as TextBox;
        TextBox op_market_value = GVOfStock.Rows[e.RowIndex].FindControl("txt_op_market_value") as TextBox;
        TextBox ob_value = GVOfStock.Rows[e.RowIndex].FindControl("txt_ob_value") as TextBox;
        
        con_WLC.Open();
        //updating the record  
        SqlCommand cmd = new SqlCommand("SP_Get_Fertilizer_update_Entry",con_WLC);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", id.Value);
        //cmd.Parameters.AddWithValue("@op_market_value", Unit_Name.Text);
        cmd.Parameters.AddWithValue("@ob_quantity", ob_quantity.Text);
        cmd.Parameters.AddWithValue("@op_market_value", op_market_value.Text);
        cmd.Parameters.AddWithValue("@ob_value", ob_value.Text);
        cmd.ExecuteNonQuery();
        con_WLC.Close();
        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        GVOfStock.EditIndex = -1;
        //Call ShowData method for displaying updated data  
        FillGrid();
    }
    protected void GVOfStock_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        GVOfStock.EditIndex = -1;
        FillGrid();
    }


    protected void GVOfStock_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        GVOfStock.EditIndex = -1;
        FillGrid();
    }
}
