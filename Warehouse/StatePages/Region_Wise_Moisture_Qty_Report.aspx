<%@ Page Title="Region Wise Moisture Quantity Report"
    Language="C#"
    MasterPageFile="~/MasterPage/StateMaster.master"
    AutoEventWireup="true"
    CodeFile="~/StatePages/Region_Wise_Moisture_Qty_Report.aspx.cs"
    Inherits="StatePages_Region_Wise_Moisture_Qty_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        body {
            background-color: #f8f9fa;
            font-family: 'Segoe UI', Arial, sans-serif;
        }

        .main-container {
            background: #fff;
            padding: 15px;
            border-radius: 8px;
            box-shadow: 0px 2px 10px rgba(0,0,0,0.10);
        }

        .header-table {
            width: 100%;
            border-bottom: 2px solid #1a5276;
            margin-bottom: 8px;
        }

        .org-title {
            color: #1a5276;
            font-size: 22px;
            font-weight: bold;
            text-transform: uppercase;
            margin: 0;
        }

        .report-title {
            font-size: 15px;
            font-weight: 600;
            color: #000;
        }

        .Grid {
            width: 100%;
            border-collapse: collapse !important;
            margin-top: 10px;
        }

            .Grid th {
                background-color: #1a5276 !important;
                color: White !important;
                padding: 8px;
                border: 1px solid #444 !important;
                text-align: center;
                font-size: 13px;
            }

            .Grid td {
                padding: 6px 8px;
                border: 1px solid #ccc !important;
                font-size: 12px;
                color: #000;
            }

            .Grid tr:nth-child(even) {
                background-color: #f8f9fa;
            }

            .Grid tr:hover {
                background-color: #eaf4ff;
            }

        .footer-style td {
            background-color: #f2f2f2 !important;
            font-weight: bold !important;
            border-top: 2px solid #000 !important;
        }

        .no-print {
            margin-bottom: 10px;
            display: flex;
            gap: 10px;
            justify-content: flex-end;
            padding: 8px;
            background: #e9ecef;
            border-radius: 5px;
        }

        @media print {

            @page {
                size: A4;
                margin: 1cm;
            }

            .no-print {
                display: none !important;
            }

            .main-container {
                box-shadow: none !important;
                padding: 0 !important;
            }

            .Grid thead {
                display: table-header-group;
            }

            .Grid tr {
                page-break-inside: avoid;
            }
        }
    </style>
    <script type="text/javascript">

        function PrintGrid() {

            var divContents =
                document.getElementById("printArea").innerHTML;

            var printWindow =
                window.open('', '', 'height=700,width=1000');

            printWindow.document.write(
                '<html><head><title>Region Wise Moisture Quantity Report</title>');

            printWindow.document.write(
                '<style>' +

                'body{font-family:Arial;font-size:12px;}' +

                'table{width:100%;border-collapse:collapse;}' +

                'th{background:#1a5276;color:white;border:1px solid #000;padding:6px;text-align:center;}' +

                'td{border:1px solid #000;padding:5px;}' +

                '.header{text-align:center;margin-bottom:15px;}' +

                '</style>');

            printWindow.document.write('</head><body>');

            printWindow.document.write(divContents);

            printWindow.document.write('</body></html>');

            printWindow.document.close();

            printWindow.focus();

            setTimeout(function () {
                printWindow.print();
                printWindow.close();
            }, 500);
        }

    </script>


</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <div class="container-fluid">


        <div class="no-print">
            <asp:Button ID="btnExport"
                runat="server"
                Text="Export To Excel"
                CssClass="btn btn-success btn-sm"
                Visible="false"
                OnClick="btnExport_Click" />

            <asp:Button ID="btnPrint"
                runat="server"
                Text="Print / Save PDF"
                CssClass="btn btn-danger btn-sm"
                OnClientClick="PrintGrid();return false;" />


        </div>

        <div id="printArea" class="main-container">

            <table class="header-table">
                <tr>

                    <td style="width: 70px;">
                        <img src="../../images/mpwlc.png"
                            style="width: 60px;" />
                    </td>

                    <td class="text-center">
                        <h1 class="org-title">M.P. Warehousing & Logistics Corporation
                        </h1>

                        <div class="report-title">
                            Region Wise Moisture Quantity Report
                        </div>

                    </td>

                    <td style="width: 220px; text-align: right; font-size: 11px;">
                        <strong>Date :</strong>
                        <asp:Label ID="lblDate"
                            runat="server"></asp:Label>

                        <br />

                        <strong>Total Records :</strong>
                        <asp:Label ID="lblTotalRecords"
                            runat="server"></asp:Label>

                        <br />

                        <strong>Unit :</strong> Qty In M.T.


                    </td>

                </tr>
            </table>

            <div class="table-responsive">

                <asp:GridView ID="gvReport"
                    runat="server"
                    AutoGenerateColumns="False"
                    CssClass="table table-bordered Grid"
                    ShowFooter="true"
                    EmptyDataText="No Record Found"
                    OnRowDataBound="gvReport_RowDataBound">
                    <Columns>

                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"
                                Width="50px" />
                        </asp:TemplateField>

                        <asp:BoundField
                            DataField="Region Name"
                            HeaderText="Region Name">
                            <ItemStyle Font-Bold="true" />
                        </asp:BoundField>

                        <asp:BoundField
                            DataField="Total Received Qty"
                            HeaderText="Total Received Qty (MT)"
                            DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>

                        <asp:BoundField
                            DataField="Total Moisture Qty"
                            HeaderText="Total Moisture Qty (MT)"
                            DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="center" />
                        </asp:BoundField>

                    </Columns>

                    <FooterStyle CssClass="footer-style" />

                </asp:GridView>

            </div>

        </div>

    </div>

</asp:Content>
