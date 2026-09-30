using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;

public partial class District_Rpt_District_Wise_Pending_Rent_Bill_Details_Summary : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid = "";
    SqlTransaction sqltran;
    string depottype = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillFinasncialYear();
            fillgrid();
        }
    }

    private void fillFinasncialYear()
    {
        try
        {
            string query = "";

            query = "select distinct CropYear from Branch_Godown_Wise_Pending_Bill_Details where startdate<Issue_Date";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlfinancial.Items.Clear();
                ddlfinancial.DataSource = ds.Tables[0];
                ddlfinancial.DataTextField = "CropYear";
                ddlfinancial.DataValueField = "CropYear";
                ddlfinancial.DataBind();
                ddlfinancial.Items.Insert(0, "--Select--");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_District_Wise_pendingBill_Summary_For_Rent_Bill_For_District", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DistrictID", Session["Depot_DistID"].ToString());
                if (ddlfinancial.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@CropYEar", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CropYEar", ddlfinancial.SelectedValue);
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet DS = new DataSet())
                    {
                        sda.Fill(DS);
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            GridView1.DataSource = DS.Tables[0];
                            GridView1.DataBind();

                            GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GridView1.FooterRow.Cells[2].Text = "Total";
                            GridView1.FooterRow.Cells[3].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalBillPendingForGeneration")).ToString();
                            //GridView1.FooterRow.Cells[4].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("TDS_Amt")).ToString();
                            //GridView1.FooterRow.Cells[5].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("OtherDeduction")).ToString();
                            //GridView1.FooterRow.Cells[6].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Payable_Amount")).ToString();
                            Session["CropYear"] = ddlfinancial.SelectedValue.ToString();
                        }
                        else
                        {
                            GridView1.DataSource = DS.Tables[0];
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }


    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }



    protected void ddlFY_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();

    }

    protected void ddlfinancial_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}