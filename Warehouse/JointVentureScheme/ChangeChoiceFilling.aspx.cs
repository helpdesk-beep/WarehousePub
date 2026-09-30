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

public partial class TribalGodown_ChangeChoiceFilling : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Reg_No"] != null && Session["Reg_No"] != "")
        {
            string regid = Session["Reg_No"].ToString();
        }
        else
        {

            Response.Redirect("WarehouseHome.aspx");
        }
    }

    //protected void btn_Click(object sender, EventArgs e)
    //{

    //}



    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Offered.aspx");
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Sp_Change_Choice_Filling", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Regid", Session["Reg_No"].ToString());
                
                con.Open();
                int k = cmd.ExecuteNonQuery();
                con.Close();              
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('श्रेणी ब से अ में परिवर्तित हो चुकी हे |'); </script> ");

            }
        }

    }
}
