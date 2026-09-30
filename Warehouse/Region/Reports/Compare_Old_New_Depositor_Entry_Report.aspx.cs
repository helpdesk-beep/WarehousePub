using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Linq;
using System.Drawing;

public partial class Region_Reports_Compare_Old_New_Depositor_Entry_Report : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null && Session["Region_ID"] != null)
        {
            if (!IsPostBack)
            {
                string regionId = Session["Region_ID"].ToString();
                if (!IsPostBack)
                {
                    BindDepositors();
                    BindReport(regionId, null);
                }
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }


    private void BindDepositors()
    {
        string query = @"SELECT Depositor_ID, Depositor_Name 
                     FROM [tbl_MetaData_DEPOSITOR] 
                     WHERE Depositor_ID IN ('129','10535','4679','181','15478')
                     ORDER BY Depositor_Name";

        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ddlDepositor.Items.Clear();

                    if (dt.Rows.Count > 0)
                    {
                        ddlDepositor.DataSource = dt;
                        ddlDepositor.DataTextField = "Depositor_Name";
                        ddlDepositor.DataValueField = "Depositor_ID";
                        ddlDepositor.DataBind();
                        ddlDepositor.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
            }
        }
    }

    protected void gvReport_PreRender(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count > 0)
        {
            // Force THEAD
            gvReport.UseAccessibleHeader = true;
            gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }



    void BindReport(string regionId, string DepositorId)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlCommand cmd = new SqlCommand("usp_Compare_Old_New_Depositor_Entry_Report_Region_Wise", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", regionId);
                // Get all selected depositor IDs as comma-separated string
                string selectedIDs = string.Join(",",
                        ddlDepositor.Items.Cast<ListItem>()
                        .Where(i => i.Selected)
                        .Select(i => i.Value)
                );

                if (string.IsNullOrEmpty(selectedIDs))
                    selectedIDs = "0";   // All depositors

                // Pass as VARCHAR explicitly, no extra quotes
                cmd.Parameters.Add("@DepositorID", SqlDbType.VarChar, 100).Value = selectedIDs.Trim();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                        dt.Rows.Add(dt.NewRow()); // Add empty row

                    gvReport.DataSource = dt;
                    gvReport.DataBind();
                }
            }
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string regionId = Session["Region_ID"].ToString();
        string DepositorId = string.IsNullOrEmpty(ddlDepositor.SelectedValue) ? null : ddlDepositor.SelectedValue;
        BindReport(regionId, DepositorId);
    }

    protected void gvReport_DataBound(object sender, EventArgs e)
    {
        if (gvReport.HeaderRow != null)
        {
            gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvReport.FooterRow != null)
        {
            gvReport.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

}
