using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_DistrictBranchGodown : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    SqlTransaction sqltrans;
    string Bill_Type = "";
    string Ref_Number = "";
    string Ref_Aid = "";
    string TheResult = "";
    public string DistrictID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                DistrictID = Request.QueryString["DistID"].ToString();
                //fillDistrict();
                fillgrid();
               

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
            using (SqlCommand cmd = new SqlCommand("Get_BranchGodwon", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DistrictID", DistrictID);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            godown_GridView.DataSource = dt;
                            godown_GridView.DataBind();

                        }
                        else
                        {
                            godown_GridView.DataSource = null;
                            godown_GridView.DataBind();
                        }
                    }
                }
            }
        }
    }
  
    
}