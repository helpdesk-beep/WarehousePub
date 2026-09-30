<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/Compare_Old_New_Depositor_Entry_Report.aspx.cs" Inherits="BranchPages_Compare_Old_New_Depositor_Entry_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- Bootstrap & DataTables CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/1.13.6/css/dataTables.bootstrap5.min.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/buttons/2.4.1/css/buttons.bootstrap5.min.css" rel="stylesheet" />

    <style>
        .card {
            border: 1px solid #ddd;
            padding: 20px;
            border-radius: 8px;
            margin-top: 20px;
        }

        .title {
            font-size: 22px;
            font-weight: bold;
            margin-bottom: 15px;
        }

        th, td {
            text-align: center;
        }

        .dataTables_wrapper .dataTables_filter input {
            margin-left: 0.5em;
        }

        .dataTables_wrapper .dataTables_paginate .paginate_button {
            padding: 0.2em 0.5em;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="title">Compare Old vs New Depositor Entry Report</div>

        <div class="row mb-3">
            <div class="col-md-4">
                <label for="ddlDepositor" class="form-label">Depositor</label>
                <asp:DropDownList ID="ddlDepositor" runat="server"
                    CssClass="form-select form-select-sm"
                    AppendDataBoundItems="true">
                    <asp:ListItem Value="0">-- All Depositors --</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-md-2 d-flex align-items-end">
                <asp:Button ID="btnSearch" runat="server" Text="Search"
                    CssClass="btn btn-primary w-100"
                    OnClick="btnSearch_Click" />
            </div>
        </div>

        <div class="table-responsive">
            <%--<asp:GridView ID="gvReport" runat="server"
                CssClass="table table-bordered table-striped table-hover"
                AutoGenerateColumns="false"
                OnDataBound="gvReport_DataBound">--%>
            <asp:GridView ID="gvReport" runat="server"
                CssClass="table table-bordered table-striped table-hover"
                AutoGenerateColumns="false"
                ClientIDMode="Static"
                UseAccessibleHeader="true"
                OnPreRender="gvReport_PreRender">

                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Region_Name" HeaderText="Region" />
                    <asp:BoundField DataField="Branch_Name" HeaderText="Branch" />
                    <asp:BoundField DataField="Old Entry" HeaderText="Old Entry" />
                    <asp:BoundField DataField="New Entry" HeaderText="New Entry" />
                    <asp:BoundField DataField="Bill Difference" HeaderText="Difference" />
                    <asp:BoundField DataField="Old Entry Amount" HeaderText="Old Amount" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="New Entry Amount" HeaderText="New Amount" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="Bill Amount Difference" HeaderText="Amount Diff" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="Old Remaining" HeaderText="Old Remaining" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="New Remaining" HeaderText="New Remaining" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="Remaining Difference" HeaderText="Remaining Diff" DataFormatString="{0:N2}" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <!-- JS Libraries -->
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/dataTables.bootstrap5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.4.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.4.1/js/buttons.bootstrap5.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.1/jszip.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.2.7/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.2.7/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.4.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.4.1/js/buttons.print.min.js"></script>

    <!-- Initialize DataTable with Buttons and Show All -->
    <%--<script>
        $(document).ready(function () {

            var tableId = '#<%= gvReport.ClientID %>';

        if ($.fn.DataTable.isDataTable(tableId)) {
            $(tableId).DataTable().destroy();
        }

        var table = $(tableId).DataTable({
            paging: true,
            searching: true,
            ordering: true,
            info: false,
            autoWidth: false,

            // ✅ Show entries dropdown ENABLE
            pageLength: -1,
            lengthMenu: [[10, 50, 100, -1], [10, 50, 100, "All"]],

            columnDefs: [
                { targets: 0, orderable: false, className: "dt-center" },
                { targets: "_all", className: "dt-center" }
            ],

            // 🔥 IMPORTANT FIX
            dom: 'lBfrtip',

            buttons: [
                {
                    extend: 'excelHtml5',
                    className: 'btn btn-success btn-sm'
                },
                {
                    extend: 'pdfHtml5',
                    orientation: 'landscape',
                    pageSize: 'A4',
                    className: 'btn btn-danger btn-sm'
                },
                {
                    extend: 'print',
                    className: 'btn btn-primary btn-sm'
                }
            ]
        });

        // ✅ Fix Serial Number
        table.on('order.dt search.dt', function () {
            table.column(0, { search: 'applied', order: 'applied' })
                .nodes()
                .each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
        }).draw();

    });
    </script>--%>

    <script>
        $(document).ready(function () {

            var tableId = '#<%= gvReport.ClientID %>';

        if ($.fn.DataTable.isDataTable(tableId)) {
            $(tableId).DataTable().destroy();
        }

        var table = $(tableId).DataTable({
            paging: true,
            searching: true,
            ordering: true,
            info: false,
            autoWidth: false,

            pageLength: -1,
            lengthMenu: [[10, 50, 100, -1], [10, 50, 100, "All"]],

            columnDefs: [
                { targets: 0, orderable: false, className: "dt-center" },
                { targets: "_all", className: "dt-center" }
            ],

            // ✅ ONE ROW LAYOUT
            dom:
                "<'row mb-2'<'col-md-3'l><'col-md-5 text-center'B><'col-md-4 text-end'f>>" +
                "<'row'<'col-12'tr>>" +
                "<'row mt-2'<'col-md-5'i><'col-md-7'p>>",

            buttons: [
                { extend: 'excelHtml5', className: 'btn btn-success btn-sm me-1' },
                { extend: 'pdfHtml5', className: 'btn btn-danger btn-sm me-1', orientation: 'landscape', pageSize: 'A4' },
                { extend: 'print', className: 'btn btn-primary btn-sm' }
            ]
        });

        // Serial No Fix
        table.on('order.dt search.dt', function () {
            table.column(0, { search: 'applied', order: 'applied' })
                .nodes()
                .each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
        }).draw();

    });
    </script>



</asp:Content>
