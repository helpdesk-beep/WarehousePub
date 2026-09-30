using System;
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Diagnostics;
using System.Resources;

public partial class StatePages_UpdateBillPaymentinSCMPSCSC : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                //filldepositer();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_SC_Bill_Details_For_Deduction_Indore_Jabalpur", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
                cmd.Parameters.AddWithValue("@BillNo", txttwhrno.Text.ToString());
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
    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = Depositor_Gridview.Rows[rowIndex];

        lblgodownname.Text = (row.FindControl("lblGodown_Name") as Label).Text;
        txtGdwnID.Text = (row.FindControl("hdngodownid") as HiddenField).Value;
        txtBillno.Text = (row.FindControl("lblBill_Number") as Label).Text;
        txtCropyera.Text = (row.FindControl("lblCropYear") as Label).Text;
        txtFY.Text = (row.FindControl("lblFinancial_Year") as Label).Text;
        txtMonth.Text = (row.FindControl("lblMonth_Name") as Label).Text;
        txtCommodity.Text = (row.FindControl("lblCommodity_Name") as Label).Text;
        txtBillAmount.Text = (row.FindControl("lblNet_Amount") as Label).Text;
        //txtDA.Text = (row.FindControl("lblAvlBags") as Label).Text;
        //txtResion.Text = (row.FindControl("lblAvlQty") as Label).Text;

        //Session["RecBags"] = (row.FindControl("lblBags") as Label).Text;
        //Session["Recqty"] = (row.FindControl("lblWeight") as Label).Text;
        //Session["Delbags"] = (row.FindControl("lblDelBags") as Label).Text;
        //Session["Delqty"] = (row.FindControl("lblDelQty") as Label).Text;
        //Session["hdncommodityid"] = (row.FindControl("hdncommodoty") as HiddenField).Value;

        //ddlDepositor.SelectedValue = (row.FindControl("hdndepositerid") as HiddenField).Value;
        divNewInsp.Visible = true;
        ModalPopupExtender1.Show();
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void btnAddCompany_Click1(object sender, EventArgs e)
    {
        GridViewRow gvr = Depositor_Gridview.SelectedRow;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            if (txtGdwnID.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown')", true);
            }


            else
            {
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                //if (Session["RecBags"].ToString() != txtbags.Text || Session["RecQty"].ToString() != txtweight.Text)
                //{
                sqltrans = con.BeginTransaction();
                //  con.Open();
                cmd = new SqlCommand("Update_SC_Bill_Amount_MPSCSC", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = sqltrans;
                cmd.Parameters.AddWithValue("@Godown_ID", txtGdwnID.Text);
                cmd.Parameters.AddWithValue("@Bill_No", txtBillno.Text);
                cmd.Parameters.AddWithValue("@NetAmount", txtBillAmount.Text);
                cmd.Parameters.AddWithValue("@DductionAmount", txtDA.Text);
                cmd.Parameters.AddWithValue("@Resion", txtResion.Text.ToString());
                cmd.Parameters.AddWithValue("@CreatedtBy", ip.ToString());
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Bill Amount Update Successfully')", true);
                    sqltrans.Commit();
                    lblgodownname.Text = "";
                    txtGdwnID.Text = "";
                    Depositor_Gridview.DataSource = "";
                    Depositor_Gridview.DataBind();
                    txtResion.Text = "";
                    txtDA.Text = "";
                    fillScheduleInsp_Grid();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record not Updatet')", true);

                }

            }
        }
        catch (Exception ex)
        {
            sqltrans.Rollback();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error')", true);
        }
        finally
        {
            con.Close();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }
}
