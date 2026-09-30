<%@ Page Title="District Wise Wheat Procurement WHR Report" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Whaet_Procurement_District_Wise_2026_27_New.aspx.cs" Inherits="StatePages_Whaet_Procurement_District_Wise_2026_27_New" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center !important; width: 100% !important; margin-bottom: 20px; }
        
        /* Grid Table Core Alignments Forced Corporate Blue Style */
        .grid-view { 
            font-family: 'Segoe UI', Arial, sans-serif; 
            border-collapse: collapse; 
            width: 100%; 
            margin-top: 15px; 
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
            background-color: #ffffff;
        }
        
        html body .container-fluid .report-panel table.grid-view tr th,
        table.grid-view thead tr th, .grid-view th {
            background: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 11px !important;
            border: 1px solid #172554 !important; padding: 10px 8px !important; white-space: normal !important;
            text-transform: uppercase;
        }
        .grid-view td { vertical-align: middle !important; border: 1px solid #e2e8f0 !important; font-size: 11px !important; text-align: left !important; padding: 8px 10px !important; }
        .grid-view tr:nth-child(even) { background-color: #f9f9f9; }
        .grid-view tr:hover:not(.subtotal-row):not(.grandtotal-row) { background-color: #f1f1f1; }
        
        /* Specificity Alignment Locks */
        html body .container-fluid .report-panel table.grid-view tr td.text-right-align, .grid-view td.text-right-align, .text-right-align { text-align: right !important; padding-right: 12px !important; white-space: nowrap !important; }
        html body .container-fluid .report-panel table.grid-view tr td.text-center-align, .grid-view td.text-center-align, .text-center-align { text-align: center !important; white-space: nowrap !important; }

        /* Subtotal and Grand Total Hierarchy Colors */
        .subtotal-row { background-color: #fed7aa !important; font-weight: bold !important; color: #000000 !important; }
        .subtotal-row td { border-top: 2px solid #ea580c !important; border-bottom: 2px solid #ea580c !important; font-weight: bold !important; }
        
        .grandtotal-row { background-color: #dbeafe !important; font-weight: bold !important; color: #1e3a8a !important; }
        .grandtotal-row td { border-top: 2px solid #2563eb !important; border-bottom: 2px solid #1e3a8a !important; font-weight: bold !important; }

        .report-panel { background: #fff; padding: 25px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); margin-bottom: 25px; }
        .filter-section { background: #f8fafc; border: 1px solid #e2e8f0; padding: 15px; border-radius: 5px; margin-bottom: 15px; }
        .btn-area { text-align: right; }
        .grid-scroller-container { width: 100% !important; max-width: 100% !important; overflow-x: auto !important; display: block !important; border: 1px solid #e2e8f0; border-radius: 4px; }

        @media print {
            @page { size: landscape; margin: 5mm; }
            body *, html * { visibility: hidden; height: auto !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100%; background: none; padding: 0; margin: 0; zoom: 70%; }
            .btn-area, .page-header, .filter-section { display: none !important; }
            .print-header-block { display: block !important; text-align: center !important; width: 100% !important; }
            .print-header-block * { text-align: center !important; }
            
            .grid-view { border-collapse: collapse !important; width: 100% !important; table-layout: auto !important; }
            thead { display: table-header-group !important; }
            tr { page-break-inside: avoid !important; }
            .grid-scroller-container { overflow-x: visible !important; border: none !important; display: inline !important; }
            
            .grid-view th { background-color: #1e3a8a !important; color: #ffffff !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
            .subtotal-row { background-color: #fed7aa !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
            .grandtotal-row { background-color: #dbeafe !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="printZone" class="container-fluid mt-3">
        <div class="page-header">District Wise Wheat Procurement & WHR Balance Ledger</div>

        <div class="print-header-block">
            <h2 style="margin: 0 auto; font-size: 24px; font-weight: bold; color: #1e3a8a; text-align: center !important;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="margin: 6px auto; font-size: 16px; color: #475569; text-align: center !important;">District Wise Wheat (Rabi) WHR Statement Progress Report (Crop Year: 2026-27)</h4>
            <p style="margin: 5px auto 0 auto; font-size: 12px; font-weight: bold; text-align: center !important; color: #334155;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
            <hr style="border: 1px solid #1e3a8a; margin-top: 10px; margin-bottom: 20px; width: 100%;" />
        </div>

        <div class="filter-section">
            <div class="row align-items-end g-3">
                <div class="col-md-4">
                    <label class="fw-bold mb-1">Target Region Filter Scope:</label>
                    <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-select">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary w-100" Style="background-color:#1e3a8a; border-color:#1e3a8a;" Text="Load Ledger" OnClick="btnSearch_Click" />
                </div>
                <div class="col-md-6 text-end btn-area">
                    <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print Report" OnClientClick="window.print(); return false;" />
                </div>
            </div>
        </div>

        <div class="report-panel">
            <div class="grid-scroller-container">
                <asp:GridView ID="gvWheat" runat="server" AutoGenerateColumns="False"
                    CssClass="grid-view table table-bordered mb-0" ShowFooter="false"
                    OnRowDataBound="gvWheat_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle Width="50px" CssClass="text-center-align" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="RegionName" HeaderText="Region Name" />
                        <asp:BoundField DataField="District_Id" HeaderText="District ID" ItemStyle-CssClass="text-center-align" />
                        <asp:BoundField DataField="DistrictName" HeaderText="District Name" />
                        
                        <asp:BoundField DataField="AcceptQty" HeaderText="Acceptance Qty (MT)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="TotalQty" HeaderText="WHR Received Qty (MT)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Averg" HeaderText="Progress Percentage (%)" DataFormatString="{0:N2}%" ItemStyle-CssClass="text-right-align" ItemStyle-Font-Bold="true" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>