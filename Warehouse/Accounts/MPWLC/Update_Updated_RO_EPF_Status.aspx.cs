using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Update_Updated_RO_EPF_Status : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    SqlTransaction sqltrans;
    DataSet ds = null;
    SqlDataAdapter da = null;
    string Bill_Type = "";
    string Ref_Number = "";
    string Ref_Aid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                fillDistrict();
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
           
            string query = "";
            
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            
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
                //gv.DataSource = null;
                //gv.DataBind();
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
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex != 0)
        {
            GetBillsDetail();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District')", true);
        }
    }
    private void GetBillsDetail()
    {
        try
        {
            string Dist_id = ddlDistrict.SelectedValue.ToString();
            string str = "";
            str = "SELECT DepotName AS Branch,Reference_No,Debit_Amount,CONVERT(VARCHAR(10), Date, 103) CreatedDate,CONVERT(VARCHAR(10), Uploaded_Date, 103) UploadedDate,CASE WHEN Bank_Type = 'O' THEN 'Other Bank' ELSE 'Same Bank (SBI)' END BankType FROM[tbl_Payment_Debit] PD INNER JOIN tbl_MetaData_DEPOT D ON PD.Branch_Id = D.BranchId WHERE PD.District_Id = '" + Dist_id + "' AND PD.HO_Marked_Status='Y' AND HO_Upload_Status IS NULL";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBOBillApp.DataSource = ds.Tables[0];
                gvBOBillApp.DataBind();
                trnewproc.Visible = true;
                lblNoofAC.Text = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                gvBOBillApp.DataSource = null;
                gvBOBillApp.DataBind();
                trnewproc.Visible = false;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data Found'); </script> ");
            }

        }

        catch (Exception ex)
        {

        }
    }
    public Boolean validate()
    {
        int chkstatus = 0;
        foreach (GridViewRow row in gvBOBillApp.Rows)
        {
            CheckBox chk_Sum = (CheckBox)(row.FindControl("chk_Sum"));
            HiddenField hdnRefno = (HiddenField)(row.FindControl("hdnRefno"));

            if (chk_Sum.Checked == true)
            {
                chkstatus = chkstatus + 1;
            }
        }
        if (chkstatus > 0)
        {
            return true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया कम से कम एक चेकबॉक्स चेक करे ')", true);
            return false;
        }
    }
    protected void Save(object sender, EventArgs e)
    {
        String TheResult = "";
        int count = 0;

        if (validate())
        {
            foreach (GridViewRow row in gvBOBillApp.Rows)
            {
                CheckBox chk_Sum = (CheckBox)(row.FindControl("chk_Sum"));
                HiddenField hdnRefno = (HiddenField)(row.FindControl("hdnRefno"));
                if (chk_Sum.Checked == true)
                {
                    con.Open();
                    cmd = new SqlCommand("[dbo].[HO_Update_Uploaded_Status]", con, sqltrans);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RefrenceNo", hdnRefno.Value);
                    cmd.Parameters.AddWithValue("@IPAddress", Request.UserHostAddress);
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        count++;
                    }
                    con.Close();
                }
            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Updated successfully..')", true);
                GetBillsDetail();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record NOT Updated')", true);
            }
        }
    }
}