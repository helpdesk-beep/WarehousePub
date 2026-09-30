using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.Collections;
using System.Linq;

public partial class Special_PV_Inspection_By_HO_Officer_Godown_Wise : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    public SqlConnection con_WLC2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC3 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    public string qry = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        if (!IsPostBack)
        {
            GetRegion();
        }
    }
    private void GetRegion()
    {
        string strDist = "";
        strDist = "SELECT Distinct Region_ID,Regionnm FROM tbl_MetaData_DISTRICT  order by Regionnm";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC3);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlRegion.DataSource = ds.Tables[0];
            ddlRegion.DataTextField = "Regionnm";
            ddlRegion.DataValueField = "Region_ID";
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0, "All");
        }
        else
        {
            ddlRegion.Items.Insert(0, "All");
        }
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDist();
    }
    private void GetDist()
    {
        string strDist = "";
        strDist = "SELECT District_Id,District_Name FROM tbl_MetaData_DISTRICT where Region_ID = '" + ddlRegion.SelectedValue + "' order by District_Name ";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC3);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "All");
        }
        else
        {
            ddldistrict.Items.Insert(0, "All");
        }
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }
    public void GetBranch()
    {
        string qry = "";
        qry = "select distinct BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "'";
        SqlDataAdapter da = new SqlDataAdapter(qry, con_WLC3);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "All");
        }
        else
        {
            ddlBranch.Items.Insert(0, "All");
        }
    }

    public void GetGodown()
    {
        string qry = "";
        qry = "select Godown_Name,Godown_ID from tbl_MetaData_GODOWN_2018 Where BranchID='" + ddlBranch.SelectedValue + "'";
        //qry = "select distinct BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "'";
        SqlDataAdapter da = new SqlDataAdapter(qry, con_WLC3);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "All");
        }
        else
        {
            ddlgodown.Items.Insert(0, "All");
        }
    }
    public void fillgrid()
    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Online_and_PV_VerifiedByRM_ForState_Godown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue);
            cmd.Parameters.AddWithValue("@GodownID", ddlgodown.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                grdGodown.DataSource = dt;
                grdGodown.DataBind();
                divGodown.Visible = true;
                grdGodown.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                grdGodown.FooterRow.Cells[4].Text = "Total";
                grdGodown.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Clossing_Bags_As_per_31_12_2024")).ToString();
                grdGodown.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Blance_As_Per_PV_By_Branch")).ToString();
                grdGodown.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("DiffirenceOnlineandPV")).ToString();
                txthoremark.Text = dt.Rows[0]["Summary_Remark"].ToString();
            }
            else
            {
                grdGodown.DataSource = null;
                grdGodown.DataBind();
            }
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

        GetGodown();

    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        try
        {
            string ErrorMsg = "";
            ErrorMsg += !string.IsNullOrEmpty(txt_InspDate.Text) ? "" : "Enter Date. \\n";
            if (ErrorMsg == "")
            {
                int ICount = 0;
                string con1 = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
                SqlConnection con2 = new SqlConnection(con1);
                if (con2.State == ConnectionState.Closed)
                {
                    con2.Open();
                }
                foreach (GridViewRow row in grdGodown.Rows)
                {
                    //CheckBox chkbox = (CheckBox)row.FindControl("Checked");
                    //if (chkbox.Checked == true)
                    //{
                    //string drpApprovalStatus = (row.FindControl("ddlApprovalStatus") as DropDownList).SelectedValue.ToString();
                    string Godown_Id = (row.FindControl("hdnGodown_ID") as HiddenField).Value;
                    string Depositor_Name = (row.FindControl("lblDepositor_Name") as Label).Text;
                    string CropYear = (row.FindControl("lblCropYear") as Label).Text;
                    string Commodity = (row.FindControl("lblCommodity") as Label).Text;
                    string Clossing_Bags_As_per_31_12_2024 = (row.FindControl("lblClossing_Bags_As_per_31_12_2024") as Label).Text;
                    string Blance_As_Per_PV_By_Branch = (row.FindControl("lblBlance_As_Per_PV_By_Branch") as Label).Text;
                    string DiffirenceOnlineandPV = (row.FindControl("lblDiffirenceOnlineandPV") as Label).Text;
                    string Rm_Remark = (row.FindControl("lblRm_Remark") as Label).Text;
                    string Jama_Bags_31_12_2024 = (row.FindControl("txtJama_Bags_31_12_2024") as TextBox).Text;
                    string Paid_Bags_31_12_2024 = (row.FindControl("txtPaid_Bags_31_12_2024") as TextBox).Text;
                    string No_of_Bags_As_perPV = (row.FindControl("txtNo_of_Bags_As_perPV") as TextBox).Text;
                    string Remark_By_HO_Inspection_Officer = (row.FindControl("txtRemark_By_HO_Inspection_Officer") as TextBox).Text;
                    Godown_Id = Godown_Id.ToString();
                    Depositor_Name = Depositor_Name.ToString();
                    CropYear = CropYear.ToString();
                    Commodity = Commodity.ToString();
                    Clossing_Bags_As_per_31_12_2024 = Clossing_Bags_As_per_31_12_2024.ToString();
                    Blance_As_Per_PV_By_Branch = Blance_As_Per_PV_By_Branch.ToString();
                    DiffirenceOnlineandPV = DiffirenceOnlineandPV.ToString();
                    Rm_Remark = Rm_Remark.ToString();
                    Jama_Bags_31_12_2024 = Jama_Bags_31_12_2024.ToString();
                    Paid_Bags_31_12_2024 = Paid_Bags_31_12_2024.ToString();
                    No_of_Bags_As_perPV = No_of_Bags_As_perPV.ToString();
                    Remark_By_HO_Inspection_Officer = Remark_By_HO_Inspection_Officer.ToString();
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlCommand cmd3 = new SqlCommand("Special_PV_Verified_By_State_Inspection_Officer", con2);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.AddWithValue("@Branch_Id", ddlBranch.SelectedValue);
                    cmd3.Parameters.AddWithValue("@Godown_ID", Godown_Id.ToString());
                    cmd3.Parameters.AddWithValue("@Depositor_Name", Depositor_Name.ToString());
                    cmd3.Parameters.AddWithValue("@CropYear", CropYear.ToString());
                    cmd3.Parameters.AddWithValue("@Commodity", Commodity.ToString());
                    cmd3.Parameters.AddWithValue("@Clossing_Bags", Clossing_Bags_As_per_31_12_2024.ToString());
                    cmd3.Parameters.AddWithValue("@PV_By_BM", Blance_As_Per_PV_By_Branch.ToString());
                    cmd3.Parameters.AddWithValue("@Diffrence", DiffirenceOnlineandPV.ToString());
                    cmd3.Parameters.AddWithValue("@PV_By_RM_Remark", Rm_Remark.ToString());
                    cmd3.Parameters.AddWithValue("@Jama_Bags", Jama_Bags_31_12_2024.ToString());
                    cmd3.Parameters.AddWithValue("@Paid_Bags", Paid_Bags_31_12_2024.ToString());
                    cmd3.Parameters.AddWithValue("@No_Of_Bags_As_Per_PV", No_of_Bags_As_perPV.ToString());
                    cmd3.Parameters.AddWithValue("@Officer_Name", txtOfficerName.Text);
                    cmd3.Parameters.AddWithValue("@Mobile_No", txtmobile.Text);
                    cmd3.Parameters.AddWithValue("@Insp_Date", getDate_MDY(txt_InspDate.Text));
                    cmd3.Parameters.AddWithValue("@Remark_By_HO", Remark_By_HO_Inspection_Officer);
                    cmd3.Parameters.AddWithValue("@Summary_Remark", txthoremark.Text);
                    cmd3.Parameters.AddWithValue("@Created_By", ddlBranch.SelectedValue);
                    cmd3.Parameters.AddWithValue("@CreatedBy_IP", ipAddress);
                    int i = cmd3.ExecuteNonQuery();
                    ICount = ICount + i;
                    //}
                }
                string strMsg = "Record Submit Succesfully!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                fillgrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' " + ErrorMsg.ToString() + "')", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }

    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
        string qry = "";
        qry = "Select Officer_Name,Mobile_No from Inspection_Scheduled_For_Office_Special_PV Where Branch_ID = '" + ddlBranch.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtOfficerName.Text = ds.Tables[0].Rows[0]["Officer_Name"].ToString();
            txtOfficerName.Attributes.Add("readonly", "readonly");
            txtmobile.Text = ds.Tables[0].Rows[0]["Mobile_No"].ToString();
            txtmobile.Attributes.Add("readonly", "readonly");
        }
        else
        {
            txtOfficerName.Text = "";
            txtOfficerName.Attributes.Add("readonly", "readonly");
            txtmobile.Text = "";
            txtmobile.Attributes.Add("readonly", "readonly");
        }
    }
}