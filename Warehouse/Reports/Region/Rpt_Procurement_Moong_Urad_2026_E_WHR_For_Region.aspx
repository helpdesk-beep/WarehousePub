<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Reports/Region/Rpt_Procurement_Moong_Urad_2026_E_WHR_For_Region.aspx.cs" Inherits="Reports_Region_Rpt_Procurement_Moong_Urad_2026_E_WHR_For_Region" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>State Level Report 2026-27</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        body { background-color: #f8f9fa; font-family: 'Segoe UI', Arial, sans-serif; -webkit-print-color-adjust: exact; }
        .main-container { background: white; padding: 20px; border-radius: 8px; box-shadow: 0 0 15px rgba(0,0,0,0.1); margin-top: 20px; }
        .header-table { width: 100%; border-bottom: 3px solid #1a5276; margin-bottom: 15px; }
        .org-title { color: #1a5276; font-size: 22px; font-weight: bold; text-transform: uppercase; margin: 0; }
        
        .Grid { width: 100%; border-collapse: collapse !important; table-layout: auto; }
        .Grid th { background-color: #1a5276 !important; color: white !important; padding: 8px; border: 1px solid #444 !important; text-align: center; font-size: 15px; }
        .Grid td { padding: 6px; border: 1px solid #ccc !important; font-size: 14px; color: #333; }
        .footer-style td { background-color: #eee !important; font-weight: bold !important; border-top: 2px solid #000 !important; color: #000; text-align: right; }

        /* PRINT SETTINGS */
        @media print {
            @page { 
                size: A4 landscape; 
                margin: 20px; /* हर तरफ से 20px की जगह */
            }
            body { background: white !important; padding: 0; margin: 0; }
            .no-print { display: none !important; }
            .main-container { padding: 0 !important; box-shadow: none !important; width: 100%; margin: 0; }
            .Grid thead { display: table-header-group !important; }
            .Grid tr { page-break-inside: avoid !important; }
            .table-responsive { display: block !important; overflow: visible !important; }
        }
    </style>

    <script type="text/javascript">
        function exportToExcel() {
            var tab = document.getElementById('<%=GridView1.ClientID %>');
            var html = tab.outerHTML;
            // Excel के लिए Blob बनाना
            var blob = new Blob(['\ufeff', html], {
                type: 'application/vnd.ms-excel'
            });
            var url = URL.createObjectURL(blob);
            var a = document.createElement('a');
            a.href = url;
            a.download = "Procurement_Report_2026_27.xls";
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
                        <td style="width: 80px; padding: 10px;">
                            <img src="../../images/mpwlc.png" style="width: 70px;" />
                        </td>
                        <td class="text-center">
                            <h1 class="org-title">M.P. Warehousing & Logistics Corporation</h1>
                            <div style="font-weight: bold; font-size: 16px;">Moong, Urad WHR Status Procurement 2026-27</div>
                        </td>
                        <td style="width: 250px; text-align: right; font-size: 12px; padding-right: 10px;">
                            <strong>Date:</strong> <asp:Label ID="labelName" runat="server"></asp:Label><br />
                            <strong>Unit:</strong> Qty In M.T. 
                        </td>
                    </tr>
                </table>

                <div class="no-print mb-3 text-right">
                    <asp:LinkButton ID="btnback" runat="server" CssClass="btn btn-secondary btn-sm" OnClick="btnback_Click">Back</asp:LinkButton>
                    <button type="button" onclick="exportToExcel()" class="btn btn-success btn-sm">Export to Excel</button>
                    <button type="button" onclick="window.print()" class="btn btn-danger btn-sm">Print / Save PDF</button>
                </div>

                <div class="table-responsive">
                    <asp:GridView runat="server" ID="GridView1" ShowFooter="true" AutoGenerateColumns="false"
                        OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated"
                        CssClass="table table-bordered Grid">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DistrictName" HeaderText="District" />
                            <asp:BoundField DataField="Branch" HeaderText="Branch" />
                            <asp:TemplateField HeaderText="Acceptance Qty" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate><%# Eval("AcceptQty") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total WHR" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate><%# Eval("totalwhr") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Qty" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate><%# Eval("TotalQty") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Submissions" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate><%# Eval("NoOfWHR_Submission") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty Sub." ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate><%# Eval("Qty_Submission") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Prints" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate><%# Eval("NoOfWHR_Print") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty Print" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate><%# Eval("Qty_Print") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="footer-style" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>