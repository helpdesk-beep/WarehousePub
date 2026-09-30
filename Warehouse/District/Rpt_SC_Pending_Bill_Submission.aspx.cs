using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;

public partial class District_Rpt_SC_Pending_Bill_Submission : System.Web.UI.Page
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
            fillGodownType();
        }
    }
    private void fillGodownType()
    {
        try
        {
            string query = "";
            query = "select distinct MG.Hired_Type [GodownType] from tbl_Metadata_Godown_2018 MG ORDER BY MG.Hired_Type";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlhiredtype.Items.Clear();
                ddlhiredtype.DataSource = ds.Tables[0];
                ddlhiredtype.DataTextField = "GodownType";
                ddlhiredtype.DataValueField = "GodownType";
                ddlhiredtype.DataBind();
                //ddlGodownType.Items.Insert(0, "--Select--");
                ddlhiredtype.Items.Insert(0, new ListItem("--Select--", "0"));
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
            using (SqlCommand cmd = new SqlCommand("Get_FinancialYear_Wise_Pending_Bill_For_Submission_to_MPSCSC_For_District", con))
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
                if (ddlhiredtype.SelectedItem.Text == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", ddlhiredtype.SelectedItem.Text);
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
                            GridView1.FooterRow.Cells[3].Text = "Total";
                            GridView1.FooterRow.Cells[4].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalBill")).ToString();
                            GridView1.FooterRow.Cells[5].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("TotalAmoun")).ToString();
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

    protected void ddlhiredtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}