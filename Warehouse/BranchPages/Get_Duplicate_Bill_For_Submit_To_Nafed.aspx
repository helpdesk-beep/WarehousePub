<%@ Page Title="NAFED Duplicate Bill Submission Ledger" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/Get_Duplicate_Bill_For_Submit_To_Nafed.aspx.cs" Inherits="BranchPages_Get_Duplicate_Bill_For_Submit_To_Nafed" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />

    <style type="text/css">
        .main-card {
            background-color: #ffffff;
            border: 1px solid #e3e6f0;
            border-radius: 0.35rem;
            box-shadow: 0 0.15rem 1.75rem 0 rgba(58, 59, 69, 0.15);
            padding: 1.5rem;
            margin-top: 1rem;
        }

        .page-header-title {
            font-size: 1.4rem;
            font-weight: 700;
            color: #4e73df;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            border-bottom: 2px solid #eaecf4;
            padding-bottom: 0.75rem;
            margin-bottom: 1.5rem;
        }

        .search-container-box {
            background-color: #f8fafc;
            border: 1px dashed #cbd5e1;
            border-radius: 6px;
            padding: 10px 15px;
            margin-bottom: 15px;
        }

        /* Grid Framework Styling */
        .custom-nafed-grid {
            width: 100%;
            border-collapse: collapse !important;
            table-layout: auto;
        }

            .custom-nafed-grid th {
                background: linear-gradient(180deg, #4e73df 0%, #224abe 100%) !important;
                color: #ffffff !important;
                font-weight: 600;
                text-align: center;
                vertical-align: middle !important;
                padding: 10px 5px !important;
                border: 1px solid #cbd5e1 !important;
                font-size: 0.8rem;
                text-transform: uppercase;
                white-space: nowrap;
            }

            .custom-nafed-grid td {
                padding: 8px 8px !important;
                vertical-align: middle !important;
                font-size: 0.82rem;
                border: 1px solid #cbd5e1 !important;
                color: #2d3748;
            }

            .custom-nafed-grid tr:nth-of-type(even) {
                background-color: #f8f9fc;
            }

        .grandtotal-row td {
            background-color: #eff6ff !important;
            border-top: 2px solid #4e73df !important;
            border-bottom: 2px double #224abe !important;
            font-weight: bold !important;
            color: #1e3a8a !important;
            font-size: 0.88rem !important;
        }

        .print-only-header {
            display: none;
        }

        /* Print Media Setup */
        @media print {
            @page {
                size: landscape;
                margin: 12mm 8mm;
            }

            body * {
                visibility: hidden;
            }

            .print-wrapper, .print-wrapper * {
                visibility: visible;
            }

            .print-wrapper {
                position: absolute;
                left: 0;
                top: 0;
                width: 100%;
                padding: 0;
                margin: 0;
                box-shadow: none;
                border: none;
            }

            .search-container-box, .action-area, .btn, .back-btn, .bottom-action-container {
                display: none !important;
            }

            .print-only-header {
                display: block !important;
                border-bottom: 3px double #1e3a8a;
                padding-bottom: 10px;
                margin-bottom: 20px;
            }

            .page-header-title {
                display: none !important;
            }

            .custom-nafed-grid {
                border-collapse: collapse !important;
                width: 100% !important;
                page-break-inside: auto;
            }

                .custom-nafed-grid tr {
                    page-break-inside: avoid !important;
                    page-break-after: auto !important;
                }

                .custom-nafed-grid thead {
                    display: table-header-group !important;
                }

                .custom-nafed-grid th {
                    background-color: #4e73df !important;
                    color: #ffffff !important;
                    -webkit-print-color-adjust: exact !important;
                    print-color-adjust: exact !important;
                }

            .grandtotal-row td {
                background-color: #eff6ff !important;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }
        }
    </style>

    <script type="text/javascript">
        // Real-time live grid search filter logic
        function performGridSearch() {
            var input = document.getElementById('txtClientSearch');
            var filter = input.value.toUpperCase();
            var grid = document.getElementById('<%= gvDuplicateBills.ClientID %>');
            if (!grid) return;

            var tr = grid.getElementsByTagName('tr');
            for (var i = 1; i < tr.length; i++) {
                if (tr[i].className.indexOf('grandtotal-row') > -1) continue;

                var showRow = false;
                var cellsToSearch = [2, 3, 4, 6, 7];

                for (var j = 0; j < cellsToSearch.length; j++) {
                    var td = tr[i].cells[cellsToSearch[j]];
                    if (td) {
                        var textValue = td.textContent || td.innerText;
                        if (textValue.toUpperCase().indexOf(filter) > -1) {
                            showRow = true;
                            break;
                        }
                    }
                }
                tr[i].style.display = showRow ? "" : "none";
            }
        }

        // Toggle Select All checkboxes + Button Visibility Update
        function toggleSelectAll(chkHeader) {
            var grid = document.getElementById('<%= gvDuplicateBills.ClientID %>');
            if (!grid) return;
            var checkboxes = grid.getElementsByTagName("input");

            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].type === "checkbox" && checkboxes[i] !== chkHeader) {
                    checkboxes[i].checked = chkHeader.checked;
                }
            }
            // Real-time check triggers immediately after select-all toggle
            updateSendButtonVisibility();
        }

        // ACCURATE CHECKBOX SELECTION CHECKER FOR BUTTON VISIBILITY
        function updateSendButtonVisibility() {
            var grid = document.getElementById('<%= gvDuplicateBills.ClientID %>');
            var btnContainer = document.getElementById('divSendButton');
            if (!grid || !btnContainer) return;

            var checkboxes = grid.getElementsByTagName("input");
            var isAnyChecked = false;

            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].type === "checkbox") {
                    // Check if it's a row checkbox (skipping header ID) or header is checked
                    if (checkboxes[i].checked && checkboxes[i].id.indexOf("chkSelectAll") === -1) {
                        isAnyChecked = true;
                        break;
                    }
                }
            }

            // Button ko flex/block center alignment me toggle karega
            btnContainer.style.display = isAnyChecked ? "block" : "none";
        }

        function CallPrint() {
            window.print();
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid print-wrapper main-card">

        <!-- PRINT BANNER HEADER -->
        <div class="print-only-header text-center">
            <h2 style="font-weight: bold; color: #1e3a8a; margin: 0; font-size: 22px;">MADHYA PRADESH WAREHOUSING & LOGISTICS CORPORATION</h2>
            <h4 style="color: #475569; margin: 5px 0 0 0; font-size: 15px;">Duplicate Digitally Signed Bills For NAFED Submission</h4>
            <p style="font-size: 12px; font-weight: bold; margin-top: 5px;">
                Selected Branch: <%= Session["BranchId"] != null ? Session["BranchId"].ToString() : "" %> | Printed On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %>
            </p>
        </div>

        <div class="page-header-title">
            <i class="fas fa-file-invoice-dollar mr-2 text-primary"></i>NAFED Duplicate Storage Bills Ledger
        </div>

        <!-- Live Grid Search & Export Actions Panel -->
        <div class="search-container-box shadow-sm row no-gutters align-items-center justify-content-between action-area">
            <div class="col-md-6 d-flex align-items-center">
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text bg-white border-right-0 text-muted"><i class="fas fa-filter"></i></span>
                    </div>
                    <input type="text" id="txtClientSearch" class="form-control border-left-0" placeholder="Search District, Branch, Godown or Bill No..." onkeyup="performGridSearch();" autocomplete="off" />
                </div>
            </div>
            <div class="col-md-5 text-right mt-2 mt-md-0">
                <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click" CssClass="btn btn-success btn-sm font-weight-bold shadow-sm mr-2">
                    <i class="fas fa-file-excel mr-1"></i> Export Excel
                </asp:LinkButton>
                <asp:LinkButton ID="btnPrint" runat="server" OnClientClick="return CallPrint();" CssClass="btn btn-secondary btn-sm font-weight-bold shadow-sm">
                    <i class="fas fa-print mr-1"></i> Print Report
                </asp:LinkButton>
            </div>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvDuplicateBills" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered custom-nafed-grid" Width="100%"
                GridLines="Both" BorderColor="#e3e6f0" BorderStyle="Solid" BorderWidth="1px"
                OnRowDataBound="gvDuplicateBills_RowDataBound" OnDataBound="gvDuplicateBills_DataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="toggleSelectAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" onclick="updateSendButtonVisibility();" />
                        </ItemTemplate>
                        <ItemStyle Width="40px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="text-center">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="District_Name" HeaderText="District Name" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Branch_Name" HeaderText="Branch Name" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true" />
                    <asp:TemplateField HeaderText="Bill Number" ItemStyle-CssClass="text-center font-weight-bold text-primary">
                        <ItemTemplate>
                            <asp:Label ID="lblBillNumber" runat="server" Text='<%# Eval("Bill_Number") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" ItemStyle-CssClass="text-center" />
                    <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" ItemStyle-CssClass="text-center" />
                    <asp:BoundField DataField="Month" HeaderText="Month" ItemStyle-CssClass="text-center" />
                    <asp:BoundField DataField="Net_Amount" HeaderText="Net Amount (₹)" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="text-success font-weight-bold" />
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-warning text-center font-weight-bold m-0" role="alert">
                        <i class="fas fa-exclamation-triangle mr-1"></i>Selected Branch context ke liye koi Digitally Signed Duplicate Bills mil nahi rahe hain.
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>

        <!-- 👇 DYNAMICALLY CONTROLLED BUTTON CONTAINER 👇 -->
        <div id="divSendButton" class="bottom-action-container mt-4 text-center action-area" style="display: none;">
            <asp:LinkButton ID="btnSendToNafed" runat="server" OnClick="btnSendToNafed_Click"
                CssClass="btn btn-primary font-weight-bold shadow px-4 py-2" Style="font-size: 1rem;"
                OnClientClick="return confirm('Are you sure you want to send selected bills to NAFED?');">
                <i class="fas fa-paper-plane mr-2"></i> Send Selected Bills to NAFED
            </asp:LinkButton>
        </div>

    </div>
</asp:Content>
