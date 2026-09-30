using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default5Ashutosh : System.Web.UI.Page
{
    // Get connection string from web.config
    string connString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // BindGrid();
            GetCommodity();
        }
    }

    public void GetCommodity()
    {
        string qry = "";
        qry = "select distinct Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in ('63', '64', '33', '52', '27', '92', '123', '31', '65','26') order by Commodity_Name asc";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }

    private void StoreAllBillsInSession()
    {
        // List to hold the bill numbers
        List<string> billList = new List<string>();

        using (SqlConnection con = new SqlConnection(connString))
        {
            // The SQL query provided in your prompt
            string query = @"SELECT NM.Bill_Number
                                FROM tbl_Nafed_Marketing_Approve_Reject NM
                                Inner join tbl_MetaData_GODOWN_2018 MG on nm.Godown_Id=MG.Godown_ID
                                Inner join tbl_MetaData_DEPOT MD on MG.BranchID=MD.BranchId
                                Inner join tbl_MetaData_DISTRICT MDD on MD.DistrictId=MDD.District_Id
                                Inner join tbl_MetaData_STORAGE_COMMODITY Sc on NM.Commodity_Id=Sc.Commodity_Id
                                Inner join tbl_Bills_Fifteen_Day_Wise_Dtl FD on NM.Bill_Number=FD.Bill_Number
                                WHERE NM.Commodity_Id='92' 
                                AND Crop_Year='2023-24' 
                                AND Marketing_Approve_Stutes='Y'";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                try
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // Add each bill number to the list
                            if (dr["Bill_Number"] != DBNull.Value)
                            {
                                billList.Add(dr["Bill_Number"].ToString());
                            }
                        }
                    }

                    // Save the List to a Session variable
                    Session["BillNos"] = billList;

                    // Optional: Provide feedback
                    // Response.Write("Stored " + billList.Count + " bills in Session.");
                }
                catch (Exception ex)
                {
                    // Handle exceptions (logging, etc.)
                    Response.Write("Error: " + ex.Message);
                }
            }
        }
    }
    private void BindGrid()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                // Using the Stored Procedure you provided
                using (SqlCommand cmd = new SqlCommand("Get_Month_Account_Months_New_CropYear", con))
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
                    cmd.Parameters.AddWithValue("@Crop_Year", ddlCropYear.SelectedValue);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvCropData.DataSource = dt;
                    gvCropData.DataBind();
                    StoreAllBillsInSession();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error: " + ex.Message;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        List<string> selectedBills = new List<string>();

        // Loop through GridView rows to find checked items
        foreach (GridViewRow row in gvCropData.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            if (chk != null && chk.Checked)
            {
                // Use DataKeys to get the Bill_Number securely
                string billNo = gvCropData.DataKeys[row.RowIndex].Value.ToString();
                selectedBills.Add(billNo);
            }
        }

        if (selectedBills.Count > 0)
        {
            // Join IDs into a comma-separated string
            string billIds = string.Join(",", selectedBills);
            Session["BillNos"] = "";
            Session["BillNos"] = billIds;
            // Redirect to a specialized Print Page (e.g., PrintTemplate.aspx) 
            // containing the actual report layout
            //Response.Write("<script>window.open('Accounting/State_Nafed_Print_Generate_Bill_AllBillPrint.aspx', '_blank');</script>");
            Response.Write("<script>window.open('Default6.aspx', '_blank');</script>");
        }
        else
        {
            lblMsg.Text = "Please select at least one record to print.";
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        if (ddlcommodity.SelectedIndex > 0 && ddlCropYear.SelectedIndex > 0)
        {
            BindGrid();
        }
    }
}