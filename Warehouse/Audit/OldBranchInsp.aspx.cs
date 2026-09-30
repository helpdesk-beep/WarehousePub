using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspection_OldBranchInsp : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string qry = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["login"] != null)
        {
            if (!IsPostBack)
            {
                getwhrdata();
            }
        }
        else
        {
            Response.Redirect("InspectionLogin.aspx");

        }
        
    }

    private void getwhrdata()
    {
        try
        {
            //string query = "SELECT convert(varchar(20),[AuditDate],103) as Auditdate ,[AuId] FROM [Intergrated_MP_STORAGE].[dbo].[BranchAudit] where BranchId='"+Session["UserID"].ToString()+"'";
            string query = "SELECT convert(varchar(20),[AuditDate],103) as Auditdate ,[AuId],AuditerName,AuditerPost FROM [Intergrated_MP_STORAGE].[dbo].[BranchAudit] where BranchId='" + Session["UserID"].ToString() + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvolddates.DataSource = ds.Tables[0];
                gvolddates.DataBind();

            }
            else
            {

            }

        }

        catch (Exception ex)
        {

        }
    }


    protected void gvolddates_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Auid"] = gvolddates.SelectedRow.Cells[1].Text.ToString();
        Response.Redirect("BranchInspecPrint.aspx");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("InspectionLogin.aspx");
    }
    protected void gvolddates_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string AUDIT_ID = "";
        try
        {
            if (e.CommandName == "GAUDIT")
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                AUDIT_ID = Convert.ToString(gvolddates.DataKeys[row.RowIndex].Value);

                if (AUDIT_ID != "")
                {
                    Session["Auid"] = AUDIT_ID;
                    Response.Redirect("GodownInspection.aspx");
                    
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occured...'); </script> ");
                }

            }
            else if (e.CommandName == "GAUDIT2")
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                AUDIT_ID = Convert.ToString(gvolddates.DataKeys[row.RowIndex].Value);
                Session["Auid"] = AUDIT_ID;
                Session["RPrint"] = "R";
                Response.Redirect("GodownInspecPrint.aspx");
            }
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        Response.Redirect("BranchInspection.aspx");
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Response.Redirect("AuditChangePassword.aspx");
    }
}