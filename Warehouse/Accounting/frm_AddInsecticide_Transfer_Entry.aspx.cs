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

public partial class Inspections_BO_frm_AddInsecticide_Transfer_Entry : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    public SqlConnection con_JVS = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
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
        SqlCommand cmdd = new SqlCommand("Sp_Get_Insecticide_Transfer_entry", con_JVS);
        cmdd.CommandType = CommandType.StoredProcedure;
        cmdd.Parameters.AddWithValue("@branch_ID", Session["UserId"].ToString());
        //conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //tblbtn.Visible = true;
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

            SqlCommand cmd = new SqlCommand("Sp_Insecticide_Transfer_entry", con_JVS);
            cmd.CommandType = CommandType.StoredProcedure;
            con_JVS.Open();
            cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@Insecticide_ID", ddlinsecticide.SelectedValue);
            cmd.Parameters.AddWithValue("@Entry_Type_ID", DropDownList1.SelectedValue);

            cmd.Parameters.AddWithValue("@Transfer_Balance_quantity", txtTfQuantity.Text);
            cmd.Parameters.AddWithValue("@Transfer_Balance_market_value", txtTfMarketRate.Text);
            cmd.Parameters.AddWithValue("@Transfer_Balance_value", txtTfValue.Value);

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
                FillBlances();
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
        txtTfQuantity.Text = "";
        txtTfMarketRate.Text = "";
        txtTfValue.Value = "";
        txtdob.Text = "";
        ddl_gdwn.SelectedValue = "0";
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
            if (txttotalQuantity.Text != "" && txtTfQuantity.Text != "")
            {
                if (Int32.Parse(txtTfQuantity.Text) > decimal.Parse(txttotalQuantity.Text))
                {
                    strMsg2 = strMsg2 + "Sent quantity should not be greater than Avl. Quantity.";
                    txtTfQuantity.Text = "";
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtTfQuantity.Text))
                    {
                        if (Int32.TryParse(txtTfQuantity.Text, out second) && decimal.TryParse(txttotalQuantity.Text, out first))
                            txttotalQuantity.Text = (first - second).ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Quantity');", true);
                        txtTfQuantity.Text = "";
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
    protected void txtTfMarketRate_TextChanged(object sender, EventArgs e)
    {
        Decimal val1 = Convert.ToDecimal(txtTfQuantity.Text);
        Decimal val2 = Convert.ToDecimal(txtTfMarketRate.Text);
        Decimal val3 = val1 * val2;
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
            Response.Redirect("/warehouse/Inspections/BO/Update_Transfer_Entry.aspx");

        }
        //if (e.CommandName == "btnDelete")
        //{
        //    //Determine the RowIndex of the Row whose Button was clicked.
        //    int rowIndex = Convert.ToInt32(e.CommandArgument);

        //    //Reference the GridView Row.
        //    GridViewRow row = GVOfStock.Rows[rowIndex];

        //    //Fetch value of Name.
        //    string hdnID = (row.FindControl("hdnpfid") as HiddenField).Value;
        //    Session["hdnID"] = hdnID.ToString();
        //    Response.Redirect("/warehouse/Inspections/BO/Update_Transfer_Entry.aspx");

        //}
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (GridViewRow row in GVOfStock.Rows)
            {
                CheckBox chk_Sum = (CheckBox)(row.FindControl("chk_Sum"));
                HiddenField hdncheckID = (HiddenField)(row.FindControl("hdncheckID"));
                if (chk_Sum.Checked == true)
                {
                    conStr.Open();
                    cmd = new SqlCommand("[dbo].[Delete_Insectiside_Entry_By_BM]", conStr);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", hdncheckID.Value);
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                    if (TheResult.StartsWith("SUCCESS"))

                    {
                        count++;
                    }
                    conStr.Close();
                }
            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Delete successfully..')", true);
                FillGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record NOT Delete')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }
        finally
        {
            conStr.Close();
        }
    }
}
