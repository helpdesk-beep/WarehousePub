using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using WLCBusinessLayer;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRepeater();
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
        }
        
    }
    private void BindRepeater()
    {
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_Top_30_images", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = con;
                con.Open();
                rptCarousel.DataSource = cmd.ExecuteReader();
                rptCarousel.DataBind();
                con.Close();
            }
        }
    }
    protected string GetActiveClass(int ItemIndex)
    {
        if (ItemIndex == 0)
        {
            return "active";
        }
        else
        {
            return "";
        }
    }
}

