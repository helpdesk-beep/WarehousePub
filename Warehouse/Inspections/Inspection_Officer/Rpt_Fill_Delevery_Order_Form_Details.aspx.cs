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

public partial class Inspections_Inspection_Officer_Rpt_Fill_Delevery_Order_Form_Details : System.Web.UI.Page
{
    public SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
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
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Name_For_DF", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
            con.Open();
            ddl_gdwn.DataSource = cmd.ExecuteReader();
            ddl_gdwn.DataTextField = "Godown_Name";
            ddl_gdwn.DataValueField = "Godown_ID";
            ddl_gdwn.DataBind();
            ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con.Close();
        }
    }

    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = GD_StackBal.Rows[rowIndex];

        //Session["S_PFID"] = (row.FindControl("hdnid") as HiddenField).Value;
        Session["hdnbranchid"] = (row.FindControl("hdnbranchid") as HiddenField).Value;
        Session["hdngodownid"] = (row.FindControl("hdngodownid") as HiddenField).Value;
        Session["hdncommodityid"] = (row.FindControl("hdncommodityid") as HiddenField).Value;
        Session["hdnInspDate"] = (row.FindControl("hdnInspDate") as HiddenField).Value;
        Session["hdnDO_No"] = (row.FindControl("hdnDO_No") as HiddenField).Value;
        // GetDist(Session["UserName"].ToString());
       // lblgdnname.Text = (row.FindControl("lblGodown_Name") as Label).Text;
        GtxtdepositformNo.Text = (row.FindControl("GtxtdepositformNo") as Label).Text; ;
        Gtxtdepositformdate.Text = (row.FindControl("Gtxtdepositformdate") as Label).Text;
        lblDONO.Text = (row.FindControl("lblDO_No") as Label).Text;
        lblDODATE.Text = (row.FindControl("lblDO_Date") as Label).Text;
        lblCommodity.Text = (row.FindControl("lblCommodity_Name") as Label).Text;
        lblBages.Text = (row.FindControl("lblTotalBags_Issued") as Label).Text;
        lblWeight.Text = (row.FindControl("lblQty_Issued_Weight") as Label).Text;

        ddlsgndepositer.SelectedValue = (row.FindControl("hdnSignature_of_depositorID") as HiddenField).Value;
        ddlBS.SelectedValue = (row.FindControl("hdnSignature_of_BMID") as HiddenField).Value;
        ddlgatrpass.SelectedValue = (row.FindControl("hdnDeposit_Gate_PassID") as HiddenField).Value;
        ddltollslip.SelectedValue = (row.FindControl("hdnKata_ParchiID") as HiddenField).Value;
        ddltruckparchi.SelectedValue = (row.FindControl("hdnTruck_ChalanID") as HiddenField).Value;
        ddlcancilWHR.SelectedValue = (row.FindControl("hdnCancil_WHRID") as HiddenField).Value;
       
        txtRemark.Text = (row.FindControl("GtxtRemark") as Label).Text;

        Session["GtxtdepositformNo"] = GtxtdepositformNo.Text.ToString();
        Session["Gtxtdepositformdate"] = Gtxtdepositformdate.Text.ToString();
        Session["ddlsgndepositer"] = ddlsgndepositer.SelectedValue.ToString();
        Session["ddlBS"] = ddlBS.SelectedValue.ToString();
        Session["ddlgatrpass"] = ddlgatrpass.SelectedValue.ToString();
        Session["ddltollslip"] = ddltollslip.SelectedValue.ToString();
        Session["ddltruckparchi"] = ddltruckparchi.SelectedValue.ToString();
        Session["ddlcancilWHR"] = ddlcancilWHR.SelectedValue.ToString();

        //fillScheduleInsp_Grid(Session["S_PFID"].ToString());
        // GetEmployeeDetails();
        divNewInsp.Visible = true;
        ModalPopupExtender1.Show();
    }
    public void GetdataForGrid()
    {
        SqlCommand cmd = new SqlCommand("[dbo].[Get_Delevery_Order_Entry_By_Inpection_Officer]", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Branch_id", ddlbranch.SelectedValue);
        cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
        cmd.Parameters.AddWithValue("@EmpID", PFID);
        cmd.Parameters.AddWithValue("@InspDate", getDate_MDY(txt_inspdate.Text));
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            tr_griddata.Visible = true;
            GD_StackBal.DataSource = dt;
            GD_StackBal.DataBind();
            //txt_inspdate.Text = dt.Rows[0]["Inspection_Date"].ToString();
        }
        else
        {
            tr_griddata.Visible = false;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        GetdataForGrid();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodownDetails();
    }

   
    protected void GD_StackBal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void GD_StackBal_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited.  
        GD_StackBal.EditIndex = e.NewEditIndex;
        GetdataForGrid();
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

    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];

        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Insp_DO_Form_Entry_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            //cmd.Parameters.AddWithValue("@ID", Session["S_PFID"].ToString());
            cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
            cmd.Parameters.AddWithValue("@Godown_ID", Session["hdngodownid"].ToString());
            //cmd.Parameters.AddWithValue("@Inspection_Date", getDate_MDY(Session["hdnInspDate"].ToString()));
            cmd.Parameters.AddWithValue("@Inspection_Date", getDate_MDY(txt_inspdate.Text));
            cmd.Parameters.AddWithValue("@DO_form_No", GtxtdepositformNo.Text);
            cmd.Parameters.AddWithValue("@Date_of_DO_form", getDate_MDY(Gtxtdepositformdate.Text));
            cmd.Parameters.AddWithValue("@DO_No", Session["hdnDO_No"].ToString());
            cmd.Parameters.AddWithValue("@Commodity_ID", Session["hdncommodityid"].ToString());
            cmd.Parameters.AddWithValue("@Signature_of_depositor", ddlsgndepositer.SelectedValue);
            cmd.Parameters.AddWithValue("@Signature_of_BM", ddlBS.SelectedValue);
            cmd.Parameters.AddWithValue("@Deposit_Gate_Pass", ddlgatrpass.SelectedValue);
            cmd.Parameters.AddWithValue("@Kata_Parchi", ddltollslip.SelectedValue);
            cmd.Parameters.AddWithValue("@Truck_Chalan", ddltruckparchi.SelectedValue);
            cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
            cmd.Parameters.AddWithValue("@Createt_By", ipAddress);
            cmd.Parameters.AddWithValue("@Cancil_WHR", ddlcancilWHR.SelectedValue);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
            
            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "DO Details Update Successfully submitted |||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                ModalPopupExtender1.Show();
                GetdataForGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
            }
        }
        catch (Exception ex)
        {

            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
            Console.WriteLine(ex.Message);
        }
    }
    protected void GD_StackBal_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        //string ipAddress;
        //ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //if (ipAddress == "" || ipAddress == null)
        //    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        ////Finding the controls from Gridview for the row which is going to update  
        //HiddenField hdnbranchid = GD_StackBal.Rows[e.RowIndex].FindControl("hdnbranchid") as HiddenField;
        //HiddenField hdngodownid = GD_StackBal.Rows[e.RowIndex].FindControl("hdngodownid") as HiddenField;
        //HiddenField hdncommodityid = GD_StackBal.Rows[e.RowIndex].FindControl("hdncommodityid") as HiddenField;
        //HiddenField hdnid = GD_StackBal.Rows[e.RowIndex].FindControl("hdnid") as HiddenField;
        //TextBox txt_DO_form_No = GD_StackBal.Rows[e.RowIndex].FindControl("txt_DO_form_No") as TextBox;
        //TextBox txt_DO_Date = GD_StackBal.Rows[e.RowIndex].FindControl("txt_DO_Date") as TextBox;
        //DropDownList ddlsgndepositer = GD_StackBal.Rows[e.RowIndex].FindControl("ddlsgndepositer") as DropDownList;
        //DropDownList ddlBS = GD_StackBal.Rows[e.RowIndex].FindControl("ddlBS") as DropDownList;
        //DropDownList ddlgatrpass = GD_StackBal.Rows[e.RowIndex].FindControl("ddlgatrpass") as DropDownList;
        //DropDownList ddltollslip = GD_StackBal.Rows[e.RowIndex].FindControl("ddltollslip") as DropDownList;
        //DropDownList ddltruckparchi = GD_StackBal.Rows[e.RowIndex].FindControl("ddltruckparchi") as DropDownList;
        //DropDownList ddlcancilWHR = GD_StackBal.Rows[e.RowIndex].FindControl("ddlcancilWHR") as DropDownList;
        //TextBox txtRemark = GD_StackBal.Rows[e.RowIndex].FindControl("txtRemark") as TextBox;

        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
        //SqlCommand cmd = new SqlCommand("Insp_DO_Form_Entry_Update", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //con.Open();
        //cmd.Parameters.AddWithValue("@ID", hdnid.Value);
        //cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
        //cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
        //cmd.Parameters.AddWithValue("@DO_form_No", txt_DO_form_No.Text);
        //cmd.Parameters.AddWithValue("@Date_of_DO_form", getDate_MDY(txt_DO_Date.Text));
        //cmd.Parameters.AddWithValue("@Commodity_ID", hdncommodityid.Value);       
        //cmd.Parameters.AddWithValue("@Signature_of_depositor", ddlsgndepositer.SelectedValue);
        //cmd.Parameters.AddWithValue("@Signature_of_BM", ddlBS.SelectedValue);
        //cmd.Parameters.AddWithValue("@Deposit_Gate_Pass", ddlgatrpass.SelectedValue);
        //cmd.Parameters.AddWithValue("@Kata_Parchi", ddltollslip.SelectedValue);
        //cmd.Parameters.AddWithValue("@Truck_Chalan", ddltruckparchi.SelectedValue);
        //cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
        //cmd.Parameters.AddWithValue("@Createt_By", ipAddress);
        //cmd.Parameters.AddWithValue("@Cancil_WHR", ddlcancilWHR.SelectedValue);
        //cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        //cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        //cmd.ExecuteNonQuery();
        //string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

        //if (TheResult.StartsWith("SUCCESS"))
        //{
        //    string strMsg = "DO Details Update Successfully submitted |||";
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
        //    //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        //    GD_StackBal.EditIndex = -1;
        //    //Call ShowData method for displaying updated data  
        //    GetdataForGrid();
        //}
        
    }
    protected void GD_StackBal_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        GD_StackBal.EditIndex = -1;
        GetdataForGrid();
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {

    }
}