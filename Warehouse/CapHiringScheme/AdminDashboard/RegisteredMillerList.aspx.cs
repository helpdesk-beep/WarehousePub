using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class CapHiringScheme_AdminDashboard_RegisteredMillerList : System.Web.UI.Page
{
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Scope"] == null)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("../AdminLogin.aspx");
        }


        if (!IsPostBack)
        {
            GetRegMillerList();
        }
    }



   
    
    public void GetRegMillerList()
    {

        string scope = Session["Scope"].ToString();
        string qr = "";

        if (scope == "H")
        {
            qr = "select mreg.Mill_Id, mreg.Registration_ID, dst.Regionnm, dst.District_Name, br.BranchName, pre.Miller, mreg.Mill_Name, mreg.CreateOn from dbo.MillRegistrationMaster as mreg inner join PreRegistration as pre on pre.Registration_ID=mreg.Registration_ID inner join dbo.MetaDataBranchWithIssueCenter as br on br.BranchID=mreg.Near_Branch_Id inner join dbo.tbl_MetaData_DISTRICT as dst on dst.District_Id=mreg.District_Id";
        }

        else if (scope == "R")
        {
            string regionid = Session["UserId"].ToString();
            qr = "select mreg.Mill_Id, mreg.Registration_ID, dst.Regionnm, dst.District_Name, br.BranchName, pre.Miller, mreg.Mill_Name, mreg.CreateOn from dbo.MillRegistrationMaster as mreg inner join PreRegistration as pre on pre.Registration_ID=mreg.Registration_ID inner join dbo.MetaDataBranchWithIssueCenter as br on br.BranchID=mreg.Near_Branch_Id inner join dbo.tbl_MetaData_DISTRICT as dst on dst.District_Id=mreg.District_Id where dst.Region_ID='"+regionid+ "' ";

        }

        else if (scope == "B")
        {
            string branchid = Session["UserId"].ToString();
            qr = "select mreg.Mill_Id, mreg.Registration_ID, dst.Regionnm, dst.District_Name, br.BranchName, pre.Miller, mreg.Mill_Name, mreg.CreateOn from dbo.MillRegistrationMaster as mreg inner join PreRegistration as pre on pre.Registration_ID=mreg.Registration_ID inner join dbo.MetaDataBranchWithIssueCenter as br on br.BranchID=mreg.Near_Branch_Id inner join dbo.tbl_MetaData_DISTRICT as dst on dst.District_Id=mreg.District_Id where br.BranchID='" + branchid + "'";

        }


        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
            gvRegMillerList.DataSource = dt;
            gvRegMillerList.DataBind();

            if (dt.Rows.Count > 0)
            {
                btnExportExcel.Visible = true;
            }

            else
            {
                btnExportExcel.Visible = false;
            }

        }

        catch (Exception ex) { }
        finally { }
    }



    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "RegisteredMillerList_" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        gvRegMillerList.Attributes["style"] = "border-collapse:separate";
        gvRegMillerList.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
}