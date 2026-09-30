using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Resources;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;

public partial class WarehouseLevel_Nafed_Godown_Bill_Wise_Payment_Status : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["GodownID_New"] != null))
        {
            if (!IsPostBack)
            {
                fillgrid();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Nafed_Godown_Bill_Wise_Payment_Status", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Godown_ID", Session["GodownID_New"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            grpendding.DataSource = dt;
                            grpendding.DataBind();
                            //grdbill.Visible = true;
                            if (dt.Rows.Count > 0)
                            {
                                lblGodownHeader.Text = "Godown : " + dt.Rows[0]["Godown"].ToString();
                            }
                            //grdbill.Visible = true;
                            grpendding.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            grpendding.FooterRow.Cells[1].Text = "Total SC Bill Amount";
                            grpendding.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("AmountRecivedFromNAfed")).ToString();
                            grpendding.FooterRow.Cells[7].Text = "Total Rent Bill Amount";
                            grpendding.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("TotalRentBillAmount")).ToString();
                            grpendding.FooterRow.Cells[15].Text = "Total Rent Bill Amount Pay to Godown Owner";
                            grpendding.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("PaytogodownOwner")).ToString();
                        }
                        else
                        {
                            grpendding.DataSource = null;
                            grpendding.DataBind();
                            //grdbill.Visible = true;
                        }
                    }
                }
            }
        }
    }
}