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

public partial class Inspections_New_Fill_Inspection_Annexure_B_New : System.Web.UI.Page
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
    string Branch_ID = "";
    string Insp_ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        PFID = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            fillBranchDetails();
            GetEmployeeInspectionDetails(PFID);
            fillGodownType();
            fillFinsncilYear();
        }

    }
    public void GetEmployeeInspectionDetails(string PFID)
    {
        SqlCommand cmd = new SqlCommand("[dbo].[Get_Inspection_Quater_Details]", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Employee_ID", PFID);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            hdninspectionid.Value = dt.Rows[0]["ID"].ToString();
            hdnmonth.Value = dt.Rows[0]["Inspection_month_ID"].ToString();
            hdnquater.Value = dt.Rows[0]["Inspection_type_ID"].ToString();
            Session["Order_Date"] = dt.Rows[0]["Order_Date"].ToString();
        }

    }
    public void fillBranchDetails()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Branch_Name_For_DF", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PFID_ID", PFID);
            con.Open();
            ddlbranch.DataSource = cmd.ExecuteReader();
            ddlbranch.DataTextField = "Depo_Name";
            ddlbranch.DataValueField = "Branch_ID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
            con.Close();
        }
    }
    public void fillGodownType()
    {
        using (SqlConnection con2 = new SqlConnection(constr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Type", con2);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con2.Open();
            ddldodowntype.DataSource = cmd.ExecuteReader();
            ddldodowntype.DataTextField = "Hired_Type";
            ddldodowntype.DataValueField = "Hired_Type";
            ddldodowntype.DataBind();
            ddldodowntype.Items.Insert(0, new ListItem("-- Select Godown Type --", "0"));
            con2.Close();
        }
    }
    public void fillGodownDetails()
    {
        using (SqlConnection con2 = new SqlConnection(constr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Name_For_Depositer_Delivery_Form", con2);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
            cmd.Parameters.AddWithValue("@HireType", ddldodowntype.SelectedValue);
            con2.Open();
            ddl_gdwn.DataSource = cmd.ExecuteReader();
            ddl_gdwn.DataTextField = "Godown_Name";
            ddl_gdwn.DataValueField = "Godown_ID";
            ddl_gdwn.DataBind();
            ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con2.Close();
        }
    }
    public void GetdataForGrid()
    {
        SqlCommand cmd = new SqlCommand("[dbo].[Inspection_for_Depositer_Details]", con2);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
        cmd.Parameters.AddWithValue("@GodownID", ddl_gdwn.SelectedValue);
        //cmd.Parameters.AddWithValue("@Inspection_ID", hdninspectionid.Value);
        cmd.Parameters.AddWithValue("@Date", getDate_MDY(txt_inspdate.Text));
        cmd.Parameters.AddWithValue("@ToDate", getDate_MDY(txttodate.Text));
        cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            tr_griddata.Visible = true;
            GD_StackBal.DataSource = ds;
            GD_StackBal.DataBind();
            btnhideshow.Visible = true;
        }
        else
        {
            tr_griddata.Visible = false;
            btnhideshow.Visible = false;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
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
        if (!string.IsNullOrEmpty(txt_inspdate.Text))
        {
            GetdataForGrid();
        }
        else
        {
           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Inspection Date.');", true);
        }
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillGodownDetails();
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
    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        SqlTransaction tn = null;
        foreach (GridViewRow row in GD_StackBal.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                TextBox GtxtdepositformNo = row.FindControl("GtxtdepositformNo") as TextBox;
                TextBox Gtxtdepositformdate = row.FindControl("Gtxtdepositformdate") as TextBox;
                Label lblWhr_No = row.FindControl("lblWhr_No") as Label;
                Label lblCreatedDate = row.FindControl("lblCreatedDate") as Label;
                HiddenField hdnCommodity_Id = row.FindControl("hdnCommodity_Id") as HiddenField;
                Label lblAvlBags = row.FindControl("lblAvlBags") as Label;
                Label lblAvlQty = row.FindControl("lblAvlQty") as Label;
                Label lblMktValue_of_Commodity = row.FindControl("lblMktValue_of_Commodity") as Label;
                //Label lblPrice = row.FindControl("lblPrice") as Label;

                DropDownList ddlgrade = row.FindControl("ddlgrade") as DropDownList;
                DropDownList ddlsgndepositer = row.FindControl("ddlsgndepositer") as DropDownList;
                DropDownList ddlBS = row.FindControl("ddlBS") as DropDownList;
                DropDownList ddlgatrpass = row.FindControl("ddlgatrpass") as DropDownList;
                DropDownList ddltollslip = row.FindControl("ddltollslip") as DropDownList;
                DropDownList ddltruckparchi = row.FindControl("ddltruckparchi") as DropDownList;
                TextBox GtxtRemark = row.FindControl("GtxtRemark") as TextBox;

                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                try
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Insp_Depositer_Form_Entry_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    tn = con.BeginTransaction();
                    cmd.Transaction = tn;
                    //cmd.Parameters.AddWithValue("@ID", hdnid.Value);
                    cmd.Parameters.AddWithValue("@Emp_ID", PFID);
                    cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
                    cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
                    cmd.Parameters.AddWithValue("@Inspection_Date", getDate_MDY(txt_inspdate.Text));
                    cmd.Parameters.AddWithValue("@deposit_form_No", GtxtdepositformNo.Text);
                    cmd.Parameters.AddWithValue("@Date_of_deposit_form", getDate_MDY(Gtxtdepositformdate.Text));
                    cmd.Parameters.AddWithValue("@WHR_No", lblWhr_No.Text);
                    cmd.Parameters.AddWithValue("@WHR_Date", getDate_MDY(lblCreatedDate.Text));
                    cmd.Parameters.AddWithValue("@Commodity_ID", hdnCommodity_Id.Value);
                    cmd.Parameters.AddWithValue("@No_of_Bags", lblAvlBags.Text);
                    cmd.Parameters.AddWithValue("@Quantity", lblAvlQty.Text);
                    cmd.Parameters.AddWithValue("@Mkt_value", lblMktValue_of_Commodity.Text);
                    cmd.Parameters.AddWithValue("@Price", 0);
                    cmd.Parameters.AddWithValue("@Grade", ddlgrade.SelectedValue);
                    cmd.Parameters.AddWithValue("@Signature_of_depositor", ddlsgndepositer.SelectedValue);
                    cmd.Parameters.AddWithValue("@Signature_of_BM", ddlBS.SelectedValue);
                    cmd.Parameters.AddWithValue("@Deposit_Gate_Pass", ddlgatrpass.SelectedValue);
                    cmd.Parameters.AddWithValue("@Kata_Parchi", ddltollslip.SelectedValue);
                    cmd.Parameters.AddWithValue("@Truck_Chalan", ddltruckparchi.SelectedValue);
                    cmd.Parameters.AddWithValue("@Remark", GtxtRemark.Text);
                    cmd.Parameters.AddWithValue("@Createt_By", ipAddress);
                    cmd.Parameters.AddWithValue("@Insp_Date", getDate_MDY(Session["Order_Date"].ToString()));
                    cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
                    //cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    //cmd.ExecuteNonQuery();
                    //string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        if (!string.IsNullOrEmpty(GtxtdepositformNo.Text))
                        {
                            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                            SqlCommand cmd2 = new SqlCommand("Tbl_JVS_Inspection_Entry_Insert", con2);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            con2.Open();
                            //cmd.Parameters.AddWithValue("@ID", hdnid.Value);
                            cmd2.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
                            cmd2.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
                            cmd2.Parameters.AddWithValue("@WHR_ID", lblWhr_No.Text);
                            cmd2.Parameters.AddWithValue("@Inspection_ID", hdninspectionid.Value);
                            cmd2.Parameters.AddWithValue("@Quater", hdnquater.Value);
                            cmd2.Parameters.AddWithValue("@Inspection_Month", hdnmonth.Value);
                            cmd2.Parameters.AddWithValue("@Inspection_Date", getDate_MDY(txt_inspdate.Text));
                            cmd2.Parameters.AddWithValue("@Insert_By", ipAddress);
                            cmd2.Parameters.AddWithValue("@Emp_ID", PFID);
                            cmd2.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
                            cmd2.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                            cmd2.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                            cmd2.ExecuteNonQuery();
                            string TheResult2 = cmd.Parameters["@TheResult"].Value.ToString();

                            if (TheResult2.StartsWith("SUCCESS"))
                            {
                                tn.Commit();
                                string strMsg = "Inspection Details Successfully submitted |||";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                                GetdataForGrid();
                            }
                            con2.Close();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    tn.Rollback();
                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
    protected void GD_StackBal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //    DropDownList ddlgrade = (e.Row.FindControl("ddlgrade") as DropDownList);
        //    DropDownList ddlsgndepositer = (e.Row.FindControl("ddlsgndepositer") as DropDownList);
        //    DropDownList ddlBS = (e.Row.FindControl("ddlBS") as DropDownList);
        //    DropDownList ddlgatrpass = (e.Row.FindControl("ddlgatrpass") as DropDownList);
        //    DropDownList ddltollslip = (e.Row.FindControl("ddltollslip") as DropDownList);
        //    DropDownList ddltruckparchi = (e.Row.FindControl("ddltruckparchi") as DropDownList);

        //    SqlCommand cmd = new SqlCommand("[dbo].[Inspection_for_Depositer_Details]", con2);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
        //    cmd.Parameters.AddWithValue("@GodownID", ddl_gdwn.SelectedValue);
        //    cmd.Parameters.AddWithValue("@Date", getDate_MDY(txt_inspdate.Text));
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    ddlgrade.SelectedValue= dt.Rows[0]["Grade"].ToString().Trim();
        //    ddlsgndepositer.SelectedValue= dt.Rows[0]["Signature_of_depositor"].ToString().Trim();
        //    ddlBS.SelectedValue= dt.Rows[0]["Signature_of_BM"].ToString().Trim();
        //    ddlgatrpass.SelectedValue= dt.Rows[0]["Deposit_Gate_Pass"].ToString().Trim();
        //    ddltollslip.SelectedValue= dt.Rows[0]["Kata_Parchi"].ToString().Trim();
        //    ddltruckparchi.SelectedValue= dt.Rows[0]["Truck_Chalan"].ToString().Trim();
        //}

    }

    protected void ddldodowntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodownDetails();
    }
}