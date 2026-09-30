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

public partial class Accounting_frm_Business_Prapatra_III_New : System.Web.UI.Page
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
            using (SqlCommand cmd = new SqlCommand("Get_Business_Prapatra_III", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
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
            HeaderCell.Text = "क्र.";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "कैप /शेड स्थल का नाम";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "पक्का केप";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "मंडी शेड";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "कच्चा केप";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "कुल केप क्षमता (3+4+5)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "पक्का केप";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "मंडी शेड";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "कच्चा केप";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "कुल केप क्षमता (7+8+9)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "पक्का केप";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "मंडी शेड";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "कच्चा केप";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "कुल केप क्षमता (11+12+13)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "पक्का केप";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "मंडी शेड";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "कच्चा केप";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "कुल केप क्षमता (11+12+13)";
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
            HeaderCell.Text = "केप / शेड की वास्तविक कुल क्षमता (MT)";
            HeaderCell.ColumnSpan = 4;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            //HeaderCell.Text = "कल दिनांक को भंडारित मात्रा";
            HeaderCell.Text = "कल" + " " + DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy")  + " भंडारित मात्रा";
            HeaderCell.ColumnSpan = 4;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            // HeaderCell.Text = "आज दिनांक को भंडारित मात्रा";
            HeaderCell.Text = "आज" + " " + DateTime.Now.AddDays(0).ToString("dd-MM-yyyy") + " भंडारित मात्रा";
            HeaderCell.ColumnSpan = 4;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
             HeaderCell.Text = "प्रगतिशील भंडारित मात्रा";
            //HeaderCell.Text = "आज" + " " + DateTime.Now.AddDays(0).ToString("dd-MM-yyyy") + " भंडारित मात्रा";
            HeaderCell.ColumnSpan = 4;
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
                HiddenField hdnGodown_ID = row.FindControl("hdnGodown_ID") as HiddenField;
                TextBox txtPakkacap1 = row.FindControl("txtPakkacap1") as TextBox;
                TextBox txtMandished1 = row.FindControl("txtMandished1") as TextBox;
                TextBox txtKachchacap1 = row.FindControl("txtKachchacap1") as TextBox;
                TextBox txtPakkacap2 = row.FindControl("txtPakkacap2") as TextBox;
                TextBox txtMandished2 = row.FindControl("txtMandished2") as TextBox;
                TextBox txtKachchacap2 = row.FindControl("txtKachchacap2") as TextBox;
                TextBox txtPakkacap3 = row.FindControl("txtPakkacap3") as TextBox;
                TextBox txtMandished3 = row.FindControl("txtMandished3") as TextBox;
                TextBox txtKachchacap3 = row.FindControl("txtKachchacap3") as TextBox;

                TextBox txtPakkacap4 = row.FindControl("txtPakkacap4") as TextBox;
                TextBox txtMandished4 = row.FindControl("txtMandished4") as TextBox;
                TextBox txtKachchacap4 = row.FindControl("txtKachchacap4") as TextBox;
                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                try
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Business_Prapatra_III_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Year", ddlcropyear.SelectedValue);
                    cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
                    cmd.Parameters.AddWithValue("@Cap_Godown_ID", hdnGodown_ID.Value);
                    cmd.Parameters.AddWithValue("@Yesterday_Date", getDate_MDY(Session["yesdate"].ToString()));
                    cmd.Parameters.AddWithValue("@Today_Date", getDate_MDY(Session["Todaydate"].ToString()));
                    cmd.Parameters.AddWithValue("@Pakkacap1", txtPakkacap1.Text.ToString());
                    cmd.Parameters.AddWithValue("@Mandished1", txtMandished1.Text.ToString());
                    cmd.Parameters.AddWithValue("@Kachchacap1", txtKachchacap1.Text.ToString());
                    cmd.Parameters.AddWithValue("@Pakkacap2", txtPakkacap2.Text.ToString());
                    cmd.Parameters.AddWithValue("@Mandished2", txtMandished2.Text.ToString());
                    cmd.Parameters.AddWithValue("@Kachchacap2", txtKachchacap2.Text.ToString());
                    cmd.Parameters.AddWithValue("@Pakkacap3", txtPakkacap3.Text.ToString());
                    cmd.Parameters.AddWithValue("@Mandished3", txtMandished3.Text.ToString());
                    cmd.Parameters.AddWithValue("@Kachchacap3", txtKachchacap3.Text.ToString());
                    cmd.Parameters.AddWithValue("@Pakkacap4", txtPakkacap4.Text.ToString());
                    cmd.Parameters.AddWithValue("@Mandished4", txtMandished4.Text.ToString());
                    cmd.Parameters.AddWithValue("@Kachchacap4", txtKachchacap4.Text.ToString());
                    cmd.Parameters.AddWithValue("@Inserted_By", ipAddress.ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Prapatra III Successfully submitted |||";
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
