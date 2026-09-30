using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for Get_WMS_WHR_List
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Get_WMS_WHR_List : System.Web.Services.WebService
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    private SqlCommand cmd = new SqlCommand();

    private SqlCommand cmd1 = new SqlCommand();

    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;
    string Query = "";
    public Get_WMS_WHR_List()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "This Method Is Used to Get WHR List")]
    public DataSet Retrive_WHR_List(string BG_Id, string District_Id, string User_Type, string Credential, string Commodity, string CropYear)
    {
        try
        {
            WarehouseApiSecurity.RequireCredential(Credential, "LegacyWlcWhrCredential");
            string query = "";
            string GetBranchId = String.Empty;
            //'129','10535','4679'
            if ((Commodity == "63" || Commodity == "64" || Commodity == "33" || Commodity == "92" || Commodity == "27" || Commodity == "26") && CropYear == "2026-27")
            {
                if (User_Type == "B")
                {
                    //('4679', '10535')
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2026-27' and WHR.DepositorID in('10535','4679','15478') and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2026 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";

                }
                else if (User_Type == "G")
                {
                    GetBranchId = Get_BranchId(BG_Id);

                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2026-27' and WHR.DepositorID in('10535','4679','15478') and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2026 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";

                } 
            }
            else if (Commodity == "75" && CropYear == "2025-26")
            {
                if (User_Type == "B")
                {
                    //('4679', '10535')
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2025-26' and WHR.DepositorID in('10535','4679','15478') and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2026 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";

                }
                else if (User_Type == "G")
                {
                    GetBranchId = Get_BranchId(BG_Id);

                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2025-26' and WHR.DepositorID in('10535','4679','15478') and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2026 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";

                }
            }
            else if (Commodity == "63" || Commodity == "64" || Commodity == "33" || Commodity == "92" || Commodity == "27" || Commodity == "26" || Commodity == "75" && CropYear == "2024-25")
            {
                if (User_Type == "B")
                {
                    //('4679', '10535')
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2024-25' and WHR.DepositorID in('10535','4679','15478') and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2024 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";

                }
                else if (User_Type == "G")
                {
                    GetBranchId = Get_BranchId(BG_Id);

                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2024-25' and WHR.DepositorID in('10535','4679','15478') and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2024 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";

                }
            }
            else if (Commodity == "63" || Commodity == "64" || Commodity == "33" || Commodity == "92" || Commodity == "27" && CropYear == "2022-23")
            {
                if (User_Type == "B")
                {
                    //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2022-23' and WHR.DepositorID='10535' and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2022 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2022-23' and WHR.DepositorID in('4679','10535') and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2022 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";

                }
                else if (User_Type == "G")
                {
                    GetBranchId = Get_BranchId(BG_Id);
                    //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2022-23' and WHR.DepositorID='10535' and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2022 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2022-23' and WHR.DepositorID in('4679','10535') and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2022 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";

                }
            }


            else if (Commodity == "63" || Commodity == "64" || Commodity == "33" || Commodity == "92" || Commodity == "27" && CropYear == "2023-24")
            {
                if (User_Type == "B")
                {
                    //('4679', '10535')
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2023-24' and WHR.DepositorID in('10535','4679') and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2023 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";

                }
                else if (User_Type == "G")
                {
                    GetBranchId = Get_BranchId(BG_Id);

                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2023-24' and WHR.DepositorID in('10535','4679') and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2023 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";

                }
            }
            else if (Commodity == "63" || Commodity == "64" || Commodity == "33" || Commodity == "92" || Commodity == "27" && CropYear == "2022-23")
            {
                if (User_Type == "B")
                {
                    //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2022-23' and WHR.DepositorID='10535' and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2022 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2022-23' and WHR.DepositorID in('4679','10535') and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2022 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";

                }
                else if (User_Type == "G")
                {
                    GetBranchId = Get_BranchId(BG_Id);
                    //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2022-23' and WHR.DepositorID='10535' and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2022 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2022-23' and WHR.DepositorID in('4679','10535') and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2022 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";

                }
            }
            else if (Commodity == "63" || Commodity == "64" || Commodity == "33" || Commodity == "92" || Commodity == "27" && CropYear == "2021-22")
            {
                if (User_Type == "B")
                {
                    //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2020-21' and WHR.DepositorID='10535' and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2020 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2021-22' and WHR.DepositorID='10535' and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2021 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";

                }
                else if (User_Type == "G")
                {
                    GetBranchId = Get_BranchId(BG_Id);
                    //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2020-21' and WHR.DepositorID='10535' and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2020 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2021-22' and WHR.DepositorID='10535' and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2021 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";

                }
            }
            else if (Commodity == "22" && CropYear == "2021-22")
            {
                if (User_Type == "B")
                {
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2021-22' and WHR.DepositorID='129' and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2021 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";


                }
                else if (User_Type == "G")
                {
                    GetBranchId = Get_BranchId(BG_Id);
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2021-22' and WHR.DepositorID='129' and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2021 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                }
            }
            else if (Commodity == "22" && CropYear == "2020-21")
            {
                if (User_Type == "B")
                {
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2020-21' and WHR.DepositorID='129' and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2020 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";


                }
                else if (User_Type == "G")
                {
                    GetBranchId = Get_BranchId(BG_Id);
                    //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2020-21' and WHR.DepositorID='129' and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2020 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2020-21' and WHR.DepositorID='129' and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2020 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";

                }
            }
            else if ((Commodity == "13" || Commodity == "8" || Commodity == "11") && CropYear == "2020-21")
            {
                if (User_Type == "B")
                {
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2020-21' and WHR.DepositorID in ('129','10535','4679') and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Kharif2020 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";


                }
                else if (User_Type == "G")
                {
                    GetBranchId = Get_BranchId(BG_Id);
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2020-21' and WHR.DepositorID in ('129','10535','4679') and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Kharif2020 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                }
            }
            else if (Commodity == "63" || Commodity == "64" || Commodity == "33" && CropYear == "2020-21")
            {
                if (User_Type == "B")
                {
                    //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2020-21' and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2020 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";
                    //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2020-21' and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2020 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2020-21' and WHR.DepositorID='10535' and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2020 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";



                }
                else if (User_Type == "G")
                {
                    GetBranchId = Get_BranchId(BG_Id);
                    //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2020-21' and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2020 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2020-21' and WHR.DepositorID='10535' and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_CMS2020 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";

                }
            }
            else if (Commodity == "22" && CropYear == "2019-20")
            {
                if (User_Type == "B")
                {
                    //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2019-20' and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";


                }
                else if (User_Type == "G")
                {
                    GetBranchId = Get_BranchId(BG_Id);
                    //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2019-20' and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";

                }
            }
            else if (Commodity == "13" || Commodity == "8" || Commodity == "11")
            {
                if (User_Type == "B")
                {
                    //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2019-20' and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2019-20' and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Kharif2019 as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";


                }
                else if (User_Type == "G")
                {
                    GetBranchId = Get_BranchId(BG_Id);
                    //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2019-20' and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2019-20' and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Kharif2019 as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";


                }
            }
            else
            {
                if (User_Type == "B")
                {
                    //query = "  SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Details as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity) order by [WHR_Issue_Date] desc";
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=@BG_Id and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id=@Commodity and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Details as DSC where DSC.BranchID=@BG_Id and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";


                }
                else if (User_Type == "G")
                {
                    GetBranchId = Get_BranchId(BG_Id);
                    //query = " SELECT [Depositor_WHR_Id],[Commodity_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,Depositor_Name,GD.Godown_Name from tbl_storage_Depositor_WHR_Relation as WHR inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=WHR.GodownID where WHR.GodownID='2309001' and WHR.CropYear='2018-19' and WHR.Arrival_Source='01' and Gid is not null  order by [WHR_Issue_Date] desc";
                    //query = " SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id=@Commodity and Gid is not null  order by [WHR_Issue_Date] desc";
                    query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=@BG_Id and WHR.CropYear='2024-25' and WHR.Arrival_Source='01' and Commodity_Id=@Commodity and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Details as DSC where DSC.BranchID=@GetBranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";

                }
            }
            //}
            SqlCommand cmd = new SqlCommand(query, con);
            if (query.IndexOf("@BG_Id", StringComparison.Ordinal) >= 0)
            {
                cmd.Parameters.AddWithValue("@BG_Id", BG_Id);
            }
            if (query.IndexOf("@Commodity", StringComparison.Ordinal) >= 0)
            {
                cmd.Parameters.AddWithValue("@Commodity", Commodity);
            }
            if (query.IndexOf("@GetBranchId", StringComparison.Ordinal) >= 0)
            {
                cmd.Parameters.AddWithValue("@GetBranchId", GetBranchId);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                //ds.WriteXml(DestPdfFileName);
            }
            else
            {

            }
            return ds;
            //}
        }

        catch (Exception)
        {

            throw;

        }
    }
    public string Get_BranchId(string GodownId)
    {
        string Godown_ID = GodownId;
        string Branch_Id = "";
        string query = "";
        try
        {
            query = "select BranchID from tbl_MetaData_GODOWN_2018 where Godown_ID=@GodownId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@GodownId", Godown_ID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Branch_Id = ds.Tables[0].Rows[0]["BranchID"].ToString();
            }
            else
            {
                Branch_Id = "";
            }

            return Branch_Id;
        }

        catch (Exception)
        {

            throw;

        }
    }

}
