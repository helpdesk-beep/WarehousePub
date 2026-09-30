<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_Edit_Reservation.aspx.cs" Inherits="Accounting_frm_Edit_Reservation" Title="Untitled Page" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                     <tr>
                        <td align="center" colspan="4">
                            <asp:Label ID="lbl_msg" runat="server" Text="No Record Found" Font-Bold="true" ForeColor="Red" Font-Size="10pt" Visible="false" ></asp:Label>
                        </td>
                    </tr>
                    <tr id="pnlGrid" runat="server" visible="true">
                        <td colspan="4" align="center" valign="top">
                            <fieldset style="width: 1050px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td align="center" valign="middle">
                                                    <asp:Label ID="lbl_head" runat="server" Text="Reservation Detail" ForeColor="whitesmoke" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top">
                                                    <asp:GridView ID="GvuEditReservation" runat="server" AutoGenerateColumns="false"
                                                        CellPadding="4" GridLines="both" AllowPaging="True" DataKeyNames="Receipt_no"
                                                        Width="100%" Font-Size="9pt" AllowSorting="True" PageSize="20" 
                                                        onrowcancelingedit="GvuEditReservation_RowCancelingEdit" 
                                                        onrowediting="GvuEditReservation_RowEditing" 
                                                        onrowupdating="GvuEditReservation_RowUpdating">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Receipt No">
                                                      <ItemTemplate>
                                                           <asp:Label ID="lblrno" runat="server" Text='<%#Bind("Receipt_no") %>'></asp:Label>
                                                      </ItemTemplate>
                                                      <EditItemTemplate>
                                                            <asp:TextBox ID="txtrno" Width="110px" runat="server" Text='<%#Bind("Receipt_no") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                               </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Depositor Name">        
                                                      <ItemTemplate>
                                                           <asp:Label ID="lblDepositor_Name" runat="server" Text='<%#Bind("Depositor_Name") %>'></asp:Label>
                                                      </ItemTemplate>
                                               </asp:TemplateField>
                                                           
                                                            <asp:TemplateField HeaderText="Commodity Name">
                                                      <ItemTemplate>
                                                           <asp:Label ID="lblCommodity_ID" runat="server" Text='<%#Bind("Commodity_Name") %>'></asp:Label>
                                                      </ItemTemplate>
                                               </asp:TemplateField>
                                                          
                                                               <asp:TemplateField HeaderText="Quantity">        
                                                      <ItemTemplate>
                                                           <asp:Label ID="lblQuantity" runat="server" Text='<%#Bind("Quantity") %>'></asp:Label>
                                                      </ItemTemplate>
                                               </asp:TemplateField>
                                                           
                                                               <asp:TemplateField HeaderText="Bags">        
                                                      <ItemTemplate>
                                                           <asp:Label ID="lblBags" runat="server" Text='<%#Bind("Bags") %>'></asp:Label>
                                                      </ItemTemplate>
                                               </asp:TemplateField>
                                                           
                                                            <asp:TemplateField HeaderText="From Date">        
                                                      <ItemTemplate>
                                                           <asp:Label ID="lblFromD" runat="server" Text='<%#Bind("FromD") %>'></asp:Label>
                                                      </ItemTemplate>
                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Upto Date">
                                                      <ItemTemplate>
                                                           <asp:Label ID="lbltodate" runat="server" Text='<%#Bind("ToD") %>'></asp:Label>
                                                      </ItemTemplate>
                                                      <EditItemTemplate>
                                                            <asp:TextBox ID="txttodate" Width="80px" runat="server" Text='<%#Bind("ToD") %>'></asp:TextBox>
                                                           <cc1:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" 
                                                             runat="server" TargetControlID="txttodate"/>
                                                      </EditItemTemplate>
                                               </asp:TemplateField>
                                                       
                                                             <asp:TemplateField HeaderText="Net Amount">        
                                                      <ItemTemplate>
                                                           <asp:Label ID="lblNet_Amount" runat="server" Text='<%#Bind("Net_Amount") %>'></asp:Label>
                                                      </ItemTemplate>
                                               </asp:TemplateField>
                                                            
                                                                <asp:TemplateField HeaderText="Godown">        
                                                      <ItemTemplate>
                                                           <asp:Label ID="lblGodown" runat="server" Text='<%#Bind("Godown") %>'></asp:Label>
                                                      </ItemTemplate>
                                               </asp:TemplateField>
                                                                
                                                             <asp:TemplateField HeaderText="Credit Amt">        
                                                      <ItemTemplate>
                                                           <asp:Label ID="lblCredit_amt" runat="server" Text='<%#Bind("Credit_amt") %>'></asp:Label>
                                                      </ItemTemplate>
                                               </asp:TemplateField>
                                                            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="BTNBLUE" ButtonType="Button" />
                                                            
                                                        </Columns>
                                                        <FooterStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Transparent" />
                                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                            Height="20px" Font-Size="10pt" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                         </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
</asp:Content>

