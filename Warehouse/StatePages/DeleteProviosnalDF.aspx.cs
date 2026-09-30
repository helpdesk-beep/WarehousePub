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

public partial class StatePages_DeleteProviosnalDF : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            if (!IsPostBack)
            {

            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void GetDepositorForm()
    {
        try
        {
            string qry = "";
            qry = "SELECT [ChkQ_ID],(select District_Name from tbl_MetaData_DISTRICT where District_Id=ChkPDF.District_ID)as District,(select DepotName from tbl_MetaData_DEPOT where BranchId=ChkPDF.Branch_ID)as Branch,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID=ChkPDF.Godown_ID)as Godown,[TC_Number],[Acceptance_No],convert(varchar(10),[Acceptance_Date],103) as [Acceptance_Date],[Recd_Bags],[NetWeight],[Society_Name],case when [Qualitychk_Status]='Y' then 'Yes' else 'No' end [Qualitychk_Status],case when [Rejection_Status]='Y' then 'Yes' else 'No' end [Rejection_Status]  FROM [ChkQuality_ProviosnalDepositorForm] as ChkPDF where WHR_Request='" + txtDFNo.Text + "' ";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Depositor_Gridview.DataSource = ds;
                Depositor_Gridview.DataBind();
                trbtnhide.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
                trbtnhide.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GetDepositorForm();
    }
    protected void btnAddCompany_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        
        try
        {
            if (txtDFNo.Text != "")
            {
                int chkFinalDF = ChkFinalDF();
                if (chkFinalDF == 0)
                {
                    sqltrans = con.BeginTransaction();
                    string qry = "";
                    qry = "insert into ChkQuality_ProviosnalDepositorForm_log select *,'" + ip + "',GETDATE() from ChkQuality_ProviosnalDepositorForm where WHR_Request='" + txtDFNo.Text + "' ";
                    SqlCommand cmd = new SqlCommand(qry, con, sqltrans);
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        string qryDel = "";
                        qryDel = "Delete ChkQuality_ProviosnalDepositorForm where WHR_Request='" + txtDFNo.Text + "' ";
                        SqlCommand cmdDel = new SqlCommand(qryDel, con, sqltrans);
                        int b = cmdDel.ExecuteNonQuery();
                        if (b > 0)
                        {
                            sqltrans.Commit();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Delete')", true);
                            trbtnhide.Visible = false;
                            txtDFNo.Text = "";
                            Depositor_Gridview.DataSource = "";
                            Depositor_Gridview.DataBind();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Not Delete Something Error')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Final Depositor Form is created.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Depositor Form No.')", true);
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
    public int ChkFinalDF()
    {
        int ch = 0;
        string strsql = "select * from MPSCSC.dbo.Final_DepositerForm_CSM2018 where WHR_Requestold='" + txtDFNo.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strsql,con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ch = 1;
        }
        else
        {
            ch = 0;
        }
        return ch;
    }
}
