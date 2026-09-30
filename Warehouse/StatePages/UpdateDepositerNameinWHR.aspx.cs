using System;
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
using Data;
using DataAccess;
using System.Diagnostics;
using System.Resources;

public partial class StatePages_UpdateDepositerNameinWHR : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                //getdistrict();
                filldepositer();
                filldepositerFill();
                ddlDepositorfill.Enabled = false;
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_WHR_Details_For_Change_Depositer_Name", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
               // cmd.Parameters.AddWithValue("@District_Id", DropDownList1.SelectedValue);
                //cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue);
                // cmd.Parameters.AddWithValue("@GodownID", ddlgodown.SelectedValue);
               // cmd.Parameters.AddWithValue("@CropYear", ddlcropyear.SelectedValue);
                cmd.Parameters.AddWithValue("@Whrno", txttwhrno.Text.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Depositor_Gridview.DataSource = dt;
                            Depositor_Gridview.DataBind();

                        }
                        else
                        {
                            Depositor_Gridview.DataSource = null;
                            Depositor_Gridview.DataBind();
                        }
                    }
                }
            }
        }
    }

    //public void GetBranch(string distID)
    //{

    //    string qry = "";
    //    qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId ='" + distID + "' order by DepotName";
    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlBranch.DataSource = ds.Tables[0];
    //        ddlBranch.DataTextField = "DepotName";
    //        ddlBranch.DataValueField = "BranchId";
    //        ddlBranch.DataBind();
    //        ddlBranch.Items.Insert(0, "--Select--");
    //    }
    //}

    public void filldepositerFill()
    {
        string query2 = "";
        // query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679')";
        query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR]";
        SqlCommand cmd2 = new SqlCommand(query2, con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count > 0)
        {
            ddlDepositorfill.DataSource = ds2;
            ddlDepositorfill.DataTextField = "Depositor_Name";
            ddlDepositorfill.DataValueField = "Depositor_ID";
            ddlDepositorfill.DataBind();
            ddlDepositorfill.Items.Insert(0, "--Select--");

        }
    }

    public void filldepositer()
    {
        string query2 = "";
        query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679','181','15478','16985')";
        //query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR]";
        SqlCommand cmd2 = new SqlCommand(query2, con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count > 0)
        {
            ddlDepositor.DataSource = ds2;
            ddlDepositor.DataTextField = "Depositor_Name";
            ddlDepositor.DataValueField = "Depositor_ID";
            ddlDepositor.DataBind();
            ddlDepositor.Items.Insert(0, "--Select--");
            //ddlDepositor.SelectedValue=
        }
    }
    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = Depositor_Gridview.Rows[rowIndex];

        lblgodownname.Text = (row.FindControl("lblGodown_Name") as Label).Text;
        txtGdwnID.Text = (row.FindControl("hdngodownid") as HiddenField).Value;
        txtwhrno.Text = (row.FindControl("lblWhr_No") as Label).Text;
        txtWHRRate.Text = (row.FindControl("lblMktvalue") as Label).Text;
        ddlDepositorfill.SelectedValue = (row.FindControl("hdndepositerid") as HiddenField).Value;
        divNewInsp.Visible = true;
        ModalPopupExtender1.Show();
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetBranch(DropDownList1.SelectedValue.ToString());
    //}
    //public void getdistrict()
    //{
    //    string qry = "select District_Name,District_Id from tbl_MetaData_DISTRICT order by District_Name ";
    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        DropDownList1.DataSource = ds.Tables[0];
    //        DropDownList1.DataTextField = "District_Name";
    //        DropDownList1.DataValueField = "District_Id";
    //        DropDownList1.DataBind();
    //        DropDownList1.Items.Insert(0, "--Select--");
    //    }
    //}

    //protected void txttwhrno_TextChanged(object sender, EventArgs e)
    //{
    //    fillScheduleInsp_Grid();
    //}

    protected void btnAddCompany_Click1(object sender, EventArgs e)
    {
        GridViewRow gvr = Depositor_Gridview.SelectedRow;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            if (txtGdwnID.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown')", true);
            }

            else if (txtwhrno.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter WHR No')", true);
            }
            else
            {
                sqltrans = con.BeginTransaction();
                //  con.Open();
                cmd = new SqlCommand("Update_WHR_Depositer_Name", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = sqltrans;
                cmd.Parameters.AddWithValue("@Godown_ID", txtGdwnID.Text);
                cmd.Parameters.AddWithValue("@WHR_No", txtwhrno.Text);
                cmd.Parameters.AddWithValue("@Depositer_Name", ddlDepositor.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Depositer_ID", ddlDepositor.SelectedValue);
                cmd.Parameters.AddWithValue("@MktValue_of_Commodity", txtWHRRate.Text);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('WHR Depositer Name Update Successfully')", true);
                    sqltrans.Commit();
                    lblgodownname.Text = "";
                    txtGdwnID.Text = "";
                    Depositor_Gridview.DataSource = "";
                    Depositor_Gridview.DataBind();
                    fillScheduleInsp_Grid();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record not Updatet')", true);

                }
            }
        }
        catch (Exception ex)
        {
            sqltrans.Rollback();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error')", true);
        }
        finally
        {
            con.Close();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        // Find the row where checkbox is checked
        CheckBox chk = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chk.NamingContainer;

        // Find TextBox inside that row
        TextBox txt = (TextBox)row.FindControl("txtMktvalue");

        // Show/Hide textbox based on checkbox
        txt.ReadOnly = chk.Checked;
    }

    protected void chkbox_CheckedChanged(object sender, EventArgs e)
    {
        if (chkbox.Checked)
        {
            txtWHRRate.Enabled = true;
            divNewInsp.Visible = true;
            ModalPopupExtender1.Show();
        }
        else
        {
            txtWHRRate.Enabled = false;
            divNewInsp.Visible = true;
            ModalPopupExtender1.Show();
        }
    }

    protected void Depositor_Gridview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblPrintStatus");
            if (lbl != null)
            {
                string status = lbl.Text;

                if (status == "printed")
                {
                    // Printed → Red
                    lbl.ForeColor = System.Drawing.Color.Red;
                }
                else if (status == "WHR Not Printed")
                {
                    // Printed → Red
                    lbl.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    // Not Printed → Green
                    lbl.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}
