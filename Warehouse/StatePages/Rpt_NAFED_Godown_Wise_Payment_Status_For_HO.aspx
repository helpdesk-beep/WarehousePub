<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_NAFED_Godown_Wise_Payment_Status_For_HO.aspx.cs" Inherits="StatePages_Rpt_NAFED_Godown_Wise_Payment_Status_For_HO" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MPWLC - NAFED Godown Wise Payment Status Report</title>
    <style type="text/css">
        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: 11px;
            background-color: #f4f6f9;
            color: #000000;
            padding: 10px;
        }

        /* MPWLC Official Header Styling */
        .header-card {
            background-color: #ffffff;
            border: 1px solid #dcdfe6;
            border-radius: 6px;
            padding: 12px;
            text-align: center;
            margin-bottom: 10px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.05);
        }

        .header-hi {
            font-size: 16px;
            font-weight: bold;
            color: #1a365d;
            margin-bottom: 3px;
        }

        .header-en {
            font-size: 17px;
            font-weight: bold;
            color: #000000;
            letter-spacing: 0.5px;
            margin-bottom: 3px;
        }

        .header-sub {
            font-size: 11px;
            color: #000000;
            font-weight: 600;
        }

        .report-title {
            margin-top: 8px;
            padding-top: 6px;
            border-top: 2px solid #008080;
            font-size: 13px;
            font-weight: bold;
            color: #008080;
            text-transform: uppercase;
        }

        /* Action Toolbar */
        .toolbar {
            display: flex;
            justify-content: space-between;
            align-items: center;
            background-color: #ffffff;
            padding: 8px 12px;
            border: 1px solid #dcdfe6;
            border-bottom: none;
            border-radius: 6px 6px 0 0;
            color: #000000;
        }

        .btn {
            padding: 6px 14px;
            font-size: 11px;
            font-weight: bold;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            margin-left: 5px;
        }

        .btn-excel {
            background-color: #1e7e34;
            color: #ffffff;
        }

        .btn-print {
            background-color: #17a2b8;
            color: #ffffff;
        }

        /* Scrollable Web View Container */
        .grid-container {
            width: 100%;
            max-height: 520px;
            overflow-y: auto;
            overflow-x: auto;
            background: #ffffff;
            border: 1px solid #dcdfe6;
            border-radius: 0 0 6px 6px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.05);
        }

        .custom-grid {
            width: 100%;
            min-width: 1800px;
            border-collapse: collapse;
        }

        /* Screen Sticky Header */
        .custom-grid th {
            position: sticky;
            top: 0;
            z-index: 10;
            background-color: #1a365d;
            color: #ffffff;
            padding: 6px 4px;
            font-size: 10px;
            font-weight: 600;
            text-align: center;
            border: 1px solid #000000;
            white-space: normal;
            word-wrap: break-word;
        }

        .custom-grid td {
            padding: 5px 4px;
            border: 1px solid #b0b0b0;
            font-size: 10.5px;
            color: #000000 !important;
            white-space: normal;
            word-wrap: break-word;
        }

        .custom-grid tr:nth-child(even) {
            background-color: #f8fafc;
        }

        .grid-footer {
            background-color: #008080 !important;
            font-weight: bold;
        }

        .grid-footer td {
            border: 1px solid #005757;
            color: #ffffff !important;
            padding: 6px 4px;
            font-size: 10.5px;
        }

        .text-right { text-align: right; }
        .text-center { text-align: center; }
        .text-left { text-align: left; }

        /* ========================================================= */
        /* PRINT CSS: HEADER REPEATS / FOOTER AT LAST PAGE ONLY      */
        /* ========================================================= */
        @media print {
            @page {
                size: A4 landscape;
                margin: 8mm 6mm 8mm 6mm;
            }

            body {
                padding: 0 !important;
                margin: 0 !important;
                background: #ffffff !important;
                color: #000000 !important;
                font-size: 8px !important;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }

            .toolbar {
                display: none !important;
            }

            .header-card {
                border: none !important;
                box-shadow: none !important;
                padding: 0 0 5px 0 !important;
                margin-bottom: 5px !important;
                text-align: center !important;
            }

            .header-hi { font-size: 12px !important; color: #000000 !important; }
            .header-en { font-size: 13px !important; color: #000000 !important; }
            .report-title { font-size: 10px !important; margin-top: 2px !important; padding-top: 2px !important; color: #008080 !important; }

            .grid-container {
                width: 100% !important;
                max-height: none !important;
                overflow: visible !important;
                border: none !important;
                box-shadow: none !important;
            }

            .custom-grid {
                width: 100% !important;
                min-width: 0 !important;
                table-layout: auto !important;
                border-collapse: collapse !important;
            }

            /* REPEAT HEADER ON EVERY PRINTED PAGE */
            .custom-grid thead {
                display: table-header-group !important;
            }

            .custom-grid tbody {
                display: table-row-group !important;
            }

            /* FOOTER ONLY AT THE END OF TABLE (LAST PAGE) */
            .custom-grid tfoot {
                display: table-row-group !important;
            }

            .custom-grid tr {
                page-break-inside: avoid !important;
            }

            .custom-grid th {
                position: static !important;
                background-color: #1a365d !important;
                color: #ffffff !important;
                font-size: 7.5px !important;
                padding: 4px 2px !important;
                border: 1px solid #000000 !important;
                font-weight: bold !important;
            }

            .custom-grid td {
                font-size: 7.5px !important;
                padding: 3px 2px !important;
                border: 1px solid #666666 !important;
                color: #000000 !important;
            }

            .grid-footer td {
                background-color: #008080 !important;
                color: #ffffff !important;
                font-size: 8px !important;
                border: 1px solid #000000 !important;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- MPWLC Official Header -->
        <div class="header-card">
            <div class="header-hi">मध्य प्रदेश वेयरहाउसिंग एंड लॉजिस्टिक्स कॉर्पोरेशन</div>
            <div class="header-en">MADHYA PRADESH WAREHOUSING & LOGISTICS CORPORATION</div>
            <div class="report-title">NAFED Godown Wise Bill Payment Status Report</div>
        </div>

        <!-- Top Action Toolbar -->
        <div class="toolbar">
            <div>
                <strong>Generated On:</strong>
                <asp:Label ID="lblDate" runat="server"></asp:Label>
            </div>
            <div>
                <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel" CssClass="btn btn-excel" OnClick="btnExportExcel_Click" />
                <button type="button" class="btn btn-print" onclick="window.print();">Print</button>
            </div>
        </div>

        <!-- Responsive Scrollable Table Container -->
        <div class="grid-container">
            <asp:GridView ID="gvGodownStatus" runat="server" AutoGenerateColumns="False"
                CssClass="custom-grid" ShowFooter="True" OnRowDataBound="gvGodownStatus_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <ItemStyle CssClass="text-center" Width="30px" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="Region" HeaderText="Region" ItemStyle-CssClass="text-left" />
                    <asp:BoundField DataField="District" HeaderText="District" ItemStyle-CssClass="text-left" />
                    <asp:BoundField DataField="Branch" HeaderText="Branch" ItemStyle-CssClass="text-left" />
                    <asp:BoundField DataField="godown_Name" HeaderText="Godown Name" ItemStyle-CssClass="text-left" />

                    <asp:BoundField DataField="NoOfGenerateBill" HeaderText="No. of Gen. Bill" ItemStyle-CssClass="text-right" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="BillAmt" HeaderText="Bill Amt" ItemStyle-CssClass="text-right" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="NoOfSUBBill" HeaderText="No. of Sub. Bill" ItemStyle-CssClass="text-right" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="SUBBillAmt" HeaderText="Sub. Bill Amt" ItemStyle-CssClass="text-right" DataFormatString="{0:N2}" />

                    <asp:BoundField DataField="PendingBillForSubmisionatBranch" HeaderText="Pending Bill (Branch)" ItemStyle-CssClass="text-right" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="PendingBillAmountForSubmision" HeaderText="Pending Amt (Branch)" ItemStyle-CssClass="text-right" DataFormatString="{0:N2}" />

                    <asp:BoundField DataField="RMsubmitbilltoNafed" HeaderText="RM Submit Bill (NAFED)" ItemStyle-CssClass="text-right" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="RMsubmitbillAmttoNafed" HeaderText="RM Submit Amt (NAFED)" ItemStyle-CssClass="text-right" DataFormatString="{0:N2}" />

                    <asp:BoundField DataField="PendingBillForSubmisionatRM" HeaderText="Pending Bill (RM)" ItemStyle-CssClass="text-right" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="PendingBillAmountForSubmisionatRM" HeaderText="Pending Amt (RM)" ItemStyle-CssClass="text-right" DataFormatString="{0:N2}" />

                    <asp:BoundField DataField="NoofbillPaymentReceivedFromNafed" HeaderText="Payment Recv. Bill Count" ItemStyle-CssClass="text-right" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="NoofbillPaymentAmountReceivedFromNafed" HeaderText="Payment Recv. Amt" ItemStyle-CssClass="text-right" DataFormatString="{0:N2}" />

                    <asp:BoundField DataField="PaymentDecuctionbyNafed" HeaderText="Deduction by NAFED" ItemStyle-CssClass="text-right" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="TotalNoofPendingBillatNafed" HeaderText="Pending Bill (NAFED)" ItemStyle-CssClass="text-right" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="TotalNoofPendingBillAmountatNafed" HeaderText="Pending Amt (NAFED)" ItemStyle-CssClass="text-right" DataFormatString="{0:N2}" />

                    <asp:BoundField DataField="NoOfBillPayment" HeaderText="No. of Bill Payment" ItemStyle-CssClass="text-right" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="BillAmtPTG" HeaderText="Bill Amt PTG" ItemStyle-CssClass="text-right" DataFormatString="{0:N2}" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>