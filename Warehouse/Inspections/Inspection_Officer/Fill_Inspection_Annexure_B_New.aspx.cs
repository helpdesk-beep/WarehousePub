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
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
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
    string Month_ID = "";
    string Insp_type = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        PFID = Session["UserId"].ToString();
        Insp_ID = Session["hdnInspection_ID"].ToString();
        Branch_ID = Session["hdnbranchid"].ToString();
        Month_ID = Session["hdninspmonth"].ToString();
        Insp_type = Session["hdninsptype"].ToString();
        if (!IsPostBack)
        {
            lblinspid.Text = Insp_ID.ToString();
            FatchScheduleInspData();
            fillGodownDetails();
            fillGodownType();
            txtmaxcpt.Attributes.Add("readonly", "readonly");
            txtsci_CPT.Attributes.Add("readonly", "readonly");
            ddlhiredtype.Enabled = false;
            ddlStorageType.Enabled = false;
        }

    }

    public void FatchScheduleInspData()
    {
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();

            }
            SqlCommand cmd = new SqlCommand("Insp_GetInspection_Schedule_For_Inspection_Officer", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
            //cmd.Parameters.AddWithValue("@godownID", Session["Godown_ID"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { conStr.Close(); }

            if (dt.Rows.Count > 0)
            {

                lblbranch.Text = dt.Rows[0]["Depotname"].ToString().Trim();
                lblinsptype.Text = dt.Rows[0]["Insp_Type"].ToString().Trim();
                lblInspPeriod.Text = dt.Rows[0]["Insp_Period"].ToString().Trim();
                //Label86.Visible = true;
                //Label79.Text = "यह जानकारी भरी जा चुकी है ।";
                //btnsaveprofile.Visible = false;
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Not Available!')", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('............!')", true);
        }
        finally
        { if (con.State == ConnectionState.Open) { con.Close(); } }
    }

    public void fillGodownDetails()
    {
        using (SqlConnection con = new SqlConnection(constr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Name_For_Inspection", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
            con.Open();
            ddl_gdwn.DataSource = cmd.ExecuteReader();
            ddl_gdwn.DataTextField = "Godown_Name";
            ddl_gdwn.DataValueField = "Godown_ID";
            ddl_gdwn.DataBind();
            ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con.Close();
        }
    }
    protected void ddl_gdwn_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SqlCommand cmd = new SqlCommand("Get_Inspection_stackwiseBal_Annex_B", conStr);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
        //cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    GD_StackBal.DataSource = ds;
        //    GD_StackBal.DataBind();
        //    this.GD_StackBal.Columns[0].Visible = false;
        //    tr_griddata.Visible = true;
        //}
        //else
        //{
        //    GetdataForGrid();
        //}
        GetdataForGrid();
    }
    public void GetdataForGrid()
    {
        SqlCommand cmd = new SqlCommand("Get_Stack_Balance_Details", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
        cmd.Parameters.AddWithValue("@Inspection_ID", Insp_ID);
        cmd.Parameters.AddWithValue("@Quater", Insp_type);
        cmd.Parameters.AddWithValue("@MonthID", Month_ID);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            tr_griddata.Visible = true;
            GD_StackBal.DataSource = ds;
            GD_StackBal.DataBind();          
            GetGdwnData();
            txtQty_TextChanged(null, null);
        }
        else
        {
            tr_griddata.Visible = false;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
            // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found...')", true);
        }
    }
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        int diff = 0; int O_Bags = 0; int P_Bags = 0; int O_sum = 0; int PV_Sum = 0;

        for (int i = 0; GD_StackBal.Rows.Count > i; i++)
        {
            TextBox NameTextBox = (TextBox)GD_StackBal.FindControl("NameTextBox");
            if (((Label)GD_StackBal.Rows[i].FindControl("lblAvlBags")).Text.ToString().Trim() == "")
            {               
                O_Bags = 0;
            }
            else
            {
            O_Bags = Convert.ToInt32(((Label)GD_StackBal.Rows[i].FindControl("lblAvlBags")).Text.ToString().Trim());

            }
            if (((TextBox)GD_StackBal.Rows[i].FindControl("GtxtbagsActual")).Text.ToString().Trim() == "")
            {
                P_Bags = 0;
            }
            else
            {
                P_Bags = Convert.ToInt32(((TextBox)GD_StackBal.Rows[i].FindControl("GtxtbagsActual")).Text.ToString().Trim());
            }
           // diff = P_Bags - O_Bags;
            diff = O_Bags - P_Bags;
            O_sum = O_Bags + O_sum;
            PV_Sum = P_Bags + PV_Sum;
            lblTotalBags_O.Text = Convert.ToString(O_sum);
            lblTptalBags_PV.Text = Convert.ToString(PV_Sum);
            ((TextBox)GD_StackBal.Rows[i].FindControl("GtxtDiffBags")).Text = Convert.ToString(diff);
            
        }
    }
    private void fillGodownType()
    {
        try
        {
            string query = "";
            query = "SELECT  [Gid],[GodownType],[TypeValue] FROM [dbo].[GodownTypeMaster]";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlhiredtype.Items.Clear();
                ddlhiredtype.DataSource = ds.Tables[0];
                ddlhiredtype.DataTextField = "GodownType";
                ddlhiredtype.DataValueField = "GodownType";
                ddlhiredtype.DataBind();
                ddlhiredtype.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlhiredtype.Items.Clear();
            }
        }
        catch (Exception)
        {
        }
    }
    public void GetGdwnData()
    {
        SqlCommand cmd = new SqlCommand("Get_Godown_Capacity_Details", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
       
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtmaxcpt.Text = dt.Rows[0]["Godown_Capacity"].ToString().Trim();
            txtsci_CPT.Text = dt.Rows[0]["Godown_Scientific_capacity"].ToString().Trim();
            ddlhiredtype.SelectedValue = dt.Rows[0]["Hired_Type"].ToString().Trim();
            ddlStorageType.SelectedValue = dt.Rows[0]["Storage_Type"].ToString().Trim();
           
        }
        else
        {

        }
    }
    public void checkvalidation()
    {
        if (txt_inspdate.Text == "" || txt_inspdate.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Inspection Date!....')", true);
            txt_inspdate.Focus();
            return;
        }

    }

    public void fillbagstxt()
    {
        for (int i = 0; GD_StackBal.Rows.Count > i; i++)
        {
            ((TextBox)GD_StackBal.Rows[i].FindControl("GtxtbagsActual")).Text = GD_StackBal.Rows[i].Cells[4].Text.ToString();
        }
    }
    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        SqlTransaction tn = null;
        foreach (GridViewRow row in GD_StackBal.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lblStack_ID = row.FindControl("lblStack_ID") as Label;
                Label lblStack_Name = row.FindControl("lblStack_Name") as Label;
                Label lblStack_capacity = row.FindControl("lblStack_capacity") as Label;
                Label lblCommodity_Name = row.FindControl("lblCommodity_Name") as Label;
                Label lblAvlBags = row.FindControl("lblAvlBags") as Label;
                Label lblAvlQty = row.FindControl("lblAvlQty") as Label;

                TextBox GtxtbagsActual = row.FindControl("GtxtbagsActual") as TextBox;
                TextBox GtxtDiffBags = row.FindControl("GtxtDiffBags") as TextBox;
                //DropDownList ddlDiffBagstYpe = row.FindControl("ddlDiffBagstYpe") as DropDownList;
                //DropDownList ddlclassifi = row.FindControl("ddlclassifi") as DropDownList;
                //DropDownList ddlStackType = row.FindControl("ddlStackType") as DropDownList;
                //TextBox GtxtLastFDate = row.FindControl("GtxtLastFDate") as TextBox;
                TextBox GtxtRemark = row.FindControl("GtxtRemark") as TextBox;

                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                try
                {
                    checkvalidation();
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Inspection_stackwiseBal_Annex_B_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    tn = con.BeginTransaction();
                    cmd.Transaction = tn;
                    //cmd.Parameters.AddWithValue("@ID", hdnid.Value);
                    cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
                    cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
                    cmd.Parameters.AddWithValue("@Inspection_Date", getDate_MDY(txt_inspdate.Text));
                    cmd.Parameters.AddWithValue("@Stack_ID", lblStack_ID.Text);
                    cmd.Parameters.AddWithValue("@Stack_Name", lblStack_Name.Text);
                    cmd.Parameters.AddWithValue("@Stack_capacity", lblStack_capacity.Text);
                    cmd.Parameters.AddWithValue("@Commodity_Name", lblCommodity_Name.Text);
                    cmd.Parameters.AddWithValue("@AvlBags", lblAvlBags.Text);
                    cmd.Parameters.AddWithValue("@AvlQty", lblAvlQty.Text);
                    cmd.Parameters.AddWithValue("@Avlailable_Bags_As_Per_PV", GtxtbagsActual.Text);
                    cmd.Parameters.AddWithValue("@Difference_of_Bags", GtxtDiffBags.Text);
                    cmd.Parameters.AddWithValue("@Remark", GtxtRemark.Text);
                    cmd.Parameters.AddWithValue("@IP_Address", ipAddress);
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {

                        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                        SqlCommand cmd2 = new SqlCommand("Tbl_JVS_Inspection_Stack_Wise_Entry_Insert", con2);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        con2.Open();
                        cmd2.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
                        cmd2.Parameters.AddWithValue("@Stack_ID", lblStack_ID.Text);
                        cmd2.Parameters.AddWithValue("@Avlailable_Bags_As_Per_PV", GtxtbagsActual.Text);
                        cmd2.Parameters.AddWithValue("@Difference_of_Bags", GtxtDiffBags.Text);
                        cmd2.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
                        cmd2.Parameters.AddWithValue("@Quater", Month_ID);
                        cmd2.Parameters.AddWithValue("@Inspection_Month", Insp_type);
                        cmd2.Parameters.AddWithValue("@Inspection_Date", getDate_MDY(txt_inspdate.Text));
                        cmd2.Parameters.AddWithValue("@Insert_By", ipAddress);
                        cmd2.Parameters.AddWithValue("@Remark", GtxtRemark.Text);
                        cmd2.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                        cmd2.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                        cmd2.ExecuteNonQuery();
                        TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                        if (TheResult.StartsWith("SUCCESS"))
                        {
                            tn.Commit();
                            string strMsg = "Inspection Details Successfully submitted |||";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                            GetdataForGrid();
                        }
                        con2.Close();
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
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('"+ ex.Message + "');", true);
                }
            }
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
}