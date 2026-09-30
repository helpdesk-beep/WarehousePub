using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for FCI_Rejected_Godown_Bill_MPSCSC
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class FCI_Rejected_Godown_Bill_MPSCSC : System.Web.Services.WebService
{

    public FCI_Rejected_Godown_Bill_MPSCSC()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    //(Description = "Insert a new rejected godown record")
    [WebMethod]
    public string InsertRejectedGodown(
        string godownId, string godownName, string cropYear, decimal rejectedQty,
        string reason, decimal upgradeQty, decimal liftQty, decimal pendingUpgrade,
        decimal pendingLift, string remark, byte[] document, string mimeType)
    {
        try
        {
            string str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
            using (SqlConnection con = new SqlConnection(str))
            {
                string query = @"INSERT INTO FCI_Rejected_Godown_List 
                    (Godown_Id, Godown_Name, Crop_Year, Rejected_QTY, Reason_For_Rejection, 
                     Upgrade_Qty, Lift_Qty, Pending_For_Upgradation, Pending_For_Lift_Upgradation_Qty, 
                     Remark, Upload_Document, Document_MimeType) 
                    VALUES 
                    (@Godown_Id, @Godown_Name, @Crop_Year, @Rejected_QTY, @Reason, 
                     @Upgrade_Qty, @Lift_Qty, @Pending_For_Upgradation, @Pending_For_Lift_Upgradation_Qty, 
                     @Remark, @Doc, @Mime)";

                SqlCommand cmd = new SqlCommand(query, con);

                // Adding Parameters Directly
                cmd.Parameters.AddWithValue("@Godown_Id", (object)godownId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Godown_Name", (object)godownName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Crop_Year", (object)cropYear ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Rejected_QTY", rejectedQty);
                cmd.Parameters.AddWithValue("@Reason", (object)reason ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Upgrade_Qty", upgradeQty);
                cmd.Parameters.AddWithValue("@Lift_Qty", liftQty);
                cmd.Parameters.AddWithValue("@Pending_For_Upgradation", pendingUpgrade);
                cmd.Parameters.AddWithValue("@Pending_For_Lift_Upgradation_Qty", pendingLift);
                cmd.Parameters.AddWithValue("@Remark", (object)remark ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Doc", (object)document ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Mime", (object)mimeType ?? DBNull.Value);

                con.Open();
                cmd.ExecuteNonQuery();
                return "Success: Record Inserted";
            }
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }


    //(Description = "Update an existing rejected godown record")
    [WebMethod]
    public string UpdateRejectedGodown(
        int id, string godownId, string godownName, string cropYear, decimal rejectedQty,
        string reason, decimal upgradeQty, decimal liftQty, decimal pendingUpgrade,
        decimal pendingLift, string remark, byte[] document, string mimeType)
    {
        try
        {
            string str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
            using (SqlConnection con = new SqlConnection(str))
            {
                // We use a dynamic approach for the document so we don't overwrite 
                // existing files with NULL if no new file is uploaded.
                string query = @"UPDATE FCI_Rejected_Godown_List SET 
                    Godown_Id=@Godown_Id, Godown_Name=@Godown_Name, Crop_Year=@Crop_Year, 
                    Rejected_QTY=@Rejected_QTY, Reason_For_Rejection=@Reason, 
                    Upgrade_Qty=@Upgrade_Qty, Lift_Qty=@Lift_Qty, 
                    Pending_For_Upgradation=@Pending_For_Upgradation, 
                    Pending_For_Lift_Upgradation_Qty=@Pending_For_Lift_Upgradation_Qty, 
                    Remark=@Remark";

                if (document != null && document.Length > 0)
                {
                    query += ", Upload_Document=@Doc, Document_MimeType=@Mime";
                }
                query += " WHERE ID=@ID";

                SqlCommand cmd = new SqlCommand(query, con);

                // Adding Parameters Directly
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Godown_Id", (object)godownId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Godown_Name", (object)godownName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Crop_Year", (object)cropYear ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Rejected_QTY", rejectedQty);
                cmd.Parameters.AddWithValue("@Reason", (object)reason ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Upgrade_Qty", upgradeQty);
                cmd.Parameters.AddWithValue("@Lift_Qty", liftQty);
                cmd.Parameters.AddWithValue("@Pending_For_Upgradation", pendingUpgrade);
                cmd.Parameters.AddWithValue("@Pending_For_Lift_Upgradation_Qty", pendingLift);
                cmd.Parameters.AddWithValue("@Remark", (object)remark ?? DBNull.Value);

                if (document != null && document.Length > 0)
                {
                    cmd.Parameters.AddWithValue("@Doc", document);
                    cmd.Parameters.AddWithValue("@Mime", mimeType);
                }

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? "Success: Record Updated" : "Error: ID not found";
            }
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }
}
