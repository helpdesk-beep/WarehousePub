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

public partial class Inspections_BO_Update_Transfer_Entry : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    public SqlConnection con_JVS = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
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
                FillGrid();
                fillGodownDetails();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void fillGodownDetails()
    {
        using (SqlConnection con = new SqlConnection(constr2))
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
    private void FillGrid()
    {
        SqlCommand cmdd = new SqlCommand("Sp_Insecticide_Transfer_entry_ID", con_JVS);
        cmdd.CommandType = CommandType.StoredProcedure;
        cmdd.Parameters.AddWithValue("@ID", Session["hdnID"].ToString());
        //conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlinsecticide.SelectedValue = dt.Rows[0]["Insecticide_ID"].ToString();
            ddlUnitStock.SelectedValue = dt.Rows[0]["Unit"].ToString();
            txtTfQuantity.Text = dt.Rows[0]["Transfer_Balance_quantity"].ToString();
            txtTfMarketRate.Text = dt.Rows[0]["Transfer_Balance_market_value"].ToString();
            txtTfValue.Value = dt.Rows[0]["Transfer_Balance_value"].ToString();
            txtdob.Text = dt.Rows[0]["Date"].ToString();
            fillGodownDetails();
            ddl_gdwn.SelectedValue = dt.Rows[0]["Godown_No"].ToString();
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlinsecticide.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('select ')", true);
            }

            //SqlCommand cmd = new SqlCommand("Sp_Insecticide_Transfer_entry", con_JVS);
            SqlCommand cmd = new SqlCommand("Sp_Update_Insecticide_Transfer_entry", con_JVS);
            cmd.CommandType = CommandType.StoredProcedure;
            con_JVS.Open();
            cmd.Parameters.AddWithValue("@ID", Session["hdnID"].ToString());
            cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@Insecticide_ID", ddlinsecticide.SelectedValue);
            cmd.Parameters.AddWithValue("@Entry_Type_ID", DropDownList1.SelectedValue);

            cmd.Parameters.AddWithValue("@Transfer_Balance_quantity", txtTfQuantity.Text);
            cmd.Parameters.AddWithValue("@Transfer_Balance_market_value", txtTfMarketRate.Text);
            cmd.Parameters.AddWithValue("@Transfer_Balance_value", txtTfValue.Value);

            cmd.Parameters.AddWithValue("@Godown_No", ddl_gdwn.SelectedValue);
            cmd.Parameters.AddWithValue("@Unit", ddlUnitStock.SelectedValue);
            cmd.Parameters.AddWithValue("@date", getDate_MDY(txtdob.Text));
            cmd.Parameters.AddWithValue("@created_by", Request.UserHostAddress);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Record Update Successfully |||";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Inspections/BO/frm_AddInsecticide_Transfer_Entry.aspx';", true);
                clear();
                con_JVS.Close();
            }
        }
        catch (Exception ex)
        {
            string except = ex.Message.ToString();

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + except + "')", true);
        }
        finally
        {
            con_JVS.Close();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    public void clear()
    {
        ddlinsecticide.SelectedValue = "0";
        ddlUnitStock.SelectedValue = "0";
        txtTfQuantity.Text = "";
        txtTfMarketRate.Text = "";
        txtTfValue.Value = "";
        txtdob.Text = "";
        ddl_gdwn.SelectedValue = "0";
    }


    protected void txtTfMarketRate_TextChanged(object sender, EventArgs e)
    {
        Decimal val1 = Convert.ToDecimal(txtTfQuantity.Text);
        Decimal val2 = Convert.ToDecimal(txtTfMarketRate.Text);
        Decimal val3 = val1 * val2;
        txtTfValue.Value = val3.ToString();
    }

    protected void txtTfQuantity_TextChanged(object sender, EventArgs e)
    {
        Decimal val1 = Convert.ToDecimal(txtTfQuantity.Text);
        Decimal val2 = Convert.ToDecimal(txtTfMarketRate.Text);
        Decimal val3 = val1 * val2;
        txtTfValue.Value = val3.ToString();
    }
}
