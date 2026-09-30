using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WLCBusinessLayer;
using System.Drawing;


public partial class Admin_Tender : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillSection();
            fillGrid();
        }

    }

    public void fillSection()
    {
        ListItem item = new ListItem("Select", "0");

        ddlSection.Items.Clear();
        ddlSection.Items.Add(item);

        DataTable dt = new Admin().GetSection();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ListItem();
                item.Value = dt.Rows[i]["SectionId"].ToString();
                item.Text = dt.Rows[i]["Section"].ToString();

                ddlSection.Items.Add(item);
            }


        }

    }

    public void fillGrid()
    {
        DataTable dt = new DataTable();
        dt = new Admin().GetTenderList();
        gvTenderList.DataSource = dt;
        gvTenderList.DataBind();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int secid = Convert.ToInt32(ddlSection.SelectedValue.ToString());
        int id = Convert.ToInt32(hfId.Value.ToString());

        string filename;
        int rvalue = 0;


        //Save
        if (hfId.Value=="0")
        {
            if (upFile.HasFile)
            {
                filename = upFile.FileName.ToString();
                string extension = System.IO.Path.GetExtension(filename);

                if (extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".xls" || extension == ".xlsx" || extension == ".jpeg" || extension == ".jpg" || extension == ".txt" || extension == ".zip" || extension == ".rar")
                {
                    upFile.SaveAs(Server.MapPath("tender_file//" + filename));

                    rvalue = new Admin().SaveTender(secid, txtTitle.Text, filename, txtExpDate.Text);

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

                else
                {
                    lblMsg.Text = "Please Upload Only pdf,xls,xlsx,zip,rar,jpg,jpeg,txt,doc,docx File";
                    lblMsg.ForeColor = Color.Red;
                }
            }
            else
            {
                lblMsg.Text = "Please choose any file";
                lblMsg.ForeColor = Color.Red;
            }
        }


        //Update
        else
        {
            if (upFile.HasFile)
            {
                filename = upFile.FileName.ToString();
                string extension = System.IO.Path.GetExtension(filename);

                if (extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".xls" || extension == ".xlsx" || extension == ".jpeg" || extension == ".jpg" || extension == ".txt" || extension == ".zip" || extension == ".rar")
                {
                    upFile.SaveAs(Server.MapPath("tender_file//" + filename));

                    rvalue = new Admin().EditTenderById(id, secid, txtTitle.Text, filename, txtExpDate.Text);

                    if (rvalue > 0)
                    {
                        lblMsg.Text = "Update Successfully";
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

                else
                {
                    lblMsg.Text = "Please Upload Only pdf,xls,xlsx,zip,rar,jpg,jpeg,txt,doc,docx File";
                    lblMsg.ForeColor = Color.Red;
                }
            }

            else
            {
                filename = hfFileName.Value.ToString();
                rvalue = new Admin().EditTenderById(id, secid, txtTitle.Text, filename, txtExpDate.Text);

                if (rvalue > 0)
                {
                    lblMsg.Text = "Update Successfully";
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
















        //if (upFile.HasFile)
        //{

        //    filename = upFile.FileName.ToString();
        //    string extension = System.IO.Path.GetExtension(filename);
        //    if (extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".xls" || extension == ".xlsx" || extension == ".jpeg" || extension == ".jpg" || extension == ".txt" || extension == ".zip" || extension == ".rar")
        //    {
        //        upFile.SaveAs(Server.MapPath("tender_file//" + filename));

        //        if (btnSave.Text == "SAVE")
        //        {
        //            rvalue = new Admin().SaveTender(secid, txtTitle.Text, filename, txtExpDate.Text);

        //            if (rvalue > 0)
        //            {
        //                lblMsg.Text = "Save Successfully";
        //                lblMsg.ForeColor = Color.Green;
        //                fillGrid();

        //            }

        //            else
        //            {
        //                lblMsg.Text = "Not Save";
        //                lblMsg.ForeColor = Color.Red;
        //            }

        //        }

        //        else if (btnSave.Text == "UPDATE")
        //        {
        //            rvalue = new Admin().EditTenderById(id, secid, txtTitle.Text, filename, txtExpDate.Text);

        //            if (rvalue > 0)
        //            {
        //                lblMsg.Text = "Update Successfully";
        //                lblMsg.ForeColor = Color.Green;
        //                btnSave.Text = "SAVE";
        //                btnSave.CssClass = "btn btn-info";
        //                fillGrid();
        //            }

        //            else
        //            {
        //                lblMsg.Text = "Not Update";
        //                lblMsg.ForeColor = Color.Red;
        //            }

        //        }

        //    }

        //    else
        //    {
        //        lblMsg.Text = "Please Upload Only pdf,xls,xlsx,zip,rar,jpg,jpeg,txt,doc,docx File";
        //        lblMsg.ForeColor = Color.Red;
        //    }
        //}

        //else
        //{
        //    lblMsg.Text = "Please choose any file";
        //    lblMsg.ForeColor = Color.Red;
        //}


    }

    protected void gvTenderList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRecord")
        {
            int id = int.Parse(e.CommandArgument.ToString());

            DataTable dt = new Admin().GetTenderById(id);
            if (dt.Rows.Count > 0)
            {
                hfId.Value = dt.Rows[0]["Id"].ToString();
               // ddlSection.SelectedItem.Text = dt.Rows[0]["Section"].ToString();
                txtTitle.Text = dt.Rows[0]["Title"].ToString();
                txtExpDate.Text = Convert.ToDateTime(dt.Rows[0]["Expiry"]).ToString("dd/MM/yyyy");              
                hfFileName.Value = dt.Rows[0]["Filename"].ToString();

                btnSave.Text = "UPDATE";
                btnSave.CssClass = "btn btn-warning";
            }
        }


        if (e.CommandName == "DeleteRecord")
        {
            int id = int.Parse(e.CommandArgument.ToString());

            int rvalue = new Admin().DeleteTenderById(id);

            if (rvalue > 0)
            {
                lblMsg.Text = "Deleted Successfully";
                lblMsg.ForeColor = Color.Green;

                fillGrid();
            }

            else
            {
                lblMsg.Text = "Sorry Not Delete";
                lblMsg.ForeColor = Color.Red;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Tender.aspx");
    }
}