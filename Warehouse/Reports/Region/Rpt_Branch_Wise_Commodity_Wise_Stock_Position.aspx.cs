using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Globalization;

public partial class Reports_Region_Rpt_Branch_Wise_Commodity_Wise_Stock_Position : System.Web.UI.Page
{
    int qtyTotal = 0;
    int grQtyTotal = 0;
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
    decimal qtyTotal17 = 0;
    decimal qtyTotal18 = 0;

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
    decimal grQtyTotal17 = 0;
    decimal grQtyTotal18 = 0;

    long storid = 0;
    int rowIndex = 1;


    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetRegion();
            FillDistrict();
        }
    }

     private void GetRegion()
    {
        string strDist = "";
        strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Regionnm";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlregion.DataSource = ds.Tables[0];
            ddlregion.DataTextField = "Regionnm";
            ddlregion.DataValueField = "Region_ID";
            ddlregion.DataBind();
            ddlregion.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlregion.Items.Insert(0, "--Select--");
        }
    }

    private void FillDistrict()
    {
        string strDist = "";
        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT where Region_ID='" + ddlregion.SelectedValue + "' order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "--Select--");
        }
        else
        {
            ddldistrict.Items.Insert(0, "--Select--");
        }
    }

    //public void fillBranchDetails()
    //{
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        SqlCommand cmd = new SqlCommand("Fill_Branch", con);
    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@District_Id", ddldistrict.SelectedValue);
    //        con.Open();
    //        ddlbranch.DataSource = cmd.ExecuteReader();
    //        ddlbranch.DataTextField = "DepotName";
    //        ddlbranch.DataValueField = "BranchId";
    //        ddlbranch.DataBind();
    //        ddlbranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
    //        con.Close();
    //    }
    //}

    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDistrict();
    }
    //protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillBranchDetails();
    //}
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

    protected void fillgrid()
    {
        Decimal opcloavg = 0;
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Paddy_Acceptance_Wise_WHR_Details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Branch", ddlbranch.SelectedValue);

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
                           // GridView1.Caption = @"<b style=""font-weight: bold;""> Branch Wise Commodity Wise Stock Position" + "</br> " + "Region Name" + " - " + ddlregion.SelectedItem.ToString() + "   -  " + "District Name" + " - " + ddldistrict.SelectedItem.ToString() + "</br> " + "Crop Year" + " - " + ddlCropYear.SelectedItem.ToString() + "   -  " + "Commodity Name" + " - " + ddlcommodity.SelectedItem.ToString();
                            //GridView1.Columns[1].Visible = false;
                            // GridView1.columns.RemoveAt(1);

                        }
                        else
                        {
                            // btnUpdate.Visible = false;
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strDist = "";
        strDist = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + ddldistrict.SelectedValue + "' order by Depotname"; ;
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "BranchId";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, "--Select--");
        }
        else
        {
            ddldistrict.Items.Insert(0, "--Select--");
        }
    }
}