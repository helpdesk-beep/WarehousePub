using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.IO;

using System.Text;

public partial class Administration_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillData();
        }
    }
    protected void fillData()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            
           // using (SqlCommand cmd = new SqlCommand("State_Dashboard", con))
            using (SqlCommand cmd = new SqlCommand("State_Dashboard_Pendancy_After_DM_Payment_at_RM", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            NoOfBill.InnerText = dt.Rows[0]["NoOfBill"].ToString();
                            NoOfBillAmt.InnerText = dt.Rows[0]["NoOfBillAmt"].ToString();
                            ICMSubmit.InnerText = dt.Rows[0]["ICMSubmit"].ToString();
                            ICMSubmitAmt.InnerText = dt.Rows[0]["ICMSubmitAmt"].ToString();
                            SubmittedICM.InnerText = dt.Rows[0]["SubmittedICM"].ToString();
                            SubmittedICMAmt.InnerText = dt.Rows[0]["SubmittedICMAmt"].ToString();
                            PendingBill.InnerText = dt.Rows[0]["PendingBill"].ToString();
                            PendingICMSubmit.InnerText = dt.Rows[0]["PendingICMSubmit"].ToString();
                            PendingICMSubmitAmt.InnerText = dt.Rows[0]["PendingICMSubmitAmt"].ToString();
                            PendingSubmittedICM.InnerText = dt.Rows[0]["PendingSubmittedICM"].ToString();
                            PendingSubmittedICMAmt.InnerText = dt.Rows[0]["PendingSubmittedICMAmt"].ToString();
                            DMBill.InnerText = dt.Rows[0]["DMBill"].ToString();
                            DMAmt.InnerText = dt.Rows[0]["DMAmt"].ToString();
                            RMDSCBill.InnerText = dt.Rows[0]["RMDSCBill"].ToString();
                            RMDSCAmt.InnerText = dt.Rows[0]["RMDSCAmt"].ToString();
                            NEFTBill.InnerText = dt.Rows[0]["NEFTBill"].ToString();
                            NEFTAmt.InnerText = dt.Rows[0]["NEFTAmt"].ToString();
                            PendingDMBill.InnerText = dt.Rows[0]["PendingDMBill"].ToString();
                            PendingDMAmt.InnerText = dt.Rows[0]["PendingDMAmt"].ToString();
                            PendingRMDSCBill.InnerText = dt.Rows[0]["PendingRMDSCBill"].ToString();
                            PendingRMDSCAmt.InnerText = dt.Rows[0]["PendingRMDSCAmt"].ToString();
                            PendingNEFTBill.InnerText = dt.Rows[0]["PendingNEFTBill"].ToString();
                            PendingNEFTAmt.InnerText = dt.Rows[0]["PendingNEFTAmt"].ToString();
                            PendingHOBill.InnerText = dt.Rows[0]["PendingHOBill"].ToString();
                            PendingHOAmt.InnerText = dt.Rows[0]["PendingHOAmt"].ToString();
                            CreatedEPFBill.InnerText = dt.Rows[0]["CreatedEPFBill"].ToString();
                            CreatedEPFAmt.InnerText = dt.Rows[0]["CreatedEPFAmt"].ToString();
                            PendingCreatedEPFBill.InnerText = dt.Rows[0]["PendingCreatedEPFBill"].ToString();
                            PendingCreatedEPFAmt.InnerText = dt.Rows[0]["PendingCreatedEPFAmt"].ToString();
                        }
                        else
                        {
                           
                        }
                    }
                }
            }
        }
    }
}