using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;

public partial class JointVentureScheme_JvsInspectionReportBranch22_23 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlTransaction sqltran;
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = null;
    string DistID = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        string SessBranch = Session["UserName"].ToString();
        string SessBranchID = Session["UserId"].ToString();
        if (SessBranch != "" && SessBranchID != "")
        {
            if (!IsPostBack)
            {
              //qry = " select (Select District_Name from tbl_MetaData_DISTRICT d where d.District_Id=g.DistrictID)as District,(Select DepotName from tbl_MetaData_DEPOT b where b.BranchId=g.BranchId)as Branch, (select Warehouse_Name from tbl_WarehouseRegistration w where w.Registration_Id=g.Registration_Id) as Warehouse_Name,g.Registration_Id,g.Godown_No,Convert(Varchar,g.Agree_Sign_Date,103) as Issue_Date, Convert(Varchar,g.Agree_End_Date,103) as Expiry_Date,G_OfferCapacity,INSP.Insp_Capacity,Agree_Capacity from tbl_Godown_Agreement as g left join (select  Ins.Godown_ID,convert(decimal(18,4),sum(Ins.G_OfferCapacity)) as G_OfferCapacity from tbl_Warehouse_Godown_Offer_2021  as Ins group by Godown_ID) as OFR on OFR.Godown_ID=g.GodownId left join (select I.godownid,sum(Vacant_Capacity) as Insp_Capacity from tbl_Godown_Inspection I where createdDate>'03/01/2021' group by I.godownid) as INSP on INSP.GodownId=g.GodownId    where g.createdDate>'03/01/2021' and g.Registration_Id in    (select REG.Registration_Id from tbl_WarehouseRegistration as REG where REG.branchid='" + Session["UserId"].ToString() + "')";
                FillGrid();
            }
        }
    }
  

   
   
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
    private void FillGrid()
    {
        SqlCommand cmdd = new SqlCommand("JvsInspectionReportBranch22_23", con);
        cmdd.CommandType = CommandType.StoredProcedure;
        cmdd.Parameters.AddWithValue("@Branch", Session["UserId"].ToString());
        //conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            AgreeGrid.DataSource = dt;
            AgreeGrid.DataBind();
            //btnUpdate.Visible = false;
        }
    }
}