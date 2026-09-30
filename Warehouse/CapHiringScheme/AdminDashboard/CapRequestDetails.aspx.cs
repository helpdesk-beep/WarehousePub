using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class CapHiringScheme_AdminDashboard_CapRequestDetails : System.Web.UI.Page
{
    DataTable dt=new DataTable();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["BookId"].ToString() != null & Request.QueryString["RegId"].ToString() != null)
        {
            string bookid = Request.QueryString["BookId"].ToString();
            string mregid = Request.QueryString["RegId"].ToString();


            dt = GetMillerDetails(mregid);
            rptPersonalDetails.DataSource = dt;
            rptPersonalDetails.DataBind();

            dt = GetMillerCapBookDetails(bookid);
            rptBookCap.DataSource = dt;
            rptBookCap.DataBind();

            dt = GetMillerCapGodownBookDetails(bookid);
            gvCapGodownBookDetails.DataSource = dt;
            gvCapGodownBookDetails.DataBind();

        }
    }



    public DataTable GetMillerDetails(string mregid)
    {

        string qr = "select * from PreRegistration pre where pre.Registration_ID='" + mregid + "'";

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }


    public DataTable GetMillerCapBookDetails(string bookid)
    {

        string qr = "select bcap.BookId, bcap.Registration_ID, dst.Regionnm, dst.District_Name, br.BranchName, bcap.TotalCapacity, bcap.TotalAmount, bcap.RequestDate from dbo.MillerCapBookMaster as bcap inner join dbo.MetaDataBranchWithIssueCenter as br on br.BranchID=bcap.BranchId inner join dbo.tbl_MetaData_DISTRICT as dst on dst.District_Id=bcap.District_Id where BookId='" + bookid + "'";

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }


    public DataTable GetMillerCapGodownBookDetails(string bookid)
    {

        string qr = "select gd.GodownID,cap.Godown_Name,gd.BookCapacityType,gd.BookGodownCapacity,gd.BookGodownAmount from dbo.MillerCapGodownBookDetails gd inner join tbl_MetaData_Miller_Cap_2020 cap on cap.Godown_ID=gd.GodownID where BookId='"+ bookid +"'";

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }



}