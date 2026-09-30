using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Activities.Expressions;
using System.Web.DynamicData;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using System.Data.SqlTypes;
using MPSCSC_GodownDetails;

public partial class StatePages_VerifyGodown : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    SqlTransaction sqltrans;
    string Bill_Type = "";
    string Ref_Number = "";
    string Ref_Aid = "";
    string TheResult = "";
    MPSCSC_GodownDetails.MPSCSC_AddGodownDetails GodownDetailsDemo = new MPSCSC_GodownDetails.MPSCSC_AddGodownDetails();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                fillDistrict();
                fillgrid();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void fillDistrict()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

            }

            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            }
            else
            {
                if (Session["RoleId"].ToString() == "2")
                {
                    query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + Session["Region_ID"].ToString() + "' order by District_Name asc";
                }
                else
                {
                    query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where District_Id ='" + Session["Depot_DistID"].ToString() + "' order by District_Name asc";

                }
            }
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "--Select--");


            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    private void fillIssuecenter()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

                //if (Session["Region_ID"].ToString() != null)
                //{
                //    region = Session["Region_ID"].ToString();

                //}
            }

            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
            {
                query = "SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "'";
            }
            else
            {
                query = "  SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "' and DepoTypeID='4'";

            }
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbranch.Items.Clear();
                ddlbranch.DataSource = ds.Tables[0];
                ddlbranch.DataTextField = "DepotName";
                ddlbranch.DataValueField = "BranchId";
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, "--Select--");

            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillIssuecenter();
    }


    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_For_Verification", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlbranch.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@BranchID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            godown_GridView.DataSource = dt;
                            godown_GridView.DataBind();
                        }
                        else
                        {
                            godown_GridView.DataSource = null;
                            godown_GridView.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void godown_GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet ds = (DataSet)Session["dsGodown"];
        godown_GridView.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void godown_GridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = godown_GridView.SelectedRow;
    }
    protected void Edit(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            String Godown_ID = (row.FindControl("hdnGodown_ID") as HiddenField).Value;
            String Godown_Name = (row.FindControl("hdngodownname") as HiddenField).Value;
            String Branch_ID = (row.FindControl("hdnbranchid") as HiddenField).Value;
            //Verify(Godown_ID);
            //AddGodown();
            AddGodown(Godown_ID, Godown_Name, Branch_ID);

            //Call the method to send details through web service to MPSCSC
            Get_AddGodownDetailsInMPSCSC(Godown_ID);
        }

    }
    //protected async void AddGodown()
    //{
    //    var client = new HttpClient();
    //    var requestbody = "{" + "\"godown_id\"" + ": " + "\"2328003030152\"" + "," + "\"godown_name\"" + ": " + "\"Test Godown Don't Use\"" + "," + "\"depot_id\"" + ": " + "\"232800303\"" + "," + "\"password\"" + ": " + "\"d18b40d204f55b6f92f44c6e847a7a9df34c4ed503cf6085cb006b3422038743\"" + "}";
    //    var requestJson = new StringContent(requestbody, Encoding.UTF8, "application/json");
    //    HttpResponseMessage response = await client.PostAsync("https://scm.mp.gov.in/getgodowndetails/GodownService/GodownApp/newgodown", requestJson);
    //    var content = await response.Content.ReadAsStringAsync(); 
    //}
    //protected async void AddGodown(string GodownID,string GodownName,)
    protected async void AddGodown(string GodownID,string GodownName,string BranchID)
    //protected async void AddGodown()
    {
        //var client = new HttpClient();
        //var requestbody = "{" + "\"godown_id\"" + ": " + "\"2328003030152\"" + "," + "\"godown_name\"" + ": " + "\"Test Godown Don't Use\"" + "," + "\"depot_id\"" + ": " + "\"232800303\"" + "," + "\"password\"" + ": " + "\"d18b40d204f55b6f92f44c6e847a7a9df34c4ed503cf6085cb006b3422038743\"" + "}";
        //var requestJson = new StringContent(requestbody, Encoding.UTF8, "application/json");
        //HttpResponseMessage response = await client.PostAsync("https://scm.mp.gov.in/getgodowndetails/GodownService/GodownApp/newgodown", requestJson);
        //var content = await response.Content.ReadAsStringAsync();
        Uri myUri = new Uri("https://scm.mp.gov.in/getgodowndetails/GodownService/GodownApp/newgodown", UriKind.Absolute);
        //WebClient client = new WebClient();
        //client.OpenRead(myUri);

        var client = new HttpClient();
        var requestbody = "{" + "\"godown_id\"" + ": " + "\"" + GodownID + "\"" + "," + "\"godown_name\"" + ": " + "\"" + GodownName + "\"" + "," + "\"depot_id\"" + ": " + "\"" + BranchID + "\"" + "," + "\"password\"" + ": " + "\"d18b40d204f55b6f92f44c6e847a7a9df34c4ed503cf6085cb006b3422038743\"" + "}";
        //var requestbody = "{" + "\"godown_id\"" + ": " + "\"23350040060\"" + "," + "\"godown_name\"" + ": " + "\"19 PMS PUSHPA WAREHOUSE\"" + "," + "\"depot_id\"" + ": " + "\"2328001\"" + "," + "\"password\"" + ": " + "\"d18b40d204f55b6f92f44c6e847a7a9df34c4ed503cf6085cb006b3422038743\"" + "}";

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(requestbody)", true);

        var requestJson = new StringContent(requestbody, Encoding.UTF8, "application/json");
        //HttpResponseMessage response = await client.PostAsync("https://scm.mp.gov.in/getgodowndetails/GodownService/GodownApp/newgodown", requestJson);
        HttpResponseMessage response = await client.PostAsync(myUri, requestJson);

        var content = await response.Content.ReadAsStringAsync();
    }
    protected void Delete(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            String Godown_ID = (row.FindControl("hdnGodown_ID2") as HiddenField).Value;
            Deleted(Godown_ID);
        }
    }
    public void Verify(String Godown_ID)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (con != null)
        {
            con.Open();
            cmd = new SqlCommand("Verify_Godown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GodownID", Godown_ID);
            cmd.Parameters.AddWithValue("@VerifyBy", ip);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Godown Verified Successfully..'); </script> ");
                fillgrid();


            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Godown NOT Verified '); </script> ");
            }
        }
    }

    public void Deleted(String Godown_ID)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (con != null)
        {
            con.Open();
            cmd = new SqlCommand("Delete_Godown_before_Verify", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GodownID", Godown_ID);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Godown Deleted Successfully..'); </script> ");
                fillgrid();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Godown NOT Delete '); </script> ");
            }
        }
    }


    public void Get_AddGodownDetailsInMPSCSC(String Godown_ID)
    {
        DataTable dtGodownDetails = new DataTable();
        SqlDataAdapter da; //= new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = str;
        con.Open();
        string qry = "SELECT * from tbl_MetaData_GODOWN_2018 WHERE Godown_ID ='" + Godown_ID + "'";
        //qry = 
        SqlCommand cmd = new SqlCommand(qry, con);
        da = new SqlDataAdapter(cmd);
        da.Fill(dtGodownDetails);
        //con.Close();
        string varGodown_ID = Godown_ID;
        var t = dtGodownDetails.Rows[0]["Verify_Date"].ToString();
        string varStateId = (dtGodownDetails.Rows[0]["StateId"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["StateId"].ToString());
        string varDistrictId = (dtGodownDetails.Rows[0]["DistrictId"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["DistrictId"].ToString());
        string varDepotId = (dtGodownDetails.Rows[0]["DepotId"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["DepotId"].ToString());
        string varGodown_Name = (dtGodownDetails.Rows[0]["Godown_Name"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Godown_Name"].ToString());
        string varGodown_Formation_Date = Convert.ToString(dtGodownDetails.Rows[0]["Godown_Formation_Date"] == DBNull.Value ? "1900-01-01 00:00:00" : Convert.ToString(dtGodownDetails.Rows[0]["Godown_Formation_Date"]));
        string varGodown_Updation_Date = Convert.ToString(dtGodownDetails.Rows[0]["Godown_Updation_Date"] == DBNull.Value ? "1900-01-01 00:00:00" : Convert.ToString(dtGodownDetails.Rows[0]["Godown_Updation_Date"]));
        //decimal varGodown_Capacity = Convert.ToDecimal(dtGodownDetails.Rows[0]["Godown_Capacity"] == DBNull.Value ? 0.00 : dtGodownDetails.Rows[0]["Godown_Capacity"]);
        string varGodown_Capacity = Convert.ToString(dtGodownDetails.Rows[0]["Godown_Capacity"] == DBNull.Value ? "0.00" : dtGodownDetails.Rows[0]["Godown_Capacity"]); 
        string varRemarks = (dtGodownDetails.Rows[0]["Remarks"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Remarks"].ToString());
        string varCreatedBy = (dtGodownDetails.Rows[0]["CreatedBy"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["CreatedBy"].ToString());
        string varUpdatedBy = (dtGodownDetails.Rows[0]["UpdatedBy"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["UpdatedBy"].ToString());
        string varDeletedBy = (dtGodownDetails.Rows[0]["DeletedBy"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["DeletedBy"].ToString());
        string varHired_Type = (dtGodownDetails.Rows[0]["Hired_Type"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Hired_Type"].ToString());
        string varStorage_Type = (dtGodownDetails.Rows[0]["Storage_Type"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Storage_Type"].ToString());
        decimal varGodown_Scientific_Capacity = Convert.ToDecimal(dtGodownDetails.Rows[0]["Godown_Scientific_Capacity"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Godown_Scientific_Capacity"]);
        string varGodown_APN = (dtGodownDetails.Rows[0]["Godown_APN"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Godown_APN"].ToString());
        string varGodown_Email = (dtGodownDetails.Rows[0]["Godown_Email"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Godown_Email"].ToString());
        string varGodown_Mobile = (dtGodownDetails.Rows[0]["Godown_Mobile"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Godown_Mobile"].ToString());
        string varGodown_Address = (dtGodownDetails.Rows[0]["Godown_Address"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Godown_Address"].ToString());
        string varBranchID = (dtGodownDetails.Rows[0]["BranchID"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["BranchID"].ToString());
        string varLicNum = (dtGodownDetails.Rows[0]["LicNum"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["LicNum"].ToString());
        string varPAN = (dtGodownDetails.Rows[0]["PAN"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["PAN"].ToString());
        string varBank_ID = (dtGodownDetails.Rows[0]["Bank_ID"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Bank_ID"].ToString());
        string varAccNo = (dtGodownDetails.Rows[0]["AccNo"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["AccNo"].ToString());
        string varIFSC_Code = (dtGodownDetails.Rows[0]["IFSC_Code"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["IFSC_Code"].ToString());
        string varBank_Add = (dtGodownDetails.Rows[0]["Bank_Add"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Bank_Add"].ToString());
        string varLatitude = (dtGodownDetails.Rows[0]["Latitude"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Latitude"].ToString());
        string varLongitude = (dtGodownDetails.Rows[0]["Longitude"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Longitude"].ToString());
        string varGodownNum = (dtGodownDetails.Rows[0]["GodownNum"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["GodownNum"].ToString());
        string varKhasranum = (dtGodownDetails.Rows[0]["Khasranum"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Khasranum"].ToString());
        string varRakwanum = (dtGodownDetails.Rows[0]["Rakwanum"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Rakwanum"].ToString());
        string varTehshilID = (dtGodownDetails.Rows[0]["TehshilID"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["TehshilID"].ToString());
        string varVillageName = (dtGodownDetails.Rows[0]["VillageName"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["VillageName"].ToString());
        string varOrg_Name = (dtGodownDetails.Rows[0]["Org_Name"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Org_Name"].ToString());
        string varGInchargeName = (dtGodownDetails.Rows[0]["GInchargeName"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["GInchargeName"].ToString());
        string varGInchargeAddress = (dtGodownDetails.Rows[0]["GInchargeAddress"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["GInchargeAddress"].ToString());
        string varGInchargeMobile = (dtGodownDetails.Rows[0]["GInchargeMobile"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["GInchargeMobile"].ToString());
        string varGInchargeEmail = (dtGodownDetails.Rows[0]["GInchargeEmail"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["GInchargeEmail"].ToString());
        string varWeightmentType = (dtGodownDetails.Rows[0]["WeightmentType"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["WeightmentType"].ToString());
        string varGodown_Reg_No = (dtGodownDetails.Rows[0]["Godown_Reg_No"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Godown_Reg_No"].ToString());
        string varIsActive = (dtGodownDetails.Rows[0]["IsActive"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["IsActive"].ToString());
        string varLR_TehsilCode = (dtGodownDetails.Rows[0]["LR_TehsilCode"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["LR_VillageCode"].ToString());
        string varLR_VillageCode = (dtGodownDetails.Rows[0]["LR_VillageCode"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["LR_VillageCode"].ToString());
        decimal varLenght = Convert.ToDecimal(dtGodownDetails.Rows[0]["Lenght"] == DBNull.Value ? 0.00 : dtGodownDetails.Rows[0]["Lenght"]);
        decimal varWidth = Convert.ToDecimal(dtGodownDetails.Rows[0]["Width"] == DBNull.Value ? 0.00 : dtGodownDetails.Rows[0]["Width"]);
        decimal varHeight = Convert.ToDecimal(dtGodownDetails.Rows[0]["Height"] == DBNull.Value ? 0.00 : dtGodownDetails.Rows[0]["Height"]);
        decimal varPremise_capacity = Convert.ToDecimal(dtGodownDetails.Rows[0]["Premise_capacity"] == DBNull.Value ? 0.00 : dtGodownDetails.Rows[0]["Premise_capacity"]);
        decimal varClosing_Balance = Convert.ToDecimal(dtGodownDetails.Rows[0]["Closing_Balance"] == DBNull.Value ? 0.00 : dtGodownDetails.Rows[0]["Closing_Balance"]);
        string varJVS_RegNo = (dtGodownDetails.Rows[0]["JVS_RegNo"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["JVS_RegNo"].ToString());
        string varLicOwnerNM = (dtGodownDetails.Rows[0]["LicOwnerNM"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["LicOwnerNM"].ToString());
        decimal varLicCapacity = Convert.ToDecimal(dtGodownDetails.Rows[0]["LicCapacity"] == DBNull.Value ? 0.00 : dtGodownDetails.Rows[0]["LicCapacity"]);
        string varWH_Name = (dtGodownDetails.Rows[0]["WH_Name"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["WH_Name"].ToString());
        string varWH_Type = (dtGodownDetails.Rows[0]["WH_Type"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["WH_Type"].ToString());
        string varVerify_By = (dtGodownDetails.Rows[0]["Verify_By"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Verify_By"].ToString());
        string varWHID = (dtGodownDetails.Rows[0]["WHID"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["WHID"].ToString());
        string varMaintain_By = (dtGodownDetails.Rows[0]["Maintain_By"] == DBNull.Value ? String.Empty : dtGodownDetails.Rows[0]["Maintain_By"].ToString());
        string varCreatedDate = Convert.ToString(dtGodownDetails.Rows[0]["CreatedDate"] == DBNull.Value ? "1900-01-01 00:00:00" : Convert.ToString(dtGodownDetails.Rows[0]["CreatedDate"]));
        string varUpdatedDate = Convert.ToString(dtGodownDetails.Rows[0]["UpdatedDate"] == DBNull.Value ? "1900-01-01 00:00:00" : Convert.ToString(dtGodownDetails.Rows[0]["UpdatedDate"]));
        string varDeletedDate = Convert.ToString(dtGodownDetails.Rows[0]["DeletedDate"] == DBNull.Value ? "1900-01-01 00:00:00" : Convert.ToString(dtGodownDetails.Rows[0]["DeletedDate"]));
        string varLicDate = Convert.ToString(dtGodownDetails.Rows[0]["LicDate"] == DBNull.Value ? "1900-01-01 00:00:00" : Convert.ToString(dtGodownDetails.Rows[0]["LicDate"]));
        string varLicIssueDate = Convert.ToString(dtGodownDetails.Rows[0]["LicIssueDate"] == DBNull.Value ? "1900-01-01 00:00:00" : Convert.ToString(dtGodownDetails.Rows[0]["LicIssueDate"]));
        string varVerify_Date = Convert.ToString(dtGodownDetails.Rows[0]["Verify_Date"] == DBNull.Value ? "1900-01-01 00:00:00" : Convert.ToString(dtGodownDetails.Rows[0]["Verify_Date"]));

        System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
        GodownDetailsDemo.AddGodownDetails(varGodown_ID, varStateId, varDistrictId, varDepotId
        , varGodown_Name, varGodown_Formation_Date, varGodown_Updation_Date, varGodown_Capacity, varRemarks, varCreatedBy, varCreatedDate
        , varUpdatedBy, varUpdatedDate, varDeletedBy, varDeletedDate, varHired_Type, varStorage_Type, varGodown_Scientific_Capacity
        , varGodown_APN, varGodown_Email, varGodown_Mobile, varGodown_Address, varBranchID, varLicNum, varLicDate, varPAN, varBank_ID, varAccNo
        , varIFSC_Code, varBank_Add, varLatitude, varLongitude, varGodownNum, varKhasranum, varRakwanum, varTehshilID
        , varVillageName, varOrg_Name, varGInchargeName, varGInchargeAddress, varGInchargeMobile, varGInchargeEmail, varWeightmentType, varLicIssueDate
        , varGodown_Reg_No, varIsActive, varLR_TehsilCode, varLR_VillageCode, varLenght, varWidth, varHeight, varPremise_capacity, varClosing_Balance
        , varJVS_RegNo, varLicOwnerNM, varLicCapacity, varWH_Name, varWH_Type
        , varVerify_By, varVerify_Date, varWHID, varMaintain_By);
        con.Close();
    }
}