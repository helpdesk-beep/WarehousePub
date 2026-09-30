<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Administration.master" AutoEventWireup="true" CodeFile="Dsc_By_Rm_DistrictGeneratedBillSummary.aspx.cs" Inherits="Administration_Public_Dsc_By_Rm_DistrictGeneratedBillSummary" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                    District Name
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="text-align: center; font-weight: bolder; color: Blue;">
                                    <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/Administration/Public/RM/BranchGeneratedBillSummary.aspx?ID="+ (Eval("District_Id").ToString())%>'
                                            title="District Name" Text=' <%# Eval("District") %>' ForeColor="Blue"></asp:HyperLink>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                            <HeaderTemplate>
                                <div style="text-align: center; font-size: 16px;">
                                    Number of Bill Generated
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblqty1" runat="server" Text='<%# Eval("NoOfRMDSC") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <div style="text-align: right;">
                                    <asp:Label ID="lblTotalqty1" runat="server" Font-Bold="true" />
                                </div>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                            <HeaderTemplate>
                                <div style="text-align: center; font-size: 16px;">
                                    Amount
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNoofBillGenerateAmt" runat="server" Text='<%# Eval("NoOfRMDSCAmt") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <div style="text-align: right;">
                                    <asp:Label ID="lblTotalqty2" runat="server" Font-Bold="true" />
                                </div>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
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
            <%--<div style="text-align: right; margin-top: 5px;">
                <a target="_blank" href="/public/Reports/MobileApp/Activities/Gram_Panchayat_Wise_Summary_Of_Verified_House_According_to_BLS_2012.aspx">और देखें >></a>
            </div>--%>
        </div>
    </div>
</asp:Content>

