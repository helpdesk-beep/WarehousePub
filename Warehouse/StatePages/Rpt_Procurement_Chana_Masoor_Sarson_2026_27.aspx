<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/StatePages/Rpt_Procurement_Chana_Masoor_Sarson_2026_27.aspx.cs" Inherits="StatePages_Rpt_Procurement_Chana_Masoor_Sarson_2026_27" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MPWLC Procurement Report</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    
    <style>
        body { background-color: #f8f9fa; font-family: 'Segoe UI', Arial, sans-serif; }
        
        /* Layout Styling */
        .main-container { background: white; padding: 20px; border-radius: 8px; }
        .header-table { width: 100%; border-bottom: 3px solid #1a5276; margin-bottom: 10px; }
        .org-title { color: #1a5276; font-size: 24px; font-weight: bold; text-transform: uppercase; margin: 0; }
        
        /* GridView Styling */
        .Grid { width: 100%; border-collapse: collapse !important; table-layout: auto; }
        .Grid th { background-color: #1a5276 !important; color: white !important; padding: 10px; border: 1px solid #333 !important; text-align: center; font-size: 13px; }
        .Grid td { padding: 8px; border: 1px solid #ccc !important; font-size: 12px; color: #000; }
        .footer-style td { background-color: #eee !important; font-weight: bold; border: 1px solid #333 !important; }

        /* PRINT SETTINGS - Yeh hissa Page Break fix karega */
        @media print {
            @page {
                size: A4;
                margin: 1cm;
            }
            body { background: white !important; }
            .no-print { display: none !important; }
            .main-container { padding: 0 !important; box-shadow: none !important; }
            
            /* Table break prevention */
            .Grid { page-break-inside: auto; border: 1px solid #000 !important; }
            .Grid tr { page-break-inside: avoid; page-break-after: auto; }
            .Grid thead { display: table-header-group; } /* Har page pe header dikhega */
            .Grid tfoot { display: table-footer-group; } /* Har page pe footer dikhega */
            
            /* Unwanted blank space removal */
            .header-table { page-break-after: avoid; }
            br { display: none; }
        }

        .no-print { margin-bottom: 20px; display: flex; gap: 10px; padding: 10px; background: #eee; border-radius: 5px; }
    </style>

    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script type="text/javascript">
        function exportToExcel() {
            var tab = document.getElementById('<%=GridView1.ClientID %>');
            var html = tab.outerHTML;
            var url = 'data:application/vnd.ms-excel,' + encodeURIComponent(html);
            var link = document.createElement('a');
            link.download = "MPWLC_Report.xls";
            link.href = url;
            link.click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">

            <div class="main-container">
                <table class="header-table">
                    <tr>
                        <td style="width: 80px; padding: 5px;">
                            <img src="../../images/mpwlc.png" style="width: 70px;" />
                        </td>
                        <td class="text-center">
                            <h1 class="org-title">M.P. Warehousing & Logistics Corporation</h1>
                            <div style="font-size: 16px; font-weight: 600;">Chana, Masoor, Sarson WHR Status Procurement 2026-27</div>
                        </td>
                        <td style="width: 180px; text-align: right; font-size: 12px;">
                            <strong>Date:</strong> <asp:Label ID="labelName" runat="server"></asp:Label><br />
                            <strong>Unit:</strong> Qty In M.T.
                        </td>
                    </tr>
                </table>
                
            <div class="no-print justify-content-end">
                <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-secondary btn-sm" OnClick="btnback_Click" />
                <button type="button" onclick="exportToExcel()" class="btn btn-success btn-sm">Export to Excel</button>
                <button type="button" onclick="window.print()" class="btn btn-danger btn-sm">Print / Save PDF</button>
            </div>
                <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                    OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated"
                    AutoGenerateColumns="false" CssClass="table table-bordered Grid">
                    <Columns>  
                        <asp:BoundField DataField="Region" HeaderText="Region" />
                        <asp:BoundField DataField="Region_ID" HeaderText="ID" ItemStyle-CssClass="text-center" />
                        <asp:BoundField DataField="DistrictName" HeaderText="District" />
                        <asp:BoundField DataField="Branch" HeaderText="Branch" />
                        <asp:TemplateField HeaderText="Acceptance Qty">
                            <ItemTemplate><%# Eval("TotalQty") %></ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="WHR Qty">
                            <ItemTemplate><%# Eval("AcceptQty") %></ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="% Over Qty">
                            <ItemTemplate><%# Eval("Averg") %>%</ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="footer-style" />
                </asp:GridView>

                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mt-2"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>