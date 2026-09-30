using System;
using System.Collections;
using System.Configuration;
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

public partial class Inspections_Officer_Godown_wise_Stock_Details : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    string PFID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        PFID = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            fillBranchDetails();
            //GetEmployeeInspectionDetails(PFID);
            // ddlbranch.SelectedValue=

        }
    }
    //public void GetEmployeeInspectionDetails(string PFID)
    //{
    //    SqlCommand cmd = new SqlCommand("[dbo].[Get_Inspection_Quater_Details]", conStr);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Employee_ID", PFID);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        hdninspectionid.Value = dt.Rows[0]["ID"].ToString();
    //        hdnmonth.Value = dt.Rows[0]["Inspection_month_ID"].ToString();
    //        hdnquater.Value = dt.Rows[0]["Inspection_type_ID"].ToString();
    //    }

    //}
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
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodownDetails();
    }

    public void fillGodownDetails()
    {
        using (SqlConnection con2 = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_NameFor_Gadna_Patrak", con2);
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


    public void fillScheduleInsp_Grid()
    {
        SqlCommand cmd = new SqlCommand("Get_Stock_Position_Stack_Wise", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
        cmd.Parameters.AddWithValue("@godown_ID", ddl_gdwn.SelectedValue);
        cmd.Parameters.AddWithValue("@cropyear", ddlcropyear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GD_StackBal.Caption = @"<b style=""font-weight: bold;"">Godown Name: " + "  -   " + ddl_gdwn.SelectedItem.ToString() + "</b> ";
            divshow.Visible = true;
            divbtn.Visible = true;
            GD_StackBal.DataSource = dt;
            GD_StackBal.DataBind();
            //GD_StackBal.FooterRow.Style.Add("text-align", "center");
            //GD_StackBal.FooterRow.Cells[11].Text = "Total";
            //GD_StackBal.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<int>("No_of_Bags")).ToString();
            //GD_StackBal.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Bags")).ToString();

        }
        else
        {
            divshow.Visible = false;
            divbtn.Visible = false;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
            string strMsg = "इस गोदाम की वर्ष " + ddlcropyear.SelectedValue.ToString() + " की एंट्री फाइनल एंट्री कर दी गई हैं यदि फाइनल एंट्री में कोई संशोधन करना हो तो कृपया RM ऑफिस से संपर्क करे  |";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "')", true);
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }
    protected void GD_StackBal_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GD_StackBal_RowCommand(object sender, GridViewCommandEventArgs e)
    {



    }
    protected void GD_StackBal_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            foreach (GridViewRow row in GD_StackBal.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hdnDepositer_ID = (HiddenField)row.FindControl("hdnDepositer_ID");
                    HiddenField hdnCommodity_ID = (HiddenField)row.FindControl("hdnCommodity_ID");
                    HiddenField hdncropyear = (HiddenField)row.FindControl("hdncropyear");
                    Label lblstack_id = (Label)row.FindControl("lblstack_id");
                    Label lblStack_Name = (Label)row.FindControl("lblStack_Name");
                    Label lblTotal_Bags = (Label)row.FindControl("lblTotal_Bags");
                    TextBox lblSpillage_bag = (TextBox)row.FindControl("lblSpillage_bag");
                    DropDownList ddlclassifi = (DropDownList)row.FindControl("ddlclassifi");
                    DropDownList ddlStackType = (DropDownList)row.FindControl("ddlStackType");
                    TextBox lblPV_Bags = (TextBox)row.FindControl("lblPV_Bags");
                    TextBox GtxtRemark = (TextBox)row.FindControl("GtxtRemark");
                    TextBox Gtxtdepositformdate = (TextBox)row.FindControl("Gtxtdepositformdate");
                    if (lblPV_Bags.Text != "0")
                    {
                        conStr.Open();
                        SqlCommand cmd = new SqlCommand("[dbo].[Annaxure_B_Entry_By_IO_Insert]", conStr);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
                        cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
                        cmd.Parameters.AddWithValue("@Employee_ID", PFID.ToString());
                        cmd.Parameters.AddWithValue("@Stack_ID", lblstack_id.Text);
                        cmd.Parameters.AddWithValue("@Stack_Name", lblStack_Name.Text);
                        cmd.Parameters.AddWithValue("@Depositer_ID", hdnDepositer_ID.Value);
                        cmd.Parameters.AddWithValue("@Commodity_Id", hdnCommodity_ID.Value);
                        cmd.Parameters.AddWithValue("@Crop_Year", hdncropyear.Value);
                        cmd.Parameters.AddWithValue("@Available_Bags", lblTotal_Bags.Text);
                        cmd.Parameters.AddWithValue("@Spillage_bags", lblSpillage_bag.Text);
                        cmd.Parameters.AddWithValue("@Classification", ddlclassifi.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Stack_Type", ddlStackType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Available_Bags_as_Per_PV", lblPV_Bags.Text);
                        cmd.Parameters.AddWithValue("@Remark", GtxtRemark.Text);
                        cmd.Parameters.AddWithValue("@Insert_By", IPAddress);
                        cmd.Parameters.AddWithValue("@Fumigation_date", getDate_MDY(Gtxtdepositformdate.Text));
                        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                        if (TheResult.StartsWith("SUCCESS"))
                        {
                            count++;
                            divshow.Visible = false;
                            divbtn.Visible = false;
                            fillScheduleInsp_Grid();
                        }
                        conStr.Close();
                    }
                }
                if (count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Annaxure B SUbmited Successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Annaxure B Not SUbmited')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
        finally
        {
            conStr.Close();
        }

    }
}