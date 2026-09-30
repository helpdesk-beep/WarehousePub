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
using System.IO;

public partial class Inspections_State_Rpt_Annaxure_C_Branch_Wise : System.Web.UI.Page
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
    string Branch_ID = "";
    string Insp_ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        //PFID = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            GetDist();
            //fillBranchDetails();
            //GetEmployeeInspectionDetails(PFID);
        }

    }
    public void GetEmployeeInspectionDetails(string PFID)
    {
        SqlCommand cmd = new SqlCommand("[dbo].[Get_Inspection_Quater_Details]", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Employee_ID", PFID);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            hdninspectionid.Value = dt.Rows[0]["ID"].ToString();
            hdnmonth.Value = dt.Rows[0]["Inspection_month_ID"].ToString();
            hdnquater.Value = dt.Rows[0]["Inspection_type_ID"].ToString();
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "
        //Control 'gvCol7' of type 'GridView' must be placed inside a form tag with runat=server."
    }
    protected void Print(object sender, EventArgs e)
    {
        GD_StackBal.UseAccessibleHeader = true;
        GD_StackBal.Columns[0].HeaderText = "Header text";
        GD_StackBal.HeaderRow.TableSection = TableRowSection.TableHeader;
        GD_StackBal.FooterRow.TableSection = TableRowSection.TableFooter;
        GD_StackBal.Attributes["style"] = "border-collapse:separate";
        foreach (GridViewRow row in GD_StackBal.Rows)
        {
            if (row.RowIndex % 10 == 0 && row.RowIndex != 0)
            {
                row.Attributes["style"] = "page-break-after:always;";

                //"Bio-Data For Devi Ahila University Indore";
            }
        }
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GD_StackBal.RenderControl(hw);
        // gvCol7.Columns[0].HeaderText = "Header text";
        string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append(",top=10,width=1000,height=600,status=0');");
        sb.Append("printWin.document.write(\"");
        string style = "<style type = 'text/css'>thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>";
        sb.Append(style + gridHTML);
        sb.Append("\");");
        //sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        //sb.Append("printWin.close();");
        sb.Append("};");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        GD_StackBal.DataBind();
    }
    private void GetDist()
    {
        string strDist = "";
        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con2);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_dist.DataSource = ds.Tables[0];
            ddl_dist.DataTextField = "District_Name";
            ddl_dist.DataValueField = "District_Id";
            ddl_dist.DataBind();
            ddl_dist.Items.Insert(0, "--Select--");
        }
        else
        {
            ddl_dist.Items.Insert(0, "--Select--");
        }
    }
  
    public void GetdataForGrid()
    {
        SqlCommand cmd = new SqlCommand("[dbo].[Get_Annaxure_C_Branch_Wise_For_State]", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Dst_Id", ddl_dist.SelectedValue);
        cmd.Parameters.AddWithValue("@OrderDate", getDate_MDY(txt_inspdate.Text));
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GD_StackBal.Caption = @"<b style=""font-weight: bold;"">Annaxure C Branch Wise , Depositer Wise , Commodity Wise"+ "</b> ";
            tr_griddata.Visible = true;
            GD_StackBal.DataSource = dt;
            GD_StackBal.DataBind();
            GD_StackBal.FooterRow.Style.Add("text-align", "center");
            GD_StackBal.FooterRow.Cells[3].Text = "Total";
            GD_StackBal.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Avl_Bags")).ToString();
            GD_StackBal.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Avl_Quantity")).ToString();
        }
        else
        {
            tr_griddata.Visible = false;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
           // string strMsg = "गोदाम " + ddl_gdwn.SelectedItem.ToString() + " की एंट्री निरीक्षण अधिकारी के द्वारा नहीं की गई है|";
           // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "')", true);
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

    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (txt_inspdate.Text == "" || txt_inspdate.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Inspection Order Date Date!....')", true);
            txt_inspdate.Focus();
            return;
        }
        else
        {
            GetdataForGrid();
        }       
    }

   
    protected void GD_StackBal_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
  
}