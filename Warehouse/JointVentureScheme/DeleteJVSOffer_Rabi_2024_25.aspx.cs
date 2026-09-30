using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;

public partial class JointVentureScheme_DeleteJVSOffer_Rabi_2024_25 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlTransaction sqltran;
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = null;
    string Branch = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        string SessBranch = Session["UserName"].ToString();
        string SessBranchID = Session["UserId"].ToString();
        if (SessBranch != "" && SessBranchID != "")
        {
            if (!IsPostBack)
            {
                lbluser.Text = SessBranch;
                //fillgrid();
            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        Branch = Session["UserId"].ToString();
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Offered_Godown_Rabi_2024_25", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Registration_Id", txtRegID.Text);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            RegGrid.DataSource = null;
                            RegGrid.DataBind();
                            RegGrid.DataSource = dt;
                            RegGrid.DataBind();
                            Label2.Visible = true;
                            Label3.Visible = true;
                            Label4.Visible = true;
                            Label5.Visible = true;
                            Button1.Visible = true;
                            Label3.Text = Convert.ToString(dt.Rows.Count);
                            decimal sum = 0;
                            for (int i = 0; i < RegGrid.Rows.Count; i++)
                            {
                                sum += Convert.ToDecimal(RegGrid.Rows[i].Cells[6].Text.ToString());
                            }
                            Label5.Text = Convert.ToString(sum);
                            decimal RegCsum = 0;
                            for (int i = 0; i < RegGrid.Rows.Count; i++)
                            {
                                RegCsum += Convert.ToDecimal(RegGrid.Rows[i].Cells[6].Text.ToString());
                            }
                            Label5.Text = Convert.ToString(sum);
                            decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Offer_Capacity"));
                            RegGrid.FooterRow.Cells[1].Text = "Total";
                            RegGrid.FooterRow.Cells[6].Text = total.ToString("N2");
                            decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("RegCapacity"));
                            RegGrid.FooterRow.Cells[5].Text = total1.ToString("N2");
                        }
                        else
                        {
                            Label2.Visible = false;
                            Label3.Visible = false;
                            Label4.Visible = false;
                            Label5.Visible = false;
                            Button1.Visible = false;
                            RegGrid.DataSource = null;
                            RegGrid.DataBind();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
                        }
                    }
                }
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtRegID.Text))
        {
            fillgrid();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Registration ID .....')", true);

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {


    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "AllOfferCapacity" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        RegGrid.Attributes["style"] = "border-collapse:separate";
        toexport.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Logins.aspx");
    }
    protected void RegGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = RegGrid.SelectedRow;
        HiddenField hdnOffer_Id = (HiddenField)gvr.FindControl("hdnOffer_Id");
        Delete(hdnOffer_Id.Value);
    }
    public void Delete(string Offer_Id)
    {
        string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        using (SqlConnection constr = new SqlConnection(CS))
        {
            SqlCommand cmd = new SqlCommand("Delete_Offered_Godown_Rabi_2024_25", constr);
            cmd.CommandType = CommandType.StoredProcedure;
            constr.Open();
            cmd.Parameters.AddWithValue("@Offer_Id", Offer_Id);
            cmd.Parameters.AddWithValue("@Deleted_By", IPAddress);
            cmd.Parameters.AddWithValue("@JVSYear", "Rab2024_25");
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = TheResult;
                fillgrid();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                //Clear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
            }
        }
    }
    public void Clear()
    {
        RegGrid.DataSource = null;
        RegGrid.DataBind();
    }
}