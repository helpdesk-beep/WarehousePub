using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JointVentureScheme_OtherGovLogin : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAgency();
        }
    }

    private void GetAgency()
    {
        try
        {
            string str = "select Agency_Name,SAID from [tbl_Storage_Agency]  where Remark='O' order by Agency_Name";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Agency.DataSource = ds.Tables[0];
                DDL_Agency.DataTextField = "Agency_Name";
                DDL_Agency.DataValueField = "SAID";
                DDL_Agency.DataBind();
                DDL_Agency.Items.Insert(0, "--Select--");
            }
            else
            {
                DDL_Agency.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        DoDepotLogin();
    }
    private void DoDepotLogin()
    {
        try
        {
            string Uname = "";
            string pwd = "";
            string Scope = "";
            if (DDL_Agency.SelectedItem.Text != "--Select--")
            {
                if (DDL_Agency.SelectedItem.Text == "Cooperative Socities" || DDL_Agency.SelectedItem.Text == "CWC" || DDL_Agency.SelectedItem.Text == "FCI" || DDL_Agency.SelectedItem.Text == "MANDI" || DDL_Agency.SelectedItem.Text == "MARKFED" || DDL_Agency.SelectedItem.Text == "MFPFED" || DDL_Agency.SelectedItem.Text == "NAFED" || DDL_Agency.SelectedItem.Text == "OILFED")
                {
                    Scope = "O";
                }
            }
            if (Scope == "O")
            {
                string strsql = "SELECT [Agency_ID],[UserName],[Password] as Password FROM [JVS_Login] where [Agency_ID]='" + DDL_Agency.SelectedValue.ToString() + "' and [UserName]='" + DDL_Agency.SelectedItem.ToString() + "' ";
                SqlDataAdapter da = new SqlDataAdapter(strsql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    Session["login"] = "true";
                    Session["Agency_ID"] = dr["Agency_ID"].ToString();
                    Session["UserName"] = dr["UserName"].ToString();
                    Session["Scope"] = "O";
                    if (DDL_Agency.SelectedValue == dr["Agency_ID"].ToString() && (txtlogpwd.Value == dr["Password"].ToString()))
                    {
                        Response.Redirect("OtherLoginWelcome.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका पासवर्ड गलत हो सकता है')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका यूसर नेम/पासवर्ड गलत हो सकता है')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter the correct Password')", true);
        }
    }
}
