using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_BO_BranchDataInsert : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getdistrict();
            //filldepositer();
            //fillScheduleInsp_Grid();
        }
    }

    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Sp_BranchManager_Details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@District_Id", DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue);
                //cmd.Parameters.AddWithValue("@BranchPwd",txtBranchPwd.Text.ToString());

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Depositor_Gridview.DataSource = dt;
                            Depositor_Gridview.DataBind();
                        }
                        else
                        {
                            Depositor_Gridview.DataSource = null;
                            Depositor_Gridview.DataBind();
                        }
                    }
                }
            }
        }
    }

    public void GetBranch(string distID)
    {

        string qry = "";
        qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId ='" + distID + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "--Select--");
        }
    }

    //public void filldepositer()
    //{
    //    string query2 = "";
    //    query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679')";
    //    SqlCommand cmd2 = new SqlCommand(query2, con);
    //    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
    //    DataSet ds2 = new DataSet();
    //    da2.Fill(ds2);
    //    if (ds2.Tables[0].Rows.Count > 0)
    //    {
    //        ddlDepositor.DataSource = ds2;
    //        ddlDepositor.DataTextField = "Depositor_Name";
    //        ddlDepositor.DataValueField = "Depositor_ID";
    //        ddlDepositor.DataBind();
    //        ddlDepositor.Items.Insert(0, "--Select--");
    //        //ddlDepositor.SelectedValue=
    //    }
    //}
    //protected void Display(object sender, EventArgs e)
    //{
    //    int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
    //    GridViewRow row = Depositor_Gridview.Rows[rowIndex];

    //    lblgodownname.Text = (row.FindControl("lblGodown_Name") as Label).Text;
    //    txtGdwnID.Text = (row.FindControl("hdngodownid") as HiddenField).Value;
    //    txtwhrno.Text = (row.FindControl("lblWhr_No") as Label).Text;
    //    ddlDepositor.SelectedValue = (row.FindControl("hdndepositerid") as HiddenField).Value;
    //    divNewInsp.Visible = true;
    //    ModalPopupExtender1.Show();
    //}

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch(DropDownList1.SelectedValue.ToString());
    }
    public void getdistrict()
    {
        string qry = "select District_Name,District_Id from tbl_MetaData_DISTRICT order by District_Name ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DropDownList1.DataSource = ds.Tables[0];
            DropDownList1.DataTextField = "District_Name";
            DropDownList1.DataValueField = "District_Id";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "--Select--");
        }
    }

    //protected void txttwhrno_TextChanged(object sender, EventArgs e)
    //{
    //    fillScheduleInsp_Grid();
    //}
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    fillScheduleInsp_Grid();
    //}

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }

}