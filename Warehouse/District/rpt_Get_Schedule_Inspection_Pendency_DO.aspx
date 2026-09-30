<%@ Page Title="District Inspection Pendency Report" Language="C#" MasterPageFile="~/MasterPage/CollectorMasterPage.master" AutoEventWireup="true" CodeFile="~/District/rpt_Get_Schedule_Inspection_Pendency_DO.aspx.cs" Inherits="District_rpt_Get_Schedule_Inspection_Pendency_DO" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        html body .container-fluid .report-panel table.table tr th, table.table thead tr th, .table th {
            background: #2563eb !important;
            color: #ffffff !important;
            font-weight: bold !important;
            text-align: center !important;
            vertical-align: middle !important;
            font-size: 12px !important;
            border: 1px solid #1e40af !important;
            padding: 10px !important;
        }
        .table td {
            vertical-align: middle !important;
            border: 1px solid #e2e8f0 !important;
            font-size: 12px !important;
            text-align: left !important;
            padding: 8px 10px !important;
        }
        .text-right-align {
            text-align: right !important;
            padding-right: 12px !important;
            white-space: nowrap !important;
        }
        .text-center-align {
            text-align: center !important;
            white-space: nowrap !important;
        }
        .footer-style td {
            background-color: #eff6ff !important;
            color: #1e3a8a !important;
            font-weight: bold !important;
            font-size: 12px !important;
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
            margin-bottom: 15px !important;
            border: 1px solid #e2e8f0 !important;
            border-radius: 4px !important;
            display: block !important;
        }
        .form-control {
            width: 100%;
            height: 36px;
            border: 1px solid #cbd5e1;
            border-radius: 4px;
            padding: 6px 10px;
            font-size: 13px;
            background: #fff;
        }
        .row {
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
            align-items: end;
        }
        .col-md-2 { width: 16%; }
        .col-md-3 { width: 24%; }

        @media print {
            @page { size: portrait; margin: 10mm 8mm 10mm 8mm; }
            body *, html * { visibility: hidden; height: auto !important; background-image: none !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100% !important; padding: 0 !important; margin: 0 !important; }
            .btn-area, .page-header, .search-row { display: none !important; }
            .print-header-block { display: block !important; width: 100% !important; }
            .report-panel { box-shadow: none !important; padding: 0 !important; border: none !important; background: transparent !important; }
            .grid-scroller-container { overflow: visible !important; max-width: 100% !important; border: none !important; display: block !important; }
            .table { border-collapse: collapse !important; width: 100% !important; table-layout: auto !important; }
            html body .container-fluid .report-panel table.table tr th, .table th { background-color: #2563eb !important; color: #ffffff !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
            .footer-style td { background-color: #eff6ff !important; color: #1e3a8a !important; -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important; }
            thead { display: table-header-group !important; }
            tfoot { display: table-footer-group !important; }
            tr { page-break-inside: avoid !important; }
        }
    </style>

    <div id="printZone" class="container-fluid mt-3">
        <div class="page-header">District Office - Inspection Pendency Summary Report</div>
        
        <div class="print-header-block">
            <h2 style="margin: 0 auto; font-size: 22px; font-weight: bold; color: #1e3a8a; text-align: center;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="margin: 6px auto; font-size: 15px; color: #475569; text-align: center;">District Level Branch-wise Scheduled Inspection Status (FY 2026-27)</h4>
            <p style="margin: 5px auto 0 auto; font-size: 11px; font-weight: bold; color: #334155; text-align: center;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
            <hr style="border: 1px solid #1e3a8a; margin-top: 10px; margin-bottom: 20px; width: 100%;" />
        </div>

        <div class="row search-row" style="margin-bottom: 15px;">
            <div class="col-md-3">
                <label style="font-weight: bold;">Inspection Quarter</label>
                <asp:DropDownList ID="ddlQuarter" runat="server" CssClass="form-control">
                    <asp:ListItem Text="--Select Quarter--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="1st Quarter" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2nd Quarter" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3rd Quarter" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4th Quarter" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Half Yearly Inspection" Value="5"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2" style="padding-top: 25px;">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
            </div>
        </div>

        <div class="report-panel">
            <div class="btn-area">
                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; color: white; border: none; padding: 6px 12px; cursor: pointer; border-radius: 4px;" Text="Export To Excel" OnClick="btnExcel_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; color: white; border: none; padding: 6px 12px; cursor: pointer; border-radius: 4px;" Text="Print Report" OnClientClick="window.print(); return false;" />
            </div>
            
            <div class="grid-scroller-container">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered table-striped table-hover mb-0" Width="100%"
                    ShowFooter="true" FooterStyle-CssClass="footer-style" DataKeyNames="Region_ID,District_id,Inspection_type_ID,Verification_Type,Financial_year"
                    EmptyDataText="No record found for inspection pendency." OnRowDataBound="gvReport_RowDataBound" OnRowCommand="gvReport_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                                <asp:HiddenField runat="server" ID="hdnRegion_ID" Value='<%# Eval("Region_ID") %>' />
                                <asp:HiddenField runat="server" ID="hdnDistrict_ID" Value='<%# Eval("District_id") %>' />
                                <asp:HiddenField runat="server" ID="hdnInspection_type_ID" Value='<%# Eval("Inspection_type_ID") %>' />
                                <asp:HiddenField runat="server" ID="hdnVerification_Type" Value='<%# Eval("Verification_Type") %>' />
                                <asp:HiddenField runat="server" ID="hdnFinancial_year" Value='<%# Eval("Financial_year") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="60px" CssClass="text-center-align" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Regionnm" HeaderText="Region Name" NullDisplayText="N/A" />
                        <asp:BoundField DataField="District_Name" HeaderText="District Name" NullDisplayText="N/A" />
                        <asp:BoundField DataField="Inspection_Status" HeaderText="Inspection Interval / Status" />
                        <asp:BoundField DataField="NoofallottedInspection" HeaderText="Total Allotted Inspections" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="NoofCompleteInspection" HeaderText="Completed Inspections" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" FooterStyle-CssClass="text-right-align" />
                        <asp:TemplateField HeaderText="Pending Inspections">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkPending" runat="server" Text='<%# Eval("NoofpendingInspection") %>' CommandName="Pending" CommandArgument='<%# Container.DataItemIndex %>' ForeColor="Blue" Font-Underline="true"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle CssClass="text-right-align" />
                            <FooterStyle CssClass="text-right-align" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>