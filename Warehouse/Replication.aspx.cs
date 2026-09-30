using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services.Protocols;

namespace Replication
{
    public partial class Replication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           

            string strTbl = DropDownList1.SelectedItem.Text;
            string noOfRecords = noOfRecord.Text;
            string from = fromDate.Text;
            string to = toDate.Text;
           
            if (strTbl == "tbl_storage_Depositor_WHR_Relation")
            {

                //ReplicateReference.Replicate_Table_DataSoapClient replicate = new ReplicateReference.Replicate_Table_DataSoapClient();
                ReplicateReference.Replicate_Table_Data replicate = new ReplicateReference.Replicate_Table_Data();
                string str = replicate.FetchDataHere(strTbl,noOfRecords,from,to);
                //Response.Write(str);
                //return;

                string Depositor_WHR_Id = "";
                string State_Id = "";
                string District_Id = "";
                string Depotid = "";
                int Commodity_Id;
                string Category_Id = "";
                string Whr_No = "";
                string Depositor_Name = "";
                String Date_of_Deposit = "";
                int TotalBags_Received;
                double Total_Qty_Received;
                string Mode_of_weighment = "";
                string BeamScale_LWB = "";
                double AvgMoisture_Content;
                string Lot_No = "";
                double MktValue_of_Commodity;
                string Arrival_Source = "";
                String WHR_Issue_Date = "";
                String CreatedBy = "";
                String CreatedDate = "";
                string UpdatedBy = "";
                string UpdatedDate = "";
                string DeletedBy = "";
                string DeletedDate = "";
                string MadeUpBags = "";
                string Client_IP = "";
                double AvgMoisture_Content_To;
                string Did;
                string CropYear = "";
                string Remark = "";
                string SangrahadDate = "";
                string LicenseNo = "";
                string LicenseDate = "";
                String wday;
                string wmon;
                string wyear;
                string BranchID = "";
                string DepositorID = "";
                string GodownID = "";
                string Gid = "";
                string Delete_Flag = "";
                string New_Column_10092022 = "";


                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(str);
                JObject jObj = JObject.Parse(str);
                int count = myDeserializedClass.tbl_storage_Depositor_WHR_Relation.Count;
                for (int i = 0; i < count; i++)
                {

                    Depositor_WHR_Id = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].Depositor_WHR_Id;
                    State_Id = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].State_Id;
                    District_Id = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].District_Id;
                    Depotid = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].Depotid;
                    Commodity_Id = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].Commodity_Id;
                    Category_Id = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].Category_Id;
                    Whr_No = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].Whr_No;
                    Depositor_Name = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].Depositor_Name;
                    Date_of_Deposit = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].Date_of_Deposit;
                    TotalBags_Received = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].TotalBags_Received;
                    Total_Qty_Received = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].Total_Qty_Received;
                    Mode_of_weighment = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].Mode_of_weighment;
                    BeamScale_LWB = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].BeamScale_LWB;
                    AvgMoisture_Content = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].AvgMoisture_Content;
                    Lot_No = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].Lot_No;
                    MktValue_of_Commodity = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].MktValue_of_Commodity;
                    Arrival_Source = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].Arrival_Source;
                    WHR_Issue_Date = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].WHR_Issue_Date;
                    CreatedBy = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].CreatedBy;
                    CreatedDate = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].CreatedDate;
                    UpdatedBy = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].UpdatedBy;
                    UpdatedDate = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].UpdatedDate;
                    DeletedBy = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].DeletedBy;
                    DeletedDate = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].DeletedDate;
                    MadeUpBags = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].MadeUpBags;
                    Client_IP = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].Client_IP;
                    AvgMoisture_Content_To = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].AvgMoisture_Content_To;
                    Did = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].Did;
                    CropYear = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].CropYear;
                    Remark = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].Remark;
                    SangrahadDate = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].SangrahadDate;
                    LicenseNo = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].LicenseNo;
                    LicenseDate = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].LicenseDate;
                    wday = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].wday;
                    wmon = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].wmon;
                    wyear = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].wyear;
                    BranchID = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].BranchID;
                    DepositorID = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].DepositorID;
                    GodownID = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].GodownID;
                    Gid = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].Gid;
                    Delete_Flag = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].Delete_Flag;
                    New_Column_10092022 = myDeserializedClass.tbl_storage_Depositor_WHR_Relation[i].New_Column_10092022;



                    save(Depositor_WHR_Id, State_Id, District_Id, Depotid, Commodity_Id, Category_Id, Whr_No, Depositor_Name, Date_of_Deposit, TotalBags_Received, Total_Qty_Received, Mode_of_weighment, BeamScale_LWB, AvgMoisture_Content, Lot_No, MktValue_of_Commodity, Arrival_Source, WHR_Issue_Date, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, DeletedBy, DeletedDate, MadeUpBags, Client_IP, AvgMoisture_Content_To, Did, CropYear, Remark, SangrahadDate, LicenseNo, LicenseDate, wday, wmon, wyear, BranchID, DepositorID, GodownID, Gid, Delete_Flag, New_Column_10092022);

                }



            }
            else if (strTbl == "tbl_storage_Stacking_Details")
            {
                //ReplicateReference.Replicate_Table_DataSoapClient replicate = new ReplicateReference.Replicate_Table_DataSoapClient();
                ReplicateReference.Replicate_Table_Data replicate = new ReplicateReference.Replicate_Table_Data();
                string str = replicate.FetchDataHere(strTbl,noOfRecords,from,to);
                //Response.Write(str);
                //return;


                string State_Id = "";
                string District_Id = "";
                string Depotid = "";
                string Godown_ID = "";
                string Stack_ID = "";
                string StorageReceipt_Id = "";
                int Bags;
                double Weight;
                String CreatedBy = "";
                String CreatedDate = "";
                string UpdatedBy = "";
                string UpdatedDate = "";
                string DeletedBy = "";
                string DeletedDate = "";
                //int autoid;
                string WHRId = "";
                string Status = "";
                string Branchid = "";

                RootStacking myDeserializedClass = JsonConvert.DeserializeObject<RootStacking>(str);
                JObject jObj = JObject.Parse(str);
                int count = myDeserializedClass.tbl_storage_Stacking_Details.Count;
                for (int i = 0; i < count; i++)
                {

                    State_Id = myDeserializedClass.tbl_storage_Stacking_Details[i].State_Id;
                    District_Id = myDeserializedClass.tbl_storage_Stacking_Details[i].District_Id;
                    //District_Id = myDeserializedClass.tbl_storage_Stacking_Details[i].District_Id;
                    Depotid = myDeserializedClass.tbl_storage_Stacking_Details[i].Depotid;
                    Godown_ID = myDeserializedClass.tbl_storage_Stacking_Details[i].Godown_ID;
                    Stack_ID = myDeserializedClass.tbl_storage_Stacking_Details[i].Stack_ID;
                    StorageReceipt_Id = myDeserializedClass.tbl_storage_Stacking_Details[i].StorageReceipt_Id;
                    Bags = myDeserializedClass.tbl_storage_Stacking_Details[i].Bags;
                    Weight = myDeserializedClass.tbl_storage_Stacking_Details[i].Weight;
                    CreatedBy = myDeserializedClass.tbl_storage_Stacking_Details[i].CreatedBy;
                    CreatedDate = myDeserializedClass.tbl_storage_Stacking_Details[i].CreatedDate;
                    UpdatedBy = myDeserializedClass.tbl_storage_Stacking_Details[i].UpdatedBy;
                    UpdatedDate = myDeserializedClass.tbl_storage_Stacking_Details[i].UpdatedDate;
                    DeletedBy = myDeserializedClass.tbl_storage_Stacking_Details[i].DeletedBy;
                    DeletedDate = myDeserializedClass.tbl_storage_Stacking_Details[i].DeletedDate;
                    //autoid = myDeserializedClass.tbl_storage_Stacking_Details[i].autoid;
                    WHRId = myDeserializedClass.tbl_storage_Stacking_Details[i].WHRId;
                    Status = myDeserializedClass.tbl_storage_Stacking_Details[i].Status;
                    Branchid = myDeserializedClass.tbl_storage_Stacking_Details[i].Branchid;

                    saveData(State_Id, District_Id, Depotid, Godown_ID, Stack_ID, StorageReceipt_Id, Bags, Weight, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, DeletedBy, DeletedDate, WHRId, Status, Branchid);

                }
            }
            else if (strTbl == "tbl_MetaData_GODOWN_2018")
            {
                ReplicateReference.Replicate_Table_Data replicate = new ReplicateReference.Replicate_Table_Data();
                //ReplicateReference.Replicate_Table_DataSoapClient replicate = new ReplicateReference.Replicate_Table_DataSoapClient();
                string str = replicate.FetchDataHere(strTbl, noOfRecords, from, to);
                //Response.Write(str);
                //return;


                string Godown_ID = "";
                string State_Id = "";
                string District_Id = "";
                string Depotid = "";
                string Godown_Name = "";
                string UpdatedDate = "";
                string Godown_Formation_Date = "";
                string Godown_Updation_Date = "";
                string Godown_Capacity = "";
                string Remarks = "";
                string CreatedBy = "";
                string CreatedDate = "";
                string UpdatedBy = "";
                string DeletedBy = "";
                string DeletedDate = "";
                string Hired_Type = "";
                string Storage_Type = "";
                string Godown_Scientific_Capacity = "";
                string Godown_APN = "";
                string Godown_Email = "";
                string Godown_Mobile = "";
                string Godown_Address = "";
                string Branchid = "";
                string LicNum = "";
                string LicDate = "";
                string PAN = "";
                string Bank_ID = "";
                string AccNo = "";
                string IFSC_Code = "";
                string Bank_Add = "";
                string Latitude = "";
                string Longitude = "";
                string GodownNum = "";
                string Khasranum = "";
                string Rakwanum = "";
                string TehshilID = "";
                string VillageName = "";
                string Org_Name = "";
                string GInchargeName = "";
                string GInchargeAddress = "";
                String GInchargeMobile = "" ;
                string GInchargeEmail = "";
                string WeightmentType = "";
                string LicIssueDate = "";
                string Godown_Reg_No = "";
                string IsActive = "";
                string LR_TehsilCode = "";
                string LR_VillageCode = "";
                string Lenght = "";
                string Width = "";
                string Height = "";
                string Premise_capacity = "";
                string Closing_Balance = "" ;
                string JVS_RegNo = "";
                string LicOwnerNM = "";
                string LicCapacity = "";
                string WH_Name = "";
                string WH_Type = "";
                string Verify_By = "";
                string Verify_Date = "";
                string WHID = "";
                

                RootMetaDataGodown myDeserializedClass = JsonConvert.DeserializeObject<RootMetaDataGodown>(str);
                JObject jObj = JObject.Parse(str);
                int count = myDeserializedClass.tbl_MetaData_GODOWN_2018.Count;
                for (int i = 0; i < count; i++)
                {

                    Godown_ID = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Godown_ID;
                    State_Id = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].StateId;
                    District_Id = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].DistrictId;
                    Depotid = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].DepotId;
                    Godown_Name = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Godown_Name;
                    Godown_Formation_Date = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Godown_Formation_Date;
                    Godown_Updation_Date = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Godown_Updation_Date;
                    Godown_Capacity = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Godown_Capacity;
                    Remarks = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Remarks;
                    CreatedBy = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].CreatedBy;
                    CreatedDate = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].CreatedDate;
                    UpdatedBy = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].UpdatedBy;
                    UpdatedDate = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].UpdatedDate;
                    DeletedBy = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].DeletedBy;
                    DeletedDate = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].DeletedDate;
                    Hired_Type = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Hired_Type;
                    Storage_Type = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Storage_Type;
                    Godown_Scientific_Capacity = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Godown_Scientific_Capacity;
                    Godown_APN = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Godown_APN;
                    Godown_Email = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Godown_Email;
                    Godown_Mobile = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Godown_Mobile;
                    Godown_Address = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Godown_Address;
                    Branchid = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].BranchID;
                    LicNum = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].LicNum;
                    LicDate = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].LicDate;
                    PAN = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].PAN;
                    Bank_ID = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Bank_ID;
                    AccNo = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].AccNo;
                    IFSC_Code = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].IFSC_Code;
                    Bank_Add = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Bank_Add;
                    Latitude = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Latitude;
                    Longitude = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Longitude;
                    GodownNum = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].GodownNum;
                    Khasranum = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Khasranum;
                    Rakwanum = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Rakwanum;
                    TehshilID = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].TehshilID;
                    VillageName = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].VillageName;
                    Org_Name = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Org_Name;
                    GInchargeName = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].GInchargeName;
                    GInchargeAddress = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].GInchargeAddress;
                    GInchargeMobile = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].GInchargeMobile;
                    GInchargeEmail = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].GInchargeEmail;
                    WeightmentType = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].WeightmentType;
                    LicIssueDate = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].LicIssueDate;
                    Godown_Reg_No = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Godown_Reg_No;
                    IsActive = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].IsActive;
                    LR_TehsilCode = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].LR_TehsilCode;
                    LR_VillageCode = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].LR_VillageCode;
                    Lenght = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Lenght;
                    Width = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Width;
                    Height = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Height;
                    Premise_capacity = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Premise_capacity;
                    Closing_Balance = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Closing_Balance;
                    JVS_RegNo = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].JVS_RegNo;
                    LicOwnerNM = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].LicOwnerNM;
                    LicCapacity = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].LicCapacity;
                    WH_Name = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].WH_Name;
                    WH_Type = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].WH_Type;
                    Verify_By = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Verify_By;
                    Verify_Date = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Verify_Date;
                    WHID = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].WHID;
                    //Maintain_By = myDeserializedClass.tbl_MetaData_GODOWN_2018[i].Maintain_By;

                    saveMetaData(Godown_ID, State_Id, District_Id, Depotid, Godown_Name, Godown_Formation_Date, Godown_Updation_Date, Godown_Capacity, Remarks, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, DeletedBy, DeletedDate, Hired_Type, Storage_Type, Godown_Scientific_Capacity, Godown_APN, Godown_Email, Godown_Mobile, Godown_Address, Branchid, LicNum, LicDate, PAN, Bank_ID, AccNo, IFSC_Code, Bank_Add, Latitude, Longitude, GodownNum, Khasranum, Rakwanum, TehshilID, VillageName, Org_Name, GInchargeName, GInchargeAddress, GInchargeMobile, GInchargeEmail, WeightmentType, LicIssueDate, Godown_Reg_No, IsActive, LR_TehsilCode, LR_VillageCode, Lenght, Width, Height, Premise_capacity, Closing_Balance, JVS_RegNo, LicOwnerNM, LicCapacity, WH_Name, WH_Type, Verify_By, Verify_Date, WHID);

                }
            }

            else
            {
                msg.Text = "Selected Table is not found";
            }
        }
        public void save(string Depositor_WHR_Id, string State_Id, string District_Id, string Depotid, int Commodity_Id, string Category_Id, string Whr_No, string Depositor_Name, string Date_of_Deposit, int TotalBags_Received, double Total_Qty_Received, string Mode_of_weighment, string BeamScale_LWB, double AvgMoisture_Content, string Lot_No, double MktValue_of_Commodity, string Arrival_Source, string WHR_Issue_Date, string CreatedBy, string CreatedDate, string UpdatedBy, string UpdatedDate, string DeletedBy, string DeletedDate, string MadeUpBags, string Client_IP, double AvgMoisture_Content_To, string Did, string CropYear, string Remark, string SangrahadDate, string LicenseNo, string LicenseDate, string wday, string wmon, string wyear, string BranchID, string DepositorID, string GodownID, string Gid, string Delete_Flag, string New_Column_10092022)
        {
            try
            { 
                string cs = System.Configuration.ConfigurationManager.ConnectionStrings["constrRepli"].ConnectionString;

                SqlConnection con = new SqlConnection(cs);

                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_storage_Depositor_WHR_Relation (Depositor_WHR_Id, State_Id, District_Id, Depotid, Commodity_Id, Category_Id, Whr_No, Depositor_Name, Date_of_Deposit, TotalBags_Received,Total_Qty_Received, Mode_of_weighment, BeamScale_LWB, AvgMoisture_Content, Lot_No, MktValue_of_Commodity, Arrival_Source, WHR_Issue_Date, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, DeletedBy, DeletedDate, MadeUpBags, Client_IP, AvgMoisture_Content_To, Did, CropYear, Remark,SangrahadDate, LicenseNo, LicenseDate, wday, wmon, wyear, BranchID, DepositorID, GodownID, Gid, Delete_Flag, New_Column_10092022) VALUES('" + Depositor_WHR_Id + "','" + State_Id + "' ,'" + District_Id + "' ,'" + Depotid + "' ,'" + Commodity_Id + "','" + Category_Id + "','" + Whr_No + "' ,'" + Depositor_Name + "' ,'" + Date_of_Deposit + "' ,'" + TotalBags_Received + "','" + Total_Qty_Received + "','" + Mode_of_weighment + "' ,'" + BeamScale_LWB + "' ,'" + AvgMoisture_Content + "' ,'" + Lot_No + "','" + MktValue_of_Commodity + "','" + Arrival_Source + "' ,'" + WHR_Issue_Date + "' ,'" + CreatedBy + "' ,'" + CreatedDate + "' ,'" + UpdatedBy + "' ,'" + UpdatedDate + "' ,'" + DeletedBy + "' ,'" + DeletedDate + "' ,'" + MadeUpBags + "' ,'" + Client_IP + "' ,'" + AvgMoisture_Content_To + "' ,'" + Did + "' ,'" + CropYear + "' ,'" + Remark + "' ,'" + SangrahadDate + "' ,'" + LicenseNo + "' ,'" + LicenseDate + "' ,'" + wday + "' ,'" + wmon + "' ,'" + wyear + "' ,'" + BranchID + "' ,'" + DepositorID + "' ,'" + GodownID + "' ,'" + Gid + "' ,'" + Delete_Flag + "' ,'" + New_Column_10092022 + "')", con);

                con.Open(); 

                cmd.ExecuteNonQuery();

                //Response.Redirect(Request.Url.AbsoluteUri);
                msg.Text = "Record Replicated Successfully.";
                con.Close();
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
     
        }

        public void saveData(string State_Id, string District_Id, string Depotid, string Godown_ID, string Stack_ID, string StorageReceipt_Id, int Bags, double Weight, string CreatedBy, string CreatedDate, string UpdatedBy, string UpdatedDate, string DeletedBy, string DeletedDate, string WHRId, string Status, string Branchid)
        {
            try
            { 
                string cs = System.Configuration.ConfigurationManager.ConnectionStrings["constrRepli"].ConnectionString;

                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_storage_Stacking_Details (State_Id, District_Id, Depotid, Godown_ID, Stack_ID, StorageReceipt_Id, Bags, Weight,CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, DeletedBy, DeletedDate, WHRId, Status, Branchid) VALUES('" + State_Id + "' ,'" + District_Id + "' ,'" + Depotid + "' ,'" + Godown_ID + "','" + Stack_ID + "','" + StorageReceipt_Id + "' ,'" + Bags + "' ,'" + Weight + "','" + CreatedBy + "' ,'" + CreatedDate + "' ,'" + UpdatedBy + "' ,'" + UpdatedDate + "' ,'" + DeletedBy + "' ,'" + DeletedDate + "' ,'" + WHRId + "' ,'" + Status + "' ,'" + Branchid + "')", con);

                con.Open();

                cmd.ExecuteNonQuery();

                msg.Text = "Record Replicated Successfully.";

                con.Close();
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
        }

        public void saveMetaData(string Godown_ID, string State_Id, string District_Id, string Depotid, string Godown_Name, string Godown_Formation_Date, string Godown_Updation_Date, string Godown_Capacity, string Remarks, string CreatedBy, string CreatedDate, string UpdatedBy, string UpdatedDate, string DeletedBy, string DeletedDate, string Hired_Type, string Storage_Type, string Godown_Scientific_Capacity, string Godown_APN, string Godown_Email, string Godown_Mobile, string Godown_Address, string Branchid, string LicNum, string LicDate, string PAN, string Bank_ID, string AccNo, string IFSC_Code, string Bank_Add, string Latitude, string Longitude, string GodownNum, string Khasranum, string Rakwanum, string TehshilID, string VillageName, string Org_Name, string GInchargeName, string GInchargeAddress, string GInchargeMobile, string GInchargeEmail, string WeightmentType, string LicIssueDate, string Godown_Reg_No, string IsActive, string LR_TehsilCode, string LR_VillageCode, string Lenght, string Width, string Height, string Premise_capacity, string Closing_Balance, string JVS_RegNo, string LicOwnerNM, string LicCapacity, string WH_Name, string WH_Type, string Verify_By, string Verify_Date, string WHID)
        {
            try
            { 
                string cs = System.Configuration.ConfigurationManager.ConnectionStrings["constrRepli"].ConnectionString;

                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_MetaData_GODOWN_2018 (Godown_ID, StateId, DistrictId, DepotId, Godown_Name, Godown_Formation_Date, Godown_Updation_Date, Godown_Capacity, Remarks, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, DeletedBy, DeletedDate, Hired_Type, Storage_Type, Godown_Scientific_Capacity, Godown_APN, Godown_Email, Godown_Mobile, Godown_Address, BranchID, LicNum, LicDate, PAN, Bank_ID, AccNo, IFSC_Code, Bank_Add, Latitude, Longitude, GodownNum, Khasranum, Rakwanum, TehshilID, VillageName, Org_Name, GInchargeName, GInchargeAddress, GInchargeMobile, GInchargeEmail, WeightmentType, LicIssueDate, Godown_Reg_No, IsActive, LR_TehsilCode, LR_VillageCode, Lenght, Width, Height, Premise_capacity, Closing_Balance, JVS_RegNo, LicOwnerNM, LicCapacity, WH_Name, WH_Type, Verify_By, Verify_Date, WHID) VALUES('" + Godown_ID + "','" + State_Id + "' ,'" + District_Id + "' ,'" + Depotid + "' ,'" + Godown_Name + "','" + Godown_Formation_Date + "','" + Godown_Updation_Date + "' ,'" + Godown_Capacity + "' ,'" + Remarks + "','" + CreatedBy + "' ,''" + CreatedDate + "'' ,'" + UpdatedBy + "' ,'" + UpdatedDate + "' ,'" + DeletedBy + "' ,'" + DeletedDate + "' ,'" + Hired_Type + "' ,'" + Storage_Type + "' ,'" + Godown_Scientific_Capacity + "','" + Godown_APN + "' ,'" + Godown_Email + "' ,'" + Godown_Mobile + "' ,'" + Godown_Address + "','" + Branchid + "','" + LicNum + "' ,'" + LicDate + "' ,'" + PAN + "','" + Bank_ID + "' ,'" + AccNo + "' ,'" + IFSC_Code + "' ,'" + Bank_Add + "' ,'" + Latitude + "' ,'" + Longitude + "' ,'" + GodownNum + "' ,'" + Khasranum + "' ,'" + Rakwanum + "','" + TehshilID + "' ,'" + VillageName + "' ,'" + Org_Name + "' ,'" + GInchargeName + "','" + GInchargeAddress + "','" + GInchargeMobile + "' ,'" + GInchargeEmail + "' ,'" + WeightmentType + "','" + LicIssueDate + "' ,'" + Godown_Reg_No + "' ,'" + IsActive + "' ,'" + LR_TehsilCode + "' ,'" + LR_VillageCode + "' ,'" + Lenght + "' ,'" + Width + "' ,'" + Height + "' ,'" + Premise_capacity + "' ,'" + Closing_Balance + "','" + JVS_RegNo + "','" + LicOwnerNM + "','" + LicCapacity + "','" + WH_Name + "','" + WH_Type + "','" + Verify_By + "','" + Verify_Date + "','" + WHID + "')", con);

                con.Open();
        
                cmd.ExecuteNonQuery();

                msg.Text = "Record Replicated Successfully.";

                con.Close();
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode);
            }
        }



        public class Root
        {
            public List<TblStorageDepositorWHRRelation> tbl_storage_Depositor_WHR_Relation { get; set; }
        }
        public class TblStorageDepositorWHRRelation
        {
            public string Depositor_WHR_Id { get; set; }
            public string State_Id { get; set; }
            public string District_Id { get; set; }
            public string Depotid { get; set; }
            public int Commodity_Id { get; set; }
            public string Category_Id { get; set; }
            public string Whr_No { get; set; }
            public string Depositor_Name { get; set; }
            public string Date_of_Deposit { get; set; }
            public int TotalBags_Received { get; set; }
            public double Total_Qty_Received { get; set; }
            public string Mode_of_weighment { get; set; }
            public string BeamScale_LWB { get; set; }
            public double AvgMoisture_Content { get; set; }
            public string Lot_No { get; set; }
            public double MktValue_of_Commodity { get; set; }
            public string Arrival_Source { get; set; }
            public string WHR_Issue_Date { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string UpdatedBy { get; set; }
            public string UpdatedDate { get; set; }
            public string DeletedBy { get; set; }
            public string DeletedDate { get; set; }
            public string MadeUpBags { get; set; }
            public string Client_IP { get; set; }
            public double AvgMoisture_Content_To { get; set; }
            public string Did { get; set; }
            public string CropYear { get; set; }
            public string Remark { get; set; }
            public string SangrahadDate { get; set; }
            public string LicenseNo { get; set; }
            public string LicenseDate { get; set; }
            public string wday { get; set; }
            public string wmon { get; set; }
            public string wyear { get; set; }
            public string BranchID { get; set; }
            public string DepositorID { get; set; }
            public string GodownID { get; set; }
            public string Gid { get; set; }
            public string Delete_Flag { get; set; }
            public string New_Column_10092022 { get; set; }
        }

        // =========TblStorageStackingDetail
        public class RootStacking
        {
            public List<TblStorageStackingDetail> tbl_storage_Stacking_Details { get; set; }
        }
        public class TblStorageStackingDetail
        {
            public string State_Id { get; set; }
            public string District_Id { get; set; }
            public string Depotid { get; set; }
            public string Godown_ID { get; set; }
            public string Stack_ID { get; set; }
            public string StorageReceipt_Id { get; set; }
            public int Bags { get; set; }
            public double Weight { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string UpdatedBy { get; set; }
            public string UpdatedDate { get; set; }
            public string DeletedBy { get; set; }
            public string DeletedDate { get; set; }
            public string WHRId { get; set; }
            public string Status { get; set; }
            public string Branchid { get; set; }

        }

        //============tbl_MetaData_GODOWN_2018
        public class RootMetaDataGodown
        {
            public List<TblMetaDataGODOWN2018> tbl_MetaData_GODOWN_2018 { get; set; }
        }

        public class TblMetaDataGODOWN2018
        {
            public string Godown_ID { get; set; }
            public string StateId { get; set; }
            public string DistrictId { get; set; }
            public string DepotId { get; set; }
            public string Godown_Name { get; set; }
            public string Godown_Formation_Date { get; set; }
            public string Godown_Updation_Date { get; set; }
            public string Godown_Capacity { get; set; }
            public string Remarks { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string UpdatedBy { get; set; }
            public string UpdatedDate { get; set; }
            public string DeletedBy { get; set; }
            public string DeletedDate { get; set; }
            public string Hired_Type { get; set; }
            public string Storage_Type { get; set; }
            public string Godown_Scientific_Capacity { get; set; }
            public string Godown_APN { get; set; }
            public string Godown_Email { get; set; }
            public string Godown_Mobile { get; set; }
            public string Godown_Address { get; set; }
            public string BranchID { get; set; }
            public string LicNum { get; set; }
            public string LicDate { get; set; }
            public string PAN { get; set; }
            public string Bank_ID { get; set; }
            public string AccNo { get; set; }
            public string IFSC_Code { get; set; }
            public string Bank_Add { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string GodownNum { get; set; }
            public string Khasranum { get; set; }
            public string Rakwanum { get; set; }
            public string TehshilID { get; set; }
            public string VillageName { get; set; }
            public string Org_Name { get; set; }
            public string GInchargeName { get; set; }
            public string GInchargeAddress { get; set; }
            public string GInchargeMobile { get; set; }
            public string GInchargeEmail { get; set; }
            public string WeightmentType { get; set; }
            public string LicIssueDate { get; set; }
            public string Godown_Reg_No { get; set; }
            public string IsActive { get; set; }
            public string LR_TehsilCode { get; set; }
            public string LR_VillageCode { get; set; }
            public string Lenght { get; set; }
            public string Width { get; set; }
            public string Height { get; set; }
            public string Premise_capacity { get; set; }
            public string Closing_Balance { get; set; }
            public string JVS_RegNo { get; set; }
            public string LicOwnerNM { get; set; }
            public string LicCapacity { get; set; }
            public string WH_Name { get; set; }
            public string WH_Type { get; set; }
            public string Verify_By { get; set; }
            public string Verify_Date { get; set; }
            public string WHID { get; set; }
       
        }

    }

}