using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_Reports_Godown_Mapping_With_WeightBridge_Region : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null && Session["Region_ID"] != null)
        {
            if (!IsPostBack)
            {
                string regionId = Session["Region_ID"].ToString();

                // Get region name
                string regionName = GetRegionName(regionId);

                // Set region name in header literal
                lblRegionName.Text = regionName;

                // Load districts and grid
                LoadDistrict(regionId);
                LoadGrid(regionId, null);
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }

    protected string GetRegionName(string regionId)
    {
        using (SqlConnection con = new SqlConnection(constr))
        using (SqlCommand cmd = new SqlCommand("SELECT Regionnm FROM tbl_MetaData_DISTRICT WHERE Region_ID = @Region_ID", con))
        {
            cmd.Parameters.AddWithValue("@Region_ID", regionId);
            con.Open();
            object result = cmd.ExecuteScalar();
            return result != null ? result.ToString() : "";
        }
    }

    void LoadDistrict(string regionId)
    {
        using (SqlConnection con = new SqlConnection(constr))
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = @"
                SELECT District_Id, District_Name 
                FROM tbl_MetaData_DISTRICT 
                WHERE Region_ID = @Region_ID 
                ORDER BY District_Name";
            cmd.Parameters.AddWithValue("@Region_ID", regionId);

            con.Open();
            ddlDistrict.DataSource = cmd.ExecuteReader();
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_Id";
            ddlDistrict.DataBind();
        }

        // Add default item
        ddlDistrict.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- All District --", ""));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string regionId = Session["Region_ID"].ToString();
        string districtId = string.IsNullOrEmpty(ddlDistrict.SelectedValue) ? null : ddlDistrict.SelectedValue;

        LoadGrid(regionId, districtId);
    }

    void LoadGrid(string regionId, string districtId)
    {
        using (SqlConnection con = new SqlConnection(constr))
        using (SqlCommand cmd = new SqlCommand("Godown_Mapping_With_WeightBridge", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Region_ID", regionId);
            cmd.Parameters.AddWithValue("@District_Id", string.IsNullOrEmpty(districtId) ? (object)DBNull.Value : districtId);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // Calculate totals
                    int totalGodown = 0, totalEntryByBM = 0, totalPending = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        totalGodown += Convert.ToInt32(row["Total Godown"]);
                        totalEntryByBM += Convert.ToInt32(row["Entry By BM"]);
                        totalPending += Convert.ToInt32(row["Pending"]);
                    }

                    // Add total row
                    DataRow totalRow = dt.NewRow();
                    totalRow["DepotName"] = "Total";
                    totalRow["Total Godown"] = totalGodown;
                    totalRow["Entry By BM"] = totalEntryByBM;
                    totalRow["Pending"] = totalPending;

                    dt.Rows.Add(totalRow);
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
    }


    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = System.Web.UI.WebControls.TableRowSection.TableHeader;
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // If DepotName column contains "Total", make it bold
            if (e.Row.Cells[3].Text == "Total")
            {
                e.Row.Font.Bold = true;
                e.Row.BackColor = System.Drawing.Color.FromArgb(230, 230, 230); // light gray
            }
        }
    }

}
