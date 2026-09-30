using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Masters_PrintGodownMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblDate.Text = DateTime.Now.ToShortDateString();
        if (!IsPostBack)
        {
            if (Session["GodownID"] != null)
            {
                if (!String.IsNullOrEmpty(Session["GodownID"].ToString()))
                {
                    fillData();
                }
            }
        }
    }
    protected void fillData()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_GODOWN_For_Print", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Godown_ID", Session["GodownID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            lblRegID.Text = dt.Rows[0]["JVS_RegNo"].ToString();
                            lblGodownID.Text = Session["GodownID"].ToString();
                            lblgodownnum.Text = dt.Rows[0]["GodownNum"].ToString();
                            lblGodownName.Text = dt.Rows[0]["Godown_Name"].ToString();
                            lblAPN.Text = dt.Rows[0]["Godown_APN"].ToString();
                            lblemailid.Text = dt.Rows[0]["Godown_Email"].ToString();
                            lblmobile.Text = dt.Rows[0]["Godown_Mobile"].ToString();
                            lblCapacity.Text = dt.Rows[0]["Godown_Capacity"].ToString();
                            lblScientificCapacity.Text = dt.Rows[0]["Godown_Scientific_Capacity"].ToString();
                            lblGodownType.Text = dt.Rows[0]["Hired_Type"].ToString();
                            lblStorageType.Text = dt.Rows[0]["Storage_Type"].ToString();
                            lblPremiseCpt.Text = dt.Rows[0]["Premise_capacity"].ToString();
                            lbllicnum.Text = dt.Rows[0]["LicNum"].ToString();
                            lblLicIssueDate.Text = dt.Rows[0]["LicIssueDate"].ToString();
                            lbllicdate.Text = dt.Rows[0]["LicDate"].ToString();
                            lbladdress.Text = dt.Rows[0]["Godown_Address"].ToString();
                            lbllatitude.Text = dt.Rows[0]["Latitude"].ToString();
                            lbllongitude.Text = dt.Rows[0]["Longitude"].ToString();
                            lblKhand.Text = dt.Rows[0]["Tehsil_Name"].ToString();
                            lblVillage.Text = dt.Rows[0]["VillageName"].ToString();
                            lblkhasra.Text = dt.Rows[0]["Khasranum"].ToString();
                            lblrakwa.Text = dt.Rows[0]["Rakwanum"].ToString();
                            lblWeightmentS.Text = dt.Rows[0]["WeightmentType"].ToString();
                            lblbranchname.Text= dt.Rows[0]["DepotName"].ToString();
                            lblDistrict.Text = dt.Rows[0]["District_Name_HI"].ToString();
                            lblEmail.Text = dt.Rows[0]["Email"].ToString();
                            lblBM.Text = dt.Rows[0]["NodalOfficeName"].ToString();
                        }
                        else
                        {
                            lblRegID.Text = "---";
                            lblGodownID.Text = "---";
                            lblgodownnum.Text = "---";
                            lblGodownName.Text = "---";
                            lblAPN.Text = "---";
                            lblemailid.Text = "---";
                            lblmobile.Text = "---";
                            lblCapacity.Text = "---";
                            lblScientificCapacity.Text = "---";
                            lblGodownType.Text = "---";
                            lblStorageType.Text = "---";
                            lblPremiseCpt.Text = "---";
                            lbllicnum.Text = "---";
                            lblLicIssueDate.Text = "---";
                            lbllicdate.Text = "---";
                            lbladdress.Text = "---";
                            lbllatitude.Text = "---";
                            lbllongitude.Text = "---";
                            lblKhand.Text = "---";
                            lblVillage.Text = "---";
                            lblkhasra.Text = "---";
                            lblrakwa.Text = "---";
                            lblWeightmentS.Text = "---";
                        }
                    }
                }
            }
        }
    }
}