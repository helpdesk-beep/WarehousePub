using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class StatePages_Insert_Excel_Data : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // fill();
            //filldistict();

        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FolderPath = Server.MapPath("~/Files/");

            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            string FilePath = FolderPath + FileName;
            FileUpload1.SaveAs(FilePath);

            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath +
                            ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';";

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();

            cmdExcel.Connection = connExcel;
            connExcel.Open();

            DataTable dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["tbl_Insert_LatLong_Excel"].ToString();
            connExcel.Close();

            connExcel.Open();
            cmdExcel.CommandText = "SELECT * FROM [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            string conStrSQL = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conStrSQL))
            {
                con.Open();
                foreach (DataRow row in dt.Rows)
                {
                    // ✅ Change table name & column names below
                    string query = @"INSERT INTO tbl_Update_LatLong
                        (District_Name, Godown_ID, Godown_Name, Issue_Center_Id, Issue_Center_Name, Lat, Long)
                        VALUES(@District_Name, @Godown_ID, @Godown_Name, @Issue_Center_Id, @Issue_Center_Name, @Lat, @Long)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@District_Name", row["District Name"].ToString());
                    cmd.Parameters.AddWithValue("@Godown_ID", row["Godown ID"].ToString());
                    cmd.Parameters.AddWithValue("@Godown_Name", row["Godown Name"].ToString());
                    cmd.Parameters.AddWithValue("@Issue_Center_Id", row["Issue Center Id"].ToString());
                    cmd.Parameters.AddWithValue("@Issue_Center_Name", row["Issue Center Name"].ToString());
                    cmd.Parameters.AddWithValue("@Lat", string.IsNullOrEmpty(row["Lat"].ToString()) ? DBNull.Value : (object)Convert.ToDecimal(row["Lat"]));
                    cmd.Parameters.AddWithValue("@Long", string.IsNullOrEmpty(row["Long"].ToString()) ? DBNull.Value : (object)Convert.ToDecimal(row["Long"]));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }

            lblMsg.Text = "✅ Data inserted successfully!";
        }
        else
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please select an Excel file first.";
        }
    }
}