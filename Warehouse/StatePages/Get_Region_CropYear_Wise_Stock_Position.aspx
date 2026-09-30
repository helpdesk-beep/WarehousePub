<%@ Page Title="Region & Crop Year Wise Stock Position" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Get_Region_CropYear_Wise_Stock_Position.aspx.cs" Inherits="StatePages_Get_Region_CropYear_Wise_Stock_Position" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .card-box {
        background-color: #fff;
        padding: 15px;
        border-radius: 6px;
        box-shadow: 0 0 10px rgba(0,0,0,0.1);
        margin: 10px;
    }
    .card-header-title {
        background-color: #004085;
        color: #ffffff;
        padding: 10px 15px;
        font-size: 16px;
        font-weight: bold;
        border-radius: 4px;
        margin-bottom: 15px;
    }
    .action-panel {
        margin-bottom: 15px;
        display: flex;
        gap: 10px;
    }
    .btn-custom {
        padding: 7px 18px;
        font-size: 13px;
        font-weight: bold;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        display: inline-flex;
        align-items: center;
    }
    .btn-excel {
        background-color: #1e7e34;
        color: white;
    }
    .btn-excel:hover {
        background-color: #155724;
    }
    .btn-print {
        background-color: #117a8b;
        color: white;
    }
    .btn-print:hover {
        background-color: #0c5460;
    }
    
    .table-responsive {
        width: 100%;
        overflow-x: auto;
        border: 1px solid #dee2e6;
        border-radius: 4px;
    }
    .grid-style {
        width: 100%;
        border-collapse: collapse;
        font-family: 'Segoe UI', Arial, sans-serif;
        font-size: 12px;
        white-space: nowrap;
    }
    
    /* Updated Professional Header Style */
    .grid-style th {
        background-color: #004085 !important;
        color: #ffffff !important;
        padding: 9px 5px !important;
        border: 1px solid #002c5c !important;
        text-align: center;
        font-weight: 600;
        font-size: 12px;
    }

    .grid-style td {
        padding: 6px 6px;
        border: 1px solid #dee2e6;
    }
    .grid-style tr:nth-child(even) {
        background-color: #f8f9fa;
    }
    .grid-style tr:hover {
        background-color: #e9ecef;
    }

    /* Footer Row Highlighting */
    .grid-style tr:last-child td {
        background-color: #eaeef3;
        border-top: 2px solid #004085;
    }

    /* Print Formatting */
    /* Print Formatting - Background Color Force Enable */
@media print {
    @page {
        size: landscape;
        margin: 5mm;
    }

    /* Force Browser to Print Background Colors */
    * {
        -webkit-print-color-adjust: exact !important;
        print-color-adjust: exact !important;
    }

    body * {
        visibility: hidden;
    }
    #printArea, #printArea * {
        visibility: visible;
    }
    #printArea {
        position: absolute;
        left: 0;
        top: 0;
        width: 100% !important;
        margin: 0 !important;
        padding: 0 !important;
    }
    .no-print {
        display: none !important;
    }
    .grid-style {
        font-size: 9px !important;
        width: 100% !important;
        table-layout: fixed;
    }

    /* Header Background Color in Print */
    .grid-style th {
        background-color: #004085 !important; /* Dark Navy Blue */
        color: #ffffff !important;            /* Pure White Text */
        border: 0.5pt solid #002c5c !important;
        -webkit-print-color-adjust: exact !important;
        print-color-adjust: exact !important;
    }

    /* Footer Row Background in Print */
    .grid-style tr:last-child td {
        background-color: #eaeef3 !important;
        -webkit-print-color-adjust: exact !important;
        print-color-adjust: exact !important;
    }

    .grid-style td {
        padding: 3px 1px !important;
        border: 0.5pt solid #000 !important;
        word-wrap: break-word;
        white-space: normal !important;
    }
}
</style>

    <script type="text/javascript">
        function printReport() {
            window.print();
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-box">
        <div class="card-header-title">
            Region & Crop Year Wise Stock Position
        </div>

        <!-- Action Buttons -->
        <div class="action-panel no-print">
            <asp:Button ID="btnExportExcel" runat="server" Text="📥 Export to Excel" CssClass="btn-custom btn-excel" OnClick="btnExportExcel_Click" />
            <asp:Button ID="btnPrint" runat="server" Text="🖨️ Print Report" CssClass="btn-custom btn-print" OnClientClick="return printReport();" />
        </div>

        <!-- Report Area -->
        <div id="printArea">
            <div style="text-align:center; margin-bottom:10px;">
                <h3 class="report-title" style="margin: 0; font-family: Arial, sans-serif;">Region & Crop Year Wise Stock Position Report</h3>
                <p style="margin: 3px 0 10px 0; font-size:11px; color:#555;">Date: <%= DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt") %></p>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="gvStockPosition" runat="server" AutoGenerateColumns="False" CssClass="grid-style" 
                    ShowFooter="True" OnRowDataBound="gvStockPosition_RowDataBound" EmptyDataText="No Stock Data Found.">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                            <HeaderStyle Width="30px" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="Region" HeaderText="Region">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="true" Width="100px" />
                            <HeaderStyle Width="100px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="2016-17" HeaderText="2016-17" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="2017-18" HeaderText="2017-18" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="2018-19" HeaderText="2018-19" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="2019-20" HeaderText="2019-20" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="2020-21" HeaderText="2020-21" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="2021-22" HeaderText="2021-22" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="2022-23" HeaderText="2022-23" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="2023-24" HeaderText="2023-24" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="2024-25" HeaderText="2024-25" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="2025-26" HeaderText="2025-26" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="2026-27" HeaderText="2026-27" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:N2}">
                            <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>