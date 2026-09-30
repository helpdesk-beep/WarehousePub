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

public partial class BranchPages_WHRPrintReset : System.Web.UI.Page
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
            fillCommodity();
            fillGodnList();
            FillBranchDetail();
        }
    }

    private void fillGodnList()
    {
        if (Session["Depot_DistID"] != null)
        {
            string query = " SELECT Godown_ID,Godown_Name  FROM tbl_MetaData_GODOWN where DistrictId ='" + Session["Depot_DistID"].ToString() + "' and  BranchID  ='" + Session["BranchId"].ToString() + "' order by Godown_Name Asc";
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

    private void fillCommodity()
    {
        string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name Asc";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.Items.Clear();
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, "--Select--");
            ddlcommodity.SelectedValue = "22";
        }
    }
    private void FillBranchDetail()
    {
        //string query = "select NodalOfficeName,NodalOfficerphone from tbl_MetaData_DEPOT where BranchId='" + Session["BranchId"].ToString() + "'";
        //SqlCommand cmd = new SqlCommand(query, con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    txtbmname.Text= ds.Tables[0].Rows[0]["NodalOfficeName"].ToString();
        //    txtbmmob.Text = ds.Tables[0].Rows[0]["NodalOfficerphone"].ToString();
        //}

        string query = "select NodalOfficeName,NodalOfficerphone,OperatorName,OperatorMobileNo from tbl_MetaData_DEPOT where BranchId='" + Session["BranchId"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtbmname.Text = ds.Tables[0].Rows[0]["NodalOfficeName"].ToString();
            txtbmmob.Text = ds.Tables[0].Rows[0]["NodalOfficerphone"].ToString();
            txtopname.Text = ds.Tables[0].Rows[0]["OperatorName"].ToString();
            txtopmobile.Text = ds.Tables[0].Rows[0]["OperatorMobileNo"].ToString();
        }
    }

    public void FillGridwhr()
    {
        if (Session["Depot_DepotID"].ToString() != "")
        {
            Branch = Session["BranchId"].ToString();
            Distid = Session["Depot_DistID"].ToString();
            string query = "select DISTINCT WHR.Depositor_WHR_Id,CONVERT(varchar(10),WHR.WHR_Issue_Date,103) AS whrdate,WHR.Depositor_Name,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,WHR.TotalBags_Received AS Bags,convert(decimal(18,2),WHR.Total_Qty_Received) AS Qty,tbl_MetaData_GODOWN.Godown_Name from tbl_storage_Depositor_WHR_Relation AS WHR join tbl_Storage_Receipt_Details on WHR.Depositor_WHR_Id = tbl_Storage_Receipt_Details.WHR_Id join tbl_storage_Stacking_Details on WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId JOIN tbl_MetaData_STORAGE_COMMODITY on WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id JOIN tbl_MetaData_GODOWN on tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID where WHR.BranchID = '" + Branch + "' and WHR.District_Id = '" + Distid + "' and tbl_MetaData_GODOWN.Godown_ID = '" + ddl_godown.SelectedValue.ToString() + "' AND tbl_Storage_Receipt_Details.WHR_Flag = 'Y' AND WHR.Commodity_Id ='" + ddlcommodity.SelectedValue.ToString() + "' order by WHR.Depositor_WHR_Id asc";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Session["ds_GridInfo"] = ds;
            if (ds.Tables[0].Rows.Count > 0)
            {
               
                gv_whr.DataSource = ds;
                gv_whr.DataBind();
                lblRowCount.Text = "";
                lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No WHR Found...')", true);
                lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
                gv_whr.DataSource = null;
                gv_whr.DataBind();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected string getDate_MDY(string inDate)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            //string whrdate = "";
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string Reqid = "";
            string ComReqid = "";
            string Branch = Session["BranchId"].ToString();
            string Dist = Session["Depot_DistID"].ToString();
            //if (con.State == ConnectionState.Closed)
            //{
            //    con.Open();
            //}
            string ComQueryMax = "select isnull(Max(ComReq_Id),0) from tbl_PrintReset_Req";
            cmd = new SqlCommand(ComQueryMax, con); // 
            con.Open();
            string str4 = cmd.ExecuteScalar().ToString();
            con.Close();
            if (Convert.ToInt64(str4) != 0)
            {
                ComReqid = Convert.ToString(Convert.ToInt64(str4) + 1);
            }
            else
            {
                ComReqid = "1000";
            }
            if (gv_whr.Rows.Count > 0)
            {
                foreach (GridViewRow gr2 in gv_whr.Rows)
                {
                    CheckBox chk_Delete = new CheckBox();
                    chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                    if (chk_Delete.Checked == false || chk_Delete.Enabled == false)
                    {
                    }
                    else
                    {
                        string WHRID = Convert.ToString(gv_whr.DataKeys[gr2.RowIndex].Value);
                        string Depositor = gr2.Cells[3].Text.ToString();
                        string whrdate = getDate_MDY(gr2.Cells[2].Text.ToString());
                        int Bags = Convert.ToInt32(gr2.Cells[5].Text.ToString());
                        decimal Weigt = Convert.ToDecimal(gr2.Cells[6].Text.ToString());
                        string QueryMax = "select isnull(Max(Req_id),0) from tbl_PrintReset_Req";
                        cmd = new SqlCommand(QueryMax, con); // 
                        con.Open();
                        string str3 = cmd.ExecuteScalar().ToString();
                        con.Close();
                        if (Convert.ToInt64(str3) != 0)
                        {
                            Reqid = Convert.ToString(Convert.ToInt64(str3) + 1);
                        }
                        else
                        {
                            Reqid = "100";
                        }
                        if (Reqid != string.Empty)
                        {
                            string qry = "Insert Into tbl_PrintReset_Req (Req_id,ComReq_Id ,Districtid,Depotid,Depositor_Name,whr_id,commodity_id,Godown_id,whr_Date,No_of_Bags,Quantity,Operator_Name,Opeartor_Mob,Bm_Name,Bm_Mob,Req_date,ip,ResetStatus) values ('" + Reqid.ToString() + "','" + ComReqid + "','" + Dist + "','" + Branch + "','" + Depositor + "','" + WHRID + "','" + ddlcommodity.SelectedValue.ToString() + "','" + ddl_godown.SelectedValue.ToString() + "','" + whrdate + "','" + Bags + "','" + Weigt + "','" + txtopname.Text.Trim().ToString() + "','" + txtopmobile.Text.Trim().ToString() + "','" + txtbmname.Text.Trim().ToString() + "','" + txtbmmob.Text.Trim().ToString() + "',getdate(),'" + ip + "','N')";
                            cmd = new SqlCommand(qry, con);
                            con.Open();
                            int c = cmd.ExecuteNonQuery();
                            con.Close();
                            if (c > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Request saved Successfully...')", true);
                                btnPrint.Visible = true;
                                Session["ComReq_Id"] = ComReqid;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
        finally
        {
            con.Close();
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "WHRPrint";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
      
    }
    protected void ddl_godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridwhr();
    }
    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
