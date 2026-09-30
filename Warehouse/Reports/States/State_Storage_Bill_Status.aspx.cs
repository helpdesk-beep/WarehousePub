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
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

public partial class Reports_States_State_Storage_Bill_Status : System.Web.UI.Page
{
    public SqlConnection Con_CSMS = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //public SqlConnection Con_WH = new SqlConnection(ConfigurationManager.ConnectionStrings["connstorage"].ToString());


    string IC_Id = "", Dist_Id = "";
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    //SendSms_ACL_WebServer smsObj = new SendSms_ACL_WebServer();

    double WhereHouseTotalBalance;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            District();
            //godown();

        }
    }
    public void Getallbill_Deails()
    {
        if (rbtnbillNo.Checked == true)
        {
            //DataSet ds = rEturnDs("select a.Crop_Year,a.BillNo,a.WearHouse_TotAmt,a.CSMS_TotAmt,a.Godown_Type,b.district_name,c.Godown_Name,d.MonthName,'Done' as Status  from StorageBillVerifi2019_Remarks  a inner join pds.districtsmp b on a.District_Id='23'+ b.district_code  left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.GodownID=c.Godown_ID  left join FIN_MonthMaster d on d.MonthID=(select case when len(a.Month) = 1 then '0'+ a.Month else a.Month end  ) where BillNo='" + txtBillNumber.Text + "'", CommandType.Text, Con_CSMS);
            DataSet ds = rEturnDs("select a.Crop_Year,a.BillNo,a.WearHouse_TotAmt,a.CSMS_TotAmt,a.Godown_Type,b.district_name,c.Godown_Name,d.MonthName,'Done' as Status  from MPSCSC.dbo.StorageBillVerifi2019_Remarks  a inner join MPSCSC.pds.districtsmp b on a.District_Id='23'+ b.district_code  left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.GodownID=c.Godown_ID  left join MPSCSC.dbo.FIN_MonthMaster d on d.MonthID=(select case when len(a.Month) = 1 then '0'+ a.Month else a.Month end  ) where BillNo='" + txtBillNumber.Text + "'", CommandType.Text, Con_CSMS);

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
        else if (rbtngodown.Checked == true)
        {
            //DataSet ds = rEturnDs("select a.Crop_Year,a.BillNo,a.WearHouse_TotAmt,a.CSMS_TotAmt,a.Godown_Type,b.district_name,c.Godown_Name,d.MonthName,'Done' as Status  from StorageBillVerifi2019_Remarks  a inner join pds.districtsmp b on a.District_Id='23'+ b.district_code  left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.GodownID=c.Godown_ID  left join FIN_MonthMaster d on d.MonthID=(select case when len(a.Month) = 1 then '0'+ a.Month else a.Month end  )  where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'", CommandType.Text, Con_CSMS);
            DataSet ds = rEturnDs("select a.Crop_Year,a.BillNo,a.WearHouse_TotAmt,a.CSMS_TotAmt,a.Godown_Type,b.district_name,c.Godown_Name,d.MonthName,'Done' as Status  from MPSCSC.dbo.StorageBillVerifi2019_Remarks  a inner join MPSCSC.pds.districtsmp b on a.District_Id='23'+ b.district_code  left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.GodownID=c.Godown_ID  left join MPSCSC.dbo.FIN_MonthMaster d on d.MonthID=(select case when len(a.Month) = 1 then '0'+ a.Month else a.Month end  )  where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'", CommandType.Text, Con_CSMS);

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


    }

    public void Getallbill_Digtal_Deails()
    {

        if (rbtnbillNo.Checked == true)
        {
            //DataSet ds = rEturnDs("select  Bill_Number,Crop_Year AS Financial_Year,Net_Amount,Sub_Amount,b.district_name,d.MonthName,c.Godown_Name from Digitally_Sign_StorageBill_IC a inner join pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join FIN_MonthMaster d on a.Month_No=d.MonthID where a.Bill_Number='" + txtBillNumber.Text + "'", CommandType.Text, Con_CSMS);
            DataSet ds = rEturnDs("select  Bill_Number,Crop_Year AS Financial_Year,Net_Amount,Sub_Amount,b.district_name,d.MonthName,c.Godown_Name from MPSCSC.dbo.Digitally_Sign_StorageBill_IC a inner join MPSCSC.pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join MPSCSC.dbo.FIN_MonthMaster d on a.Month_No=d.MonthID where a.Bill_Number='" + txtBillNumber.Text + "'", CommandType.Text, Con_CSMS);

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
        else if (rbtngodown.Checked == true)
        {
            //DataSet ds = rEturnDs("select  Bill_Number,Crop_Year AS  Financial_Year,Net_Amount,Sub_Amount,b.district_name,d.MonthName,c.Godown_Name from Digitally_Sign_StorageBill_IC a inner join pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join FIN_MonthMaster d on a.Month_No=d.MonthID where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'", CommandType.Text, Con_CSMS);
            DataSet ds = rEturnDs("select  Bill_Number,Crop_Year AS  Financial_Year,Net_Amount,Sub_Amount,b.district_name,d.MonthName,c.Godown_Name from MPSCSC.dbo.Digitally_Sign_StorageBill_IC a inner join MPSCSC.pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join MPSCSC.dbo.FIN_MonthMaster d on a.Month_No=d.MonthID where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'", CommandType.Text, Con_CSMS);

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



    }

    public void Getallbill_Digtal_Ro_Deails()
    {
        if (rbtnbillNo.Checked == true)
        {
            //DataSet ds = rEturnDs("select a.Bill_Number,a.Crop_Year AS  Financial_Year,a.Net_Amount,a.Sub_Amount,a.Ref_Bill_No,a.Party_Name,b.district_name,d.MonthName,c.Godown_Name from Intergrated_MP_STORAGE.dbo.tbl_Digitally_Signed_Bill_RO a inner join pds.districtsmp b on a.District_Id='23'+ b.district_code  left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join FIN_MonthMaster d on a.Month_No=d.MonthID where a.Ref_Bill_No='" + txtBillNumber.Text + "'", CommandType.Text, Con_CSMS);
            DataSet ds = rEturnDs("select a.Bill_Number,a.Crop_Year AS  Financial_Year,a.Net_Amount,a.Sub_Amount,a.Ref_Bill_No,a.Party_Name,b.district_name,d.MonthName,c.Godown_Name from Intergrated_MP_STORAGE.dbo.tbl_Digitally_Signed_Bill_RO a inner join MPSCSC.pds.districtsmp b on a.District_Id='23'+ b.district_code  left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join MPSCSC.dbo.FIN_MonthMaster d on a.Month_No=d.MonthID where a.Ref_Bill_No='" + txtBillNumber.Text + "'", CommandType.Text, Con_CSMS);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grd_Ro_Verification.DataSource = ds.Tables[0];
                grd_Ro_Verification.DataBind();

            }

            else
            {
                grd_Ro_Verification.DataSource = null;
                grd_Ro_Verification.DataBind();

            }
        }
        else if (rbtngodown.Checked == true)
        {
            //DataSet ds = rEturnDs("select a.Bill_Number,a.Crop_Year AS  Financial_Year,a.Net_Amount,a.Sub_Amount,a.Ref_Bill_No,a.Party_Name,b.district_name,d.MonthName,c.Godown_Name from Intergrated_MP_STORAGE.dbo.tbl_Digitally_Signed_Bill_RO a inner join pds.districtsmp b on a.District_Id='23'+ b.district_code  left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join FIN_MonthMaster d on a.Month_No=d.MonthID where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'", CommandType.Text, Con_CSMS);
            DataSet ds = rEturnDs("select a.Bill_Number,a.Crop_Year AS  Financial_Year,a.Net_Amount,a.Sub_Amount,a.Ref_Bill_No,a.Party_Name,b.district_name,d.MonthName,c.Godown_Name from Intergrated_MP_STORAGE.dbo.tbl_Digitally_Signed_Bill_RO a inner join MPSCSC.pds.districtsmp b on a.District_Id='23'+ b.district_code  left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join MPSCSC.dbo.FIN_MonthMaster d on a.Month_No=d.MonthID where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'", CommandType.Text, Con_CSMS);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grd_Ro_Verification.DataSource = ds.Tables[0];
                grd_Ro_Verification.DataBind();

            }

            else
            {
                grd_Ro_Verification.DataSource = null;
                grd_Ro_Verification.DataBind();

            }
        }


    }

    public void Getallbill_Digtal_ByDist_Deails()
    {
        if (rbtnbillNo.Checked == true)
        {

            //DataSet ds = rEturnDs("select  b.district_name,a.Net_Amount, a.Crop_Year AS  Financial_Year, a.Bill_Number,a.Sub_Amount,a.Ref_Bill_No,a.Party_Name,c.Godown_Name  from [dbo].[tbl_Digital_Sign_StorageBill_Final] a  inner join pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID  where a.Ref_Bill_No='" + txtBillNumber.Text + "'", CommandType.Text, Con_CSMS);
            DataSet ds = rEturnDs("select  b.district_name,a.Net_Amount, a.Crop_Year AS  Financial_Year, a.Bill_Number,a.Sub_Amount,a.Ref_Bill_No,a.Party_Name,c.Godown_Name  from MPSCSC.dbo.[tbl_Digital_Sign_StorageBill_Final] a  inner join MPSCSC.pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID  where a.Ref_Bill_No='" + txtBillNumber.Text + "'", CommandType.Text, Con_CSMS);

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
        else if (rbtngodown.Checked == true)
        {

            //DataSet ds = rEturnDs("select  b.district_name,a.Net_Amount, a.Crop_Year AS  Financial_Year, a.Bill_Number,a.Sub_Amount,a.Ref_Bill_No,a.Party_Name,c.Godown_Name  from [dbo].[tbl_Digital_Sign_StorageBill_Final] a  inner join pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID  where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'", CommandType.Text, Con_CSMS);
            DataSet ds = rEturnDs("select  b.district_name,a.Net_Amount, a.Crop_Year AS  Financial_Year, a.Bill_Number,a.Sub_Amount,a.Ref_Bill_No,a.Party_Name,c.Godown_Name  from MPSCSC.dbo.[tbl_Digital_Sign_StorageBill_Final] a  inner join MPSCSC.pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID  where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'", CommandType.Text, Con_CSMS);

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


    }


    public void Getallbill_Digtal_JIT_Deails()
    {
        if (rbtnbillNo.Checked == true)
        {
            //DataSet ds = rEturnDs("select a.CropYear,a.PartyID,a.PartyName,a.EPOAmt,a.Bill_no,c.Ref_Bill_No,b.district_name from  tbl_JITAllPayPush a  inner join pds.districtsmp b on a.DistID='23'+ b.district_code   inner join [dbo].[tbl_Digital_Sign_StorageBill_Final]  c on a.Bill_no=c.Bill_Number  where c.Ref_Bill_No='" + txtBillNumber.Text + "' and a.Push_To_JIT_Status='Y' and a.PartyType='G' ", CommandType.Text, Con_CSMS);
            DataSet ds = rEturnDs("select a.CropYear,a.PartyID,a.PartyName,a.EPOAmt,a.Bill_no,c.Ref_Bill_No,b.district_name from  MPSCSC.dbo.tbl_JITAllPayPush a  inner join MPSCSC.pds.districtsmp b on a.DistID='23'+ b.district_code   inner join MPSCSC.dbo.[tbl_Digital_Sign_StorageBill_Final]  c on a.Bill_no=c.Bill_Number  where c.Ref_Bill_No='" + txtBillNumber.Text + "' and a.Push_To_JIT_Status='Y' and a.PartyType='G' ", CommandType.Text, Con_CSMS);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grdJIT.DataSource = ds.Tables[0];
                grdJIT.DataBind();

            }

            else
            {
                grdJIT.DataSource = null;
                grdJIT.DataBind();

            }
        }
        else if (rbtngodown.Checked == true)
        {
            //DataSet ds = rEturnDs("select a.CropYear,a.PartyID,a.PartyName,a.EPOAmt,a.Bill_no,c.Ref_Bill_No,b.district_name from  tbl_JITAllPayPush a  inner join pds.districtsmp b on a.DistID='23'+ b.district_code   inner join [dbo].[tbl_Digital_Sign_StorageBill_Final]  c on a.Bill_no=c.Bill_Number  where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and a.Push_To_JIT_Status='Y' and a.PartyType='G' ", CommandType.Text, Con_CSMS);
            DataSet ds = rEturnDs("select a.CropYear,a.PartyID,a.PartyName,a.EPOAmt,a.Bill_no,c.Ref_Bill_No,b.district_name from  MPSCSC.dbo.tbl_JITAllPayPush a  inner join MPSCSC.pds.districtsmp b on a.DistID='23'+ b.district_code   inner join MPSCSC.dbo.[tbl_Digital_Sign_StorageBill_Final]  c on a.Bill_no=c.Bill_Number  where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and a.Push_To_JIT_Status='Y' and a.PartyType='G' ", CommandType.Text, Con_CSMS);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grdJIT.DataSource = ds.Tables[0];
                grdJIT.DataBind();

            }

            else
            {
                grdJIT.DataSource = null;
                grdJIT.DataBind();

            }

        }


    }



    public void Getallbill_Digtal_JIT_Response()
    {

        if (rbtnbillNo.Checked == true)
        {
            //DataSet ds = rEturnDs("select d.district_name,c.Ref_Bill_No,e.Godown_Name,b.AH_Amount,Bill_no,b.UPID,Bank_UTR_No,Credit_Remark,case  when Credit_Remark='S00' then 'Sucess' else  Credit_Remark end  as Remark  from tbl_JITAllPayPush a inner join [PaymetResponseFromWS_Warehouse] b on a.Bill_no=b.UPID inner join tbl_Digital_Sign_StorageBill_Final c on b.UPID=c.Bill_Number inner join pds.districtsmp d on a.DistID='23'+d.district_code inner join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 e on c.Godown_Id=e.Godown_ID where c.Ref_Bill_No='" + txtBillNumber.Text + "'  and  a.PartyType='G'", CommandType.Text, Con_CSMS);
            DataSet ds = rEturnDs("select d.district_name,c.Ref_Bill_No,e.Godown_Name,b.AH_Amount,Bill_no,b.UPID,Bank_UTR_No,Credit_Remark,case  when Credit_Remark='S00' then 'Sucess' else  Credit_Remark end  as Remark  from MPSCSC.dbo.tbl_JITAllPayPush a inner join MPSCSC.dbo.[PaymetResponseFromWS_Warehouse] b on a.Bill_no=b.UPID inner join MPSCSC.dbo.tbl_Digital_Sign_StorageBill_Final c on b.UPID=c.Bill_Number inner join MPSCSC.pds.districtsmp d on a.DistID='23'+d.district_code inner join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 e on c.Godown_Id=e.Godown_ID where c.Ref_Bill_No='" + txtBillNumber.Text + "'  and  a.PartyType='G'", CommandType.Text, Con_CSMS);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grdResponse.DataSource = ds.Tables[0];
                grdResponse.DataBind();

            }

            else
            {
                grdResponse.DataSource = null;
                grdResponse.DataBind();

            }
        }
        else if (rbtngodown.Checked == true)
        {
            //DataSet ds = rEturnDs("select d.district_name,c.Ref_Bill_No,b.AH_Amount,e.Godown_Name,Bill_no,b.UPID,Bank_UTR_No,Credit_Remark,case  when Credit_Remark='S00' then 'Sucess' else  Credit_Remark end  as Remark  from tbl_JITAllPayPush a inner join [PaymetResponseFromWS_Warehouse] b on a.Bill_no=b.UPID inner join tbl_Digital_Sign_StorageBill_Final c on b.UPID=c.Bill_Number inner join pds.districtsmp d on a.DistID='23'+d.district_code inner join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 e on c.Godown_Id=e.Godown_ID where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'  and  a.PartyType='G'", CommandType.Text, Con_CSMS);
            DataSet ds = rEturnDs("select d.district_name,c.Ref_Bill_No,b.AH_Amount,e.Godown_Name,Bill_no,b.UPID,Bank_UTR_No,Credit_Remark,case  when Credit_Remark='S00' then 'Sucess' else  Credit_Remark end  as Remark  from MPSCSC.dbo.tbl_JITAllPayPush a inner join MPSCSC.dbo.[PaymetResponseFromWS_Warehouse] b on a.Bill_no=b.UPID inner join MPSCSC.dbo.tbl_Digital_Sign_StorageBill_Final c on b.UPID=c.Bill_Number inner join MPSCSC.pds.districtsmp d on a.DistID='23'+d.district_code inner join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 e on c.Godown_Id=e.Godown_ID where c.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'  and  a.PartyType='G'", CommandType.Text, Con_CSMS);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grdResponse.DataSource = ds.Tables[0];
                grdResponse.DataBind();

            }

            else
            {
                grdResponse.DataSource = null;
                grdResponse.DataBind();

            }
        }

    }


    public void godown()
    {

        DataSet ds = rEturnDs("select Godown_ID,Godown_Name from Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018  where DistrictId='" + ddldistrict.SelectedValue.ToString() + "'", CommandType.Text, Con_CSMS);

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


    public void District()
    {
        string Qry = "";
        //if(Session["UserName"].ToString() == ""|| Session["UserName"] == null)
        //{
            Qry = "select '23' + district_code as district_code ,district_name from MPSCSC.pds.districtsmp order by district_name asc";
        //}
        //else
        //{
        //    Qry = "select '23'+district_code as district_code ,district_name from MPSCSC.pds.districtsmp where '23'+district_code in (select District_Id from tbl_MetaData_DISTRICT as DT where DT.Region_ID='"+ Session["UserName"].ToString() + "') order by  district_name asc";
        //}
        //DataSet ds = rEturnDs("select '23'+district_code as district_code ,district_name from MPSCSC.pds.districtsmp  order by  district_name asc", CommandType.Text, Con_CSMS);
        DataSet ds = rEturnDs(Qry, CommandType.Text, Con_CSMS);

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
        Getallbill_Digtal_Ro_Deails();
        Getallbill_Digtal_ByDist_Deails();
        Getallbill_Digtal_JIT_Deails();
        Getallbill_Digtal_JIT_Response();
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
        godown();
    }
}