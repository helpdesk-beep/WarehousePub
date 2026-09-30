using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using MPSCSC_DepotBranchIssueCenter;

public partial class StatePages_AddBranch : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid="";
    MPSCSC_DepotBranchIssueCenter.MPSCSC_DepotBranchDetails BranchIssueCenter = new MPSCSC_DepotBranchIssueCenter.MPSCSC_DepotBranchDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            if (!IsPostBack)
            {
                string PopMsg = "";
                PopMsg = Request.QueryString["PopMsg"];
                if (Request.QueryString["PopMsg"] != null)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + PopMsg + "'); </script> ");
                }
                fillDistrict();
                fillIssuecenter();
                FillBranchList();
                getlogintype();
            }
        }
    }


    private void fillDistrict()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

                if (Session["Region_ID"].ToString() != null)
                {
                    region = Session["Region_ID"].ToString();

                }
            }

            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            }
            else
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
            }
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
               
               
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
    private void getlogintype()
    {
        try
        {
            string str = "SELECT * FROM [Storage_Agency_type]";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbranchtype.DataSource = ds.Tables[0];
                ddlbranchtype.DataTextField = "Storage_Agency";
                ddlbranchtype.DataValueField = "Storage_Agency_ID";
                ddlbranchtype.DataBind();
                ddlbranchtype.Items.Insert(0, "---Select---");
            }
            else
            {
                ddlbranchtype.Items.Clear();
            }

        }

        catch (Exception ex)
        {
           
        }
    }
    private void fillIssuecenter()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

                if (Session["Region_ID"].ToString() != null)
                {
                    region = Session["Region_ID"].ToString();

                }
            }

            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                 query = "SELECT DepotID,DistrictId,DepotName FROM [dbo].[tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "'";
                //query = "SELECT DepotID,DistrictId,DepotName FROM [mpscsc].[dbo].[tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "'";
            }
            else
            {
                query = "SELECT [District_Id],[District_Name],Region_ID FROM [dbo].[tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
            }
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlissuecenter.Items.Clear();
                ddlissuecenter.DataSource = ds.Tables[0];
                ddlissuecenter.DataTextField = "DepotName";
                ddlissuecenter.DataValueField = "DepotID";
                ddlissuecenter.DataBind();
                

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


    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillIssuecenter();
        FillBranchList();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtbranch.Text != "" && ddlbranchtype.SelectedValue.ToString() != "---Select---")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
            string GateP_No = "";
            //string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
            //String ecn = System.Environment.MachineName;
            //string HostName = computer_name[0].ToString();
            // count branch id
            if (btnsubmit.Text == "Submit")
            {
                qry = "select isnull(Max(BranchId),0) from tbl_MetaData_DEPOT where [DistrictId]='" + ddlDistrict.SelectedValue.ToString() + "'";
                cmd = new SqlCommand(qry, con); // check GatePass_No present in tbl_Storage_GatePass_Enrty table
                string str3 = cmd.ExecuteScalar().ToString();
                if (Convert.ToInt64(str3) != 0)
                {
                    GateP_No = Convert.ToString(Convert.ToInt64(str3));
                    if (GateP_No != String.Empty || GateP_No != "")
                    {

                        qry = "select count(BranchId) from tbl_MetaData_DEPOT where [DistrictId]='" + ddlDistrict.SelectedValue.ToString() + "'";
                        cmd = new SqlCommand(qry, con); // check GatePass_No present in tbl_Storage_GatePass_Enrty table
                        string maxcount = cmd.ExecuteScalar().ToString();
                        if (Convert.ToInt16(maxcount) > 0)
                        {
                            if (GateP_No.Length == 7)
                            {
                                GateP_No = GateP_No + "01";
                            }
                            else
                            {
                                GateP_No = Convert.ToString(Convert.ToInt64(GateP_No) + 1);
                                // GateP_No = GateP_No + "N";
                            }
                        }
                    }
                }
                else
                {
                    string Distid = ddlDistrict.SelectedValue.ToString();
                    GateP_No = Distid + "0101";
                }
                string branchtype = ddlbranchtype.SelectedItem.Text.ToString();
                if (branchtype == "MPWLC")
                {
                    qry = "insert into [MetaDataBranchWithIssueCenter] ([DepotId],[IssueCenterId],[DistrictId],[CreatedDate],[CreatedBy],[CretaedBYIP],[UpdatedDate],[BranchName],[IssueCenterName],BranchID,[BranchTypeID],[BranchPwd]) values ('" + GateP_No + "','" + ddlissuecenter.SelectedValue.ToString() + "','" + ddlDistrict.SelectedValue.ToString() + "',getdate(),'" + ClientIP + "','" + ClientIP + "',getdate(),'" + txtbranch.Text + "','" + ddlissuecenter.SelectedItem.Text + "','" + GateP_No + "','O','bm2015')";
                    cmd = new SqlCommand(qry, con);
                    int c = cmd.ExecuteNonQuery();
                    /*Adding Value for Branch Details in MetaDataBranchWithIssueCenter_Test table(Test Table)*/
                    //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                    //BranchIssueCenter.AddMetaDetaBranchWithIssueCenter(GateP_No
                    //    , ddlissuecenter.SelectedValue.ToString()
                    //    , ddlDistrict.SelectedValue.ToString()
                    //    , DateTime.Now
                    //    , ClientIP
                    //    , ClientIP
                    //    , DateTime.Now
                    //    , txtbranch.Text.ToString()
                    //    , ddlissuecenter.SelectedItem.Text.ToString()
                    //    , GateP_No
                    //    , "O"
                    //    , "bm2015");
                    if (c > 0)
                    {
                        qry = "insert into tbl_MetaData_DEPOT (DepotID,[StateId],[DistrictId],DepoTypeID,[DepotName],[CreatedBy],[CreatedDate],[DepoBelongs],[BranchId]) values ('" + GateP_No + "','23','" + ddlDistrict.SelectedValue.ToString() + "','O','" + txtbranch.Text.Trim() + "','" + ClientIP + "',getdate(),'" + ddlbranchtype.SelectedItem.Text + "','" + GateP_No + "')";
                        cmd = new SqlCommand(qry, con);
                        int d = cmd.ExecuteNonQuery();
                        if (d > 0)
                        {
                            qry = "  insert into [Storage_Login]([User_Name],[Password],[DepotId],[DistrictId],[Fname],[Scope],[MasterPassword],[Access_Restrict],BranchId) values ('" + txtbranch.Text + "','wlc2015','" + ddlissuecenter.SelectedValue.ToString() + "','" + ddlDistrict.SelectedValue.ToString() + "','" + txtbranch.Text + "','1','whr2015','N','" + GateP_No + "' )";
                            cmd = new SqlCommand(qry, con);
                            int x1 = cmd.ExecuteNonQuery();
                            if (x1 > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('The Record is added successfully')", true);
                            }
                        }
                    }

                }
                else
                {
                    qry = "insert into [MetaDataBranchWithIssueCenter] ([DepotId],[IssueCenterId],[DistrictId],[CreatedDate],[CreatedBy],[CretaedBYIP],[UpdatedDate],[BranchName],[IssueCenterName],BranchID,[BranchTypeID],[BranchPwd]) values ('" + GateP_No + "','" + ddlissuecenter.SelectedValue.ToString() + "','" + ddlDistrict.SelectedValue.ToString() + "',getdate(),'" + ClientIP + "','" + ClientIP + "',getdate(),'" + txtbranch.Text + "','" + ddlissuecenter.SelectedItem.Text + "','" + GateP_No + "','" + ddlbranchtype.SelectedValue.ToString() + "','bm2015')";
                    cmd = new SqlCommand(qry, con);
                    int c = cmd.ExecuteNonQuery();
                    /*Adding Value for Branch/Issue Center Details in MetaDataBranchWithIssueCenter_Test table(Test Table)*/
                    //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                    //BranchIssueCenter.AddMetaDetaBranchWithIssueCenter(GateP_No
                    //    , ddlissuecenter.SelectedValue.ToString()
                    //    , ddlDistrict.SelectedValue.ToString()
                    //    , DateTime.Now
                    //    , ClientIP
                    //    , ClientIP
                    //    , DateTime.Now
                    //    , txtbranch.Text.ToString()
                    //    , ddlissuecenter.SelectedItem.Text.ToString()
                    //    , GateP_No
                    //    , ddlbranchtype.SelectedValue.ToString()
                    //    , "bm2015");
                    if (c > 0)
                    {
                        qry = "insert into tbl_MetaData_DEPOT (DepotID,[StateId],[DistrictId],DepoTypeID,[DepotName],[CreatedBy],[CreatedDate],[DepoBelongs],[BranchId]) values ('" + GateP_No + "','23','" + ddlDistrict.SelectedValue.ToString() + "','" + ddlbranchtype.SelectedValue.ToString() + "','" + txtbranch.Text.Trim() + "','" + ClientIP + "',getdate(),'" + ddlbranchtype.SelectedItem.Text + "','" + GateP_No + "')";
                        cmd = new SqlCommand(qry, con);
                        int d = cmd.ExecuteNonQuery();
                        /*Adding Value for Depot Details in tbl_MetaData_DEPOT_Test table(Test Table)*/
                        //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                        //BranchIssueCenter.AddDepotMasterDetails(GateP_No, "23"
                        //, ddlDistrict.SelectedValue.ToString()
                        //, ddlbranchtype.SelectedValue.ToString()
                        //, txtbranch.Text.Trim().ToString()
                        //, ClientIP
                        //, DateTime.Now
                        //, ddlbranchtype.SelectedItem.Text.ToString()
                        //, GateP_No);
                        if (d > 0)
                        {
                            qry = "  insert into [Storage_Login]([User_Name],[Password],[DepotId],[DistrictId],[Fname],[Scope],[MasterPassword],[Access_Restrict],BranchId) values ('" + txtbranch.Text + "','wlc2015','" + ddlissuecenter.SelectedValue.ToString() + "','" + ddlDistrict.SelectedValue.ToString() + "','" + txtbranch.Text + "','1','whr2015','N','" + GateP_No + "' )";
                            cmd = new SqlCommand(qry, con);
                            int x1 = cmd.ExecuteNonQuery();
                            if (x1 > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('The Record is added successfully')", true);
                            }
                        }
                    }
                }

            }
            else
            {
                qry = "UPDATE [MetaDataBranchWithIssueCenter] SET [IssueCenterId] = '" + ddlissuecenter.SelectedValue.ToString() + "',BranchTypeID='" + ddlbranchtype.SelectedValue.ToString() + "',BranchName='" + txtbranch.Text + "' ,[CreatedBy] = '" + ClientIP + "' ,[CretaedBYIP] = '" + ClientIP + "' ,[UpdatedDate] = getdate() ,[IssueCenterName] = '" + ddlissuecenter.SelectedItem.Text + "' WHERE BranchId='" + gvbranch.SelectedRow.Cells[2].Text + "' ";
                cmd = new SqlCommand(qry, con);
                int x1 = cmd.ExecuteNonQuery();
                /*For Updating Value for Branch/Issue Center Details in MetaDataBranchWithIssueCenter_Test table(Test Table)*/
                //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                //BranchIssueCenter.UpdateMetaDetaBranchWithIssueCenter(ddlissuecenter.SelectedValue.ToString()
                //, ClientIP
                //, ClientIP
                //, txtbranch.Text.ToString()
                //, ddlissuecenter.SelectedItem.Text.ToString()
                //, gvbranch.SelectedRow.Cells[2].Text.ToString()
                //, ddlbranchtype.SelectedValue.ToString());
                if (x1 > 0)
                {
                    qry = "update tbl_MetaData_DEPOT set DepoTypeID='" + ddlbranchtype.SelectedValue.ToString() + "',DepotName='" + txtbranch.Text + "',[UpdatedBy]='" + ClientIP + "' ,[UpdatedDate]=getdate() WHERE BranchId='" + gvbranch.SelectedRow.Cells[2].Text + "'";
                    cmd = new SqlCommand(qry, con);
                    int d = cmd.ExecuteNonQuery();
                    /*For Updating Value for Depot Details in tbl_MetaData_DEPOT_Test table(Test Table)*/
                    //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                    //BranchIssueCenter.UpdateMetaDetaDepot(ddlbranchtype.SelectedValue.ToString()
                    //, txtbranch.Text.ToString()
                    //, ClientIP
                    //, gvbranch.SelectedRow.Cells[2].Text.ToString());
                    if (d > 0)
                    {
                        qry = "update [Storage_Login] set User_Name='" + txtbranch.Text + "' where BranchID='" + gvbranch.SelectedRow.Cells[2].Text + "'";
                        cmd = new SqlCommand(qry, con);
                        int x2 = cmd.ExecuteNonQuery();
                        if (x2 > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('The Record is Updated successfully')", true);
                        }
                    }
                }

            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            FillBranchList();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please enter Both Branch Name Or Branch Type entry')", true);
    
        }
    }
    private void FillBranchList()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

                if (Session["Region_ID"].ToString() != null)
                {
                    region = Session["Region_ID"].ToString();

                }
            }

            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "select * from  dbo.MetaDataBranchWithIssueCenter where DistrictId='" + ddlDistrict.SelectedValue + "'";
            }
            
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
               
                gvbranch.DataSource = ds.Tables[0];

                gvbranch.DataBind();


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

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StatePages/AddBranch.aspx");
    }
    protected void gvbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnsubmit.Text = "Update";
        txtbranch.Text = gvbranch.SelectedRow.Cells[0].Text;
        if (gvbranch.SelectedRow.Cells[3].Text != "NA")
        {
            ddlissuecenter.SelectedValue = gvbranch.SelectedRow.Cells[3].Text;
            depotid = gvbranch.SelectedRow.Cells[2].Text;
            lblbranchid.Text = gvbranch.SelectedRow.Cells[2].Text;
            lblbranchid.Visible = false;
        }
    }
    protected void ddlbranchtype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}