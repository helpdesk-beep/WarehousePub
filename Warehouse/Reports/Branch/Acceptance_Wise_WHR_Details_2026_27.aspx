<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Reports/Branch/Acceptance_Wise_WHR_Details_2026_27.aspx.cs" Inherits="Reports_Branch_Acceptance_Wise_WHR_Details_2026_27" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Acceptance Wise WHR Details</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <style>
        body { background-color: #f8f9fa; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }
        .Grid { background-color: #fff; margin: 5px 0 10px 0; border: solid 1px #525252; border-collapse: collapse; width: 100%; }
        .Grid td { padding: 8px; border: solid 1px #c1c1c1; font-size: 13px; }
        .Grid th { padding: 10px 8px; color: #fff; background-color: #1a5276; border: solid 1px #525252; font-size: 14px; text-align: center; text-transform: uppercase; }
        .footer-style { background-color: #eee !important; color: #000 !important; font-weight: bold !important; text-align: right; }
        #container { display: flex; justify-content: space-between; align-items: center; background-color: #fff; padding: 15px; border-bottom: 3px solid #1a5276; }
        .org-title { font-size: 26px; font-weight: bold; color: #1a5276; }

        @media print {
            header, footer, nav, aside, .sidebar, .navbar, .no-print, .card { display: none !important; }
            body, html { background: white !important; margin: 0 !important; padding: 0 !important; visibility: hidden; }
            .printable-area, .printable-area * { visibility: visible; }
            .printable-area { position: absolute; left: 0; top: 0; width: 100% !important; }
            @page { size: A4 landscape; margin: 20px; }
            thead { display: table-header-group !important; }
            /* अगले पेजों पर 20px पैडिंग */
            .printable-area { padding: 20px !important; }
            .Grid { border: 1px solid #000 !important; width: 100% !important; }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="printable-area">
            <div id="container">
                <div><img src="../../images/mpwlc.png" style="width: 80px;" /></div>
                <div class="text-center">
                    <span class="org-title">M.P. WAREHOUSING & LOGISTICS CORPORATION</span><br />
                    <span style="font-size: 18px; font-weight: bold; color: #444;">Acceptance Wise WHR Details 2025-26</span>
                </div>
                <div style="text-align: right; font-size: 13px;">
                    <b>Date:</b> <asp:Label ID="labelName" runat="server"></asp:Label><br />
                    <b>Unit:</b> Qty In M.T.
                </div>
            </div>

            <div class="container-fluid py-3 no-print">
                <div style="text-align: right; margin-top: 15px; padding: 10px; background: #fff; border: 1px solid #ddd; border-radius: 8px;">
                    <input type="button" id="btnExport" value="Export to Excel" class="btn btn-success" style="margin-left: 10px;" />
                    <asp:Button ID="Button2" runat="server" Text="Print / Export PDF" CssClass="btn btn-danger" OnClientClick="window.print(); return false;" />
                </div>
            </div>

            <div class="table-responsive" style="margin-top: 10px; padding: 0 15px;">
                <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                    OnRowDataBound="GridView1_RowDataBound"
                    AutoGenerateColumns="false" CssClass="Grid table-bordered">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="50px">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                        <asp:BoundField DataField="Acceptance_No" HeaderText="Acceptance No" />
                        <asp:BoundField DataField="WHR_Id" HeaderText="WHR Id" />
                        <asp:BoundField DataField="Acceptance_Date" HeaderText="Date" />

                        <asp:TemplateField HeaderText="Acceptance Bags">
                            <ItemTemplate><asp:Label ID="lblAB" runat="server" Text='<%# Eval("No_of_Bags") %>'></asp:Label></ItemTemplate>
                            <FooterTemplate><asp:Label ID="lblTotalAB" runat="server"></asp:Label></FooterTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="WHR Bags">
                            <ItemTemplate><asp:Label ID="lblWB" runat="server" Text='<%# Eval("TotalBags_Received") %>'></asp:Label></ItemTemplate>
                            <FooterTemplate><asp:Label ID="lblTotalWB" runat="server"></asp:Label></FooterTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acceptance Qty">
                            <ItemTemplate><asp:Label ID="lblAQ" runat="server" Text='<%# Eval("Rec_Qty") %>'></asp:Label></ItemTemplate>
                            <FooterTemplate><asp:Label ID="lblTotalAQ" runat="server"></asp:Label></FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="WHR Qty">
                            <ItemTemplate><asp:Label ID="lblWQ" runat="server" Text='<%# Eval("Total_Qty_Received") %>'></asp:Label></ItemTemplate>
                            <FooterTemplate><asp:Label ID="lblTotalWQ" runat="server"></asp:Label></FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="footer-style" />
                </asp:GridView>
            </div>
        </div>

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src="../../JS/table2excel.js"></script>
        <script type="text/javascript">
            $(function () {
                $("#btnExport").click(function () {
                    $("[id*=GridView1]").table2excel({
                        filename: "Acceptance_Wise_WHR_Details_2026.xls"
                    });
                });
            });
        </script>
    </form>
</body>
</html>