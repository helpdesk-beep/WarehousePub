<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Date_Wise_DMO_Markfed_Available_Stack_Report.aspx.cs" Inherits="StatePages_Date_Wise_DMO_Markfed_Available_Stack_Report" EnableEventValidation="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Date Wise DMO Markfed Available Stack Report</title>
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

        /* स्क्रीन और ग्रिड हेडर नियम: फ़ोर्स्ड बोल्ड, वाइट और सही पैडिंग */
        html body .container-fluid .report-panel table.report-table tr th,
        table.report-table thead tr th,
        .report-table th {
            background: #1e3a8a !important;
            color: #ffffff !important;
            font-weight: bold !important;
            text-align: center !important;
            vertical-align: middle !important;
            font-size: 13px !important;
            border: 1px solid #172554 !important;
            padding: 12px !important;
        }

        .report-table td {
            text-align: center !important;
            vertical-align: middle !important;
            border: 1px solid #e2e8f0 !important;
            font-size: 13px !important;
            padding: 10px !important;
        }

        .text-right-align {
            text-align: right !important;
            padding-right: 12px !important;
        }

        .report-panel {
            background: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1);
        }

        @media print {
            @page {
                size: landscape;
                margin: 8mm 5mm 8mm 5mm;
            }

            body *, html * {
                visibility: hidden;
                height: auto !important;
            }

            #printZone, #printZone * {
                visibility: visible;
            }

            #printZone {
                position: relative;
                left: 0;
                top: 0;
                width: 100%;
                background: none;
                padding: 0;
                margin: 0;
                zoom: 100%;
            }

            .btn-area, .page-header, .filter-section, hr {
                display: none !important;
            }

            .print-header-block {
                display: block !important;
                page-break-after: avoid !important;
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

            /* ─── मुख्य सुधार: हेडर रिपीट को रोकने के लिए ─── */
            thead {
                display: table-row-group !important; /* table-header-group से बदलकर table-row-group किया ताकि यह अगले पेज पर रिपीट न हो */
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
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <div id="printZone" class="container-fluid mt-3">

            <div class="page-header">
                Date Wise DMO Markfed Available Stack Report
            </div>

            <div class="print-header-block">
                <h2 style="margin: 0; font-size: 24px; font-weight: bold; color: #1e3a8a;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
                <h4 style="margin: 6px 0; font-size: 16px; color: #1e3a8a; font-weight: bold;">Date Wise DMO Markfed Available Stack Report</h4>
                <h5 style="margin: 4px 0; font-size: 14px; color: #475569;">
                    As On Date: <asp:Label ID="lblPrintDate" runat="server" Font-Bold="true"></asp:Label> 
                    | Depositor ID: 4679
                </h5>
                <hr style="border: 1px solid #1e3a8a; margin-top: 5px; margin-bottom: 5px;" />
            </div>

            <div class="report-panel">
                
                <div class="row g-3 align-items-center mb-4 filter-section">
                    <div class="col-auto">
                        <label class="fw-bold mb-0">Select Date:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSearch" runat="server" Text="View Report" CssClass="btn btn-primary px-4" OnClick="btnSearch_Click" />
                    </div>
                    <div class="col-md-5 text-end btn-area">
                        <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print Report" OnClientClick="window.print(); return false;" />
                    </div>
                </div>

                <div class="table-responsive">
                    <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-bordered table-striped table-hover report-table mb-0"
                        EmptyDataText="No record found for the selected date."
                        ShowFooter="false" OnRowDataBound="gvReport_RowDataBound" OnDataBound="gvReport_DataBound">
                        <HeaderStyle Font-Bold="true" ForeColor="White" BackColor="#1e3a8a" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="60px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Regionnm" HeaderText="Region Name" />
                            <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                            <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />
                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" />
                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year" />
                            <asp:BoundField DataField="Commodity" HeaderText="Commodity" />
                            <asp:BoundField DataField="Available Bags" HeaderText="Available Bags" DataFormatString="{0:N0}" ItemStyle-CssClass="text-right-align" />
                            <asp:BoundField DataField="Available Weight" HeaderText="Available Weight (Quintals)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>