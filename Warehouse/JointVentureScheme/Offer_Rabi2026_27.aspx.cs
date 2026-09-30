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

public partial class JointVentureScheme_Offer_Rabi2026_27 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlCommand cmd = null;
    SqlTransaction sqltran;
    string R_Phase = "";
    string Reg_season = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lbljvschois.Text = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        //if(!IsPostBack)
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('आफ़र करने कि window बंद हो चुकि हैं ...'); </script> ");
        //}
        if (Session["Reg_No"] != null && Session["Reg_No"] != "")
        {
            CheckLicencevalidornotvalid();
            CheckAllreadyExist();
            CheckAllreadyStockinWarehouse();
            //New Method For 
            //CheckOtherStockDetailsInWarehouse();
            R_Phase = "1";
            //Reg_season = "R2019";
            //Reg_season = "K2019";
            //Reg_season = "JVS2022_23";
            Reg_season = "Rab2026_27";
            //JVSREG();
            if (!IsPostBack)
            {

                string TStatus = Tcheckdatetimes();
                if (TStatus == "Y")
                {
                    //if (Session["District"].ToString() == "Shahdol" || Session["District"].ToString() == "Balaghat" || Session["District"].ToString() == "Katni")
                    //{
                    string chkregupdate = checkupdate();
                    // string DistrictStatus = "Y";
                    if (chkregupdate == "Y")
                    {
                        lbluser.Text = Session["fname"].ToString();
                        int SK = 0;
                        SK = CheckOffer();
                        if (SK == 0)
                        {
                            GetRegGodown();
                            trNewOfr.Visible = true;
                            trparofr.Visible = false;
                            insertschemenNew();
                        }
                        else if (SK == 1)
                        {
                            GetRegGodownParticialOfr();
                            trparofr.Visible = true;
                            trNewOfr.Visible = false;
                            insertschemen();
                        }
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

                }
                else if (TStatus == "NS")
                {
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can't Login Before 14/06/2016 11 AM ...!'); </script> ");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You can not Offer before 08/03/2022 11:00:00 AM'); </script> ");

                }
                else if (TStatus == "NE")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Offer for Warehouse under Joint Venture Scheme has been closed..!'); </script> ");
                }
            }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registration Required for Offer...'); </script> ");
            Response.Redirect("WarehouseHome.aspx");
        }

    }

    public void CheckAllreadyStockinWarehouse()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand("[dbo].[Get_Wheat_Stock_Position_For_Offer_2025_26]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RegistrationID", Session["Reg_No"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //if (dt.Rows[0]["JVS_RegNo"].ToString() != "0" && Convert.ToInt32(dt.Rows[0]["Percentage"].ToString()) == 0)
            //{

            //}
            //else if (dt.Rows[0]["JVS_RegNo"].ToString() != "0" && (Convert.ToInt32(dt.Rows[0]["Percentage"].ToString()) <= 75))
            ////else if ((Convert.ToInt32(dt.Rows[0]["Percentage"].ToString()) <= 25))
            ////else if (dt.Rows[0]["JVS_RegNo"].ToString() != "0" && (Convert.ToInt32(dt.Rows[0]["Percentage"].ToString()) >= 25))
            //{
            //    //string strMsg = "जिन जमाकर्ताओं की Vacant Capacity 25% से अधिक रिक्त है केवल वे जमाकर्ता ही अपनी क्षमता इस रबी सीजन (2024-25) के लिए ऑफर कर सकते हैं।। ";
            //    ////ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/JointVentureScheme/WarehouseHome.aspx';", true);
            //}
            //else if (dt.Rows[0]["JVS_RegNo"].ToString() == "0" && Convert.ToInt32(dt.Rows[0]["Percentage"].ToString()) == 0)
            //{

            //}
            //else
            //{
            //    string strMsg = "प्रिय गोदाम संचालक अभी आप ऑफर नहीं कर पाएंगे, क्योंकि आपके गोदाम में 25% से अधिक स्कंध भंडारित हैं।। ";
            //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/JointVentureScheme/WarehouseHome.aspx';", true);
            //}
            // txt_inspdate.Text = dt.Rows[0]["Inspection_Date"].ToString();
            //if (dt.Rows[0]["WeightBalance"].ToString() == "0")
            //{
            //if (dt.Rows[0]["LicNum"].ToString() != "")
            //{
            //    if (dt.Rows[0]["Is_allowed"].ToString() == "Y")
            //    {
            //        ddlPMS.SelectedValue = "2";
            //        ddlPMS.Enabled = false;


            //    }
            //    else
            //    {
            //        string strMsg = "प्रिय गोदाम संचालक अभी आप ऑफर नहीं कर पाएंगे, क्योंकि आपके गोदाम में कीटग्रस्ता पाई गई होगी या आपके द्वारा स्कंध के भुगतान में व्यवधान उत्पन्न किया गया होगा।  ।";
            //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/JointVentureScheme/WarehouseHome.aspx';", true);
            //    }
            //}

        }
        else
        {
            string strMsg = "प्रिय गोदाम संचालक अभी आप ऑफर नहीं कर पाएंगे, क्योंकि आपके गोदाम में कीटग्रस्ता पाई गई होगी या आपके द्वारा स्कंध के भुगतान में व्यवधान उत्पन्न किया गया होगा।  ।";
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/JointVentureScheme/WarehouseHome.aspx';", true);
        }

    }

    public void CheckLicencevalidornotvalid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand("[dbo].[Get_Licence_Validity_For_offer]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Registration_ID", Session["Reg_No"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            // txt_inspdate.Text = dt.Rows[0]["Inspection_Date"].ToString();
            //if (dt.Rows[0]["Obstruction_BYGO"].ToString() == "Y" || dt.Rows[0]["Warehouse_Infested_Status"].ToString() == "Y")
            //{
            if (dt.Rows[0]["Offer_Status_Flag"].ToString() == "Y")
            {
                //if (dt.Rows[0]["LicNum"].ToString() != "")
                //{

                //    string strMsg = "प्रिय गोदाम संचालक अभी आप ऑफर नहीं कर पाएंगे, क्योंकि आपके गोदाम में कीटग्रस्ता पाई गई होगी या आपके द्वारा स्कंध के भुगतान में व्यवधान उत्पन्न किया गया होगा।  ।";
                //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/JointVentureScheme/WarehouseHome.aspx';", true);

                //    if (dt.Rows[0]["Is_allowed"].ToString() == "Y")
                //    {
                //        //ddlPMS.SelectedValue = "2";
                //        ddlPMS.Enabled = true;
                //    }
                //    //else
                //    //{
                //    //    string strMsg2 = "प्रिय गोदाम संचालक अभी आप ऑफर नहीं कर पाएंगे, क्योंकि आपके गोदाम में कीटग्रस्ता पाई गई होगी या आपके द्वारा स्कंध के भुगतान में व्यवधान उत्पन्न किया गया होगा।  ।";
                //    //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
                //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg2 + "');window.location ='/warehouse/JointVentureScheme/WarehouseHome.aspx';", true);
                //    //}
                //}
            }
            else if (dt.Rows[0]["Offer_Status_Flag"].ToString() == "N")
            {
                string strMsg = "आपके गोदाम का लाइसेंस अमान्य या समाप्त होने वाला है। ऑफर करने के लिए कृपया अपना लाइसेंस रिन्यू/अपडेट करें। असुविधा के लिए खेद है।";
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/JointVentureScheme/WarehouseHome.aspx';", true);
            }
            //}


            //else
            //{
            //    string strMsg = "प्रिय गोदाम संचालक अभी आप ऑफर नहीं कर पाएंगे, क्योंकि आपके गोदाम में कीटग्रस्ता पाई गई होगी या आपके द्वारा स्कंध के भुगतान में व्यवधान उत्पन्न किया गया होगा।  ।";
            //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/JointVentureScheme/WarehouseHome.aspx';", true);
            //}
        }

    }

    public void CheckAllreadyExist()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand("[dbo].[Get_Private_Warehouse_Related_Information_Check]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Registration_ID", Session["Reg_No"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            // txt_inspdate.Text = dt.Rows[0]["Inspection_Date"].ToString();
            //if (dt.Rows[0]["Obstruction_BYGO"].ToString() == "Y" || dt.Rows[0]["Warehouse_Infested_Status"].ToString() == "Y")
            //{
                if (dt.Rows[0]["Is_allowed"].ToString() == "Y")
                {
                    //if (dt.Rows[0]["LicNum"].ToString() != "")
                    //{

                    //    string strMsg = "प्रिय गोदाम संचालक अभी आप ऑफर नहीं कर पाएंगे, क्योंकि आपके गोदाम में कीटग्रस्ता पाई गई होगी या आपके द्वारा स्कंध के भुगतान में व्यवधान उत्पन्न किया गया होगा।  ।";
                    //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/JointVentureScheme/WarehouseHome.aspx';", true);

                    //    if (dt.Rows[0]["Is_allowed"].ToString() == "Y")
                    //    {
                    //        //ddlPMS.SelectedValue = "2";
                    //        ddlPMS.Enabled = true;
                    //    }
                    //    //else
                    //    //{
                    //    //    string strMsg2 = "प्रिय गोदाम संचालक अभी आप ऑफर नहीं कर पाएंगे, क्योंकि आपके गोदाम में कीटग्रस्ता पाई गई होगी या आपके द्वारा स्कंध के भुगतान में व्यवधान उत्पन्न किया गया होगा।  ।";
                    //    //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
                    //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg2 + "');window.location ='/warehouse/JointVentureScheme/WarehouseHome.aspx';", true);
                    //    //}
                    //}
                }
                else
                {
                    string strMsg = "प्रिय गोदाम संचालक अभी आप ऑफर नहीं कर पाएंगे, क्योंकि आपके गोदाम में कीटग्रस्ता पाई गई होगी या आपके द्वारा स्कंध के भुगतान में व्यवधान उत्पन्न किया गया होगा।  ।";
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/JointVentureScheme/WarehouseHome.aspx';", true);
                }
            //}


            //else
            //{
            //    string strMsg = "प्रिय गोदाम संचालक अभी आप ऑफर नहीं कर पाएंगे, क्योंकि आपके गोदाम में कीटग्रस्ता पाई गई होगी या आपके द्वारा स्कंध के भुगतान में व्यवधान उत्पन्न किया गया होगा।  ।";
            //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/JointVentureScheme/WarehouseHome.aspx';", true);
            //}
        }

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
        //string strsql = "select Registration_Id,Offer_Id from tbl_Warehouse_Capacity_Offer_Rabi_2024_25 where Registration_Id='" + Session["Reg_No"].ToString() + "'";
        string strsql = "select Registration_Id,Offer_Id from tbl_Warehouse_Capacity_Offer_Rabi_2026_27 where Registration_Id='" + Session["Reg_No"].ToString() + "'";


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

    private void JVSREG()
    {
        string regno = Session["Reg_No"].ToString();
        //string qry = "select Godown_ID,Godown_No,convert(Decimal(10,2), G_Length) as G_Length,convert(Decimal(10,2), G_Width) as G_Width,convert(Decimal(10,2),G_Height) as G_Height,convert(Decimal(10,4),G_ScientificCapacity) as G_ScientificCapacity from tbl_WarehouseGodown_Reg where Godown_ID not in (select Godown_ID from tbl_Warehouse_Godown_Offer_2019 as GD where GD.Registration_Id='" + regno + "' ) and G_ScientificCapacity !=0 and Registration_Id='" + regno + "'";
        //string qry = "select Godown_ID,Godown_No,convert(Decimal(10,2), G_Length) as G_Length,convert(Decimal(10,2), G_Width) as G_Width,convert(Decimal(10,2),G_Height) as G_Height,convert(Decimal(10,4),G_ScientificCapacity) as G_ScientificCapacity from tbl_WarehouseGodown_Reg where Godown_ID not in (select Godown_ID from tbl_Warehouse_Godown_Offer_2020 as GD where GD.Registration_Id='" + regno + "' ) and G_ScientificCapacity !=0 and Registration_Id='" + regno + "'";
        string qry = "select case when choice= 'A' then N'अ' else N'ब' end as choice   from Tbl_JVS_Choise_Filling where Reg_ID='" + regno + "'";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lbljvschois.Text = ds.Tables[0].Rows[0]["choice"].ToString();

        }
        else
        {
            lbljvschois.Text = "NO CHOICE FILLING";
        }

        if (lbljvschois.Text == "ब")
        {
            jvsB.Visible = true;
        }
        else
        {
            jvsA.Visible = true;

        }

    }

    private void GetRegGodown()
    {
        string regno = Session["Reg_No"].ToString();
        //string qry = "select Godown_ID,Godown_No,convert(Decimal(10,2), G_Length) as G_Length,convert(Decimal(10,2), G_Width) as G_Width,convert(Decimal(10,2),G_Height) as G_Height,convert(Decimal(10,4),G_ScientificCapacity) as G_ScientificCapacity from tbl_WarehouseGodown_Reg where Godown_ID not in (select Godown_ID from tbl_Warehouse_Godown_Offer_2019 as GD where GD.Registration_Id='" + regno + "' ) and G_ScientificCapacity !=0 and Registration_Id='" + regno + "'";
        //string qry = "select Godown_ID,Godown_No,convert(Decimal(10,2), G_Length) as G_Length,convert(Decimal(10,2), G_Width) as G_Width,convert(Decimal(10,2),G_Height) as G_Height,convert(Decimal(10,4),G_ScientificCapacity) as G_ScientificCapacity from tbl_WarehouseGodown_Reg where Godown_ID not in (select Godown_ID from tbl_Warehouse_Godown_Offer_2020 as GD where GD.Registration_Id='" + regno + "' ) and G_ScientificCapacity !=0 and Registration_Id='" + regno + "'";
        string qry = "select Godown_ID,Godown_No,convert(Decimal(10,2), G_Length) as G_Length,convert(Decimal(10,2), G_Width) as G_Width,convert(Decimal(10,2),G_Height) as G_Height,convert(Decimal(10,4),G_ScientificCapacity) as G_ScientificCapacity from tbl_WarehouseGodown_Reg where Godown_ID not in (select Godown_ID from tbl_Warehouse_Godown_Offer_Rabi_2026_27 as GD where GD.Registration_Id='" + regno + "' ) and G_ScientificCapacity !=0 and Registration_Id='" + regno + "'";

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
        //string qry = "SELECT GR.Godown_ID,GR.Godown_No,convert(Decimal(10,2), G_Length) as G_Length,convert(Decimal(10,2), G_Width) as G_Width,convert(Decimal(10,2),G_Height) as G_Height,convert(Decimal(10,4),G_ScientificCapacity) as G_ScientificCapacity, isnull(convert(decimal(18,4),G_ScientificCapacity),0) -isnull(convert(decimal(18,4),G_OfferCapacity),0) as AvlOfferedCpt FROM tbl_WarehouseGodown_Reg AS GR LEFT JOIN (select POFR.Godown_ID,isnull(OfrCpt,0)-isnull(IspeOfrCpt,0) G_OfferCapacity from (select OFR.Godown_ID,convert(decimal(18,4),isnull(SUM(OFR.G_OfferCapacity),0)) OfrCpt from tbl_Warehouse_Godown_Offer_2020 as OFR where OFR.Registration_Id='" + regno + "' group by OFR.Godown_ID) as POFR left join (select INSP.GodownId,isnull(sum(WGO.G_OfferCapacity),0) IspeOfrCpt from tbl_Godown_Inspection as INSP inner join tbl_Warehouse_Godown_Offer_2020 as WGO on WGO.Godown_Offer_Id=INSP.Godown_Offer_Id  where INSP.CreatedDate < CONVERT(varchar(10),'10/19/2018',101) and Fit_Unfit='UNFIT' and INSP.Registration_Id ='" + regno + "' group by INSP.GodownId) as INSPOFR on INSPOFR.GodownId=POFR.Godown_ID)  as GD on GR.Godown_ID=GD.Godown_ID where G_ScientificCapacity != 0 and GR.Registration_Id='" + regno + "' order by Godown_No asc";
        string qry = "SELECT GR.Godown_ID,GR.Godown_No,convert(Decimal(10,2), G_Length) as G_Length,convert(Decimal(10,2), G_Width) as G_Width,convert(Decimal(10,2),G_Height) as G_Height,convert(Decimal(10,4),G_ScientificCapacity) as G_ScientificCapacity, isnull(convert(decimal(18,4),G_ScientificCapacity),0) -isnull(convert(decimal(18,4),G_OfferCapacity),0) as AvlOfferedCpt FROM tbl_WarehouseGodown_Reg AS GR LEFT JOIN (select POFR.Godown_ID,isnull(OfrCpt,0)-isnull(IspeOfrCpt,0) G_OfferCapacity from (select OFR.Godown_ID,convert(decimal(18,4),isnull(SUM(OFR.G_OfferCapacity),0)) OfrCpt from tbl_Warehouse_Godown_Offer_Rabi_2026_27 as OFR where OFR.Registration_Id='" + regno + "' group by OFR.Godown_ID) as POFR left join (select INSP.GodownId,isnull(sum(WGO.G_OfferCapacity),0) IspeOfrCpt from tbl_Godown_Inspection as INSP inner join tbl_Warehouse_Godown_Offer_Rabi_2026_27 as WGO on WGO.Godown_Offer_Id=INSP.Godown_Offer_Id  where INSP.CreatedDate < CONVERT(varchar(10),'10/19/2018',101) and Fit_Unfit='UNFIT' and INSP.Registration_Id ='" + regno + "' group by INSP.GodownId) as INSPOFR on INSPOFR.GodownId=POFR.Godown_ID)  as GD on GR.Godown_ID=GD.Godown_ID where G_ScientificCapacity != 0 and GR.Registration_Id='" + regno + "' order by Godown_No asc";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
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
            lblDist.Text = ds.Tables[0].Rows[0]["DistrictId"].ToString();
            lblbranch.Text = ds.Tables[0].Rows[0]["BranchId"].ToString();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Registration Not Completed /No Data Found ')", true);
        }
    }
    protected void ckstack_CheckedChanged(object sender, EventArgs e)
    {
        //Get_Selection();
        int s;
        int Count_Rows;
        string RowNumber;
        Count_Rows = gvGodown.Rows.Count;
        for (s = 0; s < gvGodown.Rows.Count; s++)
        {

            if (((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked == true)
            {
                RowNumber = gvGodown.Rows[s].Cells[0].Text.ToString();
                string G_ofrdCpt = gvGodown.Rows[s].Cells[5].Text.ToString();
                ((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Text = G_ofrdCpt.ToString();
                ((CheckBox)gvGodown.Rows[s].FindControl("chkParCpt")).Checked = false;
                ((CheckBox)gvGodown.Rows[s].FindControl("chkParCpt")).Enabled = false;
                ((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Enabled = false;
            }
            else
            {
                if (((CheckBox)gvGodown.Rows[s].FindControl("chkParCpt")).Checked == false && ((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked == false)
                {
                    ((CheckBox)gvGodown.Rows[s].FindControl("chkParCpt")).Enabled = true;
                    ((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Text = "0";
                }
            }
            txtTotlcpt.Value = "0.00";
            txtOfferAmt.Value = "0.00";
        }
    }
    protected void chkParCpt_CheckedChanged(object sender, EventArgs e)
    {
        //int s;
        //int Count_Rows;
        //Count_Rows = gvGodown.Rows.Count;
        //for (s = 0; s < gvGodown.Rows.Count; s++)
        //{
        //    if (((CheckBox)gvGodown.Rows[s].FindControl("chkParCpt")).Checked == true)
        //    {
        //        ((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Enabled = true;
        //    }
        //    if (((CheckBox)gvGodown.Rows[s].FindControl("chkParCpt")).Checked == false)
        //    {
        //        ((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Enabled = false;
        //    }
        //    if (((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked == false && ((CheckBox)gvGodown.Rows[s].FindControl("chkParCpt")).Checked == false)
        //    {
        //        ((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Text = "0";
        //    }
        //    txtTotlcpt.Value = "0.00";
        //    txtOfferAmt.Value = "0.00";
        //}
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Can not Partially Offer')", true);
    }
    protected void ckstackPar_CheckedChanged(object sender, EventArgs e)
    {
        int s;
        int Count_Rows;
        string RowNumber;
        Count_Rows = GridViewParticialCpt.Rows.Count;
        for (s = 0; s < GridViewParticialCpt.Rows.Count; s++)
        {

            if (((CheckBox)GridViewParticialCpt.Rows[s].FindControl("ckstackPar")).Checked == true)
            {
                if (Convert.ToDecimal(GridViewParticialCpt.Rows[s].Cells[6].Text.ToString()) > 0)
                {
                    RowNumber = GridViewParticialCpt.Rows[s].Cells[0].Text.ToString();
                    string G_ofrdCpt = GridViewParticialCpt.Rows[s].Cells[6].Text.ToString();
                    ((TextBox)GridViewParticialCpt.Rows[s].FindControl("txtCapacity")).Text = G_ofrdCpt.ToString();
                    ((CheckBox)GridViewParticialCpt.Rows[s].FindControl("chkParCptPar")).Checked = false;
                    ((CheckBox)GridViewParticialCpt.Rows[s].FindControl("chkParCptPar")).Enabled = false;
                    ((TextBox)GridViewParticialCpt.Rows[s].FindControl("txtCapacity")).Enabled = false;
                }
                else
                {
                    ((CheckBox)GridViewParticialCpt.Rows[s].FindControl("ckstackPar")).Checked = false;
                    ((CheckBox)GridViewParticialCpt.Rows[s].FindControl("chkParCptPar")).Enabled = true;
                    ((TextBox)GridViewParticialCpt.Rows[s].FindControl("txtCapacity")).Text = "0";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You can Not Offer Zero Avialble Capacity Godown ')", true);
                }
            }
            else
            {
                if (((CheckBox)GridViewParticialCpt.Rows[s].FindControl("chkParCptPar")).Checked == false && ((CheckBox)GridViewParticialCpt.Rows[s].FindControl("ckstackPar")).Checked == false)
                {
                    ((CheckBox)GridViewParticialCpt.Rows[s].FindControl("chkParCptPar")).Enabled = true;
                    ((TextBox)GridViewParticialCpt.Rows[s].FindControl("txtCapacity")).Text = "0";
                }
            }
            txtTotlcpt.Value = "0.00";
            txtOfferAmt.Value = "0.00";
        }
    }
    protected void chkParCptPar_CheckedChanged(object sender, EventArgs e)
    {
        //int s;
        //int Count_Rows;
        //Count_Rows = GridViewParticialCpt.Rows.Count;
        //for (s = 0; s < GridViewParticialCpt.Rows.Count; s++)
        //{
        //    if (((CheckBox)GridViewParticialCpt.Rows[s].FindControl("chkParCptPar")).Checked == true)
        //    {
        //        ((TextBox)GridViewParticialCpt.Rows[s].FindControl("txtCapacity")).Enabled = true;
        //    }
        //    if (((CheckBox)GridViewParticialCpt.Rows[s].FindControl("chkParCptPar")).Checked == false)
        //    {
        //        ((TextBox)GridViewParticialCpt.Rows[s].FindControl("txtCapacity")).Enabled = false;
        //    }
        //    if (((CheckBox)GridViewParticialCpt.Rows[s].FindControl("ckstackPar")).Checked == false && ((CheckBox)GridViewParticialCpt.Rows[s].FindControl("chkParCptPar")).Checked == false)
        //    {
        //        ((TextBox)GridViewParticialCpt.Rows[s].FindControl("txtCapacity")).Text = "0";
        //    }
        //    if (Convert.ToDecimal(GridViewParticialCpt.Rows[s].Cells[6].Text.ToString()) == 0 && ((CheckBox)GridViewParticialCpt.Rows[s].FindControl("chkParCptPar")).Checked == true)
        //    {
        //        ((TextBox)GridViewParticialCpt.Rows[s].FindControl("txtCapacity")).Enabled = false;
        //        ((CheckBox)GridViewParticialCpt.Rows[s].FindControl("chkParCptPar")).Checked = false;
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You can Not Offer Zero Available Capacity Godown ')", true);
        //    }

        //    txtTotlcpt.Value = "0.00";
        //    txtOfferAmt.Value = "0.00";
        //}
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Can not Partially Offer')", true);

    }

    protected void btnCalCpt_Click(object sender, EventArgs e)
    {
        //Get_Selection();
        int s = 0;
        Decimal totalcpt = 0;
        Decimal TotalAmt = 0;
        int Count_Rows;
        if (trparofr.Visible == false && trNewOfr.Visible == true && Session["PreviousOfr"].ToString() == "No")
        {
            Count_Rows = gvGodown.Rows.Count;
            for (s = 0; s < gvGodown.Rows.Count; s++)
            {

                if ((((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked == true || ((CheckBox)gvGodown.Rows[s].FindControl("chkParCpt")).Checked == true) && Convert.ToDecimal(Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Text.ToString())) > 0)
                {
                    if (Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Text.ToString()) <= Convert.ToDecimal(gvGodown.Rows[s].Cells[5].Text.ToString()))
                    {
                        //if (((DropDownList)gvGodown.Rows[s].FindControl("ddlScheme")).SelectedItem.Text.ToString() != "Select")
                        //{
                        Decimal cpt = Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Text.ToString());
                        totalcpt = cpt + totalcpt;
                        txtTotlcpt.Value = Convert.ToString(totalcpt);
                        // TotalAmt = (totalcpt * Convert.ToDecimal(1));
                        TotalAmt = Math.Round(totalcpt * Convert.ToDecimal(1));
                        txtOfferAmt.Value = TotalAmt.ToString();

                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Offer Scheme ')", true);
                        //    for (int j = s + 1; j < gvGodown.Rows.Count; j++)
                        //    {
                        //        ((TextBox)gvGodown.Rows[j].FindControl("txtCapacity")).Text = "0";
                        //        ((TextBox)gvGodown.Rows[j].FindControl("txtCapacity")).Enabled = false;
                        //        ((CheckBox)gvGodown.Rows[j].FindControl("chkParCpt")).Checked = false;
                        //        ((CheckBox)gvGodown.Rows[j].FindControl("ckstack")).Checked = false;
                        //    }
                        //    txtTotlcpt.Value = "0.00";
                        //    txtOfferAmt.Value = "0.00";
                        //}
                    }
                    else
                    {
                        txtTotlcpt.Value = "0.00";
                        txtOfferAmt.Value = "0.00";
                        ((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Text = "0";
                        ((CheckBox)gvGodown.Rows[s].FindControl("chkParCpt")).Checked = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Entered Capacity Greater than the Total Capacity ')", true);
                    }
                }
                else
                {
                    ((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Text = "0";
                    ((CheckBox)gvGodown.Rows[s].FindControl("chkParCpt")).Checked = false;
                }
            }
        }
        else if (trparofr.Visible == true && trNewOfr.Visible == false && Session["PreviousOfr"].ToString() == "Yes")
        {
            Count_Rows = GridViewParticialCpt.Rows.Count;
            for (s = 0; s < GridViewParticialCpt.Rows.Count; s++)
            {

                if ((((CheckBox)GridViewParticialCpt.Rows[s].FindControl("ckstackPar")).Checked == true || ((CheckBox)GridViewParticialCpt.Rows[s].FindControl("chkParCptPar")).Checked == true) && Convert.ToDecimal(Convert.ToDecimal(((TextBox)GridViewParticialCpt.Rows[s].FindControl("txtCapacity")).Text.ToString())) > 0)
                {
                    if (Convert.ToDecimal(((TextBox)GridViewParticialCpt.Rows[s].FindControl("txtCapacity")).Text.ToString()) <= Convert.ToDecimal(GridViewParticialCpt.Rows[s].Cells[6].Text.ToString()))
                    {
                        //if (((DropDownList)GridViewParticialCpt.Rows[s].FindControl("ddlScheme")).SelectedItem.Text.ToString() != "Select")
                        //{
                        Decimal cpt = Convert.ToDecimal(((TextBox)GridViewParticialCpt.Rows[s].FindControl("txtCapacity")).Text.ToString());
                        totalcpt = cpt + totalcpt;
                        txtTotlcpt.Value = Convert.ToString(totalcpt);
                        // TotalAmt = (totalcpt * Convert.ToDecimal(1));
                        TotalAmt = Math.Round(totalcpt * Convert.ToDecimal(1));
                        txtOfferAmt.Value = TotalAmt.ToString();
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Offer Scheme ')", true);
                        //    for (int j = s + 1; j < gvGodown.Rows.Count; j++)
                        //    {
                        //        ((TextBox)GridViewParticialCpt.Rows[j].FindControl("txtCapacity")).Text = "0";
                        //        ((TextBox)GridViewParticialCpt.Rows[j].FindControl("txtCapacity")).Enabled = false;
                        //        ((CheckBox)GridViewParticialCpt.Rows[j].FindControl("chkParCptPar")).Checked = false;
                        //        ((CheckBox)GridViewParticialCpt.Rows[j].FindControl("ckstackPar")).Checked = false;
                        //        if (((DropDownList)GridViewParticialCpt.Rows[j].FindControl("ddlScheme")).Enabled == true)
                        //        {
                        //            ((DropDownList)GridViewParticialCpt.Rows[j].FindControl("ddlScheme")).SelectedIndex = -1;
                        //        }
                        //    }
                        //    txtTotlcpt.Value = "0.00";
                        //    txtOfferAmt.Value = "0.00";
                        //}
                    }
                    else
                    {
                        txtTotlcpt.Value = "0.00";
                        txtOfferAmt.Value = "0.00";
                        ((TextBox)GridViewParticialCpt.Rows[s].FindControl("txtCapacity")).Text = "0";
                        ((CheckBox)GridViewParticialCpt.Rows[s].FindControl("chkParCptPar")).Checked = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Entered Capacity Greater than the Total Capacity ')", true);
                    }
                }
                else
                {
                    ((TextBox)GridViewParticialCpt.Rows[s].FindControl("txtCapacity")).Text = "0";
                    ((CheckBox)GridViewParticialCpt.Rows[s].FindControl("chkParCptPar")).Checked = false;
                    if (((DropDownList)GridViewParticialCpt.Rows[s].FindControl("ddlScheme")).Enabled == true)
                    {
                        ((DropDownList)GridViewParticialCpt.Rows[s].FindControl("ddlScheme")).SelectedIndex = -1;
                        //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You can Not Offer Zero Capacity Godown ')", true);
                    }
                }
            }
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        string ApptypeM = Session["AppType"].ToString();
        //if (lblDist.Text == "2338" || lblDist.Text == "2314" || lblDist.Text == "2342")
        //{
        string TStatus = Tcheckdatetimes();
        if (TStatus == "Y")
        {
            int JK = 0;
            JK = CheckOffer();
            if (JK == 0)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqltran = con.BeginTransaction();
                if (CheckBox1.Checked == true)
                {
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string ofrtxt = ChkOfferID();
                    string Apptype = Session["AppType"].ToString();
                    if (Convert.ToDecimal(txtTotlcpt.Value) < 500 && Apptype == "Co-Operative Society")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can Not Offered Small Capacity ...'); </script> ");
                    }
                    else if (Convert.ToDecimal(txtTotlcpt.Value) < 500 && Apptype == "Government")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can Not Offered Small Capacity ...'); </script> ");
                    }
                    else if (Convert.ToDecimal(txtTotlcpt.Value) < 500 && Apptype == "Individual")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can Not Offered Small Capacity ...'); </script> ");
                    }
                    else if (Convert.ToDecimal(txtTotlcpt.Value) < 500 && Apptype == "Partnership Firm")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can Not Offered Small Capacity ...'); </script> ");
                    }
                    else if (Convert.ToDecimal(txtTotlcpt.Value) < 500 && Apptype == "Private Ltd.")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can Not Offered Small Capacity ...'); </script> ");
                    }
                    else if (Convert.ToDecimal(txtTotlcpt.Value) < 500 && Apptype == "Public Ltd.")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can Not Offered Small Capacity ...'); </script> ");
                    }
                    else if (ddlProcCenter.SelectedItem.Text == "--Select--")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('खरीदी केंद्र स्थापित करने हेतु सहमति / असहमति का विकल्प चुने '); </script> ");
                    }
                    else if (ddlfumigation.SelectedItem.Text == "--Select--")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेयरहाउस मे फ्यूमीगेशन कार्य करने का विकल्प चुने.... '); </script> ");
                    }
                    else if (ddlPrioritySelection.SelectedItem.Text == "--Select--")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('प्राथमिकता क्रम की पात्रता का विकल्प चुने.... '); </script> ");
                    }
                    else if (ddlPMS.SelectedItem.Text == "--Select--")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम रख-रखाव का विकल्प चुने.... '); </script> ");
                    }
                    else if (ddlCategory.SelectedItem.Text == "--Select--")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('संयुक्त भागीदारी योजना की श्रेणी(A अथवा B) का चयन करे.... '); </script> ");
                    }
                    else if (Convert.ToDecimal(txtOfferAmt.Value) > 0 && Convert.ToDecimal(txtTotlcpt.Value) > 0)
                    {
                        try
                        {
                            string qry = "INSERT INTO [tbl_Warehouse_Capacity_Offer_Rabi_2026_27] ([Offer_Id],[Registration_Id],[DistrictId],[BranchId],[Offer_Date],[Offer_Capacity],[FeesStatus],[FeesTransID],[OfferAmt],[CreatedBy],[CreatedDate],[IsActive],[AId],[Phase],[OfferSeason],[ProcCenterPer],[FumigationWork],Selected_Priority,Maintain_By,Category) VALUES ('" + ofrtxt + "','" + lblRegNo.Text + "','" + lblDist.Text + "','" + lblbranch.Text + "',GETDATE(),'" + txtTotlcpt.Value + "','N',null,'" + txtOfferAmt.Value + "','" + ip + "',GETDATE(),'Y',null,'" + R_Phase + "','" + Reg_season + "','" + ddlProcCenter.SelectedValue.ToString().Trim() + "','" + ddlfumigation.SelectedValue.ToString().Trim() + "','" + ddlPrioritySelection.SelectedValue + "','" + ddlPMS.SelectedValue + "','" + ddlCategory.SelectedValue + "')";

                            SqlCommand cmd = new SqlCommand(qry, con, sqltran);
                            int CT = 0;
                            CT = cmd.ExecuteNonQuery();
                            if (CT > 0)
                            {
                                int s = 0;
                                int a = 0;
                                string cpttype;
                                string OfrCategory = "";
                                for (s = 0; s < gvGodown.Rows.Count; s++)
                                {
                                    if (((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked == true || ((CheckBox)gvGodown.Rows[s].FindControl("chkParCpt")).Checked == true)
                                    {
                                        if (((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked == true)
                                        {
                                            cpttype = "F";
                                        }
                                        else
                                        {
                                            cpttype = "P";
                                        }
                                        if (((DropDownList)gvGodown.Rows[s].FindControl("ddlScheme")).SelectedValue.ToString().Trim() == "80" || ((DropDownList)gvGodown.Rows[s].FindControl("ddlScheme")).SelectedValue.ToString().Trim() == "00")
                                        {
                                            OfrCategory = "B";
                                        }
                                        else if (((DropDownList)gvGodown.Rows[s].FindControl("ddlScheme")).SelectedValue.ToString().Trim() == "85" || ((DropDownList)gvGodown.Rows[s].FindControl("ddlScheme")).SelectedValue.ToString().Trim() == "0")
                                        {
                                            OfrCategory = "A";
                                        }

                                        else if (((DropDownList)gvGodown.Rows[s].FindControl("ddlScheme")).SelectedValue.ToString().Trim() == "55")
                                        {
                                            OfrCategory = "BB";
                                        }

                                        else if (((DropDownList)gvGodown.Rows[s].FindControl("ddlScheme")).SelectedValue.ToString().Trim() == "60")
                                        {
                                            OfrCategory = "AB";
                                        }

                                        string offerGodownID = ofrtxt + Convert.ToString(Convert.ToInt32(1 + a));
                                        string qry1 = "INSERT INTO [tbl_Warehouse_Godown_Offer_Rabi_2026_27] ([Offer_Id],[Godown_Offer_Id],[Registration_Id],[Godown_ID],[Godown_No],[DistrictId],[BranchId],[Offer_Date],[G_OfferCapacity],[G_Scheme],[Capacity_Type],[CreatedBy],[CreatedDate],[IsActive],[Phase],[OfferSeason],[Offer_Category],Selected_Priority,Maintain_By,Category) VALUES ('" + ofrtxt + "','" + offerGodownID + "','" + lblRegNo.Text + "','" + gvGodown.Rows[s].Cells[0].Text.ToString() + "','" + gvGodown.Rows[s].Cells[1].Text.ToString() + "','" + lblDist.Text + "','" + lblbranch.Text + "',GETDATE(),'" + Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Text.ToString()) + "','" + ((DropDownList)gvGodown.Rows[s].FindControl("ddlScheme")).SelectedValue.ToString().Trim() + "','" + cpttype + "','" + ip + "',GETDATE(),'Y','" + R_Phase + "','" + Reg_season + "','" + OfrCategory + "','" + ddlPrioritySelection.SelectedValue + "','" + ddlPMS.SelectedValue + "','" + ddlCategory.SelectedValue + "')";

                                        SqlCommand cmd1 = new SqlCommand(qry1, con, sqltran);
                                        int CT1 = 0;
                                        CT1 = cmd1.ExecuteNonQuery();
                                        if (CT1 > 0)
                                        {
                                            a = a + 1;
                                        }
                                    }
                                }
                                //   ModalPopupExtender2.Show();
                                if (a > 0)
                                {
                                    sqltran.Commit();
                                    btnsubmit.Enabled = false;
                                    Session["Oft_Type"] = "FIRST";
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Saccessfully Offered  Please Take a print ')", true);
                                    btnPrint.Visible = true;
                                    btnpayment.Visible = true;
                                    // Response.Redirect("PrintOffer.aspx");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            sqltran.Rollback();
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Something Error...'); </script> ");
                        }
                        finally
                        {
                            sqltran.Dispose();
                            con.Close();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can Not Offered 0.00 Capacity ...'); </script> ");
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You are not agree to condions...'); </script> ");
                }
            }
            else
            {
                if (JK == 1)
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    sqltran = con.BeginTransaction();
                    if (CheckBox1.Checked == true)
                    {
                        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        string ofrtxt = ChkOfferID();
                        string Apptype = Session["AppType"].ToString();
                        if (Convert.ToDecimal(txtTotlcpt.Value) < 500 && Apptype == "Co-Operative Society")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can Not Offered Small Capacity ...'); </script> ");
                        }
                        else if (Convert.ToDecimal(txtTotlcpt.Value) < 500 && Apptype == "Government")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can Not Offered Small Capacity ...'); </script> ");
                        }
                        else if (Convert.ToDecimal(txtTotlcpt.Value) < 200 && Apptype == "Individual")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can Not Offered Small Capacity ...'); </script> ");
                        }
                        else if (Convert.ToDecimal(txtTotlcpt.Value) < 200 && Apptype == "Partnership Firm")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can Not Offered Small Capacity ...'); </script> ");
                        }
                        else if (Convert.ToDecimal(txtTotlcpt.Value) < 200 && Apptype == "Private Ltd.")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can Not Offered Small Capacity ...'); </script> ");
                        }
                        else if (Convert.ToDecimal(txtTotlcpt.Value) < 200 && Apptype == "Public Ltd.")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can Not Offered Small Capacity ...'); </script> ");
                        }
                        else if (ddlProcCenter.SelectedItem.Text == "--Select--")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('खरीदी केंद्र स्थापित करने हेतु सहमति/असहमति का विकल्प चुने '); </script> ");
                        }
                        else if (ddlfumigation.SelectedItem.Text == "--Select--")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेयरहाउस मे फ्यूमीगेशन कार्य करने का विकल्प चुने.... '); </script> ");
                        }
                        else if (ddlCategory.SelectedItem.Text == "--Select--")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('संयुक्त भागीदारी योजना की श्रेणी(A अथवा B) का चयन करे.... '); </script> ");
                        }
                        else if (Convert.ToDecimal(txtOfferAmt.Value) > 0 && Convert.ToDecimal(txtTotlcpt.Value) > 0)
                        {
                            try
                            {

                                string qry = "INSERT INTO [tbl_Warehouse_Capacity_Offer_Rabi_2026_27] ([Offer_Id],[Registration_Id],[DistrictId],[BranchId],[Offer_Date],[Offer_Capacity],[FeesStatus],[FeesTransID],[OfferAmt],[CreatedBy],[CreatedDate],[IsActive],[AId],[Phase],[OfferSeason],[ProcCenterPer],[FumigationWork],Selected_Priority,Maintain_By,Category) VALUES ('" + ofrtxt + "','" + lblRegNo.Text + "','" + lblDist.Text + "','" + lblbranch.Text + "',GETDATE(),'" + txtTotlcpt.Value + "','N',null,'" + txtOfferAmt.Value + "','" + ip + "',GETDATE(),'N',null,'" + R_Phase + "','" + Reg_season + "','" + ddlProcCenter.SelectedValue.ToString().Trim() + "','" + ddlfumigation.SelectedValue.ToString().Trim() + "','" + ddlPrioritySelection.SelectedValue + "','" + ddlPMS.SelectedValue + "','" + ddlCategory.SelectedValue + "')";

                                SqlCommand cmd = new SqlCommand(qry, con, sqltran);
                                int CT = 0;
                                CT = cmd.ExecuteNonQuery();
                                if (CT > 0)
                                {
                                    int s = 0;
                                    int a = 0;
                                    string cpttype;
                                    string OfrCategory = "";
                                    for (s = 0; s < GridViewParticialCpt.Rows.Count; s++)
                                    {
                                        if (((CheckBox)GridViewParticialCpt.Rows[s].FindControl("ckstackPar")).Checked == true || ((CheckBox)GridViewParticialCpt.Rows[s].FindControl("chkParCptPar")).Checked == true)
                                        {
                                            if (((CheckBox)GridViewParticialCpt.Rows[s].FindControl("ckstackPar")).Checked == true)
                                            {
                                                cpttype = "F";
                                            }
                                            else
                                            {
                                                cpttype = "P";
                                            }
                                            if (((DropDownList)GridViewParticialCpt.Rows[s].FindControl("ddlScheme")).SelectedValue.ToString().Trim() == "80" || ((DropDownList)GridViewParticialCpt.Rows[s].FindControl("ddlScheme")).SelectedValue.ToString().Trim() == "00")
                                            {
                                                OfrCategory = "B";
                                            }
                                            else if (((DropDownList)GridViewParticialCpt.Rows[s].FindControl("ddlScheme")).SelectedValue.ToString().Trim() == "85" || ((DropDownList)GridViewParticialCpt.Rows[s].FindControl("ddlScheme")).SelectedValue.ToString().Trim() == "0")
                                            {
                                                OfrCategory = "A";
                                            }

                                            else if (((DropDownList)GridViewParticialCpt.Rows[s].FindControl("ddlScheme")).SelectedValue.ToString().Trim() == "55")
                                            {
                                                OfrCategory = "BB";
                                            }

                                            else if (((DropDownList)GridViewParticialCpt.Rows[s].FindControl("ddlScheme")).SelectedValue.ToString().Trim() == "60")
                                            {
                                                OfrCategory = "AB";
                                            }

                                            string offerGodownID = ofrtxt + Convert.ToString(Convert.ToInt32(1 + a));

                                            string qry1 = "INSERT INTO [tbl_Warehouse_Godown_Offer_Rabi_2026_27] ([Offer_Id],[Godown_Offer_Id],[Registration_Id],[Godown_ID],[Godown_No],[DistrictId],[BranchId],[Offer_Date],[G_OfferCapacity],[G_Scheme],[Capacity_Type],[CreatedBy],[CreatedDate],[IsActive],[Phase],[OfferSeason],[Offer_Category],Selected_Priority,Maintain_By,Category) VALUES ('" + ofrtxt + "','" + offerGodownID + "','" + lblRegNo.Text + "','" + GridViewParticialCpt.Rows[s].Cells[0].Text.ToString() + "','" + GridViewParticialCpt.Rows[s].Cells[1].Text.ToString() + "','" + lblDist.Text + "','" + lblbranch.Text + "',GETDATE(),'" + Convert.ToDecimal(((TextBox)GridViewParticialCpt.Rows[s].FindControl("txtCapacity")).Text.ToString()) + "','" + ((DropDownList)GridViewParticialCpt.Rows[s].FindControl("ddlScheme")).SelectedValue.ToString().Trim() + "','" + cpttype + "','" + ip + "',GETDATE(),'Y','" + R_Phase + "','" + Reg_season + "','" + OfrCategory + "','" + ddlPrioritySelection.SelectedValue + "','" + ddlPMS.SelectedValue + "','" + ddlCategory.SelectedValue + "')";

                                            SqlCommand cmd1 = new SqlCommand(qry1, con, sqltran);
                                            int CT1 = 0;
                                            CT1 = cmd1.ExecuteNonQuery();
                                            if (CT1 > 0)
                                            {
                                                a = a + 1;
                                            }
                                        }
                                    }
                                    // ModalPopupExtender2.Show();
                                    if (a > 0)
                                    {
                                        sqltran.Commit();
                                        Session["ofrid"] = ofrtxt.ToString();
                                        btnsubmit.Enabled = false;
                                        btnPrint.Visible = true;
                                        btnpayment.Visible = true;
                                        Session["Oft_Type"] = "SECOND";
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Saccessfully Offered Please Take a print ')", true);

                                        // Response.Redirect("PrintSecondOffer.aspx");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                sqltran.Rollback();
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Something Error...'); </script> ");
                            }
                            finally
                            {
                                sqltran.Dispose();
                                con.Close();
                            }
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can Not Offered 0.00 Capacity ...'); </script> ");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You are not agree to condions...'); </script> ");
                    }
                }
            }
        }
        else if (TStatus == "NS")
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can't Login Before 14/06/2016 11 AM ...!'); </script> ");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You can not Offer before 11/29/2019 11:59:00 AM'); </script> ");

        }
        else if (TStatus == "NE")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Offer for Warehouse under Joint Venture Scheme has been closed..!'); </script> ");
        }
        //}
        //else
        //{
        //    pnlofferpopup.Visible = true;
        //    ModalPopupExtender1.Show();
        //}
    }
    public void inertGodown()
    {

    }
    public string ChkOfferID()
    {
        string MaxOfrID = "";
        //string QueryMax = "select MAX(Offer_Id) as Offer_Id from tbl_Warehouse_Capacity_Offer_2020 where Registration_Id='" + Session["Reg_No"].ToString() + "'";
        string QueryMax = "select MAX(Offer_Id) as Offer_Id from tbl_Warehouse_Capacity_Offer_Rabi_2026_27 where Registration_Id='" + Session["Reg_No"].ToString() + "'";

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
            //MaxOfrID = lblRegNo.Text + "22" + Convert.ToString(Convert.ToInt32(part2) + 1);
            MaxOfrID = lblRegNo.Text + "232" + Convert.ToString(Convert.ToInt32(part2) + 1);
        }
        else
        {
            //MaxOfrID = lblRegNo.Text + "191";
            //MaxOfrID = lblRegNo.Text + "201";
            // MaxOfrID = lblRegNo.Text + "211";
            //MaxOfrID = lblRegNo.Text + "221";
            MaxOfrID = lblRegNo.Text + "2321";
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

        //DateTime _effective_date = Convert.ToDateTime("03/08/2021 11:59:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("03/08/2023 11:59:00 PM");

        //savan
        //DateTime _effective_date = Convert.ToDateTime("08/02/2022 10:59:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("31/03/2023 10:59:00 PM");

        //Server
        //DateTime _effective_date = Convert.ToDateTime("03/08/2022 10:59:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("02/14/2023 11:59:00 PM");

        //21022023
        //DateTime _effective_date = Convert.ToDateTime("02/27/2023 11:59:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("03/10/2023 11:59:00 PM");

        // start 19/10/2018
        //DateTime _effective_date = Convert.ToDateTime("10/14/2019 11:59:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("10/30/2019 11:59:00 PM");
        //end 31/1/2019

        ////Old

        //   DateTime _effective_date = Convert.ToDateTime("2018-11-19 15:07:29.160");
        //   DateTime _Closing_date = Convert.ToDateTime("2019-11-30 15:07:29.160");

        ////Old For Rabi 2024-25
        //DateTime _effective_date = Convert.ToDateTime("02/27/2023 01:59:00 PM");
        //DateTime _Closing_date = Convert.ToDateTime("05/05/2024 11:59:00 PM");

        //DateTime _effective_date = Convert.ToDateTime("03/04/2025 01:59:00 PM");
        //DateTime _Closing_date = Convert.ToDateTime("01/31/2026 06:30:00 PM");

        DateTime _effective_date = Convert.ToDateTime("03/03/2026 01:59:00 PM");
        DateTime _Closing_date = Convert.ToDateTime("08/01/2026 06:30:00 PM");

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
            //string Roid = "../JointVentureScheme/PrintOffer.aspx?src=RO&vu=" + Session["Reg_No"].ToString();
            string Roid = "../JointVentureScheme/Print_RAB_2026_27_Offer.aspx?src=RO&vu=" + Session["Reg_No"].ToString();
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
            string Roid = "../JointVentureScheme/Print_RAB_2026_27_Offer.aspx?src=RO&vu=" + Session["Reg_No"].ToString();
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
        string strsql = "select top 1 case when LicType='63' then '80' when LicType='68' then '85' else '0' end as LicType,RoadType , GateType , W_BoundaryType , WeighBridge from tbl_WarehouseGodown_Reg as GREG left join  (select Registration_ID,case when RoadType='1' then 'BT' when RoadType='2' then 'CC' when RoadType='3' then 'WBM' end RoadType , GateType , W_BoundaryType , WeighBridge from tbl_WarehouseAdditionalinfo where Registration_Id='" + Session["Reg_No"].ToString() + "') as RAD on GREG.Registration_Id=RAD.Registration_ID where GREG.Registration_Id='" + Session["Reg_No"].ToString() + "' and Godown_No='" + GDWN_No + "' order by CreatedDate desc";

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


            //if (SWeighBridge == "True" && SW_BoundaryType != "3" && SGateType == "1" && (SRoadType == "WBM" || SRoadType == "BT" || SRoadType == "CC"))
            //{
            //    ch = "85";
            //}
            //else
            //{
            //    ch = "80";
            //}

            // Updatation for JVS Scheme 2022-23 Date 05032022
            if (SWeighBridge == "True" && SW_BoundaryType != "3" && SGateType == "1" && (SRoadType == "WBM" || SRoadType == "BT" || SRoadType == "CC") && lbljvschois.Text == "अ")
            {
                ch = "85";
            }
            else if (SWeighBridge == "True" && SW_BoundaryType != "3" && SGateType == "1" && (SRoadType == "WBM" || SRoadType == "BT" || SRoadType == "CC") && lbljvschois.Text == "ब")
            {
                ch = "60";
            }

            else if (lbljvschois.Text == "अ")
            {
                ch = "80";
            }

            else if (lbljvschois.Text == "ब")
            {
                ch = "55";
            }

            // ch = ds.Tables[0].Rows[0]["LicType"].ToString().Trim();
        }
        else
        {
            ch = "0";
        }
        return ch;
    }
    public void insertschemen()
    {
        int s = 0;
        int Count_Rows;
        Count_Rows = GridViewParticialCpt.Rows.Count;
        for (s = 0; s < GridViewParticialCpt.Rows.Count; s++)
        {
            string schm = CheckOfferSchem(GridViewParticialCpt.Rows[s].Cells[1].Text.ToString().Trim());
            if (schm == "80" || schm == "85")
            {
                ((DropDownList)GridViewParticialCpt.Rows[s].FindControl("ddlScheme")).SelectedValue = schm;
                ((DropDownList)GridViewParticialCpt.Rows[s].FindControl("ddlScheme")).Enabled = false;
            }

            else if (schm == "60" || schm == "55")
            {
                ((DropDownList)GridViewParticialCpt.Rows[s].FindControl("ddlScheme")).SelectedValue = schm;
                ((DropDownList)GridViewParticialCpt.Rows[s].FindControl("ddlScheme")).Enabled = false;
            }
        }
    }
    public void insertschemenNew()
    {
        int s = 0;
        int Count_Rows;
        Count_Rows = gvGodown.Rows.Count;
        for (s = 0; s < gvGodown.Rows.Count; s++)
        {

            string schm = CheckOfferSchem(gvGodown.Rows[s].Cells[1].Text.ToString().Trim());
            if (schm == "80" || schm == "85")
            {
                ((DropDownList)gvGodown.Rows[s].FindControl("ddlScheme")).SelectedValue = schm;
                ((DropDownList)gvGodown.Rows[s].FindControl("ddlScheme")).Enabled = false;
            }

            else if (schm == "60" || schm == "55")
            {
                ((DropDownList)gvGodown.Rows[s].FindControl("ddlScheme")).SelectedValue = schm;
                ((DropDownList)gvGodown.Rows[s].FindControl("ddlScheme")).Enabled = false;
            }
            else
            {
                ((DropDownList)gvGodown.Rows[s].FindControl("ddlScheme")).Enabled = false;
            }
        }
    }
    protected void btnpayment_Click(object sender, EventArgs e)
    {
        //string strsql = "";
        //if (Session["Oft_Type"].ToString() == "FIRST")
        //{
        //    strsql = "select reg.Registration_Id,Auth_Person,OreReg.MobileNo,OreReg.EmailID,Ofr.OfferAmt,Ofr.Offer_Capacity from tbl_WarehouseRegistration as Reg Inner join tbl_Warehouse_PreReg as OreReg on Reg.Registration_Id = OreReg.Reg_No left join (select Registration_Id,Offer_Capacity,OfferAmt from tbl_Warehouse_Capacity_Offer_2019 where Registration_Id='" + Session["Reg_No"].ToString() + "') as Ofr on Ofr.Registration_Id=Reg.Registration_Id where Reg.Registration_Id='" + Session["Reg_No"].ToString() + "' ";
        //}
        //else if (Session["Oft_Type"].ToString() == "SECOND")
        //{
        //    strsql = "select reg.Registration_Id,Auth_Person,OreReg.MobileNo,OreReg.EmailID,Ofr.OfferAmt,Ofr.Offer_Capacity from tbl_WarehouseRegistration as Reg Inner join tbl_Warehouse_PreReg as OreReg on Reg.Registration_Id = OreReg.Reg_No left join (select Registration_Id,Offer_Capacity,OfferAmt from tbl_Warehouse_Capacity_Offer_2019 where Registration_Id='" + Session["Reg_No"].ToString() + "' and Offer_Id='" + Session["ofrid"].ToString().Trim()  + "') as Ofr on Ofr.Registration_Id=Reg.Registration_Id where Reg.Registration_Id='" + Session["Reg_No"].ToString() + "' ";
        //}
        //SqlCommand cmd = new SqlCommand(strsql, con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //if (dt.Rows.Count > 0)
        //{
        //    lblRegID.Text = dt.Rows[0]["Registration_Id"].ToString().Trim();
        //    lblOwn.Text = dt.Rows[0]["Auth_Person"].ToString().Trim();
        //    lblcontact.Text = dt.Rows[0]["MobileNo"].ToString().Trim();
        //    lblemailid.Text = dt.Rows[0]["EmailID"].ToString().Trim();
        //    lblRegCapacity.Text = dt.Rows[0]["Offer_Capacity"].ToString().Trim();
        //    lblRegFee.Text = dt.Rows[0]["OfferAmt"].ToString().Trim();
        //}
        // ModalPopupExtender2.Show();
        //Response.Redirect("https://www.onlinesbi.sbi/sbicollect/icollecthome.htm");
        Response.Redirect("https://www.onlinesbi.sbi/sbicollect/icollecthome.htm?corpID=329338");

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //Response.Redirect("https://www.onlinesbi.sbi/sbicollect/icollecthome.htm");
        Response.Redirect("https://www.onlinesbi.sbi/sbicollect/icollecthome.htm?corpID=329338");
    }

    protected void btnpopup_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangeChoiceFilling.aspx");
    }



    protected void Button4_Click(object sender, EventArgs e)
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

    protected void ddlPrioritySelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Get_Selection();
    }
    //protected void ddlPMS_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Get_Selection();
    //}
    //public void Get_Selection()
    //{
    //    string Choice = "";
    //    string Maintenance = "";
    //    if (ddlPMS.SelectedValue == "1")
    //    {
    //        //Maintenance = " श्रेणी की पात्रता के साथ स्वयं द्वारा रख-रखाव";
    //        Maintenance = " SPMS";
    //    }
    //    else if (ddlPMS.SelectedValue == "0")
    //    {
    //        //Maintenance = " श्रेणी की पात्रता के साथ PMS एजेंसी द्वारा रख-रखाव";
    //        Maintenance = " PMS";
    //    }
    //    Choice = ddlPrioritySelection.SelectedItem.Text + Maintenance;
    //    lbljvschois.Text = Choice;
    //}

    public void CheckOtherStockDetailsInWarehouse()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand("[dbo].[Get_Other_Stock_Position_For_Offer_2024_25]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RegistrationID", Session["Reg_No"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["WeightBalance"].ToString() != "0" && dt.Rows[0]["Category"].ToString() != "0")
            {
                string Choice = "";
                string Maintenance = "";
                //Get_Selection();
                ddlPrioritySelection.ClearSelection();
                ddlCategory.ClearSelection();
                ddlPMS.ClearSelection();
                //Set Selected Value
                ddlPrioritySelection.SelectedValue = dt.Rows[0]["Selected_Priority"].ToString();
                ddlCategory.SelectedValue = dt.Rows[0]["Category"].ToString();
                //ddlPMS.SelectedValue = dt.Rows[0]["Maintain_By"].ToString();
                ddlPMS.SelectedValue = "1";
                //

                if (dt.Rows[0]["Maintain_By"].ToString() == "1")
                {
                    //Maintenance = " श्रेणी की पात्रता के साथ स्वयं द्वारा रख-रखाव";
                    Maintenance = " SPMS";
                }
                else if (dt.Rows[0]["Maintain_By"].ToString() == "2")
                {
                    //Maintenance = " श्रेणी की पात्रता के साथ PMS एजेंसी द्वारा रख-रखाव";
                    Maintenance = " PMS";
                }
                Choice = dt.Rows[0]["Selected_Priority"].ToString() + Maintenance;
                lbljvschois.Text = Choice;


                //ddlPrioritySelection.Items.FindByValue(ddlPrioritySelection.SelectedValue).Selected = true;
                //ddlCategory.Items.FindByValue(ddlCategory.SelectedValue).Selected = true;
                //ddlPMS.Items.FindByValue(ddlPMS.SelectedValue).Selected = true;
                //ddlPrioritySelection.Enabled = false;
                //ddlCategory.Enabled = false;
                ddlPMS.Enabled = true;
            }
            else
            {
                //ddlPrioritySelection.SelectedValue = "--Select--";
                //ddlCategory.SelectedValue = "--Select--";
                //ddlPMS.SelectedValue = "--Select--";
                ddlPrioritySelection.Enabled = true;
                ddlCategory.Enabled = true;
                ddlPMS.Enabled = true;
            }

        }
        else
        {

        }


    }
}