
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;


public partial class DeleteSelectedWHR : System.Web.UI.Page
{
    private string connString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
              
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    private void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            // Note: Updated WHERE clause to use parameters for Year and Org
            string query = @"
                SELECT 
                    dst.District_Name,
                    MD.DepotName,
                    gdn.Godown_Name,
                    DSWHR.Depositor_WHR_Id
                FROM tbl_Digitally_Signed_WHR_CMS2026 as DSWHR
                INNER JOIN tbl_Storage_Receipt_Details SRD ON DSWHR.Depositor_WHR_Id=SRD.WHR_Id
                INNER JOIN tbl_MetaData_GODOWN_2018 gdn ON DSWHR.GodownID=gdn.Godown_ID
                INNER JOIN tbl_MetaData_DEPOT MD ON gdn.BranchID=MD.BranchId
                INNER JOIN tbl_MetaData_DISTRICT dst ON MD.DistrictId=dst.District_Id
                LEFT JOIN tbl_Digital_Signature_CMS2026 as DSSign ON DSSign.WHR_No=DSWHR.Depositor_WHR_Id 
                LEFT JOIN tbl_DSC_WHR_XML_File_CMS2026 as XWHR ON XWHR.WHR_Id=DSWHR.Depositor_WHR_Id 
                WHERE DSWHR.Depositor_WHR_Id IN (
                    SELECT DISTINCT WHR_No FROM tbl_storage_Depositor_WHR_Relation 
                    WHERE DepositorID = @OrgID AND CropYear = @CropYear
                )
                AND DSWHR.DSC_User_Type != 'G' 
                AND DSSign.WHR_No IS NULL
                AND XWHR.WHR_Id IS NULL";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@OrgID", ddlOrganization.SelectedValue);
                cmd.Parameters.AddWithValue("@CropYear", ddlFinancialYear.SelectedValue);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvWHR.DataSource = dt;
                    gvWHR.DataBind();

                    if (gvWHR.Rows.Count > 0)
                    {
                        // REQUIRED FOR DATATABLES: Generates <thead> instead of <tr>
                        gvWHR.UseAccessibleHeader = true;
                        gvWHR.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        List<string> selectedIds = new List<string>();

        // Find checked rows in the GridView
        foreach (GridViewRow row in gvWHR.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkRow");
            if (chk != null && chk.Checked)
            {
                string id = gvWHR.DataKeys[row.RowIndex].Value.ToString();
                selectedIds.Add(id);
            }
        }

        if (selectedIds.Count > 0)
        {
            //DeleteRecords(selectedIds);
            ExecuteMultipleDelete(selectedIds);
            BindGrid(); // Refresh the list
        }
    }

    private void DeleteRecords(List<string> whrIdList)
    {
        // Construct parameterized names for .NET 4.0
        var paramNames = whrIdList.Select((id, index) => "@id" + index).ToArray();
        string inClause = string.Join(",", paramNames);

        // Standard string.Format for C# 4.0
        //string query = string.Format(@"
        //    DELETE FROM tbl_Digitally_Signed_WHR_CMS2026 
        //    WHERE Depositor_WHR_Id IN ({0})", inClause);

        using (SqlConnection conn = new SqlConnection(connString))
        {
            using (SqlCommand cmd = new SqlCommand("sp_DeleteMultipleWHR_Records", conn))
            {
                for (int i = 0; i < whrIdList.Count; i++)
                {
                    cmd.Parameters.AddWithValue(paramNames[i], whrIdList[i]);
                }
                conn.Open();
                int deletedCount = cmd.ExecuteNonQuery();
                lblStatus.Text = "<div class='alert alert-success'>" + deletedCount + " Record(s) deleted successfully.</div>";
            }
        }
    }
    public void ExecuteMultipleDelete(List<string> selectedIds)
    {
        // 1. Convert the list of IDs into a single comma-separated string
        // Example: "101,102,103"
        string idString = string.Join(",", selectedIds.ToArray());

        string connString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connString))
        {
            // 2. Define the command and specify it is a Stored Procedure
            using (SqlCommand cmd = new SqlCommand("sp_DeleteMultipleWHR_Records", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. Add the parameter matching the SQL variable name @WHR_IDs
                // We use VarChar or NVarChar depending on your SP definition
                cmd.Parameters.Add("@WHR_IDs", SqlDbType.NVarChar, -1).Value = idString;

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Success logic
                    lblStatus.Text = "Deletion successful.";
                }
                catch (SqlException ex)
                {
                    // Error handling
                    lblStatus.Text = "Database Error: " + ex.Message;
                }
            }
        }
    }


}