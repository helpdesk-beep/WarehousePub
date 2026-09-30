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

public partial class Inspections_Truti_Anupalan_Prativedan_Opinion_BY_IO : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string lblinspid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
            //cmd.Parameters.AddWithValue("@Inspection_ID", Session["hdnInspection_ID"].ToString());
            // cmd.Parameters.AddWithValue("@godownID", Session["Godown_ID"].ToString());
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

    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Truti_Patrak_Opinion_By_IO", con))
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
                            inspid.Value = dt.Rows[0]["Inspection_ID"].ToString();
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
        TextBox txtIMC = (TextBox)GrdInsp.Rows[e.RowIndex].FindControl("txtIMC");
        string ID = CIT_ID.Value;       
        string IOOpinion = txtIMC.Text.ToString();
        Updateemployee(ID, IOOpinion);
    }
    protected void Updateemployee(string ID, string IOOpinion)
    {

        string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        using (SqlConnection constr = new SqlConnection(CS))
        {
            SqlCommand cmd = new SqlCommand("Insp_Truti_Patrak_IO_Opinion_Entry", constr);
            cmd.CommandType = CommandType.StoredProcedure;
            constr.Open();
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
            cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
            cmd.Parameters.AddWithValue("@QuaterType", Session["hdnquatertype"].ToString());
            cmd.Parameters.AddWithValue("@VerificationType", Session["hdnVerificationType"].ToString());
            cmd.Parameters.AddWithValue("@FinacialYear", Session["hdnfinancialyear"].ToString());
            cmd.Parameters.AddWithValue("@Inspection_officer_opinion", IOOpinion);
            cmd.Parameters.AddWithValue("@BM_Inserted_BY", IPAddress);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "BM Entry Successfully submitted|||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                fillScheduleInsp_Grid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
            }
        }
    }
    protected void btnfinalsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Insp_Truti_Patrak_Final_Subim_to_RM_by_IO", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                //cmd.Parameters.AddWithValue("@Inspection_ID", inspid.Value);
                //cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
                cmd.Parameters.AddWithValue("@QuaterType", Session["hdnquatertype"].ToString());
                cmd.Parameters.AddWithValue("@VerificationType", Session["hdnVerificationType"].ToString());
                cmd.Parameters.AddWithValue("@FinacialYear", Session["hdnfinancialyear"].ToString());
                cmd.Parameters.AddWithValue("@IOO_Final_Submit_to_RM", 1);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Final Submitted RM Office";
                   // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/Warehouse/Inspections/Inspection_Officer/Branch_Wise_Truti_Patrak_Submit_By_BM.aspx';", true);
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

    //protected void Updateemployee(string id, string UID, string LEDPC, string LEDA, string LRDDPC, string LRDDA)
    //{

    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString);
    //    con.Open();
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand("[dbo].[Update_Exam_and_Result_Date]", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar, 10));
    //        cmd.Parameters.Add(new SqlParameter("@Univertsity_ID", SqlDbType.VarChar, 50));
    //        cmd.Parameters.Add(new SqlParameter("@LEDPC", SqlDbType.VarChar, 50));
    //        cmd.Parameters.Add(new SqlParameter("@LEDA", SqlDbType.NVarChar, 50));
    //        cmd.Parameters.Add(new SqlParameter("@LRDDPC", SqlDbType.VarChar, 20));
    //        cmd.Parameters.Add(new SqlParameter("@LRDDA", SqlDbType.VarChar, 20));
    //        cmd.Parameters.Add(new SqlParameter("@TheResult", SqlDbType.VarChar, 250));
    //        cmd.Parameters["@ID"].Value = id;
    //        cmd.Parameters["@Univertsity_ID"].Value = UID;
    //        cmd.Parameters["@LEDPC"].Value = LEDPC;
    //        cmd.Parameters["@LEDA"].Value = LEDA;
    //        cmd.Parameters["@LRDDPC"].Value = LRDDPC;
    //        cmd.Parameters["@LRDDA"].Value = LRDDA;
    //        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
    //        cmd.ExecuteNonQuery();
    //        string result = cmd.Parameters["@TheResult"].Value.ToString();
    //        if (result.Contains("SUCCESS"))
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Update Your Date Succesfully')", true);
    //            fillgrid();
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('पुनः प्रयास करे')", true);
    //        }
    //        con.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.Message);
    //    }
    //}

}