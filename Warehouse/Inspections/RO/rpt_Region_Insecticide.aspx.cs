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

public partial class Inspections_RO_rpt_Region_Insecticide : System.Web.UI.Page
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
                GetDist();
                //fillGodownDetails();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    
    private void FillGrid()
    {
        SqlCommand cmdd = new SqlCommand("rpt_Region_Insecticide",con_JVS);
        cmdd.CommandType = CommandType.StoredProcedure;
        cmdd.Parameters.AddWithValue("@Region_ID", Session["UserId"].ToString());
        if (ddldistrict.SelectedValue == "--Select--")
        {
            // ddldistrict.SelectedValue = "0";
            cmdd.Parameters.AddWithValue("@District_ID", "0");
        }
        else
        {
            cmdd.Parameters.AddWithValue("@District_ID", ddldistrict.SelectedValue);
        }
        //cmdd.Parameters.AddWithValue("@District_ID",ddldistrict.SelectedValue);
        if (ddlbranch.SelectedValue == "--Select--")
        {
            //ddlbranch.SelectedValue = "0";
            cmdd.Parameters.AddWithValue("@Branch_ID", "0");
        }
        else
        {
            cmdd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
        }
        if (ddl_Ins.SelectedValue == "--Select--")
        {
            //ddlbranch.SelectedValue = "0";
            cmdd.Parameters.AddWithValue("@Insecticide_ID", "0");
        }
        else
        {
            cmdd.Parameters.AddWithValue("@Insecticide_ID", ddl_Ins.SelectedValue);
        }

        //cmdd.Parameters.AddWithValue("@Insecticide_ID",ddl_Ins.SelectedValue);
       
        SqlDataAdapter da = new SqlDataAdapter(cmdd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GVOfStock.DataSource = dt;
            GVOfStock.DataBind();
            GVOfStock.FooterRow.Style.Add("text-align", "center");
            GVOfStock.FooterRow.Cells[2].Text = "Total";
            GVOfStock.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Opening_Balance_quantity")).ToString();
            GVOfStock.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Opening_Balance_value")).ToString();
            GVOfStock.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Receipt_Balance_quantity")).ToString();
            GVOfStock.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Receipt_Balance_value")).ToString();
            GVOfStock.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("CBQ1")).ToString();
            GVOfStock.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("CBMC1")).ToString();
            GVOfStock.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TBQ")).ToString();
            GVOfStock.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TBV")).ToString();
            GVOfStock.FooterRow.Cells[11].Text= dt.AsEnumerable().Sum(row => row.Field<decimal>("Quantity")).ToString();
            GVOfStock.FooterRow.Cells[12].Text= dt.AsEnumerable().Sum(row => row.Field<decimal>("Value")).ToString();
        }
    }
    private void GetDist()
    {
        
        string strDist = "";  


         strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT where Region_ID = '" + Session["UserId"] + "' order by District_Name";
        //strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "--Select--");
            ddlbranch.Items.Insert(0, "--Select--");
        }
        else
        {
            ddldistrict.Items.Insert(0, "--Select--");
        }
    }
    private void GetBranch(string DistID)
    {
        string strBranch = "";
        //strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
        strBranch = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + DistID + "' order by Depotname";
        SqlDataAdapter da = new SqlDataAdapter(strBranch, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "Depotname";
            ddlbranch.DataValueField = "BranchID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlbranch.Items.Insert(0, "--Select--");
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
            string[] formats = {"dd/MM/yyyy","dd-MM-yyyy","dd-MM-yy","dd-MMM-yyyy","yyyy-MM-dd","dd-MM-yyyy","M/d/yyyy","dd MMM yyyy","dd-MM-yy","yyyy/MM/dd","MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
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

    //protected void txtTfMarketRate_TextChanged(object sender, EventArgs e)
    //{
    //    decimal val1 = Convert.ToDecimal(txtTfQuantity.Text);
    //    decimal val2 = Convert.ToDecimal(txtTfMarketRate.Text);
    //    decimal val3 = val1 * val2;
    //    txtTfValue.Value = val3.ToString();
    //}

    protected void GVOfStock_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Opening Balance";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Receipt Balance";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Consumption During Entry";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Transfer During Entry";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Closing Blances";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);


            GVOfStock.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }

    }


    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch(ddldistrict.SelectedValue.ToString());
        FillGrid();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void ddl_Ins_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
}
