<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true"
    CodeFile="Reports_District.aspx.cs" Inherits="StatePages_Reports_District" Title="Warehouse Region Reports" %>

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
                    <asp:Label ID="lblDistrictReports" runat="server" Text="Region Reports District wise"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Commodity Details(MPWLC)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Warehouse Capacity and Utilization(MPWLC)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Warehouse Depositors Details(MPWLC)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Warehouse Capacity and Utilization(MPWLC till now)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">5.</span></strong>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Warehouse Scientific and Maximum Capacity and Utilization(MPWLC till now)</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
