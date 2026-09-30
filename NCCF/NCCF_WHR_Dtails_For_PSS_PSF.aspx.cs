using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NCCF_NCCF_WHR_Dtails_For_PSS_PSF : System.Web.UI.Page
{
    decimal qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal qtyTotal3 = 0;

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;

    long storid = 0;
    int rowIndex = 1;


    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
        {
            if (!IsPostBack)
            {
                fillRegion();
            }
            else
            {
                //Response.Redirect("~/Login/Login.aspx");
            }
        }
        else
        {
            Response.Redirect("~/Login/Nccf_Login.aspx");
        }
    }
    private void fillRegion()
    {
        try
        {
            if (Session["Username"].ToString() == "Indore Business")
            {
                string query = "SELECT [Region_Id],[region] FROM [tbl_MetaData_Region] Where region IN ('Bhopal', 'Jabalpur', 'Sagar', 'Indore') order by region asc";
                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlRegion.Items.Clear();
                    ddlRegion.DataSource = ds.Tables[0];
                    ddlRegion.DataTextField = "region";
                    ddlRegion.DataValueField = "Region_Id";
                    ddlRegion.DataBind();
                    ddlRegion.Items.Insert(0, "---Select---");
                }
                else
                {
                }
            }
            else
            {
                string query = "SELECT [Region_Id],[region] FROM [tbl_MetaData_Region] Where region IN ('GWALIOR', 'UJJAIN', 'REWA', 'NARMADAPURAM') order by region asc";
                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlRegion.Items.Clear();
                    ddlRegion.DataSource = ds.Tables[0];
                    ddlRegion.DataTextField = "region";
                    ddlRegion.DataValueField = "Region_Id";
                    ddlRegion.DataBind();
                    ddlRegion.Items.Insert(0, "---Select---");
                }
                else
                {
                }
            }
        }
        catch (Exception)
        {
        }
    }
    protected void fillgrid()
    {
        //Decimal opcloavg = 0;
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_WHR_Details_For_NCCF", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_Id", ddlRegion.SelectedValue);
                cmd.Parameters.AddWithValue("@CropYear", DdlCropYear.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            divgrid.Visible = true;
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

            if (GridView1.FooterRow != null)
            {
                GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            try
            {
                // Get row
                GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;

                // Get Depositor WHR Id
                string depositorWHRId = e.CommandArgument.ToString();

                // Find dropdown control
                DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");
                string selectedScheme = ddlStatus.SelectedValue;

                if (selectedScheme == "0" || string.IsNullOrEmpty(selectedScheme))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg",
                        "alert('Please select a scheme before updating.');", true);
                    return;
                }

                // Call inline query update
                UpdateSchemeName(depositorWHRId, selectedScheme);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg",
                    "alert('Error: " + ex.Message.Replace("'", "") + "');", true);
            }
        }
    }
    private void UpdateSchemeName(string depositorWHRId, string schemeName)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = @"UPDATE tbl_storage_Depositor_WHR_Relation
                         SET SchemeName = @SchemeName
                         WHERE Depositor_WHR_Id = @Depositor_WHR_Id";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@SchemeName", schemeName);
                cmd.Parameters.AddWithValue("@Depositor_WHR_Id", depositorWHRId);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                con.Close();

                if (rows > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg",
                        "alert('Record updated successfully!');", true);

                    fillgrid(); // Refresh Grid
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg",
                        "alert('Record not updated.');", true);
                }
            }
        }
    }
    protected void btnSearch1_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
}