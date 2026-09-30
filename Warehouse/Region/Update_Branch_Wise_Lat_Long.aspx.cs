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
using System.Globalization;
using System.Net;
using System.Drawing;

public partial class Region_Update_Branch_Wise_Lat_Long : System.Web.UI.Page
{
    SqlTransaction sqltrans;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlConnection jvscon = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                fillDistrict();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }

    private void fillDistrict()
    {
        try
        {
            string query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT]  where Region_ID='" + Session["Region_ID"].ToString() + "' order by District_Name asc";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "---Select---");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridwhr();
    }

    public void FillGridwhr()
    {

        gv_whr.DataSource = null;
        gv_whr.DataBind();

        SqlCommand cmd = new SqlCommand("Branch_Wise_Lat_Log_Details", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gv_whr.DataSource = ds;
            gv_whr.DataBind();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No WHR Found...')", true);
            gv_whr.DataSource = null;
            gv_whr.DataBind();
        }
    }
    protected void gv_whr_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        Label lblBranch_Name = gv_whr.Rows[e.RowIndex].FindControl("lblBranch_Name") as Label;
        Label lblBranchid = gv_whr.Rows[e.RowIndex].FindControl("lblBranchid") as Label;
        TextBox txtLatitude = gv_whr.Rows[e.RowIndex].FindControl("txtLatitude") as TextBox;
        TextBox txtlongitude = gv_whr.Rows[e.RowIndex].FindControl("txtlongitude") as TextBox;

        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Update_Branch_Wise_Lat_Long", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_Id", lblBranchid.Text);
            cmd.Parameters.AddWithValue("@Latitude", txtLatitude.Text);
            cmd.Parameters.AddWithValue("@longitude", txtlongitude.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
        FillGridwhr();
        //ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Record Save Successfully')", true);
        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Save Successfully ..'); </script> ");
    }
}