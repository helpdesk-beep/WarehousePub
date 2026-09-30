using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WLCBusinessLayer;
using System.Drawing;

public partial class Admin_Appointment : System.Web.UI.Page
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
        DataTable dt = new DataTable();
        dt = new Admin().GetAppointmentList();
        gvAppointmentList.DataSource = dt;
        gvAppointmentList.DataBind();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(hfId.Value.ToString());

        string filename;
        int rvalue = 0;


        //Save
        if (hfId.Value == "0")
        {
            if (upFile.HasFile)
            {
                filename = upFile.FileName.ToString();
                string extension = System.IO.Path.GetExtension(filename);

                if (extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".xls" || extension == ".xlsx" || extension == ".jpeg" || extension == ".jpg" || extension == ".txt" || extension == ".zip" || extension == ".rar")
                {
                    upFile.SaveAs(Server.MapPath("appointment_file//" + filename));

                    rvalue = new Admin().SaveAppointment(txtTitle.Text, filename, txtExpDate.Text);

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
                    upFile.SaveAs(Server.MapPath("appointment_file//" + filename));

                    rvalue = new Admin().EditAppointmentById(id, txtTitle.Text, filename, txtExpDate.Text);

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
                rvalue = new Admin().EditAppointmentById(id, txtTitle.Text, filename, txtExpDate.Text);

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

    protected void gvAppointmentList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRecord")
        {
            int id = int.Parse(e.CommandArgument.ToString());

            DataTable dt = new Admin().GetAppointmentById(id);
            if (dt.Rows.Count > 0)
            {
                hfId.Value = dt.Rows[0]["Id"].ToString();
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

            int rvalue = new Admin().DeleteAppointmentById(id);

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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Appointment.aspx");
    }
}