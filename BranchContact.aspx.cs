using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WLCBusinessLayer;

public partial class BranchContact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillRegion();
        }
        
    }

    public void fillRegion()
    {
        ListItem item = new ListItem("Select", "0");

        ddlRO.Items.Clear();
        ddlRO.Items.Add(item);

        DataTable dt = new Common().GetRO();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ListItem();
                item.Value = dt.Rows[i]["Region_Id"].ToString();
                item.Text = dt.Rows[i]["region"].ToString();

                ddlRO.Items.Add(item);
            }
        }

    }

    public void fillDistrict(string regid)
    {
        ListItem item = new ListItem();

        ddlDst.Items.Clear();

        DataTable dt = new Common().GetDistrict(regid);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ListItem();
                item.Value = dt.Rows[i]["District_Id"].ToString();
                item.Text = dt.Rows[i]["District_Name"].ToString();

                ddlDst.Items.Add(item);
            }

        }

    }


    protected void ddlRO_SelectedIndexChanged(object sender, EventArgs e)
    {
        string regid = ddlRO.SelectedItem.Value.ToString();
        fillDistrict(regid);
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
       
       DataTable dt= new Common().GetBranch(ddlDst.SelectedValue);
       rptBranch.DataSource = dt;
       rptBranch.DataBind();
    }
}