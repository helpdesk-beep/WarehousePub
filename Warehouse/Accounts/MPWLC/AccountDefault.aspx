<%@ Page Language="C#" MasterPageFile="~/MasterPage/Account_MPWLC_Master.master"
    AutoEventWireup="true" CodeFile="AccountDefault.aspx.cs" Inherits="Account_AccountDefault"
    Title="Report Pages States" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 1050px; margin-left: 0px">
        <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 1px; border-color: Green"
            border="1px">
            <tr style="background-color: #391200; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label10" runat="server" Text="Storage Charges Details" ForeColor="WhiteSmoke" Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton61" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton61_Click">लंबित भंडारण शुल्क राशि विवरण(Progressive Date Wise) </asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton60" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton60_Click">लंबित भंडारण शुल्क राशि विवरण(Date Wise) </asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton62" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton62_Click">लंबित भंडारण शुल्क राशि विवरण(Datewise(सोमवार आधारित)) </asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton63" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton63_Click">लंबित भंडारण शुल्क राशि विवरण(संभाग wise Total) </asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton50" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton50_Click">Branch wise Bill Details </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton107" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton107_Click">  Branch Wise - Storage Charges Bill Report </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton111" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton111_Click">  Branch Manager Approved Storage Charges Bill Details </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton115" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton115_Click"> Month Wise Created Bill Details </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton174" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton174_Click">Online Storage Charges Created Bill Summary Report(MPWLC Godown)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton175" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton175_Click">Online Storage Charges Created Bill Summary Report(other then MPWLC Godown)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton176" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton176_Click">PVT Godown Storage Charges and Rent Bill Detail</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">12.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton177" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton177_Click">PVT Godown's Online Bill Payment Status</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">13.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton178" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton178_Click">PVT Godown's End to End Billing Status</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">14.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton179" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton179_Click">Bill Generation to JIT Status</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" class="auto-style1">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">15.</span></td>
                <td class="auto-style2">
                    <asp:LinkButton ID="LinkButton180" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton180_Click">PVT Storage Charges Bill Summary Before August 2020</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">16.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton181" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton181_Click">PVT Storage Charges Bill Summary After August 2020</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">17.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton182" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton182_Click">District, Branch wise PVT Storage Charges Bill Summary Before August 2020</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">18.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton183" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton183_Click">District, Branch wise PVT Storage Charges Bill Summary After August 2020</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">19.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton184" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton184_Click">RM wise PVT Storage Charges Bill Summary with Amount Before August 2020</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">20.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton185" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton185_Click">Branch Wise PVT Godown Storage Charges Bill in Amount(Before August)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">21.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton186" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton186_Click">Region Wise PVT Godown Storage Charges Bill in Amount(Before August)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">22.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton187" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton187_Click">Region Wise District Wise PVT Godown Storage Charges Bill(Before August)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">23.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton188" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton188_Click">Branch Wise PVT Godown Storage Charges Bill in Amount(After August)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">24.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton189" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton189_Click">Region Wise PVT Godown Storage Charges Bill in Amount(After August)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">25.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton190" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton190_Click">Region Wise District Wise PVT Godown Storage Charges Bill(After August)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">26.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton192" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton192_Click">Received Payment from MPSCSC Status(After August)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">27.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton191" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton191_Click">Region Wise Payment Pendency Status at MPSCSC(After August)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">28.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton193" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton193_Click">Region Wise Pendency Report at Various Level before August</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">29.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton194" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton194_Click">Region Wise Pendency Report at Various Level from August</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">30.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton195" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton195_Click">Region Wise Payment Received Details From MPSCSC</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">31.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton200" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton200_Click">District Branch Month Wise Payment Received Details From MPSCSC</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">32.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton201" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton201_Click">Date Wise Payment Received Details From MPSCSC</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">33.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton196" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton196_Click">Pending Bill for Generation from August</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">34.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton197" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton197_Click">Pendency at BM/ICM/DM/RO/NEFT Report from August</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">35.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton198" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt">District and Branch wise Differences at Various Level Report</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">36.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton203" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton203_Click">Branch wise Payment Received from MPSCSC(From August)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">37.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton204" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton204_Click">Pendency with Amount(From August)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">38.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton205" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton205_Click">Month wise Pendency with Amount(From August))</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">39.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton206" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton206_Click">PVT Godown Receive and Paid Amount Status</asp:LinkButton>
                </td>
            </tr>
            <tr style="background-color: #391200; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label14" runat="server" Text="Storage Charges Details (New)" ForeColor="WhiteSmoke" Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton202" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton202_Click">Billing Pendency report at Various Level(From August)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton208" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton208_Click">Received Payment from MPSCSC(From August) </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton207" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton207_Click">Region wise Summary Report from August</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton209" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton209_Click">Storage Bill After August 2020 Search Godown Wise</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton210" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton210_Click">Search Storage Bill After August 2020 Status</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton211" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton211_Click">Pending Storage Bills Report for Payment</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton213" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton213_Click">Received Payment from MPSCSC Region/District/Branch Wise(After August)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton212" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton212_Click">Payment Credit From MPWLC To Godown NEW</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton214" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton214_Click">Payment August To December</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton215" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton215_Click">Payment Jan To March</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton216" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton216_Click">Pending Payment From MPSCSC </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">12.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton217" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton217_Click">Pending Bill Details Before August</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">13.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton225" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton225_Click">District Wise Pendancy at Various Level From August</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">14.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton226" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton226_Click">District Wise Pendancy at Various Level From August(Only Amount)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">15.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton228" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton228_Click">Storage/Rent Payment Pendency at Various Level From August(NEW)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">16.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton229" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt">Rent Bill Payment Pendency at Various Level From August(NEW)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">17.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton230" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton230_Click">Storage/Rent Payment Pendency at Various Level From August(NEW) Date wise</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">18.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton231" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton231_Click">Pendency against Payment Received from MPSCSC</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">19.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton232" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton232_Click">Pending for Account/beneficiary verification at various Level</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">20.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton233" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton233_Click">Pendency for Passing Order After Received Payment August</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">21.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton234" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton234_Click">Pendency at Various Level against Received Payment from MPSCSC(August 2021)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">22.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton235" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton235_Click">Credit/Debit Payment Statement from August 2020</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">23.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton236" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton236_Click">UTR_Wise_NEFT_Generation_Pendency_Details</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">24.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton237" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton237_Click">Track Godown wise Payment Status After August</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">25.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton238" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton238_Click">District-wise Pending Bill for Submission After August (2020)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">26.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton239" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton239_Click">Date and District-wise Stock Position</asp:LinkButton>
                </td>
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

