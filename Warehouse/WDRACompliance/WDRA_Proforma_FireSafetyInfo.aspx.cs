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

public partial class WDRACompliance_WDRA_Proforma_FireSafetyInfo : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((Session["BranchID"] != null))
        //{
            if (!IsPostBack)
            {
                fillDetailsInGrid();
            }
        //}
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

            ErrorMsg += !string.IsNullOrEmpty(txtWHCapacity.Text) ? "" : "Please Enter Warehouse Capacity in MT\\n";
            ErrorMsg += ddlCapacity.SelectedIndex > 0 ? "" : "Please Select Warehouse Capacity Range\\n";
            //ErrorMsg += rdlFireAlarm.SelectedIndex > 0 ? "" : "Please Select Warehouse Fire Alarm Availability\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtFireAlarm.Text) ? "" : "Please Enter Fire Alarm Count\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtFireBucket.Text) ? "" : "Please Enter Fire Bucket Count\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtFExNo.Text) ? "" : "Please Enter Fire Extinguisher Count\\n";


            //ErrorMsg += rdlClassA.SelectedValue = 1 ? "" : "Please Select Fire Extinguisher Type Class-A\\n";
            //ErrorMsg += rdlClassB.SelectedValue > 0 ? "" : "Please Select Fire Extinguisher Type Class-B\\n";
            //ErrorMsg += rdlClassC.SelectedValue > 0 ? "" : "Please Select Fire Extinguisher Type Class-C\\n";
            //ErrorMsg += rdlClassD.SelectedValue > 0 ? "" : "Please Select Fire Extinguisher Type Class-D\\n";
            //ErrorMsg += rdlClassE.SelectedValue > 0 ? "" : "Please Select Fire Extinguisher Type Class-E\\n";

            ErrorMsg += !string.IsNullOrEmpty(txtClassA.Text) ? "" : "Please Enter Class A Fire Extinguisher Count\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtClassB.Text) ? "" : "Please Enter Class B Fire Extinguisher Count\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtClassC.Text) ? "" : "Please Enter Class C Fire Extinguisher Count\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtClassD.Text) ? "" : "Please Enter Class D Fire Extinguisher Count\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtClassE.Text) ? "" : "Please Enter Class E Fire Extinguisher Count\\n";

            if (ErrorMsg == "")
            {
                //string BranchID = Session["BranchID"].ToString();
                //string GodownID = "";
                string GodownID = Session["GodownID_New"].ToString();
                string WHCapacity = txtWHCapacity.Text.ToString();
                string WHFireAlarmNo = txtFireAlarm.Text.ToString();
                string WHFireBucketNo = txtFireBucket.Text.ToString();
                string WHFireExtNo = txtFExNo.Text.ToString();
                string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

                SqlCommand cmd = new SqlCommand("usp_InsertWDRA_WH_FireSecurityDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Godown_ID", GodownID);
                cmd.Parameters.AddWithValue("@WHCapacityInMT", WHCapacity);
                cmd.Parameters.AddWithValue("@WHFireAlarmAvailable", rdlFireAlarm.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@WHFireAlarmCount", WHFireAlarmNo);
                //cmd.Parameters.AddWithValue("@WHFireBucketAvlb", WHFireBucketNo);
                cmd.Parameters.AddWithValue("@WHFireBucketCount", WHFireBucketNo);
                cmd.Parameters.AddWithValue("@WHFireExtinguisherAvlb", WHFireExtNo);
                cmd.Parameters.AddWithValue("@WHFireExtinguisherClassA", rdlClassA.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@WHFE_ClassA_Cnt", Convert.ToInt32(txtClassA.Text.ToString()));
                cmd.Parameters.AddWithValue("@WHFireExtinguisherClassB", rdlClassB.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@WHFE_ClassB_Cnt", Convert.ToInt32(txtClassB.Text.ToString()));
                cmd.Parameters.AddWithValue("@WHFireExtinguisherClassC", rdlClassC.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@WHFE_ClassC_Cnt", Convert.ToInt32(txtClassC.Text.ToString()));
                cmd.Parameters.AddWithValue("@WHFireExtinguisherClassD", rdlClassD.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@WHFE_ClassD_Cnt", Convert.ToInt32(txtClassD.Text.ToString()));
                cmd.Parameters.AddWithValue("@WHFireExtinguisherClassE", rdlClassE.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@WHFE_ClassE_Cnt", Convert.ToInt32(txtClassE.Text.ToString()));
                cmd.Parameters.AddWithValue("@CreatedBy", ClientIP);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Warehouse Fire Security Information Details Added Successfully";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Saved');", true);
                }
                //int c = cmd.ExecuteNonQuery();

                //if (c > 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Saved Successfully')", true);
                //    fillDetailsInGrid();
                //}
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Saved')", true);
                //}
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
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_Get_WDRA_WH_FireSecurityDetails", con))
            {
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
            SqlCommand cmd = new SqlCommand("usp_DeleteWDRA_WH_FireSecurityDetails", con);
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
        Response.Redirect("~/WDRACompliance/WDRA_Proforma_DocUpload.aspx");
    }

    protected void btnClkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WDRACompliance/WDRA_Proforma_WeighBridge.aspx");
    }
}


