using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;


public partial class Inspection_BranchWise : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                if (Session["DepotID"] != null)
                {
                    string depotID = Session["DepotID"].ToString();
                    string empid = Session["Empid"].ToString();
                    string financialYear = Session["financial_year"].ToString();
                    int quater = Convert.ToInt32(Session["quater"]);
                    int verificationtype = Convert.ToInt32(Session["verificationtype"]);
                    string orderNo = Session["Order_no"].ToString();
                    string DistName = Session["District_Name"].ToString();
                    string BranchName = Session["DepotName"].ToString();

                    lblDistrict.InnerText = "District : " + DistName;
                    lblBranch.InnerText = "Branch : " + BranchName;

                    gvReport.DataSource = GetReportData(depotID, empid, financialYear, quater, verificationtype, orderNo);
                    gvReport.DataBind();
                }
                else
                {
                    Response.Redirect("~Inspection_Officers.aspx");
                }




            }

            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Something Went Wrong . Please try again.');", true);

            }
        }

    }
    private DataTable GetReportData(string BranchId, string empid, string financialYear, int quater,
        int verificationtype, string orderNo)
    {
        try
        {
            DataTable dt = new DataTable();
            //string mobile = Request.QueryString["Mobile"].ToString();

            using (SqlConnection conn = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("SP_GodownWise_Pv_Summary", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("@BranchID", BranchId);
                cmd.Parameters.AddWithValue("@Emp_Id", empid);
                cmd.Parameters.AddWithValue("@Financial_Year", financialYear);
                cmd.Parameters.AddWithValue("@Quater_Type", quater);
                cmd.Parameters.AddWithValue("@verification_Type", verificationtype);
                cmd.Parameters.AddWithValue("@Order_No", orderNo);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
            }

            //Session["table"] = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                hfHasData.Value = "1";
            }
            else
            {
                hfHasData.Value = "0";
            }
            return dt;
        }
        catch (Exception) { return null; }
    }

    protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Remarks")
            {
                string[] args = e.CommandArgument.ToString().Split('|');

                string godownId = args[0];
                string godownName = args[1];

                //lblGodownName.Text = godownName;
                //lblRemarks.Text = GetGodownRemarks(godownId);
                //// lblRemarks.Text = GetGodownRemarks("2319005049");

                //ScriptManager.RegisterStartupScript(
                //    this,
                //    this.GetType(),
                //    "Pop",
                //    "$('#remarksModal').modal('show');",
                //    true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Unable To Open Popup . Please try again.');", true);
        }
    }
    protected void lnkGodown_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;

            string Godownid = lnk.CommandArgument.ToString();

            Label lblGodownName = (Label)row.FindControl("lblGodownName");
            Label lblgodownsubmitdate = (Label)row.FindControl("lbGodownSubmitDate");

            string GodownName = lblGodownName.Text;
            string GodownSubmitdate = lblgodownsubmitdate.Text;

            Session["Godownid"] = Godownid;
            Session["GodownName"] = GodownName;
            Session["GodownSubmitdate"] = GodownSubmitdate;
            //Session["District_Name"] = District_Name;

            //Session["Empid"] = "9098903755";
            //Session["financial_year"] = "2025-26";
            //Session["quater"] = 5;
            //Session["verificationtype"] = 3;
            //Session["Order_no"] = "3597";

            Response.Redirect("GodownWise.aspx", false);
        }
        catch (Exception ex)
        {
            Response.Write("Error : " + ex.Message);
        }
    }
    //private string GetGodownRemarks(string godownId)
    //{
    //    string remarks = "No remarks available.";
    //    try
    //    {
    //        string mobile = "";

    //        if (Session["Mobile"] != null)
    //        {
    //            mobile = Session["Mobile"].ToString();
    //        }
    //        else
    //        {
    //            throw new Exception("Mobile session expired");
    //        }

    //        using (SqlConnection con = new SqlConnection(conStr))
    //        {
    //            using (SqlCommand cmd = new SqlCommand("usp_Get_Stack_And_Godown_Remark", con))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                cmd.Parameters.AddWithValue("@GodownId", godownId);
    //                cmd.Parameters.AddWithValue("@Stack_ID", "0");
    //                cmd.Parameters.AddWithValue("@Emp_Id", mobile);

    //                con.Open();

    //                using (SqlDataReader dr = cmd.ExecuteReader())
    //                {
    //                    if (dr.Read())
    //                    {
    //                        if (dr["Godown_Remark"] != DBNull.Value)
    //                        {
    //                            remarks = dr["Godown_Remark"].ToString();
    //                        }
    //                    }
    //                }
    //            }
    //        }

    //        return remarks;
    //    }
    //    catch (Exception ex)
    //    {
    //        ClientScript.RegisterStartupScript(this.GetType(), "alert",
    //            "alert('Unable To Open Popup . Please try again.');", true);
    //        return remarks;
    //    }
    //}

}