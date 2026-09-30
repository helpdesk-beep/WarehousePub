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
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
public partial class StatePages_UpdateBranchIssuecenter : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            fillDistrict();
            fillDistrict2();
            fillIssuecenter();
            FillBranchList();
            fillDistrict3();
          
        }
    }

    private void fillDistrict()
    {
        try
        {
            string region = "";
           

            string query = "";
            if (Session["UserName"].ToString() == "Admin")
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            }
            else
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
            }
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();


            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void fillDistrict2()
    {
        try
        {
            string region = "";


            string query = "";
            if (Session["UserName"].ToString() == "Admin")
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            }
            else
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
            }
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict2.Items.Clear();
                ddlDistrict2.DataSource = ds.Tables[0];
                ddlDistrict2.DataTextField = "District_Name";
                ddlDistrict2.DataValueField = "District_Id";
                ddlDistrict2.DataBind();


            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void fillDistrict3()
    {
        try
        {
            string region = "";


            string query = "";
            if (Session["UserName"].ToString() == "Admin")
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            }
            else
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
            }
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict3.Items.Clear();
                ddlDistrict3.DataSource = ds.Tables[0];
                ddlDistrict3.DataTextField = "District_Name";
                ddlDistrict3.DataValueField = "District_Id";
                ddlDistrict3.DataBind();


            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void fillIssuecenter()
    {
        try
        {
          

            string query = "";
            if (Session["UserName"].ToString() == "Admin")
            {
                query = "SELECT DepotID,DistrictId,DepotName FROM [mpscsc].[dbo].[tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "'";
                //query = "SELECT DepotID,DistrictId,DepotName FROM [mpscsc].[dbo].[tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "'";
            }
           
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlissuecenter.Items.Clear();
                ddlissuecenter.DataSource = ds.Tables[0];
                ddlissuecenter.DataTextField = "DepotName";
                ddlissuecenter.DataValueField = "DepotID";
                ddlissuecenter.DataBind();


            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    private void FillBranchList()
    {
        try
        {
        
            string query = "";
            if (Session["UserName"].ToString() == "Admin")
            {
                query = "select * from  dbo.MetaDataBranchWithIssueCenter where DistrictId='" + ddlDistrict.SelectedValue + "'";
            }

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlbranch.Items.Clear();
                ddlbranch.DataSource = ds.Tables[0];
                ddlbranch.DataTextField = "BranchName";
                ddlbranch.DataValueField = "BranchID";
                ddlbranch.DataBind();


            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    private void FillBranchList2()
    {
        try
        {

            string query = "";
            if (Session["UserName"].ToString() == "Admin")
            {
                query = "select * from  dbo.MetaDataBranchWithIssueCenter where DistrictId='" + ddlDistrict2.SelectedValue + "'";
            }

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlBranch2.Items.Clear();
                ddlBranch2.DataSource = ds.Tables[0];
                ddlBranch2.DataTextField = "BranchName";
                ddlBranch2.DataValueField = "BranchID";
                ddlBranch2.DataBind();


            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void FillBranchList3()
    {
        try
        {

            string query = "";
            if (Session["UserName"].ToString() == "Admin")
            {
                query = "select * from  dbo.MetaDataBranchWithIssueCenter where DistrictId='" + ddlDistrict3.SelectedValue + "'";
            }

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlBranch3.Items.Clear();
                ddlBranch3.DataSource = ds.Tables[0];
                ddlBranch3.DataTextField = "BranchName";
                ddlBranch3.DataValueField = "BranchID";
                ddlBranch3.DataBind();


            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //if (con.State == ConnectionState.Closed)
        //{
        //    con.Open();
        //}
        try
        {
            string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
            qry = " insert into [Storage_Login_Log] select [login_id],[User_Name],[Password],[DepotId] ,[DistrictId],[Fname],[Lname],[Scope],[MasterPassword],[Access_Restrict],'" + ClientIP + "',GETDATE(),[BranchID] FROM [Intergrated_MP_STORAGE].[dbo].[Storage_Login] where  BranchID='" + ddlbranch.SelectedValue.ToString() + "'";
            cmd = new SqlCommand(qry, con);
            con.Open();
            int x1 = cmd.ExecuteNonQuery();
            con.Close();
            if (x1 > 0)
            {
                qry = " update [Storage_Login] set DepotId='" + ddlissuecenter.SelectedValue.ToString() + "' where BranchID='" + ddlbranch.SelectedValue.ToString() + "'";
                cmd = new SqlCommand(qry, con);
                con.Open();
                int x2 = cmd.ExecuteNonQuery();
                con.Close();
                if (x2 > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Updated');", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('error ", true);
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
        finally
        {
            con.Close();
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillIssuecenter();
        FillBranchList();
          
    }
    protected void ddlDistrict2_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBranchList2();
    }
    private void FillDepositorList()
    {
        try
        {
            string query = "select Depositor_Name,Depositor_ID from tbl_MetaData_DEPOSITOR where BranchId='" + ddlBranch2.SelectedValue.ToString() + "'";
            con.Open();
            SqlDataAdapter da=new SqlDataAdapter(query,con);
            DataSet ds=new DataSet();
            da.Fill(ds);
            if(ds.Tables[0].Rows.Count!=0)
            {
                CheckBoxList1.DataSource = ds;
                CheckBoxList1.DataTextField = "Depositor_Name";
                CheckBoxList1.DataValueField = "Depositor_ID";
                CheckBoxList1.DataBind();
            }
            else
            {
                Response.Write("No Results found");
            }
        }
        catch(Exception ex)
        {
            Response.Write("<br>"+ex);
        }
        finally
        {
            con.Close();
        }
    }
    protected void ddlBranch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDepositorList();
    }
    protected void ddlDistrict3_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBranchList3();
    }
    protected void btnSubmit2_Click(object sender, EventArgs e)
    {
        string strChkBox = string.Empty;
        foreach (ListItem li in CheckBoxList1.Items)
        {
            if (li.Selected == true)
            {
                strChkBox =li.Value;
                try
                {
                    string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
                    qry = "insert into tbl_MetaData_DEPOSITOR_Log select * from tbl_MetaData_DEPOSITOR where Depositor_ID='" + strChkBox + "' and BranchId='" + ddlBranch2.SelectedValue.ToString() + "'";
                    cmd = new SqlCommand(qry, con);
                    con.Open();
                    int x1 = cmd.ExecuteNonQuery();
                    con.Close();
                    if (x1 > 0)
                    {
                        qry = " update tbl_MetaData_DEPOSITOR set BranchId='" + ddlBranch3.SelectedValue.ToString() + "',UpdatedBy='" + ClientIP + "',UpdatedDate=GETDATE() where Depositor_ID='" + strChkBox + "' and BranchId='" + ddlBranch2.SelectedValue.ToString() + "'";
                        cmd = new SqlCommand(qry, con);
                        con.Open();
                        int x2 = cmd.ExecuteNonQuery();
                        con.Close();
                        if (x2 > 0)
                        {
                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Updated');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('error ", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
                }
                finally
                {
                    con.Close();
                }
            }
        }
        FillDepositorList();
    }
}
