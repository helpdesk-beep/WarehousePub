<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="ShowGodownvarificationcap.aspx.cs" Inherits="Accounting_ShowGodownvarificationcap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  

        <div style="text-align:center"> <h2 style="font-size: 20px;text-transform: uppercase;font-weight: revert;color: red;"> वह गोडाउन जो  Mapping मे  सम्मलित हो गए हे</h2>
    </div>

          <div style="text-align:center"> <asp:Button ID="show" runat="server" style="font-size:20px;" Text="Back" OnClick="show_Click" />  
    </div>

    <asp:Panel ID="StoreGrid" runat="server"  >
      
       <%--     <table style="text-align:center;margin:0 auto; border:1px solid#000;" >
                <tr>
                    <td>  <asp:DropDownList ID="DdlDist" runat="server" AutoPostBack="true" OnTextChanged="DdlDist_TextChanged"></asp:DropDownList></td>
                  <td>  <asp:DropDownList ID="ddlbranch" runat="server" OnTextChanged="ddlbranch_TextChanged" AutoPostBack="true"></asp:DropDownList></td>
                </tr>
            </table>--%>
          
     


             <asp:GridView ID="GridView1"  CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" runat="server" AutoGenerateColumns="false" Visible="true"  Width="100%"  >
                                    <Columns>
                                        <asp:TemplateField>
                                         <HeaderTemplate>  
                                        
                                                    <th style="text-align: center;">क्र.</th>
                                                    <th style="text-align: center;">Godown_ID</th>
                                                    <th style="text-align: center;">Godown_Name</th>  
                                              <th style="text-align: center;">Godown_Maximum_Capacity</th>  
                                                    <th style="text-align: center;">Godown_Scientific_Capacity</th>  
                                             <th style="text-align: center;">Vacant Capacity </th> 
                                               <th style="text-align: center;"> Godown_Available Storage </th> 

                                            
                                
                                                   
                                             
                                                </tr>
                        
                                            </HeaderTemplate>
                                            <ItemTemplate>                                                    
                       
                        <td style="text-align: center;"><%# Container.DataItemIndex + 1 %></td>                      
                        <td style="text-align: center;"><asp:Label ID="lblComment" runat="server" Text='<%#Eval("Godown_ID") %>'/> </td>
                        <td style="text-align: center;"><asp:Label ID="Label16" runat="server" Text='<%#Eval("Godown_Name") %>'/> </td>
                          <td style="text-align: center;"><asp:Label ID="Godownmcap" runat="server" Text='<%#Eval("GoDown_Cap") %>'/> </td>
                           <td style="text-align: center;"><asp:Label ID="GodownScientificcap" runat="server" Text='<%#Eval("GoDown_Scient_Cap") %>'/> </td>
                           <td style="text-align: center;"><asp:Label ID="backcapacity" runat="server" Text='<%#Eval("GoDown_Vacant_Cap") %>'></asp:Label> </td>
                             <td style="text-align: center;"><asp:Label ID="Label1" runat="server" Text='<%#Eval("flag") %>'></asp:Label> </td>
                    
                       
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
    </asp:Panel>
</asp:Content>

