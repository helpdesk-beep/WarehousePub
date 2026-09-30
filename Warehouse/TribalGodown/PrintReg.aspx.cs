using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class TribalGodown_PrintReg : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TribalGodownConString"].ToString());
    public string qry = "";
    public string App_Id = "";
    public string ImgName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string sess = Session["App_ID"].ToString();
            if (sess != "")
            {
                App_Id = sess;
                if (!IsPostBack)
                {
                    GetRegisterationData();
                    get_Region_Detail();
                }
            }
            else
            {
                Response.Redirect("UserReg.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("UserReg.aspx"); 
        }
      
    }
    public void GetRegisterationData()
    {
        string TrimTAID = App_Id.Substring(2, 4);
        if (TrimTAID == "2017")
        {
            qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],Graduation_Category,DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where [TAID]='" + App_Id + "'";
        }
        else if (TrimTAID == "2016")
        {
            qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],Graduation_Category,DistFromTO,QuantityOfForm FROM [Tbl_TribalReg2016] where [TAID]='" + App_Id + "'";
        }
        else if (TrimTAID == "2019")
        {
            qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],Graduation_Category,DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where [TAID]='" + App_Id + "'";
        }
        else if (TrimTAID == "2021")
        {
            qry = "SELECT [TAID],([FristName]+' '+[MName]+' '+[LName]) as FullName,[MothersName],[FathersName],[ACaste],[Email],[MobileNo],CONVERT(varchar(10),[DOB],103) as DateOfBirth,[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[CurDist]) as CDistrict,(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[ParDist]) as PDistrict,(select BlockName_H from tbl_Blocks where BlockID= [CurBlock]) as CBlock,(select Tehsil_Name from Tehsils where TehsilCode= [ParBlock]) as PBlock,[Education],[VoterId],[AdharCard],[PaNNo],(select District_Name_HI from tbl_MetaData_DISTRICT where District_Id=[WarDist]) as WDistrict,(select BlockName_H from tbl_Blocks where BlockID=[WarBlock]) as WBlock,[WarAddress],[WarExp],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate] as DateOfReg,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],Graduation_Category,DistFromTO,QuantityOfForm FROM [Tbl_TribalReg] where [TAID]='" + App_Id + "'";
        }
        SqlCommand cmd = new SqlCommand(qry,con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt=new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            App_Id = dt.Rows[0]["TAID"].ToString();
            lblAppNo.Text = App_Id;
            lblFullName.Text = dt.Rows[0]["FullName"].ToString();
            lblMother.Text = dt.Rows[0]["MothersName"].ToString();
            lblFather.Text = dt.Rows[0]["FathersName"].ToString();
            lblEmail.Text = dt.Rows[0]["Email"].ToString();
            lblMob.Text = dt.Rows[0]["MobileNo"].ToString();
            lblDOB.Text = dt.Rows[0]["DateOfBirth"].ToString();
            lblSex.Text = dt.Rows[0]["Sex"].ToString();
            lblRKN.Text = dt.Rows[0]["Rojgarnum"].ToString();
            lblSOI.Text = dt.Rows[0]["Sorcfinc"].ToString();
            lblCAddress.Text = dt.Rows[0]["CurrAddress"].ToString();
            lblCPIN.Text = dt.Rows[0]["CurrPinCode"].ToString();
            string Education = "";
            Education = dt.Rows[0]["Graduation_Category"].ToString();
            lblEducation.Text = dt.Rows[0]["Education"].ToString();
           
            lblCDistrict.Text = dt.Rows[0]["CDistrict"].ToString();
            lblCBlock.Text = dt.Rows[0]["CBlock"].ToString();
            lblVID.Text = dt.Rows[0]["VoterId"].ToString();
            lblADHAR.Text = dt.Rows[0]["AdharCard"].ToString();
            lblPAN.Text = dt.Rows[0]["PaNNo"].ToString();
            lblWDistrict.Text = dt.Rows[0]["WDistrict"].ToString();
            lblWBlock.Text = dt.Rows[0]["WBlock"].ToString();
            lblWAddress.Text = dt.Rows[0]["WarAddress"].ToString();
            lblBank.Text = dt.Rows[0]["BankName"].ToString();
            lblANO.Text = dt.Rows[0]["BankAcct"].ToString();
            lblIFSC.Text = dt.Rows[0]["IFSC"].ToString();
            lblP1.Text = dt.Rows[0]["FristKpName"].ToString();
            lblP1Add.Text = dt.Rows[0]["FristKpAdd"].ToString();
            lblP1Mob.Text = dt.Rows[0]["FirstKpMob"].ToString();
            lblP1Email.Text = dt.Rows[0]["FirstKpEmail"].ToString();
            lblP1Rel.Text = dt.Rows[0]["FirstKpRel"].ToString();

            lblP2.Text = dt.Rows[0]["SecKpName"].ToString();
            lblP2Add.Text = dt.Rows[0]["SecKpAdd"].ToString();
            lblP2Mob.Text = dt.Rows[0]["SecKpMob"].ToString();
            lblP2Email.Text = dt.Rows[0]["SecKpEmail"].ToString();
            lblP2Rel.Text = dt.Rows[0]["SecKpRel"].ToString();
            lblAppDate.Text = dt.Rows[0]["DateOfReg"].ToString();

            ImgName = dt.Rows[0]["AppPicName"].ToString();
            lbluser.Text = dt.Rows[0]["FullName"].ToString();

            lblDistanceQty.Text = dt.Rows[0]["DistFromTO"].ToString() + '/' + dt.Rows[0]["QuantityOfForm"].ToString();

            Fech_Image();
        }
    }
    public void Fech_Image()
    {
        string id = App_Id;
        Image1.Visible = id != "0";
        if (id != "0")
        {
            string TrimTAID = id.Substring(2, 4);
            if (TrimTAID == "2017")
            {
                byte[] bytes = (byte[])GetData("SELECT AppPic as Data FROM Tbl_TribalReg WHERE TAID =" + id).Rows[0]["Data"];
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                Image1.ImageUrl = "data:image/png;base64," + base64String;
            }
            else if (TrimTAID == "2016")
            {
                byte[] bytes = (byte[])GetData("SELECT AppPic as Data FROM Tbl_TribalReg2016 WHERE TAID =" + id).Rows[0]["Data"];
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                Image1.ImageUrl = "data:image/png;base64," + base64String;
            }
            if (TrimTAID == "2019")
            {
                byte[] bytes = (byte[])GetData("SELECT AppPic as Data FROM Tbl_TribalReg WHERE TAID =" + id).Rows[0]["Data"];
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                Image1.ImageUrl = "data:image/png;base64," + base64String;
            }
           
        }
    }
    private DataTable GetData(string query)
    {
        string QR = query;
        SqlCommand cmd = new SqlCommand(QR,con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        return dt1;
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("UserReg.aspx");
    }
    public void get_Region_Detail()
    {
        string qry = "SELECT MR.region,MR.Address,MR.Phone_No,MR.Mob_No,MR.Email FROM tbl_MetaData_Region as MR inner join tbl_MetaData_DISTRICT as MD on MD.Region_ID=MR.Region_Id inner join Tbl_TribalReg as TR on TR.WarDist=MD.District_Id where TR.TAID='" + App_Id + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //lblRegionAdd.Text = "क्षे॰ का॰ नाम :- " + dt.Rows[0]["region"].ToString() + ',' + " पता :- " + dt.Rows[0]["Address"].ToString() + ',' + " फोन :- " + dt.Rows[0]["Phone_No"].ToString() + ',' + dt.Rows[0]["Mob_No"].ToString();
            //lblRegionAdd.Text = "क्षे॰ का॰ नाम :- " + dt.Rows[0]["region"].ToString() + ", " + dt.Rows[0]["Address"].ToString() + ", " + dt.Rows[0]["Phone_No"].ToString() + ", " + dt.Rows[0]["Mob_No"].ToString() + ", " + dt.Rows[0]["Email"].ToString();
            lblRegionAdd.Text = "क्षे॰ का॰ नाम एवं पता :- " + dt.Rows[0]["region"].ToString() + "," + dt.Rows[0]["Address"].ToString();
            lblRegionAdd2.Text = "दूरभाष क्र॰ एवं ईमेल :- "+ dt.Rows[0]["Phone_No"].ToString() + "," + dt.Rows[0]["Mob_No"].ToString() + "," + dt.Rows[0]["Email"].ToString();
        }
        else
        {

        }
    }
}