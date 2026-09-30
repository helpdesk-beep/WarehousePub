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
using System.Web.SessionState;
using System.Data.SqlClient;
public partial class Default2 : System.Web.UI.Page
{
    SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
              
        
            if (!IsPostBack)
            {
               
              UpdateLoggedin_log();
                   
            }
            Session.Abandon();
            Response.Redirect("login.aspx");
           
        }
        catch (Exception ex)
        {

            Response.Redirect("login.aspx");
          
        }
        finally
        {
          
            Session.Abandon();
            Response.Redirect("login.aspx");
        }
     
    }
    protected void UpdateLoggedin_log()
    {
        try
        {
            string RoleID = "";
            string Logid = "";
            RoleID = Session["RoleId"].ToString();
            if (RoleID == "1")
            {

                Logid = Session["Depot_Logid"].ToString();

            }
            else if (RoleID == "2")
            {

                Logid = Session["Region_Logid"].ToString();

            }
            else if (RoleID == "3")
            {

                Logid = Session["State_Logid"].ToString();

            }
            else
            {

                //

            }
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            SqlCommand sqlCmd = new SqlCommand("sp_update_LoggedIn_Log", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@Login_ID", SqlDbType.Int);
            sqlCmd.Parameters["@Login_ID"].Value = Logid.ToString();
            sqlCmd.Parameters.Add("@Client_IP", SqlDbType.NVarChar, 20);
            sqlCmd.Parameters["@Client_IP"].Value = Request.ServerVariables["REMOTE_ADDR"].ToString();
            int rs = sqlCmd.ExecuteNonQuery();
            if (rs == 1)
            {
              
                Session.Abandon();

               
            }
            else
            {
                
                Session.Abandon();

                throw new Exception("Invalid user operation or session ended, Please login again!");
            }
            sqlCmd.Dispose();
            sqlCon.Close();
        }
        catch (Exception ex)
        {
            Session["errDesc"] = ex.Message;
            Server.Transfer("CustomError.aspx");
        }
        finally
        {
            sqlCon.Close();
        }
    }
  
}
