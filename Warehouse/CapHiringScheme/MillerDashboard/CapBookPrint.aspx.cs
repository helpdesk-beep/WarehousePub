using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CapHiringScheme_MillerDashboard_CapBookPrint : System.Web.UI.Page
{
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["MillerRegId"] == null || Session["Id"] == null)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("../Login.aspx");
        }



        if (!IsPostBack)
        {
            if (Session["BookId"] != null)
            {
                string bookid = Session["BookId"].ToString();

                string mregid = Session["MillerRegId"].ToString();
                dt = GetBookCapPrint(mregid, bookid);

                rptBookGDCap.DataSource = dt;
                rptBookGDCap.DataBind();

                if (dt.Rows.Count > 0)
                {

                    litMill.Text = dt.Rows[0]["Miller"].ToString();
                    litRegId.Text = dt.Rows[0]["Registration_ID"].ToString();
                    litPhone.Text = dt.Rows[0]["Mobile"].ToString();
                    ltlEmail.Text = dt.Rows[0]["Email"].ToString();
                    ltlResrvDt.Text = DateTime.Parse(dt.Rows[0]["RequestDate"].ToString()).ToString("dd/MM/yyyy");


                    ltTtlCap.Text = String.Format("{0:0.##}", (Decimal)dt.Rows[0]["TotalCapacity"]);

                    //ltTtlAmt.Text = dt.Rows[0]["TotalAmount"].ToString();

                    ltTtlAmt.Text=String.Format("{0:#}", (Decimal)dt.Rows[0]["TotalAmount"]);


                    

                }
            }
        }

    }

    public DataTable GetBookCapPrint(string mregid,string bookid)
    {
        string qr = "select * from dbo.MillerCapBookMaster as capb inner join dbo.PreRegistration as pr on pr.Registration_ID=capb.Registration_ID inner join dbo.MillerCapGodownBookDetails as gdb on gdb.BookId=capb.BookId inner join dbo.tbl_MetaData_Miller_Cap_2020 as mgcap on mgcap.Godown_ID=gdb.GodownID where gdb.Registration_ID='" + mregid + "' and capb.BookId='" + bookid + "'";

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }

}