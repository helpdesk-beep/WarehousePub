using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Drawing;


public partial class JVSMiller_RegistrationPrint : System.Web.UI.Page
{
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["MillerRegId"] == null || Session["Id"] == null)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("../Login.aspx");
        }

        hdnMRegId.Value=Session["MillerRegId"].ToString(); 

        string mregid=hdnMRegId.Value.ToString();

        dt=GetMillRegistrationMaster(mregid);

        rptRegPrint.DataSource = dt;
        rptRegPrint.DataBind();



    }



    public DataTable GetMillRegistrationMaster(string mregid)
    {
        //string qr = "select mreg.Mill_Id, mreg.Registration_ID, pr.Miller, pr.Mobile, pr.Email, pr.Aadhar, pr.Pan, mreg.Purpose_Hiring, mreg.Rice_Capacity, mreg.Office_Contact, mreg.Mill_Landmark, st.State_Name, ds.District_Name, th.Tehsil_Name, bl.Block_Name, br.BranchName, mreg.Near_Distance, mreg.Mill_Office_Address, mreg.Incharge_Peson, mreg.Incharge_Post, mreg.Incharge_Email, mreg.Incharge_Mobile, mreg.Incharge_Address, mreg.Mill_Lat, mreg.Mill_Long, mreg.CreateOn from dbo.MillRegistrationMaster as mreg inner join PreRegistration as pr on pr.Registration_ID=mreg.Registration_ID inner join dbo.tbl_Metadata_State as st on st.State_Code=mreg.State_Id inner join dbo.tbl_MetaData_DISTRICT as ds on ds.District_Id=mreg.District_Id inner join dbo.Tehsils as th on th.TehsilCode=mreg.Tehsil_Code inner join dbo.tbl_Branch_Block_Mapping as bl on bl.Block_ID=mreg.Block_Id inner join dbo.MetaDataBranchWithIssueCenter as br on br.BranchID=mreg.Near_Branch_Id where mreg.Registration_ID='" + mregid + "'";

        string qr = "select mreg.Mill_Id,mreg.CreateOn, mreg.Mill_Name, mreg.Registration_ID, pr.Miller, pr.Mobile, pr.Email, pr.Aadhar, pr.Pan, mreg.Dist_Ind_RegNo, mreg.RegNo_Issue_Date,mreg.Mpscsc_Markfed, mreg.Agree_Miller_Id,mreg.Agree_Date, mreg.Agree_Capacity, mreg.Paddy_Capacity, mreg.Rice_Capacity, mreg.Office_Contact, mreg.Mill_Landmark, st.State_Name, ds.District_Name, th.Tehsil_Name, bl.Block_Name, br.BranchName, mreg.Near_Distance, mreg.Mill_Office_Address, mreg.Incharge_Peson, mreg.Incharge_Post, mreg.Incharge_Email, mreg.Incharge_Mobile, mreg.Incharge_Address, mreg.Mill_Lat, mreg.Mill_Long, mreg.CreateOn from dbo.MillRegistrationMaster as mreg inner join PreRegistration as pr on pr.Registration_ID=mreg.Registration_ID inner join dbo.tbl_Metadata_State as st on st.State_Code=mreg.State_Id inner join dbo.tbl_MetaData_DISTRICT as ds on ds.District_Id=mreg.District_Id inner join dbo.Tehsils as th on th.TehsilCode=mreg.Tehsil_Code inner join dbo.tbl_Branch_Block_Mapping as bl on bl.Block_ID=mreg.Block_Id inner join dbo.MetaDataBranchWithIssueCenter as br on br.BranchID=mreg.Near_Branch_Id where mreg.Registration_ID='" + mregid + "'";

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }


    protected void btnMakePayment_OnClick(object sender, EventArgs e)
    {
       // Response.Redirect("");
    }
   
}