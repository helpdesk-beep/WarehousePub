<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Godown_Depositor_Wise_Stock_position.aspx.cs" Inherits="Region_State_Rpt_Godown_Depositor_Wise_Stock_position" EnableEventValidation="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stock Position Report</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center !important; width: 100% !important; margin-bottom: 5px !important; }
        
        /* Corporate Grid Standard Theme */
        .grid-view { font-family: 'Segoe UI', Arial, sans-serif; border-collapse: collapse; width: 100%; background-color: #ffffff; box-shadow: 0 2px 5px rgba(0,0,0,0.1); }
        
        html body form table.grid-view tr th, table.grid-view thead tr th, .grid-view th {
            background-color: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 11px !important;
            border: 1px solid #172554 !important; padding: 10px 6px !important; text-transform: uppercase;
        }
        .grid-view td { padding: 8px 10px; border: 1px solid #e0e0e0; font-size: 12px; color: #333333; vertical-align: middle !important; }
        .grid-view tr:nth-child(even) { background-color: #f9f9f9; }
        
        /* Structural Alignment Utilities */
        .text-right-align { text-align: right !important; padding-right: 12px !important; white-space: nowrap !important; }
        .text-center-align { text-align: center !important; white-space: nowrap !important; }

        /* Accounting Formatting Hierarchy Rows */
        .subtotal-row { background-color: #fed7aa !important; font-weight: bold !important; color: #000000 !important; }
        .subtotal-row td { border-top: 2px solid #ea580c !important; border-bottom: 2px solid #ea580c !important; font-weight: bold !important; }
        
        .grandtotal-row { background-color: #dbeafe !important; font-weight: bold !important; color: #1e3a8a !important; }
        .grandtotal-row td { border-top: 2px solid #2563eb !important; border-bottom: 2px solid #1e3a8a !important; font-weight: bold !important; }

        .report-panel { background: #fff; padding: 20px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); margin-top: 15px; }
        .filter-section { background: #f8fafc; border: 1px solid #e2e8f0; padding: 20px; border-radius: 5px; margin-bottom: 15px; }
        .btn-area { text-align: right; }
        .grid-scroller-container { width: 100% !important; max-width: 100% !important; overflow-x: auto !important; display: block !important; border: 1px solid #e2e8f0; border-radius: 4px; }

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
            .subtotal-row { background-color: #fed7aa !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
            .grandtotal-row { background-color: #dbeafe !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="printZone" class="container-fluid mt-3">
            <div class="page-header">District, Godown, Depositor, Commodity Wise Stock Position Report</div>

            <div class="print-header-block">
                <h2>M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
                <h4>District, Godown, Depositor, Commodity Wise Stock Position Summary (Qty in MT)</h4>
                <p style="margin: 3px auto; font-size: 12px; font-weight: bold; color: #334155;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
                <hr />
            </div>

            <div class="filter-section">
                <div class="row align-items-end g-3">
                    <div class="col-md-3">
                        <label class="fw-bold mb-1">Date (DD/MM/YYYY):</label>
                        <asp:TextBox ID="txtpaymentdate" runat="server" CssClass="form-control form-control-sm" placeholder="Select Date"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="fw-bold mb-1">Depositor:</label>
                        <asp:DropDownList ID="ddlDepositor" runat="server" CssClass="form-select form-select-sm"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label class="fw-bold mb-1">Commodity:</label>
                        <asp:DropDownList ID="ddlcommodity" runat="server" CssClass="form-select form-select-sm"></asp:DropDownList>
                    </div>
                    <div class="col-md-3 btn-area">
                        <asp:Button ID="btnSearch" CssClass="btn btn-primary btn-sm" runat="server" Text="Search Statement" OnClick="btnSearch_Click" Style="background-color:#1e3a8a; border-color:#1e3a8a;" />
                        <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success btn-sm" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-secondary btn-sm" Style="background-color: #475569; border-color: #475569;" Text="Print Report" OnClientClick="window.print(); return false;" />
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
                        CssClass="grid-view table table-bordered mb-0" ShowFooter="false">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                <ItemStyle Width="50px" CssClass="text-center-align" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="District" HeaderText="District Name" />
                            <asp:BoundField DataField="GodownName" HeaderText="Godown Name" />
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
                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" Visible="false" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>