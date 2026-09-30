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


public partial class StatePages_State_BranchChoiceFilling : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if ((Session["UserName"] != null))
            {
                if (!IsPostBack)
                {
                    fillgrid();
                }
            }
            else
            {
                Response.Redirect("../Logout.aspx");
            }



           

        }


    }

    private void fillgrid()
    {
        try
        {

            string query1 = "select Region_Id,region from tbl_MetaData_Region";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {

                ddlregion.DataSource = ds1.Tables[0];
                ddlregion.DataTextField = "region";
                ddlregion.DataValueField = "Region_Id";
                ddlregion.DataBind();
                ddlregion.Items.Insert(0, new ListItem("Region चुने", "0"));

            }


            string query = "";
            //query = "select R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName,Beneficiary_Id,Beneficiary_Name,Beneficiary_Type,'**' + right(Account_No, 4) as Account_No,'**' + right(IFSC_Code, 4) as IFSC_Code,GO_Approval_Statusfrom tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICTon tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id inner join tbl_MetaData_DEPOT as DP on DP.BranchId = tbl_Beneficiary_Account_Details.Branch_Id inner join tbl_MetaData_Region as R on R.Region_Id = tbl_MetaData_DISTRICT.Region_ID WHERE Region_Id='" + Session["Region_ID"].ToString() + "' order by R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName" where  TMR.Region_Id='" + Session["Region_ID"].ToString() + "';
            query = "select MDR.region, MD.District_Name,MDE.DepotName,TWR.Warehouse_Name, case when CF.Choice='A' then N'अ' when CF.Choice='BR' then N'ब्रांच से रेजेक्ट' else N'ब' end Choice,CF.*,convert(varchar(10),CF.Insert_Date,103) insertdate from Tbl_JVS_Choise_Filling as CF left join tbl_MetaData_DISTRICT as MD on MD.District_Id = CF.Dist_ID left join tbl_MetaData_Region as MDR on MDR.Region_Id = MD.Region_ID left join tbl_MetaData_DEPOT as MDE on MDE.BranchId = CF.Branch_ID left join tbl_WarehouseRegistration TWR on TWR.Registration_Id = CF.Reg_ID ";

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
                //GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("totalgodown")).ToString();
                //GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalGoCap")).ToString();
                //GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ScientificCapacity")).ToString();

               // GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlQty")).ToString();

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



    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        string query1 = "select District_Id, District_Name from tbl_metadata_district MD left  join tbl_MetaData_Region MR on MR.Region_Id = MD.Region_ID where MD.Region_ID ='" + ddlregion.SelectedValue + "'";
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

            ddlbranch.Items.Insert(0, new ListItem("Branch  चुने", "0"));

            // ddlbranch.Items.Insert(0, new ListItem("Branch चुने", "0"));
            // ddlbranch.Items.Clear();

        }
    }

    protected void ddldist_SelectedIndexChanged(object sender, EventArgs e)
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
        if (ddlregion.SelectedValue != "0" && ddldist.SelectedValue == "0" && ddlbranch.SelectedValue == "0")
        {

            if (ddlChoice.SelectedValue == "0")
            {
                string query = "";
                //query = "select R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName,Beneficiary_Id,Beneficiary_Name,Beneficiary_Type,'**' + right(Account_No, 4) as Account_No,'**' + right(IFSC_Code, 4) as IFSC_Code,GO_Approval_Statusfrom tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICTon tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id inner join tbl_MetaData_DEPOT as DP on DP.BranchId = tbl_Beneficiary_Account_Details.Branch_Id inner join tbl_MetaData_Region as R on R.Region_Id = tbl_MetaData_DISTRICT.Region_ID WHERE Region_Id='" + Session["Region_ID"].ToString() + "' order by R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName" where  TMR.Region_Id='" + Session["Region_ID"].ToString() + "';
                query = "select MDR.region, MD.District_Name,MDE.DepotName,TWR.Warehouse_Name,case when CF.Choice = 'A' then N'अ' when CF.Choice = 'BR' then N'ब्रांच से रेजेक्ट' else N'ब' end Choice, CF.*,convert(varchar(10), CF.Insert_Date, 103) insertdate  from Tbl_JVS_Choise_Filling as CF left join tbl_MetaData_DISTRICT as MD on MD.District_Id = CF.Dist_ID left join tbl_MetaData_Region as MDR on MDR.Region_Id = MD.Region_ID left join tbl_MetaData_DEPOT as MDE on MDE.BranchId = CF.Branch_ID left join tbl_WarehouseRegistration TWR on TWR.Registration_Id = CF.Reg_ID where MDR.Region_Id = '" + ddlregion.SelectedValue + "'";

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
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }
            else
            {
                string query = "";
                //query = "select R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName,Beneficiary_Id,Beneficiary_Name,Beneficiary_Type,'**' + right(Account_No, 4) as Account_No,'**' + right(IFSC_Code, 4) as IFSC_Code,GO_Approval_Statusfrom tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICTon tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id inner join tbl_MetaData_DEPOT as DP on DP.BranchId = tbl_Beneficiary_Account_Details.Branch_Id inner join tbl_MetaData_Region as R on R.Region_Id = tbl_MetaData_DISTRICT.Region_ID WHERE Region_Id='" + Session["Region_ID"].ToString() + "' order by R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName" where  TMR.Region_Id='" + Session["Region_ID"].ToString() + "';
                query = "select MDR.region, MD.District_Name,MDE.DepotName,TWR.Warehouse_Name,case when CF.Choice = 'A' then N'अ' when CF.Choice = 'BR' then N'ब्रांच से रेजेक्ट' else N'ब' end Choice, CF.*,convert(varchar(10), CF.Insert_Date, 103) insertdate  from Tbl_JVS_Choise_Filling as CF left join tbl_MetaData_DISTRICT as MD on MD.District_Id = CF.Dist_ID left join tbl_MetaData_Region as MDR on MDR.Region_Id = MD.Region_ID left join tbl_MetaData_DEPOT as MDE on MDE.BranchId = CF.Branch_ID left join tbl_WarehouseRegistration TWR on TWR.Registration_Id = CF.Reg_ID where MDR.Region_Id = '" + ddlregion.SelectedValue + "' and CF.Choice='" + ddlChoice.SelectedValue + "' ";

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
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }
        }
        




        if (ddlregion.SelectedValue != "0" && ddldist.SelectedValue != "0" && ddlbranch.SelectedValue == "0")
        {

            if (ddlChoice.SelectedValue == "0")
            {
                string query = "";
                //query = "select R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName,Beneficiary_Id,Beneficiary_Name,Beneficiary_Type,'**' + right(Account_No, 4) as Account_No,'**' + right(IFSC_Code, 4) as IFSC_Code,GO_Approval_Statusfrom tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICTon tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id inner join tbl_MetaData_DEPOT as DP on DP.BranchId = tbl_Beneficiary_Account_Details.Branch_Id inner join tbl_MetaData_Region as R on R.Region_Id = tbl_MetaData_DISTRICT.Region_ID WHERE Region_Id='" + Session["Region_ID"].ToString() + "' order by R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName" where  TMR.Region_Id='" + Session["Region_ID"].ToString() + "';
                query = "select MDR.region, MD.District_Name,MDE.DepotName,TWR.Warehouse_Name,case when CF.Choice = 'A' then N'अ' when CF.Choice = 'BR' then N'ब्रांच से रेजेक्ट' else N'ब' end Choice, CF.*,convert(varchar(10), CF.Insert_Date, 103) insertdate  from Tbl_JVS_Choise_Filling as CF left join tbl_MetaData_DISTRICT as MD on MD.District_Id = CF.Dist_ID left join tbl_MetaData_Region as MDR on MDR.Region_Id = MD.Region_ID left join tbl_MetaData_DEPOT as MDE on MDE.BranchId = CF.Branch_ID left join tbl_WarehouseRegistration TWR on TWR.Registration_Id = CF.Reg_ID where MDR.Region_Id = '" + ddlregion.SelectedValue + "' and   MD.District_Id='" + ddldist.SelectedValue + "'";

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
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }

            else
            {

                string query = "";
                //query = "select R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName,Beneficiary_Id,Beneficiary_Name,Beneficiary_Type,'**' + right(Account_No, 4) as Account_No,'**' + right(IFSC_Code, 4) as IFSC_Code,GO_Approval_Statusfrom tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICTon tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id inner join tbl_MetaData_DEPOT as DP on DP.BranchId = tbl_Beneficiary_Account_Details.Branch_Id inner join tbl_MetaData_Region as R on R.Region_Id = tbl_MetaData_DISTRICT.Region_ID WHERE Region_Id='" + Session["Region_ID"].ToString() + "' order by R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName" where  TMR.Region_Id='" + Session["Region_ID"].ToString() + "';
                query = "select MDR.region, MD.District_Name,MDE.DepotName,TWR.Warehouse_Name,case when CF.Choice = 'A' then N'अ' when CF.Choice = 'BR' then N'ब्रांच से रेजेक्ट' else N'ब' end Choice, CF.*,convert(varchar(10), CF.Insert_Date, 103) insertdate  from Tbl_JVS_Choise_Filling as CF left join tbl_MetaData_DISTRICT as MD on MD.District_Id = CF.Dist_ID left join tbl_MetaData_Region as MDR on MDR.Region_Id = MD.Region_ID left join tbl_MetaData_DEPOT as MDE on MDE.BranchId = CF.Branch_ID left join tbl_WarehouseRegistration TWR on TWR.Registration_Id = CF.Reg_ID where MDR.Region_Id = '" + ddlregion.SelectedValue + "' and   MD.District_Id='" + ddldist.SelectedValue + "' and CF.Choice='" + ddlChoice.SelectedValue + "'";

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
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }
        
        }


        if (ddlregion.SelectedValue != "0" && ddldist.SelectedValue != "0" && ddlbranch.SelectedValue != "0")
        {

            if (ddlChoice.SelectedValue == "0")
            {
                string query = "";
                //query = "select R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName,Beneficiary_Id,Beneficiary_Name,Beneficiary_Type,'**' + right(Account_No, 4) as Account_No,'**' + right(IFSC_Code, 4) as IFSC_Code,GO_Approval_Statusfrom tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICTon tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id inner join tbl_MetaData_DEPOT as DP on DP.BranchId = tbl_Beneficiary_Account_Details.Branch_Id inner join tbl_MetaData_Region as R on R.Region_Id = tbl_MetaData_DISTRICT.Region_ID WHERE Region_Id='" + Session["Region_ID"].ToString() + "' order by R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName" where  TMR.Region_Id='" + Session["Region_ID"].ToString() + "';
                query = "select MDR.region, MD.District_Name,MDE.DepotName,TWR.Warehouse_Name,case when CF.Choice = 'A' then N'अ' when CF.Choice = 'BR' then N'ब्रांच से रेजेक्ट' else N'ब' end Choice, CF.*,convert(varchar(10), CF.Insert_Date, 103) insertdate  from Tbl_JVS_Choise_Filling as CF left join tbl_MetaData_DISTRICT as MD on MD.District_Id = CF.Dist_ID left join tbl_MetaData_Region as MDR on MDR.Region_Id = MD.Region_ID left join tbl_MetaData_DEPOT as MDE on MDE.BranchId = CF.Branch_ID left join tbl_WarehouseRegistration TWR on TWR.Registration_Id = CF.Reg_ID where MDR.Region_Id = '" + ddlregion.SelectedValue + "' and   MD.District_Id='" + ddldist.SelectedValue + "' and   MDE.BranchId='" + ddlbranch.SelectedValue + "'";

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
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }

            else
            {
                string query = "";
                //query = "select R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName,Beneficiary_Id,Beneficiary_Name,Beneficiary_Type,'**' + right(Account_No, 4) as Account_No,'**' + right(IFSC_Code, 4) as IFSC_Code,GO_Approval_Statusfrom tbl_Beneficiary_Account_Details inner join tbl_MetaData_DISTRICTon tbl_Beneficiary_Account_Details.District_Id = tbl_MetaData_DISTRICT.District_Id inner join tbl_MetaData_DEPOT as DP on DP.BranchId = tbl_Beneficiary_Account_Details.Branch_Id inner join tbl_MetaData_Region as R on R.Region_Id = tbl_MetaData_DISTRICT.Region_ID WHERE Region_Id='" + Session["Region_ID"].ToString() + "' order by R.region,tbl_MetaData_DISTRICT.District_Name_HI,DP.DepotName" where  TMR.Region_Id='" + Session["Region_ID"].ToString() + "';
                query = "select MDR.region, MD.District_Name,MDE.DepotName,TWR.Warehouse_Name,case when CF.Choice = 'A' then N'अ' when CF.Choice = 'BR' then N'ब्रांच से रेजेक्ट' else N'ब' end Choice, CF.*,convert(varchar(10), CF.Insert_Date, 103) insertdate  from Tbl_JVS_Choise_Filling as CF left join tbl_MetaData_DISTRICT as MD on MD.District_Id = CF.Dist_ID left join tbl_MetaData_Region as MDR on MDR.Region_Id = MD.Region_ID left join tbl_MetaData_DEPOT as MDE on MDE.BranchId = CF.Branch_ID left join tbl_WarehouseRegistration TWR on TWR.Registration_Id = CF.Reg_ID where MDR.Region_Id = '" + ddlregion.SelectedValue + "' and   MD.District_Id='" + ddldist.SelectedValue + "' and   MDE.BranchId='" + ddlbranch.SelectedValue + "' and CF.Choice='" + ddlChoice.SelectedValue + "'";

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
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }

        }
        }



    }
