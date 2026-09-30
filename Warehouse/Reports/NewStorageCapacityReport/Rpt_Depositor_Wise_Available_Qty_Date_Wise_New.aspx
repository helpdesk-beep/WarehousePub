<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Depositor_Wise_Available_Qty_Date_Wise_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Depositor_Wise_Available_Qty_Date_Wise_New" %>

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
                        <h2 class="h5 text-dark mb-0">Depositor, Commodity, Date Wise Stock Position</h2>
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
                        <div class="row g-3 align-items-center">
                            <div class="col-md-1"></div>
                            <div class="col-md-3">
                                <label class="form-label fw-bold text-secondary mb-1">Date (DD-MM-YYYY)</label>
                                <asp:TextBox ID="txtpaymentdate" runat="server" AutoComplete="off"
                                    CssClass="form-control datepicker" placeholder="DD-MM-YYYY"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Commodity</label>
                                <%--<asp:DropDownList ID="ddlComodity" runat="server" 
                                CssClass="form-control">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            </asp:DropDownList>--%>
                                <asp:ListBox ID="ddlComodity" runat="server" SelectionMode="Multiple"
                                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
                            </div>
                            <div class="col-md-2 d-flex align-items-end">
                                <asp:Button ID="btnshow" Text="Show Details" runat="server"
                                    CssClass="btn btn-primary w-100" OnClick="btnshow_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Data Grid Section -->
        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" ShowFooter="true"
                                CssClass="table table-bordered table-striped table-hover mb-0">

                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" Width="5%" />
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
                                        <i class="bi bi-info-circle me-2"></i>Select date and commodity, then click "Show Details" to view stock position.
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
                        <li class="mb-1"><i class="bi bi-dot"></i>Shows depositor-wise stock position for selected date and commodity</li>
                        <li class="mb-1"><i class="bi bi-dot"></i>Export options available for PDF and Excel formats</li>
                        <li><i class="bi bi-dot"></i>Use filters to generate specific reports as per requirements</li>
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
            var grid = $('#<%= GridView1.ClientID %>');

            if (grid.find("thead").length > 0) {
                BindDatatable(grid);
            }
        });
    </script>
</asp:Content>

