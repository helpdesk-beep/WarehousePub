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
public partial class Special_PV_BO_Special_PV : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
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

    long storid = 0;
    int rowIndex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        if (Session["role"] != null)
        {
            if (!IsPostBack)
            {
                fillScheduleInsp_Grid();
                // fillEMPDetails();
                //fillFinsncilYear();
                //lbl_user.Text = Session["UserName"].ToString();
            }
        }
        else
        {
            Session.Abandon();
            Response.Redirect("/Warehouse/Special_PV/Special_PV_Default.aspx");
        }
    }
    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_stock_Position_Branch_Wise_For_Spicial_PV", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            //divshow.Visible = true;
                            divshow1.Visible = true;
                            GrdOfficerPreviousInsp.Columns[1].Visible = false;
                            GrdOfficerPreviousInsp.DataSource = dt;
                            GrdOfficerPreviousInsp.DataBind();
                            GrdOfficerPreviousInsp.FooterRow.Style.Add("text-align", "center");
                            GrdOfficerPreviousInsp.FooterRow.Cells[4].Text = "Total";
                            GrdOfficerPreviousInsp.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("RecBags")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("delbags")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Rec_Weight")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Del_Weight")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Bag_Balance")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Balance")).ToString();
                            // lblTotalInsp.Text = Convert.ToString(dt.Rows[0].Count);
                            //this.GrdOfficerPreviousInsp.Columns[12].Visible = false;
                            //this.GrdOfficerPreviousInsp.Columns[13].Visible = false;
                            ShowingGroupingDataInGridView(GrdOfficerPreviousInsp.Rows, 0, 10);
                        }
                        else
                        {
                            //divshow.Visible = false;
                            divshow1.Visible = false;
                            GrdOfficerPreviousInsp.DataSource = null;
                            GrdOfficerPreviousInsp.DataBind();
                            // string strMsg = "गोदाम में वर्ष " + ddlcropyear.SelectedItem.Value + " का स्टॉक उपलब्ध नहीं हैं  |";
                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "')", true);
                        }
                    }
                }
            }
        }
    }


    protected void GrdOfficerPreviousInsp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Godown_ID").ToString());
            int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "recbags").ToString());
            int tmpTotal1 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "delbags").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Rec_Weight").ToString());
            //decimal tmpTotal2 =Math.Round(Convert.ToDecimal(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OpeningQty").ToString()))/ Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OpeningBags").ToString()),2);
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Del_Weight").ToString());
            int tmpTotal4 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bag_Balance").ToString());
            decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance").ToString());


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


    }
    protected void GrdOfficerPreviousInsp_RowCreated(object sender, GridViewRowEventArgs e)
    {

        bool newRow = false;

        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "Godown_ID") != null))
        {
            if (storid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Godown_ID").ToString()))
                newRow = true;
        }
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "Godown_ID") == null))
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
            HeaderCell.ColumnSpan = 4;


            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            //HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 3;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal1.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            // HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 4;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal2.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal3.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal4.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
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
    protected void GrdOfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    //public void fillFinsncilYear()
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        SqlCommand cmd = new SqlCommand("Get_Fianancial_Year_For_inspection", con);
    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        con.Open();
    //        ddlfinancialyear.DataSource = cmd.ExecuteReader();
    //        ddlfinancialyear.DataTextField = "Financial_Year";
    //        ddlfinancialyear.DataValueField = "Financial_Year";
    //        ddlfinancialyear.DataBind();
    //        ddlfinancialyear.Items.Insert(0, new ListItem("--Select Financial Year--", "0"));
    //        con.Close();
    //    }
    //}
    //public void fillEMPDetails()
    //{
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        SqlCommand cmd = new SqlCommand("Get_Employee_For_Branch_Wise", con);
    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
    //        con.Open();
    //        ddlemp.DataSource = cmd.ExecuteReader();
    //        ddlemp.DataTextField = "Officer_Name";
    //        ddlemp.DataValueField = "Employee_ID";
    //        ddlemp.DataBind();
    //        ddlemp.Items.Insert(0, new ListItem("-- Select Employee --", "0"));
    //        con.Close();
    //    }
    //}
    //protected void btnupdatereg_Click(object sender, EventArgs e)
    //{
    //    //if (ddlemp.SelectedValue == "0")
    //    //{
    //    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Employee Name')", true);
    //    //    ddlemp.Focus();
    //    //    return;
    //    //}

    //    if (ddlverification.SelectedValue == "0")
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Inspection Type')", true);
    //        ddlverification.Focus();
    //        return;
    //    }
    //    //if (ddlquater.SelectedValue == "0")
    //    //{
    //    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Quarter')", true);
    //    //    ddlquater.Focus();
    //    //    return;
    //    //}

    //    if (string.IsNullOrEmpty(txtdob.Text))
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Inspection Date')", true);
    //        txtdob.Focus();
    //        return;
    //    }
    //    if (ddlfinancialyear.SelectedValue == "0")
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Financial Year')", true);
    //        ddlfinancialyear.Focus();
    //        return;
    //    }
    //    else
    //    {
    //        fillgrid();
    //    }
    //}
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("State_ViewFillAnexB.aspx");
    //}
    //protected string getDate_MDY(string inDate)
    //{
    //    if (inDate == "" || inDate == null)
    //    {
    //        return "01/01/1919";
    //    }
    //    else
    //    {
    //        //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
    //        string converted = "";
    //        string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
    //        converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
    //        return converted;
    //    }
    //}
    //protected void fillgrid()
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("Sync_Stack_Wise_Data_Insert_Inspection_officer_For_Special_PV", con))
    //        {
    //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
    //            cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtdob.Text));
    //            cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
    //            //cmd.Parameters.AddWithValue("@Employee_ID", ddlemp.SelectedValue);
    //            cmd.Parameters.AddWithValue("@Inspection_Type_ID", ddlverification.SelectedValue);
    //            //cmd.Parameters.AddWithValue("@Quater_Type", ddlquater.SelectedValue);
    //            using (SqlDataAdapter sda = new SqlDataAdapter())
    //            {
    //                cmd.Connection = con;
    //                sda.SelectCommand = cmd;
    //                using (DataTable dt = new DataTable())
    //                {

    //                    sda.Fill(dt);
    //                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('data inserted successfully !')", true);
    //                }
    //            }
    //        }
    //    }
    //}
    //public void CheckAllreadyExist()
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //    SqlConnection con = new SqlConnection(constr);
    //    SqlCommand cmd = new SqlCommand("[dbo].[Check_Data_in_Sync_table_For_Special_PV]", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
    //    cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
    //    //cmd.Parameters.AddWithValue("@Employee_ID", ddlemp.SelectedValue);
    //    cmd.Parameters.AddWithValue("@Inspection_Type_ID", ddlverification.SelectedValue);
    //   // cmd.Parameters.AddWithValue("@Quater_Type", ddlquater.SelectedValue);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        // txt_inspdate.Text = dt.Rows[0]["Inspection_Date"].ToString();
    //        if (dt.Rows[0]["Employee_ID"].ToString() == "YES")
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
    //        }
    //        else if (dt.Rows[0]["Employee_ID"].ToString() == "NO")
    //        {
    //            fillgrid();
    //        }
    //    }

    //}
}