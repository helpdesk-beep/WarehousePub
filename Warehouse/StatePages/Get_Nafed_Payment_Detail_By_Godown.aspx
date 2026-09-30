<%@ Page Title="Godown Wise Payment Breakdown" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Get_Nafed_Payment_Detail_By_Godown.aspx.cs" Inherits="StatePages_Get_Nafed_Payment_Detail_By_Godown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    
    <style type="text/css">
        .main-card { background-color: #ffffff; border: 1px solid #e3e6f0; border-radius: 0.35rem; box-shadow: 0 0.15rem 1.75rem 0 rgba(58, 59, 69, 0.15); padding: 1.5rem; margin-top: 1rem; }
        /* CHANGED: Green to Blue Theme Color */
        .page-header-title { font-size: 1.4rem; font-weight: 700; color: #4e73df; text-transform: uppercase; letter-spacing: 0.5px; border-bottom: 2px solid #eaecf4; padding-bottom: 0.75rem; margin-bottom: 1.5rem; }
        .info-panel { background-color: #e7f3fe; border-left: 6px solid #2196F3; padding: 15px; margin-bottom: 20px; border-radius: 0 4px 4px 0; }
        
        .custom-nafed-grid { width: 100%; border-collapse: collapse !important; }
        /* CHANGED: Header Background to Gradient Blue */
        .custom-nafed-grid th { background: linear-gradient(180deg, #4e73df 0%, #224abe 100%) !important; color: #ffffff !important; font-weight: 600; text-align: center; vertical-align: middle !important; padding: 10px 5px !important; border: 1px solid #cbd5e1 !important; font-size: 0.8rem; text-transform: uppercase; }
        .custom-nafed-grid td { padding: 8px 8px !important; vertical-align: middle !important; font-size: 0.82rem; border: 1px solid #cbd5e1 !important; color: #2d3748; }
        .custom-nafed-grid tr:nth-of-type(even) { background-color: #f8f9fc; }
        
        /* CHANGED: Added Premium Footer Total Style mapping */
        .custom-nafed-grid .footer-total-row td { background-color: #eff6ff !important; border-top: 2px solid #4e73df !important; border-bottom: 2px double #224abe !important; font-weight: bold !important; color: #1e3a8a !important; font-size: 0.85rem !important; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid main-card">
        
        <div class="page-header-title">
            <i class="fas fa-list-alt mr-2 text-primary"></i>Godown Wise Payment Breakdown Details
        </div>

        <!-- Info Header Context -->
        <div class="info-panel shadow-sm">
            <div class="row text-dark font-weight-bold">
                <div class="col-md-3">District: <asp:Label ID="lblDistrict" runat="server" CssClass="text-primary"></asp:Label></div>
                <div class="col-md-3">Godown: <asp:Label ID="lblGodown" runat="server" CssClass="text-primary"></asp:Label></div>
                <div class="col-md-3">Commodity: <asp:Label ID="lblCommodity" runat="server" CssClass="text-primary"></asp:Label></div>
                <div class="col-md-3 text-right">
                    <button type="button" class="btn btn-sm btn-dark" onclick="window.history.back();">
                        <i class="fas fa-arrow-left mr-1"></i> Back to Summary
                    </button>
                </div>
            </div>
        </div>

        <div class="table-responsive">
            <%-- CHANGED: Added ShowFooter="true" and OnRowDataBound event for totaling --%>
            <asp:GridView ID="gvNafedDetailsRow" runat="server" AutoGenerateColumns="False" 
                CssClass="table table-bordered custom-nafed-grid" Width="100%" 
                GridLines="Both" BorderColor="#e3e6f0" BorderStyle="Solid" BorderWidth="1px" 
                ShowFooter="true" OnRowDataBound="gvNafedDetailsRow_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="text-center">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="CropYear" HeaderText="Crop Year" ItemStyle-CssClass="text-center" />
                    <asp:BoundField DataField="Payment_Date" HeaderText="Transaction Date" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-CssClass="text-center" />
                    <asp:BoundField DataField="Payment" HeaderText="Payment Amt" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" />
                    <asp:BoundField DataField="TDS" HeaderText="TDS (PSS)" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="text-secondary" />
                    <asp:BoundField DataField="TDS1" HeaderText="TDS (PSF)" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="text-secondary" />
                    <asp:BoundField DataField="NetAmount" HeaderText="Net Amount" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="text-success font-weight-bold" />
                </Columns>
                <FooterStyle CssClass="footer-total-row" />
                <EmptyDataTemplate>
                    <div class="alert alert-warning text-center font-weight-bold m-0" role="alert">
                        <i class="fas fa-exclamation-triangle mr-1"></i> Is combination par koi details available nahi hain.
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>