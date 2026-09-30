using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public partial class NCCF_Branch_NCCF_Print_Bill : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["BN"] != null && !string.IsNullOrEmpty(Request.QueryString["BN"].ToString()))
            {
                string username = "";

                if (Session["Username"] != null)
                {
                    username = Session["Username"].ToString().ToLower();
                }

                if (username.Contains("bhopal"))
                {
                    lbllogbranch.Text = HttpUtility.HtmlEncode("BHOPAL NCCF");
                }
                else if (username.Contains("indore"))
                {
                    lbllogbranch.Text = HttpUtility.HtmlEncode("INDORE NCCF");
                }
                else
                {
                    lbllogbranch.Text = HttpUtility.HtmlEncode("NCCF");
                }

                string billNo = Base64Decode(Request.QueryString["BN"].ToString());
                fillgrid(billNo);
                DSCSign(billNo);
                DSCSignR(billNo);
            }
        }
    }

    protected void fillgrid(string Bill_No)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_NCCF_Bill_Branch_For_Print", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Bill_Number", Bill_No);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                GD2.DataSource = dt;
                                GD2.DataBind();
                                GD2.FooterRow.Style.Add("text-align", "center");
                                GD2.FooterRow.Cells[9].Text = "Total";
                                GD2.FooterRow.Cells[10].Text = HttpUtility.HtmlEncode(dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Charges")).ToString());

                                // Encoded outputs to prevent Persistent XSS
                                lblRegion.Text = HttpUtility.HtmlEncode(dt.Rows[0]["Regionnm"].ToString());
                                lbldist.Text = HttpUtility.HtmlEncode(dt.Rows[0]["District_Name"].ToString());
                                lblBranch.Text = HttpUtility.HtmlEncode(dt.Rows[0]["DepotName"].ToString());
                                lblbillno_Actual.Text = HttpUtility.HtmlEncode(dt.Rows[0]["Bill_Number"].ToString());
                                lblAcGdwnName.Text = HttpUtility.HtmlEncode(dt.Rows[0]["Godown_Name"].ToString());
                                lblgodownid.Text = HttpUtility.HtmlEncode(dt.Rows[0]["Godown_Id"].ToString());
                                lbldatefromto.Text = HttpUtility.HtmlEncode(dt.Rows[0]["Bill_Month"].ToString());
                                lblbillingdate.Text = HttpUtility.HtmlEncode(dt.Rows[0]["Billing_Date"].ToString());
                                lblcmd_ac.Text = HttpUtility.HtmlEncode(dt.Rows[0]["Commodity"].ToString());
                                lblrate.Text = HttpUtility.HtmlEncode(dt.Rows[0]["Commodity_Rate"].ToString());
                                lblam.Text = HttpUtility.HtmlEncode(dt.Rows[0]["Net_Amount"].ToString());
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
            // Log exception safely
        }
    }

    public void DSCSign(string Bill_No)
    {
        try
        {
            string qry = "SELECT DSC_Serial_No, DSC_Holder_Name, Client_Ip, CONVERT(varchar(10), CreatedDate, 103) AS CreatedDate FROM tbl_Digitally_Signed_Bill_Details_NCCF WHERE Ref_Bill_No = @Bill_No AND DSC_User_Type = 'B'";
            using (SqlCommand cmd = new SqlCommand(qry, con))
            {
                cmd.Parameters.AddWithValue("@Bill_No", Bill_No);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Image1.Visible = true;
                        lblBSerialNo.Text = "DSC Serial No : " + HttpUtility.HtmlEncode(dt.Rows[0]["DSC_Serial_No"].ToString());
                        lblBIP.Text = "Client IP : " + HttpUtility.HtmlEncode(dt.Rows[0]["Client_Ip"].ToString());
                        lblBHolderName.Text = "DSC Holder Name : " + HttpUtility.HtmlEncode(dt.Rows[0]["DSC_Holder_Name"].ToString());
                        lblBCreatedDate.Text = "DSC Sign Date : " + HttpUtility.HtmlEncode(dt.Rows[0]["CreatedDate"].ToString());
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
            }
        }
        catch (Exception ex)
        {
            // Log exception safely
        }
    }

    public void DSCSignR(string Bill_No)
    {
        try
        {
            string qry = "SELECT DSC_Serial_No, DSC_Holder_Name, Client_Ip, CONVERT(varchar(10), CreatedDate, 103) AS CreatedDate FROM tbl_Digitally_Signed_Bill_Details_NCCF WHERE Ref_Bill_No = @Bill_No AND DSC_User_Type = 'R'";
            using (SqlCommand cmd1 = new SqlCommand(qry, con))
            {
                cmd1.Parameters.AddWithValue("@Bill_No", Bill_No);
                using (SqlDataAdapter da1 = new SqlDataAdapter(cmd1))
                {
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        Image2.Visible = true;
                        lblICSerialNo.Text = "DSC Serial No : " + HttpUtility.HtmlEncode(dt1.Rows[0]["DSC_Serial_No"].ToString());
                        lblICIp.Text = "Client IP : " + HttpUtility.HtmlEncode(dt1.Rows[0]["Client_Ip"].ToString());
                        lblICHoldername.Text = "DSC Holder Name : " + HttpUtility.HtmlEncode(dt1.Rows[0]["DSC_Holder_Name"].ToString());
                        lblICCreatedDate.Text = "DSC Sign Date : " + HttpUtility.HtmlEncode(dt1.Rows[0]["CreatedDate"].ToString());
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
            }
        }
        catch (Exception ex)
        {
            // Log exception safely
        }
    }

    public static string Base64Decode(string base64EncodedData)
    {
        try
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        catch
        {
            return string.Empty;
        }
    }
}