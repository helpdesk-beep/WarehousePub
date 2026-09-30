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


public partial class Reports_States_Update_Private_Warehouse_Block_Jvs : System.Web.UI.Page
{
    //public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
   // private object ddlregion;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            try
            {
                if (!IsPostBack)
                {
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

            SqlCommand cmdd = new SqlCommand("Get_Update_Private_Warehouse_Related_Information_Choise",con);
            cmdd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                godown_GridView.DataSource = dt;
                godown_GridView.DataBind();
                //btnUpdate.Visible = false;
            }

            //SqlCommand cmd = new SqlCommand("Get_Update_Private_Warehouse_Related_Information_Choise",con);
            //cmd.CommandType = CommandType.StoredProcedure;
            ////cmd.Parameters.AddWithValue("@Flag", ddlFlag.SelectedValue);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            //if (dt.Rows.Count > 0)
            //{
            //    godown_GridView.DataSource = dt;
            //    godown_GridView.DataBind();

            //}
            //else
            //{

            //}
        }
        catch (Exception ex)
        {

        }
    }


    //protected void ddlFlag_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillGrid();
    //}


    //private void GetRegion()
    //{
    //    string strDist = "";
    //    strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Regionnm";
    //    SqlDataAdapter da = new SqlDataAdapter(strDist, con);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {

    //        ddlregion.DataSource = ds.Tables[0];
    //        ddlregion.DataTextField = "Regionnm";
    //        ddlregion.DataValueField = "Region_ID";
    //        ddlregion.DataBind();
    //        ddlregion.Items.Insert(0, "--Select--");
    //        ddldistrict.Items.Insert(0, "--Select--");
    //        ddlbranch.Items.Insert(0, "--Select--");
    //    }
    //    else
    //    {
    //        ddlregion.Items.Insert(0, "--Select--");
    //    }
    //}

    //protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //        string strDist = "";
    //        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT where Region_ID='" + ddlregion.SelectedValue + "' order by District_Name";
    //        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddldistrict.DataSource = ds.Tables[0];
    //            ddldistrict.DataTextField = "District_Name";
    //            ddldistrict.DataValueField = "District_Id";
    //            ddldistrict.DataBind();
    //            ddldistrict.Items.Insert(0, "--Select--");
    //            ddlbranch.Items.Insert(0, "--Select--");
    //    }
    //        else
    //        {
    //            ddldistrict.Items.Insert(0, "--Select--");
    //        }

    //}

    //protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string strBranch = "";
    //    //strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
    //    strBranch = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + ddldistrict.SelectedValue + "' order by Depotname";
    //    SqlDataAdapter da = new SqlDataAdapter(strBranch, con_WLC);
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

    public void Update(string hdnregid, string txtRegistration_ID, string ddlIsallowed)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("Update_Warehouse_Private_Warehouse_Block",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Godown_Id", hdnregid);
            cmd.Parameters.AddWithValue("@Registration_ID", txtRegistration_ID);
            cmd.Parameters.AddWithValue("@Is_allowed", ddlIsallowed);
            cmd.Parameters.AddWithValue("@insert_By", localIP);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                fillGrid();
                ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Updated  Successfully')", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }

   

    protected void godown_GridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = godown_GridView.Rows[rowIndex];

            //Fetch value of Name.
            string hdnregid = (row.FindControl("hdnregid") as HiddenField).Value;
            string txtRegistration_ID = (row.FindControl("txtRegistration_ID") as TextBox).Text;
            string ddlIsallowed = (row.FindControl("ddlIsallowed") as DropDownList).SelectedValue;

            Session["hdnregid"] = hdnregid.ToString();
            Session["ddlIsallowed"] = ddlIsallowed.ToString();

            Update(hdnregid, txtRegistration_ID, ddlIsallowed);

        }
    }
}
