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
using System.Data.SqlClient;

public partial class BranchPages_Branch_Available_Resources : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["BranchId"] != null))
        {
            if (!IsPostBack)
            {
              
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string DistrictId = Session["Depot_DistID"].ToString();
        string BranchId = Session["BranchId"].ToString();
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string qry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Resource_Availablity] ([DistrictId] ,[BranchId] ,[RegionID] ,[BMName] ,[BMContact] ,[IMName] ,[IMContact] ,[DepoCapaty] ,[RPDistance] ,[IntConnAva] ,[IntConnType] ,[ISPName] ,[SWANAvai] ,[PowerSuply] ,[EWBAva] ,[EWBType] ,[EWBOperatorName] ,[DEOperatorName] ,[DEOperatorQty] ,[GPOperatorName] ,[DPOperatorQty] ,[DesktopIS] ,[DesktopIW] ,[DesktopCS] ,[DesktopCW] ,[DesktopMS] ,[DesktopMW] ,[LaptopIS] ,[LaptopIW] ,[LaptopCS] ,[LaptopCW] ,[LaptopMS] ,[LaptopMW] ,[PrinterIS] ,[PrinterIW] ,[PrinterCS] ,[PrinterCW] ,[PrinterMS] ,[PrinterMW] ,[UPSIS] ,[UPSIW] ,[UPSCS] ,[UPSCW] ,[UPSMS] ,[UPSMW] ,[TPrinterIS] ,[TPrinterIW] ,[TPrinterCS] ,[TPrinterCW] ,[TPrinterMS] ,[TPrinterMW] ,[TabletsIS] ,[TabletsIW] ,[TabletsCS] ,[TabletsCW] ,[TabletsMS] ,[TabletsMW] ,[WeighMeterIS] ,[WeighMeterIW] ,[WeighMeterCS] ,[WeighMeterCW] ,[WeighMeterMS] ,[WeighMeterMW] ,[DongleIS] ,[DongleIW] ,[DongleCS] ,[DongleCW] ,[DongleMS] ,[DongleMW] ,Water_Avai,Toilet_Avai,Total_Staff_Branch,[CreatedBy] ,[CreatedDate] ,[UpdatedBy] ,[UpdatedDate] ,[DeletedBy] ,[DeletedDate]) VALUES ('" + DistrictId + "','" + BranchId + "','' ,'" + txtNodalOfficerName.Text + "' ,'" + txtBMContact.Text + "' ,'" + txtIMName.Text + "','" + txtIMContact.Text + "' ,'" + txtDepoCapicity.Text + "' ,'" + txtDFRP.Text + "' ,'" + IntConnctddl.SelectedValue + "' ,'" + typeOfIntrConnctddl.SelectedValue + "' ,'" + txtISPName.Text + "' ,'" + ddlSWAN.SelectedValue + "' ,'" + PowerSuplyddl.SelectedValue + "' ,'" + ddlEWAvail.SelectedValue + "' ,'" + ddlWBTYpe.SelectedValue + "','" + txtEWOName.Text + "' ,'" + txtDEONAme.Text + "' ,'" + txtDEONo.Text + "' ,'" + txtGPONAme.Text + "' ,'" + txtGPONo.Text + "' ,'" + txtTDS.Text + "' ,'" + TextBox22.Text + "' ,'" + txtSD.Text + "' ,'" + TextBox23.Text + "' ,'" + txtResources.Text + "' ,'" + TextBox24.Text + "' ,'" + TextBox1.Text + "' ,'" + TextBox2.Text + "' ,'" + TextBox3.Text + "' ,'" + TextBox25.Text + "' ,'" + TextBox26.Text + "' ,'" + TextBox27.Text + "' ,'" + TextBox4.Text + "' ,'" + TextBox5.Text + "' ,'" + TextBox6.Text + "','" + TextBox28.Text + "' ,'" + TextBox29.Text + "' ,'" + TextBox30.Text + "' ,'" + TextBox7.Text + "' ,'" + TextBox8.Text + "' ,'" + TextBox9.Text + "' ,'" + TextBox31.Text + "','" + TextBox32.Text + "' ,'" + TextBox33.Text + "' ,'" + TextBox10.Text + "' ,'" + TextBox11.Text + "' ,'" + TextBox12.Text + "' ,'" + TextBox34.Text + "' ,'" + TextBox35.Text + "' ,'" + TextBox36.Text + "' ,'" + TextBox13.Text + "' ,'" + TextBox14.Text + "' ,'" + TextBox15.Text + "' ,'" + TextBox37.Text + "' ,'" + TextBox38.Text + "' ,'" + TextBox39.Text + "' ,'" + TextBox16.Text + "' ,'" + TextBox17.Text + "' ,'" + TextBox18.Text + "' ,'" + TextBox40.Text + "' ,'" + TextBox41.Text + "' ,'" + TextBox42.Text + "' ,'" + TextBox19.Text + "' ,'" + TextBox20.Text + "' ,'" + TextBox21.Text + "' ,'" + TextBox43.Text + "' ,'" + TextBox44.Text + "' ,'" + TextBox45.Text + "' ,'" + ddlDrinkingWater.SelectedValue + "','" + ddlToilet.SelectedValue + "','" + TextBox46.Text + "','" + ip + "' ,GETDATE() ,'' ,'','','')";
        SqlCommand cmd = new SqlCommand(qry,con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Saved Successfully..'); </script> ");
        btnupdate.Enabled = false;
    }
}
