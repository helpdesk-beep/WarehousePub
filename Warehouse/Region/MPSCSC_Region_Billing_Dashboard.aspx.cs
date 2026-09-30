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
public partial class Region_MPSCSC_Region_Billing_Dashboard : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    SqlCommand cmd;
    DataSet ds;
    SqlDataAdapter da;
    string query = "";
    string cnt = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Region_ID"] != null))
        {
            if (!IsPostBack)
            {
                Generated_Bill();
              //  Submitted_BO_Bill();
                Label21.Visible = true;
                Label16.Visible = true;
                Label14.Visible = true;
                Submitted_RO_Bill();
            }
        
          else
        { 
             Response.Redirect("~/SessionExpired.htm");}


        }
    }

    protected void Label1_Click(object sender, EventArgs e)
    {
        
    }

    public void Generated_Bill()
    {
        string qry = "";

        qry = "select  Count(Bill_Number) as No_of_Bill from (select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='AD')tbl_Institution_Storage_Bill_Details";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sda.Fill(ds);
        if (ds != null)
        {
            Label1.Text = Convert.ToString(ds.Tables[0].Rows[0]["No_of_Bill"]);
        }

        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }

    public void Submitted_BO_Bill()
    {
        string qry = "";
        //string sid = Session["BranchID"].ToString();
        qry = "select count(Bill_Number) as No_of_Submitted_BO_Bill from( select distinct(Bill_Number) from tbl_Institution_Storage_Bill_Details where BO_Approval_Status='Y' and Bill_Number like '19%' and Bill_Type='AD') tbl_Institution_Storage_Bill_Details";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sda.Fill(ds);
        if (ds != null)
        {
            Label21.Text = Convert.ToString(ds.Tables[0].Rows[0]["No_of_Submitted_BO_Bill"]);
        }

        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }
    public void Submitted_RO_Bill()
    {
        string qry = "";
        string sid = Session["Region_ID"].ToString();
        //qry = "select count(Bill_Number) as No_of_Submitted_RO_Bill from( select distinct(Bill_Number) from tbl_Institution_Storage_Bill_Details where RO_Approval_Status='Y' and Bill_Number like '19%' and Bill_Type='AD') tbl_Institution_Storage_Bill_Details";
        qry = "with P as (select ist.Bill_Type,ist.District_Id,ist.Branch_Id,ist.Crop_Year,ist.BO_Approval_Status,ist.Commodity_Id,ist.Financial_Year,MDD.RegionID from tbl_Institution_Storage_Bill_Details as ist INNER JOIN  tbl_MetaData_DEPOT AS MDD ON MDD.DistrictId = ist.District_Id where ist.BO_Approval_Status = 'Y' and Bill_Type = 'AD' and Bill_Number like '19%' and MDD.RegionID = '" + Session["Region_ID"].ToString() + "' group by ist.Bill_Type,MDD.RegionID,ist.District_Id,ist.Branch_Id,Crop_Year,ist.BO_Approval_Status,ist.Commodity_Id,ist.Financial_Year)select Count(1) as No_of_Submitted_RO_Bill from P";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sda.Fill(ds);
        if (ds != null)
        {
            Label21.Text = Convert.ToString(ds.Tables[0].Rows[0]["No_of_Submitted_RO_Bill"]);
        }

        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }

   
}