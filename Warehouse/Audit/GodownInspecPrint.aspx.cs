using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspection_GodownInspecPrint: System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string qry = "";
    string branchname = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["login"] != null && Session["UserID"].ToString() != null && Session["Auid"].ToString() != null && Session["GodownId"]!="")
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["RPrint"].ToString() != null)
                    {
                        if (Session["RPrint"].ToString() == "R")
                        {

                            GodownList();
                            trDD.Visible = true;
                            tr1.Visible = false;
                            tr2.Visible = false;
                            tr3.Visible = false;
                            tr4.Visible = false;
                        }
                    }
                    else
                    {
                        getGodowndata();
                    }
                }
                catch (Exception ex)
                {
                    getGodowndata();
                }
               
            }
        }
        else
        {
            Response.Redirect("GodownInspecPrint.aspx");

        }
      
    }
    protected void GodownList()
    {
        string query = "SELECT  [Godown_ID],[Godown_Name] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where BranchID='" + Session["UserID"].ToString() + "' and Remarks='Y'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {

            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_id";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");



        }

    }
    private void getGodowndata()
    {
        try
        {
            //      string query = "SELECT  [AuditID],[EmpName],[EmpPost] FROM [Intergrated_MP_STORAGE].[dbo].[BranchAuditEmp] where AuditID='" + Session["Auid"].ToString() + "'";
            //string query = "SELECT [AuId],[BranchId],[StackID],(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.Godown_ID in (select MAX(tbl_MetaData_STACK.Godown_ID) from dbo.tbl_MetaData_STACK where Stack_ID=[BranchAuditGodownDtl].StackID)) as godown,(select Stack_Name from dbo.tbl_MetaData_STACK where Stack_ID=[BranchAuditGodownDtl].StackID) as stack,[DeposioterName],[Commid],[BichanLambai],[BichanChodai],[Atirict],(isnull([BichanLambai],0)+isnull([BichanChodai],0)+isnull([Atirict],0)) as yog,((isnull([BichanLambai],0)+isnull([BichanChodai],0)+isnull([Atirict],0))*isnull([LayerKiUchai],0)*isnull([Block],0)) as boriyonkisankhaya,[LayerKiUchai],[Block],[atririktboriupper],[atiriktborineeche],((isnull([BichanLambai],0)+isnull([BichanChodai],0)+isnull([Atirict],0))*isnull([LayerKiUchai],0)*isnull([Block],0))+ISNULL([atririktboriupper],0)+ISNULL([atiriktborineeche],0) as kulbore FROM [Intergrated_MP_STORAGE].[dbo].[BranchAuditGodownDtl] where AuId='" + Session["Auid"].ToString() + "'";
            string query = "SELECT [AuId],[BranchId],[StackID],(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.Godown_ID in (select MAX(tbl_MetaData_STACK.Godown_ID) from dbo.tbl_MetaData_STACK where Stack_ID=[BranchAuditGodownDtl].StackID)) as godown,(select Stack_Name from dbo.tbl_MetaData_STACK where Stack_ID=[BranchAuditGodownDtl].StackID) as stack,[DeposioterName],[Commid],[BichanLambai],[BichanChodai],[Atirict],(isnull([BichanLambai],0)+isnull([BichanChodai],0)+isnull([Atirict],0)) as yog,((isnull([BichanLambai],0)+isnull([BichanChodai],0)+isnull([Atirict],0))*isnull([LayerKiUchai],0)*isnull([Block],0)) as boriyonkisankhaya,[LayerKiUchai],[Block],[atririktboriupper],[atiriktborineeche],((isnull([BichanLambai],0)+isnull([BichanChodai],0)+isnull([Atirict],0))*isnull([LayerKiUchai],0)*isnull([Block],0))+ISNULL([atririktboriupper],0)+ISNULL([atiriktborineeche],0) as kulbore,convert(varchar(10),CreatedDate,103) as CreatedDate FROM [Intergrated_MP_STORAGE].[dbo].[BranchAuditGodownDtl] where AuId='" + Session["Auid"].ToString() + "' and GodownId='" + Session["GodownId"] + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                godowndtl.Visible = true;
                lblbranch44.Text = branchname;
                lblgodown.Text = ds.Tables[0].Rows[0]["godown"].ToString();
                ListView1.DataSource = ds.Tables[0];
                ListView1.DataBind();

                lbldategdn.Text = ds.Tables[0].Rows[0]["CreatedDate"].ToString();

            }
            else
            {
                godowndtl.Visible = false;
            }

        }

        catch (Exception ex)
        {

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("InspectionLogin.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("GodownInspection.aspx");
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
            string query = "SELECT [AuId],[BranchId],[StackID],(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.Godown_ID in (select MAX(tbl_MetaData_STACK.Godown_ID) from dbo.tbl_MetaData_STACK where Stack_ID=[BranchAuditGodownDtl].StackID)) as godown,(select Stack_Name from dbo.tbl_MetaData_STACK where Stack_ID=[BranchAuditGodownDtl].StackID) as stack,[DeposioterName],[Commid],[BichanLambai],[BichanChodai],[Atirict],(isnull([BichanLambai],0)+isnull([BichanChodai],0)+isnull([Atirict],0)) as yog,((isnull([BichanLambai],0)+isnull([BichanChodai],0)+isnull([Atirict],0))*isnull([LayerKiUchai],0)*isnull([Block],0)) as boriyonkisankhaya,[LayerKiUchai],[Block],[atririktboriupper],[atiriktborineeche],((isnull([BichanLambai],0)+isnull([BichanChodai],0)+isnull([Atirict],0))*isnull([LayerKiUchai],0)*isnull([Block],0))+ISNULL([atririktboriupper],0)+ISNULL([atiriktborineeche],0) as kulbore,convert(varchar(10),CreatedDate,103) as CreatedDate FROM [Intergrated_MP_STORAGE].[dbo].[BranchAuditGodownDtl] where AuId='" + Session["Auid"].ToString() + "' and GodownId='" + ddlgodown.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                godowndtl.Visible = true;
                lblbranch44.Text = branchname;
                lblgodown.Text = ds.Tables[0].Rows[0]["godown"].ToString();
                ListView1.DataSource = ds.Tables[0];
                ListView1.DataBind();

                lbldategdn.Text = ds.Tables[0].Rows[0]["CreatedDate"].ToString();

                tr1.Visible = true;
                tr2.Visible = true;
                tr3.Visible = true;
                tr4.Visible = true;
            }
            else
            {
                godowndtl.Visible = false;
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Not Found'); </script> ");
            }
        } 
    
}
