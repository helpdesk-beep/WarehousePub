<%@ Page Title="Godown Wise NAFED Bill Settlement Details" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="ReceivePaymentFromNAFED_Details_For_Rigion.aspx.cs" Inherits="Region_ReceivePaymentFromNAFED_Details_For_Rigion" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center; margin-bottom: 20px; font-family: 'Segoe UI', Arial, sans-serif; }
        
        /* Table Header Formatting Forced */
        html body .container-fluid .report-panel table.table tr th,
        table.table thead tr th, .table th {
            background: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 11px !important;
            border: 1px solid #172554 !important; padding: 10px 5px !important; text-transform: uppercase;
        }

        /* Base Data Alignment Rule: Text columns left aligned */
        .table td { 
            vertical-align: middle !important; 
            border: 1px solid #e2e8f0 !important; 
            font-size: 12px !important; 
            text-align: left !important;
            padding: 8px 10px !important;
        }

        /* High priority rules for dynamic inner grid numeric alignments */
        html body .container-fluid .report-panel table.table tr td.text-right-align,
        .table td.text-right-align,
        .text-right-align { 
            text-align: right !important; 
            padding-right: 12px !important; 
        }

        html body .container-fluid .report-panel table.table tr td.text-center-align,
        .table td.text-center-align,
        .text-center-align { 
            text-align: center !important; 
        }

        /* Formatting Color Layers Mapped Globally */
        .subtotal-row { background-color: #fed7aa !important; font-weight: bold !important; color: #000000 !important; }
        .subtotal-row td { border-top: 1px solid #ea580c !important; border-bottom: 1px solid #ea580c !important; font-weight: bold !important; background-color: #fed7aa !important; }

        .grandtotal-row { background-color: #dbeafe !important; font-weight: bold !important; color: #1e3a8a !important; }
        .grandtotal-row td { border-top: 2px solid #2563eb !important; border-bottom: 2px solid #1e3a8a !important; font-weight: bold !important; background-color: #dbeafe !important; }

        .report-panel { background: #fff; padding: 15px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); }
        .btn-area { margin-bottom: 15px; text-align: right; }
        .meta-info-strip { background-color: #f8fafc; border: 1px solid #e2e8f0; padding: 10px 15px; border-radius: 4px; margin-bottom: 15px; font-size: 14px; font-weight: bold; color: #334155; }
        .grid-scroller-container { width: 100% !important; max-width: 100% !important; overflow-x: auto !important; display: block !important; border: 1px solid #e2e8f0; border-radius: 4px; }

        /* FIXED 100% WORKING PRINT SYSTEM ENGINE STYLE SHEETS LAYOUTS */
        @media print {
            @page { size: landscape; margin: 10mm 5mm 10mm 5mm; }
            
            /* Hide MasterPage layout, headers, footers and side blocks completely */
            body *, html * { visibility: hidden; height: auto !important; background-color: transparent !important; }
            
            /* Isolate print container target block viewport safely */
            #printZone, #printZone * { visibility: visible; }
            #printZone { 
                position: absolute; left: 0; top: 0; width: 100% !important; 
                background: none !important; padding: 0 !important; margin: 0 !important; 
                zoom: 82%; display: block !important;
            }
            
            /* Dynamic Controls Elimination */
            .btn-area, .page-header, .meta-info-strip { display: none !important; height: 0 !important; padding: 0 !important; margin: 0 !important; }
            
            /* Activate Corporate Letterhead Layout Statement */
            .print-header-block { display: block !important; width: 100% !important; text-align: center !important; margin-bottom: 15px !important; }
            .print-header-block h2 { color: #1e3a8a !important; font-weight: bold !important; margin: 0 !important; }
            .print-header-block h4 { color: #475569 !important; font-weight: bold !important; margin: 5px 0 !important; }
            
            /* Break containers overflows constraints constraints limits natively */
            .report-panel { box-shadow: none !important; padding: 0 !important; border: none !important; background: none !important; }
            .grid-scroller-container { overflow-x: visible !important; overflow: visible !important; max-width: 100% !important; border: none !important; display: inline !important; }
            
            /* Enforce Core Data Grid Elements Alignment & Border Frames */
            .table { border-collapse: collapse !important; width: 100% !important; table-layout: auto !important; margin-top: 0px !important; }
            thead { display: table-header-group !important; }
            tr { page-break-inside: avoid !important; }
            .table td { border: 1px solid #cbd5e1 !important; color: #000000 !important; font-size: 11px !important; }

            /* HIGH PRIORITY: Forces exact background color printing colors profiles explicitly */
            html body #printZone .report-panel table.table thead tr th,
            html body #printZone .report-panel table.table tr th {
                background-color: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
                text-align: center !important; vertical-align: middle !important; border: 1px solid #172554 !important;
                -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important;
            }
            
            .subtotal-row td { background-color: #fed7aa !important; color: #000000 !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
            .grandtotal-row td { background-color: #dbeafe !important; color: #1e3a8a !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
            
            .table td.text-right-align { text-align: right !important; padding-right: 12px !important; }
            .table td.text-center-align { text-align: center !important; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="printZone" class="container-fluid">
        <div class="page-header">Godown Wise Billwise Settlement History (NAFED)</div>

        <div class="print-header-block">
            <h2>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4>NAFED Date Wise Billwise Settlement Details History</h4>
            <p style="margin:0; font-size:13px; font-weight:bold; color:#334155; text-align: center;">Payment Date Context: <asp:Label ID="lblPrintGodown" runat="server"></asp:Label></p>
            <hr style="border:1px solid #1e3a8a; margin-top:5px; margin-bottom:15px; opacity: 1 !important;"/>
        </div>

        <div class="report-panel">
            <div class="meta-info-strip d-flex justify-content-between align-items-center">
                <div>Payment Received Date Target: <span class="text-primary"><asp:Label ID="lblGodownName" runat="server"></asp:Label></span></div>
                <div><asp:LinkButton ID="lnkBack" runat="server" CssClass="btn btn-outline-secondary btn-sm" OnClick="lnkBack_Click" Style="font-weight:bold;">Back To Summary</asp:LinkButton></div>
            </div>

            <div class="btn-area">
                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export Bills To Excel" OnClick="btnExcel_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print This Page" OnClientClick="window.print(); return false;" />
            </div>

            <div class="grid-scroller-container">
                <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover bg-white mb-0" 
                    ShowFooter="false" OnRowDataBound="gvDetails_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle Width="50px" CssClass="text-center-align" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="District Name" HeaderText="District" />
                        <asp:BoundField DataField="Depot Name" HeaderText="Branch" />
                        <asp:BoundField DataField="Godown Name" HeaderText="Godown Name" />
                        <asp:BoundField DataField="Total Storage Charges Bill Amount" HeaderText="Bill Amount" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        
                        <asp:TemplateField HeaderText="Deduction Status From BM">
                            <ItemTemplate>Approved</ItemTemplate>
                            <ItemStyle CssClass="text-center-align" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="PSS Bill Amount" HeaderText="PSS Amount" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="PSF Bill Amount" HeaderText="PSF Amount" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="TDS Deduction From NAFED" HeaderText="TDS Deduction" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Other Deduction From NAFED" HeaderText="Other Deduction" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Received Payment From NAFED" HeaderText="Payment From NAFED" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        
                        <asp:TemplateField HeaderText="Owner Payment Status">
                            <ItemTemplate>Credited</ItemTemplate>
                            <ItemStyle CssClass="text-center-align" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit Date">
                            <ItemTemplate><%= Request.QueryString["PayDate"] %></ItemTemplate>
                            <ItemStyle CssClass="text-center-align" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>