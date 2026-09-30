<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_View_Bill_Detail.aspx.cs" Inherits="Accounting_frm_View_Bill_Detail" Title="Untitled Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style=" width:100%">
<tr id="trRentBill" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
                                                    <center>
                                                           <div style="overflow: scroll; height: 400px; overflow-x: hidden">
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Storage Bill Detail"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="6" align="center">
                                                           <asp:GridView ID="gvMasterBill" runat="server" AutoGenerateColumns="False" 
                                                                    Font-Size="10pt"
                                            DataKeyNames="Bill_Number" onrowcommand="gvMasterBill_RowCommand" 
                                                                  >
                                            <Columns>
                                                <asp:TemplateField HeaderText="Bill No.">
                                                                         <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkBillNo" runat="server" Text='<%# Eval("Bill_Number") %>' CommandName="bill"
                                                                                Font-Underline="true" ForeColor="Blue" ToolTip="Click Me"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                                                                                           
                                                                        <ItemStyle HorizontalAlign="center" Font-Size="10pt"/>
                                                                        <ControlStyle Width="150px" />
                                                                    </asp:TemplateField>
                                                 <%--<asp:BoundField DataField="Bill_Number" HeaderText="Bill No.">
                                                    <ItemStyle Width="120px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="120px" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="Bill_Name" HeaderText="Bill Name">
                                                    <ItemStyle Width="120px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="300px" />
                                                </asp:BoundField>
                                                <%-- <asp:BoundField DataField="Bill_Type" HeaderText="Bill Type">
                                                    <ItemStyle Width="120px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="120px" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor Name">
                                                    <ItemStyle Width="200px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DateOfBill" HeaderText="Date Of Bill">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                      <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                
                                                
                                             <%--   <asp:BoundField DataField="To_Date" HeaderText="To Date">
                                                    <ItemStyle Width="200px" HorizontalAlign="center"/>
                                                      <HeaderStyle Width="100px" HorizontalAlign="center"/>
                                                </asp:BoundField>--%>
                                              <asp:BoundField DataField="Sub_Amount" HeaderText="Sub Amount">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Net_Amount" HeaderText="Net Amount">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                               
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" />
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                Height="20px" Font-Size="10pt" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView> 
                                                            </td>
                                                           </tr>
                                                           
    
                                                           </table>
                                                             
                                                            </div>
                                                            <table width="100%">
                                                            <tr>
                                                            <td align="center" colspan="2">
                                                            <%--<asp:Button ID="btnGenBill" runat="server" Text="Generate Bill" Visible="false" 
                                                                CssClass="BTNBLUE" onclick="btnGenBill_Click" />
                                                                <asp:Button ID="btncancel2" runat="server" Text="Cancel" Visible="false" Width="110px" 
                                                                CssClass="BTNBLUE" onclick="btncancel2_Click" />--%>
                                                            </td>
                                                           
                                                            </tr>
                                                            </table>
                                                            </center>
                                                            </fieldset>
                                                            </td>
</tr>
<tr id="trb1" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
                                                    <center>
                                                           <div style="overflow: scroll; height: 400px; overflow-x: hidden">
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           
                                                           <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Bills Detail"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td><asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click" Font-Underline="true" Font-Bold="true" ForeColor="Blue" /></td>
                                                           </tr>
                                                           <tr>
                                                           <td style=" background-color:Maroon" align="center"><asp:Label ID="lblbillname" runat="server" Text="" Font-Bold="True" Font-Size="15pt" ForeColor="white"></asp:Label></td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="6" align="center">
                                                           <asp:GridView ID="gvB" runat="server" AutoGenerateColumns="true"  AlternatingRowStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center"
                                                                    Font-Size="10pt">
                                            
                                            <FooterStyle BackColor="#CCCC99" />
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                Height="20px" Font-Size="10pt" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView> 
                                                            </td>
                                                           </tr>
                                                           
    
                                                           </table>
                                                             
                                                            </div>
                                                            <table width="100%">
                                                            <tr>
                                                            <td align="center" colspan="2">
                                                            <%--<asp:Button ID="btnGenBill" runat="server" Text="Generate Bill" Visible="false" 
                                                                CssClass="BTNBLUE" onclick="btnGenBill_Click" />
                                                                <asp:Button ID="btncancel2" runat="server" Text="Cancel" Visible="false" Width="110px" 
                                                                CssClass="BTNBLUE" onclick="btncancel2_Click" />--%>
                                                            </td>
                                                           
                                                            </tr>
                                                            </table>
                                                            </center>
                                                            </fieldset>
                                                            </td>
</tr>
</table>




</asp:Content>


