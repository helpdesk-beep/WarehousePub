using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;

public partial class WarehouseApplication : System.Web.UI.MasterPage
{
    DataReader DObj = null;
    protected Common ComObj = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        ComObj = new Common(ConfigurationManager.AppSettings["connect_warehouse"].ToString());

        if (Session["lang"] != null)
        {

            if (Session["lang"].ToString() == "Hindi")
            {

                spanHome.InnerText = Resources.hindi.spanHome;
                hypHome.Text = Resources.hindi.hypHome;
                spanInitialization.InnerText = Resources.hindi.spanInitialization;
                hypOpeningBal.Text = Resources.hindi.hypOpeningBal;
                spanWarehouseOperations.InnerText = Resources.hindi.spanWarehouseOperations;
                hypRecieptDetails.Text = Resources.hindi.hypRecieptDetails;
                hypEditRecieptDetails.Text = Resources.hindi.hypEditRecieptDetails;
                hypDepositorForm.Text = Resources.hindi.hypDepositorForm;
                hypDeliveryGatePass.Text = Resources.hindi.hypDeliveryGatePass;
                hypPendingDeliveryGatePass.Text = Resources.hindi.hypPendingDeliveryGatePass;
                hypDeliveryOrder.Text = Resources.hindi.hypDeliveryOrder;
                hypQualityControl.Text = Resources.hindi.hypQualityControl;
                hypMadeUpBags.Text = Resources.hindi.hypMadeUpBags;
                spanMasters.InnerText = Resources.hindi.spanMasters;
                hypGodownMaster.Text = Resources.hindi.hypGodownMaster;
                hypStackMaster.Text = Resources.hindi.hypStackMaster;
                hypTransporterMaster.Text = Resources.hindi.hypTransporterMaster;
                hypDepositorMaster.Text = Resources.hindi.hypDepositorMaster;
                hypMillerMaster.Text = Resources.hindi.hypMillerMaster;
                hypDepotProfile.Text = Resources.hindi.hypDepotProfile;
                spanRoleManagement.InnerText = Resources.hindi.spanRoleManagement;
                hypAssignPage.Text = Resources.hindi.hypAssignPage;
                spanReports.InnerText = Resources.hindi.spanReports;
                hypDepotReports.Text = Resources.hindi.hypDepotReports;
                hypDepotReports.Text = Resources.hindi.hypDepotReports;
                hypRegionReports.Text = Resources.hindi.hypRegionReports;
                hypRegionReports.Text = Resources.hindi.hypRegionReports;
                hypStateReports.Text = Resources.hindi.hypStateReports;
                spanChangePassword.InnerText = Resources.hindi.spanChangePassword;
                hypChangePassword.Text = Resources.hindi.hypChangePassword;

            }

        }
        if (Session["UserName"] != null)
        {
            UxUserName.Text = Session["UserName"].ToString();
            if (Session["RoleId"].ToString() != "")
            {
                DataSet ds = GeTSelectedRole(CheckInt(Session["RoleId"].ToString()));
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drow in ds.Tables[0].Rows)
                        {
                            if (drow["Menu_Name"].ToString().Trim() == "Home")
                            {
                                divHome.Visible = true;
                                spanHome.Visible = true;
                                hypHome.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Initialization")
                            {
                                spanInitialization.Visible = true;
                                divIni.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Opening Balance")
                            {
                                hypOpeningBal.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Received Operations")
                            {
                                span_Rcpt_Details.Visible = true;
                                Div_Received.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Issued Operations")
                            {
                                divIssued.Visible = true;
                                spanIssued.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Warehouse Operations")
                            {
                                spanWarehouseOperations.Visible = true;
                                divWarehouseOpr.Visible = false;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Delete Operations")
                            {
                                div_Delete.Visible = true;
                                spn_delte.Visible = true;
                            }

                            else if (drow["Menu_Name"].ToString().Trim() == "Delete WHR")
                            {
                                spanWarehouseOperations.Visible = true;
                                divWarehouseOpr.Visible = false;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Delete Branch WHR")
                            {
                                spanWarehouseOperations.Visible = true;
                                divWarehouseOpr.Visible = false;
                                hlnkDeleteOprtrWhr.Visible = true;
                                hlnk_Delete_Open_Balance.Visible = true;
                                hlnk_DeleteDO.Visible = true;
                                hlnk_Delete_GP.Visible = true;
                            }
                            ////////////////////////////////////////////

                            else if (drow["Menu_Name"].ToString().Trim() == "Delete DO")
                            {
                                spanWarehouseOperations.Visible = true;
                                divWarehouseOpr.Visible = false;

                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Receipt Details")
                            {
                                hypRecieptDetails.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Edit Receipt Details")
                            {
                                hypEditRecieptDetails.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Depositor Form(WHR)")
                            {
                                hypDepositorForm.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Delivery GatePass")
                            {
                                hypDeliveryGatePass.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Pending GatePass of Delivery")
                            {
                                hypPendingDeliveryGatePass.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Delivery Order")
                            {
                                hypDeliveryOrder.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Quality Control Report")
                            {
                                hypQualityControl.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Made Up Bags")
                            {
                                hypMadeUpBags.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Masters")
                            {
                                spanMasters.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Godown Master")
                            {
                                hypGodownMaster.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Stack Master")
                            {
                                hypStackMaster.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Transporter Master")
                            {
                                hypTransporterMaster.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Depositor Master")
                            {
                                hypDepositorMaster.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Miller Master")
                            {
                                hypMillerMaster.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Depot Profile")
                            {
                                hypDepotProfile.Visible = false;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Role Management")
                            {
                                spanRoleManagement.Visible = true;
                                divRole.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Assign Pages To role")
                            {
                                hypAssignPage.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Reports")
                            {
                                spanReports.Visible = true;
                                divReports.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Depot Reports")
                            {
                                if (Session["UserName"].ToString() == "Chief Secretary" || Session["UserName"].ToString() == "Principal Secretary")
                                {
                                    hypDepotReports.Visible = false;
                                }
                                else
                                {
                                    hypDepotReports.Visible = true;
                                }
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Region Reports")
                            {
                                if (Session["UserName"].ToString() == "Chief Secretary" || Session["UserName"].ToString() == "Principal Secretary" || Session["UserName"].ToString() == "Commissioner Food")
                                {
                                    hypRegionReports.Visible = false;
                                }
                                else
                                {
                                    hypRegionReports.Visible = true;
                                }
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "State Reports")
                            {
                                hypStateReports.Visible = true;
                            }
                            else if (drow["Menu_Name"].ToString().Trim() == "Change Password")
                            {
                                divPass.Visible = true;
                                spanChangePassword.Visible = true;
                                hypChangePassword.Visible = true;
                            }

                           //added 12/10/2011 for commodity master
                            else if (drow["Menu_Name"].ToString().Trim() == "Commodity Master")
                            {
                                hypCommodityMaster.Visible = true;
                                hlnkratemaster.Visible = true;
                                hlnktaxmaster.Visible = true;
                                div_Delete.Visible = true;
                                spn_delte.Visible = true;
                                hlnkDeleteOprtrWhr.Visible = true;
                                hlnk_Delete_Open_Balance.Visible = true;
                                hlnk_DeleteDO.Visible = true;
                                hlnk_Delete_GP.Visible = true;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    private DataSet GeTSelectedRole(Int32 role)
    {
        DObj = new DataReader(ComObj);
        string qry = "select  rm.MenuItem_ID,m.Menu_Name from Role_MenuItem rm,MenuItem m where rm.MenuItem_ID= m.MenuItem_ID and rm.Role_ID=" + role + "";
        DataSet ds = DObj.selectAny(qry);
        return ds;
    }

    Int32 CheckInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        Int32 ValF = Int32.Parse(ValS);
        return ValF;
    }
}
