<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="StockBal_ReportForInspection.aspx.cs" Inherits="Reports_Branch_StockBal_ReportForInspection" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">
.button {
    background-color: #4CAF50; /* Green */
    border: none;
    color: white;
    padding: 0px 0px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 12px;
    font-weight:bold;
    margin: 4px 2px;
    -webkit-transition-duration: 0.4s; /* Safari */
    transition-duration: 0.4s;
    cursor: pointer;
}
.button1 {
    background-color: #4CAF50; 
    color: white; 
    border: 2px solid #4CAF50;
}

.button1:hover {
    background-color: #4CAF50;
    color: white;
}
.button2 {
    background-color: #F1B92D; 
    color: white; 
    border: 2px solid #F1B92D;
}

.button2:hover {
    background-color: #F1B92D;
    color: white;
}

.button3 {
    background-color: #f44336; 
    color: white; 
    border: 2px solid #f44336;
}

.button3:hover {
    background-color: #f44336;
    color: white;
}
.button6 {
    background-color: #E47D21;
    color: white;2301001
    border: 2px solid #E47D21;
}

.button6:hover {
    background-color: #E47D21;
    color: white;
}

</style>
<fieldset style="width:1000px ; height:470px; border: 2px solid navy;">
     <center>
        <div>
        
          <table style="width:100%;">

                      
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
          </tr>
            <tr>
                <td align="center" style="border:#FFFFFF ; border-style:solid ; border-width:2px;  background-color:#E47D21; height:25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">
                        Stock Balance Report For Inspection (Annexure B)
                </td>
            </tr>
            <tr style="border:border: 1px solid navy;" >
                <td colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton2" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton2_Click" >
                        1. &nbsp;Commodity Wise Available Stock Balance Report(Annexure A)
                   </asp:LinkButton>
                </td>
            </tr>            
            <tr style="border:border: 1px solid navy;" >
                <td colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton1_Click">
                        2. &nbsp;Godown Wise Stack Balance Report(Annexure B)
                   </asp:LinkButton>
                </td>
            </tr>
                        <tr style="border:border: 1px solid navy;" >
                <td colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton3" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton3_Click">
                        3. &nbsp;Depositor Wise Commodity Wise Stock Balance Report(Annexure C)
                   </asp:LinkButton>
                </td>
            </tr>

                <tr style="border:border: 1px solid navy;" >
                <td colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton4" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton4_Click" >
                        4. &nbsp;Wheat Stock Branch Report Till Date
                   </asp:LinkButton>
                </td>
            </tr>    
              
               <tr style="border:border: 1px solid navy;" >
                <td colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton5" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton5_Click" >
                        5. &nbsp;Rice Stock Branch Report Till Date
                   </asp:LinkButton>
                </td>
            </tr> 
               <tr style="border:border: 1px solid navy;" >
                <td colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton6" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton6_Click" >
                        6. &nbsp;  Paddy Stock Branch Report Till Date 
                   </asp:LinkButton>
                </td>
            </tr>        

               <tr style="border:border: 1px solid navy;" >
                <td colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton7" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton7_Click" >
                        7. &nbsp;Maize Stock Branch Report Till Date
                   </asp:LinkButton>
                </td>
            </tr> 

               <tr style="border:border: 1px solid navy;" >
                <td colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton8" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton8_Click" >
                        8. &nbsp;Coarse Gains Stock Branch Report Till Date
                   </asp:LinkButton>
                </td>
            </tr> 
<%--            <tr style="border:border: 1px solid navy ;" >
                <td colspan="4" align="Left" colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton4" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton4_Click">
                        2. &nbsp;Wheat Procurement 2019-20 (District Wise)
                   </asp:LinkButton>
                </td>
            </tr>
            
            <tr style="border:border: 1px solid navy ;" >
                <td colspan="4" align="Left" colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton7" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton7_Click">
                        3. &nbsp;e-WHR Wheat Procurement 2019-20
                   </asp:LinkButton>
                </td>
            </tr>  --%>                      
<%--            <tr>
                <td align="center" style="border:#FFFFFF ; border-style:solid ; border-width:2px; background-color:#E49A21; height:25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">
                        Dalhan Procurement 2019-20
                </td>
            </tr>
            <tr style="border:border: 1px solid navy ;" >
                <td colspan="4" align="Left"colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton2" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton2_Click">
                        1. &nbsp;Dalhan e-WHR 2019-20
                   </asp:LinkButton>
                </td>
            </tr>
            <tr style="border:border: 1px solid navy ;" >
                <td colspan="4" align="Left" colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton3" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton3_Click">
                        2. &nbsp;Dalhan e-WHR 2019-20 (District Wise)
                   </asp:LinkButton>
                </td>
            </tr>
            <tr style="border:border: 1px solid navy ;" >
                <td colspan="4" align="Left" colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton5" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton5_Click">
                        3. &nbsp;Dalhan e-WHR 2019-20 (District Wise and Godown Type Wise)
                   </asp:LinkButton>
                </td>
            </tr> 
            
            <tr style="border:border: 1px solid navy ;" >
                <td colspan="4" align="Left" colspan="4" align="Left" style="border:#FFFFFF ; border-style:solid ; border-width:1px; border-color:Gray; height:22px;">
                    &nbsp;
                    <asp:LinkButton ID="LinkButton6" runat="server"  ForeColor="navy" 
                        Font-Bold="true" Font-Size="10pt" onclick="LinkButton6_Click">
                        4. &nbsp;Dalhan e-WHR Online Submission Report 2019-20 (District Wise and Godown Type Wise)
                   </asp:LinkButton>
                </td>
            </tr>--%>                                                                        
          </table>
          
        </div>    
      </center>
 </fieldset>       
</asp:Content>

