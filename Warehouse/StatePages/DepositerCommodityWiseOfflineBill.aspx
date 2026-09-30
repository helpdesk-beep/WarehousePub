<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="DepositerCommodityWiseOfflineBill.aspx.cs" Inherits="SRV_StatePages_DepositerCommodityWiseOfflineBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" />

    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" />

    <!-- DataTables CSS -->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/dataTables.bootstrap4.min.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/2.4.2/css/buttons.bootstrap4.min.css" />

    <style type="text/css">
        /* Minimal custom CSS for layout only */
        .page-title {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
        }

        .summary-card {
            border-left: 4px solid #28a745;
        }

        .amount-cell {
            text-align: right;
            font-family: 'Courier New', monospace;
        }

        /* DataTables button positioning */
        .dt-buttons {
            margin-bottom: 10px;
        }

        .dt-button {
            margin-right: 5px !important;
            margin-bottom: 5px !important;
        }

        /* Ensure proper table header alignment */
        table.dataTable thead th {
            vertical-align: middle !important;
            text-align: center !important;
        }
    </style>

    <style type="text/css">
    /* GridView header background color */
    #<%= gvReport.ClientID %> th {
        background-color: #45c3f3; /* Blue color */
        color: white;              /* Text white */
        text-align: center;        /* Center align */
        vertical-align: middle;    /* Middle vertical alignment */
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid py-3">

    <!-- Page Header -->
    <div class="row mb-4">
        <div class="col-12">
            <div class="card page-title text-white">
                <div class="card-body text-center py-3">
                    <h4 class="mb-1">
                        <i class="fas fa-file-invoice-dollar"></i>&nbsp;Depositor Commodity Wise Offline Bill Report
                    </h4>
                    <p class="mb-0">Region, Financial Year & Commodity Wise Status</p>
                </div>
            </div>
        </div>
    </div>

    <!-- Filter Section -->
    <div class="row mb-4">
        <div class="col-12">
            <div class="card">
                <div class="card-body">
                    <!-- Alert Messages -->
                    <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                        <div class="alert alert-dismissible fade show" role="alert">
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                    </asp:Panel>

                    <div class="row">
                        <!-- Region Dropdown -->
                        <div class="col-md-3 col-sm-6 mb-3">
                            <label class="font-weight-bold">
                                <i class="fas fa-map-marker-alt"></i>&nbsp;Region
                            </label>
                            <asp:DropDownList ID="ddlRegion" runat="server"
                                CssClass="form-control form-control-sm" DataTextField="Regionnm" DataValueField="Region_ID"
                                AppendDataBoundItems="true">
                                <asp:ListItem Value="0">-- All Regions --</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <!-- Financial Year Dropdown -->
                        <div class="col-md-3 col-sm-6 mb-3">
                            <label class="font-weight-bold">
                                <i class="fas fa-calendar-alt"></i>&nbsp;Financial Year
                            </label>
                            <asp:DropDownList ID="ddlFinancialYear" runat="server"
                                CssClass="form-control form-control-sm" AppendDataBoundItems="true">
                                <asp:ListItem Value="0">-- All Financial Years --</asp:ListItem>
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

                        <!-- Commodity Dropdown -->
                        <div class="col-md-3 col-sm-6 mb-3">
                            <label class="font-weight-bold">
                                <i class="fas fa-box"></i>&nbsp;Commodity Name
                            </label>
                            <asp:DropDownList ID="ddlCommodity" runat="server"
                                CssClass="form-control form-control-sm" DataTextField="Commodity_Name" DataValueField="Commodity_Id"
                                AppendDataBoundItems="true">
                                <asp:ListItem Value="0">-- All Commodities --</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <!-- Depositor Dropdown -->
                        <div class="col-md-3 col-sm-6 mb-3">
                            <label class="font-weight-bold">
                                <i class="fas fa-user-tie"></i>&nbsp;Depositor Name
                            </label>
                            <asp:DropDownList ID="ddlDepositor" runat="server"
                                CssClass="form-control form-control-sm" DataTextField="Depositor_Name" DataValueField="Depositor_ID"
                                AppendDataBoundItems="true">
                                <asp:ListItem Value="0">-- All Depositors --</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- Action Buttons -->
                    <div class="row mt-2">
                        <div class="col-12">
                            <asp:Button ID="btnSearch" runat="server" Text="Search"
                                CssClass="btn btn-primary btn-sm" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset Filters"
                                CssClass="btn btn-secondary btn-sm" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Summary Section -->
    <div class="row mb-3">
        <div class="col-12">
            <div class="card summary-card">
                <div class="card-body py-2">
                    <div class="row text-center">
                        <div class="col-md-3 col-sm-6 mb-2 mb-md-0">
                            <small class="text-muted d-block">Total Records</small>
                            <h5 class="mb-0 text-success">
                                <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label></h5>
                        </div>
                        <div class="col-md-3 col-sm-6 mb-2 mb-md-0">
                            <small class="text-muted d-block">Total Amount Presented</small>
                            <h5 class="mb-0 text-primary">₹<asp:Label ID="lblTotalAmountPresented" runat="server" Text="0.00"></asp:Label></h5>
                        </div>
                        <div class="col-md-3 col-sm-6 mb-2 mb-md-0">
                            <small class="text-muted d-block">Total Amount Received</small>
                            <h5 class="mb-0 text-info">₹<asp:Label ID="lblTotalAmountReceived" runat="server" Text="0.00"></asp:Label></h5>
                        </div>
                        <div class="col-md-3 col-sm-6 mb-2 mb-md-0">
                            <small class="text-muted d-block">Total Remaining</small>
                            <h5 class="mb-0 text-warning">₹<asp:Label ID="lblTotalRemaining" runat="server" Text="0.00"></asp:Label></h5>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Data Table Section -->
    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-body">
                    <!-- DataTables will automatically add buttons here based on DOM configuration -->
                    <div class="table-responsive">
                        <%--<asp:GridView ID="gvReport" runat="server"
                            AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped table-hover"
                            EmptyDataText="No records found"
                            ShowHeaderWhenEmpty="true">--%>
                        <asp:GridView ID="gvReport" runat="server"
                            AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped table-hover"
                            AllowSorting="true"
                            OnSorting="gvReport_Sorting"
                            ShowHeaderWhenEmpty="true"
                            EmptyDataText="No records found">


                            <Columns>

                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="RegionName" HeaderText="Region" />
                                <asp:BoundField DataField="FinancialYear" HeaderText="Financial Year" />
                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" />
                                <asp:BoundField DataField="DepositorName" HeaderText="Depositor Name" />

                                <asp:BoundField DataField="TotalBillPresentedinFY" HeaderText="Total Bills"
                                    DataFormatString="{0:N0}" ItemStyle-CssClass="amount-cell" />
                                <asp:BoundField DataField="TotalBillAmountPresented"
                                    HeaderText="Bill Amount"
                                    DataFormatString="{0:N2}" />

                                <asp:BoundField DataField="TotalAmountReceivedInFY" HeaderText="Amount Received"
                                    DataFormatString="{0:N2}" ItemStyle-CssClass="amount-cell" />
                                <asp:BoundField DataField="RemainingAmountFromDepositor" HeaderText="Remaining Amount"
                                    DataFormatString="{0:N2}" ItemStyle-CssClass="amount-cell" />

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <!-- Hidden elements for DataTables -->
    <div style="display: none;">
        <asp:GridView ID="gvTotals" runat="server" AutoGenerateColumns="False"
            CssClass="table" ShowHeader="false" Visible="false">
            <Columns>
                <asp:BoundField DataField="RegionName" HeaderText="" />
                <asp:BoundField DataField="FinancialYear" HeaderText="" />
                <asp:BoundField DataField="Commodity_Name" HeaderText="" />
                <asp:BoundField DataField="DepositorName" HeaderText="TOTAL" />
                <asp:BoundField DataField="TotalBillPresentedinFY" HeaderText="" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="TotalBillAmountPresented" HeaderText="" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="TotalAmountReceivedInFY" HeaderText="" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="RemainingAmountFromDepositor" HeaderText="" DataFormatString="{0:N2}" />
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblPageInfo" runat="server" Text="Page 1 of 1"></asp:Label>
        <asp:Button ID="btnFirst" runat="server" Text="<<" OnClick="btnFirst_Click" />
        <asp:Button ID="btnPrev" runat="server" Text="<" OnClick="btnPrev_Click" />
        <asp:Button ID="btnNext" runat="server" Text=">" OnClick="btnNext_Click" />
        <asp:Button ID="btnLast" runat="server" Text=">>" OnClick="btnLast_Click" />
    </div>

</div>

<!-- jQuery -->
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<!-- Bootstrap JS -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>

<!-- DataTables JS -->
<script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
<script src="https://cdn.datatables.net/1.13.6/js/dataTables.bootstrap4.min.js"></script>
<script src="https://cdn.datatables.net/buttons/2.4.2/js/dataTables.buttons.min.js"></script>
<script src="https://cdn.datatables.net/buttons/2.4.2/js/buttons.bootstrap4.min.js"></script>
<script src="https://cdn.datatables.net/buttons/2.4.2/js/buttons.colVis.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.1/jszip.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.2.7/pdfmake.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.2.7/vfs_fonts.js"></script>
<script src="https://cdn.datatables.net/buttons/2.4.2/js/buttons.html5.min.js"></script>
<script src="https://cdn.datatables.net/buttons/2.4.2/js/buttons.print.min.js"></script>
     
<!-- DataTables Initialization Script -->
<%--<script type="text/javascript">

    $(document).ready(function () {
        $('#gvReport thead tr:eq(0)').remove();

        initDT();

        if (Sys && Sys.WebForms) {
            Sys.WebForms.PageRequestManager.getInstance()
                .add_endRequest(function () {
                    initDT();
                });
        }
    });

    function initDT() {
        var gvID = '<%= gvReport.ClientID %>';

        if ($.fn.DataTable.isDataTable('#' + gvID)) {
            $('#' + gvID).DataTable().destroy();
        }

        $('#' + gvID).DataTable({
            pageLength: 10,
            lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
            responsive: true,
            ordering: true,
            searching: true,
            info: true,
            order: [],
            dom: 'Blfrtip',
            buttons: [
                { extend: 'excelHtml5', text: '<i class="fas fa-file-excel"></i> Excel', className: 'btn btn-success btn-sm' },
                { extend: 'pdfHtml5', orientation: 'landscape', pageSize: 'A4', text: '<i class="fas fa-file-pdf"></i> PDF', className: 'btn btn-danger btn-sm' },
                { extend: 'print', text: '<i class="fas fa-print"></i> Print', className: 'btn btn-info btn-sm' }
            ],
            columnDefs: [
                { targets: 0, orderable: false, searchable: false, className: 'text-center' },
                { targets: [1, 2, 3, 4], className: 'text-center' },
                {
                    targets: [5, 6, 7, 8],
                    className: 'text-right',
                    render: function (data, type, row, meta) {
                        if (meta.row === undefined || data === null || data === '') return data;
                        var val = parseFloat(data.toString().replace(/,/g, ''));
                        if (isNaN(val)) return data;
                        return val.toLocaleString('en-IN', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                    }
                }
            ],
            initComplete: function () {
                $('[id$="btnFirst"],[id$="btnPrev"],[id$="btnNext"],[id$="btnLast"],[id$="lblPageInfo"],[id$="gvTotals"]').hide();
            }
        });
    }


</script>--%>

    <!-- DataTables Initialization Script -->
<script type="text/javascript">
    $(document).ready(function () {
        initDT();

        if (Sys && Sys.WebForms) {
            Sys.WebForms.PageRequestManager.getInstance()
                .add_endRequest(function () {
                    initDT();
                });
        }
    });

    function initDT() {
        var gvID = '<%= gvReport.ClientID %>';
        var table = $('#' + gvID);

        if ($.fn.DataTable.isDataTable('#' + gvID)) {
            $('#' + gvID).DataTable().destroy();
        }

        // Ensure proper table structure before initializing DataTables
        if (table.find('thead').length === 0) {
            table.find('tr:first').wrapAll('<thead></thead>');
        }

        if (table.find('tbody').length === 0) {
            table.find('tr:not(:first)').wrapAll('<tbody></tbody>');
        }

        $('#' + gvID).DataTable({
            pageLength: 10,
            lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
            responsive: true,
            ordering: true,
            searching: true,
            info: true,
            order: [],
            dom: 'Blfrtip',
            buttons: [
                {
                    extend: 'excelHtml5',
                    text: '<i class="fas fa-file-excel"></i> Excel',
                    className: 'btn btn-success btn-sm',
                    exportOptions: {
                        columns: ':visible'
                    }
                },
                {
                    extend: 'pdfHtml5',
                    orientation: 'landscape',
                    pageSize: 'A4',
                    text: '<i class="fas fa-file-pdf"></i> PDF',
                    className: 'btn btn-danger btn-sm',
                    exportOptions: {
                        columns: ':visible'
                    }
                },
                {
                    extend: 'print',
                    text: '<i class="fas fa-print"></i> Print',
                    className: 'btn btn-info btn-sm',
                    exportOptions: {
                        columns: ':visible'
                    }
                }
            ],
            columnDefs: [
                {
                    targets: 0,
                    orderable: false,
                    searchable: false,
                    className: 'text-center',
                    render: function (data, type, row, meta) {
                        // Return empty for header row, otherwise show serial number
                        if (type === 'display' && meta.row === 0) {
                            return '';
                        }
                        return data;
                    }
                },
                { targets: [1, 2, 3, 4], className: 'text-center' },
                {
                    targets: [5, 6, 7, 8],
                    className: 'text-right',
                    render: function (data, type, row, meta) {
                        // Skip header row
                        if (meta.row === 0) return data;

                        if (data === null || data === '' || data === undefined) return '';
                        var val = parseFloat(data.toString().replace(/,/g, ''));
                        if (isNaN(val)) return data;
                        return val.toLocaleString('en-IN', {
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2
                        });
                    }
                }
            ],
            initComplete: function () {
                // Hide unnecessary pagination elements
                $('[id$="btnFirst"],[id$="btnPrev"],[id$="btnNext"],[id$="btnLast"],[id$="lblPageInfo"],[id$="gvTotals"]').hide();

                // Ensure proper header formatting
                var api = this.api();
                api.columns().header().to$().css('text-align', 'center');
            },
            drawCallback: function () {
                // Update the summary counts after DataTables renders
                updateSummaryCounts();
            }
        });
    }

    function updateSummaryCounts() {
        var gvID = '<%= gvReport.ClientID %>';
        var table = $('#' + gvID).DataTable();
        var visibleRows = table.rows({ filter: 'applied' }).count();
        var totalRows = table.rows().count();
        
        // Only update if we have actual data (excluding header)
        if (totalRows > 1) {
            var actualDataCount = totalRows - 1; // Subtract header row
            $('#<%= lblTotalRecords.ClientID %>').text(actualDataCount);
        }
    }
</script>

</asp:Content>

