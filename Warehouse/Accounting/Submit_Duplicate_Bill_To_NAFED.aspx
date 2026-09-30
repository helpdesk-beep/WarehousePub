<%@ Page Title="Submit Duplicate Bill To NAFED" Language="C#" MasterPageFile="~/MasterPage/StateMasterNafed_Marketing.master" AutoEventWireup="true" CodeFile="~/Accounting/Submit_Duplicate_Bill_To_NAFED.aspx.cs" Inherits="Accounting_Submit_Duplicate_Bill_To_NAFED" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        body {
            background-color: #f8fafc;
            font-family: 'Segoe UI', Arial, sans-serif;
        }

        .page-header {
            background: #1e3a8a;
            color: white !important;
            padding: 12px;
            border-radius: 5px;
            margin-bottom: 15px;
            text-align: center;
            font-size: 22px;
            font-weight: bold;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }

        .print-header-block {
            display: none;
            text-align: center;
            margin-bottom: 20px;
            width: 100% !important;
        }

        /* Header Style with Proper Wrapping & Fixed Boundaries */
        html body .container-fluid .report-panel table.table tr th,
        table.table thead tr th,
        .table th {
            background-color: #2563eb !important;
            color: #ffffff !important;
            font-weight: bold !important;
            text-align: center !important;
            vertical-align: middle !important;
            font-size: 11px !important;
            border: 1px solid #1e40af !important;
            padding: 8px 4px !important;
            white-space: normal !important;
            word-wrap: break-word !important;
            word-break: break-word !important;
            line-height: 1.2 !important;
            /* Browser ko background color force print karne ke liye */
            -webkit-print-color-adjust: exact !important;
            print-color-adjust: exact !important;
        }

        /* Data Cell Formatting */
        .table td {
            vertical-align: middle !important;
            border: 1px solid #cbd5e1 !important;
            font-size: 11px !important;
            text-align: left !important;
            padding: 6px 6px !important;
            white-space: normal !important;
            word-wrap: break-word !important;
            word-break: break-word !important;
        }

        .text-right-align {
            text-align: right !important;
            padding-right: 6px !important;
        }

        .text-center-align {
            text-align: center !important;
        }

        .footer-style td {
            background-color: #eff6ff !important;
            color: #1e3a8a !important;
            font-weight: bold !important;
            font-size: 11px !important;
            border: 1px solid #cbd5e1 !important;
            padding: 8px 6px !important;
            white-space: normal !important;
            word-wrap: break-word !important;
            -webkit-print-color-adjust: exact !important;
            print-color-adjust: exact !important;
        }

        .report-panel {
            background: #fff;
            padding: 15px;
            border-radius: 5px;
            box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1);
            margin-bottom: 25px;
        }

        .controls-area {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 15px;
            flex-wrap: wrap;
            gap: 10px;
        }

        .search-box-container {
            flex: 1;
            max-width: 350px;
        }

        .search-input {
            width: 100%;
            height: 38px;
            padding: 6px 12px;
            font-size: 13px;
            border: 1px solid #cbd5e1;
            border-radius: 4px;
            outline: none;
        }

        .grid-scroller-container {
            width: 100% !important;
            max-width: 100% !important;
            overflow-x: auto !important;
            margin-bottom: 15px !important;
            border: 1px solid #e2e8f0 !important;
            border-radius: 4px !important;
            display: block !important;
        }

        .action-footer-area {
            display: none;
            margin-top: 15px;
            text-align: right;
            padding: 10px;
            background-color: #f1f5f9;
            border-radius: 5px;
            border: 1px solid #cbd5e1;
        }

        /* Perfectly Optimized Print Stylesheet */
        @media print {
            @page {
                size: landscape;
                margin: 6mm 4mm 6mm 4mm;
            }

            /* Enable Color Printing for WebKit / Blink & Firefox */
            * {
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
                color-adjust: exact !important;
            }

            body *, html * {
                visibility: hidden;
            }

            #printZone, #printZone * {
                visibility: visible;
            }

            #printZone {
                position: absolute;
                left: 0;
                top: 0;
                width: 100% !important;
                margin: 0 !important;
                padding: 0 !important;
            }

            .controls-area, .no-print, .search-box-container, .page-header, .chk-col, .action-footer-area {
                display: none !important;
            }

            .print-header-block {
                display: block !important;
                width: 100% !important;
            }

            .report-panel {
                box-shadow: none !important;
                padding: 0 !important;
                border: none !important;
                background: transparent !important;
            }

            .grid-scroller-container {
                overflow: visible !important;
                border: none !important;
            }

            .table {
                border-collapse: collapse !important;
                width: 100% !important;
                table-layout: auto !important;
            }

                .table th {
                    background-color: #2563eb !important;
                    color: #ffffff !important;
                    font-size: 8.5px !important;
                    padding: 4px 2px !important;
                    border: 1px solid #1e40af !important;
                }

                .table td {
                    font-size: 8.5px !important;
                    padding: 4px 2px !important;
                    border: 1px solid #cbd5e1 !important;
                    color: #000000 !important;
                }

            .footer-style td {
                background-color: #eff6ff !important;
                color: #1e3a8a !important;
                font-size: 8.5px !important;
                padding: 4px 2px !important;
            }
        }
    </style>

    <script type="text/javascript">
        function toggleSelectAll(headerChk) {
            var grid = document.getElementById('<%= gvDuplicateBills.ClientID %>');
            if (!grid) return;
            var checkboxes = grid.getElementsByTagName('input');
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].type === 'checkbox' && checkboxes[i] !== headerChk) {
                    checkboxes[i].checked = headerChk.checked;
                }
            }
            toggleApproveButton();
        }

        function toggleApproveButton() {
            var grid = document.getElementById('<%= gvDuplicateBills.ClientID %>');
            if (!grid) return;
            var checkboxes = grid.getElementsByTagName('input');
            var isAnyChecked = false;

            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].type === 'checkbox' && checkboxes[i].id.indexOf('chkSelectHeader') === -1) {
                    if (checkboxes[i].checked) {
                        isAnyChecked = true;
                        break;
                    }
                }
            }

            var approveArea = document.getElementById('btnApproveArea');
            if (approveArea) {
                approveArea.style.display = isAnyChecked ? 'block' : 'none';
            }
        }

        function filterGrid() {
            var input = document.getElementById('txtSearch');
            var filter = input.value.toLowerCase().trim();
            var grid = document.getElementById('<%= gvDuplicateBills.ClientID %>');
            if (!grid) return;

            var rows = grid.getElementsByTagName('tr');
            for (var i = 1; i < rows.length; i++) {
                var row = rows[i];
                if (row.parentNode.tagName.toLowerCase() === 'thead' || row.parentNode.tagName.toLowerCase() === 'tfoot') continue;

                var textContent = row.textContent || row.innerText;
                row.style.display = (textContent.toLowerCase().indexOf(filter) > -1) ? '' : 'none';
            }
        }
    </script>

    <div class="page-header">Submit Duplicate Bill To NAFED</div>

    <div id="printZone" class="container-fluid mt-3">

        <div class="print-header-block">
            <h2 style="margin: 0 auto; font-size: 18px; font-weight: bold; color: #1e3a8a; text-align: center;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="margin: 4px auto; font-size: 13px; color: #475569; text-align: center;">Duplicate Storage Bill Details Submitted To NAFED</h4>
            <p style="margin: 4px auto 0 auto; font-size: 10px; font-weight: bold; color: #334155; text-align: center;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
            <hr style="border: 1px solid #1e3a8a; margin-top: 8px; margin-bottom: 12px; width: 100%;" />
        </div>

        <div class="report-panel">

            <div class="controls-area no-print">
                <div class="search-box-container">
                    <input type="text" id="txtSearch" class="search-input" onkeyup="filterGrid()" placeholder="🔍 Type to search district, godown, bill no..." autocomplete="off" />
                </div>
                <div class="btn-area">
                    <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; color: white; border: none; padding: 6px 14px; cursor: pointer; border-radius: 4px; font-weight: bold; font-size: 12px;" Text="Export To Excel" OnClick="btnExcel_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; color: white; border: none; padding: 6px 14px; cursor: pointer; border-radius: 4px; font-weight: bold; font-size: 12px; margin-left: 5px;" Text="Print Report" OnClientClick="window.print(); return false;" />
                </div>
            </div>

            <div class="grid-scroller-container">
                <asp:GridView ID="gvDuplicateBills" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered table-striped table-hover mb-0"
                    Style="width: 100%; word-wrap: break-word;"
                    ShowFooter="true" FooterStyle-CssClass="footer-style" DataKeyNames="New_Bill"
                    EmptyDataText="No records found for duplicate NAFED bills." OnRowDataBound="gvDuplicateBills_RowDataBound" OnRowCommand="GrdBills_RowCommand">
                    <Columns>

                        <%-- Index 0: Checkbox Column --%>
                        <asp:TemplateField ItemStyle-CssClass="chk-col text-center-align" HeaderStyle-CssClass="chk-col">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectHeader" runat="server" onclick="toggleSelectAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelectRow" runat="server" onclick="toggleApproveButton();" />
                            </ItemTemplate>
                            <HeaderStyle Width="25px" HorizontalAlign="Center" />
                            <ItemStyle Width="25px" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <%-- Index 1: S.No. --%>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle Width="35px" CssClass="text-center-align" />
                            <FooterStyle CssClass="text-center-align" />
                        </asp:TemplateField>

                        <%-- Index 2 to 10: Descriptor / Text Columns --%>
                        <asp:BoundField DataField="District_Name" HeaderText="District Name" NullDisplayText="N/A" HeaderStyle-Width="85px" ItemStyle-Width="85px" />
                        <asp:BoundField DataField="DepotName" HeaderText="Depot Name" NullDisplayText="N/A" HeaderStyle-Width="85px" ItemStyle-Width="85px" />
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" NullDisplayText="N/A" HeaderStyle-Width="120px" ItemStyle-Width="120px" />
                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" NullDisplayText="N/A" HeaderStyle-Width="75px" ItemStyle-Width="75px" />
                        <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" HeaderStyle-Width="55px" ItemStyle-Width="55px" ItemStyle-CssClass="text-center-align" />
                        <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" HeaderStyle-Width="55px" ItemStyle-Width="55px" ItemStyle-CssClass="text-center-align" />
                        <asp:BoundField DataField="Month" HeaderText="Month" HeaderStyle-Width="60px" ItemStyle-Width="60px" ItemStyle-CssClass="text-center-align" />
                        <%--<asp:BoundField DataField="New_Bill" HeaderText="New Bill No." HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-CssClass="text-center-align" />--%>
                        <asp:TemplateField HeaderText="New Bill No." HeaderStyle-Width="120px" ItemStyle-Width="120px" ItemStyle-CssClass="text-center-align">
                            <ItemTemplate>
                                <asp:Label ID="lblBill_Number" runat="server" Text='<%# Eval("New_Bill") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="Old_Bill" HeaderText="Old Bill No." HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-CssClass="text-center-align" />--%>
                        <asp:TemplateField HeaderText="Old Bill No." HeaderStyle-Width="120px" ItemStyle-Width="120px" ItemStyle-CssClass="text-center-align">
                            <ItemTemplate>
                                <asp:Label ID="lblOld_Bill_Number" runat="server" Text='<%# Eval("Old_Bill") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- Index 11 to 17: Amount & Balance Columns --%>
                        <asp:BoundField DataField="New_Closing_Balance" HeaderText="New Bill Closing Balance" DataFormatString="{0:N2}" HeaderStyle-Width="85px" ItemStyle-Width="85px" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="New_Bill_Amount" HeaderText="New Bill Amount" DataFormatString="{0:N2}" HeaderStyle-Width="85px" ItemStyle-Width="85px" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="OLD_Closing_Balance" HeaderText="Old Bill Closing Balance" DataFormatString="{0:N2}" HeaderStyle-Width="85px" ItemStyle-Width="85px" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Old_Bill_Amount" HeaderText="Old Bill Amount" DataFormatString="{0:N2}" HeaderStyle-Width="85px" ItemStyle-Width="85px" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Difference_Amount" HeaderText="Difference Amount" DataFormatString="{0:N2}" HeaderStyle-Width="85px" ItemStyle-Width="85px" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Nafed_Received_Amount" HeaderText="NAFED Rec. Amount" DataFormatString="{0:N2}" HeaderStyle-Width="75px" ItemStyle-Width="75px" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <%-- Index 18: Date Column --%>
                        <asp:BoundField DataField="Nafed_Payment_Date" HeaderText="NAFED Payment Date" DataFormatString="{0:dd/MM/yyyy}" NullDisplayText="N/A" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-CssClass="text-center-align" />


                        <asp:BoundField DataField="Remaining_Amount" HeaderText="Remaining Amount" DataFormatString="{0:N2}" HeaderStyle-Width="85px" ItemStyle-Width="85px" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />

                        <%--                        <asp:TemplateField HeaderText="Print Bill" HeaderStyle-Width="70px" ItemStyle-Width="70px" ItemStyle-CssClass="text-center-align">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnPrint" runat="server" CausesValidation="false" CommandName="Print" Text="Print" CssClass="btn btn-sm btn-primary" Style="padding: 2px 8px; font-size: 11px;" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Print Bill" HeaderStyle-Width="70px" ItemStyle-Width="70px" ItemStyle-CssClass="text-center-align">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnPrint" runat="server" CausesValidation="false" CommandName="Print" Text="🖨️ Print"
                                    Style="background-color: #2563eb; color: #ffffff !important; border: 1px solid #1d4ed8; padding: 4px 10px; font-size: 11px; font-weight: 600; border-radius: 4px; text-decoration: none !important; display: inline-block; cursor: pointer; transition: all 0.2s ease-in-out; box-shadow: 0 1px 2px rgba(0,0,0,0.1);"
                                    onmouseover="this.style.backgroundColor='#1d4ed8'; this.style.borderColor='#1e40af';"
                                    onmouseout="this.style.backgroundColor='#2563eb'; this.style.borderColor='#1d4ed8';" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- Bottom Approval Button -->
            <div id="btnApproveArea" class="action-footer-area no-print">
                <asp:Button ID="btnApprove" runat="server" Text="Approve Selected Bills" CssClass="btn btn-success"
                    Style="background-color: #16a34a; color: white; border: none; padding: 8px 20px; font-weight: bold; cursor: pointer; border-radius: 4px;"
                    OnClick="btnApprove_Click" OnClientClick="return confirm('Are you sure you want to approve selected bill(s)?');" />
            </div>

        </div>
    </div>
</asp:Content>
