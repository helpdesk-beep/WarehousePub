using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Reports_States_Rpt_District_Wise_HiredTypeWise_Capacity : System.Web.UI.Page
{


    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillDistrict();
            fillGodownType();
            FillStorageType();
            fillgrid();
        }
    }

    private void fillGodownType()
    {
        try
        {
            string query = "";
            //query = "select distinct Hired_Type [HiredType] from tbl_MEtaData_Godown_2018 order by Hired_Type";
            query = "select distinct Hired_Type [HiredType] from tbl_MEtaData_Godown_2018 MG";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlHiredType.Items.Clear();
                ddlHiredType.DataSource = ds.Tables[0];
                ddlHiredType.DataTextField = "HiredType";
                ddlHiredType.DataValueField = "HiredType";
                ddlHiredType.DataBind();
                ddlHiredType.Items.Insert(0, "--Select--");
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

    private void fillDistrict()
    {
        try
        {
            string query = "";
            //query = "select distinct Hired_Type [HiredType] from tbl_MEtaData_Godown_2018 order by Hired_Type";
            query = "select District_Id,District_Name from tbl_MetaData_DISTRICT order by District_Name";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "--Select--");
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
            using (SqlCommand cmd = new SqlCommand("usp_DistrictWiseCapacityDetails", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlDistrict.SelectedValue.ToString() == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@DistrictID", '0');
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DistrictID", ddlDistrict.SelectedValue.ToString());
                }
                if (ddlHiredType.SelectedValue.ToString() == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@HiredType", '0');
                }
                else
                {
                    cmd.Parameters.AddWithValue("@HiredType", ddlHiredType.SelectedValue.ToString());
                }
                if (ddlstoragetype.SelectedValue.ToString() == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@StorageType", '0');
                }
                else
                {
                    cmd.Parameters.AddWithValue("@StorageType", ddlstoragetype.SelectedValue.ToString());
                }
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
                            GridView1.FooterRow.Style.Add("text-align", "Right");
                            GridView1.FooterRow.Cells[2].Text = "Total";
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalGodown")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("GodownScientificCapacity")).ToString();

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

    protected void ddlHiredType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlHiredType.SelectedValue.ToString() == "--Select--")
        {
            fillgrid();
        }
        else
        {
            fillgrid();
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string varHiredType;
        string District;

        varHiredType = ddlHiredType.SelectedItem.ToString();
        District = ddlDistrict.SelectedValue.ToString();
        fillgrid();
        //if (ddlHiredType.SelectedValue.ToString() == "--Select--" )
        //{
        //    fillgrid();
        //}
        //else
        //{
        //    fillgrid();
        //}
    }




    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue.ToString() == "--Select--")
        {
            fillgrid();
        }
        else
        {
            fillgrid();
        }
    }
    private void FillStorageType()
    {

        string query = " select distinct Storage_Type from tbl_MetaData_GODOWN_2018";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlstoragetype.DataSource = ds.Tables[0];
            ddlstoragetype.DataTextField = "Storage_Type";
            ddlstoragetype.DataValueField = "Storage_Type";
            ddlstoragetype.DataBind();
            ddlstoragetype.Items.Insert(0, "--Select--");
        }

    }
    protected void ddlstoragetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue.ToString() == "--Select--")
        {
            fillgrid();
        }
        else
        {
            fillgrid();
        }
    }
}