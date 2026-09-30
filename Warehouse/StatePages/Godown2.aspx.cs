using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class StatePages_Godown2 : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["UserName"] = "MPSWLC";
        if (Session["UserName"].ToString() == "MPSWLC" || Session["UserName"].ToString() == "Markfed") 
        {
            if (!IsPostBack)
            {
                fillgrid();
            }
        }
    }

    protected void fillgrid()
    {

        try
        {

            //string query = "";

            //query = "select R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName,Beneficiary_Id,Beneficiary_Name,Beneficiary_Type,'**' + right(Account_No, 4) as Account_No,'**' + right(IFSC_Code, 4) as IFSC_Code,GO_Approval_Statusfrom tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICTon tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id inner join tbl_MetaData_DEPOT as DP on DP.BranchId = tbl_Beneficiary_Account_Details.Branch_Id inner join tbl_MetaData_Region as R on R.Region_Id = tbl_MetaData_DISTRICT.Region_ID WHERE Region_Id='" + Session["Region_ID"].ToString() + "' order by R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName";
            //query = "select TMR.region as Region_Name, MD.District_Name,TMD.DepotName as Branch_Name, MG18.Hired_Type,MG18.Godown_ID,MG18.Godown_Name  ,MG18.Godown_Capacity as TotalGoCap,MG18.Godown_Scientific_Capacity as ScientificCapacity from tbl_MetaData_GODOWN_2018 as MG18 left join tbl_metadata_district as MD on MG18.DistrictId = MD.District_Id left join tbl_MetaData_DEPOT as TMD on MG18.BranchID = TMD.BranchId left join tbl_MetaData_Region as TMR on TMR.Region_Id = MD.Region_ID where MG18.BranchID ='" + Request.QueryString["BID"].ToString() + "' and MG18.Hired_Type = '" + Request.QueryString["TYPE"].ToString() + "'";
            SqlCommand cmdd = new SqlCommand("Detail", con);
            cmdd.CommandType = CommandType.StoredProcedure;
            //cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                gvbranch.DataSource = dt;
                gvbranch.DataBind();
                //GridView1.FooterRow.Style.Add("text-align", "center");
                //GridView1.FooterRow.Cells[6].Text = "Total";
                //GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalGoCap")).ToString();
                //GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ScientificCapacity")).ToString();
            }
            else
            {
                gvbranch.DataSource = null;
                gvbranch.DataBind();
            }
        }
        catch (Exception ex)
        {
            //////
        }
        //string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        //using (SqlConnection con = new SqlConnection(constr))
        //{
        //    using (SqlCommand cmd = new SqlCommand("Sp_Get_Lat_long", con))
        //    {
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        using (SqlDataAdapter sda = new SqlDataAdapter())
        //        {
        //            cmd.Connection = con;
        //            sda.SelectCommand = cmd;
        //            using (DataTable dt = new DataTable())
        //            {
        //                sda.Fill(dt);
        //                if (dt.Rows.Count > 0)
        //                {
        //                    gvbranch.DataSource = dt;
        //                    gvbranch.DataBind();

        //                }
        //                else
        //                {


        //                    gvbranch.DataSource = null;
        //                    gvbranch.DataBind();
        //                }
        //            }
        //        }
        //    }
        //}
    }
}