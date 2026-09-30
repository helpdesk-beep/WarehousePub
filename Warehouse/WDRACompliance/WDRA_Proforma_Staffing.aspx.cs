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

public partial class WDRACompliance_WDRA_Proforma_Staffing : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string GodownID = Session["GodownID_New"].ToString();
        //if ((Session["BranchID"] != null))
        //{
        if (!IsPostBack)
            {
                fillDetailsInGrid();
            }
        //}
    }

    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
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
            ErrorMsg += ddlWHEmployeeType.SelectedIndex > 0 ? "" : "Please Select Employee Type\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtWHEmployeeName.Text) ? "" : "Please Enter Name\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtEmpQualification.Text) ? "" : "Please Enter Qualification\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtEmployeeExp.Text) ? "" : "Please Enter Experience\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtEmployeeTraining.Text) ? "" : "Please Enter Training\\n";

            ErrorMsg += !string.IsNullOrEmpty(txtEmpAddress.Text) ? "" : "Please Enter Employee Address\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtEmployeeMobile.Text) ? "" : "Please Enter Employee Mobile\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtEmpDoB.Text) ? "" : "Please Enter Employee DoB\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtEmployeePoI.Text) ? "" : "Please Enter Employee PoI\\n";

            ErrorMsg += !string.IsNullOrEmpty(txtEmployeeEmail.Text) ? "" : "Please Enter Employee Email\\n";
            ErrorMsg += !string.IsNullOrEmpty(txtEmpContactPerson.Text) ? "" : "Please Enter Contact PErson Details\\n";

            if (ErrorMsg == "")
            {
                //string BranchID = Session["BranchID"].ToString();
                //string GodownID = Session["GodownID_New"].ToString();
                //string GodownID = "";
                string GodownID = Session["GodownID_New"].ToString();
                string WHEmpType = ddlWHEmployeeType.SelectedValue.ToString();
                string WHEmpName = txtWHEmployeeName.Text.ToString();
                string WHEmpQualification = txtEmpQualification.Text.ToString();
                string WHEmpExp = txtEmployeeExp.Text.ToString();
                string WHEmpTraining = txtEmployeeTraining.Text.ToString();
                string WHEmpAddress = txtEmpAddress.Text.ToString();
                string WHEmpMobile = txtEmployeeMobile.Text.ToString();
                string WHEmpDoB = txtEmpDoB.Text.ToString();
                string WHEmpPoI = txtEmployeePoI.Text.ToString();
                string WHEmpEmail = txtEmployeeEmail.Text.ToString();
                string WHEmployeeCP = txtEmpContactPerson.Text.ToString();
                string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

                SqlCommand cmd = new SqlCommand("usp_InsertWDRA_WH_StaffingInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Godown_ID", GodownID);
                cmd.Parameters.AddWithValue("@WHEmpType", WHEmpType);
                cmd.Parameters.AddWithValue("@WHEmpName", WHEmpName);
                cmd.Parameters.AddWithValue("@WHEmpQualification", WHEmpQualification);
                cmd.Parameters.AddWithValue("@WHEmpExperience", WHEmpExp);
                cmd.Parameters.AddWithValue("@WHEmpTraining", WHEmpTraining);
                cmd.Parameters.AddWithValue("@WHEmpAddress", WHEmpAddress);
                cmd.Parameters.AddWithValue("@WHEmpMobile", WHEmpMobile);
                cmd.Parameters.AddWithValue("@WHEmpDoB", WHEmpDoB);
                cmd.Parameters.AddWithValue("@WHEmpPoI", WHEmpPoI);
                cmd.Parameters.AddWithValue("@WHEmpEmail", WHEmpEmail);
                cmd.Parameters.AddWithValue("@WHEmpContactPerson", WHEmployeeCP);
                cmd.Parameters.AddWithValue("@CreatedBy", ClientIP);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Warehouse Staff Information Details Added Successfully";
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
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_Get_WDRA_WH_StaffingInfo", con))
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
            SqlCommand cmd = new SqlCommand("usp_DeleteWDRA_WH_StaffingInfo", con);
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
        Response.Redirect("~/WDRACompliance/WDRA_Proforma_LocationInfo.aspx");
    }

    protected void btnClkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WDRACompliance/WDRA_Proforma_GeneralInfo.aspx");
    }

}


