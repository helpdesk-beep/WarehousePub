<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/StatePages/Rpt_Godown_Wise_Payment_Details_NCCF_For_HO.aspx.cs" Inherits="Reports_States_Rpt_Godown_Wise_Payment_Details_NCCF_For_HO" EnableEventValidation="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Godown Wise Payment Details</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; }
        .print-header-block { display: none; text-align: center; margin-bottom: 20px; width: 100% !important; }
        
        /* Table Header Formatting Forced */
        html body table.table tr th, table.table thead tr th, .table th {
            background: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 12px !important;
            border: 1px solid #172554 !important; padding: 10px !important; white-space: normal !important;
        }
        .table td { 
            vertical-align: middle !important; border: 1px solid #e2e8f0 !important; font-size: 12px !important; 
            text-align: left !important; padding: 8px 10px !important; white-space: normal !important; word-break: break-word !important; 
        }
        
        /* High Priority Specificity Rules for Alignments */
        html body table.table tr td.text-right-align, .table td.text-right-align, .text-right-align { text-align: right !important; padding-right: 12px !important; white-space: nowrap !important; }
        html body table.table tr td.text-center-align, .table td.text-center-align, .text-center-align { text-align: center !important; white-space: nowrap !important; }

        .report-panel { background: #fff; padding: 20px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); }
        .grid-scroller-container { width: 100% !important; max-width: 100% !important; overflow-x: auto !important; display: block !important; border: 1px solid #e2e8f0; border-radius: 4px; }
        .meta-info-strip { background-color: #f8fafc; border: 1px solid #e2e8f0; padding: 10px 15px; border-radius: 4px; margin-bottom: 15px; font-size: 14px; font-weight: bold; color: #334155; }
        .btn-area { text-align: right; margin-bottom: 15px; }

        @media print {
            @page { size: landscape; margin: 5mm; }
            body *, html * { visibility: hidden; height: auto !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100%; background: none; zoom: 75%; }
            .btn-area, .page-header, .meta-info-strip { display: none !important; }
            
            .print-header-block { display: block !important; text-align: center !important; width: 100% !important; }
            .print-header-block * { text-align: center !important; }
            
            .report-panel { box-shadow: none !important; padding: 0 !important; border: none !important; }
            .table { border-collapse: collapse !important; width: 100% !important; table-layout: auto !important; }
            thead { display: table-header-group !important; }
            tr { page-break-inside: avoid !important; }
            
            .grid-scroller-container { overflow-x: visible !important; border: none !important; display: inline !important; }
            .table td.text-right-align { text-align: right !important; padding-right: 12px !important; }
            .table td.text-center-align { text-align: center !important; }

            html body #printZone table.table thead tr th {
                background-color: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important;
                -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="printZone" class="container-fluid mt-4">
            
            <div class="page-header">
                Godown Wise NCCF Payment Received Details
            </div>

            <div class="print-header-block">
                <h2 style="margin:0 auto; font-size:24px; font-weight:bold; color:#1e3a8a; text-align:center !important;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
                <h4 style="margin:6px auto; font-size:16px; color:#475569; text-align:center !important;">Godown Wise NCCF Payment Received Details</h4>
                <p style="margin:5px auto 0 auto; font-size:12px; font-weight:bold; text-align:center !important; color:#334155;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
                <hr style="border:1px solid #1e3a8a; margin-top:10px; margin-bottom:20px; width:100%;"/>
            </div>

            <div class="meta-info-strip">
                Selected Settlement Payment Date: <span class="text-primary"><asp:Label ID="lblTargetDate" runat="server"></asp:Label></span>
            </div>

            <div class="report-panel">
                <div class="btn-area">
                    <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print Report" OnClientClick="window.print(); return false;" />
                </div>

                <div class="grid-scroller-container">
                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-bordered table-striped table-hover mb-0" ShowFooter="false"
                        OnRowDataBound="gvDetails_RowDataBound" OnDataBound="gvDetails_DataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                <ItemStyle Width="50px" CssClass="text-center-align" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="Regionnm" HeaderText="Region Name" />
                            <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                            <asp:BoundField DataField="DepotName" HeaderText="Depot Name" />
                            <asp:BoundField DataField="Godown" HeaderText="Godown Name & ID" />
                            <asp:BoundField DataField="Bill_Number" HeaderText="Bill Number" ItemStyle-CssClass="text-center-align" />
                            <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" ItemStyle-CssClass="text-center-align" />
                            <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" ItemStyle-CssClass="text-center-align" />
                            <asp:BoundField DataField="Month_Name" HeaderText="Bill Month" ItemStyle-CssClass="text-center-align" />
                            <asp:BoundField DataField="Amount" HeaderText="Approved Amount" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
    </form>
</body>
</html>