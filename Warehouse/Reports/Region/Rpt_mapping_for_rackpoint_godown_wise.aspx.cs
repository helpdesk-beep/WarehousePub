using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Reports_Region_Rpt_mapping_for_rackpoint_godown_wise : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["UserName"] = "MPSWLC";
        
            if (!IsPostBack)
            {
                //GetBranch();
                //fillgrid();
                fillgrid();
            }
        
    }

    //private void GetBranch()
    //{
    //    string strBranch = "";
    //    //strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
    //    //strBranch = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + DistID + "' order by Depotname";
    //    strBranch = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot  order by Depotname";
    //    SqlDataAdapter da = new SqlDataAdapter(strBranch,con);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlbranch.DataSource = ds.Tables[0];
    //        ddlbranch.DataTextField = "Depotname";
    //        ddlbranch.DataValueField = "BranchID";
    //        ddlbranch.DataBind();
    //        ddlbranch.Items.Insert(0, "--Select--");
    //    }
    //    else
    //    {
    //        ddlbranch.Items.Insert(0, "--Select--");
    //    }
    //}

    protected void fillgrid()
    {

        try
        {

            //string query = "";
            SqlCommand cmdd = new SqlCommand("Rpt_mapping_for_rackpoint_godown_wise", con);
            cmdd.CommandType = CommandType.StoredProcedure;
            cmdd.Parameters.AddWithValue("@Regionid", Session["Region_ID"].ToString());
            //cmdd.Parameters.AddWithValue("@Truck_Chit_No", txtTruckChitNo.Text);
            //cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                gvbranch.DataSource = dt;
                gvbranch.DataBind();
                //GridView1.FooterRow.Style.Add("text-align", "center");
                //GridView1.FooterRow.Cells[6].Text = "Total";
                //GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalGoCap")).ToString();
                //GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ScientificCapacity")).ToString();
            }
            else
            {
                gvbranch.DataSource = null;
                gvbranch.DataBind();
                ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('No  Data Found')", true);
                return;
            }
        }
        catch (Exception ex)
        {
            //////
        }

    }

    //protected void txtTruckChitNo_TextChanged(object sender, EventArgs e)
    //{
    //    fillgrid();
    //}

    //protected void btnsearch_Click(object sender, EventArgs e)
    //{
    //    fillgrid();
    //}
}