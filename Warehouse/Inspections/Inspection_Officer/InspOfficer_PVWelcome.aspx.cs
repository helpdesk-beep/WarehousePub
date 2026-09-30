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
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Collections.Generic;

public partial class Inspections_Inspection_Officer_InspOfficer_PVWelcome : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string PFID = "";
    string client_IP = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();       
        PFID = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            //  fillScheduleInsp_Grid(Session["UserId"].ToString());
            lblinspid.Text = Session["SInspID"].ToString();
            FatchScheduleInspData(lblinspid.Text.ToString());
        }
    }
    public void FatchScheduleInspData(string InspID)
    {
        string strsql = "select Inspection_ID,PF_ID,(select Officer_Name  from tbl_metadata_Inspection_officer where PF_ID=ISD.PF_ID) as Officer_Name,(select district_name from tbl_metadata_district as MDDIS where MDDIS.District_id=ISD.District_ID) as distirct_name,(select Depotname from tbl_metadata_depot as MDD where MDD.branchID=ISD.Branch_ID) as Depotname, Inspection_Status,BranchManagerName,BranchManagerCUGNo,Order_No,Insp_Period,Insp_Type,Convert(varchar(10),Order_Date,103) as Order_Date,ISD.District_ID,ISD.Branch_ID from tbl_Inpection_Scheduled_Date as ISD where Inspection_ID='" + InspID + "' ";
        SqlCommand cmd = new SqlCommand(strsql, conStr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblbranch.Text = dt.Rows[0]["Depotname"].ToString().Trim();
            lblinsptype.Text = dt.Rows[0]["Insp_Type"].ToString().Trim();
            lblInspPeriod.Text = dt.Rows[0]["Insp_Period"].ToString().Trim();
        }
        else
        {

        }
    }
    protected void lnklbl_final_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        lbl_otpnovalidate.Text = "";
        otpentersection.Visible = false;
        Session["S_OTP"] = "";
        btno_generateOTP.Enabled = true;
        txtOTP.Text = "";
    }
    protected void btno_generateOTP_Click(object sender, EventArgs e)
    {
        FillOTPNumb();
        otpentersection.Visible = true;
        ModalPopupExtender1.Show();
        if (lbl_otpnovalidate.Text != "" && lbl_otpnovalidate.Text.Length == 6)
        {
            btno_generateOTP.Enabled = false;
        }
    }
    public void FillOTPNumb()
    {
        try
        {
            lbl_otpnovalidate.Text = "";
            Random random = new Random();
            string combination = "0123456789";
            StringBuilder OTP_Validate = new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                OTP_Validate.Append(combination[random.Next(combination.Length)]);
            }
            Session["S_OTP"] = OTP_Validate.ToString();
            lbl_otpnovalidate.Text = Session["S_OTP"].ToString();
        }
        catch
        {
            throw;
        }
    }
    protected void btnoptsubmit_Click(object sender, EventArgs e)
    {
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        if (txtOTP.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter OTP...'); </script> ");
            ModalPopupExtender1.Show();
        }
        else if (txtOTP.Text.Trim().Length != 6)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter 6 Digit OTP No....'); </script> ");
            ModalPopupExtender1.Show();
        }
        else if (txtOTP.Text.Trim() != lbl_otpnovalidate.Text.Trim())
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Correct OTP ...'); </script> ");
            ModalPopupExtender1.Show();
        }
        else
        {
            string qry = "update tbl_Inpection_Scheduled_Date set Inspection_Status='Submitted', UpdateBy='" + client_IP + "' , UpdatedDate=getdate() where Inspection_ID='" + lblinspid.Text + "'";
            SqlCommand cmd1 = new SqlCommand(qry, conStr);
            int CT1 = 0;
            CT1 = cmd1.ExecuteNonQuery();
            if (CT1 > 0)
            {
                Button1_Click(null, null);
                //  ModalPopupExtender2.Show();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Submit Physical Inspection Of this Branch ...'); </script> ");
                Response.Redirect("/Inspections/Inspection_Officer/InspOfficer_ScheduledInspDetail.aspx");
            }
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Inspections/Inspection_Officer/Fill_Inspection_Annexure_B.aspx");
    }
    //protected void Button3_Click(object sender, EventArgs e)
    //{
    //    Session["RoleId"] = 1;
    //    Session["BranchId"] = Session["SInsp_BranchID"].ToString();
    //    Session["reporturl"] = "";
    //    Session["reporturl"] = "Branch_GdwnWiseCmdWiseStackBal";
    //    ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Depot.aspx\",\"_blank\")", true);
    //}
    //protected void btnNewReg_Click(object sender, EventArgs e)
    //{
    //    Session["RoleId"] = 1;
    //    Session["BranchId"] = Session["SInsp_BranchID"].ToString();
    //    Session["reporturl"] = "";
    //    Session["reporturl"] = "Branch_GdwnWiseCmdWiseStockBalance";
    //    ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Depot.aspx\",\"_blank\")", true);
    //}
    //protected void Button5_Click(object sender, EventArgs e)
    //{
    //    Session["RoleId"] = 1;
    //    Session["BranchId"] = Session["SInsp_BranchID"].ToString();
    //    Session["reporturl"] = "";
    //    Session["reporturl"] = "Branch_GdwnWiseWHRWiseStockBalance";
    //    ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Depot.aspx\",\"_blank\")", true);
    //}
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["RoleId"] = 1;
        Session["Depot_DistID"] = Session["SInsp_DisID"].ToString();
        Session["BranchId"] = Session["SInsp_BranchID"].ToString();
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_GdwnWiseCmdWiseStockBalance";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session["RoleId"] = 1;
        Session["Depot_DistID"] = Session["SInsp_DisID"].ToString();
        Session["BranchId"] = Session["SInsp_BranchID"].ToString();
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_GdwnWiseCmdWiseStackBal";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        Session["RoleId"] = 1;
        Session["Depot_DistID"] = Session["SInsp_DisID"].ToString();
        Session["BranchId"] = Session["SInsp_BranchID"].ToString();
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_GdwnWiseWHRWiseStockBalance";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
}