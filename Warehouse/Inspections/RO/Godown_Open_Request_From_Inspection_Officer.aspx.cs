using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_RO_Godown_Open_Request_From_Inspection_Officer : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] != null) && (Session["UserId"] != null))
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        else
        {
            Response.Redirect("~/Inspections/Default.aspx");
        }
    }

    private void BindGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString;

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SP_Get_Godown_Open_Request_For_RM", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", Session["UserId"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvRequests.DataSource = dt;
                gvRequests.DataBind();
            }
        }
    }
    protected void gvRequests_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Approve" || e.CommandName == "Reject")
        {
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            string godownId = e.CommandArgument.ToString();
            string status = e.CommandName;
            string rmId = Session["UserID"].ToString();
            string quarter = ((HiddenField)row.FindControl("hfQuarter")).Value;
            string verificationType = ((HiddenField)row.FindControl("hfVerificationType")).Value;
            string Emp_Id = ((HiddenField)row.FindControl("hfMobile_No")).Value;
            string branchId = ((HiddenField)row.FindControl("hfBranch_ID")).Value;
            string financialYear = row.Cells[8].Text;

            string constr = ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open();
                    //string checkQuery = "SELECT COUNT(1) FROM Inspection_Final_Submit_by_Officer WHERE Branch_Id = @Branch_Id";
                    //using (SqlCommand cmdCheck = new SqlCommand(checkQuery, con))
                    //{
                    //    cmdCheck.Parameters.AddWithValue("@Branch_Id", Convert.ToInt32(branchId));
                    //    int count = Convert.ToInt32(cmdCheck.ExecuteScalar());
                    //    if (count > 0)
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Branch already final submitted.');", true);
                    //        return; 
                    //    }
                    //}

                    string checkQuery = "SELECT COUNT(1) FROM Inspection_Final_Submit_by_Officer WHERE Branch_Id = @Branch_Id AND Employee_ID=@Emp_Id AND Quater_Type=@Inspection_Quarter AND Verification_Type=@Verification_Type AND Financial_year=@Financial_Year";
                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, con))
                    {
                        cmdCheck.Parameters.AddWithValue("@Branch_Id", Convert.ToInt32(branchId));
                        cmdCheck.Parameters.AddWithValue("@Godown_Id", godownId);
                        cmdCheck.Parameters.AddWithValue("@Status", status);
                        cmdCheck.Parameters.AddWithValue("@Approved_By", rmId);
                        cmdCheck.Parameters.AddWithValue("@Inspection_Quarter", Convert.ToInt32(quarter));
                        cmdCheck.Parameters.AddWithValue("@Verification_Type", Convert.ToInt32(verificationType));
                        cmdCheck.Parameters.AddWithValue("@Financial_Year", financialYear);
                        cmdCheck.Parameters.AddWithValue("@Emp_Id", Emp_Id);
                        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());
                        if (count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Branch already final submitted.');", true);
                            return;
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("SP_Update_Godown_Request_Status", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@Approved_By", rmId);
                        cmd.Parameters.AddWithValue("@Godown_Id", godownId);
                        cmd.Parameters.AddWithValue("@Inspection_Quarter", quarter);
                        cmd.Parameters.AddWithValue("@Verification_Type", verificationType);
                        cmd.Parameters.AddWithValue("@Financial_Year", financialYear);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmdApproved = new SqlCommand("SP_Update_Godown_Request_Status_Approved", con))
                    {
                        cmdApproved.CommandType = CommandType.StoredProcedure;
                        cmdApproved.Parameters.AddWithValue("@Godown_Id", godownId);
                        cmdApproved.Parameters.AddWithValue("@Status", status);
                        cmdApproved.Parameters.AddWithValue("@Approved_By", rmId);
                        cmdApproved.Parameters.AddWithValue("@Inspection_Quarter", Convert.ToInt32(quarter));
                        cmdApproved.Parameters.AddWithValue("@Verification_Type", Convert.ToInt32(verificationType));
                        cmdApproved.Parameters.AddWithValue("@Financial_Year", financialYear);
                        cmdApproved.Parameters.AddWithValue("@Emp_Id", Emp_Id);
                        cmdApproved.ExecuteNonQuery();
                    }
                    BindGrid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg","alert('Action Completed Successfully');", true);
                }
                catch (SqlException ex)
                {
                    string safeErrorMessage = ex.Message.Replace("'", "\\'").Replace("\r\n", " ");
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",String.Format("alert('{0}');", safeErrorMessage), true);
                }
                catch (Exception ex)
                {
                    string safeGeneralMessage = ex.Message.Replace("'", "\\'").Replace("\r\n", " ");
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",String.Format("alert('An unexpected error occurred: {0}');", safeGeneralMessage), true);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }
    }
}