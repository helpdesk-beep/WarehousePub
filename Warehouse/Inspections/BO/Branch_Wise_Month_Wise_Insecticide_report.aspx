<%@ Page Title="Branch & Month Wise Insecticide Report" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_BO.master" AutoEventWireup="true" CodeFile="~/Inspections/BO/Branch_Wise_Month_Wise_Insecticide_report.aspx.cs" Inherits="Inspections_BO_Branch_Wise_Month_Wise_Insecticide_report" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center; margin-bottom: 20px; font-family: 'Segoe UI', Arial, sans-serif; }
        
        /* Table Header Formatting Forced */
        html body .container-fluid .report-panel table.table tr th,
        table.table thead tr th, .table th {
            background: #2563eb !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 11px !important;
            border: 1px solid #1e40af !important; padding: 8px !important;
        }

        /* Base Data Alignment Rule: Default for text fields */
        .table td {
            vertical-align: middle !important;
            border: 1px solid #e2e8f0 !important;
            font-size: 11px !important;
            text-align: left !important;
            padding: 6px 8px !important;
        }

        /* High Priority Specificity Rules for Alignment Fix */
        html body .container-fluid .report-panel table.table tr td.text-right-align,
        .table td.text-right-align,
        .text-right-align {
            text-align: right !important;
            padding-right: 10px !important;
        }

        html body .container-fluid .report-panel table.table tr td.text-center-align,
        .table td.text-center-align,
        .text-center-align {
            text-align: center !important;
        }

        .report-panel { background: #fff; padding: 15px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); margin-bottom: 25px; }
        .filter-section { background: #f8fafc; border: 1px solid #e2e8f0; padding: 15px; border-radius: 5px; margin-bottom: 15px; }
        .btn-area { text-align: right; }

        @media print {
            @page { size: landscape; margin: 4mm; }
            body *, html * { visibility: hidden; height: auto !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100%; background: none; padding: 0; margin: 0; zoom: 72%; }
            .btn-area, .page-header, .filter-section { display: none !important; }
            .print-header-block { display: block !important; }
            .report-panel { box-shadow: none !important; padding: 0 !important; border: none !important; }
            .table { border-collapse: collapse !important; width: 100% !important; }
            thead { display: table-header-group !important; }
            tr { page-break-inside: avoid !important; }

            .table td.text-right-align { text-align: right !important; padding-right: 10px !important; }
            .table td.text-center-align { text-align: center !important; }

            html body #printZone .report-panel table.table thead tr th,
            html body #printZone .report-panel table.table tr th {
                background-color: #2563eb !important;
                color: #ffffff !important;
                font-weight: bold !important;
                text-align: center !important;
                vertical-align: middle !important;
                border: 1px solid #1e40af !important;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="printZone" class="container-fluid mt-3">

        <div class="page-header">
            Branch Wise & Month Wise Insecticide Stock Position Report
        </div>

        <div class="print-header-block">
            <h2 style="margin:0; font-size:24px; font-weight:bold; color:#1e3a8a;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="margin:6px 0; font-size:16px; color:#475569;">Branch Wise & Month Wise Insecticide Stock Position Report</h4>
            <p style="margin:0; font-size:12px; text-align:right; font-weight:bold;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
            <hr style="border:1px solid #1e3a8a; margin-top:5px; margin-bottom:20px;"/>
        </div>

        <div class="filter-section">
            <div class="row align-items-center g-3">
                <div class="col-md-3">
                    <label class="fw-bold fs-4 mb-1">Insecticide Item:</label>
                    <asp:DropDownList ID="ddlInsecticide" runat="server" CssClass="form-control fs-4">
                        <asp:ListItem Text="-- All Insecticides --" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Aluminium Phosphide" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Melaphion" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Deltamethrin" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <label class="fw-bold fs-4 mb-1">Month:</label>
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control fs-4">
                        <asp:ListItem Text="-- All Months --" Value="0"></asp:ListItem>
                        <asp:ListItem Text="January" Value="1"></asp:ListItem>
                        <asp:ListItem Text="February" Value="2"></asp:ListItem>
                        <asp:ListItem Text="March" Value="3"></asp:ListItem>
                        <asp:ListItem Text="April" Value="4"></asp:ListItem>
                        <asp:ListItem Text="May" Value="5"></asp:ListItem>
                        <asp:ListItem Text="June" Value="6"></asp:ListItem>
                        <asp:ListItem Text="July" Value="7"></asp:ListItem>
                        <asp:ListItem Text="August" Value="8"></asp:ListItem>
                        <asp:ListItem Text="September" Value="9"></asp:ListItem>
                        <asp:ListItem Text="October" Value="10"></asp:ListItem>
                        <asp:ListItem Text="November" Value="11"></asp:ListItem>
                        <asp:ListItem Text="December" Value="12"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnSearch" runat="server" Text="View Data" CssClass="btn btn-primary w-100 fs-4" OnClick="btnSearch_Click" />
                </div>
                <div class="col-md-4 btn-area mt-4 pt-2 ms-auto">
                    <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success fs-4" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary fs-4" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print Report" OnClientClick="window.print(); return false;" />
                </div>
            </div>
        </div>

        <div class="report-panel">
            <div class="table-responsive">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered table-striped table-hover mb-0" ShowFooter="false"
                    EmptyDataText="No insecticide inventory transactions registered for selected criteria."
                    OnRowDataBound="gvReport_RowDataBound" OnDataBound="gvReport_DataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle Width="40px" CssClass="text-center-align" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                        <asp:BoundField DataField="Branch_Name" HeaderText="Branch Name" />
                        <asp:BoundField DataField="Insecticide_Name" HeaderText="Insecticide Name" />
                        
                        <asp:BoundField DataField="Receipt_Balance_quantity" HeaderText="Receipt Qty" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Receipt_Balance_market_value" HeaderText="Receipt Mkt Val" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Receipt_Balance_value" HeaderText="Receipt Value" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        
                        <asp:BoundField DataField="Consumption_Balance_quantity" HeaderText="Cons. Qty" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Consumption_Balance_market_value" HeaderText="Cons. Mkt Val" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Consumption_Balance_value" HeaderText="Cons. Value" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        
                        <asp:BoundField DataField="Transfer_Balance_quantity" HeaderText="Transfer Qty" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Transfer_Balance_market_value" HeaderText="Transfer Mkt Val" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Transfer_Balance_value" HeaderText="Transfer Value" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        
                        <asp:BoundField DataField="ClosingBalance" HeaderText="Closing Stock Qty" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>