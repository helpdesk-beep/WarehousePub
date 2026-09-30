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

public partial class Accounting_frm_Business_Prapatra_II : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                fillCommodity();
                GetEmployeeDetails();
                txtTGC1.Attributes.Add("readonly", "readonly");
                txtTCC2.Attributes.Add("readonly", "readonly");
                txtTGC3.Attributes.Add("readonly", "readonly");
                txtTCC4.Attributes.Add("readonly", "readonly");

                txttotalab.Attributes.Add("readonly", "readonly");
                txtTotalGodownCap5.Attributes.Add("readonly", "readonly");
                txtTcapCapacity5.Attributes.Add("readonly", "readonly");

            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
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

            ddlcommodity.DataSource = cmd.ExecuteReader();
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, new ListItem("-- Select Commodity --", "0"));
            con.Close();
        }
    }
    protected void GetEmployeeDetails()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Prapatra_II_For_BM", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
                cmd.Parameters.AddWithValue("@Commodity_ID", ddlcommodity.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                            GrdPrapatraI.DataSource = dt;
                            GrdPrapatraI.DataBind();

                            ddlcropyear.SelectedValue = dt.Rows[0]["Year"].ToString();
                            fillCommodity();
                            ddlcommodity.SelectedValue = dt.Rows[0]["Commodity_ID"].ToString();
                            txtweight.Text = dt.Rows[0]["Weight"].ToString();
                            txtmotaanaj.Text = dt.Rows[0]["Fat_grain_Weight"].ToString();
                            txttotalab.Text = dt.Rows[0]["Total_A_B"].ToString();
                            txtIntimationRegDate.Text = dt.Rows[0]["Date1"].ToString();
                            txtMpwlcown1.Text = dt.Rows[0]["MPWLC_OWN1"].ToString();
                            txtMpwlcJVS1.Text = dt.Rows[0]["MPWLC_JVS1"].ToString();
                            txtHiredA1.Text = dt.Rows[0]["MPWLC_HA1"].ToString();
                            txtCWC1.Text = dt.Rows[0]["MPWLC_CWC1"].ToString();
                            txtAPPG1.Text = dt.Rows[0]["Arremented_PVT_PEG1"].ToString();
                            txtMarkfed1.Text = dt.Rows[0]["Markfed1"].ToString();
                            txtoilfed1.Text = dt.Rows[0]["OILFED1"].ToString();
                            txtTGC1.Text = dt.Rows[0]["Total_Godown_Capacity1"].ToString();

                            txtMpwlcown2.Text = dt.Rows[0]["MPWLC_OWN2"].ToString();
                            txtMMC2.Text = dt.Rows[0]["MPWLC_Mandi_Cap2"].ToString();
                            txtMMS2.Text = dt.Rows[0]["MPWLC_Mandi_Shed2"].ToString();
                            txtMarkfed2.Text = dt.Rows[0]["Markfed2"].ToString();
                            txtPPC2.Text = dt.Rows[0]["Pvt_PEG_Cap2"].ToString();
                            txtTCC2.Text = dt.Rows[0]["Total_Cap_Capacity2"].ToString();

                            //txtIntimationRegDatetodaty = dt.Rows[0]["date3"].ToString();
                            //txtIntimationRegDate.Text = dt.Rows[0]["date1"].ToString();
                            txtIntimationRegDateto.Text = dt.Rows[0]["date2"].ToString();
                            txtMpwlcown3.Text = dt.Rows[0]["MPWLC_OWN3"].ToString();
                            txtMpwlcJVS3.Text = dt.Rows[0]["MPWLC_JVS3"].ToString();
                            txtMpwlcHA3.Text = dt.Rows[0]["MPWLC_HA3"].ToString();
                            txtCWC3.Text = dt.Rows[0]["MPWLC_CWC3"].ToString();
                            txtAPPG3.Text = dt.Rows[0]["Arremented_PVT_PEG3"].ToString();
                            txtMarkfed3.Text = dt.Rows[0]["Markfed3"].ToString();
                            txtoilfed3.Text = dt.Rows[0]["OILFED3"].ToString();
                            txtTGC3.Text = dt.Rows[0]["Total_Godown_Capacity3"].ToString();
                            txtMpwlcown4.Text = dt.Rows[0]["MPWLC_OWN4"].ToString();
                            txtMMC4.Text = dt.Rows[0]["MPWLC_Mandi_Cap4"].ToString();
                            txtMMS4.Text = dt.Rows[0]["MPWLC_Mandi_Shed4"].ToString();
                            txtMarkfed4.Text = dt.Rows[0]["Markfed4"].ToString();
                            txtPPC4.Text = dt.Rows[0]["Pvt_PEG_Cap4"].ToString();
                            txtTCC4.Text = dt.Rows[0]["Total_Cap_Capacity4"].ToString();

                            txtMpwlcOwn5.Text = dt.Rows[0]["MPWLC_OWN5"].ToString();
                            txtMpwlcJvs5.Text = dt.Rows[0]["MPWLC_JVS5"].ToString();
                            txtMpwlcHired5.Text = dt.Rows[0]["MPWLC_HA5"].ToString();
                            txtCwc5.Text = dt.Rows[0]["MPWLC_CWC5"].ToString();
                            txtArremPvtPegGo5.Text = dt.Rows[0]["Arremented_PVT_PEG5"].ToString();
                            txtMarkfrd5.Text = dt.Rows[0]["Markfed5"].ToString();
                            txtOilfed5.Text = dt.Rows[0]["OILFED5"].ToString();
                            txtTotalGodownCap5.Text = dt.Rows[0]["Total_Godown_Capacity5"].ToString();
                            txtMPwlcown6.Text = dt.Rows[0]["MPWLC_OWN6"].ToString();
                            txtMpwlcmanCap5.Text = dt.Rows[0]["MPWLC_Mandi_Cap5"].ToString();
                            txtMpwlcmandished5.Text = dt.Rows[0]["MPWLC_Mandi_Shed5"].ToString();
                            txtMarked6.Text = dt.Rows[0]["Markfed6"].ToString();
                            txtPvtpegcap5.Text = dt.Rows[0]["Pvt_PEG_Cap5"].ToString();
                            txtTcapCapacity5.Text = dt.Rows[0]["Total_Cap_Capacity5"].ToString();

                            Session["Date1"] = dt.Rows[0]["Date1"].ToString();
                            Session["Date2"] = dt.Rows[0]["Date2"].ToString();
                            Session["Date3"] = dt.Rows[0]["Date3"].ToString();

                            ViewState["Date1"] = dt.Rows[0]["Date1"].ToString();
                        }
                        else
                        {
                            GrdPrapatraI.DataSource = null;
                            GrdPrapatraI.DataBind();
                        }

                    }

                }
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }



    protected void txtMpwlcown1_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown1.Text);
        decimal B = decimal.Parse(txtMpwlcJVS1.Text);
        decimal C = decimal.Parse(txtHiredA1.Text);
        decimal D = decimal.Parse(txtCWC1.Text);
        decimal E = decimal.Parse(txtAPPG1.Text);
        decimal F = decimal.Parse(txtMarkfed1.Text);
        decimal G = decimal.Parse(txtoilfed1.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTGC1.Text = i.ToString();
    }
    protected void txtMpwlcJVS1_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown1.Text);
        decimal B = decimal.Parse(txtMpwlcJVS1.Text);
        decimal C = decimal.Parse(txtHiredA1.Text);
        decimal D = decimal.Parse(txtCWC1.Text);
        decimal E = decimal.Parse(txtAPPG1.Text);
        decimal F = decimal.Parse(txtMarkfed1.Text);
        decimal G = decimal.Parse(txtoilfed1.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTGC1.Text = i.ToString();
    }
    protected void txtHiredA1_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown1.Text);
        decimal B = decimal.Parse(txtMpwlcJVS1.Text);
        decimal C = decimal.Parse(txtHiredA1.Text);
        decimal D = decimal.Parse(txtCWC1.Text);
        decimal E = decimal.Parse(txtAPPG1.Text);
        decimal F = decimal.Parse(txtMarkfed1.Text);
        decimal G = decimal.Parse(txtoilfed1.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTGC1.Text = i.ToString();
    }
    protected void txtCWC1_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown1.Text);
        decimal B = decimal.Parse(txtMpwlcJVS1.Text);
        decimal C = decimal.Parse(txtHiredA1.Text);
        decimal D = decimal.Parse(txtCWC1.Text);
        decimal E = decimal.Parse(txtAPPG1.Text);
        decimal F = decimal.Parse(txtMarkfed1.Text);
        decimal G = decimal.Parse(txtoilfed1.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTGC1.Text = i.ToString();
    }
    protected void txtAPPG1_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown1.Text);
        decimal B = decimal.Parse(txtMpwlcJVS1.Text);
        decimal C = decimal.Parse(txtHiredA1.Text);
        decimal D = decimal.Parse(txtCWC1.Text);
        decimal E = decimal.Parse(txtAPPG1.Text);
        decimal F = decimal.Parse(txtMarkfed1.Text);
        decimal G = decimal.Parse(txtoilfed1.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTGC1.Text = i.ToString();
    }
    protected void txtMarkfed1_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown1.Text);
        decimal B = decimal.Parse(txtMpwlcJVS1.Text);
        decimal C = decimal.Parse(txtHiredA1.Text);
        decimal D = decimal.Parse(txtCWC1.Text);
        decimal E = decimal.Parse(txtAPPG1.Text);
        decimal F = decimal.Parse(txtMarkfed1.Text);
        decimal G = decimal.Parse(txtoilfed1.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTGC1.Text = i.ToString();
    }
    protected void txtoilfed1_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown1.Text);
        decimal B = decimal.Parse(txtMpwlcJVS1.Text);
        decimal C = decimal.Parse(txtHiredA1.Text);
        decimal D = decimal.Parse(txtCWC1.Text);
        decimal E = decimal.Parse(txtAPPG1.Text);
        decimal F = decimal.Parse(txtMarkfed1.Text);
        decimal G = decimal.Parse(txtoilfed1.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTGC1.Text = i.ToString();
    }
    protected void txtMpwlcown2_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown2.Text);
        decimal B = decimal.Parse(txtMMC2.Text);
        decimal C = decimal.Parse(txtMMS2.Text);
        decimal D = decimal.Parse(txtMarkfed2.Text);
        decimal E = decimal.Parse(txtPPC2.Text);

        decimal i = A + B + C + D + E;
        i = A + B + C + D + E;
        txtTCC2.Text = i.ToString();
    }
    protected void txtMMC2_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown2.Text);
        decimal B = decimal.Parse(txtMMC2.Text);
        decimal C = decimal.Parse(txtMMS2.Text);
        decimal D = decimal.Parse(txtMarkfed2.Text);
        decimal E = decimal.Parse(txtPPC2.Text);

        decimal i = A + B + C + D + E;
        i = A + B + C + D + E;
        txtTCC2.Text = i.ToString();
    }
    protected void txtMMS2_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown2.Text);
        decimal B = decimal.Parse(txtMMC2.Text);
        decimal C = decimal.Parse(txtMMS2.Text);
        decimal D = decimal.Parse(txtMarkfed2.Text);
        decimal E = decimal.Parse(txtPPC2.Text);

        decimal i = A + B + C + D + E;
        i = A + B + C + D + E;
        txtTCC2.Text = i.ToString();
    }
    protected void txtMarkfed2_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown2.Text);
        decimal B = decimal.Parse(txtMMC2.Text);
        decimal C = decimal.Parse(txtMMS2.Text);
        decimal D = decimal.Parse(txtMarkfed2.Text);
        decimal E = decimal.Parse(txtPPC2.Text);

        decimal i = A + B + C + D + E;
        i = A + B + C + D + E;
        txtTCC2.Text = i.ToString();
    }
    protected void txtPPC2_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown2.Text);
        decimal B = decimal.Parse(txtMMC2.Text);
        decimal C = decimal.Parse(txtMMS2.Text);
        decimal D = decimal.Parse(txtMarkfed2.Text);
        decimal E = decimal.Parse(txtPPC2.Text);

        decimal i = A + B + C + D + E;
        i = A + B + C + D + E;
        txtTCC2.Text = i.ToString();
    }
    protected void txtMpwlcown3_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown3.Text);
        decimal B = decimal.Parse(txtMpwlcJVS3.Text);
        decimal C = decimal.Parse(txtMpwlcHA3.Text);
        decimal D = decimal.Parse(txtCWC3.Text);
        decimal E = decimal.Parse(txtAPPG3.Text);
        decimal F = decimal.Parse(txtMarkfed3.Text);
        decimal G = decimal.Parse(txtoilfed3.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTGC3.Text = i.ToString();
    }
    protected void txtMpwlcJVS3_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown3.Text);
        decimal B = decimal.Parse(txtMpwlcJVS3.Text);
        decimal C = decimal.Parse(txtMpwlcHA3.Text);
        decimal D = decimal.Parse(txtCWC3.Text);
        decimal E = decimal.Parse(txtAPPG3.Text);
        decimal F = decimal.Parse(txtMarkfed3.Text);
        decimal G = decimal.Parse(txtoilfed3.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTGC3.Text = i.ToString();
    }
    protected void txtMpwlcHA3_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown3.Text);
        decimal B = decimal.Parse(txtMpwlcJVS3.Text);
        decimal C = decimal.Parse(txtMpwlcHA3.Text);
        decimal D = decimal.Parse(txtCWC3.Text);
        decimal E = decimal.Parse(txtAPPG3.Text);
        decimal F = decimal.Parse(txtMarkfed3.Text);
        decimal G = decimal.Parse(txtoilfed3.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTGC3.Text = i.ToString();
    }
    protected void txtCWC3_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown3.Text);
        decimal B = decimal.Parse(txtMpwlcJVS3.Text);
        decimal C = decimal.Parse(txtMpwlcHA3.Text);
        decimal D = decimal.Parse(txtCWC3.Text);
        decimal E = decimal.Parse(txtAPPG3.Text);
        decimal F = decimal.Parse(txtMarkfed3.Text);
        decimal G = decimal.Parse(txtoilfed3.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTGC3.Text = i.ToString();
    }
    protected void txtAPPG3_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown3.Text);
        decimal B = decimal.Parse(txtMpwlcJVS3.Text);
        decimal C = decimal.Parse(txtMpwlcHA3.Text);
        decimal D = decimal.Parse(txtCWC3.Text);
        decimal E = decimal.Parse(txtAPPG3.Text);
        decimal F = decimal.Parse(txtMarkfed3.Text);
        decimal G = decimal.Parse(txtoilfed3.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTGC3.Text = i.ToString();
    }
    protected void txtMarkfed3_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown3.Text);
        decimal B = decimal.Parse(txtMpwlcJVS3.Text);
        decimal C = decimal.Parse(txtMpwlcHA3.Text);
        decimal D = decimal.Parse(txtCWC3.Text);
        decimal E = decimal.Parse(txtAPPG3.Text);
        decimal F = decimal.Parse(txtMarkfed3.Text);
        decimal G = decimal.Parse(txtoilfed3.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTGC3.Text = i.ToString();
    }
    protected void txtoilfed3_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown3.Text);
        decimal B = decimal.Parse(txtMpwlcJVS3.Text);
        decimal C = decimal.Parse(txtMpwlcHA3.Text);
        decimal D = decimal.Parse(txtCWC3.Text);
        decimal E = decimal.Parse(txtAPPG3.Text);
        decimal F = decimal.Parse(txtMarkfed3.Text);
        decimal G = decimal.Parse(txtoilfed3.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTGC3.Text = i.ToString();
    }
    protected void txtMpwlcown4_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown4.Text);
        decimal B = decimal.Parse(txtMMC4.Text);
        decimal C = decimal.Parse(txtMMS4.Text);
        decimal D = decimal.Parse(txtMarkfed4.Text);
        decimal E = decimal.Parse(txtPPC4.Text);

        decimal i = A + B + C + D + E;
        i = A + B + C + D + E;
        txtTCC4.Text = i.ToString();
    }
    protected void txtMMC4_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown4.Text);
        decimal B = decimal.Parse(txtMMC4.Text);
        decimal C = decimal.Parse(txtMMS4.Text);
        decimal D = decimal.Parse(txtMarkfed4.Text);
        decimal E = decimal.Parse(txtPPC4.Text);

        decimal i = A + B + C + D + E;
        i = A + B + C + D + E;
        txtTCC4.Text = i.ToString();
    }
    protected void txtMMS4_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown4.Text);
        decimal B = decimal.Parse(txtMMC4.Text);
        decimal C = decimal.Parse(txtMMS4.Text);
        decimal D = decimal.Parse(txtMarkfed4.Text);
        decimal E = decimal.Parse(txtPPC4.Text);

        decimal i = A + B + C + D + E;
        i = A + B + C + D + E;
        txtTCC4.Text = i.ToString();
    }
    protected void txtMarkfed4_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown4.Text);
        decimal B = decimal.Parse(txtMMC4.Text);
        decimal C = decimal.Parse(txtMMS4.Text);
        decimal D = decimal.Parse(txtMarkfed4.Text);
        decimal E = decimal.Parse(txtPPC4.Text);

        decimal i = A + B + C + D + E;
        i = A + B + C + D + E;
        txtTCC4.Text = i.ToString();
    }
    protected void txtPPC4_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcown4.Text);
        decimal B = decimal.Parse(txtMMC4.Text);
        decimal C = decimal.Parse(txtMMS4.Text);
        decimal D = decimal.Parse(txtMarkfed4.Text);
        decimal E = decimal.Parse(txtPPC4.Text);

        decimal i = A + B + C + D + E;
        i = A + B + C + D + E;
        txtTCC4.Text = i.ToString();
    }
    public void checkvalidation()
    {
        if (string.IsNullOrEmpty(txtIntimationRegDate.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Date')", true);
            txtIntimationRegDate.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtIntimationRegDateto.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Date')", true);
            txtIntimationRegDateto.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtIntimationRegDatetodaty.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Date')", true);
            txtIntimationRegDatetodaty.Focus();
            return;
        }
    }
    protected void btnSumbmitRent_Click(object sender, EventArgs e)
    {
        try
        {
            checkvalidation();
            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Business_Prapatra_II_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
                cmd.Parameters.AddWithValue("@Commodity_ID", ddlcommodity.SelectedValue);
                cmd.Parameters.AddWithValue("@Weight", txtweight.Text.ToString());
                cmd.Parameters.AddWithValue("@Fat_grain_Weight", txtmotaanaj.Text.ToString());
                cmd.Parameters.AddWithValue("@Total_A_B", txttotalab.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_OWN1", txtMpwlcown1.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_JVS1", txtMpwlcJVS1.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_HA1", txtHiredA1.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_CWC1", txtCWC1.Text.ToString());
                cmd.Parameters.AddWithValue("@Arremented_PVT_PEG1", txtAPPG1.Text.ToString());
                cmd.Parameters.AddWithValue("@Markfed1", txtMarkfed1.Text.ToString());
                cmd.Parameters.AddWithValue("@OILFED1", txtoilfed1.Text.ToString());
                cmd.Parameters.AddWithValue("@Total_Godown_Capacity1", txtTGC1.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_OWN2", txtMpwlcown2.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_Mandi_Cap2", txtMMC2.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_Mandi_Shed2", txtMMS2.Text.ToString());
                cmd.Parameters.AddWithValue("@Markfed2", txtMarkfed2.Text.ToString());
                cmd.Parameters.AddWithValue("@Pvt_PEG_Cap2", txtPPC2.Text.ToString());
                cmd.Parameters.AddWithValue("@Total_Cap_Capacity2", txtTCC2.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_OWN3", txtMpwlcown3.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_JVS3", txtMpwlcJVS3.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_HA3", txtMpwlcHA3.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_CWC3", txtCWC3.Text.ToString());
                cmd.Parameters.AddWithValue("@Arremented_PVT_PEG3", txtAPPG3.Text.ToString());
                cmd.Parameters.AddWithValue("@Markfed3", txtMarkfed3.Text.ToString());
                cmd.Parameters.AddWithValue("@OILFED3", txtoilfed3.Text.ToString());
                cmd.Parameters.AddWithValue("@Total_Godown_Capacity3", txtTGC3.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_OWN4", txtMpwlcown4.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_Mandi_Cap4", txtMMC4.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_Mandi_Shed4", txtMMS4.Text.ToString());
                cmd.Parameters.AddWithValue("@Markfed4", txtMarkfed4.Text.ToString());
                cmd.Parameters.AddWithValue("@Pvt_PEG_Cap4", txtPPC4.Text.ToString());
                cmd.Parameters.AddWithValue("@Total_Cap_Capacity4", txtTCC4.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_OWN5", txtMpwlcOwn5.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_JVS5", txtMpwlcJvs5.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_HA5", txtMpwlcHired5.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_CWC5", txtCwc5.Text.ToString());
                cmd.Parameters.AddWithValue("@Arremented_PVT_PEG5", txtArremPvtPegGo5.Text.ToString());
                cmd.Parameters.AddWithValue("@Markfed5", txtMarkfrd5.Text.ToString());
                cmd.Parameters.AddWithValue("@OILFED5", txtOilfed5.Text.ToString());
                cmd.Parameters.AddWithValue("@Total_Godown_Capacity5", txtTotalGodownCap5.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_OWN6", txtMPwlcown6.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_Mandi_Cap5", txtMpwlcmanCap5.Text.ToString());
                cmd.Parameters.AddWithValue("@MPWLC_Mandi_Shed5", txtMpwlcmandished5.Text.ToString());
                cmd.Parameters.AddWithValue("@Markfed6", txtMarked6.Text.ToString());
                cmd.Parameters.AddWithValue("@Pvt_PEG_Cap5", txtPvtpegcap5.Text.ToString());
                cmd.Parameters.AddWithValue("@Total_Cap_Capacity5", txtTcapCapacity5.Text.ToString());
                cmd.Parameters.AddWithValue("@Inserted_By", IPAddress.ToString());
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.ToString());
                cmd.Parameters.AddWithValue("@Year", ddlcropyear.SelectedValue);
                cmd.Parameters.AddWithValue("@Date1", getDate_MDY(txtIntimationRegDate.Text));
                cmd.Parameters.AddWithValue("@Date2", getDate_MDY(txtIntimationRegDateto.Text));
                cmd.Parameters.AddWithValue("@Date3", getDate_MDY(txtIntimationRegDatetodaty.Text));
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Record Save Successfully |||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    GetEmployeeDetails();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
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

    protected void txtweight_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtweight.Text);
        decimal B = decimal.Parse(txtmotaanaj.Text);

        decimal i = A + B ;
        i = A + B ;
        txttotalab.Text = i.ToString();
    }

    protected void txtmotaanaj_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtweight.Text);
        decimal B = decimal.Parse(txtmotaanaj.Text);

        decimal i = A + B;
        i = A + B;
        txttotalab.Text = i.ToString();
    }

    //protected void fillScheduleInsp_Grid()
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("SP_Godown_Business_Prapatra_Stock", con))
    //        {
    //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchID"]);              
    //            using (SqlDataAdapter sda = new SqlDataAdapter())
    //            {
    //                cmd.Connection = con;
    //                sda.SelectCommand = cmd;
    //                using (DataTable dt = new DataTable())
    //                {
    //                    sda.Fill(dt);
    //                    if (dt.Rows.Count > 0)
    //                    {                         
                  
    //                        GrdOfficerPreviousInsp.DataSource = dt;
    //                        GrdOfficerPreviousInsp.DataBind();
    //                    }
    //                    else
    //                    {
                        
    //                        GrdOfficerPreviousInsp.DataSource = null;
    //                        GrdOfficerPreviousInsp.DataBind();
    //                   }
    //                }
    //            }
    //        }
    //    }
    //}
    protected void txtPvtpegcap5_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMPwlcown6.Text);
        decimal B = decimal.Parse(txtMpwlcmanCap5.Text);
        decimal C = decimal.Parse(txtMpwlcmandished5.Text);
        decimal D = decimal.Parse(txtMarked6.Text);
        decimal E = decimal.Parse(txtPvtpegcap5.Text);

        decimal i = A + B + C + D + E;
        i = A + B + C + D + E;
        txtTcapCapacity5.Text = i.ToString();
    }

    protected void txtMarked6_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMPwlcown6.Text);
        decimal B = decimal.Parse(txtMpwlcmanCap5.Text);
        decimal C = decimal.Parse(txtMpwlcmandished5.Text);
        decimal D = decimal.Parse(txtMarked6.Text);
        decimal E = decimal.Parse(txtPvtpegcap5.Text);

        decimal i = A + B + C + D + E;
        i = A + B + C + D + E;
        txtTcapCapacity5.Text = i.ToString();
    }

    protected void txtMpwlcOwn5_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcOwn5.Text);
        decimal B = decimal.Parse(txtMpwlcJvs5.Text);
        decimal C = decimal.Parse(txtMpwlcHired5.Text);
        decimal D = decimal.Parse(txtCwc5.Text);
        decimal E = decimal.Parse(txtArremPvtPegGo5.Text);
        decimal F = decimal.Parse(txtMarkfrd5.Text);
        decimal G = decimal.Parse(txtOilfed5.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTotalGodownCap5.Text = i.ToString();
    }

    protected void txtMpwlcJvs5_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcOwn5.Text);
        decimal B = decimal.Parse(txtMpwlcJvs5.Text);
        decimal C = decimal.Parse(txtMpwlcHired5.Text);
        decimal D = decimal.Parse(txtCwc5.Text);
        decimal E = decimal.Parse(txtArremPvtPegGo5.Text);
        decimal F = decimal.Parse(txtMarkfrd5.Text);
        decimal G = decimal.Parse(txtOilfed5.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTotalGodownCap5.Text = i.ToString();
    }

    protected void txtMpwlcHired5_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcOwn5.Text);
        decimal B = decimal.Parse(txtMpwlcJvs5.Text);
        decimal C = decimal.Parse(txtMpwlcHired5.Text);
        decimal D = decimal.Parse(txtCwc5.Text);
        decimal E = decimal.Parse(txtArremPvtPegGo5.Text);
        decimal F = decimal.Parse(txtMarkfrd5.Text);
        decimal G = decimal.Parse(txtOilfed5.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTotalGodownCap5.Text = i.ToString();
    }

    protected void txtArremPvtPegGo5_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcOwn5.Text);
        decimal B = decimal.Parse(txtMpwlcJvs5.Text);
        decimal C = decimal.Parse(txtMpwlcHired5.Text);
        decimal D = decimal.Parse(txtCwc5.Text);
        decimal E = decimal.Parse(txtArremPvtPegGo5.Text);
        decimal F = decimal.Parse(txtMarkfrd5.Text);
        decimal G = decimal.Parse(txtOilfed5.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTotalGodownCap5.Text = i.ToString();
    }

    protected void txtMarkfrd5_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcOwn5.Text);
        decimal B = decimal.Parse(txtMpwlcJvs5.Text);
        decimal C = decimal.Parse(txtMpwlcHired5.Text);
        decimal D = decimal.Parse(txtCwc5.Text);
        decimal E = decimal.Parse(txtArremPvtPegGo5.Text);
        decimal F = decimal.Parse(txtMarkfrd5.Text);
        decimal G = decimal.Parse(txtOilfed5.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTotalGodownCap5.Text = i.ToString();
    }

    protected void txtOilfed5_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcOwn5.Text);
        decimal B = decimal.Parse(txtMpwlcJvs5.Text);
        decimal C = decimal.Parse(txtMpwlcHired5.Text);
        decimal D = decimal.Parse(txtCwc5.Text);
        decimal E = decimal.Parse(txtArremPvtPegGo5.Text);
        decimal F = decimal.Parse(txtMarkfrd5.Text);
        decimal G = decimal.Parse(txtOilfed5.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTotalGodownCap5.Text = i.ToString();
    }

    protected void txtMPwlcown6_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMPwlcown6.Text);
        decimal B = decimal.Parse(txtMpwlcmanCap5.Text);
        decimal C = decimal.Parse(txtMpwlcmandished5.Text);
        decimal D = decimal.Parse(txtMarked6.Text);
        decimal E = decimal.Parse(txtPvtpegcap5.Text);

        decimal i = A + B + C + D + E;
        i = A + B + C + D + E;
        txtTcapCapacity5.Text = i.ToString();
    }

    protected void txtMpwlcmanCap5_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMPwlcown6.Text);
        decimal B = decimal.Parse(txtMpwlcmanCap5.Text);
        decimal C = decimal.Parse(txtMpwlcmandished5.Text);
        decimal D = decimal.Parse(txtMarked6.Text);
        decimal E = decimal.Parse(txtPvtpegcap5.Text);

        decimal i = A + B + C + D + E;
        i = A + B + C + D + E;
        txtTcapCapacity5.Text = i.ToString();
    }

    protected void txtMpwlcmandished5_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMPwlcown6.Text);
        decimal B = decimal.Parse(txtMpwlcmanCap5.Text);
        decimal C = decimal.Parse(txtMpwlcmandished5.Text);
        decimal D = decimal.Parse(txtMarked6.Text);
        decimal E = decimal.Parse(txtPvtpegcap5.Text);

        decimal i = A + B + C + D + E;
        i = A + B + C + D + E;
        txtTcapCapacity5.Text = i.ToString();
    }

    protected void txtCwc5_TextChanged(object sender, EventArgs e)
    {
        decimal A = decimal.Parse(txtMpwlcOwn5.Text);
        decimal B = decimal.Parse(txtMpwlcJvs5.Text);
        decimal C = decimal.Parse(txtMpwlcHired5.Text);
        decimal D = decimal.Parse(txtCwc5.Text);
        decimal E = decimal.Parse(txtArremPvtPegGo5.Text);
        decimal F = decimal.Parse(txtMarkfrd5.Text);
        decimal G = decimal.Parse(txtOilfed5.Text);

        decimal i = A + B + C + D + E + F + G;
        i = A + B + C + D + E + F + G;
        txtTotalGodownCap5.Text = i.ToString();
    }
    protected void txtTotalGodownCap5_TextChanged(object sender, EventArgs e)
    {

    }

    protected void GrdPrapatraI_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "क्र.";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "धान";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "मोटा अनाज";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "कुल अनुमानित उपार्जन";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "भण्डारण हेतु कुल आवश्यक क्षमता";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC OWN";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "JVS";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Hired + Adhigrahan";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Agrementedd PVT PEG Godown";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "CWC";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Markfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Oilfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Total Godown Capacity (7 to 13)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC OWN";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Mandi CAP";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Mandi Shed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Markfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Pvt. PEG CAP";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Total CAP Capacity (14 to 18)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC OWN";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "JVS";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Hired + Adhigrahan";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Agrementedd PVT PEG Godown";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "CWC";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Markfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Oilfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Total Godown Capacity (20 to 26)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC OWN";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Mandi CAP";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Mandi Shed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Markfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Pvt. PEG CAP";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Total CAP Capacity (28 to 32)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC OWN";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "JVS";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Hired + Adhigrahan";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Agrementedd PVT PEG Godown";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "CWC";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Markfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Oilfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Total Godown Capacity (34 to 40)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC OWN";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Mandi CAP";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Mandi Shed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Markfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Pvt. PEG CAP";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Total CAP Capacity (41 to 46)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);


            GrdPrapatraI.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "क्रमांक";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "अनुमानित उपार्जन";
            HeaderCell.ColumnSpan = 4;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            GrdPrapatraI.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "GODOWN CAPACITY";
            HeaderCell.ColumnSpan = 8;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "CAP CAPACITY";
            HeaderCell.ColumnSpan = 6;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "GODOWN CAPACITY";
            HeaderCell.ColumnSpan = 8;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "CAP CAPACITY";
            HeaderCell.ColumnSpan = 6;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "GODOWN CAPACITY";
            HeaderCell.ColumnSpan = 8;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "CAP CAPACITY";
            HeaderCell.ColumnSpan = 6;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            GrdPrapatraI.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();

            HeaderCell.Text = "कवर्ड गोडाम एवं कैप की दिनांक " + "-----------------" + " की स्थिति में कुल रिक्त भण्डारण क्षमता(में.टन)";

            HeaderCell.ColumnSpan = 14;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "दिनांक " + "-----------------" + "  के बाद आज दिनांक " + "-----------------" + " तक मिली गोदाम / कैप की कुल रिक्त भण्डारण क्षमता(स्कंध के उठाव तथा नविन प्राप्त क्षमता)(में.टन)";
            HeaderCell.ColumnSpan = 14;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "आज दिनांक तक उपलब्ध कुल प्रगतिशील रिक्त क्षमता (में.टन)";
            HeaderCell.ColumnSpan = 14;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);


            GrdPrapatraI.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }
    }
}
