<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true"
    CodeFile="Reports.aspx.cs" Inherits="IssueCenterLevel_Storage_Reports" Title="Storage Reports" %>

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
    <style type="text/css">
        .ancker {
            ForeColor: navy;
            Font-Bold: true;
            Font-Size: 10pt;
        }
    </style>
    <div style="width: 1000px; margin-left: 0px">
        <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 1px; border-color: Green" border="1px">
            <tr style="background-color: #0bb6e6; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="lblStorageReports" runat="server" Text="शाखा रिपोर्ट सूची" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>

            <tr style="background-color: #E49A21; height: 25px">
                <td colspan="2" align="center">
                    <asp:LinkButton ID="LinkButton170" runat="server" ValidationGroup="lik2"
                        ForeColor="white" Font-Bold="true" PostBackUrl="~/Reports/Branch/StockBal_ReportForInspection.aspx"
                        Font-Size="11pt">Stock Balance Reports for Inspection</asp:LinkButton>
                </td>
            </tr>

            <tr style="background-color: Green; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label12" runat="server" Text="FIFO अनुसार स्टॉक रिपोर्ट" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton112" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton112_Click">FIFO Wise Available Stock Details(For Issue Gate Pass)</asp:LinkButton>
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton118" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton118_Click">FIFO पद्धति से किये गए भुगतान की जानकारी</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton119" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton119_Click">WHR wise Freeze Stock Status for FIFO</asp:LinkButton>
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton120" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton120_Click">Summary of Freeze Stock for FIFO</asp:LinkButton>
                </td>
            </tr>
            <tr style="background-color: Green; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label1" runat="server" Text="गोदामो की रिपोर्ट" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>



            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton52" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton52_Click">Godown Type wise Godown list </asp:LinkButton>
                </td>
            </tr>


            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton80" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton80_Click">Godown Wise Stack Capacity</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton90" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton90_Click">Internal Godown Mapping Comparision Detail</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton95" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton95_Click">New Verified Godown(Existence/Non-Existence) Detail</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">5.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton96" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton96_Click">New Verified Godown Available Stock and Vacant Capacity Report</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">6.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton101" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton101_Click">View Uploaded Digital Signature Certificates (DSC) Details </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">7.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton103" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton103_Click">View WHR Capacity Against JVS Agreemented Capacity(Crop Year 2019-20) </asp:LinkButton>
                </td>
            </tr>

            <tr style="background-color: Maroon; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label3" runat="server" Text="गोदामो की क्षमता एवं उपयोगिता संबन्धित रिपोर्ट" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton34" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton34_Click">Godown Wise Current Capacity </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton43" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton43_Click">Graphical Godown detail </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton54" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton54_Click">Godown Geo Location current status </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton55" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton55_Click">Godown type wise current capacity </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">5.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton62" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton62_Click">Steel Silo Capacity & Utilization </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">6.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton63" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton63_Click">PVT. PEG Godowns Capacity and Utilization </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td align="center" class="style1">
                    <strong><span style="color: Navy">7.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton64" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton64_Click">WDRA Godowns Capacity and Utilization</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">8.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton65" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton65_Click">JVS Godowns Capacity and Utilization</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">9.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton66" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton66_Click">Owned Godown Capacity and Utilization</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">10.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton69" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton69_Click">Commodity Wise Godown wise Stock Report</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">11.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton70" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton70_Click">Godown Wise Commodity Summary</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">12.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton78" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton78_Click">Godown Capacity and Utilization in Graph Representation</asp:LinkButton>
                </td>
            </tr>

            <tr style="background-color: Teal; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label4" runat="server" Text="रजिस्टर रिपोर्ट" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="navy" Font-Bold="true" Font-Size="10pt"
                        OnClick="LinkButton1_Click">Daily Receipt and Release Register</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td align="center">
                    <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton12" runat="server" ForeColor="navy" Font-Bold="true" Font-Size="10pt"
                        OnClick="LinkButton12_Click" PostBackUrl="~/Reports/Branch/StackWiseRegNew.aspx">Stack wise Register</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Depositor Ledger</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton13" runat="server" OnClick="LinkButton13_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Stock Register</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">5.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Stack wise Condition Report Register</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">6.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton9" runat="server" OnClick="LinkButton9_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">GatePass Register</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">7.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton16" runat="server" OnClick="LinkButton16_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Stock Valuation Register</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">8.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton20" runat="server" OnClick="LinkButton20_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Stock Register (Godwn and Commodity Wise)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">9.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton3" runat="server" Width="304px" OnClick="LinkButton3_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Stack wise Register Report ( All Stack)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">10.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton39" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt"
                        PostBackUrl="~/Reports/Branch/Depositerlezer.aspx">Depositer Lazer(New)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">11.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton40" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" PostBackUrl="~/Reports/Branch/StockRegister.aspx">Stock Register(New)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">12.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton47" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton47_Click">Reservation Register </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">13.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton49" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton49_Click">Godown Register Details </asp:LinkButton>
                </td>
            </tr>

            <tr style="background-color: Green; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label5" runat="server" Text="स्कंध के जमा संबंधित रिपोर्ट" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">WareHouse Receipt</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton23" runat="server" Width="304px" ForeColor="navy" Font-Bold="true" Font-Size="10pt"
                        OnClick="LinkButton23_Click">Godown Wise WHR Details Report  </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton24" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton24_Click">Godownwise & Stackwise Receipt Details Report</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton25" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton25_Click">Godown Wise & WHR Wise Stock Balance Report</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">5.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton32" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton32_Click">WHR From Depositor Form </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">6.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton33" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton33_Click">Godown Wise Whr Status(MPWLC) </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">7.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton35" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton35_Click">Soceity Wise whr Details </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">8.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton36" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton36_Click">Total Reciving Details</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">9.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton37" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton37_Click">CropYear Wise - Commodity Wise WHR Report</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">10.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton38" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton38_Click">Depositer wise WHR Details</asp:LinkButton>
                </td>
            </tr>

            <%--            <tr>
                <td style="width: 10px" align="center">
                  <strong><span style="color: Navy">11.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton41" runat="server"  ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="10pt" onclick="LinkButton41_Click"   
                       >WHR and Depositor form details</asp:LinkButton></td>
            </tr>--%>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">11.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton46" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton46_Click">Menual WHR Detail </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">12.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton58" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton58_Click">Crop Year Wise WHR Detail </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">13.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton81" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton81_Click">Depositor Form No./Challan/TC No. Wise Receiving Details </asp:LinkButton>
                </td>
            </tr>



            <tr style="background-color: Maroon; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label6" runat="server" Text="स्कंध के भुगतान संबंधित रिपोर्ट" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Delivery Order</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton15" runat="server" ForeColor="navy" Font-Bold="true" Font-Size="10pt"
                        OnClick="LinkButton15_Click">DO wise issue details( MPSCSC )</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton17" runat="server" OnClick="LinkButton17_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Truck Challan Issue details( MPSCSC )</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton26" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton26_Click">Gatepass Detail Reciver wise </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">5.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton44" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton44_Click">DO/TO wise Gatepass Detail </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">6.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton45" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton45_Click">WHR Issued and cancelled register</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">7.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton48" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton48_Click">Pending DO/TO Details </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">8.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton71" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton71_Click">Pending Paddy DO Details </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">9.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton83" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton83_Click">Pending Door Step DO Details </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">10.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton84" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton84_Click">Pending Other Scheme DO Details </asp:LinkButton>
                </td>
            </tr>

            <tr style="background-color: Blue; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label7" runat="server" Text="दिनांक वार स्कंध जमा-भुगतान संबंधित रिपोर्ट" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton19" runat="server" OnClick="LinkButton19_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Godown Stack wise Getpass Details B/W Two Dates</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton14" runat="server" OnClick="LinkButton14_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Daily Receipt( MPSCSC )</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Receipt Acknowledgement( MPSCSC )</asp:LinkButton>
                </td>
            </tr>
            <%--             <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong>
                </td>
                <td>
                   
                    <asp:LinkButton ID="LinkButton21" runat="server" OnClick="LinkButton21_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Daily Issue For All Commodity ( MPSCSC )</asp:LinkButton>
                </td>
            </tr>--%>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton22" runat="server" Width="304px" ForeColor="navy" Font-Bold="true"
                        PostBackUrl="~/Reports/Branch/Rpt_Depositorformreceipt.aspx" Font-Size="10pt">Depositor Form Receipt(Datewise)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">5.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton28" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton28_Click">Commodity Wise Depositor Wise WHR Status Selected Month </asp:LinkButton>
                </td>
            </tr>
            <%--             <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">6.</span></strong></td>
                <td>
                   <asp:LinkButton ID="LinkButton31" runat="server"  ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="10pt" onclick="LinkButton31_Click" >Godown Wise Depositor wise WHR Report till Selected Month </asp:LinkButton></td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">7.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton29" runat="server"  ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="10pt" onclick="LinkButton29_Click" >Depositor Wise WHR Current status Monthly </asp:LinkButton></td>
            </tr>--%>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">6.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton30" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton30_Click">Depositor Wise Godown Wise WHR Current status Between selected Month </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">7.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton50" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton50_Click">Monthly Report with Opening Closing commodity (On WHR Issuce Date) </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">8.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton51" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton51_Click">Opening Closing Godown wise </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">9.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton56" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton56_Click">Opening Closing Godown Wise Report</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">10.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton67" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton67_Click">Godown Wise Receive Issue Between two Dates</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">11.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton68" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton68_Click">Godown Wise WHR Detail Between two Dates</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">12.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton72" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton72_Click">Godown Wise Commodity wise Opening Closing Between two Dates</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">13.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton79" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton79_Click">Godown Wise Stock Issue with GP & DO Detail against MPSCSC DO </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">14.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton85" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton85_Click">Godown Wise Gatepass details Between Two dates</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">15.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton88" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton88_Click">Godown Wise Closing Balance (Particular Date)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">16.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton89" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton89_Click">Commodity Wise Closing Balance (Particular Date)</asp:LinkButton>
                </td>
            </tr>

            <tr style="background-color: Teal; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label10" runat="server" Text="उपार्जन सम्ब्बंधित रिपोर्ट" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton74" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton74_Click">Kharif Procurement 2016-17 ( Commodity Wise ) </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton42" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton42_Click">Procurement 2015-16(Wheat) </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton57" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton57_Click">Wheat procurement 2016-17 </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton82" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton82_Click">Wheat procurement 2017-18 </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">5.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton86" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton86_Click">Remaining Depositor Form For Creating WHR Wheat procurement 2017-18 </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">6.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton91" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton91_Click">Kharif Procurement 2017-18 (Commodity Wise) </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">7.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkWheat1819" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkWheat1819_Click">Wheat Procurement 2018-19 </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">8.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkGram" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkGram_Click">GRAM,LENTIL,Mustard-Sarason Procurement 2018-19 </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">9.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton93" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton93_Click1">Quality Check Status Against Provisional Deposite Form GRAM,LENTIL,Mustard-Sarason Procurement 2018-19 </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">10.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton97" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton97_Click">  (Ground-Nut,Moong,RAM TIL,Urad,Tilli) Procurement 2018-19</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">11.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton98" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton98_Click"> Coarse Grain Procurement 2018-19 (Bajra,Jowar)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">12.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton99" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton99_Click">Paady Procurement 2018-19 </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">13.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton100" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton100_Click">Wheat Procurement 2019-20 </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">14.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton102" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton102_Click">e-WHR Online Submission and Print Detail (Dalhan Procurement 2019-20)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">15.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton108" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton108_Click">Wheat Procurement 2020-21 </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">16.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton109" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton109_Click">Chana, Masoor and Sarson Procurement 2020-21 </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">17.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton110" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton110_Click">WHR wise Acceptance Note Chana, Masoor and Sarson Procurement 2020-21 </asp:LinkButton>
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">18.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton117" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton117_Click">WHR wise Acceptance Note Chana, Masoor and Sarson Procurement 2022-23 </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">19.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton111" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton111_Click">WHR wise Acceptance Note Wheat Procurement 2020-21 </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">20.</span></strong></td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Procurement_Wheat.aspx" class="ancker" target="_blank">Wheat Procurement 2021-22</a>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">21.</span></strong></td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Procurement_CMS.aspx" class="ancker" target="_blank">Chana, Masoor and Sarson Procurement 2021-22</a>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">22.</span></strong></td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Pending_WHR_Details.aspx" class="ancker" target="_blank">Pending Acceptance for Generate Dalhan WHR 2022-23</a>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">23.</span></strong></td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Rabi_Pending_WHR_Details.aspx" class="ancker" target="_blank">Pending Acceptance Note Rabi Procurement 2022-23</a>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">24.</span></strong></td>
                <td>
                     <asp:LinkButton ID="LinkButton113" runat="server" OnClick="LinkButton18_Click"
                     ForeColor="navy" Font-Bold="true" Font-Size="10pt">WHR Wise Acceptance Note Wheat Procurement 2021-22</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">25.</span></strong></td>
                <td>
                     <asp:LinkButton ID="LinkButton116" runat="server" OnClick="LinkButton116_Click"
                     ForeColor="navy" Font-Bold="true" Font-Size="10pt">WHR Wise Acceptance Note Wheat Procurement 2022-23</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">26.</span></strong></td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Procurement_Moong.aspx" class="ancker" target="_blank">Moong Procurement 2021-22</a>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">27.</span></strong></td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Procurement_urad.aspx" class="ancker" target="_blank">Urad Procurement 2021-22</a>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">28.</span></strong></td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Procurement_Wheat_2022.aspx" class="ancker" target="_blank">Wheat Procurement 2022-23</a>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">29.</span></strong></td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Procurement_CMS2022.aspx" class="ancker" target="_blank">Chana, Masoor and Sarson Procurement 2022-23</a>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">30.</span></strong></td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Procurement_Moong_Ured_2022_23.aspx" class="ancker" target="_blank">Moong Urad Procurement 2022-23</a>

                </td>
            </tr>
            <tr>
                 <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">31.</span></strong></td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Moong_Urad_Pending_WHR_Details_2022.aspx" class="ancker" target="_blank">Pending Acceptance Note Moong Urad Procurement 2022-23</a>

                </td>
            </tr>
            <tr style="background-color: Gray; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label8" runat="server" Text="वर्तमान स्टॉक की रिपोर्ट" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <%--            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton18" runat="server" OnClick="LinkButton18_Click"
                     ForeColor="navy" Font-Bold="true" Font-Size="10pt">Commodity Wise - Godown Wise Available Stack Stock Position </asp:LinkButton>
               </td>
            </tr>--%>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton18" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton41_Click">Godown Wise WHR Details (With CropYear) </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton27" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton27_Click">Godown Wise Available Stack Capacity </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton75" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton75_Click">Commodity wise MPSCSC Stock Report </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton76" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton76_Click">Depositor Wise - Commodity Wise Stock Report (Not MPSCSC) </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">5.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton29" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton29_Click1">Godown Wise MPSCSC Stock Report </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">6.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton41" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton41_Click">Godown Wise WHR Details (With CropYear) </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">7.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton31" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton31_Click1">Godown Wise - Commodity Wise MPSCSC Stock Report (With CropYear) </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">8.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton8" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton8_Click">MPSCSC Available Stock Report in Chart Representation </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">9.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton77" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton77_Click">Commodity Wise Available Stock Report in Graph Representation </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">10.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton87" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton87_Click">Godown Wise Available Stock</asp:LinkButton>
                </td>
            </tr>

            <tr style="background-color: Green; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label11" runat="server" Text="बीलिंग संबन्धित रिपोर्ट" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton92" runat="server"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt"
                        OnClick="LinkButton92_Click">Month Wise - Created Bill Details</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton104" runat="server"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt"
                        OnClick="LinkButton104_Click">Godown Wise Commodity Wise WHR Balance</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton105" runat="server"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton105_Click">Track Online Bill Status</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton106" runat="server"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton106_Click">Online Bill Payment Status</asp:LinkButton>
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">5.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton114" runat="server"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton114_Click">Online Bill Payment Status for Owned Godown Bedore August</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">6.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton107" runat="server"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton107_Click">Online Bill Payment Status(Success/Failed)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">7.</span></strong>
                </td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Get_Bill_Detail_After_August_District_Branch_Wise_Difference_With_Amount.aspx" class="ancker" target="_blank">PVT Godown Storage Charges Bill Generation(From Aug.)</a>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">8.</span></strong>
                </td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Get_Date_Wise_Payment_Received_From_MPSCSC_Details_From_Aug.aspx" class="ancker" target="_blank">Date Wise Payment Received From MPSCSC</a>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">9.</span></strong>
                </td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Get_Payment_Received_From_MPSCSC_Details_From_Aug_Godown_Wise.aspx" class="ancker" target="_blank">Godown Wise Payment Received From MPSCSC</a>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">10.</span></strong>
                </td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Get_Pending_Bill_Detail_August.aspx" class="ancker" target="_blank">Branch wise Bill Pendency Report From August</a>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">11.</span></strong>
                </td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Get_Payment_Credit_From_MPWLC_To_Godown_Wise.aspx" class="ancker" target="_blank">Branch wise Bill Pendency Report From August</a>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">12.</span></strong>
                </td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Pending_Bill_Details_Motn_Wise.aspx" class="ancker" target="_blank">Month wise Pending Bill Details from August</a>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">13.</span></strong>
                </td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Get_Payment_Credit_From_MPWLC_To_Godown_Wise.aspx" class="ancker" target="_blank">Payment to Godown Owner from August</a>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">14.</span></strong>
                </td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Branch_And_Month_Wise_Pending_Amount.aspx" class="ancker" target="_blank">Pending Storage Bills Report for Payment</a>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">15.</span></strong>
                </td>
                <td>
                    <a href="../../Reports/Branch/Payment_Credit_From_MPWLC_To_Godown.aspx" class="ancker" target="_blank">Payment Credit From MPWLC To Godown(NEW)</a>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">16.</span></strong>
                </td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Godown_Wise_Pendancy_At_Various_Level_New.aspx" class="ancker" target="_blank">Pendency at Various Level(After August)</a>
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">17.</span></strong>
                </td>
                <td>
                    <a href="../../Reports/Branch/Rpt_Godown_And_Month_Wise_Pending_Amount_For_Branch.aspx" class="ancker" target="_blank">Owned Godown Payment Status After August</a>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">18.</span></strong>
                </td>
                <td>
                    <a href="../../Reports/Branch/Pending_Bill_for_Submission.aspx" class="ancker" target="_blank">Pending Bill for Submission After August (2020)</a>
                </td>
            </tr>
            <tr style="background-color: Green; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label9" runat="server" Text="अन्य रिपोर्ट" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton11" runat="server" OnClick="LinkButton11_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Scheme wise Outflow- District Accounting</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton10" runat="server" OnClick="LinkButton10_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Transactions - District Accounting</asp:LinkButton>
                </td>
            </tr>

            <%--  <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">28.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton28" runat="server"  ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="10pt" onclick="LinkButton28_Click" >Godown Wise Stack wise Receiving Details(Between Dates)</asp:LinkButton>
                 </td>
            </tr>--%>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton60" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton60_Click">Paddy Loss During Storage(Manual Entry) Report</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton73" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton73_Click">Wheat-PSS Gain During Storage(Manual Entry) Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">5.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton53" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton53_Click">Godown Geo Location </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">6.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton59" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton59_Click">Godown Mapping Report</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">7.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton61" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton61_Click">Godown Wise Detail List For Updation</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">8.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton21" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton21_Click">Stock Current Status during Godown Handover</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">9.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton94" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt"
                        PostBackUrl="~/Reports/Branch/Branch_OpeningClosingBtDate.aspx">MPSCSC Stock Opening Closing Between Dates</asp:LinkButton>
                </td>
            </tr>

                     <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">10.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton115" runat="server" ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt"
                        PostBackUrl="~/Accounting/BranchChoiceFilling.aspx">JVS Choice Filling 2021-2022</asp:LinkButton>
                </td>
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
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">

    <script language="JavaScript1.2">
var message="MPWLC STORAGE MODULE"
var neonbasecolor="gray"
var neontextcolor="yellow"
var flashspeed=100  //in milliseconds

///No need to edit below this line/////

var n=0
if (document.all||document.getElementById){
document.write('<font color="'+neonbasecolor+'">')
for (m=0;m<message.length;m++)
document.write('<span id="neonlight'+m+'">'+message.charAt(m)+'</span>')
document.write('</font>')
}
else
document.write(message)

function crossref(number){
var crossobj=document.all? eval("document.all.neonlight"+number) : document.getElementById("neonlight"+number)
return crossobj
}

function neon(){

//Change all letters to base color
if (n==0){
for (m=0;m<message.length;m++)
//eval("document.all.neonlight"+m).style.color=neonbasecolor
crossref(m).style.color=neonbasecolor
}

//cycle through and change individual letters to neon color
crossref(n).style.color=neontextcolor

if (n<message.length-1)
n++
else{
n=0
clearInterval(flashing)
setTimeout("beginneon()",1500)
return
}
}

function beginneon(){
if (document.all||document.getElementById)
flashing=setInterval("neon()",flashspeed)
}
beginneon()
    </script>

    <style type="text/css">
        .style1 {
            width: 10px;
        }
    </style>

</asp:Content>

