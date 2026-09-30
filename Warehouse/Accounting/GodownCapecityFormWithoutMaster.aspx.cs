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
using System.Security.Principal;

public partial class Accounting_GodownCapecityFormWithoutMaster : System.Web.UI.Page
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
        string query = "select commodity_id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {


            DdlCurrodity.DataSource = ds.Tables[0];
            DdlCurrodity.DataTextField = "Commodity_Name";
            DdlCurrodity.DataValueField = "commodity_id";
            DdlCurrodity.DataBind();
            DdlCurrodity.Items.Insert(0, new ListItem("Commodity चुने", "0"));


        }
    }

    protected void gvCol3_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
       
        HiddenField hdngodownid = gvCol3.Rows[e.RowIndex].FindControl("hdngodownid") as HiddenField;
        TextBox GodownTotalCapacity1 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalCapacity1") as TextBox;
        TextBox GodownTotalUseCapacity1 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalUseCapacity1") as TextBox;
       

        TextBox GodownTotalCapacity2 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalCapacity2") as TextBox;
        TextBox GodownTotalUseCapacity2 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalUseCapacity2") as TextBox;
    

        TextBox GodownTotalCapacity3 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalCapacity3") as TextBox;
        TextBox GodownTotalUseCapacity3 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalUseCapacity3") as TextBox;
 

        TextBox GodownTotalCapacity4 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalCapacity4") as TextBox;
        TextBox GodownTotalUseCapacity4 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalUseCapacity4") as TextBox;
       

        TextBox GodownTotalCapacity5 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalCapacity5") as TextBox;
        TextBox GodownTotalUseCapacity5 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalUseCapacity5") as TextBox;

        TextBox GodownTotalCapacity6 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalCapacity6") as TextBox;
        TextBox GodownTotalUseCapacity6 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalUseCapacity6") as TextBox;

        TextBox GodownTotalCapacity7 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalCapacity7") as TextBox;
        TextBox GodownTotalUseCapacity7 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalUseCapacity7") as TextBox;

        TextBox GodownTotalCapacity8 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalCapacity8") as TextBox;
        TextBox GodownTotalUseCapacity8 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalUseCapacity8") as TextBox;

        TextBox GodownTotalCapacity9 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalCapacity9") as TextBox;
        TextBox GodownTotalUseCapacity9 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalUseCapacity9") as TextBox;

        TextBox GodownTotalCapacity10 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalCapacity10") as TextBox;
        TextBox GodownTotalUseCapacity10 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalUseCapacity10") as TextBox;

        TextBox GodownTotalCapacity11 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalCapacity11") as TextBox;
        TextBox GodownTotalUseCapacity11 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalUseCapacity11") as TextBox;

        TextBox GodownTotalCapacity12 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalCapacity12") as TextBox;
        TextBox GodownTotalUseCapacity12 = gvCol3.Rows[e.RowIndex].FindControl("GodownTotalUseCapacity12") as TextBox;
  

        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Sp_Godown_Capacity_QTY", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchID"].ToString());
            cmd.Parameters.AddWithValue("@Godown_ID", hdngodownid.Value);
            cmd.Parameters.AddWithValue("@DdlDepositer_ID", DdlDepositer.SelectedValue);
            cmd.Parameters.AddWithValue("@DdlCurrodity_ID", DdlCurrodity.SelectedValue);
            cmd.Parameters.AddWithValue("@GodownTotalCapacity_2010_11", GodownTotalCapacity1.Text);
            cmd.Parameters.AddWithValue("@GodownTotalUseCapacity_2010_11", GodownTotalUseCapacity1.Text);
      

            cmd.Parameters.AddWithValue("@GodownTotalCapacity_2011_12", GodownTotalCapacity2.Text);
            cmd.Parameters.AddWithValue("@GodownTotalUseCapacity_2011_12", GodownTotalUseCapacity2.Text);
           

            cmd.Parameters.AddWithValue("@GodownTotalCapacity_2012_13", GodownTotalCapacity3.Text);
            cmd.Parameters.AddWithValue("@GodownTotalUseCapacity_2012_13", GodownTotalUseCapacity3.Text);
          

            cmd.Parameters.AddWithValue("@GodownTotalCapacity_2013_14", GodownTotalCapacity4.Text);
            cmd.Parameters.AddWithValue("@GodownTotalUseCapacity_2013_14", GodownTotalUseCapacity4.Text);
           

            cmd.Parameters.AddWithValue("@GodownTotalCapacity_2014_15", GodownTotalCapacity5.Text);
            cmd.Parameters.AddWithValue("@GodownTotalUseCapacity_2014_15", GodownTotalUseCapacity5.Text);

            cmd.Parameters.AddWithValue("@GodownTotalCapacity_2015_16", GodownTotalCapacity6.Text);
            cmd.Parameters.AddWithValue("@GodownTotalUseCapacity_2015_16", GodownTotalUseCapacity6.Text);

            cmd.Parameters.AddWithValue("@GodownTotalCapacity_2016_17", GodownTotalCapacity7.Text);
            cmd.Parameters.AddWithValue("@GodownTotalUseCapacity_2016_17", GodownTotalUseCapacity7.Text);

            cmd.Parameters.AddWithValue("@GodownTotalCapacity_2017_18", GodownTotalCapacity8.Text);
            cmd.Parameters.AddWithValue("@GodownTotalUseCapacity_2017_18", GodownTotalUseCapacity8.Text);

            cmd.Parameters.AddWithValue("@GodownTotalCapacity_2018_19", GodownTotalCapacity9.Text);
            cmd.Parameters.AddWithValue("@GodownTotalUseCapacity_2018_19", GodownTotalUseCapacity9.Text);

            cmd.Parameters.AddWithValue("@GodownTotalCapacity_2019_20", GodownTotalCapacity10.Text);
            cmd.Parameters.AddWithValue("@GodownTotalUseCapacity_2019_20", GodownTotalUseCapacity10.Text);

            cmd.Parameters.AddWithValue("@GodownTotalCapacity_2020_21", GodownTotalCapacity11.Text);
            cmd.Parameters.AddWithValue("@GodownTotalUseCapacity_2020_21", GodownTotalUseCapacity11.Text);

            cmd.Parameters.AddWithValue("@GodownTotalCapacity_2021_22", GodownTotalCapacity12.Text);
            cmd.Parameters.AddWithValue("@GodownTotalUseCapacity_2021_22", GodownTotalUseCapacity12.Text);
  
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            datafll();

                  }
        catch (Exception ex)
        {
          
            Console.WriteLine(ex.Message);
        }
        ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Data Save Successfully')", true);
     
    }

    protected void DdlCurrodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmdd = new SqlCommand("Sp_Godown_capacity_Show", con);
        cmdd.CommandType = CommandType.StoredProcedure;
        cmdd.Parameters.AddWithValue("@Branchid", Session["BranchID"].ToString());
        cmdd.Parameters.AddWithValue("@DdlCurrodity", DdlCurrodity.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmdd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            gvCol3.DataSource = dt;
            gvCol3.DataBind();
        }
        else
        {
            gvCol3.DataSource = null;
            gvCol3.DataBind();
        }
    }

    void datafll()
    {

        SqlCommand cmdd = new SqlCommand("Sp_Godown_capacity_Show", con);
        cmdd.CommandType = CommandType.StoredProcedure;
        cmdd.Parameters.AddWithValue("@Branchid", Session["BranchID"].ToString());
        cmdd.Parameters.AddWithValue("@DdlCurrodity", DdlCurrodity.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmdd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            gvCol3.DataSource = dt;
            gvCol3.DataBind();
        }
        else
        {
            gvCol3.DataSource = null;
            gvCol3.DataBind();
        }
    }


}