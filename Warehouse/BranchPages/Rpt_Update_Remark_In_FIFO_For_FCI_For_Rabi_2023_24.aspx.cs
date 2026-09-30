using System;
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
using System.Drawing;
using System.IO;

public partial class BranchPages_Rpt_Update_Remark_In_FIFO_For_FCI_For_Rabi_2023_24 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd;
    DataTable dt = new DataTable();
    private object localIP;

    int gridcount;
    int ZeroCount;
    int valuecount;
    int rownumber = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                GetBranchData();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void GetBranchData()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Details_For_FIFO_Rabi_2023_24", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());

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
                            // Session["BranchName"] = ddlbranch.SelectedItem.ToString();
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
                            //GridView1.DataSource = dt;
                            //GridView1.DataBind();
                            GridView2.DataSource = dt;
                            GridView2.DataBind();
                            showgrid.Visible = true;
                            // GridView1.Caption = @"<b style=""font-weight: bold; text-align:center;""> M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "गोदाम वार FIFO नीति से स्टॉक के उठाव  की स्थिति" + "</br> " + "शाखा का नाम" + "  -   " + ddlbranch.SelectedItem.ToString();
                            //GridView1.Columns[1].Visible = false;
                            //// GridView1.columns.RemoveAt(1);
                            //lblsyncdate.Text = dt.Rows[0]["StockPositionAsOnDate"].ToString();

                            //GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;color:red;", "right");
                            //GridView1.FooterRow.Cells[1].Text = "Total";
                            //GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlBalance")).ToString();

                        }
                        else
                        {
                            showgrid.Visible = false;

                            GridView2.DataSource = null;
                            GridView2.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
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

    public int GenerateRandomNo()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/Warehouse/StatePages/Rpt_Branch_Wise_WHR_Wise_FIFO_Details.aspx','_newtab');", true);
        }
    }
}