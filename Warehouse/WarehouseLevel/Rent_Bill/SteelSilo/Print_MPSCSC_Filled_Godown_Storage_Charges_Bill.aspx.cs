using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public partial class WarehouseLevel_Rent_Bill_SteelSilo_Print_MPSCSC_Filled_Godown_Storage_Charges_Bill : System.Web.UI.Page
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
                                Invoice_No.Text = ds.Tables[0].Rows[0]["Invoice_No"].ToString()+"/ "+ ds.Tables[0].Rows[0]["CreatedDate"].ToString();
                                DepotName.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                                Godown_Name.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                                Commodity.Text = ds.Tables[0].Rows[0]["Commodity"].ToString();
                                Period.Text = ds.Tables[0].Rows[0]["Period"].ToString();
                                Commodity_Rate.Text = ds.Tables[0].Rows[0]["Commodity_Rate"].ToString();
                                Days.Text = ds.Tables[0].Rows[0]["Days"].ToString();
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

    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}