using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Kharif2020_21_fr_WHR_Insert_CSMS : System.Web.UI.Page
{
   // SqlConnection con_CSMS = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMS"].ToString());
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    string maxdate;
    string District_Id = "";
    string BranchID = "";
    string Purchase_Center = "";
    string Commodity_Id = "";
    int Qty_Rvd_No_of_Bags = 0;
    decimal Qty_Rvd_Weight = 0;
    string Depositor_Form_No = "";
    string Godown = "";
    string WHR_Id = "";
    string Acceptance_No = "";
    string Crop_Year = "";
    string CreatedDate = "";
    string WHR_CreatedDate="";
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_msg.Text = "";
        lbl_maxtcrtdate.Text = "";
    }

    public void GetMaxCreatedDate()
    {
        //code for get max Created date from WPMS2020.dbo.tbl_WHR_Rabi2020
        try
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            //string qry = "Select convert(varchar(20),max(CreatedDate),105) as maxdt from WPMS2020.dbo.tbl_WHR_Rabi2020";
            string qry = "Select max(Created_Date) as maxdt from csms.dbo.tbl_WHR_Accnote_Kharif2021_Storage";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandTimeout = 0;
            DataSet ds = new DataSet();
            da.Fill(ds);

            maxdate = ds.Tables[0].Rows[0]["maxdt"].ToString();
            lbl_maxtcrtdate.Text = maxdate;

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        catch (Exception ex)
        {
            lbl_msg.Text = ex.Message;
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }

    public void Select_Rec()
    {
        try
        {

            //code for with max created date
            if (maxdate == "")
            {
                lbl_maxtcrtdate.Text = "Max created date is Null";

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                SqlCommand cmd = new SqlCommand("[SP_WHR_Kharif2020_without_maxdate_csms]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@Max_Created_Date", MaxCreatedDate);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 0;
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;

                da.Fill(ds);

                grdview_data.DataSource = ds;
                grdview_data.DataBind();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {

                        District_Id = ds.Tables[0].Rows[i]["District_Id"].ToString();
                        BranchID = ds.Tables[0].Rows[i]["BranchID"].ToString();
                        Purchase_Center = ds.Tables[0].Rows[i]["Purchase_Center"].ToString();
                        Commodity_Id = ds.Tables[0].Rows[i]["Commodity_Id"].ToString();
                        Qty_Rvd_No_of_Bags = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty_Rvd_No_of_Bags"]);
                        Qty_Rvd_Weight = Convert.ToDecimal(ds.Tables[0].Rows[i]["Qty_Rvd_Weight"]);
                        Depositor_Form_No = ds.Tables[0].Rows[i]["Depositor_Form_No"].ToString();
                        Godown = ds.Tables[0].Rows[i]["Godown"].ToString();
                        WHR_Id = ds.Tables[0].Rows[i]["WHR_Id"].ToString();
                        Acceptance_No = ds.Tables[0].Rows[i]["Acceptance_No"].ToString();
                        Crop_Year = ds.Tables[0].Rows[i]["Crop_Year"].ToString();
                        WHR_CreatedDate = ds.Tables[0].Rows[i]["CreatedDate"].ToString();

                        // SqlCommand cmd1 = new SqlCommand("PPMS2020.dbo.Insert_tbl_WHR_Kharif2020", con_WPMS);
                        SqlCommand cmd1 = new SqlCommand("csms.dbo.[SP_Insert_WHR_Kharif2020_21]", con);
                        cmd1.CommandType = CommandType.StoredProcedure;

                        cmd1.Parameters.AddWithValue("District_Id", District_Id);
                        cmd1.Parameters.AddWithValue("BranchID", BranchID);
                        cmd1.Parameters.AddWithValue("Purchase_Center", Purchase_Center);
                        cmd1.Parameters.AddWithValue("Commodity_Id", Commodity_Id);
                        cmd1.Parameters.AddWithValue("Qty_Rvd_No_of_Bags", Qty_Rvd_No_of_Bags);
                        cmd1.Parameters.AddWithValue("Qty_Rvd_Weight", Qty_Rvd_Weight);
                        cmd1.Parameters.AddWithValue("Depositor_Form_No", Depositor_Form_No);
                        cmd1.Parameters.AddWithValue("Godown", Godown);
                        cmd1.Parameters.AddWithValue("WHR_Id", WHR_Id);
                        cmd1.Parameters.AddWithValue("Acceptance_No", Acceptance_No);
                        cmd1.Parameters.AddWithValue("Crop_Year", Crop_Year);
                        cmd1.Parameters.AddWithValue("WHR_CreatedDate", WHR_CreatedDate);


                        if (con.State == ConnectionState.Closed) { con.Open(); }
                        cmd1.ExecuteNonQuery();
                        if (con.State == ConnectionState.Open) { con.Close(); }

                        lbl_msg.Text = "Records Added Successfully";
                        lbl_rowcount.Text = ds.Tables[0].Rows.Count.ToString();

                    }
                }
                else
                {
                    lbl_rowcount.Visible = false;
                    lbl_msg.Text = "Records not found";
                }
            }

            //code for max created date is null
            else
            {
                DateTime MaxCreatedDate;
                MaxCreatedDate = Convert.ToDateTime(maxdate);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd2 = new SqlCommand("SP_WHR_Kharif2020_csms", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@Max_Created_Date", MaxCreatedDate);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                cmd2.CommandTimeout = 0;
                DataSet ds2 = new DataSet();
                da2.SelectCommand = cmd2;

                da2.Fill(ds2);

                grdview_data.DataSource = ds2;
                grdview_data.DataBind();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j <= ds2.Tables[0].Rows.Count - 1; j++)
                    {
                        District_Id = ds2.Tables[0].Rows[j]["District_Id"].ToString();
                        BranchID = ds2.Tables[0].Rows[j]["BranchID"].ToString();
                        Purchase_Center = ds2.Tables[0].Rows[j]["Purchase_Center"].ToString();
                        Commodity_Id = ds2.Tables[0].Rows[j]["Commodity_Id"].ToString();
                        Qty_Rvd_No_of_Bags = Convert.ToInt32(ds2.Tables[0].Rows[j]["Qty_Rvd_No_of_Bags"]);
                        Qty_Rvd_Weight = Convert.ToDecimal(ds2.Tables[0].Rows[j]["Qty_Rvd_Weight"]);
                        Depositor_Form_No = ds2.Tables[0].Rows[j]["Depositor_Form_No"].ToString();
                        Godown = ds2.Tables[0].Rows[j]["Godown"].ToString();
                        WHR_Id = ds2.Tables[0].Rows[j]["WHR_Id"].ToString();
                        Acceptance_No = ds2.Tables[0].Rows[j]["Acceptance_No"].ToString();
                        Crop_Year = ds2.Tables[0].Rows[j]["Crop_Year"].ToString();
                        WHR_CreatedDate = ds2.Tables[0].Rows[j]["CreatedDate"].ToString();

                        //SqlCommand cmd3 = new SqlCommand("PPMS2020.dbo.Insert_tbl_WHR_Kharif2020", con_WPMS);
                        SqlCommand cmd3 = new SqlCommand("csms.dbo.[SP_Insert_WHR_Kharif2020_21]", con);
                        cmd3.CommandType = CommandType.StoredProcedure;

                        cmd3.Parameters.AddWithValue("District_Id", District_Id);
                        cmd3.Parameters.AddWithValue("BranchID", BranchID);
                        cmd3.Parameters.AddWithValue("Purchase_Center", Purchase_Center);
                        cmd3.Parameters.AddWithValue("Commodity_Id", Commodity_Id);
                        cmd3.Parameters.AddWithValue("Qty_Rvd_No_of_Bags", Qty_Rvd_No_of_Bags);
                        cmd3.Parameters.AddWithValue("Qty_Rvd_Weight", Qty_Rvd_Weight);
                        cmd3.Parameters.AddWithValue("Depositor_Form_No", Depositor_Form_No);
                        cmd3.Parameters.AddWithValue("Godown", Godown);
                        cmd3.Parameters.AddWithValue("WHR_Id", WHR_Id);
                        cmd3.Parameters.AddWithValue("Acceptance_No", Acceptance_No);
                        cmd3.Parameters.AddWithValue("Crop_Year", Crop_Year);
                        cmd3.Parameters.AddWithValue("WHR_CreatedDate", WHR_CreatedDate);

                        if (con.State == ConnectionState.Closed) { con.Open(); }
                        cmd3.ExecuteNonQuery();
                        if (con.State == ConnectionState.Open) { con.Close(); }

                        lbl_msg.Text = "Records Added Successfully";
                        lbl_rowcount.Text = ds2.Tables[0].Rows.Count.ToString();

                    }
                }
                else
                {
                    lbl_rowcount.Visible = false;
                    lbl_msg.Text = "Records not found";
                }
            }
        }
        catch (Exception ex)
        {
            lbl_msg.Text = ex.Message;
        }
        finally
        {
            con.Close();
           // con_CSMS.Close();
        }
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        GetMaxCreatedDate();
        Select_Rec();
    }
}