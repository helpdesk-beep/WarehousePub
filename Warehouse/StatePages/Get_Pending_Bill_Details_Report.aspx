<%@ Page Title="MPWLC - Pending Bill Details Report" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Get_Pending_Bill_Details_Report.aspx.cs" Inherits="StatePages_Get_Pending_Bill_Details_Report" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!-- Select2 CSS -->
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />

    <style type="text/css">
        .card-box {
            background-color: #fff;
            padding: 15px;
            border-radius: 6px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            margin: 10px;
            position: relative;
        }
        .card-header-title {
            background-color: #0b2545;
            color: #ffffff;
            padding: 10px 15px;
            font-size: 16px;
            font-weight: bold;
            border-radius: 4px;
            margin-bottom: 15px;
        }
        .filter-panel {
            background-color: #f8f9fa;
            border: 1px solid #e9ecef;
            padding: 15px;
            border-radius: 4px;
            margin-bottom: 15px;
        }
        .filter-row {
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
            align-items: flex-end;
        }
        .form-group-box {
            display: flex;
            flex-direction: column;
            flex: 1;
            min-width: 220px;
        }
        .form-group-box label {
            font-size: 12px;
            font-weight: bold;
            margin-bottom: 5px;
            color: #333;
        }
        .action-panel {
            margin-bottom: 15px;
            display: flex;
            gap: 10px;
        }
        .btn-custom {
            padding: 7px 18px;
            font-size: 13px;
            font-weight: bold;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            display: inline-flex;
            align-items: center;
        }
        .btn-show { background-color: #0056b3; color: white; }
        .btn-show:hover { background-color: #004085; }
        .btn-excel { background-color: #1e7e34; color: white; }
        .btn-excel:hover { background-color: #155724; }
        .btn-print { background-color: #117a8b; color: white; }
        .btn-print:hover { background-color: #0c5460; }
        
        /* SELECT2 STYLING */
        .select2-container {
            width: 100% !important;
        }
        .select2-container--default .select2-selection--single {
            height: 38px !important;
            border: 1px solid #ced4da !important;
            border-radius: 4px !important;
            background-color: #ffffff !important;
        }
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            line-height: 36px !important;
            font-size: 13px !important;
            color: #212529 !important;
            padding-left: 10px !important;
        }
        .select2-container--default .select2-selection--single .select2-selection__arrow {
            height: 36px !important;
            right: 5px !important;
        }
        .select2-dropdown {
            border: 1px solid #0b2545 !important;
            z-index: 999999 !important;
            box-shadow: 0 4px 10px rgba(0,0,0,0.15) !important;
        }
        .select2-search--dropdown {
            padding: 6px !important;
        }
        .select2-search--dropdown .select2-search__field {
            height: 34px !important;
            padding: 4px 8px !important;
            width: 100% !important;
            border: 1px solid #0056b3 !important;
            border-radius: 4px !important;
            box-sizing: border-box !important;
            outline: none !important;
        }

        /* GRIDVIEW STYLING */
        .table-responsive {
            width: 100%;
            overflow-x: auto;
            border: 1px solid #dee2e6;
            border-radius: 4px;
        }
        .grid-style {
            width: 100%;
            border-collapse: collapse;
            font-family: 'Segoe UI', Arial, sans-serif;
            font-size: 12px;
            white-space: nowrap;
        }
        .grid-style th {
            background-color: #0b2545 !important;
            color: #ffffff !important;
            padding: 9px 8px !important;
            border: 1px solid #002c5c !important;
            text-align: center;
            font-weight: 600;
        }
        .grid-style td {
            padding: 6px 8px;
            border: 1px solid #dee2e6;
        }
        .grid-style tr:nth-child(even) { background-color: #f8f9fa; }
        .grid-style tr:hover { background-color: #e9ecef; }
        
        .grid-style tr.grid-footer-row td {
            background-color: #e2e8f0 !important;
            font-weight: bold !important;
            border-top: 2px solid #0b2545 !important;
            border-bottom: 1px solid #cbd5e1 !important;
            padding: 8px !important;
        }

        /* STYLISH LOADING OVERLAY CSS */
        .loading-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(11, 37, 69, 0.45);
            backdrop-filter: blur(2px);
            z-index: 9999999;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .loading-card {
            background: #ffffff;
            padding: 22px 35px;
            border-radius: 8px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.25);
            display: flex;
            align-items: center;
            gap: 15px;
            border-left: 5px solid #0b2545;
        }
        .spinner-ring {
            width: 32px;
            height: 32px;
            border: 4px solid #e2e8f0;
            border-top: 4px solid #0b2545;
            border-radius: 50%;
            animation: spin 0.8s linear infinite;
        }
        @keyframes spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }
        .loading-text {
            font-family: 'Segoe UI', Arial, sans-serif;
            font-size: 14px;
            font-weight: bold;
            color: #0b2545;
        }

        /* PRINT STYLING */
        @media print {
            @page { size: landscape; margin: 5mm; }
            * { -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
            body * { visibility: hidden; }
            #printArea, #printArea * { visibility: visible; }
            #printArea { position: absolute; left: 0; top: 0; width: 100% !important; margin: 0 !important; }
            .no-print { display: none !important; }
            .grid-style { font-size: 9px !important; width: 100% !important; table-layout: fixed; }
            .grid-style th { background-color: #0b2545 !important; color: #fff !important; }
            .grid-style th, .grid-style td { padding: 3px 1px !important; border: 0.5pt solid #000 !important; word-wrap: break-word; white-space: normal !important; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="card-box">
        <div class="card-header-title " align="center">
            MPWLC - Pending Bill Details Report
        </div>

        <asp:UpdatePanel ID="upFilters" runat="server">
            <ContentTemplate>
                <!-- Filter Dropdowns Panel -->
                <div class="filter-panel no-print">
                    <div class="filter-row">
                        <div class="form-group-box">
                            <label>Region:</label>
                            <asp:DropDownList ID="ddlRegion" runat="server" CssClass="make-searchable" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group-box">
                            <label>District:</label>
                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="make-searchable" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group-box">
                            <label>Branch / Depot:</label>
                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="make-searchable" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group-box">
                            <label>Godown:</label>
                            <asp:DropDownList ID="ddlGodown" runat="server" CssClass="make-searchable">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div style="margin-top: 15px; text-align: right;">
                        <asp:Button ID="btnShow" runat="server" Text="🔍 Show Details" CssClass="btn-custom btn-show" OnClick="btnShow_Click" />
                    </div>
                </div>

                <!-- Report Section -->
                <div id="printArea" runat="server" visible="false">
                    <div class="action-panel no-print">
                        <asp:Button ID="btnExportExcel" runat="server" Text="📥 Export to Excel" CssClass="btn-custom btn-excel" OnClick="btnExportExcel_Click" />
                        <asp:Button ID="btnPrint" runat="server" Text="🖨️ Print Report" CssClass="btn-custom btn-print" OnClientClick="return printReport();" />
                    </div>

                    <div style="text-align:center; margin-bottom:12px;">
                        <h3 style="margin: 0; font-family: Arial, sans-serif; color: #0b2545;">Madhya Pradesh Warehousing & Logistics Corporation (MPWLC)</h3>
                        <h4 style="margin: 5px 0; font-family: Arial, sans-serif;">Pending Bill Details Report</h4>
                        <p style="margin: 3px 0 10px 0; font-size:11px; color:#555;">Generated Date: <%= DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt") %></p>
                    </div>

                    <div class="table-responsive">
                        
                        <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" CssClass="grid-style"
                            ShowFooter="True" OnRowDataBound="gvDetails_RowDataBound"
                            EmptyDataText="No Pending Bill Records Available for Selected Criteria.">
                            <FooterStyle CssClass="grid-footer-row" />
                            <Columns>
                                <%-- Index 0 --%>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="35px" />
                                </asp:TemplateField>

                                <%-- Index 1 --%>
                                <asp:BoundField DataField="Regionnm" HeaderText="Region Name" />

                                <%-- Index 2 --%>
                                <asp:BoundField DataField="District_Name" HeaderText="District Name" />

                                <%-- Index 3 --%>
                                <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />

                                <%-- Index 5 --%>
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />

                                <%-- Index 6 --%>
                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name" />

                                <%-- Index 7 --%>
                                <asp:BoundField DataField="CropYear" HeaderText="Crop Year" ItemStyle-HorizontalAlign="Center" />

                                <%-- Index 8 --%>
                                <asp:BoundField DataField="Balance" HeaderText="Balance" DataFormatString="{0:N5}">
                                    <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                </asp:BoundField>

                                <%-- Index 9 --%>
                                <asp:BoundField DataField="Expected_Bills_Till_July" HeaderText="Expected Bills Till July">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <%-- Index 10 --%>
                                <asp:BoundField DataField="Actual_Bills_Generated" HeaderText="Actual Bills Generated">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <%-- Index 11 --%>
                                <asp:BoundField DataField="Pending_Bills" HeaderText="Pending Bills">
                                    <ItemStyle HorizontalAlign="Center" Font-Bold="true" ForeColor="Red" />
                                </asp:BoundField>

                                <%-- Index 12 --%>
                                <asp:BoundField DataField="Pending_Month_Names" HeaderText="Pending Month Names" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExportExcel" />
            </Triggers>
        </asp:UpdatePanel>

        <!-- LOADING PROGRESS SPINNER (TRIGGERED AUTOMATICALLY ON SHOW DETAILS / DROPDOWNS) -->
        <asp:UpdateProgress ID="upProgress" runat="server" AssociatedUpdatePanelID="upFilters">
            <ProgressTemplate>
                <div class="loading-overlay">
                    <div class="loading-card">
                        <div class="spinner-ring"></div>
                        <div class="loading-text">Fetching Report Data, Please Wait...</div>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

    </div>

    <!-- Select2 Scripts -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

    <script type="text/javascript">
        function applySelect2() {
            $('.make-searchable').each(function () {
                $(this).select2({
                    width: '100%',
                    placeholder: "-- Select --",
                    allowClear: false
                });
            });
        }

        $(document).on('select2:open', function () {
            setTimeout(function () {
                let searchBox = document.querySelector('.select2-container--open .select2-search__field');
                if (searchBox) {
                    searchBox.focus();
                }
            }, 30);
        });

        $(document).ready(function () {
            applySelect2();
        });

        if (typeof (Sys) !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function (sender, args) {
                applySelect2();
            });
        }

        // Dedicated Print Function
        function printReport() {
            var gridContent = document.getElementById('<%= printArea.ClientID %>').innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1100');

            printWindow.document.write('<html><head><title>Pending Bill Details Report</title>');
            printWindow.document.write('<style>');
            printWindow.document.write('@page { size: landscape; margin: 10mm; }');
            printWindow.document.write('body { font-family: Arial, sans-serif; font-size: 11px; padding: 10px; }');
            printWindow.document.write('.no-print { display: none !important; }');
            printWindow.document.write('table { width: 100%; border-collapse: collapse; margin-top: 10px; }');
            printWindow.document.write('th { background-color: #0b2545 !important; color: #ffffff !important; border: 1px solid #000; padding: 6px; font-size: 11px; text-align: center; -webkit-print-color-adjust: exact; print-color-adjust: exact; }');
            printWindow.document.write('td { border: 1px solid #000; padding: 5px; font-size: 10px; }');
            printWindow.document.write('tr.grid-footer-row td { background-color: #e2e8f0 !important; font-weight: bold; -webkit-print-color-adjust: exact; print-color-adjust: exact; }');
            printWindow.document.write('</style></head><body>');
            printWindow.document.write(gridContent);
            printWindow.document.write('</body></html>');

            printWindow.document.close();
            printWindow.focus();

            setTimeout(function () {
                printWindow.print();
                printWindow.close();
            }, 500);

            return false;
        }
    </script>
</asp:Content>