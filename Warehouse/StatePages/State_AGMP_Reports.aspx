<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AGMP_Master.master" AutoEventWireup="true" CodeFile="State_AGMP_Reports.aspx.cs" Inherits="StatePages_State_AGMP_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 1000px; margin-left: 0px; height:475px">
        
        <table style="width:100%;">
            <%--<tr>
                <td align="center" style="border:#FFFFFF ; border-style:solid ; border-width:2px; background-color:#008CBA; height:25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">
                        Procurement Reports 
                </td>
            </tr>--%>
                      
           <tr>
<%-- Start Forth Box--%>
            <%--<td align="center" style="font-size:14px;">
            <div style="width: 250px; height:160px ; border-radius:5px; " class="button button6" > 
                <table style="width:100%;">
                    <tr>
                        <td colspan="2" style="font-weight:bold">
                            <asp:Label ID="Label3" runat="server" BorderStyle="Double" BorderWidth="2px" BorderColor="White" Font-Size="14px" 
                            Width="240px" Height="22px" Text="Wheat 2019-20"></asp:Label>                        
                        </td>
                    </tr>
                    <tr>
                        <td style="height:10px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label24" runat="server" Text="Depositor Form Quantity"></asp:Label>                        
                        </td>
                        <td align="left">
                            <asp:Label ID="wlbldf" runat="server"  Text="Quantity"></asp:Label>                        
                        </td>                                                
                    </tr>
                    <tr>
                        <td style="height:5px;">
                        </td>
                    </tr>                     
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label26" runat="server" Text="WHR Quantity"></asp:Label>                        
                        </td>
                        <td align="left">
                            <asp:Label ID="wlblwhr" runat="server" Text="Quantity"></asp:Label>                        
                        </td>                                                
                    </tr>
                    <tr>
                        <td style="height:5px;">
                        </td>
                    </tr>                     
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label28" runat="server" Text="WHR % Agint DF"></asp:Label>                        
                        </td>
                        <td align="left">
                            <asp:Label ID="wlblper" runat="server" Text="Quantity"></asp:Label>                        
                        </td>                                                
                    </tr>
                    <tr>
                        <td style="height:10px;">
                        </td>
                    </tr>                                         
                    <tr>
                        <td align="center" colspan="2"><asp:LinkButton ID="lnkwheat" runat="server" align="right" 
                                ForeColor="White" Font-Size="10pt">View Details</asp:LinkButton>
                        </td>
                    </tr>                                                            
                </table>
             </div>
            </td>--%> 
             
            
            <%-- End Forth Box--%>

            <%-- Start First Box--%>
             
            <%--<td align="center" style="font-size:14px;">
            <div style="width: 250px; height:160px ; border-radius: 5px; " class="button button2">
                <table style="width:100%; ">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label1" runat="server" BorderStyle="Double" BorderWidth="2px" BorderColor="White" Font-Size="14px" 
                            Width="240px" Height="22px" Text="Dalhan Tilhan 2019-20"></asp:Label>                        
                        </td>
                    </tr>
                    <tr>
                        <td style="height:10px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label5" runat="server"  Text="Depositor Form Quantity"></asp:Label>                        
                        </td>
                        <td align="left">
                            <asp:Label ID="Plbldf" runat="server"  Text="Quantity"></asp:Label>                        
                        </td>                                                
                    </tr>
                    <tr>
                        <td style="height:5px;">
                        </td>
                    </tr>                    
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label7" runat="server" Text="WHR Quantity"></asp:Label>                        
                        </td>
                        <td align="left">
                            <asp:Label ID="PlblWHR" runat="server" Text="Quantity"></asp:Label>                        
                        </td>                                                
                    </tr>
                    <tr>
                        <td style="height:5px;">
                        </td>
                    </tr>                    
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label9" runat="server" Text="WHR % Agint DF"></asp:Label>                        
                        </td>
                        <td align="left">
                            <asp:Label ID="PlblPer" runat="server" Text="Quantity"></asp:Label>                        
                        </td>                                                
                    </tr>
                    <tr>
                        <td style="height:10px;">
                        </td>
                    </tr>                                         
                    <tr>
                        <td align="center" colspan="2">
                            <asp:LinkButton ID="btnbmpassword" runat="server" align="right" 
                                ForeColor="White">View Details</asp:LinkButton>                            
                        </td>
                    </tr>                                                            
                </table>
            </div>
            </td>--%>
            
            <%-- End First Box--%>                     
            
<%-- Start Second Box--%>
            
            <%--<td align="center" style="font-size:14px;">
            <div style="width: 250px; height:160px ;border-radius: 5px;"  class="button button1">
                <table style="width:100%;">
                    <tr>
                        <td colspan="2" style="font-weight:bold">
                            <asp:Label ID="Label11" runat="server" BorderStyle="Double" BorderWidth="2px" BorderColor="White" Font-Size="14px" 
                            Width="240px" Height="22px" Text="Coarse Grains 2019-20"></asp:Label>                        
                        </td>
                    </tr>
                    <tr>
                        <td style="height:10px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label12" runat="server"  Text="Depositor Form Quantity"></asp:Label>                        
                        </td>
                        <td align="left">
                            <asp:Label ID="DTlbldf" runat="server"  Text="Quantity"></asp:Label>                        
                        </td>                                                
                    </tr>
                    <tr>
                        <td style="height:5px;">
                        </td>
                    </tr>                    
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label14" runat="server"  Text="WHR Quantity"></asp:Label>                        
                        </td>
                        <td align="left">
                            <asp:Label ID="DTlblwhr" runat="server"  Text="Quantity"></asp:Label>                        
                        </td>                                                
                    </tr>
                    <tr>
                        <td style="height:5px;">
                        </td>
                    </tr>                     
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label16" runat="server"  Text="WHR % Agint DF"></asp:Label>                        
                        </td>
                        <td align="left">
                            <asp:Label ID="DTlblper" runat="server"  Text="Quantity"></asp:Label>                        
                        </td>                                                
                    </tr> 
                    <tr>
                        <td style="height:10px;">
                        </td>
                    </tr>                                         
                    <tr>
                        <td align="center" colspan="2"><asp:LinkButton ID="lnkdalhantilhan" runat="server" align="right" 
                                ForeColor="White"  Font-Size="10pt">View Details</asp:LinkButton>
                        </td>
                    </tr>                                                           
                </table>                                 
            </div>
            </td>--%>
        <%-- End Second Box--%>
         <%-- Start third Box--%>   
                  
            <%--<td align="center" style="font-size:14px;">
            <div style="width: 250px; height:160px ; border-radius: 5px; " class="button button3">
                <table style="width:100%;">
                    <tr>
                        <td colspan="2" style="font-weight:bold">
                            <asp:Label ID="Label2" runat="server" BorderStyle="Double" BorderWidth="2px" BorderColor="White" Font-Size="14px" 
                            Width="240px" Height="22px" Text="JVS Offered 2019-20"></asp:Label>                        
                        </td>
                    </tr>
                    <tr>
                        <td style="height:10px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label18" runat="server" Text="Offered Capacity"></asp:Label>                        
                        </td>
                        <td align="left">
                            <asp:Label ID="Clbldf" runat="server"  Text="Quantity"></asp:Label>                        
                        </td>                                                
                    </tr>
                    <tr>
                        <td style="height:5px;">
                        </td>
                    </tr>                    
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label20" runat="server"  Text="Inspection Capacity"></asp:Label>                        
                        </td>
                        <td align="left">
                            <asp:Label ID="clblwhr" runat="server"  Text="Quantity"></asp:Label>                        
                        </td>                                                
                    </tr>
                    <tr>
                        <td style="height:5px;">
                        </td>
                    </tr>                    
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label22" runat="server" Text="Agreement Capacity"></asp:Label>                        
                        </td>
                        <td align="left">
                            <asp:Label ID="clblper" runat="server"  Text="Quantity"></asp:Label>                        
                        </td>                                                
                    </tr>
                    <tr>
                        <td style="height:5px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="height:10px;">
                        </td>
                    </tr>                                         
                    <tr>
                        <td align="center" colspan="2"><asp:LinkButton ID="lnkCorsegrain" runat="server" align="right" 
                                ForeColor="White" Font-Size="10pt">View Details</asp:LinkButton>
                        </td>
                    </tr>                                                             
                </table>                                 
            </div>
            </td>--%>
            
            <%-- End third Box--%>   
               <tr>
                <td align="center" style="border:#FFFFFF ; border-style:solid ; border-width:2px;  background-color:#E47D21; height:25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">
                        Rabi Procurement 2021-22
                </td>
            </tr>
            <tr style="border:border: 1px solid navy;" >
                <td colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton21" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton21_Click"> 1. &nbsp;Wheat Procurement 2021-22 </asp:LinkButton>
                </td>
            </tr>
              <tr style="border:border: 1px solid navy;" >
                <td colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton22" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton22_Click"> 2. &nbsp;Dalhan Procurement 2021-22 </asp:LinkButton>
                </td>
            </tr>
             
                <tr style="border: border: 1px solid navy;">
                   <td colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton24" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton24_Click"> 4. &nbsp;Moong,Udad Procurement WHR 2021-22 </asp:LinkButton>
                   </td>
               </tr>
               <tr>
                <td align="center" style="border:#FFFFFF ; border-style:solid ; border-width:2px;  background-color:#E47D21; height:25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">
                        Kharif Procurement 2020-21
                </td>
            </tr>
            <tr >
                <td colspan="4" align="Left" style="border:1px solid Gray ; " class="auto-style1">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton18" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton18_Click"> 1. &nbsp;Paddy Procurement 2020-21 </asp:LinkButton>
                </td>
            </tr>
                <tr >
                <td colspan="4" align="Left" style="border:1px solid Gray ; " class="auto-style1">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton19" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton19_Click"> 2. &nbsp;Bajra Procurement 2020-21 </asp:LinkButton>
                </td>
            </tr>
                <tr >
                <td colspan="4" align="Left" style="border:1px solid Gray ; " class="auto-style1">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton20" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton20_Click"> 3. &nbsp;Jowar Procurement 2020-21 </asp:LinkButton>
                </td>
            </tr>
           <tr>
                <td align="center" style="border:#FFFFFF ; border-style:solid ; border-width:2px;  background-color:#E47D21; height:25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">
                        Wheat Procurement 2020-21
                </td>
            </tr>
            <tr style="border:border: 1px solid navy;" >
                <td colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton15" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton15_Click"> 1. &nbsp;Wheat Procurement 2020-21 </asp:LinkButton>
                </td>
            </tr>
              <tr style="border:border: 1px solid navy;" >
                <td colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton16" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton16_Click"> 2. &nbsp;Dalhan Procurement 2020-21 </asp:LinkButton>
                </td>
            </tr>
             
                    <tr>
                <td align="center" style="border:#FFFFFF ; border-style:solid ; border-width:2px;  background-color:#E47D21; height:25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">
                        Paddy Procurement 2019-20
                </td>
            </tr>
            <tr style="border:border: 1px solid navy;" >
                <td colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton12" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton12_Click"> 1. &nbsp;Paddy Procurement 2019-20 </asp:LinkButton>
                </td>
            </tr>
            <tr style="border:border: 1px solid navy;" >
                <td colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton13" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton13_Click"> 2. &nbsp;Paddy Procurement 2019-20 </asp:LinkButton>
                </td>
            </tr>
              <tr style="border:border: 1px solid navy;" >
                <td colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton14" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton14_Click"> 3. &nbsp;Paddy Procurement 2019-20 (MPWLC Only) </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" style="border:#FFFFFF ; border-style:solid ; border-width:2px;  background-color:#E47D21; height:25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">
                        Wheat Procurement 2019-20
                </td>
            </tr>
            <tr style="border:border: 1px solid navy;" >
                <td colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton1_Click"> 1. &nbsp;Wheat Procurement 2019-20 </asp:LinkButton>
                </td>
            </tr>
            <tr style="border:border: 1px solid navy ;" >
                <td colspan="4" align="Left" colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton4" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton4_Click"> 2. &nbsp;Wheat Procurement 2019-20 (District Wise) </asp:LinkButton>
                </td>
            </tr>
                         
            <tr>
                <td align="center" style="border:#FFFFFF ; border-style:solid ; border-width:2px; background-color:#E49A21; height:25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">
                        Dalhan Procurement 2019-20
                
                </td>
            </tr>
            <tr style="border:border: 1px solid navy ;" >
                <td colspan="4" align="Left"colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton2" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton2_Click"> 1. &nbsp;Dalhan e-WHR 2019-20 </asp:LinkButton>
                </td>
            </tr>
            <tr style="border:border: 1px solid navy ;" >
                <td colspan="4" align="Left" colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton3" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton3_Click"> 2. &nbsp;Dalhan e-WHR 2019-20 (District Wise) </asp:LinkButton>
                </td>
            </tr>
            <tr style="border:border: 1px solid navy ;" >
                <td colspan="4" align="Left" colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton5" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton5_Click"> 3. &nbsp;Dalhan e-WHR 2019-20 (District Wise and Godown Type Wise) </asp:LinkButton>
                </td>
            </tr> 
            
            
                                                                             
          </table>

    </div>
</asp:Content>

