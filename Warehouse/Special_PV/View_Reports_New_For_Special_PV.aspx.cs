using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.Collections;
using System.Linq;

public partial class Special_PV_Region_View_Reports_New_For_Special_PV : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd;
    DataTable dt = new DataTable();
    string PFID = "";

    int qtyTotal = 0;
    int grQtyTotal = 0;
    decimal qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal qtyTotal3 = 0;
    decimal qtyTotal4 = 0;
    decimal qtyTotal5 = 0;

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;
    decimal grQtyTotal4 = 0;
    decimal grQtyTotal5 = 0;

    //long storid = 0;
    long storid = 0;
    int rowIndex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        //lbl_user.Text = Session["UserName"].ToString();
        txt_Region.Text = Session["UserName"].ToString();
        // ddl_Region.SelectedItem.Text = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            //fillInpOff_Grid();
            // GetRegion();
            GetDist();
            //fillFinsncilYear();
        }
    }
    private void GetDist()
    {
        string strDist = "";
        strDist = "SELECT District_Id,District_Name FROM tbl_MetaData_DISTRICT where Region_ID = '" + Session["UserId"].ToString() + "' order by District_Name ";
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
    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }
    public void GetBranch()
    {
        string qry = "";
        qry = "select distinct BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "'";
        SqlDataAdapter da = new SqlDataAdapter(qry, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlBranch.Items.Insert(0, "--Select--");
        }
    }
    public void fillInpOff_Grid()
    {
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();

            }
            SqlCommand cmd = new SqlCommand("Get_Complite_Special_Inspection_Details", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", ddlBranch.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { conStr.Close(); }

            if (dt.Rows.Count > 0)
            {
                GrdOfficerPreviousInsp.DataSource = dt;
                GrdOfficerPreviousInsp.DataBind();
                divremark.Visible = true;
                divverify.Visible = true;
                GrdOfficerPreviousInsp.FooterRow.Style.Add("text-align", "center");
                GrdOfficerPreviousInsp.FooterRow.Cells[11].Text = "Total";
                GrdOfficerPreviousInsp.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Number_Of_Block")).ToString();
                GrdOfficerPreviousInsp.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("No_of_Bags")).ToString();
                GrdOfficerPreviousInsp.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Up")).ToString();
                GrdOfficerPreviousInsp.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Below")).ToString();
                GrdOfficerPreviousInsp.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Bags")).ToString();
                GrdOfficerPreviousInsp.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Spillage_bag")).ToString();
                //lblOfficerList.Text = Convert.ToString(dt.Rows.Count);
                ShowingGroupingDataInGridView(GrdOfficerPreviousInsp.Rows, 0, 0);
            }
            else
            {
                GrdOfficerPreviousInsp.DataSource = null;
                GrdOfficerPreviousInsp.DataBind();
                // lblOfficerList.Text = "0";
                string strMsg = "इस ब्रांच के द्वारा वर्ष 2025-26 की एंट्री गड़ना पत्रक में नहीं की गई हैं ,पहले ब्रांच के द्वारा गड़ना पत्रक की एंट्री करनी होगी |";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
        finally
        { if (conStr.State == ConnectionState.Open) { conStr.Close(); } }
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fillInpOff_Grid();
    }

    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        try
        {
            string ErrorMsg = "";
            ErrorMsg += !string.IsNullOrEmpty(txtremark.Text) ? "" : "Enter Remark \\n";
            if (ErrorMsg == "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Insp_Special_PV_RM_VErify_Status", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                //tn = con.BeginTransaction();
                //cmd.Transaction = tn;
                cmd.Parameters.AddWithValue("@Branch_ID", ddlBranch.SelectedValue);
                cmd.Parameters.AddWithValue("@Rm_Remark", txtremark.Text);
                cmd.Parameters.AddWithValue("@Rm_Update_By", Session["UserId"].ToString());
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "data has been final submitted successfully";
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');", true);
                    //fillScheduleInsp_Grid();
                    divremark.Visible = false;
                    divverify.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                }
                con.Close();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' " + ErrorMsg.ToString() + "')", true);
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void GrdOfficerPreviousInsp_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "stack_id") != null))
        {
            if (storid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "stack_id").ToString()))
                newRow = true;
        }
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "stack_id") == null))
        {
            newRow = true;
            rowIndex = 0;
        }
        if (newRow)
        {
            GridView GridView1 = (GridView)sender;
            GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            NewTotalRow.Font.Bold = true;
            // NewTotalRow.BackColor = System.Drawing.Color.Gray;
            NewTotalRow.ForeColor = System.Drawing.Color.Black;
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Sub Total";
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.ColumnSpan = 12;

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = qtyTotal.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = qtyTotal1.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = qtyTotal2.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = qtyTotal3.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = qtyTotal4.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = qtyTotal5.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            GrdOfficerPreviousInsp.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal = 0;
            qtyTotal1 = 0;
            qtyTotal2 = 0;
            qtyTotal3 = 0;
            qtyTotal4 = 0;
            qtyTotal5 = 0;
        }
    }
    void ShowingGroupingDataInGridView(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
    {
        if (totalColumns == 0) return;
        int i, count = 1;
        ArrayList lst = new ArrayList();
        lst.Add(gridViewRows[0]);
        var ctrl = gridViewRows[0].Cells[startIndex];
        for (i = 1; i < gridViewRows.Count; i++)
        {
            TableCell nextTbCell = gridViewRows[i].Cells[startIndex];
            if (ctrl.Text == nextTbCell.Text)
            {
                count++;
                nextTbCell.Visible = false;
                lst.Add(gridViewRows[i]);
            }
            else
            {
                if (count > 1)
                {
                    ctrl.RowSpan = count;
                    ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
                }
                count = 1;
                lst.Clear();
                ctrl = gridViewRows[i].Cells[startIndex];
                lst.Add(gridViewRows[i]);
            }
        }
        if (count > 1)
        {
            ctrl.RowSpan = count;
            ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
        }
        count = 1;
        lst.Clear();
    }
    protected void GrdOfficerPreviousInsp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "stack_id").ToString());
            int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Number_Of_Block").ToString());
            int tmpTotal1 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "No_of_Bags").ToString());
            int tmpTotal2 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Up").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Below").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total_Bags").ToString());
            int tmpTotal5 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Spillage_bag").ToString());

            qtyTotal += tmpTotal;
            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
            qtyTotal5 += tmpTotal5;

            grQtyTotal += tmpTotal;
            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
            grQtyTotal5 += tmpTotal5;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnFinalsubmit = (HiddenField)e.Row.FindControl("hdnFinalsubmit");
            //Button btnRemove = (Button)e.Row.FindControl("btnRemove");
            //Button btnEdit = (Button)e.Row.FindControl("btnEdit");
            if (hdnFinalsubmit.Value == "0")
            {
                //divbtn.Visible = true;
                btn_saveInspDate.Visible = true;
                divremark.Visible = true;
                //divremark.Visible = true;
                //txtremark.Visible = true;
                //btnPrint.Visible = false;
            }
            else if (hdnFinalsubmit.Value == "1")
            {
                //divbtn.Visible = false;
                btn_saveInspDate.Visible = false;
                divremark.Visible = false;
                //divremark.Visible = false;
                //txtremark.Visible = false;
                // btnPrint.Visible = true;
            }
        }
    }

    protected void GrdOfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}