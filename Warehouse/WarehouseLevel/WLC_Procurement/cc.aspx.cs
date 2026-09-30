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

public partial class WarehouseLevel_WLC_Deposit_From_W_New : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["GodownID_New"] != null))
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
                rbdate.Visible = true;
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
                //query = "SELECT B.[Source_ID],B.[Source_Name] + ' (' + convert(nvarchar(10),((SELECT COUNT([Acceptance_Date]) FROM  [mpscsc].[dbo].[Acceptance_Note_Detail] AS A WHERE  A.Distt_ID='" + Distid + "' AND B.Source_ID='01' AND A.IssueCenter_ID='" + Session["G_DepotID"] + "' and A.TC_Number not in (select Challan_No froM tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Session["Depot_DistID"] + "' and AST.Depotid='" + Session["G_DepotID"] + "' and challan_no is not null)) + (SELECT COUNT(C.[S_of_arrival]) FROM [mpscsc].[dbo].[tbl_Receipt_Details] AS C WHERE C.Depot_ID='" + Session["G_DepotID"] + "'  AND C.Dist_Id='" + Distid + "' AND C.S_of_arrival=B.Source_ID and c.challan_no not in (select Challan_No FROM  tbl_Storage_Arrival_Stock AST where AST.District_Id='" + Session["Depot_DistID"] + "' and AST.Depotid='" + Session["G_DepotID"] + "' and AST.challan_no is not null ) and C.Vehile_no not in (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Session["G_DepotID"] + "' and AST.Depotid='" + Session["G_DepotID"] + "')))) + ')' as Source_Name FROM [MPSCSC].[dbo].[Source_Arrival_Type] AS B order by  Source_ID ";
                query = "select Source_ID,Source_Name from  [MPSCSC].[dbo].[Source_Arrival_Type] AS B order by  Source_ID";


            }
            else
            {
                //query = "SELECT B.[Source_ID],B.[Source_Name] + ' (' + convert(nvarchar(10),((SELECT COUNT(A.[Acceptance_Date]) FROM  [mpscsc].[dbo].[Acceptance_Note_Detail] AS A inner join  [mpscsc].[dbo].SCSC_Procurement as sp on A.Acceptance_No=sp.Acceptance_No WHERE  A.Distt_ID='" + Distid + "' AND B.Source_ID='01' AND sp.Crop_Year='" + ddlcropyear.SelectedItem.Text + "' and a.Purchase_Center=sp.Purchase_Center and a.TC_Number=sp.TC_Number and a.Truck_No=sp.Truck_Number AND A.IssueCenter_ID='" + Session["G_DepotID"] + "' and A.TC_Number not in (select Challan_No froM tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Session["Depot_DistID"] + "' and AST.Depotid='" + Session["G_DepotID"] + "' and challan_no is not null)) + (SELECT COUNT(C.[S_of_arrival]) FROM [mpscsc].[dbo].[tbl_Receipt_Details] AS C WHERE C.Depot_ID='" + Session["G_DepotID"] + "' and C.Crop_year='" + ddlcropyear.SelectedItem.Text + "' AND C.Dist_Id='" + Distid + "' AND C.S_of_arrival=B.Source_ID and c.challan_no not in (select Challan_No FROM  tbl_Storage_Arrival_Stock AST where AST.District_Id='" + Session["Depot_DistID"] + "' and AST.Depotid='" + Session["G_DepotID"] + "' and AST.challan_no is not null ) and C.Vehile_no not in (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Session["G_DepotID"] + "' and AST.Depotid='" + Session["G_DepotID"] + "')))) + ')' as Source_Name FROM [MPSCSC].[dbo].[Source_Arrival_Type] AS B order by  Source_ID ";
                // query = "SELECT B.[Source_ID],B.[Source_Name] + ' (' + convert(nvarchar(10),((SELECT COUNT([Acceptance_Date]) FROM  [mpscsc].[dbo].[Acceptance_Note_Detail] AS A WHERE  A.Distt_ID='" + Distid + "' AND B.Source_ID='01' AND A.IssueCenter_ID='" + Session["G_DepotID"] + "' and A.TC_Number not in (select Challan_No froM tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Session["Depot_DistID"] + "' and AST.Depotid='" + Session["G_DepotID"] + "' and challan_no is not null)) + (SELECT COUNT(C.[S_of_arrival]) FROM [mpscsc].[dbo].[tbl_Receipt_Details] AS C WHERE C.Depot_ID='" + Session["G_DepotID"] + "'  AND C.Dist_Id='" + Distid + "' AND C.S_of_arrival=B.Source_ID and c.challan_no not in (select Challan_No FROM  tbl_Storage_Arrival_Stock AST where AST.District_Id='" + Session["Depot_DistID"] + "' and AST.Depotid='" + Session["G_DepotID"] + "' and AST.challan_no is not null ) and C.Vehile_no not in (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Session["G_DepotID"] + "' and AST.Depotid='" + Session["G_DepotID"] + "')))) + ')' as Source_Name FROM [MPSCSC].[dbo].[Source_Arrival_Type] AS B order by  Source_ID ";
                query = "select Source_ID,Source_Name from  [MPSCSC].[dbo].[Source_Arrival_Type] AS B order by  Source_ID";

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
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            if (Session["BranchType"].ToString() == "G" && Session["GodownID_New"].ToString() != null)
            {
                string query = "";
                string Depot = Session["G_DepotID"].ToString();
                string Dist = Session["Depot_DistID"].ToString();
                if (ddlArrival_Source.SelectedValue == "07")
                {
                    query = " SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,convert(varchar(20),RD.arrival_date,103) as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.district_code=substring('" + Dist + "',3,2)  and RD.DepotID='" + Depot + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "'  AND RD.S_of_arrival = '07' and RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "' and RC.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='07' and challan_no is not null and RD.Truck_No!=AST.Truck_No)";
                }
                else if (ddlArrival_Source.SelectedValue == "03")
                {
                    if (ddlcropyear.SelectedItem.Text == "All")
                    {
                        query = " SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,convert(varchar(10),RD.arrival_date,103) as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name,rd.RO_No FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2) and RD.Depot_ID= '" + Depot + "' AND RD.S_of_arrival = '03' and RD.Godown='" + Session["GodownID_New"].ToString() + "'  and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "' and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='03' and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    }
                    else
                    {

                        query = "  SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,convert(varchar(10),RD.arrival_date,103) as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name,rd.RO_No FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2) and RD.Depot_ID= '" + Depot + "' AND RD.S_of_arrival = '03' and RD.Godown ='" + Session["GodownID_New"].ToString() + "' and  RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "' and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='03'  and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";

                    }

                }
                else if (ddlArrival_Source.SelectedValue == "04")
                {
                    if (ddlcropyear.SelectedItem.Text == "All")
                    {
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and sa.Source_ID = '04' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "' and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='04' and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    }
                    else
                    {
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "'  and sa.Source_ID = '04' and RD.Godown ='" + Session["GodownID_New"].ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "'  and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='04' and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    }
                }
                else if (ddlArrival_Source.SelectedValue == "05")
                {
                    if (ddlcropyear.SelectedItem.Text == "2019-2020")
                    {
                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details_2019 as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "'))  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";

                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details_2019 as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and sa.Source_ID = '" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "' and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details_2019 as RD with(Nolock) join tbl_MetaData_STORAGE_COMMODITY as cm with(Nolock) on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa with(Nolock) on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and sa.Source_ID = '" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC with(Nolock) join tbl_Storage_Arrival_Stock as AST with(Nolock) on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "' and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details_2019 as RD with(Nolock) join tbl_MetaData_STORAGE_COMMODITY as cm with(Nolock) on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa with(Nolock) on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and sa.Source_ID = '" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "'))  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details_2019 as RD with(Nolock) join tbl_MetaData_STORAGE_COMMODITY as cm with(Nolock) on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa with(Nolock) on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and sa.Source_ID = '" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "')) order by RD.arrival_date desc";

                    }
                    else
                    {
                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "'  and sa.Source_ID = '04' and RD.Godown ='" + Session["GodownID_New"].ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "'  and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='04' and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "'  and sa.Source_ID = '04' and RD.Godown ='" + Session["GodownID_New"].ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "'  and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='04' and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";

                    }
                }
                else if (ddlArrival_Source.SelectedValue == "10")
                {
                    if (ddlcropyear.SelectedItem.Text == "All")
                    {
                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,null as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where  RD.Depot_ID= '" + Depot + "' and sa.Source_ID = '02' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='02')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='02' and RD.Vehile_no!=AST.Truck_No)";
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where  RD.Depot_ID= '" + Depot + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    }
                    else
                    {

                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,null as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='"+ddlcropyear.SelectedItem.Text+"' and RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID= '" + Depot + "' and sa.Source_ID = '02' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='02')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='02' and RD.Vehile_no!=AST.Truck_No)";
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "'";

                    }
                }
                else if (ddlArrival_Source.SelectedValue == "02")
                {
                     // lblMsg.Text = "Chanera branch";
                    //if (ddlcropyear.SelectedItem.Text == "All")
                    //{
                    //    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,null as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where  RD.Depot_ID= '" + Depot + "' and sa.Source_ID = '02' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='02')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='02' and RD.Vehile_no!=AST.Truck_No)";
                    //    query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where  RD.Depot_ID= '" + Depot + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    //}
                    //else
                    //{

                    //    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,null as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='"+ddlcropyear.SelectedItem.Text+"' and RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID= '" + Depot + "' and sa.Source_ID = '02' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='02')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='02' and RD.Vehile_no!=AST.Truck_No)";
                    //    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    //    //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "'))  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    //query = "SELECT Dist_Id,Depot_ID,RD.Receipt_id,Vehile_no,RD.challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join dbo.tbl_MetaData_GODOWN G ON G.Godown_ID=RD.Godown join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival left join tbl_Storage_Arrival_Stock as AST on AST.Challan_No=RD.challan_no where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and G.BranchID='" + Session["G_BranchID"].ToString() + "'and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' AND AST.Challan_No IS NULL AND AST.Truck_No IS NULL order by RD.arrival_date desc";
                    //query = "SELECT Dist_Id,Depot_ID,RD.Receipt_id,Vehile_no,RD.challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD with(nolock) join dbo.tbl_MetaData_GODOWN G ON G.Godown_ID=RD.Godown join tbl_MetaData_STORAGE_COMMODITY as cm with(nolock) on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa with(nolock) on sa.Source_ID  =RD.S_of_arrival left join tbl_Storage_Arrival_Stock as AST on AST.Challan_No=RD.challan_no AND AST.Crop_Year=RD.Crop_year and AST.Commodity_Id=RD.Commodity and AST.Qty_No_of_Bags=RD.No_of_Bags where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and G.BranchID='" + Session["G_BranchID"].ToString() + "'and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' AND AST.Challan_No IS NULL AND AST.Truck_No IS NULL and RD.Godown='" + Session["GodownID_New"].ToString()+"' order by RD.arrival_date desc";
                    query = "SELECT Dist_Id,Depot_ID,RD.Receipt_id,Vehile_no,RD.challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD with(nolock) join dbo.tbl_MetaData_GODOWN G ON G.Godown_ID=RD.Godown join tbl_MetaData_STORAGE_COMMODITY as cm with(nolock) on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa with(nolock) on sa.Source_ID  =RD.S_of_arrival left join tbl_Storage_Arrival_Stock as AST on AST.Challan_No=RD.challan_no AND AST.BranchID=RD.Branch AND AST.Crop_Year=RD.Crop_year and AST.Commodity_Id=RD.Commodity and AST.Qty_No_of_Bags=RD.No_of_Bags where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and G.BranchID='" + Session["G_BranchID"].ToString() + "'and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' AND AST.Challan_No IS NULL AND AST.Truck_No IS NULL and RD.Godown='" + Session["GodownID_New"].ToString()+"' order by RD.arrival_date desc";

                    //}
                }
                else
                {
                    if (ddlcropyear.SelectedItem.Text == "All")
                    {
                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,null as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where  RD.Depot_ID= '" + Depot + "' and sa.Source_ID = '02' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='02')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='02' and RD.Vehile_no!=AST.Truck_No)";
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where  RD.Depot_ID= '" + Depot + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    }
                    else
                    {

                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,null as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='"+ddlcropyear.SelectedItem.Text+"' and RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID= '" + Depot + "' and sa.Source_ID = '02' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='02')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='02' and RD.Vehile_no!=AST.Truck_No)";
                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "'))  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                        //Dharmendra Savan Upload
                        query = "SELECT Dist_Id,Depot_ID,RD.Receipt_id,Vehile_no,RD.challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join dbo.tbl_MetaData_GODOWN G ON G.Godown_ID=RD.Godown join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival left join tbl_Storage_Arrival_Stock as AST on AST.Challan_No=RD.challan_no AND AST.BranchID=RD.Branch where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and G.BranchID='" + Session["G_BranchId"].ToString() + "' and G.Godown_ID='" + Session["GodownID_New"].ToString() + "' and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' AND AST.Challan_No IS NULL AND AST.Truck_No IS NULL order by RD.arrival_date desc";

                    }
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
                string query = "";
                string Depot = Session["G_DepotID"].ToString();
                string Dist = Session["Depot_DistID"].ToString();
                if (ddlArrival_Source.SelectedValue == "07")
                {
                    query = " SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,convert(varchar(20),RD.arrival_date,103) as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.district_code=substring('" + Dist + "',3,2)  and RD.DepotID='" + Depot + "' AND RD.S_of_arrival = '07' and RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "' and RC.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='07' and challan_no is not null and RD.Truck_No!=AST.Truck_No)";
                }
                else if (ddlArrival_Source.SelectedValue == "03")
                {
                    if (ddlcropyear.SelectedItem.Text == "All")
                    {
                        query = " SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,convert(varchar(10),RD.arrival_date,103) as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name,rd.RO_No FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2) and RD.Depot_ID= '" + Depot + "' AND RD.S_of_arrival = '03' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["G_BranchId"].ToString() + "') and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "' and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='03' and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    }
                    else
                    {

                        query = "  SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,convert(varchar(10),RD.arrival_date,103) as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name,rd.RO_No FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2) and RD.Depot_ID= '" + Depot + "' AND RD.S_of_arrival = '03' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["G_BranchId"].ToString() + "') and  RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "' and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='03'  and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";

                    }

                }
                else if (ddlArrival_Source.SelectedValue == "04")
                {
                    if (ddlcropyear.SelectedItem.Text == "All")
                    {
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and sa.Source_ID = '04' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["G_BranchId"].ToString() + "') and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "' and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='04' and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    }
                    else
                    {
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "'  and sa.Source_ID = '04' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["G_BranchId"].ToString() + "') and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "'  and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='04' and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    }
                }
                else if (ddlArrival_Source.SelectedValue == "10")
                {
                    if (ddlcropyear.SelectedItem.Text == "All")
                    {
                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,null as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where  RD.Depot_ID= '" + Depot + "' and sa.Source_ID = '02' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='02')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='02' and RD.Vehile_no!=AST.Truck_No)";
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where  RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["G_BranchId"].ToString() + "') and sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    }
                    else
                    {

                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,null as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='"+ddlcropyear.SelectedItem.Text+"' and RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID= '" + Depot + "' and sa.Source_ID = '02' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='02')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='02' and RD.Vehile_no!=AST.Truck_No)";
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["G_BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "'";

                    }
                }
                else
                {
                    if (ddlcropyear.SelectedItem.Text == "All")
                    {
                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,null as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where  RD.Depot_ID= '" + Depot + "' and sa.Source_ID = '02' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='02')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='02' and RD.Vehile_no!=AST.Truck_No)";
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where  RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["G_BranchId"].ToString() + "') and sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";
                    }
                    else
                    {

                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,null as 'RO_date',Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='"+ddlcropyear.SelectedItem.Text+"' and RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID= '" + Depot + "' and sa.Source_ID = '02' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='02')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='02' and RD.Vehile_no!=AST.Truck_No)";
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Depot_ID= '" + Depot + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["G_BranchId"].ToString() + "') and  sa.Source_ID ='" + ddlArrival_Source.SelectedValue.ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "')  and RD.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";

                    }
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
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillGridRailHead()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            try
            {
                if (Session["BranchType"].ToString() == "G" && Session["GodownID_New"].ToString() != null)
                {
                    string Distid = Session["Depot_DistID"].ToString().Substring(2, 2);
                    String Districtid = Session["Depot_DistID"].ToString();
                    string query = "";
                    if (ddlcropyear.SelectedItem.Text == "All")
                    {
                        //query = " SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,null as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.DepotID= '" + Session["G_DepotID"].ToString() + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Districtid + "' and RC.Depotid= '" + Session["G_DepotID"].ToString() + "' and challan_no is not null and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "')";
                        query = " SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,null as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.DepotID= '" + Session["G_DepotID"].ToString() + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Districtid + "' and RC.Depotid= '" + Session["G_DepotID"].ToString() + "' and challan_no is not null and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "')";
                        //query = " SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,null as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.district_code= '" + Distid + "' and RD.DepotID= '" + Session["G_DepotID"].ToString() + "' and RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Districtid + "' and RC.Depotid= '" + Session["G_DepotID"].ToString() + "' and challan_no is not null and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "') and RD.Truck_No NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Districtid + "' and AST.Depotid= '" + Session["G_DepotID"].ToString() + "' and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "')";
                    }
                    else
                    {
                        query = " SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,null as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.DepotID= '" + Session["G_DepotID"].ToString() + "' and RD.Cropyear='" + ddlcropyear.SelectedItem.Text + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Districtid + "' and RC.Depotid= '" + Session["G_DepotID"].ToString() + "' and challan_no is not null and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "')";
                        // query = " SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,null as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.district_code= '" + Distid + "' and RD.DepotID= '" + Session["G_DepotID"].ToString() + "' and RD.Cropyear='"+ddlcropyear.SelectedItem.Text+"' and RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Districtid + "' and RC.Depotid= '" + Session["G_DepotID"].ToString() + "' and challan_no is not null and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "') and RD.Truck_No NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Districtid + "' and AST.Depotid= '" + Session["G_DepotID"].ToString() + "' and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "')";

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
                else
                {
                    /////
                    string Distid = Session["Depot_DistID"].ToString().Substring(2, 2);
                    String Districtid = Session["Depot_DistID"].ToString();
                    string query = "";
                    if (ddlcropyear.SelectedItem.Text == "All")
                    {
                        query = " SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,null as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.DepotID= '" + Session["G_DepotID"].ToString() + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["G_BranchId"].ToString() + "') and RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Districtid + "' and RC.Depotid= '" + Session["G_DepotID"].ToString() + "' and challan_no is not null and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "')";
                        //query = " SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,null as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.district_code= '" + Distid + "' and RD.DepotID= '" + Session["G_DepotID"].ToString() + "' and RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Districtid + "' and RC.Depotid= '" + Session["G_DepotID"].ToString() + "' and challan_no is not null and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "') and RD.Truck_No NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Districtid + "' and AST.Depotid= '" + Session["G_DepotID"].ToString() + "' and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "')";
                    }
                    else
                    {
                        query = " SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,null as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.DepotID= '" + Session["G_DepotID"].ToString() + "' and RD.Cropyear='" + ddlcropyear.SelectedItem.Text + "' and RD.Godown in (select Godown_ID from  dbo.tbl_MetaData_GODOWN where  tbl_MetaData_GODOWN.BranchID='" + Session["G_BranchId"].ToString() + "') and RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Districtid + "' and RC.Depotid= '" + Session["G_DepotID"].ToString() + "' and challan_no is not null and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "')";
                        // query = " SELECT district_code as 'Dist_Id',DepotID as 'Depot_ID',Rack_No,Truck_No as 'Vehile_no',TC_Number as 'challan_no',null as RO_No,null as RO_date,Recd_Bags as 'No_of_Bags',Recd_Qty,null as IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.RR_receipt_Depot as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity where RD.district_code= '" + Distid + "' and RD.DepotID= '" + Session["G_DepotID"].ToString() + "' and RD.Cropyear='"+ddlcropyear.SelectedItem.Text+"' and RD.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Districtid + "' and RC.Depotid= '" + Session["G_DepotID"].ToString() + "' and challan_no is not null and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "') and RD.Truck_No NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Districtid + "' and AST.Depotid= '" + Session["G_DepotID"].ToString() + "' and AST.Source_of_Arrival = '" + ddlArrival_Source.SelectedValue.ToString() + "')";

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
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
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
                        cmd.Parameters.AddWithValue("@Depot_ID", Session["G_DepotID"].ToString());
                        cmd.Parameters.AddWithValue("@Purchase_Center", ddl_society.SelectedValue);
                    }

                    else
                    {
                        cmd = new SqlCommand("MPWLC_Select_PendingTC_from_Proc_CropYealy", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Dist_Id", Session["Depot_DistID"].ToString());
                        cmd.Parameters.AddWithValue("@Depot_ID", Session["G_DepotID"].ToString());
                        cmd.Parameters.AddWithValue("@Purchase_Center", ddl_society.SelectedValue);
                        cmd.Parameters.AddWithValue("@CropYear", ddlcropyear.SelectedItem.Text);
                    }
                }
                if (RadioButton2.Checked)
                {

                    cmd = new SqlCommand("MPWLC_Select_PendingTC_from_Proc_BWDates", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Dist_Id", Session["Depot_DistID"].ToString());
                    cmd.Parameters.AddWithValue("@Depot_ID", Session["G_DepotID"].ToString());
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
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("MPWLC_Select_PendingTC_from_Proc_CropYealy", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Dist_Id", Session["Depot_DistID"].ToString());
                cmd.Parameters.AddWithValue("@Depot_ID", Session["G_DepotID"].ToString());
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
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    int rowIndex = gvr.RowIndex;
                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    String Receiptid = Convert.ToString(GvuFromFCI_OTHDepot.DataKeys[row.RowIndex].Value);
                    string lblchallan_no = (GvuFromFCI_OTHDepot.Rows[rowIndex].FindControl("lblchallan_no") as Label).Text;
                    string lblRO_date = (GvuFromFCI_OTHDepot.Rows[rowIndex].FindControl("lblRO_date") as Label).Text;
                    //String lblchallan_no = row.FindControl("lblchallan_no") as Label;
                    //String lblRO_date = row.FindControl("lblRO_date") as Label;
                    Session["WLC_Dep_Receipt_id"] = Receiptid.ToString();
                    Session["ChallanNowlc"] = lblchallan_no.ToString();
                    Session["WLC_Challan_Datewlc"] = lblRO_date.ToString();
                    Session["WLC_Dep_Dist_Id"] = Session["Depot_DistID"].ToString();
                    Session["WLC_Dep_Depot_ID"] = Session["G_DepotID"].ToString();
                    Session["Mode"] = "Add";
                    if(ddlArrival_Source.SelectedValue.ToString() == "--Select--")
                    {
                        Session["WLCDepSource"] = "05";
                    }
                    else
                    {
                        Session["WLCDepSource"] = ddlArrival_Source.SelectedValue;

                    }
                    Response.Redirect("~/WarehouseLevel/WLC_FRM_01_02_03_Receipt_PvtW.aspx");
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
            // Response.Redirect("WLC_FRM_01_02_03_Receipt_PvtW.aspx");
            Response.Redirect("~/WarehouseLevel/Receiptfrom_Procurement_W.aspx");
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
            //Session["WLCDepSource"] = ddlArrival_Source.SelectedValue;
            // Response.Redirect("WLC_FRM_01_02_03_Receipt_PvtW.aspx");
            Response.Redirect("~/WarehouseLevel/Receiptfrom_Procurement_W.aspx");
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
    //30-03.-2015

    protected void fillProcNew()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            try
            {
                if (Session["BranchType"].ToString() == "G" && Session["GodownID_New"].ToString() != null)
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand();
                    if (RadioButton1.Checked)
                    {
                        if (ddlcropyear.SelectedItem.Text == "All")
                        {
                            cmd = new SqlCommand("select  Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.whrreq1 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.GodownID='" + Session["GodownID_New"].ToString() + "' and whrr.WHR_Request not in (select Distinct sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                        }
                        else if (ddlcropyear.SelectedItem.Text == "2015-2016")
                        {
                            cmd = new SqlCommand("select  Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.whrreq1 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.GodownID='" + Session["GodownID_New"].ToString() + "' and whrr.WHR_Request not in (select Distinct sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);

                        }
                        else if (ddlcropyear.SelectedItem.Text == "2016-2017")
                        {
                            if (ddlProcCmd.SelectedItem.Text == "Paddy-Common")
                            {
                                // cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DeposioterFormDtl as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "'  and whrr.GodownID='" + Session["GodownID_New"].ToString() + "' and  whrr.WHR_Request not in (select Distinct sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);

                                cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2016 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.GodownID='" + Session["GodownID_New"].ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                            }
                            else if (ddlProcCmd.SelectedItem.Text == "Paddy-Grade-A")
                            {
                                cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2016 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.GodownID='" + Session["GodownID_New"].ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                            }
                            else if (ddlProcCmd.SelectedItem.Text == "Bajra")
                            {
                                cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2016 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.GodownID='" + Session["GodownID_New"].ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                            }
                            else if (ddlProcCmd.SelectedItem.Text == "Jau")
                            {
                                cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2016 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.GodownID='" + Session["GodownID_New"].ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                            }
                            else if (ddlProcCmd.SelectedItem.Text == "Jowar")
                            {
                                cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2016 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.GodownID='" + Session["GodownID_New"].ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                            }
                            else if (ddlProcCmd.SelectedItem.Text == "Maize(Makka)")
                            {
                                cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2016 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.GodownID='" + Session["GodownID_New"].ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                            }
                            else if (ddlProcCmd.SelectedItem.Text == "Wheat-PSS")
                            {
                                cmd = new SqlCommand("select  Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DeposioterFormDtl as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "'  and whrr.GodownID='" + Session["GodownID_New"].ToString() + "' and  whrr.WHR_Request not in (select Distinct sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);

                                //cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DeposioterFormDtl as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                                //local
                                //cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DeposioterFormDtl as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                            }
                            else if (ddlProcCmd.SelectedItem.Text == "Godown Owner(Stock Handover)")
                            {
                                cmd = new SqlCommand("select CONVERT(varchar(10),FSDO.Delivery_Order_Date,103) as Acceptance_Date,SGE.BranchID as IssueCenter_ID,SUM(DSDG.No_Of_Bags) as Recd_Bags,SUM(DSDG.Bags_Weight+DSDG.Loss-DSDG.Gain) as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=SGE.Commodity_ID) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=SGE.BranchID and Godown_ID=SGE.Godown_ID)as Godown,FSDO.Delivery_Order_No as WHR_Request from tbl_Storage_Final_Stock_Delivery_Order as FSDO inner join tbl_Storage_GatePass_Enrty as SGE on SGE.Issue_Source_ID=FSDO.Delivery_Order_No inner join [tbl_Storage_Final_Stock_Delivery_GatePass] as FSDG on FSDG.GatePass_No=SGE.GatePass_No inner join tbl_Delivery_Stacking_Details_GatePass as DSDG on DSDG.GatePass_No=SGE.GatePass_No where FSDG.DeliverdAgent='11' and SGE.BranchID='" + Session["G_BranchId"].ToString() + "' and SGE.Godown_ID='" + Session["GodownID_New"].ToString() + "' and FSDO.Delivery_Order_No not in (select Distinct sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=FSDO.Delivery_Order_No and sss.IssueID='NA')group by FSDO.Delivery_Order_No,FSDO.Delivery_Order_Date,FSDO.Qty_Issued_No_Bags_Sound,FSDO.Qty_Issued_Weight,SGE.Commodity_ID,SGE.BranchID,SGE.Godown_ID order by Acceptance_Date", con);
                            }
                        }
                       
                        else if (ddlcropyear.SelectedItem.Text == "2017-2018")
                        { 
                            //Wheat 2017-18
                            if (ddlProcCmd.SelectedItem.Text == "Wheat-PSS")
                            {
                                //cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DeposioterFormDtl as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                                cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormWheat2017 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.GodownID='" + Session["GodownID_New"].ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                            }
                            else if (ddlProcCmd.SelectedItem.Text == "Paddy-Common")
                            {
                                cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormKharif2017 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.GodownID='" + Session["GodownID_New"].ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                            }

                        }
                        //Wheat 2018-19
                        else if (ddlcropyear.SelectedItem.Text == "2018-2019")
                        {
                            //Wheat 2018-19
                            if (ddlProcCmd.SelectedItem.Text == "Wheat-PSS")
                            {
                                //cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormWheat2017 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.GodownID='" + Session["GodownID_New"].ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                                cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormWheat2018 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.GodownID='" + Session["GodownID_New"].ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                            }
                            else if ((ddlProcCmd.SelectedItem.Text == "GRAM" || ddlProcCmd.SelectedItem.Text == "Mustard-Sarason" || ddlProcCmd.SelectedItem.Text == "LENTIL") && ddlDepositor.SelectedValue.ToString() == "10535")
                            {
                                //CSM
                                //cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormCSM2018 as whrr where Branch_Id='" + Session["BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                                cmd = new SqlCommand("select Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DepositorFormCSM2018 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.GodownID='" + Session["GodownID_New"].ToString() + "' and whrr.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and whrr.WHR_Request not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);


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
                else
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand();
                    if (RadioButton1.Checked)
                    {

                        //latest 30-05-15 cmd = new SqlCommand("SELECT distinct prc.IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',sum(prc.Bags) as Recd_Bags, sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Acceptance_Note_Detail] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId inner join MPSCSC.dbo.SCSC_Procurement as sp on Prc.IssueID=sp.Receipt_Id and Prc.Acceptance_No=sp.Acceptance_No  where Prc.WHR_Request is not null  and  sp.Branch_Id='" + Session["G_BranchId"].ToString() + "' and Prc.WHR_Request not in (select Distinct sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=Prc.WHR_Request and sss.IssueID='NA') group by WHR_Request,prc.IssueCenter_ID,Prc.Acceptance_Date,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name", con);


                        // cmd = new SqlCommand("select  Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,'Wheat-PSS' as Commodity_Name,WHR_Request  from mpscsc.dbo.whrreq1 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.WHR_Request not in (select Distinct sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);

                        //latest from accep tbl 16-06-15   
                        if (ddlcropyear.SelectedItem.Text == "All")
                        {
                            cmd = new SqlCommand("select  Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.whrreq1 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.WHR_Request not in (select Distinct sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);
                        }
                        else if (ddlcropyear.SelectedItem.Text == "2015-2016")
                        {
                            cmd = new SqlCommand("select  Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.whrreq1 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select Distinct sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);

                        }
                        else
                        {
                            cmd = new SqlCommand("select  Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whrr.Commodity_Id) as Commodity_Name,(select Godown_Name from  dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id and Godown_ID=whrr.GodownID)as Godown ,WHR_Request  from mpscsc.dbo.DeposioterFormDtl as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and whrr.WHR_Request not in (select Distinct sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.AcceptanceNo=whrr.WHR_Request and sss.IssueID='NA') order by Acceptance_Date", con);

                        }

                        //16-06-2015
                        // cmd = new SqlCommand("select  Acceptance_Date,IssueCenter_ID,Bags as Recd_Bags,Qty as Recd_Qty,'Wheat-PSS' as Commodity_Name, (select Godown_Name from  dbo.tbl_MetaData_GODOWN where Godown_ID=whrr.GodownID and tbl_MetaData_GODOWN.BranchID=whrr.Branch_Id) as Godown ,WHR_Request  from mpscsc.dbo.whrreq1 as whrr where Branch_Id='" + Session["G_BranchId"].ToString() + "' and whrr.WHR_Request not in (select Distinct sss.Acpt_FCIRO_No from tbl_Storage_Receipt_Details As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.IssueID='NA') and whrr.Acceptance_Date not in (select Distinct sss.Acpt_FCIRO_No from tbl_Storage_Receipt_Details As sss where  sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.Acpt_FCIRO_No=whrr.WHR_Request and sss.Acpt_FCIRO_Date=whrr.Acceptance_Date and sss.IssueID='NA') order by Acceptance_Date", con);

                        cmd.CommandType = CommandType.Text;

                        //  cmd.Parameters.AddWithValue("@Depot_ID", Session["G_DepotID"].ToString());
                        // cmd.Parameters.AddWithValue("@Purchase_Center", ddl_society.SelectedValue);

                    }


                    da.SelectCommand = cmd;
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

            Response.Redirect("WLC_FRM_01_02_03_Receipt_PvtW.aspx");
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
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
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
                cmd.Parameters["@depot_id"].Value = Session["G_BranchId"].ToString();
                int Res = cmd.ExecuteNonQuery();
                da.SelectCommand = cmd;
                da.Fill(ds1, "temp");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if (ddldepositortype.SelectedItem.Text == "Institution")
                    {
                        //ddlDepositor.DataSource = ds1;
                        //ddlDepositor.DataTextField = "Depositor_Name";
                        //ddlDepositor.DataValueField = "Depositor_ID";
                        //ddlDepositor.DataBind();
                        //ddlDepositor.Items.Insert(0, "--Select--");
                        //ddlDepositor.SelectedValue = "129";

                        //For Institution
                        //string query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','181','184','4679','10535')";
                        string query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','181','184','4679','10535','926','14966','15478')";

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
                    Response.Redirect("WLC_FRM_01_02_03_Receipt_PvtW.aspx");

                }
                else if (ddldepositortype.SelectedItem.Text != "Institution")
                {
                    Session["depositortypeA"] = ddldepositortype.SelectedItem.Text;
                    Session["DepositorA"] = ddlDepositor.SelectedItem.Text;
                    Session["WLCDepSource"] = "NON-MPSCSC";
                    Response.Redirect("WLC_FRM_01_02_03_Receipt_PvtW.aspx");

                }
                else if (ddlDepositor.SelectedItem.Text == "NCCF")
                {
                    Session["depositortypeA"] = ddldepositortype.SelectedItem.Text;
                    Session["DepositorA"] = ddlDepositor.SelectedItem.Text;
                    Session["WLCDepSource"] = "NON-MPSCSC";
                    Response.Redirect("WLC_FRM_01_02_03_Receipt_PvtW.aspx");

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
        string str = "";
        if (RadioButton1.Checked)
        {
            if (ddlcropyear.SelectedItem.Text == "All")
            {
                str = "SELECT [Society_Id],(('('+[Society_Id]+')'+'/'+[Society_Name]+' / '+[SocPlace])+'  ( '+CAST((SELECT COUNT(Acceptance_Date) FROM [mpscsc].[dbo].[Acceptance_Note_Detail] AS A WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["G_DepotID"] + "' and  A.Purchase_Center=S.Society_Id AND A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss join tbl_Storage_Receipt_Details as TTT on sss.Receipt_ID=TTT.StorageReceipt_Id where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.DepotId='" + Session["G_DepotID"] + "' and sss.IssueID is not null) AND  A.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join  tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id  where RC.District_Id='" + Session["Depot_DistID"] + "' and RC.Depotid='" + Session["G_DepotID"] + "' and ast.IssueID is null and challan_no is not null)) AS VARCHAR(10)) +' )') AS Society_Name,((SELECT COUNT(Acceptance_Date) FROM [mpscsc].[dbo].[Acceptance_Note_Detail] AS A WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["G_DepotID"] + "' and  A.Purchase_Center=S.Society_Id AND A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss join tbl_Storage_Receipt_Details as TTT on sss.Receipt_ID=TTT.StorageReceipt_Id where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.DepotId='" + Session["G_DepotID"] + "' and sss.IssueID is not null) AND  A.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join  tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id  where RC.District_Id='" + Session["Depot_DistID"] + "' and RC.Depotid='" + Session["G_DepotID"] + "' and ast.IssueID is null and challan_no is not null))) AS Count FROM [mpscsc].[dbo].[Society] AS S where Society_Id in (SELECT [Purchase_Center] FROM [mpscsc].[dbo].[Acceptance_Note_Detail] where Distt_ID='" + cetId + "')";
                //str = "SELECT [Society_Id],(('('+[Society_Id]+')'+'/'+[Society_Name]+' / '+[SocPlace])+'  ( '+CAST((SELECT COUNT(Acceptance_Date) FROM [mpscsc].[dbo].[Acceptance_Note_Detail] AS A WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["G_DepotID"] + "' and  A.Purchase_Center=S.Society_Id AND A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss join tbl_Storage_Receipt_Details as TTT on sss.Receipt_ID=TTT.StorageReceipt_Id where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.DepotId='" + Session["G_DepotID"] + "' and sss.IssueID is not null) AND  A.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join  tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id  where RC.District_Id='" + Session["Depot_DistID"] + "' and RC.Depotid='" + Session["G_DepotID"] + "' and ast.IssueID is null and challan_no is not null)) AS VARCHAR(10)) +' )') AS Society_Name,((SELECT COUNT(Acceptance_Date) FROM [mpscsc].[dbo].[Acceptance_Note_Detail] AS A WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["G_DepotID"] + "' and  A.Purchase_Center=S.Society_Id AND A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss join tbl_Storage_Receipt_Details as TTT on sss.Receipt_ID=TTT.StorageReceipt_Id where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.DepotId='" + Session["G_DepotID"] + "' and sss.IssueID is not null) AND  A.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join  tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id  where RC.District_Id='" + Session["Depot_DistID"] + "' and RC.Depotid='" + Session["G_DepotID"] + "' and ast.IssueID is null and challan_no is not null))) AS Count FROM [mpscsc].[dbo].[Society] AS S where Society_Id in (SELECT [Purchase_Center] FROM [mpscsc].[dbo].[Acceptance_Note_Detail] where Distt_ID='" + cetId + "')";

            }
            if (ddlcropyear.SelectedItem.Text != "All")
            {
                // str = " select Society_Id, (('('+[Society_Id]+')'+'/'+[Society_Name]+' / '+[SocPlace])+ '('+cast((select count(a.Acceptance_No) from [mpscsc].[dbo].[Acceptance_Note_Detail] AS A  WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["G_DepotID"] + "' and a.Acceptance_No in(select Acceptance_No from mpscsc.dbo.SCSC_Procurement as sp  where sp.Crop_Year='" + ddlcropyear.SelectedItem.Text + "' and sp.IssueCenter_ID='" + Session["G_DepotID"] + "') and  A.Purchase_Center=S.Society_Id and A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss join tbl_Storage_Receipt_Details as TTT on sss.Receipt_ID=TTT.StorageReceipt_Id where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.DepotId='" + Session["G_DepotID"] + "' and sss.IssueID is not null) AND  A.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join  tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id  where RC.District_Id='" + Session["Depot_DistID"] + "' and RC.Depotid='" + Session["G_DepotID"] + "' and ast.IssueID is null and challan_no is not null))AS VARCHAR(10)) +' )') as Society_Name,(select count(a.Acceptance_No) from [mpscsc].[dbo].[Acceptance_Note_Detail] AS A  WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["G_DepotID"] + "' and a.Acceptance_No in(select Acceptance_No from mpscsc.dbo.SCSC_Procurement as sp  where sp.Crop_Year='" + ddlcropyear.SelectedItem.Text + "' and sp.IssueCenter_ID='" + Session["G_DepotID"] + "') and  A.Purchase_Center=S.Society_Id and A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss join tbl_Storage_Receipt_Details as TTT on sss.Receipt_ID=TTT.StorageReceipt_Id where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.DepotId='" + Session["G_DepotID"] + "' and sss.IssueID is not null) AND  A.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join  tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id  where RC.District_Id='" + Session["Depot_DistID"] + "' and RC.Depotid='" + Session["G_DepotID"] + "' and ast.IssueID is null and challan_no is not null)) as Count from [mpscsc].[dbo].[Society] AS S where Society_Id in (SELECT Distinct [Purchase_Center] FROM [mpscsc].[dbo].[Acceptance_Note_Detail] where Distt_ID='" + cetId + "') order by Society_Id ";
                str = "select Distinct Society_Id, (('('+ [Society_Id]+')'+'/'+[Society_Name]+' / '+[SocPlace])+'('+cast((select count(a.Acceptance_No) from [mpscsc].[dbo].[Acceptance_Note_Detail] AS A  WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["G_DepotID"] + "' and a.Acceptance_No in(select  Acceptance_No from mpscsc.dbo.SCSC_Procurement as sp  where sp.Crop_Year='" + ddlcropyear.SelectedItem.Text + "' and sp.IssueCenter_ID='" + Session["G_DepotID"] + "') and  A.Purchase_Center=S.Society_Id and A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.IssueID is not null))as varchar(10))) as Society_Name,(select count(a.Acceptance_No) from [mpscsc].[dbo].[Acceptance_Note_Detail] AS A  WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["G_DepotID"] + "' and a.Acceptance_No in(select  Acceptance_No from mpscsc.dbo.SCSC_Procurement as sp  where sp.Crop_Year='" + ddlcropyear.SelectedItem.Text + "' and sp.IssueCenter_ID='" + Session["G_DepotID"] + "') and  A.Purchase_Center=S.Society_Id and A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.BranchID='" + Session["G_BranchId"].ToString() + "' and sss.IssueID is not null)) as Count from [mpscsc].[dbo].[Society] AS S where Society_Id in (SELECT Distinct [Purchase_Center] FROM [mpscsc].[dbo].[Acceptance_Note_Detail] where Distt_ID='" + cetId + "') order by Society_Id";
            }
        }
        if (RadioButton2.Checked)
        {
            if (txtdatefrom.Text != "" && txtdateto.Text != "")
            {
                str = " select Society_Id, (('('+[Society_Id]+')'+'/'+[Society_Name]+' / '+[SocPlace])+ '('+cast((select count(a.Acceptance_No) from [mpscsc].[dbo].[Acceptance_Note_Detail] AS A  WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["G_DepotID"] + "' and a.Acceptance_No in(select Acceptance_No from mpscsc.dbo.SCSC_Procurement as sp  where sp.Acceptance_Date between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "' and sp.IssueCenter_ID='" + Session["G_DepotID"] + "') and  A.Purchase_Center=S.Society_Id and A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss join tbl_Storage_Receipt_Details as TTT on sss.Receipt_ID=TTT.StorageReceipt_Id where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.DepotId='" + Session["G_DepotID"] + "' and sss.IssueID is not null) AND  A.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join  tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id  where RC.District_Id='" + Session["Depot_DistID"] + "' and RC.Depotid='" + Session["G_DepotID"] + "' and ast.IssueID is null and challan_no is not null))AS VARCHAR(10)) +' )') as Society_Name,(select count(a.Acceptance_No) from [mpscsc].[dbo].[Acceptance_Note_Detail] AS A  WHERE A.Acceptance_No <> '' and A.Distt_ID='" + cetId + "' and A.IssueCenter_Id='" + Session["G_DepotID"] + "' and a.Acceptance_No in(select Acceptance_No from mpscsc.dbo.SCSC_Procurement as sp  where sp.Acceptance_Date between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "' and sp.IssueCenter_ID='" + Session["G_DepotID"] + "') and  A.Purchase_Center=S.Society_Id and A.IssueID NOT IN (select sss.IssueID from tbl_Storage_Arrival_Stock As sss join tbl_Storage_Receipt_Details as TTT on sss.Receipt_ID=TTT.StorageReceipt_Id where sss.District_Id='" + Session["Depot_DistID"] + "' and sss.DepotId='" + Session["G_DepotID"] + "' and sss.IssueID is not null) AND  A.TC_Number not in (select Challan_No from tbl_Storage_Receipt_Details RC join  tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id  where RC.District_Id='" + Session["Depot_DistID"] + "' and RC.Depotid='" + Session["G_DepotID"] + "' and ast.IssueID is null and challan_no is not null)) as Count from [mpscsc].[dbo].[Society] AS S where Society_Id in (SELECT [Purchase_Center] FROM [mpscsc].[dbo].[Acceptance_Note_Detail] where Distt_ID='" + cetId + "') order by Society_Id ";
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

        //Response.Redirect("~/IssueCenterLevel/Storage/DatewiseReceiving.aspx");
        
        fillGridFCI_OTDepot_Datewise();
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
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            try
            {
                if (Session["BranchType"].ToString() == "G" && Session["GodownID_New"].ToString() != null)
                {
                    ddlgodown.Items.Clear();
                    string query = "";


                    query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN] WHERE BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID= '" + Session["GodownID_New"].ToString() + "'  ORDER BY [Godown_Name] ";

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
                else
                {

                    ddlgodown.Items.Clear();
                    string query = "";


                    query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN] WHERE BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID in (select distinct Godown_ID from tbl_MetaData_STACK where BranchID ='" + Session["G_BranchId"].ToString() + "' and Commodity_Id='" + ddlcomm.SelectedValue + "')  ORDER BY [Godown_Name] ";

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
        ListItem[] items = new ListItem[7];
        items[0] = new ListItem((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        items[1] = new ListItem((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString(), (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        items[2] = new ListItem((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        items[3] = new ListItem((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString(), (DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        items[4] = new ListItem((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString(), (DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        items[5] = new ListItem((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString(), (DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
        items[6] = new ListItem((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString(), (DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2));
        ddlcropyear.Items.Insert(0, "All");
        ddlcropyear.SelectedIndex = 1;
        //ddlcropyear.SelectedIndex = 2;
        ddlcropyear.Items.AddRange(items);
        ddlcropyear.DataBind();
    }
    protected void fillGridFCI_OTDepot_Datewise()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            if (Session["BranchType"].ToString() == "G" && Session["GodownID_New"].ToString() != null)
            {
                string query = "";
                string Depot = Session["G_DepotID"].ToString();
                string Dist = Session["Depot_DistID"].ToString();
                //if (ddlArrival_Source.SelectedValue == "05")
                if ("05" == "05")
                {
                    if (ddlcropyear.SelectedItem.Text == "2019-2020")
                    {
                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details_2019 as RD with(Nolock) join tbl_MetaData_STORAGE_COMMODITY as cm with(Nolock) on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa with(Nolock) on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and sa.Source_ID = '" + ddlArrival_Source.SelectedValue.ToString() + "' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='" + ddlArrival_Source.SelectedValue.ToString() + "' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "')) order by RD.arrival_date desc";
                        //query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details_2019 as RD with(Nolock) join tbl_MetaData_STORAGE_COMMODITY as cm with(Nolock) on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa with(Nolock) on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.arrival_date='08/26/2020' and RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and sa.Source_ID = '05' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='05' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "') and challan_date='08/26/2020') order by RD.arrival_date desc";
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details_2019 as RD with(Nolock) join tbl_MetaData_STORAGE_COMMODITY as cm with(Nolock) on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa with(Nolock) on sa.Source_ID  =RD.S_of_arrival where RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "' and RD.arrival_date='"+ getDate_MDY(txtdatewisedate.Text) + "' and RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and sa.Source_ID = '05' and RD.Godown='" + Session["GodownID_New"].ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Dist + "' and AST.Depotid='" + Depot + "' and challan_no is not null AND AST.Source_of_Arrival ='05' and AST.Crop_Year in ('" + ddlcropyear.SelectedValue + "','" + ddlcropyear.SelectedItem.Text + "') and challan_date='" + getDate_MDY(txtdatewisedate.Text) + "') order by RD.arrival_date desc";

                    }
                    
                    else
                    {
                        query = "SELECT Dist_Id,Depot_ID,Receipt_id,Vehile_no,challan_no,null as 'RO_No' ,convert(varchar(20),RD.arrival_date,103) as RO_date,Recieved_Bags,Recd_Qty,IsDeposit,cm.Commodity_Name FROM MPSCSC.dbo.tbl_Receipt_Details as RD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=RD.Commodity join MPSCSC.dbo.Source_Arrival_Type as sa on sa.Source_ID  =RD.S_of_arrival where RD.Dist_Id=substring('" + Dist + "',3,2)  and RD.Depot_ID='" + Depot + "' and RD.Crop_year='" + ddlcropyear.SelectedItem.Text + "'  and sa.Source_ID = '04' and RD.Godown ='" + Session["GodownID_New"].ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Receipt_Details RC join tbl_Storage_Arrival_Stock as AST on AST.Receipt_ID=RC.StorageReceipt_Id where RC.District_Id='" + Dist + "'  and RC.Depotid='" + Depot + "' and AST.Source_of_Arrival ='04' and challan_no is not null and RD.Vehile_no!=AST.Truck_No) order by RD.arrival_date desc";

                    }
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
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }


}
