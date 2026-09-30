using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
//using MPSCSC_WS;

public partial class Region_frm_Delete_Rent_Bill_NAFED : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connect_warehouse"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //MPSCSC_WS.MPSCSC_InstituitionStorageBillDetails MPSCSCDemo = new MPSCSC_WS.MPSCSC_InstituitionStorageBillDetails();

    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        fillDistrict();
                        //fillddlbillType();
                        //string strMsg = "यहाँ सुविधा कुछ दिनों के लिए सॉफ्टवेयर में कार्य होने कारण बंद कर दी गई हैं |||";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Welcome.aspx';", true);
                    }
                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
    }
    //private void fillddlbillType()
    //{
    //    try
    //    {

    //        string query = "";

    //        query = "select distinct Bill_Type from tbl_Storage_Bill_Details";

    //        cmd = new SqlCommand(query, con);
    //        da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlbillType.Items.Clear();
    //            ddlbillType.DataSource = ds.Tables[0];
    //            ddlbillType.DataTextField = "Bill_Type";
    //            ddlbillType.DataValueField = "Bill_Type";
    //            ddlbillType.DataBind();
    //            ddlbillType.Items.Insert(0, "---Select---");
    //            gv.DataSource = null;
    //            gv.DataBind();
    //        }
    //        else
    //        {
    //            ////
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //////
    //    }
    //}
    private void fillDistrict()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

                if (Session["Region_ID"].ToString() != null)
                {
                    region = Session["Region_ID"].ToString();

                }
            }
            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            }
            else
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
            }
            cmd = new SqlCommand(query, con);
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
                gv.DataSource = null;
                gv.DataBind();
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
            string query = "select depo.BranchId,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";
            cmd = new SqlCommand(query, con);
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
                gv.DataSource = null;
                gv.DataBind();
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
    public void FillGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            //using (SqlCommand cmd = new SqlCommand("Get_Bill_Details_For_Delete_Bill_From_RM", con))
            using (SqlCommand cmd = new SqlCommand("Get_NAFED_Storage_Bill_Details_For_Delete_Bill_From_RM", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", ddlDepotList.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@BillType", ddlbillType.SelectedValue.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    Session["ds_GridInfo"] = ds;
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        gv.DataSource = ds;
                        gv.DataBind();
                        lblRowCount.Text = "";
                        lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
                        Btn_Delete.Enabled = true;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Bill Data Found...')", true);
                        lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
                        gv.DataSource = null;
                        gv.DataBind();
                    }
                }
            }
        }
    }
    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        int count = 0;
        //Session["DistrictID"] = ddlDistrict.SelectedValue.ToString();
        //Session["BranchID"] = ddlDepotList.SelectedValue.ToString();
        try
        {
            if (gv.Rows.Count > 0)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                foreach (GridViewRow gr2 in gv.Rows)
                {
                    DataSet ds = (DataSet)Session["ds_GridInfo"];
                    CheckBox chk_Delete = new CheckBox();
                    string Bill_No = Convert.ToString(gv.DataKeys[gr2.RowIndex].Value);
                    string Bill_Type = gv.Rows[gr2.RowIndex].Cells[2].Text.ToString();
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                    HiddenField hdnFinBill_No = (HiddenField)(gr2.FindControl("hdnFinBill_No"));
                    if (string.IsNullOrEmpty(hdnFinBill_No.Value))
                    {
                        hdnFinBill_No.Value = "0";
                    }
                    if (chk_Delete.Checked == true)
                    {
                        if (hdnFinBill_No.Value == "0")
                        {
                            ds = (DataSet)Session["ds_GridInfo"];
                            foreach (DataRow drs in ds.Tables[0].Select("Bill_Number = '" + Bill_No + "'"))
                            {
                                {
                                    SqlCommand cmd = new SqlCommand("Delete_Rent_Bill_For_NAFED", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@BillNumber", Bill_No);
                                    cmd.Parameters.AddWithValue("@IPAddress", ip);
                                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                                    cmd.ExecuteNonQuery();
                                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                                    if (TheResult.StartsWith("SUCCESS"))
                                    {

                                    }
                                }
                            }
                                count++;
                            }
                        else
                            {
                                lbl_notfound.Visible = true;
                                lbl_notfound.Text = "इस स्टोरेज बिल " + Bill_No + " का फाइनल बिल बन चुका है, कृपया इसका पहले फाइनल बिल डिलीट करवाएं|";
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('इस स्टोरेज बिल '" + Bill_No + "' का फाइनल बिल बन चुका है,कृपया इसका पहले फाइनल बिल डिलीट करवाएं|')", true);
                                break;
                            }
                        }
                        //i = i + 1;
                    }
                }
            else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
                }
                if (count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully deleted')", true);
                    FillGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast One Record For Deleting')", true);
                }
            }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
        finally
        {
            con.Close();
        }
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void ddlbillType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
}