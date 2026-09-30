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
using System.IO;
using System.Data.SqlClient;

public partial class Procurement_Upload_WHR : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public string qry = "";
    DataTable Dt1 = new DataTable();
    SqlCommand cmd = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string fileName = fileuploadimage.PostedFile.FileName;
        int fileLength = fileuploadimage.PostedFile.ContentLength;
        byte[] imageBytes = new byte[fileLength];
        fileuploadimage.PostedFile.InputStream.Read(imageBytes, 0, fileLength);
        string StrimageBytes = Convert.ToBase64String(imageBytes);
        string Extension = Path.GetExtension(fileuploadimage.PostedFile.FileName);

        //string sql = "INSERT INTO Tbl_TribalReg([TAID],[FristName],[MName],[LName],[MothersName],[FathersName],[ACaste],[Email],[MobileNo],[DOB],[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],[CurDist],[ParDist],[CurBlock],[ParBlock],[Education],[VoterId],[AdharCard],[PaNNo],[WarDist],[WarBlock],[WarAddress],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate],CreateBy,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],AID,Graduation_Category,EducationPicName,EducationPicType,EducationPic,CastPicName,CastPicType,CastPic,DistFromTO,QuantityOfForm) VALUES (@TAID,@FristName,@MName,@LName,@MothersName,@FathersName,@ACaste,@Email,@MobileNo,@DOB,@TtlFmlIncome,@Sex,@TtlFmlMembers,@Occupation,@ProjCost,@Rojgarnum,@Sorcfinc,@CurrAddress,@ParAddress,@CurrPinCode,@ParPinCode,@CurDist,@ParDist,@CurBlock,@ParBlock,@Education,@VoterId,@AdharCard,@PaNNo,@WarDist,@WarBlock,@WarAddress,@BankName,@BankAcct,@IFSC,@FristKpName,@SecKpName,@FristKpAdd,@SecKpAdd,@FirstKpMob,@SecKpMob,@FirstKpEmail,@SecKpEmail,@FirstKpRel,@SecKpRel,@CretaedDate,@CreateBy,@RojgarPicName,@RojgarPicType,@RojgarPic,@AppPicName,@AppPicType,@AppPic,@AID,@Graduation_Category,@EducationPicName,@EducationPicType,@EducationPic,@CastPicName,@CastPicType,@CastPic,@DistFromTO,@QuantityOfForm)";
        qry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_WHR_File_Data]([WHR_Id],[WHR_File],[WHR_File_Type],[WHR_File_Name]) VALUES(@WHR_Id,@WHR_File,@WHR_File_Type,@WHR_File_Name)";

        SqlCommand cmd = new SqlCommand(qry, Con);
        SqlParameter[] prms = new SqlParameter[4];


        prms[0] = new SqlParameter("@WHR_Id", SqlDbType.VarChar, 50);
        prms[0].Value = txtUploadWHRId.Text;
        prms[1] = new SqlParameter("@WHR_File", SqlDbType.Image);
        prms[1].Value = imageBytes;
        prms[2] = new SqlParameter("@WHR_File_Type", SqlDbType.VarChar, 50);
        prms[2].Value = Extension;
        prms[3] = new SqlParameter("@WHR_File_Name", SqlDbType.NVarChar, 50);
        prms[3].Value = fileName;

        int CT = 0;
        cmd.Parameters.AddRange(prms);
        Con.Open();
        CT = cmd.ExecuteNonQuery();
        Con.Close();
        if (CT > 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Save Successfully...'); </script> ");
            //Session["App_ID"] = App_No;
            //Response.Redirect("PrintReg.aspx");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occured...'); </script> ");
        }
    }
    protected void BtnPdf_Click(object sender, EventArgs e)
    {
        //string qry = "select [DSWHR_FIle] from [tbl_WHR_File_Data]";
        string qry = "select [WHR_File] from [tbl_WHR_File_Data] where WHR_Id='"+ txtDownloadWHRId.Text +"'";

        SqlCommand cmd = new SqlCommand(qry,Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        // get the data from the datatable
        byte[] bytFile = (byte[])dt.Rows[0]["WHR_File"];
        // you need to pass the file extension and MIME type & ContentType - hardcoded for brevity
        // note the \ characters are to escape the " quote marks
        this.Context.Response.ContentType = "\".pdf\",\"application/pdf\"";
        // add Header
        this.Context.Response.AddHeader("Content-disposition", "attachment; filename=Request WHR.pdf");
        this.Context.Response.BinaryWrite(bytFile);
        Response.End(); 
    }
    protected void btnPdfSigned_Click(object sender, EventArgs e)
    {
        string qry = "select [DSWHR_FIle] from [tbl_WHR_File_Data] where WHR_Id='" + txtDownloadWHRId.Text + "'";
        //string qry = "select [WHR_File] from [tbl_WHR_File_Data]";

        SqlCommand cmd = new SqlCommand(qry, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        // get the data from the datatable
        byte[] bytFile = (byte[])dt.Rows[0]["DSWHR_FIle"];
        // you need to pass the file extension and MIME type & ContentType - hardcoded for brevity
        // note the \ characters are to escape the " quote marks
        this.Context.Response.ContentType = "\".pdf\",\"application/pdf\"";
        // add Header
        this.Context.Response.AddHeader("Content-disposition", "attachment; filename=E-Sign WHR.pdf");
        this.Context.Response.BinaryWrite(bytFile);
        Response.End(); 
    }
}
