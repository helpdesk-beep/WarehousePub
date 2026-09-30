using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_RO_Region_Openning_Blnc : System.Web.UI.Page
{
    public SqlConnection con_JVS = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    //public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgrid();
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string ErrorMsg = "";
            if (!string.IsNullOrEmpty(txtDate.Text))
            {
                getDate_MDY(txtDate.Text);
            }
            ErrorMsg += ddlinsecticide.SelectedIndex > 0 ? "" : "Please Select Insecticide Name... \\n";
            ErrorMsg += ddlUnitStock.SelectedIndex > 0 ? "" : "Please Select Unit... \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtQuantity.Text) ? "" : "Please Enter Quantity. \\n";
            if (ErrorMsg == "")
            {
                SqlCommand cmd = new SqlCommand("usp_Insert_Opning_blnc_Entry_RM", con_JVS);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_id", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@Opening_Date", getDate_MDY(txtDate.Text));
                cmd.Parameters.AddWithValue("@Insecticide_ID", ddlinsecticide.SelectedValue);
                cmd.Parameters.AddWithValue("@Unit", ddlUnitStock.SelectedValue);
                cmd.Parameters.AddWithValue("@Opening_Balance_quantity", txtQuantity.Text);
                cmd.Parameters.AddWithValue("@Opening_Balance_market_value", txtMarketRed.Text);
                cmd.Parameters.AddWithValue("@Opening_Balance_value", txtValue.Value);
                cmd.Parameters.AddWithValue("@created_by", Session["UserId"].ToString());
                con_JVS.Open();
                cmd.ExecuteNonQuery();
                con_WLC.Close();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Save Sucessfully')", true);
                fillgrid();
                clear();
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
    }
    public void clear()
    {
        ddlinsecticide.SelectedValue = "0";
        ddlUnitStock.SelectedValue = "0";
        txtQuantity.Text = "";
        txtMarketRed.Text = "";
        txtValue.Value = "";
        txtDate.Text = "";
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
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Opening_Blnc_RM", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_Id", Session["UserId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            grdwhr.DataSource = dt;
                            grdwhr.DataBind();
                            Div1.Visible = true;
                        }
                        else
                        {
                            grdwhr.DataSource = null;
                            grdwhr.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void txtMarketRed_TextChanged(object sender, EventArgs e)
    {
        decimal val1 = Convert.ToDecimal(txtQuantity.Text);
        decimal val2 = Convert.ToDecimal(txtMarketRed.Text);
        decimal val3 = val1 * val2;
        txtValue.Value = val3.ToString();
    }
}