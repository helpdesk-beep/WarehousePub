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

public partial class Inspections_Inspection_Officer_Annaxure_C : System.Web.UI.Page
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
    public void fillGodownDetails()
    {
        using (SqlConnection con2 = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Name_For_Anaxure_C", con2);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
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
        SqlCommand cmd = new SqlCommand("[dbo].[Get_Annaxure_C_WHR_Wise_Details_for_IO]", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Branch_id", ddlbranch.SelectedValue);
        cmd.Parameters.AddWithValue("@Godown_id", ddl_gdwn.SelectedValue);
        cmd.Parameters.AddWithValue("@inp_date", txt_inspdate.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
             GD_StackBal.Caption = @"<b style=""font-weight: bold;"">Last Inspection Date: " + "  -   " + dt.Rows[0]["Inspection_Date"].ToString() +"</b> ";
            tr_griddata.Visible = true;
            GD_StackBal.DataSource = dt;
            GD_StackBal.DataBind();
            btnhideshow.Visible = true;
            GD_StackBal.FooterRow.Style.Add("text-align", "center");
            GD_StackBal.FooterRow.Cells[6].Text = "Total";
            GD_StackBal.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("No_of_Bags")).ToString();
            GD_StackBal.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Quantity")).ToString();
        }
        else
        {
            tr_griddata.Visible = false;
            btnhideshow.Visible = false;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
            //string strMsg = "गोदाम " + ddl_gdwn.SelectedItem.ToString() + " की एंट्री हो चुकी है |";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "')", true);
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
        fillGodownDetails();
    }

    //protected void btn_saveInspDate_Click(object sender, EventArgs e)
    //{
    //    SqlTransaction tn = null;
    //    foreach (GridViewRow row in GD_StackBal.Rows)
    //    {
    //        if (row.RowType == DataControlRowType.DataRow)
    //        {
    //            HiddenField hdndepositerid = row.FindControl("hdndepositerid") as HiddenField;
    //            HiddenField hdnCommodity_Id = row.FindControl("hdnCommodity_Id") as HiddenField;
    //            Label lblGodown_Name = row.FindControl("lblGodown_Name") as Label;
    //            Label lblCreatedDate = row.FindControl("lblCreatedDate") as Label;
    //            Label lblDepositor_whr_id = row.FindControl("lblDepositor_whr_id") as Label;
    //            Label lblAvlBags = row.FindControl("lblAvlBags") as Label;
    //            Label lblAvlQty = row.FindControl("lblAvlQty") as Label;
    //            TextBox lblMktValue_of_Commodity = row.FindControl("lblMktValue_of_Commodity") as TextBox;
    //            TextBox GtxtRemark = row.FindControl("GtxtRemark") as TextBox;
    //            //Label lblPrice = row.FindControl("lblPrice") as Label;
    //            string ipAddress;
    //            ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
    //            if (ipAddress == "" || ipAddress == null)
    //                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
    //            try
    //            {

    //                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
    //                SqlCommand cmd = new SqlCommand("Annaxeure_C_Entry_By_IO_Insert", con);
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                con.Open();
    //                tn = con.BeginTransaction();
    //                cmd.Transaction = tn;
    //                //cmd.Parameters.AddWithValue("@ID", hdnid.Value);
    //                cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
    //                cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
    //                cmd.Parameters.AddWithValue("@Employee_ID", PFID.ToString());
    //                cmd.Parameters.AddWithValue("@Depositer_ID", hdndepositerid.Value);
    //                cmd.Parameters.AddWithValue("@Commodity_ID", hdnCommodity_Id.Value);
    //                cmd.Parameters.AddWithValue("@WHR_ID", lblDepositor_whr_id.Text);
    //                cmd.Parameters.AddWithValue("@Avl_Bags", lblAvlBags.Text);
    //                cmd.Parameters.AddWithValue("@Avl_Quantity", lblAvlQty.Text);
    //                cmd.Parameters.AddWithValue("@Place_In_Bank", lblMktValue_of_Commodity.Text);
    //                cmd.Parameters.AddWithValue("@Remark", GtxtRemark.Text);
    //                cmd.Parameters.AddWithValue("@Insert_BY", ipAddress);
    //                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
    //                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
    //                cmd.ExecuteNonQuery();
    //                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

    //                if (TheResult.StartsWith("SUCCESS"))
    //                {

    //                    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    //                    SqlCommand cmd2 = new SqlCommand("Annaxeure_C_Entry_By_IO_Insert", con2);
    //                    cmd2.CommandType = CommandType.StoredProcedure;
    //                    con2.Open();
    //                    cmd2.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
    //                    cmd2.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
    //                    cmd2.Parameters.AddWithValue("@Employee_ID", PFID.ToString());
    //                    cmd2.Parameters.AddWithValue("@Depositer_ID", hdndepositerid.Value);
    //                    cmd2.Parameters.AddWithValue("@Commodity_ID", hdnCommodity_Id.Value);
    //                    cmd2.Parameters.AddWithValue("@WHR_ID", lblDepositor_whr_id.Text);
    //                    cmd2.Parameters.AddWithValue("@Avl_Bags", lblAvlBags.Text);
    //                    cmd2.Parameters.AddWithValue("@Avl_Quantity", lblAvlQty.Text);
    //                    cmd2.Parameters.AddWithValue("@Place_In_Bank", lblMktValue_of_Commodity.Text);
    //                    cmd2.Parameters.AddWithValue("@Remark", GtxtRemark.Text);
    //                    cmd2.Parameters.AddWithValue("@Insert_BY", ipAddress);
    //                    cmd2.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
    //                    cmd2.Parameters["@TheResult"].Direction = ParameterDirection.Output;
    //                    cmd2.ExecuteNonQuery();
    //                    TheResult = cmd.Parameters["@TheResult"].Value.ToString();

    //                    if (TheResult.StartsWith("SUCCESS"))
    //                    {
    //                        tn.Commit();
    //                        string strMsg = "Annaxure C submitted Successfully  |||";
    //                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
    //                        GetdataForGrid();
    //                    }
    //                    con2.Close();
    //                }
    //                else
    //                {
    //                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
    //                }
    //                con.Close();
    //            }
    //            catch (Exception ex)
    //            {
    //                tn.Rollback();
    //                //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
    //                Console.WriteLine(ex.Message);
    //            }
    //        }
    //    }
    //}
    protected void GD_StackBal_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}