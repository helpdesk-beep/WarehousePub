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

public partial class JointVentureScheme_JVSOfferReport : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlTransaction sqltran;
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = null;
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
            Response.Redirect("UserReg.aspx");
        }
    }
    public void gerreg()
    {
        try
        {
            string qry = "";
            if (rdoDist.Checked == true)
            {
                qry = "select MDDIS.District_Name,MDD.DepotName,Warehouse_Name,WAR.Registration_Id,convert(Decimal(18,2),RegCapacity) as RegCapacity,RegAmt,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,OfferAmt,CONVERT(varchar(10),COFF.CreatedDate,103) as OfferedDate from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer as COFF on WAR.Registration_Id=COFF.Registration_Id  inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and WAR.DistrictId='" + ddlDist.SelectedValue.ToString() + "'  order by District_Name,DepotName";
            }
            else if (rdoBranch.Checked == true)
            {
                qry = "select MDDIS.District_Name,MDD.DepotName,Warehouse_Name,WAR.Registration_Id,convert(Decimal(18,2),RegCapacity) as RegCapacity,RegAmt,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,OfferAmt,CONVERT(varchar(10),COFF.CreatedDate,103) as OfferedDate from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer as COFF on WAR.Registration_Id=COFF.Registration_Id  inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and WAR.BranchId='" + ddlDist.SelectedValue.ToString() + "'  order by District_Name,DepotName";
            }
            else
            {
                qry = "select MDDIS.District_Name,MDD.DepotName,Warehouse_Name,WAR.Registration_Id,convert(Decimal(18,2),RegCapacity) as RegCapacity,RegAmt,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,OfferAmt,CONVERT(varchar(10),COFF.CreatedDate,103) as OfferedDate from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer as COFF on WAR.Registration_Id=COFF.Registration_Id  inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' order by District_Name,DepotName";
            }
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["dsReg"] = ds;
                RegGrid.DataSource = ds;
                RegGrid.DataBind();
                Label2.Visible = true;
                Label3.Visible = true;
                Label4.Visible = true;
                Label5.Visible = true;
                Label3.Text = Convert.ToString(ds.Tables[0].Rows.Count);
                decimal sum = 0;
                for (int i = 0; i < RegGrid.Rows.Count; i++)
                {
                    sum += Convert.ToDecimal(RegGrid.Rows[i].Cells[7].Text.ToString());
                }
                Label5.Text = Convert.ToString(sum);
            }
            else
            {
                Label2.Visible = false;
                Label3.Visible = false;
                Label4.Visible = false;
                Label5.Visible = false;
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
    private void getbranch()
    {
        string query = "select BranchId,DepotName from tbl_MetaData_DEPOT order by DepotName";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDist.Items.Clear();
            ddlDist.DataSource = ds.Tables[0];
            ddlDist.DataTextField = "DepotName";
            ddlDist.DataValueField = "BranchId";
            ddlDist.DataBind();
            ddlDist.Items.Insert(0, "--Select--");
        }
    }

    protected void rdoDist_CheckedChanged(object sender, EventArgs e)
    {
        Label1.Text = "Select District :";
        Label1.Visible = true;
        ddlDist.Visible = true;
        RegGrid.DataSource = null;
        RegGrid.DataBind();
        Label2.Visible = false;
        Label3.Visible = false;
        Label4.Visible = false;
        Label5.Visible = false;
        getDistrict();
    }
    protected void rdoBranch_CheckedChanged(object sender, EventArgs e)
    {
        Label1.Text = "Select Branch :";
        Label1.Visible = true;
        ddlDist.Visible = true;
        RegGrid.DataSource = null;
        RegGrid.DataBind();
        Label2.Visible = false;
        Label3.Visible = false;
        Label4.Visible = false;
        Label5.Visible = false;
        getbranch();
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
        Label1.Visible = false;
        ddlDist.Visible = false;
        ddlDist.Items.Clear();
        ddlDist.DataSource = null;
        ddlDist.DataBind();
        gerreg();
    }
}
