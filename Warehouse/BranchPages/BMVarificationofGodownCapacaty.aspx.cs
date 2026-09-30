using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web;

public partial class Accounting_BMVarificationofGodownCapacaty : System.Web.UI.Page
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
                fill();
                filldistict();

            }
        }
        else
        {
            Response.Redirect("../Logout.aspx");
        }
    }
    protected void fill()
    {
        //string query1 = "select * from tbl_Godown_Vacant_Capacity_Rabi2022 where BranchId = '" + Session["BranchId"].ToString() + "' and Vacant_Capacity_BYBM is null and Closing_Balance is null ";
        //string query1 = "select *,case when Godown_Flag is null then 'Panding' when Godown_Flag='Y' then N'सहमत' else N'असहमत' end as  Godown_Flagmark from MPSCSC.dbo.tbl_Godown_Vacant_Capacity_Rabi2022 where BranchId = '" + Session["BranchId"].ToString() + "'";
        //string query1 = "select wms.District_Id,wms.District_Name,wms.BranchId,wms.Branch,wms.Godown_Name,wms.GodownID,wms.Hired_Type,wms.Godown_Scientific_Capacity,wms.Godown_Max_Capacity,CSMS.Vacant_Capacity_Dynamic,wms.Latitude,wms.Longitude,wms.Vacant_Capacity_BYBM,wms.Unload_Capacity,wms.Godown_Flag,wms.Offered_Dist_Type,wms.Offered_Dist_ID,wms.Insert_Date,wms.Closing_Balance,wms.UpdateDate,wms.UpdatedBY,case when wms.Godown_Flag is null then 'Panding' when wms.Godown_Flag='Y'then N'सहमत' else N'असहमत' end as  Godown_Flagmark from MPSCSC.dbo.tbl_Godown_Vacant_Capacity_Rabi2022 as CSMS inner join  tbl_Godown_Vacant_Capacity_Rabi2022 as wms on CSMS.GodownID=wms.GodownID  where wms.BranchId = '" + Session["BranchId"].ToString() + "'";
        string query1 = "select wms.District_Id,wms.District_Name,wms.BranchId,wms.Branch,wms.Godown_Name,wms.GodownID,wms.Hired_Type,wms.Godown_Scientific_Capacity,wms.Godown_Max_Capacity,CSMS.Vacant_Capacity_Dynamic,wms.Latitude,wms.Longitude,wms.Vacant_Capacity_BYBM,wms.Unload_Capacity,wms.Godown_Flag,wms.Offered_Dist_Type,wms.Offered_Dist_ID,wms.Insert_Date,wms.Closing_Balance,wms.UpdateDate,wms.UpdatedBY,case when wms.Godown_Flag is null then 'Panding' when wms.Godown_Flag='Y'then N'सहमत' else N'असहमत' end as  Godown_Flagmark from MPSCSC.dbo.tbl_Godown_Vacant_Capacity_Rabi2023 as CSMS inner join  tbl_Godown_Vacant_Capacity_Rabi2023 as wms on CSMS.GodownID=wms.GodownID  where wms.BranchId = '" + Session["BranchId"].ToString() + "'";

        SqlCommand cmd1 = new SqlCommand(query1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        
        da1.Fill(ds1);

        if(ds1.Tables.Count>0)
        {
        GridView1.DataSource = ds1.Tables[0];
        GridView1.DataBind();
      

        }

    }



    protected void show_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShowGodownvarificationcap.aspx");
    }

    protected void Edit(object sender, EventArgs e)
    {
        //string query1 = "select District_Id,BranchId,GodownID,Godown_Name from tbl_Godown_Vacant_Capacity_Rabi2022 where BranchId = '" + Session["BranchId"].ToString() + "' ";
        string query1 = "select District_Id,BranchId,GodownID,Godown_Name from tbl_Godown_Vacant_Capacity_Rabi2023 where BranchId = '" + Session["BranchId"].ToString() + "' ";
        SqlCommand cmd1 = new SqlCommand(query1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        if(ds1.Tables.Count>0)
        {

    
            Hiddendistid.Value = ds1.Tables[0].Rows[0]["District_Id"].ToString();
            Hiddenbranch.Value = ds1.Tables[0].Rows[0]["BranchId"].ToString();
          
            //Label8.Text= ds1.Tables[0].Rows[0]["GodownID"].ToString();
           // Label9.Text= ds1.Tables[0].Rows[0]["Godown_Name"].ToString();         
          
        }
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            hdngdnid.Value = row.Cells[1].Text;
            Label8.Text = row.Cells[1].Text;
            Label9.Text = row.Cells[2].Text;
            //lblGodownID.Text = row.Cells[1].Text;
           // txtVacantCapacity.Text = row.Cells[4].Text;
           // txtUnloadCapacity.Text = row.Cells[5].Text;
        }

        //string query2 = "select Latitude,Longitude from tbl_Godown_Vacant_Capacity_Rabi2022 where GodownID = '" + hdngdnid.Value + "' ";

        string query2 = "select Latitude,Longitude from tbl_Godown_Vacant_Capacity_Rabi2023 where GodownID = '" + hdngdnid.Value + "' ";
        SqlCommand cmd2 = new SqlCommand(query2, con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables.Count > 0)
        {


            TxtLatitude.Text = ds2.Tables[0].Rows[0]["Latitude"].ToString();
            TextLongitude.Text = ds2.Tables[0].Rows[0]["Longitude"].ToString();

            //Label8.Text= ds1.Tables[0].Rows[0]["GodownID"].ToString();
            // Label9.Text= ds1.Tables[0].Rows[0]["Godown_Name"].ToString();        

        }

        popup.Show();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            
            if (OfferDistType.SelectedValue == "O")
            {
                EnterHiddendistid.Value = ddldistict.SelectedValue;
            }

            if (TxtLatitude.Text != "" || TextLongitude.Text != "")
            {


                if (Godownflag.SelectedValue != "0" && OfferDistType.SelectedValue != "0")
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                    //SqlCommand cmd = new SqlCommand("Sp_Godown_Backend_Capacity2022", con);
                    SqlCommand cmd = new SqlCommand("Sp_Godown_Vacant_Capacity2023", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DistID", Hiddendistid.Value);
                    cmd.Parameters.AddWithValue("@Branch_ID", Hiddenbranch.Value);
                    cmd.Parameters.AddWithValue("@Godown_ID", hdngdnid.Value);
                    cmd.Parameters.AddWithValue("@GodownVacantCapacity", txtVacantCapacity.Text);
                    cmd.Parameters.AddWithValue("@GodownUnloadCapacity", txtUnloadCapacity.Text);
                    cmd.Parameters.AddWithValue("@Godown_Flag", Godownflag.SelectedValue);
                    cmd.Parameters.AddWithValue("@Dist_type", OfferDistType.SelectedValue);
                    cmd.Parameters.AddWithValue("@OfferedDistID", EnterHiddendistid.Value);
                    cmd.Parameters.AddWithValue("@GodownClosingBalance", ClosingBalance.Text);

                    cmd.Parameters.AddWithValue("@Latitude", TxtLatitude.Text);
                    cmd.Parameters.AddWithValue("@Longitude", TextLongitude.Text);
                    cmd.Parameters.AddWithValue("@insert_By", Request.UserHostAddress);
                    //cmd.Parameters.AddWithValue("@IP", ip);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    fill();
                    lblmsg.Text = "Godown Update Data Successfully";
                    ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Godown Update Data Successfully')", true);

                }
                else
                {
                    lblmsg.Text = "Select DropDown Values..... ";
                }
            }
            else 
            {
                lblmsg.Text = "Please Fill Latitude and Longitude..... ";
            }
        }

        catch (Exception ex)
        {
            lblmsg.Text = "Select All Fields..... ";
            Console.WriteLine(ex.Message);
        }
    }

    protected void OfferDistType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(OfferDistType.SelectedValue=="S")
        {
            EnterHiddendistid.Value = Hiddendistid.Value;
            // OfferedDistID.Enabled = false;
            ddldistict.Enabled = false;
            popup.Show();
        }
        else 
        {
            ddldistict.Enabled = true;
            popup.Show();
        }

    }

    protected void filldistict()
    {
        string query = "select District_Id,District_Name from tbl_metadata_district";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);


        ddldistict.DataSource = ds.Tables[0];
        ddldistict.DataTextField = "District_Name";
        ddldistict.DataValueField = "District_Id";
        ddldistict.DataBind();
        ddldistict.Items.Insert(0, new ListItem("जिला चुने", "0"));
    }


    }