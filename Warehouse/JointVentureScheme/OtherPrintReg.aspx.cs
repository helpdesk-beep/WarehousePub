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
using System.Data.SqlClient;

public partial class JointVentureScheme_OtherPrintReg : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    public string App_Id = "";
    public string ImgName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Session["App_ID"] = "233918016";
           // Session["Reg_No"] = "0402181464";
            string sess = Session["Reg_No"].ToString();
            if (sess != "")
            {
                App_Id = sess;
                if (!IsPostBack)
                {
                    GetRegisterationData();
                    //get_Region_Detail();
                    if (Session["Scope"].ToString() != "B")
                    {
                       // Response.Redirect("BranchHome.aspx");
                        link1.Visible = false;
                    }
                }
            }
            else
            {
                if (Session["Scope"].ToString() == "B")
                {
                    Response.Redirect("Logins.aspx");
                }
                else if (Session["Scope"].ToString() == "O")
                {
                    Response.Redirect("OtherGovLogin.aspx");
                }
                else
                {
                    Response.Redirect("OtherGovLogin.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("UserReg.aspx");
        }
    }
    public void GetRegisterationData()
    {
        qry = "SELECT WR.Warehouse_Name,WR.Warehouse_Address,WR.EmailID,WR.Mobile_No,WR.Registration_Id,WR.RegAmt,WR.RegCapacity,MDD.DepotName,TH.Tehsil_Name,WR.DistFNBranch,CONVERT(varchar(10),WR.Registration_Date,103) as Registration_Date,(select Agency_Name from tbl_storage_agency where SAID=WR.WarehouseHiredtype) as WarehouseHiredtype,MDD.DepotName,MDDIS.District_Name FROM tbl_WarehouseRegistration as WR inner join tbl_MetaData_DEPOT as MDD on WR.BranchId=MDD.BranchId inner join Tehsils as TH on WR.TehsilID=TH.TehsilCode inner join tbl_MetaData_DISTRICT as MDDIS on MDDIS.District_Id=WR.DistrictId where WR.Registration_Id='" + Session["Reg_No"].ToString() + "'";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            App_Id = dt.Rows[0]["Registration_Id"].ToString();
            lblRegId.Text = App_Id;
            lblRegEmail.Text = dt.Rows[0]["EmailID"].ToString();
            //lblDOB.Text = dt.Rows[0]["DOB"].ToString();
          //  lblRegMobile.Text = dt.Rows[0]["MobileNo"].ToString();
            lblAppType.Text = dt.Rows[0]["WarehouseHiredtype"].ToString();
          //  lblRegFee.Text = dt.Rows[0]["RegAmt"].ToString();
            lblWName.Text = dt.Rows[0]["Warehouse_Name"].ToString();
            lblWAddres.Text = dt.Rows[0]["Warehouse_Address"].ToString();
          //  lblWEmail.Text = dt.Rows[0]["EmailID"].ToString();
            lblWtehsil.Text = dt.Rows[0]["Tehsil_Name"].ToString();
            lblWMobile.Text = dt.Rows[0]["Mobile_No"].ToString();
            lblWDist.Text = dt.Rows[0]["District_Name"].ToString();
            lblWBranch.Text = dt.Rows[0]["DepotName"].ToString();
            lblWBranchDist.Text = dt.Rows[0]["DistFNBranch"].ToString();
          //  lblPanNo.Text = dt.Rows[0]["PAN_No"].ToString();
         //   lblAadharNo.Text = dt.Rows[0]["Aadhar_No"].ToString();
            lblRegCpt.Text = dt.Rows[0]["RegCapacity"].ToString();
            lblRegDate.Text = dt.Rows[0]["Registration_Date"].ToString();
            lblAppDate.Text = DateTime.Now.ToString();
        }
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
    protected void link1_Click(object sender, EventArgs e)
    {
        if (Session["Scope"].ToString() == "B")
        {
            Response.Redirect("BranchHome.aspx");
        }
        else if (Session["Scope"].ToString() == "O")
        {
            Response.Redirect("OtherLoginWelcome.aspx");
        }
        else
        {
            Response.Redirect("JointVentureSchemeApp.aspx");
        }
    }
}
