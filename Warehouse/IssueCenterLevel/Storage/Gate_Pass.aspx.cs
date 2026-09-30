using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Text;
using System.Resources;


public partial class IssueCenterLevel_Storage_Gate_Pass : System.Web.UI.Page
{
    string scr = "";
    string ids = "";
    string gpr = "";
    string GPNo = "";
    SqlConnection _sqlCon = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());

    HttpCookie mCookie = null;
    protected void Page_Init(object sender, EventArgs e)
    {
        
        

    }
   

    protected void Page_Load(object sender, EventArgs e)
    {
       
      
        try
        {
            if (Session["lang"].ToString() == "Hindi")
            {

                ResourceManager rm = ResourceManager.CreateFileBasedResourceManager("hindi", Server.MapPath("."), null);
                lblCGSWC.Text = Resources.hindi.lblCGSWC;
                //lblDuplicate.Text = Resources.hindi.lbld
                lblDepot.Text = Resources.hindi.lblDepot;
                lblDistrict.Text = Resources.hindi.lblDistrict;
                lblGatePass.Text = Resources.hindi.lblGatePass;
                lblSerialNo.Text = Resources.hindi.lblSerialNo;
                lblDate_GP.Text = Resources.hindi.lblDate_GP;
                lblNameDepot.Text = Resources.hindi.lblNameDepot;
                lblGodownNo.Text = Resources.hindi.lblGodownNo;
                lblStackNo.Text = Resources.hindi.lblStackNo;
                lblDepositorReceiverName.Text = Resources.hindi.lblDepositorReceiverName;
                lblCommodityName.Text = Resources.hindi.lblCommodityName;
                lblSchemeName.Text = Resources.hindi.lblSchemeName;
                lblTypeVehicle.Text = Resources.hindi.lblTypeVehicle;
                lblTruckNumber.Text = Resources.hindi.lblTruckNumber;
                lblDriverName.Text = Resources.hindi.lblDriverName;
                lblLicenseNo.Text = Resources.hindi.lblLicenseNo;
                lblValidUpto.Text = Resources.hindi.lblValidUpto;
                lblArrivalDepTime.Text = Resources.hindi.lblArrivalDepTime;
                lblNoofBags.Text = Resources.hindi.lblNoofBags;
                lblWeight.Text = Resources.hindi.lblWeight;
                lblDesc.Text = Resources.hindi.lblDesc;
                lblNameOfDepot.Text = Resources.hindi.lblNameDepot;
                lblSignGodwnIncharge.Text = Resources.hindi.lblSignGodwnIncharge;
                lblSignBranchManager.Text = Resources.hindi.lblSignBranchManager;
                lblSignTruckDriver.Text = Resources.hindi.lblSignTruckDriver;
                lblDeparture.Text = Resources.hindi.lblDeparture;
                lblTime.Text = Resources.hindi.lblTime;
                lblQltsKgsgms.Text = Resources.hindi.lblQltsKgsgms;

                lblTCNo.Text = Resources.hindi.lblTCNo;
                lblRemarks.Text = Resources.hindi.lblRemarks;

            }
           
                if ((Request["src"] == "MC" && Request["gp"] != null))
                {

                    lblDeparture.ControlStyle.Font.Strikeout = true;
                    lblArrivalDepTime.ControlStyle.Font.Strikeout = false;
                    fillgrid();

                    lblGatePass.Text = "Final Receipt GatePass";
                    scr = Request["src"].ToString();
                    ids = "";
                    gpr = Request["gp"].ToString();
                    GPNo = Request["gp"].ToString();
                    _sqlCon.Open();
                    SqlCommand _cmd = new SqlCommand();
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();
                    _cmd.Connection = _sqlCon;
                    _cmd.CommandText = "sp_getGatePass_Details";
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.Add("@issue_Source", SqlDbType.NChar, 10);
                    _cmd.Parameters["@issue_Source"].Value = scr;
                    _cmd.Parameters.Add("@issue_source_id", SqlDbType.NVarChar, 20);
                    _cmd.Parameters["@issue_source_id"].Value = ids;
                    _cmd.Parameters.Add("@GatePass_No", SqlDbType.NVarChar, 20);
                    _cmd.Parameters["@GatePass_No"].Value = gpr;

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


                        if (ds.Tables[0].Rows[0]["stack_name"].ToString() == "")
                        {
                            lblStackno1.Text = "........";
                        }
                        else
                        {
                            lblStackno1.Text = ds.Tables[0].Rows[0]["stack_name"].ToString();
                        }

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
                            //lblCommodityName1.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                        lblCommodityName1.Text = Session["WHMSCommodity"].ToString();
                    }


                        if (ds.Tables[0].Rows[0]["Vehicle_Type"].ToString() == "")
                        {
                            lblTypeofVehicle.Text = "........";
                        }
                        else
                        {
                            lblTypeofVehicle.Text = ds.Tables[0].Rows[0]["Vehicle_Type"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["Depositor/Issuer_name"].ToString() == "")
                        {
                            lblDepositorName1.Text = "........";
                        }
                        else
                        {
                            lblDepositorName1.Text = ds.Tables[0].Rows[0]["Depositor/Issuer_name"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["Driver_Name"].ToString() == "")
                        {
                            lblDriverName1.Text = "........";
                        }
                        else
                        {
                            lblDriverName1.Text = ds.Tables[0].Rows[0]["Driver_Name"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["Vehicle_No"].ToString() == "")
                        {
                            lblTruckNo.Text = "........";
                        }
                        else
                        {
                            lblTruckNo.Text = ds.Tables[0].Rows[0]["Vehicle_No"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["NO_of_Bage"].ToString() == "")
                        {
                            lblNoOfBags1.Text = "........";
                        }
                        else
                        {
                            lblNoOfBags1.Text = ds.Tables[0].Rows[0]["NO_of_Bage"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["Weight"].ToString() == "")
                        {
                            lblweight1.Text = "........";
                        }
                        else
                        {
                            lblweight1.Text = ds.Tables[0].Rows[0]["Weight"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["printed"].ToString().Trim() == "0" || ds.Tables[0].Rows[0]["printed"].ToString().Trim() == "NO")
                        {
                            trDuplicate.Visible = false;
                        }
                        else
                        {
                            trDuplicate.Visible = true;
                        }


                        if (ds.Tables[0].Rows[0]["License_No"].ToString() == "")
                        {
                            lbllicense1.Text = "........";
                        }
                        else
                        {
                            lbllicense1.Text = ds.Tables[0].Rows[0]["License_No"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["Valid_Upto"].ToString() == "")
                        {
                            lblValid1.Text = "........";
                        }
                        else
                        {
                            lblValid1.Text = ds.Tables[0].Rows[0]["Valid_Upto"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["Arrival_Dep_Time"].ToString() == "")
                        {
                            lblArrivalDate.Text = "........";
                        }
                        else
                        {
                            lblArrivalDate.Text = ds.Tables[0].Rows[0]["Arrival_Dep_Time"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["Remarks"].ToString() == "")
                        {
                            Label10.Text = "........";
                        }
                        else
                        {
                            Label10.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["Status"].ToString().Trim() == "Cancel")
                        {
                            imgcancelled.Visible = true;
                        }
                        else
                        {
                            imgcancelled.Visible = false;
                        }

                        if (ds.Tables[0].Rows[0]["Miller_Name"].ToString().Trim() == "")
                        {
                            lblMiller.Visible = false;
                            lblMillerName.Visible = false;
                            lblMillerName.Text = "........";
                        }
                        else
                        {
                            lblMiller.Visible = true;
                            lblMillerName.Visible = true;
                            lblMillerName.Text = ds.Tables[0].Rows[0]["Miller_Name"].ToString().Trim();
                        }
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


                else if ((Request["vu"] != "" && Request["src"] != "" && Request["id"] == null))//&&(Request["vu"].ToString() != "" && Request["src"].ToString() != "" && Request["id"].ToString() == ""))
                {
                    scr = Request["src"].ToString();
                    //ids = Request["id"].ToString();
                    gpr = "";
                    GPNo = Request["vu"].ToString();
                    //fillData();                                           
                    _sqlCon.Open();
                    SqlCommand _cmd = new SqlCommand();
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();
                    _cmd.Connection = _sqlCon;
                    //For Arrival 
                    if (Request["src"] == "MC")
                    {
                        lblDeparture.ControlStyle.Font.Strikeout = true;
                        lblArrivalDepTime.ControlStyle.Font.Strikeout = false;
                        _cmd.CommandText = "sp_getGatePass_PendingDetails";
                        _cmd.CommandType = CommandType.StoredProcedure;
                        _cmd.Parameters.Add("@issue_Source", SqlDbType.NChar, 10);
                        _cmd.Parameters["@issue_Source"].Value = Request["src"].ToString();
                        _cmd.Parameters.Add("@GatePass_no", SqlDbType.NVarChar, 20);
                        _cmd.Parameters["@GatePass_no"].Value = Request["vu"].ToString();
                    }
                    //For Delivery
                    else if (Request["src"] == "DO")
                    {
                        lblArrivalDepTime.ControlStyle.Font.Strikeout = true;
                        lblDeparture.ControlStyle.Font.Strikeout = false;
                        _cmd.CommandText = "sp_getGatePass_Details_DO";
                        _cmd.CommandType = CommandType.StoredProcedure;
                        _cmd.Parameters.Add("@issue_Source", SqlDbType.NChar, 10);
                        _cmd.Parameters["@issue_Source"].Value = scr;
                        //_cmd.Parameters.Add("@issue_source_id", SqlDbType.NVarChar, 20);
                        //_cmd.Parameters["@issue_source_id"].Value = ids;
                        _cmd.Parameters.Add("@GatePass_No", SqlDbType.NVarChar, 20);
                        _cmd.Parameters["@GatePass_No"].Value = Request["vu"].ToString();
                    }
                    else if (Request["src"] == "RO")
                    {
                        lblArrivalDepTime.ControlStyle.Font.Strikeout = true;
                        lblDeparture.ControlStyle.Font.Strikeout = false;
                        _cmd.CommandText = "sp_getGatePass_Details_RO";
                        _cmd.CommandType = CommandType.StoredProcedure;
                        _cmd.Parameters.Add("@issue_Source", SqlDbType.NChar, 10);
                        _cmd.Parameters["@issue_Source"].Value = scr;
                        //_cmd.Parameters.Add("@issue_source_id", SqlDbType.NVarChar, 20);
                        //_cmd.Parameters["@issue_source_id"].Value = ids;
                        _cmd.Parameters.Add("@GatePass_No", SqlDbType.NVarChar, 20);
                        _cmd.Parameters["@GatePass_No"].Value = Request["vu"].ToString();

                    }
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
                            lblDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
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
                            //lblSN.Text = ds.Tables[0].Rows[0]["gatepass_no"].ToString();  GP_No
                            lblSN.Text = ds.Tables[0].Rows[0]["GP_No"].ToString();
                        }
                        //if (ds.Tables[0].Rows[0]["FinancialYear"].ToString() == "")
                        //{
                        //    lblFinYear.Text = "";
                        //}
                        //else
                        //{
                        //    lblFinYear.Text = ds.Tables[0].Rows[0]["FinancialYear"].ToString() + "/";
                        //}
                        if (ds.Tables[0].Rows[0]["godown_name"].ToString() == "")
                        {
                            lblGodownNo1.Text = "........";
                        }
                        else
                        {
                            lblGodownNo1.Text = ds.Tables[0].Rows[0]["godown_name"].ToString();
                        }


                        if (ds.Tables[0].Rows[0]["stack_name"].ToString() == "")
                        {
                            lblStackno1.Text = "........";
                        }
                        else
                        {
                            lblStackno1.Text = ds.Tables[0].Rows[0]["stack_name"].ToString();
                        }

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


                        if (ds.Tables[0].Rows[0]["Vehicle_Type"].ToString() == "")
                        {
                            lblTypeofVehicle.Text = "........";
                        }
                        else
                        {
                            lblTypeofVehicle.Text = ds.Tables[0].Rows[0]["Vehicle_Type"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["Depositor/Issuer_name"].ToString() == "")
                        {
                            lblDepositorName1.Text = "........";
                        }
                        else
                        {
                            lblDepositorName1.Text = ds.Tables[0].Rows[0]["Depositor/Issuer_name"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["Driver_Name"].ToString() == "")
                        {
                            lblDriverName1.Text = "........";
                        }
                        else
                        {
                            lblDriverName1.Text = ds.Tables[0].Rows[0]["Driver_Name"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["Vehicle_No"].ToString() == "")
                        {
                            lblTruckNo.Text = "........";
                        }
                        else
                        {
                            lblTruckNo.Text = ds.Tables[0].Rows[0]["Vehicle_No"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["NO_of_Bage"].ToString() == "")
                        {
                            lblNoOfBags1.Text = "........";
                        }
                        else
                        {
                            lblNoOfBags1.Text = ds.Tables[0].Rows[0]["NO_of_Bage"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["Weight"].ToString() == "")
                        {
                            lblweight1.Text = "........";
                        }
                        else
                        {
                            lblweight1.Text = ds.Tables[0].Rows[0]["Weight"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["printed"].ToString().Trim() == "0" || ds.Tables[0].Rows[0]["printed"].ToString().Trim() == "NO")
                        {
                            trDuplicate.Visible = false;
                        }
                        else
                        {
                            trDuplicate.Visible = true;
                        }

                        //
                        if (ds.Tables[0].Rows[0]["License_No"].ToString() == "")
                        {
                            lbllicense1.Text = "........";
                        }
                        else
                        {
                            lbllicense1.Text = ds.Tables[0].Rows[0]["License_No"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["Valid_Upto"].ToString() == "")
                        {
                            lblValid1.Text = "........";
                        }
                        else
                        {
                            lblValid1.Text = ds.Tables[0].Rows[0]["Valid_Upto"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["Arrival_Dep_Time"].ToString() == "")
                        {
                            lblArrivalDate.Text = "........";
                        }
                        else
                        {
                            lblArrivalDate.Text = ds.Tables[0].Rows[0]["Arrival_Dep_Time"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["Remarks"].ToString() == "")
                        {
                            Label10.Text = "........";
                        }
                        else
                        {
                            Label10.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                        }

                        //to show image cancled if its status is cancel
                        if (ds.Tables[0].Rows[0]["Status"].ToString().Trim() == "Cancel")
                        {
                            imgcancelled.Visible = true;
                        }
                        else
                        {
                            imgcancelled.Visible = false;
                        }

                        if (ds.Tables[0].Rows[0]["Issue_Source"].ToString().Trim() == "MC")
                        {
                            lblGatePass.Text = "Receipt Gate Pass";
                        }
                        else if (ds.Tables[0].Rows[0]["Issue_Source"].ToString().Trim() == "DO")
                        {
                            lblGatePass.Text = "Delivery Gate Pass";
                        }
                        else if (ds.Tables[0].Rows[0]["Issue_Source"].ToString().Trim() == "RO")
                        {
                            lblGatePass.Text = "Delivery Gate Pass";
                        }
                        else
                        {
                            lblGatePass.Text = "Gate Pass";
                        }
                        if (ds.Tables[0].Rows[0]["Miller_Name"].ToString().Trim() == "")
                        {
                            lblMiller.Visible = false;
                            lblMillerName.Visible = false;
                            lblMillerName.Text = "........";
                        }
                        else
                        {
                            lblMiller.Visible = true;
                            lblMillerName.Visible = true;
                            lblMillerName.Text = ds.Tables[0].Rows[0]["Miller_Name"].ToString().Trim();
                        }
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


                else if ((Request["src"] != "" && Request["id"] != null && Request["vu"] == null))//&& (Request["src"].ToString() != "" && Request["id"].ToString() != "" && Request["vu"].ToString()==""))
                {
                    if (Request["src"] == "MC")
                    {
                        lblDeparture.ControlStyle.Font.Strikeout = true;
                        _sqlCon.Open();
                        SqlCommand _cmd = new SqlCommand();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter();
                        _cmd.Connection = _sqlCon;

                        _cmd.CommandText = "sp_getGatePass_Details";
                        _cmd.CommandType = CommandType.StoredProcedure;
                        _cmd.Parameters.Add("@issue_Source", SqlDbType.NChar, 10);
                        _cmd.Parameters["@issue_Source"].Value = Request["src"].ToString();
                        _cmd.Parameters.Add("@issue_source_id", SqlDbType.NVarChar, 20);
                        _cmd.Parameters["@issue_source_id"].Value = Request["id"].ToString();
                        _cmd.Parameters.Add("@GatePass_No", SqlDbType.NVarChar, 20);
                        _cmd.Parameters["@GatePass_No"].Value = "";
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
                                lblDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
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
                            //if (ds.Tables[0].Rows[0]["gatepass_no"].ToString() == "")
                            {
                                lblSN.Text = "........";
                            }
                            else
                            {
                                //lblSN.Text =  ds.Tables[0].Rows[0]["gatepass_no"].ToString();
                                lblSN.Text = ds.Tables[0].Rows[0]["GP_No"].ToString();
                                //lblSN.Text = ds.Tables[0].Rows[0]["FinancialYear"].ToString() + "/" + ds.Tables[0].Rows[0]["gatepass_no"].ToString();
                            }
                            //if (ds.Tables[0].Rows[0]["FinancialYear"].ToString() == "")
                            //{
                            //    lblFinYear.Text = "";
                            //}
                            //else
                            //{
                            //    lblFinYear.Text = ds.Tables[0].Rows[0]["FinancialYear"].ToString() + "/";
                            //}
                            if (ds.Tables[0].Rows[0]["godown_name"].ToString() == "")
                            {
                                lblGodownNo1.Text = "........";
                            }
                            else
                            {
                                lblGodownNo1.Text = ds.Tables[0].Rows[0]["godown_name"].ToString();
                            }


                            if (ds.Tables[0].Rows[0]["stack_name"].ToString() == "")
                            {
                                lblStackno1.Text = "........";
                            }
                            else
                            {
                                lblStackno1.Text = ds.Tables[0].Rows[0]["stack_name"].ToString();
                            }

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


                            if (ds.Tables[0].Rows[0]["Vehicle_Type"].ToString() == "")
                            {
                                lblTypeofVehicle.Text = "........";
                            }
                            else
                            {
                                lblTypeofVehicle.Text = ds.Tables[0].Rows[0]["Vehicle_Type"].ToString();
                            }

                            if (ds.Tables[0].Rows[0]["Depositor/Issuer_name"].ToString() == "")
                            {
                                lblDepositorName1.Text = "........";
                            }
                            else
                            {
                                lblDepositorName1.Text = ds.Tables[0].Rows[0]["Depositor/Issuer_name"].ToString();
                            }

                            if (ds.Tables[0].Rows[0]["Driver_Name"].ToString() == "")
                            {
                                lblDriverName1.Text = "........";
                            }
                            else
                            {
                                lblDriverName1.Text = ds.Tables[0].Rows[0]["Driver_Name"].ToString();
                            }

                            if (ds.Tables[0].Rows[0]["Vehicle_No"].ToString() == "")
                            {
                                lblTruckNo.Text = "........";
                            }
                            else
                            {
                                lblTruckNo.Text = ds.Tables[0].Rows[0]["Vehicle_No"].ToString();
                            }

                            if (ds.Tables[0].Rows[0]["NO_of_Bage"].ToString() == "")
                            {
                                lblNoOfBags1.Text = "........";
                            }
                            else
                            {
                                lblNoOfBags1.Text = ds.Tables[0].Rows[0]["NO_of_Bage"].ToString();
                            }

                            if (ds.Tables[0].Rows[0]["Weight"].ToString() == "")
                            {
                                lblweight1.Text = "........";
                            }
                            else
                            {
                                lblweight1.Text = ds.Tables[0].Rows[0]["Weight"].ToString();
                            }
                            if (ds.Tables[0].Rows[0]["printed"].ToString().Trim() == "0" || ds.Tables[0].Rows[0]["printed"].ToString().Trim() == "NO")
                            {
                                trDuplicate.Visible = false;
                            }
                            else
                            {
                                trDuplicate.Visible = true;
                            }

                            //
                            if (ds.Tables[0].Rows[0]["License_No"].ToString() == "")
                            {
                                lbllicense1.Text = "........";
                            }
                            else
                            {
                                lbllicense1.Text = ds.Tables[0].Rows[0]["License_No"].ToString();
                            }
                            if (ds.Tables[0].Rows[0]["Valid_Upto"].ToString() == "")
                            {
                                lblValid1.Text = "........";
                            }
                            else
                            {
                                lblValid1.Text = ds.Tables[0].Rows[0]["Valid_Upto"].ToString();
                            }
                            if (ds.Tables[0].Rows[0]["Arrival_Dep_Time"].ToString() == "")
                            {
                                lblArrivalDate.Text = "........";
                            }
                            else
                            {
                                lblArrivalDate.Text = ds.Tables[0].Rows[0]["Arrival_Dep_Time"].ToString();
                            }
                            if (ds.Tables[0].Rows[0]["Remarks"].ToString() == "")
                            {
                                Label10.Text = "........";
                            }
                            else
                            {
                                Label10.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                            }

                            //to show image cancled if its status is cancel
                            if (ds.Tables[0].Rows[0]["Status"].ToString().Trim() == "Cancel")
                            {
                                imgcancelled.Visible = true;
                            }
                            else
                            {
                                imgcancelled.Visible = false;
                            }

                            if (ds.Tables[0].Rows[0]["Issue_Source"].ToString().Trim() == "MC")
                            {
                                lblGatePass.Text = "Receipt Gate Pass";
                            }
                            else if (ds.Tables[0].Rows[0]["Issue_Source"].ToString().Trim() == "DO")
                            {
                                lblGatePass.Text = "Delivery Gate Pass";
                            }
                            else
                            {
                                lblGatePass.Text = "Gate Pass";
                            }
                            if (ds.Tables[0].Rows[0]["Miller_Name"].ToString().Trim() == "")
                            {
                                lblMiller.Visible = false;
                                lblMillerName.Visible = false;
                                lblMillerName.Text = "........";
                            }
                            else
                            {
                                lblMiller.Visible = true;
                                lblMillerName.Visible = true;
                                lblMillerName.Text = ds.Tables[0].Rows[0]["Miller_Name"].ToString().Trim();
                            }
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
                }
                else
                {
                    Session["errDesc"] = "Sorry ,retry or login again";
                    Server.Transfer("../../CustomError.aspx");
                }

                //if (Request["src"] != "" && Request["id"] != null)
                //{
                //    GDStack.DataSource = null;
                //    GDStack.DataBind();
                //    GDStack.Visible = false;
                //    trNormal.Visible = true;
                //    trFinal.Visible = false;
                //    scr = Request["src"].ToString();
                //    //ids = Request["id"].ToString();
                //    gpr = "";
                //    //fillData();                                           
                //    _sqlCon.Open();
                //    SqlCommand _cmd = new SqlCommand();
                //    DataSet ds = new DataSet();
                //    SqlDataAdapter da = new SqlDataAdapter();
                //    _cmd.Connection = _sqlCon;
                //    //_cmd.CommandText = "sp_getGatePass_Details";

                //    if (Request["src"] == "DO" && Request["vu"] != "")
                //    {
                //        _cmd.CommandText = "sp_getGatePass_Details_DO";
                //        _cmd.CommandType = CommandType.StoredProcedure;
                //        _cmd.Parameters.Add("@issue_Source", SqlDbType.NChar, 10);
                //        _cmd.Parameters["@issue_Source"].Value = scr;
                //        _cmd.Parameters.Add("@issue_source_id", SqlDbType.NVarChar, 20);
                //        _cmd.Parameters["@issue_source_id"].Value = ids;

                //    }
                //    else if (Request["src"] == "MC")
                //    {
                //        _cmd.CommandText = "sp_getGatePass_Details";
                //        _cmd.CommandType = CommandType.StoredProcedure;
                //        _cmd.Parameters.Add("@issue_Source", SqlDbType.NChar, 10);
                //        _cmd.Parameters["@issue_Source"].Value = scr;
                //        _cmd.Parameters.Add("@issue_source_id", SqlDbType.NVarChar, 20);
                //        _cmd.Parameters["@issue_source_id"].Value = ids;
                //        _cmd.Parameters.Add("@GatePass_No", SqlDbType.NVarChar, 20);
                //        _cmd.Parameters["@GatePass_No"].Value = Request["vu"].ToString();
                //    }
                //    //dr = _cmd1.ExecuteReader();
                //    da.SelectCommand = _cmd;
                //    da.Fill(ds, "temp");
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        if (ds.Tables[0].Rows[0]["DepotName"].ToString() == "")
                //        {
                //            lbldepot1.Text = "........";
                //        }
                //        else
                //        {
                //            lbldepot1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                //        }
                //        if (ds.Tables[0].Rows[0]["Date"].ToString() == "")
                //        {
                //            lblDate.Text = "........";
                //        }
                //        else
                //        {
                //            lblDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
                //        }
                //        if (ds.Tables[0].Rows[0]["District_Name"].ToString() == "")
                //        {
                //            lbldistrict1.Text = "........";
                //        }
                //        else
                //        {
                //            lbldistrict1.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                //        }


                //        if (ds.Tables[0].Rows[0]["gatepass_no"].ToString() == "")
                //        {
                //            lblSN.Text = "........";
                //        }
                //        else
                //        {
                //            lblSN.Text = ds.Tables[0].Rows[0]["gatepass_no"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["godown_name"].ToString() == "")
                //        {
                //            lblGodownNo1.Text = "........";
                //        }
                //        else
                //        {
                //            lblGodownNo1.Text = ds.Tables[0].Rows[0]["godown_name"].ToString();
                //        }


                //        if (ds.Tables[0].Rows[0]["stack_name"].ToString() == "")
                //        {
                //            lblStackno1.Text = "........";
                //        }
                //        else
                //        {
                //            lblStackno1.Text = ds.Tables[0].Rows[0]["stack_name"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["DepotName"].ToString() == "")
                //        {

                //            lblNameOfDepot.Text = "........";
                //        }
                //        else
                //        {
                //            lblNameOfDepot.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["Scheme_Name"].ToString() == "")
                //        {
                //            lblschemeName1.Text = "........";
                //        }
                //        else
                //        {
                //            lblschemeName1.Text = ds.Tables[0].Rows[0]["Scheme_Name"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["Commodity_Name"].ToString() == "")
                //        {
                //            lblCommodityName1.Text = "........";
                //        }
                //        else
                //        {
                //            lblCommodityName1.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                //        }


                //        if (ds.Tables[0].Rows[0]["Vehicle_Type"].ToString() == "")
                //        {
                //            lblTypeofVehicle.Text = "........";
                //        }
                //        else
                //        {
                //            lblTypeofVehicle.Text = ds.Tables[0].Rows[0]["Vehicle_Type"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["Depositor/Issuer_name"].ToString() == "")
                //        {
                //            lblDepositorName1.Text = "........";
                //        }
                //        else
                //        {
                //            lblDepositorName1.Text = ds.Tables[0].Rows[0]["Depositor/Issuer_name"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["Driver_Name"].ToString() == "")
                //        {
                //            lblDriverName1.Text = "........";
                //        }
                //        else
                //        {
                //            lblDriverName1.Text = ds.Tables[0].Rows[0]["Driver_Name"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["Vehicle_No"].ToString() == "")
                //        {
                //            lblTruckNo.Text = "........";
                //        }
                //        else
                //        {
                //            lblTruckNo.Text = ds.Tables[0].Rows[0]["Vehicle_No"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["NO_of_Bage"].ToString() == "")
                //        {
                //            lblNoOfBags1.Text = "........";
                //        }
                //        else
                //        {
                //            lblNoOfBags1.Text = ds.Tables[0].Rows[0]["NO_of_Bage"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["Weight"].ToString() == "")
                //        {
                //            lblweight1.Text = "........";
                //        }
                //        else
                //        {
                //            lblweight1.Text = ds.Tables[0].Rows[0]["Weight"].ToString();
                //        }
                //        if (ds.Tables[0].Rows[0]["printed"].ToString().Trim() == "0" || ds.Tables[0].Rows[0]["printed"].ToString().Trim() == "NO")
                //        {
                //            trDuplicate.Visible = false;
                //        }
                //        else
                //        {
                //            trDuplicate.Visible = true;
                //        }

                //        //
                //        if (ds.Tables[0].Rows[0]["License_No"].ToString() == "")
                //        {
                //            lbllicense1.Text = "........";
                //        }
                //        else
                //        {
                //            lbllicense1.Text = ds.Tables[0].Rows[0]["License_No"].ToString();
                //        }
                //        if (ds.Tables[0].Rows[0]["Valid_Upto"].ToString() == "")
                //        {
                //            lblValid1.Text = "........";
                //        }
                //        else
                //        {
                //            lblValid1.Text = ds.Tables[0].Rows[0]["Valid_Upto"].ToString();
                //        }
                //        if (ds.Tables[0].Rows[0]["Arrival_Dep_Time"].ToString() == "")
                //        {
                //            lblArrivalDate.Text = "........";
                //        }
                //        else
                //        {
                //            lblArrivalDate.Text = ds.Tables[0].Rows[0]["Arrival_Dep_Time"].ToString();
                //        }
                //        if (ds.Tables[0].Rows[0]["Remarks"].ToString() == "")
                //        {
                //            Label10.Text = "........";
                //        }
                //        else
                //        {
                //            Label10.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                //        }
                //       
                //        //to show image cancled if its status is cancel
                //        if (ds.Tables[0].Rows[0]["Status"].ToString().Trim() == "Cancel")
                //        {
                //            imgcancelled.Visible = true;
                //        }
                //        else
                //        {
                //            imgcancelled.Visible = false;
                //        }

                //        if (ds.Tables[0].Rows[0]["Issue_Source"].ToString().Trim() == "MC")
                //        {
                //            lblGatePass.Text = "Receipt Gate Pass";
                //        }
                //        else if (ds.Tables[0].Rows[0]["Issue_Source"].ToString().Trim() == "DO")
                //        {
                //            lblGatePass.Text = "Delivery Gate Pass";
                //        }
                //        else
                //        {
                //            lblGatePass.Text = "Gate Pass";
                //        }
                //        if (ds.Tables[0].Rows[0]["Miller_Name"].ToString().Trim() == "")
                //        {
                //            lblMiller.Visible = false;
                //            lblMillerName.Visible = false;
                //            lblMillerName.Text = "........";
                //        }
                //        else
                //        {
                //            lblMiller.Visible = true;
                //            lblMillerName.Visible = true;
                //            lblMillerName.Text = ds.Tables[0].Rows[0]["Miller_Name"].ToString().Trim();
                //        }
                //        _sqlCon.Close();
                //        _cmd.Dispose();
                //        //ds.Dispose();

                //    }
                //}
                //else if (Request["src"] == "MC" && Request["vu"] != null)
                //{
                //    GDStack.DataSource = null;
                //    GDStack.DataBind();
                //    GDStack.Visible = false;
                //    trNormal.Visible = true;
                //    trFinal.Visible = false;
                //    _sqlCon.Open();
                //    SqlCommand _cmd = new SqlCommand();
                //    DataSet ds = new DataSet();
                //    SqlDataAdapter da = new SqlDataAdapter();
                //    _cmd.Connection = _sqlCon;

                //    _cmd.CommandText = "sp_getGatePass_PendingDetails";
                //    _cmd.CommandType = CommandType.StoredProcedure;
                //    _cmd.Parameters.Add("@issue_Source", SqlDbType.NChar, 10);
                //    _cmd.Parameters["@issue_Source"].Value = Request["src"].ToString();
                //    _cmd.Parameters.Add("@GatePass_no", SqlDbType.NVarChar, 20);
                //    _cmd.Parameters["@GatePass_no"].Value = Request["vu"].ToString();

                //    da.SelectCommand = _cmd;
                //    da.Fill(ds, "temp");
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        if (ds.Tables[0].Rows[0]["Date"].ToString() == "")
                //        {
                //            lblDate.Text = "........";
                //        }
                //        else
                //        {
                //            lblDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
                //        }
                //        if (ds.Tables[0].Rows[0]["printed"].ToString().Trim() == "NO" || ds.Tables[0].Rows[0]["printed"].ToString().Trim() == "0")
                //        {
                //            trDuplicate.Visible = false;
                //        }
                //        else
                //        {
                //            trDuplicate.Visible = true;
                //        }
                //        if (ds.Tables[0].Rows[0]["DepotName"].ToString() == "")
                //        {
                //            lbldepot1.Text = "........";
                //        }
                //        else
                //        {
                //            lbldepot1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["District_Name"].ToString() == "")
                //        {
                //            lbldistrict1.Text = "........";
                //        }
                //        else
                //        {
                //            lbldistrict1.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                //        }


                //        if (ds.Tables[0].Rows[0]["gatepass_no"].ToString() == "")
                //        {
                //            lblSN.Text = "........";
                //        }
                //        else
                //        {
                //            lblSN.Text = ds.Tables[0].Rows[0]["gatepass_no"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["godown_name"].ToString() == "")
                //        {
                //            lblGodownNo1.Text = "........";
                //        }
                //        else
                //        {
                //            lblGodownNo1.Text = ds.Tables[0].Rows[0]["godown_name"].ToString();
                //        }


                //        if (ds.Tables[0].Rows[0]["stack_name"].ToString() == "")
                //        {
                //            lblStackno1.Text = "........";
                //        }
                //        else
                //        {
                //            lblStackno1.Text = ds.Tables[0].Rows[0]["stack_name"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["DepotName"].ToString() == "")
                //        {

                //            lblNameOfDepot.Text = "........";
                //        }
                //        else
                //        {
                //            lblNameOfDepot.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["Scheme_Name"].ToString() == "")
                //        {
                //            lblschemeName1.Text = "........";
                //        }
                //        else
                //        {
                //            lblschemeName1.Text = ds.Tables[0].Rows[0]["Scheme_Name"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["Commodity_Name"].ToString() == "")
                //        {
                //            lblCommodityName1.Text = "........";
                //        }
                //        else
                //        {
                //            lblCommodityName1.Text = ds.Tables[0].Rows[0]["Commodity_Name"].ToString();
                //        }


                //        if (ds.Tables[0].Rows[0]["Vehicle_Type"].ToString() == "")
                //        {
                //            lblTypeofVehicle.Text = "........";
                //        }
                //        else
                //        {
                //            lblTypeofVehicle.Text = ds.Tables[0].Rows[0]["Vehicle_Type"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["Depositor/Issuer_name"].ToString() == "")
                //        {
                //            lblDepositorName1.Text = "........";
                //        }
                //        else
                //        {
                //            lblDepositorName1.Text = ds.Tables[0].Rows[0]["Depositor/Issuer_name"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["Driver_Name"].ToString() == "")
                //        {
                //            lblDriverName1.Text = "........";
                //        }
                //        else
                //        {
                //            lblDriverName1.Text = ds.Tables[0].Rows[0]["Driver_Name"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["Vehicle_No"].ToString() == "")
                //        {
                //            lblTruckNo.Text = "........";
                //        }
                //        else
                //        {
                //            lblTruckNo.Text = ds.Tables[0].Rows[0]["Vehicle_No"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["NO_of_Bage"].ToString() == "")
                //        {
                //            lblNoOfBags1.Text = "........";
                //        }
                //        else
                //        {
                //            lblNoOfBags1.Text = ds.Tables[0].Rows[0]["NO_of_Bage"].ToString();
                //        }

                //        if (ds.Tables[0].Rows[0]["Weight"].ToString() == "")
                //        {
                //            lblweight1.Text = "........";
                //        }
                //        else
                //        {
                //            lblweight1.Text = ds.Tables[0].Rows[0]["Weight"].ToString();
                //        }

                //        //
                //        if (ds.Tables[0].Rows[0]["License_No"].ToString() == "")
                //        {
                //            lbllicense1.Text = "........";
                //        }
                //        else
                //        {
                //            lbllicense1.Text = ds.Tables[0].Rows[0]["License_No"].ToString();
                //        }
                //        if (ds.Tables[0].Rows[0]["Valid_Upto"].ToString() == "")
                //        {
                //            lblValid1.Text = "........";
                //        }
                //        else
                //        {
                //            lblValid1.Text = ds.Tables[0].Rows[0]["Valid_Upto"].ToString();
                //        }
                //        if (ds.Tables[0].Rows[0]["Arrival_Dep_Time"].ToString() == "")
                //        {
                //            lblArrivalDate.Text = "........";
                //        }
                //        else
                //        {
                //            lblArrivalDate.Text = ds.Tables[0].Rows[0]["Arrival_Dep_Time"].ToString();
                //        }
                //        if (ds.Tables[0].Rows[0]["Remarks"].ToString() == "")
                //        {
                //            Label10.Text = "........";
                //            //txtRemarks.Text="........";
                //        }
                //        else
                //        {
                //            Label10.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                //            //txtRemarks.Text=ds.Tables[0].Rows[0]["Remarks"].ToString();
                //        }

                //        
                //        //to show image cancled if its status is cancel
                //        if (ds.Tables[0].Rows[0]["Status"].ToString().Trim() == "Cancel")
                //        {
                //            imgcancelled.Visible = true;
                //        }
                //        else
                //        {
                //            imgcancelled.Visible = false;
                //        }

                //        if (ds.Tables[0].Rows[0]["Issue_Source"].ToString().Trim() == "MC")
                //        {
                //            lblGatePass.Text = "Receipt Gate Pass";
                //        }
                //        else if (ds.Tables[0].Rows[0]["Issue_Source"].ToString().Trim() == "DO")
                //        {
                //            lblGatePass.Text = "Delivery Gate Pass";
                //        }
                //        else
                //        {
                //            lblGatePass.Text = "Gate Pass";
                //        }
                //        if (ds.Tables[0].Rows[0]["Miller_Name"].ToString().Trim() == "")
                //        {
                //            lblMiller.Visible = false;
                //            lblMillerName.Visible = false;
                //            lblMillerName.Text = "........";
                //        }
                //        else
                //        {
                //            lblMiller.Visible = true;
                //            lblMillerName.Visible = true;
                //            lblMillerName.Text = ds.Tables[0].Rows[0]["Miller_Name"].ToString().Trim();
                //        }
                //        _sqlCon.Close();
                //        _cmd.Dispose();

                //    }

                //}
                /*else if (Request["src"] == "DO" && Request["vu"] != "")
                {
                    _sqlCon.Open();
                    SqlCommand _cmd = new SqlCommand();
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();
                    _cmd.Connection = _sqlCon;

                    _cmd.CommandText = "sp_getGatePass_Details_DO";
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.Add("@issue_Source", SqlDbType.NChar, 10);
                    _cmd.Parameters["@issue_Source"].Value = scr;
                    //_cmd.Parameters.Add("@issue_source_id", SqlDbType.NVarChar, 20);
                    //_cmd.Parameters["@issue_source_id"].Value = ids;
                    _cmd.Parameters.Add("@GatePass_No", SqlDbType.NVarChar, 20);
                    _cmd.Parameters["@GatePass_No"].Value = Request["vu"].ToString();

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
                            lblDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["District_Name"].ToString() == "")
                        {
                            lbldistrict1.Text = "........";
                        }
                        else
                        {
                            lbldistrict1.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                        }


                        if (ds.Tables[0].Rows[0]["gatepass_no"].ToString() == "")
                        {
                            lblSN.Text = "........";
                        }
                        else
                        {
                            lblSN.Text = ds.Tables[0].Rows[0]["gatepass_no"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["godown_name"].ToString() == "")
                        {
                            lblGodownNo1.Text = "........";
                        }
                        else
                        {
                            lblGodownNo1.Text = ds.Tables[0].Rows[0]["godown_name"].ToString();
                        }


                        if (ds.Tables[0].Rows[0]["stack_name"].ToString() == "")
                        {
                            lblStackno1.Text = "........";
                        }
                        else
                        {
                            lblStackno1.Text = ds.Tables[0].Rows[0]["stack_name"].ToString();
                        }

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
                            lblschemeName1.Text = "........";
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


                        if (ds.Tables[0].Rows[0]["Vehicle_Type"].ToString() == "")
                        {
                            lblTypeofVehicle.Text = "........";
                        }
                        else
                        {
                            lblTypeofVehicle.Text = ds.Tables[0].Rows[0]["Vehicle_Type"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["Depositor/Issuer_name"].ToString() == "")
                        {
                            lblDepositorName1.Text = "........";
                        }
                        else
                        {
                            lblDepositorName1.Text = ds.Tables[0].Rows[0]["Depositor/Issuer_name"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["Driver_Name"].ToString() == "")
                        {
                            lblDriverName1.Text = "........";
                        }
                        else
                        {
                            lblDriverName1.Text = ds.Tables[0].Rows[0]["Driver_Name"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["Vehicle_No"].ToString() == "")
                        {
                            lblTruckNo.Text = "........";
                        }
                        else
                        {
                            lblTruckNo.Text = ds.Tables[0].Rows[0]["Vehicle_No"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["NO_of_Bage"].ToString() == "")
                        {
                            lblNoOfBags1.Text = "........";
                        }
                        else
                        {
                            lblNoOfBags1.Text = ds.Tables[0].Rows[0]["NO_of_Bage"].ToString();
                        }

                        if (ds.Tables[0].Rows[0]["Weight"].ToString() == "")
                        {
                            lblweight1.Text = "........";
                        }
                        else
                        {
                            lblweight1.Text = ds.Tables[0].Rows[0]["Weight"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["printed"].ToString().Trim() == "0" || ds.Tables[0].Rows[0]["printed"].ToString().Trim() == "NO")
                        {
                            trDuplicate.Visible = false;
                        }
                        else
                        {
                            trDuplicate.Visible = true;
                        }

                        //
                        if (ds.Tables[0].Rows[0]["License_No"].ToString() == "")
                        {
                            lbllicense1.Text = "........";
                        }
                        else
                        {
                            lbllicense1.Text = ds.Tables[0].Rows[0]["License_No"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["Valid_Upto"].ToString() == "")
                        {
                            lblValid1.Text = "........";
                        }
                        else
                        {
                            lblValid1.Text = ds.Tables[0].Rows[0]["Valid_Upto"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["Arrival_Dep_Time"].ToString() == "")
                        {
                            lblArrivalDate.Text = "........";
                        }
                        else
                        {
                            lblArrivalDate.Text = ds.Tables[0].Rows[0]["Arrival_Dep_Time"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["Remarks"].ToString() == "")
                        {
                            Label10.Text = "........";
                        }
                        else
                        {
                            Label10.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                        }
                       
                        //to show image cancled if its status is cancel
                        if (ds.Tables[0].Rows[0]["Status"].ToString().Trim() == "Cancel")
                        {
                            imgcancelled.Visible = true;
                        }
                        else
                        {
                            imgcancelled.Visible = false;
                        }

                        if (ds.Tables[0].Rows[0]["Issue_Source"].ToString().Trim() == "MC")
                        {
                            lblGatePass.Text = "Receipt Gate Pass";
                        }
                        else if (ds.Tables[0].Rows[0]["Issue_Source"].ToString().Trim() == "DO")
                        {
                            lblGatePass.Text = "Delivery Gate Pass";
                        }
                        else
                        {
                            lblGatePass.Text = "Gate Pass";
                        }
                        if (ds.Tables[0].Rows[0]["Miller_Name"].ToString().Trim() == "")
                        {
                            lblMiller.Visible = false;
                            lblMillerName.Visible = false;
                            lblMillerName.Text = "........";
                        }
                        else
                        {
                            lblMiller.Visible = true;
                            lblMillerName.Visible = true;
                            lblMillerName.Text = ds.Tables[0].Rows[0]["Miller_Name"].ToString().Trim();
                        }
                        _sqlCon.Close();
                        _cmd.Dispose();
                        //ds.Dispose();

                    }
                }//*/
           
            
            
      
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
    
    protected void btnclk_Click(object sender, EventArgs e)
    {
        try
        {
            
            //only Delivery GP can be updated with Printed Status
            if(GPNo.ToString() !="")
            {
               
                _sqlCon.Open();
                SqlCommand _cmd = new SqlCommand();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                _cmd.Connection = _sqlCon;
              
                //_cmd.CommandText = "update tbl_Storage_GatePass_Enrty set printed='YES' where gatepass_no='" + Convert.ToInt64(GPNo.ToString()) + "' ";
                _cmd.CommandText = "update tbl_Storage_GatePass_Enrty set printed='YES' where gatepass_no='" + (GPNo.ToString()) + "' ";
                _cmd.CommandType = CommandType.Text;
                _cmd.ExecuteReader();
                _sqlCon.Close();
                _cmd.Dispose();
                   
              
                btnclk.Visible = false;
            }
         
            string str = "<script language='javascript'>window.print();document.getElementById('IMG1').style.display='None';</Script>";
            RegisterStartupScript("str", str);
           
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
    protected void ipbtn_ServerClick(object sender, EventArgs e)
    {

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
