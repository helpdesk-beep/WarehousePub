<%@ Page Title="Godown Wise Moisture Report"
    Language="C#"
    MasterPageFile="~/MasterPage/CollectorMasterPage.master"
    AutoEventWireup="true"
    CodeFile="Mobile_App_Godown_Wise_Moisture_Report.aspx.cs"
    Inherits="District_Mobile_App_Godown_Wise_Moisture_Report" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"
        rel="stylesheet" />

    <style type="text/css">

        body {
            background: #f4f6f9;
        }

        .report-box {
            background: #fff;
            padding: 15px;
            border-radius: 6px;
            box-shadow: 0 2px 8px rgba(0,0,0,.15);
        }

        .Grid {
            width: 100%;
            border-collapse: collapse !important;
        }

        .Grid th {
            background: #1a5276 !important;
            color: #fff !important;
            border: 1px solid #000 !important;
            text-align: center;
            font-size: 11px;
            padding: 5px;
            vertical-align: middle;
        }

        .Grid td {
            border: 1px solid #ccc !important;
            text-align: center;
            font-size: 11px;
            padding: 4px;
        }

        .Grid tr:nth-child(even) {
            background: #f8f9fa;
        }

        .footer-style td {
            font-weight: bold !important;
            background: #e9ecef !important;
        }

        .btn-area {
            text-align: right;
            margin-bottom: 10px;
        }

        @media print {

            @page {
                size: A4 landscape;
                margin: 8mm;
            }

            .no-print {
                display: none !important;
            }

            body {
                zoom: 85%;
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
                window.open('', '', 'width=1400,height=900');

            printWindow.document.write('<html><head>');
            printWindow.document.write('<title>Godown Wise Moisture Report</title>');

            printWindow.document.write('<style>');
            printWindow.document.write('@page{size:A4 landscape;margin:8mm;}');
            printWindow.document.write('body{font-family:Arial;font-size:11px;}');
            printWindow.document.write('table{width:100%;border-collapse:collapse;}');
            printWindow.document.write('th{background:#1a5276;color:#fff;border:1px solid #000;padding:4px;}');
            printWindow.document.write('td{border:1px solid #000;padding:4px;text-align:center;}');
            printWindow.document.write('</style>');

            printWindow.document.write('</head><body>');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');

            printWindow.document.close();

            setTimeout(function () {
                printWindow.focus();
                printWindow.print();
            }, 500);
        }

    </script>

    <div class="container-fluid mt-2">

        <div class="btn-area no-print">


            <asp:Button ID="btnExport"
                runat="server"
                Text="Export To Excel"
                CssClass="btn btn-success btn-sm"
                OnClick="btnExport_Click" />

            <asp:Button ID="btnPrint"
                runat="server"
                Text="Print / Save PDF"
                CssClass="btn btn-danger btn-sm"
                OnClientClick="PrintGrid(); return false;" />

        </div>

        <div id="printArea" class="report-box">

            <table width="100%">
                <tr>

                    <td width="80">
                        <img src="../images/mpwlc.png"
                            style="width:70px;" />
                    </td>

                    <td align="center">

                        <h3 style="margin:0;color:#1a5276;">
                            M.P. Warehousing & Logistics Corporation
                        </h3>

                        <h5 style="margin-top:5px;">
                            Godown Wise Moisture Report
                        </h5>

                    </td>

                    <td width="250" align="right">

                        <b>Date :</b>
                        <asp:Label ID="lblDate" runat="server"></asp:Label>

                        <br />

                        <b>Total Records :</b>
                        <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>

                    </td>

                </tr>
            </table>

            <hr />

            <div class="table-responsive">

                <asp:GridView ID="gvReport"
                    runat="server"
                    AutoGenerateColumns="False"
                    CssClass="table table-bordered Grid"
                    ShowFooter="true"
                    EmptyDataText="No Record Found"
                    OnRowDataBound="gvReport_RowDataBound">

                    <Columns>

                    <asp:TemplateField HeaderText="क्रमांक">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Godown Name"
                        HeaderText="गोदाम का नाम" />

                    <asp:BoundField DataField="Total_Stack"
                        HeaderText="कुल स्टेको की संख्या" />

                    <asp:BoundField DataField="Prev Stack"
                        HeaderText="पूर्व दिनांक तक नमी अंकित किये गये स्टेको की संख्या" />

                    <asp:BoundField DataField="Today Stack"
                        HeaderText="आज दिनांक तक नमी अंकित किये गये स्टेको की संख्या" />

                    <asp:BoundField DataField="Total Moisture Stack"
                        HeaderText="कुल नमी अंकित स्टेको की संख्या" />

                    <asp:BoundField DataField="Pending Stack"
                        HeaderText="शेष स्टेको की संख्या" />

                    <asp:BoundField DataField="Prev Moisture Sent_DM"
                        HeaderText="पूर्व दिनांक तक MPSCSC को प्रेषित स्टेको की संख्या" />

                    <asp:BoundField DataField="Today Moisture Sent_DM"
                        HeaderText="आज दिनांक को MPSCSC को प्रेषित स्टेको की संख्या" />

                    <asp:BoundField DataField="Total Moisture Sent_DM"
                        HeaderText="कुल MPSCSC को प्रेषित स्टेको की संख्या" />

                    <asp:BoundField DataField="Prev Moisture Submit To FCI"
                        HeaderText="पूर्व दिनांक तक FCI को प्रेषित स्टेको की संख्या" />

                    <asp:BoundField DataField="Today Moisture Submit To FCI"
                        HeaderText="आज दिनांक को FCI को प्रेषित स्टेको की संख्या" />

                    <asp:BoundField DataField="Total Moisture Submit To FCI"
                        HeaderText="कुल FCI को प्रेषित स्टेको की संख्या" />

                    <asp:BoundField DataField="FCI Inspected Stack"
                        HeaderText="FCI द्वारा Cross Check किये गये स्टेको की संख्या" />

                    <asp:BoundField DataField="FCI Inspected Date"
                        HeaderText="FCI Inspection Date"
                        DataFormatString="{0:dd-MM-yyyy}" />

                </Columns>

                    <FooterStyle CssClass="footer-style" />

                </asp:GridView>

            </div>

        </div>

    </div>

</asp:Content>