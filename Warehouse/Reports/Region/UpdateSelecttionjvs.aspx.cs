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

public partial class Reports_Region_UpdateSelecttionjvs : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
            {
                if (!IsPostBack)
                {
                    fill();
                }
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fill()
    {

        string query = "select District_Id, District_Name from tbl_metadata_district";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {

            ddldist.DataSource = ds.Tables[0];
            ddldist.DataTextField = "District_Name";
            ddldist.DataValueField = "District_Id";
            ddldist.DataBind();
            ddldist.Items.Insert(0, new ListItem("जिला चुने", "0"));

        }




      
    }


    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        
        HiddenField hdngodownid = GridView1.Rows[e.RowIndex].FindControl("hdngodownid") as HiddenField;
       // TextBox GodownScientificcap = GridView1.Rows[e.RowIndex].FindControl("GodownScientificcap") as TextBox;
       // TextBox backcapacity = GridView1.Rows[e.RowIndex].FindControl("backcapacity") as TextBox;
        DropDownList ddlflag = GridView1.Rows[e.RowIndex].FindControl("ddlflag") as DropDownList;

        if (ddlflag.SelectedValue != "0")
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Sp_Choise_filling", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@regid", hdngodownid.Value);
                cmd.Parameters.AddWithValue("@Godown_flag", ddlflag.SelectedValue);
               

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            fillcc();

            ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('श्रेणी Update Successfully')", true);
        }
        else 
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Pls select श्रेणी')", true);
        }

    }

    protected void ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {
        string query = "select BranchId,DepotName from tbl_metadata_depot where DistrictId ='" + ddldist.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranchname.DataSource = ds.Tables[0];
            ddlbranchname.DataTextField = "DepotName";
            ddlbranchname.DataValueField = "BranchId";
            ddlbranchname.DataBind();

            ddlbranchname.Items.Insert(0, new ListItem("ब्रांच चुने ", "0"));
        }
    }
    protected void searchid_Click(object sender, EventArgs e)
    {
        fillcc();
    }

    protected void fillcc()
    {

        string query1 = "select TMR.region, MD.District_Name,TMD.DepotName,TWR.Warehouse_Name,JCF.* ,case when JCF.Choice = 'A' then N'अ' when JCF.Choice = 'BR' then N'ब्रांच से रिजेक्‍ट' else N'ब ' end Cho from Tbl_JVS_Choise_Filling JCF left join tbl_MetaData_DISTRICT MD on MD.District_Id = JCF.Dist_ID left join tbl_MetaData_DEPOT TMD on TMD.BranchId = JCF.Branch_ID left join tbl_MetaData_Region TMR on TMR.Region_Id = MD.Region_ID left join tbl_WarehouseRegistration TWR on TWR.Registration_Id = JCF.Reg_ID  where  JCF.Branch_ID = '" + ddlbranchname.SelectedValue + "'";
        SqlCommand cmd1 = new SqlCommand(query1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        GridView1.DataSource = ds1.Tables[0];
        GridView1.DataBind();
    }


}
