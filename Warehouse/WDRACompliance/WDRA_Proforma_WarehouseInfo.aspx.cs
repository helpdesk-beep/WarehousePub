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

public partial class WDRACompliance_WDRA_Proforma_WarehouseInfo : System.Web.UI.Page
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
        //string GodownID = Session["GodownID_New"].ToString();
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
            ErrorMsg += !string.IsNullOrEmpty(txtWHYoC.Text) ? "" : "Please Enter Warehouse Year of Construction\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtWHName.Text) ? "" : "Please Enter Warehouse Name\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtWHOwnerName.Text) ? "" : "Please Enter Warehouse Owner Name\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtWHOwnerContact.Text) ? "" : "Please Enter Warehouse Owner Contact\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtWHAltContact.Text) ? "" : "Please Enter Warehouse Owner Alternate Contact\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtWHOwnerAddress.Text) ? "" : "Please Enter Warehouse Owner Address\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtWDRARegNo.Text) ? "" : "Please Enter WDRA Registration No\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtValidityDate.Text) ? "" : "Please Enter WDRA License Validity Date\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtWHLicenseIssueDate.Text) ? "" : "Please Enter WDRA License Issue Date \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtWHOwnerAadhar.Text) ? "" : "Please Enter Warehouse Owner Aadhar No\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtWHOwnerEmail.Text) ? "" : "Please Enter Warehouse Owner Email\\n";

            if (ErrorMsg == "")
            {
                //string BranchID = Session["BranchID"].ToString();
                //string GodownID = "";
                string GodownID = Session["GodownID_New"].ToString();
                string WHYoc = txtWHYoC.Text.ToString();
                string WHName = txtWHName.Text.ToString();
                string WHAddress = txtWHAddress.Text.ToString();
                string WHOwnerName = txtWHOwnerName.Text.ToString();
                string WHOwnerContact = txtWHOwnerContact.Text.ToString();
                string WHAltContact = txtWHAltContact.Text.ToString();
                string WHOwnerAddress = txtWHOwnerAddress.Text.ToString();
                string WHWDRARegNo = txtWDRARegNo.Text.ToString();
                string WHLicValidityDate = txtValidityDate.Text.ToString();
                string WHLicIssueDate = txtWHLicenseIssueDate.Text.ToString();
                string WHOwnerAadhar = txtWHOwnerAadhar.Text.ToString();
                string WHOwnerEmailAddress = txtWHOwnerEmail.Text.ToString();
                //string Remarks = txtRemarks.Text.ToString();
                string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
                SqlCommand cmd = new SqlCommand("usp_InsertWDRA_WHWarehouseInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Godown_ID", GodownID);
                cmd.Parameters.AddWithValue("@WHYoC", WHYoc);
                cmd.Parameters.AddWithValue("@WHName", WHName);
                cmd.Parameters.AddWithValue("@WHOwnerName", WHOwnerName);
                cmd.Parameters.AddWithValue("@WHOwnerContact1", WHOwnerContact);
                cmd.Parameters.AddWithValue("@WHOwnerContact2", WHAltContact);
                cmd.Parameters.AddWithValue("@WHOwnerEmail", WHOwnerEmailAddress);
                cmd.Parameters.AddWithValue("@WHOwnerAddress", WHOwnerAddress);
                cmd.Parameters.AddWithValue("@WHAddress", WHAddress);
                cmd.Parameters.AddWithValue("@WHOwnerPOI", WHOwnerAadhar);
                cmd.Parameters.AddWithValue("@WHWDRALicenceNo", WHWDRARegNo);
                cmd.Parameters.AddWithValue("@WHWDRALicenceValidityDate", WHLicValidityDate);
                cmd.Parameters.AddWithValue("@WHWDRALicenceIssueDate", WHLicIssueDate);
                cmd.Parameters.AddWithValue("@CreatedBy", ClientIP);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Warehouse Security Details Added Successfully";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You have already Submitted!');", true);
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
        //string GodownID = Session["GodownID_New"].ToString();
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_Get_WDRA_WHWarehouseInfo", con))
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
            SqlCommand cmd = new SqlCommand("usp_DeleteWDRA_WHWarehouseInfo", con);
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
        Response.Redirect("~/WDRACompliance/WDRA_Proforma_GeneralInfo.aspx");
    }
}


