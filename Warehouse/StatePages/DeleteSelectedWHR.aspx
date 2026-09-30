<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/StatePages/DeleteSelectedWHR.aspx.cs" Inherits="DeleteSelectedWHR"%> %>



<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WHR Management</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/1.13.6/css/dataTables.bootstrap5.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.0/font/bootstrap-icons.css" />

    <style>
        body {
            background-color: #f4f7f6;
            font-family: 'Segoe UI', sans-serif;
        }

        .card {
            border-radius: 12px;
            border: none;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
        }

        .dataTables_wrapper .dataTables_filter input {
            width: 300px;
            margin-left: 0.5em;
            display: inline-block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container py-4">
            <div class="card p-4 mb-4">
                <h5 class="fw-bold mb-4"><i class="bi bi-search me-2"></i>Filter WHR Records</h5>
                <div class="row g-3">
                    <div class="col-md-4">
                        <label class="form-label fw-bold small">FINANCIAL YEAR</label>
                        <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-select">
                            <asp:ListItem Text="2025-26" Value="2025-26"></asp:ListItem>
                            <asp:ListItem Text="2026-27" Value="2026-27" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label fw-bold small">ORGANIZATION</label>
                        <asp:DropDownList ID="ddlOrganization" runat="server" CssClass="form-select">
                            <asp:ListItem Text="NCCF" Value="15478"></asp:ListItem>
                            <asp:ListItem Text="NAFED" Value="10535"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4 d-flex align-items-end">
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary w-100 fw-bold" OnClick="btnSearch_Click">
                            <i class="bi bi-search me-2"></i>Fetch Records
                        </asp:LinkButton>
                    </div>
                </div>
            </div>

            <div class="card p-4">
                <div class="d-flex justify-content-between align-items-center mb-3">
                    <h5 class="fw-bold mb-0">Record List</h5>
                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger fw-bold"
                        OnClick="btnDelete_Click" OnClientClick="return confirmDelete();">
                        <i class="bi bi-trash me-1"></i>Delete Selected
                    </asp:LinkButton>
                </div>

                <asp:Label ID="lblStatus" runat="server"></asp:Label>

                <div class="table-responsive">
                    <asp:GridView ID="gvWHR" runat="server" AutoGenerateColumns="False"
                        ClientIDMode="Static" DataKeyNames="Depositor_WHR_Id"
                        CssClass="table table-hover table-bordered w-100">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input type="checkbox" id="chkHeader" class="form-check-input" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRow" runat="server" CssClass="form-check-input" />
                                </ItemTemplate>
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="District_Name" HeaderText="District" />
                            <asp:BoundField DataField="DepotName" HeaderText="Depot" />
                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown" />
                            <asp:BoundField DataField="Depositor_WHR_Id" HeaderText="WHR ID" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="text-center p-3 text-muted">No records found. Click Search to fetch data.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/dataTables.bootstrap5.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            initializeTable();
        });

        function initializeTable() {
            var table = $('#gvWHR').DataTable({
                "destroy": true, // Critical for re-initialization after PostBack
                "paging": true,
                "ordering": true,
                "info": true,
                "searching": true, // Global Filter enabled
                "columnDefs": [
                    { "orderable": false, "targets": 0 } // Disable sort on checkbox column
                ],
                "language": {
                    "search": "", // Removes "Search:" text
                    "searchPlaceholder": "Filter results..."
                }
            });

            // "Select All" Logic
            $('#chkHeader').on('click', function () {
                var rows = table.rows({ 'search': 'applied' }).nodes();
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
            });

            // Styling adjustments for Bootstrap 5 integration
            $('.dataTables_filter input').addClass('form-control form-control-sm ms-2');
            $('.dataTables_length select').addClass('form-select form-select-sm d-inline-block w-auto ms-1 me-1');
        }

        function confirmDelete() {
            // Check visible checkboxes (excluding header)
            var count = $('#gvWHR tbody input[type="checkbox"]:checked').length;
            if (count === 0) {
                alert("Please select at least one record to delete.");
                return false;
            }
            return confirm("Are you sure you want to delete " + count + " selected records?");
        }
    </script>
</body>
</html>
