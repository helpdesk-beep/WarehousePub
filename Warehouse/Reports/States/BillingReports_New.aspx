<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master"
    AutoEventWireup="true" CodeFile="BillingReports_New.aspx.cs" Inherits="IssueCenterLevel_Storage_BillingReports_New"
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
                <td><a href="../../Reports/States/Rpt_GodownFinancialYearWisePaymentStatus.aspx" target="_blank">Payment Status Godown Type,Financial Year Month Wise Submision,Receiving and Pending Amount in (Cr.)</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td><a href="../../Reports/States/Rpt_Get_Recived_and_Pending_Amount_From_Aug_At_MPSCSC.aspx" target="_blank">Region District Wise Pendancy on MPSCSC</a></td>

            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td><a href="../../Reports/States/Rpt_JVS_Payment_Against_Received_Payment_From_MPSCSC.aspx" target="_blank">Date/UTR Wise Payment Status (Receiving and Pay to Godown) at Various Level</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td><a href="../../Reports/States/Rpt_Payment_Status_OF_Financial_Year_hired_Type_MPSCSC.aspx" target="_blank">Finacial Year Wise Payment Status</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td><a href="../../Reports/States/Rpt_Region_Wise_Payment_status_For_JVS.aspx" target="_blank">Region Financial Year Wise Payment Status</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Region_Wise_DSC_Payment_details.aspx" target="_blank">SC,Rent Bill Payment, DSC, BM, Godown and DM MPSCSC/HO MPSCSC Payment Status</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span></td>
                <td><a href="../../Reports/States/Rpt_Date_Wise_Payment_Received_From_MPSCSC.aspx" target="_blank">Date wise Payment Received From MPSCSC</a></td>
            </tr>
           
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Godown_Wise_Payment_Status_From_MPSCSC.aspx" target="_blank">Godown Wise/Bill Wise Pendency At MPSCSC</a></td>
            </tr>
              <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span></td>
                <td><a href="../../Reports/States/Rpt_Pending_Bill_Details_Summary.aspx" target="_blank">Pending Bills For Generation at Branch Level(SC)</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span></td>
                <td><a href="../../Reports/States/Rpt_District_Wise_Pending_Bill_Details_Summary.aspx" target="_blank">District Wise SC Pending Bills For Generation at Branch</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span></td>
                <td><a href="../../Reports/States/Rpt_JVS_Payment_Against_Received_Payment_From_MPSCSC_Region_Wise.aspx" target="_blank">Region District Wise Pendency at Various Level against Received Payment from MPSCSC(August 2021)</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">12.</span></td>
                <td><a href="../../Reports/States/Rpt_Region_Wise_JVS_Payment_Against_Received_Payment_From_MPSCSC.aspx" target="_blank">Region Wise Pendency at Various Level against Received Payment from MPSCSC(August 2021)</a></td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">13.</span></td>
                <td><a href="../../StatePages/Rpt_Update_Loss_Gain_by_RM.aspx" target="_blank">Region Wise Loss Gain Entry BY RM</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">14.</span></td>
                <td><a href="../../Reports/States/GetDistrict_Wise_JVS_Payment_Against_Received_Payment_Status.aspx" target="_blank">District Wise JVS Payment Against Received Payment Status</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">15.</span></td>
                <td><a href="../../Reports/States/Rpt_Penging_Rent_Bill_Generation_Against_Received_Amount_From_MPSCSC.aspx" target="_blank">Penging Rent Bill Generation Against Received Amount From MPSCSC</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">16.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_NAFED_Payment_Status_All.aspx" target="_blank">NAFED Payment Status From 1st Apr. 2024</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">17.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_NAFED_Payment_Summary.aspx" target="_blank">Region, District Wise NAFED Payment Summary From 1st Apr. 2024(Only Pvt. Warehouse)</a></td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">18.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Get_Rent_Bill_Generation_Pendancy_SUMMARY.aspx" target="_blank">Region, District, Wise Pending Rent Bill Generation From Aug. 2020</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">19.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_District_Wise_NAFED_Payment_Summary.aspx" target="_blank">District Wise NAFED Payment Summary From 1st Apr. 2024(Only Pvt. Warehouse)</a></td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">20.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_NAFED_Payment_Summary_For_Review.aspx" target="_blank">Region Wise NAFED Summary For Review</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">21.</span></td>
                <td><a href="../../Inspections/Reports/Rpt_Region_Get_Rent_Bill_Generation_Pendancy_SUMMARy.aspx" target="_blank">Region Wise Pending Rent Bill Generation For Review</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">22.</span></td>
                <td><a href="../../Reports/States/Rpt_MPSCSC_Payment_Receive_and_Payto_Godown_Review.aspx" target="_blank">Region Wise Summary For Review MPSCSC</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">23.</span></td>
                <td><a href="../../StatePages/Rpt_StorageAndREnt_Bill_Pendency.aspx" target="_blank">Financial Year Wise Pending Bill Generation</a></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">24.</span></td>
                <td><a href="../../Reports/States/Get_Branch_Wise_JVS_Payment_Against_Received_Payment_Pending_Rent_Bill_For_Generation.aspx" target="_blank">Branch Wise Rent Bill Pending Against Receive Storage Bill From MPSCSC</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">25.</span></td>
                <td><a href="../../Reports/States/Rpt_District_Wise_Pending_Rent_Bill_Details_Summary.aspx" target="_blank">Year Wise Rent Bill Generation</a></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">26.</span></td>
                <td><a href="../../Reports/States/Rpt_Get_FinancialYear_Wise_Pending_Bill_For_Submission_to_MPSCSC.aspx" target="_blank">Pending Bill For Submission</a></td>
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

