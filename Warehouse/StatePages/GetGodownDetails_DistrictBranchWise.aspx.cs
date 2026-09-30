using System;
using System.Linq;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Diagnostics;
using System.Resources;

public partial class StatePages_GetGodownDetails_DistrictBranchWise : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    string Branch = "";
    string Distid = "";
    decimal qtyTotal = 0;
    decimal grQtyTotal = 0;
    decimal qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    string storid = "0";
    int rowIndex = 1;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                getdistrict();
                fillGodownType();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    //public void GetBranchData()
    //{
    //    try
    //    {
    //        string qry = "";
    //        qry = "select Godown_ID,Godown_Name,Hired_Type,Storage_Type,Godown_Capacity,Closing_Balance,LicNum,convert(varchar(10),LicDate,103) as LicDate,Godown_Scientific_Capacity  from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and IsActive='Y'";
    //        SqlCommand cmd = new SqlCommand(qry, con);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            Depositor_Gridview.DataSource = ds;
    //            Depositor_Gridview.DataBind();
    //            //trmobtxt.Visible = true;
    //            //tr1.Visible = true;
    //            //tr2.Visible = true;
    //            //trbtnhide.Visible = true;
    //            //txtGdwnID.Text = "";
    //            //txtclosing.Text = "";
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
    //            //trbtnhide.Visible = false;
    //            //trmobtxt.Visible = false;
    //            //trbtnhide.Visible = false;
    //            //tr1.Visible = false;
    //            //tr2.Visible = false;
    //            Depositor_Gridview.DataSource = null;
    //            Depositor_Gridview.DataBind();
    //            //txtGdwnID.Text = "";
    //            //txtclosing.Text = "";
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

    public void GetBranch(string distID)
    {

        string qry = "";
        qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId ='" + distID + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "--Select--");
        }
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch(ddlDistrict.SelectedValue.ToString());
    }

    public void getdistrict()
    {
        string qry = "select District_Name,District_Id from tbl_MetaData_DISTRICT order by District_Name ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDistrict.DataSource = ds.Tables[0];
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_Id";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetBranchData();
        //FillBillDetailsInGrid();
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

    protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void FillBillDetailsInGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_GetGodownDetails_DistrictBranchWise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                if (ddlBranch.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@BranchID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue);
                }
                if (ddlGodownType.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@HiredType", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@HiredType", ddlGodownType.SelectedValue);
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    cmd.ExecuteNonQuery();
                    sda.Fill(ds);
                    DataTable MainTable = ds.Tables[0];
                    if (MainTable.Rows.Count > 0)
                    {
                        Depositor_Gridview.DataSource = MainTable;
                        Depositor_Gridview.DataBind();
                        Depositor_Gridview.FooterRow.Style.Add("text-align", "right");
                        Depositor_Gridview.FooterRow.Cells[4].Text = "Grand Total";
                        Depositor_Gridview.FooterRow.Cells[5].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("ScientificCapacityInMT")).ToString();
                        Depositor_Gridview.FooterRow.Cells[6].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("MaxCapacityInMT")).ToString();
                    }
                    else
                    {
                        Depositor_Gridview.DataSource = null;
                        Depositor_Gridview.DataBind();
                    }

                }
            }
        }
    }

    protected void Depositor_Gridview_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "GodownName").ToString());
            decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "MaxCapacityInMT").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ScientificCapacityInMT").ToString());

            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;

            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;


        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillBillDetailsInGrid();
    }

}
