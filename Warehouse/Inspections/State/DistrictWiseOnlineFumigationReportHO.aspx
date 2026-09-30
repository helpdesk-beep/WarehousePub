<%@ Page Title="District Wise Online Fumigation Report" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="DistrictWiseOnlineFumigationReportHO.aspx.cs" Inherits="Inspections_State_DistrictWiseOnlineFumigationReportHO" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center !important; width: 100% !important; margin-bottom: 5px !important; }
        
        /* Corporate Grid Standard Theme Mapping Blue Settings */
        .grid-view { font-family: 'Segoe UI', Arial, sans-serif; border-collapse: collapse; width: 100%; background-color: #ffffff; box-shadow: 0 2px 5px rgba(0,0,0,0.1); }
        
        html body form table.grid-view tr th, table.grid-view thead tr th, .grid-view th {
            background-color: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 11px !important;
            border: 1px solid #172554 !important; padding: 10px 6px !important; text-transform: uppercase;
        }
        .grid-view td { padding: 8px 10px; border: 1px solid #e0e0e0; font-size: 13px; color: #333333; vertical-align: middle !important; }
        .grid-view tr:nth-child(even) { background-color: #f9f9f9; }
        .grid-view tr:hover:not(.grandtotal-row) { background-color: #f1f1f1; }
        
        /* Structural Alignment Utilities */
        .text-right-align { text-align: right !important; padding-right: 12px !important; white-space: nowrap !important; }
        .text-center-align { text-align: center !important; white-space: nowrap !important; }
        
        .subtotal-row { background-color: #fed7aa !important; font-weight: bold !important; color: #000000 !important; }
        .subtotal-row td { border-top: 1px solid #ea580c !important; border-bottom: 1px solid #ea580c !important; font-weight: bold !important; }

        .grandtotal-row { background-color: #dbeafe !important; font-weight: bold !important; color: #1e3a8a !important; }
        .grandtotal-row td { border-top: 2px solid #2563eb !important; border-bottom: 2px solid #1e3a8a !important; font-weight: bold !important; }

        .report-panel { background: #fff; padding: 20px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); margin-top: 15px; }
        .btn-area { text-align: right; margin-bottom: 15px; }
        .grid-scroller-container { width: 100% !important; max-width: 100% !important; overflow-x: auto !important; display: block !important; border: 1px solid #e2e8f0; border-radius: 4px; }

        @media print {
            @page { size: landscape; margin: 5mm; }
            body *, html * { visibility: hidden; height: auto !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100%; background: none; padding: 0 !important; margin: 0 !important; zoom: 72%; }
            .btn-area, .page-header { display: none !important; }
            
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="printZone" class="container-fluid mt-3">
        <div class="page-header">District Wise Online Fumigation Master Ledger</div>

        <div class="print-header-block">
            <h2>M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
            <h4>District Wise Stack Fumigation Progress Percent Statement Report</h4>
            <p style="margin: 3px auto; font-size: 12px; font-weight: bold; color: #334155;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
            <hr />
        </div>

        <div class="report-panel">
            <div class="btn-area">
                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-secondary" Style="background-color: #475569; border-color: #475569; color:#fff;" Text="Print Report" OnClientClick="window.print(); return false;" />
            </div>

            <div class="grid-scroller-container">
                <asp:GridView ID="gvFumigation" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid-view table table-bordered mb-0" ShowFooter="false" OnRowDataBound="gvFumigation_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle Width="50px" CssClass="text-center-align" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="Region_Name" HeaderText="Region Name" />
                        <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                        <asp:BoundField DataField="Financial_year" HeaderText="Financial Year" ItemStyle-CssClass="text-center-align" />
                        
                        <asp:BoundField DataField="Total_Godowns_Covered" HeaderText="Total Godowns Covered" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Fumigated_Till_Yesterday" HeaderText="Fumigated Till Yesterday" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Fumigated_Today" HeaderText="Fumigated Today" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Completed_Fumigations" HeaderText="Completed Fumigations" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Opened_Stacks" HeaderText="Total Opened Stacks" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        
                        <%-- Hidden backend reference fields context bounds map securely --%>
                        <asp:BoundField DataField="District_Id" HeaderText="District ID" Visible="false" />
                        <asp:BoundField DataField="Region_ID" HeaderText="Region ID" Visible="false" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>