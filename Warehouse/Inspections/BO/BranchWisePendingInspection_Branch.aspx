<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchWisePendingInspection_Branch.aspx.cs" Inherits="Inspections_BO_BranchWisePendingInspection_Branch" %>

<!DOCTYPE html>
<html lang="hi">
<head runat="server">
    <title>Pending Inspections</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .main-card {
            border: none;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
            background: #ffffff;
        }

        .card-header-custom {
            background: linear-gradient(135deg, #1e3c72 0%, #2a5298 100%);
            color: white;
            border-top-left-radius: 10px;
            border-top-right-radius: 10px;
            padding: 1.5rem;
        }
        /* GridView Custom Styling */
        .table {
            margin-bottom: 0;
            vertical-align: middle;
        }

            .table th {
                background-color: #f1f4f9 !important;
                color: #333 !important;
                font-weight: 600;
                text-transform: uppercase;
                font-size: 0.85rem;
                letter-spacing: 0.5px;
                border-bottom: 2px solid #dee2e6 !important;
            }

        .table-striped tbody tr:nth-of-type(odd) {
            background-color: rgba(0, 0, 0, 0.01);
        }

        .table-hover tbody tr:hover {
            background-color: rgba(42, 82, 152, 0.05);
            transition: background-color 0.2s ease;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container my-5">

            <div class="card main-card">

                <div class="card-header-custom d-flex justify-content-between align-items-center">
                    <div>
                        <h4 class="mb-1"><i class="fa-solid fa-list-check me-2"></i>Pending Inspections</h4>
                        <p class="mb-0 text-white-50 small">List of all inspections awaiting verification</p>
                    </div>
                    <div>
                        <span class="badge bg-warning text-dark fs-6 px-3 py-2">
                            <i class="fa-solid fa-clock me-1"></i>Action Required
                        </span>
                    </div>
                </div>

                <div class="card-body bg-light border-bottom border-light-subtle p-3">
                    <div class="row g-3 align-items-center">
                        <div class="col-md-4">
                            <div class="input-group input-group-sm">
                                <span class="input-group-text bg-white"><i class="fa fa-search text-muted"></i></span>
                                <input type="text" class="form-control" placeholder="Search by Officer or Branch..." />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <select class="form-select form-select-sm">
                                <option selected>All Inspection Types</option>
                            </select>
                        </div>
                        <div class="col-md-5 text-md-end">
                            <button type="button" class="btn btn-sm btn-outline-secondary me-2">
                                <i class="fa-solid fa-file-excel me-1"></i>Export
                           
                            </button>
                            <button type="button" class="btn btn-sm btn-primary">
                                <i class="fa-solid fa-rotate me-1"></i>Refresh
                           
                            </button>
                        </div>
                    </div>
                </div>

                <div class="card-body p-0">
                    <div class="table-responsive">

                        <asp:GridView ID="gvPendingInspection"
                            runat="server"
                            AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped table-hover align-middle">

                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Officer_Name" HeaderText="Inspection Officer" />
                                <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />
                                <asp:BoundField DataField="Inspection_Status" HeaderText="Inspection Type" />
                                <asp:BoundField DataField="VerificationType" HeaderText="Verification Type" />
                                <asp:BoundField DataField="Order_No" HeaderText="Order No" />
                                <asp:BoundField DataField="Order_Date" HeaderText="Order Date" DataFormatString="{0:dd/MM/yyyy}" />
                            </Columns>

                        </asp:GridView>

                    </div>
                </div>

                <div class="card-footer bg-white text-muted small py-3">
                    <i class="fa-solid fa-info-circle me-1"></i>Showing real-time data from the central repository.
               
                </div>

            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
