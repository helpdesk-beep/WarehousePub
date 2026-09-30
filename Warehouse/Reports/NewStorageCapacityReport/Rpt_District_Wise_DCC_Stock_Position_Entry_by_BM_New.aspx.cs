using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;

public partial class SRV_Storage_Reports_Inspenctions_Rpt_District_Wise_DCC_Stock_Position_Entry_by_BM_New : System.Web.UI.Page
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
            DateTime now = DateTime.Now;
            string date = now.GetDateTimeFormats('d')[0];
            string time = now.GetDateTimeFormats('t')[0];
            //lbldate.Text= date+'-'+ time;
            fillRegion();
            fillCropyear();
            fillComodity();
            fillgrid();
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
                //ddlComodity.Items.Insert(0, "--Select--");
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

    private void fillRegion()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "select Distinct Region_ID,Regionnm from tbl_MetaData_DISTRICT order by Region_ID";
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

    private void fillCropyear()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "select distinct CropYear from FAQ_NON_FAQ_DCC_WHR_Wise_Stock_position where DCC_Stock>0 order by CropYear";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcropyear.Items.Clear();
                ddlcropyear.DataSource = ds.Tables[0];
                ddlcropyear.DataTextField = "CropYear";
                ddlcropyear.DataValueField = "CropYear";
                ddlcropyear.DataBind();
                ddlcropyear.Items.Insert(0, "--Select--");
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
                using (SqlCommand cmd = new SqlCommand("Get_District_Wise_DCC_Stock_Position_For_State_New", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (ddlRegion.SelectedValue == "--Select--")
                    {
                        cmd.Parameters.AddWithValue("@RegionID", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RegionID", ddlRegion.SelectedValue);
                    }
                    if (ddlcropyear.SelectedValue == "--Select--")
                    {
                        cmd.Parameters.AddWithValue("@CropYear", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CropYear", ddlcropyear.SelectedValue);
                    }
                    //if (ddlComodity.SelectedValue == "--Select--")
                    //{
                    //    cmd.Parameters.AddWithValue("@CommodityID", 0);
                    //}
                    //else
                    //{
                    //    cmd.Parameters.AddWithValue("@CommodityID", ddlComodity.SelectedValue);
                    //}
                    string selectedIDs = string.Join(",",
                            ddlComodity.Items.Cast<ListItem>()
                            .Where(i => i.Selected)
                            .Select(i => i.Value)
                    );

                    if (string.IsNullOrEmpty(selectedIDs))
                        selectedIDs = "0";   // All commodities
                    else
                        cmd.Parameters.Add("@CommodityIDs", SqlDbType.VarChar).Value = selectedIDs;
                    //cmd.Parameters.AddWithValue("@CommodityIDs", selectedIDs);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + " - " + "Stock Position in(M.T.)";
                                GridView1.DataSource = dt;
                                GridView1.DataBind();
                                GridView1.FooterRow.Style.Add("text-align", "Right");
                                GridView1.FooterRow.Cells[3].Text = "Total";
                                GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("DCCStockEntryByBM")).ToString("#,##0.00");
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

    protected void ddlComodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void ddlcropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}