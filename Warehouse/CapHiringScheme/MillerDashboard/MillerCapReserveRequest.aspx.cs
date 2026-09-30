using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Xml;
using System.Collections;
using System.Data.SqlClient;

public partial class JVSMiller_MillerCapRequest : System.Web.UI.Page
{
    DataTable dt;
    int rvalue;
    
    SqlParameter param;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["MillerRegId"] == null || Session["Id"] == null)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("../Login.aspx");
        }

        if (!IsPostBack)
        {
            string mreg = Session["MillerRegId"].ToString();

            DataTable dt = GetRegistration(mreg);
            if (dt.Rows.Count == 0)
            {
                Response.Redirect("Registration.aspx");

            }


            fillDistrict();
            

        }

    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranch();
    }


     //fill district
    public void fillDistrict()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetDistrict();
            if (dt.Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = dt;
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {

        }
    }

  
    //fill branch by district
    public void fillBranch()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetBranchByDistrict(ddlDistrict.SelectedValue.ToString());
           // if (dt.Rows.Count > 0)
           // {
                ddlBranch.Items.Clear();
                ddlBranch.DataSource = dt;
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchID";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
           // }
        }
        catch (Exception ex)
        {

        }
    }

    //fill grid
    public void fillGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetGodownDetails(ddlBranch.SelectedValue.ToString());

            gvGodownCap.DataSource = dt;
            gvGodownCap.DataBind();

            if (dt.Rows.Count > 0)
            {
                divCalculate.Visible = true;
            }
            else {

                divCalculate.Visible = false;

            }

            
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkbtnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
        txtTotalBookCap.Text = "0";
        txtTotalAmount.Text = "0";
    }


    protected void radListFullPartial_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((RadioButtonList)sender).NamingContainer);
        int index = row.RowIndex;
        RadioButtonList radListFullPartial = (RadioButtonList)gvGodownCap.Rows[index].FindControl("radListFullPartial");
        CheckBox chkGodown = (CheckBox)gvGodownCap.Rows[index].FindControl("chkGodown");

        TextBox txtStack = (TextBox)gvGodownCap.Rows[index].FindControl("txtStack");
        Label lblbookingAmount = (Label)gvGodownCap.Rows[index].FindControl("lblbookingAmount");
        Label lblbookingCap = (Label)gvGodownCap.Rows[index].FindControl("lblbookingCap");

        if (chkGodown.Checked)
        {

            Literal litAvailCap = (Literal)gvGodownCap.Rows[index].FindControl("litAvailCap");

            //TextBox txtbookingCap = (TextBox)gvGodownCap.Rows[index].FindControl("txtbookingCap");
            //Label lblbookingAmount = (Label)gvGodownCap.Rows[index].FindControl("lblbookingAmount");

            if (radListFullPartial.SelectedValue == "F")
            {
                int NoOfStack=GetNoOfStack(float.Parse(litAvailCap.Text.ToString()));
                txtStack.Text = NoOfStack.ToString();
                txtStack.ReadOnly = true;

                int TtlCap = GetTotalCap(NoOfStack);

                lblbookingCap.Text = TtlCap.ToString();

                float amt = TtlCap * 10;
                lblbookingAmount.Text = amt.ToString();


                txtTotalBookCap.Text = "0";
                txtTotalAmount.Text = "0";

            }

            else if (radListFullPartial.SelectedValue == "P")
            {

                txtStack.Text = "0";
                txtStack.ReadOnly = false;
                lblbookingAmount.Text = "0";
                lblbookingCap.Text = "0";

                txtTotalBookCap.Text = "0";
                txtTotalAmount.Text = "0";
            }
        }
        else
        {

            txtStack.Text = "0";
            txtStack.ReadOnly = false;
            lblbookingAmount.Text = "0";
            lblbookingCap.Text = "0";

            txtTotalBookCap.Text = "0";
            txtTotalAmount.Text = "0";


            radListFullPartial.ClearSelection();
            Response.Write("<script>alert('Please checked godown cap');</script>");

        }

       // string yourvalue = cb1.Text;
        //here you can find your control and get value(Id).

    }


    protected void txtbookingCap_OnTextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        int index = row.RowIndex;
        TextBox txtStack = (TextBox)gvGodownCap.Rows[index].FindControl("txtStack");
        Label lblbookingAmount = (Label)gvGodownCap.Rows[index].FindControl("lblbookingAmount");
        Label lblbookingCap = (Label)gvGodownCap.Rows[index].FindControl("lblbookingCap");


        Literal litAvailCap = (Literal)gvGodownCap.Rows[index].FindControl("litAvailCap");

        CheckBox chkGodown = (CheckBox)gvGodownCap.Rows[index].FindControl("chkGodown");

        RadioButtonList radListFullPartial = (RadioButtonList)gvGodownCap.Rows[index].FindControl("radListFullPartial");
      
        if (chkGodown.Checked)
        {
            if (radListFullPartial.SelectedIndex != -1)
            {
                if (txtStack.Text.ToString() != "")
                {
                    int NoOfStack = int.Parse(txtStack.Text.ToString());

                    int TtlCap = GetTotalCap(NoOfStack);

                    float availcap = float.Parse(litAvailCap.Text.ToString());

                    if (TtlCap <= availcap)
                    {
                        float amt = TtlCap * 10;
                        lblbookingAmount.Text = amt.ToString();
                        lblbookingCap.Text = TtlCap.ToString();

                    }
                    else
                    {

                        txtStack.Text = "0";
                        lblbookingAmount.Text = "0";
                        lblbookingCap.Text ="0";

                        txtTotalBookCap.Text = "0";
                        txtTotalAmount.Text = "0";

                        Response.Write("<script>alert('Number of Stack value more than Total Vacant Cap');</script>");
                    }
                }
                else
                {
                    txtStack.Text = "0";
                   lblbookingAmount.Text = "0";
                  lblbookingCap.Text ="0";

                  txtTotalBookCap.Text = "0";
                  txtTotalAmount.Text = "0";
                }
            }

            else
            {

                txtStack.Text = "0";
                lblbookingAmount.Text = "0";
                  lblbookingCap.Text ="0";

                  txtTotalBookCap.Text = "0";
                  txtTotalAmount.Text = "0";

                Response.Write("<script>alert('Please select Full/Partial');</script>");
            }
        }
        else
        {
              txtStack.Text = "0";
                lblbookingAmount.Text = "0";
                  lblbookingCap.Text ="0";

                  txtTotalBookCap.Text = "0";
                  txtTotalAmount.Text = "0";

            Response.Write("<script>alert('Please checked godown cap');</script>");
        }


    }


    
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        float total = 0;
        float totalamt = 0;

        foreach (GridViewRow row in gvGodownCap.Rows)
        {
            CheckBox chkGodown = (CheckBox)row.Cells[0].FindControl("chkGodown");
            if (chkGodown.Checked)
            {
                Label lblbookingCap = (Label)row.Cells[4].FindControl("lblbookingCap");
              
                float bookingcap = float.Parse(lblbookingCap.Text.ToString());

                total = total + bookingcap;



                Label lblbookingAmount = (Label)row.Cells[5].FindControl("lblbookingAmount");
                float bookingAmount = float.Parse(lblbookingAmount.Text.ToString());

                totalamt = totalamt + bookingAmount;


            }

            txtTotalBookCap.Text = total.ToString();
            txtTotalAmount.Text = totalamt.ToString();

        }

        btnSubmit.Visible = true;
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        float ttlcap = float.Parse(txtTotalBookCap.Text);
        if (ttlcap > 1000)
        {

            string mregid = Session["MillerRegId"].ToString();
            string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

            StringWriter sw = new StringWriter();
            XmlTextWriter xtw = new XmlTextWriter(sw);

            xtw.WriteStartElement("root");

            foreach (GridViewRow row in gvGodownCap.Rows)
            {
                CheckBox chkGodown = (CheckBox)row.Cells[0].FindControl("chkGodown");
                if (chkGodown.Checked)
                {
                    xtw.WriteStartElement("detail");

                    HiddenField hdnGodownId = (HiddenField)row.Cells[0].FindControl("hdnGodownId");
                    RadioButtonList radListFullPartial = (RadioButtonList)row.Cells[6].FindControl("radListFullPartial");

                    Label lblbookingCap = (Label)row.Cells[7].FindControl("lblbookingCap");
                    
                    Label lblbookingAmount = (Label)row.Cells[7].FindControl("lblbookingAmount");


                    xtw.WriteElementString("Registration_ID", mregid);
                    xtw.WriteElementString("GodownID", hdnGodownId.Value.ToString());
                    xtw.WriteElementString("BookGodownCapacity", lblbookingCap.Text);
                    xtw.WriteElementString("BookGodownAmount", lblbookingAmount.Text);
                    xtw.WriteElementString("BookCapacityType", radListFullPartial.SelectedValue);

                    xtw.WriteEndElement();

                }

            }
            xtw.WriteEndElement();
            xtw.Close();
            string strxml = sw.ToString();

            dt = SaveMillerCapBook(mregid, ddlDistrict.SelectedValue.ToString(), ddlBranch.SelectedValue.ToString(), Convert.ToDecimal(txtTotalBookCap.Text), Convert.ToDecimal(txtTotalAmount.Text), ClientIP, strxml);

            if (dt.Rows.Count > 0)
            {
                string bookid = dt.Rows[0]["BookId"].ToString();
                Response.Write("<script>alert('Book Successfully');</script>");
                Session["BookId"] = bookid;
                Response.Redirect("CapBookPrint.aspx");
            }

            else
            {

                Response.Write("<script>alert('Sorry Not Book');</script>");

            }

        }
        else
        {
            Response.Write("<script>alert('Reserve Total Capacity More than 1000 MT');</script>");
        }


    }

    public DataTable GetDistrict()
    {
        string qr = "select District_Id,District_Name from tbl_MetaData_DISTRICT where State_Id=23 order by District_Name";
        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }


    public DataTable GetBranchByDistrict(string dstid)
    {
        string qr = "SELECT mbi.BranchID,mbi.BranchName FROM MetaDataBranchWithIssueCenter as mbi WHERE mbi.BranchID in (select [Branch_ID] from tbl_Branch_Block_Mapping where [District_ID]='" + dstid.Trim() + "')";

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }

    public DataTable GetGodownDetails(string branchid)
    {
        //string qr = "select wcap.Godown_ID,wcap.Godown_Name,wcap.Godown_Capacity,wcap.Lenght,wcap.Width,wcap.Height,isNull(bcap.SumBookGodownCapacity,0) as Booked_Capacity,(wcap.Godown_Capacity-isNUll(bcap.SumBookGodownCapacity,0)) as Available_Capacity, wcap.DistrictId,wcap.BranchID from dbo.tbl_MetaData_Miller_Cap as wcap left join (select GodownID,sum(BookGodownCapacity) as SumBookGodownCapacity from dbo.MillerCapGodownBookDetails group by GodownID ) as bcap on wcap.Godown_ID=bcap.GodownID where wcap.BranchID='" + branchid + "'";

        string qr = "select wcap.Godown_ID,wcap.Godown_Name, wcap.BranchID, wcap.Godown_Capacity, wcap.Utilized_capacity, wcap.Vacant_capacity from dbo.tbl_MetaData_Miller_Cap_2020 as wcap where wcap.BranchID='" + branchid + "'";

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }

    //Book Total Cap & Godown
    public DataTable SaveMillerCapBook(string mregid, string distid, string branchid, decimal totcap, decimal totamt, string ip, string strxml)
    {
        ArrayList list = new ArrayList();

        param = new SqlParameter("@Registration_ID", mregid);
        list.Add(param);

        param = new SqlParameter("@District_Id", distid);
        list.Add(param);

        param = new SqlParameter("@BranchId", branchid);
        list.Add(param);

        param = new SqlParameter("@TotalCapacity", totcap);
        list.Add(param);

        param = new SqlParameter("@TotalAmount", totamt);
        list.Add(param);

        param = new SqlParameter("@IPAddress", ip);
        list.Add(param);

        param = new SqlParameter("@strXML", strxml);
        list.Add(param);

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", "[spMillerCapBookMaster_Insert]", list, true);
        }

        catch (Exception ex) { }
        finally { }
        return dt;

    }

    public DataTable GetRegistration(string mreg)
    {
        string qr = "select * from MillRegistrationMaster where Registration_ID='" + mreg + "'";

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }

    public int GetTotalCap(int noOfStack)
    {

        int TtlCap = 0;
        int OneStackMt = 150;

        TtlCap = OneStackMt * noOfStack;

        return TtlCap;

    }

    public int GetNoOfStack(float TotalCap)
    {

        int NoOfStack = 0;
        int OneStackMt = 150;

        NoOfStack = (int)TotalCap / OneStackMt;

        return NoOfStack;

    }

}