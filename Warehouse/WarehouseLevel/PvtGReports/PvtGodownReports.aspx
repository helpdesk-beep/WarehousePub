<%@ Page Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="PvtGodownReports.aspx.cs" Inherits="WarehouseLevel_PvtGReports_PvtGodownReports" Title="Pvt Godown Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="Text/javascript">
        var theChild;

        function OpenWindow() {
            if (theChild != null) {
                if (!theChild.closed) {
                    theChild.close();
                }
            }
            url = 'ReportViewer_Depot.aspx';
            theChild = window.open(url, 'theChild', 'height=800,width=780,top=1,left=1,resizable=1');
        }
    </script>

    <div style="width: 1000px; margin-left: 0px">
        <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 1px; border-color: Green"
            border="1px">
            <tr style="background-color: #0bb6e6; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label3" runat="server" Text="New Reports" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton17" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton17_Click">Godown Wise WHR Balance Details (With CropYear)</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton18" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton18_Click">FIFO Wise Available Stock Details(For Issue Gate Pass)</asp:LinkButton></td>
            </tr>
            <tr style="background-color: #0bb6e6; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label1" runat="server" Text="MPSCSC Billing Reports" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton16" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton16_Click">Billing Status from August</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>
                    <a href="../../WarehouseLevel/Rpt_Godown_And_Month_Wise_Pending_Amount.aspx" class="ancker" target="_blank">Pending Storage Bills Report for Payment</a>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong>
                </td>
                <td>
                    <a href="../../WarehouseLevel/Payment_Credit_From_MPWLC_To_Godown.aspx" class="ancker" target="_blank">Payment Credit From MPWLC To Godown</a>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong>
                </td>
                <td>
                    <a href="../Rent_Bill/SteelSilo/Rpt_Steel_Silo_Bill_Details.aspx" class="ancker" target="_blank">Still Statement For Silo</a>
                </td>
            </tr>
            <tr style="background-color: #0bb6e6; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label4" runat="server" Text="NAFED Billing Reports" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>
                    <a href="../../WarehouseLevel/Nafed_Godown_Bill_Wise_Payment_Status.aspx" class="ancker" target="_blank">Nafed Payment Credit From MPWLC To Godown</a>
                </td>
            </tr>
            <tr style="background-color: #0bb6e6; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="lblStorageReports" runat="server" Text="Godown Storage Reports" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton2" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton2_Click">WHR Wise Capacity & Utilization</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton3" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton3_Click">Commodity Wise Stock Summary</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton1" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt"
                    OnClick="LinkButton1_Click">Godownwise & (WHR)wise Balance Details Report</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton4" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton4_Click">Delivery Order Wise Issue Detail</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">5.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton5" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton5_Click">Commodity Wise Opening Closing B/W Two Dates</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">6.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton6" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton6_Click">Godown Wise Stack Detail</asp:LinkButton></td>
            </tr>




            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">7.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton7" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton7_Click">Wheat Procurement 2017-18</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">8.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton8" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton8_Click">WHR Details From Depositor Form</asp:LinkButton></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">9.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton9" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton9_Click">Commodity Wise - WHR Details</asp:LinkButton></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">10.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton10" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton10_Click">CropYear Wise - WHR Details</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">11.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton11" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton11_Click">Stack Wise -Stock status</asp:LinkButton></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">12.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton12" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton12_Click">Gatepass Details Between Two Dates</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">13.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton13" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton13_Click">DateWise - WHR Details (MPSCSC)</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">14.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton14" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton14_Click">Pirnt Delivery Order</asp:LinkButton></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">15.</span></strong>
                </td>
                <td>&nbsp;<asp:LinkButton ID="LinkButton15" runat="server"
                    ForeColor="navy" Font-Bold="true" Font-Size="10pt"
                    OnClick="LinkButton15_Click">Procurement Report 2018-19</asp:LinkButton></td>
            </tr>



            <tr>
                <td>
                    <asp:Label ID="District" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

