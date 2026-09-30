<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Region/Reports/Rpt_Godown_Bill_Details.aspx.cs" Inherits="Region_Reports_Rpt_Godown_Bill_Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bill Wise Details</title>
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />

    <style type="text/css">
        fieldset {
            border: 1px solid #2095A1;
            padding: 0.35em 0.625em 0.75em;
            margin: 10px;
            border-radius: 5px;
            padding-left: 20px;
        }

        legend {
            padding: 2px 8px;
            border-radius: 10px;
            width: auto;
            border: 1px solid #2095A1;
            font-size: 17px;
            font-weight: bold;
            color: #030203;
        }

        .content-wrapper {
            padding: 1.75rem 1.25rem;
        }

        .table-bordered th, .table-bordered td {
            border: 1px solid #030203;
        }

        .form-control {
            border: 1px solid #767B83;
            border-radius: 8px;
        }

        .table th {
            text-align: center;
        }

        .form-inline {
            display: block !important;
        }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: #647e68 !important;
            color: black !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
            color: black !important;
        }

        element.style {
            font-size: medium !important;
        }

        /* Responsive Grid & Table Styles */
        .table-responsive {
            overflow-x: auto;
        }

        /* Modern Grid Header Style */
        .custom-grid th {
            background: linear-gradient(90deg, #767B83, #767B83); /* Smooth blue gradient */
            color: #ffffff !important;
            text-align: center;
            font-weight: 600;
            font-size: 14px;
            padding: 8px;
            border-bottom: 2px solid #023e8a !important;
        }

        /* Optional: Slight hover highlight for rows */
        .custom-grid tr:hover td {
            background-color: #e3f2fd !important;
        }
        /* Alternate row colors for better readability */
        .custom-grid tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        /* Table borders and cell spacing */
        .table-bordered th, .table-bordered td {
            border: 1px solid #222 !important;
            vertical-align: middle;
        }

        /* Bold text for Godown Info */
        #lblGodownInfo {
            font-size: 20px !important;
            color: #0a3d62 !important;
            font-weight: bold !important;
        }

        /* Pagination styling */
        .GridPager {
            text-align: right;
            padding: 8px;
        }

        /* === Print View Cleanup (Hide Footer + Pager) === */
        @media print {
            .btn, .GridPager, tfoot, .grid-footer, tr:last-child {
                display: none !important;
                visibility: hidden !important;
            }

            body {
                margin: 0;
            }

            table {
                border-collapse: collapse !important;
                width: 100%;
            }

            th, td {
                border: 1px solid black !important;
                padding: 4px;
            }
        }
        /* === Buttons === */
        .btn-export {
            margin-right: 8px;
            font-weight: 600;
        }
    </style>
    <style type="text/css">
        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 0px 0px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-weight: bold;
            margin: 4px 2px;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            cursor: pointer;
        }

        .button1 {
            background-color: white;
            color: black;
            border: 2px solid #4CAF50;
        }

            .button1:hover {
                background-color: #4CAF50;
                color: white;
            }

        .button2 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button2:hover {
                background-color: #008CBA;
                color: white;
            }

        .auto-style1 {
            height: 10px;
            width: 558px;
        }

        .auto-style2 {
            width: 558px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container_wrapper">
            <fieldset>
                <legend>Bill Wise Details</legend>
                <div class="mb-2">
                    <asp:Button ID="btnBack" runat="server" CssClass="btn-export" Text="Back" OnClick="btnBack_Click" />
                    <asp:Button ID="btnExportExcel" runat="server" CssClass="btn-export" Text="Export to Excel" OnClick="btnExportExcel_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary btn-export" Text="Print" OnClientClick="return printGrid();" />
                </div>
                <!-- Godown Info -->
                <div class="text-center mb-3" style="margin-bottom: 20px">
                    <asp:Label ID="lblGodownInfo" runat="server"
                        CssClass="fw-bold text-primary"
                        Style="font-size: 20px; font-weight: 700; text-align: center; display: block;">
                    </asp:Label>
                </div>

                <!-- Responsive Grid -->
                <div class="table-responsive">
                    <asp:GridView ID="grdBillDetails" runat="server"
                        CssClass="table table-bordered table-hover custom-grid"
                        AutoGenerateColumns="true" AllowPaging="true" PageSize="20"
                        OnPageIndexChanging="grdBillDetails_PageIndexChanging">
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
        <script type="text/javascript">
            // 🖨 Print functionality for GridView
            function printGrid() {
                var grid = document.getElementById('<%= grdBillDetails.ClientID %>');
                var printWindow = window.open('', '', 'height=700,width=900');
                printWindow.document.write('<html><head><title>Pending Bill For Generation</title>');
                printWindow.document.write('<style>table{border-collapse:collapse;width:100%;}th,td{border:1px solid black;padding:6px;text-align:center;}th{background:#cfe2f3;}tfoot,tr:last-child{display:none;}</style>');
                printWindow.document.write('</head><body>');
                printWindow.document.write('<h3 style="text-align:center;">Pending Bill For Generation</h3>');
                printWindow.document.write(grid.outerHTML);
                printWindow.document.write('</body></html>');
                printWindow.document.close();
                printWindow.print();
                return false;
            }
        </script>
    </form>
</body>
</html>
