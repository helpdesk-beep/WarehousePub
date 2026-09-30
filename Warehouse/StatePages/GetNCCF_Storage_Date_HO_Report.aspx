<%@ Page Title="NCCF Storage Charges Monitoring Dashboard" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="~/StatePages/GetNCCF_Storage_Date_HO_Report.aspx.cs" Inherits="StatePages_GetNCCF_Storage_Date_HO_Report" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 20px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center; margin-bottom: 20px; font-family: 'Segoe UI', Arial, sans-serif; }
        
        /* Corporate Blue Grid System */
        .grid-view { font-family: 'Segoe UI', Arial; border-collapse: collapse; width: 100%; background-color: #ffffff; }
        html body .container-fluid .report-panel table.grid-view tr th, .grid-view th {
            background: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 10px !important;
            border: 1px solid #172554 !important; padding: 10px 4px !important; text-transform: uppercase;
        }
        .grid-view td { padding: 6px 8px; border: 1px solid #cbd5e1; font-size: 11px; color: #333333; vertical-align: middle !important; }
        .grid-view tr:nth-child(even) { background-color: #f8fafc; }
        
        /* Text Alignment Overrides */
        .text-right-align { text-align: right !important; padding-right: 8px !important; white-space: nowrap !important; }
        .text-center-align { text-align: center !important; white-space: nowrap !important; }
        
        /* Grand Total Formatting Layer */
        .grandtotal-row { background-color: #dbeafe !important; font-weight: bold !important; color: #1e3a8a !important; }
        .grandtotal-row td { background-color: #dbeafe !important; border-top: 2px solid #2563eb !important; border-bottom: 2px solid #1e3a8a !important; font-weight: bold !important; }

        .report-panel { background: #fff; padding: 15px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); }
        .filter-panel { background: #f1f5f9; padding: 12px; border-radius: 5px; border: 1px solid #cbd5e1; margin-bottom: 15px; }
        .btn-area { text-align: right; }
        .grid-scroller-container { width: 100% !important; overflow-x: auto !important; display: block !important; border: 1px solid #e2e8f0; border-radius: 4px; }

        /* PRINT MANAGEMENT SYSTEM AUTO-SCALING ENGINE */
        @media print {
            @page { size: landscape; margin: 10mm 5mm 10mm 5mm; }
            body *, html * { visibility: hidden; height: auto !important; background-color: transparent !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100% !important; zoom: 52%; display: block !important; }
            
            .btn-area, .page-header, .filter-panel { display: none !important; height: 0 !important; padding: 0 !important; margin: 0 !important; }
            
            .print-header-block { display: block !important; width: 100% !important; text-align: center !important; margin-bottom: 15px !important; }
            .print-header-block h2 { color: #1e3a8a !important; font-weight: bold !important; margin: 0 !important; font-size: 24px; }
            .print-header-block h4 { color: #475569 !important; font-weight: bold !important; margin: 6px 0 !important; font-size: 15px; }
            
            .report-panel { box-shadow: none !important; padding: 0 !important; border: none !important; background: none !important; }
            .grid-scroller-container { overflow-x: visible !important; overflow: visible !important; max-width: 100% !important; border: none !important; display: inline !important; }
            
            .grid-view { border-collapse: collapse !important; width: 100% !important; table-layout: auto !important; }
            thead { display: table-header-group !important; }
            tr { page-break-inside: avoid !important; }
            .grid-view td { border: 1px solid #94a3b8 !important; color: #000000 !important; font-size: 11px !important; }
            .grid-view td a { color: #000000 !important; text-decoration: none !important; pointer-events: none !important; }

            html body #printZone .report-panel table.grid-view thead tr th { 
                background-color: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important; 
                -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; 
            }
            .grandtotal-row td { background-color: #dbeafe !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="printZone" class="container-fluid mt-2">
        <div class="page-header">NCCF Storage Charges Monitoring Drill-Down Master Ledger</div>

        <asp:HiddenField ID="hfLevel" runat="server" Value="1" />
        <asp:HiddenField ID="hfRegionID" runat="server" />
        <asp:HiddenField ID="hfDistrictID" runat="server" />
        <asp:HiddenField ID="hfBranchID" runat="server" />

        <div class="filter-panel">
            <div class="row g-2 align-items-center">
                <div class="col-auto"><label class="fw-bold small text-muted">From Date:</label></div>
                <div class="col-auto"><asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control form-control-sm text-center fw-bold" Width="110px"></asp:TextBox></div>
                <div class="col-auto"><label class="fw-bold small text-muted">To Date:</label></div>
                <div class="col-auto"><asp:TextBox ID="txtToDate" runat="server" CssClass="form-control form-control-sm text-center fw-bold" Width="110px"></asp:TextBox></div>
                <div class="col-auto">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm px-3" Text="Filter System Records" OnClick="btnSearch_Click" />
                    <asp:LinkButton ID="lnkResetHO" runat="server" CssClass="btn btn-outline-danger btn-sm px-2" Visible="false" OnClick="lnkResetHO_Click">Back To HO Base View</asp:LinkButton>
                </div>
            </div>
        </div>

        <div class="print-header-block">
            <h2>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4>NCCF Storage Charges Monitoring Abstract Status Statement</h4>
            <p style="margin: 4px 0 0 0; font-size: 13px; font-weight: bold; color: #475569;">Scope Context Hierarchy: <asp:Label ID="lblPrintScope" runat="server" Text="State Wide Region Overview"></asp:Label> | Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
            <hr style="border: 1px solid #1e3a8a; margin-top: 5px; opacity: 1 !important;"/>
        </div>

        <div class="report-panel">
            <div class="d-flex justify-content-between align-items-center mb-2">
                <div><span class="badge bg-dark small px-2 py-1 fs-6">Active Node Scope: <asp:Label ID="lblCurrentScope" runat="server" Text="State Wide Region Overview"></asp:Label></span></div>
                <div class="btn-area">
                    <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success btn-sm" Style="background-color: #16a34a;" Text="Export View to Excel" OnClick="btnExcel_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-secondary btn-sm" Text="Print Hardcopy Report" OnClientClick="window.print(); return false;" />
                </div>
            </div>

            <div class="grid-scroller-container">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false" CssClass="table grid-view table-bordered table-striped mb-0" 
                    ShowFooter="false" OnRowDataBound="gvReport_RowDataBound" OnRowCommand="gvReport_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle Width="40px" CssClass="text-center-align" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Target Node Scope Location Desc">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDrill" runat="server" CommandName="DrillDown" CommandArgument='<%# Eval("Target_ID") + "|" + Eval("Display_Name") %>' Text='<%# Eval("Display_Name") %>' Font-Bold="true" ForeColor="#2563eb" Style="text-decoration:none;"></asp:LinkButton>
                                <asp:Label ID="lblPlain" runat="server" Text='<%# Eval("Display_Name") %>' Font-Bold="true" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="noofgdwn" HeaderText="Godown Count" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="NoOfGenerateBill" HeaderText="Generated Bills Count" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="BillAmt" HeaderText="Generated Bill Amt" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="NoOfSUBBill" HeaderText="Submitted Bills Count" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="SUBBillAmt" HeaderText="Submitted Bill Amt" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="PendingBillForSubmisionatBranch" HeaderText="Pending Bills at Branch Count" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="PendingBillAmountForSubmision" HeaderText="Pending Bill Amt at Branch" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="RMsubmitbilltonccf" HeaderText="RM Submitted Count" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="RMsubmitbillAmttonccf" HeaderText="RM Submitted Amt" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="PendingBillForSubmisionatRM" HeaderText="Pending Bills at RM Count" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="PendingBillAmountForSubmisionatRM" HeaderText="Pending Bill Amt at RM" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="NoofbillPaymentReceivedFromNCCF" HeaderText="NCCF Received Count" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="NoofbillPaymentAmountReceivedFromNCCF" HeaderText="NCCF Received Amt" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="PaymentDecuctionbyNCCF" HeaderText="NCCF Deduction Amt" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="TotalNoofPendingBillatNCCF" HeaderText="Total Pending Count at NCCF" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="TotalNoofPendingBillAmountatNCCF" HeaderText="Total Pending Amt at NCCF" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="NoOfBillPayment" HeaderText="PTG Bill Payment Count" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="BillAmtPTG" HeaderText="PTG Bill Payment Amt" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>