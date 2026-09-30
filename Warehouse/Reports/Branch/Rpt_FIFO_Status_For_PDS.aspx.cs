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

public partial class Reports_Branch_Rpt_Rpt_FIFO_Status_For_PDS : System.Web.UI.Page
{
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
            if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
            {

                fillgrid();
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_WHR_Wise_Data_For_FIFO_Policy", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    int Storage_Value = 0;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet dt = new DataSet())
                    {
                        sda.Fill(dt);
                        if (dt.Tables[0].Rows.Count > 0)
                        {
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
                            gdstackdetail.DataSource = dt;
                            gdstackdetail.DataBind();
                            // gdstackdetail.Caption = @"<b style=""font-weight: bold;""> M.P. Warehousing & Logistics Corporarion" + "</br> " + "Storage Charges Bill(After August)";
                            lblNoofAC.Text = dt.Tables[0].Rows.Count.ToString();
                            gdstackdetail.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            gdstackdetail.FooterRow.Cells[5].Text = "Total";
                            gdstackdetail.FooterRow.Cells[6].Text = dt.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Qty")).ToString();
                            gdstackdetail.FooterRow.Cells[7].Text = dt.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("FIFOFrizwedStock")).ToString();
                            gdstackdetail.FooterRow.Cells[8].Text = dt.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("AvailQty")).ToString();

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

    }
}