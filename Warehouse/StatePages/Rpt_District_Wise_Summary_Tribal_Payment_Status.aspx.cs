using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class StatePages_Rpt_District_Wise_Summary_Tribal_Payment_Status : System.Web.UI.Page
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
            //fillComodity();
            fillgrid();
        }
    }

    private void fillgrid()
    {
        try
        {
            SqlCommand cmdd = new SqlCommand("Get_Summary_For_Tribal_Godown", con);
            cmdd.CommandType = CommandType.StoredProcedure;
            //if (ddlCapacity.SelectedValue == "--Select--")
            //{
            //    cmdd.Parameters.AddWithValue("@FinancialYEar", 0);
            //}
            //else
            //{
            //    cmdd.Parameters.AddWithValue("@FinancialYEar", ddlCapacity.SelectedValue);
            //}
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.FooterRow.Style.Add("text-align", "right");
                GridView1.FooterRow.Cells[2].Text = "Total";
                GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("StorageAmount")).ToString();
                GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("JVS_Bill_Amount")).ToString();
                GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PFCredit_Amount")).ToString();
                GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingatMPWLC")).ToString();
                GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ReceiveFromMPSCSC")).ToString();
                //GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingatMPSCSC")).ToString();

                //btnUpdate.Visible = false;
            }
        }

        catch (Exception ex)
        { 
        }
    }
    //private void fillComodity()
    //{
    //    try
    //    {

    //        string query = "";
    //        query = "select distinct Financial_Year from tbl_Institution_Storage_Bill_Details where From_Date>='08/01/2020'";
    //        cmd = new SqlCommand(query, con);
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlCapacity.Items.Clear();
    //            ddlCapacity.DataSource = ds.Tables[0];
    //            ddlCapacity.DataTextField = "Financial_Year";
    //            ddlCapacity.DataValueField = "Financial_Year";
    //            ddlCapacity.DataBind();
    //            ddlCapacity.Items.Insert(0, "--Select--");
    //        }
    //        else
    //        {
    //            ////
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //////
    //    }
    //}

    

    //protected void ddlCapacity_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgrid();
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
}