using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WLCBusinessLayer;

using System.Drawing;

public partial class Admin_News : System.Web.UI.Page
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
        DataTable dt = new Admin().GetNewsList();
        if (dt.Rows.Count > 0)
        {
            gvNewsList.DataSource = dt;
            gvNewsList.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string filename;
        int rvalue = 0;
       int id= Convert.ToInt32(hfId.Value.ToString());
        
        //Save
       if (hfId.Value == "0")
       {
           if (upFile.HasFile)
           {
               filename = upFile.FileName.ToString();
               string extension = System.IO.Path.GetExtension(filename);
               if (extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".xls" || extension == ".xlsx")
               {
                   upFile.SaveAs(Server.MapPath("news_file//" + filename));

                   rvalue = new Admin().SaveNews(txtTitle.Text, txtDescription.Text, upFile.FileName);

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
                   lblMsg.Text = "Please upload only PDF,DOC,Xls extension file only";
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
               if (extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".xls" || extension == ".xlsx")
               {
                   upFile.SaveAs(Server.MapPath("news_file//" + filename));

                   rvalue = new Admin().EditNewsById(id, txtTitle.Text, txtDescription.Text, upFile.FileName);

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

               else {
                   lblMsg.Text = "Please upload only PDF,DOC,Xls extension file only";
                   lblMsg.ForeColor = Color.Red;
               }
           }

           else
           {
               filename=hfFileName.Value.ToString();
               rvalue = new Admin().EditNewsById(id, txtTitle.Text, txtDescription.Text, filename);

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
        Response.Redirect("News.aspx");
    }

    

    protected void gvNewsList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRecord")
        {
           int id= int.Parse(e.CommandArgument.ToString());

           DataTable dt= new Admin().GetNewsById(id);
           if (dt.Rows.Count > 0)
           {
               hfId.Value = dt.Rows[0]["Id"].ToString();
               txtTitle.Text = dt.Rows[0]["Title"].ToString();
               txtDescription.Text = dt.Rows[0]["Description"].ToString();
               hfFileName.Value= dt.Rows[0]["Filename"].ToString();

               btnSave.Text = "UPDATE";
               btnSave.CssClass = "btn btn-warning";
           }
        }


        if (e.CommandName == "DeleteRecord")
        {
            int id = int.Parse(e.CommandArgument.ToString());

            int rvalue=new Admin().DeleteNewsById(id);

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
