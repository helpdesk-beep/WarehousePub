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
    //SqlCommand cmd = new SqlCommand();
    // SqlDataAdapter da = null;
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
        SqlCommand cmdd = new SqlCommand("Sp_Get_Insecticide_Consumption_entry", con_JVS);
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

            SqlCommand cmd = new SqlCommand("Sp_Insecticide_Consumption_entry", con_JVS);
            cmd.CommandType = CommandType.StoredProcedure;
            con_JVS.Open();
            cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@Insecticide_ID", ddlinsecticide.SelectedValue);
            cmd.Parameters.AddWithValue("@Entry_Type_ID", DropDownList1.SelectedValue);
            cmd.Parameters.AddWithValue("@Consumption_Balance_quantity", txtQuantity.Text);
            cmd.Parameters.AddWithValue("@Consumption_Balance_value", txtValue.Value);
            cmd.Parameters.AddWithValue("@Consumption_Balance_market_value", txtMarketRed.Text);
            cmd.Parameters.AddWithValue("@Godown_No", ddl_gdwn.SelectedValue);
            cmd.Parameters.AddWithValue("@Unit", ddlUnitStock.SelectedValue);
            cmd.Parameters.AddWithValue("@date", getDate_MDY(txtdob.Text));
            cmd.Parameters.AddWithValue("@created_by", Request.UserHostAddress);
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
        txtQuantity.Text = "";
        txtMarketRed.Text = "";
        txtValue.Value = "";
        txtdob.Text = "";
        ddl_gdwn.SelectedValue = "0";
    }


    protected void txtMarketRed_TextChanged(object sender, EventArgs e)
    {
        decimal val1 = Convert.ToDecimal(txtQuantity.Text);
        decimal val2 = Convert.ToDecimal(txtMarketRed.Text);
        decimal val3 = val1 * val2;
        txtValue.Value = val3.ToString();
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


            GVOfStock.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }

    }

    protected void GVOfStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GVOfStock.Rows[rowIndex];

            //Fetch value of Name.
            string hdnID = (row.FindControl("hdnpfid") as HiddenField).Value;
            Session["hdnID"] = hdnID.ToString();
            Response.Redirect("/warehouse/Inspections/BO/Update_Consumption_During.aspx");

        }
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            //Reference the GridView Row.
            GridViewRow row = GVOfStock.Rows[rowIndex];
            string hdnpfid = (row.FindControl("hdnpfid") as HiddenField).Value;
            string hdnpfbid = (row.FindControl("hdnpfbid") as HiddenField).Value;
            Session["hdnpfid"] = hdnpfid.ToString();
            Session["hdnpfbid"] = hdnpfbid.ToString();
            RemoveRowvcpgqualificationbygridviewAEPRH(hdnpfid, hdnpfbid);

        }
    }
    public void RemoveRowvcpgqualificationbygridviewAEPRH(string id, string Branch_ID)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (con_JVS.State == ConnectionState.Closed)
        {
            con_JVS.Open();
        }
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (con_JVS.State == ConnectionState.Closed)
            {
                con_JVS.Open();
            }

            SqlCommand cmd = new SqlCommand("Delete_Insecticide_Consumption_In_Inspection", con_JVS);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id.ToString());
            cmd.Parameters.AddWithValue("@Branch_ID", Branch_ID.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                FillGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }

    private void FillBlances()
    {
        SqlCommand cmdd = new SqlCommand("Get_Branct_OpningBlances_Total", con_JVS);
        cmdd.CommandType = CommandType.StoredProcedure;
        cmdd.Parameters.AddWithValue("@BranchID", Session["UserId"].ToString());
        cmdd.Parameters.AddWithValue("@Insecticide_ID", ddlinsecticide.SelectedValue);
        //conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txttotalQuantity.Text = dt.Rows[0]["balance"].ToString();
            Session["inc_balance"] = dt.Rows[0]["balance"].ToString();
        }
    }
    protected void ddlinsecticide_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBlances();
    }
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal first = 0;
            int second = 0;
            string strMsg2 = "";
            if (txttotalQuantity.Text != "" && txtQuantity.Text != "")
            {
                if (Int32.Parse(txtQuantity.Text) > decimal.Parse(txttotalQuantity.Text))
                {
                    strMsg2 = strMsg2 + "Sent quantity should not be greater than Avl. Quantity.";
                    txtQuantity.Text = "";
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtQuantity.Text))
                    {
                        if (Int32.TryParse(txtQuantity.Text, out second) && decimal.TryParse(txttotalQuantity.Text, out first))
                            txttotalQuantity.Text = (first - second).ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Quantity');", true);
                        txtQuantity.Text = "";
                    }
                }
            }
            if (strMsg2.Trim() == "")
            {
                lblmsg.Text = "";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
