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

public partial class Procurement_WLC_ProcStacking__NAFED_ANB : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataTable EditStack = new DataTable();
    DataTable EditStackNonMPSCSC = new DataTable();
    string receiptid = string.Empty;
    string ArrivalStockid = string.Empty;
    SqlCommand cmd = null;
    SqlTransaction sqltran;
    //string Depositor_Id = "";
    string Godown_Id_New = "";
    string DepositorNM = "";
    string DepositorId = "";
    string comid = "";
    decimal PRBags = 0;
    decimal PRQty = 0;
    string WLC_Depositor_No = "";
    string Commodity_Name = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CalendarExtender.EndDate = DateTime.Now;   //to dissable future  Date
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                if (Session["lang"].ToString() == "Hindi")
                {
                    //lblInstruction.Text = Resources.hindi.lblInstruction1;
                    lblDepositDetail.Text = Resources.hindi.lblSourceOfDeposit;
                    //lblAcceptanceNote.Text = Resources.hindi.lblAcceptanceNote;
                    //lblSourcesociety.Text = Resources.hindi.lblSourcesociety;
                    //lblProcTCNo.Text = Resources.hindi.lblProcTCNo;
                    //lblProcTruckNo.Text = Resources.hindi.lblProcTruckNo;
                    lblProcCommodity.Text = Resources.hindi.lblCommodity;
                    //lblProcMoisture.Text = Resources.hindi.lblMoisture;
                    lblProcDepostiDate.Text = Resources.hindi.lblDepositDate;
                    lblProcBags.Text = Resources.hindi.lblBagNumber;
                    lblProcQtyDeposit.Text = Resources.hindi.lblQtyDeposit;
                    lblProcWeigmentMode.Text = Resources.hindi.lblweigmentMode;
                    lblProcWCMNo.Text = Resources.hindi.lblWCMNo;
                    lblGodownNo.Text = Resources.hindi.lblGodownNo;
                    lblStackNo.Text = Resources.hindi.lblStackNo;
                    lblStackBags.Text = Resources.hindi.lblBagNumber;
                    lblStackWt.Text = Resources.hindi.lblStackWt;
                    lblStackMaxCap.Text = Resources.hindi.lblStackMaxCap;
                    lblStackCurrentCapacity.Text = Resources.hindi.lblStackCurrentCapacity;
                    lblStackAvailable.Text = Resources.hindi.lblStackAvailable;
                    btnAddStack.Text = Resources.hindi.btnAddStack;
                    lblStackingInform.Text = Resources.hindi.lblStackingInform;
                    lblRemarks.Text = Resources.hindi.lblRemarks;
                    lblProcBagsAcceptable.Text = Resources.hindi.lblProcBagsAcceptable;
                    lblProcQtyAcceptable.Text = Resources.hindi.lblProcQtyAcceptable;

                }
                string var = ClientScript.GetPostBackEventReference(btnsave, "").ToString();
                btnsave.Attributes.Add("onClick", "javascript :if ( Page_ClientValidate() ){this.disabled=true; this.value='Please Wait...';" + var + "};");
                if (!IsPostBack)
                {
                    //for preventing duplicate record insert
                    //
                    string PopMsg = "";
                    PopMsg = Request.QueryString["PopMsg"];
                    if (PopMsg != null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + PopMsg + "')", true);
                    }
                    //Printcurrentdate();
                    fillCropYear();
                    //fillprocCommodity();
                    ViewState["ckstat"] = "Empty";
                    ViewState["ckEditstat"] = "Empty";
                    if (Session["WLC_StorageReceipt_Id"] == null)
                    {
                        if (Session["Mode"] != null)
                        {
                            if (Session["Mode"].ToString() == "Add")
                            {
                                if (Session["WLCDepSource"] != null)
                                {
                                    if (Session["whrreq"] == "OldProc")
                                    {
                                        Session["dt1"] = null;
                                        //FillProcData();
                                    }
                                    else
                                    {
                                        Session["dt1"] = null;
                                        FillProcRabi2020();

                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid Record access, try again!')", true);
                                }
                                btnUpdate.Visible = false;
                                btnsave.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        if (Session["Mode"] != null)
                        {
                            if (Session["Mode"].ToString() == "Edit")
                            {
                                btnUpdate.Visible = true;
                                btnsave.Visible = false;
                                gdEditStackingDetails.Visible = true;
                                gdstackingdetails.Visible = false;
                                if (!IsPostBack)
                                {
                                    FillDefaultsForUpdate();
                                }
                            }
                            else if (Session["Mode"].ToString() == "Add")
                            {
                                btnUpdate.Visible = false;
                                btnsave.Visible = true;
                                gdEditStackingDetails.Visible = false;
                                gdstackingdetails.Visible = true;
                                Session["EditStack"] = null;
                            }
                        }
                    }
                }
                else
                {
                    Session.Remove("DType");
                    Session.Remove("Did");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error has occurred(page Load) , try again!')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void Insert_WLCQC()
    {
        string DepositorNo = Session["whrreq"].ToString();
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
        if (Session["Depot_DistID"] != null && Session["ProcComm"] != null)
        {
            string query2 = "";
            if ((Session["ProcComm"].ToString() == "8" || Session["ProcComm"].ToString() == "11"))
            {
                query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Prc.Book_No FROM MPSCSC.dbo.[Acceptance_Note_CoarseGrain2018] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and Prc.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
            }
            else if ((Session["ProcComm"].ToString() == "13" || Session["ProcComm"].ToString() == "14"))
            {
                query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Prc.Book_No FROM MPSCSC.dbo.[Acceptance_Note_Kharif2018] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and Prc.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
            }
            else
            {
                query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Prc.Book_No FROM MPSCSC.dbo.[Acceptance_Note_Dalhan2018] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and Prc.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
            }

            SqlCommand cmd2 = new SqlCommand(query2, Con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            if (ds2.Tables[0].Rows.Count >= 1)
            {
                for (int z = 0; z < ds2.Tables[0].Rows.Count; z++)
                {
                    string StorageReceipt_Id = receiptid;
                    string Distt_ID = ds2.Tables[0].Rows[z]["Distt_ID"].ToString();
                    string IssueCenter_ID = ds2.Tables[0].Rows[z]["IssueCenter_ID"].ToString();
                    string Purchase_Center = ds2.Tables[0].Rows[z]["Purchase_Center"].ToString();
                    string Dispatch_Date = ds2.Tables[0].Rows[z]["Dispatch_Date"].ToString();
                    string TC_Number = ds2.Tables[0].Rows[z]["TC_Number"].ToString();
                    string Truck_Number = ds2.Tables[0].Rows[z]["Truck_No"].ToString();
                    string Commodity_Id = ddlProcCommodity.SelectedValue.ToString();
                    string Crop_Year = ds2.Tables[0].Rows[z]["CropYear"].ToString();
                    int No_of_Bags = Convert.ToInt32(ds2.Tables[0].Rows[z]["Recd_Bags"]);
                    string Acceptance_No = ds2.Tables[0].Rows[z]["Acceptance_No"].ToString();
                    string Acceptance_Date = ds2.Tables[0].Rows[z]["Acceptance_Date"].ToString();
                    string Godown = ds2.Tables[0].Rows[z]["godown"].ToString();
                    string IssueId = ds2.Tables[0].Rows[z]["IssueID"].ToString();
                    string Branch_Id = ds2.Tables[0].Rows[z]["Branch_Id"].ToString();
                    string TaulParchi = ds2.Tables[0].Rows[z]["TaulParchi"].ToString();
                    string Weighbridge_ID = ds2.Tables[0].Rows[z]["Weighbridge_ID"].ToString();
                    string Weighbridge_TaulParchi = ds2.Tables[0].Rows[z]["Weighbridge_TaulParchi"].ToString();
                    float Weighbridge_Qty = Convert.ToSingle(ds2.Tables[0].Rows[z]["Weighbridge_Qty"]);
                    float Rec_Qty = Convert.ToSingle(ds2.Tables[0].Rows[z]["Recd_Qty"]);
                    string Depositor_Form_No = ds2.Tables[0].Rows[z]["Depositor_Form_No"].ToString();
                    string Depositor_QC = ds2.Tables[0].Rows[z]["Book_No"].ToString();
                    //string Created_Date = "";
                    //string IP_Address = "";

                    if (Con.State == ConnectionState.Closed)
                    {
                        Con.Open();
                    }
                    string RQry = "";

                    //RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_WLCQC_Proc_Kharif2018]([Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Depositor_QC_Status,WLC_QC_Status) VALUES ('" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Depositor_QC + "','" + ddlWLCQC.Text + "')";
                    RQry = "INSERT INTO [Integrated_MP_STORAGE].[dbo].[tbl_WLCQC_Proc_Kharif2018]([Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Depositor_QC_Status,WLC_QC_Status,Rejected_Bags,Rejected_Qty) VALUES ('" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Depositor_QC + "','" + ddlWLCQC.Text + "','" + Convert.ToDecimal(txtProcBagsAcceptable.Text) + "','" + Convert.ToDecimal(txtProcQtyAcceptable.Text) + "')";

                    cmd = new SqlCommand(RQry, Con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Connection = Con;
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    Response.Redirect("../Procurement/WLC_ProcurementReceipt.aspx");
                    //Session["RefreshButton"] = "No";
                }
            }
        }
    }
    public void Insert_PartiallyRejection()
    {
        string DepositorNo = Session["whrreq"].ToString();
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
        if (Session["Depot_DistID"] != null && Session["ProcComm"] != null)
        {
            string query2 = "";
            if ((Session["ProcComm"].ToString() == "8" || Session["ProcComm"].ToString() == "11"))
            {
                query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Prc.Book_No FROM MPSCSC.dbo.[Acceptance_Note_CoarseGrain2018] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and Prc.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
            }
            else if ((Session["ProcComm"].ToString() == "13" || Session["ProcComm"].ToString() == "14"))
            {
                query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Prc.Book_No FROM MPSCSC.dbo.[Acceptance_Note_Kharif2018] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and Prc.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
            }
            else
            {
                query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Prc.Book_No FROM MPSCSC.dbo.[Acceptance_Note_Dalhan2018] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and Prc.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
            }

            SqlCommand cmd2 = new SqlCommand(query2, Con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            if (ds2.Tables[0].Rows.Count >= 1)
            {
                for (int z = 0; z < ds2.Tables[0].Rows.Count; z++)
                {
                    string StorageReceipt_Id = receiptid;
                    string Distt_ID = ds2.Tables[0].Rows[z]["Distt_ID"].ToString();
                    string IssueCenter_ID = ds2.Tables[0].Rows[z]["IssueCenter_ID"].ToString();
                    string Purchase_Center = ds2.Tables[0].Rows[z]["Purchase_Center"].ToString();
                    string Dispatch_Date = ds2.Tables[0].Rows[z]["Dispatch_Date"].ToString();
                    string TC_Number = ds2.Tables[0].Rows[z]["TC_Number"].ToString();
                    string Truck_Number = ds2.Tables[0].Rows[z]["Truck_No"].ToString();
                    string Commodity_Id = ddlProcCommodity.SelectedValue.ToString();
                    string Crop_Year = ds2.Tables[0].Rows[z]["CropYear"].ToString();
                    int No_of_Bags = Convert.ToInt32(ds2.Tables[0].Rows[z]["Recd_Bags"]);
                    string Acceptance_No = ds2.Tables[0].Rows[z]["Acceptance_No"].ToString();
                    string Acceptance_Date = ds2.Tables[0].Rows[z]["Acceptance_Date"].ToString();
                    string Godown = ds2.Tables[0].Rows[z]["godown"].ToString();
                    string IssueId = ds2.Tables[0].Rows[z]["IssueID"].ToString();
                    string Branch_Id = ds2.Tables[0].Rows[z]["Branch_Id"].ToString();
                    string TaulParchi = ds2.Tables[0].Rows[z]["TaulParchi"].ToString();
                    string Weighbridge_ID = ds2.Tables[0].Rows[z]["Weighbridge_ID"].ToString();
                    string Weighbridge_TaulParchi = ds2.Tables[0].Rows[z]["Weighbridge_TaulParchi"].ToString();
                    float Weighbridge_Qty = Convert.ToSingle(ds2.Tables[0].Rows[z]["Weighbridge_Qty"]);
                    float Rec_Qty = Convert.ToSingle(ds2.Tables[0].Rows[z]["Recd_Qty"]);
                    string Depositor_Form_No = ds2.Tables[0].Rows[z]["Depositor_Form_No"].ToString();
                    string Depositor_QC = ds2.Tables[0].Rows[z]["Book_No"].ToString();
                    decimal RejBags = 0;
                    decimal RejQty = 0;
                    RejBags = Convert.ToDecimal(txtRejBags.Text);
                    RejQty = Convert.ToDecimal(txtRejQty.Text);

                    //string Created_Date = "";
                    //string IP_Address = "";

                    if (Con.State == ConnectionState.Closed)
                    {
                        Con.Open();
                    }
                    string RQry = "";

                    //RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_WLCQC_Proc_Kharif2018]([Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Depositor_QC_Status,WLC_QC_Status) VALUES ('" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Depositor_QC + "','" + ddlWLCQC.Text + "')";
                    RQry = "INSERT INTO [Integrated_MP_STORAGE].[dbo].[tbl_WLCQC_Proc_Kharif2018]([Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Depositor_QC_Status,WLC_QC_Status,Rejected_Bags,Rejected_Qty) VALUES ('" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Depositor_QC + "','" + ddlWLCQC.Text + "','" + RejBags + "','" + RejQty + "')";

                    cmd = new SqlCommand(RQry, Con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Connection = Con;
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    //Response.Redirect("../Procurement/WLC_ProcurementReceipt.aspx");
                    //Response.Redirect("../Procurement/WLC_DepositorForm_WHR.aspx");
                    //Session["RefreshButton"] = "No";
                }
            }
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        int Send_Bags = 0;
        decimal Send_Qty = 0;
        Send_Bags = Convert.ToInt32(Session["SendBags"]);
        Send_Qty = Convert.ToDecimal(Session["SendQty"]);
        string GHired_Type = FillGodown_Type();
        string GLicence_Validity = GetGodown_Licence();
        if (GHired_Type == "Y" || GLicence_Validity == "Y")
        {
            if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
            {
                try
                {
                    DepositorId = lblDepositorIds.Text;
                    DepositorNM = lblDepositor.Text;

                    btnsave.Enabled = false;
                    string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
                    int _stackbags = 0;
                    decimal _stackwt = 0;
                    int l;
                    comid = lblCommodityId.Text;
                    if (Session["WLCDepSource"].ToString() == "01")//Procurement
                    {
                        if (Session["ProcComm"].ToString() == "" || Session["RecDepositor"].ToString() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid Depositor/Commodity')", true);
                            return;
                        }
                        if (gdstackingdetails.Rows.Count > 0)
                        {
                            for (l = 0; l < gdstackingdetails.Rows.Count; l++)
                            {
                                _stackbags = _stackbags + int.Parse(gdstackingdetails.Rows[l].Cells[5].Text.ToString());
                                _stackwt = _stackwt + decimal.Parse(gdstackingdetails.Rows[l].Cells[6].Text.ToString());
                            }
                            if (Convert.ToInt32(txtProcBagsAcceptable.Text.ToString()) != Convert.ToInt32(_stackbags.ToString()) || decimal.Parse(txtProcQtyAcceptable.Text) != decimal.Parse(_stackwt.ToString()))
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Total Bags/Weight Recieved should be equal to sum of Stack Bags/Weight ')", true);
                            }
                            else
                            {
                                if (Con.State == ConnectionState.Closed)
                                {
                                    Con.Open();
                                }
                                //20052020 By A Start
                                qry = "select count(Acpt_FCIRO_No) from tbl_Storage_Receipt_Details where Acpt_FCIRO_No= '" + lblDF_Receipt_ID.Text.Trim() + "'";
                                cmd = new SqlCommand(qry, Con);
                                string aa1 = cmd.ExecuteScalar().ToString();
                                if (Convert.ToInt16(aa1) == 0)
                                {
                                    //20052020 By A END

                                    sqltran = Con.BeginTransaction();

                                    qry = "select count(ArrivalStock_Id) from tbl_Storage_Arrival_Stock where District_Id='" + Session["Depot_DistID"].ToString() + "' and BranchID = '" + Session["BranchId"].ToString() + "' and Commodity_Id = '" + ddlProcCommodity.SelectedValue.ToString() + "' and Challan_No = '" + txtProcTCNo.Text.Trim().ToString() + "' and Truck_No = '" + txtProcTruckNo.Text.Trim().ToString() + "' and Qty_No_of_Bags = '" + _stackbags + "' and Qty_Wt ='" + _stackwt + "' and AcceptanceNo = '" + txtAcceptanceNote.Text.Trim().ToString() + "'";
                                    cmd = new SqlCommand(qry, Con, sqltran); // check ArrivalStockid present in tbl_Storage_Arrival_Stock table
                                    string precount = cmd.ExecuteScalar().ToString();
                                    if (Convert.ToInt16(precount) == 0)
                                    {
                                        qry = "select isnull(Max(ArrivalStock_Id),0) from tbl_Storage_Arrival_Stock where District_Id='" + Session["Depot_DistID"].ToString() + "' and BranchID='" + Session["BranchId"].ToString() + "' ";
                                        cmd = new SqlCommand(qry, Con, sqltran); // check ArrivalStockid present in tbl_Storage_Arrival_Stock table
                                        string str3 = cmd.ExecuteScalar().ToString();
                                        if (Convert.ToInt64(str3) != 0)
                                        {
                                            ArrivalStockid = Convert.ToString(Convert.ToInt64(str3) + 1);
                                            if (ArrivalStockid != String.Empty || ArrivalStockid != "")
                                            {
                                            Found:
                                                qry = "select count(ArrivalStock_Id) from tbl_Storage_Arrival_Stock where ArrivalStock_Id='" + ArrivalStockid + "'";
                                                cmd = new SqlCommand(qry, Con, sqltran); // check ArrivalStockid present in tbl_Storage_Arrival_Stock table
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
                                        cmd = new SqlCommand("MPWLC_sp_MomentChallan_Reciept_insert_Proc", Con, sqltran);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@ArrivalStockId", ArrivalStockid);
                                        cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                                        cmd.Parameters.AddWithValue("@DepotId", Session["Depot_DepotID"].ToString());
                                        cmd.Parameters.AddWithValue("@DepositDate", getDate_MDY(Convert.ToString(txtProcDepostiDate.Text.Trim())));
                                        //cmd.Parameters.AddWithValue("@Commodity_Id", ddlProcCommodity.SelectedValue.ToString());
                                        cmd.Parameters.AddWithValue("@Commodity_Id", lblCommodityId.Text.ToString());
                                        //cmd.Parameters.AddWithValue("@Mode_of_weighment", ddlProcWeigmentMode.SelectedValue.ToString());
                                        cmd.Parameters.AddWithValue("@Mode_of_weighment", "");
                                        //cmd.Parameters.AddWithValue("@AcceptanceNo", txtAcceptanceNote.Text);
                                        cmd.Parameters.AddWithValue("@AcceptanceNo", WLC_Depositor_No);
                                        cmd.Parameters.AddWithValue("@PurchasCentre", "DF");
                                        cmd.Parameters.AddWithValue("@IssueID", "NA");
                                        cmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());
                                        if (txtProcBags.Text.Trim().ToString() == "")
                                        {
                                            cmd.Parameters.AddWithValue("@Qty_No_of_Bags", _stackbags);
                                        }
                                        else
                                        {
                                            //cmd.Parameters.AddWithValue("@Qty_No_of_Bags", Convert.ToInt64(txtProcBags.Text.ToString()));
                                            cmd.Parameters.AddWithValue("@Qty_No_of_Bags", Convert.ToInt64(Send_Bags));
                                        }
                                        if (txtProcQtyDeposit.Text.Trim().ToString() == "")
                                        {
                                            cmd.Parameters.AddWithValue("@Qty_Wt", _stackwt);
                                        }
                                        else
                                        {
                                            //cmd.Parameters.AddWithValue("@Qty_Wt", Convert.ToDecimal(txtProcQtyDeposit.Text.Trim().ToString()));
                                            cmd.Parameters.AddWithValue("@Qty_Wt", Convert.ToDecimal(Send_Qty));
                                        }
                                        cmd.Parameters.AddWithValue("@Category_Id", "1");

                                        cmd.Parameters.AddWithValue("@Depositor_Name", DepositorNM.ToString());

                                        cmd.Parameters.AddWithValue("@DepositorType", "Institution");
                                        cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue.ToString());
                                        cmd.Parameters.AddWithValue("@Sender_District", "23" + hfSending_Dist.Value);
                                        cmd.Parameters.AddWithValue("@Sender_Godown", ddlSourceS.SelectedValue.ToString());
                                        //cmd.Parameters.AddWithValue("@Challan_No", "NA");
                                        //cmd.Parameters.AddWithValue("@Truck_No", "NA");
                                        //cmd.Parameters.AddWithValue("@Challan_No", txtProcTCNo.Text.ToString());
                                        //cmd.Parameters.AddWithValue("@Truck_No", txtProcTruckNo.Text.ToString());
                                        cmd.Parameters.AddWithValue("@Challan_No", DBNull.Value);
                                        cmd.Parameters.AddWithValue("@Truck_No", DBNull.Value);
                                        cmd.Parameters.AddWithValue("@Source_of_Arrival", Session["WLCDepSource"].ToString());
                                        cmd.Parameters.AddWithValue("@ArrivalSource_ID", "01");
                                        if (txtProcMoisture.Text.ToString().Trim() == "")
                                        {
                                            cmd.Parameters.AddWithValue("@Quality_Moisture", DBNull.Value);
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@Quality_Moisture", Convert.ToDecimal(txtProcMoisture.Text.ToString()));
                                        }
                                        cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.ToString());
                                        cmd.Parameters.AddWithValue("@Scheme_ID", "0");
                                        //cmd.Parameters.AddWithValue("@Acpt_FCIRO_No", txtAcceptanceNote.Text.ToString());
                                        //cmd.Parameters.AddWithValue("@Acpt_FCIRO_Date", getDate_MDY(Convert.ToString(hfAcptDate.Value.Trim())));
                                        cmd.Parameters.AddWithValue("@Acpt_FCIRO_No", lblDF_Receipt_ID.Text);
                                        cmd.Parameters.AddWithValue("@Acpt_FCIRO_Date", getDate_MDY(Convert.ToString(hfAcptDate.Value.Trim())));
                                        cmd.Parameters.AddWithValue("@CreatedBy", ClientIP.ToString());
                                        cmd.Parameters.AddWithValue("@Client_IP", ClientIP.ToString());
                                        cmd.Parameters.AddWithValue("@Transporter_id", DBNull.Value);
                                        cmd.Parameters.AddWithValue("@Miller", DBNull.Value);
                                        if (txtProcWCMNo.Text.Trim() == "")
                                        {
                                            cmd.Parameters.AddWithValue("@WCMNo_Sending", DBNull.Value);
                                        }
                                        else
                                        {
                                            //cmd.Parameters.AddWithValue("@WCMNo_Sending", txtProcWCMNo.Text.Trim().ToString());
                                            cmd.Parameters.AddWithValue("@WCMNo_Sending", DBNull.Value);
                                        }
                                        if (txtProcBagsAcceptable.Text.Trim().ToString() == "")
                                        {
                                            cmd.Parameters.AddWithValue("@Acceptable_Bags", _stackbags);
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@Acceptable_Bags", Convert.ToInt64(txtProcBagsAcceptable.Text.ToString()));
                                        }
                                        if (txtProcQtyAcceptable.Text.Trim().ToString() == "")
                                        {
                                            cmd.Parameters.AddWithValue("@Acceptable_Wt", _stackwt);
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@Acceptable_Wt", Convert.ToDecimal(txtProcQtyAcceptable.Text.Trim().ToString()));
                                        }
                                        //new
                                        //cmd.Parameters.AddWithValue("@Ack_Book_No", lblBook_No.Text.Trim().ToString());
                                        cmd.Parameters.AddWithValue("@Ack_Book_No", DBNull.Value);
                                        //
                                        cmd.Parameters.Add("@receiptid", SqlDbType.NVarChar, 20);
                                        cmd.Parameters["@receiptid"].Direction = ParameterDirection.Output;
                                        int res = cmd.ExecuteNonQuery();
                                        receiptid = cmd.Parameters["@receiptid"].Value.ToString();
                                        if (res > 0)
                                        {
                                            for (int j = 0; j < gdstackingdetails.Rows.Count; j++)
                                            {
                                                if (Con.State == ConnectionState.Closed)
                                                {
                                                    Con.Open();
                                                }
                                                cmd = new SqlCommand("MPWLC_sp_stackingdetails_insert", Con, sqltran);
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                cmd.Parameters.AddWithValue("@receiptid", receiptid);
                                                cmd.Parameters.AddWithValue("@GodownId", gdstackingdetails.Rows[j].Cells[1].Text.ToString());
                                                cmd.Parameters.AddWithValue("@StackId", gdstackingdetails.Rows[j].Cells[2].Text.ToString());
                                                cmd.Parameters.AddWithValue("@SBags", int.Parse(gdstackingdetails.Rows[j].Cells[5].Text.ToString()));
                                                cmd.Parameters.AddWithValue("@SWeight", decimal.Parse(gdstackingdetails.Rows[j].Cells[6].Text.ToString()));
                                                cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                                                cmd.Parameters.AddWithValue("@DepotId", Session["Depot_DepotID"].ToString());
                                                cmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());
                                                cmd.Connection = Con;
                                                cmd.ExecuteNonQuery();
                                            }
                                            sqltran.Commit();
                                            Session["dt1"] = null;
                                            gdstackingdetails.DataSource = null;
                                            gdstackingdetails.DataBind();
                                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record saved successfully')", true);
                                            Session["ReceiptId"] = receiptid;
                                            Session["DepositorNo"] = Session["DF_Receipt_ID"].ToString();
                                            Session["GodownId"] = ddlGodownNo.SelectedValue.ToString();
                                            Session["DepositorName"] = DepositorNM;
                                            Session["DepositorId"] = DepositorId;
                                            Session["CommodityId"] = lblCommodityId.Text.ToString();
                                            Session["WLC_DFN"] = lblWLC_DFN.Text.ToString();
                                            Session["Crop_Year"] = ddlcropyear.SelectedValue;
                                            Response.Redirect("~/Procurement/WLC_ProcWHR_NAFED_ANB.aspx",false);
                                            Session["RefreshButton"] = "No";
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record is already saved..')", true);
                                    }
                                    //20052020 By A Start
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('WLC Depositor Form No. Allready Present...')", true);
                                }
                                //20052020 By A END
                            } ///here transactions ends

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please add Stack information first')", true);
                        }
                        //}
                    }
                    else
                    {
                        sqltran.Rollback();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in button save click')", true);
                    }
                    btnsave.Enabled = true;
                }
                catch (Exception ex)
                {
                    sqltran.Rollback();
                    lblmsg.Text = ex.ToString();
                }
                finally
                {
                    Con.Close();
                    //  sqltran.Dispose();
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please check Godown Licence No & Validity/Invalid Login')", true);
        }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenterLevel/Storage/WLC_Deposit_From.aspx");
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void ddlGodownNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillStack();
        ddlStackNo_SelectedIndexChanged(sender, e);
    }

    protected void fillStack()
    {
        try
        {
            string Commodity_ID = Session["ProcComm"].ToString();
            string query = "";
            ddlStackNo.Items.Clear();
            query = "SELECT Stack_ID, Stack_Name FROM tbl_MetaData_STACK WHERE (Godown_ID = '" + ddlGodownNo.SelectedValue + "' and Commodity_Id = '" + Commodity_ID + "' and Stack_Killed = 'N' ) order by Stack_Name";
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
                ddlStackNo.Items.Insert(0, " --Select--");
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in FillStack has occured, try again ')", true);
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
                // String query = "select tbl_MetaData_STACK.Stack_capacity,(select (isnull(a.wet,0) - isnull(b.wet2,0)) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details where Stack_ID = tbl_MetaData_STACK.Stack_ID) a,(select SUM(Bags_Weight) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = tbl_MetaData_STACK.Stack_ID AND GP.Status !='CANCEL') b) AS 'Current_Capacity' from tbl_MetaData_STACK  where BranchId='" + Session["BranchId"].ToString() + "' and tbl_MetaData_STACK.Stack_ID = '" + ddlStackNo.SelectedValue.ToString() + "'";
                String query = "select tbl_MetaData_STACK.Stack_capacity,(select (isnull(a.wet,0) - isnull(b.wet2,0)) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details where Stack_ID = tbl_MetaData_STACK.Stack_ID) a,(select SUM(Bags_Weight+loss-Gain) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = tbl_MetaData_STACK.Stack_ID AND GP.Status !='CANCEL') b) AS 'Current_Capacity' from tbl_MetaData_STACK  where BranchId='" + Session["BranchId"].ToString() + "' and tbl_MetaData_STACK.Stack_ID = '" + ddlStackNo.SelectedValue.ToString() + "'";
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
    protected void btnAddStack_Click(object sender, EventArgs e)
    {
        bool checkstatus = false;
        try
        {
            if (txtStackBags.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No of Bags to be added in Stack is required!')", true);
                return;
            }
            else if (txtStackWt.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Bags Weight to be added in Stack is required!')", true);
                return;
            }
            else if ((txtStackAvailable.Text != "") && (Convert.ToDecimal(txtStackAvailable.Text) < Convert.ToDecimal(txtStackWt.Text)))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Insuffient StackCapacity in the Selected Stack!')", true);
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
                        else if (Session["Mode"].ToString() == "NON-Edit")
                        {
                            //Edit Stack for NON MPSCSC
                            ADD_EditStockNonMPSCSC();
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
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Entry for this stack is already done')", true);
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
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No stack under the selected Commodity ,Category and Godown Number')", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in btnAddStack_Click has occured, try again')", true);
        }
    }
    //protected void btnAddStack_Click(object sender, EventArgs e)
    //{
    //    bool checkstatus = false;
    //    try
    //    {
    //        if (txtStackBags.Text == "")
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No of Bags to be added in Stack is required!')", true);
    //            return;
    //        }
    //        else if (txtStackWt.Text == "")
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Bags Weight to be added in Stack is required!')", true);
    //            return;
    //        }
    //        else if ((txtStackAvailable.Text != "") && (Convert.ToDecimal(txtStackAvailable.Text) < Convert.ToDecimal(txtStackWt.Text)))
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Insuffient StackCapacity in the Selected Stack!')", true);
    //            return;
    //        }
    //        else
    //        {
    //            if (Session["WLC_StorageReceipt_Id"] != null)
    //            {
    //                if (Session["Mode"] != null)
    //                {
    //                    if (Session["Mode"].ToString() == "Edit")
    //                    {
    //                        ADD_EditStock();
    //                    }
    //                    else if (Session["Mode"].ToString() == "NON-Edit")
    //                    {
    //                        //Edit Stack for NON MPSCSC
    //                        ADD_EditStockNonMPSCSC();
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                if (ddlStackNo.Items.Count > 0)
    //                {

    //                    btnsave.Enabled = true;
    //                    if (Session["dt1"] == null)
    //                    {
    //                        Dt1 = CreateTable();
    //                        Session["dt1"] = Dt1;
    //                    }
    //                    if (gdstackingdetails.Rows.Count == 0)
    //                    {                        // adding rows to the datatable
    //                        DataRow dr = ((DataTable)Session["dt1"]).NewRow();
    //                        ((DataTable)Session["dt1"]).AcceptChanges();
    //                        dr["Godownid"] = ddlGodownNo.SelectedValue;
    //                        dr["Stackid"] = ddlStackNo.SelectedValue;
    //                        dr["GodownName"] = ddlGodownNo.SelectedItem.Text;
    //                        dr["StackName"] = ddlStackNo.SelectedItem.Text;
    //                        dr["Bags"] = txtStackBags.Text.Trim();
    //                        dr["Weight"] = txtStackWt.Text.Trim();
    //                        if (gdstackingdetails.Rows.Count > 0)
    //                        {
    //                            int i;

    //                            // checking whether or not the stack is already added to the grid view
    //                            for (i = 0; i <= gdstackingdetails.Rows.Count - 1; i++)
    //                            {
    //                                string stackid = gdstackingdetails.Rows[i].Cells[2].Text.ToString();
    //                                string selectstackid = ddlStackNo.SelectedValue.ToString();
    //                                if (stackid == selectstackid)
    //                                {
    //                                    checkstatus = true;
    //                                }
    //                            }
    //                            if (checkstatus == false)
    //                            {
    //                                ((DataTable)Session["dt1"]).Rows.Add(dr);
    //                                ((DataTable)Session["dt1"]).AcceptChanges();
    //                                gdstackingdetails.DataSource = (DataTable)Session["dt1"];
    //                                gdstackingdetails.DataBind();
    //                                txtStackBags.Text = null;
    //                                txtStackWt.Text = null;
    //                                chksum();
    //                            }
    //                            else
    //                            {
    //                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Entry for this stack is already done')", true);
    //                            }
    //                        }
    //                        else
    //                        {
    //                            ((DataTable)Session["dt1"]).Rows.Add(dr);
    //                            ((DataTable)Session["dt1"]).AcceptChanges();
    //                            gdstackingdetails.DataSource = (DataTable)Session["dt1"];
    //                            gdstackingdetails.DataBind();
    //                            txtStackBags.Text = null;
    //                            txtStackWt.Text = null;
    //                            chksum();
    //                        }
    //                    }
    //                    else
    //                    {
    //                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No stack under the selected Commodity ,Category and Godown Number')", true);
    //                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आप एक से अधिक स्टेक मे WHR नहीं बना सकते है|')", true);
    //                    }
    //                }
    //                else
    //                {
    //                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No stack under the selected Commodity ,Category and Godown Number')", true);
    //                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आप एक से अधिक स्टेक मे WHR नहीं बना सकते है|')", true);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in btnAddStack_Click has occured, try again')", true);
    //    }
    //}

    protected void ADD_EditStock()
    {
        bool checkEditstatus = false;
        try
        {
            if (ddlStackNo.Items.Count > 0)
            {

                btnUpdate.Enabled = true;
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
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Entry for this stack is already done')", true);
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
                btnUpdate.Enabled = true;
                gdEditStackingDetails.Enabled = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No stack under the selected Commodity ,Category and Godown Number')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in Add_EDITStock has occured, try again')", true);
        }
    }

    protected void ADD_EditStockNonMPSCSC()
    {
        bool checkEditstatusNonMPCSCS = false;
        try
        {
            if (ddlStackNo.Items.Count > 0)
            {

                btnUpdate.Enabled = true;
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
                            checkEditstatusNonMPCSCS = true;
                        }
                    }
                    if (checkEditstatusNonMPCSCS == false)
                    {
                        ((DataTable)Session["EditStack"]).Rows.Add(dr);
                        ((DataTable)Session["EditStack"]).AcceptChanges();
                        gdEditStackingDetails.DataSource = (DataTable)Session["EditStack"];
                        gdEditStackingDetails.DataBind();
                        txtStackBags.Text = null;
                        txtStackWt.Text = null;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Entry for this stack is already done')", true);
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
                }
                btnUpdate.Enabled = true;
                gdEditStackingDetails.Enabled = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No stack under the selected Commodity ,Category and Godown Number')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in ADD_EditStockNonMPSCSC has occured, try again')", true);
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

    private DataTable CreateTableEditStackNonMPSCSC()
    {
        DataTable dtEditStackNonMPSCSC = new DataTable();//DataTable is created
        DataColumn Godownid = new DataColumn("Godownid", Type.GetType("System.String"));
        DataColumn Stackid = new DataColumn("Stackid", Type.GetType("System.String"));
        DataColumn GodownName = new DataColumn("GodownName", Type.GetType("System.String"));
        DataColumn StackName = new DataColumn("StackName", Type.GetType("System.String"));
        DataColumn Bags = new DataColumn("Bags", Type.GetType("System.Int32"));
        DataColumn Weight = new DataColumn("Weight", Type.GetType("System.Decimal"));
        dtEditStackNonMPSCSC.Columns.Add(Godownid);//Column is added to the DataTable
        dtEditStackNonMPSCSC.Columns.Add(Stackid);//Column is added to the DataTable
        dtEditStackNonMPSCSC.Columns.Add(GodownName);//Column is added to the DataTable
        dtEditStackNonMPSCSC.Columns.Add(StackName);//Column is added to the DataTable
        dtEditStackNonMPSCSC.Columns.Add(Bags);//Column is added to the DataTable
        dtEditStackNonMPSCSC.Columns.Add(Weight);//Column is added to the DataTable
        dtEditStackNonMPSCSC.AcceptChanges();
        return dtEditStackNonMPSCSC;
    }

    protected void gdEditStackingDetails_PreRender(object sender, EventArgs e)
    {
        int count = 0;
        count = gdEditStackingDetails.Rows.Count;
        if (count > 0)
        {
            btnUpdate.Enabled = true;
        }
        else
        {
            btnUpdate.Enabled = false;
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
            btnUpdate.Enabled = false;
            gdEditStackingDetails.Enabled = false;
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error gdEditStackingDetails_RowEditing has occured, try again'); </script> ");
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

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (Session["WLC_StorageReceipt_Id"] != null)
        {
            string Arrivalid = Session["WLC_StorageReceipt_Id"].ToString();
            if (Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }
            qry = "update [tbl_Storage_Arrival_Stock] set DepositDate='" + getDate_MDY(txtProcDepostiDate.Text.Trim().ToString()) + "',Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' where ArrivalStock_Id ='" + Arrivalid + "'";
            cmd = new SqlCommand(qry, Con);
            int z = cmd.ExecuteNonQuery();
            if (z > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Updated Successfully ')", true);
                Con.Close();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Updated')", true);
            }
        }
    }

    private void fillprocCommodity()
    {
        try
        {
            string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Qry_Order";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlProcCommodity.Items.Clear();
                ddlProcCommodity.DataSource = ds.Tables[0];
                ddlProcCommodity.DataTextField = "Commodity_Name";
                ddlProcCommodity.DataValueField = "Commodity_Id";
                ddlProcCommodity.DataBind();
            }
        }
        catch (Exception)
        {

            // throw;
        }
    }

    protected void FillProcData()
    {
        try
        {
            string query = "SELECT Purchase_Center,[Truck_No] AS Truck_Number,[TC_Number],[godown],[CommodityId],[Bags] AS Recd_Bags,[Accept_Qty] AS Recd_Qty,Acceptance_No,Convert(varchar(10),Acceptance_Date,103) as 'Acceptance_Date',[Sending_District],(select [Society_Name] From [MPSCSC].[dbo].[Society] AS SOC where SOC.Society_Id='" + Session["SocietyCode"].ToString() + "') AS Society_Name,[IssueID] FROM [mpscsc].[dbo].[Acceptance_Note_Detail] AS ADN WHERE Distt_ID='" + Session["WLC_Distt_ID"].ToString() + "' AND Purchase_Center='" + Session["SocietyCode"].ToString() + "' AND IssueCenter_ID='" + Session["WLC_IssueCenter_ID"].ToString() + "' AND TC_Number='" + Session["WLC_TC_Number"].ToString() + "' AND Truck_No='" + Session["TruckNo"] + "' AND IssueID='" + Session["IssueId"].ToString() + "'";
            //  string query = "SELECT Purchase_Center,[Truck_No] AS Truck_Number,[TC_Number],[godown],[CommodityId],[Bags] AS Recd_Bags,[Accept_Qty] AS Recd_Qty,Acceptance_No,Convert(varchar(10),Acceptance_Date,103) as 'Acceptance_Date',[Sending_District],(select [Society_Name] From  [MPSCSC].[dbo].[Society] AS SOC where SOC.Society_Id='" + Session["SocietyCode"].ToString() + "') AS Society_Name,[IssueID] FROM [mpscsc].[dbo].[Acceptance_Note_Detail] AS ADN WHERE Distt_ID='" + Session["WLC_Distt_ID"].ToString() + "' AND Purchase_Center='" + Session["SocietyCode"].ToString() + "' AND IssueCenter_ID='" + Session["WLC_IssueCenter_ID"].ToString() + "' AND TC_Number='" + Session["WLC_TC_Number"].ToString() + "' AND Truck_No='" + Session["TruckNo"] + "' AND IssueID='" + Session["IssueId"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count == 1)
            {
                txtProcTruckNo.Visible = true;
                lblProcTCNo.Visible = true;
                lblProcTruckNo.Visible = true;
                txtProcTCNo.Visible = true;
                lblSourcesociety.Visible = true;
                lblAcceptanceNote.Text = "Acceptance No.";
                lbl_societyname.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();
                ddlSourceS.Items.Add(new ListItem(ds.Tables[0].Rows[0][9].ToString(), ds.Tables[0].Rows[0][0].ToString()));
                txtProcTruckNo.Text = ds.Tables[0].Rows[0]["Truck_Number"].ToString();
                txtProcTruckNo.Enabled = false;
                txtProcTCNo.Text = ds.Tables[0].Rows[0]["TC_Number"].ToString();
                txtProcTCNo.Enabled = false;
                ddlProcCommodity.SelectedValue = ds.Tables[0].Rows[0]["CommodityId"].ToString();
                ddlProcCommodity.Enabled = false;
                txtProcQtyDeposit.Text = ds.Tables[0].Rows[0]["Recd_Qty"].ToString();
                txtProcQtyDeposit.Enabled = false;
                txtProcBags.Text = ds.Tables[0].Rows[0]["Recd_Bags"].ToString();
                txtProcBags.Enabled = false;

                txtProcBagsAcceptable.Text = ds.Tables[0].Rows[0]["Recd_Bags"].ToString();
                txtProcQtyAcceptable.Text = ds.Tables[0].Rows[0]["Recd_Qty"].ToString();
                //
                hfAcptDate.Value = ds.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                txtAcceptanceNote.Text = ds.Tables[0].Rows[0]["Acceptance_No"].ToString();
                txtAcceptanceNote.Enabled = false;
                hfSending_Dist.Value = ds.Tables[0].Rows[0]["sending_district"].ToString();
                FillGodown();
                string godownid = ds.Tables[0].Rows[0]["godown"].ToString();
                if (godownid != "")
                {
                    ddlGodownNo.SelectedValue = ds.Tables[0].Rows[0]["godown"].ToString();
                }


                fillStack();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid Record')", true);
                Response.Redirect("~/IssueCenterLevel/Storage/WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record!" + "");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in FillProcData has occured, try again')", true);
        }
    }
    //27-03-15
    protected void FillProcDataNew()
    {
        try
        {
            string DepoNo = Session["whrreq"].ToString();
            if (DepoNo.Substring(0, 2) == "18")
            {
                //string query2 = "SELECT distinct prc.IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.Bags) as Recd_Bags, sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Acceptance_Note_Wheat2017] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId inner join MPSCSC.dbo.SCSC_Procurement_Wheat2017 as sp on Prc.IssueID=sp.Receipt_Id and Prc.Acceptance_No=sp.Acceptance_No  where Prc.WHR_Request is not null  and  sp.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.WHR_Request='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' group by WHR_Request,prc.IssueCenter_ID,Prc.Acceptance_Date,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name,prc.godown";
                string query2 = "SELECT distinct prc.IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.Bags) as Recd_Bags, sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Acceptance_Note_Wheat2018] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId inner join MPSCSC.dbo.SCSC_Procurement_Wheat2018 as sp on Prc.IssueID=sp.Receipt_Id and Prc.Acceptance_No=sp.Acceptance_No  where Prc.WHR_Request is not null  and  sp.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.WHR_Request='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' group by WHR_Request,prc.IssueCenter_ID,Prc.Acceptance_Date,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name,prc.godown";

                SqlCommand cmd2 = new SqlCommand(query2, Con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                if (ds2.Tables[0].Rows.Count == 1)
                {
                    // lbl_societyname.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();
                    // ddlSourceS.Items.Add(new ListItem(ds.Tables[0].Rows[0][9].ToString(), ds.Tables[0].Rows[0][0].ToString()));
                    //txtProcTruckNo.Text = ds.Tables[0].Rows[0]["Truck_Number"].ToString();
                    lblSourcesociety.Visible = false;
                    lblAcceptanceNote.Text = "Depositor Form No:";
                    lblProcTCNo.Visible = true;
                    lblProcTruckNo.Visible = false;
                    txtProcTruckNo.Visible = false;
                    txtProcTCNo.Visible = true;
                    txtProcTCNo.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                    txtProcTCNo.Enabled = false;
                    ddlProcCommodity.SelectedValue = ds2.Tables[0].Rows[0]["CommodityId"].ToString();
                    ddlProcCommodity.Enabled = false;
                    txtProcQtyDeposit.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                    txtProcQtyDeposit.Enabled = false;
                    txtProcBags.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    txtProcBags.Enabled = false;

                    txtProcBagsAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    txtProcQtyAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                    //
                    hfAcptDate.Value = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                    txtAcceptanceNote.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                    txtAcceptanceNote.Enabled = false;
                    // hfSending_Dist.Value = ds.Tables[0].Rows[0]["sending_district"].ToString();
                    FillGodown();
                    string godownid = ds2.Tables[0].Rows[0]["godown"].ToString();
                    if (godownid != "")
                    {
                        ddlGodownNo.SelectedValue = ds2.Tables[0].Rows[0]["godown"].ToString();
                    }


                    fillStack();
                }
            }
            else
            {
                //06-05-without acceptancedate filter string query = "SELECT distinct prc.IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.Bags) as Recd_Bags,sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Acceptance_Note_Detail] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId where Prc.WHR_Request='" + Session["whrreq"].ToString() + "' group by WHR_Request,prc.IssueCenter_ID,Prc.Acceptance_Date,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name,prc.godown";
                //local string query = "SELECT distinct prc.IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.Bags) as Recd_Bags,sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Acceptance_Note_Detail] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId where Prc.WHR_Request='" + Session["whrreq"].ToString() + "' group by WHR_Request,prc.IssueCenter_ID,Prc.Acceptance_Date,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name,prc.godown";
                //19-05-15   string query = "SELECT distinct prc.IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.Bags) as Recd_Bags,sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Acceptance_Note_Detail] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId where Prc.WHR_Request='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' group by WHR_Request,prc.IssueCenter_ID,Prc.Acceptance_Date,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name,prc.godown";
                string query = "SELECT distinct prc.IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.Bags) as Recd_Bags, sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Acceptance_Note_Detail] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId inner join MPSCSC.dbo.SCSC_Procurement as sp on Prc.IssueID=sp.Receipt_Id and Prc.Acceptance_No=sp.Acceptance_No  where Prc.WHR_Request is not null  and  sp.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.WHR_Request='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' group by WHR_Request,prc.IssueCenter_ID,Prc.Acceptance_Date,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name,prc.godown";

                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    // lbl_societyname.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();
                    // ddlSourceS.Items.Add(new ListItem(ds.Tables[0].Rows[0][9].ToString(), ds.Tables[0].Rows[0][0].ToString()));
                    //txtProcTruckNo.Text = ds.Tables[0].Rows[0]["Truck_Number"].ToString();
                    lblSourcesociety.Visible = false;
                    lblAcceptanceNote.Text = "Depositor Form No:";
                    lblProcTCNo.Visible = true;
                    lblProcTruckNo.Visible = false;
                    txtProcTruckNo.Visible = false;
                    txtProcTCNo.Visible = true;
                    txtProcTCNo.Text = ds.Tables[0].Rows[0]["WHR_Request"].ToString();
                    txtProcTCNo.Enabled = false;
                    ddlProcCommodity.SelectedValue = ds.Tables[0].Rows[0]["CommodityId"].ToString();
                    ddlProcCommodity.Enabled = false;
                    txtProcQtyDeposit.Text = ds.Tables[0].Rows[0]["Recd_Qty"].ToString();
                    txtProcQtyDeposit.Enabled = false;
                    txtProcBags.Text = ds.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    txtProcBags.Enabled = false;

                    txtProcBagsAcceptable.Text = ds.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    txtProcQtyAcceptable.Text = ds.Tables[0].Rows[0]["Recd_Qty"].ToString();
                    //
                    hfAcptDate.Value = ds.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                    txtAcceptanceNote.Text = ds.Tables[0].Rows[0]["WHR_Request"].ToString();
                    txtAcceptanceNote.Enabled = false;
                    // hfSending_Dist.Value = ds.Tables[0].Rows[0]["sending_district"].ToString();
                    FillGodown();
                    string godownid = ds.Tables[0].Rows[0]["godown"].ToString();
                    if (godownid != "")
                    {
                        ddlGodownNo.SelectedValue = ds.Tables[0].Rows[0]["godown"].ToString();
                    }


                    fillStack();
                }
                else if (Session["Depot_DistID"] != null)
                {

                    string query2 = "SELECT distinct prc.IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.Bags) as Recd_Bags, sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Acceptance_Note_Detail2016] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId inner join MPSCSC.dbo.SCSC_Procurement2016 as sp on Prc.IssueID=sp.Receipt_Id and Prc.Acceptance_No=sp.Acceptance_No  where Prc.WHR_Request is not null  and  sp.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.WHR_Request='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' group by WHR_Request,prc.IssueCenter_ID,Prc.Acceptance_Date,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name,prc.godown";

                    SqlCommand cmd2 = new SqlCommand(query2, Con);
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2);
                    if (ds2.Tables[0].Rows.Count == 1)
                    {
                        // lbl_societyname.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();
                        // ddlSourceS.Items.Add(new ListItem(ds.Tables[0].Rows[0][9].ToString(), ds.Tables[0].Rows[0][0].ToString()));
                        //txtProcTruckNo.Text = ds.Tables[0].Rows[0]["Truck_Number"].ToString();
                        lblSourcesociety.Visible = false;
                        lblAcceptanceNote.Text = "Depositor Form No:";
                        lblProcTCNo.Visible = true;
                        lblProcTruckNo.Visible = false;
                        txtProcTruckNo.Visible = false;
                        txtProcTCNo.Visible = true;
                        txtProcTCNo.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                        txtProcTCNo.Enabled = false;
                        ddlProcCommodity.SelectedValue = ds2.Tables[0].Rows[0]["CommodityId"].ToString();
                        ddlProcCommodity.Enabled = false;
                        txtProcQtyDeposit.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                        txtProcQtyDeposit.Enabled = false;
                        txtProcBags.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                        txtProcBags.Enabled = false;

                        txtProcBagsAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                        txtProcQtyAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                        //
                        hfAcptDate.Value = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                        txtAcceptanceNote.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                        txtAcceptanceNote.Enabled = false;
                        // hfSending_Dist.Value = ds.Tables[0].Rows[0]["sending_district"].ToString();
                        FillGodown();
                        string godownid = ds2.Tables[0].Rows[0]["godown"].ToString();
                        if (godownid != "")
                        {
                            ddlGodownNo.SelectedValue = ds2.Tables[0].Rows[0]["godown"].ToString();
                        }


                        fillStack();
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid Record')", true);
                    Response.Redirect("~/IssueCenterLevel/Storage/WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record!" + "");

                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in FillProcData has occured, try again')", true);
        }
    }
    protected void FillProcDataNewCMS()
    {
        try
        {
            string DepoNo = Session["whrreq"].ToString();
            if (DepoNo.Substring(0, 2) == "18")
            {
                //string query2 = "SELECT distinct prc.IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.Bags) as Recd_Bags, sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Acceptance_Note_Wheat2018] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId inner join MPSCSC.dbo.SCSC_Procurement_Wheat2018 as sp on Prc.IssueID=sp.Receipt_Id and Prc.Acceptance_No=sp.Acceptance_No  where Prc.WHR_Request is not null  and  sp.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.WHR_Request='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' group by WHR_Request,prc.IssueCenter_ID,Prc.Acceptance_Date,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name,prc.godown";
                //string query2 = "SELECT distinct prc.IssueCenter_ID,convert(varchar(10),Prc.Created_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.Acpt_Bags) as Recd_Bags, sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_ReqGdn as WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.Final_DepositerForm_CSM2018 as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId inner join MPSCSC.dbo.SCSC_Procurement_CSM as sp on Prc.IssueID=sp.Receipt_Id and Prc.Acceptance_No=sp.Acceptance_No  where Prc.WHR_ReqGdn is not null  and  sp.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.WHR_ReqGdn='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Created_Date,103)='" + Session["AccepDate"].ToString() + "' group by WHR_ReqGdn,prc.IssueCenter_ID,Prc.Created_Date,Prc.CommodityId,Prc.WHR_ReqGdn,cm.Commodity_Name,prc.godown";
                string query2 = "SELECT distinct prc.IssueCenter_ID,convert(varchar(10),sp.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.Acpt_Bags) as Recd_Bags, sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_ReqGdn as WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.Final_DepositerForm_CSM2018 as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId inner join MPSCSC.dbo.SCSC_Procurement_CSM as sp on Prc.IssueID=sp.Receipt_Id and Prc.Acceptance_No=sp.Acceptance_No  where Prc.WHR_ReqGdn is not null  and  sp.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.WHR_ReqGdn='" + Session["whrreq"].ToString() + "' and convert(varchar(10),sp.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' group by WHR_ReqGdn,prc.IssueCenter_ID,sp.Acceptance_Date,Prc.CommodityId,Prc.WHR_ReqGdn,cm.Commodity_Name,prc.godown";

                SqlCommand cmd2 = new SqlCommand(query2, Con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                if (ds2.Tables[0].Rows.Count == 1)
                {
                    // lbl_societyname.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();
                    // ddlSourceS.Items.Add(new ListItem(ds.Tables[0].Rows[0][9].ToString(), ds.Tables[0].Rows[0][0].ToString()));
                    //txtProcTruckNo.Text = ds.Tables[0].Rows[0]["Truck_Number"].ToString();
                    lblSourcesociety.Visible = false;
                    lblAcceptanceNote.Text = "Depositor Form No:";
                    lblProcTCNo.Visible = true;
                    lblProcTruckNo.Visible = false;
                    txtProcTruckNo.Visible = false;
                    txtProcTCNo.Visible = true;
                    txtProcTCNo.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                    txtProcTCNo.Enabled = false;
                    ddlProcCommodity.SelectedValue = ds2.Tables[0].Rows[0]["CommodityId"].ToString();
                    ddlProcCommodity.Enabled = false;
                    txtProcQtyDeposit.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                    txtProcQtyDeposit.Enabled = false;
                    txtProcBags.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    txtProcBags.Enabled = false;

                    txtProcBagsAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    txtProcQtyAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                    //
                    hfAcptDate.Value = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                    txtAcceptanceNote.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                    txtAcceptanceNote.Enabled = false;
                    // hfSending_Dist.Value = ds.Tables[0].Rows[0]["sending_district"].ToString();
                    FillGodown();
                    string godownid = ds2.Tables[0].Rows[0]["godown"].ToString();
                    if (godownid != "")
                    {
                        ddlGodownNo.SelectedValue = ds2.Tables[0].Rows[0]["godown"].ToString();
                    }


                    fillStack();
                }
            }
            else
            {
                //06-05-without acceptancedate filter string query = "SELECT distinct prc.IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.Bags) as Recd_Bags,sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Acceptance_Note_Detail] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId where Prc.WHR_Request='" + Session["whrreq"].ToString() + "' group by WHR_Request,prc.IssueCenter_ID,Prc.Acceptance_Date,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name,prc.godown";
                //local string query = "SELECT distinct prc.IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.Bags) as Recd_Bags,sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Acceptance_Note_Detail] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId where Prc.WHR_Request='" + Session["whrreq"].ToString() + "' group by WHR_Request,prc.IssueCenter_ID,Prc.Acceptance_Date,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name,prc.godown";
                //19-05-15   string query = "SELECT distinct prc.IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.Bags) as Recd_Bags,sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Acceptance_Note_Detail] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId where Prc.WHR_Request='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' group by WHR_Request,prc.IssueCenter_ID,Prc.Acceptance_Date,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name,prc.godown";
                string query = "SELECT distinct prc.IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.Bags) as Recd_Bags, sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Acceptance_Note_Detail] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId inner join MPSCSC.dbo.SCSC_Procurement as sp on Prc.IssueID=sp.Receipt_Id and Prc.Acceptance_No=sp.Acceptance_No  where Prc.WHR_Request is not null  and  sp.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.WHR_Request='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' group by WHR_Request,prc.IssueCenter_ID,Prc.Acceptance_Date,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name,prc.godown";

                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    // lbl_societyname.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();
                    // ddlSourceS.Items.Add(new ListItem(ds.Tables[0].Rows[0][9].ToString(), ds.Tables[0].Rows[0][0].ToString()));
                    //txtProcTruckNo.Text = ds.Tables[0].Rows[0]["Truck_Number"].ToString();
                    lblSourcesociety.Visible = false;
                    lblAcceptanceNote.Text = "Depositor Form No:";
                    lblProcTCNo.Visible = true;
                    lblProcTruckNo.Visible = false;
                    txtProcTruckNo.Visible = false;
                    txtProcTCNo.Visible = true;
                    txtProcTCNo.Text = ds.Tables[0].Rows[0]["WHR_Request"].ToString();
                    txtProcTCNo.Enabled = false;
                    ddlProcCommodity.SelectedValue = ds.Tables[0].Rows[0]["CommodityId"].ToString();
                    ddlProcCommodity.Enabled = false;
                    txtProcQtyDeposit.Text = ds.Tables[0].Rows[0]["Recd_Qty"].ToString();
                    txtProcQtyDeposit.Enabled = false;
                    txtProcBags.Text = ds.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    txtProcBags.Enabled = false;

                    txtProcBagsAcceptable.Text = ds.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    txtProcQtyAcceptable.Text = ds.Tables[0].Rows[0]["Recd_Qty"].ToString();
                    //
                    hfAcptDate.Value = ds.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                    txtAcceptanceNote.Text = ds.Tables[0].Rows[0]["WHR_Request"].ToString();
                    txtAcceptanceNote.Enabled = false;
                    // hfSending_Dist.Value = ds.Tables[0].Rows[0]["sending_district"].ToString();
                    FillGodown();
                    string godownid = ds.Tables[0].Rows[0]["godown"].ToString();
                    if (godownid != "")
                    {
                        ddlGodownNo.SelectedValue = ds.Tables[0].Rows[0]["godown"].ToString();
                    }


                    fillStack();
                }
                else if (Session["Depot_DistID"] != null)
                {

                    string query2 = "SELECT distinct prc.IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.Bags) as Recd_Bags, sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Acceptance_Note_Detail2016] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId inner join MPSCSC.dbo.SCSC_Procurement2016 as sp on Prc.IssueID=sp.Receipt_Id and Prc.Acceptance_No=sp.Acceptance_No  where Prc.WHR_Request is not null  and  sp.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.WHR_Request='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' group by WHR_Request,prc.IssueCenter_ID,Prc.Acceptance_Date,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name,prc.godown";

                    SqlCommand cmd2 = new SqlCommand(query2, Con);
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2);
                    if (ds2.Tables[0].Rows.Count == 1)
                    {
                        // lbl_societyname.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();
                        // ddlSourceS.Items.Add(new ListItem(ds.Tables[0].Rows[0][9].ToString(), ds.Tables[0].Rows[0][0].ToString()));
                        //txtProcTruckNo.Text = ds.Tables[0].Rows[0]["Truck_Number"].ToString();
                        lblSourcesociety.Visible = false;
                        lblAcceptanceNote.Text = "Depositor Form No:";
                        lblProcTCNo.Visible = true;
                        lblProcTruckNo.Visible = false;
                        txtProcTruckNo.Visible = false;
                        txtProcTCNo.Visible = true;
                        txtProcTCNo.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                        txtProcTCNo.Enabled = false;
                        ddlProcCommodity.SelectedValue = ds2.Tables[0].Rows[0]["CommodityId"].ToString();
                        ddlProcCommodity.Enabled = false;
                        txtProcQtyDeposit.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                        txtProcQtyDeposit.Enabled = false;
                        txtProcBags.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                        txtProcBags.Enabled = false;

                        txtProcBagsAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                        txtProcQtyAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                        //
                        hfAcptDate.Value = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                        txtAcceptanceNote.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                        txtAcceptanceNote.Enabled = false;
                        // hfSending_Dist.Value = ds.Tables[0].Rows[0]["sending_district"].ToString();
                        FillGodown();
                        string godownid = ds2.Tables[0].Rows[0]["godown"].ToString();
                        if (godownid != "")
                        {
                            ddlGodownNo.SelectedValue = ds2.Tables[0].Rows[0]["godown"].ToString();
                        }


                        fillStack();
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid Record')", true);
                    Response.Redirect("~/IssueCenterLevel/Storage/WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record!" + "");

                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in FillProcData has occured, try again')", true);
        }
    }
    protected void FillProcKharif2016()
    {
        try
        {
            if (Session["Depot_DistID"] != null && Session["ProcComm"] != "22")
            {
                string query2 = "SELECT distinct prc.IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.Bags) as Recd_Bags, sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Acceptance_Note_Kharif2016] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId inner join MPSCSC.dbo.SCSC_Procurement_Kharif2016 as sp on Prc.IssueID=sp.Receipt_Id and Prc.Acceptance_No=sp.Acceptance_No  where Prc.WHR_Request is not null  and  sp.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.WHR_Request='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and Prc.CommodityId='" + Session["ProcComm"].ToString() + "' group by WHR_Request,prc.IssueCenter_ID,Prc.Acceptance_Date,Prc.CommodityId,Prc.WHR_Request,cm.Commodity_Name,prc.godown";

                SqlCommand cmd2 = new SqlCommand(query2, Con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                if (ds2.Tables[0].Rows.Count == 1)
                {
                    lblSourcesociety.Visible = false;
                    lblAcceptanceNote.Text = "Depositor Form No:";
                    lblProcTCNo.Visible = true;
                    lblProcTruckNo.Visible = false;
                    txtProcTruckNo.Visible = false;
                    txtProcTCNo.Visible = true;
                    txtProcTCNo.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                    txtProcTCNo.Enabled = false;
                    ddlProcCommodity.SelectedValue = ds2.Tables[0].Rows[0]["CommodityId"].ToString();
                    ddlProcCommodity.Enabled = false;
                    txtProcQtyDeposit.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                    txtProcQtyDeposit.Enabled = false;
                    txtProcBags.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    txtProcBags.Enabled = false;

                    txtProcBagsAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    txtProcQtyAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                    //
                    hfAcptDate.Value = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                    txtAcceptanceNote.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                    txtAcceptanceNote.Enabled = false;
                    // hfSending_Dist.Value = ds.Tables[0].Rows[0]["sending_district"].ToString();
                    FillGodown();
                    string godownid = ds2.Tables[0].Rows[0]["godown"].ToString();
                    if (godownid != "")
                    {
                        ddlGodownNo.SelectedValue = ds2.Tables[0].Rows[0]["godown"].ToString();
                    }
                    fillStack();
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid Record')", true);
                Response.Redirect("~/IssueCenterLevel/Storage/WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record!" + "");

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in FillProcData has occured, try again')", true);
        }
    }

    protected void FillProcKharif2017()
    {
        try
        {
            string RecDepositor = "";
            RecDepositor = Session["RecDepositor"].ToString();
            if (Session["Depot_DistID"] != null && Session["ProcComm"] != "22")
            {
                if (RecDepositor != "--Select--" && RecDepositor != "")
                {

                    string query2 = "";
                    if (RecDepositor == "10535" || RecDepositor == "129" || RecDepositor == "181" || RecDepositor == "4679")
                    {
                        ddlWLCQC.Enabled = true;
                        if (Session["ProcComm"].ToString() == "8" || Session["ProcComm"].ToString() == "11")
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Recd_Bags) as Recd_Bags,(AD.NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                        }
                        else if (Session["ProcComm"].ToString() == "13" || Session["ProcComm"].ToString() == "14")
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Recd_Bags) as Recd_Bags,(AD.NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                        }
                        else
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Recd_Bags) as Recd_Bags,(AD.NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                        }
                    }
                    else
                    {
                        ddlWLCQC.Enabled = false;
                        if (Session["ProcComm"].ToString() == "8" || Session["ProcComm"].ToString() == "11")
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                        }
                        else if (Session["ProcComm"].ToString() == "13" || Session["ProcComm"].ToString() == "14")
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                        }
                        else
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                        }
                    }
                    SqlCommand cmd2 = new SqlCommand(query2, Con);
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2);
                    if (ds2.Tables[0].Rows.Count == 1)
                    {
                        lblProcTruckNo.Visible = true;
                        txtProcTruckNo.Visible = true;
                        txtProcTCNo.Enabled = false;

                        txtProcTruckNo.Text = ds2.Tables[0].Rows[0]["Truck_Number"].ToString();
                        txtADno.Text = ds2.Tables[0].Rows[0]["Acceptance_No"].ToString();
                        txtProcDepostiDate.Text = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                        txtProcMoisture.Text = ds2.Tables[0].Rows[0]["Moisture"].ToString();
                        txtProcWCMNo.Text = ds2.Tables[0].Rows[0]["Weighbridge_TaulParchi"].ToString();
                        //txtRemarks.Text = "Stiching Bags:" + ds2.Tables[0].Rows[0]["Stiching_bags"].ToString() + ",Stencile Bags:" + ds2.Tables[0].Rows[0]["Stencile_bags"].ToString() + ",Bags Nottagged:" + ds2.Tables[0].Rows[0]["Bags_Nottagged"].ToString() + ",Bags No Color Code:" + ds2.Tables[0].Rows[0]["Bags_NotColorCode"].ToString();
                        lblBook_No.Text = ds2.Tables[0].Rows[0]["Book_No"].ToString();


                        lblSourcesociety.Visible = false;
                        lblAcceptanceNote.Text = "Depositor Form No:";
                        lblProcTCNo.Visible = true;
                        //lblProcTruckNo.Visible = false;
                        //txtProcTruckNo.Visible = false;
                        txtProcTCNo.Visible = true;
                        txtProcTCNo.Text = ds2.Tables[0].Rows[0]["TC_Number"].ToString();
                        txtProcTCNo.Enabled = false;
                        ddlProcCommodity.SelectedValue = ds2.Tables[0].Rows[0]["CommodityId"].ToString();
                        ddlProcCommodity.Enabled = false;
                        txtProcQtyDeposit.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                        txtProcQtyDeposit.Enabled = false;
                        txtProcBags.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                        txtProcBags.Enabled = false;

                        txtProcBagsAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                        txtProcQtyAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                        //
                        hfAcptDate.Value = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                        txtAcceptanceNote.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                        txtAcceptanceNote.Enabled = false;
                        // hfSending_Dist.Value = ds.Tables[0].Rows[0]["sending_district"].ToString();
                        Godown_Id_New = ds2.Tables[0].Rows[0]["godown"].ToString();
                        FillGodown();
                        string godownid = ds2.Tables[0].Rows[0]["godown"].ToString();

                        if (godownid != "")
                        {
                            ddlGodownNo.SelectedValue = ds2.Tables[0].Rows[0]["godown"].ToString();
                        }
                        fillStack();
                        FillDepositor();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please check Depositor')", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid Record')", true);
                Response.Redirect("~/IssueCenterLevel/Storage/WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record!" + "");

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in FillProcData has occured, try again')", true);
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
                //query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN_2018] WHERE BranchId = '" + Session["BranchId"].ToString() + "' and IsActive='Y' and Godown_ID in (select distinct Godown_ID from tbl_MetaData_STACK where BranchID ='" + Session["BranchId"].ToString() + "' and Commodity_Id='" + ddlProcCommodity.SelectedValue + "' and Godown_ID='')  ORDER BY [Godown_Name] "; 
                query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN_2018] WHERE BranchId = '" + Session["BranchId"].ToString() + "' and IsActive='Y' and Godown_ID='" + Godown_Id_New + "' ORDER BY [Godown_Name] ";

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
                    //ddlGodownNo.Items.Insert(0, " --select--");
                }
                else
                {
                    ddlGodownNo.DataSource = null;
                    ddlGodownNo.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in FillGodown has occurred , try again!')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public string FillGodown_Type()
    {
        string Hired_Type = "Y";
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                string query2 = "";
                query2 = "SELECT [Hired_Type] FROM [tbl_MetaData_GODOWN_2018] WHERE BranchId = '" + Session["BranchId"].ToString() + "' and IsActive='Y' and Godown_ID='" + ddlGodownNo.SelectedValue + "'";
                SqlCommand cmd2 = new SqlCommand(query2, Con);
                Con.Open();
                string HType = cmd2.ExecuteScalar().ToString();
                Con.Close();
                if (HType == "Joint Venture(JV)" || HType == "WDRA" || HType == "PVT.PEG" || HType == "Tribal Scheme")
                {
                    Hired_Type = "N";
                }
                else
                {
                    Hired_Type = "Y";
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in Fill Hired has occurred , try again!')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
        return Hired_Type;
    }
    public string GetGodown_Licence()
    {
        string IsLic_Valid = "Y";
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                string query2 = "";
                //query2 = "SELECT [Hired_Type] FROM [tbl_MetaData_GODOWN_2018] WHERE BranchId = '" + Session["BranchId"].ToString() + "' and IsActive='Y' and Godown_ID='" + ddlGodownNo.SelectedValue + "'";
                query2 = "select count(Godown_ID) as IsValid from tbl_MetaData_GODOWN_2018 where LicNum is not null and LicNum!='0' and LicNum!='' and LicDate>GETDATE() and BranchId = '" + Session["BranchId"].ToString() + "' and IsActive='Y' and Godown_ID='" + ddlGodownNo.SelectedValue + "'";

                SqlCommand cmd2 = new SqlCommand(query2, Con);
                Con.Open();
                string LType = cmd2.ExecuteScalar().ToString();
                Con.Close();
                if (LType == "0" || LType == "" || LType == null)
                {
                    IsLic_Valid = "N";
                }
                else
                {
                    IsLic_Valid = "Y";
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in Fill Licence has occurred , try again!')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
        return IsLic_Valid;
    }
    protected void FillDepositor()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                //string RecDepositor = "";
                //RecDepositor = Session["RecDepositor"].ToString();
                string District_Id = Session["Depot_DistID"].ToString();
                comid = Session["ProcComm"].ToString();
                ddlDepositorName.Items.Clear();
                string query = "";
                if ((comid == "8" || comid == "11"))
                {
                    query = "select distinct D.Depositor_Name,D.Depositor_ID from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as KR inner join tbl_MetaData_DEPOSITOR as D on D.LicNum=KR.Purchase_Center where KR.DepositerNo='" + Session["whrreq"].ToString() + "' and KR.Book_No='Rejected'";
                }
                else if ((comid == "13" || comid == "14"))
                {
                    query = "select distinct D.Depositor_Name,D.Depositor_ID from MPSCSC.dbo.Acceptance_Note_Kharif2018 as KR inner join tbl_MetaData_DEPOSITOR as D on D.LicNum=KR.Purchase_Center where KR.DepositerNo='" + Session["whrreq"].ToString() + "' and KR.Book_No='Rejected'";
                }
                else if (comid == "22")
                {
                    query = "select distinct D.Depositor_Name,D.Depositor_ID from MPSCSC.dbo.Acceptance_Note_Rabi2019 as KR inner join tbl_MetaData_DEPOSITOR as D on D.LicNum=KR.Purchase_Center where KR.DepositerNo='" + Session["whrreq"].ToString() + "' and KR.Book_No='Rejected'";
                }
                else
                {
                    query = "select distinct D.Depositor_Name,D.Depositor_ID from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as KR inner join tbl_MetaData_DEPOSITOR as D on D.LicNum=KR.Purchase_Center where KR.DepositerNo='" + Session["whrreq"].ToString() + "' and KR.Book_No='Rejected'";
                }

                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepositorName.DataSource = ds.Tables[0];
                    ddlDepositorName.DataTextField = "Depositor_Name";
                    ddlDepositorName.DataValueField = "Depositor_ID";
                    ddlDepositorName.DataBind();
                    //ddlGodownNo.Items.Insert(0, " --select--");
                    DepositorNM = ddlDepositorName.SelectedItem.Text;
                    DepositorId = ddlDepositorName.SelectedValue.ToString();
                    lblDepositorIds.Text = DepositorId;
                    lblDeositorNames.Text = DepositorNM;
                }
                else
                {
                    string query2 = "";
                    //query = "select S.Society_Name,KR.Purchase_Center from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as KR inner join MPSCSC.dbo.Society_Pulses18 as S on S.PCID=KR.Purchase_Center where KR.DepositerNo='" + Session["whrreq"].ToString() + "'";
                    query2 = "select ISMFD from tbl_MetaData_DISTRICT where District_Id='" + Session["Depot_DistID"].ToString() + "'";
                    //query2 = "select Wheat_Markfed from tbl_MetaData_DISTRICT where District_Id='" + Session["Depot_DistID"].ToString() + "'";

                    //cmd = new SqlCommand(qry, Con, sqltran); // check ArrivalStockid present in tbl_Storage_Arrival_Stock table
                    SqlCommand cmd2 = new SqlCommand(query2, Con);
                    Con.Open();
                    string ism = cmd2.ExecuteScalar().ToString();
                    Con.Close();
                    if (ism == "Y" && comid == "13" || comid == "14")
                    {
                        DepositorNM = "DMO Markfed";
                        DepositorId = "4679";
                    }
                    else if (ism == "N" && comid == "13" || comid == "14")
                    {
                        DepositorNM = "NAFED";
                        DepositorId = "10535";
                    }
                    else if (comid == "92" || comid == "27" || comid == "31" || comid == "65" || comid == "123")
                    {
                        if (District_Id == "2309" || District_Id == "2327")
                        {
                            DepositorNM = "FCI";
                            DepositorId = "181";
                        }
                        else if (District_Id == "2310" || District_Id == "2311")
                        {
                            DepositorNM = "MPSCSC";
                            DepositorId = "129";
                        }
                        else if (District_Id == "2333")
                        {
                            DepositorNM = "DMO Markfed";
                            DepositorId = "4679";
                        }
                        else
                        {
                            DepositorNM = "NAFED";
                            DepositorId = "10535";
                        }

                    }
                    else if (comid == "63" || comid == "64" || comid == "33" || comid == "52")
                    {
                        DepositorNM = "NAFED";
                        DepositorId = "10535";
                    }
                    else
                    {
                        DepositorNM = "MPSCSC";
                        DepositorId = "129";
                    }
                    lblDepositorIds.Text = DepositorId;
                    lblDeositorNames.Text = DepositorNM;

                    ListItem[] items = new ListItem[1];
                    items[0] = new ListItem(DepositorNM, DepositorId);
                    ddlDepositorName.SelectedIndex = 0;
                    ddlDepositorName.Items.AddRange(items);
                    ddlDepositorName.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in FillGodown has occurred , try again!')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void ChangedFillDepositor()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                //string RecDepositor = "";
                //RecDepositor = Session["RecDepositor"].ToString();
                string District_Id = Session["Depot_DistID"].ToString();
                comid = Session["ProcComm"].ToString();
                //ddlDepositorName.Items.Clear();
                string query = "";
                //string a = ddlDepositorName.SelectedItem.Text;
                if (ddlDepositorName.SelectedItem.Text == "MPSCSC")
                {
                    query = "select  'DMO Markfed' as Depositor_Name,'4679' as Depositor_ID";
                }
                else if (ddlDepositorName.SelectedItem.Text == "DMO Markfed")
                {
                    query = "select  'MPSCSC' as Depositor_Name,'129' as Depositor_ID";
                }
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepositorName.DataSource = ds.Tables[0];
                    ddlDepositorName.DataTextField = "Depositor_Name";
                    ddlDepositorName.DataValueField = "Depositor_ID";
                    ddlDepositorName.DataBind();
                    //ddlGodownNo.Items.Insert(0, " --select--");
                    DepositorNM = ddlDepositorName.SelectedItem.Text;
                    DepositorId = ddlDepositorName.SelectedValue.ToString();
                    lblDepositorIds.Text = DepositorId;
                    lblDeositorNames.Text = DepositorNM;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in FillGodown has occurred , try again!')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillCropYear()
    {
        ListItem[] items = new ListItem[7];
        items[0] = new ListItem((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2), (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString());
        items[1] = new ListItem((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2), (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString());
        items[2] = new ListItem((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2), (DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString());
        items[3] = new ListItem((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2), (DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString());
        items[4] = new ListItem((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2), (DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString());
        items[5] = new ListItem((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2), (DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString());
        items[6] = new ListItem((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2), (DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString());
        //ddlcropyear.Items.Insert(0, "All");
        //ddlcropyear.SelectedIndex = 1;
        ddlcropyear.SelectedIndex = 0;
        ddlcropyear.Items.AddRange(items);
        ddlcropyear.DataBind();
    }

    protected void Printcurrentdate()
    {
        string query = "SELECT  convert(varchar(10),getdate(),103) as 'Date1'";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtProcDepostiDate.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
        }
    }

    protected void FillDefaultsForUpdate()
    {
        try
        {
            string Arrivalid = Session["WLC_StorageReceipt_Id"].ToString();
            //string query = "select DISTINCT AST.ArrivalStock_Id,AST.Receipt_ID,AST.Commodity_Id,AST.Challan_No,AST.Truck_No,SRD.Acpt_FCIRO_No,convert(nvarchar(10),SRD.Acpt_FCIRO_Date,103) AS AcceptDate,(SRD.Qty_Rvd_No_of_Bags) AS Bags,CONVERT(DECIMAL(18,2),SRD.Qty_Rvd_Weight) AS Weight,AST.Qty_No_of_Bags,CONVERT(DECIMAL(18,2),AST.Qty_Wt) as Qty_Wt,convert(nvarchar(10),AST.DepositDate,103) AS DepositDate,AST.Remarks,AST.Quality_Moisture,AST.Crop_Year,SOC.Society_Name from tbl_Storage_Arrival_Stock as AST join tbl_Storage_Receipt_Details AS SRD on AST.Receipt_ID = SRD.StorageReceipt_Id join MPSCSC.dbo.Society AS SOC on AST.PurchasCentre = SOC.Society_Id where SRD.WHR_Flag='N' and SRD.WHR_Id is null AND AST.DepotId = '" + Session["Depot_DepotID"].ToString() + "' AND AST.District_Id = '" + Session["Depot_DistID"].ToString() + "' and AST.ArrivalStock_Id = '" + Arrivalid + "' ORDER BY AST.ArrivalStock_Id";
            string query = "select DISTINCT AST.ArrivalStock_Id,AST.Receipt_ID,AST.Commodity_Id,AST.Challan_No,AST.Truck_No,SRD.Acpt_FCIRO_No,convert(nvarchar(10),SRD.Acpt_FCIRO_Date,103) AS AcceptDate,(SRD.Qty_Rvd_No_of_Bags) AS Bags,CONVERT(DECIMAL(18,2),SRD.Qty_Rvd_Weight) AS Weight,AST.Qty_No_of_Bags,CONVERT(DECIMAL(18,2),AST.Qty_Wt) as Qty_Wt,convert(nvarchar(10),AST.DepositDate,103) AS DepositDate,AST.Remarks,AST.Quality_Moisture,AST.Crop_Year from tbl_Storage_Arrival_Stock as AST join tbl_Storage_Receipt_Details AS SRD on AST.Receipt_ID = SRD.StorageReceipt_Id where SRD.WHR_Flag='N' and SRD.WHR_Id is null AND AST.BranchId = '" + Session["BranchId"].ToString() + "' AND AST.District_Id = '" + Session["Depot_DistID"].ToString() + "' and AST.ArrivalStock_Id = '" + Arrivalid + "' ORDER BY AST.ArrivalStock_Id";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count == 1)
            {
                txtAcceptanceNote.Text = ds.Tables[0].Rows[0]["Acpt_FCIRO_No"].ToString();
                //lbl_societyname.Text = ds.Tables[0].Rows[0]["Society_Name"].ToString();
                txtProcTCNo.Text = ds.Tables[0].Rows[0]["Challan_No"].ToString();
                txtProcTCNo.Enabled = false;
                txtProcTruckNo.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                txtProcTruckNo.Enabled = false;
                ddlProcCommodity.SelectedValue = ds.Tables[0].Rows[0]["Commodity_Id"].ToString();
                ddlProcCommodity.Enabled = false;
                txtProcQtyAcceptable.Text = ds.Tables[0].Rows[0]["Weight"].ToString();
                txtProcBagsAcceptable.Text = ds.Tables[0].Rows[0]["Bags"].ToString();
                txtProcQtyDeposit.Text = ds.Tables[0].Rows[0]["Qty_Wt"].ToString();
                txtProcQtyDeposit.Enabled = false;
                txtProcBags.Text = ds.Tables[0].Rows[0]["Qty_No_of_Bags"].ToString();
                txtProcBags.Enabled = false;
                txtProcDepostiDate.Text = ds.Tables[0].Rows[0]["DepositDate"].ToString();
                txtProcDepostiDate.Enabled = true;
                txtRemarks.Text = ds.Tables[0].Rows[0]["Quality_Moisture"].ToString();
                ddlcropyear.SelectedItem.Text = ds.Tables[0].Rows[0]["Crop_Year"].ToString();
                ddlcropyear.Enabled = true;
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                FillGodown();
                fillStack();
                ddlGodownNo.Enabled = false;
                ddlStackNo.Enabled = false;

                if (ds.Tables[0].Rows[0]["Receipt_ID"].ToString() != "")
                {
                    string Receiptid = ds.Tables[0].Rows[0]["Receipt_ID"].ToString();
                    qry = "select ssd.Godown_ID as 'Godownid',ssd.Stack_ID as 'Stackid',Godown_Name as 'GodownName',Stack_Name as 'StackName',ssd.Bags,ssd.Weight FROM tbl_storage_Stacking_Details ssd join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID=ssd.Godown_ID join tbl_MetaData_STACK on tbl_MetaData_STACK.Stack_ID=ssd.Stack_ID where ssd.StorageReceipt_Id='" + Receiptid + "'";
                    cmd = new SqlCommand(qry, Con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        Session["EditStack"] = ds1.Tables[0];
                        gdEditStackingDetails.DataSource = (DataTable)Session["EditStack"];
                        gdEditStackingDetails.DataBind();
                    }
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Invalid Record, Can not be edited !'); </script> ");
                // Response.Redirect("Edit_WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record, Can not be edited!" + "");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error FillDefaultsforUpdate has occurred , try again!'); </script> ");
        }
        finally
        {
            Con.Close();
        }
    }

    //Onion insert
    protected void FillProcOnion()
    {
        try
        {
            string query2 = "";
            string DepoNo = Session["whrreq"].ToString();
            if (Session["ProcComm"] == "106")
            {
                //query2 = "SELECT distinct '' as IssueCenter_ID,convert(varchar(10),Prc.DepositerDate,103) as 'Acceptance_Date',prc.godown,sum(prc.Recd_Bags) as Recd_Bags, sum(Prc.Recd_Qty) AS Recd_Qty,Prc.Commodity as CommodityId,Prc.DepositerNumber as WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Onion_Depositer] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.Commodity where Prc.DepositerNumber is not null  and  Prc.branch='" + Session["BranchId"].ToString() + "' and Prc.DepositerNumber='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.DepositerDate,103)='" + Session["AccepDate"].ToString() + "' group by DepositerNumber,Prc.DepositerDate,Prc.Commodity,cm.Commodity_Name,prc.godown";
                query2 = "SELECT distinct '' as IssueCenter_ID,convert(varchar(10),Prc.DepositerDate,103) as 'Acceptance_Date',prc.godown,sum(prc.Recd_Bags) as Recd_Bags, sum(Prc.Recd_Qty) AS Recd_Qty,Prc.Commodity as CommodityId,Prc.DepositerNumber as WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.[Onion_Depositer] as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.Commodity where Prc.DepositerNumber is not null  and  Prc.branch='" + Session["BranchId"].ToString() + "' and Prc.DepositerNumber='" + Session["whrreq"].ToString() + "' and convert(varchar(10),Prc.DepositerDate,103)='" + Session["AccepDate"].ToString() + "' group by DepositerNumber,convert(varchar(10),Prc.DepositerDate,103),Prc.Commodity,cm.Commodity_Name,prc.godown";
            }
            else if (Session["ProcComm"] == "52")
            {
                query2 = "SELECT distinct IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.RecievedBags) as Recd_Bags, sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId as CommodityId,Prc.WHR_Request as WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.Pulse_Acceptance_Detail as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId where Prc.WHR_Request is not null and Prc.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.WHR_Request='" + Session["whrreq"].ToString() + "' and Prc.CommodityId='52' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' group by WHR_Request,Prc.Acceptance_Date,Prc.CommodityId,cm.Commodity_Name,prc.godown,IssueCenter_ID";
            }
            else if (Session["ProcComm"] == "92")
            {
                query2 = "SELECT distinct IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.RecievedBags) as Recd_Bags, sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId as CommodityId,Prc.WHR_Request as WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.Pulse_Acceptance_Detail as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId where Prc.WHR_Request is not null and Prc.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.WHR_Request='" + Session["whrreq"].ToString() + "' and Prc.CommodityId='92' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' group by WHR_Request,Prc.Acceptance_Date,Prc.CommodityId,cm.Commodity_Name,prc.godown,IssueCenter_ID";
            }
            else if (Session["ProcComm"] == "27")
            {
                query2 = "SELECT distinct IssueCenter_ID,convert(varchar(10),Prc.Acceptance_Date,103) as 'Acceptance_Date',prc.godown,sum(prc.RecievedBags) as Recd_Bags, sum(Prc.Accept_Qty) AS Recd_Qty,Prc.CommodityId as CommodityId,Prc.WHR_Request as WHR_Request,cm.Commodity_Name FROM MPSCSC.dbo.Pulse_Acceptance_Detail as Prc join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=Prc.CommodityId where Prc.WHR_Request is not null and Prc.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.WHR_Request='" + Session["whrreq"].ToString() + "' and Prc.CommodityId='27' and convert(varchar(10),Prc.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' group by WHR_Request,Prc.Acceptance_Date,Prc.CommodityId,cm.Commodity_Name,prc.godown,IssueCenter_ID";
            }

            SqlCommand cmd2 = new SqlCommand(query2, Con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            if (ds2.Tables[0].Rows.Count == 1)
            {
                lblSourcesociety.Visible = false;
                lblAcceptanceNote.Text = "Depositor Form No:";
                lblProcTCNo.Visible = true;
                lblProcTruckNo.Visible = false;
                txtProcTruckNo.Visible = false;
                txtProcTCNo.Visible = true;
                txtProcTCNo.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                txtProcTCNo.Enabled = false;
                ddlProcCommodity.SelectedValue = ds2.Tables[0].Rows[0]["CommodityId"].ToString();
                ddlProcCommodity.Enabled = false;
                txtProcQtyDeposit.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                txtProcQtyDeposit.Enabled = false;
                txtProcBags.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                txtProcBags.Enabled = false;
                txtProcBagsAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                txtProcQtyAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                hfAcptDate.Value = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                txtAcceptanceNote.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                txtAcceptanceNote.Enabled = false;
                // hfSending_Dist.Value = ds.Tables[0].Rows[0]["sending_district"].ToString();
                FillGodown();
                string godownid = ds2.Tables[0].Rows[0]["godown"].ToString();
                if (godownid != "")
                {
                    ddlGodownNo.SelectedValue = ds2.Tables[0].Rows[0]["godown"].ToString();
                }
                fillStack();
            }
            //}
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in FillProcData has occured, try again')", true);
        }
    }
    //private void fillCategory_non()
    //{
    //    try
    //    {

    //        string query = "SELECT Category_Id, Category_Name FROM tbl_MetaData_STORAGE_CATEGORY";
    //        SqlCommand cmd = new SqlCommand(query, Con);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlCategory_Non.Items.Clear();
    //            ddlCategory_Non.DataSource = ds.Tables[0];
    //            ddlCategory_Non.DataTextField = "Category_Name";
    //            ddlCategory_Non.DataValueField = "Category_Id";
    //            ddlCategory_Non.DataBind();

    //        }
    //    }
    //    catch (Exception)
    //    {

    //        /// throw;
    //    }

    //}
    protected void ddlWLCQC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWLCQC.SelectedItem.Text == "Partially Rejected")
        {
            FSWLCQC.Visible = true;
        }
        else
        {
            FSWLCQC.Visible = false;
        }
    }
    protected void btnPRejection_Click(object sender, EventArgs e)
    {
        PRBags = Convert.ToDecimal(txtProcBagsAcceptable.Text) - Convert.ToDecimal(txtRejBags.Text);
        PRQty = Convert.ToDecimal(txtProcQtyAcceptable.Text) - Convert.ToDecimal(txtRejQty.Text);
        txtProcBagsAcceptable.Text = PRBags.ToString();
        txtProcQtyAcceptable.Text = PRQty.ToString();

    }
    protected void chkDepositor_CheckedChanged(object sender, EventArgs e)
    {
        ChangedFillDepositor();
    }
    //public void GetWLC_Depositor_FN()
    //{
    //    try
    //    {
    //        string Godown_Id = ddlGodownNo.SelectedValue.ToString();
    //        if (Godown_Id != "" && Godown_Id != null)
    //        {
    //            // string GodownId = ddl_godown.SelectedValue;
    //            string GodownId = Godown_Id;
    //            string WHR_Id = "";
    //            if (Con.State == ConnectionState.Closed)
    //            {
    //                Con.Open();
    //            }
    //            //string QueryMax = "select isnull(Max(Did),0)+1 from tbl_storage_Depositor_WHR_Relation where District_Id='" + Session["Depot_DistID"].ToString() + "' and BranchId='" + Session["BranchId"].ToString() + "' ";
    //            string QueryMax = "select isnull(Max(Did),0)+1 from tbl_storage_Depositor_WHR_Relation where District_Id='" + Session["Depot_DistID"].ToString() + "' and BranchId='" + Session["BranchId"].ToString() + "' ";

    //            cmd = new SqlCommand(QueryMax, Con); // check WhrId present in whr_status table
    //            string str3 = cmd.ExecuteScalar().ToString();
    //            if ((str3 == String.Empty) || str3 == "")
    //            {
    //                str3 = "0";
    //            }
    //            if (Convert.ToInt64(str3) != 0)
    //            {
    //                string Depotid = Session["Depot_DepotID"].ToString();
    //                WHR_Id = GodownId + System.DateTime.Now.Date.ToString("yy") + Convert.ToString(Convert.ToInt64(str3));
    //            }
    //            else
    //            {
    //                string Depotid = Session["Depot_DepotID"].ToString();
    //                WHR_Id = "";
    //                WHR_Id = GodownId + System.DateTime.Now.Date.ToString("yy") + Convert.ToString(Convert.ToInt64(str3));
    //            }
    //            lbl_whrno.Text = WHR_Id.ToString();
    //            lbl_didid.Text = str3;
    //        }
    //        else
    //        {
    //            lbl_message.Text = "Try again!";
    //            return;
    //        }
    //    }
    //    catch (System.Data.SqlClient.SqlException ex)
    //    {
    //        string msg = "Insert Error:";
    //        msg += ex.Message;
    //        throw new Exception(msg);
    //    }
    //    finally
    //    {
    //        con.Close();
    //    }
    //}
    protected void FillProcRabi2020()
    {
        try
        {
            string RecDepositor = "";
            DepositorId = Session["RecDepositor"].ToString();
            comid = Session["ProcComm"].ToString();
            if (comid.ToString() == "22")
            {
                Commodity_Name = "Wheat-PSS";
            }
            else if (comid.ToString() == "63")
            {
                Commodity_Name = "GRAM";
            }
            else if (comid.ToString() == "64")
            {
                Commodity_Name = "LENTIL";
            }
            else if (comid.ToString() == "33")
            {
                Commodity_Name = "Mustard-Sarason";
            }
            else if (comid.ToString() == "13")
            {
                Commodity_Name = "Paddy-Common";
            }
            else if (comid.ToString() == "11")
            {
                Commodity_Name = "Jowar";
            }
            else if (comid.ToString() == "8")
            {
                Commodity_Name = "Bajra";
            }
            else if (comid.ToString() == "3")
            {
                Commodity_Name = "Rice-Raw-Common";
            }
            else if (comid.ToString() == "92")
            {
                Commodity_Name = "Moong";
            }
            else if (comid.ToString() == "27")
            {
                Commodity_Name = "Urad";
            }

            else if (comid.ToString() == "129")
            {
                Commodity_Name = "Fortified_Rice";
            }
            else if (comid.ToString() == "131")
            {
                Commodity_Name = "Fortified Rice Kerne";
            }
            else if (comid.ToString() == "26")
            {
                Commodity_Name = "Soya-Beans";
            }
            else if (comid.ToString() == "120")
            {
                Commodity_Name = "CHANA";
            }
            if (DepositorId.ToString() == "129")
            {
                DepositorNM = "MPSCSC";
            }
            else if (DepositorId.ToString() == "10535")
            {
                DepositorNM = "NAFED";
            }
            else if (DepositorId.ToString() == "4679")
            {
                DepositorNM = "DMO Markfed";
            }
            else if (DepositorId.ToString() == "15478")
            {
                DepositorNM = "NCCF";
            }

            lblDepositor.Text = DepositorNM;
            lblDepositorIds.Text = DepositorId;
            lblCommodity.Text = Commodity_Name;
            string DepositDate = Session["DepositDate"].ToString();
            txtProcDepostiDate.Text = DepositDate;
            hfAcptDate.Value = DepositDate;
            WLC_Depositor_No = Session["DF_Receipt_ID"].ToString();
            lblCommodityId.Text = comid.ToString();
            lblDF_Receipt_ID.Text = WLC_Depositor_No;
            lblWLC_DFN.Text = WLC_Depositor_No;
            if (Session["Depot_DistID"] != null)
            {
                if (DepositorId != "--Select--" && DepositorId != "")
                {

                    //string query2 = "";
                    //if (RecDepositor == "129" || RecDepositor == "4679")
                    //if (RecDepositor == "129")
                    //{
                    //    //ddlWLCQC.Enabled = true;
                    //    //if (Session["ProcComm"].ToString() == "22")
                    //    //{
                    //    //    query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Recd_Bags) as Recd_Bags,(AD.NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Rabi2019 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";

                    //    //}
                    //}
                    //else
                    //{
                    //ddlWLCQC.Enabled = false;
                    //if (Session["ProcComm"].ToString() == "22")
                    //{
                    //    query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Rabi2019 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                    //}
                    //else if (Session["ProcComm"].ToString() == "8" || Session["ProcComm"].ToString() == "11")
                    //{
                    //    query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                    //}
                    //else if (Session["ProcComm"].ToString() == "13" || Session["ProcComm"].ToString() == "14")
                    //{
                    //    query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                    //}
                    //else
                    //{
                    //    query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                    //}
                    //}
                    //SqlCommand cmd2 = new SqlCommand(query2, Con);
                    //SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    //DataSet ds2 = new DataSet();
                    //da2.Fill(ds2);
                    //if (ds2.Tables[0].Rows.Count == 1)
                    //{
                    //lblProcTruckNo.Visible = true;
                    //txtProcTruckNo.Visible = true;
                    //txtProcTCNo.Enabled = false;

                    //txtProcTruckNo.Text = ds2.Tables[0].Rows[0]["Truck_Number"].ToString();
                    //txtADno.Text = ds2.Tables[0].Rows[0]["Acceptance_No"].ToString();
                    //txtProcDepostiDate.Text = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                    //txtProcMoisture.Text = ds2.Tables[0].Rows[0]["Moisture"].ToString();
                    //txtProcWCMNo.Text = ds2.Tables[0].Rows[0]["Weighbridge_TaulParchi"].ToString();
                    ////txtRemarks.Text = "Stiching Bags:" + ds2.Tables[0].Rows[0]["Stiching_bags"].ToString() + ",Stencile Bags:" + ds2.Tables[0].Rows[0]["Stencile_bags"].ToString() + ",Bags Nottagged:" + ds2.Tables[0].Rows[0]["Bags_Nottagged"].ToString() + ",Bags No Color Code:" + ds2.Tables[0].Rows[0]["Bags_NotColorCode"].ToString();
                    //lblBook_No.Text = ds2.Tables[0].Rows[0]["Book_No"].ToString();
                    //lblSourcesociety.Visible = false;
                    //lblAcceptanceNote.Text = "Depositor Form No:";
                    //lblProcTCNo.Visible = true;
                    ////lblProcTruckNo.Visible = false;
                    ////txtProcTruckNo.Visible = false;
                    //txtProcTCNo.Visible = true;
                    //txtProcTCNo.Text = ds2.Tables[0].Rows[0]["TC_Number"].ToString();
                    //txtProcTCNo.Enabled = false;
                    //ddlProcCommodity.SelectedValue = ds2.Tables[0].Rows[0]["CommodityId"].ToString();
                    //ddlProcCommodity.Enabled = false;
                    //txtProcQtyDeposit.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                    //txtProcQtyDeposit.Enabled = false;
                    //txtProcBags.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    //txtProcBags.Enabled = false;
                    //txtProcBagsAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                    //txtProcQtyAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                    txtProcBagsAcceptable.Text = Session["RecBags"].ToString();
                    txtProcQtyAcceptable.Text = Session["RecQty"].ToString();
                    //
                    //hfAcptDate.Value = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                    //txtAcceptanceNote.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                    //txtAcceptanceNote.Enabled = false;
                    // hfSending_Dist.Value = ds.Tables[0].Rows[0]["sending_district"].ToString();
                    Godown_Id_New = Session["GodownID"].ToString();
                    FillGodown();
                    string godownid = Session["GodownID"].ToString();

                    if (godownid != "")
                    {
                        ddlGodownNo.SelectedValue = Session["GodownID"].ToString();
                    }
                    fillStack();
                    lblmsg.Text = "";
                    //FillDepositor();
                    //}
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please check Depositor')", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid Record')", true);
                Response.Redirect("~/IssueCenterLevel/Storage/WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record!" + "");

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in FillProcData has occured, try again')", true);
        }
    }
    protected void FillProcKharif2019()
    {
        try
        {
            string RecDepositor = "";
            RecDepositor = Session["RecDepositor"].ToString();
            if (Session["Depot_DistID"] != null && (Session["ProcComm"] == "13" || Session["ProcComm"] == "11" || Session["ProcComm"] == "8"))
            {
                if (RecDepositor != "--Select--" && RecDepositor != "")
                {

                    string query2 = "";
                    if (RecDepositor == "129" || RecDepositor == "4679" || RecDepositor == "10535")
                    {
                        //ddlWLCQC.Enabled = true;
                        ddlWLCQC.Enabled = false;
                        if (Session["ProcComm"].ToString() == "13" || Session["ProcComm"].ToString() == "11" || Session["ProcComm"].ToString() == "8")
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Recd_Bags) as Recd_Bags,(AD.NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Kharif2019 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";

                        }
                    }
                    else
                    {
                        ddlWLCQC.Enabled = false;
                        if (Session["ProcComm"].ToString() == "22")
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Rabi2019 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                        }
                        else if (Session["ProcComm"].ToString() == "8" || Session["ProcComm"].ToString() == "11")
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                        }
                        else if (Session["ProcComm"].ToString() == "13" || Session["ProcComm"].ToString() == "14")
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                        }
                        else
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                        }
                    }
                    SqlCommand cmd2 = new SqlCommand(query2, Con);
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2);
                    if (ds2.Tables[0].Rows.Count == 1)
                    {
                        lblProcTruckNo.Visible = true;
                        txtProcTruckNo.Visible = true;
                        txtProcTCNo.Enabled = false;

                        txtProcTruckNo.Text = ds2.Tables[0].Rows[0]["Truck_Number"].ToString();
                        txtADno.Text = ds2.Tables[0].Rows[0]["Acceptance_No"].ToString();
                        txtProcDepostiDate.Text = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                        txtProcMoisture.Text = ds2.Tables[0].Rows[0]["Moisture"].ToString();
                        txtProcWCMNo.Text = ds2.Tables[0].Rows[0]["Weighbridge_TaulParchi"].ToString();
                        //txtRemarks.Text = "Stiching Bags:" + ds2.Tables[0].Rows[0]["Stiching_bags"].ToString() + ",Stencile Bags:" + ds2.Tables[0].Rows[0]["Stencile_bags"].ToString() + ",Bags Nottagged:" + ds2.Tables[0].Rows[0]["Bags_Nottagged"].ToString() + ",Bags No Color Code:" + ds2.Tables[0].Rows[0]["Bags_NotColorCode"].ToString();
                        lblBook_No.Text = ds2.Tables[0].Rows[0]["Book_No"].ToString();


                        lblSourcesociety.Visible = false;
                        lblAcceptanceNote.Text = "Depositor Form No:";
                        lblProcTCNo.Visible = true;
                        //lblProcTruckNo.Visible = false;
                        //txtProcTruckNo.Visible = false;
                        txtProcTCNo.Visible = true;
                        txtProcTCNo.Text = ds2.Tables[0].Rows[0]["TC_Number"].ToString();
                        txtProcTCNo.Enabled = false;
                        ddlProcCommodity.SelectedValue = ds2.Tables[0].Rows[0]["CommodityId"].ToString();
                        ddlProcCommodity.Enabled = false;
                        txtProcQtyDeposit.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                        txtProcQtyDeposit.Enabled = false;
                        txtProcBags.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                        txtProcBags.Enabled = false;

                        txtProcBagsAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                        txtProcQtyAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                        //
                        hfAcptDate.Value = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                        txtAcceptanceNote.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                        txtAcceptanceNote.Enabled = false;
                        // hfSending_Dist.Value = ds.Tables[0].Rows[0]["sending_district"].ToString();
                        Godown_Id_New = ds2.Tables[0].Rows[0]["godown"].ToString();
                        FillGodown();
                        string godownid = ds2.Tables[0].Rows[0]["godown"].ToString();

                        if (godownid != "")
                        {
                            ddlGodownNo.SelectedValue = ds2.Tables[0].Rows[0]["godown"].ToString();
                        }
                        fillStack();
                        FillDepositor();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please check Depositor')", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid Record')", true);
                Response.Redirect("~/IssueCenterLevel/Storage/WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record!" + "");

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in FillProcData has occured, try again')", true);
        }
    }
    protected void FillProcCMS2019()
    {
        try
        {
            string RecDepositor = "";
            //RecDepositor = Session["RecDepositor"].ToString();
            RecDepositor = "10535";
            if (Session["Depot_DistID"] != null)
            {
                if (RecDepositor != "--Select--" && RecDepositor != "")
                {

                    string query2 = "";
                    if (RecDepositor == "10535")
                    {
                        ddlWLCQC.Enabled = true;
                        if (Session["ProcComm"].ToString() == "63" || Session["ProcComm"].ToString() == "64" || Session["ProcComm"].ToString() == "33")
                        {
                            //query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Recd_Bags) as Recd_Bags,(AD.NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Recd_Bags) as Recd_Bags,(AD.NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_CSM2019 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";

                        }
                    }
                    else
                    {
                        ddlWLCQC.Enabled = false;
                        if (Session["ProcComm"].ToString() == "8" || Session["ProcComm"].ToString() == "11")
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                        }
                        else if (Session["ProcComm"].ToString() == "13" || Session["ProcComm"].ToString() == "14")
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                        }
                        else
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                        }
                    }
                    SqlCommand cmd2 = new SqlCommand(query2, Con);
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2);
                    if (ds2.Tables[0].Rows.Count == 1)
                    {
                        lblProcTruckNo.Visible = true;
                        txtProcTruckNo.Visible = true;
                        txtProcTCNo.Enabled = false;

                        txtProcTruckNo.Text = ds2.Tables[0].Rows[0]["Truck_Number"].ToString();
                        txtADno.Text = ds2.Tables[0].Rows[0]["Acceptance_No"].ToString();
                        txtProcDepostiDate.Text = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                        txtProcMoisture.Text = ds2.Tables[0].Rows[0]["Moisture"].ToString();
                        txtProcWCMNo.Text = ds2.Tables[0].Rows[0]["Weighbridge_TaulParchi"].ToString();
                        //txtRemarks.Text = "Stiching Bags:" + ds2.Tables[0].Rows[0]["Stiching_bags"].ToString() + ",Stencile Bags:" + ds2.Tables[0].Rows[0]["Stencile_bags"].ToString() + ",Bags Nottagged:" + ds2.Tables[0].Rows[0]["Bags_Nottagged"].ToString() + ",Bags No Color Code:" + ds2.Tables[0].Rows[0]["Bags_NotColorCode"].ToString();
                        lblBook_No.Text = ds2.Tables[0].Rows[0]["Book_No"].ToString();


                        lblSourcesociety.Visible = false;
                        lblAcceptanceNote.Text = "Depositor Form No:";
                        lblProcTCNo.Visible = true;
                        //lblProcTruckNo.Visible = false;
                        //txtProcTruckNo.Visible = false;
                        txtProcTCNo.Visible = true;
                        txtProcTCNo.Text = ds2.Tables[0].Rows[0]["TC_Number"].ToString();
                        txtProcTCNo.Enabled = false;
                        ddlProcCommodity.SelectedValue = ds2.Tables[0].Rows[0]["CommodityId"].ToString();
                        ddlProcCommodity.Enabled = false;
                        txtProcQtyDeposit.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                        txtProcQtyDeposit.Enabled = false;
                        txtProcBags.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                        txtProcBags.Enabled = false;

                        txtProcBagsAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                        txtProcQtyAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                        //
                        hfAcptDate.Value = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                        txtAcceptanceNote.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                        txtAcceptanceNote.Enabled = false;
                        // hfSending_Dist.Value = ds.Tables[0].Rows[0]["sending_district"].ToString();
                        Godown_Id_New = ds2.Tables[0].Rows[0]["godown"].ToString();
                        FillGodown();
                        string godownid = ds2.Tables[0].Rows[0]["godown"].ToString();

                        if (godownid != "")
                        {
                            ddlGodownNo.SelectedValue = ds2.Tables[0].Rows[0]["godown"].ToString();
                        }
                        fillStack();
                        FillDepositor();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please check Depositor')", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid Record')", true);
                Response.Redirect("~/IssueCenterLevel/Storage/WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record!" + "");

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in FillProcData has occured, try again')", true);
        }
    }
    protected void FillProcArhar2019()
    {
        try
        {
            string RecDepositor = "";
            //RecDepositor = Session["RecDepositor"].ToString();
            RecDepositor = "10535";
            if (Session["Depot_DistID"] != null)
            {
                if (RecDepositor != "--Select--" && RecDepositor != "")
                {

                    string query2 = "";
                    if (RecDepositor == "10535")
                    {
                        ddlWLCQC.Enabled = true;
                        if (Session["ProcComm"].ToString() == "52")
                        {
                            //query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Recd_Bags) as Recd_Bags,(AD.NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Recd_Bags) as Recd_Bags,(AD.NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Tuar2019 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";

                        }
                    }
                    else
                    {
                        ddlWLCQC.Enabled = false;
                        if (Session["ProcComm"].ToString() == "8" || Session["ProcComm"].ToString() == "11")
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                        }
                        else if (Session["ProcComm"].ToString() == "13" || Session["ProcComm"].ToString() == "14")
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                        }
                        else
                        {
                            query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
                        }
                    }
                    SqlCommand cmd2 = new SqlCommand(query2, Con);
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2);
                    if (ds2.Tables[0].Rows.Count == 1)
                    {
                        lblProcTruckNo.Visible = true;
                        txtProcTruckNo.Visible = true;
                        txtProcTCNo.Enabled = false;

                        txtProcTruckNo.Text = ds2.Tables[0].Rows[0]["Truck_Number"].ToString();
                        txtADno.Text = ds2.Tables[0].Rows[0]["Acceptance_No"].ToString();
                        txtProcDepostiDate.Text = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                        txtProcMoisture.Text = ds2.Tables[0].Rows[0]["Moisture"].ToString();
                        txtProcWCMNo.Text = ds2.Tables[0].Rows[0]["Weighbridge_TaulParchi"].ToString();
                        //txtRemarks.Text = "Stiching Bags:" + ds2.Tables[0].Rows[0]["Stiching_bags"].ToString() + ",Stencile Bags:" + ds2.Tables[0].Rows[0]["Stencile_bags"].ToString() + ",Bags Nottagged:" + ds2.Tables[0].Rows[0]["Bags_Nottagged"].ToString() + ",Bags No Color Code:" + ds2.Tables[0].Rows[0]["Bags_NotColorCode"].ToString();
                        lblBook_No.Text = ds2.Tables[0].Rows[0]["Book_No"].ToString();


                        lblSourcesociety.Visible = false;
                        lblAcceptanceNote.Text = "Depositor Form No:";
                        lblProcTCNo.Visible = true;
                        //lblProcTruckNo.Visible = false;
                        //txtProcTruckNo.Visible = false;
                        txtProcTCNo.Visible = true;
                        txtProcTCNo.Text = ds2.Tables[0].Rows[0]["TC_Number"].ToString();
                        txtProcTCNo.Enabled = false;
                        ddlProcCommodity.SelectedValue = ds2.Tables[0].Rows[0]["CommodityId"].ToString();
                        ddlProcCommodity.Enabled = false;
                        txtProcQtyDeposit.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                        txtProcQtyDeposit.Enabled = false;
                        txtProcBags.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                        txtProcBags.Enabled = false;

                        txtProcBagsAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
                        txtProcQtyAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
                        //
                        hfAcptDate.Value = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
                        txtAcceptanceNote.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
                        txtAcceptanceNote.Enabled = false;
                        // hfSending_Dist.Value = ds.Tables[0].Rows[0]["sending_district"].ToString();
                        Godown_Id_New = ds2.Tables[0].Rows[0]["godown"].ToString();
                        FillGodown();
                        string godownid = ds2.Tables[0].Rows[0]["godown"].ToString();

                        if (godownid != "")
                        {
                            ddlGodownNo.SelectedValue = ds2.Tables[0].Rows[0]["godown"].ToString();
                        }
                        fillStack();
                        FillDepositor();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please check Depositor')", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid Record')", true);
                Response.Redirect("~/IssueCenterLevel/Storage/WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record!" + "");

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in FillProcData has occured, try again')", true);
        }
    }
    //protected void FillProcRabi2020()
    //{
    //    try
    //    {
    //        string RecDepositor = "";
    //        RecDepositor = Session["RecDepositor"].ToString();
    //        if (Session["Depot_DistID"] != null && Session["ProcComm"] == "22")
    //        {
    //            if (RecDepositor != "--Select--" && RecDepositor != "")
    //            {

    //                string query2 = "";
    //                if (RecDepositor == "129")
    //                {
    //                    ddlWLCQC.Enabled = true;
    //                    if (Session["ProcComm"].ToString() == "22")
    //                    {
    //                        //query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Recd_Bags) as Recd_Bags,(AD.NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
    //                        query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Recd_Bags) as Recd_Bags,(AD.NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Rabi2019 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";

    //                    }
    //                }
    //                else
    //                {
    //                    ddlWLCQC.Enabled = false;
    //                    if (Session["ProcComm"].ToString() == "22")
    //                    {
    //                        query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Rabi2019 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
    //                    }
    //                    else if (Session["ProcComm"].ToString() == "8" || Session["ProcComm"].ToString() == "11")
    //                    {
    //                        query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
    //                    }
    //                    else if (Session["ProcComm"].ToString() == "13" || Session["ProcComm"].ToString() == "14")
    //                    {
    //                        query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
    //                    }
    //                    else
    //                    {
    //                        query2 = "select AD.IssueCenter_ID,AD.Purchase_Center,convert(varchar(10),AD.Acceptance_Date,103) as 'Acceptance_Date',AD.Recd_Godown as godown,(AD.Reject_Bags) as Recd_Bags,(AD.Rejected_NetWeight) AS Recd_Qty,AD.Commodity_Id as CommodityId,AD.DepositerNo as WHR_Request,cm.Commodity_Name,AD.TC_Number,AD.Truck_Number,AD.Acceptance_No,AD.Stiching_bags,AD.Stencile_bags,AD.Moisture,AD.Weighbridge_TaulParchi,AD.Bags_Nottagged,AD.Bags_NotColorCode,AD.Book_No from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD join tbl_MetaData_STORAGE_COMMODITY as cm on cm.Commodity_Id=AD.Commodity_Id where AD.DepositerNo is not null and  AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.DepositerNo='" + Session["whrreq"].ToString() + "' and convert(varchar(10),AD.Acceptance_Date,103)='" + Session["AccepDate"].ToString() + "' and AD.Commodity_Id='" + Session["ProcComm"].ToString() + "'";
    //                    }
    //                }
    //                SqlCommand cmd2 = new SqlCommand(query2, Con);
    //                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
    //                DataSet ds2 = new DataSet();
    //                da2.Fill(ds2);
    //                if (ds2.Tables[0].Rows.Count == 1)
    //                {
    //                    lblProcTruckNo.Visible = true;
    //                    txtProcTruckNo.Visible = true;
    //                    txtProcTCNo.Enabled = false;

    //                    txtProcTruckNo.Text = ds2.Tables[0].Rows[0]["Truck_Number"].ToString();
    //                    txtADno.Text = ds2.Tables[0].Rows[0]["Acceptance_No"].ToString();
    //                    txtProcDepostiDate.Text = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
    //                    txtProcMoisture.Text = ds2.Tables[0].Rows[0]["Moisture"].ToString();
    //                    txtProcWCMNo.Text = ds2.Tables[0].Rows[0]["Weighbridge_TaulParchi"].ToString();
    //                    //txtRemarks.Text = "Stiching Bags:" + ds2.Tables[0].Rows[0]["Stiching_bags"].ToString() + ",Stencile Bags:" + ds2.Tables[0].Rows[0]["Stencile_bags"].ToString() + ",Bags Nottagged:" + ds2.Tables[0].Rows[0]["Bags_Nottagged"].ToString() + ",Bags No Color Code:" + ds2.Tables[0].Rows[0]["Bags_NotColorCode"].ToString();
    //                    lblBook_No.Text = ds2.Tables[0].Rows[0]["Book_No"].ToString();


    //                    lblSourcesociety.Visible = false;
    //                    lblAcceptanceNote.Text = "Depositor Form No:";
    //                    lblProcTCNo.Visible = true;
    //                    //lblProcTruckNo.Visible = false;
    //                    //txtProcTruckNo.Visible = false;
    //                    txtProcTCNo.Visible = true;
    //                    txtProcTCNo.Text = ds2.Tables[0].Rows[0]["TC_Number"].ToString();
    //                    txtProcTCNo.Enabled = false;
    //                    ddlProcCommodity.SelectedValue = ds2.Tables[0].Rows[0]["CommodityId"].ToString();
    //                    ddlProcCommodity.Enabled = false;
    //                    txtProcQtyDeposit.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
    //                    txtProcQtyDeposit.Enabled = false;
    //                    txtProcBags.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
    //                    txtProcBags.Enabled = false;

    //                    txtProcBagsAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Bags"].ToString();
    //                    txtProcQtyAcceptable.Text = ds2.Tables[0].Rows[0]["Recd_Qty"].ToString();
    //                    //
    //                    hfAcptDate.Value = ds2.Tables[0].Rows[0]["Acceptance_Date"].ToString();
    //                    txtAcceptanceNote.Text = ds2.Tables[0].Rows[0]["WHR_Request"].ToString();
    //                    txtAcceptanceNote.Enabled = false;
    //                    // hfSending_Dist.Value = ds.Tables[0].Rows[0]["sending_district"].ToString();
    //                    Godown_Id_New = ds2.Tables[0].Rows[0]["godown"].ToString();
    //                    FillGodown();
    //                    string godownid = ds2.Tables[0].Rows[0]["godown"].ToString();

    //                    if (godownid != "")
    //                    {
    //                        ddlGodownNo.SelectedValue = ds2.Tables[0].Rows[0]["godown"].ToString();
    //                    }
    //                    fillStack();
    //                    FillDepositor();
    //                }
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please check Depositor')", true);
    //            }

    //        }
    //        else
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid Record')", true);
    //            Response.Redirect("~/IssueCenterLevel/Storage/WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record!" + "");

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in FillProcData has occured, try again')", true);
    //    }
    //}
}