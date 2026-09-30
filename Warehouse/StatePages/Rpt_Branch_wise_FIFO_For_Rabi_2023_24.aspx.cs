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

public partial class StatePages_Rpt_Branch_wise_FIFO_For_Rabi_2023_24 : System.Web.UI.Page
{


    decimal qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal qtyTotal3 = 0;

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;

    long storid = 0;
    int rowIndex = 1;


    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    int gridcount;
    int ZeroCount;
    int valuecount;
    int rownumber = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            labelName.Text = DateTime.Now.ToString();
            fillRegion();
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

    private void fillDistrict()
    {
        try
        {
            string query = "";

            query = "SELECT District_Id,District_Name FROM [tbl_MetaData_DISTRICT] where Region_ID='" + ddlRegion.SelectedValue + "' order by Regionnm asc";

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
                ddldistrict.Items.Insert(0, "--Select--");
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

    private void fillBranch()
    {
        try
        {
            string query = "";

            query = "SELECT BranchId,DepotName FROM tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "' order by DepotName asc";

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
                ddlbranch.Items.Insert(0, "--Select--");
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

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDistrict();
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranch();
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillGodown();
    }
    protected void tbnview_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Details_For_FIFO_Rabi_2023_24", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
                
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet dt = new DataSet())
                    {
                        int Storage_Value = 0;
                        sda.Fill(dt);
                        if (dt.Tables[0].Rows.Count > 0)
                        {
                            Session["BranchName"] = ddlbranch.SelectedItem.ToString();
                            gridcount = dt.Tables[0].Rows.Count;
                            foreach (DataRow dr in dt.Tables[0].Rows)
                            {
                                Storage_Value = Convert.ToInt32(dr["FIFOFrizwedStock"]);
                                if (Storage_Value == 0)
                                {
                                    ZeroCount = ZeroCount + 1;
                                }
                                else if (Storage_Value > 0)
                                {
                                    valuecount = valuecount + 1;
                                }
                                //valuecount = valuecount+Convert.ToInt32(dr["DeleveryDone"]);
                            }
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.Caption = @"<b style=""font-weight: bold; text-align:center;""> M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "गोदाम वार FIFO नीति से स्टॉक के उठाव  की स्थिति" + "</br> " + "शाखा का नाम" + "  -   " + ddlbranch.SelectedItem.ToString();
                            //GridView1.Columns[1].Visible = false;
                            //// GridView1.columns.RemoveAt(1);
                            //lblsyncdate.Text = dt.Rows[0]["StockPositionAsOnDate"].ToString();

                            //GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;color:red;", "right");
                            //GridView1.FooterRow.Cells[1].Text = "Total";
                            //GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlBalance")).ToString();

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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //  HiddenField hdnVerificationType = (HiddenField)e.Row.FindControl("hdnVerificationType");
            HiddenField hdndiffirence = (HiddenField)e.Row.FindControl("hdndiffirence");
            HiddenField hdnQty = (HiddenField)e.Row.FindControl("hdnQty");

            rownumber = rownumber + 1;
            string checkvalue = hdnQty.Value;
            if (gridcount == ZeroCount)
            {
                e.Row.BackColor = ColorTranslator.FromHtml("#FFFF00");//e9716b Yellow
            }
            else if (Convert.ToInt32(hdndiffirence.Value) > 0 && valuecount > 0)
            {
                e.Row.BackColor = ColorTranslator.FromHtml("#28b779");//e9716b Green
                valuecount = valuecount - 1;
            }
            else if (Convert.ToInt32(hdndiffirence.Value) == 0 && valuecount > 0)
            {
                e.Row.BackColor = ColorTranslator.FromHtml("#e9716b");//e9716b Red
            }
            else if (Convert.ToInt32(hdndiffirence.Value) == 0 && valuecount == 0)
            {
                e.Row.BackColor = ColorTranslator.FromHtml("#FFFF00");//e9716b yeloow
            }

        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    string Averg = DataBinder.Eval(e.Row.DataItem, "statuswhr").ToString();
        //    if (Averg == "Red")
        //    {
        //        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8C5BA");
        //        e.Row.Font.Bold = true;
        //    }
        //    else if (Averg == "Yellow")
        //    {
        //        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FAF8C1");
        //        e.Row.Font.Bold = true;
        //    }
        //    else if (Averg == "Grean")
        //    {
        //        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#C5EC92");
        //        e.Row.Font.Bold = true;
        //    }
        //}
    }
   
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

      
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://mpwarehousing.mp.gov.in/");
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Annexure_B")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GridView1.Rows[rowIndex];
            //Fetch value of Name.           
            string hdnBranchId = (row.FindControl("hdnBranchId") as HiddenField).Value;
            Session["BranchId"] = hdnBranchId.ToString();
            //Response.Redirect("/StatePages/Rpt_FIFO_Status_For_PDS.aspx");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/Warehouse/StatePages/Rpt_Branch_Wise_WHR_Wise_FIFO_Details_For_Rabi_2023_24.aspx','_newtab');", true);
        }
    }
}