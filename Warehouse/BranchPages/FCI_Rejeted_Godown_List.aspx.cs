using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_FCI_Rejeted_Godown_List : System.Web.UI.Page
{
    private string connString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    FCI_Rejected_Godown_Bill_MPSCSC MPSCSC_Demo = new FCI_Rejected_Godown_Bill_MPSCSC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["BranchId"] != null)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }

    private void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand("SELECT gdn.Godown_Name as GodownName,Fci.* FROM FCI_Rejected_Godown_List Fci inner join tbl_MetaData_GODOWN_2018 Gdn on gdn.Godown_ID= fci.Godown_Id where gdn.BranchID= '" + Session["BranchId"].ToString() + "' order by gdn.Godown_Name", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            gvRejectedGodowns.DataSource = dt;
            gvRejectedGodowns.DataBind();
        }
    }

    protected void gvRejectedGodowns_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        int recordID = (int)gvRejectedGodowns.DataKeys[index].Value;

        if (e.CommandName == "UpdateRecord")
        {
            GridViewRow row = gvRejectedGodowns.Rows[index];

            string godownName = row.Cells[1].Text;
            string godownId = row.Cells[2].Text;
            string cropYear = row.Cells[3].Text;
            string rejectedQuantity = row.Cells[4].Text;
            string reasonForRejection = row.Cells[5].Text;
            TextBox upgrade = (TextBox)row.FindControl("txtUpgradeQty");
            TextBox lift = (TextBox)row.FindControl("txtLiftQty");
            TextBox pUp = (TextBox)row.FindControl("txtPendingUp");
            TextBox pLift = (TextBox)row.FindControl("txtPendingLift");
            FileUpload fu = (FileUpload)row.FindControl("fuDoc");

            TextBox txtUpQty = (TextBox)row.FindControl("txtUpgradeQty");
            TextBox txtLftQty = (TextBox)row.FindControl("txtLiftQty");
            TextBox txtPndUp = (TextBox)row.FindControl("txtPendingUp");
            TextBox txtPndLft = (TextBox)row.FindControl("txtPendingLift");


            byte[] fileData = null;
            string mimeType = "";

            if (fu.HasFile)
            {
                if (fu.PostedFile.ContentLength > 20971520) // 20MB limit
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('File size must be less than 20MB');", true);
                    return;
                }

                using (BinaryReader br = new BinaryReader(fu.PostedFile.InputStream))
                {
                    fileData = br.ReadBytes(fu.PostedFile.ContentLength);
                }
                mimeType = fu.PostedFile.ContentType;
            }

            int res = UpdateDatabase(recordID, upgrade.Text, lift.Text, pUp.Text, pLift.Text, fileData, mimeType);

            try
            {
                System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
                // Consuming the Service Method
                string result = MPSCSC_Demo.UpdateRejectedGodown(
                    recordID,
                    godownId,
                    godownName,
                    cropYear,
                   Convert.ToDecimal(rejectedQuantity),      // Rejected_QTY (Pass existing or zero if not editing here)
                    reasonForRejection,     // Reason (Pass existing or empty)
                    decimal.Parse(txtUpQty.Text),
                    decimal.Parse(txtLftQty.Text),
                    decimal.Parse(txtPndUp.Text),
                    decimal.Parse(txtPndLft.Text),
                    "",     // Remark
                    fileData,
                    mimeType
                );

                // Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + result + "');", true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Service Error: " + ex.Message + "');", true);
            }

            if (res > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "alert('Record Updated Successfully');");
                BindGrid();
            }
            else
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "alert('Record Updation Failed');");




        }
        else if (e.CommandName == "ViewDoc")
        {
            DownloadFile(recordID);
        }
    }


    private void DownloadFile(int id)
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand("SELECT Upload_Document, Document_MimeType FROM FCI_Rejected_Godown_List WHERE ID=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read() && dr["Upload_Document"] != DBNull.Value)
                {
                    byte[] bytes = (byte[])dr["Upload_Document"];
                    string contentType = dr["Document_MimeType"].ToString();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = contentType;

                    // "inline" opens it in the browser tab
                    Response.AddHeader("Content-Disposition", "inline; filename=FCI_Doc_" + id);
                    Response.BinaryWrite(bytes);

                    // Reset the form target so the Update button doesn't open in a new tab later
                    Response.Write("<script>window.document.forms[0].target='_self';</script>");
                    Response.End();
                }
            }
        }
    }
    private int UpdateDatabase(int id, string up, string lift, string pUp, string pLift, byte[] doc, string mime)
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            string query = @"UPDATE FCI_Rejected_Godown_List 
                             SET Upgrade_Qty=@up, Lift_Qty=@lift, Pending_For_Upgradation=@pUp, Pending_For_Lift_Upgradation_Qty=@pLift";

            if (doc != null)
            {
                query += ", Upload_Document=@doc, Document_MimeType=@mime";
            }
            query += " WHERE ID=@id";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@up", decimal.Parse(up));
            cmd.Parameters.AddWithValue("@lift", decimal.Parse(lift));
            cmd.Parameters.AddWithValue("@pUp", decimal.Parse(pUp));
            cmd.Parameters.AddWithValue("@pLift", decimal.Parse(pLift));
            cmd.Parameters.AddWithValue("@id", id);

            if (doc != null)
            {
                cmd.Parameters.AddWithValue("@doc", doc);
                cmd.Parameters.AddWithValue("@mime", mime);
            }
            con.Open();
            return cmd.ExecuteNonQuery();




        }
    }
}