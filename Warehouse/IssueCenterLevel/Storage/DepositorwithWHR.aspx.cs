using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Data;
using System.Data.SqlClient;
using System.Text;
using System.Resources;
using System.Globalization;

public partial class IssueCenterLevel_DepositorSlip : System.Web.UI.Page
{
    DataTable Dt1 = new DataTable();
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    SqlTransaction sqltran;
    string crtdate;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["lang"].ToString() == "Hindi")
            {
                ResourceManager rm = ResourceManager.CreateFileBasedResourceManager("hindi", Server.MapPath("."), null);
                lblWHRDetailStackingInfo.Text = Resources.hindi.lblWHRDetailStackingInfo;
                lblInstruction.Text = Resources.hindi.lblInstruction;
                lblDepositorType.Text = Resources.hindi.lblDepositorType;
                //lblWHRNumber.Text = Resources.hindi.lblWHRNumber;
                lblDepositorName.Text = Resources.hindi.lblDepositorName;
                lblCommodity.Text = Resources.hindi.lblCommodity;
               // lblCategory.Text = Resources.hindi.lblCategory;
                lblCropYear.Text = Resources.hindi.lblCropYear;
                lblAvgMoisture.Text = Resources.hindi.lblAvgMoisture;
                lblStackingInfo.Text = Resources.hindi.lblStackingInform;
                lblGodownNo.Text = Resources.hindi.lblGodownNo;
                lblStackNo.Text = Resources.hindi.lblStackNo;
                lblNoofBags.Text = Resources.hindi.lblNoofBags;
                lblwt.Text = Resources.hindi.lblwt;
                btnadd.Text = Resources.hindi.btnaddnew;
                lblLotNo.Text = Resources.hindi.lblLotNo;
                lblMarketValue.Text = Resources.hindi.lblMarketValue;
                lblSorcePfArrival.Text = Resources.hindi.lblSorcePfArrival;
                lblWHRDate.Text = Resources.hindi.lblWHRDate;
                lblStackingInform.Text = Resources.hindi.lblStackingInform;
                lblScheme.Text = Resources.hindi.lblScheme;
                lblCurStackCap.Text = Resources.hindi.lblCurStackCap;
                lblAvailable.Text = Resources.hindi.lblAvailable;
                lblMaxCap.Text = Resources.hindi.lblMaxCap;
                //lblDistrict.Text = Resources.hindi.lblDistrict;
                //lblDepot.Text = Resources.hindi.lblDepot;
                //lblSourcesociety.Text = Resources.hindi.lblSourcesociety;
                lbl_From.Text = Resources.hindi.lbl_From;
                lbl_To.Text = Resources.hindi.lbl_To;
            }

            if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!IsPostBack)
                {
                    Session["RefreshButton"] = "No";
                    string PopMsg = "";
                    PopMsg = Request.QueryString["PopMsg"];
                    if (PopMsg != null)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + PopMsg + "'); </script> ");
                        //lblmsg.Text = PopMsg.ToString();
                    }
                    showmsg.Visible = false;
                    lbl_whrno.Text = "";
                    Session["dt1"] = null;
                    EmptyGrid();
                    ViewState["ckstat"] = "Empty";
                    fillCropYear();
                    fillCommodity();
                   // fiilCategory();
                    btnsave.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to save this record , please be sure that you have entered correct data?');");
                    Add_Depositor();
                    Printcurrentdate();
                    fillGodnList();
                    UxCommodity.SelectedValue = "22";
                    ddlpurpose.SelectedValue = "0";
                    Getrates();
                    if (ddldepositortype.Items.Count > 0)
                    {
                        for (int d = 0; d < ddldepositortype.Items.Count; d++)
                        {
                            if (ddldepositortype.Items[d].Text == "Institution")
                            {
                                ddldepositortype.Items[d].Selected = true;
                                ddldepositortype_SelectedIndexChanged(sender, e);
                            }
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("../../Logout.aspx");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    private void Getrates()
    {
        try
        {
            qry = "SELECT [Rate] FROM [Intergrated_MP_STORAGE].[dbo].[PurchaseRateMaster] where ComID='" + UxCommodity.SelectedValue.ToString() + "' and CropYear='" + ddlcropyear.SelectedValue.ToString() + "'";
            cmd = new SqlCommand(qry, con);
            IDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtmarketval.Text = ds.Tables[0].Rows[0]["Rate"].ToString();

                
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = ex.Message.ToString();
        }
    }
    private void Add_Depositor()
    {
        if (Session["Depot_DistID"] != null)
        {
            string query = "select Depositor_Type from tbl_MetaData_Depositor_Type order by Report_Seq_Id";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldepositortype.DataSource = ds;
                ddldepositortype.DataTextField = "Depositor_Type";
                ddldepositortype.DataValueField = "Depositor_Type";
                ddldepositortype.DataBind();
                ddldepositortype.Items.Insert(0, "--Select--");
            }
        }
    }

    private void fillGodnList()
    {
        if (Session["Depot_DistID"] != null)
        {
            string query = " SELECT Godown_ID,Godown_Name  FROM tbl_MetaData_GODOWN where DistrictId ='" + Session["Depot_DistID"].ToString() + "' and  BranchId  ='" + Session["BranchId"].ToString() + "' order by Godown_Name Asc";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodownlist.DataSource = ds.Tables[0];
                ddlgodownlist.DataTextField = "Godown_Name";
                ddlgodownlist.DataValueField = "Godown_ID";
                ddlgodownlist.DataBind();
                ddlgodownlist.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlgodownlist.Items.Insert(0, "--Select--");
            }
        }
    }

    //private void fiilCategory()
    //{
    //    if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
    //    {
    //        string query = "SELECT Category_Id, Category_Name FROM tbl_MetaData_STORAGE_CATEGORY";
    //        SqlCommand cmd = new SqlCommand(query, con);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            UxCategory.DataSource = ds.Tables[0];
    //            UxCategory.DataTextField = "Category_Name";
    //            UxCategory.DataValueField = "Category_Id";
    //            UxCategory.DataBind();
    //            UxCategory.Items.Insert(0, "--Select--");
    //            UxCategory.SelectedValue = "1";               
    //        }
    //        else
    //        {
               
    //        }
    //    }
    //}

    private void fillCommodity()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            string query = "SELECT [Commodity_Id], [Commodity_Name] FROM [tbl_MetaData_STORAGE_COMMODITY] order by Commodity_Name asc";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                UxCommodity.Items.Clear();
                UxCommodity.DataSource = ds.Tables[0];
                UxCommodity.DataTextField = "Commodity_Name";
                UxCommodity.DataValueField = "Commodity_Id";
                UxCommodity.DataBind();
                UxCommodity.Items.Insert(0, "--Select--");
            }
            else
            {
                UxCommodity.Items.Insert(0, "--Select--");
            }
        }
    }

    private void EmptyGrid()
    {
        Session["dt1"] = null;
        gdstackingdetails.DataSource = null;
        gdstackingdetails.DataBind();
    }

    private void Empty()
    {
        btnsave.Enabled = false;
        txtmoistcontent.Text = null;
        txtmarketval.Text = null;
        txtwhrno.Text = null;
        txtwhrdate.Text = null;
    }

    protected void UxRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnsave.Enabled = false;
            lblmsg.Text = "";
            EmptyGrid();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void UxMandi_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnsave.Enabled = false;
            lblmsg.Text = "";
            EmptyGrid();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void UxCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnsave.Enabled = false;
            lblmsg.Text = "";
            EmptyGrid();
            Getrates();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void UxCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnsave.Enabled = false;
            lblmsg.Text = "";
            EmptyGrid();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    private DataTable CreateTable()
    {
        DataTable _dt = new DataTable();//DataTable is created
        DataColumn Godownid = new DataColumn("Godownid", Type.GetType("System.String"));
        DataColumn Stackid = new DataColumn("Stackid", Type.GetType("System.String"));
        DataColumn GodownName = new DataColumn("GodownName", Type.GetType("System.String"));
        DataColumn StackName = new DataColumn("StackName", Type.GetType("System.String"));
        DataColumn Bags = new DataColumn("Bags", Type.GetType("System.Int32"));
        DataColumn Weight = new DataColumn("Weight", Type.GetType("System.Decimal"));
        _dt.Columns.Add(Godownid);//Column is added to the DataTable
        _dt.Columns.Add(Stackid);//Column is added to the DataTable
        _dt.Columns.Add(GodownName);//Column is added to the DataTable
        _dt.Columns.Add(StackName);//Column is added to the DataTable
        _dt.Columns.Add(Bags);//Column is added to the DataTable
        _dt.Columns.Add(Weight);//Column is added to the DataTable
        _dt.AcceptChanges();
        return _dt;
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        bool checkstatus = false;
        try
        {
            //check for Available stack....and do not Add to the Grid...Return ....
            if (Convert.ToDecimal(AvlStackCap.Text) < Convert.ToDecimal(txtwt.Text))
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Insuffient StackCapacity in the Selected Stack!'); </script> ");
                return;
            }

            if (ddlstacklist.Items.Count > 0)
            {
                if (txtnobags.Text == "")
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No. of Bags field cannot be empty'); </script> ");
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "No. of Bags field cannot be empty";
                }
                else if (txtwt.Text == "")
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Weight field cannot be empty'); </script> ");
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Weight field cannot be empty";
                }
                else
                {
                    btnsave.Enabled = true;
                    if (Session["dt1"] == null)
                    {
                        Dt1 = CreateTable();
                        Session["dt1"] = Dt1;
                    }
                    DataRow dr = ((DataTable)Session["dt1"]).NewRow();
                    ((DataTable)Session["dt1"]).AcceptChanges();
                    dr["Godownid"] = ddlgodownlist.SelectedValue;
                    dr["Stackid"] = ddlstacklist.SelectedValue;
                    dr["GodownName"] = ddlgodownlist.SelectedItem.Text;
                    dr["StackName"] = ddlstacklist.SelectedItem.Text;
                    dr["Bags"] = txtnobags.Text.Trim();
                    dr["Weight"] = txtwt.Text.Trim();
                    if (gdstackingdetails.Rows.Count > 0)
                    {
                        int i;
                        for (i = 0; i <= gdstackingdetails.Rows.Count - 1; i++)
                        {
                            string _stackid = Convert.ToString(gdstackingdetails.Rows[i].Cells[2].Text.ToString());
                            string _selectstackid = Convert.ToString(ddlstacklist.SelectedValue.ToString());
                            if (_stackid == _selectstackid)
                            {
                                checkstatus = true;
                            }
                        }
                        if (checkstatus == false)
                        {
                            ((DataTable)Session["dt1"]).Rows.Add(dr);
                            ((DataTable)Session["dt1"]).AcceptChanges();
                            gdstackingdetails.DataSource = (DataTable)Session["dt1"];
                            gdstackingdetails.DataBind();
                            txtnobags.Text = null;
                            txtwt.Text = null;
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Entry for this stack is already done'); </script> ");
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Entry for this stack is already done";
                        }
                    }
                    else
                    {
                        ((DataTable)Session["dt1"]).Rows.Add(dr);
                        ((DataTable)Session["dt1"]).AcceptChanges();
                        gdstackingdetails.DataSource = (DataTable)Session["dt1"];
                        gdstackingdetails.DataBind();
                        txtnobags.Text = null;
                        txtwt.Text = null;
                    }
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No stack under the selected Commodity ,Category and Godown Number'); </script> ");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occurred,Please Try Again..'); </script> ");
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Text = "Some error has occurred ";
        }

    }

    protected void gdstackingdetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (ViewState["ckstat"].ToString() != "Delete")
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occurred,Please Try Again..'); </script> ");
        }
    }

    protected void gdstackingdetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int i = e.RowIndex;
            if (gdstackingdetails.Rows.Count < 1)
            {
                ViewState["ckstat"] = "Delete";
            }
            ((DataTable)Session["dt1"]).Rows[i].Delete();
            ((DataTable)Session["dt1"]).AcceptChanges();

            gdstackingdetails.DataSource = (DataTable)Session["dt1"];
            gdstackingdetails.DataBind();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occurred,Please Try Again..'); </script> ");
        }
    }

    protected void gdstackingdetails_PreRender(object sender, EventArgs e)
    {
        int _count = 0;
        _count = gdstackingdetails.Rows.Count;
        if (_count > 0)
        {
            btnsave.Enabled = true;
        }
        else
        {
            btnsave.Enabled = false;
        }
    }

    protected void UxMandi_PreRender(object sender, EventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void ddlstacklist_SelectedIndexChanged(object sender, EventArgs e)
    {

        CurStackCap.Text = "0";
        MaxStackCap.Text = "0";
        AvlStackCap.Text = "0";
        try
        {
            if (ddlstacklist.Items.Count > 0)
            {
                //String query = "select tbl_MetaData_STACK.Stack_capacity,(select (isnull(a.wet,0) - isnull(b.wet2,0)) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details where Stack_ID = tbl_MetaData_STACK.Stack_ID) a,(select SUM(Bags_Weight) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = tbl_MetaData_STACK.Stack_ID AND GP.Status !='CANCEL') b) AS 'Current_Capacity' from tbl_MetaData_STACK  where tbl_MetaData_STACK.Stack_ID = '" + ddlstacklist.SelectedValue.ToString() + "'";
                string query = "select tbl_MetaData_STACK.Stack_capacity,(select (isnull(a.wet,0) - isnull(b.wet2,0)-isnull(l.loss,0)+isnull(g.gain,0)) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details where Stack_ID = tbl_MetaData_STACK.Stack_ID) a,(select SUM(Bags_Weight) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = tbl_MetaData_STACK.Stack_ID AND GP.Status !='CANCEL') b,(select isnull(SUM(Loss),0) as loss from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = tbl_MetaData_STACK.Stack_ID AND GP.Status !='CANCEL') as l,(select isnull(SUM(Gain),0) as gain from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = tbl_MetaData_STACK.Stack_ID AND GP.Status !='CANCEL') as g) AS 'Current_Capacity' from tbl_MetaData_STACK  where tbl_MetaData_STACK.Stack_ID = '" + ddlstacklist.SelectedValue.ToString() + "'"; 
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    double Stackcap = Convert.ToDouble(ds.Tables[0].Rows[0]["Stack_capacity"].ToString());
                    double cureentcap = Convert.ToDouble(ds.Tables[0].Rows[0]["Current_Capacity"].ToString());
                    CurStackCap.Text = cureentcap.ToString();
                    MaxStackCap.Text = Stackcap.ToString();
                    AvlStackCap.Text = String.Format("{0:0.00000}", (Convert.ToDouble(MaxStackCap.Text) - Convert.ToDouble(CurStackCap.Text)));
                    lblmsg.Text = "";
                }
            }
            else
            {
              //  Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "No stack under the selected Commodity ,Category and Godown Number" + "'); </script> ");
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "No stack under the selected Commodity ,Category and Godown Number";
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in ddlStackNo_SelectedIndexChanged has occured, try again'); </script> ");
        }

    }

    protected void ddlgodownlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           // string query = "SELECT Stack_ID,Stack_Name  FROM tbl_MetaData_STACK WHERE (Godown_ID ='" + ddlgodownlist.SelectedValue + "') AND (DepotId = '" + Session["Depot_DepotID"].ToString() + "') and  Category_Id  = '" + 1 + "' and Stack_Killed = 'N' order by Stack_Name";
            string query = "SELECT Stack_ID, Stack_Name FROM tbl_MetaData_STACK WHERE (Godown_ID = '" + ddlgodownlist.SelectedValue + "' and Commodity_Id = '" + UxCommodity.SelectedValue.ToString() + "' and Category_Id  = '" + 1 + "' and Stack_Killed = 'N' ) order by Stack_Name";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlstacklist.DataSource = ds;
                ddlstacklist.DataTextField = "Stack_Name";
                ddlstacklist.DataValueField = "Stack_ID";
                ddlstacklist.DataBind();
                lblmsg.Text = "";
            }
            else
            {
               // Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "No stack under the selected Commodity, Category and Godown Number" + "'); </script> ");
                lblmsg.Text = "No stack under the selected Commodity, Category and Godown Number";
                ddlstacklist.Items.Clear();
            }
            ddlstacklist_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
    }

    protected void Printcurrentdate()
    {
        try
        {
            string query = "SELECT  convert(varchar(10),getdate(),103) as 'Date1'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtwhrdate.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
                txtsvrdate.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["RefreshButton"] = "No";// Session["RefreshButton"];
    }

    protected void ddldepositortype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDepositor.Items.Clear();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand();
            DataSet ds1 = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Connection = con;
            //Added MPWLC & FCI in Depositor Master and showing here too regardless of Depot Selected  
            cmd = new SqlCommand("sp_getDepositor_Depo_wise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Depositor_Type", SqlDbType.VarChar, 50);//20
            cmd.Parameters["@Depositor_Type"].Value = ddldepositortype.SelectedValue.ToString().Trim();
            cmd.Parameters.Add("@depot_id", SqlDbType.VarChar, 20);
            cmd.Parameters["@depot_id"].Value = Session["BranchId"].ToString();
            int _index = cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();
            da.SelectCommand = cmd;
            da.Fill(ds1, "temp");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlDepositor.DataSource = ds1;
                ddlDepositor.DataTextField = "Depositor_Name";
                ddlDepositor.DataValueField = "Depositor_ID";
                ddlDepositor.DataBind();
            }
            else
            {
                fillDropDownList1();
                ddl_Sofarrival.SelectedValue = "06";
              //  lblSourcesociety.Visible = false;
                ddlpurpose.Visible = true;
                trArrivalS.Visible = false;
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No Depositor found'); </script> ");
            }
            con.Close();
            cmd.Dispose();
            ddlDepositor_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
        finally
        {
            
            con.Close();
        }
    }

    private void fillDropDownList1()
    {
        try
        {
          // string query = "SELECT * FROM MPSCSCSVR.[MPSCSC].dbo.Source_Arrival_Type  order by Source_ID";
           string query = "SELECT * FROM [MPSCSC].dbo.Source_Arrival_Type  order by Source_ID";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_Sofarrival.Items.Clear();
                ddl_Sofarrival.DataSource = ds.Tables[0];
                ddl_Sofarrival.DataTextField = "Source_Name";
                ddl_Sofarrival.DataValueField = "Source_ID";
                ddl_Sofarrival.DataBind();
                ddl_Sofarrival.Items.Insert(0, "--Select--");
                ddl_Sofarrival.SelectedValue = "01";
            }
            else
            {
                ddl_Sofarrival.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
    }

    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            fillDropDownList1();
            if (ddlDepositor.Items.Count > 0)
            {
                if (ddlDepositor.SelectedItem.Text.ToUpper() == "MPSCSC") //|| (ddlDepositor.SelectedItem.Text.ToUpper() != "FCI") || (ddlDepositor.SelectedItem.Text.ToUpper() != "F.C.I."))
                {

                    trArrivalS.Visible = true;
                    ddlpurpose.Items.Clear();
                    fillddlpurpose();
                    ddlpurpose.Visible = true;
                    lblScheme.Visible = true;
                }
                else if (ddlDepositor.SelectedItem.Text.ToUpper() == "FCI")
                {
                    ddl_Sofarrival.SelectedValue = "03";

                    trArrivalS.Visible = true;
                    ddlpurpose.Items.Clear();
                    ddlpurpose.Items.Add(new ListItem(" Non Scheme", "0"));
                    ddlpurpose.Visible = false;
                    lblScheme.Visible = false;
                }
                else if (ddlDepositor.SelectedItem.Text.ToUpper() == "F.C.I.")
                {
                    ddl_Sofarrival.SelectedValue = "03";

                    trArrivalS.Visible = true;
                    ddlpurpose.Items.Clear();
                    ddlpurpose.Items.Add(new ListItem(" Non Scheme", "0"));
                    ddlpurpose.Visible = false;
                    lblScheme.Visible = false;
                }
                else
                {
                    ddl_Sofarrival.SelectedValue = "06";
                 //   lblSourcesociety.Visible = false;
                    trArrivalS.Visible = false;
                    ddlpurpose.Items.Clear();
                    ddlpurpose.Items.Add(new ListItem(" Non Scheme", "0"));
                    ddlpurpose.Visible = false;
                    lblScheme.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
        finally
        {
            con.Close();
        }
    }

    private void fillddlpurpose()
    {
        try
        {
            string query = "SELECT [Scheme_Name], [Scheme_Id] FROM [tbl_MetaData_SCHEME] where Status='Y' order by Scheme_Name Asc";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlpurpose.Items.Clear();
                ddlpurpose.DataSource = ds.Tables[0];
                ddlpurpose.DataTextField = "Scheme_Name";
                ddlpurpose.DataValueField = "Scheme_Id";
                ddlpurpose.DataBind();
            }
        }
        catch (Exception)
        {
            /////////////
        }
    }

    protected void btnNewMC_Click(object sender, EventArgs e)
    {
        try
        {
            showmsg.Visible = false;
            lbl_whrno.Text = "";
            Session["dt1"] = null;
            Session["RefreshButton"] = "No";
            EmptyGrid();
            Empty();
            Response.Redirect("DepositorwithWHR.aspx");
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
    }

    protected void fillCropYear()
    {
        try
        {
            ddlcropyear.Items.Clear();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand();
            DataSet ds1 = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Connection = con;
            cmd = new SqlCommand("Yearwise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            int _index = cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();
            da.SelectCommand = cmd;
            da.Fill(ds1);
            ddlcropyear.DataSource = ds1;
            ddlcropyear.DataTextField = "Crop_Year";
            ddlcropyear.DataValueField = "Crop_Year";
            ddlcropyear.DataBind();
            ddlcropyear.SelectedValue = "2015-16";
            con.Close();
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
        finally
        {
            con.Close();
        }

    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
      
            //if (DateTime.ParseExact(txtwhrdate.Text.Trim().ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) > DateTime.ParseExact(txtsvrdate.Text.Trim().ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
              
            //{
            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please select proper date from calendar'); </script> ");
            //}
            //else
            //{
                            string Depositor_WHR_Id = "";
            if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqltran = con.BeginTransaction();

                string BranchId = Session["BranchID"].ToString();

                string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
                string WHR_No = txtwhrno.Text.Trim().ToString();
                string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
                String ecn = System.Environment.MachineName;
                string HostName = computer_name[0].ToString();
                int _stackbags = 0;
                decimal _stackwt = 0;
                int l;
                for (l = 0; l < gdstackingdetails.Rows.Count; l++)
                {
                    _stackbags = _stackbags + int.Parse(gdstackingdetails.Rows[l].Cells[5].Text.ToString());
                    _stackwt = _stackwt + decimal.Parse(gdstackingdetails.Rows[l].Cells[6].Text.ToString());
                }

                if (ddldepositortype.SelectedItem.Text == " --Select--" || ddlDepositor.Items.Count == 0)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Type/Name of Depositor not specified properly'); </script> ");
                }
                else if (ddl_Sofarrival.SelectedItem.Text == "--Select--" || ddl_Sofarrival.SelectedValue == "0")
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Source of Arrival not specified properly'); </script> ");
                }

                else if (Session["RefreshButton"].ToString() != ViewState["RefreshButton"].ToString())
                {
                    Response.Redirect("DepositorwithWHR.aspx?PopMsg=" + "Record Already Saved! Do not Refresh again!!" + "");
                }
                else
                {

                    
                        //string QueryMax = "select Max(Did) from tbl_storage_Depositor_WHR_Relation where District_Id='" + Session["Depot_DistID"].ToString() + "' and DepotId='" + Session["Depot_DepotID"].ToString() + "' ";
                        string QueryMax = "select isnull(Max(Did),0)+1 from tbl_storage_Depositor_WHR_Relation where District_Id='" + Session["Depot_DistID"].ToString() + "' and BranchId='" +BranchId + "' ";
                        cmd = new SqlCommand(QueryMax, con, sqltran); // check WhrId present in whr_status table
                        string str3 = cmd.ExecuteScalar().ToString();
                        if ((str3 == String.Empty) || str3 == "")
                        {
                            str3 = "0";
                        }

                    //**************************godown id chek
                        if (ddlgodownlist.SelectedValue == "0")
                        {

                        }
                        else
                        {
                            Session["gdwnid"] = ddlgodownlist.SelectedValue;
                        }
                        //************* Depositor_WHR_Id farmate id yy + Dpotid + unique no. ************
                        if (Convert.ToInt64(str3) != 0)
                        {
                            string Depotid = Session["Depot_DepotID"].ToString();
                            
                            //Depositor_WHR_Id = Convert.ToString(Convert.ToInt64(Depositor_WHR_Id) + 1);
                            Depositor_WHR_Id = "";
                            Depositor_WHR_Id = Session["gdwnid"] + System.DateTime.Now.Date.ToString("yy") + Convert.ToString(Convert.ToInt64(str3));
                            //Depositor_WHR_Id = Depositor_WHR_Id + System.DateTime.Now.Date.ToString("yy");
                            if (Depositor_WHR_Id != String.Empty || Depositor_WHR_Id != "")
                            {
                            Found:
                                string Queryc = "select count(ArrivalStock_Id) from tbl_Storage_Arrival_Stock where ArrivalStock_Id='" + Depositor_WHR_Id.ToString() + "'";
                                cmd = new SqlCommand(Queryc, con, sqltran); // check GatePass_No present in tbl_Storage_GatePass_Enrty table
                                string maxcount = cmd.ExecuteScalar().ToString();
                                if (Convert.ToInt16(maxcount) > 0)
                                {
                                    //Depositor_WHR_Id = Convert.ToString(Convert.ToInt64(Depositor_WHR_Id) + 1);
                                    Depositor_WHR_Id = "";
                                    str3 = Convert.ToString(Convert.ToInt64(str3) + 1);
                                    Depositor_WHR_Id = Session["gdwnid"] + System.DateTime.Now.Date.ToString("yy") + Convert.ToString(Convert.ToInt64(str3));
                                    //Depositor_WHR_Id = System.DateTime.Now.Date.ToString("yy") + Depositor_WHR_Id;
                                    goto Found;
                                }
                            }
                        }
                        else
                        {
                            string Depotid = Session["Depot_DepotID"].ToString();
                            //Depositor_WHR_Id = Convert.ToString(Convert.ToInt64(Depositor_WHR_Id) + 1);
                            Depositor_WHR_Id = "";
                            Depositor_WHR_Id = Session["gdwnid"] + System.DateTime.Now.Date.ToString("yy") + Convert.ToString(Convert.ToInt64(str3));
                            //Depositor_WHR_Id = Depotid + "100";
                            //Depositor_WHR_Id = System.DateTime.Now.Date.ToString("yy") + Depositor_WHR_Id;
                        }
                        txtwhrno.Text = Depositor_WHR_Id.ToString();

                        string Query = "select count(Whr_No) from tbl_storage_Depositor_WHR_Relation where Whr_No = '" + Depositor_WHR_Id + "' and District_Id='" + Session["Depot_DistID"].ToString() + "' and BranchId='" + Session["BranchId"].ToString() + "' ";
                        cmd = new SqlCommand(Query, con, sqltran); // check WhrId present in whr_status table
                        string str2 = cmd.ExecuteScalar().ToString();
                        if (Convert.ToInt64(str2) == 0) ////not found than insert else update
                        {
                            string datestring = txtwhrdate.Text;
                            string[] tempsplit = datestring.Split('/');
                            string joinstring = "/";
                            string newdate = tempsplit[2] + joinstring + tempsplit[1] + joinstring + tempsplit[0];
                            string dateinus = new DateTime(Convert.ToInt16(tempsplit[2]), Convert.ToInt16(tempsplit[1]), Convert.ToInt16(tempsplit[0])).ToString("d", new CultureInfo("en-US"));
                            //whr date
                            string moisturecontent = "0";
                            if (txtmoistcontent.Text == "")
                            {
                                moisturecontent = "0";
                            }
                            else
                            {
                                moisturecontent= txtmoistcontent.Text.Trim().ToString();
                            }
                           // cmd.Parameters.AddWithValue("@Lot_No", DBNull.Value);
                            string moisturecontent_to = "0";
                            if (txtmoistcontent_To.Text == "")
                            {
                                moisturecontent_to="0";
                            }
                            else
                            {
                                moisturecontent_to= txtmoistcontent_To.Text.Trim().ToString();
                            }
                            string sofarrival=null;
                            if (ddl_Sofarrival.SelectedValue == "0")
                            {
                               sofarrival=  null;//@ArrivalSrc
                            }
                            else
                            {
                                sofarrival=ddl_Sofarrival.SelectedValue.ToString();
                            }
                            string marketvalue = "0";
                            if (txtmarketval.Text == "")
                            {
                                marketvalue = "0";
                            }
                            else
                            {
                                marketvalue= txtmarketval.Text.Trim().ToString();
                            }
                            string sttry = "insert into tbl_storage_Depositor_WHR_Relation (Depositor_WHR_Id, state_id ,District_Id,DepotId,Commodity_Id,Category_Id ,Whr_No,Depositor_Name,Date_of_Deposit,TotalBags_Received,Total_Qty_Received,AvgMoisture_Content,Lot_No ,MktValue_of_Commodity,Arrival_Source,WHR_Issue_Date,CreatedBy,CreatedDate,Client_IP,AvgMoisture_Content_To,Did,CropYear,Remark,SangrahadDate,LicenseNo,wday,wmon,wyear,BranchID,DepositorID) values ('" + Depositor_WHR_Id + "','23','" + Session["Depot_DistID"].ToString() + "' ,'" + Session["Depot_DepotID"].ToString() + "' ,'" + UxCommodity.SelectedValue.ToString() + "','1',upper('" + txtwhrno.Text.Trim().ToString() + "'),'" + ddlDepositor.SelectedItem.Text.ToString() + "','" + dateinus + "','" + _stackbags + "','" + _stackwt + "','" + moisturecontent + "','" + null + "' ,'" + marketvalue + "','" + sofarrival + "','" + dateinus + "','" + HostName.ToString() + "',GETDATE(),'" + ClientIP.ToString() + "','" + moisturecontent_to + "','" + str3.ToString() + "','" + ddlcropyear.SelectedItem.Text + "','" + TextBox2.Text.Trim().ToString() + "','" + TextBox1.Text + "','O','" + Convert.ToInt16(tempsplit[0]) + "','" + Convert.ToInt16(tempsplit[1]) + "','" + Convert.ToInt16(tempsplit[2]) + "','" + BranchId + "','" + ddlDepositor.SelectedValue + "') ";
                         cmd = new SqlCommand(sttry, con, sqltran);
                        
                       
                        cmd.CommandType = CommandType.Text;
                       
                        int res = cmd.ExecuteNonQuery();
                        string reciptid;
                        if (res > 0)
                        {

                            string strw = "insert into [dbo].[whrprintstatus] (WHRID,[PrintStatus],[DateCreated],[IsActive]) values ('" + Depositor_WHR_Id + "','1st',Getdate(),'Yes') ";
                            cmd = new SqlCommand(strw, con, sqltran);
                            int res2whr = cmd.ExecuteNonQuery();

                            if ((ddl_Sofarrival.SelectedValue == "01") || (ddl_Sofarrival.SelectedValue == "02") || (ddl_Sofarrival.SelectedValue == "03"))
                            {
                                cmd = new SqlCommand("sp_receiptdetails_opening_insert", con, sqltran);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Commodity_Id", UxCommodity.SelectedValue);
                                cmd.Parameters.AddWithValue("@Category_Id", 1);
                                cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                                cmd.Parameters.AddWithValue("@DepotId", Session["Depot_DepotID"].ToString());
                                cmd.Parameters.AddWithValue("@WHR_Id", Depositor_WHR_Id);
                                cmd.Parameters.AddWithValue("@DepositorType", ddldepositortype.SelectedValue);
                                cmd.Parameters.AddWithValue("@DepositorName", ddlDepositor.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@Qty_Rvd_No_of_Bags", _stackbags);
                                cmd.Parameters.AddWithValue("@Qty_Rvd_Weight", _stackwt);
                                cmd.Parameters.AddWithValue("@Client_IP", ClientIP.ToString());
                                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                cmd.Parameters.Add("@receiptid", SqlDbType.NVarChar, 20);
                                //cmd.Parameters.Add("@receiptid", SqlDbType.BigInt);
                                cmd.Parameters["@receiptid"].Direction = ParameterDirection.Output;
                                int res1 = cmd.ExecuteNonQuery();
                                reciptid = cmd.Parameters["@receiptid"].Value.ToString();
                                if (res1 > 0)
                                {
                                    // inserting the values in the Arrival Details Table

                                    cmd = new SqlCommand("sp_arrivaldetails_opening_insert", con, sqltran);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@ArrivalStock_Id", Depositor_WHR_Id);
                                    cmd.Parameters.AddWithValue("@Commodity_Id", UxCommodity.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Category_Id", 1);
                                    cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                                    cmd.Parameters.AddWithValue("@DepotId", Session["Depot_DepotID"].ToString());
                                    cmd.Parameters.AddWithValue("@Sender_District", "0");
                                    cmd.Parameters.AddWithValue("@Sender_Godown", "0");
                                    cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Source_of_Arrival", ddl_Sofarrival.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Lot_No", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Depositor_Name", ddlDepositor.SelectedItem.Text);
                                    cmd.Parameters.AddWithValue("@DepositorType", ddldepositortype.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Receipt_ID", reciptid);
                                    cmd.Parameters.AddWithValue("@Scheme_ID", ddlpurpose.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Qty_No_of_Bags", _stackbags);
                                    cmd.Parameters.AddWithValue("@Qty_Wt", _stackwt);
                                    cmd.Parameters.AddWithValue("@Client_IP", ClientIP.ToString());
                                    cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                    cmd.ExecuteNonQuery();

                                    // inersting the stacking details
                                    int j;
                                    for (j = 0; j < gdstackingdetails.Rows.Count; j++)
                                    {

                                        cmd = new SqlCommand("sp_stackingdetails_openening_insert", con, sqltran);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@WHRId", Depositor_WHR_Id);
                                        cmd.Parameters.AddWithValue("@receiptid", reciptid);
                                        cmd.Parameters.AddWithValue("@GodownId", gdstackingdetails.Rows[j].Cells[1].Text.ToString());
                                        cmd.Parameters.AddWithValue("@StackId", gdstackingdetails.Rows[j].Cells[2].Text.ToString());
                                        cmd.Parameters.AddWithValue("@Bags", int.Parse(gdstackingdetails.Rows[j].Cells[5].Text.ToString()));
                                        cmd.Parameters.AddWithValue("@Weight", decimal.Parse(gdstackingdetails.Rows[j].Cells[6].Text.ToString()));
                                        cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                                        cmd.Parameters.AddWithValue("@DepotId", Session["Depot_DepotID"].ToString());
                                        cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                        cmd.ExecuteNonQuery();

                                        // filling the stack register table

                                        cmd = new SqlCommand("sp_dailystackingreceipt_entry", con, sqltran);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@StackId", gdstackingdetails.Rows[j].Cells[2].Text.ToString());
                                        cmd.Parameters.AddWithValue("@ReceiptBags", int.Parse(gdstackingdetails.Rows[j].Cells[5].Text.ToString()));
                                        cmd.Parameters.AddWithValue("@ReceiptWts", decimal.Parse(gdstackingdetails.Rows[j].Cells[6].Text.ToString()));
                                        cmd.ExecuteNonQuery();
                                    }

                                    /////////////////////////////////////////////////Update Status in Table Whr_Status ////////////////

                                    string Status = "Select count(WhrId) from whr_status where WhrId = '" + Depositor_WHR_Id + "'";
                                    cmd = new SqlCommand(Status, con, sqltran); // check WhrId present in whr_status table
                                    string str1 = cmd.ExecuteScalar().ToString();
                                    if (Convert.ToInt16(str1) == 0) ////not found than insert else update
                                    {
                                        string str = "Insert Into whr_status(WhrId,Statusflag) values('" + Depositor_WHR_Id + "','N')";
                                        cmd = new SqlCommand(str, con, sqltran);
                                        int req = cmd.ExecuteNonQuery();
                                        if (req > 0)
                                        {
                                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('WHR record saved successfully Your WHR NO is "+Depositor_WHR_Id+"'); </script> ");
                                            Empty();
                                            Session["RefreshButton"] = "Yes";
                                            btnNewMC.Visible = true;
                                            btnNewMC.Enabled = true;
                                            Session["dt1"] = null;
                                            lblmsg.ForeColor = System.Drawing.Color.Red;
                                            lblmsg.Text = "The Record is added successfully";
                                            showmsg.Visible = true;
                                            lbl_whrno.Text = Depositor_WHR_Id;

                                        }
                                        else
                                        {
                                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Not save...'); </script> ");
                                        }
                                    }
                                    else
                                    {
                                        string str = "Update whr_status Set Statusflag = 'N' where WhrId = '" + Depositor_WHR_Id + "'";
                                        cmd = new SqlCommand(str, con, sqltran);
                                        int res2 = cmd.ExecuteNonQuery();
                                        if (res2 > 0)
                                        {
                                            //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('WHR record saved successfully'); </script> ");
                                            Empty();
                                            lblmsg.ForeColor = System.Drawing.Color.Red;
                                            lblmsg.Text = "The Record is added successfully";
                                            Session["RefreshButton"] = "Yes";
                                            btnNewMC.Visible = true;
                                            btnNewMC.Enabled = true;
                                            EmptyGrid();
                                            Session["dt1"] = null;
                                            showmsg.Visible = true;
                                            lbl_whrno.Text = Depositor_WHR_Id; 
                                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('WHR record saved successfully Your WHR NO is " + Depositor_WHR_Id + "')", true);
                                           

                                        }
                                        else
                                        {
                                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not save...')", true);                                 
                                        }
                                    }
                                }
                                cmd.Dispose();
                                sqltran.Commit();

                            }
                            //else other then Proc/FCI/OtherDepot
                            else
                            {
                                cmd = new SqlCommand("sp_receiptdetails_opening_insert", con, sqltran);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Commodity_Id", UxCommodity.SelectedValue);
                                cmd.Parameters.AddWithValue("@Category_Id", 1);
                                cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                                cmd.Parameters.AddWithValue("@DepotId", Session["Depot_DepotID"].ToString());
                                cmd.Parameters.AddWithValue("@WHR_Id", Depositor_WHR_Id);
                                cmd.Parameters.AddWithValue("@DepositorType", ddldepositortype.SelectedValue);
                                cmd.Parameters.AddWithValue("@DepositorName", ddlDepositor.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@Qty_Rvd_No_of_Bags", _stackbags);
                                cmd.Parameters.AddWithValue("@Qty_Rvd_Weight", _stackwt);
                                cmd.Parameters.AddWithValue("@Client_IP", ClientIP.ToString());
                                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                cmd.Parameters.Add("@receiptid", SqlDbType.NVarChar, 20);
                                cmd.Parameters["@receiptid"].Direction = ParameterDirection.Output;
                                int res3 = cmd.ExecuteNonQuery();
                                reciptid = cmd.Parameters["@receiptid"].Value.ToString();
                                if (res3 > 0)
                                {
                                    // inserting the values in the Arrival Details Table

                                    cmd = new SqlCommand("sp_arrivaldetails_opening_insert", con, sqltran);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@ArrivalStock_Id", Depositor_WHR_Id);
                                    cmd.Parameters.AddWithValue("@Commodity_Id", UxCommodity.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Category_Id", 1);
                                    cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                                    cmd.Parameters.AddWithValue("@DepotId", Session["Depot_DepotID"].ToString());
                                    cmd.Parameters.AddWithValue("@Sender_District", "0");
                                    cmd.Parameters.AddWithValue("@Sender_Godown", "0");
                                    cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Source_of_Arrival", ddl_Sofarrival.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Lot_No", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Depositor_Name", ddlDepositor.SelectedItem.Text);
                                    cmd.Parameters.AddWithValue("@DepositorType", ddldepositortype.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Receipt_ID", reciptid);
                                    cmd.Parameters.AddWithValue("@Scheme_ID", ddlpurpose.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Qty_No_of_Bags", _stackbags);
                                    cmd.Parameters.AddWithValue("@Qty_Wt", _stackwt);
                                    cmd.Parameters.AddWithValue("@Client_IP", ClientIP.ToString());
                                    cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                    cmd.ExecuteNonQuery();
                                    int j;
                                    for (j = 0; j < gdstackingdetails.Rows.Count; j++)
                                    {
                                        cmd = new SqlCommand("sp_stackingdetails_openening_insert", con, sqltran);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@WHRId", Depositor_WHR_Id);
                                        cmd.Parameters.AddWithValue("@receiptid", reciptid);
                                        cmd.Parameters.AddWithValue("@GodownId", gdstackingdetails.Rows[j].Cells[1].Text.ToString());
                                        cmd.Parameters.AddWithValue("@StackId", gdstackingdetails.Rows[j].Cells[2].Text.ToString());
                                        cmd.Parameters.AddWithValue("@Bags", int.Parse(gdstackingdetails.Rows[j].Cells[5].Text.ToString()));
                                        cmd.Parameters.AddWithValue("@Weight", decimal.Parse(gdstackingdetails.Rows[j].Cells[6].Text.ToString()));
                                        cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                                        cmd.Parameters.AddWithValue("@DepotId", Session["Depot_DepotID"].ToString());
                                        cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                        cmd.ExecuteNonQuery();

                                        // filling the stack register table
                                        cmd = new SqlCommand("sp_dailystackingreceipt_entry", con, sqltran);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@StackId", gdstackingdetails.Rows[j].Cells[2].Text.ToString());
                                        cmd.Parameters.AddWithValue("@ReceiptBags", int.Parse(gdstackingdetails.Rows[j].Cells[5].Text.ToString()));
                                        cmd.Parameters.AddWithValue("@ReceiptWts", decimal.Parse(gdstackingdetails.Rows[j].Cells[6].Text.ToString()));
                                        cmd.ExecuteNonQuery();
                                    }

                                    /////////////////////////////////////////////////Update Status in Table Whr_Status ////////////////

                                    string Status = "Select count(WhrId) from whr_status where WhrId = '" + Depositor_WHR_Id + "'";
                                    cmd = new SqlCommand(Status, con, sqltran); // check WhrId present in whr_status table
                                    string str1 = cmd.ExecuteScalar().ToString();
                                    if (Convert.ToInt16(str1) == 0) ////not found than insert else update
                                    {
                                        string str = "Insert Into whr_status(WhrId,Statusflag) values('" + Depositor_WHR_Id + "','N')";
                                        cmd = new SqlCommand(str, con, sqltran);
                                        int req = cmd.ExecuteNonQuery();
                                        if (req > 0)
                                        {
                                            //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('WHR record saved successfully'); </script> ");
                                            Empty();
                                            Session["RefreshButton"] = "Yes";
                                            btnNewMC.Visible = true;
                                            btnNewMC.Enabled = true;
                                            Session["dt1"] = null;
                                            lblmsg.ForeColor = System.Drawing.Color.Red;
                                            lblmsg.Text = "The Record is added successfully";
                                            showmsg.Visible = true;
                                            lbl_whrno.Text = Depositor_WHR_Id;
                                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('WHR record saved successfully Your WHR NO is " + Depositor_WHR_Id + "')", true);
                                            // Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('WHR record saved successfully Your WHR NO is " + Depositor_WHR_Id + "'); </script> ");

                                        }
                                        else
                                        {
                                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not save...')", true);                                   
                                        }
                                    }
                                    else
                                    {
                                        string str = "Update whr_status Set Statusflag = 'N' where WhrId = '" + Depositor_WHR_Id + "'";
                                        cmd = new SqlCommand(str, con, sqltran);
                                        int res2 = cmd.ExecuteNonQuery();
                                        if (res2 > 0)
                                        {

                                            //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('WHR record saved successfully'); </script> ");
                                            Empty();
                                            lblmsg.ForeColor = System.Drawing.Color.Red;
                                            lblmsg.Text = "The Record is added successfully";
                                            Session["RefreshButton"] = "Yes";
                                            btnNewMC.Visible = true;
                                            btnNewMC.Enabled = true;
                                            EmptyGrid();
                                            Session["dt1"] = null;
                                            showmsg.Visible = true;
                                            lbl_whrno.Text = Depositor_WHR_Id;
                                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('WHR record saved successfully Your WHR NO is " + Depositor_WHR_Id + "')", true);
                                           
                                           // Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('WHR record saved successfully Your WHR NO is " + Depositor_WHR_Id + "'); </script> ");
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not save...')", true);                                  
                                        }
                                    }
                                }

                                cmd.Dispose();
                                sqltran.Commit();
                            }
                        }
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('The WHR No already exist.'); </script> ");
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "The WHR No already exist.";
                    }
                }
            }
           // }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
       
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Welcome.aspx");
    }

    protected void ddlcropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Getrates();
    }
}
