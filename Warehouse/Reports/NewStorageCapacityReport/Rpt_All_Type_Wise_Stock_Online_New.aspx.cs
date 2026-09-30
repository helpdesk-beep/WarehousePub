using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SRV_Storage_Reports_Inspenctions_Rpt_All_Type_Wise_Stock_Online_New : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    decimal qtyTotal = 0;
    decimal grQtyTotal = 0;
    decimal qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal qtyTotal3 = 0;
    decimal qtyTotal4 = 0;
    decimal qtyTotal5 = 0;
    decimal qtyTotal6 = 0;
    decimal qtyTotal7 = 0;
    decimal qtyTotal8 = 0;
    decimal qtyTotal9 = 0;
    decimal qtyTotal10 = 0;
    decimal qtyTotal11 = 0;
    decimal qtyTotal12 = 0;
    decimal qtyTotal13 = 0;
    decimal qtyTotal14 = 0;
    decimal qtyTotal15 = 0;
    decimal qtyTotal16 = 0;

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;
    decimal grQtyTotal4 = 0;
    decimal grQtyTotal5 = 0;
    decimal grQtyTotal6 = 0;
    decimal grQtyTotal7 = 0;
    decimal grQtyTotal8 = 0;
    decimal grQtyTotal9 = 0;
    decimal grQtyTotal10 = 0;
    decimal grQtyTotal11 = 0;
    decimal grQtyTotal12 = 0;
    decimal grQtyTotal13 = 0;
    decimal grQtyTotal14 = 0;
    decimal grQtyTotal15 = 0;
    decimal grQtyTotal16 = 0;
    string storid = "0";
    int rowIndex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //fillComodity();
            //fillgrid();
            filDivision();
            //fillGodown();
            fillCropYear();
            GetCommodityType();
            GetDepositorType();
            fillGodownType();
            fillDivision();

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
        //fillgrid();
        if (ddldivision.SelectedValue == "All")
        {
            fillDivision();
        }
        else if (ddldivision.SelectedValue != "All" & ddldistrict.SelectedValue == "All")
        {
            fillgrid();
        }
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
        divregion.Visible = false;
        filBranch();
        //if (ddldistrict.SelectedValue == "All")
        //{
        //    fillgrid();
        //}
        //else
        //{
        //    fillDistrictgrid();
        //}
        if (ddldivision.SelectedValue == "All")
        {
            fillDivision();
        }
        else if (ddldivision.SelectedValue != "All" & ddldistrict.SelectedValue == "All")
        {
            fillgrid();
        }
        else if (ddldistrict.SelectedValue != "All" & ddlbranch.SelectedValue == "All")
        {
            fillDistrictgrid();
        }
        //else if (ddlbranch.SelectedValue != "All")
        //{
        //    fillGodowngrid();
        //}

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
                ddlgodowntype.Items.Insert(0, "All");
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
        fillGodown();
        //fillgrid();
        //fillGodowngrid();
        if (ddldivision.SelectedValue == "All")
        {
            fillDivision();
        }
        else if (ddldivision.SelectedValue != "All" & ddldistrict.SelectedValue == "All")
        {
            fillgrid();
        }
        else if (ddldistrict.SelectedValue != "All" & ddlbranch.SelectedValue == "All")
        {
            fillDistrictgrid();
        }
        else if (ddlbranch.SelectedValue != "All")
        {
            fillGodowngrid();
        }
    }
    private void fillGodown()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 Where BranchID ='" + ddlbranch.SelectedValue.ToString() + "' And Hired_Type ='" + ddlgodowntype.SelectedItem.Text + "' Order By Godown_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodown.Items.Clear();
                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
                ddlGodown.Items.Insert(0, "All");
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
    public void GetCommodityType()
    {
        string qry = "";
        qry = "Select Comm_Group_id,Group_name from tbl_MetaData_STORAGE_COMMODITY_Group Order By Group_name ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlCommoditytype.DataSource = ds.Tables[0];
            ddlCommoditytype.DataTextField = "Group_name";
            ddlCommoditytype.DataValueField = "Comm_Group_id";
            ddlCommoditytype.DataBind();
            ddlCommoditytype.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void GetCommodity()
    {
        string qry = "";
        qry = "select Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY Where  Rep_Grp_Code='" + ddlCommoditytype.SelectedValue + "' order by Commodity_Name asc";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    private void GetDepositorType()
    {
        string qry = "";
        qry = "select Depositor_Type,Depositor_Type from tbl_MetaData_Depositor_Type";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDepositorType.DataSource = ds.Tables[0];
            ddlDepositorType.DataTextField = "Depositor_Type";
            ddlDepositorType.DataValueField = "Depositor_Type";
            ddlDepositorType.DataBind();
            ddlDepositorType.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    private void GetDepositor()
    {
        if (ddlDepositorType.SelectedItem.Text == "Institution")
        {
            //For Institution
            string query2 = "";
            query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','4679','181')";

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
                //ddlDepositor.SelectedValue = "129";
            }
            //For Institution

        }
        if (ddlDepositorType.SelectedItem.Text == "Govt. Agencies")
        {
            //For Institution
            string query2 = "";
            query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('10535','15478')";

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
                //ddlDepositor.SelectedValue = "129";
            }
            //For Institution

        }
        else if (ddlDepositorType.SelectedItem.Text == "Jabti")
        {
            //For Institution
            string query2 = "";
            query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE DepositorType_ID ='" + ddlDepositorType.SelectedValue + "'";

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
                //ddlDepositor.SelectedValue = "129";
            }
            //For Institution

        }
        else
        {
            string depositer = ddlDepositorType.SelectedValue.ToString().Trim();
            string qry = "";
            qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE DepositorType_ID ='" + ddlDepositorType.SelectedValue + "' AND BranchId='" + ddlbranch.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepositor.DataSource = ds.Tables[0];
                ddlDepositor.DataTextField = "Depositor_Name";
                ddlDepositor.DataValueField = "Depositor_ID";
                ddlDepositor.DataBind();
                ddlDepositor.Items.Insert(0, new ListItem("All", "0"));
            }
        }
    }
    protected void ddlDepositorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepositor();
        if (ddldivision.SelectedValue == "All")
        {
            fillDivision();
        }
        else if (ddldivision.SelectedValue != "All" & ddldistrict.SelectedValue == "All")
        {
            fillgrid();
        }
        else if (ddldistrict.SelectedValue != "All" & ddlbranch.SelectedValue == "All")
        {
            fillDistrictgrid();
        }
        else if (ddlbranch.SelectedValue != "All")
        {
            fillGodowngrid();
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
    protected void fillDivision()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Region_Wise_Stock_Position_For_All_Type", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                if (ddlCommoditytype.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Comm_Group_id", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Comm_Group_id", ddlCommoditytype.SelectedValue);
                }
                if (ddlcommodity.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
                }
                if (ddlDepositorType.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Depositor_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Depositor_Type", ddlDepositorType.SelectedItem.ToString());
                }
                if (ddlDepositor.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Depositor_ID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Depositor_ID", ddlDepositor.SelectedValue);
                }
                if (ddlgodowntype.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", ddlgodowntype.SelectedValue);
                }
                if (ddlGodown.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Godown_ID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue);
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
                            grddivision.DataSource = dt;
                            grddivision.DataBind();
                            divregion.Visible = false;
                            divdistrict.Visible = false;
                            DivGodown.Visible = false;
                            divdivision.Visible = true;
                            grddivision.FooterRow.Style.Add("text-align", "Right");
                            grddivision.FooterRow.Cells[2].Text = "Total";
                            grddivision.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString("#,##0.00");
                        }
                        else
                        {
                            grddivision.DataSource = null;
                            grddivision.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Region_Wise_Stock_Entry_by_BM", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                if (ddlCommoditytype.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Comm_Group_id", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Comm_Group_id", ddlCommoditytype.SelectedValue);
                }
                if (ddlcommodity.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
                }
                if (ddlDepositorType.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Depositor_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Depositor_Type", ddlDepositorType.SelectedItem.ToString());
                }
                if (ddlDepositor.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Depositor_ID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Depositor_ID", ddlDepositor.SelectedValue);
                }

                if (ddlgodowntype.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", ddlgodowntype.SelectedValue);
                }
                if (ddlGodown.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Godown_ID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue);
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
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            divregion.Visible = true;
                            divdistrict.Visible = false;
                            DivGodown.Visible = false;
                            divdivision.Visible = false;
                            GridView1.FooterRow.Style.Add("text-align", "Right");
                            GridView1.FooterRow.Cells[2].Text = "Total";
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString("#,##0.00");
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
    protected void fillDistrictgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_District_Wise_Stock_Entry_by_BM", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddldistrict.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@District_Id", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@District_Id", ddldistrict.SelectedValue);
                }
                if (ddlcropyear.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@CropYear", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CropYear", ddlcropyear.SelectedValue);
                }
                if (ddlCommoditytype.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Comm_Group_id", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Comm_Group_id", ddlCommoditytype.SelectedValue);
                }
                if (ddlcommodity.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
                }
                if (ddlDepositorType.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Depositor_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Depositor_Type", ddlDepositorType.SelectedItem.ToString());
                }
                if (ddlDepositor.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Depositor_ID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Depositor_ID", ddlDepositor.SelectedValue);
                }

                if (ddlgodowntype.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", ddlgodowntype.SelectedValue);
                }
                if (ddlGodown.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Godown_ID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue);
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
                            GridView2.DataSource = dt;
                            GridView2.DataBind();
                            divdistrict.Visible = true;
                            divregion.Visible = false;
                            DivGodown.Visible = false;
                            divdivision.Visible = false;
                            GridView2.FooterRow.Style.Add("text-align", "left");
                            GridView2.FooterRow.Cells[2].Text = "Total";
                            GridView2.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString("#,##0.00");
                        }
                        else
                        {
                            GridView2.DataSource = null;
                            GridView2.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void fillGodowngrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Branch_Wise_Stock_Entry_by_BM", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddldistrict.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@District_Id", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@District_Id", ddldistrict.SelectedValue);
                }
                if (ddlbranch.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Branch_Id", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Branch_Id", ddlbranch.SelectedValue);
                }
                if (ddlcropyear.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@CropYear", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CropYear", ddlcropyear.SelectedValue);
                }
                if (ddlCommoditytype.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Comm_Group_id", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Comm_Group_id", ddlCommoditytype.SelectedValue);
                }
                if (ddlcommodity.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
                }
                if (ddlDepositorType.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Depositor_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Depositor_Type", ddlDepositorType.SelectedItem.ToString());
                }
                if (ddlDepositor.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Depositor_ID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Depositor_ID", ddlDepositor.SelectedValue);
                }

                if (ddlgodowntype.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", ddlgodowntype.SelectedValue);
                }
                if (ddlGodown.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Godown_ID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue);
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
                            grdgdn.DataSource = dt;
                            grdgdn.DataBind();
                            divdistrict.Visible = false;
                            divregion.Visible = false;
                            DivGodown.Visible = true;
                            divdivision.Visible = false;
                            grdgdn.FooterRow.Style.Add("text-align", "left");
                            grdgdn.FooterRow.Cells[2].Text = "Total";
                            grdgdn.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString("#,##0.00");
                        }
                        else
                        {
                            grdgdn.DataSource = null;
                            grdgdn.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "District_Id").ToString());
            decimal tmpTotal16 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());

            qtyTotal16 += tmpTotal16;
            grQtyTotal16 += tmpTotal16;


        }
    }
    protected void GridView1_OnRowCreated(object sender, GridViewRowEventArgs e)
    {

        bool newRow = false;

        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "District_Id") != null))
        {
            if (storid != Convert.ToString(DataBinder.Eval(e.Row.DataItem, "District_Id").ToString()))
                newRow = true;
        }
        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "District_Id") == null))
        {
            newRow = true;
            rowIndex = 0;
        }
        if (newRow)
        {
            GridView GridView1 = (GridView)sender;
            GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            NewTotalRow.Font.Bold = true;
            // NewTotalRow.BackColor = System.Drawing.Color.Gray;
            NewTotalRow.ForeColor = System.Drawing.Color.Black;
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Sub Total";
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.ColumnSpan = 3;
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal16.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal = 0;
            qtyTotal16 = 0;

        }


    }
    protected void ddlCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillgrid();
        if (ddldivision.SelectedValue == "All")
        {
            fillDivision();
        }
        else if (ddldivision.SelectedValue != "All" & ddldistrict.SelectedValue == "All")
        {
            fillgrid();
        }
        else if (ddldistrict.SelectedValue != "All" & ddlbranch.SelectedValue == "All")
        {
            fillDistrictgrid();
        }
        else if (ddlbranch.SelectedValue != "All")
        {
            fillGodowngrid();
        }
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedValue == "All")
        {
            fillDivision();
        }
        else if (ddldivision.SelectedValue != "All" & ddldistrict.SelectedValue == "All")
        {
            fillgrid();
        }
        else if (ddldistrict.SelectedValue != "All" & ddlbranch.SelectedValue == "All")
        {
            fillDistrictgrid();
        }
        else if (ddlbranch.SelectedValue != "All")
        {
            fillGodowngrid();
        }
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedValue == "All")
        {
            fillDivision();
        }
        else if (ddldivision.SelectedValue != "All" & ddldistrict.SelectedValue == "All")
        {
            fillgrid();
        }
        else if (ddldistrict.SelectedValue != "All" & ddlbranch.SelectedValue == "All")
        {
            fillDistrictgrid();
        }
        else if (ddlbranch.SelectedValue != "All")
        {
            fillGodowngrid();
        }
        //fillgrid();
        //fillGodowngrid();
    }
    protected void ddlcropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedValue == "All")
        {
            fillDivision();
        }
        else if (ddldivision.SelectedValue != "All" & ddldistrict.SelectedValue == "All")
        {
            fillgrid();
        }
        else if (ddldistrict.SelectedValue != "All" & ddlbranch.SelectedValue == "All")
        {
            fillDistrictgrid();
        }
        else if (ddlbranch.SelectedValue != "All")
        {
            fillGodowngrid();
        }
        //fillgrid();
        //fillGodowngrid();
    }
    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedValue == "All")
        {
            fillDivision();
        }
        else if (ddldivision.SelectedValue != "All" & ddldistrict.SelectedValue == "All")
        {
            fillgrid();
        }
        else if (ddldistrict.SelectedValue != "All" & ddlbranch.SelectedValue == "All")
        {
            fillDistrictgrid();
        }
        else if (ddlbranch.SelectedValue != "All")
        {
            fillGodowngrid();
        }
        //fillgrid();
        //fillGodowngrid();
    }
    protected void ddlCommoditytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCommodity();
        if (ddldivision.SelectedValue == "All")
        {
            fillDivision();
        }
        else if (ddldivision.SelectedValue != "All" & ddldistrict.SelectedValue == "All")
        {
            fillgrid();
        }
        else if (ddldistrict.SelectedValue != "All" & ddlbranch.SelectedValue == "All")
        {
            fillDistrictgrid();
        }
        else if (ddlbranch.SelectedValue != "All")
        {
            fillGodowngrid();
        }
        //fillgrid();
        //fillGodowngrid();
    }
    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedValue == "All")
        {
            fillDivision();
        }
        else if (ddldivision.SelectedValue != "All" & ddldistrict.SelectedValue == "All")
        {
            fillgrid();
        }
        else if (ddldistrict.SelectedValue != "All" & ddlbranch.SelectedValue == "All")
        {
            fillDistrictgrid();
        }
        else if (ddlbranch.SelectedValue != "All")
        {
            fillGodowngrid();
        }
        //fillgrid();
        //fillGodowngrid();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BranchId").ToString());
            decimal tmpTotal16 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());
            qtyTotal16 += tmpTotal16;
            grQtyTotal16 += tmpTotal16;
        }
    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;

        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "BranchId") != null))
        {
            if (storid != Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BranchId").ToString()))
                newRow = true;
        }
        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "BranchId") == null))
        {
            newRow = true;
            rowIndex = 0;
        }
        if (newRow)
        {
            GridView GridView1 = (GridView)sender;
            GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            NewTotalRow.Font.Bold = true;
            // NewTotalRow.BackColor = System.Drawing.Color.Gray;
            NewTotalRow.ForeColor = System.Drawing.Color.Black;
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Sub Total";
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.ColumnSpan = 3;
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal16.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal = 0;
            qtyTotal16 = 0;

        }
    }

    protected void grdgdn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Godown_ID").ToString());
            decimal tmpTotal16 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());

            qtyTotal16 += tmpTotal16;

            grQtyTotal16 += tmpTotal16;


        }
    }

    protected void grdgdn_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;

        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "Godown_ID") != null))
        {
            if (storid != Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Godown_ID").ToString()))
                newRow = true;
        }
        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "Godown_ID") == null))
        {
            newRow = true;
            rowIndex = 0;
        }
        if (newRow)
        {
            GridView GridView1 = (GridView)sender;
            GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            NewTotalRow.Font.Bold = true;
            // NewTotalRow.BackColor = System.Drawing.Color.Gray;
            NewTotalRow.ForeColor = System.Drawing.Color.Black;
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Sub Total";
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.ColumnSpan = 3;
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal16.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal = 0;
            qtyTotal16 = 0;

        }
    }

    protected void grddivision_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Region_ID").ToString());
            decimal tmpTotal16 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());

            qtyTotal16 += tmpTotal16;
            grQtyTotal16 += tmpTotal16;


        }
    }

    protected void grddivision_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;

        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "Region_ID") != null))
        {
            if (storid != Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Region_ID").ToString()))
                newRow = true;
        }
        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "Region_ID") == null))
        {
            newRow = true;
            rowIndex = 0;
        }
        if (newRow)
        {
            GridView GridView1 = (GridView)sender;
            GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            NewTotalRow.Font.Bold = true;
            // NewTotalRow.BackColor = System.Drawing.Color.Gray;
            NewTotalRow.ForeColor = System.Drawing.Color.Black;
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Sub Total";
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.ColumnSpan = 3;
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal16.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal = 0;
            qtyTotal16 = 0;

        }
    }
}