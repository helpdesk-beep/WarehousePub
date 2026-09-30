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

public partial class Inspections_State_Add_Item : System.Web.UI.Page
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
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        if ((Session["UserId"] != null))
        {
            if (!IsPostBack)
            {
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/Inspections/Default.aspx");
        }
    }
    protected void FillGrid()
    {
        using (SqlConnection con = new SqlConnection(con_WLC2))
        {
            SqlCommand cmd = new SqlCommand("Usp_SelectInventory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Region_ID", Session["UserId"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                Grdinventory.DataSource = dt;
                Grdinventory.DataBind();

                //lblOfficerList.Text = Convert.ToString(dt.Rows.Count);
                //ViewState["Region"] = dt;
            }
            else
            {
                Grdinventory.DataSource = null;
                Grdinventory.DataBind();
                //lblOfficerList.Text = "0";
            }
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        try
        {
            string ErrorMsg = "";
            ErrorMsg += !string.IsNullOrEmpty(txtItemName.Text) ? "" : "Enter Item Name \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtPrice.Text) ? "" : "Enter Item Price \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtavailablequan.Text) ? "" : "Enter Item Available Quantity \\n";
            if (ErrorMsg == "")
            {
                if (btnsave.Text == "Save")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("InsertItem", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Inventory_Name", txtItemName.Text);
                    cmd.Parameters.AddWithValue("@Inventory_Rate", Convert.ToDecimal(txtPrice.Text).ToString());
                    cmd.Parameters.AddWithValue("@Inventory_Quentity", txtavailablequan.Text);
                    cmd.Parameters.AddWithValue("@Createdby_Ip", ipAddress);
                    cmd.Parameters.AddWithValue("@IP_Adress", ipAddress);
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Add Item Successfully |||";
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
                    string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Usp_UpdateItem", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Inventory_Id", ViewState["Inventory_Id"].ToString());
                    cmd.Parameters.AddWithValue("@Inventory_Name", txtItemName.Text);
                    cmd.Parameters.AddWithValue("@Inventory_Rate", Convert.ToDecimal(txtPrice.Text).ToString());
                    cmd.Parameters.AddWithValue("@Inventory_Quentity", txtavailablequan.Text);
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Item Update Successfully|||";
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
    protected void TextClear()
    {
        txtItemName.Text = "";
        txtPrice.Text = "";
        txtavailablequan.Text = "";
    }
    protected void Grdinventory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRecord")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblRowNumber = (Label)row.FindControl("lblRowNumber");
            Label lblInventoryName = (Label)row.FindControl("lblInventoryName");
            Label lblInventoryRate = (Label)row.FindControl("lblInventoryRate");
            Label lblQuentity = (Label)row.FindControl("lblQuentity");
            ViewState["Inventory_Id"] = lblRowNumber.Text;
            if (e.CommandName == "EditRecord")
            {
                txtItemName.Text = lblInventoryName.Text;
                txtPrice.Text = lblInventoryRate.Text;
                txtavailablequan.Text = lblQuentity.Text;
            }
            btnsave.Text = "Edit";
        }
    }
}