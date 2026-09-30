using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Globalization;

public partial class StatePages_Rpt_GetpassWiseDODetail : System.Web.UI.Page
{
    decimal qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal qtyTotal3 = 0;
    decimal qtyTotal4 = 0;
    decimal qtyTotal5 = 0;

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;
    decimal grQtyTotal4 = 0;
    decimal grQtyTotal5 = 0;

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
        if (!IsPostBack)
        {
            labelName.Text = DateTime.Now.ToString();
            fillScheduleInsp_Grid();
        }
    }
    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("Get_GetpassWiseDODetail", con))
            //using (SqlCommand cmd = new SqlCommand("Godown_Check_03Jan2024", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Gatepass_No", Request.QueryString["GatePassNo"].ToString());
                //cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue);
                //cmd.Parameters.AddWithValue("@BranchPwd",txtBranchPwd.Text.ToString());

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    //using (DataTable dt = new DataTable())
                    //{
                    //sda.Fill(dt);
                    sda.Fill(ds);
                    DataTable tableA = ds.Tables[0];
                    //DataTable tableB = ds.Tables[1];
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GrdGodown.DataSource = tableA;
                        GrdGodown.DataBind();
                        lblwhrno.Text = Request.QueryString["GatePassNo"].ToString();
                        lblwhrdate.Text = ds.Tables[0].Rows[0]["GPDate"].ToString();
                        //lblReceiveBags.Text = ds.Tables[1].Rows[0]["RecBags"].ToString();
                        //lblReceiveWeight.Text = ds.Tables[1].Rows[0]["RecQty"].ToString();
                        //lblDepositor.Text = ds.Tables[1].Rows[0]["Depositor_Name"].ToString();
                        //lblAvailableBags.Text = ds.Tables[1].Rows[0]["AvailBags"].ToString();
                        //lblAvailableWeight.Text = ds.Tables[1].Rows[0]["AvailQty"].ToString();
                        //GrdGodown.FooterRow.Style.Add("text-align", "right");
                        //GrdGodown.FooterRow.Cells[1].Text = "Total";
                        //GrdGodown.FooterRow.Cells[2].Text = tableA.AsEnumerable().Sum(row => row.Field<int>("No_Of_Bags")).ToString();
                        //GrdGodown.FooterRow.Cells[3].Text = tableA.AsEnumerable().Sum(row => row.Field<decimal>("Bags_Weight")).ToString();
                        //GrdGodown.FooterRow.Cells[4].Text = tableA.AsEnumerable().Sum(row => row.Field<decimal>("Loss")).ToString();
                        //GrdGodown.FooterRow.Cells[5].Text = tableA.AsEnumerable().Sum(row => row.Field<decimal>("Gain")).ToString();
                    }
                    else
                    {
                        GrdGodown.DataSource = null;
                        GrdGodown.DataBind();                       
                    }

                }
            }
        }
    }
   
}