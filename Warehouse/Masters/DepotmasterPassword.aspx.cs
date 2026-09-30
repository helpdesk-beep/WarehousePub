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
using System.Text;
public partial class Masters_DepotmasterPassword : System.Web.UI.Page
{
    SqlConnection _sqlCon = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());

    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"] != null)
        {
            string user = Session["UserName"].ToString();
            if (user != "MPSWLC")
            {
                Response.Redirect("../login.aspx");
            }
        }
        else 
        {

            Response.Redirect("../login.aspx");
        
        }

        if (!IsPostBack)
        {

            fillpassgrid();
        }
    }

    private void fillpassgrid()
    {
        if (Session["UserName"] != null)
        {
            string user = Session["UserName"].ToString();
            if (user == "MPSWLC")
            {
                string str = "SELECT [User_Name],convert(varchar(20),[login_id])+'nicF$9' as 'pwd',[login_id]  FROM [Storage_Login] where MasterPassword is null";
                SqlDataAdapter da = new SqlDataAdapter(str, _sqlCon);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        gvPwdEN.DataSource = dt;
                        gvPwdEN.DataBind();
                    }

                }

                else
                {
                    gvPwdEN.DataSource = null;
                    gvPwdEN.DataBind();
                    errmsg.Text = "No pending Branch Found";
                    btnUpdate.Visible = false;

                }

            }

        }
    }
  
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        try
        {
            for (int i = 0; i < gvPwdEN.Rows.Count; i++)
            {
              
                if (_sqlCon.State == ConnectionState.Closed)
                    _sqlCon.Open();

             string strchk=  ((HiddenField)gvPwdEN.Rows[i].FindControl("txtMEncrypted")).Value.ToString();
             string strchkId = ((HiddenField)gvPwdEN.Rows[i].FindControl("txtUID")).Value.ToString();

             if (strchkId != null && strchkId != "")
                {

                    if (strchk != null && strchk != "")
                    {
                        string str_upadte = "update Storage_Login set Password=convert(varbinary(300),'" + ((HiddenField)gvPwdEN.Rows[i].FindControl("txtEncrypted")).Value.Trim() + "') ,MasterPassword=convert(varbinary(300),'" + ((HiddenField)gvPwdEN.Rows[i].FindControl("txtMEncrypted")).Value.Trim() + "') where login_id='" + ((HiddenField)gvPwdEN.Rows[i].FindControl("txtUID")).Value.Trim() + "'";

                        SqlCommand cmd = new SqlCommand(str_upadte, _sqlCon);
                        int ax = cmd.ExecuteNonQuery();
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Password is Saved...'); </script> ");
                        fillpassgrid();
                    }

                    else {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('First Craete Password For Branch'); </script> ");
                    
                    }
                }
            }

        }

        catch (Exception esa)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "There are some error occured" + esa.Message.ToString() + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());

        }
    }
    protected void gvPwdEN_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void linkbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("DepotMaster.aspx");
    }
}
