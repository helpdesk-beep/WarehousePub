<%@ Page Title="Region-wise Report of Commodity" Language="C#" MasterPageFile="~/MasterPages/FCI_Master_New.master" AutoEventWireup="true" CodeFile="~/FCINew/Rpt_District_Wise_Qty_Available_Last_10_Year_Date_Wise_For_FCI.aspx.cs" Inherits="FCINew_Rpt_District_Wise_Qty_Available_Last_10_Year_Date_Wise_For_FCI" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .print-header-block { display: none; text-align: center !important; width: 100% !important; margin-bottom: 20px; }
        
        fieldset { border: 1px solid #2095A1; padding: 0.35em 0.625em 0.75em; margin: 10px; border-radius: 5px; padding-left: 20px; background: #fff; }
        legend { padding: 2px 8px; border-radius: 10px; width: auto; border: 1px solid #2095A1; font-size: 17px; font-weight: bold; color: #030203; }
        
        html body .container-fluid .report-panel table.table tr th, .table th, th.sorting {
            background: #2563eb !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 11px !important;
            border: 1px solid #1e40af !important; padding: 8px !important; white-space: normal !important;
        }
        .table td { vertical-align: middle !important; border: 1px solid #cbd5e1 !important; font-size: 11px !important; text-align: left !important; padding: 6px 8px !important; color: black !important; }
        
        .text-right-align { text-align: right !important; padding-right: 10px !important; white-space: nowrap !important; }
        .text-center-align { text-align: center !important; white-space: nowrap !important; }

        .button { border: none; color: white; padding: 6px 14px; text-align: center; text-decoration: none; display: inline-block; font-size: 12px; font-weight: bold; margin: 4px 2px; transition-duration: 0.4s; cursor: pointer; border-radius: 4px; }
        .button2 { background-color: white; color: black; border: 2px solid #008CBA; }
        .button2:hover { background-color: #008CBA; color: white; }

        /* MULTI-SELECT DROPDOWN STYLES */
        .multiselect-dropdown-wrapper { position: relative; display: inline-block; width: 100%; }
        .multiselect-select-box { background-color: #ffffff; border: 1px solid #ced4da; border-radius: 0.375rem; padding: 0.375rem 2.25rem 0.375rem 0.75rem; cursor: pointer; position: relative; user-select: none; font-size: 14px; color: #212529; height: 38px; display: flex; align-items: center; }
        .multiselect-select-box::after { content: ""; position: absolute; right: 12px; top: 50%; transform: translateY(-50%); border: 5px solid transparent; border-top-color: #6c757d; }
        .multiselect-layer-container { display: none; position: absolute; top: 100%; left: 0; right: 0; z-index: 1050; background: #ffffff; border: 1px solid #dee2e6; border-radius: 0.375rem; box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15); max-height: 280px; overflow-y: auto; padding: 8px; }
        .multiselect-search-box { width: 100% !important; padding: 6px 10px !important; font-size: 13px !important; margin-bottom: 8px !important; border: 1px solid #cbd5e1 !important; border-radius: 4px !important; box-sizing: border-box !important; }
        .multiselect-layer-container table { width: 100%; }
        .multiselect-layer-container table label { margin-left: 8px !important; cursor: pointer !important; font-size: 13px !important; color: #334155 !important; display: inline-block; vertical-align: middle; margin-bottom: 0; }
        .multiselect-layer-container table input[type="checkbox"] { cursor: pointer !important; width: 15px; height: 15px; vertical-align: middle; }
        .multiselect-layer-container table tr { display: block; padding: 4px 0; }

        @media print {
            @page { size: landscape; margin: 8mm 5mm 5mm 5mm; }
            body, html { background: #ffffff !important; color: #000000 !important; }
            .main-header, #sidebar, #sidebarToggle, #loader-overlay { display: none !important; width: 0 !important; height: 0 !important; }
            #page-wrapper { margin-left: 0 !important; padding: 0 !important; }
            .button, .filter-row { display: none !important; }
            legend { display: none !important; }
            fieldset { border: none !important; padding: 0 !important; margin: 0 !important; }
            .print-header-block { display: block !important; text-align: center !important; width: 100% !important; }
            #printZone { position: absolute; left: 0; top: 0; width: 100%; padding: 0; margin: 0; }
            .table-responsive { overflow-x: visible !important; border: none !important; }
            .table { border-collapse: collapse !important; width: 100% !important; }
            html body #printZone table thead tr th { background-color: #1e3a8a !important; color: #ffffff !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
        }
    </style>

    <script type="text/javascript">
        function toggleMenuContainer(containerId, searchInputId, e) {
            var menu = document.getElementById(containerId);
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

        // NO POSTBACK ENGINE - Only updates UI text state locally on choice selection
        function checkboxClickEngine(listClientId, index) {
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
        }

        function updateCaptions() {
            var cbl = document.getElementById('<%= chkCommodityList.ClientID %>');
            if (!cbl) return;
            var checkboxes = cbl.getElementsByTagName('input');
            var labels = cbl.getElementsByTagName('label');
            var selectedText = "";
            var count = 0;

            if (checkboxes.length > 0 && checkboxes[0].checked) {
                selectedText = "-- All Commodities --";
            } else {
                for (var i = 1; i < checkboxes.length; i++) {
                    if (checkboxes[i].checked) {
                        count++;
                        if (selectedText === "") { selectedText = labels[i].innerHTML; }
                    }
                }
                if (count > 1) { selectedText = "Multiple Commodities (" + count + ")"; }
            }
            var captionDiv = document.getElementById('divDropdownSummaryCaption');
            if (captionDiv) { captionDiv.innerHTML = selectedText; }
        }

        document.onclick = function (e) {
            var target = e.target;
            var menu = document.getElementById('dropdownMenuContainer');
            var cap = document.getElementById('divDropdownSummaryCaption');
            var src = document.getElementById('txtCommoditySearch');
            if (menu && target !== menu && !menu.contains(target) && target !== cap && target !== src) { menu.style.display = 'none'; }
        };

        function registerFrameworkEvents() {
            var cblComm = document.getElementById('<%= chkCommodityList.ClientID %>');
            if (cblComm) {
                var chks = cblComm.getElementsByTagName('input');
                for (var i = 0; i < chks.length; i++) {
                    (function (idx) {
                        chks[idx].onclick = function () { checkboxClickEngine('<%= chkCommodityList.ClientID %>', idx); };
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
        <fieldset>
            <legend align="center">District, Commodity, Date Wise Stock Position <label style="color: red">(In M.T.)</label></legend>
            
            <div class="print-header-block">
                <h2 style="margin:0 auto; font-size:24px; font-weight:bold; color:#1e3a8a; text-align:center !important;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
                <h4 style="margin:6px auto; font-size:16px; color:#475569; text-align:center !important;">District, Commodity & Date Wise Stock Position Summary</h4>
                <p style="margin:5px auto 0 auto; font-size:11px; font-weight:bold; text-align:center !important; color:#334155;">Position As On: <asp:Label ID="lblPrintDate" runat="server"></asp:Label> | Commodity: <asp:Label ID="lblPrintCommodity" runat="server"></asp:Label> | Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
                <hr style="border:1px solid #1e3a8a; margin-top:10px; margin-bottom:20px; width:100%;"/>
            </div>

            <div class="row mb-3">
                <div class="col-md-12">
                    <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="window.print(); return false;" />
                    <asp:Button ID="btnExcel" runat="server" Text="Exporttoexcel" CssClass="button button2" OnClick="btnExcel_Click" OnClientClick="setTimeout(function(){ if(typeof toggleGlobalLoader === 'function') toggleGlobalLoader(false); }, 300);" />
                </div>
            </div>

            <div class="row filter-row align-items-center mb-4" style="font-size: large;">
                <div class="col-md-2 text-md-right">
                    <asp:Label ID="lblDistrict" runat="server" Font-Size="10pt" Font-Bold="true">Date (DD-MM-YYYY) :</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtpaymentdate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 text-md-right">
                    <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="true">Commodity :</asp:Label>
                </div>
                <div class="col-md-4">
                    <div class="multiselect-dropdown-wrapper">
                        <div id="divDropdownSummaryCaption" class="multiselect-select-box" onclick="toggleMenuContainer('dropdownMenuContainer', 'txtCommoditySearch', event)">
                            -- All Commodities --
                        </div>
                        <div id="dropdownMenuContainer" class="multiselect-layer-container">
                            <input type="text" id="txtCommoditySearch" placeholder="Type name to filter..." class="multiselect-search-box" onkeyup="filterItemsLive('txtCommoditySearch', '<%= chkCommodityList.ClientID %>')" autocomplete="off" />
                            <%-- REMOVED AutoPostBack AND OnSelectedIndexChanged FOR CLEAN LOCAL SELECTION --%>
                            <asp:CheckBoxList ID="chkCommodityList" runat="server" RepeatLayout="Table" RepeatColumns="1">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnshow" Text="Show Details" runat="server" CssClass="button button2" OnClick="btnshow_Click" />
                </div>
            </div>

            <div class="row">
                <div class="table-responsive p-2">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" ShowFooter="true"
                        CssClass="table table-bordered table-striped table-hover mb-0" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" 
                        OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                <ItemStyle Width="50px" CssClass="text-center-align" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                    </asp:GridView>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>