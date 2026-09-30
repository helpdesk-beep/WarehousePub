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

public partial class Inspections_State_rpt_Insecticide_DateWise : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            //GetRegion();
           
                FillGrid();
            //GetRegion();
            GetDist();
        }
    }

    //protected string getDate_MDY(string inDate)
    //{
    //    if (inDate == "" || inDate == null)
    //    {
    //        return "01/01/1919";
    //    }
    //    else
    //    {
    //        string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
    //        string converted = "";
    //        string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
    //        converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
    //        return converted;
    //    }
    //}
    private void FillGrid()
    {
        //SqlCommand cmdd = new SqlCommand("Sp_Insecticide_rpt_district", con_JVS);
        SqlCommand cmdd = new SqlCommand("Sp_Insecticide_rpt_DateWise", con_JVS);
        cmdd.CommandType = CommandType.StoredProcedure;
        //if (ddlregion.SelectedValue == "--Select--" )
        //{
        //    cmdd.Parameters.AddWithValue("@Region_ID", "0");

        //}
        if (txtFDate.Text == "")
        {
            cmdd.Parameters.AddWithValue("@Date", "");

        }
        else
        {
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(txtFDate.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy/MM/dd");

            // string setdatedata = string.Format("{0:yy/MM/dd}", txtFDate.Text);
            cmdd.Parameters.AddWithValue("@Date", converted);
        }
        if (ddldistrict.SelectedValue == "--Select--")
        {
            cmdd.Parameters.AddWithValue("@districtID", "0");
        }
        else 
        {
            cmdd.Parameters.AddWithValue("@districtID",ddldistrict.SelectedValue);
        }
        if (ddlbranch.SelectedValue == "--Select--")
        {
            cmdd.Parameters.AddWithValue("@Branch_ID", "0");
        }
        else
        {
            cmdd.Parameters.AddWithValue("@Branch_ID",ddlbranch.SelectedValue);
        }
        //cmdd.Parameters.AddWithValue("@Region_ID", ddlregion.SelectedValue);
        //cmdd.Parameters.AddWithValue("@districtID", ddldistrict.SelectedValue);
        //cmdd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
        cmdd.Parameters.AddWithValue("@Insecticide_ID", ddl_Ins.SelectedValue);
        //cmdd.Parameters.AddWithValue("@Date", getDate_MDY(txtFDate.Text));

        SqlDataAdapter da = new SqlDataAdapter(cmdd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GVOfStock.DataSource = dt;
            GVOfStock.DataBind();
            GVOfStock.FooterRow.Style.Add("text-align", "center");
            GVOfStock.FooterRow.Cells[3].Text = "Total";
            GVOfStock.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Opening_Balance_quantity")).ToString();
            GVOfStock.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Opening_Balance_value")).ToString();
            GVOfStock.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Receipt_Balance_quantity")).ToString();
            GVOfStock.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Receipt_Balance_value")).ToString();
            GVOfStock.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("CBQ1")).ToString();
            GVOfStock.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("CBV1")).ToString();
            GVOfStock.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TBQ")).ToString();
            GVOfStock.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TBV")).ToString();
            GVOfStock.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Quantity")).ToString();
            GVOfStock.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Value")).ToString();
        }
    }
    //private void GetRegion()
    //{
    //    string strDist = "";
    //    strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Regionnm";
    //    SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlregion.DataSource = ds.Tables[0];
    //        ddlregion.DataTextField = "Regionnm";
    //        ddlregion.DataValueField = "Region_ID";
    //        ddlregion.DataBind();
    //        ddlregion.Items.Insert(0, "--Select--");
    //    }
    //    else
    //    {
    //        ddlregion.Items.Insert(0, "--Select--");
    //    }
    //}
    private void GetDist()
    {
        string strDist = "";
        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT  order by District_Name";
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
    
    protected string GetDate_MDY(string inDate)
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
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy/MM/dd");
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



    protected void GVOfStock_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 4;
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



    //protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //FillGrid();
    //    //GetRegion();
    //    GetDist(ddlregion.SelectedValue.ToString());
    //    FillGrid();
    //}

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





    protected void txtFDate_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
}
