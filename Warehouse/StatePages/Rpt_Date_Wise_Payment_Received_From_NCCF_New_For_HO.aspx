<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/StatePages/Rpt_Date_Wise_Payment_Received_From_NCCF_New_For_HO.aspx.cs" Inherits="Reports_States_Rpt_Date_Wise_Payment_Received_From_NCCF_New_For_HO" EnableEventValidation="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Date Wise NCCF Payment Received Report</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        body { background-color: #f8fafc; font-family: 'Segoe UI', Arial, sans-serif; }
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center; margin-bottom: 20px; width: 100% !important; }
        
        /* Table Headers styling forced */
        html body table.table tr th, table.table thead tr th, .table th {
            background: #2563eb !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 12px !important;
            border: 1px solid #1e40af !important; padding: 10px !important;
        }
        .table td { vertical-align: middle !important; border: 1px solid #e2e8f0 !important; font-size: 12px !important; text-align: left !important; padding: 8px 10px !important; }
        
        /* High-priority Specificity rules for Grid Layouts */
        html body table.table tr td.text-right-align, .table td.text-right-align, .text-right-align { text-align: right !important; padding-right: 12px !important; }
        html body table.table tr td.text-center-align, .table td.text-center-align, .text-center-align { text-align: center !important; }

        .report-panel { background: #fff; padding: 20px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); margin-top: 15px; }
        .filter-section { background: #f8fafc; border: 1px solid #e2e8f0; padding: 15px; border-radius: 5px; margin-bottom: 15px; }
        .btn-area { text-align: right; }
        .grid-scroller-container { width: 100% !important; max-width: 100% !important; overflow-x: auto !important; border-radius: 4px; display: block !important; }

        @media print {
            @page { size: landscape; margin: 5mm; }
            body *, html * { visibility: hidden; height: auto !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100%; background: none; zoom: 80%; }
            .btn-area, .page-header, .filter-section { display: none !important; }
            
            /* Print Header Centering Fix */
            .print-header-block { display: block !important; text-align: center !important; width: 100% !important; }
            .print-header-block * { text-align: center !important; }
            
            .table { border-collapse: collapse !important; width: 100% !important; }
            .grid-scroller-container { overflow-x: visible !important; display: inline !important; border: none !important; }
            .table td a { color: #000000 !important; text-decoration: none !important; pointer-events: none !important; font-weight: normal !important; }
            
            html body #printZone table.table thead tr th { 
                background-color: #2563eb !important; color: #ffffff !important; 
                -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="printZone" class="container-fluid mt-4">
            
            <div class="page-header">
                Date Wise Payment Received From NCCF
            </div>

            <div class="print-header-block">
                <h2 style="margin:0 auto; font-size:24px; font-weight:bold; color:#1e3a8a; text-align:center !important;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
                <h4 style="margin:6px auto; font-size:16px; color:#475569; text-align:center !important;">Date Wise Payment Received From NCCF - Summary</h4>
                <p style="margin:5px auto 0 auto; font-size:12px; font-weight:bold; text-align:center !important; color:#334155;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
                <hr style="border:1px solid #1e3a8a; margin-top:10px; width:100%;"/>
            </div>

            <div class="filter-section">
                <div class="row align-items-center g-3">
                    <div class="col-auto">
                        <label class="fw-bold mb-0">Select Financial Year:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-7 btn-area ms-auto">
                        <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print Report" OnClientClick="window.print(); return false;" />
                    </div>
                </div>
            </div>

            <div class="report-panel">
                <div class="grid-scroller-container">
                    <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-bordered table-striped table-hover mb-0" ShowFooter="false"
                        EmptyDataText="No transaction summaries discovered for selected year criteria."
                        OnRowDataBound="gvReport_RowDataBound" OnDataBound="gvReport_DataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                <ItemStyle Width="50px" CssClass="text-center-align" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Payment Received Date">
                                <ItemTemplate>
                                    <a href='<%# "Rpt_Godown_Wise_Payment_Details_NCCF_For_HO.aspx?PayDate=" + Server.UrlEncode(Convert.ToDateTime(Eval("CreatedOn")).ToString("dd/MM/yyyy")) %>' 
                                       target="_blank" style="font-weight:bold; color:#2563eb; text-decoration:none;">
                                       <%# Eval("CreatedOn") %>
                                    </a>
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center-align" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="TotalNetAmount" HeaderText="Total Net Amount" ItemStyle-CssClass="text-right-align" />
                            <asp:BoundField DataField="TotalTDSDeduction" HeaderText="Total TDS Deduction" ItemStyle-CssClass="text-right-align" />
                            <asp:BoundField DataField="TotalOtherDeduction" HeaderText="Total Other Deduction" ItemStyle-CssClass="text-right-align" />
                            <asp:BoundField DataField="TotalApprovedAmount" HeaderText="Total Approved Amount" ItemStyle-CssClass="text-right-align" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
    </form>
</body>
</html>