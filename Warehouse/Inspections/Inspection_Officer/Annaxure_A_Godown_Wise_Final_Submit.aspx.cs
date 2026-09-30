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
using System.Configuration;
using System;

public partial class Inspections_Reports_Annaxure_A_Godown_Wise_Final_Submit : System.Web.UI.Page
{
    public SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    string PFID = "";
    SqlTransaction sqltran;
    string client_IP = "";
    int a_id = 0;
    string Branch_ID = "";
    string Insp_ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        PFID = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            fillBranchDetails();
        }

    }
    public void fillBranchDetails()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Branch_Name_For_DF", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PFID_ID", PFID);
            con.Open();
            ddlbranch.DataSource = cmd.ExecuteReader();
            ddlbranch.DataTextField = "Depo_Name";
            ddlbranch.DataValueField = "Branch_ID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
            con.Close();
        }
    }
    public void fillGodownDetails()
    {
        using (SqlConnection con2 = new SqlConnection(constr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Name_For_DF", con2);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
            con2.Open();
            ddl_gdwn.DataSource = cmd.ExecuteReader();
            ddl_gdwn.DataTextField = "Godown_Name";
            ddl_gdwn.DataValueField = "Godown_ID";
            ddl_gdwn.DataBind();
            ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con2.Close();
        }
    }
    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Rpt_Get_Godown_Wise_Data_For_Annaxure_A_Branch_Wise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
                cmd.Parameters.AddWithValue("@Employee_ID", PFID.ToString());
                cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GrdOfficerPreviousInsp.Caption = @"<b style=""font-weight: bold;""> Inspection Officer Name: " + "  -   " + Session["UserName"].ToString() +" , " + "Branch Name: " + "  -   " + ddlbranch.SelectedItem.ToString() +"</b> ";
                            divshow.Visible = true;
                            divbtn.Visible = true;
                            GrdOfficerPreviousInsp.DataSource = dt;
                            GrdOfficerPreviousInsp.DataBind();
                            GrdOfficerPreviousInsp.FooterRow.Style.Add("text-align", "center");
                            GrdOfficerPreviousInsp.FooterRow.Cells[4].Text = "Total";
                            GrdOfficerPreviousInsp.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("AvlBags")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlQty")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("No_of_bage_in_PV")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Spilage_Bags")).ToString();
                            //GrdOfficerPreviousInsp.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Diifirence")).ToString();
                        }
                        else
                        {
                            divshow.Visible = false;
                            divbtn.Visible = false;
                            GrdOfficerPreviousInsp.DataSource = null;
                            GrdOfficerPreviousInsp.DataBind();
                            //string strMsg = "गोदाम में वर्ष " + ddlcropyear.SelectedItem.Value + " का स्टॉक उपलब्ध नहीं हैं  |";
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "')", true);
                        }
                    }
                }
            }
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodownDetails();
    }

    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            foreach (GridViewRow row in GrdOfficerPreviousInsp.Rows)
            {
                HiddenField hdnCommodity_Id = (HiddenField)row.FindControl("hdnCommodity_Id");
                HiddenField hdnDepositer_ID = (HiddenField)row.FindControl("hdnDepositer_ID");
                Label lblCapacity = (Label)row.FindControl("lblGodown_Scientific_Capacity");
                Label lblAvlBagsGS = (Label)row.FindControl("lblAvlBagsGS");
                Label lblAvlQtyGS = (Label)row.FindControl("lblAvlQtyGS");
                TextBox txtAvlBagsasPV = (TextBox)row.FindControl("txtAvlBagsasPV");
                TextBox lblSpillage_bag = (TextBox)row.FindControl("lblSpillage_bag");
                Label lblCrop_Year = (Label)row.FindControl("lblCrop_Year");
                TextBox txtDiffirance_in_PV = (TextBox)row.FindControl("txtDiffirance_in_PV");
                TextBox GtxtRemark = (TextBox)row.FindControl("GtxtRemark");

                con2.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[Annaxure_A_Entry_By_IO_Insert_New]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_Id", ddlbranch.SelectedValue);
                cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
                cmd.Parameters.AddWithValue("@Employee_ID", PFID.ToString());
                cmd.Parameters.AddWithValue("@Capasity", lblCapacity.Text);
                cmd.Parameters.AddWithValue("@Depositer_ID", hdnDepositer_ID.Value);
                cmd.Parameters.AddWithValue("@Commodity_ID", hdnCommodity_Id.Value);
                cmd.Parameters.AddWithValue("@Crop_Year", lblCrop_Year.Text);
                cmd.Parameters.AddWithValue("@AVl_Bags", lblAvlBagsGS.Text);
                cmd.Parameters.AddWithValue("@AVl_Quantity", lblAvlQtyGS.Text);
                cmd.Parameters.AddWithValue("@AVl_Bags_in_PV", txtAvlBagsasPV.Text);
                cmd.Parameters.AddWithValue("@Spilage_Bags", lblSpillage_bag.Text);
                cmd.Parameters.AddWithValue("@PV_Verification_Details_By_IO", txtDiffirance_in_PV.Text);
                cmd.Parameters.AddWithValue("@Remark", GtxtRemark.Text);
                cmd.Parameters.AddWithValue("@Inserted_By", IPAddress);
                cmd.Parameters.AddWithValue("@Insp_Date", getDate_MDY(Session["Order_Date"].ToString()));
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    count++;
                    fillScheduleInsp_Grid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "')", true);
                }
                con2.Close();

            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Annaxure A Submited Successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Annaxure A Not Submited')", true);
            }
        }
        catch (Exception ex)
        {
            //tn.Rollback();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }
}