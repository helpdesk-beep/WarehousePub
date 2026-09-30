<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="Branch_Report.aspx.cs" Inherits="IssueCenterLevel_Storage_Branch_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
                    <asp:Label ID="lblStorageReports" runat="server" Text="Storage Reports" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Daily Receipt and Release Register</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton12" runat="server" OnClick="LinkButton12_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Stack wise Register</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Depositor Ledger</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton13" runat="server" OnClick="LinkButton13_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Stock Register</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">5.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Delivery Order</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">6.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">WareHouse Receipt</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">7.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Stack wise Condition Report Register</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">8.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton8_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Daily Receipt/Issue of WHR Register( MPSCSC )</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">9.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton9" runat="server" OnClick="LinkButton9_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">GatePass Register</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">10.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton18" runat="server" OnClick="LinkButton18_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Godown wise stack wise Current Stock position</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">11.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton16" runat="server" OnClick="LinkButton16_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Stock Valuation Register</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">12.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton19" runat="server" OnClick="LinkButton19_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Daily Issue Commodity wise( MPSCSC )</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">13.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton14" runat="server" OnClick="LinkButton14_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Daily Receipt Commodity wise( MPSCSC )</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">14.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton15" runat="server" OnClick="LinkButton15_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">DO wise issue details( MPSCSC )</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">15.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton11" runat="server" OnClick="LinkButton11_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Scheme wise Outflow- District Accounting</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">16.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton10" runat="server" OnClick="LinkButton10_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Transactions - District Accounting</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">17.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton17" runat="server" OnClick="LinkButton17_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Truck Challan Iusse details( MPSCSC )</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">18.</span></strong></td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Receipt Acknowledgement( MPSCSC )</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">19.</span></strong></td>
                <td>
                   &nbsp; <asp:LinkButton ID="LinkButton21" runat="server" OnClick="LinkButton21_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Daily Issue For All Commodity ( MPSCSC )</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">20.</span></strong></td>
                <td>
                   &nbsp; <asp:LinkButton ID="LinkButton20" runat="server" OnClick="LinkButton20_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Stock Register (Godwn No and Commodity Name Wise)</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">21.</span></strong></td>
                <td>
                   &nbsp; <asp:LinkButton ID="LinkButton3" runat="server" Width="304px" OnClick="LinkButton3_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Stack wise Register Report ( All Stack)</asp:LinkButton></td>
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

