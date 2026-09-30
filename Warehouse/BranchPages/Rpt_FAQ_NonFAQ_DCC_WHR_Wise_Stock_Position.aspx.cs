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

public partial class BranchPages_WHR_Wise_Stock_Position : System.Web.UI.Page
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
        }
    }

    private void fillBranchList()
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

    private void fillCommodity()
    {
        string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in('22','3','13','129','131')";
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
    //protected void btnsave_Click(object sender, EventArgs e)
    //{
    //    //string whrdate = "";
    //    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
    //    string Reqid = "";
    //    string ComReqid = "";
    //    string Branch = Session["BranchId"].ToString();
    //    string Dist = Session["Depot_DistID"].ToString();
    //    string commid = ddlcommodity.SelectedValue.ToString();
    //    if (con.State == ConnectionState.Closed)
    //    {
    //        con.Open();
    //    }
    //    //if (rbtdellist.SelectedValue == "01")
    //    //{
    //        string ComQueryMax = "select isnull(Max(Req_id),0) from tbl_Opening_Delete_Req";
    //        cmd = new SqlCommand(ComQueryMax, con); // 
    //        string str4 = cmd.ExecuteScalar().ToString();
    //        if (Convert.ToInt64(str4) != 0)
    //        {
    //            ComReqid = Convert.ToString(Convert.ToInt64(str4) + 1);
    //        }
    //        else
    //        {
    //            ComReqid = "1000";
    //        }
    //    if (gv_whr.Rows.Count > 0)
    //    {
    //        foreach (GridViewRow gr2 in gv_whr.Rows)
    //        {
    //            CheckBox chk_Delete = new CheckBox();
    //            chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
    //            if (chk_Delete.Checked == false || chk_Delete.Enabled == false)
    //            {
    //            }
    //            else
    //            {
    //                string WHRID = Convert.ToString(gv_whr.DataKeys[gr2.RowIndex].Value);
    //                string Depositor = gr2.Cells[3].Text.ToString();
    //                string whrdate = getDate_MDY(gr2.Cells[2].Text.ToString());
    //                int Bags = Convert.ToInt32(gr2.Cells[5].Text.ToString());
    //                decimal Weigt = Convert.ToDecimal(gr2.Cells[6].Text.ToString());
    //                string QueryMax = "select isnull(Max(CONVERT (int,Req_id)),0) from tbl_Opening_Delete_Req";
    //                cmd = new SqlCommand(QueryMax, con); // 
    //                string str3 = cmd.ExecuteScalar().ToString();
    //                if (Convert.ToInt64(str3) != 0)
    //                {
    //                    Reqid = Convert.ToString(Convert.ToInt64(str3) + 1);
    //                }
    //                else
    //                {
    //                    Reqid = "1000";
    //                }
    //                if (Reqid != string.Empty)
    //                {
    //                    //string qry = "Insert Into tbl_Opening_Delete_Req (Req_id,ComReq_Id ,Districtid,Depotid,Depositor_Name,whr_id,commodity_id,Godown_id,whr_Date,No_of_Bags,Quantity,Operator_Name,Opeartor_Mob,Bm_Name,Bm_Mob,Req_date,Status,ip,DeleteStatus) values ('" + Reqid.ToString() + "','" + ComReqid + "','" + Dist + "','" + Branch + "','" + Depositor + "','" + WHRID + "','" + ddlcommodity.SelectedValue.ToString() + "','" + ddl_godown.SelectedValue.ToString() + "','" + whrdate + "','" + Bags + "','" + Weigt + "','" + txtopname.Text.Trim().ToString() + "','" + txtopmobile.Text.Trim().ToString() + "','" + txtbmname.Text.Trim().ToString() + "','" + txtbmmob.Text.Trim().ToString() + "',getdate(),'D','" + ip + "','N')";
    //                      string qry = "Insert Into tbl_Stock_Selection_For_FIFO_Delivery (ComReq_Id ,Districtid,Branch_Id,Depositor_Name,whr_id,commodity_id,Godown_id,whr_Date,No_of_Bags,Quantity,Req_date,Status,ip,FIFO_Status,Delivery_Mode) values ('" + ComReqid.ToString() + "','" + Dist + "','" + Branch + "','" + Depositor + "','" + WHRID + "','" + ddlcommodity.SelectedValue.ToString() + "','" + ddl_godown.SelectedValue.ToString() + "','" + whrdate + "','" + Bags + "','" + Weigt + "',getdate(),'Y','" + ip + "','Y','"+ ddlDispatchCategory.SelectedValue +"')";
    //                    cmd = new SqlCommand(qry, con);
    //                    int c = cmd.ExecuteNonQuery();
    //                    if (c > 0)
    //                    {
    //                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('saved Successfully...')", true);
    //                        btnPrint.Visible = true;
    //                        Session["ComReq_Id"] = ComReqid;
    //                        //FillGridwhr();
    //                    }
    //                }
    //            }
    //        }
    //        FillGridwhr();
    //    }
  
    //    if (con.State == ConnectionState.Open)
    //    {
    //        con.Close();
    //    }
    //}
  public void FillGridwhr()
    {
        if (Session["Depot_DepotID"].ToString() != "")
        {
            gv_whr.DataSource = null;
            gv_whr.DataBind();
            Branch = Session["BranchId"].ToString();
            Distid = Session["Depot_DistID"].ToString();
            
            SqlCommand cmd = new SqlCommand("Get_WHR_Wise_Stock_Position_FOR_FAQ_NONFAQ_For_Rpt", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
            cmd.Parameters.AddWithValue("@Commodityid", ddlcommodity.SelectedValue);
            if (ddl_godown.SelectedValue == "--Select--")
            {
                cmd.Parameters.AddWithValue("@GodownID", "0");
            }
            else
            {
                cmd.Parameters.AddWithValue("@GodownID", ddl_godown.SelectedValue);
            }
            cmd.Parameters.AddWithValue("@CropYear", ddlcropyear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
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


    protected void rbtdellist_SelectedIndexChanged(object sender, EventArgs e)
    {
        //trgdnlist.Visible = true;
        EmptyGrid();
        ddl_godown.SelectedIndex = 0;
    }

    private void EmptyGrid()
    {
        gv_whr.DataSource = null;
        gv_whr.DataBind();
        
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {

        //Session["Requestfor"] = rbtdellist.SelectedValue.ToString();
        Response.Redirect("~/BranchPages/PrintDeleteRequest.aspx");

    }

    protected void ddlcropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridwhr();
    }


    protected void gv_whr_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //Label lblWhr_No = gv_whr.Rows[e.RowIndex].FindControl("lblWhr_No") as Label;
        //TextBox txtFAQ_Stock = gv_whr.Rows[e.RowIndex].FindControl("txtFAQ_Stock") as TextBox;
        //TextBox txtNon_FAQ_Stock = gv_whr.Rows[e.RowIndex].FindControl("txtNon_FAQ_Stock") as TextBox;
        //TextBox txtDCC_Stock = gv_whr.Rows[e.RowIndex].FindControl("txtDCC_Stock") as TextBox;

        //    try
        //    {
        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
        //        SqlCommand cmd = new SqlCommand("SP_WHR_Wise_Stock_Position_FOR_FAQ_NONFAQ_2024", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Branch_Id", Session["BranchId"].ToString());
        //        cmd.Parameters.AddWithValue("@Godown_ID", ddl_godown.SelectedValue);
        //        cmd.Parameters.AddWithValue("@WHRNo", lblWhr_No.Text);
        //        cmd.Parameters.AddWithValue("@FAQ_Stock", txtFAQ_Stock.Text);
        //        cmd.Parameters.AddWithValue("@Non_FAQ_Stock", txtNon_FAQ_Stock.Text);
        //        cmd.Parameters.AddWithValue("@DCC_Stock", txtDCC_Stock.Text);

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.Message);
        //    }
        //FillGridwhr();
        ////ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Record Save Successfully')", true);
        //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Save Successfully ..'); </script> ");

        
    }

    protected void ddl_godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridwhr();
    }

    protected void ddlcropyear_SelectedIndexChanged1(object sender, EventArgs e)
    {
        FillGridwhr();
    }
}