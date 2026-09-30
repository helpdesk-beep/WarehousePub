using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WLCBusinessLayer;
using System.Drawing;

public partial class Admin_Gallery : System.Web.UI.Page
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
        DataTable dt = new Admin().GetGalleryList();
        if (dt.Rows.Count > 0)
        {
            gvGalleryList.DataSource = dt;
            gvGalleryList.DataBind();
           
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string imagename;
        int rvalue = 0;
        int id = Convert.ToInt32(hfId.Value.ToString());

        //Save
        if (hfId.Value == "0")
        {
            if (upImageFile.HasFile)
            {
                imagename = upImageFile.FileName.ToString();
                string extension = System.IO.Path.GetExtension(imagename);
                if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".gif" )
                {
                    upImageFile.SaveAs(Server.MapPath("image_file//" + imagename));

                    rvalue = new Admin().SaveGallery(txtTitle.Text, txtDescription.Text, upImageFile.FileName);
                  
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
                    lblMsg.Text = "Please upload only PNG,JPG,JPEG,GIF extension file only";
                    lblMsg.ForeColor = Color.Red;
                }
            }

            else
            {
                lblMsg.Text = "Please choose any image file";
                lblMsg.ForeColor = Color.Red;
            }
        }

         //Update
        else
        {
            if (upImageFile.HasFile)
            {
                imagename = upImageFile.FileName.ToString();
                string extension = System.IO.Path.GetExtension(imagename);
                if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".gif")
                {
                    upImageFile.SaveAs(Server.MapPath("image_file//" + imagename));

                    rvalue = new Admin().EditGalleryById(id, txtTitle.Text, txtDescription.Text, upImageFile.FileName);

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
                    lblMsg.Text = "Please upload only PNG,JPG,JPEG,GIF extension file only";
                    lblMsg.ForeColor = Color.Red;
                }
            }

            else
            {
                imagename = hfImageName.Value.ToString();
                rvalue = new Admin().EditGalleryById(id, txtTitle.Text, txtDescription.Text, imagename);

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
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Gallery.aspx");
    }



    protected void gvGalleryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRecord")
        {
            int id = int.Parse(e.CommandArgument.ToString());

            DataTable dt = new Admin().GetGalleryById(id);
            if (dt.Rows.Count > 0)
            {
                hfId.Value = dt.Rows[0]["Id"].ToString();
                txtTitle.Text = dt.Rows[0]["Title"].ToString();
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                hfImageName.Value = dt.Rows[0]["ImageName"].ToString();
                
                btnSave.Text = "UPDATE";
                btnSave.CssClass = "btn btn-warning";
            }
        }


        if (e.CommandName == "DeleteRecord")
        {
            int id = int.Parse(e.CommandArgument.ToString());

            int rvalue = new Admin().DeleteGalleryById(id);

            if (rvalue > 0)
            {
                lblMsg.Text = "Deleted Successfully";
                lblMsg.ForeColor = Color.Green;

                fillGrid();
            }

            else
            {
                lblMsg.Text = "Not Delete";
                lblMsg.ForeColor = Color.Red;
            }
        }
    }

}