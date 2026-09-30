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


public partial class EncryptPassword : System.Web.UI.Page
{
    SqlConnection _sqlCon = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string str = "SELECT [User_Name],convert(varchar(20),[login_id])+'nicF$9' as 'pwd',[login_id]  FROM [Storage_Login] ";
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
        }
    }
    protected void btnEncrypt_Click(object sender, EventArgs e)
    {
        //for (int i = 0; i < gvPwdEN.Rows.Count; i++)
        //{
        //    string pwd = gvPwdEN.Rows[i].Cells[2].Text;
        //    //string ep = "MD5('" + pwd + "'";
        //    //string str = "<script language='javascript'>MD5(pwd);</Script>";
        //    //RegisterStartupScript("str", str);


        //    StringBuilder str = new StringBuilder();
        //    str.Append("<script>");
        //    str.Append("MD5('" + pwd.ToString() + "');</script>");
        //    this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        //}
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        
            try
            {
                for (int i = 0; i < gvPwdEN.Rows.Count; i++)
                {
                    //Response.Write(((TextBox)gvPwdEN.Rows[1].FindControl("txtEncrypted")).Text);
                    //Response.Write("  -  ");
                    //Response.Write(((TextBox)gvPwdEN.Rows[1].FindControl("txtUID")).Text);
                    //Response.Write("  -  ");
                    //Response.Write(((TextBox)gvPwdEN.Rows[1].FindControl("txtMEncrypted")).Text);
                    if (_sqlCon.State == ConnectionState.Closed)
                        _sqlCon.Open();

                    string str_upadte = "update Storage_Login set Password=convert(varbinary(300),'" + ((TextBox)gvPwdEN.Rows[i].FindControl("txtEncrypted")).Text.Trim() + "') ,MasterPassword=convert(varbinary(300),'" + ((TextBox)gvPwdEN.Rows[i].FindControl("txtMEncrypted")).Text.Trim() + "') where login_id='" + ((TextBox)gvPwdEN.Rows[i].FindControl("txtUID")).Text.Trim() + "'";

                    SqlCommand cmd = new SqlCommand(str_upadte, _sqlCon);
                    int ax = cmd.ExecuteNonQuery();
                    

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
}
