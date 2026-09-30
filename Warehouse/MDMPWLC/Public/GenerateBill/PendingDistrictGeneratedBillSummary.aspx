<%@ Page Title="" Language="C#" MasterPageFile="~/MDMPWLC/MasterPages/MasterPage2.master" AutoEventWireup="true" CodeFile="PendingDistrictGeneratedBillSummary.aspx.cs" Inherits="Administration_Public_PendingDistrictGeneratedBillSummary" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="row" style="margin-bottom: 10px;">
        <div class="col-md-12 mg-t-10">
            <div id="div20" runat="server" visible="true" style="padding-top: 1%;">
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="True" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
                    <Columns>
                        <asp:TemplateField HeaderText="क्र.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                            </ItemTemplate>
                            <ItemStyle Width="1%" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <div style="text-align: center; font-size: 16px;">
                                    Region Name
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="text-align: center; font-weight: bolder; color: Blue;">
                                    <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/MDMPWLC/Public/GenerateBill/PendingBranchGeneratedBillSummary.aspx?ID="+ (Eval("District_Id").ToString())%>'
                                        title="Regionnm Name" Text=' <%# Eval("District") %>' ForeColor="Blue"></asp:HyperLink>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                            <HeaderTemplate>
                                <div style="text-align: center; font-size: 16px;">
                                    Pending of Bill Generation
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblqty1" runat="server" Text='<%# Eval("PendingNoOfGenerateBill") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <div style="text-align: right;">
                                    <asp:Label ID="lblTotalqty1" runat="server" Font-Bold="true" />
                                </div>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                            <HeaderTemplate>
                                <div style="text-align: center; font-size: 16px;">
                                    Amount
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNoofBillGenerateAmt" runat="server" Text='<%# Eval("NoofBillGenerateAmt") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <div style="text-align: right;">
                                    <asp:Label ID="lblTotalqty2" runat="server" Font-Bold="true" />
                                </div>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>--%>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="alert alert-danger" style="text-align: center; margin: auto; margin-top: 10px; margin-bottom: 5px; font-size: 10pt; color: Green">
                            WARNING: No Records Found
                        </div>
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

