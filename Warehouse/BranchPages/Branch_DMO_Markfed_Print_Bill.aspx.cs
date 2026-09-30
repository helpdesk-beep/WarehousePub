using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_Branch_DMO_Markfed_Print_Bill : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string qry;
    decimal TT1;
    decimal totalClosing = 0;
    decimal totalCharges = 0;
    int totalBags = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["BN"].ToString()))
            {
                fillgrid(Base64Decode(Request.QueryString["BN"].ToString()));
                DSCSign(Base64Decode(Request.QueryString["BN"].ToString()));
                DSCSignR(Base64Decode(Request.QueryString["BN"].ToString()));

            }
        }
    }
    protected void fillgrid(String Bill_No)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_DMO_Markfed_Bill_Branch_For_Print", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Bill_Number", Bill_No);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                GD2.DataSource = dt;
                                GD2.DataBind();
                                lblRegion.Text = dt.Rows[0]["Regionnm"].ToString();
                                lbldist.Text = dt.Rows[0]["District_Name"].ToString();
                                lblBranch.Text = dt.Rows[0]["DepotName"].ToString();
                                lblbillno_Actual.Text = dt.Rows[0]["Bill_Number"].ToString();
                                lblAcGdwnName.Text = dt.Rows[0]["Godown_Name"].ToString();
                                lblgodownid.Text = dt.Rows[0]["Godown_Id"].ToString();
                                lbldatefromto.Text = dt.Rows[0]["Bill_Month"].ToString();
                                lblbillingdate.Text = dt.Rows[0]["Billing_Date"].ToString();
                                lblcmd_ac.Text = dt.Rows[0]["Commodity"].ToString();
                                lblrate.Text = dt.Rows[0]["Commodity_Rate"].ToString();
                                lblam.Text = dt.Rows[0]["Net_Amount"].ToString();
                            }
                            else
                            {
                                GD2.DataSource = null;
                                GD2.DataBind();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    public void DSCSign(String Bill_No)
    {
        qry = "select DSC_Serial_No,DSC_Holder_Name,Client_Ip,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_Details_DMO_Markfed where Ref_Bill_No='" + Bill_No + "'  and DSC_User_Type='B'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Image1.Visible = true;
            lblBSerialNo.Text = "DSC Serial No : " + dt.Rows[0]["DSC_Serial_No"].ToString();
            lblBIP.Text = "Client IP : " + dt.Rows[0]["Client_Ip"].ToString();
            lblBHolderName.Text = "DSC Holder Name : " + dt.Rows[0]["DSC_Holder_Name"].ToString();
            lblBCreatedDate.Text = "DSC Sign Date : " + dt.Rows[0]["CreatedDate"].ToString();
        }
        else
        {
            Image1.Visible = false;
            lblBSerialNo.Text = "";
            lblBIP.Text = "";
            lblBHolderName.Text = "";
            lblBCreatedDate.Text = "";
        }
    }
    public void DSCSignR(String Bill_No)
    {

        qry = "select DSC_Serial_No,DSC_Holder_Name,Client_Ip,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_Details_DMO_Markfed where Ref_Bill_No='" + Bill_No + "'  and DSC_User_Type='R'";
        SqlCommand cmd1 = new SqlCommand(qry, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            Image2.Visible = true;
            lblICSerialNo.Text = "DSC Serial No : " + dt1.Rows[0]["DSC_Serial_No"].ToString();
            lblICIp.Text = "Client IP : " + dt1.Rows[0]["Client_Ip"].ToString();
            lblICHoldername.Text = "DSC Holder Name : " + dt1.Rows[0]["DSC_Holder_Name"].ToString();
            lblICCreatedDate.Text = "DSC Sign Date : " + dt1.Rows[0]["CreatedDate"].ToString();
        }
        else
        {
            Image2.Visible = false;
            lblICSerialNo.Text = "";
            lblICIp.Text = "";
            lblICHoldername.Text = "";
            lblICCreatedDate.Text = "";
        }
    }
    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
    protected void GD2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalClosing += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Closing_Balance"));
            totalCharges += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total_Charges"));
            totalBags += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Chargable_Bags"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "TOTAL";
            e.Row.Cells[0].Font.Bold = true;

            e.Row.Cells[7].Text = totalClosing.ToString("N2");
            e.Row.Cells[9].Text = totalBags.ToString();
            e.Row.Cells[10].Text = totalCharges.ToString("N2");

        }
    }
}