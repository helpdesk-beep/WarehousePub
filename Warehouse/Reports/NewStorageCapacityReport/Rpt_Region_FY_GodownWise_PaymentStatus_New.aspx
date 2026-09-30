<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Region_FY_GodownWise_PaymentStatus_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Region_FY_GodownWise_PaymentStatus_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
    <style>
    table.dataTable thead th,
    table.table thead th,
    table.table th,
    .Grid th {
        background-color: #00AAD2 !important;
        color: #ffffff !important;
        text-align: center !important;
        vertical-align: middle !important;
        white-space: normal !important;
        word-break: break-word;
        font-size: 14px;
        font-weight: bold;
    }

    /* Remove Bootstrap override */
    .table > :not(caption) > * > * {
        background-color: transparent;
    }

    /* GridView Header fallback */
    asp\:GridView th {
        background-color: #00AAD2 !important;
        color: #fff !important;
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="container1">
        <!-- Header -->
        <div class="text-center">
            <h2 class="header">M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
            <h4 class="header">Report For Review of Region, Financial Year Wise, Godown wise Payment Position Report (Amount in Cr Rs)</h4>
        </div>

        <!-- Filters Section -->
        <div class="card p-4 mb-4" style="border: 5px solid #e3e3e8; border-radius: 5px;">
            <div class="row text-center">
                <div class="col-lg-1">
                    <asp:Label ID="lblRegion" runat="server" Font-Bold="true" ForeColor="Navy">संभाग:</asp:Label>
                </div>
                <div class="col-lg-2">
                    <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="true" CssClass="form-control">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-lg-1">
                    <asp:Label ID="lblFY" runat="server" Font-Bold="true" ForeColor="Navy">वित्‍तीय वर्ष:</asp:Label>
                </div>
                <div class="col-lg-2">
                    <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="2010-11">2010-11</asp:ListItem>
                        <asp:ListItem Value="2011-12">2011-12</asp:ListItem>
                        <asp:ListItem Value="2012-13">2012-13</asp:ListItem>
                        <asp:ListItem Value="2013-14">2013-14</asp:ListItem>
                        <asp:ListItem Value="2014-15">2014-15</asp:ListItem>
                        <asp:ListItem Value="2015-16">2015-16</asp:ListItem>
                        <asp:ListItem Value="2016-17">2016-17</asp:ListItem>
                        <asp:ListItem Value="2017-18">2017-18</asp:ListItem>
                        <asp:ListItem Value="2018-19">2018-19</asp:ListItem>
                        <asp:ListItem Value="2019-20">2019-20</asp:ListItem>
                        <asp:ListItem Value="2020-21">2020-21</asp:ListItem>
                        <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                        <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                        <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                        <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-lg-2">
                    <asp:Label ID="lblGodownType" runat="server" Font-Bold="true" ForeColor="Navy">गोदाम का प्रकार:</asp:Label>
                </div>
                <div class="col-lg-2">
                    <asp:DropDownList ID="ddlGodownType" runat="server" CssClass="form-control">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-lg-2">
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-sm"
                            Text="Search" OnClick="btnSearch_Click" ValidationGroup="A" />
                    </div>
                </div>
            </div>

        </div>

        <!-- GridView Section -->
        <div class="col-md-12">
            <asp:GridView ID="GV_FYGodownWisePayment" runat="server" AutoGenerateColumns="False"
                ShowFooter="true" ClientIDMode="Static"
                CssClass="table table-bordered table-striped table-hover"
                OnRowCreated="GV_FYGodownWisePayment_OnRowCreated">

                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Region">
                        <ItemTemplate>
                            <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("RegionName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="वित्‍तीय वर्ष">
                        <ItemTemplate>
                            <asp:Label ID="lblFinancialYear" runat="server" Text='<%# Eval("FinancialYear") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="गोदाम का प्रकार">
                        <ItemTemplate>
                            <asp:Label ID="lblGodownType" runat="server" Text='<%# Eval("GodownType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="गोदाम संख्या">
                        <ItemTemplate>
                            <asp:Label ID="lblGodownCnt" runat="server" Text='<%# Eval("TotalGodownCount") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="वित्‍तीय वर्ष में किराये की कुल राशि (Cr)">
                        <ItemTemplate>
                            <asp:Label ID="lblFY_GodownRentAmount" runat="server" Text='<%# Eval("TotalAmountInRentInFY") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="वित्‍तीय वर्ष में भुगतान राशि (Cr)">
                        <ItemTemplate>
                            <asp:Label ID="lblFY_AmountPaymentToGodown" runat="server" Text='<%# Eval("TotalAmountPaidToGodownOwnerInFY") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="गोदाम संचालक की शेष लंबित राशि (Cr)">
                        <ItemTemplate>
                            <asp:Label ID="lblGodownOwnerPendingAmount" runat="server" Text='<%# Eval("RemainingAmountOfGodownOwner") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <script>
        $(document).ready(function () {
            if ($.fn.DataTable.isDataTable('#GV_FYGodownWisePayment')) {
                $('#GV_FYGodownWisePayment').DataTable().destroy();
            }

            // Initialize DataTable
            BindDatatable($('#GV_FYGodownWisePayment'));
        });

        function BindDatatable(gv) {
            gv.DataTable({
                paging: true,
                searching: true,
                ordering: true,
                info: true,
                lengthMenu: [[10, 20, 30, 50, -1], ['10', '20', '30', '50', 'All']],
                pageLength: 10,
                scrollX: true,
                dom: '<"top d-flex justify-content-between align-items-center"Bfl>rt<"bottom"ip><"clear">',
                destroy: true,
                autoWidth: false,
                buttons: [
                    { extend: 'excelHtml5', title: 'Godown_Wise_Report', text: 'Export to Excel 💾' },
                    { extend: 'pdfHtml5', title: 'Godown_Wise_Report', text: 'Export to PDF 📄', orientation: 'landscape', pageSize: 'A3' },
                    { extend: 'csvHtml5', text: 'Export CSV' },
                    { extend: 'print', text: 'Print' }
                ],
                initComplete: function () {
                    const wrapper = $(this).closest('.dataTables_wrapper');
                    wrapper.find('select').addClass('form-select form-select-sm');
                    wrapper.find('input[type=search]').addClass('form-control form-control-sm').attr('placeholder', 'Search...');
                }
            });
        }
    </script>
</asp:Content>
