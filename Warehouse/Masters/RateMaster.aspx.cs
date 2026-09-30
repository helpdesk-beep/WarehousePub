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

public partial class Masters_RateMaster : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetPackingType();
            GetVerity();
            GetWeight();
        }
    }

    void GetVerity()
    {
        string query = "select verity_code,verity_Eng from dbo.tbl_MetaData_Verity";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlverity.DataSource = ds.Tables[0];
            ddlverity.DataTextField = "verity_Eng";
            ddlverity.DataValueField = "verity_code";
            ddlverity.DataBind();
            ddlverity.Items.Insert(0, "--Select--");
        }
    }

    void GetCommodityChk()
    {
        CheckBoxList1.Items.Clear();
        string verty = ddlverity.SelectedValue;
        string query = "select Commodity_ID,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Rep_Grp_Code='" + verty + "' order by Commodity_Name";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.Connection = Con;
        Con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lst = new ListItem();
            lst.Text = dr["Commodity_Name"].ToString();
            lst.Value = dr["Commodity_ID"].ToString();
            CheckBoxList1.Items.Add(lst);
        }
    }

    void GetPackingType()
    {
        string query = "select Packing_Id,Packing_Name + '('+rtrim(Remarks)+')' as Packing_Name from dbo.Packing_type ";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlpacktype.DataSource = ds.Tables[0];
            ddlpacktype.DataTextField = "Packing_Name";
            ddlpacktype.DataValueField = "Packing_Id";
            ddlpacktype.DataBind();
            ddlpacktype.Items.Insert(0, "--Select--");
        }
    }

    void GetWeight()
    {
        string query = "select Weigt_ID,Weight_Type from dbo.tbl_MetaData_WeightType";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlweight.DataSource = ds.Tables[0];
            ddlweight.DataTextField = "Weight_Type";
            ddlweight.DataValueField = "Weigt_ID";
            ddlweight.DataBind();
            ddlweight.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlverity_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCommodityChk();
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
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
        if (ddlverity.SelectedItem.Text == "--Select--" || ddlpacktype.SelectedItem.Text == "--Select--" || ddlweight.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Verity/Commodity/Packing Type/Weight');", true);
        }
        else
        {
            string fdate = getDate_MDY(fromdate.Text);
            string vdate = getDate_MDY(revdate.Text);

            if (fdate == "" || vdate == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Date.. ');", true);
            }
            else
            {
                string vcode = ddlverity.SelectedValue;
                string comdty = "";
                string packtype = ddlpacktype.SelectedValue;
                string weight = ddlweight.SelectedValue;
                string stheight = ddlstackheight.SelectedValue;
                string mfromdate = getDate_MDY(fromdate.Text);
                decimal rate = CheckNull(txtrate.Text);
                string mrevdate = getDate_MDY(revdate.Text);
                decimal revrate = CheckNull(txtrevrate.Text);
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string st = "A";
                string tid = "";
                string mremarks = txtremark.Text;
                string temp = "NNN";
                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    if (CheckBoxList1.Items[i].Selected == true)
                    {
                        comdty = CheckBoxList1.Items[i].Value;
                        tid = vcode + comdty + packtype + weight + stheight;
                        string query = "Select * from dbo.tbl_MetaData_Storage_Rate where Verity_Code=" + vcode + " and Commodity_Id=" + comdty + " and Packing_type='" + packtype + "'and Weight='" + weight + "'and Stack_Height='" + stheight + "'";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            string qryinsert = "insert into dbo.tbl_MetaData_Storage_Rate(Verity_Code,Commodity_Id,Packing_type,Weight,Stack_Height,Effective_date,Rate,Revised_date,Revised_rate,Created_date,ip,Status,Transuction_ID,Remarks)values(" + vcode + "," + comdty + ",'" + packtype + "','" + weight + "'," + stheight + ",'" + mfromdate + "'," + rate + ",'" + mrevdate + "'," + revrate + ",getdate(),'" + ip + "','" + st + "','" + tid + "','" + mremarks + "')";
                            cmd.CommandText = qryinsert;
                            cmd.Connection = Con;
                            try
                            {
                                Con.Open();
                                cmd.ExecuteNonQuery();
                                Con.Close();
                                string qrylogins = "insert into dbo.tbl_MetaData_Storage_Rate_log(Verity_Code,Commodity_Id,Packing_type,Weight,Stack_Height,Effective_date,Rate,Revised_date,Revised_rate,Created_date,ip,Status,Transuction_ID,Operation,Remarks)values(" + vcode + "," + comdty + ",'" + packtype + "','" + weight + "'," + stheight + ",'" + mfromdate + "'," + rate + ",'" + mrevdate + "'," + revrate + ",getdate(),'" + ip + "','" + st + "','" + tid + "','I','" + mremarks + "')";
                                cmd.CommandText = qrylogins;
                                cmd.Connection = Con;
                                try
                                {
                                    Con.Open();
                                    cmd.ExecuteNonQuery();
                                    Con.Close();
                                    temp = "YYY";
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
                            //lblchk.Visible = true;
                            //ListBox1.Visible = true;
                            //ListBox1.Items.Add(CheckBoxList1.Items[i].Text);
                        }
                    }
                }
                if (temp == "YYY")
                {
                    btnsave.Enabled = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Saved Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Commodity');", true);
                }
            }
        }
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenterLevel/Storage/Report_Region.aspx");
    }

    protected void btnnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Masters/RateMaster.aspx");
    }
}
