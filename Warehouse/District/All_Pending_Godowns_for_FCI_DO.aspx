<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CollectorMasterPage.master" AutoEventWireup="true" CodeFile="All_Pending_Godowns_for_FCI_DO.aspx.cs" Inherits="District_All_Pending_Godowns_for_FCI_DO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; padding: 20px; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 20px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center !important; width: 100% !important; margin-bottom: 20px; }
        .navigation-panel { background: #f1f5f9; padding: 10px 15px; border-radius: 5px; border: 1px solid #cbd5e1; margin-bottom: 15px; }

        .grid-view { font-family: 'Segoe UI', Arial; border-collapse: collapse; width: 100%; background-color: #ffffff; }
        html body .grid-scroller-container table.grid-view tr th, .grid-view th {
            background: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 11px !important;
            border: 1px solid #172554 !important; padding: 10px 4px !important; text-transform: uppercase;
        }
        .grid-view td { padding: 6px 10px; border: 1px solid #cbd5e1; font-size: 12px; color: #333333; vertical-align: middle !important; }
        .grid-view tr:nth-child(even) { background-color: #f8fafc; }
        
        .text-center-align { text-align: center !important; white-space: nowrap !important; }
        .report-panel { background: #fff; padding: 15px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); margin-bottom: 25px; border: 1px solid #e2e8f0; }
        .btn-area { text-align: right; margin-bottom: 15px; }
        .grid-scroller-container { width: 100% !important; max-width: 100% !important; overflow-x: auto !important; display: block !important; border: 1px solid #e2e8f0; border-radius: 4px; }

        @media print {
            @page { size: landscape; margin: 5mm; }
            body *, html * { visibility: hidden; height: auto !important; background-color: transparent !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100%; padding: 0; margin: 0; zoom: 70%; display: block !important; }
            .btn-area, .page-header, .navigation-panel { display: none !important; }
            .print-header-block { display: block !important; text-align: center !important; width: 100% !important; }
            .report-panel { box-shadow: none !important; padding: 0 !important; border: none !important; }
            .grid-view { border-collapse: collapse !important; width: 100% !important; table-layout: auto !important; }
            thead { display: table-header-group !important; }
            tr { page-break-inside: avoid !important; }
            .grid-scroller-container { overflow-x: visible !important; max-width: none !important; border: none !important; display: inline !important; }
            html body #printZone .report-panel table.grid-view thead tr th { background-color: #1e3a8a !important; color: #ffffff !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
        }
    </style>

    <div id="printZone" class="container-fluid mt-3">
            <div class="page-header">FCI Moisture Inspection Pending Godowns Exception Ledger</div>

            <div class="navigation-panel">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <span class="fw-bold text-muted">Dynamic Search Context:</span>
                        <asp:Label ID="lblScopeHeader" runat="server" CssClass="badge bg-dark px-3 py-1 fs-6 ms-1"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="print-header-block">
                <h2 style="font-size: 24px; font-weight: bold; color: #1e3a8a;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
                <h4>FCI Moisture Inspection Missing Clearance Ledger Sheets</h4>
                <p style="font-size: 12px; font-weight: bold; color: #334155; margin-top: 4px;">
                    Scope Context: <asp:Label ID="lblPrintRegion" runat="server"></asp:Label> 
                    | Extracted On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %>
                </p>
                <hr style="border: 1px solid #1e3a8a; margin-top: 8px;" />
            </div>

            <div class="report-panel">
                <div class="btn-area">
                    <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success btn-sm" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export Un-Inspected List" OnClick="btnExcel_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-secondary btn-sm" Text="Print Exceptions Report" OnClientClick="window.print(); return false;" />
                </div>

                <div class="grid-scroller-container">
                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false"
                        CssClass="table grid-view table-bordered table-striped table-hover mb-0" ShowFooter="false"
                        EmptyDataText="Excellent Compliance! No pending records available inside this tracking context scope.">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                <ItemStyle Width="45px" CssClass="text-center-align" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="Region Name" HeaderText="Region Name" ItemStyle-Font-Bold="true" />
                            <asp:BoundField DataField="District Name" HeaderText="District Name" />
                            <asp:BoundField DataField="Branch Name" HeaderText="Depot Name" />
                            <asp:BoundField DataField="Godown Name" HeaderText="Godown Name" ItemStyle-Font-Bold="true" />
                            <asp:BoundField DataField="Is_Godown_Inspected_By_FCI" HeaderText="Is Godown Inspected By FCI" ItemStyle-CssClass="text-center-align" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
</asp:Content>

