using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web;

public partial class Accounting_ShowGodownvarificationcap : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (!IsPostBack)
            {
                fill();

            }
        }
        else
        {
            Response.Redirect("../Logout.aspx");
        }
    }



    protected void fill()
    {
        string query1 = "select TMG.Godown_Name ,FGC.*,case when Godown_flag='Y' then 'SELECT' else 'REJECT' end flag from Tbl_Flag_Godown_Cap as FGC left join tbl_MetaData_GODOWN_2018 as TMG on FGC.Godown_ID=TMG.Godown_ID  where FGC.Branch_ID='" + Session["BranchId"].ToString() + "'"
;
        SqlCommand cmd1 = new SqlCommand(query1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();

        da1.Fill(ds1);

        GridView1.DataSource = ds1.Tables[0];
        GridView1.DataBind();
    }

    protected void show_Click(object sender, EventArgs e)
    {
        Response.Redirect("VarificationofGodownCapacaty.aspx");
    }
}