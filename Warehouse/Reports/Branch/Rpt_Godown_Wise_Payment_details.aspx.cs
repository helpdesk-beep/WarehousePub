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
using System.IO;
public partial class Reports_Branch_Rpt_Godown_Wise_Payment_details : System.Web.UI.Page
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
            string PopMsg = "";
            PopMsg = Request.QueryString["PopMsg"];
            //GetFinacialYear();
            if (Request.QueryString["PopMsg"] != null)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + PopMsg + "'); </script> ");
            }
            //loadMonth();
            fillGodown();
        }
    }
    //public void GetFinacialYear()
    //{
    //    string qry = "";
    //    qry = "Select distinct Financial_Year from tbl_Institution_Storage_Bill_Details where Financial_Year>='2020-2021'";
    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlFY.DataSource = ds.Tables[0];
    //        ddlFY.DataTextField = "Financial_Year";
    //        ddlFY.DataValueField = "Financial_Year";
    //        ddlFY.DataBind();
    //        ddlFY.Items.Insert(0, new ListItem("All", "0"));
    //    }
    //    else
    //    {

    //    }
    //}
    //private void loadMonth()
    //{
    //    List<Month> commoList = new List<Month>();
    //    commoList.Add(new Month(0, "All"));
    //    commoList.Add(new Month(1, "1"));
    //    commoList.Add(new Month(2, "2"));
    //    commoList.Add(new Month(3, "3"));
    //    commoList.Add(new Month(4, "4"));
    //    commoList.Add(new Month(5, "5"));
    //    commoList.Add(new Month(6, "6"));
    //    commoList.Add(new Month(7, "7"));
    //    commoList.Add(new Month(8, "8"));
    //    commoList.Add(new Month(9, "9"));
    //    commoList.Add(new Month(10, "10"));
    //    commoList.Add(new Month(11, "11"));
    //    commoList.Add(new Month(12, "12"));

    //    ddlMonth.DataSource = commoList;
    //    ddlMonth.DataTextField = "MonthName";
    //    ddlMonth.DataValueField = "MonthID";
    //    ddlMonth.DataBind();

    //}
    //class Month
    //{
    //    public int MonthID { get; set; }
    //    public string MonthName { get; set; }

    //    public Month(int MonthID, string MonthName)
    //    {
    //        this.MonthID = MonthID;
    //        this.MonthName = MonthName;
    //    }
    //    public Month() { }
    //}

    private void fillGodown()
    {
        try
        {

            string query = "";

            query = "SELECT Godown_ID,Godown_Name FROM [dbo].[tbl_MetaData_GODOWN] where BranchId='" + Session["BranchId"].ToString() + "'";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodown.Items.Clear();
                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
                ddlGodown.Items.Insert(0, "--Select--");
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
            using (SqlCommand cmd = new SqlCommand("Rpt_Godown_And_Month_Wise_Pending_Amount_For_Godown", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //if (ddlFY.SelectedValue == "All")
                //{
                //    cmd.Parameters.AddWithValue("@FinacialYear", "0");
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@FinacialYear", ddlFY.SelectedValue.ToString());
                //}
                cmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue);
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
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[16].Text = "Godown Owner Paid Amount";
                            GridView1.FooterRow.Cells[6].Text = "Storage Total Bill Amount";
                            GridView1.FooterRow.Cells[11].Text = "Rent Total Bill Amount";
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount")).ToString("#,##0");
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("RecievedAmount")).ToString("#,##0");
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("RentBillAmt")).ToString("#,##0");
                            GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PaymentToGO")).ToString("#,##0");
                            //GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount")).ToString("#,###,###.##");
                            //GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("RecievedAmount")).ToString("#,###,###.##");
                            //GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("RentBillAmt")).ToString("#,###,###.##");
                            //GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PaymentToGO")).ToString("#,###,###.##");
                        }
                        else
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }


    protected void ddlFY_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}