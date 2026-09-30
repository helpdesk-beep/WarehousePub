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
public partial class StatePages_MPSCSC_Billing_Dashboard : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    SqlCommand cmd;
    DataSet ds;
    SqlDataAdapter da;
    string query = "";
    string cnt = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                Generated_Bill();
                Submitted_BO_Bill();
                Label20.Visible = false;
                Label16.Visible = false;
                Label14.Visible = false;
            }
            else
            {
                // Response.Redirect("~/SessionExpired.htm");}

            }
        }
    }
    
    public void Generated_Bill()
    {
        string qry = "";
        string sid = Session["BranchID"].ToString();
        qry = "select  Count(Bill_Number) as No_of_Bill from (select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='AD' and Branch_Id='" + Session["BranchID"].ToString() + "')tbl_Institution_Storage_Bill_Details";
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
        string sid = Session["BranchID"].ToString();
        //qry = "select count(Bill_Number) as No_of_Submitted_BO_Bill from( select distinct(Bill_Number) from tbl_Institution_Storage_Bill_Details where BO_Approval_Status='Y' and Bill_Number like '19%' and Bill_Type='AD' and Branch_Id='" + Session["BranchID"].ToString() + "') tbl_Institution_Storage_Bill_Details";
        qry = "select count(Bill_Number) as No_of_Submitted_BO_Bill from( select distinct(Bill_Number) from tbl_Institution_Storage_Bill_Details where BO_Approval_Status='Y' and (Bill_Number like '19%' or Bill_Number like '20%') and Bill_Type='AD' and Branch_Id='" + Session["BranchID"].ToString() + "') tbl_Institution_Storage_Bill_Details";

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


    protected void Label21_Click(object sender, EventArgs e)
    {
        Response.Redirect("MPSCSC_Billing_Dashboard_Details.aspx");
    }
}