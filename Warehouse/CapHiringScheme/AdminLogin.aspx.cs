using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class AdminLogin : System.Web.UI.Page
{
    DataTable dt;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            

        }
    }

    protected void lbtnState_Click(object sender, EventArgs e)
    {
        litHeading.Text = "State Login";

        panelUserLevel.Visible = false;
        panelLogin.Visible = true;
        hdnLevel.Value = "HO";

    }

    protected void lbtnRegion_Click(object sender, EventArgs e)
    {
        GetRegion();

        litHeading.Text = "Region Login";

        panelUserLevel.Visible = false;
        panelLogin.Visible = true;

        divRegion.Visible = true;
        hdnLevel.Value = "RO";

    }

    //protected void lbtnDistrict_Click(object sender, EventArgs e)
    //{
    //    litHeading.Text = "District Login";

    //    panelUserLevel.Visible = false;
    //    panelLogin.Visible = true;

    //    divDistrict.Visible = true;
       

    //}

    protected void lbtnBranch_Click(object sender, EventArgs e)
    {
        GetDist("1");

        litHeading.Text = "Branch Login";

        panelUserLevel.Visible = false;
        panelLogin.Visible = true;

        divDistrict.Visible = true;
        divBranch.Visible = true;

        hdnLevel.Value = "BO";

       
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }



    protected void btnLogin_Click(object sender, EventArgs e)
    {
        DoDepotLogin();
    }






    //Get Region
    private void GetRegion()
    {
        string qr = "SELECT region,Region_Id FROM tbl_MetaData_Region order by region";
        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);

            if (dt.Rows.Count > 0)
            {

                ddlRegion.DataSource = dt;
                ddlRegion.DataTextField = "region";
                ddlRegion.DataValueField = "Region_Id";
                ddlRegion.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    // Get Dist
    private void GetDist(string Scope)
    {
        
            string srvr = System.Configuration.ConfigurationManager.AppSettings["HostedServer"].ToString();
            if (Scope == "1")
            {
                string qr = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";

                try
                {
                    dt = new Sqldatalayer().SelectData("mycon", qr, null, false);

                    if (dt.Rows.Count > 0)
                    {

                        ddlDistrict.DataSource = dt;
                        ddlDistrict.DataTextField = "District_Name";
                        ddlDistrict.DataValueField = "District_Id";
                        ddlDistrict.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
                }
                finally { }
            }

      
    }

    // Get Branch
    private void GetBranch()
    {
        string DistrictId = ddlDistrict.SelectedValue.ToString();

        string qr = "SELECT mbi.BranchID,mbi.BranchName FROM MetaDataBranchWithIssueCenter as mbi  WHERE mbi.[DistrictId] = '" + DistrictId.ToString() + "' and  mbi.BranchTypeID not in ('2','3','4','5','6','7','8','9','10') order by mbi.BranchName";
        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);

            if (dt.Rows.Count > 0)
            {

                ddlBranch.DataSource = dt;
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchID";
                ddlBranch.DataBind();
            }

            else
            {
                ddlBranch.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
        finally { }
    }

    // Do Login
    private void DoDepotLogin()
    {
        
            string Uname = "";
            string pwd = "";
            string Scope = "";

            if (hdnLevel.Value == "HO")
            {
                //Uname = "HOMPWLC";
                Uname = "100";
                pwd = txtPassword.Text;
                Scope = "H";
            }

            else if (hdnLevel.Value == "RO")
            {
                Uname = ddlRegion.SelectedValue.ToString();
                pwd = txtPassword.Text;
                Scope = "R";
            }

            else if (hdnLevel.Value == "BO")
            {
                Uname = ddlBranch.SelectedValue.ToString();
                pwd = txtPassword.Text;
                Scope = "B";
            }


            try
            {
            if (Scope == "H" || Scope == "R")
            {
                string qr = "SELECT [UserId],[UserName],[Password],[MasterPass],[Scope] FROM [JVS_Login] where UserId='" + Uname + "' and SCOPE='" + Scope + "'";
            
                    dt = new Sqldatalayer().SelectData("mycon", qr, null, false);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        Session["login"] = "true";
                        Session["UserId"] = dr["UserId"].ToString();
                        Session["UserName"] = dr["UserName"].ToString();
                        Session["Scope"] = dr["Scope"].ToString();
                        if (Uname == dr["UserId"].ToString() && (pwd == dr["Password"].ToString() || pwd == dr["MasterPass"].ToString()))
                        {
                            if (Scope == "H" || Scope == "R")
                            {
                                Response.Redirect("AdminDashboard/Default.aspx",false);
                            }

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

                else if (Scope == "B")
                {
                    //string strsql = "SELECT [UserId],[UserName],[Password],[MasterPass],[Scope] FROM [JVS_Login] where UserId='" + Uname + "' and SCOPE='" + Scope + "'";
                    string qr = "SELECT BranchID,BranchName,BranchPwd as Password FROM MetaDataBranchWithIssueCenter where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and BranchName='" + ddlBranch.SelectedItem.ToString() + "'";

                    dt = new Sqldatalayer().SelectData("mycon", qr, null, false);


                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                    
                        Session["login"] = "true";
                        Session["UserId"] = dr["BranchID"].ToString();
                        Session["UserName"] = dr["BranchName"].ToString();
                        Session["DistID"] = ddlDistrict.SelectedValue.ToString();
                        Session["Scope"] = "B";

                        if (Uname == dr["BranchID"].ToString() && (pwd == dr["Password"].ToString()))
                        {
                            Session["Agency_ID"] = '1';
                            Response.Redirect("AdminDashboard/Default.aspx", false);
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



   
