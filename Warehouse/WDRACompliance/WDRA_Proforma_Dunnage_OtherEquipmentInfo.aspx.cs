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

public partial class WDRACompliance_WDRA_Proforma_Dunnage_OtherEquipmentInfo : System.Web.UI.Page
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
                //string GodownID = "";
                string GodownID = Session["GodownID_New"].ToString();
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
                SqlCommand cmd = new SqlCommand("usp_InsertWDRA_WH_Dunnage_Other_EquipmentDetailsInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Godown_ID",GodownID);
                cmd.Parameters.AddWithValue("@WHTarpaulin", EQTarpaulin);
                cmd.Parameters.AddWithValue("@WHLadder", EQLadder);
                cmd.Parameters.AddWithValue("@WHFirstAidbox", EQFirstAidbox);
                cmd.Parameters.AddWithValue("@WHPlatformScales", EQPlatformScales);
                cmd.Parameters.AddWithValue("@WHGumBoots", EQGumBoots);
                cmd.Parameters.AddWithValue("@WHGoggles", EQGoggles);
                cmd.Parameters.AddWithValue("@WHGasMask", EQGasMask);
                cmd.Parameters.AddWithValue("@WHCanister", EQCanister);
                cmd.Parameters.AddWithValue("@WHPolytheneFilm", EQPolytheneFilm);
                cmd.Parameters.AddWithValue("@WHBambooMats", EQBambooMats);
                cmd.Parameters.AddWithValue("@WHWoodenCrates", EQWoodenCrates);
                cmd.Parameters.AddWithValue("@WHHectoliterWeightApparatus", EQHectoliterWeightApparatus);
                cmd.Parameters.AddWithValue("@WHSampleDivider", EQSampleDivider);
                cmd.Parameters.AddWithValue("@WHVernierCaliper", EQVernierCaliper);
                cmd.Parameters.AddWithValue("@WHThermohygrometer", EQThermohygrometer);
                cmd.Parameters.AddWithValue("@WHFilterPapers", EQFilterPapers);
                cmd.Parameters.AddWithValue("@WHSpecimenTubes", EQSpecimenTubes);
                cmd.Parameters.AddWithValue("@WHMetalProbe", EQMetalProbe);
                cmd.Parameters.AddWithValue("@WHPhosphineAlertPersonalMonitor", EQPhosphineAlertPersonalMonitor);
                cmd.Parameters.AddWithValue("@WHPhosphineGasMonitor", EQPhosphineGasMonitor);
                cmd.Parameters.AddWithValue("@WHToolBox", EQToolBox);
                cmd.Parameters.AddWithValue("@WHDustMask", EQDustMask);
                cmd.Parameters.AddWithValue("@WHAprons", EQAprons);
                cmd.Parameters.AddWithValue("@WHResuscitator", EQResuscitator);
                cmd.Parameters.AddWithValue("@WHSCBA", EQSCBA);
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
        string BranchID = Session["BranchID"].ToString();
        string GodownID = Session["GodownID_New"].ToString();
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_Get_WDRA_WH_Dunnage_Other_EquipmentDetailsInfo", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@GodownID", GodownID);
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
            SqlCommand cmd = new SqlCommand("usp_DeleteWDRA_WH_Dunnage_Other_EquipmentDetailsInfo", con);
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
        Response.Redirect("~/WDRACompliance/WDRA_Proforma_WeighBridge.aspx");
    }

    protected void btnClkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WDRACompliance/WDRA_Proforma_Disinfection_WeighEquipmentInfo.aspx");
    }

}


