<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master"
    AutoEventWireup="true" CodeFile="New_Storage_Capacity_Reports.aspx.cs" Inherits="IssueCenterLevel_Storage_Report_Region"
    Title="Report Pages States" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 1050px; margin-left: 0px">
        <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 1px; border-color: Green"
            border="1px">
            <tr style="background-color: #0bb6e6; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="lblRegionReports" runat="server" Text="State Report " ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>

            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Godown_Wise_Qty_Available_Last_10_Year.aspx" target="_blank">Godown Wise Stock Position</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_District_Wise_Qty_Available_Last_10_Year_All_Commodity.aspx" target="_blank">District Wise Stock Position for All Commodity</a></td>

            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Branch_Wise_Qty_Available_Last_10_Year.aspx" target="_blank">Branch Wise Last 10 Year Stock Position</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_District_Wise_Qty_Available_Last_10_Year.aspx" target="_blank">District Wise Last 10 Year Stock Position</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_District_Wise_Qty_Available_Last_10_Year_For_Storage_Type.aspx" target="_blank">District Storage Wise Last 10 Year Stock Position</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Region_Wise_Qty_Available_Last_10_Year_For_Storage_Type.aspx" target="_blank">Region Storage Wise Last 10 Year Stock Position</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Branch_Wise_Qty_Available_Last_10_Year_Date_Wise.aspx" target="_blank">Branch Commodity,CropYear,Date Wise Stock Position</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Godown_Wise_Qty_Available_Last_10_Year_Date_Wise.aspx" target="_blank">Godown Commodity,CropYear,Date Wise Stock Position</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_District_Wise_Qty_Avl_COmplete_JVS.aspx" target="_blank">Complete JVS District Wise Stock Position</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Godown_Wise_Qty_Available_Last_10_Year_Date_Wise_SiloBags.aspx" target="_blank">Silo Bags Stock Position Crop Year Wise</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Godown_Wise_Qty_Available_Last_10_Year_Date_Wise_CAP.aspx" target="_blank">CAP Stock Position Crop Year Wise</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">12.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Godown_Capacity.aspx" target="_blank">Capacity wise Godown List</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">13.</span></td>
                <td><a href="../../Inspections/Reports/ViewUpdateGodownDetails2023.aspx" target="_blank">View Update Godown List 2023</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">14.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_District_Wise_Qty_Available_Last_10_Year_Date_Wise.aspx" target="_blank">District Commodity,CropYear,Date Wise Stock Position</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">15.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Godown_Wise_Qty_Available_Last_10_Year_Date_Wise_HireType.aspx" target="_blank">Godown Commodity,CropYear,Hired Type ,Storage,Date Wise Stock Position</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">16.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_DistrictWise_HiredTypeWise_GodownCapacity.aspx" target="_blank">District Hired Type Godown Capacity</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">17.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Godown_Wise_Qty_Available_Last_10_Year_Date_Wise_SteelSilo.aspx" target="_blank">Steel Silo</a></td>
            </tr>
            <tr>
    <td style="width: 10px" align="center">
        <span style="color: Navy; font-weight: bold; font-size: 10pt">70.</span></td>
    <td><a href="../../Inspections/Reports/Rpt_Godown_Wise_Qty_Available_Last_10_Year_Date_Wise_JVS_Godown.aspx" target="_blank">JVS Godown Stock Position</a></td>
</tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">18.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Owned_Godown_Details.aspx" target="_blank">Owned,PVT.PEG,BOT-AUB,CWC,Steel Silo,Hired,Tribal Schemes Godown Capacity and available Stock Position in M.T.</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">19.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_District_Wise_Qty_Avl_Complete_JVS_Owned.aspx" target="_blank">Region,District wise Total Capacity,Available Capacity,Vacant Capacity Report</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">20.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_District_Wise_FAQ_Non_FAQ_DCC_Stock_position.aspx" target="_blank">District wise FAQ , Non-FAQ and DCC Stock position</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">21.</span></td>
                <td><a href="../../StatePages/Rpt_Dist_Yearwise_StockPosition.aspx" target="_blank">Report for Stock Position District wise-Year wise</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">22.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_AvlQty_Vacant_and_Godown_Capacity.aspx" target="_blank">District Wise Capacity,Avl. Stock and Vacant Capacity</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">23.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Date_Wise_All_Crop_wise_Balance_Details_District_Wise_Hired.aspx" target="_blank">District Wise Hired Type Wise Capacity in MT</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">24.</span></td>
                <td><a href="../../Reports/States/Rpt_Region_Wise_Stock_Position_Fill_Detail_By_BM.aspx" target="_blank">Region Crop Year Wise Stock Entry By Branch Manager (Review Report)</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">25.</span></td>
                <td><a href="../../Reports/States/Rpt_District_Wise_Stock_Position_Fill_Detail_By_BM.aspx" target="_blank">District Crop Year Wise Stock Entry By Branch Manager (Review Report)</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">26.</span></td>
                <td><a href="../../Reports/States/Rpt_District_CropYear_Wise_StockPosition.aspx" target="_blank">Report For Review of District , Crop Year Wise, Commodity Wise Stock Position Report (Month wise in MT)</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">27.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_District_Wise_DCC_Stock_Position.aspx" target="_blank">Godown wise DCC Stock Position Entry by BM</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">28.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_District_Wise_DCC_Stock_Position_Entry_by_BM.aspx" target="_blank">District Crop Year wise DCC Stock Position Entry by BM</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">29.</span></td>
                <td><a href="../../Reports/States/Rpt_Region_FY_DepositorWise_PaymentStatus.aspx" target="_blank">जमाकर्ता से भुगतान की स्थिति (प्रोफोर्मा-अ)</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">30.</span></td>
                <td><a href="../../Reports/States/Rpt_Region_FY_GodownWise_PaymentStatus.aspx" target="_blank">निजी गोदामों के भुगतान की स्थिति (प्रोफोर्मा-ब)</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">31.</span></td>
                <td><a href="../../Reports/States/Rpt_Godown_Wise_Payment_Status_For_Owned_Godown.aspx" target="_blank">Owned Godown Wise Payment Status</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">32.</span></td>
                <td><a href="../../Reports/States/Rpt_District_Wise_Payment_status_Godown_Type_Wise.aspx" target="_blank">Godown Type Wise Payment Status</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">33.</span></td>
                <td><a href="../../Reports/States/Rpt_Payment_Status_OF_Finacial_Year_hired_Type_Wise.aspx" target="_blank">Financial Year wise Payment Status</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">34.</span></td>
                <td><a href="../../Reports/States/Rpt_Region_FY_DepositorWise_PaymentStatusWithDistrict.aspx" target="_blank">जमाकर्ता से भुगतान की स्थिति (प्रोफोर्मा-अ)-जिलावार-शाखावार</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">35.</span></td>
                <td><a href="../../Reports/States/Rpt_Region_FY_GodownWise_PaymentStatusWithDistrict.aspx" target="_blank">निजी गोदामों के भुगतान की स्थिति (प्रोफोर्मा-ब)-जिलावार-शाखावार</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">36.</span></td>
                <td><a href="../../Reports/States/Region_Wise_Payment_Status.aspx" target="_blank">संभागवार जमाकर्ता से भुगतान की स्थिति (प्रोफोर्मा-अ)</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">37.</span></td>
                <td><a href="../../Reports/States/Rpt_FinancialYear_Wise_Payment_Status_Offline.aspx" target="_blank">संभागवार जमाकर्ता वर्ष वार भुगतान की स्थिति (प्रोफोर्मा-अ)</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">38.</span></td>
                <td><a href="../../statePages/Rpt_District_Wise_HiredTypeWise_Capacity.aspx" target="_blank">District Godown Type Wise Godown Number and Capcity in LMT</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">39.</span></td>
                <td><a href="../../Reports/States/Rpt_District_CropYear_Depositorwise_StockPosition_NAFED.aspx" target="_blank">Report for Stock Availability Position District, Depositor, CropYear, Commodity wise for NAFED/MARKFED/NCCF(Quantity in MT)</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">40.</span></td>
                <td><a href="../../Reports/States/GetGodownDetails_Region_HiredWise.aspx" target="_blank">Report for Godown Details Region wise (Capacity in MT)</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">41.</span></td>
                <td><a href="../../statePages/GetGodownDetails_DistrictBranchWise.aspx" target="_blank">Report for Godown Details District Branch wise (Capacity in MT)</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">42.</span></td>
                <td><a href="../../Reports/States/Rpt_Region_FY_DepositorWise_PaymentStatus_JVS.aspx" target="_blank">Report for Depositor wise Payment Status with JVS Payment From Branch</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">43.</span></td>
                <td><a href="../../Reports/States/Rpt_Region_FY_DepositorWise_JVSOwnerPaymentStatus_RM.aspx" target="_blank">Report for Depositor wise Payment Status with JVS Payment from Region</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">44.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_District_Wise_FAQ_Non_FAQ_DCC_Stock_position_New.aspx" target="_blank">FAQ,Non-FAQ,DCC and other Data details (Real Time)</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">44.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_District_Wise_Qty_Available_Last_10_Year_Date_Wise_New.aspx" target="_blank">District Crop Year Wise Stock Position(Drill Down)</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">45.</span></td>
                <td><a href="../../Reports/States/Rpt_Payment_Status_OF_Godown_Type_Wise.aspx" target="_blank">District Godown Type Payment Status</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">46.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_District_Wise_Stock_Position_Without_DCC.aspx" target="_blank">District, Commodity Wise Stock Position Without DCC Stock</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">47.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Region_Wise_DCC_Stock_Position.aspx" target="_blank">क्षेत्रीय कार्यालय वार डीसीसी ,कीटग्रस्‍त स्‍कंध ,आटा फारमेशन,फारेन मेटर एवं  खराब बारदाने में भंडारित स्‍कंध की शेष मात्रा</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">48.</span></td>
                <td><a href="../../Reports/States/Rpt_DistrictAll_CropYear_Depositorwise_StockPosition_NAFED.aspx" target="_blank">Summary Report for Stock Availability Position Depositor, CropYear, Commodity wise for NAFED/MARKFED/NCCF(Quantity in MT)</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">49.</span></td>
                <td><a href="../../Reports/States/Rpt_DistrictAll_CropYear_Depositorwise_StockPosition_NAFED_Dlahan_Tilhan.aspx" target="_blank">Dlahan, Tilhan Summary Report for Stock Availability for NAFED/MARKFED/NCCF(Quantity in MT)</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">50.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Region_Commodity_Wise_Stock_Position.aspx" target="_blank">Division Distirct Wise All Commodity Stock Position</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">51.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Region_Wise_Stock_entry_by_BM.aspx" target="_blank">Division Wise All Commodity Stock Position (Entry by BM)</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">52.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_All_Type_Wise_Stock_entry_by_BM.aspx" target="_blank">All Type Wise Stock Position (Entry by BM)</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">53.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_All_Type_Wise_Stock_Online.aspx" target="_blank">All Type Wise Stock Position (Online)</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">54.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Region_Hired_type_wise_Godown_Details.aspx" target="_blank">Division,District,Branch,Godown And Hired Type Godown Capacity & Avl. Stock</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">55.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Branch_Commodity_Wise_Stock_Position_For_Spicial_PV_DistrictWIse.aspx" target="_blank">District,Godown Wise Stock Position</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">56.</span></td>
                <td><a href="../../StatePages/Check_Godown_Status_For_All.aspx" target="_blank">Check Godown Details by Godown ID(All Information)</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">57.</span></td>
                <td><a href="../../StatePages/Acceptance_Wise_WHR_Details_For_All.aspx" target="_blank">Acceptance Wise WHR Details</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">58.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Godown_Wise_Qty_Available_and_VacantCapacity.aspx" target="_blank">Godown Wise Commodity wise Capcity ,Avl. Stock and Vacant Capacity in MT</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">59.</span></td>
                <td><a href="../../Reports/States/Rpt_Godown_Depositor_Wise_Stock_position.aspx" target="_blank">District, Godown, Depositor, Commodity Wise Stock Position in MT</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">60.</span></td>
                <td><a href="../../Reports/States/District_Wise_Depositor_Commodity_Wise_Stock_Position.aspx" target="_blank">Region, District, Commodity Wise Stock Position in MT</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">61.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Date_Region_Wise_Stock_position.aspx" target="_blank">Region, District, Depositor, Commodity Wise Stock Position in MT</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">62.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_GodownType_Wise_Capacity_Vacant_Details.aspx" target="_blank">Godown Wise Capacity , Avl. Stock and Vacant Details</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">63.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Depositor_Wise_Available_Qty_Date_Wise.aspx" target="_blank">Depositor Wise Stock Position in MT</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">64.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Godown_Wise_Update_Capcity_diffirence.aspx" target="_blank">Godown Wise Update Diffirence Capacity in MT</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">65.</span></td>
                <td><a href="../../Reports/States/Rpt_Godown_CropYear_Depositorwise_StockPosition.aspx" target="_blank">District,Godown,Depositor,CropYear Wise,Commodity Wise Stock Position Report in MT</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">66.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_District_Wise_Qty_Available_Last_10_Year_Date_Wise_AND_Depositer_Wise.aspx" target="_blank">Depositor,Commodity Wise Stock Position Report in MT</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">67.</span></td>
                <td><a href="../../reports/States/Godown_Mapping_With_WeightBridge.aspx" target="_blank">Godown Mapping With Weighbridge</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">68.</span></td>
                <td><a href="../../StatePages/DepositerCommodityWiseOfflineBill.aspx" target="_blank">Depositer Commodity Wise Offline Payment Status</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">69.</span></td>
                <td><a href="../../StatePages/Compare_Old_New_Depositor_Entry_Report.aspx" target="_blank">Compare Old vs New Depositor Entry Report</a></td>
            </tr>
             <tr>
     <td style="width: 10px" align="center">
         <span style="color: Navy; font-weight: bold; font-size: 10pt">70.</span></td>
     <td><a href="../../Inspections/Reports/Rpt_District_Wise_Qty_Available_Last_10_Year_Date_Wise_Only_Covered_Godown.aspx" target="_blank">District Commodity,CropYear,Date Wise Stock Position (Only Covered Godown)</a></td>
 </tr>

                        <tr>
    <td style="width: 10px" align="center">
        <span style="color: Navy; font-weight: bold; font-size: 10pt">71.</span></td>
    <td><a href="../../StatePages/Godown_Wise_Capacity_Classification.aspx" target="_blank">Godown Wise Classified Capacity</a></td>
</tr>

                                    <tr>
    <td style="width: 10px" align="center">
        <span style="color: Navy; font-weight: bold; font-size: 10pt">72.</span></td>
    <td><a href="../../StatePages/Godown_Wise_Report_Sub_Total.aspx" target="_blank">Godown Capacity And Sub Total</a></td>
</tr>
                                    <tr>
    <td style="width: 10px" align="center">
        <span style="color: Navy; font-weight: bold; font-size: 10pt">73.</span></td>
    <td><a href="../../StatePages/Region_Wise_Moisture_Qty_Report.aspx" target="_blank">Region Wise Moisture Qty Report</a></td>
</tr>
            
                                                <tr>
    <td style="width: 10px" align="center">
        <span style="color: Navy; font-weight: bold; font-size: 10pt">74.</span></td>
    <td><a href="../../StatePages/Depositor_Wise_CropYear_Wise_Commodity_Wise_Stock_Position.aspx" target="_blank">Depositor Wise CropYear Wise Commodity Wise Stock Position</a></td>
</tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">75.</span></td>
                <td><a href="../../Reports/States/Rpt_Godown_Depositor_Wise_Stock_position_New.aspx" target="_blank">Godow Wise Customize Report</a></td>
            </tr>

            
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">76.</span></td>
                <td><a href="../../Reports/States/Get_PVT_Depositor_Stock_Balance_District_Wise.aspx" target="_blank">District Wise Private (Cultivator) Stock Balance Position In M.T.</a></td>
            </tr>

            
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">77.</span></td>
                <td><a href="../../Reports/States/Get_PVT_Depositor_Stock_Balance.aspx" target="_blank">District, Branch, Godow Wise Private (Cultivator) Stock Balance Position In M.T.</a></td>
            </tr>

            
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">78.</span></td>
                <td><a href="../../StatePages/Get_Region_CropYear_Wise_Stock_Position.aspx" target="_blank">Get_Region Crop Year Wise Stock Position</a></td>
            </tr>

        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <script language="JavaScript1.2">
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 10px;
            height: 37px;
        }

        .auto-style2 {
            height: 37px;
        }
    </style>
</asp:Content>

