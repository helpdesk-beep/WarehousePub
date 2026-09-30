using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Reports_NewStorageCapacityReport_Rpt_District_Wise_Qty_Available_Last_10_Year_Date_Wise_New_New : System.Web.UI.Page
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
            fillComodity();
            fillDepositor();
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
    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (txtpaymentdate.Text == "")
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Date!....')", true);
            txtpaymentdate.Focus();
            return;
        }
        Session["Date"] = txtpaymentdate.Text.ToString();
        Session["Commodity"] = ddlComodity.SelectedValue.ToString();

        fillgrid();

    }
    private void fillComodity()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
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

    private void fillDepositor()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679','181','15478')";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepositor.Items.Clear();
                ddlDepositor.DataSource = ds.Tables[0];
                ddlDepositor.DataTextField = "Depositor_Name";
                ddlDepositor.DataValueField = "Depositor_ID";
                ddlDepositor.DataBind();
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
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Date_Wise_All_Crop_wise_Balance_Details_District_Wise_For_MPSCSC_New_New", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    string selectedIDs = string.Join(",",
                            ddlComodity.Items.Cast<ListItem>()
                            .Where(i => i.Selected)
                            .Select(i => i.Value)
                    );

                    if (string.IsNullOrEmpty(selectedIDs))
                        selectedIDs = "0";   // All commodities
                    else
                        //    selectedIDs = "'" + selectedIDs + "'";
                        //cmd.Parameters.AddWithValue("@CommodityIDs", selectedIDs);
                        cmd.Parameters.Add("@CommodityIDs", SqlDbType.VarChar).Value = selectedIDs;
                    string selectedDIDs = string.Join(",",
                            ddlDepositor.Items.Cast<ListItem>()
                            .Where(i => i.Selected)
                            .Select(i => i.Value)
                    );

                    if (string.IsNullOrEmpty(selectedIDs))
                        selectedIDs = "0";   // All commodities
                    else
                        cmd.Parameters.Add("@DepositorIDs", SqlDbType.VarChar).Value = selectedIDs;
                        //cmd.Parameters.AddWithValue("@DepositorIDs", selectedDIDs);
                    cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtpaymentdate.Text));
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
                                GridView1.FooterRow.Cells[1].Text = "Total";
                                GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("A")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("B")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("C")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("D")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("E")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("F")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("G")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("H")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("I")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString("#,##0.00");
                                Session["Date"] = txtpaymentdate.Text.ToString();
                                Session["Commodity"] = ddlComodity.SelectedValue.ToString();
                                Session["DepositorID"] = ddlDepositor.SelectedValue.ToString();
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
        catch (Exception ex)
        {
            string script = "alert('Error: " + ex.Message.Replace("'", "\\'") + "');";
            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script, true);
        }
        
    }
    protected void drpDwnCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void dllGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlComodity.SelectedValue != "--Select--")
            fillgrid();
    }
}