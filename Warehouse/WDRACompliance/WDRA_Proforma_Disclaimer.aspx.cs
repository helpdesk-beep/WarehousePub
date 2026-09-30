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

public partial class WDRACompliance_WDRA_Proforma_Disclaimer : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblDisclaimer.Text = "We have gone through the all the complaince forms and submitted as required.";
        txtDisclaimerDate.Text = DateTime.Now.ToString("dd-mm-yyyy tt:mm:ss"); 
        if ((Session["Godown_NewID"] != null))
        {
            //if (!IsPostBack)
            //{
                //fillDetailsInGrid();
            //}
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
        //string GodownID = "2312";//Session["Godown_NewID"].ToString();
        //string GodownID = "";
        string GodownID = Session["GodownID_New"].ToString();
        string AcknowledgementNo = "";
        AcknowledgementNo = "ACK" + GodownID.ToString();
        string ErrorMsg = "";
        lblMsg.Text = "";
        //ErrorMsg += chkDisclaimer.Checked.Equals(true) ? "" : "Please click on Disclaimer CheckBox To Submit\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtRemarks.Text) ? "" : "Enter Remarks\\n";
        if (ErrorMsg == "")
        {
            //string GodownID = "";//Session["BranchID"].ToString();
            string WHDisclaimerStatus = chkDisclaimer.Checked.ToString();
            string WHDisclaimerDate = DateTime.Now.ToString("dd-mm-yyyy HH:mm:ss");
            string Remarks = txtRemarks.Text.ToString();
            string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
            SqlCommand cmd = new SqlCommand("usp_InsertWDRA_WHDisclaimerInfo", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Godown_ID", GodownID);
            cmd.Parameters.AddWithValue("@WHAcknowledgementNo", AcknowledgementNo);
            cmd.Parameters.AddWithValue("@WHDisclaimerCheck", WHDisclaimerStatus);
            cmd.Parameters.AddWithValue("@WHDisclamerSignDate", WHDisclaimerDate);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@CreatedBy", ClientIP);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Warehouse Disclaimer recorded successfully. Your Acknowledgement No is:" + AcknowledgementNo ;
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

    protected void btnClkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WDRACompliance/WDRA_Proforma_CommodityInfo.aspx");
    }

}


