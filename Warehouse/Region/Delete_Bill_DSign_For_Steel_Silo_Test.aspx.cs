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
using MPSCSC_WS;

public partial class Region_Delete_Bill_DSign_For_Steel_Silo : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    MPSCSC_WS.MPSCSC_InstituitionStorageBillDetails MPSCSCDemo = new MPSCSC_WS.MPSCSC_InstituitionStorageBillDetails();

    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    string Bill_Type = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            //string region = Session["Region_ID"].ToString();
            if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        //string strMsg = "यहाँ सुविधा कुछ दिनों के लिए सॉफ्टवेयर में कार्य होने कारण बंद कर दी गई हैं |||";


                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Welcome.aspx';", true);
                    }
                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[Get_Steel_Silo_Bill_Details_Delete]", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BillNumber", txtbillnumber.Text);
                cmd.Parameters.AddWithValue("@TypeID", ddlBillType.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            gv.DataSource = dt;
                            gv.DataBind();
                            lblRowCount.Text = "";
                            lblRowCount.Text = "Total No. Of records are : " + dt.Rows.Count.ToString();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Bill Data Found...')", true);
                            lblRowCount.Text = "Total No. Of records are : " + dt.Rows.Count.ToString();
                            gv.DataSource = null;
                            gv.DataBind();
                        }
                    }
                }
            }
        }
    }
    //public void FillGrid()
    //{
    //    //string query = "";
    //    //if (RadioButton1.Checked == true)
    //    //{
    //    //    Bill_Type = "SCB";
    //    //}
    //    //else if (RadioButton2.Checked == true)
    //    //{
    //    //    Bill_Type = "GRB";
    //    //}

    //    //if (Bill_Type == "SCB")
    //    //{
    //    //    //query = "select SB.Bill_Number,case when SB.Bill_Type='AD' then 'Daily Storage Charges Bill' when SB.Bill_Type='AU' then 'Accrued Storage Charges Bill' when SB.Bill_Type='OD' then 'Daily Over & Above Storage Charges Bill' when SB.Bill_Type='HG' then 'Godown(Hired) Rent Bill' when SB.Bill_Type='GR' then 'Godown(JVS) Rent Bill' when SB.Bill_Type='OU' then 'Accrued Over & Above Storage Charges Bill' when SB.Bill_Type='RB' then 'Reservation Bill' else '' end as Bill_Name,Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount from tbl_Institution_Storage_Bill_Details as SB where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and SB.Bill_Number not in (select DS.Ref_Bill_No from tbl_Digitally_Signed_Bill_Details as DS where DS.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and DS.Ref_Bill_No is not null)";
    //    //    query = "select SB.Bill_Number,'Storage Charges Bill' as Bill_Name,SB.Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount,DSB.DSC_Holder_Name,DSB.DSC_Serial_No from tbl_Institution_Storage_Bill_Details as SB inner join tbl_Digitally_Signed_Bill_Details as DSB on DSB.Ref_Bill_No=SB.Bill_Number where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and SB.Bill_Number in (select DS.Ref_Bill_No from tbl_Digitally_Signed_Bill_Details as DS where DS.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and DS.Ref_Bill_No is not null) and SB.Bill_Number not in (select RM.Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM as RM where RM.BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and RM.Ref_Bill_Number is not null) and SB.Bill_Number not in (select CDS.Ref_Bill_No from MPSCSC.dbo.Digitally_Sign_StorageBill_IC as CDS where Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and CDS.Ref_Bill_No is not null) and SB.Bill_Number='" + txtSearchWHR.Text + "'";
    //    //}
    //    //else if (Bill_Type == "GRB")
    //    //{
    //    //    //query = "select SB.Bill_Number,case when SB.Bill_Type='AD' then 'Daily Storage Charges Bill' when SB.Bill_Type='AU' then 'Accrued Storage Charges Bill' when SB.Bill_Type='OD' then 'Daily Over & Above Storage Charges Bill' when SB.Bill_Type='HG' then 'Godown(Hired) Rent Bill' when SB.Bill_Type='GR' then 'Godown(JVS) Rent Bill' when SB.Bill_Type='OU' then 'Accrued Over & Above Storage Charges Bill' when SB.Bill_Type='RB' then 'Reservation Bill' else '' end as Bill_Name,Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount from tbl_Institution_Storage_Bill_Details as SB where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and SB.Bill_Number not in (select DS.Ref_Bill_No from tbl_Digitally_Signed_Bill_Details as DS where DS.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and DS.Ref_Bill_No is not null)";
    //    //    query = "select SB.Bill_Number,'Godown Rent Bill' as Bill_Name,Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount from tbl_Institution_Storage_Bill_Details as SB where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and SB.Bill_Number in (select distinct DS.Bill_Number from tbl_Digitally_Signed_Bill_PVT as DS where DS.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and DS.Bill_Number is not null) and SB.Bill_Number not in (select RM.Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM as RM where RM.BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and RM.Ref_Bill_Number is not null) and SB.Bill_Number='" + txtSearchWHR.Text + "'";

    //    //}

    //    cmd = new SqlCommand("Get_Steel_Silo_Bill_Details_Delete", con);
    //    da = new SqlDataAdapter(cmd);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    Session["ds_GridInfo"] = ds;
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        gv.DataSource = ds;
    //        gv.DataBind();
    //        lblRowCount.Text = "";
    //        lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
    //        Btn_Delete.Enabled = true;
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Bill Data Found...')", true);
    //        lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
    //        gv.DataSource = null;
    //        gv.DataBind();
    //    }
    //}
    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    
    //protected void btn_Close_Click(object sender, EventArgs e)
    //{
    //    //Response.Redirect("~/Branch_Welcome.aspx");
    //}
    protected void btnSerachWHR_Click(object sender, EventArgs e)
    {
        if (txtbillnumber.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Bill No')", true);
        }
        else
        {
            fillgrid();
        }
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = gv.Rows[rowIndex];

            //Fetch value of Name.
            string hdnbillnumber = (row.FindControl("hdnbillnumber") as HiddenField).Value;
            string hdnbilltype = (row.FindControl("hdnbilltype") as HiddenField).Value;
            Session["hdnbillnumber"] = hdnbillnumber.ToString();
            Session["hdnbilltype"] = hdnbilltype.ToString();
            RemoveRow(hdnbillnumber);
            //RemoveRow(hdnbillnumber, hdnbilltype);
            // RemoveRowJVS(hdnId);

        }
    }
    public void RemoveRow(string billno)
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

            SqlCommand cmd = new SqlCommand("Delete_Steel_Silo_Bill", con_WLC);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BillNumber", billno.ToString());
            cmd.Parameters.AddWithValue("@DeletedBy", localIP.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                fillgrid();
                //Web Service Call For data to MPSCSC
                System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
                MPSCSCDemo.EDDeleteFinalBillSteelSilo(billno.ToString(), localIP.ToString());
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Remove Row Successfully||| !');", true);
            }



        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }
}