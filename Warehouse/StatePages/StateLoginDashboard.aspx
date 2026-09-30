<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="StateLoginDashboard.aspx.cs" Inherits="StatePages_StateLoginDashboard" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
       
    .popbag
    {
    background-color:gray;
    filter:alpha(opacity=90);
    opacity:0.8;
    z-index:10000;
    }
    .modalpop
    {
    background-color:#FFFFFF;
    border-width:3px;
    border-color:Black;
    padding-top:10px;
    padding-left:10px;
    width:700px;
    height:500px;
    border-radius: 5px;
    text-shadow:yellow;
    }
    </style>
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
    background-color: #008CBA; 
    color: white; 
    border: 2px solid #008CBA;
}

.button2:hover {
    background-color: #008CBA;
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
    color: white;
    border: 2px solid #E47D21;
}

.button6:hover {
    background-color: #E47D21;
    color: white;
}

</style>

<fieldset style="width:900px ; height:650px ; border: 2px solid navy; margin-left:5px; margin-right:5px">
     <center>
        <div>
        
          <table style="width:100%;">
            <tr>
                <td align="center" style="border:#FFFFFF ; border-style:solid ; border-width:2px; background-color:#008CBA; height:25px;" colspan="4">
                <span style="color: #FFFFFF; font-weight: bolder; font-size: 18px">Dashboard</span>
                </td>
            </tr>
            <tr>
                <td align="right" style="height:10px;" align="right" colspan="4">
                    <asp:Label ID="Label4" runat="server" BorderStyle="Double" BorderWidth="2px" BorderColor="Red" Font-Size="12px" 
                             Text="Qty In M.T" Font-Bold="true"></asp:Label>                 
                </td>
            </tr>
          
            <tr>
             <%-- Start First Box--%>
             
            <td align="center" style="font-size:14px;">
            <div style="width: 250px; height:200px ; border-radius: 5px; " class="button button2">
                <table style="width:100%; ">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label1" runat="server" BorderStyle="Double" BorderWidth="2px" BorderColor="White" Font-Size="14px" 
                            Width="240px" Height="22px" Text="Paddy 2018-19"></asp:Label>                        
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
<%--<asp:Button ID="btnbmpassword" runat="server" Text="BM Password" />   --%>                             
                        </td>
                    </tr>                                                            
                </table>
            </div>
            </td>
            
            <%-- End First Box--%>
            
            <%-- Start Second Box--%>
            
            <td align="center" style="font-size:14px;">
            <div style="width: 250px; height:200px ;border-radius: 5px;"  class="button button1">
                <table style="width:100%;">
                    <tr>
                        <td colspan="2" style="font-weight:bold">
                            <asp:Label ID="Label11" runat="server" BorderStyle="Double" BorderWidth="2px" BorderColor="White" Font-Size="14px" 
                            Width="240px" Height="22px" Text="Dalhan Tilhan 2018-19"></asp:Label>                        
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
            </td>
        <%-- End Second Box--%>   
        
         <%-- Start third Box--%>   
                  
            <td align="center" style="font-size:14px;">
            <div style="width: 250px; height:200px ; border-radius: 5px; " class="button button3">
                <table style="width:100%;">
                    <tr>
                        <td colspan="2" style="font-weight:bold">
                            <asp:Label ID="Label2" runat="server" BorderStyle="Double" BorderWidth="2px" BorderColor="White" Font-Size="14px" 
                            Width="240px" Height="22px" Text="Coarse Grains 2018-19"></asp:Label>                        
                        </td>
                    </tr>
                    <tr>
                        <td style="height:10px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label18" runat="server" Text="Depositor Form Quantity"></asp:Label>                        
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
                            <asp:Label ID="Label20" runat="server"  Text="WHR Quantity"></asp:Label>                        
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
                            <asp:Label ID="Label22" runat="server" Text="WHR % Agint DF"></asp:Label>                        
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
            </td>
            
            <%-- End third Box--%>
             
            <%-- Start Forth Box--%>
            <td align="center" style="font-size:14px;">
            <div style="width: 250px; height:200px ; border-radius:5px; " class="button button6" > 
                <table style="width:100%;">
                    <tr>
                        <td colspan="2" style="font-weight:bold">
                            <asp:Label ID="Label3" runat="server" BorderStyle="Double" BorderWidth="2px" BorderColor="White" Font-Size="14px" 
                            Width="240px" Height="22px" Text="Wheat 2018-19"></asp:Label>                        
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
            </td>  
            
            <%-- End Forth Box--%>  
                               
            </tr>
                    <tr>
                        <td style="height:10px;">
                        </td>
                    </tr>           
            <tr>
            <%--  strat Chart first section  --%>
                <td colspan="2" style="vertical-align:top">
                          <table width="100%" style="background-color:White;">
                            <tr>
                                <td style="background-color:#FFF881; color:Black; font-size:10pt; font-weight:bold ;text-align:center; height:25px" >Godown Type Wise Paddy Storage 2018-19
                                </td>                            
                            </tr>   
                                                      
                            <tr>
                <td style="background-color:White;">
              
                <cc1:PieChart ID="PieChart1" runat="server" ChartHeight="300px"
                    ChartWidth="490px" ChartTitleColor="#0E426C" Font-Size="Small" BorderColor="Blue" BorderWidth="2px" BorderStyle="Solid">
                </cc1:PieChart>
                </td> 
                             
                
                                                                                                                                                                                                                                   
                             </tr>                                
                          </table>                 
                </td>
            <%--  End Chart first section  --%>
            
            <%--  strat Chart second section  --%>
                <td colspan="2">
                          <table width="100%" style="background-color:White;">
                            <tr>
                                <td style="background-color:#e884b4; color:White; font-size:10pt; font-weight:bold ; text-align:center; height:25px">Region Wise Godown Vacant Capacity
                                </td>                            
                            </tr>   
                            <tr>
                <td style="background-color:White;">
              
                <cc1:PieChart ID="countrychart" runat="server" ChartHeight="300px"
                    ChartWidth="490px" ChartTitleColor="#0E426C" Font-Size="6px" BorderColor="Blue" BorderWidth="2px" BorderStyle="Solid">
                </cc1:PieChart>
                </td>                                                                                                                                                                                                              
                             </tr>                                
                          </table>                 
                </td>
            <%--  End Chart section  --%>            
            </tr>  
            <tr>

            </tr>         
            
          </table>
</div>

 <%-- Start First Div Popup Data--%>
 
  <asp:Panel ID="pnllogin" runat="server" ScrollBars="Vertical" class="modalpop">
    <table id="modalpop"  width="98%">
    <tr>
    <td align="center" valign="top" style="color:White; font-weight:bold; background-color:#008CBA; height:25px; width:400px; font-size:14px; border-radius: 5px">
           &nbsp;&nbsp;&nbsp;&nbsp; Paddy Procurement 2018-19  
    </td>
    <td style="color:White; font-weight:bold; background-color:#008CBA; height:25px; width:40px ;border-radius:5px">
    <asp:Button ID="btncancelpwd" runat="server" Text="Close" BackColor="#008CBA" Font-Bold="true" Font-Size="12px" height="25px" ForeColor="White" 
    width="100%" BorderColor="#008CBA"/>
    </td>
    </tr>
    <tr>
        <td colspan="2" >
                <asp:GridView ID="GD_Paddy"  runat="server" DataKeyNames="District_Id" AutoGenerateColumns="False" Width="100%" Height="300px" Font-Size="10pt" 
                Font-Bold="true" BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px"  HeaderStyle-BorderColor="White"
                 CellPadding="2" CellSpacing="2" ShowFooter="true" >
    
                            <Columns>
    <asp:TemplateField HeaderText = "S No." ItemStyle-Width="40px">
        <ItemTemplate>
            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
    </asp:TemplateField>                            
                                <asp:BoundField DataField="District_Id" HeaderText="District_Id" ReadOnly="True" SortExpression="District_Id" Visible="false"/>
                                <asp:BoundField DataField="District_Name" HeaderText="District Name" ReadOnly="True" SortExpression="District_Name" ItemStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField="TotalDF" HeaderText="Total Depositor Form" ReadOnly="True" SortExpression="TotalDF" HeaderStyle-Width="120px"/>
                                <asp:BoundField DataField="TotalAcceptQty" HeaderText="Total Depositor Form Qty (In M.T)" ReadOnly="True" SortExpression="TotalAcceptQty" HeaderStyle-Width="120px"/>
                                <asp:BoundField DataField="totalwhr" HeaderText="Total WHR" ReadOnly="True" SortExpression="totalwhr" HeaderStyle-Width="120px"/>
                                <asp:BoundField DataField="TotalWHRQty" HeaderText="Total WHR Qty (In M.T)" ReadOnly="True" SortExpression="TotalWHRQty" HeaderStyle-Width="120px"/>                                                          
                            </Columns>
            
                                <FooterStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="15px" Font-Size="8pt" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                 <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle BackColor="#008CBA" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                Height="20px" Font-Size="10pt" /> <AlternatingRowStyle BackColor="#eeeeee" />
                             </asp:GridView>            
        </td>
    </tr>
    <tr>
        <td align="center">
            
        </td>
    </tr>
    </table>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btncancelpwd" TargetControlID="btnbmpassword" BackgroundCssClass="popbag" PopupControlID="pnllogin">
    </cc1:ModalPopupExtender>
    
    <cc1:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="btnbmpassword">
        <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnllogin" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
        </Animations>
    </cc1:AnimationExtender> 
    
 <%-- End Of First Div Popup Data--%>

 <%-- Start Second Div Popup Data--%>
 
  <asp:Panel ID="pnldalhantilhan" runat="server" ScrollBars="Vertical" class="modalpop">
    <table id="Table1"  width="98%">
    <tr>
    <td align="center" valign="top" style="color:White; font-weight:bold; background-color:#4CAF50; height:25px; width:400px; font-size:14px; border-radius: 5px">
           &nbsp;&nbsp;&nbsp;&nbsp; Dalhan Tilhan Procurement 2018-19  
    </td>
    <td style="color:White; font-weight:bold; background-color:#4CAF50; height:25px; width:40px ;border-radius:5px">
    <asp:Button ID="btnclosedalhan" runat="server" Text="Close" BackColor="#4CAF50" Font-Bold="true" Font-Size="12px" height="25px" ForeColor="White" 
    width="100%" BorderColor="#008CBA"/>
    </td>
    </tr>
    <tr>
        <td colspan="2" >
                <asp:GridView ID="GD_DalhanTilhan"  runat="server" DataKeyNames="District_Id" AutoGenerateColumns="False" Width="100%" Height="300px" Font-Size="10pt" 
                Font-Bold="true" BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px"  HeaderStyle-BorderColor="White"
                 CellPadding="2" CellSpacing="2" ShowFooter="true">
    
                            <Columns>
    <asp:TemplateField HeaderText = "S No." ItemStyle-Width="40px">
        <ItemTemplate>
            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
    </asp:TemplateField>                            
                                <asp:BoundField DataField="District_Id" HeaderText="District_Id" ReadOnly="True" SortExpression="District_Id" Visible="false"/>
                                <asp:BoundField DataField="District_Name" HeaderText="District Name" ReadOnly="True" SortExpression="District_Name" ItemStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField="TotalDF" HeaderText="Total Depositor Form" ReadOnly="True" SortExpression="TotalDF" HeaderStyle-Width="120px"/>
                                <asp:BoundField DataField="TotalAcceptQty" HeaderText="Total Depositor Form Qty (In M.T)" ReadOnly="True" SortExpression="TotalAcceptQty" HeaderStyle-Width="120px"/>
                                <asp:BoundField DataField="totalwhr" HeaderText="Total WHR" ReadOnly="True" SortExpression="totalwhr" HeaderStyle-Width="120px"/>
                                <asp:BoundField DataField="TotalWHRQty" HeaderText="Total WHR Qty (In M.T)" ReadOnly="True" SortExpression="TotalWHRQty" HeaderStyle-Width="120px"/>                                                          
                            </Columns>
            
                                <FooterStyle BackColor="#4CAF50" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="15px" Font-Size="8pt" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                 <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle BackColor="#4CAF50" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                Height="20px" Font-Size="10pt" /> <AlternatingRowStyle BackColor="#eeeeee" />
                             </asp:GridView>            
        </td>
    </tr>
    <tr>
        <td align="center">
            
        </td>
    </tr>
    </table>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" CancelControlID="btnclosedalhan" TargetControlID="lnkdalhantilhan" BackgroundCssClass="popbag" PopupControlID="pnldalhantilhan">
    </cc1:ModalPopupExtender>
    
    <cc1:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="lnkdalhantilhan">
        <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnldalhantilhan" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
        </Animations>
    </cc1:AnimationExtender> 
    
 <%-- End Of Second Div Popup Data--%>
 
 <%-- Start third Div Popup Data--%>
 
  <asp:Panel ID="pnlCorsegrain" runat="server" ScrollBars="Vertical" class="modalpop">
    <table id="Table2"  width="98%">
    <tr>
    <td align="center" valign="top" style="color:White; font-weight:bold; background-color:#f44336; height:25px; width:400px; font-size:14px; border-radius: 5px">
           &nbsp;&nbsp;&nbsp;&nbsp; Coarse Grains Procurement 2018-19  
    </td>
    <td style="color:White; font-weight:bold; background-color:#f44336; height:25px; width:40px ;border-radius:5px">
    <asp:Button ID="btnCorsegrainclose" runat="server" Text="Close" BackColor="#f44336" Font-Bold="true" Font-Size="12px" height="25px" ForeColor="White" 
    width="100%" BorderColor="#f44336"/>
    </td>
    </tr>
    <tr>
        <td colspan="2" >
                <asp:GridView ID="GD_Corasegrain"  runat="server" DataKeyNames="District_Id" AutoGenerateColumns="False" Width="100%" Height="300px" Font-Size="10pt" 
                Font-Bold="true" BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px"  HeaderStyle-BorderColor="White"
                 CellPadding="2" CellSpacing="2" ShowFooter="true">
    
                            <Columns>
    <asp:TemplateField HeaderText = "S No." ItemStyle-Width="40px">
        <ItemTemplate>
            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
    </asp:TemplateField>                            
                                <asp:BoundField DataField="District_Id" HeaderText="District_Id" ReadOnly="True" SortExpression="District_Id" Visible="false"/>
                                <asp:BoundField DataField="District_Name" HeaderText="District Name" ReadOnly="True" SortExpression="District_Name" ItemStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField="TotalDF" HeaderText="Total Depositor Form" ReadOnly="True" SortExpression="TotalDF" HeaderStyle-Width="120px"/>
                                <asp:BoundField DataField="TotalAcceptQty" HeaderText="Total Depositor Form Qty (In M.T)" ReadOnly="True" SortExpression="TotalAcceptQty" HeaderStyle-Width="120px"/>
                                <asp:BoundField DataField="totalwhr" HeaderText="Total WHR" ReadOnly="True" SortExpression="totalwhr" HeaderStyle-Width="120px"/>
                                <asp:BoundField DataField="TotalWHRQty" HeaderText="Total WHR Qty (In M.T)" ReadOnly="True" SortExpression="TotalWHRQty" HeaderStyle-Width="120px"/>                                                          
                            </Columns>
            
                                <FooterStyle BackColor="#f44336" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="15px" Font-Size="8pt" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                 <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle BackColor="#f44336" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                Height="20px" Font-Size="10pt" /> <AlternatingRowStyle BackColor="#eeeeee" />
                             </asp:GridView>            
        </td>
    </tr>
    <tr>
        <td align="center">
            
        </td>
    </tr>
    </table>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" CancelControlID="btnCorsegrainclose" TargetControlID="lnkCorsegrain" BackgroundCssClass="popbag" PopupControlID="pnlCorsegrain">
    </cc1:ModalPopupExtender>
    
    <cc1:AnimationExtender ID="AnimationExtender2" runat="server" TargetControlID="lnkCorsegrain">
        <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnlCorsegrain" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
        </Animations>
    </cc1:AnimationExtender> 
    
 <%-- End Of third Div Popup Data--%>
 
 <%-- Start forth Div Popup Data--%>
 
  <asp:Panel ID="pnlwheat" runat="server" ScrollBars="Vertical" class="modalpop">
    <table id="Table3"  width="98%">
    <tr>
    <td align="center" valign="top" style="color:White; font-weight:bold; background-color:#E47D21; height:25px; width:400px; font-size:14px; border-radius: 5px">
           &nbsp;&nbsp;&nbsp;&nbsp; Wheat Procurement 2018-19  
    </td>
    <td style="color:White; font-weight:bold; background-color:#E47D21; height:25px; width:40px ;border-radius:5px">
    <asp:Button ID="btnwheatclose" runat="server" Text="Close" BackColor="#E47D21" Font-Bold="true" Font-Size="12px" height="25px" ForeColor="White" 
    width="100%" BorderColor="#E47D21"/>
    </td>
    </tr>
    <tr>
        <td colspan="2" >
                <asp:GridView ID="GD_wheatpopup"  runat="server" DataKeyNames="District_Id" AutoGenerateColumns="False" Width="100%" Height="300px" Font-Size="10pt" 
                Font-Bold="true" BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px"  HeaderStyle-BorderColor="White"
                 CellPadding="2" CellSpacing="2" ShowFooter="true">
    
                            <Columns>
    <asp:TemplateField HeaderText = "S No." ItemStyle-Width="40px">
        <ItemTemplate>
            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
    </asp:TemplateField>                            
                                <asp:BoundField DataField="District_Id" HeaderText="District_Id" ReadOnly="True" SortExpression="District_Id" Visible="false"/>
                                <asp:BoundField DataField="District_Name" HeaderText="District Name" ReadOnly="True" SortExpression="District_Name" ItemStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField="TotalDF" HeaderText="Total Depositor Form" ReadOnly="True" SortExpression="TotalDF" HeaderStyle-Width="120px"/>
                                <asp:BoundField DataField="TotalAcceptQty" HeaderText="Total Depositor Form Qty (In M.T)" ReadOnly="True" SortExpression="TotalAcceptQty" HeaderStyle-Width="120px"/>
                                <asp:BoundField DataField="totalwhr" HeaderText="Total WHR" ReadOnly="True" SortExpression="totalwhr" HeaderStyle-Width="120px"/>
                                <asp:BoundField DataField="TotalWHRQty" HeaderText="Total WHR Qty (In M.T)" ReadOnly="True" SortExpression="TotalWHRQty" HeaderStyle-Width="120px"/>                                                          
                            </Columns>
            
                                <FooterStyle BackColor="#E47D21" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="15px" Font-Size="8pt" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                 <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle BackColor="#E47D21" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                Height="20px" Font-Size="10pt" /> <AlternatingRowStyle BackColor="#eeeeee" />
                             </asp:GridView>            
        </td>
    </tr>
    <tr>
        <td align="center">
            
        </td>
    </tr>
    </table>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" CancelControlID="btnwheatclose" TargetControlID="lnkwheat" BackgroundCssClass="popbag" PopupControlID="pnlwheat">
    </cc1:ModalPopupExtender>
    
    <cc1:AnimationExtender ID="AnimationExtender3" runat="server" TargetControlID="lnkwheat">
        <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnlwheat" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
        </Animations>
    </cc1:AnimationExtender> 
    
 <%-- End Of forth Div Popup Data--%>
 
                      
     </center>
</fieldset>
</asp:Content>

