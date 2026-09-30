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
using System.Text;
using System.Data.SqlClient;
using System.Resources;

public partial class IssueCenterLevel_Storage_GatepassFrmMenualWHR : System.Web.UI.Page
{
    string scr = "";
    string ids = "";
    string gpr = "";
    string GPNo = "";
    SqlConnection _sqlCon = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());

    HttpCookie mCookie = null;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            

            if ((Request["src"] == "RO"))
            {

                lblDeparture.ControlStyle.Font.Strikeout = true;
                lblArrivalDepTime.ControlStyle.Font.Strikeout = false;
                //fillgrid();

                lblGatePass.Text = "Final Receipt GatePass";
                scr = Request["src"].ToString();
                ids = "";
                gpr = Request["vu"].ToString();
                GPNo = Request["vu"].ToString();
                _sqlCon.Open();
                SqlCommand _cmd = new SqlCommand();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
               // string gpass = Session["Gtpass"].ToString();
                _cmd.Connection = _sqlCon;
                _cmd.CommandText = "SELECT  ro.Release_Order_No as Challan_No,[Did],null as Scheme_Name,[DistId],CONVERT(varchar(20),[MenualWhrDeliveryDtl].CreatedDate,103) as Date,(select District_Name from dbo.tbl_MetaData_DISTRICT where tbl_MetaData_DISTRICT.District_Id=[MenualWhrDeliveryDtl].DistId) as District_Name,[MenualWhrDeliveryDtl].[BranchId],(select DepotName from dbo.tbl_MetaData_DEPOT where tbl_MetaData_DEPOT.BranchId=[MenualWhrDeliveryDtl].BranchId) as DepotName,[MenualWhrDeliveryDtl].[Gatepass_No] as GP_No,[MenualWhrDeliveryDtl].[GodownID],(select Godown_Name from dbo.tbl_MetaData_GODOWN where tbl_MetaData_GODOWN.Godown_ID=[MenualWhrDeliveryDtl].GodownID) as godown_name ,[MenualWhrDeliveryDtl].[Bags],[Weight],[MenualWhrDeliveryDtl].[MenualWHR],[Loss],[Gain],[MenualWhrDeliveryDtl].[Mid],(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where  tbl_MetaData_STORAGE_COMMODITY.Commodity_Id= ro.Commodity_Id) as Commodity_Name,ro.Truckno as Vehicle_No,(select Depositor_Name from dbo.tbl_MetaData_DEPOSITOR where tbl_MetaData_DEPOSITOR.Depositor_ID= mwhr.Depositor) as 'Depositor/Issuer_name' FROM [Intergrated_MP_STORAGE].[dbo].[MenualWhrDeliveryDtl] inner join dbo.tbl_RO_Details as ro on [MenualWhrDeliveryDtl].Gatepass_No=ro.GatePass_No and [MenualWhrDeliveryDtl].BranchId=ro.BranchID inner join [tbl_MenualWHR] as mwhr on [MenualWhrDeliveryDtl].Mid=mwhr.Mid and [MenualWhrDeliveryDtl].BranchId=mwhr.BranchID where  [MenualWhrDeliveryDtl].GatePass_No='" + GPNo + "'";
                _cmd.CommandType = CommandType.Text;

                
                da.SelectCommand = _cmd;
                da.Fill(ds, "temp");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["DepotName"].ToString() == "")
                    {
                        lbldepot1.Text = "........";
                    }
                    else
                    {
                        lbldepot1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["Date"].ToString() == "")
                    {
                        lblDate.Text = "........";
                    }
                    else
                    {
                        lblDate.Text = getDate_MDY(ds.Tables[0].Rows[0]["Date"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["District_Name"].ToString() == "")
                    {
                        lbldistrict1.Text = "........";
                    }
                    else
                    {
                        lbldistrict1.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                    }


                    if (ds.Tables[0].Rows[0]["GP_No"].ToString() == "")
                    {
                        lblSN.Text = "........";
                    }
                    else
                    {
                        lblSN.Text = ds.Tables[0].Rows[0]["GP_No"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["godown_name"].ToString() == "")
                    {
                        lblGodownNo1.Text = "........";
                    }
                    else
                    {
                        lblGodownNo1.Text = ds.Tables[0].Rows[0]["godown_name"].ToString();
                    }


                    //if (ds.Tables[0].Rows[0]["stack_name"].ToString() == "")
                    //{
                    //    lblStackno1.Text = "........";
                    //}
                    //else
                    //{
                    //    lblStackno1.Text = ds.Tables[0].Rows[0]["stack_name"].ToString();
                    //}

                    if (ds.Tables[0].Rows[0]["DepotName"].ToString() == "")
                    {

                        lblNameOfDepot.Text = "........";
                    }
                    else
                    {
                        lblNameOfDepot.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["Scheme_Name"].ToString() == "")
                    {
                        lblschemeName1.Text = "Non Scheme";
                    }
                    else
                    {
                        lblschemeName1.Text = ds.Tables[0].Rows[0]["Scheme_Name"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["Commodity_Name"].ToString() == "")
                    {
                        lblCommodityName1.Text = "........";
                    }
                    else
                    {
                        lblCommodityName1.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                    }


                    //if (ds.Tables[0].Rows[0]["Vehicle_Type"].ToString() == "")
                    //{
                    //    lblTypeofVehicle.Text = "........";
                    //}
                    //else
                    //{
                    //    lblTypeofVehicle.Text = ds.Tables[0].Rows[0]["Vehicle_Type"].ToString();
                    //}

                    if (ds.Tables[0].Rows[0]["Depositor/Issuer_name"].ToString() == "")
                    {
                        lblDepositorName1.Text = "........";
                    }
                    else
                    {
                        lblDepositorName1.Text = ds.Tables[0].Rows[0]["Depositor/Issuer_name"].ToString();
                    }

                    //if (ds.Tables[0].Rows[0]["Driver_Name"].ToString() == "")
                    //{
                    //    lblDriverName1.Text = "........";
                    //}
                    //else
                    //{
                    //    lblDriverName1.Text = ds.Tables[0].Rows[0]["Driver_Name"].ToString();
                    //}

                    if (ds.Tables[0].Rows[0]["Vehicle_No"].ToString() == "")
                    {
                        lblTruckNo.Text = "........";
                    }
                    else
                    {
                        lblTruckNo.Text = ds.Tables[0].Rows[0]["Vehicle_No"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["Bags"].ToString() == "")
                    {
                        lblNoOfBags1.Text = "........";
                    }
                    else
                    {
                        lblNoOfBags1.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["Weight"].ToString() == "")
                    {
                        lblweight1.Text = "........";
                    }
                    else
                    {
                        lblweight1.Text = ds.Tables[0].Rows[0]["Weight"].ToString();
                    }
                    //if (ds.Tables[0].Rows[0]["printed"].ToString().Trim() == "0" || ds.Tables[0].Rows[0]["printed"].ToString().Trim() == "NO")
                    //{
                    //    trDuplicate.Visible = false;
                    //}
                    //else
                    //{
                    //    trDuplicate.Visible = true;
                    //}


                    //if (ds.Tables[0].Rows[0]["License_No"].ToString() == "")
                    //{
                    //    lbllicense1.Text = "........";
                    //}
                    //else
                    //{
                    //    lbllicense1.Text = ds.Tables[0].Rows[0]["License_No"].ToString();
                    //}
                    //if (ds.Tables[0].Rows[0]["Valid_Upto"].ToString() == "")
                    //{
                    //    lblValid1.Text = "........";
                    //}
                    //else
                    //{
                    //    lblValid1.Text = ds.Tables[0].Rows[0]["Valid_Upto"].ToString();
                    //}
                    //if (ds.Tables[0].Rows[0]["Arrival_Dep_Time"].ToString() == "")
                    //{
                    //    lblArrivalDate.Text = "........";
                    //}
                    //else
                    //{
                    //    lblArrivalDate.Text = ds.Tables[0].Rows[0]["Arrival_Dep_Time"].ToString();
                    //}
                    //if (ds.Tables[0].Rows[0]["Remarks"].ToString() == "")
                    //{
                    //    Label10.Text = "........";
                    //}
                    //else
                    //{
                    //    Label10.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                    //}

                    //if (ds.Tables[0].Rows[0]["Status"].ToString().Trim() == "Cancel")
                    //{
                    //    imgcancelled.Visible = true;
                    //}
                    //else
                    //{
                    //    imgcancelled.Visible = false;
                    //}

                    //if (ds.Tables[0].Rows[0]["Miller_Name"].ToString().Trim() == "")
                    //{
                    //    lblMiller.Visible = false;
                    //    lblMillerName.Visible = false;
                    //    lblMillerName.Text = "........";
                    //}
                    //else
                    //{
                    //    lblMiller.Visible = true;
                    //    lblMillerName.Visible = true;
                    //    lblMillerName.Text = ds.Tables[0].Rows[0]["Miller_Name"].ToString().Trim();
                    //}
                    if (ds.Tables[0].Rows[0]["Challan_No"].ToString().Trim() == "")
                    {
                        lblTCNo1.Text = "...........";
                    }
                    else
                    {
                        lblTCNo1.Text = ds.Tables[0].Rows[0]["Challan_No"].ToString().Trim();
                    }
                    _sqlCon.Close();
                    _cmd.Dispose();


                }

            }

            else
            {
                Session["errDesc"] = "Sorry ,retry or login again";
                Server.Transfer("../../CustomError.aspx");
            }

        }
        catch (Exception ex)
        {

            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occured, try again" + "," + "Error:-" + ex.Message + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
            // lblmsg.ForeColor = System.Drawing.Color.Red;
            //lblmsg.Text = "Data could not be saved as some error has occurred ";
        }
    }
    protected void fillgrid()
    {
        try
        {

            SqlCommand _cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            _cmd.Connection = _sqlCon;
            _cmd.CommandText = "select godown_id as 'GodownName',stack_id as 'StackName' from tbl_Storage_GatePass_Enrty where gatepass_no='" + Convert.ToInt32(Request["gp"].ToString()) + "' ";
            _cmd.CommandType = CommandType.Text;
            da.SelectCommand = _cmd;
            da.Fill(ds, "temp");
            if (ds.Tables[0].Rows.Count > 0)
            {
                GDStack.Visible = true;
                trNormal.Visible = false;
                trFinal.Visible = true;
                GDStack.DataSource = ds.Tables[0];
                GDStack.DataBind();
            }
            _sqlCon.Close();
            _cmd.Dispose();
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occured, try again" + "," + "Error:-" + ex.Message + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
        finally
        {
            _sqlCon.Close();
        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
}
