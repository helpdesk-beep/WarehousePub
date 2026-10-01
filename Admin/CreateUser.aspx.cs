using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WLCBusinessLayer;
using System.Drawing;
using System.Xml;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;


public partial class Admin_Agreement : System.Web.UI.Page
{
    Admin clsAdmin = new Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillRegion();
            FillGrid();
            fillRoll();
            if (Request.QueryString["ID"] != null)
            {
                fillRegion();
                filldetails();
            }
        }

    }

    public void fillRoll()
    {
        ListItem item = new ListItem("Select", "0");

        ddlrole.Items.Clear();
        ddlrole.Items.Add(item);

        DataTable dt = WebsiteLookups.GetRoles();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ListItem();
                item.Value = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[i]["ID"].ToString()) ? dt.Rows[i]["ID"].ToString() : "");
                item.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[i]["Roll"].ToString())? dt.Rows[i]["Roll"].ToString():"");

                ddlrole.Items.Add(item);
            }


        }

    }
    public void fillRegion()
    {
        ListItem item = new ListItem("Select", "0");

        ddlregion.Items.Clear();
        ddlregion.Items.Add(item);

        DataTable dt = WebsiteLookups.GetRegions();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ListItem();
                item.Value = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[i]["Region_Id"].ToString()) ? dt.Rows[i]["Region_Id"].ToString() : "");
                item.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[i]["region"].ToString()) ? dt.Rows[i]["region"].ToString() : "");

                ddlregion.Items.Add(item);
            }


        }

    }

    public void fillDistrict()
    {
        ListItem item = new ListItem("Select", "0");

        ddldistrict.Items.Clear();
        ddldistrict.Items.Add(item);

        DataTable dt = WebsiteLookups.GetDistricts(ddlregion.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ListItem();
                item.Value = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[i]["District_Id"].ToString()) ? dt.Rows[i]["District_Id"].ToString() : "");
                item.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[i]["District_Name"].ToString()) ? dt.Rows[i]["District_Name"].ToString() : "");

                ddldistrict.Items.Add(item);
            }


        }

    }
    public void fillBranch()
    {
        ListItem item = new ListItem("Select", "0");

        ddlbranch.Items.Clear();
        ddlbranch.Items.Add(item);

        DataTable dt = WebsiteLookups.GetBranches(ddldistrict.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ListItem();
                item.Value = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[i]["BranchId"].ToString()) ? dt.Rows[i]["BranchId"].ToString() : "");
                item.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[i]["DepotName"].ToString()) ? dt.Rows[i]["DepotName"].ToString() : "");

                ddlbranch.Items.Add(item);
            }


        }

    }
    public void fillGodown()
    {
        ListItem item = new ListItem("Select", "0");

        ddlgodown.Items.Clear();
        ddlgodown.Items.Add(item);

        DataTable dt = WebsiteLookups.GetGodowns(ddlbranch.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ListItem();
                item.Value = dt.Rows[i]["Godown_ID"].ToString();
                item.Text = dt.Rows[i]["Godown_Name"].ToString();

                ddlgodown.Items.Add(item);
            }
        }

    }
    public void FillGrid()
    {
        DataTable dtdetails = new DataTable();
        dtdetails = clsAdmin.GetCreteUsers();
        if (dtdetails.Rows.Count > 0)
        {
            grdalreadyattended.DataSource = dtdetails;
            grdalreadyattended.DataBind();
        }
        else
        {
            grdalreadyattended.DataSource = null;
            grdalreadyattended.DataBind();
        }
    }
    void filldetails()
    {
        DataTable dtdetails = new DataTable();
        dtdetails = clsAdmin.GetCreateUserbyid(Convert.ToInt32(Request.QueryString["ID"]));
        if (dtdetails.Rows.Count > 0)
        {
            txtusername.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dtdetails.Rows[0]["Username"].ToString())? dtdetails.Rows[0]["Username"].ToString():"");
            fillRoll();
            ddlrole.SelectedValue = dtdetails.Rows[0]["Role"].ToString();
            if (ddlrole.SelectedValue.ToString() == "2")
            {
                fillRegion();
                divregion.Visible = true;
                divdistrict.Visible = false;
                divbranch.Visible = false;
                divgodown.Visible = false;
            }
            else if (ddlrole.SelectedValue.ToString() == "3")
            {
                divregion.Visible = true;
                divdistrict.Visible = true;
                divbranch.Visible = false;
                divgodown.Visible = false;
            }
            else if (ddlrole.SelectedValue.ToString() == "4")
            {
                divregion.Visible = true;
                divdistrict.Visible = true;
                divbranch.Visible = true;
                divgodown.Visible = false;
            }
            else if (ddlrole.SelectedValue.ToString() == "5")
            {
                divregion.Visible = true;
                divdistrict.Visible = true;
                divbranch.Visible = true;
                divgodown.Visible = true;
            }
            else
            {
                divregion.Visible = false;
                divdistrict.Visible = false;
                divbranch.Visible = false;
                divgodown.Visible = false;
            }
            fillRegion();
            //ddlregion.SelectedValue = HttpUtility.HtmlEncode(dtdetails.Rows[0]["Region_ID"].ToString());
            ddlregion.SelectedValue = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dtdetails.Rows[0]["Region_ID"].ToString()) ? dtdetails.Rows[0]["Region_ID"].ToString() : "");
            fillDistrict();
            ddldistrict.SelectedValue = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dtdetails.Rows[0]["District_ID"].ToString()) ? dtdetails.Rows[0]["District_ID"].ToString() : "");
            fillBranch();
            ddlbranch.SelectedValue = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dtdetails.Rows[0]["Branch_ID"].ToString()) ? dtdetails.Rows[0]["Branch_ID"].ToString() : "");
            txtemail.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dtdetails.Rows[0]["Emp_ID"].ToString()) ? dtdetails.Rows[0]["Emp_ID"].ToString() : "");
            txtmobileno.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dtdetails.Rows[0]["Mobile_No"].ToString()) ? dtdetails.Rows[0]["Mobile_No"].ToString() : "");
            txtname.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dtdetails.Rows[0]["Name"].ToString()) ? dtdetails.Rows[0]["Name"].ToString() : "");
            txtpassword.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dtdetails.Rows[0]["Password"].ToString()) ? dtdetails.Rows[0]["Password"].ToString() : "");
            btnSave.Text = "Update";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {

            if (btnSave.Text != "Update")
            {
                //clsguarddata.Insert_GuardData(txtdate.Text.Trim(), ddlcategory.SelectedValue, ddlservicepost.SelectedValue, txtname.Text.Trim(), txtfathername.Text.Trim(), txttelno.Text.Trim(), txtmobileno.Text.Trim(), txtaddress.Text.Trim(), ddlReligion.SelectedValue, txtexpectedsalary.Text.Trim(), cbexp.Checked == true ? "1" : "0", txtyear.Text.Trim(), txtcompany.Text.Trim(), clsCredentials.GetUserNo, ddldistrict.SelectedValue, txtreference.Text.Trim(), txtremark.Text.Trim(), txtrefcontactno.Text.Trim(), ddlcall.SelectedValue, txtcalldate.Text.Trim(), txtcalldetail.Text.Trim());
                Response.Write(getdata("Insert"));
                DataTable Dt = clsAdmin.CreateNewUser(getdata("Insert"));

                if (Dt != null && Dt.Rows.Count > 0 && Convert.ToInt32("0" + Dt.Rows[0][0]) > 0)
                {
                    // fns.updateLOG("Guard Data has been created !!", "", txtdate.Text.Trim(), clsCredentials.GetUserNo());
                    lblmsg.Text = "Creat User Sucessfully";
                    FillGrid();
                    //  FillData();
                    //clear();
                }
                else
                {
                    lblmsg.Text = "User Not Created";
                }

            }
            else
            {

                //clsguarddata.Insert_GuardData(txtdate.Text.Trim(), ddlcategory.SelectedValue, ddlservicepost.SelectedValue, txtname.Text.Trim(), txtfathername.Text.Trim(), txttelno.Text.Trim(), txtmobileno.Text.Trim(), txtaddress.Text.Trim(), ddlReligion.SelectedValue, txtexpectedsalary.Text.Trim(), cbexp.Checked == true ? "1" : "0", txtyear.Text.Trim(), txtcompany.Text.Trim(), clsCredentials.GetUserNo, ddldistrict.SelectedValue, txtreference.Text.Trim(), txtremark.Text.Trim(), txtrefcontactno.Text.Trim(), ddlcall.SelectedValue, txtcalldate.Text.Trim(), txtcalldetail.Text.Trim());
                Response.Write(getdata("Edit"));
                DataTable Dt = clsAdmin.CreateNewUser(getdata("Edit"));

                if (Dt != null && Dt.Rows.Count > 0 && Convert.ToInt32("0" + Dt.Rows[0][0]) > 0)
                {
                    lblmsg.Text = "User Details Update Sucessfully";
                    FillGrid();
                    //  FillData();
                    //clear();
                }
                else
                {
                    lblmsg.Text = "User Details Not Update";
                }

            }


        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
        }

    }

    public string getdata(string forwhat)
    {
        string newpassword = txtpassword.Text;
        byte[] newpasswordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(newpassword);
        string newsaltpwd = Convert.ToBase64String(newpasswordAndSaltBytes);
        byte[] passwor = System.Text.Encoding.UTF8.GetBytes(newpassword + newsaltpwd);
        byte[] newhashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwor);
        string newhashString = Convert.ToBase64String(newhashBytes);
        string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        XmlWriterSettings wsetting = new XmlWriterSettings();
        wsetting.NewLineOnAttributes = true;
        wsetting.Indent = true;
        wsetting.OmitXmlDeclaration = true;
        wsetting.CloseOutput = false;
        wsetting.Encoding = Encoding.UTF8;
        StringBuilder str = new StringBuilder();
        XmlWriter xw = XmlWriter.Create(str, wsetting);
        xw.WriteStartDocument();
        xw.WriteStartElement("ROOT");
        xw.WriteStartElement("ROWS");
        xw.WriteAttributeString("Username", txtusername.Text);
        xw.WriteAttributeString("Password", txtpassword.Text);
        xw.WriteAttributeString("salt", newsaltpwd);
        xw.WriteAttributeString("hashedPassword", newhashString);
        xw.WriteAttributeString("Role", ddlrole.SelectedValue);
        xw.WriteAttributeString("Emp_ID", txtemail.Text);
        xw.WriteAttributeString("Region_ID", ddlregion.SelectedValue);
        xw.WriteAttributeString("District_ID", ddldistrict.SelectedValue);
        xw.WriteAttributeString("Branch_ID", ddlbranch.SelectedValue);
        xw.WriteAttributeString("Name", txtname.Text);
        xw.WriteAttributeString("Mobile_No", txtmobileno.Text);
        xw.WriteAttributeString("Created_By", localIP.ToString());

        if (forwhat.Equals("Edit"))
            xw.WriteAttributeString("ID", Request.QueryString["ID"].ToString());
        xw.WriteEndElement();
        xw.WriteEndElement();
        xw.WriteEndDocument();
        xw.Flush();
        xw.Close();
        return str.ToString();

    }

    protected void gvAgreementList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRecord")
        {
            int id = int.Parse(e.CommandArgument.ToString());

            DataTable dt = new Admin().GetAgreementById(id);
            if (dt.Rows.Count > 0)
            {
            }
        }


        if (e.CommandName == "DeleteRecord")
        {
            int id = int.Parse(e.CommandArgument.ToString());

            int rvalue = new Admin().DeleteAgreementById(id);

            if (rvalue > 0)
            {
                //lblMsg.Text = "Deleted Successfully";
                //lblMsg.ForeColor = Color.Green;

                //fillGrid();
            }

            else
            {
                //lblMsg.Text = "Sorry Not Delete";
                //lblMsg.ForeColor = Color.Red;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateUser.aspx");
    }

    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDistrict();
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranch();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodown();
    }

    protected void ddlrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrole.SelectedValue.ToString() == "2")
        {
            fillRegion();
            divregion.Visible = true;
            divdistrict.Visible = false;
            divbranch.Visible = false;
            divgodown.Visible = false;
        }
        else if (ddlrole.SelectedValue.ToString() == "3")
        {
            divregion.Visible = true;
            divdistrict.Visible = true;
            divbranch.Visible = false;
            divgodown.Visible = false;
        }
        else if (ddlrole.SelectedValue.ToString() == "4")
        {
            divregion.Visible = true;
            divdistrict.Visible = true;
            divbranch.Visible = true;
            divgodown.Visible = false;
        }
        else if (ddlrole.SelectedValue.ToString() == "5")
        {
            divregion.Visible = true;
            divdistrict.Visible = true;
            divbranch.Visible = true;
            divgodown.Visible = true;
        }
        else
        {
            divregion.Visible = false;
            divdistrict.Visible = false;
            divbranch.Visible = false;
            divgodown.Visible = false;
        }
    }
}