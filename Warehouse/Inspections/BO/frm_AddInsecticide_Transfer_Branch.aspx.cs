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

public partial class Inspections_BO_frm_AddInsecticide_Transfer_Branch : System.Web.UI.Page
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
                //fillGodownDetails();
                GetDist();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    
    private void FillGrid()
    {
        SqlCommand cmdd = new SqlCommand("Sp_Get_Insecticide_Transfer_District_Branch_Entry", con_JVS);
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
    private void GetDist()
    {

        string strDist = "";


        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT  order by District_Name";
        //strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDistrictT.DataSource = ds.Tables[0];
            ddlDistrictT.DataTextField = "District_Name";
            ddlDistrictT.DataValueField = "District_Id";
            ddlDistrictT.DataBind();
            ddlDistrictT.Items.Insert(0, "--Select--");
            ddlBranchT.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlDistrictT.Items.Insert(0, "--Select--");
        }
    }
    private void GetBranch()
    {
        string strBranch = "";
        //strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
        strBranch = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" +ddlDistrictT.SelectedValue + "' order by Depotname";
        SqlDataAdapter da = new SqlDataAdapter(strBranch, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranchT.DataSource = ds.Tables[0];
            ddlBranchT.DataTextField = "Depotname";
            ddlBranchT.DataValueField = "BranchID";
            ddlBranchT.DataBind();
            ddlBranchT.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlBranchT.Items.Insert(0, "--Select--");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlinsecticide.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Insecticide')", true);
            }

            //else if (ddlDistrictT.SelectedValue=="0")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District')", true);
            //}

            SqlCommand cmd = new SqlCommand("Sp_Insecticide_Transfer_District_Branch_Entry", con_JVS);
            cmd.CommandType = CommandType.StoredProcedure;
            con_JVS.Open();
            cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@Insecticide_ID", ddlinsecticide.SelectedValue);
            cmd.Parameters.AddWithValue("@Entry_Type_ID", DropDownList1.SelectedValue);
            cmd.Parameters.AddWithValue("@Transfer_Balance_quantity", txtTfQuantity.Text);
            cmd.Parameters.AddWithValue("@Transfer_Balance_market_value", txtTfMarketRate.Text);
            cmd.Parameters.AddWithValue("@Transfer_Balance_value", txtTfValue.Value);
            cmd.Parameters.AddWithValue("@Unit", ddlUnitStock.SelectedValue);
            cmd.Parameters.AddWithValue("@date", getDate_MDY(txtdob.Text));
            cmd.Parameters.AddWithValue("@created_by", Request.UserHostAddress);
            cmd.Parameters.AddWithValue("@District_T",ddlDistrictT.SelectedValue);
            cmd.Parameters.AddWithValue("@Branch_T", ddlBranchT.SelectedValue);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Save Sucessfully')", true);
                FillGrid();
                con_JVS.Close();
                clear();
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
        ddlBranchT.Items.Clear();
        ddlDistrictT.Items.Clear();
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
            HeaderCell.ColumnSpan = 5;
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
            Response.Redirect("/warehouse/Inspections/BO/Update_Transfer_Entry_Branch.aspx");

        }
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            //Reference the GridView Row.
            GridViewRow row = GVOfStock.Rows[rowIndex];
            string hdnpfid = (row.FindControl("hdnpfid") as HiddenField).Value;
            string hdnpbid = (row.FindControl("hdnpbid") as HiddenField).Value;
            Session["hdnpfid"] = hdnpfid.ToString();
            Session["hdnpbid"] = hdnpbid.ToString();
            RemoveRowvcpgqualificationbygridviewAEPRH(hdnpfid,hdnpbid);

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

            SqlCommand cmd = new SqlCommand("Delete_Insecticide_Transfer_In_Inspection", con_JVS);
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


    protected void ddlDistrictT_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }
}
