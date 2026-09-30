<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Rpt_Payment_Received_but_BM_Not_Deduction_Bill.aspx.cs" Inherits="BranchPages_Rpt_Payment_Received_but_BM_Not_Deduction_Bill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
         
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="background-color: #0bb6e6; height: 25px">
                                <td colspan="4" align="center">
                                    <asp:Label ID="lblhead" runat="server" Text="जमाकर्ता के द्वारा भुगतान करने के पश्चात शाखा प्रबंधक द्वारा गोदाम के जिन  बिलो का डिडक्शन नहीं किया गया हैं उनकी जानकारी " Font-Size="12pt"
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
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <%--<asp:TemplateField HeaderText="District Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/Reports/Region/Rpt_Branch_Wise_Pandancy_at_Varius_Level_after_recieved_payment.aspx?DistrictId="+ (Eval("District_Id").ToString())%>'
                                            title="District Name" Text=' <%# Eval("District_Name") %>' ForeColor="Blue"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                                <asp:BoundField DataField="DepotName" HeaderText="Branch" HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown"  HeaderStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="Hired_Type" HeaderText="Godown Type" HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Bill_Number" HeaderText="Storage Bill Number" HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Crop_Year" HeaderText="Crop_Year" HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Financial_Year" HeaderText="Financial_Year" HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Month" HeaderText="Month" HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Payable_Amount" HeaderText="Amount Received From MPSCSC" HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="OtherDeduction" HeaderText="Amount Deduction From MPSCSC" HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PaymentReceivingDate" HeaderText="Payment Receiving Date" HeaderStyle-HorizontalAlign="Right" />
                                           
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

