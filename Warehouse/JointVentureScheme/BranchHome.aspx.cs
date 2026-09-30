using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class JointVentureScheme_BranchHome : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        string sess = Session["UserName"].ToString();
        string BranchID = Session["UserId"].ToString();
        if (sess != "")
        {
            if (!IsPostBack)
            {
                lbluser.Text = sess;
            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Logins.aspx");
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
       
    }
    public string Tcheckdatetimes()
    {
        DateTime ServerDate = new DateTime();
        //Test
        //DateTime _effective_date = Convert.ToDateTime("02/18/2018 00:05:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("02/26/2018 05:00:00 PM");
        //Actual
        DateTime _effective_date = Convert.ToDateTime("02/18/2019 11:59:00 AM");
        DateTime _Closing_date = Convert.ToDateTime("06/30/2019 11:59:00 PM");
        ////Old
      //  DateTime _effective_date = Convert.ToDateTime("06/27/2017 11:30:00 AM");
       // DateTime _Closing_date = Convert.ToDateTime("07/17/2017 05:00:00 PM");

        string S = "";
        string QueryMax = "select getdate() as CDateTime";
        cmd = new SqlCommand(QueryMax, con);
        con.Open();
        string str3 = cmd.ExecuteScalar().ToString();
        con.Close();

        if ((str3 != String.Empty) || str3 != "")
        {
            ServerDate = Convert.ToDateTime(str3);
            ///Manage Time        
            //ServerDate = ServerDate.AddMinutes(-4);
            ServerDate = ServerDate.AddMinutes(-2);
        }
        if (ServerDate < _effective_date)
        {
            S = "NS";
        }
        else if (ServerDate > _Closing_date)
        {
            S = "NE";
        }
        else
        {
            S = "Y";
        }
        return S;
    }
}
