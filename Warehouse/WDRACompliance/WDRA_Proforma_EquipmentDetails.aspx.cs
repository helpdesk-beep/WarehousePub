using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Resources;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;

public partial class WDRACompliance_WDRA_Proforma_EquipmentDetails : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                fillDetailsInGrid();
            }
        }
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rowIndex++;
                }
            }
        }
    }

    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ErrorMsg = "";
        lblMsg.Text = "";

        ErrorMsg += !string.IsNullOrEmpty(txtPhysicalBalance.Text) ? "" : "Enter Number of Physical Balance\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtCounterBalance.Text) ? "" : "Enter Number of Counter Balance\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtDigitalMoistureMeter.Text) ? "" : "Enter Number of Digital Moisture Meter\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtSieveSet.Text) ? "" : "Enter Number of Sieve Set\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtScoops.Text) ? "" : "Enter Number of Scoops\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtForCepsBrushes.Text) ? "" : "Enter Number of ForCeps Brushes\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtWeightBox.Text) ? "" : "Enter Number of Weight Box\\n";

        ErrorMsg += !string.IsNullOrEmpty(txtEnamelPlates.Text) ? "" : "Enter Number of Enamel Plates\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtSampleBagsPolythene.Text) ? "" : "Enter Number of SampleBags Polythene\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtSampleBagsCloth.Text) ? "" : "Enter Number of SampleBags Cloth\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtParkhiBagTrier.Text) ? "" : "Enter Number of ParkhiBag Trier\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtSampleSeal.Text) ? "" : "Enter Number of Sample Seal\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtMagnifyingGlassMagnification.Text) ? "" : "Enter Number of Magnifying Glass Magnification\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtPetriDish.Text) ? "" : "Enter Number of Petri Dish\\n";

        ErrorMsg += !string.IsNullOrEmpty(txtMeasuringCylinders.Text) ? "" : "Enter Number of Measuring Cylinders\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtRecommendedPesticides.Text) ? "" : "Enter Number of Recommended Pesticides\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtRatControl.Text) ? "" : "Enter Number of Rat Control\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtRatCages.Text) ? "" : "Enter Number of Rat Cages\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtThermoplasticFumigationCovers.Text) ? "" : "Enter Number of Thermoplastic Fumigation Covers\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtMultilayeredCLFumigationCovers.Text) ? "" : "Enter Number of MultilayeredCL Fumigation Covers\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtFootHandSprayers.Text) ? "" : "Enter Number of FootHand Sprayer\\n";

        ErrorMsg += !string.IsNullOrEmpty(txtSandsnakes.Text) ? "" : "Enter Number of Sandsnakes\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtAdhesiveTape.Text) ? "" : "Enter Number of Adhesive Tape\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtTarpaulin.Text) ? "" : "Enter Number of Tarpaulin\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtLadder.Text) ? "" : "Enter Number of Ladder\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtFirstAidbox.Text) ? "" : "Enter Number of First Aidbox\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtPlatformScales.Text) ? "" : "Enter Number of PlatformScales\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtGumBoots.Text) ? "" : "Enter Number of Gum Boots\\n";

        ErrorMsg += !string.IsNullOrEmpty(txtGoggles.Text) ? "" : "Enter Number of Goggles\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtGasMask.Text) ? "" : "Enter Number of GasMask\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtCanister.Text) ? "" : "Enter Number of Canister\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtPolytheneFilm.Text) ? "" : "Enter Number of PolytheneFilm\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtBambooMats.Text) ? "" : "Enter Number of BambooMats\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtWoodenCrates.Text) ? "" : "Enter Number of WoodenCrates\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtHectoliterWeightApparatus.Text) ? "" : "Enter Number of HectoliterWeightApparatus\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtSampleDivider.Text) ? "" : "Enter Number of SampleDivider\\n";

        ErrorMsg += !string.IsNullOrEmpty(txtVernierCaliper.Text) ? "" : "Enter Number of Vernier Caliper\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtThermohygrometer.Text) ? "" : "Enter Number of Thermohygrometer\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtFilterPapers.Text) ? "" : "Enter Number of Filter Papers\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtSpecimenTubes.Text) ? "" : "Enter Number of Specimen Tubes\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtMetalProbe.Text) ? "" : "Enter Number of Metal Probe\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtPhosphineAlertPersonalMonitor.Text) ? "" : "Enter Number of Phosphine Alert Personal Monitor\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtPhosphineGasMonitor.Text) ? "" : "Enter Number of Phosphine Gas Monitor\\n";

        ErrorMsg += !string.IsNullOrEmpty(txtToolBox.Text) ? "" : "Enter Number of ToolBox\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtDustMask.Text) ? "" : "Enter Number of DustMask\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtAprons.Text) ? "" : "Enter Number of Aprons\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtResuscitator.Text) ? "" : "Enter Number of Resuscitator\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtSCBA.Text) ? "" : "Enter Number of SCBA\\n";

        if (ErrorMsg == "")
        {
            string DistrictID = Session["Depot_DistID"].ToString();
            string BranchID = Session["BranchID"].ToString();
            string GodownID = "";

            string EQPhysicalBalance = txtPhysicalBalance.Text.ToString();
            string EQCounterBalance = txtCounterBalance.Text.ToString();
            string EQDigitalMoistureMeter = txtDigitalMoistureMeter.Text.ToString();
            string EQSieveSet = txtSieveSet.Text.ToString();
            string EQScoops = txtScoops.Text.ToString();
            string EQForCepsBrushes = txtForCepsBrushes.Text.ToString();
            string EQWeightBox = txtWeightBox.Text.ToString();

            string EQEnamelPlates = txtEnamelPlates.Text.ToString();
            string EQSampleBagsPolythene = txtSampleBagsPolythene.Text.ToString();
            string EQSampleBagsCloth = txtSampleBagsCloth.Text.ToString();
            string EQParkhiBagTrier = txtParkhiBagTrier.Text.ToString();
            string EQSampleSeal = txtSampleSeal.Text.ToString();
            string EQMagnifyingGlassMagnification = txtMagnifyingGlassMagnification.Text.ToString();
            string EQPetriDish = txtPetriDish.Text.ToString();

            string EQMeasuringCylinders = txtMeasuringCylinders.Text.ToString();
            string EQRecommendedPesticides = txtRecommendedPesticides.Text.ToString();
            string EQRatControl = txtRatControl.Text.ToString();
            string EQRatCages = txtRatCages.Text.ToString();
            string EQThermoplasticFumigationCovers = txtThermoplasticFumigationCovers.Text.ToString();
            string EQMultilayeredCLFumigationCovers = txtMultilayeredCLFumigationCovers.Text.ToString();
            string EQFootHandSprayers = txtFootHandSprayers.Text.ToString();

            string EQSandsnakes = txtSandsnakes.Text.ToString();
            string EQAdhesiveTape = txtAdhesiveTape.Text.ToString();
            string EQTarpaulin = txtTarpaulin.Text.ToString();
            string EQLadder = txtLadder.Text.ToString();
            string EQFirstAidbox = txtFirstAidbox.Text.ToString();
            string EQPlatformScales = txtPlatformScales.Text.ToString();
            string EQGumBoots = txtGumBoots.Text.ToString();

            string EQGoggles = txtGoggles.Text.ToString();
            string EQGasMask = txtGasMask.Text.ToString();
            string EQCanister = txtCanister.Text.ToString();
            string EQPolytheneFilm = txtPolytheneFilm.Text.ToString();
            string EQBambooMats = txtBambooMats.Text.ToString();
            string EQWoodenCrates = txtWoodenCrates.Text.ToString();
            string EQHectoliterWeightApparatus = txtHectoliterWeightApparatus.Text.ToString();
            string EQSampleDivider = txtSampleDivider.Text.ToString();

            string EQVernierCaliper = txtVernierCaliper.Text.ToString();
            string EQThermohygrometer = txtThermohygrometer.Text.ToString();
            string EQFilterPapers = txtFilterPapers.Text.ToString();
            string EQSpecimenTubes = txtSpecimenTubes.Text.ToString();
            string EQMetalProbe = txtMetalProbe.Text.ToString();
            string EQPhosphineAlertPersonalMonitor = txtPhosphineAlertPersonalMonitor.Text.ToString();
            string EQPhosphineGasMonitor = txtPhosphineGasMonitor.Text.ToString();

            string EQToolBox = txtToolBox.Text.ToString();
            string EQDustMask = txtDustMask.Text.ToString();
            string EQAprons = txtAprons.Text.ToString();
            string EQResuscitator = txtResuscitator.Text.ToString();
            string EQSCBA = txtSCBA.Text.ToString();

            string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
            string qry = "INSERT INTO [dbo].[tbl_PaymentStatusInfomation_PvtGodown] ([FinancialYear],[DistrictID],[BranchID],[GodownID],[GodownType],[GodownAgreementCapacity(InMT)]" +
                                     ",[GodownAgreementDate],[TotalAmountInRentInFY],[TotalAmountPaidToGodownInFY],[RemainingAmountOfGodownOwner],[AgreementEndDate], [Remarks]" +
                                     ",[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[DeletedBy], [DeletedDate])" +
                                     " VALUES ('" + ddlFinancialYear.SelectedValue + "','" + DistrictID + "','" + BranchID + "','" + GodownID + "','" + GodownType + "','" + GodownAgrCapacity + "','" + getDate_MDY(txtAgreementDate.Text) + "','" + GdnTotalAmountinRent + "','" + GdnTotalAmountPaymentinFY + "','" + GdnOwnerPendingAmount + "','" + getDate_MDY(txtAgreementEndDate.Text) + "','" + Remarks + "','" + ClientIP + "', GETDATE()" + ",'" + ClientIP + "', GETDATE()" + ",'" + ClientIP + "', GETDATE()" + ")";
            cmd = new SqlCommand(qry, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int c = cmd.ExecuteNonQuery();

            if (c > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Saved Successfully')", true);
                fillDetailsInGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Saved')", true);
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' " + ErrorMsg.ToString() + "')", true);
        }
    }

    protected void fillDetailsInGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        string BranchID = Session["BranchID"].ToString();
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    DataTable MainTable = ds.Tables[0];

                    if (MainTable.Rows.Count > 0)
                    {
                        GV_EntryDone.DataSource = MainTable;
                        GV_EntryDone.DataBind();
                    }
                    else
                    {
                        GV_EntryDone.DataSource = null;
                        GV_EntryDone.DataBind();
                    }

                }
            }
        }
    }

    protected void GV_EntryDone_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GV_EntryDone.Rows[rowIndex];

            //Fetch value of Name.
            string hdnId = (row.FindControl("hdnId") as HiddenField).Value;
            Session["hdnId"] = hdnId.ToString();
            RemoveRow(hdnId);

        }
    }

    public void RemoveRow(string id)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmd1 = new SqlCommand();
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            SqlCommand cmd = new SqlCommand("usp_DeletePaymentStatusEntry", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Deleted Successfully!');", true);
                fillDetailsInGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Deleted!');", true);
            }

        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }

    protected void btnClkNext_Click(object sender, EventArgs e)
    {

    }
}


