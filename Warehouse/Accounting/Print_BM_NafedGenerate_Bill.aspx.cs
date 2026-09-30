using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public partial class Accounting_Print_BM_NafedGenerate_Bill : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string qry;
    decimal TT1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["BN"].ToString()))
            {
                //GetFinalBillDetails(Base64Decode(Request.QueryString["BN"].ToString()));
                fillgrid(Base64Decode(Request.QueryString["BN"].ToString()));
                DSCSign(Base64Decode(Request.QueryString["BN"].ToString()));
                DSCSignR(Base64Decode(Request.QueryString["BN"].ToString()));
                //String Bill_No = Session["Bill_Number"].ToString();
            }
        }
    }

    private void GetFinalBillDetails()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_For_Print", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Bill_Number", Session["Bill_Number"].ToString());
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
                                GD2.FooterRow.Style.Add("text-align;font-weight: bold;text-align:center;", "Center");
                                GD2.FooterRow.Cells[10].Text = "Total";
                                GD2.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Charges")).ToString();

                                lbldist.Text = dt.Rows[0]["District_Name"].ToString();
                                lblBranch.Text = dt.Rows[0]["DepotName"].ToString();
                                lblbillno_Actual.Text = dt.Rows[0]["Bill_Number"].ToString();
                                lblAcGdwnName.Text = dt.Rows[0]["Godown_Name"].ToString();
                                //lblRate.Text = ds.Tables[0].Rows[0]["Commodity_Rate"].ToString();
                                lbldatefromto.Text = dt.Rows[0]["Billing_Date"].ToString();
                                lblcmd_ac.Text = dt.Rows[0]["Commodity"].ToString();

                                //lblSPAN.Text = dt.Rows[0]["PAN"].ToString();
                                //lblSGST.Text = dt.Rows[0]["GST"].ToString();
                                ////btnDetail.Text = "WAREHOUSES DETAIL(" + ds.Tables[0].Rows[0]["TotalGodown"].ToString() + ")";
                                //Panel2.Visible = true;
                                //grdbill.Visible = true;
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
    //public void GetBillOtherDataActual(String BillNo)
    //{
    //    SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_For_Print", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@BillNumber", BillNo);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        lbldatefromto.Text = dt.Rows[0]["Month"].ToString();
    //        lbldepositor.Text = "MPSCSC";
    //        lblbillno_Actual.Text = dt.Rows[0]["Bill_Number"].ToString();
    //        lblcmd_ac.Text = dt.Rows[0]["Commodity_Name"].ToString() + " " + dt.Rows[0]["Crop_Year"].ToString();


    //        lblAcGdwnName.Text = dt.Rows[0]["Godown_Name"].ToString();
    //        lblCropyear.Text = dt.Rows[0]["Commodity_Rate"].ToString();
    //        lblBranch.Text = dt.Rows[0]["Branch"].ToString();
    //        lbldist.Text = dt.Rows[0]["District"].ToString();
    //    }
    //    else
    //    {
    //        lbldatefromto.Text = "";
    //        lbldepositor.Text = "";
    //        lblbillno_Actual.Text = "";
    //        lblcmd_ac.Text = "";
    //        lblAcGdwnName.Text = "";
    //        lblCropyear.Text = "";
    //    }
    //}

    protected void fillgrid(String Bill_No)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_For_Print", con))
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
                                GD2.FooterRow.Style.Add("text-align;font-weight: bold;text-align:center;", "Center");
                                GD2.FooterRow.Cells[11].Text = "Total";
                                GD2.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Charges")).ToString();

                                lbldist.Text = dt.Rows[0]["District_Name"].ToString();
                                lblBranch.Text = dt.Rows[0]["DepotName"].ToString();
                                lblbillno_Actual.Text = dt.Rows[0]["Bill_Number"].ToString();
                                lblAcGdwnName.Text = dt.Rows[0]["Godown_Name"].ToString();
                                //lblRate.Text = ds.Tables[0].Rows[0]["Commodity_Rate"].ToString();
                                lbldatefromto.Text = dt.Rows[0]["Billing_Date"].ToString();
                                lblcmd_ac.Text = dt.Rows[0]["Commodity"].ToString();

                                //lblSPAN.Text = dt.Rows[0]["PAN"].ToString();
                                //lblSGST.Text = dt.Rows[0]["GST"].ToString();
                                ////btnDetail.Text = "WAREHOUSES DETAIL(" + ds.Tables[0].Rows[0]["TotalGodown"].ToString() + ")";
                                //Panel2.Visible = true;
                                //grdbill.Visible = true;
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
        qry = "select DSC_Serial_No,DSC_Holder_Name,Client_Ip,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_Details_NAFED where Ref_Bill_No='" + Bill_No + "'  and DSC_User_Type='B'";
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

        qry = "select DSC_Serial_No,DSC_Holder_Name,Client_Ip,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_Details_NAFED where Ref_Bill_No='" + Bill_No + "'  and DSC_User_Type='R'";
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
}