using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Region_UpdateGodownEntryinAgrrement : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlConnection jvscon = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            try
            {
                if (!IsPostBack)
                {
                    fillDistrict();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void fillDistrict()
    {
        try
        { 
            string query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT]  where Region_ID='" + Session["Region_ID"].ToString() + "' order by District_Name asc";
            cmd = new SqlCommand(query, con);
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
                ddlDistrict.Items.Insert(0, "---Select---");
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
    private void getDepot(string distId)
    {
        try
        {
            string query = "";
            //query = "select depo.BranchId,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' and DepoTypeID='4' order by DepotName asc";
            query = "select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepotList.DataSource = ds.Tables[0];
                ddlDepotList.DataTextField = "DepotName";
                ddlDepotList.DataValueField = "BranchId";
                ddlDepotList.DataBind();
                ddlDepotList.Items.Insert(0, "---Select---");
             
            }
            else
            {
                ddlDepotList.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception)
        {
            ///////
        }
    }
    private void getRegGodown()
    {
        try
        {
            //string query = "";
            //query = "select depo.BranchId,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' and DepoTypeID='4' order by DepotName asc";
         //   query = "select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";

            cmd = new SqlCommand("Get_New_Offer_JVS_Godown", jvscon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID",ddlDepotList.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodown.DataSource = ds.Tables[0];
                ddlgodown.DataTextField = "WH_Name";
                ddlgodown.DataValueField = "Registration_Id";
                ddlgodown.DataBind();
                ddlgodown.Items.Insert(0, "---Select---");

            }
            else
            {
                ddlgodown.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception ex)
        {
            ///////
        }
    }
    private void FillData()
    {
        try
        {
            //string query = "";
            //query = "select depo.BranchId,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' and DepoTypeID='4' order by DepotName asc";
            //   query = "select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";

            cmd = new SqlCommand("Get_Details_by_Registration_ID", jvscon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RegID", ddlgodown.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblregid.Text = dt.Rows[0]["Registration_Id"].ToString();
                lblwhname.Text = dt.Rows[0]["WH_Name"].ToString();
                lblpriority.Text = dt.Rows[0]["Selected_Priority"].ToString();
                lblMaintainby.Text = dt.Rows[0]["Maintain_By"].ToString();
                lbloffercapacity.Text = dt.Rows[0]["Offer_Capacity"].ToString();

                Session["Registration_Id"] = dt.Rows[0]["Registration_Id"].ToString();
                Session["Selected_Priority"] = dt.Rows[0]["Selected_Priority"].ToString();
                Session["Maintain_By"] = dt.Rows[0]["Maintain_By"].ToString();
                showdetails.Visible = true;
            }
            else
            {
                showdetails.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ///////
        }
    }


    private void getIntGodown()
    {
        try
        {
           // string query = "";
            //query = "select depo.BranchId,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' and DepoTypeID='4' order by DepotName asc";
           // query = "select Godown_ID,Godown_Name+' - '+Godown_ID AS Godown_Name from tbl_MetaData_GODOWN_2018 where BranchID='" + ddlDepotList.SelectedValue.ToString() + "' order by Godown_Name asc";

            cmd = new SqlCommand("Get_Godown_Name_not_in_2022_Table", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", ddlDepotList.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlintgodown.DataSource = ds.Tables[0];
                ddlintgodown.DataTextField = "Godown_Name";
                ddlintgodown.DataValueField = "Godown_ID";
                ddlintgodown.DataBind();
                ddlintgodown.Items.Insert(0, "---Select---");

            }
            else
            {
                ddlintgodown.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception ex)
        {
            ///////
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex != 0)
        {
            getDepot(ddlDistrict.SelectedValue.ToString());
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District')", true);
        }
    }
    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District First..')", true);
        }
        else if (ddlDepotList.SelectedIndex != 0)
        {
            getRegGodown();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Branch First..')", true);
        }
    }
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
  
    protected void btn_Close_Click(object sender, EventArgs e)
    {
       // Response.Redirect("~/StatePages/CreatePvtGodownLogin.aspx");
    }

    //new implement

     protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillData();
        getIntGodown();
    }

    protected void ddlRegGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    public void checkvalidation()
    {
        if (ddlintgodown.SelectedValue == "---Select---")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('गोदाम का नाम चुनें')", true);
            ddlintgodown.Focus();
            return;
        }      
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            checkvalidation();
            string CS = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Update_Godown_in_Godown_2022_for_FIFO", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@Registration_ID", Session["Registration_Id"].ToString());
                if (Session["Maintain_By"].ToString()=="PMS")
                {
                    cmd.Parameters.AddWithValue("@Priority", 10);
                }
               else if (Session["Selected_Priority"].ToString() == "1")
                {
                    cmd.Parameters.AddWithValue("@Priority", 11);
                }
                else if (Session["Selected_Priority"].ToString() == "2")
                {
                    cmd.Parameters.AddWithValue("@Priority", 12);
                }
                else if (Session["Selected_Priority"].ToString() == "3")
                {
                    cmd.Parameters.AddWithValue("@Priority", 13);
                }
                else if (Session["Selected_Priority"].ToString() == "4")
                {
                    cmd.Parameters.AddWithValue("@Priority", 14);
                }
                else if (Session["Selected_Priority"].ToString() == "5")
                {
                    cmd.Parameters.AddWithValue("@Priority", 15);
                }
                cmd.Parameters.AddWithValue("@GodownID", ddlintgodown.SelectedValue);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Godown Entry Successfully Submitted|||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    //Clear();
                    //fillGrid();
                }
                else
                {
                    string strMsg2 = "Godown Data Already Exist|||";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + strMsg2 + "');", true);
                }
                //fillGrid();
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
}
