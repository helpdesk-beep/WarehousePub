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

public partial class Accounting_frm_View_Bill_Detail : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                Fill_Master_Bill_Detail();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void Fill_Master_Bill_Detail()
    {
     
        string BranchID = Session["BranchID"].ToString();
        //qry = "select SB.Bill_Number,Bill_Type,Sub_Amount,case when Bill_Type='AD' then 'Daily Storage Charges Bill' when Bill_Type='AU' then 'Accrued Storage Charges Bill' when Bill_Type='OD' then 'Daily Over & Above Storage Charges Bill' when Bill_Type='OU' then 'Accrued Over & Above Storage Charges Bill' when Bill_Type='RB' then 'Reservation Bill' else '' end as Bill_Name,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount from tbl_Storage_Bill_Details as SB where SB.Branch_Id='" + BranchID + "'";
        qry = "select SB.Bill_Number,Bill_Type,Sub_Amount,case when Bill_Type='AD' then 'Daily Storage Charges Bill' when Bill_Type='AU' then 'Accrued Storage Charges Bill' when Bill_Type='OD' then 'Daily Over & Above Storage Charges Bill' when Bill_Type='OU' then 'Accrued Over & Above Storage Charges Bill' when SB.Bill_Type='HG' then 'Godown(Hired) Rent Bill' when SB.Bill_Type='GR' then 'Godown(JVS) Rent Bill' when Bill_Type='RB' then 'Reservation Bill' else '' end as Bill_Name,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount from tbl_Storage_Bill_Details as SB where SB.Branch_Id='" + BranchID + "'";
        cmd.CommandText = qry;
        cmd.Connection = con;
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            gvMasterBill.DataSource = dt;
            gvMasterBill.DataBind();
            trRentBill.Visible = true;
        }
    }
    protected void gvMasterBill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "bill")
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                string BillNo = Convert.ToString(gvMasterBill.DataKeys[row.RowIndex].Value);

                if (BillNo != "")
                {
                    //Session["GodownId"] = GodownId;
                    //Response.Redirect("~/Masters/StackMaster.aspx");
                    Get_Bill_Detail(BillNo);
                }
                else
                {

                }

            }
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
 
    }
    public void Get_Bill_Detail(string BN)
    {
        string Bill_No = BN;
        string Bill_Name = "";
        string Bill_Types = "";
        Bill_Types = Chk_Bill_Type(Bill_No);
        if (Bill_Types == "AD")
        {
            Bill_Name = "Daily Storage Charges Bill";
            qry = "select CONVERT(varchar(10),Dates,103) as Date,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges from tbl_Bills_Daily_Storage_Charges_Details where Bill_Number='" + Bill_No + "'";
        }
        else if (Bill_Types == "AU")
        {
            Bill_Name = "Accrued Storage Charges Bill";
            qry = "select (select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=A.Commodity_Id) as Commodity,WHR_No,CONVERT(varchar(10),A.Deposit_Date,103) as Deposit_Date,CONVERT(varchar(10),A.Delivery_Date,103) as Delivery_Date,A.Deposit_Bags,A.Monthly_Rate,A.Rebate_Bags,A.Rebate_Per,A.Net_Amount from tbl_Accrued_Storage_Charges_WHR_Details as A where A.Bill_Number='"+ Bill_No +"'";
        }
        else if (Bill_Types == "OD")
        {
            Bill_Name = "Daily Over & Above Storage Charges Bill";
            qry = "select CONVERT(varchar(10),Dates,103) as Date,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Reserved_Bag,Over_Bag,Per_Day_Rate,Total_Charges from tbl_OVER_N_ABOVE_Storage_Daily_Charges where Bill_Number='" + Bill_No + "'";
        }
        else if (Bill_Types == "OU")
        {
            Bill_Name = "Accrued Over & Above Storage Charges Bill";
            qry = "SELECT convert(varchar(10),BDSC.Dates,103) as Date,BDSC.Opening_Balance,BDSC.Receive_Bags,BDSC.Issue_Bags,BDSC.Closing_Balance,BDSC.Reserved_Bag,BDSC.Over_Bag,case when BDSC.Accrued_Period='H' then '15 Days' when BDSC.Accrued_Period='M' then '01 Month' else '' end as Accrued_Period,BDSC.Monthly_Rate,BDSC.Total_Charges FROM tbl_OVER_N_ABOVE_Accrued_Storage_Charges  as BDSC WHERE BDSC.Bill_Number='"+ Bill_No +"'";
            //qry = "select CONVERT(varchar(10),Dates,103) as Date,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Reserved_Bag,Over_Bag,Monthly_Rate,Total_Charges,Accrued_Period from tbl_OVER_N_ABOVE_Accrued_Storage_Charges where Bill_Number='" + Bill_No + "'";
        }
        else if (Bill_Types == "RB")
        {
            Bill_Name = "Reservation Bill";
            qry = "select (select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=R.Commodity_Id) as Commodity,CONVERT(varchar(10),R.From_Date,103) as Reserve_From,CONVERT(varchar(10),R.To_Date,103) as Reserve_To,R.Reservation_Period,R.Reserved_Capacity,R.Commodity_Rate,R.Net_Amount from tbl_Reseravation_Bill_Details as R where Bill_Number='" + Bill_No + "'";
        }
        else
        {

        }

        cmd.CommandText = qry;
        cmd.Connection = con;
        da.SelectCommand = cmd;
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            gvB.DataSource = dt1;
            gvB.DataBind();
            trb1.Visible = true;
            trRentBill.Visible = false;
            lblbillname.Text = Bill_Name;
        }
    }
    public string Chk_Bill_Type(string BillNo)
    {
        string Bill_Types = "";
        string Bill_No = "";
        Bill_No = BillNo;

        qry = "select Bill_Type from tbl_Storage_Bill_Details where Bill_Number='"+ Bill_No +"'";
        cmd.CommandText = qry;
        cmd.Connection = con;
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows[0][0].ToString() != "")
        {
            Bill_Types = dt.Rows[0][0].ToString();
        }
        return Bill_Types;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Fill_Master_Bill_Detail();
        trRentBill.Visible = true;
        Response.Redirect("~/Accounting/frm_View_Bill_Detail.aspx");
        trb1.Visible = false;
    }
}
