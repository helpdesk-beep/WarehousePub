<%@ Page Title="Depositor & CropYear Wise Stock Position" Language="C#" MasterPageFile="~/MasterPages/FCI_Master_New.master" AutoEventWireup="true" CodeFile="~/FCINew/Depositor_Wise_CropYear_Wise_Commodity_Wise_Stock_Position.aspx.cs" Inherits="FCINew_Depositor_Wise_CropYear_Wise_Commodity_Wise_Stock_Position" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center !important; width: 100% !important; margin-bottom: 20px; }
        
        html body .container-fluid .report-panel table.table tr th, .table th {
            background: #2563eb !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 11px !important;
            border: 1px solid #1e40af !important; padding: 8px !important; white-space: normal !important;
        }
        .table td { vertical-align: middle !important; border: 1px solid #e2e8f0 !important; font-size: 11px !important; text-align: left !important; padding: 6px 8px !important; }
        
        html body .container-fluid .report-panel table.table tr td.text-right-align, .table td.text-right-align, .text-right-align { text-align: right !important; padding-right: 10px !important; white-space: nowrap !important; }
        html body .container-fluid .report-panel table.table tr td.text-center-align, .table td.text-center-align, .text-center-align { text-align: center !important; white-space: nowrap !important; }

        .report-panel { background: #fff; padding: 15px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); margin-top: 15px; }
        .filter-section { background: #f8fafc; border: 1px solid #e2e8f0; padding: 15px; border-radius: 5px; margin-bottom: 15px; }
        .grid-scroller-container { width: 100% !important; max-width: 100% !important; overflow-x: auto !important; display: block !important; border: 1px solid #e2e8f0; border-radius: 4px; }

        .multiselect-dropdown-wrapper { position: relative; display: inline-block; width: 100%; }
        .multiselect-select-box { background-color: #ffffff; border: 1px solid #ced4da; border-radius: 0.375rem; padding: 0.375rem 2.25rem 0.375rem 0.75rem; cursor: pointer; position: relative; user-select: none; font-size: 14px; color: #212529; }
        .multiselect-select-box::after { content: ""; position: absolute; right: 12px; top: 50%; transform: translateY(-50%); border: 5px solid transparent; border-top-color: #6c757d; }
        .multiselect-layer-container { display: none; position: absolute; top: 100%; left: 0; right: 0; z-index: 1050; background: #ffffff; border: 1px solid #dee2e6; border-radius: 0.375rem; box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15); max-height: 280px; overflow-y: auto; padding: 8px; }
        
        .multiselect-search-box { width: 100% !important; padding: 6px 10px !important; font-size: 13px !important; margin-bottom: 8px !important; border: 1px solid #cbd5e1 !important; border-radius: 4px !important; box-sizing: border-box !important; }
        .multiselect-search-box:focus { border-color: #3b82f6 !important; outline: none !important; box-shadow: 0 0 0 2px rgba(59, 130, 246, 0.2) !important; }

        .multiselect-layer-container table { width: 100%; }
        .multiselect-layer-container table label { margin-left: 8px !important; cursor: pointer !important; font-size: 13px !important; color: #334155 !important; display: inline-block; vertical-align: middle; }
        .multiselect-layer-container table input[type="checkbox"] { cursor: pointer !important; width: 15px; height: 15px; vertical-align: middle; }
        .multiselect-layer-container table tr { display: block; padding: 3px 0; }

        @media print {
            @page { size: landscape; margin: 5mm; }
            body *, html * { visibility: hidden; height: auto !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100%; background: none; padding: 0; margin: 0; zoom: 75%; }
            .btn-area, .page-header, .filter-section { display: none !important; }
            .print-header-block { display: block !important; text-align: center !important; width: 100% !important; }
            .print-header-block * { text-align: center !important; }
            .table { border-collapse: collapse !important; width: 100% !important; table-layout: auto !important; }
            thead { display: table-header-group !important; }
            tr { page-break-inside: avoid !important; }
            .grid-scroller-container { overflow-x: visible !important; border: none !important; display: inline !important; }
            html body #printZone .report-panel table.table thead tr th { background-color: #2563eb !important; color: #ffffff !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
        }
    </style>

    <script type="text/javascript">
        function toggleMenuContainer(containerId, searchInputId, e) {
            var menu = document.getElementById(containerId);
            var otherMenuId = (containerId === 'dropdownMenuContainer') ? 'dropdownDepositorContainer' : 'dropdownMenuContainer';
            document.getElementById(otherMenuId).style.display = 'none';

            if (menu.style.display === 'block') {
                menu.style.display = 'none';
            } else {
                menu.style.display = 'block';
                setTimeout(function () {
                    var searchInput = document.getElementById(searchInputId);
                    if (searchInput) searchInput.focus();
                }, 50);
            }
            e.stopPropagation();
        }

        function filterItemsLive(searchInputId, listClientId) {
            var input = document.getElementById(searchInputId);
            var filter = input.value.toLowerCase().trim();
            var cbl = document.getElementById(listClientId);
            if (!cbl) return;
            var rows = cbl.getElementsByTagName('tr');

            for (var i = 0; i < rows.length; i++) {
                var label = rows[i].getElementsByTagName('label')[0];
                if (label) {
                    var textValue = label.textContent || label.innerText;
                    if (i === 0 || textValue.toLowerCase().indexOf(filter) > -1) {
                        rows[i].style.display = "";
                    } else {
                        rows[i].style.display = "none";
                    }
                }
            }
        }

        function checkboxClickEngine(listClientId, uniqueId, index) {
            var cbl = document.getElementById(listClientId);
            var checkboxes = cbl.getElementsByTagName('input');

            if (index === 0) {
                if (checkboxes[0].checked) {
                    for (var i = 1; i < checkboxes.length; i++) { checkboxes[i].checked = false; }
                }
            } else {
                if (checkboxes[index].checked) { checkboxes[0].checked = false; }
            }

            var anyChecked = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) { anyChecked = true; break; }
            }
            if (!anyChecked) { checkboxes[0].checked = true; }

            updateCaptions();

            setTimeout(function () {
                __doPostBack(uniqueId, '');
            }, 100);
        }

        function updateCaptions() {
            processCaptionField('<%= chkCommodityList.ClientID %>', 'divDropdownSummaryCaption', '-- All Commodities --', 'Multiple Commodities');
            processCaptionField('<%= chkDepositorList.ClientID %>', 'divDepositorSummaryCaption', '-- All Depositors --', 'Multiple Depositors');
        }

        function processCaptionField(listId, captionId, defaultText, multiplePrefix) {
            var cbl = document.getElementById(listId);
            if (!cbl) return;
            var checkboxes = cbl.getElementsByTagName('input');
            var labels = cbl.getElementsByTagName('label');
            var selectedText = "";
            var count = 0;

            if (checkboxes.length > 0 && checkboxes[0].checked) {
                selectedText = defaultText;
            } else {
                for (var i = 1; i < checkboxes.length; i++) {
                    if (checkboxes[i].checked) {
                        count++;
                        if (selectedText === "") { selectedText = labels[i].innerHTML; }
                    }
                }
                if (count > 1) { selectedText = multiplePrefix + " (" + count + ")"; }
            }
            var captionDiv = document.getElementById(captionId);
            if (captionDiv) { captionDiv.innerHTML = selectedText; }
        }

        document.onclick = function (e) {
            var target = e.target;
            var menu1 = document.getElementById('dropdownMenuContainer');
            var cap1 = document.getElementById('divDropdownSummaryCaption');
            var src1 = document.getElementById('txtCommoditySearch');

            var menu2 = document.getElementById('dropdownDepositorContainer');
            var cap2 = document.getElementById('divDepositorSummaryCaption');
            var src2 = document.getElementById('txtDepositorSearch');

            if (menu1 && target !== menu1 && !menu1.contains(target) && target !== cap1 && target !== src1) { menu1.style.display = 'none'; }
            if (menu2 && target !== menu2 && !menu2.contains(target) && target !== cap2 && target !== src2) { menu2.style.display = 'none'; }
        };

        function registerFrameworkEvents() {
            var cblComm = document.getElementById('<%= chkCommodityList.ClientID %>');
            if (cblComm) {
                var chks = cblComm.getElementsByTagName('input');
                for (var i = 0; i < chks.length; i++) {
                    (function (idx) {
                        chks[idx].onclick = function () { checkboxClickEngine('<%= chkCommodityList.ClientID %>', '<%= chkCommodityList.UniqueID %>', idx); };
                    })(i);
                }
            }

            var cblDep = document.getElementById('<%= chkDepositorList.ClientID %>');
            if (cblDep) {
                var chksDep = cblDep.getElementsByTagName('input');
                for (var i = 0; i < chksDep.length; i++) {
                    (function (idx) {
                        chksDep[idx].onclick = function () { checkboxClickEngine('<%= chkDepositorList.ClientID %>', '<%= chkDepositorList.UniqueID %>', idx); };
                    })(i);
                }
            }
            updateCaptions();
        }

        window.onload = function () { registerFrameworkEvents(); };
        if (typeof (Sys) !== 'undefined' && typeof (Sys.WebForms) !== 'undefined') {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () { registerFrameworkEvents(); });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div id="printZone" class="container-fluid mt-3">
        <div class="page-header">Depositor Wise CropYear Wise Commodity Wise Stock Position</div>

        <div class="print-header-block">
            <h2 style="margin:0 auto; font-size:24px; font-weight:bold; color:#1e3a8a; text-align:center !important;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="margin:6px auto; font-size:16px; color:#475569; text-align:center !important;">Depositor Wise CropYear Wise Commodity Wise Stock Position Summary</h4>
            <p style="margin:5px auto 0 auto; font-size:11px; font-weight:bold; text-align:center !important; color:#334155;">Position As On: <asp:Label ID="lblPrintDate" runat="server"></asp:Label> | Filtered Scope: <asp:Label ID="lblPrintCommodity" runat="server" Text="All Commodities"></asp:Label> | Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
            <hr style="border:1px solid #1e3a8a; margin-top:10px; margin-bottom:20px; width:100%;"/>
        </div>

        <div class="filter-section">
            <div class="row align-items-end g-3">
                <div class="col-md-2">
                    <label class="fw-bold mb-1">Select Position Date:</label>
                    <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                
                <div class="col-md-3">
                    <label class="fw-bold mb-1">Select Depositors:</label>
                    <div class="multiselect-dropdown-wrapper">
                        <div id="divDepositorSummaryCaption" class="multiselect-select-box" onclick="toggleMenuContainer('dropdownDepositorContainer', 'txtDepositorSearch', event)">
                            -- All Depositors --
                        </div>
                        <div id="dropdownDepositorContainer" class="multiselect-layer-container">
                            <input type="text" id="txtDepositorSearch" placeholder="Search depositor..." class="multiselect-search-box" onkeyup="filterItemsLive('txtDepositorSearch', '<%= chkDepositorList.ClientID %>')" autocomplete="off" />
                            <asp:CheckBoxList ID="chkDepositorList" runat="server" RepeatLayout="Table" RepeatColumns="1" 
                                AutoPostBack="true" OnSelectedIndexChanged="chkDepositorList_SelectedIndexChanged">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>

                <div class="col-md-3">
                    <label class="fw-bold mb-1">Select Commodities:</label>
                    <div class="multiselect-dropdown-wrapper">
                        <div id="divDropdownSummaryCaption" class="multiselect-select-box" onclick="toggleMenuContainer('dropdownMenuContainer', 'txtCommoditySearch', event)">
                            -- All Commodities --
                        </div>
                        <div id="dropdownMenuContainer" class="multiselect-layer-container">
                            <input type="text" id="txtCommoditySearch" placeholder="Type name to filter..." class="multiselect-search-box" onkeyup="filterItemsLive('txtCommoditySearch', '<%= chkCommodityList.ClientID %>')" autocomplete="off" />
                            <asp:CheckBoxList ID="chkCommodityList" runat="server" RepeatLayout="Table" RepeatColumns="1" 
                                AutoPostBack="true" OnSelectedIndexChanged="chkCommodityList_SelectedIndexChanged">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>

                <div class="col-auto">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Style="background-color:#1e3a8a; border-color:#1e3a8a;" Text="Search Date" OnClick="btnSearch_Click" />
                </div>
                <div class="col text-end btn-area">
                    <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-secondary" Style="background-color: #475569; border-color: #475569;" Text="Print Document" OnClientClick="window.print(); return false;" />
                </div>
            </div>
        </div>

        <div class="report-panel">
            <div class="grid-scroller-container">
                <asp:GridView ID="gvStock" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered table-striped table-hover mb-0" ShowFooter="false"
                    EmptyDataText="No stock positions logged for specified parameters scope."
                    OnRowDataBound="gvStock_RowDataBound" OnDataBound="gvStock_DataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle Width="40px" CssClass="text-center-align" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Depositor Name">
                            <ItemTemplate>
                                <%# HttpUtility.HtmlEncode(Eval("depositor_Name") ?? "") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Commodity Name">
                            <ItemTemplate>
                                <%# HttpUtility.HtmlEncode(Eval("Commodity") ?? "") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="2016-17" HeaderText="2016-17" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="2017-18" HeaderText="2017-18" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="2018-19" HeaderText="2018-19" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="2019-20" HeaderText="2019-20" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="2020-21" HeaderText="2020-21" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="2021-22" HeaderText="2021-22" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="2022-23" HeaderText="2022-23" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="2023-24" HeaderText="2023-24" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="2024-25" HeaderText="2024-25" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="2025-26" HeaderText="2025-26" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="2026-27" HeaderText="2026-27" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        
                        <asp:BoundField DataField="Total" HeaderText="Total Qty (MT)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" ItemStyle-Font-Bold="true" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>