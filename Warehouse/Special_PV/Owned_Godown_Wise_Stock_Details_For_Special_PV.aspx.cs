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

public partial class Special_PV_Owned_Godown_Wise_Stock_Details_For_Special_PV : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string PFID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        PFID = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            if (Session["dateofclossing"] != null)
            {
                if (!string.IsNullOrEmpty(Session["dateofclossing"].ToString()))
                {
                    fillsyncbranchwisedata();
                }
            }
            fillGodownDetails();
            //fillFinsncilYear();
            //fillEMPDetails();
        }
    }
    //public void fillFinsncilYear()
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        SqlCommand cmd = new SqlCommand("Get_Fianancial_Year_For_inspection", con);
    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        con.Open();
    //        ddlfinancialyear.DataSource = cmd.ExecuteReader();
    //        ddlfinancialyear.DataTextField = "Financial_Year";
    //        ddlfinancialyear.DataValueField = "Financial_Year";
    //        ddlfinancialyear.DataBind();
    //        ddlfinancialyear.Items.Insert(0, new ListItem("--Select Financial Year--", "0"));
    //        con.Close();
    //    }
    //}
    protected void fillsyncbranchwisedata()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Sync_Branch_Stock_position_Emp_Insp_Quater_Wise_For_Special_PV", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@FromDate", Session["dateofclossing"].ToString());
                //cmd.Parameters.AddWithValue("@Financial_Year", Session["Financialyear"].ToString());
                //cmd.Parameters.AddWithValue("@Employee_ID", Session["Employeeid"].ToString());
                //cmd.Parameters.AddWithValue("@Inspection_Type_ID", Session["Verificaitontype"].ToString());
                //cmd.Parameters.AddWithValue("@Quater_Type", Session["quaterid"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                    }
                }
            }
        }
    }
    public void fillGodownDetails()
    {
        string conStr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Name", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
            con.Open();
            ddl_gdwn.DataSource = cmd.ExecuteReader();
            ddl_gdwn.DataTextField = "Godown_Name";
            ddl_gdwn.DataValueField = "Godown_ID";
            ddl_gdwn.DataBind();
            ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con.Close();
        }
    }
    //public void fillEMPDetails()
    //{
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        SqlCommand cmd = new SqlCommand("Get_Employee_For_Branch_Wise", con);
    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
    //        con.Open();
    //        ddlemp.DataSource = cmd.ExecuteReader();
    //        ddlemp.DataTextField = "Officer_Name";
    //        ddlemp.DataValueField = "Employee_ID";
    //        ddlemp.DataBind();
    //        ddlemp.Items.Insert(0, new ListItem("-- Select Employee --", "0"));
    //        con.Close();
    //    }
    //}
    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Insp_Get_Special_PV_Godown_wise_Data_For_Owned", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", PFID);
                cmd.Parameters.AddWithValue("@gdnID", ddl_gdwn.SelectedValue);
                //cmd.Parameters.AddWithValue("@EmployeeID", ddlemp.SelectedValue);
                //cmd.Parameters.AddWithValue("@QuaterID", ddlquater.SelectedValue);
                //cmd.Parameters.AddWithValue("@InspectionTypeID", ddlverification.SelectedValue);
                //cmd.Parameters.AddWithValue("@FinYear", ddlfinancialyear.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            divshow.Visible = true;
                            GrdOfficerPreviousInsp.DataSource = dt;
                            GrdOfficerPreviousInsp.DataBind();
                            GrdOfficerPreviousInsp.FooterRow.Style.Add("text-align", "center");
                            GrdOfficerPreviousInsp.FooterRow.Cells[4].Text = "Total";
                            GrdOfficerPreviousInsp.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("RecBags")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("delbags")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Rec_Weight")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Del_Weight")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Bag_Balance")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Balance")).ToString();
                        }
                        else
                        {
                            divshow.Visible = false;
                            GrdOfficerPreviousInsp.DataSource = null;
                            GrdOfficerPreviousInsp.DataBind();
                            string strMsg = "गोदाम में स्टॉक उपलब्ध नहीं हैं  |";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "')", true);
                        }
                    }
                }
            }
        }
    }
    protected void GrdOfficerPreviousInsp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
            // string hdnEmployee_ID = (row.FindControl("hdnEmployee_ID") as HiddenField).Value;
            string hdnbranchid = (row.FindControl("hdnbranchid") as HiddenField).Value;
            string hdnCommodity_Id = (row.FindControl("hdnCommodity_Id") as HiddenField).Value;
            string hdnCropYear = (row.FindControl("hdnCropYear") as HiddenField).Value;
            string hdnDepositor_ID = (row.FindControl("hdnDepositor_ID") as HiddenField).Value;
            string lblGodown_Name = (row.FindControl("lblGodown_Name") as Label).Text;
            Session["hdnGodown_ID"] = hdnGodown_ID.ToString();
            Session["hdnbranchid"] = hdnbranchid.ToString();
            Session["hdnCommodity_Id"] = hdnCommodity_Id.ToString();
            Session["hdnCropYear"] = hdnCropYear.ToString();
            Session["hdnDepositor_ID"] = hdnDepositor_ID.ToString();
            //Session["ddlquater"] = ddlquater.SelectedValue;
            //Session["ddlverification"] = ddlverification.SelectedValue;
            //Session["ddlfinancialyear"] = ddlfinancialyear.SelectedValue;
            Session["lblGodown_Name"] = lblGodown_Name.ToString();
            //Session["hdnEmployee_ID"] = hdnEmployee_ID.ToString();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('/Warehouse/Special_PV/Owned_Godown_Stack_wise_Stock_Details_For_Special_PV.aspx','_newtab');", true);
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }
    protected void ddl_gdwn_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }
}