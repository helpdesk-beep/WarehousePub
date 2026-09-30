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

public partial class JointVentureScheme_Agreement_JVS2022_23_Kharif : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string ip;
    int Agree_AID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        string SessBranch = Session["UserName"].ToString();
        string SessBranchID = Session["UserId"].ToString();
        if (SessBranchID != "" && SessBranch != "")
        {
            if (!IsPostBack)
            {
                lbluser.Text = SessBranch;
                GetDist();
                divHide();
                getReg();
            }
        }
       
    }
    protected void ddlWarName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = "";
        if (ddl_session.SelectedValue.ToString() == "JVS2022_23_Kharif")
        {
            //qry = "select Distinct GodownID,Godown_No from tbl_Godown_Inspection where Registration_Id='" + ddlWarName.SelectedValue + "' and GodownId not in(select AGR.GodownId from tbl_Godown_Agreement as AGR where CreatedDate > convert(varchar(10),'01/01/2019',101)) and Fit_Unfit='FIT' and CreatedDate > convert(varchar(10),'01/01/2019',101)";
            qry = "select Distinct GodownID,Godown_No from tbl_Godown_Inspection_Kharif_2022 as INSP where Registration_Id='" + ddlWarName.SelectedValue + "' and INSP.Inspection_Id not in(select AGR.Inspection_Id from tbl_Godown_Agreement as AGR where CreatedDate > convert(varchar(10),'02/20/2020',101)) and Fit_Unfit='FIT' and CreatedDate > convert(varchar(10),'02/20/2020',101)";
        }
        //else if (ddl_session.SelectedValue.ToString() == "JVS2022_23_Kharif")
        //{
        //    qry = "select Distinct GodownID,Godown_No from tbl_Godown_Inspection as INSP where Registration_Id='" + ddlWarName.SelectedValue + "' and INSP.Inspection_Id not in(select AGR.Inspection_Id from tbl_Godown_Agreement as AGR where CreatedDate > convert(varchar(10),'02/20/2021',101)) and Fit_Unfit='FIT' and CreatedDate > convert(varchar(10),'02/20/2021',101)";
        //}

        //else if (ddl_session.SelectedValue.ToString() == "JVS2022_23_Kharif")
        //{
        //    qry = "select Distinct GodownID,Godown_No from tbl_Godown_Inspection as INSP where Registration_Id='" + ddlWarName.SelectedValue + "' and INSP.Inspection_Id not in(select AGR.Inspection_Id from tbl_Godown_Agreement as AGR where CreatedDate > convert(varchar(10),'03/10/2022',101)) and Fit_Unfit='FIT' and CreatedDate > convert(varchar(10),'03/10/2022',101)";
        //}
        //GetWareName(qry);
        GetGodown(qry);
    }
    public void getReg()
    {
        string qry = "";
        if (ddl_session.SelectedValue.ToString() == "JVS2022_23_Kharif")
        {
           // qry = "select Distinct g.Registration_Id,g.Fit_Unfit,w.Warehouse_Name from tbl_Godown_Inspection g join tbl_WarehouseRegistration w on g.Registration_Id=w.Registration_Id  where Fit_Unfit='FIT' and g.BranchId='" + Session["UserId"].ToString() + "' and g.Jvs_session='Rabi_2022_23'  and g.CreatedDate > convert(varchar(10),'03/10/2022',101) ";
            qry = "select Distinct g.Registration_Id,g.Fit_Unfit,w.Warehouse_Name from tbl_Godown_Inspection_Kharif_2022 g join tbl_WarehouseRegistration w on g.Registration_Id=w.Registration_Id  where Fit_Unfit='FIT' and g.BranchId='" + Session["UserId"].ToString() + "' and g.CreatedDate > convert(varchar(10),'10/10/2022',101) ";
        }
        GetWareName(qry);
    }
    protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string qry = "";
        //if (ddl_session.SelectedValue.ToString() == "JVS2020_21")
        //{
        //    qry = "select Distinct g.Registration_Id,g.Fit_Unfit,w.Warehouse_Name from tbl_Godown_Inspection g join tbl_WarehouseRegistration w on g.Registration_Id=w.Registration_Id  where Fit_Unfit='FIT' and g.BranchId='" + Session["UserId"].ToString() + "' and g.CreatedDate > convert(varchar(10),'02/20/2020',101) ";
        //}
        //GetWareName(qry);
    }

    public void GetWareName(string qry)
    {

        // qry = "select Distinct g.Registration_Id,g.Fit_Unfit,w.Warehouse_Name from tbl_Godown_Inspection g join tbl_WarehouseRegistration w on g.Registration_Id=w.Registration_Id  where Fit_Unfit='FIT' and g.BranchId='" + Session["UserId"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlWarName.DataSource = ds.Tables[0];
            ddlWarName.DataTextField = "Warehouse_Name";
            ddlWarName.DataValueField = "Registration_Id";
            ddlWarName.DataBind();
            ddlWarName.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlWarName.Items.Clear();
            ddlWarName.DataBind();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Another Godown Found')", true);
        }
    }
    public void GetGodown(string qry) 
    {
        //  string qry = "select GodownID,Godown_No from tbl_Godown_Inspection where Registration_Id='" + ddlWarName.SelectedValue + "' and GodownId not in(select GodownId from tbl_Godown_Agreement) and Fit_Unfit='FIT'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_No";
            ddlgodown.DataValueField = "GodownID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlgodown.Items.Clear();
            ddlgodown.DataBind();
            divHide();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Another Godown Found')", true);
        }
    }

    private void GetDist()
    {
        string strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DDLDistrict.DataSource = ds.Tables[0];
            DDLDistrict.DataTextField = "District_Name";
            DDLDistrict.DataValueField = "District_Id";
            DDLDistrict.DataBind();
            DDLDistrict.Items.Insert(0, "--Select--");
            Session["SDist"] = ds;
        }
        else
        {
            DDLDistrict.Items.Insert(0, "--Select--");
        }
    }

    public void GetGodwnOfferData(string qry)
    {
        // 
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblAuthorised.Text = dt.Rows[0]["Owner_Name"].ToString();
            lblEmail.Text = dt.Rows[0]["Owner_Email"].ToString();
            lblMobile.Text = dt.Rows[0]["Owner_MobileNo"].ToString();
            lblVacant.Text = dt.Rows[0]["vacant_Capacity"].ToString();
            lblDistrict.Text = dt.Rows[0]["District"].ToString();
            lblTehsil.Text = dt.Rows[0]["Tehsil"].ToString();
            txtInspectedCpt.Text = dt.Rows[0]["vacant_Capacity"].ToString();
            Session["Inspection_Id"] = dt.Rows[0]["Inspection_Id"].ToString();
            Session["Godown_Offer_Id"] = dt.Rows[0]["Godown_Offer_Id"].ToString();
            Session["Offer_Id"] = dt.Rows[0]["Offer_Id"].ToString();
            Session["Registration_Id"] = dt.Rows[0]["Registration_Id"].ToString();
            Session["GodownId"] = dt.Rows[0]["GodownId"].ToString();
            Session["Godown_No"] = dt.Rows[0]["Godown_No"].ToString();
            Session["RegionId"] = dt.Rows[0]["RegionId"].ToString();
            Session["DistrictId"] = dt.Rows[0]["DistrictId"].ToString();
            Session["BranchId"] = dt.Rows[0]["BranchId"].ToString();
            // Session["Scheme"] = dt.Rows[0]["Insp_Offer_Scheme"].ToString();
            lblinspscheme.Text = dt.Rows[0]["Insp_Offer_Scheme"].ToString();
            lblofferscheme.Text = dt.Rows[0]["Offer_Scheme"].ToString();
            DDLDistrict.SelectedValue = dt.Rows[0]["DistrictId"].ToString();
            DDLDistrict_SelectedIndexChanged(null, null);
            ddlBranch.SelectedValue = dt.Rows[0]["BranchId"].ToString();
            ddlBranch_SelectedIndexChanged(null, null);
            if (dt.Rows[0]["WMS_GodownID"].ToString() != "NG")
            {
                ddlGodownWHMS.SelectedValue = dt.Rows[0]["WMS_GodownID"].ToString();
            }
            txtSecondPart.Text = ddlWarName.SelectedItem.Text.Trim();
            string WDRAlictype = dt.Rows[0]["Present_Validity_WDRA"].ToString().Trim();
            string Staetlictype = dt.Rows[0]["WDRAL_Present_Validity"].ToString().Trim();

            if (Staetlictype != "")
            {
                Session["Scheme"] = "NON WDRA";
            }
            else if (WDRAlictype != "")
            {
                Session["Scheme"] = "WDRA";
            }

            string WDRAlicno = dt.Rows[0]["WDRA_LicenseNo"].ToString().Trim();
            string StateLicNo = dt.Rows[0]["Warehouse_LicenseNo"].ToString().Trim();

            if (WDRAlicno != "")
            {
                lbllicno.Text = dt.Rows[0]["WDRA_LicenseNo"].ToString().Trim();
                lbllicexpdate.Text = dt.Rows[0]["WDRA_LicExpDate"].ToString();
                lbllictype.Text = Session["Scheme"].ToString();
            }
            else if (StateLicNo != "")
            {
                lbllicno.Text = dt.Rows[0]["Warehouse_LicenseNo"].ToString().Trim();
                lbllicexpdate.Text = dt.Rows[0]["StateLicExpDate"].ToString();
                lbllictype.Text = Session["Scheme"].ToString();
            }
        }
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = "";
        if (ddl_session.SelectedValue.ToString() == "JVS2022_23_Kharif")
        {
            //qry = "select Owner_Name,Owner_MobileNo,Owner_Email,vacant_Capacity,Inspection_Id,Godown_Offer_Id,Offer_Id,Registration_Id,GodownId,Godown_No,RegionId,DistrictId,BranchId,G_Scheme as Scheme,District_Name as District,Tehsil_Name as Tehsil,g.WMS_GodownID from tbl_Godown_Inspection g left join ( select Tehsil_Name,TehsilCode from Tehsils) t on t.TehsilCode=g.Ware_TehsilId left join (Select District_Name,District_Id from tbl_MetaData_DISTRICT) d on d.District_Id=g.DistrictId left join (Select distinct Godown_ID,G_Scheme from tbl_Warehouse_Godown_Offer_2019 where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "') GF on GF.Godown_ID=g.GodownId where g.GodownId='" + ddlgodown.SelectedValue.ToString() + "' and g.CreatedDate > convert(varchar(10),'01/01/2019',101) and  g.Fit_Unfit='FIT'";
            qry = "select Owner_Name,Owner_MobileNo,Owner_Email,vacant_Capacity,Inspection_Id,Godown_Offer_Id,Offer_Id,Registration_Id,GodownId,Godown_No,RegionId,DistrictId,BranchId,Offer_Scheme,CASE WHEN g.Maintain_By='0' then 'PMS' WHEN g.Maintain_By='1' then 'Self' end +'-'+ CAST(g.Insp_Offer_Scheme as varchar) AS Insp_Offer_Scheme,WDRA_LicenseNo,convert(varchar(10),WDRA_LicenseDate,103) as WDRA_LicExpDate,Warehouse_LicenseNo,convert(varchar(10),Warehouse_LicenseDate,103) as StateLicExpDate,Present_Validity_WDRA,WDRAL_Present_Validity,District_Name as District,Tehsil_Name as Tehsil,g.WMS_GodownID from tbl_Godown_Inspection_Kharif_2022 g left join ( select Tehsil_Name,TehsilCode from Tehsils) t on t.TehsilCode=g.Ware_TehsilId left join (Select District_Name,District_Id from tbl_MetaData_DISTRICT) d on d.District_Id=g.DistrictId where g.GodownId='" + ddlgodown.SelectedValue.ToString() + "' and g.CreatedDate > convert(varchar(10),'03/10/2022',101) and  g.Fit_Unfit='FIT' and g.Godown_Offer_Id not in (select AGR.Godown_Offer_Id from tbl_Godown_agreement as AGR  where AGR.Jvs_session='JVS2022_23_Kharif' and AGR.CreatedDate > convert(varchar(10),'12/01/2022',101) and AGR.GodownId='" + ddlgodown.SelectedValue.ToString() + "')";
        }
        GetGodwnOfferData(qry);
        divHide();
    }

    public void divHide()
    {
        if (ddlWarName.SelectedValue == "--Select--" && ddlgodown.SelectedValue == "")
        {
            divOwner.Visible = false;
        }
        else
        {
            divOwner.Visible = true;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string QueryMax = "select convert(varchar(10),getdate(),101) as CDateTime";
        SqlCommand cmddate = new SqlCommand(QueryMax, con);
        con.Open();
        var str3 = cmddate.ExecuteScalar().ToString();
        con.Close();
        if (DDLDistrict.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select District...'); </script> ");
        }
        else if (ddlBranch.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Branch...'); </script> ");
        }
        else if (ddlGodownWHMS.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Map WHMS Godown ...'); </script> ");
        }
        else if (txtAgreementSign.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Agreement Date...'); </script> ");
        }
        else if (txtAgreementDur.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Agreement Duration Date...'); </script> ");

        }
        else if (txtAgreeEnd.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Agreement End Date...'); </script> ");

        }
        else if (txtStamp.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter value of Stamp Paper...'); </script> ");

        }
        else if (txtDateOfPurchase.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Purchase Date of Stamp Paper...'); </script> ");

        }
        else if (txtStampCode.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Code Of Stamp Paper...'); </script> ");

        }
        else if (txtFirstPart.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Name of First Party...'); </script> ");

        }
        else if (txtSecondPart.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Name of Second Party...'); </script> ");

        }
        else if (chkDeclaration.Checked == false)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check Declaration...'); </script> ");
        }
        else if (Convert.ToDecimal(lblVacant.Text) < Convert.ToDecimal(txtInspectedCpt.Text))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Cannot Agreement Greater then Inpected Vacant Capacity...'); </script> ");
        }
        else if (lbllicno.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please check Licence No...'); </script> ");
        }
        else
        {
            string schm = "";
            int HTPvtLogin = 0;
            if (Session["Scheme"].ToString() == "NON WDRA")
            {
                schm = "NON WDRA";
                HTPvtLogin = 10;
            }
            else if (Session["Scheme"].ToString() == "WDRA")
            {
                schm = "WDRA";
                HTPvtLogin = 6;
            }
            string AgreeID = ChkInspectionID();
            //string qry = "INSERT INTO [dbo].[tbl_Godown_Agreement]([Agreement_Id],[Inspection_Id],[Godown_Offer_Id],[Offer_Id],[Registration_Id],[GodownId],[Godown_No],[RegionId],[DistrictId],[BranchId],[Agree_Sign_Date],[Agree_Duration],[Stamp_Value],[Stamp_Purchase_Date],[Stamp_Code],[FirstParty_Name],[SecondParty_Name],[Insp_Capacity],[Agree_Capacity],[G_Scheme_Rate],[Godown_Type],[Crop_Year],[Commodity_Id],[CreatedBy],[CreatedDate],[Agree_End_Date],[Auto_ID],[WHMS_Godown_ID])VALUES('" + AgreeID + "','" + Session["Inspection_Id"].ToString() + "','" + Session["Godown_Offer_Id"].ToString() + "','" + Session["Offer_Id"].ToString() + "','" + Session["Registration_Id"].ToString() + "','" + Session["GodownId"].ToString() + "','" + Session["Godown_No"].ToString() + "','" + Session["RegionId"] + "','" + Session["DistrictId"].ToString() + "','" + Session["BranchId"].ToString() + "','" + getDate_MDY(txtAgreementSign.Text) + "','" + txtAgreementDur.Text + "','" + txtStamp.Text + "','" + getDate_MDY(txtDateOfPurchase.Text) + "','" + txtStampCode.Text + "','" + txtFirstPart.Text + "','" + txtSecondPart.Text + "','" + lblVacant.Text + "','" + txtInspectedCpt.Text + "','" + lblinspscheme.Text.Trim() + "','" + schm + "','2020-21','22','" + ip + "',GETDATE(),'" + getDate_MDY(txtAgreeEnd.Text) + "','" + Agree_AID + "','" + ddlGodownWHMS.SelectedValue.ToString() + "')";
            string qry = "INSERT INTO [dbo].[tbl_Godown_Agreement]([Agreement_Id],[Inspection_Id],[Godown_Offer_Id],[Offer_Id],[Registration_Id],[GodownId],[Godown_No],[RegionId],[DistrictId],[BranchId],[Agree_Sign_Date],[Agree_Duration],[Stamp_Value],[Stamp_Purchase_Date],[Stamp_Code],[FirstParty_Name],[SecondParty_Name],[Insp_Capacity],[Agree_Capacity],[G_Scheme_Rate],[Godown_Type],[Crop_Year],[Commodity_Id],[CreatedBy],[CreatedDate],[Agree_End_Date],[Auto_ID],[WHMS_Godown_ID],[Jvs_session])VALUES('" + AgreeID + "','" + Session["Inspection_Id"].ToString() + "','" + Session["Godown_Offer_Id"].ToString() + "','" + Session["Offer_Id"].ToString() + "','" + Session["Registration_Id"].ToString() + "','" + Session["GodownId"].ToString() + "','" + Session["Godown_No"].ToString() + "','" + Session["RegionId"] + "','" + Session["DistrictId"].ToString() + "','" + Session["BranchId"].ToString() + "','" + getDate_MDY(txtAgreementSign.Text) + "','" + txtAgreementDur.Text + "','" + txtStamp.Text + "','" + getDate_MDY(txtDateOfPurchase.Text) + "','" + txtStampCode.Text + "','" + txtFirstPart.Text + "','" + txtSecondPart.Text + "','" + lblVacant.Text + "','" + txtInspectedCpt.Text + "','" + lblinspscheme.Text.Trim() + "','" + schm + "','2022-23','22','" + ip + "',GETDATE(),'" + getDate_MDY(txtAgreeEnd.Text) + "','" + Agree_AID + "','" + ddlGodownWHMS.SelectedValue.ToString() + "','JVS2022_23_Kharif')";

            con.Open();
            SqlCommand cmd = new SqlCommand(qry, con);
            int a = cmd.ExecuteNonQuery();
            con.Close();
            if (a == 1)
            {
                //string qry1 = "INSERT INTO [tbl_Agreemented_Godown_Mapping_2020] ([Godown_ID],[Registration_Id],[Agreement_Id],[GodownId_JVS],[GodownNo_JVS],[Agree_Capacity],[Godown_Type],[DistrictId],[BranchId],[Is_Active],[CreatedBy],[CreatedDate],[Inspection_ID]) VALUES ('" + ddlGodownWHMS.SelectedValue.ToString() + "','" + ddlWarName.SelectedValue.ToString() + "','" + AgreeID + "','" + ddlgodown.SelectedValue.ToString() + "','" + ddlgodown.SelectedItem.Text + "','" + txtInspectedCpt.Text + "','" + schm + "','" + DDLDistrict.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','Y','" + ip + "',GETDATE(),'" + Session["Inspection_Id"].ToString() + "')";
                string qry1 = "INSERT INTO [tbl_Agreemented_Godown_Mapping_2020] ([Godown_ID],[Registration_Id],[Agreement_Id],[GodownId_JVS],[GodownNo_JVS],[Agree_Capacity],[Godown_Type],[DistrictId],[BranchId],[Is_Active],[CreatedBy],[CreatedDate],[Inspection_ID],JVS_Year) VALUES ('" + ddlGodownWHMS.SelectedValue.ToString() + "','" + ddlWarName.SelectedValue.ToString() + "','" + AgreeID + "','" + ddlgodown.SelectedValue.ToString() + "','" + ddlgodown.SelectedItem.Text + "','" + txtInspectedCpt.Text + "','" + schm + "','" + DDLDistrict.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','Y','" + ip + "',GETDATE(),'" + Session["Inspection_Id"].ToString() + "','Kharif_2022_23')";

                sqlcon.Open();
                SqlCommand cmd1 = new SqlCommand(qry1, sqlcon);
                int a1 = cmd1.ExecuteNonQuery();
                if (a1 == 1)
                {
                    string qry2 = "";
                    string GD_Name = ReturnGodownName();
                    string LoginId = GetMax();
                    int chkWHMS = 0;
                    updatelicdataWHMS();
                    chkWHMS = ChkWHMSGodown();
                    if (chkWHMS == 0)
                    {
                        //qry2 = "INSERT INTO [Pvt_Warehouse_Login]([Login_Id],[Godown_Name],[Password],[Godown_Id],[DistrictId],[BranchID],[DepotId],[Scope],[MasterPassword],[Access_Restrict],[GodownTypeId],[Active],[GM_Pwd],Is_Agreement,W_Reg_No,[Is_W18_Agreement],[W_Reg_Id],[Is_W19_Agreement],[Is_W19_RegID]) VALUES ('" + LoginId + "','" + GD_Name + "','wlc2015','" + ddlGodownWHMS.SelectedValue.ToString() + "','" + DDLDistrict.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','1','whrNic16','N','" + HTPvtLogin + "','Y','wlc2015','','','','','Y','" + ddlWarName.SelectedValue.ToString().Trim() + "')";
                        //qry2 = "INSERT INTO [Pvt_Warehouse_Login]([Login_Id],[Godown_Name],[Password],[Godown_Id],[DistrictId],[BranchID],[DepotId],[Scope],[MasterPassword],[Access_Restrict],[GodownTypeId],[Active],[GM_Pwd],Is_Agreement,W_Reg_No,[Is_W18_Agreement],[W_Reg_Id],[Is_W19_Agreement],[Is_W19_RegID],CreatedBy,CreatedDate) VALUES ('" + LoginId + "','" + GD_Name + "','wlc2015','" + ddlGodownWHMS.SelectedValue.ToString() + "','" + DDLDistrict.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','1','whrNic16','N','" + HTPvtLogin + "','Y','wlc2015','','','','','Y','" + ddlWarName.SelectedValue.ToString().Trim() + "','" + ip + "',GETDATE())";
                        //qry2 = "INSERT INTO [Pvt_Warehouse_Login]([Login_Id],[Godown_Name],[Password],[Godown_Id],[DistrictId],[BranchID],[DepotId],[Scope],[MasterPassword],[Access_Restrict],[GodownTypeId],[Active],[GM_Pwd],Is_Agreement,W_Reg_No,[Is_W18_Agreement],[W_Reg_Id],[Is_W19_Agreement],[Is_W19_RegID],CreatedBy,CreatedDate,Is_W20_Agreement,Is_W20_RegID) VALUES ('" + LoginId + "','" + GD_Name + "','wlc2015','" + ddlGodownWHMS.SelectedValue.ToString() + "','" + DDLDistrict.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','1','whrNic16','N','" + HTPvtLogin + "','Y','wlc2015','','','','','Y','" + ddlWarName.SelectedValue.ToString().Trim() + "','" + ip + "',GETDATE()),'Y','" + ddlWarName.SelectedValue.ToString().Trim() + "'";
                        qry2 = "INSERT INTO [Pvt_Warehouse_Login]([Login_Id],[Godown_Name],[Password],[Godown_Id],[DistrictId],[BranchID],[DepotId],[Scope],[MasterPassword],[Access_Restrict],[GodownTypeId],[Active],[GM_Pwd],Is_Agreement,W_Reg_No,[Is_W18_Agreement],[W_Reg_Id],[Is_W19_Agreement],[Is_W19_RegID],CreatedBy,CreatedDate,Is_W20_Agreement,Is_W20_RegID) VALUES ('" + LoginId + "','" + GD_Name + "','wlc2015','" + ddlGodownWHMS.SelectedValue.ToString() + "','" + DDLDistrict.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','" + ddlBranch.SelectedValue.ToString() + "','1','whrNic16','N','" + HTPvtLogin + "','Y','wlc2015','','','','','','" + ddlWarName.SelectedValue.ToString().Trim() + "','" + ip + "',GETDATE(),'Y','" + ddlWarName.SelectedValue.ToString().Trim() + "')";



                        SqlCommand cmd2 = new SqlCommand(qry2, sqlcon);
                        int a2 = cmd2.ExecuteNonQuery();
                        if (a2 == 1)
                        {
                            btnSubmit.Enabled = false;
                            ModalPopupExtender1.Show();
                            //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Submitted Successfully!!!'); </script> ");
                        }
                    }
                    else if (chkWHMS == 1)
                    {
                        string qry3 = "INSERT INTO [Pvt_Warehouse_Login_log] SELECT *  FROM [Intergrated_MP_STORAGE].[dbo].[Pvt_Warehouse_Login] where Godown_Id='" + ddlGodownWHMS.SelectedValue.ToString() + "'";
                        SqlCommand cmd3 = new SqlCommand(qry3, sqlcon);
                        int a3 = cmd3.ExecuteNonQuery();
                        if (a3 == 1)
                        {
                            //qry2 = "Update Pvt_Warehouse_Login set GodownTypeId='" + HTPvtLogin + "',Is_W19_Agreement='Y' , Is_W19_RegID='" + ddlWarName.SelectedValue.ToString().Trim() + "' where Godown_Id='" + ddlGodownWHMS.SelectedValue.ToString() + "'";
                            qry2 = "Update Pvt_Warehouse_Login set GodownTypeId='" + HTPvtLogin + "',Is_W20_Agreement='Y' , Is_W20_RegID='" + ddlWarName.SelectedValue.ToString().Trim() + "',UpdateBy='" + ip + "',UpdatedDate=GETDATE() where Godown_Id='" + ddlGodownWHMS.SelectedValue.ToString() + "'";

                            SqlCommand cmd2 = new SqlCommand(qry2, sqlcon);
                            int a2 = cmd2.ExecuteNonQuery();
                            if (a2 == 1)
                            {
                                btnSubmit.Enabled = false;
                                ModalPopupExtender1.Show();
                                //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Submitted Successfully!!!'); </script> ");
                            }
                        }
                    }
                }
                sqlcon.Close();
            }
        }
    }
    private string GetMax()
    {
        string QueryMax = "select MAX(Login_Id)+1 from Pvt_Warehouse_Login";
        SqlCommand cmd = new SqlCommand(QueryMax, sqlcon);
        string str3 = cmd.ExecuteScalar().ToString();
        return str3;
    }
    public int ChkWHMSGodown()
    {
        int ch = 0;
        string strsql = "select Godown_Id from Pvt_Warehouse_Login where Godown_Id='" + ddlGodownWHMS.SelectedValue.ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strsql, sqlcon);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ch = 1;
        }
        else
        {
            ch = 0;
        }
        return ch;
    }

    public string ReturnGodownName()
    {
        string GodownNameWHMS = "";
        string strsql = "select Godown_Name,Godown_ID from tbl_MetaData_Godown_2018 where Godown_ID='" + ddlGodownWHMS.SelectedValue.ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strsql, sqlcon);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GodownNameWHMS = dt.Rows[0]["Godown_Name"].ToString();
        }
        return GodownNameWHMS;
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

    public string ChkInspectionID()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string MaxInsID = "";
        string QueryMax = "select MAX(Auto_ID) as InsPecID from tbl_Godown_Agreement where GodownId='" + ddlgodown.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(QueryMax, con);
        string str3 = cmd.ExecuteScalar().ToString();
        if ((str3 != String.Empty) || str3 != "")
        {
            int lencount = Convert.ToInt32(str3.Length.ToString());
            string part1 = str3.Substring(0, lencount - 1);
            int partlen1 = Convert.ToInt32(part1.Length.ToString());
            string part2 = str3.Substring(partlen1, lencount - partlen1);
            MaxInsID = Session["Inspection_Id"].ToString() + Convert.ToString(Convert.ToInt32(part2) + 1);
            Agree_AID = Convert.ToInt32(part2) + 1;
        }
        else
        {
            MaxInsID = Session["Inspection_Id"].ToString() + "1";
            Agree_AID = 1;
        }
        con.Close();
        return MaxInsID;
    }
    protected void DDLDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }
    private void GetBranch()
    {
        if (sqlcon.State == ConnectionState.Closed)
        {
            sqlcon.Open();
        }
        string strDist = "SELECT BranchId,DepotName FROM tbl_MetaData_DEPOT where DistrictId='" + DDLDistrict.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strDist, sqlcon);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlBranch.Items.Insert(0, "--Select--");
        }
        sqlcon.Close();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetWMSGodownID();
    }
    public void GetWMSGodownID()
    {
        if (sqlcon.State == ConnectionState.Closed)
        {
            sqlcon.Open();
        }
        string qry = "select Godown_Name +' ('+ Godown_ID +')' as GodownName,Godown_ID,Godown_Name as GodownN from tbl_MetaData_Godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "' order by GodownName";
        SqlCommand cmd = new SqlCommand(qry, sqlcon);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGodownWHMS.DataSource = ds.Tables[0];
            ddlGodownWHMS.DataTextField = "GodownName";
            ddlGodownWHMS.DataValueField = "Godown_ID";
            ddlGodownWHMS.DataBind();
            ddlGodownWHMS.Items.Insert(0, "--Select--");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
        }
        sqlcon.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Agreement.aspx");
        Response.Redirect("Agreement_JVS2021_22.aspx");
    }

    public int ChkAgreement()
    {
        int chk = 0;
        //  string strsql = " select GodownId from tbl_Godown_Agreement where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and GodownId='"+ ddlgodown.SelectedValue.ToString() +"'";
        string strsql = "";
        if (ddl_session.SelectedValue.ToString() == "JVS2022_23")
        {
            strsql = "select GodownId from tbl_Godown_Agreement where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and Jvs_session='JVS2022_23' and GodownId='" + ddlgodown.SelectedValue.ToString() + "' and CreatedDate > convert(varchar(10),'02/20/2021',101) ";
        }
        SqlCommand cmd = new SqlCommand(strsql, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            chk = 1;
        }
        else
        {
            chk = 0;
        }
        return chk;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Logins.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Agreement.aspx");
        Response.Redirect("Agreement_JVS2022_23.aspx");
    }
    public void updatelicdataWHMS()
    {
        ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string qry2 = "";

        qry2 = "insert into tbl_metadata_godown_2018_log select * from tbl_metadata_godown_2018 where godown_id='" + ddlWarName.SelectedValue.ToString() + "'";
        SqlCommand cmd2 = new SqlCommand(qry2, sqlcon);
        int a2 = cmd2.ExecuteNonQuery();

        qry2 = "update tbl_metadata_godown_2018 set licNum='" + lbllicno.Text.Trim() + "',licdate='" + getDate_MDY(lbllicexpdate.Text.Trim()) + "',updatedBy='" + ip + "',updatedDate=GETDATE()  where godown_id='" + ddlWarName.SelectedValue.ToString() + "'";
        SqlCommand cmd3 = new SqlCommand(qry2, sqlcon);
        int a3 = cmd3.ExecuteNonQuery();
    }
}