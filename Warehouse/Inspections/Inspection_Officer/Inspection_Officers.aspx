<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Inspections/Inspection_Officer/Inspection_Officers.aspx.cs" Inherits="Inspection_Inspection_Officers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inspection Report | Special PV</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&display=swap" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf-autotable/3.5.29/jspdf.plugin.autotable.min.js"></script>

    <style>
        body {
            background: linear-gradient(135deg, #eef2f7, #f8f9fc);
            font-family: 'Inter', sans-serif;
            padding: 30px 15px;
        }

        .page-card {
            width: 95%;
            max-width: 1600px;
            margin: auto;
            background: #ffffff;
            border-radius: 16px;
            box-shadow: 0 20px 40px rgba(0,0,0,0.08);
            overflow: hidden;
        }

        .page-header {
            background: linear-gradient(135deg, #0d6efd, #6610f2);
            color: white;
            padding: 30px 20px;
            text-align: center;
        }

            .page-header h1 {
                font-size: 2.2rem;
                font-weight: 700;
                margin-bottom: 8px;
            }

            .page-header p {
                font-size: 1.1rem;
                opacity: 0.9;
                margin: 0;
            }

        .page-content {
            padding: 30px;
        }

        .table {
            border-radius: 12px;
            overflow: hidden;
        }

            .table thead {
                background: #0d6efd;
                color: #fff;
                font-size: 0.95rem;
            }

            .table th {
                font-weight: 600;
                border: none;
                text-align: center;
                white-space: nowrap;
            }

            .table td {
                vertical-align: middle;
                text-align: center;
            }

        .table-hover tbody tr:hover {
            background-color: #f3f6ff;
            transition: 0.2s ease-in-out;
        }

        .branch-link {
            color: #0d6efd;
            font-weight: 600;
            text-decoration: none;
        }

            .branch-link:hover {
                color: #6610f2;
                text-decoration: underline;
            }

        .pagination .page-link {
            border-radius: 8px;
            margin: 0 3px;
            color: #0d6efd;
        }

        .pagination .page-item.active .page-link {
            background: linear-gradient(135deg, #0d6efd, #6610f2);
            border: none;
        }

        .empty-box {
            background: #f8f9fa;
            border-radius: 12px;
            padding: 30px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="page-card">

            <div class="page-header">
                <h1>Inspection Report </h1>
                <p>Branch Wise</p>
            </div>

            <div class="page-content">
                <div class="row mb-3">
                    <div class="col-md-6">
                    </div>
                    <div class="col-md-6 text-end">
                        <asp:Button ID="btnExcel" runat="server"
                            Text="Export Excel"
                            CssClass="btn btn-success btn-sm me-2"
                            OnClientClick="return downloadExcel();" />

                        <asp:Button ID="btnPdf" runat="server"
                            Text="Export PDF"
                            CssClass="btn btn-danger btn-sm"
                            OnClientClick="return downloadPdf();" />
                    </div>
                </div>

                <div class="table-responsive">
                    <asp:GridView ID="gvOfficeList" runat="server"
                        AutoGenerateColumns="false"
                        CssClass="table table-striped table-hover align-middle"
                        AllowPaging="true"
                        PageSize="100"
                        GridLines="None"
                        OnPageIndexChanging="gvOfficeList_PageIndexChanging">

                        <PagerStyle CssClass="pagination justify-content-center mt-4" />

                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 + (gvOfficeList.PageIndex * gvOfficeList.PageSize) %>
                                </ItemTemplate>
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="Officer_Name" HeaderText="Officer Name" />
                            <asp:BoundField DataField="District_Name" HeaderText="Allotted District" />

                            <asp:BoundField DataField="District_Name" HeaderText="Allotted District" />

                            <asp:TemplateField HeaderText="Allotted Branch">
                                <ItemTemplate>

                                    <asp:Label ID="lblDepotName" runat="server"
                                        Text='<%# Eval("DepotName") %>' Visible="false"></asp:Label>

                                    <asp:Label ID="lblDistrict" runat="server"
                                        Text='<%# Eval("District_Name") %>' Visible="false"></asp:Label>

                                    <asp:LinkButton ID="lnkBranch" runat="server"
                                        Text='<%# Eval("DepotName") %>'
                                        CommandArgument='<%# Eval("BranchId") %>'
                                        OnClick="lnkBranch_Click"
                                        CssClass="branch-link">
                                    </asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Total_godown" HeaderText="NO Of Godowns" />
                            <asp:BoundField DataField="Online_Stack" HeaderText="NO Of Stack as Per Online" />
                            <asp:BoundField DataField="Insp_Stack" HeaderText="NO Of Stack as Per PV" />
                            <asp:BoundField DataField="TotalBags_AsPerPV" HeaderText="NO Of Bags as Per PV" />
                            <asp:BoundField DataField="SpillageBags_AsPerPV" HeaderText="NO Of Spillas Bags as Per PV" />
                        </Columns>

                        <EmptyDataTemplate>
                            <div class="empty-box text-center">
                                <h6 class="mb-1">No records available</h6>
                                <small class="text-muted">Inspection officer data not found.</small>
                            </div>
                        </EmptyDataTemplate>

                    </asp:GridView>
                </div>

            </div>
        </div>

    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function downloadPdf() {

            var table = document.getElementById('<%= gvOfficeList.ClientID %>');

            if (!table || table.rows.length <= 1) {
                alert('No data to export.');
                return false;
            }

            var jsPDF = window.jspdf.jsPDF;
            var doc = new jsPDF('l', 'pt', 'a4');

            // Title
            doc.setFontSize(16);
            doc.text('Inspection Report – Special PV', 420, 40, { align: 'center' });

            doc.setFontSize(12);
            doc.text('Inspection Officer List', 420, 60, { align: 'center' });

            doc.setFontSize(9);
            doc.text('Generated on: ' + new Date().toLocaleString(), 760, 80);

            doc.autoTable({
                html: table,
                startY: 100,
                theme: 'grid',
                styles: {
                    fontSize: 8,
                    halign: 'center'
                },
                headStyles: {
                    fillColor: [233, 236, 239]
                }
            });

            var fileName = 'Inspection_Officers_' +
                new Date().toISOString().replace(/[:T]/g, '').substring(0, 12) + '.pdf';

            doc.save(fileName);

            return false;
        }
    </script>

    <script type="text/javascript">
        function downloadExcel() {

            var table = document.getElementById('<%= gvOfficeList.ClientID %>');

            if (!table || table.rows.length <= 1) {
                alert('No records found to export.');
                return false;
            }

            var html = '';
            html += '<html><head><meta charset="utf-8">';
            html += '<style>';
            html += 'table{border-collapse:collapse;width:100%;}';
            html += 'th,td{border:1px solid #000;padding:6px;text-align:center;}';
            html += 'th{background-color:#d9edf7;font-weight:bold;}';
            html += '</style>';
            html += '</head><body>';

            html += '<h3 style="text-align:center;">Inspection Report – Special PV</h3>';
            html += '<h4 style="text-align:center;">Inspection Officer List</h4>';

            html += '<p style="text-align:right;font-size:12px;">';
            html += 'Generated on: ' + new Date().toLocaleString();
            html += '</p>';

            html += table.outerHTML;

            html += '</body></html>';

            var blob = new Blob([html], {
                type: 'application/vnd.ms-excel;charset=utf-8;'
            });

            var fileName = 'Inspection_Officers_' +
                new Date().getTime() + '.xls';

            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = fileName;

            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);

            return false;
        }
    </script>

</body>
</html>
