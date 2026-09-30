using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_States_Payment_BillsFromCSMStoMPWLC_Status_Rept_Region_Wise : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    string query = "";
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillRegion();
            fillComodity();
            GetCropYear();
        }
    }
    private void fillComodity()
    {
        try
        {

            string query = "";
            query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlComodity.Items.Clear();
                ddlComodity.DataSource = ds.Tables[0];
                ddlComodity.DataTextField = "Commodity_Name";
                ddlComodity.DataValueField = "Commodity_Id";
                ddlComodity.DataBind();
                ddlComodity.Items.Insert(0, "--Select--");
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
    private void fillRegion()
    {
        try
        {
            string query = "";

            query = "SELECT DISTINCT [Region_ID],[Regionnm] FROM [tbl_MetaData_DISTRICT] order by Regionnm asc";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlRegion.Items.Clear();
                ddlRegion.DataSource = ds.Tables[0];
                ddlRegion.DataTextField = "Regionnm";
                ddlRegion.DataValueField = "Region_ID";
                ddlRegion.DataBind();
                ddlRegion.Items.Insert(0, "--Select--");
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

    //private void fillDistrict()
    //{
    //    try
    //    {
    //        string query = "";

    //        query = "SELECT District_Id,District_Name FROM [tbl_MetaData_DISTRICT] where Region_ID='" + ddlRegion.SelectedValue + "' order by Regionnm asc";

    //        cmd = new SqlCommand(query, con);
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddldistrict.Items.Clear();
    //            ddldistrict.DataSource = ds.Tables[0];
    //            ddldistrict.DataTextField = "District_Name";
    //            ddldistrict.DataValueField = "District_Id";
    //            ddldistrict.DataBind();
    //            ddldistrict.Items.Insert(0, "--Select--");
    //        }
    //        else
    //        {
    //            ////
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //////
    //    }
    //}

    //private void fillBranch()
    //{
    //    try
    //    {
    //        string query = "";

    //        query = "SELECT BranchId,DepotName FROM tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "' order by DepotName asc";

    //        cmd = new SqlCommand(query, con);
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlbranch.Items.Clear();
    //            ddlbranch.DataSource = ds.Tables[0];
    //            ddlbranch.DataTextField = "DepotName";
    //            ddlbranch.DataValueField = "BranchId";
    //            ddlbranch.DataBind();
    //            ddlbranch.Items.Insert(0, "--Select--");
    //        }
    //        else
    //        {
    //            ////
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //////
    //    }
    //}
    void GetCropYear()
    {
        try
        {
            qry = "select 'All' as CropText ,'2%' as CropYear union(select distinct CropYear, CropYear + '%' as CropYear from tbl_storage_Depositor_WHR_Relation where CropYear like('2%')) order by CropYear";
            cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds == null)
            {
            }
            else
            {
                ddlCropYear.DataSource = ds.Tables[0];
                ddlCropYear.DataTextField = "CropText";
                ddlCropYear.DataValueField = "CropText";
                ddlCropYear.DataBind();
                //ddlCropYear.Items.Insert(0, "--Select Crop Year--");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void fillgrid()
    {

        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Branch_wise_Stock_Position_Fill_TA", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionID", ddlRegion.SelectedValue);
                cmd.Parameters.AddWithValue("@Commodity_ID", ddlComodity.SelectedValue);
                cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedValue);
                //if (ddlRegion.SelectedValue == "--Select--")
                //{
                //    cmd.Parameters.AddWithValue("@RegionID", 0);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@RegionID", ddlRegion.SelectedValue);
                //}

                //if (ddlComodity.SelectedValue == "--Select--")
                //{
                //    cmd.Parameters.AddWithValue("@Commodity_ID", 0);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@Commodity_ID", ddlComodity.SelectedValue);
                //}
                //if (ddlbranch.SelectedValue == "--Select--")
                //{
                //    cmd.Parameters.AddWithValue("@branchid", 0);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@branchid", ddlbranch.SelectedValue);
                //}


                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            //grdavlqty.Caption = @"<b style=""font-weight: bold;"">M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "received Payment from MPSCSC through NEFT Payment System " + "</b> ";
                            grdavlqty.DataSource = dt;
                            grdavlqty.DataBind();
                            divbtn.Visible = true;
                        }
                        else
                        {
                            grdavlqty.DataSource = null;
                            grdavlqty.DataBind();
                        }
                    }
                }
            }
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
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        // fillgrid();
        // fillDistrict();
    }
    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        // fillgrid();
        // fillBranch();
        // fillFinancialYear();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillgrid();
        // fillFinancialYear();
    }

    protected void ddlfy_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillgrid();
    }

    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillgrid();
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
    }

    protected void tbnview_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    //protected void ddlComodity_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgrid();
    //}

    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            foreach (GridViewRow row in grdavlqty.Rows)
            {
                HiddenField hdnBranchID = (HiddenField)row.FindControl("hdnBranchID");
                Label lblOnlineQty = (Label)row.FindControl("lblOnlineQty");
                TextBox lblAvlQty = (TextBox)row.FindControl("lblAvlQty");

                con.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[Insert_Branch_Wise_Stock_Position_Filled_by_TA]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", hdnBranchID.Value);
                cmd.Parameters.AddWithValue("@Commodity_ID", ddlComodity.SelectedValue);
                cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedValue);
                cmd.Parameters.AddWithValue("@OnlineQty", lblOnlineQty.Text);
                cmd.Parameters.AddWithValue("@AvlQty", lblAvlQty.Text);
                cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    count++;
                    fillgrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "')", true);
                }
                con.Close();
                //if (TheResult.StartsWith("SUCCESS"))
                //{
                //    string strMsg = "Filed Stock Position Successfully|||";

                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                //    fillgrid();
                //}
                ////else
                ////{
                ////    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
                ////}
            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Filed Stock Position Successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Annaxure A Not Submited')", true);
            }
        }
        catch (Exception ex)
        {
            //tn.Rollback();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }

    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}