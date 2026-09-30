using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

public partial class MasterPage_Gdwn : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_start.Text = System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");

        if (!string.IsNullOrEmpty(Session["Depot_DepotID"] as string))
        {

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
                    //hypPendingDeliveryGatePass.Text = Resources.hindi.hypPendingDeliveryGatePass;
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
                    //spanChangePassword.InnerText = Resources.hindi.spanChangePassword;
                    //hypChangePassword.Text = Resources.hindi.hypChangePassword;

                }
                //string midtime = System.DateTime.Now.TimeOfDay.Milliseconds.ToString();
            }
            string branchtype = Session["BranchType"].ToString();
            if (branchtype == "I" || branchtype == "O" || branchtype == "8" || branchtype == "9" || branchtype == "1")
            {

                hypGodownMaster.Visible = true;
                divAcc.Visible = true;
                divNAFED.Visible = true;
                divNCCF.Visible = true;
                divMARKFED.Visible = true;
            }
            else
            {
                hypGodownMaster.Visible = false;

            }
            if (Session["UserName"] != null)
            {
                UxUserName.Text = Session["UserName"].ToString();
            }
            else
            {
                Response.Redirect("../login.aspx ");
            }
            //string endtime = System.DateTime.Now.TimeOfDay.Milliseconds.ToString();
        }


        else
        {
            Response.Redirect("../login.aspx ");
        }
        lbl_end.Text = System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");

        //lbl_end.Text = seconds;
    }
    //protected override void OnPreRender(EventArgs e)
    //{
    //    base.OnPreRender(e);

    //    TimeSpan tsDuration = DateTime.Now.Subtract((DateTime)Context.Items["Request_Start_Time"]);
    //    lbl_time.Text = "<em>Web-page processing duration: " + tsDuration.TotalMilliseconds + " miliseconds.</em>";

    //    double count =Convert.ToDouble(tsDuration.TotalMilliseconds.ToString());
    //    if (count<=2000 )
    //    {
    //        lbl_count.Text = "T1(Processing Within 2 Sec.)";

    //    }
    //    else if (count <= 4000)
    //    {

    //        lbl_count.Text = "T1(Processing Within 4 Sec.)";
    //    }
    //    else if (count <= 6000)
    //    {

    //        lbl_count.Text = "T1(Processing Within 6 Sec.)";
    //    }
    //    else if (count <= 8000)
    //    {

    //        lbl_count.Text = "T1(Processing Within 8 Sec.)";
    //    }
    //    else if (count <= 10000)
    //    {

    //        lbl_count.Text = "T1(Processing Within 10 Sec.)";
    //    }
    //    else 
    //    {

    //        lbl_count.Text = "T1(Processing more than 10 Sec.)";
    //    }
    //}

}
