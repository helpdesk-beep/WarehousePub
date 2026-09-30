using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;

public partial class Reports_States_Rpt_Delete_WHR : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connect_warehouse"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            //string region = Session["Region_ID"].ToString();
            if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        Session["RefreshButton"] = "No";
                        string PopMsg = "";
                        PopMsg = Request.QueryString["PopMsg"];
                        if (PopMsg != null)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + PopMsg + "')", true);
                        }
                       
                            fillDistrict();
                       
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
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["RefreshButton"] = "No";// Session["RefreshButton"];
    }

    private void getDepot(string distId)
    {
        try
        {
            string query = "select depo.BranchId,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepotList.DataSource = ds.Tables[0];
                ddlDepotList.DataTextField = "DepotName";
                ddlDepotList.DataValueField = "BranchId";
                ddlDepotList.DataBind();
                ddlDepotList.Items.Insert(0, "---Select---");
                ddlGodown.DataSource = null;
                ddlGodown.DataBind();
                gv.DataSource = null;
                gv.DataBind();
            }
            else
            {
                ddlDepotList.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception)
        {
            ///////
        }
    }

    public void FillGrid()
    {
        cmd = new SqlCommand("Get_Delete_WHR_Details", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Branch_ID",ddlDepotList.SelectedValue);
        cmd.Parameters.AddWithValue("@District_ID",ddlDistrict.SelectedValue);
        cmd.Parameters.AddWithValue("@Goodwn_ID",ddlGodown.SelectedValue);
        da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        Session["ds_GridInfo"] = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            lblRowCount.Text = "";
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No WHR Found...')", true);
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            gv.DataSource = null;
            gv.DataBind();
        }
    }

    private void fillDistrict()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {
                
                if (Session["Region_ID"].ToString() != null)
                {
                    region = Session["Region_ID"].ToString();

                }
            }
            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            }
            else
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
            }
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "---Select---");
                gv.DataSource = null;
                gv.DataBind();
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex != 0)
        {
            getDepot(ddlDistrict.SelectedValue.ToString());
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District')", true);
        } 
    }

    private void Getgodowns()
    {
        string str = "select Godown_Name,Godown_id from tbl_MetaData_GODOWN where BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and DistrictId = '" + ddlDistrict.SelectedValue.ToString() + "'  order by Godown_Name ";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGodown.DataSource = ds.Tables[0];
            ddlGodown.DataTextField = "Godown_Name";
            ddlGodown.DataValueField = "Godown_id";
            ddlGodown.DataBind();
            ddlGodown.Items.Insert(0, "--Select--");
            gv.DataSource = null;
            gv.DataBind();
        }
        else
        {
            ddlGodown.DataSource = null;
            ddlGodown.DataBind();
            ddlGodown.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District First..')", true);
        }
        else if (ddlDepotList.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Branch First..')", true);
        }
        else if (ddlGodown.SelectedIndex != 0)
        {
            FillGrid();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Godown First')", true);
        }
    }

    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlDistrict.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District First..')", true);
        }
        else if (ddlDepotList.SelectedIndex != 0)
        {
            Getgodowns();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Branch First..')", true);
        }
    }
}
