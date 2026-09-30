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

public partial class StatePages_UpdateCUGMobNo : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            if (!IsPostBack)
            {
                GetDistrict();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void GetBranchCUG()
    {
        try
        {
            string qry = "";
            qry = "select Branch_Id,Branch_Name,Mobile_No,(select NodalOfficerphone from tbl_MetaData_DEPOT as MD where BranchId=WC.Branch_Id) as In_Depot_Master from tbl_Warehousing_Contact as WC where Branch_Id in (select MDD.BranchId from tbl_MetaData_DEPOT as MDD where MDD.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "') order by Branch_Name";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Depositor_Gridview.DataSource = ds;
                Depositor_Gridview.DataBind();
                trmobtxt.Visible = true;
                trbtnhide.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
                trbtnhide.Visible = false;
                trmobtxt.Visible = false;
                trbtnhide.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void GetDistrict()
    {
            string qry = "";
            qry = "select District_Id,District_Name from tbl_MetaData_DISTRICT  order by District_Name";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "--Select--");
            }
    }
    protected void btnAddCompany_Click(object sender, EventArgs e)
    {
        GridViewRow gvr = Depositor_Gridview.SelectedRow;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            if (txtDFNo.Text != "")
            {
                        string qryDel = "";
                        qryDel = "update tbl_Warehousing_Contact set Mobile_No='" + txtDFNo.Text + "'  where Branch_Id='"+ gvr.Cells[0].Text +"' ";
                        SqlCommand cmdDel = new SqlCommand(qryDel, con, sqltrans);
                        int b = cmdDel.ExecuteNonQuery();
                        if (b > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Update')", true);
                            trmobtxt.Visible = false;
                            trbtnhide.Visible = false;
                            txtDFNo.Text = "";
                            txtbranch.Text = "";
                            Depositor_Gridview.DataSource = "";
                            Depositor_Gridview.DataBind();
                            ddlDistrict_SelectedIndexChanged(sender, e);
                        }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Mobile No.')", true);
            }
        }
        catch (Exception ex)
        {
            sqltrans.Rollback();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error')", true);
        }
        finally
        {
            con.Close();
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranchCUG();
    }
    protected void Depositor_Gridview_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = Depositor_Gridview.SelectedRow;
        txtbranch.Text = gvr.Cells[1].Text;
        txtDFNo.Text = gvr.Cells[2].Text;
        
    }
}
