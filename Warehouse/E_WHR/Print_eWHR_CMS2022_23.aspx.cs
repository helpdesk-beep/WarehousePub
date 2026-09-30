using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


public partial class E_WHR_Print_eWHR_CMS2022_23 : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    String District = "";
    String Depot = "";
    string Language = "";
    string ArrivalSource = "";
    string Branch = "";

    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["RoleId"] != null && Session["RoleId"] != "")
        {
            if (!IsPostBack)
            {
                //to aboide multiple click
                string var = ClientScript.GetPostBackEventReference(btnprint, "").ToString();
                btnprint.Attributes.Add("onClick", "javascript :if ( Page_ClientValidate() ){this.disabled=true; this.value='Please Wait...';" + var + "};");
                fillDistrict();
                //fillwhrList();
                Panel1.Visible = false;
                btnprint.Visible = false;
                btnbmpassword.Enabled = false;
                //txtWHR.Visible = false;

            }
            Printcurrentdate();
            ListView2.DataSource = null;
            ListView2.DataBind();
            lvoffice.DataSource = null;
            lvoffice.DataBind();
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void fillDistrict()
    {
        try
        {
            string UserId = Session["RoleId"].ToString();
            string query = "";
            //if (UserId == "10")
            //{
            query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            //query = "select distcd4 as District_Id,district_name as District_Name from mpscsc.pds.[districtsmp] where CSM_MArkfed='MPSCSC' and district_name!='State HQ' order by district_name asc";

            //}
            //else if (UserId == "9")
            //{
            //    query = "select distcd4 as District_Id,district_name as District_Name from mpscsc.pds.[districtsmp] where CSM_MArkfed='MARKFED' and district_name!='State HQ' order by district_name asc";
            //}
            cmd = new SqlCommand(query, Con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "---Select---");
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
    private void fillwhrList()
    {
        if (ddlCommodity.SelectedValue != "-1")
        {
            //if (Session["Depot_DistID"] != null)
            {
                DDLwhr.Items.Clear();
                District = ddlDistrict.SelectedValue.ToString();
                //Depot = Session["Depot_DepotID"].ToString();
                Branch = ddlDepotList.SelectedValue.ToString();
                string query = "";

                //if (ddlCommodity.SelectedValue == "52")
                //{
                //    query = "select distinct Depositor_whr_id,WHR_Issue_Date from tbl_storage_Depositor_WHR_Relation as WHR inner join tbl_DSC_eWHR_Submission as SWHR on SWHR.WHR_Id=WHR.Depositor_WHR_Id and WHR.BranchID=SWHR.BranchId inner join tbl_DSC_User_Upload_Detail as U on U.SerialNumber=SWHR.BSerial_No where WHR.BranchID='" + Branch + "' and SWHR.Commodity_Id='" + ddlCommodity.SelectedValue + "' and SWHR.DistrictId='" + District + "' and WHR.Gid is null and U.User_Type='B' and SWHR.PrintedIp='' and WHR.Depositor_WHR_Id in (select S.Depositor_whr_id from View_Digitally_Signed_WHR_Detail as S where User_Type='B')";
                //}
                //else
                //{
                //query = "select distinct Depositor_whr_id,WHR_Issue_Date from tbl_storage_Depositor_WHR_Relation as WHR inner join tbl_DSC_eWHR_Submission as SWHR on SWHR.WHR_Id=WHR.Depositor_WHR_Id and WHR.BranchID=SWHR.BranchId inner join tbl_DSC_User_Upload_Detail as U on U.SerialNumber=SWHR.BSerial_No where WHR.BranchID='" + Branch + "' and SWHR.Commodity_Id='" + ddlCommodity.SelectedValue + "' and SWHR.DistrictId='" + District + "' and WHR.Gid is null and U.User_Type='B' and SWHR.PrintedIp='' and WHR.Depositor_WHR_Id in (select S.Depositor_whr_id from View_Digitally_Signed_WHR_Detail as S where User_Type='B')";
                query = "select distinct Depositor_whr_id,WHR_Issue_Date from tbl_storage_Depositor_WHR_Relation as WHR inner join tbl_DSC_eWHR_Submission_CMS2022 as SWHR on SWHR.WHR_Id=WHR.Depositor_WHR_Id and WHR.BranchID=SWHR.BranchId inner join tbl_DSC_User_Upload_Detail as U on U.SerialNumber=SWHR.BSerial_No where WHR.BranchID='" + Branch + "' and SWHR.Commodity_Id='" + ddlCommodity.SelectedValue + "' and SWHR.DistrictId='" + District + "' and WHR.Gid is null and U.User_Type='B' and SWHR.PrintedIp='' and WHR.Depositor_WHR_Id in (select S.Depositor_whr_id from View_Digitally_Signed_WHR_CMS2022 as S where User_Type='B')";

                //}


                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DDLwhr.DataSource = ds.Tables[0];
                    DDLwhr.DataTextField = "Depositor_whr_id";
                    DDLwhr.DataValueField = "Depositor_whr_id";
                    DDLwhr.DataBind();
                    DDLwhr.Items.Insert(0, "--Select--");
                }
                else
                {
                    DDLwhr.Items.Insert(0, "--Select--");
                }
            }
        }
    }

    protected void Printcurrentdate()
    {
        try
        {
            string query = "SELECT  getdate() as 'Date1'";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbldatetime.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
                lblcuurentdateoff.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
    }
    private void whrdetail()
    {
        if (ddlDistrict.SelectedValue.ToString() != null)
        {
            try
            {
                string query = "";
                string whr = "";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                if (DDLwhr.SelectedIndex != 0)
                {
                    whr = DDLwhr.SelectedItem.Text;
                }
                else
                {
                    whr = TextBox1.Text;
                }
                District = ddlDistrict.SelectedValue.ToString();
                Depot = ddlDepotList.SelectedValue.ToString();
                Branch = ddlDepotList.SelectedValue.ToString();
                //Get Arrival Type
                GetArrivalTypeofWHR();
                if (ArrivalSource == "BS")
                {
                    //Bhavanter Scheme
                    query = "SELECT DISTINCT WHR.Depositor_WHR_Id,WHR.Commodity_Id,('(' +CONVERT(NVARCHAR(50),(convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,2),WHR.Total_Qty_Received))) + ')'+ dbo.[AnkNumberToWords](convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,2),WHR.Total_Qty_Received))) as MktValue_of_Commodity,convert(decimal(18,2),WHR.MktValue_of_Commodity) as MktValue_of_Commodityno, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name, WHR.Depositor_Name, LicenseNo,LicenseDate ,tbl_MetaData_DEPOT.DepotName, CONVERT(NVARCHAR(10), WHR.Date_of_Deposit, 103) AS Depositdate,CONVERT(NVARCHAR(10), WHR.WHR_Issue_Date, 103) AS whrDate ,CONVERT(NVARCHAR(10), WHR.SangrahadDate, 103) AS StorageRateDate,('Moisture%'+CONVERT(NVARCHAR(250),((convert(decimal(18,2),WHR.AvgMoisture_Content)+convert(decimal(18,2),WHR.AvgMoisture_Content_To))/2))+' '+WHR.Remark+' Bhavantar Yojna') as Remark ,WHR.TotalBags_Received,  convert(decimal(18,4),WHR.Total_Qty_Received) as Total_Qty_Received, tbl_MetaData_GODOWN.Godown_Name, tbl_MetaData_STORAGE_CATEGORY.Category_Name FROM   tbl_storage_Depositor_WHR_Relation AS WHR INNER JOIN tbl_storage_Stacking_Details ON WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId INNER JOIN tbl_MetaData_STORAGE_CATEGORY ON WHR.Category_Id = tbl_MetaData_STORAGE_CATEGORY.Category_Id INNER JOIN tbl_MetaData_STORAGE_COMMODITY ON WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id INNER JOIN tbl_MetaData_GODOWN ON tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID INNER JOIN tbl_MetaData_DEPOT ON WHR.Depotid = tbl_MetaData_DEPOT.DepotID WHERE (WHR.Depositor_WHR_Id = '" + whr + "' and WHR.BranchID='" + Branch + "'  )";
                }
                else
                {
                    //query = "SELECT DISTINCT DSC.Depositor_WHR_Id,DSC.Commodity_Id,('(' +CONVERT(NVARCHAR(50),(convert(decimal(18,2),DSC.MktValue_of_Commodity)*convert(decimal(18,2),DSC.Total_Qty_Received))) + ')'+ dbo.[AnkNumberToWords](convert(decimal(18,2),DSC.MktValue_of_Commodity)*convert(decimal(18,2),DSC.Total_Qty_Received))) as MktValue_of_Commodity,convert(decimal(18,2),DSC.MktValue_of_Commodity) as MktValue_of_Commodityno, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name, DSC.Depositor_Name, LicenseNo,LicenseDate ,tbl_MetaData_DEPOT.DepotName, CONVERT(NVARCHAR(10), DSC.Date_of_Deposit, 103) AS Depositdate,CONVERT(NVARCHAR(10), DSC.WHR_Issue_Date, 103) AS whrDate,CONVERT(NVARCHAR(10), DSC.SangrahadDate, 103) AS StorageRateDate,('Moisture%'+CONVERT(NVARCHAR(250),((convert(decimal(18,2),DSC.AvgMoisture_Content)+convert(decimal(18,2),DSC.AvgMoisture_Content_To))/2))+' '+DSC.Remark) as Remark,DSC.TotalBags_Received,  convert(decimal(18,4),DSC.Total_Qty_Received) as Total_Qty_Received, tbl_MetaData_GODOWN.Godown_Name, tbl_MetaData_STORAGE_CATEGORY.Category_Name FROM tbl_storage_Depositor_WHR_Relation AS WHR INNER JOIN tbl_storage_Stacking_Details ON WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId INNER JOIN tbl_MetaData_STORAGE_CATEGORY ON WHR.Category_Id = tbl_MetaData_STORAGE_CATEGORY.Category_Id INNER JOIN tbl_MetaData_STORAGE_COMMODITY ON WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id INNER JOIN tbl_MetaData_GODOWN ON tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID INNER JOIN tbl_MetaData_DEPOT ON WHR.Depotid = tbl_MetaData_DEPOT.DepotID inner join tbl_Digitally_Signed_WHR_Details as DSC on DSC.Depositor_WHR_Id=WHR.Depositor_WHR_Id WHERE (WHR.Depositor_WHR_Id = '" + whr + "' and WHR.BranchID='" + Branch + "' AND WHR.Gid IS NULL and WHR.Depositor_WHR_Id in (select S.Depositor_whr_id from View_Digitally_Signed_WHR_Detail as S where S.User_Type='B') and WHR.Depositor_WHR_Id in (select WHR_Id from tbl_DSC_eWHR_Submission where WHR_Id='" + whr + "' and PrintedIp=''))";
                    //query = "SELECT DISTINCT DSC.Depositor_WHR_Id,DSC.Commodity_Id,('(' +CONVERT(NVARCHAR(50),(convert(decimal(18,2),DSC.MktValue_of_Commodity)*convert(decimal(18,2),DSC.Total_Qty_Received))) + ')'+ dbo.[AnkNumberToWords](convert(decimal(18,2),DSC.MktValue_of_Commodity)*convert(decimal(18,2),DSC.Total_Qty_Received))) as MktValue_of_Commodity,convert(decimal(18,2),DSC.MktValue_of_Commodity) as MktValue_of_Commodityno, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name, DSC.Depositor_Name, LicenseNo,LicenseDate ,tbl_MetaData_DEPOT.DepotName, CONVERT(NVARCHAR(10), DSC.Date_of_Deposit, 103) AS Depositdate,CONVERT(NVARCHAR(10), DSC.WHR_Issue_Date, 103) AS whrDate,CONVERT(NVARCHAR(10), DSC.SangrahadDate, 103) AS StorageRateDate,('Moisture%'+CONVERT(NVARCHAR(250),((convert(decimal(18,2),DSC.AvgMoisture_Content)+convert(decimal(18,2),DSC.AvgMoisture_Content_To))/2))+' '+DSC.Remark) as Remark,DSC.TotalBags_Received,  convert(decimal(18,4),DSC.Total_Qty_Received) as Total_Qty_Received, tbl_MetaData_GODOWN.Godown_Name, tbl_MetaData_STORAGE_CATEGORY.Category_Name FROM tbl_storage_Depositor_WHR_Relation AS WHR INNER JOIN tbl_storage_Stacking_Details ON WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId INNER JOIN tbl_MetaData_STORAGE_CATEGORY ON WHR.Category_Id = tbl_MetaData_STORAGE_CATEGORY.Category_Id INNER JOIN tbl_MetaData_STORAGE_COMMODITY ON WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id INNER JOIN tbl_MetaData_GODOWN ON tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID INNER JOIN tbl_MetaData_DEPOT ON WHR.Depotid = tbl_MetaData_DEPOT.DepotID inner join tbl_Digitally_Signed_WHR_CMS2020 as DSC on DSC.Depositor_WHR_Id=WHR.Depositor_WHR_Id WHERE (WHR.Depositor_WHR_Id = '" + whr + "' and WHR.BranchID='" + Branch + "' AND WHR.Gid IS NULL and WHR.Depositor_WHR_Id in (select S.Depositor_whr_id from View_Digitally_Signed_WHR_CMS2020 as S where S.User_Type='B') and WHR.Depositor_WHR_Id in (select WHR_Id from tbl_DSC_eWHR_Submission_CMS2020 where WHR_Id='" + whr + "' and PrintedIp=''))";
                    //BM
                    //query = "SELECT DISTINCT WHR.Depositor_WHR_Id,WHR.Commodity_Id,('(' +CONVERT(NVARCHAR(50),FLOOR((convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,4),WHR.Total_Qty_Received)))) + ')'+ dbo.[AnkNumberToWords](FLOOR((convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,4),WHR.Total_Qty_Received))))+' Rupees') as MktValue_of_Commodity,convert(decimal(18,2),WHR.MktValue_of_Commodity) as MktValue_of_Commodityno, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name, WHR.Depositor_Name, LicenseNo,LicenseDate ,tbl_MetaData_DEPOT.DepotName, CONVERT(NVARCHAR(10), WHR.Date_of_Deposit, 103) AS Depositdate,CONVERT(NVARCHAR(10), WHR.WHR_Issue_Date, 103) AS whrDate ,CONVERT(NVARCHAR(10), WHR.SangrahadDate, 103) AS StorageRateDate,('Moisture % '+CONVERT(NVARCHAR(250),(CONVERT(decimal(18,2),(convert(decimal(18,2),WHR.AvgMoisture_Content)+convert(decimal(18,2),WHR.AvgMoisture_Content_To))/2)))+' '+WHR.Remark) as Remark ,WHR.TotalBags_Received,  convert(decimal(18,4),WHR.Total_Qty_Received) as Total_Qty_Received, tbl_MetaData_GODOWN.Godown_Name, tbl_MetaData_STORAGE_CATEGORY.Category_Name FROM tbl_storage_Depositor_WHR_Relation AS WHR INNER JOIN tbl_storage_Stacking_Details ON WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId INNER JOIN tbl_MetaData_STORAGE_CATEGORY ON WHR.Category_Id = tbl_MetaData_STORAGE_CATEGORY.Category_Id INNER JOIN tbl_MetaData_STORAGE_COMMODITY ON WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id INNER JOIN tbl_MetaData_GODOWN ON tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID INNER JOIN tbl_MetaData_DEPOT ON WHR.Depotid = tbl_MetaData_DEPOT.DepotID WHERE (WHR.Depositor_WHR_Id = '" + whr + "' and WHR.BranchID='" + BranchId + "'  )";

                    //query = "SELECT DISTINCT DSC.Depositor_WHR_Id,DSC.Commodity_Id,('(' +CONVERT(NVARCHAR(50),FLOOR((convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,4),WHR.Total_Qty_Received)))) + ')'+ dbo.[AnkNumberToWords](FLOOR((convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,4),WHR.Total_Qty_Received))))+' Rupees') as MktValue_of_Commodity,convert(decimal(18,2),WHR.MktValue_of_Commodity) as MktValue_of_Commodityno, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name, WHR.Depositor_Name, LicenseNo,LicenseDate ,tbl_MetaData_DEPOT.DepotName, CONVERT(NVARCHAR(10), WHR.Date_of_Deposit, 103) AS Depositdate,CONVERT(NVARCHAR(10), WHR.WHR_Issue_Date, 103) AS whrDate ,CONVERT(NVARCHAR(10), WHR.SangrahadDate, 103) AS StorageRateDate,('Moisture % '+CONVERT(NVARCHAR(250),(CONVERT(decimal(18,2),(convert(decimal(18,2),WHR.AvgMoisture_Content)+convert(decimal(18,2),WHR.AvgMoisture_Content_To))/2)))+' '+WHR.Remark) as Remark ,WHR.TotalBags_Received,  convert(decimal(18,4),WHR.Total_Qty_Received) as Total_Qty_Received, tbl_MetaData_GODOWN.Godown_Name, tbl_MetaData_STORAGE_CATEGORY.Category_Name FROM tbl_storage_Depositor_WHR_Relation AS WHR INNER JOIN tbl_storage_Stacking_Details ON WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId INNER JOIN tbl_MetaData_STORAGE_CATEGORY ON WHR.Category_Id = tbl_MetaData_STORAGE_CATEGORY.Category_Id INNER JOIN tbl_MetaData_STORAGE_COMMODITY ON WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id INNER JOIN tbl_MetaData_GODOWN ON tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID INNER JOIN tbl_MetaData_DEPOT ON WHR.Depotid = tbl_MetaData_DEPOT.DepotID inner join tbl_Digitally_Signed_WHR_CMS2022 as DSC on DSC.Depositor_WHR_Id=WHR.Depositor_WHR_Id WHERE (WHR.Depositor_WHR_Id = '" + whr + "' and WHR.BranchID='" + Branch + "' AND WHR.Gid IS NULL and WHR.Depositor_WHR_Id in (select S.Depositor_whr_id from View_Digitally_Signed_WHR_CMS2022 as S where S.User_Type='B') and WHR.Depositor_WHR_Id in (select WHR_Id from tbl_DSC_eWHR_Submission_CMS2022 where WHR_Id='" + whr + "' and PrintedIp=''))";
                    query = "SELECT DISTINCT DSC.Depositor_WHR_Id,DSC.Commodity_Id,('(' +CONVERT(NVARCHAR(50),FLOOR((convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,4),WHR.Total_Qty_Received)))) + ')'+ dbo.[AnkNumberToWords](FLOOR((convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,4),WHR.Total_Qty_Received))))+' Rupees') as MktValue_of_Commodity,convert(decimal(18,2),WHR.MktValue_of_Commodity) as MktValue_of_Commodityno, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name, WHR.Depositor_Name, LicenseNo,LicenseDate ,tbl_MetaData_DEPOT.DepotName, CONVERT(NVARCHAR(10), WHR.Date_of_Deposit, 103) AS Depositdate,CONVERT(NVARCHAR(10), WHR.WHR_Issue_Date, 103) AS whrDate ,CONVERT(NVARCHAR(10), WHR.SangrahadDate, 103) AS StorageRateDate,('Moisture % '+CONVERT(NVARCHAR(250),(CONVERT(decimal(18,2),(convert(decimal(18,2),WHR.AvgMoisture_Content)+convert(decimal(18,2),WHR.AvgMoisture_Content_To))/2)))+' '+WHR.Remark) as Remark ,WHR.TotalBags_Received,  convert(decimal(18,4),WHR.Total_Qty_Received) as Total_Qty_Received, tbl_MetaData_GODOWN.Godown_Name, tbl_MetaData_STORAGE_CATEGORY.Category_Name,WHR.CropYear FROM tbl_storage_Depositor_WHR_Relation AS WHR INNER JOIN tbl_storage_Stacking_Details ON WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId INNER JOIN tbl_MetaData_STORAGE_CATEGORY ON WHR.Category_Id = tbl_MetaData_STORAGE_CATEGORY.Category_Id INNER JOIN tbl_MetaData_STORAGE_COMMODITY ON WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id INNER JOIN tbl_MetaData_GODOWN ON tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID INNER JOIN tbl_MetaData_DEPOT ON WHR.Depotid = tbl_MetaData_DEPOT.DepotID inner join tbl_Digitally_Signed_WHR_CMS2022 as DSC on DSC.Depositor_WHR_Id=WHR.Depositor_WHR_Id WHERE (WHR.Depositor_WHR_Id = '" + whr + "' and WHR.BranchID='" + Branch + "' AND WHR.Gid IS NULL and WHR.Depositor_WHR_Id in (select S.Depositor_whr_id from View_Digitally_Signed_WHR_CMS2022 as S where S.User_Type='B') and WHR.Depositor_WHR_Id in (select WHR_Id from tbl_DSC_eWHR_Submission_CMS2022 where WHR_Id='" + whr + "' and PrintedIp=''))";


                }
                cmd = new SqlCommand(query, Con);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Commodity_Id"].ToString() == "25" || ds.Tables[0].Rows[0]["Commodity_Id"].ToString() == "29" || ds.Tables[0].Rows[0]["Commodity_Id"].ToString() == "96" || ds.Tables[0].Rows[0]["Commodity_Id"].ToString() == "97")
                    {
                        //query = "SELECT DISTINCT WHR.Depositor_WHR_Id,WHR.Commodity_Id,('(' +CONVERT(NVARCHAR(50),(convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,2),WHR.TotalBags_Received))) + ')'+ dbo.[AnkNumberToWords](convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,2),WHR.TotalBags_Received))) as MktValue_of_Commodity,convert(decimal(18,2),WHR.MktValue_of_Commodity) as MktValue_of_Commodityno, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name, WHR.Depositor_Name, LicenseNo,LicenseDate ,tbl_MetaData_DEPOT.DepotName, CONVERT(NVARCHAR(10), WHR.Date_of_Deposit, 103) AS Depositdate,CONVERT(NVARCHAR(10), WHR.WHR_Issue_Date, 103) AS whrDate ,CONVERT(NVARCHAR(10), WHR.SangrahadDate, 103) AS StorageRateDate,('Moisture%'+CONVERT(NVARCHAR(250),((convert(decimal(18,2),WHR.AvgMoisture_Content)+convert(decimal(18,2),WHR.AvgMoisture_Content_To))/2))+' '+WHR.Remark) as Remark ,WHR.TotalBags_Received,  convert(decimal(18,4),WHR.Total_Qty_Received) as Total_Qty_Received, tbl_MetaData_GODOWN.Godown_Name, tbl_MetaData_STORAGE_CATEGORY.Category_Name FROM   tbl_storage_Depositor_WHR_Relation AS WHR INNER JOIN tbl_storage_Stacking_Details ON WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId INNER JOIN tbl_MetaData_STORAGE_CATEGORY ON WHR.Category_Id = tbl_MetaData_STORAGE_CATEGORY.Category_Id INNER JOIN tbl_MetaData_STORAGE_COMMODITY ON WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id INNER JOIN tbl_MetaData_GODOWN ON tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID INNER JOIN tbl_MetaData_DEPOT ON WHR.Depotid = tbl_MetaData_DEPOT.DepotID WHERE (WHR.Depositor_WHR_Id = '" + whr + "' and WHR.BranchID='" + Branch + "'  )";
                        query = "SELECT DISTINCT WHR.Depositor_WHR_Id,WHR.Commodity_Id,('(' +CONVERT(NVARCHAR(50),(convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,2),WHR.TotalBags_Received))) + ')'+ dbo.[AnkNumberToWords](convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,2),WHR.TotalBags_Received))) as MktValue_of_Commodity,convert(decimal(18,2),WHR.MktValue_of_Commodity) as MktValue_of_Commodityno, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name, WHR.Depositor_Name, LicenseNo,LicenseDate ,tbl_MetaData_DEPOT.DepotName, CONVERT(NVARCHAR(10), WHR.Date_of_Deposit, 103) AS Depositdate,CONVERT(NVARCHAR(10), WHR.WHR_Issue_Date, 103) AS whrDate ,CONVERT(NVARCHAR(10), WHR.SangrahadDate, 103) AS StorageRateDate,('Moisture%'+CONVERT(NVARCHAR(250),((convert(decimal(18,2),WHR.AvgMoisture_Content)+convert(decimal(18,2),WHR.AvgMoisture_Content_To))/2))+' '+WHR.Remark) as Remark ,WHR.TotalBags_Received,  convert(decimal(18,4),WHR.Total_Qty_Received) as Total_Qty_Received, tbl_MetaData_GODOWN.Godown_Name, tbl_MetaData_STORAGE_CATEGORY.Category_Name,WHR.CropYear FROM   tbl_storage_Depositor_WHR_Relation AS WHR INNER JOIN tbl_storage_Stacking_Details ON WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId INNER JOIN tbl_MetaData_STORAGE_CATEGORY ON WHR.Category_Id = tbl_MetaData_STORAGE_CATEGORY.Category_Id INNER JOIN tbl_MetaData_STORAGE_COMMODITY ON WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id INNER JOIN tbl_MetaData_GODOWN ON tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID INNER JOIN tbl_MetaData_DEPOT ON WHR.Depotid = tbl_MetaData_DEPOT.DepotID WHERE (WHR.Depositor_WHR_Id = '" + whr + "' and WHR.BranchID='" + Branch + "'  )";

                        cmd = new SqlCommand(query, Con);
                        da = new SqlDataAdapter(cmd);
                        ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Panel1.Visible = true;
                            //  btnbmpassword.Enabled = true;
                            //  lblwhr.Text = ds.Tables[0].Rows[0]["Depositor_WHR_Id"].ToString();
                            lblwhrno.Text = ds.Tables[0].Rows[0]["Depositor_WHR_Id"].ToString();
                            lblwhrNooff.Text = ds.Tables[0].Rows[0]["Depositor_WHR_Id"].ToString();

                            //branch Name
                            string qry1 = "select  Hired_Type,Godown_ID,BranchId from [tbl_MetaData_GODOWN] where Godown_ID= (select max(Godown_ID) from  [tbl_storage_Stacking_Details] where WHRId='" + whr + "')";
                            SqlCommand cmd1 = new SqlCommand(qry1, Con);
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            DataSet ds1 = new DataSet();
                            da1.Fill(ds1);
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                string storagetype = ds1.Tables[0].Rows[0][0].ToString();
                                if (storagetype == "OtherAgency")
                                {
                                    lblbrachname1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                                    lblbranchoff.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                                }


                            }
                            // lblbrachname1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                            //lblbranchoff.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();


                            // lbldatetime.Text = DateTime.Now.Date.ToString();
                            //lbldepositorname1.Text = ds.Tables[0].Rows[0]["Depositor_Name"].ToString();

                            lblCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                            lbldatefrom.Text = ds.Tables[0].Rows[0]["StorageRateDate"].ToString();
                            lblstordateoff.Text = ds.Tables[0].Rows[0]["StorageRateDate"].ToString();

                            lbldepositor2.Text = ds.Tables[0].Rows[0]["Depositor_Name"].ToString();
                            lbldepositopoffice.Text = ds.Tables[0].Rows[0]["Depositor_Name"].ToString();

                            lblgodownname.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                            lblgodownoffice.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();

                            lblwhrdateoffice.Text = ds.Tables[0].Rows[0]["whrDate"].ToString();
                            lbldate3.Text = ds.Tables[0].Rows[0]["whrDate"].ToString();

                            string ddd = "https://chart.googleapis.com/chart?chs=100x100&amp;cht=qr&amp;chl=WHR No:'" + lblwhrno.Text + "'Commodity: '" + ds.Tables[0].Rows[0]["Commodity_Name"].ToString() + "' WHR Date: '" + lbldate3.Text + "'Depositor:'" + lbldepositor2.Text + "'Godown:'" + lblgodownname.Text + "'WHR Qty:'" + ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString() + "' ";
                            Image3.ImageUrl = "https://chart.googleapis.com/chart?chs=100x100&cht=qr&chl=WHR No:'" + lblwhrno.Text + "'Commodity: '" + ds.Tables[0].Rows[0]["Commodity_Name"].ToString() + "' WHR Date: '" + lbldate3.Text + "'Depositor:'" + lbldepositor2.Text + "'Godown:'" + lblgodownname.Text + "'WHR Qty:'" + ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString() + "' ";
                            Image4.ImageUrl = "https://chart.googleapis.com/chart?chs=100x100&cht=qr&chl=WHR No:'" + lblwhrno.Text + "'Commodity: '" + ds.Tables[0].Rows[0]["Commodity_Name"].ToString() + "' WHR Date: '" + lbldate3.Text + "'Depositor:'" + lbldepositor2.Text + "'Godown:'" + lblgodownname.Text + "'WHR Qty:'" + ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString() + "' ";
                            ListView1.DataSource = ds.Tables[0];

                            ListView1.DataBind();

                            lvofficem.DataSource = ds.Tables[0];
                            lvofficem.DataBind();
                            // WHR.Style.Add("background-image", "../../images/whrback.png");


                        }

                    }
                    else
                    {
                        Panel1.Visible = true;
                        //  btnbmpassword.Enabled = true;
                        //  lblwhr.Text = ds.Tables[0].Rows[0]["Depositor_WHR_Id"].ToString();
                        lblwhrno.Text = ds.Tables[0].Rows[0]["Depositor_WHR_Id"].ToString();
                        lblwhrNooff.Text = ds.Tables[0].Rows[0]["Depositor_WHR_Id"].ToString();

                        //branch Name
                        string qry1 = "select  Hired_Type,Godown_ID,BranchId from [tbl_MetaData_GODOWN] where Godown_ID= (select max(Godown_ID) from  [tbl_storage_Stacking_Details] where WHRId='" + whr + "')";
                        SqlCommand cmd1 = new SqlCommand(qry1, Con);
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            string storagetype = ds1.Tables[0].Rows[0][0].ToString();
                            if (storagetype == "OtherAgency")
                            {
                                lblbrachname1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                                lblbranchoff.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                            }


                        }
                        // lblbrachname1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                        //lblbranchoff.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();


                        // lbldatetime.Text = DateTime.Now.Date.ToString();
                        //lbldepositorname1.Text = ds.Tables[0].Rows[0]["Depositor_Name"].ToString();

                        lblCropYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                        lbldatefrom.Text = ds.Tables[0].Rows[0]["StorageRateDate"].ToString();
                        lblstordateoff.Text = ds.Tables[0].Rows[0]["StorageRateDate"].ToString();

                        lbldepositor2.Text = ds.Tables[0].Rows[0]["Depositor_Name"].ToString();
                        lbldepositopoffice.Text = ds.Tables[0].Rows[0]["Depositor_Name"].ToString();

                        lblgodownname.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                        lblgodownoffice.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();

                        lblwhrdateoffice.Text = ds.Tables[0].Rows[0]["whrDate"].ToString();
                        lbldate3.Text = ds.Tables[0].Rows[0]["whrDate"].ToString();

                        string ddd = "https://chart.googleapis.com/chart?chs=100x100&amp;cht=qr&amp;chl=WHR No:'" + lblwhrno.Text + "'Commodity: '" + ds.Tables[0].Rows[0]["Commodity_Name"].ToString() + "' WHR Date: '" + lbldate3.Text + "'Depositor:'" + lbldepositor2.Text + "'Godown:'" + lblgodownname.Text + "'WHR Qty:'" + ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString() + "' ";
                        Image3.ImageUrl = "https://chart.googleapis.com/chart?chs=100x100&cht=qr&chl=WHR No:'" + lblwhrno.Text + "'Commodity: '" + ds.Tables[0].Rows[0]["Commodity_Name"].ToString() + "' WHR Date: '" + lbldate3.Text + "'Depositor:'" + lbldepositor2.Text + "'Godown:'" + lblgodownname.Text + "'WHR Qty:'" + ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString() + "' ";
                        Image4.ImageUrl = "https://chart.googleapis.com/chart?chs=100x100&cht=qr&chl=WHR No:'" + lblwhrno.Text + "'Commodity: '" + ds.Tables[0].Rows[0]["Commodity_Name"].ToString() + "' WHR Date: '" + lbldate3.Text + "'Depositor:'" + lbldepositor2.Text + "'Godown:'" + lblgodownname.Text + "'WHR Qty:'" + ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString() + "' ";
                        ListView1.DataSource = ds.Tables[0];

                        ListView1.DataBind();

                        lvofficem.DataSource = ds.Tables[0];
                        lvofficem.DataBind();
                        // WHR.Style.Add("background-image", "../../images/whrback.png");

                    }

                }

                else
                {
                    Panel1.Visible = false;

                    btnprint.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No record found...')", true);
                }
                string query2 = "SELECT [Depositor_Type] from [tbl_MetaData_DEPOSITOR] WHERE ([Depositor_Name] = '" + lbldepositor2.Text + "')";
                SqlCommand cmd2 = new SqlCommand(query2, Con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    string depositorType = ds2.Tables[0].Rows[0]["Depositor_Type"].ToString();
                    //if (depositorType == "Institution")
                    //{
                    //    lblinstType.Text = "अपरक्राम्य";
                    //    instypeEng.Text = "Not Negotiable";
                    //    lblnegohoff.Text = "अपरक्राम्य";
                    //    lblnegooff.Text = "Not Negotiable";

                    //}
                    //else
                    //{
                    //    lblinstType.Text = "परक्राम्य";
                    //    instypeEng.Text = "Negotiable";
                    //    lblnegohoff.Text = "परक्राम्य";
                    //    lblnegooff.Text = "Negotiable";
                    //}
                    lblinstType.Text = "अपरक्राम्य";
                    instypeEng.Text = "Not Negotiable";
                    lblnegohoff.Text = "अपरक्राम्य";
                    lblnegooff.Text = "Not Negotiable";
                }
            }
            catch (Exception ex)
            {


            }
        }
    }
    private static string NumbersToWords(int inputNumber)
    {
        int inputNo = inputNumber;

        if (inputNo == 0)
            return "Zero";

        int[] numbers = new int[4];
        int first = 0;
        int u, h, t;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (inputNo < 0)
        {
            sb.Append("Minus ");
            inputNo = -inputNo;
        }

        string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
        string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
        string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
        string[] words3 = { "Thousand ", "Lakh ", "Crore " };

        numbers[0] = inputNo % 1000; // units
        numbers[1] = inputNo / 1000;
        numbers[2] = inputNo / 100000;
        numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
        numbers[3] = inputNo / 10000000; // crores
        numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

        for (int i = 3; i > 0; i--)
        {
            if (numbers[i] != 0)
            {
                first = i;
                break;
            }
        }
        for (int i = first; i >= 0; i--)
        {
            if (numbers[i] == 0) continue;
            u = numbers[i] % 10; // ones
            t = numbers[i] / 10;
            h = numbers[i] / 100; // hundreds
            t = t - 10 * h; // tens
            if (h > 0) sb.Append(words0[h] + "Hundred ");
            if (u > 0 || t > 0)
            {
                if (h > 0 || i == 0) sb.Append("and ");
                if (t == 0)
                    sb.Append(words0[u]);
                else if (t == 1)
                    sb.Append(words1[u]);
                else
                    sb.Append(words2[t - 2] + words0[u]);
            }
            if (i != 0) sb.Append(words3[i - 1]);
        }
        return sb.ToString().TrimEnd();

    }
    private void qtyissuedtl()
    {
        if (ddlDistrict.SelectedValue.ToString() != null)
        {
            District = ddlDistrict.SelectedValue.ToString();
            Depot = ddlDepotList.SelectedValue.ToString();
            //string query = "SELECT mg.Godown_Name,ms.Stack_Name,[No_Of_Bags],[Bags_Weight],CONVERT(VARCHAR(10),ssd.[CreatedDate],103) as ID from [tbl_Delivery_Stacking_Details_GatePass] as ssd inner join tbl_MetaData_GODOWN as mg on ssd.Godown_ID=mg.Godown_ID inner join tbl_MetaData_STACK as ms on ssd.Stack_ID=ms.Stack_ID  where Depositor_WHR_Id='" + DDLwhr.SelectedItem.Text + "'";
            string whr = "";
            if (DDLwhr.SelectedIndex != 0)
            {
                whr = DDLwhr.SelectedItem.Text;
            }
            else
            {
                whr = TextBox1.Text;
            }
            // string query = "WITH cte(Godown_Name, Stack_Name, No_Of_Bags, Bags_Weight,[Bags_Weight_iswsue],loss,gain ,CreatedDate, rn) AS (SELECT mg.Godown_Name,ms.Stack_Name,[No_Of_Bags],[Bags_Weight],(WHR.Total_Qty_Received-loss+gain-[Bags_Weight]) as [Bags_Weight_iswsue],Loss,Gain,CONVERT(VARCHAR(10),sge.Issue_Date,103) as ID,Row_number()OVER(ORDER BY sge.Issue_Date) rn from [tbl_Delivery_Stacking_Details_GatePass] as ssd inner join tbl_MetaData_GODOWN as mg on ssd.Godown_ID=mg.Godown_ID inner join tbl_MetaData_STACK as ms on ssd.Stack_ID=ms.Stack_ID inner join tbl_storage_Depositor_WHR_Relation AS WHR on ssd.Depositor_WHR_Id=WHR.Depositor_WHR_Id inner join dbo.tbl_Storage_GatePass_Enrty as sge on ssd.GatePass_No=sge.GatePass_No where WHR.Depositor_WHR_Id='" + whr + "'),cte1 AS (SELECT TOP 1 Godown_Name,Stack_Name,No_Of_Bags,Bags_Weight, loss,gain,rn,CreatedDate,cast([Bags_Weight_iswsue] as numeric(18,2)) AS available_quantity FROM   cte ORDER  BY rn UNION ALL SELECT a.Godown_Name,a.Stack_Name,a.No_Of_Bags,a.[Bags_Weight],a.loss,a.gain,a.rn,a.CreatedDate, cast(b.available_quantity - a.[Bags_Weight]-a.loss+a.gain as numeric(18,2)) AS available_quantity FROM   cte a INNER JOIN cte1 b ON a.rn - 1 = b.rn)SELECT CreatedDate,Godown_Name,Stack_Name,No_Of_Bags,Bags_Weight,available_quantity FROM   cte1 ";
            // string query = "SELECT mg.Godown_Name,ms.Stack_Name,[No_Of_Bags],[Bags_Weight],WHR.Total_Qty_Received,WHR.TotalBags_Received,(WHR.Total_Qty_Received-loss+gain-[Bags_Weight]) as [Bags_Weight_iswsue],Loss,Gain,CONVERT(VARCHAR(10),sge.Issue_Date,103) as ID,Row_number()OVER(ORDER BY sge.Issue_Date) rn from [tbl_Delivery_Stacking_Details_GatePass] as ssd inner join tbl_MetaData_GODOWN as mg on ssd.Godown_ID=mg.Godown_ID inner join tbl_MetaData_STACK as ms on ssd.Stack_ID=ms.Stack_ID inner join tbl_storage_Depositor_WHR_Relation AS WHR on ssd.Depositor_WHR_Id=WHR.Depositor_WHR_Id inner join dbo.tbl_Storage_GatePass_Enrty as sge on ssd.GatePass_No=sge.GatePass_No where WHR.Depositor_WHR_Id='" + whr + "' order by sge.Issue_Date asc";
            string query = "SELECT mg.Godown_Name,ms.Stack_Name,isnull([No_Of_Bags],0)[No_Of_Bags], isnull([Bags_Weight],0)Bags_Weight,isnull(WHR.Total_Qty_Received,0) Total_Qty_Received,isnull(WHR.TotalBags_Received,0)TotalBags_Received,(WHR.Total_Qty_Received-loss+gain-[Bags_Weight]) as [Bags_Weight_iswsue],Loss,Gain,CONVERT(VARCHAR(10),sge.Issue_Date,103) as ID,Row_number()OVER(ORDER BY sge.Issue_Date) rn from [tbl_Delivery_Stacking_Details_GatePass] as ssd inner join tbl_MetaData_GODOWN as mg on ssd.Godown_ID=mg.Godown_ID inner join tbl_MetaData_STACK as ms on ssd.Stack_ID=ms.Stack_ID inner join tbl_storage_Depositor_WHR_Relation AS WHR on ssd.Depositor_WHR_Id=WHR.Depositor_WHR_Id inner join dbo.tbl_Storage_GatePass_Enrty as sge on ssd.GatePass_No=sge.GatePass_No where WHR.Depositor_WHR_Id='" + whr + "' order by sge.Issue_Date asc";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dss = new DataTable();
            dss.Columns.Add("Date", typeof(string));
            dss.Columns.Add("DelQty", typeof(string));
            dss.Columns.Add("AvlQty", typeof(string));
            dss.Columns.Add("DelBags", typeof(string));
            dss.Columns.Add("AvlBags", typeof(string));
            if (ds.Tables[0].Rows.Count > 0)
            {
                decimal avilableQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString());
                int avilableBags = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalBags_Received"].ToString()); ;
                DDLwhr.DataSource = ds.Tables[0];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    avilableQty = avilableQty - Convert.ToDecimal(ds.Tables[0].Rows[i]["Bags_Weight"].ToString()) - Convert.ToDecimal(ds.Tables[0].Rows[i]["Loss"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[i]["Gain"].ToString());
                    avilableBags = avilableBags - Convert.ToInt32(ds.Tables[0].Rows[i]["No_Of_Bags"].ToString());


                    dss.Rows.Add(ds.Tables[0].Rows[i]["ID"].ToString(), ds.Tables[0].Rows[i]["Bags_Weight"].ToString(), avilableQty.ToString(), ds.Tables[0].Rows[i]["No_Of_Bags"].ToString(), avilableBags);


                }


                //foreach (ListViewDataItem di in ListView2.Items)
                //{


                //}
                ListView2.DataSource = dss;

                ListView2.DataBind();

                lvoffice.DataSource = dss;
                lvoffice.DataBind();

            }
            else
            {

            }
        }
    }

    //27/02/2014......
    // private void GetAvilableqty()
    //{
    //    try
    //    {
    //            if (Session["Depot_DistID"] != null)
    //    {
    //        string whr = "";
    //        if (DDLwhr.SelectedIndex != 0)
    //        {
    //            whr = DDLwhr.SelectedItem.Text;
    //        }
    //        else
    //        {
    //            whr = TextBox1.Text;
    //        }


    //        District = Session["Depot_DistID"].ToString();
    //        Depot = Session["Depot_DepotID"].ToString();
    //        string query = "SELECT mg.Godown_Name,ms.Stack_Name,[No_Of_Bags],[Bags_Weight],WHR.Total_Qty_Received as [Bags_Weight_iswsue],CONVERT(VARCHAR(10),ssd.[CreatedDate],103) as ID from [tbl_Delivery_Stacking_Details_GatePass] as ssd inner join tbl_MetaData_GODOWN as mg on ssd.Godown_ID=mg.Godown_ID inner join tbl_MetaData_STACK as ms on ssd.Stack_ID=ms.Stack_ID inner join tbl_storage_Depositor_WHR_Relation AS WHR on ssd.Depositor_WHR_Id=WHR.Depositor_WHR_Id where WHR.Depositor_WHR_Id='232700201914163'";
    //        SqlCommand cmd = new SqlCommand(query, Con);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {


    //            DDLwhr.DataSource = ds.Tables[0];
    //            ListView2.DataSource = ds.Tables[0];

    //            ListView2.DataBind();

    //        }
    //        else
    //        {

    //        }
    //    }

    //    }
    //    catch (Exception ex)
    //    {


    //    }


    //}
    private void GetDepotBelongs(string depotId)
    {
        try
        {
            string whr = "";
            string BranchType = "";

            if (DDLwhr.SelectedIndex != 0)
            {
                whr = DDLwhr.SelectedItem.Text;
            }
            else
            {
                whr = TextBox1.Text;
            }

            //string str = "SELECT [BranchTypeID] FROM [MetaDataBranchWithIssueCenter] where BranchID='" + Session["BranchID"].ToString() + "'";
            string str = "SELECT [BranchTypeID] FROM [MetaDataBranchWithIssueCenter] where BranchID='" + Branch + "'";

            SqlDataAdapter da7 = new SqlDataAdapter(str, Con);
            DataSet ds7 = new DataSet();
            da7.Fill(ds7);
            if (ds7.Tables[0].Rows.Count > 0)
            {
                BranchType = ds7.Tables[0].Rows[0][0].ToString();
            }

            if (BranchType == "I" || BranchType == "O" || BranchType == "0" || BranchType == "1")
            {

                //string qry1 = "select  Hired_Type,Godown_ID,BranchId from [tbl_MetaData_GODOWN] where Godown_ID= (select max(Godown_ID) from  [tbl_storage_Stacking_Details] where WHRId='" + whr + "')";
                string qry1 = "select  Hired_Type,Godown_ID,BranchId,LicNum,CONVERT(varchar(10),LicDate,103) as LicDate from [tbl_MetaData_GODOWN_2018] where Godown_ID= (select max(Godown_ID) from  [tbl_storage_Stacking_Details] where WHRId='" + whr + "')";

                SqlCommand cmd1 = new SqlCommand(qry1, Con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    string storagetype = ds1.Tables[0].Rows[0][0].ToString().Trim();
                    string LicNos = ds1.Tables[0].Rows[0][3].ToString().Trim();
                    string LicDates = ds1.Tables[0].Rows[0][4].ToString().Trim();
                    //if (storagetype != "OtherAgency" && storagetype != "SteelSilo" && storagetype != "WDRA" && storagetype != "Markfed" && storagetype != "Oil-Fed" && storagetype != "PVT.PEG" && storagetype != "CWC")
                    //if (storagetype != "OtherAgency" && storagetype != "Markfed" && storagetype != "Oil-Fed" && storagetype != "CWC")
                    if (storagetype != "OtherAgency" && storagetype != "Markfed" && storagetype != "Oil-Fed")
                    {
                        string qry = "Select *  from tbl_MetaData_DEPOT where BranchID='" + ds1.Tables[0].Rows[0][2].ToString() + "'";
                        SqlCommand cmd = new SqlCommand(qry, Con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblcorpnamehnd.Visible = true;
                            lblcorpnamehndof.Visible = true;
                            lbldepositorname1.Text = "MPWLC";
                            Label9.Text = "MPWLC";
                            lblcorpnamehnd.Text = "(म.प्र. वेयरहाउसिंग एंड लाजिस्टिक्स कार्पोरेशन)";
                            lblcorpnamehndof.Text = "(म.प्र. वेयरहाउसिंग एंड लाजिस्टिक्स कार्पोरेशन)";
                            Image2.Visible = true;
                            Image1.Visible = true;
                            mpwlcAothO.Visible = false;
                            mpwlcothOf.Visible = false;

                            lblbrachname1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                            lblbranchoff.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();

                            lblcorpname.Text = "(Madhya Pradesh Warehousing and Logistics Corporation)";
                            lblcorpnameof.Text = "(Madhya Pradesh Warehousing and Logistics Corporation)";


                            //ddlDepoBeloggsTo.SelectedItem.Text = ds.Tables[0].Rows[0]["DepoBelongs"].ToString();
                            // txtTehsilName.Text = ds.Tables[0].Rows[0]["TehsilName"].ToString();
                            lbladdr.Text = ds.Tables[0].Rows[0]["DepotAddress"].ToString();
                            lbladdoffice.Text = ds.Tables[0].Rows[0]["DepotAddress"].ToString();
                            //  txtLocationPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                            // txtLocationFaxNo.Text = ds.Tables[0].Rows[0]["FaxNo"].ToString();
                            // txtLocationEMailAddress.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                            lblgodampal.Text = ds.Tables[0].Rows[0]["NodalOfficeName"].ToString();
                            lblgodampaloffice.Text = ds.Tables[0].Rows[0]["NodalOfficeName"].ToString();
                            // lbllicensedate.Text = ds.Tables[0].Rows[0]["LicenceDate"].ToString();
                            //lbllicensno.Text = ds.Tables[0].Rows[0]["LincenseNo"].ToString();


                            //string licNo = ds.Tables[0].Rows[0]["LincenseNo"].ToString();
                            string licNo = LicNos;
                            if (licNo == "")
                            {
                                lbllicensno.Text = "__________________";
                                lbllicofficeno.Text = "__________________";
                            }
                            else
                            {
                                //lbllicensno.Text = ds.Tables[0].Rows[0]["LincenseNo"].ToString();
                                //lbllicofficeno.Text = ds.Tables[0].Rows[0]["LincenseNo"].ToString();
                                lbllicensno.Text = LicNos;
                                lbllicofficeno.Text = LicNos;
                            }
                            //string LicDate = ds.Tables[0].Rows[0]["LicenceDate"].ToString();
                            string LicDate = LicDates;
                            if (LicDate == "")
                            {
                                lbllicensedate.Text = "_________________";
                                lbllicsnceoffedate.Text = "_________________";
                            }
                            else
                            {
                                //lbllicensedate.Text = ds.Tables[0].Rows[0]["LicenceDate"].ToString();
                                //lbllicsnceoffedate.Text = ds.Tables[0].Rows[0]["LicenceDate"].ToString();
                                lbllicensedate.Text = LicDates;
                                lbllicsnceoffedate.Text = LicDates;
                            }
                            //txtNodalOfficerAddress.Text = ds.Tables[0].Rows[0]["NodalOfficeraddress"].ToString();
                            //txtNodalOfficerPhoneNo.Text = ds.Tables[0].Rows[0]["NodalOfficerphone"].ToString();
                            //txtNodalOfficerMobileNo.Text = ds.Tables[0].Rows[0]["NodalOfficerMobile"].ToString();
                            //txtNodalOfficerFaxNo.Text = ds.Tables[0].Rows[0]["NodalOfficerFax"].ToString();
                            //txtNodalOfficerEmailAddress.Text = ds.Tables[0].Rows[0]["NodalOfficerEmail"].ToString();

                        }
                    }
                    else if (storagetype == "SteelSilo")
                    {
                        //select Steel Silo corp data
                        string qry2 = " SELECT [SiloName],[SiloAddress],[MobileNo],[EmailId],[LicenceNum],[LicenceDate],[AuthSignatory],[WlcCoSign] FROM [Intergrated_MP_STORAGE].[dbo].[Tbl_MetaData_Silo] where SiloID='" + ds1.Tables[0].Rows[0][1].ToString() + "'";
                        SqlCommand cmd2 = new SqlCommand(qry2, Con);
                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2);
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            lbladdr.Text = ds2.Tables[0].Rows[0]["SiloAddress"].ToString();
                            lbladdoffice.Text = ds2.Tables[0].Rows[0]["SiloAddress"].ToString();

                            lbldepositorname1.Text = ds2.Tables[0].Rows[0]["SiloName"].ToString();
                            Label9.Text = ds2.Tables[0].Rows[0]["SiloName"].ToString();
                            lblgodampal.Text = ds2.Tables[0].Rows[0]["AuthSignatory"].ToString();
                            lblgodampaloffice.Text = ds2.Tables[0].Rows[0]["AuthSignatory"].ToString();

                            lblcorpname.Text = ds2.Tables[0].Rows[0]["SiloName"].ToString();
                            lblcorpnameof.Text = ds2.Tables[0].Rows[0]["SiloName"].ToString();
                            Image2.Visible = false;
                            Image1.Visible = false;
                            lblcorpnamehnd.Visible = false;
                            lblcorpnamehndof.Visible = false;
                            mpwlcAothO.Visible = true;
                            mpwlcothOf.Visible = true;
                            mpwlcAothO.Text = "MPWLC द्वारा अधिकृत व्यक्ति";
                            mpwlcothOf.Text = "MPWLC द्वारा अधिकृत व्यक्ति";
                            string licNo = ds2.Tables[0].Rows[0]["LicenceNum"].ToString();
                            if (licNo == "")
                            {
                                lbllicensno.Text = "__________________";
                                lbllicofficeno.Text = "__________________";
                            }
                            else
                            {
                                lbllicensno.Text = ds2.Tables[0].Rows[0]["LicenceNum"].ToString();
                                lbllicofficeno.Text = ds2.Tables[0].Rows[0]["LicenceNum"].ToString();
                            }
                            string LicDate = ds2.Tables[0].Rows[0]["LicenceDate"].ToString();
                            if (LicDate == "")
                            {
                                lbllicensedate.Text = "_________________";
                                lbllicsnceoffedate.Text = "_________________";
                            }
                            else
                            {
                                lbllicensedate.Text = ds2.Tables[0].Rows[0]["LicenceDate"].ToString();
                                lbllicsnceoffedate.Text = ds2.Tables[0].Rows[0]["LicenceDate"].ToString();
                            }

                        }
                    }
                    else
                    {
                        //Private Data

                        //string qry2 = "select  [Godown_ID],[Godown_APN],Godown_Name,[Godown_Email],[Godown_Mobile],[Godown_Address],LicNum,convert (nvarchar(20),LicDate,103) as LicDate,BranchId from [tbl_MetaData_GODOWN] where Godown_ID= (select max(Godown_ID) from  [tbl_storage_Stacking_Details] where WHRId='" + whr + "')";
                        string qry2 = "select  GD1.[Godown_ID],GD1.[Godown_APN],GD2.Godown_Name,GD1.[Godown_Email],GD1.[Godown_Mobile],GD1.[Godown_Address],GD2.LicNum,convert (varchar(20),GD2.LicDate,103) as LicDate,GD2.BranchId from [tbl_MetaData_GODOWN] as GD1 inner join tbl_MetaData_GODOWN_2018 as GD2 on GD1.Godown_ID=GD2.Godown_ID where GD1.Godown_ID= (select max(Godown_ID) from  [tbl_storage_Stacking_Details] where WHRId='" + whr + "')";

                        SqlCommand cmd2 = new SqlCommand(qry2, Con);
                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2);
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            lbladdr.Text = ds2.Tables[0].Rows[0]["Godown_Address"].ToString();
                            lbladdoffice.Text = ds2.Tables[0].Rows[0]["Godown_Address"].ToString();

                            lbldepositorname1.Text = ds2.Tables[0].Rows[0]["Godown_Name"].ToString();
                            Label9.Text = ds2.Tables[0].Rows[0]["Godown_Name"].ToString();
                            lblgodampal.Text = ds2.Tables[0].Rows[0]["Godown_APN"].ToString();
                            lblgodampaloffice.Text = ds2.Tables[0].Rows[0]["Godown_APN"].ToString();

                            lblcorpname.Text = ds2.Tables[0].Rows[0]["Godown_Name"].ToString();
                            lblcorpnameof.Text = ds2.Tables[0].Rows[0]["Godown_Name"].ToString();
                            Image2.Visible = false;
                            Image1.Visible = false;
                            lblcorpnamehnd.Visible = false;
                            lblcorpnamehndof.Visible = false;
                            mpwlcAothO.Visible = true;
                            mpwlcothOf.Visible = true;
                            mpwlcAothO.Text = "MPWLC द्वारा अधिकृत व्यक्ति";
                            mpwlcothOf.Text = "MPWLC द्वारा अधिकृत व्यक्ति";
                            string licNo = ds2.Tables[0].Rows[0]["LicDate"].ToString();
                            if (licNo == "")
                            {
                                lbllicensno.Text = "__________________";
                                lbllicofficeno.Text = "__________________";
                            }
                            else
                            {
                                lbllicensno.Text = ds2.Tables[0].Rows[0]["LicNum"].ToString();
                                lbllicofficeno.Text = ds2.Tables[0].Rows[0]["LicNum"].ToString();
                            }
                            string LicDate = ds2.Tables[0].Rows[0]["LicDate"].ToString();
                            if (LicDate == "")
                            {
                                lbllicensedate.Text = "_________________";
                                lbllicsnceoffedate.Text = "_________________";
                            }
                            else
                            {
                                lbllicensedate.Text = ds2.Tables[0].Rows[0]["LicDate"].ToString();
                                lbllicsnceoffedate.Text = ds2.Tables[0].Rows[0]["LicDate"].ToString();
                            }

                        }

                    }
                }
            }
            else if (BranchType == "4")
            {
                string qry1 = "select  Hired_Type,Godown_ID,BranchId,LicNum,CONVERT(varchar(10),LicDate,103) as LicDate from [tbl_MetaData_GODOWN_2018] where Godown_ID= (select max(Godown_ID) from  [tbl_storage_Stacking_Details] where WHRId='" + whr + "')";
                string LicNos = "";
                string LicDates = "";
                SqlCommand cmd1 = new SqlCommand(qry1, Con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    //string storagetype = ds1.Tables[0].Rows[0][0].ToString().Trim();
                    LicNos = ds1.Tables[0].Rows[0][3].ToString().Trim();
                    LicDates = ds1.Tables[0].Rows[0][4].ToString().Trim();
                }

                string qry = "Select *  from tbl_MetaData_DEPOT where BranchID='" + Session["BranchID"].ToString() + "'";
                SqlCommand cmd = new SqlCommand(qry, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblcorpnamehnd.Visible = true;
                    lblcorpnamehndof.Visible = true;
                    lbldepositorname1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    Label9.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    lblcorpnamehnd.Text = "";
                    lblcorpnamehndof.Text = "";
                    Image1.Visible = true;
                    Image2.Visible = true;
                    Image1.Attributes.Add("src", "../../images/mfd.PNG");
                    Image2.Attributes.Add("src", "../../images/mfd.PNG");
                    //   Image2.Style.Add("ImageUrl", "url('../../images/mfd.PNG')");
                    //Image1.Style.Add("ImageUrl", "url('../../images/mfd.PNG')");
                    mpwlcAothO.Visible = false;
                    mpwlcothOf.Visible = false;

                    lblbrachname1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    lblbranchoff.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();

                    lblcorpname.Text = "M.P. State Cooperative Marketing Federation Ltd.";
                    lblcorpnameof.Text = "M.P. State Cooperative Marketing Federation Ltd.";
                    //  Image2.Visible = false;
                    // Image1.Visible = false;
                    lblcorpnamehnd.Visible = false;
                    lblcorpnamehndof.Visible = false;
                    mpwlcAothO.Visible = false;
                    mpwlcothOf.Visible = false;
                    // mpwlcAothO.Text = "MPWLC द्वारा अधिकृत व्यक्ति";
                    //  mpwlcothOf.Text = "MPWLC द्वारा अधिकृत व्यक्ति";

                    //ddlDepoBeloggsTo.SelectedItem.Text = ds.Tables[0].Rows[0]["DepoBelongs"].ToString();
                    // txtTehsilName.Text = ds.Tables[0].Rows[0]["TehsilName"].ToString();
                    lbladdr.Text = ds.Tables[0].Rows[0]["DepotAddress"].ToString();
                    lbladdoffice.Text = ds.Tables[0].Rows[0]["DepotAddress"].ToString();
                    //  txtLocationPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                    // txtLocationFaxNo.Text = ds.Tables[0].Rows[0]["FaxNo"].ToString();
                    // txtLocationEMailAddress.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                    lblgodampal.Text = ds.Tables[0].Rows[0]["NodalOfficeName"].ToString();
                    lblgodampaloffice.Text = ds.Tables[0].Rows[0]["NodalOfficeName"].ToString();
                    // lbllicensedate.Text = ds.Tables[0].Rows[0]["LicenceDate"].ToString();
                    //lbllicensno.Text = ds.Tables[0].Rows[0]["LincenseNo"].ToString();


                    string licNo = LicNos;
                    if (licNo == "")
                    {
                        lbllicensno.Text = "__________________";
                        lbllicofficeno.Text = "__________________";
                    }
                    else
                    {
                        //lbllicensno.Text = ds.Tables[0].Rows[0]["LincenseNo"].ToString();
                        //lbllicofficeno.Text = ds.Tables[0].Rows[0]["LincenseNo"].ToString();
                        lbllicensno.Text = LicNos;
                        lbllicofficeno.Text = LicNos;
                    }
                    string LicDate = LicDates;
                    if (LicDate == "")
                    {
                        lbllicensedate.Text = "_________________";
                        lbllicsnceoffedate.Text = "_________________";
                    }
                    else
                    {
                        //lbllicensedate.Text = ds.Tables[0].Rows[0]["LicenceDate"].ToString();
                        //lbllicsnceoffedate.Text = ds.Tables[0].Rows[0]["LicenceDate"].ToString();
                        lbllicensedate.Text = LicDates;
                        lbllicsnceoffedate.Text = LicDates;
                    }
                    //txtNodalOfficerAddress.Text = ds.Tables[0].Rows[0]["NodalOfficeraddress"].ToString();
                    //txtNodalOfficerPhoneNo.Text = ds.Tables[0].Rows[0]["NodalOfficerphone"].ToString();
                    //txtNodalOfficerMobileNo.Text = ds.Tables[0].Rows[0]["NodalOfficerMobile"].ToString();
                    //txtNodalOfficerFaxNo.Text = ds.Tables[0].Rows[0]["NodalOfficerFax"].ToString();
                    //txtNodalOfficerEmailAddress.Text = ds.Tables[0].Rows[0]["NodalOfficerEmail"].ToString();

                }
            }
            else
            {
                string qry1 = "select  Hired_Type,Godown_ID,BranchId,LicNum,CONVERT(varchar(10),LicDate,103) as LicDate from [tbl_MetaData_GODOWN_2018] where Godown_ID= (select max(Godown_ID) from  [tbl_storage_Stacking_Details] where WHRId='" + whr + "')";
                string LicNos = "";
                string LicDates = "";
                SqlCommand cmd1 = new SqlCommand(qry1, Con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    //string storagetype = ds1.Tables[0].Rows[0][0].ToString().Trim();
                    LicNos = ds1.Tables[0].Rows[0][3].ToString().Trim();
                    LicDates = ds1.Tables[0].Rows[0][4].ToString().Trim();
                }
                string qry = "Select *  from tbl_MetaData_DEPOT where BranchID='" + Session["BranchID"].ToString() + "'";
                SqlCommand cmd = new SqlCommand(qry, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblcorpnamehnd.Visible = true;
                    lblcorpnamehndof.Visible = true;
                    lbldepositorname1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    Label9.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    lblcorpnamehnd.Text = "";
                    lblcorpnamehndof.Text = "";
                    Image2.Visible = true;
                    Image1.Visible = true;
                    mpwlcAothO.Visible = false;
                    mpwlcothOf.Visible = false;

                    lblbrachname1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    lblbranchoff.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();

                    lblcorpname.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    lblcorpnameof.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                    Image2.Visible = false;
                    Image1.Visible = false;
                    lblcorpnamehnd.Visible = false;
                    lblcorpnamehndof.Visible = false;
                    mpwlcAothO.Visible = true;
                    mpwlcothOf.Visible = true;
                    mpwlcAothO.Text = "MPWLC द्वारा अधिकृत व्यक्ति";
                    mpwlcothOf.Text = "MPWLC द्वारा अधिकृत व्यक्ति";

                    //ddlDepoBeloggsTo.SelectedItem.Text = ds.Tables[0].Rows[0]["DepoBelongs"].ToString();
                    // txtTehsilName.Text = ds.Tables[0].Rows[0]["TehsilName"].ToString();
                    lbladdr.Text = ds.Tables[0].Rows[0]["DepotAddress"].ToString();
                    lbladdoffice.Text = ds.Tables[0].Rows[0]["DepotAddress"].ToString();
                    //  txtLocationPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                    // txtLocationFaxNo.Text = ds.Tables[0].Rows[0]["FaxNo"].ToString();
                    // txtLocationEMailAddress.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                    lblgodampal.Text = ds.Tables[0].Rows[0]["NodalOfficeName"].ToString();
                    lblgodampaloffice.Text = ds.Tables[0].Rows[0]["NodalOfficeName"].ToString();
                    // lbllicensedate.Text = ds.Tables[0].Rows[0]["LicenceDate"].ToString();
                    //lbllicensno.Text = ds.Tables[0].Rows[0]["LincenseNo"].ToString();


                    string licNo = LicNos;
                    if (licNo == "")
                    {
                        lbllicensno.Text = "__________________";
                        lbllicofficeno.Text = "__________________";
                    }
                    else
                    {
                        //lbllicensno.Text = ds.Tables[0].Rows[0]["LincenseNo"].ToString();
                        //lbllicofficeno.Text = ds.Tables[0].Rows[0]["LincenseNo"].ToString();
                        lbllicensno.Text = LicNos;
                        lbllicofficeno.Text = LicNos;
                    }
                    string LicDate = LicDates;
                    if (LicDate == "")
                    {
                        lbllicensedate.Text = "_________________";
                        lbllicsnceoffedate.Text = "_________________";
                    }
                    else
                    {
                        //lbllicensedate.Text = ds.Tables[0].Rows[0]["LicenceDate"].ToString();
                        //lbllicsnceoffedate.Text = ds.Tables[0].Rows[0]["LicenceDate"].ToString();
                        lbllicensedate.Text = LicDates;
                        lbllicsnceoffedate.Text = LicDates;
                    }
                    //txtNodalOfficerAddress.Text = ds.Tables[0].Rows[0]["NodalOfficeraddress"].ToString();
                    //txtNodalOfficerPhoneNo.Text = ds.Tables[0].Rows[0]["NodalOfficerphone"].ToString();
                    //txtNodalOfficerMobileNo.Text = ds.Tables[0].Rows[0]["NodalOfficerMobile"].ToString();
                    //txtNodalOfficerFaxNo.Text = ds.Tables[0].Rows[0]["NodalOfficerFax"].ToString();
                    //txtNodalOfficerEmailAddress.Text = ds.Tables[0].Rows[0]["NodalOfficerEmail"].ToString();

                }
            }

        }
        catch (Exception)
        {
            /////////////
        }

    }

    private void checkifprinted()
    {
        if (ddlDistrict.SelectedValue.ToString() != null)
        {
            string whr = "";
            if (DDLwhr.SelectedIndex != 0)
            {
                whr = DDLwhr.SelectedItem.Text;
            }
            else
            {
                whr = TextBox1.Text;
            }

            District = ddlDistrict.SelectedValue.ToString();
            Depot = ddlDepotList.SelectedValue.ToString();
            string query = "SELECT PrintStatus FROM [Intergrated_MP_STORAGE].[dbo].[whrprintstatus] where WHRID='" + whr + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string whrstatus = ds.Tables[0].Rows[0]["PrintStatus"].ToString();
                if (whrstatus == "1st")
                {
                    //lblcopytype.Text = "ORIGNAL COPY";
                    lblcopytype.Text = "ORIGINAL COPY";
                    //Table1.Visible = true;
                    lblorico.Visible = true;
                    // lbldatefrom.Text = TextBox1.Text;
                    //Label mylabel = (Label)ListView1.FindControl("Label2");
                    //mylabel.Text = TextBox2.Text;
                    WHR.Style.Add("background-image", "url('../images/origanal.png')");
                    WHR.Style.Add("background-repeat", "repeat");
                    Table1.Style.Add("background-image", "url('../images/officecopy.png')");
                    Table1.Style.Add("background-repeat", "repeat");
                    // imgori.ImageUrl = "../../images/origanal.png";
                    btnbmpassword.Enabled = true;
                    btnprint.Visible = true;
                }
                else if (whrstatus == "2nd")
                {
                    btnprint.Visible = false;
                    btnbmpassword.Enabled = false;
                    lblcopytype.Text = "PHOTO COPY";
                    Table1.Visible = false;
                    lblorico.Visible = false;
                    // WHR.Style.Add("background-image", "url('../../images/officecopy.png')");
                    //WHR.Style.Add("background-repeat", "repeat");
                    WHR.Style.Add("background-image", "url('../images/photocopy.png')");
                    WHR.Style.Add("background-repeat", "repeat");
                    Table1.Style.Add("background-image", "url('../../images/photocopy.png')");
                    Table1.Style.Add("background-repeat", "repeat");
                }
                else if (whrstatus == "4th")
                {
                    btnprint.Visible = false;
                    btnbmpassword.Enabled = true;
                    lblcopytype.Text = "DUPLICATE COPY";
                    lblorico.Text = "DUPLICATE COPY";
                    Table1.Visible = false;
                    lblorico.Visible = false;
                    // WHR.Style.Add("background-image", "url('../../images/officecopy.png')");
                    //WHR.Style.Add("background-repeat", "repeat");
                    WHR.Style.Add("background-image", "url('../../images/dupli.png')");
                    WHR.Style.Add("background-repeat", "repeat");
                    Table1.Style.Add("background-image", "url('../../images/photocopy.png')");
                    Table1.Style.Add("background-repeat", "repeat");
                }
                else
                {
                    btnprint.Visible = false;
                    btnbmpassword.Enabled = false;
                    lblcopytype.Text = "PHOTO COPY";
                    Table1.Visible = false;
                    WHR.Style.Add("background-image", "url('../../images/photocopy.png')");
                    WHR.Style.Add("background-repeat", "repeat");
                    Table1.Style.Add("background-image", "url('../../images/photocopy.png')");
                    Table1.Style.Add("background-repeat", "repeat");
                }
            }
            else
            {

            }
        }
    }
    protected void DDLwhr_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        Panel1.Visible = true;
        ListView1.DataSource = null;

        ListView1.DataBind();
        ListView2.DataSource = null;

        ListView2.DataBind();
        lvofficem.DataSource = null;
        lvofficem.DataBind();
        lvoffice.DataSource = null;
        lvoffice.DataBind();
        //btnprint.Visible = false;
        whrdetail();
        qtyissuedtl();
        checkifprinted();
        //GetDepotBelongs(Session["Depot_DepotID"].ToString());
        GetDepotBelongs(Branch);
        //GetWhr_Stacks();
        GetDepositorForm_Detail();
        //GetArrivalTypeofWHR();
        Verify_eWHR();
    }
    public void Verify_eWHR()
    {
        //string query
        string Enter_WHR = "";
        if (DDLwhr.SelectedItem.Text != "--Select")
        {
            Enter_WHR = DDLwhr.SelectedItem.Text;
        }
        else if (TextBox1.Text.ToString() != "")
        {
            Enter_WHR = TextBox1.Text.ToString();
        }
        string Is_DSC_Verified = "";
        string Depositor_WHR_Id = "";
        string Commodity_Id = "";
        string Depositor_Name = "";
        string Date_of_Deposit = "";
        string TotalBags_Received = "";
        string Total_Qty_Received = "";
        string AvgMoisture_Content = "";
        string MktValue_of_Commodity = "";
        string WHR_Issue_Date = "";
        string WHR_CreatedDate = "";
        string WHR_Client_IP = "";
        string CropYear = "";
        string Remark = "";
        string BranchID = "";
        string DepositorID = "";
        string GodownID = "";
        string Depositor_Form_No = "";
        string Grade = "";
        string CheckSum = "";
        string District_Id = "";
        string WHR_Signing_Date = "";
        string DSC_Holder_Name = "";
        string DSC_Serial_No = "";
        string DSC_Signed_Ip = "";
        string AvgMoisture_Content_To = "";
        string SangrahadDate = "";
        string LocalIp = "";
        //string query1 = "SELECT [Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],DSWHR.[CreatedDate],DSWHR.[CreatedBy],DSWHR.DSC_Serial_No,DSWHR.DSC_Holder_Name,DSWHR.Client_Ip from tbl_Digitally_Signed_WHR_Details as DSWHR inner join tbl_Digital_Signature_Details as DSSign on DSSign.WHR_No=DSWHR.Depositor_WHR_Id where DSWHR.Depositor_WHR_Id='" + Enter_WHR + "' and DSWHR.DSC_User_Type!='G' and DSWHR.DSC_Serial_No=DSSign.DSC_Serial_No and DSWHR.Depositor_WHR_Id in (select S.Depositor_whr_id from View_Digitally_Signed_WHR_Detail as S where S.User_Type='B')";
        string query1 = "SELECT [Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],DSWHR.[CreatedDate],DSWHR.[CreatedBy],DSWHR.DSC_Serial_No,DSWHR.DSC_Holder_Name,DSWHR.Client_Ip from tbl_Digitally_Signed_WHR_CMS2022 as DSWHR inner join tbl_Digital_Signature_CMS2022 as DSSign on DSSign.WHR_No=DSWHR.Depositor_WHR_Id where DSWHR.Depositor_WHR_Id='" + Enter_WHR + "' and DSWHR.DSC_User_Type!='G' and DSWHR.DSC_Serial_No=DSSign.DSC_Serial_No and DSWHR.Depositor_WHR_Id in (select S.Depositor_whr_id from View_Digitally_Signed_WHR_CMS2022 as S where S.User_Type='B')";

        SqlCommand cmd = new SqlCommand(query1, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet XMLds = new DataSet();
        da.Fill(XMLds);
        if (XMLds.Tables[0].Rows.Count > 0)
        {
            int WS = 0;
            //string whrstatus = ds.Tables[0].Rows[0]["PrintStatus"].ToString();
            Depositor_WHR_Id = XMLds.Tables[0].Rows[0]["Depositor_WHR_Id"].ToString();
            Commodity_Id = XMLds.Tables[0].Rows[0]["Commodity_Id"].ToString();
            Depositor_Name = XMLds.Tables[0].Rows[0]["Depositor_Name"].ToString();
            Date_of_Deposit = XMLds.Tables[0].Rows[0]["Date_of_Deposit"].ToString();
            TotalBags_Received = XMLds.Tables[0].Rows[0]["TotalBags_Received"].ToString();
            Total_Qty_Received = XMLds.Tables[0].Rows[0]["Total_Qty_Received"].ToString();
            AvgMoisture_Content = XMLds.Tables[0].Rows[0]["AvgMoisture_Content"].ToString();
            MktValue_of_Commodity = XMLds.Tables[0].Rows[0]["MktValue_of_Commodity"].ToString();
            WHR_Issue_Date = XMLds.Tables[0].Rows[0]["WHR_Issue_Date"].ToString();
            WHR_CreatedDate = XMLds.Tables[0].Rows[0]["WHR_CreatedDate"].ToString();
            WHR_Client_IP = XMLds.Tables[0].Rows[0]["WHR_Client_IP"].ToString();
            CropYear = XMLds.Tables[0].Rows[0]["CropYear"].ToString();
            Remark = XMLds.Tables[0].Rows[0]["Remark"].ToString();
            BranchID = XMLds.Tables[0].Rows[0]["BranchID"].ToString();
            DepositorID = XMLds.Tables[0].Rows[0]["DepositorID"].ToString();
            GodownID = XMLds.Tables[0].Rows[0]["GodownID"].ToString();
            Depositor_Form_No = XMLds.Tables[0].Rows[0]["Depositor_Form_No"].ToString();
            Grade = XMLds.Tables[0].Rows[0]["Category_Id"].ToString();
            District_Id = XMLds.Tables[0].Rows[0]["District_Id"].ToString();
            CheckSum = XMLds.Tables[0].Rows[0]["WHR_Check_Sum"].ToString();
            DateTime Created_Date = Convert.ToDateTime(XMLds.Tables[0].Rows[0]["CreatedDate"]);
            WHR_Signing_Date = Created_Date.ToString("dd/MM/yyyy HH:mm:ss");
            DSC_Holder_Name = XMLds.Tables[0].Rows[0]["DSC_Holder_Name"].ToString();
            DSC_Serial_No = XMLds.Tables[0].Rows[0]["DSC_Serial_No"].ToString();
            DSC_Signed_Ip = XMLds.Tables[0].Rows[0]["CreatedBy"].ToString();
            AvgMoisture_Content_To = XMLds.Tables[0].Rows[0]["AvgMoisture_Content_To"].ToString();
            SangrahadDate = XMLds.Tables[0].Rows[0]["SangrahadDate"].ToString();
            LocalIp = XMLds.Tables[0].Rows[0]["Client_Ip"].ToString();
            //Generate CSum
            string CheckSumString = Depositor_WHR_Id + Commodity_Id + Depositor_Name + Date_of_Deposit + TotalBags_Received + Total_Qty_Received + AvgMoisture_Content + MktValue_of_Commodity + WHR_Issue_Date + WHR_CreatedDate + WHR_Client_IP + CropYear + Remark + BranchID + DepositorID + GodownID + Depositor_Form_No + Grade + District_Id + AvgMoisture_Content_To + SangrahadDate + DSC_Serial_No + LocalIp + DSC_Holder_Name;
            var sha1 = System.Security.Cryptography.SHA1.Create();
            byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
            byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
            //var hashstr  = Convert.ToBase64String(hash);
            var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");
            //Generate CSum
            if (hashstrs == CheckSum)
            {
                if (LocalIp == null || LocalIp == "")
                {
                    LocalIp = DSC_Signed_Ip;
                }
                Is_DSC_Verified = "Y";
                lbldscT.Text = "e-WHR Digitally Signed By";
                lblDSC_Holder.Text = DSC_Holder_Name.ToString();
                //lblSerNo.Text = "Serial Number : " + DSC_Serial_No.ToString();
                lblSigningDate.Text = "Sign Date : " + WHR_Signing_Date.ToString();
                lblIpAdd.Text = "Signed IP Address : " + LocalIp.ToString();

                lbldscT2.Text = "e-WHR Digitally Signed By";
                lblDSC_Holder2.Text = DSC_Holder_Name.ToString();
                //lblSerNo2.Text = "Serial Number : " + DSC_Serial_No.ToString();
                lblSigningDate2.Text = "Sign Date : " + WHR_Signing_Date.ToString();
                lblIpAdd2.Text = "Signed IP Address : " + LocalIp.ToString();
                Image5.Visible = true;
                Image6.Visible = true;


                //btnprint.Visible = true;
                btnbmpassword.Enabled = false;

            }
            else
            {
                Is_DSC_Verified = "N";
                lbldscT.Text = "";
                lblDSC_Holder.Text = "";
                //lblSerNo.Text = "";
                lblSigningDate.Text = "";
                lblIpAdd.Text = "";

                lbldscT2.Text = "";
                lblDSC_Holder2.Text = "";
                //lblSerNo2.Text = "";
                lblSigningDate2.Text = "";
                lblIpAdd2.Text = "";
                Image5.Visible = false;
                Image6.Visible = false;
            }
        }
        else
        {
            lbldscT.Text = "";
            lblDSC_Holder.Text = "";
            //lblSerNo.Text = "";
            lblSigningDate.Text = "";
            lblIpAdd.Text = "";

            lbldscT2.Text = "";
            lblDSC_Holder2.Text = "";
            //lblSerNo2.Text = "";
            lblSigningDate2.Text = "";
            lblIpAdd2.Text = "";
            Image5.Visible = false;
            Image6.Visible = false;
        }
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        string Agency = "";
        string UserId = Session["RoleId"].ToString();
        //if (UserId == "10")
        //{
        //    Agency = "MPSCSC";
        //}
        //else if (UserId == "9")
        //{
        Agency = "MARKFED";
        //}
        string whr = "";
        if (DDLwhr.SelectedIndex != 0)
        {
            whr = DDLwhr.SelectedItem.Text;
        }
        else
        {
            whr = TextBox1.Text;
        }
        string query1 = "SELECT PrintStatus FROM [Intergrated_MP_STORAGE].[dbo].[whrprintstatus] where WHRID='" + lblwhrno.Text + "'";
        SqlCommand cmd = new SqlCommand(query1, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            int WS = 0;
            int WL = 0;
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string whrstatus = ds.Tables[0].Rows[0]["PrintStatus"].ToString();
            if (whrstatus == "1st")
            {
                //Log
                string queryL = "insert into WHRPrintStatus_Log(WHRID,PrintStatus,DateCreated,CreatedBy) values ('" + whr + "','2nd',GETDATE(),'" + ip + "')";
                Con.Open();
                SqlCommand cmdL = new SqlCommand(queryL, Con);
                WL = cmdL.ExecuteNonQuery();
                Con.Close();
                //Log
                string query = "update [whrprintstatus] set PrintStatus='2nd' where WHRID='" + whr + "'";

                Con.Open();
                SqlCommand cmd2 = new SqlCommand(query, Con);

                WS = cmd2.ExecuteNonQuery();
                Con.Close();
                //whrdetail();
                //qtyissuedtl();
                // btnprint.Attributes.Add("OnClientClick", "PrintDiv();self.opener=null;self.close();return false;");
                if (WS > 0)
                {
                    //update in DS

                    //string queryDS = "update [tbl_DSC_eWHR_Submission] set PrintedDate=GETDATE(),PrintedIp='" + ip + "' where WHR_Id='" + whr + "'";
                    string queryDS = "update [tbl_DSC_eWHR_Submission_CMS2022] set PrintedDate=GETDATE(),PrintedIp='" + ip + "',PrintedBy='" + Agency + "' where WHR_Id='" + whr + "'";

                    Con.Open();
                    SqlCommand cmdDS = new SqlCommand(queryDS, Con);

                    int WSDS = cmdDS.ExecuteNonQuery();
                    Con.Close();
                    if (WSDS > 0)
                    {
                        Panel1.Visible = false;
                        whrdetail();
                        qtyissuedtl();
                        checkifprinted();
                    }
                }
                else
                {
                    SetPrintS();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Error Please Contact to Head Office...'); </script> ");

                }
                //GetDepotBelongs(Session["Depot_DepotID"].ToString());

            }
            //else if (whrstatus == "4th")
            //{
            //    string query = "update [whrprintstatus] set PrintStatus='2nd' where WHRID='" + whr + "'";
            //    Con.Open();
            //    SqlCommand cmd2 = new SqlCommand(query, Con);

            //    WS = cmd2.ExecuteNonQuery();
            //    Con.Close();
            //    //whrdetail();
            //    //qtyissuedtl();
            //    // btnprint.Attributes.Add("OnClientClick", "PrintDiv();self.opener=null;self.close();return false;");
            //    if (WS > 0)
            //    {
            //        Panel1.Visible = false;
            //        whrdetail();
            //        qtyissuedtl();
            //        checkifprinted();
            //    }
            //    else
            //    {
            //        SetPrintS();
            //        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Error Please Contact to Head Office...'); </script> ");

            //    }
            //    //GetDepotBelongs(Session["Depot_DepotID"].ToString());

            //}
            //else
            //{
            //    string query = "update [whrprintstatus] set PrintStatus='3rd' where WHRID='" + whr + "'";
            //    Con.Open();
            //    SqlCommand cmd2 = new SqlCommand(query, Con);

            //    WS = cmd2.ExecuteNonQuery();
            //    Con.Close();
            //    if (WS > 0)
            //    {
            //        Panel1.Visible = false;
            //        //whrdetail();
            //        //qtyissuedtl();
            //        whrdetail();
            //        qtyissuedtl();
            //        checkifprinted();
            //    }
            //    else
            //    {
            //        SetPrintS();
            //    }
            //    //GetDepotBelongs(Session["Depot_DepotID"].ToString());

            //}
        }
        fillwhrList();
    }
    public void SetPrintS()
    {
        int s = 0;
        string whr = "";
        if (DDLwhr.SelectedIndex != 0)
        {
            whr = DDLwhr.SelectedItem.Text;
        }
        else
        {
            whr = TextBox1.Text;
        }
        string query = "update [whrprintstatus] set PrintStatus='2nd' where WHRID='" + whr + "'";
        Con.Open();
        SqlCommand cmd2 = new SqlCommand(query, Con);

        s = cmd2.ExecuteNonQuery();
        if (s == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Error Please Contact to Head Office...'); </script> ");

        }
        Con.Close();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        District = ddlDistrict.SelectedValue.ToString();
        Depot = ddlDepotList.SelectedValue.ToString();
        //string query = "select distinct Depositor_whr_id,WHR_Issue_Date from tbl_storage_Depositor_WHR_Relation inner join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID=tbl_storage_Depositor_WHR_Relation.GodownID where tbl_storage_Depositor_WHR_Relation.District_id ='" + District + "' and tbl_storage_Depositor_WHR_Relation.BranchID='" + Session["BranchId"] + "' and Gid is null and Depositor_whr_id='" + TextBox1.Text + "' and tbl_MetaData_GODOWN.Hired_Type in ('Owned','Hired','JointVenture(JV)','Markfed','CWC') and Commodity_Id in ('123','31','92','65','27','8','11','13','14') and  tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id in ( select distinct WHRId from tbl_storage_Stacking_Details as RD inner join Receive_Proc_Kharif2018 as RPK on RPK.StorageReceipt_Id=RD.StorageReceipt_Id where RD.Branchid='" + Session["BranchId"] + "' ) order by tbl_storage_Depositor_WHR_Relation.WHR_Issue_Date desc";
        //Rabi 2019
        //string query = "select distinct Depositor_wqhr_id,WHR_Issue_Date from tbl_storage_Depositor_WHR_Relation inner join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID=tbl_storage_Depositor_WHR_Relation.GodownID where tbl_storage_Depositor_WHR_Relation.District_id ='" + District + "' and tbl_storage_Depositor_WHR_Relation.BranchID='" + Session["BranchId"] + "' and Gid is null and Depositor_whr_id='" + TextBox1.Text + "' and tbl_MetaData_GODOWN.Hired_Type in ('Owned','Hired','JointVenture(JV)','Markfed','CWC') and Commodity_Id in ('63','64','33')";
        //string query = "select distinct Depositor_wqhr_id,WHR_Issue_Date from tbl_storage_Depositor_WHR_Relation inner join tbl_MetaData_GODOWN_2018 on tbl_MetaData_GODOWN_2018.Godown_ID=tbl_storage_Depositor_WHR_Relation.GodownID where tbl_storage_Depositor_WHR_Relation.District_id ='" + District + "' and tbl_storage_Depositor_WHR_Relation.BranchID='" + Session["BranchId"] + "' and Gid is null and Depositor_whr_id='" + TextBox1.Text + "' and tbl_MetaData_GODOWN_2018.Hired_Type in ('Owned','Hired','Markfed','CWC') and Commodity_Id in ('63','64','33')";
        string query = "select distinct Depositor_whr_id,WHR_Issue_Date from tbl_storage_Depositor_WHR_Relation inner join tbl_MetaData_GODOWN_2018 on tbl_MetaData_GODOWN_2018.Godown_ID=tbl_storage_Depositor_WHR_Relation.GodownID where tbl_storage_Depositor_WHR_Relation.District_id ='" + District + "' and tbl_storage_Depositor_WHR_Relation.BranchID='" + Depot + "' and Gid is null and Depositor_whr_id='" + TextBox1.Text + "' and tbl_MetaData_GODOWN_2018.Hired_Type in ('Owned','Hired','Markfed','CWC') and Commodity_Id in ('63','64','33','92','27')";

        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //Action
            DDLwhr.SelectedIndex = 0;
            // Panel1.Visible = true;
            ListView1.DataSource = null;

            ListView1.DataBind();
            ListView2.DataSource = null;

            ListView2.DataBind();

            lvofficem.DataSource = null;
            lvofficem.DataBind();
            lvoffice.DataSource = null;
            lvoffice.DataBind();
            btnprint.Visible = false;
            whrdetail();
            qtyissuedtl();
            checkifprinted();
            //GetDepotBelongs(Session["Depot_DepotID"].ToString());
            GetDepotBelongs(Branch);
            //GetWhr_Stacks();
            //GetDepositorForm_Detail();
            //TextBox2.Text= NumbersToWords(512000012);
            //GetArrivalTypeofWHR();
            //Action End
            Verify_eWHR();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Invalid WHR NO.'); </script> ");
        }
    }

    protected void btnsubmitpwd_Click(object sender, EventArgs e)
    {
        //string query2 = "SELECT [BranchID],[BranchTypeID],[BranchPwd] FROM [dbo].[MetaDataBranchWithIssueCenter] where BranchID='" + Session["BranchID"].ToString() + "' and [BranchPwd]='" + txtPassword.Text.Trim() + "'";
        string query2 = "SELECT [BranchID],[BranchTypeID],[BranchPwd] FROM [dbo].[MetaDataBranchWithIssueCenter] where BranchID='" + Branch + "' and [BranchPwd]='" + txtPassword.Text.Trim() + "'";

        SqlCommand cmdd = new SqlCommand(query2, Con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmdd);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        if (ds1.Tables[0].Rows.Count > 0)
        {

            //  btnprint.Text = "Print";
            btnprint.Visible = true;
            btnbmpassword.Enabled = false;
            //  whrdetail();
            qtyissuedtl();

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Wrong Password'); </script> ");
            btnprint.Visible = false;
        }
    }
    public void GetArrivalTypeofWHR()
    {
        try
        {
            string query = "";
            string s = "";
            string whr = "";
            if (DDLwhr.SelectedIndex != 0)
            {
                whr = DDLwhr.SelectedItem.Text;
            }
            else
            {
                whr = TextBox1.Text;
            }
            query = "select Arrival_Source from  tbl_storage_Depositor_WHR_Relation where Depositor_WHR_Id='" + whr + "'";
            Con.Open();
            SqlCommand cmd2 = new SqlCommand(query, Con);

            s = cmd2.ExecuteScalar().ToString();
            if (s != "" && s != null)
            {
                ArrivalSource = s;
            }
            else
            {
                ArrivalSource = "";
            }
            Con.Close();
        }
        catch (Exception)
        {
            ArrivalSource = "";
        }
        finally
        {
            Con.Close();
        }
    }
    //private void GetWhr_Stacks()
    //{
    //    if (ddlDistrict.SelectedValue.ToString() != null)
    //    {
    //        District = ddlDistrict.SelectedValue.ToString();
    //        Depot = ddlDepotList.SelectedValue.ToString();
    //        string whr = "";
    //        if (DDLwhr.SelectedIndex != 0)
    //        {
    //            whr = DDLwhr.SelectedItem.Text;
    //        }
    //        else
    //        {
    //            whr = TextBox1.Text;
    //        }
    //        string AllStack = "";
    //        string Stack = "";
    //        //string query = "SELECT mg.Godown_Name,ms.Stack_Name,isnull([No_Of_Bags],0)[No_Of_Bags], isnull([Bags_Weight],0)Bags_Weight,isnull(WHR.Total_Qty_Received,0) Total_Qty_Received,isnull(WHR.TotalBags_Received,0)TotalBags_Received,(WHR.Total_Qty_Received-loss+gain-[Bags_Weight]) as [Bags_Weight_iswsue],Loss,Gain,CONVERT(VARCHAR(10),sge.Issue_Date,103) as ID,Row_number()OVER(ORDER BY sge.Issue_Date) rn from [tbl_Delivery_Stacking_Details_GatePass] as ssd inner join tbl_MetaData_GODOWN as mg on ssd.Godown_ID=mg.Godown_ID inner join tbl_MetaData_STACK as ms on ssd.Stack_ID=ms.Stack_ID inner join tbl_storage_Depositor_WHR_Relation AS WHR on ssd.Depositor_WHR_Id=WHR.Depositor_WHR_Id inner join dbo.tbl_Storage_GatePass_Enrty as sge on ssd.GatePass_No=sge.GatePass_No where WHR.Depositor_WHR_Id='" + whr + "' order by sge.Issue_Date asc";
    //        string query = "select ST.Stack_Name from tbl_storage_Stacking_Details as SD inner join tbl_MetaData_STACK as ST on ST.Stack_ID=SD.Stack_ID where SD.WHRId='" + whr + "' order by ST.Stack_Name";

    //        SqlCommand cmd = new SqlCommand(query, Con);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);

    //        //DataTable dss = new DataTable();
    //        //dss.Columns.Add("Date", typeof(string));
    //        //dss.Columns.Add("DelQty", typeof(string));
    //        //dss.Columns.Add("AvlQty", typeof(string));
    //        //dss.Columns.Add("DelBags", typeof(string));
    //        //dss.Columns.Add("AvlBags", typeof(string));
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            //decimal avilableQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString());
    //            //int avilableBags = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalBags_Received"].ToString()); ;
    //            //DDLwhr.DataSource = ds.Tables[0];
    //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //            {
    //                if (i == 0)
    //                {
    //                    Stack = ds.Tables[0].Rows[i]["Stack_Name"].ToString();
    //                    AllStack = Stack;
    //                }
    //                else
    //                {
    //                    Stack = ds.Tables[0].Rows[i]["Stack_Name"].ToString();
    //                    AllStack = AllStack + ',' + Stack;
    //                }
    //            }
    //            lblStack.Text = AllStack.ToString();
    //            lblStack2.Text = AllStack.ToString();

    //        }
    //        else
    //        {
    //            lblStack.Text = "--";
    //            lblStack2.Text = "--";
    //        }
    //    }
    //}
    private void GetDepositorForm_Detail()
    {
        if (ddlDistrict.SelectedValue.ToString() != null)
        {
            District = ddlDistrict.SelectedValue.ToString();
            Depot = ddlDepotList.SelectedValue.ToString();
            string whr = "";
            if (DDLwhr.SelectedIndex != 0)
            {
                whr = DDLwhr.SelectedItem.Text;
            }
            else
            {
                whr = TextBox1.Text;
            }
            //string AllStack = "";
            //string query = "select KR.Depositor_Form_No,KR.Acceptance_No,(S.Society_Name+'('+KR.Purchase_Center+')') as Society,RC.Ack_Book_No from Receive_Proc_Kharif2018 as KR inner join tbl_Storage_Receipt_Details as RC on RC.StorageReceipt_Id=KR.StorageReceipt_Id and RC.Acpt_FCIRO_No=KR.Depositor_Form_No inner join MPSCSC.dbo.Society_Pulses18 as S on S.PCID=KR.Purchase_Center where RC.WHR_Id='" + whr + "' and RC.WHR_Flag='Y'";
            //Rabi 2019
            //string query = "select KR.Depositor_Form_No,KR.Acceptance_No,(S.Society_Name+'('+KR.Purchase_Center+')') as Society,RC.Ack_Book_No from Receive_Proc_CMS2019 as KR inner join tbl_Storage_Receipt_Details as RC on RC.StorageReceipt_Id=KR.StorageReceipt_Id and RC.Acpt_FCIRO_No=KR.Depositor_Form_No inner join MPSCSC.dbo.UparjanKendra_Wht2019 as S on S.Cntr_ID=KR.Purchase_Center where RC.WHR_Id='" + whr + "' and RC.WHR_Flag='Y'";
            string query = "";
            //if (ddlCommodity.SelectedValue == "52")
            //{
            //    query = "select KR.Depositor_Form_No,KR.Acceptance_No,(S.Society_Name+'('+KR.Purchase_Center+')') as Society,RC.Ack_Book_No from Receive_Proc_Arhar2019 as KR inner join tbl_Storage_Receipt_Details as RC on RC.StorageReceipt_Id=KR.StorageReceipt_Id and RC.Acpt_FCIRO_No=KR.Depositor_Form_No inner join MPSCSC.dbo.UparjanKendra_Tuar as S on S.PCID=KR.Purchase_Center where RC.WHR_Id='" + whr + "' and RC.WHR_Flag='Y'";
            //}
            //else
            //{
            //query = "select KR.Depositor_Form_No,KR.Acceptance_No,(S.Society_Name+'('+KR.Purchase_Center+')') as Society,RC.Ack_Book_No from Receive_Proc_CMS2019 as KR inner join tbl_Storage_Receipt_Details as RC on RC.StorageReceipt_Id=KR.StorageReceipt_Id and RC.Acpt_FCIRO_No=KR.Depositor_Form_No inner join MPSCSC.dbo.uparjankendra_csm2019 as S on S.Cntr_ID=KR.Purchase_Center where RC.WHR_Id='" + whr + "' and RC.WHR_Flag='Y'";
            //query = "select KR.Depositor_Form_No,KR.Acceptance_No,(S.Society_Name+'('+KR.Purchase_Center+')') as Society,RC.Ack_Book_No from Receive_Proc_CMS2019 as KR inner join tbl_Storage_Receipt_Details as RC on RC.StorageReceipt_Id=KR.StorageReceipt_Id and RC.Acpt_FCIRO_No=KR.Depositor_Form_No inner join MPSCSC.dbo.uparjankendra_csm2019 as S on S.Cntr_ID=KR.Purchase_Center where RC.WHR_Id='" + whr + "' and RC.WHR_Flag='Y'";
            //query = "select Acpt_FCIRO_No as Depositor_Form_No from tbl_Storage_Receipt_Details where WHR_Id='" + whr + "' and WHR_Flag='Y'";
            query = "select Acpt_FCIRO_No as Depositor_Form_No from tbl_Storage_Receipt_Details where WHR_Id='" + whr + "' and WHR_Flag='Y' and District_Id='" + District + "' and BranchID='" + Depot + "'";


            //}
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //string ACCREJ = ds.Tables[0].Rows[0]["Ack_Book_No"].ToString();
                //if (ACCREJ != "Rejected")
                //{
                //    lblAccRej.Text = "Acceptance No.:";
                //    lblAccRej2.Text = "Acceptance No.:";
                //}
                //else if (ACCREJ == "Rejected")
                //{
                //    lblAccRej.Text = "Rejection No.:";
                //    lblAccRej2.Text = "Rejection No.:";
                //}
                lblDFNo.Text = ds.Tables[0].Rows[0]["Depositor_Form_No"].ToString();
                //lblAccNo.Text = ds.Tables[0].Rows[0]["Acceptance_No"].ToString();
                //lblSname.Text = ds.Tables[0].Rows[0]["Society"].ToString();

                lblDFNo2.Text = ds.Tables[0].Rows[0]["Depositor_Form_No"].ToString();
                //lblAccNo2.Text = ds.Tables[0].Rows[0]["Acceptance_No"].ToString();
                //lblSname2.Text = ds.Tables[0].Rows[0]["Society"].ToString();

            }
            else
            {
                //lblDFNo.Text = "";
                //lblAccNo.Text = "";
                //lblSname.Text = "";
                //lblDFNo2.Text = "";
                //lblAccNo2.Text = "";
                //lblSname2.Text = "";
                //string query2 = "select KR.Depositor_Form_No,KR.Acceptance_No,(S.Society_Name+'('+KR.Purchase_Center+')') as Society from Receive_Proc_Kharif2018 as KR inner join tbl_Storage_Receipt_Details as RC on RC.StorageReceipt_Id=KR.StorageReceipt_Id inner join MPSCSC.dbo.Society_Pulses18 as S on S.PCID=KR.Purchase_Center where RC.WHR_Id='" + whr + "' and RC.WHR_Flag='Y'";
                //string query2 = "select KR.Depositor_Form_No,KR.Acceptance_No,(S.Society_Name+'('+KR.Purchase_Center+')') as Society from Receive_Proc_Kharif2018 as KR inner join tbl_Storage_Receipt_Details as RC on RC.StorageReceipt_Id=KR.StorageReceipt_Id inner join MPSCSC.dbo.society_kharif18 as S on S.Society_Id=KR.Purchase_Center where RC.WHR_Id='" + whr + "' and RC.WHR_Flag='Y'";
                string query2 = "select KR.Depositor_Form_No,KR.Acceptance_No,(S.Society_Name+'('+KR.Purchase_Center+')') as Society,RC.Ack_Book_No from Receive_Proc_Kharif2018 as KR inner join tbl_Storage_Receipt_Details as RC on RC.StorageReceipt_Id=KR.StorageReceipt_Id and RC.Acpt_FCIRO_No=KR.Depositor_Form_No inner join MPSCSC.dbo.society_kharif18 as S on S.Society_Id=KR.Purchase_Center where RC.WHR_Id='" + whr + "' and RC.WHR_Flag='Y'";


                SqlCommand cmd2 = new SqlCommand(query2, Con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    string ACCREJ = ds2.Tables[0].Rows[0]["Ack_Book_No"].ToString();
                    if (ACCREJ != "Rejected")
                    {
                        lblAccRej.Text = "Acceptance No.:";
                        lblAccRej2.Text = "Acceptance No.:";
                    }
                    else if (ACCREJ == "Rejected")
                    {
                        lblAccRej.Text = "Rejection No.:";
                        lblAccRej2.Text = "Rejection No.:";
                    }
                    lblDFNo.Text = ds2.Tables[0].Rows[0]["Depositor_Form_No"].ToString();
                    lblAccNo.Text = ds2.Tables[0].Rows[0]["Acceptance_No"].ToString();
                    lblSname.Text = ds2.Tables[0].Rows[0]["Society"].ToString();

                    lblDFNo2.Text = ds2.Tables[0].Rows[0]["Depositor_Form_No"].ToString();
                    lblAccNo2.Text = ds2.Tables[0].Rows[0]["Acceptance_No"].ToString();
                    lblSname2.Text = ds2.Tables[0].Rows[0]["Society"].ToString();

                }
                else
                {
                    lblDFNo.Text = "";
                    lblAccNo.Text = "";
                    lblSname.Text = "";
                    lblDFNo2.Text = "";
                    lblAccNo2.Text = "";
                    lblSname2.Text = "";
                }
            }
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex != 0)
        {
            getDepot(ddlDistrict.SelectedValue.ToString());
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District')", true);
        }
    }
    private void getDepot(string distId)
    {
        try
        {
            string query = "";
            //query = "select depo.BranchId,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' and DepoTypeID='4' order by DepotName asc";
            query = "select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";

            cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepotList.DataSource = ds.Tables[0];
                ddlDepotList.DataTextField = "DepotName";
                ddlDepotList.DataValueField = "BranchId";
                ddlDepotList.DataBind();
                ddlDepotList.Items.Insert(0, "---Select---");
                //ddlGodown.DataSource = null;
                //ddlGodown.DataBind();
                //gv.DataSource = null;
                //gv.DataBind();
            }
            else
            {
                ddlDepotList.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception)
        {
            ///////
        }
    }
    protected void ddlCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillwhrList();
    }
    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillwhrList();
    }
}