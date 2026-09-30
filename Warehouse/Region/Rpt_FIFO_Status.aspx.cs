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
using System.Globalization;
using System.IO;
using System.Text;
using System.Drawing;

public partial class Reports_Region_Rpt_FIFO_Status : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    string qry = "";
    SqlTransaction sqltrans;
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    string Bill_Type = "";
    string Ref_Number = "";
    string Ref_Aid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetRegionfordist();
            fillDistrict();
            //GetBillsDetail(); 

        }
    }
    private void GetRegionfordist()
    {
        try
        {
            string qrySelect = "SELECT * FROM tbl_MetaData_Region where Region_ID='" + Session["Region_Logid"].ToString() + "' order by region";
            SqlDataAdapter da = new SqlDataAdapter(qrySelect, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlregion.DataSource = ds.Tables[0];
                ddlregion.DataTextField = "region";
                ddlregion.DataValueField = "Region_Id";
                ddlregion.DataBind();
                ddlregion.Enabled = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    //protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillDistrict();
    //    GetBillsDetail();
    //}
    private void fillDistrict()
    {
        try
        {
            string region = "";
            //if (Session["UserName"].ToString() != "MPSWLC")
            //{

            //    if (Session["Region_ID"].ToString() != null)
            //    {
            //        region = Session["Region_ID"].ToString();

            //    }
            //}
            string query = "";
            //if (Session["UserName"].ToString() == "MPSWLC")
            //{
            //    query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            //}
            //else
            //{
            //    query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
            //}
            query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + Session["Region_Logid"].ToString() + "' order by District_Name asc";
            cmd = new SqlCommand(query, con);
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
                //gv.DataSource = null;
                //gv.DataBind();
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
    public void GetBranch()
    {
        string qry = "";
        qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDepotList.DataSource = ds.Tables[0];
            ddlDepotList.DataTextField = "DepotName";
            ddlDepotList.DataValueField = "BranchId";
            ddlDepotList.DataBind();
            ddlDepotList.Items.Insert(0, "--Select--");
        }
    }
    public void fillGodownDetails()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Fill_Godown_List", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue);
            cmd.Parameters.AddWithValue("@Branch_ID", ddlDepotList.SelectedValue);
            con.Open();
            ddlgodown.DataSource = cmd.ExecuteReader();
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_id";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con.Close();
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex != 0)
        {
            GetBranch();
           // GetBillsDetail();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District')", true);
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_WHR_Wise_Data_For_FIFO_Policy_For_Region", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", ddlDepotList.SelectedValue);
                if(ddlgodown.SelectedValue== "-- Select Godown --")
                {
                cmd.Parameters.AddWithValue("@GodownID", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@GodownID", ddlgodown.SelectedValue);
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            gdstackdetail.DataSource = dt;
                            gdstackdetail.DataBind();
                            // gdstackdetail.Caption = @"<b style=""font-weight: bold;""> M.P. Warehousing & Logistics Corporarion" + "</br> " + "Storage Charges Bill(After August)";
                           // lblNoofAC.Text = dt.Rows.Count.ToString();
                            gdstackdetail.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            gdstackdetail.FooterRow.Cells[5].Text = "Total";
                            gdstackdetail.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Qty")).ToString();
                            gdstackdetail.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("FIFOFrizwedStock")).ToString();
                            gdstackdetail.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvailQty")).ToString();

                        }
                        else
                        {
                            gdstackdetail.DataSource = dt;
                            gdstackdetail.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void gdstackdetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //  HiddenField hdnVerificationType = (HiddenField)e.Row.FindControl("hdnVerificationType");
            HiddenField hdndiffirence = (HiddenField)e.Row.FindControl("hdndiffirence");
            if (Convert.ToInt32(hdndiffirence.Value) > 0)
            {
                e.Row.BackColor = ColorTranslator.FromHtml("#28b779");//e9716b
            }
            //else
            //{
            //    e.Row.BackColor = ColorTranslator.FromHtml("#28b779");//28b779
            //}
        }

    }

    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodownDetails();
        fillgrid();
    }

    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}