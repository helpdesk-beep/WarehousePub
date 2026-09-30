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

public partial class Accounting_frm_Business_Prapatra_7 : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string yesdate = "";
    string Todaydate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                GetEmployeeDetails();
                yesdate= DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                Session["yesdate"] = yesdate.ToString();
                Todaydate = DateTime.Now.AddDays(0).ToString("dd/MM/yyyy");
                Session["Todaydate"] = Todaydate.ToString();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void GetEmployeeDetails()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Business_Prapatra_7", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branchid", Session["BranchId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            GrdPrapatraI.DataSource = ds;
                            GrdPrapatraI.DataBind();

                        }
                        else
                        {
                            GrdPrapatraI.DataSource = null;
                            GrdPrapatraI.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void GrdPrapatraI_RowCreated(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "S.No.";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Branch Name";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC+HIRED+JVS";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "PVT PEG";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MARKFED";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "QMANDI BOARD";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "OILFED";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "CWC";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "TOTAL(3 To 8)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC+HIRED+JVS";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "PVT PEG";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MARKFED";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "QMANDI BOARD";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "OILFED";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "CWC";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "TOTAL(10 To 15)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC+HIRED+JVS";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "PVT PEG";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MARKFED";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "QMANDI BOARD";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "OILFED";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "CWC";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "TOTAL(17 To 22)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            GrdPrapatraI.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "विगत दिनांक तक भंडारित धान की मात्रा ";
            HeaderCell.ColumnSpan = 7;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "आज दिनांक"+ " " + DateTime.Now.AddDays(0).ToString("dd - MM - yyyy") + "को भंडारित धान की मात्रा";
            HeaderCell.ColumnSpan = 7;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "आज दिनांक" + " " + DateTime.Now.AddDays(0).ToString("dd - MM - yyyy") + " तक कुल भंडारित धान की मात्रा";
            HeaderCell.ColumnSpan = 7;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

           
            GrdPrapatraI.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
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

    protected void GrdPrapatraI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txt1 = (TextBox)e.Row.FindControl("txtPakkacap1");
            TextBox txt2 = (TextBox)e.Row.FindControl("txtMandished1");
            TextBox txt3 = (TextBox)e.Row.FindControl("txtKachchacap1");
            //decimal add = Convert.ToDecimal(txt1.Text) + Convert.ToDecimal(txt2.Text) + Convert.ToDecimal(txt3.Text);
            //(e.Row.FindControl("txtTotal1") as Label).Text = add.ToString();
        }
    }

    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GrdPrapatraI.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnBranch_ID = row.FindControl("hdnBranch_ID") as HiddenField;
                TextBox txtMPWLC_HIRED_JVS1 = row.FindControl("txtMPWLC_HIRED_JVS1") as TextBox;
                TextBox txtPVT_PEG1 = row.FindControl("txtPVT_PEG1") as TextBox;
                TextBox txtMARKFED1 = row.FindControl("txtMARKFED1") as TextBox;
                TextBox txtQMANDI_BOARD1 = row.FindControl("txtQMANDI_BOARD1") as TextBox;
                TextBox txtOILFED1 = row.FindControl("txtOILFED1") as TextBox;
                TextBox txtCWC1 = row.FindControl("txtCWC1") as TextBox;

                TextBox txtMPWLC_HIRED_JVS2 = row.FindControl("txtMPWLC_HIRED_JVS2") as TextBox;
                TextBox txtPVT_PEG2 = row.FindControl("txtPVT_PEG2") as TextBox;
                TextBox txtMARKFED2 = row.FindControl("txtMARKFED2") as TextBox;
                TextBox txtQMANDI_BOARD2 = row.FindControl("txtQMANDI_BOARD2") as TextBox;
                TextBox txtOILFED2 = row.FindControl("txtOILFED2") as TextBox;
                TextBox txtCWC2 = row.FindControl("txtCWC2") as TextBox;

                TextBox txtMPWLC_HIRED_JVS3 = row.FindControl("txtMPWLC_HIRED_JVS3") as TextBox;
                TextBox txtPVT_PEG3 = row.FindControl("txtPVT_PEG3") as TextBox;
                TextBox txtMARKFED3 = row.FindControl("txtMARKFED3") as TextBox;
                TextBox txtQMANDI_BOARD3 = row.FindControl("txtQMANDI_BOARD3") as TextBox;
                TextBox txtOILFED3 = row.FindControl("txtOILFED3") as TextBox;
                TextBox txtCWC3 = row.FindControl("txtCWC3") as TextBox;

                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                try
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Business_Prapatra_7_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Year", ddlcropyear.SelectedValue);
                    cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
                    cmd.Parameters.AddWithValue("@MPWLC_HIRED_JVS1", txtMPWLC_HIRED_JVS1.Text);
                    cmd.Parameters.AddWithValue("@PVT_PEG1", txtPVT_PEG1.Text);
                    cmd.Parameters.AddWithValue("@MARKFED1", txtMARKFED1.Text);
                    cmd.Parameters.AddWithValue("@QMANDI_BOARD1", txtQMANDI_BOARD1.Text.ToString());
                    cmd.Parameters.AddWithValue("@OILFED1", txtOILFED1.Text.ToString());
                    cmd.Parameters.AddWithValue("@CWC1", txtCWC1.Text.ToString());
                    cmd.Parameters.AddWithValue("@MPWLC_HIRED_JVS2", txtMPWLC_HIRED_JVS2.Text.ToString());
                    cmd.Parameters.AddWithValue("@PVT_PEG2", txtPVT_PEG2.Text.ToString());
                    cmd.Parameters.AddWithValue("@MARKFED2", txtMARKFED2.Text.ToString());
                    cmd.Parameters.AddWithValue("@QMANDI_BOARD2", txtQMANDI_BOARD2.Text.ToString());
                    cmd.Parameters.AddWithValue("@OILFED2", txtOILFED2.Text.ToString());
                    cmd.Parameters.AddWithValue("@CWC2", txtCWC2.Text.ToString());
                    cmd.Parameters.AddWithValue("@MPWLC_HIRED_JVS3", txtMPWLC_HIRED_JVS3.Text.ToString());
                    cmd.Parameters.AddWithValue("@PVT_PEG3", txtPVT_PEG3.Text.ToString());
                    cmd.Parameters.AddWithValue("@MARKFED3", txtMARKFED3.Text.ToString());
                    cmd.Parameters.AddWithValue("@QMANDI_BOARD3", txtQMANDI_BOARD3.Text.ToString());
                    cmd.Parameters.AddWithValue("@OILFED3", txtOILFED3.Text.ToString());
                    cmd.Parameters.AddWithValue("@CWC3", txtCWC3.Text.ToString());
                    cmd.Parameters.AddWithValue("@Inserted_By", ipAddress.ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Prapatra 7 Successfully submitted |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        GetEmployeeDetails();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
