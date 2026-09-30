using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;


public partial class JointVentureScheme_Branch_CheckOffer_WH_Status : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        string SessBranch = Session["UserName"].ToString();
        string SessBranchID = Session["UserId"].ToString();
        if (SessBranchID != "" && SessBranch != "")
        {
            if (!IsPostBack)
            {
                lbluser.Text = SessBranch;
                gerreg();
            }
        }
    }
    public void gerreg()
    {
        try
        {
            string qry = "";
            //qry = "select REG.Registration_Id,REG.Warehouse_name,RegCapacity,RegAmt ,isnull(REGPMT.DepositedRegAmount,0) DepositedRegAmt,case when RegAmt<=isnull(REGPMT.DepositedRegAmount,0) then 'Confirmed' else 'Pending' end RegPaymentStatus,isnull(Offer_Capacity,0)Offer_Capacity,isnull(OfferAmt,0)OfferAmt,isnull(OFRPMT.DepositedOfferAmount,0) ofrdepositedamt,case when OfferAmt<=isnull(OFRPMT.DepositedOfferAmount,0) then 'Confirmed' else 'Pending' end OfferPaymentStatus from tbl_WarehouseRegistration as REG inner join tbl_Warehouse_PreReg as PREG on REG.Registration_Id=PREG.Reg_No left join (select Registration_Id,SUM(Offer_Capacity) as Offer_Capacity,SUM(OfferAmt)as OfferAmt from  tbl_Warehouse_Capacity_Offer_2019 group by Registration_Id) as OFR on OFR.Registration_Id=REG.Registration_Id left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedRegAmount from tbl_Payment_Status as PYT where CategoryName='REGISTRATION FEE' group by REGISTRATIONID) REGPMT on REG.Registration_Id=REGPMT.REGISTRATIONID left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedOfferAmount from tbl_Payment_Status as PYT where CategoryName='OFFER FEES' and TransactionDate>CONVERT(varchar(10),'02/17/2019',101) group by REGISTRATIONID) OFRPMT on REG.Registration_Id=OFRPMT.REGISTRATIONID  where REG.BranchID='" + Session["UserId"].ToString() + "'";
            //qry = "select REG.Registration_Id,REG.Warehouse_name,RegCapacity,RegAmt ,isnull(REGPMT.DepositedRegAmount,0) DepositedRegAmt,case when RegAmt<=isnull(REGPMT.DepositedRegAmount,0) then 'Confirmed' else 'Pending' end RegPaymentStatus,isnull(Offer_Capacity,0)Offer_Capacity,isnull(OfferAmt,0)OfferAmt,isnull(OFRPMT.DepositedOfferAmount,0) ofrdepositedamt,case when OfferAmt<=isnull(OFRPMT.DepositedOfferAmount,0) then 'Confirmed' else 'Pending' end OfferPaymentStatus from tbl_WarehouseRegistration as REG inner join tbl_Warehouse_PreReg as PREG on REG.Registration_Id=PREG.Reg_No left join (select Registration_Id,SUM(Offer_Capacity) as Offer_Capacity,SUM(OfferAmt)as OfferAmt from  tbl_Warehouse_Capacity_Offer_2020 group by Registration_Id) as OFR on OFR.Registration_Id=REG.Registration_Id left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedRegAmount from tbl_Payment_Status as PYT where CategoryName='REGISTRATION FEE' group by REGISTRATIONID) REGPMT on REG.Registration_Id=REGPMT.REGISTRATIONID left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedOfferAmount from tbl_Payment_Status as PYT where CategoryName='OFFER FEES' and TransactionDate>CONVERT(varchar(10),'02/19/2020',101) group by REGISTRATIONID) OFRPMT on REG.Registration_Id=OFRPMT.REGISTRATIONID  where REG.BranchID='" + Session["UserId"].ToString() + "'";
            //qry = "select REG.Registration_Id,REG.Warehouse_name,RegCapacity,RegAmt ,isnull(REGPMT.DepositedRegAmount,0) DepositedRegAmt,case when RegAmt<=isnull(REGPMT.DepositedRegAmount,0) then 'Confirmed' else 'Pending' end RegPaymentStatus,isnull(Offer_Capacity,0)Offer_Capacity,isnull(OfferAmt,0)OfferAmt,isnull(OFRPMT.DepositedOfferAmount,0) ofrdepositedamt,case when OfferAmt<=isnull(OFRPMT.DepositedOfferAmount,0) then 'Confirmed' else 'Pending' end OfferPaymentStatus from tbl_WarehouseRegistration as REG inner join tbl_Warehouse_PreReg as PREG on REG.Registration_Id=PREG.Reg_No left join (select Registration_Id,SUM(Offer_Capacity) as Offer_Capacity,SUM(OfferAmt)as OfferAmt from  tbl_Warehouse_Capacity_Offer_2022 group by Registration_Id) as OFR on OFR.Registration_Id=REG.Registration_Id left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedRegAmount from tbl_Payment_Status as PYT where CategoryName='REGISTRATION FEE' group by REGISTRATIONID) REGPMT on REG.Registration_Id=REGPMT.REGISTRATIONID left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedOfferAmount from tbl_Payment_Status as PYT where CategoryName='OFFER FEES' and TransactionDate>CONVERT(varchar(10),'3/10/2022',101) group by REGISTRATIONID) OFRPMT on REG.Registration_Id=OFRPMT.REGISTRATIONID  where REG.BranchID='" + Session["UserId"].ToString() + "'";
            //qry = "select REG.Registration_Id,REG.Warehouse_name,RegCapacity,RegAmt ,isnull(REGPMT.DepositedRegAmount,0) DepositedRegAmt,case when RegAmt<=isnull(REGPMT.DepositedRegAmount,0) then 'Confirmed' else 'Pending' end RegPaymentStatus,isnull(Offer_Capacity,0)Offer_Capacity,isnull(OfferAmt,0)OfferAmt,isnull(OFRPMT.DepositedOfferAmount,0) ofrdepositedamt,case when OfferAmt<=isnull(OFRPMT.DepositedOfferAmount,0) then 'Confirmed' else 'Pending' end OfferPaymentStatus from tbl_WarehouseRegistration as REG inner join tbl_Warehouse_PreReg as PREG on REG.Registration_Id=PREG.Reg_No INNER join (select Registration_Id,SUM(Offer_Capacity) as Offer_Capacity,SUM(OfferAmt)as OfferAmt from tbl_Warehouse_Capacity_Offer_Rabi_2024_25 group by Registration_Id) as OFR on OFR.Registration_Id=REG.Registration_Id left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedRegAmount from tbl_Payment_Status as PYT where CategoryName='REGISTRATION FEE' and (SELECT DATEDIFF(DAY, TransactionDate, Getdate()) AS DateDiff) <= 1099 group by REGISTRATIONID) REGPMT on REG.Registration_Id=REGPMT.REGISTRATIONID left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedOfferAmount from tbl_Payment_Status as PYT where CategoryName='OFFER FEES' and TransactionDate>=CONVERT(varchar(10),'3/15/2024',101) group by REGISTRATIONID) OFRPMT on REG.Registration_Id=OFRPMT.REGISTRATIONID  where REG.BranchID='" + Session["UserId"].ToString() + "'";
            //qry = "select REG.Registration_Id,REG.Warehouse_name,RegCapacity,RegAmt ,isnull(REGPMT.DepositedRegAmount,0) DepositedRegAmt,case when RegAmt<=isnull(REGPMT.DepositedRegAmount,0) then 'Confirmed' else 'Pending' end RegPaymentStatus,isnull(Offer_Capacity,0)Offer_Capacity,isnull(OfferAmt,0)OfferAmt,isnull(OFRPMT.DepositedOfferAmount,0) ofrdepositedamt,case when OfferAmt<=isnull(OFRPMT.DepositedOfferAmount,0) then 'Confirmed' else 'Pending' end OfferPaymentStatus from tbl_WarehouseRegistration as REG inner join tbl_Warehouse_PreReg as PREG on REG.Registration_Id=PREG.Reg_No INNER join (select Registration_Id,SUM(Offer_Capacity) as Offer_Capacity,SUM(OfferAmt)as OfferAmt from tbl_Warehouse_Capacity_Offer_Rabi_2025_26 group by Registration_Id) as OFR on OFR.Registration_Id=REG.Registration_Id left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedRegAmount from tbl_Payment_Status as PYT where CategoryName='REGISTRATION FEE' and (SELECT DATEDIFF(DAY, TransactionDate, Getdate()) AS DateDiff) <= 1099 group by REGISTRATIONID) REGPMT on REG.Registration_Id=REGPMT.REGISTRATIONID left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedOfferAmount from tbl_Payment_Status as PYT where CategoryName='OFFER FEES' and TransactionDate>=CONVERT(varchar(10),'03/05/2025',101) group by REGISTRATIONID) OFRPMT on REG.Registration_Id=OFRPMT.REGISTRATIONID  where REG.BranchID='" + Session["UserId"].ToString() + "'";
            qry = "select REG.Registration_Id,REG.Warehouse_name,RegCapacity,RegAmt ,isnull(REGPMT.DepositedRegAmount,0) DepositedRegAmt,case when RegAmt<=isnull(REGPMT.DepositedRegAmount,0) then 'Confirmed' else 'Pending' end RegPaymentStatus,isnull(Offer_Capacity,0)Offer_Capacity,isnull(OfferAmt,0)OfferAmt,isnull(OFRPMT.DepositedOfferAmount,0) ofrdepositedamt,case when OfferAmt<=isnull(OFRPMT.DepositedOfferAmount,0) then 'Confirmed' else 'Pending' end OfferPaymentStatus from tbl_WarehouseRegistration as REG inner join tbl_Warehouse_PreReg as PREG on REG.Registration_Id=PREG.Reg_No INNER join (select Registration_Id,SUM(Offer_Capacity) as Offer_Capacity,SUM(OfferAmt)as OfferAmt from tbl_Warehouse_Capacity_Offer_Rabi_2026_27 group by Registration_Id) as OFR on OFR.Registration_Id=REG.Registration_Id left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedRegAmount from tbl_Payment_Status as PYT where CategoryName='REGISTRATION FEE' and (SELECT DATEDIFF(DAY, TransactionDate, Getdate()) AS DateDiff) <= 1099 group by REGISTRATIONID) REGPMT on REG.Registration_Id=REGPMT.REGISTRATIONID left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedOfferAmount from tbl_Payment_Status as PYT where CategoryName='OFFER FEES' and TransactionDate>=CONVERT(varchar(10),'03/18/2026',101) group by REGISTRATIONID) OFRPMT on REG.Registration_Id=OFRPMT.REGISTRATIONID  where REG.BranchID='" + Session["UserId"].ToString() + "'";
            
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                RegGrid.DataSource = ds;
                RegGrid.DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Wrong ')", true);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("UserReg.aspx");
    }
}
