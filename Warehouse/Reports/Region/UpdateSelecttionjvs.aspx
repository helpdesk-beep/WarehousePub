<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="UpdateSelecttionjvs.aspx.cs" Inherits="Reports_Region_UpdateSelecttionjvs" %>

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

    <div style="text-align:center"> <h2 style="font-size: 25px;text-transform: uppercase;font-weight: revert;color: red;">Jvs Offier की वह  श्रेणीयाँ  जो  गलत  सम्मलित हो गए हे उन्हे सही करे </h2>
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
                                                    <th style="text-align: center;">region</th>
                                                    <th style="text-align: center;">District_Name</th>  
                                              <th style="text-align: center;">Branch_Name</th>  
                                                    <th style="text-align: center;">Reg_ID</th>  
                                                    <th style="text-align: center;">Godown_Name</th> 
                                             <th style="text-align: center;">Total_Godown_Capacity </th> 
                                               <th style="text-align: center;">श्रेणी</th>  
                                                <th style="text-align: center;">Date</th>  
                                             
                             <th style="text-align: center;">श्रेणी चुने  </th>

                                               <th style="text-align: center;">Update श्रेणी </th> 
                                             
                                                </tr>
                        
                                            </HeaderTemplate>
                                            <ItemTemplate>                                                    
                       
                        <td style="text-align: center;"><%# Container.DataItemIndex + 1 %></td>
                        <asp:HiddenField ID="hdngodownid" runat="server" Value='<%#Eval("Reg_ID") %>'/>
                                              
                        <td style="text-align: center;"><asp:Label ID="lblComment" runat="server" Text='<%#Eval("region") %>'/> </td>
                        <td style="text-align: center;"><asp:Label ID="Label16" runat="server" Text='<%#Eval("District_Name") %>'/> </td>
                          <td style="text-align: center;"><asp:Label ID="Godownmcap" runat="server" Text='<%#Eval("DepotName") %>'/> </td>
                           <td style="text-align: center;"><asp:Label ID="GodownScientificcap" runat="server" Text='<%#Eval("Reg_ID") %>'/> </td>
                        <td style="text-align: center;"><asp:Label ID="Label1" runat="server" Text='<%#Eval("Warehouse_Name") %>'/> </td>
                          <td style="text-align: center;"><asp:Label ID="Textbox1" runat="server" Text='<%#Eval("WareHouse_Cap") %>'/> </td>
                           <td style="text-align: center;"><asp:Label ID="backcapacity" runat="server" Text='<%#Eval("Cho") %>'></asp:Label> </td>
                          <td style="text-align: center;"><asp:Label ID="lable1" runat="server" Text='<%#Eval("Insert_Date") %>'></asp:Label> </td>
                                         <td style="text-align: center;">
                                 <asp:DropDownList ID="ddlflag" runat="server" >
                                      <asp:ListItem Text="Select-श्रेणी" Value="0"></asp:ListItem>
                                     <asp:ListItem Text="अ " Value="A"></asp:ListItem>
                                      <asp:ListItem Text="ब " Value="B"></asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                         <td style="text-align: center;"><asp:Button ID="btn_Update" runat="server" Visible="false" Text="Update" OnClientClick="return confirm('Do you want to Update this श्रेणी?');" CommandName="Update"/>  </td>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
    </asp:Panel>
</asp:Content>

