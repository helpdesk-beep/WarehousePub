using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Repush_File_Status : System.Web.UI.Page
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
            fillParty();
            //GetBillsDetail();
        }
    }
    private void fillParty()
    {
        try
        {
            cmd = new SqlCommand("dbo.Get_Party_Name", con, sqltrans);
            cmd.CommandType = CommandType.StoredProcedure;

            //SqlDataAdapter da = new SqlDataAdapter(str, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPartyName.Items.Clear();
                ddlPartyName.DataSource = ds.Tables[0];
                ddlPartyName.DataTextField = "Party_Name";
                ddlPartyName.DataValueField = "Party_Id";
                ddlPartyName.DataBind();
                ddlPartyName.Items.Insert(0, "---Select---");
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
    protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPartyName.SelectedIndex != 0)
        {
            GetBillsDetail();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Party Name')", true);
        }
    }
    private void GetBillsDetail()
    {
        try
        {

            cmd = new SqlCommand("dbo.Get_Beneficiary_For_Repush", con, sqltrans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Party_ID", ddlPartyName.SelectedValue);

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
                btnSave.Visible = true;
                lblNoofAC.Text = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                gvBOBillApp.DataSource = null;
                gvBOBillApp.DataBind();
                trnewproc.Visible = false;
                tblbtn.Visible = false;
                btnSave.Visible = false;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data Found'); </script> ");
            }

        }

        catch (Exception ex)
        {

        }
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (GridViewRow row in gvBOBillApp.Rows)
            {
                CheckBox chk_Sum = (CheckBox)(row.FindControl("chk_Sum"));
                HiddenField hdnBillNo = (HiddenField)(row.FindControl("hdnBillNo"));
                if (chk_Sum.Checked == true)
                {
                    con.Open();
                    cmd = new SqlCommand("[dbo].[Update_Bank_Fail_Status]", con, sqltrans);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BillNo", hdnBillNo.Value);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Request.UserHostAddress);
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
        Response.Redirect("~/StatePages/Repush_File_Status.aspx");
    }
}