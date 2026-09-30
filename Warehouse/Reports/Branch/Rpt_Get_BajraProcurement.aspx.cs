using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Region_Reports_Rpt_Get_BajraProcurement : System.Web.UI.Page
{


    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["UserName"].ToString() != null)
            {
                loadCommodity();
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
    }
    private void loadCommodity()
    {
        List<Commodity> commoList = new List<Commodity>();
        commoList.Add(new Commodity(0, "Select"));
        commoList.Add(new Commodity(13, "Paddy-Common"));
        commoList.Add(new Commodity(8, "Bajra"));
        commoList.Add(new Commodity(11, "Jowar"));

        drpDwnCommodity.DataSource = commoList;
        drpDwnCommodity.DataTextField = "commodityName";
        drpDwnCommodity.DataValueField = "commodityID";
        drpDwnCommodity.DataBind();

    }
    class Commodity
    {
        public int commodityID { get; set; }
        public string commodityName { get; set; }

        public Commodity(int comID, string comName)
        {
            this.commodityID = comID;
            this.commodityName = comName;
        }

        public Commodity() { }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("GetBranchWiseBajraProcurement", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
                cmd.Parameters.AddWithValue("@Commodity_Id", drpDwnCommodity.SelectedValue);
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
                            GridView1.Caption = @"<b style=""font-weight: bold;""> Region Name" + " " + dt.Rows[0]["Region"].ToString() + "</br> " + "M.P. Warehousing & Logistics Corporarion" + "</br> " + "PVT Godown Storage Charges Bill(After August)";

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
    protected void drpDwnCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

}