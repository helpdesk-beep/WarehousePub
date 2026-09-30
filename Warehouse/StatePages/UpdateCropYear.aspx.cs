using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Text;

public partial class StatePages_UpdateCropYear : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid = "";
    SqlTransaction sqltran;
    string depottype = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            if (!IsPostBack)
            {
                string PopMsg = "";
                PopMsg = Request.QueryString["PopMsg"];
                if (Request.QueryString["PopMsg"] != null)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + PopMsg + "'); </script> ");
                }
                grid.Visible = false;
                fillDistrict();
                fillComodity();
                //fillIssuecenter();
                // fillGodown();
            }
        }
        else if (Session["RoleId"].ToString() == "10")
        {
            if (!IsPostBack)
            {
                string PopMsg = "";
                PopMsg = Request.QueryString["PopMsg"];
                if (Request.QueryString["PopMsg"] != null)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + PopMsg + "'); </script> ");
                }
                fillDistrict();
                fillComodity();
                // fillIssuecenter();
                //  fillGodown();
            }

        }
    }
    private void fillDistrict()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

                //if (Session["Region_ID"].ToString() != null)
                //{
                //    region = Session["Region_ID"].ToString();

                //}
            }

            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            }
            else
            {
                if (Session["RoleId"].ToString() == "2")
                {
                    query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
                }
                else
                {
                    query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where District_Id ='" + Session["Depot_DistID"].ToString() + "' order by District_Name asc";

                }
            }
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
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
                ddlDistrict.Items.Insert(0, "--Select--");


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

    private void fillIssuecenter()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

                //if (Session["Region_ID"].ToString() != null)
                //{
                //    region = Session["Region_ID"].ToString();

                //}
            }

            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "'";
            }
            else
            {
                query = "  SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "' and DepoTypeID='4'";

            }
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbranch.Items.Clear();
                ddlbranch.DataSource = ds.Tables[0];
                ddlbranch.DataTextField = "DepotName";
                ddlbranch.DataValueField = "BranchId";
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, "--Select--");
                depottype = ds.Tables[0].Rows[0]["DepoTypeID"].ToString().Trim();

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


    private void fillGodown()
    {
        try
        {

            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT Godown_ID,Godown_Name FROM [dbo].[tbl_MetaData_GODOWN] where BranchId='" + ddlbranch.SelectedValue + "'";
            }
            else
            {
                query = "SELECT Godown_ID,Godown_Name FROM [dbo].[tbl_MetaData_GODOWN] where BranchId='" + ddlbranch.SelectedValue + "'";

            }
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodown.Items.Clear();
                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
                ddlGodown.Items.Insert(0, "--Select--");
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
    private void fillComodity()
    {
        try
        {

            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            }
            else
            {
                query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";

            }
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlComodity.Items.Clear();
                ddlComodity.DataSource = ds.Tables[0];
                ddlComodity.DataTextField = "Commodity_Name";
                ddlComodity.DataValueField = "Commodity_Id";
                ddlComodity.DataBind();
                ddlComodity.Items.Insert(0, "--Select--");
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
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillIssuecenter();
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetGodown(ddlbranch.SelectedValue.ToString());
        fillGodown();
    }
    //void GetCropYear()
    //{
    //    try
    //    {
    //        qry = "select distinct CropYear from View_WHRcurrentstock where Commodity_Id='" + ddlComodity.SelectedValue.ToString() + "' and Godown_ID='" + ddlGodown.SelectedValue.ToString() + "'";
    //        cmd = new SqlCommand(qry, con);
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds == null)
    //        {
    //        }
    //        else
    //        {
    //            ddlCropYear.DataSource = ds.Tables[0];
    //            ddlCropYear.DataTextField = "CropYear";
    //            ddlCropYear.DataValueField = "CropYear";
    //            ddlCropYear.DataBind();
    //            ddlCropYear.Items.Insert(0, "--Select--");
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //////
    //    }
    //}

    void GetCropYear()
    {
        try
        {
            qry = "select distinct CASE WHEN CropYear='' THEN 'No Crop Year' else CropYear END AS CropYear from View_WHRcurrentstock " +
                  "where Commodity_Id='" + ddlComodity.SelectedValue.ToString() + "' " +
                  "and Godown_ID='" + ddlGodown.SelectedValue.ToString() + "'";

            cmd = new SqlCommand(qry, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            da.Fill(ds);

            ddlCropYear.Items.Clear();

            // Default Hide
            grid.Visible = false;

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlCropYear.DataSource = ds.Tables[0];
                ddlCropYear.DataTextField = "CropYear";
                ddlCropYear.DataValueField = "CropYear";
                ddlCropYear.DataBind();

                ddlCropYear.Items.Insert(0, "--Select--");
            }
            //else
            //{
            //    ddlCropYear.Items.Insert(0, "No Crop Year");

            //    // Grid Hide
            //    grid.Visible = false;

            //    // Optional Message
            //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "msg", "alert('Selected Godown me Crop Year available nahi hai');",true);
            //}
        }
        catch (Exception)
        {
            ddlCropYear.Items.Clear();
            ddlCropYear.Items.Insert(0, "No Crop Year");

            grid.Visible = false;
        }
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCropYear();
    }
    //public void FillGrid()
    //{
    //    DataTable dt = new DataTable();
    //    cmd = new SqlCommand("dbo.Get_storage_Depositor_WHR_Relation_For_Update_Crop_Year", con, sqltran);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
    //    cmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue);
    //    cmd.Parameters.AddWithValue("@CommodityId", ddlComodity.SelectedValue);
    //    cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedValue);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        grid.Visible = true;
    //        // CropYearForUpdate();
    //        godown_GridView.DataSource = dt;
    //        godown_GridView.DataBind();
    //        Session["dsGodown"] = dt;

    //    }
    //}

    public void FillGrid()
    {
        // Agar Crop Year select nahi hai
        //if (ddlCropYear.SelectedIndex <= 0 ||
        //    ddlCropYear.SelectedItem.Text == "No Crop Year")
        if (ddlCropYear.SelectedIndex <= 0)
        {
            grid.Visible = false;
            return;
        }

        DataTable dt = new DataTable();

        cmd = new SqlCommand("dbo.Get_storage_Depositor_WHR_Relation_For_Update_Crop_Year", con);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
        cmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue);
        cmd.Parameters.AddWithValue("@CommodityId", ddlComodity.SelectedValue);
        if (ddlCropYear.SelectedValue == "No Crop Year")
        {
            cmd.Parameters.AddWithValue("@CropYear", "");
        }
        else
        {
            cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedValue);
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            grid.Visible = true;

            godown_GridView.DataSource = dt;
            godown_GridView.DataBind();

            Session["dsGodown"] = dt;
        }
        else
        {
            grid.Visible = false;

            godown_GridView.DataSource = null;
            godown_GridView.DataBind();
        }
    }

    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    public void CropYearForUpdate()
    {
        try
        {
            qry = "select distinct CropYear from View_WHRcurrentstock where CropYear IN('2014-15','2015-16','2016-17','2017-18','2018-19','2019-20','2020-21','2021-22')";
            cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds == null)
            {
            }
            else
            {
                ddlChangeCropYear.DataSource = ds.Tables[0];
                ddlChangeCropYear.DataTextField = "CropYear";
                ddlChangeCropYear.DataValueField = "CropYear";
                ddlChangeCropYear.DataBind();
                ddlChangeCropYear.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void godown_GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnCropYear = (HiddenField)e.Row.FindControl("hdnCropYear");
            DropDownList ddlCropYear = (DropDownList)e.Row.FindControl("ddlCropYear");
        }
    }
    public string getData()
    {
        XmlWriterSettings wsettings = new XmlWriterSettings();
        wsettings.NewLineOnAttributes = true;
        wsettings.Indent = true;
        wsettings.OmitXmlDeclaration = true;
        wsettings.Encoding = Encoding.UTF8;
        wsettings.CloseOutput = false;
        StringBuilder str = new StringBuilder();
        XmlWriter xw = XmlWriter.Create(str, wsettings);
        xw.WriteStartDocument();
        xw.WriteStartElement("ROOT");
        foreach (GridViewRow row in godown_GridView.Rows)
        {
            CheckBox chkbtn = (CheckBox)(row.FindControl("chkbtn"));
            HiddenField hdnDepositor_WHR_Id = (HiddenField)(row.FindControl("hdnDepositor_WHR_Id"));
            if (chkbtn.Checked == true)
            {
                xw.WriteStartElement("ROWS");
                xw.WriteAttributeString("Depositor_WHR_Id", hdnDepositor_WHR_Id.Value);
                xw.WriteAttributeString("CropYear", ddlChangeCropYear.SelectedValue);
                xw.WriteAttributeString("IPAddress", Request.UserHostAddress);
                xw.WriteEndElement();
            }
        }
        xw.WriteEndElement();
        xw.WriteEndDocument();
        xw.Flush();
        xw.Close();
        Response.Write(str.ToString());
        return str.ToString();
    }
    public Boolean validate()
    {
        int chkstatus = 0;
        foreach (GridViewRow row in godown_GridView.Rows)
        {
            CheckBox chkbtn = (CheckBox)(row.FindControl("chkbtn"));
            HiddenField hdnGPID = (HiddenField)(row.FindControl("hdnGPID"));

            if (chkbtn.Checked == true)
            {
                chkstatus = chkstatus + 1;
            }
        }
        if (chkstatus > 0)
        {
            return true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया कम से कम एक चेकबॉक्स चेक करे ')", true);
            return false;
        }
    }
    protected void Save(object sender, EventArgs e)
    {
        String TheResult = "";
        if (con != null)
        {
            if (validate())
            {
                con.Open();
                cmd = new SqlCommand("[Update_Crop_Year]", con, sqltran);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@strXML", getData());
                cmd.Parameters.AddWithValue("@DeleteFlag", "S");
                // cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 100);
                //cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                //TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record updated successfully..')", true);
                    grid.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record NOT updated')", true);
                }
            }
        }
    }

    protected void godown_GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        godown_GridView.PageIndex = e.NewPageIndex;
        FillGrid();
    }
}