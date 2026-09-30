<%@ Page Title="Godown Wise Bill Settlement Details" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="~/Region/ReceivePaymentFromNCCF_Details_For_Rigion.aspx.cs" Inherits="Region_ReceivePaymentFromNCCF_Details_For_Rigion" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center; margin-bottom: 20px; font-family: 'Segoe UI', Arial, sans-serif; }
        
        /* Table Header Formatting Forced */
        html body .container-fluid .report-panel table.table tr th,
        table.table thead tr th, .table th {
            background: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 12px !important;
            border: 1px solid #172554 !important; padding: 10px !important;
        }

        /* Base Data Alignment Rule: Applied to general text/status cells */
        .table td { 
            vertical-align: middle !important; 
            border: 1px solid #e2e8f0 !important; 
            font-size: 12px !important; 
            text-align: left !important; /* Status text fields default left rahenge */
            padding: 8px 10px !important;
        }

        /* 100% FORCE FIX: High priority rules for dynamic inner grid alignments */
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

        .report-panel { background: #fff; padding: 15px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); }
        .btn-area { margin-bottom: 15px; text-align: right; }
        .meta-info-strip { background-color: #f8fafc; border: 1px solid #e2e8f0; padding: 10px 15px; border-radius: 4px; margin-bottom: 15px; font-size: 14px; font-weight: bold; color: #334155; }

        @media print {
            @page { size: landscape; margin: 5mm; }
            body *, html * { visibility: hidden; height: auto !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100%; background: none; padding: 0; margin: 0; zoom: 75%; }
            .btn-area, .page-header, .meta-info-strip { display: none !important; }
            .print-header-block { display: block !important; }
            .report-panel { box-shadow: none !important; padding: 0 !important; border: none !important; }
            .table { border-collapse: collapse !important; width: 100% !important; }
            thead { display: table-header-group !important; }
            tr { page-break-inside: avoid !important; }

            /* STRIP LINK STYLES ON PRINT: Keeps text black and unclickable */
            .table td a, .table tr td a {
                color: #000000 !important;
                text-decoration: none !important;
                pointer-events: none !important;
                cursor: default !important;
            }

            /* Print layout ke liye alignment patterns to lock down data flow */
            .table td.text-right-align { text-align: right !important; padding-right: 12px !important; }
            .table td.text-center-align { text-align: center !important; }

            /* Forces background colors on printed sheets header mapping */
            html body #printZone .report-panel table.table thead tr th,
            html body #printZone .report-panel table.table tr th {
                background-color: #1e3a8a !important;
                color: #ffffff !important;
                font-weight: bold !important;
                text-align: center !important;
                vertical-align: middle !important;
                border: 1px solid #172554 !important;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="printZone" class="container-fluid">

        <div class="page-header">
            Godown Wise Billwise Settlement History (NCCF)
        </div>

        <div class="print-header-block">
            <h2 style="margin:0; font-size:24px; font-weight:bold; color:#1e3a8a;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="margin:6px 0; font-size:16px; color:#475569; font-weight:bold;">Godown Wise Billwise Settlement History As On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></h4>
            <p style="margin:0; font-size:13px; font-weight:bold; color:#334155;">Selected Godown: <asp:Label ID="lblPrintGodown" runat="server"></asp:Label></p>
            <hr style="border:1px solid #1e3a8a; margin-top:5px; margin-bottom:20px;"/>
        </div>

        <div class="report-panel">
            <div class="meta-info-strip d-flex justify-content-between align-items-center">
                <div>Selected Godown Name: <span class="text-primary"><asp:Label ID="lblGodownName" runat="server"></asp:Label></span></div>
                <div><asp:LinkButton ID="lnkBack" runat="server" CssClass="btn btn-outline-secondary btn-sm" OnClick="lnkBack_Click" Style="font-weight:bold;">Back To Summary</asp:LinkButton></div>
            </div>

            <div class="btn-area">
                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export Godown Bills To Excel" OnClick="btnExcel_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print This Page" OnClientClick="window.print(); return false;" />
            </div>

            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover bg-white" 
                ShowFooter="false" OnRowDataBound="gvDetails_RowDataBound" OnDataBound="gvDetails_DataBound">
                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        <ItemStyle Width="50px" CssClass="text-center-align" />
                    </asp:TemplateField>
                     <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name" ItemStyle-CssClass="text-center-align" />
                    <asp:BoundField DataField="Total Storage Charges Bill" HeaderText="Bill Number" ItemStyle-CssClass="text-center-align" />
                    <asp:BoundField DataField="Total Storage Charges Bill Amount" HeaderText="Bill Amount" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                    <asp:BoundField DataField="Month_Name" HeaderText="Bill Month" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                    <asp:BoundField DataField="Deduction Status From BM MPWLC" HeaderText="Deduction Status From BM" />
                    <asp:BoundField DataField="PSS Bill Amount" HeaderText="PSS Amount" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                    <asp:BoundField DataField="PSF Bill Amount" HeaderText="PSF Amount" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                    <asp:BoundField DataField="TDS Deduction From NCCF" HeaderText="TDS Deduction" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                    <asp:BoundField DataField="Other Deduction From NCCF" HeaderText="Other Deduction" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                    <asp:BoundField DataField="Received Payment From NCCF" HeaderText="Payment From NCCF" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                    <asp:BoundField DataField="Payment Status From MPWLC to Godown Owner" HeaderText="Owner Payment Status / Credit Amt" />
                    <asp:BoundField DataField="Payment Date" HeaderText="Credit Date" ItemStyle-CssClass="text-center-align" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>