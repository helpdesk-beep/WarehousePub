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
using System.Text;
using System.Collections.Generic;

public partial class Special_PV_Special_PV_Final_Submit : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
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
        PFID = Session["UserId"].ToString();

        if (!IsPostBack)
        {
            fillScheduleInsp_Grid();
        }
    }
    public void fillScheduleInsp_Grid()
    {
        SqlCommand cmd = new SqlCommand("Get_Verify_By_Branch_Manager_For_Final_Submit", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
        // cmd.Parameters.AddWithValue("@cropyear", ddlcropyear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //GD_StackBal.Caption = @"<b style=""font-weight: bold;"">Godown Name: " + "  -   " + ddl_gdwn.SelectedItem.ToString() + "</b> ";
            divshow.Visible = true;
            divbtn.Visible = true;
            btn_FinalSubmit.Visible = true;
            //divremark.Visible = true;
            //txtremark.Visible = true;
            GD_StackBal.DataSource = dt;
            GD_StackBal.DataBind();
            GD_StackBal.FooterRow.Style.Add("text-align", "center");
            GD_StackBal.FooterRow.Cells[11].Text = "Total";
            GD_StackBal.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Number_Of_Block")).ToString();
            GD_StackBal.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("No_of_Bags")).ToString();
            GD_StackBal.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Up")).ToString();
            GD_StackBal.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Below")).ToString();
            GD_StackBal.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Bags")).ToString();
            GD_StackBal.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Spillage_bag")).ToString();
            ShowingGroupingDataInGridView(GD_StackBal.Rows, 0, 0);
        }
        else
        {
            divbtn.Visible = false;
            divremark.Visible = false;
            divshow.Visible = true;
            btn_FinalSubmit.Visible = false;
            //divremark.Visible = false;
            //txtremark.Visible = false;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
            //string strMsg = "इस गोदाम के द्वारा वर्ष " + ddl_gdwn.SelectedItem.Value + " की एंट्री गड़ना पत्रक में नहीं की गई हैं ,पहले गोदाम के द्वारा गड़ना पत्रक की एंट्री करनी होगी |";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "')", true);
        }
    }
    protected void GD_StackBal_RowDataBound(object sender, GridViewRowEventArgs e)
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
                divbtn.Visible = true;
                btn_FinalSubmit.Visible = true;
                divremark.Visible = true;
                //divremark.Visible = true;
                //txtremark.Visible = true;
                //btnPrint.Visible = false;
            }
            else if (hdnFinalsubmit.Value == "1")
            {
                divbtn.Visible = false;
                btn_FinalSubmit.Visible = false;
                divremark.Visible = false;
                //divremark.Visible = false;
                //txtremark.Visible = false;
                // btnPrint.Visible = true;
            }
        }
    }
    protected void GD_StackBal_RowCommand(object sender, GridViewCommandEventArgs e)
    {

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
    protected void GD_StackBal_RowCreated(object sender, GridViewRowEventArgs e)
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

            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal = 0;
            qtyTotal1 = 0;
            qtyTotal2 = 0;
            qtyTotal3 = 0;
            qtyTotal4 = 0;
            qtyTotal5 = 0;
        }
    }

    protected void btn_FinalSubmit_Click(object sender, EventArgs e)
    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];

        try
        {
            string ErrorMsg = "";
            ErrorMsg += !string.IsNullOrEmpty(txtremark.Text) ? "" : "Enter Remark \\n";
            if (ErrorMsg == "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Special_PV_Final_Submit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                //tn = con.BeginTransaction();
                //cmd.Transaction = tn;
                cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@BM_Remark", txtremark.Text);
                cmd.Parameters.AddWithValue("@Final_Submit_By", Session["UserId"].ToString());
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "data has been final submitted successfully";
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');", true);
                    fillScheduleInsp_Grid();
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
            // tn.Rollback();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
    }
}