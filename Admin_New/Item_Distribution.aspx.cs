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

public partial class Admin_New_Item_Distribution : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    public SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
    string con_WLC2 = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
    private object con;
    private object ob_value;
    DataTable dt = new DataTable();
    String isBlock = "";
    string IPAddress;
    int iCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] != null)
        {
            if (!IsPostBack)
            {
                BindItem();
                BindDepartment();
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("/Login/Login.aspx");
        }
    }
    protected void BindItem()
    {
        string strDist = "";
        strDist = "Select Inventory_Id,Inventory_Name from Mst_Inventory";
        SqlDataAdapter da = new SqlDataAdapter(strDist, constr);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlitem.DataSource = ds.Tables[0];
            ddlitem.DataTextField = "Inventory_Name";
            ddlitem.DataValueField = "Inventory_Id";
            ddlitem.DataBind();
            ddlitem.Items.Insert(0, new ListItem("--Select Item--", "0"));
            constr.Close();
        }
    }
    protected void FillGrid()
    {
        using (SqlConnection con = new SqlConnection(con_WLC2))
        {
            SqlCommand cmd = new SqlCommand("Usp_Select_Distribute_Item", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                GrdDistribution.DataSource = dt;
                GrdDistribution.DataBind();
            }
            else
            {
                GrdDistribution.DataSource = null;
                GrdDistribution.DataBind();
            }
        }
    }
    protected void ddlinventory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Fillquantity", con))
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
                                txtremainning.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Total_Quantity"].ToString()) ? ds.Tables[0].Rows[0]["Total_Quantity"].ToString() : "0");
                            }
                            else
                            {
                                txtremainning.Text = "";
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    protected void BindDepartment()
    {
        string strDist = "";
        strDist = "Select Department_Id,Deparment_Name from tbl_Department";
        SqlDataAdapter da = new SqlDataAdapter(strDist, constr);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldepartment.DataSource = ds.Tables[0];
            ddldepartment.DataTextField = "Deparment_Name";
            ddldepartment.DataValueField = "Department_Id";
            ddldepartment.DataBind();
            ddldepartment.Items.Insert(0, new ListItem("--Select Item--", "0"));
            constr.Close();
        }
        else
        {
            ddldepartment.Items.Insert(0, new ListItem("--Select Item--", "0"));
        }

    }
    protected void txtdistribute_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int first = 0;
            int second = 0;
            string strMsg2 = "";
            if (txtremainning.Text != "" && txtdistribute.Text != "")
            {
                if (Int32.Parse(txtdistribute.Text) > Int32.Parse(txtremainning.Text))
                {
                    strMsg2 = strMsg2 + "Sold Item quantity should not be greater than Avl. Item Quantity.";
                    txtdistribute.Text = "";
                    txtremai.Text = "0";
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtdistribute.Text))
                    {
                        if (Int32.TryParse(txtdistribute.Text, out second) && Int32.TryParse(txtremainning.Text, out first))
                            txtremai.Text = (first - second).ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Item Quantity');", true);
                        txtdistribute.Text = "";
                        txtremai.Text = "";
                    }
                }
            }
            if (strMsg2.Trim() == "")
            {
                lblMsg.Text = "";
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
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            //checkvalidation();
            string ErrorMsg = "";
            if (!string.IsNullOrEmpty(txtDate.Text))
            {
                getDate_MDY(txtDate.Text);
            }
            ErrorMsg += ddlitem.SelectedIndex > 0 ? "" : "Please Select Item... \\n";
            ErrorMsg += ddldepartment.SelectedIndex > 0 ? "" : "Please Select Department... \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtDesignationname.Text) ? "" : "Enter Designation Name. \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtemp.Text) ? "" : "Enter Employe Name \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtdistributer.Text) ? "" : "Enter Distributer Name \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtdistribute.Text) ? "" : "Enter Distribute Qty \\n";
            if (ErrorMsg == "")
            {
                if (btnsave.Text == "Save")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Insert_DistributedItem", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Inventory_Id", ddlitem.SelectedValue);
                    cmd.Parameters.AddWithValue("@Available_Qty", txtremainning.Text);
                    cmd.Parameters.AddWithValue("@Distribution_Date", getDate_MDY(txtDate.Text));
                    cmd.Parameters.AddWithValue("@Department_Id", ddldepartment.SelectedValue);
                    cmd.Parameters.AddWithValue("@Designation", txtDesignationname.Text);
                    cmd.Parameters.AddWithValue("@Employee_Name", txtemp.Text);
                    cmd.Parameters.AddWithValue("@Distributer_Name", txtdistributer.Text);
                    cmd.Parameters.AddWithValue("@Distribute_Quantity", txtdistribute.Text);
                    cmd.Parameters.AddWithValue("@Remaining_Quantity", txtremai.Text);
                    cmd.Parameters.AddWithValue("@Remark", txtremark.Text);
                    cmd.Parameters.AddWithValue("@CreateBy", Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@Createdby_Ip", GetLocalIPAddress());
                    cmd.Parameters.AddWithValue("@IP_Adress", GetLocalIPAddress());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Distributed Item Insert Successfully |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        FillGrid();
                        TextClear();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                        TextClear();
                    }
                }
                else if (btnsave.Text == "Edit")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Usp_Update_DistributedItem", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Distribution_Id", ViewState["Distribution_Id"].ToString());
                    cmd.Parameters.AddWithValue("@Inventory_Id", ddlitem.SelectedValue);
                    cmd.Parameters.AddWithValue("@Available_Qty", txtremainning.Text);
                    cmd.Parameters.AddWithValue("@Distribution_Date", getDate_MDY(txtDate.Text));
                    cmd.Parameters.AddWithValue("@Department_Id", ddldepartment.SelectedValue);
                    cmd.Parameters.AddWithValue("@Designation", txtDesignationname.Text);
                    cmd.Parameters.AddWithValue("@Employee_Name", txtemp.Text);
                    cmd.Parameters.AddWithValue("@Distributer_Name", txtdistributer.Text);
                    cmd.Parameters.AddWithValue("@Distribute_Quantity", txtdistribute.Text);
                    cmd.Parameters.AddWithValue("@Remaining_Quantity", txtremai.Text);
                    cmd.Parameters.AddWithValue("@Remark", txtremark.Text);
                    cmd.Parameters.AddWithValue("@Updated_by", Session["UserId"].ToString());
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
            Console.WriteLine(ex.Message);
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
    protected void GrdDistribution_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRecord")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblRowNumber = (Label)row.FindControl("lblRowNumber");
            Label lblInventoryId = (Label)row.FindControl("lblInventoryId");
            Label lblAvailable_Qty = (Label)row.FindControl("lblAvailable_Qty");
            Label lblDistribution_Date = (Label)row.FindControl("lblDistribution_Date");
            Label lblDepartment_Id = (Label)row.FindControl("lblDepartment_Id");
            Label lblDesignation = (Label)row.FindControl("lblDesignation");
            Label lblEmployee_Name = (Label)row.FindControl("lblEmployee_Name");
            Label lblDistributer_Name = (Label)row.FindControl("lblDistributer_Name");
            Label lblDistribute_Quantity = (Label)row.FindControl("lblDistribute_Quantity");
            Label lblRemaining_Quantity = (Label)row.FindControl("lblRemaining_Quantity");
            Label lblRemark = (Label)row.FindControl("lblRemark");
            ViewState["Distribution_Id"] = lblRowNumber.Text;
            if (e.CommandName == "EditRecord")
            {
                ddlitem.SelectedValue = lblInventoryId.Text;
                txtremainning.Text = lblAvailable_Qty.Text;
                txtDate.Text = lblDistribution_Date.Text;
                ddldepartment.SelectedValue = lblDepartment_Id.Text;
                txtDesignationname.Text = lblDesignation.Text;
                txtemp.Text = lblEmployee_Name.Text;
                txtdistributer.Text = lblDistributer_Name.Text;
                txtdistribute.Text = lblDistribute_Quantity.Text;
                txtremai.Text = lblRemaining_Quantity.Text;
                txtremark.Text = lblRemark.Text;
            }
            btnsave.Text = "Edit";
        }
    }
    protected void TextClear()
    {
        ddlitem.ClearSelection();
        ddldepartment.ClearSelection();
        txtremainning.Text = "";
        txtDate.Text = "";
        txtDesignationname.Text = "";
        txtdistributer.Text = "";
        txtdistribute.Text = "";
        txtremai.Text = "";
        txtemp.Text = "";
        txtremark.Text = "";
    }
}