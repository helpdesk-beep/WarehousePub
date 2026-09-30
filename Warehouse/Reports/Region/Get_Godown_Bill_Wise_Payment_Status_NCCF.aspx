<%@ Page Title="Godown Bill Wise Payment Status NCCF" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="~/Reports/Region/Get_Godown_Bill_Wise_Payment_Status_NCCF.aspx.cs" Inherits="Reports_Region_Get_Godown_Bill_Wise_Payment_Status_NCCF" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        body {
            background-color: #f8fafc;
            font-family: 'Segoe UI', Arial, sans-serif;
        }

        .page-header {
            background: #1e3a8a;
            color: white !important;
            padding: 12px;
            border-radius: 5px;
            margin-bottom: 15px;
            text-align: center;
            font-size: 22px;
            font-weight: bold;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }

        .print-header-block {
            display: none;
            text-align: center !important;
            width: 100% !important;
            margin-bottom: 20px;
        }

        /* Grid Table Core Alignments Forced Corporate Blue Style */
        .grid-view {
            font-family: 'Segoe UI', Arial, sans-serif;
            border-collapse: collapse;
            width: 100%;
            margin-top: 15px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
            background-color: #ffffff;
        }

            html body .container-fluid .report-panel table.grid-view tr th,
            table.grid-view thead tr th, .grid-view th {
                background: #1e3a8a !important;
                color: #ffffff !important;
                font-weight: bold !important;
                text-align: center !important;
                vertical-align: middle !important;
                font-size: 11px !important;
                border: 1px solid #172554 !important;
                padding: 10px 8px !important;
                white-space: normal !important;
                text-transform: uppercase;
            }

            .grid-view td {
                vertical-align: middle !important;
                border: 1px solid #e2e8f0 !important;
                font-size: 11px !important;
                text-align: left !important;
                padding: 8px 10px !important;
            }

            .grid-view tr:nth-child(even) {
                background-color: #f9f9f9;
            }

            .grid-view tr:hover:not(.subtotal-row):not(.grandtotal-row) {
                background-color: #f1f1f1;
            }

            /* Specificity Alignment Locks */
            html body .container-fluid .report-panel table.grid-view tr td.text-right-align, .grid-view td.text-right-align, .text-right-align {
                text-align: right !important;
                padding-right: 12px !important;
                white-space: nowrap !important;
            }

            html body .container-fluid .report-panel table.grid-view tr td.text-center-align, .grid-view td.text-center-align, .text-center-align {
                text-align: center !important;
                white-space: nowrap !important;
            }

        /* Subtotal and Grand Total Hierarchy Colors */
        .subtotal-row {
            background-color: #fef08a !important;
            font-weight: bold !important;
            color: #000000 !important;
        }

            .subtotal-row td {
                border-top: 2px solid #ca8a04 !important;
                border-bottom: 2px solid #ca8a04 !important;
                font-weight: bold !important;
            }

        .grandtotal-row {
            background-color: #dbeafe !important;
            font-weight: bold !important;
            color: #1e3a8a !important;
        }

            .grandtotal-row td {
                border-top: 2px solid #2563eb !important;
                border-bottom: 2px solid #1e3a8a !important;
                font-weight: bold !important;
            }

        .report-panel {
            background: #fff;
            padding: 25px;
            border-radius: 5px;
            box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1);
            margin-bottom: 25px;
        }

        .filter-section {
            background: #f8fafc;
            border: 1px solid #e2e8f0;
            padding: 15px;
            border-radius: 5px;
            margin-bottom: 15px;
        }

        .btn-area {
            text-align: right;
        }

        .grid-scroller-container {
            width: 100% !important;
            max-width: 100% !important;
            overflow-x: auto !important;
            display: block !important;
            border: 1px solid #e2e8f0;
            border-radius: 4px;
        }

        @media print {
            @page {
                size: landscape;
                margin: 5mm;
            }

            body *, html * {
                visibility: hidden;
                height: auto !important;
            }

            #printZone, #printZone * {
                visibility: visible;
            }

            #printZone {
                position: absolute;
                left: 0;
                top: 0;
                width: 100%;
                background: none;
                padding: 0;
                margin: 0;
                zoom: 70%;
            }

            .btn-area, .page-header, .filter-section {
                display: none !important;
            }

            .print-header-block {
                display: block !important;
                text-align: center !important;
                width: 100% !important;
            }

                .print-header-block * {
                    text-align: center !important;
                }

            .grid-view {
                border-collapse: collapse !important;
                width: 100% !important;
                table-layout: auto !important;
            }

            thead {
                display: table-header-group !important;
            }

            tr {
                page-break-inside: avoid !important;
            }

            .grid-scroller-container {
                overflow-x: visible !important;
                border: none !important;
                display: inline !important;
            }

            .grid-view th {
                background-color: #1e3a8a !important;
                color: #ffffff !important;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }

            .subtotal-row {
                background-color: #fef08a !important;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }

            .grandtotal-row {
                background-color: #dbeafe !important;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="printZone" class="container-fluid mt-3">
        <div class="page-header">Godown Bill Wise Payment Status Logs (NCCF)</div>

        <div class="print-header-block">
            <h2 style="margin: 0 auto; font-size: 24px; font-weight: bold; color: #1e3a8a; text-align: center !important;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="margin: 6px auto; font-size: 16px; color: #475569; text-align: center !important;">Godown Bill Wise Payment Progress Statement Ledger - NCCF</h4>
            <p style="margin: 5px auto 0 auto; font-size: 12px; font-weight: bold; text-align: center !important; color: #334155;">
                From Date:
                <asp:Label ID="lblPrintFromDate" runat="server"></asp:Label>
                | 
                To Date:
                <asp:Label ID="lblPrintToDate" runat="server"></asp:Label>
                | 
                Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %>
            </p>
            <hr style="border: 1px solid #1e3a8a; margin-top: 10px; margin-bottom: 20px; width: 100%;" />
        </div>

        <div class="filter-section">
            <div class="row align-items-end g-3">
                <div class="col-md-3">
                    <label class="fw-bold mb-1">From Date (Payment):</label>
                    <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label class="fw-bold mb-1">To Date (Payment):</label>
                    <asp:TextBox ID="txtToDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary w-100" Style="background-color: #1e3a8a; border-color: #1e3a8a;" Text="Search Ledger" OnClick="btnSearch_Click" />
                </div>
                <div class="col-md-4 text-end btn-area">
                    <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print Report" OnClientClick="window.print(); return false;" />
                </div>
            </div>
        </div>

        <div class="report-panel">
            <div class="grid-scroller-container">
                <asp:GridView ID="gvNCCF" runat="server" AutoGenerateColumns="False"
                    CssClass="grid-view table table-bordered mb-0" ShowFooter="false">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle Width="50px" CssClass="text-center-align" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="District_Name" HeaderText="District" />
                        <asp:BoundField DataField="DepotName" HeaderText="Branch" />
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" />
                        <asp:BoundField DataField="Month_Name" HeaderText="Month" ItemStyle-CssClass="text-center-align" />
                        <asp:BoundField DataField="Total Storage Charges Bill" HeaderText="Bill No" ItemStyle-CssClass="text-center-align" />
                        <asp:BoundField DataField="Deduction Status From BM MPWLC" HeaderText="BM Deduction Status" ItemStyle-CssClass="text-center-align" />

                        <asp:BoundField DataField="Total Storage Charges Bill Amount" HeaderText="Storage Bill Amt" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="PSS Bill Amount" HeaderText="PSS Bill Amt" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="PSF Bill Amount" HeaderText="PSF Bill Amt" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="TDS Deduction From NCCF" HeaderText="TDS Deduct (NCCF)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Other Deduction From NCCF" HeaderText="Other Deduct (NCCF)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Received Payment From NCCF" HeaderText="Recd Payment NCCF" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />

                        <asp:BoundField DataField="Total Rent Bill Amount" HeaderText=" Rent Bill Amount" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total Rent Bill" HeaderText=" Rent Bill" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="TDS_Detuction_Amount" HeaderText="TDS Deduct (MPWLC)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Gain_Detuction_Amount" HeaderText="Loss/Gain Deduct (MPWLC)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Detuction_Amt" HeaderText="Total Deduction From MPWLC" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Payment Status From MPWLC to Godown Owner" HeaderText="Owner Payment Status" />
                        <asp:BoundField DataField="Actual Patment From MPWLC to Godown Owner" HeaderText="Actual Patment to Gpdpwn Owner" />
                        <asp:BoundField DataField="Payment Date" HeaderText="Payment Date" ItemStyle-CssClass="text-center-align" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
