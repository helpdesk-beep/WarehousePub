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
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
public partial class Reports_States_Geolocationwisecapacity : System.Web.UI.Page
{
    StringBuilder str = new StringBuilder();
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        
            if (Page.IsPostBack == false)
            {

                GetDist("4");
                getgeolocationdtl();
            }
       
    }
    private void GetDist(string Scope)
    {
        try
        {
            string srvr = System.Configuration.ConfigurationManager.AppSettings["HostedServer"].ToString();
            string strDist = "";

            if (Scope == "4")
            {
                strDist = "SELECT [District_Id] as  [login_id], [District_Name] as  [User_Name] FROM [tbl_MetaData_DISTRICT] order by [User_Name]";
            }
            SqlDataAdapter da = new SqlDataAdapter(strDist, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Dist.DataSource = ds.Tables[0];
                DDL_Dist.DataTextField = "User_Name";
                DDL_Dist.DataValueField = "login_id";
                DDL_Dist.DataBind();
                DDL_Dist.Items.Insert(0, "---Select---");
            }
            else
            {
                DDL_Dist.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }

    private void getDepot(string distId)
    {
        try
        {
            string str = "SELECT  [DepotID],[DepotName],[BranchId] FROM [dbo].[tbl_MetaData_DEPOT] where DistrictId='" + DDL_Dist.SelectedValue.ToString() + "' order by DepotName";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Depot.DataSource = ds.Tables[0];
                DDL_Depot.DataTextField = "DepotName";
                DDL_Depot.DataValueField = "BranchId";
                DDL_Depot.DataBind();
                
            }
            else
            {
                DDL_Depot.Items.Clear();
            }

            DDL_Depot.Visible = true;
        }

        catch (Exception ex)
        {

        }
    }
    private void getgeolocationdtl()
    {
        try
        {
            string str = "SELECT [DistrictId],[DepotID],[DepotName],[latitude],[longitude],[BranchID] FROM [Intergrated_MP_STORAGE].[dbo].[DepotStockPosition_GMap]";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];

                GridView1.DataBind();
              
            }
            else
            {
               // DDL_Depot.Items.Clear();
            }

           // DDL_Depot.Visible = true;
        }

        catch (Exception ex)
        {

        }
    }


    protected void DDL_Dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        getDepot(DDL_Dist.SelectedItem.Value.ToString());
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string qryinsert = "update [DepotStockPosition_GMap] set latitude='" + txtlatitude.Text.Trim() + "' ,longitude='" + txtlong.Text.Trim() + "' ,Ip_add='" + ip + "' where BranchID='" + DDL_Depot.SelectedValue.ToString() + "'";
        cmd.CommandText = qryinsert;
        cmd.Connection = con;
        
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
           
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Inserted Successfully.. ');", true);
            getgeolocationdtl();
        }
        catch (Exception Ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error on updation');", true);

        }
        finally
        {
            con.Close();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDL_Dist.SelectedValue = GridView1.SelectedRow.Cells[1].Text;
        getDepot(GridView1.SelectedRow.Cells[1].Text);
        DDL_Depot.SelectedValue = GridView1.SelectedRow.Cells[2].Text;
        
        txtlong.Text = GridView1.SelectedRow.Cells[5].Text;
        txtlatitude.Text=GridView1.SelectedRow.Cells[4].Text;
    }
}
