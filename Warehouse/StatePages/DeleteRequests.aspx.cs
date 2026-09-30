using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class StatePages_DeleteRequests : System.Web.UI.Page
{

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    DataSet ds = null;
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    SqlTransaction sqltran;
    string Branch = "";
    string Distid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            if (!IsPostBack)
            {
                GetRegion();
                //F_Selectreciptdata();
                //F_SelectwhrData();
                //F_SelectGatepassData();
                //F_SelectDepositorwhrData();
            }
        }
        else
        {
            Response.Redirect("../Logout.aspx");
        }

    }
    public void GetRegion()
    {
        string qry = "select region,Region_Id from tbl_MetaData_Region";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlRegion.DataSource = ds.Tables[0];
            ddlRegion.DataTextField = "region";
            ddlRegion.DataValueField = "Region_Id";
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0, "--Se|ect--");
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchByWHRId();
        System.Threading.Thread.Sleep(1000);
    }

    protected void SearchByWHRId()
    {
        
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            //string query = " SELECT [Req_id] ,[tbl_Opening_Delete_Req].[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Opening_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[tbl_Opening_Delete_Req].[Depositor_Name],[whr_id],[tbl_Opening_Delete_Req].[commodity_id],[Godown_id],(select Godown_Name from tbl_MetaData_GODOWN where [tbl_Opening_Delete_Req].Godown_id=tbl_MetaData_GODOWN.Godown_ID) as GodownName,CONVERT(nvarchar(30),[whr_Date],103) as WHRDate,[No_of_Bags],[Quantity],[Operator_Name] ,[Bm_Name] , CONVERT(nvarchar(30),[Req_date],103) as RequestDate,[Status] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Opening_Delete_Req] inner join tbl_storage_Depositor_WHR_Relation on tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id=[tbl_Opening_Delete_Req].whr_id where DeleteStatus='N' and Status='D' and Districtid in (select District_Id from dbo.tbl_MetaData_DISTRICT where Region_ID='" + Session["Region_ID"].ToString() + "') order by [tbl_Opening_Delete_Req].Req_date desc";
            string query = " SELECT [Req_id] ,[tbl_Opening_Delete_Req].[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Opening_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[tbl_Opening_Delete_Req].[Depositor_Name],[whr_id],[tbl_Opening_Delete_Req].[commodity_id],[Godown_id],(select Godown_Name from tbl_MetaData_GODOWN where [tbl_Opening_Delete_Req].Godown_id=tbl_MetaData_GODOWN.Godown_ID) as GodownName,CONVERT(nvarchar(30),[whr_Date],103) as WHRDate,[No_of_Bags],[Quantity],[Operator_Name] ,[Bm_Name] , CONVERT(nvarchar(30),[Req_date],103) as RequestDate,[Status] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Opening_Delete_Req] inner join tbl_storage_Depositor_WHR_Relation on tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id=[tbl_Opening_Delete_Req].whr_id where DeleteStatus='N' and Status='D' and Districtid in (select District_Id from dbo.tbl_MetaData_DISTRICT where whr_id='" + txtWHR_ID.Text + "') order by [tbl_Opening_Delete_Req].Req_date desc";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblwhrcount.Text = ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvdepositorwhr.DataSource = ds.Tables[0];

                gvdepositorwhr.DataBind();
                txtWHR_ID.Text = "";

            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Can not do this.....')", true);
        }
        //}

    }

    protected void F_Selectreciptdata()
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            string query = "SELECT [Req_id],[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Receipt_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[Receipt_id],[Arrivalstock_id],[commodity_id],[Godown_id],[TC_No],[No_of_Bags],[Quantity] ,[Opeartor_Mob],[Bm_Name] ,[Bm_Mob] ,CONVERT(nvarchar(30),[Req_date],103) as RequestDate FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Receipt_Delete_Req]  where   DeleteStatus='N'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblreceiptcount.Text = ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvreceipt.DataSource = ds.Tables[0];

                gvreceipt.DataBind();

            }
        }
        else if (Session["RoleId"].ToString() == "9")
        {
            string query = "SELECT [Req_id],[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Receipt_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[Receipt_id],[Arrivalstock_id],[commodity_id],[Godown_id],[TC_No],[No_of_Bags],[Quantity] ,[Opeartor_Mob],[Bm_Name] ,[Bm_Mob] ,CONVERT(nvarchar(30),[Req_date],103) as RequestDate FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Receipt_Delete_Req]  where   DeleteStatus='N' and Depotid in (select BranchId from [tbl_MetaData_DEPOT] where DepoTypeID='4')";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblreceiptcount.Text = ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvreceipt.DataSource = ds.Tables[0];

                gvreceipt.DataBind();

            }
        }
        else if (Session["RoleId"].ToString() == "10")
        {
            string query = "SELECT [Req_id],[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Receipt_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[Receipt_id],[Arrivalstock_id],[commodity_id],[Godown_id],[TC_No],[No_of_Bags],[Quantity] ,[Opeartor_Mob],[Bm_Name] ,[Bm_Mob] ,CONVERT(nvarchar(30),[Req_date],103) as RequestDate FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Receipt_Delete_Req]  where   DeleteStatus='N' and Depotid in (select BranchId from [tbl_MetaData_DEPOT] where DepoTypeID='4' and DistrictId='" + Session["Depot_DistID"].ToString() + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblreceiptcount.Text = ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvreceipt.DataSource = ds.Tables[0];

                gvreceipt.DataBind();

            }
        }
        //else if(Session["Region_ID"].ToString() != null)
        else
        {
            //string query = "SELECT [Req_id],[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Receipt_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[Receipt_id],[Arrivalstock_id],[commodity_id],[Godown_id],[TC_No],[No_of_Bags],[Quantity] ,[Opeartor_Mob],[Bm_Name] ,[Bm_Mob] ,CONVERT(nvarchar(30),[Req_date],103) as RequestDate FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Receipt_Delete_Req]  where   DeleteStatus='N' and Districtid in (select District_Id from dbo.tbl_MetaData_DISTRICT where Region_ID='" + Session["Region_ID"].ToString() + "')";
            string query = "SELECT [Req_id],[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Receipt_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[Receipt_id],[Arrivalstock_id],[commodity_id],[Godown_id],[TC_No],[No_of_Bags],[Quantity] ,[Opeartor_Mob],[Bm_Name] ,[Bm_Mob] ,CONVERT(nvarchar(30),[Req_date],103) as RequestDate FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Receipt_Delete_Req]  where   DeleteStatus='N' and Districtid in (select District_Id from dbo.tbl_MetaData_DISTRICT where Region_ID='" + ddlRegion.SelectedValue.ToString() + "')";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblreceiptcount.Text = ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvreceipt.DataSource = ds.Tables[0];

                gvreceipt.DataBind();

            }

        }

    }

    protected void F_SelectwhrData()
    {
        //if (Session["UserName"].ToString() == "MPSWLC")
        //{
        //    string query = "SELECT [Req_id] ,[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Opening_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[Depositor_Name],[whr_id],[commodity_id],[Godown_id],(select Godown_Name from tbl_MetaData_GODOWN where [tbl_Opening_Delete_Req].Godown_id=tbl_MetaData_GODOWN.Godown_ID) as GodownName,CONVERT(nvarchar(30),[whr_Date],103) as WHRDate,[No_of_Bags],[Quantity],[Operator_Name] ,[Bm_Name] , CONVERT(nvarchar(30),[Req_date],103) as RequestDate,[Status] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Opening_Delete_Req] where  DeleteStatus='N' and Status='O'";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    lblwhrdel.Text = ds.Tables[0].Rows.Count.ToString();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        gvwhrs.DataSource = ds.Tables[0];

        //        gvwhrs.DataBind();

        //    }
        //}
        //else if (Session["RoleId"].ToString() == "9")
        //{
        //    string query = "SELECT [Req_id] ,[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Opening_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[Depositor_Name],[whr_id],[commodity_id],[Godown_id],(select Godown_Name from tbl_MetaData_GODOWN where [tbl_Opening_Delete_Req].Godown_id=tbl_MetaData_GODOWN.Godown_ID) as GodownName,CONVERT(nvarchar(30),[whr_Date],103) as WHRDate,[No_of_Bags],[Quantity],[Operator_Name] ,[Bm_Name] , CONVERT(nvarchar(30),[Req_date],103) as RequestDate,[Status] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Opening_Delete_Req] where  DeleteStatus='N' and Status='O' and Depotid in (select BranchId from [tbl_MetaData_DEPOT] where DepoTypeID='4')";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    lblwhrdel.Text = ds.Tables[0].Rows.Count.ToString();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        gvwhrs.DataSource = ds.Tables[0];

        //        gvwhrs.DataBind();

        //    }
        //}
        //else if (Session["RoleId"].ToString() == "10")
        //{
        //    string query = "SELECT [Req_id] ,[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Opening_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[Depositor_Name],[whr_id],[commodity_id],[Godown_id],(select Godown_Name from tbl_MetaData_GODOWN where [tbl_Opening_Delete_Req].Godown_id=tbl_MetaData_GODOWN.Godown_ID) as GodownName,CONVERT(nvarchar(30),[whr_Date],103) as WHRDate,[No_of_Bags],[Quantity],[Operator_Name] ,[Bm_Name] , CONVERT(nvarchar(30),[Req_date],103) as RequestDate,[Status] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Opening_Delete_Req] where  DeleteStatus='N' and Status='O' and Depotid in (select BranchId from [tbl_MetaData_DEPOT] where DepoTypeID='4' and DistrictId='" + Session["Depot_DistID"].ToString() + "')";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    lblwhrdel.Text = ds.Tables[0].Rows.Count.ToString();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        gvwhrs.DataSource = ds.Tables[0];

        //        gvwhrs.DataBind();

        //    }
        //}
        ////else if (Session["Region_ID"].ToString() != null)
        //else
        //{
        //string query = "SELECT [Req_id] ,[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Opening_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[Depositor_Name],[whr_id],[commodity_id],[Godown_id],(select Godown_Name from tbl_MetaData_GODOWN where [tbl_Opening_Delete_Req].Godown_id=tbl_MetaData_GODOWN.Godown_ID) as GodownName,CONVERT(nvarchar(30),[whr_Date],103) as WHRDate,[No_of_Bags],[Quantity],[Operator_Name] ,[Bm_Name] , CONVERT(nvarchar(30),[Req_date],103) as RequestDate,[Status] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Opening_Delete_Req] where  DeleteStatus='N' and Status='O' and Districtid in (select District_Id from dbo.tbl_MetaData_DISTRICT where Region_ID='" + Session["Region_ID"].ToString() + "')";
        string query = "SELECT [Req_id] ,[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Opening_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[Depositor_Name],[whr_id],[commodity_id],[Godown_id],(select Godown_Name from tbl_MetaData_GODOWN where [tbl_Opening_Delete_Req].Godown_id=tbl_MetaData_GODOWN.Godown_ID) as GodownName,CONVERT(nvarchar(30),[whr_Date],103) as WHRDate,[No_of_Bags],[Quantity],[Operator_Name] ,[Bm_Name] , CONVERT(nvarchar(30),[Req_date],103) as RequestDate,[Status] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Opening_Delete_Req] where  DeleteStatus='N' and Status='O' and Districtid in (select District_Id from dbo.tbl_MetaData_DISTRICT where Region_ID='" + ddlRegion.SelectedValue.ToString() + "')";

        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        lblwhrdel.Text = ds.Tables[0].Rows.Count.ToString();
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvwhrs.DataSource = ds.Tables[0];

            gvwhrs.DataBind();

        }

        //}

    }

    protected void F_SelectDepositorwhrData()
    {
        //if (Session["UserName"].ToString() == "MPSWLC")
        //{
        //    string query = "SELECT [Req_id] ,[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Opening_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[Depositor_Name],[whr_id],[commodity_id],[Godown_id],(select Godown_Name from tbl_MetaData_GODOWN where [tbl_Opening_Delete_Req].Godown_id=tbl_MetaData_GODOWN.Godown_ID) as GodownName,CONVERT(nvarchar(30),[whr_Date],103) as WHRDate,[No_of_Bags],[Quantity],[Operator_Name] ,[Bm_Name] , CONVERT(nvarchar(30),[Req_date],103) as RequestDate,[Status] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Opening_Delete_Req] where DeleteStatus='N' and Status='D'";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    lblwhrcount.Text = ds.Tables[0].Rows.Count.ToString();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        gvdepositorwhr.DataSource = ds.Tables[0];

        //        gvdepositorwhr.DataBind();

        //    }
        //}
        //else if (Session["RoleId"].ToString() == "9")
        //{
        //    string query = "SELECT [Req_id] ,[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Opening_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[Depositor_Name],[whr_id],[commodity_id],[Godown_id],(select Godown_Name from tbl_MetaData_GODOWN where [tbl_Opening_Delete_Req].Godown_id=tbl_MetaData_GODOWN.Godown_ID) as GodownName,CONVERT(nvarchar(30),[whr_Date],103) as WHRDate,[No_of_Bags],[Quantity],[Operator_Name] ,[Bm_Name] , CONVERT(nvarchar(30),[Req_date],103) as RequestDate,[Status] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Opening_Delete_Req] where DeleteStatus='N' and Status='D' and Depotid in (select BranchId from [tbl_MetaData_DEPOT] where DepoTypeID='4')";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    lblwhrcount.Text = ds.Tables[0].Rows.Count.ToString();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        gvdepositorwhr.DataSource = ds.Tables[0];

        //        gvdepositorwhr.DataBind();

        //    }
        //}
        //else if (Session["RoleId"].ToString() == "10")
        //{
        //   string query = "SELECT [Req_id] ,[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Opening_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[Depositor_Name],[whr_id],[commodity_id],[Godown_id],(select Godown_Name from tbl_MetaData_GODOWN where [tbl_Opening_Delete_Req].Godown_id=tbl_MetaData_GODOWN.Godown_ID) as GodownName,CONVERT(nvarchar(30),[whr_Date],103) as WHRDate,[No_of_Bags],[Quantity],[Operator_Name] ,[Bm_Name] , CONVERT(nvarchar(30),[Req_date],103) as RequestDate,[Status] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Opening_Delete_Req] where DeleteStatus='N' and Status='D' and Depotid in (select BranchId from [tbl_MetaData_DEPOT] where DepoTypeID='4' and DistrictId='" + Session["Depot_DistID"].ToString() + "')";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    lblwhrcount.Text = ds.Tables[0].Rows.Count.ToString();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        gvdepositorwhr.DataSource = ds.Tables[0];

        //        gvdepositorwhr.DataBind();

        //    }
        //}
        ////else if (Session["Region_ID"].ToString() != null)
        //else 
        //{
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            //string query = " SELECT [Req_id] ,[tbl_Opening_Delete_Req].[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Opening_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[tbl_Opening_Delete_Req].[Depositor_Name],[whr_id],[tbl_Opening_Delete_Req].[commodity_id],[Godown_id],(select Godown_Name from tbl_MetaData_GODOWN where [tbl_Opening_Delete_Req].Godown_id=tbl_MetaData_GODOWN.Godown_ID) as GodownName,CONVERT(nvarchar(30),[whr_Date],103) as WHRDate,[No_of_Bags],[Quantity],[Operator_Name] ,[Bm_Name] , CONVERT(nvarchar(30),[Req_date],103) as RequestDate,[Status] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Opening_Delete_Req] inner join tbl_storage_Depositor_WHR_Relation on tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id=[tbl_Opening_Delete_Req].whr_id where DeleteStatus='N' and Status='D' and Districtid in (select District_Id from dbo.tbl_MetaData_DISTRICT where Region_ID='" + Session["Region_ID"].ToString() + "') order by [tbl_Opening_Delete_Req].Req_date desc";
            string query = " SELECT [Req_id] ,[tbl_Opening_Delete_Req].[Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Opening_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName,[tbl_Opening_Delete_Req].[Depositor_Name],[whr_id],[tbl_Opening_Delete_Req].[commodity_id],[Godown_id],(select Godown_Name from tbl_MetaData_GODOWN where [tbl_Opening_Delete_Req].Godown_id=tbl_MetaData_GODOWN.Godown_ID) as GodownName,CONVERT(nvarchar(30),[whr_Date],103) as WHRDate,[No_of_Bags],[Quantity],[Operator_Name] ,[Bm_Name] , CONVERT(nvarchar(30),[Req_date],103) as RequestDate,[Status] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Opening_Delete_Req] inner join tbl_storage_Depositor_WHR_Relation on tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id=[tbl_Opening_Delete_Req].whr_id where DeleteStatus='N' and Status='D' and Districtid in (select District_Id from dbo.tbl_MetaData_DISTRICT where Region_ID='" + ddlRegion.SelectedValue.ToString() + "') order by [tbl_Opening_Delete_Req].Req_date desc";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblwhrcount.Text = ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvdepositorwhr.DataSource = ds.Tables[0];

                gvdepositorwhr.DataBind();

            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Can not do this.....')", true);
        }
        //}

    }
    protected void F_SelectGatepassData()
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            string query = "SELECT  [Req_id], [Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Gatepass_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName ,[GatepassNo],[commodity_id] ,[Godown_id] ,CONVERT(nvarchar(30),[Gatepass_Date],103) as RequestDate ,[No_of_Bags] ,[Quantity] ,[Operator_Name] ,[Bm_Name] ,[Bm_Mob] ,CONVERT(nvarchar(30),[Req_date],103) as RequestDate FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Gatepass_Delete_Req] where DeleteStatus='N'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblgatepasscount.Text = ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvgatepass.DataSource = ds.Tables[0];

                gvgatepass.DataBind();

            }
        }
        else if (Session["RoleId"].ToString() == "9")
        {
            string query = "SELECT  [Req_id], [Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Gatepass_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName ,[GatepassNo],[commodity_id] ,[Godown_id] ,CONVERT(nvarchar(30),[Gatepass_Date],103) as RequestDate ,[No_of_Bags] ,[Quantity] ,[Operator_Name] ,[Bm_Name] ,[Bm_Mob] ,CONVERT(nvarchar(30),[Req_date],103) as RequestDate FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Gatepass_Delete_Req] where DeleteStatus='N' and Depotid in (select BranchId from [tbl_MetaData_DEPOT] where DepoTypeID='4')";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblgatepasscount.Text = ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvgatepass.DataSource = ds.Tables[0];

                gvgatepass.DataBind();

            }
        }
        else if (Session["RoleId"].ToString() == "10")
        {
            string query = "SELECT  [Req_id], [Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Gatepass_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName ,[GatepassNo],[commodity_id] ,[Godown_id] ,CONVERT(nvarchar(30),[Gatepass_Date],103) as RequestDate ,[No_of_Bags] ,[Quantity] ,[Operator_Name] ,[Bm_Name] ,[Bm_Mob] ,CONVERT(nvarchar(30),[Req_date],103) as RequestDate FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Gatepass_Delete_Req] where DeleteStatus='N' and Depotid in (select BranchId from [tbl_MetaData_DEPOT] where DepoTypeID='4' and DistrictId='" + Session["Depot_DistID"].ToString() + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblgatepasscount.Text = ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvgatepass.DataSource = ds.Tables[0];
                gvgatepass.DataBind();
            }
        }
        //else if (Session["Region_ID"].ToString() != null)
        else
        {
            //string query = "SELECT  [Req_id], [Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Gatepass_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName ,[GatepassNo],[commodity_id] ,[Godown_id] ,CONVERT(nvarchar(30),[Gatepass_Date],103) as RequestDate ,[No_of_Bags] ,[Quantity] ,[Operator_Name] ,[Bm_Name] ,[Bm_Mob] ,CONVERT(nvarchar(30),[Req_date],103) as RequestDate FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Gatepass_Delete_Req] where DeleteStatus='N' and Districtid in (select District_Id from dbo.tbl_MetaData_DISTRICT where Region_ID='" + Session["Region_ID"].ToString() + "')";
            string query = "SELECT  [Req_id], [Depotid],(select DepotName from dbo.tbl_MetaData_DEPOT where [tbl_Gatepass_Delete_Req].Depotid=tbl_MetaData_DEPOT.DepotID) as DepotName ,[GatepassNo],[commodity_id] ,[Godown_id] ,CONVERT(nvarchar(30),[Gatepass_Date],103) as RequestDate ,[No_of_Bags] ,[Quantity] ,[Operator_Name] ,[Bm_Name] ,[Bm_Mob] ,CONVERT(nvarchar(30),[Req_date],103) as RequestDate FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Gatepass_Delete_Req] where DeleteStatus='N' and Districtid in (select District_Id from dbo.tbl_MetaData_DISTRICT where Region_ID='" + ddlRegion.SelectedValue.ToString() + "')";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblgatepasscount.Text = ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvgatepass.DataSource = ds.Tables[0];

                gvgatepass.DataBind();

            }
        }


    }
    protected void gvgatepass_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int indx = e.NewPageIndex;
            gvgatepass.PageIndex = e.NewPageIndex;
            F_SelectGatepassData();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    protected void gvwhrs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int indx = e.NewPageIndex;
            gvwhrs.PageIndex = e.NewPageIndex;
            F_SelectwhrData();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    protected void gvreceipt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int indx = e.NewPageIndex;
            gvreceipt.PageIndex = e.NewPageIndex;
            F_Selectreciptdata();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    protected void gvreceipt_SelectedIndexChanged(object sender, EventArgs e)
    {
        int count = 0;
        try
        {
            if (gvreceipt.Rows.Count > 0)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                sqltran = con.BeginTransaction();
                //foreach (GridViewRow gr2 in gvreceipt.Rows)
                //{
                DataSet ds = (DataSet)Session["ds_GridInfo"];
                // CheckBox chk_Delete = new CheckBox();
                string ArrivalStock_Id = gvreceipt.SelectedRow.Cells[6].Text.ToString();
                string Branch = gvreceipt.SelectedRow.Cells[3].Text.ToString();
                string Receiptid = gvreceipt.SelectedRow.Cells[5].Text.ToString();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string reqid = gvreceipt.SelectedRow.Cells[2].Text.ToString();

                string query = "SELECT * FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] where StorageReceipt_Id='" + Receiptid + "' and WHR_Flag !='Y' and WHR_Id is null";
                SqlCommand cmd = new SqlCommand(query, con, sqltran);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds1 = new DataSet();
                da.Fill(ds1);

                if (ds1.Tables[0].Rows.Count > 0)
                {

                    //////////////////////del_tbl_Storage_Arrival_Stock_DeleteLog/////////////////////////
                    string qry = "";
                    qry = "Insert Into tbl_Storage_Arrival_Stock_DeleteLog select [ArrivalStock_Id] ,[State_Id],[District_Id],[DepotId],[Commodity_Id],[Category_Id],[Crop_Year],[Depositor_Name],[Sender_District],[Sender_Godown],[Book_No],[Challan_No],[Movement_Challan_Date],[Gate_PassNo],[Transporter_id],[Truck_No],[Truck_Driver_Name],[Source_of_Arrival],[Rice_Category],[Miller_Name],[Qty_No_of_Bags],[Qty_Wt],[Lot_No],[Quality_Moisture],[Quality_category],[CreatedBy],[UpdatedBy],'" + ip + "',[Gunny_bags_type],[Gunny_bags_new],[Gunny_bags_old],[Remarks],[DepositorType],[Receipt_Status],[Receipt_ID],[DepositDate],[CreatedDate],[Scheme_ID],[Client_IP],getdate(),[AcceptanceNo],[PurchasCentre],[IssueID],BranchID from tbl_Storage_Arrival_Stock where ArrivalStock_Id='" + ArrivalStock_Id.ToString() + "' and BranchID='" + Branch + "'";
                    cmd = new SqlCommand(qry, con, sqltran);
                    int rex = cmd.ExecuteNonQuery();
                    if (rex > 0)
                    {
                        qry = "Delete from tbl_Storage_Arrival_Stock where ArrivalStock_Id='" + ArrivalStock_Id.ToString() + "' and BranchID = '" + Branch + "'";
                        cmd = new SqlCommand(qry, con, sqltran);
                        int x = cmd.ExecuteNonQuery();
                    }

                    //////////////////////tbl_Storage_Receipt_Details_DeleteLog/////////////////////////

                    qry = "Insert Into tbl_Storage_Receipt_Details_DeleteLog select [StorageReceipt_Id],[State_Id],[District_Id],[Depotid],[Commodity_Id],[Category_Id],[WHR_Flag],[WHR_Id],[Grade_Name],[Receipt_Date],[Gate_PassNo],[Mode_of_weighment],[BeamScale_LWB],[Qty_Rvd_No_of_Bags],[Qty_Rvd_Weight],[Supply_gunny_New],[Supply_gunny_Old],[Godown_Delivery_No],[Distance_From_PC],[Ack_Book_No],[Ack_Serial_No],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[AnalysisStatus],[Depositortype],[Supply_gunny_Once_used],[Type_of_gunny],[DepositorName],[ArrivalSource_ID],[Remarks],[Acpt_FCIRO_No],[Acpt_FCIRO_Date],[Client_IP],[IssueID],BranchID,Rid from tbl_Storage_Receipt_Details where StorageReceipt_Id='" + Receiptid + "'  and BranchID='" + Branch + "'";
                    cmd = new SqlCommand(qry, con, sqltran);
                    int rex1 = cmd.ExecuteNonQuery();
                    if (rex1 > 0)
                    {
                        qry = "Delete from tbl_Storage_Receipt_Details where StorageReceipt_Id='" + Receiptid + "' and BranchID='" + Branch + "'";
                        cmd = new SqlCommand(qry, con, sqltran);
                        int x = cmd.ExecuteNonQuery();
                    }

                    //////////////////////tbl_storage_Stacking_Details_DeleteLog/////////////////////////

                    qry = "Insert Into tbl_storage_Stacking_Details_DeleteLog ([State_Id],[District_Id],[Depotid],[Godown_ID],[Stack_ID],[StorageReceipt_Id],[Bags],[Weight],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[DeletedBy],[DeletedDate],[autoid],[WHRId],[Status],Branchid)  select [State_Id],[District_Id],[Depotid],[Godown_ID],[Stack_ID],[StorageReceipt_Id],[Bags],[Weight],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[autoid],[WHRId],[Status],BranchID from tbl_storage_Stacking_Details where StorageReceipt_Id='" + Receiptid + "'  and Branchid='" + Branch + "'";
                    cmd = new SqlCommand(qry, con, sqltran);
                    int rex2 = cmd.ExecuteNonQuery();
                    if (rex2 > 0)
                    {
                        qry = "Delete from tbl_storage_Stacking_Details where StorageReceipt_Id='" + Receiptid + "'  and Branchid='" + Branch + "'";
                        cmd = new SqlCommand(qry, con, sqltran);
                        int x = cmd.ExecuteNonQuery();
                    }

                    qry = "update [tbl_Receipt_Delete_Req] set  DeleteStatus='Y' where  Req_id='" + reqid + "'";
                    cmd = new SqlCommand(qry, con, sqltran);
                    int rex22 = cmd.ExecuteNonQuery();
                    count++;
                    // }
                    //  }
                    //   }
                    sqltran.Commit();
                    con.Close();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
            }
            if (count > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully deleted')", true);
                F_Selectreciptdata();
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast One Record For Deleting')", true);
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();

        }
        finally
        {
            con.Close();
        }
    }

    protected void gvwhrs_SelectedIndexChanged(object sender, EventArgs e)
    {
        int count = 0;
        string WHRID = "";
        string qry = "";
        string StackId = "";
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        sqltran = con.BeginTransaction();
        //Perform Delete operation with log
        try
        {
            WHRID = gvwhrs.SelectedRow.Cells[5].Text.ToString();
            string Branch = gvwhrs.SelectedRow.Cells[2].Text.ToString();
            string reqid = gvwhrs.SelectedRow.Cells[1].Text.ToString();

            if (gvwhrs.Rows.Count > 0)
            {

                //First Select 
                string query = "select SSD.Stack_ID,SSD.Bags,SSD.Weight,tbl_MetaData_STACK.NO_OF_BAG_REceived,tbl_MetaData_STACK.Net_Weight_BAG_Received from tbl_storage_Stacking_Details AS SSD join tbl_MetaData_STACK on SSD.Stack_ID = tbl_MetaData_STACK.Stack_ID where SSD.WHRId = '" + WHRID.ToString() + "' ";
                cmd = new SqlCommand(query, con, sqltran);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                int z = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    StackId = dr[0].ToString();
                    int Bags = Convert.ToInt32(dr[1].ToString());
                    Decimal Qty = Convert.ToDecimal(dr[2].ToString());
                    int stackBags = Convert.ToInt32(dr[3].ToString());
                    Decimal stackQty = Convert.ToDecimal(dr[4].ToString());
                    int RemaingBag = stackBags - Bags;
                    decimal RemaingWat = stackQty - Qty;

                    if (RemaingBag >= 0 && RemaingWat >= 0)
                    {
                        qry = "update [tbl_MetaData_STACK] set [tbl_MetaData_STACK].NO_OF_BAG_REceived='" + RemaingBag + "',[tbl_MetaData_STACK].Net_Weight_BAG_Received='" + RemaingWat + "' where [tbl_MetaData_STACK].Stack_ID='" + StackId + "'";
                        cmd = new SqlCommand(qry, con, sqltran);
                        z = cmd.ExecuteNonQuery();

                        ///////////////////////////update daily transaction status//////////

                        qry = "Update DailyStacking_TransactionStatus set DailyStacking_TransactionStatus.ReceiptBags='" + RemaingBag + "' ,DailyStacking_TransactionStatus.ReceiptWts='" + RemaingWat + "',DailyStacking_TransactionStatus.UpdatedBy ='" + ip + "',DailyStacking_TransactionStatus.UpdatedDate =getdate()  where ((Stackid='" + StackId + "')and (autoid=(select max(autoid) from DailyStacking_TransactionStatus where Stackid='" + StackId + "')))";
                        cmd = new SqlCommand(qry, con, sqltran);
                        int y = cmd.ExecuteNonQuery();
                    }
                }
                if (z > 0)
                {
                    //////////////////////tbl_storage_Depositor_WHR_Relation_DeleteLog/////////////////////////

                    qry = "Insert Into [tbl_storage_Depositor_WHR_Relation_Log] SELECT [Depositor_WHR_Id],[State_Id],[District_Id],[Depotid],[Commodity_Id],[Category_Id],[Whr_No],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[Mode_of_weighment],[BeamScale_LWB],[AvgMoisture_Content],[Lot_No] ,[MktValue_of_Commodity],[Arrival_Source],[WHR_Issue_Date],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[MadeUpBags],[Client_IP],[AvgMoisture_Content_To],[Did] ,[CropYear],[Remark] ,[SangrahadDate],[LicenseNo],[LicenseDate] ,[wday],[wmon],[wyear],[BranchID],[DepositorID],GodownID,Gid from tbl_storage_Depositor_WHR_Relation where Depositor_WHR_Id='" + WHRID.ToString() + "' and BranchID='" + Branch + "'";
                    cmd = new SqlCommand(qry, con, sqltran);
                    int d = cmd.ExecuteNonQuery();
                    if (d > 0)
                    {
                        qry = "Delete from tbl_storage_Depositor_WHR_Relation where Depositor_WHR_Id='" + WHRID.ToString() + "' and BranchID='" + Branch + "'";
                        cmd = new SqlCommand(qry, con, sqltran);
                        int x = cmd.ExecuteNonQuery();
                    }

                    //////////////////////del_tbl_Storage_Arrival_Stock_DeleteLog/////////////////////////

                    string qry2 = "Insert Into tbl_Storage_Arrival_Stock_DeleteLog select [ArrivalStock_Id] ,[State_Id],[District_Id],[DepotId],[Commodity_Id],[Category_Id],[Crop_Year],[Depositor_Name],[Sender_District],[Sender_Godown],[Book_No],[Challan_No],[Movement_Challan_Date],[Gate_PassNo],[Transporter_id],[Truck_No],[Truck_Driver_Name],[Source_of_Arrival],[Rice_Category],[Miller_Name],[Qty_No_of_Bags],[Qty_Wt],[Lot_No],[Quality_Moisture],[Quality_category],[CreatedBy],[UpdatedBy],'" + ip + "',[Gunny_bags_type],[Gunny_bags_new],[Gunny_bags_old],[Remarks],[DepositorType],[Receipt_Status],[Receipt_ID],[DepositDate],[CreatedDate],[Scheme_ID],[Client_IP],getdate(),[AcceptanceNo],[PurchasCentre],[IssueID],[BranchId] from tbl_Storage_Arrival_Stock where ArrivalStock_Id='" + WHRID.ToString() + "'  and BranchID='" + Branch + "'";
                    cmd = new SqlCommand(qry2, con, sqltran);
                    int rex = cmd.ExecuteNonQuery();
                    if (rex > 0)
                    {
                        qry = "Delete from tbl_Storage_Arrival_Stock where ArrivalStock_Id='" + WHRID.ToString() + "' and BranchID='" + Branch + "'";
                        cmd = new SqlCommand(qry, con, sqltran);
                        int x = cmd.ExecuteNonQuery();
                    }

                    //////////////////////tbl_Storage_Receipt_Details_DeleteLog/////////////////////////

                    qry = "Insert Into tbl_Storage_Receipt_Details_DeleteLog select [StorageReceipt_Id],[State_Id],[District_Id],[Depotid],[Commodity_Id],[Category_Id],[WHR_Flag],[WHR_Id],[Grade_Name],[Receipt_Date],[Gate_PassNo],[Mode_of_weighment],[BeamScale_LWB],[Qty_Rvd_No_of_Bags],[Qty_Rvd_Weight],[Supply_gunny_New],[Supply_gunny_Old],[Godown_Delivery_No],[Distance_From_PC],[Ack_Book_No],[Ack_Serial_No],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[AnalysisStatus],[Depositortype],[Supply_gunny_Once_used],[Type_of_gunny],[DepositorName],[ArrivalSource_ID],[Remarks],[Acpt_FCIRO_No],[Acpt_FCIRO_Date],[Client_IP],[IssueID],[BranchId],Rid from tbl_Storage_Receipt_Details where WHR_Id='" + WHRID.ToString() + "'  and BranchID='" + Branch + "'";
                    cmd = new SqlCommand(qry, con, sqltran);
                    int rex1 = cmd.ExecuteNonQuery();
                    if (rex1 > 0)
                    {
                        qry = "Delete from tbl_Storage_Receipt_Details where WHR_Id='" + WHRID + "' and BranchID='" + Branch + "'";
                        cmd = new SqlCommand(qry, con, sqltran);
                        int x = cmd.ExecuteNonQuery();
                    }

                    //////////////////////tbl_storage_Stacking_Details_DeleteLog/////////////////////////

                    qry = "Insert Into tbl_storage_Stacking_Details_DeleteLog ([State_Id],[District_Id],[Depotid],[Godown_ID],[Stack_ID],[StorageReceipt_Id],[Bags],[Weight],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[DeletedBy],[DeletedDate],[autoid],[WHRId],[Status],[Branchid])  select [State_Id],[District_Id],[Depotid],[Godown_ID],[Stack_ID],[StorageReceipt_Id],[Bags],[Weight],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[autoid],[WHRId],[Status],[Branchid] from tbl_storage_Stacking_Details where WHRId='" + WHRID + "'  and Branchid='" + Branch + "'";
                    cmd = new SqlCommand(qry, con, sqltran);
                    int rex2 = cmd.ExecuteNonQuery();
                    if (rex2 > 0)
                    {
                        qry = "Delete from tbl_storage_Stacking_Details where WHRId='" + WHRID + "'  and Branchid='" + Branch + "'";
                        cmd = new SqlCommand(qry, con, sqltran);
                        int x = cmd.ExecuteNonQuery();
                    }

                    //////////////////////////whr_status_table/////////////////////////////////////////

                    qry = "insert into whr_status_log select WhrId,Statusflag,getdate(),'" + ip + "' from whr_status where WhrId = '" + WHRID + "'";
                    cmd = new SqlCommand(qry, con, sqltran);
                    int x1 = cmd.ExecuteNonQuery();
                    if (x1 > 0)
                    {
                        qry = "Delete from whr_status where WhrId = '" + WHRID + "'";
                        cmd = new SqlCommand(qry, con, sqltran);
                        int x = cmd.ExecuteNonQuery();
                    }
                    qry = "insert into [dbo].[whrprintstatus_Delete_Log] select [WHRID],[PrintStatus],[DateCreated],GETDATE(),[IsActive] from whrprintstatus where WHRID='" + WHRID + "'";
                    cmd = new SqlCommand(qry, con, sqltran);
                    int rt = cmd.ExecuteNonQuery();


                    if (x1 > 0)
                    {

                        qry = " delete from  dbo.whrprintstatus where WHRID='" + WHRID + "'";
                        cmd = new SqlCommand(qry, con, sqltran);
                        int df = cmd.ExecuteNonQuery();
                    }

                    qry = "update [tbl_Opening_Delete_Req] set  DeleteStatus='Y' where whr_id='" + WHRID + "' and Req_id='" + reqid + "'";
                    cmd = new SqlCommand(qry, con, sqltran);
                    int rex22 = cmd.ExecuteNonQuery();
                }


                count++;




                sqltran.Commit();
                con.Close();
            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully deleted .')", true);
                F_SelectwhrData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast one Record for Deleting..')", true);
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record not Deleted .')", true);
        }
        finally
        {
            sqltran.Dispose();
            con.Close();
            //FillGridOnLoad();
        }
    }
    protected void gvgatepass_SelectedIndexChanged(object sender, EventArgs e)
    {
        int count = 0;
        string qry = "";
        try
        {
            if (gvgatepass.Rows.Count > 0)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string Gatepass = gvgatepass.SelectedRow.Cells[4].Text.ToString();
                string Branch = gvgatepass.SelectedRow.Cells[2].Text.ToString();
                string reqid = gvgatepass.SelectedRow.Cells[1].Text.ToString();

                //   qry = "Insert Into tbl_Storage_GatePass_Enrty_DeleteLog SELECT [GatePass_No],[State_ID],[District_ID],[Depot_ID],[Godown_ID],[Stack_ID],[Depositor/Issuer_Name],[Commodity_ID],[Scheme_ID],[Vehicle_Type],[Vehicle_No],[Driver_Name],[NO_of_Bage],[Weight],[Issue_Source],[Issue_Source_ID],[CreatedBy],[Issue_Date],[Status],[Printed] ,[License_No],[Valid_Upto],[Arrival_Dep_Time],[Remarks],[Miller_Id],[ReasonforCancelation],'" + ip + "',getdate(),[GP_FIN_YR],[GP_SL_No],[OGatePass_No] from tbl_Storage_GatePass_Enrty where GatePass_No='" + Gatepass + "'";
                qry = "Insert Into tbl_Storage_GatePass_Enrty_DeleteLog SELECT [GatePass_No],[State_ID],[District_ID],[Depot_ID],[Godown_ID],[Stack_ID],[Depositor/Issuer_Name],[Commodity_ID],[Scheme_ID],[Vehicle_Type],[Vehicle_No],[Driver_Name],[NO_of_Bage],[Weight],[Issue_Source],[Issue_Source_ID],[CreatedBy],[Issue_Date],[Status],[Printed],[License_No],[Valid_Upto],[Arrival_Dep_Time],[Remarks],[Miller_Id],[ReasonforCancelation],'" + ip + "',getdate(),[GP_FIN_YR],[GP_SL_No],OGatePass_No,[BranchID],[GodownNam] from  tbl_Storage_GatePass_Enrty where GatePass_No='" + Gatepass + "'";
                cmd = new SqlCommand(qry, con);
                int c = cmd.ExecuteNonQuery();
                if (c > 0)
                {
                    qry = "Delete from tbl_Storage_GatePass_Enrty where GatePass_No='" + Gatepass + "'";
                    cmd = new SqlCommand(qry, con);
                    int d = cmd.ExecuteNonQuery();
                    if (d > 0)
                    {
                        qry = "insert into [tbl_RO_Details_Log] SELECT [Trans_ID],[State_Id],[District_Id],[DepotId],[Commodity_Id],[Release_Order_No],[Release_Order_Date],[RO_Quantity],[FPSId],[FPS],[Truckno],[GatePass_No],[RO_Quantity_issued],[allotment_month],[allotment_year],'" + ip + "',GETDATE(),[BranchID] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_RO_Details] where GatePass_No='" + Gatepass + "'";
                        cmd = new SqlCommand(qry, con);
                        int t = cmd.ExecuteNonQuery();
                        if (t >= 0)
                        {

                            qry = "Delete from tbl_RO_Details where GatePass_No = '" + Gatepass + "'";
                            cmd = new SqlCommand(qry, con);
                            int x = cmd.ExecuteNonQuery();
                            if (x >= 0)
                            {
                                qry = "insert into  tbl_Storage_Final_Stock_Delivery_GatePass_DelLog Select [StockDeliveryOrderGatePass_Id] ,[State_Id],[District_Id],[DepotId],[Commodity_Id],[Category_Id],[WHR_Id],[Delivery_Order_No],[Delivery_Order_Date],[Qty_Issued_No_Bags_Sound] ,[Qty_Issued_No_Bags_Spilage],[Qty_Issued_Weight],[Value_Stock_Delivered],[Moisture_Content],[Purpose_Of_Issue],[Issue_within_Outside],[Rental_Amt_Received],[Cash_Credit],[DD_No],[DD_Date],[DD_BankId],[Sample_Serial_No],[Vikas_Chand],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[RecipientDistrict],[RecipientDepot],[TransporterId],[DeliverdAgent],[Districtdeliverd],[deliveredName],[DelveredAddress],[CWCstatus],[FPSId],[FPS],[GatePass_No],[DeliveryOrderID],[trans_id] from tbl_Storage_Final_Stock_Delivery_GatePass where GatePass_No = '" + Gatepass + "'";
                                qry = "insert into tbl_Storage_Final_Stock_Delivery_GatePass_DelLog select * from tbl_Storage_Final_Stock_Delivery_GatePass where  BranchID='" + Branch + "' and  tbl_Storage_Final_Stock_Delivery_GatePass.GatePass_No='" + Gatepass + "'";
                                cmd = new SqlCommand(qry, con);
                                int y = cmd.ExecuteNonQuery();
                                if (y > 0)
                                {
                                    qry = "Delete from tbl_Storage_Final_Stock_Delivery_GatePass where GatePass_No='" + Gatepass + "'";
                                    cmd = new SqlCommand(qry, con);
                                    int f = cmd.ExecuteNonQuery();
                                }

                                /////////////////////////////////////////////

                                qry = "insert into tbl_Delivery_Stacking_Details_GatePass_DeleteLog select [Godown_ID],[Stack_ID],[No_Of_Bags],[Bags_Weight],[CreatedBy],[CreatedDate] ,[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[Depositor_WHR_Id],[StockDeliveryOrderGatePass_Id],[Autoid] ,[Loss],[Gain],[GatePass_No] from tbl_Delivery_Stacking_Details_GatePass  where GatePass_No = '" + Gatepass + "'";
                                cmd = new SqlCommand(qry, con);
                                int z = cmd.ExecuteNonQuery();
                                if (z > 0)
                                {
                                    qry = "Delete from tbl_Delivery_Stacking_Details_GatePass where GatePass_No='" + Gatepass + "'";
                                    cmd = new SqlCommand(qry, con);
                                    int i = cmd.ExecuteNonQuery();
                                }


                                qry = "update tbl_Gatepass_Delete_Req set  DeleteStatus='Y' where  Req_id='" + reqid + "'";
                                cmd = new SqlCommand(qry, con, sqltran);
                                int rex22 = cmd.ExecuteNonQuery();
                            }
                        }
                    }

                }


                count++;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully deleted')", true);
                F_SelectGatepassData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Check WHR Delevery status')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }
        finally
        {
            con.Close();
        }
    }
    protected void gvdepositorwhr_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int indx = e.NewPageIndex;
            gvdepositorwhr.PageIndex = e.NewPageIndex;
            F_SelectDepositorwhrData();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    //protected void gvdepositorwhr_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Reject")
    //    {
    //        int count = 0;
    //        try
    //        {
    //            if (gvdepositorwhr.Rows.Count > 0)
    //            {
    //                if (con.State == ConnectionState.Closed)
    //                {
    //                    con.Open();
    //                }
    //                string qry = "";
    //                string WHRID = gvdepositorwhr.SelectedRow.Cells[6].Text.ToString();
    //                string Branch = gvdepositorwhr.SelectedRow.Cells[3].Text.ToString();
    //                string reqid = gvdepositorwhr.SelectedRow.Cells[2].Text.ToString();
    //                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

    //                //Stackid = drs[17].ToString();


    //                qry = "update [tbl_Opening_Delete_Req] set  DeleteStatus='Y' where whr_id='" + WHRID + "' and Req_id='" + reqid + "'";
    //                cmd = new SqlCommand(qry, con, sqltran);
    //                int rex22 = cmd.ExecuteNonQuery();

    //                count++;


    //            }
    //            else
    //            {
    //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
    //            }
    //            if (count > 0)
    //            {
    //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully Updated')", true);
    //                F_SelectDepositorwhrData();
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast One Record For Deleting')", true);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
    //        }
    //        finally
    //        {
    //            con.Close();
    //        }
    //    }
    //}
    //protected void gvdepositorwhr_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int count = 0;
    //    try
    //    {
    //        if (gvdepositorwhr.Rows.Count > 0)
    //        {
    //            if (con.State == ConnectionState.Closed)
    //            {
    //                con.Open();
    //            }
    //            string qry = "";
    //            string WHRID = gvdepositorwhr.SelectedRow.Cells[6].Text.ToString();
    //            string Branch = gvdepositorwhr.SelectedRow.Cells[3].Text.ToString();
    //            string reqid = gvdepositorwhr.SelectedRow.Cells[2].Text.ToString();
    //            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();


    //            string query = "SELECT * FROM [tbl_Delivery_Stacking_Details_GatePass] where Depositor_WHR_Id='" + WHRID + "'";
    //            SqlCommand cmd = new SqlCommand(query, con);
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            DataSet ds1 = new DataSet();
    //            da.Fill(ds1);

    //            if (ds1.Tables[0].Rows.Count == 0)
    //            {
    //                //Stackid = drs[17].ToString();
    //                if (con.State == ConnectionState.Closed)
    //                {
    //                    con.Open();
    //                }
    //                sqltran = con.BeginTransaction();
    //                qry = "Insert Into [tbl_storage_Depositor_WHR_Relation_Log] SELECT [Depositor_WHR_Id],[State_Id],[District_Id],[Depotid],[Commodity_Id],[Category_Id],[Whr_No],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[Mode_of_weighment],[BeamScale_LWB],[AvgMoisture_Content],[Lot_No] ,[MktValue_of_Commodity],[Arrival_Source],[WHR_Issue_Date],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[MadeUpBags],[Client_IP],[AvgMoisture_Content_To],[Did] ,[CropYear],[Remark] ,[SangrahadDate],[LicenseNo],[LicenseDate] ,[wday],[wmon],[wyear],[BranchID],[DepositorID],GodownID,Gid,'S','D',FAQ_Stock,Non_FAQ_Stock,DCC_Stock,FAQUpdateDate from tbl_storage_Depositor_WHR_Relation where Depositor_WHR_Id='" + WHRID + "'";
    //                cmd = new SqlCommand(qry, con, sqltran);
    //                int c = cmd.ExecuteNonQuery();
    //                if (c > 0)
    //                {
    //                    qry = "Delete from tbl_storage_Depositor_WHR_Relation where Depositor_WHR_Id='" + WHRID + "'";
    //                    cmd = new SqlCommand(qry, con, sqltran);
    //                    int d = cmd.ExecuteNonQuery();
    //                    if (d > 0)
    //                    {
    //                        qry = "insert into whr_status_log select WhrId,Statusflag,getdate(),'" + ip + "' from whr_status where WhrId = '" + WHRID + "'";
    //                        cmd = new SqlCommand(qry, con, sqltran);
    //                        int x1 = cmd.ExecuteNonQuery();
    //                        if (x1 > 0)
    //                        {
    //                            qry = "Delete from whr_status where WhrId = '" + WHRID + "'";
    //                            cmd = new SqlCommand(qry, con, sqltran);
    //                            int x = cmd.ExecuteNonQuery();
    //                            if (x > 0)
    //                            {
    //                                qry = "Insert Into tbl_Storage_Receipt_Details_DeleteLog select [StorageReceipt_Id],[State_Id],[District_Id],[Depotid],[Commodity_Id],[Category_Id],[WHR_Flag],[WHR_Id],[Grade_Name],[Receipt_Date],[Gate_PassNo],[Mode_of_weighment],[BeamScale_LWB],[Qty_Rvd_No_of_Bags],[Qty_Rvd_Weight],[Supply_gunny_New],[Supply_gunny_Old],[Godown_Delivery_No],[Distance_From_PC],[Ack_Book_No],[Ack_Serial_No],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[AnalysisStatus],[Depositortype],[Supply_gunny_Once_used],[Type_of_gunny],[DepositorName],[ArrivalSource_ID],[Remarks],[Acpt_FCIRO_No],[Acpt_FCIRO_Date],[Client_IP],[IssueID],[BranchId],Rid from tbl_Storage_Receipt_Details where WHR_Id='" + WHRID.ToString() + "'  and BranchID='" + Branch + "'";
    //                                cmd = new SqlCommand(qry, con, sqltran);
    //                                int rex1 = cmd.ExecuteNonQuery();

    //                                qry = "Update tbl_Storage_Receipt_Details Set WHR_Flag = 'N',WHR_Id = null where WHR_Id = '" + WHRID + "'";
    //                                cmd = new SqlCommand(qry, con, sqltran);
    //                                int y = cmd.ExecuteNonQuery();

    //                                ///////////////////////////////////////////////
    //                                qry = "Insert Into tbl_storage_Stacking_Details_DeleteLog ([State_Id],[District_Id],[Depotid],[Godown_ID],[Stack_ID],[StorageReceipt_Id],[Bags],[Weight],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[DeletedBy],[DeletedDate],[autoid],[WHRId],[Status],[Branchid])  select [State_Id],[District_Id],[Depotid],[Godown_ID],[Stack_ID],[StorageReceipt_Id],[Bags],[Weight],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[autoid],[WHRId],[Status],[Branchid] from tbl_storage_Stacking_Details where WHRId='" + WHRID + "'  and Branchid='" + Branch + "'";
    //                                cmd = new SqlCommand(qry, con, sqltran);
    //                                int rex2 = cmd.ExecuteNonQuery();


    //                                qry = "Update tbl_storage_Stacking_Details Set WHRId = null where WHRId = '" + WHRID + "' ";
    //                                cmd = new SqlCommand(qry, con, sqltran);
    //                                int z = cmd.ExecuteNonQuery();



    //                                qry = " insert into [dbo].[whrprintstatus_Delete_Log] select [WHRID],[PrintStatus],[DateCreated],GETDATE(),[IsActive] from whrprintstatus where WHRID='" + WHRID + "'";
    //                                cmd = new SqlCommand(qry, con, sqltran);
    //                                int rt = cmd.ExecuteNonQuery();

    //                                qry = " delete from  dbo.whrprintstatus where WHRID='" + WHRID + "'";
    //                                cmd = new SqlCommand(qry, con, sqltran);
    //                                int df = cmd.ExecuteNonQuery();

    //                                qry = "update [tbl_Opening_Delete_Req] set  DeleteStatus='Y' where whr_id='" + WHRID + "' and Req_id='" + reqid + "'";
    //                                cmd = new SqlCommand(qry, con, sqltran);
    //                                int rex22 = cmd.ExecuteNonQuery();


    //                            }
    //                        }
    //                    }
    //                    count++;
    //                }
    //                sqltran.Commit();
    //                con.Close();

    //            }
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
    //        }
    //        if (count > 0)
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully deleted')", true);
    //            F_SelectDepositorwhrData();
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast One Record For Deleting')", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        sqltran.Rollback();
    //        con.Close();
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
    //    }
    //    finally
    //    {
    //        con.Close();
    //    }
    //}


    //protected void gvdepositorwhr_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        if (e.CommandName == "DeleteRow" || e.CommandName == "Reject")
    //        {
    //            int rowIndex = Convert.ToInt32(e.CommandArgument);
    //            GridViewRow row = gvdepositorwhr.Rows[rowIndex];

    //            // WHR, ReqId, Branch nikalna
    //            string WHRID = gvdepositorwhr.DataKeys[rowIndex].Values["whr_id"].ToString();
    //            string reqid = gvdepositorwhr.DataKeys[rowIndex].Values["Req_id"].ToString();
    //            string Branch = gvdepositorwhr.DataKeys[rowIndex].Values["Depotid"].ToString();
    //            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

    //            if (con.State == ConnectionState.Closed)
    //                con.Open();

    //            SqlTransaction sqltran = con.BeginTransaction();
    //            SqlCommand cmd;

    //            if (e.CommandName == "Reject")
    //            {
    //                string qry = "update [tbl_Opening_Delete_Req] set DeleteStatus='Y' where whr_id=@WHRID and Req_id=@Reqid";
    //                cmd = new SqlCommand(qry, con, sqltran);
    //                cmd.Parameters.AddWithValue("@WHRID", WHRID);
    //                cmd.Parameters.AddWithValue("@Reqid", reqid);

    //                int result = cmd.ExecuteNonQuery();
    //                sqltran.Commit();

    //                if (result > 0)
    //                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "msg", "alert('Record rejected successfully!');", true);
    //                else
    //                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "msg", "alert('No record found to reject!');", true);
    //            }

    //            else if (e.CommandName == "DeleteRow")
    //            {
    //                // Yahan aapka delete wala pura logic (SelectedIndexChanged wala) dalna hai
    //                // WHRID aur Branch ab yahan available hai
    //                // ...
    //                sqltran.Commit();
    //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "msg", "alert('Record deleted successfully!');", true);


    //            }

    //            F_SelectDepositorwhrData();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "msg", "alert('" + ex.Message.Replace("'", "") + "');", true);
    //    }
    //    finally
    //    {
    //        if (con.State == ConnectionState.Open)
    //            con.Close();
    //    }
    //}


    protected void gvdepositorwhr_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int count = 0;

        try
        {
            if (e.CommandName == "Reject" || e.CommandName == "DeleteRow")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvdepositorwhr.Rows[rowIndex];

                // WHR, ReqId, Branch nikalna from DataKeys
                //string WHRID = gvdepositorwhr.DataKeys[rowIndex].Values["whr_id"].ToString();
                //string reqid = gvdepositorwhr.DataKeys[rowIndex].Values["Req_id"].ToString();
                //string Branch = gvdepositorwhr.DataKeys[rowIndex].Values["Depotid"].ToString();
                string qry = "";
                //string WHRID = gvdepositorwhr.SelectedRow.Cells[6].Text.ToString();
                //string Branch = gvdepositorwhr.SelectedRow.Cells[3].Text.ToString();
                //string reqid = gvdepositorwhr.SelectedRow.Cells[2].Text.ToString();
                string WHRID = gvdepositorwhr.DataKeys[rowIndex].Values["whr_id"].ToString();
                string reqid = gvdepositorwhr.DataKeys[rowIndex].Values["Req_id"].ToString();
                string Branch = gvdepositorwhr.DataKeys[rowIndex].Values["Depotid"].ToString();
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (e.CommandName == "Reject")
                {
                    SqlTransaction tran = con.BeginTransaction();
                    qry = "update [tbl_Opening_Delete_Req] set DeleteStatus='Y' where whr_id=@WHRID and Req_id=@Reqid";
                    SqlCommand cmd = new SqlCommand(qry, con, tran);
                    cmd.Parameters.AddWithValue("@WHRID", WHRID);
                    cmd.Parameters.AddWithValue("@Reqid", reqid);
                    int result = cmd.ExecuteNonQuery();
                    tran.Commit();

                    if (result > 0)
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "msg", "alert('Record rejected successfully!');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "msg", "alert('No record found to reject!');", true);
                }

                else if (e.CommandName == "DeleteRow")
                {
                    string query = "SELECT * FROM [tbl_Delivery_Stacking_Details_GatePass] where Depositor_WHR_Id='" + WHRID + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds1 = new DataSet();
                    da.Fill(ds1);

                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        //Stackid = drs[17].ToString();
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        sqltran = con.BeginTransaction();
                        qry = "Insert Into [tbl_storage_Depositor_WHR_Relation_Log] SELECT [Depositor_WHR_Id],[State_Id],[District_Id],[Depotid],[Commodity_Id],[Category_Id],[Whr_No],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[Mode_of_weighment],[BeamScale_LWB],[AvgMoisture_Content],[Lot_No] ,[MktValue_of_Commodity],[Arrival_Source],[WHR_Issue_Date],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[MadeUpBags],[Client_IP],[AvgMoisture_Content_To],[Did] ,[CropYear],[Remark] ,[SangrahadDate],[LicenseNo],[LicenseDate] ,[wday],[wmon],[wyear],[BranchID],[DepositorID],GodownID,Gid,'S','D',FAQ_Stock,Non_FAQ_Stock,DCC_Stock,FAQUpdateDate,infestedstock,doughformation,foreignmatter,badgunnybags,SchemeName from tbl_storage_Depositor_WHR_Relation where Depositor_WHR_Id='" + WHRID + "'";
                        cmd = new SqlCommand(qry, con, sqltran);
                        int c = cmd.ExecuteNonQuery();
                        if (c > 0)
                        {
                            qry = "Delete from tbl_storage_Depositor_WHR_Relation where Depositor_WHR_Id='" + WHRID + "'";
                            cmd = new SqlCommand(qry, con, sqltran);
                            int d = cmd.ExecuteNonQuery();
                            if (d > 0)
                            {
                                qry = "insert into whr_status_log select WhrId,Statusflag,getdate(),'" + ip + "' from whr_status where WhrId = '" + WHRID + "'";
                                cmd = new SqlCommand(qry, con, sqltran);
                                int x1 = cmd.ExecuteNonQuery();
                                if (x1 > 0)
                                {
                                    qry = "Delete from whr_status where WhrId = '" + WHRID + "'";
                                    cmd = new SqlCommand(qry, con, sqltran);
                                    int x = cmd.ExecuteNonQuery();
                                    if (x > 0)
                                    {
                                        qry = "Insert Into tbl_Storage_Receipt_Details_DeleteLog select [StorageReceipt_Id],[State_Id],[District_Id],[Depotid],[Commodity_Id],[Category_Id],[WHR_Flag],[WHR_Id],[Grade_Name],[Receipt_Date],[Gate_PassNo],[Mode_of_weighment],[BeamScale_LWB],[Qty_Rvd_No_of_Bags],[Qty_Rvd_Weight],[Supply_gunny_New],[Supply_gunny_Old],[Godown_Delivery_No],[Distance_From_PC],[Ack_Book_No],[Ack_Serial_No],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[AnalysisStatus],[Depositortype],[Supply_gunny_Once_used],[Type_of_gunny],[DepositorName],[ArrivalSource_ID],[Remarks],[Acpt_FCIRO_No],[Acpt_FCIRO_Date],[Client_IP],[IssueID],[BranchId],Rid from tbl_Storage_Receipt_Details where WHR_Id='" + WHRID.ToString() + "'  and BranchID='" + Branch + "'";
                                        cmd = new SqlCommand(qry, con, sqltran);
                                        int rex1 = cmd.ExecuteNonQuery();

                                        qry = "Update tbl_Storage_Receipt_Details Set WHR_Flag = 'N',WHR_Id = null where WHR_Id = '" + WHRID + "'";
                                        cmd = new SqlCommand(qry, con, sqltran);
                                        int y = cmd.ExecuteNonQuery();

                                        ///////////////////////////////////////////////
                                        qry = "Insert Into tbl_storage_Stacking_Details_DeleteLog ([State_Id],[District_Id],[Depotid],[Godown_ID],[Stack_ID],[StorageReceipt_Id],[Bags],[Weight],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[DeletedBy],[DeletedDate],[autoid],[WHRId],[Status],[Branchid])  select [State_Id],[District_Id],[Depotid],[Godown_ID],[Stack_ID],[StorageReceipt_Id],[Bags],[Weight],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[autoid],[WHRId],[Status],[Branchid] from tbl_storage_Stacking_Details where WHRId='" + WHRID + "'  and Branchid='" + Branch + "'";
                                        cmd = new SqlCommand(qry, con, sqltran);
                                        int rex2 = cmd.ExecuteNonQuery();


                                        qry = "Update tbl_storage_Stacking_Details Set WHRId = null where WHRId = '" + WHRID + "' ";
                                        cmd = new SqlCommand(qry, con, sqltran);
                                        int z = cmd.ExecuteNonQuery();



                                        qry = " insert into [dbo].[whrprintstatus_Delete_Log] select [WHRID],[PrintStatus],[DateCreated],GETDATE(),[IsActive] from whrprintstatus where WHRID='" + WHRID + "'";
                                        cmd = new SqlCommand(qry, con, sqltran);
                                        int rt = cmd.ExecuteNonQuery();

                                        qry = " delete from  dbo.whrprintstatus where WHRID='" + WHRID + "'";
                                        cmd = new SqlCommand(qry, con, sqltran);
                                        int df = cmd.ExecuteNonQuery();

                                        qry = "update [tbl_Opening_Delete_Req] set  DeleteStatus='Y' where whr_id='" + WHRID + "' and Req_id='" + reqid + "'";
                                        cmd = new SqlCommand(qry, con, sqltran);
                                        int rex22 = cmd.ExecuteNonQuery();


                                    }
                                }
                            }
                            count++;
                        }
                        sqltran.Commit();
                        con.Close();

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
                }
                if (count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully deleted')", true);
                    F_SelectDepositorwhrData();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('WHR डिलीट करने से पहले DO डिलीट करे !!!')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "msg", "alert('" + ex.Message.Replace("'", "") + "');", true);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }


    protected void gvreceipt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "reject")
        {

            int count = 0;
            try
            {
                if (gvreceipt.Rows.Count > 0)
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    sqltran = con.BeginTransaction();
                    //foreach (GridViewRow gr2 in gvreceipt.Rows)
                    //{
                    DataSet ds = (DataSet)Session["ds_GridInfo"];
                    // CheckBox chk_Delete = new CheckBox();
                    string ArrivalStock_Id = gvreceipt.SelectedRow.Cells[6].Text.ToString();
                    string Branch = gvreceipt.SelectedRow.Cells[3].Text.ToString();
                    string Receiptid = gvreceipt.SelectedRow.Cells[5].Text.ToString();
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string reqid = gvreceipt.SelectedRow.Cells[2].Text.ToString();

                    string qry = "";


                    qry = "update [tbl_Receipt_Delete_Req] set  DeleteStatus='R' where  Req_id='" + reqid + "'";
                    cmd = new SqlCommand(qry, con, sqltran);
                    int rex22 = cmd.ExecuteNonQuery();
                    count++;
                    // }
                    //  }
                    //   }
                    sqltran.Commit();
                    con.Close();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
                }
                if (count > 0)
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully Updated')", true);
                    F_Selectreciptdata();
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast One Record For Deleting')", true);
                }
            }
            catch (Exception ex)
            {
                sqltran.Rollback();

            }
            finally
            {
                con.Close();
            }

        }
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetRegion();
        // F_Selectreciptdata();
        //F_SelectwhrData();
        //F_SelectGatepassData();
        F_SelectDepositorwhrData();
    }
}