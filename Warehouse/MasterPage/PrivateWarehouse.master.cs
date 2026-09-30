using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class MasterPage_PrivateWarehouse : System.Web.UI.MasterPage
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_start.Text = System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");

        if (!string.IsNullOrEmpty(Session["GodownID_New"] as string))
        {
            if (!IsPostBack)
            {
                CheckGodownType();
                if (Session["G_BranchID"].ToString() == "2327002" || Session["G_BranchID"].ToString() == "2320007" || Session["G_BranchID"].ToString() == "2318001" || Session["G_BranchID"].ToString() == "2329001" || Session["G_BranchID"].ToString() == "232800303" || Session["G_BranchID"].ToString() == "2308003")
                {
                    if (GetGodown())
                    {
                        divSS.Visible = true;
                    }
                    else
                    {
                        divSS.Visible = false;
                    }
                }
                else if (Session["G_BranchID"].ToString() == "2341001" || Session["G_BranchID"].ToString() == "232600503" || Session["G_BranchID"].ToString() == "2312003" || Session["G_BranchID"].ToString() == "232600503" || Session["G_BranchID"].ToString() == "232700601" || Session["G_BranchID"].ToString() == "234100301" || Session["G_BranchID"].ToString() == "2312001" || Session["G_BranchID"].ToString()== "232700607")
                {
                    if (GetGodown())
                    {
                        divSS.Visible = true;
                    }
                    else
                    {
                        divSS.Visible = false;
                    }
                }
                else
                {
                    divSS.Visible = false;
                }
            }
            if (Session["lang"] != null)
            {

                if (Session["lang"].ToString() == "Hindi")
                {

                    spanHome.InnerText = Resources.hindi.spanHome;
                    hypHome.Text = Resources.hindi.hypHome;
                    spanInitialization.InnerText = Resources.hindi.spanInitialization;
                    hypOpeningBal.Text = Resources.hindi.hypOpeningBal;
                    spanWarehouseOperations.InnerText = Resources.hindi.spanWarehouseOperations;
                    //hypRecieptDetails.Text = Resources.hindi.hypRecieptDetails;
                    //hypEditRecieptDetails.Text = Resources.hindi.hypEditRecieptDetails;
                    //hypDepositorForm.Text = Resources.hindi.hypDepositorForm;
                    //hypDeliveryGatePass.Text = Resources.hindi.hypDeliveryGatePass;
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
                    
                }

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

    public void CheckGodownType()
    {
        string qry = "";
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string sid = Session["G_BranchID"].ToString();
        string gid = Session["GodownID_New"].ToString();
        qry = "select Maintain_By from Pvt_Warehouse_Login where Godown_Id='" + Session["GodownID_New"] + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Session["Maintainby"] = dt.Rows[0]["Maintain_By"].ToString();
            if (dt.Rows[0]["Maintain_By"].ToString() == "0" && Session["Logintype"].ToString() == "14")
            {
                div3.Visible = false;
                div5.Visible = true;
            }
            else
            {
                div3.Visible = true;
                div5.Visible = false;
            }
        }
    }
    public bool GetGodown()
    {
        string qry = "";
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string sid = Session["G_BranchID"].ToString();
        string gid = Session["GodownID_New"].ToString();
        qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["G_BranchID"].ToString() + "' and Godown_ID='" + Session["GodownID_New"] + "' AND Hired_Type='Steel Silo'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            return false;
        }
        else
        {

            return true;
        }
    }
}
