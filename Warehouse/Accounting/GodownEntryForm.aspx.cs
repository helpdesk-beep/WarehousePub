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

public partial class Accounting_GodownEntryForm : System.Web.UI.Page
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
            }
        }
        else
        {
            Response.Redirect("../Logout.aspx");
        }

     
    }

  


    protected void fill()
    {
        string query = "select District_Id,District_Name from tbl_metadata_district";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);


            DropDownList1.DataSource = ds.Tables[0];
            DropDownList1.DataTextField = "District_Name";
            DropDownList1.DataValueField = "District_Id";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("जिला चुने", "0"));
            


          
      
        if (ds.Tables[0].Rows.Count > 0)
        {
            DropDownList5.DataSource = ds.Tables[0];
            DropDownList5.DataTextField = "District_Name";
            DropDownList5.DataValueField = "District_Id";
            DropDownList5.DataBind();            
            DropDownList5.Items.Insert(0, new ListItem("जिला चुने", "0"));


          

        }

        string query1 = "select TMD.District_Name as DistName,TMDB.DepotName as Branch_Name,TSD.*  from TBl_StoredFromOther_District  TSD left join tbl_metadata_district TMD on TSD.District_ID = TMD.District_Id left join tbl_metadata_depot TMDB on TMDB.BranchId = TSD.Branch__ID where TSD.EntryBranchID = '" + Session["BranchId"].ToString() + "' ";
        SqlCommand cmd1 = new SqlCommand(query1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
       // Repeater1.DataSource = ds1.Tables[0];
       // Repeater1.DataBind();


        GridView1.DataSource = ds1.Tables[0];
        GridView1.DataBind();

        string query11 = "select TMD.District_Name as DistName,TMDB.DepotName as Branch_Name,TSD.*  from TBl_RecievedFromOther_District  TSD left join tbl_metadata_district TMD on TSD.District_ID = TMD.District_Id left join tbl_metadata_depot TMDB on TMDB.BranchId = TSD.Branch__ID where TSD.EntryBranchID = '" + Session["BranchId"].ToString() + "' ";
        SqlCommand cmd11 = new SqlCommand(query11, con);
        SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
        DataSet ds11 = new DataSet();
        da11.Fill(ds11);
       // Repeater2.DataSource = ds11.Tables[0];
       // Repeater2.DataBind();

        gvCol3.DataSource = ds11.Tables[0];
        gvCol3.DataBind();

        // Repeater2.DataSource = ds1.Tables[0];
        // Repeater2.DataBind();



    }



    protected void btn_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Sp_GodownEntry", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GDistID", DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@Branchname", ddlbranchname.SelectedValue);
                cmd.Parameters.AddWithValue("@TomarrowTotalQuintity", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@ToDayTotalQuintity", TextBox3.Text.Trim());
               // cmd.Parameters.AddWithValue("@TotalQuintity", TextBox4.Text.Trim());              
                cmd.Parameters.AddWithValue("@OWNGodown", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@adhigrahanGodown", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@JVSGodown", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@CAP", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@GODOWN", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@SECONDCAP", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@CWC", TextBox11.Text.Trim());
                cmd.Parameters.AddWithValue("@OLDFED", TextBox12.Text.Trim());
                cmd.Parameters.AddWithValue("@ENTRYBRANCHID", Session["BranchId"].ToString());                
                //cmd.Parameters.AddWithValue("@Totalsum", TextBox13.Text.Trim());
                cmd.Parameters.AddWithValue("@type", 1);
                con.Open();
                int k = cmd.ExecuteNonQuery();
                con.Close();
                TextBox1.Text = "";
                TextBox3.Text = "";
               // TextBox4.Text = "";
                TextBox2.Text = "";
                TextBox5.Text = "";
                TextBox6.Text = "";
                TextBox7.Text = "";
                TextBox9.Text = "";
                TextBox10.Text = "";
                TextBox11.Text = "";
                TextBox12.Text = "";
               // TextBox13.Text = "";
                // Session["msg"] = "Data Insert Successfully";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Insert Successfully'); </script> ");

            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Sp_GodownEntry", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@GDistID", DropDownList5.SelectedValue);
                cmd.Parameters.AddWithValue("@Branchname", hdnBranchid.Value);
                cmd.Parameters.AddWithValue("@TomarrowTotalQuintity", TextBox14.Text.Trim());
                cmd.Parameters.AddWithValue("@ToDayTotalQuintity", TextBox15.Text.Trim());
               // cmd.Parameters.AddWithValue("@TotalQuintity", TextBox16.Text.Trim());
                cmd.Parameters.AddWithValue("@OWNGodown", TextBox17.Text.Trim());
                cmd.Parameters.AddWithValue("@adhigrahanGodown", TextBox18.Text.Trim());
                cmd.Parameters.AddWithValue("@JVSGodown", TextBox19.Text.Trim());
                cmd.Parameters.AddWithValue("@CAP", TextBox21.Text.Trim());
                cmd.Parameters.AddWithValue("@GODOWN", TextBox22.Text.Trim());
                cmd.Parameters.AddWithValue("@SECONDCAP", TextBox23.Text.Trim());
                cmd.Parameters.AddWithValue("@CWC", TextBox24.Text.Trim());
                cmd.Parameters.AddWithValue("@OLDFED", TextBox25.Text.Trim());
                //cmd.Parameters.AddWithValue("@Totalsum", TextBox26.Text.Trim());
                cmd.Parameters.AddWithValue("@ENTRYBRANCHID", Session["BranchId"].ToString());
                cmd.Parameters.AddWithValue("@type", 2);
                con.Open();
                int k = cmd.ExecuteNonQuery();
                con.Close();
                TextBox14.Text = "";
                TextBox15.Text = "";
               // TextBox16.Text = "";
                TextBox17.Text = "";
                TextBox18.Text = "";
                TextBox19.Text = "";
                TextBox21.Text = "";
                TextBox22.Text = "";
                TextBox23.Text = "";
                TextBox24.Text = "";
                TextBox25.Text = "";
                //TextBox26.Text = "";
                // Session["msg"] = "Data Insert Successfully";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Insert Successfully'); </script> ");

            }
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        string query = "select DepotID,DepotName from tbl_metadata_depot where DistrictId ='" + DropDownList1.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranchname.DataSource = ds.Tables[0];
            ddlbranchname.DataTextField = "DepotName";
            ddlbranchname.DataValueField = "DepotID";
            ddlbranchname.DataBind();
            
            ddlbranchname.Items.Insert(0, new ListItem("ब्रांच चुने ", "0"));

        }
    }

    [WebMethod]
    public static string getBranchbydistidN(string distid)
    {
        string officerDetails = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
          DataTable OfficerDT = new DataTable();

        string query = "select DepotID,DepotName from tbl_metadata_depot where DistrictId ='" + distid + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(OfficerDT);
       // OfficerDT = new LogicClass().GetOfficerDetailsthana(Convert.ToInt32(distid), typeN);
        officerDetails = ConvertDataTabletoString(OfficerDT);
        return officerDetails;
    }

    public static string ConvertDataTabletoString(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

  
}
    