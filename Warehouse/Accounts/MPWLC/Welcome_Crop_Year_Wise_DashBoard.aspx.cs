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

public partial class StatePages_Welcome_DashBoard : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd;
    DataSet ds;
    SqlDataAdapter da;
    string query = "";
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCropYear();
            fillGet_Commodity();
            fillKharib202122grid();
            fillgrid();
            filldata();
            fillBillData();
            fillBillGodownOwnerData();
            fillInpOff_Grid();
            fillGodownDetails();
            //if (ddlCropYear.SelectedItem.ToString()=="All")
            //{
            //    allcropyear.Visible = true;
            //    fillgrid();
            //    filldata();
            //    fillBillData();
            //    fillBillGodownOwnerData();
            //    fillInpOff_Grid();
            //}
            //else
            //{
            //    allcropyear.Visible = false;
            //}

        }
    }

    public void fillInpOff_Grid()
    {
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();

            }
            SqlCommand cmd = new SqlCommand("Get_Total_Complite_Pending_Insp", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { conStr.Close(); }

            if (dt.Rows.Count > 0)
            {
                lbltiarm.Text = dt.Rows[0]["Totalallotedinspection"].ToString();
                lblinspdone.Text = dt.Rows[0]["totalcompliteinspection"].ToString();
                lblpendinginsp.Text = dt.Rows[0]["totalPendinginspection"].ToString();
                lblgi.Text = dt.Rows[0]["GI"].ToString();
                lblpvi.Text = dt.Rows[0]["PVI"].ToString();
                lblboth.Text = dt.Rows[0]["Both"].ToString();

                lblgic.Text = dt.Rows[0]["GIC"].ToString();
                lblpviC.Text = dt.Rows[0]["PVIC"].ToString();
                lblbothc.Text = dt.Rows[0]["BothC"].ToString();

                lblgip.Text = dt.Rows[0]["GIP"].ToString();
                lblpviP.Text = dt.Rows[0]["PVIP"].ToString();
                lblbothP.Text = dt.Rows[0]["BothP"].ToString();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
        finally
        { if (conStr.State == ConnectionState.Open) { conStr.Close(); } }
    }
    public void fillGet_Commodity()
    {
        string conStr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Commodity", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();

            ddlcommodity.DataSource = cmd.ExecuteReader();
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, new ListItem("--Select--", "0"));
            con.Close();
        }
    }
    void GetCropYear()
    {
        try
        {
            qry = "select 'All' as CropText ,'2%' as CropYear union(select distinct CropYear, CropYear + '%' as CropYear from tbl_storage_Depositor_WHR_Relation where CropYear like('2%')) order by CropYear";
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
                ddlCropYear.DataSource = ds.Tables[0];
                ddlCropYear.DataTextField = "CropText";
                ddlCropYear.DataValueField = "CropText";
                ddlCropYear.DataBind();
                //ddlCropYear.Items.Insert(0, "--Select Crop Year--");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void LinkButton33_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CommodityWiseStateReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
    }

    protected void LinkButton141_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_RegionWise_HiredTypeWise_StockRpt";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Available_Stock_Report", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedItem.ToString());
                if (ddlcommodity.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", '0');
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //lblRecBags.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Bags"].ToString()).ToString();
                                //lblIssueBags.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Issuebags"].ToString()).ToString();
                                //lblAvlBags.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["AvlBags"].ToString()).ToString();
                                lblRecQty.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Weight"].ToString()).ToString();
                                lblIssueQty.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Issueweigt"].ToString()).ToString();
                                lblAvlQty.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Avlweight"].ToString()).ToString();
                                if (lblRecQty.Text == "")
                                {
                                    lblRecQty.Text = "000";
                                }
                                if (lblIssueQty.Text == "")
                                {
                                    lblIssueQty.Text = "000";
                                }
                                if (lblAvlQty.Text == "")
                                {
                                    lblAvlQty.Text = "000";
                                }
                                //allcropyear.Visible = true;
                                //cropyearwise.Visible = false;
                            }
                        }
                        //if (ds.Tables[1].Rows.Count > 0)
                        //{
                        //    //lblcropRecBags.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["CropRecBags"].ToString()).ToString();
                        //    //lblcropIssueBags.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["CropIssuebags"].ToString()).ToString();
                        //    //lblcropAvlBags.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["CropAvlBags"].ToString()).ToString();
                        //    lblCropRecQty.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["CropRecWeight"].ToString()).ToString();
                        //    lblCropIssueQty.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["CropIssueweigt"].ToString()).ToString();
                        //    lblCropAvlQty.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["CropAvlweight"].ToString()).ToString();

                        //    if (lblCropRecQty.Text == "")
                        //    {
                        //        lblCropRecQty.Text = "000";
                        //    }
                        //    if (lblCropIssueQty.Text == "")
                        //    {
                        //        lblCropIssueQty.Text = "000";
                        //    }
                        //    if (lblCropAvlQty.Text == "")
                        //    {
                        //        lblCropAvlQty.Text = "000";
                        //    }
                        //    //allcropyear.Visible = false;
                        //    //cropyearwise.Visible = true;
                        //}

                        //if (ds.Tables[2].Rows.Count > 0)
                        //{
                        //    //lblcropRecBags.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["CropRecBags"].ToString()).ToString();
                        //    //lblcropIssueBags.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["CropIssuebags"].ToString()).ToString();
                        //    //lblcropAvlBags.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["CropAvlBags"].ToString()).ToString();
                        //    lblcmdRQ.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["CMDRecWeight"].ToString()).ToString();
                        //    lblcmdIQ.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["CMDIssueweigt"].ToString()).ToString();
                        //    lblcmdAQ.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["CMDAvlweight"].ToString()).ToString();
                        //    //allcropyear.Visible = false;
                        //    //cropyearwise.Visible = true;
                        //}
                        //if (ds.Tables[3].Rows.Count > 0)
                        //{
                        //    //lblcropRecBags.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["CropRecBags"].ToString()).ToString();
                        //    //lblcropIssueBags.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["CropIssuebags"].ToString()).ToString();
                        //    //lblcropAvlBags.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["CropAvlBags"].ToString()).ToString();
                        //    lblCropCMDRQ.Text = Convert.ToDecimal(ds.Tables[3].Rows[0]["CropCMDRecWeight"].ToString()).ToString();
                        //    lblCropCMDIQ.Text = Convert.ToDecimal(ds.Tables[3].Rows[0]["CropCMDIssueweigt"].ToString()).ToString();
                        //    lblCropCMDAQ.Text = Convert.ToDecimal(ds.Tables[3].Rows[0]["CropCMDAvlweight"].ToString()).ToString();
                        //    //allcropyear.Visible = false;
                        //    //cropyearwise.Visible = true;
                        //}
                        else
                        {

                        }
                    }
                }
            }
        }
    }

    protected void filldata()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_JVS_Reg_Choice_Filling_For_Dashboard", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                lblTR.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalRegistrasion"].ToString()).ToString();
                                lblTC.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalChoice"].ToString()).ToString();
                                lblRFC.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["RamaningForChoices"].ToString()).ToString();
                                lblFCA.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["ChoiceA"].ToString()).ToString();
                                lblFCB.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["ChoiceB"].ToString()).ToString();
                                //allcropyear.Visible = true;
                                //cropyearwise.Visible = false;
                            }
                        }
                       
                        else
                        {

                        }
                    }
                }
            }
        }
    }

    protected void fillBillData()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            //using (SqlCommand cmd = new SqlCommand("Get_Amount_From_MPSCSC_From_Aug", con))
            using (SqlCommand cmd = new SqlCommand("Get_Peyment_details_Recived_and_Pending_From_MPSCSC_From_Aug_For_Dashboard", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            lblATSBMPSCSC.Text = Convert.ToDecimal(dt.Rows[0]["SUBBillAmt"].ToString()).ToString();
                            lblTARFMPSCSC.Text = Convert.ToDecimal(dt.Rows[0]["TotalPayableAmount"].ToString()).ToString();
                            lblTPBAFMPSCSC.Text = Convert.ToDecimal(dt.Rows[0]["PendingatMPSCSC"].ToString()).ToString();

                            if (lblATSBMPSCSC.Text == "")
                            {
                                lblATSBMPSCSC.Text = "000";
                            }
                            if (lblTARFMPSCSC.Text == "")
                            {
                                lblTARFMPSCSC.Text = "000";
                            }
                            if (lblTPBAFMPSCSC.Text == "")
                            {
                                lblTPBAFMPSCSC.Text = "000";
                            }
                            //lblTAMPWLC.Text = Convert.ToDecimal(dt.Rows[0]["TotalPayableAmount"].ToString()).ToString();
                            //lblTACMPWLC.Text = Convert.ToDecimal(dt.Rows[0]["PFCredit_Amount"].ToString()).ToString();
                            //lblTPMPWLC.Text = Convert.ToDecimal(dt.Rows[0]["PendingatMPWLC"].ToString()).ToString();
                        }
                        else
                        {
                            
                        }
                    }
                }
            }
        }
    }

    protected void fillBillGodownOwnerData()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            //using (SqlCommand cmd = new SqlCommand("Get_Amount_From_MPSCSC_From_Aug", con))
            using (SqlCommand cmd = new SqlCommand("Region_Wise_Summary_of_Panding_Payment_For_Godown_For_Dashboard", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                            lblTAMPWLC.Text = Convert.ToDecimal(dt.Rows[0]["PayToGO"].ToString()).ToString();
                            lblTACMPWLC.Text = Convert.ToDecimal(dt.Rows[0]["PayToGOdownowner"].ToString()).ToString();
                            lblTPMPWLC.Text = Convert.ToDecimal(dt.Rows[0]["PandingatMpwlc"].ToString()).ToString();

                            if (lblTAMPWLC.Text == "")
                            {
                                lblTAMPWLC.Text = "000";
                            }
                            if (lblTACMPWLC.Text == "")
                            {
                                lblTACMPWLC.Text = "000";
                            }
                            if (lblTPMPWLC.Text == "")
                            {
                                lblTPMPWLC.Text = "000";
                            }
                        }
                        else
                        {

                        }
                    }
                }
            }
        }
    }
    protected void fillKharib202122grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Total_Procurement_KhariF_2021_22", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                lblPaddyAcceptanceQuantity.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["WheatTotalQty"].ToString()).ToString();
                                lblpaddyWHRQuantity.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["WheatAcceptQty"].ToString()).ToString();
                                lblpaddytotalQty.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["WheatAverg"].ToString()).ToString();
                            }
                        }
                        if (ds.Tables[1].Rows.Count > 0)
                        {

                            lblBajraAcceptanceQuantity.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["CMSTotalQty"].ToString()).ToString();
                            lblBajraWHRQuantity.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["CMSAcceptQty"].ToString()).ToString();
                            lblBajraTotalQty.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["CMSAverg"].ToString()).ToString();
                        }                       
                        //if (ds.Tables.Count > 0)
                        //{
                        //    if (ds.Tables[0].Rows.Count > 0)
                        //    {
                        //        lblPaddyAcceptanceQuantity.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["PaddyTotalQty"].ToString()).ToString();
                        //        lblpaddyWHRQuantity.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["PaddyAcceptQty"].ToString()).ToString();
                        //        lblpaddytotalQty.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["PaddyAverg"].ToString()).ToString();
                        //    }
                        //}
                        //if (ds.Tables[1].Rows.Count > 0)
                        //{

                        //    lblBajraAcceptanceQuantity.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["BajraTotalQty"].ToString()).ToString();
                        //    lblBajraWHRQuantity.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["BajraAcceptQty"].ToString()).ToString();
                        //    lblBajraTotalQty.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["BajraAverg"].ToString()).ToString();
                        //}
                        //if (ds.Tables[2].Rows.Count > 0)
                        //{

                        //    lblJowarAcceptanceQuantity.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["JowarTotalQty"].ToString()).ToString();
                        //    lblJowarWHRQuantity.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["JowarAcceptQty"].ToString()).ToString();
                        //    lblJowarTotalQty.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["JowarAverg"].ToString()).ToString();
                        //}
                        else
                        {

                        }
                    }
                }
            }
        }
    }
    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedItem.ToString() == "All")
        {
            allcropyear.Visible = true;
            cropyearwise.Visible = false;
            //Commoditywise.Visible = false;
            //BothCropCommodity.Visible = false;
            fillgrid();
        }  
        //else if (ddlCropYear.SelectedItem.ToString() != "All" && ddlcommodity.SelectedValue != "--Select--")
        //{
        //    allcropyear.Visible = false;
        //    cropyearwise.Visible = false;
        //    Commoditywise.Visible = false;
        //    BothCropCommodity.Visible = true;
        //    fillgrid();
        //}
        else
        {
            allcropyear.Visible = false;
            cropyearwise.Visible = true;
            //Commoditywise.Visible = false;
            //BothCropCommodity.Visible = false;
            fillgrid();
        }
    }

    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcommodity.SelectedValue == "--Select--")
        {           
            allcropyear.Visible = true;
            cropyearwise.Visible = false;
            //Commoditywise.Visible = false;
            //BothCropCommodity.Visible = false;
            fillgrid();
        }
        //else if (ddlCropYear.SelectedItem.ToString() != "All" && ddlcommodity.SelectedValue != "--Select--")
        //{
        //    allcropyear.Visible = false;
        //    cropyearwise.Visible = false;
        //    Commoditywise.Visible = false;
        //    BothCropCommodity.Visible = true;
        //    fillgrid();
        //}
        else
        {
            allcropyear.Visible = true;
            cropyearwise.Visible = false;
            //Commoditywise.Visible = true;
            //BothCropCommodity.Visible = false;
            fillgrid();
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            string CS = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            using (SqlConnection constr = new SqlConnection(CS))
            {
                cmd = new SqlCommand("Godown_Wise_Stock_Positions", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Update...');if(alert){ window.location='Welcome_Crop_Year_Wise_DashBoard.aspx';}</script>");
                }
                else
                {
                    Page.RegisterStartupScript("UserMsg", "<script>alert('Error..');if(alert){ window.location='Welcome_Crop_Year_Wise_DashBoard.aspx';}</script>");
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }
    }

    protected void fillGodownDetails()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Total_Number_of_Godown_AndCapacity", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                lbltg.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["totalgodown"].ToString()).ToString();
                                lbltgcapacity.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalCapacity"].ToString()).ToString();
                            }
                        }
                        if (ds.Tables[1].Rows.Count > 0)
                        {

                            lblcoveredgdn.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["TotalCoverdGodown"].ToString()).ToString();
                            lblcoveredgdncapacity.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["TotalCapacityCoverd"].ToString()).ToString();
                        }
                        if (ds.Tables[2].Rows.Count > 0)
                        {

                            lblsilobaggdn.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["TotalSiloBag"].ToString()).ToString();
                            lblsilobaggdncapacity.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["TotalCapacityOwnedSiloBag"].ToString()).ToString();
                        }

                        if (ds.Tables[3].Rows.Count > 0)
                        {

                            lblcapgdn.Text = Convert.ToDecimal(ds.Tables[3].Rows[0]["TotalCAP"].ToString()).ToString();
                            lblcapgdncapacity.Text = Convert.ToDecimal(ds.Tables[3].Rows[0]["TotalCapacityOwnedCAP"].ToString()).ToString();
                        }
                        if (ds.Tables[4].Rows.Count > 0)
                        {

                            lblstealsilogdn.Text = Convert.ToDecimal(ds.Tables[4].Rows[0]["TotalSteelSilo"].ToString()).ToString();
                            lblstealsilogdncapacity.Text = Convert.ToDecimal(ds.Tables[4].Rows[0]["TotalCapacityOwnedCAPSteelSilo"].ToString()).ToString();
                        }
                        else
                        {

                        }
                    }
                }
            }
        }
    }
}