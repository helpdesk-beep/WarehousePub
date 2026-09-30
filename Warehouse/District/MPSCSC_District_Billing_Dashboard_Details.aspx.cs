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

public partial class District_MPSCSC_District_Billing_Dashboard_Details : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    SqlCommand cmd;
    DataSet ds;
    SqlDataAdapter da;
    string query = "";
    string cnt = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["Depot_DistID"] != null))
        {
            if (!IsPostBack)
            {
                Submitted_Bill();

                Comm_Dtl();
            }
            else
            {
                //     Response.Redirect("~/SessionExpired.htm");}
            }
        }
    }

    protected void Label1_Click(object sender, EventArgs e)
    {

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    public void Submitted_Bill()
    {

        string qry = "";
        string Did = Session["Depot_DistID"].ToString();
        UxUserName.Text = Session["UserName"].ToString();
        // qry = "select Godown_Id ,Crop_Year,MONTH ,SUM(convert(float, Weight)) Weight,sum(Net_Amount)NetAmount   from  tbl_Institution_Storage_Bill_Details where BO_Approval_Status='Y' and Bill_Number like '19%' and Bill_Type = 'AD' and Branch_Id='" + Session["BranchID"].ToString() + "' group by Branch_Id ,Godown_Id  ,Crop_Year ,MONTH ";
        qry = "select i.Godown_Id,g.Godown_Name,Crop_Year,MONTH ,sum(Net_Amount)Net_Amount,(Select min(Closing_Weight) From tbl_Bill_Institution_Daily_Charges b Where i.Godown_Id = b.Godown_Id and month(b.Dates) = i.Month)as Min_Weight ,(Select max(Closing_Weight) From tbl_Bill_Institution_Daily_Charges b Where i.Godown_Id = b.Godown_Id ) as Max_Weight from tbl_Institution_Storage_Bill_Details i inner join tbl_MetaData_GODOWN_2018 g on i.Godown_Id = g.Godown_ID inner join tbl_MetaData_STORAGE_COMMODITY c on i.Commodity_Id = c.Commodity_Id where BO_Approval_Status = 'Y' and i.Bill_Number like '19%' and Bill_Type = 'AD' and District_Id = '" + Session["Depot_DistID"].ToString() + "' group by District_Id ,i.Godown_Id,g.Godown_Name,Crop_Year,MONTH";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();
        sda.Fill(dt);
        GVGeneratedBill.DataSource = dt;
        GVGeneratedBill.DataBind();


        decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount"));
        GVGeneratedBill.FooterRow.Cells[6].Text = "Total :";
        GVGeneratedBill.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
        GVGeneratedBill.FooterRow.Cells[7].Text = total.ToString();
        GVGeneratedBill.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
        lbl_Distname.Text = Session["UserName"].ToString();

        con.Open();

        cmd.ExecuteNonQuery();

        con.Close();

    }

    public void Comm_Dtl()
    {

        string qry = "";
        string Did = Session["Depot_DistID"].ToString();
        UxUserName.Text = Session["UserName"].ToString();
        // qry = "select Godown_Id ,Crop_Year,MONTH ,SUM(convert(float, Weight)) Weight,sum(Net_Amount)NetAmount   from  tbl_Institution_Storage_Bill_Details where BO_Approval_Status='Y' and Bill_Number like '19%' and Bill_Type = 'AD' and Branch_Id='" + Session["BranchID"].ToString() + "' group by Branch_Id ,Godown_Id  ,Crop_Year ,MONTH ";
        qry = "select distinct i.Commodity_Id,c.Commodity_Name,Commodity_Rate from tbl_Institution_Storage_Bill_Details i inner join tbl_MetaData_GODOWN_2018 g on i.Godown_Id = g.Godown_ID inner join tbl_MetaData_STORAGE_COMMODITY c on i.Commodity_Id = c.Commodity_Id where BO_Approval_Status = 'Y' and Bill_Number like '19%' and Bill_Type = 'AD' and District_Id = '" + Session["Depot_DistID"].ToString() + "' group by District_Id ,i.Godown_Id,g.Godown_Name,i.Commodity_Id,c.Commodity_Name,Crop_Year,MONTH,Commodity_Rate ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();
        sda.Fill(dt);
        Gv_cmdt.DataSource = dt;
        Gv_cmdt.DataBind();

        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("MPSCSC_District_Billing_Dashboard.aspx");
    }

    protected void btn_prt_Click(object sender, EventArgs e)
    {
        // PrintPage();
    }

    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "MPSCSC District Bill Details" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        Label3.RenderControl(htmltextwrtter);
        lbl_Distname.RenderControl(htmltextwrtter);
        Gv_cmdt.HeaderRow.Style.Add("background-color", "#6699FF");
        Gv_cmdt.GridLines = GridLines.Both;
        Gv_cmdt.HeaderStyle.Font.Bold = true;
        Gv_cmdt.RenderControl(htmltextwrtter);
        GVGeneratedBill.GridLines = GridLines.Both;
        GVGeneratedBill.HeaderStyle.Font.Bold = true;
        GVGeneratedBill.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    
    protected void btn_exp_els_Click(object sender, EventArgs e)
    {

        ExportGridToExcel();
    }
}