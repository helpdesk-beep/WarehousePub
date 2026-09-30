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

public partial class StatePages_DltChoiseFilling : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            if (!IsPostBack)
            {
                fill();


            }
       }
    }


    


        protected void fill()
    {

        string query1 = "select MD.District_Name,MDE.DepotName,TWR.Warehouse_Name, case when CF.Choice='A' then N'अ' when CF.Choice='BR' then N'ब्रांच से रिजेक्‍ट' else N'ब' end Choice,CF.Reg_ID,convert(varchar(10),CF.Insert_Date,103) insertdate from Tbl_JVS_Choise_Filling as CF left join tbl_MetaData_DISTRICT as MD on MD.District_Id = CF.Dist_ID left join tbl_MetaData_DEPOT as MDE on MDE.BranchId = CF.Branch_ID left join tbl_WarehouseRegistration TWR on TWR.Registration_Id = CF.Reg_ID ";
        SqlCommand cmd1 = new SqlCommand(query1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        GridView1.DataSource = ds1.Tables[0];
        GridView1.DataBind();
    }
    


    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        HiddenField regid = GridView1.Rows[e.RowIndex].FindControl("regid") as HiddenField;
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_ChoiseFilling_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RegID", regid.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
        fill();
        ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Choice Filling Delete  Successfully')", true);

    }




    protected void searchreg_Click(object sender, EventArgs e)
    {
        string query1 = "select MD.District_Name,MDE.DepotName,TWR.Warehouse_Name, case when CF.Choice='A' then N'अ' when CF.Choice='BR' then N'ब्रांच से रिजेक्‍ट' else N'ब' end Choice,CF.Reg_ID,convert(varchar(10),CF.Insert_Date,103) insertdate from Tbl_JVS_Choise_Filling as CF left join tbl_MetaData_DISTRICT as MD on MD.District_Id = CF.Dist_ID left join tbl_MetaData_DEPOT as MDE on MDE.BranchId = CF.Branch_ID left join tbl_WarehouseRegistration TWR on TWR.Registration_Id = CF.Reg_ID where CF.Reg_ID='" + regidserch.Text.Trim() + "'";
        SqlCommand cmd1 = new SqlCommand(query1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        GridView1.DataSource = ds1.Tables[0];
        GridView1.DataBind();
    }
}