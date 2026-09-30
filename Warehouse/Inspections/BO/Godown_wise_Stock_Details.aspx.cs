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

public partial class Inspections_BO_Godown_wise_Stock_Details : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
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
            // fillCropYear();
            fillGodownDetails();
            fillFinsncilYear();
        }
    }
    public void fillFinsncilYear()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Fianancial_Year_For_inspection", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            ddlfinancialyear.DataSource = cmd.ExecuteReader();
            ddlfinancialyear.DataTextField = "Financial_Year";
            ddlfinancialyear.DataValueField = "Financial_Year";
            ddlfinancialyear.DataBind();
            ddlfinancialyear.Items.Insert(0, new ListItem("--Select Financial Year--", "0"));
            con.Close();
        }
    }
    public void fillGodownDetails()
    {
        string conStr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr2))
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
            // ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con.Close();
        }
    }
    public void fillScheduleInsp_Grid()
    {
       // SqlCommand cmd = new SqlCommand("Rpt_Insp_Get_Stack_Block_Wise_Details", conStr);
        SqlCommand cmd = new SqlCommand("Get_Gadna_Patrak_For_BO", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
       // cmd.Parameters.AddWithValue("@cropyear", ddlcropyear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GD_StackBal.Caption = @"<b style=""font-weight: bold;"">Godown Name: " + "  -   " + ddl_gdwn.SelectedItem.ToString() + "</b> ";
            divshow.Visible = true;
            divbtn.Visible = true;
            GD_StackBal.DataSource = dt;
            GD_StackBal.DataBind();
            GD_StackBal.FooterRow.Style.Add("text-align", "center");
            GD_StackBal.FooterRow.Cells[11].Text = "Total";
            GD_StackBal.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<int>("No_of_Bags")).ToString();
            GD_StackBal.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Up")).ToString();
            GD_StackBal.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Below")).ToString();
            GD_StackBal.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Bags")).ToString();
            GD_StackBal.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Spillage_bag")).ToString();
            ShowingGroupingDataInGridView(GD_StackBal.Rows, 0, 0);
        }
        else
        {
            divbtn.Visible = false;
            divshow.Visible = true;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
            string strMsg = "इस गोदाम के द्वारा वर्ष " + ddl_gdwn.SelectedItem.Value + " की एंट्री गड़ना पत्रक में नहीं की गई हैं ,पहले गोदाम के द्वारा गड़ना पत्रक की एंट्री करनी होगी |";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "')", true);
            
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (ddlfinancialyear.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Financial Year!....')", true);
            ddlfinancialyear.Focus();
            return;
        }
        else
        {
            fillScheduleInsp_Grid();
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
            Button btnRemove = (Button)e.Row.FindControl("btnRemove");
            Button btnEdit = (Button)e.Row.FindControl("btnEdit");
            if (hdnFinalsubmit.Value == "0")
            {
               
                divbtn.Visible = true;
                btnPrint.Visible = false;
            }
            else if (hdnFinalsubmit.Value == "1")
            {
               
                divbtn.Visible = false;
                btnPrint.Visible = true;
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

        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "stack_id") != null) )
        {
            if (storid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "stack_id").ToString()))
                newRow = true;
        }
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "stack_id") == null) )
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
            HeaderCell.ColumnSpan = 11;


            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = qtyTotal.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            //HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 3;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = qtyTotal1.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            // HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 4;
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
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    GridView HeaderGrid = (GridView)sender;
        //    GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
        //    TableCell HeaderCell = new TableCell();

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "";
        //    HeaderCell.ColumnSpan = 6;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "लम्बाई";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "चौड़ाई ";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "अतिरिक्त";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "ऊपर";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "निचे";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);


        //    GD_StackBal.Controls[0].Controls.AddAt(0, HeaderGridRow);
        //    HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        //}
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    GridView HeaderGrid = (GridView)sender;
        //    GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
        //    TableCell HeaderCell = new TableCell();

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "क्रमांक";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "जमाकर्ता का नाम";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "स्कंध का नाम";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "वर्ष";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "स्टैक आईडी";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "स्टैक नाम";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "स्टेक प्लानिंग बिछान";
        //    HeaderCell.ColumnSpan = 3;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "योग (7+8+9)";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "बोरो के लेयर की ऊंचाई";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "ब्लॉक क्र./संख्या";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "बोरियो की संख्या (10*11*12)";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "अतिरिक्त पाई गई बोरियो की संख्या";
        //    HeaderCell.ColumnSpan = 2;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "टोटल बौरे";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Spillage Bag";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);


        //    GD_StackBal.Controls[0].Controls.AddAt(0, HeaderGridRow);
        //    HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        //}
    }

    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        //SqlTransaction tn = null;
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];

        try
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Insp_Stack_Block_Wise_Godown_Entry_Final_Submit", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            //tn = con.BeginTransaction();
            //cmd.Transaction = tn;
            cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
            cmd.Parameters.AddWithValue("@FinancialYear", ddlfinancialyear.SelectedValue);
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
        catch (Exception ex)
        {
            // tn.Rollback();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }

    }
}