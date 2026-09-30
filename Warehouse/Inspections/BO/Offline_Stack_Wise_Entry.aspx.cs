using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_BO_Offline_Stack_Wise_Entry : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            fillGodownDetails();
            fillCommodity();
            if (!String.IsNullOrEmpty(Session["UserId"].ToString()))
            {
                fillGrid();
                fillDepositertype();
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
            con.Close();
        }
    }
    public void fillCommodity()
    {
        string conStr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Storage_Commodity", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();

            ddlCommodity.DataSource = cmd.ExecuteReader();
            ddlCommodity.DataTextField = "Commodity_Name";
            ddlCommodity.DataValueField = "Commodity_Id";
            ddlCommodity.DataBind();
            ddlCommodity.Items.Insert(0, new ListItem("-- Select Commodity --", "0"));
            con.Close();
        }
    }

    public void fillDepositertype()
    {
        string conStr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Depositor_Type", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();

            ddldepositertype.DataSource = cmd.ExecuteReader();
            ddldepositertype.DataTextField = "Depositor_Type";
            ddldepositertype.DataValueField = "DepositorType_ID";
            ddldepositertype.DataBind();
            ddldepositertype.Items.Insert(0, new ListItem("-- Select Depositor_Type --", "0"));
            con.Close();
        }
    }

    public void fillDepositerName()
    {
        string conStr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Depositer_Name", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Depositor_Type", ddldepositertype.SelectedValue);
            cmd.Parameters.AddWithValue("@BranchID", Session["UserId"].ToString());
            con.Open();

            ddldepositername.DataSource = cmd.ExecuteReader();
            ddldepositername.DataTextField = "Depositor_Name";
            ddldepositername.DataValueField = "Depositor_ID";
            ddldepositername.DataBind();
            ddldepositername.Items.Insert(0, new ListItem("-- Select Depositor_Type --", "0"));
            con.Close();
        }
    }
    protected void fillGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Offline_Stack_Entry", con))
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
            using (SqlCommand cmd = new SqlCommand("Get_Offline_Stack_Entry_By_ID", con))
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
                            ddlcropyear.SelectedValue = dt.Rows[0]["CropYear"].ToString();
                            ddldepositertype.SelectedValue = dt.Rows[0]["DepositorType_ID"].ToString();
                            fillDepositerName();
                            ddldepositername.SelectedValue = dt.Rows[0]["Depositer_id"].ToString();
                            txtReceiptNo.Text = dt.Rows[0]["Stack_No"].ToString();
                            ddlCommodity.SelectedValue = dt.Rows[0]["Commodity_ID"].ToString();
                            txtBags.Text = dt.Rows[0]["Bags"].ToString();
                            txtWeight.Text = dt.Rows[0]["Weight"].ToString();
                            hdnHID.Value = dt.Rows[0]["ID"].ToString();
                            ddl_gdwn.Enabled = false;
                            btn_addnewoff.Text = "Update";
                            Session["hdnID"] = "0";
                        }
                        else
                        {

                            ddl_gdwn.SelectedValue = "0";
                            ddlcropyear.SelectedValue = "0";
                            ddldepositertype.SelectedValue = "0";
                            ddldepositername.SelectedValue = "0";
                            txtReceiptNo.Text = "";
                            ddlCommodity.SelectedValue = "0";
                            txtBags.Text = "";
                            txtWeight.Text = "";
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
        ddlcropyear.SelectedValue = "0";
        ddldepositertype.SelectedValue = "0";
        ddldepositername.SelectedValue = "0";
        txtReceiptNo.Text = "";
        ddlCommodity.SelectedValue = "0";
        txtBags.Text = "";
        txtWeight.Text = "";
        ddl_gdwn.Enabled = true;
        hdnHID.Value = "0";
    }
    public void checkvalidation()
    {
        if (ddl_gdwn.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('गोदाम का नाम चुनें')", true);
            ddl_gdwn.Focus();
            return;
        }
        if (ddlcropyear.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Crop Year')", true);
            ddlcropyear.Focus();
            return;
        }
        if (ddldepositertype.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Depositer type')", true);
            ddldepositertype.Focus();
            return;
        }
        if (ddldepositername.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Depositer Name')", true);
            ddldepositername.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtReceiptNo.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Stack No.')", true);
            txtReceiptNo.Focus();
            return;
        }
        if (ddlCommodity.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('स्कंध चुनें')", true);
            ddlCommodity.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtBags.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('बोरे रिक्त नहीं हो सकता है')", true);
            txtBags.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtWeight.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('बजन की मात्रा रिक्त नहीं हो सकता है')", true);
            txtWeight.Focus();
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
                SqlCommand cmd = new SqlCommand("Insert_Offline_Stack_Entry", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@ID", hdnHID.Value);
                cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@Godown_No", ddl_gdwn.SelectedValue);
                cmd.Parameters.AddWithValue("@CropYear", ddlcropyear.SelectedValue);
                cmd.Parameters.AddWithValue("@Depositer_id", ddldepositername.SelectedValue);
                cmd.Parameters.AddWithValue("@Stack_No", txtReceiptNo.Text);
                cmd.Parameters.AddWithValue("@Commodity_ID", ddlCommodity.SelectedValue);
                cmd.Parameters.AddWithValue("@Bags", txtBags.Text);
                cmd.Parameters.AddWithValue("@Weight", txtWeight.Text);
                cmd.Parameters.AddWithValue("@Insert_By", IPAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Details of Offline Stack Successfully Submitted|||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    Clear();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
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
            Response.Redirect("~/Inspections/BO/Offline_Stack_Wise_Entry.aspx");

        }
        if (e.CommandName == "BlockWiseWntry")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];
            //Fetch value of Name.           
            string hdnGodown_ID = (row.FindControl("hdnGodown_ID") as HiddenField).Value;
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnCommodity_Id = (row.FindControl("hdnCommodity_Id") as HiddenField).Value;
            string hdnCropYear = (row.FindControl("hdnCropYear") as HiddenField).Value;
            string hdnDepositor_ID = (row.FindControl("hdnDepositor_ID") as HiddenField).Value;
            string godownname = (row.FindControl("lblGodown_Name") as Label).Text;


            string lblstack_id = (row.FindControl("lblStack_No") as Label).Text;
            string lblStack_Name = (row.FindControl("lblStack_No") as Label).Text;
            string lblDepositor_Name = (row.FindControl("lblDepositor_Name") as Label).Text;
            string lblcommodity = (row.FindControl("lblCommodity_Name") as Label).Text;
            string lblrecbags = (row.FindControl("lblBags") as Label).Text;
            string lblRecWeight = (row.FindControl("lblWeight") as Label).Text;

            Session["hdnGodown_ID"] = hdnGodown_ID.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnCommodity_Id"] = hdnCommodity_Id.ToString();
            Session["hdnCropYear"] = hdnCropYear.ToString();
            Session["hdnDepositor_ID"] = hdnDepositor_ID.ToString();

            Session["lblstack_id"] = lblstack_id.ToString();
            Session["lblStack_Name"] = lblStack_Name.ToString();
            Session["lblDepositor_Name"] = lblDepositor_Name.ToString();
            Session["lblcommodity"] = lblcommodity.ToString();
            Session["lblrecbags"] = lblrecbags.ToString();
            Session["lblRecWeight"] = lblRecWeight.ToString();
            Session["lblGodown_Name"] = godownname.ToString();
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Inspections/BO/Owned_Bolck_Wise_Entry.aspx';", true);
            Page.ClientScript.RegisterStartupScript(
   this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/BO/Create_Gadna_Patrak_For_Offline_Stack.aspx','_newtab');", true);
            //Response.Redirect("/Warehouse/Inspections/BO/Owned_Bolck_Wise_Entry.aspx");
            //Response.Redirect("window.location ='/Inspections/BO/Owned_Bolck_Wise_Entry.aspx'");
        }
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];

            //Fetch value of Name.
            string hdnid = (row.FindControl("hdnID") as HiddenField).Value;
            string hdnGodown_ID = (row.FindControl("hdnGodown_ID") as HiddenField).Value;
            Session["hdnid"] = hdnid.ToString();
            Session["hdnGodown_ID"] = hdnGodown_ID.ToString();
            RemoveRowvcpgqualificationbygridviewAEPRH(hdnid, hdnGodown_ID);

        }
    }
    public void RemoveRowvcpgqualificationbygridviewAEPRH(string id,string Godownid)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();
            }

            SqlCommand cmd = new SqlCommand("Offline_Stack_Remove", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@GodownID", Godownid.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                fillGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
    protected void GrdOfficerPreviousInsp_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void ddldepositertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDepositerName();
    }

    protected void ddl_gdwn_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}