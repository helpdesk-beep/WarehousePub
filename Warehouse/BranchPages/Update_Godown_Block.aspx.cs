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

public partial class BranchPages_Update_Godown_Block : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    DataSet ds = null;
    SqlDataAdapter da = null;

    protected void Page_Load(object sender, EventArgs e)
    {
         if ((Session["BranchID"] != null))
        {
        if (!IsPostBack)
        {
            GetGodown();
            GetBlock();
        }
        }
         else
         {
             Response.Redirect("~/SessionExpired.htm");
         }
    }
    public void GetGodown()
    {
        string Dist_id = Session["Depot_DistID"].ToString();
        string sid = Session["BranchID"].ToString();
        ddlgodown.Items.Clear();
        //qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type in ('Owned')";
        qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where Godown_ID not in (select Godown_ID from tbl_Godown_Asset_Map) and DistrictId='" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlgodown.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");
        }
    }
    public void GetBlock()
    {
        string Dist_id = Session["Depot_DistID"].ToString();
        string sid = Session["BranchID"].ToString();
        ddlBlock.Items.Clear();
        //qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type in ('Owned')";
        qry = " select b_nm as Block_Name,b_cd as Block_Code from tbl_Block_Master_Map where District_Id='" + Dist_id + "'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlBlock.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlBlock.DataSource = ds.Tables[0];
            ddlBlock.DataTextField = "Block_Name";
            ddlBlock.DataValueField = "Block_Code";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, "--Select--");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlgodown.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg1", "<script language=javascript>alert('Please Select Godown');</script>");
        }
        else if (ddlBlock.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg1", "<script language=javascript>alert('Please Select Block');</script>");
        }
        else
        {
            string Dist_id = Session["Depot_DistID"].ToString();
            string sid = Session["BranchID"].ToString();
            qry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Asset_Map] ([Branch_ID],[Godown_ID],[Godown_Name] ,[b_nm],[b_cd],[District_Id]) VALUES ('" + sid + "','" + ddlgodown.SelectedValue + "','" + ddlgodown.SelectedItem.Text + "' ,'" + ddlBlock.SelectedItem.Text + "','" + ddlBlock.SelectedValue + "','" + Dist_id + "')";
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            GetGodown();
        }
    }
}
