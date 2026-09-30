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
using System.IO;

public partial class Reports_Branch_StackWiseRegNew : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            filldepositor();
            fillCommodity();
            fillGodown();
            fillStack();
            //ddlcomm.SelectedValue = "22";
            // ddldepositer.SelectedValue = "MPSCSC";
        }
    }

    private void ExportGridToExcel()
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
        // GridView1.GridLines = GridLines.Both;
        // GridView1.HeaderStyle.Font.Bold = true;
        // GridView1.UseAccessibleHeader = true;
        //ListView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        //  GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
        ListView1.Attributes["style"] = "border-collapse:separate";
        gv.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }

    protected string getDate_MDY(string inDate)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

    }
    protected void fillStack()
    {
        try
        {
            string query = "";
            ddlstack.Items.Clear();

            query = "SELECT Stack_ID, Stack_Name FROM tbl_MetaData_STACK WHERE (Godown_ID = '" + ddlgodown.SelectedValue + "' and Stack_Killed = 'N' ) order by Stack_Name";
           
           
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlstack.DataSource = ds.Tables[0];
                ddlstack.DataTextField = "Stack_Name";
                ddlstack.DataValueField = "Stack_ID";
                ddlstack.DataBind();
            }
            else
            {
                ddlstack.Items.Clear();
                ddlstack.DataSource = null;
                ddlstack.DataBind();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in FillStack has occured, try again '); </script> ");
        }
    }
    private void filldepositor()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            // string query = "SELECT  distinct( tbl_Storage_Arrival_Stock.Depositor_Name)  as Depositor_Name  FROM tbl_Storage_Receipt_Details INNER JOIN tbl_Storage_Arrival_Stock ON tbl_Storage_Receipt_Details.StorageReceipt_Id = tbl_Storage_Arrival_Stock.Receipt_ID where tbl_Storage_Receipt_Details.Depotid ='" + Session["Depot_DepotID"].ToString() + "'  order by Depositor_Name";
            string query = "SELECT  distinct( DepositorName)  as Depositor_Name  FROM tbl_Storage_Receipt_Details  where tbl_Storage_Receipt_Details.BranchId ='" + Session["BranchId"].ToString() + "'  order by Depositor_Name";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldepositer.Items.Clear();
                ddldepositer.DataSource = ds.Tables[0];
                ddldepositer.DataTextField = "Depositor_Name";
                ddldepositer.DataValueField = "Depositor_Name";
                ddldepositer.DataBind();
                //ddldepositorname.Items.Insert(0, "--Select--");

            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }


    private void fillCommodity()
    {
        string query = "SELECT distinct Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in (select Commodity_Id from dbo.tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["BranchId"].ToString() + "' and Depositor_Name='" + ddldepositer.SelectedItem.Text + "')  order by Commodity_Name Asc ";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcomm.Items.Clear();
            ddlcomm.DataSource = ds.Tables[0];
            ddlcomm.DataTextField = "Commodity_Name";
            ddlcomm.DataValueField = "Commodity_Id";
            ddlcomm.DataBind();
            ddlcomm.Items.Insert(0, "--Select--");

        }
    }

    private void fillGodown()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            // string query = "SELECT  distinct( tbl_Storage_Arrival_Stock.Depositor_Name)  as Depositor_Name  FROM tbl_Storage_Receipt_Details INNER JOIN tbl_Storage_Arrival_Stock ON tbl_Storage_Receipt_Details.StorageReceipt_Id = tbl_Storage_Arrival_Stock.Receipt_ID where tbl_Storage_Receipt_Details.Depotid ='" + Session["Depot_DepotID"].ToString() + "'  order by Depositor_Name";
            string query = "select Godown_Name,Godown_ID from  dbo.tbl_MetaData_GODOWN where BranchID='" + Session["BranchId"].ToString() + "' and Godown_ID in (select Godown_ID from [tbl_Storage_Receipt_Details] where BranchID='" + Session["BranchId"].ToString() + "' and DepositorName='" + ddldepositer.SelectedItem.Text + "') and Remarks='Y' order by Godown_Name ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodown.Items.Clear();
                ddlgodown.DataSource = ds.Tables[0];
                ddlgodown.DataTextField = "Godown_Name";
                ddlgodown.DataValueField = "Godown_ID";
                ddlgodown.DataBind();
                //ddldepositorname.Items.Insert(0, "--Select--");
                // ddlgodown.SelectedValue = "MPSCSC";
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {


    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        GetSR();
        getdatafromgrid();
    }
    protected void btnexel_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }

    private void GetSR()
    {
        if (RadioButton1.Checked)
        {
            qry = "SELECT [Depositor_WHR_Id],[Category_Id],[commodity],[Depositor_Name],[GatePass_No],[Issue_Source_ID],convert(varchar(20),[DeliveryDate],103) as DeliveryDate ,[Delmoisture],[Mode_of_weighment],[AvgMoisture_Content],[MktValue_of_Commodity],convert(varchar(20),[WHR_Issue_Date],103) as WHR_Issue_Date ,[datecom],[AvgMoisture_Content_To],[BranchID],[Godown_ID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM ,[recbags],[recweght],[Loss],[Gain],[delbags],[delwght],([AvgMoisture_Content]+[AvgMoisture_Content_To])/2 as Moisture FROM [Intergrated_MP_STORAGE].[dbo].[uiondata] where BranchID='" + Session["BranchId"].ToString() + "' and Depositor_Name='" + ddldepositer.SelectedItem.Text + "' and Commodity_Id='" + ddlcomm.SelectedValue.ToString() + "' and Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and '"+ddlstack.SelectedValue.ToString()+"'  in (select Stack_ID from dbo.tbl_MetaData_STACK where tbl_MetaData_STACK.Godown_ID=[dbo].[uiondata].Godown_ID )";
            da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnexel.Enabled = true;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                int avilalebags = 0;
                decimal avilalewight = 0;
                decimal currentvalue = 0;
                decimal Provalue = 0;
                lblbranch.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
                lblgodown.Text = ddlgodown.SelectedItem.Text;
                GridView1.HeaderRow.Cells[20].Visible = false;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    avilalebags = avilalebags + Convert.ToInt32(GridView1.Rows[i].Cells[2].Text) - Convert.ToInt32(GridView1.Rows[i].Cells[9].Text);
                    GridView1.Rows[i].Cells[18].Text = avilalebags.ToString();


                    decimal tvalue = Convert.ToDecimal(GridView1.Rows[i].Cells[10].Text) - Convert.ToDecimal(GridView1.Rows[i].Cells[13].Text) + Convert.ToDecimal(GridView1.Rows[i].Cells[12].Text);
                    GridView1.Rows[i].Cells[14].Text = tvalue.ToString();

                    avilalewight = avilalewight + Convert.ToDecimal(GridView1.Rows[i].Cells[3].Text) - Convert.ToDecimal(GridView1.Rows[i].Cells[14].Text);
                    GridView1.Rows[i].Cells[19].Text = avilalewight.ToString();

                    currentvalue = (Convert.ToDecimal(GridView1.Rows[i].Cells[3].Text) + Convert.ToDecimal(GridView1.Rows[i].Cells[14].Text)) * Convert.ToDecimal(GridView1.Rows[i].Cells[20].Text);
                    GridView1.Rows[i].Cells[21].Text = Math.Round(currentvalue).ToString();

                    Provalue = Provalue + ((Convert.ToDecimal(GridView1.Rows[i].Cells[3].Text) * Convert.ToDecimal(GridView1.Rows[i].Cells[20].Text)) - (Convert.ToDecimal(GridView1.Rows[i].Cells[14].Text)) * Convert.ToDecimal(GridView1.Rows[i].Cells[20].Text));

                    GridView1.Rows[i].Cells[22].Text = Math.Round(Provalue).ToString();

                    GridView1.Rows[i].Cells[20].Visible = false;
                }
                GridView1.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('NO Record found')", true);
            }

        }
        else if (RadioButton2.Checked)
        {

            qry = "SELECT [Depositor_WHR_Id],[Category_Id],[commodity],[Depositor_Name],[GatePass_No],[Issue_Source_ID],convert(varchar(20),[DeliveryDate],103) as DeliveryDate ,[Delmoisture],[Mode_of_weighment],[AvgMoisture_Content],[MktValue_of_Commodity],convert(varchar(20),[WHR_Issue_Date],103) as WHR_Issue_Date ,[datecom],[AvgMoisture_Content_To],[BranchID],[Godown_ID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM ,[recbags],[recweght],[Loss],[Gain],[delbags],[delwght],([AvgMoisture_Content]+[AvgMoisture_Content_To])/2 as Moisture FROM [Intergrated_MP_STORAGE].[dbo].[uiondata] where BranchID='" + Session["BranchId"].ToString() + "' and Commodity_Id='" + ddlcomm.SelectedValue.ToString() + "' and Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and '" + ddlstack.SelectedValue.ToString() + "'  in (select Stack_ID from dbo.tbl_MetaData_STACK where tbl_MetaData_STACK.Godown_ID=[dbo].[uiondata].Godown_ID )";
            da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnexel.Enabled = true;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                int avilalebags = 0;
                decimal avilalewight = 0;
                decimal currentvalue = 0;
                decimal Provalue = 0;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    avilalebags = avilalebags + Convert.ToInt32(GridView1.Rows[i].Cells[2].Text) - Convert.ToInt32(GridView1.Rows[i].Cells[9].Text);
                    GridView1.Rows[i].Cells[17].Text = avilalebags.ToString();
                    avilalewight = avilalewight + Convert.ToDecimal(GridView1.Rows[i].Cells[3].Text) - Convert.ToDecimal(GridView1.Rows[i].Cells[10].Text) + Convert.ToDecimal(GridView1.Rows[i].Cells[12].Text) - Convert.ToDecimal(GridView1.Rows[i].Cells[13].Text);
                    GridView1.Rows[i].Cells[18].Text = avilalewight.ToString();

                    currentvalue = Convert.ToDecimal(GridView1.Rows[i].Cells[3].Text) * Convert.ToDecimal(GridView1.Rows[i].Cells[19].Text);
                    GridView1.Rows[i].Cells[20].Text = currentvalue.ToString();
                    Provalue = Provalue + Convert.ToDecimal(GridView1.Rows[i].Cells[20].Text);
                    GridView1.Rows[i].Cells[21].Text = Provalue.ToString();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('NO Record found')", true);
            }

        }
    }

    protected void ddldepositer_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodown();
        fillCommodity();
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        ddldepositer.Enabled = true;
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        ddldepositer.Enabled = false;
    }

    protected void getdatafromgrid()
    {
        DataTable dt = new DataTable();

        // add the columns to the datatable            
        if (GridView1.HeaderRow != null)
        {

            for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
            {
                dt.Columns.Add(GridView1.HeaderRow.Cells[i].Text);
            }
        }

        //  add each of the data rows to the table
        foreach (GridViewRow row in GridView1.Rows)
        {
            DataRow dr;
            dr = dt.NewRow();

            for (int i = 0; i < row.Cells.Count; i++)
            {
                dr[i] = row.Cells[i].Text.Replace("&nbsp;", "");
            }
            dt.Rows.Add(dr);
        }

        //  add the footer row to the table
        if (GridView1.FooterRow != null)
        {
            DataRow dr;
            dr = dt.NewRow();

            for (int i = 0; i < GridView1.FooterRow.Cells.Count; i++)
            {
                dr[i] = GridView1.FooterRow.Cells[i].Text.Replace("&nbsp;", "");
            }
            dt.Rows.Add(dr);
        }
        ListView1.DataSource = dt;
        ListView1.DataBind();
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillStack();
    }
}
