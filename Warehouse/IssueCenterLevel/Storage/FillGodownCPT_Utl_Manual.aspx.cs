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
using System.Security.Principal;
using Microsoft.Reporting.WebForms;


public partial class IssueCenterLevel_Storage_FillGodownCPT_Utl_Manual : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Depot_DepotID"].ToString() != "")
        {
            if (!IsPostBack)
            {
                GetGodown();
                GetGdwnBranch();
                fillGodownType();
                FatchGodown();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void GetGodown()
    {
        string qry = "SELECT Godown_ID,Godown_Name  FROM tbl_MetaData_GODOWN where DistrictId ='" + Session["Depot_DistID"].ToString() + "' and  BranchID  ='" + Session["BranchId"].ToString() + "' and Remarks='Y' order by Godown_Name Asc";
        SqlCommand cmd = new SqlCommand(qry, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGdwn.DataSource = ds.Tables[0];
            ddlGdwn.DataTextField = "Godown_Name";
            ddlGdwn.DataValueField = "Godown_ID";
            ddlGdwn.DataBind();
            ddlGdwn.Items.Insert(0, "--Select--");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
        }
    }
    public void GetGdwnBranch()
    {
        string qry = "SELECT DepotName,BranchId  FROM tbl_MetaData_DEPOT where DistrictId ='" + Session["Depot_DistID"].ToString() + "'  order by DepotName Asc";
        SqlCommand cmd = new SqlCommand(qry, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranchActual.DataSource = ds.Tables[0];
            ddlBranchActual.DataTextField = "DepotName";
            ddlBranchActual.DataValueField = "BranchId";
            ddlBranchActual.DataBind();
            ddlBranchActual.Items.Insert(0, "--Select--");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
        }
    }
    private void fillGodownType()
    {
            string query = "";
            query = "SELECT  [Gid],[GodownType],[TypeValue] FROM [dbo].[GodownTypeMaster]";
            cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlHiredType.Items.Clear();
                ddlHiredType.DataSource = ds.Tables[0];
                ddlHiredType.DataTextField = "GodownType";
                ddlHiredType.DataValueField = "TypeValue";
                ddlHiredType.DataBind();
                ddlHiredType.Items.Insert(0, "--Select--");
            }
            else
            {

            }
    }
    private void fillBranchType()
    {
            string query = "select Godown_Name,Godown_ID from tbl_MetaData_GODOWN where Remarks='Y' and Godown_ID='" + ddlGdwn.SelectedValue.ToString() + "'";
            cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtGdwnID.Text = ds.Tables[0].Rows[0]["Godown_ID"].ToString().Trim();
                txtGdwnChangeName.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString().Trim();
            }
            else
            {

            }
    }
    protected void ddlGdwn_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranchType();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string clientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (txtGdwnID.Text=="")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Cannot Blank Insert Godown ID(WHMS)...'); </script> ");
        }
        else if (ddlHiredType.SelectedItem.Text=="--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Hired Type...'); </script> ");
        }
        else if (ddlstoragetype.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Storage Type...'); </script> ");
        }
        else if (txtSciCpt.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Scientific Capacity...'); </script> ");
        }
        else if (txtGdwnMaxCpt.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Max Capacity...'); </script> ");
        }
        else
        {
            if (btnSubmit.Text == "Submit")
            {
                int ch = CheckGodonw();
                if (ch == 0)
                {
                    String InsertQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Stock_Deposite_Gdwn_2018]([ReigonID],[DistirictID],[BranchID],[BranchID_Select],[GodownName],[GodownNo],[Hired_Type],[Godown_Scientific_Capacity],[Godown_Capacity],[WHMS_GodownName],[WHMS_GodownID],[CreatedBy],[CreatedDate],[Storage_Type]) VALUES('','" + Session["Depot_DistID"].ToString() + "','" + Session["BranchId"].ToString() + "','" + ddlBranchActual.SelectedValue.ToString() + "','" + txtGdwnChangeName.Text + "','" + txtGdwnNo.Text + "','" + ddlHiredType.SelectedValue.ToString() +"','" + txtSciCpt.Text + "','" + txtGdwnMaxCpt.Text + "','" + ddlGdwn.SelectedItem.Text + "','" + ddlGdwn.SelectedValue.ToString() + "','" + clientIP + "',GETDATE(),'" + ddlstoragetype.SelectedItem.Text + "') ";
                    Con.Open();
                    cmd = new SqlCommand(InsertQry, Con);
                    int a = cmd.ExecuteNonQuery();
                    Con.Close();
                    if (a == 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Save Successully...')", true);
                        FatchGodown();
                    }
                }
                else if(ch==1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('This Godown Already Add ...')", true);
                }
            }
            else if (btnSubmit.Text == "Update")
            {
                String InsertQry = "UPDATE [tbl_Stock_Deposite_Gdwn_2018]   SET [BranchID_Select] = '" + ddlBranchActual.SelectedValue.ToString() + "' ,[GodownName] = '" + txtGdwnChangeName.Text + "' ,[GodownNo] = '" + txtGdwnNo.Text + "',[Hired_Type] = '" + ddlHiredType.SelectedValue.ToString() + "' ,[Godown_Scientific_Capacity] = '" + txtSciCpt.Text + "' ,[Godown_Capacity] = '" + txtGdwnMaxCpt.Text + "',[Storage_Type] = '" + ddlstoragetype.SelectedItem.Text + "',[UpdatedBy]='" + clientIP + "',UpdatedDate=GETDATE() WHERE [WHMS_GodownID]='" + txtGdwnID.Text + "'";
                Con.Open();
                cmd = new SqlCommand(InsertQry, Con);
                int a = cmd.ExecuteNonQuery();
                Con.Close();
                if (a == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Update Successully...')", true);
                    FatchGodown();
                }
            }
        }
    }
    public int CheckGodonw()
    {
        int chk = 0;
        string strsql = "select * from tbl_Stock_Deposite_Gdwn_2018 where WHMS_GodownID ='" + ddlGdwn.SelectedValue.ToString() +"'";
        SqlCommand cmd = new SqlCommand(strsql, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            chk = 1;
        }
        else
        {
            chk = 0;
        }
        return chk;
    }
    public void FatchGodown()
    {

            string qry = "SELECT [WHMS_GodownName],[WHMS_GodownID],[Hired_Type],[Godown_Scientific_Capacity],[Godown_Capacity],[Storage_Type] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Stock_Deposite_Gdwn_2018] where BranchId='" + Session["BranchId"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(qry, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gridgdwn.DataSource = null;
                gridgdwn.DataBind();
                gridgdwn.DataSource = ds;
                gridgdwn.DataBind();
            }
            else
            {
                gridgdwn.DataSource = null;
                gridgdwn.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Previous Record Found ')", true);
            }
    }
    protected void gridgdwn_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Text = "Update";
        FatchUpdateGdwn();

    }
    private void FatchUpdateGdwn()
    {
        btnSubmit.Text = "Update";
        GridViewRow gvr = gridgdwn.SelectedRow;
        txtGdwnID.Text = gvr.Cells[2].Text;
        string query = "select BranchID_Select,GodownName,GodownNo,Hired_Type,Storage_Type,Godown_Scientific_Capacity,Godown_Capacity from tbl_Stock_Deposite_Gdwn_2018 where WHMS_GodownID = '" + txtGdwnID.Text +"'";
        cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter();
        da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGdwn.SelectedValue = txtGdwnID.Text;
            ddlGdwn.Enabled = false;
            ddlBranchActual.SelectedValue = ds.Tables[0].Rows[0]["BranchID_Select"].ToString().Trim();
            txtGdwnChangeName.Text = ds.Tables[0].Rows[0]["GodownName"].ToString().Trim();
            txtGdwnNo.Text = ds.Tables[0].Rows[0]["GodownNo"].ToString().Trim();
            ddlHiredType.SelectedValue = ds.Tables[0].Rows[0]["Hired_Type"].ToString().Trim();
            ddlstoragetype.SelectedValue = ds.Tables[0].Rows[0]["Storage_Type"].ToString().Trim();
            txtSciCpt.Text = ds.Tables[0].Rows[0]["Godown_Scientific_Capacity"].ToString().Trim();
            txtGdwnMaxCpt.Text = ds.Tables[0].Rows[0]["Godown_Capacity"].ToString().Trim();
        }
        else
        {

        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenterLevel/Storage/FillGodownCPT_Utl_Manual.aspx");
    }
}

