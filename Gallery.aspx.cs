using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WLCBusinessLayer;

public partial class Gallery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGrid();
        }
    }
    public void FillGrid()
    {
        DataTable dt;
        dt = new Common().GetGalleryTitleList();
        if (dt.Rows.Count > 0)
        {
            GVOfStock.DataSource = dt;
            GVOfStock.DataBind();
            Session["Title"] = dt.Rows[0]["Title"].ToString();
        }
    }
    protected void GVOfStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "COUNT_REQUEST_ID")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GVOfStock.Rows[rowIndex];

            //Fetch value of Name.
            string hdnTitle = (row.FindControl("hdnTitle") as HiddenField).Value;
            Session["hdnTitle"] = hdnTitle.ToString();
            Response.Redirect("GalleryDetails.aspx");

        }
        //if (e.CommandName == "COUNT_REQUEST_ID")
        //{
        //    LinkButton lnkView = (LinkButton)e.CommandSource;
        //    string dealId = lnkView.CommandArgument;
        //   // string hdnID = (row.FindControl("hdnTitle") as HiddenField).Value;
        //    Session["hdnTitle"] = dealId.ToString();
        //    Response.Redirect("GalleryDetails.aspx");
        //}
        //if (e.CommandName == "COUNT_REQUEST_ID")
        //{
        //    //Determine the RowIndex of the Row whose Button was clicked.
        //    int rowIndex = Convert.ToInt32(e.CommandArgument);

        //    //Reference the GridView Row.
        //    GridViewRow row = GVOfStock.Rows[rowIndex];
        //    //Fetch value of Name.           
        //    string hdnID = (row.FindControl("hdnTitle") as HiddenField).Value;
        //    Session["hdnTitle"] = hdnID.ToString();
        //    Response.Redirect("GalleryDetails.aspx");
        //}
    }
   
}