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
using System.Collections.Generic;

public partial class StatePages_Update_Old_Godown_Lat_Long : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            GetDistrict();
            FillGrid();
        }
    }
    public void GetDistrict()
    {
        string qry = "";
        qry = "select distinct District_Id,District_Name from tbl_MetaData_DISTRICT Order By District_Name ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void GetBranch()
    {
        string qry = "";
        qry = "select distinct DepotID,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "DepotID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }
    protected void FillGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Update_Lat_Long_For_State", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
                cmd.Parameters.AddWithValue("@DistrictID", ddldistrict.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            grdlatlong.DataSource = dt;
                            grdlatlong.DataBind();
                            divgrd.Visible = true;
                        }
                        else
                        {
                            grdlatlong.DataSource = null;
                            grdlatlong.DataBind();
                            divgrd.Visible = false;
                        }
                    }
                }
            }
        }
    }

    protected void grdlatlong_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "updateRow")
        {
            string ipAddress;
            ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipAddress == "" || ipAddress == null)
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            Label RowNumber = (Label)row.FindControl("lblRowNumber");
            string Godown_Name = (row.FindControl("lblGodown_Name") as Label).Text;
            string Godown_ID = (row.FindControl("hdnGodown_ID") as HiddenField).Value;
            string Latitude = (row.FindControl("txtLatitude") as TextBox).Text;
            string Longitude = (row.FindControl("txtLongitude") as TextBox).Text;
            if (Latitude != "")
            {
                if (Longitude != "")
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Update_Old_Godown_Lat_Long", con1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con1.Open();
                    cmd.Parameters.AddWithValue("@Godown_ID", Godown_ID);
                    cmd.Parameters.AddWithValue("@Lat", Latitude);
                    cmd.Parameters.AddWithValue("@Long", Longitude);
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Latitude And Longitude Update Succesfully |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        ///TextClear();
                        FillGrid();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Feel Longitude!');", true);
                    //TextClear();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Feel Latitude!');", true);
                //TextClear();
            }
        }
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
}