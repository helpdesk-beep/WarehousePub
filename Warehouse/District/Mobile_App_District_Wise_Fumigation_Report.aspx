<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CollectorMasterPage.master" AutoEventWireup="true" CodeFile="~/District/Mobile_App_District_Wise_Fumigation_Report.aspx.cs" Inherits="District_Mobile_App_District_Wise_Fumigation_Report" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"
        rel="stylesheet" />

    <style type="text/css">
        body {
            background-color: #f8f9fa;
        }

        .main-container {
            background: #fff;
            padding: 15px;
            border-radius: 8px;
            box-shadow: 0px 2px 10px rgba(0,0,0,.10);
        }

        .header-table {
            width: 100%;
            border-bottom: 2px solid #1a5276;
            margin-bottom: 10px;
        }

        .org-title {
            color: #1a5276;
            font-size: 22px;
            font-weight: bold;
            margin: 0;
        }

        .report-title {
            font-size: 15px;
            font-weight: 600;
        }

        .Grid {
            width: 100%;
            border-collapse: collapse !important;
        }

            .Grid th {
                background: #1a5276 !important;
                color: White !important;
                border: 1px solid #444 !important;
                text-align: center;
                padding: 8px;
                font-size: 12px;
            }

            .Grid td {
                border: 1px solid #ccc !important;
                padding: 6px;
                font-size: 12px;
            }

            .Grid tr:nth-child(even) {
                background-color: #f8f9fa;
            }

        .footer-style td {
            font-weight: bold !important;
            background: #f2f2f2 !important;
        }

        .no-print {
            display: flex;
            justify-content: flex-end;
            gap: 10px;
            margin-bottom: 10px;
        }

        @media print {

            .no-print {
                display: none !important;
            }

            .Grid thead {
                display: table-header-group;
            }

            .Grid tr {
                page-break-inside: avoid;
            }
        }
    </style>

    <style type="text/css">
        .Grid th {
            background: #1a5276 !important;
            color: #fff !important;
            text-align: center;
            font-size: 11px;
        }

        .Grid td {
            font-size: 11px;
            text-align: center;
        }

        .footer-style td {
            background: #f2f2f2 !important;
            font-weight: bold !important;
        }

        @media print {

            @page {
                size: A4 landscape;
                margin: 8mm;
            }

            body {
                zoom: 85%;
            }

            .no-print {
                display: none !important;
            }

            .Grid {
                width: 100%;
                border-collapse: collapse;
            }

                .Grid th {
                    background: #1a5276 !important;
                    color: #fff !important;
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

     var printContents =
         document.getElementById("printArea").cloneNode(true);

     var links = printContents.getElementsByTagName("a");

     while (links.length > 0) {

         var txt =
             document.createTextNode(links[0].innerText);

         links[0].parentNode.replaceChild(txt, links[0]);
     }

     var WinPrint =
         window.open('', '', 'width=1400,height=900');

     WinPrint.document.write('<html><head>');
     WinPrint.document.write('<title>District Wise Moisture Report</title>');

     WinPrint.document.write('<style>');
     WinPrint.document.write('@page{size:A4 landscape;margin:8mm;}');
     WinPrint.document.write('body{font-family:Arial;font-size:11px;}');
     WinPrint.document.write('table{width:100%;border-collapse:collapse;}');
     WinPrint.document.write('th{background:#1a5276;color:#fff;border:1px solid #000;padding:5px;}');
     WinPrint.document.write('td{border:1px solid #000;padding:5px;}');
     WinPrint.document.write('</style>');

     WinPrint.document.write('</head><body>');
     WinPrint.document.write(printContents.innerHTML);
     WinPrint.document.write('</body></html>');

     WinPrint.document.close();

     setTimeout(function () {
         WinPrint.focus();
         WinPrint.print();
     }, 500);
        }

    </script >

         <div class="container-fluid">

        <div class="no-print">

            <asp:Button ID="btnExport"
                runat="server"
                Text="Export To Excel"
                CssClass="btn btn-success btn-sm"
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

                    <td width="70">
                        <img src="../images/mpwlc.png"
                            style="width: 60px;" />
                    </td>

                    <td align="center">

                        <h2 class="org-title">M.P. Warehousing & Logistics Corporation
                        </h2>

                        <div class="report-title">
                            District Wise Moisture Report
                        </div>

                    </td>

                    <td width="220" align="right">

                        <strong>Date :</strong>
                        <asp:Label ID="lblDate" runat="server"></asp:Label>

                        <br />

                        <strong>Total Records :</strong>
                        <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>

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
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Branch Name">
                            <ItemTemplate>

                                <asp:HyperLink ID="lnkBranch"
                                    runat="server"
                                    Text='<%# Eval("DepotName") %>'
                                    NavigateUrl='<%# "Mobile_App_Godown_Summary_Fumigation_Report.aspx?BranchID=" + Eval("BranchId") %>'
                                    Target="_blank">
                                </asp:HyperLink>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Financial_year"
                            HeaderText="Financial Year" />

                        <asp:BoundField DataField="Total_Godowns_Covered"
                            HeaderText="Total Godowns Covered" />

                        <asp:BoundField DataField="Total_Stacks_Fumigated"
                            HeaderText="Total Stacks Fumigated" />

                        <asp:BoundField DataField="Completed_Fumigations"
                            HeaderText="Completed Fumigations" />

                        <asp:BoundField DataField="Total_Opened_Stacks"
                            HeaderText="Opened Stacks" />

                    </Columns>


                    <FooterStyle CssClass="footer-style" />
                </asp:GridView>

            </div>

        </div>

    </div>

</asp:Content>
