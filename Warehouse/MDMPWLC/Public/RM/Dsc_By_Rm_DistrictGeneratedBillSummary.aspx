<%@ Page Title="" Language="C#" MasterPageFile="~/MDMPWLC/MasterPages/MasterPage2.master" AutoEventWireup="true" CodeFile="Dsc_By_Rm_DistrictGeneratedBillSummary.aspx.cs" Inherits="Administration_Public_Dsc_By_Rm_DistrictGeneratedBillSummary" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
        <div class="card">
            <div class="card-header">
                <%--<h5 class="mb-0">Data Tables - Print, Excel, CSV, PDF Buttons</h5>--%>
                <h5>Branch Wise Payment Received Details From MPSCSC</h5>
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Visible="true"
                            ShowFooter="true" class="table table-striped table-bordered first">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
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
                                        <div>
                                            <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/MDMPWLC/Public/RM/Dsc_By_Rm_BranchGeneratedBillSummary.aspx?ID="+ (Eval("District_Id").ToString())%>'
                                                title="District Name" Text=' <%# Eval("District") %>'></asp:HyperLink>
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
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="alert alert-danger" style="text-align: center; margin: auto; margin-top: 10px; margin-bottom: 5px; font-size: 10pt; color: Green">
                                    WARNING: No Records Found
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    <%--<div style="text-align: right; margin-top: 5px;">
                <a target="_blank" href="/public/Reports/MobileApp/Activities/Gram_Panchayat_Wise_Summary_Of_Verified_House_According_to_BLS_2012.aspx">और देखें >></a>
            </div>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

