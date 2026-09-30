<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Godown_Mapping_With_WeightBridge.aspx.cs" Inherits="Reports_States_Godown_Mapping_With_WeightBridge" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Godown Mapping With Weight Bridge</title>

    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <!-- Bootstrap CSS & JS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>

    <!-- DataTables CSS/JS -->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/dataTables.bootstrap4.min.css" />
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/dataTables.bootstrap4.min.js"></script>

    <!-- Buttons Extension -->
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/2.4.2/css/buttons.bootstrap4.min.css" />
    <script src="https://cdn.datatables.net/buttons/2.4.2/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.4.2/js/buttons.bootstrap4.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.1/jszip.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.2.7/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.2.7/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.4.2/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.4.2/js/buttons.print.min.js"></script>

    <script>
        $(document).ready(function () {
            $('#<%= GridView1.ClientID %>').DataTable({
                destroy: true,
                paging: true,
                searching: true,
                ordering: true,
                info: true,
                responsive: true,
                pageLength: 10,
                lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
                dom: "<'row mb-2'<'col-sm-4 col-md-4'l><'col-sm-4 col-md-4'B><'col-sm-4 col-md-4'f>>" +
                    "<'row'<'col-sm-12'tr>>" +
                    "<'row mt-2'<'col-sm-12 col-md-6'i><'col-sm-12 col-md-6'p>>",
                buttons: [
                    { extend: 'excelHtml5', title: 'Godown Mapping' },
                    {
                        extend: 'pdfHtml5',
                        title: 'Godown Mapping',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        exportOptions: { columns: ':visible' },
                        customize: function (doc) {
                            doc.styles.tableHeader.fillColor = '#0d6efd';
                            doc.styles.tableHeader.color = '#fff';
                            doc.styles.tableHeader.alignment = 'center';
                            doc.content[1].table.widths = Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                            // Add borders
                            doc.content[1].layout = {
                                hLineWidth: function (i, node) { return 0.5; },
                                vLineWidth: function (i, node) { return 0.5; },
                                hLineColor: function (i, node) { return '#000'; },
                                vLineColor: function (i, node) { return '#000'; }
                            };
                        }
                    },
                    {
                        extend: 'print',
                        title: 'Godown Mapping With WeightBridge',
                        exportOptions: { columns: ':visible' },
                        customize: function (win) {
                            $(win.document.body).find('table').addClass('table-bordered').css('font-size', '14px');
                            $(win.document.body).find('thead').css('background-color', '#0d6efd').css('color', '#fff');
                        }
                    }
                ]
            });
        });
    </script>

    <style>
        body {
            background-color: #f4f6f9;
            font-family: 'Segoe UI', Tahoma, sans-serif;
        }

        .page-header {
            background: linear-gradient(90deg, #0d6efd, #084298);
            color: #fff;
            padding: 18px;
            border-radius: 6px;
            text-align: center;
            font-size: 22px;
            font-weight: 600;
            margin-bottom: 20px;
        }

        .custom-card {
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.08);
            margin-bottom: 20px;
        }

            .custom-card .card-body {
                padding: 20px;
            }

        .filter-label {
            font-weight: 600;
            color: #333;
            margin-bottom: 6px;
        }

        .btn-search {
            background: linear-gradient(90deg, #198754, #146c43);
            border: none;
            color: #fff;
            font-weight: 600;
            height: 38px;
        }

            .btn-search:hover {
                background: linear-gradient(90deg, #146c43, #0f5132);
            }

        .table {
            font-size: 14px;
        }

            .table thead th {
                background-color: #0d6efd !important;
                color: #fff;
                text-align: center;
                vertical-align: middle;
            }

            .table tbody td {
                vertical-align: middle;
                text-align: center;
            }

        .table-striped tbody tr:nth-of-type(odd) {
            background-color: #f9fbff;
        }

        .table-hover tbody tr:hover {
            background-color: #eef4ff;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container-fluid mt-3">

            <div class="page-header">
                Godown Mapping With Weight Bridge
        <div style="font-size: 14px; font-weight: 400;">Region & District Wise Status</div>
            </div>

            <!-- FILTER PANEL -->
            <div class="custom-card">
                <div class="card-body">
                    <div class="row g-3 align-items-end">
                        <div class="col-md-2"></div>
                        <div class="col-md-3">
                            <div class="filter-label">Region</div>
                            <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-md-3">
                            <div class="filter-label">District</div>
                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="col-md-1">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-search w-100" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- GRID PANEL -->
            <div class="custom-card">
                <div class="card-body">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped table-hover"
                        AutoGenerateColumns="False" OnPreRender="GridView1_PreRender" OnRowDataBound="GridView1_RowDataBound"
                        GridLines="None">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Regionnm" HeaderText="Region" />
                            <asp:BoundField DataField="District_Name" HeaderText="District" />
                            <asp:BoundField DataField="DepotName" HeaderText="Depot" />
                            <asp:BoundField DataField="Total Godown" HeaderText="Total Godown" />
                            <asp:BoundField DataField="Entry By BM" HeaderText="Entry By BM" />
                            <asp:BoundField DataField="Pending" HeaderText="Pending" />
                        </Columns>
                    </asp:GridView>


                </div>
            </div>

        </div>
    </form>
</body>
</html>
