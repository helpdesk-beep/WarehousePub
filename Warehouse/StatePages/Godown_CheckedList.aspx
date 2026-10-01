<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Godown_CheckedList.aspx.cs" Inherits="Godown_CheckedList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Advanced Search - Region Summary</title>

    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="<%= ResolveUrl("~/assets/js/bootstrap.min.js") %>"></script>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/css/bootstrap-multiselect.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/js/bootstrap-multiselect.min.js"></script>

    <%-- DataTables Core --%>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.25/css/jquery.dataTables.min.css" />
    <script type="text/javascript" src="https://cdn.datatables.net/1.13.11/js/jquery.dataTables.min.js"></script>

    <%-- DataTables Buttons Extension Libraries --%>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.7.1/css/buttons.dataTables.min.css" />
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.7.1/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript"
        src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.2/jszip.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.7.1/js/buttons.html5.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.7.1/js/buttons.print.min.js"></script>

    <style>
        /* Existing Multi-select Button Styles (for bigger text) */
        .btn-group .multiselect {
            text-align: left;
            border-radius: 4px;
           /* [cite: 3] */
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            background-color: #fff;
           /* [cite: 4] */
            color: #555;
           /* [cite: 4] */
            font-size: 16px;
           /* [cite: 5] */
            padding: 8px 12px;
           /* [cite: 6] */
            height: auto;
           /* [cite: 7] */
        }

        /* Base Container */
        .container {
            width: 95%;
           /* [cite: 8] */
            margin: 20px auto;
           /* [cite: 8] */
            /* *** NEW: Colorful Border for the Page Content *** */
            border: 2px solid #17a2b8; /* Teal border */
            border-radius: 8px; /* Rounded corners */
            padding-top: 20px;
            padding-bottom: 20px;
        }

        /* Filter Section Styling */
        .filter-container {
            padding: 20px;
           /* [cite: 9] */
            border: 1px solid #ddd;
           /* [cite: 9] */
            border-radius: 6px;
           /* [cite: 9] */
            background-color: #f9f9f9;
           /* [cite: 9] */
            margin-bottom: 20px;
           /* [cite: 10] */
        }

        /* Form Group Spacing */
        .form-group {
            margin-bottom: 15px;
           /* [cite: 11] */
        }

        /* Label Styling */
        label {
            font-weight: bold;
           /* [cite: 12] */
            display: block;
           /* [cite: 12] */
            margin-bottom: 5px;
           /* [cite: 12] */
            color: #333;
           /* [cite: 12] */
            margin-right: 12px;
           /* [cite: 12] */
        }

        /* Search Button Style */
        .btn-primary {
            background-color: #007bff;
           /* [cite: 15] */
            border-color: #007bff;
           /* [cite: 15] */
            transition: background-color 0.3s ease;
           /* [cite: 15] */
        }

            .btn-primary:hover, .btn-primary:focus {
                background-color: #0056b3;
               /* [cite: 16] */
                border-color: #004085;
               /* [cite: 16] */
            }

        /* Gridview Styles for DataTables */
        .dataTables_wrapper {
            margin-top: 20px;
           /* [cite: 17] */
        }

        .parentGrid {
            width: 100% !important;
           /* [cite: 18] */
        }

        /* *** NEW: Colorful GridView Header *** */
        #gvRegions thead th {
            background-color: #008080 !important; /* Dark Teal Background */
            color: white !important; /* White text for contrast */
            border-color: #006666 !important; /* Darker border */
        }

        /* Clickable Region Column */
        td.region-detail-control {
            font-weight: bold;
           /* [cite: 19] */
            color: #007bff;
           /* [cite: 19] */
            cursor: pointer;
           /* [cite: 19] */
            text-decoration: underline;
           /* [cite: 19] */
        }

        /* Style for the expand/collapse icon */
        td.details-icon {
            background: url('https://cdn.datatables.net/examples/resources/details_open.png') no-repeat center center;
           /* [cite: 20] */
            cursor: pointer;
           /* [cite: 20] */
            width: 25px;
           /* [cite: 20] */
        }

        tr.shown td.details-icon {
            background: url('https://cdn.datatables.net/examples/resources/details_close.png') no-repeat center center;
           /* [cite: 21] */
        }

        /* Style for the nested table */
        div.slider {
            display: none;
           /* [cite: 22] */
        }

        table.nestedTable {
            margin: 10px;
           /* [cite: 23] */
            width: 98% !important;
           /* [cite: 23] */
            border: 1px solid #ccc;
           /* [cite: 23] */
            background-color: #fff;
           /* [cite: 23] */
        }

        table.nestedTable th {
                background-color: #e9ecef;
               /* [cite: 24] */
                padding: 8px;
               /* [cite: 25] */
                text-align: left;
               /* [cite: 25] */
                border-bottom: 1px solid #ddd;
               /* [cite: 25] */
                color: #495057;
               /* [cite: 25] */
         }

        table.nestedTable td {
                padding: 8px;
               /* [cite: 26] */
         }

        /* DataTables Buttons alignment */
        div.dt-buttons {
            float: left;
           /* [cite: 27] */
            margin-right: 10px;
           /* [cite: 27] */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <h2 class="text-primary">✨ Warehouse Search Filters</h2>

        <div class="row filter-container">
            <div class="col-md-3">
                <div class="form-group">
                    <label for="<%= ddlCropYear.ClientID %>">Crop Year (Multi Select):</label>
                    <asp:ListBox ID="ddlCropYear" runat="server" SelectionMode="Multiple"
                        CssClass="checkbox-multiselect" Width="100%"></asp:ListBox>
                </div>
            </div>

            <div class="col-md-3">
                <div class="form-group">
                    <label for="<%= ddlCommodity.ClientID %>">Commodity (Multi Select):</label>
                    <asp:ListBox ID="ddlCommodity" runat="server" SelectionMode="Multiple"
                        CssClass="checkbox-multiselect" Width="100%"></asp:ListBox>
                </div>
            </div>

            <div class="col-md-3">
                <div class="form-group">
                    <label for="<%= ddlGodownType.ClientID %>">Godown Type (Multi Select):</label>
                    <asp:ListBox ID="ddlGodownType" runat="server" SelectionMode="Multiple"
                        CssClass="checkbox-multiselect" Width="100%"></asp:ListBox>
                </div>
            </div>
            <div class="col-md-3 mt-12" style="margin-top: 15px;">
                <div class="form-group">
                    <asp:Button ID="btnSearch" runat="server" Text="🔍 Search Warehouses"
                        CssClass="btn btn-primary btn-lg" OnClientClick="return LoadWarehouseSummary();" />
                </div>
            </div>
        </div>
        <hr />



        <h3>📊 Region Summary Results</h3>
        <div class="well well-sm clearfix">
            <div class="pull-left dt-buttons-placeholder">
                <asp:Button ID="btnExport" runat="server" Text="Export All to Excel 💾" OnClick="btnExport_Click" CssClass="btn btn-success" />
                <asp:Button ID="btnExportPDF" runat="server" Text="Export All to PDF 📄" OnClick="btnExportPDF_Click" CssClass="btn btn-danger" />
            </div>
        </div>
        <table id="gvRegions" class="parentGrid display" style="width: 100%">
            <thead>
                <tr>
                    <th></th>
                    <%-- Icon column --%>
                    <th>Region</th>
                    <th>Total Received Weight</th>
                    <th>Total Delivered Weight</th>
                    <th>Total Loss</th>
                    <th>Total Gain</th>
                    <th>Gain %</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    </div>

    <script type="text/javascript">
        var summaryDataTable;

        // Function to check all required filters
        function checkRequiredFilters(cropYears, commodities, godownTypes) {
            if (!cropYears || cropYears.length === 0) {
                alert("Please select at least one Crop Year.");
                return false;
            }
            if (!commodities || commodities.length === 0) {
                alert("Please select at least one Commodity.");
                return false;
            }
            if (!godownTypes || godownTypes.length === 0) {
                alert("Please select at least one Godown Type.");
                return false;
            }
            return true;
        }

        function LoadWarehouseSummary() {
            var cropYears = $('#<%= ddlCropYear.ClientID %>').val();
            var commodities = $('#<%= ddlCommodity.ClientID %>').val();
            var godownTypes = $('#<%= ddlGodownType.ClientID %>').val();

            if (!checkRequiredFilters(cropYears, commodities, godownTypes)) {
                return false; // Prevent AJAX call if filters are missing
            }

            var filters = {
                cropYears: cropYears ?
                    cropYears.join(',') : '',
                commodityIDs: commodities ?
                    commodities.join(',') : '',
                godownTypes: godownTypes ?
                    godownTypes.join(',') : ''
            };
            $('#gvRegions tbody').html('<tr><td colspan="7" style="text-align:center;">Loading summary data...</td></tr>');

            $.ajax({
                type: "POST",
                url: "Godown_CheckedList.aspx/GetRegionSummary",
                data: JSON.stringify(filters),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    InitializeDataTable(response.d);
                },
                error: function (xhr, status, error) {
                    console.error("AJAX Error:", error);
                    alert("An error occurred while fetching summary data.");
                    $('#gvRegions tbody').html('<tr><td colspan="7" style="text-align:center;">Error loading summary data.</td></tr>');
                }
            });
            return false;
        }

        function formatDetailRow(rowData) {
            var regionName = rowData.Region;
            var html = '<div class="slider"><div class="loading-message" style="text-align:center; padding:20px;">Loading details for **' + regionName + '**...</div></div>';
            return html;
        }

        function loadDetailData(row, rowData, detailRowElement) {
            var cropYears = $('#<%= ddlCropYear.ClientID %>').val();
            var commodities = $('#<%= ddlCommodity.ClientID %>').val();
            var godownTypes = $('#<%= ddlGodownType.ClientID %>').val();

            // NOTE: Check not needed here as this is triggered after summary is loaded.

            var filters = {
                regionName: rowData.Region,
                cropYears: cropYears ?
                    cropYears.join(',') : '',
                commodityIDs: commodities ?
                    commodities.join(',') : '',
                godownTypes: godownTypes ?
                    godownTypes.join(',') : ''
            };
            $.ajax({
                type: "POST",
                url: "Godown_CheckedList.aspx/GetRegionDetailGrid",
                data: JSON.stringify(filters),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var receivedHtml = response.d;
                    var tempDiv = $('<div>').html(receivedHtml);
                    var nestedTableHtml = tempDiv.find('table.nestedTable').parent().html();
                    detailRowElement.html(nestedTableHtml);

                    // Initialize DataTables on the nested table
                    var nestedTable = detailRowElement.find('table.nestedTable');
                    if (nestedTable.length > 0) {
                        nestedTable.DataTable({
                            "paging": true,
                            "searching": true,
                            "ordering": true,
                            "info": false,
                            "pageLength": 5,
                            "destroy": true,
                            "autoWidth": false
                        });
                    }
                },
                error: function (xhr, status, error) {
                    detailRowElement.html('<span style="color:red;padding:20px;">Error loading detail data: ' + error + '</span>');
                    console.error("Detail AJAX Error:", error);
                }
            });
        }

        // --- NEW HELPER FUNCTION TO FETCH AND CONSOLIDATE DATA FOR EXCEL ---
        function getConsolidatedExportData() {
            var cropYears = $('#<%= ddlCropYear.ClientID %>').val();
            var commodities = $('#<%= ddlCommodity.ClientID %>').val();
            var godownTypes = $('#<%= ddlGodownType.ClientID %>').val();

            // CRITICAL CHECK: Ensure all filters are selected before attempting synchronous export
            if (!checkRequiredFilters(cropYears, commodities, godownTypes)) {
                return [];
            }

            var filters = {
                cropYears: cropYears ?
                    cropYears.join(',') : '',
                commodityIDs: commodities ?
                    commodities.join(',') : '',
                godownTypes: godownTypes ?
                    godownTypes.join(',') : ''
            };

            var consolidatedData = [];
            // NOTE: This MUST be synchronous (async: false) so the DataTables button 
            // waits for the data before proceeding with the export.
            $.ajax({
                type: "POST",
                url: "Godown_CheckedList.aspx/GetConsolidatedExportData", // The C# method we created
                data: JSON.stringify(filters),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    consolidatedData = response.d || [];
                },
                error: function (xhr, status, error) {
                    console.error("Consolidated Export AJAX Error:", error);
                    // Alert user and return empty list (which triggers the DataTables internal check)
                    alert("Error fetching consolidated data for export. Check server logs or ensure all required filters are selected.");
                }
            });
            return consolidatedData;
        }

        function InitializeDataTable(data) {
            var gridId = '#gvRegions';
            if ($.fn.DataTable.isDataTable(gridId)) {
                summaryDataTable.destroy();
            }

            summaryDataTable = $(gridId).DataTable({
                "dom": 'lBfrtip',
                "data": data,
                "columns": [
                    {
                        "className": 'details-icon',
                        "orderable": false,
                        "data": null,
                        "defaultContent": ''
                    },
                    {
                        "data": "Region",
                        "className": 'region-detail-control'
                    },
                    { "data": "Received_Weight", "render": $.fn.dataTable.render.number(',', '.', 2, '') },
                    { "data": "Delivered_Weight", "render": $.fn.dataTable.render.number(',', '.', 2, '') },
                    { "data": "Loss", "render": $.fn.dataTable.render.number(',', '.', 2, '') },
                    { "data": "Gain", "render": $.fn.dataTable.render.number(',', '.', 2, '') },
                    { "data": "Gain_Percentage", "render": $.fn.dataTable.render.number(',', '.', 2, '') }
                ],
                "order": [[1, 'asc']],
                "paging": true,
                "searching": true,
                "info": true,
                "pageLength": 10,
                "autoWidth": false,
                "destroy": true,
                "language": {
                    "emptyTable": "No summary data found based on the selected filters."
                },
                "buttons": [
                    {
                        // 🌟 CONSOLIDATED EXCEL BUTTON 
                        extend: 'excelHtml5',
                        text: 'Export ALL Data to Excel 💾',
                        title: 'Consolidated Warehouse Report',
                        filename: 'Consolidated_Warehouse_Report',
                        // Override the default action to use our custom data source
                        action: function (e, dt, button, config) {
                            var consolidatedData = getConsolidatedExportData();

                            if (consolidatedData.length === 0) {
                                // If consolidatedData is empty (due to missing filter or server error), 
                                // the function exits and the default export is implicitly bypassed.
                                return;
                            }

                            // 1. Define the custom exportData function
                            var exportConfig = $.extend({}, config, {

                                exportData: function (dt) {
                                    // DEFINE ALL 14 HEADERS FOR THE EXPORT FILE
                                    var headers = [
                                        "Region/Detail",
                                        "District",
                                        "Branch",
                                        "Godown",
                                        "Godown Type",
                                        "Crop Year",
                                        "Received Bags",
                                        "Received Weight",
                                        "Delivered Bags",
                                        "Delivered Weight",
                                        "Loss",
                                        "Gain",
                                        "Gain %",
                                        "No Of Days"
                                    ];

                                    var body = [];

                                    // Map the array of objects into an array of arrays
                                    consolidatedData.forEach(function (row) {
                                        body.push([
                                            row.RegionDetail,
                                            row.District,
                                            row.Branch,
                                            row.Godown,
                                            row.Godown_Type,
                                            row.Crop_Year,
                                            row.Received_Bags,
                                            row.Received_Weight,
                                            row.Delivered_Bags,
                                            row.Delivered_Weight,
                                            row.Loss,
                                            row.Gain,
                                            row.Gain_Percentage,
                                            row.No_Of_Days
                                        ]);
                                    });

                                    return {
                                        header: headers,
                                        body: body

                                    };
                                }
                            });
                            // 2. Execute the Excel export using the modified config
                            $.fn.dataTable.ext.buttons.excelHtml5.action.call(this, e, dt, button, exportConfig);
                        }
                    },
                    // STANDARD SUMMARY EXPORT BUTTON
                    {
                        extend: 'excelHtml5',

                        text: 'Export Summary Only',
                        title: 'Region Summary Report',
                        exportOptions: {
                            columns: ':visible:not(:eq(0))'
                        }
                    },
                    {
                        extend: 'print',

                        text: 'Print Report',
                        title: 'Region Summary Report',
                        exportOptions: {
                            columns: ':visible:not(:eq(0))'
                        }
                    }
                ]
            });
            // Event listener for opening and closing detail rows (UNCHANGED)
            $(gridId + ' tbody').off('click', 'td.region-detail-control').on('click', 'td.region-detail-control', function () {
                var tr = $(this).closest('tr');
                var row = summaryDataTable.row(tr);

                var iconCell = tr.find('td.details-icon');


                if (row.child.isShown()) {
                    var currentChild = row.child();
                    $('div.slider', currentChild).slideUp(400, function () {
                        row.child.hide();
                        tr.removeClass('shown');

                        iconCell.removeClass('shown');
                    });
                } else {
                    row.child(formatDetailRow(row.data())).show();

                    tr.addClass('shown');
                    iconCell.addClass('shown');

                    var detailRowElement = row.child();
                    var sliderDiv = $('div.slider', detailRowElement);
                    loadDetailData(row, row.data(), sliderDiv);

                    sliderDiv.slideDown(400);
                }
            });
        }


        $(document).ready(function () {
            $('.checkbox-multiselect').multiselect({
                enableFiltering: true,
                includeSelectAllOption: true,
                maxHeight: 250,
                nonSelectedText: 'Select one or more...',
                buttonWidth: '100%'
            });
            InitializeDataTable([]);
        });
    </script>
</asp:Content>
