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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;

public partial class Accounting_frm_Gdwn_RentBillDeduction_BM_For_NCCF : System.Web.UI.Page
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
                GetGodown();
                SetInitialRow();
                fillFinancialYear();
                fillMonth();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void FillBillDetails()
    {
        string FinYear = ddlFyear.SelectedItem.Text;
        string RFinYear = ddlFyear.SelectedItem.Text;
        RFinYear = NineCrop(RFinYear);
        FinYear = FinYear.ToString();
        string cropyear = ddlCropYear.SelectedItem.Text;
        string Rcropyear = ddlCropYear.SelectedItem.Text;
        Rcropyear = NineCrop(Rcropyear);
        cropyear = cropyear.ToString();
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Rent_And_SC_Bill_Details_For_NCCF", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Godown_Id", ddlgodown.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Commodity_Id", ddlcomodity.SelectedValue);
                cmd.Parameters.AddWithValue("@Crop_Year", cropyear.ToString());
                cmd.Parameters.AddWithValue("@RCrop_Year", Rcropyear.ToString());
                cmd.Parameters.AddWithValue("@Financial_Year", FinYear.ToString());
                cmd.Parameters.AddWithValue("@RFinancial_Year", RFinYear.ToString());
                cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                lblActualBillNo.Text = ds.Tables[0].Rows[0]["SCBillNo"].ToString();
                                txtActAmt.Text = ds.Tables[0].Rows[0]["SCBillNet_Amount"].ToString();
                            }
                            else
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('इस माह का स्टोरेज बिल नहीं बनाया गया हैं ...'); </script> ");
                            }
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                lblrentbillno.Text = ds.Tables[1].Rows[0]["RentBillNo"].ToString();
                                lblMonth2.Text = ds.Tables[1].Rows[0]["Month"].ToString();
                                lblRPM.Text = ds.Tables[1].Rows[0]["RentCommodity_Rate"].ToString();
                                txtBilAmt.Text = ds.Tables[1].Rows[0]["RentBillNet_Amount"].ToString();
                                billdetails.Visible = true;
                            }
                            else
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('इस माह का रेंट बिल नहीं बनाया गया हैं ...'); </script> ");
                            }
                        }
                        else
                        {

                        }
                    }
                }
            }
        }
    }
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        string FinYear = ddlFyear.SelectedItem.Text;
        FinYear = NineCrop(FinYear);
        string cropyear = ddlCropYear.SelectedItem.Text;
        cropyear = NineCrop(cropyear);
        if (ddlCropYear.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Crop Year...'); </script> ");
        }
        else if (ddlmonth.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Month...'); </script> ");
        }
        else
        {
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Check_Deduction_For_NCCF", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Godown_Id", ddlgodown.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Commodity_Id", ddlcomodity.SelectedValue);
                    cmd.Parameters.AddWithValue("@Crop_Year", cropyear.ToString());
                    cmd.Parameters.AddWithValue("@Financial_Year", FinYear.ToString());
                    cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["Bill_No"].ToString() != "" || dt.Rows[0]["Bill_No"].ToString() == "NULL")
                                {
                                    billdetails.Visible = false;
                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('इस गोडाउन का इस माह के बिल का कटोत्रा हो गया हैं ...'); </script> ");
                                }
                                else
                                {
                                    FillBillDetails();
                                }
                            }
                            else
                            {
                                billdetails.Visible = false;
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('इस गोडाउन का इस माह के बिल का कटोत्रा हो गया हैं ...'); </script> ");
                            }
                        }
                    }
                }
            }
        }
        //if (ddlCropYear.SelectedItem.Text == "--Select--")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Crop Year...'); </script> ");
        //}
        //else if (ddlmonth.SelectedItem.Text == "--Select--")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Month...'); </script> ");
        //}
        //else
        //{
        //    FillBillDetails();
        //}

    }
    protected void fillMonth()
    {
        //ddlmonth.ClearSelection();
        ddlmonth.Items.Clear();
        ddlmonth.Items.Add(new ListItem("--Select--", "0"));
        ddlmonth.Items.Add(new ListItem("January", "1"));
        ddlmonth.Items.Add(new ListItem("February", "2"));
        ddlmonth.Items.Add(new ListItem("March", "3"));
        ddlmonth.Items.Add(new ListItem("April", "4"));
        ddlmonth.Items.Add(new ListItem("May", "5"));
        ddlmonth.Items.Add(new ListItem("June", "6"));
        ddlmonth.Items.Add(new ListItem("July", "7"));
        ddlmonth.Items.Add(new ListItem("August", "8"));
        ddlmonth.Items.Add(new ListItem("September", "9"));
        ddlmonth.Items.Add(new ListItem("October", "10"));
        ddlmonth.Items.Add(new ListItem("November", "11"));
        ddlmonth.Items.Add(new ListItem("December", "12"));
        ddlmonth.SelectedIndex = 0;
    }
    protected void fillFinancialYear()
    {
        ddlFyear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        //ddlFyear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        ddlFyear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        ddlFyear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        ddlFyear.Items.Add((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        ddlFyear.Items.Add((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        ddlFyear.Items.Add((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
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
        qry = "select distinct Commodity_Id,Commodity_Name from View_WHRcurrentstock where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Depositor_Name='NCCF'";
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
    }
    protected void ddlcomodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  GetBillList();
        GetCropYear();
    }
    protected void btnSumbmitRent_Click(object sender, EventArgs e)
    {
        if (txtlocknotopen.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('यदी कोई भी कटोत्रा नहीं है तो '0' की प्रविष्टि करें, ताला नहीं खोलना ...')", true);
            txtlocknotopen.Focus();
            return;
        }
        if (txtRoadBlock.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('यदी कोई भी कटोत्रा नहीं है तो '0' की प्रविष्टि करें, रास्ता अवरुद्ध कर देना ...')", true);
            txtRoadBlock.Focus();
            return;
        }
        
        else
        {
            Insert_Godown_Rent_Deduction();
        }
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
                string qry3 = "INSERT INTO [tbl_Godown_Rent_Deduction_Amount_NCCF]([Bill_No],[District_Id],[Branch_Id],[Godown_Id],Dunnage_PS_Amt,Elect_Beam_Scale_Amt,TResources_Deduct_Amt,[TBill_Deduct_Amt],TBill_Amount,CreatedBy,CreatedDate,Ref_Bill_No) VALUES ('" + lblrentbillno.Text.ToString() + "','" + DistrictId + "','" + BranchId + "','" + ddlgodown.SelectedValue + "','" + txtlocknotopen.Text.Trim() + "','" + txtRoadBlock.Text.Trim() + "','" + txt_GrandTotal.Text.Trim() + "','" + txt_GrandTotal.Text.Trim() + "','" + txtBilAmt.Text + "','" + ip + "',GETDATE(),'" + lblActualBillNo.Text.ToString() + "')";
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
                            string qry2 = "INSERT INTO [tbl_Godown_Rent_Deduction_Vivran_NCCF]([Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Res_ID],[Res_Det_Karan],[Deduction_Amt],[Res_Det_Remark],CreatedBy,CreatedDate) VALUES ('" + lblrentbillno.Text.ToString() + "','" + DistrictId + "','" + BranchId + "','" + ddlgodown.SelectedValue + "','" + S_R_Vivran + "',N'" + ((TextBox)gvGodown.Rows[i].FindControl("txtK_Vivran")).Text.Trim() + "','" + S_R_Rashi + "',N'" + ((TextBox)gvGodown.Rows[i].FindControl("txtK_Remark")).Text.Trim() + "','" + ip + "',GETDATE())";
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Successfully Save Record'); window.location =('frm_Gdwn_RentBillDeduction_BM_For_NCCF.aspx');", true);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Something Wrong Please try Again'); window.location =('frm_Gdwn_RentBillDeduction_BM.aspx');", true);
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
            //txt_GrandTotal.Text = Convert.ToString(Math.Round( sum));
            decimal sum11 = Convert.ToDecimal(txtlocknotopen.Text);
            decimal sum12 = Convert.ToDecimal(txtRoadBlock.Text);
            decimal Result = sum11 + sum12 + sum;
            txt_GrandTotal.Text = Convert.ToString(Math.Round(Result));
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('पहले कतोत्र की जानकारी भरे..'); </script> ");
        }
    }
    public string NineCrop(string INCROPS)
    {
        string InCrop = INCROPS;
        string OutCrop = "";
        if (InCrop == "2015-16")
        {
            OutCrop = "2015-2016";
        }
        else if (InCrop == "2016-17")
        {
            OutCrop = "2016-2017";
        }
        else if (InCrop == "2017-18")
        {
            OutCrop = "2017-2018";
        }
        else if (InCrop == "2018-19")
        {
            OutCrop = "2018-2019";
        }
        else if (InCrop == "2019-20")
        {
            OutCrop = "2019-2020";
        }
        else if (InCrop == "2020-21")
        {
            OutCrop = "2020-2021";
        }
        else if (InCrop == "2021-22")
        {
            OutCrop = "2021-2022";
        }
        else if (InCrop == "2022-23")
        {
            OutCrop = "2022-2023";
        }
        else if (InCrop == "2023-24")
        {
            OutCrop = "2023-2024";
        }
        else if (InCrop == "2024-25")
        {
            OutCrop = "2024-2025";
        }
        else if (InCrop == "2025-26")
        {
            OutCrop = "2025-2026";
        }
        else if (InCrop == "2026-27")
        {
            OutCrop = "2026-2027";
        }
        return OutCrop;
    }
    void GetCropYear()
    {
        qry = "select distinct CropYear from View_WHRcurrentstock where Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlCropYear.DataSource = ds.Tables[0];
            ddlCropYear.DataTextField = "CropYear";
            ddlCropYear.DataValueField = "CropYear";
            ddlCropYear.DataBind();
            ddlCropYear.Items.Insert(0, "--Select--");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("frm_Gdwn_RentBillDeduction_BM.aspx");
    }
}
