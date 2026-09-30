using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mobile_GodownCordinates2 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlTransaction sqltran;
    string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            fillGodnList();
        }
    }

    private void fillGodnList()
    {
        if (Session["Depot_DistID"] != null)
        {
            string query = " SELECT Godown_ID,Godown_Name  FROM tbl_MetaData_GODOWN where DistrictId ='" + Session["Depot_DistID"].ToString() + "' and  BranchId  ='" + Session["BranchId"].ToString() + "' order by Godown_Name Asc";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodown.DataSource = ds.Tables[0];
                ddlgodown.DataTextField = "Godown_Name";
                ddlgodown.DataValueField = "Godown_ID";
                ddlgodown.DataBind();
                ddlgodown.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlgodown.Items.Insert(0, "--Select--");
            }
        }
        else
        {
            Response.Redirect("mlogin.aspx");
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {


        string s = (this.Request.Form.Get("n_test"));
        //  lblmsg.Text = "the n_test value is " + s;
        string longitude = (this.Request.Form.Get("n_long"));
        if (s != "" && longitude != "")
        {
            string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            qry = "Insert Into tbl_MetaData_GODOWN_log SELECT * from tbl_MetaData_GODOWN where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and BranchID='" + Session["BranchId"].ToString() + "'";
            cmd = new SqlCommand(qry, con);
            int c = cmd.ExecuteNonQuery();

            if (c > 0)
            {
                qry = "update [tbl_MetaData_GODOWN] set Latitude='" + s + "' , Longitude='" + longitude + "' where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and BranchID='" + Session["BranchId"].ToString() + "'";
                cmd = new SqlCommand(qry, con);
                cmd.ExecuteNonQuery();
                lblmsg.Text = "Coordinates Updated";

                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Coordinates Updated '); </script> ");

            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        else
        {
            lblmsg.Text = "Please click/Tap on red baloon within map";
        }

    }
}