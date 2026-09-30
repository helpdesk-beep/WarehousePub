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

public partial class Accounting_VarificationofGodownCapacaty : System.Web.UI.Page
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
        string query1 = "select MG18.Godown_ID,MG18.Godown_Name,MG18.Godown_Capacity,MG18.Godown_Scientific_Capacity from tbl_MetaData_GODOWN_2018 as MG18 left join Tbl_Flag_Godown_Cap as GC on MG18.Godown_ID=GC.Godown_ID where  MG18.BranchID = '" + Session["BranchId"].ToString() + "' AND GC.Godown_ID is null";
        SqlCommand cmd1 = new SqlCommand(query1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        
        da1.Fill(ds1);

        GridView1.DataSource = ds1.Tables[0];
        GridView1.DataBind();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
       
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        HiddenField hdngodownid = GridView1.Rows[e.RowIndex].FindControl("hdngodownid") as HiddenField;
        Label Godownmcap = GridView1.Rows[e.RowIndex].FindControl("Godownmcap") as Label;
        TextBox GodownScientificcap= GridView1.Rows[e.RowIndex].FindControl("GodownScientificcap") as TextBox;
        TextBox backcapacity = GridView1.Rows[e.RowIndex].FindControl("backcapacity") as TextBox;
        DropDownList GodownCap= GridView1.Rows[e.RowIndex].FindControl("GodownCap") as DropDownList;
        if (backcapacity.Text != "")
        {

            if (GodownCap.SelectedValue != "0")
            {
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Sp_Flag_Godown_Cap", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
                    cmd.Parameters.AddWithValue("@Godown_ID", hdngodownid.Value);
                    cmd.Parameters.AddWithValue("@Godown_mcap", Godownmcap.Text);
                    cmd.Parameters.AddWithValue("@GodownScientificcap", GodownScientificcap.Text);
                    cmd.Parameters.AddWithValue("@Godown_backcapacity", backcapacity.Text);
                    cmd.Parameters.AddWithValue("@Godown_Flag", GodownCap.SelectedValue);
                    cmd.Parameters.AddWithValue("@IP", ip);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    fill();
                    ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Godown ADD Successfully')", true);

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('कृप्या मैपिंग  हेतु गोडाउन की उपलब्धता चयन करे  ')", true);

            }
        }
        else
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Please Fill Vacant Capacity   ')", true);

        }



    }

    protected void show_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShowGodownvarificationcap.aspx");
    }
}