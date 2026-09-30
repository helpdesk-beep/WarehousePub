using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_MRecivingDtl : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataTable EditStack = new DataTable();
    DataTable EditStackNonMPSCSC = new DataTable();
    string Todaydate = "";
    string CheckValid = "";
    string receiptid = string.Empty;
    string gatePassid = string.Empty;
    string ArrivalStockid = string.Empty;
    SqlCommand cmd = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGodnList();
            filldepositor();
            fillCommodity();
         //   Fillgrid();
            FillGodown();

            ViewState["ckstat"] = "Empty";
            Session["RefreshButton"] = "No";
        }
    }

    private void fillGodnList()
    {
        if (Session["Depot_DistID"] != null)
        {
            //string query = " SELECT Godown_ID,Godown_Name  FROM tbl_MetaData_GODOWN where DistrictId ='" + Session["Depot_DistID"].ToString() + "' and  DepotId  ='" + Session["Depot_DepotID"].ToString() + "' order by Godown_Name Asc";
            string query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN] WHERE BranchId = '" + Session["BranchId"].ToString() + "'   ORDER BY [Godown_Name] Asc ";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_godown.DataSource = ds.Tables[0];
                ddl_godown.DataTextField = "Godown_Name";
                ddl_godown.DataValueField = "Godown_ID";
                ddl_godown.DataBind();
                ddl_godown.Items.Insert(0, "--Select--");
            }
        }
    }
    private void filldepositor()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            // string query = "SELECT  distinct( tbl_Storage_Arrival_Stock.Depositor_Name)  as Depositor_Name  FROM tbl_Storage_Receipt_Details INNER JOIN tbl_Storage_Arrival_Stock ON tbl_Storage_Receipt_Details.StorageReceipt_Id = tbl_Storage_Arrival_Stock.Receipt_ID where tbl_Storage_Receipt_Details.Depotid ='" + Session["Depot_DepotID"].ToString() + "'  order by Depositor_Name";
            string query = "  select distinct (select tbl_MetaData_DEPOSITOR.Depositor_Name from dbo.tbl_MetaData_DEPOSITOR where Depositor_ID=[Tbl_Mobile_Receiving].DepositorID) as Depositor_Name,Tbl_Mobile_Receiving.DepositorID as Depositor_ID from [Tbl_Mobile_Receiving] where [Tbl_Mobile_Receiving].BranchId='" + Session["BranchId"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldepositorname.Items.Clear();
                ddldepositorname.DataSource = ds.Tables[0];
                ddldepositorname.DataTextField = "Depositor_Name";
                ddldepositorname.DataValueField = "Depositor_ID";
                ddldepositorname.DataBind();
                ddldepositorname.Items.Insert(0, "--Select--");
                // ddldepositorname.SelectedItem.Text = "MPSCSC";
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void fillCommodity()
    {
        string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name Asc";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.Items.Clear();
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, "--Select--");
            ddlcommodity.SelectedValue = "22";
        }
    }
    private void Fillgrid()
    {
        try
        {
            string query = "";
            //string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Qry_Order";

            query = "SELECT  [MRid],[BranchId],[DepositorType],[DepositorID],[DepositFrom],[ChallanNo],[ReceiptID],[TruckNo] ,[Commodity],[DateofDeposit],[category],[CropYear],[Transpoter],[WCMNO],[MOWgt],[QtySent],[BagsSent],[QtyRec],[BagsRec],[Godown],[DateofReceipt],[WRecNum] FROM [Intergrated_MP_STORAGE].[dbo].[Tbl_Mobile_Receiving] where WrecNum is null and BranchId='" + Session["BranchId"].ToString() + "'  and Commodity='" + ddlcommodity.SelectedValue.ToString() + "' and Godown='" + ddl_godown.SelectedValue.ToString() + "' and DateofDeposit='" + getDate_MDY(txtdepositdate.Text) + "' order by DateofDeposit asc";
            
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvrec.DataSource = ds.Tables[0];
                gvrec.DataBind();
                gvrec.HeaderRow.Cells[7].Visible = false;
                gvrec.HeaderRow.Cells[8].Visible = false;

                gvrec.HeaderRow.Cells[9].Visible = false;
                gvrec.HeaderRow.Cells[10].Visible = false;
                gvrec.HeaderRow.Cells[11].Visible = false;
                gvrec.HeaderRow.Cells[12].Visible = false;
                gvrec.HeaderRow.Cells[13].Visible = false;
             //   gvrec.HeaderRow.Cells[14].Visible = false;
                gvrec.HeaderRow.Cells[16].Visible = false;
                for (int i = 0; i < gvrec.Rows.Count; i++)
                {
                    gvrec.Rows[i].Cells[7].Visible = false;
                    gvrec.Rows[i].Cells[8].Visible = false;
                    gvrec.Rows[i].Cells[9].Visible = false;
                    gvrec.Rows[i].Cells[10].Visible = false;
                    gvrec.Rows[i].Cells[11].Visible = false;
                    gvrec.Rows[i].Cells[12].Visible = false;
                    gvrec.Rows[i].Cells[13].Visible = false;
                   // gvrec.Rows[i].Cells[14].Visible = false;
                    gvrec.Rows[i].Cells[16].Visible = false;
                }
                
            }
        }
        catch (Exception)
        {

            //// throw;
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        Fillgrid();
        int gdbag = 0;
        decimal gdwts = 0;
        for (int s = 0; s < gvrec.Rows.Count; s++)
        {
            gdbag = gdbag + int.Parse(gvrec.Rows[s].Cells[4].Text.ToString());
            gdwts = gdwts + decimal.Parse(gvrec.Rows[s].Cells[5].Text.ToString());
        }
        lblbagstotal.Text = gdbag.ToString();
        lblqtytotal.Text = gdwts.ToString();
        ddlGodownNo.SelectedValue = ddl_godown.SelectedValue.ToString();
    }
    protected void gdstackingdetails_PreRender(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            count = gdstackingdetails.Rows.Count;
            if (count > 0)
            {
                btnsave.Enabled = true;
            }
            else
            {
                btnsave.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error gdStackingDetails_PreRender has occured, try again'); </script> ");
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
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error gdStackingDetails_RowCreated has occured, try again'); </script> ");
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
            chksum();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in gdStackingDetails_RowDeleting has occured, try again'); </script> ");
        }
    }

    protected void gdEditStackingDetails_PreRender(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            count = gdEditStackingDetails.Rows.Count;
            if (count > 0)
            {
                // btnUpdate.Enabled = true;
            }
            else
            {
                //   btnUpdate.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in gdEdit StackingDetails has occured, try again'); </script> ");
        }
    }

    protected void gdEditStackingDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (ViewState["ckEditstat"].ToString() != "Delete")
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in gdEditStackingDetails_RowCreated has occured, try again'); </script> ");
        }
    }

    protected void gdEditStackingDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int i = e.RowIndex;
            if (gdEditStackingDetails.Rows.Count < 1)
            {
                ViewState["ckEditstat"] = "Delete";
            }

            ((DataTable)Session["EditStack"]).Rows[i]["Bags"] = "0";
            ((DataTable)Session["EditStack"]).Rows[i]["Weight"] = "0";
            ((DataTable)Session["EditStack"]).AcceptChanges();

            gdEditStackingDetails.DataSource = (DataTable)Session["EditStack"];
            gdEditStackingDetails.DataBind();
            chksumEdit();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in gdEditStackingDetails_RowDeleting has occured, try again'); </script> ");
        }
    }
    protected void ddlGodownNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            fillStack();
            // ddlStackNo_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error ddlGodownNo_Selected Index has occured, try again'); </script> ");
        }
    }
    protected void FillGodown()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                ddlGodownNo.Items.Clear();
                string query = "";

               
                    query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN] WHERE BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID in (select distinct Godown_ID from tbl_MetaData_STACK where BranchID ='" + Session["BranchId"].ToString() + "' and Commodity_Id='" + ddlcommodity.SelectedValue.ToString() + "')  ORDER BY [Godown_Name] ";
               
              
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlGodownNo.DataSource = ds.Tables[0];
                    ddlGodownNo.DataTextField = "Godown_Name";
                    ddlGodownNo.DataValueField = "Godown_ID";
                    ddlGodownNo.DataBind();
                    ddlGodownNo.Items.Insert(0, " --select--");
                }
                else
                {
                    ddlGodownNo.DataSource = null;
                    ddlGodownNo.DataBind();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in FillGodown has occurred , try again!'); </script> ");
            }
        }
        else
        {
            Response.Redirect("../../Logout.aspx");
        }
    }
    protected void fillStack()
    {
        try
        {
            string query = "";
            ddlStackNo.Items.Clear();

            query = "SELECT Stack_ID, Stack_Name FROM tbl_MetaData_STACK WHERE (Godown_ID = '" + ddlGodownNo.SelectedValue + "' and Commodity_Id = '" + ddlcommodity.SelectedValue.ToString() + "' and Stack_Killed = 'N' ) order by Stack_Name";


            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlStackNo.DataSource = ds.Tables[0];
                ddlStackNo.DataTextField = "Stack_Name";
                ddlStackNo.DataValueField = "Stack_ID";
                ddlStackNo.DataBind();
                ddlStackNo.Items.Insert(0, " --select--");
            }
            else
            {
                ddlStackNo.Items.Clear();
                ddlStackNo.DataSource = null;
                ddlStackNo.DataBind();
            }

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in FillStack has occured, try again '); </script> ");
        }
    }
    protected void gdEditStackingDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            int i = e.NewEditIndex;
            if (gdEditStackingDetails.Rows.Count < 1)
            {
                ViewState["ckEditstat"] = "Edit";
            }

            ddlGodownNo.SelectedValue = ((DataTable)Session["EditStack"]).Rows[i][0].ToString();
            ddlGodownNo_SelectedIndexChanged(sender, e);
            ddlStackNo.SelectedValue = ((DataTable)Session["EditStack"]).Rows[i][1].ToString();
            ddlGodownNo.Enabled = false;
            ddlStackNo.Enabled = false;
            txtStackBags.Text = ((DataTable)Session["EditStack"]).Rows[i][4].ToString();
            txtStackWt.Text = ((DataTable)Session["EditStack"]).Rows[i][5].ToString();

            ((DataTable)Session["EditStack"]).Rows[i].Delete();

            ((DataTable)Session["EditStack"]).AcceptChanges();

            gdEditStackingDetails.DataSource = (DataTable)Session["EditStack"];
            gdEditStackingDetails.DataBind();
            chksumEdit();
            // btnUpdate.Enabled = false;
            gdEditStackingDetails.Enabled = false;
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error gdEditStackingDetails_RowEditing has occured, try again'); </script> ");
        }
    }
    protected void ADD_EditStock()
    {
        bool checkEditstatus = false;
        try
        {
            if (ddlStackNo.Items.Count > 0)
            {


                if (Session["EditStack"] == null)
                {
                    EditStack = CreateTableEditStack();

                    Session["EditStack"] = EditStack;

                }
                // adding rows to the datatable
                DataRow dr = ((DataTable)Session["EditStack"]).NewRow();
                ((DataTable)Session["EditStack"]).AcceptChanges();
                dr["Godownid"] = ddlGodownNo.SelectedValue;
                dr["Stackid"] = ddlStackNo.SelectedValue;
                dr["GodownName"] = ddlGodownNo.SelectedItem.Text;
                dr["StackName"] = ddlStackNo.SelectedItem.Text;
                dr["Bags"] = txtStackBags.Text.Trim();
                dr["Weight"] = txtStackWt.Text.Trim();
                if (gdEditStackingDetails.Rows.Count > 0)
                {
                    int i;
                    // checking whether or not the stack is already added to the grid view
                    for (i = 0; i <= gdEditStackingDetails.Rows.Count - 1; i++)
                    {
                        string stackid = gdEditStackingDetails.Rows[i].Cells[2].Text.ToString();
                        string selectstackid = ddlStackNo.SelectedValue.ToString();
                        if (stackid == selectstackid)
                        {
                            checkEditstatus = true;
                        }
                    }
                    if (checkEditstatus == false)
                    {
                        ((DataTable)Session["EditStack"]).Rows.Add(dr);
                        ((DataTable)Session["EditStack"]).AcceptChanges();
                        gdEditStackingDetails.DataSource = (DataTable)Session["EditStack"];
                        gdEditStackingDetails.DataBind();
                        txtStackBags.Text = null;
                        txtStackWt.Text = null;
                        chksumEdit();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Entry for this stack is already done'); </script> ");
                    }
                }
                else
                {
                    ((DataTable)Session["EditStack"]).Rows.Add(dr);
                    ((DataTable)Session["EditStack"]).AcceptChanges();
                    gdEditStackingDetails.DataSource = (DataTable)Session["EditStack"];
                    gdEditStackingDetails.DataBind();
                    txtStackBags.Text = null;
                    txtStackWt.Text = null;
                    chksumEdit();
                }

                gdEditStackingDetails.Enabled = true;
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No stack under the selected Commodity ,Category and Godown Number'); </script> ");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in Add_EDITStock has occured, try again'); </script> ");
        }
    }
    protected void btnAddStack_Click(object sender, EventArgs e)
    {
        bool checkstatus = false;
        try
        {
            if (txtStackBags.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No of Bags to be added in Stack is required!'); </script> ");
                return;
            }
            else if (txtStackWt.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Bags Weight to be added in Stack is required!'); </script> ");
                return;
            }
            else if ((txtStackAvailable.Text != "") && (Convert.ToDecimal(txtStackAvailable.Text) < Convert.ToDecimal(txtStackWt.Text)))
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Insuffient StackCapacity in the Selected Stack!'); </script> ");
                return;
            }
            else
            {
                if (Session["WLC_StorageReceipt_Id"] != null)
                {
                    if (Session["Mode"] != null)
                    {
                        if (Session["Mode"].ToString() == "Edit")
                        {
                            ADD_EditStock();
                        }

                    }
                }
                else
                {
                    if (ddlStackNo.Items.Count > 0)
                    {

                        btnsave.Enabled = true;
                        if (Session["dt1"] == null)
                        {
                            Dt1 = CreateTable();
                            Session["dt1"] = Dt1;
                        }
                        // adding rows to the datatable
                        DataRow dr = ((DataTable)Session["dt1"]).NewRow();
                        ((DataTable)Session["dt1"]).AcceptChanges();
                        dr["Godownid"] = ddlGodownNo.SelectedValue;
                        dr["Stackid"] = ddlStackNo.SelectedValue;
                        dr["GodownName"] = ddlGodownNo.SelectedItem.Text;
                        dr["StackName"] = ddlStackNo.SelectedItem.Text;
                        dr["Bags"] = txtStackBags.Text.Trim();
                        dr["Weight"] = txtStackWt.Text.Trim();
                        if (gdstackingdetails.Rows.Count > 0)
                        {
                            int i;

                            // checking whether or not the stack is already added to the grid view
                            for (i = 0; i <= gdstackingdetails.Rows.Count - 1; i++)
                            {
                                string stackid = gdstackingdetails.Rows[i].Cells[2].Text.ToString();
                                string selectstackid = ddlStackNo.SelectedValue.ToString();
                                if (stackid == selectstackid)
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
                                txtStackBags.Text = null;
                                txtStackWt.Text = null;
                                chksum();
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Entry for this stack is already done'); </script> ");
                            }
                        }
                        else
                        {
                            ((DataTable)Session["dt1"]).Rows.Add(dr);
                            ((DataTable)Session["dt1"]).AcceptChanges();
                            gdstackingdetails.DataSource = (DataTable)Session["dt1"];
                            gdstackingdetails.DataBind();
                            txtStackBags.Text = null;
                            txtStackWt.Text = null;
                            chksum();
                        }
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No stack under the selected Commodity ,Category and Godown Number'); </script> ");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }
    }
    protected void chksum()
    {
        try
        {
            int stackbag = 0;
            decimal stackwts = 0;
            if (gdstackingdetails.Rows.Count > 0)
            {
                for (int s = 0; s < gdstackingdetails.Rows.Count; s++)
                {
                    stackbag = stackbag + int.Parse(gdstackingdetails.Rows[s].Cells[5].Text.ToString());
                    stackwts = stackwts + decimal.Parse(gdstackingdetails.Rows[s].Cells[6].Text.ToString());
                }

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in Chksum has occured, try again'); </script> ");
        }
    }

    protected void chksumEdit()
    {
        try
        {
            int stackbag = 0;
            decimal stackwts = 0;
            if (gdEditStackingDetails.Rows.Count > 0)
            {
                for (int s = 0; s < gdEditStackingDetails.Rows.Count; s++)
                {
                    stackbag = stackbag + int.Parse(gdEditStackingDetails.Rows[s].Cells[5].Text.ToString());
                    stackwts = stackwts + decimal.Parse(gdEditStackingDetails.Rows[s].Cells[6].Text.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in chksumEdit has occured, try again'); </script> ");
        }
    }
    private DataTable CreateTable()
    {
        DataTable dt = new DataTable();//DataTable is created
        DataColumn Godownid = new DataColumn("Godownid", Type.GetType("System.String"));
        DataColumn Stackid = new DataColumn("Stackid", Type.GetType("System.String"));
        DataColumn GodownName = new DataColumn("GodownName", Type.GetType("System.String"));
        DataColumn StackName = new DataColumn("StackName", Type.GetType("System.String"));
        DataColumn Bags = new DataColumn("Bags", Type.GetType("System.Int32"));
        DataColumn Weight = new DataColumn("Weight", Type.GetType("System.Decimal"));
        dt.Columns.Add(Godownid);//Column is added to the DataTable
        dt.Columns.Add(Stackid);//Column is added to the DataTable
        dt.Columns.Add(GodownName);//Column is added to the DataTable
        dt.Columns.Add(StackName);//Column is added to the DataTable
        dt.Columns.Add(Bags);//Column is added to the DataTable
        dt.Columns.Add(Weight);//Column is added to the DataTable

        dt.AcceptChanges();
        return dt;
    }

    private DataTable CreateTableEditStack()
    {
        DataTable dtEditStack = new DataTable();//DataTable is created
        DataColumn Godownid = new DataColumn("Godownid", Type.GetType("System.String"));
        DataColumn Stackid = new DataColumn("Stackid", Type.GetType("System.String"));
        DataColumn GodownName = new DataColumn("GodownName", Type.GetType("System.String"));
        DataColumn StackName = new DataColumn("StackName", Type.GetType("System.String"));
        DataColumn Bags = new DataColumn("Bags", Type.GetType("System.Int32"));
        DataColumn Weight = new DataColumn("Weight", Type.GetType("System.Decimal"));
        dtEditStack.Columns.Add(Godownid);//Column is added to the DataTable
        dtEditStack.Columns.Add(Stackid);//Column is added to the DataTable
        dtEditStack.Columns.Add(GodownName);//Column is added to the DataTable
        dtEditStack.Columns.Add(StackName);//Column is added to the DataTable
        dtEditStack.Columns.Add(Bags);//Column is added to the DataTable
        dtEditStack.Columns.Add(Weight);//Column is added to the DataTable

        dtEditStack.AcceptChanges();
        return dtEditStack;
    }

    protected void ddlGodownNo_SelectedIndexChanged1(object sender, EventArgs e)
    {
        fillStack();
    }
    protected void ddlStackNo_PreRender(object sender, EventArgs e)
    {
        ddlStackNo_SelectedIndexChanged(sender, e);
    }
    protected void ddlStackNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtStackCurrentCapacity.Text = "0";
        txtStackMaxCap.Text = "0";
        txtStackAvailable.Text = "0";
        try
        {
            if (ddlStackNo.Items.Count > 0)
            {
                // String query = "select tbl_MetaData_STACK.Stack_capacity,(select (isnull(a.wet,0) - isnull(b.wet2,0)) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details where Stack_ID = tbl_MetaData_STACK.Stack_ID) a,(select SUM(Bags_Weight) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = tbl_MetaData_STACK.Stack_ID AND GP.Status !='CANCEL') b) AS 'Current_Capacity' from tbl_MetaData_STACK  where tbl_MetaData_STACK.Stack_ID = '" + ddlStackNo.SelectedValue.ToString() + "'";
                String query = "select tbl_MetaData_STACK.Stack_capacity,(select (isnull(a.wet,0) - isnull(b.wet2,0)-isnull(l.loss,0)+isnull(g.gain,0)) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details where Stack_ID = tbl_MetaData_STACK.Stack_ID) a,(select SUM(Bags_Weight) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = tbl_MetaData_STACK.Stack_ID AND GP.Status !='CANCEL') b,(select isnull(SUM(Loss),0) as loss from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = tbl_MetaData_STACK.Stack_ID AND GP.Status !='CANCEL') as l,(select isnull(SUM(Gain),0) as gain from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = tbl_MetaData_STACK.Stack_ID AND GP.Status !='CANCEL') as g) AS 'Current_Capacity' from tbl_MetaData_STACK  where tbl_MetaData_STACK.Stack_ID = '" + ddlStackNo.SelectedValue.ToString() + "'";
                //  string query = "select convert(decimal(18,2),TMS.Stack_capacity) as Stack_capacity,convert(decimal(18,2),(select (isnull(a.wet,0) - isnull(b.wet2,0)- (select isnull(SUM(Loss),0) as loss from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL')+ (select isnull(SUM(Gain),0) as gain from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL')  ) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details join tbl_Storage_Receipt_Details on tbl_storage_Stacking_Details.StorageReceipt_Id = tbl_Storage_Receipt_Details.StorageReceipt_Id where Stack_ID = TMS.Stack_ID) a,(select SUM(Bags_Weight) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL') b)) AS 'Current_Capacity' from tbl_MetaData_STACK as TMS join tbl_MetaData_GODOWN as TMG on TMS.Godown_ID = TMG.Godown_ID JOIN tbl_MetaData_STORAGE_COMMODITY AS TMSC on TMS.Commodity_Id = TMSC.Commodity_Id where TMS.DepotId = '" + Session["WLC_Depot_ID"].ToString() + "' and TMS.Stack_Killed = 'N' and TMS.Stack_ID='" + ddlStackNo.SelectedValue.ToString() + "' order by TMS.Stack_ID";
                // string query = "select convert(decimal(18,2),TMS.Stack_capacity) as Stack_capacity,convert(decimal(18,2),(select (isnull(a.wet,0) - isnull(b.wet2,0)- (select isnull(SUM(Loss),0) as loss from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL')+ (select isnull(SUM(Gain),0) as gain from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL')  ) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details join tbl_Storage_Receipt_Details on tbl_storage_Stacking_Details.StorageReceipt_Id = tbl_Storage_Receipt_Details.StorageReceipt_Id where Stack_ID = TMS.Stack_ID) a,(select SUM(Bags_Weight) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL') b)) AS 'Current_Capacity' from tbl_MetaData_STACK as TMS join tbl_MetaData_GODOWN as TMG on TMS.Godown_ID = TMG.Godown_ID JOIN tbl_MetaData_STORAGE_COMMODITY AS TMSC on TMS.Commodity_Id = TMSC.Commodity_Id where TMS.DepotId = '" + Session["WLC_Depot_ID"].ToString() + "' and TMS.Stack_Killed = 'N' and TMS.Stack_ID='" + ddlStackNo.SelectedValue.ToString() + "' order by TMS.Stack_ID";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    double Stackcap = Convert.ToDouble(ds.Tables[0].Rows[0]["Stack_capacity"].ToString());
                    double cureentcap = Convert.ToDouble(ds.Tables[0].Rows[0]["Current_Capacity"].ToString());
                    txtStackCurrentCapacity.Text = cureentcap.ToString();
                    txtStackMaxCap.Text = Stackcap.ToString();
                    txtStackAvailable.Text = String.Format("{0:0.00000}", (Convert.ToDouble(txtStackMaxCap.Text) - Convert.ToDouble(txtStackCurrentCapacity.Text)));
                }

            }
            else
            {
                //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "No stack under the selected Commodity ,Category and Godown Number" + "'); </script> ");  
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in ddlStackNo_SelectedIndexChanged has occured, try again'); </script> ");
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
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                btnsave.Enabled = false;

                string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
                try
                {
                    int _stackbags = 0;
                    decimal _stackwt = 0;
                    int l;
                    if (gdstackingdetails.Rows.Count > 0)
                    {
                        for (l = 0; l < gdstackingdetails.Rows.Count; l++)
                        {
                            _stackbags = _stackbags + int.Parse(gdstackingdetails.Rows[l].Cells[5].Text.ToString());
                            _stackwt = _stackwt + decimal.Parse(gdstackingdetails.Rows[l].Cells[6].Text.ToString());
                        }

                        if (Convert.ToInt32(lblbagstotal.Text.ToString()) != Convert.ToInt32(_stackbags.ToString()) || (decimal.Parse(lblqtytotal.Text) != decimal.Parse(_stackwt.ToString())))
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Total Bags/Weight Recieved should be equal to sum of Stack Bags/Weight !'); </script> ");
                        }
                        else
                        {

                            if (Con.State == ConnectionState.Closed)
                            {
                                Con.Open();
                            }
                            string QueryMax = "select isnull(Max(ArrivalStock_Id),0) from tbl_Storage_Arrival_Stock where District_Id='" + Session["Depot_DistID"].ToString() + "' and BranchID='" + Session["BranchId"].ToString() + "' ";
                            cmd = new SqlCommand(QueryMax, Con); // check ArrivalStockid present in tbl_Storage_Arrival_Stock table
                            string str3 = cmd.ExecuteScalar().ToString();
                            if (Convert.ToInt64(str3) != 0)
                            {
                                ArrivalStockid = Convert.ToString(Convert.ToInt64(str3) + 1);
                                if (ArrivalStockid != String.Empty || ArrivalStockid != "")
                                {
                                Found:
                                    string Queryc = "select count(ArrivalStock_Id) from tbl_Storage_Arrival_Stock where ArrivalStock_Id='" + ArrivalStockid.ToString() + "'";
                                    cmd = new SqlCommand(Queryc, Con); // check ArrivalStockid present in tbl_Storage_Arrival_Stock table
                                    string maxcount = cmd.ExecuteScalar().ToString();
                                    if (Convert.ToInt16(maxcount) > 0)
                                    {
                                        ArrivalStockid = Convert.ToString(Convert.ToInt64(ArrivalStockid) + 1);
                                        goto Found;
                                    }
                                }
                            }
                            else
                            {
                                string Depotid = Session["Depot_DepotID"].ToString();
                                string BranchId = Session["BranchId"].ToString();
                                ArrivalStockid = BranchId + System.DateTime.Now.Year.ToString().Substring(2, 2) + "00001";
                            }
                            for (int ino = 0; ino < gvrec.Rows.Count; ino++)
                            {

                              

                                   cmd = new SqlCommand("MPWLC_sp_MomentChallan_Reciept_insert", Con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@ArrivalStockId", ArrivalStockid);
                                    cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                                    cmd.Parameters.AddWithValue("@DepotId", Session["Depot_DepotID"].ToString());
                                    cmd.Parameters.AddWithValue("@DepositDate", getDate_MDY(Convert.ToString(txtdepositdate.Text.Trim())));
                                    cmd.Parameters.AddWithValue("@Commodity_Id",ddlcommodity.SelectedValue.ToString());
                                    cmd.Parameters.AddWithValue("@Mode_of_weighment", (gvrec.Rows[ino].Cells[3].Text.ToString()));
                                    cmd.Parameters.AddWithValue("@AcceptanceNo", "");
                                    cmd.Parameters.AddWithValue("@PurchasCentre", "");
                                    cmd.Parameters.AddWithValue("@IssueID", "");
                                    cmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());

                                    cmd.Parameters.AddWithValue("@Qty_No_of_Bags", Convert.ToInt64(gvrec.Rows[ino].Cells[15].Text.ToString()));

                                    cmd.Parameters.AddWithValue("@Qty_Wt", Convert.ToDecimal(gvrec.Rows[ino].Cells[14].Text.ToString()));

                                    cmd.Parameters.AddWithValue("@Category_Id", (gvrec.Rows[ino].Cells[11].Text.ToString()));
                                    cmd.Parameters.AddWithValue("@Depositor_Name", ddldepositorname.SelectedItem.Text.ToString());
                                    cmd.Parameters.AddWithValue("@Crop_Year",gvrec.Rows[ino].Cells[12].Text.ToString());
                                    cmd.Parameters.Add("@DepositorType", SqlDbType.VarChar, 20);
                                    cmd.Parameters["@DepositorType"].Value = "Institution";
                                   
                                        cmd.Parameters.AddWithValue("@Sender_District", Session["Depot_DistID"].ToString());
                                        cmd.Parameters.AddWithValue("@Sender_Godown", Session["Depot_DepotID"].ToString());
                                    
                                    cmd.Parameters.AddWithValue("@Challan_No", gvrec.Rows[ino].Cells[0].Text.ToString());
                                    cmd.Parameters.AddWithValue("@Truck_No",gvrec.Rows[ino].Cells[1].Text.ToString());
                                    cmd.Parameters.AddWithValue("@Source_of_Arrival", gvrec.Rows[ino].Cells[9].Text.ToString());
                                    cmd.Parameters.Add("@ArrivalSource_ID", "");
                                
                                    cmd.Parameters.AddWithValue("@Miller", DBNull.Value);
                                    //}

                                   
                                        cmd.Parameters.AddWithValue("@Quality_Moisture", DBNull.Value);
                                   
                                    cmd.Parameters.AddWithValue("@Remarks", "");
                                    cmd.Parameters.AddWithValue("@Scheme_ID", "0");
                                 
                                    cmd.Parameters.AddWithValue("@Acpt_FCIRO_No", gvrec.Rows[ino].Cells[10].Text.ToString());
                                    cmd.Parameters.AddWithValue("@Acpt_FCIRO_Date", DBNull.Value);
                                    //}
                                    cmd.Parameters.AddWithValue("@CreatedBy", Session["Depot_DepotID"].ToString());
                                    cmd.Parameters.AddWithValue("@Client_IP", ClientIP.ToString());
                                   
                                    cmd.Parameters.AddWithValue("@Transporter_id", gvrec.Rows[ino].Cells[13].Text.ToString());
                                    // }

                                   
                                    cmd.Parameters.AddWithValue("@WCMNo_Sending", gvrec.Rows[ino].Cells[2].Text.ToString());
                                    

                                    cmd.Parameters.Add("@receiptid", SqlDbType.NVarChar, 20);
                                    cmd.Parameters["@receiptid"].Direction = ParameterDirection.Output;
                                  
                                    cmd.Parameters.AddWithValue("@Acceptable_Bags", Convert.ToInt64(gvrec.Rows[ino].Cells[4].Text.ToString()));
                                    //}
                                    //if (txtQtyAcceptable.Text.Trim().ToString() == "")
                                    //{
                                    //    cmd.Parameters.AddWithValue("@Acceptable_Wt", _stackwt);
                                    //}
                                    //else
                                    //{
                                    cmd.Parameters.AddWithValue("@Acceptable_Wt", Convert.ToDecimal(gvrec.Rows[ino].Cells[5].Text.ToString()));
                                    //}

                                    cmd.ExecuteNonQuery();
                                    receiptid = cmd.Parameters["@receiptid"].Value.ToString();
                                    txtArrivalSrcId.Text = ArrivalStockid;

                                    qry = "update [Tbl_Mobile_Receiving] set WRecNum='" + receiptid + "' where MRid='" + gvrec.Rows[ino].Cells[6].Text.ToString() + "'";
                                    cmd = new SqlCommand(qry, Con);
                                    int c = cmd.ExecuteNonQuery();
                                
                            }
                            cmd.Dispose();
                            Con.Close();
                            // adding the stack details to the database
                            string G = "";
                            string S = "";
                            int j;
                            for (j = 0; j < gdstackingdetails.Rows.Count; j++)
                            {
                                if (Con.State == ConnectionState.Closed)
                                {
                                    Con.Open();
                                }
                                SqlCommand sqlCmd = new SqlCommand();
                                sqlCmd.Connection = Con;
                                sqlCmd.CommandText = "MPWLC_sp_stackingdetails_insert";
                                sqlCmd.CommandType = CommandType.StoredProcedure;
                                sqlCmd.Parameters.AddWithValue("@receiptid", receiptid);
                                sqlCmd.Parameters.AddWithValue("@GodownId", gdstackingdetails.Rows[j].Cells[1].Text.ToString());
                                sqlCmd.Parameters.AddWithValue("@StackId", gdstackingdetails.Rows[j].Cells[2].Text.ToString());
                                sqlCmd.Parameters.AddWithValue("@SBags", int.Parse(gdstackingdetails.Rows[j].Cells[5].Text.ToString()));
                                sqlCmd.Parameters.AddWithValue("@SWeight", decimal.Parse(gdstackingdetails.Rows[j].Cells[6].Text.ToString()));
                                sqlCmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                                sqlCmd.Parameters.AddWithValue("@DepotId", Session["Depot_DepotID"].ToString());
                                sqlCmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());
                                sqlCmd.ExecuteNonQuery();

                                //changed added fn to update the gate pass godown/stack info in gate pass table also
                                G = G + gdstackingdetails.Rows[j].Cells[3].Text.ToString() + "/";
                                S = S + gdstackingdetails.Rows[j].Cells[4].Text.ToString() + "/";
                            }

                            G = G.TrimEnd('/');
                            S = S.TrimEnd('/');
                            Session["dt1"] = null;
                            gdstackingdetails.DataSource = null;
                            gdstackingdetails.DataBind();
                           // FillFCIOTDeoptData();
                            FillGodown();
                           // ddlGodownNo.SelectedValue = ddl_godown.SelectedValue.ToString();
                            //fillStack();
                            Session["RefreshButton"] = "Yes";
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record saved successfully'); </script> ");
                            Session["RefreshButton"] = "No";


                        } ///here transactions ends
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please add Stack information first'); </script> ");
                    }

                }//Try end
                catch (Exception ex)
                {
                    // throw;
                   lblmsg.Text = ex.Message.ToString();
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                }

            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
               lblmsg.Text = ex.ToString();
            }
            finally
            {
                Con.Close();
                btnsave.Enabled = true;
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
}