using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_Reports_Rpt_Branch_Pendency_at_RM : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Double TT1, TT2, TT3, TT4;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["DID"]))
            {
                fillgrid(Request.QueryString["DID"].ToString());
            }
        }
    }
    protected void fillgrid(string RID)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[Get_Branch_Wise_Pendancy_at_Varius_Lavel_New]", con))
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
                            GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. Warehousing & Logistics Corporarion " + "</br> " + "Branch Wise Bill Pending Status At Various Levels(From August 2020)" + "</b> ";

                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Style.Add("font-size", "X-Large");
                            GridView1.FooterRow.Cells[1].Text = "Total";
                            GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingPassingOrder")).ToString();
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingAGMDSC")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ApprovedAmtbyAGM")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingCreatedEPF")).ToString();
                            divshowdetails.Visible = true;
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                            divshowdetails.Visible = false;
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
            Label lblPendingPassingOrder = (Label)e.Row.FindControl("lblPendingPassingOrder");
            Label lblPendingAGMDSC = (Label)e.Row.FindControl("lblPendingAGMDSC");
            Label lblApprovedAmtbyAGM = (Label)e.Row.FindControl("lblApprovedAmtbyAGM");
            Label lblCreatedEPFAmount = (Label)e.Row.FindControl("lblCreatedEPFAmount");
            HiddenField hdnBranch_Id = (HiddenField)e.Row.FindControl("hdnBranch_Id");



            if (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PendingPassingOrder")) > 0)
            {
                lblPendingPassingOrder.Text = "<a  href='Rpt_Passing_Order_Pendency_at_RM.aspx?BID=" + (hdnBranch_Id.Value) + "' target='_blank' style='color: white'>" + DataBinder.Eval(e.Row.DataItem, "PendingPassingOrder") + "</a>";
            }
            if (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PendingAGMDSC")) > 0)
            {
                lblPendingAGMDSC.Text = "<a  href='Rpt_AGM_DSC_Pendency_at_RM.aspx?BID=" + (hdnBranch_Id.Value) + "' target='_blank' style='color: white'>" + DataBinder.Eval(e.Row.DataItem, "PendingAGMDSC") + "</a>";
            }
            if (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ApprovedAmtbyAGM")) > 0)
            {
                lblApprovedAmtbyAGM.Text = "<a  href='Rpt_Pendency_DSC_at_RM.aspx?BID=" + (hdnBranch_Id.Value)  + "' target='_blank' style='color: white'>" + DataBinder.Eval(e.Row.DataItem, "ApprovedAmtbyAGM") + "</a>";
            }
            if (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PendingCreatedEPF")) > 0)
            {
                lblCreatedEPFAmount.Text = "<a  href='Rpt_NEFT_File_Pendency_at_RM.aspx?BID=" + (hdnBranch_Id.Value)  + "' target='_blank' style='color: white'>" + DataBinder.Eval(e.Row.DataItem, "PendingCreatedEPF") + "</a>";
            }
           


            //TT1 += Convert.ToInt64(lblNumberofGPs.Text);
            //TT2 += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "NoofSwachhaGrahisRegistered"));
            //TT3 += Convert.ToInt64(lblNumberofVillages.Text);
            //TT4 += Convert.ToInt64(lblMappedVillages.Text);
            //TT5 += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "NotMappedVillages"));
            //TT6 += Convert.ToInt64(lblMappedSwachhaGrihi.Text);
            //TT7 += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "NotMappedSwachhaGrihi"));
            //TT8 += Convert.ToInt64(lblWorkOrderGenerated.Text);
            //TT9 += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "WorkOrderNotGenerated"));
            //TT10 += Convert.ToInt64(lblUserGenerated.Text);
            //TT11 += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "UserNotCreated"));
            //TT12 += Convert.ToInt64(lblMappedFamily.Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //e.Row.Cells[2].Text = "<div style='text-align: left'>" + "Total" + "</div>";
            //e.Row.Cells[2].BackColor = System.Drawing.Color.Silver;
            //e.Row.Cells[3].Text = "<div style='text-align: right'>" + TT1.ToString() + "</div>";
            //e.Row.Cells[3].BackColor = System.Drawing.Color.Silver;
            //e.Row.Cells[4].Text = "<div style='text-align: right'>" + TT2.ToString() + "</div>";
            //e.Row.Cells[4].BackColor = System.Drawing.Color.Silver;
            //e.Row.Cells[5].Text = "<div style='text-align: right'>" + TT3.ToString() + "</div>";
            //e.Row.Cells[5].BackColor = System.Drawing.Color.Silver;
            //e.Row.Cells[6].Text = "<div style='text-align: right'>" + TT4.ToString() + "</div>";
            //e.Row.Cells[6].BackColor = System.Drawing.Color.Silver;
            //e.Row.Cells[7].Text = "<div style='text-align: right'>" + TT5.ToString() + "</div>";
            //e.Row.Cells[7].BackColor = System.Drawing.Color.Silver;
            //e.Row.Cells[8].Text = "<div style='text-align: right'>" + TT6.ToString() + "</div>";
            //e.Row.Cells[8].BackColor = System.Drawing.Color.Silver;
            //e.Row.Cells[9].Text = "<div style='text-align: right'>" + TT7.ToString() + "</div>";
            //e.Row.Cells[9].BackColor = System.Drawing.Color.Silver;
            //e.Row.Cells[10].Text = "<div style='text-align: right'>" + TT8.ToString() + "</div>";
            //e.Row.Cells[10].BackColor = System.Drawing.Color.Silver;
            //e.Row.Cells[11].Text = "<div style='text-align: right'>" + TT9.ToString() + "</div>";
            //e.Row.Cells[11].BackColor = System.Drawing.Color.Silver;
            //e.Row.Cells[12].Text = "<div style='text-align: right'>" + TT10.ToString() + "</div>";
            //e.Row.Cells[12].BackColor = System.Drawing.Color.Silver;
            //e.Row.Cells[13].Text = "<div style='text-align: right'>" + TT11.ToString() + "</div>";
            //e.Row.Cells[13].BackColor = System.Drawing.Color.Silver;
            //e.Row.Cells[14].Text = "<div style='text-align: right'>" + TT12.ToString() + "</div>";
            //e.Row.Cells[14].BackColor = System.Drawing.Color.Silver;
        }
    }
}