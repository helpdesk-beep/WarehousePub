<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/NCCF/Branch_NCCF_Print_Bill_Multiple_Bill.aspx.cs" Inherits="NCCF_Branch_NCCF_Print_Bill_Multiple_Bill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NCCF Storage Bill - Multiple Bills</title>

    <link href="http://mpsc.mp.nic.in/Warehouse/Administration/assets/css/Table-MultiDesigns.css" rel="stylesheet" type="text/css" />
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>" type="text/javascript"></script>
    <link href="http://mpsc.mp.nic.in/Warehouse/Administration/assets/Resources/js/Alerts/sweetalert.css" rel="stylesheet" type="text/css" />
    <script src="http://mpsc.mp.nic.in/Warehouse/Administration/assets/Resources/js/Alerts/sweetalert-dev.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"></script>

    <style>
        body {
            font-family: 'Segoe UI', Calibri, sans-serif;
            background: #fff;
            margin: 0;
            padding: 0;
        }

        #printWrapper {
            width: 950px;
            margin: 20px auto;
            padding: 15px;
            border: 2px solid #000;
            border-radius: 6px;
            box-sizing: border-box;
        }

        .bill-page {
            page-break-after: always;
            margin-bottom: 30px;
            border-bottom: 3px dashed #333;
            padding-bottom: 20px;
        }

        .top-nav {
            margin-bottom: 15px;
            font-size: 14px;
            font-weight: bold;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .top-nav a {
            text-decoration: none;
            color: #0066cc;
            font-weight: bold;
            margin-right: 15px;
        }

        .invoice-header {
            text-align: center;
            border-bottom: 3px solid #000;
            padding-bottom: 15px;
            margin-bottom: 20px;
        }

        .invoice-header strong {
            font-size: 22px;
            text-transform: uppercase;
        }

        .invoice-header div {
            margin-top: 5px;
            font-size: 14px;
        }

        .info-box {
            width: 95%;
            border: 2px solid #000;
            padding: 10px 15px;
            margin-bottom: 20px;
            background: #fafafa;
            border-radius: 6px;
            font-size: 14px;
        }

        .info-box table {
            width: 100%;
            border-collapse: collapse;
        }

        .info-box td {
            padding: 5px;
            vertical-align: top;
        }

        .info-box strong {
            font-weight: 600;
        }

        .EU_DataTable {
            border: 1px solid #333;
            font-size: 13px;
            width: 100%;
            margin-top: 10px;
            border-collapse: collapse;
            table-layout: fixed;
        }

        .EU_DataTable th, .EU_DataTable td {
            border: 1px solid #333 !important;
            padding: 6px;
            text-align: center;
        }

        .EU_DataTable tr:nth-child(even) td {
            background: #f4faff;
        }

        .amount-box {
            margin-top: 25px;
            padding: 12px;
            border: 2px solid #e40000;
            background: #fff4f4;
            color: #e40000;
            font-weight: bold;
            border-radius: 6px;
            font-size: 15px;
        }

        .signature-box {
            width: 100%;
            margin-top: 30px;
            padding: 12px;
            border: 2px solid #000;
            border-radius: 6px;
            box-sizing: border-box;
        }

        .signature-box table {
            width: 100%;
            table-layout: fixed;
        }

        .signature-box td {
            text-align: center;
            vertical-align: top;
        }

        .signature-title {
            font-weight: bold;
            font-size: 14px;
            margin-top: 10px;
            display: block;
        }

        .notice-box {
            margin-top: 30px;
            padding: 10px;
            text-align: center;
            color: red;
            border-top: 3px solid #000;
            font-weight: bold;
            font-size: 14px;
        }

        .summary-box {
            background: #f0f8ff;
            border: 2px solid #0066cc;
            padding: 10px;
            margin-bottom: 20px;
            border-radius: 5px;
            font-weight: bold;
        }

        #loaderOverlay {
            display: none;
            position: fixed;
            z-index: 9999;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background: rgba(255, 255, 255, 0.8);
            align-items: center;
            justify-content: center;
        }

        .spinner {
            border: 8px solid #f3f3f3;
            border-top: 8px solid #007bff;
            border-radius: 50%;
            width: 60px;
            height: 60px;
            animation: spin 1s linear infinite;
        }

        @keyframes spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }

        .btn-export {
            background-color: #007bff;
            color: white;
            font-size: 14px;
            font-weight: bold;
            padding: 8px 18px;
            border: none;
            border-radius: 6px;
            cursor: pointer;
        }

        .btn-export:hover {
            background-color: #0056b3;
        }

        @media print {
            body {
                margin: 0;
                padding: 0;
            }

            #printWrapper {
                width: 950px;
                margin: 0 auto;
                border: 2px solid #000;
                padding: 15px;
                border-radius: 6px;
                box-sizing: border-box;
            }

            .top-nav, .btn-export, .summary-box {
                display: none !important;
            }

            table, th, td {
                border-collapse: collapse !important;
                page-break-inside: avoid;
            }

            .bill-page {
                page-break-after: always;
                border-bottom: none;
            }
        }

        @page {
            size: A4 portrait;
            margin: 10mm;
        }
    </style>
</head>
<body>
    <div id="loaderOverlay">
        <div class="spinner"></div>
    </div>

    <form id="form1" runat="server">
        <div id="printWrapper">
            <div class="top-nav">
                <div>
                    <a href="javascript:void(0);" onclick="window.history.back();">Back</a>
                    <a href="javascript:void(0);" onclick="window.print();">Print</a>
                </div>
                <div>
                    <asp:Button ID="btnExportPdf" runat="server" Text="Export to PDF" 
                        OnClick="btnExportPdf_Click" CssClass="btn-export" />
                </div>
            </div>

            <%--<div class="summary-box" id="summaryBox" runat="server" visible="false">
                <asp:Label ID="lblSummary" runat="server"></asp:Label>
            </div>--%>

            <asp:PlaceHolder ID="phBills" runat="server"></asp:PlaceHolder>
        </div>

        <script type="text/javascript">
            document.getElementById('<%= btnExportPdf.ClientID %>').onclick = function (e) {
                e.preventDefault();

                const { jsPDF } = window.jspdf;
                var doc = new jsPDF("p", "pt", "a4");

                var panels = document.querySelectorAll(".bill-page");
                var exportBtn = document.getElementById('<%= btnExportPdf.ClientID %>');
                var loader = document.getElementById("loaderOverlay");

                exportBtn.style.display = "none";
                loader.style.display = "flex";

                let currentPanel = 0;

                function processNextPanel() {
                    if (currentPanel >= panels.length) {
                        doc.save("NCCF_Bills.pdf");
                        exportBtn.style.display = "inline-block";
                        loader.style.display = "none";
                        return;
                    }

                    var panel = panels[currentPanel];

                    html2canvas(panel, { scale: 1.5, useCORS: true }).then(canvas => {
                        var imgData = canvas.toDataURL("image/jpeg", 0.9);

                        var pageWidth = doc.internal.pageSize.getWidth();
                        var pageHeight = doc.internal.pageSize.getHeight();

                        var marginLeft = 30, marginTop = 30, marginRight = 30, marginBottom = 30;
                        var usableWidth = pageWidth - marginLeft - marginRight;
                        var usableHeight = pageHeight - marginTop - marginBottom;

                        var imgWidth = usableWidth;
                        var imgHeight = (canvas.height * imgWidth) / canvas.width;

                        if (imgHeight > usableHeight) {
                            imgHeight = usableHeight;
                            imgWidth = (canvas.width * imgHeight) / canvas.height;
                        }

                        if (currentPanel > 0) {
                            doc.addPage();
                        }

                        doc.setLineWidth(1);
                        doc.rect(marginLeft - 5, marginTop - 5, usableWidth + 10, usableHeight + 10);
                        doc.addImage(imgData, "JPEG", marginLeft, marginTop, imgWidth, imgHeight);

                        currentPanel++;
                        processNextPanel();
                    });
                }

                processNextPanel();
            };

            window.onload = function () {
                document.getElementById("loaderOverlay").style.display = "none";
            };
        </script>
    </form>
</body>
</html>