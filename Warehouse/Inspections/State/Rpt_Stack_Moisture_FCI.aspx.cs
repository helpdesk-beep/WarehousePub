using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class Inspections_State_Rpt_Stack_Moisture_FCI : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConstrMoisture"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime currDate = DateTime.Now.Date;
            DateTime prevDate = currDate.AddDays(-1);

            ViewState["CurrDate"] = currDate.ToString("dd-MM-yyyy");
            ViewState["PrevDate"] = prevDate.ToString("dd-MM-yyyy");

            BindReport();
        }
    }

    private void BindReport()
    {
        string query = @"
        
        DECLARE @CurrDate DATE = CAST(GETDATE() AS DATE)
DECLARE @PrevDate DATE = DATEADD(DAY, -1, @CurrDate)

SELECT 
    ROW_NUMBER() OVER (ORDER BY g.godown_Name) AS [S.No],
    g.godown_Name AS [Godown Name],
    -- Count of items created on or before yesterday
    ISNULL(SUM(CASE WHEN CAST(m.Create_on AS DATE) <= @PrevDate THEN 1 ELSE 0 END), 0) AS [Prev Stack],
    -- Count of items created on or before today
    ISNULL(SUM(CASE WHEN CAST(m.Create_on AS DATE) <= @CurrDate THEN 1 ELSE 0 END), 0) AS [Current Stack],
    
    -- TOTAL: Logic for Prev + Current (Summing both conditions)
    ISNULL(SUM(
        CASE WHEN CAST(m.Create_on AS DATE) <= @PrevDate THEN 1 ELSE 0 END + 
        CASE WHEN CAST(m.Create_on AS DATE) <= @CurrDate THEN 1 ELSE 0 END
    ), 0) AS [Total Stack],

    ISNULL(COUNT(m.Stack_ID), 0) AS [Moisture Entry],
    ISNULL(COUNT(DISTINCT m.Stack_ID), 0) AS [Submit To FCI]

FROM Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 g
INNER JOIN tbl_Stack_Wise_Moisture_Entry_By_BM m ON g.Godown_ID = m.Godown_ID
LEFT JOIN tbl_fci_message f ON m.Godown_ID = f.Godown_Id

GROUP BY g.godown_Name
ORDER BY g.godown_Name";

        SqlDataAdapter da = new SqlDataAdapter(query, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvReport.DataSource = dt;
        gvReport.DataBind();

        // 🔥 this is required
        gvReport.HeaderRow.DataBind();

        // Summary
        decimal totalStack = 0, moisture = 0, fci = 0;

        foreach (DataRow row in dt.Rows)
        {
            totalStack += Convert.ToDecimal(row["Total Stack"]);
            moisture += Convert.ToDecimal(row["Moisture Entry"]);
            fci += Convert.ToDecimal(row["Submit To FCI"]);
        }

        lblTotalStack.Text = totalStack.ToString();
        lblMoisture.Text = moisture.ToString();
        lblFCI.Text = fci.ToString();
    }

    protected void gvReport_PreRender(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count > 0)
        {
            gvReport.UseAccessibleHeader = true;
            gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=StackReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        gvReport.RenderControl(new System.Web.UI.HtmlTextWriter(Response.Output));
        Response.End();
    }

    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
        // Required for export
    }
}