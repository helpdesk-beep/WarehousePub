<%@ Page Title="Pending WHR Details" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/Reports/Branch/Rpt_Pending_WHR_Details_2026.aspx.cs" Inherits="Reports_Branch_Rpt_Pending_WHR_Details_2026" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        .report-card { background: #fff; padding: 25px; border-radius: 12px; box-shadow: 0 4px 20px rgba(0,0,0,0.08); border: 1px solid #dee2e6; margin-top: 20px; }
        .grid-header { background-color: #1a5276 !important; color: white !important; font-weight: 600 !important; text-transform: uppercase; font-size: 0.85rem; }
        .footer-style { background-color: #f8f9fa !important; color: #1a5276 !important; font-weight: bold !important; border-top: 2px solid #1a5276 !important; }
        
        @media print {
            header, nav, footer, aside, .sidebar, .navbar, .no-print { display: none !important; }
            body, html { background: white !important; visibility: hidden; }
            .printable-area, .printable-area * { visibility: visible; }
            .printable-area { position: absolute; left: 0; top: 0; width: 100% !important; padding: 20px !important; }
            @page { size: A4 landscape; margin: 20px; }
            thead { display: table-header-group !important; }
        }
    </style>

    <script type="text/javascript">
        function exportToExcelWithHeader() {
            var grid = document.getElementById('<%=Depositor_Gridview.ClientID %>');
            if (grid == null) {
                alert("No data found to export!");
                return;
            }

            // हेडर और ग्रिड का HTML निकालें
            var headerHtml = document.getElementById("HeaderTitleSection").innerHTML;
            var gridHtml = grid.outerHTML;

            var finalHtml = "<html><head><meta charset='utf-8'></head><body>" + headerHtml + "<br/>" + gridHtml + "</body></html>";

            var blob = new Blob([finalHtml], { type: "application/vnd.ms-excel" });
            var url = URL.createObjectURL(blob);
            var a = document.createElement("a");
            a.href = url;
            a.download = "Pending_WHR_Report_2026.xls";
            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid px-2 printable-area">
        <div class="report-card">
            <div id="HeaderTitleSection" class="mb-4">
                <div class="d-flex align-items-center justify-content-between border-bottom pb-3">
                    <div id="LogoPart">
                        <img src="../../images/mpwlc.png" style="width: 70px;" />
                    </div>
                    <div class="text-center" id="TitlePart">
                        <h2 style="color: #1a5276; font-weight: bold; margin: 0;">M.P. Warehousing & Logistics Corporation</h2>
                        <h5 class="text-secondary mt-1">Pending WHR Details Report 2026</h5>
                    </div>
                    <div class="text-end" style="font-size: 12px;" id="DatePart">
                        <strong>Date:</strong> <%= DateTime.Now.ToString("dd-MM-yyyy") %><br />
                        <strong>Unit:</strong> Qty In M.T.
                    </div>
                </div>
            </div>

            <div class="no-print d-flex justify-content-end mb-4">
                <button type="button" onclick="exportToExcelWithHeader()" class="btn btn-success btn-sm me-2 shadow-sm">
                    Export to Excel
                </button>
                <button type="button" onclick="window.print()" class="btn btn-danger btn-sm shadow-sm">
                    Print / Save PDF
                </button>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="Depositor_Gridview" runat="server" AutoGenerateColumns="False" Width="100%" 
                    CssClass="table table-bordered" OnPageIndexChanging="Depositor_Gridview_PageIndexChanging" 
                    AllowPaging="true" PageSize="100" >
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <HeaderStyle CssClass="grid-header text-center" Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="District_Name" HeaderText="District" HeaderStyle-CssClass="grid-header" />
                        <asp:BoundField DataField="Branch" HeaderText="Branch" HeaderStyle-CssClass="grid-header" />
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown" HeaderStyle-CssClass="grid-header" />
                        <asp:BoundField DataField="Acceptance_No" HeaderText="Acceptance No" HeaderStyle-CssClass="grid-header" />
                        <asp:BoundField DataField="Acceptance_Date" HeaderText="Acceptance Date" HeaderStyle-CssClass="grid-header" />
                        
                        <asp:TemplateField HeaderText="Send Bags">
                            <HeaderStyle CssClass="grid-header text-center" />
                            <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemTemplate><%# Eval("Recd_Bags") %></ItemTemplate>
                            <FooterTemplate><asp:Label ID="lblTotalBags" runat="server" /></FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Send Qty.">
                            <HeaderStyle CssClass="grid-header text-center" />
                            <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                            <ItemTemplate><%# Eval("Recd_Qty", "{0:N3}") %></ItemTemplate>
                            <FooterTemplate><asp:Label ID="lblTotalQty" runat="server" /></FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="footer-style" />
                    <PagerStyle CssClass="pagination justify-content-center pt-3 no-print" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>