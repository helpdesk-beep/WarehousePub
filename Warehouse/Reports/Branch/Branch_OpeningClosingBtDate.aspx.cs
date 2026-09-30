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
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class IssueCenterLevel_Storage_Branch_OpeningClosingBtDate : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        if (dprlst_Commodity.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Commodity..')", true);

        }
        else if (fromDate.Text=="")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select From Date..')", true);
        }
        else if (todate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select To Date..')", true);
        }
        else 
        {
            try
            {
            string datefrom = getDate_MDY(fromDate.Text);
            string dateto = getDate_MDY(todate.Text);
            string datefrm = datefrom;
            DataSet sqlds = new DataSet();
            int total_days = Convert.ToDateTime(dateto).Day - Convert.ToDateTime(datefrm).Day;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

                //DataTable dt1 = new DataTable();
                //SqlCommand sqlComm = new SqlCommand("Alldates_BetweenDate", con);
                //sqlComm.CommandType = CommandType.StoredProcedure;
                //sqlComm.Parameters.AddWithValue("@fromdate", datefrom);
                //sqlComm.Parameters.AddWithValue("@todate", dateto);
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = sqlComm;
                //da.Fill(dt1);
                //sqlComm.Dispose();
                dt.Columns.Add("DateCorrect", typeof(string));
                dt.Columns.Add("OpneBal", typeof(string));
                dt.Columns.Add("RecWeight", typeof(string));
                dt.Columns.Add("IssueWeight", typeof(string));
                dt.Columns.Add("ClosingBl", typeof(string));
                for (int i = 1; i <= total_days+1; i++)
                {
                    string qry = "select COALESCE(RecDel.BranchID,OpneBal.BranchID) as BranchID, dateon as Datetr ,OpneBal.OpeningBl,isnull(RecDel.RecWeight,0) as RecWeight,isnull(RecDel.IssueWeight,0) as IssueWeight,(OpeningBl+isnull(RecWeight,0)-isnull(IssueWeight,0)) as ClosingBl from (select COALESCE(DelDetail.BranchID,RecDetail.BranchID) as BranchID,COALESCE(DelDetail.GPDate,RecDetail.WHRDate) as DateOn,isnull(RecWeight,0) as RecWeight,isnull(IssueWeight,0) as IssueWeight  from (select WHR.BranchID,convert(varchar(20),WHR_Issue_Date,103) as WHRDate,SUM(Weight) as RecWeight from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID = '" + Session["BranchID"].ToString() + "' and Depositor_Name ='MPSCSC' and commodity_id ='" + dprlst_Commodity.SelectedValue.ToString() + "' and WHR_Issue_Date = convert(varchar(20),'" + datefrm + "',102) group by WHR_Issue_Date,WHR.BranchID ) as RecDetail full outer join ( (select sge.BranchID,convert(varchar(20),Issue_Date,103) as GPDate,sum(dsdg.DelWeight+Loss-Gain)as IssueWeight from  (select Godown_ID,GatePass_No,sum(Bags_Weight) as DelWeight,isnull(SUM(Loss),0) as Loss ,isnull(sum(Gain),0) as Gain  from  tbl_Delivery_Stacking_Details_GatePass  where Depositor_WHR_Id in ( select WHR.Depositor_WHR_Id from tbl_storage_Stacking_Details as SSD join  tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id  where WHR.BranchID = '" + Session["BranchID"].ToString() + "' and Depositor_Name ='MPSCSC' and commodity_id ='" + dprlst_Commodity.SelectedValue.ToString() + "' ) group by GatePass_No,godown_id) as dsdg  inner join tbl_Storage_GatePass_Enrty as sge on dsdg.GatePass_No = sge.GatePass_No where sge.Issue_Date = convert(varchar(10),'" + datefrm + "',102) and Commodity_ID ='" + dprlst_Commodity.SelectedValue.ToString() + "' and  sge.Status ='Active' and sge.BranchID='" + Session["BranchID"].ToString() + "' group by Issue_Date,sge.BranchID)) as DelDetail on DelDetail.GPDate=RecDetail.WHRDate ) as RecDel full outer join ( select RecDetail.BranchID,RecWeight-IssueWeight as OpeningBl from ( select WHR.BranchID,SUM(Weight) as RecWeight from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID = '" + Session["BranchID"].ToString() + "' and Depositor_Name ='MPSCSC' and commodity_id ='" + dprlst_Commodity.SelectedValue.ToString() + "'  and WHR.WHR_Issue_Date < convert(varchar(10),'" + datefrm + "',102) group by WHR.BranchID ) as RecDetail full outer join( (select sge.BranchID,sum(dsdg.DelWeight+Loss-Gain)as IssueWeight from  (select Godown_ID,GatePass_No,sum(Bags_Weight) as DelWeight,isnull(SUM(Loss),0) as Loss ,isnull(sum(Gain),0) as Gain  from  tbl_Delivery_Stacking_Details_GatePass where Depositor_WHR_Id in ( select WHR.Depositor_WHR_Id from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID = '" + Session["BranchID"].ToString() + "' and Depositor_Name ='MPSCSC' and commodity_id ='" + dprlst_Commodity.SelectedValue.ToString() + "' ) group by GatePass_No,godown_id) as dsdg  inner  join tbl_Storage_GatePass_Enrty as sge on dsdg.GatePass_No = sge.GatePass_No where sge.Issue_Date < convert(varchar(10),'" + datefrm + "',102)and Commodity_ID ='" + dprlst_Commodity.SelectedValue.ToString() + "' and  sge.Status ='Active' and sge.BranchID='" + Session["BranchID"].ToString() + "' group by sge.BranchID) ) as DelDetail on RecDetail.BranchID=DelDetail.BranchID) as OpneBal on OpneBal.BranchID=RecDel.BranchID ";
                    SqlDataAdapter sda = new SqlDataAdapter(qry, con);

                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    DataRow row = dt.NewRow();

                    var OpneBal = ds.Tables[0].Rows[0]["OpeningBl"].ToString();
                    var RecQty = ds.Tables[0].Rows[0]["RecWeight"].ToString();
                    var Delqty = ds.Tables[0].Rows[0]["IssueWeight"].ToString();
                    var CloBal = ds.Tables[0].Rows[0]["ClosingBl"].ToString();

                    //row["OpneBal"] = ds.Tables[0].Rows[0]["OpeningBl"].ToString();
                    //row["RecWeight"] = ds.Tables[0].Rows[0]["RecWeight"].ToString();
                    //row["IssueWeight"] = ds.Tables[0].Rows[0]["IssueWeight"].ToString();
                    //row["ClosingBl"] = ds.Tables[0].Rows[0]["ClosingBl"].ToString();
                    //row["Datetr"] = ds.Tables[0].Rows[0]["Datetr"].ToString();

                    DateTime date = Convert.ToDateTime(datefrm);
                    dt.Rows.Add(date.ToString("dd/MM/yyyy"), OpneBal, RecQty, Delqty, CloBal);
                    //DateTime date = Convert.ToDateTime(datefrm);
                    date = date.AddDays(1);
                    datefrm = date.ToString("MM/dd/yyyy");
                }
                gv_whr.DataSource = dt;
                gv_whr.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void dprlst_Commodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_whr.DataSource = null;
        gv_whr.DataBind();
    }
}
