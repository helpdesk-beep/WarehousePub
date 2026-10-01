using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public static class WebsiteLookups
{
    public static DataTable GetRoles()
    {
        DataTable roles = new DataTable();
        roles.Columns.Add("ID", typeof(string));
        roles.Columns.Add("Roll", typeof(string));
        roles.Rows.Add("1", "Head Office");
        roles.Rows.Add("2", "Regional Office");
        roles.Rows.Add("3", "District");
        roles.Rows.Add("4", "Branch");
        roles.Rows.Add("5", "Godown");
        return roles;
    }

    public static DataTable GetRegions()
    {
        return Load("SELECT Region_Id, Region FROM tbl_MetaData_Region ORDER BY Region");
    }

    public static DataTable GetDistricts(string regionId)
    {
        return Load("SELECT District_Id, District_Name FROM tbl_MetaData_DISTRICT WHERE (@RegionId = '0' OR Region_Id = @RegionId) ORDER BY District_Name",
            new SqlParameter("@RegionId", SqlDbType.VarChar, 20) { Value = regionId });
    }

    public static DataTable GetBranches(string districtId)
    {
        return Load("SELECT BranchId, DepotName FROM tbl_MetaData_DEPOT WHERE DistrictId = @DistrictId ORDER BY DepotName",
            new SqlParameter("@DistrictId", SqlDbType.VarChar, 20) { Value = districtId });
    }

    public static DataTable GetGodowns(string branchId)
    {
        return Load("SELECT Godown_ID, Godown_Name FROM tbl_MetaData_GODOWN_2018 WHERE BranchID = @BranchId ORDER BY Godown_Name",
            new SqlParameter("@BranchId", SqlDbType.VarChar, 20) { Value = branchId });
    }

    private static DataTable Load(string query, params SqlParameter[] parameters)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
        {
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            DataTable results = new DataTable();
            adapter.Fill(results);
            return results;
        }
    }
}
