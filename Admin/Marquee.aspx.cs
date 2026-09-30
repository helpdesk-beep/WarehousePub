using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WLCBusinessLayer;
using System.Drawing;

public partial class Admin_Marquee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid();
        }

    }

    public void fillGrid() 
    {
        DataTable dt = new Admin().GetMarqueeList();
        if (dt.Rows.Count > 0)
        {
            gvMarqueeList.DataSource = dt;
            gvMarqueeList.DataBind();
        
        }
    
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int rvalue = 0;
        int id = Convert.ToInt32(hfId.Value.ToString());

        if (hfId.Value == "0")
        {
            rvalue = new Admin().SaveMarquee(txtTitle.Text, txtLink.Text);

            if (rvalue > 0)
            {
                lblMsg.Text = "Save Successfully";
                lblMsg.ForeColor = Color.Green;
                fillGrid();
            }

            else
            {
                lblMsg.Text = "Not Save";
                lblMsg.ForeColor = Color.Red;
            }
        }

        else {
            rvalue = new Admin().EditMarqueeById(id, txtTitle.Text, txtLink.Text);

            if (rvalue > 0)
            {
                lblMsg.Text = "Updated Successfully";
                lblMsg.ForeColor = Color.Green;

                hfId.Value = "0";
                btnSave.Text = "SAVE";
                btnSave.CssClass = "btn btn-info";
                fillGrid();
            }

            else
            {
                lblMsg.Text = "Not Update";
                lblMsg.ForeColor = Color.Red;
            }
        
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Marquee.aspx");
    }


    protected void gvMarqueeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRecord")
        {
           int id= int.Parse(e.CommandArgument.ToString());

           DataTable dt= new Admin().GetMarqueeById(id);
           if (dt.Rows.Count > 0)
           {
               hfId.Value = dt.Rows[0]["Id"].ToString();
               txtTitle.Text = dt.Rows[0]["Title"].ToString();
               txtLink.Text = dt.Rows[0]["Link"].ToString();

               btnSave.Text = "UPDATE";
               btnSave.CssClass = "btn btn-warning";
           }
        }


        if (e.CommandName == "DeleteRecord")
        {
            int id = int.Parse(e.CommandArgument.ToString());

            int rvalue=new Admin().DeleteMarqueeById(id);

            if (rvalue > 0)
            {
                lblMsg.Text = "Deleted Successfully";
                lblMsg.ForeColor = Color.Green;

                fillGrid();
            }

            else {
                lblMsg.Text = "Not Delete";
                lblMsg.ForeColor = Color.Red;
            }
        }
    }
    }
