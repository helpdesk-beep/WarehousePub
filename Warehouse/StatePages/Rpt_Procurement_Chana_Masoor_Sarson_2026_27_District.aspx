<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/StatePages/Rpt_Procurement_Chana_Masoor_Sarson_2026_27_District.aspx.cs" Inherits="StatePages_Rpt_Procurement_Chana_Masoor_Sarson_2026_27_District" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MPWLC District Report</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    
    <style>
        body { background-color: #f8f9fa; font-family: 'Segoe UI', Arial, sans-serif; }
        
        /* Layout Styling */
        .main-container { background: white; padding: 15px; border-radius: 8px; }
        .header-table { width: 100%; border-bottom: 2px solid #1a5276; margin-bottom: 5px; }
        .org-title { color: #1a5276; font-size: 22px; font-weight: bold; text-transform: uppercase; margin: 0; }
        
        /* GridView Styling */
        .Grid { width: 100%; border-collapse: collapse !important; table-layout: auto; margin-top: 10px; }
        .Grid th { background-color: #1a5276 !important; color: white !important; padding: 8px; border: 1px solid #444 !important; text-align: center; font-size: 13px; }
        .Grid td { padding: 6px 8px; border: 1px solid #ccc !important; font-size: 12px; color: #000; }
        .footer-style td { background-color: #f2f2f2 !important; font-weight: bold !important; border-top: 2px solid #000 !important; }

        /* Blue Header Removal Logic */
        tr[style*="background-color:#3AC0F2"], tr[style*="background-color: #3ac0f2"] {
            display: none !important;
        }

        /* PRINT OPTIMIZATION */
        @media print {
            @page { size: A4; margin: 1cm; }
            body { background: white !important; }
            .no-print { display: none !important; }
            .main-container { padding: 0 !important; box-shadow: none !important; }
            
            /* Table Flow */
            .Grid { page-break-inside: auto; }
            .Grid tr { page-break-inside: avoid !important; page-break-after: auto; }
            .Grid thead { display: table-header-group; } 
            
            /* Anti-Gap */
            .header-table { page-break-after: avoid; }
            .Grid td, .Grid th { orphans: 3; widows: 3; }
        }

        .no-print { margin-bottom: 10px; display: flex; gap: 10px; padding: 8px; background: #e9ecef; border-radius: 5px; }
    </style>

    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script type="text/javascript">
        // Reliable Excel Export
        function exportToExcel() {
            var tab = document.getElementById('<%=GridView1.ClientID %>');
            var html = tab.outerHTML.replace(/<tr style="background-color:#3AC0F2;">.*?<\/tr>/gi, "");

            var blob = new Blob([html], { type: 'application/vnd.ms-excel' });
            var url = URL.createObjectURL(blob);
            var a = document.createElement('a');
            a.href = url;
            a.download = "Rpt_District_Procurement_2025_26.xls";
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
                        <td style="width: 70px; padding: 5px;">
                            <img src="../images/mpwlc.png" style="width: 60px;" />
                        </td>
                        <td class="text-center">
                            <h1 class="org-title">M.P. Warehousing & Logistics Corporation</h1>
                            <div style="font-size: 15px; font-weight: 600;">Chana, Masoor, Sarson WHR Status Procurement 2025-26 (District Level)</div>
                        </td>
                        <td style="width: 180px; text-align: right; font-size: 11px;">
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
                <div class="table-responsive">
                    <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                        AutoGenerateColumns="false" CssClass="table table-bordered Grid">
                        <Columns>  
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="DistrictName" HeaderText="District Name" ItemStyle-Font-Bold="true" />
                            
                            <asp:TemplateField HeaderText="Acceptance Quantity">
                                <ItemTemplate><%# Eval("TotalQty") %></ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="WHR Quantity">
                                <ItemTemplate><%# Eval("AcceptQty") %></ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="% Over Quantity">
                                <ItemTemplate><%# Eval("Averg") %>%</ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" BackColor="#f9f9f9" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="footer-style" />
                    </asp:GridView>
                </div>

                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mt-2" style="font-size:12px;"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>