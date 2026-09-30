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
using AjaxControlToolkit;
using System.Linq;

public partial class StatePages_Rpt_StorageAndREnt_Bill_Pendency : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd = null;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }
    protected void fillgrid()
    {
        string CropYear = ddlfinancial.SelectedValue.ToString();
        con.Open();
        SqlCommand cmd = new SqlCommand("Storage_And_rent_Bill_Pendding_Status", con);
        cmd.Parameters.AddWithValue("@Financial_Year", CropYear);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 100;
        cmd.ExecuteNonQuery();
        //cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter();
        da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GV_StockReport.DataSource = dt;
            GV_StockReport.DataBind();
            GV_StockReport.FooterRow.Style.Add("text-align", "center");
            GV_StockReport.FooterRow.Cells[3].Text = "Total";
            GV_StockReport.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalSCBillGenerated")).ToString();
            GV_StockReport.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalRentBillGenerated")).ToString();
            GV_StockReport.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingRentBillForGenerate")).ToString();
        }
        else
        {
            GV_StockReport.DataSource = null;
            GV_StockReport.DataBind();
        }
    }

    protected void ddlfinancial_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}