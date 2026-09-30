using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Reports_Branch_Rpt_District_Wise_Payment_Status_All_Godown_Type_Wise : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime now = DateTime.Now;
            string date = now.GetDateTimeFormats('d')[0];
            string time = now.GetDateTimeFormats('t')[0];
            //lbldate.Text= date+'-'+ time;
            fillComodity();
            fillgrid();
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
    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    
    private void fillComodity()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select distinct Hired_Type From tbl_MetaData_GODOWN_2018";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodownType.Items.Clear();
                ddlGodownType.DataSource = ds.Tables[0];
                ddlGodownType.DataTextField = "Hired_Type";
                ddlGodownType.DataValueField = "Hired_Type";
                ddlGodownType.DataBind();
                ddlGodownType.Items.Insert(0, "--Select--");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("GodownTypeWisePAymentStatus_For_Branch", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
                if (ddlGodownType.SelectedValue== "--Select--")
                {
                    cmd.Parameters.AddWithValue("@HireType", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@HireType", ddlGodownType.SelectedValue);
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.FooterRow.Style.Add("text-align", "Right");
                            GridView1.FooterRow.Cells[3].Text = "Total";
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfGenerateBill")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BillAmt")).ToString();
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSUBBill")).ToString();
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("SUBBillAmt")).ToString();

                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingBillForSubmisionatBranch")).ToString();
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingBillAmountForSubmision")).ToString();
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoofbillPaymentReceivedFromMPSCSC")).ToString();
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoofbillPaymentAmountReceivedFromMPSCSC")).ToString();
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PaymentDecuctionbyMPSCSC")).ToString();
                            GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalNoofPendingBillatMPSCSC")).ToString();
                            GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalNoofPendingBillAmountatMPSCSC")).ToString();
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "View")
        //{
        //    // Retrieve the ID from the CommandArgument
        //    string Godownid = Convert.ToString(e.CommandArgument);
        //    Session["GodownID"] = Godownid.ToString();
        //   // Response.Redirect("~/Reports/States/Rpt_Godown_Bill_Wise_Payment_Status.aspx");
        //    Response.Redirect("window.open('~/Reports/States/Rpt_Godown_Bill_Wise_Payment_Status.aspx','_blank')");
        //    // Logic to handle the view action
        //    // For example, redirect to a detail page or show a modal
        //    //lblMessage.Text = "You clicked View for Item ID";
        //}
        if (e.CommandName == "View")
        {
            GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            //Session["GodownID"] = (row.RowIndex).ToString();
            string Godownid = Convert.ToString(e.CommandArgument);
            Session["GodownID"] = Godownid.ToString();
            // Label Billnumber = (Label)row.FindControl("lblBill_Number");
            string url = "../States/Rpt_Godown_Bill_Wise_Payment_Status.aspx?BN=" + (Session["GodownID"].ToString());
            string s = "window.open('" + url + "', 'popup_window', 'width=1000,height=600,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }//http://localhost:61844/Reports/States/Rpt_Godown_Bill_Wise_Payment_Status.aspx
    protected void drpDwnCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
  
    protected void ddlComodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}