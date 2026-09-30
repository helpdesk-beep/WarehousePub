using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class StatePages_BillStatusTracking : System.Web.UI.Page
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
            fillAgencyType();
        }
    }

    private void fillAgencyType()
    {
        try
        {

            string query = "";
            query = "select distinct Depositor_ID,Depositor_Name from tbl_MetaData_DEPOSITOR where  Depositor_ID IN('129', '181', '10535', '4765') order by Depositor_Name ";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlAgencyType.Items.Clear();
                ddlAgencyType.DataSource = ds.Tables[0];
                ddlAgencyType.DataTextField = "Depositor_Name";
                ddlAgencyType.DataValueField = "Depositor_ID";
                ddlAgencyType.DataBind();
                ddlAgencyType.Items.Insert(0, "--Select--");
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

    private void fillBillType()
    {
        try
        {

            string query = "";
            query = "";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlBillType.Items.Clear();
                ddlBillType.DataSource = ds.Tables[0];
                ddlBillType.DataTextField = "";
                ddlBillType.DataValueField = "";
                ddlBillType.DataBind();
                ddlBillType.Items.Insert(0, "--Select--");
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

    protected void fillBillDetailsInGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            //DataSet myDataSet = new DataSet();
            //da.Fill(myDataSet);

            //myDataTable = new DataTable();
            //myDataTable = myDataSet.Tables[0];

            using (SqlCommand cmd = new SqlCommand("usp_BillDetailsStatus", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BillNumber", txtBillNumber.Text.ToString());
                cmd.Parameters.AddWithValue("@DepositorID", ddlAgencyType.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@BillType", ddlBillType.SelectedValue.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);

                    //if()
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable MainTable = ds.Tables[0];
                        DataView view1 = new DataView(ds.Tables[0]);
                        DataTable tableB = view1.ToTable("MainTable", true, "BillNumber", "FinalBillNumber", "FinancialYear", "DepositorName", "DistrictName", "BranchName", "GodownName-ID", "CropYear", "BillMonth", "CreatedDate", "BillAmount", "BillSubAmount", "BillType", "CommodityName", "BOApprovalStatus", "ROApprovalStatus");

                        DataView view2 = new DataView(ds.Tables[0]);
                        DataTable tableC = view2.ToTable("MainTable", true, "BillNumber", "BillAmount", "FinancialYear", "CropYear");

                        Depositor_Gridview.DataSource = tableB;
                        Depositor_Gridview.DataBind();
                        GV_BillAmountDetails.DataSource = tableC;
                        GV_BillAmountDetails.DataBind();
                    }
                    else
                    {
                        Depositor_Gridview.DataSource = null;
                        Depositor_Gridview.DataBind();
                        GV_BillAmountDetails.DataSource = null;
                        GV_BillAmountDetails.DataBind();
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        DataTable DSCTable = ds.Tables[1];

                        /*************************DSC Related Information**********************/
                        DataView view3 = new DataView(ds.Tables[1]);
                        DataTable DSCtable = view3.ToTable("DSCTable", true, "BillNumber", "GodownDSCHolderName", "GodownDSCApprovalDate", "BranchDSCHolderName", "BranchDSCApprovalDate", "RMDSCHolderName", "RMDSCApprovalDate");

                        GV_BillDSCDetails.DataSource = DSCTable;
                        GV_BillDSCDetails.DataBind();
                    }
                    else
                    {
                        //DataView view3 = new DataView(ds.Tables[1]);
                        //DataTable DSCtable = view3.ToTable("DSCTable", true, "BillNumber", "GodownDSCHolderName", "GodownDSCApprovalDate", "BranchDSCHolderName", "BranchDSCApprovalDate", "RMDSCHolderName", "RMDSCApprovalDate");
                        GV_BillDSCDetails.DataSource = null;
                        GV_BillDSCDetails.DataBind();
                    }

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        /*************************Katotra Related Information**********************/
                        DataView view4 = new DataView(ds.Tables[2]);
                        DataTable KatotraTable = view4.ToTable("KatotraTable", true, "GodownName", "GodownID", "BranchID", "ReferenceBillNumber", "BillNumber", "DeductionAmount", "BillAmount");

                        GV_BillKatotraDetails.DataSource = KatotraTable;
                        GV_BillKatotraDetails.DataBind();
                    }
                    else 
                    {
                        GV_BillKatotraDetails.DataSource = null;
                        GV_BillKatotraDetails.DataBind();
                    }
                    //DataTable MainTable = ds.Tables[0];
                    //DataTable DSCTable = ds.Tables[1];
                    //DataTable KatotraTable = ds.Tables[2];

                    //DataView view1 = new DataView(ds.Tables[0]);
                    //DataTable tableB = view1.ToTable("MainTable", true, "BillNumber", "FinalBillNumber", "FinancialYear", "DepositorName", "DistrictName", "BranchName", "GodownName-ID", "CropYear", "BillMonth", "CreatedDate", "BillAmount", "BillSubAmount", "BillType", "CommodityName", "BOApprovalStatus", "ROApprovalStatus");

                    //DataView view2 = new DataView(ds.Tables[0]);
                    //DataTable tableC = view2.ToTable("MainTable", true, "BillNumber", "BillAmount", "FinancialYear", "CropYear");

                    /*************************DSC Related Information**********************/
                    //DataView view3 = new DataView(ds.Tables[1]);
                    //DataTable DSCtable = view3.ToTable("DSCTable", true, "BillNumber", "GodownDSCHolderName", "GodownDSCApprovalDate", "BranchDSCHolderName", "BranchDSCApprovalDate", "RMDSCHolderName", "RMDSCApprovalDate");

                    ///*************************Katotra Related Information**********************/
                    //DataView view4 = new DataView(ds.Tables[2]);
                    //DataTable Katotratable = view4.ToTable("KatotraTable", true, "GodownName", "GodownID", "BranchID", "ReferenceBillNumber", "BillNumber", "DeductionAmount", "BillAmount");

                    ///****************************/
                    //DataTable tableB;
                    //tableB = MainTable.Copy();
                    //tableB.Columns.RemoveAt(4);
                    //tableB.Columns.RemoveAt(5);
                    //tableB.Columns.RemoveAt(6);
                    //tableB.Columns.RemoveAt(7);


                    ///***********************************/
                    //DataTable tableC;
                    //tableC = MainTable.Copy();
                    //tableC.Columns.RemoveAt(0);
                    //tableC.Columns.RemoveAt(1);
                    //tableC.Columns.RemoveAt(2);
                    //tableC.Columns.RemoveAt(3);



                    //if (MainTable.Rows.Count > 0)
                    //    {
                    //        Depositor_Gridview.DataSource = tableB;
                    //        Depositor_Gridview.DataBind();
                    //        GV_BillAmountDetails.DataSource = tableC;
                    //        GV_BillAmountDetails.DataBind();
                    //        GV_BillDSCDetails.DataSource = DSCTable;
                    //        GV_BillDSCDetails.DataBind();
                    //        GV_BillKatotraDetails.DataSource = KatotraTable;
                    //        GV_BillKatotraDetails.DataBind();
                    //}
                    //else
                    //    {
                    //        Depositor_Gridview.DataSource = null;
                    //        Depositor_Gridview.DataBind();
                    //        GV_BillAmountDetails.DataSource = null;
                    //        GV_BillAmountDetails.DataBind();
                    //        GV_BillDSCDetails.DataSource = null;
                    //        GV_BillDSCDetails.DataBind();
                    //        GV_BillDSCDetails.DataSource = null;
                    //        GV_BillDSCDetails.DataBind();
                    //        GV_BillKatotraDetails.DataSource = null;
                    //        GV_BillKatotraDetails.DataBind();
                    //}
                
                }
            }
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillBillDetailsInGrid();
    }

    protected void ddlBillType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlAgencyType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}