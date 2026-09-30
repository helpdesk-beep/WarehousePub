<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Godown_CropYear_Depositorwise_StockPosition_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Godown_CropYear_Depositorwise_StockPosition_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="container-fluid py-3">
        <!-- Header Section -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="d-flex justify-content-between align-items-center border-bottom pb-2">
                    <div>
                        <h1 class="h4 text-primary mb-0">M.P. WAREHOUSING & LOGISTICS CORPORATION</h1>
                        <h2 class="h5 text-dark mb-0">Review of District, Depositor, CropYear Wise, Commodity Wise Stock Position</h2>
                    </div>
                    <div class="text-end">
                        <span class="badge bg-danger text-white p-2">All quantities in Metric Tons (MT)</span>
                    </div>
                </div>
            </div>
        </div>

        <!-- Filter Section -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <div class="row g-3">
                            <div class="col-md-3">
                                <label class="form-label fw-bold text-secondary mb-1">Date (DD-MM-YYYY)</label>
                                <asp:TextBox ID="txtpaymentdate" runat="server"
                                    CssClass="form-control" placeholder="DD-MM-YYYY"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label fw-bold text-secondary mb-1">Depositor</label>
                                <asp:DropDownList ID="ddldepositor" runat="server"
                                    CssClass="form-control">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label fw-bold text-secondary mb-1">Commodity</label>
                                <%--<asp:DropDownList ID="ddlComodity" runat="server" 
                                CssClass="form-control">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            </asp:DropDownList>--%>
                                <asp:ListBox ID="ddlComodity" runat="server" SelectionMode="Multiple"
                                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
                            </div>
                            <div class="col-md-3 d-flex align-items-end">
                                <asp:Button ID="btnshow" Text="Show Details" runat="server"
                                    CssClass="btn btn-primary w-100" OnClick="btnshow_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Status Message -->
        <div class="row mb-3">
            <div class="col-12">
                <asp:Label ID="lblmsg" runat="server" CssClass="alert alert-warning d-flex align-items-center mb-0 d-none"></asp:Label>
                <asp:HiddenField ID="hfId" Value="0" runat="server" />
                <asp:HiddenField ID="hfFileName" Value="0" runat="server" />
            </div>
        </div>

        <!-- Data Grid Section -->
        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <h5 class="mb-0">Stock Position Details</h5>
                    </div>
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <asp:GridView ID="GV_StockPositionDetails" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-bordered table-striped table-hover mb-0"
                                ShowFooter="true"
                                OnRowDataBound="GV_StockPositionDetails_OnRowDataBound">

                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" Width="5%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="District">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("District") %>'>0</asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                        <ItemStyle CssClass="align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Branch Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepotName" runat="server" Text='<%# Eval("DepotName") %>'>0</asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                        <ItemStyle CssClass="align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Godown Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGodown" runat="server" Text='<%# Eval("Godown") %>'>0</asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                        <ItemStyle CssClass="align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="2017-18">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY1" runat="server" Text='<%# Eval("[2017-18]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="2018-19">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY2" runat="server" Text='<%# Eval("[2018-19]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="2019-20">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY3" runat="server" Text='<%# Eval("[2019-20]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="2020-21">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY4" runat="server" Text='<%# Eval("[2020-21]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="2021-22">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY5" runat="server" Text='<%# Eval("[2021-22]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="2022-23">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY6" runat="server" Text='<%# Eval("[2022-23]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="2023-24">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY7" runat="server" Text='<%# Eval("[2023-24]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="2024-25">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY8" runat="server" Text='<%# Eval("[2024-25]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle fw-bold" />
                                        <ItemStyle CssClass="text-end align-middle fw-bold" />
                                    </asp:TemplateField>
                                </Columns>

                                <HeaderStyle CssClass="bg-primary text-white" />
                                <FooterStyle CssClass="bg-light fw-bold" />
                                <RowStyle CssClass="align-middle" />
                                <AlternatingRowStyle CssClass="bg-light" />
                                <SelectedRowStyle CssClass="table-warning" />
                                <PagerStyle CssClass="pagination justify-content-center my-3" />

                                <EmptyDataTemplate>
                                    <div class="alert alert-info text-center m-3">
                                        <i class="bi bi-info-circle me-2"></i>Select filters and click "Show Details" to view stock position review.
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Summary Information -->
        <div class="row mt-3">
            <div class="col-12">
                <div class="alert alert-secondary">
                    <strong class="d-block mb-2">Note:</strong>
                    <ul class="list-unstyled mb-0">
                        <li class="mb-1"><i class="bi bi-dot"></i>All quantities are displayed in Metric Tons (MT)</li>
                        <li class="mb-1"><i class="bi bi-dot"></i>Review report showing district, depositor, crop year and commodity wise stock position</li>
                        <li class="mb-1"><i class="bi bi-dot"></i>Stock data across multiple crop years (2017-18 to 2024-25)</li>
                        <li><i class="bi bi-dot"></i>Filter by Date, Depositor and Commodity for specific analysis</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <script>
        $(document).ready(function () {
            var grid = $('#<%= GV_StockPositionDetails.ClientID %>');

            if (grid.find("thead").length > 0) {
                BindDatatable(grid);
            }
        });
    </script>
</asp:Content>

