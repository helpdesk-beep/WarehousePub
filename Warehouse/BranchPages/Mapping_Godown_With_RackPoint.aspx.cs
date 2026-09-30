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

public partial class BranchPages_Mapping_Godown_With_RackPoint : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    DataSet ds = null;
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    string Branch = "";
    string Distid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGridwhr();
            GetDist();
        }
    }
    private void GetDist()
    {
        try
        {
            string strDist = "";
            strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
            SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Dist.DataSource = ds.Tables[0];
                DDL_Dist.DataTextField = "District_Name";
                DDL_Dist.DataValueField = "District_Id";
                DDL_Dist.DataBind();
                DDL_Dist.Items.Insert(0, "---Select---");
            }
            else
            {
                DDL_Dist.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }
    private void GetBranch()
    {
        try
        {
            string DistrictId = DDL_Dist.SelectedValue.ToString();
            //string qry = "SELECT [TehsilCode],[Tehsil_Name] FROM [Tehsils] where District_Code='" + DistrictId + "' order by [Tehsil_Name]";
            string str = "SELECT mbi.BranchId,mbi.DepotName FROM tbl_MetaData_DEPOT as mbi  WHERE mbi.[DistrictId] = '" + DistrictId.ToString() + "' order by mbi.DepotName";
            SqlDataAdapter da = new SqlDataAdapter(str, con_WLC);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Branch.DataSource = ds.Tables[0];
                DDL_Branch.DataTextField = "DepotName";
                DDL_Branch.DataValueField = "BranchId";
                DDL_Branch.DataBind();
                DDL_Branch.Items.Insert(0, "---Select---");
                //Session["BranchType"] = ds.Tables[0].Rows[0]["BranchTypeID"].ToString();
            }
            else
            {
                DDL_Branch.Items.Clear();
            }
            //lbl_Depot.Visible = true;
            //DDL_Depot.Visible = true;
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }
    private void fillGodnList()
    {
        if (Session["Depot_DistID"] != null)
        {
            string query = " SELECT Godown_ID,Godown_Name  FROM tbl_MetaData_GODOWN_2018 where BranchID  ='" + DDL_Branch.SelectedValue + "' and IsActive='Y' and Hired_Type='Rack Point' order by Godown_Name Asc";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlRackpoint.DataSource = ds.Tables[0];
                ddlRackpoint.DataTextField = "Godown_Name";
                ddlRackpoint.DataValueField = "Godown_ID";
                ddlRackpoint.DataBind();
                //ddlRackpoint.Items.Insert(0, "--Select--");
                ddlRackpoint.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
    }


    public void FillGridwhr()
    {
        if (Session["Depot_DepotID"].ToString() != "")
        {
            Branch = Session["BranchId"].ToString();
            Distid = Session["Depot_DistID"].ToString();
            cmd = new SqlCommand("Get_Godown_For_Rackpoint_Mapping", con);
            da = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranhcID", Session["BranchId"].ToString());

            DataSet ds = new DataSet();
            da.Fill(ds);
            Session["ds_GridInfo"] = ds;
            if (ds.Tables[0].Rows.Count > 0)
            {

                GV_Fifo.DataSource = ds;
                GV_Fifo.DataBind();

                lblRowCount.Text = "";
                lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपकी शाखा द्वारा सभी गोदामों की मैपिंग कर दी  गई है')", true);
                lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
                GV_Fifo.DataSource = null;
                GV_Fifo.DataBind();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }



    protected void btnPrint_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/BranchPages/PrintDeleteRequest.aspx");

    }

    //protected void ddl_godown_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillGridwhr();
    //}
    public void checkvalidation()
    {
        if (DDL_Branch.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Branch')", true);
            DDL_Branch.Focus();
            return;
        }
       else if (ddlRackpoint.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Rack Point')", true);
            ddlRackpoint.Focus();
            return;
        }
       
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            int count = 0;
            checkvalidation();
            foreach (GridViewRow row in GV_Fifo.Rows)
            {
                string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                CheckBox chk_Sum = (CheckBox)(row.FindControl("chk_Sum"));
                HiddenField hdncheckID = (HiddenField)(row.FindControl("hdncheckID"));
                if (chk_Sum.Checked == true)
                {
                    con.Open();
                    cmd = new SqlCommand("[dbo].[SP_mapping_for_rackpoint_godown_wise]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BranchID", DDL_Branch.SelectedValue);
                    cmd.Parameters.AddWithValue("@RackPointID", ddlRackpoint.SelectedValue);
                    cmd.Parameters.AddWithValue("@GodownID", hdncheckID.Value);
                    cmd.Parameters.AddWithValue("@IP_Address", IPAddress.ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                    if (TheResult.StartsWith("SUCCESS"))

                    {
                        count++;
                    }
                    con.Close();
                }
            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Submitted successfully..')", true);
                FillGridwhr();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record NOT Delete')", true);
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

    //protected void ddlDispatchCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillGridwhr();
    //}

    protected void DDL_Dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }

    protected void DDL_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodnList();
    }
}