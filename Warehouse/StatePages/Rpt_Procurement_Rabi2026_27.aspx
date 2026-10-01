<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Procurement_Rabi2026_27.aspx.cs" Inherits="StatePages_Rpt_Procurement_Rabi2026_27" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wheat Procurement Report 2026-27</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body { background-color: #f8f9fa; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }
        .report-card { background: white; padding: 30px; border-radius: 12px; box-shadow: 0 4px 15px rgba(0,0,0,0.1); margin-top: 20px; }
        .header-table { width: 100%; border-bottom: 3px solid #1a5276; margin-bottom: 15px; }
        .org-title { color: #1a5276; font-size: 24px; font-weight: bold; text-transform: uppercase; }
        
        .Grid { width: 100%; border-collapse: collapse; margin-top: 15px; }
        .Grid th { background-color: #1a5276 !important; color: white !important; padding: 10px; border: 1px solid #444; text-align: center; font-size: 13px; }
        .Grid td { padding: 8px; border: 1px solid #ccc; font-size: 12px; }
        .footer-style td { background-color: #eee !important; font-weight: bold !important; color: #000; border-top: 2px solid #1a5276 !important; }

        @media print {
            header, footer, nav, aside, .sidebar, .navbar, .no-print, .btnMargin, .container.py-4 { display: none !important; }
            body, html { background: white !important; visibility: hidden; }
            .printable-area, .printable-area * { visibility: visible; }
            .printable-area { position: absolute; left: 0; top: 0; width: 100%; padding: 20px !important; }
            @page { size: A4 portrait; margin: 20px; }
            thead { display: table-header-group !important; }
            .report-card { border: none !important; box-shadow: none !important; width: 100% !important; }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid1 printable-area">
            <div class="report-card mx-auto" >
                <table class="header-table">
                    <tr>
                        <td style="width: 80px;"><img src="../../images/mpwlc.png" style="width: 70px;" /></td>
                        <td class="text-center">
                            <h1 class="org-title">M.P. Warehousing & Logistics Corporation</h1>
                            <div style="font-size: 18px; font-weight: 600;">Wheat WHR Status Procurement 2026-27</div>
                        </td>
                        <td style="width: 220px; text-align: right; font-size: 12px;">
                            <strong>Date:</strong> <asp:Label ID="labelName" runat="server"></asp:Label><br />
                            <strong>Unit:</strong> Qty In M.T.
                        </td>
                    </tr>
                </table>

                <div class="no-print d-flex justify-content-end mb-4">
                    <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-secondary btn-sm mr-2" OnClick="btnback_Click" />
                    <button type="button" onclick="window.print()" class="btn btn-danger btn-sm mr-2">Print / Save PDF</button>
                    <button type="button" id="btnExport" class="btn btn-success btn-sm">Export to Excel</button>
                </div>

                <div class="table-responsive">
                    <asp:GridView runat="server" ID="GridView1" ShowFooter="true" AutoGenerateColumns="false" 
                        CssClass="Grid table table-bordered" OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="50px">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Region" HeaderText="Region" ItemStyle-Width="150px" ItemStyle-Font-Bold="true" />
                            <asp:BoundField DataField="Region_ID" HeaderText="ID" Visible="false" />
                            <asp:BoundField DataField="DistrictName" HeaderText="District" />
                            <asp:BoundField DataField="Branch" HeaderText="Branch" />
                            <asp:TemplateField HeaderText="Acceptance Quantity" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate><asp:Label ID="lblAQ" runat="server" Text='<%# Eval("TotalQty", "{0:N2}") %>'></asp:Label></ItemTemplate>
                                <FooterTemplate><asp:Label ID="lblTotalAQ" runat="server" /></FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="WHR Quantity" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate><asp:Label ID="lblWHRQ" runat="server" Text='<%# Eval("AcceptQty", "{0:N2}") %>'></asp:Label></ItemTemplate>
                                <FooterTemplate><asp:Label ID="lblTotalWHRQ" runat="server" /></FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="% Over Qty" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate><%# Eval("Averg") %>%</ItemTemplate>
                                <FooterTemplate><asp:Label ID="lblAvgTotal" runat="server" /></FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="footer-style" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
        <script src="../JS/table2excel.js"></script>
        <script type="text/javascript">
            $(function () {
                $("#btnExport").click(function () {
                    $("[id*=GridView1]").table2excel({ filename: "Wheat_Procurement_2026_27.xls" });
                });
            });
        </script>
    </form>
</body>
</html>