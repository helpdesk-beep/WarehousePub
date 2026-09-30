<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="~/Inspections/State/Godown_Stack_Report.aspx.cs" Inherits="Inspections_State_Godown_Stack_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .report-container {
            width: 100%;
            padding: 15px;
        }

        .report-title {
            font-size: 26px;
            font-weight: bold;
            color: #1d3557;
            margin-bottom: 20px;
        }

        .gridview {
            width: 100%;
            border-collapse: collapse;
        }

            .gridview th {
                background-color: #1d3557;
                color: white;
                padding: 10px;
                border: 1px solid #dcdcdc;
                text-align: center;
                font-size: 14px;
            }

            .gridview td {
                padding: 8px;
                border: 1px solid #dcdcdc;
                text-align: center;
                font-size: 13px;
            }

            .gridview tr:nth-child(even) {
                background-color: #f5f5f5;
            }

            .gridview tr:hover {
                background-color: #e8f4ff;
            }

        .left-align {
            text-align: left !important;
        }

        .footer-style {
            background-color: #1d3557;
            color: #000;
            font-weight: bold;
        }

        .btn-refresh {
            background-color: #1d3557;
            color: white;
            border: none;
            padding: 8px 18px;
            cursor: pointer;
            border-radius: 4px;
            margin-bottom: 15px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="report-container">

        <div class="report-title">
            Godown Stack Report
        </div>

        <asp:Button ID="btnRefresh" runat="server"
            Text="Refresh Report"
            CssClass="btn-refresh"
            OnClick="btnRefresh_Click" />


        <br />
        <br />
        <asp:Button ID="btnPrint" runat="server"
            Text="Print"
            CssClass="btn-refresh"
            OnClientClick="PrintGrid(); return false;" />

        <button type="button"
            class="btn-refresh"
            onclick="ExportToExcel()">
            Export Excel
        </button>

        <button type="button"
            class="btn-refresh"
            onclick="ExportToPDF()">
            Export PDF
        </button>

        <div id="ReportDiv">

            <asp:GridView ID="gvReport" runat="server"
                AutoGenerateColumns="false"
                CssClass="gridview"
                Width="100%"
                GridLines="Both"
                ShowFooter="true"
                EmptyDataText="No Record Found"
                OnRowDataBound="gvReport_RowDataBound">

                <Columns>

                    <asp:BoundField DataField="S.No" HeaderText="S.No" />

                    <asp:BoundField DataField="Godown Name"
                        HeaderText="Godown Name">
                        <ItemStyle CssClass="left-align" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Godown ID"
                        HeaderText="Godown ID" />

                    <asp:BoundField DataField="Prev Stack"
                        HeaderText="Prev Stack" />

                    <asp:BoundField DataField="Current Stack"
                        HeaderText="Current Stack" />

                    <asp:BoundField DataField="Total Stack"
                        HeaderText="Total Stack" />

                    <asp:BoundField DataField="Sent_DM"
                        HeaderText="Sent DM" />

                    <asp:BoundField DataField="Submit To FCI"
                        HeaderText="Submit To FCI" />

                    <asp:BoundField DataField="FCI Inspected Stack"
                        HeaderText="FCI Inspected Stack" />

                </Columns>

                <FooterStyle CssClass="footer-style" />

            </asp:GridView>

        </div>

    </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.18.5/xlsx.full.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf-autotable/3.5.31/jspdf.plugin.autotable.min.js"></script>

    <script type="text/javascript">

        // PRINT
        function PrintGrid() {

            var divContents = document.getElementById("ReportDiv").innerHTML;

            var a = window.open('', '', 'height=700, width=1000');

            a.document.write('<html>');
            a.document.write('<head>');
            a.document.write('<title>Godown Stack Report</title>');
            a.document.write('</head>');
            a.document.write('<body >');
            a.document.write(divContents);
            a.document.write('</body></html>');

            a.document.close();
            a.print();
        }

        // EXCEL EXPORT
        function ExportToExcel() {

            var table = document.getElementById("<%= gvReport.ClientID %>");

            var wb = XLSX.utils.table_to_book(table, {
                sheet: "Godown Stack Report"
            });

            XLSX.writeFile(wb, "Godown_Stack_Report.xlsx");
        }

        // PDF EXPORT
        async function ExportToPDF() {

            const { jsPDF } = window.jspdf;

            var doc = new jsPDF('l', 'pt', 'a4');

            doc.setFontSize(16);
            doc.setTextColor(0, 0, 0);

            doc.text("Godown Stack Report", 40, 30);

            doc.autoTable({

                html: '#<%= gvReport.ClientID %>',

                startY: 50,

                theme: 'grid',

                styles: {
                    fontSize: 8,
                    textColor: [0, 0, 0],
                    lineColor: [0, 0, 0],
                    lineWidth: 0.5
                },

                headStyles: {
                    fillColor: [29, 53, 87],
                    textColor: [255, 255, 255],
                    lineColor: [0, 0, 0],
                    lineWidth: 0.8,
                    fontStyle: 'bold'
                },

                bodyStyles: {
                    lineColor: [0, 0, 0],
                    lineWidth: 0.5
                },

                footStyles: {
                    fillColor: [220, 220, 220],
                    textColor: [0, 0, 0],
                    fontStyle: 'bold',
                    lineColor: [0, 0, 0],
                },

                alternateRowStyles: {
                    fillColor: [245, 245, 245]
                }

            });

            doc.save('Godown_Stack_Report.pdf');
        }

    </script>

</asp:Content>
