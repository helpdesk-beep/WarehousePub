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

public partial class StatePages_DeleteAcceptanceNoteRabi : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = null;
    SqlTransaction sqltrans;
    string RegionID = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                getdistrict();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    public void GetBranchData()
    {
        try
        {
            //string qry = "";
            //qry = "select Godown_ID,Godown_Name,Hired_Type,Storage_Type,Godown_Capacity,Closing_Balance,LicNum,convert(varchar(10),LicDate,103) as LicDate,Godown_Scientific_Capacity  from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "'";
            cmd = new SqlCommand("dbo.Get_Accaptance_Details_Rabi", con, sqltrans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Depositor_Gridview.DataSource = ds;
                Depositor_Gridview.DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
                Depositor_Gridview.DataSource = null;
                Depositor_Gridview.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void GetAcceptanceData(String DF_Receipt_Id)
    {
        try
        {
            //string qry = "";
            //qry = "select Godown_ID,Godown_Name,Hired_Type,Storage_Type,Godown_Capacity,Closing_Balance,LicNum,convert(varchar(10),LicDate,103) as LicDate,Godown_Scientific_Capacity  from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "'";
            cmd = new SqlCommand("dbo.Get_Acceptance_Details_ReceiptID_Rabi", con, sqltrans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DF_Receipt_Id", DF_Receipt_Id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void GetBranch(string distID)
    {

        string qry = "";
        qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId ='" + distID + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "--Select--");
        }
    }

    protected void Depositor_Gridview_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = Depositor_Gridview.SelectedRow;
        //  txtGdwnID.Text = gvr.Cells[0].Text;
        try
        {

        }
        catch (Exception)
        {

            throw;
        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch(DropDownList1.SelectedValue.ToString());
    }
    public void getdistrict()
    {
        string qry = "select District_Name,District_Id from tbl_MetaData_DISTRICT order by District_Name ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DropDownList1.DataSource = ds.Tables[0];
            DropDownList1.DataTextField = "District_Name";
            DropDownList1.DataValueField = "District_Id";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranchData();
    }
    protected void Edit(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            String hdnAcpt_FCIRO_No = (row.FindControl("hdnAcpt_FCIRO_No") as HiddenField).Value;
            GetAcceptanceData(hdnAcpt_FCIRO_No);
            popup.Show();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = GridView1.SelectedRow;
        //  txtGdwnID.Text = gvr.Cells[0].Text;
    }
    public void Delete(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chk_Delete = (CheckBox)(row.FindControl("chk_Delete"));
                HiddenField hdnAcceptance_No = (HiddenField)(row.FindControl("hdnAcceptance_No"));
                if (chk_Delete.Checked == true)
                {
                    con.Open();
                    cmd = new SqlCommand("[dbo].[Delete_Acceptence_Note_Rabi]", con, sqltrans);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Acceptance_No", hdnAcceptance_No.Value);
                    cmd.Parameters.AddWithValue("@IPAddress", Request.UserHostAddress);
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        count++;
                    }
                    con.Close();
                }
            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record deleted successfully..')", true);
                GetBranchData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record NOT deleted')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }
        finally
        {
            con.Close();
        }
    }
}