using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Diagnostics;
using System.Resources;

public partial class StatePages_View_Aggrement_and_WHR_Details : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                fillCommodity();
                fillCropyear();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Registration_Wise_Accaptance_and_WHR_Qty", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
                cmd.Parameters.AddWithValue("@cropyear", ddlcropyear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Registration_No", txttwhrno.Text.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Depositor_Gridview.DataSource = dt;
                            Depositor_Gridview.DataBind();
                        }
                        else
                        {
                            Depositor_Gridview.DataSource = null;
                            Depositor_Gridview.DataBind();
                        }
                    }
                }
            }
        }
    }
   
    public void fillCommodity()
    {
        string query2 = "";
        query2 = "select Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
        SqlCommand cmd2 = new SqlCommand(query2, con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.DataSource = ds2;
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, "--Select--");
            //ddlDepositor.SelectedValue=
        }
    }
    public void fillCropyear()
    {
        SqlCommand cmd2 = new SqlCommand("Fill_CropYear", con);
        cmd2.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count > 0)
        {
            ddlcropyear.DataSource = ds2;
            ddlcropyear.DataTextField = "Crop_Year";
            ddlcropyear.DataValueField = "ID";
            ddlcropyear.DataBind();
            ddlcropyear.Items.Insert(0, "--Select--");
            //ddlDepositor.SelectedValue=
        }
    }

    protected void txttwhrno_TextChanged(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }
}
