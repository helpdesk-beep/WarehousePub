using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class StatePages_GodownCheck : System.Web.UI.Page
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
            //getdistrict();
            //filldepositer();
            //fillScheduleInsp_Grid();
        }
    }

    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("Godown_Check", con))
            //using (SqlCommand cmd = new SqlCommand("Godown_Check_03Jan2024", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Godown_ID", txtGodownID.Text);
                //cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue);
                //cmd.Parameters.AddWithValue("@BranchPwd",txtBranchPwd.Text.ToString());

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    //using (DataTable dt = new DataTable())
                    //{
                    //sda.Fill(dt);
                    sda.Fill(ds);
                    DataTable tableA = ds.Tables[0];
                    DataTable tableB = ds.Tables[1];
                    if (tableA.Rows.Count > 0)
                        {
                            Depositor_Gridview.DataSource = tableA;
                            Depositor_Gridview.DataBind();
                            GV_Capacity.DataSource = tableB;
                            GV_Capacity.DataBind();
                        }
                    else
                        {
                            Depositor_Gridview.DataSource = null;
                            Depositor_Gridview.DataBind();
                            GV_Capacity.DataSource = null;
                            GV_Capacity.DataBind();
                        }
                
                }
            }
        }
    }

    //public void GetBranch(string distID)
    //{

    //    string qry = "";
    //    qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId ='" + distID + "' order by DepotName";
    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlBranch.DataSource = ds.Tables[0];
    //        ddlBranch.DataTextField = "DepotName";
    //        ddlBranch.DataValueField = "BranchId";
    //        ddlBranch.DataBind();
    //        ddlBranch.Items.Insert(0, "--Select--");
    //    }
    //}

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

    //protected string getDate_MDY(string inDate)
    //{
    //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
    //    DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
    //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
    //    return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    //}
    //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetBranch(DropDownList1.SelectedValue.ToString());
    //}
    //public void getdistrict()
    //{
    //    string qry = "select District_Name,District_Id from tbl_MetaData_DISTRICT order by District_Name ";
    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        DropDownList1.DataSource = ds.Tables[0];
    //        DropDownList1.DataTextField = "District_Name";
    //        DropDownList1.DataValueField = "District_Id";
    //        DropDownList1.DataBind();
    //        DropDownList1.Items.Insert(0, "--Select--");
    //    }
    //}

    //protected void txttwhrno_TextChanged(object sender, EventArgs e)
    //{
    //    fillScheduleInsp_Grid();
    //}
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    fillScheduleInsp_Grid();
    //}

    //protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillScheduleInsp_Grid();
    //}




    protected void btnCheck_Click(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
        fillgrdBeneficiarydetails();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtRegistrationID.Text != "" && txtRegistrationID.Text.Length > 5 && ddlSeason.SelectedValue != "--Select--")
        {
            fillGridGodownAgreementOfferDetails();
            fillGridGodownPaymentDetails();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter/Select Registration ID/Season .....')", true);
        }
    }

    protected void fillGridGodownAgreementOfferDetails()
    {
        //string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        try
        {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("GetGodownCapacityDetails", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Season", ddlSeason.SelectedValue);
                cmd.Parameters.AddWithValue("@RegistrationId", txtRegistrationID.Text);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    //DataTable dt = new DataTable();
                    //DataTable dt1 = new DataTable();
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        //sda.Fill(dt1);
                        if (dt.Rows.Count > 0)// && dt1.Rows.Count>0)
                        {
                            grdGodownOtherDetails.DataSource = dt;
                            grdGodownOtherDetails.DataBind();
                            //grdPaymentRegDtls.DataSource = dt1;
                            //grdPaymentRegDtls.DataBind();
                        }
                        //else if (dt.Rows.Count > 0 && dt1.Rows.Count == 0)
                        //{
                        //    grdGodownOtherDetails.DataSource = dt;
                        //    grdGodownOtherDetails.DataBind();
                        //    grdPaymentRegDtls.DataSource = null;
                        //    grdPaymentRegDtls.DataBind();
                        //}
                        //else if (dt.Rows.Count == 0 && dt1.Rows.Count > 0)
                        //{
                        //    grdGodownOtherDetails.DataSource = null;
                        //    grdGodownOtherDetails.DataBind();
                        //    grdPaymentRegDtls.DataSource = dt1;
                        //    grdPaymentRegDtls.DataBind();
                        //}
                        else
                        {
                            grdGodownOtherDetails.DataSource = null;
                            grdGodownOtherDetails.DataBind();
                            //grdPaymentRegDtls.DataSource = null;
                            //grdPaymentRegDtls.DataBind();

                        }
                    }
                }
            }
        }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Wrong ')", true);
        }
    }

    protected void ddlSeason_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void fillGridGodownPaymentDetails()
    {
        //string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("GetGodownPaymentDetails", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Season", ddlSeason.SelectedValue);
                    cmd.Parameters.AddWithValue("@RegistrationId", txtRegistrationID.Text);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                grdPaymentRegDtls.DataSource = dt;
                                grdPaymentRegDtls.DataBind();
                            }
                            else
                            {
                                grdPaymentRegDtls.DataSource = null;
                                grdPaymentRegDtls.DataBind();
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Wrong ')", true);
        }
    }

    protected void CheckGodownCurrentCapacity()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("usp_GetCurrentGodownCapacity_ForOffer2024_25", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegistrationID", txtWRegID.Text);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GV_GdnCapacity.DataSource = dt;
                            GV_GdnCapacity.DataBind();
                        }
                        else
                        {
                            GV_GdnCapacity.DataSource = null;
                            GV_GdnCapacity.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void fillgrdBeneficiarydetails()
    {
        //string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Godown_Beneficiary_details", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Godown_ID", txtGodownID.Text);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                grdBeneficiarydetails.DataSource = dt;
                                grdBeneficiarydetails.DataBind();
                            }
                            else
                            {
                                grdBeneficiarydetails.DataSource = null;
                                grdBeneficiarydetails.DataBind();
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Wrong ')", true);
        }
    }
    protected void btnCheckWHCapacity_Click(object sender, EventArgs e)
    {
        CheckGodownCurrentCapacity();
    }

}