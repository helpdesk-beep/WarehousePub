using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;

public partial class SRV_Storage_Reports_Inspenctions_Rpt_Godown_Wise_Qty_Available_and_VacantCapacity_New : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            filDivision();
            //fillGodown();
            fillCropYear();
            GetDepositor();
            fillGodownType();
            fillStorageType();
            //fillGodownType();
        }
    }
    private void fillGodownType()
    {
        try
        {

            string query = "";
            query = "Select  Distinct Hired_Type from tbl_MetaData_GODOWN_2018 Order By Hired_Type";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodowntype.Items.Clear();
                ddlgodowntype.DataSource = ds.Tables[0];
                ddlgodowntype.DataTextField = "Hired_Type";
                ddlgodowntype.DataValueField = "Hired_Type";
                ddlgodowntype.DataBind();
                //ddlgodowntype.Items.Insert(0, "All");
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

    private void fillStorageType()
    {
        try
        {

            string query = "";
            query = "Select  Distinct Storage_Type from tbl_MetaData_GODOWN_2018";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlStorageType.Items.Clear();
                ddlStorageType.DataSource = ds.Tables[0];
                ddlStorageType.DataTextField = "Storage_Type";
                ddlStorageType.DataValueField = "Storage_Type";
                ddlStorageType.DataBind();
                //ddlStorageType.Items.Insert(0, "All");
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
    private void filDivision()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select Distinct Region_ID,Regionnm from tbl_MetaData_DISTRICT Order By Regionnm ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldivision.Items.Clear();
                ddldivision.DataSource = ds.Tables[0];
                ddldivision.DataTextField = "Regionnm";
                ddldivision.DataValueField = "Region_ID";
                ddldivision.DataBind();
                ddldivision.Items.Insert(0, "All");
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
    protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        filDistrict();
        
    }
    private void filDistrict()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select District_Id,District_Name from tbl_MetaData_DISTRICT where Region_id='" + ddldivision.SelectedValue + "' Order By District_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldistrict.Items.Clear();
                ddldistrict.DataSource = ds.Tables[0];
                ddldistrict.DataTextField = "District_Name";
                ddldistrict.DataValueField = "District_Id";
                ddldistrict.DataBind();
                ddldistrict.Items.Insert(0, "All");
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
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        //divregion.Visible = false;
        filBranch();
        

    }
    private void filBranch()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue.ToString() + "' Order By DepotName ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbranch.Items.Clear();
                ddlbranch.DataSource = ds.Tables[0];
                ddlbranch.DataTextField = "DepotName";
                ddlbranch.DataValueField = "BranchId";
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, "All");
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

    protected void ddlgodowntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    private void fillCropYear()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            //query = "Select distinct CropYear from tbl_storage_Depositor_WHR_Relation where CropYear not in('All','Before 2009','Before 2013','Before 2014','','All','0')";
            query = "Select Crop_Year from tbl_MetaData_Crop_Year Order By Crop_Year ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcropyear.Items.Clear();
                ddlcropyear.DataSource = ds.Tables[0];
                ddlcropyear.DataTextField = "Crop_Year";
                ddlcropyear.DataValueField = "Crop_Year";
                ddlcropyear.DataBind();
                ddlcropyear.Items.Insert(0, "All");
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
    private void GetDepositor()
    {
        string query2 = "";
        query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','181','184','4679','10535','15478')";

        //}
        SqlCommand cmd2 = new SqlCommand(query2, con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count > 0)
        {
            ddlDepositor.DataSource = ds2;
            ddlDepositor.DataTextField = "Depositor_Name";
            ddlDepositor.DataValueField = "Depositor_ID";
            ddlDepositor.DataBind();
            ddlDepositor.Items.Insert(0, "All");
            
        }
        
    }

    protected void ddlcropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        
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
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (txtpaymentdate.Text == "")
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Date!....')", true);
            txtpaymentdate.Focus();
            return;
        }
        fillgrid();

    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Stock_Positio_As_Per_Selection_New", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtpaymentdate.Text));
                if (ddldivision.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Regionid", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Regionid", ddldivision.SelectedValue);
                }
                if (ddlcropyear.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@CropYear", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CropYear", ddlcropyear.SelectedValue);
                }
                if (ddlDepositor.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Depositor_ID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Depositor_ID", ddlDepositor.SelectedValue);
                }
                //if (ddlgodowntype.SelectedValue == "All")
                //{
                //    cmd.Parameters.AddWithValue("@Hired_Type", "0");
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@Hired_Type", ddlgodowntype.SelectedValue);
                //}
                //if (ddlStorageType.SelectedValue == "All")
                //{
                //    cmd.Parameters.AddWithValue("@Storage_Type", "0");
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@Storage_Type", ddlStorageType.SelectedValue);
                //}

                string selectedIDs = string.Join(",",
                        ddlgodowntype.Items.Cast<ListItem>()
                        .Where(i => i.Selected)
                        .Select(i => i.Value)
                );

                if (string.IsNullOrEmpty(selectedIDs))
                    selectedIDs = "0";   // All commodities
                else
                    //selectedIDs = "'" + selectedIDs + "'";
                    cmd.Parameters.AddWithValue("@Hired_Type", selectedIDs);

                string selectedIDStorage_Types = string.Join(",",
                        ddlStorageType.Items.Cast<ListItem>()
                        .Where(i => i.Selected)
                        .Select(i => i.Value)
                );

                if (string.IsNullOrEmpty(selectedIDStorage_Types))
                    selectedIDStorage_Types = "0";   // All commodities
                else
                    //selectedIDs = "'" + selectedIDs + "'";
                    cmd.Parameters.AddWithValue("@Storage_Type", selectedIDStorage_Types);

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                            GridView1.DataSource = dt;
                            GridView1.DataBind();

                            GridView1.FooterRow.Style.Add("text-align", "left");
                            GridView1.FooterRow.Cells[3].Text = "Total";
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("GodownCapacity")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[22].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[23].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("VancatCapcity")).ToString("#,##0.00");
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }

}