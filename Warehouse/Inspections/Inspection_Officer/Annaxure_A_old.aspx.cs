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

public partial class Inspections_Inspection_Officer_Annaxure_A : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    string PFID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        PFID = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            //fillScheduleInsp_Grid();
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
        using (SqlConnection con2 = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Name_For_Anaxure_A", con2);
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
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodownDetails();
    }
    protected void FillGoodwnStoragecapacity()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Insp_Godown_Wise_Stirage_Details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
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
                            // divgodowncapacity.Visible = true;
                            grdstoragecapcity.DataSource = dt;
                            grdstoragecapcity.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Data_For_Annaxure_A", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
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
                            divshow.Visible = true;
                            divbtn.Visible = true;
                            GrdOfficerPreviousInsp.DataSource = dt;
                            GrdOfficerPreviousInsp.DataBind();
                            GrdOfficerPreviousInsp.FooterRow.Style.Add("text-align", "center");
                            GrdOfficerPreviousInsp.FooterRow.Cells[5].Text = "Total";
                            GrdOfficerPreviousInsp.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("AvlBags")).ToString();
                            GrdOfficerPreviousInsp.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlQty")).ToString();
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

    public void RemoveRow(string hdnGodown_ID, string hdnDepositer_ID, string hdnCommodity_Id, string lblCrop_Year)
    {
        try
        {
            int count = 0;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            conStr.Open();
            SqlCommand cmd = new SqlCommand("[dbo].[Annaxure_A_Remove_Rows]", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Godown_ID", hdnGodown_ID);
            cmd.Parameters.AddWithValue("@Depositer_ID", hdnDepositer_ID);
            cmd.Parameters.AddWithValue("@Commodity_ID", hdnCommodity_Id);
            cmd.Parameters.AddWithValue("@Crop_Year", lblCrop_Year);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
            if (TheResult.StartsWith("SUCCESS"))
            {              
                    count++;
                    fillScheduleInsp_Grid();                
            }
            conStr.Close();

            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Annaxure A Deleted Successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Annaxure A Not Delete')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
        finally
        {
            conStr.Close();
        }

    }
    protected void GrdOfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];

            //Fetch value of Name.
            string hdnGodown_ID = (row.FindControl("hdnGodown_ID") as HiddenField).Value;
            string hdnDepositer_ID = (row.FindControl("hdnDepositer_ID") as HiddenField).Value;
            string hdnCommodity_Id = (row.FindControl("hdnCommodity_Id") as HiddenField).Value;
            string lblCrop_Year = (row.FindControl("lblCrop_Year") as Label).Text;
            Session["hdnGodown_ID"] = hdnGodown_ID.ToString();
            Session["hdnDepositer_ID"] = hdnDepositer_ID.ToString();
            Session["hdnCommodity_Id"] = hdnCommodity_Id.ToString();
            Session["lblCrop_Year"] = lblCrop_Year.ToString();
            RemoveRow(hdnGodown_ID, hdnDepositer_ID, hdnCommodity_Id, lblCrop_Year);

        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        FillGoodwnStoragecapacity();
        fillScheduleInsp_Grid();
        FillGodownStorageCapacitygdnWise();
    }
    public void FillGodownStorageCapacitygdnWise()
    {
        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        foreach (GridViewRow row in grdstoragecapcity.Rows)
        {
            HiddenField hdnCommodity_Id = (HiddenField)row.FindControl("hdnCommodity_Id");
            HiddenField hdnDepositer_ID = (HiddenField)row.FindControl("hdnDepositorID");
            Label lblCropYear = (Label)row.FindControl("lblCropYear");
            Label lblAvlBags = (Label)row.FindControl("lblAvlBags");
            Label lblAvlQty = (Label)row.FindControl("lblAvlQty");
            Label lblDepositor_Name = (Label)row.FindControl("lblDepositor_Name");
            Label lblCommodity = (Label)row.FindControl("lblCommodity");

            conStr.Open();
            SqlCommand cmd = new SqlCommand("[dbo].[Godown_Storage_Capacity_for_Annaxure_A_Insert]", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_Id", ddlbranch.SelectedValue);
            cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
            cmd.Parameters.AddWithValue("@Depositer_ID", hdnDepositer_ID.Value);
            cmd.Parameters.AddWithValue("@Depositor_Name", lblDepositor_Name.Text);
            cmd.Parameters.AddWithValue("@Commodity_ID", hdnCommodity_Id.Value);
            cmd.Parameters.AddWithValue("@Commodity_Name", lblCommodity.Text);
            cmd.Parameters.AddWithValue("@Crop_Year", lblCropYear.Text);
            cmd.Parameters.AddWithValue("@No_Of_Bags", lblAvlBags.Text);
            cmd.Parameters.AddWithValue("@Quantity", lblAvlQty.Text);
            cmd.Parameters.AddWithValue("@Inserted_By", IPAddress);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
            if (TheResult.StartsWith("SUCCESS"))
            {
                //count++;
                //fillScheduleInsp_Grid();
            }

            conStr.Close();

        }
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
                Label lblSpillage_bag = (Label)row.FindControl("lblSpillage_bag");
                Label lblCrop_Year = (Label)row.FindControl("lblCrop_Year");
                TextBox txtDiffirance_in_PV = (TextBox)row.FindControl("txtDiffirance_in_PV");
                TextBox GtxtRemark = (TextBox)row.FindControl("GtxtRemark");

                conStr.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[Annaxure_A_Entry_By_IO_Insert_New]", conStr);
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
                conStr.Close();

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