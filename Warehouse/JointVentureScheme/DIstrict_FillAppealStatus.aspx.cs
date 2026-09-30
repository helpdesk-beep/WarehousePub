using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
public partial class JointVentureScheme_DIstrict_FillAppealStatus : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlCommand cmd = null;
    SqlTransaction sqltran;
    string R_Phase = "";
    string Reg_season = "";
    string regno = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            R_Phase = "1";
            Reg_season = "R2019";
            if (!IsPostBack)
            {
                lbluser.Text = Session["UserName"].ToString();
            }
        }
        else
        {
            Response.Redirect("logins.aspx");
        }
    }
    private void GetunfitGodown()
    {
        try
        {
            gvGodown.DataSource = "";
            gvGodown.DataBind();
           // regno = Session["Reg_No"].ToString();
            string qry = "select (select Warehouse_Name from tbl_WarehouseRegistration as REG where REG.Registration_Id=INSP.Registration_Id ) as Warehouse_Name,GOFR.Godown_ID,GOFR.Godown_No,GOFR.G_OfferCapacity, Convert(varchar(10),GOFR.Offer_Date,103) as Offer_Date,GOFR.G_Scheme,INSP.Fit_Unfit,Convert(varchar(10),INSP.Insp_Date,103) as Insp_Date,GOFR.Godown_Offer_Id,INSP.Inspection_Id ,(select Convert(varchar(10),CreatedDate,103) from tbl_Godown_Appeal as IAPL where IAPL.Inspection_ID=INSP.Inspection_Id ) as Appeal_date,GOFR.Registration_Id from tbl_Warehouse_Godown_Offer_2019 as GOFR inner join tbl_Godown_Inspection as INSP on GOFR.Godown_Offer_Id=INSP.Godown_Offer_Id where Fit_Unfit='UNFIT' AND INSP.CreatedDate>CONVERT(VARCHAR(10),'02/17/2019',101) and INSP.Registration_Id in (select distinct APL.Registration_ID from tbl_Godown_Appeal as APL where Appeal_Status='A') and GOFR.DistrictId ='" + Session["UserId"].ToString() + "' order by GOFR.Godown_No";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGodown.DataSource = ds;
                gvGodown.DataBind();
                this.gvGodown.Columns[1].Visible = false;
                this.gvGodown.Columns[8].Visible = false;
                this.gvGodown.Columns[9].Visible = false;
                this.gvGodown.Columns[10].Visible = false;
            }
            else
            {
                gvGodown.DataSource = "";
                gvGodown.DataBind();
                trUnfit.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void GetShemeGodown()
    {
        try
        {
            regno = Session["Reg_No"].ToString();
            string qry = "select GOFR.Godown_ID,GOFR.Godown_No,GOFR.G_OfferCapacity, Convert(varchar(10),GOFR.Offer_Date,103) as Offer_Date,GOFR.G_Scheme,INSP.Insp_Offer_Scheme,INSP.Fit_Unfit,Convert(varchar(10),INSP.Insp_Date,103) as Insp_Date,GOFR.Godown_Offer_Id from tbl_Warehouse_Godown_Offer_2019 as GOFR inner join tbl_Godown_Inspection as INSP on GOFR.Godown_Offer_Id=INSP.Godown_Offer_Id where  Fit_Unfit='FIT' AND INSP.CreatedDate>CONVERT(VARCHAR(10),'02/17/2019',101) and G_Scheme!=Insp_Offer_Scheme and GOFR.Registration_Id='" + regno + "' order by GOFR.Godown_No";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                this.GridView1.Columns[0].Visible = false;
                this.GridView1.Columns[8].Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ckstack_CheckedChanged(object sender, EventArgs e)
    {
        int s;
        int Count_Rows;
        int chkcount = 0;
        string GdwnID;
        string reg_no;
        string inspID;
       // regno = Session["Reg_No"].ToString();
        Count_Rows = gvGodown.Rows.Count;
        for (s = 0; s < gvGodown.Rows.Count; s++)
        {
                if (((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked == true)
                {
                  //  chkcount = chkcount + 1;
                    GdwnID = gvGodown.Rows[s].Cells[1].Text.ToString();
                    reg_no = gvGodown.Rows[s].Cells[10].Text.ToString();
                    inspID = gvGodown.Rows[s].Cells[9].Text.ToString();

                    //  qry = "select INSP.Fit_Unfit,Convert(varchar(10),INSP.Insp_Date,103) as Insp_Date ,case when IsWare_Under_Constr=1 then 'Yes' else 'No' end IsWare_Under_Constr, case when IsWare_Disputed=1 then 'Yes' else 'No' end IsWare_Disputed,case when IsWare_Damage=1 then 'Yes' else 'No' end IsWare_Damage,case when IsWareS_Comm_NGovt=1 then 'Yes' else 'No' end IsWareS_Comm_NGovt,case when IsWare_BlackListed=1 then 'Yes' else 'No' end IsWare_BlackListed,case when IsOnline_Info_Wrong=1 then 'Yes' else 'No' end IsOnline_Info_Wrong, case when IsMandatory_Condi_NAvail=1 then 'Yes' else 'No' end IsMandatory_Condi_NAvail,case when IsNotAgm_AOffer_LastYr=1 then 'Yes' else 'No' end  IsNotAgm_AOffer_LastYr ,Remark,(select Appeal_Remark from tbl_Godown_Appeal as IAPL where IAPL.Inspection_ID=INSP.Inspection_Id ) as Appeal_Remark from tbl_Godown_Inspection as INSP where INSP.Registration_Id='" + regno + "' and GodownId='" + GdwnID + "' and Fit_Unfit='UNFIT' ";
                    qry = "select INSP.Fit_Unfit,Convert(varchar(10),INSP.Insp_Date,103) as Insp_Date , IsWare_Under_Constr, IsWare_Disputed, IsWare_Damage, IsWareS_Comm_NGovt, IsWare_BlackListed,  IsOnline_Info_Wrong,   IsMandatory_Condi_NAvail, IsWH_CplessthenFiveHundred ,Remark  ,(select Appeal_Remark from tbl_Godown_Appeal as IAPL where IAPL.Inspection_ID=INSP.Inspection_Id ) as Appeal_Remark from tbl_Godown_Inspection as INSP  where INSP.Registration_Id='" + reg_no + "' and GodownId='" + GdwnID + "' and INSP.Inspection_Id='" + inspID + "' and Fit_Unfit='UNFIT' ";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        ddlWareConstruct.SelectedValue = dt.Rows[0]["IsWare_Under_Constr"].ToString();
                        ddlWarelitigation.SelectedValue = dt.Rows[0]["IsWare_Disputed"].ToString();
                        ddlWareDamage.SelectedValue = dt.Rows[0]["IsWare_Damage"].ToString();
                        ddlWarePrivateDepositor.SelectedValue = dt.Rows[0]["IsWareS_Comm_NGovt"].ToString();
                        ddlBlackList.SelectedValue = dt.Rows[0]["IsWare_BlackListed"].ToString();
                        ddlofrinfo.SelectedValue = dt.Rows[0]["IsOnline_Info_Wrong"].ToString();
                        ddlFacilities.SelectedValue = dt.Rows[0]["IsMandatory_Condi_NAvail"].ToString();
                        ddlcptless.SelectedValue = dt.Rows[0]["IsWH_CplessthenFiveHundred"].ToString();
                        txtreamrkWH_Owner.Value = dt.Rows[0]["Appeal_Remark"].ToString();
                        chkfitunfit();
                       // break;
                    }
                    for (int j = 0; j < gvGodown.Rows.Count; j++)
                    {
                        if (s != j)
                        {
                            ((CheckBox)gvGodown.Rows[j].FindControl("ckstack")).Checked = false;
                        }
                    }
                }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("logins.aspx");
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        trUnfit.Visible = true;
        trscheme.Visible = false;
        GetunfitGodown();
        
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        trUnfit.Visible = false;
        GetShemeGodown();
        trscheme.Visible = true;
    }

    protected void chkschemegdwn_CheckedChanged(object sender, EventArgs e)
    {
        int s;
        int Count_Rows;
        Count_Rows = GridView1.Rows.Count;
        for (s = 0; s < GridView1.Rows.Count; s++)
        {
            if (((CheckBox)GridView1.Rows[s].FindControl("chkschemegdwn")).Checked == true)
            {
                qry = "select case when [Elec_Weigh_Avail]=1 then 'Yes' else 'No' end Elec_Weigh_Avail, case when G_Gates_Condition=1 then 'Yes' else 'No' end G_Gates_Condition,case when [RoadType]=1 then 'BT' when [RoadType]=2 then 'CC'  when [RoadType]=3 then 'WBM'  else 'Other' end [RoadType], case when W_BoundaryType=1 then 'Boundary Wall' when W_BoundaryType=2 then 'channeling Fencing'  when W_BoundaryType=4 then 'Barbed Wire Fancing'  else 'Other' end W_BoundaryType from tbl_Godown_Inspection as INSP where INSP.CreatedDate>CONVERT(VARCHAR(10),'02/17/2019',101) and INSP.Registration_Id='" + Session["Reg_No"].ToString() + "' and GodownId='" + GridView1.Rows[s].Cells[0].Text.ToString() + "' and Godown_Offer_Id='" + GridView1.Rows[s].Cells[8].Text.ToString() + "'  and Fit_Unfit='FIT'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblgate.Text = dt.Rows[0]["G_Gates_Condition"].ToString();
                    lblWeibrige.Text = dt.Rows[0]["Elec_Weigh_Avail"].ToString();
                    lblboundry.Text = dt.Rows[0]["W_BoundaryType"].ToString();
                    lblroadtype.Text = dt.Rows[0]["W_BoundaryType"].ToString();
                }
            }
        }
    }
    protected void btnUnfit_Click(object sender, EventArgs e)
    {
        if (RadioButton1.Checked == true)
        {
            if (txtRemark.Value == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Discription For Re-Inspection')", true);
            }
            else if (ddlWareConstruct.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम निर्माणाधीन है ? दर्ज करे : :...'); </script> ");
                ddlWareConstruct.Focus();
            }
            else if (ddlWarelitigation.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम विवादग्रस्त है ? दर्ज करे : :...'); </script> ");
                ddlWarelitigation.Focus();
            }
            else if (ddlWareDamage.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम क्षतिग्रस्त है ? दर्ज करे : :...'); </script> ");
                ddlWareDamage.Focus();
            }
            else if (ddlWarePrivateDepositor.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम में शासकीय स्कंध के अतिरिक्त पूर्व से स्कंध भण्डारित है ? दर्ज करे : :...'); </script> ");
                ddlWarePrivateDepositor.Focus();
            }
            else if (ddlBlackList.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम ’’ब्लेक लिस्टेड’’ है ? दर्ज करे : :...'); </script> ");
                ddlBlackList.Focus();
            }
            else if (ddlofrinfo.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या ऑनलाइन ऑफर संबंधी जानकारी गलत दी गयी है ?  दर्ज करे : :...'); </script> ");
                ddlofrinfo.Focus();
            }
            else if (ddlcptless.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम की भण्डारण क्षमता एक परिसर में न्यूनतम 500 मे.टन से कम है ?  दर्ज करे : :...'); </script> ");
                ddlcptless.Focus();
            }

            else if (ddlFacilities.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम वैज्ञानिक भंडारण हेतु अयोग्य हे ( यदि गोदाम योग्य हे तो यह भी सुनिश्चित करे की वायुसंचरण हेतु रोशनदान हो तथा गोदाम की ऊंचाई कम से कम 14 फिट है) दर्ज करे : :...'); </script> ");
                ddlFacilities.Focus();
            }
            else if (txtSystemFitunfit.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम वैज्ञानिक भंडारण हेतु अयोग्य हे ( पात्र / अपात्र दर्ज करे ...'); </script> ");
                txtSystemFitunfit.Focus();
            }
            else
            {
                int s;
               // int Count_Rows;
                string GdwnID;
                string Regis_No;
                string inspID;
                
               
                string ClientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                for (s = 0; s < gvGodown.Rows.Count; s++)
                {
                    if (((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked == true)
                    {
                        GdwnID = gvGodown.Rows[s].Cells[1].Text.ToString();
                        Regis_No = gvGodown.Rows[s].Cells[10].Text.ToString();
                        inspID = gvGodown.Rows[s].Cells[9].Text.ToString();

                        qry = "Insert into tbl_Godown_Inspection_log select * from tbl_Godown_Inspection where Inspection_Id='" + inspID + "' and Registration_Id='" + Regis_No + "' and GodownId='" + GdwnID + "'";
                        con.Open();
                        SqlCommand cmd = new SqlCommand(qry, con);
                        int a = cmd.ExecuteNonQuery();
                        con.Close();
                        if (a == 1)
                        {
                            qry = "Update [JointVentureScheme2018].[dbo].[tbl_Godown_Inspection] set [IsWare_Under_Constr]='" + ddlWareConstruct.SelectedValue + "',[IsWare_Disputed]='" + ddlWarelitigation.SelectedValue + "',[IsWare_Damage]='" + ddlWareDamage.SelectedValue + "',[IsWareS_Comm_NGovt]='" + ddlWarePrivateDepositor.SelectedValue + "',[IsWare_BlackListed]='" + ddlBlackList.SelectedValue + "',[IsOnline_Info_Wrong]='" + ddlofrinfo.SelectedValue + "',[IsMandatory_Condi_NAvail]='" + ddlFacilities.SelectedValue + "',[IsWH_CplessthenFiveHundred]='" + ddlcptless.SelectedValue + "',UpdatedDate=GETDATE(),UpdateBy='" + ClientIP + "' , [RE_Inspe_Discription]=N'" + txtRemark.Value.Trim() + "',[RE_Inspe] ='Y',Fit_Unfit='" + txtSystemFitunfit.Text.Trim() + "',AutoFit_Unfit='" + txtSystemFitunfit.Text.Trim() + "' where Inspection_Id='" + inspID + "' and Registration_Id='" + Regis_No + "' and GodownId='" + GdwnID + "'";
                            con.Open();
                            SqlCommand cmd1 = new SqlCommand(qry, con);
                            int a1 = cmd1.ExecuteNonQuery();
                            con.Close();
                            if (a1 == 1)
                            {
                                qry = "update tbl_Godown_Appeal set Appeal_Status='R' where Inspection_Id='" + inspID + "' and Registration_Id='" + Regis_No + "' and Godown_Id='" + GdwnID + "'";
                                con.Open();
                                SqlCommand cmd11 = new SqlCommand(qry, con);
                                int a11 = cmd11.ExecuteNonQuery();
                                con.Close();
                                if (a11 == 1)
                                {
                                    ModalPopupExtender1.Show();
                                  //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Submit Inspection')", true);
                                  //  RadioButton1_CheckedChanged(null, null);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    protected void ddlWareConstruct_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlWarelitigation_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlWareDamage_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlWarePrivateDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlBlackList_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlcptless_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlFacilities_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlWareSeasonCpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlofrinfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    public void chkfitunfit()
    {
        if (ddlWareConstruct.SelectedItem.Text == "No" && ddlWarelitigation.SelectedItem.Text == "No" && ddlWareDamage.SelectedItem.Text == "No" && ddlWarePrivateDepositor.SelectedItem.Text == "No" && ddlBlackList.SelectedItem.Text == "No" && ddlcptless.SelectedItem.Text == "No" && ddlFacilities.SelectedItem.Text == "No" && ddlofrinfo.SelectedItem.Text == "No")
        {
            txtSystemFitunfit.Text = "FIT";
          //  lblDecFitUnfit.Text = "FIT";
        }
        else
        {
            txtSystemFitunfit.Text = "UNFIT";
          //  lblDecFitUnfit.Text = "UNFIT";
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("DIstrict_FillAppealStatus.aspx");
    }
}
