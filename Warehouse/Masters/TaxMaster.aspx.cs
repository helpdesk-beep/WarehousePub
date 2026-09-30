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

public partial class Masters_TaxMaster : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string qry = "";
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnnew.Visible = false;
            get_Tax_Detail();
        }
    }
    public void get_Tax_Detail()
    {
        try
        {
            qry = "select Service_Tax,TDS,Remarks from Tax_Master";
            SqlCommand cmd = new SqlCommand(qry, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtsertax.Text = dt.Rows[0]["Service_Tax"].ToString();
                txttds.Text = dt.Rows[0]["TDS"].ToString();
                txtremark.Text = dt.Rows[0]["Remarks"].ToString();
                txtsertax.Enabled = false;
                txttds.Enabled = false;
                txtremark.Enabled = false;
                btnsave.Text = "Edit";
            }
            else
            {
                btnsave.Text = "Insert";
            }
        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = ex.Message;
        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    decimal CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;
    }

    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        decimal stax = CheckNull(txtsertax.Text);
        decimal tds = CheckNull(txttds.Text);
        string remarks = txtremark.Text;
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

        if (btnsave.Text.ToString() == "Edit")
        {
            txtsertax.Enabled = true;
            txttds.Enabled = true;
            txtremark.Enabled = true;
            btnsave.Text = "Update";
        }
        else if (btnsave.Text.ToString() == "Insert")
        {
            string qryinsert = "insert into Tax_Master(Service_Tax,TDS,Remarks,Created_Date,IP)values(" + stax + "," + tds + ",'" + remarks + "',getdate(),'" + ip + "')";
            try
            {
                Con.Open();
                cmd.CommandText = qryinsert;
                cmd.Connection = Con;
                cmd.ExecuteNonQuery();
                Con.Close();
                //btnsave.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Inserted Successfully.. ');", true);
                //btnnew.Visible = true;
                //btnsave.Text = "Save";
                get_Tax_Detail();
            }
            catch (Exception Ex)
            {
                lblmsg.Visible = true;
                lblmsg.Text = Ex.Message;
            }
            finally
            {
                Con.Close();
            }
        }
        else
        {
                string qryinsert = "update dbo.Tax_Master set Service_Tax=" + stax + ",TDS=" + tds + ",Remarks='" + remarks + "',Created_Date=getdate(),IP='" + ip + "'"; 
                cmd.CommandText = qryinsert;
                cmd.Connection = Con;
                try
                {
                    Con.Open();
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    //btnsave.Enabled = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Updated Successfully.. ');", true);
                    //btnnew.Visible = true;
                    //btnsave.Text = "Save";
                    get_Tax_Detail();
                }
                catch (Exception Ex)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = Ex.Message;
                }
                finally
                {
                    Con.Close();
                }
        }
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenterLevel/Storage/Report_Region.aspx");
    }

    protected void btnnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Masters/TaxMaster.aspx");
    }
}
