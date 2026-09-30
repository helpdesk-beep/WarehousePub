<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMasterNafed.master" AutoEventWireup="true" CodeFile="GetNfdWHR.aspx.cs" Inherits="StatePages_GetNfdWHR" Title="Search WHR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td colspan="6" align="center" valign="top">
                                    <fieldset style="width: 980px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">View WHR Details</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="4">
                                                            <span style="color: Navy; font-size: 10pt; font-weight: bold">Note :- Record Will be
                                                                Search on the basis of WHR No.</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="4">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4" align="center">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                      
                                                         <td style="width: 500px" colspan="4" align="center">
                                                            Enter WHR Number <asp:TextBox ID="txtWHRSerach" runat="server"></asp:TextBox>
                                                            
                                                            <asp:Button ID="Btn_Search" runat="server" Text="Search" Width="100px"
                                CssClass="BTNBLUE" onclick="Btn_Search_Click" />
                                                        </td>
                                                       
                                                    </tr>
                                        
                                                </table>
                                            </div>
                                        </center>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px" colspan="4">
                                </td>
                            </tr>
              
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    <asp:Panel ID="panelContainer" runat="server" Height="380px" ScrollBars="Vertical"
                                        Width="100%" BorderColor="navy" BorderWidth="1px">
                                        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="10pt"
                                            DataKeyNames="Depositor_WHR_Id" 
                                           >
                                            <Columns>
                                                <asp:BoundField DataField="District_Name" HeaderText="District">
                                                    <ItemStyle Width="100px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Branch" HeaderText="Branch">
                                                    <ItemStyle Width="100px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="60px" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="Depositor_WHR_Id" HeaderText="WHR No.">
                                                    <ItemStyle Width="120px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="whrdate" HeaderText="WHR Date">
                                                    <ItemStyle Width="100px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                      <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name">
                                                    <ItemStyle Width="200px" HorizontalAlign="center"/>
                                                      <HeaderStyle Width="100px" HorizontalAlign="center"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Bags" HeaderText="No. of Bags">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Qty" HeaderText="Quantity (in Qtls.)">
                                                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name">
                                                    <ItemStyle Width="400px" HorizontalAlign="center" VerticalAlign="Bottom" />
                                                </asp:BoundField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" />
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                Height="20px" Font-Size="10pt" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 15px" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    
                                  
                                </td>
                            </tr>
                        </table>
                    </div>
              <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
        </center>
    </fieldset>
</asp:Content>

