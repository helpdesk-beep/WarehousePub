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

public partial class Inspections_BO_frm_AddInsecticide_Consumption_During : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    public SqlConnection con_JVS = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    private object con;
    private object ob_value;

    public object GridView1 { get; private set; }
    public Label HiddenField { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] != null))
        {
            if (!IsPostBack)
            {
                FillGrid();
                fillGodownDetails();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void fillGodownDetails()
    {
        using (SqlConnection con = new SqlConnection(constr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Name", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
            con.Open();
            ddl_gdwn.DataSource = cmd.ExecuteReader();
            ddl_gdwn.DataTextField = "Godown_Name";
            ddl_gdwn.DataValueField = "Godown_ID";
            ddl_gdwn.DataBind();
            ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con.Close();
        }
    }
    private void FillGrid()
    {
        SqlCommand cmdd = new SqlCommand("Sp_Get_Insecticide_Consumption_entry",con_JVS);
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

            SqlCommand cmd = new SqlCommand("Sp_Insecticide_Consumption_entry", con_JVS);
            cmd.CommandType = CommandType.StoredProcedure;
            con_JVS.Open();
            cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@Insecticide_ID", ddlinsecticide.SelectedValue);
            cmd.Parameters.AddWithValue("@Entry_Type_ID", DropDownList1.SelectedValue);
            if (DropDownList1.SelectedValue == "1")
            {
                cmd.Parameters.AddWithValue("@Consumption_Balance_quantity", txtQuantity.Text);
                cmd.Parameters.AddWithValue("@Consumption_Balance_value", txtValue.Value);
                cmd.Parameters.AddWithValue("@Consumption_Balance_market_value", txtMarketRed.Text);
                cmd.Parameters.AddWithValue("@Transfer_Balance_quantity", 0);
                cmd.Parameters.AddWithValue("@Transfer_Balance_market_value", 0);
                cmd.Parameters.AddWithValue("@Transfer_Balance_value", 0);
            }
            else if (DropDownList1.SelectedValue == "2")
            {
                cmd.Parameters.AddWithValue("@Consumption_Balance_quantity", 0);
                cmd.Parameters.AddWithValue("@Consumption_Balance_value", 0);
                cmd.Parameters.AddWithValue("@Consumption_Balance_market_value", 0);
                cmd.Parameters.AddWithValue("@Transfer_Balance_quantity", txtTfQuantity.Text);
                cmd.Parameters.AddWithValue("@Transfer_Balance_market_value", txtTfMarketRate.Text);
                cmd.Parameters.AddWithValue("@Transfer_Balance_value", txtTfValue.Value);
            }
            cmd.Parameters.AddWithValue("@Godown_No", ddl_gdwn.SelectedValue);
            cmd.Parameters.AddWithValue("@Unit",ddlUnitStock.SelectedValue);
            cmd.Parameters.AddWithValue("@date", getDate_MDY(txtdob.Text));
            cmd.Parameters.AddWithValue("@created_by",Request.UserHostAddress);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Save Sucessfully')", true);
                FillGrid();
                clear();
                con_JVS.Close();
            }
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    public void clear()
    {
        ddlinsecticide.SelectedValue = "0";
        ddlUnitStock.SelectedValue = "0";
        //ddloffices.SelectedValue = "0";
        txtQuantity.Text = "";
        txtMarketRed.Text = "";
        txtValue.Value = "";
        txtdob.Text = "";
        txtTfQuantity.Text = "";
        txtTfMarketRate.Text = "";
        txtTfValue.Value = "";
        ddl_gdwn.SelectedValue = "0";
        //txtExpriyDate.Text = "";
    }


    protected void txtMarketRed_TextChanged(object sender, EventArgs e)
    {
        Decimal val1 = Convert.ToDecimal(txtQuantity.Text);
        Decimal val2 = Convert.ToDecimal(txtMarketRed.Text);
        Decimal val3 = val1 * val2;
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
       TextBox ob_quantity = GVOfStock.Rows[e.RowIndex].FindControl("txt_ob_quantity") as TextBox;
        TextBox ob_market_value = GVOfStock.Rows[e.RowIndex].FindControl("txt_op_market_value") as TextBox;
        TextBox ob_value = GVOfStock.Rows[e.RowIndex].FindControl("txt_ob_value") as TextBox;
        
        con_JVS.Open();
        //updating the record  
        SqlCommand cmd = new SqlCommand("SP_Get_Fertilizer_update_Entry", con_JVS);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", id.Value);
       
        cmd.Parameters.AddWithValue("@Opening_Balance_quantity", ob_quantity.Text);
        cmd.Parameters.AddWithValue("@Opening_Balance_market_value", ob_market_value.Text);
        cmd.Parameters.AddWithValue("@Opening_Balance_value", ob_value.Text);
        cmd.ExecuteNonQuery();
        con_JVS.Close();
         
        GVOfStock.EditIndex = -1;
          
        FillGrid();
    }
    protected void GVOfStock_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
         
        GVOfStock.EditIndex = -1;
        FillGrid();
    }


    protected void GVOfStock_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        GVOfStock.EditIndex = -1;
        FillGrid();
    }

    protected void txtTfMarketRate_TextChanged(object sender, EventArgs e)
    {
        decimal val1 = Convert.ToDecimal(txtTfQuantity.Text);
        decimal val2 = Convert.ToDecimal(txtTfMarketRate.Text);
        decimal val3 = val1 * val2;
        txtTfValue.Value = val3.ToString();
    }

    protected void GVOfStock_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Consumption During Entry";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Transfer During Entry";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            
            GVOfStock.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == "0")
        {
            CDE.Visible = false;
            CDED.Visible = false;
            TDE.Visible = false;
            TDED.Visible = false;

        }
         else if (DropDownList1.SelectedValue == "1")
        {
            CDE.Visible = true;
            CDED.Visible = true;
            TDE.Visible = false;
            TDED.Visible = false;

        }
        else if (DropDownList1.SelectedValue=="2")
        {
            CDE.Visible = false;
            CDED.Visible = false;
            TDE.Visible = true;
            TDED.Visible = true;
        }
    }
}
