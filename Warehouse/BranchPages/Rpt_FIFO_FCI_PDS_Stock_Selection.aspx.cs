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

public partial class BranchPages_Rpt_FIFO_FCI_PDS_Stock_Selection : System.Web.UI.Page
{
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
            fillGodnList();
        }
    }

    private void fillGodnList()
    {
        if (Session["Depot_DistID"] != null)
        {
            string query = " SELECT Godown_ID,Godown_Name  FROM tbl_MetaData_GODOWN_2018 where DistrictId ='" + Session["Depot_DistID"].ToString() + "' and  BranchID  ='" + Session["BranchId"].ToString() + "' and IsActive='Y' order by Godown_Name Asc";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_godown.DataSource = ds.Tables[0];
                ddl_godown.DataTextField = "Godown_Name";
                ddl_godown.DataValueField = "Godown_ID";
                ddl_godown.DataBind();
                ddl_godown.Items.Insert(0, "--Select--");
            }
        }
    }


    public void FillGridwhr()
    {
        if (Session["Depot_DepotID"].ToString() != "")
        {
            Branch = Session["BranchId"].ToString();
            Distid = Session["Depot_DistID"].ToString();
            cmd = new SqlCommand("Get_FIFO_Selection_Data", con);
            da = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Godown_ID", ddl_godown.SelectedValue);
            if (ddlDispatchCategory.SelectedValue == "--Select--")
            {
                cmd.Parameters.AddWithValue("@Delivery_Mode", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Delivery_Mode", ddlDispatchCategory.SelectedValue);
            }
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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No WHR Found...')", true);
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

    protected void ddl_godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridwhr();
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (GridViewRow row in GV_Fifo.Rows)
            {
                string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                CheckBox chk_Sum = (CheckBox)(row.FindControl("chk_Sum"));
                HiddenField hdncheckID = (HiddenField)(row.FindControl("hdncheckID"));
                if (chk_Sum.Checked == true)
                {
                    con.Open();
                    cmd = new SqlCommand("[dbo].[Delete_Stock_Selection_For_FIFO_Delivery]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@WHRID", hdncheckID.Value);
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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Delete successfully..')", true);
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

    protected void ddlDispatchCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridwhr();
    }
}