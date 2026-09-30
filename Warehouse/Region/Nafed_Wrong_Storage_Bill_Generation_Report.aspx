<%@ Page Title="NAFED Storage Charges & Rent Bill Difference Report" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="Nafed_Wrong_Storage_Bill_Generation_Report.aspx.cs" Inherits="Region_Nafed_Wrong_Storage_Bill_Generation_Report" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center; margin-bottom: 20px; font-family: 'Segoe UI', Arial, sans-serif; }
        
        /* Table Header Formatting Forced for Screen View */
        html body .container-fluid .report-panel table.table tr th,
        table.table thead tr th, .table th {
            background: #2563eb !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 12px !important;
            border: 1px solid #1e40af !important; padding: 10px !important;
        }

        /* Base Data Alignment Rule: Default for text columns (District, Depot, Godown, Commodity) */
        .table td {
            vertical-align: middle !important;
            border: 1px solid #e2e8f0 !important;
            font-size: 12px !important;
            text-align: left !important;
            padding: 8px 10px !important;
        }

        /* High priority rules for Right & Center alignment */
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

        .report-panel { background: #fff; padding: 15px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); margin-bottom: 25px; }
        .btn-area { margin-bottom: 15px; text-align: right; }

        @media print {
            @page { size: landscape; margin: 5mm; }
            body *, html * { visibility: hidden; height: auto !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100%; background: none; padding: 0; margin: 0; zoom: 75%; }
            .btn-area, .page-header { display: none !important; }
            .print-header-block { display: block !important; }
            .report-panel { box-shadow: none !important; padding: 0 !important; border: none !important; }
            .table { border-collapse: collapse !important; width: 100% !important; }
            thead { display: table-header-group !important; }
            tr { page-break-inside: avoid !important; }

            /* Strip link styles on print */
            .table td a, .table tr td a {
                color: #000000 !important;
                text-decoration: none !important;
                pointer-events: none !important;
                cursor: default !important;
            }

            .table td.text-right-align { text-align: right !important; padding-right: 12px !important; }
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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="printZone" class="container-fluid">

        <div class="page-header">
            NAFED Storage Charges & Rent Bill Difference Report
        </div>

        <div class="print-header-block">
            <h2 style="margin: 0; font-size: 24px; font-weight: bold; color: #1e3a8a;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="margin: 6px 0; font-size: 16px; color: #475569;">NAFED Rent vs Storage Bill Difference Summary As On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></h4>
            <hr style="border: 1px solid #1e3a8a; margin-top: 5px; margin-bottom: 20px;" />
        </div>

        <div class="report-panel">
            <div class="btn-area">
                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print Report" OnClientClick="window.print(); return false;" />
            </div>

            <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false"
                CssClass="table table-bordered table-striped table-hover" ShowFooter="false"
                OnRowDataBound="gvReport_RowDataBound" OnDataBound="gvReport_DataBound">
                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        <ItemStyle Width="50px" CssClass="text-center-align" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="Regionnm" HeaderText="District Name" ItemStyle-CssClass="text-center-align" />
                    <asp:BoundField DataField="DepotName" HeaderText="Depot Name" ItemStyle-CssClass="text-center-align" />
                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" ItemStyle-CssClass="text-center-align" />
                    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" ItemStyle-CssClass="text-center-align" />
                    <asp:BoundField DataField="Financial_Year" HeaderText="Fin Year" ItemStyle-CssClass="text-center-align" />
                    <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" ItemStyle-CssClass="text-center-align" />
                    <asp:BoundField DataField="Month_Name" HeaderText="Month" ItemStyle-CssClass="text-center-align" />
                    <asp:BoundField DataField="Storage_Bill" HeaderText="Storage Bill No." ItemStyle-CssClass="text-center-align" />
                    <asp:BoundField DataField="Storage_Bill_Amount" HeaderText="Storage Bill Amount" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                    <asp:BoundField DataField="Rent_Bill" HeaderText="Rent Bill No." ItemStyle-CssClass="text-center-align" />
                    <asp:BoundField DataField="Rent_Bill_Amount" HeaderText="Rent Bill Amount" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                    <asp:BoundField DataField="Difference_Amount" HeaderText="Difference Amount" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>