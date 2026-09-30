using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_NewStorageCapacityReport_Godown_Wise_Rabi_Kharif_Multiple_CropYear_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCropYear();
            fillComodity();
        }
    }

    protected void BindCropYear()
    {
        List<string> cropYears = new List<string> {
        "2010-11", "2012-13", "2013-14", "2014-15", "2015-16",
        "2016-17", "2017-18", "2018-19", "2019-20", "2020-21",
        "2021-22", "2022-23", "2023-24", "2024-25", "2025-26"
    };

        ddlCropYear.DataSource = cropYears;
        // Removed DataTextField and DataValueField because the list is just strings
        ddlCropYear.DataBind();

        // Optional: Add a default 'Select' option
        // ddlCropYear.Items.Insert(0, new ListItem("-- Select Crop Year --", "0"));
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
    public DataTable GetGodownWiseReport(string cropYear, string Commodity, string asOnDate)
    {
        // 1. Get connection string from web.config
        string connString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(connString))
        {
            // 2. Define the Command and specify the Stored Procedure name
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Rabi_KHarif_Multiple", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. Add Parameters (Matching your SP variables exactly)
                cmd.Parameters.Add("@CropYearList", SqlDbType.NVarChar, 20).Value = cropYear;
                cmd.Parameters.Add("@AsOnDate", SqlDbType.Date).Value = asOnDate;
                cmd.Parameters.Add("@CommodityIdList", SqlDbType.NVarChar, 200).Value = Commodity;

                try
                {
                    con.Open();
                    // 4. Use SqlDataAdapter to fill the DataTable
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    // Handle or log the error as needed
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "showSweetAlert('Warning!', 'Something Went Wrong!', 'warning');", true);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        return dt;
    }
    private void fillComodity()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                {
                    string query = "";
                    //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
                    query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
                    SqlCommand cmd = new SqlCommand(query, con);
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
                        //ddlComodity.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "showSweetAlert('Warning!', 'No Record Found!', 'warning');", true);
                    }
                }
            }
        }
        catch (Exception)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "showSweetAlert('Error!', 'Something Went Wrong!', 'error');", true);
        }
    }
    private string GetSelectedItems(ListBox lst)
    {
        List<string> selectedItems = new List<string>();
        foreach (ListItem item in lst.Items)
        {
            if (item.Selected)
            {
                selectedItems.Add(item.Value);
            }
        }
        // Returns "1,2,5" or "2023-24,2024-25"
        return string.Join(",", selectedItems.ToArray());
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string years = GetSelectedItems(ddlCropYear);
        string commodities = GetSelectedItems(ddlComodity);
        string date = txtdate.Text;

        if (years != "0" && !string.IsNullOrEmpty(date))
        {
            DataTable reportData = GetGodownWiseReport(years, commodities, date);

            gvGodownReport.DataSource = reportData;
            gvGodownReport.DataBind();
        }
        else
        {
            // ScriptManager.RegisterStartupScript to show an alert if data is missing
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Reset all fields
        ddlCropYear.ClearSelection();
        ddlComodity.ClearSelection();
        txtdate.Text = string.Empty;
        gvGodownReport.DataSource = null;
        gvGodownReport.DataBind();

        // Redirect or refresh to clear validation states if needed
        Response.Redirect(Request.RawUrl);
    }
}