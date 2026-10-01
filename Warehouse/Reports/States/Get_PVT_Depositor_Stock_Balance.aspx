<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Get_PVT_Depositor_Stock_Balance.aspx.cs" Inherits="Reports_States_Get_PVT_Depositor_Stock_Balance" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Private (Cultivator) Stock Balance Position Report</title>
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style type="text/css">
        body {
            background-color: #f8fafc;
            font-family: 'Segoe UI', system-ui, sans-serif;
            color: #334155;
            padding: 15px;
        }
        fieldset {
            border: 1px solid #0d6efd;
            padding: 15px;
            margin-bottom: 20px;
            border-radius: 8px;
            background: #fff;
        }
        legend {
            padding: 6px 25px;
            border-radius: 20px;
            width: 100%;
            border: 1px solid #0d6efd;
            font-size: 16px;
            font-weight: bold;
            background-color: #0d6efd;
            color: white;
            text-align: center;
            float: none;
            line-height: 1.4;
        }
        .button {
            padding: 6px 15px;
            font-size: 13px;
            font-weight: bold;
            border-radius: 6px;
            cursor: pointer;
            transition: 0.2s;
            text-decoration: none;
            display: inline-block;
        }
        .button2 {
            background-color: white;
            color: #0d6efd;
            border: 2px solid #0d6efd;
        }
        .button2:hover {
            background-color: #0d6efd;
            color: white;
        }
        .print-header-block {
            display: none;
        }
        .grid-container {
            max-height: 520px;
            overflow-y: auto;
            overflow-x: auto;
            border: 1px solid #cbd5e1;
            border-radius: 6px;
            background-color: #ffffff;
        }
        .custom-matrix-grid {
            border-collapse: separate !important;
            border-spacing: 0;
            width: 100%;
            margin-bottom: 0 !important;
        }
        .custom-matrix-grid th {
            position: sticky !important;
            top: 0;
            background: #0d6efd !important;
            color: white !important;
            text-align: center;
            font-size: 12px;
            font-weight: 600;
            vertical-align: middle;
            z-index: 10;
            border: 1px solid #cbd5e1 !important;
            padding: 10px 4px !important;
            text-transform: uppercase;
            box-shadow: inset 0 -1px 0 #cbd5e1;
        }
        .custom-matrix-grid td {
            font-size: 12px;
            color: #1e293b !important;
            vertical-align: middle;
            padding: 6px 8px !important;
            border: 1px solid #cbd5e1 !important;
        }
        .custom-matrix-grid tr:nth-child(even) {
            background-color: #f8fafc;
        }
        .custom-matrix-grid tr:hover {
            background-color: #f1f5f9;
        }
        .subtotal-row {
            background-color: #fed7aa !important;
            font-weight: bold !important;
            color: #000000 !important;
        }
        .subtotal-row td {
            border-top: 1px solid #cbd5e1 !important;
            border-bottom: 1px solid #cbd5e1 !important;
        }
        .grandtotal-row td {
            background-color: #dbeafe !important;
            border-top: 2px solid #2563eb !important;
            border-bottom: 2px double #2563eb !important;
            font-weight: bold !important;
            color: #1e40af !important;
        }
        .text-right-align {
            text-align: right !important;
            padding-right: 12px !important;
            white-space: nowrap !important;
        }
        .text-center-align {
            text-align: center !important;
            white-space: nowrap !important;
        }
        .column-chooser-box {
            background: #f8fafc;
            border: 1px solid #cbd5e1;
            border-radius: 6px;
            padding: 12px;
            margin-top: 15px;
        }
        .column-chooser-box label {
            margin-right: 15px;
            margin-left: 5px;
            font-size: 13px;
            font-weight: 500;
            color: #334155;
            cursor: pointer;
        }
        .multiselect-dropdown-wrapper {
            position: relative;
            display: inline-block;
            width: 100%;
        }
        .multiselect-select-box {
            background-color: #ffffff;
            border: 1px solid #ced4da;
            border-radius: 0.375rem;
            padding: 0.375rem 2.25rem 0.375rem 0.75rem;
            cursor: pointer;
            user-select: none;
            font-size: 14px;
        }
        .multiselect-select-box::after {
            content: "";
            position: absolute;
            right: 12px;
            top: 50%;
            transform: translateY(-50%);
            border: 5px solid transparent;
            border-top-color: #6c757d;
        }
        .multiselect-layer-container {
            display: none;
            position: absolute;
            top: 100%;
            left: 0;
            right: 0;
            z-index: 1050;
            background: #ffffff;
            border: 1px solid #dee2e6;
            border-radius: 0.375rem;
            box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
            max-height: 220px;
            overflow-y: auto;
            padding: 8px;
        }
        .multiselect-search-box {
            width: 100% !important;
            padding: 6px 10px !important;
            margin-bottom: 8px !important;
            border: 1px solid #cbd5e1 !important;
            border-radius: 4px !important;
        }
        .multiselect-layer-container table {
            width: 100%;
        }
        .multiselect-layer-container table label {
            margin-left: 8px !important;
            cursor: pointer !important;
            font-size: 13px !important;
        }
        @media print {
            @page { size: landscape; margin: 10mm 5mm; }
            * { -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
            body, html { background: #fff !important; color: #000 !important; padding: 0; }
            .filter-section, .button-area, .column-chooser-box { display: none !important; }
            .print-header-block { display: block !important; width: 100% !important; text-align: center !important; margin-bottom: 15px; }
            legend { display: none !important; }
            fieldset { border: none !important; padding: 0; margin: 0; }
            .grid-container { max-height: none !important; overflow: visible !important; border: none !important; }
            .custom-matrix-grid th { background-color: #1e3a8a !important; color: #fff !important; border: 1px solid #000 !important; position: static !important; }
            .custom-matrix-grid td { border: 1px solid #000 !important; font-size: 10px !important; background-color: transparent !important; }
        }
    </style>

    <script type="text/javascript">
        function toggleDropdownMenu(e, containerId, searchBoxId) {
            var menu = document.getElementById(containerId);
            if (!menu) return;

            var currentDisplay = menu.style.display;
            if (menu) menu.style.display = 'none';

            if (currentDisplay !== 'block') {
                menu.style.display = 'block';
                setTimeout(function () {
                    var sBox = document.getElementById(searchBoxId);
                    if (sBox) sBox.focus();
                }, 60);
            }
            if (e && e.stopPropagation) { e.stopPropagation(); } else { window.event.cancelBubble = true; }
        }

        function filterLive(searchBoxId, cblClientID) {
            var input = document.getElementById(searchBoxId).value.toLowerCase().trim();
            var cbl = document.getElementById(cblClientID);
            if (!cbl) return;
            var rows = cbl.getElementsByTagName('tr');
            for (var i = 0; i < rows.length; i++) {
                var label = rows[i].getElementsByTagName('label')[0];
                if (label) {
                    var textValue = label.textContent || label.innerText;
                    rows[i].style.display = (i === 0 || textValue.toLowerCase().indexOf(input) > -1) ? "" : "none";
                }
            }
        }

        function onCheckboxClicked(cblClientID, index, captionId, defaultText) {
            var cbl = document.getElementById(cblClientID);
            if (!cbl) return;
            var checkboxes = cbl.getElementsByTagName('input');
            if (index === 0) {
                if (checkboxes[0].checked) {
                    for (var i = 1; i < checkboxes.length; i++) checkboxes[i].checked = false;
                }
            } else {
                if (checkboxes[index].checked) checkboxes[0].checked = false;
            }
            var anyChecked = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) { anyChecked = true; break; }
            }
            if (!anyChecked && checkboxes.length > 0) checkboxes[0].checked = true;
            updateCaptionText(cblClientID, captionId, defaultText);
        }

        function updateCaptionText(cblClientID, captionId, defaultText) {
            var cbl = document.getElementById(cblClientID);
            if (!cbl) return;
            var checkboxes = cbl.getElementsByTagName('input');
            var labels = cbl.getElementsByTagName('label');
            var selectedText = "";
            var count = 0;

            if (checkboxes.length > 0 && checkboxes[0].checked) { selectedText = defaultText; }
            else {
                for (var i = 1; i < checkboxes.length; i++) {
                    if (checkboxes[i].checked) {
                        count++;
                        if (selectedText === "") selectedText = labels[i].innerHTML;
                    }
                }
                if (count > 1) selectedText = "Selected (" + count + ")";
            }
            var capDiv = document.getElementById(captionId);
            if (capDiv) { capDiv.innerHTML = selectedText || "-- Select --"; }
        }

        document.onclick = function (e) {
            var target = (e && e.target) || window.event.srcElement;
            var comMenu = document.getElementById('commodityMenuContainer');
            if (comMenu && !comMenu.contains(target) && target.id !== 'divComCaption' && target.id !== 'txtComSearch') {
                comMenu.style.display = 'none';
            }
        };

        function bindEvents() {
            var comCbl = document.getElementById('<%= ddlComodity.ClientID %>');
            if (comCbl) {
                var chks2 = comCbl.getElementsByTagName('input');
                for (var i = 0; i < chks2.length; i++) {
                    (function (idx) {
                        chks2[idx].onclick = function () { onCheckboxClicked('<%= ddlComodity.ClientID %>', idx, 'divComCaption', '-- All Commodities --'); };
                    })(i);
                }
            }
            updateCaptionText('<%= ddlComodity.ClientID %>', 'divComCaption', '-- All Commodities --');
        }

        window.onload = function () {
            bindEvents();
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid">

            <div class="print-header-block text-center">
                <h2 class="fw-bold" style="color: #1e3a8a; font-size: 20px;">M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
                <h5 class="text-muted fw-semibold" style="font-size: 13px;">District, Branch, Godown, Commodity Wise Private (Cultivator) Stock Balance Position Report</h5>
                <p style="font-size: 11px; font-weight: bold;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %> | Quantity In M.T.</p>
                <hr style="border: 1.5px solid #1e3a8a; margin-top: 5px; margin-bottom: 15px;" />
            </div>

            <fieldset>
                <legend>Godown Wise Private (Cultivator) Stock Balance Position <span class="badge bg-danger">In M.T.</span></legend>

                <div class="card mb-3 shadow-sm button-area bg-light">
                    <div class="card-body d-flex justify-content-between align-items-center py-2">
                        <div class="d-flex gap-2">
                            <asp:Button ID="btnPrint" runat="server" Text="📄 Print / Save PDF" CssClass="button button2" OnClientClick="window.print(); return false;" />
                            <asp:Button ID="btnExportExcel" runat="server" Text="📊 Export To Excel" CssClass="button button2" OnClick="btnExportExcel_Click" />
                        </div>
                    </div>
                </div>

                <div class="card mb-4 shadow-sm filter-section bg-light">
                    <div class="card-body">
                        <div class="row align-items-end g-3">

                            <div class="col-md-4">
                                <label class="form-label fw-bold text-dark"><i class="fa fa-calendar text-primary mr-1"></i>As On Date (DD-MM-YYYY):</label>
                                <asp:TextBox ID="txtpaymentdate" runat="server" CssClass="form-select" placeholder="dd-MM-yyyy" autocomplete="off"></asp:TextBox>
                            </div>

                            <div class="col-md-4">
                                <label class="form-label fw-bold text-dark"><i class="fa fa-cubes text-warning mr-1"></i>Commodity Filter:</label>
                                <div class="multiselect-dropdown-wrapper">
                                    <div id="divComCaption" class="multiselect-select-box" onclick="toggleDropdownMenu(event, 'commodityMenuContainer', 'txtComSearch')">
                                        -- All Commodities --
                                    </div>
                                    <div id="commodityMenuContainer" class="multiselect-layer-container">
                                        <input type="text" id="txtComSearch" placeholder="Filter commodity..." class="multiselect-search-box" onkeyup="filterLive('txtComSearch', '<%= ddlComodity.ClientID %>')" autocomplete="off" />
                                        <asp:CheckBoxList ID="ddlComodity" runat="server" RepeatLayout="Table" RepeatColumns="1"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <asp:Button ID="btnshow" runat="server" Text="🔍 Search Position" CssClass="btn btn-primary w-100 fw-bold" OnClick="btnSearch_Click" Style="background-color: #0d6efd; border-color: #0d6efd;" />
                            </div>
                        </div>

                        <div class="column-chooser-box">
                            <div class="fw-bold mb-2 text-primary" style="font-size: 14px;"><i class="fa fa-sliders me-1"></i>Select Financial Years to Display:</div>
                            <asp:CheckBoxList ID="cblColumns" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="cblColumns_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">2016-17</asp:ListItem>
                                <asp:ListItem Value="1" Selected="True">2017-18</asp:ListItem>
                                <asp:ListItem Value="2" Selected="True">2018-19</asp:ListItem>
                                <asp:ListItem Value="3" Selected="True">2019-20</asp:ListItem>
                                <asp:ListItem Value="4" Selected="True">2020-21</asp:ListItem>
                                <asp:ListItem Value="5" Selected="True">2021-22</asp:ListItem>
                                <asp:ListItem Value="6" Selected="True">2022-23</asp:ListItem>
                                <asp:ListItem Value="7" Selected="True">2023-24</asp:ListItem>
                                <asp:ListItem Value="8" Selected="True">2024-25</asp:ListItem>
                                <asp:ListItem Value="9" Selected="True">2025-26</asp:ListItem>
                                <asp:ListItem Value="10" Selected="True">2026-27</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>

                <div class="grid-container">
                    <asp:GridView ID="GV_StockPositionDetails" runat="server" AutoGenerateColumns="False" ShowFooter="false"
                        CssClass="table table-bordered custom-matrix-grid">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                <ItemStyle Width="50px" CssClass="text-center-align" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="District" HeaderText="District Name" ItemStyle-Font-Bold="true" />
                            <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                            <asp:BoundField DataField="GodownName" HeaderText="Godown Name" />
                            <asp:BoundField DataField="CommodityName" HeaderText="Commodity" />

                            <asp:TemplateField HeaderText="2016-17" ItemStyle-CssClass="text-right-align">
                                <ItemTemplate><asp:Label ID="lblCY1" runat="server" Text='<%# Eval("[2016-17]") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="2017-18" ItemStyle-CssClass="text-right-align">
                                <ItemTemplate><asp:Label ID="lblCY2" runat="server" Text='<%# Eval("[2017-18]") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="2018-19" ItemStyle-CssClass="text-right-align">
                                <ItemTemplate><asp:Label ID="lblCY3" runat="server" Text='<%# Eval("[2018-19]") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="2019-20" ItemStyle-CssClass="text-right-align">
                                <ItemTemplate><asp:Label ID="lblCY4" runat="server" Text='<%# Eval("[2019-20]") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="2020-21" ItemStyle-CssClass="text-right-align">
                                <ItemTemplate><asp:Label ID="lblCY5" runat="server" Text='<%# Eval("[2020-21]") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="2021-22" ItemStyle-CssClass="text-right-align">
                                <ItemTemplate><asp:Label ID="lblCY6" runat="server" Text='<%# Eval("[2021-22]") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="2022-23" ItemStyle-CssClass="text-right-align">
                                <ItemTemplate><asp:Label ID="lblCY7" runat="server" Text='<%# Eval("[2022-23]") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="2023-24" ItemStyle-CssClass="text-right-align">
                                <ItemTemplate><asp:Label ID="lblCY8" runat="server" Text='<%# Eval("[2023-24]") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="2024-25" ItemStyle-CssClass="text-right-align">
                                <ItemTemplate><asp:Label ID="lblCY9" runat="server" Text='<%# Eval("[2024-25]") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="2025-26" ItemStyle-CssClass="text-right-align">
                                <ItemTemplate><asp:Label ID="lblCY10" runat="server" Text='<%# Eval("[2025-26]") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="2026-27" ItemStyle-CssClass="text-right-align">
                                <ItemTemplate><asp:Label ID="lblCY11" runat="server" Text='<%# Eval("[2026-27]") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total Qty In MT" ItemStyle-CssClass="text-right-align" ItemStyle-Font-Bold="true">
                                <ItemTemplate><asp:Label ID="lblTotal" runat="server"></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" Visible="false" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-warning text-center fw-bold m-0">
                                ⚠ No stock balance entries found matching the criteria.
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
    </form>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$=txtpaymentdate]").datepicker({
                changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy'
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm) {
            prm.add_endRequest(function () {
                $("[id$=txtpaymentdate]").datepicker({
                    changeMonth: true, changeYear: true, dateFormat: 'dd/mm/yy'
                });
                bindEvents();
            });
        }
    </script>
</body>
</html>