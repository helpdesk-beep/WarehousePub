using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

public partial class Region_Region_Reports_GodownWise_NCCF_Recieved_Payment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindReportGrid();
        }
    }

    private void BindReportGrid()
    {
        // Replace with your exact connection string name from your web.config file
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

        string query = @"
            Select 
                Dis.District_Name,
                MD.DepotName,
                MG.Godown_Name,
                SUM(SD.Net_Amount) As [Total Storage Charges Bill Amount],
                Count(SD.Bill_Number) AS [Total Storage Charges Bill],
                SUM(CR.PSS) AS [PSS Bill Amount],
                SUM(CR.PSF) AS [PSF Bill Amount],
                SUM(CR.TDS_Deduction) AS [TDS Deduction From NCCF],
                SUM(CR.Other_Deduction) AS [Other Deduction From NCCF],
                SUM(CR.Pass_Amount) AS [Received Payment From NCCF]
            from tbl_NCCF_Storage_Bill_Details SD
            INNER JOIN dbo.tbl_MetaData_GODOWN_2018 MG ON SD.Godown_ID=MG.Godown_ID
            INNER JOIN dbo.tbl_MetaData_DEPOT MD ON MG.BranchID = MD.BranchId
            INNER JOIN dbo.tbl_MetaData_DISTRICT Dis ON MD.DistrictId = Dis.District_Id
            inner join tbl_NCCF_Marketing_Approve_Reject as CR WITH (NOLOCK) on SD.Bill_Number=CR.Bill_Number
            where CR.Fin_Bill_No is not null
            Group by Dis.District_Name,MD.DepotName,MG.Godown_Name
            Order by Dis.District_Name,MD.DepotName,MG.Godown_Name";

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        gvReport.DataSource = dt;
                        gvReport.DataBind();

                        // Enforces clean <thead> and <th> output structuring inside ASP.NET compilation
                        gvReport.UseAccessibleHeader = true;
                        gvReport.HeaderRow.TableSection = System.Web.UI.WebControls.TableRowSection.TableHeader;
                    }
                    else
                    {
                        gvReport.DataSource = null;
                        gvReport.DataBind();
                    }
                }
            }
        }
    }
}