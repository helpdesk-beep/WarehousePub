<%@ Page Title="Godown Wise Payment Details" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="ReceivePaymentFromNAFED_Details_For_HO.aspx.cs" Inherits="StatePages_ReceivePaymentFromNAFED_Details_For_HO" EnableEventValidation="false" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" />
    <style type="text/css">
        .detail-card { background-color: #ffffff; border: 1px solid #e3e6f0; border-radius: 0.35rem; box-shadow: 0 0.15rem 1.75rem 0 rgba(58, 59, 69, 0.15); padding: 1.5rem; margin-top: 1rem; }
        .page-title { font-size: 1.4rem; font-weight: 700; color: #008CBA; margin-bottom: 15px; border-bottom: 2px solid #008CBA; padding-bottom: 8px; text-transform: uppercase; letter-spacing: 0.5px; }
        .back-link { margin-bottom: 15px; display: inline-block; font-weight: bold; color: #4e73df; text-decoration: none; padding: 6px 12px; background: #eaecf4; border-radius: 4px; transition: all 0.2s; }
        .back-link:hover { background: #dddfeb; color: #224abe; text-decoration: none; }
        .info-panel { background-color: #e7f3fe; border-left: 6px solid #2196F3; padding: 12px; margin-bottom: 15px; font-weight: 600; color: #1e3a8a; border-radius: 0 4px 4px 0; }
        .action-buttons { margin-bottom: 15px; text-align: right; }
        
        /* Premium Grid System Overrides */
        .custom-detail-grid { border-collapse: collapse !important; width: 100%; }
        .custom-detail-grid th { background: linear-gradient(180deg, #008CBA 0%, #006699 100%) !important; color: #ffffff !important; font-weight: 600; text-align: center; vertical-align: middle !important; padding: 0.75rem !important; border: 1px solid #dee2e6 !important; font-size: 0.85rem; text-transform: uppercase; }
        .custom-detail-grid td { padding: 0.65rem !important; vertical-align: middle !important; font-size: 0.85rem; border: 1px solid #dee2e6 !important; color: #2d3748; }
        .custom-detail-grid tr:nth-of-type(even) { background-color: #f8fafc; }
        .custom-detail-grid tr:hover { background-color: #f1f5f9 !important; }
        
        /* Grand Total Target Rules */
        .grandtotal-row td { background-color: #e2f1f6 !important; border-top: 2px solid #008CBA !important; border-bottom: 2px solid #005f73 !important; font-weight: bold !important; color: #004085 !important; }

        /* Print Media Level Absolute Separation */
        @media print {
            body * { visibility: hidden; }
            .print-wrapper, .print-wrapper * { visibility: visible; }
            .print-wrapper { position: absolute; left: 0; top: 0; width: 100%; padding: 0; margin: 0; box-shadow: none; border: none; }
            .back-link, .action-buttons, .btn, #lnkBack { display: none !important; }
            .custom-detail-grid th { background: #008CBA !important; color: #ffffff !important; -webkit-print-color-adjust: exact; print-color-adjust: exact; }
        }
    </style>
    <script type="text/javascript">
        function CallPrint() {
            window.print();
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid print-wrapper detail-card">
        <div class="page-title">
            <i class="fas fa-warehouse mr-2"></i>Godown Wise Payment Details (NAFED)
        </div>
        
        <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/StatePages/ReceivePaymentFromNAFED_For_HO.aspx" CssClass="back-link"><i class="fas fa-arrow-left mr-1"></i> Back to Summary Page</asp:HyperLink>
        
        <div class="info-panel shadow-sm">
            <i class="far fa-calendar-check mr-1"></i> Selected Payment Date: <asp:Label ID="lblSelectedDate" runat="server"></asp:Label>
        </div>

        <div class="action-buttons">
            <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click" CssClass="btn btn-success btn-sm font-weight-bold mr-2 shadow-sm">
                <i class="fas fa-file-excel mr-1"></i> Export to Excel
            </asp:LinkButton>
            <asp:LinkButton ID="btnPrint" runat="server" OnClientClick="return CallPrint();" CssClass="btn btn-secondary btn-sm font-weight-bold shadow-sm">
                <i class="fas fa-print mr-1"></i> Print Report
            </asp:LinkButton>
        </div>

        <div class="table-responsive">
            <asp:GridView ID="gvPaymentDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                CssClass="table table-bordered custom-detail-grid" GridLines="Both" BorderColor="#e3e6f0" BorderStyle="Solid" BorderWidth="1px"
                OnRowDataBound="gvPaymentDetails_RowDataBound" OnDataBound="gvPaymentDetails_DataBound">
                <Columns>
                    <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="text-center">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        <ItemStyle Width="60px" />
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="District Name" HeaderText="District Name" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Depot Name" HeaderText="Depot Name" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Godown Name" HeaderText="Godown Name" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="font-weight-bold" />
                    
                    <asp:BoundField DataField="Total Storage Charges Bill Amount" HeaderText="Total Bill Amt" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="font-weight-bold" />
                    <asp:BoundField DataField="Total Storage Charges Bill" HeaderText="Total Bills" ItemStyle-CssClass="text-center text-muted" />
                    <asp:BoundField DataField="PSS Bill Amount" HeaderText="PSS Bill Amt" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="PSF Bill Amount" HeaderText="PSF Bill Amt" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="TDS Deduction From NAFED" HeaderText="TDS Deduction" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="text-danger" />
                    <asp:BoundField DataField="Other Deduction From NAFED" HeaderText="Other Deduct." DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Received Payment From NAFED" HeaderText="Received Payment" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="text-success font-weight-bold" />
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-danger text-center font-weight-bold m-0" role="alert">
                        इस तारीख के लिए कोई विस्तृत विवरण नहीं मिला (No Detailed Data Found).
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>