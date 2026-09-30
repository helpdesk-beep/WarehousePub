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

public partial class WDRACompliance_WDRA_Proforma_WeighBridge : System.Web.UI.Page
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
            if (!string.IsNullOrEmpty(txtInstallationDate.Text))
            {
                getDate_MDY(txtInstallationDate.Text);
            }
            if (!string.IsNullOrEmpty(txtWBStampingDate.Text))
            {
                getDate_MDY(txtWBStampingDate.Text);
            }
            ErrorMsg += !string.IsNullOrEmpty(txtWeighbridgeCapacity.Text) ? "" : "Please Enter Weighbridge Capacity \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtWBMake.Text) ? "" : "Please Enter Weighbrigde Make\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtInstallationDate.Text) ? "" : "Please Enter Weighbridge Installation Date \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtWBStampingDate.Text) ? "" : "Please Enter Weighbridge Stamping Date \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtWBOwnerName.Text) ? "" : "Please Enter Weighbridge Owner Name \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtWBName.Text) ? "" : "Please Enter Weighbridge Name\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtDistance.Text) ? "" : "Please Enter Weighbridge Distance from Warehouse\\n";
            if (ErrorMsg == "")
            {
                //string BranchID = Session["BranchID"].ToString();
                //string GodownID = "";
                string GodownID = Session["GodownID_New"].ToString();
                string WBCapacity = txtWeighbridgeCapacity.Text.ToString();
                string WBMake = txtWBMake.Text.ToString();
                string WBInstallationDate = txtInstallationDate.Text.ToString();
                string WBStampingDate = txtWBStampingDate.Text.ToString();
                string WBTypeOfLorry = rdlWBLorryType.SelectedValue.ToString();
                string WBOwnerName = txtWBOwnerName.Text.ToString();
                string WBName = txtWBName.Text.ToString();
                string WBDistance = txtDistance.Text.ToString();
                string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

                //string qry = "INSERT INTO [dbo].[tbl_WDRA_WH_WeighbridgeInfo] ([Godown_ID],[WeighbridgeCapacityInMT],[WeighbridgeMake],[WeighbridgeDateOfInstallation]" +
                //            ",[WeighbridgeDateOfDateLastStamping],[WBTypeOfLorry],[WBOwnerName],[WBName],[WBDistanceFromGodown],[CreatedBy],[CreatedDate])" +
                //            " VALUES ('" + Godown_ID + "','" + WBCapacity + "','" + WBMake + "','" + WBInstallationDate + "','" + WBStampingDate + "','" + WBTypeOfLorry + "','" + WBOwnerName + "','" + WBName + "','" + WBDistance + "','" + ClientIP + "', GETDATE()" + ")";
                SqlCommand cmd = new SqlCommand("usp_InsertWDRA_WH_WeighbridgeInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Godown_ID", GodownID);
                cmd.Parameters.AddWithValue("@WeighbridgeCapacityInMT", WBCapacity);
                cmd.Parameters.AddWithValue("@WeighbridgeMake", WBMake);
                cmd.Parameters.AddWithValue("@WeighbridgeDateOfInstallation", WBInstallationDate);
                cmd.Parameters.AddWithValue("@WeighbridgeDateOfDateLastStamping", WBStampingDate);
                cmd.Parameters.AddWithValue("@WBTypeOfLorry", WBTypeOfLorry);
                cmd.Parameters.AddWithValue("@WBOwnerName", WBOwnerName);
                cmd.Parameters.AddWithValue("@WBName", WBName);
                cmd.Parameters.AddWithValue("@WBDistanceFromGodown", WBDistance);
                cmd.Parameters.AddWithValue("@CreatedBy", ClientIP);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Warehouse Weighbridge Information Details Added Successfully";
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
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_Get_WDRA_WH_WeighbridgeInfo", con))
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
            SqlCommand cmd = new SqlCommand("usp_DeleteWDRA_WH_WeighbridgeInfo", con);
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
        Response.Redirect("~/WDRACompliance/WDRA_Proforma_FireSafetyInfo.aspx");
    }

    protected void btnClkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WDRACompliance/WDRA_Proforma_Dunnage_OtherEquipmentInfo.aspx");
    }
}


