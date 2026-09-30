using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class StatePages_DeleteMultipleWHR : System.Web.UI.Page
{
    // Uses the connection string from your Web.config
    private string connStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"].ToString() != null)
            {
                if (!IsPostBack)
                {
                    BindGrid();
                }
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }

        }
    }

    private void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(connStr))
        {
            string query = @"
                SELECT A.DispatchId, A.Commodity, A.Season, A.DispatchCreatedDate, A.VehicleNo, 
                       A.DispatchQuantity, A.DispatchBags, A.CenterName, A.Pacs, A.GodownName, 
                       A.GoodwnID, A.Moisture, A.BagType, B.DF_Receipt_Id, B.Acceptance_Date, 
                       B.Acceptance_No
                FROM Recieved_DispatchId_From_NAFED_ANB A
                INNER JOIN Receive_Proc_NAFED_ANB_26R B ON A.DispatchId = B.acceptance_no
                LEFT JOIN tbl_Storage_Receipt_Details C ON B.DF_Receipt_Id = C.acpt_FCIRO_no
                WHERE C.Acpt_FCIRO_No IS NULL";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvDispatch.DataSource = dt;
                gvDispatch.DataBind();

                // Ensures the GridView renders <thead> and <tfoot> for DataTables
                if (gvDispatch.Rows.Count > 0)
                {
                    gvDispatch.UseAccessibleHeader = true;
                    gvDispatch.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gvDispatch.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
        }
    }

    [WebMethod]
    public static string DeleteSelected(List<string> ids)
    {
        // Must re-fetch connection string in static method
        string connStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        try
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                string joinedIds = string.Join("','", ids);
                // Deleting from Table B as requested
                string deleteQuery = "DELETE FROM Receive_Proc_NAFED_ANB_26R WHERE Acceptance_No IN ('" + joinedIds + "')";

                using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                {
                    int count = cmd.ExecuteNonQuery();
                    return count + " record(s) deleted successfully.";
                }
            }
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }
}