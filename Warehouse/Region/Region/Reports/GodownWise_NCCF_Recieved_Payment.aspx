<%@ Page Title="Godown-Wise NCCF Received Payment Report" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="~/Region/Region/Reports/GodownWise_NCCF_Recieved_Payment.aspx.cs" Inherits="Region_Region_Reports_GodownWise_NCCF_Recieved_Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!-- DataTables CSS Core & Extension Layers (Bootstrap 5 styling) -->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/dataTables.bootstrap5.min.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/2.4.1/css/buttons.bootstrap5.min.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.7.0/css/colReorder.bootstrap5.min.css" />
    
    <style type="text/css">
        /* Structural styling overrides to avoid rendering collisions with the layout master */
        .dt-buttons {
            margin-bottom: 15px;
        }
        .dt-button {
            margin-right: 6px !important;
        }
        .table-responsive {
            padding: 5px;
        }
        th {
            white-space: nowrap;
            background-color: #f8f9fa;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid mt-3">
        <div class="card shadow-sm">
            <div class="card-header bg-primary text-white">
                <h4 class="mb-0 fs-5">Godown-Wise NCCF Received Payment Summary</h4>
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <!-- GridView mappings built to bind identically to your SQL query fields -->
                    <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" 
                        CssClass="table table-striped table-bordered align-middle" ClientIDMode="Static">
                        <Columns>
                            <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                            <asp:BoundField DataField="DepotName" HeaderText="Depot Name" />
                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                            <asp:BoundField DataField="Total Storage Charges Bill Amount" HeaderText="Total Storage Bill Amount" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="Total Storage Charges Bill" HeaderText="Total Storage Bills" />
                            <asp:BoundField DataField="PSS Bill Amount" HeaderText="PSS Bill Amount" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="PSF Bill Amount" HeaderText="PSF Bill Amount" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="TDS Deduction From NCCF" HeaderText="TDS Deduction" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="Other Deduction From NCCF" HeaderText="Other Deduction" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="Received Payment From NCCF" HeaderText="Received Payment" DataFormatString="{0:N2}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <!-- Core Dependent Scripts -->
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>

    <!-- DataTables Base Component Libraries -->
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/dataTables.bootstrap5.min.js"></script>
    
    <!-- Export Pipeline Mechanics & Virtual Asset Parsers -->
    <script src="https://cdn.datatables.net/buttons/2.4.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.4.1/js/buttons.bootstrap5.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.1/jszip.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.2.7/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.2.7/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.4.1/js/buttons.html5.min.js"></script>
    
    <!-- ColReorder Pipeline Engine (Interactive Column Drags) -->
    <script src="https://cdn.datatables.net/colreorder/1.7.0/js/dataTables.colReorder.min.js"></script>

    <script type="text/javascript">
        function initializeReportDataTable() {
            var gridTarget = $('#gvReport');

            // Only instantiate components if data rows are physically present
            if (gridTarget.find('tbody tr').length > 0) {
                gridTarget.DataTable({
                    "destroy": true,
                    "paging": true,
                    "ordering": true,
                    "searching": true,
                    "pageLength": 10,
                    "colReorder": true, // Activates movable columns
                    "dom": '<"row"<"col-md-6"B><"col-md-6"f>>rt<"row"<"col-md-6"i><"col-md-6"p>>',
                    "buttons": [
                        {
                            extend: 'excelHtml5',
                            text: 'Export to Excel',
                            className: 'btn btn-success btn-sm text-white fw-bold',
                            title: 'GodownWise_NCCF_Received_Payment_Summary'
                        },
                        {
                            extend: 'pdfHtml5',
                            text: 'Export to PDF',
                            className: 'btn btn-danger btn-sm text-white fw-bold',
                            title: 'GodownWise_NCCF_Received_Payment_Summary',
                            orientation: 'landscape', // Wide table configuration format
                            pageSize: 'A4'
                        }
                    ]
                });
            }
        }

        $(document).ready(function () {
            initializeReportDataTable();
        });

        // Lifecycle hook execution if code is running inside an UpdatePanel workflow
        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        if (prmInstance) {
            prmInstance.add_endRequest(function () {
                initializeReportDataTable();
            });
        }
    </script>
</asp:Content>