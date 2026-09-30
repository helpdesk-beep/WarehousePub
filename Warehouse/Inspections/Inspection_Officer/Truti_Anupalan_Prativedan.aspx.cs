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

public partial class Inspections_Inspection_Officer_Truti_Anupalan_Prativedan : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string lblinspid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            
            lblinspid = Session["hdnInspection_ID"].ToString();
            FatchInspData();
            fillScheduleInsp_Grid();
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
                lblregionname.Text = dt.Rows[0]["Regionnm"].ToString().Trim();
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
    public void checkvalidation()
    {
        if (txttruti.Text == "" || txttruti.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter!....')", true);
            txttruti.Focus();
            return;
        }        

    }
   
    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Truti_Anupalan_Prativedan_Details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Inspection_ID", Session["hdnInspection_ID"].ToString());
                //cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
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

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            checkvalidation();
            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Insp_Truti_Anupalan_Prativedan_By_IO_to_BM", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@Inspection_ID", Session["hdnInspection_ID"].ToString());
                cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@Error_Details_By_IO", txttruti.Text);
                cmd.Parameters.AddWithValue("@EIO_Inserted_BY", IPAddress);
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@QuaterType", Session["hdnquatertype"].ToString());
                cmd.Parameters.AddWithValue("@VerificationType", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@FinacialYear", Session["hdnfinancialyear"].ToString());
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Details Successfully submitted|||";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    txttruti.Text = "";
                    fillScheduleInsp_Grid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
   
    protected void btnfinalsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Insp_Truti_Patrak_Final_Subim_to_BM", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@Inspection_ID", Session["hdnInspection_ID"].ToString());
                cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@EIO_Final_Submit_to_BM", 1);
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@QuaterType", Session["hdnquatertype"].ToString());
                cmd.Parameters.AddWithValue("@VerificationType", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@FinacialYear", Session["hdnfinancialyear"].ToString());
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "क्या आप फाइनल  सबमिट करना चाहते हैं ? फाइनल सबमिट  करने के पश्चात यहाँ त्रुटि पत्रक आपकी स्क्रीन से हट जायेगा | ";
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Inspections/Inspection_Officer/Branch_Wise_Truti_Patrak.aspx';", true);
                    fillScheduleInsp_Grid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
}