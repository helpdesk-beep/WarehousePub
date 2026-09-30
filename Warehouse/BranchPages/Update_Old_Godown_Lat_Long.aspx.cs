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

public partial class BranchPages_Update_Old_Godown_Lat_Long : System.Web.UI.Page
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
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (!IsPostBack)
            {
                //fillRegion();
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("../login.aspx");
        }
    }
    protected void FillGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Update_Lat_Long", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                            grdlatlong.DataSource = dt;
                            grdlatlong.DataBind();
                        }
                        else
                        {
                            grdlatlong.DataSource = null;
                            grdlatlong.DataBind();
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
}