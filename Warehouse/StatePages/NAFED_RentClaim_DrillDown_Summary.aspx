<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="~/StatePages/NAFED_RentClaim_DrillDown_Summary.aspx.cs" Inherits="StatePages_NAFED_RentClaim_DrillDown_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 18px; font-weight: bold; }
        .print-header-block { display: none; text-align: center; margin-bottom: 20px; }
        
        .grid-view { font-family: 'Segoe UI', Arial; border-collapse: collapse; width: 100%; background-color: #ffffff; }
        html body .container-fluid .report-panel table.grid-view tr th, .grid-view th {
            background: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 10px !important;
            border: 1px solid #172554 !important; padding: 10px 4px !important; text-transform: uppercase;
        }
        .grid-view td { padding: 6px 8px; border: 1px solid #cbd5e1; font-size: 11px; color: #333333; vertical-align: middle !important; }
        .grid-view tr:nth-child(even) { background-color: #f8fafc; }
        
        .text-right-align { text-align: right !important; padding-right: 10px !important; white-space: nowrap !important; }
        .text-center-align { text-align: center !important; white-space: nowrap !important; }
        .grandtotal-row { background-color: #dbeafe !important; font-weight: bold !important; color: #1e3a8a !important; }
        .grandtotal-row td { background-color: #dbeafe !important; border-top: 2px solid #2563eb !important; border-bottom: 2px solid #1e3a8a !important; font-weight: bold !important; }

        .report-panel { background: #fff; padding: 15px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); }
        .filter-panel { background: #f1f5f9; padding: 12px; border-radius: 5px; border: 1px solid #cbd5e1; margin-bottom: 15px; }
        .grid-scroller-container { width: 100% !important; overflow-x: auto !important; display: block !important; border: 1px solid #e2e8f0; }

        @media print {
            @page { size: landscape; margin: 10mm 5mm 10mm 5mm; }
            body *, html * { visibility: hidden; height: auto !important; background-color: transparent !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100% !important; zoom: 55%; display: block !important; }
            .btn-area, .page-header, .filter-panel { display: none !important; }
            .grid-scroller-container { overflow-x: visible !important; border: none !important; }
            html body #printZone .report-panel table.grid-view thead tr th { background-color: #1e3a8a !important; color: #ffffff !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
            .grandtotal-row td { background-color: #dbeafe !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="printZone" class="container-fluid mt-2">
        <div class="page-header">NAFED Rent Abstract Claim & Settlement Dashboard (Amounts in Crores)</div>

        <asp:HiddenField ID="hfLevel" runat="server" Value="1" />
        <asp:HiddenField ID="hfRegionID" runat="server" />
        <asp:HiddenField ID="hfDistrictID" runat="server" />
        <asp:HiddenField ID="hfBranchID" runat="server" />

        <div class="filter-panel">
            <div class="row g-2 align-items-center">
                <div class="col-auto"><label class="fw-bold small text-muted">From Date:</label></div>
                <div class="col-auto"><asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control form-control-sm text-center" Width="110px"></asp:TextBox></div>
                <div class="col-auto"><label class="fw-bold small text-muted">To Date:</label></div>
                <div class="col-auto"><asp:TextBox ID="txtToDate" runat="server" CssClass="form-control form-control-sm text-center" Width="110px"></asp:TextBox></div>
                <div class="col-auto">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm px-3" Text="Filter Ledger" OnClick="btnSearch_Click" />
                    <asp:LinkButton ID="lnkResetHO" runat="server" CssClass="btn btn-outline-danger btn-sm px-2" Visible="false" OnClick="lnkResetHO_Click">Back To Top HO View</asp:LinkButton>
                </div>
            </div>
        </div>

        <div class="print-header-block">
            <h2>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4>NAFED Storage & Owners Rent Settlement Abstract Statement</h4>
            <p style="margin:4px 0 0 0; font-size:12px; font-weight:bold; color:#475569;">Scope: <asp:Label ID="lblPrintScope" runat="server" Text="State Wide Region Overview"></asp:Label> | Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
            <hr style="border: 1px solid #1e3a8a; opacity: 1 !important;"/>
        </div>

        <div class="report-panel">
            <div class="d-flex justify-content-between align-items-center mb-2">
                <div><span class="badge bg-dark small px-2 py-1 fs-6">Scope: <asp:Label ID="lblCurrentScope" runat="server" Text="State Wide Region Overview"></asp:Label></span></div>
                <div class="btn-area">
                    <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success btn-sm" Style="background-color: #16a34a;" Text="Export to Excel" OnClick="btnExcel_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-secondary btn-sm" Text="Print Report" OnClientClick="window.print(); return false;" />
                </div>
            </div>

            <div class="grid-scroller-container">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false" CssClass="table grid-view table-bordered table-striped mb-0" 
                    ShowFooter="false" OnRowDataBound="gvReport_RowDataBound" OnRowCommand="gvReport_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No."><ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate><ItemStyle Width="40px" CssClass="text-center-align" /></asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Target Node Scope Location Desc">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDrill" runat="server" CommandName="DrillDown" CommandArgument='<%# Eval("Target_ID") + "|" + Eval("Display_Name") %>' Text='<%# Eval("Display_Name") %>' Font-Bold="true" ForeColor="#2563eb" Style="text-decoration:none;"></asp:LinkButton>
                                <asp:Label ID="lblPlain" runat="server" Text='<%# Eval("Display_Name") %>' Font-Bold="true" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Total_Godowns_Count" HeaderText="Godown Count" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="TotalBillAmountSubmittedtoNAFED" HeaderText="Submitted To NAFED (In Cr.)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="AmountRecivedFromNAfed" HeaderText="Received From NAFED (In Cr.)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="PendingatNafed" HeaderText="Pending at NAFED (In Cr.)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="TotalRentBillAmountD" HeaderText="Total Rent Bill Amount (In Cr.)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="TotalAmountPassedbyRMAfterAllDeduction" HeaderText="Passed By RM After Deductions (In Cr.)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="PendingatRM" HeaderText="Pending at RM (In Cr.)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="TotalDeductionbyRM" HeaderText="Total Deduction By RM (In Cr.)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="PaytogodownOwner" HeaderText="Pay To Godown Owner (In Cr.)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="PendingatRMForPaytogodownOwner" HeaderText="Pending at RM For Owner Pay (In Cr.)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

