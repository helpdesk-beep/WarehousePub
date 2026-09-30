using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using System;

public partial class Inspections_Inspection_Officer_View_Annaxures_Branch_Wise_with_Remark : System.Web.UI.Page
{
    public SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    string PFID = "";
    SqlTransaction sqltran;
    string client_IP = "";
    int a_id = 0;
    string Region_ID = "";
    string Insp_ID = "";
    string Veri_ID = "";
    string currentYear = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        if (!IsPostBack)
        {
            currentYear = DateTime.Now.Year.ToString();
            fillFinsncilYear();
        }
    }
    public void fillFinsncilYear()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Fianancial_Year_For_inspection", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            ddlfinancialyear.DataSource = cmd.ExecuteReader();
            ddlfinancialyear.DataTextField = "Financial_Year";
            ddlfinancialyear.DataValueField = "Financial_Year";
            ddlfinancialyear.DataBind();
            ddlfinancialyear.Items.Insert(0, new ListItem("--Select Financial Year--", "0"));
            con.Close();
        }
    }
    protected void FillAllAnnaxuresRemark()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_All_Annaxures_Remark", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@Inspection_type_ID", Session["hdninsp_type_id"].ToString());
                cmd.Parameters.AddWithValue("@verificatiotype", Session["hdnVerificationType"].ToString());
                //cmd.Parameters.AddWithValue("@Year", ddlyear.SelectedValue);
                cmd.Parameters.AddWithValue("@PF_ID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            txtRemark.Text = dt.Rows[0]["RemarkA"].ToString();
                            if (dt.Rows[0]["RemarkA"].ToString() == null || dt.Rows[0]["RemarkA"].ToString() == "")
                            {
                                btnA.Visible = true;
                            }
                            else
                            {
                                btnA.Visible = false;
                            }
                            txtremarkB.Text = dt.Rows[0]["RemarkB"].ToString();
                            if (dt.Rows[0]["RemarkB"].ToString() == null || dt.Rows[0]["RemarkB"].ToString() == "")
                            {
                                btnB.Visible = true;
                            }
                            else
                            {
                                btnB.Visible = false;
                            }
                            txtremarkc.Text = dt.Rows[0]["RemarkC"].ToString();
                            if (dt.Rows[0]["RemarkC"].ToString() == null || dt.Rows[0]["RemarkC"].ToString() == "")
                            {
                                btnc.Visible = true;
                            }
                            else
                            {
                                btnc.Visible = false;
                            }
                            txtgadnapatrak.Text = dt.Rows[0]["RemarkGP"].ToString();
                            if (dt.Rows[0]["RemarkGP"].ToString() == null || dt.Rows[0]["RemarkGP"].ToString() == "")
                            {
                                btngadnapatrak.Visible = true;
                            }
                            else
                            {
                                btngadnapatrak.Visible = false;
                            }

                        }
                    }
                }
            }
        }
    }
    protected void GetOnlineRecord()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Online_Record_As_par_insepection_Date", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@Quater_Type", Session["hdninsp_type_id"].ToString());
                cmd.Parameters.AddWithValue("@Inspection_Type_ID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            lblOnlineRecord.Text = dt.Rows[0]["OnlineRecord"].ToString();
                        }
                        
                    }
                }
            }
        }
    }
    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Summary_Reports_Annaxure_A_Branch_Wise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                // cmd.Parameters.AddWithValue("@QuarterID", Session["hdninsp_type_id"].ToString());
                // cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnVerificationType"].ToString());
                //// cmd.Parameters.AddWithValue("@Year", ddlyear.SelectedValue);
                // cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@QuarterID", Session["hdninsp_type_id"].ToString());
                cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
                cmd.Parameters.AddWithValue("@OrderNo", Session["lblOrder_No"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            divremarkA.Visible = true;
                            GrdOfficerPreviousInsp.Caption = @"<b style=""font-weight: bold;"">Annaxure A: " + "</b> ";
                            GrdOfficerPreviousInsp.DataSource = dt;
                            GrdOfficerPreviousInsp.DataBind();
                            FillAllAnnaxuresRemark();
                        }
                        else
                        {
                            divremarkA.Visible = false;
                            GrdOfficerPreviousInsp.DataSource = null;
                            GrdOfficerPreviousInsp.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void FillAnnaxureBgrd()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Summary_Reports_Annaxure_B_Branch_Wise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@QuarterID", Session["hdninsp_type_id"].ToString());
                cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnVerificationType"].ToString());
                //cmd.Parameters.AddWithValue("@Year", ddlyear.SelectedValue);
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
                cmd.Parameters.AddWithValue("@OrderNo", Session["lblOrder_No"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            divRemarkB.Visible = true;
                            grdannaxureB.Caption = @"<b style=""font-weight: bold;"">Annaxure B: " + "</b> ";
                            grdannaxureB.DataSource = dt;
                            grdannaxureB.DataBind();
                            FillAllAnnaxuresRemark();

                        }
                        else
                        {
                            divRemarkB.Visible = false;
                            grdannaxureB.DataSource = null;
                            grdannaxureB.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void FillAnnaxureCgrd()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Summary_Reports_Annaxure_C_Branch_Wise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@QuarterID", Session["hdninsp_type_id"].ToString());
                cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnVerificationType"].ToString());
                // cmd.Parameters.AddWithValue("@Year", ddlyear.SelectedValue);
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
                cmd.Parameters.AddWithValue("@OrderNo", Session["lblOrder_No"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            divRemarkC.Visible = true;
                            GrdAnnaxureC.Caption = @"<b style=""font-weight: bold;"">Annaxure C: " + "</b> ";
                            GrdAnnaxureC.DataSource = dt;
                            GrdAnnaxureC.DataBind();
                            FillAllAnnaxuresRemark();
                        }
                        else
                        {
                            divRemarkC.Visible = false;
                            GrdAnnaxureC.DataSource = null;
                            GrdAnnaxureC.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void FillGandanPatrak()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Summary_Reports_Gadna_Patrak_Branch_Wise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@QuarterID", Session["hdninsp_type_id"].ToString());
                cmd.Parameters.AddWithValue("@InspTypeID", Session["hdnVerificationType"].ToString());
                //  cmd.Parameters.AddWithValue("@Year", ddlyear.SelectedValue);
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
                cmd.Parameters.AddWithValue("@OrderNo", Session["lblOrder_No"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            divGadnaPAtrak.Visible = true;
                            Grddagnapatrak.Caption = @"<b style=""font-weight: bold;"">Counting Sheet: " + "</b> ";
                            Grddagnapatrak.DataSource = dt;
                            Grddagnapatrak.DataBind();
                            FillAllAnnaxuresRemark();
                        }
                        else
                        {
                            divGadnaPAtrak.Visible = false;
                            Grddagnapatrak.DataSource = null;
                            Grddagnapatrak.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
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

    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    //protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    protected void btnA_Click(object sender, EventArgs e)
    {
        try
        {
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            foreach (GridViewRow row in GrdOfficerPreviousInsp.Rows)
            {
                Label lblAVl_Bags = (Label)row.FindControl("lblAVl_Bags");
                Label lblAVl_Bags_in_PV = (Label)row.FindControl("lblAVl_Bags_in_PV");
                Label lblSpilage_Bags = (Label)row.FindControl("lblSpilage_Bags");
                Label lblTotalAvlBags2 = (Label)row.FindControl("lblTotalAvlBags2");
                Label lblTotalAvlBags = (Label)row.FindControl("lblTotalAvlBags");


                conStr.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[Insert_Annaxure_A_Final_Summary_With_Remark]", conStr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@QuaterID", Session["hdninsp_type_id"].ToString());
                cmd.Parameters.AddWithValue("@InspectionTypeID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@Year", DateTime.Now.Year.ToString());
                cmd.Parameters.AddWithValue("@According_Online_Record", lblAVl_Bags.Text);
                cmd.Parameters.AddWithValue("@Inspections_Record", lblAVl_Bags_in_PV.Text);
                cmd.Parameters.AddWithValue("@Spilage_Bags", lblSpilage_Bags.Text);
                cmd.Parameters.AddWithValue("@According_Online_Record_Grater_Bags", lblTotalAvlBags2.Text);
                cmd.Parameters.AddWithValue("@According_Online_Record_Less_Bags", lblTotalAvlBags.Text);
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                cmd.Parameters.AddWithValue("@Inserted_By", IPAddress);
                cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Annaxure A Remark SUbmited Successfully')", true);
                    FillAllAnnaxuresRemark();
                }
               
                conStr.Close();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }

    }

    protected void btnB_Click(object sender, EventArgs e)
    {
        try
        {
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            foreach (GridViewRow row in grdannaxureB.Rows)
            {
                Label lblAVl_Bags = (Label)row.FindControl("lblAvailable_Bags");
                Label lblAVl_Bags_in_PV = (Label)row.FindControl("lblAvailable_Bags_as_Per_PV");
                Label lblSpilage_Bags = (Label)row.FindControl("lblSpillage_bags");
                Label lblTotalAvlBags2 = (Label)row.FindControl("lblTotalAvlBags2");
                Label lblTotalAvlBags = (Label)row.FindControl("lblTotalAvlBags");


                conStr.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[Insert_Annaxure_B_Final_Summary_With_Remark]", conStr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@QuaterID", Session["hdninsp_type_id"].ToString());
                cmd.Parameters.AddWithValue("@InspectionTypeID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@Year", DateTime.Now.Year.ToString());
                cmd.Parameters.AddWithValue("@According_Online_Record", lblAVl_Bags.Text);
                cmd.Parameters.AddWithValue("@Inspections_Record", lblAVl_Bags_in_PV.Text);
                cmd.Parameters.AddWithValue("@Spilage_Bags", lblSpilage_Bags.Text);
                cmd.Parameters.AddWithValue("@According_Online_Record_Grater_Bags", lblTotalAvlBags2.Text);
                cmd.Parameters.AddWithValue("@According_Online_Record_Less_Bags", lblTotalAvlBags.Text);
                cmd.Parameters.AddWithValue("@Remark", txtremarkB.Text);
                cmd.Parameters.AddWithValue("@Inserted_By", IPAddress);
                cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Annaxure B Remark SUbmited Successfully')", true);
                    FillAllAnnaxuresRemark();
                }
                conStr.Close();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }
    }

    protected void btnc_Click(object sender, EventArgs e)
    {
        try
        {
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            foreach (GridViewRow row in GrdAnnaxureC.Rows)
            {
                Label lblAVl_Bags = (Label)row.FindControl("lblAvailable_Bags");
                Label lblAVl_Bags_in_PV = (Label)row.FindControl("lblPlaceinbank");



                conStr.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[Insert_Annaxure_C_Final_Summary_With_Remark]", conStr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@QuaterID", Session["hdninsp_type_id"].ToString());
                cmd.Parameters.AddWithValue("@InspectionTypeID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@Year", DateTime.Now.Year.ToString());
                cmd.Parameters.AddWithValue("@According_Online_Record", lblAVl_Bags.Text);
                cmd.Parameters.AddWithValue("@Number_of_receipts_bank", lblAVl_Bags_in_PV.Text);
                cmd.Parameters.AddWithValue("@Remark", txtremarkc.Text);
                cmd.Parameters.AddWithValue("@Inserted_By", IPAddress);
                cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Annaxure C Remark SUbmited Successfully')", true);
                    FillAllAnnaxuresRemark();
                }
                conStr.Close();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }
    }

    protected void btngadnapatrak_Click(object sender, EventArgs e)
    {
        try
        {
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            foreach (GridViewRow row in Grddagnapatrak.Rows)
            {
                Label lblAVl_Bags = (Label)row.FindControl("lblTotal_Bags");
                Label lblAVl_Bags_in_PV = (Label)row.FindControl("lblSpillage_bag");


                conStr.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[Insert_Gadna_Patrak_Final_Summary_With_Remark]", conStr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@QuaterID", Session["hdninsp_type_id"].ToString());
                cmd.Parameters.AddWithValue("@InspectionTypeID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@Year", DateTime.Now.Year.ToString());
                cmd.Parameters.AddWithValue("@According_Online_Record", lblAVl_Bags.Text);
                cmd.Parameters.AddWithValue("@Spilage_Bags", lblAVl_Bags_in_PV.Text);
                cmd.Parameters.AddWithValue("@Remark", txtgadnapatrak.Text);
                cmd.Parameters.AddWithValue("@Inserted_By", IPAddress);
                cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Gadna Patrak Remark SUbmited Successfully')", true);
                    FillAllAnnaxuresRemark();
                }
                conStr.Close();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }
    }

    protected void ddlfinancialyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetOnlineRecord();
        fillScheduleInsp_Grid();
        FillAnnaxureBgrd();
        FillAnnaxureCgrd();
        FillGandanPatrak();
    }
}