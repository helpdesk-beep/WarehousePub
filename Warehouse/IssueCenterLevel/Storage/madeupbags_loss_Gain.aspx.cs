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

public partial class IssueCenterLevel_Storage_madeupbags_loss_Gain : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    string qry = "";
    DataSet ds = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (!IsPostBack)
            {
                txt_Gain.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
                txt_bags.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");
                txt_Gain.Attributes.Add("onkeypress", "return IsNumericProcQty(event,this)");
                txt_bags.Attributes.Add("onkeypress", "return IsNumericProcQty(event,this)");
                string depotId = Session["Depot_DepotID"].ToString();
                btnsave.Attributes.Add("OnClick", " return AskForComment()");
                fillCommodity();
                Printcurrentdate();
                rbtflag.SelectedValue = "L";
                string depotIdn = Session["Depot_DepotID"].ToString();
                GetWHRDetails(depotIdn);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    public void GetGodown()
    {
        if (ddl_cmdty.SelectedIndex != 0)
        {
            qry = " SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN] WHERE  DepotId = '" + Session["Depot_DepotID"] + "' and  Godown_ID in (select distinct Godown_ID from tbl_MetaData_STACK where  DepotId ='" + Session["Depot_DepotID"] + "' and  Commodity_Id='" + ddl_cmdty.SelectedValue + "')  ORDER BY [Godown_Name]";
            da = new SqlDataAdapter(qry, con);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_godown.DataSource = ds.Tables[0];
                ddl_godown.DataValueField = "Godown_ID";
                ddl_godown.DataTextField = "Godown_Name";
                ddl_godown.DataBind();
                ddl_godown.Items.Insert(0, new ListItem("--Select--", "0"));
                ddl_godown.SelectedIndex = 0;
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('please Select commodity')", true);
        }
    }

    protected void ddl_godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_godown.SelectedIndex != 0)
        {
            qry = "SELECT Stack_ID, Stack_Name FROM tbl_MetaData_STACK WHERE (Godown_ID = '" + ddl_godown.SelectedValue + "' and Commodity_Id = '" + ddl_cmdty.SelectedValue + "' and  Stack_Killed = 'N' ) order by Stack_Name ";
            da = new SqlDataAdapter(qry, con);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_stackno.DataSource = ds.Tables[0];
                ddl_stackno.DataValueField = "Stack_ID";
                ddl_stackno.DataTextField = "Stack_Name";
                ddl_stackno.DataBind();
                ddl_stackno.Items.Insert(0, new ListItem("--Select--", "0"));
                ddl_stackno.SelectedIndex = 0;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('There is no stack under selected commodity & Godown')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown First..')", true);
        }

    }

    private void fillCommodity()
    {
        qry = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name asc";
        cmd = new SqlCommand(qry, con);
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_cmdty.DataSource = ds.Tables[0];
            ddl_cmdty.DataTextField = "Commodity_Name";
            ddl_cmdty.DataValueField = "Commodity_Id";
            ddl_cmdty.DataBind();
            ddl_cmdty.Items.Insert(0, new ListItem("--Select--", "0"));
            ddl_cmdty.SelectedIndex = 0;
        }
    }

    protected void ddl_cmdty_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodown();
    }

    public void FillWhr()
    {
        qry = "SELECT DISTINCT Depositor_WHR_Id  FROM [tbl_storage_Depositor_WHR_Relation] WHR join [tbl_storage_Stacking_Details] SSD ON WHR.Depositor_WHR_Id = SSD.WHRId WHERE WHR.Depotid='" + Session["Depot_DepotID"].ToString() + "' AND SSD.Godown_ID='" + ddl_godown.SelectedValue.ToString() + "' AND SSD.Stack_ID = '" + ddl_stackno.SelectedValue.ToString() + "' ORDER BY Depositor_WHR_Id";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_whrno.DataSource = ds.Tables[0];
            ddl_whrno.DataValueField = "Depositor_WHR_Id";
            ddl_whrno.DataTextField = "Depositor_WHR_Id";
            ddl_whrno.DataBind();
            ddl_whrno.Items.Insert(0, new ListItem("--Select--", "0"));
            ddl_whrno.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('There is no WHR Found..')", true);
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_cmdty.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Commodity..')", true);
                return;
            }
            else if (ddl_godown.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown..')", true);
                return;
            }
            else if (ddl_stackno.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Stack..')", true);
                return;
            }
            else if (txt_bags.Text == "0" || txt_bags.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Insert number of bags')", true);
                return;
            }
            else if (txt_Gain.Text == "0" || txt_Gain.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Insert Quantity')", true);
                return;
            }
            else
            {
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (btnsave.Text == "Save")
                {
                    if (rbtflag.SelectedValue == "G")
                    {
                        string tbl = "INSERT INTO [Loss_gain]([District_Id],[Depotid],[Whrid],[Commodity_Id],[Godown_Id],[Stack_Id],[MBags],[Loss_GainQty],[Quantity],[IP_Address],[CreatedDate],[GFlag],Gain_Qnty,DOC) VALUES ('" + Session["Depot_DistID"] + "','" + Session["Depot_DepotID"] + "','" + ddl_whrno.SelectedValue + "','" + ddl_cmdty.SelectedValue + "','" + ddl_godown.SelectedValue + "','" + ddl_stackno.SelectedValue + "','" + txt_bags.Text + "',0,'','" + ip + "',GETDATE(),'" + rbtflag.SelectedValue.ToString() + "','" + txt_Gain.Text.Trim() + "','" + getDate_MDY(txt_collectiondate.Text.ToString()) + "')";
                        SqlCommand cmd = new SqlCommand(tbl, con);
                        int result1 = cmd.ExecuteNonQuery();
                        if (result1 > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Saved Sucessfully|')", true);
                            GetWHRDetails(Session["Depot_DepotID"].ToString());
                            txt_bags.Text = "0";
                            txt_Gain.Text = "0";
                            ddl_stackno.Items.Clear();
                            ddl_whrno.Items.Clear();
                            ddl_godown.Items.Clear();
                            btnsave.Text = "Save";
                            ddl_cmdty.SelectedIndex = 0;
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Try Again|')", true);
                        }
                    }
                    else
                    {
                        string tbl = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Loss_gain]([District_Id],[Depotid],[Whrid],[Commodity_Id],[Godown_Id],[Stack_Id],[MBags],[Loss_GainQty],[Quantity],[IP_Address],[CreatedDate],[GFlag],Gain_Qnty,DOC) VALUES ('" + Session["Depot_DistID"] + "','" + Session["Depot_DepotID"] + "','" + ddl_whrno.SelectedValue + "','" + ddl_cmdty.SelectedValue + "','" + ddl_godown.SelectedValue + "','" + ddl_stackno.SelectedValue + "','" + txt_bags.Text.Trim().ToString() + "','" + txt_Gain.Text.Trim().ToString() + "','','" + ip + "',GETDATE(),'" + rbtflag.SelectedValue.ToString() + "',0,'" + getDate_MDY(txt_collectiondate.Text.ToString()) + "')";
                        SqlCommand cmd = new SqlCommand(tbl, con);
                        int result1 = cmd.ExecuteNonQuery();

                        if (result1 > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Saved Sucessfully|')", true);
                            GetWHRDetails(Session["Depot_DepotID"].ToString());
                            txt_bags.Text = "0";
                            txt_Gain.Text = "0";
                            ddl_stackno.Items.Clear();
                            ddl_whrno.Items.Clear();
                            ddl_godown.Items.Clear();
                            btnsave.Text = "Save";
                            ddl_cmdty.SelectedIndex = 0;
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Try Again|')", true);
                        }
                    }

                }
                if (btnsave.Text == "Update")
                {
                    string Ins_logtbl = "insert into Loss_gain_log select * from Loss_gain where DepotId = '" + Session["Depot_DepotID"] + "' and Whrid='" + ddl_whrno.SelectedValue.ToString() + "'";
                    SqlCommand cmd1 = new SqlCommand(Ins_logtbl, con);
                    int x = cmd1.ExecuteNonQuery();
                    if (x > 0)
                    {
                        if (rbtflag.SelectedValue == "G")
                        {
                            string del_logtbl = "UPDATE [Loss_gain] SET [MBags]='" + txt_bags.Text.Trim().ToString() + "',[Gain_Qnty]='" + txt_Gain.Text.Trim().ToString() + "',[GFlag]='" + rbtflag.SelectedValue.ToString() + "',[DOC]='" + getDate_MDY(txt_collectiondate.Text.ToString()) + "'  WHERE DepotId = '" + Session["Depot_DepotID"] + "' and Whrid='" + ddl_whrno.SelectedValue + "' and Stack_Id = '" + ddl_stackno.SelectedValue.ToString() + "'";
                            SqlCommand cmd = new SqlCommand(del_logtbl, con);
                            int result = cmd.ExecuteNonQuery();
                            con.Close();
                            if (result > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Update Sucessfully|')", true);
                                GetWHRDetails(Session["Depot_DepotID"].ToString());
                                txt_bags.Text = "0";
                                txt_Gain.Text = "0";
                                ddl_stackno.Items.Clear();
                                ddl_whrno.Items.Clear();
                                ddl_godown.Items.Clear();
                                btnsave.Text = "Save";
                                ddl_cmdty.SelectedIndex = 0;
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Try Again|')", true);
                                txt_bags.Text = "0";
                                txt_Gain.Text = "0";
                            }
                        }
                        else
                        {
                            string del_logtbl = "UPDATE [Loss_gain] SET [MBags]='" + txt_bags.Text + "' ,[Loss_GainQty] ='" + txt_Gain.Text.Trim() + "',[GFlag]='" + rbtflag.SelectedValue.ToString() + "',[DOC]='" + getDate_MDY(txt_collectiondate.Text.ToString()) + "' WHERE DepotId = '" + Session["Depot_DepotID"] + "' and Whrid='" + ddl_whrno.SelectedValue + "' and Stack_Id = '" + ddl_stackno.SelectedValue.ToString() + "'";
                            SqlCommand cmd = new SqlCommand(del_logtbl, con);
                            int result = cmd.ExecuteNonQuery();
                            con.Close();
                            if (result > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Update Sucessfully|')", true);
                                GetWHRDetails(Session["Depot_DepotID"].ToString());
                                txt_bags.Text = "0";
                                txt_Gain.Text = "0";
                                ddl_stackno.Items.Clear();
                                ddl_whrno.Items.Clear();
                                ddl_godown.Items.Clear();
                                btnsave.Text = "Save";
                                ddl_cmdty.SelectedIndex = 0;
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Try Again|')", true);
                                txt_bags.Text = "0";
                                txt_Gain.Text = "0";
                            }
                        }

                    }
                }
            }
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        finally
        {
            con.Close();
        }
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }

    protected void ddl_stackno_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_cmdty.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Commodity..')", true);
            return;
        }
        else if (ddl_godown.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown..')", true);
            return;
        }
        else if (ddl_stackno.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Stack..')", true);
            return;
        }
        else
        {
            FillWhr();
        }
    }

    protected void Printcurrentdate()
    {
        qry = "SELECT  convert(varchar(10),getdate(),103) as 'Date1'";
        cmd = new SqlCommand(qry, con);
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_collectiondate.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
        }
    }

    private void GetWHRDetails(string depotid)
    {
        try
        {
            qry = "SELECT DISTINCT WHR.Depositor_WHR_Id,WHR.Depositor_Name,MSC.Commodity_Name,TMG.Godown_Name,TMS.Stack_Name,sum(TSSD.Bags) as Recied_Bags,CONVERT(DECIMAL(18,2),sum(TSSD.Weight)) AS Recivedqty,isnull(sum(SSD.No_Of_Bags),0) as DelBags,CONVERT(DECIMAL(18,2),isnull(sum(SSD.Bags_Weight),0)) as DelQty,isnull(sum(Loss_GainQty),0)  as loss,isnull(sum(Gain_Qnty),0) as gain,(sum(TSSD.Bags) - isnull(sum(SSD.No_Of_Bags),0)) AS Availbags,CONVERT(DECIMAL(18,2),(sum(TSSD.Weight) - isnull(sum(SSD.Bags_Weight),0)-isnull(sum(Loss_GainQty),0)+isnull(sum(Gain_Qnty),0))) as Availwet FROM tbl_storage_Depositor_WHR_Relation AS WHR JOIN tbl_storage_Stacking_Details TSSD ON WHR.Depositor_WHR_Id = TSSD.WHRId left JOIN tbl_Delivery_Stacking_Details_GatePass AS SSD ON WHR.Depositor_WHR_Id = SSD.Depositor_WHR_Id join tbl_MetaData_STORAGE_COMMODITY as MSC ON WHR.Commodity_Id = MSC.Commodity_Id join tbl_MetaData_STACK TMS ON TSSD.Stack_ID = TMS.Stack_ID JOIN tbl_MetaData_GODOWN TMG ON TSSD.Godown_ID = TMG.Godown_ID join Loss_gain as ls on SSD.Depositor_WHR_Id=ls.Whrid where WHR.Depotid ='" + depotid + "' and TMS.Stack_Killed = 'N' group by WHR.Depositor_WHR_Id,WHR.Depositor_Name,MSC.Commodity_Name,TMG.Godown_Name,TMS.Stack_Name";
            cmd = new SqlCommand(qry, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvwhr.DataSource = ds.Tables[0];
                gvwhr.DataBind();
            }
            else
            {
                gvwhr.DataSource = null;
                gvwhr.DataBind();
            }
        }
        catch (Exception)
        {
            ///////
        }
    }

    private void GetWHRDetailswithid()
    {
        try
        {
            qry = "select MBags,Loss_GainQty,Gain_Qnty,GFlag from Loss_gain where Loss_gain.Whrid = '" + ddl_whrno.SelectedValue.ToString() + "' and Stack_Id = '"+ ddl_stackno.SelectedValue.ToString() +"'";
            cmd = new SqlCommand(qry, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string flag = ds.Tables[0].Rows[0]["GFlag"].ToString();
                if (flag == "G")
                {
                    txt_bags.Text = ds.Tables[0].Rows[0]["MBags"].ToString();
                    txt_Gain.Text = ds.Tables[0].Rows[0]["Gain_Qnty"].ToString();
                    rbtflag.SelectedValue = ds.Tables[0].Rows[0]["GFlag"].ToString();
                    btnsave.Text = "Update";
                }
                else
                {
                    txt_bags.Text = ds.Tables[0].Rows[0]["MBags"].ToString();
                    txt_Gain.Text = ds.Tables[0].Rows[0]["Loss_GainQty"].ToString();
                    rbtflag.SelectedValue = ds.Tables[0].Rows[0]["GFlag"].ToString();
                    btnsave.Text = "Update";
                }
                
            }
            else
            {
                txt_bags.Text = "0";
                txt_Gain.Text = "0";
            }
        }
        catch (Exception)
        {
            ///////
        }
    }

    protected void ddl_whrno_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_cmdty.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Commodity..')", true);
            return;
        }
        else if (ddl_godown.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown..')", true);
            return;
        }
        else if (ddl_stackno.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Stack..')", true);
            return;
        }
        else
        {
            GetWHRDetailswithid();
        }
    }
}