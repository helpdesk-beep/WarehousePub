<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="VarificationofGodownCapacaty.aspx.cs" Inherits="Accounting_VarificationofGodownCapacaty" %>

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
    <table>
        <tr>
            <td>
                    <div style="text-align:center"> <h2 style="font-size: 25px;text-transform: uppercase;font-weight: revert;color: blue;"> Storage Plan For Kharif 2021-22  </h2>
    </div>

        <div style="text-align:center"> <h2 style="font-size: 20px;text-transform: uppercase;font-weight: revert;color: red;">Note-: Mapping मे गोडाउनों को सम्मिलित  कराने  हेतु Select/Reject करे  </h2>
    </div>
            </td>

            <td>
             
                 <div style="text-align:center"> <asp:Button ID="show" runat="server" style="font-size:20px; color:green; font-weight:800" Text="List Of Selected/Rejected Godown For Mapping" OnClick="show_Click"  />  
                </div>
                     </td>
        </tr>
    </table>

   

         


    <asp:Panel ID="StoreGrid" runat="server"  >
      
       <%--     <table style="text-align:center;margin:0 auto; border:1px solid#000;" >
                <tr>
                    <td>  <asp:DropDownList ID="DdlDist" runat="server" AutoPostBack="true" OnTextChanged="DdlDist_TextChanged"></asp:DropDownList></td>
                  <td>  <asp:DropDownList ID="ddlbranch" runat="server" OnTextChanged="ddlbranch_TextChanged" AutoPostBack="true"></asp:DropDownList></td>
                </tr>
            </table>--%>
          
     


             <asp:GridView ID="GridView1"  CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" runat="server" AutoGenerateColumns="false" Visible="true" OnRowUpdating="GridView1_RowUpdating" Width="100%"  >
                                    <Columns>
                                        <asp:TemplateField>
                                         <HeaderTemplate>  
                                        
                                                    <th style="text-align: center;">क्र.</th>
                                                    <th style="text-align: center;">Godown_ID</th>
                                                    <th style="text-align: center;">Godown_Name</th>  
                                              <th style="text-align: center;">Godown_Maximum_Capacity</th>  
                                                    <th style="text-align: center;">Godown_Scientific_Capacity</th>  
                                             <th style="text-align: center;">Vacant Capacity (in Qtl.)</th> 
                                               <th style="text-align: center;">Godown_Available Storage For Kharif 2021-22</th> 
                                                    <th style="text-align: center;">Submit</th> 
                                             
                                                </tr>
                        
                                            </HeaderTemplate>
                                            <ItemTemplate>                                                    
                       
                        <td style="text-align: center;"><%# Container.DataItemIndex + 1 %></td>
                        <asp:HiddenField ID="hdngodownid" runat="server" Value='<%#Eval("Godown_ID") %>'/>
                        <td style="text-align: center;"><asp:Label ID="lblComment" runat="server" Text='<%#Eval("Godown_ID") %>'/> </td>
                        <td style="text-align: center;"><asp:Label ID="Label16" runat="server" Text='<%#Eval("Godown_Name") %>'/> </td>
                          <td style="text-align: center;"><asp:Label ID="Godownmcap" runat="server" Text='<%#Eval("Godown_Capacity") %>'/> </td>
                           <td style="text-align: center;"><asp:TextBox ID="GodownScientificcap" style="width: 70%;" runat="server" Text='<%#Eval("Godown_Scientific_Capacity") %>'/> </td>
                           <td style="text-align: center;"><asp:TextBox ID="backcapacity" ToolTip="In Qtl." style="width: 70%;"  runat="server"></asp:TextBox> </td>
                      <td style="text-align: center;"><asp:DropDownList ID="GodownCap" runat="server">
                          <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                          <asp:ListItem Text="Select" Value="Y"></asp:ListItem>
                           <asp:ListItem Text="Reject" Value="N"></asp:ListItem>
                                                      </asp:DropDownList></td>
                         <td style="text-align: center;"><asp:Button ID="btn_Update" runat="server" OnClientClick="return confirm('Do you want to ADD this Godown?');"  Text="Save" CommandName="Update"/>  </td>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
    </asp:Panel>
</asp:Content>

