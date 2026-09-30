using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Branch_DailyReceiptRelease : System.Web.UI.Page
{

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    public string qry = "";
    int total1 = 0;
    int total2 = 0;
    decimal total3 = 0;
    decimal total4 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBox1_CalendarExtender.EndDate = DateTime.Now;   //to dissable future  Date
        TextBox2_CalendarExtender.EndDate = DateTime.Now;   //to dissable future  Date
        lbldateprinted.Text = System.DateTime.Now.ToString();

        if (!IsPostBack)
        {
            Printcurrentdate();
            dtl.Visible = false;
        }
    }


    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    private void GetDL()
    {

        qry = "SELECT [Depositor_WHR_Id],[Category_Id],[commodity],[Depositor_Name],[GatePass_No],[Issue_Source_ID],convert(varchar(20),[DeliveryDate],103) as DeliveryDate ,convert(varchar(20),[WHR_Issue_Date],103) as WHR_Issue_Date ,[datecom],[BranchID],[Godown_ID],[Godown_ID]+'('+(select Godown_Name from dbo.tbl_MetaData_GODOWN where Godown_ID=[uiondata].Godown_ID)+')' as godownname,[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM ,[recbags],[recweght],[Loss],[Gain],[delbags],[delwght] FROM [Intergrated_MP_STORAGE].[dbo].[uiondata] where BranchID='" + Session["BranchId"].ToString() + "' and datecom between '" + getDate_MDY(TextBox1.Text) + "' and  '" + getDate_MDY(TextBox2.Text) + "' order by datecom";
        da = new SqlDataAdapter(qry, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            btnexel.Enabled = true;
            gvrr.DataSource = ds.Tables[0];
            gvrr.DataBind();
            //ListView1.DataSource = ds.Tables[0];
            //ListView1.DataBind();
            int avilalebags = 0;
            decimal avilalewight = 0;
            decimal Provalue = 0;
            decimal currentvalue = 0;
         //   gvrr.HeaderRow.Cells[18].Visible = false;
            lblbranch.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
            //lblcommodity.Text = ds.Tables[0].Rows[0]["commodity"].ToString();
            //lbldeposter.Text = ds.Tables[0].Rows[0]["Depositor_Name"].ToString();
            for (int i = 0; i < gvrr.Rows.Count; i++)
            {
                dtl.Visible = true;
                //avilalebags = avilalebags + Convert.ToInt32(gvrr.Rows[i].Cells[2].Text) - Convert.ToInt32(gvrr.Rows[i].Cells[7].Text);
                //gvrr.Rows[i].Cells[16].Text = avilalebags.ToString();
                ////  ListView1.Items[i].Sub


                //decimal tvalue = Convert.ToDecimal(gvrr.Rows[i].Cells[8].Text) - Convert.ToDecimal(gvrr.Rows[i].Cells[11].Text) + Convert.ToDecimal(GridView1.Rows[i].Cells[10].Text);
                //gvrr.Rows[i].Cells[12].Text = tvalue.ToString();
                //currentvalue = (Convert.ToDecimal(gvrr.Rows[i].Cells[3].Text) + Convert.ToDecimal(gvrr.Rows[i].Cells[12].Text)) * Convert.ToDecimal(GridView1.Rows[i].Cells[18].Text);


                //avilalewight = avilalewight + Convert.ToDecimal(GridView1.Rows[i].Cells[3].Text) - Convert.ToDecimal(GridView1.Rows[i].Cells[12].Text);
                //GridView1.Rows[i].Cells[17].Text = avilalewight.ToString();


                //GridView1.Rows[i].Cells[19].Text = Math.Round(currentvalue).ToString();

                //Provalue = Provalue + (Convert.ToDecimal(GridView1.Rows[i].Cells[3].Text) * Convert.ToDecimal(GridView1.Rows[i].Cells[18].Text)) - (Convert.ToDecimal(GridView1.Rows[i].Cells[12].Text) * Convert.ToDecimal(GridView1.Rows[i].Cells[18].Text));
                //GridView1.Rows[i].Cells[20].Text = Math.Round(Provalue).ToString();

                //GridView1.Rows[i].Cells[18].Visible = false;
            }
            btnexel.Visible = true;
        }
        else
        {
            gvrr.DataSource =null;
            gvrr.DataBind();
            btnexel.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('NO Record found')", true);
        }

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        GetDL();
        lbldatefrom.Text = TextBox1.Text;
        lvldateto.Text = TextBox2.Text;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {


    }
    protected void btnexel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "DepositorLezer" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        gvrr.GridLines = GridLines.Both;
        gvrr.HeaderStyle.Font.Bold = true;
        gvrr.UseAccessibleHeader = true;
        gvrr.HeaderRow.TableSection = TableRowSection.TableHeader;
        gvrr.FooterRow.TableSection = TableRowSection.TableFooter;
       // ListView1.Attributes["style"] = "border-collapse:separate";
        toexport.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    protected void gvrr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
          if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total1 += (DataBinder.Eval(e.Row.DataItem, "recbags") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "recbags")) : 0;

            total2 += (DataBinder.Eval(e.Row.DataItem, "delbags") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "delbags")) : 0;

            total3 += (DataBinder.Eval(e.Row.DataItem, "recweght") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "recweght")) : 0;

            total4 += (DataBinder.Eval(e.Row.DataItem, "delwght") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "delwght")) : 0;


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lblMarkOne = (Label)e.Row.FindControl("recbags");

            lblMarkOne.Text = total1.ToString();

            Label lblMarkSecond = (Label)e.Row.FindControl("lblissubags");

            lblMarkSecond.Text = total2.ToString();


            Label lblMarkOne2 = (Label)e.Row.FindControl("lblrecqty");

            lblMarkOne2.Text = total3.ToString();

            Label lblMarkSecond2 = (Label)e.Row.FindControl("lblissueqty");

            lblMarkSecond2.Text = total4.ToString();

        }
    
    }
    protected void Printcurrentdate()
    {
        try
        {
            string query = "SELECT  convert(varchar(10),getdate(),103) as 'Date1'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                TextBox1.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
                TextBox2.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
    }
}