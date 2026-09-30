<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Report_Region.aspx.cs" Inherits="Region_Report_Region" Title="States Report::" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 1050px; margin-left: 0px">
        <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 1px; border-color: Green"
            border="1px">
            <tr style="background-color: #0bb6e6; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="lblRegionReports" runat="server" Text="State Report Region Wise" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ValidationGroup="link1"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Commodity Details(MPWLC)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Warehouse Capacity and Utilization(MPWLC)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Warehouse Depositors Details(MPWLC)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton18" runat="server" OnClick="LinkButton18_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Godown wise stack wise Current Stock position</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Registered Operator Details (Issue Centre)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Registered Operator Details (District Manager)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Operators Login  Details</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Warehouse Capacity and Utilization(MPWLC DistrictWise)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton8_Click" ValidationGroup="link1"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Commodity Details(MPWLC DistrictWise)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton9" runat="server" OnClick="LinkButton9_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Warehouse Depositors Details(MPWLC DistrictWise)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton10" runat="server" OnClick="LinkButton10_Click" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Warehouse Capacity and Utilization(MPWLC) between two dates</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">12.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton11" runat="server" OnClick="LinkButton11_Click" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Warehouse Capacity and Utilization(till now)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">13.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton12" runat="server" OnClick="LinkButton12_Click" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Current Stock Position at Branch (GMap)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">14.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton13" runat="server" ValidationGroup="lik2" OnClick="LinkButton13_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Warehouse Scientific and Maximum Capacity and Utilization(till now)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">15.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton14" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton14_Click">RegionWise Commodity Details</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">16.</span>
                </td>
                <td style="width: 36px; height: 18px;">
                    &nbsp;
                    <asp:LinkButton ID="lblDeleteAllWHRDOReport" runat="server" Font-Bold="true" Font-Size="10pt"
                        ForeColor="navy" OnClick="lblDeleteAllWHRDOReport_Click" ValidationGroup="lik2"
                        Width="272px">Delete WHR,DO,Gatepass Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">17.</span>
                </td>
                <td style="width: 36px; height: 18px;">
                    &nbsp;
                    <asp:LinkButton ID="lnk_allentryReport" runat="server" Font-Bold="true" Font-Size="10pt"
                        ForeColor="navy" OnClick="lnk_allentryReport_Click" ValidationGroup="lik2" Width="272px">DistrictWise & BranchWise Entry</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">18.</span>
                </td>
                <td style="width: 36px">
                    &nbsp;
                    <asp:LinkButton ID="lnkdelWHRCompare" runat="server" Font-Bold="true" Font-Size="10pt"
                        ForeColor="navy" OnClick="lnkdelWHRCompare_Click" ValidationGroup="lik2" Width="270px">Delete WHR Compare Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">17.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton15" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="~/Reports/States/Rpt_Statewhrdetail_drilldown.aspx" Font-Bold="true"
                        Font-Size="10pt">RegionWise WHR,DO,Receipt Details</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">18.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton16" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="~/Reports/States/Rpt_Commoditywise_whr.aspx" Font-Bold="true" Font-Size="10pt">CommodityWise & Branchwise WHR Details</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">19.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton17" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="~/Reports/States/Rptworkstatus.aspx" Font-Bold="true" Font-Size="10pt">Districtwise Work Status Repot</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">20.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton19" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="~/Reports/States/Rpt_Deliveryissuedetails.aspx" Font-Bold="true"
                        Font-Size="10pt">Godownwise Receipt & Issue Details Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">21.</span>
                </td>
                <td style="width: 36px">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton21" runat="server" Font-Bold="true" Font-Size="10pt"
                        ForeColor="navy" OnClick="LinkButton21_Click" ValidationGroup="lik2" Width="428px">District wise and Branch wise Pending Gatepass Entry</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">22.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton20" runat="server" OnClick="LinkButton20_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">District wise stack wise Current Stock position</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">23.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton22" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="~/Reports/States/Rpt_Notworking.aspx" Font-Bold="true" Font-Size="10pt">Districtwise Branches 'Not Started Working' Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">22.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton23" runat="server" OnClick="LinkButton23_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Pending Delivery Order Gatepass Entry(with operator Details)</asp:LinkButton>
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">24.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton24" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="~/Reports/States/RptAcceptancewhr.aspx" Font-Bold="true" Font-Size="10pt">Districtwise Acceptance with WHR No. Details Report</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

