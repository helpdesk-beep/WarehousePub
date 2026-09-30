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

public partial class Inspections_State_State_ViewFillAnexB : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        //lbl_user.Text = Session["UserName"].ToString();
        if (!IsPostBack)
        {
            
        }
    }
    public void fillInpOff_Grid()
    {
        string strsql = "";
        if (rdoDist.Checked == true)
        {
            strsql = "select  Officer_Name,District_Name,Depotname,Godown_Name,PVGDWN.Godown_ID,PVGDWN.Hired_type,PVGDWN.Storage_Type,convert(Varchar(10),Inspection_Date,103) as InspDate,Total_Bags_AsPerOnline,Total_Bags_AsPerPV,Total_Bags_AsPerOnline-Total_Bags_AsPerPV as DiffBags from tbl_PV_Metadata_Godown as PVGDWN inner join tbl_MetaData_GODOWN_2018 as MG on MG.Godown_ID=PVGDWN.godown_ID inner join tbl_metadata_depot as MD on MD.BranchID=PVGDWN.branchID inner join tbl_metadata_district as MDDIS on MDDIS.District_ID=PVGDWN.DistrictID inner join tbl_metadata_Inspection_officer as INSP on INSP.PF_ID=Insp_Officer_ID where PVGDWN.DistrictID='" + ddl_dist.SelectedValue.ToString() + "' order by Officer_Name,District_Name,Depotname ";
        }
        else if (rdoInspOff.Checked == true)
        {
            strsql = "select Officer_Name,District_Name,Depotname,Godown_Name,PVGDWN.Godown_ID,PVGDWN.Hired_type,PVGDWN.Storage_Type,convert(Varchar(10),Inspection_Date,103) as InspDate,Total_Bags_AsPerOnline,Total_Bags_AsPerPV,Total_Bags_AsPerOnline-Total_Bags_AsPerPV as DiffBags from tbl_PV_Metadata_Godown as PVGDWN inner join tbl_MetaData_GODOWN_2018 as MG on MG.Godown_ID=PVGDWN.godown_ID inner join tbl_metadata_depot as MD on MD.BranchID=PVGDWN.branchID inner join tbl_metadata_district as MDDIS on MDDIS.District_ID=PVGDWN.DistrictID inner join tbl_metadata_Inspection_officer as INSP on INSP.PF_ID=Insp_Officer_ID where INSP.PF_ID='" + ddl_dist.SelectedValue.ToString() + "' order by Officer_Name,District_Name,Depotname ";
        }
        SqlCommand cmd = new SqlCommand(strsql, conStr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvCustomers.DataSource = ds;
            gvCustomers.DataBind();
        }
        else
        {
            gvCustomers.DataSource = null;
            gvCustomers.DataBind();
        }
    }

    public void fillInpOff_Grid(string Godown_ID)
    {
        string strsql = "select godownid,stackID,StackCommodity,Avl_Bags,Avl_Bags_AsPer_PV,Diff_Bags,Type_Of_DiffBags,Stack_Classification,Stack_PVType,Remarks from tbl_stackwiseBal_Annex_B where godownid='" + Godown_ID + "' order by StackCommodity";
        SqlCommand cmd = new SqlCommand(strsql, conStr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("StackName");
            con_WLC.Open();
            for (int intCount = 0; intCount < ds.Tables[0].Rows.Count; intCount++)
            {
                string id = ds.Tables[0].Rows[intCount]["stackID"].ToString();
                {
                    SqlCommand cmdd = new SqlCommand("select Stack_Name  FROM tbl_MetaData_STACK where stack_ID='" + id + "'", con_WLC);
                    string comm = (string)cmdd.ExecuteScalar().ToString();
                    ds.Tables[0].Rows[intCount]["StackName"] = comm;
                }
            }
            con_WLC.Close();

            pnlofferpopup.Visible = true;
            ModalPopupExtender1.Show();

            gvOrders.DataSource = ds;
            gvOrders.DataBind();

            DataTable dt = ds.Tables[0];
            decimal total = dt.AsEnumerable().Sum(row => row.Field<Int32>("Avl_Bags"));
            gvOrders.FooterRow.Cells[1].Text = "Total";
            gvOrders.FooterRow.Cells[2].Text = total.ToString("N2");

            decimal total1 = dt.AsEnumerable().Sum(row => row.Field<Int32>("Avl_Bags_AsPer_PV"));
            gvOrders.FooterRow.Cells[3].Text = total1.ToString("N2");
        }
        else
        {
            gvOrders.DataSource = null;
            gvOrders.DataBind();
        }
    }
    protected void gvCustomers_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = gvCustomers.SelectedRow;
        fillInpOff_Grid(gvr.Cells[0].Text);
    }

    protected void rdoDist_CheckedChanged(object sender, EventArgs e)
    {
        string strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_dist.DataSource = "";
            ddl_dist.DataSource = ds.Tables[0];
            ddl_dist.DataTextField = "District_Name";
            ddl_dist.DataValueField = "District_Id";
            ddl_dist.DataBind();
            ddl_dist.Items.Insert(0, "--Select--");
        }
        else
        {
            ddl_dist.DataSource = "";
            ddl_dist.DataBind();
        }
    }
    protected void rdoInspOff_CheckedChanged(object sender, EventArgs e)
    {
        string strDist = "select Officer_Name,PF_ID from tbl_metadata_Inspection_officer order by Officer_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, conStr);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_dist.DataSource = "";
            ddl_dist.DataSource = ds.Tables[0];
            ddl_dist.DataTextField = "Officer_Name";
            ddl_dist.DataValueField = "PF_ID";
            ddl_dist.DataBind();
            ddl_dist.Items.Insert(0, "--Select--");
        }
        else
        {
            ddl_dist.DataSource = "";
            ddl_dist.DataBind();
        }
    }
    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoDist.Checked == true)
        {
            fillInpOff_Grid();
        }
        else if (rdoInspOff.Checked == true)
        {
            fillInpOff_Grid();
        }
    }
}