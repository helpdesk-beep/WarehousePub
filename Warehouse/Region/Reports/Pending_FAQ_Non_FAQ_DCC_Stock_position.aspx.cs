using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

public partial class StatePages_Pending_FAQ_Non_FAQ_DCC_Stock_position : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    DataTable dt = new DataTable();
    public string LicDate = "";
    //public string WManagerId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (Session["UserName"] != null)
            {
                if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
                {
                    if (!IsPostBack)
                    {
                        fillgrid();
                        //FillDistrict();
                        //GetRegion();
                    }
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
            //fillgrid();
            //GetRegion();
        }
    }
    //private void GetRegion()
    //{
    //    string strDist = "";
    //    strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Regionnm";
    //    SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlregion.DataSource = ds.Tables[0];
    //        ddlregion.DataTextField = "Regionnm";
    //        ddlregion.DataValueField = "Region_ID";
    //        ddlregion.DataBind();
    //        ddlregion.Items.Insert(0, "--Select--");
    //    }
    //    else
    //    {
    //        ddlregion.Items.Insert(0, "--Select--");
    //    }
    //}

     protected void fillgrid()
    {
        Decimal opcloavg = 0;
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Pending_WHR_Entry_By_BM_For_FAQNONFAQDCC_Stock", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Districtid", Request.QueryString["DistrictId"].ToString());
                cmd.Parameters.AddWithValue("@Commodityid", Request.QueryString["Commodity_Id"].ToString());
                
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + " - " + "Stock Position in(Qtl.)";
                            GrdOfficerPreviousInsp.DataSource = dt;
                            GrdOfficerPreviousInsp.DataBind();
                            GrdOfficerPreviousInsp.FooterRow.Style.Add("text-align", "Right");
                            GrdOfficerPreviousInsp.FooterRow.Cells[6].Text = "Total";
                            GrdOfficerPreviousInsp.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OnlineStockPosition")).ToString();
                            //GrdOfficerPreviousInsp.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("FAQStockEntryByBM")).ToString();
                            //GrdOfficerPreviousInsp.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NonFAQStockEntryByBM")).ToString();
                            //GrdOfficerPreviousInsp.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("DCCStockEntryByBM")).ToString();
                            //GrdOfficerPreviousInsp.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingEntryatBM")).ToString();
                        }
                        else
                        {

                            GrdOfficerPreviousInsp.DataSource = null;
                            GrdOfficerPreviousInsp.DataBind();
                        }
                    }
                }
            }
        }
    }
   
    protected void ddlCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}
