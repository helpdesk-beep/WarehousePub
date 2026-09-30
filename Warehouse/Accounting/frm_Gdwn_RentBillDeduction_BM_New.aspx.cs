using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Resources;
using System.Text;

public partial class Accounting_frm_Gdwn_RentBillDeduction_BM_New : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    SqlTransaction sqltran;
    DataSet ds = null;
    DataSet ds2 = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                //Session["Bill_No"] = "23280010482006123";
                GetGodown();
                SetInitialRow();
                FillBillDetails(Session["Bill_No"].ToString());
                FillStorageBillDetails();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Tid", typeof(string)));
     //   dt.Columns.Add(new DataColumn("SocietyName", typeof(string)));
    //    dt.Columns.Add(new DataColumn("ProcDist", typeof(string)));
        dt.Columns.Add(new DataColumn("RDetuction", typeof(string)));
        dt.Columns.Add(new DataColumn("K_Vivran", typeof(string)));
        dt.Columns.Add(new DataColumn("K_Rashi", typeof(string)));
        dt.Columns.Add(new DataColumn("K_Remark", typeof(string)));
        dr = dt.NewRow();
        dr["Tid"] = 1;
    //    dr["SocietyName"] = string.Empty;
    //    dr["ProcDist"] = string.Empty;
        dr["RDetuction"] = 0;
        dr["K_Vivran"] = string.Empty;
        dr["K_Rashi"] = string.Empty;
        dr["K_Remark"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        gvGodown.DataSource = dt;
        gvGodown.DataBind();
    }
    protected void gvGodown_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    con.Open();
        //    var ddl = (DropDownList)e.Row.FindControl("ddlD_Vivran");
        //    int DistrictId = Convert.ToInt32(e.Row.Cells[0].Text);
        //    SqlCommand cmd = new SqlCommand("select TID,Res_Det_Vivran from tbl_metadata_RecDetuction", con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    con.Close();
        //    ddl.DataSource = ds;
        //    ddl.DataTextField = "Res_Det_Vivran";
        //    ddl.DataValueField = "TID";
        //    ddl.DataBind();
        //    ddl.Items.Insert(0, new ListItem("--Select--", "0"));
        //}

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //con.Open();
            //var ddl = (DropDownList)e.Row.FindControl("ddltxtProcName");
            //int DistrictId = Convert.ToInt32(e.Row.Cells[0].Text);
            //SqlCommand cmd = new SqlCommand("select TID,Res_Det_Vivran from tbl_metadata_RecDetuction", con);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //con.Close();
            //ddl.DataSource = ds;
            //ddl.DataTextField = "Res_Det_Vivran";
            //ddl.DataValueField = "TID";
            //ddl.DataBind();
            //ddl.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rowIndex++;
                }
            }
        }
    }

    private void AddNewRowToGrid()
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["Tid"] = dtCurrentTable.Rows.Count + 1;
                // drCurrentRow["SocietyName"] = "";
               // drCurrentRow["ProcDist"] = "";
                drCurrentRow["RDetuction"] = 0;
                drCurrentRow["K_Vivran"] = "";
                drCurrentRow["K_Rashi"] = "";
                drCurrentRow["K_Remark"] = "";

                //add new row to DataTable
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState
                ViewState["CurrentTable"] = dtCurrentTable;

                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    //if (((CheckBox)gvGodown.Rows[i].FindControl("ckstack")).Checked == true)
                    //{
                    //extract the DropDownList Selected Items
                    //DropDownList ddl1 = (DropDownList)gvImprest.Rows[i].Cells[1].FindControl("DropDownList1");
          //          DropDownList ProcName = (DropDownList)gvGodown.Rows[i].Cells[1].FindControl("ddltxtProcName");
          //          TextBox ProcDist = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtProcDistance");
                    DropDownList RDetuction = (DropDownList)gvGodown.Rows[i].Cells[1].FindControl("ddl_RDetuction");
                    TextBox K_Vivran = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtK_Vivran");
                    TextBox K_Rashi = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtK_Rashi");
                    TextBox K_Remark = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtK_Remark");

                    //dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                    //dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                    //dtCurrentTable.Rows[i]["Proc_Name"] = Convert.ToString(ProcName.Text);

            //        dtCurrentTable.Rows[i]["SocietyName"] = Convert.ToString(ProcName.SelectedValue.ToString());
            //        dtCurrentTable.Rows[i]["ProcDist"] = Convert.ToString(ProcDist.Text);
                    dtCurrentTable.Rows[i]["RDetuction"] = Convert.ToString(RDetuction.SelectedValue.ToString());
                    dtCurrentTable.Rows[i]["K_Vivran"] = Convert.ToString(K_Vivran.Text);
                    dtCurrentTable.Rows[i]["K_Rashi"] = Convert.ToString(K_Rashi.Text);
                    dtCurrentTable.Rows[i]["K_Remark"] = Convert.ToString(K_Remark.Text);
                }
                //Rebind the Grid with the current data
                gvGodown.DataSource = dtCurrentTable;
                gvGodown.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData();
        
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }


    public void FillStorageBillDetails()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Get_Storage_Bill_Details_by_Bill_Number", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Godown_Id", Session["godownid"].ToString());
            cmd.Parameters.AddWithValue("@Commodity_Id", Session["commodityid"].ToString());
            cmd.Parameters.AddWithValue("@Financial_Year", Session["Financial_Year"].ToString());
            cmd.Parameters.AddWithValue("@Month", Session["Month"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                lblActualBillNo.Text = dt.Rows[0]["Bill_Number"].ToString();
                txtActAmt.Text = dt.Rows[0]["Net_Amount"].ToString();               
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
        finally
        { if (con.State == ConnectionState.Open) { con.Close(); } }

    }

    public void FillBillDetails(string billnumber)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Get_Rent_Bill_Details_by_Bill_Number", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Bill_Number", billnumber.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                GetGodown();
                ddlgodown.SelectedValue = dt.Rows[0]["Godown_Id"].ToString();
                GetCommodity();
                ddlcomodity.SelectedValue = dt.Rows[0]["Commodity_Id"].ToString();
                lblbillno.Text = dt.Rows[0]["Bill_Number"].ToString();
                lblMonth.Text = dt.Rows[0]["Month"].ToString();
                lblRPM.Text = dt.Rows[0]["Commodity_Rate"].ToString();
                txtBilAmt.Text = dt.Rows[0]["Net_Amount"].ToString();
                Session["godownid"] = dt.Rows[0]["Godown_Id"].ToString();
                Session["commodityid"] = dt.Rows[0]["Commodity_Id"].ToString();
                Session["Financial_Year"] = dt.Rows[0]["Financial_Year"].ToString();
                Session["Month"] = dt.Rows[0]["Month"].ToString();
                Session["Crop_Year"] = dt.Rows[0]["Crop_Year"].ToString();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
        finally
        { if (con.State == ConnectionState.Open) { con.Close(); } }

    }

    public void GetGodown()
    {
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string sid = Session["BranchID"].ToString();
        ddlgodown.Items.Clear();
        qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlgodown.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");

            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");

        }
    }
    void GetCommodity()
    {
        qry = "select distinct Commodity_Id,Commodity_Name from View_WHRcurrentstock where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Depositor_Name='MPSCSC'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlcomodity.DataSource = ds.Tables[0];
            ddlcomodity.DataTextField = "Commodity_Name";
            ddlcomodity.DataValueField = "Commodity_ID";
            ddlcomodity.DataBind();
            ddlcomodity.Items.Insert(0, "--Select--");
        }
    }
   
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCommodity();
        FillStorageBillDetails();
    }
    protected void ddlcomodity_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void ddlBill_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        //string sid = Session["BranchID"].ToString();
        //string str = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+Godown_Id+')' as Godown,Depositor_Category from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + ddlcomodity.SelectedValue + "' and Branch_Id='" + sid + "' and Bill_Number='" + ddlBill.SelectedItem.Text + "'";
        //da = new SqlDataAdapter(str, con);
        //ds = new DataSet();
        //da.Fill(ds);
        //if (ds == null)
        //{
        //}
        //else
        //{
        //    lblMonth.Text = ds.Tables[0].Rows[0]["Bill_Month"].ToString();
        //    Session["Monthid"]= ds.Tables[0].Rows[0]["Bill_Month"].ToString();
        //    lblRPM.Text = ds.Tables[0].Rows[0]["Rate_PM"].ToString();
        //    txtBilAmt.Text = ds.Tables[0].Rows[0]["Sub_Amount"].ToString();
        //}
    }
    protected void btnSumbmitRent_Click(object sender, EventArgs e)
    {
        Insert_Godown_Rent_Deduction();
    }
    public void Insert_Godown_Rent_Deduction()
    {
        int a = 0;
        try
        {
            if (ddlgodown.SelectedItem.Text.Trim() == "--Select--")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown...')", true);
            }
            else if (ddlcomodity.SelectedItem.Text.Trim() == "--Select--")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Commodity...')", true);
            }
            //else if (ddlBill.SelectedItem.Text.Trim() == "--Select--")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Bill No...')", true);
            //}
            //else if (lblActualBillNo.Text.Trim() == "--Select--")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Actual Bill No...')", true);
            //}
            else if (txtActAmt.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Bill Actual Amount Not Null ...')", true);
            }
            else if (txtBilAmt.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('JVS Bill Amount Not Null...')", true);
            }
            else if (lblRPM.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Rate Per Month Not be null ...')", true);
            }
            else if (lblMonth.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Month of Bill Not null...')", true);
            }
            else if (gvGodown.Rows.Count == 0 && gvGodown.Rows.Count == null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter at least any one record of Detuction ...')", true);
            }
            else if (txt_GrandTotal.Text != "" && gvGodown.Rows.Count > 0)
            {
                string DistrictId = Session["Depot_DistID"].ToString();
                string BranchId = Session["BranchId"].ToString();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqltran = con.BeginTransaction();

                string qry3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Amount]([Bill_No],[District_Id],[Branch_Id],[Godown_Id],TResources_Deduct_Amt,[TBill_Deduct_Amt],TBill_Amount,CreatedBy,CreatedDate,Ref_Bill_No) VALUES ('" + lblbillno.Text.ToString() + "','" + DistrictId + "','" + BranchId + "','" + ddlgodown.SelectedValue + "','" + txt_GrandTotal.Text.Trim() + "','" + txt_GrandTotal.Text.Trim() + "','" + txtBilAmt.Text + "','" + ip + "',GETDATE(),'" + lblActualBillNo.Text.ToString() + "')";
                SqlCommand cmd3 = new SqlCommand(qry3, con, sqltran);
                int c3 = cmd3.ExecuteNonQuery();
                if (c3 > 0)
                {
                    for (int i = 0; i < gvGodown.Rows.Count; i++)
                    {
                        string S_R_Vivran = ((DropDownList)gvGodown.Rows[i].FindControl("ddl_RDetuction")).SelectedValue.ToString();
                        string S_R_Rashi = ((TextBox)gvGodown.Rows[i].FindControl("txtK_Rashi")).Text;
                        if (S_R_Vivran != "0" && S_R_Rashi != "")
                        {
                            string qry2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Vivran]([Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Res_ID],[Res_Det_Karan],[Deduction_Amt],[Res_Det_Remark],CreatedBy,CreatedDate) VALUES ('" + lblbillno.Text.ToString() + "','" + DistrictId + "','" + BranchId + "','" + ddlgodown.SelectedValue + "','" + S_R_Vivran + "',N'" + ((TextBox)gvGodown.Rows[i].FindControl("txtK_Vivran")).Text.Trim() + "','" + S_R_Rashi + "',N'" + ((TextBox)gvGodown.Rows[i].FindControl("txtK_Remark")).Text.Trim() + "','" + ip + "',GETDATE())";
                            SqlCommand cmd2 = new SqlCommand(qry2, con, sqltran);
                            int c2 = cmd2.ExecuteNonQuery();
                            if (c2 > 0)
                            {
                                a = a + 1;
                            }
                        }
                    }
                }
                if (a == gvGodown.Rows.Count)
                {
                    sqltran.Commit();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Successfully Save Record'); window.location =('frm_Gdwn_RentBillDeduction_BM_New.aspx');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Wrong...')", true);
                }
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Something Wrong Please try Again'); window.location =('frm_Gdwn_RentBillDeduction_BM_New.aspx');", true);
        }
        finally
        {
            sqltran.Dispose();
            con.Close();
        }

    }
    protected void gvGodown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        decimal sum = 0;

        for (int i = 0; i < gvGodown.Rows.Count; i++)
        {
            string S_R_Vivran = ((DropDownList)gvGodown.Rows[i].FindControl("ddl_RDetuction")).SelectedValue.ToString();
            string S_R_Rashi = ((TextBox)gvGodown.Rows[i].FindControl("txtK_Rashi")).Text;
            if (S_R_Vivran != "0" && S_R_Rashi != "")
            {
                sum = sum + Convert.ToDecimal(S_R_Rashi);
            }
            else
            {

            }
        }
        if (gvGodown.Rows.Count > 0)
        {
            txt_GrandTotal.Text = Convert.ToString(Math.Round( sum));
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('पहले कतोत्र की जानकारी भरे..'); </script> ");
        }
    }
    //protected void ddlActualBillNo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
    //    string sid = Session["BranchID"].ToString();
    //    string str = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+Godown_Id+')' as Godown,Depositor_Category from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + ddlcomodity.SelectedValue + "' and Branch_Id='" + sid + "' and Bill_Number='" + ddlActualBillNo.SelectedValue.ToString() + "'";
    //    da = new SqlDataAdapter(str, con);
    //    ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds == null)
    //    {

    //    }
    //    else
    //    {
    //        if (lblMonth.Text.Trim() == ds.Tables[0].Rows[0]["Bill_Month"].ToString())
    //        {
    //            txtActAmt.Text = ds.Tables[0].Rows[0]["Sub_Amount"].ToString();
    //        }
    //        else
    //        {
    //            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Correct Bill No. for this Month.'); </script> ");
    //            ddlActualBillNo.SelectedIndex = -1;
    //            txtActAmt.Text = "";
    //        }
    //    }
    //}
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("frm_Gdwn_RentBillDeduction_BM_New.aspx");
    }
}
