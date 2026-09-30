using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Text;
using System.Resources;


public partial class IssueCenterLevel_QualityControl : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    SqlConnection _sqlCon = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
 
    HttpCookie mCookie = null;
    protected void Page_Init(object sender, EventArgs e)
    {
       

    }
  
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["lang"].ToString() == "Hindi")
            {

                //ResourceManager rm = ResourceManager.CreateFileBasedResourceManager("hindi", Server.MapPath("."), null);
                lblFortQulRept.Text = Resources.hindi.lblFortQulRept;

                lblInstruction.Text = Resources.hindi.lblInstruction;

                lblGodownNo.Text = Resources.hindi.lblGodownNo;
                lblStackNo.Text = Resources.hindi.lblStackNo;
                //btnsearch.Text = Resources.hindi.btnsearch;
                //lblRetrieveStackInfo.Text = Resources.hindi.lblRetrieveStackInfo;

                lblDOinspect.Text = Resources.hindi.lblDOinspect;

                lblClassOfStock.Text = Resources.hindi.lblClassOfStock;
                lblQltsKgsgms.Text = Resources.hindi.lblQltsKgsgms;
                lblClear.Text = Resources.hindi.lblClear;
                lblFew.Text = Resources.hindi.lblFew;
                lblHeavy.Text = Resources.hindi.lblHeavy;

                lblPercentOfWeevilledGrains.Text = Resources.hindi.lblPercentOfWeevilledGrains;
                lblBoredDamage.Text = Resources.hindi.lblBoredDamage;
                lblChalky.Text = Resources.hindi.lblChalky;
                lblGermiDiscolor.Text = Resources.hindi.lblGermiDiscolor;

                lblCatgryOfStock.Text = Resources.hindi.lblCatgryOfStock;
                lblCategoryA.Text = Resources.hindi.lblCategoryA;
                lblCategoryB.Text = Resources.hindi.lblCategoryB;
                lblCategoryC.Text = Resources.hindi.lblCategoryC;
                lblCategoryD.Text = Resources.hindi.lblCategoryD;
                lblMoisture.Text = Resources.hindi.lblMoisture;
                lblNatureofInfest.Text = Resources.hindi.lblNatureofInfest;
                lbltreatRecomd.Text = Resources.hindi.lbltreatRecomd;
                lblRemark.Text = Resources.hindi.lblRemark;

                lblProgress.Text = Resources.hindi.lblProgress;

                lblSprayedDetail.Text = Resources.hindi.lblSprayedDetail;
                lblTreatType.Text = Resources.hindi.lblTreatType;
                lbltreatDate.Text = Resources.hindi.lbltreatDate;
                lblNameChemicalUsed.Text = Resources.hindi.lblNameChemicalUsed;

                lblFumigatedDetails.Text = Resources.hindi.lblFumigatedDetails;
                lblTypeOfTreatment.Text = Resources.hindi.lblTypeOfTreatment;
                lblDateOfTreatment.Text = Resources.hindi.lblDateOfTreatment;
                lblChemicalName.Text = Resources.hindi.lblChemicalName;

            }
          

            if (!IsPostBack)
            {
                // to take care of REFRESH using RIGHT CLICK of MOUSE
                Session["RefreshButton"] = "No";
                string PopMsg = "";
                PopMsg = Request.QueryString["PopMsg"];
                if (PopMsg != null)
                {
                    //The Page is Reloaded with Message
                    StringBuilder str = new StringBuilder();
                    str.Append("<script>");
                    str.Append("alert('" + PopMsg + "');</script>");
                    this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());

                }

                fillddlgodownlist();
                //fillddlstacklist();
              
            }
          
            Printcurrentdate();
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occured, try again" + "," + "Error:-" + ex.Message + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
    }

    private void fillddlgodownlist()
    {

        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {

            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter dap = new SqlDataAdapter();
                if (_sqlCon.State == ConnectionState.Closed)
                {
                    _sqlCon.Open();
                }

                string str = "SELECT Godown_Name, Godown_ID FROM tbl_MetaData_GODOWN WHERE (DepotId = '" + Session["Depot_DepotID"].ToString() + "') order by Godown_Name";

                SqlCommand _cmd = new SqlCommand(str, _sqlCon);
                _cmd.CommandType = CommandType.Text;
                dap.SelectCommand = _cmd;
                dap.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlgodownlist.DataSource = ds;
                    ddlgodownlist.DataTextField = "Godown_Name";
                    ddlgodownlist.DataValueField = "Godown_ID";
                    ddlgodownlist.DataBind();
                    ddlgodownlist.Items.Insert(0, "--Select--");

                }

                _cmd.Dispose();
                _sqlCon.Close();
                ds.Clear();
            }
            catch (Exception ex)
            {
                StringBuilder str = new StringBuilder();
                str.Append("<script>");
                str.Append("alert('" + "Some error has occured, try again" + "," + "Error:-" + ex.Message + "');</script>");
                this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
            }
        }
        else {


            Response.Redirect("../../Logout.aspx");
        
        }
    }

    private void fillddlstacklist()
    {

        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                DataSet dsstc = new DataSet();
                SqlDataAdapter dap = new SqlDataAdapter();
                if (_sqlCon.State == ConnectionState.Closed)
                {
                    _sqlCon.Open();
                }

                string str = "SELECT Stack_ID, Stack_Name FROM tbl_MetaData_STACK where Godown_id ='" + ddlgodownlist.SelectedItem.Value + "'  and   DepotId  ='" + Session["Depot_DepotID"].ToString() + "'  order by Stack_Name";

                SqlCommand _cmdstc = new SqlCommand(str, _sqlCon);
                _cmdstc.CommandType = CommandType.Text;
                dap.SelectCommand = _cmdstc;
                dap.Fill(dsstc);
                if (dsstc.Tables[0].Rows.Count > 0)
                {
                    ddlstacklist.DataSource = dsstc;
                    ddlstacklist.DataTextField = "Stack_Name";
                    ddlstacklist.DataValueField = "Stack_ID";
                    ddlstacklist.DataBind();
                    ddlgodownlist.Items.Insert(0, "--Select--");
                }
                else
                {
                    ddlstacklist.Items.Clear();

                }


                _cmdstc.Dispose();

                _sqlCon.Close();
            }
            catch (Exception ex)
            {
                StringBuilder str = new StringBuilder();
                str.Append("<script>");
                str.Append("alert('" + "Some error has occured, try again" + "," + "Error:-" + ex.Message + "');</script>");
                this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
            }
        }
        else {

            Response.Redirect("../../Logout.aspx");
        
        }
    }
 
    protected void gdstackdetail_PreRender(object sender, EventArgs e)
    {
        try
        {
            int _count = gdstackdetail.Rows.Count;
            int i;
            int _bags = 0;
            decimal _wt = 0;
            if (_count > 0)
            {
                for (i = 0; i < gdstackdetail.Rows.Count; i++)
                {
                    _bags = _bags + int.Parse(gdstackdetail.Rows[i].Cells[5].Text.ToString());
                    _wt = _wt + decimal.Parse(gdstackdetail.Rows[i].Cells[6].Text.ToString());
                    ViewState["commid"] = gdstackdetail.Rows[i].Cells[2].Text.ToString();
                }
                ViewState["totalbags"] = _bags;
                ViewState["totalwt"] = _wt;

            }
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred , try again!" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
      
    }
    protected void ddlgodownlist_SelectedIndexChanged(object sender, EventArgs e)
    {

        gdstackdetail.DataSource = null;
        gdstackdetail.DataBind();
        fillddlstacklist();
        btnsave.Enabled = false;
        lblmsg.Text = "";
    }
    protected void ddlstacklist_SelectedIndexChanged(object sender, EventArgs e)
    {
        gdstackdetail.DataSource = null;
        gdstackdetail.DataBind();
        btnsave.Enabled = false;
        srchdatagrid();
        
       
    }
    protected void gdstackdetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;
    }
    

    private void Empty()
    {
        txtinsdate.Text = null;
        txtclear.Text = "0";
        txtfew.Text = "0";
        txtheavy.Text = "0";
        txtbordamage.Text = "0";
        txtgerdiscolored.Text = "0";
        txtcatA.Text = "0";
        txtcatB.Text = "0";
        txtcatC.Text = "0";
        txtcatD.Text = "0";
        txtmoisture.Text = null;
        txtinfestation.Text = null;
        txtrecdata.Text = null;
        txtremark.Text = null;
        txtsprayedtreatment.Text = "N";
        txtspraydate.Text = null;
        txtsprayedchemical.Text = "N";
        txtfungdate.Text = null;
        txtfumichemical.Text = "N";
        txtfumitreatment.Text = "N";
        txtmoisture.Text = "0";
        ddlgodownlist.SelectedIndex = -1;
        ddlstacklist.SelectedIndex = -1;
        txtfungdate.Text = txtspraydate.Text = txtinsdate.Text = DateTime.Today.ToString("dd/MM/yyyy");

        

    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    this.Validate("SaveValid");      
        //    if (Page.IsValid)
        //    {
        //            if (ddlstacklist.Items.Count == 0)
        //            {
        //                StringBuilder str = new StringBuilder();
        //                str.Append("<script>");
        //                str.Append("alert('" + "No stack under selected Godown" + "');</script>");
        //                this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        //                lblmsg.ForeColor = System.Drawing.Color.Red;
        //                lblmsg.Text = "No stack under selected Godown";
        //            }
        //            else
        //            {
        //                if (_sqlCon.State == ConnectionState.Closed)
        //                {
        //                    _sqlCon.Open();
        //                }
        //                SqlDataAdapter da = new SqlDataAdapter();
        //                SqlCommand _cmd = new SqlCommand("sp_stackquality_select", _sqlCon);
        //                _cmd.CommandType = CommandType.StoredProcedure;
        //                _cmd.Parameters.Add("@Godown_ID", SqlDbType.NVarChar, 20);
        //                _cmd.Parameters["@Godown_ID"].Value = ddlgodownlist.SelectedValue;
        //                _cmd.Parameters.Add("@Stack_ID", SqlDbType.NVarChar, 20);
        //                _cmd.Parameters["@Stack_ID"].Value = ddlstacklist.SelectedValue;
        //                da.SelectCommand = _cmd;
        //                da.Fill(ds, "temp");
        //                if (ds != null)
        //                {
        //                    if (ds.Tables[0].Rows.Count > 0)
        //                    {
        //                        gdstackdetail.DataSource = ds;
        //                        gdstackdetail.DataBind();
        //                    }
        //                }                    
        //                _cmd.Dispose();                      
        //                da.Dispose();
        //                ds.Clear();
        //                int _count = gdstackdetail.Rows.Count;
        //                if (_count > 0)
        //                {
        //                    btnsave.Enabled = true;
        //                }
        //                else
        //                {
        //                    btnsave.Enabled = false;
        //                    StringBuilder str = new StringBuilder();
        //                    str.Append("<script>");
        //                    str.Append("alert('" + "No Data under the current selected Stack Number" + "');</script>");
        //                    this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        //                    lblmsg.ForeColor = System.Drawing.Color.Red;
        //                    lblmsg.Text = "No Data under the current selected Stack Number";
        //                }
        //            }
        //    }
        //    else
        //    {
        //        Session["errDesc"] = "Invalid input data";
        //        Server.Transfer("../../CustomError.aspx");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    StringBuilder str = new StringBuilder();
        //    str.Append("<script>");
        //    str.Append("alert('" + "Some error has occurred" + "');</script>");
        //    this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        //    lblmsg.ForeColor = System.Drawing.Color.Red;
        //    lblmsg.Text = "Some error has occurred ";
        //}
    }

    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
        //if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        //{
        //    try
        //    {
        //        this.Validate("SaveValid");
        //            Double doubleparse;
        //            DateTime dateparse;

        //            if (DateTime.TryParse(getDate_MDY(txtinsdate.Text), out dateparse) == false)
        //            {
        //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid date;'); </script> ");
        //            }
        //            else if (Double.TryParse(txtclear.Text, out doubleparse) == false)
        //            {
        //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid operation;'); </script> ");
        //            }
        //            else if (Double.TryParse(txtfew.Text, out doubleparse) == false)
        //            {
        //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid operation;'); </script> ");
        //            }
        //            else if (Double.TryParse(txtheavy.Text, out doubleparse) == false)
        //            {
        //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid operation;'); </script> ");
        //            }
        //            else if (Double.TryParse(txtbordamage.Text, out doubleparse) == false)
        //            {
        //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid operation;'); </script> ");
        //            }
        //            else if (Double.TryParse(Txtchalky.Text, out doubleparse) == false)
        //            {
        //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid operation;'); </script> ");
        //            }
        //            else if (Double.TryParse(txtgerdiscolored.Text, out doubleparse) == false)
        //            {
        //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid operation;'); </script> ");
        //            }
        //            else if (Double.TryParse(txtcatA.Text, out doubleparse) == false)
        //            {
        //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid operation;'); </script> ");
        //            }
        //            else if (Double.TryParse(txtcatB.Text, out doubleparse) == false)
        //            {
        //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid operation;'); </script> ");
        //            }
        //            else if (Double.TryParse(txtcatC.Text, out doubleparse) == false)
        //            {
        //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid operation;'); </script> ");
        //            }
        //            else if (Double.TryParse(txtcatD.Text, out doubleparse) == false)
        //            {
        //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid operation;'); </script> ");
        //            }
        //            else if (Double.TryParse(txtmoisture.Text, out doubleparse) == false)
        //            {
        //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid operation;'); </script> ");
        //            }
        //            else if (Double.TryParse(txtsprayedtreatment.Text, out doubleparse) == false)
        //            {
        //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid operation;'); </script> ");
        //            }
        //            else if (Double.TryParse(txtfumitreatment.Text, out doubleparse) == false)
        //            {
        //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid operation;'); </script> ");
        //            }
        //            else if ((txtspraydate.Text != "") && (DateTime.TryParse(getDate_MDY(txtspraydate.Text), out dateparse) == false))
        //            {

        //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid operation;'); </script> ");

        //            }
        //            else if ((txtfungdate.Text != "") && (DateTime.TryParse(getDate_MDY(txtfungdate.Text), out dateparse) == false))
        //            {

        //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('invalid operation;'); </script> ");

        //            }
        //            else
        //            {
        //                decimal _totalclassificationwt;
        //                decimal _totalcategorywt;
        //                if (txtclear.Text == "")
        //                {
        //                    txtclear.Text = "0";
        //                }
        //                if (txtfew.Text == "")
        //                {
        //                    txtfew.Text = "0";
        //                }
        //                if (txtheavy.Text == "")
        //                {
        //                    txtheavy.Text = "0";
        //                }
        //                if (txtcatA.Text == "")
        //                {
        //                    txtcatA.Text = "0";
        //                }
        //                if (txtcatB.Text == "")
        //                {
        //                    txtcatB.Text = "0";
        //                }
        //                if (txtcatC.Text == "")
        //                {
        //                    txtcatC.Text = "0";
        //                }
        //                if (txtcatD.Text == "")
        //                {
        //                    txtcatD.Text = "0";
        //                }


        //                _totalclassificationwt = Convert.ToDecimal(txtclear.Text.ToString().Trim()) + Convert.ToDecimal(txtfew.Text.ToString().Trim()) + Convert.ToDecimal(txtheavy.Text.ToString().Trim());
        //                _totalcategorywt = Convert.ToDecimal(txtcatA.Text.ToString().Trim()) + Convert.ToDecimal(txtcatB.Text.ToString().Trim()) + Convert.ToDecimal(txtcatC.Text.ToString().Trim()) + Convert.ToDecimal(txtcatD.Text.ToString().Trim());
        //                if (decimal.Parse(ViewState["totalwt"].ToString()) < _totalclassificationwt)
        //                {
        //                    StringBuilder str = new StringBuilder();
        //                    str.Append("<script>");
        //                    str.Append("alert('" + "Entered weights in Classification are not correct " + "');</script>");
        //                    this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        //                    lblmsg.ForeColor = System.Drawing.Color.Red;
        //                    lblmsg.Text = "Entered weights in Classification are not correct ";
        //                }

        //                else if (decimal.Parse(ViewState["totalwt"].ToString()) < _totalcategorywt)
        //                {
        //                    StringBuilder str = new StringBuilder();
        //                    str.Append("<script>");
        //                    str.Append("alert('" + "Entered weights in Category are not correct " + "');</script>");
        //                    this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        //                    lblmsg.ForeColor = System.Drawing.Color.Red;
        //                    lblmsg.Text = "Entered weights in Category are not correct ";
        //                }
        //                else if (Session["RefreshButton"].ToString() != ViewState["RefreshButton"].ToString())
        //                {
        //                    Response.Redirect("QualityControl.aspx?PopMsg=" + "Record Already Saved! Do not Refresh again!!" + "");

        //                }
        //                else
        //                {



        //                    int _qualityid;
        //                    if (_sqlCon.State == ConnectionState.Closed)
        //                    {
        //                        _sqlCon.Open();
        //                    }
        //                    SqlCommand _cmd = new SqlCommand("sp_qualitycontrol_insert", _sqlCon);
        //                    _cmd.CommandType = CommandType.StoredProcedure;
        //                    _cmd.Parameters.Add("@Godown_ID", SqlDbType.NVarChar, 20);
        //                    _cmd.Parameters["@Godown_ID"].Value = ddlgodownlist.SelectedValue;
        //                    _cmd.Parameters.Add("@Stack_ID", SqlDbType.NVarChar, 20);
        //                    _cmd.Parameters["@Stack_ID"].Value = ddlstacklist.SelectedValue;
        //                    _cmd.Parameters.Add("@District_Id", SqlDbType.NVarChar, 20);
        //                    _cmd.Parameters["@District_Id"].Value = Session["Depot_DistID"].ToString();
        //                    _cmd.Parameters.Add("@Depo_Id", SqlDbType.NVarChar, 20);
        //                    _cmd.Parameters["@Depo_Id"].Value = Session["Depot_DepotID"].ToString();
        //                    _cmd.Parameters.Add("@Commodity_Id", SqlDbType.NVarChar, 20);

        //                    _cmd.Parameters["@Commodity_Id"].Value = ViewState["commid"].ToString();

        //                    _cmd.Parameters.Add("@Qty_No_of_Bags", SqlDbType.Int);
        //                    _cmd.Parameters["@Qty_No_of_Bags"].Value = int.Parse(ViewState["totalbags"].ToString());
        //                    _cmd.Parameters.Add("@Qty_Weight", SqlDbType.Decimal);
        //                    _cmd.Parameters["@Qty_Weight"].Value = decimal.Parse(ViewState["totalwt"].ToString());
        //                    _cmd.Parameters.Add("@Inspection_Date", SqlDbType.DateTime);
        //                    _cmd.Parameters["@Inspection_Date"].Value = getDate_MDY(txtinsdate.Text.ToString());
        //                    _cmd.Parameters.Add("@Weevilled_Damage_Per", SqlDbType.Int);
        //                    if (txtbordamage.Text != "")
        //                    {
        //                        _cmd.Parameters["@Weevilled_Damage_Per"].Value = txtbordamage.Text.Trim();
        //                    }
        //                    else
        //                    {
        //                        _cmd.Parameters["@Weevilled_Damage_Per"].Value = 0;
        //                    }
        //                    _cmd.Parameters.Add("@Germenated_Discoloured_Per", SqlDbType.Int);
        //                    if (txtgerdiscolored.Text != "")
        //                    {
        //                        _cmd.Parameters["@Germenated_Discoloured_Per"].Value = txtgerdiscolored.Text.Trim();
        //                    }
        //                    else
        //                    {
        //                        _cmd.Parameters["@Germenated_Discoloured_Per"].Value = 0;
        //                    }
        //                    _cmd.Parameters.Add("@Chalky_Per", SqlDbType.Int);
        //                    if (Txtchalky.Text != "")
        //                    {
        //                        _cmd.Parameters["@Chalky_Per"].Value = Txtchalky.Text.Trim();
        //                    }
        //                    else
        //                    {
        //                        _cmd.Parameters["@Chalky_Per"].Value = 0;
        //                    }
        //                    _cmd.Parameters.Add("@CatyA", SqlDbType.Decimal);
        //                    if (txtcatA.Text != "")
        //                    {
        //                        _cmd.Parameters["@CatyA"].Value = txtcatA.Text.Trim();
        //                    }
        //                    else
        //                    {
        //                        _cmd.Parameters["@CatyA"].Value = 0;
        //                    }

        //                    _cmd.Parameters.Add("@CatyB", SqlDbType.Decimal);
        //                    if (txtcatB.Text != "")
        //                    {
        //                        _cmd.Parameters["@CatyB"].Value = txtcatB.Text.Trim();
        //                    }
        //                    else
        //                    {
        //                        _cmd.Parameters["@CatyB"].Value = 0;
        //                    }

        //                    _cmd.Parameters.Add("@CatyC", SqlDbType.Decimal);
        //                    if (txtcatC.Text != "")
        //                    {
        //                        _cmd.Parameters["@CatyC"].Value = txtcatC.Text.Trim();
        //                    }
        //                    else
        //                    {
        //                        _cmd.Parameters["@CatyC"].Value = 0;
        //                    }

        //                    _cmd.Parameters.Add("@CatyD", SqlDbType.Decimal);
        //                    if (txtcatD.Text != "")
        //                    {
        //                        _cmd.Parameters["@CatyD"].Value = txtcatD.Text.Trim();
        //                    }
        //                    else
        //                    {
        //                        _cmd.Parameters["@CatyD"].Value = 0;
        //                    }

        //                    _cmd.Parameters.Add("@Per_of_Moisture", SqlDbType.Decimal);
        //                    if (txtmoisture.Text.Trim() == "")
        //                    {
        //                        _cmd.Parameters["@Per_of_Moisture"].Value = 0;
        //                    }
        //                    else
        //                    {

        //                        _cmd.Parameters["@Per_of_Moisture"].Value = txtmoisture.Text.Trim();
        //                    }
        //                    _cmd.Parameters.Add("@Intensity_of_Infestation", SqlDbType.NVarChar, 20);//50
        //                    _cmd.Parameters["@Intensity_of_Infestation"].Value = txtinfestation.Text.Trim();
        //                    _cmd.Parameters.Add("@Treatment_Recommended", SqlDbType.NVarChar, 50);
        //                    _cmd.Parameters["@Treatment_Recommended"].Value = txtrecdata.Text.Trim();
        //                    _cmd.Parameters.Add("@Type_of_Treatment_Sprayed", SqlDbType.NVarChar, 30);
        //                    if (txtsprayedtreatment.Text != "")
        //                    {
        //                        _cmd.Parameters["@Type_of_Treatment_Sprayed"].Value = txtsprayedtreatment.Text.Trim();
        //                    }
        //                    else
        //                    {
        //                        _cmd.Parameters["@Type_of_Treatment_Sprayed"].Value = 0;
        //                    }

        //                    _cmd.Parameters.Add("@Date_of_Treatment_Sprayed", SqlDbType.DateTime);
        //                    if (txtspraydate.Text == "")
        //                    {
        //                        _cmd.Parameters["@Date_of_Treatment_Sprayed"].Value = "01/01/1900";
        //                    }
        //                    else
        //                    {
        //                        _cmd.Parameters["@Date_of_Treatment_Sprayed"].Value = getDate_MDY(txtspraydate.Text.ToString());
        //                    }
        //                    _cmd.Parameters.Add("@Name_of_Chemical_Used_Sprayed", SqlDbType.NVarChar, 30);
        //                    if (txtsprayedchemical.Text != "")
        //                    {
        //                        _cmd.Parameters["@Name_of_Chemical_Used_Sprayed"].Value = txtsprayedchemical.Text.Trim();
        //                    }
        //                    else
        //                    {
        //                        _cmd.Parameters["@Name_of_Chemical_Used_Sprayed"].Value = 0;
        //                    }


        //                    _cmd.Parameters.Add("@Date_of_Treatment_Fumigated", SqlDbType.DateTime);
        //                    if (txtfungdate.Text.ToString() == "")
        //                    {
        //                        _cmd.Parameters["@Date_of_Treatment_Fumigated"].Value = "01/01/1900";
        //                    }
        //                    else
        //                    {
        //                        _cmd.Parameters["@Date_of_Treatment_Fumigated"].Value = getDate_MDY(txtfungdate.Text.ToString());

        //                    }
        //                    _cmd.Parameters.Add("@Type_of_Treatment_Fumigated", SqlDbType.NVarChar, 30);
        //                    if (txtsprayedtreatment.Text != "")
        //                    {
        //                        _cmd.Parameters["@Type_of_Treatment_Fumigated"].Value = txtfumitreatment.Text.Trim();
        //                    }
        //                    else
        //                    {
        //                        _cmd.Parameters["@Type_of_Treatment_Fumigated"].Value = 0;
        //                    }
        //                    _cmd.Parameters.Add("@Name_of_Chemical_Used_Fumigated", SqlDbType.NVarChar, 30);
        //                    if (txtsprayedchemical.Text != "")
        //                    {
        //                        _cmd.Parameters["@Name_of_Chemical_Used_Fumigated"].Value = txtfumichemical.Text.Trim();
        //                    }
        //                    else
        //                    {
        //                        _cmd.Parameters["@Name_of_Chemical_Used_Fumigated"].Value = 0;
        //                    }

        //                    _cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar, 50);
        //                    _cmd.Parameters["@Remarks"].Value = txtremark.Text.Trim();
        //                    _cmd.Parameters.Add("@Classification_Qty_Clear", SqlDbType.Decimal);
        //                    if (txtclear.Text != "")
        //                    {
        //                        _cmd.Parameters["@Classification_Qty_Clear"].Value = txtclear.Text.Trim();
        //                    }
        //                    else
        //                    {
        //                        _cmd.Parameters["@Classification_Qty_Clear"].Value = 0;
        //                    }

        //                    _cmd.Parameters.Add("@Classification_Qty_Few", SqlDbType.Decimal);
        //                    if (txtfew.Text != "")
        //                    {
        //                        _cmd.Parameters["@Classification_Qty_Few"].Value = txtfew.Text.Trim();
        //                    }
        //                    else
        //                    {
        //                        _cmd.Parameters["@Classification_Qty_Few"].Value = 0;
        //                    }

        //                    _cmd.Parameters.Add("@Classification_Qty_Heavy", SqlDbType.Decimal);
        //                    if (txtheavy.Text != "")
        //                    {
        //                        _cmd.Parameters["@Classification_Qty_Heavy"].Value = txtheavy.Text.Trim();
        //                    }
        //                    else
        //                    {
        //                        _cmd.Parameters["@Classification_Qty_Heavy"].Value = 0;
        //                    }


        //                    _cmd.ExecuteNonQuery();

        //                    _cmd.Dispose();

        //                    StringBuilder str = new StringBuilder();
        //                    str.Append("<script>");
        //                    str.Append("alert('" + "The Record is added successfully" + "');</script>");
        //                    this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        //                    lblmsg.ForeColor = System.Drawing.Color.Red;
        //                    lblmsg.Text = "The Record is added successfully ";
        //                    Empty();
        //                    gdstackdetail.DataSource = null;
        //                    gdstackdetail.DataBind();
        //                    btnsave.Enabled = false;
        //                    Session["RefreshButton"] = "Yes";
        //                    //_objHelp.JamForm(this.Controls);

        //                }
        //            }
                
              
        //    }
        //    catch (Exception ex)
        //    {
        //        StringBuilder str = new StringBuilder();
        //        str.Append("<script>");
        //        str.Append("alert('" + "Data could not be saved as some  error has occurred" + "');</script>");
        //        this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        //        lblmsg.ForeColor = System.Drawing.Color.Red;
        //        lblmsg.Text = "Data could not be saved as some error has occurred ";
        //    }
        //}
        //else {
        //    Response.Redirect("../../Logout.aspx");
        
        //}
    }
    protected void txtheavy_TextChanged(object sender, EventArgs e)
    {

    }
    protected void srchdatagrid()
    {
        try
        {
            this.Validate("SaveValid");
            if (Page.IsValid)
            {
                if (ddlstacklist.Items.Count == 0)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("<script>");
                    str.Append("alert('" + "No stack under selected Godown" + "');</script>");
                    this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "No stack under selected Godown";
                }
                else
                {
                    if (_sqlCon.State == ConnectionState.Closed)
                    {
                        _sqlCon.Open();
                    }
                    SqlDataAdapter da = new SqlDataAdapter();
                    SqlCommand _cmd = new SqlCommand("sp_stackquality_select", _sqlCon);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.Add("@Godown_ID", SqlDbType.NVarChar, 20);
                    _cmd.Parameters["@Godown_ID"].Value = ddlgodownlist.SelectedValue;
                    _cmd.Parameters.Add("@Stack_ID", SqlDbType.NVarChar, 20);
                    _cmd.Parameters["@Stack_ID"].Value = ddlstacklist.SelectedValue;
                    da.SelectCommand = _cmd;
                    da.Fill(ds, "temp");
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            gdstackdetail.DataSource = ds;
                            gdstackdetail.DataBind();
                        }
                    }
                    _cmd.Dispose();
                    da.Dispose();
                    ds.Clear();
                    int _count = gdstackdetail.Rows.Count;
                    if (_count > 0)
                    {
                        btnsave.Enabled = true;
                    }
                    else
                    {
                        btnsave.Enabled = false;
                        StringBuilder str = new StringBuilder();
                        str.Append("<script>");
                        str.Append("alert('" + "No Data under the current selected Stack Number" + "');</script>");
                        this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "No Data under the current selected Stack Number";
                    }
                }
            }
            else
            {
                Session["errDesc"] = "Invalid input data";
                Server.Transfer("../../CustomError.aspx");
            }
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Text = "Some error has occurred ";
        }
    }
    protected void Printcurrentdate()
    {
        try
        {
            if (_sqlCon.State == ConnectionState.Closed)
            {
                _sqlCon.Open();
            }
            SqlCommand _cmd1 = new SqlCommand();
            DataSet ds1 = new DataSet();
            SqlDataAdapter da1 = new SqlDataAdapter();
            _cmd1.Connection = _sqlCon;
            _cmd1.CommandText = "SELECT  convert(varchar(10),getdate(),103) as 'Date1'";
            _cmd1.CommandType = CommandType.Text;
            da1.SelectCommand = _cmd1;
            da1.Fill(ds1, "temp");

          
            txtinsdate.Text = ds1.Tables[0].Rows[0]["Date1"].ToString();
            txtspraydate.Text = ds1.Tables[0].Rows[0]["Date1"].ToString();
            txtfungdate.Text = ds1.Tables[0].Rows[0]["Date1"].ToString();
         
            _cmd1.Dispose();
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred , try again!" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
        finally
        {
            _sqlCon.Close();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["RefreshButton"] = "No";// Session["RefreshButton"];
    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {

        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                if (_sqlCon.State == ConnectionState.Closed)
                {
                    _sqlCon.Open();
                }
                SqlCommand _cmd = new SqlCommand();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                _cmd.Connection = _sqlCon;
                _cmd.CommandText = "SELECT Godown_ID FROM tbl_MetaData_GODOWN WHERE DepotId = '" + Session["Depot_DepotID"].ToString() + "' ";
                _cmd.CommandType = CommandType.Text;
                da.SelectCommand = _cmd;
                da.Fill(ds, "tbl_MetaData_GODOWN");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataView dv = ds.Tables[0].DefaultView;
                    string gd;
                    args.IsValid = false;    // Assume False
                    // Loop through table and compare each record against user's entry
                    foreach (DataRowView datarow in dv)
                    {
                        // Extract e-mail address from the current row
                        gd = datarow["Godown_ID"].ToString();
                        // Compare e-mail address against user's entry
                        if (gd == args.Value)
                        {
                            args.IsValid = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                StringBuilder str = new StringBuilder();
                str.Append("<script>");
                str.Append("alert('" + "Some error has occurred , try again!" + "');</script>");
                this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
            }
            finally
            {
                _sqlCon.Close();
            }
        }
        else 
        {

            Response.Redirect("../../Logout.aspx");
        
        }
    }
    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {

        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                if (_sqlCon.State == ConnectionState.Closed)
                {
                    _sqlCon.Open();
                }
                SqlCommand _cmd = new SqlCommand();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                _cmd.Connection = _sqlCon;
                _cmd.CommandText = "SELECT Stack_ID FROM tbl_MetaData_STACK WHERE DepotId = '" + Session["Depot_DepotID"].ToString() + "' and Godown_id = '" + ddlgodownlist.SelectedValue + "' ";
                _cmd.CommandType = CommandType.Text;
                da.SelectCommand = _cmd;
                da.Fill(ds, "tbl_MetaData_STACK");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataView dv = ds.Tables[0].DefaultView;
                    string stk;
                    args.IsValid = false;    // Assume False
                    // Loop through table and compare each record against user's entry
                    foreach (DataRowView datarow in dv)
                    {
                        // Extract e-mail address from the current row
                        stk = datarow["Stack_ID"].ToString();
                        // Compare e-mail address against user's entry
                        if (stk == args.Value)
                        {
                            args.IsValid = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                StringBuilder str = new StringBuilder();
                str.Append("<script>");
                str.Append("alert('" + "Some error has occurred , try again!" + "');</script>");
                this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
            }
            finally
            {
                _sqlCon.Close();
            }
        }
        else {

            Response.Redirect("../../Logout.aspx");
        
        }
    }

    protected string getDate_MDY(string inDate)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

    }


    protected void btnsave_Click(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                this.Validate("SaveValid");
                Double doubleparse;
                DateTime dateparse;

                if (DateTime.TryParse(getDate_MDY(txtinsdate.Text), out dateparse) == false)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Inspection date ;'); </script> ");
                }
                else if (Double.TryParse(txtclear.Text, out doubleparse) == false)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('txtClear error !;'); </script> ");
                }
                else if (Double.TryParse(txtfew.Text, out doubleparse) == false)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('txtfew error;'); </script> ");
                }
                else if (Double.TryParse(txtheavy.Text, out doubleparse) == false)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('txtheavy error;'); </script> ");
                }
                else if (Double.TryParse(txtbordamage.Text, out doubleparse) == false)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('txtbordamage error;'); </script> ");
                }
                else if (Double.TryParse(Txtchalky.Text, out doubleparse) == false)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Txtchalky error;'); </script> ");
                }
                else if (Double.TryParse(txtgerdiscolored.Text, out doubleparse) == false)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('txtgerdiscolored error;'); </script> ");
                }
                else if (Double.TryParse(txtcatA.Text, out doubleparse) == false)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('txtcatA error;'); </script> ");
                }
                else if (Double.TryParse(txtcatB.Text, out doubleparse) == false)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('txtcatB error;'); </script> ");
                }
                else if (Double.TryParse(txtcatC.Text, out doubleparse) == false)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('txtcatC error;'); </script> ");
                }
                else if (Double.TryParse(txtcatD.Text, out doubleparse) == false)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('txtcatD error;'); </script> ");
                }
                else if (Double.TryParse(txtmoisture.Text, out doubleparse) == false)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Inter Moister Content !;'); </script> ");
                }
                //else if (Double.TryParse(txtsprayedtreatment.Text, out doubleparse) == false)
                //{
                //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Type of Sprayed Trtm !;'); </script> ");
                //}
                //else if (Double.TryParse(txtfumitreatment.Text, out doubleparse) == false)
                //{
                //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Type of Fumi. Trtm !;'); </script> ");
                //}
                else if ((txtspraydate.Text != "") && (DateTime.TryParse(getDate_MDY(txtspraydate.Text), out dateparse) == false))
                {

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('txtspraydate error;'); </script> ");

                }
                else if ((txtfungdate.Text != "") && (DateTime.TryParse(getDate_MDY(txtfungdate.Text), out dateparse) == false))
                {

                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('txtfungdate error;'); </script> ");

                }
                else
                {
                    decimal _totalclassificationwt;
                    decimal _totalcategorywt;
                    if (txtclear.Text == "")
                    {
                        txtclear.Text = "0";
                    }
                    if (txtfew.Text == "")
                    {
                        txtfew.Text = "0";
                    }
                    if (txtheavy.Text == "")
                    {
                        txtheavy.Text = "0";
                    }
                    if (txtcatA.Text == "")
                    {
                        txtcatA.Text = "0";
                    }
                    if (txtcatB.Text == "")
                    {
                        txtcatB.Text = "0";
                    }
                    if (txtcatC.Text == "")
                    {
                        txtcatC.Text = "0";
                    }
                    if (txtcatD.Text == "")
                    {
                        txtcatD.Text = "0";
                    }


                    _totalclassificationwt = Convert.ToDecimal(txtclear.Text.ToString().Trim()) + Convert.ToDecimal(txtfew.Text.ToString().Trim()) + Convert.ToDecimal(txtheavy.Text.ToString().Trim());
                    _totalcategorywt = Convert.ToDecimal(txtcatA.Text.ToString().Trim()) + Convert.ToDecimal(txtcatB.Text.ToString().Trim()) + Convert.ToDecimal(txtcatC.Text.ToString().Trim()) + Convert.ToDecimal(txtcatD.Text.ToString().Trim());
                    if (decimal.Parse(ViewState["totalwt"].ToString()) < _totalclassificationwt)
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append("<script>");
                        str.Append("alert('" + "Entered weights in Classification are not correct " + "');</script>");
                        this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "Entered weights in Classification are not correct ";
                    }

                    else if (decimal.Parse(ViewState["totalwt"].ToString()) < _totalcategorywt)
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append("<script>");
                        str.Append("alert('" + "Entered weights in Category are not correct " + "');</script>");
                        this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "Entered weights in Category are not correct ";
                    }
                    else if (Session["RefreshButton"].ToString() != ViewState["RefreshButton"].ToString())
                    {
                        Response.Redirect("QualityControl.aspx?PopMsg=" + "Record Already Saved! Do not Refresh again!!" + "");

                    }
                    else
                    {



                        int _qualityid;
                        if (_sqlCon.State == ConnectionState.Closed)
                        {
                            _sqlCon.Open();
                        }
                        SqlCommand _cmd = new SqlCommand("sp_qualitycontrol_insert", _sqlCon);
                        _cmd.CommandType = CommandType.StoredProcedure;
                        _cmd.Parameters.Add("@Godown_ID", SqlDbType.NVarChar, 20);
                        _cmd.Parameters["@Godown_ID"].Value = ddlgodownlist.SelectedValue;
                        _cmd.Parameters.Add("@Stack_ID", SqlDbType.NVarChar, 20);
                        _cmd.Parameters["@Stack_ID"].Value = ddlstacklist.SelectedValue;
                        _cmd.Parameters.Add("@District_Id", SqlDbType.NVarChar, 20);
                        _cmd.Parameters["@District_Id"].Value = Session["Depot_DistID"].ToString();
                        _cmd.Parameters.Add("@Depo_Id", SqlDbType.NVarChar, 20);
                        _cmd.Parameters["@Depo_Id"].Value = Session["Depot_DepotID"].ToString();
                        _cmd.Parameters.Add("@Commodity_Id", SqlDbType.NVarChar, 20);

                        _cmd.Parameters["@Commodity_Id"].Value = ViewState["commid"].ToString();

                        _cmd.Parameters.Add("@Qty_No_of_Bags", SqlDbType.Int);
                        _cmd.Parameters["@Qty_No_of_Bags"].Value = int.Parse(ViewState["totalbags"].ToString());
                        _cmd.Parameters.Add("@Qty_Weight", SqlDbType.Decimal);
                        _cmd.Parameters["@Qty_Weight"].Value = decimal.Parse(ViewState["totalwt"].ToString());
                        _cmd.Parameters.Add("@Inspection_Date", SqlDbType.DateTime);
                        _cmd.Parameters["@Inspection_Date"].Value = getDate_MDY(txtinsdate.Text.ToString());
                        _cmd.Parameters.Add("@Weevilled_Damage_Per", SqlDbType.Int);
                        if (txtbordamage.Text != "")
                        {
                            _cmd.Parameters["@Weevilled_Damage_Per"].Value = txtbordamage.Text.Trim();
                        }
                        else
                        {
                            _cmd.Parameters["@Weevilled_Damage_Per"].Value = 0;
                        }
                        _cmd.Parameters.Add("@Germenated_Discoloured_Per", SqlDbType.Int);
                        if (txtgerdiscolored.Text != "")
                        {
                            _cmd.Parameters["@Germenated_Discoloured_Per"].Value = txtgerdiscolored.Text.Trim();
                        }
                        else
                        {
                            _cmd.Parameters["@Germenated_Discoloured_Per"].Value = 0;
                        }
                        _cmd.Parameters.Add("@Chalky_Per", SqlDbType.Int);
                        if (Txtchalky.Text != "")
                        {
                            _cmd.Parameters["@Chalky_Per"].Value = Txtchalky.Text.Trim();
                        }
                        else
                        {
                            _cmd.Parameters["@Chalky_Per"].Value = 0;
                        }
                        _cmd.Parameters.Add("@CatyA", SqlDbType.Decimal);
                        if (txtcatA.Text != "")
                        {
                            _cmd.Parameters["@CatyA"].Value = txtcatA.Text.Trim();
                        }
                        else
                        {
                            _cmd.Parameters["@CatyA"].Value = 0;
                        }

                        _cmd.Parameters.Add("@CatyB", SqlDbType.Decimal);
                        if (txtcatB.Text != "")
                        {
                            _cmd.Parameters["@CatyB"].Value = txtcatB.Text.Trim();
                        }
                        else
                        {
                            _cmd.Parameters["@CatyB"].Value = 0;
                        }

                        _cmd.Parameters.Add("@CatyC", SqlDbType.Decimal);
                        if (txtcatC.Text != "")
                        {
                            _cmd.Parameters["@CatyC"].Value = txtcatC.Text.Trim();
                        }
                        else
                        {
                            _cmd.Parameters["@CatyC"].Value = 0;
                        }

                        _cmd.Parameters.Add("@CatyD", SqlDbType.Decimal);
                        if (txtcatD.Text != "")
                        {
                            _cmd.Parameters["@CatyD"].Value = txtcatD.Text.Trim();
                        }
                        else
                        {
                            _cmd.Parameters["@CatyD"].Value = 0;
                        }

                        _cmd.Parameters.Add("@Per_of_Moisture", SqlDbType.Decimal);
                        if (txtmoisture.Text.Trim() == "")
                        {
                            _cmd.Parameters["@Per_of_Moisture"].Value = 0;
                        }
                        else
                        {

                            _cmd.Parameters["@Per_of_Moisture"].Value = txtmoisture.Text.Trim();
                        }
                        _cmd.Parameters.Add("@Intensity_of_Infestation", SqlDbType.NVarChar, 20);//50
                        _cmd.Parameters["@Intensity_of_Infestation"].Value = txtinfestation.Text.Trim();
                        _cmd.Parameters.Add("@Treatment_Recommended", SqlDbType.NVarChar, 50);
                        _cmd.Parameters["@Treatment_Recommended"].Value = txtrecdata.Text.Trim();
                        _cmd.Parameters.Add("@Type_of_Treatment_Sprayed", SqlDbType.NVarChar, 30);
                        if (txtsprayedtreatment.Text != "")
                        {
                            _cmd.Parameters["@Type_of_Treatment_Sprayed"].Value = txtsprayedtreatment.Text.Trim();
                        }
                        else
                        {
                            _cmd.Parameters["@Type_of_Treatment_Sprayed"].Value = "N";
                        }

                        _cmd.Parameters.Add("@Date_of_Treatment_Sprayed", SqlDbType.DateTime);
                        if (txtspraydate.Text == "")
                        {
                            _cmd.Parameters["@Date_of_Treatment_Sprayed"].Value = "01/01/1900";
                        }
                        else
                        {
                            _cmd.Parameters["@Date_of_Treatment_Sprayed"].Value = getDate_MDY(txtspraydate.Text.ToString());
                        }
                        _cmd.Parameters.Add("@Name_of_Chemical_Used_Sprayed", SqlDbType.NVarChar, 30);
                        if (txtsprayedchemical.Text != "")
                        {
                            _cmd.Parameters["@Name_of_Chemical_Used_Sprayed"].Value = txtsprayedchemical.Text.Trim();
                        }
                        else
                        {
                            _cmd.Parameters["@Name_of_Chemical_Used_Sprayed"].Value = "N";
                        }


                        _cmd.Parameters.Add("@Date_of_Treatment_Fumigated", SqlDbType.DateTime);
                        if (txtfungdate.Text.ToString() == "")
                        {
                            _cmd.Parameters["@Date_of_Treatment_Fumigated"].Value = "01/01/1900";
                        }
                        else
                        {
                            _cmd.Parameters["@Date_of_Treatment_Fumigated"].Value = getDate_MDY(txtfungdate.Text.ToString());

                        }
                        _cmd.Parameters.Add("@Type_of_Treatment_Fumigated", SqlDbType.NVarChar, 30);
                        if (txtsprayedtreatment.Text != "")
                        {
                            _cmd.Parameters["@Type_of_Treatment_Fumigated"].Value = txtfumitreatment.Text.Trim();
                        }
                        else
                        {
                            _cmd.Parameters["@Type_of_Treatment_Fumigated"].Value = "N";
                        }
                        _cmd.Parameters.Add("@Name_of_Chemical_Used_Fumigated", SqlDbType.NVarChar, 30);
                        if (txtsprayedchemical.Text != "")
                        {
                            _cmd.Parameters["@Name_of_Chemical_Used_Fumigated"].Value = txtfumichemical.Text.Trim();
                        }
                        else
                        {
                            _cmd.Parameters["@Name_of_Chemical_Used_Fumigated"].Value = "N";
                        }

                        _cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar, 50);
                        _cmd.Parameters["@Remarks"].Value = txtremark.Text.Trim();
                        _cmd.Parameters.Add("@Classification_Qty_Clear", SqlDbType.Decimal);
                        if (txtclear.Text != "")
                        {
                            _cmd.Parameters["@Classification_Qty_Clear"].Value = txtclear.Text.Trim();
                        }
                        else
                        {
                            _cmd.Parameters["@Classification_Qty_Clear"].Value = 0;
                        }

                        _cmd.Parameters.Add("@Classification_Qty_Few", SqlDbType.Decimal);
                        if (txtfew.Text != "")
                        {
                            _cmd.Parameters["@Classification_Qty_Few"].Value = txtfew.Text.Trim();
                        }

                        else
                        {
                            _cmd.Parameters["@Classification_Qty_Few"].Value = 0;
                        }

                        _cmd.Parameters.Add("@Classification_Qty_Heavy", SqlDbType.Decimal);
                        if (txtheavy.Text != "")
                        {
                            _cmd.Parameters["@Classification_Qty_Heavy"].Value = txtheavy.Text.Trim();
                        }
                        else
                        {
                            _cmd.Parameters["@Classification_Qty_Heavy"].Value = 0;
                        }

                      
                        _cmd.ExecuteNonQuery();
                        _cmd.Dispose();
                        //StringBuilder str1 = new StringBuilder();
                        //str1.Append("<script>");
                        //str1.Append("alert('" + "The Record is added successfully" + "');</script>");
                        //this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str1.ToString());
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('The Record is added successfully;'); </script> ");
                        lblmsg.Visible = true;
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "The Record is added successfully ";
                        Empty();
                        //Response.Redirect("QualityControl.aspx");
                        gdstackdetail.DataSource = null;
                        gdstackdetail.DataBind();
                        btnsave.Enabled = false;
                        Session["RefreshButton"] = "Yes";

                        
                        //_objHelp.JamForm(this.Controls);

                    }
                }

            }
            catch (Exception ex)
            {
                StringBuilder str = new StringBuilder();
                str.Append("<script>");
                str.Append("alert('" + "Data could not be saved as some  error has occurred" + "');</script>");
                this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Data could not be saved as some error has occurred ";
            }
        }
        else
        {
            Response.Redirect("../../Logout.aspx");

        }
    }
}
