using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Reports_States_Rpt_Search_HO_StorageBillVerification_N : System.Web.UI.Page
{
    public SqlConnection Con_WH = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string IC_Id = "", Dist_Id = "";
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            District();
            //godown();
            //year();
            //godown();

        }
    }

    public void Getallbill_Deails()
    {
        //if (rbtnbillNo.Checked == true)
        //{
        //    DataSet ds = rEturnDs("select a.Crop_Year,a.BillNo,a.WearHouse_TotAmt,a.CSMS_TotAmt,a.Godown_Type,b.district_name,c.Godown_Name,d.MonthName,'Done' as Status  from StorageBillVerifi2019_Remarks  a inner join pds.districtsmp b on a.District_Id='23'+ b.district_code  left join dbo.tbl_MetaData_GODOWN_2018 c on a.GodownID=c.Godown_ID  left join FIN_MonthMaster d on d.MonthID=(select case when len(a.Month) = 1 then '0'+ a.Month else a.Month end  ) where BillNo='" + txtBillNumber.Text + "'", CommandType.Text, Con_CSMS);

        //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        grd_details.DataSource = ds.Tables[0];
        //        grd_details.DataBind();

        //    }

        //    else
        //    {
        //        grd_details.DataSource = null;
        //        grd_details.DataBind();

        //    }
        //}
        //else if (rbtngodown.Checked == true)
        //{
        //    DataSet ds = rEturnDs("select a.Crop_Year,a.BillNo,cast(CAST(a.WearHouse_TotAmt as decimal(18,5)) as float) as WearHouse_TotAmt,cast(CAST(a.CSMS_TotAmt as decimal(18,5)) as float) as CSMS_TotAmt,a.Godown_Type,b.district_name,c.Godown_Name,year(ins.From_Date) as year,d.MonthName,'Done' as Status from StorageBillVerifi2019_Remarks a  inner join pds.districtsmp b on a.District_Id = '23' + b.district_code left join dbo.tbl_MetaData_GODOWN_2018 c on a.GodownID = c.Godown_ID  left join FIN_MonthMaster d on d.MonthID = (select case when len(a.Month) = 1 then '0' + a.Month else a.Month end  ) inner join tbl_Institution_Storage_Bill_Details ins on ins.Bill_Number = a.BillNo where c.Godown_ID = '" + ddlgodown.SelectedValue.ToString() + "' and year(ins.From_Date)= '" + ddlyear.SelectedValue.ToString() + "' ", CommandType.Text, Con_CSMS);

        //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        grd_details.DataSource = ds.Tables[0];
        //        grd_details.DataBind();

        //    }

        //    else
        //    {
        //        grd_details.DataSource = null;
        //        grd_details.DataBind();

        //    }
        //}

        DataSet ds = rEturnDs("select a.Crop_Year,a.BillNo,cast(CAST(a.WearHouse_TotAmt as decimal(18,5)) as float) as WearHouse_TotAmt,cast(CAST(a.CSMS_TotAmt as decimal(18,5)) as float) as CSMS_TotAmt,a.Godown_Type,b.district_name,c.Godown_Name,year(ins.From_Date) as year,d.MonthName,'Done' as Status from mpscsc.dbo.StorageBillVerifi2019_Remarks a  inner join mpscsc.pds.districtsmp b on a.District_Id = '23' + b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.GodownID = c.Godown_ID  left join mpscsc.dbo.FIN_MonthMaster d on d.MonthID = (select case when len(a.Month) = 1 then '0' + a.Month else a.Month end  ) inner join Intergrated_MP_STORAGE.dbo.tbl_Institution_Storage_Bill_Details ins on ins.Bill_Number = a.BillNo where c.Godown_ID = '" + ddlgodown.SelectedValue.ToString() + "' and year(ins.From_Date)= '" + ddlyear.SelectedValue.ToString() + "' ", CommandType.Text, Con_WH);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grd_details.DataSource = ds.Tables[0];
            grd_details.DataBind();

        }

        else
        {
            grd_details.DataSource = null;
            grd_details.DataBind();

        }

    }

    public void Getallbill_Digtal_Deails()
    {

        //if (rbtnbillNo.Checked == true)
        //{
        //    DataSet ds = rEturnDs("select  Bill_Number,Crop_Year AS Financial_Year,Net_Amount,Sub_Amount,b.district_name,d.MonthName,c.Godown_Name from Digitally_Sign_StorageBill_IC a inner join pds.districtsmp b on a.District_Id='23'+ b.district_code left join dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join FIN_MonthMaster d on a.Month_No=d.MonthID where a.Bill_Number='" + txtBillNumber.Text + "'", CommandType.Text, Con_CSMS);

        //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        grd_digital_sign.DataSource = ds.Tables[0];
        //        grd_digital_sign.DataBind();

        //    }

        //    else
        //    {
        //        grd_digital_sign.DataSource = null;
        //        grd_digital_sign.DataBind();

        //    }
        //}
        //else if (rbtngodown.Checked == true)
        //{
        //    DataSet ds = rEturnDs("select a.Bill_Number,a.Crop_Year AS Financial_Year,a.Net_Amount,a.Sub_Amount,b.district_name, d.MonthName,year(i.From_Date) as year,c.Godown_Name,i.From_Date from Digitally_Sign_StorageBill_IC a inner join pds.districtsmp b on a.District_Id='23'+ b.district_code left join dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join FIN_MonthMaster d on a.Month_No=d.MonthID inner join tbl_Institution_Storage_Bill_Details i on i.Bill_Number =a.Bill_Number where c.Godown_ID=  '" + ddlgodown.SelectedValue.ToString() + "' and year(i.From_Date) = '" + ddlyear.SelectedValue.ToString() + "' ", CommandType.Text, Con_CSMS);
        //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        grd_digital_sign.DataSource = ds.Tables[0];
        //        grd_digital_sign.DataBind();

        //    }

        //    else
        //    {
        //        grd_digital_sign.DataSource = null;
        //        grd_digital_sign.DataBind();

        //    }
        //}


        DataSet ds = rEturnDs("select a.Bill_Number,a.Crop_Year AS Financial_Year,a.Net_Amount,a.Sub_Amount,b.district_name, d.MonthName,year(i.From_Date) as year,c.Godown_Name,i.From_Date from mpscsc.dbo.Digitally_Sign_StorageBill_IC a inner join mpscsc.pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join mpscsc.dbo.FIN_MonthMaster d on a.Month_No=d.MonthID inner join Intergrated_MP_STORAGE.dbo.tbl_Institution_Storage_Bill_Details i on i.Bill_Number =a.Bill_Number where c.Godown_ID=  '" + ddlgodown.SelectedValue.ToString() + "' and year(i.From_Date) = '" + ddlyear.SelectedValue.ToString() + "' ", CommandType.Text, Con_WH);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grd_digital_sign.DataSource = ds.Tables[0];
            grd_digital_sign.DataBind();

        }

        else
        {
            grd_digital_sign.DataSource = null;
            grd_digital_sign.DataBind();

        }

    }

    public void Getallbill_Digtal_DM_DSC_Deails()
    {
        //if (rbtnbillNo.Checked == true)
        //{
        //    DataSet ds = rEturnDs("select a.Bill_Number,a.Crop_Year AS  Financial_Year,a.Net_Amount,a.Sub_Amount,a.Ref_Bill_No,a.Party_Name,b.district_name,d.MonthName,c.Godown_Name from dbo.tbl_Digital_Sign_StorageBill_Final_ForNeft a inner join pds.districtsmp b on a.District_Id='23'+ b.district_code  left join dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join FIN_MonthMaster d on a.Month=d.MonthID where a.Ref_Bill_No='" + txtBillNumber.Text + "'", CommandType.Text, Con_CSMS);

        //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        grd_DMDSC_Verification.DataSource = ds.Tables[0];
        //        grd_DMDSC_Verification.DataBind();

        //    }

        //    else
        //    {
        //        grd_DMDSC_Verification.DataSource = null;
        //        grd_DMDSC_Verification.DataBind();

        //    }
        //}
        //else if (rbtngodown.Checked == true)
        //{
        //    DataSet ds = rEturnDs("select a.Bill_Number,a.Crop_Year AS  Financial_Year,a.Net_Amount,a.Sub_Amount,a.Ref_Bill_No,a.Party_Name,b.district_name,year(ins.From_Date) as year,d.MonthName,c.Godown_Name from dbo.tbl_Digital_Sign_StorageBill_Final_ForNeft a inner join pds.districtsmp b on a.District_Id='23'+ b.district_code  left join dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join FIN_MonthMaster d on a.Month=d.MonthID inner join tbl_Institution_Storage_Bill_Details ins on ins.Bill_Number = a.Bill_Number where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and year(ins.From_Date)= '" + ddlyear.SelectedValue.ToString() + "'", CommandType.Text, Con_CSMS);

        //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        grd_DMDSC_Verification.DataSource = ds.Tables[0];
        //        grd_DMDSC_Verification.DataBind();

        //    }

        //    else
        //    {
        //        grd_DMDSC_Verification.DataSource = null;
        //        grd_DMDSC_Verification.DataBind();

        //    }
        //}

        DataSet ds = rEturnDs("select a.Bill_Number,a.Crop_Year AS  Financial_Year,a.Net_Amount,a.Sub_Amount,a.Ref_Bill_No,a.Party_Name,b.district_name,year(ins.From_Date) as year,d.MonthName ,c.Godown_Name from mpscsc.dbo.tbl_Digital_Sign_StorageBill_Final_ForNeft a inner join mpscsc.pds.districtsmp b on a.District_Id='23'+ b.district_code  left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join mpscsc.dbo.FIN_MonthMaster d on a.Month=d.MonthID inner join Intergrated_MP_STORAGE.dbo.tbl_Institution_Storage_Bill_Details ins on ins.Bill_Number = a.Bill_Number  where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and year(ins.From_Date)= '" + ddlyear.SelectedValue.ToString() + "' order by MonthName desc", CommandType.Text, Con_WH);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grd_DMDSC_Verification.DataSource = ds.Tables[0];
            grd_DMDSC_Verification.DataBind();

        }

        else
        {
            grd_DMDSC_Verification.DataSource = null;
            grd_DMDSC_Verification.DataBind();

        }

    }

    public void Getallbill_Digtal_ByDist_Deails()
    {
        //if (rbtnbillNo.Checked == true)
        //{

        //    DataSet ds = rEturnDs("select  b.district_name,a.Net_Amount, a.Crop_Year AS  Financial_Year, a.Bill_Number,a.Sub_Amount,a.Ref_Bill_No,a.Party_Name,c.Godown_Name  from [dbo].[tbl_Digital_Sign_StorageBill_Final] a  inner join pds.districtsmp b on a.District_Id='23'+ b.district_code left join dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID  where a.Ref_Bill_No='" + txtBillNumber.Text + "' ", CommandType.Text, Con_CSMS);

        //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        grdDMlevel.DataSource = ds.Tables[0];
        //        grdDMlevel.DataBind();

        //    }

        //    else
        //    {
        //        grdDMlevel.DataSource = null;
        //        grdDMlevel.DataBind();

        //    }
        //}
        //else if (rbtngodown.Checked == true)
        //{

        //    DataSet ds = rEturnDs("select  b.district_name, a.Crop_Year AS  Financial_Year, a.Bill_Number,a.Net_Amount,a.Sub_Amount,a.Ref_Bill_No,a.Party_Name,year(ins.From_Date) as year,c.Godown_Name  from [dbo].[tbl_Processing_StorageBill_AtDM] a  inner join pds.districtsmp b on a.District_Id='23'+ b.district_code left join dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID inner join dbo.tbl_Institution_Storage_Bill_Details ins on ins.Bill_Number = a.Bill_Number	  where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and year(ins.From_Date)= '" + ddlyear.SelectedValue.ToString() + "'", CommandType.Text, Con_CSMS);

        //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        grdDMlevel.DataSource = ds.Tables[0];
        //        grdDMlevel.DataBind();

        //    }

        //    else
        //    {
        //        grdDMlevel.DataSource = null;
        //        grdDMlevel.DataBind();

        //    }
        //}
        DataSet ds = rEturnDs("select  b.district_name, a.Crop_Year AS  Financial_Year, a.Bill_Number,a.Net_Amount,a.Sub_Amount,a.Ref_Bill_No,a.Party_Name,year(ins.From_Date) as year,c.Godown_Name  from mpscsc.dbo.[tbl_Processing_StorageBill_AtDM] a  inner join mpscsc.pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID inner join Intergrated_MP_STORAGE.dbo.tbl_Institution_Storage_Bill_Details ins on ins.Bill_Number = a.Bill_Number	  where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and year(ins.From_Date)= '" + ddlyear.SelectedValue.ToString() + "'", CommandType.Text, Con_WH);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdDMlevel.DataSource = ds.Tables[0];
            grdDMlevel.DataBind();

        }

        else
        {
            grdDMlevel.DataSource = null;
            grdDMlevel.DataBind();

        }

    }


    public void Getallbill_Digtal_NEFT_Deails()
    {
        //if (rbtnbillNo.Checked == true)
        //{

        //    DataSet ds = rEturnDs("select a.Bill_Number,a.Crop_Year AS  Financial_Year,a.Net_Amount,a.Base_Amount AS Sub_Amount,a.Ref_Bill_No,a.Party_Name,b.district_name,d.MonthName, c.Godown_Name from dbo.[tbl_Storage_Payment_MPSCSC_NEFT] a  inner join pds.districtsmp b on a.District_Id='23'+ b.district_code left join dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join FIN_MonthMaster d on a.Month=d.MonthID where c.Ref_Bill_No='" + txtBillNumber.Text + "' ", CommandType.Text, Con_CSMS);
        //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        grdNeft.DataSource = ds.Tables[0];
        //        grdNeft.DataBind();

        //    }

        //    else
        //    {
        //        grdNeft.DataSource = null;
        //        grdNeft.DataBind();

        //    }
        //}
        //else if (rbtngodown.Checked == true)
        //{
        //    DataSet ds = rEturnDs("select a.Bill_Number,a.Crop_Year AS  Financial_Year,a.Net_Amount,a.Base_Amount As Sub_Amount,a.Ref_Bill_No,a.Party_Name,b.district_name,year(ins.From_Date) as year, d.MonthName,c.Godown_Name from dbo.[tbl_Storage_Payment_MPSCSC_NEFT] a  inner join pds.districtsmp b on a.District_Id='23'+ b.district_code  left join dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID  left join FIN_MonthMaster d on a.Month=d.MonthID  inner join tbl_Institution_Storage_Bill_Details ins on ins.Bill_Number = a.Bill_Number  where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and year(ins.From_Date)= '" + ddlyear.SelectedValue.ToString() + "' ", CommandType.Text, Con_CSMS);

        //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        grdNeft.DataSource = ds.Tables[0];
        //        grdNeft.DataBind();

        //    }

        //    else
        //    {
        //        grdNeft.DataSource = null;
        //        grdNeft.DataBind();

        //    }

        //}

        DataSet ds = rEturnDs("select a.Bill_Number,a.Crop_Year AS  Financial_Year,a.Net_Amount,a.Base_Amount As Sub_Amount,a.Ref_Bill_No,a.Party_Name,b.district_name,year(ins.From_Date) as year, d.MonthName,c.Godown_Name from mpscsc.dbo.[tbl_Storage_Payment_MPSCSC_NEFT] a  inner join mpscsc.pds.districtsmp b on a.District_Id='23'+ b.district_code  left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID  left join mpscsc.dbo.FIN_MonthMaster d on a.Month=d.MonthID  inner join Intergrated_MP_STORAGE.dbo.tbl_Institution_Storage_Bill_Details ins on ins.Bill_Number = a.Bill_Number  where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and year(ins.From_Date)= '" + ddlyear.SelectedValue.ToString() + "' order by MonthName desc ", CommandType.Text, Con_WH);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdNeft.DataSource = ds.Tables[0];
            grdNeft.DataBind();

        }

        else
        {
            grdNeft.DataSource = null;
            grdNeft.DataBind();

        }

    }


    public void godown()
    {

        DataSet ds = rEturnDs("select Godown_ID,Godown_Name from Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018  where DistrictId='" + ddldistrict.SelectedValue.ToString() + "'", CommandType.Text, Con_WH);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");
        }

        else
        {

        }
    }

    public void year()
    {

        DataSet ds = rEturnDs("select Years from mpscsc.dbo.CalendarYear ", CommandType.Text, Con_WH);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            ddlyear.DataSource = ds.Tables[0];
            ddlyear.DataTextField = "Years";
            ddlyear.DataValueField = "Years";
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, "--Select--");
        }

        else
        {

        }
    }


    public void District()
    {

        DataSet ds = rEturnDs("select '23'+district_code as district_code ,district_name from mpscsc.pds.districtsmp  order by  district_name asc", CommandType.Text, Con_WH);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "district_name";
            ddldistrict.DataValueField = "district_code";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "--Select--");
        }

        else
        {

        }
    }


    protected DataSet rEturnDs(string sQL, CommandType commandType, SqlConnection conn)
    {
        DataSet dS = new DataSet();
        SqlCommand cmd = new SqlCommand(sQL);
        cmd.CommandType = commandType;
        int i = 0;
        checked
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dS);

            }
            catch (Exception e)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                //throw new Exception(e.Message);
                //  Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + e.Message + "'); </script> ");
                ClientScript.RegisterStartupScript(this.GetType(), "script", e.Message);

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dS;
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        pnldata.Visible = true;
        Getallbill_Deails();
        Getallbill_Digtal_Deails();
        Getallbill_Digtal_DM_DSC_Deails();
        Getallbill_Digtal_ByDist_Deails();
        Getallbill_Digtal_NEFT_Deails();
        //Getallbill_Digtal_JIT_Response();
    }
    protected void rbtnbillNo_CheckedChanged(object sender, EventArgs e)
    {
        pnlbillno.Visible = true;
        pnlgodwnno.Visible = false;
        pnlsave.Visible = true;


    }
    protected void rbtngodown_CheckedChanged(object sender, EventArgs e)
    {
        pnlbillno.Visible = false;
        pnlgodwnno.Visible = true;
        pnlsave.Visible = true;

    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlyear.Visible = true;
        god.Visible = true;
        god1.Visible = true;
        pnlyear.Visible = true;
        //yeartd1.Visible = true;
        godown();
        year();
    }
}