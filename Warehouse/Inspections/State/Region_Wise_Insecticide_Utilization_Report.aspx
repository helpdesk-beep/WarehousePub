<%@ Page Title="Region Wise Insecticide Utilization Report" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true"
    CodeFile="~/Inspections/State/Region_Wise_Insecticide_Utilization_Report.aspx.cs" Inherits="Inspections_State_Region_Wise_Insecticide_Utilization_Report" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
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

        /* स्क्रीन और ग्रिड हेडर नियम: फ़ोर्स्ड बोल्ड, वाइट और सेंटर्ड */
        html body .container-fluid .report-panel table.report-table tr th,
        table.report-table thead tr th,
        .report-table th {
            background: #1e3a8a !important;
            color: #ffffff !important;
            font-weight: bold !important;
            text-align: center !important;
            vertical-align: middle !important;
            font-size: 12px !important;
            border: 1px solid #172554 !important;
            padding: 10px !important;
        }

        .report-table td {
            text-align: center !important;
            vertical-align: middle !important;
            border: 1px solid #e2e8f0 !important;
            font-size: 12px !important;
        }

        /* दाएँ अलाइनमेंट (संख्याओं के लिए) */
        .text-right-align {
            text-align: right !important;
            padding-right: 8px !important;
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

        /* लैंडस्केप ओवरफ़्लो फिक्स और 85% रिस्पॉन्सिव ज़ूम (Moisture रिपोर्ट की तरह) */
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

            .btn-area, .page-header, .filter-section {
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

            .report-table {
                border-collapse: collapse !important;
                width: 100% !important;
            }

            thead {
                display: table-header-group !important;
            }

            tr {
                page-break-inside: avoid !important;
            }

            html body #printZone .report-panel table.report-table thead tr th {
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
    <div id="printZone" class="container-fluid mt-3">

        <!-- स्क्रीन हेडर -->
        <div class="page-header">
            Region Wise Insecticide Utilization Report (<%= DateTime.Now.ToString("dd-MM-yyyy") %>)
        </div>

        <!-- प्रिंट हेडर ब्लॉक (Moisture रिपोर्ट पैटर्न) -->
        <div class="print-header-block">
            <h2 style="margin: 0; font-size: 24px; font-weight: bold; color: #1e3a8a;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="margin: 6px 0; font-size: 16px; color: #1e3a8a; font-weight: bold;">Region Wise Insecticide Utilization Report</h4>
            <h5 style="margin: 4px 0; font-size: 14px; color: #475569;">Insecticide:
                <asp:Label ID="lblPrintInsecticide" runat="server" Font-Bold="true"></asp:Label>
                | As On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></h5>
            <hr style="border: 1px solid #1e3a8a; margin-top: 5px; margin-bottom: 5px;" />
        </div>

        <div class="report-panel">

            <!-- फ़िल्टर और एक्शन एरिया -->
            <div class="row g-3 align-items-center mb-3 filter-section">
                <div class="col-auto">
                    <label for="ddlInsecticide" class="form-label fw-bold mb-0 fs-4">Select Insecticide:</label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlInsecticide" runat="server" CssClass="form-control fs-4">
                        <asp:ListItem Value="1" Selected="True">Alluminium Phosphide</asp:ListItem>
                        <asp:ListItem Value="2">Melaphion</asp:ListItem>
                        <asp:ListItem Value="3">Deltamethrin</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnSearch" runat="server" Text="View Report" CssClass="btn btn-primary fs-4 px-4" OnClick="btnSearch_Click" />
                </div>
                <div class="col-md-5 text-end btn-area">
                    <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success fs-4" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary fs-4" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print Report" OnClientClick="window.print(); return false;" />
                </div>
            </div>

            <!-- डेटा तालिका -->
            <div class="table-responsive">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-striped table-hover report-table mb-0"
                    EmptyDataText="No data found for the selected Insecticide."
                    ShowFooter="false" OnRowDataBound="gvReport_RowDataBound" OnDataBound="gvReport_DataBound">
                    <HeaderStyle Font-Bold="true" ForeColor="White" BackColor="#1e3a8a" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Regionnm" HeaderText="Region Name" />
                        <asp:BoundField DataField="Insecticide_Name" HeaderText="Insecticide Name" />

                        <asp:BoundField DataField="HO_Transfer_To_RM" HeaderText="HO Transfer To RM" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Received_Quantity_To_RM" HeaderText="Total Received Quantity To RM" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Pending For RM Receiving" HeaderText="Pending For RM Receiving" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Balance at RM" HeaderText="Balance at RM" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total Balance at RM" HeaderText="Total Balance at RM" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="RO_Transfer_to_Branch" HeaderText="RO Transfer to Branch" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Pending For RM Transfer to Branch" HeaderText="Pending For RM Transfer to Branch" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Consumption_TO_OWN_Godown" HeaderText="Consumption TO OWN Godown" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Transfer_To_JVS_Godown" HeaderText="Transfer To JVS Godown" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Pending_At_Branch" HeaderText="Pending At Branch" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
