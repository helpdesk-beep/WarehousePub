using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;

public partial class JointVentureScheme_JVSFinalReg_Report : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        try
        {
            if (!IsPostBack)
            {
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("JointVentureSchemeApp.aspx");
        }
    }
    public void gerreg()
    {
        try
        {
            string qry = "";
            if (rdoDist.Checked == true)
            {
                qry = "select (select BranchName from MetaDataBranchWithIssueCenter as MDIC where MDIC.BranchID=WR.BranchId) as BranchName,CONCAT('******',SUBSTRING(Registration_Id, 10, 3)) as Registration_Id,Warehouse_Name,WR.Warehouse_Address,Auth_Person,CONCAT('******',SUBSTRING(WPR.MobileNo, 8,2)) as MobileNo,CONVERT(decimal(18,2),WR.Warehouse_Capacity) as  Registered_Capacity, convert(varchar(10),WR.CreatedDate,103) as Registration_Date from tbl_Warehouse_PreReg as WPR inner join tbl_WarehouseRegistration as WR on WR.Registration_Id=WPR.Reg_No inner join (select REGISTRATIONID,FEE from tbl_Payment_Status where CategoryName='REGISTRATION FEE') as PS on PS.REGISTRATIONID=WR.Registration_Id where WR.RegAmt<=PS.FEE and WR.DistrictId='" + ddlDist.SelectedValue.ToString() + "'  order by WR.Registration_Date";
            }
            else
            {
                qry = "select (select BranchName from MetaDataBranchWithIssueCenter as MDIC where MDIC.BranchID=WR.BranchId) as BranchName,CONCAT('****',SUBSTRING(Registration_Id, 8, 3)) as Registration_Id,Warehouse_Name,WR.Warehouse_Address,Auth_Person,CONCAT('******',SUBSTRING(WPR.MobileNo, 8,2)) as MobileNo,CONVERT(decimal(18,2),WR.Warehouse_Capacity) as  Registered_Capacity, convert(varchar(10),WR.CreatedDate,103) as Registration_Date from tbl_Warehouse_PreReg as WPR inner join tbl_WarehouseRegistration as WR on WR.Registration_Id=WPR.Reg_No inner join (select REGISTRATIONID,FEE from tbl_Payment_Status where CategoryName='REGISTRATION FEE') as PS on PS.REGISTRATIONID=WR.Registration_Id where WR.RegAmt<=PS.FEE order by WR.Registration_Date";
            }
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                RegGrid.DataSource = ds;
                RegGrid.DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void getDistrict()
    {
        string query = "select District_Id,District_Name from tbl_MetaData_DISTRICT order by District_Name";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDist.Items.Clear();
            ddlDist.DataSource = ds.Tables[0];
            ddlDist.DataTextField = "District_Name";
            ddlDist.DataValueField = "District_Id";
            ddlDist.DataBind();
            ddlDist.Items.Insert(0, "--Select--");
        }
    }
    protected void rdoDist_CheckedChanged(object sender, EventArgs e)
    {
        ddlDist.Visible = true;
        RegGrid.DataSource = null;
        RegGrid.DataBind();
        getDistrict();
    }
    protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDist.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select  :...'); </script> ");
        }
        else
        {
            gerreg();
        }
    }
    protected void rdoAll_CheckedChanged(object sender, EventArgs e)
    {
        ddlDist.Visible = false;
        ddlDist.Items.Clear();
        ddlDist.DataSource = null;
        ddlDist.DataBind();
        gerreg();
    }
}
