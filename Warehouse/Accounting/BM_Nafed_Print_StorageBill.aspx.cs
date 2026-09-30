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
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;

public partial class Accounting_BM_Nafed_Print_StorageBill : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["BranchId"] != null))
        {
            if (!IsPostBack)
            {
                GetDistrict();
                GetBranch();
                GetCommodity();
                // GetBillNo();
                pnllogin.Visible = false;
            }

        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    public void GetDistrict()
    {
        string Dist_id = Session["Depot_DistID"].ToString();
        string qry = "";
        qry = "select distinct District_Id,District_Name from tbl_MetaData_DISTRICT  where District_Id='" + Dist_id + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtDistrictName.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
            txtDistrictName.Attributes.Add("readonly", "readonly");
        }
        else
        {

        }
    }
    public void GetCommodity()
    {
        string Dist_id = Session["Depot_DistID"].ToString();
        string qry = "";
        qry = "select distinct Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in ('63', '64', '33', '52', '27', '92', '123', '31', '65') order by Commodity_Name asc";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            //ddlcommodity.Items.Insert(0, "--Select--");
            ddlcommodity.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {

        }
    }
    public void GetBranch()
    {
        //string Dist_id = Session["Depot_DistID"].ToString();
        //string Branch_Id = Session["BranchId"].ToString();
        string qry = "";
        qry = "select distinct DepotID,DepotName from tbl_MetaData_DEPOT where DistrictId='" + Dist_id + "' And DepotID='" + Branch_Id + "' ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtBranch.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
            txtBranch.Attributes.Add("readonly", "readonly");
        }
        else
        {

        }
    }
    public void fillgrid()
    {
        string Dist_id = Session["Depot_DistID"].ToString();
        string Branch_Id = Session["BranchId"].ToString();
        //SqlConnection con_WLC1 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_Detals", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District_Id", Dist_id);
            cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
            cmd.Parameters.AddWithValue("@Financial_Year", ddlFinancialyear.SelectedValue);
            cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
            cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                GrdBills.DataSource = dt;
                GrdBills.DataBind();
                grdbill.Visible = true;
                ddlFinancialyear.ClearSelection();
                ddlmonth.ClearSelection();
                ddlcommodity.ClearSelection();
                //lblOfficerList.Text = Convert.ToString(dt.Rows.Count);
                //ViewState["Region"] = dt;
            }
            else
            {
                GrdBills.DataSource = null;
                GrdBills.DataBind();
                grdbill.Visible = true;
                ddlFinancialyear.ClearSelection();
                ddlmonth.ClearSelection();
                ddlcommodity.ClearSelection();
                //lblOfficerList.Text = "0";
            }
        }
    }
    //public void GetBillNo()
    //{
    //    // string Dist_id = Session["Depot_DistID"].ToString();
    //    string Branch_Id = Session["BranchId"].ToString();
    //    string qry = "";
    //    qry = "select distinct DW.Bill_Number from tbl_Bills_Fifteen_Day_Wise_Dtl DW Join tbl_Storage_Bill_Details SB On DW.Bill_Number = SB.Bill_Number where Branch_Id='" + Branch_Id + "'";
    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlBillNo.DataSource = ds.Tables[0];
    //        ddlBillNo.DataTextField = "Bill_Number";
    //        ddlBillNo.DataValueField = "Bill_Number";
    //        ddlBillNo.DataBind();
    //        ddlBillNo.Items.Insert(0, "--Select--");
    //    }
    //    else
    //    {

    //    }
    //}
    protected void gvBill_SelectedIndexChanged(object sender, EventArgs e)
    {

        string Bill_No = GridView1.SelectedRow.Cells[0].Text;
        string Godown_ID = GridView1.SelectedRow.Cells[5].Text;
        string JVS_Bill = GridView1.SelectedRow.Cells[12].Text;
        Response.Write("<script>window.open ('BM_PrintGeneratedBill_DSC.aspx?BillNo=" + Bill_No + "&GodownID=" + Godown_ID + "&JVSBill=" + JVS_Bill + "','_blank');</script>");
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounting/BM_PrintFinal_StorageBill.aspx");
    }
    public void DSCSign()
    {
        // qry = "select DSC_Serial_No,DSC_Holder_Name,Client_Ip,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_Details_NAFED where Ref_Bill_No='" + ddlBillNo.SelectedValue.ToString() + "'  and DSC_User_Type='B'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Image1.Visible = true;
            lblBSerialNo.Text = "DSC Serial No : " + dt.Rows[0]["DSC_Serial_No"].ToString();
            lblBIP.Text = "Client IP : " + dt.Rows[0]["Client_Ip"].ToString();
            lblBHolderName.Text = "DSC Holder Name : " + dt.Rows[0]["DSC_Holder_Name"].ToString();
            lblBCreatedDate.Text = "DSC Sign Date : " + dt.Rows[0]["CreatedDate"].ToString();
        }
        else
        {
            Image1.Visible = false;
            lblBSerialNo.Text = "";
            lblBIP.Text = "";
            lblBHolderName.Text = "";
            lblBCreatedDate.Text = "";
        }
    }
    public void DSCSignR()
    {

        //qry = "select DSC_Serial_No,DSC_Holder_Name,Client_Ip,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_Details_NAFED where Ref_Bill_No='" + ddlBillNo.SelectedValue.ToString() + "'  and DSC_User_Type='R'";
        SqlCommand cmd1 = new SqlCommand(qry, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            Image2.Visible = true;
            lblICSerialNo.Text = "DSC Serial No : " + dt1.Rows[0]["DSC_Serial_No"].ToString();
            lblICIp.Text = "Client IP : " + dt1.Rows[0]["Client_Ip"].ToString();
            lblICHoldername.Text = "DSC Holder Name : " + dt1.Rows[0]["DSC_Holder_Name"].ToString();
            lblICCreatedDate.Text = "DSC Sign Date : " + dt1.Rows[0]["CreatedDate"].ToString();
        }
        else
        {
            Image2.Visible = false;
            lblICSerialNo.Text = "";
            lblICIp.Text = "";
            lblICHoldername.Text = "";
            lblICCreatedDate.Text = "";
        }
    }
    protected void gvBill_SelectedIndexChanged1(object sender, EventArgs e)
    {
        string Bill_No = GridView1.SelectedRow.Cells[0].Text;
        string Godown_ID = GridView1.SelectedRow.Cells[5].Text;
        string JVS_Bill = GridView1.SelectedRow.Cells[12].Text;
        Response.Write("<script>window.open ('BM_PrintGeneratedBill_DSC.aspx?BillNo=" + Bill_No + "&GodownID=" + Godown_ID + "&JVSBill=" + JVS_Bill + "','_blank');</script>");
        //ModalPopupExtender1.Show();
        //GetBillDetail();
    }
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        // ModalPopupExtender1.Show();
        // GetBillDetail();
    }
    private void GetFinalBillDetail()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_For_Print", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    // cmd.Parameters.AddWithValue("@Bill_Number", ddlBillNo.SelectedValue);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                gvBill.DataSource = dt;
                                gvBill.DataBind();
                                gvBill.FooterRow.Style.Add("text-align;font-weight: bold;text-align:center;", "Center");
                                gvBill.FooterRow.Cells[10].Text = "Total";
                                gvBill.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Charges")).ToString();

                                lblDistrict.Text = dt.Rows[0]["District_Name"].ToString();
                                lblBranch.Text = dt.Rows[0]["DepotName"].ToString();
                                Label12.Text = dt.Rows[0]["DepotName"].ToString();
                                //lblRate.Text = ds.Tables[0].Rows[0]["Commodity_Rate"].ToString();
                                lblBillingDate.Text = dt.Rows[0]["Billing_Date"].ToString();

                                lblSPAN.Text = dt.Rows[0]["PAN"].ToString();
                                lblSGST.Text = dt.Rows[0]["GST"].ToString();
                                //btnDetail.Text = "WAREHOUSES DETAIL(" + ds.Tables[0].Rows[0]["TotalGodown"].ToString() + ")";
                                Panel2.Visible = true;
                                grdbill.Visible = true;
                            }
                            else
                            {
                                gvBill.DataSource = null;
                                gvBill.DataBind();
                            }
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
        }
    }
    void ShowingGroupingDataInGridView(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
    {
        if (totalColumns == 0) return;
        int i, count = 1;
        ArrayList lst = new ArrayList();
        lst.Add(gridViewRows[0]);
        var ctrl = gridViewRows[0].Cells[startIndex];
        for (i = 1; i < gridViewRows.Count; i++)
        {
            TableCell nextTbCell = gridViewRows[i].Cells[startIndex];
            if (ctrl.Text == nextTbCell.Text)
            {
                count++;
                nextTbCell.Visible = false;
                lst.Add(gridViewRows[i]);
            }
            else
            {
                if (count > 1)
                {
                    ctrl.RowSpan = count;
                    ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
                }
                count = 1;
                lst.Clear();
                ctrl = gridViewRows[i].Cells[startIndex];
                lst.Add(gridViewRows[i]);
            }
        }
        if (count > 1)
        {
            ctrl.RowSpan = count;
            ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
        }
        count = 1;
        lst.Clear();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    protected void GrdBills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Session["Bill_Number"] = (row.RowIndex).ToString();
            Label Billnumber = (Label)row.FindControl("lblBill_Number");
            string url = "Print_BM_NafedGenerate_Bill.aspx?BN=" + Base64Encode(Billnumber.Text);
            string s = "window.open('" + url + "', 'popup_window', 'width=600,height=600,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }
}