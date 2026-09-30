<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="DltChoiseFilling.aspx.cs" Inherits="StatePages_DltChoiseFilling" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    
 <link type="text/css" rel="Stylesheet" href="css/style_new.css" />
    
    <script src="../JS/Jquery.3.6.0.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         


    <div style="text-align:center"> <h2 style="font-size: 25px;text-transform: uppercase;font-weight: revert;color: gray;"> ऐसे  Choice Feeling जो गलत चुन ली गई हों उन्हें Delete करें </h2>
    </div>

    <asp:Panel ID="StoreGrid" runat="server"  >
      
            <table style="text-align:center;margin:0 auto; border:1px solid#000;" >
                <tr>
                    <td>Enter Registration_ID <asp:TextBox ID="regidserch" runat="server"></asp:TextBox></td>
                  <td> <asp:Button ID="searchreg" runat="server" Text="Search" OnClick="searchreg_Click" /><td>
                </tr>
            </table>
          
     


             <asp:GridView ID="GridView1"  CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" runat="server" AutoGenerateColumns="false" Visible="true" OnRowUpdating="GridView1_RowUpdating" Width="100%"  >
                                    <Columns>
                                        <asp:TemplateField>
                                         <HeaderTemplate>  
                                        
                                                    <th style="text-align: center;">क्र.</th>
                                               <th style="text-align: center;">Reg_ID</th>
                                                    <th style="text-align: center;">District_Name</th>
                                                    <th style="text-align: center;">DepotName</th>  
                                               <th style="text-align: center;">Warehouse_Name</th> 
                                             <th style="text-align: center;">Choice</th> 
                                             
                                                </tr>
                        
                                            </HeaderTemplate>
                                            <ItemTemplate>                                                    
                       
                        <td style="text-align: center;"><%# Container.DataItemIndex + 1 %></td>
                        <asp:HiddenField ID="regid" runat="server" Value='<%#Eval("Reg_ID") %>'/>
                         <td style="text-align: center;"><asp:Label ID="Label3" runat="server" Text='<%#Eval("Reg_ID") %>'/> </td>
                        <td style="text-align: center;"><asp:Label ID="lblComment" runat="server" Text='<%#Eval("District_Name") %>'/> </td>
                        <td style="text-align: center;"><asp:Label ID="Label16" runat="server" Text='<%#Eval("DepotName") %>'/> </td>
                                                 <td style="text-align: center;"><asp:Label ID="Label1" runat="server" Text='<%#Eval("Warehouse_Name") %>'/> </td>
                                                 <td style="text-align: center;"><asp:Label ID="Label2" runat="server" Text='<%#Eval("Choice") %>'/> </td>
                         <td style="text-align: center;"><asp:Button ID="btn_Update" Visible="false" runat="server" Text="Delete" CommandName="Update"/>  </td>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
    </asp:Panel>
</asp:Content>

