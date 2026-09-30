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
using System.Collections.Generic;

public partial class Inspections_BO_Update_Dead_Stock_Entry_New : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string PFID = "";
    SqlTransaction sqltran;
    int a_id = 0;
    SqlCommand cmd;
    DataTable dt = new DataTable();
    string ID = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        CalendarExtender1.EndDate = DateTime.Now;
        CalendarExtender2.EndDate = DateTime.Now;
        ID = Session["hdnID"].ToString();
        if (!IsPostBack)
        {
            fillCategory();
            FillData();

        }
    }
    public void fillCategory()
    {
        string conStr2 = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Insp_Dead_Dunnage_Stock_Master", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();

            ddlArticles.DataSource = cmd.ExecuteReader();
            ddlArticles.DataTextField = "Category_Name";
            ddlArticles.DataValueField = "ID";
            ddlArticles.DataBind();
            ddlArticles.Items.Insert(0, new ListItem("-- Select Articles --", "0"));
            con.Close();
        }
    }
    public void FillData()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Insp_tbl_Dead_Stock_By_ID", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DID", ID.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            ddlArticles.SelectedValue = dt.Rows[0]["Description"].ToString();
                            if (dt.Rows[0]["Description"].ToString() == "66")
                            {
                                otherartical.Visible = true;
                            }
                            else
                            {
                                otherartical.Visible = false;
                            }
                            txtQty.Text = dt.Rows[0]["Quantity"].ToString();
                            txtPurchaser.Text = dt.Rows[0]["Purchaser_Receiver_Name"].ToString();
                            txtReceiptDate.Text = dt.Rows[0]["Receipt_Date"].ToString();
                            txtBillNo.Text = dt.Rows[0]["Bill_No"].ToString();
                            txtBillDate.Text = dt.Rows[0]["Bill_Date"].ToString();
                            txtCost.Text = dt.Rows[0]["Cost_of_the_Articles"].ToString();
                            txtAccidental.Text = dt.Rows[0]["Accidental_Charge"].ToString();
                            txtTotal.Text = dt.Rows[0]["Total"].ToString();
                            txtRate.Text = dt.Rows[0]["Rate_of_Dep"].ToString();
                            txtDep.Text = dt.Rows[0]["Depreciation"].ToString();
                            txtWritten.Text = dt.Rows[0]["Written_Down_Value"].ToString();
                            txtRemark.Text = dt.Rows[0]["Remarks"].ToString();
                            txtarticalname.Text = dt.Rows[0]["Other_Artical_Name"].ToString();
                            btn_addnewoff.Text = "Update";
                            txtBillNo.Enabled = false;
                        }
                        else
                        {

                            ddlArticles.SelectedValue = "0";
                            txtQty.Text = "";
                            txtPurchaser.Text = "";
                            txtReceiptDate.Text = "";
                            txtBillNo.Text = "";
                            txtBillDate.Text = "";
                            txtCost.Text = "";
                            txtAccidental.Text = "";
                            txtTotal.Text = "";
                            txtRate.Text = "";
                            txtDep.Text = "";
                            txtWritten.Text = "";
                            txtRemark.Text = "";
                            txtBillNo.Enabled = true;
                        }
                    }
                }
            }
        }
    }
    public void checkvalidation()
    {
        if (ddlArticles.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Description of the Articles')", true);
            ddlArticles.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtQty.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Quantity of the Articles can not be blank')", true);
            txtQty.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtPurchaser.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Name of the Party from whom articles purchased/ Received can not be blank')", true);
            txtPurchaser.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtReceiptDate.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Date of Receipt is not valid')", true);
            txtReceiptDate.Focus();
            return;
        }
        //if (string.IsNullOrEmpty(txtBillNo.Text))
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Bill Number can not be blank')", true);
        //    txtBillNo.Focus();
        //    return;
        //}
        //if (string.IsNullOrEmpty(txtBillDate.Text))
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Bill Date can not be blank')", true);
        //    txtBillDate.Focus();
        //    return;
        //}
        if (string.IsNullOrEmpty(txtCost.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Cost of the Articles can not be blank')", true);
            txtCost.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtAccidental.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Accidental Charge can not be blank')", true);
            txtAccidental.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtTotal.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Total can not be blank')", true);
            txtTotal.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtRate.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Rate of Dep. can not be blank')", true);
            txtRate.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtDep.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Depreciation can not be blank')", true);
            txtDep.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtWritten.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Written down Value can not be blank')", true);
            txtWritten.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtRemark.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Remarks can not be blank')", true);
            txtRemark.Focus();
            return;
        }
    }
    public void Clear()
    {
        ddlArticles.SelectedValue = "0";
        txtQty.Text = "";
        txtPurchaser.Text = "";
        txtReceiptDate.Text = "";
        txtBillNo.Text = "";
        txtBillDate.Text = "";
        txtCost.Text = "";
        txtAccidental.Text = "";
        txtTotal.Text = "";
        txtRate.Text = "";
        txtDep.Text = "";
        txtWritten.Text = "";
        txtRemark.Text = "";
        txtarticalname.Text = "";
        txtBillNo.Enabled = true;
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
                SqlCommand cmd = new SqlCommand("Update_Insp_tbl_Dead_Stock_New", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@ID", ID.ToString());
                cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@Article_Category", ddlArticles.SelectedValue);
                cmd.Parameters.AddWithValue("@Quantity", txtQty.Text);
                cmd.Parameters.AddWithValue("@Purchaser_Receiver_Name", txtPurchaser.Text);
                cmd.Parameters.AddWithValue("@Receipt_Date", txtReceiptDate.Text);
                cmd.Parameters.AddWithValue("@Bill_No", txtBillNo.Text);
                cmd.Parameters.AddWithValue("@Bill_Date", txtBillDate.Text);
                cmd.Parameters.AddWithValue("@Cost_of_the_Articles", txtCost.Text);
                cmd.Parameters.AddWithValue("@Accidental_Charge", txtAccidental.Text);
                cmd.Parameters.AddWithValue("@Total", txtTotal.Text);
                cmd.Parameters.AddWithValue("@Rate_of_Dep", txtRate.Text);
                cmd.Parameters.AddWithValue("@Depreciation", txtDep.Text);
                cmd.Parameters.AddWithValue("@Written_Down_Value", txtWritten.Text);
                cmd.Parameters.AddWithValue("@Remarks", txtRemark.Text);
                cmd.Parameters.AddWithValue("@Insert_By", IPAddress);
                cmd.Parameters.AddWithValue("@Other_Artical_Name", txtarticalname.Text);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Dead Stock Details Update Successfully|||";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Inspections/BO/Dead_Stock_Entry_New.aspx';", true);
                    Clear();
                }
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


    protected void ddlArticles_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlArticles.SelectedValue == "66")
        {
            otherartical.Visible = true;
        }
        else
        {
            otherartical.Visible = false;
        }
    }
}