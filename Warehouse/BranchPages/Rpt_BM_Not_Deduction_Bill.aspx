<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Rpt_BM_Not_Deduction_Bill.aspx.cs" Inherits="BranchPages_Rpt_BM_Not_Deduction_Bill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
         
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="background-color: #0bb6e6; height: 25px">
                                <td colspan="4" align="center">
                                    <asp:Label ID="lblhead" runat="server" Text="शाखा प्रबंधक द्वारा गोदाम के जिन  बिलो का डिडक्शन नहीं किया गया हैं उनकी जानकारी " Font-Size="12pt"
                                        ForeColor="whitesmoke" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 10px">
                                </td>
                            </tr>                           
                              r>
                               <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>                            
                            <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>
                         
                            <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text=""
                                        Font-Size="10pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px">
                                </td>
                            </tr>
                           
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    <asp:GridView ID="gv_whr" runat="server" AutoGenerateColumns="false" Width="100%"
                                        Font-Size="10pt">
                                        <Columns>      
                                             <asp:TemplateField HeaderText="Region" ItemStyle-Width="10">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWhr_No" runat="server" Text='<%# Eval("Region") %>' />
                                            </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField HeaderText="District" ItemStyle-Width="10">
                                            <ItemTemplate>
                                             <asp:Label ID="lblWHR_Issue_Date" runat="server" Text='<%# Eval("District") %>' />
                                            </ItemTemplate>
                                                 </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Branch" ItemStyle-Width="10">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStack_ID" runat="server" Text='<%# Eval("Branch") %>' />
                                            </ItemTemplate>
                                              </asp:TemplateField>
                                             <asp:TemplateField HeaderText="godown_Name" ItemStyle-Width="10">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStack_Name" runat="server" Text='<%# Eval("godown_Name") %>' />
                                            </ItemTemplate>
                                              </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Godown_ID" ItemStyle-Width="10">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBagBalance" runat="server" Text='<%# Eval("Godown_ID") %>' />
                                            </ItemTemplate>
                                               </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Commodity Name" ItemStyle-Width="10">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCommodity_Name" runat="server" Text='<%# Eval("Commodity_Name") %>' />
                                            </ItemTemplate>
                                               </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Crop_Year" ItemStyle-Width="10">
                                            <ItemTemplate>
                                             <asp:Label ID="lblWeightBalance" runat="server" Text='<%# Eval("Crop_Year") %>' />
                                            </ItemTemplate>
                                               </asp:TemplateField>
                                             
                                            <asp:TemplateField HeaderText="Financial_Year" ItemStyle-Width="10">
                                            <ItemTemplate>
                                            <asp:Label ID="txtFAQ_Stock" runat="server" Text='<%# Eval("Financial_Year") %>' />
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Month" ItemStyle-Width="10">
                                            <ItemTemplate>
                                            <asp:Label ID="txtNon_FAQ_Stock" runat="server" Text='<%# Eval("Month") %>' />
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Bill Number" ItemStyle-Width="10">
                                            <ItemTemplate>
                                            <asp:Label ID="txtDCC_Stock" runat="server" Text='<%# Eval("NoOfBillPayment") %>' />
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Bill Amount" ItemStyle-Width="10">
                                            <ItemTemplate>
                                            <asp:Label ID="txtDCC_Stock" runat="server" Text='<%# Eval("BillAmtPTG") %>' />
                                            </ItemTemplate>
                                            </asp:TemplateField>

                                           <%-- <asp:TemplateField HeaderText="DCC Stock" ItemStyle-Width="10">
                                            <ItemTemplate>
                                            <asp:Button ID="btn_Update" runat="server" Text="Update" OnClientClick="return confirm('Do you want to Update this Stock Balance?');" CommandName="Update"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>--%>
                                           
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
                         
                            <tr>
                                <td style="height: 15px">
                                </td>
                            </tr>
                            <%--<tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnsave" runat="server" Text="Submit" Width="120px" CssClass="BTNBLUE"
                                        ValidationGroup="SaveValid" OnClick="btnsave_Click"/>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Width="120px" CssClass="BTNBLUE"
                                        CausesValidation="false" Visible="false" onclick="btnPrint_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="SaveValid" />
                                </td>
                            </tr>--%>
                        </table>
                    </div>
               
        </center>
    </fieldset>
</asp:Content>

