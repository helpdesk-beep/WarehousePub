<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="MPSCSC_Reports.aspx.cs" Inherits="IssueCenterLevel_Storage_MPSCSC_Reports" Title="MPSCSC Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width: 1050px; margin-left: 0px">
        <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 1px; border-color: Green"
            border="1px">
            <tr style="background-color: #0bb6e6; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="lblRegionReports" runat="server" Text="State Level MPSCSC Stock Report" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
                  <tr>
                <td style="width: 10px; height: 28px;" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td style="height: 28px">
                      <asp:LinkButton ID="LinkButton4" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton4_Click">Wheat Stock Report Till Date</asp:LinkButton></td>
            </tr>
              <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton5" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton5_Click">Paddy Stock Report Till Date</asp:LinkButton></td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton6" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton6_Click">Rice Stock Report Till Date</asp:LinkButton></td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton7" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton7_Click">Maize Stock Report Till Date</asp:LinkButton></td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton8" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton8_Click">Coarse Grains Stock Report Till Date</asp:LinkButton></td>
            </tr>
                <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                <td>
                      <asp:LinkButton ID="lncCropWiseMpscsc" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="lncCropWiseMpscsc_Click">Crop Year Wise MPSCSC Current Stock Report</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span>
                </td>
                <td>
                          <asp:LinkButton ID="LinkButton97" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton97_Click">Crop Year Wise Gunny Stock Report</asp:LinkButton>  
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span>
                </td>
                <td>
                          <asp:LinkButton ID="LinkButton11" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton11_Click1">Crop Year Wise Rice Stock Report</asp:LinkButton>  
                </td>
            </tr>
                            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span></td>
                <td>
                      <asp:LinkButton ID="Lnk13" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="Lnk13_Click">Crop Year & District Wise Wheat Stock Report</asp:LinkButton></td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span></td>
                <td>
                      <asp:LinkButton ID="lnkTillWheat" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="lnkTillWheat_Click">Region Wise Wheat Stock Report Till Date</asp:LinkButton></td>
            </tr>
                          <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span></td>
                <td>
                      <asp:LinkButton ID="lnkTillRice" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" OnClick="lnkTillRice_Click">Region Wise Rice Stock Report Till Date</asp:LinkButton></td>
            </tr>
                           <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">12.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton1_Click">Commodity Storage Type Wise Current Stock Report Till Date</asp:LinkButton></td>
            </tr>
                             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">13.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton3" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton3_Click">Commodity and Crop Year Wise Stock Report Till Date</asp:LinkButton></td>
            </tr>
                                 <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">14.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton9" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton9_Click">Issue Center & Branch Wise Wheat Stock Report</asp:LinkButton></td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">15.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton10" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton10_Click">Issue Center & Branch Wise Paddy Stock Report</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">16.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton12" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton12_Click">Issue Center & Branch Wise Rice Stock Report</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">17.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton13" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton13_Click">Issue Center & Branch Wise Maize Stock Report</asp:LinkButton></td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">18.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton14" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton14_Click">Issue Center & Branch Wise Coarse Grains Stock Report</asp:LinkButton></td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">19.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton15" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton15_Click">Issue Center & Branch Wise Salt Stock Report</asp:LinkButton></td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">20.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton16" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton16_Click">Issue Center & Branch Wise Sugar Stock Report</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">21.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton17" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton17_Click">Issue Center & Branch Wise Gunny Stock Report</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">22.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton18" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton18_Click">Godown Type Wise Difference B/W Gate Pass Issue Date & Entry Date</asp:LinkButton></td>
            </tr>
               <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">23.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton19" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton19_Click">Godown Wise Stock Issue Detail Against MPSCSC DO</asp:LinkButton></td>
            </tr>
                <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">24.</span></td>
                <td>
                      <asp:LinkButton ID="LinkButton2" runat="server" ValidationGroup="lik2" 
                          ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkButton2_Click1">Crop Year Wise - Commodity Wise MPSCSC Current Stock Report</asp:LinkButton></td>
            </tr>            
             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">25.</span></td>
                <td>
                      <asp:LinkButton ID="LinkAllC" runat="server" ValidationGroup="lik2" ForeColor="navy" Font-Bold="true" 
                      Font-Size="10pt" onclick="LinkAllC_Click">Commodity Wise Branch Wise Available Stock Status</asp:LinkButton></td>
            </tr>
            
                      <%--  <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">26.</span>
                </td>
                <td style="width: 36px; height: 18px;">
                    <asp:LinkButton ID="LinkButton27" runat="server" ValidationGroup="lik2" ForeColor="navy" Font-Bold="true" 
                     Font-Size="10pt" onclick="LinkButton27_Click" Width="400px" > WHR Report(MPSCSC)</asp:LinkButton>
                </td>
            </tr>--%>
            
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">26.</span>
                </td>
                <td>
                   <asp:LinkButton ID="LinkButton29" runat="server" ValidationGroup="lik2" ForeColor="navy" Font-Bold="true" 
                   Font-Size="10pt" onclick="LinkButton29_Click"  > Commodity Wise WHR Report(MPSCSC)</asp:LinkButton>
                </td>
            </tr>
<%--            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">27.</span></td>
                <td>
                <asp:LinkButton ID="LinkButton74" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" OnClientClick="window.document.forms[0].target='_blank';"
                        Font-Size="10pt" PostBackUrl="~/Reports/States/Rpt_MPSCSC_Commodity_Region.aspx">WHR Report(MPSCSC) Commodity Region Wise</asp:LinkButton></td>
            </tr>--%>
<%--            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">28.</span></td>
                <td>
                <asp:LinkButton ID="LinkButton75" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" OnClientClick="window.document.forms[0].target='_blank';"
                        Font-Size="10pt" PostBackUrl="~/Reports/States/Rpt_MPSCSC_Commodity_State.aspx">WHR Report(MPSCSC) Commodity (State)</asp:LinkButton></td>
            </tr>--%>
            
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">27.</span>
                </td>
                <td>
                          <asp:LinkButton ID="LinkButton46" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton46_Click" >Godown Wise Current Stock Report </asp:LinkButton>  
                </td>
            </tr>
            
                        <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">28.</span>
                </td>
                <td>
                          <asp:LinkButton ID="LinkButton20" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton20_Click" >MPSCSC Current Stock With Closing Balance</asp:LinkButton>  
                </td>
            </tr>
                              <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">29.</span>
                </td>
                <td>
                          <asp:LinkButton ID="LinkButton21" runat="server" ValidationGroup="lik2" PostBackUrl="~/GIS_Base_MPSCSC_CurretnStock.aspx" 
                        ForeColor="navy" Font-Bold="true" OnClientClick="window.document.forms[0].target='_blank';"
                        Font-Size="10pt">MPSCSC GIS Based Current Stock</asp:LinkButton>  
                </td>
            </tr>
  <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">30.</span>
                </td>
                <td>
                          <asp:LinkButton ID="LinkButton22" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton22_Click" >CropYear Wise - Commodity Wise Stock Report</asp:LinkButton>  
                </td>
            </tr>                            
        </table>
    </div>
</asp:Content>

