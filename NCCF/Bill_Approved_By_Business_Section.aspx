<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Nccf_Master.master" AutoEventWireup="true" CodeFile="~/NCCF/Bill_Approved_By_Business_Section.aspx.cs" Inherits="NCCF_Bill_Approved_By_Business_Section" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <style>
        .custom-grid {
            width: 100%;
            border-collapse: collapse;
            background: #fff;
            font-size: 14px;
        }
        .custom-grid th {
            background: #8ea9c5 !important;
            color: white;
            text-align: center;
            padding: 10px !important;
            font-weight: 600;
        }
        .custom-grid td {
            padding: 8px !important;
            vertical-align: middle !important;
        }
        .custom-grid tr:nth-child(even) {
            background: #f2f6fc;
        }
        .custom-grid tr:hover {
            background: #d9ebff;
            transition: 0.2s;
        }
        .BTNBLUE {
            background: #5bc773;
            border: none;
            padding: 5px 10px;
            color: white !important;
            border-radius: 4px;
            font-size: 13px;
            cursor: pointer;
        }
        .BTNBLUE:hover {
            background: #46cb63;
        }
        .approve-dropdown {
            width: 100px;
            height: 30px;
        }
        .remark-box {
            width: 120px;
            height: 30px;
            margin-top: 5px;
        }
        .sticky-header th {
            position: sticky;
            top: 0;
            z-index: 10;
            background: #0066cc !important;
            color: #fff !important;
        }
        .grid-theme {
            border-collapse: collapse;
            width: 100%;
            font-family: Arial;
        }
        .grid-theme td, .grid-theme th {
            padding: 8px;
            border: 1px solid #ddd;
        }
        .grid-theme tr:nth-child(even) {
            background-color: #f9f9f9;
        }
        .grid-theme tr:hover {
            background-color: #e2f1ff;
        }
        #searchBox {
            padding: 8px;
            width: 250px;
            margin-bottom: 12px;
            border-radius: 4px;
            border: 1px solid #aaa;
        }
        .highlight-total {
            background-color: #ffc107;
            color: #000;
            font-weight: bold;
            padding: 5px 10px;
            border-radius: 5px;
            border: 1px solid #ff9800;
        }
    </style>

    <script type="text/javascript">
        function calculatePSF(psfBox) {
            var row = psfBox.closest("tr");
            var pssBox = row.querySelector("[id*='txtPSS']");
            var originalPSS = parseFloat(pssBox.getAttribute("data-original"));
            if (isNaN(originalPSS)) {
                originalPSS = parseFloat(pssBox.value) || 0;
                pssBox.setAttribute("data-original", originalPSS);
            }
            var psf = parseFloat(psfBox.value) || 0;
            if (psf > originalPSS) {
                alert("PSF amount PSS se zyada nahi ho sakta");
                psfBox.value = 0;
                pssBox.value = originalPSS.toFixed(2);
                return;
            }
            pssBox.value = (originalPSS - psf).toFixed(2);
        }

        function initSelect2() {
            $('#<%= ddldistrict.ClientID %>').select2({ placeholder: "Select District", allowClear: true, width: '100%' });
            $('#<%= ddlbranch.ClientID %>').select2({ placeholder: "Select Branch", allowClear: true, width: '100%' });
            $('#<%= ddlGodown.ClientID %>').select2({ placeholder: "Select Godown", allowClear: true, width: '100%' });
        }
        $(document).ready(function () { initSelect2(); });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () { initSelect2(); });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content_wrapper">
        <fieldset>
            <legend>Pending Bills For Processing</legend>
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px; font-weight: bold;">Commodity</label>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlcommodity" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px; font-weight: bold;">Crop Year</label>
                        <asp:DropDownList CssClass="form-control" ID="ddlcropyear" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px; font-weight: bold;">District Name</label>
                        <asp:DropDownList CssClass="form-control select2" ID="ddldistrict" runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px; font-weight: bold;">Branch Name</label>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlbranch" runat="server" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged1" AutoPostBack="true">
                            <asp:ListItem Value="0">All</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px; font-weight: bold;">Godown Name</label>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlGodown" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px; font-weight: bold;">Financial Year</label>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlFinancialyear" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                            <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                            <asp:ListItem Value="2025-26">2025-26</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px; font-weight: bold;">Month</label>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlmonth" runat="server">
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
                </div>
            </div>
            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-2" style="text-align: center; margin-top: 40px">
                    <asp:Button runat="server" CssClass="btn btn-success" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" autopostback="true" />
                </div>
            </div>
        </fieldset>

        <fieldset style="align-content: center" runat="server" id="grdbill" visible="false">
            <legend>Bills Details</legend>
            <div class="row" style="margin-bottom: 10px;">
                <div class="col-md-2">
                    <asp:Button runat="server" class="btn btn-primary btn-block" Text="Export All Data" ID="btnExport" OnClick="btnExport_Click" />
                </div>
                <div class="col-md-1">
                    <label style="margin-top: 10px; font-weight: bold;">Search:</label>
                </div>
                <div class="col-md-2">
                    <input type="text" id="searchBox" placeholder="Search..." onkeyup="searchGrid()" />
                </div>
                <div class="col-md-1">
                    <label style="margin-top: 10px; font-weight: bold;">Total Bills:</label>
                </div>
                <div class="col-md-1" style="margin-top: 10px;">
                    <asp:Label ID="litTotalBills" runat="server" CssClass="highlight-total"></asp:Label>
                </div>
                <div class="col-md-1">
                    <label style="margin-top: 10px; font-weight: bold;">Total Amt:</label>
                </div>
                <div class="col-md-1" style="margin-top: 10px;">
                    <asp:Label ID="litTotalAmount" runat="server" CssClass="highlight-total"></asp:Label>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:GridView ID="GrdBills" runat="server" AutoGenerateColumns="False" CssClass="custom-grid"
                                OnDataBound="GrdBills_DataBound" OnPageIndexChanging="GrdBills_PageIndexChanging"
                                autopostback="true" OnRowCommand="GrdBills_RowCommand" OnRowDataBound="GrdBills_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                                            <asp:HiddenField runat="server" ID="hdnDistrict_Id" Value='<%#Eval("District_Id")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branch Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblBranch_Name" Text='<%# Eval("Branch_Name") %>'></asp:Label>
                                            <asp:HiddenField runat="server" ID="hdnBranch_Id" Value='<%#Eval("Branch_Id")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Godown Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                            <asp:HiddenField runat="server" ID="hdnGodown_Id" Value='<%#Eval("Godown_Id")%>' />
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
                                            <asp:HiddenField runat="server" ID="hdnCommodity_Id" Value='<%#Eval("Commodity_Id")%>' />
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
                                            <asp:HiddenField runat="server" ID="hdnMonth" Value='<%#Eval("Month")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Chargable Bags">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotal_Chargeble_Bags" Text='<%# Eval("Total_Chargeble_Bags") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Amount">
                                        <ItemTemplate>
                                            <asp:Label runat="server" CssClass="net-amount" ID="lblNet_Amount" Text='<%# Eval("Net_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approve" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlApprovalStatus" runat="server" onchange="showRemark(this)" Style="width: 120px;">
                                                <asp:ListItem Value="Y">Approve</asp:ListItem>
                                                <asp:ListItem Value="N">Reject</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="remarkBox" Style="display: none; width: 120px; margin-top: 5px;" Text="NA" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PSS" ItemStyle-Width="120px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPSS" runat="server" CssClass="form-control text-center" Placeholder="pss" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PSF" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPSF" runat="server" CssClass="form-control text-center" Placeholder="psf" onkeyup="calculatePSF(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Update">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="false" CommandName="EditRow" CssClass="btn btn-sm btn-success" CommandArgument='<%# Eval("Bill_Number")%>'><i class="fa fa-save">Update</i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Print Bill">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnPrint" runat="server" CausesValidation="false" CommandName="Print" CommandArgument='<%# Eval("Bill_Number")%>' CssClass="btn btn-sm btn-primary"><i class="fa fa-print">Print</i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select" ItemStyle-Width="50px">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" ToolTip="Select All (Max 100)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="return limitSelection(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            
            <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnPrintSelected" runat="server" Text="Print Selected Bills (Max 100)" CssClass="btn btn-success btn-lg" OnClick="btnPrintSelected_Click" OnClientClick="return validateSelection();" Style="padding: 10px 30px; font-weight: bold;" />
                    <asp:Label ID="lblSelectionMessage" runat="server" ForeColor="Red" Font-Bold="true" Style="margin-left: 15px;"></asp:Label>
                </div>
            </div>
        </fieldset>
    </div>

    <script>
        function searchGrid() {
            var input = document.getElementById("searchBox").value.toLowerCase();
            var table = document.getElementById("<%= GrdBills.ClientID %>");
            var rows = table.getElementsByTagName("tr");
            var visibleCount = 0;
            var totalAmount = 0;

            for (var i = 1; i < rows.length; i++) {
                var show = false;
                var cells = rows[i].getElementsByTagName("td");
                for (var j = 0; j < cells.length; j++) {
                    if (cells[j] && cells[j].innerText.toLowerCase().includes(input)) {
                        show = true;
                        break;
                    }
                }
                rows[i].style.display = show ? "" : "none";
                if (show) {
                    visibleCount++;
                    var amountLabel = rows[i].querySelector(".net-amount");
                    if (amountLabel) {
                        var amt = parseFloat(amountLabel.innerText.replace(/,/g, '')) || 0;
                        totalAmount += amt;
                    }
                }
            }
            document.getElementById("<%= litTotalBills.ClientID %>").innerText = visibleCount;
            document.getElementById("<%= litTotalAmount.ClientID %>").innerText = totalAmount.toFixed(2);
        }

        function showRemark(ddl) {
            let row = ddl.closest("tr");
            let remark = row.querySelector(".remarkBox");
            if (remark) {
                remark.style.display = (ddl.value === "N") ? "block" : "none";
            }
        }

        function limitSelection(checkbox) {
            var grid = document.getElementById("<%= GrdBills.ClientID %>");
            var checkboxes = grid.querySelectorAll("input[type=checkbox][id*='chkSelect']:not([id*='chkSelectAll'])");
            var checkedCount = 0;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) checkedCount++;
            }
            if (checkbox.checked && checkedCount >= 100) {
                alert("You can select maximum 100 bills at a time.");
                checkbox.checked = false;
                return false;
            }
            updateSelectAll();
            return true;
        }

        function updateSelectAll() {
            var grid = document.getElementById("<%= GrdBills.ClientID %>");
            if (!grid) return;
            var chkAll = grid.querySelector("input[type=checkbox][id*='chkSelectAll']");
            var checkboxes = grid.querySelectorAll("input[type=checkbox][id*='chkSelect']:not([id*='chkSelectAll'])");
            if (!chkAll) return;
            var allChecked = true;
            var anyChecked = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (!checkboxes[i].checked) { allChecked = false; } else { anyChecked = true; }
            }
            chkAll.checked = allChecked && anyChecked;
        }

        function validateSelection() {
            var grid = document.getElementById("<%= GrdBills.ClientID %>");
            if (!grid) { alert("Grid not found!"); return false; }
            var checkboxes = grid.querySelectorAll("input[type=checkbox][id*='chkSelect']:not([id*='chkSelectAll'])");
            var selectedCount = 0;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) selectedCount++;
            }
            if (selectedCount === 0) { alert("Please select at least one bill to print."); return false; }
            if (selectedCount > 100) { alert("You can select maximum 100 bills at a time."); return false; }
            return true;
        }
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () { updateSelectAll(); });
    </script>
</asp:Content>