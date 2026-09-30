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

public partial class JointVentureScheme_AgreementLicenseUpdate_Karif_2022_23 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltran;
    SqlCommand cmd;
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        string SessRegion = Session["UserName"].ToString();
        string SessRegionid = Session["UserId"].ToString();
        if (SessRegion != "" && SessRegionid != "")
        {
            if (!IsPostBack)
            {
                lbluser.Text = SessRegion;
            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtInsID.Text != "")
        {

            Search();
        }

        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Inspection ID .....')", true);

        }
    }
    public void Search()
    {

        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_License_Status_Kharif_2022_23", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Inspection_Id", txtInsID.Text);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            gvGodown.DataSource = dt;
                            gvGodown.DataBind();


                        }
                        else
                        {
                            gvGodown.DataSource = null;
                            gvGodown.DataBind();


                        }
                    }
                }
            }
        }

    }
    protected void gvGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        TRHide.Visible = true;
        GridViewRow gvr = gvGodown.SelectedRow;
        // txtVacantCptUpdate.Text = gvr.Cells[7].Text;

        if (gvr.Cells[1].Text != "")
        {
            hdnTid.Value = gvr.Cells[1].Text;
        }
        else
        {
            hdnTid.Value = "0";
        }

        //if (gvr.Cells[9].Text != "")
        //{
        //    txtwdra.Text = gvr.Cells[9].Text;
        //}
        //else
        //{
        //    txtwdra.Text = "";
        //}
        if (gvr.Cells[10].Text != "")
        {
            txtLiNo.Text = gvr.Cells[10].Text;
        }
        else
        {
            txtLiNo.Text = "";
        }
        if (gvr.Cells[11].Text != "")
        {
            txtisd.Text = gvr.Cells[11].Text;
        }
        else
        {
            txtisd.Text = "";
        }
        if (gvr.Cells[12].Text != "")
        {
            txtexd.Text = gvr.Cells[12].Text;
        }
        else
        {
            txtexd.Text = "";
        }

        //if (gvr.Cells[13].Text != "")
        //{
        //    txtNwdra.Text = gvr.Cells[13].Text;
        //}
        //else
        //{
        //    txtNwdra.Text = "";
        //}
        if (gvr.Cells[14].Text != "")
        {
            txtNLiNo.Text = gvr.Cells[14].Text;
        }
        else
        {
            txtNLiNo.Text = "";
        }
        if (gvr.Cells[15].Text != "")
        {
            txtNisd.Text = gvr.Cells[15].Text;
        }
        else
        {
            txtNisd.Text = "";
        }
        if (gvr.Cells[16].Text != "")
        {
            txtNexd.Text = gvr.Cells[16].Text;
        }
        else
        {
            txtNexd.Text = "";
        }
        //ddlFitUnfit.SelectedItem.Text = gvr.Cells[8].Text;
        if (gvGodown.SelectedRow != null)
        {
            gv.Visible = true;
            btnUpdateCpt.Visible = true;
        }
        else
        {
            gv.Visible = false;
            btnUpdateCpt.Visible = false;
        }
    }
    protected void btnUpdateCpt_Click(object sender, EventArgs e)
    {

        string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        using (SqlConnection constr = new SqlConnection(CS))
        {
            SqlCommand cmd = new SqlCommand("Update_License_Status_Kharif_2022_23", constr);
            cmd.CommandType = CommandType.StoredProcedure;
            constr.Open();
            cmd.Parameters.AddWithValue("@TId", hdnTid.Value);
            if (ddlGodownType.SelectedItem.ToString() == "WDRA")
            {
                cmd.Parameters.AddWithValue("@Present_Validity_WDRA", "1");
                cmd.Parameters.AddWithValue("@WDRAL_Present_Validity", "".Trim());
                cmd.Parameters.AddWithValue("@WDRA_LicenseNo", txtLiNo.Text.Trim());
                cmd.Parameters.AddWithValue("@WDRA_LIssueDate", getDate_MDY(txtisd.Text.Trim()));
                cmd.Parameters.AddWithValue("@WDRA_LicenseDate", getDate_MDY(txtexd.Text.Trim()));

                cmd.Parameters.AddWithValue("@Warehouse_LicenseNo", "".Trim());
                cmd.Parameters.AddWithValue("@Warehouse_LIssueDate", "".Trim());
                cmd.Parameters.AddWithValue("@Warehouse_LicenseDate", "".Trim());
            }
            else if (ddlGodownType.SelectedItem.ToString() == "NON WDRA")
            {
                cmd.Parameters.AddWithValue("@WDRA_LicenseNo", "".Trim());
                cmd.Parameters.AddWithValue("@WDRA_LIssueDate", "".Trim());
                cmd.Parameters.AddWithValue("@WDRA_LicenseDate", "".Trim());
                cmd.Parameters.AddWithValue("@WDRAL_Present_Validity", "1");
                cmd.Parameters.AddWithValue("@Present_Validity_WDRA", "".Trim());
                cmd.Parameters.AddWithValue("@Warehouse_LicenseNo", txtNLiNo.Text.Trim());
                cmd.Parameters.AddWithValue("@Warehouse_LIssueDate", getDate_MDY(txtNisd.Text.Trim()));
                cmd.Parameters.AddWithValue("@Warehouse_LicenseDate", getDate_MDY(txtNexd.Text.Trim()));
            }
            cmd.Parameters.AddWithValue("@UpdateBy", IPAddress);

            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "License Status Updated Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                //Clear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
            }
            Search();
        }
    }
    //catch (Exception ex)
    //    {
    //        string strMsg2 = ex.Message.ToString();
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
    //    }
    //}
    //public int checkAgreement()
    //{

    //}
    public void Clear()
    {
        ddlGodownType.SelectedValue = "0";
        txtLiNo.Text = "";
        txtisd.Text = "";
        txtexd.Text = "";
        //txtNwdra.Text = "";
        txtNLiNo.Text = "";
        txtNisd.Text = "";
        txtNexd.Text = "";
        TRHide.Visible = false;
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgreementLicenseUpdate.aspx");
    }

    protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGodownType.SelectedValue == "1")
        {
            wdra.Visible = true;
            nonwdra.Visible = false;
            btnu.Visible = true;
        }
        else if (ddlGodownType.SelectedValue == "2")
        {
            wdra.Visible = false;
            nonwdra.Visible = true;
            btnu.Visible = true;
        }
        else if (ddlGodownType.SelectedValue == "0")
        {
            wdra.Visible = false;
            nonwdra.Visible = false;
            btnu.Visible = false;
        }
    }
}