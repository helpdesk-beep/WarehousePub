using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
public partial class JointVentureScheme_BranchJvsChoiceFilling : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlTransaction sqltran;
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = null;
    string Branch = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.Cache.SetExpires(DateTime.Now);
        //Response.Cache.SetNoStore();
        string SessBranch = Session["UserName"].ToString();
        string SessBranchID = Session["UserId"].ToString();
        if (SessBranch != "" && SessBranchID != "")
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    fill();
                }
            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
    }
        protected void fill()
        {


            string query1 = "select * from tbl_WarehouseRegistration where Registration_Id not in (select Reg_ID from Tbl_JVS_Choise_Filling) and BranchId = '" + Session["UserId"].ToString() + "' ";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            GridView1.DataSource = ds1.Tables[0];
            GridView1.DataBind();
        }



        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            HiddenField hdnregid = GridView1.Rows[e.RowIndex].FindControl("hdnregid") as HiddenField;
            HiddenField HiddenDist = GridView1.Rows[e.RowIndex].FindControl("HiddenDist") as HiddenField;
            Label Label1 = GridView1.Rows[e.RowIndex].FindControl("Label1") as Label;
            Label Label3 = GridView1.Rows[e.RowIndex].FindControl("Label3") as Label;
            DropDownList ddlchouse = GridView1.Rows[e.RowIndex].FindControl("ddlchouse") as DropDownList;
            if (ddlchouse.SelectedValue != "0")
            {
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Sp_Branch_Choice_Filling", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Reg_ID", hdnregid.Value);
                    cmd.Parameters.AddWithValue("@Dist_ID", HiddenDist.Value);
                    cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@Warehouse_Name", Label1.Text.Trim());
                    cmd.Parameters.AddWithValue("@WareHouse_Cap", Label3.Text.Trim());
                    cmd.Parameters.AddWithValue("@Choice", "BR");
                    cmd.Parameters.AddWithValue("@insert_By", Request.UserHostAddress);
                    //cmd.Parameters.AddWithValue("@Godown_Id", hdnregid.Value);
                    cmd.Parameters.AddWithValue("@Remark", ddlchouse.SelectedValue);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    fill();
                    ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Choice Filling Remark Successfully')", true);

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Remark चयन करे')", true);
            }


        }
    }

