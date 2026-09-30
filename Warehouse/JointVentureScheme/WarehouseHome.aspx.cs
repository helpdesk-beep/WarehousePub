using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class JointVentureScheme_WarehouseHome : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd = null;
    string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["email"] != null) && (Session["mobile"] != null))
        {
            //  Tr2.Visible = false;
           // string TStatus = Tcheckdatetimes();
          //  if (TStatus == "Y")
           // {
                //TrReg.Visible = true;
                //TrOffer.Visible = true;
                try
                {
                    if (Session["Reg_No"] == null || Session["Reg_No"] == "")
                    {
                        //string d = Session["Reg_No"].ToString();
                        //  TrReg.Visible = true;
                    }
                    else
                    {
                        //string d = Session["Reg_No"].ToString();
                        //  TrReg.Visible = false;
                    }
                    lbluser.Text = Session["fname"].ToString() + " " + Session["mname"].ToString() + " " + Session["lname"].ToString();
                }
                catch (Exception ex)
                {
                    Response.Redirect("UserReg.aspx");
                }
            //}
            //else if (TStatus == "NS")
            //{
            //    //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can't Login Before 14/06/2016 11 AM ...!'); </script> ");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You can not Register/Offer before 29/11/2019 11:00:00 AM'); </script> ");
            //    //TrReg.Visible = false;
            //    //TrOffer.Visible = false;
            //}
            //else if (TStatus == "NE")
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Offer for Warehouse under Joint Venture Scheme has been closed..!'); </script> ");
            //    //TrReg.Visible = false;
            //    //TrOffer.Visible = false;
            //}
        }
        else
        {
            Response.Redirect("UserReg.aspx");
        }
    }

    //public void CheckAllreadyExist()
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //    SqlConnection con = new SqlConnection(constr);
    //    SqlCommand cmd = new SqlCommand("[dbo].[Get_Private_Warehouse_Related_Information_Check]", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Registration_ID", Session["UserId"].ToString());
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        // txt_inspdate.Text = dt.Rows[0]["Inspection_Date"].ToString();
    //        if (dt.Rows[0]["Employee_ID"].ToString() == "YES")
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
    //        }
    //        else if (dt.Rows[0]["Employee_ID"].ToString() == "NO")
    //        {
    //            fillgrid();
    //        }
    //    }

    //}
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("UserReg.aspx");
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://www.onlinesbi.sbi/sbicollect/icollecthome.htm?corpID=329338");
    }        

    public string Tcheckdatetimes()
    {
        DateTime ServerDate = new DateTime();

        //Test
        //DateTime _effective_date = Convert.ToDateTime("02/18/2018 00:05:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("02/26/2018 05:00:00 PM");
        //Actual
        //DateTime _effective_date = Convert.ToDateTime("07/28/2018 11:00:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("09/15/2018 11:59:00 PM");

        //DateTime _effective_date = Convert.ToDateTime("03/08/2022 10:59:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("03/31/2023 10:59:00 PM");

        //Savan
        //DateTime _effective_date = Convert.ToDateTime("08/02/2022 10:59:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("31/03/2023 10:59:00 PM");

        //DateTime _effective_date = Convert.ToDateTime("02/08/2022 10:59:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("02/14/2023 10:59:00 PM");

        
        DateTime _effective_date = Convert.ToDateTime("02/27/2023 01:59:00 PM");
        DateTime _Closing_date = Convert.ToDateTime("03/30/2023 11:59:00 PM");

        //  DateTime _Closing_date = Convert.ToDateTime("01/07/2022 05:10:00 PM");

        ////Old

        //  DateTime _effective_date = Convert.ToDateTime("2018-12-26 11:00:57.290");
        //   DateTime _Closing_date = Convert.ToDateTime("2019-12-26 11:00:57.290");

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
    protected void btnNewReg_Click(object sender, EventArgs e)
    {
        int a = checkqry("select * from tbl_Warehouse_PreReg where Reg_No is null and EmailID='" + Session["email"].ToString().Trim() + "'");
        if (a == 1)
        {
            ModalPopupExtender2.Show();
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Warehouse Registration Already Fill in Previous ......'); </script> ");
        }
    }
    protected void btnPaymentReg_Click(object sender, EventArgs e)
    {
        int a = checkqry("select Wreg.Registration_Id from tbl_WarehouseRegistration as Wreg inner join tbl_Warehouse_PreReg as PReg on Wreg.Registration_Id=PReg.Reg_No where PReg.EmailID='" + Session["email"].ToString().Trim() + "'");
        if (a == 1)
        {
            Response.Redirect("WarehousePayment.aspx");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registration Not Complete......'); </script> ");
        }

    }
    protected void btnupdatereg_Click(object sender, EventArgs e)
    {
        int a = checkqry("select * from tbl_Warehouse_PreReg where Reg_No is not null and EmailID='" + Session["email"].ToString().Trim() + "'");
        if (a == 1)
        {
            Response.Redirect("UpdateRegistration.aspx");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Warehouse Registration Not Complete First Complete Registration then Update if Required   ......'); </script> ");
        }
    }
    protected void btnoffer_Click(object sender, EventArgs e)
    {

        // ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('दिनाँक 31-12-2021 तक ऑफर बंद किए गए हैं|'); </script> ");
        //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('आफ़र करने कि window बंद हो चुकि हैं ...'); </script> ");

        // Comment before JVS 2023-24 offer
        //int D = checkqry("select Reg_ID from Tbl_JVS_Choise_Filling where Reg_ID='" + Session["Reg_No"].ToString() + "'");
        //if (D == 1)
        //{

        int a = checkqry("select Wreg.Registration_Id from tbl_WarehouseRegistration as Wreg inner join tbl_Warehouse_PreReg as PReg on Wreg.Registration_Id=PReg.Reg_No where PReg.EmailID='" + Session["email"].ToString().Trim() + "'");
        if (a == 1)
        {
            string chkup = checkupdate();
            if (chkup == "Y")
            // if (chkup == "N")
            {
                //int b = checkqry("select Wreg.Registration_Id from tbl_WarehouseRegistration as Wreg inner join tbl_Warehouse_PreReg as PReg on Wreg.Registration_Id=PReg.Reg_No where PReg.EmailID='" + Session["email"].ToString().Trim() + "' and (SELECT DATEDIFF(DAY, Wreg.Registration_Date, Getdate()) AS DateDiff)<=1096");
                //int b = checkqry("select Wreg.Registration_Id from tbl_WarehouseRegistration as Wreg inner join tbl_Warehouse_PreReg as PReg on Wreg.Registration_Id=PReg.Reg_No where PReg.EmailID='" + Session["email"].ToString().Trim() + "' and (SELECT DATEDIFF(DAY, Wreg.Registration_Date, Getdate()) AS DateDiff)<=1099 and Wreg.Registration_Id in (select A.Registration_Id from (select REG.Registration_Id,RegCapacity,RegAmt*2 as RegAmt ,isnull(REGPMT.DepositedRegAmount,0) DepositedRegAmt,case when RegAmt*2<=isnull(REGPMT.DepositedRegAmount,0) then 'Confirmed' else 'Pending' end RegPaymentStatus,isnull(Offer_Capacity,0)Offer_Capacity,isnull(OfferAmt,0)OfferAmt,isnull(OFRPMT.DepositedOfferAmount,0) ofrdepositedamt,case when OfferAmt<=isnull(OFRPMT.DepositedOfferAmount,0) then 'Confirmed' else 'Pending' end OfferPaymentStatus from tbl_WarehouseRegistration as REG inner join tbl_Warehouse_PreReg as PREG on REG.Registration_Id=PREG.Reg_No left join (select Registration_Id,SUM(Offer_Capacity) as Offer_Capacity,SUM(OfferAmt)as OfferAmt from  tbl_Warehouse_Capacity_Offer_2021 group by Registration_Id) as OFR on OFR.Registration_Id=REG.Registration_Id left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedRegAmount from tbl_Payment_Status as PYT where CategoryName='REGISTRATION FEE' group by REGISTRATIONID) REGPMT on REG.Registration_Id=REGPMT.REGISTRATIONID left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedOfferAmount from tbl_Payment_Status as PYT where CategoryName='OFFER FEES' and TransactionDate>CONVERT(varchar(10),'02/19/2020',101) group by REGISTRATIONID) OFRPMT on REG.Registration_Id=OFRPMT.REGISTRATIONID where REG.EmailID='" + Session["email"].ToString().Trim() + "') as A where A.RegPaymentStatus='Confirmed')");
                int b = checkqry("select Wreg.Registration_Id from tbl_WarehouseRegistration as Wreg inner join tbl_Warehouse_PreReg as PReg on Wreg.Registration_Id=PReg.Reg_No where PReg.EmailID='" + Session["email"].ToString().Trim() + "' and (SELECT DATEDIFF(DAY, Wreg.Registration_Date, Getdate()) AS DateDiff)<=1099");

                if (b == 1)
                {
                    ModalPopupExtender1.Show();
                }
                else
                {
                    //int c = checkqry("select Wreg.Registration_Id from tbl_WarehouseRegistration as Wreg inner join tbl_Warehouse_PreReg as PReg on Wreg.Registration_Id=PReg.Reg_No where PReg.EmailID='" + Session["email"].ToString().Trim() + "' and (SELECT DATEDIFF(DAY, Wreg.Registration_Date, Getdate()) AS DateDiff)>=1099 and Wreg.Registration_Id in (select A.Registration_Id from (select REG.Registration_Id,RegCapacity,RegAmt*2 as RegAmt ,isnull(REGPMT.DepositedRegAmount,0) DepositedRegAmt,case when RegAmt*2<=isnull(REGPMT.DepositedRegAmount,0) then 'Confirmed' else 'Pending' end RegPaymentStatus,isnull(Offer_Capacity,0)Offer_Capacity,isnull(OfferAmt,0)OfferAmt,isnull(OFRPMT.DepositedOfferAmount,0) ofrdepositedamt,case when OfferAmt<=isnull(OFRPMT.DepositedOfferAmount,0) then 'Confirmed' else 'Pending' end OfferPaymentStatus from tbl_WarehouseRegistration as REG inner join tbl_Warehouse_PreReg as PREG on REG.Registration_Id=PREG.Reg_No left join (select Registration_Id,SUM(Offer_Capacity) as Offer_Capacity,SUM(OfferAmt)as OfferAmt from  tbl_Warehouse_Capacity_Offer_2021 group by Registration_Id) as OFR on OFR.Registration_Id=REG.Registration_Id left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedRegAmount from tbl_Payment_Status as PYT where CategoryName='REGISTRATION FEE' group by REGISTRATIONID) REGPMT on REG.Registration_Id=REGPMT.REGISTRATIONID left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedOfferAmount from tbl_Payment_Status as PYT where CategoryName='OFFER FEES' and TransactionDate>CONVERT(varchar(10),'02/19/2021',101) group by REGISTRATIONID) OFRPMT on REG.Registration_Id=OFRPMT.REGISTRATIONID where REG.EmailID='" + Session["email"].ToString().Trim() + "') as A where A.RegPaymentStatus='Confirmed')");
                    int c = checkqry("select ttt.REGISTRATIONID,ttt.DateDiff From(Select tt.REGISTRATIONID,tt.TransactionDate,DATEDIFF(DAY, TransactionDate, Getdate()) AS DateDiff From(select top 1 REGISTRATIONID, TransactionDate from tbl_Payment_Status as PYT where CategoryName like '%REGISTRATION%' AND REGISTRATIONID in (select Wreg.Registration_Id from tbl_WarehouseRegistration as Wreg inner join tbl_Warehouse_PreReg as PReg on Wreg.Registration_Id = PReg.Reg_No where PReg.EmailID = '" + Session["email"].ToString().Trim() + "') order by TransactionDate desc)tt)ttt where ttt.DateDiff<=1099");
                    
                    if (c == 1)
                    {
                        ModalPopupExtender1.Show();
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('आफ़र करने के लिए पहले रजिस्ट्रेशन Re-New करे...'); </script> ");
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('आफ़र करने के लिए पहले अपना रजिस्ट्रेशन अपडेट करें...'); </script> ");
            }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registration Not Complete......'); </script> ");
        }
    //}

    //else
    //{
    //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('पहले श्रेणी चयन करें......'); </script> ");
    //}
    // Commend for Stop Offer for Kharif 2022-23
    //string chkup = checkupdate();
    //if (chkup == "Y")
    //{
    //ModalPopupExtender1.Show();
        //}
        //else
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Online JVS Offer for 2023-24 will be Start From 27/02/2023......'); </script> ");
        //}
    }
    protected void btnpaymentoffer_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://www.onlinesbi.sbi/sbicollect/icollecthome.htm?corpID=329338");
    }
    protected void btnPreviewregofr_Click(object sender, EventArgs e)
    {
        int a = checkqry("select Wreg.Registration_Id from tbl_WarehouseRegistration as Wreg inner join tbl_Warehouse_PreReg as PReg on Wreg.Registration_Id=PReg.Reg_No where PReg.EmailID='" + Session["email"].ToString().Trim() + "'");
        if (a == 1)
        {
            string chkup = checkupdate();
            if (chkup == "Y")
            {
                Response.Redirect("UpdatePrintPreview.aspx");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('पहले अपना रजिस्ट्रेशन अपडेट करें...'); </script> ");
            }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registration Not Complete......'); </script> ");
        }
    }

    public int checkqry(string strsql)
    {
        int ch = 0;
        SqlDataAdapter da = new SqlDataAdapter(strsql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ch = 1;
        }
        else
        {
            ch = 0;
        }
        return ch;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (chkreg1.Checked == true && chkreg2.Checked == true && chkreg3.Checked == true && chkregparisar.Checked == true && chkregtype.Checked == true)
        {
            Response.Redirect("WarehouseRegistration.aspx");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('रजिस्ट्रेशन करने के लिए सभी आवश्यक निर्देशों का चुनाव करना आनिवार्य हें |......'); </script> ");
            ModalPopupExtender2.Show();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //if (chkofr1.Checked == true && chkofr2.Checked == true && chkofr3.Checked == true)
        if (chkofr1.Checked == true && chkofr3.Checked == true)
        {
            //Response.Redirect("Offered.aspx");
            //Response.Redirect("Offer_Kharif2022_23.aspx");
            //Response.Redirect("Offer_Rabi2023_24.aspx");
            //Response.Redirect("Offer_Rabi2024_25.aspx");
            //Response.Redirect("Offer_Rabi2025_26.aspx");
            Response.Redirect("Offer_Rabi2026_27.aspx");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('ऑफर करने के लिए सभी आवश्यक निर्देशों का चुनाव करना आनिवार्य हें |......'); </script> ");
            ModalPopupExtender1.Show();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("AppealUnfitGdwn.aspx");
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
    protected void btnChange_Click(object sender, EventArgs e)
    {
        if (txtForgotPassword.Value == "" && txtnewpass.Value == "" && txtnewpassre.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter All Field'); </script> ");
            ModalPopupExtender3.Show();
        }
        else
        {
            string pass = "";
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string QueryMax = "select Auth_Person,MobileNo,EmailID,Password from tbl_Warehouse_PreReg where EmailID='" + Session["email"].ToString().Trim() + "'";
            SqlDataAdapter da = new SqlDataAdapter(QueryMax, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                pass = ds.Tables[0].Rows[0]["Password"].ToString().Trim();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (pass == txtForgotPassword.Value.Trim())
                {
                    if (txtnewpass.Value.Trim() == txtnewpassre.Value.Trim())
                    {
                        qry = "INSERT INTO [tbl_Warehouse_PreReg_Log] select * FROM [tbl_Warehouse_PreReg] where EmailID='" + Session["email"].ToString() + "'";
                        cmd = new SqlCommand(qry, con);
                        int A1 = 0;
                        A1 = cmd.ExecuteNonQuery();
                        if (A1 == 1)
                        {
                            qry = "UPDATE [tbl_Warehouse_PreReg] SET Password='" + txtnewpass.Value.Trim() + "' ,[UpdatedBy] = '" + ip + "',[UpdatedDate] = GETDATE() where EmailID='" + Session["email"].ToString() + "'";
                            cmd = new SqlCommand(qry, con);
                            int CT = 0;
                            CT = cmd.ExecuteNonQuery();
                            if (CT == 1)
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Change Password'); </script> ");
                                txtForgotPassword.Value = ""; txtnewpass.Value = ""; txtnewpassre.Value = "";
                            }
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Password Does Not Match please Re-Enter Correct Password'); </script> ");
                        ModalPopupExtender3.Show();
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Old Password Does Not Match'); </script> ");
                    ModalPopupExtender3.Show();
                }

            }
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("Warehouse_TrackPaymentStatus.aspx");
    }



    protected void btnjvs_Click1(object sender, EventArgs e)
    {
        string TStatus = Tcheckdatetimes();
        if (TStatus == "Y")
        {

            string strsql = "select * from Tbl_JVS_Choise_Filling where Reg_ID='" + Session["Reg_No"].ToString() + "'";


            SqlDataAdapter da = new SqlDataAdapter(strsql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Choice Filling  हो चुकी हैं |'); </script> ");
            }
            else
            {
                int a = checkqry("select Wreg.Registration_Id from tbl_WarehouseRegistration as Wreg inner join tbl_Warehouse_PreReg as PReg on Wreg.Registration_Id=PReg.Reg_No where PReg.EmailID='" + Session["email"].ToString().Trim() + "'");
                if (a == 1)
                {
                    string chkup = checkupdate();
                    if (chkup == "Y")
                    {
                        //int b = checkqry("select Wreg.Registration_Id from tbl_WarehouseRegistration as Wreg inner join tbl_Warehouse_PreReg as PReg on Wreg.Registration_Id=PReg.Reg_No where PReg.EmailID='" + Session["email"].ToString().Trim() + "' and (SELECT DATEDIFF(DAY, Wreg.Registration_Date, Getdate()) AS DateDiff)<=1096");
                        //int b = checkqry("select Wreg.Registration_Id from tbl_WarehouseRegistration as Wreg inner join tbl_Warehouse_PreReg as PReg on Wreg.Registration_Id=PReg.Reg_No where PReg.EmailID='" + Session["email"].ToString().Trim() + "' and (SELECT DATEDIFF(DAY, Wreg.Registration_Date, Getdate()) AS DateDiff)<=1099 and Wreg.Registration_Id in (select A.Registration_Id from (select REG.Registration_Id,RegCapacity,RegAmt*2 as RegAmt ,isnull(REGPMT.DepositedRegAmount,0) DepositedRegAmt,case when RegAmt*2<=isnull(REGPMT.DepositedRegAmount,0) then 'Confirmed' else 'Pending' end RegPaymentStatus,isnull(Offer_Capacity,0)Offer_Capacity,isnull(OfferAmt,0)OfferAmt,isnull(OFRPMT.DepositedOfferAmount,0) ofrdepositedamt,case when OfferAmt<=isnull(OFRPMT.DepositedOfferAmount,0) then 'Confirmed' else 'Pending' end OfferPaymentStatus from tbl_WarehouseRegistration as REG inner join tbl_Warehouse_PreReg as PREG on REG.Registration_Id=PREG.Reg_No left join (select Registration_Id,SUM(Offer_Capacity) as Offer_Capacity,SUM(OfferAmt)as OfferAmt from  tbl_Warehouse_Capacity_Offer_2021 group by Registration_Id) as OFR on OFR.Registration_Id=REG.Registration_Id left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedRegAmount from tbl_Payment_Status as PYT where CategoryName='REGISTRATION FEE' group by REGISTRATIONID) REGPMT on REG.Registration_Id=REGPMT.REGISTRATIONID left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedOfferAmount from tbl_Payment_Status as PYT where CategoryName='OFFER FEES' and TransactionDate>CONVERT(varchar(10),'02/19/2020',101) group by REGISTRATIONID) OFRPMT on REG.Registration_Id=OFRPMT.REGISTRATIONID where REG.EmailID='" + Session["email"].ToString().Trim() + "') as A where A.RegPaymentStatus='Confirmed')");
                        int b = checkqry("select Wreg.Registration_Id from tbl_WarehouseRegistration as Wreg inner join tbl_Warehouse_PreReg as PReg on Wreg.Registration_Id=PReg.Reg_No where PReg.EmailID='" + Session["email"].ToString().Trim() + "' and (SELECT DATEDIFF(DAY, Wreg.Registration_Date, Getdate()) AS DateDiff)<=1099");

                        if (b == 1)
                        {
                            ModalPopupExtender4.Show();
                        }
                        else
                        {
                            int c = checkqry("select Wreg.Registration_Id from tbl_WarehouseRegistration as Wreg inner join tbl_Warehouse_PreReg as PReg on Wreg.Registration_Id=PReg.Reg_No where PReg.EmailID='" + Session["email"].ToString().Trim() + "' and (SELECT DATEDIFF(DAY, Wreg.Registration_Date, Getdate()) AS DateDiff)>=1099 and Wreg.Registration_Id in (select A.Registration_Id from (select REG.Registration_Id,RegCapacity,RegAmt*2 as RegAmt ,isnull(REGPMT.DepositedRegAmount,0) DepositedRegAmt,case when RegAmt*2<=isnull(REGPMT.DepositedRegAmount,0) then 'Confirmed' else 'Pending' end RegPaymentStatus,isnull(Offer_Capacity,0)Offer_Capacity,isnull(OfferAmt,0)OfferAmt,isnull(OFRPMT.DepositedOfferAmount,0) ofrdepositedamt,case when OfferAmt<=isnull(OFRPMT.DepositedOfferAmount,0) then 'Confirmed' else 'Pending' end OfferPaymentStatus from tbl_WarehouseRegistration as REG inner join tbl_Warehouse_PreReg as PREG on REG.Registration_Id=PREG.Reg_No left join (select Registration_Id,SUM(Offer_Capacity) as Offer_Capacity,SUM(OfferAmt)as OfferAmt from  tbl_Warehouse_Capacity_Offer_2021 group by Registration_Id) as OFR on OFR.Registration_Id=REG.Registration_Id left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedRegAmount from tbl_Payment_Status as PYT where CategoryName='REGISTRATION FEE' group by REGISTRATIONID) REGPMT on REG.Registration_Id=REGPMT.REGISTRATIONID left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedOfferAmount from tbl_Payment_Status as PYT where CategoryName='OFFER FEES' and TransactionDate>CONVERT(varchar(10),'02/19/2020',101) group by REGISTRATIONID) OFRPMT on REG.Registration_Id=OFRPMT.REGISTRATIONID where REG.EmailID='" + Session["email"].ToString().Trim() + "') as A where A.RegPaymentStatus='Confirmed')");
                            if (c == 1)
                            {
                                ModalPopupExtender4.Show();
                            }
                            else
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('आफ़र करने के लिए पहले रजिस्ट्रेशन Re-New करे...'); </script> ");
                            }
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('आफ़र करने के लिए पहले अपना रजिस्ट्रेशन अपडेट करें...'); </script> ");
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registration Not Complete......'); </script> ");
                }
            }
        }    
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Date for Choice Filling has been Finished...'); </script> ");
                }

}

    protected void Button6_Click(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true && CheckBox2.Checked == true && CheckBox3.Checked == true && CheckBox4.Checked==true)
        {
            Response.Redirect("OfferedJvs.aspx");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('ऑफर करने के लिए सभी आवश्यक निर्देशों का चुनाव करना आनिवार्य हें |......'); </script> ");
            ModalPopupExtender4.Show();
        }
    }

    void checkdate()
    {


    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        Response.Redirect("Warehouse_ChoiseFillingGodown.aspx");
    }



    protected void UpdateGodownAToB_Click(object sender, EventArgs e)
    {
        Response.Redirect("Godown_ChoiceFillingJvs.aspx");
		//Response.Redirect("#");
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        Response.Redirect("Godown_JVSBranchReport.aspx");
        //Response.Redirect("#");
    }

    protected void ButtonChoisePMS_Click(object sender, EventArgs e)
    {
        Response.Redirect("Warehouse_Self_And_Pms_Choise_Felling.aspx");
    }
}
