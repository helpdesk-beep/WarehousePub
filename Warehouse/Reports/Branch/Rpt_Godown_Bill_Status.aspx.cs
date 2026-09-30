using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Reports_Branch_Rpt_Godown_Bill_Status : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Depot_DistID"] != null) && (Session["BranchId"] != null))
            {
                fillGodown();
                fillgrid(Session["BranchId"].ToString());
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
    }
    private void fillGodown()
    {
        try
        {


            string query = "";
            query = "SELECT Godown_ID,Godown_Name FROM [dbo].[tbl_MetaData_GODOWN] where BranchId='" + Session["BranchId"].ToString() + "'";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodown.Items.Clear();
                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
                ddlGodown.Items.Insert(0, "--All--");
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
    protected void fillgrid(string RID)
    {
        string Godown = "0";
        if (ddlGodown.SelectedValue == "--All--")
        {
            Godown = "0";
        }
        else
        {
            Godown = ddlGodown.SelectedValue;
        }
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Godown_Bill_Status", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", RID);
                cmd.Parameters.AddWithValue("@MonthID", ddlmonth.SelectedValue);
                cmd.Parameters.AddWithValue("@GodownID", Godown);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            //GridView1.Caption = @"<b style=""font-weight: bold;""> M.P. Warehousing & Logistics Corporarion" + "</br> " + "Storage Charges Bill";
                            // lblNoofAC.Text = dt.Rows.Count.ToString();
                        }
                        else
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid(Session["BranchId"].ToString());
    }
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid(Session["BranchId"].ToString());
    }
}