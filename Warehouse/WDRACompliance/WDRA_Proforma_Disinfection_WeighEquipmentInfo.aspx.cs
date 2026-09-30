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

public partial class WDRACompliance_WDRA_Proforma_Disinfection_WeighEquipmentInfo : System.Web.UI.Page
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
        try
        {
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
            

            if (ErrorMsg == "")
            {
                string BranchID = Session["BranchID"].ToString();
                //string GodownID = "";
                string GodownID = Session["GodownID_New"].ToString();
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
                string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
                SqlCommand cmd = new SqlCommand("usp_InsertWDRA_WH_Disinfestation_Weight_EquipmentDetailsInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Godown_ID", GodownID);
                cmd.Parameters.AddWithValue("@WHPhysicalBalance", EQPhysicalBalance);
                cmd.Parameters.AddWithValue("@WHCounterBalance", EQCounterBalance);
                cmd.Parameters.AddWithValue("@WHDigitalMoistureMeter", EQDigitalMoistureMeter);
                cmd.Parameters.AddWithValue("@WHSieveSet", EQSieveSet);
                cmd.Parameters.AddWithValue("@WHScoops", EQScoops);
                cmd.Parameters.AddWithValue("@WHForCepsBrushes", EQForCepsBrushes);
                cmd.Parameters.AddWithValue("@WHWeightBox", EQWeightBox);
                cmd.Parameters.AddWithValue("@WHEnamelPlates", EQEnamelPlates);
                cmd.Parameters.AddWithValue("@WHSampleBagsPolythene", EQSampleBagsPolythene);
                cmd.Parameters.AddWithValue("@WHSampleBagsCloth", EQSampleBagsCloth);
                cmd.Parameters.AddWithValue("@WHParkhi_BagTrier", EQParkhiBagTrier);
                cmd.Parameters.AddWithValue("@WHSampleSeal", EQSampleSeal);
                cmd.Parameters.AddWithValue("@WHMagnifyingGlassMagnification", EQMagnifyingGlassMagnification);
                cmd.Parameters.AddWithValue("@WHPetriDish", EQPetriDish);
                cmd.Parameters.AddWithValue("@WHMeasuringCylinders", EQMeasuringCylinders);
                cmd.Parameters.AddWithValue("@WHRecommendedPesticides", EQRecommendedPesticides);
                cmd.Parameters.AddWithValue("@WHRatControl", EQRatControl);
                cmd.Parameters.AddWithValue("@WHRatCages", EQRatCages);
                cmd.Parameters.AddWithValue("@WHThermoplasticFumigationCovers", EQThermoplasticFumigationCovers);
                cmd.Parameters.AddWithValue("@WHMultilayeredCrossLaminatedFumigationCovers", EQMultilayeredCLFumigationCovers);
                cmd.Parameters.AddWithValue("@WHFootHandSprayers", EQFootHandSprayers);
                cmd.Parameters.AddWithValue("@WHSandsnakes", EQSandsnakes);
                cmd.Parameters.AddWithValue("@WHAdhesiveTape", EQAdhesiveTape);
                cmd.Parameters.AddWithValue("@CreatedBy", ClientIP);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Warehouse Disineftion and Weighing Equipment Information Details Added Successfully";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Saved');", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' " + ErrorMsg.ToString() + "')", true);
            }

        }

        catch (Exception ex)
        {
            string except = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + except + "')", true);
        }
        finally
        {
            con.Close();
            fillDetailsInGrid();
        }
    }

    protected void fillDetailsInGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        //string BranchID = Session["BranchID"].ToString();
        string GodownID = ""; //Session["GodownID_New"].ToString();
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_Get_WDRA_WH_Disinfestation_Weight_EquipmentDetailsInfo", con))
            {
                //SqlCommand cmd = new SqlCommand("usp_Get_WDRA_WH_Disinfestation_Weight_EquipmentDetailsInfo", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@BranchID", BranchID);
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
            SqlCommand cmd = new SqlCommand("usp_DeleteWDRA_WH_Disinfestation_Weight_EquipmentDetailsInfo", con);
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
        Response.Redirect("~/WDRACompliance/WDRA_Proforma_Dunnage_OtherEquipmentInfo.aspx");
    }

    protected void btnClkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WDRACompliance/WDRA_Proforma_LocationInfo.aspx");
    }
}


