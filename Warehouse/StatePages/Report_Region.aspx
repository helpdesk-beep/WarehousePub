<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master"
    AutoEventWireup="true" CodeFile="Report_Region.aspx.cs" Inherits="IssueCenterLevel_Storage_Report_Region"
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
            <tr style="background-color: Maroon; height: 25px">
                <td colspan="2" align="center">
                    <%-- <asp:Label ID="Label6" runat="server" Text="MPSCSC Stock Reports" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>--%>
                    <asp:LinkButton ID="lnkMPSCSCR" runat="server" ValidationGroup="lik2"
                        ForeColor="white" Font-Bold="true" PostBackUrl="~/IssueCenterLevel/Storage/MPSCSC_Reports.aspx"
                        Font-Size="11pt">MPSCSC Stock Reports</asp:LinkButton>
                </td>
            </tr>

            <tr style="background-color: #E49A21; height: 25px">
                <td colspan="2" align="center">
                    <%-- <asp:Label ID="Label6" runat="server" Text="MPSCSC Stock Reports" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>--%>
                    <asp:LinkButton ID="LinkButton170" runat="server" ValidationGroup="lik2"
                        ForeColor="white" Font-Bold="true" PostBackUrl="~/StatePages/State_Reports_Procurement_201920.aspx"
                        Font-Size="11pt">Procurement WHR Reports</asp:LinkButton>
                </td>
            </tr>
            <%--    <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                      <asp:LinkButton ID="lncCropWiseMpscsc" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="lncCropWiseMpscsc_Click">Crop Year Wise MPSCSC Current Stock Report</asp:LinkButton></td>
            </tr>
            
                <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton2" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton2_Click1">Crop Year Wise - Commodity Wise MPSCSC Current Stock Report</asp:LinkButton></td>
            </tr>            
             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                      <asp:LinkButton ID="LinkAllC" runat="server" ValidationGroup="lik2" ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkAllC_Click">MPSCSC All Commodity Stock Status</asp:LinkButton></td>
            </tr>
            
                        <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span>
                </td>
                <td style="width: 36px; height: 18px;">
                    <asp:LinkButton ID="LinkButton27" runat="server" ValidationGroup="lik2" ForeColor="navy" Font-Bold="true" 
                     Font-Size="10pt" onclick="LinkButton27_Click" Width="400px" > WHR Report(MPSCSC)</asp:LinkButton>
                </td>
            </tr>
            
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span>
                </td>
                <td>
                   <asp:LinkButton ID="LinkButton29" runat="server" ValidationGroup="lik2" ForeColor="navy" Font-Bold="true" 
                   Font-Size="10pt" onclick="LinkButton29_Click"  > Commodity Wise WHR Report(MPSCSC)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                <td>
                <asp:LinkButton ID="LinkButton74" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" OnClientClick="window.document.forms[0].target='_blank';"
                        Font-Size="10pt" PostBackUrl="~/Reports/States/Rpt_MPSCSC_Commodity_Region.aspx">WHR Report(MPSCSC) Commodity Region Wise</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span></td>
                <td>
                <asp:LinkButton ID="LinkButton75" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" OnClientClick="window.document.forms[0].target='_blank';"
                        Font-Size="10pt" PostBackUrl="~/Reports/States/Rpt_MPSCSC_Commodity_State.aspx">WHR Report(MPSCSC) Commodity (State)</asp:LinkButton></td>
            </tr>
            
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span>
                </td>
                <td>
                          <asp:LinkButton ID="LinkButton46" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton46_Click" >Godown Wise Current Capacity(MPSCSC)  </asp:LinkButton>  
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span>
                </td>
                <td>
                          <asp:LinkButton ID="LinkButton97" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton97_Click">Gunny Stock Report</asp:LinkButton>  
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span>
                </td>
                <td>
                          <asp:LinkButton ID="LinkButton11" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton11_Click1">Rice Stock Report</asp:LinkButton>  
                </td>
            </tr>
                            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span></td>
                <td>
                      <asp:LinkButton ID="Lnk13" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="Lnk13_Click">Wheat PSS Current Stock Report</asp:LinkButton></td>
            </tr>
                          <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">12.</span></td>
                <td>
                      <asp:LinkButton ID="lnkTillWheat" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="lnkTillWheat_Click">Wheat PSS Current Stock Report Till Date</asp:LinkButton></td>
            </tr>
                          <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">13.</span></td>
                <td>
                      <asp:LinkButton ID="lnkTillRice" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" OnClick="lnkTillRice_Click">Rice Current Stock Report Till Date</asp:LinkButton></td>
            </tr>--%>



            <tr style="background-color: #0bb636; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label7" runat="server" Text="Godown Details " ForeColor="WhiteSmoke" Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton68" runat="server" ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton68_Click1">Branch Wise Godown List</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton89" runat="server" ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton89_Click">Issue Center Wise MPWLC Branch Detail</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton90" runat="server" ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton90_Click">Issue Center Wise Non-MPWLC Branch Detail</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton34" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton34_Click">Region Wise Godown List </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton94" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt"
                        OnClick="LinkButton94_Click">Godown Wise Latitude & Longitude Detail </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton95" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton95_Click">Godown Wise stack Quality Control Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton96" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton96_Click">Unutilized Godown List at Till Date</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton98" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton98_Click">Branch Wise Godown Count</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton2" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton2_Click2">Pvt. Godown/Silo List</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton97" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton97_Click1">Branch Wise Godown List with Organization</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton102" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton102_Click">Newly Created Godown Detail BW Two Dates</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">12.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton103" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton103_Click">Mapped(Internal) Premises Wise Godown Details</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">13.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton105" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton105_Click">Pending Mapping(Internal) of Premises Wise Godown Details</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">14.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton106" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton106_Click">Premises Wise Godown comparision with System's Godown Details</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">15.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton110" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton110_Click">Joint Venture Scheme (JVS) Godown Owner Details</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">16.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton116" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton116_Click">Godown Wise WHR Quantity againts Aggrement Capacity ( JVS & WDRA ) </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">17.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton117" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton117_Click">District Wise Branch Additional Details </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">18.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton125" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton125_Click"> Godown Wise WHR Quantity againts Warehouse Agreemented Capacity (Wheat 2018-19) </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">19.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton155" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton155_Click"> New Verify Godown Summary against Old Godown </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">20.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton156" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton156_Click"> Premises Capacity >= 2000 MT </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">21.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton157" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton157_Click"> Godown Capacity >= 2000 MT </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">22.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton158" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton158_Click"> Hired Type wise Verify godown Capacity Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 28px;" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">23.</span></td>
                <td style="height: 28px">
                    <asp:LinkButton ID="LinkButton160" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton160_Click">Godown Wise New Verified Godown Details</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px; height: 28px;" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">24.</span></td>
                <td style="height: 28px">
                    <asp:LinkButton ID="LinkButton165" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt"
                        OnClick="LinkButton165_Click">Godown Wise Vacant Capacity (based on Closing Balance 15/10/2018)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px; height: 28px;" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">25.</span></td>
                <td style="height: 28px">
                    <asp:LinkButton ID="LinkButton166" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt"
                        OnClick="LinkButton166_Click">District Wise Godown Type Wise Vacant Capacity (based on Closing Balance 15/10/2018)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 28px;" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">26.</span></td>
                <td style="height: 28px">
                    <asp:LinkButton ID="LinkButton167" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt"
                        OnClick="LinkButton167_Click">District Wise No Of Godown,Godown Capacitya and Vacant Capacity(based on Closing Balance 15/10/2018)</asp:LinkButton>
                </td>
            </tr>

            <tr style="background-color: #CC901F; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label13" runat="server" Text="Godown Maping Report" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton146" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton146_Click">Godown Mapping with Village </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton123" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton123_Click">Godown Mapping with Procurement Center Wheat 2018-2019 </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton169" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton169_Click">View Uploaded Digital Signature Certificates (DSC) Details </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton172" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton172_Click">Branch Hardware & other Resources Detail</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton173" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton173_Click">Godown Owners Account Details</asp:LinkButton>
                </td>
            </tr>
            <tr style="background-color: #CC99FF; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label1" runat="server" Text="Business Reports" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>



            <%--            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span>
                </td>
                <td>
                  <asp:LinkButton ID="LinkButton58" runat="server"  ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton58_Click">Summary of Depositor wise Report</asp:LinkButton>
                    </td>
            </tr>--%>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton100" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton100_Click">Godown Wise Capacity/Stock Position</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton8_Click" ValidationGroup="link1"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Commodity Wise Stock Position</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton9" runat="server" OnClick="LinkButton9_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Warehouse Depositors Details(MPWLC DistrictWise)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Summary of Warehouse Depositors Details(Receiving Only)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton77" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton77_Click">Commodity Loss during Storage(Manual Enter) Report</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton78" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton78_Click">Effective Date wise Rates Detail</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton84" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton84_Click">Commodity Loss during Storage Manual Entry Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton87" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton87_Click">Category Wise Stock Status Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton108" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton108_Click">Wheat Procurement 2017 - Godown Priority List Based on  First Deposite Date </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton145" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton145_Click">Procurement 2018-19 - Godown Priority(FIFO) List Based on  First Deposite Date </asp:LinkButton>
                </td>
            </tr>

            <tr style="background-color: #0099CC; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label12" runat="server" Text="Summary Reports" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span>
                </td>
                <td>

                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ValidationGroup="link1"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Summary of Commodity Type Wise Available Stock (MPSCSC)</asp:LinkButton>
                </td>
            </tr>


            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton33" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton33_Click">Summary Of Commodity Wise Available Stock Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton122" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton122_Click">Summary Of Vacant Capacity (District WIse)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton140" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton140_Click">Summary Of Chana,Sarson,Masoor Procurement 2018-19 Available Stock Report (District Wise)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton141" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton141_Click">Summary Of Godown Capacity and Available Stock Report (Region Wise - Godown Type Wise)</asp:LinkButton>
                </td>
            </tr>

            <tr style="background-color: #993300; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label2" runat="server" Text="Operator Details" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span>
                </td>
                <td>&nbsp;
                    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Registered Operator Details (District Manager)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span>
                </td>
                <td>&nbsp;
                                        
                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Operators Login  Details</asp:LinkButton>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span>
                </td>
                <td>&nbsp;
                                                                   
                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Registered Operator Details (Issue Centre)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span>
                </td>
                <td>&nbsp;
                                                                   
                    <asp:LinkButton ID="LinkButton88" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton88_Click">Branch Wise Last Operation Detail</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span>
                </td>
                <td>&nbsp;
                                                                   
                    <asp:LinkButton ID="LinkButton91" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton91_Click">WHR Wise Date Difference B/W WHR Issue Date and Entry Date</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span>
                </td>
                <td>&nbsp;
                                                                   
                    <asp:LinkButton ID="LinkButton93" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton93_Click">Gate Pass Wise Date Difference B/W Gate Pass Issue Date and Entry Date</asp:LinkButton>
                </td>
            </tr>
            <tr style="background-color: #C019FF; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label9" runat="server" Text=" Capacity and Utilization Reports" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>


            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton45" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton45_Click">Region Wise, CropYear Wise Godown Capacity and Utilization</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton79" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton79_Click">Steel Silo Capacity and Utilization</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton80" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton80_Click">WDRA-Godowns  Capacity and Utilization</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton81" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton81_Click">PVT.PEG-Godowns Capacity and Utilization</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton82" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton82_Click">JVS-Godowns Capacity and Utilization</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton83" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton83_Click">Owned-Godowns Capacity and Utilization</asp:LinkButton>
                </td>
            </tr>

            <%--            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton11" runat="server" OnClick="LinkButton11_Click" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Warehouse Capacity and Utilization(till now)</asp:LinkButton>
          
                       </td>
            </tr>--%>

            <%--            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span>
                </td>
                <td>
                   
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Warehouse Capacity and Utilization(MPWLC)</asp:LinkButton>
                </td>
            </tr>--%>

            <%--            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">13.</span>
                </td>
                <td>
                   
                    <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Warehouse Capacity and Utilization(MPWLC DistrictWise)</asp:LinkButton>
                   </td>
            </tr>--%>

            <%--             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">14.</span>
                </td>
                <td>      
                    <asp:LinkButton ID="LinkButton13" runat="server" ValidationGroup="lik2" OnClick="LinkButton13_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Warehouse Scientific and Maximum Capacity and Utilization(till now)</asp:LinkButton>
                                  </td>
            </tr>--%>





            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton42" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton42_Click">Godown Type wise Current Capacity (State)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton48" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton48_Click">Branch wise Capacity Utilization (Owned,Hired,JV,Other)  </asp:LinkButton>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton31" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton31_Click">District Wise Godown Available Capacity</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton171" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton171_Click">Region Wise Godown Type Wise Capacity and Utilization (Closing Date)</asp:LinkButton>
                </td>
            </tr>


            <tr style="background-color: #CC99FF; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label3" runat="server" Text="Current Stock Reports" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span>
                </td>
                <td style="width: 36px; height: 18px;">
                    <asp:LinkButton ID="LinkButton26" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton26_Click" Width="400px" Height="16px">Depositor wise Godown Stock Report</asp:LinkButton>
                </td>
            </tr>



            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton28" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton28_Click"> Commodity Wise Depositor wise WHR Report</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span>
                </td>
                <td style="width: 36px">
                    <asp:LinkButton ID="LinkButton30" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton30_Click" Width="500px">Godown Wise WHR Details (With CropYear & Commodity filter)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton20" runat="server" OnClick="LinkButton20_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Godown Wise Stack Stock Position (District)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton43" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton43_Click">Commodity wise WHR Wise Godown Details </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px;" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton24" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="~/Reports/States/RptAcceptancewhr.aspx" Font-Bold="true" Font-Size="10pt">Districtwise Acceptance with WHR No. Details Report</asp:LinkButton>
                </td>
            </tr>


            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton15" runat="server" Width="500px" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="~/Reports/States/Rpt_Statewhrdetail_drilldown.aspx" OnClientClick="window.document.forms[0].target='_blank';" Font-Bold="true"
                        Font-Size="10pt">RegionWise WHR,DO,Receipt Details</asp:LinkButton>
                </td>
            </tr>
            <%--             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton17" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="~/Reports/States/Rptworkstatus.aspx" Font-Bold="true" Font-Size="10pt">Districtwise Work Status Repot</asp:LinkButton>
                </td>
            </tr>--%>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton35" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton35_Click">Commodity wise WHR Balance Detail </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span></td>
                <td>

                    <asp:LinkButton ID="LinkButton18" runat="server" OnClick="LinkButton18_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Commodity Wise - Godown wise stack Stock position</asp:LinkButton>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton16" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="~/Reports/States/Rpt_Commoditywise_whr.aspx" Font-Bold="true" Font-Size="10pt">CommodityWise & Branchwise WHR Details</asp:LinkButton>

                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton53" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton53_Click">Godown wise - Commodity Wsie Available Stock (With CropYear)</asp:LinkButton></td>
            </tr>



            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">12.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton59" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton59_Click">Godown wise Available Stock</asp:LinkButton></td>
            </tr>



            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">13.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton71" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" OnClientClick="window.document.forms[0].target='_blank';"
                        Font-Size="10pt" PostBackUrl="~/Reports/States/Andriod_ReceivingStock.aspx">Mobile/Tablet based Stock Details</asp:LinkButton></td>
            </tr>


            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">14.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton76" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton76_Click">Onion Current Stock Report</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">15.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton85" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton85_Click">Old Stock Report Before 2015-16 </asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">16.</span></td>
                <td>
                    <asp:LinkButton ID="SCSR" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="SCSR_Click">State Current Stock Status</asp:LinkButton></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">18.</span></td>
                <td>
                    <asp:LinkButton ID="LinkBtn2" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkBtn2_Click">Commodity Wise Stock Report In Graph Representation </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">19.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton58" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton58_Click">Arhar/Tuar Current Stock Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">20.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton69" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton69_Click">Urad Current Stock Report</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">21.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton104" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton104_Click">Godown Wise Available Stock With Loss Gain </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">22.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton154" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton154_Click">MPSCSC commodity & WHR wise Stock Report </asp:LinkButton>
                </td>
            </tr>


            <tr style="background-color: #CC901F; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label8" runat="server" Text="Date Wise Received and Issue Details Report" ForeColor="WhiteSmoke" Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center"><span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton86" runat="server" ValidationGroup="lik2" ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton86_Click">Godown Wise Receive Issue Between two Dates </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center"><span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton66" runat="server" ValidationGroup="lik2" ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton66_Click">Commodity wise Between date with Opening closing </asp:LinkButton>
                </td>

            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton54" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton54_Click">Branch wise Daily Transactions </asp:LinkButton></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton56" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton56_Click">Branch wise Day wise Transactions </asp:LinkButton></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span>
                </td>
                <td>

                    <asp:LinkButton ID="LinkButton10" runat="server" OnClick="LinkButton10_Click" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Warehouse Capacity and Utilization Between two dates</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton19" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="~/Reports/States/Rpt_Deliveryissuedetails.aspx" Font-Bold="true"
                        Font-Size="10pt" Width="500px">Godownwise Receipt & Issue Details Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center"><span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span></td>
                <td>
                    <asp:LinkButton ID="lnkOCBD" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="lnkOCBD_Click">Branch Wise Opening Closing Between two Dates </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center"><span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton46" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt"
                        OnClick="LinkButton46_Click1">Gatepass Wise Stock Issue Detail B/W Two Dates</asp:LinkButton>
                </td>
            </tr>


            <tr style="background-color: #0099CC; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label5" runat="server" Text="Deletion & Panding  Details" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1</span></td>
                <td>
                    <asp:LinkButton ID="lblDeleteAllWHRDOReport" runat="server" Font-Bold="true" Font-Size="10pt"
                        ForeColor="navy" OnClick="lblDeleteAllWHRDOReport_Click" ValidationGroup="lik2"
                        Width="272px">Delete WHR,DO,Gatepass Report</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton47" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton47_Click">Total deletion and print reset detail branch wise  </asp:LinkButton>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton23" runat="server" OnClick="LinkButton23_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Pending Delivery Order Gatepass Entry(with operator Details)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton22" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="~/Reports/States/Rpt_Notworking.aspx" Font-Bold="true" Font-Size="10pt">Districtwise Branches &apos;Not Started Working&apos; Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton25" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="~/Reports/States/Rptchallannotreceived.aspx" Font-Bold="true" Font-Size="10pt">Challan No. with Acceptance Not Received in Warehouse Details Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                <td>
                    <asp:LinkButton ID="lnkdelWHRCompare" runat="server" Font-Bold="true" Font-Size="10pt"
                        ForeColor="navy" OnClick="lnkdelWHRCompare_Click" ValidationGroup="lik2" Width="270px">Delete WHR Compare Report</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton32" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton32_Click">Pending Receiving Details</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton21" runat="server" Font-Bold="true" Font-Size="10pt"
                        ForeColor="navy" OnClick="LinkButton21_Click" ValidationGroup="lik2" Width="428px">District wise and Branch wise Pending Gatepass Entry</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton148" runat="server" Font-Bold="true" Font-Size="10pt"
                        ForeColor="navy" ValidationGroup="lik2" Width="428px"
                        OnClick="LinkButton148_Click">Chana, Masur, Sarso Deleted WHR detail</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton149" runat="server" Font-Bold="true" Font-Size="10pt"
                        ForeColor="navy" ValidationGroup="lik2" OnClick="LinkButton149_Click">Chana, Masur, Sarso Warehouse Quality Check Delete detail</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton152" runat="server" Font-Bold="true" Font-Size="10pt"
                        ForeColor="navy" ValidationGroup="lik2" OnClick="LinkButton152_Click">Chana, Masur, Sarso Deleted WHR detail against Request Date</asp:LinkButton>
                </td>
            </tr>

            <tr style="background-color: #993300; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label4" runat="server" Text="Procurement Details" ForeColor="WhiteSmoke" Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton49" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton49_Click">Wheat-PSS 2015-16(Own,hired,JV)  </asp:LinkButton>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton41" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton41_Click">WHR Report with Menual WHR (Wheat Proc2015-16)</asp:LinkButton>


                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton38" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton38_Click">Procurment Work Status </asp:LinkButton>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton39" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton39_Click">Procurment Work Status 2015-16 </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton44" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton44_Click">Total Procurement 2015 on Godown hired type </asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton51" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton51_Click">Paddy Procurement(Own,Hired,JV)2015-16</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton52" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton52_Click">Total Paddy Procurement2015-16(with manual whr)</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px; height: 28px;" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span></td>
                <td style="height: 28px">
                    <asp:LinkButton ID="LinkButton64" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton64_Click">Wheat Procurement 2016-17(Own,Hired,JV)</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton65" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton65_Click">Wheat Procurement 2016-17(All)</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton70" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton70_Click">Wheat Procurement 2016-17(WDRA,PVT-PEG,Steel Silo)</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton72" runat="server" Width="500px" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="~/Reports/States/Andriod_ReceivingStockProc.aspx" Font-Bold="true" OnClientClick="window.document.forms[0].target='_blank';"
                        Font-Size="10pt">Mobile/Tablet based Stock Procurement Details</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">12.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton73" runat="server" Width="500px" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="~/Reports/States/Andriod_ReceivingStockProc_Para.aspx" Font-Bold="true" OnClientClick="window.document.forms[0].target='_blank';"
                        Font-Size="10pt">Mobile/Tablet based Stock Procurement Details Godown wise</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">13.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton92" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton92_Click">Godown Type Wise Procurement (Wheat) Stock Details</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">14.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton99" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton99_Click">Kharif Procurement 2016-17 Commodity Wise</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">15.</span></td>
                <td>
                    <asp:LinkButton ID="lnkAllK" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="lnkAllK_Click">Kharif Procurement 2016-17 All Commodity</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">16.</span></td>
                <td>
                    <asp:LinkButton ID="lnkKharif2" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="lnkKharif2_Click">Paddy Procurement Kharif 2016-17</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">17.</span></td>
                <td>
                    <asp:LinkButton ID="lnkCGProc" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="lnkCGProc_Click">Coarse Grain Procurement Kharif 2016-17</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">18.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton13" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton13_Click">Paddy Procurement Kharif 2016-17 (Godown Wise)</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">19.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton11" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton11_Click2">Wheat Procurement 2017-18</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">20.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton17" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton17_Click">GodownTypeWise Wheat Procurement 2017-18</asp:LinkButton></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">21.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton27" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton27_Click1">District Wise Wheat Procurement 2017-18</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">22.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton29" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton29_Click1">Remaining Depositor Form For WHR Wheat Procurement 2017-18</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">23.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton74" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton74_Click">Arhar Procurement 2017-18</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">24.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton75" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton75_Click">Onion Procurement 2017-18</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">25.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton101" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton101_Click">Pulses Procurement 2017-18</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">26.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton112" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton112_Click">Kharif Procurement 2017-18(Commodity Wise)</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">27.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton113" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton113_Click">Paddy Procurement Kharif 2017-18</asp:LinkButton></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">28.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton124" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton124_Click">Wheat Procurement 2018-19</asp:LinkButton></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">29.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton126" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton126_Click">Pending Depositor Form To Generating WHR Wheat Procurement 2018-19 (After Two Day)</asp:LinkButton></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">30.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton127" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton127_Click">Godown Capacity and Utilization (Manually Enter)</asp:LinkButton></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">31.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton131" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton131_Click">Wheat Procurement 2018-19(WLC)</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">32.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton132" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton132_Click">Wheat Procurement 2018-19(NON-WLC)</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">33.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton142" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton142_Click"> Wheat Procurement 2018-19 (District Wise WLC)</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">34.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton128" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton128_Click">GRAM Procurement 2018-19</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">35.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton129" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton129_Click">Sarson Procurement 2018-19</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">36.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton130" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton130_Click">Masoor Procurement 2018-19</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">37.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton143" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton143_Click">GRAM, Sarson, Masoor Procurement 2018-19 (District Wise WLC)</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">38.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton144" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton144_Click">GRAM, Sarson, Masoor Procurement 2018-19 (District Wise NON-WLC)</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">39.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton135" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton135_Click">District wise Summary of Chana,Sarson,Masoor Procurement 2018-19</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">40.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton136" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton136_Click"> Comparative Report of Provisional D.F , Final D.F & WHR Issued Quantity of Procured Chana,Sarson,Masoor</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">41.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton139" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton139_Click">Region, District wise Report of WHR Issued Quantity against Final D.F of Procured Chana,Sarson,Masoor</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">42.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton133" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton133_Click">WHR time Difference against Depositor form (After 36 Hour)</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">43.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton134" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton134_Click">WHR time Difference against Depositor form (After 36 Hour) between two dates</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">44.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton137" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton137_Click"> WHR time Difference against Depositor form (Hour Wise)</asp:LinkButton></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">45.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton138" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton138_Click"> Date Wise Hour Wise WHR Created against Depositor form Wheat Procurement 2018-19</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">46.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton147" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton147_Click">Region,District wise WHR status against Provisional Depositor Form</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">47.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton150" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton150_Click">Region,District and Society wise WHR status for Rejected Quantity</asp:LinkButton></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">48.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton151" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton151_Click">WHR Details against Depositor Form,Acceptance Note /TC Details (Wheat Procurement 2018-19) </asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">49.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton153" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton153_Click">Godown Wise Procurement 2018-19 Report (Wheat-PSS,Gram,Lentil,Sarsoo) </asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">50.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton159" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton159_Click">Kharif Procurement 2018-19 (Ground-Nut,Moong,RAM TIL,Urad,Tilli) </asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">51.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton161" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton161_Click">Coarse Grain Procurement 2018-19 (Bajra,Jowar) </asp:LinkButton></td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">52.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton162" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton162_Click">Paddy Procurement 2018-19 </asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">53.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton163" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton163_Click">Paddy Procurement 2018-19 Storage in CAP</asp:LinkButton></td>
            </tr>

            <%--                                                             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">54.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton164" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" >Procurement 2018-19 WHR Againt Rejection Note</asp:LinkButton></td>
               </tr>  --%>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">54.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton164" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton164_Click">Procurement 2018-19 WHR Againt Rejection Note</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">55.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton168" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton168_Click">Wheat Procurement 2019-20</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">56.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton199" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton199_Click">Dalhan e-WHR 2019-20</asp:LinkButton></td>
            </tr>

             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">57.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton224" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton224_Click">Agency Wise WHR Rabi 2021-22</asp:LinkButton></td>
            </tr>

            <tr style="background-color: Blue; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label6" runat="server" Text="Bhavantar Scheme Reports" ForeColor="WhiteSmoke" Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton109" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton109_Click">Commodity wise Stock Deposit Summary</asp:LinkButton>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton114" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton114_Click">WHR wise Stock Deposit Detail</asp:LinkButton>

                </td>
            </tr>
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
                    <asp:Label ID="Label14" runat="server" Text="Storage Charges Details" ForeColor="WhiteSmoke" Font-Bold="True" Font-Size="12pt"></asp:Label>
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
                        Font-Size="10pt" OnClick="LinkButton214_Click" >Payment August To December</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton215" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton215_Click" >Payment Jan To March</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton216" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton216_Click" >Pending Payment From MPSCSC </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">12.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton217" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton217_Click" >Pending Bill Details Before August</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">13.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton225" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton225_Click" >District Wise Pendancy at Various Level From August</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">14.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton226" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton226_Click" >District Wise Pendancy at Various Level From August(Only Amount)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">15.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton228" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton228_Click" >Storage/Rent Payment Pendency at Various Level From August(NEW)</asp:LinkButton>
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
            <tr style="background-color: #391200; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label11" runat="server" Text="Other Reports (Old)" ForeColor="WhiteSmoke" Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton36" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton36_Click">Region Wise (Branch Wise)Capacity and Utilization (Old)</asp:LinkButton>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton37" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton37_Click">Commodity Wise, CropYear Wise, Region Wise Capacity & Utilization (Old)</asp:LinkButton>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton57" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton57_Click">Region Wise Capacity & Utilization Between Dates(Own,hired,jv,PVt.PEG) (Old)</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton55" runat="server" ValidationGroup="link1"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton55_Click">Summary of Capacity & Utilization(Region Wise) Current</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton40" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton40_Click">Storage Type Wise Capacity </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span>
                </td>
                <td style="width: 36px">
                    <asp:LinkButton ID="LinkButton12" runat="server" OnClick="LinkButton12_Click" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" Width="500px">Current Stock Position at Branch (GoogleMap)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span>
                </td>
                <td>
                    <asp:LinkButton ID="lnk_allentryReport" runat="server" Font-Bold="true" Font-Size="10pt"
                        ForeColor="navy" OnClick="lnk_allentryReport_Click" ValidationGroup="lik2"
                        Width="350px">BranchWise Total WHR,DO,Gatepass Entry</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span></td>
                <td>

                    <asp:LinkButton ID="LinkButton67" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton67_Click">Godown Utilization in Graph Representation  </asp:LinkButton></td>
            </tr>
            <%--             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span></td>
                <td>
                         
                         <asp:LinkButton ID="LinkButton69" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="LinkButton69_Click"  >graphical Godown Utilization 2  </asp:LinkButton></td>
            </tr>--%>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton14" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton14_Click">RegionWise Commodity Details</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton7" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">State Level Wheat-PSS Gain Manual Entry Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton118" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton118_Click">JVS Agreemented Godown List</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">12.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton119" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton119_Click">Paddy Procurement 2017-18 (Own,JVS,Hired,PVT_PEG Godown)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">13.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton120" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton120_Click">Paddy Procurement 2017-18 (Markfed,CWC,FCI,SteelSilo Godown)</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">14.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton121" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton121_Click">Godown Wise Stored Quantity Against Registered Capacity (Wheat 2017)</asp:LinkButton>
                </td>
            </tr>
             <tr style="background-color: #391200; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label15" runat="server" Text="Tachnical Section Reports" ForeColor="WhiteSmoke" Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton218" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton218_Click">Sync Loss Gain Date Wise</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton227" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton227_Click">Region Wise Loss Gain(By Sync Table)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton219" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton219_Click">District Wise Loss Gain(By Sync Table)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton220" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton220_Click">Branch Wise Loss Gain(By Sync Table)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton221" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton221_Click">Godown Wise Loss Gain (By Sync Table)</asp:LinkButton>
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton222" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton222_Click">Branch Wise Loss Gain (Real Time)</asp:LinkButton>
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton223" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton223_Click">Godown Wise Loss Gain (Real Time)</asp:LinkButton>
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

