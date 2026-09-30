<%@ Page Title="Branch Wise Online & Offline Moisture Details" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_BO.master" AutoEventWireup="true" CodeFile="~/Inspections/BO/Godown_Wise_OnlineOffline_Moisture_Details_For_BO.aspx.cs" Inherits="Inspections_BO_Godown_Wise_OnlineOffline_Moisture_Details_For_BO" EnableEventValidation="false" %>

<%-- FIXED LAYER 1: Separated CSS to 'head' content block to stop 'Multiple Contents Applied' exception --%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center !important; width: 100% !important; margin-bottom: 20px; font-family: 'Segoe UI', Arial, sans-serif; }
        
        /* Table Header Formatting Forced */
        html body .container-fluid .report-panel table.table tr th,
        table.table thead tr th, .table th {
            background: #2563eb !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 11px !important;
            border: 1px solid #1e40af !important; padding: 10px !important; white-space: normal !important;
        }
        
        /* Base Data Cell Properties */
        .table td { 
            vertical-align: middle !important; border: 1px solid #e2e8f0 !important; font-size: 11px !important; 
            text-align: left !important; padding: 8px 10px !important; white-space: normal !important; word-break: break-word !important; 
        }
        
        /* Specificity Alignment Utility Locks */
        html body .container-fluid .report-panel table.table tr td.text-right-align, .table td.text-right-align, .text-right-align { text-align: right !important; padding-right: 12px !important; white-space: nowrap !important; }
        html body .container-fluid .report-panel table.table tr td.text-center-align, .table td.text-center-align, .text-center-align { text-align: center !important; white-space: nowrap !important; }

        .report-panel { background: #fff; padding: 15px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); margin-bottom: 25px; }
        .btn-area { text-align: right; margin-bottom: 15px; }
        .grid-scroller-container { width: 100% !important; max-width: 100% !important; overflow-x: auto !important; display: block !important; border: 1px solid #e2e8f0; border-radius: 4px; }

        /* PERFECT PRINT MEDIA MANAGEMENT ENGINE FILTER */
        @media print {
            @page { size: landscape; margin: 5mm; }
            body *, html * { visibility: hidden; height: auto !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100%; background: none; padding: 0; margin: 0; zoom: 72%; }
            .btn-area, .page-header { display: none !important; }
            
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
            
            /* FIXED FOR PRINT: Prevents links from being clickable or styled blue on hardcopy layouts */
            html body #printZone .table td a, .table td a, #printZone a { 
                color: #000000 !important; text-decoration: none !important; pointer-events: none !important; cursor: default !important; font-weight: normal !important; 
            }
            .table td a::after { content: none !important; display: none !important; }
        }
    </style>
</asp:Content>

<%-- FIXED LAYER 2: GridView and body data blocks isolated into ContentPlaceHolder1 cleanly --%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="printZone" class="container-fluid mt-3">
        <div class="page-header">Godown Wise Online / Offline Moisture Entry Status Logs</div>

        <div class="print-header-block">
            <h2 style="margin: 0 auto; font-size: 24px; font-weight: bold; color: #1e3a8a; text-align: center !important;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="margin: 6px auto; font-size: 16px; color: #475569; text-align: center !important;">Godown Wise Online & Offline Moisture Entry Progress Summary Details</h4>
            <p style="margin: 5px auto 0 auto; font-size: 12px; font-weight: bold; text-align: center !important; color: #334155;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
            <hr style="border: 1px solid #1e3a8a; margin-top: 10px; margin-bottom: 20px; width: 100%;" />
        </div>

        <div class="report-panel">
            <div class="btn-area">
                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print Report" OnClientClick="window.print(); return false;" />
            </div>

            <div class="grid-scroller-container">
                <%-- FIXED DESIGN: DataKeyNames map Godown_ID securely. Godown_ID Boundfield explicitly dropped from column streams --%>
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false" DataKeyNames="Godown_ID"
                    CssClass="table table-bordered table-striped table-hover mb-0" ShowFooter="false"
                    OnRowDataBound="gvReport_RowDataBound" OnDataBound="gvReport_DataBound">
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