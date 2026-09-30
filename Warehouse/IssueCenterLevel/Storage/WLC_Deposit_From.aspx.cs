using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.Resources;

public partial class IssueCenterLevel_Storage_WLC_Deposit_From : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (Session["lang"].ToString() == "Hindi")
            {
                lblDepositDetail.Text = Resources.hindi.lblDepositDetail;
                lblDepositorType.Text = Resources.hindi.lblDepositorType;
                lblDepositorName.Text = Resources.hindi.lblDepositorName;
                lblSourceOfDeposit.Text = Resources.hindi.lblSourceOfDeposit;
            }
            if (!IsPostBack)
            {
                string script = "$(document).ready(function () { $('[id*=ddlArrival_Source]').click();  });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
                    
                if (RadioButton1.Checked)
                {
                    pnldate.Visible = false;
                }
                fillCropYear();
                fillDepositorType();
                ddldepositortype_SelectedIndexChanged(sender, e);
                FillArrivalSourceddl();
                fillProcNew();
                fillCommodity();
                FillGodown();
                //rbdate.Visible = false;
               
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillDepositorType()
    {
        try
        {
            string query = "select Depositor_Type from tbl_MetaData_Depositor_Type order by Report_Seq_Id";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldepositortype.DataSource = ds.Tables[0];
                ddldepositortype.DataTextField = "Depositor_Type";
                ddldepositortype.DataValueField = "Depositor_Type";
                ddldepositortype.DataBind();
                ddldepositortype.Items.Insert(0, "--Select--");
            }

            if (ddldepositortype.Items.Count > 0)
            {
                for (int d = 0; d < ddldepositortype.Items.Count; d++)
                {
                    if (ddldepositortype.Items[d].Text == "Institution")
                    {
                        ddldepositortype.Items[d].Selected = true;
                    }
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

    protected void FillArrivalSourceddl()
    {
        try
        {
            string Distid = Session["Depot_DistID"].ToString().Substring(2, 2);
            string query = "";
            if (ddlcropyear.SelectedItem.Text == "All")
            {
                query = "select Source_ID,Source_Name from  [MPSCSC].[dbo].[Source_Arrival_Type] AS B order by  Source_ID";  
                //local
                //query = "select Source_ID,Source_Name from [MPSCSC].[dbo].[Source_Arrival_Type] AS B order by  Source_ID";      
            }
            else
            {
                query = "select Source_ID,Source_Name from  [MPSCSC].[dbo].[Source_Arrival_Type] AS B order by  Source_ID";
                //local
                //query = "select Source_ID,Source_Name from [MPSCSC].[dbo].[Source_Arrival_Type] AS B order by  Source_ID";
            }
                
                SqlCommand cmd = new SqlCommand(query, con);
               // cmd.CommandTimeout = 90000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlArrival_Source.DataSource = ds.Tables[0];
                ddlArrival_Source.DataTextField = "Source_Name";
                ddlArrival_Source.DataValueField = "Source_ID";
                ddlArrival_Source.DataBind();
                ddlArrival_Source.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlArrival_Source.Items.Insert(0, "--Select--");
            }
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            
            throw new Exception(msg);
        }
    }

    protected void fillGridFCI_OTDepot()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            string query = "";
            string Depot = Session["Depot_DepotID"].ToString();
            string Dist = Session["Depot_DistID"].ToString();
            if (ddlArrival_Source.SelectedValue == "07")
            {
                query = " SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,convert(varchar(20),RD.arrival_date,103) as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM  MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.district_code=substring('" + Dist + "',3,2)  and RD.DepotID='" + Depot + "' AND RD.S_of_arrival = '07' and RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "' and RC.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='07' and challan_no is not null and RD.Truck_No!=AST.Truck_No)";
            }
            else if (ddlArrival_Source.SelectedValue == "03")
            {
                if (ddlcropyear.SelectedItem.Text == "All")
                {
                    query = " SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,convert(varchar(10),RD.arrival_date,103) as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name,rd.RO_No FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2) and RD.Depot_ID= '" + Depot + "' AND RD.S_of_arrival = '03' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "' and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='03' and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                }
                else
                {

                    query = "  SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,convert(varchar(10),RD.arrival_date,103) as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name,rd.RO_No FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2) and RD.Depot_ID= '" + Depot + "' AND RD.S_of_arrival = '03' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and  RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "' and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='03'  and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";

                }

            }
            else if (ddlArrival_Source.SelectedValue == "04")
            {
                if (ddlcropyear.SelectedItem.Text == "All")
                {
                    query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and sa.Source_ID = '04' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "' and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='04' and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                }
                else
                {
                    query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "'  and sa.Source_ID = '04' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "'  and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='04' and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                }
            }
            else if (ddlArrival_Source.SelectedValue == "05")
            {
                if (ddlcropyear.SelectedItem.Text == "2019-2020")
                {
                    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "'))  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details_2019 as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "'))  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details_2019 as RD with(Nolock) join tbl_MetaData_STORAGE_COMMODITY as cm with(Nolock) on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa with(Nolock) on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "'))  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";

                }
                else
                {
                    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "'  and sa.Source_ID = '04' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "'  and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='04' and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "'  and sa.Source_ID = '05' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "'  and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='05' and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";

                }
            }
            else if (ddlArrival_Source.SelectedValue == "10")
            {
                if (ddlcropyear.SelectedItem.Text == "All")
                {
                    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,null as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where  RD.Depot_ID= '" + Depot + "' and sa.Source_ID = '02' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='02')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='02' and RD.Vehile_no!=AST.Truck_No)";
                    query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where  RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                }
                else
                {

                    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSCSVR.MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSCSVR.MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "'";
                    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSCSVR.MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSCSVR.MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year='" + ddlcropyear.SelectedValue + "') and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No and AST.Crop_Year='" + ddlcropyear.SelectedValue + "') order by RD.arrival_date desc";
                    query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Branch='" + Session["BranchId"].ToString() + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN_2018 where  tbl_MetaData_GODOWN_2018.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.BranchID='" + Session["BranchId"].ToString() + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year='" + ddlcropyear.SelectedValue + "') and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.BranchID='" + Session["BranchId"].ToString() + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No and AST.Crop_Year='" + ddlcropyear.SelectedValue + "') order by RD.arrival_date desc";

                }
            }
            else
            {
                if (ddlcropyear.SelectedItem.Text == "All")
                {
                    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,null as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where  RD.Depot_ID= '" + Depot + "' and sa.Source_ID = '02' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='02')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='02' and RD.Vehile_no!=AST.Truck_No)";
                    query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where  RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                }
                else if (ddlcropyear.SelectedItem.Text == "2016-2017")
                {
                    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSCSVR.MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSCSVR.MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where (RD.Crop_year is null or RD.Crop_year='' or RD.Crop_year='2016-2017') and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSCSVR.MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSCSVR.MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='2016-2017' and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='2016-2017' and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "'))  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                }                
                else
                {

                    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,null as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='"+ddlcropyear.SelectedItem.Text+"' and RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID= '" + Depot + "' and sa.Source_ID = '02' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='02')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='02' and RD.Vehile_no!=AST.Truck_No)";
                    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSCSVR.MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSCSVR.MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    // query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN_2018 where  tbl_MetaData_GODOWN_2018.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "'))  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    // cHANGE Depot ID to Branch ID
                    if (Session["BranchId"].ToString() == "2349002" && ddlcropyear.SelectedItem.Text == "2021-2022")
                    {
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Branch= '" + Session["BranchId"].ToString() + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN_2018 where  tbl_MetaData_GODOWN_2018.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "') and RD.Vehile_no=AST.Truck_No) order by RD.arrival_date desc";
                    }
                    else
                    {
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Branch= '" + Session["BranchId"].ToString() + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN_2018 where  tbl_MetaData_GODOWN_2018.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "') and RD.Vehile_no=AST.Truck_No and RD.Recd_Qty=AST.Qty_Wt)  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    }
                        

                    //if (Session["BranchId"].ToString() == "2334002")
                    //{
                    //    query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='2020-2021' and RD.Depot_ID= '2334002' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID='2334002') and sa.Source_ID ='02' and A_dist='18'";
                    //}
                    //else
                    //{
                    //    query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN_2018 where  tbl_MetaData_GODOWN_2018.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "'))  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";

                    //}
                }
                //else
                //{

                //    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,null as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='"+ddlcropyear.SelectedItem.Text+"' and RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID= '" + Depot + "' and sa.Source_ID = '02' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='02')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='02' and RD.Vehile_no!=AST.Truck_No)";
                //    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSCSVR.MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSCSVR.MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                //    if (Session["BranchId"].ToString() == "231300406")
                //    {
                //        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='2021-2022' and RD.Depot_ID= '231300406' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID='231300406') and sa.Source_ID ='02' and RD.Dist_Id='13'";
                //    }
                //    else
                //    {
                //        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN_2018 where  tbl_MetaData_GODOWN_2018.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "'))  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";

                //    }
                //}
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GvuFromFCI_OTHDepot.DataSource = ds;
                GvuFromFCI_OTHDepot.DataBind();
                lblMsg.Text = "";
                lblMsg.Visible = false;
                trfromothdepot.Visible = true;
                tr_Disfromprc.Visible = false;
                trfromrailhead.Visible = false;
            }
            else
            {
                lblMsg.Text = "No pending movement challan for this source of arrival for the depositor MPSCSC";
                lblMsg.Visible = true;
                GvuFromFCI_OTHDepot.DataSource = null;
                GvuFromFCI_OTHDepot.DataBind();
                trfromothdepot.Visible = false;
                tr_Disfromprc.Visible = false;
                trfromrailhead.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillGridRailHead()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                string Distid = Session["Depot_DistID"].ToString().Substring(2, 2);
                String Districtid = Session["Depot_DistID"].ToString();
                string query = "";
                if (ddlcropyear.SelectedItem.Text == "All")
                {
                    query = " SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,null as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.DepotID= '" + Session["Depot_DepotID"].ToString() + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Districtid + "' and RC.Depotid= '" + Session["Depot_DepotID"].ToString() + "' and challan_no is not null and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "')";
                     //query = " SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,null as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.district_code= '" + Distid + "' and RD.DepotID= '" + Session["Depot_DepotID"].ToString() + "' and RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Districtid + "' and RC.Depotid= '" + Session["Depot_DepotID"].ToString() + "' and challan_no is not null and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "') and RD.Truck_No NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Districtid + "' and AST.Depotid= '" + Session["Depot_DepotID"].ToString() + "' and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "')";
                }
                else if (ddlcropyear.SelectedItem.Text == "2021-2022")
                {
                    query = "SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,null as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.DepotID= '" + Session["Depot_DepotID"].ToString() + "' and RD.Cropyear='" + ddlcropyear.SelectedItem.Text + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and RD.TC_Number not in (select AST.Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Districtid + "' and AST.Depotid= '" + Session["Depot_DepotID"].ToString() + "' and AST.Challan_No is not null and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "' AND Crop_Year in('2021-22','2021-2022'))";
                }
                else
                {
                    // query = " SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,null as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.district_code= '" + Distid + "' and RD.DepotID= '" + Session["Depot_DepotID"].ToString() + "' and RD.Cropyear='"+ddlcropyear.SelectedItem.Text+"' and RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Districtid + "' and RC.Depotid= '" + Session["Depot_DepotID"].ToString() + "' and challan_no is not null and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "') and RD.Truck_No NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Districtid + "' and AST.Depotid= '" + Session["Depot_DepotID"].ToString() + "' and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "')";
                    //query = " SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,null as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSCSVR.MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.DepotID= '" + Session["Depot_DepotID"].ToString() + "' and RD.Cropyear='" + ddlcropyear.SelectedItem.Text + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Districtid + "' and RC.Depotid= '" + Session["Depot_DepotID"].ToString() + "' and challan_no is not null and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "')";
                    //query = "SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,null as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.DepotID= '" + Session["Depot_DepotID"].ToString() + "' and RD.Cropyear='" + ddlcropyear.SelectedItem.Text + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and RD.TC_Number not in (select AST.Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Districtid + "' and AST.Depotid= '" + Session["Depot_DepotID"].ToString() + "' and AST.Challan_No is not null and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "')";
                    query = "SELECT district_code as 'Dist_Id',BranchID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,null as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.RR_receipt_Depot as RD INNER JOIN dbo.tbl_MetaData_GODOWN gdn ON RD.Godown=gdn.Godown_ID join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where gdn.BranchID= '" + Session["BranchId"].ToString() + "' and RD.Cropyear='" + ddlcropyear.SelectedItem.Text + "' and RD.TC_Number not in (select AST.Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Districtid + "' and AST.BranchID= '" + Session["BranchId"].ToString() + "' and AST.Challan_No is not null and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "')";
               
                }
                    SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvuFrom_RailHead.DataSource = ds;
                    gvuFrom_RailHead.DataBind();
                    lblMsg.Text = "";
                    lblMsg.Visible = false;
                    trfromrailhead.Visible = true;
                    trfromothdepot.Visible = false;
                    tr_Disfromprc.Visible = false;
                }
                else
                {
                    lblMsg.Text = "No pending movement challan for this source of arrival for the depositor MPSCSC";
                    lblMsg.Visible = true;
                    gvuFrom_RailHead.DataSource = null;
                    gvuFrom_RailHead.DataBind();
                    trfromrailhead.Visible = false;
                    trfromothdepot.Visible = false;
                    tr_Disfromprc.Visible = false;
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

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void fillGridProc()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                if (RadioButton1.Checked)
                {
                    if (ddlcropyear.SelectedItem.Text == "All")
                    {
                        cmd = new SqlCommand("MPWLC_Select_PendingTC_from_Proc_New", con);

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Dist_Id", Session["Depot_DistID"].ToString());
                        cmd.Parameters.AddWithValue("@Depot_ID", Session["Depot_DepotID"].ToString());
                        cmd.Parameters.AddWithValue("@Purchase_Center", ddl_society.SelectedValue);
                    }

                    else
                    {
                        cmd = new SqlCommand("MPWLC_Select_PendingTC_from_Proc_CropYealy", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Dist_Id", Session["Depot_DistID"].ToString());
                        cmd.Parameters.AddWithValue("@Depot_ID", Session["Depot_DepotID"].ToString());
                        cmd.Parameters.AddWithValue("@Purchase_Center", ddl_society.SelectedValue);
                        cmd.Parameters.AddWithValue("@CropYear", ddlcropyear.SelectedItem.Text);
                    }
                }
                if (RadioButton2.Checked)
                {
                   
                    cmd = new SqlCommand("MPWLC_Select_PendingTC_from_Proc_BWDates", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Dist_Id", Session["Depot_DistID"].ToString());
                    cmd.Parameters.AddWithValue("@Depot_ID", Session["Depot_DepotID"].ToString());
                    cmd.Parameters.AddWithValue("@Purchase_Center", ddl_society.SelectedValue);

                    cmd.Parameters.AddWithValue("@Date1", getDate_MDY(txtdatefrom.Text.Trim().ToString()));
                    cmd.Parameters.AddWithValue("@Date2", getDate_MDY(txtdateto.Text.Trim().ToString()));
                }
               
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvuDispatchFromPC.DataSource = ds;
                    GvuDispatchFromPC.DataBind();
                    lblMsg.Text = "";
                    lblMsg.Visible = false;
                    tr_Disfromprc.Visible = true;
                    trfromrailhead.Visible = false;
                    trfromothdepot.Visible = false;
                }
                else
                {
                    lblMsg.Text = "No pending movement challan for this source of arrival for the depositor MPSCSC";
                    lblMsg.Visible = true;
                    GvuDispatchFromPC.DataSource = null;
                    GvuDispatchFromPC.DataBind();
                    tr_Disfromprc.Visible = false;
                    trfromrailhead.Visible = false;
                    trfromothdepot.Visible = false;
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

    protected void fillGridProcCropyearly()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("MPWLC_Select_PendingTC_from_Proc_CropYealy", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Dist_Id", Session["Depot_DistID"].ToString());
                cmd.Parameters.AddWithValue("@Depot_ID", Session["Depot_DepotID"].ToString());
                cmd.Parameters.AddWithValue("@Purchase_Center", ddl_society.SelectedValue);
                cmd.Parameters.AddWithValue("@CropYear", ddlcropyear.SelectedItem.Text);
                // cmd.Parameters.AddWithValue("@Acceptance_Date", getDate_MDY(txt_SAD.Text.Trim().ToString()));
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvuDispatchFromPC.DataSource = ds;
                    GvuDispatchFromPC.DataBind();
                    lblMsg.Text = "";
                    lblMsg.Visible = false;
                    tr_Disfromprc.Visible = true;
                    trfromrailhead.Visible = false;
                    trfromothdepot.Visible = false;
                }
                else
                {
                    lblMsg.Text = "No pending movement challan for this source of arrival for the depositor MPSCSC";
                    lblMsg.Visible = true;
                    GvuDispatchFromPC.DataSource = null;
                    GvuDispatchFromPC.DataBind();
                    tr_Disfromprc.Visible = false;
                    trfromrailhead.Visible = false;
                    trfromothdepot.Visible = false;
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

    protected void GvuFromFCI_OTHDepot_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditFCI_OTHDepot")
            {
                try
                {
                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    String Receiptid = Convert.ToString(GvuFromFCI_OTHDepot.DataKeys[row.RowIndex].Value);
                    string ChallanNo = Convert.ToString(GvuFromFCI_OTHDepot.Rows[row.RowIndex].Cells[0].Text.ToString());
                    string RO_date = Convert.ToString(GvuFromFCI_OTHDepot.Rows[row.RowIndex].Cells[3].Text.ToString());
                    //gdtruckdetail.Rows[i].Cells[12].Text.ToString());

                    Session["WLC_Dep_Receipt_id"] = Receiptid.ToString();
                    Session["WLC_Dep_Dist_Id"] = Session["Depot_DistID"].ToString();
                    Session["WLC_Dep_Depot_ID"] = Session["Depot_DepotID"].ToString();
                    Session["Mode"] = "Add";
                    Session["WLCDepSource"] = ddlArrival_Source.SelectedValue;
                    Session["ChallanNo"] = ChallanNo.Trim();
                    Session["RO_date"] = RO_date.ToString();
                    //Session["CropYear"] = ddlcropyear.SelectedValue.ToString();
                    Response.Redirect("~/IssueCenterLevel/Storage/WLC_FRM_01_02_03_Receipt.aspx");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    string msg = "Insert Error:";
                    msg += ex.Message;
                    throw new Exception(msg);
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

    protected void lnkDepositProc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkProc = (LinkButton)sender;
            string[] CommandArgument = lnkProc.CommandArgument.Split(',');
            Session["WLC_Distt_ID"] = CommandArgument[0];
            Session["WLC_IssueCenter_ID"] = CommandArgument[1];
            Session["WLC_TC_Number"] = CommandArgument[2];
            Session["Acceptance No"] = CommandArgument[3];
            Session["SocietyCode"] = ddl_society.SelectedValue;
            Session["Mode"] = "Add";
            Session["WLCDepSource"] = ddlArrival_Source.SelectedValue;
            Session["IssueId"] = CommandArgument[4];
            Session["TruckNo"] = CommandArgument[5];
            Session["whrreq"] = "OldProc";
            Session["ProcComm"] = ddlProcCmd.SelectedValue.ToString();
            // Response.Redirect("WLC_FRM_01_02_03_Receipt.aspx");
            Response.Redirect("~/BranchPages/Receiptfrom_Procurement.aspx");
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }

    }
    //new procurment 27/03/15

    protected void lnkDepositProcNew_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkProc = (LinkButton)sender;
            string[] CommandArgument = lnkProc.CommandArgument.Split(',');
           
            Session["whrreq"] = CommandArgument[0];
            Session["AccepDate"] = CommandArgument[1];
            string acc = Session["AccepDate"].ToString();
            Session["Mode"] = "Add";
            Session["WLCDepSource"] = "01";
            Session["ProcComm"] = ddlProcCmd.SelectedValue.ToString();
            //from godown owner
           // Session["RecType"] = ddlProcCmd.SelectedItem.Text;
            //Session["WLCDepSource"] = ddlArrival_Source.SelectedValue;
            // Response.Redirect("WLC_FRM_01_02_03_Receipt.aspx");
            Response.Redirect("~/BranchPages/Receiptfrom_Procurement.aspx");
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }

    }
    //27/03/15
    protected void gdnewproc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int indx = e.NewPageIndex;
            gdnewproc.PageIndex = e.NewPageIndex;
            fillProcNew();
        }
        catch (Exception ex)
        {
            StringBuilder str1 = new StringBuilder();
            str1.Append("<script>");
            str1.Append("alert('" + "Some error has occured, try again" + "," + "Error:-" + ex.Message + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str1.ToString());
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
                if (RadioButton1.Checked)
                {
                    if (ddlcropyear.SelectedItem.Text == "All")
                    {
                        cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.whrreq1 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                        //local
                        //cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.whrreq1 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                    }
                    else if (ddlcropyear.SelectedItem.Text == "2015-2016")
                    {
                        cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.whrreq1 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                        //local
                        //cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.whrreq1 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                    }
                    else if (ddlcropyear.SelectedItem.Text == "2016-2017")
                    {
                        if (ddlProcCmd.SelectedItem.Text == "Paddy-Common")
                        {
                            cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2016 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                            //local
                            //cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2016 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                        }
                        else if (ddlProcCmd.SelectedItem.Text == "Paddy-Grade-A")
                        {
                            cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2016 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                        }
                        else if (ddlProcCmd.SelectedItem.Text == "Bajra")
                        {
                            cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2016 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                        }
                        else if (ddlProcCmd.SelectedItem.Text == "Jau")
                        {
                            cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2016 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                        }
                        else if (ddlProcCmd.SelectedItem.Text == "Jowar")
                        {
                            cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2016 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                        }
                        else if (ddlProcCmd.SelectedItem.Text == "Maize(Makka)")
                        {
                            cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2016 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                        }
                        else if (ddlProcCmd.SelectedItem.Text == "Wheat-PSS")
                        {
                            cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DeposioterFormDtl as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                            //local
                            //cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DeposioterFormDtl as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                        }
                    }
                        //Wheat 2017-18
                    else if (ddlcropyear.SelectedItem.Text == "2017-2018")
                    {
                        if (ddlProcCmd.SelectedItem.Text == "Onion")
                        {
                            //cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormWheat2017 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                            cmd = new SqlCommand("select CONVERT(varchar(10),DepositerDate,103) as Acceptance_Date,'' as IssueCenter_ID,sum(Recd_Bags) as Recd_Bags,sum(Recd_Qty) as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.branch and Godown_ID=whrr.godown)as Godown ,DepositerNumber as WHR_Request from mpscsc.dbo.Onion_Depositer as whrr where branch='" + Session["BranchId"].ToString() + "' and whrr.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.DepositerNumber not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.DepositerNumber and sss.IssueID='NA') and DepositerNumber is not null group by DepositerNumber,DepositerDate,whrr.Commodity,whrr.branch,whrr.godown order by DepositerDate", con);
                        }
                        else if (ddlProcCmd.SelectedItem.Text == "Moong")
                        {
                            cmd = new SqlCommand("select distinct CONVERT(varchar(10),Acceptance_Date,103) as Acceptance_Date,IssueCenter_ID,sum(RecievedBags) as Recd_Bags,sum(Accept_Qty) as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.CommodityId) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.godown)as Godown ,WHR_Request from mpscsc.dbo.Pulse_Acceptance_Detail as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') and WHR_Request is not null and whrr.CommodityId='92' group by WHR_Request,Acceptance_Date,whrr.CommodityId,whrr.Branch_Id,whrr.godown,IssueCenter_ID order by Acceptance_Date", con);
                        }
                        else if (ddlProcCmd.SelectedItem.Text == "Arahar")
                        {
                            cmd = new SqlCommand("select distinct CONVERT(varchar(10),Acceptance_Date,103) as Acceptance_Date,IssueCenter_ID,sum(RecievedBags) as Recd_Bags,sum(Accept_Qty) as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.CommodityId) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.godown)as Godown ,WHR_Request from mpscsc.dbo.Pulse_Acceptance_Detail as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') and WHR_Request is not null and whrr.CommodityId='52' group by WHR_Request,Acceptance_Date,whrr.CommodityId,whrr.Branch_Id,whrr.godown,IssueCenter_ID order by Acceptance_Date", con);
                        }
                        else if (ddlProcCmd.SelectedItem.Text == "Urad")
                        {
                            cmd = new SqlCommand("select distinct CONVERT(varchar(10),Acceptance_Date,103) as Acceptance_Date,IssueCenter_ID,sum(RecievedBags) as Recd_Bags,sum(Accept_Qty) as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.CommodityId) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.godown)as Godown ,WHR_Request from mpscsc.dbo.Pulse_Acceptance_Detail as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') and WHR_Request is not null and whrr.CommodityId='27' group by WHR_Request,Acceptance_Date,whrr.CommodityId,whrr.Branch_Id,whrr.godown,IssueCenter_ID order by Acceptance_Date", con);
                        }
                        else if (ddlProcCmd.SelectedItem.Text == "Wheat-PSS")
                        {
                            //wheat
                            //cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from MPSCSCSVR.mpscsc.dbo.DeposioterFormDtl as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                            cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormWheat2017 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                        }
                            //Kharif 2017
                        else if (ddlProcCmd.SelectedItem.Text == "Jowar" || ddlProcCmd.SelectedItem.Text == "Bajra" || ddlProcCmd.SelectedItem.Text == "Paddy-Grade-A")
                        {
                            //Jowar and Bajra 2017
                            //cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2017 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                              cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2017 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);

                        }
                        else
                        {
                            //Paddy 2017
                            cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2017 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.Commodity_Id='13' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                        }
                    }
                    //Wheat 2018-19
                    else if (ddlcropyear.SelectedItem.Text == "2018-2019")
                    {
                        if (ddlProcCmd.SelectedItem.Text == "Wheat-PSS")
                        {
                            //wheat
                            //cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormWheat2017 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                            cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormWheat2018 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);

                        }
                        else if ((ddlProcCmd.SelectedItem.Text == "GRAM" || ddlProcCmd.SelectedItem.Text == "Mustard-Sarason" || ddlProcCmd.SelectedItem.Text == "LENTIL") && ddlDepositor.SelectedValue.ToString() == "10535")
                        {
                            //wheat
                            //cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormWheat2017 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                            cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormCSM2018 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);

                        }       
                    }
                    cmd.CommandType = CommandType.Text;
                }
                da.SelectCommand = cmd;
                if (cmd.CommandText != null && cmd.CommandText != "")
                {
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gdnewproc.DataSource = ds;
                        gdnewproc.DataBind();
                        lblMsg.Text = "";
                        lblMsg.Visible = false;
                        tr_Disfromprc.Visible = true;
                        trfromrailhead.Visible = false;
                        trfromothdepot.Visible = false;
                        trnewproc.Visible = true;
                    }
                    else
                    {
                        lblMsg.Text = "No pending movement challan for this source of arrival for the depositor MPSCSC";
                        lblMsg.Visible = true;
                        GvuDispatchFromPC.DataSource = null;
                        GvuDispatchFromPC.DataBind();
                        tr_Disfromprc.Visible = false;
                        trfromrailhead.Visible = false;
                        trfromothdepot.Visible = false;
                        trnewproc.Visible = false;
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data found...'); </script> ");
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

    protected void GvuFromFCI_OTHDepot_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int indx = e.NewPageIndex;
            GvuFromFCI_OTHDepot.PageIndex = e.NewPageIndex;
            fillGridFCI_OTDepot();
        }
        catch (Exception ex)
        {
            StringBuilder str1 = new StringBuilder();
            str1.Append("<script>");
            str1.Append("alert('" + "Some error has occured, try again" + "," + "Error:-" + ex.Message + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str1.ToString());
        }
    }

    protected void GvuDispatchFromPC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int indx = e.NewPageIndex;
            GvuDispatchFromPC.PageIndex = e.NewPageIndex;
            fillGridProc();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
    }

    protected void ddlArrival_Source_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["WLCDepSource"] = ddlArrival_Source.SelectedValue;
        if (ddlArrival_Source.Items.Count > 0)
        {
            if (ddlArrival_Source.SelectedValue == "01" || ddlArrival_Source.SelectedItem.Text == "Procurement")
            {
                //Change this line to true if want to show truck wise receiving..
                Show_soc.Visible = false;
                trfromothdepot.Visible = false;
                tr_Disfromprc.Visible = false;
                ddl_society.DataSource = null;
                ddl_society.DataBind();
                GetSociety();
                rbdate.Visible = false;
                rbchallan.Checked = true;
            }
            else if (ddlArrival_Source.SelectedValue == "07")
            {
                Show_soc.Visible = false;
                fillGridRailHead();
                rbdate.Visible = false;
                rbchallan.Checked = true;
            }
            else
            {
                Show_soc.Visible = false;
                fillGridFCI_OTDepot();
                rbdate.Visible = true;
            }
        }
    }

    protected void lnkRailHead_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkRHead = (LinkButton)sender;

            string[] CommandArgument = lnkRHead.CommandArgument.Split(',');

            Session["WLC_Distt_ID"] = CommandArgument[0];

            Session["WLC_Depot_ID"] = CommandArgument[1];

            Session["WLC_TC_Number"] = CommandArgument[2];

            Session["Mode"] = "Add";

            Session["WLCDepSource"] = ddlArrival_Source.SelectedValue;

            Response.Redirect("WLC_FRM_01_02_03_Receipt.aspx");
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
    }

    protected void gvuFrom_RailHead_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int indx = e.NewPageIndex;
            gvuFrom_RailHead.PageIndex = e.NewPageIndex;
            fillGridRailHead();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
    }

    protected void ddldepositortype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                ddlDepositor.Items.Clear();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                DataSet ds1 = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                
                cmd.Connection = con;
                cmd = new SqlCommand("sp_getDepositor_Depo_wise", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Depositor_Type", SqlDbType.VarChar, 20);
                cmd.Parameters["@Depositor_Type"].Value = ddldepositortype.SelectedValue.ToString().Trim();
                cmd.Parameters.Add("@depot_id", SqlDbType.VarChar, 20);
                cmd.Parameters["@depot_id"].Value = Session["BranchId"].ToString();
                int Res = cmd.ExecuteNonQuery();
                da.SelectCommand = cmd;
                da.Fill(ds1, "temp");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if (ddldepositortype.SelectedItem.Text == "Institution")
                    {
                        //For Institution
                        string query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','181','184','4679','10535','15478')";
                        SqlCommand cmd2 = new SqlCommand(query2, con);
                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2);
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            ddlDepositor.DataSource = ds2;
                            ddlDepositor.DataTextField = "Depositor_Name";
                            ddlDepositor.DataValueField = "Depositor_ID";
                            ddlDepositor.DataBind();
                            ddlDepositor.Items.Insert(0, "--Select--");
                            ddlDepositor.SelectedValue = "129";
                        }
                        //For Institution
                       
                    }
                    else
                    {
                        ddlDepositor.DataSource = ds1;
                        ddlDepositor.DataTextField = "Depositor_Name";
                        ddlDepositor.DataValueField = "Depositor_ID";
                        ddlDepositor.DataBind();
                        ddlDepositor.Items.Insert(0, "--Select--");
                       // ddlDepositor.SelectedValue = "129";

                }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Depositor Found!')", true);

                    //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No Depositor Found!'); </script> ");
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
                con.Close();
            }
        }
        else
        {
            Response.Redirect("../../Logout.aspx");
        }
    }

    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepositor.Items.Count > 0)
            {
                if (ddlDepositor.SelectedItem.Text.Trim().ToUpper() == "MPSCSC")
                {
                    Session["depositortypeA"] = ddldepositortype.SelectedItem.Text;
                    Session["DepositorA"] = ddlDepositor.SelectedItem.Text;
                    FillArrivalSourceddl();
                   
                }
                else if (ddlDepositor.SelectedItem.Text == "DMO Markfed")
                {
                    Session["depositortypeA"] = ddldepositortype.SelectedItem.Text;
                    Session["DepositorA"] = ddlDepositor.SelectedItem.Text;
                    Session["WLCDepSource"] = "NON-MPSCSC";
                    Response.Redirect("WLC_FRM_01_02_03_Receipt.aspx");

                }
                else if (ddlDepositor.SelectedItem.Text == "NCCF")
                {
                    Session["depositortypeA"] = ddldepositortype.SelectedItem.Text;
                    Session["DepositorA"] = ddlDepositor.SelectedItem.Text;
                    Session["WLCDepSource"] = "NON-MPSCSC";
                    Response.Redirect("WLC_FRM_01_02_03_Receipt.aspx");

                }
                else if (ddlDepositor.SelectedItem.Text == "NAFED")
                {
                    Session["depositortypeA"] = ddldepositortype.SelectedItem.Text;
                    Session["DepositorA"] = ddlDepositor.SelectedItem.Text;
                    Session["WLCDepSource"] = "NON-MPSCSC";
                    Response.Redirect("WLC_FRM_01_02_03_Receipt.aspx");

                }
                else if (ddldepositortype.SelectedItem.Text != "Institution")
                {
                    Session["depositortypeA"] = ddldepositortype.SelectedItem.Text;
                    Session["DepositorA"] = ddlDepositor.SelectedItem.Text;
                    Session["WLCDepSource"] = "NON-MPSCSC";
                    Response.Redirect("WLC_FRM_01_02_03_Receipt.aspx");

                }
                else
                {
                    GvuDispatchFromPC.DataSource = null;
                    GvuDispatchFromPC.DataBind();
                    gvuFrom_RailHead.DataSource = null;
                    gvuFrom_RailHead.DataBind();
                    GvuFromFCI_OTHDepot.DataSource = null;
                    GvuFromFCI_OTHDepot.DataBind();
                    ddlArrival_Source.DataSource = "";
                    ddlArrival_Source.DataBind();
                    ddlArrival_Source.Items.Insert(0, "--Select--");
                    ddl_society.DataSource = "";
                    ddl_society.DataBind();


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

    protected void ddl_society_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["WLCDepSource"] = ddlArrival_Source.SelectedValue;
        Session["SocietyCode"] = ddl_society.SelectedValue;
        if (ddlArrival_Source.Items.Count > 0)
        {
           
                //if (ddlArrival_Source.SelectedValue == "01" || ddlArrival_Source.SelectedItem.Text == "Procurement" )
                //{
                //    fillGridProcCropyearly();

                //}
                if (ddlArrival_Source.SelectedValue == "01" || ddlArrival_Source.SelectedItem.Text == "Procurement")
                {
                    fillGridProc();

                }
                else if (ddlArrival_Source.SelectedValue == "07")
                {
                    fillGridRailHead();
                }
                else
                {
                    fillGridFCI_OTDepot();
                }
           
        }
    }

    public void GetSociety()
    {
        //try
        //{
            string distcode = Session["Depot_DistID"].ToString();
            string cetId = distcode.Substring(distcode.Length - 2);
            string str="";
            if (RadioButton1.Checked)
            {
                if (ddlcropyear.SelectedItem.Text == "All")
                {
                     str = "SELECT [Society_Id],(('('+[Society_Id]+')'+'/'+[Society_Name]+' / '+[SocPlace])+'  ( '+CAST((SELECT COUNT(Acceptance_Date) FROM [mpscsc].[dbo].[Acceptance_Note_Detail] AS A WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["Depot_DepotID"] + "' and  A.Purchase_Center=S.Society_Id AND A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss join tbl_Storage_Receipt_Details as TTT on sss.Receipt_ID=TTT.StorageReceipt_Id where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.DepotId='" + Session["Depot_DepotID"] + "' and sss.IssueID is not null) AND  A.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join  tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id  where RC.District_Id='" + Session["Depot_DistID"] + "' and RC.Depotid='" + Session["Depot_DepotID"] + "' and ast.IssueID is null and challan_no is not null)) AS VARCHAR(10)) +' )') AS Society_Name,((SELECT COUNT(Acceptance_Date) FROM [mpscsc].[dbo].[Acceptance_Note_Detail] AS A WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["Depot_DepotID"] + "' and  A.Purchase_Center=S.Society_Id AND A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss join tbl_Storage_Receipt_Details as TTT on sss.Receipt_ID=TTT.StorageReceipt_Id where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.DepotId='" + Session["Depot_DepotID"] + "' and sss.IssueID is not null) AND  A.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join  tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id  where RC.District_Id='" + Session["Depot_DistID"] + "' and RC.Depotid='" + Session["Depot_DepotID"] + "' and ast.IssueID is null and challan_no is not null))) AS Count FROM [mpscsc].[dbo].[Society] AS S where Society_Id in (SELECT [Purchase_Center] FROM [mpscsc].[dbo].[Acceptance_Note_Detail] where Distt_ID='" + cetId + "')";
                    //str = "SELECT [Society_Id],(('('+[Society_Id]+')'+'/'+[Society_Name]+' / '+[SocPlace])+'  ( '+CAST((SELECT COUNT(Acceptance_Date) FROM [mpscsc].[dbo].[Acceptance_Note_Detail] AS A WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["Depot_DepotID"] + "' and  A.Purchase_Center=S.Society_Id AND A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss join tbl_Storage_Receipt_Details as TTT on sss.Receipt_ID=TTT.StorageReceipt_Id where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.DepotId='" + Session["Depot_DepotID"] + "' and sss.IssueID is not null) AND  A.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join  tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id  where RC.District_Id='" + Session["Depot_DistID"] + "' and RC.Depotid='" + Session["Depot_DepotID"] + "' and ast.IssueID is null and challan_no is not null)) AS VARCHAR(10)) +' )') AS Society_Name,((SELECT COUNT(Acceptance_Date) FROM [mpscsc].[dbo].[Acceptance_Note_Detail] AS A WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["Depot_DepotID"] + "' and  A.Purchase_Center=S.Society_Id AND A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss join tbl_Storage_Receipt_Details as TTT on sss.Receipt_ID=TTT.StorageReceipt_Id where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.DepotId='" + Session["Depot_DepotID"] + "' and sss.IssueID is not null) AND  A.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join  tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id  where RC.District_Id='" + Session["Depot_DistID"] + "' and RC.Depotid='" + Session["Depot_DepotID"] + "' and ast.IssueID is null and challan_no is not null))) AS Count FROM [mpscsc].[dbo].[Society] AS S where Society_Id in (SELECT [Purchase_Center] FROM [mpscsc].[dbo].[Acceptance_Note_Detail] where Distt_ID='" + cetId + "')";
                
                }
                if (ddlcropyear.SelectedItem.Text != "All")
                {
                   // str = " select Society_Id, (('('+[Society_Id]+')'+'/'+[Society_Name]+' / '+[SocPlace])+ '('+cast((select count(a.Acceptance_No) from MPSCSCSVR.[mpscsc].[dbo].[Acceptance_Note_Detail] AS A  WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["Depot_DepotID"] + "' and a.Acceptance_No in(select Acceptance_No from MPSCSCSVR.mpscsc.dbo.SCSC_Procurement as sp  where sp.Crop_Year='" + ddlcropyear.SelectedItem.Text + "' and sp.IssueCenter_ID='" + Session["Depot_DepotID"] + "') and  A.Purchase_Center=S.Society_Id and A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss join tbl_Storage_Receipt_Details as TTT on sss.Receipt_ID=TTT.StorageReceipt_Id where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.DepotId='" + Session["Depot_DepotID"] + "' and sss.IssueID is not null) AND  A.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join  tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id  where RC.District_Id='" + Session["Depot_DistID"] + "' and RC.Depotid='" + Session["Depot_DepotID"] + "' and ast.IssueID is null and challan_no is not null))AS VARCHAR(10)) +' )') as Society_Name,(select count(a.Acceptance_No) from MPSCSCSVR.[mpscsc].[dbo].[Acceptance_Note_Detail] AS A  WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["Depot_DepotID"] + "' and a.Acceptance_No in(select Acceptance_No from MPSCSCSVR.mpscsc.dbo.SCSC_Procurement as sp  where sp.Crop_Year='" + ddlcropyear.SelectedItem.Text + "' and sp.IssueCenter_ID='" + Session["Depot_DepotID"] + "') and  A.Purchase_Center=S.Society_Id and A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss join tbl_Storage_Receipt_Details as TTT on sss.Receipt_ID=TTT.StorageReceipt_Id where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.DepotId='" + Session["Depot_DepotID"] + "' and sss.IssueID is not null) AND  A.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join  tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id  where RC.District_Id='" + Session["Depot_DistID"] + "' and RC.Depotid='" + Session["Depot_DepotID"] + "' and ast.IssueID is null and challan_no is not null)) as Count from MPSCSCSVR.[mpscsc].[dbo].[Society] AS S where Society_Id in (SELECT Distinct [Purchase_Center] FROM MPSCSCSVR.[mpscsc].[dbo].[Acceptance_Note_Detail] where Distt_ID='" + cetId + "') order by Society_Id ";
                    str = "select Distinct Society_Id, (('('+ [Society_Id]+')'+'/'+[Society_Name]+' / '+[SocPlace])+'('+cast((select count(a.Acceptance_No) from [mpscsc].[dbo].[Acceptance_Note_Detail] AS A  WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["Depot_DepotID"] + "' and a.Acceptance_No in(select  Acceptance_No from mpscsc.dbo.SCSC_Procurement as sp  where sp.Crop_Year='" + ddlcropyear.SelectedItem.Text + "' and sp.IssueCenter_ID='" + Session["Depot_DepotID"] + "') and  A.Purchase_Center=S.Society_Id and A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.IssueID is not null))as varchar(10))) as Society_Name,(select count(a.Acceptance_No) from [mpscsc].[dbo].[Acceptance_Note_Detail] AS A  WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["Depot_DepotID"] + "' and a.Acceptance_No in(select  Acceptance_No from mpscsc.dbo.SCSC_Procurement as sp  where sp.Crop_Year='" + ddlcropyear.SelectedItem.Text + "' and sp.IssueCenter_ID='" + Session["Depot_DepotID"] + "') and  A.Purchase_Center=S.Society_Id and A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.IssueID is not null)) as Count from [mpscsc].[dbo].[Society] AS S where Society_Id in (SELECT Distinct [Purchase_Center] FROM [mpscsc].[dbo].[Acceptance_Note_Detail] where Distt_ID='" + cetId + "') order by Society_Id";
                }
            }
            if (RadioButton2.Checked)
            {
                if (txtdatefrom.Text != "" && txtdateto.Text != "")
                {
                    str = " select Society_Id, (('('+[Society_Id]+')'+'/'+[Society_Name]+' / '+[SocPlace])+ '('+cast((select count(a.Acceptance_No) from [mpscsc].[dbo].[Acceptance_Note_Detail] AS A  WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["Depot_DepotID"] + "' and a.Acceptance_No in(select Acceptance_No from mpscsc.dbo.SCSC_Procurement as sp  where sp.Acceptance_Date between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "' and sp.IssueCenter_ID='" + Session["Depot_DepotID"] + "') and  A.Purchase_Center=S.Society_Id and A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss join tbl_Storage_Receipt_Details as TTT on sss.Receipt_ID=TTT.StorageReceipt_Id where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.DepotId='" + Session["Depot_DepotID"] + "' and sss.IssueID is not null) AND  A.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join  tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id  where RC.District_Id='" + Session["Depot_DistID"] + "' and RC.Depotid='" + Session["Depot_DepotID"] + "' and ast.IssueID is null and challan_no is not null))AS VARCHAR(10)) +' )') as Society_Name,(select count(a.Acceptance_No) from [mpscsc].[dbo].[Acceptance_Note_Detail] AS A  WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["Depot_DepotID"] + "' and a.Acceptance_No in(select Acceptance_No from mpscsc.dbo.SCSC_Procurement as sp  where sp.Acceptance_Date between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "' and sp.IssueCenter_ID='" + Session["Depot_DepotID"] + "') and  A.Purchase_Center=S.Society_Id and A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss join tbl_Storage_Receipt_Details as TTT on sss.Receipt_ID=TTT.StorageReceipt_Id where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.DepotId='" + Session["Depot_DepotID"] + "' and sss.IssueID is not null) AND  A.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join  tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id  where RC.District_Id='" + Session["Depot_DistID"] + "' and RC.Depotid='" + Session["Depot_DepotID"] + "' and ast.IssueID is null and challan_no is not null)) as Count from [mpscsc].[dbo].[Society] AS S where Society_Id in (SELECT [Purchase_Center] FROM [mpscsc].[dbo].[Acceptance_Note_Detail] where Distt_ID='" + cetId + "') order by Society_Id ";
                }
            }

                SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable Dt1 = new DataTable();
                Dt1 = ds.Tables[0];
                foreach (DataRow dr in Dt1.Rows)
                {
                    int count = Convert.ToInt32(dr["Count"].ToString());
                    if (count == 0)
                    {
                        dr.Delete();
                    }
                }
                Dt1.AcceptChanges();
                if (Dt1.Rows.Count > 0)
                {
                    ddl_society.DataSource = Dt1;
                    ddl_society.DataValueField = "Society_Id";
                    ddl_society.DataTextField = "Society_Name";
                    ddl_society.DataBind();
                    ddl_society.Items.Insert(0, new ListItem("-- Select Society --", "0"));
                    ddl_society.SelectedIndex = 0;
                }
            }
        }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    protected void ddlcropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
            lblcropyr.Text = ddlcropyear.SelectedItem.Text.ToString();
            FillArrivalSourceddl();
            fillProcNew();
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        pnldate.Visible = false;
        pnlcropyr.Visible = true;

    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        pnlcropyr.Visible = false;
        pnldate.Visible = true;
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        Session["WLCDepSource"] = ddlArrival_Source.SelectedValue;
        if (ddlArrival_Source.Items.Count > 0)
        {
            if (ddlArrival_Source.SelectedValue == "01" || ddlArrival_Source.SelectedItem.Text == "Procurement")
            {
                Show_soc.Visible = true;
                trfromothdepot.Visible = false;
                tr_Disfromprc.Visible = false;
                ddl_society.DataSource = null;
                ddl_society.DataBind();
                GetSociety();
            }
            else if (ddlArrival_Source.SelectedValue == "07")
            {
                Show_soc.Visible = false;
                fillGridRailHead();
            }
            else
            {
                Show_soc.Visible = false;
                fillGridFCI_OTDepot();
            }
        }
    }
    protected void rbchallan_CheckedChanged(object sender, EventArgs e)
    {
        trdatewise.Visible = false;
    }
    protected void rbdate_CheckedChanged(object sender, EventArgs e)
    {
        trdatewise.Visible = true;
    }
    protected void btndatesub_Click(object sender, EventArgs e)
    {
        Session["Dateofdeposit"] = txtdatewisedate.Text;
        Session["commodityid"] = ddlcomm.SelectedValue.ToString();
        Session["WLCDepSource"] = ddlArrival_Source.SelectedValue.ToString();
        Session["godownid"] = ddlgodown.SelectedValue.ToString();
        Session["Mode"] = "Add";
        Session["CropYear"] = ddlcropyear.SelectedValue.ToString();

        Response.Redirect("~/IssueCenterLevel/Storage/DatewiseReceiving.aspx");
    }

    private void fillCommodity()
    {
        try
        {
            //string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Qry_Order";
            string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name asc";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcomm.DataSource = ds.Tables[0];
                ddlcomm.DataTextField = "Commodity_Name";
                ddlcomm.DataValueField = "Commodity_Id";
                ddlcomm.DataBind();
            }
        }
        catch (Exception)
        {

            //// throw;
        }
    }

    protected void FillGodown()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                ddlgodown.Items.Clear();
                string query = "";

                
                    query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN] WHERE BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID in (select distinct Godown_ID from tbl_MetaData_STACK where BranchID ='" + Session["BranchId"].ToString() + "' and Commodity_Id='" +  ddlcomm.SelectedValue + "')  ORDER BY [Godown_Name] ";
               
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlgodown.DataSource = ds.Tables[0];
                    ddlgodown.DataTextField = "Godown_Name";
                    ddlgodown.DataValueField = "Godown_ID";
                    ddlgodown.DataBind();
                   // ddlgodown.Items.Insert(0, " --select--");
                }
                else
                {
                    ddlgodown.DataSource = null;
                    ddlgodown.DataBind();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in FillGodown has occurred , try again!'); </script> ");
            }
        }
        else
        {
            Response.Redirect("../../Logout.aspx");
        }
    }

    protected void ddlcomm_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGodown();
    }
    protected void ddlProcCmd_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblcropyr.Text = ddlcropyear.SelectedItem.Text.ToString();
        FillArrivalSourceddl();
        fillProcNew();
    }
    protected void fillCropYear()
    {
        ListItem[] items = new ListItem[8];
        items[0] = new ListItem((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        items[1] = new ListItem((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString(), (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        items[2] = new ListItem((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        items[3] = new ListItem((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString(), (DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        items[4] = new ListItem((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString(), (DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        items[5] = new ListItem((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString(), (DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
        items[6] = new ListItem((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString(), (DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2));
        items[7] = new ListItem((DateTime.Now.Year - 7) + "-" + (DateTime.Now.Year - 6).ToString(), (DateTime.Now.Year - 7) + "-" + (DateTime.Now.Year - 6).ToString().Substring(2, 2));
        ddlcropyear.Items.Insert(0, "All");
        ddlcropyear.SelectedIndex = 1;
        //ddlcropyear.SelectedIndex = 2;
        ddlcropyear.Items.AddRange(items);
        ddlcropyear.DataBind();
    }
}