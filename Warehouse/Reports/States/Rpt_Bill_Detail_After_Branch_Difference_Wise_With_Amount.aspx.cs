using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
public partial class Reports_States_Rpt_Bill_Detail_After_Branch_Difference_Wise_With_Amount : System.Web.UI.Page
{
    int qtyTotal = 0;
    int grQtyTotal = 0;
    decimal qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal qtyTotal3 = 0;
    decimal qtyTotal4 = 0;
    decimal qtyTotal5 = 0;
    decimal qtyTotal6 = 0;
    decimal qtyTotal7 = 0;
    decimal qtyTotal8 = 0;
    decimal qtyTotal9 = 0;
    decimal qtyTotal10 = 0;
    decimal qtyTotal11 = 0;
    decimal qtyTotal12 = 0;
    decimal qtyTotal13 = 0;
    decimal qtyTotal14 = 0;
    decimal qtyTotal15 = 0;
    decimal qtyTotal16 = 0;
    decimal qtyTotal17 = 0;
    decimal qtyTotal18 = 0;
    decimal qtyTotal19 = 0;
    decimal qtyTotal20 = 0;
    decimal qtyTotal21 = 0;
    decimal qtyTotal22 = 0;
    decimal qtyTotal23 = 0;
    decimal qtyTotal24 = 0;

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;
    decimal grQtyTotal4 = 0;
    decimal grQtyTotal5 = 0;
    decimal grQtyTotal6 = 0;
    decimal grQtyTotal7 = 0;
    decimal grQtyTotal8 = 0;
    decimal grQtyTotal9 = 0;
    decimal grQtyTotal10 = 0;
    decimal grQtyTotal11 = 0;
    decimal grQtyTotal12 = 0;
    decimal grQtyTotal13 = 0;
    decimal grQtyTotal14 = 0;
    decimal grQtyTotal15 = 0;
    decimal grQtyTotal16 = 0;
    decimal grQtyTotal17 = 0;
    decimal grQtyTotal18 = 0;
    decimal grQtyTotal19 = 0;
    decimal grQtyTotal20 = 0;
    decimal grQtyTotal21 = 0;
    decimal grQtyTotal22 = 0;
    decimal grQtyTotal23 = 0;
    decimal grQtyTotal24 = 0;

    int storid = 0;
    int rowIndex = 1;


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
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                fillgrid(Request.QueryString["ID"].ToString());
            }
            else
            {
                fillgrid("0");
            }
        }
    }
    protected void fillgrid(string RID)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[Get_Bill_Detail_After_Branch_Difference_Wise_With_Amount]", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DistrictID", RID);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.Caption = @"<b style=""font-weight: bold;"">Branch Wise " + "</br> " + "M.P. Warehousing & Logistics Corporarion " + "</br> " + "Godown Storage Charges Bill(From August)" + "</b> ";
                            GridView1.Columns[1].Visible = false;
                            // GridView1.columns.RemoveAt(1);

                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[0].Text = "Total";
                            //GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<int>("noofgdwn")).ToString();
                            //GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfGenerateBill")).ToString();
                            //GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoofBillGenerateAmt")).ToString();
                            //GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfDSCSingBill")).ToString();
                            //GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoofBillAmtDSC")).ToString();
                            //GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("BilldiffDSC")).ToString();
                            //GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BilldiffAmt")).ToString();
                            //GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSUBBill")).ToString();
                            //GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("SUBBillAmt")).ToString();
                            //GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<int>("DSCsignedbyBM")).ToString();
                            //GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("DSCsignedbyBMAmt")).ToString();
                            //GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSignBill_CSMS")).ToString();
                            //GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoofCSMS_BillAmt")).ToString();
                            //GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<int>("BMsubmited")).ToString();
                            //GridView1.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BMAmtsubmited")).ToString();
                            //GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<int>("DMBill")).ToString();
                            //GridView1.FooterRow.Cells[18].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("DMAmt")).ToString();
                            //GridView1.FooterRow.Cells[19].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingDMBill")).ToString();
                            //GridView1.FooterRow.Cells[20].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingDMAmt")).ToString();
                            //GridView1.FooterRow.Cells[21].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfGdwnRM")).ToString();
                            //GridView1.FooterRow.Cells[22].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Approved_By_AGM_Acc_of_RO_Leval_With_Amount")).ToString();
                            //GridView1.FooterRow.Cells[23].Text = dt.AsEnumerable().Sum(row => row.Field<int>("SignedbyICM")).ToString();
                            //GridView1.FooterRow.Cells[24].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AmtSignedbyICM")).ToString();
                            //GridView1.FooterRow.Cells[25].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfRMDSC")).ToString();
                            //GridView1.FooterRow.Cells[26].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoOfRMDSCAmt")).ToString();
                            //GridView1.FooterRow.Cells[27].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ApprovedbyAGM")).ToString();
                            //GridView1.FooterRow.Cells[28].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ApprovedAmtbyAGM")).ToString();
                            //GridView1.FooterRow.Cells[29].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_File_Generated")).ToString();
                            //GridView1.FooterRow.Cells[30].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_File_Amount_Generated")).ToString();
                            //GridView1.FooterRow.Cells[31].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_File_Uploaded")).ToString();
                            //GridView1.FooterRow.Cells[32].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_File_Amount_Uploaded")).ToString();
                            //GridView1.FooterRow.Cells[33].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoofBillNEFT")).ToString();
                            //GridView1.FooterRow.Cells[34].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NEFT_Amount_By_Bank")).ToString();
                            GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<int>("noofgdwn")).ToString();
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfGenerateBill")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoofBillGenerateAmt")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfDSCSingBill")).ToString();
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoofBillAmtDSC")).ToString();
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("BilldiffDSC")).ToString();
                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BilldiffAmt")).ToString();
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSUBBill")).ToString();
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("SUBBillAmt")).ToString();
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<int>("DSCsignedbyBM")).ToString();
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("DSCsignedbyBMAmt")).ToString();
                            //GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSignBill_CSMS")).ToString();
                            //GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoofCSMS_BillAmt")).ToString();
                            //GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<int>("BMsubmited")).ToString();
                            //GridView1.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BMAmtsubmited")).ToString();
                            GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("DMBill")).ToString();
                            GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("DMAmt")).ToString();
                            GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingDMBill")).ToString();
                            GridView1.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingDMAmt")).ToString();
                            GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfGdwnRM")).ToString();
                            GridView1.FooterRow.Cells[18].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Approved_By_AGM_Acc_of_RO_Leval_With_Amount")).ToString();
                            GridView1.FooterRow.Cells[19].Text = dt.AsEnumerable().Sum(row => row.Field<int>("SignedbyICM")).ToString();
                            GridView1.FooterRow.Cells[20].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AmtSignedbyICM")).ToString();
                            GridView1.FooterRow.Cells[21].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfRMDSC")).ToString();
                            GridView1.FooterRow.Cells[22].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoOfRMDSCAmt")).ToString();
                            GridView1.FooterRow.Cells[23].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ApprovedbyAGM")).ToString();
                            GridView1.FooterRow.Cells[24].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ApprovedAmtbyAGM")).ToString();
                            GridView1.FooterRow.Cells[25].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_File_Generated")).ToString();
                            GridView1.FooterRow.Cells[26].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_File_Amount_Generated")).ToString();
                            GridView1.FooterRow.Cells[27].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_File_Uploaded")).ToString();
                            GridView1.FooterRow.Cells[28].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_File_Amount_Uploaded")).ToString();
                            GridView1.FooterRow.Cells[29].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoofBillNEFT")).ToString();
                            GridView1.FooterRow.Cells[30].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NEFT_Amount_By_Bank")).ToString();
                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //storid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "District_Id").ToString());
            //int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "noofgdwn").ToString());
            //decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfGenerateBill").ToString());
            //decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofBillGenerateAmt").ToString());
            //decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfDSCSingBill").ToString());
            //decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofBillAmtDSC").ToString());
            //decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BilldiffDSC").ToString());
            //decimal tmpTotal6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BilldiffAmt").ToString());

            //decimal tmpTotal7 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfSUBBill").ToString());
            //decimal tmpTotal8 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SUBBillAmt").ToString());
            //decimal tmpTotal9 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DSCsignedbyBM").ToString());
            //decimal tmpTotal10 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DSCsignedbyBMAmt").ToString());
            //decimal tmpTotal11 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfSignBill_CSMS").ToString());
            //decimal tmpTotal12 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofCSMS_BillAmt").ToString());
            //decimal tmpTotal13 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BMsubmited").ToString());
            //decimal tmpTotal14 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BMAmtsubmited").ToString());
            //decimal tmpTotal15 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfGdwnRM").ToString());
            //decimal tmpTotal16 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Approved_By_AGM_Acc_of_RO_Leval_With_Amount").ToString());
            //decimal tmpTotal17 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SignedbyICM").ToString());
            //decimal tmpTotal18 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AmtSignedbyICM").ToString());
            //decimal tmpTotal19 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfRMDSC").ToString());
            //decimal tmpTotal20 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfRMDSCAmt").ToString());
            //decimal tmpTotal21 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ApprovedbyAGM").ToString());
            //decimal tmpTotal22 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ApprovedAmtbyAGM").ToString());
            //decimal tmpTotal23 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofBillNEFT").ToString());
            //decimal tmpTotal24 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NEFT_Amount_By_Bank").ToString());

            int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "noofgdwn").ToString());
            decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfGenerateBill").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofBillGenerateAmt").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfDSCSingBill").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofBillAmtDSC").ToString());
            decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BilldiffDSC").ToString());
            decimal tmpTotal6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BilldiffAmt").ToString());

            decimal tmpTotal7 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfSUBBill").ToString());
            decimal tmpTotal8 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SUBBillAmt").ToString());
            decimal tmpTotal9 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DSCsignedbyBM").ToString());
            decimal tmpTotal10 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DSCsignedbyBMAmt").ToString());
            //decimal tmpTotal11 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfSignBill_CSMS").ToString());
            //decimal tmpTotal12 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofCSMS_BillAmt").ToString());
            //decimal tmpTotal13 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BMsubmited").ToString());
            //decimal tmpTotal14 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BMAmtsubmited").ToString());
            decimal tmpTotal15 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfGdwnRM").ToString());
            decimal tmpTotal16 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Approved_By_AGM_Acc_of_RO_Leval_With_Amount").ToString());
            decimal tmpTotal17 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SignedbyICM").ToString());
            decimal tmpTotal18 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AmtSignedbyICM").ToString());
            decimal tmpTotal19 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfRMDSC").ToString());
            decimal tmpTotal20 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfRMDSCAmt").ToString());
            decimal tmpTotal21 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ApprovedbyAGM").ToString());
            decimal tmpTotal22 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ApprovedAmtbyAGM").ToString());
            decimal tmpTotal23 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofBillNEFT").ToString());
            decimal tmpTotal24 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NEFT_Amount_By_Bank").ToString());

            Label lblBilldiffDSC = (Label)e.Row.FindControl("lblBilldiffDSC");
            Label lblDSCsignedbyBM = (Label)e.Row.FindControl("lblDSCsignedbyBM");
            Label lblBMsubmited = (Label)e.Row.FindControl("lblBMsubmited");
            Label lblSignedbyICM = (Label)e.Row.FindControl("lblSignedbyICM");
            Label lblApprovedbyAGM = (Label)e.Row.FindControl("lblApprovedbyAGM");
            Label lblPendingDMBill = (Label)e.Row.FindControl("lblPendingDMBill");
            String BranchID = DataBinder.Eval(e.Row.DataItem, "Branch_Id").ToString();

            if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BilldiffDSC")) > 0)
            {
                lblBilldiffDSC.Text = "<a  href='Rpt_Pending_for_Digital_Signing.aspx?ID=" + (BranchID) + "' target='_blank' style='color: Blue'>" + DataBinder.Eval(e.Row.DataItem, "BilldiffDSC") + "</a>";
            }
            if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DSCsignedbyBM")) > 0)
            {
                lblDSCsignedbyBM.Text = "<a  href='Rpt_Pending_Bill_for_Submission_to_Centre_Incharge.aspx?ID=" + (BranchID) + "' target='_blank' style='color: Blue'>" + DataBinder.Eval(e.Row.DataItem, "DSCsignedbyBM") + "</a>";
            }
            //if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BMsubmited")) > 0)
            //{
            //    lblBMsubmited.Text = "<a  href='Rpt_Pending_Bill_for_Digital_Signing_to_Centre_Incharge.aspx?ID=" + (BranchID) + "' target='_blank' style='color: Blue'>" + DataBinder.Eval(e.Row.DataItem, "BMsubmited") + "</a>";
            //}
            //if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SignedbyICM")) > 0)
            //{
            //    lblSignedbyICM.Text = "<a  href='Rpt_Pending_Bill_for_Passing_at_Account_Manager.aspx?ID=" + (BranchID) + "' target='_blank' style='color: Blue'>" + DataBinder.Eval(e.Row.DataItem, "SignedbyICM") + "</a>";
            //}
            if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ApprovedbyAGM")) > 0)
            {
                lblApprovedbyAGM.Text = "<a  href='Rpt_Pending_Bill_for_Digital_Signing_by_RM.aspx?ID=" + (BranchID) + "' target='_blank' style='color: Blue'>" + DataBinder.Eval(e.Row.DataItem, "ApprovedbyAGM") + "</a>";
            }
            if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingDMBill")) > 0)
            {
                lblApprovedbyAGM.Text = "<a  href='Rpt_Get_Pending_Bill_for_DM.aspx?ID=" + (BranchID) + "' target='_blank' style='color: Blue'>" + DataBinder.Eval(e.Row.DataItem, "PendingDMBill") + "</a>";
            }


            qtyTotal += tmpTotal;
            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
            qtyTotal5 += tmpTotal5;
            qtyTotal6 += tmpTotal6;
            qtyTotal7 += tmpTotal7;
            qtyTotal8 += tmpTotal8;
            qtyTotal9 += tmpTotal9;
            qtyTotal10 += tmpTotal10;
            //qtyTotal11 += tmpTotal11;
            //qtyTotal12 += tmpTotal12;
            //qtyTotal13 += tmpTotal13;
            //qtyTotal14 += tmpTotal14;
            qtyTotal15 += tmpTotal15;
            qtyTotal16 += tmpTotal16;
            qtyTotal17 += tmpTotal17;
            qtyTotal18 += tmpTotal18;
            qtyTotal19 += tmpTotal19;
            qtyTotal20 += tmpTotal20;
            qtyTotal21 += tmpTotal21;
            qtyTotal22 += tmpTotal22;
            qtyTotal23 += tmpTotal23;
            qtyTotal24 += tmpTotal24;


            grQtyTotal += tmpTotal;
            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
            grQtyTotal5 += tmpTotal5;
            grQtyTotal6 += tmpTotal6;
            grQtyTotal7 += tmpTotal7;
            grQtyTotal8 += tmpTotal8;
            grQtyTotal9 += tmpTotal9;
            grQtyTotal10 += tmpTotal10;
            //grQtyTotal11 += tmpTotal11;
            //grQtyTotal12 += tmpTotal12;
            //grQtyTotal13 += tmpTotal13;
            //grQtyTotal14 += tmpTotal14;
            grQtyTotal15 += tmpTotal15;
            grQtyTotal16 += tmpTotal16;
            grQtyTotal17 += tmpTotal17;
            grQtyTotal18 += tmpTotal18;
            grQtyTotal19 += tmpTotal19;
            grQtyTotal20 += tmpTotal20;
            grQtyTotal21 += tmpTotal21;
            grQtyTotal22 += tmpTotal22;
            grQtyTotal23 += tmpTotal23;
            grQtyTotal24 += tmpTotal24;

        }

    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell1 = new TableHeaderCell();

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 10;
        cell1.Text = "Branch Manager MPWLC";
        row1.Controls.Add(cell1);

        //cell1 = new TableHeaderCell();
        //cell1.ColumnSpan = 4;
        //cell1.Text = "Centre Incharge MPSCSC";
        //row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 4;
        cell1.Text = "District Manager MPSCSC";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 8;
        cell1.Text = "Regional Manager MPSCSC";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 6;
        cell1.Text = "Payment Details";
        row1.Controls.Add(cell1);

        row1.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row1);

        GridViewRow row = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Region Name";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "No. of Godown";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Bill Generated";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Generated Bill Amount";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Digitally Signed Bill";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Digitally Signed Bill Amount";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bill for Digital Signing";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bill Amount for Digital Signing";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        // cell.Text = "Bills Submitted to Centre Incharge";
        cell.Text = "Bills Submitted to DM MPSCSC";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        //cell.Text = "Bills Amount Submitted to Centre Incharge";
        cell.Text = "Bills Amount Submitted to DM MPSCSC";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        //cell.Text = "Pending Bill for Submission to Centre Incharge (MPSCSC)";
        cell.Text = "Pending Bill for Submission to DM MPSCSC";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        //cell.Text = "Pending Bill Amount for Submission to Centre Incharge (MPSCSC)";
        cell.Text = "Pending Bill Amount for Submission to DM MPSCSC";
        row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Digitally Signed Bills";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Digitally Signed Bills Amount";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Pending Bills for Digital Signing";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Pending Bills Amount for Digital Signing";
        //row.Controls.Add(cell);


        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Total Bill";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Recieved Amount";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bill";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Amount";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Total Bill Passed by Account Manager";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Total Bill Amount Passed by Account Manager";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bill for Passing at Account Manager ";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bill Amount for Passing at Account Manager";
        row.Controls.Add(cell);


        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Digitally Signed Bills by RM";
        row.Controls.Add(cell);


        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Digitally Signed Bills Amount by RM";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bills for Digital Signing by RM";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bills Amount for Digital Signing by RM";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Generated Payment File (in No's.)";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Generated Payment File (Amount in Rs.)";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Uploaded Payment File (in No's.)";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Uploaded Payment File (Amount in Rs.)";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "NEFT Payment to Godown Owner (in No's.)";
        row.Controls.Add(cell);


        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "NEFT Payment to Godown Owner (Amount in Rs.)";
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(1, row);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //bool newRow = false;

        //if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "District_Id") != null))
        //{
        //    if (storid != Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "District_Id").ToString()))
        //        newRow = true;
        //}
        //if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "District_Id") == null))
        //{
        //    newRow = true;
        //    rowIndex = 0;
        //}
        //if (newRow)
        //{
        //    GridView GridView1 = (GridView)sender;
        //    GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
        //    NewTotalRow.Font.Bold = true;
        //    // NewTotalRow.BackColor = System.Drawing.Color.Gray;
        //    NewTotalRow.ForeColor = System.Drawing.Color.Black;
        //    TableCell HeaderCell = new TableCell();
        //    HeaderCell.Text = "Sub Total";
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.ColumnSpan = 2;
        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    //HeaderCell.HorizontalAlign = HorizontalAlign.Left;
        //    //HeaderCell.ColumnSpan = 3;
        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal1.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    // HeaderCell.HorizontalAlign = HorizontalAlign.Left;
        //    //HeaderCell.ColumnSpan = 4;
        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal2.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal3.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal4.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal5.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal6.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal7.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal8.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal9.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal10.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal11.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal12.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal13.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal14.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal15.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal16.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal17.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal18.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal19.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal20.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal21.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);


        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal22.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);


        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal23.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);


        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal24.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
        //    rowIndex++;
        //    qtyTotal = 0;
        //    qtyTotal1 = 0;
        //    qtyTotal2 = 0;
        //    qtyTotal3 = 0;
        //    qtyTotal4 = 0;
        //    qtyTotal5 = 0;
        //    qtyTotal6 = 0;
        //    qtyTotal7 = 0;
        //    qtyTotal8 = 0;
        //    qtyTotal9 = 0;
        //    qtyTotal10 = 0;
        //    qtyTotal11 = 0;
        //    qtyTotal12 = 0;
        //    qtyTotal13 = 0;
        //    qtyTotal14 = 0;
        //    qtyTotal15 = 0;
        //    qtyTotal16 = 0;
        //    qtyTotal17 = 0;
        //    qtyTotal18 = 0;
        //    qtyTotal19 = 0;
        //    qtyTotal20 = 0;
        //    qtyTotal21 = 0;
        //    qtyTotal22 = 0;
        //    qtyTotal23 = 0;
        //    qtyTotal24 = 0;
        // }



    }
}