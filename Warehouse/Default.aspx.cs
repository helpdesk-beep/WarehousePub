using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class _Default : System.Web.UI.Page 
{
    public SqlConnection conStr2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string PFID = "";
    SqlTransaction sqltran;
    int a_id = 0;
    SqlCommand cmd;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //DataTable dt = new Admin().GetNewsList();
            //dt = new Admin().GetNewsList();
            //if (dt.Rows.Count > 0)
            //{
            //    rptNewsUpdate.DataSource = dt;
            //    rptNewsUpdate.DataBind();
            //    rptNewsUpdate.Visible = true;
            //}

            //else
            //{
            //    rptNewsUpdate.Visible = false;
            //}

            // dt = new Admin().GetDownloadList();
            //if (dt.Rows.Count > 0)
            //{
            //    rptDownload.DataSource = dt;
            //    rptDownload.DataBind();
            //}
            //fillGrid();
        }
        
    }
  
    //protected void fillGrid()
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("Get_Godown_Hired_Type_Wise", con))
    //        {
    //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //           // cmd.Parameters.AddWithValue("@BranchID", Session["UserId"].ToString());
    //            using (SqlDataAdapter sda = new SqlDataAdapter())
    //            {
    //                cmd.Connection = con;
    //                sda.SelectCommand = cmd;
    //                using (DataTable dt = new DataTable())
    //                {
    //                    sda.Fill(dt);
    //                    if (dt.Rows.Count > 0)
    //                    {
    //                        lblowndgdn.Text = dt.Rows[0]["Owned"].ToString();
    //                        lblpg.Text = dt.Rows[0]["PVTPEG"].ToString();
    //                        lblJVS.Text = dt.Rows[0]["JVS"].ToString();
    //                        lblHired.Text = dt.Rows[0]["Hired"].ToString();
    //                        lblSteelSilo.Text = dt.Rows[0]["SteelSilo"].ToString();
    //                        lblSiloBags.Text = dt.Rows[0]["SiloBags"].ToString();
    //                        lblWDRA.Text = dt.Rows[0]["WDRA"].ToString();
    //                        lblMarkfed.Text = dt.Rows[0]["Markfed"].ToString();
    //                        lblTS.Text = dt.Rows[0]["TS"].ToString();
    //                        lblFCI.Text = dt.Rows[0]["FCI"].ToString();
    //                        lblCWC.Text = dt.Rows[0]["CWC"].ToString();
    //                    }
    //                    else
    //                    {

    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
}

