using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
public partial class WarehouseLevel_Rpt_Godown_And_Month_Wise_Pending_Amount : System.Web.UI.Page
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
            if (Request.QueryString["PopMsg"] != null)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + PopMsg + "'); </script> ");
            }
            //fillFinasncialYear();
            //loadMonth();           
            fillgrid();
        }
    }
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
    class Month
    {
        public int MonthID { get; set; }
        public string MonthName { get; set; }

        public Month(int MonthID, string MonthName)
        {
            this.MonthID = MonthID;
            this.MonthName = MonthName;
        }
        public Month() { }
    }
    //private void fillFinasncialYear()
    //{
    //    try
    //    {
    //        string query = "";

    //        query = "select distinct Financial_Year from tbl_Institution_Storage_Bill_Details where Financial_Year>'2019-2020'";

    //        cmd = new SqlCommand(query, con);
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlFY.Items.Clear();
    //            ddlFY.DataSource = ds.Tables[0];
    //            ddlFY.DataTextField = "Financial_Year";
    //            ddlFY.DataValueField = "Financial_Year";
    //            ddlFY.DataBind();
    //            ddlFY.Items.Insert(0, "--Select--");
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
                cmd.Parameters.AddWithValue("@GodownID", Session["GodownID_New"].ToString());
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
                            GridView1.Caption = @"<b style=""font-weight: bold; text-align:center;""> Godown Name" + "  -   " + dt.Rows[0]["Godown_Name"].ToString() ;
                            GridView1.FooterRow.Style.Add("text-align", "center");
                            GridView1.FooterRow.Cells[10].Text = "Total";
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("RecievedAmount")).ToString();
                            GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("RentBillAmt")).ToString();
                            // lblgdnname.Text = dt.Rows[0]["Godown_Name"].ToString();
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
   
  
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlFY_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}