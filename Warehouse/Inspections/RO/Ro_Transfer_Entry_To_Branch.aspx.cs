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
using System.Text;
using System.Net;
using System.Net.Sockets;
public partial class Inspections_RO_Ro_Transfer_Entry_To_Branch : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    public SqlConnection con_JVS = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    private object con;
    private object ob_value;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] != null))
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Session["UserId"].ToString()))
                {
                    FillGrid();
                    GetRegion();
                }

            }
        }
        else
        {
            Response.Redirect("~/Inspections/Default.aspx");
        }
    }
    private void FillBlances()
    {
        SqlCommand cmdd = new SqlCommand("Get_OpningBlances_Total_RM_Transfer", con_JVS);
        cmdd.CommandType = CommandType.StoredProcedure;
        cmdd.Parameters.AddWithValue("@Insecticide_ID", ddlinsecticide.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmdd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblopeningblc.Text = dt.Rows[0]["balance"].ToString();
            Session["inc_balance"] = dt.Rows[0]["balance"].ToString();
        }
    }
    private void GetRegion()
    {
        string strDist = "";
        strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlregion.DataSource = ds.Tables[0];
            ddlregion.DataTextField = "Regionnm";
            ddlregion.DataValueField = "Region_ID";
            ddlregion.DataBind();
            ddlregion.Items.Insert(0, new ListItem("--Select Region--", "0"));
            con_WLC.Close();
        }
    }
    protected void ddlinsecticide_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBlances();
    }
    protected void txtMarketRed_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            if (!string.IsNullOrEmpty(txtQuantity.Text) || !string.IsNullOrEmpty(txtMarketRed.Text))
            {
                decimal val1 = Convert.ToDecimal(txtQuantity.Text);
                decimal val2 = Convert.ToDecimal(txtMarketRed.Text);
                decimal val3 = val1 * val2;
                txtvalue.Text = val3.ToString();
            }
            else
            {
                txtvalue.Text = "";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


    }
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            if (!string.IsNullOrEmpty(txtQuantity.Text))
            {
                decimal val1 = Convert.ToDecimal(lblopeningblc.Text);
                decimal val2 = Convert.ToDecimal(txtQuantity.Text);
                decimal val3 = val1 - val2;
                lblremaining.Text = val3.ToString();
            }
            else
            {
                lblremaining.Text = "";
            }
            if (!string.IsNullOrEmpty(txtQuantity.Text) && (!string.IsNullOrEmpty(txtMarketRed.Text)))
            {
                decimal val1 = Convert.ToDecimal(lblopeningblc.Text);
                decimal val2 = Convert.ToDecimal(txtQuantity.Text);
                decimal val3 = val1 - val2;
                txtvalue.Text = val3.ToString();
            }
            else
            {
                txtMarketRed.Text = "";
                txtvalue.Text = "";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtExpiry.Text = "";
        txt_OpeningDate.Text = "";
        ddlregion.ClearSelection();
        ddlinsecticide.ClearSelection();
        ddlUnitStock.ClearSelection();
        txtQuantity.Text = "";
        txtMarketRed.Text = "";
        txtvalue.Text = "";
        txtExpiry.Text = "";
        lblopeningblc.Text = "";
        txtInvoiceNo.Text = "";
        lblremaining.Text = "";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlinsecticide.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('select ')", true);
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Ro_Transfer_Entry_To_Branch", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Opening_Date", getDate_MDY(txt_OpeningDate.Text));
                cmd.Parameters.AddWithValue("@Insecticide_ID", ddlinsecticide.SelectedValue);
                cmd.Parameters.AddWithValue("@Region_id", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@Opening_Balance_quantity", txtQuantity.Text);
                cmd.Parameters.AddWithValue("@Opening_Balance_market_value", txtMarketRed.Text);
                cmd.Parameters.AddWithValue("@Opening_Balance_value", txtvalue.Text);
                cmd.Parameters.AddWithValue("@Unit", ddlUnitStock.SelectedValue);
                cmd.Parameters.AddWithValue("@Date_Expriy", getDate_MDY(txtExpiry.Text));
                cmd.Parameters.AddWithValue("@InvoiceNo", txtInvoiceNo.Text);
                cmd.Parameters.AddWithValue("@RM_to_RM_Transfer", ddlregion.SelectedValue);
                cmd.Parameters.AddWithValue("@created_by", GetLocalIPAddress());
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Save Sucessfully')", true);
                FillGrid();
                Clear();
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
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        String ipaddress = string.Empty;
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipaddress = ip.ToString();
            }
        }
        return ipaddress.Length > 0 ? ipaddress : null;
    }
    private void FillGrid()
    {
        //Sp_Get_Ro_Received_Insecticide_Entry
        SqlCommand cmdd = new SqlCommand("Sp_Get_RM_Transfer_Insecticide", con_JVS);
        cmdd.CommandType = CommandType.StoredProcedure;
        cmdd.Parameters.AddWithValue("@Region_id", Session["UserId"].ToString());
        //conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GVRMtransfer.DataSource = dt;
            GVRMtransfer.DataBind();
            //btnUpdate.Visible = false;
        }
    }
    protected void Clear()
    {
        txt_OpeningDate.Text = "";
        ddlregion.ClearSelection();
        ddlinsecticide.ClearSelection();
        ddlUnitStock.ClearSelection();
        txtQuantity.Text = "";
        txtMarketRed.Text = "";
        txtvalue.Text = "";
        txtExpiry.Text = "";
        lblopeningblc.Text = "";
        txtInvoiceNo.Text = "";
        lblremaining.Text = "";
    }
    protected void GVRMtransfer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            //Reference the GridView Row.
            GridViewRow row = GVRMtransfer.Rows[rowIndex];
            string hdnTransferID = (row.FindControl("id") as HiddenField).Value;
            Session["hdnid"] = hdnTransferID.ToString();
            RemoveRowvcpgqualificationbygridviewAEPRH(hdnTransferID);
        }
    }
    public void RemoveRowvcpgqualificationbygridviewAEPRH(string hdnTransferID)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Delete_Rm_To_Rm_Transfer_Entry", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@id", hdnTransferID.ToString());
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
}