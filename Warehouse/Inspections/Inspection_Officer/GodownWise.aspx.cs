using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class Inspection_GodownWise : System.Web.UI.Page
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
                    string Godownid = Session["Godownid"].ToString();
                    string GodownName = Session["GodownName"].ToString();
                    string GodownSubmitdate = Session["GodownSubmitdate"].ToString();

                    BindHeader(GodownName);
                    BindReport(depotID, Godownid, empid, financialYear, quater, verificationtype, orderNo, GodownSubmitdate);
                }
                else
                {
                    Response.Redirect("Inspection_Officers.aspx");
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "alert",
                    "alert('Something went wrong. Please try again.');",
                    true
                );

            }
        }

    }
    private void BindHeader(string Godown_Name)
    {

        lblGodown.InnerText = "Godown :- " + Godown_Name;
    }

    private void BindReport(string depotID, string godownId, string empid, string financialYear, int quater,
        int verificationtype, string orderNo, string date)
    {
        try
        {
            DateTime dts;

            if (string.IsNullOrEmpty(date))
            {
                dts = DateTime.Now;
            }
            else
            {
                dts = Convert.ToDateTime(date);
            }

            string formattedDate = dts.ToString("yyyy-MM-dd");
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("SP_StackWise_PV_Summary", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 600;
                cmd.Parameters.Add("@Godown_ID", SqlDbType.VarChar, 20)
                   .Value = godownId;
                cmd.Parameters.Add("@BranchID", SqlDbType.VarChar, 10)
                   .Value = depotID;
                cmd.Parameters.AddWithValue("@Emp_Id", empid);
                cmd.Parameters.AddWithValue("@Financial_Year", financialYear);
                cmd.Parameters.AddWithValue("@Quater_Type", quater);
                cmd.Parameters.AddWithValue("@verification_Type", verificationtype);
                cmd.Parameters.AddWithValue("@Order_No", orderNo);
                cmd.Parameters.AddWithValue("@date", formattedDate);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
            }


            gvReport.DataSource = dt;
            gvReport.DataBind();
            if (dt != null && dt.Rows.Count > 0)
            {
                hfHasData.Value = "1";
            }
            else
            {
                hfHasData.Value = "0";
            }
            // Session["table"] = dt;
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(
                this.GetType(),
               "alert",
                "alert('Something went wrong. Please try again.');",
                true
            );

        }
    }

    protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Remarks")
            {
                string[] args = e.CommandArgument.ToString().Split('|');

                string stackId = args[0];
                string stackName = args[1];

                lblStackName.Text = stackName;

                // fetch remarks + images
                LoadStackDetails(stackId);

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "Pop",
                    "$('#remarksModal').modal('show');",
                    true);
            }
        }
        catch (Exception ex)
        {

            ClientScript.RegisterStartupScript(
           this.GetType(),
          "alert",
           "alert('Unable to Open Popup. Please try again.');",
           true
       );
        }
    }



    private void LoadStackDetails(string stackId)
    {
        try
        {
            string depotID = Session["DepotID"].ToString();
            string empid = Session["Empid"].ToString();
            string financialYear = Session["financial_year"].ToString();
            int quater = Convert.ToInt32(Session["quater"]);
            int verificationtype = Convert.ToInt32(Session["verificationtype"]);
            string orderNo = Session["Order_no"].ToString();
            string DistName = Session["District_Name"].ToString();
            string BranchName = Session["DepotName"].ToString();
            string Godownid = Session["Godownid"].ToString();
            string GodownName = Session["GodownName"].ToString();
            string GodownSubmitdate = Session["GodownSubmitdate"].ToString();
            imgStack.Visible = false;
            imgCommodity.Visible = false;



            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(@"usp_Get_Stack_Image", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Godown_ID", SqlDbType.VarChar, 20)
                  .Value = Godownid;
                    cmd.Parameters.Add("@BranchID", SqlDbType.VarChar, 10)
                       .Value = depotID;
                    cmd.Parameters.AddWithValue("@Emp_Id", empid);
                    cmd.Parameters.AddWithValue("@Financial_Year", financialYear);
                    cmd.Parameters.AddWithValue("@Quater_Type", quater);
                    cmd.Parameters.AddWithValue("@verification_Type", verificationtype);
                    cmd.Parameters.AddWithValue("@Order_No", orderNo);
                    cmd.Parameters.AddWithValue("@Stack_ID", stackId);

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (dr["Commodity_Name"] != DBNull.Value)
                            {
                                lblCommodityName.Text = dr["Commodity_Name"].ToString();
                            }

                            if (dr["StackImage"] != DBNull.Value)
                            {
                                byte[] stackImg = (byte[])dr["StackImage"];
                                imgStack.ImageUrl = "data:image/jpeg;base64," +
                                    Convert.ToBase64String(stackImg);
                                imgStack.Visible = true;
                            }

                            if (dr["CommodityImage"] != DBNull.Value)
                            {
                                byte[] commodityImg = (byte[])dr["CommodityImage"];
                                imgCommodity.ImageUrl = "data:image/jpeg;base64," +
                                    Convert.ToBase64String(commodityImg);
                                imgCommodity.Visible = true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

            ClientScript.RegisterStartupScript(
           this.GetType(),
          "alert",
           "alert('Unable to Open Popup. Please try again.');",
           true
       );
        }
    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                object val = DataBinder.Eval(e.Row.DataItem, "TotalBags_AsOnline");
                int onlinebags = -1;

                if (val != null && val != DBNull.Value)
                {
                    int.TryParse(val.ToString(), out onlinebags);
                }

                if (onlinebags == 0)
                {
                    e.Row.CssClass = e.Row.CssClass + " zero-online-row";
                }
            }
            catch
            {

            }
        }
    }

}