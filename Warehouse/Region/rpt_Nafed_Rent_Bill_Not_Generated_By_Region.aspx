<%@ Page Title="NAFED Rent Bill Not Generated Report - Region Wise" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="~/Region/rpt_Nafed_Rent_Bill_Not_Generated_By_Region.aspx.cs" Inherits="Region_rpt_Nafed_Rent_Bill_Not_Generated_By_Region" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script type="text/jscript" src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/jscript" src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <style type="text/css">
        .card-box {
            background-color: #fff;
            padding: 15px;
            border-radius: 6px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            margin: 10px;
        }

        .card-header-title {
            background-color: #004085;
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

        .form-group {
            display: flex;
            flex-direction: column;
            min-width: 200px;
            flex: 1;
        }

            .form-group label {
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

        .btn-show {
            background-color: #0056b3;
            color: white;
        }

            .btn-show:hover {
                background-color: #004085;
            }

        .btn-excel {
            background-color: #1e7e34;
            color: white;
        }

            .btn-excel:hover {
                background-color: #155724;
            }

        .btn-print {
            background-color: #117a8b;
            color: white;
        }

            .btn-print:hover {
                background-color: #0c5460;
            }

        /* Select2 Custom Fixes */
        .select2-container .select2-selection--single {
            height: 34px !important;
            border: 1px solid #ced4da !important;
        }

        .select2-container--default .select2-selection--single .select2-selection__rendered {
            line-height: 32px !important;
            font-size: 13px;
        }

        .select2-container--default .select2-selection--single .select2-selection__arrow {
            height: 32px !important;
        }

        /* Table Styling with Fixed Sticky Header */
        .table-responsive {
            width: 100%;
            max-height: 550px; /* Vertical Scrollbar ke liye height */
            overflow-y: auto;
            overflow-x: auto;
            border: 1px solid #dee2e6;
            border-radius: 4px;
            position: relative;
        }

        .grid-style {
            width: 100%;
            border-collapse: separate; /* Sticky header ke clean rendering ke liye */
            border-spacing: 0;
            font-family: 'Segoe UI', Arial, sans-serif;
            font-size: 12px;
            white-space: nowrap;
        }

            /* FIXED STICKY HEADER STYLING */
            .grid-style th {
                background-color: #004085 !important;
                color: #ffffff !important;
                padding: 9px 5px !important;
                border: 1px solid #002c5c !important;
                text-align: center;
                font-weight: 600;
                position: sticky !important;
                top: 0 !important;
                z-index: 10 !important;
                box-shadow: 0 2px 2px -1px rgba(0, 0, 0, 0.4);
            }

            .grid-style td {
                padding: 6px 6px;
                border: 1px solid #dee2e6;
            }

            .grid-style tr:nth-child(even) {
                background-color: #f8f9fa;
            }

            .grid-style tr:hover {
                background-color: #e9ecef;
            }

            .grid-style tr:last-child td {
                background-color: #eaeef3;
                border-top: 2px solid #004085;
            }

        /* AAPKA ORIGINAL PRINT CSS (BILKUL UNTOUCHED) */
        @media print {
            @page {
                size: landscape;
                margin: 5mm;
            }

            * {
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }

            body * {
                visibility: hidden;
            }

            #printArea, #printArea * {
                visibility: visible;
            }

            #printArea {
                position: absolute;
                left: 0;
                top: 0;
                width: 100% !important;
                margin: 0 !important;
            }

            .no-print {
                display: none !important;
            }

            .table-responsive {
                max-height: none !important; /* Print me full table aaye */
                overflow: visible !important;
            }

            .grid-style {
                font-size: 9px !important;
                width: 100% !important;
                table-layout: fixed;
            }

                .grid-style th {
                    position: static !important; /* Print me sticky band rahe */
                    background-color: #004085 !important;
                    color: #fff !important;
                    box-shadow: none !important;
                }

                .grid-style th, .grid-style td {
                    padding: 3px 1px !important;
                    border: 0.5pt solid #000 !important;
                    word-wrap: break-word;
                    white-space: normal !important;
                }
        }
    </style>
    <script type="text/javascript">
        function initSelect2() {
            $('.select2').select2({
                width: '100%',
                placeholder: "-- Select --",
                allowClear: true
            });
        }
        $(document).ready(function () {
            initSelect2();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                initSelect2();
            });
        }
        function printReport() {
            window.print();
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card-box">
        <div class="card-header-title">
            NAFED Rent Bill Not Generated Report - Region Wise
        </div>

        <!-- Filter Dropdowns Panel (No Region Dropdown) -->
        <div class="filter-panel no-print">
            <div class="filter-row">
                <div class="form-group">
                    <label>District:</label>
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Branch / Depot:</label>
                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="select2">
                    </asp:DropDownList>
                </div>
                <div class="form-group" style="flex: 0;">
                    <asp:Button ID="btnShow" runat="server" Text="🔍 Show Data" CssClass="btn-custom btn-show" OnClick="btnShow_Click" />
                </div>
            </div>
        </div>

        <!-- Action Buttons -->
        <div class="action-panel no-print">
            <asp:Button ID="btnExportExcel" runat="server" Text="📥 Export to Excel" CssClass="btn-custom btn-excel" OnClick="btnExportExcel_Click" />
            <asp:Button ID="btnPrint" runat="server" Text="🖨️ Print Report" CssClass="btn-custom btn-print" OnClientClick="return printReport();" />
        </div>

        <!-- Printable Report Area -->
        <div id="printArea">
            <div class="print-header" style="text-align: center; margin-bottom: 10px;">
                <h3 style="margin: 0; font-family: Arial, sans-serif;">NAFED Rent Bill Not Generated Report</h3>
                <p style="margin: 3px 0 10px 0; font-size: 11px; color: #555;">Generated On: <%= DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt") %></p>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" CssClass="grid-style"
                    ShowFooter="True" OnRowDataBound="gvReport_RowDataBound" EmptyDataText="No Record Found matching selected filters.">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="35px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Regionnm" HeaderText="Region" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="District_Name" HeaderText="District" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="DepotName" HeaderText="Depot/Branch" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Month_Name" HeaderText="Month">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="From_Date" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="To_Date" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Storage_Bill_No" HeaderText="Storage Bill No.">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Storage_Bill_Amount" HeaderText="Storage Amount" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Bill_Status" HeaderText="Bill Status">
                            <ItemStyle HorizontalAlign="Center" Font-Bold="true" ForeColor="Red" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>