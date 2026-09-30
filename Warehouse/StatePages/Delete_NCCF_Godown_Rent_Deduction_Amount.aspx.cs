using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Delete_NCCF_Godown_Rent_Deduction_Amount : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlTransaction sqltrans;
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    string Bill_Type = "";
    string Ref_Number = "";
    string Ref_Aid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillDistrict();
        }
    }
    private void fillDistrict()
    {
        try
        {
            string region = "";
            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT Distinct NS.District_Id,MD.District_Name FROM tbl_Institution_NCCF_Storage_Bill_Details NS inner join tbl_MetaData_DISTRICT MD on NS.District_Id=MD.District_Id order by MD.District_Name asc";
            }
            else
            {
                if (Session["RoleId"].ToString() == "2")
                {
                    query = "SELECT Distinct NS.District_Id,MD.District_Name FROM tbl_Institution_NCCF_Storage_Bill_Details NS inner join tbl_MetaData_DISTRICT MD on NS.District_Id=MD.District_Id where MD.Region_ID='" + region + "' order by MD.District_Name asc";
                }
                else
                {
                    query = "SELECT Distinct NS.District_Id,MD.District_Name FROM tbl_Institution_NCCF_Storage_Bill_Details NS inner join tbl_MetaData_DISTRICT MD on NS.District_Id=MD.District_Id where NS.District_Id ='" + Session["Depot_DistID"].ToString() + "' order by MD.District_Name asc";

                }
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
                ddlDistrict.Items.Insert(0, "--Select--");
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
            string region = "";
            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "'";
            }
            else
            {
                query = "  SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "' and DepoTypeID='4'";

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
                ddlbranch.DataTextField = "DepotName";
                ddlbranch.DataValueField = "BranchId";
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, "--Select--");

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
    private void fillGodown()
    {
        try
        {
            string query = "";

            query = "select Distinct NS.Godown_ID,MG.Godown_Name from tbl_Institution_NCCF_Storage_Bill_Details  NS inner join tbl_MetaData_GODOWN_2018 MG On NS.Godown_ID=MG.Godown_ID where NS.Branch_Id='" + ddlbranch.SelectedValue + "' order by MG.Godown_Name";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodown.Items.Clear();
                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
                ddlGodown.Items.Insert(0, "--Select--");

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
    private void GetBillsDetail()
    {
        try
        {
            cmd = new SqlCommand("dbo.Get_NCCF_Godown_Rent_Deduction_Amount", con, sqltrans);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
            cmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue);

            //SqlDataAdapter da = new SqlDataAdapter(str, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBOBillApp.DataSource = ds.Tables[0];
                gvBOBillApp.DataBind();
                trnewproc.Visible = true;
                tblbtn.Visible = true;
                btnDelete.Visible = true;
                lblNoofAC.Text = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                gvBOBillApp.DataSource = null;
                gvBOBillApp.DataBind();
                trnewproc.Visible = false;
                tblbtn.Visible = false;
                btnDelete.Visible = false;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data Found'); </script> ");
            }

        }

        catch (Exception ex)
        {

        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillIssuecenter();
    }
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        GetBillsDetail();
        System.Threading.Thread.Sleep(1000);
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        // GetBillsDetail();
        fillGodown();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (GridViewRow row in gvBOBillApp.Rows)
            {
                CheckBox chk_Sum = (CheckBox)(row.FindControl("chk_Sum"));
                HiddenField hdnRef_Bill_No = (HiddenField)(row.FindControl("hdnRef_Bill_No"));
                if (chk_Sum.Checked == true)
                {
                    con.Open();
                    cmd = new SqlCommand("[dbo].[Delete_NCCF_Godown_Rent_Deduction_Amount]", con, sqltrans);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Ref_Bill_No", hdnRef_Bill_No.Value);
                    cmd.Parameters.AddWithValue("@IPAddress", Request.UserHostAddress);
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        count++;
                    }
                    con.Close();
                }
            }
            //if (count > 0)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Delete successfully..')", true);
            //    GetBillsDetail();
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record NOT Delete , कटोत्रा डिलीट करने से पहले बिल से डीएससी डिलीट करे')", true);
            //}
            if (count > 0)
            {
                // Success SweetAlert call
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mymsg1", "showAlert('Record Deleted successfully.', 'success');", true);
                GetBillsDetail();
            }
            else
            {
                // Warning SweetAlert call with your custom message
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mymsg1", "showAlert('Record NOT Deleted, कटोत्रा डिलीट करने से पहले बिल से डीएससी डिलीट करे', 'warning');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }
        finally
        {
            con.Close();
        }
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StatePages/Delete_NCCF_Godown_Rent_Deduction_Amount.aspx");
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillsDetail();
    }
}