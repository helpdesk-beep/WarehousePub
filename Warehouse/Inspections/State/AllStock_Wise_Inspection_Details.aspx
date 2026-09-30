<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="AllStock_Wise_Inspection_Details.aspx.cs"
    Inherits="Inspections_State_AllStock_Wise_Inspection_Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>All Stock Wise Inspection Details</title>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Font Awesome -->
    <link rel="stylesheet"
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" />

    <!-- Excel -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.18.5/xlsx.full.min.js"></script>

    <style>
        body {
            background: linear-gradient(to right, #eef2f7, #e3ebf6);
            font-family: 'Segoe UI', sans-serif;
            overflow-x: hidden;
        }

        .full-width-container {
            width: 97%;
            margin: auto;
            max-width: 100%;
        }

        .header-box {
            padding: 18px;
            border-radius: 18px;
            background: linear-gradient(135deg, #4e73df, #6f42c1);
            color: white;
            box-shadow: 0 8px 25px rgba(0,0,0,0.15);
        }

        .info-card {
            background: rgba(255,255,255,0.95);
            border-radius: 18px;
            padding: 20px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.08);
            margin-bottom: 20px;
        }

        .detail-item {
            padding: 12px;
            border-right: 1px solid #eee;
        }

        .label-text {
            display: block;
            font-size: 11px;
            font-weight: 700;
            color: #777;
            text-transform: uppercase;
            margin-bottom: 5px;
        }

        .value-text {
            font-size: 15px;
            font-weight: 600;
            color: #2d3436;
        }

        .search-section {
            background: white;
            padding: 15px;
            border-radius: 15px;
            margin-bottom: 20px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.05);
        }

        .grid-container {
            height: 580px;
            overflow-y: auto;
            background: white;
            border-radius: 15px;
            padding: 10px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.08);
        }

        .custom-grid {
            width: 100% !important;
            font-size: 12px;
        }

            .custom-grid thead th {
                position: sticky;
                top: 0;
                z-index: 100;
                background: #3f51b5 !important;
                color: white !important;
                text-align: center;
                vertical-align: middle;
                padding: 12px;
                white-space: nowrap;
            }

            .custom-grid tbody td,
            .custom-grid tfoot td {
                text-align: center;
                vertical-align: middle;
                padding: 6px;
                word-break: break-word;
            }

            .custom-grid tfoot td {
                background: #dbe4ff !important;
                font-weight: bold;
                color: #000;
            }

        .godown-col {
            min-width: 220px;
            text-align: left !important;
            padding-left: 10px !important;
            font-weight: 600;
        }

        .remark-cell {
            width: 160px;
            min-width: 160px;
            max-width: 160px;
            text-align: left;
            line-height: 18px;
        }

        .badge-diff {
            padding: 6px 12px;
            border-radius: 50px;
            font-size: 12px;
            font-weight: 700;
            display: inline-block;
        }

        .diff-success {
            background: #d1fae5;
            color: #047857;
        }

        .diff-danger {
            background: #fee2e2;
            color: #dc2626;
        }

        .img-thumb {
            width: 55px;
            height: 55px;
            object-fit: cover;
            border-radius: 10px;
            cursor: pointer;
            transition: 0.3s;
            border: 2px solid #e5e7eb;
        }

            .img-thumb:hover {
                transform: scale(1.08);
            }

        .btn-export {
            border-radius: 10px;
            padding: 10px 18px;
            font-weight: 600;
            width: 100%;
        }

        @media(max-width:768px) {

            .detail-item {
                border-right: none;
                border-bottom: 1px solid #eee;
            }

            .grid-container {
                height: auto;
            }
        }
    </style>

</head>

<body>

    <form id="form1" runat="server">

        <div class="full-width-container mt-3">

            <!-- HEADER --> 
            <div class="header-box text-center mb-4">

                <h2>
                    <i class="fa-solid fa-warehouse me-2"></i>
                    All Godown Stock Wise Inspection Details
                </h2>

            </div>

            <!-- INFO -->
            <div class="info-card">

                <div class="row g-3">

                    <div class="col-md-4 detail-item">

                        <span class="label-text">Officer</span>

                        <asp:Label ID="lblOfficer"
                            runat="server"
                            CssClass="value-text">
                        </asp:Label>

                    </div>

                    <div class="col-md-4 detail-item">

                        <span class="label-text">District</span>

                        <asp:Label ID="lblDistrict"
                            runat="server"
                            CssClass="value-text">
                        </asp:Label>

                    </div>

                    <div class="col-md-4 detail-item">

                        <span class="label-text">Depot</span>

                        <asp:Label ID="lblDepot"
                            runat="server"
                            CssClass="value-text">
                        </asp:Label>

                    </div>

                </div>

            </div>

            <!-- SEARCH -->
            <div class="search-section">

                <div class="row align-items-center g-2">

                    <div class="col-md-8">

                        <asp:TextBox ID="txtSearch"
                            runat="server"
                            CssClass="form-control"
                            placeholder="🔍 Search Here..."
                            onkeyup="SearchGrid()">
                        </asp:TextBox>

                    </div>

                    <div class="col-md-2">

                        <button type="button"
                            class="btn btn-success btn-export"
                            onclick="ExportToExcel()">
                            📥 Export Excel

                        </button>

                    </div>

                    <div class="col-md-2">

                        <button type="button"
                            class="btn btn-danger btn-export"
                            onclick="PrintReport()">
                            🖨 Print Report

                        </button>

                    </div>

                </div>

            </div>

            <!-- GRID -->
            <div class="grid-container">

                <asp:GridView ID="gvReport"
                    runat="server"
                    AutoGenerateColumns="False"
                    CssClass="table table-hover table-bordered custom-grid"
                    ShowFooter="true"
                    GridLines="None"
                    EmptyDataText="No Record Found"
                    OnRowDataBound="gvReport_RowDataBound">

                    <Columns>

                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Godown Name">
                            <ItemTemplate>
                                <div class="godown-col">
                                    <%# Eval("Godown_Name") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                        <asp:BoundField DataField="Stack_Name"
                            HeaderText="Stack Name" />
                        
                        <asp:BoundField DataField="Crop_Year"
                            HeaderText="Crop Year" />
                        
                        <asp:BoundField DataField="Depositer_Name"
                            HeaderText="Depositer Name" />
                        
                        <asp:BoundField DataField="Commodity_Name"
                            HeaderText="Commodity Name" />

                        <asp:BoundField DataField="OnlineBags"
                            HeaderText="Online Bags" />

                        <asp:BoundField DataField="PV_Bags"
                            HeaderText="PV Bags" />

                        <asp:BoundField DataField="SpillageBags"
                            HeaderText="Spillage" />

                        <asp:TemplateField HeaderText="Difference">

                            <ItemTemplate>

                                <div class='<%# Convert.ToInt32(Eval("Difference")) == 0 ? "badge-diff diff-success" : "badge-diff diff-danger" %>'>

                                    <%# Eval("Difference") %>
                                </div>

                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Remark">

                            <ItemTemplate>

                                <div class="remark-cell">
                                    <%# Eval("Remark") %>
                                </div>

                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Stack Image">

                            <ItemTemplate>

                                <img src='<%# Eval("StackImageBase64") %>'
                                    class="img-thumb"
                                    onclick='<%# "showImage(\"" + Eval("StackImageBase64") + "\")" %>'
                                    data-bs-toggle="modal"
                                    data-bs-target="#imgModal"
                                    onerror="this.src=''" />

                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Commodity Image">

                            <ItemTemplate>

                                <img src='<%# Eval("CommodityImageBase64") %>'
                                    class="img-thumb"
                                    onclick='<%# "showImage(\"" + Eval("CommodityImageBase64") + "\")" %>'
                                    data-bs-toggle="modal"
                                    data-bs-target="#imgModal"
                                    onerror="this.src=''" />

                            </ItemTemplate>

                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            </div>

        </div>

        <!-- IMAGE MODAL -->
        <div class="modal fade"
            id="imgModal"
            tabindex="-1">

            <div class="modal-dialog modal-xl modal-dialog-centered">

                <div class="modal-content bg-dark border-0">

                    <div class="modal-header border-0">

                        <h5 class="modal-title text-white">Image Preview
                        </h5>

                        <button type="button"
                            class="btn-close btn-close-white"
                            data-bs-dismiss="modal">
                        </button>

                    </div>

                    <div class="modal-body text-center">

                        <img id="modalImage"
                            class="img-fluid"
                            style="max-height: 80vh; object-fit: contain;" />

                    </div>

                </div>

            </div>

        </div>

    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

    <script>

        function showImage(src) {

            document.getElementById("modalImage").src = src;

        }

        // SEARCH
        function SearchGrid() {

            var input =
                document.getElementById('<%= txtSearch.ClientID %>');

        var filter =
            input.value.toLowerCase();

        var table =
            document.getElementById('<%= gvReport.ClientID %>');

            var tr =
                table.getElementsByTagName("tr");

            for (var i = 1; i < tr.length; i++) {

                var td = tr[i].getElementsByTagName("td");

                var found = false;

                for (var j = 0; j < td.length; j++) {

                    if (td[j] &&
                        td[j].innerText.toLowerCase().indexOf(filter) > -1) {

                        found = true;
                        break;
                    }
                }

                tr[i].style.display = found ? "" : "none";
            }
        }

        // EXCEL EXPORT
        function ExportToExcel() {

            var table =
                document.getElementById('<%= gvReport.ClientID %>');

            var clonedTable =
                table.cloneNode(true);

            // REMOVE IMAGE COLUMNS
            for (var i = 0; i < clonedTable.rows.length; i++) {

                var row = clonedTable.rows[i];

                if (row.cells.length > 1) {

                    row.deleteCell(row.cells.length - 1);
                    row.deleteCell(row.cells.length - 1);
                }
            }

            var wb =
                XLSX.utils.table_to_book(clonedTable,
                    {
                        sheet: "Godown Report"
                    });

            XLSX.writeFile(wb,
                "Godown_Stack_Report.xlsx");
        }

        // PRINT
        function PrintReport() {

            var table =
                document.getElementById('<%= gvReport.ClientID %>');

        var clonedTable =
            table.cloneNode(true);

        // REMOVE IMAGE COLUMNS
        for (var i = 0; i < clonedTable.rows.length; i++) {

            var row = clonedTable.rows[i];

            if (row.cells.length > 1) {

                row.deleteCell(row.cells.length - 1);
                row.deleteCell(row.cells.length - 1);
            }
        }

        var officer =
            document.getElementById('<%= lblOfficer.ClientID %>').innerText;

        var district =
            document.getElementById('<%= lblDistrict.ClientID %>').innerText;

        var depot =
            document.getElementById('<%= lblDepot.ClientID %>').innerText;

                     var win =
                         window.open('', '', 'width=1600,height=900');

                     win.document.write(`

        <html>

        <head>

        <title>Godown Stack Report</title>

        <style>

            @page{
                size: landscape;
                margin: 10mm;
            }

            body{
                font-family:Arial;
                padding:10px;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }

            h2{
                text-align:center;
                margin-bottom:15px;
            }

            .top-info{
                margin-bottom:15px;
                font-size:14px;
                font-weight:bold;
            }

            .top-info span{
                margin-right:30px;
            }

            table{
                width:100%;
                border-collapse:collapse;
                font-size:11px;
            }

            th{
                background:#3f51b5 !important;
                color:#fff !important;
                border:1px solid #000;
                padding:8px;
                text-align:center;
                white-space:nowrap;
            }

            td{
                border:1px solid #000;
                padding:6px;
                text-align:center;
                vertical-align:middle;
                word-break:break-word;
            }

            tfoot td{
                background:#dbe4ff !important;
                font-weight:bold;
            }

            img{
                display:none;
            }

            tr{
                page-break-inside:avoid;
            }

            .remark-cell{
                width:140px !important;
                text-align:left;
                line-height:17px;
            }

        </style>

        </head>

        <body> 

            <h2>
                All Godown Stock Wise Inspection Details
            </h2>

            <div class="top-info">

                <span>
                    Officer : ${officer}
                </span>

                <span>
                    District : ${district}
                </span>

                <span>
                    Depot : ${depot}
                </span>

            </div>

        `);

                     win.document.write(clonedTable.outerHTML);

                     win.document.write(`

        </body>
        </html>

        `);

                     win.document.close();

                     setTimeout(function () {

                         win.focus();
                         win.print();

                     }, 700);
                 }

    </script>

</body>
</html>
