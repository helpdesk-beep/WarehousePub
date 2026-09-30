<%@ Page Title="District Wise Online Fumigation Summary (HO)" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="~/Inspections/State/DistrictWiseOnlineFumigationReportHO_New.aspx.cs" Inherits="Inspections_State_DistrictWiseOnlineFumigationReportHO_New" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center; margin-bottom: 20px; font-family: 'Segoe UI', Arial, sans-serif; }
        
        /* Table Header System */
        html body .container-fluid .report-panel table.table tr th,
        table.table thead tr th, .table th {
            background: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 11px !important;
            border: 1px solid #172554 !important; padding: 10px 5px !important; text-transform: uppercase;
        }

        /* Base Data Alignment */
        .table td { 
            vertical-align: middle !important; 
            border: 1px solid #e2e8f0 !important; 
            font-size: 12px !important; 
            text-align: left !important;
            padding: 8px 10px !important;
        }

        .text-right-align { text-align: right !important; padding-right: 12px !important; }
        .text-center-align { text-align: center !important; }

        /* Dynamic Rows Color Profiles */
        .subtotal-row { background-color: #fed7aa !important; font-weight: bold !important; color: #000000 !important; }
        .subtotal-row td { border-top: 1px solid #ea580c !important; border-bottom: 1px solid #ea580c !important; font-weight: bold !important; background-color: #fed7aa !important; }

        .grandtotal-row { background-color: #dbeafe !important; font-weight: bold !important; color: #1e3a8a !important; }
        .grandtotal-row td { border-top: 2px solid #2563eb !important; border-bottom: 2px solid #1e3a8a !important; font-weight: bold !important; background-color: #dbeafe !important; }

        .report-panel { background: #fff; padding: 15px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); }
        .btn-area { margin-bottom: 15px; text-align: right; }
        .grid-scroller-container { width: 100% !important; max-width: 100% !important; overflow-x: auto !important; display: block !important; border: 1px solid #e2e8f0; border-radius: 4px; }

        /* Print Media Layout */
        @media print {
            @page { size: landscape; margin: 10mm 5mm 10mm 5mm; }
            body *, html * { visibility: hidden; height: auto !important; background-color: transparent !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100% !important; background: none !important; padding: 0 !important; margin: 0 !important; zoom: 82%; display: block !important; }
            .btn-area, .page-header { display: none !important; height: 0 !important; padding: 0 !important; margin: 0 !important; }
            .print-header-block { display: block !important; width: 100% !important; text-align: center !important; margin-bottom: 15px !important; }
            .print-header-block h2 { color: #1e3a8a !important; font-weight: bold !important; margin: 0 !important; }
            .print-header-block h4 { color: #475569 !important; font-weight: bold !important; margin: 5px 0 !important; }
            .report-panel { box-shadow: none !important; padding: 0 !important; border: none !important; background: none !important; }
            .grid-scroller-container { overflow-x: visible !important; overflow: visible !important; max-width: 100% !important; border: none !important; display: inline !important; }
            .table { border-collapse: collapse !important; width: 100% !important; table-layout: auto !important; }
            thead { display: table-header-group !important; }
            tr { page-break-inside: avoid !important; }
            .table td { border: 1px solid #cbd5e1 !important; color: #000000 !important; font-size: 11px !important; }
            
            html body #printZone .report-panel table.table thead tr th {
                background-color: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
                -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important;
            }
            .subtotal-row td { background-color: #fed7aa !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
            .grandtotal-row td { background-color: #dbeafe !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="printZone" class="container-fluid">
        <div class="page-header">District Wise Online Fumigation Summary (HO)</div>

        <div class="print-header-block">
            <h2>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4>Head Office - District Wise Online Fumigation Report Summary</h4>
            <p style="margin:0; font-size:12px; font-weight:bold; color:#475569;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
            <hr style="border:1px solid #1e3a8a; margin-top:5px; margin-bottom:15px; opacity: 1 !important;"/>
        </div>

        <div class="report-panel">
            <div class="btn-area">
                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export Summary To Excel" OnClick="btnExcel_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print This Report" OnClientClick="window.print(); return false;" />
            </div>

            <div class="grid-scroller-container">
                <asp:GridView ID="gvFumigation" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover bg-white mb-0" 
                    ShowFooter="false" OnRowDataBound="gvFumigation_RowDataBound" OnDataBound="gvFumigation_DataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle Width="50px" CssClass="text-center-align" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="Region_Name" HeaderText="Region Name" />
                        <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                        
                        <asp:BoundField DataField="Total_Godowns_Covered" HeaderText="Total Godowns Covered" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Stack" HeaderText="Total Stack" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Fumigated_Till_Yesterday" HeaderText="Fumigated Till Yesterday" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Fumigated_Today" HeaderText="Fumigated Today" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Completed_Fumigations" HeaderText="Completed Fumigations" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Pending Stack For Fumigation" HeaderText="Pending Stack For Fumigation" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Opened_Stacks" HeaderText="Total Opened Stacks" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>