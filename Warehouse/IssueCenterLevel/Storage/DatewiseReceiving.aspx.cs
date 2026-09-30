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

public partial class IssueCenterLevel_Storage_DatewiseReceiving : System.Web.UI.Page
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
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (!IsPostBack)
            {              
               // FillGodown();
               // fillStack();
                FillFCIOTDeoptData();
                ViewState["ckstat"] = "Empty";
            }

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
                                for (int ino = 0; ino < gvchallan.Rows.Count; ino++)
                                {
                                    if (((CheckBox)gvchallan.Rows[ino].FindControl("ckboxtrucklist")).Checked == true)
                                    {
                                        cmd = new SqlCommand("MPWLC_sp_MomentChallan_Reciept_insert", Con);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@ArrivalStockId", ArrivalStockid);
                                        cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                                        cmd.Parameters.AddWithValue("@DepotId", Session["Depot_DepotID"].ToString());
                                        cmd.Parameters.AddWithValue("@DepositDate", getDate_MDY(Convert.ToString(txtdateofdepo.Text.Trim())));
                                        cmd.Parameters.AddWithValue("@Commodity_Id", gvchallan.Rows[ino].Cells[17].Text.ToString());
                                        cmd.Parameters.AddWithValue("@Mode_of_weighment", ((DropDownList)gvchallan.Rows[ino].FindControl("ddlmow")).Text.ToString());
                                        cmd.Parameters.AddWithValue("@AcceptanceNo", "");
                                        cmd.Parameters.AddWithValue("@PurchasCentre", "");
                                        cmd.Parameters.AddWithValue("@IssueID", "");
                                        cmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());

                                        cmd.Parameters.AddWithValue("@Qty_No_of_Bags", Convert.ToInt64(gvchallan.Rows[ino].Cells[1].Text.ToString()));

                                        cmd.Parameters.AddWithValue("@Qty_Wt", Convert.ToDecimal(gvchallan.Rows[ino].Cells[2].Text.ToString()));

                                        cmd.Parameters.AddWithValue("@Category_Id", ((DropDownList)gvchallan.Rows[ino].FindControl("ddlgcategory")).SelectedValue.ToString());
                                        cmd.Parameters.AddWithValue("@Depositor_Name", "MPSCSC");
                                        cmd.Parameters.AddWithValue("@Crop_Year", ((DropDownList)gvchallan.Rows[ino].FindControl("ddlcropy")).Text.ToString());
                                        cmd.Parameters.Add("@DepositorType", SqlDbType.VarChar, 20);
                                        cmd.Parameters["@DepositorType"].Value = "Institution";
                                        if ((Session["WLCDepSource"].ToString() == "02") || (Session["WLCDepSource"].ToString() == "03"))//Other Depot & FCI
                                        {
                                            cmd.Parameters.AddWithValue("@Sender_District", gvchallan.Rows[ino].Cells[15].Text.ToString());
                                            cmd.Parameters.AddWithValue("@Sender_Godown", gvchallan.Rows[ino].Cells[13].Text.ToString());
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@Sender_District", Session["Depot_DistID"].ToString());
                                            cmd.Parameters.AddWithValue("@Sender_Godown", Session["Depot_DepotID"].ToString());
                                        }
                                        cmd.Parameters.AddWithValue("@Challan_No", gvchallan.Rows[ino].Cells[0].Text.ToString());
                                        cmd.Parameters.AddWithValue("@Truck_No", gvchallan.Rows[ino].Cells[20].Text.ToString());
                                        cmd.Parameters.AddWithValue("@Source_of_Arrival", Session["WLCDepSource"].ToString());
                                        cmd.Parameters.Add("@ArrivalSource_ID", Session["WLCDepSource"].ToString());
                                        //if (Session["WLCDepSource"].ToString() == "04")//Levy Rice
                                        //{
                                        //    cmd.Parameters.AddWithValue("@Miller", ddlCMR_Rice_Miller.SelectedValue);
                                        //}
                                        //else if (Session["WLCDepSource"].ToString() == "05")//CMR
                                        //{
                                        //    cmd.Parameters.AddWithValue("@Miller", ddlCMR_Rice_Miller.SelectedValue);
                                        //}
                                        //else
                                        //{
                                            cmd.Parameters.AddWithValue("@Miller", DBNull.Value);
                                        //}

                                            if (((TextBox)gvchallan.Rows[ino].FindControl("txtmoisture")).Text.ToString().Trim() == "")
                                        {
                                            cmd.Parameters.AddWithValue("@Quality_Moisture", DBNull.Value);
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@Quality_Moisture", Convert.ToDecimal(((TextBox)gvchallan.Rows[ino].FindControl("txtmoisture")).Text.ToString()));
                                        }
                                        cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.ToString());
                                        cmd.Parameters.AddWithValue("@Scheme_ID", "0");
                                        //if (Session["WLCDepSource"].ToString() == "03")//FCI
                                        //{
                                        //    cmd.Parameters.AddWithValue("@Acpt_FCIRO_No", hfRoNo.Value.ToString());
                                        //    cmd.Parameters.AddWithValue("@Acpt_FCIRO_Date", getDate_MDY(Convert.ToString(hfRODate.Value.Trim())));
                                        //}
                                        //else
                                        //{
                                            cmd.Parameters.AddWithValue("@Acpt_FCIRO_No", DBNull.Value);
                                            cmd.Parameters.AddWithValue("@Acpt_FCIRO_Date", DBNull.Value);
                                        //}
                                        cmd.Parameters.AddWithValue("@CreatedBy", Session["Depot_DepotID"].ToString());
                                        cmd.Parameters.AddWithValue("@Client_IP", ClientIP.ToString());
                                        //if (ddlTransporter.Items.Count == 0)
                                        //{
                                        //    cmd.Parameters.AddWithValue("@Transporter_id", DBNull.Value);
                                        //}
                                        //else
                                        //{
                                        cmd.Parameters.AddWithValue("@Transporter_id", gvchallan.Rows[ino].Cells[19].Text.ToString());
                                       // }
                                        if (((TextBox)gvchallan.Rows[ino].FindControl("txtwcmno")).Text.ToString().Trim() == "")
                                        {
                                            cmd.Parameters.AddWithValue("@WCMNo_Sending", DBNull.Value);
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@WCMNo_Sending", ((TextBox)gvchallan.Rows[ino].FindControl("txtwcmno")).Text.ToString().Trim());
                                        }

                                        cmd.Parameters.Add("@receiptid", SqlDbType.NVarChar, 20);
                                        cmd.Parameters["@receiptid"].Direction = ParameterDirection.Output;
                                        //if (txtBagsAcceptable.Text.Trim().ToString() == "")
                                        //{
                                        //    cmd.Parameters.AddWithValue("@Acceptable_Bags", _stackbags);
                                        //}
                                        //else
                                        //{
                                        cmd.Parameters.AddWithValue("@Acceptable_Bags", Convert.ToInt64(((TextBox)gvchallan.Rows[ino].FindControl("txtbagsrec")).Text.ToString()));
                                        //}
                                        //if (txtQtyAcceptable.Text.Trim().ToString() == "")
                                        //{
                                        //    cmd.Parameters.AddWithValue("@Acceptable_Wt", _stackwt);
                                        //}
                                        //else
                                        //{
                                            cmd.Parameters.AddWithValue("@Acceptable_Wt", Convert.ToDecimal(((TextBox)gvchallan.Rows[ino].FindControl("txtqtyrec")).Text.ToString()));
                                        //}

                                        cmd.ExecuteNonQuery();
                                        receiptid = cmd.Parameters["@receiptid"].Value.ToString();
                                        txtArrivalSrcId.Text = ArrivalStockid;
                                       
                                    }
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
                                FillFCIOTDeoptData();
                                FillGodown();
                                ddlGodownNo.SelectedValue=gvchallan.Rows[0].Cells[13].Text.ToString();
                                fillStack();
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
                        //lblmsg.Text = ex.Message.ToString();
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
               
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('"+ex.Message+"'); </script> ");
               // lblmsg.Text = ex.ToString();
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


    protected void ckboxtrucklist_CheckedChanged(object sender, EventArgs e)
    {



        int _checkboxstatus = -1;
        int i;
        for (i = 0; i < gvchallan.Rows.Count; i++)
        {
            if (((CheckBox)gvchallan.Rows[i].FindControl("ckboxtrucklist")).Checked == true)
            {
                if (_checkboxstatus == -1)
                {
                    _checkboxstatus = _checkboxstatus + 2;
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Only one selection is allowed !')", true);
                    ((CheckBox)gvchallan.Rows[i].FindControl("ckboxtrucklist")).Checked = false;
                }
            }

        }
        if (_checkboxstatus == 1)
        {


            decimal _issedwt = 0;
            int _issedBags = 0;
            for (int ino = 0; ino < gvchallan.Rows.Count; ino++)
            {
                if (((TextBox)gvchallan.Rows[ino].FindControl("txtbagsrec")).Text.ToString() != "")
                {
                    if (((CheckBox)gvchallan.Rows[ino].FindControl("ckboxtrucklist")).Checked == true)
                    {
                        _issedwt = _issedwt + decimal.Parse(((TextBox)gvchallan.Rows[ino].FindControl("txtqtyrec")).Text.ToString());
                        _issedBags = _issedBags + int.Parse(((TextBox)gvchallan.Rows[ino].FindControl("txtbagsrec")).Text.ToString());
                        ((TextBox)gvchallan.Rows[ino].FindControl("txtqtyrec")).Enabled = false;
                        ((TextBox)gvchallan.Rows[ino].FindControl("txtbagsrec")).Enabled = false;
                        ((TextBox)gvchallan.Rows[ino].FindControl("txtwcmno")).Enabled = false;
                        ((TextBox)gvchallan.Rows[ino].FindControl("txtmoisture")).Enabled = false;
                    }
                    else
                    {
                        ((TextBox)gvchallan.Rows[ino].FindControl("txtqtyrec")).Enabled = true;
                        ((TextBox)gvchallan.Rows[ino].FindControl("txtbagsrec")).Enabled = true;
                        ((TextBox)gvchallan.Rows[ino].FindControl("txtwcmno")).Enabled = true;
                        ((TextBox)gvchallan.Rows[ino].FindControl("txtmoisture")).Enabled = true;

                    }


                    lblbagstotal.Text = _issedBags.ToString();
                    lblqtytotal.Text = _issedwt.ToString();
                }
                else
                {
                 //   Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please enter bags/bags to be receivied'); </script> ");

                }
            }
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
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in btnAddStack_Click has occured, try again'); </script> ");
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
    protected void fillStack()
    {
        try
        {
            string query = "";
            ddlStackNo.Items.Clear();
            
                query = "SELECT Stack_ID, Stack_Name FROM tbl_MetaData_STACK WHERE (Godown_ID = '" + ddlGodownNo.SelectedValue + "' and Commodity_Id = '" + Session["commodityid"].ToString() + "' and Stack_Killed = 'N' ) order by Stack_Name";
           
           
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

    protected void ddlStackNo_PreRender(object sender, EventArgs e)
    {
        ddlStackNo_SelectedIndexChanged(sender, e);
    }
    private ArrayList GetCropYear()
    {
        ArrayList arr = new ArrayList();
        arr.Add(new ListItem((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2), (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2)));
        arr.Add(new ListItem((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2), (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2)));
        arr.Add(new ListItem((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2), (DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2)));
        arr.Add(new ListItem((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2), (DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2)));
        arr.Add(new ListItem((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2), (DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2)));
        arr.Add(new ListItem((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2), (DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2)));
        arr.Add(new ListItem((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2), (DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2)));
        return arr;
    }
    private void FillDropDownList(DropDownList ddl)
    {
        ArrayList arr = GetCropYear();
        foreach (ListItem item in arr)
        {
            ddl.Items.Add(item);
        }
        ddl.SelectedIndex = 1;
    }
    protected void FillFCIOTDeoptData()
    {
        try
        {
            //string Dist_id = Session["WLC_Dep_Dist_Id"].ToString();
            //Dist_id = Dist_id.Substring(2, 2);
            string datedeposit = Session["Dateofdeposit"].ToString();

            string query = "";
            string s = Session["CropYear"].ToString();
            if (Session["WLCDepSource"].ToString() == "16")
            {
                query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,(SELECT [Commodity_Name] FROM [tbl_MetaData_STORAGE_COMMODITY] where Commodity_Id=Rcpt.Commodity) as comm ,Category,Recd_Qty,No_of_Bags,A_Depo,A_Dist,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Transporter,Ds.District_Name,'NA' as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.tbl_MetaData_DISTRICT as Ds on '23'+Rcpt.Dist_Id=Ds.District_Id  where  Rcpt.Depot_ID= '" + Session["Depot_DepotID"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "' and Rcpt.arrival_date='" + getDate_MDY(datedeposit) + "' and Rcpt.Commodity='" + Session["commodityid"].ToString() + "' ";
            }
            else if (Session["WLCDepSource"].ToString() == "--Select--" && Session["CropYear"].ToString()=="2020-21")
            {
                Session["WLCDepSource"] = "05";
                query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,(SELECT [Commodity_Name] FROM [tbl_MetaData_STORAGE_COMMODITY] where Commodity_Id=Rcpt.Commodity) as comm ,Category,Recd_Qty,No_of_Bags,A_Depo,A_Dist,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Transporter,Ds.District_Name,'NA' as 'DepotName' FROM CSMS.dbo.[tbl_Receipt_Details_2019] as Rcpt join MPSCSC.dbo.tbl_MetaData_DISTRICT as Ds on '23'+Rcpt.A_Dist=Ds.District_Id  where  Rcpt.Depot_ID= '" + Session["Depot_DepotID"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "' and Rcpt.arrival_date='" + getDate_MDY(datedeposit) + "' and Rcpt.Commodity='" + Session["commodityid"].ToString() + "' and Rcpt.Branch='" + Session["BranchId"].ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Session["Depot_DistID"].ToString() + "' and AST.Depotid='" + Session["Depot_DepotID"].ToString() + "' and challan_no is not null AND AST.Source_of_Arrival ='" + Session["WLCDepSource"].ToString() + "')  and Rcpt.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Session["Depot_DistID"].ToString() + "' and AST.Depotid='" + Session["Depot_DepotID"].ToString() + "' AND AST.Source_of_Arrival ='" + Session["WLCDepSource"].ToString() + "' and Rcpt.Vehile_no!=AST.Truck_No)";
            }
            else if (Session["WLCDepSource"].ToString() == "--Select--" && Session["CropYear"].ToString() != "2020-21")
            {
                query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,(SELECT [Commodity_Name] FROM [tbl_MetaData_STORAGE_COMMODITY] where Commodity_Id=Rcpt.Commodity) as comm ,Category,Recd_Qty,No_of_Bags,A_Depo,A_Dist,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Transporter,Ds.District_Name,'NA' as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.tbl_MetaData_DISTRICT as Ds on '23'+Rcpt.A_Dist=Ds.District_Id  where  Rcpt.Depot_ID= '" + Session["Depot_DepotID"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "' and Rcpt.arrival_date='" + getDate_MDY(datedeposit) + "' and Rcpt.Commodity='" + Session["commodityid"].ToString() + "' and Rcpt.Branch='" + Session["BranchId"].ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Session["Depot_DistID"].ToString() + "' and AST.Depotid='" + Session["Depot_DepotID"].ToString() + "' and challan_no is not null AND AST.Source_of_Arrival ='" + Session["WLCDepSource"].ToString() + "')  and Rcpt.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Session["Depot_DistID"].ToString() + "' and AST.Depotid='" + Session["Depot_DepotID"].ToString() + "' AND AST.Source_of_Arrival ='" + Session["WLCDepSource"].ToString() + "' and Rcpt.Vehile_no!=AST.Truck_No)";
            }
            else
            { 
            //ori 1  query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,(SELECT [Commodity_Name] FROM [tbl_MetaData_STORAGE_COMMODITY] where Commodity_Id=Rcpt.Commodity) as comm ,Category,Recd_Qty,No_of_Bags,A_Depo,A_Dist,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Transporter,Ds.District_Name,Dp.DepotName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.tbl_MetaData_DISTRICT as Ds on '23'+Rcpt.A_Dist=Ds.District_Id join MPSCSC.dbo.tbl_MetaData_DEPOT as Dp on  Rcpt.A_Depo=Dp.DepotID where  Rcpt.Depot_ID= '" + Session["Depot_DepotID"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "' and Rcpt.arrival_date='" + getDate_MDY(datedeposit) + "' and Rcpt.challan_no not in (select Challan_No from dbo.tbl_Storage_Arrival_Stock where BranchID='" + Session["Depot_DepotID"].ToString() + "' and Source_of_Arrival='" + Session["WLCDepSource"].ToString() + "')";
            //query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,(SELECT [Commodity_Name] FROM [tbl_MetaData_STORAGE_COMMODITY] where Commodity_Id=Rcpt.Commodity) as comm ,Category,Recd_Qty,No_of_Bags,A_Depo,A_Dist,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Transporter,Ds.District_Name,'NA' as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.tbl_MetaData_DISTRICT as Ds on '23'+Rcpt.A_Dist=Ds.District_Id  where  Rcpt.Depot_ID= '" + Session["Depot_DepotID"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "' and Rcpt.arrival_date='" + getDate_MDY(datedeposit) + "' and Rcpt.Commodity='" + Session["commodityid"].ToString() + "' ";
            query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,(SELECT [Commodity_Name] FROM [tbl_MetaData_STORAGE_COMMODITY] where Commodity_Id=Rcpt.Commodity) as comm ,Category,Recd_Qty,No_of_Bags,A_Depo,A_Dist,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Transporter,Ds.District_Name,'NA' as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details_2019] as Rcpt join MPSCSC.dbo.tbl_MetaData_DISTRICT as Ds on '23'+Rcpt.A_Dist=Ds.District_Id  where  Rcpt.Depot_ID= '" + Session["Depot_DepotID"].ToString() + "' and Rcpt.S_of_Arrival = '05' and Rcpt.arrival_date='" + getDate_MDY(datedeposit) + "' and Rcpt.Commodity='" + Session["commodityid"].ToString() + "' and Rcpt.Branch='" + Session["BranchId"].ToString() + "' and challan_no not in (select Challan_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Session["Depot_DistID"].ToString() + "' and AST.Depotid='" + Session["Depot_DepotID"].ToString() + "' and challan_no is not null AND AST.Source_of_Arrival ='05')  and Rcpt.Vehile_no NOT IN (select Truck_No from tbl_Storage_Arrival_Stock as AST where AST.District_Id='" + Session["Depot_DistID"].ToString() + "' and AST.Depotid='" + Session["Depot_DepotID"].ToString() + "' AND AST.Source_of_Arrival ='05' and Rcpt.Vehile_no!=AST.Truck_No)";
            }

            // query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,No_of_Bags,A_Depo,A_Dist,Transporter,Ds.District_Name,Dp.DepotName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.tbl_MetaData_DISTRICT as Ds on '23'+Rcpt.A_Dist=Ds.District_Id join MPSCSC.dbo.tbl_MetaData_DEPOT as Dp on  Rcpt.A_Depo=Dp.DepotID where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";


            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvchallan.DataSource = ds.Tables[0];
                gvchallan.DataBind();
                gvchallan.HeaderRow.Cells[13].Visible = false;
                gvchallan.HeaderRow.Cells[14].Visible = false;
                gvchallan.HeaderRow.Cells[15].Visible = false;
                gvchallan.HeaderRow.Cells[16].Visible = false;
                gvchallan.HeaderRow.Cells[17].Visible = false;
                gvchallan.HeaderRow.Cells[18].Visible = false;
                gvchallan.HeaderRow.Cells[19].Visible = false;
                gvchallan.HeaderRow.Cells[20].Visible = false;
                for (int i = 0; i < gvchallan.Rows.Count; i++)
                {

                    gvchallan.Rows[i].Cells[13].Visible = false;
                    gvchallan.Rows[i].Cells[14].Visible = false;
                    gvchallan.Rows[i].Cells[15].Visible = false;
                    gvchallan.Rows[i].Cells[16].Visible = false;
                    gvchallan.Rows[i].Cells[17].Visible = false;
                    gvchallan.Rows[i].Cells[18].Visible = false;
                    gvchallan.Rows[i].Cells[19].Visible = false;
                    gvchallan.Rows[i].Cells[20].Visible = false;

                    //Extract and Fill the DropDownList with Data
                    DropDownList ddl1 = (DropDownList)gvchallan.Rows[i].Cells[10].FindControl("ddlcropy");
                    FillDropDownList(ddl1);
                }
                lblcommodityname.Text = ds.Tables[0].Rows[0]["comm"].ToString();
                txtdateofdepo.Text = ds.Tables[0].Rows[0]["arrivaldate"].ToString();
                FillGodown();
                ddlGodownNo.SelectedValue = ds.Tables[0].Rows[0]["Godown"].ToString();
                fillStack();
               
               // ddlStackNo_SelectedIndexChanged(sender, e);
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Invalid Record'); </script> ");
                Response.Redirect("WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record!" + "");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('"+ex.Message+"'); </script> ");
        }
        finally
        {
            Con.Close();
        }
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
    protected void FillGodown()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                ddlGodownNo.Items.Clear();
                string query = "";

                if (Session["Mode"].ToString() == "NON-Edit")//Non MPSCSC Edit
                {
                    query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN] WHERE BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID in (select distinct Godown_ID from tbl_MetaData_STACK where BranchID ='" + Session["BranchId"].ToString() + "' and Commodity_Id='" + Session["commodityid"].ToString() + "')  ORDER BY [Godown_Name] ";
                }
                else
                {

                    if (Session["WLCDepSource"].ToString() == "NON-MPSCSC")
                    {
                        query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN] WHERE BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID in (select distinct Godown_ID from tbl_MetaData_STACK where BranchID ='" + Session["BranchId"].ToString() + "' and Commodity_Id='" + Session["commodityid"].ToString() + "')  ORDER BY [Godown_Name] ";
                    }
                    else
                    {
                        query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN] WHERE BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID in (select distinct Godown_ID from tbl_MetaData_STACK where BranchID ='" + Session["BranchId"].ToString() + "' and Commodity_Id='" + Session["commodityid"].ToString() + "')  ORDER BY [Godown_Name] ";
                    }
                }
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


    protected void btn_Close_Click(object sender, EventArgs e)
    {

    }
    protected void gvchallan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
            ////Replace your find corntrol code with this 
            //DropDownList drpnop = (DropDownList)e.Row.FindControl("dropdownnop");
            //drpnop.Items.Insert(0, "2015-16");
            //drpnop.Items.Insert(1, "2014-15");
            //drpnop.Items.Insert(2, "2013-14");
            //drpnop.Items.Insert(3, "2012-13");
            //drpnop.Items.Insert(4, "2011-12");
            //drpnop.Items.Insert(5, "2010-11");
            //drpnop.Items.Insert(6, "2009-10");
            //drpnop.Items.Insert(7, "Before 2009");

            //drpnop.SelectedIndex = 0;

        
    }
   
}
