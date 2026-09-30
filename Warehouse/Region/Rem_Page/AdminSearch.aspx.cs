using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class AdminSearch : System.Web.UI.Page
{

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == "Admin")
        {
            if (!IsPostBack)
            {
                GetDist("4");

            }
        }
        else
        {

            Response.Redirect("login.aspx");
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
            string str = "SELECT [login_id], [User_Name] FROM [Storage_Login] WHERE [DistrictId] = '" + distId.ToString() + "' and Scope=1 order by [User_Name]";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Depot.DataSource = ds.Tables[0];
                DDL_Depot.DataTextField = "USER_NAME";
                DDL_Depot.DataValueField = "login_id";
                DDL_Depot.DataBind();
                DDL_Depot.Items.Insert(0, "---Select---");
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



    protected void DDL_Dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        getDepot(DDL_Dist.SelectedItem.Value.ToString());
    }
    protected void submitdo_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        try
        {
            //qry = "select DepotID,DepotName FROM [MPSCSCSVR].[MPSCSC].dbo.tbl_MetaData_DEPOT  where DistrictId='" + ddlRecDistrict.SelectedValue.ToString() + "' order by DepotName";
            qry = "select * from   [MPSCSCSVR].[MPSCSC].dbo.[issue_against_do]  where district_code='" + DDL_Dist.SelectedValue.ToString() + "' and delivery_order_no='" + txtdo.Text + "' and issueCentre_code='" + DDL_Depot.SelectedValue.ToString() + "'";
            cmd = new SqlCommand(qry, con);
           IDataAdapter da = new SqlDataAdapter(cmd);
           DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
               
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }
    protected void submitchallan_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        try
        {
            if (txtchallan.Text != "")
            {
                //qry = "select DepotID,DepotName FROM [MPSCSCSVR].[MPSCSC].dbo.tbl_MetaData_DEPOT  where DistrictId='" + ddlRecDistrict.SelectedValue.ToString() + "' order by DepotName";
                qry = "select  " + txtchallan.Text + "";
                cmd = new SqlCommand(qry, con);
                IDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtgodownid.Text != "")
            {
                cmd = new SqlCommand("delete_godown", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Dist_Id", DDL_Dist.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@DepotId", DDL_Depot.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Godown_ID", txtgodownid.Text);

                int res = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Fill Data')", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in Deletion')", true);
        }
    }
}