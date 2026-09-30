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

public partial class TribalGodown_AdminPanel : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TribalGodownConString"].ToString());
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataTable EditStack = new DataTable();
    DataTable EditStackNonMPSCSC = new DataTable();
    string Todaydate = "";
    string CheckValid = "";
    string receiptid = string.Empty;
    string gatePassid = string.Empty;
    string ArrivalStockid = string.Empty;
    string URL = "";
    string LoginType = "";
    string UserId = "";
    decimal total1 = 0;
    decimal total2 = 0;
    decimal total3 = 0;
    decimal total4 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if ((Session["reporturl"] != "" && Session["reporturl"] != null)&&(Session["UserId"].ToString() != null || Session["UserId"].ToString() != null) && (Session["UserName"].ToString() != null || Session["UserName"].ToString() != null))
            {
                URL = Session["reporturl"].ToString();
                lbluser.Text = Session["UserName"].ToString();
                LoginType = Session["Scope"].ToString();
                UserId = Session["UserId"].ToString();
                if (!IsPostBack)
                {
                    CheckReportUrl();
                }
            }
            else
            {
                Response.Redirect("StateRegionDistrictLogin.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("StateRegionDistrictLogin.aspx");
        }
    }
    protected void GVApp_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void ddlreporttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    public void GetMeritList()
    {
        btnPrint.Visible = true;
        if (ddlphase.SelectedItem.Text == "Phase 1")
        {
            qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg2016] where subject='A' and DATEPART(MM,CretaedDate)='6' order by CretaedDate";
        }
        else if (ddlphase.SelectedItem.Text == "Phase 2")
        {
            qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg2016] where subject='A' and DATEPART(MM,CretaedDate)='8' order by CretaedDate";
        }
        else if (ddlphase.SelectedItem.Text == "Phase 5")
        {
            qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject='A' and CretaedDate>'08/20/2019' order by CretaedDate";
        }
        else
        {
            qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject='A' order by CretaedDate";
        }
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GvReport.DataSource = dt;
            GvReport.DataBind();
            trMerit.Visible = true;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No Record Found...'); </script> ");
            trMerit.Visible = false;
        }
    }
    public void GetRejectedList()
    {
        btnPrint.Visible = true;
        if (LoginType == "H")
        {
            if (ddlphase.SelectedItem.Text == "Phase 1")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],Remark,DistFromTO,QuantityOfForm FROM [Tbl_TribalReg2016] where subject='R' and DATEPART(MM,CretaedDate)='6' order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 2")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],Remark,DistFromTO,QuantityOfForm FROM [Tbl_TribalReg2016] where subject='R' and DATEPART(MM,CretaedDate)='8' order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 5")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],Remark,DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject='R' and CretaedDate>'08/20/2019' order by CretaedDate";
            }
            else
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],Remark,DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject='R' order by CretaedDate";
            }
        }
        else if (LoginType == "R")
        {
            if (ddlphase.SelectedItem.Text == "Phase 1")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],Remark,DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id=[Tbl_TribalReg2016].CurDist where subject='R' and DATEPART(MM,CretaedDate)='6' and tbl_MetaData_DISTRICT.Region_ID='" + UserId + "' order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 2")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],Remark,DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id=[Tbl_TribalReg2016].CurDist where subject='R' and DATEPART(MM,CretaedDate)='8' and tbl_MetaData_DISTRICT.Region_ID='" + UserId + "' order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 5")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],Remark,DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id=[Tbl_TribalReg].CurDist where subject='R' and CretaedDate>'08/20/2019' and tbl_MetaData_DISTRICT.Region_ID='" + UserId + "' order by CretaedDate";
            }
            else
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],Remark,DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id=[Tbl_TribalReg].CurDist where subject='R' and tbl_MetaData_DISTRICT.Region_ID='" + UserId + "' order by CretaedDate";
            }
        }
        else if (LoginType == "D")
        {
            if (ddlphase.SelectedItem.Text == "Phase 1")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],Remark,DistFromTO,QuantityOfForm FROM [Tbl_TribalReg2016] where subject='R' and DATEPART(MM,CretaedDate)='6' and CurDist='" + UserId + "' order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 2")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],Remark,DistFromTO,QuantityOfForm FROM [Tbl_TribalReg2016] where subject='R' and DATEPART(MM,CretaedDate)='8' and CurDist='" + UserId + "' order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 5")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],Remark,DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject='R' and CretaedDate>'08/20/2019' and CurDist='" + UserId + "' order by CretaedDate";
            }
            else
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],Remark,DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject='R' and CurDist='" + UserId + "' order by CretaedDate";
            }
        }
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            gvRejected.DataSource = dt;
            gvRejected.DataBind();
            trRejected.Visible = true;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No Record Found...'); </script> ");
            trRejected.Visible = false;
        }
    }
    public void GetApprovedList()
    {
        btnPrint.Visible = true;
        if (LoginType == "H")
        {
            if (ddlphase.SelectedItem.Text == "Phase 1")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg2016] where subject='A' and DATEPART(MM,CretaedDate)='6' order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 2")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg2016] where subject='A' and DATEPART(MM,CretaedDate)='8' order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 5")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject='A' and CretaedDate>'08/20/2019' order by CretaedDate";
            }
            else
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject='A  order by CretaedDate";
            }
        }
        else if (LoginType == "R")
        {
            if (ddlphase.SelectedItem.Text == "Phase 1")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id=[Tbl_TribalReg2016].CurDist where subject='A' and DATEPART(MM,CretaedDate)='6' and tbl_MetaData_DISTRICT.Region_ID='" + UserId + "' order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 2")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id=[Tbl_TribalReg2016].CurDist where subject='A' and DATEPART(MM,CretaedDate)='8' and tbl_MetaData_DISTRICT.Region_ID='" + UserId + "' order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 5")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id=[Tbl_TribalReg].CurDist where subject='A' and CretaedDate>'08/20/2019' and tbl_MetaData_DISTRICT.Region_ID='" + UserId + "' order by CretaedDate";
            }
            else
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id=[Tbl_TribalReg].CurDist where subject='A' and tbl_MetaData_DISTRICT.Region_ID='" + UserId + "' order by CretaedDate";
            }
        }
        else if (LoginType == "D")
        {
            if (ddlphase.SelectedItem.Text == "Phase 1")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg2016] where subject='A' and DATEPART(MM,CretaedDate)='6' and CurDist='" + UserId + "' order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 2")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg2016] where subject='A' and DATEPART(MM,CretaedDate)='8' and CurDist='" + UserId + "' order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 5")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject='A' and CretaedDate>'08/20/2019' and CurDist='" + UserId + "' order by CretaedDate";
            }
            else
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject='A' and CurDist='" + UserId + "' order by CretaedDate";
            }
        }

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            gvAproved.DataSource = dt;
            gvAproved.DataBind();
            trApprovedList.Visible = true;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No Record Found...'); </script> ");
            trApprovedList.Visible = false;
        }
    }
    public void GetApprovedListHR()
    {
        btnPrint.Visible = true;
        if (LoginType == "H")
        {
            if (ddlphase.SelectedItem.Text == "Phase 1")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg2016] where subject is null and DATEPART(MM,CretaedDate)='6' order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 2")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg2016] where subject is null and DATEPART(MM,CretaedDate)='8' order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 3")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject is null and DATEPART(MM,CretaedDate) in ('6','7') order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 4")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject is null and DATEPART(MM,CretaedDate) in ('10','11') order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 5")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject is null and CretaedDate>'08/20/2019' order by CretaedDate";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 6")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject is null and CretaedDate>'04/05/2021' order by CretaedDate";
            }
        }
        else if (LoginType == "R")
        {
            qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id=[Tbl_TribalReg].CurDist where subject is null and tbl_MetaData_DISTRICT.Region_ID='" + UserId + "' order by CretaedDate";
        }

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            gvAproved.DataSource = dt;
            gvAproved.DataBind();
            trApprovedList.Visible = true;
            Label5.Text = "Applied Application List";

        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No Record Found...'); </script> ");
            trApprovedList.Visible = false;
        }
    }
    public void GetAppDetail()
    {
        if (LoginType == "D")
        {
            if (ddlphase.SelectedItem.Text == "Phase 1")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg2016] where subject is null and DATEPART(MM,CretaedDate)='6' and CurDist='" + UserId + "' order by [CretaedDate]";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 2")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg2016] where subject is null and DATEPART(MM,CretaedDate)='8' and CurDist='" + UserId + "' order by [CretaedDate]";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 3")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject is null and DATEPART(MM,CretaedDate) in ('6','7') and CurDist='" + UserId + "' order by [CretaedDate]";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 4")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject is null and DATEPART(MM,CretaedDate) in ('10','11') and CurDist='" + UserId + "' order by [CretaedDate]";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 5")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject is null and DATEPART(MM,CretaedDate) in ('9') and CurDist='" + UserId + "' order by [CretaedDate]";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 6")
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject is null and DATEPART(MM,CretaedDate) in ('4') and CurDist='" + UserId + "' order by [CretaedDate]";
            }
            else
            {
                qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where subject is null and CretaedDate>'08/15/2019' and CurDist='" + UserId + "' order by [CretaedDate]";
            }
        }
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GVApp.DataSource = dt;
            GVApp.DataBind();
            lblRowCount.Text = "Total records are : " + dt.Rows.Count.ToString();

            Session["dtApp"] = dt;
            trPendList.Visible = true;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No Record Found...'); </script> ");
            lblheading.Visible = false;
            lblRowCount.Visible = false;
            trPendList.Visible = false;
        }
    }
    protected void GVApp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string FId = "";
        if (e.CommandName == "Approve")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = GVApp.Rows[rowIndex];

            if (row.RowType == DataControlRowType.DataRow)
            {
                Label txtId = (Label)GVApp.Rows[rowIndex].FindControl("lblTAID");
                FId = txtId.Text;
                Approve_Detail(FId);
                ViewState["FID"] = FId;
                
            }
          
        }
        else if (e.CommandName == "Reject")
        {
          
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = GVApp.Rows[rowIndex];

            if (row.RowType == DataControlRowType.DataRow)
            {
                Label txtId = (Label)GVApp.Rows[rowIndex].FindControl("lblTAID");
                FId = txtId.Text;
              
                ViewState["FID"] = FId;
            }
            pnllogin.Visible = true;
        }
        else if (e.CommandName == "ViewFile")
        {
            //ModalPopupExtender1.Show();
            Panel1.Visible = true;
          
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = GVApp.Rows[rowIndex];

            if (row.RowType == DataControlRowType.DataRow)
            {
                Label txtId = (Label)GVApp.Rows[rowIndex].FindControl("lblTAID");
                string FIds = "";
                FIds = txtId.Text;
                ViewState["FID"] = FIds;
                //GetFile(FIds);
            }
        }
    }
    public void GetFile(string FID)
    {
        string DQuery = "select RojgarPic,RojgarPicName,RojgarPicType,AppPic,AppPicName,AppPicType from Tbl_TribalReg where TAID='"+ FID +"'";
        SqlCommand cmd = new SqlCommand(DQuery,con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Byte[] Appbytes = (Byte[])dt.Rows[0]["AppPic"];
            String FileName = dt.Rows[0]["AppPicType"].ToString();

            string FileId = "";
            FileId = FID;
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "/" + FileName;
            Response.AddHeader("content-disposition", "attachment;filename="
            + dt.Rows[0]["AppPicName"].ToString());
            Response.BinaryWrite(Appbytes);
            Response.Flush();
            Response.End();
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occured...'); </script> ");
        }
    }
    public void GetFileDoc(string FID)
    {
        string DQuery = "select RojgarPic,RojgarPicName,RojgarPicType,AppPic,AppPicName,AppPicType from Tbl_TribalReg where TAID='" + FID + "'";
        SqlCommand cmd = new SqlCommand(DQuery, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Byte[] Docbytes = (Byte[])dt.Rows[0]["RojgarPic"];
            String FileName = dt.Rows[0]["RojgarPicType"].ToString();
            string FileId = "";
            FileId = FID;
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "/" + FileName;
            Response.AddHeader("content-disposition", "attachment;filename="
            + dt.Rows[0]["RojgarPicName"].ToString());
            Response.BinaryWrite(Docbytes);
            Response.Flush();
            Response.End();
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occured...'); </script> ");

        }
    }
    public void GetEducationCer(string FID)
    {
        string DQuery = "select EducationPic,EducationPicName,EducationPicType from Tbl_TribalReg where TAID='" + FID + "'";
        SqlCommand cmd = new SqlCommand(DQuery, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Byte[] Docbytes = (Byte[])dt.Rows[0]["EducationPic"];
            String FileName = dt.Rows[0]["EducationPicType"].ToString();
            string FileId = "";
            FileId = FID;
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "/" + FileName;
            Response.AddHeader("content-disposition", "attachment;filename="
            + dt.Rows[0]["EducationPicName"].ToString());
            Response.BinaryWrite(Docbytes);
            Response.Flush();
            Response.End();
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occured...'); </script> ");

        }

    }
    public void GetCastCer(string FID)
    {
        string DQuery = "select CastPic,CastPicName,CastPicType from Tbl_TribalReg where TAID='" + FID + "'";
        SqlCommand cmd = new SqlCommand(DQuery, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Byte[] Docbytes = (Byte[])dt.Rows[0]["CastPic"];
            String FileName = dt.Rows[0]["CastPicType"].ToString();
            string FileId = "";
            FileId = FID;
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "/" + FileName;
            Response.AddHeader("content-disposition", "attachment;filename="
            + dt.Rows[0]["CastPicName"].ToString());
            Response.BinaryWrite(Docbytes);
            Response.Flush();
            Response.End();
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occured...'); </script> ");

        }
    }
    public void Approve_Detail(string id)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string APPID = id;
        int CT = 0;
        qry = "update Tbl_TribalReg set Subject='A',ModifiedBy='" + ip + "',ModifiedDate=getdate() where TAID='" + APPID + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        con.Open();
        CT=cmd.ExecuteNonQuery();
        con.Close();
      
        if (CT > 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Approved Successfully...'); </script> ");
            GetAppDetail();
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occured...'); </script> ");
        }
        
    }
    public void Reject_Reason(string id)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string APPID = id;
        int CT = 0;
        qry = "update Tbl_TribalReg set Subject='R',ModifiedBy='" + ip + "',ModifiedDate=getdate(), Remark=N'" + txtReason.Text + "' where TAID='" + APPID + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        con.Open();
        CT = cmd.ExecuteNonQuery();
        con.Close();

        if (CT > 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Reject Successfully...'); </script> ");
            GetAppDetail();
            txtReason.Text = "";
            pnllogin.Visible = false;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occured...'); </script> ");
        }

    }
    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("StateRegionDistrictLogin.aspx");
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Report" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GvReport.GridLines = GridLines.Both;
        GvReport.HeaderStyle.Font.Bold = true;
        GvReport.UseAccessibleHeader = true;
        GvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        GvReport.FooterRow.TableSection = TableRowSection.TableFooter;
        //ListView1.Attributes["style"] = "border-collapse:separate";
        DivExport.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void GVApp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dts = (DataTable)Session["dtApp"];
        GVApp.PageIndex = e.NewPageIndex;
        //fillGrid(ds);
        FillAppDetail(dts);
    }
    private void FillAppDetail(DataTable dt)
    {
        GVApp.DataSource = dt;
        GVApp.DataBind();
       // lblRowCount.Text = "Total records are : " + godown_GridView.Rows.Count.ToString();
    }
    protected void btnSubmitR_Click(object sender, EventArgs e)
    {
        string APN = "";
        APN = ViewState["FID"].ToString();
        Reject_Reason(APN);

    }
    protected void btnCancelR_Click(object sender, EventArgs e)
    {
        pnllogin.Visible = false;
    }
    public void hp2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void btn_click(object sender, EventArgs e)
    {
        ImageButton ibtn1 = sender as ImageButton;
        int rowIndex = Convert.ToInt32(ibtn1.Attributes["RowIndex"]);
    }
    protected void bynImg_Click(object sender, EventArgs e)
    {
        string App_Id = "";
        App_Id = ViewState["FID"].ToString();
        GetFile(App_Id);
        Panel1.Visible = false;
    }
    protected void btnDoc_Click(object sender, EventArgs e)
    {
        string App_Id = "";
        App_Id = ViewState["FID"].ToString();
        GetFileDoc(App_Id);
        Panel1.Visible = false;
    }
    protected void x_Click(object sender, ImageClickEventArgs e)
    {
        Panel1.Visible = false;
    }
    public void Total_Application_Status()
    {
        if (LoginType == "H")
        {
            if (ddlphase.SelectedItem.Text == "Phase 1")
            {
                qry = "SELECT tbl_MetaData_DISTRICT.District_Name_HI as WarDist,tbl_Blocks.BlockName_H as WarBlock, count(TAID) as Total,(SELECT  isnull(count(tt.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt where Subject='A' and tt.WarBlock=[Tbl_TribalReg].WarBlock) as Approved,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject='R' and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Rejected,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject is null and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Pending FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id= [Tbl_TribalReg].WarDist inner join tbl_Blocks on tbl_Blocks.BlockID=[Tbl_TribalReg].WarBlock group by District_Name_HI,BlockName_H,WarBlock order by WarDist,WarBlock";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 2")
            {
                qry = "SELECT tbl_MetaData_DISTRICT.District_Name_HI as WarDist,tbl_Blocks.BlockName_H as WarBlock, count(TAID) as Total,(SELECT  isnull(count(tt.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt where Subject='A' and tt.WarBlock=[Tbl_TribalReg].WarBlock) as Approved,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject='R' and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Rejected,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject is null and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Pending FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id= [Tbl_TribalReg].WarDist inner join tbl_Blocks on tbl_Blocks.BlockID=[Tbl_TribalReg].WarBlock group by District_Name_HI,BlockName_H,WarBlock order by WarDist,WarBlock";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 5")
            {
                qry = "SELECT tbl_MetaData_DISTRICT.District_Name_HI as WarDist,tbl_Blocks.BlockName_H as WarBlock, count(TAID) as Total,(SELECT  isnull(count(tt.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt where Subject='A' and CretaedDate>'08/20/2019' and tt.WarBlock=[Tbl_TribalReg].WarBlock) as Approved,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject='R' and CretaedDate > '08/20/2019' and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Rejected,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject is null and CretaedDate > '08/20/2019' and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Pending FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id= [Tbl_TribalReg].WarDist inner join tbl_Blocks on tbl_Blocks.BlockID=[Tbl_TribalReg].WarBlock where Tbl_TribalReg.CretaedDate > '08/20/2019' group by District_Name_HI,BlockName_H,WarBlock order by WarDist,WarBlock";
            }
            else
            {
                qry = "SELECT tbl_MetaData_DISTRICT.District_Name_HI as WarDist,tbl_Blocks.BlockName_H as WarBlock, count(TAID) as Total,(SELECT  isnull(count(tt.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt where Subject='A' and tt.WarBlock=[Tbl_TribalReg].WarBlock) as Approved,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject='R' and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Rejected,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject is null and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Pending FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id= [Tbl_TribalReg].WarDist inner join tbl_Blocks on tbl_Blocks.BlockID=[Tbl_TribalReg].WarBlock group by District_Name_HI,BlockName_H,WarBlock order by WarDist,WarBlock";
            }
        }
        else if (LoginType == "R")
        {
            if (ddlphase.SelectedItem.Text == "Phase 1")
            {
                qry = "SELECT tbl_MetaData_DISTRICT.District_Name_HI as WarDist,tbl_Blocks.BlockName_H as WarBlock, count(TAID) as Total,(SELECT  isnull(count(tt.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt where Subject='A' and tt.WarBlock=[Tbl_TribalReg].WarBlock) as Approved,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject='R' and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Rejected,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject is null and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Pending FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id= [Tbl_TribalReg].WarDist inner join tbl_Blocks on tbl_Blocks.BlockID=[Tbl_TribalReg].WarBlock where tbl_MetaData_DISTRICT.Region_ID='" + UserId + "' group by District_Name_HI,BlockName_H,WarBlock order by WarDist,WarBlock";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 2")
            {
                qry = "SELECT tbl_MetaData_DISTRICT.District_Name_HI as WarDist,tbl_Blocks.BlockName_H as WarBlock, count(TAID) as Total,(SELECT  isnull(count(tt.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt where Subject='A' and tt.WarBlock=[Tbl_TribalReg].WarBlock) as Approved,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject='R' and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Rejected,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject is null and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Pending FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id= [Tbl_TribalReg].WarDist inner join tbl_Blocks on tbl_Blocks.BlockID=[Tbl_TribalReg].WarBlock where tbl_MetaData_DISTRICT.Region_ID='" + UserId + "' group by District_Name_HI,BlockName_H,WarBlock order by WarDist,WarBlock";
            }
            else
            {
                qry = "SELECT tbl_MetaData_DISTRICT.District_Name_HI as WarDist,tbl_Blocks.BlockName_H as WarBlock, count(TAID) as Total,(SELECT  isnull(count(tt.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt where Subject='A' and tt.WarBlock=[Tbl_TribalReg].WarBlock) as Approved,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject='R' and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Rejected,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject is null and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Pending FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id= [Tbl_TribalReg].WarDist inner join tbl_Blocks on tbl_Blocks.BlockID=[Tbl_TribalReg].WarBlock where tbl_MetaData_DISTRICT.Region_ID='" + UserId + "' group by District_Name_HI,BlockName_H,WarBlock order by WarDist,WarBlock";
            }
        }
        else if (LoginType == "D")
        {
            if (ddlphase.SelectedItem.Text == "Phase 1")
            {
                qry = "SELECT tbl_MetaData_DISTRICT.District_Name_HI as WarDist,tbl_Blocks.BlockName_H as WarBlock, count(TAID) as Total,(SELECT  isnull(count(tt.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt where Subject='A' and tt.WarBlock=[Tbl_TribalReg].WarBlock) as Approved,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject='R' and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Rejected,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject is null and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Pending FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id= [Tbl_TribalReg].WarDist inner join tbl_Blocks on tbl_Blocks.BlockID=[Tbl_TribalReg].WarBlock where tbl_MetaData_DISTRICT.District_Id='" + UserId + "' group by District_Name_HI,BlockName_H,WarBlock order by WarDist,WarBlock";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 2")
            {
                qry = "SELECT tbl_MetaData_DISTRICT.District_Name_HI as WarDist,tbl_Blocks.BlockName_H as WarBlock, count(TAID) as Total,(SELECT  isnull(count(tt.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt where Subject='A' and tt.WarBlock=[Tbl_TribalReg].WarBlock) as Approved,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject='R' and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Rejected,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject is null and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Pending FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id= [Tbl_TribalReg].WarDist inner join tbl_Blocks on tbl_Blocks.BlockID=[Tbl_TribalReg].WarBlock where tbl_MetaData_DISTRICT.District_Id='" + UserId + "' group by District_Name_HI,BlockName_H,WarBlock order by WarDist,WarBlock";
            }
            else if (ddlphase.SelectedItem.Text == "Phase 5")
            {
                qry = "SELECT tbl_MetaData_DISTRICT.District_Name_HI as WarDist,tbl_Blocks.BlockName_H as WarBlock, count(TAID) as Total,(SELECT  isnull(count(tt.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt where Subject='A' and CretaedDate > '08/20/2019' and tt.WarBlock=[Tbl_TribalReg].WarBlock) as Approved,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject='R' and CretaedDate > '08/20/2019' and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Rejected,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject is null and CretaedDate > '08/20/2019' and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Pending FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id= [Tbl_TribalReg].WarDist inner join tbl_Blocks on tbl_Blocks.BlockID=[Tbl_TribalReg].WarBlock where tbl_MetaData_DISTRICT.District_Id='" + UserId + "' group by District_Name_HI,BlockName_H,WarBlock order by WarDist,WarBlock";
            }
            else
            {
                qry = "SELECT tbl_MetaData_DISTRICT.District_Name_HI as WarDist,tbl_Blocks.BlockName_H as WarBlock, count(TAID) as Total,(SELECT  isnull(count(tt.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt where Subject='A' and tt.WarBlock=[Tbl_TribalReg].WarBlock) as Approved,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject='R' and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Rejected,(SELECT  isnull(count(tt2.TAID),0) as totalrecapr FROM [Tbl_TribalReg] as tt2 where Subject is null and tt2.WarBlock=[Tbl_TribalReg].WarBlock) as Pending FROM [Tbl_TribalReg] inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id= [Tbl_TribalReg].WarDist inner join tbl_Blocks on tbl_Blocks.BlockID=[Tbl_TribalReg].WarBlock where tbl_MetaData_DISTRICT.District_Id='" + UserId + "' group by District_Name_HI,BlockName_H,WarBlock order by WarDist,WarBlock";
            }
        }
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridTotalStatus.DataSource = dt;
            GridTotalStatus.DataBind();
            trSummary.Visible = true;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No Record Found...'); </script> ");
            lblheading.Visible = false;
            lblRowCount.Visible = false;
            trSummary.Visible = false;
        }

    }
    protected void btnSS_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Report" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GridTotalStatus.GridLines = GridLines.Both;
        GridTotalStatus.HeaderStyle.Font.Bold = true;
        GridTotalStatus.UseAccessibleHeader = true;
        GridTotalStatus.HeaderRow.TableSection = TableRowSection.TableHeader;
        GridTotalStatus.FooterRow.TableSection = TableRowSection.TableFooter;
        Div1.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
   public void CheckReportUrl()
    
   {
        if (URL == "HOR1")
        {
            if (LoginType == "D")
            {
                GetAppDetail();
            }
            else
            {
                GetApprovedListHR();               
            }         
        }
        else if (URL == "HOR2")
        {
            GetMeritList();
        }
        else if (URL == "HOR3")
        {
            Total_Application_Status();           
        }
        else if (URL == "HOR34")
        {
            GetApprovedList();           
        }
        else if (URL == "HOR4")
        {
           GetRejectedList();
        }
    }
   protected void Button2_Click(object sender, EventArgs e)
   {
       Response.Clear();
       Response.Buffer = true;
       Response.ClearContent();
       Response.ClearHeaders();
       Response.Charset = "";
       string FileName = "Report" + DateTime.Now + ".xls";
       StringWriter strwritter = new StringWriter();
       HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
       Response.Cache.SetCacheability(HttpCacheability.NoCache);
       Response.ContentType = "application/vnd.ms-excel";
       Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
       gvAproved.GridLines = GridLines.Both;
       gvAproved.HeaderStyle.Font.Bold = true;
       gvAproved.UseAccessibleHeader = true;
       gvAproved.HeaderRow.TableSection = TableRowSection.TableHeader;
       gvAproved.FooterRow.TableSection = TableRowSection.TableFooter;
       Div3.RenderControl(htmltextwrtter);
       Response.Write(strwritter.ToString());
       Response.End();
   }
   protected void Button1_Click(object sender, EventArgs e)
   {
       Response.Clear();
       Response.Buffer = true;
       Response.ClearContent();
       Response.ClearHeaders();
       Response.Charset = "";
       string FileName = "Report" + DateTime.Now + ".xls";
       StringWriter strwritter = new StringWriter();
       HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
       Response.Cache.SetCacheability(HttpCacheability.NoCache);
       Response.ContentType = "application/vnd.ms-excel";
       Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
       gvRejected.GridLines = GridLines.Both;
       gvRejected.HeaderStyle.Font.Bold = true;
       gvRejected.UseAccessibleHeader = true;
       gvRejected.HeaderRow.TableSection = TableRowSection.TableHeader;
       gvRejected.FooterRow.TableSection = TableRowSection.TableFooter;
       Div2.RenderControl(htmltextwrtter);
       Response.Write(strwritter.ToString());
       Response.End();
   }
   protected void gvAproved_RowCommand(object sender, GridViewCommandEventArgs e)
   {
        if (e.CommandName == "ViewFile2")
        {
            //ModalPopupExtender1.Show();
            Panel1.Visible = true;
          
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvAproved.Rows[rowIndex];

            if (row.RowType == DataControlRowType.DataRow)
            {
                Label txtId = (Label)gvAproved.Rows[rowIndex].FindControl("lblTAID");
                string FIds = "";
                FIds = txtId.Text;
                ViewState["FID"] = FIds;
                //GetFile(FIds);
            }
        }
        else if (e.CommandName == "PrintF")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvAproved.Rows[rowIndex];

            if (row.RowType == DataControlRowType.DataRow)
            {
                Label txtId = (Label)gvAproved.Rows[rowIndex].FindControl("lblTAID");
                string FIds = "";
                FIds = txtId.Text;
                Session["App_ID"] = FIds;
                Response.Redirect("PrintReg.aspx");
            }
            
        }
     
   }
   public void GetAllDoc(string FID)
   {
       string DQuery = "select RojgarPic,RojgarPicName,RojgarPicType,AppPic,AppPicName,AppPicType from Tbl_TribalReg where TAID='" + FID + "'";
       SqlCommand cmd = new SqlCommand(DQuery, con);
       SqlDataAdapter da = new SqlDataAdapter(cmd);
       DataTable dt = new DataTable();
       da.Fill(dt);
       if (dt.Rows.Count > 0)
       {
           Byte[] Docbytes = (Byte[])dt.Rows[0]["RojgarPic"];
           String FileName = dt.Rows[0]["RojgarPicType"].ToString();
           string FileId = "";
           FileId = FID;
           Response.Buffer = true;
           Response.Charset = "";
           Response.Cache.SetCacheability(HttpCacheability.NoCache);
           Response.ContentType = "/" + FileName;
           Response.AddHeader("content-disposition", "attachment;filename="
           + dt.Rows[0]["RojgarPicName"].ToString());
           Response.BinaryWrite(Docbytes);
           Response.Flush();
           Response.End();
       }
       else
       {
           ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occured...'); </script> ");

       }
      
   }
   protected void btnEducation_Click(object sender, EventArgs e)
   {
       string App_Id = "";
       App_Id = ViewState["FID"].ToString();
       GetEducationCer(App_Id);
       Panel1.Visible = false;
   }
   protected void btnCast_Click(object sender, EventArgs e)
   {
       string App_Id = "";
       App_Id = ViewState["FID"].ToString();
       GetCastCer(App_Id);
       Panel1.Visible = false;
   }
   protected void lnkHome_Click(object sender, EventArgs e)
   {
       Response.Redirect("Tribal_State_Home.aspx");
   }
   protected void GridTotalStatus_RowDataBound(object sender, GridViewRowEventArgs e)
   {
       //decimal total1 = 0;
       //decimal total2 = 0;
       //decimal total3 = 0;
       //decimal total4 = 0;

       if (e.Row.RowType == DataControlRowType.DataRow)
       {
           total1 += (DataBinder.Eval(e.Row.DataItem, "Total") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total")) : 0;

           total2 += (DataBinder.Eval(e.Row.DataItem, "Approved") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Approved")) : 0;

           total3 += (DataBinder.Eval(e.Row.DataItem, "Rejected") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Rejected")) : 0;

           total4 += (DataBinder.Eval(e.Row.DataItem, "Pending") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Pending")) : 0;
       }
       if (e.Row.RowType == DataControlRowType.Footer)
       {
           Label lblTotal = (Label)e.Row.FindControl("Total");

           lblTotal.Text = total1.ToString();

           Label lblApproved = (Label)e.Row.FindControl("Approved");

           lblApproved.Text = total2.ToString();

           Label lblRejected = (Label)e.Row.FindControl("Rejected");

           lblRejected.Text = total3.ToString();

           Label lblPending = (Label)e.Row.FindControl("Pending");

           lblPending.Text = total4.ToString();

          

       }
   }
   protected void ddlphase_SelectedIndexChanged(object sender, EventArgs e)
   {
       CheckReportUrl();
   }
}