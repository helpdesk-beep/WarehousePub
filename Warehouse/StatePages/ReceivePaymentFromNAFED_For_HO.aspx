<%@ Page Title="Date Wise Payment Received From NAFED" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="ReceivePaymentFromNAFED_For_HO.aspx.cs" Inherits="StatePages_ReceivePaymentFromNAFED_For_HO" EnableEventValidation="false" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    
    <style type="text/css">
        .main-card { background-color: #ffffff; border: 1px solid #e3e6f0; border-radius: 0.35rem; box-shadow: 0 0.15rem 1.75rem 0 rgba(58, 59, 69, 0.15); padding: 1.5rem; margin-top: 1rem; }
        .page-header-title { font-size: 1.5rem; font-weight: 700; color: #4e73df; text-transform: uppercase; letter-spacing: 0.5px; border-bottom: 2px solid #eaecf4; padding-bottom: 0.75rem; margin-bottom: 1.5rem; }
        .filter-section { background-color: #f8f9fc; border: 1px solid #e3e6f0; border-radius: 0.35rem; padding: 1.25rem; margin-bottom: 1.5rem; }
        .date-picker-input, .custom-select-dt { cursor: pointer; background-color: #ffffff !important; }
        
        .custom-grid { border-collapse: collapse !important; width: 100%; margin-bottom: 1rem; }
        .custom-grid th { background: linear-gradient(180deg, #4e73df 0%, #224abe 100%) !important; color: #ffffff !important; font-weight: 600; text-align: center; vertical-align: middle !important; padding: 0.75rem !important; border: 1px solid #dee2e6 !important; font-size: 0.85rem; text-transform: uppercase; }
        .custom-grid td { padding: 0.65rem !important; vertical-align: middle !important; font-size: 0.85rem; border: 1px solid #dee2e6 !important; color: #2d3748; }
        .custom-grid tr:nth-of-type(even) { background-color: #f8f9fc; }
        .custom-grid tr:hover { background-color: #eaecf4 !important; transition: background-color 0.2s ease-in-out; }
        
        /* Fixed Grand Total CSS classes */
        .grandtotal-row td { background-color: #eff6ff !important; border-top: 2px solid #4e73df !important; border-bottom: 2px solid #224abe !important; font-weight: bold !important; color: #1e3a8a !important; }

        @media print {
            body * { visibility: hidden; }
            .printable-area, .printable-area * { visibility: visible; }
            .printable-area { position: absolute; left: 0; top: 0; width: 100%; padding: 0; margin: 0; box-shadow: none; border: none; }
            .filter-section, .action-buttons, input[type="submit"], .btn { display: none !important; }
            .custom-grid th { background: #4e73df !important; color: #ffffff !important; -webkit-print-color-adjust: exact; print-color-adjust: exact; }
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
    <div class="container-fluid printable-area main-card">
        <div class="page-header-title">
            <i class="fas fa-money-check-alt mr-2 text-primary"></i>Date Wise Payment Received From NAFED
        </div>
        
        <div class="filter-section shadow-sm">
            <div class="form-row align-items-end">
                <div class="form-group col-xl-3 col-md-4 col-sm-6 mb-2 mb-md-0">
                    <label class="font-weight-bold text-dark"><i class="far fa-calendar-alt mr-1 text-secondary"></i>From Date:</label>
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control date-picker-input" TextMode="Date" onkeydown="return false;"></asp:TextBox>
                </div>
                <div class="form-group col-xl-3 col-md-4 col-sm-6 mb-2 mb-md-0">
                    <label class="font-weight-bold text-dark"><i class="far fa-calendar-alt mr-1 text-secondary"></i>To Date:</label>
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control date-picker-input" TextMode="Date" onkeydown="return false;"></asp:TextBox>
                </div>
                <div class="form-group col-xl-3 col-md-4 col-sm-12 mb-3 mb-md-0">
                    <label class="font-weight-bold text-dark"><i class="fas fa-map-marker-alt mr-1 text-secondary"></i>Select Region:</label>
                    <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control custom-select-dt"></asp:DropDownList>
                </div>
                <div class="form-group col-xl-3 col-md-12 text-md-right">
                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary btn-block font-weight-bold shadow-sm" OnClick="btnSearch_Click" Height="38px" style="padding-top: 7px;">
                        <i class="fas fa-search mr-1"></i> Search Data
                    </asp:LinkButton>
                </div>
            </div>
        </div>

        <div class="action-buttons mb-3 d-flex justify-content-end">
            <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click" CssClass="btn btn-success btn-sm font-weight-bold mr-2 shadow-sm">
                <i class="fas fa-file-excel mr-1"></i> Export to Excel
            </asp:LinkButton>
            <asp:LinkButton ID="btnPrint" runat="server" OnClientClick="return CallPrint();" CssClass="btn btn-secondary btn-sm font-weight-bold shadow-sm">
                <i class="fas fa-print mr-1"></i> Print Report
            </asp:LinkButton>
        </div>

        <div class="table-responsive">
            <asp:GridView ID="gvPaymentSummary" runat="server" AutoGenerateColumns="False" 
                CssClass="table table-bordered custom-grid" Width="100%" 
                GridLines="Both" BorderColor="#e3e6f0" BorderStyle="Solid" BorderWidth="1px"
                OnRowCommand="gvPaymentSummary_RowCommand" OnRowDataBound="gvPaymentSummary_RowDataBound" OnDataBound="gvPaymentSummary_DataBound">
                <Columns>
                    <asp:BoundField DataField="Region_ID" HeaderText="Region ID" ItemStyle-CssClass="text-center font-weight-bold text-muted" />
                    <asp:BoundField DataField="Region_Name" HeaderText="Region Name" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="font-weight-bold text-dark" />
                    
                    <asp:TemplateField HeaderText="Payment Date" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkPaymentDate" runat="server" 
                                Text='<%# Eval("Payment_Date") %>' 
                                CommandName="ViewDetails" 
                                CommandArgument='<%# Eval("Payment_Date") + "|" + Eval("Region_ID") %>'
                                Font-Bold="true" CssClass="text-primary text-decoration-none">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Total Storage Charges Bill Amount" HeaderText="Total Bill Amt" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="font-weight-bold" />
                    <asp:BoundField DataField="Total Storage Charges Bill" HeaderText="Total Bills" ItemStyle-CssClass="text-center text-secondary" />
                    <asp:BoundField DataField="PSS Bill Amount" HeaderText="PSS Bill Amt" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="PSF Bill Amount" HeaderText="PSF Bill Amt" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="TDS Deduction From NCCF" HeaderText="TDS Deduction" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="text-danger" />
                    <asp:BoundField DataField="Other Deduction From NCCF" HeaderText="Other Deduct." DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Received Payment From NCCF" HeaderText="Received Payment" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="text-success font-weight-bold" />
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-custom text-center font-weight-bold m-0" style="background-color: #fff5f5; color: #e53e3e; border: 1px solid #fed7d7;" role="alert">
                        <i class="fas fa-exclamation-circle mr-1"></i> कोई डेटा उपलब्ध नहीं है (No Data Found).
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>