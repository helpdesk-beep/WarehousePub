<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="UpdateShowGodownvarificationcap.aspx.cs" Inherits="Reports_Region_UpdateShowGodownvarificationcap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <script  type="text/javascript">  

           function ConfirmOnDelete() {
               if (confirm("Are you sure want to Delete This Inspection ?") == true)
                   return true;
               else
                   return false;
           }


       </script>

    <div style="text-align:center"> <h2 style="font-size: 25px;text-transform: uppercase;font-weight: revert;color: red;">वह गोडाउन जो  Mapping मे गलत  सम्मलित हो गए हे उन्हे सही करे </h2>
    </div>

    <table>
        <tr>
            <td>
                <label id="lbl"  runat="server">Select District</label>
                <asp:DropDownList ID="ddldist" runat="server" OnSelectedIndexChanged="ddldist_SelectedIndexChanged" AutoPostBack="true"> </asp:DropDownList>
            </td>

                  <td>
                <label id="Label2"  runat="server">Select Branch</label>
                <asp:DropDownList ID="ddlbranchname" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="searchid" runat="server" Text="Search" OnClick="searchid_Click" />
            </td>
        </tr>
    </table>

    <asp:Panel ID="StoreGrid" runat="server"  >
             <asp:GridView ID="GridView1"  CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" runat="server" AutoGenerateColumns="false" Visible="true" OnRowUpdating="GridView1_RowUpdating" Width="100%"  >
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
                                             
                                               <th style="text-align: center;">Update Godown_Available Storage </th> 
                                             
                                                </tr>
                        
                                            </HeaderTemplate>
                                            <ItemTemplate>                                                    
                       
                        <td style="text-align: center;"><%# Container.DataItemIndex + 1 %></td>
                        <asp:HiddenField ID="hdngodownid" runat="server" Value='<%#Eval("Godown_ID") %>'/>
                                              
                        <td style="text-align: center;"><asp:Label ID="lblComment" runat="server" Text='<%#Eval("Godown_ID") %>'/> </td>
                        <td style="text-align: center;"><asp:Label ID="Label16" runat="server" Text='<%#Eval("Godown_Name") %>'/> </td>
                          <td style="text-align: center;"><asp:Label ID="Godownmcap" runat="server" Text='<%#Eval("GoDown_Cap") %>'/> </td>
                           <td style="text-align: center;"><asp:Textbox ID="GodownScientificcap" runat="server" Text='<%#Eval("GoDown_Scient_Cap") %>'/> </td>
                           <td style="text-align: center;"><asp:Textbox ID="backcapacity" runat="server" Text='<%#Eval("GoDown_Vacant_Cap") %>'></asp:Textbox> </td>
                          <td style="text-align: center;"><asp:Label ID="lable1" runat="server" Text='<%#Eval("flag") %>'></asp:Label> </td>
                             <td style="text-align: center;">
                                 <asp:DropDownList ID="ddlflag" runat="server" >
                                      <asp:ListItem Text="Select-Storage-flag" Value="0"></asp:ListItem>
                                     <asp:ListItem Text="Select" Value="Y"></asp:ListItem>
                                      <asp:ListItem Text="Reject" Value="N"></asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                         <td style="text-align: center;"><asp:Button ID="btn_Update" runat="server" Text="Update" OnClientClick="return confirm('Do you want to Update this Godown?');" CommandName="Update"/>  </td>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
    </asp:Panel>
</asp:Content>

