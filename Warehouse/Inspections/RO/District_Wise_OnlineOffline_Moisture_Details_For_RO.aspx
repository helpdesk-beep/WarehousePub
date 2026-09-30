<%@ Page Title="District Wise Online & Offline Moisture Details" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO.master" AutoEventWireup="true" CodeFile="~/Inspections/RO/District_Wise_OnlineOffline_Moisture_Details_For_RO.aspx.cs" Inherits="Inspections_RO_District_Wise_OnlineOffline_Moisture_Details_For_RO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
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
            font-family: 'Segoe UI', Arial, sans-serif;
        }

        /* SCREEN THEME RULES: Forced Bold, White and Centered elements */
        html body .container-fluid .report-panel table.table tr th,
        html body .container-fluid .report-panel table.table tr th a,
        html body .container-fluid .report-panel table.table tr th span,
        table.table thead tr th,
        .table th {
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
            text-align: center !important;
            vertical-align: middle !important;
            border: 1px solid #e2e8f0 !important;
        }

        .report-panel {
            background: #fff;
            padding: 15px;
            border-radius: 5px;
            box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1), 0 2px 4px -1px rgba(0,0,0,0.06);
        }

        .btn-area {
            margin-bottom: 15px;
            text-align: right;
        }

        /* PORTRAIT OVERFLOW FIXED VIA AUTOMATIC LANDSCAPE AND 85% RESPONSIVE ZOOM */
        @media print {
            @page {
                size: landscape;
                margin: 5mm;
            }

            body *, html * {
                visibility: hidden;
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
                zoom: 85%;
            }

            .btn-area, .page-header {
                display: none !important;
            }

            .print-header-block {
                display: block !important;
            }

            .report-panel {
                box-shadow: none !important;
                padding: 0 !important;
                border: none !important;
            }

            .table {
                border-collapse: collapse !important;
                width: 100% !important;
            }

            thead {
                display: table-header-group !important; /* Forces headers repeat splits */
            }

            tr {
                page-break-inside: avoid !important;
            }

            html body #printZone .report-panel table.table thead tr th {
                background: #1e3a8a !important;
                color: #ffffff !important;
                font-weight: bold !important;
                text-align: center !important;
                vertical-align: middle !important;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="printZone" class="container-fluid">

        <div class="page-header">
            District Wise Online & Offline Moisture Details (<%= DateTime.Now.ToString("dd-MM-yyyy") %>)
        </div>

        <div class="print-header-block">
            <h2 style="margin: 0; font-size: 24px; font-weight: bold; color: #1e3a8a;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="margin: 6px 0; font-size: 16px; color: #475569; letter-spacing: 0.5px;">District Wise Online & Offline Moisture Details For HO As On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></h4>
            <%--<p style="margin: 0; font-size: 12px; text-align: right; font-weight: bold; color: #334155;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>--%>
            <hr style="border: 1px solid #1e3a8a; margin-top: 5px; margin-bottom: 5px;" />
        </div>

        <div class="report-panel">
            <div class="btn-area">
                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print Report" OnClientClick="window.print(); return false;" />
            </div>

            <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false"
                CssClass="table table-bordered table-striped table-hover" ShowFooter="false"
                OnRowDataBound="gvReport_RowDataBound" OnDataBound="gvReport_DataBound">
                <HeaderStyle Font-Bold="true" ForeColor="White" BackColor="#2563eb" HorizontalAlign="Center" VerticalAlign="Middle" />
                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="Region Name" HeaderText="Region Name" />
                    <asp:BoundField DataField="District Name" HeaderText="District Name" />
                    <asp:BoundField DataField="Total Stack" HeaderText="Total Stack" />
                    <asp:BoundField DataField="Online Moisture Up to Date" HeaderText="Online Moisture Up to Date" />
                    <asp:BoundField DataField="Offline Moisture Entry" HeaderText="Offline Moisture Entry" />
                    <asp:BoundField DataField="Total Moisture" HeaderText="Total Moisture" />
                    <asp:BoundField DataField="Pending Stack For Moisture" HeaderText="Pending Stack For Moisture" />
                    <asp:BoundField DataField="Stack Moisture Sent to DM MPSCSC/FCI" HeaderText="Stack Moisture Sent to DM MPSCSC/FCI" />
                    <asp:BoundField DataField="FCI Inspected Stack" HeaderText="FCI Inspected Stack" />
                    <asp:BoundField DataField="Pending at FCI" HeaderText="Pending at FCI" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
