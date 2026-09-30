using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Principal;


public partial class Reports_States_Region_Insurance_Details_Delete : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    // private object ddlregion;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
        {
            try
            {
                if (!IsPostBack)
                {
                    //GetDist();
                    fillGrid();
                    //GetRegion();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    private void fillGrid()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("Sp_jvs_insurance_Detail_delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Flag", ddlFlag.SelectedValue);
            cmd.Parameters.AddWithValue("@RegionID", Session["Region_ID"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                godown_GridView.DataSource = ds.Tables[0];
                godown_GridView.DataBind();

            }
            else
            {

            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void ddlFlag_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }





    protected void godown_GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        HiddenField RegID = godown_GridView.Rows[e.RowIndex].FindControl("RegID") as HiddenField;


        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Delete_Insurance_Details",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RegID", RegID.Value);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            fillGrid();
            //ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Delete Successfully')", true);
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                //GetEmployeeDetails();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
            }

        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }


    }
    //private void GetDist()
    //{
    //    string strDist = "";
    //    strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT where Region_ID='" + Session["Region_ID"].ToString() + "' order by District_Name";
    //    SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddldistrict.DataSource = ds.Tables[0];
    //        ddldistrict.DataTextField = "District_Name";
    //        ddldistrict.DataValueField = "District_Id";
    //        ddldistrict.DataBind();
    //        ddldistrict.Items.Insert(0, "--Select--");
    //        ddlbranch.Items.Insert(0, "--Select--");
    //    }
    //    else
    //    {
    //        ddldistrict.Items.Insert(0, "--Select--");
    //    }
    //}


    //protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    {
    //        string strDist = "";
    //        strDist = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + ddldistrict.SelectedValue + "' order by Depotname"; ;
    //        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlbranch.DataSource = ds.Tables[0];
    //            ddlbranch.DataTextField = "DepotName";
    //            ddlbranch.DataValueField = "BranchId";
    //            ddlbranch.DataBind();
    //            ddlbranch.Items.Insert(0, "--Select--");
    //        }
    //        else
    //        {
    //            ddlbranch.Items.Insert(0, "--Select--");
    //        }
    //    }
    //}
}
