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

public partial class Inspections_BO_FCI_Inspection_Report : System.Web.UI.Page
{
    public SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string PFID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        PFID = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            GetdataForGrid();
        }

    }
    public void GetdataForGrid()
    {
        SqlCommand cmd = new SqlCommand("[dbo].[Get_Godown_Details_For_FCI_Inspection]", con2);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BranchID", PFID.ToString());
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

    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        //SqlTransaction tn = null;
        //foreach (GridViewRow row in GD_StackBal.Rows)
        //{
        //    if (row.RowType == DataControlRowType.DataRow)
        //    {
        //        TextBox GtxtdepositformNo = row.FindControl("GtxtdepositformNo") as TextBox;
        //        TextBox Gtxtdepositformdate = row.FindControl("Gtxtdepositformdate") as TextBox;
        //        Label lblWhr_No = row.FindControl("lblWhr_No") as Label;
        //        Label lblCreatedDate = row.FindControl("lblCreatedDate") as Label;
        //        HiddenField hdnCommodity_Id = row.FindControl("hdnCommodity_Id") as HiddenField;
        //        Label lblAvlBags = row.FindControl("lblAvlBags") as Label;
        //        Label lblAvlQty = row.FindControl("lblAvlQty") as Label;
        //        Label lblMktValue_of_Commodity = row.FindControl("lblMktValue_of_Commodity") as Label;
        //        //Label lblPrice = row.FindControl("lblPrice") as Label;

        //        DropDownList ddlgrade = row.FindControl("ddlgrade") as DropDownList;
        //        DropDownList ddlsgndepositer = row.FindControl("ddlsgndepositer") as DropDownList;
        //        DropDownList ddlBS = row.FindControl("ddlBS") as DropDownList;
        //        DropDownList ddlgatrpass = row.FindControl("ddlgatrpass") as DropDownList;
        //        DropDownList ddltollslip = row.FindControl("ddltollslip") as DropDownList;
        //        DropDownList ddltruckparchi = row.FindControl("ddltruckparchi") as DropDownList;
        //        TextBox GtxtRemark = row.FindControl("GtxtRemark") as TextBox;

        //        string ipAddress;
        //        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //        if (ipAddress == "" || ipAddress == null)
        //            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        //        try
        //        {

        //            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
        //            SqlCommand cmd = new SqlCommand("Insp_Depositer_Form_Entry_Insert", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            con.Open();
        //            tn = con.BeginTransaction();
        //            cmd.Transaction = tn;
        //            //cmd.Parameters.AddWithValue("@ID", hdnid.Value);
        //            cmd.Parameters.AddWithValue("@Emp_ID", PFID);
        //            cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
        //            cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
        //            cmd.Parameters.AddWithValue("@Inspection_Date", getDate_MDY(txt_inspdate.Text));
        //            cmd.Parameters.AddWithValue("@deposit_form_No", GtxtdepositformNo.Text);
        //            cmd.Parameters.AddWithValue("@Date_of_deposit_form", getDate_MDY(Gtxtdepositformdate.Text));
        //            cmd.Parameters.AddWithValue("@WHR_No", lblWhr_No.Text);
        //            cmd.Parameters.AddWithValue("@WHR_Date", getDate_MDY(lblCreatedDate.Text));
        //            cmd.Parameters.AddWithValue("@Commodity_ID", hdnCommodity_Id.Value);
        //            cmd.Parameters.AddWithValue("@No_of_Bags", lblAvlBags.Text);
        //            cmd.Parameters.AddWithValue("@Quantity", lblAvlQty.Text);
        //            cmd.Parameters.AddWithValue("@Mkt_value", lblMktValue_of_Commodity.Text);
        //            cmd.Parameters.AddWithValue("@Price", 0);
        //            cmd.Parameters.AddWithValue("@Grade", ddlgrade.SelectedValue);
        //            cmd.Parameters.AddWithValue("@Signature_of_depositor", ddlsgndepositer.SelectedValue);
        //            cmd.Parameters.AddWithValue("@Signature_of_BM", ddlBS.SelectedValue);
        //            cmd.Parameters.AddWithValue("@Deposit_Gate_Pass", ddlgatrpass.SelectedValue);
        //            cmd.Parameters.AddWithValue("@Kata_Parchi", ddltollslip.SelectedValue);
        //            cmd.Parameters.AddWithValue("@Truck_Chalan", ddltruckparchi.SelectedValue);
        //            cmd.Parameters.AddWithValue("@Remark", GtxtRemark.Text);
        //            cmd.Parameters.AddWithValue("@Createt_By", ipAddress);
        //            cmd.Parameters.AddWithValue("@Insp_Date", getDate_MDY(Session["Order_Date"].ToString()));
        //            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        //            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        //            cmd.ExecuteNonQuery();
        //            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

        //            if (TheResult.StartsWith("SUCCESS"))
        //            {
        //                if (!string.IsNullOrEmpty(GtxtdepositformNo.Text))
        //                {
        //                    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
        //                    SqlCommand cmd2 = new SqlCommand("Tbl_JVS_Inspection_Entry_Insert", con2);
        //                    cmd2.CommandType = CommandType.StoredProcedure;
        //                    con2.Open();
        //                    //cmd.Parameters.AddWithValue("@ID", hdnid.Value);
        //                    cmd2.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
        //                    cmd2.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
        //                    cmd2.Parameters.AddWithValue("@WHR_ID", lblWhr_No.Text);
        //                    cmd2.Parameters.AddWithValue("@Inspection_ID", hdninspectionid.Value);
        //                    cmd2.Parameters.AddWithValue("@Quater", hdnquater.Value);
        //                    cmd2.Parameters.AddWithValue("@Inspection_Month", hdnmonth.Value);
        //                    cmd2.Parameters.AddWithValue("@Inspection_Date", getDate_MDY(txt_inspdate.Text));
        //                    cmd2.Parameters.AddWithValue("@Insert_By", ipAddress);
        //                    cmd2.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        //                    cmd2.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        //                    cmd2.ExecuteNonQuery();
        //                    TheResult = cmd.Parameters["@TheResult"].Value.ToString();

        //                    if (TheResult.StartsWith("SUCCESS"))
        //                    {
        //                        tn.Commit();
        //                        string strMsg = "Inspection Details Successfully submitted |||";
        //                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
        //                        GetdataForGrid();
        //                    }
        //                    con2.Close();
        //                }
        //            }
        //            else
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
        //            }
        //            con.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            tn.Rollback();
        //            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
        //            Console.WriteLine(ex.Message);
        //        }
        //    }
        //}
    }
    protected void GD_StackBal_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}