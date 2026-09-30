<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="~/StatePages/UpdateLicenceDetails.aspx.cs" Inherits="StatePages_UpdateLicenceDetails" Title="WDRA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=yes">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <style>
        /* ----- RESPONSIVE BASE STYLES ----- */
        * {
            box-sizing: border-box;
        }
        
        body {
            background-color: #f4f7f6;
            font-size: 14px;
        }
        
        /* Header Styling */
        .top-navbar {
            background-color: #2c4a8a;
            color: white;
            padding: 10px 20px;
            border-bottom: 4px solid #f39c12;
        }
        
        .logout-btn {
            background-color: #e74c3c;
            color: white;
            border-radius: 20px;
            padding: 5px 15px;
            text-decoration: none;
        }
        
        /* Search Box Card - FULLY RESPONSIVE */
        .search-container {
            background: white;
            border-radius: 10px;
            padding: 20px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.05);
            margin-top: -20px;
            border: 1px solid #eee;
        }
        
        .search-title {
            color: #333;
            font-weight: bold;
            margin-bottom: 20px;
            font-size: 1.1rem;
        }
        
        .btn-search-custom {
            background-color: #00b894;
            color: white;
            font-weight: bold;
            border: none;
            padding: 8px 30px;
            white-space: nowrap;
        }
        
        .btn-search-custom:hover {
            background-color: #019874;
        }
        
        /* Search Form Row - Responsive */
        .search-form-row {
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            align-items: center;
            gap: 12px;
        }
        
        .search-select {
            width: auto;
            min-width: 130px;
        }
        
        .search-input {
            width: 280px;
            min-width: 180px;
        }
        
        /* Table Styling - RESPONSIVE */
        .custom-table {
            margin-top: 30px;
            background: white;
            border-radius: 8px;
            overflow-x: auto;
            box-shadow: 0 4px 10px rgba(0,0,0,0.05);
        }
        
        .custom-table thead {
            background-color: #5dade2;
            color: white;
            vertical-align: middle;
        }
        
        .custom-table th {
            font-weight: 500;
            border: 1px solid #4895c2;
            text-align: center;
            padding: 12px 5px;
            font-size: 13px;
        }
        
        .custom-table td {
            text-align: center;
            vertical-align: middle;
            border: 1px solid #dee2e6;
            color: #555;
        }
        
        /* Make tables scroll horizontally on mobile */
        .table-responsive-wrapper {
            overflow-x: auto;
            -webkit-overflow-scrolling: touch;
        }
        
        .table-responsive-wrapper table {
            min-width: 600px;
        }
        
        /* Status Badges */
        .status-confirmed {
            border: 1px solid #2ecc71;
            color: #2ecc71;
            padding: 2px 8px;
            border-radius: 4px;
            font-weight: bold;
        }
        
        .status-pending {
            border: 1px solid #e74c3c;
            color: #e74c3c;
            padding: 2px 8px;
            border-radius: 4px;
            font-weight: bold;
        }
        
        /* Modal Styles */
        .modal-backdrop {
            z-index: 1040 !important;
        }
        
        .modal {
            z-index: 1050 !important;
        }
        
        /* Select2 fixes */
        .select2-container--open {
            z-index: 9999999 !important;
        }
        
        .select2-container .select2-selection--single {
            height: 38px !important;
            border: 1px solid #dee2e6 !important;
        }
        
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            line-height: 36px !important;
        }
        
        .select2-container--default .select2-selection--single .select2-selection__arrow {
            height: 36px !important;
        }
        
        /* ----- RESPONSIVE MEDIA QUERIES ----- */
        @media screen and (max-width: 992px) {
            .container {
                padding-left: 15px;
                padding-right: 15px;
            }
            
            .search-container {
                padding: 20px 15px;
            }
            
            .search-title {
                font-size: 1rem;
            }
        }
        
        @media screen and (max-width: 768px) {
            body {
                font-size: 13px;
            }
            
            .search-form-row {
                flex-direction: column;
                width: 100%;
            }
            
            .search-select {
                width: 100% !important;
                min-width: unset;
            }
            
            .search-input {
                width: 100% !important;
                min-width: unset;
            }
            
            .btn-search-custom {
                width: 100%;
                white-space: normal;
            }
            
            .search-title {
                font-size: 0.95rem;
                text-align: center;
            }
            
            /* Table font smaller on mobile */
            .custom-table th,
            .custom-table td {
                font-size: 12px;
                padding: 8px 4px;
            }
            
            .custom-table .btn-sm {
                padding: 4px 8px;
                font-size: 11px;
            }
            
            /* Modal adjustments */
            .modal-dialog {
                margin: 10px;
            }
            
            .modal-body {
                padding: 15px;
            }
        }
        
        @media screen and (max-width: 576px) {
            .container {
                padding-left: 10px;
                padding-right: 10px;
            }
            
            .search-container {
                padding: 15px 12px;
            }
            
            .search-title {
                font-size: 0.85rem;
                margin-bottom: 15px;
            }
            
            .custom-table th,
            .custom-table td {
                font-size: 11px;
                padding: 6px 3px;
            }
            
            .custom-table .btn-sm {
                padding: 3px 6px;
                font-size: 10px;
            }
            
            /* Modal form fields stack on mobile */
            .modal-body .row {
                display: flex;
                flex-direction: column;
            }
            
            .modal-body .col-md-6 {
                width: 100%;
                margin-bottom: 12px;
            }
            
            .modal-footer {
                flex-direction: column;
                gap: 8px;
            }
            
            .modal-footer .btn {
                width: 100%;
            }
            
            /* Header badges */
            .bg-success.text-white.p-2,
            .bg-primary.text-white.p-2 {
                flex-direction: column;
                gap: 8px;
                text-align: center;
            }
        }
        
        @media screen and (max-width: 400px) {
            .custom-table th,
            .custom-table td {
                font-size: 10px;
                padding: 5px 2px;
            }
            
            .custom-table .btn-sm i {
                margin-right: 2px;
            }
        }
        
        /* Print styles */
        @media print {
            .btn-search-custom,
            .logout-btn,
            .modal-footer .btn,
            .custom-table .btn-sm {
                display: none !important;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container mt-3 mt-md-5">
        <div class="row justify-content-center">
            <div class="col-12 col-md-10 col-lg-8">
                <div class="search-container">
                    <h5 class="search-title text-center">Track Registration/Offer Payment Status (Rabi 2026-27)</h5>
                    <div class="search-form-row">
                        <asp:DropDownList ID="ddllicenceType" runat="server" CssClass="form-select search-select">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="WDRA" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Non-WDRA" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtLicenceNo" runat="server" CssClass="form-control search-input" AutoComplete="Off" placeholder="Enter Licence No"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="SEARCH" CssClass="btn btn-search-custom" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>
        
        <!-- WDRA Panel -->
        <asp:Panel ID="pnlWDRA" runat="server" Visible="false">
            <div class="custom-table">
                <div class="bg-success text-white p-2 p-md-3 fw-bold d-flex flex-wrap justify-content-between align-items-center">
                    <span><i class="fa-solid fa-building-circle-check me-2"></i>WDRA Registration Details</span>
                    <span class="badge bg-light text-success mt-1 mt-md-0">Active Records</span>
                </div>
                <div class="table-responsive-wrapper">
                    <asp:GridView ID="gvWDRA" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-bordered table-hover mb-0"
                        EmptyDataText="No WDRA Record Found">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="License_Code" HeaderText="License Code" />
                            <asp:BoundField DataField="WarehouseName" HeaderText="Warehouse Name" />
                            <asp:BoundField DataField="District" HeaderText="District" />
                            <asp:BoundField DataField="WarehousemanName" HeaderText="Warehouseman Name" />
                            <asp:TemplateField HeaderText="Capacity (MT)">
                                <ItemTemplate>
                                    <asp:Label ID="lblCapacity" runat="server" Text='<%# Eval("Licensed_Capacity_MT") %>' CssClass="fw-bold text-primary"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Mobile_No" HeaderText="Mobile No" />
                            <asp:BoundField DataField="Issue_date" HeaderText="Issue Date" />
                            <asp:BoundField DataField="Valid_date" HeaderText="Valid Upto" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="btn btn-sm btn-outline-success"
                                        OnCommand="Edit_Command" CommandArgument='<%# Container.DataItemIndex %>'>
                                        <i class="fa fa-edit"></i> Update
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="p-3 text-danger fw-bold text-center">No WDRA Record Found for this License Number.</div>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="table-light text-center" />
                        <RowStyle CssClass="text-center" />
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>

        <!-- Non-WDRA Panel -->
        <asp:Panel ID="pnlNonWDRA" runat="server" Visible="false">
            <div class="custom-table">
                <div class="bg-primary text-white p-2 p-md-3 fw-bold d-flex flex-wrap justify-content-between align-items-center">
                    <span><i class="fa-solid fa-file-invoice me-2"></i>Non-WDRA License Details</span>
                    <span class="badge bg-light text-primary mt-1 mt-md-0">State Registration</span>
                </div>
                <div class="table-responsive-wrapper">
                    <asp:GridView ID="gvNonWDRA" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-bordered table-hover mb-0"
                        EmptyDataText="No Non-WDRA Record Found">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="License_Code" HeaderText="License Code" />
                            <asp:BoundField DataField="Application_Code" HeaderText="Application Code" />
                            <asp:BoundField DataField="Warehouse_Name" HeaderText="Warehouse Name" />
                            <asp:BoundField DataField="District" HeaderText="District" />
                            <asp:BoundField DataField="Licensed_Capacity_MT" HeaderText="Capacity (MT)" />
                            <asp:BoundField DataField="Warehouse_Man_Name" HeaderText="Warehouseman/Owner" />
                            <asp:BoundField DataField="Issuance_Date" HeaderText="Issuance Date" />
                            <asp:BoundField DataField="Valid_Till" HeaderText="Valid Till" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-sm btn-outline-primary"
                                        OnCommand="Edit_Command" CommandArgument='<%# Container.DataItemIndex %>'>
                                        <i class="fa fa-edit"></i> Update
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="p-3 text-danger fw-bold text-center">No NON-WDRA Record Found for this License Number.</div>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="table-light text-center" />
                        <RowStyle CssClass="text-center" />
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>

        <!-- Update Modal - Fully Responsive -->
        <div class="modal fade" id="updateModal" tabindex="-1" aria-labelledby="updateModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-scrollable modal-lg">
                <div class="modal-content">
                    <div class="modal-header bg-dark text-white">
                        <h5 class="modal-title" id="updateModalLabel"><i class="fa fa-edit me-2"></i>Update Licence Details</h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row g-3">
                            <asp:HiddenField ID="hfSelectedId" runat="server" />

                            <div class="col-12 col-md-6">
                                <label class="form-label fw-bold">License Code</label>
                                <asp:TextBox ID="popLicenseCode" runat="server" AutoComplete="Off" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-12 col-md-6">
                                <label class="form-label fw-bold">Warehouse Name</label>
                                <asp:TextBox ID="popWhName" runat="server" AutoComplete="Off" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-12 col-md-6">
                                <label class="form-label fw-bold">District</label>
                                <asp:DropDownList ID="popDistrict" runat="server" CssClass="form-select select2-dist">
                                    <asp:ListItem Text="Select District" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-12 col-md-6">
                                <label class="form-label fw-bold">Capacity (MT)</label>
                                <asp:TextBox ID="popCapacity" runat="server" AutoComplete="Off" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-12 col-md-6">
                                <label class="form-label fw-bold">Owner/Manager Name</label>
                                <asp:TextBox ID="popOwner" runat="server" AutoComplete="Off" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-12 col-md-6" id="divMobile" runat="server">
                                <label class="form-label fw-bold">Mobile No</label>
                                <asp:TextBox ID="popMobile" runat="server" AutoComplete="Off" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-12 col-md-6" id="divApplicationCode" runat="server">
                                <label class="form-label fw-bold">Application Code</label>
                                <asp:TextBox ID="popAppCode" runat="server" AutoComplete="Off" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-12 col-md-6">
                                <label class="form-label fw-bold">Issuance Date</label>
                                <asp:TextBox ID="popIssueDate" runat="server" AutoComplete="Off" CssClass="form-control" placeholder="dd/mm/yyyy"></asp:TextBox>
                            </div>
                            <div class="col-12 col-md-6">
                                <label class="form-label fw-bold">Valid Upto</label>
                                <asp:TextBox ID="popValidTill" runat="server" AutoComplete="Off" CssClass="form-control" placeholder="dd/mm/yyyy"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <asp:Button ID="btnFinalUpdate" runat="server" Text="Save Changes" CssClass="btn btn-success" OnClick="btnFinalUpdate_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function showModal() {
            var modalElement = document.getElementById('updateModal');
            var myModal = new bootstrap.Modal(modalElement);
            myModal.show();
            setTimeout(initSelect2, 300);
        }

        function initSelect2() {
            if (typeof $ !== 'undefined' && $.fn.select2) {
                $('.select2-dist').select2({
                    dropdownParent: $('#updateModal'),
                    width: '100%',
                    placeholder: 'Search District...',
                    allowClear: true
                });
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                initSelect2();
            });
        }

        $(document).ready(function () {
            initSelect2();
        });
    </script>
</asp:Content>