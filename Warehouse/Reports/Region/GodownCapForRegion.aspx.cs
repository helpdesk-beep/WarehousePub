using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;


public partial class Reports_Region_GodownCapForRegion : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] != null)
            {
                if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
                {
                    if (!IsPostBack)
                    {
                        fillgrid();
                    }
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }

        }


    }

    private void fillgrid()
    {
        try
        {
            string query1 = "select District_Id, District_Name from tbl_metadata_district MD left  join tbl_MetaData_Region MR on MR.Region_Id = MD.Region_ID where MD.Region_ID ='" + Session["Region_ID"].ToString() + "'";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {

                ddldist.DataSource = ds1.Tables[0];
                ddldist.DataTextField = "District_Name";
                ddldist.DataValueField = "District_Id";
                ddldist.DataBind();
                ddldist.Items.Insert(0, new ListItem("जिला चुने", "0"));

            }


            string query = "";
            //query = "select R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName,Beneficiary_Id,Beneficiary_Name,Beneficiary_Type,'**' + right(Account_No, 4) as Account_No,'**' + right(IFSC_Code, 4) as IFSC_Code,GO_Approval_Statusfrom tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICTon tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id inner join tbl_MetaData_DEPOT as DP on DP.BranchId = tbl_Beneficiary_Account_Details.Branch_Id inner join tbl_MetaData_Region as R on R.Region_Id = tbl_MetaData_DISTRICT.Region_ID WHERE Region_Id='" + Session["Region_ID"].ToString() + "' order by R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName";
            query = "select TMR.region as Region_Name, MD.District_Name,TMD.DepotName as Branch_Name,MG18.BranchID, MG18.Hired_Type,count(MG18.Godown_ID) as totalgodown ,sum(MG18.Godown_Capacity) as TotalGoCap,sum(MG18.Godown_Scientific_Capacity) as ScientificCapacity from tbl_MetaData_GODOWN_2018 as MG18 left join tbl_metadata_district as MD on MG18.DistrictId = MD.District_Id left join tbl_MetaData_DEPOT as TMD on MG18.BranchID = TMD.BranchId left join tbl_MetaData_Region as TMR on TMR.Region_Id = MD.Region_ID where  TMR.Region_Id='" + Session["Region_ID"].ToString() + "' group by TMR.region, MD.District_Name,TMD.DepotName, MG18.Hired_Type,MG18.BranchID order by TMR.region, MD.District_Name,TMD.DepotName";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();

                GridView1.FooterRow.Style.Add("text-align", "center");
                GridView1.FooterRow.Cells[4].Text = "Total";
                GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("totalgodown")).ToString();
                GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalGoCap")).ToString();
                GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ScientificCapacity")).ToString();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    protected void ddldist_TextChanged(object sender, EventArgs e)
    {
        string query = "select BranchId,DepotName from tbl_metadata_depot where DistrictId ='" + ddldist.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "BranchId";
            ddlbranch.DataBind();         
            ddlbranch.Items.Insert(0, new ListItem("ब्रांच चुने ", "0"));

        }
    }

    protected void SearchID_Click(object sender, EventArgs e)
    {

        if (ddldist.SelectedValue != "0" && ddlbranch.SelectedValue!="0")
        {
            string query = "";
            //query = "select R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName,Beneficiary_Id,Beneficiary_Name,Beneficiary_Type,'**' + right(Account_No, 4) as Account_No,'**' + right(IFSC_Code, 4) as IFSC_Code,GO_Approval_Statusfrom tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICTon tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id inner join tbl_MetaData_DEPOT as DP on DP.BranchId = tbl_Beneficiary_Account_Details.Branch_Id inner join tbl_MetaData_Region as R on R.Region_Id = tbl_MetaData_DISTRICT.Region_ID WHERE Region_Id='" + Session["Region_ID"].ToString() + "' order by R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName";
            query = "select TMR.region as Region_Name, MD.District_Name,TMD.DepotName as Branch_Name,MG18.BranchID, MG18.Hired_Type,count(MG18.Godown_ID) as totalgodown ,sum(MG18.Godown_Capacity) as TotalGoCap,sum(MG18.Godown_Scientific_Capacity) as ScientificCapacity from tbl_MetaData_GODOWN_2018 as MG18 left join tbl_metadata_district as MD on MG18.DistrictId = MD.District_Id left join tbl_MetaData_DEPOT as TMD on MG18.BranchID = TMD.BranchId left join tbl_MetaData_Region as TMR on TMR.Region_Id = MD.Region_ID where MG18.BranchID='" + ddlbranch.SelectedValue + "' and  MG18.DistrictId='" + ddldist.SelectedValue + "' group by TMR.region, MD.District_Name,TMD.DepotName, MG18.Hired_Type,MG18.BranchID order by TMR.region, MD.District_Name,TMD.DepotName";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();

                GridView1.FooterRow.Style.Add("text-align", "center");
                GridView1.FooterRow.Cells[4].Text = "Total";
                GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("totalgodown")).ToString();
                GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalGoCap")).ToString();
                GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ScientificCapacity")).ToString();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }


        if (ddldist.SelectedValue != "0" && ddlbranch.SelectedValue == "0")
        {
            string query = "";
            //query = "select R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName,Beneficiary_Id,Beneficiary_Name,Beneficiary_Type,'**' + right(Account_No, 4) as Account_No,'**' + right(IFSC_Code, 4) as IFSC_Code,GO_Approval_Statusfrom tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICTon tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id inner join tbl_MetaData_DEPOT as DP on DP.BranchId = tbl_Beneficiary_Account_Details.Branch_Id inner join tbl_MetaData_Region as R on R.Region_Id = tbl_MetaData_DISTRICT.Region_ID WHERE Region_Id='" + Session["Region_ID"].ToString() + "' order by R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName";
            query = "select TMR.region as Region_Name, MD.District_Name,TMD.DepotName as Branch_Name,MG18.BranchID, MG18.Hired_Type,count(MG18.Godown_ID) as totalgodown ,sum(MG18.Godown_Capacity) as TotalGoCap,sum(MG18.Godown_Scientific_Capacity) as ScientificCapacity from tbl_MetaData_GODOWN_2018 as MG18 left join tbl_metadata_district as MD on MG18.DistrictId = MD.District_Id left join tbl_MetaData_DEPOT as TMD on MG18.BranchID = TMD.BranchId left join tbl_MetaData_Region as TMR on TMR.Region_Id = MD.Region_ID where    MG18.DistrictId='" + ddldist.SelectedValue + "' group by TMR.region, MD.District_Name,TMD.DepotName, MG18.Hired_Type,MG18.BranchID order by TMR.region, MD.District_Name,TMD.DepotName";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();

                GridView1.FooterRow.Style.Add("text-align", "center");
                GridView1.FooterRow.Cells[4].Text = "Total";
                GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("totalgodown")).ToString();
                GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalGoCap")).ToString();
                GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ScientificCapacity")).ToString();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        }
}