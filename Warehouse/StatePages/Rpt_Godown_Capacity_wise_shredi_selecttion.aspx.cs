using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;


public partial class StatePages_Rpt_Godown_Capacity_wise_shredi_selecttion : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["UserName"].ToString() != null)
            {
                if (!IsPostBack)
                {
                    fillgrid();
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }



        }


    }

    private void fillgrid()
    {
        try
        {
       

            string query1 = "select Region_Id,region from tbl_MetaData_Region";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {

                ddlregion.DataSource = ds1.Tables[0];
                ddlregion.DataTextField = "region";
                ddlregion.DataValueField = "Region_Id";
                ddlregion.DataBind();
                ddlregion.Items.Insert(0, new ListItem("Region चुने", "0"));

            }


           
        }
        catch (Exception)
        {
            //////
        }
    }

    //protected void ddldist_TextChanged(object sender, EventArgs e)
    //{
    //    string query = "select BranchId,DepotName from tbl_metadata_depot where DistrictId ='" + ddldist.SelectedValue + "'";
    //    SqlCommand cmd = new SqlCommand(query, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlbranch.DataSource = ds.Tables[0];
    //        ddlbranch.DataTextField = "DepotName";
    //        ddlbranch.DataValueField = "BranchId";
    //        ddlbranch.DataBind();         
    //        ddlbranch.Items.Insert(0, new ListItem("ब्रांच चुने ", "0"));

    //    }
    //}

    protected void SearchID_Click(object sender, EventArgs e)
    {

        if (ddlregion.SelectedValue != "0" && ddldist.SelectedValue != "0" && ddllevelType.SelectedValue!="0")
        {
            SqlCommand cmdd = new SqlCommand("Sp_Godown_Capacity_Wise_Choise_Filling", con);
            cmdd.CommandType = CommandType.StoredProcedure;
            cmdd.Parameters.AddWithValue("@RegionID", ddlregion.SelectedValue);
            cmdd.Parameters.AddWithValue("@DistrictID", ddldist.SelectedValue);
            cmdd.Parameters.AddWithValue("@Type", ddllevelType.SelectedValue);
            

            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                //GridView1.FooterRow.Style.Add("text-align", "center");
                //GridView1.FooterRow.Cells[2].Text = "Total";
                //GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalRegistrasion")).ToString();
                //GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalChoice")).ToString();

                //GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ChoiceA")).ToString();
                //GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ChoiceB")).ToString();
                //GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ChoiceBR")).ToString();
                //GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("RamaningForChoices")).ToString();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();              
            }
        }

        else
        {

            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('सभी Fields का चयन करे'); </script> ");
        }


 







    }

    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        string query1 = "select District_Id, District_Name from tbl_metadata_district MD left  join tbl_MetaData_Region MR on MR.Region_Id = MD.Region_ID where MD.Region_ID ='" + ddlregion.SelectedValue + "'";
        SqlCommand cmd1 = new SqlCommand(query1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        if (ds1.Tables[0].Rows.Count > 0)
        {

            ddldist.DataSource = ds1.Tables[0];
            ddldist.DataTextField = "District_Name";
            ddldist.DataValueField = "District_Id";
            ddldist.DataBind();
            ddldist.Items.Insert(0, new ListItem("जिला चुने", "0"));

           // ddlbranch.Items.Insert(0, new ListItem("Branch चुने", "0"));
           // ddlbranch.Items.Clear();

        }
    }
}