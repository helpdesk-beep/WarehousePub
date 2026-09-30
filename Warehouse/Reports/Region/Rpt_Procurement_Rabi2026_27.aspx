<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Reports/Region/Rpt_Procurement_Rabi2026_27.aspx.cs" Inherits="Reports_Region_Rpt_Procurement_Rabi2026_27" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wheat WHR Status 2026-27</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    
    <style>
        body { background-color: #f8f9fa; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }
        
        /* Layout Styling */
        .main-container { background: white; padding: 20px; border-radius: 8px; box-shadow: 0 0 15px rgba(0,0,0,0.1); margin-top: 20px; }
        .header-table { width: 100%; border-bottom: 3px solid #1a5276; margin-bottom: 15px; }
        .org-title { color: #1a5276; font-size: 26px; font-weight: bold; text-transform: uppercase; margin: 0; }
        .report-subtitle { font-size: 18px; color: #444; font-weight: 600; margin-top: 5px; }
        
        /* GridView Styling */
        .Grid { width: 100%; border-collapse: collapse !important; margin-top: 10px; background-color: white; }
        .Grid th { background-color: #1a5276 !important; color: white !important; padding: 12px 8px; border: 1px solid #444 !important; text-align: center; font-size: 14px; vertical-align: middle; }
        .Grid td { padding: 8px; border: 1px solid #ccc !important; font-size: 13px; color: #333; vertical-align: middle; }
        .Grid tr:nth-child(even) { background-color: #f9f9f9; }
        .Grid tr:hover { background-color: #f1f1f1; }
        
        /* Footer styling for totals */
        .footer-style td { background-color: #eee !important; font-weight: bold !important; border-top: 2px solid #000 !important; color: #000; }

        /* Button Styling */
        .no-print { margin-bottom: 20px; display: flex; gap: 10px; padding: 10px; background: #e9ecef; border-radius: 5px; }

        /* PRINT OPTIMIZATION */
        @media print {
            @page { size: A4; margin: 0.5cm; }
            body { background: white !important; margin: 0 !important; }
            .no-print { display: none !important; }
            .main-container { padding: 0 !important; box-shadow: none !important; width: 100%; margin: 0 !important; }
            .header-table { margin-bottom: 10px !important; page-break-after: avoid; }
            .Grid { page-break-before: avoid; margin-top: 0 !important; width: 100% !important; }
            .Grid thead { display: table-header-group; }
            .Grid tr { page-break-inside: avoid !important; }
        }
    </style>

    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script type="text/javascript">
        // Excel Export Logic
        function exportToExcel() {
            var tab = document.getElementById('<%=GridView1.ClientID %>');
            var html = tab.outerHTML;
            var blob = new Blob(['\ufeff', html], { type: 'application/vnd.ms-excel' });
            var url = URL.createObjectURL(blob);
            var a = document.createElement('a');
            a.href = url;
            a.download = "Wheat_Procurement_Report_2026.xls";
            a.click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="main-container">
                
                <table class="header-table">
                    <tr>
                        <td style="width: 100px; padding: 10px; vertical-align: middle;">
                            <img src="../../images/mpwlc.png" style="width: 85px;" />
                        </td>
                        <td class="text-center" style="vertical-align: middle;">
                            <h1 class="org-title">M.P. Warehousing & Logistics Corporation</h1>
                            <div class="report-subtitle">Wheat WHR Status Procurement 2026-27</div>
                        </td>
                        <td style="width: 250px; text-align: right; padding-right: 15px; font-size: 14px; vertical-align: middle;">
                            <strong>Date:</strong> <asp:Label ID="labelName" runat="server"></asp:Label><br />
                            <strong>Unit:</strong> Qty In M.T.
                        </td>
                    </tr>
                </table>

                <div class="no-print d-flex justify-content-end align-items-center">
                    <asp:LinkButton ID="btnback" runat="server" CssClass="btn btn-secondary btn-sm" OnClick="btnback_Click">Back</asp:LinkButton>
                    <button type="button" onclick="exportToExcel()" class="btn btn-success btn-sm ml-2">Export to Excel</button>
                    <button type="button" onclick="window.print()" class="btn btn-danger btn-sm ml-2">Print / Save PDF</button>
                </div>

                <div class="table-responsive">
                    <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                        OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" 
                        AutoGenerateColumns="false" CssClass="table table-bordered Grid">
                        <Columns>  
                            <asp:BoundField DataField="DistrictName" HeaderText="District" />
                            <asp:BoundField DataField="District_Id" HeaderText="District ID" ItemStyle-CssClass="text-center" />
                            <asp:BoundField DataField="Branch" HeaderText="Branch" />
                            
                            <asp:TemplateField HeaderText="Acceptance Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblAQ" runat="server" Text='<%# Eval("TotalQty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="WHR Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblWHRQ" runat="server" Text='<%# Eval("AcceptQty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="% Over Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblAverg" runat="server" Text='<%# Eval("Averg") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="footer-style" />
                    </asp:GridView>
                </div>

                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mt-3 d-block" style="font-weight:bold; font-size: 16px;"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>