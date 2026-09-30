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

public partial class Reports_Region_UpdateShowGodownvarificationcap : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
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
        TextBox GodownScientificcap = GridView1.Rows[e.RowIndex].FindControl("GodownScientificcap") as TextBox;
        TextBox backcapacity = GridView1.Rows[e.RowIndex].FindControl("backcapacity") as TextBox;
        DropDownList ddlflag = GridView1.Rows[e.RowIndex].FindControl("ddlflag") as DropDownList;

        if (ddlflag.SelectedValue != "0")
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Sp_Show_Godown_varificationcap_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Godown_ID", hdngodownid.Value);
                cmd.Parameters.AddWithValue("@Godown_flag", ddlflag.SelectedValue);
                cmd.Parameters.AddWithValue("@GoDown_Scient_Cap", GodownScientificcap.Text);
                cmd.Parameters.AddWithValue("@GoDown_Vacant_Cap", backcapacity.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            fillcc();

            ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Godown Update Successfully')", true);
        }
        else 
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Pls select Godown_Available Storage')", true);
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

        string query1 = "select TMG.Godown_Name ,FGC.*,case when Godown_flag='Y' then 'SELECT' else 'REJECT' end flag from Tbl_Flag_Godown_Cap as FGC left join tbl_MetaData_GODOWN_2018 as TMG on FGC.Godown_ID=TMG.Godown_ID  where FGC.Branch_ID='" + ddlbranchname.SelectedValue + "'";
        SqlCommand cmd1 = new SqlCommand(query1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        GridView1.DataSource = ds1.Tables[0];
        GridView1.DataBind();
    }


}
