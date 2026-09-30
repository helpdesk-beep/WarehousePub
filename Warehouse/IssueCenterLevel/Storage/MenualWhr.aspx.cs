using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class IssueCenterLevel_Storage_MenualWhr : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    DataSet ds = null;
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    SqlTransaction sqltran;
    string Branch = "";
    string Distid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (!IsPostBack)
            {
                fillProcNew();

            }

        }
    }

    protected void fillProcNew()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
               
                    //latest from accep tbl 16-06-15   
                 cmd = new SqlCommand("select  Acceptance_Date,Bags as Recd_Bags,Qty as Recd_Qty,whrr.Commodity_Id as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where Godown_ID=whrr.GodownID and tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id)as Godown ,WHR_Request,whrr.GodownID  from MPSCSCSVR.mpscsc.dbo.whrreq1 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.WHR_Request not in (select Distinct sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') and whrr.WHR_Request not in (select [DepositorForn]  FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MenualWHR] where DepositorForn =whrr.WHR_Request ) order by Acceptance_Date", con);

               //local cmd = new SqlCommand("select  Acceptance_Date,Bags as Recd_Bags,Qty as Recd_Qty,whrr.Commodity_Id as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where Godown_ID=whrr.GodownID and tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id)as Godown ,WHR_Request,whrr.GodownID  from mpscsc.dbo.whrreq1 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.WHR_Request not in (select Distinct sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') and whrr.WHR_Request not in (select [DepositorForn]  FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MenualWHR] where DepositorForn =whrr.WHR_Request ) order by Acceptance_Date", con);
                   cmd.CommandType = CommandType.Text;

                 
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    //SqlDataAdapter da2 = new SqlDataAdapter();
                    //DataSet ds2 = new DataSet();
                    //SqlCommand cmd2 = new SqlCommand();

                    //cmd2 = new SqlCommand("select  Acceptance_Date,Bags as Recd_Bags,Qty as Recd_Qty,'Wheat-PSS' as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where Godown_ID=whrr.GodownID and tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id)as Godown ,WHR_Request,whrr.GodownID  from mpscsc.dbo.whrreq1 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.WHR_Request not in (select [DepositorForn]  FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MenualWHR] where DepositorForn ='" + ds.Tables[0].Rows[0]["WHR_Request"].ToString() + "' ) order by Acceptance_Date", con);
                    //cmd2.CommandType = CommandType.Text;


                    //da2.SelectCommand = cmd2;
                    //da2.Fill(ds2);
                    //if (ds2.Tables[0].Rows.Count > 0)
                    //{
                        GridView1.DataSource = ds;
                        GridView1.DataBind();

                        GridView1.HeaderRow.Cells[8].Visible = false;
                        GridView1.HeaderRow.Cells[9].Visible = false;
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            GridView1.Rows[i].Cells[8].Visible = false;
                            GridView1.Rows[i].Cells[9].Visible = false;

                        }
                   // }
                   
                }
                else
                {
                   
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                sqltran = con.BeginTransaction();
                //foreach (GridViewRow gr2 in gvreceipt.Rows)
                //{
                DataSet ds = (DataSet)Session["ds_GridInfo"];

                // CheckBox chk_Delete = new CheckBox();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                string whrnum = ((TextBox)GridView1.SelectedRow.FindControl("txtwhr")).Text;
                string whrdate = ((TextBox)GridView1.SelectedRow.FindControl("txtwhrdate")).Text;

                if(whrnum !="" && whrdate !="")
                {

                  //////////////////////del_tbl_Storage_Arrival_Stock_DeleteLog/////////////////////////
                    string qry = "";

                    qry = "INSERT INTO [dbo].[tbl_MenualWHR]([MenualWHR],[WHRDate],[Qty],[Bags],[Godown],[Remark],[DepositorForn],[CreatedDate],[CreatedBy],[DepositDate],GodownID,BranchID,Commidity,Depositor) VALUES ('" + whrnum + "','" + getDate_MDY(whrdate) + "','" + GridView1.SelectedRow.Cells[2].Text.ToString() + "','" + GridView1.SelectedRow.Cells[3].Text.ToString() + "','" + GridView1.SelectedRow.Cells[8].Text.ToString() + "','Y','" + GridView1.SelectedRow.Cells[0].Text.ToString() + "',GetDate(),'" + ip + "','" + getDate_MDY(GridView1.SelectedRow.Cells[1].Text.ToString()) + "','" + GridView1.SelectedRow.Cells[8].Text.ToString() + "','" + Session["BranchId"].ToString() + "','" + GridView1.SelectedRow.Cells[9].Text.ToString() + "','129')";
                    cmd = new SqlCommand(qry, con, sqltran);
                    int rex = cmd.ExecuteNonQuery();
                    if (rex > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('WHR saved.....')", true);
                    }

                    //////////////////////tbl_Storage_Receipt_Details_DeleteLog/////////////////////////

                 
                    sqltran.Commit();
                    con.Close();
                    fillProcNew();
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter WHR Number and Date')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record found')", true);
            }
           
        }
        catch (Exception ex)
        {
            sqltran.Rollback();

        }
        finally
        {
            con.Close();
        }
    }
}