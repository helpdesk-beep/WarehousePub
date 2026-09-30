using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.MobileControls;
public partial class Mobile_MReceivning : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataTable EditStack = new DataTable();
    DataTable EditStackNonMPSCSC = new DataTable();
    string Todaydate = "";
    string CheckValid = "";
    string receiptid = string.Empty;
    string gatePassid = string.Empty;
    string ArrivalStockid = string.Empty;
    SqlCommand cmd = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // txtdod.Attributes["type"] = "number"; 
            txtdod.Attributes["type"] = "date";
            fillDepositorType();
            fillCommodity();
            FillGodown();
            
            ddldepositortype_SelectedIndexChanged(sender, e);
         //   FillArrivalSourceddl();
            fillTransporter();
            fillCategory();
            fillCropYear();
        }
    }

    private void fillCommodity()
    {
        try
        {
            string query = "";
            //string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Qry_Order";
            if (ddldepositortype.SelectedItem.Text == "Institution")
            {
                query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name asc";
            }
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCommodity.DataSource = ds.Tables[0];
                ddlCommodity.DataTextField = "Commodity_Name";
                ddlCommodity.DataValueField = "Commodity_Id";
                ddlCommodity.DataBind();
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

             
                query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN] WHERE BranchID = '" + Session["BranchId"].ToString() + "' and Remarks='Y' ORDER BY [Godown_Name] ";
                   
                
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlgodown.DataSource = ds.Tables[0];
                    ddlgodown.DataTextField = "Godown_Name";
                    ddlgodown.DataValueField = "Godown_ID";
                    ddlgodown.DataBind();
                    ddlgodown.Items.Insert(0, " --select--");
                }
                else
                {
                    ddlgodown.DataSource = null;
                    ddlgodown.DataBind();
                }
            }
            catch (Exception ex)
            {
                //lblmsg.Text = ex.Message.ToString();
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in FillGodown has occurred , try again!'); </script> ");
            }
        }
        else
        {
            Response.Redirect("../../Logout.aspx");
        }
    }

    protected void fillDepositorType()
    {
        try
        {
            string query = "select Depositor_Type from tbl_MetaData_Depositor_Type order by Report_Seq_Id";
            SqlCommand cmd = new SqlCommand(query, Con);
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
    protected void ddldepositortype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                ddlDepositor.Items.Clear();
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                DataSet ds1 = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.Connection = Con;
                cmd = new SqlCommand("sp_getDepositor_Depo_wise", Con);
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
                        ddlDepositor.DataSource = ds1;
                        ddlDepositor.DataTextField = "Depositor_Name";
                        ddlDepositor.DataValueField = "Depositor_ID";
                        ddlDepositor.DataBind();
                        ddlDepositor.Items.Insert(0, "--Select--");
                        ddlDepositor.SelectedValue = "129";
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
                Con.Close();
            }
        }
        else
        {
            Response.Redirect("../../Logout.aspx");
        }
    }

    protected void FillArrivalSourceddl()
    {
        try
        {
            string Distid = Session["Depot_DistID"].ToString().Substring(2, 2);
            string query = "";
           
            query = "SELECT B.[Source_ID],B.[Source_Name] Source_Name FROM MPSCSCSVR.[MPSCSC].[dbo].[Source_Arrival_Type] AS B order by  Source_ID";
          //  query = "SELECT B.[Source_ID],B.[Source_Name] Source_Name FROM [MPSCSC].[dbo].[Source_Arrival_Type] AS B order by  Source_ID";

            SqlCommand cmd = new SqlCommand(query, Con);
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
    private void fillCategory()
    {
        try
        {
            string query = "SELECT Category_Id, Category_Name FROM tbl_MetaData_STORAGE_CATEGORY";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCategory.Items.Clear();
                ddlCategory.DataSource = ds.Tables[0];
                ddlCategory.DataTextField = "Category_Name";
                ddlCategory.DataValueField = "Category_Id";
                ddlCategory.DataBind();
            }
        }
        catch (Exception)
        {

            //// throw;
        }

    }

    protected void fillCropYear()
    {
        //ddlcropyear.Items.Insert(0, "Crop Year Not Indicated");
        ddlcropyear.Items.Insert(0, "2015-16");
        ddlcropyear.Items.Insert(1, "2014-15");
        ddlcropyear.Items.Insert(2, "2013-14");
        ddlcropyear.Items.Insert(3, "2012-13");
        ddlcropyear.Items.Insert(4, "2011-12");
        ddlcropyear.Items.Insert(5, "2010-11");
        ddlcropyear.Items.Insert(6, "2009-10");
        ddlcropyear.Items.Insert(7, "Before 2009");
        ddlcropyear.SelectedIndex = 0;
    }
    private void fillTransporter()
    {
        try
        {
            // string query = "SELECT Transporter_ID, Transporter_Name FROM MPSCSC.dbo.Transporter_Table WHERE IsActive = 'Y' order by Transporter_Name";
            string query = "SELECT Transporter_ID, Transporter_Name FROM MPSCSCSVR.MPSCSC.dbo.Transporter_Table WHERE IsActive = 'Y' order by Transporter_Name";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlTransporter.Items.Clear();
                ddlTransporter.DataSource = ds.Tables[0];
                ddlTransporter.DataTextField = "Transporter_Name";
                ddlTransporter.DataValueField = "Transporter_ID";
                ddlTransporter.DataBind();
            }
        }
        catch (Exception)
        {
        }
    }


    protected void FillFCIOTDeoptData()
    {
        try
        {
            string Dist_id = Session["Depot_DistID"].ToString();
            Dist_id = Dist_id.Substring(2, 2);
            string query = "";
            if (ddlArrival_Source.SelectedValue == "04")
            {

                query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Recd_Qty,No_of_Bags,s_name,Transporter FROM MPSCSCSVR.MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + ddlArrival_Source.SelectedValue + "'";
                // query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,No_of_Bags,s_name,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
            }
            else if (ddlArrival_Source.SelectedValue == "03")
            {
                //old online  query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,Recd_Qty,No_of_Bags,convert(varchar(10),arrival_date,103) as 'arrivaldate',Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter,Ds.District as 'District_Name',Dp.DepoName as 'DepotName' FROM MPSCSCSVR.MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSCSVR.MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No join MPSCSCSVR.MPSCSC.dbo.DepoCode as Ds on Rcpt.A_Dist=Ds.District_Code join MPSCSCSVR.MPSCSC.dbo.DepoCode as Dp on  Rcpt.A_Depo=Dp.DepoCode where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,Recd_Qty,No_of_Bags,convert(varchar(10),arrival_date,103) as 'arrivaldate',Rcpt.RO_No,convert(varchar(10),Rcpt.arrival_date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter,Dp.District as 'District_Name',Dp.DepoName as 'DepotName' FROM MPSCSCSVR.MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSCSVR.MPSCSC.dbo.DepoCode as Dp on  Rcpt.A_Depo=Dp.DepoCode where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + ddlArrival_Source.SelectedValue + "'";
                //offline  query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,No_of_Bags,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter,Ds.District as 'District_Name',Dp.DepoName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No join MPSCSC.dbo.DepoCode as Ds on Rcpt.A_Dist=Ds.District_Code join MPSCSC.dbo.DepoCode as Dp on  Rcpt.A_Depo=Dp.DepoCode where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
            }
            else if (ddlArrival_Source.SelectedValue == "02")
            {
                query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,No_of_Bags,A_Depo,A_Dist,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Transporter,Ds.District_Name,Dp.DepotName as 'DepotName' FROM MPSCSCSVR.MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSCSVR.MPSCSC.dbo.tbl_MetaData_DISTRICT as Ds on '23'+Rcpt.Dist_Id=Ds.District_Id join MPSCSCSVR.MPSCSC.dbo.tbl_MetaData_DEPOT as Dp on  Rcpt.Depot_ID=Dp.DepotID where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["Depot_DepotID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + ddlArrival_Source.SelectedValue + "'";
             //  query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,No_of_Bags,A_Depo,A_Dist,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Transporter,Ds.District_Name,Dp.DepotName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.tbl_MetaData_DISTRICT as Ds on '23'+Rcpt.Dist_Id=Ds.District_Id join MPSCSC.dbo.tbl_MetaData_DEPOT as Dp on  Rcpt.Depot_ID=Dp.DepotID where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["Depot_DepotID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + ddlArrival_Source.SelectedValue + "'";

            }
            else if (ddlArrival_Source.SelectedValue == "15")
            {
                //old online  query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,Recd_Qty,No_of_Bags,convert(varchar(10),arrival_date,103) as 'arrivaldate',Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter,Ds.District as 'District_Name',Dp.DepoName as 'DepotName' FROM MPSCSCSVR.MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSCSVR.MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No join MPSCSCSVR.MPSCSC.dbo.DepoCode as Ds on Rcpt.A_Dist=Ds.District_Code join MPSCSCSVR.MPSCSC.dbo.DepoCode as Dp on  Rcpt.A_Depo=Dp.DepoCode where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Recd_Qty,Recieved_Bags as  'No_of_Bags',s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSCSVR.MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt left join MPSCSCSVR.MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + ddlArrival_Source.SelectedValue + "'";
                //offline  query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,No_of_Bags,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter,Ds.District as 'District_Name',Dp.DepoName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No join MPSCSC.dbo.DepoCode as Ds on Rcpt.A_Dist=Ds.District_Code join MPSCSC.dbo.DepoCode as Dp on  Rcpt.A_Depo=Dp.DepoCode where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
            }
            else if (ddlArrival_Source.SelectedValue == "07")//From Rail Head
            {
                query = "SELECT distinct Truck_No,TC_Number,Commodity,Recd_Qty,Recd_Bags,Transporter_ID,Scheme  FROM MPSCSCSVR.MPSCSC.dbo.[RR_receipt_Depot] as Rcpt where  Rcpt.DepotID= '" + Session["WLC_Depot_ID"].ToString() + "' and Rcpt.TC_Number= '" + txtchallan.Value + "' ";
                //query = "SELECT distinct Truck_No,TC_Number,Commodity,Recd_Qty,Recd_Bags,Transporter_ID,Scheme  FROM MPSCSC.dbo.[RR_receipt_Depot] as Rcpt where Rcpt.district_code ='" + Session["WLC_Distt_ID"].ToString() + "' and Rcpt.DepotID= '" + Session["WLC_Depot_ID"].ToString() + "' and Rcpt.TC_Number= '" + Session["WLC_TC_Number"].ToString() + "' ";
            }
            else if (ddlArrival_Source.SelectedValue != null)
            {
                // query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,No_of_Bags,s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt left join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Recd_Qty,No_of_Bags,s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSCSVR.MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt left join MPSCSCSVR.MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + ddlArrival_Source.SelectedValue + "'";
                //  query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,No_of_Bags,s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt left join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" +'";

            }
           

            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count == 1)
            {
              //  txtTCNo.Text = ds.Tables[0].Rows[0]["challan_no"].ToString();
                txtchallan.Disabled = true;
                txttrucknum.Value = ds.Tables[0].Rows[0]["Vehile_no"].ToString();
                //txtTruckNo.Enabled = false;
                ddlCommodity.SelectedValue = ds.Tables[0].Rows[0]["Commodity"].ToString();
                ddlCommodity.Enabled = false;
                ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["Category"].ToString();
               // txtdod.Text = ds.Tables[0].Rows[0]["arrivaldate"].ToString();
                Session["arrivaldate"]=ds.Tables[0].Rows[0]["arrivaldate"].ToString();
                qtyrec.Value = ds.Tables[0].Rows[0]["Recd_Qty"].ToString();
                Session["Bagssent"] = ds.Tables[0].Rows[0]["No_of_Bags"].ToString();
                Session["QtySent"] = ds.Tables[0].Rows[0]["Recd_Qty"].ToString();
                //txtQtyDeposit.Enabled = false;
                txtnumbags.Value = ds.Tables[0].Rows[0]["No_of_Bags"].ToString();
               // txtBags.Text = ds.Tables[0].Rows[0]["No_of_Bags"].ToString();
                //txtnumbags.Enabled = false;
                ddlgodown.SelectedValue = ds.Tables[0].Rows[0]["Godown"].ToString();
               

                ddlTransporter.SelectedValue = ds.Tables[0].Rows[0]["Transporter"].ToString();
                ddlTransporter.Enabled = false;

                //if (Session["WLCDepSource"].ToString() == "04" || Session["WLCDepSource"].ToString() == "05")
                //{
                  
                  
                //}
                //else if (Session["WLCDepSource"].ToString() == "03")
                //{
                   
                 
                //}
                //else if (Session["WLCDepSource"].ToString() == "02")
                //{
                  
                //}
                //else
                //{
                 

                //}
                FillGodown();
               
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Invalid Record'); </script> ");
                Response.Redirect("WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record!" + "");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error FillFCIOTDeoptData has occured, try again'); </script> ");
        }
        finally
        {
            Con.Close();
        }
    }
    protected string getDate_MDY(string inDate)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

    }


    protected void btnsrch_Click(object sender, EventArgs e)
    {

        string query = "SELECT [Receipt_id] FROM mpscscsvr.[MPSCSC].[dbo].[tbl_Receipt_Details] where  Depot_ID='" + Session["Depot_DepotID"].ToString() + "' and S_of_arrival='" + ddlArrival_Source.SelectedValue + "' and challan_no='" + txtchallan.Value + "' and Godown in (select Godown_ID from dbo.tbl_MetaData_GODOWN where BranchID='" + Session["BranchId"].ToString() + "')";
       // string query = "SELECT [Receipt_id] FROM [MPSCSC].[dbo].[tbl_Receipt_Details] where  Depot_ID='" + Session["Depot_DepotID"].ToString() + "' and S_of_arrival='" + ddlArrival_Source.SelectedValue + "' and challan_no='" + txtchallan.Value + "' and Godown in (select Godown_ID from dbo.tbl_MetaData_GODOWN where BranchID='" + Session["BranchId"].ToString() + "')";

            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count == 1)
            {
                Session["WLC_Dep_Receipt_id"] = ds.Tables[0].Rows[0][0].ToString();
            }
            FillFCIOTDeoptData();
      
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
        if (Con.State == ConnectionState.Closed)
        {
            Con.Open();
        }
        qry = "insert into [Tbl_Mobile_Receiving] ([BranchId],[DepositorType],[DepositorID],[DepositFrom],[ChallanNo],[ReceiptID],[TruckNo],[Commodity],[DateofDeposit],[category],[CropYear],[Transpoter],[WCMNO],[MOWgt],[QtySent],[BagsSent],[QtyRec],[BagsRec],[Godown],[CreatedBy],[CreatedDate],[DateofReceipt],[Remark]) values ('" + Session["BranchId"].ToString() + "','" + ddldepositortype.SelectedValue.ToString() + "','" + ddlDepositor.SelectedValue.ToString() + "','" + ddlArrival_Source.SelectedValue.ToString() + "','" + txtchallan.Value + "','" + Session["WLC_Dep_Receipt_id"].ToString() + "','" + txttrucknum.Value + "','" + ddlCommodity.SelectedValue.ToString() + "','" + getDate_MDY(txtdod.Value) + "','" + ddlCategory.SelectedValue.ToString() + "','" + ddlcropyear.SelectedItem.Text + "','" + ddlTransporter.SelectedValue.ToString() + "','" + txtwcmno.Value + "','" + ddlmow.SelectedItem.Text + "','" + Convert.ToDecimal(Session["QtySent"].ToString()) + "','" + Convert.ToInt32(Session["Bagssent"].ToString()) + "','" + Convert.ToDecimal(qtyrec.Value) + "','" + Convert.ToInt32(txtnumbags.Value) + "','" + ddlgodown.SelectedValue.ToString() + "','" + ClientIP + "',getdate(),'" + getDate_MDY(Session["arrivaldate"].ToString()) + "','N')";
        cmd = new SqlCommand(qry, Con);
        int c = cmd.ExecuteNonQuery();
        if (Con.State == ConnectionState.Open)
        {
            Con.Close();
        }
        if (c > 0)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Inserted '); </script> ");
           
        }
    }
    //[WebMethod]
    //public static string InsertData(string DepositorType, string DepositorID, string QtyRec)
    //{
    //    string msg = string.Empty;
    //    using (SqlConnection con = new SqlConnection("Data Source=Suresh;Integrated Security=true;Initial Catalog=Intergrated_MP_STORAGE"))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("insert into Tbl_Mobile_Receiving(DepositorType,DepositorID,QtyRec) VALUES(@DepositorType,@DepositorID,@QtyRec)", con))
    //        {
    //            con.Open();
    //            cmd.Parameters.AddWithValue("@DepositorType", DepositorType);
    //            cmd.Parameters.AddWithValue("@DepositorID", DepositorID);
    //            cmd.Parameters.AddWithValue("@QtyRec", QtyRec);
    //            int i = cmd.ExecuteNonQuery();
    //            con.Close();
    //            if (i == 1)
    //            {
    //                msg = "true";
    //            }
    //            else
    //            {
    //                msg = "false";
    //            }
    //        }
    //    }
    //    return msg;
    //}
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("MReceivning.aspx");
    }
}