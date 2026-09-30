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
using System.Configuration;
using System;
using System.Collections;

public partial class Inspections_RO_View_Insp : System.Web.UI.Page
{
    public SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    string PFID = "";
    SqlTransaction sqltran;
    string client_IP = "";
    int a_id = 0;
    string Region_ID = "";
    string Insp_ID = "";
    string Veri_ID = "";

    int AqtyTotal = 0;
    int AgrQtyTotal = 0;
    decimal AqtyTotal1 = 0;
    decimal AqtyTotal2 = 0;
    decimal AqtyTotal3 = 0;
    decimal AqtyTotal4 = 0;
    decimal AqtyTotal5 = 0;

    decimal AgrQtyTotal1 = 0;
    decimal AgrQtyTotal2 = 0;
    decimal AgrQtyTotal3 = 0;
    decimal AgrQtyTotal4 = 0;
    decimal AgrQtyTotal5 = 0;

    long Astorid = 0;
    int ArowIndex = 1;

    int BqtyTotal = 0;
    int BgrQtyTotal = 0;
    decimal BqtyTotal1 = 0;
    decimal BqtyTotal2 = 0;
    decimal BqtyTotal3 = 0;
    decimal BqtyTotal4 = 0;
    decimal BqtyTotal5 = 0;

    decimal BgrQtyTotal1 = 0;
    decimal BgrQtyTotal2 = 0;
    decimal BgrQtyTotal3 = 0;
    decimal BgrQtyTotal4 = 0;
    decimal BgrQtyTotal5 = 0;

    //long storid = 0;
    long Bstorid = 0;
    int BrowIndex = 1;

    int CuntyqtyTotal = 0;
    int CuntygrQtyTotal = 0;
    decimal CuntyqtyTotal1 = 0;
    decimal CuntyqtyTotal2 = 0;
    decimal CuntyqtyTotal3 = 0;
    decimal CuntyqtyTotal4 = 0;
    decimal CuntyqtyTotal5 = 0;

    decimal CuntygrQtyTotal1 = 0;
    decimal CuntygrQtyTotal2 = 0;
    decimal CuntygrQtyTotal3 = 0;
    decimal CuntygrQtyTotal4 = 0;
    decimal CuntygrQtyTotal5 = 0;

    long Cuntystorid = 0;
    long Cuntygodownid = 0;
    int CuntyrowIndex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        if (!IsPostBack)
        {
            lblname.Text = Session["hdnemployeename"].ToString();
            lblbranchname.Text = Session["lblBranchName"].ToString();
            lblInsBranch.Text = Session["hdnbranchname"].ToString();
            lblInstype.Text = Session["lblInsp_Type"].ToString();
            lblInstype2.Text = Session["lblInsp_Type"].ToString();
            lblorderdate.Text = Session["lblOrder_Date"].ToString();
            lbldesignation.Text = Session["hdndesignation"].ToString();
            lblorderno.Text = Session["lblOrder_No"].ToString();
            todate.Text = Session["lblFinalSubmitDate"].ToString();
            fillScheduleInsp_Grid();
            FillAnnaxureBgrd();
            FillAnnaxureCgrd();
            FillGandanPatrak();
            fillAnnaxureADetails();
            FillAnnaxureBDetails();
            GetdataForGrid();
            fillCountingSheat();
            FillAllAnnaxuresRemark();
        }
    }
    protected void FillAllAnnaxuresRemark()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_All_Annaxures_Remark", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@Inspection_type_ID", Session["hdnQuarterID"].ToString());
                cmd.Parameters.AddWithValue("@verificatiotype", Session["hdnVerificationType"].ToString());
                //  cmd.Parameters.AddWithValue("@Year", ddlyear.SelectedValue);
                cmd.Parameters.AddWithValue("@PF_ID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@Financial_Year", Session["hdnFinancial_Year"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            txtRemark.Text = dt.Rows[0]["RemarkA"].ToString();
                            txtremarkB.Text = dt.Rows[0]["RemarkB"].ToString();
                            txtremarkc.Text = dt.Rows[0]["RemarkC"].ToString();
                            txtgadnapatrak.Text = dt.Rows[0]["RemarkGP"].ToString();
                            
                        }
                    }
                }
            }
        }
    }
    protected void fillAnnaxureADetails()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Annaxur_A_Data_For_HO", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@Quaterid", Session["hdnQuarterID"].ToString());
                cmd.Parameters.AddWithValue("@Insp_Typeid", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@FinacialYear", Session["hdnFinancial_Year"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            //GrdOfficerPreviousInsp.Caption = @"<b style=""font-weight: bold;"">Branch Name: " + "  -   " + ddlbranch.SelectedItem.ToString() + "</b> ";
                            divshow.Visible = true;

                           // btnPrint.Visible = true;
                            AnnaxureADetaisls.DataSource = dt;
                            AnnaxureADetaisls.DataBind();
                            AnnaxureADetaisls.Columns[1].Visible = false;
                            AnnaxureADetaisls.Caption = @"<b style=""font-weight: bold;"">Annaxure A Details Godown Wise: " + "</b> ";
                            AnnaxureADetaisls.FooterRow.Style.Add("text-align", "center");
                            AnnaxureADetaisls.FooterRow.Cells[5].Text = "Total";
                            AnnaxureADetaisls.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("AvlBags")).ToString();
                            AnnaxureADetaisls.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlQty")).ToString();
                            AnnaxureADetaisls.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("No_of_bage_in_PV")).ToString();
                            AnnaxureADetaisls.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Spilage_Bags")).ToString();
                            AnnaxureAdetails(AnnaxureADetaisls.Rows, 0, 0);
                        }
                        else
                        {
                            divshow.Visible = false;
                           // btnPrint.Visible = false;
                            AnnaxureADetaisls.DataSource = null;
                            AnnaxureADetaisls.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void AnnaxureADetaisls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Astorid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Godown_ID").ToString());
            int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "AvlBags").ToString());
            decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AvlQty").ToString());
            int tmpTotal2 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "No_of_bage_in_PV").ToString());
            int tmpTotal3 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Spilage_Bags").ToString());

            AqtyTotal += tmpTotal;
            AqtyTotal1 += tmpTotal1;
            AqtyTotal2 += tmpTotal2;
            AqtyTotal3 += tmpTotal3;

            AgrQtyTotal += tmpTotal;
            AgrQtyTotal1 += tmpTotal1;
            AgrQtyTotal2 += tmpTotal2;
            AgrQtyTotal3 += tmpTotal3;

        }

    }
    protected void AnnaxureADetaisls_RowCommand(object sender, GridViewCommandEventArgs e)
    {


    }
    protected void AnnaxureADetaisls_RowCreated(object sender, GridViewRowEventArgs e)
    {

        bool newRow = false;

        if ((Astorid > 0) && (DataBinder.Eval(e.Row.DataItem, "Godown_ID") != null))
        {
            if (Astorid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Godown_ID").ToString()))
                newRow = true;
        }
        if ((Astorid > 0) && (DataBinder.Eval(e.Row.DataItem, "Godown_ID") == null))
        {
            newRow = true;
            ArowIndex = 0;
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
            HeaderCell.ColumnSpan = 5;


            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = AqtyTotal.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            //HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 3;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = AqtyTotal1.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            // HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 4;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = AqtyTotal2.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = AqtyTotal3.ToString();
            NewTotalRow.Cells.Add(HeaderCell);


            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + ArowIndex, NewTotalRow);
            ArowIndex++;
            AqtyTotal = 0;
            AqtyTotal1 = 0;
            AqtyTotal2 = 0;
            AqtyTotal3 = 0;

        }

    }
    void AnnaxureAdetails(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
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
                    AnnaxureAdetails(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
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
            AnnaxureAdetails(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
        }
        count = 1;
        lst.Clear();
    }
    protected void GrdOfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = grdannaxureB.Rows[rowIndex];

            //Fetch value of Name.
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnQuarterID = (row.FindControl("hdnQuarterID") as HiddenField).Value;
            string hdnVerificationType = (row.FindControl("hdnVerificationType") as HiddenField).Value;
            string hdnemployeeid = (row.FindControl("hdnemployeeid") as HiddenField).Value;
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnQuarterID"] = hdnQuarterID.ToString();
            Session["hdnVerificationType"] = hdnVerificationType.ToString();
            Session["hdnemployeeid"] = hdnemployeeid.ToString();
            // Response.Redirect("/warehouse/Inspections/BO/Owned_Edit_Bolck_Wise_Entry.aspx");
            Page.ClientScript.RegisterStartupScript(
  this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/Reports/View_Annaxure_A.aspx','_newtab');", true);
        }
    }
    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Summary_Reports_Employee_Wise_Annaxure_A", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@QuarterID", Session["hdnQuarterID"].ToString());
                cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@FinacialYear", Session["hdnFinancial_Year"].ToString());
                cmd.Parameters.AddWithValue("@OrderNo", Session["lblOrder_No"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                            GrdOfficerPreviousInsp.Caption = @"<b style=""font-weight: bold;"">Annaxure A: " + "</b> ";
                            GrdOfficerPreviousInsp.DataSource = dt;
                            GrdOfficerPreviousInsp.DataBind();

                        }
                        else
                        {

                            GrdOfficerPreviousInsp.DataSource = null;
                            GrdOfficerPreviousInsp.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void grdannaxureB_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = grdannaxureB.Rows[rowIndex];

            //Fetch value of Name.
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnQuarterID = (row.FindControl("hdnQuarterID") as HiddenField).Value;
            string hdnVerificationType = (row.FindControl("hdnVerificationType") as HiddenField).Value;
            string hdnemployeeid = (row.FindControl("hdnemployeeid") as HiddenField).Value;
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnQuarterID"] = hdnQuarterID.ToString();
            Session["hdnVerificationType"] = hdnVerificationType.ToString();
            Session["hdnemployeeid"] = hdnemployeeid.ToString();
            // Response.Redirect("/warehouse/Inspections/BO/Owned_Edit_Bolck_Wise_Entry.aspx");
            Page.ClientScript.RegisterStartupScript(
  this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/Reports/View_Annaxure_B.aspx','_newtab');", true);
        }
    }
    protected void FillAnnaxureBgrd()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Summary_Reports_Employee_Wise_Annaxure_B", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@QuarterID", Session["hdnQuarterID"].ToString());
                cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@FinacialYear", Session["hdnFinancial_Year"].ToString());
                cmd.Parameters.AddWithValue("@OrderNo", Session["lblOrder_No"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                            grdannaxureB.Caption = @"<b style=""font-weight: bold;"">Annaxure B: " + "</b> ";
                            grdannaxureB.DataSource = dt;
                            grdannaxureB.DataBind();

                        }
                        else
                        {

                            grdannaxureB.DataSource = null;
                            grdannaxureB.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void GrdAnnaxureC_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = grdannaxureB.Rows[rowIndex];

            //Fetch value of Name.
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnQuarterID = (row.FindControl("hdnQuarterID") as HiddenField).Value;
            string hdnVerificationType = (row.FindControl("hdnVerificationType") as HiddenField).Value;
            string hdnemployeeid = (row.FindControl("hdnemployeeid") as HiddenField).Value;
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnQuarterID"] = hdnQuarterID.ToString();
            Session["hdnVerificationType"] = hdnVerificationType.ToString();
            Session["hdnemployeeid"] = hdnemployeeid.ToString();
            // Response.Redirect("/warehouse/Inspections/BO/Owned_Edit_Bolck_Wise_Entry.aspx");
            Page.ClientScript.RegisterStartupScript(
  this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/Reports/View_Annaxure_C.aspx','_newtab');", true);
        }
    }
    protected void FillAnnaxureCgrd()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Summary_Reports_Employee_Wise_Annaxure_C", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@QuarterID", Session["hdnQuarterID"].ToString());
                cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@FinacialYear", Session["hdnFinancial_Year"].ToString());
                //cmd.Parameters.AddWithValue("@OrderNo", Session["lblOrder_No"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                            GrdAnnaxureC.Caption = @"<b style=""font-weight: bold;"">Annaxure C: " + "</b> ";
                            GrdAnnaxureC.DataSource = dt;
                            GrdAnnaxureC.DataBind();

                        }
                        else
                        {

                            GrdAnnaxureC.DataSource = null;
                            GrdAnnaxureC.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void Grddagnapatrak_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = grdannaxureB.Rows[rowIndex];

            //Fetch value of Name.
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnQuarterID = (row.FindControl("hdnQuarterID") as HiddenField).Value;
            string hdnVerificationType = (row.FindControl("hdnVerificationType") as HiddenField).Value;
            string hdnemployeeid = (row.FindControl("hdnemployeeid") as HiddenField).Value;
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnQuarterID"] = hdnQuarterID.ToString();
            Session["hdnVerificationType"] = hdnVerificationType.ToString();
            Session["hdnemployeeid"] = hdnemployeeid.ToString();
            // Response.Redirect("/warehouse/Inspections/BO/Owned_Edit_Bolck_Wise_Entry.aspx");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/Reports/View_Gadna_Patrak_For_HO.aspx','_newtab');", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/Reports/Akhilesh_Bhai.aspx','_newtab');", true);
        }
    }
    protected void FillGandanPatrak()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Summary_Reports_Employee_Wise_Gadna_Patrak", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@QuarterID", Session["hdnQuarterID"].ToString());
                cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@FinacialYear", Session["hdnFinancial_Year"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                            Grddagnapatrak.Caption = @"<b style=""font-weight: bold;"">Counting Sheet: " + "</b> ";
                            Grddagnapatrak.DataSource = dt;
                            Grddagnapatrak.DataBind();

                        }
                        else
                        {

                            Grddagnapatrak.DataSource = null;
                            Grddagnapatrak.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
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
    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
        FillAnnaxureBgrd();
        FillAnnaxureCgrd();
        FillGandanPatrak();
    }

    //AddAnnaxureA B Details

    protected void FillAnnaxureBDetails()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Data_Annaxure_B_All_Godown", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@Quaterid", Session["hdnQuarterID"].ToString());
                cmd.Parameters.AddWithValue("@Insp_Typeid", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@FinacialYear", Session["hdnFinancial_Year"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            divshow.Visible = true;
                            // GrdOfficerPreviousInsp.Caption = @"<b style=""font-weight: bold;"">Inspection Officer Name: " + "  -   " + Session["lblOfficer_Name"].ToString() + " , " + "Branch Name:" + dt.Rows[0]["DepotName"].ToString() + "</b> ";
                            AnnaxureBDetails.DataSource = dt;
                            AnnaxureBDetails.DataBind();
                            AnnaxureBDetails.Caption = @"<b style=""font-weight: bold;"">Annaxure B Details Godown Wise: " + "</b> ";
                            AnnaxureBDetails.FooterRow.Style.Add("text-align", "center");
                            AnnaxureBDetails.FooterRow.Cells[6].Text = "Total";
                            AnnaxureBDetails.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Available_Bags")).ToString();
                            AnnaxureBDetails.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Spillage_bags")).ToString();
                            AnnaxureBDetails.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PV_Bags")).ToString();
                            ShowingGroupingDataInGridViewAnnaxureBDetails(AnnaxureBDetails.Rows, 0, 0);
                        }
                        else
                        {
                            divshow.Visible = false;
                            AnnaxureBDetails.DataSource = null;
                            AnnaxureBDetails.DataBind();
                            // string strMsg = "गोदाम " + ddl_gdwn.SelectedItem.ToString() + " की पहले एंट्री करे |";
                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "')", true);
                        }
                    }
                }
            }
        }
    }
    protected void AnnaxureBDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Bstorid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Godown_ID").ToString());
            int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Available_Bags").ToString());
            int tmpTotal1 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Spillage_bags").ToString());
            int tmpTotal2 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PV_Bags").ToString());

            BqtyTotal += tmpTotal;
            BqtyTotal1 += tmpTotal1;
            BqtyTotal2 += tmpTotal2;


            BgrQtyTotal += tmpTotal;
            BgrQtyTotal1 += tmpTotal1;
            BgrQtyTotal2 += tmpTotal2;


        }
    }
    void ShowingGroupingDataInGridViewAnnaxureBDetails(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
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
                    ShowingGroupingDataInGridViewAnnaxureBDetails(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
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
            ShowingGroupingDataInGridViewAnnaxureBDetails(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
        }
        count = 1;
        lst.Clear();
    }


    protected void AnnaxureBDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {

        bool newRow = false;

        if ((Bstorid > 0) && (DataBinder.Eval(e.Row.DataItem, "Godown_ID") != null))
        {
            if (Bstorid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Godown_ID").ToString()))
                newRow = true;
        }
        if ((Bstorid > 0) && (DataBinder.Eval(e.Row.DataItem, "Godown_ID") == null))
        {
            newRow = true;
            BrowIndex = 0;
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
            HeaderCell.ColumnSpan = 7;


            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = BqtyTotal.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            //HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 3;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = BqtyTotal1.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            // HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 4;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = BqtyTotal2.ToString();
            NewTotalRow.Cells.Add(HeaderCell);


            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + BrowIndex, NewTotalRow);
            BrowIndex++;
            BqtyTotal = 0;
            BqtyTotal1 = 0;
            BqtyTotal2 = 0;

        }

    }

    // Annaxure C Details

    public void GetdataForGrid()
    {
        SqlCommand cmd = new SqlCommand("[dbo].[Get_Annaxure_C_For_HO]", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
        //cmd.Parameters.AddWithValue("@Quaterid", Session["hdnqauterid"].ToString());
        //cmd.Parameters.AddWithValue("@Insp_Typeid", Session["hdnverificationid"].ToString());
        //cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnempid"].ToString());

        cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
        cmd.Parameters.AddWithValue("@Quaterid", Session["hdnQuarterID"].ToString());
        cmd.Parameters.AddWithValue("@Insp_Typeid", Session["hdnVerificationType"].ToString());
        cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnemployeeid"].ToString());
        cmd.Parameters.AddWithValue("@FinacialYear", Session["hdnFinancial_Year"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //tr_griddata.Visible = true;
            //btnPrint.Visible = true;
            Grd_AnnaxureC.DataSource = dt;
            Grd_AnnaxureC.DataBind();
            // GD_StackBal.Caption = @"<b style=""font-weight: bold;"">Godown Name: " + "  -   " + ddl_gdwn.SelectedItem.ToString() + "</b> ";
            Grd_AnnaxureC.FooterRow.Style.Add("text-align", "center");
            Grd_AnnaxureC.FooterRow.Cells[5].Text = "Total";
            Grd_AnnaxureC.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Avl_Bags")).ToString();
            Grd_AnnaxureC.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Avl_Quantity")).ToString();

        }
        else
        {
            //tr_griddata.Visible = false;
            //btnPrint.Visible = false;
            Grd_AnnaxureC.DataSource = null;
            Grd_AnnaxureC.DataBind();
        }
    }
    protected void Grd_AnnaxureC_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    // Counting Sheat Details

    public void fillCountingSheat()
    {
        SqlCommand cmd = new SqlCommand("Get_Gadna_Patrak_For_HO", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
        //cmd.Parameters.AddWithValue("@Quaterid", Session["hdnqauterid"].ToString());
        //cmd.Parameters.AddWithValue("@Insp_Typeid", Session["hdnverificationid"].ToString());
        //cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnempid"].ToString());

        cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
        cmd.Parameters.AddWithValue("@Quaterid", Session["hdnQuarterID"].ToString());
        cmd.Parameters.AddWithValue("@Insp_Typeid", Session["hdnVerificationType"].ToString());
        cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnemployeeid"].ToString());
        cmd.Parameters.AddWithValue("@FinacialYear", Session["hdnFinancial_Year"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //   GD_StackBal.Caption = @"<b style=""font-weight: bold;"">Godown Name: " + "  -   " + ddl_gdwn.SelectedItem.ToString() + "</b> ";
            divshow.Visible = true;
            GD_StackBal.Columns[1].Visible = false;
            GD_StackBal.DataSource = dt;
            GD_StackBal.DataBind();
            GD_StackBal.Caption = @"<b style=""font-weight: bold;"">Counting Sheat Details Godown Wise: " + "</b> ";
            GD_StackBal.FooterRow.Style.Add("text-align", "center");
            GD_StackBal.FooterRow.Cells[12].Text = "Total";
            GD_StackBal.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("No_of_Bags")).ToString();
            GD_StackBal.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Up")).ToString();
            GD_StackBal.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Below")).ToString();
            GD_StackBal.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Bags")).ToString();
            GD_StackBal.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Spillage_bag")).ToString();
            ShowingGroupingDataInGridViewCountingsheat(GD_StackBal.Rows, 0, 0);
        }
        else
        {
            divshow.Visible = true;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
            //string strMsg = "इस गोदाम के द्वारा वर्ष " + ddl_gdwn.SelectedValue.ToString() + " की एंट्री गड़ना पत्रक में नहीं की गई हैं ,पहले गोदाम के द्वारा गड़ना पत्रक की एंट्री करनी होगी |";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "')", true);
        }
    }

    protected void GD_StackBal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Cuntystorid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "stack_id").ToString());
            Cuntygodownid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Godown_ID").ToString());
            int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Number_Of_Block").ToString());
            int tmpTotal1 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "No_of_Bags").ToString());
            int tmpTotal2 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Up").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Below").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total_Bags").ToString());
            int tmpTotal5 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Spillage_bag").ToString());

            CuntyqtyTotal += tmpTotal;
            CuntyqtyTotal1 += tmpTotal1;
            CuntyqtyTotal2 += tmpTotal2;
            CuntyqtyTotal3 += tmpTotal3;
            CuntyqtyTotal4 += tmpTotal4;
            CuntyqtyTotal5 += tmpTotal5;

            CuntygrQtyTotal += tmpTotal;
            CuntygrQtyTotal1 += tmpTotal1;
            CuntygrQtyTotal2 += tmpTotal2;
            CuntygrQtyTotal3 += tmpTotal3;
            CuntygrQtyTotal4 += tmpTotal4;
            CuntygrQtyTotal5 += tmpTotal5;

        }

    }
    protected void GD_StackBal_RowCommand(object sender, GridViewCommandEventArgs e)
    {


    }
    protected void GD_StackBal_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;

        //if ((Cuntystorid >= 0) && (DataBinder.Eval(e.Row.DataItem, "stack_id") != null))
        //{
        //    if (Cuntystorid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "stack_id").ToString()))
        //        newRow = true;
        //}
        //if ((Cuntystorid >= 0) && (DataBinder.Eval(e.Row.DataItem, "stack_id") == null) && (Cuntygodownid > 0))
        //{
        //    newRow = true;
        //    CuntyrowIndex = 0;
        //}
        //if (newRow)
        //{
        //    GridView GridView1 = (GridView)sender;
        //    GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
        //    NewTotalRow.Font.Bold = true;
        //    // NewTotalRow.BackColor = System.Drawing.Color.Gray;
        //    NewTotalRow.ForeColor = System.Drawing.Color.Black;
        //    TableCell HeaderCell = new TableCell();
        //    HeaderCell.Text = "Sub Total";
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.ColumnSpan = 12;


        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.Text = CuntyqtyTotal.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    //HeaderCell.HorizontalAlign = HorizontalAlign.Left;
        //    //HeaderCell.ColumnSpan = 3;
        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.Text = CuntyqtyTotal1.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    // HeaderCell.HorizontalAlign = HorizontalAlign.Left;
        //    //HeaderCell.ColumnSpan = 4;
        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.Text = CuntyqtyTotal2.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.Text = CuntyqtyTotal3.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.Text = CuntyqtyTotal4.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.Text = CuntyqtyTotal5.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + CuntyrowIndex, NewTotalRow);
        //    CuntyrowIndex++;
        //    CuntyqtyTotal = 0;
        //    CuntyqtyTotal1 = 0;
        //    CuntyqtyTotal2 = 0;
        //    CuntyqtyTotal3 = 0;
        //    CuntyqtyTotal4 = 0;
        //    CuntyqtyTotal5 = 0;


        //}
        if ((Cuntygodownid > 0) && (DataBinder.Eval(e.Row.DataItem, "Godown_ID") != null))
        {
            if (Cuntygodownid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Godown_ID").ToString()))
                newRow = true;
        }
        if ((Cuntygodownid > 0) && (DataBinder.Eval(e.Row.DataItem, "Godown_ID") == null))
        {
            newRow = true;
            CuntyrowIndex = 0;
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
            HeaderCell.Text = CuntyqtyTotal.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            //HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 3;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = CuntyqtyTotal1.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            // HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 4;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = CuntyqtyTotal2.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = CuntyqtyTotal3.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = CuntyqtyTotal4.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = CuntyqtyTotal5.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + CuntyrowIndex, NewTotalRow);
            CuntyrowIndex++;
            CuntyqtyTotal = 0;
            CuntyqtyTotal1 = 0;
            CuntyqtyTotal2 = 0;
            CuntyqtyTotal3 = 0;
            CuntyqtyTotal4 = 0;
            CuntyqtyTotal5 = 0;


        }
    }
    void ShowingGroupingDataInGridViewCountingsheat(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
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
                    ShowingGroupingDataInGridViewCountingsheat(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
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
            ShowingGroupingDataInGridViewCountingsheat(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
        }
        count = 1;
        lst.Clear();
    }
}
