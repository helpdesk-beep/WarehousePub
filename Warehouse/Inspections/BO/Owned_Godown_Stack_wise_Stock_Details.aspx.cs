using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Collections.Generic;

public partial class Inspections_BO_Owned_Godown_Stack_wise_Stock_Details : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string PFID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
       // PFID = Session["UserId"].ToString();
        Session["hdnGodown_ID"].ToString();
        Session["hdnbranchid"].ToString();
        Session["hdnCommodity_Id"].ToString();
        Session["hdnCropYear"].ToString();
        Session["hdnDepositor_ID"].ToString();
        if (!IsPostBack)
        {
            lblTotalInsp.Text = Session["lblGodown_Name"].ToString();
            fillScheduleInsp_Grid();
        }
    }

    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Insp_Get_Satck_Wise_Details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@Godown_ID", Session["hdnGodown_ID"].ToString());
                cmd.Parameters.AddWithValue("@Crop_Year", Session["hdnCropYear"].ToString());
                cmd.Parameters.AddWithValue("@Commodity_ID", Session["hdnCommodity_Id"].ToString());
                cmd.Parameters.AddWithValue("@DepositerID", Session["hdnDepositor_ID"].ToString());
                cmd.Parameters.AddWithValue("@InspectiontypeID", Session["ddlverification"].ToString());
                cmd.Parameters.AddWithValue("@QuaterType", Session["ddlquater"].ToString());
                cmd.Parameters.AddWithValue("@FinancialYear", Session["ddlfinancialyear"].ToString());
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnEmployee_ID"].ToString());

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            // load_img.Visible = true;
                            
                            GrdOfficerPreviousInsp.DataSource = dt;
                            GrdOfficerPreviousInsp.DataBind();
                            GrdOfficerPreviousInsp.FooterRow.Style.Add("text-align", "center");
                            GrdOfficerPreviousInsp.FooterRow.Cells[5].Text = "Total";
                            GrdOfficerPreviousInsp.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("RecBags")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("RecWeight")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("IssueBags")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("IssueWeight")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Bag_Balance")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Balance")).ToString();

                            //load_img.Visible = false;

                        }
                        else
                        {

                            GrdOfficerPreviousInsp.DataSource = null;
                            GrdOfficerPreviousInsp.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void GrdOfficerPreviousInsp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    HiddenField hdnVerificationType = (HiddenField)e.Row.FindControl("hdnVerificationType");
        //    Button btnfilloverallinsp = (Button)e.Row.FindControl("btnfilloverallinsp");
        //    if (hdnVerificationType.Value == "1")
        //    {
        //        btnfilloverallinsp.Visible = true;
        //    }
        //    else if(hdnVerificationType.Value=="2")
        //    {
        //        btnfilloverallinsp.Visible = false;
        //    }
        //}

    }
    protected void GrdOfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Overallinsp")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];
            //Fetch value of Name.           
            string hdnGodown_ID = (row.FindControl("hdnGodown_ID") as HiddenField).Value;
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnCommodity_Id = (row.FindControl("hdnCommodity_Id") as HiddenField).Value;
            string hdnCropYear = (row.FindControl("hdnCropYear") as HiddenField).Value;
            string hdnDepositor_ID = (row.FindControl("hdnDepositor_ID") as HiddenField).Value;

            
            string lblstack_id = (row.FindControl("lblstack_id") as Label).Text;
            string lblStack_Name = (row.FindControl("lblStack_Name") as Label).Text;
            string lblDepositor_Name = (row.FindControl("lblDepositor_Name") as Label).Text;
            string lblcommodity = (row.FindControl("lblcommodity") as Label).Text;
            string lblrecbags = (row.FindControl("lblBag_Balance") as Label).Text;
            string lblRecWeight = (row.FindControl("lblBalance") as Label).Text;

            Session["hdnGodown_ID"] = hdnGodown_ID.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnCommodity_Id"] = hdnCommodity_Id.ToString();
            Session["hdnCropYear"] = hdnCropYear.ToString();
            Session["hdnDepositor_ID"] = hdnDepositor_ID.ToString();

            Session["lblstack_id"] = lblstack_id.ToString();
            Session["lblStack_Name"] = lblStack_Name.ToString();
            Session["lblDepositor_Name"] = lblDepositor_Name.ToString();
            Session["lblcommodity"] = lblcommodity.ToString();
            Session["lblrecbags"] = lblrecbags.ToString();
            Session["lblRecWeight"] = lblRecWeight.ToString();

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Inspections/BO/Owned_Bolck_Wise_Entry.aspx';", true);
            Page.ClientScript.RegisterStartupScript(
   this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/BO/Owned_Bolck_Wise_Entry.aspx','_newtab');", true);
            //Response.Redirect("/Warehouse/Inspections/BO/Owned_Bolck_Wise_Entry.aspx");
            //Response.Redirect("window.location ='/Inspections/BO/Owned_Bolck_Wise_Entry.aspx'");


        }
    }
}