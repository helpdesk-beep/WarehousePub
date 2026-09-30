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
public partial class Accounting_Update_Currect_Storage_Bill_With_RentBill : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            GetDistrict();
            //SetInitialRow();
            fillFinancialYear();
            fillMonth();
        }

    }

    protected void FillBillDetails()
    {
        string FinYear = ddlFyear.SelectedItem.Text;
        FinYear = NineCrop(FinYear);
        string cropyear = ddlCropYear.SelectedItem.Text;
        cropyear = NineCrop(cropyear);
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Rent_And_SC_Bill_Details", con))
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
                            //lblrentbillno.Text = ds.Tables[1].Rows[0]["RentBillNo"].ToString();
                            //lblMonth2.Text = ds.Tables[1].Rows[0]["Month"].ToString();
                            //lblRPM.Text = ds.Tables[1].Rows[0]["RentCommodity_Rate"].ToString();
                            //txtBilAmt.Text = ds.Tables[1].Rows[0]["RentBillNet_Amount"].ToString();
                            //lblActualBillNo.Text = ds.Tables[0].Rows[0]["SCBillNo"].ToString();
                            //txtActAmt.Text = ds.Tables[0].Rows[0]["SCBillNet_Amount"].ToString();

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
        FillBillDetails();
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
        ddlFyear.Items.Add((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2));
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
    }
    protected void gvGodown_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
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

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        //AddNewRowToGrid();
    }
    public void GetDistrict()
    {

        try
        {
            //string query = "select depo.BranchId,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";
            string query = "select District_Name,District_Id from tbl_MetaData_DISTRICT order by District_Name";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldistrict.DataSource = ds.Tables[0];
                ddldistrict.DataTextField = "District_Name";
                ddldistrict.DataValueField = "District_Id";
                ddldistrict.DataBind();
                ddldistrict.Items.Insert(0, "---Select---");
                //ddlGodown.DataSource = null;
                //ddlGodown.DataBind();
                //gv.DataSource = null;
                //gv.DataBind();
            }
            else
            {
                ddldistrict.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception)
        {
            ///////
        }
    }

    public void GetBranch()
    {

        try
        {
            string query = "select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue+"' order by DepotName";
           //string query = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 where BranchID='" + ddlDepotList.SelectedValue.ToString() + "'";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbranch.DataSource = ds.Tables[0];
                ddlbranch.DataTextField = "DepotName";
                ddlbranch.DataValueField = "BranchId";
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, "---Select---");
                //ddlGodown.DataSource = null;
                //ddlGodown.DataBind();
                //gv.DataSource = null;
                //gv.DataBind();
            }
            else
            {
                ddlbranch.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception)
        {
            ///////
        }
    }

    public void GetGodown()
    {
       
        ddlgodown.Items.Clear();
        qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='"+ddldistrict.SelectedValue+"' and BranchID='" +ddlbranch.SelectedValue + "'";
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
    }
    protected void ddlcomodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  GetBillList();
        GetCropYear();
    }

    protected void btnSumbmitRent_Click(object sender, EventArgs e)
    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Update_Cureect_Storage_Bill_Number", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        cmd.Parameters.AddWithValue("@JVS_Bill_Number", lblrentbillno.Text.ToString());
        cmd.Parameters.AddWithValue("@SC_Bill_Number", lblActualBillNo.Text.ToString());
        cmd.Parameters.AddWithValue("@IPAddress", ipAddress);
        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

        if (TheResult.StartsWith("SUCCESS"))
        {
            string strMsg = "Update Currect Storage Cherges Bill Number Successfully |||";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
        }
        else
        {
            string strMsg = "Not Updatted |||";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
        }
    }

    protected void gvGodown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

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

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodown();
    }
}
