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

public partial class Region_Search_do : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connect_warehouse"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    SqlTransaction sqltran;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            try
            {
                if (!IsPostBack)
                {
                    Btn_Delete.Enabled = false;
                    Session["RefreshButton"] = "No";
                    string PopMsg = "";
                    PopMsg = Request.QueryString["PopMsg"];
                    if (PopMsg != null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + PopMsg + "')", true);
                    }
                    deletenotinrecords();
                    fillDistrict();
                    Btn_Delete.Attributes.Add("onclick", "javascript:return confirm('Are you sure and  wants to delete this record , please be sure for deleting data?');");
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

    private void getDepot(string distId)
    {
        try
        {
            qry = "select depo.DepotID,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";
            cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepotList.DataSource = ds.Tables[0];
                ddlDepotList.DataTextField = "DepotName";
                ddlDepotList.DataValueField = "DepotID";
                ddlDepotList.DataBind();
                ddlDepotList.Items.Insert(0, "---Select---");
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
        qry = "SELECT [Trans_ID],tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,[Release_Order_No],[Release_Order_Date],[RO_Quantity],[FPS],[Truckno],[GatePass_No],[allotment_month],[allotment_year]  FROM [tbl_RO_Details] join tbl_MetaData_STORAGE_COMMODITY on [tbl_RO_Details].Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id where DepotId = '" + ddlDepotList.SelectedValue.ToString() + "' and Release_Order_No = '" + txtdonumber.Text.Trim().ToString() + "'";
        cmd = new SqlCommand(qry, con);
        da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            lblRowCount.Text = "";
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            Btn_Delete.Enabled = true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No DO Found...')", true);
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            gv.DataSource = null;
            gv.DataBind();
        }
    }

    private void fillDistrict()
    {
        try
        {
            qry = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            cmd = new SqlCommand(qry, con);
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

    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        int count = 0;
        try
        {
            if (gv.Rows.Count > 0)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqltran = con.BeginTransaction();
                foreach (GridViewRow gr2 in gv.Rows)
                {
                    CheckBox chk_Delete = new CheckBox();
                    string Transid = Convert.ToString(gv.DataKeys[gr2.RowIndex].Value);
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                    if (chk_Delete.Checked == true)
                    {
                        qry = "Delete from tbl_RO_Details where [Trans_ID]='" + Transid.ToString() + "' and DepotId='" + ddlDepotList.SelectedValue.ToString() + "'";
                        cmd = new SqlCommand(qry, con, sqltran);
                        int x = cmd.ExecuteNonQuery();
                        count++;           
                    }
                }
                sqltran.Commit();
                con.Close();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully deleted')", true);
                FillGrid();
            }
            else
            {
                lbl_notfound.Visible = true;
                lbl_notfound.Text = "Please Select Atleast One Record For Deleting";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast One Record For Deleting')", true);
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            lbl_notfound.Visible = true;
            lbl_notfound.Text = ex.ToString();
        }
        finally
        {
            con.Close();
        }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StatePages/StateWelcome.aspx");
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

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (txtdonumber.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please fill do number')", true);
            return;
        }
        else if(ddlDistrict.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please select district')", true);
            return;
        }
        else if (ddlDepotList.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please select Branch')", true);
        }
        else
        {
            FillGrid();
        }
    }

    private void deletenotinrecords()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        qry = "delete from [tbl_RO_Details] where GatePass_No in (select GatePass_No from tbl_Storage_GatePass_Enrty_DeleteLog)";
        cmd = new SqlCommand(qry, con, sqltran);
        int x = cmd.ExecuteNonQuery();
        con.Close();
    }
}
