using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public partial class WarehouseLevel_Rent_Bill_SteelSilo_Print_MPSCSC_Vacant_Capacity_Godown_Storage_Charges_Bill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["BN"].ToString()))
            {
                fillgrid();
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
                                Invoice_No.Text = ds.Tables[0].Rows[0]["Invoice_No"].ToString() + "/ " + ds.Tables[0].Rows[0]["CreatedDate"].ToString();
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
                            gvIStorageCharge.FooterRow.Cells[8].Text = "Total";
                            gvIStorageCharge.FooterRow.Cells[9].Text = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Total_Charges")).ToString();

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
    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}