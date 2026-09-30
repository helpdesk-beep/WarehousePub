<%@ Page Title="District Wise Report" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="District_Wise_Online_Offline_Fumigation_Percentage_Report_HO.aspx.cs" Inherits="District_Wise_Online_Offline_Fumigation_Percentage_Report_HO" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            text-align: center;
            margin-bottom: 20px;
            width: 100% !important;
        }

        /* Table Header Formatting Forced */
        html body .container-fluid .report-panel table.table tr th, table.table thead tr th, .table th {
            background: #2563eb !important;
            color: #ffffff !important;
            font-weight: bold !important;
            text-align: center !important;
            vertical-align: middle !important;
            font-size: 11px !important;
            border: 1px solid #1e40af !important;
            padding: 10px !important;
            white-space: normal !important;
        }

        .table td {
            vertical-align: middle !important;
            border: 1px solid #e2e8f0 !important;
            font-size: 11px !important;
            text-align: left !important;
            padding: 8px 10px !important;
            word-break: break-word !important;
            white-space: normal !important;
        }

        html body .container-fluid .report-panel table.table tr td.text-right-align, .table td.text-right-align, .text-right-align {
            text-align: right !important;
            padding-right: 12px !important;
            white-space: nowrap !important;
        }

        html body .container-fluid .report-panel table.table tr td.text-center-align, .table td.text-center-align, .text-center-align {
            text-align: center !important;
            white-space: nowrap !important;
        }

        /* Footer Styling Integration */
        .footer-style td {
            background-color: #eff6ff !important;
            color: #1e3a8a !important;
            font-weight: bold !important;
            font-size: 11px !important;
            border: 1px solid #cbd5e1 !important;
            padding: 10px !important;
        }

        .report-panel {
            background: #fff;
            padding: 15px;
            border-radius: 5px;
            box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1);
            margin-bottom: 25px;
        }

        .btn-area {
            text-align: right;
            margin-bottom: 15px;
        }

        .grid-scroller-container {
            width: 100% !important;
            max-width: 100% !important;
            overflow-x: auto !important;
            overflow-y: hidden !important;
            margin-bottom: 15px !important;
            border: 1px solid #e2e8f0 !important;
            border-radius: 4px !important;
            display: block !important;
        }

        .print-text-only {
            display: none;
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
                zoom: 80%;
            }

            .btn-area, .page-header {
                display: none !important;
            }

            .print-header-block {
                display: block !important;
            }

            .print-header-block * {
                text-align: center !important;
            }

            .report-panel {
                box-shadow: none !important;
                padding: 0 !important;
                border: none !important;
            }

            .table {
                border-collapse: collapse !important;
                width: 100% !important;
                table-layout: auto !important;
            }

            thead { display: table-header-group !important; }
            tfoot { display: table-footer-group !important; }
            tr { page-break-inside: avoid !important; }

            .grid-scroller-container {
                overflow-x: visible !important;
                max-width: none !important;
                border: none !important;
                display: inline !important;
            }

            /* Print View: Hide link URL tags and show bold text labels */
            .table td a {
                display: none !important;
            }

            .print-text-only {
                display: inline !important;
                color: #000000 !important;
            }

            html body #printZone .report-panel table.table thead tr th {
                background-color: #2563eb !important;
                color: #ffffff !important;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }

            html body #printZone .report-panel table.table tr.footer-style td {
                background-color: #eff6ff !important;
                color: #1e3a8a !important;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="printZone" class="container-fluid mt-3">
        <div class="page-header">District Wise Online & Offline Fumigation Progress Report</div>

        <div class="print-header-block">
            <h2 style="margin: 0 auto; font-size: 24px; font-weight: bold; color: #1e3a8a; text-align: center !important;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="margin: 6px auto; font-size: 16px; color: #475569; text-align: center !important;">District Wise Online/Offline Stack Fumigation Progress Summary</h4>
            <p style="margin: 5px auto 0 auto; font-size: 12px; font-weight: bold; text-align: center !important; color: #334155;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
            <hr style="border: 1px solid #1e3a8a; margin-top: 10px; margin-bottom: 20px; width: 100%;" />
        </div>

        <div class="report-panel">
            <div class="btn-area">
                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; color: white; border: none; padding: 6px 12px; cursor: pointer;" Text="Export To Excel" OnClick="btnExcel_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; color: white; border: none; padding: 6px 12px; cursor: pointer;" Text="Print Report" OnClientClick="window.print(); return false;" />
            </div>

            <div class="grid-scroller-container">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered table-striped table-hover mb-0" Width="100%"
                    ShowFooter="true" FooterStyle-CssClass="footer-style"
                    EmptyDataText="No district records found." OnRowDataBound="gvReport_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle Width="50px" CssClass="text-center-align" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Regionnm" HeaderText="Region Name" />
                        <asp:TemplateField HeaderText="District Name">
                            <ItemTemplate>
                                <asp:HyperLink Target="_blank" ID="lnkDistrict" runat="server" 
                                    Text='<%# Eval("District_Name") %>' 
                                    NavigateUrl='<%# "Branch_Wise_Online_Offline_Fumigation_Percentage_Report_HO.aspx?District_Id=" + Eval("District_Id") %>' 
                                    Style="font-weight: bold !important;">
                                </asp:HyperLink>
                                <asp:Label ID="lblDistrictPrint" runat="server" CssClass="print-text-only" 
                                    Text='<%# Eval("District_Name") %>' 
                                    Style="font-weight: bold !important;">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Total_Godown" HeaderText="Total Godowns" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Online_Stack" HeaderText="Total Online Stacks" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Godowns_Covered" HeaderText="Total Godowns Covered" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Online_Fumigated_Stack" HeaderText="Total Online Fumigated Stacks" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Offline_Fumigated" HeaderText="Total Offline Fumigated" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Fumigation_Stack" HeaderText="Total Fumigation Stack" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Pending_Godown" HeaderText="Total Pending Godown" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Pending_Stack_For_Fumigation" HeaderText="Pending Stack For Fumigation" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Opened_Stacks" HeaderText="Total Opened Stacks" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Fumigation_Percentage" HeaderText="Fumigation Progress (%)" DataFormatString="{0:N2}%" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>