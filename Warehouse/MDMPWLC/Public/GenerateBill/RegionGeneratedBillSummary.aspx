<%@ Page Title="" Language="C#" MasterPageFile="~/MDMPWLC/MasterPages/MasterPage2.master" AutoEventWireup="true" CodeFile="RegionGeneratedBillSummary.aspx.cs" Inherits="Administration_Public_GeneratedBillSummary" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div>
        <!-- ============================================================== -->
        <!-- data table  -->
        <!-- ============================================================== -->
        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
            <div class="card">
                <div class="card-header">
                    <%--<h5 class="mb-0">Data Tables - Print, Excel, CSV, PDF Buttons</h5>--%>
                    <h5>Region Wise Payment Received Details From MPSCSC</h5>
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
                                                Region Name
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="text-align: left; font-weight: bolder;">
                                                <%-- <%# Eval("Regionnm")%>--%>
                                                <asp:HyperLink ID="sdffsft" runat="server" NavigateUrl='<%#"~/MDMPWLC/Public/GenerateBill/DistrictGeneratedBillSummary.aspx?ID="+ (Eval("Region_ID").ToString())%>'
                                                    title="Regionnm Name" Text=' <%# Eval("Regionnm") %>'></asp:HyperLink>
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
                                            <asp:Label ID="lblqty1" runat="server" Text='<%# Eval("NoOfGenerateBill") %>' />
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
                                            <asp:Label ID="lblNoofBillGenerateAmt" runat="server" Text='<%# Eval("NoofBillGenerateAmt") %>' />
                                        </ItemTemplate>
                                      

                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="alert alert-danger" style="text-align: center; margin: auto; margin-top: 10px; margin-bottom: 5px; font-size: 10pt; color: Green">
                                        WARNING: No Records Found
                                    </div>
                                </EmptyDataTemplate>
                               <%-- <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />--%>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

