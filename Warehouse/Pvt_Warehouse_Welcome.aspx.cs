using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Pvt_Warehouse_Welcome : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_start.Text = System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
        
        if (!string.IsNullOrEmpty(Session["GodownID_New"] as string))
        {
            
            //CheckPassChange();
            if (Session["UserName"].ToString() != "")
            {
                UxName.Text = Session["UserName"].ToString();
            }

            if (Session["IsSussess"] == "Success")
            {
                Session.Remove("IsSussess");
            }
            if (!IsPostBack)
            {
                GetLatiLongi();
            }
        }
        else
        {
            Response.Redirect("Logout.aspx");
        }
        lbl_end.Text = System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
    }
    public void CheckPassChange()
    {
            string Old_Pass1 = string.Empty;
            string Old_Pass2 = string.Empty;
            string str = "select Godown_Name,Godown_Id,Password,MasterPassword,GM_Pwd from Pvt_Warehouse_Login where Godown_Id ='" + Session["GodownID_New"].ToString() + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Old_Pass1 = ds.Tables[0].Rows[0]["Password"].ToString();
                Old_Pass2 = ds.Tables[0].Rows[0]["GM_Pwd"].ToString();
               
            }
            if (Old_Pass1 == "wlc2015" || Old_Pass2 == "bm2015")
            {
                Response.Redirect("~/WarehouseLevel/Change_Password_PvtW.aspx");
            }

             
    }
    public void GetLatiLongi()
    {
        try
        {
            string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
            string latitude = Request.Cookies["Lati"].Value.ToString();
            string longitude = Request.Cookies["Longi"].Value.ToString();
            //string latitude = (this.Request.Form.Get("n_lati"));
            //string longitude = (this.Request.Form.Get("n_long"));  
            if (latitude != "" && longitude != "" && latitude != null && longitude != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                qry = "select GodownId from [tbl_GodownLatitudeLongitude] where GodownId='" + Session["GodownID_New"].ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(qry, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    qry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_GodownLatitudeLongitude]([GodownId],[Latitude],[Longitude],[CreatedBy],[CreatedDate]) VALUES ('" + Session["GodownID_New"].ToString() + "','" + latitude + "','" + longitude + "','" + ClientIP + "',getdate())";
                    cmd = new SqlCommand(qry, con);
                    int c = cmd.ExecuteNonQuery();
                    if (c > 0)
                    {
                        //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Coordinates Updated '); </script> ");
                    }
                }
                else
                {
                    qry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_GodownLatitudeLongitude_Log] select [GodownId],[Latitude],[Longitude],[CreatedBy],[CreatedDate] from [tbl_GodownLatitudeLongitude] where [GodownId]='" + Session["GodownID_New"].ToString() + "'";
                    cmd = new SqlCommand(qry, con);
                    int c = cmd.ExecuteNonQuery();
                    if (c > 0)
                    {
                        qry = "update [tbl_GodownLatitudeLongitude] set [Latitude]='" + latitude + "',[Longitude]='" + longitude + "',CreatedBy='" + ClientIP + "',CreatedDate=getdate() where [GodownId]='" + Session["GodownID_New"].ToString() + "'";
                        cmd = new SqlCommand(qry, con);
                        cmd.ExecuteNonQuery();
                        //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Coordinates Updated '); </script> ");
                    }

                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            else
            {
                //lblmsg.Text = "Errror";
            }
        }
        catch (Exception ex)
        {
         
        }
    }

}
