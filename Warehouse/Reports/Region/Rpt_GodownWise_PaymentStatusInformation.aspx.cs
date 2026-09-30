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

public partial class Region_Rpt_GodownWise_PaymentStatusInformation : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            fillDistrict();
            fillGodownType();
            fillGodownName();
            //fillDetailsInGrid();
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
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd = new SqlCommand(query, con);
            sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("--Select--", "0"));

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
    private void fillGodownName()
    {
        string vBranchID = ddlDepotList.SelectedValue.ToString();
        try
        {
            string query = "";
            query = "select MG.Godown_ID [GodownID], MG.Godown_Name [GodownName] from tbl_Metadata_Godown_2018 MG WHERE MG.BranchID =" + vBranchID + " and Hired_Type  ='" + ddlGodownType.SelectedItem.ToString() + "' and IsActive='Y' order by Godown_Name Asc";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodownName.Items.Clear();
                ddlGodownName.DataSource = ds.Tables[0];
                ddlGodownName.DataTextField = "GodownName";
                ddlGodownName.DataValueField = "GodownID";
                ddlGodownName.DataBind();
                ddlGodownName.Items.Insert(0, "--Select--");
                ddlGodownName.Items.Insert(0, new ListItem("--Select--", "0"));
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
    private void fillGodownType()
    {
        try
        {
            string query = "";
            query = "select distinct MG.Hired_Type [GodownType] from tbl_Metadata_Godown_2018 MG ORDER BY MG.Hired_Type";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodownType.Items.Clear();
                ddlGodownType.DataSource = ds.Tables[0];
                ddlGodownType.DataTextField = "GodownType";
                ddlGodownType.DataValueField = "GodownType";
                ddlGodownType.DataBind();
                //ddlGodownType.Items.Insert(0, "--Select--");
                ddlGodownType.Items.Insert(0, new ListItem("--Select--", "0"));
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
                ddlDepotList.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception)
        {
            ///////
        }
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
        //string ErrorMsg = "";
        //lblMsg.Text = "";
        //ErrorMsg += ddlFinancialYear.SelectedIndex > 0 ? "" : "वित्‍तीय वर्ष चुने \\n";
        //ErrorMsg += ddlGodownType.SelectedIndex > 0 ? "" : "गोदाम का प्रकार चुने \\n";
        //ErrorMsg += ddlGodownName.SelectedIndex > 0 ? "" : "गोदाम का नाम चुने \\n";
        //if (ErrorMsg == "")
        //{
        string RegionID = Session["Region_ID"].ToString();
        string DistrictID = ddlDistrict.SelectedValue.ToString();
        string BranchID = ddlDepotList.SelectedValue.ToString();
        string FinancialYear = ddlFinancialYear.SelectedValue.ToString();
        string GodownID = ddlGodownName.SelectedValue.ToString();
        string GodownType = ddlGodownType.SelectedItem.ToString();

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        fillDetailsInGrid();
        //}
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        else
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' " + ErrorMsg.ToString() + "')", true);
        }
    }
    protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodownName();
    }
    protected void fillDetailsInGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        string RegionID = Session["Region_ID"].ToString();
        //string BranchID = Session["BranchID"].ToString();
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_GodownWisePaymentEntry", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@RegionID", RegionID);
                cmd.Parameters.AddWithValue("@FinancialYear", ddlFinancialYear.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@DistrictID", ddlDistrict.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@BranchID", ddlDepotList.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@GodownType", ddlGodownType.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@GodownID", ddlGodownName.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    DataTable MainTable = ds.Tables[0];

                    if (MainTable.Rows.Count > 0)
                    {
                        GV_EntryDone.DataSource = MainTable;
                        GV_EntryDone.DataBind();
                        GV_EntryDone.FooterRow.Cells[7].Text = "Total";
                        GV_EntryDone.FooterRow.Cells[8].Text = MainTable.AsEnumerable().Sum(row => row.Field<Decimal>("TotalAmountInRentInFY")).ToString();
                        GV_EntryDone.FooterRow.Cells[9].Text = MainTable.AsEnumerable().Sum(row => row.Field<Decimal>("TotalAmountPaidToGodownOwnerInFY")).ToString();
                        GV_EntryDone.FooterRow.Cells[10].Text = MainTable.AsEnumerable().Sum(row => row.Field<Decimal>("RemainingAmountOfGodownOwner")).ToString();
                    }
                    else
                    {
                        GV_EntryDone.DataSource = null;
                        GV_EntryDone.DataBind();
                    }

                }
            }
        }
    }
    protected void GV_EntryDone_RowCommand(object sender, GridViewCommandEventArgs e)
    {

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

}


