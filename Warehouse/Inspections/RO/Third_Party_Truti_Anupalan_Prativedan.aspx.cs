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

public partial class Inspections_RO_Third_Party_Truti_Anupalan_Prativedan : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string lblinspid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //lblinspid = Session["hdnTAPID"].ToString();
            FatchInspData();
            fillScheduleInsp_Grid();
            CheckFinalSubmit();
        }
    }
    protected void CheckFinalSubmit()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Check_TrutiPatrak_Final_Submit_For_HOMPWLC", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@InspectionTypeID", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@QuaterID", Session["hdnquatertype"].ToString());
                cmd.Parameters.AddWithValue("@FinancialYear", Session["hdnfinancialyear"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            txtremrktqro.Text = dt.Rows[0]["Remrak_by_TQRO"].ToString();
                            txtremarkTQHO.Text = dt.Rows[0]["Remrak_by_TQHO"].ToString();
                            //string strMsg = "यहाँ Truti patrak आपके द्वारा पहले ही HO MPWLC को Final Submit किया जा चुका है |||";
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        }
                    }
                }
            }
        }
    }
    public void FatchInspData()
    {
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();

            }
            SqlCommand cmd = new SqlCommand("Get_Truti_Patrak_Details", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
            cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
            cmd.Parameters.AddWithValue("@QuaterType", Session["hdnquatertype"].ToString());
            cmd.Parameters.AddWithValue("@VerificationType", Session["hdnVerificationType"].ToString());
            cmd.Parameters.AddWithValue("@FinacialYear", Session["hdnfinancialyear"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { conStr.Close(); }

            if (dt.Rows.Count > 0)
            {
                // lblregionname.Text = dt.Rows[0]["Regionnm"].ToString().Trim();
                lblinspectionofficername.Text = dt.Rows[0]["Officer_Name"].ToString().Trim();
                lblavdhi.Text = dt.Rows[0]["Observation_Date_From_to"].ToString().Trim();
                lblbranch.Text = dt.Rows[0]["Depotname"].ToString().Trim();
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('............!')", true);
        }
        finally
        { if (con.State == ConnectionState.Open) { con.Close(); } }
    }

    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (
            SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Truti_Patrak_For_RM_Submit_by_IO", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@QuaterType", Session["hdnquatertype"].ToString());
                cmd.Parameters.AddWithValue("@VerificationType", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@FinacialYear", Session["hdnfinancialyear"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GrdInsp.DataSource = dt;
                            GrdInsp.DataBind();
                            grd.Visible = true;
                        }
                        else
                        {
                            grd.Visible = false;
                            GrdInsp.DataSource = null;
                            GrdInsp.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void GrdInsp_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        HiddenField CIT_ID = (HiddenField)GrdInsp.Rows[e.RowIndex].FindControl("CIT_ID");
        TextBox txtRM = (TextBox)GrdInsp.Rows[e.RowIndex].FindControl("txtRM");
        string ID = CIT_ID.Value;
        string submittoho = txtRM.Text.ToString();
        //Updateemployee(ID, submittoho);
    }
    //protected void Updateemployee(string ID, string submittoho)
    //{

    //    string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    //    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
    //    using (SqlConnection constr = new SqlConnection(CS))
    //    {
    //        SqlCommand cmd = new SqlCommand("Insp_Truti_Patrak_submit_to_HO_by_RO", constr);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        constr.Open();
    //        cmd.Parameters.AddWithValue("@ID", ID);
    //        cmd.Parameters.AddWithValue("@RO_Opinion", submittoho);
    //        cmd.Parameters.AddWithValue("@BM_Inserted_BY", IPAddress);
    //        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
    //        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
    //        cmd.ExecuteNonQuery();
    //        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

    //        if (TheResult.StartsWith("SUCCESS"))
    //        {
    //            string strMsg = "RO Entry Successfully submitted|||";
    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
    //            fillScheduleInsp_Grid();
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
    //        }
    //    }
    //}
    //protected void btnfinalsubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    //        using (SqlConnection constr = new SqlConnection(CS))
    //        {
    //            SqlCommand cmd = new SqlCommand("Insp_Truti_Patrak_Final_Subim_to_HO", constr);
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            constr.Open();
    //            cmd.Parameters.AddWithValue("@Inspection_ID", Session["hdnInspection_ID"].ToString());
    //            cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
    //            cmd.Parameters.AddWithValue("@RM_Final_Submit_to_HO", 1);
    //            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
    //            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
    //            cmd.ExecuteNonQuery();
    //            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

    //            if (TheResult.StartsWith("SUCCESS"))
    //            {
    //                string strMsg = "क्या आप फाइनल  सबमिट करना चाहते हैं ? फाइनल सबमिट  करने के पश्चात यहाँ त्रुटि पत्रक आपकी स्क्रीन से हट जायेगा | ";
    //                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Inspections/RO/Branch_Wise_Truti_Patrak.aspx';", true);
    //                fillScheduleInsp_Grid();
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string strMsg2 = ex.Message;
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
    //    }
    //}
}