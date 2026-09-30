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

public partial class Inspections_BO_frm_AddInsecticide_receipt_during : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    public SqlConnection con_JVS = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    //public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    private object con;
    private object ob_value;

    public object GridView1 { get; private set; }
    public Label HiddenField { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        //CalendarExtender1.EndDate = DateTime.Now;
        //CalendarExtender2.EndDate = DateTime.Now;
        if ((Session["UserId"] != null))
        {
            if (!IsPostBack)
            {
                if (Session["message"] != null)
                {

                    txtMsg.InnerText = Session["message"].ToString();
                    Session["message"] = null;

                }
               
                btnUpdate.Visible = false;
                if (!String.IsNullOrEmpty(Session["UserId"].ToString()))
                {
                    FillGrid();
                    btnUpdate.Visible = false;
                }
                if (Session["hdnID"] != null)
                {
                    if (!String.IsNullOrEmpty(Session["hdnID"].ToString()))
                    {
                        FillDataforUpdate();
                    }
                }
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void FillGrid()
    {
        SqlCommand cmdd = new SqlCommand("Sp_Get_Insecticide_Fertilizer_Entry", con_JVS);
        cmdd.CommandType = CommandType.StoredProcedure;
        cmdd.Parameters.AddWithValue("@branch_ID", Session["UserId"].ToString());
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
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
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

            SqlCommand cmd = new SqlCommand("sp_insecticide_fertilizer_entry_Receipt", con_JVS);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@Insecticide_ID", ddlinsecticide.SelectedValue);
            cmd.Parameters.AddWithValue("@Receipt_Balance_quantity", txtQuantity.Text);
            cmd.Parameters.AddWithValue("@Receipt_Balance_value", txtValue.Value);
            cmd.Parameters.AddWithValue("@Receipt_Balance_market_value", txtMarketRed.Text);
            cmd.Parameters.AddWithValue("@Unit", ddlUnitStock.SelectedValue);
            cmd.Parameters.AddWithValue("@created_by",Request.UserHostAddress);
            cmd.Parameters.AddWithValue("@Insecticide_Given_By", ddloffices.SelectedValue);
            cmd.Parameters.AddWithValue("@date", getDate_MDY(txtdob.Text));
            cmd.Parameters.AddWithValue("@Expiry_date", getDate_MDY(txtExpriyDate.Text)); 
            con_JVS.Open();
            cmd.ExecuteNonQuery();
            con_JVS.Close();
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
            con_JVS.Close();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlinsecticide.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('select ')", true);
            }

            SqlCommand cmd = new SqlCommand("SP_Get_Insecticide_Fertilizer_Entry_update_Entry", con_WLC);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", Session["hdntblID2"].ToString());
            cmd.Parameters.AddWithValue("@Opening_Balance_quantity", txtQuantity.Text);
            cmd.Parameters.AddWithValue("@Opening_Balance_value", txtValue.Value);
            cmd.Parameters.AddWithValue("@Opening_Balance_market_value", txtMarketRed.Text);
            cmd.Parameters.AddWithValue("@update_by", Request.UserHostAddress);
            con_WLC.Open();
            cmd.ExecuteNonQuery();
            con_WLC.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Update Sucessfully')", true);
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
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
        ddloffices.SelectedValue = "0";
        txtQuantity.Text = "";
        txtMarketRed.Text = "";
        txtValue.Value = "";
        txtdob.Text = "";
        txtExpriyDate.Text = "";
    }


    protected void txtMarketRed_TextChanged(object sender, EventArgs e)
    {
        decimal val1 = Convert.ToDecimal(txtQuantity.Text);
        decimal val2 = Convert.ToDecimal(txtMarketRed.Text);
        decimal val3 = val1 * val2;
        txtValue.Value = val3.ToString();
    }


    protected void GVOfStock_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {

        //HiddenField id = GVOfStock.Rows[e.RowIndex].FindControl("hdnpfid") as HiddenField;
        //TextBox ob_quantity = GVOfStock.Rows[e.RowIndex].FindControl("txt_ob_quantity") as TextBox;
        //TextBox ob_market_value = GVOfStock.Rows[e.RowIndex].FindControl("txt_op_market_value") as TextBox;
        //TextBox ob_value = GVOfStock.Rows[e.RowIndex].FindControl("txt_ob_value") as TextBox;

        //con_JVS.Open();
        ////updating the record  
        //SqlCommand cmd = new SqlCommand("SP_Get_Fertilizer_update_Entry", con_JVS);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@ID", id.Value);

        //cmd.Parameters.AddWithValue("@Opening_Balance_quantity", ob_quantity.Text);
        //cmd.Parameters.AddWithValue("@Opening_Balance_market_value", ob_market_value.Text);
        //cmd.Parameters.AddWithValue("@Opening_Balance_value", ob_value.Text);
        //cmd.ExecuteNonQuery();
        //con_JVS.Close();

        //GVOfStock.EditIndex = -1;

        //FillGrid();
    }
    protected void GVOfStock_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
         
        GVOfStock.EditIndex = -1;
        FillGrid();
    }

    

    public void FillDataforUpdate()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Sp_Get_Insecticide_Fertilizer_Entry_By_ID", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", Session["hdnID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            txtdob.Text= dt.Rows[0]["Date"].ToString();
                            ddlUnitStock.SelectedValue = dt.Rows[0]["Unit"].ToString();
                            ddlinsecticide.SelectedValue = dt.Rows[0]["Insecticide_ID"].ToString();
                            ddloffices.SelectedValue = dt.Rows[0]["Insecticide_Given_By"].ToString();
                           
                            txtQuantity.Text = dt.Rows[0]["Receipt_Balance_quantity"].ToString();
                            txtMarketRed.Text = dt.Rows[0]["Receipt_Balance_market_value"].ToString();
                            txtValue.Value = dt.Rows[0]["Receipt_Balance_value"].ToString();
                            txtExpriyDate.Text = dt.Rows[0]["Expiry_Date"].ToString();
                            btnUpdate.Visible = true;
                            Session["hdntblID"] = "0";
                            Session["hdntblID2"] = dt.Rows[0]["id"].ToString();
                            btnSubmit.Visible = false;
                        }
                        else
                        {
                            ddlinsecticide.SelectedValue = "0";
                            ddlUnitStock.SelectedValue = "0";
                            txtQuantity.Text = "";
                            txtMarketRed.Text = "";
                            txtValue.Value = "";
                            btnUpdate.Visible = false;

                        }
                    }
                }
            }
        }
    }
    protected void GVOfStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Overallinsp")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GVOfStock.Rows[rowIndex];
            //Fetch value of Name.           
            string hdnID = (row.FindControl("hdnID") as HiddenField).Value;
            Session["hdnID"] = hdnID.ToString();
            Response.Redirect("~/Inspections/BO/frm_AddInsecticide_receipt_during.aspx");
        }

    }
    protected void GVOfStock_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited.  
        GVOfStock.EditIndex = e.NewEditIndex;
        FillGrid();
    }

    protected void GVOfStock_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        GVOfStock.EditIndex = -1;
        FillGrid();
    }
}
