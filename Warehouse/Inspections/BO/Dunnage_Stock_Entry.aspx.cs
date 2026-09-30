using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_BO_Dunnage_Stock_Entry : System.Web.UI.Page
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
            using (SqlCommand cmd = new SqlCommand("Get_Insp_tbl_Dunnage_Stock", con))
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
            using (SqlCommand cmd = new SqlCommand("Get_Insp_tbl_Dunnage_Stock_By_ID", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DID", Session["hdnID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            txtDate.Text = dt.Rows[0]["Date"].ToString();
                            txtOpening.Text = dt.Rows[0]["OB_No_of_Mats_Under_Use"].ToString();
                            txtServiceable.Text = dt.Rows[0]["Serviceable_Mats_in_Hand"].ToString();
                            txtUnserviceable.Text = dt.Rows[0]["Unserviceable_Mats_in_Hand"].ToString();
                            txtQtyRec.Text = dt.Rows[0]["Qty_Received_Mats"].ToString();
                            txtQtySizeRec.Text = dt.Rows[0]["Qty_Received_Mats_Size"].ToString();
                            txtQtyValueRec.Text = dt.Rows[0]["Qty_Received_Mats_Value"].ToString();
                            txtFromDate.Text = dt.Rows[0]["From_Date"].ToString();
                            txtCIServiceable.Text = dt.Rows[0]["CI_No_of_Mats_Seal"].ToString();
                            txtCIUnserviceable.Text = dt.Rows[0]["CI_No_of_Mats_Unseal"].ToString();
                            txtCIValue.Text = dt.Rows[0]["CI_No_of_Mats_Value"].ToString();
                            txtQtyUsed.Text = dt.Rows[0]["Qty_Used_No_of_Mats"].ToString();
                            txtQtyUsedSize.Text = dt.Rows[0]["Qty_Used_No_of_Mats_Size"].ToString();
                            txtQtyUsedValue.Text = dt.Rows[0]["Qty_Used_No_of_Mats_Value"].ToString();
                            txtFloorArea.Text = dt.Rows[0]["Floor_Area"].ToString();
                            fillGodownDetails();
                            ddl_gdwn.SelectedValue = dt.Rows[0]["Godown_No"].ToString();
                            txtQtyTran.Text = dt.Rows[0]["Transfered_No_of_Mats"].ToString();
                            txtQtyTranSize.Text = dt.Rows[0]["Transfered_No_of_Mats_Size"].ToString();
                            txtQtyTranValue.Text = dt.Rows[0]["Transfered_No_of_Mats_Value"].ToString();
                            txtCentreName.Text = dt.Rows[0]["Name_of_Centre_to_Which_Transferred"].ToString();
                            txtCBUnderUse.Text = dt.Rows[0]["CB_No_of_Mats_Under_Use"].ToString();
                            txtCBServiceable.Text = dt.Rows[0]["CB_No_of_Mats_Serviceable"].ToString();
                            txtCBUnserviceable.Text = dt.Rows[0]["CB_No_of_Mats_In_Hand_Unserviceable"].ToString();
                            txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
                            txtDate.Enabled = false;
                            ddl_gdwn.Enabled = false;
                            btn_addnewoff.Text = "Update";
                            Session["hdnID"] = "0";
                        }
                        else
                        {

                            txtDate.Text = "";
                            txtOpening.Text = "";
                            txtServiceable.Text = "";
                            txtUnserviceable.Text = "";
                            txtQtyRec.Text = "";
                            txtQtySizeRec.Text = "";
                            txtQtyValueRec.Text = "";
                            txtFromDate.Text = "";
                            txtCIServiceable.Text = "";
                            txtCIUnserviceable.Text = "";
                            txtCIValue.Text = "";
                            txtQtyUsed.Text = "";
                            txtQtyUsedSize.Text = "";
                            txtQtyUsedValue.Text = "";
                            txtFloorArea.Text = "";
                            ddl_gdwn.SelectedValue = "0";
                            txtQtyTran.Text = "";
                            txtQtyTranSize.Text = "";
                            txtQtyTranValue.Text = "";
                            txtCentreName.Text = "";
                            txtCBUnderUse.Text = "";
                            txtCBServiceable.Text = "";
                            txtCBUnserviceable.Text = "";
                            txtRemarks.Text = "";
                            ddl_gdwn.Enabled = true;
                            txtDate.Enabled = true;

                        }
                    }
                }
            }
        }
    }
    public void checkvalidation()
    {
        if (string.IsNullOrEmpty(txtDate.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Date can not be blank')", true);
            txtDate.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtOpening.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Opening Balance can not be blank')", true);
            txtOpening.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtServiceable.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Serviceable (No. of mats in hand) can not be blank')", true);
            txtServiceable.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtUnserviceable.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Unserviceable (No. of mats in hand) can not be blank')", true);
            txtUnserviceable.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtQtyRec.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No. of mats Quantity Received can not be blank')", true);
            txtQtyRec.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtQtySizeRec.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No. of mats size Quantity Received can not be blank')", true);
            txtQtySizeRec.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtQtyValueRec.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No. of mats value Quantity Received can not be blank')", true);
            txtQtyValueRec.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtFromDate.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('From Date can not be blank')", true);
            txtFromDate.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtCIServiceable.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No. of mats Serviceable (Total of CI No. 3a & 4) can not be blank')", true);
            txtCIServiceable.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtCIUnserviceable.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No. of mats Unserviceable (Total of CI No. 3a & 4) can not be blank')", true);
            txtCIUnserviceable.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtCIValue.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No. of mats Value (Total of CI No. 3a & 4) can not be blank')", true);
            txtCIValue.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtQtyUsed.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Quantity Used (No. of mats) can not be blank')", true);
            txtQtyUsed.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtQtyUsedSize.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Quantity Used (No. of mats size) can not be blank')", true);
            txtQtyUsedSize.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtQtyUsedValue.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Quantity Used (No. of mats value) can not be blank')", true);
            txtQtyUsedValue.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtFloorArea.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Floor Area can not be blank')", true);
            txtFloorArea.Focus();
            return;
        }
        if (string.IsNullOrEmpty(ddl_gdwn.SelectedValue))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Godown No. can not be blank')", true);
            ddl_gdwn.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtQtyTran.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Quantity Transferred (No of mats) can not be blank')", true);
            txtQtyTran.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtQtyTranSize.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Quantity Transferred (No of mats size) can not be blank')", true);
            txtQtyTranSize.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtQtyTranValue.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Quantity Transferred (No of mats value) can not be blank')", true);
            txtQtyTranValue.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtCentreName.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Name of Centre which transferred can not be blank')", true);
            txtCentreName.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtCBUnderUse.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No.of mats Under Use (Closing Balance) can not be blank')", true);
            txtCBUnderUse.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtCBServiceable.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No.of mats Serviceable (Closing Balance) can not be blank')", true);
            txtCBServiceable.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtCBUnserviceable.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No.of mats Unserviceable (Closing Balance) can not be blank')", true);
            txtCBUnserviceable.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtRemarks.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Remarks can not be blank')", true);
            txtRemarks.Focus();
            return;
        }
    }
    public void Clear()
    {
        txtDate.Text = "";
        txtOpening.Text = "";
        txtServiceable.Text = "";
        txtUnserviceable.Text = "";
        txtQtyRec.Text = "";
        txtQtySizeRec.Text = "";
        txtQtyValueRec.Text = "";
        txtFromDate.Text = "";
        txtCIServiceable.Text = "";
        txtCIUnserviceable.Text = "";
        txtCIValue.Text = "";
        txtQtyUsed.Text = "";
        txtQtyUsedSize.Text = "";
        txtQtyUsedValue.Text = "";
        txtFloorArea.Text = "";
        ddl_gdwn.SelectedValue = "0";
        txtQtyTran.Text = "";
        txtQtyTranSize.Text = "";
        txtQtyTranValue.Text = "";
        txtCentreName.Text = "";
        txtCBUnderUse.Text = "";
        txtCBServiceable.Text = "";
        txtCBUnserviceable.Text = "";
        txtRemarks.Text = "";
        ddl_gdwn.Enabled = true;
        txtDate.Enabled = true;
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
                SqlCommand cmd = new SqlCommand("Insert_Insp_tbl_Dunnage_Stock", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@Date", txtDate.Text);
                cmd.Parameters.AddWithValue("@OB_No_of_Mats_Under_Use", txtOpening.Text);
                cmd.Parameters.AddWithValue("@Serviceable_Mats_in_Hand", txtServiceable.Text);
                cmd.Parameters.AddWithValue("@Unserviceable_Mats_in_Hand", txtUnserviceable.Text);
                cmd.Parameters.AddWithValue("@Qty_Received_Mats", txtQtyRec.Text);
                cmd.Parameters.AddWithValue("@Qty_Received_Mats_Size", txtQtySizeRec.Text);
                cmd.Parameters.AddWithValue("@Qty_Received_Mats_Value", txtQtyValueRec.Text);
                cmd.Parameters.AddWithValue("@From_Date", txtFromDate.Text);
                cmd.Parameters.AddWithValue("@CI_No_of_Mats_Seal", txtCIServiceable.Text);
                cmd.Parameters.AddWithValue("@CI_No_of_Mats_Unseal", txtCIUnserviceable.Text);
                cmd.Parameters.AddWithValue("@CI_No_of_Mats_Value", txtCIValue.Text);
                cmd.Parameters.AddWithValue("@Qty_Used_No_of_Mats", txtQtyUsed.Text);
                cmd.Parameters.AddWithValue("@Qty_Used_No_of_Mats_Size", txtQtyUsedSize.Text);
                cmd.Parameters.AddWithValue("@Qty_Used_No_of_Mats_Value", txtQtyUsedValue.Text);
                cmd.Parameters.AddWithValue("@Floor_Area", txtFloorArea.Text);
                cmd.Parameters.AddWithValue("@Godown_No", ddl_gdwn.SelectedValue);
                cmd.Parameters.AddWithValue("@Transfered_No_of_Mats", txtQtyTran.Text);
                cmd.Parameters.AddWithValue("@Transfered_No_of_Mats_Size", txtQtyTranSize.Text);
                cmd.Parameters.AddWithValue("@Transfered_No_of_Mats_Value", txtQtyTranValue.Text);
                cmd.Parameters.AddWithValue("@Name_of_Centre_to_Which_Transferred", txtCentreName.Text);
                cmd.Parameters.AddWithValue("@CB_No_of_Mats_Under_Use", txtCBUnderUse.Text);
                cmd.Parameters.AddWithValue("@CB_No_of_Mats_Serviceable", txtCBServiceable.Text);
                cmd.Parameters.AddWithValue("@CB_No_of_Mats_In_Hand_Unserviceable", txtCBUnserviceable.Text);
                cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
                cmd.Parameters.AddWithValue("@Insert_By", IPAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Dead Stock Details Successfully Submitted|||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    Clear();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
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
            Response.Redirect("~/Inspections/BO/Dunnage_Stock_Entry.aspx");

        }
    }
    protected void GrdOfficerPreviousInsp_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}