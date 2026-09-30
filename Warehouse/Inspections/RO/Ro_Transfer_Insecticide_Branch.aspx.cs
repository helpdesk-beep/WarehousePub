using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Principal;

public partial class Inspections_RO_Ro_Received_Insecticide_Entry : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    private object con;
    private object ob_value;
    public object GridView1 { get; private set; }
    public Label HiddenField { get; private set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] != null))
        {
            if (!IsPostBack)
            {
                clear();

                btnUpdate.Visible = false;
                if (!String.IsNullOrEmpty(Session["UserId"].ToString()))
                {
                    FillGrid();
                    GetDist();
                    btnUpdate.Visible = false;
                }
                if (Session["hdnID"] != null)
                {
                    if (!String.IsNullOrEmpty(Session["hdnID"].ToString()))
                    {
                        FillDataforUpdate();


                    }
                }
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void FillGrid()
    {
        //Sp_Get_Ro_Received_Insecticide_Entry
        SqlCommand cmdd = new SqlCommand("Sp_Get_Ro_Transfer_Insecticide_Branch", con_WLC);
        cmdd.CommandType = CommandType.StoredProcedure;
        cmdd.Parameters.AddWithValue("@Region_id", Session["UserId"].ToString());
        //conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GVOfStock.DataSource = dt;
            GVOfStock.DataBind();
            btnUpdate.Visible = false;
        }
    }
    private void FillBlances()
    {
        SqlCommand cmdd = new SqlCommand("Get_OpningBlances_Total_RO", con_WLC);
        cmdd.CommandType = CommandType.StoredProcedure;
        cmdd.Parameters.AddWithValue("@RegionID", Session["UserId"].ToString());
        cmdd.Parameters.AddWithValue("@Insecticide_ID", ddlinsecticide.SelectedValue);
        //conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblincbal.Text = dt.Rows[0]["balance"].ToString();
            Session["inc_balance"] = dt.Rows[0]["balance"].ToString();
        }
    }
    private void GetDist()
    {
        string strDist = "";
        strDist = "SELECT distinct District_Name,District_Id FROM Intergrated_MP_STORAGE.dbo.tbl_MetaData_DISTRICT where Region_ID='" + Session["UserId"].ToString() + "' order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "--Select--");
        }
        else
        {
            ddldistrict.Items.Insert(0, "--Select--");
        }
    }
    private void GetBranch(string DistID)
    {
        string strBranch = "";
        //strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
        strBranch = "SELECT Depotname,BranchID FROM Intergrated_MP_STORAGE.dbo.tbl_MetaData_Depot where DistrictID='" + DistID + "' order by Depotname";
        SqlDataAdapter da = new SqlDataAdapter(strBranch, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "Depotname";
            ddlbranch.DataValueField = "BranchID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlbranch.Items.Insert(0, "--Select--");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlinsecticide.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('select ')", true);
            }
            //else if (Convert.ToDecimal(Session["inc_balance"].ToString()) < Convert.ToDecimal(txtQuantity.Text.ToString()))
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('App shesh balance se jyada transfer nahi kar sakte hein yadi apko jayda dawai kshetriy karyalay ko dena hein to pehle , main balance me dawai ki entry karna padegi  ')", true);
            //}
            else
            {
                //Sp_Ro_Received_Insecticide_Entry
                SqlCommand cmd = new SqlCommand("Sp_Ro_Transfer_Insecticide_Branch", con_WLC);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_id", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@District_ID", ddldistrict.SelectedValue);
                cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
                cmd.Parameters.AddWithValue("@Opening_Date", getDate_MDY(txtOpeningDate.Text));
                //cmd.Parameters.AddWithValue("@Region_id",ddlregion.SelectedValue);
                cmd.Parameters.AddWithValue("@Insecticide_ID", ddlinsecticide.SelectedValue);
                cmd.Parameters.AddWithValue("@Opening_Balance_quantity", txtQuantity.Text);
                cmd.Parameters.AddWithValue("@Opening_Balance_value", txtValue.Text);
                cmd.Parameters.AddWithValue("@Opening_Balance_market_value", txtMarketRed.Text);
                cmd.Parameters.AddWithValue("@Unit", ddlUnitStock.SelectedValue);
                cmd.Parameters.AddWithValue("@Date_Expriy", getDate_MDY(txtExpriy.Text));
                cmd.Parameters.AddWithValue("@InvoiceNo", txtInvoiceNo.Text);
                cmd.Parameters.AddWithValue("@created_by", Request.UserHostAddress);
                con_WLC.Open();
                cmd.ExecuteNonQuery();
                con_WLC.Close();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Save Sucessfully')", true);
                FillGrid();
                clear();
            }
        }
        catch (Exception ex)
        {
            string except = ex.Message.ToString();

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + except + "')", true);
        }
        finally
        {
            con_WLC.Close();
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlinsecticide.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('select ')", true);
            }

            SqlCommand cmd = new SqlCommand("SP_Ho_Get_Fertilizer_update_Entry", con_WLC);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", Session["hdnID"].ToString());
            //cmd.Parameters.AddWithValue("@Region_id",ddlregion.SelectedValue);
            cmd.Parameters.AddWithValue("@Opening_Balance_quantity", txtQuantity.Text);
            cmd.Parameters.AddWithValue("@Opening_Balance_value", txtValue.Text);
            cmd.Parameters.AddWithValue("@Opening_Balance_market_value", txtMarketRed.Text);
            cmd.Parameters.AddWithValue("@Unit", txtMarketRed.Text);
            cmd.Parameters.AddWithValue("@update_by", Request.UserHostAddress);
            cmd.Parameters.AddWithValue("@Date_Expriy", getDate_MDY(txtExpriy.Text));
            con_WLC.Open();
            cmd.ExecuteNonQuery();
            con_WLC.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Update Sucessfully')", true);
            btnUpdate.Visible = false;
            Session["hdntblID"] = "0";
            btnSubmit.Visible = true;
            FillGrid();
            clear();
        }
        catch (Exception ex)
        {
            string except = ex.Message.ToString();

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + except + "')", true);
        }
        finally
        {
            con_WLC.Close();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    public void clear()
    {
        //txtPurchase.Text = "";
        //ddlregion.SelectedValue = "0";
        ddlinsecticide.SelectedValue = "0";
        ddlUnitStock.SelectedValue = "0";
        txtQuantity.Text = "";
        txtMarketRed.Text = "";
        txtValue.Text = "";
        txtExpriy.Text = "";
        lblincbal.Text = "";
        txtOpeningDate.Text = "";
        //ddldistrict.SelectedValue = "";
        ddlbranch.SelectedValue = "";
        txtInvoiceNo.Text = "";

    }
    protected void txtMarketRed_TextChanged(object sender, EventArgs e)
    {
        decimal val1 = Convert.ToDecimal(txtQuantity.Text);
        decimal val2 = Convert.ToDecimal(txtMarketRed.Text);
        decimal val3 = val1 * val2;
        txtValue.Text = val3.ToString();
    }
    public void FillDataforUpdate()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SP_Ho_Get_Fertilizer_Entry_ID", con))
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
                            ddlinsecticide.SelectedValue = dt.Rows[0]["Insecticide_ID"].ToString();
                            // ddlinsecticide.Enabled = true;
                            ddlUnitStock.SelectedValue = dt.Rows[0]["Unit"].ToString();
                            // ddlUnitStock.Enabled = true;
                            //txtPurchase.Text = dt.Rows[0]["Date_Purchase"].ToString();
                            txtQuantity.Text = dt.Rows[0]["Opening_Balance_quantity"].ToString();
                            txtMarketRed.Text = dt.Rows[0]["Opening_Balance_market_value"].ToString();
                            txtValue.Text = dt.Rows[0]["Opening_Balance_value"].ToString();
                            txtExpriy.Text = dt.Rows[0]["Date_Expriy"].ToString();
                            btnUpdate.Visible = true;
                            Session["hdntblID"] = "0";
                            btnSubmit.Visible = false;
                        }
                        else
                        {
                            //ddlinsecticide.Enabled = false;
                            //ddlUnitStock.Enabled = false;
                            ddlinsecticide.SelectedValue = "0";
                            ddlUnitStock.SelectedValue = "0";
                            txtQuantity.Text = "";
                            txtMarketRed.Text = "";
                            txtValue.Text = "";
                            btnUpdate.Visible = false;

                        }
                    }
                }
            }
        }
    }
    protected void GVOfStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Overallinsp")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GVOfStock.Rows[rowIndex];
            //Fetch value of Name.           
            string hdnID = (row.FindControl("hdnID") as HiddenField).Value;
            Session["hdnID"] = hdnID.ToString();
            Response.Redirect("~/Inspections/State/Ro_Insecticide_Distribution_Entry.aspx");
        }
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            //Reference the GridView Row.
            GridViewRow row = GVOfStock.Rows[rowIndex];
            string hdnID = (row.FindControl("hdnID") as HiddenField).Value;
            Session["hdnID"] = hdnID.ToString();
            RemoveRowvcpgqualificationbygridviewAEPRH( hdnID);

        }
    }
    public void RemoveRowvcpgqualificationbygridviewAEPRH(string id)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (con_WLC.State == ConnectionState.Closed)
        {
            con_WLC.Open();
        }
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (con_WLC.State == ConnectionState.Closed)
            {
                con_WLC.Open();
            }

            SqlCommand cmd = new SqlCommand("Delete_Insecticide_Transfer_For_Rm", con_WLC);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id.ToString());
            cmd.Parameters.AddWithValue("@delete_by", Request.UserHostAddress);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                FillGrid();
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
    protected void GVOfStock_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited.  
        GVOfStock.EditIndex = e.NewEditIndex;
        FillGrid();
    }
    protected void GVOfStock_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {

        //HiddenField id = GVOfStock.Rows[e.RowIndex].FindControl("hdnpfid") as HiddenField;
        ////TextBox Unit_Name = GVOfStock.Rows[e.RowIndex].FindControl("lbl_Unit_Name") as TextBox;
        //TextBox ob_quantity = GVOfStock.Rows[e.RowIndex].FindControl("txt_ob_quantity") as TextBox;
        //TextBox ob_market_value = GVOfStock.Rows[e.RowIndex].FindControl("txt_op_market_value") as TextBox;
        //TextBox ob_value = GVOfStock.Rows[e.RowIndex].FindControl("txt_ob_value") as TextBox;

        //con_WLC.Open();
        ////updating the record  
        //SqlCommand cmd = new SqlCommand("SP_Get_Fertilizer_update_Entry", con_WLC);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@ID", id.Value);
        ////cmd.Parameters.AddWithValue("@op_market_value", Unit_Name.Text);
        //cmd.Parameters.AddWithValue("@Opening_Balance_quantity", ob_quantity.Text);
        //cmd.Parameters.AddWithValue("@Opening_Balance_market_value", ob_market_value.Text);
        //cmd.Parameters.AddWithValue("@Opening_Balance_value", ob_value.Text);
        //cmd.ExecuteNonQuery();
        //con_WLC.Close();
        ////Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        //GVOfStock.EditIndex = -1;
        ////Call ShowData method for displaying updated data  
        //FillGrid();
    }
    protected void GVOfStock_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        GVOfStock.EditIndex = -1;


    }
    protected void GVOfStock_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        GVOfStock.EditIndex = -1;
        FillGrid();
    }
    protected void ddlinsecticide_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBlances();
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch(ddldistrict.SelectedValue.ToString());
    }
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal first = 0;
            int second = 0;
            string strMsg2 = "";
            if (lblincbal.Text != "" && txtQuantity.Text != "")
            {
                if (Int32.Parse(txtQuantity.Text) > decimal.Parse(lblincbal.Text))
                {
                    strMsg2 = strMsg2 + "Sent quantity should not be greater than Avl. Quantity.";
                    txtQuantity.Text = "";
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtQuantity.Text))
                    {
                        if (Int32.TryParse(txtQuantity.Text, out second) && decimal.TryParse(lblincbal.Text, out first))
                            lblincbal.Text = (first - second).ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Quantity');", true);
                        txtQuantity.Text = "";
                    }
                }
            }
            if (strMsg2.Trim() == "")
            {
                lblmsg.Text = "";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
