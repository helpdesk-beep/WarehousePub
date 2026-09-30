<%@ Page Language="C#" MasterPageFile="~/MasterPage/CollectorMasterPage.master" AutoEventWireup="true" CodeFile="Collector_Reports.aspx.cs" Inherits="IssueCenterLevel_Storage_Collector_Reports" Title="Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width: 950px; margin-left: 0px">
        <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 1px; border-color: Green; background-image: url(../../images/images4.jpg)"
            border="1px">

<%--            <tr>
                <td colspan="2" style="text-align: center;border-collapse: collapse; border:solid 1px white; height: 24px;">
                    <span style="font-size: 9pt; color: maroon; font-family: Microsoft Sans Serif"><strong>
                        <asp:Label ID="lblRegionReports" runat="server" Text="Collector Report District Wise"></asp:Label></strong></span></td>
            </tr>--%>

                        <tr style="background-color:Gray; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label8" runat="server" Text="Collector Report District Wise" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr style="background-color:Teal; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label9" runat="server" Text="New Billing Reports" ForeColor="whitesmoke"
                     Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong></td>
                <td>
                    <a href="../../District/Rpt_SC_Pending_Bill_Submission.aspx" class="ancker" target="_blank"> Pending Bill For Submission to DM MPSCSC </a>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong></td>
                <td>
                    <a href="../../District/Rpt_District_Wise_Pending_Rent_Bill_Details_Summary.aspx" class="ancker" target="_blank"> Pending Rent Bill Generation MPSCSC </a>

                </td>
            </tr>
            <tr style="background-color:Teal; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label6" runat="server" Text="Procurement WHR Reports" ForeColor="whitesmoke"
                     Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong></td>
                <td>
                    <a href="../../District/Rpt_Procurement_Kharif2024_25_For_District.aspx" class="ancker" target="_blank"> Paddy Procurement 2024-25 </a>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong></td>
                <td>
                    <a href="../../District/Rpt_Procurement_Rabi2025_26_For_District.aspx" class="ancker" target="_blank"> Wheat-PSS Procurement 2025-26 </a>

                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong></td>
                <td>
                    <a href="../../District/Rpt_Procurement_CMS_2025_26.aspx" class="ancker" target="_blank"> Chana,Masoor, Sarson Procurement 2025-26 </a>

                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong></td>
                <td>
                    <a href="../../District/Rpt_Procurement_Moong_Urad_2025_26_District.aspx" class="ancker" target="_blank"> Moong Urad Procurement 2025-26 </a>

                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">5.</span></strong></td>
                <td>
                    <a href="../../District/Rpt_Procurement_Kharif2025_26_For_District.aspx" class="ancker" target="_blank"> Karif Procurement 2025-26 </a>

                </td>
            </tr>
            <tr style="background-color:Teal; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label4" runat="server" Text="Godown Details" ForeColor="whitesmoke"
                     Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
                <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                   <strong><span style="color: Navy">1.</span></strong></td>
        <td >
            <asp:LinkButton ID="LinkButton13" runat="server" ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="9pt"  ValidationGroup="lik2" 
                onclick="LinkButton13_Click">Branch Wise Godowns List</asp:LinkButton></td>
    </tr>
     <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                   <strong><span style="color: Navy">2.</span></strong></td>
        <td >
            <asp:LinkButton ID="LinkButton26" runat="server" ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="9pt"  ValidationGroup="lik2" onclick="LinkButton26_Click" 
               >Pvt. Godown/Silo Login List</asp:LinkButton></td>
    </tr>
            
            
            
                                    <tr style="background-color:Navy; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label1" runat="server" Text="Capacity and Utilization" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
    <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                   <strong><span style="color: Navy">1.</span></strong></td>
        <td >
            <asp:LinkButton ID="LinkButton7" runat="server" ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="9pt" OnClick="LinkButton7_Click" ValidationGroup="lik2">Owned Godowns Branch Wise Capacity and Utilization</asp:LinkButton></td>
    </tr>
    <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                   <strong><span style="color: Navy">2.</span></strong></td>
        <td >
            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton1_Click">JVS Godowns Branch Wise Capacity and Utilization</asp:LinkButton></td>
    </tr>
       <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                   <strong><span style="color: Navy">3.</span></strong></td>
        <td >
            <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton2_Click">WDRA Godowns Branch Wise Capacity and Utilization</asp:LinkButton></td>
    </tr>
           <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                   <strong><span style="color: Navy">4.</span></strong></td>
        <td >
            <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton3_Click">Steel Silo Branch Wise Capacity and Utilization</asp:LinkButton></td>
    </tr>
           <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                   <strong><span style="color: Navy">5.</span></strong></td>
        <td >
            <asp:LinkButton ID="LinkButton5" runat="server" ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton5_Click">PVT PEG Godowns Branch Wise Capacity and Utilization</asp:LinkButton></td>
    </tr>
               <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                <strong><span style="color: Navy">6.</span></strong></td>
           <td >
            <asp:LinkButton ID="LinkButton14" runat="server" ForeColor="navy" Font-Bold="true"
                        
                        Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton14_Click" >Godown Wise Capacity and Utilization</asp:LinkButton></td>
    </tr>
     <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                <strong><span style="color: Navy">7.</span></strong></td>
           <td >
            <asp:LinkButton ID="LinkButton25" runat="server" ForeColor="navy" Font-Bold="true"
                        
                        Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton25_Click">Godown wise Commodity Summary</asp:LinkButton></td>
    </tr>
    
                                        <tr style="background-color:Maroon; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label2" runat="server" Text="Stock Reports" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            
                    <tr>
            <td style="width: 49px; text-align: center; font-size: 10px"> 
                <strong><span style="color: Navy">1.</span></strong></td>
           <td>
           <asp:LinkButton ID="LinkButton16" runat="server" ForeColor="navy" Font-Bold="true" 
                   Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton16_Click" >
               Commodity Wise MPSCSC Available Stock
           </asp:LinkButton></td>
        </tr> 
            <tr>
            <td style="width: 49px; text-align: center; font-size: 10px"> 
                <strong><span style="color: Navy">2.</span></strong></td>
           <td>
           <asp:LinkButton ID="LinkButton23" runat="server" ForeColor="navy" Font-Bold="true" 
                   Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton23_Click" >
              Depositor Wise Commodity Wise  Available Stock (Non-MPSCSC)
           </asp:LinkButton></td>
        </tr>
              <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                   <strong><span style="color: Navy">3.</span></strong></td>
        <td >
            <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton4_Click">Godown Wise Opening Closing B/W Two Dates</asp:LinkButton></td>
    </tr>
    <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                   <strong><span style="color: Navy">4.</span></strong></td>
        <td >
            <asp:LinkButton ID="LinkButton6" runat="server" ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton6_Click">Commodity Wise WHR Wise Stock Report</asp:LinkButton></td>
    </tr>
            <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                   <strong><span style="color: Navy">5.</span></strong></td>
        <td >
            <asp:LinkButton ID="LinkButton10" runat="server" ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="9pt" ValidationGroup="lik2" 
                onclick="LinkButton10_Click">Godown Wise Available Stock Report</asp:LinkButton></td>
    </tr>
                
        <tr>
            <td style="width: 49px; text-align: center; font-size: 10px"> 
                <strong><span style="color: Navy">6.</span></strong></td>
           <td>
           <asp:LinkButton ID="LinkButton11" runat="server" ForeColor="navy" Font-Bold="true" 
                   Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton11_Click">
                CropYear Wise WHR Wise Available Stock Report
           </asp:LinkButton></td>
       </tr>
       
        <tr>
            <td style="width: 49px; text-align: center; font-size: 10px"> 
                <strong><span style="color: Navy">7.</span></strong></td>
           <td>
           <asp:LinkButton ID="LinkButton12" runat="server" ForeColor="navy" Font-Bold="true" 
                   Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton12_Click">
                MPSCSC Available Stock (Godown Wise)
           </asp:LinkButton></td>
        </tr>       
        <tr>
            <td style="width: 49px; text-align: center; font-size: 10px"> 
                <strong><span style="color: Navy">8.</span></strong></td>
           <td>
           <asp:LinkButton ID="LinkButton15" runat="server" ForeColor="navy" Font-Bold="true" 
                   Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton15_Click" >
                CropYear Wise - Commodity Wise Available Stock
           </asp:LinkButton></td>
        </tr>  
         <tr>
            <td style="width: 49px; text-align: center; font-size: 10px"> 
                <strong><span style="color: Navy">9.</span></strong></td>
           <td>
           <asp:LinkButton ID="LinkButton17" runat="server" ForeColor="navy" Font-Bold="true" 
                   Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton17_Click">
                Wheat Stock Report Till Date
           </asp:LinkButton></td>
        </tr>  
        <tr>
            <td style="width: 49px; text-align: center; font-size: 10px"> 
                <strong><span style="color: Navy">10.</span></strong></td>
           <td>
           <asp:LinkButton ID="LinkButton18" runat="server" ForeColor="navy" Font-Bold="true" 
                   Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton18_Click">
                Paddy Stock Report Till Date
           </asp:LinkButton></td>
        </tr>  
        <tr>
            <td style="width: 49px; text-align: center; font-size: 10px"> 
                <strong><span style="color: Navy">11.</span></strong></td>
           <td>
           <asp:LinkButton ID="LinkButton19" runat="server" ForeColor="navy" Font-Bold="true" 
                   Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton19_Click">
                Rice Stock Report Till Date
           </asp:LinkButton></td>
        </tr>  
         <tr>
            <td style="width: 49px; text-align: center; font-size: 10px"> 
                <strong><span style="color: Navy">12.</span></strong></td>
           <td>
           <asp:LinkButton ID="LinkButton20" runat="server" ForeColor="navy" Font-Bold="true" 
                   Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton20_Click">
                Maize Stock Report Till Date
           </asp:LinkButton></td>
        </tr>  
         <tr>
            <td style="width: 49px; text-align: center; font-size: 10px"> 
                <strong><span style="color: Navy">13.</span></strong></td>
           <td>
           <asp:LinkButton ID="LinkButton21" runat="server" ForeColor="navy" Font-Bold="true" 
                   Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton21_Click">
                Coarse Grains Stock Report Till Date
           </asp:LinkButton></td>
        </tr>  
         <tr>
            <td style="width: 49px; text-align: center; font-size: 10px"> 
                <strong><span style="color: Navy">14.</span></strong></td>
           <td>
           <asp:LinkButton ID="LinkButton24" runat="server" ForeColor="navy" Font-Bold="true" 
                   Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton24_Click">
                Gunny Stock Report Till Date
           </asp:LinkButton></td>
        </tr>  
             <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">15.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton133" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton133_Click">District Wise Stock Position in MT</asp:LinkButton>
                    </td>
                </tr>
 
                                            <tr style="background-color:Fuchsia; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label3" runat="server" Text="Procurement Reports" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                   <strong><span style="color: Navy">1.</span></strong></td>
        <td >
            <asp:LinkButton ID="LinkButton27" runat="server" ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="9pt" ValidationGroup="lik2" 
                onclick="LinkButton27_Click">Wheat Procurement 2017-18</asp:LinkButton></td>
    </tr>
     <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                   <strong><span style="color: Navy">2.</span></strong></td>
        <td >
            <asp:LinkButton ID="LinkButton8" runat="server" ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton8_Click">Paddy Procurement Kharif 2016-17</asp:LinkButton></td>
    </tr>

           
     <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                   <strong><span style="color: Navy">3.</span></strong></td>
        <td >
            <asp:LinkButton ID="LinkButton9" runat="server" ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton9_Click">Coarse Grain Procurement Kharif 2016-17</asp:LinkButton></td>
    </tr>

             <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                   <strong><span style="color: Navy">4.</span></strong></td>
        <td >
            <asp:LinkButton ID="Lnk_btn_paddy1920" runat="server" ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="9pt" ValidationGroup="lik2" onclick="Lnk_btn_paddy1920_Click">Paddy Procurement Kharif 2019-20</asp:LinkButton></td>
    </tr>

             <tr>
            <td style="width: 49px; text-align: center; font-size: 10px">
                   <strong><span style="color: Navy">5.</span></strong></td>
        <td >
            <asp:LinkButton ID="Lnk_btn_coarsegrn1920" runat="server" ForeColor="navy" Font-Bold="true"
                         
                        Font-Size="9pt" ValidationGroup="lik2" onclick="Lnk_btn_coarsegrn1920_Click">CoarseGrains Procurement Kharif 2019-20</asp:LinkButton></td>
    </tr>
     <tr style="background-color:Teal; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label5" runat="server" Text="Other Reports" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            
          <tr>
            <td style="width: 49px; text-align: center; font-size: 10px"> <strong><span style="color: Navy">1.</span></strong></td>
            <td><asp:LinkButton ID="LinkButton22" runat="server" ForeColor="navy" Font-Bold="true"
                Font-Size="9pt" ValidationGroup="lik2" onclick="LinkButton22_Click">Branch Wise - Commodity Wise Opening Closing Between Date</asp:LinkButton></td>
         </tr>

                <tr style="background-color: #af1d12; height: 25px">
                    <td colspan="2" align="center">
                        <asp:Label ID="Label7" runat="server" Text="Billing Reports New" ForeColor="whitesmoke"
                            Font-Bold="true" Font-Size="12pt"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">1.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton100" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton100_Click1">MPSCSC से लंबित भुगतान का विवरण </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">2.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton102" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton102_Click">MPSCSC से प्राप्त भुगतान के पश्यात विभिन्य स्तर पर लंबित बिलों की जानकारी </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">3.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton101" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton101_Click">Godown Wise MPSCSC से लंबित भुगतान का विवरण</asp:LinkButton>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">4.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton103" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton103_Click">क्षेत्रीय प्रबंधक के स्तर पर NEFT फाइल बनाने के लिए लंबित देयक</asp:LinkButton>
                    </td>
                </tr>

                 <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">5.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton117" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton117_Click">	Pendency Payment Details(After August)</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">6.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton124" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton124_Click">Godown Wise Payment Status(Only JVS)</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">7.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton128" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton128_Click">District Wise Payment Status(Only JVS)</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">8.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton129" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton129_Click">Payment Status of BOT Godowns (Filled Capacity)</asp:LinkButton>
                    </td>
                </tr>

                 <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">9.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton131" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton131_Click">District Wise Payment Status( Receiving,Pending and Pay to godown)</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">10.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton132" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton132_Click">ब्रांच मैनेजर द्वारा गोदामों के रेंट बिलो के लंबित कटोत्रा की जानकारी </asp:LinkButton>
                    </td>
                </tr>

                 <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">11.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton134" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton134_Click">District,Commodity Wise Payment Status( Receiving,Pending and Pay to godown) </asp:LinkButton>
                    </td>
                </tr>

                 <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">7.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton138" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton138_Click">Owned Godown Wise Payment Status</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">8.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton139" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton139_Click">Godown Type Wise Payment Status</asp:LinkButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px; color: Navy; font-weight: bold" align="center">9.</td>
                    <td>
                        <asp:LinkButton ID="LinkButton140" runat="server" Font-Bold="true"
                            Font-Size="10pt" ForeColor="navy"
                            ValidationGroup="link1" OnClick="LinkButton140_Click">ब्रांच मेनेजर द्वारा बनाने हेतु शेष स्टोरेज चार्जेज बिल</asp:LinkButton>
                    </td>
                </tr>
  </table>
  </div>
</asp:Content>