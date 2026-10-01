<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/Print_NCCF_Storage_Bill.aspx.cs" Inherits="BranchPages_Print_NCCF_Storage_Bill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- ------------------- CSS ------------------- -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />

    <style type="text/css">
        body {
            background: #eef3fb;
            font-family: 'Segoe UI', sans-serif;
        }

        fieldset {
            border: 2px solid #0078d7 !important;
            background: #ffffff;
            border-radius: 10px;
            padding: 20px;
            margin-top: 12px;
            box-shadow: 0 3px 10px rgba(0,0,0,0.12);
        }

        legend {
            font-size: 20px;
            width: auto !important;
            padding: 6px 14px;
            border-radius: 8px;
            background: #0078d7;
            color: white;
            border: none;
        }

        label {
            font-weight: 600;
        }

        .btn-theme {
            background: #0078d7;
            color: white;
            font-weight: bold;
            border-radius: 6px;
            padding: 6px 18px;
            transition: 0.3s;
        }

            .btn-theme:hover {
                background: #005fa3;
                color: white;
            }

        .button2 {
            background: #ffffff;
            border: 2px solid #0078d7;
            color: #0078d7;
            font-weight: bold;
            padding: 4px 14px;
            border-radius: 5px;
            transition: .3s;
        }

            .button2:hover {
                background: #0078d7;
                color: white;
            }

        .table {
            background: white;
            border-radius: 10px !important;
            overflow: hidden;
        }

            .table thead th {
                background: #0078d7 !important;
                color: white !important;
                text-align: center;
                font-size: 14px;
            }

            .table tbody td {
                background: #ffffff !important;
                color: #000 !important;
            }

        .BTNBLUE {
            color: #0078d7;
            font-weight: bold;
        }

            .BTNBLUE:hover {
                color: #005fa3;
                text-decoration: underline;
            }

        /* Select2 Styling */
        .select2-container .select2-selection--single {
            height: 36px !important;
            padding: 4px 8px;
            border: 1px solid #0D6EFD !important;
            border-radius: 8px !important;
        }

        .select2-container--default .select2-selection--single .select2-selection__arrow {
            height: 34px !important;
            right: 8px;
        }

        .select2-dropdown {
            border: 1px solid #0D6EFD !important;
        }

        .select2-container--default .select2-results__option--highlighted {
            background-color: #0D6EFD !important;
            color: white !important;
        }

        .select2-results__option {
            padding: 8px 10px;
        }
    </style>

    <!-- ------------------- JS ------------------- -->
    <%-- <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.full.min.js"></script>
    <script type="text/javascript">
        function MakeGodownSearchable() {
            var ddl = $('#<%= ddlGodown.ClientID %>');

            if (ddl.hasClass("select2-hidden-accessible")) {
                ddl.select2('destroy');
            }

            ddl.select2({
                placeholder: "Search Godown...",
                allowClear: true,
                width: '100%'
            });

            // Auto focus search box when dropdown opens
            ddl.on('select2:open', function () {
                $('.select2-container--open .select2-search__field').focus();
            });
        }

        $(document).ready(function () {
            MakeGodownSearchable();
        });

        // UpdatePanel / AJAX support
        if (typeof (Sys) !== 'undefined') {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                MakeGodownSearchable();
            });
        }


    </script>--%>
    <%--Updated By Ashutosh--%>
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlGodown]").select2();
        });
    </script>
    <%--Updated By Ashutosh End--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- ========================= SEARCH SECTION =========================== -->
    <fieldset>
        <legend>Print NCCF Storage Bills</legend>

        <div class="row mt-3">

            <div class="col-md-2">
                <label>Commodity</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" ToolTip="Select Commodity"
                    ErrorMessage="Select Commodity" InitialValue="0" ForeColor="Red"
                    Text="<i class='fa fa-exclamation-circle'></i>"
                    ControlToValidate="ddlcommodity" Display="Dynamic" runat="server" />
                <asp:DropDownList CssClass="form-control" ID="ddlcommodity" runat="server"></asp:DropDownList>
            </div>

            <div class="col-md-2">
                <label>Crop Year</label>
                <asp:DropDownList CssClass="form-control" ID="ddlcropyear" runat="server"></asp:DropDownList>
            </div>

            <div class="col-md-2">
                <label>Godown Name</label>
                <asp:DropDownList CssClass="form-control" ID="ddlGodown" runat="server" AutoPostBack="true"></asp:DropDownList>
            </div>

            <div class="col-md-2">
                <label>Financial Year</label>
                <asp:DropDownList CssClass="form-control" ID="ddlFinancialyear" runat="server">
                    <asp:ListItem Value="0">All</asp:ListItem>
                    <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                    <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                    <asp:ListItem Value="2025-26">2025-26</asp:ListItem>
		    <asp:ListItem Value="2026-27">2026-27</asp:ListItem>

                </asp:DropDownList>
            </div>

            <div class="col-md-2">
                <label>Month</label>
                <asp:DropDownList CssClass="form-control" ID="ddlmonth" runat="server">
                    <asp:ListItem Value="0">All</asp:ListItem>
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-md-2 d-flex align-items-end">
                <asp:Button runat="server" CssClass="btn btn-theme btn-block" ValidationGroup="a" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" />
            </div>

        </div>
    </fieldset>


    <!-- ========================= GRID SECTION =========================== -->
    <div class="row justify-content-center" id="grdbill" runat="server" visible="false">
        <div class="col-12">
            <fieldset>
                <legend>Bills Details</legend>

                <div class="row mb-3">
                    <div class="col-md-4">
                        <asp:Button ID="Button2" runat="server" Text="Export To PDF" CssClass="button2" OnClientClick="printGrid()" />
                        &nbsp;
                    <input type="button" id="btnExport" value="Export To Excel" class="button2" />
                    </div>

                    <div class="col-md-3"></div>

                    <div class="col-md-2">
                        <label>Total Count</label>
                        <asp:TextBox ID="txtCount" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        <label>Total Amount</label>
                        <asp:TextBox ID="lblAmount" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <!-- Grid Responsive Wrapper -->
                <div class="table-responsive px-2">
                    <asp:GridView runat="server" DataKeyNames="Bill_Number" ID="GrdBills"
                        CssClass="table table-bordered table-striped table-hover w-100"
                        AutoGenerateColumns="False"
                        OnRowCommand="GrdBills_RowCommand">

                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblBranch_Name" Text='<%# Eval("Branch_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Godown Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblBill_Number" Text='<%# Eval("Bill_Number") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Commodity Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCommodity_Name" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCrop_Year" Text='<%# Eval("Crop_Year") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Financial Year">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFinancial_Year" Text='<%# Eval("Financial_Year") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Month">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblMonth" Text='<%# Eval("MonthYear") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Closing Balance">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblClosing_Balance" Text='<%# Eval("Closing_Balance") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net Amount">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblNet_Amount" Text='<%# Eval("Net_Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Print Bill">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnPrint" runat="server" CausesValidation="false" CommandName="Print" CommandArgument='<%# Eval("Bill_Number")%>' Text="Print" CssClass="BTNBLUE" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </fieldset>
        </div>
    </div>

    <script type="text/javascript" src="../../JS/table2excel.js"></script>
    <script type="text/javascript">
        $("#btnExport").click(function () {
            $("[id*=GrdBills]").table2excel({
                filename: "Nafed_Print_Bill_Summary.xls"
            });
        });
    </script>
</asp:Content>

