using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillData();
            fillgrid();
            //lblusername.Text = Session[""].ToString();
        }
    }
    protected void fillData()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("State_Dashboard", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            NoOfBill.InnerText = dt.Rows[0]["NoOfBill"].ToString();
                            NoOfBillAmt.InnerText = dt.Rows[0]["NoOfBillAmt"].ToString();
                            ICMSubmit.InnerText = dt.Rows[0]["ICMSubmit"].ToString();
                            ICMSubmitAmt.InnerText = dt.Rows[0]["ICMSubmitAmt"].ToString();
                            SubmittedICM.InnerText = dt.Rows[0]["SubmittedICM"].ToString();
                            SubmittedICMAmt.InnerText = dt.Rows[0]["SubmittedICMAmt"].ToString();
                            PendingBill.InnerText = dt.Rows[0]["PendingBill"].ToString();
                            PendingICMSubmit.InnerText = dt.Rows[0]["PendingICMSubmit"].ToString();
                            PendingICMSubmitAmt.InnerText = dt.Rows[0]["PendingICMSubmitAmt"].ToString();
                            PendingSubmittedICM.InnerText = dt.Rows[0]["PendingSubmittedICM"].ToString();
                            PendingSubmittedICMAmt.InnerText = dt.Rows[0]["PendingSubmittedICMAmt"].ToString();
                            DMBill.InnerText = dt.Rows[0]["DMBill"].ToString();
                            DMAmt.InnerText = dt.Rows[0]["DMAmt"].ToString();
                            RMDSCBill.InnerText = dt.Rows[0]["RMDSCBill"].ToString();
                            RMDSCAmt.InnerText = dt.Rows[0]["RMDSCAmt"].ToString();
                            NEFTBill.InnerText = dt.Rows[0]["NEFTBill"].ToString();
                            NEFTAmt.InnerText = dt.Rows[0]["NEFTAmt"].ToString();
                            PendingDMBill.InnerText = dt.Rows[0]["PendingDMBill"].ToString();
                            PendingDMAmt.InnerText = dt.Rows[0]["PendingDMAmt"].ToString();
                            PendingRMDSCBill.InnerText = dt.Rows[0]["PendingRMDSCBill"].ToString();
                            PendingRMDSCAmt.InnerText = dt.Rows[0]["PendingRMDSCAmt"].ToString();
                            PendingNEFTBill.InnerText = dt.Rows[0]["PendingNEFTBill"].ToString();
                            PendingNEFTAmt.InnerText = dt.Rows[0]["PendingNEFTAmt"].ToString();
                        }
                        else
                        {

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
            //using (SqlCommand cmd = new SqlCommand("Get_Amount_From_MPSCSC_From_Aug", con))
            using (SqlCommand cmd = new SqlCommand("Get_Peyment_details_Recived_and_Pending_From_MPSCSC_From_Aug", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            example.DataSource = dt;
                            example.DataBind();
                            example.Caption = @"<b style=""font-weight: bold;"">Region Wise " + "</br> " + "Payment Received Details From MPSCSC " + "</br> ";
                            //  grd.Columns[1].Visible = false;
                            // GridView1.columns.RemoveAt(1);

                            //example.FooterRow.Style.Add("text-align", "right");
                            //example.FooterRow.Cells[1].Text = "Total";
                            //example.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSUBBill")).ToString();
                            //example.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("SUBBillAmt")).ToString();
                            //example.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TDSDeduction")).ToString();
                            //example.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OtherDeduction")).ToString();
                            //example.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PaymentReceivedTilldate")).ToString();
                            //example.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingatMPSCSC")).ToString();

                        }

                    }
                }
            }
        }
    }

    protected void LinkButton_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../StatePages/State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_Procurement_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../StatePages/State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../StatePages/State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
}