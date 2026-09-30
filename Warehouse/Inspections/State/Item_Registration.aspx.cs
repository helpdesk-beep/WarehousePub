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
using System.Net.Sockets;
using System.Net;

public partial class Inspections_State_Item_Registration : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    public SqlConnection con_JVS = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string con_WLC2 = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    private object con;
    private object ob_value;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] != null))
        {
            if (!IsPostBack)
            {
                BindItem();
                FillGrid();
                GrdItem.Columns[3].Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/Inspections/Default.aspx");
        }
    }
    protected void BindItem()
    {
        string strDist = "";
        strDist = "Select Inventory_Id,Inventory_Name from Mst_Inventory";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_JVS);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlitem.DataSource = ds.Tables[0];
            ddlitem.DataTextField = "Inventory_Name";
            ddlitem.DataValueField = "Inventory_Id";
            ddlitem.DataBind();
            ddlitem.Items.Insert(0, new ListItem("--Select Item--", "0"));
            con_WLC.Close();
        }
    }
    protected void ddlitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_FillRate", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Inventory_Id", ddlitem.SelectedValue);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                txtprice.Text = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Inventory_Rate"].ToString()) ? ds.Tables[0].Rows[0]["Inventory_Rate"].ToString() : "";
                                txtAvailable.Text = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Inventory_Quentity"].ToString()) ? ds.Tables[0].Rows[0]["Inventory_Quentity"].ToString() : "";
                            }
                            else
                            {
                                txtprice.Text = "";
                                txtAvailable.Text = "";
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('!')", true);
        }
    }
    protected void FillGrid()
    {
        using (SqlConnection con = new SqlConnection(con_WLC2))
        {
            SqlCommand cmd = new SqlCommand("Usp_SelectItem", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Region_ID", Session["UserId"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                GrdItem.DataSource = dt;
                GrdItem.DataBind();

                //lblOfficerList.Text = Convert.ToString(dt.Rows.Count);
                //ViewState["Region"] = dt;
            }
            else
            {
                GrdItem.DataSource = null;
                GrdItem.DataBind();
                //lblOfficerList.Text = "0";
            }
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            //checkvalidation();
            string ErrorMsg = "";
            if (!string.IsNullOrEmpty(txtpurchasedate.Text))
            {
                getDate_MDY(txtpurchasedate.Text);
            }
            ErrorMsg += ddlitem.SelectedIndex > 0 ? "" : "Please Select Item... \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtbillnumber.Text) ? "" : "Enter Bill No. \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtQuantity.Text) ? "" : "Enter Item Qiuantity \\n";
            if (ErrorMsg == "")
            {
                if (btnsave.Text == "Save")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("InsertPurchaseItem", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Inventory_Id", ddlitem.SelectedValue);
                    cmd.Parameters.AddWithValue("@Item_Rate", txtprice.Text);
                    cmd.Parameters.AddWithValue("@Bill_Number", txtbillnumber.Text);
                    cmd.Parameters.AddWithValue("@Item_PurchaseDate", getDate_MDY(txtpurchasedate.Text));
                    cmd.Parameters.AddWithValue("@Item_Quantity", txtQuantity.Text);
                    cmd.Parameters.AddWithValue("@Total_Amount", txtAmount.Text);
                    cmd.Parameters.AddWithValue("@Total_Quantity", txttotalquentity.Text);
                    cmd.Parameters.AddWithValue("@CreateBy", "1");
                    cmd.Parameters.AddWithValue("@Createdby_Ip", GetLocalIPAddress());
                    cmd.Parameters.AddWithValue("@IP_Adress", GetLocalIPAddress());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Purchase Item Insert Successfully |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        FillGrid();
                        TextClear();
                        //ModalPopupExtender1.Show();
                        //GetEmployeeDetails();
                        //Getstoragedutysubmision();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                        TextClear();
                    }
                }
                else if (btnsave.Text == "Edit")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Usp_Update_PurchaseItem", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Item_Id", ViewState["Item_Id"].ToString());
                    cmd.Parameters.AddWithValue("@Inventory_Id", ddlitem.SelectedValue);
                    cmd.Parameters.AddWithValue("@Item_Rate", txtprice.Text);
                    cmd.Parameters.AddWithValue("@Available_Quantity", txtAvailable.Text);
                    cmd.Parameters.AddWithValue("@Bill_Number", txtbillnumber.Text);
                    cmd.Parameters.AddWithValue("@Item_PurchaseDate", getDate_MDY(txtpurchasedate.Text));
                    cmd.Parameters.AddWithValue("@Item_Quantity", txtQuantity.Text);
                    cmd.Parameters.AddWithValue("@Total_Amount", txtAmount.Text);
                    cmd.Parameters.AddWithValue("@Total_Quantity", txttotalquentity.Text);
                    cmd.Parameters.AddWithValue("@Updated_by", "2");
                    cmd.Parameters.AddWithValue("@Updatedby_Ip", GetLocalIPAddress());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Purchase Item Update Successfully|||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        FillGrid();
                        TextClear();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
                        TextClear();
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' " + ErrorMsg.ToString() + "')", true);
            }
        }
        catch (Exception ex)
        {

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
            Console.WriteLine(ex.Message);
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
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        int first = 0;
        int second = 0;
        decimal Third = 0;

        if (!string.IsNullOrEmpty(txtQuantity.Text))
        {
            if (Int32.TryParse(txtQuantity.Text, out second) && Int32.TryParse(txtAvailable.Text, out first))
                txttotalquentity.Text = (first + second).ToString();
            if (decimal.TryParse(txtprice.Text, out Third) && Int32.TryParse(txtQuantity.Text, out second))
                txtAmount.Text = (Third * second).ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Item Quantity');", true);
            txttotalquentity.Text = "";
            txtAmount.Text = "";
        }
    }
    protected void GrdItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRecord")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblRowNumber = (Label)row.FindControl("lblRowNumber");
            Label lblInventoryId = (Label)row.FindControl("lblInventoryId");
            Label lblItemRate = (Label)row.FindControl("lblItemRate");
            Label lblAvaiQuantity = (Label)row.FindControl("lblAvaiQuantity");
            Label lblBillNumber = (Label)row.FindControl("lblBillNumber");
            Label lblItemPurchaseDate = (Label)row.FindControl("lblItemPurchaseDate");
            Label lblItemQuantity = (Label)row.FindControl("lblItemQuantity");
            Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");
            Label lblTotalQuantity = (Label)row.FindControl("lblTotalQuantity");
            ViewState["Item_Id"] = lblRowNumber.Text;
            if (e.CommandName == "EditRecord")
            {
                ddlitem.SelectedValue = lblInventoryId.Text;
                txtprice.Text = lblItemRate.Text;
                txtAvailable.Text = lblAvaiQuantity.Text;
                txtbillnumber.Text = lblBillNumber.Text;
                txtpurchasedate.Text = lblItemPurchaseDate.Text;
                txtQuantity.Text = lblItemQuantity.Text;
                txtAmount.Text = lblTotalAmount.Text;
                txttotalquentity.Text = lblTotalQuantity.Text;
            }
            btnsave.Text = "Edit";
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
    protected void TextClear()
    {
        ddlitem.ClearSelection();
        txtprice.Text = "";
        txtAvailable.Text = "";
        txtbillnumber.Text = "";
        txtpurchasedate.Text = "";
        txtQuantity.Text = "";
        txtAmount.Text = "";
        txttotalquentity.Text = "";
    }
}