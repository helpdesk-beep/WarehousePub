using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_BO_Holder_Register_Entry : System.Web.UI.Page
{
    public SqlConnection conStr2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string PFID = "";
    SqlTransaction sqltran;
    int a_id = 0;
    SqlCommand cmd;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        CalendarExtender1.EndDate = DateTime.Now;
        CalendarExtender2.EndDate = DateTime.Now;
        if (!IsPostBack)
        {
            fillGodownDetails();
            if (!String.IsNullOrEmpty(Session["UserId"].ToString()))
            {
                fillGrid();
            }
            if (Session["hdnID"] != null)
            {
                if (!String.IsNullOrEmpty(Session["hdnID"].ToString()))
                {
                    FillData();

                }
            }
        }
    }
    public void fillGodownDetails()
    {
        string conStr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Name", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
            con.Open();

            ddl_gdwn.DataSource = cmd.ExecuteReader();
            ddl_gdwn.DataTextField = "Godown_Name";
            ddl_gdwn.DataValueField = "Godown_ID";
            ddl_gdwn.DataBind();
            ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            // ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con.Close();
        }
    }
    protected void fillGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Holders_Register_Entry", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["UserId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GrdOfficerPreviousInsp.DataSource = dt;
                            GrdOfficerPreviousInsp.DataBind();
                        }
                        else
                        {

                            GrdOfficerPreviousInsp.DataSource = null;
                            GrdOfficerPreviousInsp.DataBind();
                        }
                    }
                }
            }
        }
    }
    public void FillData()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Holders_Register_Entry_By_ID", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", Session["hdnID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            ddl_gdwn.SelectedValue = dt.Rows[0]["Godown_No"].ToString();
                            txtDepositorName.Text = dt.Rows[0]["Depositor_Name"].ToString();
                            txtDepositorAddress.Text = dt.Rows[0]["Depositer_Address"].ToString();
                            txtLedgerFolio.Text = dt.Rows[0]["Depositors_Ledger_Folio"].ToString();
                            txtSubsequentName.Text = dt.Rows[0]["Name_of_Subsequent_Holder"].ToString();
                            txtSubsequentAddr.Text = dt.Rows[0]["Address_of_Subsequent_Holder"].ToString();
                            txtIntimationRegDate.Text = dt.Rows[0]["Date_of_Intimation_Registration"].ToString();
                            txtReference.Text = dt.Rows[0]["Reference_Instrument_Document_Transferring"].ToString();
                            txtIntimationDate.Text = dt.Rows[0]["Date_of_Intimation"].ToString();
                            txtOtherPerticular.Text = dt.Rows[0]["Other_Particular"].ToString();
                            txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
                            hdnHID.Value = dt.Rows[0]["ID"].ToString();
                            ddl_gdwn.Enabled = false;
                            btn_addnewoff.Text = "Update";
                            Session["hdnID"] = "0";
                        }
                        else
                        {

                            ddl_gdwn.SelectedValue = "";
                            txtDepositorName.Text = "";
                            txtDepositorAddress.Text = "";
                            txtLedgerFolio.Text = "";
                            txtSubsequentName.Text = "";
                            txtSubsequentAddr.Text = "";
                            txtIntimationRegDate.Text = "";
                            txtReference.Text = "";
                            txtIntimationDate.Text = "";
                            txtOtherPerticular.Text = "";
                            txtRemarks.Text = "";
                            ddl_gdwn.Enabled = true;
                            hdnHID.Value = "0";
                        }
                    }
                }
            }
        }
    }
    public void Clear()
    {
        ddl_gdwn.SelectedValue = "0";
        txtDepositorName.Text = "";
        txtDepositorAddress.Text = "";
        txtLedgerFolio.Text = "";
        txtSubsequentName.Text = "";
        txtSubsequentAddr.Text = "";
        txtIntimationRegDate.Text = "";
        txtReference.Text = "";
        txtIntimationDate.Text = "";
        txtOtherPerticular.Text = "";
        txtRemarks.Text = "";
        ddl_gdwn.Enabled = true;
        hdnHID.Value = "0";
    }
    public void checkvalidation()
    {
        if (ddl_gdwn.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Godown Name')", true);
            ddl_gdwn.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtDepositorName.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Name of the Original Depositor can not be blank')", true);
            txtDepositorName.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtDepositorAddress.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Addresss of the Original Depositor can not be blank')", true);
            txtDepositorAddress.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtLedgerFolio.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Depositors Ledger Folio can not be blank')", true);
            txtLedgerFolio.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtSubsequentName.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Name of Subsequent Holders who have sent Intimation to the Manager can not be blank')", true);
            txtSubsequentName.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtSubsequentAddr.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Address of Subsequent Holders who have sent Intimation to the Manager can not be blank')", true);
            txtSubsequentAddr.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtIntimationRegDate.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Date of Intimation for Registration under Rule 30 can not be blank')", true);
            txtIntimationRegDate.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtReference.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Reference to the instrument or document transferring possession can not be blank')", true);
            txtReference.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtIntimationDate.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Date of Intimation given under Rule 30 can not be blank')", true);
            txtIntimationDate.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtOtherPerticular.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Other Particular can not be blank')", true);
            txtOtherPerticular.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtRemarks.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Remarks can not be blank')", true);
            txtRemarks.Focus();
            return;
        }
    }
    protected void btnsaveprofile_Click(object sender, EventArgs e)
    {
        try
        {
            checkvalidation();
            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Insert_Holders_Register_Entry", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@ID", hdnHID.Value);
                cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@Godown_No", ddl_gdwn.SelectedValue);
                cmd.Parameters.AddWithValue("@Depositor_Name", txtDepositorName.Text);
                cmd.Parameters.AddWithValue("@Depositer_Address", txtDepositorAddress.Text);
                cmd.Parameters.AddWithValue("@Depositors_Ledger_Folio", txtLedgerFolio.Text);
                cmd.Parameters.AddWithValue("@Name_of_Subsequent_Holder", txtSubsequentName.Text);
                cmd.Parameters.AddWithValue("@Address_of_Subsequent_Holder", txtSubsequentAddr.Text);
                cmd.Parameters.AddWithValue("@Date_of_Intimation_Registration", txtIntimationRegDate.Text);
                cmd.Parameters.AddWithValue("@Reference_Instrument_Document_Transferring", txtReference.Text);
                cmd.Parameters.AddWithValue("@Date_of_Intimation", txtIntimationDate.Text);
                cmd.Parameters.AddWithValue("@Other_Particular", txtOtherPerticular.Text);
                cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
                cmd.Parameters.AddWithValue("@Insert_By", IPAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Holder Register Details Successfully Submitted|||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    Clear();
                    fillGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('"+ TheResult + "');", true);
                }
                fillGrid();
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void GrdOfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Overallinsp")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];
            //Fetch value of Name.           
            string hdnID = (row.FindControl("hdnID") as HiddenField).Value;
            Session["hdnID"] = hdnID.ToString();
            Response.Redirect("~/Inspections/BO/Holder_Register_Entry.aspx");
        }
    }
    protected void GrdOfficerPreviousInsp_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}