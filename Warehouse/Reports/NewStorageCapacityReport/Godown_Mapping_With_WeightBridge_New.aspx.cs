using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class SRV_Storage_Reports_Inspenctions_Godown_Mapping_With_WeightBridge_New : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadRegion();
            LoadDistrict(null);
            LoadGrid();
        }
    }

    void LoadRegion()
    {
        using (SqlConnection con = new SqlConnection(constr))
        //using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT Regionnm FROM tbl_MetaData_DISTRICT ORDER BY Regionnm", con))
        using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT Region_ID, Regionnm FROM tbl_MetaData_DISTRICT ORDER BY Regionnm", con))
        {
            con.Open();
            ddlRegion.DataSource = cmd.ExecuteReader();
            ddlRegion.DataTextField = "Regionnm";
            ddlRegion.DataValueField = "Region_ID";
            ddlRegion.DataBind();
        }
        ddlRegion.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- All Region --", ""));
    }

    void LoadDistrict(string region)
    {
        using (SqlConnection con = new SqlConnection(constr))
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = con;
            //cmd.CommandText = "SELECT DISTINCT District_Name FROM tbl_MetaData_DISTRICT " + "WHERE (@Region IS NULL OR Regionnm=@Region) ORDER BY District_Name";
            cmd.CommandText = "SELECT DISTINCT District_Id, District_Name FROM tbl_MetaData_DISTRICT " + "WHERE (@Region_ID IS NULL OR Region_ID=@Region_ID) ORDER BY District_Name";
            cmd.Parameters.AddWithValue("@Region_ID", string.IsNullOrEmpty(region) ? (object)DBNull.Value : region);

            con.Open();
            ddlDistrict.DataSource = cmd.ExecuteReader();
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_Id";
            ddlDistrict.DataBind();
        }
        ddlDistrict.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- All District --", ""));
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDistrict(ddlRegion.SelectedValue);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadGrid();
    }


    void LoadGrid()
    {
        using (SqlConnection con = new SqlConnection(constr))
        using (SqlCommand cmd = new SqlCommand("Godown_Mapping_With_WeightBridge", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Region_ID", string.IsNullOrEmpty(ddlRegion.SelectedValue) ? (object)DBNull.Value : ddlRegion.SelectedValue);
            cmd.Parameters.AddWithValue("@District_Id",
                string.IsNullOrEmpty(ddlDistrict.SelectedValue) ? (object)DBNull.Value : ddlDistrict.SelectedValue);

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
                    totalRow["DepotName"] = "Total"; // Label for total
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
            ScriptManager.RegisterStartupScript(this, GetType(),
                "datatable", "InitDataTable();", true);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Highlight the total row
            if (e.Row.Cells[3].Text == "Total") // DepotName column
            {
                e.Row.Font.Bold = true;
                e.Row.BackColor = System.Drawing.Color.FromArgb(230, 230, 230); // light gray
            }
        }
    }





}