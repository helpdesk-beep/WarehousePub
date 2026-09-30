using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

public partial class Reports_Region_RO_Generate_EPF_For_Left : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    string Bill_Type = "";
    string Ref_Number = "";
    string Ref_Aid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["District_Id"]))
                {
                    fillgrid();
                }
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void fillgrid()
    {

        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Bill_Wise_Details_Pandancy_at_RM_Level_For_File_Generation", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DistrictID", Request.QueryString["District_Id"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            //GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "Payment Pending no of months,Days From MPSCSC" + "</b> ";
                            gvBOBillApp.DataSource = dt;
                            gvBOBillApp.DataBind();
                            gvBOBillApp.FooterRow.Cells[1].Text = "Total";
                            gvBOBillApp.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount")).ToString();
                            trnewproc.Visible = true;
                        }
                        else
                        {
                            trnewproc.Visible = false;
                            gvBOBillApp.DataSource = null;
                            gvBOBillApp.DataBind();
                        }
                    }
                }
            }
        }
    }
  
    protected void btn_Sbi_Link_Click(object sender, EventArgs e)
    {
        //Response.Redirect("https://yonobusiness.sbi/login/yonobusinesslogin");
    }
}