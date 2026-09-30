<%@ Page Title="Printed Bills History - NCCF" Language="C#" MasterPageFile="~/MasterPages/Nccf_Master.master" AutoEventWireup="true" CodeFile="~/NCCF/NCCF_Printed_Bills_History.aspx.cs" Inherits="NCCF_NCCF_Printed_Bills_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- Bootstrap 5 CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome for icons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" />
    <!-- Bootstrap Datepicker CSS -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" />

    <style>
        /* Page Container */
        .history-container {
            background: #f8f9fa;
            padding: 25px;
            border-radius: 15px;
            box-shadow: 0 0 20px rgba(0,0,0,0.05);
            margin: 20px;
        }

        /* Page Title */
        .page-title {
            color: #2c3e50;
            font-size: 24px;
            font-weight: 600;
            margin-bottom: 25px;
            padding-bottom: 10px;
            border-bottom: 3px solid #507CD1;
            display: flex;
            align-items: center;
            gap: 10px;
        }

            .page-title i {
                color: #507CD1;
                font-size: 28px;
            }

        /* Search Box Styling */
        .search-container {
            background: white;
            padding: 25px;
            border-radius: 12px;
            box-shadow: 0 5px 15px rgba(0,0,0,0.08);
            margin-bottom: 25px;
            border: 1px solid #e9ecef;
        }

        .search-title {
            color: #495057;
            font-size: 18px;
            font-weight: 600;
            margin-bottom: 20px;
            display: flex;
            align-items: center;
            gap: 8px;
        }

            .search-title i {
                color: #507CD1;
            }

        .search-table {
            width: 100%;
            border-collapse: separate;
            border-spacing: 0 10px;
        }

            .search-table td {
                padding: 8px 15px;
            }

        .search-label {
            font-weight: 600;
            color: #495057;
            white-space: nowrap;
        }

        .form-control-custom {
            width: 100%;
            padding: 10px 15px;
            border: 2px solid #e0e4e8;
            border-radius: 8px;
            font-size: 14px;
            transition: all 0.3s ease;
        }

            .form-control-custom:focus {
                border-color: #507CD1;
                outline: none;
                box-shadow: 0 0 0 3px rgba(80, 124, 209, 0.1);
            }

        .btn-search {
            background: linear-gradient(135deg, #507CD1 0%, #3459a8 100%);
            color: white;
            border: none;
            padding: 10px 30px;
            border-radius: 8px;
            font-weight: 600;
            font-size: 14px;
            cursor: pointer;
            transition: all 0.3s ease;
            display: inline-flex;
            align-items: center;
            gap: 8px;
        }

            .btn-search:hover {
                transform: translateY(-2px);
                box-shadow: 0 5px 15px rgba(80, 124, 209, 0.3);
            }

            .btn-search i {
                font-size: 16px;
            }

        .btn-reset {
            background: #6c757d;
            color: white;
            border: none;
            padding: 10px 30px;
            border-radius: 8px;
            font-weight: 600;
            font-size: 14px;
            cursor: pointer;
            transition: all 0.3s ease;
            display: inline-flex;
            align-items: center;
            gap: 8px;
            margin-left: 10px;
        }

            .btn-reset:hover {
                background: #5a6268;
                transform: translateY(-2px);
            }

        /* Grid Container */
        .grid-container {
            background: white;
            padding: 20px;
            border-radius: 12px;
            box-shadow: 0 5px 15px rgba(0,0,0,0.08);
            border: 1px solid #e9ecef;
        }

        /* GridView Styling */
        .grid-view {
            width: 100%;
            border-collapse: collapse;
            font-size: 14px;
            table-layout: fixed;
        }

            .grid-view th {
                background: linear-gradient(135deg, #507CD1 0%, #3459a8 100%);
                color: white;
                font-weight: 600;
                padding: 12px 8px;
                text-align: center;
                border: 1px solid #3d67b1;
                white-space: nowrap;
            }

            .grid-view td {
                padding: 10px 8px;
                text-align: left;
                border: 1px solid #e9ecef;
                color: #495057;
                word-wrap: break-word;
            }

                .grid-view td:nth-child(6) { /* Bill Number column */
                    font-weight: 600;
                    color: #507CD1;
                }

            .grid-view tr:nth-child(even) {
                background-color: #f8f9fa;
            }

            .grid-view tr:hover {
                background-color: #e8f0fe;
                transition: 0.3s;
            }

        /* Delete Button Styling */
        .btn-delete {
            background-color: #dc3545;
            color: white;
            border: none;
            padding: 5px 12px;
            border-radius: 6px;
            font-size: 12px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            display: inline-flex;
            align-items: center;
            gap: 5px;
            text-decoration: none;
        }

            .btn-delete:hover {
                background-color: #c82333;
                transform: scale(1.05);
                box-shadow: 0 2px 8px rgba(220, 53, 69, 0.3);
                color: white;
                text-decoration: none;
            }

            .btn-delete i {
                font-size: 12px;
            }

        /* Summary Cards */
        .summary-cards {
            display: flex;
            gap: 20px;
            margin-bottom: 25px;
        }

        .summary-card {
            flex: 1;
            background: white;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 3px 10px rgba(0,0,0,0.05);
            border-left: 4px solid #507CD1;
            display: flex;
            align-items: center;
            gap: 15px;
        }

        .summary-icon {
            width: 50px;
            height: 50px;
            background: rgba(80, 124, 209, 0.1);
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            color: #507CD1;
            font-size: 24px;
        }

        .summary-content h3 {
            margin: 0;
            font-size: 14px;
            color: #6c757d;
            font-weight: 500;
        }

        .summary-content p {
            margin: 5px 0 0;
            font-size: 24px;
            font-weight: 700;
            color: #2c3e50;
        }

        /* Export Buttons */
        .export-buttons {
            display: flex;
            gap: 10px;
            margin-bottom: 15px;
        }

        .btn-export {
            background: #28a745;
            color: white;
            border: none;
            padding: 8px 20px;
            border-radius: 6px;
            font-size: 13px;
            font-weight: 600;
            cursor: pointer;
            transition: 0.3s;
            display: inline-flex;
            align-items: center;
            gap: 5px;
        }

            .btn-export:hover {
                background: #218838;
            }

        .btn-excel {
            background: #1d72b8;
        }

            .btn-excel:hover {
                background: #155a8a;
            }

        /* Pagination Styling */
        .pagination-container {
            margin-top: 20px;
            text-align: center;
        }

        .grid-view .pager {
            background: #f8f9fa;
        }

            .grid-view .pager table {
                margin: 0 auto;
            }

            .grid-view .pager td {
                border: none;
                padding: 5px;
            }

            .grid-view .pager a {
                padding: 5px 10px;
                background: white;
                border: 1px solid #dee2e6;
                color: #507CD1;
                text-decoration: none;
                border-radius: 4px;
                margin: 0 2px;
            }

            .grid-view .pager span {
                padding: 5px 10px;
                background: #507CD1;
                color: white;
                border: 1px solid #507CD1;
                border-radius: 4px;
                margin: 0 2px;
            }

        /* No Records Found */
        .no-records {
            text-align: center;
            padding: 50px;
            color: #6c757d;
            font-size: 16px;
        }

            .no-records i {
                font-size: 48px;
                margin-bottom: 10px;
                color: #dee2e6;
            }

        /* Column Widths */
        .col-sno {
            width: 5%;
        }

        .col-godown-id {
            width: 10%;
        }

        .col-godown-name {
            width: 20%;
        }

        .col-branch {
            width: 15%;
        }

        .col-commodity {
            width: 15%;
        }

        .col-bill-no {
            width: 20%;
        }

        .col-action {
            width: 10%;
        }

        /* Responsive */
        @media (max-width: 768px) {
            .summary-cards {
                flex-direction: column;
            }

            .search-table td {
                display: block;
                width: 100%;
            }

            .grid-view {
                font-size: 12px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="history-container">
        <!-- Page Title -->
        <div class="page-title">
            <i class="fas fa-print"></i>
            Printed Bills History - NCCF
        </div>


        <!-- Search Box -->
        <div class="search-container">
            <div class="search-title">
                <i class="fas fa-search"></i>
                Search Filters
            </div>
            <table class="search-table">
                <tr>
                    <td class="search-label"><i class="far fa-calendar-alt me-2"></i>From Date:</td>
                    <td>
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control-custom datepicker"
                            placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                    </td>
                    <td class="search-label"><i class="far fa-calendar-alt me-2"></i>To Date:</td>
                    <td>
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control-custom datepicker"
                            placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="search-label"><i class="fas fa-hashtag me-2"></i>Bill Number:</td>
                    <td>
                        <asp:TextBox ID="txtBillNo" runat="server" CssClass="form-control-custom"
                            placeholder="Enter Bill Number"></asp:TextBox>
                    </td>
                    <td class="search-label"><i class="fas fa-building me-2"></i>District:</td>
                    <td>
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control-custom">
                            <asp:ListItem Value="">All Districts</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="search-label"><i class="fas fa-hashtag me-2"></i>Commodity:</td>
                    <td>
                        <asp:DropDownList ID="ddlCommodity" runat="server" CssClass="form-control-custom">
                            <asp:ListItem Value="">All Commodity</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="search-label"><i class="fas fa-building me-2"></i>Crop Year:</td>
                    <td>
                        <asp:DropDownList ID="ddlCropYear" runat="server" CssClass="form-control-custom">
                            <asp:ListItem Value="">All Crop Year</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center;">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                            CssClass="btn-search" UseSubmitBehavior="false" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click"
                            CssClass="btn-reset" UseSubmitBehavior="false" />
                    </td>
                </tr>
            </table>
        </div>

        <!-- Export Buttons -->
        <div class="export-buttons">
            <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel"
                OnClick="btnExportExcel_Click" CssClass="btn-export btn-excel" />
        </div>

        <!-- Grid Container with Specific Columns and Delete Button -->
        <div class="grid-container">
            <asp:GridView ID="gvPrintedBills" runat="server"
                AutoGenerateColumns="false"
                CssClass="grid-view"
                AllowPaging="true"
                PageSize="100"
                DataKeyNames="PrintLogID,Bill_Number"
                OnPageIndexChanging="gvPrintedBills_PageIndexChanging"
                OnRowDataBound="gvPrintedBills_RowDataBound"
                OnRowCommand="gvPrintedBills_RowCommand"
                EmptyDataText="No records found."
                EmptyDataRowStyle-CssClass="no-records">

                <Columns>
                    <asp:TemplateField HeaderText="">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="toggleAll(this)" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                            <asp:HiddenField ID="hfBillNo" runat="server" Value='<%# Eval("Bill_Number") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" CssClass="col-sno" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="Godown_Id" HeaderText="Godown ID"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="col-godown-id" />

                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name"
                        ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="col-godown-name" />

                    <asp:BoundField DataField="Branch_Name" HeaderText="Branch Name"
                        ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="col-branch" />

                    <asp:BoundField DataField="Commodity" HeaderText="Commodity"
                        ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="col-commodity" />

                    <asp:BoundField DataField="Bill_Number" HeaderText="Bill Number"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="col-bill-no"
                        ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#507CD1" />

                    <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="col-action">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server"
                                CommandName="DeleteRecord"
                                CommandArgument='<%# Eval("Bill_Number") %>'
                                CssClass="btn-delete"
                                ToolTip="Delete this record">
                                <i class="fas fa-trash-alt"></i> Delete
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <EmptyDataTemplate>
                    <div class="no-records">
                        <i class="fas fa-file-excel"></i>
                        <h4>No Printed Bills Found</h4>
                        <p>Try adjusting your search filters</p>
                    </div>
                </EmptyDataTemplate>

                <PagerSettings Mode="NumericFirstLast"
                    FirstPageText="First"
                    LastPageText="Last"
                    PageButtonCount="5" />
                <PagerStyle CssClass="pager" HorizontalAlign="Center" />
            </asp:GridView>
            <asp:Button ID="btnDeleteSelected" runat="server"
                Text="Delete Selected"
                CssClass="btn-delete"
                OnClick="btnDeleteSelected_Click" />
        </div>
    </div>

    <!-- Only Datepicker Script (No Delete Confirmation Script) -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            // Initialize Datepickers
            $('.datepicker').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true,
                orientation: 'bottom'
            });

            $('.datepicker').on('change', function () {
                var datePattern = /^(\d{2})\/(\d{2})\/(\d{4})$/;
                if (!datePattern.test($(this).val())) {
                    $(this).val('');
                }
            });
        });
    </script>
    <script type="text/javascript">
        function toggleAll(source) {
            var checkboxes = document.querySelectorAll('[id*="chkSelect"]');
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = source.checked;
            }
        }
</script>
</asp:Content>
