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

public partial class Masters_DepositorMaster : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    DataReader DObj = null;
    protected Common ComObj = null, cmn = null;
    public SqlDataAdapter sqlda;
    SqlTransaction sqltran;
    DataSet sqlds;
    DataSet ds1 ;
    string MaxDPID = "";

    protected void Page_Load(object sender, EventArgs e)
    {     
        try
        {

            //txtDepositorName.Attributes.Add("onkeypress", "return Spc_characteralpha(this);");
            ////txtContactNo.Attributes.Add("onkeypress", "return CheckOnlyNumeric(event,this);");
            //txtAddress.Enabled = false;
            //txtContactNo.Enabled = false;
            //txtDepositorName.Enabled = false;
            //drp_lstDepositorType.Enabled = false;

            ComObj = new Common(ConfigurationManager.AppSettings["connect_warehouse"].ToString());

            if (Session["lang"].ToString() == "Hindi")
            {
                //lblDepositorMasterEntry.Text = Resources.hindi.lblDepositorMasterEntry;
                //Label1.Text = Resources.hindi.lblDepositor;
                //Label2.Text = Resources.hindi.lblDepositorType;
                //Label3.Text = Resources.hindi.address;
                //Label4.Text = Resources.hindi.conactno;
            }

            if (!IsPostBack)
            {
                if (Session["Depot_DepotID"].ToString() != "")
                {
                   
                    string depotId = Session["Depot_DepotID"].ToString();
                    GetDepositor(depotId);

                    //GetDepositorType();
                }
                //ViewState["DepositorId"] = Depositor_Gridview.SelectedRow.Cells[1].Text.Trim();
                //txtDepositorName.Text = Depositor_Gridview.SelectedRow.Cells[2].Text.Trim();
                //string depositorType = Depositor_Gridview.SelectedRow.Cells[3].Text.Trim();
                //drp_lstDepositorType.SelectedItem.Selected = false;
                //foreach (ListItem lst in drp_lstDepositorType.Items)
                //{

                //    if (lst.Value == depositorType)
                //    {
                //        lst.Enabled = true;
                //        lst.Selected = true;
                //    }

                //}
                //txtAddress.Text = Depositor_Gridview.SelectedRow.Cells[4].Text.Trim();
                //txtContactNo.Text = Depositor_Gridview.SelectedRow.Cells[5].Text.Trim();
                //GetSocietyDepositor();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
       
    }

    //private void GetDepositorType()
    //{
    //    DObj = new DataReader(ComObj);
    //    string qry = "SELECT [Depositor_Type_ID], [Depositor_Type] FROM [tbl_MetaData_Depositor_Type] where Depositor_Type_Id !=4";
    //    //DataSet ds = DObj.selectAny(qry);
    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds == null)
    //    {

    //    }
    //    else
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            drp_lstDepositorType.DataSource = ds.Tables[0];
    //            drp_lstDepositorType.DataTextField = "Depositor_Type";
    //            drp_lstDepositorType.DataValueField = "Depositor_Type";
    //            drp_lstDepositorType.DataBind();
    //            drp_lstDepositorType.Items.Insert(0, "--Select--");
    //        }
            
    //    }
    //}

    private void GetDepositor(string depotId)
    {
        string BranchId = Session["BranchID"].ToString();
        con.Open();
        string query = "SELECT [Depositor_ID],[State_ID],[Depot_ID],[Depositor_Name],[Depositor_Type],[Address],[Contact_No] FROM [tbl_MetaData_DEPOSITOR]  where BranchId='" + BranchId + "' union SELECT [Depositor_ID],[State_ID],[Depot_ID],[Depositor_Name],[Depositor_Type],[Address],[Contact_No] FROM [tbl_MetaData_DEPOSITOR]  where Depositor_Name in ('MPSCSC','MPWLC','FCI') AND  Depositor_Type='Institution' ";
          
        sqlda = new SqlDataAdapter(query, con);
        sqlds = new DataSet();
        sqlda.Fill(sqlds, "table");
        Depositor_Gridview.DataSource = sqlds;
        Depositor_Gridview.DataBind();

        if (sqlds.Tables[0].Rows.Count > 0)
            {
                //ViewState["dsDepositor"] = sqlds;
                fillGrid(sqlds);
            }
            con.Close();
    }
    private void fillGrid(DataSet ds)
    {
        Depositor_Gridview.DataSource = ds.Tables[0];
        Depositor_Gridview.DataBind();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string Client_Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string depotId = Session["Depot_DepotID"].ToString();
        string BranchId = Session["BranchID"].ToString();
        string DistID = Session["Depot_DistID"].ToString();
        int res = 0;
        string query = "";
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        //string address = txtAddress.Text;
        //string ContactNo = txtContactNo.Text;
        //string depositorName = txtDepositorName.Text;
        //string depositorType = drp_lstDepositorType.SelectedValue;
        //if (btnEdit.Text == "Edit")
        //{
        //    //txtAddress.Enabled = true;
        //    //txtContactNo.Enabled = true;
        //    //txtDepositorName.Enabled = true;
        //    //drp_lstDepositorType.Enabled = true;
        //    btnEdit.Text = "Upda te";
        //    btnCancel.Text = "Cancel";
        //}
        //else if (btnEdit.Text == "Update")
        //{
        //    string depositorid = ViewState["DepositorId"].ToString();
        //    if (depositorid == "129" || depositorid == "184" || depositorid == "181")
        //    {
        //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('You Can Not Update Institution depositor......'); </script> ");

        //        //query = "UPDATE [tbl_MetaData_DEPOSITOR] SET [Depositor_Name] = case when (([Depositor_Name] = 'MPSCSC' and [Depositor_Type] = 'Institution') OR '" + depositorName + "'='MPSCSC' ) then [Depositor_Name] when (([Depositor_Name] = 'MPWLC' and [Depositor_Type] = 'Institution') OR '" + depositorName + "'='MPWLC' ) then [Depositor_Name] when (([Depositor_Name] = 'FCI' and [Depositor_Type] = 'Institution') OR '" + depositorName + "'='FCI' ) then [Depositor_Name] else '" + depositorName + "' end, [Depositor_Type] = case when ([Depositor_Name] = 'MPSCSC' and [Depositor_Type] = 'Institution') then [Depositor_Type] when ([Depositor_Name] = 'MPWLC' and [Depositor_Type] = 'Institution') then [Depositor_Type] when ([Depositor_Name] = 'FCI' and [Depositor_Type] = 'Institution') then [Depositor_Type] else '" + depositorType + "' end,[Address] = '" + address + "', [Contact_No] = '" + ContactNo + "' WHERE [Depositor_ID] = '" + depositorid + "' and [Depositor_ID] not in (select Depositor_ID from [tbl_MetaData_DEPOSITOR],tbl_Storage_Arrival_Stock where [tbl_MetaData_DEPOSITOR].Depositor_Name=tbl_Storage_Arrival_Stock.Depositor_Name )";
        //    }
        //    else
        //    {
        //        try
        //        {
        //            if (con != null)
        //            {
        //                con.Open();
        //                string log_qry = "insert into tbl_MetaData_DEPOSITOR_Log select [Depositor_ID],[State_ID],[District_ID],[Depot_ID],[Depositor_Name],[Depositor_Type],[Address],[Contact_No],[IsActive],[CreatedBy],[CreatedDate],'" + Client_Ip + "',getdate(),[DeletedBy],[DeletedDate],[BranchId] from [tbl_MetaData_DEPOSITOR] where Depositor_ID='" + depositorid + "'";
        //                cmd = new SqlCommand(log_qry, con);
        //                int s = cmd.ExecuteNonQuery();
        //                con.Close();

        //                if (s > 0)
        //                {
        //                    con.Open();
        //                    query = "update tbl_MetaData_DEPOSITOR set Depositor_Name='" + depositorName + "',Depositor_Type='" + depositorType + "',Address='" + address + "',Contact_No='" + ContactNo + "',UpdatedBy='" + Client_Ip + "',UpdatedDate=GETDATE() where Depositor_ID='" + depositorid + "'";
        //                    cmd = new SqlCommand(query, con);
        //                    int d = cmd.ExecuteNonQuery();
        //                    con.Close();
        //                    if (d > 0)
        //                    {
        //                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Updated Successfully......'); </script> ");
        //                    }
        //                }
        //                con.Close();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            lblMsg.Text = ex.Message.ToString();
        //        }
        //        finally
        //        {
        //            con.Close();
        //            GetDepositor(depotId);
        //        }
        //        res = 1;
        //        btnEdit.Text = "Edit";
        //        btnCancel.Text = "New";
        //        txtAddress.Enabled = false;
        //        txtContactNo.Enabled = false;
        //        txtDepositorName.Enabled = false;
        //        drp_lstDepositorType.Enabled = false;
        //    }
             
            
        //}
        //else if (btnEdit.Text == "Insert")
        //{
        //    if (txtDepositorName.Text != "" && txtAddress.Text != "" && txtContactNo.Text != "")
        //    {
        //        string stateId = Session["State_StateID"].ToString();
        //        string distId = Session["Depot_DistID"].ToString();
        //        //query = "if((select count(Depositor_Name)from [tbl_MetaData_DEPOSITOR] where Depositor_Name='" + depositorName + "' and   BranchId='" + BranchId + "') =0 and '" + depositorName + "' not in ('MPSCSC','MPWLC','FCI')) begin INSERT INTO [tbl_MetaData_DEPOSITOR] (State_ID,District_ID,Depot_ID,Depositor_Name,Depositor_Type,Address,Contact_No,BranchId) VALUES ('" + stateId + "', '" + distId + "', '" + depotId + "', '" + depositorName + "', '" + depositorType + "', '" + address + "', '" + ContactNo + "','"+BranchId+"') end";
        //        query = "if((select count(Depositor_Name)from [tbl_MetaData_DEPOSITOR] where Depositor_Name='" + depositorName + "' and   BranchId='" + BranchId + "') =0 and '" + depositorName + "' not in ('MPSCSC','MPWLC','FCI','DMO Markfed')) begin INSERT INTO [tbl_MetaData_DEPOSITOR] (State_ID,District_ID,Depot_ID,Depositor_Name,Depositor_Type,Address,Contact_No,BranchId,CreatedBy,CreatedDate,IsActive) VALUES ('" + stateId + "', '" + distId + "', '" + depotId + "', '" + depositorName + "', '" + depositorType + "', '" + address + "', '" + ContactNo + "','" + BranchId + "','" + Client_Ip + "',getdate(),'Y') end";

        //        try
        //        {
        //            if (con != null)
        //            {
        //                int Count = 0;
        //                con.Open();
        //                cmd.Connection = con;
        //                cmd.CommandText = query;
        //                Count=cmd.ExecuteNonQuery();
        //                if (Count > 0)
        //                {
        //                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully......'); </script> ");
        //                }
        //                else
        //                {
        //                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Not Inserted......'); </script> ");
        //                }
        //                con.Close();
        //                GetDepositor(depotId);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            lblMsg.Text = ex.Message.ToString();
        //        }
        //        finally
        //        {
        //            con.Close();
        //        }
        //        res = 2;
        //        btnEdit.Text = "Edit";
        //        btnCancel.Text = "New";
        //        txtAddress.Enabled = false;
        //        txtContactNo.Enabled = false;
        //        txtDepositorName.Enabled = false;
        //        drp_lstDepositorType.Enabled = false;
        //    }
        //    else
        //    {

        //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please enter Depositer name/Address/Contact No.'); </script> ");
        //    }
        //}
        try
        {
            if (ddldptype_select.SelectedItem.Text != "--Select--")
            {
                if (btnEdit.Text == "Submit" && ddldptype_select.SelectedItem.Text == "Cultivator")
                {
                    string mobiletxt = txtmobile.Text;
                    if (txtdepositor_name.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Depositor Name...')", true);
                    }
                    else if (txtf_name.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Father Name...')", true);
                    }
                    else if (mobiletxt.Length < 10)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter 10 Digits mobile No...')", true);
                    }
                    else if (txtaddress.Text=="")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Postal Address With Pin Code...')", true);
                    }
                    else
                    {
                        string Createddate = getDate_MDY(DateTime.Now.ToString("dd/MM/yyyy"));
                        string qry = "INSERT INTO [tbl_MetaData_DEPOSITOR] ([State_ID],[District_ID],[Depot_ID],[Depositor_Name],[Depositor_Type],[Address],[Contact_No],[IsActive],[CreatedBy],[CreatedDate],[BranchId],[D_FatherName],[TehshilID],[VillageName],[Cast_Category],[Aadhaar],[SamagraID],[PatwariHalkaNo],[RinPustikaNo],[Bank_ID],[Bank_BranchID],[IFSC_Code],[AccNo],[IsApprove],[DepositorType_ID]) VALUES ('23','" + DistID + "','" + depotId + "','" + txtdepositor_name.Text + "','" + ddldptype_select.SelectedItem.Text + "','" + txtaddress.Text + "','" + txtmobile.Text + "','Y','" + Client_Ip + "','" + Createddate + "','" + BranchId + "','" + txtf_name.Text + "','" + ddltehsil.SelectedValue.ToString() + "',N'" + ddlVillage.SelectedItem.Text + "','" + ddlcast.SelectedItem.Text + "','" + txtuid.Text + "','" + txtsamagrah.Text + "','" + txthlkano.Text + "','" + txtrinpustikano.Text + "','" + ddlBank.SelectedValue.ToString() + "','" + ddlBankBranch.SelectedValue.ToString() + "','" + txtifsc.Text + "','" + txtAccNo.Text + "','N','" + ddldptype_select.SelectedValue.ToString() + "')";
                        cmd = new SqlCommand(qry, con);
                        int s = cmd.ExecuteNonQuery();
                        if (s > 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Insert Successfully ......'); </script> ");
                           
                        }
                    }
                }
                    //For Coperative Society
                else if (btnEdit.Text == "Submit" && ddldptype_select.SelectedItem.Text == "Co-op Societies")
                {
                    string mobiletxt = txtmobile.Text;
                    if (ddlDepositor.SelectedItem.Text == "--Select--")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please select Society Name...')", true);
                    }                   
                    else
                    {
                        string Createddate = getDate_MDY(DateTime.Now.ToString("dd/MM/yyyy"));
                        string qry = "INSERT INTO [tbl_MetaData_DEPOSITOR] ([State_ID],[District_ID],[Depot_ID],[Depositor_Name],[Depositor_Type],[Address],[Contact_No],[IsActive],[CreatedBy],[CreatedDate],[BranchId],[D_FatherName],[TehshilID],[VillageName],[Cast_Category],[Aadhaar],[SamagraID],[PatwariHalkaNo],[RinPustikaNo],[Bank_ID],[Bank_BranchID],[IFSC_Code],[AccNo],[IsApprove],[DepositorType_ID],LicNum) VALUES ('23','" + DistID + "','" + depotId + "',N'" + ddlDepositor.SelectedItem.Text + "','" + ddldptype_select.SelectedItem.Text + "','','','Y','" + Client_Ip + "','" + Createddate + "','" + BranchId + "','','','','','','','','','','','','','N','" + ddldptype_select.SelectedValue.ToString() + "','" + ddlDepositor.SelectedValue + "')";
                        cmd = new SqlCommand(qry, con);
                        int s = cmd.ExecuteNonQuery();
                        if (s > 0)
                        {
                            GetSocietyDepositor();
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Insert Successfully ......'); </script> ");
                            Response.Redirect("~/Masters/DepositorMaster.aspx");

                        }
                    }
                }
                else if (btnEdit.Text == "Submit" && ddldptype_select.SelectedItem.Text != "Cultivator" && ddldptype_select.SelectedItem.Text != "Bhavantar Farmer")
                {
                    string mobiletxt = txtorgmobile.Text;
                    if (txtorgname.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Depositor/Organzation Name...')", true);
                    }
                    else if (txtorgapn.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Owner Name...')", true);
                    }
                    else if (mobiletxt.Length < 10)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter 10 Digits mobile No...')", true);
                    }
                    else if (txtorgaddress.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Postal Address With Pin Code...')", true);
                    }
                    else if (txtgstin.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter GSTIN No....')", true);
                    }
                    else if (txtorgpan.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter PAN No....')", true);
                    }
                    else if (txtAccNo.Text=="")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Bank Account No')", true);
                    }
                    else if (txtifsc.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter IFSC Code')", true);
                    }
                    else
                    {
                        string qry = "INSERT INTO [tbl_MetaData_DEPOSITOR] ([State_ID],[District_ID],[Depot_ID],[Depositor_Name],[APN],[Depositor_Type],[Address],[Contact_No],[IsActive],[CreatedBy],[CreatedDate],[BranchId],[TehshilID],[GSTIN],[PAN],[Bank_ID],[Bank_BranchID],[IFSC_Code],[AccNo],[IsApprove],[DepositorType_ID],[LicNum]) VALUES ('23','" + DistID + "','" + depotId + "','" + txtorgname.Text + "', '" + txtorgapn.Text + "', '" + ddldptype_select.SelectedItem.Text + "','" + txtorgaddress.Text + "','" + txtorgmobile.Text + "','Y', '" + Client_Ip + "',GETDATE(),'" + BranchId + "','" + ddlorgtehsil.SelectedValue.ToString() + "', '" + txtgstin.Text + "','" + txtorgpan.Text + "','" + ddlBank.SelectedValue.ToString() + "','" + ddlBankBranch.SelectedValue.ToString() + "', '" + txtifsc.Text + "','" + txtAccNo.Text + "','N','" + ddldptype_select.SelectedValue.ToString() + "','" + txtlicence.Text + "')";
                        cmd = new SqlCommand(qry, con);
                        int s = cmd.ExecuteNonQuery();
                        if (s > 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Insert Successfully ......'); </script> ");
                        }
                    }
                }

                else if (btnEdit.Text == "Update" && ddldptype_select.SelectedItem.Text == "Cultivator")
                {
                    MaxDPID = Depositor_Gridview.SelectedRow.Cells[1].Text.Trim();
                    string mobiletxt = txtmobile.Text;
                    int a = ChkDepositorStock();
                    if (a == 1)
                    {
                        if (txtdepositor_name.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Depositor Name...')", true);
                        }
                        else if (txtf_name.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Father Name...')", true);
                        }
                        else if (mobiletxt.Length < 10)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter 10 Digits mobile No...')", true);
                        }
                        else if (txtaddress.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Postal Address With Pin Code...')", true);
                        }
                        else
                        {
                            string qry2 = "insert into [tbl_MetaData_DEPOSITOR_log] select * from [tbl_MetaData_DEPOSITOR] where Depositor_ID ='" + MaxDPID + "' and BranchId= '" + Session["BranchID"].ToString() + "' ";
                            SqlCommand cmd2 = new SqlCommand(qry2, con);
                            int s2 = cmd2.ExecuteNonQuery();
                            if (s2 > 0)
                            {
                                string qry = "UPDATE [tbl_MetaData_DEPOSITOR] set [Depositor_Name]='" + txtdepositor_name.Text + "',[Depositor_Type]='" + ddldptype_select.SelectedItem.Text + "',[Address]='" + txtaddress.Text + "',[Contact_No]='" + txtmobile.Text + "',[D_FatherName]='" + txtf_name.Text + "',[TehshilID]='" + ddltehsil.SelectedValue.ToString() + "',[VillageName]=N'" + ddlVillage.SelectedItem.Text.Trim() + "',[Cast_Category]='" + ddlcast.SelectedItem.Text + "',[Aadhaar]='" + txtuid.Text + "',[SamagraID]='" + txtsamagrah.Text + "',[PatwariHalkaNo]='" + txthlkano.Text + "',[RinPustikaNo]='" + txtrinpustikano.Text + "',[Bank_ID]='" + ddlBank.SelectedValue.ToString() + "',[Bank_BranchID]='" + ddlBankBranch.SelectedValue.ToString() + "',[IFSC_Code]='" + txtifsc.Text + "',[AccNo]='" + txtAccNo.Text + "',[IsApprove]='N',[DepositorType_ID]='" + ddldptype_select.SelectedValue.ToString() + "',UpdatedBy='" + Client_Ip + "',UpdatedDate=GETDATE() where Depositor_ID ='" + MaxDPID + "' and BranchId= '" + Session["BranchID"].ToString() + "' ";
                                cmd = new SqlCommand(qry, con);
                                int s = cmd.ExecuteNonQuery();
                                if (s > 0)
                                {

                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Updated Successfully ......'); </script> ");

                                }
                            }
                        }
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some Stock Balance Can not Update......'); </script> ");
                    }
                }

                else if (btnEdit.Text == "Update" && ddldptype_select.SelectedItem.Text != "Cultivator" && ddldptype_select.SelectedItem.Text != "Bhavantar Farmer")
                {
                    string mobiletxt = txtorgmobile.Text;
                    MaxDPID = Depositor_Gridview.SelectedRow.Cells[1].Text.Trim();
                    int a = ChkDepositorStock();
                    if (a == 1)
                    {
                        if (txtorgname.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Depositor/Organzation Name...')", true);
                        }
                        else if (txtorgapn.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Owner Name...')", true);
                        }
                        else if (mobiletxt.Length < 10)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter 10 Digits mobile No...')", true);
                        }
                        else if (txtorgaddress.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Postal Address With Pin Code...')", true);
                        }
                        else
                        {
                            string qry = "insert into [tbl_MetaData_DEPOSITOR_log] select * from [tbl_MetaData_DEPOSITOR] where Depositor_ID ='" + MaxDPID + "' and BranchId= '" + Session["BranchID"].ToString() + "' ";
                            cmd = new SqlCommand(qry, con);
                            int s = cmd.ExecuteNonQuery();
                            if (s > 0)
                            {
                                string qry1 = "UPDATE [tbl_MetaData_DEPOSITOR] set [Depositor_Name]='" + txtorgname.Text + "',[Depositor_Type]='" + ddldptype_select.SelectedItem.Text + "',[Address]='" + txtorgaddress.Text + "',[Contact_No]='" + txtorgmobile.Text + "',[APN]='" + txtorgapn.Text + "',[TehshilID]='" + ddlorgtehsil.SelectedValue.ToString() + "',[GSTIN]='" + txtgstin.Text + "',[PAN]='" + txtorgpan.Text + "',[Bank_ID]='" + ddlBank.SelectedValue.ToString() + "',[Bank_BranchID]='" + ddlBankBranch.SelectedValue.ToString() + "',[IFSC_Code]='" + txtifsc.Text + "',[AccNo]='" + txtAccNo.Text + "',[IsApprove]='N',[DepositorType_ID]='" + ddldptype_select.SelectedValue.ToString() + "',UpdatedBy='" + Client_Ip + "',UpdatedDate=GETDATE(),LicNum='" + txtlicence.Text + "' where Depositor_ID ='" + MaxDPID + "' and BranchId= '" + Session["BranchID"].ToString() + "' ";
                                SqlCommand cmd1 = new SqlCommand(qry1, con);
                                int s1 = cmd1.ExecuteNonQuery();
                                if (s1 > 0)
                                {
                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Updated Successfully ......'); </script> ");
                                }
                            }
                        }
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some Stock Balance Can not Update......'); </script> ");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Something Error ..'); </script> ");
        }
    }

    public int ChkDepositorStock()
    {
        int chk = 0;
        string depositorNM = Depositor_Gridview.SelectedRow.Cells[2].Text.Trim();
        string strsql = "select convert(decimal(18,5),isnull(SUM(RecQty) - isnull(SUM(DelQty),0),0)) as AvlQty from View_WHRcurrentstock where Depositor_Name='" + depositorNM + "' and BranchID='" + Session["BranchID"].ToString() + "' ";
        SqlCommand cmd = new SqlCommand(strsql, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 1 )
        {
            if (ds.Tables[0].Rows[0]["AvlQty"].ToString() == "0.00000")
            {
                chk = 1;
            }
        }
        return chk;
    }
    //private void GetMaxID()
    //{
    //    try
    //    {
    //        if (con.State == ConnectionState.Closed)
    //        {
    //            con.Open();
    //        }
    //        string qry = "select MAX(Depositor_ID) as MAXID from tbl_MetaData_Depositor";
    //        SqlCommand cmd = new SqlCommand(qry, con);
    //        string maxid = cmd.ExecuteScalar().ToString();
    //        MaxDPID=Convert.ToInt32(maxid);
    //        if ((maxid == String.Empty) && maxid == "")
    //        {
    //            MaxDPID = 0 + 1;
    //        }
    //        else if ((maxid != String.Empty) && maxid != "" && MaxDPID != 0)
    //        {
    //            MaxDPID = MaxDPID + 1;
    //        }
    //    }
    //    catch (Exception)
    //    {
    //    }
    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
       
        //if (btnCancel.Text == "Cancel")
        //{
        //    ViewState["DepositorId"] = Depositor_Gridview.SelectedRow.Cells[1].Text.Trim();
        //    txtDepositorName.Text = Depositor_Gridview.SelectedRow.Cells[2].Text.Trim();
        //    string depositorType = Depositor_Gridview.SelectedRow.Cells[3].Text.Trim();
        //    drp_lstDepositorType.SelectedItem.Selected = false;
        //    foreach (ListItem lst in drp_lstDepositorType.Items)
        //    {
        //        if (lst.Value == depositorType)
        //        {
        //            lst.Enabled = true;
        //            lst.Selected = true;
        //        }
        //    }
        //    txtAddress.Text = Depositor_Gridview.SelectedRow.Cells[4].Text.Trim();
        //    txtContactNo.Text = Depositor_Gridview.SelectedRow.Cells[5].Text.Trim();
        //    txtAddress.Enabled = false;
        //    txtContactNo.Enabled = false;
        //    txtDepositorName.Enabled = false;
        //    drp_lstDepositorType.Enabled = false;
        //    btnEdit.Text = "Edit";
        //    btnCancel.Text = "New";
        // }
        //else if (btnCancel.Text == "New")
        //{
        //    //Panel1.Visible = true;
        //    //btnEdit.Text = "Insert";
        //    //txtAddress.Enabled = true;
        //    //txtContactNo.Enabled = true;
        //    //txtDepositorName.Enabled = true;
        //    //drp_lstDepositorType.Enabled = true;
        //    //txtAddress.Text = "";
        //    //txtContactNo.Text = "";
        //    //txtDepositorName.Text = "";
        //    //drp_lstDepositorType.SelectedItem.Selected = false;
        //}
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void Depositor_Gridview_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ViewState["DepositorId"] = Depositor_Gridview.SelectedRow.Cells[1].Text.Trim();
        //txtDepositorName.Text = Depositor_Gridview.SelectedRow.Cells[2].Text.Trim();
        //string depositorType = Depositor_Gridview.SelectedRow.Cells[3].Text.Trim();
        //drp_lstDepositorType.SelectedItem.Selected = false;
        //foreach (ListItem lst in drp_lstDepositorType.Items)
        //{1
        //    if (lst.Value == depositorType)
        //    {
        //        lst.Enabled = true;
        //        lst.Selected = true;
        //    }
        //}
        //txtAddress.Text = Depositor_Gridview.SelectedRow.Cells[4].Text.Trim();
        //txtContactNo.Text = Depositor_Gridview.SelectedRow.Cells[5].Text.Trim();
        //con.Close();
        //btnEdit.Text = "Edit";
        //btnCancel.Text = "New";
        if (Depositor_Gridview.SelectedRow.Cells[3].Text != "Institution")
        {
            if (Depositor_Gridview.SelectedRow.Cells[3].Text != "Bhavantar Farmer")
            {
                Label8.Text = "Update Depositor";
                MaxDPID = Depositor_Gridview.SelectedRow.Cells[1].Text.Trim();
                ddltypevisible.Visible = true;
                btnEdit.Text = "Update";
                Getdptype();
                GetBankDt();
                fatchDPdeatil();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('You Can Not Update Bhavantar Farmer......'); </script> ");
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('You Can Not Update Institution depositor......'); </script> ");
        }
    }

    protected void Depositor_Gridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string Client_Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string depotId = Session["Depot_DepotID"].ToString();
        string BranchId = Session["BranchID"].ToString();
        int _rowindex = e.RowIndex;
        string did = Depositor_Gridview.DataKeys[_rowindex].Value.ToString();

        if (did == "129" || did == "184" || did == "181")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('You Can Not Delete Institution depositor......'); </script> ");
            //query = "UPDATE [tbl_MetaData_DEPOSITOR] SET [Depositor_Name] = case when (([Depositor_Name] = 'MPSCSC' and [Depositor_Type] = 'Institution') OR '" + depositorName + "'='MPSCSC' ) then [Depositor_Name] when (([Depositor_Name] = 'MPWLC' and [Depositor_Type] = 'Institution') OR '" + depositorName + "'='MPWLC' ) then [Depositor_Name] when (([Depositor_Name] = 'FCI' and [Depositor_Type] = 'Institution') OR '" + depositorName + "'='FCI' ) then [Depositor_Name] else '" + depositorName + "' end, [Depositor_Type] = case when ([Depositor_Name] = 'MPSCSC' and [Depositor_Type] = 'Institution') then [Depositor_Type] when ([Depositor_Name] = 'MPWLC' and [Depositor_Type] = 'Institution') then [Depositor_Type] when ([Depositor_Name] = 'FCI' and [Depositor_Type] = 'Institution') then [Depositor_Type] else '" + depositorType + "' end,[Address] = '" + address + "', [Contact_No] = '" + ContactNo + "' WHERE [Depositor_ID] = '" + depositorid + "' and [Depositor_ID] not in (select Depositor_ID from [tbl_MetaData_DEPOSITOR],tbl_Storage_Arrival_Stock where [tbl_MetaData_DEPOSITOR].Depositor_Name=tbl_Storage_Arrival_Stock.Depositor_Name )";
        }
        else
        {
            try
            {

                string qryChk = "select sum(RecWeight-(IssueWeight+loss-gain)) as AvailQty,Depositor_Name,DepositorID from (select RecDetail.WHRId,RecWeight,RecDetail.DepositorID,isnull(IssueWeight,0) as IssueWeight,isnull(Loss,0) as Loss,isnull(Gain,0) as Gain,Depositor_Name from( select WHRId,SUM(Weight) as RecWeight,WHR.Depositor_Name,WHR.DepositorID from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID = '" + Session["BranchID"].ToString() + "' and WHR.DepositorID='" + CheckInt(did) + "' group by WHRId,WHR.Depositor_Name,DepositorID ) as RecDetail left join ( select Depositor_WHR_Id,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and GP.BranchID='" + Session["BranchID"].ToString() + "' group by DSD.Depositor_WHR_Id ) as DelDetail on RecDetail.WHRId=DelDetail.Depositor_WHR_Id ) as mrh group by Depositor_Name,DepositorID";
                cmd = new SqlCommand(qryChk, con);
                string str4 = cmd.ExecuteScalar().ToString();
                decimal Davlqty = Convert.ToDecimal(str4);
                if (Davlqty < 0)
                {
                    string log_qry = "insert into tbl_MetaData_DEPOSITOR_Log select [Depositor_ID],[State_ID],[District_ID],[Depot_ID],[Depositor_Name],[Depositor_Type],[Address],[Contact_No],[IsActive],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + Client_Ip + "',getdate(),[BranchId] from [tbl_MetaData_DEPOSITOR] where Depositor_ID='" + CheckInt(did) + "'";
                    cmd = new SqlCommand(log_qry, con);
                    int s = cmd.ExecuteNonQuery();
                    con.Close();
                    if (s > 0)
                    {
                        string query = "DELETE FROM [tbl_MetaData_DEPOSITOR] WHERE (Depositor_name not in ('MPSCSC','MPWLC','FCI','DMO Markfed')) and [Depositor_ID] = " + CheckInt(did) + "  and Depositor_ID not in (select Depositor_ID from [tbl_MetaData_DEPOSITOR],tbl_Storage_Arrival_Stock where [tbl_MetaData_DEPOSITOR].Depositor_Name=tbl_Storage_Arrival_Stock.Depositor_Name )";
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data deleted Successfully......'); </script> ");
                    }
                    con.Close();
                    GetDepositor(depotId);
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('You Can Not Delete depositor Because Some Stock Remaining ......'); </script> ");
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
        }
    }
    Int32 CheckInt(string Val)
    {

        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        Int32 ValF = Int32.Parse(ValS);
        return ValF;

    }

    //----------------------New Changes On Page ---------------


    
    protected void Depositor_Gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string depotId = Session["Depot_DepotID"].ToString();
        string BranchId = Session["BranchID"].ToString();
        Depositor_Gridview.PageIndex = e.NewPageIndex;
        GetDepositor(BranchId);

    }

    protected void btnnewdp_Click(object sender, EventArgs e)
    {
        Label8.Text = "Add New Depositor";
        ddlchangeDP.Visible = false;
        Label22.Visible = false;
        ddlorgcngtype.Visible = false;
        Label25.Visible = false;
        ddltypevisible.Visible = true;
        Getdptype();
        GetBankDt();
        btnEdit.Text = "Submit";
        pnlnewdp.Visible = false;
        pnlotherdp.Visible = false;
        pnlbank.Visible = false;
        btnapprove.Visible = false;
    }
    private void GetTehsil()
    {
        try
        {
            string qry = "select TehsilCode,Tehsil_Name from Tehsils where District_Code ='" + Session["Depot_DistID"].ToString() +"'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0 && ddldptype_select.SelectedItem.Text == "Cultivator" || ddldptype_select.SelectedItem.Text == "Bhavantar Farmer")
            {
                ddltehsil.DataSource = ds.Tables[0];
                ddltehsil.DataTextField = "Tehsil_Name";
                ddltehsil.DataValueField = "TehsilCode";
                ddltehsil.DataBind();
                ddltehsil.Items.Insert(0, "--Select--");
            }
            else if (ds.Tables[0].Rows.Count > 0 && ddldptype_select.SelectedItem.Text != "Cultivator" && ddldptype_select.SelectedItem.Text != "Bhavantar Farmer")
            {
                ddlorgtehsil.DataSource = ds.Tables[0];
                ddlorgtehsil.DataTextField = "Tehsil_Name";
                ddlorgtehsil.DataValueField = "TehsilCode";
                ddlorgtehsil.DataBind();
                ddlorgtehsil.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception)
        {
        }
    }

    public void Get_Village()
    {
        //string DistrictId = ddlDistrict.SelectedValue.ToString();
        string qry = "SELECT [Hindi_Village] FROM [Intergrated_MP_STORAGE].[dbo].[VillageMaster] where Tehsil_ID='" + ddltehsil.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        DataSet ds1 = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds1);
        if (ds1 == null)
        {
        }
        else
        {
            ddlVillage.DataSource = ds1.Tables[0];
            ddlVillage.DataTextField = "Hindi_Village";
            ddlVillage.DataValueField = "Hindi_Village";
            ddlVillage.DataBind();
            ddlVillage.Items.Insert(0, "--Select--");
        }
    }

    private void Getdptype()
    {
        try
        {
            string qry = "select Depositor_Type,Depositor_Type_Id from tbl_MetaData_Depositor_Type where Depositor_Type_Id !='4' order by Depositor_Type";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldptype_select.DataSource = ds.Tables[0];
                ddldptype_select.DataTextField = "Depositor_Type";
                ddldptype_select.DataValueField = "Depositor_Type_Id";
                ddldptype_select.DataBind();
                ddldptype_select.Items.Insert(0, "--Select--");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlchangeDP.DataSource = ds.Tables[0];
                ddlchangeDP.DataTextField = "Depositor_Type";
                ddlchangeDP.DataValueField = "Depositor_Type_Id";
                ddlchangeDP.DataBind();
                ddlchangeDP.Items.Insert(0, "--Select--");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlorgcngtype.DataSource = ds.Tables[0];
                ddlorgcngtype.DataTextField = "Depositor_Type";
                ddlorgcngtype.DataValueField = "Depositor_Type_Id";
                ddlorgcngtype.DataBind();
                ddlorgcngtype.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception)
        {
        }
    }

    private void GetBankDt()
    {
        try
        {
            string qry = "select distinct BANK,BankID from IfscBankmar15 where BankID is not null and DistrictId='" + Session["Depot_DistID"].ToString() + "' order by BANK";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlBank.DataSource = ds.Tables[0];
                ddlBank.DataTextField = "BANK";
                ddlBank.DataValueField = "BankID";
                ddlBank.DataBind();
                ddlBank.Items.Insert(0, "--Select--");
                ddlBank.Items.Insert(1, new ListItem("Other", ""));
            }
        }
        catch (Exception)
        {
        }
    }

    protected void ddltehsil_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_Village();
    }
    protected void ddldptype_select_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldptype_select.SelectedItem.Text == "Cultivator")
        {
            pnlnewdp.Visible = true;
            pnlbank.Visible = true;
            btnapprove.Visible = true;
            BPanel.Visible = false;
            pnlotherdp.Visible = false;
            GetTehsil();
        }

        else if (ddldptype_select.SelectedItem.Text == "Bhavantar Farmer")
        {
            BPanel.Visible = true;
            pnlnewdp.Visible = false;
            pnlbank.Visible = false;
            btnapprove.Visible = false;
            pnlotherdp.Visible = false;
        }
        else if (ddldptype_select.SelectedItem.Text == "Co-op Societies")
        {
            GetSocietyDepositor();
            pnlCopSociety.Visible = true;
            BPanel.Visible = false;
            pnlnewdp.Visible = false;
            pnlbank.Visible = false;
            btnapprove.Visible = true;
            pnlotherdp.Visible = false;
        }
        else if (ddldptype_select.SelectedItem.Text != "Cultivator" && ddldptype_select.SelectedItem.Text != "--Select--" && ddldptype_select.SelectedItem.Text != "Bhavantar Farmer")
        {
            pnlnewdp.Visible = false;
            pnlotherdp.Visible = true;
            BPanel.Visible = false;
            pnlbank.Visible = true;
            btnapprove.Visible = true;
            GetTehsil();
        }
        else
        {
            pnlnewdp.Visible = false;
            pnlotherdp.Visible = false;
            pnlbank.Visible = false;
            btnapprove.Visible = false;
            BPanel.Visible = false;
        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtifsc.Text = "";
            if (ddlBank.SelectedItem.Text.ToString() != "Other" && ddlBank.SelectedItem.Text.ToString() != "--Select--")
            {
                Label19.Visible = true;
                ddlBankBranch.Visible = true;
                string qry = "select BRANCH,ID from IfscBankmar15 where BankID='" + ddlBank.SelectedValue.ToString() + "' and DistrictId='" + Session["Depot_DistID"].ToString() + "' order by BRANCH";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlBankBranch.DataSource = ds.Tables[0];
                    ddlBankBranch.DataTextField = "BRANCH";
                    ddlBankBranch.DataValueField = "ID";
                    ddlBankBranch.DataBind();
                    ddlBankBranch.Items.Insert(0, "--Select--");
                }
            }
            else if (ddlBank.SelectedItem.Text.ToString() == "Other")
            {
                ddlBankBranch.DataSource = null;
                ddlBankBranch.DataBind();
                Label19.Visible = false;
                ddlBankBranch.Visible = false;
            }
            else
            {
            }
        }
        catch (Exception)
        {

        }
    }
    protected void ddlBankBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string qry = "select IFSC from IfscBankmar15 where BankID='" + ddlBank.SelectedValue.ToString() + "' and DistrictId='" + Session["Depot_DistID"].ToString() + "' and ID='" + ddlBankBranch.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtifsc.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();
            }
        }
        catch (Exception)
        {
        }
    }

    private void fatchDPdeatil()
    {
        try
        {
            string qry = "select * from tbl_MetaData_DEPOSITOR where Depositor_ID ='" + MaxDPID + "' and BranchId= '" + Session["BranchID"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string DPtypeID = ds.Tables[0].Rows[0]["DepositorType_ID"].ToString();
                if (DPtypeID == "1")
                {
                    ddldptype_select.SelectedValue = ds.Tables[0].Rows[0]["DepositorType_ID"].ToString();
                    ddldptype_select_SelectedIndexChanged(null, EventArgs.Empty);
                    //ddlchangeDP.SelectedValue = ds.Tables[0].Rows[0]["DepositorType_ID"].ToString();
                    if (ds.Tables[0].Rows[0]["Bank_ID"].ToString() != "--Select--" && ds.Tables[0].Rows[0]["Bank_ID"].ToString() != "")
                    {
                        ddlBank.SelectedValue = ds.Tables[0].Rows[0]["Bank_ID"].ToString();
                        ddlBank_SelectedIndexChanged(null, EventArgs.Empty);
                        ddlBankBranch.SelectedValue = ds.Tables[0].Rows[0]["Bank_BranchID"].ToString();
                    }
                    txtdepositor_name.Text = ds.Tables[0].Rows[0]["Depositor_Name"].ToString();
                    txtf_name.Text = ds.Tables[0].Rows[0]["D_FatherName"].ToString();
                    txtmobile.Text = ds.Tables[0].Rows[0]["Contact_No"].ToString();
                    txtaddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                    txthlkano.Text = ds.Tables[0].Rows[0]["PatwariHalkaNo"].ToString();
                    txtrinpustikano.Text = ds.Tables[0].Rows[0]["RinPustikaNo"].ToString();
                    txtifsc.Text = ds.Tables[0].Rows[0]["IFSC_Code"].ToString();
                    txtAccNo.Text = ds.Tables[0].Rows[0]["AccNo"].ToString();
                    txtuid.Text = ds.Tables[0].Rows[0]["Aadhaar"].ToString();
                    txtsamagrah.Text = ds.Tables[0].Rows[0]["SamagraID"].ToString();
                    if (ds.Tables[0].Rows[0]["TehshilID"].ToString() != "--Select--" && ds.Tables[0].Rows[0]["TehshilID"].ToString() != "")
                    {
                        ddltehsil.SelectedValue = ds.Tables[0].Rows[0]["TehshilID"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["Cast_Category"].ToString() != "--Select--" && ds.Tables[0].Rows[0]["Cast_Category"].ToString() != "")
                    {
                        ddlcast.SelectedItem.Text = ds.Tables[0].Rows[0]["Cast_Category"].ToString();
                    }
                }
                else  if (DPtypeID != "1")
                {
                    ddldptype_select.SelectedValue = ds.Tables[0].Rows[0]["DepositorType_ID"].ToString();
                    ddldptype_select_SelectedIndexChanged(null, EventArgs.Empty);
                    ddlorgcngtype.SelectedValue = ds.Tables[0].Rows[0]["DepositorType_ID"].ToString();
                    if (ds.Tables[0].Rows[0]["Bank_ID"].ToString() != "--Select--" && ds.Tables[0].Rows[0]["Bank_ID"].ToString() != "")
                    {
                        ddlBank.SelectedValue = ds.Tables[0].Rows[0]["Bank_ID"].ToString();
                        ddlBank_SelectedIndexChanged(null, EventArgs.Empty);
                        ddlBankBranch.SelectedValue = ds.Tables[0].Rows[0]["Bank_BranchID"].ToString();
                    }
                    txtorgname.Text = ds.Tables[0].Rows[0]["Depositor_Name"].ToString();
                    txtorgapn.Text = ds.Tables[0].Rows[0]["APN"].ToString();
                    txtorgmobile.Text = ds.Tables[0].Rows[0]["Contact_No"].ToString();
                    txtorgaddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                    txtifsc.Text = ds.Tables[0].Rows[0]["IFSC_Code"].ToString();
                    txtAccNo.Text = ds.Tables[0].Rows[0]["AccNo"].ToString();
                    txtgstin.Text = ds.Tables[0].Rows[0]["GSTIN"].ToString();
                    txtorgpan.Text= ds.Tables[0].Rows[0]["PAN"].ToString();
                    txtlicence.Text = ds.Tables[0].Rows[0]["LicNum"].ToString();
                    if (ds.Tables[0].Rows[0]["TehshilID"].ToString() != "--Select--" && ds.Tables[0].Rows[0]["TehshilID"].ToString() != "")
                    {
                        ddlorgtehsil.SelectedValue = ds.Tables[0].Rows[0]["TehshilID"].ToString();
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }
    private void cleardata()
    {
        try
        {
            txtAccNo.Text = "";
            txtifsc.Text = "";
            ddlBank.SelectedIndex = -1;
            ddlcast.SelectedIndex = -1;
            txtdepositor_name.Text = "";
            txtf_name.Text = "";
            txtmobile.Text = "";
            txtorgaddress.Text = "";
            txthlkano.Text = "";
            txtgstin.Text = "";
            txthlkano.Text = "";
            txtaddress.Text = "";
            txtorgapn.Text = "";
            txtorgpan.Text = "";
            ddlorgtehsil.SelectedIndex = -1;
            ddltehsil.SelectedIndex = -1;
            ddlVillage.SelectedIndex = -1;
            txtorgmobile.Text = "";
        }
        catch (Exception ex)
        {

        }
    }

    protected void SearchRegBtn_Click(object sender, EventArgs e)
    {
        try
        {
            string FrmRegNo = BtxtRegiNo.Text;
            BhawanterFarmerGetRegDetail.Web_Service_For_Emandi BhawanterFrm = new BhawanterFarmerGetRegDetail.Web_Service_For_Emandi();
            sqlds = BhawanterFrm.FarmerRegistrationRequestById(FrmRegNo, "EMandi2017", "Kharif#2017");
            if (sqlds.Tables[0].Rows.Count > 0)
            {
                BtxtRegiNo.Enabled = false;
                sqlds.Tables[0].Columns.Add("Commodity");
                for (int intCount = 0; intCount < sqlds.Tables[0].Rows.Count; intCount++)
                {
                    string id = sqlds.Tables[0].Rows[intCount]["crpcode"].ToString();
                    {
                        SqlCommand cmdd = new SqlCommand("select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id='" + id + "'", con);
                        con.Open();
                        string comm = (string)cmdd.ExecuteScalar().ToString();
                        sqlds.Tables[0].Rows[intCount]["Commodity"] = comm;
                        con.Close();
                        
                    }
                }
                Bsavebtn.Visible = true;
                FarmerGrid.DataSource = sqlds;
                FarmerGrid.DataBind();
                FarmerGrid.Columns[8].Visible = false;
                FarmerGrid.Columns[10].Visible = false;
                FarmerGrid.Columns[11].Visible = false;
                FarmerGrid.Columns[12].Visible = false;
                FarmerGrid.Columns[13].Visible = false;
                FarmerGrid.Columns[14].Visible = false;
                FarmerGrid.Columns[15].Visible = false;
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No Record Found..'); </script> ");
                Bsavebtn.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
        finally
        { 
            con.Close(); 
        }
    }

    protected void chk_Insert_CheckedChanged(object sender, EventArgs e)
    {
        FarmerGrid.Enabled = false;
        try
        {
            for (int k = 0; k < FarmerGrid.Rows.Count; k++)
            {
                if (((CheckBox)FarmerGrid.Rows[k].FindControl("chk_Insert")).Checked == true)
                {
                    string frmid = FarmerGrid.Rows[k].Cells[1].Text.Trim();
                    Session["cropcode_fatch"] = FarmerGrid.Rows[k].Cells[15].Text.Trim();
                    BhawanterFarmerGetRegDetail.Web_Service_For_Emandi BhawanterFrm = new BhawanterFarmerGetRegDetail.Web_Service_For_Emandi();
                    DataSet ds1 = BhawanterFrm.FarmerLandRecords("EMandi2017", "Kharif#2017", frmid, Session["cropcode_fatch"].ToString());
                    BCropGrid.DataSource = ds1;
                    BCropGrid.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
           con.Close();
        }
    }
    protected void Bsavebtn_Click(object sender, EventArgs e)
    {
       
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            sqltran = con.BeginTransaction();
            string Client_Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string qry1 = "select Depositor_ID,LicNum from tbl_MetaData_DEPOSITOR where LicNum='" + BtxtRegiNo.Text + "' ";
            cmd = new SqlCommand(qry1, con ,sqltran);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["DepositorIDAlready"] = ds.Tables[0].Rows[0]["Depositor_ID"].ToString();
            }
            else
            {
                foreach (GridViewRow gr2 in FarmerGrid.Rows)
                {
                    CheckBox chk_Insert = new CheckBox();
                    chk_Insert = (CheckBox)gr2.Cells[0].FindControl("chk_Insert");
                    if (chk_Insert.Checked == true)
                    {
                        string FRegNo = gr2.Cells[1].Text.ToString();
                        string FarmerName = gr2.Cells[2].Text.ToString();
                        string FarmerFName = gr2.Cells[3].Text.ToString();
                        string FcontactNo = gr2.Cells[4].Text.ToString();
                        string FVillage = gr2.Cells[6].Text.ToString();
                        string Fsamagrah = gr2.Cells[7].Text.ToString();
                        string Fuid = gr2.Cells[8].Text.ToString();
                        string Fcast = gr2.Cells[9].Text.ToString();
                        string Fpat = gr2.Cells[10].Text.ToString();
                        string FRin = gr2.Cells[11].Text.ToString();
                        string FTehsil = gr2.Cells[12].Text.ToString();
                        string Fifsc = gr2.Cells[13].Text.ToString();
                        string FAcc = gr2.Cells[14].Text.ToString();
                        string Fcropcode = gr2.Cells[15].Text.ToString();
                        string qry = "INSERT INTO [tbl_MetaData_DEPOSITOR] ([State_ID],[District_ID],[Depot_ID],[Depositor_Name],[Depositor_Type],[Contact_No],[IsActive],[CreatedBy],[CreatedDate],[BranchId],[D_FatherName],[TehshilID],[VillageName],[Cast_Category],[Aadhaar],[SamagraID],[PatwariHalkaNo],[RinPustikaNo],[IFSC_Code],[AccNo],[IsApprove],[DepositorType_ID],[LicNum]) VALUES ('23','" + Session["Depot_DistID"].ToString() + "','" + Session["Depot_DepotID"].ToString() + "','" + FarmerName + "','" + ddldptype_select.SelectedItem.Text + "','" + FcontactNo + "','Y','" + Client_Ip + "',GETDATE(),'" + Session["BranchID"].ToString() + "','" + FarmerFName + "','" + FTehsil + "',N'" + FVillage + "',N'" + Fcast + "','" + Fuid + "','" + Fsamagrah + "','" + Fpat + "','" + FRin + "','" + Fifsc + "','" + FAcc + "','N','" + ddldptype_select.SelectedValue.ToString() + "','" + FRegNo + "')";
                        cmd = new SqlCommand(qry, con,sqltran);
                        int s = cmd.ExecuteNonQuery();
                    }
                }
                cmd = new SqlCommand("select Depositor_ID,LicNum from tbl_MetaData_DEPOSITOR where LicNum='" + BtxtRegiNo.Text + "'", con, sqltran);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                Session["DepositorIDAlready"] = ds2.Tables[0].Rows[0]["Depositor_ID"].ToString();
            }
            string frm = BtxtRegiNo.Text;
            string crp = Session["cropcode_fatch"].ToString();
            string chkland = "select * from tbl_FarmerLandRecord where Farmer_Id='" + frm + "' and crpcode='" + crp + "'";
            cmd = new SqlCommand(chkland, con, sqltran);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('This Depositor Already Save Record For this Commodity..'); </script> ");
            }
            else
            {
                foreach (GridViewRow gr3 in BCropGrid.Rows)
                {
                    CheckBox chk_land = new CheckBox();
                    chk_land = (CheckBox)gr3.Cells[0].FindControl("chk_Insert_land");
                    if (chk_land.Checked == true)
                    {
                        string FLRegNo = gr3.Cells[2].Text.ToString();
                        string crpcd = gr3.Cells[4].Text.ToString();
                        string rkba = gr3.Cells[5].Text.ToString();
                        string khasra = gr3.Cells[6].Text.ToString();
                        string qry = "INSERT INTO [tbl_FarmerLandRecord] ([distirctID],[BranchID],[DepositorID],[Farmer_Id],[crpcode],[KhasaraNo],[Rakba],[createdBy],[CreatedDate]) VALUES ('" + Session["Depot_DistID"].ToString() + "','" + Session["BranchID"].ToString() + "','" + Session["DepositorIDAlready"].ToString() + "', '" + FLRegNo + "','" + crpcd + "','" + khasra + "','" + rkba + "','" + Client_Ip + "',GETDATE())";
                        cmd = new SqlCommand(qry, con,sqltran);
                        int s = cmd.ExecuteNonQuery();
                        if (s > 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Successfully Save Record..'); </script> ");
                            sqltran.Commit();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Something error..'); </script> ");
            
        }
        finally
        {
            sqltran.Dispose();
            con.Close();
        }

    }
    public void GetSocietyDepositor()
    {
        ddlDepositor.ClearSelection();
        string Procurement_Type = "";
        string query2 = "";
        Procurement_Type = ddlProcType.SelectedItem.Text;
        if (Procurement_Type == "Dalhan/Tilhan Procurement")
        {
            //string query2 = "select distinct (S.Society_Name+'('+KR.Purchase_Center+')') as Depositor_Name,KR.Purchase_Center as Depositor_ID from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as KR inner join MPSCSC.dbo.Society_Pulses18 as S on S.PCID=KR.Purchase_Center where KR.Branch_Id='" + Session["BranchId"].ToString() + "'";
            query2 = "select distinct (S.Society_Name+'('+KR.Purchase_Center+')') as Depositor_Name,KR.Purchase_Center as Depositor_ID from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as KR inner join MPSCSC.dbo.Society_Pulses18 as S on S.PCID=KR.Purchase_Center where KR.Branch_Id='" + Session["BranchId"].ToString() + "' and KR.Purchase_Center not in (select distinct ISNULL(LicNum,'') from tbl_MetaData_DEPOSITOR where BranchId='" + Session["BranchId"].ToString() + "')";
        }
        else if (Procurement_Type == "Paddy Procurement")
        {
            query2 = "select distinct (S.Society_Name+'('+KR.Purchase_Center+')') as Depositor_Name,KR.Purchase_Center as Depositor_ID from MPSCSC.dbo.Acceptance_Note_Kharif2018 as KR inner join MPSCSC.dbo.Society_Kharif18 as S on S.Society_Id=KR.Purchase_Center where KR.Branch_Id='" + Session["BranchId"].ToString() + "' and KR.Purchase_Center not in (select distinct ISNULL(LicNum,'') from tbl_MetaData_DEPOSITOR where BranchId='" + Session["BranchId"].ToString() + "')";
        }
        else if (Procurement_Type == "Coarse Grain Procurement")
        {
            query2 = "select distinct (S.Society_Name+'('+KR.Purchase_Center+')') as Depositor_Name,KR.Purchase_Center as Depositor_ID from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as KR inner join MPSCSC.dbo.Society_Kharif18 as S on S.Society_Id=KR.Purchase_Center where KR.Branch_Id='" + Session["BranchId"].ToString() + "' and KR.Purchase_Center not in (select distinct ISNULL(LicNum,'') from tbl_MetaData_DEPOSITOR where BranchId='" + Session["BranchId"].ToString() + "')";
        }
        else if (Procurement_Type == "Wheat Procurement 2019-20")
        {
            //query2 = "select distinct (S.Society_Name+'('+KR.Purchase_Center+')') as Depositor_Name,KR.Purchase_Center as Depositor_ID from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as KR inner join MPSCSC.dbo.Society_Kharif18 as S on S.Society_Id=KR.Purchase_Center where KR.Branch_Id='" + Session["BranchId"].ToString() + "' and KR.Purchase_Center not in (select distinct ISNULL(LicNum,'') from tbl_MetaData_DEPOSITOR where BranchId='" + Session["BranchId"].ToString() + "')";
            query2 = "select distinct (S.Society_Name+'('+KR.Purchase_Center+')') as Depositor_Name,KR.Purchase_Center as Depositor_ID from MPSCSC.dbo.Acceptance_Note_Rabi2019 as KR inner join MPSCSC.dbo.UparjanKendra_Wht2019 as S on S.Cntr_ID=KR.Purchase_Center where KR.Branch_Id='" + Session["BranchId"].ToString() + "' and KR.Purchase_Center not in (select distinct ISNULL(LicNum,'') from tbl_MetaData_DEPOSITOR where BranchId='" + Session["BranchId"].ToString() + "')";

        }
        else if (Procurement_Type == "Wheat Procurement 2020-21")
        {
            query2 = "select distinct (S.Society_Name+'('+KR.Purchase_Center+')') as Depositor_Name,KR.Purchase_Center as Depositor_ID from MPSCSC.dbo.Acceptance_Note_Rabi2020 as KR inner join MPSCSC.dbo.UparjanKendra_Wht2020 as S on S.Cntr_ID=KR.Purchase_Center where KR.Branch_Id='" + Session["BranchId"].ToString() + "' and KR.Purchase_Center not in (select distinct ISNULL(LicNum,'') from tbl_MetaData_DEPOSITOR where BranchId='" + Session["BranchId"].ToString() + "')";

        }
        SqlCommand cmd2 = new SqlCommand(query2, con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count > 0)
        {
            ddlDepositor.DataSource = ds2;
            ddlDepositor.DataTextField = "Depositor_Name";
            ddlDepositor.DataValueField = "Depositor_ID";
            ddlDepositor.DataBind();
            ddlDepositor.Items.Insert(0, "--Select--");
            //ddlDepositor.SelectedValue = "10535";
        }
        else
        {
            ddlDepositor.DataSource = null;
            //ddlDepositor.DataTextField = "Depositor_Name";
            //ddlDepositor.DataValueField = "Depositor_ID";
            ddlDepositor.DataBind();
            //ddlDepositor.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlProcType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSocietyDepositor();
    }
}
