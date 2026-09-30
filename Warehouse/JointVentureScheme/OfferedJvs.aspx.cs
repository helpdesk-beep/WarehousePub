using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text;

public partial class JointVentureScheme_OfferedJvs : System.Web.UI.Page
{
    public String GodownID;public decimal GodownCap;

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlCommand cmd = null;
    SqlTransaction sqltran;
    string R_Phase = "";
    string Reg_season = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        if (Session["Reg_No"] != null && Session["Reg_No"] != "")
        {
            R_Phase = "1";
            //Reg_season = "R2019";
            //Reg_season = "K2019";
            Reg_season = "JVS2021_22";
            if (!IsPostBack)
            {             
                // string TStatus = Tcheckdatetimes();
                 //if (TStatus == "Y")
                // {
                     //if (Session["District"].ToString() == "Shahdol" || Session["District"].ToString() == "Balaghat" || Session["District"].ToString() == "Katni")
                     //{
                         string chkregupdate = checkupdate();
                         // string DistrictStatus = "Y";
                         if (chkregupdate == "Y")
                         {
                             lbluser.Text = Session["fname"].ToString();
                             int SK = 0;
                             SK = CheckOffer();
                             //if (SK == 0)
                            // {
                                // GetRegGodown();
                                // trNewOfr.Visible = true;
                                // trparofr.Visible = false;
                               //  insertschemenNew();
                            // }
                            // else if (SK == 1)
                            // {
                                 GetRegGodownParticialOfr();
                                 trparofr.Visible = true;
                                 trNewOfr.Visible = false;
                                // insertschemen();
                            // }
                             GetRegDtl();
                         }
                         else
                         {
                             ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('आफ़र करने के लिए पहले अपना रजिस्ट्रेशन अपडेट करें...'); </script> ");
                         }
                     //}
                     //else
                     //{
                     //    pnlofferpopup.Visible = true;
                     //    ModalPopupExtender1.Show();
                     //}

                 //}
                 //else if (TStatus == "NS")
                 //{
                 //    //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can't Login Before 14/06/2016 11 AM ...!'); </script> ");
                 //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You can not Offer before 20/02/2021 11:00:00 AM'); </script> ");

                 //}
                 //else if (TStatus == "NE")
                 //{
                 //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Offer for Warehouse under Joint Venture Scheme has been closed..!'); </script> ");
                 //}
            }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registration Required for Offer...'); </script> ");
            Response.Redirect("WarehouseHome.aspx");
        }
        checkbtn();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("WarehouseHome.aspx");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("UserReg.aspx");
    }
    public int CheckOffer()
    {
        int ch = 0;
        //string strsql = "select Registration_Id,Offer_Id from tbl_Warehouse_Capacity_Offer_2020 where Registration_Id='" + Session["Reg_No"].ToString() + "'";
        string strsql = "select Registration_Id,Offer_Id from tbl_Warehouse_Capacity_Offer_2021 where Registration_Id='" + Session["Reg_No"].ToString() + "'";


        SqlDataAdapter da = new SqlDataAdapter(strsql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ch = 1;
            Session["PreviousOfr"] = "Yes";
        }
        else
        {
            ch = 0;
            Session["PreviousOfr"] = "No";
        }
        return ch;
    }


    public int checkbtn()
    {
        int ch = 0;
        //string strsql = "select Registration_Id,Offer_Id from tbl_Warehouse_Capacity_Offer_2020 where Registration_Id='" + Session["Reg_No"].ToString() + "'";
        string strsql = "select * from Tbl_JVS_Choise_Filling where Reg_ID='" + Session["Reg_No"].ToString() + "'";


        SqlDataAdapter da = new SqlDataAdapter(strsql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            btnsubmit.Visible = false;
        }
        else
        {
            btnsubmit.Visible = true;
        }
        return ch;
    }
    private void GetRegGodown()
    {
            string regno = Session["Reg_No"].ToString();
        //string qry = "select Godown_ID,Godown_No,convert(Decimal(10,2), G_Length) as G_Length,convert(Decimal(10,2), G_Width) as G_Width,convert(Decimal(10,2),G_Height) as G_Height,convert(Decimal(10,4),G_ScientificCapacity) as G_ScientificCapacity from tbl_WarehouseGodown_Reg where Godown_ID not in (select Godown_ID from tbl_Warehouse_Godown_Offer_2019 as GD where GD.Registration_Id='" + regno + "' ) and G_ScientificCapacity !=0 and Registration_Id='" + regno + "'";
        //string qry = "select Godown_ID,Godown_No,convert(Decimal(10,2), G_Length) as G_Length,convert(Decimal(10,2), G_Width) as G_Width,convert(Decimal(10,2),G_Height) as G_Height,convert(Decimal(10,4),G_ScientificCapacity) as G_ScientificCapacity from tbl_WarehouseGodown_Reg where Godown_ID not in (select Godown_ID from tbl_Warehouse_Godown_Offer_2020 as GD where GD.Registration_Id='" + regno + "' ) and G_ScientificCapacity !=0 and Registration_Id='" + regno + "'";
        string qry = "select Godown_ID,Godown_No,convert(Decimal(10,2), G_Length) as G_Length,convert(Decimal(10,2), G_Width) as G_Width,convert(Decimal(10,2),G_Height) as G_Height,convert(Decimal(10,4),G_ScientificCapacity) as G_ScientificCapacity from tbl_WarehouseGodown_Reg where Godown_ID not in (select Godown_ID from tbl_Warehouse_Godown_Offer_2021 as GD where GD.Registration_Id='" + regno + "' ) and G_ScientificCapacity !=0 and Registration_Id='" + regno + "'";

        SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGodown.DataSource = ds;
                gvGodown.DataBind();
                this.gvGodown.Columns[0].Visible = false;
                
            }
            else
            {
               ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Registration Not Completed /No Data Found ')", true);
            }
    }
    private void GetRegGodownParticialOfr()
    {
        string regno = Session["Reg_No"].ToString();
        //  Update on 19/02/2021
        string qry1 = "select sum(G_ScientificCapacity) as TCap from tbl_WarehouseGodown_Reg where Registration_Id='" + regno + "'";
        SqlCommand cmd1 = new SqlCommand(qry1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        Totalcaphdn.Value = ds1.Tables[0].Rows[0]["TCap"].ToString();
        Label2.Text = ds1.Tables[0].Rows[0]["TCap"].ToString();
        string qry = "SELECT GR.Godown_ID,GR.Godown_No,convert(Decimal(10,2), G_Length) as G_Length,convert(Decimal(10,2), G_Width) as G_Width,convert(Decimal(10,2),G_Height) as G_Height,convert(Decimal(10,4),G_ScientificCapacity) as G_ScientificCapacity, isnull(convert(decimal(18,4),G_ScientificCapacity),0) -isnull(convert(decimal(18,4),G_OfferCapacity),0) as AvlOfferedCpt FROM tbl_WarehouseGodown_Reg AS GR LEFT JOIN (select POFR.Godown_ID,isnull(OfrCpt,0)-isnull(IspeOfrCpt,0) G_OfferCapacity from (select OFR.Godown_ID,convert(decimal(18,4),isnull(SUM(OFR.G_OfferCapacity),0)) OfrCpt from tbl_Warehouse_Godown_Offer_2021 as OFR where OFR.Registration_Id='" + regno + "' group by OFR.Godown_ID) as POFR left join (select INSP.GodownId,isnull(sum(WGO.G_OfferCapacity),0) IspeOfrCpt from tbl_Godown_Inspection as INSP inner join tbl_Warehouse_Godown_Offer_2021 as WGO on WGO.Godown_Offer_Id=INSP.Godown_Offer_Id  where INSP.CreatedDate < CONVERT(varchar(10),'10/19/2018',101) and Fit_Unfit='UNFIT' and INSP.Registration_Id ='" + regno + "' group by INSP.GodownId) as INSPOFR on INSPOFR.GodownId=POFR.Godown_ID)  as GD on GR.Godown_ID=GD.Godown_ID where G_ScientificCapacity != 0 and GR.Registration_Id='" + regno + "' order by Godown_No asc";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GodId.Value = ds.Tables[0].Rows[0]["Godown_ID"].ToString();
        GodCAp.Value = ds.Tables[0].Rows[0]["G_ScientificCapacity"].ToString();

        if (ds.Tables[0].Rows.Count > 0)
        {
            GridViewParticialCpt.DataSource = ds;
            GridViewParticialCpt.DataBind();
            this.GridViewParticialCpt.Columns[0].Visible = false;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Godown Available for Offer')", true);
        }
    }
    private void GetRegDtl()
    {
        string regno = Session["Reg_No"].ToString();
        string qry = "select Registration_Id,Warehouse_Name,DistrictId,BranchId from tbl_WarehouseRegistration where Registration_Id='" + regno + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblWName.Text = ds.Tables[0].Rows[0]["Warehouse_Name"].ToString();
            lblRegNo.Text = ds.Tables[0].Rows[0]["Registration_Id"].ToString();
            lblDist.Text  = ds.Tables[0].Rows[0]["DistrictId"].ToString();
            lblbranch.Text = ds.Tables[0].Rows[0]["BranchId"].ToString();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Registration Not Completed /No Data Found ')", true);
        }
    }

    public string ChkOfferID()
    {
        string MaxOfrID = "";
        //string QueryMax = "select MAX(Offer_Id) as Offer_Id from tbl_Warehouse_Capacity_Offer_2020 where Registration_Id='" + Session["Reg_No"].ToString() + "'";
        string QueryMax = "select MAX(Offer_Id) as Offer_Id from tbl_Warehouse_Capacity_Offer_2021 where Registration_Id='" + Session["Reg_No"].ToString() + "'";

        cmd = new SqlCommand(QueryMax, con, sqltran);
        string str3 = cmd.ExecuteScalar().ToString();
        if ((str3 != String.Empty) || str3 != "")
        {
            //int lencount = Convert.ToInt32(str3.Length.ToString());
            //string part1 = str3.Substring(0, lencount - 2);
            //int partlen1 = Convert.ToInt32(part1.Length.ToString());
            //string part2 = str3.Substring(partlen1, lencount - partlen1);
            //MaxOfrID = part1 + Convert.ToString(Convert.ToInt32(part2) + 1);

            int lencount = Convert.ToInt32(str3.Length.ToString());
            string part1 = str3.Substring(0, lencount - 1);
            int partlen1 = Convert.ToInt32(part1.Length.ToString());
            string part2 = str3.Substring(partlen1, lencount - partlen1);
            //MaxOfrID = lblRegNo.Text + "19" + Convert.ToString(Convert.ToInt32(part2) + 1);
            //MaxOfrID = lblRegNo.Text + "20" + Convert.ToString(Convert.ToInt32(part2) + 1);
            MaxOfrID = lblRegNo.Text + "21" + Convert.ToString(Convert.ToInt32(part2) + 1);
        }
        else
        {
            //MaxOfrID = lblRegNo.Text + "191";
            //MaxOfrID = lblRegNo.Text + "201";
            MaxOfrID = lblRegNo.Text + "211";
        }
        return MaxOfrID;
    }
    public string Tcheckdatetimes()
    {
        DateTime ServerDate = new DateTime();
        //Test
        //DateTime _effective_date = Convert.ToDateTime("02/18/2018 00:05:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("02/26/2018 05:00:00 PM");
        //Actual

        DateTime _effective_date = Convert.ToDateTime("12/20/2021 11:59:00 AM");
        DateTime _Closing_date = Convert.ToDateTime("11/07/2023 11:59:00 PM");


       // DateTime _Closing_date = Convert.ToDateTime("01/07/2022 05:10:00 PM");

        // start 19/10/2018
        //DateTime _effective_date = Convert.ToDateTime("10/14/2019 11:59:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("10/30/2019 11:59:00 PM");
        //end 31/1/2019

        ////Old

        //   DateTime _effective_date = Convert.ToDateTime("2018-11-19 15:07:29.160");
        //   DateTime _Closing_date = Convert.ToDateTime("2019-11-30 15:07:29.160");

        string S = "";
        string QueryMax = "select getdate() as CDateTime";
        cmd = new SqlCommand(QueryMax, con);
        con.Open();
        string str3 = cmd.ExecuteScalar().ToString();
        con.Close();

        if ((str3 != String.Empty) || str3 != "")
        {
            ServerDate = Convert.ToDateTime(str3);
            ///Manage Time        
            //ServerDate = ServerDate.AddMinutes(-4);
            ServerDate = ServerDate.AddMinutes(-2);
        }
        if (ServerDate < _effective_date)
        {
            S = "NS";
        }
        else if (ServerDate > _Closing_date)
        {
            S = "NE";
        }
        else
        {
            S = "Y";
        }
        return S;
    }
    public string checkupdate()
    {
        
        string D = "";
        string QueryMax = "select IsActive from tbl_WarehouseRegistration where Registration_Id='" + Session["Reg_No"].ToString() + "' and UpdatedDate > CONVERT(varchar(10),'02/01/2019',101)";
        SqlDataAdapter da = new SqlDataAdapter(QueryMax, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            D = ds.Tables[0].Rows[0]["IsActive"].ToString().Trim();
        }
        else
        {
            D = "N";
        }
        return D;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {



        if (Session["Oft_Type"].ToString() == "FIRST")
        {
            string Roid = "../JointVentureScheme/PrintOffer.aspx?src=RO&vu=" + Session["Reg_No"].ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("<script>");
            sb.Append("window.open(");
            sb.Append("'" + Roid + "'");
            sb.Append(",'MyWindow', 'height=620,width=980');");
            sb.Append("</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(GetType(), "sb", sb.ToString());
           // Response.Redirect("PrintOffer.aspx");
        }
        else if (Session["Oft_Type"].ToString() == "SECOND")
        {
            string Roid = "../JointVentureScheme/PrintSecondOffer.aspx?src=RO&vu=" + Session["Reg_No"].ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("<script>");
            sb.Append("window.open(");
            sb.Append("'" + Roid + "'");
            sb.Append(",'MyWindow', 'height=620,width=980');");
            sb.Append("</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(GetType(), "sb", sb.ToString());
          //  Response.Redirect("PrintSecondOffer.aspx");
        }
    }
    public string CheckOfferSchem(string GDWN_No)
    {
        string ch = "";
       // string strsql = "select top 1 LicType from tbl_WarehouseGodown_Reg where Registration_Id='" + Session["Reg_No"].ToString() + "' and Godown_No='" + GDWN_No + "' order by CreatedDate desc";
        //string strsql = "select top 1  LicType,RoadType , GateType , W_BoundaryType , WeighBridge from tbl_WarehouseGodown_Reg as GREG left join  (select Registration_ID,case when RoadType='1' then 'BT' when RoadType='2' then 'CC' when RoadType='3' then 'WBM' end RoadType , GateType , W_BoundaryType , WeighBridge from tbl_WarehouseAdditionalinfo where Registration_Id='" + Session["Reg_No"].ToString() + "') as RAD on GREG.Registration_Id=RAD.Registration_ID where GREG.Registration_Id='" + Session["Reg_No"].ToString() + "' and Godown_No='" + GDWN_No + "' order by CreatedDate desc";
        string strsql = "select top 1 case when LicType='63' then '78' when LicType='68' then '83' else '0' end as LicType,RoadType , GateType , W_BoundaryType , WeighBridge from tbl_WarehouseGodown_Reg as GREG left join  (select Registration_ID,case when RoadType='1' then 'BT' when RoadType='2' then 'CC' when RoadType='3' then 'WBM' end RoadType , GateType , W_BoundaryType , WeighBridge from tbl_WarehouseAdditionalinfo where Registration_Id='" + Session["Reg_No"].ToString() + "') as RAD on GREG.Registration_Id=RAD.Registration_ID where GREG.Registration_Id='" + Session["Reg_No"].ToString() + "' and Godown_No='" + GDWN_No + "' order by CreatedDate desc";

        SqlDataAdapter da = new SqlDataAdapter(strsql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string Slictype = ds.Tables[0].Rows[0]["LicType"].ToString().Trim();
            string SRoadType = ds.Tables[0].Rows[0]["RoadType"].ToString().Trim();
            string SGateType = ds.Tables[0].Rows[0]["GateType"].ToString().Trim();
            string SW_BoundaryType = ds.Tables[0].Rows[0]["W_BoundaryType"].ToString().Trim();
            string SWeighBridge = ds.Tables[0].Rows[0]["WeighBridge"].ToString().Trim();

           // if (SRoadType != "WBM" && SWeighBridge == "True" && (Slictype == "68" || Slictype == "0") && SW_BoundaryType != "3" && SGateType == "1")
            if (SWeighBridge == "True" && SW_BoundaryType != "3" && SGateType == "1" && (SRoadType == "WBM" || SRoadType == "BT" || SRoadType == "CC"))
            {
                ch = "83";
            }
            else
            {
                ch = "78";
            }
            
            // ch = ds.Tables[0].Rows[0]["LicType"].ToString().Trim();
        }
        else
        {
            ch="0";
        }
        return ch;
    }


    protected void btnpayment_Click(object sender, EventArgs e)
    {
        string strsql = "";
        if (Session["Oft_Type"].ToString() == "FIRST")
        {
            strsql = "select reg.Registration_Id,Auth_Person,OreReg.MobileNo,OreReg.EmailID,Ofr.OfferAmt,Ofr.Offer_Capacity from tbl_WarehouseRegistration as Reg Inner join tbl_Warehouse_PreReg as OreReg on Reg.Registration_Id = OreReg.Reg_No left join (select Registration_Id,Offer_Capacity,OfferAmt from tbl_Warehouse_Capacity_Offer_2019 where Registration_Id='" + Session["Reg_No"].ToString() + "') as Ofr on Ofr.Registration_Id=Reg.Registration_Id where Reg.Registration_Id='" + Session["Reg_No"].ToString() + "' ";
        }
        else if (Session["Oft_Type"].ToString() == "SECOND")
        {
            strsql = "select reg.Registration_Id,Auth_Person,OreReg.MobileNo,OreReg.EmailID,Ofr.OfferAmt,Ofr.Offer_Capacity from tbl_WarehouseRegistration as Reg Inner join tbl_Warehouse_PreReg as OreReg on Reg.Registration_Id = OreReg.Reg_No left join (select Registration_Id,Offer_Capacity,OfferAmt from tbl_Warehouse_Capacity_Offer_2019 where Registration_Id='" + Session["Reg_No"].ToString() + "' and Offer_Id='" + Session["ofrid"].ToString().Trim()  + "') as Ofr on Ofr.Registration_Id=Reg.Registration_Id where Reg.Registration_Id='" + Session["Reg_No"].ToString() + "' ";
        }
        SqlCommand cmd = new SqlCommand(strsql, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblRegID.Text = dt.Rows[0]["Registration_Id"].ToString().Trim();
            lblOwn.Text = dt.Rows[0]["Auth_Person"].ToString().Trim();
            lblcontact.Text = dt.Rows[0]["MobileNo"].ToString().Trim();
            lblemailid.Text = dt.Rows[0]["EmailID"].ToString().Trim();
            lblRegCapacity.Text = dt.Rows[0]["Offer_Capacity"].ToString().Trim();
            lblRegFee.Text = dt.Rows[0]["OfferAmt"].ToString().Trim();
        }
        ModalPopupExtender2.Show();
      //  Response.Redirect("https://www.onlinesbi.com/sbicollect/icollecthome.htm");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://www.onlinesbi.com/sbicollect/icollecthome.htm");
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string TStatus = Tcheckdatetimes();
        if (TStatus == "Y")
        { 
        string client_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;

       if(CheckBox1.Checked==true)
        {

        if(RadioButton1.Checked==true)
        {
            RedioHiddenField.Value = "A";
        }

        if (RadioButton2.Checked == true)
        {
            RedioHiddenField.Value = "B";
        }


        if (RedioHiddenField.Value != "")
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_Jvs_Choise_Filling", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Reg_ID", lblRegNo.Text);
                    cmd.Parameters.AddWithValue("@Dist_ID", lblDist.Text);
                    cmd.Parameters.AddWithValue("@Branch_ID", lblbranch.Text);
                    //  cmd.Parameters.AddWithValue("@Warehouse_Name", lblWName.Text);          
                    cmd.Parameters.AddWithValue("@WareHouse_Cap", Totalcaphdn.Value);
                    cmd.Parameters.AddWithValue("@Choice", RedioHiddenField.Value);
                    cmd.Parameters.AddWithValue("@IPadd", client_IP);
                    //cmd.Parameters.AddWithValue("@Godown_Id", GodId.Value);
                    // cmd.Parameters.AddWithValue("@type", 1); 

                    con.Open();
                    int k = cmd.ExecuteNonQuery();
                    con.Close();

                    //fill();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Insert Successfully'); </script> ");
                    btnsubmit.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा सफलतापूर्वक श्रेणी का चयन किया जा चुका हैं| क्रपया प्रिन्ट लेवे| ')", true);
                    btnPrint.Visible = true;
                 

                }
            }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('श्रेणी का चयन करे'); </script> ");
        }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('चेक बॉक्स का चयन करे'); </script> ");
        }
        }

        else 
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Date for Choice Filling has been Finished...'); </script> ");
        }
    }

  

    protected void btnPrint_Click1(object sender, EventArgs e)
    {
      //  if (Session["Oft_Type"].ToString() == "FIRST")
        //{
            string Roid = "../JointVentureScheme/PrintOfferJVS.aspx?src=RO&vu=" + Session["Reg_No"].ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("<script>");
            sb.Append("window.open(");
            sb.Append("'" + Roid + "'");
            sb.Append(",'MyWindow', 'height=620,width=980');");
            sb.Append("</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(GetType(), "sb", sb.ToString());
            // Response.Redirect("PrintOffer.aspx");
       // }
       // else if (Session["Oft_Type"].ToString() == "SECOND")
       // {
           // string Roid = "../JointVentureScheme/PrintSecondOffer.aspx?src=RO&vu=" + Session["Reg_No"].ToString();
           // StringBuilder sb = new StringBuilder();
           // sb.Append("<script>");
           // sb.Append("window.open(");
           // sb.Append("'" + Roid + "'");
           // sb.Append(",'MyWindow', 'height=620,width=980');");
           // sb.Append("</script>");
        //   this.Page.ClientScript.RegisterClientScriptBlock(GetType(), "sb", sb.ToString());
            //  Response.Redirect("PrintSecondOffer.aspx");
       // }
    }
}
