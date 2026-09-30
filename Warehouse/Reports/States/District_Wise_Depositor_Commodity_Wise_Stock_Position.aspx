<%@ Page Language="C#" AutoEventWireup="true" CodeFile="District_Wise_Depositor_Commodity_Wise_Stock_Position.aspx.cs" Inherits="Reports_States_District_Wise_Depositor_Commodity_Wise_Stock_Position" EnableEventValidation="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stock Position Report</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center !important; width: 100% !important; margin-bottom: 5px !important; }
        
        .grid-view { font-family: 'Segoe UI', Arial, sans-serif; border-collapse: collapse; width: 100%; background-color: #ffffff; box-shadow: 0 2px 5px rgba(0,0,0,0.1); }
        
        html body form table.grid-view tr th, table.grid-view thead tr th, .grid-view th {
            background-color: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 11px !important;
            border: 1px solid #172554 !important; padding: 10px 6px !important; text-transform: uppercase;
        }
        .grid-view td { padding: 8px 10px; border: 1px solid #e0e0e0; font-size: 12px; color: #333333; vertical-align: middle !important; }
        .grid-view tr:nth-child(even) { background-color: #f9f9f9; }
        
        .text-right-align { text-align: right !important; padding-right: 12px !important; white-space: nowrap !important; }
        .text-center-align { text-align: center !important; white-space: nowrap !important; }

        .report-panel { background: #fff; padding: 20px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); margin-top: 15px; }
        .filter-section { background: #f8fafc; border: 1px solid #e2e8f0; padding: 20px; border-radius: 5px; margin-bottom: 15px; }
        .grid-scroller-container { width: 100% !important; max-width: 100% !important; overflow-x: auto !important; display: block !important; border: 1px solid #e2e8f0; border-radius: 4px; }
        .custom-label { font-size: 13px; color: #1e3a8a; }

        /* ADVANCED DROPDOWN MULTISELECT SYSTEM COMPONENTS */
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
            position: relative;
            user-select: none;
            font-size: 14px;
            color: #212529;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
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
            max-height: 280px;
            overflow-y: auto;
            padding: 8px;
        }
        .multiselect-search-box {
            width: 100% !important;
            padding: 6px 10px !important;
            font-size: 13px !important;
            margin-bottom: 8px !important;
            border: 1px solid #cbd5e1 !important;
            border-radius: 4px !important;
            box-sizing: border-box !important;
        }
        .multiselect-search-box:focus {
            border-color: #3b82f6 !important;
            outline: none !important;
            box-shadow: 0 0 0 2px rgba(59, 130, 246, 0.2) !important;
        }
        .multiselect-layer-container table { width: 100%; }
        .multiselect-layer-container table label {
            margin-left: 8px !important;
            cursor: pointer !important;
            font-size: 13px !important;
            color: #334155 !important;
            display: inline-block;
            vertical-align: middle;
        }
        .multiselect-layer-container table input[type="checkbox"] {
            cursor: pointer !important;
            width: 15px;
            height: 15px;
            vertical-align: middle;
        }
        .multiselect-layer-container table tr { display: block; padding: 3px 0; }

        @media print {
            @page { size: landscape; margin: 5mm; }
            body *, html * { visibility: hidden; height: auto !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100%; background: none; padding: 0 !important; margin: 0 !important; zoom: 70%; }
            .btn-area, .page-header, .filter-section { display: none !important; }
            
            .print-header-block { display: block !important; text-align: center !important; width: 100% !important; margin-bottom: 5px !important; }
            .print-header-block h2 { margin: 0 auto !important; color: #1e3a8a !important; font-weight: bold; }
            .print-header-block h4 { margin: 4px auto !important; color: #475569 !important; }
            .print-header-block hr { border: 1px solid #1e3a8a !important; margin-top: 5px !important; margin-bottom: 10px !important; opacity: 1 !important; }
            
            .report-panel { box-shadow: none !important; padding: 0 !important; border: none !important; }
            .grid-view { border-collapse: collapse !important; width: 100% !important; margin-top: 0px !important; }
            
            thead { display: table-header-group !important; }
            tr { page-break-inside: avoid !important; }
            .grid-scroller-container { overflow-x: visible !important; border: none !important; display: inline !important; }
            .grid-view th { background-color: #1e3a8a !important; color: #ffffff !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
        }
    </style>

    <script type="text/javascript">
        // Open or Close Panels smoothly
        function toggleDropdownPanel(panelId, searchInputId, e) {
            var menu = document.getElementById(panelId);
            var isAlreadyOpen = (menu.style.display === 'block');

            closeAllDropdownPanels();

            if (!isAlreadyOpen) {
                menu.style.display = 'block';
                setTimeout(function () {
                    var searchInput = document.getElementById(searchInputId);
                    if (searchInput) searchInput.focus();
                }, 50);
            }
            e.stopPropagation();
        }

        function closeAllDropdownPanels() {
            var menus = ['divDepositorMenu', 'divCommodityMenu'];
            menus.forEach(function (id) {
                var element = document.getElementById(id);
                if (element) element.style.display = 'none';
            });
        }

        // Live filtration engine
        function filterItemsLive(inputId, cblClientId) {
            var input = document.getElementById(inputId);
            var filter = input.value.toLowerCase().trim();
            var cbl = document.getElementById(cblClientId);
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

        // HIGH PERFORMANCE MUTUAL EXCLUSION TOGGLE RULES (ALL vs INDIVIDUAL SELECTION)
        function handleCheckboxLogic(cblClientId, selectedIndex) {
            var cbl = document.getElementById(cblClientId);
            var checkboxes = cbl.getElementsByTagName('input');

            if (selectedIndex === 0) {
                // Agar "ALL" select kiya, toh baaki saare ddeselect kardo
                if (checkboxes[0].checked) {
                    for (var i = 1; i < checkboxes.length; i++) {
                        checkboxes[i].checked = false;
                    }
                }
            } else {
                // Agar koi specific option select kiya, toh "ALL" ko automatic uncheck kardo
                if (checkboxes[selectedIndex].checked) {
                    checkboxes[0].checked = false;
                }
            }

            // Fallback: Agar saare ddeselect ho gaye, toh dynamic select back "ALL" auto-trigger
            var anyChecked = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) { anyChecked = true; break; }
            }
            if (!anyChecked) {
                checkboxes[0].checked = true;
            }
        }

        // Dynamic text title manager
        function updateSelectBoxCaption(cblClientId, captionId, defaultText, singularPrefix) {
            var cbl = document.getElementById(cblClientId);
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
                        if (selectedText === "") {
                            selectedText = labels[i].textContent || labels[i].innerText;
                        }
                    }
                }
                if (count > 1) {
                    selectedText = "Multiple " + singularPrefix + " (" + count + ")";
                }
            }

            var captionDiv = document.getElementById(captionId);
            if (captionDiv) {
                captionDiv.innerHTML = (selectedText === "") ? defaultText : selectedText;
            }
        }

        document.onclick = function (e) {
            if (!e.target.closest('.multiselect-dropdown-wrapper')) {
                closeAllDropdownPanels();
            }
        };

        // Attach event listeners during runtime compilation
        function bindControlsRuntimeEvents() {
            var depCblId = '<%= cblDepositor.ClientID %>';
            var comCblId = '<%= cblCommodity.ClientID %>';

            // Depositor List Box Event hooks
            var depCbl = document.getElementById(depCblId);
            if (depCbl) {
                var chks = depCbl.getElementsByTagName('input');
                for (var i = 0; i < chks.length; i++) {
                    (function (index) {
                        chks[index].onclick = function () {
                            handleCheckboxLogic(depCblId, index);
                            updateSelectBoxCaption(depCblId, 'lblDepositorCaption', 'ALL DEPOSITORS', 'Depositors');
                        };
                    })(i);
                }
            }

            // Commodity List Box Event hooks
            var comCbl = document.getElementById(comCblId);
            if (comCbl) {
                var chks = comCbl.getElementsByTagName('input');
                for (var i = 0; i < chks.length; i++) {
                    (function (index) {
                        chks[index].onclick = function () {
                            handleCheckboxLogic(comCblId, index);
                            updateSelectBoxCaption(comCblId, 'lblCommodityCaption', 'ALL COMMODITIES', 'Commodities');
                        };
                    })(i);
                }
            }

            updateSelectBoxCaption(depCblId, 'lblDepositorCaption', 'ALL DEPOSITORS', 'Depositors');
            updateSelectBoxCaption(comCblId, 'lblCommodityCaption', 'ALL COMMODITIES', 'Commodities');
        }

        window.onload = function () {
            bindControlsRuntimeEvents();
        };

        if (typeof (Sys) !== 'undefined' && typeof (Sys.WebForms) !== 'undefined') {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                bindControlsRuntimeEvents();
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="printZone" class="container-fluid mt-3">
            <div class="page-header">District, Depositor, Commodity Wise Stock Position Report in MT</div>

            <div class="print-header-block">
                <h2>M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
                <h4>District, Depositor, Commodity Wise Stock Position Summary (Qty in MT)</h4>
                <p style="margin: 3px auto; font-size: 12px; font-weight: bold; color: #334155;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
                <hr />
            </div>

            <div class="filter-section">
                <div class="row align-items-end g-3">
                    <div class="col-md-2 mb-1">
                        <label class="fw-bold custom-label mb-1">As On Date:</label>
                        <asp:TextBox ID="txtpaymentdate" runat="server" type="date" CssClass="form-control"></asp:TextBox>
                    </div>
                    
                    <div class="col-md-3">
                        <label class="fw-bold custom-label mb-1">Select Depositors:</label>
                        <div class="multiselect-dropdown-wrapper">
                            <div id="lblDepositorCaption" class="multiselect-select-box" onclick="toggleDropdownPanel('divDepositorMenu', 'txtDepSearch', event)">
                                ALL DEPOSITORS
                            </div>
                            <div id="divDepositorMenu" class="multiselect-layer-container">
                                <input type="text" id="txtDepSearch" placeholder="Search depositor..." class="multiselect-search-box" onkeyup="filterItemsLive('txtDepSearch', '<%= cblDepositor.ClientID %>')" autocomplete="off" />
                                <asp:CheckBoxList ID="cblDepositor" runat="server" RepeatLayout="Table" RepeatColumns="1"></asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-md-3">
                        <label class="fw-bold custom-label mb-1">Select Commodities:</label>
                        <div class="multiselect-dropdown-wrapper">
                            <div id="lblCommodityCaption" class="multiselect-select-box" onclick="toggleDropdownPanel('divCommodityMenu', 'txtComSearch', event)">
                                ALL COMMODITIES
                            </div>
                            <div id="divCommodityMenu" class="multiselect-layer-container">
                                <input type="text" id="txtComSearch" placeholder="Search commodity..." class="multiselect-search-box" onkeyup="filterItemsLive('txtComSearch', '<%= cblCommodity.ClientID %>')" autocomplete="off" />
                                <asp:CheckBoxList ID="cblCommodity" runat="server" RepeatLayout="Table" RepeatColumns="1"></asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-md-4 text-md-end text-start mb-1">
                        <asp:Button ID="btnSearch" CssClass="btn btn-primary btn-sm px-3 me-1" runat="server" Text="Search Statement" OnClick="btnSearch_Click" Style="background-color:#1e3a8a; border-color:#1e3a8a;" />
                        <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success btn-sm px-3 me-1" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-secondary btn-sm px-3" Style="background-color: #475569; border-color: #475569;" Text="Print Report" OnClientClick="window.print(); return false;" />
                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col-12 text-center">
                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="report-panel">
                <div class="grid-scroller-container">
                    <asp:GridView ID="GV_StockPositionDetails" runat="server" AutoGenerateColumns="False" 
                        CssClass="grid-view table table-bordered mb-0" ShowFooter="false"
                        OnRowDataBound="GV_StockPositionDetails_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50px" CssClass="text-center-align" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Region" HeaderText="Region" />
                            <asp:BoundField DataField="District" HeaderText="District Name" />
                            <asp:BoundField DataField="CommodityName" HeaderText="Commodity" />
                            
                            <asp:BoundField DataField="2017-18" HeaderText="2017-18" DataFormatString="{0:F2}" ItemStyle-CssClass="text-right-align" />
                            <asp:BoundField DataField="2018-19" HeaderText="2018-19" DataFormatString="{0:F2}" ItemStyle-CssClass="text-right-align" />
                            <asp:BoundField DataField="2019-20" HeaderText="2019-20" DataFormatString="{0:F2}" ItemStyle-CssClass="text-right-align" />
                            <asp:BoundField DataField="2020-21" HeaderText="2020-21" DataFormatString="{0:F2}" ItemStyle-CssClass="text-right-align" />
                            <asp:BoundField DataField="2021-22" HeaderText="2021-22" DataFormatString="{0:F2}" ItemStyle-CssClass="text-right-align" />
                            <asp:BoundField DataField="2022-23" HeaderText="2022-23" DataFormatString="{0:F2}" ItemStyle-CssClass="text-right-align" />
                            <asp:BoundField DataField="2023-24" HeaderText="2023-24" DataFormatString="{0:F2}" ItemStyle-CssClass="text-right-align" />
                            <asp:BoundField DataField="2024-25" HeaderText="2024-25" DataFormatString="{0:F2}" ItemStyle-CssClass="text-right-align" />
                            <asp:BoundField DataField="2025-26" HeaderText="2025-26" DataFormatString="{0:F2}" ItemStyle-CssClass="text-right-align" />
                            <asp:BoundField DataField="2026-27" HeaderText="2026-27" DataFormatString="{0:F2}" ItemStyle-CssClass="text-right-align" />
                            
                            <asp:BoundField DataField="Total" HeaderText="Total Qty (MT)" DataFormatString="{0:F2}" ItemStyle-CssClass="text-right-align" ItemStyle-Font-Bold="true" />
                            <asp:BoundField DataField="RowTypeMarker" HeaderText="Marker" Visible="false" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>