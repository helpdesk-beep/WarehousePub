<%@ Page Title="Paddy WHR Details" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/Reports/Branch/Rpt_Paddy_WHR_Details_2026_27.aspx.cs" Inherits="Reports_Branch_Rpt_Paddy_WHR_Details_2026_27" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        /* स्क्रीन के लिए स्टाइल */
        .report-card {
            background: #ffffff;
            border-radius: 12px;
            box-shadow: 0 4px 20px rgba(0,0,0,0.08);
            border: 1px solid #dee2e6;
            margin-top: 20px;
            padding: 20px;
        }
        
        .grid-header {
            background-color: #1a5276 !important;
            color: white !important;
            font-weight: 600 !important;
            text-transform: uppercase;
            font-size: 0.85rem;
        }

        .footer-style {
            background-color: #f8f9fa !important;
            color: #1a5276 !important;
            font-weight: bold !important;
            border-top: 2px solid #1a5276 !important;
        }

        /* --- पूर्ण प्रिंट सुधार: मास्टर पेज हटाना और 20px पैडिंग --- */
        @media print {
            /* 1. मास्टर पेज के सभी बाहरी एलिमेंट्स को पूरी तरह छुपाएं */
            header, footer, nav, aside, .sidebar, .navbar, .left-side, .no-print, #header, #footer { 
                display: none !important; 
            }

            /* 2. मुख्य बॉडी को क्लीन करें */
            body, html {
                background: white !important;
                margin: 0 !important;
                padding: 0 !important;
                visibility: hidden; 
            }

            /* 3. @page डिक्लेरेशन: चारों तरफ से 20px की जगह सुनिश्चित करता है */
            @page { 
                size: A4 landscape; 
                margin: 20px; /* यहाँ से 20px की पैडिंग/मार्जिन सेट होती है */
            }

            /* 4. केवल रिपोर्ट वाले हिस्से (printable-area) को दिखाएं */
            .printable-area, .printable-area * {
                visibility: visible;
            }

            .printable-area {
                position: absolute;
                left: 0;
                top: 0;
                width: 100% !important;
                padding: 20px !important; /* प्रिंट में अंदरूनी पैडिंग */
            }

            /* 5. रिपोर्ट कार्ड डिज़ाइन को सादा करें */
            .report-card { 
                border: none !important; 
                box-shadow: none !important; 
                padding: 0 !important; 
                margin: 0 !important;
                width: 100% !important;
            }

            .table-responsive { overflow: visible !important; }
            
            /* टेबल हेडर हर पेज पर रिपीट हो */
            thead { display: table-header-group !important; }
            
            /* प्रिंट में फॉन्ट साइज थोड़ा छोटा ताकि डेटा फिट आए */
            .custom-grid td, .custom-grid th { font-size: 11px !important; }
        }
    </style>

    <script type="text/javascript">
        function exportToExcel() {
            var tab = document.getElementById('<%=Depositor_Gridview.ClientID %>');
            var html = tab.outerHTML;
            var blob = new Blob(['\ufeff', html], { type: 'application/vnd.ms-excel' });
            var url = URL.createObjectURL(blob);
            var a = document.createElement('a');
            a.href = url;
            a.download = "Paddy_WHR_Details_2026_27.xls";
            a.click();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid px-2 printable-area">
        <div class="report-card">
            
            <div class="d-flex justify-content-between align-items-center mb-4 border-bottom pb-3">
                <h4 class="text-primary fw-bold mb-0">Paddy WHR Details Report 2026-27</h4>
                <div class="no-print">
                    <button type="button" onclick="exportToExcel()" class="btn btn-success btn-sm me-2 shadow-sm">
                        Export to Excel
                    </button>
                    <button type="button" onclick="window.print()" class="btn btn-danger btn-sm shadow-sm">
                        Print Report
                    </button>
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="Depositor_Gridview" runat="server" AutoGenerateColumns="False" 
                    Width="100%" CssClass="table table-bordered custom-grid" 
                    OnPageIndexChanging="Depositor_Gridview_PageIndexChanging" AllowPaging="true" 
                    PageSize="100" ShowFooter="true" OnRowDataBound="Depositor_Gridview_RowDataBound">
                    
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <HeaderStyle CssClass="grid-header text-center" Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="District_Name" HeaderText="District" HeaderStyle-CssClass="grid-header" />
                        <asp:BoundField DataField="Branch" HeaderText="Branch" HeaderStyle-CssClass="grid-header" />
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown" HeaderStyle-CssClass="grid-header" />
                        <asp:BoundField DataField="DepositerNo" HeaderText="Depositor Form" HeaderStyle-CssClass="grid-header" />
                        <asp:BoundField DataField="Acceptance_No" HeaderText="Acceptance No" HeaderStyle-CssClass="grid-header" />
                        <asp:BoundField DataField="Acceptance_Date" HeaderText="Acceptance Date" HeaderStyle-CssClass="grid-header" />
                        <asp:BoundField DataField="TC_Number" HeaderText="TC Number" HeaderStyle-CssClass="grid-header" />
                        <asp:BoundField DataField="Truck_Number" HeaderText="Truck Number" HeaderStyle-CssClass="grid-header" />
                        
                        <asp:TemplateField HeaderText="Send Bags">
                            <HeaderStyle CssClass="grid-header text-center" />
                            <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                            <ItemTemplate><%# Eval("Recd_Bags") %></ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalBags" runat="server" />
                            </FooterTemplate>
                            <FooterStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Send Qty.">
                            <HeaderStyle CssClass="grid-header text-center" />
                            <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                            <ItemTemplate><%# Eval("Recd_Qty") %></ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalQty" runat="server" />
                            </FooterTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                    </Columns>

                    <FooterStyle CssClass="footer-style" />
                    <PagerStyle CssClass="pagination justify-content-center pt-3 no-print" />
                    <AlternatingRowStyle BackColor="#f8f9fa" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>