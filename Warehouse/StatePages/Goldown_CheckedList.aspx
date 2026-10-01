<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Goldown_CheckedList.aspx.cs" Inherits="Goldown_CheckedList" %>

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

    <%-- 🌟 DataTables Buttons Extension Libraries 🌟 --%>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.7.1/css/buttons.dataTables.min.css" />
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.7.1/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.2/jszip.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.7.1/js/buttons.html5.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.7.1/js/buttons.print.min.js"></script>


    <style type="text/css">
        /* Base Container */
        .container {
            width: 95%;
            margin: 20px auto;
        }

        /* Filter Section Styling */
        .filter-container {
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 6px;
            background-color: #f9f9f9;
            margin-bottom: 20px;
        }

        /* Form Group Spacing */
        .form-group {
            margin-bottom: 15px;
        }

        /* Label Styling */
        label {
            font-weight: bold;
            display: block;
            margin-bottom: 5px;
            color: #333;
            margin-right: 12px;
        }

        /* Multi-select button styling for a cleaner look */
        .btn-group .multiselect {
            text-align: left;
            border-radius: 4px;
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            background-color: #fff;
            color: #555;
        }

        /* Search Button Style */
        .btn-primary {
            background-color: #007bff;
            border-color: #007bff;
            transition: background-color 0.3s ease;
        }

            .btn-primary:hover, .btn-primary:focus {
                background-color: #0056b3;
                border-color: #004085;
            }

        /* Gridview Styles for DataTables */
        .dataTables_wrapper {
            margin-top: 20px;
        }

        .parentGrid {
            width: 100% !important;
        }

        /* Clickable Region Column */
        td.region-detail-control {
            font-weight: bold;
            color: #007bff;
            cursor: pointer;
            text-decoration: underline;
        }

        /* Style for the expand/collapse icon (now purely visual feedback) */
        td.details-icon {
            background: url('https://cdn.datatables.net/examples/resources/details_open.png') no-repeat center center;
            cursor: pointer;
            width: 25px;
        }

        tr.shown td.details-icon {
            background: url('https://cdn.datatables.net/examples/resources/details_close.png') no-repeat center center;
        }

        /* Style for the nested table */
        div.slider {
            display: none;
        }

        table.nestedTable {
            margin: 10px;
            width: 98% !important;
            border: 1px solid #ccc;
            background-color: #fff;
        }

            table.nestedTable th {
                background-color: #e9ecef; /* Lighter background for nested headers */
                padding: 8px;
                text-align: left;
                border-bottom: 1px solid #ddd;
                color: #495057;
            }

            table.nestedTable td {
                padding: 8px;
            }

        /* DataTables Buttons alignment */
        div.dt-buttons {
            float: left;
            margin-right: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <h2>Warehouse Search Filters</h2>

        <div class="form-group">
            <div class="col-md-3">
                <label for="<%= ddlCropYear.ClientID %>">Crop Year (Multi Select):</label>
                <asp:ListBox ID="ddlCropYear" runat="server" SelectionMode="Multiple"
                    CssClass="checkbox-multiselect" Width="300px"></asp:ListBox>
            </div>

            <div class="col-md-3">
                <label for="<%= ddlCommodity.ClientID %>">Commodity (Multi Select):</label>
                <asp:ListBox ID="ddlCommodity" runat="server" SelectionMode="Multiple"
                    CssClass="checkbox-multiselect" Width="300px"></asp:ListBox>
            </div>

            <div class="col-md-3">
                <label for="<%= ddlGodownType.ClientID %>">Godown Type (Multi Select):</label>
                <asp:ListBox ID="ddlGodownType" runat="server" SelectionMode="Multiple"
                    CssClass="checkbox-multiselect" Width="300px"></asp:ListBox>
            </div>
        </div>
        <hr />

        <asp:Button ID="btnSearch" runat="server" Text="Search Warehouses"
            CssClass="btn btn-primary" OnClientClick="return LoadWarehouseSummary();" />

        <h3>Region Summary Results</h3>
        <div>
            <table id="gvRegions" class="parentGrid display" style="width: 100%">
                <thead>
                    <tr>
                        <th></th>
                        <%-- Icon column --%>
                        <th>Region</th>
                        <th>Total Rec. Weight (x/10)</th>
                        <th>Total Del. Weight (x/10)</th>
                        <th>Total Loss (x/10)</th>
                        <th>Total Gain (x/10)</th>
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

        function LoadWarehouseSummary() {
            var cropYears = $('#<%= ddlCropYear.ClientID %>').val();
            var commodities = $('#<%= ddlCommodity.ClientID %>').val();
            var godownTypes = $('#<%= ddlGodownType.ClientID %>').val();

            var filters = {
                cropYears: cropYears ? cropYears.join(',') : '',
                commodityIDs: commodities ? commodities.join(',') : '',
                godownTypes: godownTypes ? godownTypes.join(',') : ''
            };

            $('#gvRegions tbody').html('<tr><td colspan="7" style="text-align:center;">Loading summary data...</td></tr>');

            $.ajax({
                type: "POST",
                url: "Goldown_CheckedList.aspx/GetRegionSummary",
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
            var filters = {
                regionName: rowData.Region,
                cropYears: cropYears ? cropYears.join(',') : '',
                commodityIDs: commodities ? commodities.join(',') : '',
                godownTypes: godownTypes ? godownTypes.join(',') : ''
            };

            $.ajax({
                type: "POST",
                url: "Goldown_CheckedList.aspx/GetRegionDetailGrid",
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

        // --- NEW HELPER FUNCTION TO FETCH ALL DETAIL DATA FOR EXPORT ---
        function GetAllRegionDetailsForExport(regionData) {
            var cropYears = $('#<%= ddlCropYear.ClientID %>').val();
            var commodities = $('#<%= ddlCommodity.ClientID %>').val();
            var godownTypes = $('#<%= ddlGodownType.ClientID %>').val();

            // Collect all region names for a single batch call to the server-side method
            var regionNames = regionData.map(function (item) {
                return item.Region;
            }).join(',');

            var filters = {
                regionNames: regionNames,
                cropYears: cropYears ? cropYears.join(',') : '',
                commodityIDs: commodities ? commodities.join(',') : '',
                godownTypes: godownTypes ? godownTypes.join(',') : ''
            };

            // Assuming you will create a new C# WebMethod called "GetAllRegionDetails" 
            // that accepts comma-separated regionNames and returns all detail tables' HTML 
            // consolidated into one large string.
            return $.ajax({
                type: "POST",
                url: "Goldown_CheckedList.aspx/GetAllRegionDetails",
                data: JSON.stringify(filters),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                // Setting async to false is generally discouraged, but necessary here 
                // to ensure data is fetched before the button function continues.
                // A better approach is often to show a "Loading..." modal.
                async: false
            }).responseText;
        }

        // 📌 Not Use MODIFIED: InitializeDataTable function with Custom Export Button
        function InitializeDataTable123(data) {
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
                // 🌟 CUSTOM BUTTON CONFIGURATION
                "buttons": [
                    {
                        // New custom button to trigger a consolidated export
                        extend: 'excelHtml5',
                        text: 'Export ALL Data to Excel 💾',
                        title: 'Consolidated Region Report',
                        exportOptions: {
                            // Exclude the icon column
                            columns: ':visible:not(:eq(0))'
                        },
                        customize: function (xlsx) {
                            // Get the main region summary sheet
                            var sheet = xlsx.xl.worksheets['sheet1.xml'];

                            // Get all parent data rows currently filtered/displayed
                            var visibleData = summaryDataTable.rows({ search: 'applied' }).data().toArray();

                            // Call the new synchronous AJAX function to get all detail HTML 
                            // (this requires a new C# method - see section below)
                            var responseText = GetAllRegionDetailsForExport(visibleData);

                            // Parse the JSON response
                            var jsonResponse = JSON.parse(responseText);
                            var consolidatedHtml = jsonResponse.d;

                            if (consolidatedHtml) {
                                // 1. Create a temporary element to hold the generated HTML tables
                                var tempContainer = $('<div>').html(consolidatedHtml);

                                // 2. Prepare the consolidated data table structure
                                var $tempTable = $('<table>').addClass('dataTable');

                                // 3. Append the main table (from DataTables)
                                // Note: DataTables creates a copy of the table structure internally for export.
                                // We are focusing on appending the detail data.

                                // 4. Iterate over each *nestedTable* in the fetched HTML and append its rows/data
                                tempContainer.find('table.nestedTable').each(function () {
                                    var $nestedTable = $(this);

                                    // Get the region name associated with the detail table (if possible)
                                    // We assume the C# method wraps the table with a header/context.
                                    // For simplicity here, we'll just insert a separator row.
                                    var regionHeader = $('<tr><td colspan="7" style="background-color:#d0d0d0; font-weight:bold;">DETAILS: ' +
                                        $nestedTable.prev('div').text() + '</td></tr>');

                                    $tempTable.append(regionHeader);

                                    // Append header and body rows from the nested table
                                    // Clone headers to ensure they are present for each detail section
                                    $tempTable.append($nestedTable.find('thead').clone().find('tr').children().prepend($('<th>').text('Region Detail')).parent());
                                    $tempTable.append($nestedTable.find('tbody').clone().find('tr').children().prepend($('<td>').text('DETAIL')).parent());
                                });

                                // This is the simplest way to inject: append the custom table's HTML 
                                // as a new set of rows at the end of the main data table in the XML.
                                // *NOTE: This assumes the column count of the main and detail tables are consistent for clean export.*

                                // For a reliable method, you typically need to use the DataTables API 
                                // to get the existing table data and then APPEND new data objects/arrays 
                                // before the export process.

                                // A simpler, more reliable approach is to return a custom HTML string 
                                // that represents the final consolidated output:
                                var fullTableHtml = '<html><head><style>table, td, th {border: 1px solid black;}</style></head><body>' +
                                    '<h2>Region Summary</h2>' + summaryDataTable.table().node().outerHTML +
                                    '<h2>Region Details</h2>' + consolidatedHtml +
                                    '</body></html>';

                                // For Excel export, returning the HTML will be ignored.
                                // Instead, we manipulate the XML of the existing sheet for the custom data:

                                // This part is highly dependent on DataTables/Excel XML structure and non-trivial.
                                // **RECOMMENDED ACTION:** The cleanest solution is for the server-side C# code
                                // (`Goldown_CheckedList.aspx.cs`) to produce the consolidated data as an **Array of Objects (JSON)**, 
                                // and then this JavaScript `customize` function can easily inject those objects 
                                // as new rows into the DataTables internal structure before export.

                                // Since we have HTML, a simpler approach is to use a different export button 
                                // like `copyHtml5` or to generate a completely separate file.

                                // --- FALLBACK: Use a separate button for HTML-based print/export ---
                                // For the HTML approach, the 'Print' button is the easiest to modify for a consolidated view.
                            }
                            // For Excel, manipulation is too complex. We will stick to the default excel export 
                            // for the main table and add a new button for a consolidated print/HTML view.

                        }
                    },
                    // The existing print button is the best way to do a consolidated view with your current structure
                    {
                        extend: 'print',
                        text: 'Print ALL Data',
                        title: 'Region Summary Report',
                        exportOptions: {
                            columns: ':visible:not(:eq(0))'
                        },
                        // CUSTOMIZE FOR PRINT
                        customize: function (win) {
                            var visibleData = summaryDataTable.rows({ search: 'applied' }).data().toArray();
                            var responseText = GetAllRegionDetailsForExport(visibleData);
                            var jsonResponse = JSON.parse(responseText);
                            var consolidatedHtml = jsonResponse.d;

                            // Append the consolidated detail HTML to the print window body
                            if (consolidatedHtml) {
                                $(win.document.body).append('<br><h3>Region Details Breakdown</h3>');
                                $(win.document.body).append(consolidatedHtml);
                            }
                        }
                    },
                    // The original Excel button is kept, but its text is changed
                    {
                        extend: 'excelHtml5',
                        text: 'Export Summary Only (Excel)',
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

        var summaryDataTable;

        // --- NEW HELPER FUNCTION TO FETCH AND CONSOLIDATE DATA FOR EXCEL ---
        // This function calls the server-side method to get all parent and child data as JSON.
      
        function getConsolidatedExportData() {
            var cropYears = $('#<%= ddlCropYear.ClientID %>').val();
            var commodities = $('#<%= ddlCommodity.ClientID %>').val();
            var godownTypes = $('#<%= ddlGodownType.ClientID %>').val();

            var filters = {
                cropYears: cropYears ? cropYears.join(',') : '',
                commodityIDs: commodities ? commodities.join(',') : '',
                godownTypes: godownTypes ? godownTypes.join(',') : ''
            };

            var consolidatedData = [];

            // NOTE: This MUST be synchronous (async: false) so the DataTables button 
            // waits for the data before proceeding with the export.
            $.ajax({
                type: "POST",
                url: "Goldown_CheckedList.aspx/GetConsolidatedExportData", // The C# method we created
                data: JSON.stringify(filters),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    consolidatedData = response.d || [];
                },
                error: function (xhr, status, error) {
                    console.error("Consolidated Export AJAX Error:", error);
                    // Alert user and return empty list
                    alert("Error fetching consolidated data for export. Check server logs.");
                }
            });

            return consolidatedData;
        }
        function LoadWarehouseSummary() {
            var cropYears = $('#<%= ddlCropYear.ClientID %>').val();
            var commodities = $('#<%= ddlCommodity.ClientID %>').val();
            var godownTypes = $('#<%= ddlGodownType.ClientID %>').val();

            var filters = {
                cropYears: cropYears ? cropYears.join(',') : '',
                commodityIDs: commodities ? commodities.join(',') : '',
                godownTypes: godownTypes ? godownTypes.join(',') : ''
            };

            $('#gvRegions tbody').html('<tr><td colspan="7" style="text-align:center;">Loading summary data...</td></tr>');

            $.ajax({
                type: "POST",
                url: "Goldown_CheckedList.aspx/GetRegionSummary",
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
            var filters = {
                regionName: rowData.Region,
                cropYears: cropYears ? cropYears.join(',') : '',
                commodityIDs: commodities ? commodities.join(',') : '',
                godownTypes: godownTypes ? godownTypes.join(',') : ''
            };

            $.ajax({
                type: "POST",
                url: "Goldown_CheckedList.aspx/GetRegionDetailGrid",
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
                                alert("No data found for consolidated export.");
                                return;
                            }

                            // 1. Define the custom exportData function
                            var exportConfig = $.extend({}, config, {
                                exportData: function (dt) {
                                    // Define the headers that match the final consolidated data structure
                                    var headers = ["Region/Detail", "Rec. Weight", "Del. Weight", "Loss", "Gain", "Gain %"];
                                    var body = [];

                                    // Map the array of objects into an array of arrays
                                    consolidatedData.forEach(function (row) {
                                        body.push([
                                            row.RegionDetail,
                                            row.Received_Weight,
                                            row.Delivered_Weight,
                                            row.Loss,
                                            row.Gain,
                                            row.Gain_Percentage
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
                buttonWidth: '300px'
            });

            InitializeDataTable([]);
        });

    </script>
</asp:Content>
