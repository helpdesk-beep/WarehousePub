<%@ Page Title="Godown Wise Moisture Entry Details" Language="C#" MasterPageFile="~/MasterPage/CollectorMasterPage.master" AutoEventWireup="true" CodeFile="Godown_Wise_Online_Offline_Moisture_Details.aspx.cs" Inherits="District_Godown_Wise_Online_Offline_Moisture_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center !important; width: 100% !important; margin-bottom: 20px; font-family: 'Segoe UI', Arial, sans-serif; }
        
        /* Table Header Formatting Forced */
        html body .container-fluid .report-panel table.table tr th,
        table.table thead tr th, .table th {
            background: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 11px !important;
            border: 1px solid #172554 !important; padding: 10px !important; white-space: normal !important;
        }
        .table td { vertical-align: middle !important; border: 1px solid #e2e8f0 !important; font-size: 11px !important; text-align: left !important; padding: 8px 10px !important; white-space: normal !important; word-break: break-word !important; }
        
        /* Specificity Alignment Locks */
        html body .container-fluid .report-panel table.table tr td.text-right-align, .table td.text-right-align, .text-right-align { text-align: right !important; padding-right: 12px !important; white-space: nowrap !important; }
        html body .container-fluid .report-panel table.table tr td.text-center-align, .table td.text-center-align, .text-center-align { text-align: center !important; white-space: nowrap !important; }

        .report-panel { background: #fff; padding: 15px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); margin-bottom: 25px; }
        .btn-area { text-align: right; margin-bottom: 15px; }
        .meta-info-strip { background-color: #f8fafc; border: 1px solid #e2e8f0; padding: 10px 15px; border-radius: 4px; margin-bottom: 15px; font-size: 14px; font-weight: bold; color: #334155; }
        
        /* BOUNDARY SCROLLER WRAPPER */
        .grid-scroller-container { width: 100% !important; max-width: 100% !important; overflow-x: auto !important; display: block !important; border: 1px solid #e2e8f0; border-radius: 4px; }

        @media print {
            @page { size: landscape; margin: 5mm; }
            body *, html * { visibility: hidden; height: auto !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100%; background: none; padding: 0; margin: 0; zoom: 72%; }
            .btn-area, .page-header, .meta-info-strip { display: none !important; }
            
            .print-header-block { display: block !important; text-align: center !important; width: 100% !important; }
            .print-header-block * { text-align: center !important; }
            
            .report-panel { box-shadow: none !important; padding: 0 !important; border: none !important; }
            .table { border-collapse: collapse !important; width: 100% !important; table-layout: auto !important; }
            thead { display: table-header-group !important; }
            tr { page-break-inside: avoid !important; }
            .grid-scroller-container { overflow-x: visible !important; max-width: none !important; border: none !important; display: inline !important; }
            
            .table td { white-space: normal !important; word-break: break-word !important; }
            .table td.text-right-align { text-align: right !important; padding-right: 12px !important; }
            .table td.text-center-align { text-align: center !important; }

            html body #printZone .report-panel table.table thead tr th,
            html body #printZone .report-panel table.table tr th {
                background-color: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
                text-align: center !important; vertical-align: middle !important; border: 1px solid #172554 !important;
                -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important;
            }
        }
    </style>
    <div id="printZone" class="container-fluid mt-3">
        <div class="page-header">Godown Wise Online / Offline Moisture Entry Status Logs</div>

        <div class="print-header-block">
            <h2 style="margin: 0 auto; font-size: 24px; font-weight: bold; color: #1e3a8a; text-align: center !important;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="margin: 6px auto; font-size: 16px; color: #475569; text-align: center !important;">Godown Wise Online & Offline Moisture Entry Progress Details</h4>
            <p style="margin: 5px auto 0 auto; font-size: 12px; font-weight: bold; text-align: center !important; color: #334155;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
            <hr style="border: 1px solid #1e3a8a; margin-top: 10px; margin-bottom: 20px; width: 100%;" />
        </div>

        <div class="report-panel">
            <div class="btn-area">
                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print Report" OnClientClick="window.print(); return false;" />
            </div>

            <div class="grid-scroller-container">
                <asp:GridView ID="gvGodownDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="Godown_ID"
                    CssClass="table table-bordered table-striped table-hover mb-0" ShowFooter="false"
                    EmptyDataText="No active stack moisture mappings caught inside this targeted depot sector boundary."
                    OnRowDataBound="gvGodownDetails_RowDataBound" OnDataBound="gvGodownDetails_DataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle Width="50px" CssClass="text-center-align" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                        
                        <asp:BoundField DataField="Total Stack" HeaderText="Total Stack" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Online Moisture Up to Date" HeaderText="Online Moisture Up to Date" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Offline Moisture entry" HeaderText="Offline Moisture Entry" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total Moisture" HeaderText="Total Moisture" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Pending Stack For Moisture" HeaderText="Pending Stack For Moisture" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Stack Moisture Sent to DM MPSCSC/FCI" HeaderText="Stack Moisture Sent to DM" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="FCI Inspected Stack" HeaderText="FCI Inspected Stack" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Pending at FCI" HeaderText="Pending at FCI" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>


</asp:Content>

