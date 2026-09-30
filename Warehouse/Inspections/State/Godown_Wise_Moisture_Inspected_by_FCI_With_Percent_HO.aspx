<%@ Page Title="Godown Stack Details Wise FCI Inspection" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="Godown_Wise_Moisture_Inspected_by_FCI_With_Percent_HO.aspx.cs" Inherits="Inspections_State_Godown_Wise_Moisture_Inspected_by_FCI_With_Percent_HO" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 20px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center !important; width: 100% !important; margin-bottom: 20px; font-family: 'Segoe UI', Arial, sans-serif; }
        .navigation-panel { background: #f1f5f9; padding: 10px 15px; border-radius: 5px; border: 1px solid #cbd5e1; margin-bottom: 15px; }

        .grid-view { font-family: 'Segoe UI', Arial; border-collapse: collapse; width: 100%; background-color: #ffffff; }
        html body .container-fluid .report-panel table.grid-view tr th, .grid-view th {
            background: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 11px !important;
            border: 1px solid #172554 !important; padding: 10px 4px !important; text-transform: uppercase;
        }
        .grid-view td { padding: 6px 10px; border: 1px solid #cbd5e1; font-size: 12px; color: #333333; vertical-align: middle !important; }
        .grid-view tr:nth-child(even) { background-color: #f8fafc; }
        
        .text-right-align { text-align: right !important; padding-right: 12px !important; white-space: nowrap !important; }
        .text-center-align { text-align: center !important; white-space: nowrap !important; }
        
        .grandtotal-row td { background-color: #eff6ff !important; border-top: 2px solid #2563eb !important; border-bottom: 2px solid #1e3a8a !important; font-weight: bold !important; color: #1e3a8a !important; }
        .report-panel { background: #fff; padding: 15px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); margin-bottom: 25px; }
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
            .grid-view td { border: 1px solid #94a3b8 !important; color: #000000 !important; }
            html body #printZone .report-panel table.grid-view thead tr th { background-color: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="printZone" class="container-fluid mt-2">
        <div class="page-header">Godown Wise Stack Moisture Inspection Detail View</div>

        <div class="navigation-panel">
            <div class="d-flex justify-content-between align-items-center">
                <div>
                    <span class="fw-bold text-muted">Active Center Scope:</span>
                    <asp:Label ID="lblScopeHeader" runat="server" CssClass="badge bg-dark px-3 py-1 fs-6 ms-1"></asp:Label>
                </div>
            </div>
        </div>

        <div class="print-header-block">
            <h2 style="font-size: 24px; font-weight: bold; color: #1e3a8a;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4>Individual Godown Warehouse Moisture Inspection Status Sheet</h4>
            <p style="font-size: 12px; font-weight: bold; color: #334155; margin-top: 4px;">
                Center Scope: <asp:Label ID="lblPrintRegion" runat="server"></asp:Label> 
                | Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %>
            </p>
            <hr style="border: 1px solid #1e3a8a; margin-top: 8px;" />
        </div>

        <div class="report-panel">
            <div class="btn-area">
                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success btn-sm" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary btn-sm" Text="Print / PDF" OnClientClick="window.print(); return false;" />
            </div>

            <div class="grid-scroller-container">
                <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false"
                    CssClass="table grid-view table-bordered table-striped table-hover mb-0" ShowFooter="false"
                    EmptyDataText="No warehouse stack records available inside selected dynamic scope." OnRowDataBound="gvDetails_RowDataBound" OnDataBound="gvDetails_DataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle Width="45px" CssClass="text-center-align" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Region Name" HeaderText="Region" ItemStyle-Font-Bold="true" />
                        <asp:BoundField DataField="District Name" HeaderText="District" />
                        <asp:BoundField DataField="Branch Name" HeaderText="Branch / Center" />
                        <asp:BoundField DataField="Godown Name" HeaderText="Godown Name" ItemStyle-Font-Bold="true" />
                        
                        <asp:BoundField DataField="FCI_Inspected_Godowns" HeaderText="FCI Inspected Godowns" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="PendingGodownForInspectbyFCI" HeaderText="Pending Godowns for FCI" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="GodownPendingPercantage" HeaderText="Godowns Inspected %" DataFormatString="{0:0.00}%" ItemStyle-CssClass="text-right-align" />
                        
                        <asp:BoundField DataField="Total_Moisture" HeaderText="Total Moisture Stacks" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Stack_Send_TO_DM_FCI" HeaderText="Moisture Sent To DM/FCI" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Inspected_Stack" HeaderText="FCI Inspected Stacks" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Percantage" HeaderText="Clearance Percentage %" DataFormatString="{0:0.00}%" ItemStyle-CssClass="text-right-align" ItemStyle-Font-Bold="true" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>