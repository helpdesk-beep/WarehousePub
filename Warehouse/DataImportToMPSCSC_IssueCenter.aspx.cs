using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Web.Security;
using WS_MetaDataBranchIssueCenter;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public partial class DataImportToMPSCSC_IssueCenter : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //public SqlConnection conMPSCSC = new SqlConnection(ConfigurationManager.ConnectionStrings["MPSCSC"].ToString());

    SqlTransaction sqltrans;
    SqlCommand cmd = null;
    string TheResult = "";
    string TableName = "";
    WS_MetaDataBranchIssueCenter.MPSCSC_DepotBranchDetails WSForData = new WS_MetaDataBranchIssueCenter.MPSCSC_DepotBranchDetails();
    //WSForMPSCSCData_Local.WS_DataFromMPSCSC WSForDataLocal = new WSForMPSCSCData_Local.WS_DataFromMPSCSC();


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    

    protected void btnImportCommodity_Click(object sender, EventArgs e)
    {
        //conMPSCSC.Open();
        con.Open();
        WSForData.AddMetaDetaBranchWithIssueCenter("23232301", "23232301", "2321",Convert.ToDateTime("2024-06-04 12:31:00"), "117.202.25.130", "117.202.25.130", Convert.ToDateTime("2024-06-04 12:31:00"),
        "TestBranch","TestBranch", "23232301", "I", "bm2021");
        con.Close();
        //conMPSCSC.Close();

    }
}
