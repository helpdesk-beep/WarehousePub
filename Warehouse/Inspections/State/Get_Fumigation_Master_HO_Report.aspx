<%@ Page Title="Fumigation Stack Balance Summary Dashboard" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="~/Inspections/State/Get_Fumigation_Master_HO_Report.aspx.cs" Inherits="Inspections_State_Get_Fumigation_Master_HO_Report" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 20px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center !important; width: 100% !important; margin-bottom: 20px; font-family: 'Segoe UI', Arial, sans-serif; }
        
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
        
        .subtotal-row td { background-color: #f1f5f9 !important; font-weight: bold !important; color: #1e3a8a !important; border-top: 1px solid #cbd5e1 !important; border-bottom: 1px solid #cbd5e1 !important; }
        .grandtotal-row td { background-color: #eff6ff !important; border-top: 2px solid #2563eb !important; border-bottom: 2px solid #1e3a8a !important; font-weight: bold !important; color: #1e3a8a !important; }

        .report-panel { background: #fff; padding: 15px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); margin-bottom: 25px; }
        .btn-area { text-align: right; margin-bottom: 15px; }
        .grid-scroller-container { width: 100% !important; max-width: 100% !important; overflow-x: auto !important; display: block !important; border: 1px solid #e2e8f0; border-radius: 4px; }

        @media print {
            @page { size: landscape; margin: 5mm; }
            body *, html * { visibility: hidden; height: auto !important; background-color: transparent !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100%; padding: 0; margin: 0; zoom: 70%; display: block !important; }
            .btn-area, .page-header { display: none !important; }
            
            .print-header-block { display: block !important; text-align: center !important; width: 100% !important; }
            .report-panel { box-shadow: none !important; padding: 0 !important; border: none !important; }
            .grid-view { border-collapse: collapse !important; width: 100% !important; table-layout: auto !important; }
            thead { display: table-header-group !important; }
            tr { page-break-inside: avoid !important; }
            .grid-scroller-container { overflow-x: visible !important; max-width: none !important; border: none !important; display: inline !important; }
            .grid-view td { border: 1px solid #94a3b8 !important; color: #000000 !important; }
            
            html body #printZone .report-panel table.grid-view thead tr th {
                background-color: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
                -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="printZone" class="container-fluid mt-2">
        <div class="page-header">District Wise Online & Offline Fumigation Balance Statement Dashboard</div>

        <div class="print-header-block">
            <h2 style="font-size: 24px; font-weight: bold; color: #1e3a8a;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="font-size: 15px; color: #475569; margin-top: 5px;">District Wise Online/Offline Stack Fumigation Balance Details</h4>
            <p style="font-size: 12px; font-weight: bold; color: #334155; margin-top: 4px;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
            <hr style="border: 1px solid #1e3a8a; margin-top: 8px;" />
        </div>

        <div class="report-panel">
            <div class="btn-area">
                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success btn-sm" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-secondary btn-sm" Text="Print Ledger Report" OnClientClick="window.print(); return false;" />
            </div>

            <div class="grid-scroller-container">
                <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="District_Id"
                    CssClass="table grid-view table-bordered table-striped table-hover mb-0" ShowFooter="false"
                    EmptyDataText="No records available inside selected scope." OnRowDataBound="gvDetails_RowDataBound" OnDataBound="gvDetails_DataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle Width="40px" CssClass="text-center-align" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Regionnm" HeaderText="Region Name" ItemStyle-Font-Bold="true" />
                        <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                        
                        <asp:BoundField DataField="Total_Godown" HeaderText="Total Godown" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Online_Stack" HeaderText="Total Online Stack" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Godowns_Covered" HeaderText="Total Godowns Covered" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Online_Fumigated_Stack" HeaderText="Total Online Fumigated Stack" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Offline_Fumigated" HeaderText="Total Offline Fumigated" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Fumigation_Stack" HeaderText="Total Fumigation Stack" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Pending_Godown" HeaderText="Total Pending Godown" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Pending_Stack_For_Fumigation" HeaderText="Pending Stack For Fumigation" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Opened_Stacks" HeaderText="Total Opened Stacks" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Fumigation_Percentage" HeaderText="Fumigation Percentage" DataFormatString="{0:0.00}%" ItemStyle-CssClass="text-right-align" ItemStyle-Font-Bold="true" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>