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
using System.Text;
using System.Collections.Generic;


public partial class Inspection_Fill_Inspection_Annexure_B : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
   // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string PFID = "";
    SqlTransaction sqltran;
    string client_IP = "";
    int a_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        client_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        lbl_user.Text = Session["UserName"].ToString();
        PFID = Session["UserId"].ToString();
        if (Session["UserName"] != null && PFID != "")
        {
            if (!IsPostBack)
            {
               // lblinspid.Text = Session["SInspID"].ToString();
                FatchScheduleInspData(lblinspid.Text.Trim());
               // GetGdwn(Session["SInsp_BranchID"].ToString());
                fillGodownType();
            }
        }
        else
        {
            Response.Redirect("../../Logout.aspx");
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("InspectionOfficer_Welcome.aspx");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("InspectionLogin.aspx");
    }
    private void GetGdwn(string Branch)
    {
        try
        {
            string str = "";
            str = "select Godown_ID,Godown_Name +' (' +Godown_ID +')' as Godown_Name from tbl_MetaData_GODOWN_2018 where BranchID='" + Branch.Trim() + "' and Godown_ID not in (select distinct ANX.GodownID from tbl_stackwiseBal_Annex_B as ANX ) order by Godown_Name";
            SqlDataAdapter da = new SqlDataAdapter(str, conStr);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_gdwn.DataSource = ds.Tables[0];
                ddl_gdwn.DataTextField = "Godown_Name";
                ddl_gdwn.DataValueField = "Godown_ID";
                ddl_gdwn.DataBind();
                ddl_gdwn.Items.Insert(0, "--Select--");
            }
            else
            {
                ddl_gdwn.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }
    protected void ddl_gdwn_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strsql = "SELECT MDG.BranchID,MDG.Godown_ID,MDS.Stack_ID,Stack_Name,MDS.Commodity_Id,Commodity_Name,MDS.Stack_capacity,isnull(AvlBags,0) as AvlBags,isnull(AvlQty,0) as AvlQty FROM tbl_MetaData_STACK as MDS inner join tbl_MetaData_GODOWN_2018 as MDG on MDG.Godown_ID=MDS.Godown_ID inner join tbl_metadata_storage_commodity as CMD on CMD.Commodity_ID=MDS.Commodity_ID left join (select WHR.Godown_ID,WHR.Stack_ID,WHR.Commodity_Id,SUM(RecBags-DelBags) as AvlBags , SUM(RecQty-DelQty) as AvlQty from View_WHRcurrentstock as WHR where WHR.Godown_ID='" + ddl_gdwn.SelectedValue.ToString() + "' and WHR.Stack_ID in (select MS.Stack_ID FROM tbl_MetaData_STACK as MS where MS.Stack_Killed='N' and MS.Godown_ID='" + ddl_gdwn.SelectedValue.ToString() + "') group by WHR.Godown_ID,WHR.Stack_ID,WHR.Commodity_Id having SUM(RecBags-DelBags)!=0) as WHR on MDS.Stack_ID= WHR.Stack_ID and MDS.Commodity_ID=WHR.Commodity_Id where MDS.Stack_Killed='N' and MDG.Godown_ID='" + ddl_gdwn.SelectedValue.ToString() + "' order by Commodity_Name,Stack_Name ";
        SqlCommand cmd = new SqlCommand(strsql, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            tr_griddata.Visible = true;
            GD_StackBal.DataSource = ds;
            GD_StackBal.DataBind();
            this.GD_StackBal.Columns[0].Visible = false;
            fillbagstxt();
            GetGdwnData();
            txtQty_TextChanged(null,null);
        }
        else
        {
            tr_griddata.Visible = false;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found...')", true);
        }
    }
    public void fillbagstxt()
    {
        for (int i = 0; GD_StackBal.Rows.Count > i; i++)
        {
            ((TextBox)GD_StackBal.Rows[i].FindControl("GtxtbagsActual")).Text = GD_StackBal.Rows[i].Cells[4].Text.ToString();
        }
    }
    public void FatchScheduleInspData(string InspID)
    {
        string strsql = "select Inspection_ID,PF_ID,(select Officer_Name  from tbl_metadata_Inspection_officer where PF_ID=ISD.PF_ID) as Officer_Name,(select district_name from tbl_metadata_district as MDDIS where MDDIS.District_id=ISD.District_ID) as distirct_name,(select Depotname from tbl_metadata_depot as MDD where MDD.branchID=ISD.Branch_ID) as Depotname, Inspection_Status,BranchManagerName,BranchManagerCUGNo,Order_No,Insp_Period,Insp_Type,Convert(varchar(10),Order_Date,103) as Order_Date,ISD.District_ID,ISD.Branch_ID from tbl_Inpection_Scheduled_Date as ISD where Inspection_ID='" + InspID + "' ";
        SqlCommand cmd = new SqlCommand(strsql, conStr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblbranch.Text = dt.Rows[0]["Depotname"].ToString().Trim();
            lblinsptype.Text = dt.Rows[0]["Insp_Type"].ToString().Trim();
            lblInspPeriod.Text = dt.Rows[0]["Insp_Period"].ToString().Trim();
        }
        else
        {

        }
    }
    public void GetGdwnData()
    {
        string strsql = "select Godown_ID,Convert(Decimal(18,2),Godown_Capacity) as Godown_Capacity,Convert(Decimal(18,2),Godown_Scientific_capacity) as Godown_Scientific_capacity,Hired_Type,Storage_Type from tbl_MetaData_GODOWN_2018 where Godown_ID='" + ddl_gdwn.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(strsql, conStr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtmaxcpt.Text = dt.Rows[0]["Godown_Capacity"].ToString().Trim();
            txtsci_CPT.Text = dt.Rows[0]["Godown_Scientific_capacity"].ToString().Trim();
            ddlhiredtype.SelectedValue = dt.Rows[0]["Hired_Type"].ToString().Trim();
            ddlStorageType.SelectedValue = dt.Rows[0]["Storage_Type"].ToString().Trim();
        }
        else
        {

        }
    }
    private void fillGodownType()
    {
        try
        {
            string query = "";
            query = "SELECT  [Gid],[GodownType],[TypeValue] FROM [dbo].[GodownTypeMaster]";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlhiredtype.Items.Clear();
                ddlhiredtype.DataSource = ds.Tables[0];
                ddlhiredtype.DataTextField = "GodownType";
                ddlhiredtype.DataValueField = "GodownType";
                ddlhiredtype.DataBind();
                ddlhiredtype.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlhiredtype.Items.Clear();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        string transid = "";
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        if (GD_StackBal.Rows.Count == 0 || GD_StackBal.Rows.Count == null)
        {

        }
        else
        {
            sqltran = conStr.BeginTransaction();
            a_id = ChkInspAID(lblinspid.Text.Trim());
            transid = lblinspid.Text.Trim() + a_id;
            int CountRow = GD_StackBal.Rows.Count;
            int CountInsertrow =0;
            try
            {
                if (ddl_gdwn.SelectedItem.Text == "--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please select Godown...')", true);
                    ddl_gdwn.Focus();
                }
                else if (txt_inspdate.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Inspection Date...')", true);
                    ddlhiredtype.Focus();
                }
                else if (ddlhiredtype.SelectedItem.Text=="--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Hired Type...')", true);
                    ddlhiredtype.Focus();
                }
                else if (ddlStorageType.SelectedItem.Text=="--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please select Storage Type...')", true);
                    ddlStorageType.Focus();
                }
                else if (txtsci_CPT.Text=="")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Scientific Capacity...')", true);
                    txtsci_CPT.Focus();
                }
                else if (txtmaxcpt.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Max Capacity...')", true);
                    txtmaxcpt.Focus();
                }
                else
                {
                    for (int i = 0; GD_StackBal.Rows.Count > i; i++)
                    {
                        if (((TextBox)GD_StackBal.Rows[i].FindControl("GtxtbagsActual")).Text.ToString().Trim()=="")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter PV Actual Bags...')", true);
                            ((TextBox)GD_StackBal.Rows[i].FindControl("GtxtbagsActual")).Focus();
                        }
                        else if (((TextBox)GD_StackBal.Rows[i].FindControl("GtxtDiffBags")).Text.ToString().Trim() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Diff Bags...')", true);
                            ((TextBox)GD_StackBal.Rows[i].FindControl("GtxtDiffBags")).Focus();
                        }
                        else if (((DropDownList)GD_StackBal.Rows[i].FindControl("ddlclassifi")).SelectedItem.Text.Trim() == "--Select--")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Classification...')", true);
                            ((DropDownList)GD_StackBal.Rows[i].FindControl("ddlclassifi")).Focus();
                        }
                        else if (((DropDownList)GD_StackBal.Rows[i].FindControl("ddlDiffBagstYpe")).SelectedItem.Text.Trim() == "--Select--")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Difference of Bags Type...')", true);
                            ((DropDownList)GD_StackBal.Rows[i].FindControl("ddlDiffBagstYpe")).Focus();
                        }
                        else if (((DropDownList)GD_StackBal.Rows[i].FindControl("ddlStackType")).SelectedItem.Text.Trim() == "--Select--")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Stack Open/Covered (If Fumigated / CAP)...')", true);
                            ((DropDownList)GD_StackBal.Rows[i].FindControl("ddlStackType")).Focus();
                        }


                        else if (Convert.ToInt32(((TextBox)GD_StackBal.Rows[i].FindControl("GtxtDiffBags")).Text.ToString().Trim()) != 0 && ((DropDownList)GD_StackBal.Rows[i].FindControl("ddlDiffBagstYpe")).SelectedValue == "No Difference")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('If Difference of Bags is Available Then You Cannot Select No Difference...')", true);
                            ((DropDownList)GD_StackBal.Rows[i].FindControl("ddlDiffBagstYpe")).Focus();
                        }
                        else if (Convert.ToInt32(((TextBox)GD_StackBal.Rows[i].FindControl("GtxtDiffBags")).Text.ToString().Trim()) != 0 && ((TextBox)GD_StackBal.Rows[i].FindControl("GtxtRemark")).Text.ToString().Trim() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('If Difference of Bags is Available , So Please Enter Remark In Detail...')", true);
                            ((DropDownList)GD_StackBal.Rows[i].FindControl("ddlDiffBagstYpe")).Focus();
                        }
                        else if (((DropDownList)GD_StackBal.Rows[i].FindControl("ddlStackType")).SelectedValue == "Covered" && ddlStorageType.SelectedValue == "Covered" && ((TextBox)GD_StackBal.Rows[i].FindControl("GtxtLastFDate")).Text.ToString().Trim() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('If Stack is Covered , So Please Enter Last Fumigation Date...')", true);
                            ((DropDownList)GD_StackBal.Rows[i].FindControl("ddlDiffBagstYpe")).Focus();
                        }
                        else
                        {
                            string qry = "INSERT INTO [tbl_stackwiseBal_Annex_B]([Trans_ID],[Insp_Officer_ID],[InspectionID],[DistrictID],[BranchID],[GodownID],[StackID],[StackCapacity],[StackCommodity],[Avl_Bags],[Avl_Qty],[Avl_Bags_AsPer_PV],[Diff_Bags],[Stack_Classification],[Last_Fumigation_Date],[InspectionDate],[Remarks],[CreatedBy],[CreatedDate],[AID],[Type_Of_DiffBags],[Stack_PVType]) VALUES('" + transid + "','" + Session["UserId"].ToString() + "','" + lblinspid.Text.ToString() + "','" + Session["SInsp_DisID"].ToString() + "','" + Session["SInsp_BranchID"].ToString() + "','" + ddl_gdwn.SelectedValue.ToString() + "','" + GD_StackBal.Rows[i].Cells[0].Text.ToString().Trim() + "','" + GD_StackBal.Rows[i].Cells[2].Text.ToString().Trim() + "','" + GD_StackBal.Rows[i].Cells[3].Text.ToString().Trim() + "','" + GD_StackBal.Rows[i].Cells[4].Text.ToString().Trim() + "','" + GD_StackBal.Rows[i].Cells[5].Text.ToString().Trim() + "','" + ((TextBox)GD_StackBal.Rows[i].FindControl("GtxtbagsActual")).Text.ToString().Trim() + "','" + ((TextBox)GD_StackBal.Rows[i].FindControl("GtxtDiffBags")).Text.ToString().Trim() + "','" + ((DropDownList)GD_StackBal.Rows[i].FindControl("ddlclassifi")).SelectedValue.ToString() + "','" + getDate_MDY(((TextBox)GD_StackBal.Rows[i].FindControl("GtxtLastFDate")).Text.ToString().Trim()) + "','" + getDate_MDY(txt_inspdate.Text) + "',N'" + ((TextBox)GD_StackBal.Rows[i].FindControl("GtxtRemark")).Text.ToString().Trim() + "','" + client_IP + "',GETDATE(),'" + a_id + "','" + ((DropDownList)GD_StackBal.Rows[i].FindControl("ddlDiffBagstYpe")).SelectedValue.ToString() + "','" + ((DropDownList)GD_StackBal.Rows[i].FindControl("ddlStackType")).SelectedValue.ToString() + "')";
                            // string qry = "INSERT INTO [tbl_stackwiseBal_Annex_B]([Trans_ID],[Insp_Officer_ID],[InspectionID],[DistrictID],[BranchID],[GodownID]) VALUES('" + transid + "','" + Session["UserId"].ToString() + "','" + lblinspid.Text.ToString() + "','" + Session["SDistID"].ToString() + "','" + Session["SBranchID"].ToString() + "','" + ddl_gdwn.SelectedValue.ToString() + "')";
                            SqlCommand cmd1 = new SqlCommand(qry, conStr, sqltran);
                            int CT1 = 0;
                            CT1 = cmd1.ExecuteNonQuery();
                            if (CT1 > 0)
                            {
                                a_id = Convert.ToInt32(a_id + 1);
                                transid = lblinspid.Text.Trim() + a_id;
                                CountInsertrow = CountInsertrow + 1;
                            }
                        }
                    }
                    string gdwnqry = "INSERT INTO [tbl_PV_Metadata_Godown]([DistrictID],[BranchID],[Godown_ID],[Godown_Capacity],[Godown_Scientific_Capacity],[Hired_Type],[Storage_Type],[Inspection_Date],[CreatedBy],[CreatedDate],[Inspection_ID],[Insp_Officer_ID],[Total_Bags_AsPerOnline],[Total_Bags_AsPerPV]) VALUES('" + Session["SInsp_DisID"].ToString() + "','" + Session["SInsp_BranchID"].ToString() + "','" + ddl_gdwn.SelectedValue.ToString() + "','" + txtmaxcpt.Text.Trim() + "','" + txtsci_CPT.Text.Trim() + "','" + ddlhiredtype.SelectedValue.ToString().Trim() + "','" + ddlStorageType.SelectedValue.ToString().Trim() + "','" + getDate_MDY(txt_inspdate.Text) + "','"+ ClientIP +"',GETDATE(),'" + lblinspid.Text.ToString() + "','" + Session["UserId"].ToString() + "','" + lblTotalBags_O.Text.Trim() +"','" + lblTptalBags_PV.Text.Trim() +"')";
                    SqlCommand cmd2 = new SqlCommand(gdwnqry, conStr, sqltran);
                    int CT2 = 0;
                    CT2 = cmd2.ExecuteNonQuery();
                }
                if ((CountInsertrow == CountRow) && CountRow != 0 && CountInsertrow != 0)
                {
                    sqltran.Commit();
                    pnlofferpopup.Visible = true;
                    ModalPopupExtender1.Show();
                }
            }
            catch (Exception ex)
            {
                sqltran.Rollback();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Something Error...'); </script> ");
            }
            finally
            {
                sqltran.Dispose();
                conStr.Close();
            }
        }
    }
    public int ChkInspAID(string InspID)
    {
        int MaxAID = 0;
        string QueryMax = "select MAX(AID) as AID from tbl_stackwiseBal_Annex_B where InspectionID='" + InspID + "' ";
        SqlCommand cmd = new SqlCommand(QueryMax, conStr, sqltran);
        string str3 = cmd.ExecuteScalar().ToString();
        if ((str3 != String.Empty) || str3 != "")
        {
            int a = Convert.ToInt32(str3.ToString());
            MaxAID = a + 1;
        }
        else
        {
            MaxAID = 1;
        }
        return MaxAID;
    }
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        int diff = 0; int O_Bags = 0; int P_Bags = 0; int O_sum = 0; int PV_Sum = 0;

        for (int i = 0; GD_StackBal.Rows.Count > i; i++)
        {
            if (GD_StackBal.Rows[i].Cells[4].Text.ToString().Trim() == "")
            {
                O_Bags = 0;
            }
            else
            {
                O_Bags = Convert.ToInt32(GD_StackBal.Rows[i].Cells[4].Text.ToString().Trim());
            }
            if (((TextBox)GD_StackBal.Rows[i].FindControl("GtxtbagsActual")).Text.ToString().Trim() == "")
            {
                P_Bags = 0;
            }
            else
            {
                P_Bags = Convert.ToInt32(((TextBox)GD_StackBal.Rows[i].FindControl("GtxtbagsActual")).Text.ToString().Trim());
            }
            diff = P_Bags-O_Bags;
            O_sum = O_Bags + O_sum;
            PV_Sum = P_Bags + PV_Sum;
            lblTotalBags_O.Text = Convert.ToString(O_sum);
            lblTptalBags_PV.Text = Convert.ToString(PV_Sum);
            ((TextBox)GD_StackBal.Rows[i].FindControl("GtxtDiffBags")).Text = Convert.ToString(diff);
            if (ddlStorageType.SelectedValue == "Permanent(CAP)" || ddlStorageType.SelectedValue == "Temporary(CAP)")
            {
                ((DropDownList)GD_StackBal.Rows[i].FindControl("ddlStackType")).SelectedValue = "Covered";
            }
        }
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("Fill_Inspection_Annexure_B.aspx");
    }

}
