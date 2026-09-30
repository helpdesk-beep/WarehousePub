<%@ Page Title="District Wise Moisture Inspected by FCI" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="~/Inspections/State/rpt_District_Wise_Moisture_Inspected_by_FCI_With_Percent.aspx.cs" Inherits="Inspections_State_rpt_District_Wise_Moisture_Inspected_by_FCI_With_Percent" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center !important; width: 100% !important; margin-bottom: 5px !important; padding: 0 !important; }
        
        .grid-view { 
            font-family: 'Segoe UI', Arial, sans-serif; 
            border-collapse: collapse; 
            width: 100%; 
            margin-top: 5px !important; 
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
            background-color: #ffffff;
        }

        html body .container-fluid .report-panel table.grid-view tr th,
        html body .container-fluid .report-panel table.grid-view tr th a,
        html body .container-fluid .report-panel table.grid-view tr th span,
        table.grid-view thead tr th, 
        .grid-view th, .grid-view th a, .grid-view th span { 
            background-color: #1e3a8a !important; 
            color: #ffffff !important; 
            text-decoration: none !important;
            padding: 10px 8px !important; 
            text-align: center !important; 
            font-size: 11px !important; 
            font-weight: bold !important;
            border: 1px solid #172554 !important; 
            text-transform: uppercase;
            vertical-align: middle !important;
        }
        
        .grid-view td { padding: 10px 8px; border: 1px solid #e0e0e0; font-size: 12px; color: #333333; vertical-align: middle !important; }
        .grid-view tr:nth-child(even) { background-color: #f9f9f9; }
        .grid-view tr:hover:not(.subtotal-row):not(.grandtotal-row) { background-color: #f1f1f1; }

        .subtotal-row { background-color: #fed7aa !important; font-weight: bold !important; color: #000000 !important; }
        .subtotal-row td { border-top: 2px solid #ea580c !important; border-bottom: 2px solid #ea580c !important; font-weight: bold !important; }

        .grandtotal-row { background-color: #dbeafe !important; font-weight: bold !important; color: #1e3a8a !important; }
        .grandtotal-row td { border-top: 2px solid #2563eb !important; border-bottom: 2px solid #1e3a8a !important; font-weight: bold !important; }

        .report-panel { background: #fff; padding: 15px 25px !important; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); margin-top: 5px !important; }
        .btn-area { text-align: right; margin-bottom: 10px !important; }
        .grid-scroller-container { width: 100% !important; max-width: 100% !important; overflow-x: auto !important; display: block !important; border: 1px solid #e2e8f0; border-radius: 4px; }

        @media print {
            @page { size: landscape; margin: 4mm 5mm 5mm 5mm; }
            body *, html * { visibility: hidden; height: auto !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100%; background: none; padding: 0 !important; margin: 0 !important; zoom: 72%; }
            .btn-area, .page-header { display: none !important; }
            
            .print-header-block { display: block !important; text-align: center !important; width: 100% !important; margin-bottom: 5px !important; padding: 0 !important; }
            .print-header-block h2 { margin: 0 auto !important; padding: 0 !important; }
            .print-header-block h4 { margin: 3px auto !important; padding: 0 !important; }
            .print-header-block p { margin: 2px auto !important; padding: 0 !important; }
            .print-header-block hr { border: 1px solid #1e3a8a !important; margin-top: 5px !important; margin-bottom: 8px !important; opacity: 1 !important; }
            
            .report-panel { box-shadow: none !important; padding: 0 !important; margin: 0 !important; border: none !important; }
            .grid-view { border-collapse: collapse !important; width: 100% !important; margin-top: 0px !important; }
            
            thead { display: table-header-group !important; }
            tr { page-break-inside: avoid !important; }
            
            .grid-scroller-container { overflow-x: visible !important; border: none !important; display: inline !important; padding: 0 !important; margin: 0 !important; }
            .grid-view th, .grid-view th a, .grid-view th span { background-color: #1e3a8a !important; color: #ffffff !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
            .subtotal-row { background-color: #fed7aa !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
            .grandtotal-row { background-color: #dbeafe !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="printZone" class="container-fluid mt-2">
        <div class="page-header">District Wise Moisture Inspected by FCI Statement</div>

        <div class="print-header-block">
            <h2 style="margin: 0 auto; font-size: 24px; font-weight: bold; color: #1e3a8a; text-align: center !important;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="margin: 6px auto; font-size: 16px; color: #475569; text-align: center !important;">District Wise Moisture Inspection Progress Percent Ledger</h4>
            <p style="margin: 5px auto 0 auto; font-size: 12px; font-weight: bold; text-align: center !important; color: #334155;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
            <hr />
        </div>

        <div class="report-panel">
            <div class="btn-area">
                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print Document" OnClientClick="window.print(); return false;" />
            </div>

            <div class="grid-scroller-container">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False"
                    CssClass="grid-view table table-bordered mb-0" ShowFooter="false" OnRowDataBound="gvReport_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="Region Name" HeaderText="Region Name" />
                        <asp:BoundField DataField="District Name" HeaderText="District Name" />
                        
                        <asp:BoundField DataField="Total_Godown" HeaderText="Total Godowns" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="FCI_Inspected_Godowns" HeaderText="FCI Inspected Godowns" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="PendingGodownForInspectbyFCI" HeaderText="Pending Godown For Inspect by FCI" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="GodownPendingPercantage" HeaderText="Godown Inspection Progress (%)" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:F2}%" ItemStyle-Font-Bold="true" />

                        <asp:BoundField DataField="Total_Moisture" HeaderText="Total Moisture" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="Total_Stack_Send_TO_DM_FCI" HeaderText="Moisture Sent to DM" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="Total_Inspected_Stack" HeaderText="FCI Inspected Stack" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                        
                        <asp:BoundField DataField="Percantage" HeaderText="Stack Percentage (%)" DataFormatString="{0:F2}%" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>