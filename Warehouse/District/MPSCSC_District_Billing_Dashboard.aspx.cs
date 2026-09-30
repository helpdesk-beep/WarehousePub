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
public partial class MPSCSC_District_Billing_Dashboard : System.Web.UI.Page
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
                Generated_Bill();
                // Submitted_BO_Bill();
                Submitted_District_Bill();
                Label21.Visible = true;
                Label16.Visible = false;
                Label14.Visible = false;
                //Submitted_RO_Bill();
            }

            else
            {
                //  Response.Redirect("~/SessionExpired.htm");}


            }
        }
    }

    protected void Label1_Click(object sender, EventArgs e)
    {
        
    }

    public void Generated_Bill()
    {
        string qry = "";
        string Did = Session["Depot_DistID"].ToString();
        qry = "select  Count(Bill_Number) as No_of_Bill from (select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='AD' and District_Id='" + Session["Depot_DistID"].ToString() + "')tbl_Institution_Storage_Bill_Details";
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
   
      public void Submitted_District_Bill()
    {
        string qry = "";
        string Did = Session["Depot_DistID"].ToString();
        qry = "select count(Bill_Number) as No_of_Submitted_BO_Bill from( select distinct(Bill_Number) from tbl_Institution_Storage_Bill_Details where BO_Approval_Status='Y' and Bill_Number like '19%' and Bill_Type='AD' and District_Id='" + Session["Depot_DistID"].ToString() + "') tbl_Institution_Storage_Bill_Details";
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
        Response.Redirect("MPSCSC_District_Billing_Dashboard_Details.aspx");
    }
}