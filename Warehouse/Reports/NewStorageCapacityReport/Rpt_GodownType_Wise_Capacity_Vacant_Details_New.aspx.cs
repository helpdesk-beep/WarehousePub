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

public partial class SRV_Storage_Reports_Inspenctions_Rpt_GodownType_Wise_Capacity_Vacant_Details_New : System.Web.UI.Page
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

        //if (Session["UserName"].ToString() == "MPSWLC")
        //{
        if (!IsPostBack)
        {
            labelName.Text = DateTime.Now.ToString();
            FillStorageType();
            FillGodownType();
            //fillgrid();
        }
        //}

    }
    private void FillStorageType()
    {

        string query = " select distinct Storage_Type from tbl_MetaData_GODOWN_2018";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlstorageType.DataSource = ds.Tables[0];
            ddlstorageType.DataTextField = "Storage_Type";
            ddlstorageType.DataValueField = "Storage_Type";
            ddlstorageType.DataBind();
            //ddlstorageType.Items.Insert(0, "--Select--");
        }

    }
    private void FillGodownType()
    {

        string query = "select distinct Hired_Type from tbl_MetaData_GODOWN_2018";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGodownType.DataSource = ds.Tables[0];
            ddlGodownType.DataTextField = "Hired_Type";
            ddlGodownType.DataValueField = "Hired_Type";
            ddlGodownType.DataBind();
            //ddlGodownType.Items.Insert(0, "--Select--");
        }

    }
    protected void fillgrid()
    {
        Decimal opcloavg = 0;
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_hired_Type_Wise_Capacity_And_Vacant_Space_New", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //if (ddlstorageType.SelectedValue == "--Select--")
                //{
                //    cmd.Parameters.AddWithValue("@Storage_Type", "0");
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@Storage_Type", ddlstorageType.SelectedValue);
                //}
                //if (ddlGodownType.SelectedValue == "--Select--")
                //{
                //    cmd.Parameters.AddWithValue("@Hired_Type", "0");
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@Hired_Type", ddlGodownType.SelectedValue);
                //}

                string selectedIDStorage_Type = string.Join(",",
                        ddlstorageType.Items.Cast<ListItem>()
                        .Where(i => i.Selected)
                        .Select(i => i.Value)
                );

                if (string.IsNullOrEmpty(selectedIDStorage_Type))
                    selectedIDStorage_Type = "0";   // All commodities
                else
                    //selectedIDs = "'" + selectedIDs + "'";
                    cmd.Parameters.AddWithValue("@Storage_Type", selectedIDStorage_Type);


                string selectedIDGodownType = string.Join(",",
                        ddlGodownType.Items.Cast<ListItem>()
                        .Where(i => i.Selected)
                        .Select(i => i.Value)
                );

                if (string.IsNullOrEmpty(selectedIDGodownType))
                    selectedIDGodownType = "0";   // All commodities
                else
                    //selectedIDs = "'" + selectedIDs + "'";
                    cmd.Parameters.AddWithValue("@Hired_Type", selectedIDGodownType);


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
                            GridView1.Caption = @"<b style=""font-weight: bold; text-align:center;""> M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "Godown Wise Capacity";//+ "</br> " + "Region Name" + "  -   " + dt.Rows[0]["Region"].ToString()
                            //GridView1.Columns[1].Visible = false;
                            // GridView1.columns.RemoveAt(1);
                            GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GridView1.FooterRow.Cells[4].Text = "Total";
                            //GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalGodown")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("GodownCapacity")).ToString();
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Quantityofstockstoredinwarehouse")).ToString();
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("currentlyvacantcapacity")).ToString();

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
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Welcome.aspx");
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void ddlstorageType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}