using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DeleteRefranceFile : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_JVS = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void fillgrid()
    {

        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Payment_File_Status", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if(txtbillnumber.Text=="")
                {
                    cmd.Parameters.AddWithValue("@BillNo", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BillNo", txtbillnumber.Text);
                }
                if (txtrefranceno.Text == "")
                {
                    cmd.Parameters.AddWithValue("@RefranceNo", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RefranceNo", txtbillnumber.Text);
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
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

    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

    }

    protected void tbnview_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    public void RemoveRow(string id,string BillNo,string Refranceno)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (con_WLC.State == ConnectionState.Closed)
        {
            con_WLC.Open();
        }
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (con_WLC.State == ConnectionState.Closed)
            {
                con_WLC.Open();
            }

            SqlCommand cmd = new SqlCommand("Delete_Wrong_NEFT_File", con_WLC
                );
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Godown_ID", id.ToString());
            cmd.Parameters.AddWithValue("@Bill_No", BillNo.ToString());
            cmd.Parameters.AddWithValue("@Reference_No", Refranceno.ToString());
            //cmd.Parameters.AddWithValue("@Insert_By", localIP.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GridView1.Rows[rowIndex];

            //Fetch value of Name.
            string lblGodown_ID = (row.FindControl("lblGodown_ID") as Label).Text;
            string lblBill_Number = (row.FindControl("lblBill_Number") as Label).Text;
            string lblRef_Bill_No = (row.FindControl("lblRef_Bill_No") as Label).Text;
            Session["lblGodown_ID"] = lblGodown_ID.ToString();
            Session["lblBill_Number"] = lblBill_Number.ToString();
            Session["lblRef_Bill_No"] = lblRef_Bill_No.ToString();
            RemoveRow(lblGodown_ID, lblBill_Number, lblRef_Bill_No);
            // RemoveRowJVS(hdnId);

        }
    }
}