using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Resources;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;
using System.Net;
using System.Net.Sockets;

public partial class BranchPages_Delete_DCC_Wise_Stock_Entry : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (!IsPostBack)
            {
                fillGodownType();
                fillCropYear();
                GetCommodityType();
                GetDepositorType();
                fillGodownType();
            }
        }
        else
        {
            Response.Redirect("../login.aspx");
        }
    }
    private void fillGodownType()
    {
        try
        {

            string query = "";
            query = "Select  Distinct Hired_Type from tbl_MetaData_GODOWN_2018 Order By Hired_Type";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodowntype.Items.Clear();
                ddlgodowntype.DataSource = ds.Tables[0];
                ddlgodowntype.DataTextField = "Hired_Type";
                ddlgodowntype.DataValueField = "Hired_Type";
                ddlgodowntype.DataBind();
                ddlgodowntype.Items.Insert(0, "--Select--");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlgodowntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodown();
    }
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void fillGodown()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 Where BranchID ='" + Session["BranchId"].ToString() + "' And Hired_Type ='" + ddlgodowntype.SelectedItem.Text + "' Order By Godown_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodown.Items.Clear();
                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
                ddlGodown.Items.Insert(0, "--Select--");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void fillCropYear()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            //query = "Select distinct CropYear from tbl_storage_Depositor_WHR_Relation where CropYear not in('All','Before 2009','Before 2013','Before 2014','','--Select--','0')";
            query = "Select Crop_Year from tbl_MetaData_Crop_Year Order By Crop_Year ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcropyear.Items.Clear();
                ddlcropyear.DataSource = ds.Tables[0];
                ddlcropyear.DataTextField = "Crop_Year";
                ddlcropyear.DataValueField = "Crop_Year";
                ddlcropyear.DataBind();
                ddlcropyear.Items.Insert(0, "--Select--");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    public void GetCommodityType()
    {
        string qry = "";
        qry = "Select Comm_Group_id,Group_name from tbl_MetaData_STORAGE_COMMODITY_Group Order By Group_name ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlCommoditytype.DataSource = ds.Tables[0];
            ddlCommoditytype.DataTextField = "Group_name";
            ddlCommoditytype.DataValueField = "Comm_Group_id";
            ddlCommoditytype.DataBind();
            ddlCommoditytype.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void GetCommodity()
    {
        string qry = "";
        qry = "select Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY Where  Rep_Grp_Code='" + ddlCommoditytype.SelectedValue + "' order by Commodity_Name asc";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    protected void ddlCommoditytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCommodity();
    }
    private void GetDepositorType()
    {
        string qry = "";
        qry = "select Depositor_Type_Id,Depositor_Type from tbl_MetaData_Depositor_Type order by Depositor_Type";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDepositorType.DataSource = ds.Tables[0];
            ddlDepositorType.DataTextField = "Depositor_Type";
            ddlDepositorType.DataValueField = "Depositor_Type_Id";
            ddlDepositorType.DataBind();
            ddlDepositorType.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    private void GetDepositor()
    {

        if (ddlDepositorType.SelectedItem.Text == "Institution")
        {
            //For Institution
            string query2 = "";
            query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679','181','15478')";

            //}
            SqlCommand cmd2 = new SqlCommand(query2, con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddlDepositor.DataSource = ds2;
                ddlDepositor.DataTextField = "Depositor_Name";
                ddlDepositor.DataValueField = "Depositor_ID";
                ddlDepositor.DataBind();
                ddlDepositor.Items.Insert(0, "--Select--");
                //ddlDepositor.SelectedValue = "129";
            }
            //For Institution

        }
        else if (ddlDepositorType.SelectedItem.Text == "Jabti")
        {
            //For Institution
            string query2 = "";
            query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE DepositorType_ID ='" + ddlDepositorType.SelectedValue + "'";

            //}
            SqlCommand cmd2 = new SqlCommand(query2, con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddlDepositor.DataSource = ds2;
                ddlDepositor.DataTextField = "Depositor_Name";
                ddlDepositor.DataValueField = "Depositor_ID";
                ddlDepositor.DataBind();
                ddlDepositor.Items.Insert(0, "--Select--");
                //ddlDepositor.SelectedValue = "129";
            }
            //For Institution

        }
        else
        {
            string depositer = ddlDepositorType.SelectedValue.ToString().Trim();
            string qry = "";
            qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE DepositorType_ID ='" + ddlDepositorType.SelectedValue + "' AND BranchId='" + Session["BranchId"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepositor.DataSource = ds.Tables[0];
                ddlDepositor.DataTextField = "Depositor_Name";
                ddlDepositor.DataValueField = "Depositor_ID";
                ddlDepositor.DataBind();
                ddlDepositor.Items.Insert(0, new ListItem("All", "0"));
            }
        }
    }
    protected void ddlDepositorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepositor();
    }
    protected void fillGrid()
    {
        string ErrorMsg = "";
        DataSet ds = new DataSet();
        if (ErrorMsg == "")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Get_Stock_Dcc_Details_For_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            //cmd.Parameters.AddWithValue("@DepotID", ddlbranch.SelectedValue.ToString());
            //cmd.Parameters.AddWithValue("@HiredType", ddlgodowntype.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Depositor_ID", ddlDepositor.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedItem.Text);
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                sda.Fill(ds);
                DataTable MainTable = ds.Tables[0];
                if (MainTable.Rows.Count > 0)
                {
                    GV_EntryDone.DataSource = MainTable;
                    GV_EntryDone.DataBind();
                    GV_EntryDone.FooterRow.Style.Add("text-align", "right");
                }
                else
                {
                    GV_EntryDone.DataSource = null;
                    GV_EntryDone.DataBind();
                }

            }
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void GV_EntryDone_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GV_EntryDone.Rows[rowIndex];

            //Fetch value of Name.
            string hdnGodown_ID = (row.FindControl("hdnGodown_ID") as HiddenField).Value;
            string hdnDepositor_ID = (row.FindControl("hdnDepositor_ID") as HiddenField).Value;
            string hdnCommodity_Id = (row.FindControl("hdnCommodity_Id") as HiddenField).Value;
            string hdnCrop_Year = (row.FindControl("hdnCrop_Year") as HiddenField).Value;
            Session["hdnGodown_ID"] = hdnGodown_ID.ToString();
            Session["hdnDepositor_ID"] = hdnDepositor_ID.ToString();
            Session["hdnCommodity_Id"] = hdnCommodity_Id.ToString();
            Session["hdnCrop_Year"] = hdnCrop_Year.ToString();
            RemoveRow(hdnGodown_ID.ToString(), hdnDepositor_ID.ToString(), hdnCommodity_Id.ToString(), hdnCrop_Year.ToString());

        }
    }
    public void RemoveRow(string Godown_ID, string Depositor_ID, string Commodity_Id, string Crop_Year)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmd1 = new SqlCommand();
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            SqlCommand cmd = new SqlCommand("usp_Delete_Dcc_Stock_Entry", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Godown_ID", Godown_ID);
            cmd.Parameters.AddWithValue("@Depositor_ID", Depositor_ID);
            cmd.Parameters.AddWithValue("@Commodity_Id", Commodity_Id);
            cmd.Parameters.AddWithValue("@Crop_Year", Crop_Year);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Deleted Successfully!');", true);
                //fillDetailsInGrid();
                fillGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Deleted!');", true);
            }

        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }
}