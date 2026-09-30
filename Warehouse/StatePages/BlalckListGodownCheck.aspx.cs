using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class StatePages_BlalckListGodownCheck : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
    }

    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("Get_BlackList_Godown_details", con))
            //using (SqlCommand cmd = new SqlCommand("Godown_Check_03Jan2024", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if(txtGodownID.Text=="")
                {
                    cmd.Parameters.AddWithValue("@GodownID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@GodownID", txtGodownID.Text);
                }
                if (txtRegistration.Text == "")
                {
                    cmd.Parameters.AddWithValue("@Registration_ID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Registration_ID", txtRegistration.Text);
                }

                
                //cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue);
                //cmd.Parameters.AddWithValue("@BranchPwd",txtBranchPwd.Text.ToString());

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    //using (DataTable dt = new DataTable())
                    //{
                    //sda.Fill(dt);
                    sda.Fill(ds);
                    DataTable tableA = ds.Tables[0];
                   // DataTable tableB = ds.Tables[1];
                    if (tableA.Rows.Count > 0)
                        {
                            Depositor_Gridview.DataSource = tableA;
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

   
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }

   

}