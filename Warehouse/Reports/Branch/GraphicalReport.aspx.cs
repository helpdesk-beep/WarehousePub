using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class Reports_Branch_GraphicalReport : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            bindchart();
            fillGodnList();
            bindchartcomm();
        }
    }

    protected void bindchart()
    {
        string query = "SELECT mg.Godown_Name, cast(ISNULL(mg.Godown_Capacity,0) as decimal(18,2)) as Godown_Capacity,(round((SELECT ISNULL(SUM(RecQty) - SUM(DelQty), 0) AS Expr1 FROM View_WHRcurrentstock WHERE (BranchID = mg.BranchID) AND (Godown_ID = mg.Godown_ID))*100/ NULLIF(cast(ISNULL(mg.Godown_Capacity,0) as decimal(18,2)),0),2))  as utilization FROM  tbl_MetaData_GODOWN AS mg where mg.Remarks='Y' and mg.BranchID='" + Session["BranchId"].ToString() + "'";

        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        DataTable dt = new DataTable();
        dt = ds.Tables[0];

        string category = "";
        if (dt.Rows.Count > 0)
        {
            decimal[] values = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                category = category + "," + dt.Rows[i]["Godown_Name"].ToString();
                values[i] = Convert.ToDecimal(dt.Rows[i]["utilization"]);
            }

            BarChart1.CategoriesAxis = category.Remove(0, 1);

            BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values, BarColor = "#2fd1f9", Name = "Godown" });
        }
    }

    private void fillGodnList()
    {
        if (Session["Depot_DistID"] != null)
        {
            string query = " SELECT Godown_ID,Godown_Name  FROM tbl_MetaData_GODOWN where DistrictId ='" + Session["Depot_DistID"].ToString() + "' and  BranchId  ='" + Session["BranchId"].ToString() + "' and Remarks='Y' order by Godown_Name Asc";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodown.DataSource = ds.Tables[0];
                ddlgodown.DataTextField = "Godown_Name";
                ddlgodown.DataValueField = "Godown_ID";
                ddlgodown.DataBind();
                
            }
            else
            {
                ddlgodown.Items.Insert(0, "--Select--");
            }
        }
    }

    protected void bindchartcomm()
    {
        string query = "SELECT [Depotid],[Commodity_Id],[BranchID],[Godown_ID],[Commodity_Name],sum([RecQty])RecQTy,sum([DelQty])DelQTy,sum([RecBags])RecBags,(sum([RecQty])-sum([DelQty]))avlqty,sum([DelBags])DelBags FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] where BranchID='" + Session["BranchId"].ToString() + "' and Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' group by [Depotid],[Commodity_Id],[BranchID],[Godown_ID],[Commodity_Name]";

        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        DataTable dt = new DataTable();
        dt = ds.Tables[0];

        string category = "";
        if (dt.Rows.Count > 0)
        {
            decimal[] values = new decimal[dt.Rows.Count];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    category = category + "," + dt.Rows[i]["Commodity_Name"].ToString();
                    values[i] = Convert.ToDecimal(dt.Rows[i]["avlqty"]);
                }

                AreaChart1.CategoriesAxis = category.Remove(0, 1);

                AreaChart1.Series.Add(new AjaxControlToolkit.AreaChartSeries { Data = values, AreaColor = "#3dc0f4", Name = "Commodity" });
            }
        }
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string query = "SELECT [Depotid],[Commodity_Id],[BranchID],[Godown_ID],[Commodity_Name],sum([RecQty])RecQTy,sum([DelQty])DelQTy,sum([RecBags])RecBags,(sum([RecQty])-sum([DelQty]))avlqty,sum([DelBags])DelBags FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] where BranchID='" + Session["BranchId"].ToString() + "' and Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' group by [Depotid],[Commodity_Id],[BranchID],[Godown_ID],[Commodity_Name]";

        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        DataTable dt = new DataTable();
        dt = ds.Tables[0];

        string category = "";
        if (dt.Rows.Count > 0)
        {
            decimal[] values = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                category = category + "," + dt.Rows[i]["Commodity_Name"].ToString();
                values[i] = Convert.ToDecimal(dt.Rows[i]["avlqty"]);
            }

            AreaChart1.CategoriesAxis = category.Remove(0, 1);

            AreaChart1.Series.Add(new AjaxControlToolkit.AreaChartSeries { Data = values, AreaColor = "#3dc0f4", Name = "Commodity" });
        }
    }
}
