using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Diagnostics;
using System.Resources;

public partial class Inspections_State_Update_Godown_2018_To_Godown_2022 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() != null)
        {

        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            string CS = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Update_CropYear_2022_23", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Update Crop Year 2022-23 Successfully |||";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
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

    protected void btnUpdateBlanckCropYear_Click(object sender, EventArgs e)
    {
        try
        {
            string CS = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Update_Null_CropYear_to_2022_23", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Update Null Crop Year To 2022-23 Successfully |||";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string str19 = "Insert Into tbl_Institution_Storage_Bill_Details_Log SELECT * FROM tbl_Institution_Storage_Bill_Details WHERE YEAR(From_Date) = '2023' and MONTH(From_Date)>= 4 AND Financial_Year=''";
        cmd = new SqlCommand(str19, con);
        int req19 = cmd.ExecuteNonQuery();

        string str20 = "Update tbl_Institution_Storage_Bill_Details set Financial_Year='2023-2024' WHERE YEAR (From_Date)= '2023' and MONTH(From_Date)>= 4 AND Financial_Year=''";
        cmd = new SqlCommand(str20, con);
        int req20 = cmd.ExecuteNonQuery();
        if (req20 > 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Successfully Financial Year'); </script> ");
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Financial Year Already Updatetd'); </script> ");
        }
        con.Close();
    }

    protected void btnAddCompany_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string str19 = "Insert into tbl_Warehousing_Contact (Branch_Id,Branch_Name,Mobile_No) values('"+txtBranchID.Text+"','"+txtbranchname.Text.ToString()+"','"+txtmobileno.Text.ToString()+"')";
        cmd = new SqlCommand(str19, con);
        int req19 = cmd.ExecuteNonQuery();
      
        if (req19 > 0)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Successfully Branch Details'); </script> ");
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Financial Year Already Updatetd'); </script> ");
        }
        con.Close();
    }
}
