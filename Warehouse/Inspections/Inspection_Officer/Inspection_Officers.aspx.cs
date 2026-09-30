using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Inspection_Inspection_Officers : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString;
    string Empid, Order_no, financial_year;
    int quater, verificationtype;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //if (Session["Empid"] != null &&
                //    Session["financial_year"] != null &&
                //    Session["quater"] != null &&
                //    Session["verificationtype"] != null &&
                //    Session["Order_no"] != null)
                //{
                //string Empid = Session["Empid"].ToString();
                //string financial_year = Session["financial_year"].ToString();
                //int quater = Convert.ToInt32(Session["quater"]);
                //int verificationtype = Convert.ToInt32(Session["verificationtype"]);
                //string Order_no = Session["Order_no"].ToString();
                string Empid = "8989484189";
                string financial_year = "2025-26";
                int quater = 5;
                int verificationtype = 3;
                string Order_no = "2976";

                BindGrid(Empid, quater, verificationtype, financial_year, Order_no);
                //}
                //else
                //{
                //    Response.Redirect("Login.aspx");
                //}
            }
            catch (Exception ex)
            {
            }
        }

    }
    private void BindGrid(string empid, int quatertype, int verificationtype, string financialyear, string Orderno)
    {
        try
        {

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("SP_BranchWise_PV_Summary", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Emp_id", empid);
                cmd.Parameters.AddWithValue("@Financial_Year", financialyear);
                cmd.Parameters.AddWithValue("@Quater_Type", quatertype);
                cmd.Parameters.AddWithValue("@verification_Type", verificationtype);
                cmd.Parameters.AddWithValue("@Order_No", Orderno);
                cmd.CommandTimeout = 600;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
            }

            gvOfficeList.DataSource = dt;
            gvOfficeList.DataBind();
            Session["table"] = dt;
        }
        catch (Exception ex)
        {

        }
    }

    protected void gvOfficeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
         
            gvOfficeList.PageIndex = e.NewPageIndex;
            Empid = "8989484189";
            financial_year = "2025-26";
            quater = 5;
            verificationtype = 3;
            Order_no = "2976";
            BindGrid(Empid, quater, verificationtype, financial_year, Order_no);
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkBranch_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;

            string BranchId = lnk.CommandArgument.ToString();

            Label lblDepotName = (Label)row.FindControl("lblDepotName");
            Label lblDistrict = (Label)row.FindControl("lblDistrict");

            string DepotName = lblDepotName.Text;
            string District_Name = lblDistrict.Text;

            Session["DepotID"] = BranchId;
            Session["DepotName"] = DepotName;
            Session["District_Name"] = District_Name;

            Session["Empid"] = "8989484189";
            Session["financial_year"] = "2025-26";
            Session["quater"] = 5;
            Session["verificationtype"] = 3;
            Session["Order_no"] = "2976";



            Response.Redirect("BranchWise.aspx", false);
        }
        catch (Exception ex)
        {
            Response.Write("Error : " + ex.Message);
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = Session["table"] as DataTable;

            if (dt == null || dt.Rows.Count == 0)
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "alert",
                    "alert('No records found to export.');",
                    true
                );
                return;
            }

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader(
                "Content-Disposition",
                "attachment;filename=Inspection_Officers_" +
                DateTime.Now.ToString("ddMMyyyy_HHmm") + ".xls"
            );

            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                gvOfficeList.AllowPaging = false;
                gvOfficeList.DataBind();
                gvOfficeList.RenderControl(hw);

                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception)
        {
            ClientScript.RegisterStartupScript(
                this.GetType(),
                "alert",
                "alert('Excel export failed.');",
                true
            );
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }

}