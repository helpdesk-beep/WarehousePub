using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class CapHiringScheme_AdminDashboard_CapRequestList : System.Web.UI.Page
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
            GetCapRequestList();
        }
    }

    public void GetCapRequestList()
    {

        string scope = Session["Scope"].ToString();
        string qr = "";

        if (scope == "H")
        {
            qr = "select bcap.BookId, bcap.Registration_ID, dst.Regionnm, dst.District_Name, br.BranchName, pre.Miller, bcap.TotalCapacity, bcap.TotalAmount, bcap.RequestDate from dbo.MillerCapBookMaster as bcap inner join PreRegistration as pre on pre.Registration_ID=bcap.Registration_ID inner join dbo.MetaDataBranchWithIssueCenter as br on br.BranchID=bcap.BranchId inner join dbo.tbl_MetaData_DISTRICT as dst on dst.District_Id=bcap.District_Id";
        }

        else if (scope == "R")
        {
            string regionid = Session["UserId"].ToString();
            qr = "select bcap.BookId, bcap.Registration_ID, dst.Regionnm, dst.District_Name, br.BranchName, pre.Miller, bcap.TotalCapacity, bcap.TotalAmount, bcap.RequestDate from dbo.MillerCapBookMaster as bcap inner join PreRegistration as pre on pre.Registration_ID=bcap.Registration_ID inner join dbo.MetaDataBranchWithIssueCenter as br on br.BranchID=bcap.BranchId inner join dbo.tbl_MetaData_DISTRICT as dst on dst.District_Id=bcap.District_Id where dst.Region_ID='" + regionid + "' ";

        }

        else if (scope == "B")
        {
            string branchid = Session["UserId"].ToString();
            qr = "select bcap.BookId, bcap.Registration_ID, dst.Regionnm, dst.District_Name, br.BranchName, pre.Miller, bcap.TotalCapacity, bcap.TotalAmount, bcap.RequestDate from dbo.MillerCapBookMaster as bcap inner join PreRegistration as pre on pre.Registration_ID=bcap.Registration_ID inner join dbo.MetaDataBranchWithIssueCenter as br on br.BranchID=bcap.BranchId inner join dbo.tbl_MetaData_DISTRICT as dst on dst.District_Id=bcap.District_Id where br.BranchID='" + branchid + "'";

        }


        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
            gvCapReqList.DataSource = dt;
            gvCapReqList.DataBind();

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
        gvCapReqList.Attributes["style"] = "border-collapse:separate";
        gvCapReqList.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
}