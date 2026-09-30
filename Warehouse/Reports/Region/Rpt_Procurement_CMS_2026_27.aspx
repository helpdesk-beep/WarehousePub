<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Procurement_CMS_2026_27.aspx.cs" Inherits="Reports_Region_Rpt_Procurement_CMS_2026_27" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MPWLC Procurement Report 2026-27</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    
    <style>
        body { background-color: #f8f9fa; font-family: 'Segoe UI', Arial, sans-serif; }
        
        /* Layout Styling */
        .main-container { background: white; padding: 15px; border-radius: 8px; box-shadow: 0 0 10px rgba(0,0,0,0.05); }
        .header-table { width: 100%; border-bottom: 2px solid #1a5276; margin-bottom: 10px; background-color: #fff9e6; }
        .org-title { color: #1a5276; font-size: 22px; font-weight: bold; text-transform: uppercase; margin: 0; }
        
        /* GridView Styling */
        .Grid { width: 100%; border-collapse: collapse !important; table-layout: auto; margin-top: 10px; }
        .Grid th { background-color: #1a5276 !important; color: white !important; padding: 10px; border: 1px solid #444 !important; text-align: center; font-size: 13px; }
        .Grid td { padding: 8px; border: 1px solid #ccc !important; font-size: 12px; color: #000; }
        .footer-style td { background-color: #f2f2f2 !important; font-weight: bold !important; border-top: 2px solid #000 !important; }

        /* Hide the annoying blue strip if it appears */
        tr[style*="background-color:#3AC0F2"], tr[style*="background-color: #3ac0f2"] {
            display: none !important;
        }

        /* PRINT OPTIMIZATION */
        @media print {
            @page { size: A4; margin: 1cm; }
            body { background: white !important; }
            .no-print { display: none !important; }
            .main-container { padding: 0 !important; box-shadow: none !important; }
            .header-table { background-color: white !important; }
            
            /* Table Flow Fixes */
            .Grid { page-break-inside: auto; }
            .Grid tr { page-break-inside: avoid !important; page-break-after: auto; }
            .Grid thead { display: table-header-group; } 
            
            .Grid td, .Grid th { orphans: 3; widows: 3; }
        }

        .no-print { margin-bottom: 15px; display: flex; gap: 10px; padding: 10px; background: #e9ecef; border-radius: 5px; }
    </style>

    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script type="text/javascript">
        function exportToExcel() {
            var tab = document.getElementById('<%=GridView1.ClientID %>');
            // Clean HTML for Excel
            var html = tab.outerHTML.replace(/<tr style="background-color:#3AC0F2;">.*?<\/tr>/gi, "");

            var blob = new Blob([html], { type: 'application/vnd.ms-excel' });
            var url = URL.createObjectURL(blob);
            var a = document.createElement('a');
            a.href = url;
            a.download = "Procurement_CMS_Report_2026_27.xls";
            a.click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid mt-3">

            <div class="main-container">
                <table class="header-table">
                    <tr>
                        <td style="width: 80px; padding: 10px;">
                            <img src="../../images/mpwlc.png" style="width: 70px;" />
                        </td>
                        <td class="text-center">
                            <h1 class="org-title">M.P. Warehousing & Logistics Corporation</h1>
                            <div style="font-size: 16px; font-weight: 600;">Chana, Masoor, Sarson WHR Status Procurement 2026-27</div>
                        </td>
                        <td style="width: 200px; text-align: right; padding-right: 15px; font-size: 12px;">
                            <strong>Date:</strong> <asp:Label ID="labelName" runat="server"></asp:Label><br />
                            <strong>Unit:</strong> Qty In M.T.
                        </td>
                    </tr>
                </table>
                
            <div class="no-print justify-content-end">
                <button type="button" onclick="history.back()" class="btn btn-secondary btn-sm">Back</button>
                <button type="button" onclick="exportToExcel()" class="btn btn-success btn-sm">Export to Excel</button>
                <button type="button" onclick="window.print()" class="btn btn-danger btn-sm">Print / Save PDF</button>
            </div>

                <div class="table-responsive">
                    <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                        OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                        AutoGenerateColumns="false" CssClass="table table-bordered Grid">
                        <Columns>  

                            <asp:BoundField DataField="DistrictName" HeaderText="District" />
                            <asp:BoundField DataField="District_Id" HeaderText="ID" ItemStyle-CssClass="text-center" />
                            <asp:BoundField DataField="Branch" HeaderText="Branch" />
                            
                            <asp:TemplateField HeaderText="Acceptance Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblAQ" runat="server" Text='<%# Eval("TotalQty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="WHR Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblWHRQ" runat="server" Text='<%# Eval("AcceptQty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="% Over Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblAverg" runat="server" Text='<%# Eval("Averg") %>'></asp:Label>%
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="footer-style" />
                    </asp:GridView>
                </div>

                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mt-3 d-block" style="font-weight:bold;"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>