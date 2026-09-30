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

public partial class Region_Reports_Rpt_Get_BajraProcurement : System.Web.UI.Page
{
   

    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (string.IsNullOrEmpty(Session["UserName"] as string))
        //{
        //    Response.Redirect("~/login.aspx");
        //}
        //else if (Session["UserName"].ToString() == "MPSWLC")
        //{

        if (!IsPostBack)
        {
            loadCommodity();
        }
        //}
        //else
        //{
        //    Response.Redirect("~/login.aspx");
        //}
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
            using (SqlCommand cmd = new SqlCommand("GetBajraProcurement", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", Session["Region_Logid"].ToString());
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
                            GridView1.Caption = @"<b style=""font-weight: bold;""> Region Name" +" " + dt.Rows[0]["Region"].ToString()+ "</br> " + "M.P. Warehousing & Logistics Corporarion" + "</br> " + "PVT Godown Storage Charges Bill(After August)" ;
                            
                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

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