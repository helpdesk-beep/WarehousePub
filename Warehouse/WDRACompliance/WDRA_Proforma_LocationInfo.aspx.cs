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

public partial class WDRACompliance_WDRA_Proforma_LocationInfo : System.Web.UI.Page
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
        try
        {
            string ErrorMsg = "";
            lblMsg.Text = "";

            ErrorMsg += !string.IsNullOrEmpty(txtPhone.Text) ? "" : "Warehouse Phone \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtPSJurisdiction.Text) ? "" : "Warehouse Police Station Jurisdiction \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtPSPhone.Text) ? "" : "Polce Station Phone  \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtDistanceFromPS.Text) ? "" : "Polce Station Distance from Warehouse \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtLocationPS.Text) ? "" : "Warehouse Location from PS \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtFSJurisdiction.Text) ? "" : "Warehouse Fire Station Jurisdiction \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtFSPhone.Text) ? "" : " Fire Station Phone\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtFSDistance.Text) ? "" : "Fire Station Distance from Warehouse \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtFSLocation.Text) ? "" : "Fire Station Location \\n";
            if (ErrorMsg == "")
            {
                //string BranchID = Session["BranchID"].ToString();
                //string GodownID = "";
                string GodownID = Session["GodownID_New"].ToString();
                string WHPhone = txtPhone.Text.ToString();
                string WHPSJurisdiction = txtPSJurisdiction.Text.ToString();
                string WHPSPhone = txtPSPhone.Text.ToString();
                string WHDistanceFromPS = txtDistanceFromPS.Text.ToString();
                string WHLocationFRomPS = txtLocationPS.Text.ToString();
                //Fire Station
                string WHFSJurisdiction = txtFSJurisdiction.Text.ToString();
                string WHFSPhone = txtFSPhone.Text.ToString();
                string WHDistanceFromFS = txtFSDistance.Text.ToString();
                string WHLocationFromFS = txtFSLocation.Text.ToString();

                string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

                SqlCommand cmd = new SqlCommand("usp_InsertWDRA_WHLocationInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Godown_ID", GodownID);
                cmd.Parameters.AddWithValue("@@WHPhone", WHPhone);
                cmd.Parameters.AddWithValue("@WHJurisdictionPoliceStation", WHPSJurisdiction);
                cmd.Parameters.AddWithValue("@WHPoliceStationPhone", WHPSPhone);
                cmd.Parameters.AddWithValue("@WHDistanceFromPoliceStation", WHDistanceFromPS);
                cmd.Parameters.AddWithValue("@WHPoliceStationLocation", WHLocationFRomPS);

                cmd.Parameters.AddWithValue("@WHJurisdictionFireStation", WHFSJurisdiction);
                cmd.Parameters.AddWithValue("@WHFireStationPhone", WHFSPhone);
                cmd.Parameters.AddWithValue("@WHDistanceFromFireStation", WHDistanceFromFS);
                cmd.Parameters.AddWithValue("@WHFireStationLocation", WHLocationFromFS);

                cmd.Parameters.AddWithValue("@CreatedBy", ClientIP);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                int c = cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Warehouse Location Details Added Successfully";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    //clear();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You have already Submitted!');", true);
                }

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
        catch (Exception ex)
        {
            string except = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + except + "')", true);
        }
        finally
        {
            con.Close();
        }
    }

    protected void fillDetailsInGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        //string GodownID = Session["GodownID"].ToString();
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_Get_tbl_WDRA_WHLocationInfo", con))
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
            SqlCommand cmd = new SqlCommand("usp_DeleteWDRA_WHLocationInfo", con);
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
        Response.Redirect("~/WDRACompliance/WDRA_Proforma_Disinfection_WeighEquipmentInfo.aspx");
    }

    protected void btnClkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WDRACompliance/WDRA_Proforma_Staffing.aspx");
    }
}


