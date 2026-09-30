 using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Specialized;
using System.Collections;


public partial class Inspections_Inspection_Officer_CivilWork : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataTable Dt2 = new DataTable();
    DataTable EditStack = new DataTable();
    DataTable EditStackNonMPSCSC = new DataTable();
    string Todaydate = "";
    string CheckValid = "";
    string receiptid = string.Empty;
    string gatePassid = string.Empty;
    string ArrivalStockid = string.Empty;
    SqlCommand cmd = null;
    private string con;

    public string PFID { get; private set; }

    private string branchid;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["login"] != null)
        //{
        PFID = Session["UserId"].ToString();
        branchid = Session["hdnbranchid"].ToString();
        if (!IsPostBack)
        {
            fillBranchDetails();
            fillGodownDetails();
            //owncapacity();
            //owncapacityuses();
            //capcapacity();
            //capcapacityuses();
            //Branchmgname();
            //TextBox1_CalendarExtender.SelectedDate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            //owngwnnum();
            ////GodownList();
            //GetEmpDetail();
            //Hiredcapacity();
            //Hiredcapacityuses();
            //fillFinancialYear();
            //SetInitialRow();
        }
        //}
        //else
        //{
        //    Response.Redirect("InspectionLogin.aspx");

        //}
    }
    public void fillBranchDetails()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Branch_Name_For_DF", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PFID_ID", PFID);
            con.Open();
            ddlbranch.DataSource = cmd.ExecuteReader();
            ddlbranch.DataTextField = "Depo_Name";
            ddlbranch.DataValueField = "Branch_ID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
            ddlbranch.SelectedValue = branchid.ToString();
            ddlbranch.Enabled = false;
            con.Close();
        }
    }

    public void fillGodownDetails()
    {
        //using (SqlConnection con2 = new SqlConnection(con))
        {

            //SqlCommand cmd = new SqlCommand("Get_Godown_Name_For_DF", con2);
            SqlCommand cmd = new SqlCommand("Get_Godown_Name_For_Civil_Work", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
            Con.Open();
            ddl_gdwn.DataSource = cmd.ExecuteReader();
            ddl_gdwn.DataTextField = "Godown_Name";
            ddl_gdwn.DataValueField = "Godown_ID";
            ddl_gdwn.DataBind();
            ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            Con.Close();
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //if (ddlinsecticide.SelectedValue == "0")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('select ')", true);
            //}

            SqlCommand cmd = new SqlCommand("Insert_Civil_Work", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
            cmd.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
            cmd.Parameters.AddWithValue("@Inspection_Quater", Session["hdninsp_type_id"].ToString());
            cmd.Parameters.AddWithValue("@Finacial_Year", Session["hdnfinancialyear"].ToString());
            cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnemployeeid"].ToString());
            cmd.Parameters.AddWithValue("@Order_No", Session["lblOrder_No"].ToString());
            //cmd.Parameters.AddWithValue("@Date_Of_Oblance", getDate_MDY(txtdob.Text));
            cmd.Parameters.AddWithValue("@Ghight_plinth", txtGhight_plinth.Text);
            cmd.Parameters.AddWithValue("@Ghight_InsOfficerTeam", txtGhight_InsOfficerTeam.Text);
            cmd.Parameters.AddWithValue("@Ghight_Remark", txtGhight_Remark.Text);
            cmd.Parameters.AddWithValue("@Gprotec_Plinthprotection", txtGprotec_Plinthprotection.Text);
            cmd.Parameters.AddWithValue("@Gprotec_Insofficerteam", txtGprotec_Insofficerteam.Text);
            cmd.Parameters.AddWithValue("@Gprotec_Remark", txtGprotec_Remark.Text);
            cmd.Parameters.AddWithValue("@WallFloors_Floors", ddlWallFloors_Floors.SelectedValue);
            cmd.Parameters.AddWithValue("@WallFloors_InsOfficerTeam", txtWallFloors_InsOfficerTeam.Text);
            cmd.Parameters.AddWithValue("@WallFloors_Remark", txtWallFloors_Remark.Text);
            cmd.Parameters.AddWithValue("@FloorPos_FloorPosition", ddlFloorPos_FloorPosition.SelectedValue);
            cmd.Parameters.AddWithValue("@FloorPos_InsOfficerTeam", txtFloorPos_InsOfficerTeam.Text);
            cmd.Parameters.AddWithValue("@FloorPos_Remark", txtFloorPos_Remark.Text);
            cmd.Parameters.AddWithValue("@ceilingrooftype_RoofType", ddlceilingrooftype_RoofType.SelectedValue);
            cmd.Parameters.AddWithValue("@ceilingrooftype_RoofPosition", ddlceilingrooftype_RoofPosition.SelectedValue);
            cmd.Parameters.AddWithValue("@ceilingrooftype_Remak", txtceilingrooftype_Remak.Text);
            cmd.Parameters.AddWithValue("@trussposition_TrussPosition", ddltrussposition_TrussPosition.SelectedValue);
            cmd.Parameters.AddWithValue("@trussposition_InsOfficerTeam", ddltrussposition_InsOfficerTeam.SelectedValue);
            cmd.Parameters.AddWithValue("@trussposition_Remark", txttrussposition_Remark.Text);
            cmd.Parameters.AddWithValue("@trusshegiht_TrussHeightFloor", txttrusshegiht_TrussHeightFloor.Text);
            cmd.Parameters.AddWithValue("@trusshegiht_Baranda", txttrusshegiht_Baranda.Text);
            cmd.Parameters.AddWithValue("@trusshegiht_Remark", txttrusshegiht_Remark.Text);
            cmd.Parameters.AddWithValue("@Shutterposition_ShutterPosition", txtShutterposition_ShutterPosition.Text);
            cmd.Parameters.AddWithValue("@Shutterposition_ShutterForged", txtShutterposition_ShutterForged.Text);
            cmd.Parameters.AddWithValue("@Shutterposition_NumberShutters", txtShutterposition_NumberShutters.Text);
            cmd.Parameters.AddWithValue("@Shutterposition_Erosion", txtShutterposition_Erosion.Text);
            cmd.Parameters.AddWithValue("@ddlShutterposition_Erosion", ddlShutterposition_Erosion.SelectedValue);
            cmd.Parameters.AddWithValue("@Shutterposition_Remark", txtShutterposition_Remark.Text);
            cmd.Parameters.AddWithValue("@conditionventilator_NuTopVentilators", txtconditionventilator_NuTopVentilators.Text);
            cmd.Parameters.AddWithValue("@conditionventilator_InsOfficerTeam", txtconditionventilator_InsOfficerTeam.Text);
            cmd.Parameters.AddWithValue("@conditionventilator_Remark", txtconditionventilator_Remark.Text);
            cmd.Parameters.AddWithValue("@lowerventilators_NuLowerVen", txtlowerventilators_NuLowerVen.Text);
            cmd.Parameters.AddWithValue("@lowerventilators_InsOfficerTeam", txtlowerventilators_InsOfficerTeam.Text);
            cmd.Parameters.AddWithValue("@lowerventilators_Remark", txtlowerventilators_Remark.Text);
            cmd.Parameters.AddWithValue("@Turboventilator_NuTurboVen", txtTurboventilator_NuTurboVen.Text);
            cmd.Parameters.AddWithValue("@Turboventilator_InsOfficerTeam", txtTurboventilator_InsOfficerTeam.Text);
            cmd.Parameters.AddWithValue("@Turboventilator_Remark", txtTurboventilator_Remark.Text);
            cmd.Parameters.AddWithValue("@Drainage_WaterFillingPosition", txtDrainage_WaterFillingPosition.Text);
            cmd.Parameters.AddWithValue("@Drainage_InsOfficerTeam", txtDrainage_InsOfficerTeam.Text);
            cmd.Parameters.AddWithValue("@Drainage_Remark", txtDrainage_Remark.Text);
            cmd.Parameters.AddWithValue("@Drain_Drain", txtDrain_Drain.Text);
            cmd.Parameters.AddWithValue("@Drain_InsOfficerTeam", txtDrain_InsOfficerTeam.Text);
            cmd.Parameters.AddWithValue("@Drain_Remark", txtDrain_Remark.Text);
            cmd.Parameters.AddWithValue("@boundary_BoundaryType", ddlboundary_BoundaryType.SelectedValue);
            cmd.Parameters.AddWithValue("@boundary_InsOfficerTeam", txtboundary_InsOfficerTeam.Text);
            cmd.Parameters.AddWithValue("@boundary_Remark", txtboundary_Remark.Text);
            cmd.Parameters.AddWithValue("@MainGate_MainGateNo", txtMainGate_MainGateNo.Text);
            cmd.Parameters.AddWithValue("@MainGate_MainGateposition", txtMainGate_MainGateposition.Text);
            cmd.Parameters.AddWithValue("@MainGate_InsOfficerTeam", txtMainGate_InsOfficerTeam.Text);
            cmd.Parameters.AddWithValue("@MainGate_Remark", txtMainGate_Remark.Text);
            cmd.Parameters.AddWithValue("@RoadType_RoadType", ddlRoadType_RoadType.SelectedValue);
            cmd.Parameters.AddWithValue("@RoadType_InsOfficerTeam", ddlRoadType_InsOfficerTeam.SelectedValue);
            cmd.Parameters.AddWithValue("@RoadType_RoadCondition", ddlRoadType_RoadCondition.SelectedValue);
            cmd.Parameters.AddWithValue("@RoadType_Remark", txtRoadType_Remark.Text);
            cmd.Parameters.AddWithValue("@firesafsys_NuFireEx", txtfiresafsys_NuFireEx.Text);
            cmd.Parameters.AddWithValue("@firesafsys_FireRefillDate", ddlfiresafsys_FireRefillDate.SelectedValue);
            cmd.Parameters.AddWithValue("@firesafsys_ValidityDate", txtfiresafsys_ValidityDate.Text);
            cmd.Parameters.AddWithValue("@firesafsys_NuSandBuck", txtfiresafsys_NuSandBuck.Text);
            cmd.Parameters.AddWithValue("@firesafsys_PhoneNu", ddltxtfiresafsys_PhoneNu.SelectedValue);
            cmd.Parameters.AddWithValue("@firesafsys_Remark", txtfiresafsys_Remark.Text);
            cmd.Parameters.AddWithValue("@Weybridge_DharmInPremises", ddlWeybridge_DharmInPremises.SelectedValue);
            cmd.Parameters.AddWithValue("@Weybridge_Ability", txtWeybridge_Ability.Text);
            cmd.Parameters.AddWithValue("@Weybridge_DateCalibration", txtWeybridge_DateCalibration.Text);
            cmd.Parameters.AddWithValue("@Weybridge_CertificateNo", txtWeybridge_CertificateNo.Text);
            cmd.Parameters.AddWithValue("@Weybridge_ValidityDate", txtWeybridge_ValidityDate.Text);
            cmd.Parameters.AddWithValue("@Weybridge_DistanceCampus", txtWeybridge_DistanceCampus.Text);
            cmd.Parameters.AddWithValue("@OfficeArea_RoofPosition", ddlOfficeArea_RoofPosition.SelectedValue);
            cmd.Parameters.AddWithValue("@OfficeArea_FloorPosition", ddlOfficeArea_FloorPosition.SelectedValue);
            cmd.Parameters.AddWithValue("@OfficeArea_WindowDoorPosition", ddlOfficeArea_WindowDoorPosition.SelectedValue);
            cmd.Parameters.AddWithValue("@OfficeArea_ToiletStatus", ddlOfficeArea_ToiletStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@OfficeArea_ElesyStemStatus", txtOfficeArea_ElesyStemStatus.Text);
            cmd.Parameters.AddWithValue("@OfficeArea_PantingPosition", txtOfficeArea_PantingPosition.Text);
            cmd.Parameters.AddWithValue("@OfficeArea_PaintingPosition", txtOfficeArea_PaintingPosition.Text);
            cmd.Parameters.AddWithValue("@OfficeArea_Remark", txtOfficeArea_Remark.Text);
            cmd.Parameters.AddWithValue("@ddl_gdwn", ddl_gdwn.SelectedValue);
            cmd.Parameters.AddWithValue("@created_by", Request.UserHostAddress);
            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Save Sucessfully')", true);
            //FillGrid();
            Clear();
        }
        catch (Exception ex)
        {
            //string except = ex.Message.ToString();

            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + except + "')", true);
        }
        //finally
        //{
        //    con_WLC.Close();
        //}
    }

    public void Clear()
    {
        txtGhight_plinth.Text = "";
       txtGhight_InsOfficerTeam.Text = "";
        txtGhight_Remark.Text = "";
        txtGprotec_Plinthprotection.Text = "";
        txtGprotec_Insofficerteam.Text = "";
        txtGprotec_Remark.Text = "";
        ddlWallFloors_Floors.SelectedValue = "0";
        txtWallFloors_InsOfficerTeam.Text = "";
        txtWallFloors_Remark.Text = "";
        ddlFloorPos_FloorPosition.SelectedValue = "0";
        txtFloorPos_InsOfficerTeam.Text = "";
        txtFloorPos_Remark.Text = "";
        ddlceilingrooftype_RoofType.SelectedValue = "0";
        ddlceilingrooftype_RoofPosition.SelectedValue = "0";
        txtceilingrooftype_Remak.Text = "";
        ddltrussposition_TrussPosition.SelectedValue = "0";
        ddltrussposition_InsOfficerTeam.SelectedValue = "0";
        txttrussposition_Remark.Text = "";
        txttrusshegiht_TrussHeightFloor.Text = "";
        txttrusshegiht_Baranda.Text = "";
        txttrusshegiht_Remark.Text = "";
        txtShutterposition_ShutterPosition.Text = "";
        txtShutterposition_ShutterForged.Text = "";
        txtShutterposition_NumberShutters.Text = "";
        //txtShutterposition_Erosion
        //ddlShutterposition_Erosion

        //ddlShutterposition_Remark.SelectedValue = "0";
        txtconditionventilator_NuTopVentilators.Text = "";
        txtconditionventilator_InsOfficerTeam.Text = "";
        txtconditionventilator_Remark.Text = "";
        txtlowerventilators_NuLowerVen.Text = "";
        txtlowerventilators_InsOfficerTeam.Text = "";
        txtlowerventilators_Remark.Text = "";
        txtTurboventilator_NuTurboVen.Text = "";
        txtTurboventilator_InsOfficerTeam.Text = "";
        txtTurboventilator_Remark.Text = "";
        txtDrainage_WaterFillingPosition.Text = "";
        txtDrainage_InsOfficerTeam.Text = "";
        txtDrainage_Remark.Text = "";
        txtDrain_Drain.Text = "";
        txtDrain_InsOfficerTeam.Text = "";
        txtDrain_Remark.Text = "";
        ddlboundary_BoundaryType.SelectedValue = "0";
        txtboundary_InsOfficerTeam.Text = "";
        txtboundary_Remark.Text = "";
        txtMainGate_MainGateNo.Text = "";
        txtMainGate_MainGateposition.Text = "";
        txtMainGate_InsOfficerTeam.Text = "";
        txtMainGate_Remark.Text = "";
        ddlRoadType_RoadType.SelectedValue = "0";
        ddlRoadType_InsOfficerTeam.SelectedValue = "0";
        ddlRoadType_RoadCondition.SelectedValue = "0";
        txtRoadType_Remark.Text = "";
        txtfiresafsys_NuFireEx.Text = "";
        ddlfiresafsys_FireRefillDate.SelectedValue = "0";
        txtfiresafsys_ValidityDate.Text = "";
        txtfiresafsys_NuSandBuck.Text = "";
        ddltxtfiresafsys_PhoneNu.SelectedValue = "0";
        //ddltxtfiresafsys_Remark.SelectedValue = "0";
        ddlWeybridge_DharmInPremises.SelectedValue = "0";
        txtWeybridge_Ability.Text = "";
        txtWeybridge_DateCalibration.Text = "";
        txtWeybridge_CertificateNo.Text = "";
        txtWeybridge_ValidityDate.Text = "";
        //ddlWeybridge_DistanceCampus.SelectedValue = "0";
        ddlOfficeArea_RoofPosition.SelectedValue = "0";
        ddlOfficeArea_FloorPosition.SelectedValue = "0";
        ddlOfficeArea_WindowDoorPosition.SelectedValue = "0";
        ddlOfficeArea_ToiletStatus.SelectedValue = "0";
        txtOfficeArea_ElesyStemStatus.Text = "";
        txtOfficeArea_PantingPosition.Text = "";
        txtOfficeArea_PaintingPosition.Text = "";
        txtOfficeArea_Remark.Text = "";
    }
}