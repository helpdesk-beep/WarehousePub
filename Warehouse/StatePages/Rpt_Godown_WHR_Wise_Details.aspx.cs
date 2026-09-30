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

public partial class StatePages_Rpt_Godown_WHR_Wise_Details : System.Web.UI.Page
{
    decimal qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal qtyTotal3 = 0;
    decimal qtyTotal4 = 0;
    decimal qtyTotal5 = 0;

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;
    decimal grQtyTotal4 = 0;
    decimal grQtyTotal5 = 0;

    long storid = 0;
    int rowIndex = 1;
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            labelName.Text = DateTime.Now.ToString();
            FillCropYear();
            FillDepositor();
            fillGodowngrid();
        }
    }
    private void FillCropYear()
    {

        // string query = " select distinct Storage_Type from tbl_MetaData_GODOWN_2018";
        SqlCommand cmd = new SqlCommand("Get_CropYear_by_WHR_Relation", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlCropYear.DataSource = ds.Tables[0];
            ddlCropYear.DataTextField = "CropYear";
            ddlCropYear.DataValueField = "CropYear";
            ddlCropYear.DataBind();
            //ddlstorageType.Items.Insert(0, "All");
            ddlCropYear.Items.Insert(0, new ListItem("All", "0"));
        }

    }

    private void FillDepositor()
    {

         string query = "select distinct DepositorID,Depositor_Name from tbl_storage_Depositor_WHR_Relation where GodownID='" + Request.QueryString["GodownID"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldepositor.DataSource = ds.Tables[0];
            ddldepositor.DataTextField = "Depositor_Name";
            ddldepositor.DataValueField = "DepositorID";
            ddldepositor.DataBind();
            //ddlstorageType.Items.Insert(0, "All");
            ddldepositor.Items.Insert(0, new ListItem("All", "0"));
        }

    }
    protected void fillGodowngrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_WHR_Wise_Stock_position", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GodownID", Request.QueryString["GodownID"].ToString());
                if (ddlCropYear.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@CropYear", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedValue);
                }
                if (ddldepositor.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Depositorid", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Depositorid", ddldepositor.SelectedValue);
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
                            GrdGodown.DataSource = dt;
                            GrdGodown.DataBind();
                            //divdivision.Visible = false;
                            GrdGodown.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GrdGodown.FooterRow.Cells[5].Text = "Total";
                            GrdGodown.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalBags_Received")).ToString();
                            GrdGodown.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Qty_Received")).ToString();
                            GrdGodown.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("No_Of_Bags")).ToString();
                            GrdGodown.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Bags_Weight")).ToString();
                            GrdGodown.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<int>("AvailableBags")).ToString();
                            GrdGodown.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvailableQty")).ToString();
                        }
                        else
                        {
                            GrdGodown.DataSource = null;
                            GrdGodown.DataBind();
                        }
                    }
                }
            }
        }
    }


    //protected void GrdGodown_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //}

    //protected void GrdGodown_RowCreated(object sender, GridViewRowEventArgs e)
    //{

    //}
    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodowngrid();
    }

    protected void ddldepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodowngrid();
    }
}