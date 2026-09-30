using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Rpt_District_Wise_Qty_Available_Last_10_Year_For_Storage_TypeNew : System.Web.UI.Page
{
   
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillComodity();
        }
    }
    private void fillComodity()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "";
                query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in(3,13,22,92,63,8,11,12) order by Commodity_Name";
                cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    drpDwnCommodity.Items.Clear();
                    drpDwnCommodity.DataSource = ds.Tables[0];
                    drpDwnCommodity.DataTextField = "Commodity_Name";
                    drpDwnCommodity.DataValueField = "Commodity_Id";
                    drpDwnCommodity.DataBind();
                    //drpDwnCommodity.Items.Insert(0, "--Select--");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "showSweetAlert('Success!', 'No Record Found!', 'Success');", true);
                } 
            }
        }
        catch (Exception)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "showSweetAlert('Error!', 'Something Went Wrong!', 'error');", true);
        }
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("District_Wise_Stock_Position_Last_10_Years_Storage_typeNew", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ******** MULTIPLE COMMODITY IDS ********
                    string selectedIDs = string.Join(",",
                        drpDwnCommodity.Items.Cast<ListItem>()
                        .Where(i => i.Selected)
                        .Select(i => i.Value)
                    );

                    if (string.IsNullOrEmpty(selectedIDs))
                        selectedIDs = "0";   // All commodities
                    else
                        //    selectedIDs = "'" + selectedIDs + "'";

                        //cmd.Parameters.AddWithValue("@CommodityIDs", selectedIDs);
                        cmd.Parameters.Add("@CommodityIDs", SqlDbType.VarChar).Value = selectedIDs;
                    cmd.Parameters.AddWithValue("@Type", ddltype.SelectedValue);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                GridView1.DataSource = dt;
                                GridView1.DataBind();

                                GridView1.FooterRow.Style.Add("text-align", "left");
                                GridView1.FooterRow.Cells[1].Text = "Total";

                                GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("2016-17")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("2017-18")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("2018-19")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("2019-20")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("2020-21")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("2021-22")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("2022-23")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("2023-24")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("2024-25")).ToString("#,##0.00");
                                //GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("2025-26")).ToString("#,##0.00");
                            }
                            else
                            {
                                GridView1.DataSource = null;
                                GridView1.DataBind();

                                ClientScript.RegisterStartupScript(
                                    this.GetType(),
                                    "SweetAlert",
                                    "showSweetAlert('Info!', 'No Record Found!', 'info');",
                                    true
                                );
                            }
                        }
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            string script = "alert('Error: " + ex.Message.Replace("'", "\\'") + "');";
            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script, true);
        }
        finally
        {
            cmd = null;
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SRV/Storage_Reports/Inspenctions/Rpt_District_Wise_Qty_Available_Last_10_Year_For_Storage_TypeNew.aspx", true);
    }
}