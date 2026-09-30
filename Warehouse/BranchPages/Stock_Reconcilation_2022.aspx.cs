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
using System.Drawing;

public partial class Reports_Branch_Stock_Reconcilation_2022 : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid = "";
    SqlTransaction sqltran;
    string depottype = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] != null)
                fillgrid();
            else
                Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Wise_online_Stock_position", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@DistrictID", Session["Depot_DistID"].ToString());
                cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
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
                            Depositor_Gridview.DataSource = dt;
                            Depositor_Gridview.DataBind();
                        }
                    }
                }
            }
        }
    }

    //protected void Depositor_Gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    int indx = e.NewPageIndex;
    //    Depositor_Gridview.PageIndex = e.NewPageIndex;
    //    fillgrid();
    //}

    [Obsolete]
    protected void Depositor_Gridview_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label Godown_Name = Depositor_Gridview.Rows[e.RowIndex].FindControl("Godown_Name") as Label;
        Label Godown_ID = Depositor_Gridview.Rows[e.RowIndex].FindControl("Godown_ID") as Label;
        Label BagBalance = Depositor_Gridview.Rows[e.RowIndex].FindControl("BagBalance") as Label;
        Label WeightBalance = Depositor_Gridview.Rows[e.RowIndex].FindControl("WeightBalance") as Label;
        TextBox txtPhysical_Waight_Balances = Depositor_Gridview.Rows[e.RowIndex].FindControl("txtPhysical_Waight_Balances") as TextBox;
        TextBox txtRemark = Depositor_Gridview.Rows[e.RowIndex].FindControl("txtRemark") as TextBox;

        //if (ddlflag.SelectedValue != "0")
        //{
        if (txtRemark.Text != "")
        {

            try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Ins_Godown_Wise_online_Stock_position_2022", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Branch_Id", Session["BranchId"].ToString());
                    cmd.Parameters.AddWithValue("@Godown_Name", Godown_Name.Text);
                    cmd.Parameters.AddWithValue("@Godown_Id", Godown_ID.Text);
                    cmd.Parameters.AddWithValue("@Online_Avl_Begs", BagBalance.Text);
                    cmd.Parameters.AddWithValue("@Online_Avl_Begs_Waight", WeightBalance.Text); 
                    cmd.Parameters.AddWithValue("@Physical_Waight_Balances", txtPhysical_Waight_Balances.Text); 
                    cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            fillgrid();
            //ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Record Save Successfully')", true);
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Save Successfully ..'); </script> ");

        }
        else
        {
            //ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Please Enter Remark')", true);
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Remark ..'); </script> ");
        }

    }

    //protected void Depositor_Gridview_RowCommand1(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        //  HiddenField hdnVerificationType = (HiddenField)e.Row.FindControl("hdnVerificationType");
    //        Label txtstatus = (Label)e.Row.FindControl("txtstatus");
    //        if (string.IsNullOrEmpty(txtstatus.Text))
    //        {
    //            e.Row.BackColor = ColorTranslator.FromHtml("#e9716b");//e9716b
    //        }
    //        else
    //        {
    //            e.Row.BackColor = ColorTranslator.FromHtml("#28b779");//28b779
    //        }
    //    }
    //}

   
    protected void Depositor_Gridview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //  HiddenField hdnVerificationType = (HiddenField)e.Row.FindControl("hdnVerificationType");
            Label txtstatus = (Label)e.Row.FindControl("txtstatus");
            if (txtstatus.Text== "Done")
            {
                e.Row.BackColor = ColorTranslator.FromHtml("#28b779");//e9716b
            }
            else if (txtstatus.Text == "Pending")
            {
                e.Row.BackColor = ColorTranslator.FromHtml("#e9716b");//28b779
            }
        }

    }
}