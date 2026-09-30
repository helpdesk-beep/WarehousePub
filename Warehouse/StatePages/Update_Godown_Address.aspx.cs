using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Update_Godown_Address : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            if (!IsPostBack)
            {
                fillRegion();
            }
        }
    }
    private void fillRegion()
    {
        try
        {
            string str = "SELECT Distinct [Region_ID],[Regionnm] FROM [tbl_MetaData_DISTRICT] order by Regionnm asc";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlregion.DataSource = ds.Tables[0];
                ddlregion.DataTextField = "Regionnm";
                ddlregion.DataValueField = "Region_ID";
                ddlregion.DataBind();
                ddlregion.Items.Insert(0, "Select");
            }
            else
            {
                ddlregion.Items.Clear();
            }

        }

        catch (Exception ex)
        {

        }
    }
    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDistrict();
        FillGrid();
    }
    private void fillDistrict()
    {
        try
        {
            string str = "SELECT District_Id,District_Name FROM [tbl_MetaData_DISTRICT] Where Region_ID='" + ddlregion.SelectedValue + "' order by District_Name asc";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldistrct.DataSource = ds.Tables[0];
                ddldistrct.DataTextField = "District_Name";
                ddldistrct.DataValueField = "District_Id";
                ddldistrct.DataBind();
                ddldistrct.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddldistrct.Items.Clear();
            }

        }

        catch (Exception ex)
        {

        }
    }
    protected void ddldistrct_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranch();
        FillGrid();
    }
    private void fillBranch()
    {
        try
        {
            string str = "SELECT BranchId,DepotName FROM tbl_MetaData_DEPOT Where DistrictId='" + ddldistrct.SelectedValue + "' order by DepotName asc";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbranch.DataSource = ds.Tables[0];
                ddlbranch.DataTextField = "DepotName";
                ddlbranch.DataValueField = "BranchId";
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlbranch.Items.Clear();
            }

        }

        catch (Exception ex)
        {

        }
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodown();
        FillGrid();
    }
    private void fillGodown()
    {
        try
        {
            string query = "";
            query = "Select Godown_ID,Godown_Name from tbl_MetaData_GODOWN Where BranchID ='" + ddlbranch.SelectedValue + "' Order By Godown_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodownName.DataSource = ds.Tables[0];
                ddlgodownName.DataTextField = "Godown_Name";
                ddlgodownName.DataValueField = "Godown_ID";
                ddlgodownName.DataBind();
                ddlgodownName.Items.Insert(0, "Select");
            }
            else
            {
                ddlgodownName.Items.Clear();
                ddlgodownName.Items.Insert(0, "Select");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            //checkvalidation();
            string ErrorMsg = "";
            ErrorMsg += ddlgodownName.SelectedIndex > 0 ? "" : "Please Select Godown... \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtaddress.Text) ? "" : "Enter Godown Address. \\n";
            if (ErrorMsg == "")
            {
                if (btnsave.Text == "Save")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Update_Godown_Address", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Godown_ID", ddlgodownName.SelectedValue);
                    cmd.Parameters.AddWithValue("@Godown_Address", txtaddress.Text);
                    cmd.Parameters.AddWithValue("@Licence_Type", ddllicence.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Session["State_Logid"].ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Godown Address Update Successfully|||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        FillGrid();
                        TextClear();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
                        TextClear();
                    }
                }
                if (btnsave.Text == "Update")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Update_Godown_Address", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Godown_ID", ddlgodownName.SelectedValue);
                    cmd.Parameters.AddWithValue("@Godown_Address", txtaddress.Text);
                    cmd.Parameters.AddWithValue("@Licence_Type", ddllicence.SelectedValue);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Session["State_Logid"].ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Godown Address Update Successfully|||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        FillGrid();
                        TextClear();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
                        TextClear();
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' " + ErrorMsg.ToString() + "')", true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    protected void TextClear()
    {
        ddlgodownName.ClearSelection();
        ddllicence.ClearSelection();
        txtaddress.Text = "";
    }
    protected void FillGrid()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Usp_Select_Godown_Details_For_State", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddlregion.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@RegionID", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RegionID", ddlregion.SelectedValue);
            }
            if (ddldistrct.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@DistrictId", '0');
            }
            else
            {
                cmd.Parameters.AddWithValue("@DistrictId", ddldistrct.SelectedValue);
            }
            if (ddlbranch.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@BranchID", '0');
            }
            else
            {
                cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                Grdgdn.DataSource = dt;
                Grdgdn.DataBind();
            }
            else
            {
                Grdgdn.DataSource = null;
                Grdgdn.DataBind();
            }
        }
    }
    protected void Grdgdn_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRecord")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblRowNumber = (Label)row.FindControl("lblRowNumber");
            Label lblRegion_ID = (Label)row.FindControl("lblRegion_ID");
            Label lblDistrictId = (Label)row.FindControl("lblDistrictId");
            Label lblBranchID = (Label)row.FindControl("lblBranchID");
            Label lblGodown_ID = (Label)row.FindControl("lblGodown_ID");
            Label lblGodown_Address = (Label)row.FindControl("lblGodown_Address");
            Label lbllicence = (Label)row.FindControl("lbllicence");
            ViewState["Godown_ID"] = lblRowNumber.Text;
            if (e.CommandName == "EditRecord")
            {
                ddlregion.SelectedValue = lblRegion_ID.Text;
                ddldistrct.SelectedValue = lblDistrictId.Text;
                ddlbranch.SelectedValue = lblBranchID.Text;
                ddlgodownName.SelectedValue = lblGodown_ID.Text;
                ddllicence.SelectedValue = lbllicence.Text;
                txtaddress.Text = lblGodown_Address.Text;
            }
            btnsave.Text = "Update";
        }
    }



}