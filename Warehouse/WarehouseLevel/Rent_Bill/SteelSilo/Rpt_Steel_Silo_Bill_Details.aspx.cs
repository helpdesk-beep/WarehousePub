using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WarehouseLevel_Rent_Bill_SteelSilo_Rpt_Steel_Silo_Bill_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Session["GodownID_New"].ToString()))
            {
                //fillgrid();
                FillBranchname();
            }
        }
    }
    protected void FillBranchname()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            //using (SqlCommand cmd = new SqlCommand("[dbo].[Get_Steel_Silo_Bill_Details]", con))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Get_Branch_Name_For_Silo]", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Godown_ID ", Session["GodownID_New"].ToString());
                //cmd.Parameters.AddWithValue("@TypeID", ddlBillType.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt); 
                        if (dt.Rows.Count > 0)
                        {
                            lblbranchname.Text = dt.Rows[0]["District_Name"].ToString();
                            lblbranchname.Text = dt.Rows[0]["DepotName"].ToString();
                        }
                    }
                }
            }
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            //using (SqlCommand cmd = new SqlCommand("[dbo].[Get_Steel_Silo_Bill_Details]", con))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Rpt_GetActual_Steel_Silo_BillData_Second]", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BillNumber", txtBillNu.Text);
                cmd.Parameters.AddWithValue("@GodownID ", Session["GodownID_New"].ToString());
                //cmd.Parameters.AddWithValue("@TypeID", ddlBillType.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            gvIStorageCharge.DataSource = dt;
                            gvIStorageCharge.DataBind();
                            gvIStorageCharge.Caption = @"<b style=""font-weight: bold;""> Adani Agri Logistics(MP) Ltd." + "</br> " + "Branch Name" + " - " + lblbranchname.Text.ToString() + "</br> " + "Stock Statement of Wheat for the month of" + " - " + dt.Rows[0]["BillMonth"].ToString()+" - "+ dt.Rows[0]["BillYEar"].ToString();
                            gvIStorageCharge.FooterRow.Style.Add("text-align", "center") ;
                            gvIStorageCharge.FooterRow.Cells[2].Text = "Total"; 
                            gvIStorageCharge.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Opening_Weight")).ToString();
                            gvIStorageCharge.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Receive_Weight")).ToString();
                            gvIStorageCharge.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Issue_Weight")).ToString();
                            gvIStorageCharge.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Closing_Weight")).ToString();
                            //gvIStorageCharge.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount")).ToString();

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
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    protected void ddlBillType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void gvIStorageCharge_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {
            hdnBillNumber.Value = e.CommandArgument.ToString();
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            hdnBillCategory.Value = (gvIStorageCharge.Rows[rowIndex].FindControl("hdnBillCategoryID") as HiddenField).Value;
            if (hdnBillCategory.Value.Equals("1"))
            {
                Response.Redirect("Print_Filld_Godown_Rent_Bill.aspx?BN=" + Base64Encode(hdnBillNumber.Value));
            }
            else if (hdnBillCategory.Value.Equals("2"))
            {
                Response.Redirect("Print_Vacant_Godown_Rent_Bill.aspx?BN=" + Base64Encode(hdnBillNumber.Value));
            }
            else if (hdnBillCategory.Value.Equals("3"))
            {
                Response.Redirect("Print_Service_Charges_Godown_Rent_Bill.aspx?BN=" + Base64Encode(hdnBillNumber.Value));
            }
            else if (hdnBillCategory.Value.Equals("4"))
            {
                Response.Redirect("Print_Procurment_Vacant_Godown_Rent_Bill.aspx?BN=" + Base64Encode(hdnBillNumber.Value));
            }
            else if (hdnBillCategory.Value.Equals("5"))
            {
                Response.Redirect("Print_Variable_Procurment_Godown_Rent_Bill.aspx?BN=" + Base64Encode(hdnBillNumber.Value));
            }
            else if (hdnBillCategory.Value.Equals("6"))
            {
                Response.Redirect("Print_MPSCSC_Filled_Godown_Storage_Charges_Bill.aspx?BN=" + Base64Encode(hdnBillNumber.Value));
            }
            else if (hdnBillCategory.Value.Equals("7"))
            {
                Response.Redirect("Print_MPSCSC_Vacant_Capacity_Godown_Storage_Charges_Bill.aspx?BN=" + Base64Encode(hdnBillNumber.Value));
            }

        }
    }

    protected void btnSerch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
}