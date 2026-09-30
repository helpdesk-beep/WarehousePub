using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public partial class WarehouseLevel_Rent_Bill_SteelSilo_Print_Variable_Procurment_Godown_Rent_Bill : System.Web.UI.Page
{
    private string qry;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["BN"].ToString()))
            {
                fillgrid();
                DSCSign();
            }
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetActual_Steel_Silo_BillData]", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BillNumber", Base64Decode(Request.QueryString["BN"].ToString()));
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Invoice_No.Text = ds.Tables[0].Rows[0]["Invoice_No"].ToString();
                                DepotName.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                                Godown_Name.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                                District_Name.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                                State.Text = ds.Tables[0].Rows[0]["State"].ToString();
                                GSTNo.Text = ds.Tables[0].Rows[0]["GSTNo"].ToString();
                                PanNo.Text = ds.Tables[0].Rows[0]["PanNo"].ToString();
                                Commodity.Text = ds.Tables[0].Rows[0]["Commodity"].ToString();
                                Period.Text = ds.Tables[0].Rows[0]["Period"].ToString();
                                Commodity_Rate.Text = ds.Tables[0].Rows[0]["Commodity_Rate"].ToString();
                                Days.Text = ds.Tables[0].Rows[0]["Days"].ToString();
                                pGodown.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                                lblBranch.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                                lbldist.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                                lblOffceAddress.Text = ds.Tables[0].Rows[0]["RegOffice"].ToString();
                                lblCorpOfficeAddress.Text = ds.Tables[0].Rows[0]["CorpOffice"].ToString();
                                lblCINNO.Text = ds.Tables[0].Rows[0]["CIN_No"].ToString();
                                lblBillNo.Text = ds.Tables[0].Rows[0]["Bill_Number"].ToString();
                            }

                            gvIStorageCharge.DataSource = ds.Tables[1];
                            gvIStorageCharge.DataBind();
                            gvIStorageCharge.FooterRow.Style.Add("text-align", "right");
                            gvIStorageCharge.FooterRow.Cells[6].Text = "Total";
                            gvIStorageCharge.FooterRow.Cells[7].Text = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Total_Charges")).ToString();

                        }
                        else
                        {
                            gvIStorageCharge.DataSource = null;
                            gvIStorageCharge.DataBind();
                        }
                    }
                }
            }
        }
    }
    public void DSCSign()
    {
        qry = "select DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_RPO_SteelSilo where Ref_Bill_No ='" + Base64Decode(Request.QueryString["BN"].ToString()) + "' and DSC_User_Type_RAM='R'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Image2.Visible = true;
            lblBSerialNo.Text = "DSC Serial No : " + dt.Rows[0]["DSC_Serial_No_RAM"].ToString();
            lblBIP.Text = "Client IP : " + dt.Rows[0]["Client_Ip_RAM"].ToString();
            lblBHolderName.Text = "DSC Holder Name : " + dt.Rows[0]["DSC_Holder_Name_RAM"].ToString();
            lblBCreatedDate.Text = "DSC Sign Date : " + dt.Rows[0]["CreatedDate"].ToString();
        }
        else
        {
            Image2.Visible = false;
            lblBSerialNo.Text = "";
            lblBIP.Text = "";
            lblBHolderName.Text = "";
            lblBCreatedDate.Text = "";
        }

        qry = "select DSC_Serial_No,DSC_Holder_Name,Client_Ip,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_SteelSilo where Bill_Number ='" + Base64Decode(Request.QueryString["BN"].ToString()) + "' and DSC_User_Type='B'";
        SqlCommand cmd2 = new SqlCommand(qry, con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataTable dt2 = new DataTable();
        da2.Fill(dt2);
        if (dt2.Rows.Count > 0)
        {
            Image1.Visible = true;
            lblBMMSerialNo.Text = "DSC Serial No : " + dt2.Rows[0]["DSC_Serial_No"].ToString();
            lblBMMIp.Text = "Client IP : " + dt2.Rows[0]["Client_Ip"].ToString();
            lblBMMHoldername.Text = "DSC Holder Name : " + dt2.Rows[0]["DSC_Holder_Name"].ToString();
            lblBMMCreatedDate.Text = "DSC Sign Date : " + dt2.Rows[0]["CreatedDate"].ToString();
        }
        else
        {
            Image1.Visible = false;
            lblBMMSerialNo.Text = "";
            lblBMMIp.Text = "";
            lblBMMHoldername.Text = "";
            lblBMMCreatedDate.Text = "";
        }

        qry = "select DSC_Serial_No,DSC_Holder_Name,Client_Ip,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_SteelSilo where Bill_Number ='" + Base64Decode(Request.QueryString["BN"].ToString()) + "' and DSC_User_Type='G'";
        SqlCommand cmd3 = new SqlCommand(qry, con);
        SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
        DataTable dt3 = new DataTable();
        da3.Fill(dt3);
        if (dt3.Rows.Count > 0)
        {
            Image3.Visible = true;
            lblGOMSerialNo.Text = "DSC Serial No : " + dt3.Rows[0]["DSC_Serial_No"].ToString();
            lblGOMIp.Text = "Client IP : " + dt3.Rows[0]["Client_Ip"].ToString();
            lblGOMHoldername.Text = "DSC Holder Name : " + dt3.Rows[0]["DSC_Holder_Name"].ToString();
            lblGOMCreatedDate.Text = "DSC Sign Date : " + dt3.Rows[0]["CreatedDate"].ToString();
        }
        else
        {
            Image3.Visible = false;
            lblGOMSerialNo.Text = "";
            lblGOMIp.Text = "";
            lblGOMHoldername.Text = "";
            lblGOMCreatedDate.Text = "";
        }


        qry = "select DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,Convert(varchar(10),CreatedDate,103) as CreatedDate from mpscsc.dbo.tbl_Digitally_Signed_Bill_RPO_SteelSilo_csms where Ref_Bill_No ='" + Base64Decode(Request.QueryString["BN"].ToString()) + "' and DSC_User_Type_RAM='D'";
        SqlCommand cmd4 = new SqlCommand(qry, con);
        SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
        DataTable dt4 = new DataTable();
        da4.Fill(dt4);
        if (dt4.Rows.Count > 0)
        {
            Image4.Visible = true;
            lblNanSerialNo.Text = "DSC Serial No : " + dt4.Rows[0]["DSC_Serial_No_RAM"].ToString();
            lblNanIP.Text = "Client IP : " + dt4.Rows[0]["Client_Ip_RAM"].ToString();
            lblNanHolderName.Text = "DSC Holder Name : " + dt4.Rows[0]["DSC_Holder_Name_RAM"].ToString();
            lblNanCreatedDate.Text = "DSC Sign Date : " + dt4.Rows[0]["CreatedDate"].ToString();
        }
        else
        {
            Image4.Visible = false;
            lblNanSerialNo.Text = "";
            lblNanIP.Text = "";
            lblNanHolderName.Text = "";
            lblNanCreatedDate.Text = "";
        }


    }
    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

    }
}