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

public partial class Accounting_frm_Business_Prapatra_III : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                fillCommodity();
                GetEmployeeDetails();
                txttotal123.Attributes.Add("readonly", "readonly");
                txttotal456.Attributes.Add("readonly", "readonly");
                txttotal789.Attributes.Add("readonly", "readonly");
                //txtTCC4.Attributes.Add("readonly", "readonly");
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void fillCommodity()
    {
        string conStr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr2))
        {
            SqlCommand cmd = new SqlCommand("Get_CAP_Details", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
            con.Open();

            ddlcapshedname.DataSource = cmd.ExecuteReader();
            ddlcapshedname.DataTextField = "Godown_Name";
            ddlcapshedname.DataValueField = "Godown_ID";
            ddlcapshedname.DataBind();
            ddlcapshedname.Items.Insert(0, new ListItem("-- Select --", "0"));
            con.Close();
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
            HeaderCell.Text = "कल भंडारित मात्रा";
            HeaderCell.ColumnSpan = 4;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
           // HeaderCell.Text = "आज दिनांक को भंडारित मात्रा";
            HeaderCell.Text = "आज भंडारित मात्रा";
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
    public void checkvalidation()
    {
        if (string.IsNullOrEmpty(txtIntimationRegDate.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Date')", true);
            txtIntimationRegDate.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txttodaydate.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Date')", true);
            txttodaydate.Focus();
            return;
        }
        if (ddlcapshedname.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Cap Name')", true);
            ddlcapshedname.Focus();
            return;
        }
    }
    protected void btnSumbmitRent_Click(object sender, EventArgs e)
    {
        try
        {
            //checkvalidation();
            if (string.IsNullOrEmpty(txtIntimationRegDate.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Date')", true);
                txtIntimationRegDate.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txttodaydate.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Date')", true);
                txttodaydate.Focus();
                return;
            }
            if (ddlcapshedname.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Cap Name')", true);
                ddlcapshedname.Focus();
                return;
            }
            else
            {
                string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                using (SqlConnection constr = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("Business_Prapatra_III_Insert", constr);
                    cmd.CommandType = CommandType.StoredProcedure;
                    constr.Open();
                    cmd.Parameters.AddWithValue("@Year", ddlcropyear.SelectedValue);
                    cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
                    cmd.Parameters.AddWithValue("@Cap_Godown_ID", ddlcapshedname.SelectedValue);
                    cmd.Parameters.AddWithValue("@Yesterday_Date", getDate_MDY(txtIntimationRegDate.Text));
                    cmd.Parameters.AddWithValue("@Today_Date", getDate_MDY(txttodaydate.Text));
                    cmd.Parameters.AddWithValue("@Pakkacap1", txtpakkacap1.Text.ToString());
                    cmd.Parameters.AddWithValue("@Mandished1", txtmandished1.Text.ToString());
                    cmd.Parameters.AddWithValue("@Kachchacap1", txtkachchacap1.Text.ToString());
                    cmd.Parameters.AddWithValue("@Total1", txttotal123.Text.ToString());
                    cmd.Parameters.AddWithValue("@Pakkacap2", txtpakkacap2.Text.ToString());
                    cmd.Parameters.AddWithValue("@Mandished2", txtmandished2.Text.ToString());
                    cmd.Parameters.AddWithValue("@Kachchacap2", txtkachchacap2.Text.ToString());
                    cmd.Parameters.AddWithValue("@Total2", txttotal456.Text.ToString());
                    cmd.Parameters.AddWithValue("@Pakkacap3", txtpakkacap3.Text.ToString());
                    cmd.Parameters.AddWithValue("@Mandished3", txtmandished3.Text.ToString());
                    cmd.Parameters.AddWithValue("@Kachchacap3", txtkachchacap3.Text.ToString());
                    cmd.Parameters.AddWithValue("@Total3", txttotal789.Text.ToString());
                    cmd.Parameters.AddWithValue("@Inserted_By", IPAddress.ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Record Save Successfully |||";

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        //GetdataForGrid();
                        //Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
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

    protected void txtpakkacap1_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtpakkacap1.Text);
        decimal B = decimal.Parse(txtmandished1.Text);
        decimal C = decimal.Parse(txtkachchacap1.Text);

        decimal i = A + B + C;
        i = A + B + C;
        txttotal123.Text = i.ToString();
    }

    protected void txtmandished1_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtpakkacap1.Text);
        decimal B = decimal.Parse(txtmandished1.Text);
        decimal C = decimal.Parse(txtkachchacap1.Text);

        decimal i = A + B + C;
        i = A + B + C;
        txttotal123.Text = i.ToString();
    }

    protected void txtkachchacap1_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtpakkacap1.Text);
        decimal B = decimal.Parse(txtmandished1.Text);
        decimal C = decimal.Parse(txtkachchacap1.Text);

        decimal i = A + B + C;
        i = A + B + C;
        txttotal123.Text = i.ToString();
    }

    protected void txtpakkacap2_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtpakkacap2.Text);
        decimal B = decimal.Parse(txtmandished2.Text);
        decimal C = decimal.Parse(txtkachchacap2.Text);

        decimal i = A + B + C;
        i = A + B + C;
        txttotal456.Text = i.ToString();
    }

    protected void txtmandished2_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtpakkacap2.Text);
        decimal B = decimal.Parse(txtmandished2.Text);
        decimal C = decimal.Parse(txtkachchacap2.Text);

        decimal i = A + B + C;
        i = A + B + C;
        txttotal456.Text = i.ToString();
    }

    protected void txtkachchacap2_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtpakkacap2.Text);
        decimal B = decimal.Parse(txtmandished2.Text);
        decimal C = decimal.Parse(txtkachchacap2.Text);

        decimal i = A + B + C;
        i = A + B + C;
        txttotal456.Text = i.ToString();
    }

    protected void txtpakkacap3_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtpakkacap3.Text);
        decimal B = decimal.Parse(txtmandished3.Text);
        decimal C = decimal.Parse(txtkachchacap3.Text);

        decimal i = A + B + C;
        i = A + B + C;
        txttotal789.Text = i.ToString();
    }

    protected void txtmandished3_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtpakkacap3.Text);
        decimal B = decimal.Parse(txtmandished3.Text);
        decimal C = decimal.Parse(txtkachchacap3.Text);

        decimal i = A + B + C;
        i = A + B + C;
        txttotal789.Text = i.ToString();
    }

    protected void txtkachchacap3_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtpakkacap3.Text);
        decimal B = decimal.Parse(txtmandished3.Text);
        decimal C = decimal.Parse(txtkachchacap3.Text);

        decimal i = A + B + C;
        i = A + B + C;
        txttotal789.Text = i.ToString();
    }
}
