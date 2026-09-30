using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Reports_States_AllpendingReciving : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Fillgridpendingreciving();
        }
    }

    protected void Fillgridpendingreciving()
    {
        if ((Session["State_StateID"] != null))
        {
            try
            {
                
                
                string query = " SELECT DISTINCT D.District_Id, D.District_Name, TMD.DepotID, TMD.DepotName,(SELECT COUNT([Acceptance_Date]) as precurment FROM  [MPSCSCSVR].[mpscsc].[dbo].[Acceptance_Note_Detail] AS A WHERE  A.IssueCenter_ID=TMD.DepotID and A.TC_Number not in (select Challan_No froM tbl_Storage_Arrival_Stock as AST where AST.Depotid=a.IssueCenter_ID and challan_no is not null)) as proccount,(SELECT count(challan_no) FROM MPSCSCSVR.MPSCSC.dbo.tbl_Receipt_Details as RD  join MPSCSCSVR.MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where  RD.Depot_ID= tmd.DepotID and sa.Source_ID = '02' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.Depotid=rd.Depot_ID and challan_no is not null AND AST.Source_of_Arrival ='02')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.Depotid=rd.Depot_ID AND AST.Source_of_Arrival ='02' and RD.Vehile_no!=AST.Truck_No)) as od2,(SELECT count(challan_no) FROM MPSCSCSVR.MPSCSC.dbo.tbl_Receipt_Details as RD join MPSCSCSVR.MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Depot_ID=TMD.DepotID and sa.Source_ID = '04' and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where  RC.Depotid=RD.Depot_ID and  AST.Source_of_Arrival ='04' and challan_no is not null and RD.Vehile_no!=AST.Truck_No)) as od2,(SELECT count(challan_no) FROM MPSCSCSVR.MPSCSC.dbo.tbl_Receipt_Details as RD join MPSCSCSVR.MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where  RD.Depot_ID= TMD.DepotID AND RD.S_of_arrival = '03' and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.Depotid=RD.Depot_ID and AST.Source_of_Arrival ='03' and challan_no is not null and RD.Vehile_no!=AST.Truck_No)) as od3,(SELECT count(TC_Number) as 'challan_no' FROM MPSCSCSVR.MPSCSC.dbo.RR_receipt_Depot as RD where RD.DepotID=TMD.DepotID AND RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.Depotid=rd.DepotID AND AST.Source_of_Arrival ='07' and challan_no is not null and RD.Truck_No!=AST.Truck_No)) as od4 FROM            tbl_MetaData_DISTRICT AS D INNER JOIN tbl_MetaData_DEPOT AS TMD ON D.District_Id = TMD.DistrictId AND D.District_Id = TMD.DistrictId GROUP BY D.District_Id, D.District_Name, TMD.DepotID, TMD.DepotName ORDER BY D.District_Id";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvpendingdata.DataSource = ds;
                    gvpendingdata.DataBind();
                    
                   
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found!')", true);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

}