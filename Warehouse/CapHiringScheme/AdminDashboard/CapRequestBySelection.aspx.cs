using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Xml;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;

public partial class CapHiringScheme_AdminDashboard_CapRequest : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null && Session["UserName"] == null && Session["Scope"] == null)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("../AdminLogin.aspx");
        }

        if (!IsPostBack)
        {
            GetRegion();
            string scope = Session["Scope"].ToString();
            if (scope == "R")
            {
                string regionid = Session["UserId"].ToString();
                ddlRegion.SelectedValue = regionid;
                GetDistrictByRegion(ddlRegion.SelectedValue);
                ddlRegion.Enabled = false;
            }


            else if (scope == "B")
            {
                divRegion.Visible = false;
                divDistrict.Visible = false;

                string dstid = Session["DistID"].ToString();
                GetBranchByDistrict(dstid);

                string branchid = Session["UserId"].ToString();
                ddlBranch.SelectedValue = branchid;
                ddlBranch.Enabled = false;

                
            }

            fillGrid();
        }
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDistrictByRegion(ddlRegion.SelectedValue.ToString());
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranchByDistrict(ddlDistrict.SelectedValue.ToString());
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
   
    //fill grid
    public void fillGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetCapRequestList(ddlRegion.SelectedValue, ddlDistrict.SelectedValue, ddlBranch.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                btnExportExcel.Visible = true;
            }

            else
            {
                btnExportExcel.Visible = false;
            }

            gvCapReqList.DataSource = dt;
            gvCapReqList.DataBind();

        }
        catch (Exception ex)
        {

        }
    }




    public DataTable GetCapRequestList(string regionid, string districtid, string branchid)
    {
        try
        {
            dt = new DataTable();

            string qr = "select bcap.BookId, bcap.Registration_ID, dst.Regionnm, dst.District_Name, br.BranchName, pre.Miller, bcap.TotalCapacity, bcap.TotalAmount, bcap.RequestDate from dbo.MillerCapBookMaster as bcap inner join PreRegistration as pre on pre.Registration_ID=bcap.Registration_ID inner join dbo.MetaDataBranchWithIssueCenter as br on br.BranchID=bcap.BranchId inner join dbo.tbl_MetaData_DISTRICT as dst on dst.District_Id=bcap.District_Id  where (dst.Region_ID=@regionid or @regionid=0) and (dst.District_Id=@districtid or @districtid=0) and (br.BranchID=@branchid or @branchid=0);";


            string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = qr;

                    cmd.Parameters.AddWithValue("@regionid", regionid);

                    cmd.Parameters.AddWithValue("@districtid", districtid);

                    cmd.Parameters.AddWithValue("@branchid", branchid);

                    cmd.Connection = con;
                    con.Open();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    con.Close();
                }
            }
        }

        catch (Exception ex) { }
        finally { }
        return dt;
        
    }

    // Fill Region
    public void GetRegion()
    {

        string qr = "select Region_Id,region from dbo.tbl_MetaData_Region";

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);

            if (dt.Rows.Count > 0)
            {
                ddlRegion.Items.Clear();
                ddlRegion.DataSource = dt;
                ddlRegion.DataTextField = "region";
                ddlRegion.DataValueField = "Region_Id";
                ddlRegion.DataBind();
                ddlRegion.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        catch (Exception ex) { }
        finally { }
    }

    // Fill District
    public void GetDistrictByRegion(string regionid)
    {

        string qr = "select District_Id,District_Name from dbo.tbl_MetaData_DISTRICT where Region_ID='" + regionid + "'";

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);

            if (dt.Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = dt;
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        catch (Exception ex) { }
        finally { }
    }

    // Fill Branch
    public DataTable GetBranchByDistrict(string dstid)
    {
        string qr = "SELECT mbi.BranchID,mbi.BranchName FROM MetaDataBranchWithIssueCenter as mbi WHERE mbi.BranchID in (select [Branch_ID] from tbl_Branch_Block_Mapping where [District_ID]='" + dstid.Trim() + "')";

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);

            if (dt.Rows.Count > 0)
            {
                ddlBranch.Items.Clear();
                ddlBranch.DataSource = dt;
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchID";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "CapRequestList_" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        gvCapReqList.Attributes["style"] = "border-collapse:separate";
        gvCapReqList.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
}