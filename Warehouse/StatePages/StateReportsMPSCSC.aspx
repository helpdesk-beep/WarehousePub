<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMasterMPSCSC.master" AutoEventWireup="true" CodeFile="StateReportsMPSCSC.aspx.cs" Inherits="StatePages_StateReportsMPSCSC" Title="MPSCSC Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 1000px; margin-left: 0px; height:475px">
        <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 1px; border-color: Green"
            border="1px">
            <tr style="background-color: #0bb6e6; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="lblRegionReports" runat="server" Text="State Report " ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr style="background-color:Maroon; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label3" runat="server" Text="Dalhan/Tilhan WHR Report 2020-21" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton6" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton6_Click">Branch wise Chana,Masoor and Sarson e-WHR Report</asp:LinkButton></td>
               </tr>
                <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton1_Click">Chana,Masoor and Sarson e-WHR Submission Report</asp:LinkButton></td>
               </tr>
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton2" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="LinkButton2_Click">Pending Bill Amount From August 2020 Report</asp:LinkButton></td>
               </tr>
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton3" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="LinkButton3_Click">Report for Stock Position District wise-Year wise</asp:LinkButton></td>
               </tr>
              
            <%--<tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span>
                </td>
                <td>
                    &nbsp;
                       <asp:LinkButton ID="LinkButton45" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="LinkButton45_Click"   >Godown Wise Capacity Utilization  </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton36" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="LinkButton36_Click"    >Godown Capacity Utilization(Region Wise) </asp:LinkButton>
                   
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span>
                </td>
                <td>
                    &nbsp;
                                               
                     <asp:LinkButton ID="LinkButton37" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="LinkButton37_Click"        >Godown Capacity Utilization Commodity Wise Region Wise</asp:LinkButton>
                   
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
                <td>
                    &nbsp;
                                                                   
                    <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton4_Click">Registered Operator Details (Issue Centre)</asp:LinkButton>
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
                    &nbsp;
                    <asp:LinkButton ID="LinkButton26" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt"  Width="400px" OnClick="LinkButton26_Click">Depositor wise WHR Report(ALL)</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="lnk_allentryReport" runat="server" Font-Bold="true" Font-Size="10pt"
                        ForeColor="navy"  ValidationGroup="lik2" 
                        Width="350px" OnClick="lnk_allentryReport_Click">BranchWise total WHR,DO,Gatepass Entry</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton19" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="~/Reports/States/Rpt_Deliveryissuedetails.aspx" Font-Bold="true"
                        Font-Size="10pt" Width="500px">Godownwise Receipt & Issue Details Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton28" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="LinkButton28_Click"   > Commodity Wise Depositor wise WHR Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span>
                </td>
                <td style="width: 36px">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton30" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt"  Width="500px"   > Commodity Wise Crop Year Wise  WHR Report</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span>
                </td>
  <td>
                     <asp:LinkButton ID="LinkButton43" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt"  >Godown wise WHR Current Details </asp:LinkButton>
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span>
                </td>
                <td>
                    &nbsp;
                  
                    <asp:LinkButton ID="LinkButton24" runat="server" ValidationGroup="lik2" ForeColor="navy"
                        PostBackUrl="#" Font-Bold="true" Font-Size="10pt">Districtwise Acceptance with WHR No. Details Report</asp:LinkButton>
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton31" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt"   >District Wise Current Godown status</asp:LinkButton>
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton32" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt"    >Pending Receiving Details</asp:LinkButton>
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span></td></td>
                <td>
                    <asp:LinkButton ID="LinkButton33" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt"    >Commodity Wise current capacity(State) </asp:LinkButton>
                 </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton34" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt"     >Region Wise Godown List </asp:LinkButton>
                        </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">12.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton35" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt"      >Crop yearly commodity wise WHR Report </asp:LinkButton>
                   </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">13.</span></td>
                <td>
                         <asp:LinkButton ID="LinkButton48" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt"  >Branch wise Capacity Utilization  </asp:LinkButton>
                     
                     </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">14.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton53" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt"    >Godown wise crop yearly commodity details </asp:LinkButton></td>
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
                        ForeColor="navy"  ValidationGroup="lik2"
                        Width="272px">Delete WHR,DO,Gatepass Report</asp:LinkButton>
                                                            </td>
            </tr>
           
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                  <asp:LinkButton ID="LinkButton47" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" >Total deletion and print reset detail branch wise  </asp:LinkButton>
                
                      </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton23" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Pending Delivery Order Gatepass Entry(with operator Details)</asp:LinkButton>
                                                            </td>
            </tr>
              
             <tr style="background-color: #993300; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label4" runat="server" Text="Procurement Details" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                          <asp:LinkButton ID="LinkButton51" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="LinkButton51_Click" >Paddy Procurement 2015-16</asp:LinkButton></td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                           <asp:LinkButton ID="LinkButton52" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" >Total Paddy Procurement2015-16(with manual whr)</asp:LinkButton></td>
            </tr>--%>
        </table>
    </div>
</asp:Content>
