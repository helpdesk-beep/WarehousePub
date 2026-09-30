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
using System.Data.OleDb;

public partial class JointVentureScheme_UploadPaymentData : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlTransaction sqltran;
    SqlCommand cmd;
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString().Trim() == "HOMPWLC" && Session["Scope"].ToString().Trim() == "H")
        {
            if (!IsPostBack)
            {

            }
        }
        else
        {
            Session.Abandon();
            Response.Redirect("Logins.aspx");
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Logins.aspx");
    }
    protected void PopulateGrid(object sender, EventArgs e)
    {
        // CHECK IF A FILE HAS BEEN SELECTED.
        if ((FileUpload.HasFile))
        {
            if (!Convert.IsDBNull(FileUpload.PostedFile) &
                    FileUpload.PostedFile.ContentLength > 0)
            {
                // SAVE THE SELECTED FILE IN THE ROOT DIRECTORY.
                FileUpload.SaveAs(Server.MapPath(".") + "\\" + FileUpload.FileName);

                // SET A CONNECTION WITH THE EXCEL FILE.
                OleDbConnection myExcelConn = new OleDbConnection
               ("Provider=Microsoft.ACE.OLEDB.12.0; " + "Data Source=" + Server.MapPath(".") + "\\" + FileUpload.FileName + ";Extended Properties=Excel 12.0;");
                try
                {
                    myExcelConn.Open();

                    // GET DATA FROM EXCEL SHEET.
                    OleDbCommand objOleDB =
                        new OleDbCommand("SELECT * FROM [Sheet1$]", myExcelConn);

                    // READ THE DATA EXTRACTED FROM THE EXCEL FILE.
                    OleDbDataReader objBulkReader = null;
                    objBulkReader = objOleDB.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(objBulkReader);

                    // FINALLY, BIND THE EXTRACTED DATA TO THE GRIDVIEW.
                    grid_payment.DataSource = dt;
                    grid_payment.DataBind();
                    txtgridcount.Text = "Total Record : "+Convert.ToString(grid_payment.Rows.Count);
                    lblConfirm.Text = "DATA IMPORTED TO THE GRID, SUCCESSFULLY.";
                    lblConfirm.Attributes.Add("style", "color:green");
                }
                catch (Exception ex)
                {
                    // SHOW ERROR MESSAGE, IF ANY.
                    lblConfirm.Text = ex.Message;
                    lblConfirm.Attributes.Add("style", "color:red");
                }
                finally
                {
                    // CLEAR.
                    myExcelConn.Close(); myExcelConn = null;
                }
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        sqltran = con.BeginTransaction();
        int rcount = 0;
        if (grid_payment.Rows.Count>0)
        {
            try
            {
                for (int i = 0;  grid_payment.Rows.Count>i; i++)
                {
                    int  bnkref = ChkBankReference(grid_payment.Rows[i].Cells[2].Text.ToString().Trim());
                    if (bnkref==0)
                    {
                        qry = "INSERT INTO [JointVentureScheme2018].[dbo].[tbl_Payment_Status] ([CategoryName],[PaymentMode],[BankReferenceNo],[TransactionDate],[Amount],[Status],[REGISTRATIONID],[NAMEOFDEPOSITOR],[CONTACTNO],[EMAILID],[FEE],[Remarks]) VALUES('" + grid_payment.Rows[i].Cells[0].Text.ToString().Trim() + "','" + grid_payment.Rows[i].Cells[1].Text.ToString().Trim() + "','" + grid_payment.Rows[i].Cells[2].Text.ToString().Trim() + "','" + grid_payment.Rows[i].Cells[3].Text.ToString().Trim() + "','" + grid_payment.Rows[i].Cells[4].Text.ToString().Trim() + "','" + grid_payment.Rows[i].Cells[5].Text.ToString().Trim() + "','" + grid_payment.Rows[i].Cells[6].Text.ToString().Trim() + "','" + grid_payment.Rows[i].Cells[7].Text.ToString().Trim() + "','" + grid_payment.Rows[i].Cells[8].Text.ToString().Trim() + "','" + grid_payment.Rows[i].Cells[9].Text.ToString().Trim() + "','" + grid_payment.Rows[i].Cells[10].Text.ToString().Trim() + "','" + grid_payment.Rows[i].Cells[11].Text.ToString().Trim() + "') ";
                        cmd = new SqlCommand(qry, con, sqltran);
                        int A11 = cmd.ExecuteNonQuery();
                        if (A11 == 1)
                        {
                            rcount = rcount + 1;
                        }
                    }
                }
                if (rcount >0)
                {
                    sqltran.Commit();
                    grid_payment.DataSource = "";
                    grid_payment.DataBind();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Insert Records...'); </script> ");
                }
            }
            catch (Exception ex)
            {
                sqltran.Rollback();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Something Error...'); </script> ");
            }
            finally
            {
                sqltran.Dispose();
                con.Close();
            }
        }
    }

    public int ChkBankReference(string bnkrefchk)
    {
        int chk = 0;
        string strsql = "select * from tbl_Payment_Status where BankReferenceNo='" + bnkrefchk.Trim() + "'";
        SqlCommand cmd = new SqlCommand(strsql, con, sqltran);
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
}
