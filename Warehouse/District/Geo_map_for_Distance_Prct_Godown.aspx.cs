using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class District_Geo_map_for_Distance_Prct_Godown : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
   
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_withindist.Visible = false;
        lbl_interdist.Visible = false;
        if (!IsPostBack)
        {            
            
        }

    }

    private void plotpoint_depot()
    {
        SqlDataAdapter da = null;
        DataTable dt = new DataTable();       
        string str = "select DISTINCT [PCID],[Latitude], [Longitude],[Dist] from [PPMS2019].[dbo].[ProcCntrLocation] where PCID='"+txt_pcid.Text+"' and Latitude!='' and Longitude!='' ";
        da = new SqlDataAdapter(str, con);
        da.Fill(dt);


        Session["dt"] = dt;
        GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
        GoogleMapForASPNet1.GoogleMapObject.Width = "100%";
        GoogleMapForASPNet1.GoogleMapObject.Height = "700px";
        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 7;
        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", Convert.ToDouble(dt.Rows[0]["latitude"]), Convert.ToDouble(dt.Rows[0]["Longitude"]));
        decimal vaccap = 0;

        GooglePoint GP1 = new GooglePoint();
        GP1.ID = "PCID" + 0 + 1.ToString();

        GP1.Latitude = CheckNull(dt.Rows[0]["Latitude"].ToString());
        GP1.Longitude = CheckNull(dt.Rows[0]["Longitude"].ToString());
        GP1.ToolTip = dt.Rows[0]["PCID"].ToString();
        GP1.IconImage = "icons/Blue.png";
        GP1.InfoHTML = "<b> Procurement Centre Id : </b> " + dt.Rows[0]["PCID"].ToString() + "<br />";
        if (GP1.Latitude > 0 && GP1.Longitude > 0)
        {
            GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);
        }
       /* for (int i = 0; i < dt.Rows.Count; i++)
        {
           // vaccap = Convert.ToDecimal(dt.Rows[i]["Godown_Capacity"].ToString()) - Convert.ToDecimal(dt.Rows[i]["currentqty"].ToString());
            Boolean temp_red = false;
            GooglePoint GP1 = new GooglePoint();
            GP1.ID = "PCID" + i + 1.ToString();

              GP1.Latitude = CheckNull(dt.Rows[i]["Latitude"].ToString());
              GP1.Longitude = CheckNull(dt.Rows[i]["Longitude"].ToString());
           
            GP1.ToolTip = dt.Rows[i]["PCID"].ToString();
            decimal avi = 0;
            decimal forzero = 0;
            decimal forzero2 = 0;
            /* if (Convert.ToDecimal(dt.Rows[i]["currentqty"].ToString()) == 0)
             {
                 forzero = 1;
             }
             else
             {
                 forzero = Convert.ToDecimal(dt.Rows[i]["currentqty"].ToString());
             }
             if (Convert.ToDecimal(dt.Rows[i]["Godown_Capacity"].ToString()) == 0)
             {
                 forzero2 = 1;
             }
             else
             {
                 forzero2 = Convert.ToDecimal(dt.Rows[i]["Godown_Capacity"].ToString());
             }
             avi = (forzero * 100) / forzero2;
             if (CheckNullQty(dt.Rows[i]["currentqty"].ToString()) <= 0)
             {
                 if (Convert.ToDecimal(dt.Rows[i]["Godown_Capacity"].ToString()) != 0)
                 {
                     temp_red = true;
                     GP1.IconImage = "/icons/Green.png";
                 }
                 else
                 {
                     GP1.IconImage = "icons/wr.png";
                 }
             }

             else if (avi > 80)
             {
                 temp_red = false;
                 GP1.IconImage = "icons/Red.png";
             }
             else
             {
                 GP1.IconImage = "icons/Blue.png";
             }

            GP1.IconImage = "icons/Blue.png";
            GP1.InfoHTML = "<b> Procurement Centre Id : </b> " + dt.Rows[i]["PCID"].ToString() +  "<br />";
            if (GP1.Latitude > 0 && GP1.Longitude > 0)
            {
                GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);
            }
        }*/
    }

    protected double CheckNull(string Val)
    {
        string chkval = "0"; string dotClean = "0"; string othr = "0"; string mrgval = "0";
        double rval = 0;
        if (Val == "" || Val.ToLower().Contains("&nbsp;") || Val == null || Val == "0")
        {
            rval = 0;
        }
        else
        {
            try
            {
                if (Val.Length > 4)
                {
                    chkval = Val.Substring(0, 3);
                    dotClean = Val.Replace(".", "");
                    othr = dotClean.Remove(0, 2);
                    mrgval = chkval + othr;
                    rval = Convert.ToDouble(mrgval);
                }
            }
            catch (Exception ex)
            {

            }
        }
        return rval;
    }

    protected double CheckNullQty(string Val)
    {
        double rval = 0;
        if (Val == "" || Val.ToLower().Contains("&nbsp;") || Val == null)
        {
            rval = 0;
        }
        else
        {
            rval = Convert.ToDouble(Val);
        }
        return rval;
    }

    protected void btn_dist_Click(object sender, EventArgs e)
    {
        
        Within_Dist_Distance_Vac_Capacity();
        //plotpoint_depot();
    }
      
    protected void Within_Dist_Distance_Vac_Capacity()
    {
        
        lbl_withindist.Visible = true;
        string Prc_Lat;
        string Prc_Long;
        string PRCID;
        string Prc_Dist;
        decimal Dst_Distance;
        decimal Vact_Range;
        Dst_Distance = Convert.ToDecimal(txt_distance.Text);
        Vact_Range = Convert.ToDecimal(txt_vctrange.Text);
        DataTable dt1 = new DataTable();
        
        dt1.Columns.AddRange(new DataColumn[]
        {
            new DataColumn("PCID",typeof(string)),
            new DataColumn("Prc_Lat",typeof(decimal)),
            new DataColumn("Prc_Long",typeof(decimal)),
            new DataColumn("Branch_Id",typeof(string)),
            new DataColumn("State_Id",typeof(string)),
            new DataColumn("Godown_ID",typeof(string)),
            new DataColumn("Godown_Name",typeof(string)),
            new DataColumn("Gdn_Lat",typeof(decimal)),
            new DataColumn("Gdn_Long",typeof(decimal)),
             new DataColumn("Distance",typeof(decimal)),
        });

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        // SqlCommand cmd = new SqlCommand("Select DISTINCT [PCID],[Latitude], [Longitude],[Dist] from [PPMS2019].[dbo].[ProcCntrLocation] where PCID='" + txt_pcid.Text+ "' and Latitude!='' and Longitude!=''", con);
        SqlCommand cmd = new SqlCommand("Select DISTINCT[PCID], pcn.[GodMandi_Name],[Latitude], [Longitude],[Dist],mdt.District_Name from[PPMS2019].[dbo].[ProcCntrLocation] as prc join Intergrated_MP_STORAGE.dbo.tbl_MetaData_DISTRICT as mdt on mdt.District_Id = prc.Dist join[PPMS2019].[dbo].[Procurement_Center] as pcn on pcn.Cntr_Id = prc.PCID where[PCID] = '" + txt_pcid.Text + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        cmd.ExecuteNonQuery();
        if (con.State == ConnectionState.Open)
        {
            con.Close();

        }
        Session["dt"] = dt;
        
        GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
        GoogleMapForASPNet1.GoogleMapObject.Width = "100%";
        GoogleMapForASPNet1.GoogleMapObject.Height = "700px";
        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 7;
        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", Convert.ToDouble(dt.Rows[0]["latitude"]), Convert.ToDouble(dt.Rows[0]["Longitude"]));
        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("2", Convert.ToDouble(dt.Rows[0]["latitude"]), Convert.ToDouble(dt.Rows[0]["Longitude"]));
        GooglePoint GP2 = new GooglePoint();
        GP2.ID = "ProcurementID" + 0 + 2.ToString();

        GP2.Latitude = CheckNull(dt.Rows[0]["Latitude"].ToString());
        GP2.Longitude = CheckNull(dt.Rows[0]["Longitude"].ToString());
        GP2.IconImage = "icons/Yellow.png";
        GP2.ToolTip = "Procurement Centre Id   :" + dt.Rows[0]["PCID"].ToString() + "\r\n"  + "Procurement Name   :" + dt.Rows[0]["GodMandi_Name"].ToString();
        GP2.InfoHTML = "<b> Procurement Centre Id : </b> " + dt.Rows[0]["PCID"].ToString() + "<br />" + "<b>Procurement Name   :</b>" + dt.Rows[0]["GodMandi_Name"].ToString();

        if (GP2.Latitude > 0 && GP2.Longitude > 0)
        {
            
            GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP2);

            
        }

        //Alternatively, you can also do it like this: for centre point
        /* GooglePoint GP = new GooglePoint();
         GP.ID = "1";
         GP.Latitude = 43.65669;
         GP.Longitude = -79.43270;
         GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP); */



        Session["Prc_Id"] = dt.Rows[0]["PCID"].ToString();
        Prc_Lat = dt.Rows[0]["Latitude"].ToString();
        Prc_Long = dt.Rows[0]["Longitude"].ToString();
        Prc_Dist = dt.Rows[0]["Dist"].ToString();
        PRCID = Session["Prc_Id"].ToString();        

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmd1 = new SqlCommand("[SP_Distance_Within_Dist_with_Gdn_Vacant_Cpty]", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@PCID", PRCID);
        cmd1.Parameters.AddWithValue("@PrcDist", Prc_Dist);
        cmd1.Parameters.AddWithValue("@DstDistance", Dst_Distance);
        cmd1.Parameters.AddWithValue("@VactRange", Vact_Range);
        cmd1.Parameters.AddWithValue("@PrcLat", Prc_Lat);
        cmd1.Parameters.AddWithValue("@PrcLong", Prc_Long);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        cmd1.CommandTimeout = 0;

        da1.Fill(dt1);
        cmd1.ExecuteNonQuery();

        if (con.State == ConnectionState.Open)
        {
            con.Close();

        }
        decimal Vact_capty = 0;
        if (dt1.Rows.Count > 0)
        {

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                Vact_capty = Convert.ToDecimal(dt1.Rows[i]["Godown_Capacity"].ToString()) - Convert.ToDecimal(dt1.Rows[i]["Utilized_Capacity"].ToString());
                GooglePoint GP1 = new GooglePoint();
                Boolean temp_red = false;
               
                GP1.ID = "GodownID" + i + 1.ToString();

                GP1.Latitude = CheckNull(dt1.Rows[i]["Latitude"].ToString());
                GP1.Longitude = CheckNull(dt1.Rows[i]["Longitude"].ToString());

                GP1.ToolTip = "Godown Name   :" + dt1.Rows[i]["Godown_Name"].ToString() + " \r\n  " + "Vacant Capacity   :" + dt1.Rows[i]["Vacant_Capacity"].ToString();

                decimal avi = 0;
                decimal forzero = 0;
                decimal forzero2 = 0;
                if (Convert.ToDecimal(dt1.Rows[i]["Utilized_Capacity"].ToString()) == 0)
                {
                    forzero = 1;
                }
                else
                {
                    forzero = Convert.ToDecimal(dt1.Rows[i]["Utilized_Capacity"].ToString());
                }
                if (Convert.ToDecimal(dt1.Rows[i]["Godown_Capacity"].ToString()) == 0)
                {
                    forzero2 = 1;
                }
                else
                {
                    forzero2 = Convert.ToDecimal(dt1.Rows[i]["Godown_Capacity"].ToString());
                }
                avi = (forzero * 100) / forzero2;
                if (CheckNullQty(dt1.Rows[i]["Utilized_Capacity"].ToString()) <= 0)
                {
                    if (Convert.ToDecimal(dt1.Rows[i]["Godown_Capacity"].ToString()) != 0)
                    {
                        temp_red = true;
                        GP1.IconImage = "/icons/Green.png";
                        
                    }
                    else
                    {
                        GP1.IconImage = "icons/wr.png";
                    }
                }

                else if (avi > 80)
                {
                    temp_red = false;
                    GP1.IconImage = "icons/Red.png";
                }
                else
                {
                    GP1.IconImage = "icons/Blue.png";
                }
                                
                GP1.InfoHTML = "<b> Godown Name : </b> " + dt1.Rows[i]["Godown_Name"].ToString() + "<br />" + "<b>Godown Capacity   :</b>" + dt1.Rows[i]["Godown_Capacity"].ToString()+ "<br />" + "Vacant Capacity   :" + dt1.Rows[i]["Vacant_Capacity"].ToString()+ "<br />" + "Distance   :" + dt1.Rows[i]["Arial_Distance"].ToString();
                if (GP1.Latitude > 0 && GP1.Longitude > 0)
                {
                    GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);
                }

                grd_Distance.DataSource = dt1;
                grd_Distance.DataBind();

            }

        }

        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Vacant godown not found.... '); </script> ");

        }
    }

   

    protected void txt_pcid_TextChanged(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmd = new SqlCommand("Select DISTINCT [PCID],pcn.[GodMandi_Name],[Latitude], [Longitude],[Dist],mdt.District_Name from[PPMS2019].[dbo].[ProcCntrLocation] as prc join tbl_MetaData_DISTRICT as mdt on mdt.District_Id = prc.Dist join[PPMS2019].[dbo].[Procurement_Center] as pcn on pcn.Cntr_Id = prc.PCID where[PCID] = '" + txt_pcid + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dt = new DataSet();
        da.Fill(dt);
        cmd.ExecuteNonQuery();

        if (dt.Tables[0].Rows.Count > 0)
        {

            lbl_distid.Text = dt.Tables[0].Rows[0]["Dist"].ToString();
            lbl_distname.Text = dt.Tables[0].Rows[0]["District_Name"].ToString();
            lbl_prcname.Text = dt.Tables[0].Rows[0]["GodMandi_Name"].ToString();

        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();

        }
    }

    protected void Inter_Dist_Distance_Vac_Capacity()
    {
        
        lbl_interdist.Visible = true;
        string Prc_Lat;
        string Prc_Long;
        string PRCID;
        decimal Vact_Range;
        decimal Pdistance;
        Pdistance = Convert.ToDecimal(txt_distance.Text);
        Vact_Range = Convert.ToDecimal(txt_vctrange.Text);
        DataTable dt1= new DataTable();
        dt1.Columns.AddRange(new DataColumn[]
        {
            new DataColumn("PCID",typeof(string)),
            new DataColumn("Prc_Lat",typeof(decimal)),
            new DataColumn("Prc_Long",typeof(decimal)),
            new DataColumn("Branch_Id",typeof(string)),
            new DataColumn("State_Id",typeof(string)),
            new DataColumn("Godown_ID",typeof(string)),
            new DataColumn("Godown_Name",typeof(string)),
            new DataColumn("Gdn_Lat",typeof(decimal)),
            new DataColumn("Gdn_Long",typeof(decimal)),
             new DataColumn("Distance",typeof(decimal)),
        });


        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        // SqlCommand cmd = new SqlCommand("Select DISTINCT [PCID],[Latitude], [Longitude],[Dist] from [PPMS2019].[dbo].[ProcCntrLocation] where PCID='" + txt_pcid.Text + "' and Latitude!='' and Longitude!=''", con);
        SqlCommand cmd = new SqlCommand("Select DISTINCT[PCID], pcn.[GodMandi_Name],[Latitude], [Longitude],[Dist],mdt.District_Name from[PPMS2019].[dbo].[ProcCntrLocation] as prc join tbl_MetaData_DISTRICT as mdt on mdt.District_Id = prc.Dist join[PPMS2019].[dbo].[Procurement_Center] as pcn on pcn.Cntr_Id = prc.PCID where[PCID] = '" + txt_pcid.Text + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        cmd.ExecuteNonQuery();
        if (con.State == ConnectionState.Open)
        {
            con.Close();

        }
        Session["dt"] = dt;
        GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
        GoogleMapForASPNet1.GoogleMapObject.Width = "100%";
        GoogleMapForASPNet1.GoogleMapObject.Height = "700px";
        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 7;
        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", Convert.ToDouble(dt.Rows[0]["latitude"]), Convert.ToDouble(dt.Rows[0]["Longitude"]));
        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("2", Convert.ToDouble(dt.Rows[0]["latitude"]), Convert.ToDouble(dt.Rows[0]["Longitude"]));
        GooglePoint GP2 = new GooglePoint();
        GP2.ID = "ProcurementID" + 0 + 2.ToString();

        GP2.Latitude = CheckNull(dt.Rows[0]["Latitude"].ToString());
        GP2.Longitude = CheckNull(dt.Rows[0]["Longitude"].ToString());
        GP2.IconImage = "icons/Yellow.png";
        GP2.ToolTip = "Procurement Centre Id   :" + dt.Rows[0]["PCID"].ToString() + "\r\n" + "Procurement Name   :" + dt.Rows[0]["GodMandi_Name"].ToString();
        GP2.InfoHTML = "<b> Procurement Centre Id : </b> " + dt.Rows[0]["PCID"].ToString() + "<br />" + "Procurement Name   :" + dt.Rows[0]["GodMandi_Name"].ToString();

        if (GP2.Latitude > 0 && GP2.Longitude > 0)
        {
            GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP2);
        }
             

        Session["Prc_Id"] = dt.Rows[0]["PCID"].ToString();
        Prc_Lat = dt.Rows[0]["Latitude"].ToString();
        Prc_Long = dt.Rows[0]["Longitude"].ToString();
        PRCID = Session["Prc_Id"].ToString();

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmd1 = new SqlCommand("[SP_Distance_Inter_District_with_Gdn_Vacant_Cpty]", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@PCID", PRCID);
        cmd1.Parameters.AddWithValue("@VactRange", Vact_Range);
        cmd1.Parameters.AddWithValue("@Distance", Pdistance);
        cmd1.Parameters.AddWithValue("@PrcLat", Prc_Lat);
        cmd1.Parameters.AddWithValue("@PrcLong", Prc_Long);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        cmd1.CommandTimeout = 0;

        da1.Fill(dt1);
        cmd1.ExecuteNonQuery();

        if (con.State == ConnectionState.Open)
        {
            con.Close();

        }

        decimal Vact_capty = 0;
        if (dt1.Rows.Count > 0)
        {
            
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                Vact_capty = Convert.ToDecimal(dt1.Rows[i]["Godown_Capacity"].ToString()) - Convert.ToDecimal(dt1.Rows[i]["Utilized_Capacity"].ToString());
                GooglePoint GP1 = new GooglePoint();
                Boolean temp_red = false;
                
                GP1.ID = "GodownID" + i + 1.ToString();

                GP1.Latitude = CheckNull(dt1.Rows[i]["Latitude"].ToString());
                GP1.Longitude = CheckNull(dt1.Rows[i]["Longitude"].ToString());
               
                GP1.ToolTip = "Godown Name   :" + dt1.Rows[i]["Godown_Name"].ToString() + " \r\n " + "Vacant Capacity   :" + dt1.Rows[i]["Vacant_Capacity"].ToString();

                decimal avi = 0;
                decimal forzero = 0;
                decimal forzero2 = 0;
                if (Convert.ToDecimal(dt1.Rows[i]["Utilized_Capacity"].ToString()) == 0)
                {
                    forzero = 1;
                }
                else
                {
                    forzero = Convert.ToDecimal(dt1.Rows[i]["Utilized_Capacity"].ToString());
                }
                if (Convert.ToDecimal(dt1.Rows[i]["Godown_Capacity"].ToString()) == 0)
                {
                    forzero2 = 1;
                }
                else
                {
                    forzero2 = Convert.ToDecimal(dt1.Rows[i]["Godown_Capacity"].ToString());
                }
                avi = (forzero * 100) / forzero2;
                if (CheckNullQty(dt1.Rows[i]["Utilized_Capacity"].ToString()) <= 0)
                {
                    if (Convert.ToDecimal(dt1.Rows[i]["Godown_Capacity"].ToString()) != 0)
                    {
                        temp_red = true;
                        GP1.IconImage = "/icons/Green.png";
                    }
                    else
                    {
                        GP1.IconImage = "icons/wr.png";
                    }
                }

                else if (avi > 80)
                {
                    temp_red = false;
                    GP1.IconImage = "icons/Red.png";
                }
                else
                {
                    GP1.IconImage = "icons/Blue.png";
                }

                 GP1.InfoHTML = "<b> Godown Name : </b> " + dt1.Rows[i]["Godown_Name"].ToString() + "<br />" + "Godown Capacity   :" + dt1.Rows[i]["Godown_Capacity"].ToString() + "<br />" + "Vacant Capacity   :" + dt1.Rows[i]["Vacant_Capacity"].ToString() + "<br />" + "Distance   :" + dt1.Rows[i]["Arial_Distance"].ToString();
                if (GP1.Latitude > 0 && GP1.Longitude > 0)
                {
                    GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);
                    
                }

                grd_Distance.DataSource = dt1;
                grd_Distance.DataBind();

            }

        }

        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Vacant godown not found.... '); </script> ");

        }
    }

    protected void btn_interdist_Click(object sender, EventArgs e)
    {            
        Inter_Dist_Distance_Vac_Capacity();
    }
}